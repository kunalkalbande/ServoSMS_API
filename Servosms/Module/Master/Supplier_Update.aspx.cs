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
	/// Summary description for Supplier_Update_aspx.
	/// </summary>
	public partial class Supplier_Update_aspx : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// and also fatch the vendor information according to select supplier ID in comes from url.
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
				CreateLogFiles.ErrorLog("Form:Supplier_Update.aspx,Class:PartiesClass.cs,Method:page_load "+ ex.Message+" EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				try
				{
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="12";
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
					}
					if(Edit_Flag=="0")
						btnUpdate.Enabled=false;
					#endregion
					getbeat();
					// Fills the credit limit combo with the 30 numbers.
					for(i=1;i<=30;i++)
					{
						DropCrDay.Items.Add(i.ToString ());
					}
					lblSupplierID.Text = Request.QueryString.Get("ID");
					PartiesClass obj=new PartiesClass();
					SqlDataReader SqlDtr;
					string sql;

					#region Fetch Extra Cities from Database and add to the ComboBox
					sql="select distinct Country from Beat_Master";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCountry.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					sql="select distinct City from Beat_Master";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCity.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
				
				
					string sql1;
					sql1="select  distinct State from Beat_Master";
					SqlDtr=obj.GetRecordSet(sql1);
					while(SqlDtr.Read())
					{
						DropState.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
				
					#endregion

					SqlDtr = obj.SupplierList(lblSupplierID.Text.ToString (),"",""  );
					while (SqlDtr.Read ())
					{
						lblName.Text =SqlDtr.GetValue(1).ToString ();
						TempVenderName.Text=SqlDtr.GetValue(1).ToString ();
						DropSuppType.SelectedIndex=DropSuppType.Items.IndexOf(DropSuppType.Items.FindByValue(SqlDtr.GetValue(2).ToString ()));
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
						txtOpBalance.Text =SqlDtr.GetValue(11).ToString ();
						DropBal.SelectedIndex=DropBal.Items.IndexOf(DropBal.Items.FindByValue(SqlDtr.GetValue(12).ToString ()));
						DropCrDay.SelectedIndex=DropCrDay.Items.IndexOf(DropCrDay.Items.FindByValue(SqlDtr.GetValue(13).ToString ()));
						txtTinNo.Text = SqlDtr.GetValue(14).ToString();  
					}
					SqlDtr.Close();
				}	
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Supplier_Update.aspx,Class:PartiesClass.cs,Method:page_load EXCEPTION: "+ ex.Message+" User_ID: "+uid);

				}
			}
		}

		/// <summary>
		/// This method is used to concatenate the city,state,country for javascripting.
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
				CreateLogFiles.ErrorLog("Form:Supplier_Update.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
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
		/// this method is used to update the vendor information with the help of ProsupplierUpdate Procedure
		/// before check the vendor name should not be duplicate after that update the vendor balance.
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
                string sql1="";
				if(!TempVenderName.Text.ToLower().Trim().Equals(lblName.Text.ToLower().Trim()))
				{
					string sname=lblName.Text.Trim();
					sql1="select * from Supplier where Supp_Name='"+sname.Trim()+"'";
					SqlDtr=obj.GetRecordSet(sql1);
					if(SqlDtr.HasRows)
					{
						MessageBox.Show("Vendor Name  "+sname+" Already Exist");
						return;
					}
					SqlDtr.Close();
					sql1="select * from Ledger_Master where Ledger_Name='"+sname.Trim()+"'";
					SqlDtr=obj.GetRecordSet(sql1);
					if(SqlDtr.HasRows)
					{
						MessageBox.Show("Ledger Name  "+sname+" Already Exist");
						return;
					}
					SqlDtr.Close();
				}
				//				sql1 = "Select Tin_No,Supp_ID from supplier where Tin_No = '"+txtTinNo.Text.Trim()+"'";
				//				SqlDtr= obj.GetRecordSet(sql1);
				//				if(SqlDtr.HasRows)
				//				{
				//					if(SqlDtr.Read())
				//					{
				//						if(!lblSupplierID.Text.Equals(SqlDtr["Supp_ID"].ToString() ) )
				//						{
				//							MessageBox.Show("The Tin No. "+txtTinNo.Text.Trim()+" Already Exist");
				//							return;
				//						}
				//					}
				//				
				//				}
				//				SqlDtr.Close();
				
				obj.Supp_ID = lblSupplierID.Text;
				obj.Supp_Name =lblName.Text.ToString();
				obj.TempCustName=TempVenderName.Text;
				obj.Supp_Type =DropSuppType.SelectedItem.Value.ToString ();
				obj.Address =txtAddress.Text.ToString();
				obj.City =DropCity.SelectedItem.Value.ToString ();
				obj.State=DropState.SelectedItem.Value.ToString ();
				obj.Country=DropCountry.SelectedItem.Value.ToString ();
				obj.EMail =txtEMail.Text.ToString();
				if(txtPhoneRes.Text=="")
					obj.Tel_Res="0";
				else

					obj.Tel_Res =txtPhoneRes.Text ;
				if(txtPhoneOff.Text=="")
					obj.Tel_Off="0";
				else
					obj.Tel_Off  =txtPhoneOff.Text ;
				if(txtMobile.Text=="")
					obj.Mobile="0";
				else
					obj.Mobile =txtMobile.Text;
				if(txtOpBalance.Text=="")
					obj.Op_Balance="0";
				else

					obj.Op_Balance  =txtOpBalance.Text;
				obj.Balance_Type =DropBal.SelectedItem.Value.ToString(); 
				if(DropCrDay.SelectedIndex==0)
					obj.CR_Days="0";
				else
					obj.CR_Days=DropCrDay.SelectedItem.Value.ToString();
				obj.Tin_No = txtTinNo.Text.Trim(); 
				// call function to update the supplier details
				obj.UpdateSupplier();	
				string Ledger_ID = "";
				dbobj.SelectQuery("select Ledger_ID from Ledger_Master where Ledger_Name=(select Supp_Name from Supplier where Supp_ID='"+lblSupplierID.Text.Trim()+"')",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Ledger_ID = SqlDtr.GetValue(0).ToString();
				}
				UpdateCustomerBalance(Ledger_ID);
				MessageBox.Show("Vendor Updated");
				Clear();
				CreateLogFiles.ErrorLog("Form:Supllier_update.aspx, Method:btnUpdate_Click "+"   Supplier_ID "+	obj.Supp_ID  +"   Supplier Name  "+lblName.Text.ToString()+"  IS UPDATED   "+"  user  "+uid  );
				Response.Redirect("Supplier_List.aspx",false);

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Supplier_Update.aspx,Class:PartiesClass.cs,Method:btnUpdate_Click().  EXCEPTION   " +ex.Message+"  user  "+uid  );
			}
		}

		/// <summary>
		/// This method is used to clear the whole form.
		/// </summary>
		public void Clear()
		{
			txtEMail.Text="";
			txtAddress.Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry.SelectedIndex=0;
			DropSuppType.SelectedIndex=0; 
			DropBal.SelectedIndex=0; 
			txtPhoneOff.Text="";
			txtPhoneRes.Text="";
			txtMobile.Text="";
			txtOpBalance.Text=""; 
			TempVenderName.Text="";
		}


		/// <summary>
		/// This method is used to select the state and country according to selected city.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				/*InventoryClass  obj=new InventoryClass ();
				SqlDataReader SqlDtr;
				string sql;
				sql="select State,Country from Beat_Master Where City='"+DropCity.SelectedItem.Text+"'";
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					DropState.SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByValue(SqlDtr.GetValue(0).ToString()))));
					DropCountry.SelectedIndex=(DropCountry .Items.IndexOf((DropCountry.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
			
				}*/
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Supplier_Update.aspx,Class:PartiesClass.cs,Method:DropCity_SelectedIndexChanged().  EXCEPTION   " +ex.Message+"  user  "+uid  );
			}
		}

		/// <summary>
		/// This method is used to update Ledger balance in AccountsLedgerTable.
		/// </summary>
		/// <param name="Ledger_ID"></param>
		public void UpdateCustomerBalance(string Ledger_ID)
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
		}
		
		protected void DropState_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
	}
}