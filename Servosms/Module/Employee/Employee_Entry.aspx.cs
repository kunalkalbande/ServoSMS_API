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
	/// Created by	: Anand Mittal
	/// Created on	:
	///	Description	: This form is used for record the Employee profile. Each time when we click SAVE PROFILE
	///				button the data filled in the form will saved and new Employee_ID will generate.
	///				After Saving the record a popup message will show for confirmation.
	/// Tables Used	:
	///				1. Employee
	///	stored procedures :	
	///				1. ProEmployeeEntry - This Stored Procedures Accepts 13 input type Paramenters 
	///				   and fire the insert command to save the record in the Employee Table.
	/// </summary>
	public partial class Employee_Entry : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
        string BaseUri = "http://localhost:64862";
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
				CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if (!IsPostBack)
			{
				try
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
						//string msg="UnAthourized Visit to Employee Entry Page";
						//		dbobj.LogActivity(msg,Session["User_Name"].ToString());  
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
						return;
					}
					if(Add_Flag=="0")
						btnUpdate.Enabled=false;
					#endregion
					fillDriverDetails();

					EmployeeClass obj=new EmployeeClass();
					SqlDataReader SqlDtr;
					string sql;

					GetNextEmpID();
                    #region Fetch Extra Cities from Database and add to the ComboBox
                    List<string> DropCity1 = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/EmployeeEntry/FetchCity").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            DropCity1 = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    foreach (var item in DropCity1)
                    {
                        DropCity.Items.Add(item);
                    }
                    //sql ="select distinct City from Beat_Master order by City asc";
                    //SqlDtr=obj.GetRecordSet(sql);
                    //while(SqlDtr.Read())
                    //{
                    //	 DropCity.Items.Add(SqlDtr.GetValue(0).ToString()); 

                    //}
                    //SqlDtr.Close();
                    #endregion

                    #region Fetch Extra Cities from Database and add to the ComboBox

                    List<string> DropState1 = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/EmployeeEntry/FetchState").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            DropState1 = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    foreach (var item in DropState1)
                    {
                        DropState.Items.Add(item);
                    }

                    //sql ="select distinct state from Beat_Master order by state asc";
                    //SqlDtr=obj.GetRecordSet(sql);
                    //while(SqlDtr.Read())
                    //{

                    //	DropState.Items.Add(SqlDtr.GetValue(0).ToString()); 

                    //}
                    //SqlDtr.Close();
                    #endregion

                    #region Fetch Extra Cities from Database and add to the ComboBox
                    List<string> DropCountry1 = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/EmployeeEntry/FetchCountry").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            DropCountry1 = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    foreach (var item in DropCountry1)
                    {
                        DropCountry.Items.Add(item);
                    }
                    //sql ="select distinct country from Beat_Master order by country asc";
					//SqlDtr=obj.GetRecordSet(sql);
					//while(SqlDtr.Read())
					//{				
					//	DropCountry.Items.Add(SqlDtr.GetValue(0).ToString()); 
					//}
					//SqlDtr.Close();
					#endregion
			
					getbeat();
					lblDrLicense.Visible = false;
					lblVehicleNo.Visible = false;
					lblLicenseVali.Visible = false;
					lblLICPolicy.Visible = false;
					lblLICValid.Visible = false;  
					txtLicenseNo.Visible = false;
					txtLicenseValidity.Visible  = false;
					txtLICNo.Visible = false;
					txtLICvalidity.Visible = false;
					DropVehicleNo.Visible = false;
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
                    Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                }
			}
		}

		/// <summary>
		/// This method is used to concating city,state,country for using javascript.
		/// </summary>
		public void getbeat()
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				//SqlDataReader sqldtr;
				//string sql;
				string str="";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/EmployeeEntry/FetchData").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        str = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                txtbeatname.Text = str;
    //            sql ="select city,state,country from beat_master";
				//sqldtr=obj.GetRecordSet(sql);
				//while(sqldtr.Read())
				//{
				//	str=str+sqldtr.GetValue(0).ToString()+":";
				//	str=str+sqldtr.GetValue(1).ToString()+":";
				//	str=str+sqldtr.GetValue(2).ToString()+"#";
				//}
				//txtbeatname.Text=str;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}

		/// <summary>
		/// This Method fetch the Vehcile No and ID and Fill the Vehicle No. Combo from Vehicleentry table.
		/// </summary>
		public void fillDriverDetails()
		{ 
			txtLicenseValidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;    
			txtLICvalidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;   
			DropVehicleNo.Items.Clear();
			DropVehicleNo.Items.Add("Select");
			//SqlDataReader SqlDtr = null;

            List<string> Vehicle = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/EmployeeEntry/FetchVehicle").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    Vehicle = JsonConvert.DeserializeObject<List<string>>(id);
                }
                else
                    Res.EnsureSuccessStatusCode();
            }
            foreach (var item in Vehicle)
            {
                DropVehicleNo.Items.Add(item);
            }

   //         dbobj.SelectQuery("Select vehicle_no from vehicleentry",ref SqlDtr);
			//while(SqlDtr.Read())
			//{
			//	DropVehicleNo.Items.Add(SqlDtr.GetValue(0).ToString());   
			//}
			//SqlDtr.Close();
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
		/// This is used to save the employee information with the help of ProEmployeeEntry Procedure
		/// before check the given employee name must be unique.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{       
			EmployeeClass obj=new EmployeeClass();
			string str2="";
			try
			{
				SqlDataReader SqlDr;
				
				if(DropDesig.SelectedItem.Text == "Driver")
				{
					if(txtLicenseNo.Text.Trim().Equals("") ) 
					{
						MessageBox.Show("Please enter License No");
						return;
					}
				}
				string sql1; 
				string ename="";
				if(txtFName.Text.Trim()!="")
					ename+=txtFName.Text.Trim();
				if(txtMName.Text.Trim()!="")
					ename+=" "+txtMName.Text.Trim();
				if(txtLName.Text.Trim()!="")
					ename+=" "+txtLName.Text.Trim();
				//((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() ));
				sql1="select Emp_Id from employee where Emp_Name='"+ename.Trim()+"'";
				SqlDr=obj.GetRecordSet(sql1);
				if(SqlDr.HasRows)
				{
					MessageBox.Show("Employee Name  "+ename+" Already Exist");
					return;
				}
				SqlDr.Close();
				sql1="select * from Ledger_Master where Ledger_Name='"+ename.Trim()+"'";
				SqlDr=obj.GetRecordSet(sql1);
				if(SqlDr.HasRows)
				{
					MessageBox.Show("Employee Name  " + ename+" Already Exist");
					return;
				}
				SqlDr.Close();
				obj.Emp_ID = LblEmployeeID.Text.ToString();
				//obj.Emp_Name =StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() ));
				obj.Emp_Name =ename.Trim();
				obj.Address =StringUtil.FirstCharUpper(txtAddress.Text.ToString());
			
				obj.EMail =txtEMail.Text;
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
				// Call the function from employee class to execute the procedure to insert the record.
				
				/************Add by vikas 27.10.2012*****************/
				if(RbtnActive.Checked==true)
					obj.Status="1";
				else if(RbtnNone.Checked==true)
					obj.Status="0";
				/************End*****************/

				obj.InsertEmployee ();	
				MessageBox.Show("Employee Saved");
				CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Class:Employeee.cs,Methd: btnUpdate_Click   "+"Employee ID "+LblEmployeeID.Text.ToString()+"   Employee Name  " +obj.Emp_Name   +" city   "+ 	obj.City   +" Salary  "+ 	obj.Salary + " Designation "+ obj.Designation    +  " IS SAVED  "+" userid "+Session["User_Name"].ToString()+"    "+uid);
				Clear();
				GetNextEmpID();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Class:Employeee.cs,Methd: btnUpdate_Click   Exception: "+ex.Message +"   userid  "+str2+"  "+uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}

		/// <summary>
		/// Thid Method is used to clear the form
		/// </summary>
		public void Clear()
		{
			LblEmployeeID.Text="";
			txtFName.Text="";
			txtMName.Text="";
			txtLName.Text="";
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
			txtLicenseNo.Text = "";
			txtLICNo.Text = "";
			DropVehicleNo.SelectedIndex = 0;
			txtLicenseValidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;    
			txtLICvalidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year ;   
		}

		/// <summary>
		/// Method to fetch the Next Employee ID from table employee and display in lable.
		/// The ID initially starts with 1001.
		/// </summary>
		public void GetNextEmpID()
		{
			EmployeeClass obj=new EmployeeClass();
			SqlDataReader SqlDtr;

			#region Fetch Next Employee ID
			SqlDtr =obj.GetNextEmployeeID ();
			while(SqlDtr.Read())
			{
				LblEmployeeID.Text=SqlDtr.GetSqlValue(0).ToString ();
				if (LblEmployeeID.Text=="Null")
					LblEmployeeID.Text ="9001";
			}		
			SqlDtr.Close();
			#endregion
		}

		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			/*InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			// This is used to fetch the state and country according to selected city. This method set the coressponding values of state and country in there respective combos.
			sql="select State,Country from Beat_Master where City='"+ DropCity.SelectedItem.Value +"'" ;
			SqlDtr=obj.GetRecordSet(sql); 
			
			try
			{
				//CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Method:DropCity_SelectedIndexChanged "+uid);
				while(SqlDtr.Read())
				{
				
					DropState.SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByValue(SqlDtr.GetValue(0).ToString()))));
					DropCountry.SelectedIndex=(DropCountry .Items.IndexOf((DropCountry.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
				}
				SqlDtr.Close();
				//CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Method:DropCity_SelectedIndexChanged "+uid);
				}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Employee_Entry.aspx,Method:DropCity_SelectedIndexChanged ,"+" state and country is select for city :"+  DropCity.SelectedItem.Value+  "  EXCEPTION" +ex.Message+" userid "+uid);

			}*/
		}

		/// <summary>
		/// This method is used to fetch values about Designation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropDesig_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
			// if designation is Driver then the Additional fields to enter the driver license details and vehicle Number is are visibles , Otherwise not visibles.
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

		protected void DropVehicleNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtMName_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}