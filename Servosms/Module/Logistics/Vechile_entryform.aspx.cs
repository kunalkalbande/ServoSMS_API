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
using DBOperations;
using RMG;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using Servo_API.Models;
#endregion

namespace Servosms.Module.Logistics
{
	/// <summary>
	/// Summary description for Vechile_entryform.
	/// </summary>
	public partial class Vechile_entryform : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.HyperLink Home;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator8;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator9;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator11;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator12;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator10;
		protected System.Web.UI.WebControls.CompareValidator Comparevalidator2;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
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
        #region PageLoad...
        protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());

				if(!Page.IsPostBack)
				{					
					fillOilCombo();
					getID();
					btnSave.Enabled = true;
					btnEdit.Enabled  = false;
					btnDelete.Enabled = false;
					checkPrevileges();
					txtrtoregvalidity.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;     
					txtVehicleyear.Text  = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;     
					txtvalidityinsurance.Text= System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;     
					# region Dropdown Route Name
					SqlConnection con11;
					SqlCommand cmdselect11;
					SqlDataReader dtrdrive11;
					con11=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con11.Open ();
					cmdselect11 = new SqlCommand( "Select Route_name  From Route", con11 );
					dtrdrive11 = cmdselect11.ExecuteReader();
					DropDownList1.Items.Add("Select");
					while(dtrdrive11.Read())
					{
						DropDownList1.Items.Add(dtrdrive11.GetString(0));
					}
					dtrdrive11.Close();
					con11.Close ();
					# endregion
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
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
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="6";
			string SubModule="3";
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
				btnSave.Enabled=false;
			if(Edit_Flag=="0")
				btnEdit.Enabled=false;
			if(Del_Flag=="0")
				btnDelete.Enabled=false;
			#endregion
		}

		/// <summary>
		/// This method fills the Oil related combos from table products.
		/// </summary>
		public void fillOilCombo()
		{
			try
			{				
				DropEngineOil.Items.Clear();
				DropEngineOil.Items.Add("Select");

                List<string> lstDropEngineOil = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropEngineOil").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropEngineOil = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropEngineOil != null)
                {
                    foreach (var EngineOil in lstDropEngineOil)
                        DropEngineOil.Items.Add(EngineOil);
                }

    //            while (SqlDtr.Read())
				//{
				//	DropEngineOil.Items.Add(SqlDtr.GetValue(0).ToString());   
				//}
				//SqlDtr.Close();
               
				//dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Brake Oil%'",ref SqlDtr);
				Dropbreak.Items.Clear();
				Dropbreak.Items.Add("Select");

                List<string> lstDropbreak = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropbreak").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropbreak = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropbreak != null)
                {
                    foreach (var Break in lstDropbreak)
                        Dropbreak.Items.Add(Break);
                }

    //            while (SqlDtr.Read())
				//{
				//	Dropbreak.Items.Add(SqlDtr.GetValue(0).ToString());   
				//}
				//SqlDtr.Close();

				//dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Gear Oil%'",ref SqlDtr);

				Dropgear.Items.Clear();
				Dropgear.Items.Add("Select");

                List<string> lstDropGear = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropGear").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropGear = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropGear != null)
                {
                    foreach (var Gear in lstDropGear)
                        Dropgear.Items.Add(Gear);
                }

    //            while (SqlDtr.Read())
				//{
				//	Dropgear.Items.Add(SqlDtr.GetValue(0).ToString());   
				//}
				//SqlDtr.Close();

//				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Collent%'",ref SqlDtr);
				Dropcoolent.Items.Clear();
				Dropcoolent.Items.Add("Select");

                List<string> lstDropCoolent = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropCoolent").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropCoolent = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropCoolent != null)
                {
                    foreach (var Coolent in lstDropCoolent)
                        Dropcoolent.Items.Add(Coolent);
                }

    //            while (SqlDtr.Read())
				//{
				//	Dropcoolent.Items.Add(SqlDtr.GetValue(0).ToString());   
				//}
				//SqlDtr.Close();
			 
//				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Grease%'",ref SqlDtr);
				Dropgrease.Items.Clear();
				Dropgrease.Items.Add("Select");

                List<string> lstDropGrease = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropGrease").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropGrease = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropGrease != null)
                {
                    foreach (var Grease in lstDropGrease)
                        Dropgrease.Items.Add(Grease);
                }

				Droptransmission.Items.Clear();
				Droptransmission.Items.Add("Select");

                List<string> lstDropTransmission = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropTransmission").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropTransmission = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropTransmission != null)
                {
                    foreach (var Transmission in lstDropTransmission)
                        Droptransmission.Items.Add(Transmission);
                }                
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:fillOilCombo() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// This method returns the next vehicle ID from vehicle entry table.
		/// </summary>
		public void getID()
		{
			try
			{
                string strVehicleID = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/GetNextVehicledetailID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        strVehicleID = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                lblVehicleID.Text = strVehicleID;  
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:getID() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
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
		/// This method clears the whole form.
		/// </summary>
		public void Clear()
		{
			Dropbreak.SelectedIndex=0;
			DropEngineOil.SelectedIndex = 0;
			Dropcoolent.SelectedIndex=0;
			DropDownList1.SelectedIndex=0;
			DropFuelused.SelectedIndex=0;
			Dropgear.SelectedIndex=0;
			Dropgrease.SelectedIndex=0;
			Droptransmission.SelectedIndex=0;
			DropVechileType2.SelectedIndex=0;
			txtEngineKM.Text = "";
			txtEngineOilDate.Text = "";
			txtEngineQty.Text = "";
			txtInsCompName.Text = ""; 
			txtbearkinword.Text="";
			txtbreakdt.Text="";
			txtbreakkm.Text="";
			txtcoolentdt.Text="";
			txtcoolentinword.Text="";
			txtcoolentkm.Text="";
	
			txtfuelintank.Text="";
			txtfuelinword.Text="";
		
			txtgeardt.Text="";
			txtgearinword.Text="";
			txtgearkm.Text="";
			txtgreasedt.Text="";
			txtgreaseinword.Text="";
			txtgreasekm.Text="";
			txtinsuranceno.Text="";
			txtmodelnm.Text="";
			txtrtono.Text="";
			txtrtoregvalidity.Text=System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;
			txtVehicleyear.Text  = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;     
			txtvalidityinsurance.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;     
			txttransinword.Text="";
			txttransmissiondt.Text="";
			txttransmissionkm.Text="";
			txtvechileavarge.Text="";
			txtVehiclemreading.Text="";
			txtVehiclenm.Text="";
			txtVehicleno.Text="";
		}

		/// <summary>
		/// This method is used to call the Clear() function for clear the form or reset the form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnReset_Click(object sender, System.EventArgs e)
		{
			Clear();
		}

		/// <summary>
		/// This method returns the date in MM/DD/YYYY format.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string ToMMddYYYY(string str)
		{
			string date="";
			if(!str.Trim().Equals("")) 
			{
				string [] strarr = new string[3];			
				strarr=str.IndexOf("/")>0? str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
				date = strarr[1]+"/"+strarr[0]+"/"+strarr[2];
				return date;
			}
			else
			{
				return date;
			}
		}

		/// <summary>
		/// This is used to save the vehicle entry into the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
                VehicleEntryModel vehEntryModel = new VehicleEntryModel();
                vehEntryModel.Vehicle_ID = lblVehicleID.Text;
                vehEntryModel.VehicleType2 = DropVechileType2.SelectedItem.Text.ToString();
                vehEntryModel.Vehicleno = txtVehicleno.Text.Trim();

                vehEntryModel.Vehiclenm = txtVehiclenm.Text.Trim();

                vehEntryModel.RTO_Reg_Val_yrs = GenUtil.str2MMDDYYYY(Request.Form["txtrtoregvalidity"].ToString().Trim());
                vehEntryModel.Model_name = txtmodelnm.Text.Trim();

                vehEntryModel.RTO_Reg_No = txtrtono.Text.Trim();
                vehEntryModel.Vehicle_Man_Date = GenUtil.str2MMDDYYYY(Request.Form["txtVehicleyear"].ToString().Trim());
                vehEntryModel.Insurance_No = txtinsuranceno.Text.Trim();
                vehEntryModel.Meter_Reading = txtVehiclemreading.Text.Trim();
                vehEntryModel.Insurance_validity = GenUtil.str2MMDDYYYY(Request.Form["txtvalidityinsurance"].ToString().Trim());
                vehEntryModel.RouteName = DropDownList1.SelectedItem.Text.Trim();
                vehEntryModel.Insurance_Comp_name = txtInsCompName.Text.Trim();

                vehEntryModel.Fuel_Used = DropFuelused.SelectedItem.Text.Trim();
                vehEntryModel.Fuel_Used_Qty = txtfuelinword.Text.Trim();
                vehEntryModel.Start_Fuel_Qty = txtfuelintank.Text.Trim();

                vehEntryModel.EngineOil = DropEngineOil.SelectedItem.Text.Trim();
                vehEntryModel.Engine_Oil_Qty = txtEngineQty.Text.Trim();
                vehEntryModel.Engine_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtEngineOilDate"].ToString().Trim());

                vehEntryModel.Gear_Oil = Dropgear.SelectedItem.Text.Trim();
                vehEntryModel.Gear_Oil_Qty = txtgearinword.Text.Trim();
                vehEntryModel.Gear_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtgeardt"].ToString().Trim());

                vehEntryModel.Brake_Oil = Dropbreak.SelectedItem.Text.Trim();
                vehEntryModel.Brake_Oil_Qty = txtbearkinword.Text.Trim();
                vehEntryModel.Brake_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtbreakdt"].ToString().Trim());
                
                vehEntryModel.Coolent = Dropcoolent.SelectedItem.Text.Trim();
                vehEntryModel.Coolent_Oil_Qty = txtcoolentinword.Text.Trim();
                vehEntryModel.Coolent_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtcoolentdt"].ToString().Trim());

                vehEntryModel.Grease = Dropgrease.SelectedItem.Text.Trim();
                vehEntryModel.Grease_Qty = txtgreaseinword.Text.Trim();
                vehEntryModel.Grease_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtgreasedt"].ToString().Trim());                

                vehEntryModel.Trans_Oil = Droptransmission.SelectedItem.Text.Trim();
                vehEntryModel.Trans_Oil_Qty = txttransinword.Text.Trim();
                vehEntryModel.Trans_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txttransmissiondt"].ToString().Trim());
                vehEntryModel.Trans_Oil_km = txttransmissionkm.Text.Trim();
                vehEntryModel.Vehicle_Avg = txtvechileavarge.Text.Trim();

                vehEntryModel.Engine_Oil_km = txtEngineKM.Text.Trim();
                vehEntryModel.Gear_Oil_km = txtgearkm.Text.Trim();
                vehEntryModel.Brake_Oil_km = txtbreakkm.Text.Trim();
                vehEntryModel.Coolent_km = txtcoolentkm.Text.Trim();
                vehEntryModel.Grease_km = txtgreasekm.Text.Trim();


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(vehEntryModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/VehicleEntry/InsertVehicleEntry", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show("Vehicle Saved");
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }
    				
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Save "+ " Vehicle entry for vehicle Name  "+txtVehiclenm.Text.Trim().ToUpper()+" Saved   userid "+ uid );
				Clear();
				getID();
				checkPrevileges(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Save "+ " EXCEPTION  "+ex.Message+"   userid   "+ uid );				
			}
		}

		/// <summary>
		/// returns blank if the date field contains the default value 1/1/1900.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string checkDate(string str)
		{
			if(!str.Trim().Equals(""))
			{
				if(str.Trim().Equals("1/1/1900") || str.Trim().Equals("01/01/1900"))
					str = "";
			}
			return str;
		}

		protected void DropFuelused_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		protected void Dropgear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This method is used to fatch the all vehicle no from databse and fill the dropdownlist on edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit1_Click(object sender, System.EventArgs e)
		{
			try
			{
				Clear();
				lblVehicleID.Visible = false;
				DropVehicleID.Visible = true;
				DropVehicleID.Items.Clear();
				DropVehicleID.Items.Add("Select");
				//SqlDataReader SqlDtr = null;
                //dbobj.SelectQuery("Select vehicledetail_id from vehicleentry ",ref SqlDtr);

                List<string> lstDropVehicleID = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/FillDropVehicleID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lstDropVehicleID = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                if (lstDropVehicleID != null)
                {
                    foreach (var VehicleID in lstDropVehicleID)
                        DropVehicleID.Items.Add(VehicleID);
                }

				btnSave.Enabled  = false;
				btnEdit.Enabled  = true;
				btnDelete.Enabled  = true;
				btnEdit1.Visible  = false;
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btnEdit1_Click"+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// This method is used to fatch the particular record according to select the vehicle no from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropVehicleID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            VehicleEntryModel vehEntryModel = new VehicleEntryModel();
            try
			{
				if(DropVehicleID.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Vehicle ID");
					return;
				}
				Clear();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/VehicleEntry/GetDropVehicleID_SelectedData?VehicleID=" + DropVehicleID.SelectedItem.Text.Trim()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        vehEntryModel = JsonConvert.DeserializeObject<VehicleEntryModel>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

				if(vehEntryModel != null)
                { 
					DropVechileType2.SelectedIndex = DropVechileType2.Items.IndexOf( DropVechileType2.Items.FindByText(vehEntryModel.VehicleType2.Trim()));
					txtVehicleno.Text = vehEntryModel.Vehicleno.Trim();
					txtVehiclenm.Text = vehEntryModel.Vehiclenm.Trim();
					txtrtoregvalidity.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.RTO_Reg_Val_yrs.Trim())));
					txtmodelnm.Text = vehEntryModel.Model_name.Trim();
					txtrtono.Text = vehEntryModel.RTO_Reg_No.Trim();   
					txtVehicleyear.Text =  checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Vehicle_Man_Date.ToString().Trim())));
					txtinsuranceno.Text = vehEntryModel.Insurance_No.Trim(); 
					txtVehiclemreading.Text = vehEntryModel.Meter_Reading.Trim(); 
					txtvalidityinsurance.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Insurance_validity.Trim()))); 
					
					DropDownList1.SelectedIndex  = DropDownList1.Items.IndexOf( DropDownList1.Items.FindByText(vehEntryModel.RouteName));

					txtInsCompName.Text = vehEntryModel.Insurance_Comp_name.Trim(); 					
					DropFuelused.SelectedIndex  = DropFuelused.Items.IndexOf( DropFuelused.Items.FindByText(vehEntryModel.Fuel_Used.Trim()));
					txtfuelinword.Text = vehEntryModel.Fuel_Used_Qty.Trim(); 
					txtfuelintank.Text = vehEntryModel.Start_Fuel_Qty.Trim();  
					
					DropEngineOil.SelectedIndex  = DropEngineOil.Items.IndexOf(DropEngineOil.Items.FindByText(vehEntryModel.EngineOil));
					txtEngineQty.Text = vehEntryModel.Engine_Oil_Qty.Trim(); 
					txtEngineOilDate.Text =  checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Engine_Oil_Dt.Trim())));  
					txtEngineKM.Text = vehEntryModel.Engine_Oil_km.Trim();  					

					Dropgear.SelectedIndex  = Dropgear.Items.IndexOf( Dropgear.Items.FindByText(vehEntryModel.Gear_Oil));
					txtgearinword.Text = vehEntryModel.Gear_Oil_Qty.Trim(); 
					txtgeardt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Gear_Oil_Dt.Trim())));  
					txtgearkm .Text = vehEntryModel.Gear_Oil_km.Trim();  
					
					Dropbreak.SelectedIndex  =  Dropbreak.Items.IndexOf( Dropbreak.Items.FindByText(vehEntryModel.Brake_Oil));
					txtbearkinword.Text = vehEntryModel.Brake_Oil_Qty.Trim(); 
					txtbreakdt .Text =checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Brake_Oil_Dt.Trim())));  
					txtbreakkm.Text = vehEntryModel.Brake_Oil_km.Trim();                      

					Dropcoolent.SelectedIndex  = Dropcoolent.Items.IndexOf( Dropcoolent.Items.FindByText(vehEntryModel.Coolent));
					txtcoolentinword .Text = vehEntryModel.Coolent_Oil_Qty.Trim(); 
					txtcoolentdt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Coolent_Oil_Dt.Trim())));  
					txtcoolentkm.Text = vehEntryModel.Coolent_km.Trim();  
                    
					Dropgrease .SelectedIndex  = Dropgrease.Items.IndexOf( Dropgrease.Items.FindByText(vehEntryModel.Grease));
					txtgreaseinword .Text = vehEntryModel.Grease_Qty.Trim(); 
					txtgreasedt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Grease_Dt.Trim())));  
					txtgreasekm.Text = vehEntryModel.Grease_km.Trim();                      

					Droptransmission.SelectedIndex  = Droptransmission.Items.IndexOf( Droptransmission.Items.FindByText(vehEntryModel.Trans_Oil));
					txttransinword .Text = vehEntryModel.Trans_Oil_Qty.Trim(); 
					txttransmissiondt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(vehEntryModel.Trans_Oil_Dt.Trim())));  
					txttransmissionkm.Text = vehEntryModel.Trans_Oil_km.Trim();  

					txtvechileavarge.Text = vehEntryModel.Vehicle_Avg.ToString();
					checkPrevileges();

                }
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:DropVehicleID_SelectedIndexChanged "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}  

		/// <summary>
		/// This function returns only date from date and time .
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

		/// <summary>
		/// This method is used to update the vehicle no information on edit time
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			
			if(DropVehicleID.SelectedIndex == 0)
			{
				MessageBox.Show("Please select Vehicle ID");
				return;
			}
            try
            {
                VehicleEntryModel vehEntryModel = new VehicleEntryModel();
                vehEntryModel.Vehicle_ID = DropVehicleID.SelectedItem.Text.Trim();
                vehEntryModel.VehicleType2 = DropVechileType2.SelectedItem.Text.ToString();
                vehEntryModel.Vehicleno = txtVehicleno.Text.Trim();

                vehEntryModel.Vehiclenm = txtVehiclenm.Text.Trim();

                vehEntryModel.RTO_Reg_Val_yrs = GenUtil.str2MMDDYYYY(Request.Form["txtrtoregvalidity"].ToString().Trim());
                vehEntryModel.Model_name = txtmodelnm.Text.Trim();

                vehEntryModel.RTO_Reg_No = txtrtono.Text.Trim();
                vehEntryModel.Vehicle_Man_Date = GenUtil.str2MMDDYYYY(Request.Form["txtVehicleyear"].ToString().Trim());
                vehEntryModel.Insurance_No = txtinsuranceno.Text.Trim();
                vehEntryModel.Meter_Reading = txtVehiclemreading.Text.Trim();
                vehEntryModel.Insurance_validity = GenUtil.str2MMDDYYYY(Request.Form["txtvalidityinsurance"].ToString().Trim());
                vehEntryModel.RouteName = DropDownList1.SelectedItem.Text.Trim();
                vehEntryModel.Insurance_Comp_name = txtInsCompName.Text.Trim();

                vehEntryModel.Fuel_Used = DropFuelused.SelectedItem.Text.Trim();
                vehEntryModel.Fuel_Used_Qty = txtfuelinword.Text.Trim();
                vehEntryModel.Start_Fuel_Qty = txtfuelintank.Text.Trim();

                vehEntryModel.EngineOil = DropEngineOil.SelectedItem.Text.Trim();
                vehEntryModel.Engine_Oil_Qty = txtEngineQty.Text.Trim();
                vehEntryModel.Engine_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtEngineOilDate"].ToString().Trim());

                vehEntryModel.Gear_Oil = Dropgear.SelectedItem.Text.Trim();
                vehEntryModel.Gear_Oil_Qty = txtgearinword.Text.Trim();
                vehEntryModel.Gear_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtgeardt"].ToString().Trim());

                vehEntryModel.Brake_Oil = Dropbreak.SelectedItem.Text.Trim();
                vehEntryModel.Brake_Oil_Qty = txtbearkinword.Text.Trim();
                vehEntryModel.Brake_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtbreakdt"].ToString().Trim());

                vehEntryModel.Coolent = Dropcoolent.SelectedItem.Text.Trim();
                vehEntryModel.Coolent_Oil_Qty = txtcoolentinword.Text.Trim();
                vehEntryModel.Coolent_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtcoolentdt"].ToString().Trim());

                vehEntryModel.Grease = Dropgrease.SelectedItem.Text.Trim();
                vehEntryModel.Grease_Qty = txtgreaseinword.Text.Trim();
                vehEntryModel.Grease_Dt = GenUtil.str2MMDDYYYY(Request.Form["txtgreasedt"].ToString().Trim());

                vehEntryModel.Trans_Oil = Droptransmission.SelectedItem.Text.Trim();
                vehEntryModel.Trans_Oil_Qty = txttransinword.Text.Trim();
                vehEntryModel.Trans_Oil_Dt = GenUtil.str2MMDDYYYY(Request.Form["txttransmissiondt"].ToString().Trim());
                vehEntryModel.Trans_Oil_km = txttransmissionkm.Text.Trim();
                vehEntryModel.Vehicle_Avg = txtvechileavarge.Text.Trim();

                vehEntryModel.Engine_Oil_km = txtEngineKM.Text.Trim();
                vehEntryModel.Gear_Oil_km = txtgearkm.Text.Trim();
                vehEntryModel.Brake_Oil_km = txtbreakkm.Text.Trim();
                vehEntryModel.Coolent_km = txtcoolentkm.Text.Trim();
                vehEntryModel.Grease_km = txtgreasekm.Text.Trim();
                vehEntryModel.Vehicle_ID = DropVehicleID.SelectedItem.Text.Trim();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(vehEntryModel);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/VehicleEntry/UpdateVehicleEntry", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show("Vehicle Updated");
                        CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Edit " + " Vehicle entry for vehicle Name  " + txtVehiclenm.Text.Trim().ToUpper() + " Updated   userid " + uid);
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }
   
				
				Clear();
				btnSave .Enabled = true;
				btnEdit.Enabled  = false;
				btnEdit1.Visible = true;
				DropVehicleID.Visible = false;
				lblVehicleID.Visible = true;
				btnDelete.Enabled = false;
				getID();
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Edit "+ " EXCEPTION  "+ex.Message+"   userid   "+ uid );
			}
		}

		/// <summary>
		/// This method is used to delete the particular record, who is select from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropVehicleID.SelectedIndex == 0)
				{
					MessageBox.Show("Please Select Vehicle ID");
					return;
				}

                int c = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(DropVehicleID.SelectedItem.Text.Trim());
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/VehicleEntry/DeleteVehicleEntry?vehicleID=" + DropVehicleID.SelectedItem.Text.Trim(), byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        c = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(responseString);
                        MessageBox.Show("Vehicle Deleted");
                        CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Delete " + " Vehicle entry for vehicle Name  " + txtVehiclenm.Text.Trim().ToUpper() + " Deleted   userid " + uid);
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }

                
				//dbobj.Insert_or_Update("Delete from vehicleentry where vehicledetail_id = "+DropVehicleID.SelectedItem.Text.Trim(),ref c);    
				if(c > 0)
				{					
					Clear();
					btnSave .Enabled = true;
					btnEdit.Enabled  = false;
					btnEdit1.Visible = true;
					DropVehicleID.Visible = false;
					lblVehicleID.Visible = true;
					btnDelete.Enabled = false;
					getID();
					checkPrevileges();

				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Delete "+ " EXCEPTION  "+ex.Message+"   userid   "+ uid );
			}
		}

		protected void DropVechileType2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
	}
}