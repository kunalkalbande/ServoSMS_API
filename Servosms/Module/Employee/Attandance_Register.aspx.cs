/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using System.Data.SqlClient;
using RMG;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Servosms.Module.Employee
{
    /// <summary>
    /// Summary description for Attandance_Register.
    /// </summary>
    public partial class Attandance_Register : System.Web.UI.Page
    {
        public static int Row_No;
        string BaseUri = "http://localhost:64862";
        /// <summary>
        /// This method is used for setting the Session variable for userId 
        /// and also check accessing priviledges for particular user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            string uid = "";
            DBOperations.DBUtil dbobj = new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
            try
            {
                uid = (Session["User_Name"].ToString());


            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Addtandance_Registor,Method:Page_load Exception :" + ex.Message + "  userid " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                txtdate.Text = GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());


                panEmp.Visible = false;
                #region Check Privileges
                int i;
                string View_flag = "0", Add_Flag = "0", Edit_Flag = "0", Del_Flag = "0";
                string Module = "2";
                string SubModule = "1";
                string[,] Priv = (string[,])Session["Privileges"];
                for (i = 0; i < Priv.GetLength(0); i++)
                {
                    if (Priv[i, 0] == Module && Priv[i, 1] == SubModule)
                    {
                        View_flag = Priv[i, 2];
                        Add_Flag = Priv[i, 3];
                        Edit_Flag = Priv[i, 4];
                        Del_Flag = Priv[i, 5];
                        break;
                    }
                }
                if (View_flag == "0")
                {
                    //string msg="UnAthourized Visit to Attandance Register Page";
                    Response.Redirect("../../Sysitem/AccessDeny.aspx", false);
                    return;
                }
                if (Add_Flag == "0")
                    Btnsave.Enabled = false;
                #endregion
            }
        }
        public int flage = 0;
        protected void btnView_Click(object sender, System.EventArgs e)
        {
            flage = 1;
        }


        protected void Btnsave_Click(object sender, System.EventArgs e)
        {
            try
            {
                Save();
                MessageBox.Show("Attendance Save");
                //CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). Attendance of employee ID "+Request.Params.Get("tempEmpID"+i)+" Updated. userid :"+ Session["User_Name"].ToString());
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). EXCEPTION: " + ex.Message + " userid :" + Session["User_Name"].ToString());
            }
        }
        /// <summary>
        /// Save Function Add by vikas 7.11.2012
        /// </summary>
        public void Save()
        {
            try
            {
                EmployeeClass obj = new EmployeeClass();
                int Total_Rows = 0;
                SqlDataReader SqlDtr;
                string sql;
                string empid = "";
                Total_Rows = System.Convert.ToInt32(Request.Params.Get("lblTotal_Row"));
                for (int i = 0; i < Total_Rows; i++)
                {
                    string str = "";
                    if (Request.Params.Get("Chk" + i) != null)
                    {
                        EmployeeClass obj1 = new EmployeeClass();
                        obj1.Att_Date = GenUtil.str2MMDDYYYY(txtdate.Text.ToString());

                        obj1.Emp_ID = Request.Params.Get("lblEmpID" + i);
                        obj1.Status = "1";
                        string str1="";
                        string Att_Date= obj1.Att_Date;
                        string lblEmpID= Request.Params.Get("lblEmpID" + i); 
                        
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/AttandanceRegister/SaveAttandance?Att_Date=" + Att_Date + "&lblEmpID=" + lblEmpID).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                str1 = JsonConvert.DeserializeObject<string>(id);
                            }
                        }

                        //sql = "select Status from Attandance_Register where Att_Date='" + GenUtil.str2MMDDYYYY(txtdate.Text.ToString()) + "' and  Emp_ID=" + Request.Params.Get("lblEmpID" + i) + "";
                        //SqlDtr = obj.GetRecordSet(sql);
                        //while (SqlDtr.Read())
                        //{
                        //    str = SqlDtr.GetValue(0).ToString();
                        //}
                        //SqlDtr.Close();
                        str = str1;
                        if (str.Equals(""))
                        {
                            obj1.InsertEmployeeAttandance();
                            CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). Attendance of employee ID " + Request.Params.Get("lblEmpID" + i) + " Saved. userid :" + Session["User_Name"].ToString());
                            empid = empid + " " + Request.Params.Get("lblEmpID" + i) + "   ";
                        }
                        else
                        {
                            obj.Att_Date = GenUtil.str2MMDDYYYY(txtdate.Text.ToString());

                            obj.Emp_ID = Request.Params.Get("lblEmpID" + i);
                            obj.Status = "1";
                            obj.UpdateEmployeeAttandance();
                            //sql="insert into Attandance_Register (Att_Date,Emp_Id,Status)  values('"+Att_Date+"','"+Emp_ID+"','"+Status+"')";
                            //obj.ExecRecord(sql);
                            CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). Attendance of employee ID " + Request.Params.Get("tempEmpID" + i) + " Updated. userid :" + Session["User_Name"].ToString());
                        }
                    }
                    else
                    {
                        string Emp_ID = Request.Params.Get("lblEmpID" + i);
                        string Attan_Date = GenUtil.str2MMDDYYYY(txtdate.Text.ToString());
                        
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/AttandanceRegister/DeleteAttandance?Emp_ID=" + Emp_ID + "&Attan_Date=" + Attan_Date).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;                        
                            }
                        }
                        //sql = "delete from Attandance_Register where emp_id=" + Emp_ID + " and att_date='" + Attan_Date + "'";
                        //obj.ExecRecord(sql);
                    }
                }

                panEmp.Visible = false;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). EXCEPTION: " + ex.Message + " userid :" + Session["User_Name"].ToString());
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
