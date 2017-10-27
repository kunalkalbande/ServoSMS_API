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
	/// Summary description for Supplier_Entry.
	/// </summary>
	public partial class Supplier_Entry : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
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
				CreateLogFiles.ErrorLog("Form:Supplier_Entry.aspx,Method:page_load"+"  EXCEPTION  "+ ex.Message+uid);
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
						//	string msg="UnAthourized Visit to Vendor Entry Page";
						//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					}
					if(Add_Flag=="0")
						btnUpdate.Enabled=false;
					#endregion

					getbeat();
					// Fills the Credit Limit combo with 30 Numbers.
					for(i=1;i<=30;i++)
					{
						DropCrDay.Items.Add(i.ToString ());
					}
					PartiesClass obj=new PartiesClass();
					SqlDataReader SqlDtr;
					string sql;
					GetNextSupplierID();
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
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Supplier_Entry.aspx,Method:page_load"+"  EXCEPTION  "+ ex.Message+uid);
				}
			}
		}

		/// <summary>
		/// This is used to concatinate city,state,country for javascript.
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
				CreateLogFiles.ErrorLog("Form:Supplier_Entry.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
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
		/// Its checks the before save that the account period is inserted in organisaton table or not.
		/// </summary>
		/// <returns></returns>
		public bool checkAcc_Period()
		{
			SqlDataReader SqlDtr = null;
			int c = 0;
			dbobj.SelectQuery("Select count(Acc_Date_From) from Organisation",ref SqlDtr);
			if(SqlDtr.Read())
			{
				c = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());  
			}
			SqlDtr.Close();

			if(c > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// This method is used to Update the vendor entry with the help of ProSupplierEntry Procedure before check
		/// the vendor name must be unique if vendor name is duplicate then show popup message for user.
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
				//                string sname=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim())) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() )); 				SqlDataReader SqlDtr;
				//				string sql1="select Supp_ID from supplier where Supp_Name='"+sname+"'";
				//				
				//				SqlDtr=obj.GetRecordSet(sql1);
				//				
				//				if(SqlDtr.HasRows)
				//				{
				//					MessageBox.Show("Vendor Name  "+sname+" Already Exist");
				//					return;
				//				}
				//				SqlDtr.Close();
				string sql1; 
				SqlDataReader SqlDtr = null;
				string sname="";
				if(txtFName.Text.Trim()!="")
					sname+=txtFName.Text.Trim();
				if(txtMName.Text.Trim()!="")
					sname+=" "+txtMName.Text.Trim();
				if(txtLName.Text.Trim()!="")
					sname+=" "+txtLName.Text.Trim();
				//((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() ));
				sql1="select Supp_Id from Supplier where Supp_Name='"+sname.Trim()+"'";
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
				//				sql1 = "Select Tin_No from supplier where Tin_No = '"+txtTinNo.Text.Trim()+"'";
				//				SqlDtr= obj.GetRecordSet(sql1);
				//				if(SqlDtr.HasRows)
				//				{
				//					MessageBox.Show("The Tin No. "+txtTinNo.Text.Trim()+" Already Exist");
				//					return;
				//				}
				//				SqlDtr.Close();
				obj.Supp_ID=lblSupplierID.Text;
				if(txtMName.Text!="" && txtLName.Text!="")
					obj.Supp_Name=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim())+" "+ (txtLName.Text.ToString().Trim()));
				else if(txtMName.Text=="" &&  txtLName.Text!="" )
					obj.Supp_Name=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim())) +" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim()));
				else if(txtMName.Text!="" &&  txtLName.Text=="")
					obj.Supp_Name=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim())) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim()));
				else if (txtLName.Text=="" &&  txtMName.Text=="")
					obj.Supp_Name=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim()));
				obj.Supp_Type=DropType.SelectedItem.Value.ToString(); 
				obj.Address=txtAddress.Text.Trim();
				obj.City=DropCity.SelectedItem.Value.ToString();
				obj.State=DropState.SelectedItem.Value.ToString();
				obj.Country=DropCountry.SelectedItem.Value.ToString();
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
				if(txtOpBalance.Text=="")
					obj.Op_Balance="0";
				else
					obj.Op_Balance=txtOpBalance.Text;
				obj.Balance_Type =DropBal.SelectedItem.Value.ToString();
				if(DropCrDay.SelectedIndex==0)
					obj.CR_Days="0";
				else
					obj.CR_Days=DropCrDay.SelectedItem.Value.ToString();
				obj.Tin_No = txtTinNo.Text.Trim(); 
				// call the function to insert the supplier details.
				obj.InsertSupplier();
				MessageBox.Show("Vendor Saved");
				Clear();
				GetNextSupplierID();
				CreateLogFiles.ErrorLog("Form:Vender_Entry.aspx, Method:btnUpdate_Click "+"   Supplier_ID "+	obj.Supp_ID  +"   Supplier Type   "+obj.Supp_Type+" supplier City "+obj.City+"  IS SAVED  " +"  user  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vender_Entry.aspx, Method:btnUpdate_Click ().  EXCEPTION:  "+ ex.Message+"  user  "+uid );
			}
		}
	
		/// <summary>
		/// This method is used to Clear the form.
		/// </summary>
		public void Clear()
		{
			lblSupplierID.Text="";
			txtFName.Text="";
			txtMName.Text="";
			txtLName.Text="";
			txtEMail.Text="";
			txtAddress.Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry.SelectedIndex=0;
			DropType.SelectedIndex=0; 
			DropBal.SelectedIndex=0; 
			DropCrDay.SelectedIndex=0; 
			txtPhoneOff.Text="";
			txtPhoneRes.Text="";
			txtMobile.Text="";
			txtOpBalance.Text="";
			txtTinNo.Text = "";
		}

		/// <summary>
		/// This method is used to returns the Next Supplier ID from table Supplier.
		/// </summary>
		public void GetNextSupplierID()
		{
			PartiesClass obj=new PartiesClass();
			SqlDataReader SqlDtr;
				
			#region Fetch Next Vendor ID
			SqlDtr =obj.GetNextSupplierID();
			while(SqlDtr.Read())
			{
				lblSupplierID.Text =SqlDtr.GetSqlValue(0) .ToString ();
				if (lblSupplierID.Text =="Null")
					lblSupplierID.Text ="1001";
			}	
			SqlDtr.Close();
			#endregion
		}


		/// <summary>
		/// Select the state and country according to the selected City.
		/// This method is not used.
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
				sql="select State,Country from Beat_Master where City='"+ DropCity.SelectedItem.Value +"'" ;
				SqlDtr=obj.GetRecordSet(sql); 
				while(SqlDtr.Read())
				{
				
					DropState.SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByValue(SqlDtr.GetValue(0).ToString()))));
					DropCountry.SelectedIndex=(DropCountry .Items.IndexOf((DropCountry.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
				
				} */ 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Supplier_Entry.aspx,Method:DropCity_SelectedIndexChanged().  EXCEPTION: "+ ex.Message+" User_ID: "+uid);
			}
		}

		protected void txtPhoneOff_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}