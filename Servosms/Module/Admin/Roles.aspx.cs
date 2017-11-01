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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using Servo_API.Models;

namespace Servosms.Module.Admin
{
	/// <summary>
	/// Summary description for Roles.
	/// </summary>
	public partial class Roles : System.Web.UI.Page
	{
        string baseUri = "http://localhost:64862";
        DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{ 
				uid=(Session["User_Name"].ToString ());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:Page_Load"+"  EXCEPTION  "+ex.Message+" userid   "+uid);
				Response.Redirect("ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				#region Check Privileges if the user is admin then grant the access
				if(Session["User_ID"].ToString ()!="1001")
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				#endregion
				GetNextRoleID();
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
		/// This method is used to clear the form.
		/// </summary>
		public void Clear()
		{
			lblRoleID.Text="";
			txtRoleName.Text="";
			txtDesc.Text="";  
			
		}

		/// <summary>
		/// This method is used to fatch the next Role ID from Roles table and stored in textbox.
		/// </summary>
		public void GetNextRoleID()
		{
			//EmployeeClass obj=new EmployeeClass();
			//SqlDataReader SqlDtr;
			//string sql;
			try
			{
                //#region Fetch Next Role ID
                //sql="select max(Role_ID)+1 from Roles";
                //SqlDtr =obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	lblRoleID.Text=SqlDtr.GetSqlValue(0).ToString ();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/Roles/GetNextRoleID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lblRoleID.Text = JsonConvert.DeserializeObject<string>(id);
                    }
                }
                if (lblRoleID.Text=="Null")
						lblRoleID.Text ="1001";
				//}		
				//SqlDtr.Close();
				//#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:GetNextRoleID   EXCEPTION: "+ex.Message+" userid   "+uid);
			}
		}

		/// <summary>
		/// This method is used to update the Role with the help of ProRolesUpdate procedure.
		/// </summary>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			EmployeeClass obj=new EmployeeClass();
			obj.Role_Name=txtRoleName.Text.ToString();
			obj.Description =txtDesc.Text.ToString();
			try
			{
				if(dropRoleID.Visible)
				{
					obj.Role_ID=dropRoleID.SelectedItem.Value;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(obj);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/Roles/UpdateRole", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            //var prodd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                        }
                    }
                    //obj.UpdateRoles();
                    CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:btnUpdateClick   Role  name "+obj.Role_Name +" Updated   "+uid);
					MessageBox.Show("Role Updated");
				}
				else
				{
					#region Check Role Already Created or Not
					int count=0;
					//DBOperations.DBUtil  dbobj=new DBOperations.DBUtil();
					//dbobj.ExecuteScalar("select count(*) from Roles where Role_Name='"+ txtRoleName.Text.Trim() +"'",ref count);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/Roles/GetCheckRoleExists?txtRoleName=" + txtRoleName.Text.Trim()).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            count = JsonConvert.DeserializeObject<int>(id);
                        }
                    }

                    if (count>0)
					{
						MessageBox.Show("Role already Exists");
						return;
					}
					#endregion

					obj.Role_ID = lblRoleID.Text.ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(obj);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/Roles/InsertRole", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            //var prodd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                        }
                    }

                    //obj.InsertRoles();	
					CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:btnUpdate_Click   Role Name "+obj.Role_Name +" Created   "+uid);
					MessageBox.Show("Role Created");
				}
				Clear();
				GetNextRoleID();
				lblRoleID.Visible=true;
				dropRoleID.Visible=false;
				btnEdit.Visible=true;
				btnUpdate.Text="Save";
				dropRoleID.SelectedIndex=0;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:btnUpdate_Click   EXCEPTION: "+ex.Message+" userid   "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the all role ID from roles table and fill the dropdownlist on edit time.
		/// </summary>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			dropRoleID.SelectedIndex=0;
			lblRoleID.Visible=false;
			btnEdit.Visible=false;
			dropRoleID.Visible=true;
			btnUpdate.Text="Update";
			try
			{
				#region	Fetch All Role ID
				dropRoleID.Items.Clear();
				dropRoleID.Items.Add("Select");
                List<string> lstDropRoleID = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/Roles/FillDropRoleID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropRoleID = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                }

                if (lstDropRoleID != null)
                {
                    foreach (var RoleID in lstDropRoleID)
                        dropRoleID.Items.Add(RoleID);
                }

    //            DBOperations.DBUtil obj=new DBOperations.DBUtil();
				//SqlDataReader SqlDtr=null;

				//obj.SelectQuery("select Role_ID from Roles",ref SqlDtr);
				//while(SqlDtr.Read())
				//{
				//	dropRoleID.Items.Add(SqlDtr.GetValue(0).ToString());
				//}
				//SqlDtr.Close();
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:btnEdit_Click   EXCEPTION: "+ex.Message+" userid   "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the role infomatoin when u select the role ID from dropdownlist.
		/// </summary>
		protected void dropRoleID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				Clear();
                RolesModel role = new RolesModel();

                DBOperations.DBUtil obj=new DBOperations.DBUtil();
				//SqlDataReader SqlDtr=null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/Roles/GetSelectedRoleIDData?RoleID=" + dropRoleID.SelectedItem.Value).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        role = JsonConvert.DeserializeObject<RolesModel>(id);
                    }
                }

                //obj.SelectQuery("select * from roles where Role_Id='"+ dropRoleID.SelectedItem.Value +"'",ref SqlDtr);
				if(role != null)
				{
					txtRoleName.Text= role.Role_Name.ToString();
					txtDesc.Text= role.Description.ToString();
				}
				//SqlDtr.Close();
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:dropRoleID_SelectedIndexChanged    "+"  userid "+uid);
			
			}
			catch(Exception ex)
			{
				MessageBox.Show("Please select Role ID");
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:dropRoleID_SelectedIndexChanged"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}

		/// <summary>
		/// This method is used to delete the particular Role ID from roles table which role id select from 
		/// dropdownlist on edit time.
		/// </summary>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{  
			try
			{
				if(dropRoleID.SelectedIndex==0)
				{
					RMG.MessageBox.Show("Please select the Role ID");
					return;
				}
				int output=0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/Roles/GetCheckRoleExistsUser_master?txtRoleName=" + dropRoleID.SelectedItem.Value).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        output = JsonConvert.DeserializeObject<int>(id);
                    }
                }

                //DBOperations.DBUtil obj=new DBOperations.DBUtil();
				//obj.ExecuteScalar("select count(*) from User_master where Role_ID='" + dropRoleID.SelectedItem.Value + "'",ref output);
				if(output>0) 
				{
					MessageBox.Show("Selected Role cannot be Deleted");
					return;
				}
				else
				{
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(dropRoleID.SelectedItem.Value);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/Roles/DeleteRole?txtRoleName=" + dropRoleID.SelectedItem.Value, byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            output = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(responseString);
                        }
                    }

                    //obj.Insert_or_Update("delete from roles where Role_Id='"+ dropRoleID.SelectedItem.Value +"'",ref output);
					dropRoleID.Items.Remove(dropRoleID.SelectedItem.Value); 
					MessageBox.Show("Role Deleted");
					CreateLogFiles.ErrorLog("Form:Roles.aspx,Method: btnDelete_Click"+ uid);
					Clear();
					lblRoleID.Visible=true;
					dropRoleID.Visible=false;
					btnEdit.Visible=true;
					btnUpdate.Text="Save";
					GetNextRoleID();
					dropRoleID.SelectedIndex=0;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("Please select Role ID");
				CreateLogFiles.ErrorLog("Form:Roles.aspx,Method:btnDelete_Click"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}
	}
}