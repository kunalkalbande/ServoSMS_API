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
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Employee
{
	/// <summary>
	/// Summary description for Leave_Register.
	/// </summary>
	public partial class Leave_Register : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
        string BaseUri = "http://localhost:64862";
        /// <summary>
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Leave_Register.aspx,Method:pageload"+"EXCEPTION  "+ ex.Message+ uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);	
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="2";
				string SubModule="3";
				string[,] Priv=(string[,]) Session["Privileges"];
				for(i=0;i<Priv.GetLength(0);i++)
				{
					if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
					{						
						View_flag=Priv[i,2];
						Add_Flag=Priv[i,3];
						Edit_Flag=Priv[i,4];
						Del_Flag=Priv[i,5];
						break;
					}
				}	
				if(View_flag=="0")
				{
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				//***********
				if(Add_Flag=="0")
					btnApply.Enabled=false;
				//***********
				#endregion
				try
				{
					// Sets todays date in from and to date text boxes.
					txtDateFrom.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
					txtDateTO.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();  

					#region Fetch Employee ID and Name of All Employee
					EmployeeClass  obj=new EmployeeClass();
					

                    List<string> Employee = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/LeaveRegister/GetEmployeeData").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            Employee = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                            
                    }
                    foreach (var item in Employee)
                    {
                        DropEmpName.Items.Add(item);
                    }
                    //               sql ="select Emp_ID,Emp_Name from Employee where status='1' order by Emp_Name";
                    //SqlDtr=obj.GetRecordSet(sql);
                    //while(SqlDtr.Read())
                    //{
                    //	DropEmpName.Items.Add(SqlDtr.GetValue(0).ToString ()+":"+SqlDtr.GetValue(1).ToString());				
                    //}
                    //SqlDtr.Close();
                    #endregion
                }
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Leave_Register.aspx,Method:pageload"+"EXCEPTION  "+ ex.Message+ uid);
				}
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTO.Text = Request.Form["txtDateTO"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTO"].ToString().Trim();
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

		/// <summary>
		/// method converts the dd/mm/yyyy date to mm/dd/yyyy
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.Split(new char[]{'/'},str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// This method is used to save the employee leave with the help of ProLeaveEntry Procedure
		/// before check the date if Leave save already in this given date then first delete the record 
		/// and save the record update otherwise save the record in Leave_Register table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnApply_Click(object sender, System.EventArgs e)
		{
			EmployeeClass obj=new EmployeeClass();
			try
			{
				#region Check Validation
				if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(txtDateTO.Text))>0)
				{
					MessageBox.Show("Date From Should Be Less Than Date To");
					return;
				}
				if(DateTime.Compare(ToMMddYYYY(GenUtil.str2DDMMYYYY(GenUtil.trimDate(DateTime.Now.ToString()))),ToMMddYYYY(txtDateFrom.Text))>0)
				{
					MessageBox.Show("Date From Should Be Gratter Or Equal to Date To");
					return;
				}
				int Count=0,Count1=0;
                string str1, str2, str3;
                str1=DropEmpName.SelectedItem.Value.Substring(0, DropEmpName.SelectedItem.Value.LastIndexOf(":")); ;
                str2 = GenUtil.str2MMDDYYYY(txtDateFrom.Text);
                str3 = GenUtil.str2MMDDYYYY(txtDateFrom.Text);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LeaveRegister/CountLeaveRegister?str1=" + str1 + "&str2=" + str2 + "&str3=" + str3).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        Count1 = JsonConvert.DeserializeObject<int>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();

                }
                Count = Count1;

    //            string str = "select count(*) from Leave_Register where Emp_ID='"+DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":"))+"' and cast(floor(cast(cast(date_from as datetime) as float)) as datetime) <='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(date_to as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and isSanction=1";
				//dbobj.ExecuteScalar(str,ref Count);
				if(Count>0)
				{
					MessageBox.Show("Employee already allow leave is given date");
					return;
				}

                str1 = DropEmpName.SelectedItem.Value.Substring(0, DropEmpName.SelectedItem.Value.LastIndexOf(":"));
                str2 = GenUtil.str2MMDDYYYY(txtDateFrom.Text);
                str3 = GenUtil.str2MMDDYYYY(txtDateFrom.Text);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LeaveRegister/GetLeaveRegister?str1=" + str1 + "&str2=" + str2 + "&str3=" + str3).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        Count1 = JsonConvert.DeserializeObject<int>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();

                }
                Count = Count1;
    //            str = "select * from Leave_Register where Emp_ID='"+DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":"))+"' and cast(floor(cast(cast(date_from as datetime) as float)) as datetime) <='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(date_to as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and isSanction=0";
				//dbobj.ExecuteScalar(str,ref Count);
				if(Count>0)
				{
					int x=0;
					dbobj.Insert_or_Update("delete from Leave_Register where Emp_ID='"+DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":"))+"' and cast(floor(cast(cast(date_from as datetime) as float)) as datetime) <='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(date_to as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and isSanction=0",ref x);
				}
				#endregion
				obj.Emp_Name = DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":")) ;
				obj.Date_From  =ToMMddYYYY(txtDateFrom.Text).ToShortDateString(); 
				obj.Date_To  = ToMMddYYYY(txtDateTO.Text).ToShortDateString();
				obj.Reason =StringUtil.FirstCharUpper(txtReason.Text.ToString());
				obj.Days = txtleaveday.Text.ToString().Trim();           //add by vikas 17.11.2012
				// calls fuction to insert the leave
				obj.InsertLeave();
				MessageBox.Show("Leave Application Saved");
				Clear();
				CreateLogFiles.ErrorLog("Form:Leave_Register.aspx,Method:btnApply_Click"+"  empname  :" + obj.Emp_Name   +" datefrom  "+ obj.Date_From  + "        uptodate   "+obj.Date_To  +" for Reason "+ obj.Reason  +"  is saved  "+"  userid:   "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Leave_Register.aspx,Method:btnApply_Click"+"  empname  :" + obj.Emp_Name    +"  is saved  "+"  EXCEPTION  "+ex.Message  +"  userid:   "+uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}

		/// <summary>
		/// Method to clear the form.
		/// </summary>
		public void Clear()
		{
			DropEmpName.SelectedIndex=0;
			txtDateFrom.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
			txtDateTO.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();  
			txtReason.Text="";
			txtleaveday.Text="";
		}

		protected void txtDateFrom_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
