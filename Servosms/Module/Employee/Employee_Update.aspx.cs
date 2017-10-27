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
using System.Data.SqlClient ;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes ;
using RMG;

namespace Servosms.Module.Employee
{
	/// <summary>
	/// Summary description for Employee_Update.
	/// </summary>
	public partial class Employee_Update : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// and also fatch the employee information according to select employee ID in comes from url.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception  ex)
			{
				CreateLogFiles.ErrorLog("Form:Employee_Update.aspx,Method:page_load"+" EXCEPTION  "+ ex.Message+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
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
				if(View_flag=="0")
				{
					//	string msg="UnAthourized Visit to Enployee Entry Page";
					//dbobj.LogActivity(msg,Session["User_Name"].ToString());  
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				//************
				if(Edit_Flag=="0")
					btnUpdate.Enabled=false;
				//************
				#endregion
				try
				{
					LblEmployeeID.Text=Request.QueryString.Get("ID"); 
					MasterClass obj1=new MasterClass();
					EmployeeClass obj=new EmployeeClass (); 
					SqlDataReader SqlDtr;	
					string sql;
					getbeat();
					#region Fetch Extra Cities,Designation,country and State from Database and add to the ComboBox
					sql="select distinct Country from Beat_Master";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropCountry.Items.Add(SqlDtr.GetValue(0).ToString()); 
					}
					SqlDtr.Close();
					sql="select distinct City from Beat_Master order by city";
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
				
					DropVehicleNo.Items.Clear();
					DropVehicleNo.Items.Add("Select");
					SqlDtr = obj.GetRecordSet("Select vehicle_no from vehicleentry");
					while(SqlDtr.Read())
					{
						DropVehicleNo.Items.Add(SqlDtr.GetValue(0).ToString());    
					}
					SqlDtr.Close();

					txtLicenseNo .Text = "";
					txtLicenseValidity.Text = "";
					txtLICNo.Text = "";
					txtLICvalidity.Text = "";
					DropVehicleNo.SelectedIndex = 0; 

					#endregion

					SqlDtr = obj.EmployeeList(LblEmployeeID.Text.ToString (),"",""  );
					while (SqlDtr.Read ())
					{
						lblName.Text =SqlDtr.GetValue(1).ToString ();
						TempEmpName.Text=SqlDtr.GetValue(1).ToString ();
						DropDesig.SelectedIndex =DropDesig.Items.IndexOf(DropDesig.Items.FindByValue(SqlDtr.GetValue(2).ToString ()));
						txtAddress.Text =SqlDtr.GetValue(3).ToString ();
						DropCity.SelectedIndex =DropCity.Items.IndexOf(DropCity.Items.FindByValue(SqlDtr.GetValue(4).ToString ()));
						DropState.SelectedIndex =DropState.Items.IndexOf(DropState.Items.FindByValue(SqlDtr.GetValue(5).ToString ()));
						// If the designation is driver then it shows the extra fields related to driver , else hide that fields.
						if(SqlDtr.GetValue(2).ToString().Trim().Equals("Driver"))
						{
							lblDrLicense.Visible = true;
							lblLicenseVali.Visible = true;
							lblLICPolicy.Visible  = true;
							lblLICValid.Visible = true;  
							lblVehicleNo.Visible = true; 
							txtLicenseNo .Visible = true;
							txtLicenseValidity.Visible  = true;
							txtLICNo.Visible = true;
							txtLICvalidity.Visible = true;
							DropVehicleNo.Visible =true; 
						}
						else
						{
							lblDrLicense.Visible = false;
							lblLicenseVali.Visible = false;
							lblLICPolicy.Visible  = false;
							lblLICValid.Visible = false;
							lblVehicleNo.Visible = false; 
							txtLicenseNo .Visible = false;
							txtLicenseValidity.Visible  = false;
							txtLICNo.Visible = false;
							txtLICvalidity.Visible = false;
							DropVehicleNo.Visible =false; 
						}
						DropCountry.SelectedIndex =DropCountry.Items.IndexOf(DropCountry.Items.FindByValue(SqlDtr.GetValue(6).ToString ()));
						txtContactNo .Text =SqlDtr.GetValue(7).ToString ();
						if(txtContactNo.Text=="0")
							txtContactNo.Text="";
						txtMobile.Text =SqlDtr.GetValue(8).ToString ();
						if(txtMobile.Text=="0")
							txtMobile.Text="";
						txtEMail.Text =SqlDtr.GetValue(9).ToString ();
						txtSalary.Text =SqlDtr.GetValue(10).ToString ();
						txtOT_Comp.Text =SqlDtr.GetValue(11).ToString ();
						txtLicenseNo .Text = SqlDtr.GetValue(12).ToString();
						txtLicenseValidity.Text = GenUtil.str2DDMMYYYY(trimDate(SqlDtr.GetValue(13).ToString()));
						txtLICNo.Text = SqlDtr.GetValue(14).ToString();
						txtLICvalidity.Text = GenUtil.str2DDMMYYYY(trimDate(SqlDtr.GetValue(15).ToString()));
						//Response.Write(SqlDtr.GetValue(16).ToString());  
						SqlDataReader rdr = null;
						dbobj.SelectQuery("Select vehicle_no from vehicleentry where vehicledetail_id = "+SqlDtr.GetValue(16).ToString(),ref rdr);
						if(rdr.Read())
						{
							//Response.Write(rdr.GetValue(0).ToString ());  
							DropVehicleNo.SelectedIndex =DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(rdr.GetValue(0).ToString ().Trim() ));
						}
						rdr.Close();
						rdr = null;
						dbobj.SelectQuery("Select Op_Balance,Bal_Type from Ledger_Master where Ledger_Name = '"+SqlDtr.GetValue(1).ToString()+"'",ref rdr);
						if(rdr.Read())
						{
							txtopbal.Text=rdr.GetValue(0).ToString();
							DropType.SelectedIndex =DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(rdr.GetValue(1).ToString ().Trim() ));
						}
						rdr.Close();

						/********Add by vikas 27.10.2012*********************/
						if(SqlDtr["Status"].ToString().Trim()!=null && SqlDtr["Status"].ToString().Trim()!="")
						{
							if(SqlDtr["Status"].ToString().Trim()=="1")
								RbtnActive.Checked=true;
							else 
								RbtnNone.Checked=true;
						}
						else
						{
							RbtnActive.Checked=false;
							RbtnNone.Checked=false;
						}
						/********End*********************/
					}
					SqlDtr.Close(); 
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Employee_Update.aspx,Method:Page_Load() "+"EmployeeID.   EXCEPTION" + ex.Message+" userid  "+uid);
				}
			}
		}

		/// <summary>
		/// This method is used to concatenate the city,state,country for 
		/// fetching the state,country with corrosponding to city with the help of java script.
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
				CreateLogFiles.ErrorLog("Form:Employee_Update.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
			}
		}

		/// <summary>
		/// This method is used to return only date from date time
		/// </summary>
		/// <param name="strDate"></param>
		/// <returns></returns>
		public string trimDate(string strDate)
		{
			int pos = strDate.IndexOf(" ");
			if(pos != -1)
			{
				strDate = strDate.Substring(0,pos);
			}
			else
			{
				strDate = "";					
			}
			return strDate;
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
		/// This method is used to update the particular employee record with the help of ProEmployeeUpdate Procedure
		/// before check the Employee name must be unique in Employee as well as Ledger Master table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			EmployeeClass obj=new EmployeeClass();
			SqlDataReader SqlDtr=null;
			try
			{
				if(!TempEmpName.Text.ToLower().Trim().Equals(lblName.Text.ToLower().Trim()))
				{
					string ename=lblName.Text.Trim();
					string sql1="select * from employee where Emp_Name='"+ename.Trim()+"'";
					SqlDtr=obj.GetRecordSet(sql1);
					if(SqlDtr.HasRows)
					{
						MessageBox.Show("Employee Name  "+ename+" Already Exist");
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
				obj.Emp_ID = LblEmployeeID.Text.ToString ();
				obj.Emp_Name =lblName.Text ;
				obj.TempEmpName=TempEmpName.Text;
				obj.Address =txtAddress.Text.ToString();
				obj.EMail =txtEMail.Text.ToString ();
				obj.City =DropCity.SelectedItem .Value.ToString ();
				obj.State  =DropState.SelectedItem .Value.ToString ();
				obj.Country  =DropCountry.SelectedItem .Value.ToString ();
				if(txtContactNo.Text.ToString()=="")
					obj.Phone="0";
				else
					obj.Phone =txtContactNo.Text.ToString ();
				if(txtMobile.Text.ToString()=="")
					obj.Mobile="0";
				else
					obj.Mobile =txtMobile.Text.ToString();
				obj.Designation =DropDesig.SelectedItem .Value.ToString ();
				obj.Salary =txtSalary.Text.ToString ();
				obj.OT_Compensation =txtOT_Comp.Text.ToString();
				obj.Dr_License_No  = txtLicenseNo.Text.ToString().Trim ();
				obj.Dr_License_validity  = GenUtil.str2MMDDYYYY (txtLicenseValidity.Text.Trim());
				obj.Dr_LIC_No = txtLICNo.Text.Trim();
				obj.Dr_LIC_validity = GenUtil.str2MMDDYYYY(txtLicenseValidity.Text.Trim()) ;  
				obj.OpBalance=txtopbal.Text.Trim().ToString();
				obj.BalType=DropType.SelectedItem.Text;
				string vehicle_no = DropVehicleNo.SelectedItem.Text.Trim();
				if(vehicle_no.Equals(""))
				{
					vehicle_no = "0";
				}
			
				obj.Vehicle_NO = vehicle_no; 
				// calls the update employee procedures through the UpdateEmployee() method.
				/************Add by vikas 27.10.2012*****************/
				if(RbtnActive.Checked==true)
					obj.Status="1";
				else if(RbtnNone.Checked==true)
					obj.Status="0";
				/************End*****************/
				obj.UpdateEmployee ();
				string Ledger_ID = "";
				dbobj.SelectQuery("select Ledger_ID from Ledger_Master where Ledger_Name=(select Emp_Name from Employee where Emp_ID='"+LblEmployeeID.Text.Trim()+"')",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Ledger_ID = SqlDtr.GetValue(0).ToString();
				}
				UpdateCustomerBalance(Ledger_ID);
				MessageBox.Show("Employee Updated");
				CreateLogFiles.ErrorLog("Form:Employee_Update.aspx,Method:btnUpdate_Click"+"EmployeeID   " +LblEmployeeID.Text.ToString ()+  " IS UPDATED  "+" userid "+ uid);
				Response.Redirect("Employee_List.aspx",false);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Employee_Update.aspx,Method:btnUpdate_Click"+"EmployeeID  "+LblEmployeeID.Text.ToString()+ "  IS UPDATED "+"  EXCEPTION" + ex.Message+" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to clear the form.
		/// </summary>
		public void Clear()
		{
			txtEMail.Text="";
			txtAddress.Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry.SelectedIndex=0;
			DropDesig.SelectedIndex=0;
			txtContactNo.Text="";
			txtMobile.Text="";  
			txtOT_Comp.Text="";
			txtSalary.Text="";
			TempEmpName.Text="";
		}

		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		protected void txtAddress_TextChanged(object sender, System.EventArgs e)
		{
		}

		protected void DropState_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		
		/// <summary>
		/// This method is used to Fetch record according to the designation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropDesig_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// If Designation is driver then visible the extra fields.
			if(DropDesig.SelectedItem.Text == "Driver")
			{
				txtLicenseNo.Text = "";
				txtLicenseValidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;    
				txtLICvalidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;   
				txtLICNo.Text = "";
				DropVehicleNo.SelectedIndex = 0;
				txtLicenseNo.Visible = true;
				txtLicenseValidity.Visible  = true;
				txtLICNo.Visible = true;
				txtLICvalidity.Visible = true;
				DropVehicleNo.Visible = true;
				lblDrLicense.Visible = true;
				lblLicenseVali.Visible = true;
				lblLICPolicy.Visible = true;
				lblLICValid.Visible = true;  
				lblVehicleNo.Visible = true;
			}
			else
			{
				txtLicenseNo.Text = "";
				txtLICNo.Text = "";
				txtLicenseValidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;    
				txtLICvalidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;   
				DropVehicleNo.SelectedIndex = 0;
				lblDrLicense.Visible = false;
				lblLicenseVali.Visible = false;
				lblLICPolicy.Visible = false;
				lblLICValid.Visible = false;  
				txtLicenseNo.Visible = false;
				txtLicenseValidity.Visible  = false;
				txtLICNo.Visible = false;
				txtLICvalidity.Visible = false;
				DropVehicleNo.Visible = false;
				lblVehicleNo.Visible = false;
			}
		}

		/// <summary>
		/// This method is used to update the Employee balance in AccountsLedgerTable on edit time.
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
	}
}