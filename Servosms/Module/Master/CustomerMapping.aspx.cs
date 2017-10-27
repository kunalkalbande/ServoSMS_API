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
using Servosms.Sysitem.Classes ;
using RMG;
using DBOperations;


namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for CustomerMapping.
	/// </summary>
	public partial class CustomerMapping : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid="";
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values and also 
		/// check accessing priviledges for particular user.
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
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			try
			{
				if(!Page.IsPostBack)
				{
					/*InventoryClass obj = new InventoryClass();
					SqlDataReader rdr=obj.GetRecordSet("select Emp_Name from Employee where Designation='Servo Sales Representative'");
					DropSSRName.Items.Clear();
					DropSSRName.Items.Add("Select");
					while(rdr.Read())
					{
						DropSSRName.Items.Add(rdr["Emp_Name"].ToString());
					}
					rdr.Close();*/
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="5";
					string[,] Priv=(string[,]) Session["Privileges"];
					for(i=0;i<Priv.GetLength(0);i++)
					{
						if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
						{						
							View_Flag=Priv[i,2];
							Add_Flag=Priv[i,3];
							Edit_Flag=Priv[i,4];
							Del_Flag=Priv[i,5];
							break;
						}
					}	
					Cache["Add"]=Add_Flag;
					Cache["View"]=View_Flag;
					Cache["Edit"]=Edit_Flag;
					Cache["Del"]=Del_Flag;
					if(View_Flag=="0")
					{
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
						return;
					}
					if(Add_Flag=="0")
						btnSubmit.Enabled=false;
					if(Edit_Flag=="0")
						DropSSRName.Enabled=false;
					#endregion
					FillList();
					FillCustomer();
				}
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:PageLoad,     User  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:PageLoad,  EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the Employee name whose designation have Servo Sales Representative
		/// and fill the dropdownlist when page is loaded.
		/// </summary>
		public void FillCustomer()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader SqlDtr;
				string sql="select emp_name from employee where Designation='Servo Sales Representative' order by emp_name";
				SqlDtr = obj.GetRecordSet (sql);
				DropSSRName.Items.Clear();
				DropSSRName.Items.Add("Select");;
				while(SqlDtr.Read ())
				{
					DropSSRName.Items.Add(SqlDtr.GetValue(0).ToString ());
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:FillCustomer"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the all customer from customer table and fill the list.
		/// </summary>
		public void FillList()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader SqlDtr;
				string sql="select Cust_Name from customer order by cust_name";
				SqlDtr = obj.GetRecordSet (sql);
				ListCustomer.Items.Clear();
				while(SqlDtr.Read ())
				{
					ListCustomer.Items.Add(SqlDtr.GetValue(0).ToString ());
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:FillList"+ ex.Message+"  EXCEPTION  "+uid);
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


		/// <summary>
		/// This method is used to move the customer from one list to anather.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnIn_Click(object sender, System.EventArgs e)
		{
			try
			{
				while(ListCustomer.SelectedItem.Selected)
				{
					ListassignCustomer.Items.Add(ListCustomer.SelectedItem.Value);  
					ListCustomer.Items.Remove(ListCustomer.SelectedItem.Value);
				}
			}
			catch(Exception)
			{
				
			}
		}

		/// <summary>
		/// This method is used to move the customer from one list to anather.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnout_Click(object sender, System.EventArgs e)
		{
			try
			{
				while(ListassignCustomer.SelectedItem.Selected)
				{
					ListCustomer.Items.Add(ListassignCustomer.SelectedItem.Value);
					ListassignCustomer.Items.Remove(ListassignCustomer.SelectedItem.Value);  
				}
			}
			catch(Exception)
			{
			}		
		}

		/// <summary>
		/// This method is used to move all customer from assign customer list and remove the all customer
		/// from customer list vice versa.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btn1_Click(object sender, System.EventArgs e)
		{
			if(btn1.Text.Trim().Equals(">>"))
			{
				try
				{
					btn1.Text="<<";
					foreach(System.Web.UI.WebControls.ListItem lst in ListCustomer.Items)
						ListassignCustomer.Items.Add(lst);
					ListCustomer.Items.Clear();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:cmdrpt_Click"+ ex.Message);
				}
			}
			else
			{
				try
				{
					btn1.Text=">>";
					foreach(System.Web.UI.WebControls.ListItem lst in ListassignCustomer.Items)
						ListCustomer.Items.Add(lst);
					ListassignCustomer.Items.Clear();
					
				}
				catch(Exception ex)
				{	
					CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:btnOut_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
				}
			}
		}

		/// <summary>
		/// This method is used to fatch the all customer whose ssr is select from dropdownlist
		/// and fill the data in both list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropSSRName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select Cust_Name from Customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSRName.SelectedItem.Text+"') order by Cust_Name");
				ListassignCustomer.Items.Clear();
				FillList();
				while(rdr.Read())
				{
					ListassignCustomer.Items.Add(rdr["Cust_Name"].ToString());
					ListCustomer.Items.Remove(rdr["Cust_Name"].ToString());
				}
				rdr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:DropSSRName_SelectedIndexChanged,   EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}

		/// <summary>
		/// This method is used to save or update the customer according to ssr in customer table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader SqlDtr=null;
				int x=0;
				string EmpID="";
				string sql="select Emp_ID from Employee where Emp_Name='"+DropSSRName.SelectedItem.Text+"'";
				SqlDtr = obj.GetRecordSet(sql);
				if(SqlDtr.Read ())
				{
					EmpID=SqlDtr["Emp_ID"].ToString();
				}
				SqlDtr.Close();
				for(int i=0;i<ListassignCustomer.Items.Count;++i)
				{
					ListassignCustomer.SelectedIndex =i;
					string pname = ListassignCustomer.SelectedItem.Value; 
					dbobj.Insert_or_Update("update Customer set ssr='"+EmpID+"' where Cust_Name='"+pname+"'",ref x);
				}
				MessageBox.Show("Customer Mapping Update"); 
				FillList();
				DropSSRName.SelectedIndex=0;
				ListassignCustomer.Items.Clear();
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:btnSubmit_Click,  Update Customer Mapping,  User  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:btnSubmit_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}
	}
}