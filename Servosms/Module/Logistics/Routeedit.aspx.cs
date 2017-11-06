
/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
#region Directives...
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
using System.Text;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using Servo_API.Models;
#endregion

namespace Servosms.Module.Logistics
{
	/// <summary>
	/// Summary description for Routeedit.
	/// </summary>
	public partial class Routeedit : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btndelete;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		string uid;
        string baseUri = "http://localhost:64862";

        /// <summary>
        /// Put user code to initialize the page here
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// and generate the next ID also.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Page Load...
        protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
				if(!Page.IsPostBack)
				{
					RouteName="";
					lblRouteID.Visible=true;
					DropDownList1.Visible=false;
					GetNextRouteNo();
					# region Dropdown Route Name
					Button1.Enabled=true;
					btnDel.Enabled=true;
					btnsave.Visible=false;
					btnEdit.Visible=true;
					checkPrevileges();

                    List<string> lstRouteNames = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var Res = client.GetAsync("api/RouteMaster/GetFillRouteNames").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            lstRouteNames = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }

                    if (lstRouteNames != null)
                    {
                        foreach (var Route in lstRouteNames)
                            DropDownList1.Items.Add(Route);
                    }

     //               SqlConnection con;
					//SqlCommand cmdselect;
					//SqlDataReader dtrdrive;
					//con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					//con.Open ();
					//cmdselect = new SqlCommand( "Select Route_name From Route", con );
					//dtrdrive = cmdselect.ExecuteReader();
					//DropDownList1.Items.Add("Select");
					//while (dtrdrive.Read()) 
					//{
					//	DropDownList1.Items.Add(dtrdrive.GetString(0));
					//}
					//dtrdrive.Close();
					//con.Close ();

