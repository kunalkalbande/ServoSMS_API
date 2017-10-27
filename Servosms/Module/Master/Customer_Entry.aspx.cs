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
using DBOperations;
using RMG;
using System.Text;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for Customer_Entry.
	/// </summary>
	public partial class Customer_Entry : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.TextBox TextBox1;
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
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:onpageload" + ex.Message+" EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			//txtbeatname.Visible=false;
			if(!IsPostBack)
			{
				try
				{
					# region Fill dropType
					PartiesClass obj=new PartiesClass();
					SqlDataReader SqlDtr;
					string sql;
					SqlDtr=obj.GetRecordSet("select * from CustomerType order by CustomerTypeName");
					DropType.Items.Clear();
					DropType.Items.Add("Select");
					while(SqlDtr.Read())
					{
						DropType.Items.Add(SqlDtr.GetValue(1).ToString());
					}
					SqlDtr.Close();
					#endregion
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
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
					if(View_flag=="0")
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					if(Add_Flag=="0")
						btnUpdate.Enabled=false;
					#endregion

					for(i=1;i<=30;i++)
					{
						DropCrDay.Items.Add(i.ToString ());
					}
					GetNextCustomerID();
					getbeat();
			
					#region Fetch Extra Cities from Database and add to the ComboBox
					sql="select distinct City from Beat_Master order by City asc";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCity.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					#endregion

					#region Fetch Extra Cities from Database and add to the ComboBox
					sql="select distinct state from Beat_Master order by state asc";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropState.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					#endregion

					#region Fetch Extra Cities from Database and add to the ComboBox
					sql="select distinct country from Beat_Master order by country asc";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCountry.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					#endregion

					#region Fetch SSR Employee from Employee Table and add to the ComboBox
					sql="select Emp_Name from Employee where Designation='Servo Sales Representative' order by Emp_Name";
					SqlDtr=obj.GetRecordSet(sql);
					DropSSR.Items.Clear();
					DropSSR.Items.Add("Select");
					while(SqlDtr.Read())
					{
						DropSSR.Items.Add(SqlDtr.GetValue(0).ToString());
					}
					SqlDtr.Close();
					#endregion
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:onpageload" + ex.Message+" EXCEPTION  "+uid);
				}
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
		/// This is used to concatenate the city,state,country for further use in javascript.
		/// </summary>
		public void getbeat()
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader sqldtr;
				string sql;
				string str="";
				sql="select city,state,country from beat_master";
				sqldtr=obj.GetRecordSet(sql);
				while(sqldtr.Read())
				{
					str=str+sqldtr.GetValue(0).ToString()+":";
					str=str+sqldtr.GetValue(1).ToString()+":";
					str=str+sqldtr.GetValue(2).ToString()+"#";
				}
				txtbeatname.Value=str;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
			}
		}

		/// <summary>
		/// Returns date in MM/DD/YYYY format.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];
			strarr=str.IndexOf("-")>0?str.Split(new char[]{'-'},str.Length): str.Split(new char[] { '/' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);
			return(dt);
		}

		/// <summary>
		/// Its checks the before save that the account period is inserted in organisaton table or not.
		/// </summary>
		/// <returns></returns>
		public bool checkAcc_Period()
		{
			int c = 0;
			try
			{
				SqlDataReader SqlDtr = null;
				
				dbobj.SelectQuery("Select count(Acc_Date_From) from Organisation",ref SqlDtr);
				if(SqlDtr.Read())
				{
					c = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());  
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:checkAcc_Period(). EXCEPTION : " + ex.Message+"  User_ID: "+uid);
			}

			if(c > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// This method is used to Save the Customer Information in customer table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			PartiesClass obj=new PartiesClass();
			try
			{
                StringBuilder errorMessage = new StringBuilder();
                if (txtTinNo.Text != string.Empty)
                {
                    string sPattern = "^[a-zA-Z0-9]+$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtTinNo.Text, sPattern))
                    {
                        errorMessage.Append("- Please Enter GSTIN No. in Alpha Numeric");
                        errorMessage.Append("\n");
                    }
                }
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage.ToString());
                    return;
                }
                if (!checkAcc_Period())
				{
					MessageBox.Show("Please enter the Accounts Period from Organization Details");
					return;
				}
				SqlDataReader SqlDtr;
				//string cname=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim())) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() )); 
				string cname="";
				if(txtFName.Text.Trim()!="")
					cname=txtFName.Text.Trim();
				
				//Coment by vikas 16.05.09
				/*if(txtMName.Text.Trim()!="")
					cname+=" "+txtMName.Text.Trim();
				if(txtLName.Text.Trim()!="")
					cname+=" "+txtLName.Text.Trim();*/

				//((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() ));
				string sql1="select Cust_Id from Customer where Cust_Name='"+cname.Trim()+"'";
				SqlDtr=obj.GetRecordSet(sql1);
				if(SqlDtr.HasRows)
				{
					MessageBox.Show("Customer Name  "+cname+" Already Exist");
					return;
				}
				SqlDtr.Close();
				sql1="select * from Ledger_Master where Ledger_Name='"+cname.Trim()+"'";
				SqlDtr=obj.GetRecordSet(sql1);
				if(SqlDtr.HasRows)
				{
					MessageBox.Show("Ledger Name  "+cname+" Already Exist");
					return;
				}
				SqlDtr.Close();
				if(!txtTinNo.Text.Trim().Equals(""))
				{
					sql1 = "Select Tin_No,Cust_ID from customer where Tin_No = '"+txtTinNo.Text.Trim()+"' and Tin_No<>'unregister' and Tin_No<>'UNREGISTERED' and Tin_No<>'Un Register'";
					SqlDtr= obj.GetRecordSet(sql1);
					if(SqlDtr.HasRows)
					{
						if(SqlDtr.Read())
						{
							if(!LblCustomerID.Text.Equals(SqlDtr["Cust_ID"].ToString() ) )
							{
								MessageBox.Show("The Tin No. "+txtTinNo.Text.Trim()+" Already Exist");
								return;
							}
						}
				
					}
					SqlDtr.Close();
				}
				else
				{
					txtTinNo.Text="Un Register";
				}
				obj.Cust_ID=LblCustomerID.Text;
				//obj.Cust_Name =StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim()))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim())); 
				//string Name = StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim() ));
				string Name = txtFName.Text.ToString().Trim();
			
				//Coment By vikas 16.05.09
				/*if(!txtMName.Text.ToString().Trim().Equals(""))
					Name += " "+txtMName.Text.ToString().Trim();
				if(!txtLName.Text.ToString().Trim().Equals(""))
					Name += " "+txtLName.Text.ToString().Trim();*/

				obj.Cust_Name =Name;
				obj.Cust_Type=DropType.SelectedItem.Value.ToString(); 
				obj.Address=txtAddress.Text.Trim();
				obj.City=DropCity.SelectedItem.Value.ToString();
				obj.State=DropState.SelectedItem.Value.ToString();
				obj.Country=DropCountry.SelectedItem.Value.ToString(); 
				if(DropSSR.SelectedIndex==0)
					obj.SSR="";
				else
				{
					SqlDtr=obj.GetRecordSet("select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"'");
					if(SqlDtr.Read())
						obj.SSR=SqlDtr["Emp_ID"].ToString();
					else
						obj.SSR="";
					SqlDtr.Close();
				}

				if(txtPhoneOff.Text=="")
					obj.Tel_Off="0";
				else
					obj.Tel_Off =txtPhoneOff.Text;
				if(txtPhoneRes.Text=="")
					obj.Tel_Res="0";
				else
					obj.Tel_Res =txtPhoneRes.Text;
				if(txtMobile.Text=="")
					obj.Mobile="0";
				else
					obj.Mobile =txtMobile.Text;
				obj.EMail =txtEMail.Text.Trim();
				obj.CR_Limit=txtCRLimit.Text ;
				if(DropCrDay.SelectedIndex==0)
					obj.CR_Days="0";
				else
					obj.CR_Days=DropCrDay.SelectedItem.Value.ToString();
				if(txtOpBalance.Text=="")
					obj.Op_Balance="0";
				else
					obj.Op_Balance=txtOpBalance.Text;
				obj.Balance_Type =DropBal.SelectedItem.Value.ToString();
				obj.EntryDate=ToMMddYYYY(DateTime.Now.Date.ToShortDateString()).ToString();
				obj.Tin_No = txtTinNo.Text.Trim();
				if(txtcode.Text.Trim().Equals(""))
					obj.sadbhavnacd="0";
				else
					obj.sadbhavnacd=txtcode.Text.Trim().ToString();
				obj.ContactPerson=txtContactPerson.Text;
				// Call to this method Inserts the customer details into the customer table.
				obj.InsertCustomer();
				MessageBox.Show("Customer Saved");
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs: Method:btnUpdate_Click "+" Cust Name  "+ obj.Cust_Name    +" Cust id  "+obj.Cust_ID +"Cust Type    "+ obj.Cust_Type  +"  Cust Address  "+ obj.Address   +" Cust City "+obj.City  +" Cust State  "+ obj.State   +" Cust Cuntry "+ ToMMddYYYY(DateTime.Now.Date.ToShortDateString()).ToShortDateString()+"obj.Country" +" Opening Balance  "+  obj.Op_Balance  +"  date  "+obj.EntryDate +"    IS  SAVED    User  "+uid );
				Clear();
				GetNextCustomerID();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs: Method:btnUpdate_Click "+" Cust Name  "+  obj.Cust_Name   +" Cust id  "+  obj.Cust_ID+"Cust Type    "+ obj.Cust_Type  +"  Cust Address  "+ obj.Address    +" Cust City "+ obj.City +" Cust State  "+ obj.State     +" Cust Cuntry "+ obj.Country +" Opening Balance  "+     obj.Op_Balance   +"  EXCEPTION "+ ex.Message  + "  User Type "+uid);
			}
		}

		/// <summary>
		/// Method to clear the form.
		/// </summary>
		public void Clear()
		{
			LblCustomerID.Text="";
			txtFName.Text="";
			//txtMName.Text="";
			//txtLName.Text="";
			txtEMail.Text="";
			txtOpBalance.Text="";
			txtAddress.Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry.SelectedIndex=0;
			DropType.SelectedIndex=0; 
			DropCrDay.SelectedIndex=0; 
			DropBal.SelectedIndex=0; 
			txtCRLimit.Text="";
			txtPhoneOff.Text="";
			txtPhoneRes.Text="";
			txtMobile.Text="";
			txtTinNo.Text = "";
			txtcode.Text="";
			DropSSR.SelectedIndex=0;
			txtContactPerson.Text="";
		}

		/// <summary>
		/// Returns the next Customer ID. from customer table.
		/// </summary>
		public void GetNextCustomerID()
		{
			try
			{
				PartiesClass obj=new PartiesClass();
				SqlDataReader SqlDtr;

				#region Fetch Next Customer ID
				SqlDtr =obj.GetNextCustomerID();
				while(SqlDtr.Read())
				{
					LblCustomerID.Text =SqlDtr.GetSqlValue(0).ToString ();
					if (LblCustomerID.Text=="Null")
						LblCustomerID.Text ="1001";
				}	
				SqlDtr.Close();
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs: Method:GetNextCustomerID().  EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}


		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Select the state and country according to the selected city.
			/*	try
				{
				
					InventoryClass  obj=new InventoryClass ();
					SqlDataReader SqlDtr;
					string sql;
					sql="select State,Country from Beat_Master where City='"+ DropCity.SelectedItem.Value +"'" ;
					SqlDtr=obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
				
						DropState.SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByValue(SqlDtr.GetValue(0).ToString()))));
						DropCountry.SelectedIndex=(DropCountry .Items.IndexOf((DropCountry.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
				
					} 
        
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:DropCity_SelectedIndexChanged" + "EXCEPTION  "+ex.Message+uid);
				
				}*/
		}

		protected void txtAddress_TextChanged(object sender, System.EventArgs e)
		{
		}

		protected void DropType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		protected void txtFName_TextChanged(object sender, System.EventArgs e)
		{
		}
	}
}