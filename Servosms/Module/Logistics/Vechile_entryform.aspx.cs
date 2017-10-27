/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
    # region Directives...
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
using System.Data .SqlClient ;
using System.Text;
using Servosms.Sysitem.Classes ;
using DBOperations;
using RMG;
# endregion

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

		/// <summary>
		/// Put user code to initialize the page here
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region PageLoad...
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());

				if(!Page.IsPostBack)
				{
					# region Dropdown Vehicle type
					/*SqlConnection con;
					SqlCommand cmdselect;
					SqlDataReader dtrVechile;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					cmdselect = new SqlCommand( "Select Vehicle_Type  From Vehicle", con );
					dtrVechile = cmdselect.ExecuteReader();
					DropVechileType2.Items.Add("Select");
					while(dtrVechile.Read())
					{
						DropVechileType2.Items.Add(dtrVechile.GetString(0));
					}
					dtrVechile.Close();
					con.Close ();*/
					# endregion
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
				SqlDataReader SqlDtr = null;
				//				dbobj.SelectQuery("Select prod_name from products where category = 'Fuel'",ref SqlDtr);
				//				DropFuelused.Items.Clear();
				//				DropFuelused.Items.Add("Select");   
				//				while(SqlDtr.Read())
				//				{
				//					DropFuelused.Items.Add(SqlDtr.GetValue(0).ToString());   
				//				}
				//				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Engine Oil%'",ref SqlDtr);
				DropEngineOil.Items.Clear();
				DropEngineOil.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					DropEngineOil.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();
               
				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Brake Oil%'",ref SqlDtr);
				Dropbreak.Items.Clear();
				Dropbreak.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropbreak.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Gear Oil%'",ref SqlDtr);
				Dropgear.Items.Clear();
				Dropgear.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropgear.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Collent%'",ref SqlDtr);
				Dropcoolent.Items.Clear();
				Dropcoolent.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropcoolent.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();
			 
				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Grease%'",ref SqlDtr);
				Dropgrease.Items.Clear();
				Dropgrease.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropgrease.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Transmission Oil%'",ref SqlDtr);
				Droptransmission.Items.Clear();
				Droptransmission.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Droptransmission.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();
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
				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("Select max(vehicledetail_id) from vehicleentry",ref SqlDtr);
				if(SqlDtr.Read())
				{
					string str = SqlDtr.GetValue(0).ToString();
					if(!str.Trim().Equals(""))  
					{
						int id  = System.Convert.ToInt32(str.Trim());
						id = id + 1;
						str= id.ToString();
					}
					else
					{
						str = "1001";
					}
					lblVehicleID.Text = str;
 
				}
				else
				{
					lblVehicleID.Text = "1001";
				}
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
				SqlConnection con;
				string strInsert ;
				SqlCommand cmdInsert;
				DateTime dt1=DateTime.Now ;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				strInsert = "Insert Vehicleentry(vehicledetail_id,Vehicle_Type,Vehicle_no,vehicle_name,RTO_Reg_Val_yrs,Model_name,RTO_Reg_No,Vehicle_Man_Date,Insurance_No,Meter_Reading,Insurance_validity,Vehicle_Route,Insurance_Comp_Name,Fuel_Used,Fuel_Used_Qty,start_Fuel_Qty,Engine_Oil,Engine_Oil_Qty,Engine_Oil_Dt,Engine_Oil_km,Gear_Oil,Gear_Oil_Qty,Gear_Oil_Dt,Gear_Oil_Km,Brake_Oil,Brake_Oil_Qty,Brake_Oil_Dt,Brake_Oil_km,Coolent,Coolent_Qty,Coolent_Dt,Coolent_Km,Grease,Grease_Qty,Grease_Dt,Grease_Km,Trans_Oil,Trans_Oil_Qty,Trans_Oil_Dt,Trans_Oil_Km,Vehicle_Avg)"
					+"values(@vehicledetail_id,@Vehicle_Type,@Vehicle_no,@vehicle_name,@RTO_Reg_Val_yrs,@Model_name,@RTO_Reg_No,@Vehicle_Man_Date,@Insurance_No,@Meter_Reading,@Insurance_validity,@Vehicle_Route,@Insurance_Comp_Name,@Fuel_Used,@Fuel_Used_Qty,@start_Fuel_Qty,@Engine_Oil,@Engine_Oil_Qty,@Engine_Oil_Dt,@Engine_Oil_km,@Gear_Oil,@Gear_Oil_Qty,@Gear_Oil_Dt,@Gear_Oil_Km,@Brake_Oil,@Brake_Oil_Qty,@Brake_Oil_Dt,@Brake_Oil_km,@Coolent,@Coolent_Qty,@Coolent_Dt,@Coolent_Km,@Grease,@Grease_Qty,@Grease_Dt,@Grease_Km,@Trans_Oil,@Trans_Oil_Qty,@Trans_Oil_Dt,@Trans_Oil_Km,@Vehicle_Avg)";

				cmdInsert=new SqlCommand (strInsert,con);
				cmdInsert.Parameters .Add ("@vehicledetail_id",lblVehicleID.Text);
				cmdInsert.Parameters .Add ("@Vehicle_Type",DropVechileType2.SelectedItem .Text .ToString () );
				cmdInsert.Parameters .Add ("@Vehicle_no",txtVehicleno.Text.Trim());
				cmdInsert.Parameters .Add ("@Vehicle_name",txtVehiclenm .Text.Trim());
				cmdInsert.Parameters .Add ("@RTO_Reg_Val_yrs",GenUtil.str2MMDDYYYY(Request.Form["txtrtoregvalidity"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Model_name",txtmodelnm.Text.Trim());
				cmdInsert.Parameters .Add ("@RTO_Reg_No",txtrtono.Text.Trim());
				cmdInsert.Parameters .Add ("@Vehicle_Man_Date",GenUtil.str2MMDDYYYY(Request.Form["txtVehicleyear"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Insurance_No",txtinsuranceno.Text.Trim());
				cmdInsert.Parameters .Add ("@Meter_Reading",txtVehiclemreading.Text.Trim());
				cmdInsert.Parameters .Add ("@Insurance_validity",GenUtil.str2MMDDYYYY(Request.Form["txtvalidityinsurance"].ToString().Trim()));

                SqlDataReader SqlDtr  = null;
				string route_id = "";
				//Fetch the route id for selected route from route table.
				dbobj.SelectQuery("Select route_id from Route where route_name='"+DropDownList1.SelectedItem.Text.Trim()+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
					route_id = SqlDtr.GetValue(0).ToString();       
				}
				SqlDtr.Close();
				cmdInsert.Parameters .Add ("@Vehicle_Route",route_id);
				cmdInsert.Parameters .Add ("@Insurance_Comp_name",txtInsCompName.Text.Trim() );
				string prod_id = "";
				//Fetch the product id for selected product of type fuel from table products.
				//				dbobj.SelectQuery("Select prod_id from products where prod_name='"+DropFuelused.SelectedItem.Text.Trim()+"' and Category ='Fuel'" ,ref SqlDtr);
				//				if(SqlDtr.Read())
				//				{
				//					prod_id = SqlDtr.GetValue(0).ToString();       
				//				}
				//				SqlDtr.Close();
				//cmdInsert.Parameters .Add ("@Fuel_Used",prod_id );
				cmdInsert.Parameters .Add ("@Fuel_Used",DropFuelused.SelectedItem.Text.Trim());
				cmdInsert.Parameters .Add ("@Fuel_Used_Qty",txtfuelinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Start_Fuel_Qty",txtfuelintank.Text.Trim() );
				//Fetch the product id for selected product of type Engine Oil from table products.
				string[] strArr = DropEngineOil.SelectedItem.Text.Split(new char[] {':'},DropEngineOil.SelectedItem.Text.Length);    
				if(DropEngineOil.SelectedIndex != 0)
				{
					
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Engine Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Engine_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Engine_Oil_Qty",txtEngineQty.Text.Trim());
				cmdInsert.Parameters .Add ("@Engine_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtEngineOilDate"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Engine_Oil_km",txtEngineKM.Text.Trim());
				//Fetch the product id for selected product of type Gear Oil from table products.
				if(Dropgear.SelectedIndex != 0)
				{
					strArr = Dropgear.SelectedItem.Text.Split(new char[] {':'},Dropgear.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Gear Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Gear_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Gear_Oil_Qty",txtgearinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Gear_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtgeardt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Gear_Oil_km",txtgearkm.Text.Trim());
				//Fetch the product id for selected product of type Brake Oil from table products. 
				if(Dropbreak.SelectedIndex != 0)
				{
					strArr = Dropbreak.SelectedItem.Text.Split(new char[] {':'},Dropbreak.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Brake Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Brake_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Brake_Oil_Qty",txtbearkinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Brake_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtbreakdt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Brake_Oil_km",txtbreakkm.Text.Trim());
				//Fetch the product id for selected product of type Coolent from table products. 
				if(Dropcoolent.SelectedIndex != 0)
				{
					strArr = Dropcoolent.SelectedItem.Text.Split(new char[] {':'},Dropcoolent.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Collent%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Coolent",prod_id);
				cmdInsert.Parameters .Add ("@Coolent_Qty",txtcoolentinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Coolent_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtcoolentdt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Coolent_km",txtcoolentkm.Text.Trim());
				//Fetch the product id for selected product of type Grease from table products.
				if(Dropgrease.SelectedIndex != 0)
				{
					strArr = Dropgrease.SelectedItem.Text.Split(new char[] {':'},Dropgrease.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Grease%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Grease",prod_id);
				cmdInsert.Parameters .Add ("@Grease_Qty",txtgreaseinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Grease_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtgreasedt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Grease_km",txtgreasekm.Text.Trim());
				//Fetch the product id for selected product of type Transmission Oil from table products.
				if(Droptransmission.SelectedIndex != 0)
				{
					strArr = Droptransmission.SelectedItem.Text.Split(new char[] {':'},Droptransmission.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Transmission Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Trans_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Trans_Oil_Qty",txttransinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Trans_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txttransmissiondt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Trans_Oil_km",txttransmissionkm.Text.Trim());
				cmdInsert.Parameters .Add ("@Vehicle_Avg",txtvechileavarge.Text.Trim());			
				cmdInsert.ExecuteNonQuery();
				con.Close ();
				MessageBox.Show("Vehicle Saved");
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Save "+ " Vehicle entry for vehicle Name  "+txtVehiclenm.Text.Trim().ToUpper()+" Saved   userid "+ uid );
				Clear();
				getID();
				checkPrevileges(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Save "+ " EXCEPTION  "+ex.Message+"   userid   "+ uid );
				//Response.Write(ex.Message +"hh45er"+ex.StackTrace); 
				//Response.Redirect("HolidayEntryForm.aspx"); 
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
				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("Select vehicledetail_id from vehicleentry ",ref SqlDtr);
				while(SqlDtr.Read())
				{
					DropVehicleID.Items.Add(SqlDtr.GetValue(0).ToString());    
				}
				SqlDtr.Close();
				btnSave .Enabled  = false;
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
			try
			{
				if(DropVehicleID.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Vehicle ID");
					return;
				}
				Clear();
				SqlDataReader SqlDtr = null;
				SqlDataReader SqlDtr1= null;
				dbobj.SelectQuery("Select * from vehicleentry where vehicledetail_id = "+DropVehicleID.SelectedItem.Text.Trim(),ref SqlDtr);
				if(SqlDtr.Read())
				{
					DropVechileType2.SelectedIndex = DropVechileType2.Items.IndexOf( DropVechileType2.Items.FindByText(SqlDtr["Vehicle_Type"].ToString().Trim()));
					txtVehicleno.Text = SqlDtr["Vehicle_No"].ToString().Trim();
					txtVehiclenm.Text = SqlDtr["Vehicle_Name"].ToString().Trim();
					txtrtoregvalidity.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["RTO_Reg_Val_Yrs"].ToString().Trim())));
					txtmodelnm.Text = SqlDtr["Model_Name"].ToString().Trim();
					txtrtono.Text = SqlDtr["RTO_Reg_No"].ToString().Trim();   
					txtVehicleyear.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Vehicle_man_date"].ToString().Trim())));
					txtinsuranceno.Text = SqlDtr["Insurance_No"].ToString().Trim(); 
					txtVehiclemreading.Text = SqlDtr["Meter_Reading"].ToString().Trim(); 
					txtvalidityinsurance.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Insurance_Validity"].ToString().Trim()))); 
					string route_name = "";
					dbobj.SelectQuery("Select route_name from route where route_id="+SqlDtr["Vehicle_Route"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						route_name = SqlDtr1.GetValue(0).ToString();  
					}
					SqlDtr1.Close();
					DropDownList1.SelectedIndex  = DropDownList1.Items.IndexOf( DropDownList1.Items.FindByText(route_name));

					txtInsCompName.Text =  SqlDtr["Insurance_Comp_Name"].ToString().Trim(); 
					//					string fuel_used = "";
					//					dbobj.SelectQuery("Select Fuel_used from vehicleentry where vehicledetail_id='"+DropVehicleID.SelectedItem.Text+"'",ref SqlDtr1); 
					//					if(SqlDtr1.Read())
					//					{
					//						fuel_used = SqlDtr1.GetValue(0).ToString();  
					//
					//					}
					//					SqlDtr1.Close();
					DropFuelused.SelectedIndex  = DropFuelused.Items.IndexOf( DropFuelused.Items.FindByText(SqlDtr["Fuel_Used"].ToString().Trim()));
					txtfuelinword.Text =  SqlDtr["Fuel_Used_Qty"].ToString().Trim(); 
					txtfuelintank.Text = SqlDtr["Start_Fuel_Qty"].ToString().Trim();  

					string engine_oil = "";
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Engine Oil%' and  prod_id="+SqlDtr["Engine_Oil"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						engine_oil = SqlDtr1.GetValue(0).ToString();  

					}
					SqlDtr1.Close();
					DropEngineOil .SelectedIndex  = DropEngineOil.Items.IndexOf( DropEngineOil.Items.FindByText(engine_oil));
					txtEngineQty.Text =  SqlDtr["Engine_Oil_Qty"].ToString().Trim(); 
					txtEngineOilDate.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Engine_Oil_Dt"].ToString().Trim())));  
					txtEngineKM.Text = SqlDtr ["Engine_OIl_Km"].ToString().Trim();  

					string gear_oil = "";
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Gear Oil%' and  prod_id="+SqlDtr["Gear_Oil"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						gear_oil = SqlDtr1.GetValue(0).ToString();  

					}
					SqlDtr1.Close();
					Dropgear.SelectedIndex  = Dropgear.Items.IndexOf( Dropgear.Items.FindByText(gear_oil));
					txtgearinword.Text =  SqlDtr["Gear_Oil_Qty"].ToString().Trim(); 
					txtgeardt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Gear_Oil_Dt"].ToString().Trim())));  
					txtgearkm .Text = SqlDtr ["Gear_OIl_Km"].ToString().Trim();  

					string brake_oil = "";
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Brake Oil%' and  prod_id="+SqlDtr["Brake_Oil"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						brake_oil = SqlDtr1.GetValue(0).ToString();  

					}
					SqlDtr1.Close();
					Dropbreak.SelectedIndex  = Dropbreak.Items.IndexOf( Dropbreak.Items.FindByText(brake_oil));
					txtbearkinword.Text =  SqlDtr["Brake_Oil_Qty"].ToString().Trim(); 
					txtbreakdt .Text =checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Brake_Oil_Dt"].ToString().Trim())));  
					txtbreakkm.Text = SqlDtr ["Brake_OIl_Km"].ToString().Trim();  

					string coolent = "";
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Collent%' and  prod_id="+SqlDtr["Coolent"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						coolent = SqlDtr1.GetValue(0).ToString();  

					}
					SqlDtr1.Close();
					Dropcoolent.SelectedIndex  = Dropcoolent.Items.IndexOf( Dropcoolent.Items.FindByText(coolent));
					txtcoolentinword .Text =  SqlDtr["Coolent_Qty"].ToString().Trim(); 
					txtcoolentdt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Coolent_Dt"].ToString().Trim())));  
					txtcoolentkm.Text = SqlDtr ["Coolent_Km"].ToString().Trim();  

					string grease = "";
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Grease%' and  prod_id="+SqlDtr["Grease"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						grease = SqlDtr1.GetValue(0).ToString();  

					}
					SqlDtr1.Close();
					Dropgrease .SelectedIndex  = Dropgrease.Items.IndexOf( Dropgrease.Items.FindByText(grease));
					txtgreaseinword .Text =  SqlDtr["Grease_Qty"].ToString().Trim(); 
					txtgreasedt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Grease_Dt"].ToString().Trim())));  
					txtgreasekm.Text = SqlDtr ["grease_Km"].ToString().Trim();  

					string trans_oil = "";
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where Category like 'Transmission Oil%' and  prod_id="+SqlDtr["Trans_OIl"].ToString().Trim(),ref SqlDtr1); 
					if(SqlDtr1.Read())
					{
						trans_oil = SqlDtr1.GetValue(0).ToString();  

					}
					SqlDtr1.Close();
					Droptransmission.SelectedIndex  = Droptransmission.Items.IndexOf( Droptransmission.Items.FindByText(trans_oil));
					txttransinword .Text =  SqlDtr["Trans_OIl_Qty"].ToString().Trim(); 
					txttransmissiondt.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Trans_OIl_Dt"].ToString().Trim())));  
					txttransmissionkm.Text = SqlDtr ["Trans_Oil_Km"].ToString().Trim();  

					txtvechileavarge.Text = SqlDtr["Vehicle_Avg"].ToString();
					checkPrevileges();
                               
				}
				SqlDtr.Close();
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
                SqlConnection con;
                string strInsert;
                SqlCommand cmdInsert;
                DateTime dt1 = DateTime.Now;
                con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                con.Open();
                strInsert = "update Vehicleentry set Vehicle_Type = @Vehicle_Type,Vehicle_no = @Vehicle_no,vehicle_name = @vehicle_name,RTO_Reg_Val_yrs = @RTO_Reg_Val_yrs,Model_name = @Model_name,RTO_Reg_No = @RTO_Reg_No,Vehicle_Man_Date =@Vehicle_Man_Date,Insurance_No = @Insurance_No,Meter_Reading=@Meter_Reading,Insurance_validity = @Insurance_validity,Vehicle_Route = @Vehicle_Route,Insurance_Comp_Name = @Insurance_Comp_Name,Fuel_Used=@Fuel_Used,Fuel_Used_Qty = @Fuel_Used_Qty,start_Fuel_Qty = @start_Fuel_Qty,"
                    + "Engine_Oil = @Engine_Oil,Engine_Oil_Qty =@Engine_Oil_Qty,Engine_Oil_Dt = @Engine_Oil_Dt,Engine_Oil_km = @Engine_Oil_km,Gear_Oil = @Gear_Oil,Gear_Oil_Qty = @Gear_Oil_Qty,Gear_Oil_Dt = @Gear_Oil_Dt,Gear_Oil_Km = @Gear_Oil_Km,Brake_oil = @Brake_Oil,Brake_Oil_Qty = @Brake_Oil_Qty,BRake_Oil_dt = @Brake_Oil_Dt,Brake_OIl_Km = @Brake_Oil_Km,Coolent = @Coolent, Coolent_Qty = @Coolent_Qty, Coolent_Dt =@Coolent_Dt, Coolent_km = @Coolent_Km,Grease = @Grease,Grease_Qty =@Grease_Qty,Grease_Dt = @Grease_Dt,Grease_Km = @Grease_Km,"
                    + "Trans_Oil = @Trans_Oil, Trans_Oil_Qty = @Trans_Oil_Qty, Trans_Oil_Dt = @Trans_Oil_Dt, Trans_Oil_Km = @Trans_Oil_km,Vehicle_Avg = @Vehicle_Avg where Vehicledetail_id = " + DropVehicleID.SelectedItem.Text.Trim();
                cmdInsert = new SqlCommand(strInsert, con);
                cmdInsert.Parameters.Add("@Vehicle_Type", DropVechileType2.SelectedItem.Text.ToString());
                cmdInsert.Parameters.Add("@Vehicle_no", txtVehicleno.Text.Trim());
                cmdInsert.Parameters.Add("@Vehicle_name", txtVehiclenm.Text.Trim());
                cmdInsert.Parameters.Add("@RTO_Reg_Val_yrs", GenUtil.str2MMDDYYYY(Request.Form["txtrtoregvalidity"].ToString().Trim()));
                cmdInsert.Parameters.Add("@Model_name", txtmodelnm.Text.Trim());
                cmdInsert.Parameters.Add("@RTO_Reg_No", txtrtono.Text.Trim());
                cmdInsert.Parameters.Add("@Vehicle_Man_Date", GenUtil.str2MMDDYYYY(Request.Form["txtVehicleyear"].ToString().Trim()));
                cmdInsert.Parameters.Add("@Insurance_No", txtinsuranceno.Text.Trim());
                cmdInsert.Parameters.Add("@Meter_Reading", txtVehiclemreading.Text.Trim());
                cmdInsert.Parameters.Add("@Insurance_validity", GenUtil.str2MMDDYYYY(Request.Form["txtvalidityinsurance"].ToString().Trim()));
                SqlDataReader SqlDtr = null;
                string route_id = "";
                dbobj.SelectQuery("Select route_id from Route where route_name='" + DropDownList1.SelectedItem.Text.Trim() + "'", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    route_id = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                cmdInsert.Parameters.Add("@Vehicle_Route", route_id);
                cmdInsert.Parameters.Add("@Insurance_Comp_name", txtInsCompName.Text.Trim());
                string prod_id = "";
                //					dbobj.SelectQuery("Select prod_id from products where prod_name='"+DropFuelused.SelectedItem.Text.Trim()+"' and Category ='Fuel'" ,ref SqlDtr);
                //					if(SqlDtr.Read())
                //					{
                //						prod_id = SqlDtr.GetValue(0).ToString();       
                //					}
                //					SqlDtr.Close();
                //cmdInsert.Parameters .Add ("@Fuel_Used",prod_id);
                cmdInsert.Parameters.Add("@Fuel_Used", DropFuelused.SelectedItem.Text.Trim());
                cmdInsert.Parameters.Add("@Fuel_Used_Qty", txtfuelinword.Text.Trim());
                cmdInsert.Parameters.Add("@Start_Fuel_Qty", txtfuelintank.Text.Trim());
                string[] strArr = DropEngineOil.SelectedItem.Text.Split(new char[] { ':' }, DropEngineOil.SelectedItem.Text.Length);
                if (DropEngineOil.SelectedIndex != 0)
                {
                    prod_id = "";
                    dbobj.SelectQuery("Select prod_id from products where prod_name='" + strArr[0].Trim() + "' and pack_type ='" + strArr[1].Trim() + "' and Category like 'Engine Oil%'", ref SqlDtr);
                    if (SqlDtr.Read())
                    {
                        prod_id = SqlDtr.GetValue(0).ToString();
                    }
                    SqlDtr.Close();
                }
                else
                {
                    prod_id = "0";
                }
                cmdInsert.Parameters.Add("@Engine_Oil", prod_id);
                cmdInsert.Parameters.Add("@Engine_Oil_Qty", txtEngineQty.Text.Trim());
                cmdInsert.Parameters.Add("@Engine_Oil_Dt", GenUtil.str2MMDDYYYY(Request.Form["txtEngineOilDate"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Engine_Oil_km",txtEngineKM.Text.Trim());

				if(Dropgear.SelectedIndex != 0)
				{
					strArr = Dropgear.SelectedItem.Text.Split(new char[] {':'},Dropgear.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Gear Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Gear_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Gear_Oil_Qty",txtgearinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Gear_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtgeardt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Gear_Oil_km",txtgearkm.Text.Trim());

				if(Dropbreak.SelectedIndex != 0)
				{
					strArr = Dropbreak.SelectedItem.Text.Split(new char[] {':'},Dropbreak.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Brake Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Brake_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Brake_Oil_Qty",txtbearkinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Brake_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtbreakdt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Brake_Oil_km",txtbreakkm.Text.Trim());

				if(Dropcoolent.SelectedIndex != 0 )
				{
					strArr = Dropcoolent.SelectedItem.Text.Split(new char[] {':'},Dropcoolent.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Collent%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Coolent",prod_id);
				cmdInsert.Parameters .Add ("@Coolent_Qty",txtcoolentinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Coolent_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtcoolentdt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Coolent_km",txtcoolentkm.Text.Trim());
				
				if(Dropgrease.SelectedIndex != 0)
				{
					strArr = Dropgrease.SelectedItem.Text.Split(new char[] {':'},Dropgrease.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Grease%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Grease",prod_id);
				cmdInsert.Parameters .Add ("@Grease_Qty",txtgreaseinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Grease_Dt",GenUtil.str2MMDDYYYY(Request.Form["txtgreasedt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Grease_km",txtgreasekm.Text.Trim());

				if(Droptransmission.SelectedIndex != 0)
				{
					strArr = Droptransmission.SelectedItem.Text.Split(new char[] {':'},Droptransmission.SelectedItem.Text.Length);    
					prod_id = "";
					dbobj.SelectQuery("Select prod_id from products where prod_name='"+strArr[0].Trim()+"' and pack_type ='"+strArr[1].Trim()+"' and Category like 'Transmission Oil%'" ,ref SqlDtr);
					if(SqlDtr.Read())
					{
						prod_id = SqlDtr.GetValue(0).ToString();       
					}
					SqlDtr.Close();
				}
				else
				{
					prod_id = "0";
				}
				cmdInsert.Parameters .Add ("@Trans_Oil",prod_id);
				cmdInsert.Parameters .Add ("@Trans_Oil_Qty",txttransinword.Text.Trim());
				cmdInsert.Parameters .Add ("@Trans_Oil_Dt",GenUtil.str2MMDDYYYY(Request.Form["txttransmissiondt"].ToString().Trim()));

                cmdInsert.Parameters .Add ("@Trans_Oil_km",txttransmissionkm.Text.Trim());
				cmdInsert.Parameters .Add ("@Vehicle_Avg",txtvechileavarge.Text.Trim());			
				cmdInsert.ExecuteNonQuery();
				con.Close ();
				MessageBox.Show("Vehicle Updated");
				CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Edit "+ " Vehicle entry for vehicle Name  "+txtVehiclenm.Text.Trim().ToUpper()+" Updated   userid "+ uid );
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
				dbobj.Insert_or_Update("Delete from vehicleentry where vehicledetail_id = "+DropVehicleID.SelectedItem.Text.Trim(),ref c);    
				if(c > 0)
				{
					MessageBox.Show("Vehicle Deleted");
					CreateLogFiles.ErrorLog("Form:Vehicle_EntryForm.aspx,Method:btn_Delete "+ " Vehicle entry for vehicle Name  "+txtVehiclenm.Text.Trim().ToUpper()+" Deleted   userid "+ uid );
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