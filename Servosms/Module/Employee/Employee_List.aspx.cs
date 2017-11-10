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

namespace Servosms.Module.Employee
{
	/// <summary>
	/// Summary description for Employee_List.
	/// </summary>
	public partial class Employee_List : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable Main;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
        string BaseUri = "http://localhost:64862";
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
			this.GridSearch.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GridSearch_PageIndexChanged);
			this.GridSearch.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.GridSearch_DeleteCommand);

		}
		#endregion

		/// <summary>
		/// This method is used for searching the record.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cache["strorderby"]="Emp_ID ASC";
				Session["Column"]="Emp_ID";
				Session["order"]="ASC";
				Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:EmployeeList.aspx,Class:Employee.cs,Method:btnSearch_Click"+  "EXCEPTION"+ ex.Message+  uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}
		
		/// <summary>
		/// This method is used for binding the datagrid.
		/// </summary>
		public  void Bindthedata()
		{
			//GridSearch.CurrentPageIndex=0;
			DataSet ds=new DataSet();
			EmployeeClass  obj=new EmployeeClass();

            DataSet dataset = new DataSet();
            string ID1 = txtEmpID.Text.Trim().ToString();
            string name1 = txtName.Text.Trim().ToString();
            string desig1 = txtDesig.Text.Trim().ToString();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/EmployeeClass/ShowEmployeeInfo1?ID1=" + ID1 + "&name1=" + name1 + "&desig1=" + desig1).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    dataset = JsonConvert.DeserializeObject<DataSet>(id);
                }
            }
            //ds =obj.ShowEmployeeInfo(txtEmpID.Text.Trim ().ToString(),txtName.Text.Trim ().ToString() , txtDesig.Text.Trim ().ToString());
            //             
                ds = dataset;
            DataTable dt=ds.Tables[0];
			DataView dv=new DataView(dt);
			dv.Sort=System.Convert.ToString(Cache["strorderby"]);
			//****
			//if(ds.Tables[0].Rows.Count>0)
			if(dv.Count>0)
			{
				GridSearch.DataSource=dv;
				GridSearch.DataBind();
				GridSearch.Visible=true;
			}
			else
			{
				MessageBox.Show("Employee Not Found");
				GridSearch.Visible=false;
			}
		}
		
		/// <summary>
		/// this is used to make sorting the datagrid onclicking of the datagridheader
		/// </summary>
		string strorderby="";
		public void sortcommand_click(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["order"].Equals("ASC"))
					{
						strorderby=e.SortExpression.ToString()+" DESC";
						Session["order"]="DESC";
					}
					else
					{
						strorderby=e.SortExpression.ToString()+" ASC";
						Session["order"]="ASC";
					}
				}
				else
				{
					strorderby=e.SortExpression.ToString()+" ASC";
					Session["order"]="ASC";
				}
				Session["column"]=e.SortExpression.ToString();
				Cache["strorderby"]=strorderby;
				Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:EmployeeList.aspx,Class:Employee.cs,Method:SortCommand_Click"+"  "+ex.Message+"  USERID  "+ uid);
			}
		}
		
		/// <summary>
		/// This method is used for changing the index of page.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void GridSearch_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			GridSearch.CurrentPageIndex =(int)e.NewPageIndex;
			try
			{    
				Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:EmployeeList.aspx,Class:Employee.cs,Method:GridSearch_PageIndexChanged"+  "EXCEPTION"+ ex.Message+  uid);
			}
		}

		/// <summary>
		/// This method is used for setting the Session variable for userId
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
				CreateLogFiles.ErrorLog("Form:EmployeeList.aspx,Class:Employee.cs,Method:page_Load"+  "EXCEPTION"+ ex.Message+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				#region Check Privileges
				checkPrivileges();
				if(View_flag=="0")
				{
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				#endregion 
			}
		}

		/// <summary>
		/// This method checks the user privileges from session.
		/// </summary>
		public void checkPrivileges()
		{
			int i;
			string Module="2";
			string SubModule="2";
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
		}

		/// <summary>
		/// This method is used to delete the partucular Employee record select from datagrid.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void GridSearch_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			checkPrivileges();
			if(Del_Flag =="0")
			{
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}
			SqlConnection sqlConn=new SqlConnection();
			try
			{
				int Count=0;
				//SqlDataReader rdr=null;

                string str;
                int Count1=0;
                str = e.Item.Cells[0].Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/EmployeeList/FetchData?str="+str).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        Count1 = JsonConvert.DeserializeObject<int>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                Count = Count1;
    //            dbobj.SelectQuery("select count(*) from AccountsLedgerTable where Ledger_ID='"+e.Item.Cells[0].Text+"'",ref rdr);
				//if(rdr.Read())
				//{
				//	Count=int.Parse(rdr.GetValue(0).ToString());
				//}
				if(Count>1)
				{
					MessageBox.Show("Please Remove The All Transaction Concerning Employee");
					return;
				}
				string strCon=System.Configuration.ConfigurationSettings.AppSettings["Servosms"];
				SqlCommand sqlCmd=new SqlCommand();

                str = e.Item.Cells[1].Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/EmployeeList/DeleteEmployee?str=" + str).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        //LedgerName = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                //            sqlCmd.CommandText="Delete from Employee Where Emp_ID='"+e.Item.Cells[1].Text+"'";
                //sqlConn.ConnectionString=strCon;
                //sqlConn.Open();
                //sqlCmd.Connection=sqlConn;
                //sqlCmd.ExecuteNonQuery();
                ////*********	
                //sqlConn.Close();
                //sqlCmd.Dispose();
                str = e.Item.Cells[0].Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/EmployeeList/DeleteLedgerMaster?str=" + str).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        //LedgerName = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
    //            sqlCmd.CommandText="Delete from Ledger_Master Where Ledger_ID='"+e.Item.Cells[0].Text+"'";
				//sqlConn.ConnectionString=strCon;
				//sqlConn.Open();
				//sqlCmd.Connection=sqlConn;
				//sqlCmd.ExecuteNonQuery();
				//sqlConn.Close();
				//sqlCmd.Dispose();
				CreateLogFiles.ErrorLog("Form:EmployeeList.aspx,Class:Employee.cs,Method:GridSearch_DeleteCommand"+" Employee "+ e.Item.Cells[1].Text+" IS DELETED "+"  "+" USER ID "+ uid);
				MessageBox.Show("Employee Deleted");
				Response.Redirect("Employee_List.aspx",false);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:EmployeeList.aspx,Class:Employee.cs,Method:GridSearch_DeleteCommand"+" Employee "+ e.Item.Cells[1].Text+" IS DELETED "+"  "+ex.Message+"  USERID  "+ uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}

		protected void GridSearch_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}