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
using DBOperations;
using System.Text;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for Customer_Update_aspx.
	/// </summary>
	public partial class Customer_Update_aspx : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		/// <summary>
		/// Put user code to initialize the page here
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values and also fill some 
		/// additional information and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// and also fatch the customer information according to select customer ID in comes from url.
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
				CreateLogFiles.ErrorLog("Form:Customer_Update.aspx,Method:on_pageload,Class:PartiesClass.cs " + "EXCEPTION  "+ex.Message+"  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				try
				{
					//*************
					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					SqlDataReader SqlDtr1; 
					cmd=new SqlCommand("select * from CustomerType order by CustomerTypeName",con);
					SqlDtr1=cmd.ExecuteReader();
					DropCustType.Items.Clear();
					DropCustType.Items.Add("SELECT");
					if(SqlDtr1.HasRows)
					{
						while(SqlDtr1.Read())
						{
							DropCustType.Items.Add(SqlDtr1.GetValue(1).ToString());
						}
					}
					con.Close();
					SqlDtr1.Close();
					cmd.Dispose();
					getbeat();
					//********
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
					if(Edit_Flag=="0")
						btnUpdate.Enabled=false;
					#endregion

					for(i=1;i<=30;i++)
					{
						DropCrDay.Items.Add(i.ToString ());
					}
						
					LblCustomerID.Text = Request.QueryString.Get("ID");
					PartiesClass obj=new PartiesClass();
					PartiesClass obj1=new PartiesClass();
					SqlDataReader SqlDtr;
					string sql;

					#region Fetch Extra Cities from Database and add to the ComboBox
					sql="select City from Beat_Master order by city";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCity.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					sql="select distinct State from Beat_Master";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropState.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					sql="select distinct Country from Beat_Master";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCountry.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					#endregion

					#region Fetch SSR Employee from Employee Table and add to the ComboBox
					sql="select Emp_Name from Employee where Designation='Servo Sales Representative' and status=1 order by Emp_Name";
					SqlDtr=obj.GetRecordSet(sql);
					DropSSR.Items.Clear();
					DropSSR.Items.Add("Select");
					while(SqlDtr.Read())
					{
						DropSSR.Items.Add(SqlDtr.GetValue(0).ToString());
					}
					SqlDtr.Close();
					#endregion

					#region Fatch the Customer information according to Customer ID and fill the textboxes and dropdownlist
					SqlDtr = obj.CustomerList(LblCustomerID.Text.ToString (),"","");
					while (SqlDtr.Read ())
					{
						lblName.Text =SqlDtr.GetValue(1).ToString ();
						TempCustName.Text=SqlDtr.GetValue(1).ToString ();
						DropCustType.SelectedIndex =DropCustType.Items.IndexOf(DropCustType.Items.FindByValue(SqlDtr.GetValue(2).ToString ()));
						txtAddress.Text =SqlDtr.GetValue(3).ToString ();
						DropCity.SelectedIndex =DropCity.Items.IndexOf(DropCity.Items.FindByValue(SqlDtr.GetValue(4).ToString ()));
						DropState.SelectedIndex =DropState.Items.IndexOf(DropState.Items.FindByValue(SqlDtr.GetValue(5).ToString ()));
						DropCountry.SelectedIndex =DropCountry.Items.IndexOf(DropCountry.Items.FindByValue(SqlDtr.GetValue(6).ToString ()));
						if(SqlDtr.GetValue(7).ToString().Equals("0"))
							txtPhoneRes.Text = "";
						else
							txtPhoneRes.Text =SqlDtr.GetValue(7).ToString ();

						if(SqlDtr.GetValue(8).ToString().Equals("0"))
							txtPhoneOff.Text = "";
						else
							txtPhoneOff.Text =SqlDtr.GetValue(8).ToString ();

						if(SqlDtr.GetValue(9).ToString().Equals("0"))
							txtMobile.Text = "";
						else
							txtMobile.Text =SqlDtr.GetValue(9).ToString ();

						txtEMail.Text =SqlDtr.GetValue(10).ToString ();
						txtCRLimit.Text =SqlDtr.GetValue(11).ToString ();
						DropCrDay.SelectedIndex=DropCrDay.Items.IndexOf(DropCrDay.Items.FindByValue(SqlDtr.GetValue(12).ToString ()));
						if(SqlDtr["SSR"].ToString()!="" && SqlDtr["SSR"].ToString()!=null)
						{
							SqlDtr1=obj1.GetRecordSet("select Emp_Name from Employee where Emp_ID='"+SqlDtr["SSR"].ToString()+"'");
							if(SqlDtr1.Read())
								DropSSR.SelectedIndex=DropSSR.Items.IndexOf(DropSSR.Items.FindByValue(SqlDtr1["Emp_Name"].ToString ()));
							else
								DropSSR.SelectedIndex=0;
							SqlDtr1.Close();
						}
						else
							DropSSR.SelectedIndex=0;
						txtOpBalance.Text =SqlDtr.GetValue(13).ToString ();
						DropBal.SelectedIndex=DropBal.Items.IndexOf(DropBal.Items.FindByValue(SqlDtr.GetValue(14).ToString ()));
						txtTinNo.Text = SqlDtr.GetValue(15).ToString().Trim();
						txtcode.Text=SqlDtr.GetValue(17).ToString().Trim();
						txtContactPerson.Text=SqlDtr["ContactPerson"].ToString();
					}
					SqlDtr.Close(); 
					#endregion
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Customer_Update.aspx,Method:on_pageload,Class:PartiesClass.cs " + "EXCEPTION  "+ex.Message+"  "+uid);
				}
			}		
		}

		/// <summary>
		/// This is used to concating the city,state,country for using in javascript.
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
				txtbeatname.Text=str;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Update.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
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
		/// This method is used to check the customer name, ledger name and Tin no can not be duplicate 
		/// in customer or Ledger_Master table. After that call stored procedure 'ProCustomerUpdate' for 
		/// update the custoemr information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			PartiesClass obj=new PartiesClass();
			SqlDataReader SqlDtr = null;
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
                if (!TempCustName.Text.ToLower().Trim().Equals(lblName.Text.ToLower().Trim()))
				{
					string ename=lblName.Text.Trim();
					string sql1="select * from Customer where Cust_Name='"+ename.Trim()+"'";
					SqlDtr=obj.GetRecordSet(sql1);
					if(SqlDtr.HasRows)
					{
						MessageBox.Show("Customer Name  "+ename+" Already Exist");
						return;
					}
					SqlDtr.Close();
					sql1="select * from Ledger_Master where Ledger_Name='"+ename.Trim()+"'";
					SqlDtr=obj.GetRecordSet(sql1);
					if(SqlDtr.HasRows)
					{
						MessageBox.Show("Ledger Name  "+ename+" Already Exist");
						return;
					}
					SqlDtr.Close();
				}
		        
				if(!txtTinNo.Text.Trim().Equals(""))
				{
					string sql1 = "Select Tin_No,Cust_ID from customer where Tin_No = '"+txtTinNo.Text.Trim()+"' and Tin_No<>'unregister' and Tin_No<>'UNREGISTERED' and Tin_No<>'Un Register'";
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

				obj.Cust_ID = LblCustomerID.Text;
				obj.Cust_Name  =lblName.Text.ToString().Trim();
				obj.TempCustName=TempCustName.Text;
				obj.Cust_Type =DropCustType.SelectedItem.Value.ToString ();  
				obj.Address =txtAddress.Text.ToString();
				obj.City =DropCity.SelectedItem.Value.ToString ();
				obj.State=DropState.SelectedItem.Value.ToString ();
				obj.Country=DropCountry.SelectedItem.Value.ToString ();
				obj.EMail =txtEMail.Text.ToString ();
				if(txtcode.Text=="")
					obj.sadbhavnacd="0";
				else
					obj.sadbhavnacd=txtcode.Text.ToString();
					
				if(txtPhoneRes.Text=="")
					obj.Tel_Res="0";
				else
					obj.Tel_Res =txtPhoneRes.Text ;
				if(txtPhoneOff.Text=="")
					obj.Tel_Off  ="0";
				else
					obj.Tel_Off  =txtPhoneOff.Text ;
				if(txtMobile.Text=="")
					obj.Mobile="0";
				else
					obj.Mobile =txtMobile.Text;
				if(txtCRLimit.Text=="")
					obj.CR_Limit="0";
				else
					obj.CR_Limit =txtCRLimit.Text;
				if(DropCrDay.SelectedIndex==0)
					obj.CR_Days="0";
				else
					obj.CR_Days=DropCrDay.SelectedItem.Value.ToString();
				string OpBal="0";
				if(txtOpBalance.Text=="")
					obj.Op_Balance="0";
				else
				{
					obj.Op_Balance  =txtOpBalance.Text;
					OpBal=txtOpBalance.Text;
				}
				obj.Balance_Type =DropBal.SelectedItem.Value.ToString();
				obj.Tin_No= txtTinNo.Text.Trim(); 
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
				obj.ContactPerson=txtContactPerson.Text;
				// Calls method to update the customers details.
				obj.UpdateCustomer();
				/* Comment by Mahesh b'coz balance updated by existing procedure
				string Ledger_ID = "";
				dbobj.SelectQuery("select Ledger_ID from Ledger_Master where Ledger_Name=(select Cust_Name from Customer where Cust_ID='"+LblCustomerID.Text.Trim()+"')",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Ledger_ID = SqlDtr.GetValue(0).ToString();
				}
				SqlDtr.Close();
				//UpdateCustomerBalance(LblCustomerID.Text.Trim(),Ledger_ID);
				object op=null;
				dbobj.ExecProc(OprType.Update,"UpdateAccountsLedgerForCustomer",ref op,"@Ledger_ID",Ledger_ID);
				*/
				MessageBox.Show("Customer Updated");
				Clear();
				CreateLogFiles.ErrorLog("Form:Customer_Updates.aspx,Class:PartiesClass.cs: Method:btnUpdate_Click "+" Recored of  - Cust Name  "+ obj.Cust_Name    +" Cust id  "+obj.Cust_ID + "   IS   UPDATED "+"  User Type "+uid );
				Response.Redirect("Customer_List.aspx",false);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Updates.aspx,Class:PartiesClass.cs: Method:btnUpdate_Click "+" recored of  "+" Cust Name  "+ obj.Cust_Name    + " IS UPDATED "+" EXCEPTION   "+ex.Message+"  User Type "+uid );//Err.ErrorLog(Server.MapPath("Logs/ErrorLog"),"Form:Customer_Update.aspx,Method:btnUpdate_Click,Class:PartiesClass.cs " + ex.Message);
			}
		}	
	
		/// <summary>
		/// This method used to clear the form.
		/// </summary>
		public void Clear()
		{
			txtEMail.Text="";
			txtAddress.Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry.SelectedIndex=0;
			DropCustType.SelectedIndex=0; 
			DropCrDay.SelectedIndex=0; 
			DropBal.SelectedIndex=0; 
			txtCRLimit.Text="";
			txtPhoneOff.Text="";
			txtPhoneRes.Text="";
			txtMobile.Text="";
			txtOpBalance.Text="";
			txtcode.Text="";
			TempCustName.Text="";
		}

		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Select the state and country according to the selected city.
			try
			{
				//				InventoryClass  obj=new InventoryClass ();
				//				SqlDataReader SqlDtr;
				//				string sql;
				//				sql="select State,Country from Beat_Master where City='"+ DropCity.SelectedItem.Value +"'" ;
				//				SqlDtr=obj.GetRecordSet(sql); 
				//				while(SqlDtr.Read())
				//				{
				//				
				//					DropState.SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByText(SqlDtr.GetValue(0).ToString()))));
				//													
				//					DropCountry.SelectedIndex=(DropCountry .Items.IndexOf((DropCountry.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
				//				
				//				} 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Update.aspx,Class:Customer_Update.aspx.cs ,Method:DropCity_SelectedIndexChanged" + "EXCEPTION  "+ex.Message+uid);
			}
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <param name="Cust_ID"></param>
		/// <param name="Ledger_ID"></param>
		public void UpdateCustomerBalance(string Cust_ID,string Ledger_ID)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=null;
			SqlCommand cmd;
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			double Bal=0;
			string BalType="";
			int i=0;
			string str="select * from AccountsLedgerTable where Ledger_ID='"+Ledger_ID+"' order by entry_date";
			rdr=obj.GetRecordSet(str);
			Bal=0;
			BalType="";
			i=0;
			while(rdr.Read())
			{
				if(i==0)
				{
					BalType=rdr["Bal_Type"].ToString();
					i++;
				}
				if(double.Parse(rdr["Credit_Amount"].ToString())!=0)
				{
					if(BalType=="Cr")
					{
						Bal+=double.Parse(rdr["Credit_Amount"].ToString());
						BalType="Cr";
					}
					else
					{
						Bal-=double.Parse(rdr["Credit_Amount"].ToString());
						if(Bal<0)
						{
							Bal=double.Parse(Bal.ToString().Substring(1));
							BalType="Cr";
						}
						else
							BalType="Dr";
					}
				}
				else if(double.Parse(rdr["Debit_Amount"].ToString())!=0)
				{
					if(BalType=="Dr")
						Bal+=double.Parse(rdr["Debit_Amount"].ToString());
					else
					{
						Bal-=double.Parse(rdr["Debit_Amount"].ToString());
						if(Bal<0)
						{
							Bal=double.Parse(Bal.ToString().Substring(1));
							BalType="Dr";
						}
						else
							BalType="Cr";
					}
				}
				Con.Open();
				cmd = new SqlCommand("update AccountsLedgerTable set Balance='"+Bal.ToString()+"',Bal_Type='"+BalType+"' where Ledger_ID='"+rdr["Ledger_ID"].ToString()+"' and Particulars='"+rdr["Particulars"].ToString()+"' ",Con);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				Con.Close();
							
			}
			rdr.Close();
			
			string str1="select * from CustomerLedgerTable where CustID='"+Cust_ID+"' order by entrydate";
			rdr=obj.GetRecordSet(str1);
			Bal=0;
			i=0;
			BalType="";
			while(rdr.Read())
			{
				if(i==0)
				{
					BalType=rdr["BalanceType"].ToString();
					i++;
				}
				if(double.Parse(rdr["CreditAmount"].ToString())!=0)
				{
					if(BalType=="Cr.")
					{
						Bal+=double.Parse(rdr["CreditAmount"].ToString());
						BalType="Cr.";
					}
					else
					{
						Bal-=double.Parse(rdr["CreditAmount"].ToString());
						if(Bal<0)
						{
							Bal=double.Parse(Bal.ToString().Substring(1));
							BalType="Cr.";
						}
						else
							BalType="Dr.";
					}
				}
				else if(double.Parse(rdr["DebitAmount"].ToString())!=0)
				{
					if(BalType=="Dr.")
						Bal+=double.Parse(rdr["DebitAmount"].ToString());
					else
					{
						Bal-=double.Parse(rdr["DebitAmount"].ToString());
						if(Bal<0)
						{
							Bal=double.Parse(Bal.ToString().Substring(1));
							BalType="Dr.";
						}
						else
							BalType="Cr.";
					}
				}
				Con.Open();
				cmd = new SqlCommand("update CustomerLedgerTable set Balance='"+Bal.ToString()+"',BalanceType='"+BalType+"' where CustID='"+rdr["CustID"].ToString()+"' and Particular='"+rdr["Particular"].ToString()+"' ",Con);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				Con.Close();
			}
			rdr.Close();
		}

		protected void lblName_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}