					# endregion
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}
		# endregion

		/// <summary>
		/// This method checks the user privileges from session.
		/// </summary>
		public void checkPrevileges()
		{
			try
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="6";
				string SubModule="1";
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
				if(Add_Flag=="0")
					Button1.Enabled=false;
				if(Edit_Flag=="0")
				{
				
					btnEdit.Enabled=false;
					btnsave.Enabled = false;  
				}
				if(Del_Flag=="0")
				{
				
					btnDel.Enabled=false;
				}
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
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

		public static string RouteName="";

		/// <summary>
		/// This method is used to fatch the record according to select the Route ID from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region DropDownList1_SelectedIndexChanged...
		protected void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(DropDownList1.SelectedItem.Text=="Select")
				{
					MessageBox.Show("Please select the Route name to Update");
				}
				else
				{
                    RouteMasterModel route = new RouteMasterModel();
                    RouteName ="";
					btnDel.Enabled=true;
					btnsave.Enabled=true;

                    string strHidden = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var Res = client.GetAsync("api/RouteMaster/GetRouteInfo?selectedRoute=" + DropDownList1.SelectedItem.Text.ToString()).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            route = JsonConvert.DeserializeObject<RouteMasterModel>(id);

                            if(route != null)
                            {
                                txtrname.Text = route.Route_Name;
                                RouteName = route.Route_Name;
                                txtrkm.Text = route.Route_Km;
                            }

                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }

     //               SqlConnection con44;
					//SqlCommand cmdselect44;
					//SqlDataReader dtrdrive44;
					//con44=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					//con44.Open ();
					//cmdselect44 = new SqlCommand( "Select Route_name,Route_km  From Route where Route_name=@Route_name", con44 );
					//cmdselect44.Parameters .Add ("@Route_name",DropDownList1.SelectedItem .Text.ToString());
					//dtrdrive44 = cmdselect44.ExecuteReader();
					//while (dtrdrive44.Read()) 
					//{
					//	txtrname.Text=dtrdrive44.GetString(0);
					//	RouteName=dtrdrive44.GetString(0);
					//	txtrkm.Text =dtrdrive44.GetString(1);
					//}
					//dtrdrive44.Close();
					//con44.Close ();
				
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:DropDownList1_SelectedIndexChanged "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}
		# endregion
		
		/// <summary>
		/// This method is used to update the Route ID information in the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region Edit Save Button...
		protected void btnsave_Click(object sender, System.EventArgs e)
		{
			try
			{
								
				string sRName=txtrname.Text.ToString().Trim();
                
				int iCount=0;
				if(sRName.Trim()!=RouteName.Trim())
				{                    
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var Res = client.GetAsync("api/RouteMaster/CheckIfRouteNameExists?routeName=" + sRName).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            iCount = JsonConvert.DeserializeObject<int>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
				}

				if(iCount==0)
				{
					Button1.Visible=true;
					btnDel.Enabled=true;
					btnsave.Visible=false;
					btnEdit.Visible=true;

                    RouteMasterModel route = new RouteMasterModel();

                    if (txtrname.Text == "")
                        route.Route_Name = "";
                    else
                        route.Route_Name = txtrname.Text.Trim();

                    if (txtrkm.Text == "")
                        route.Route_Km = "";
                    else
                        route.Route_Km = txtrkm.Text.Trim();

                    if (DropDownList1.SelectedIndex == 0)
                        route.Index_Route_Name = "";
                    else
                        route.Selected_Route_Name = DropDownList1.SelectedItem.Text.ToString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(route);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/RouteMaster/UpdateRoute", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            MessageBox.Show("Route Updated");
                            CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btnsave_Click " + " Route Name  " + txtrname.Text.Trim().ToString() + " updated. userid " + uid);
                        }
                        else
                            response.EnsureSuccessStatusCode();
                    }
                    
					Clear();
					fill();
					GetNextRouteNo();
					checkPrevileges();
				}
				else
				{
					MessageBox.Show("This Route already exists");
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btnsave_Click "+ " EXCEPTION "+ex.Message +"  userid  "+ uid );
			}
		}
		# endregion
	
		/// <summary>
		/// This method is used to Save the Route ID information in the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region Add Button...
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
                int iCount = 0;

                string sRName=txtrname.Text.ToString().Trim();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/RouteMaster/CheckIfRouteNameExists?routeName=" + sRName).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        iCount = JsonConvert.DeserializeObject<int>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
   
				if(iCount==0)
				{
                    RouteMasterModel route = new RouteMasterModel();
                    
                    route.Route_Name = txtrname.Text.Trim().ToString();
                    route.Route_Km = txtrkm.Text.Trim().ToString();                    

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(route);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/RouteMaster/InsertRoute", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            MessageBox.Show("Route details Saved");
                            CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:Button1_Click " + " New Route Name  " + txtrname.Text.Trim().ToString() + " saved   userid " + uid);
                        }
                        else
                            response.EnsureSuccessStatusCode();
                    }

					Clear();
					fill();
					GetNextRouteNo();
					checkPrevileges();
				}
				else
				{
					MessageBox.Show("This Route already exists");
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:Button1_Click "+ " EXCEPTION  "+ex.Message+"     userid  "+ uid );
			}
		}
		# endregion

		/// <summary>
		/// This method is used to clear the form.
		/// </summary>
		# region Clear Function...
		public void Clear()
		{
			RouteName="";
			txtrname.Text="";
			txtrkm .Text="";
			DropDownList1.SelectedIndex=0;
			lblRouteID.Visible=true;
			DropDownList1.Visible=false;
		}
		# endregion

		/// <summary>
		/// This method is used to fetch all the Route Name from database and fill the dropdownlist.
		/// </summary>
		# region Fill Function...
		public void fill()
		{
			try
			{
                List<string> lstRouteNames = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/RouteMaster/GetFillRouteNames").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstRouteNames = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstRouteNames != null)
                {
                    DropDownList1.Items.Clear();
                    DropDownList1.Items.Add("Select");
                    foreach (var Route in lstRouteNames)
                        DropDownList1.Items.Add(Route);
                }
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:fill().  EXCEPTION : "+ex.Message+"     userid  "+ uid );
			}
		}
		# endregion


		/// <summary>
		/// This method is used to call the Clear() funtion.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region Reset Button...
		private void BtnReset_Click(object sender, System.EventArgs e)
		{
			Clear();
		}
		# endregion


		/// <summary>
		/// This method is used to enable or disabled the property of control on edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region Edit Button...
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Button1.Visible=false;
				btnDel.Enabled=false;
				btnsave.Visible=true;
				btnEdit.Visible=false;
				DropDownList1.Visible=true;
				DropDownList1.Enabled=true;
				lblRouteID.Visible=false;
				//				if(DropDownList1.SelectedItem.Text=="Select")
				//				{
				//					MessageBox.Show("Please select the Route Name to Update");
				//				}
				//checkPrevileges();
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btnEdit_Click     userid  "+ uid );	
			}
			catch(Exception ex )
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btnEdit_Click"+ " EXCEPTION  "+ex.Message+"     userid  "+ uid );
			}
		}
		# endregion

		/// <summary>
		/// This method is used to delete the particular record, select from the dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region Delete Button...
		protected void btnDel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//				if(DropDownList1.SelectedItem.Text=="Select")
				//				{
				//					MessageBox.Show("Please select the Route name to Delete");
				//				}
				//				else
				//				{
				if(DropDownList1.Visible==true)
				{
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(txtrname.Text.ToString());
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/RouteMaster/DeleteRoute?route=" + txtrname.Text.ToString(), byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            MessageBox.Show("Route Deleted");
                            CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btn_Delete " + " Route Name  " + txtrname.Text.Trim().ToString() + " deleted   userid " + uid);
                        }
                        else
                            response.EnsureSuccessStatusCode();
                    }

                    //SqlConnection con10;
                    //SqlCommand cmdselect10;
                    //SqlDataReader dtredit10;
                    //string strdelete10;
                    //con10=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                    //con10.Open ();

                    //strdelete10 = "Delete Route where Route_name =@Route_name";
                    //cmdselect10 = new SqlCommand( strdelete10, con10);
                    //cmdselect10.Parameters .Add ("@Route_name",txtrname.Text .ToString ());
                    //dtredit10 = cmdselect10.ExecuteReader();

                    
					Clear();
					fill();
					Button1.Visible=true;
					btnDel.Enabled=true;
					btnsave.Visible=false;
					GetNextRouteNo();
					btnEdit.Visible=true;
					checkPrevileges();
				}
				else
				{
					MessageBox.Show("Please Click The Edit button for Editing the Record");
					return;
				}
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btn_Delete  Route  "+txtrname.Text .ToString ()+" deleted  userid   "+ uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btn_Delete "+ " EXCEPTION  "+ex.Message +"  userid  "+ uid );
			}
		}
		# endregion

		protected void txtrkm_TextChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This method is used to fatch the next Route ID from database.
		/// </summary>
		public void GetNextRouteNo()
		{
            try
            {
                string strVehicleLBID = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/RouteMaster/GetNextRouteID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        strVehicleLBID = JsonConvert.DeserializeObject<string>(id);
                        lblRouteID.Text = strVehicleLBID;
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                //InventoryClass obj=new InventoryClass();
                //SqlDataReader SqlDtr;
                //string sql;

                //#region Fetch the Next Route ID
                //sql="select Max(Route_ID)+1 from Route";
                //SqlDtr=obj.GetRecordSet(sql);
                //if(SqlDtr.Read())
                //{
                //	lblRouteID.Text=SqlDtr.GetValue(0).ToString();
                //	if(lblRouteID.Text=="")
                //	{
                //		lblRouteID.Text="1";
                //	}
                //}
                //else
                //	lblRouteID.Text="1";
                //SqlDtr.Close ();		
                //#endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:GetNextRouteNo " + " EXCEPTION  " + ex.Message + "  userid  " + uid);
            }
        }
    }
}
