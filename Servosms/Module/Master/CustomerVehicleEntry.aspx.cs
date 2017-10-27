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
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using RMG;

namespace Servosms.Module.Master
{
	/// <summary>
	/// This form is not used in this project.
	/// </summary>
	public partial class CustomerVehicleEntry : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// Put user code to initialize the page here
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:pageload().  EXCEPTION:"+ ex.Message+".  User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				try
				{
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="4";
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
					if(Add_Flag=="0")
					{
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
						return;
					}
					#endregion 
					btnUpdate.Enabled = false;
					btnDelete.Enabled = false;
					getParties();
					getID();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:pageload().  EXCEPTION:"+ ex.Message+".  User: "+uid);
				}
			}
		}

		/// <summary>
		/// This is used to get the party name and city.
		/// </summary>
		public void getParties()
		{
			SqlDataReader SqlDtr = null;
			dbobj.SelectQuery("Select Cust_Name+':'+City from Customer",ref SqlDtr); 
			DropCustomerName.Items.Clear();
			DropCustomerName.Items.Add("Select"); 
			while(SqlDtr.Read())
			{
				DropCustomerName.Items.Add(SqlDtr.GetValue(0).ToString());    
			}
			SqlDtr.Close();
		}

		/// <summary>
		/// This method is used to generate ID auto
		/// </summary>
		public void getID()
		{
			SqlDataReader SqlDtr = null;
			dbobj.SelectQuery("Select max(CVE_ID)+1 from Customer_Vehicles ",ref SqlDtr);
			if(SqlDtr.Read())
			{
				if(!SqlDtr.GetValue(0).ToString().Equals(""))
					lblID.Text = SqlDtr.GetValue(0).ToString();
			}
			SqlDtr.Close();
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
		/// This method is used to insert the record in the database
		/// </summary>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropCustomerName.SelectedIndex == 0)
				{
					MessageBox.Show("Please the Customer Name"); 
					return;
				}
				SqlDataReader SqlDtr = null;
				string cust_id = "";
				string temp = DropCustomerName.SelectedItem.Text;  
				string[] strArr = temp.Split(new char[] {':'},temp.Length);
				dbobj.SelectQuery("Select Cust_ID from Customer where Cust_Name='"+strArr[0].Trim()+"' and City = '"+strArr[1].Trim()+"'" ,ref SqlDtr); 
				if(SqlDtr.Read())
				{
					cust_id = SqlDtr.GetValue(0).ToString();  
				}
				SqlDtr.Close();
				int c= 0;
				dbobj.Insert_or_Update("insert into Customer_Vehicles values("+lblID.Text+","+cust_id+",'"+txtVehicle1.Text.Trim()+"','"+txtVehicle2.Text.Trim()+"','"+txtVehicle3.Text.Trim()+"','"+txtVehicle4.Text.Trim()+"','"+txtVehicle5.Text.Trim()+"','"+txtVehicle6.Text.Trim()+"','"+txtVehicle7.Text.Trim()+"','"+txtVehicle8.Text.Trim()+"','"+txtVehicle9.Text.Trim()+"','"+txtVehicle10.Text.Trim()+"')" ,ref c); 
				if(c > 0)
				{
					MessageBox.Show("Customer Vehicle Entry Saved" );
					CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnSave_Click(). Customer Vehicle Entry with ID = "+lblID.Text+"  Saved.   User: "+uid); 
					clear(); 
					getID();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnSave_Click().  EXCEPTION:"+ ex.Message+".  User: "+uid);
			}
		}
		
		/// <summary>
		/// This method is used to clear the form.
		/// </summary>
		public void clear()
		{
			DropCustomerName.SelectedIndex = 0;
			txtVehicle1.Text = "";  
			txtVehicle2.Text = "";
			txtVehicle3.Text = "";
			txtVehicle4.Text = "";
			txtVehicle5.Text = "";
			txtVehicle6.Text = "";
			txtVehicle7.Text = "";
			txtVehicle8.Text = "";
			txtVehicle9.Text = "";
			txtVehicle10.Text = "";
		}

		/// <summary>
		/// This method is used to fill CustomerVehicleEntry ID from Customer_Vehicle table in the dropdownlist on edit time
		/// </summary>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				lblID.Visible = false;
				DropID.Visible =true;
				btnUpdate.Enabled = true;
				btnDelete.Enabled = true; 
				btnSave.Enabled = false; 
				DropID.Items.Clear();
				DropID.Items.Add("Select" );
				SqlDataReader SqlDtr =null;
				dbobj.SelectQuery("Select CVE_ID from Customer_Vehicles" ,ref SqlDtr);
				while(SqlDtr.Read())
				{
					DropID.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close(); 
				btnEdit.Visible  = false; 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnEdit_Click().  EXCEPTION:"+ ex.Message+".  User: "+uid);
			}
		}

		protected void DropID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(DropID.SelectedIndex == 0)
				{
					MessageBox.Show("Please Select the CVE ID"); 
					return;
				}
				SqlDataReader SqlDtr = null;
				SqlDataReader SqlDtr1 = null;
				dbobj.SelectQuery("Select * from Customer_Vehicles where CVE_ID = "+DropID.SelectedItem.Text.Trim(),ref SqlDtr); 
				if(SqlDtr.Read())
				{
					dbobj.SelectQuery("Select Cust_Name+':'+City from Customer where Cust_ID = "+SqlDtr.GetValue(1).ToString(),ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						DropCustomerName.SelectedIndex = DropCustomerName.Items.IndexOf(DropCustomerName.Items.FindByValue(SqlDtr1.GetValue(0).ToString()));        
					}
					SqlDtr1.Close();

					txtVehicle1.Text = SqlDtr.GetValue(2).ToString();  
					txtVehicle2.Text = SqlDtr.GetValue(3).ToString();  
					txtVehicle3.Text = SqlDtr.GetValue(4).ToString();  
					txtVehicle4.Text = SqlDtr.GetValue(5).ToString();  
					txtVehicle5.Text = SqlDtr.GetValue(6).ToString();  
					txtVehicle6.Text = SqlDtr.GetValue(7).ToString();  
					txtVehicle7.Text = SqlDtr.GetValue(8).ToString();  
					txtVehicle8.Text = SqlDtr.GetValue(9).ToString();  
					txtVehicle9.Text = SqlDtr.GetValue(10).ToString();  
					txtVehicle10.Text = SqlDtr.GetValue(11).ToString();  
				}
				SqlDtr.Close(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:DropID_SelectedIndexChanged().  EXCEPTION:"+ ex.Message+".  User: "+uid); 
			}
		}

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropID.SelectedIndex == 0)
				{
					MessageBox.Show("Please the CVE ID"); 
					return;
				}
				if(DropCustomerName.SelectedIndex == 0)
				{
					MessageBox.Show("Please the Customer Name"); 
					return;
				}

				SqlDataReader SqlDtr = null;
				string cust_id = "";
				string temp = DropCustomerName.SelectedItem.Text;  
				string[] strArr = temp.Split(new char[] {':'},temp.Length);
				dbobj.SelectQuery("Select Cust_ID from Customer where Cust_Name='"+strArr[0].Trim()+"' and City = '"+strArr[1].Trim()+"'" ,ref SqlDtr); 
				if(SqlDtr.Read())
				{
					cust_id = SqlDtr.GetValue(0).ToString();  
				}
				SqlDtr.Close();

				int c= 0;
				dbobj.Insert_or_Update("Update Customer_Vehicles set Cust_ID = "+cust_id +",Vehicle_No1 = '"+txtVehicle1.Text.Trim()+"',Vehicle_No2 = '"+txtVehicle2.Text.Trim()+"',Vehicle_No3 = '"+txtVehicle3.Text.Trim()+"',Vehicle_No4 = '"+txtVehicle4.Text.Trim()+"',Vehicle_No5 = '"+txtVehicle5.Text.Trim()+"',Vehicle_No6 = '"+txtVehicle6.Text.Trim()+"',Vehicle_No7 = '"+txtVehicle7.Text.Trim()+"',Vehicle_No8 = '"+txtVehicle8.Text.Trim()+"',Vehicle_No9 = '"+txtVehicle9.Text.Trim()+"',Vehicle_No10 = '"+txtVehicle10.Text.Trim()+"' where CVE_ID = "+DropID.SelectedItem.Text.Trim(),ref c); 
				if( c >0)
				{
					MessageBox.Show("Customer Vehicle Entry Updated");
					CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnUpdate_Click(). Customer Vehicle Entry with ID = "+DropID.SelectedItem.Text.Trim()+" Updated.   User: "+uid); 
					lblID.Visible  =true;
					DropID.Visible = false;
					btnEdit.Visible = true; 
					btnUpdate.Enabled = false;
					btnDelete.Enabled = false; 
					btnSave.Enabled = true; 
					clear();
					getID();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnUpdate_Click().  EXCEPTION:"+ ex.Message+".  User: "+uid); 
			}
		}

		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropID.SelectedIndex == 0)
				{
					MessageBox.Show("Please the CVE ID"); 
					return;
				}
				int c = 0;
				dbobj.Insert_or_Update("Delete from Customer_Vehicles where CVE_ID="+DropID.SelectedItem.Text.Trim(),ref c); 

				if( c >0)
				{
					MessageBox.Show("Customer Vehicle Entry Deleted"); 
					CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnDelete_Click(). Customer Vehicle Entry with ID = "+DropID.SelectedItem.Text.Trim()+" Deleted.   User: "+uid); 
					lblID.Visible  =true;
					DropID.Visible = false;
					btnEdit.Visible = true; 
					btnUpdate.Enabled = false;
					btnDelete.Enabled = false; 
					btnSave.Enabled = true; 
					clear();
					getID();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerVehicleEntry.aspx,Method:btnDelete_Click().  EXCEPTION:"+ ex.Message+".  User: "+uid); 
			}
		}
	}
}