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
using Servosms.Sysitem.Classes ;
using RMG;
using DBOperations; 
using System.Data .SqlClient ;
using System.IO; 
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servosms.Module.Logistics
{
	/// <summary>
	/// Summary description for Vehicle_logbook.
	/// </summary>
	public partial class Vehicle_logbook : System.Web.UI.Page
	{
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
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
				if(!Page.IsPostBack)
				{
					txtDOE.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;        
					getID();
					fillVehicleNo();
					getVehicleInfo();
					fillOilCombo(); 

					# region Dropdown Route Name
					SqlConnection con11;
					SqlCommand cmdselect11;
					SqlDataReader dtrdrive11;
					con11=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con11.Open ();
					cmdselect11 = new SqlCommand( "Select Route_name  From Route", con11 );
					dtrdrive11 = cmdselect11.ExecuteReader();
					Dropvehicleroute.Items.Add("Select");
					while(dtrdrive11.Read())
					{
						Dropvehicleroute.Items.Add(dtrdrive11.GetString(0));
					}
					dtrdrive11.Close();
					con11.Close ();
					# endregion
					btnSave .Enabled = true;
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					checkPrevileges(); 
				}
                else
                {
                   
                    txtVehiclename.Text = Request.Form["txtVehiclename"]; 
                    txtDOE.Text = Request.Form["txtDOE"];
                    txtdrivername.Text = Request.Form["txtdrivername"]; 
                    txtmeterreadpre.Text = Request.Form["txtmeterreadpre"];
                    txtmeterreadcurr.Text = Request.Form["txtmeterreadcurr"];
                    txtfuelused.Text = Request.Form["txtfuelused"];
                    txtengineqty.Text = Request.Form["txtengineqty"];
                    txtGearqty.Text = Request.Form["txtGearqty"];
                    txtGreaseqty.Text = Request.Form["txtGreaseqty"];
                    txtBrakeqty.Text = Request.Form["txtBrakeqty"];
                    txtCoolentqty.Text = Request.Form["txtCoolentqty"];
                    txtTranqty.Text = Request.Form["txtTranqty"];
                    txtTollqty.Text = Request.Form["txtTollqty"];
                    txtPoliceqty.Text = Request.Form["txtPoliceqty"];
                    txtfoodqty.Text = Request.Form["txtfoodqty"];
                    txtMiscqty.Text = Request.Form["txtMiscqty"];
                }
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}

		/// <summary>
		/// This method checks the user privileges from session.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="6";
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
				btnSave.Enabled=false;
			if(Edit_Flag=="0")
				btnEdit.Enabled=false;
			if(Del_Flag=="0")
				btnDelete.Enabled=false;
			#endregion
		}

		/// <summary>
		/// Fills the Oil related combos with Oil names and package from the product table.
		/// </summary>
		public void fillOilCombo()
		{
			try
			{
				SqlDataReader SqlDtr = null;
				//				dbobj.SelectQuery("Select prod_name from products where category = 'Fuel'",ref SqlDtr);
				//				Dropfuelused.Items.Clear();
				//				Dropfuelused.Items.Add("Select");   
				//				while(SqlDtr.Read())
				//				{
				//					Dropfuelused.Items.Add(SqlDtr.GetValue(0).ToString());   
				//				}
				//				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Engine Oil%'",ref SqlDtr);
				Dropengineoil.Items.Clear();
				Dropengineoil.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropengineoil.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();
               
				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Brake Oil%'",ref SqlDtr);
				Dropbrakeoil.Items.Clear();
				Dropbrakeoil.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropbrakeoil.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Gear Oil%'",ref SqlDtr);
				Dropgearoil.Items.Clear();
				Dropgearoil.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Dropgearoil.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category like 'Collents%'",ref SqlDtr);
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

				dbobj.SelectQuery("Select prod_name+':'+pack_type from products where category = 'Transmission Oil'",ref SqlDtr);
				Droptranoil.Items.Clear();
				Droptranoil.Items.Add("Select");   
				while(SqlDtr.Read())
				{
					Droptranoil.Items.Add(SqlDtr.GetValue(0).ToString());   
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:fillCombo() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// Returns the next ID for vehicle Log book.
		/// </summary>
		public void getID()
		{
			try
			{
				SqlDataReader SqlDtr = null;
				int id = 0;
				string strID = "";
				dbobj.SelectQuery("Select max(VDLB_id) from VDLB",ref SqlDtr); 
				if(SqlDtr.Read())
				{
					strID = SqlDtr.GetValue(0).ToString();
					if(!strID.Trim().Equals(""))
					{
						id = System.Convert.ToInt32(strID);
						id = id + 1;
						lblVDLBID.Text = id.ToString(); 
					}
					else
					{
						lblVDLBID.Text = "1001";
					}
				}
				else
				{
					lblVDLBID.Text = "1001";
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:getID() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// Method to fill the vehcile no combo with vehcile ID and no.
		/// </summary>
		public void fillVehicleNo()
		{
			try
			{
				SqlDataReader SqlDtr = null ;
				dbobj.SelectQuery("Select vehicle_no+' VID '+cast(vehicledetail_id as varchar) from vehicleentry",ref SqlDtr);
				while(SqlDtr.Read())
				{
					DropVehicleNo.Items.Add(SqlDtr.GetValue(0).ToString());    
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:fillvehicleNO() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// This method fetch the vehcile related information and put it into the Hidden field for java script.
		/// </summary>
		public void getVehicleInfo()
		{
			try
			{
				string s = "";
				SqlDataReader SqlDtr = null ;
				SqlDataReader SqlDtr1 = null;
				string meter_reading = "";
				dbobj.SelectQuery("select ve.vehicle_no+' VID '+cast(vehicledetail_id as varchar),vehicle_name,meter_reading,vehicledetail_id from vehicleentry ve",ref SqlDtr );
				while(SqlDtr.Read())
				{
					string emp_name = "";
					dbobj.SelectQuery("Select emp_name from employee where vehicle_id = "+SqlDtr.GetValue(3).ToString().Trim()+" and designation = 'Driver'",ref SqlDtr1);
					if(SqlDtr1.HasRows)
					{
						if(SqlDtr1.Read())
							emp_name = SqlDtr1.GetValue(0).ToString();  
					
					}
					SqlDtr1.Close();
				
					meter_reading = SqlDtr.GetValue(2).ToString();
					dbobj.SelectQuery("Select top 1 meter_reading_cur from VDLB where vehicle_no = "+SqlDtr.GetValue(3).ToString().Trim()+" order by DOE desc",ref SqlDtr1);
					if(SqlDtr1.HasRows)
					{
						if(SqlDtr1.Read())
							meter_reading = SqlDtr1.GetValue(0).ToString();  
					
					}
					SqlDtr1.Close();
					s  = s + SqlDtr.GetValue(0).ToString()+"~"+SqlDtr.GetValue(1).ToString()+"~"+emp_name+"~"+meter_reading+"#";        
				}
				SqlDtr.Close();
				txtHidden.Value = s;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:getVehicleInfo() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
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
		/// This method is used to save the vehicle log book information with the help of stored procedure.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				string VDLB_ID  =lblVDLBID.Text;
				string vehicle_no = DropVehicleNo.SelectedItem.Text;
				string strDOE = txtDOE.Text.Trim();
				strDOE = GenUtil.str2MMDDYYYY(strDOE);
				string meter_reading_pre = txtmeterreadpre.Text.Trim();
				string meter_reading_cur = txtmeterreadcurr.Text.Trim();

				if(System.Convert.ToDouble(meter_reading_pre) > System.Convert.ToDouble(meter_reading_cur) )
				{
					MessageBox.Show("Current Meter Reading should not be less than Previous Meter Reading");
					return ;
				}
				if(txtfuelused.Text.Trim().Equals("0") ||  txtfuelused.Text.Trim().Equals("0.0") )
				{
					MessageBox.Show("The Fuel Used quantity should not be 0.");
					return;
				}
				string vehicle_route = Dropvehicleroute.SelectedItem.Text.Trim();
				string Fuel_Used = Dropfuelused.SelectedItem.Text.Trim();
				string Fuel_Used_Qty = txtfuelused.Text.Trim();
				string engine = Dropengineoil.SelectedItem.Text.Trim();
				string Engine_Oil = "";
				string Engine_pack = "";
				string[] strArr = engine.Split(new char[] {':'}, engine.Length);
				if(!engine.Trim().Equals("Select"))
				{ 
					Engine_Oil = strArr[0].Trim();
					Engine_pack = strArr[1].Trim();  

				}				
				string Engine_Oil_Qty = txtengineqty.Text.Trim();
				string Gear = Dropgearoil.SelectedItem.Text.Trim();
				string Gear_Oil = "";
				string Gear_pack = "";
				if(!Gear.Trim().Equals("Select"))
				{ 
					strArr = Gear.Split(new char[] {':'}, Gear.Length);
					Gear_Oil = strArr[0].Trim();
					Gear_pack = strArr[1].Trim();  

				}		
				string Gear_Oil_Qty = txtGearqty.Text.Trim();

				string Grease1 = Dropgrease.SelectedItem.Text.Trim();
				string Grease = "";
				string Grease_pack = "";
				if(!Grease1.Trim().Equals("Select"))
				{ 
					strArr = Grease1.Split(new char[] {':'}, Grease1.Length);
					Grease = strArr[0].Trim();
					Grease_pack = strArr[1].Trim();  

				}	
				string Grease_Qty  = txtGreaseqty.Text.Trim();
				string Brake = Dropbrakeoil.SelectedItem.Text.Trim();
				string Brake_Oil = "";
				string Brake_pack = "";
				if(!Brake.Trim().Equals("Select"))
				{ 
					strArr = Brake.Split(new char[] {':'}, Brake.Length);
					Brake_Oil = strArr[0].Trim();
					Brake_pack = strArr[1].Trim();  

				}	
				string Brake_Oil_Qty = txtBrakeqty.Text.Trim();
				string Trans = Droptranoil.SelectedItem.Text.Trim();
				string Trans_Oil = "";
				string Trans_pack = "";
				if(!Trans.Trim().Equals("Select"))
				{ 
					strArr = Trans.Split(new char[] {':'}, Trans.Length);
					Trans_Oil = strArr[0].Trim();
					Trans_pack = strArr[1].Trim();  

				}	
				string Trans_Oil_Qty = txtTranqty.Text.Trim();             
				string coolent1 = Dropcoolent.SelectedItem.Text.Trim();
				string coolent = "";
				string coolent_pack = "";
				if(!coolent1.Trim().Equals("Select"))
				{ 
					strArr = coolent1.Split(new char[] {':'}, coolent1.Length);
					coolent = strArr[0].Trim();
					coolent_pack = strArr[1].Trim();  

				}	
				string coolent_Qty = txtCoolentqty.Text.Trim();
				string Toll = txtTollqty.Text.Trim();
				string Police = txtPoliceqty.Text.Trim();
				string Food = txtfoodqty.Text.Trim();
				string misc = txtMiscqty.Text.Trim();
				object op = null;
				// calls the procedure proVDLBEntry to insert the vehicle log details
				dbobj.ExecProc(OprType.Insert,"proVDLBEntry",ref op,"@VDLB_ID",VDLB_ID,"@vehicle_no",vehicle_no,"@DOE",strDOE,"@Meter_Reading_Pre",meter_reading_pre,"@Meter_Reading_Cur",meter_reading_cur,"@vehicle_route",vehicle_route,"@Fuel_Used",Fuel_Used,"@Fuel_Used_Qty",Fuel_Used_Qty,"@Engine_Oil",Engine_Oil,"@Engine_pack",Engine_pack,"@Engine_Oil_Qty",Engine_Oil_Qty,"@Gear_Oil",Gear_Oil,"@Gear_pack",Gear_pack,"@Gear_Oil_Qty",Gear_Oil_Qty,"@Grease",Grease,"@Grease_pack",Grease_pack,"@Grease_Qty",Grease_Qty, 
					"@Brake_Oil",Brake_Oil,"@Brake_pack",Brake_pack,"@Brake_Oil_Qty",Brake_Oil_Qty,"@Coolent",coolent,"@Coolent_Pack",coolent_pack,"@Coolent_Qty",coolent_Qty,"@Trans_Oil",Trans_Oil,"@Trans_pack",Trans_pack,"@Trans_Oil_Qty",Trans_Oil_Qty,"@Toll",Toll,"@Police",Police,"@Food",Food,"@Misc",misc);                   
				MessageBox.Show("Vehicle Log Book Saved");
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnSave_Click "+ "vehicle Daily log book for vehicle no."+vehicle_no+" saved.   Userid "+ uid );
				makeReport(); 
				//print(); 
				clear();
				getID();
				getVehicleInfo();    
				btnSave .Enabled = true;
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnSave_Click "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// Clears the whole form.
		/// </summary>
		public void clear()
		{
			DropVehicleNo.SelectedIndex = 0;
			txtVehiclename.Text = "";
			txtDOE.Text = System.DateTime.Now.Day+"/"+System.DateTime.Now.Month+"/"+System.DateTime.Now.Year;
			txtdrivername.Text = "";
			txtmeterreadpre .Text = "";
			txtmeterreadcurr.Text = "";
			Dropvehicleroute.SelectedIndex = 0;
			Dropfuelused.SelectedIndex = 0;
			txtfuelused.Text = "";
			Dropengineoil.SelectedIndex = 0;
			txtengineqty.Text = "";
			Dropgearoil.SelectedIndex = 0;
			txtGearqty.Text = "";
			Dropgrease.SelectedIndex = 0;
			txtGreaseqty.Text = "";
			Dropbrakeoil.SelectedIndex = 0;
			txtBrakeqty.Text = "";
			Dropcoolent.SelectedIndex = 0;
			txtCoolentqty.Text = "";
			Droptranoil.SelectedIndex = 0;
			txtTranqty.Text = "";
			txtTollqty.Text = "";
			txtPoliceqty.Text = "";
			txtfoodqty.Text = "";
			txtMiscqty.Text = "";
		}

		/// <summary>
		/// This method is used to fill the VDLB ID in drpdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit1_Click(object sender, System.EventArgs e)
		{
			try
			{
				clear();
				btnSave .Enabled = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;
				btnEdit1.Visible = false;
				lblVDLBID.Visible = false;
				DropVDLBID.Visible = true;
				checkPrevileges();
				DropVDLBID.Items.Clear();
				DropVDLBID.Items.Add("Select");
				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("Select VDLB_id from vdlb order by VDLB_ID",ref SqlDtr);
				while(SqlDtr.Read())
				{
					DropVDLBID.Items.Add(SqlDtr.GetValue(0).ToString());
 
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnEdit1_click "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// This method fetch the vehcile related information and put it into the Hidden field for java script.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropVDLBID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{			
				if(DropVDLBID.SelectedIndex == 0)
				{
					MessageBox.Show("Please select VDLB ID");
					return ;
				}
				clear();
				string vdlb_id = DropVDLBID.SelectedItem.Text.Trim();    
				SqlDataReader SqlDtr = null;
				SqlDataReader SqlDtr1 = null;
				dbobj.SelectQuery("select v.*,(ve.vehicle_no+' VID '+cast(ve.vehicledetail_id as varchar)) as v_no,ve.vehicle_name,ve.vehicledetail_id from vdlb v,vehicleentry ve where  ve.vehicledetail_id = v.vehicle_no and  vdlb_id ="+vdlb_id,ref SqlDtr);
				if(SqlDtr.Read())
				{
					string emp_name = "";
					dbobj.SelectQuery("Select emp_name from employee where vehicle_id = "+SqlDtr["vehicledetail_id"].ToString().Trim()+" and designation = 'Driver'",ref SqlDtr1);
					if(SqlDtr1.HasRows)
					{
						if(SqlDtr1.Read())
							emp_name = SqlDtr1.GetValue(0).ToString().Trim();     
					
					}
					SqlDtr1.Close();
					string vehicle_no = SqlDtr["v_no"].ToString().Trim();
					DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByText(vehicle_no));
					txtVehiclename.Text = SqlDtr["vehicle_name"].ToString().Trim();
					txtDOE.Text = GenUtil.str2DDMMYYYY(trimDate(SqlDtr["DOE"].ToString().Trim()));
					txtdrivername.Text = emp_name;
					txtmeterreadpre.Text = SqlDtr["meter_reading_pre"].ToString().Trim();
					txtmeterreadcurr.Text = SqlDtr["meter_reading_cur"].ToString().Trim();
					dbobj.SelectQuery("Select route_name From route where route_id ="+SqlDtr["vehicle_route"].ToString().Trim(),ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Dropvehicleroute.SelectedIndex = Dropvehicleroute.Items.IndexOf(Dropvehicleroute.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
				
					//					dbobj.SelectQuery("Select Fuel_used From vehicleentry where prod_id ="+SqlDtr["Fuel_Used"].ToString().Trim()+" and Category = 'Fuel'",ref SqlDtr1);
					//					if(SqlDtr1.Read())
					//					{
					//						Dropfuelused.SelectedIndex = Dropfuelused.Items.IndexOf(Dropfuelused.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					//					}
					//					SqlDtr1.Close();
					Dropfuelused.SelectedIndex  = Dropfuelused.Items.IndexOf( Dropfuelused.Items.FindByText(SqlDtr["Fuel_Used"].ToString().Trim()));
					txtfuelused.Text = SqlDtr["Fuel_Used_Qty"].ToString().Trim();
					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Engine_Oil"].ToString().Trim()+" and Category like 'Engine Oil%'",ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Dropengineoil.SelectedIndex = Dropengineoil.Items.IndexOf(Dropengineoil.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
					txtengineqty.Text = SqlDtr["Engine_Oil_Qty"].ToString().Trim();  

					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Gear_Oil"].ToString().Trim()+" and Category like 'Gear Oil%'",ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Dropgearoil.SelectedIndex = Dropgearoil.Items.IndexOf(Dropgearoil.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
					txtGearqty.Text = SqlDtr["Gear_Oil_Qty"].ToString().Trim();  

					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Grease"].ToString().Trim()+" and Category like 'Grease%'",ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Dropgrease.SelectedIndex = Dropgrease.Items.IndexOf(Dropgrease.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
					txtGreaseqty.Text = SqlDtr["Grease_Qty"].ToString().Trim();  

					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Brake_Oil"].ToString().Trim()+" and Category like 'Brake Oil%'",ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Dropbrakeoil.SelectedIndex = Dropbrakeoil.Items.IndexOf(Dropbrakeoil.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
					txtBrakeqty.Text = SqlDtr["Brake_Oil_Qty"].ToString().Trim();  

//					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Brake_Oil"].ToString().Trim()+" and Category = 'Brake Oil'",ref SqlDtr1);
//					if(SqlDtr1.Read())
//					{
//						Dropbrakeoil.SelectedIndex = Dropbrakeoil.Items.IndexOf(Dropbrakeoil.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
//					}
//					SqlDtr1.Close();
//					txtBrakeqty.Text = SqlDtr["Brake_Oil_Qty"].ToString().Trim();  

					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Coolent"].ToString().Trim()+" and Category like 'Collents%'",ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Dropcoolent.SelectedIndex = Dropcoolent.Items.IndexOf(Dropcoolent.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
					txtCoolentqty.Text = SqlDtr["Coolent_Qty"].ToString().Trim();  

					dbobj.SelectQuery("Select prod_name+':'+pack_type from products where prod_id ="+SqlDtr["Trans_Oil"].ToString().Trim()+" and Category like 'Transmission Oil%'",ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						Droptranoil.SelectedIndex = Droptranoil.Items.IndexOf(Droptranoil.Items.FindByText(SqlDtr1.GetValue(0).ToString().Trim() )); 
					}
					SqlDtr1.Close();
					txtTranqty.Text = SqlDtr["Trans_Oil_Qty"].ToString().Trim();  
                
					txtTollqty.Text = SqlDtr["Toll"].ToString().Trim();
					txtPoliceqty.Text = SqlDtr["Police"].ToString().Trim();
					txtfoodqty.Text = SqlDtr["Food"].ToString().Trim();
					txtMiscqty.Text = SqlDtr["misc"].ToString().Trim(); 
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:DropVDLBID_SelectedIndexChanged "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}
		
		/// <summary>
		/// This is used to trim date from space.
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
		/// This method is used to update the vehicle log book information on edit time with the help of stored procedure.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropVDLBID.SelectedIndex == 0)
				{
					MessageBox.Show("Please select VDLB ID");
					return ;
				}
				string VDLB_ID  =DropVDLBID.SelectedItem.Text.Trim();
				string vehicle_no = DropVehicleNo.SelectedItem.Text;
				string strDOE = txtDOE.Text.Trim();
				strDOE = GenUtil.str2MMDDYYYY(strDOE);
				string meter_reading_pre = txtmeterreadpre.Text.Trim();
				string meter_reading_cur = txtmeterreadcurr.Text.Trim();
				if(System.Convert.ToDouble(meter_reading_pre) > System.Convert.ToDouble(meter_reading_cur) )
				{
					MessageBox.Show("Current Meter Reading should not be less than Previous Meter Reading");
					return ;
				}
				if(txtfuelused.Text.Trim().Equals("0") ||  txtfuelused.Text.Trim().Equals("0.0") )
				{
					MessageBox.Show("The Fuel Used quantity should not be 0.");
					return;
				}

				string vehicle_route = Dropvehicleroute.SelectedItem.Text.Trim();
				string Fuel_Used = Dropfuelused.SelectedItem.Text.Trim();
				string Fuel_Used_Qty = txtfuelused.Text.Trim();
				string engine = Dropengineoil.SelectedItem.Text.Trim();
				string Engine_Oil = "";
				string Engine_pack = "";
				string[] strArr = engine.Split(new char[] {':'}, engine.Length);
				if(!engine.Trim().Equals("Select"))
				{ 
					Engine_Oil = strArr[0].Trim();
					Engine_pack = strArr[1].Trim();  
				}				
				string Engine_Oil_Qty = txtengineqty.Text.Trim();
				string Gear = Dropgearoil.SelectedItem.Text.Trim();
				string Gear_Oil = "";
				string Gear_pack = "";
				if(!Gear.Trim().Equals("Select"))
				{ 
					strArr = Gear.Split(new char[] {':'}, Gear.Length);
					Gear_Oil = strArr[0].Trim();
					Gear_pack = strArr[1].Trim();  
				}		
				string Gear_Oil_Qty = txtGearqty.Text.Trim();
				string Grease1 = Dropgrease.SelectedItem.Text.Trim();
				string Grease = "";
				string Grease_pack = "";
				if(!Grease1.Trim().Equals("Select"))
				{ 
					strArr = Grease1.Split(new char[] {':'}, Grease1.Length);
					Grease = strArr[0].Trim();
					Grease_pack = strArr[1].Trim();  
				}	
				string Grease_Qty  = txtGreaseqty.Text.Trim();
				string Brake = Dropbrakeoil.SelectedItem.Text.Trim();
				string Brake_Oil = "";
				string Brake_pack = "";
				if(!Brake.Trim().Equals("Select"))
				{ 
					strArr = Brake.Split(new char[] {':'}, Brake.Length);
					Brake_Oil = strArr[0].Trim();
					Brake_pack = strArr[1].Trim();  
				}	
				string Brake_Oil_Qty = txtBrakeqty.Text.Trim();
				string Trans = Droptranoil.SelectedItem.Text.Trim();
				string Trans_Oil = "";
				string Trans_pack = "";
				if(!Trans.Trim().Equals("Select"))
				{ 
					strArr = Trans.Split(new char[] {':'}, Trans.Length);
					Trans_Oil = strArr[0].Trim();
					Trans_pack = strArr[1].Trim();  
				}	
				string Trans_Oil_Qty = txtTranqty.Text.Trim();             
				string coolent1 = Dropcoolent.SelectedItem.Text.Trim();
				string coolent = "";
				string coolent_pack = "";
				if(!coolent1.Trim().Equals("Select"))
				{ 
					strArr = coolent1.Split(new char[] {':'}, coolent1.Length);
					coolent = strArr[0].Trim();
					coolent_pack = strArr[1].Trim();  
				}	
				string coolent_Qty = txtCoolentqty.Text.Trim();
				string Toll = txtTollqty.Text.Trim();
				string Police = txtPoliceqty.Text.Trim();
				string Food = txtfoodqty.Text.Trim();
				string misc = txtMiscqty.Text.Trim();
				object op = null;
				dbobj.ExecProc(OprType.Insert,"proVDLBUpdate",ref op,"@VDLB_ID",VDLB_ID,"@vehicle_no",vehicle_no,"@DOE",strDOE,"@Meter_Reading_Pre",meter_reading_pre,"@Meter_Reading_Cur",meter_reading_cur,"@vehicle_route",vehicle_route,"@Fuel_Used",Fuel_Used,"@Fuel_Used_Qty",Fuel_Used_Qty,"@Engine_Oil",Engine_Oil,"@Engine_pack",Engine_pack,"@Engine_Oil_Qty",Engine_Oil_Qty,"@Gear_Oil",Gear_Oil,"@Gear_pack",Gear_pack,"@Gear_Oil_Qty",Gear_Oil_Qty,"@Grease",Grease,"@Grease_pack",Grease_pack,"@Grease_Qty",Grease_Qty, 
					"@Brake_Oil",Brake_Oil,"@Brake_pack",Brake_pack,"@Brake_Oil_Qty",Brake_Oil_Qty,"@Coolent",coolent,"@Coolent_Pack",coolent_pack,"@Coolent_Qty",coolent_Qty,"@Trans_Oil",Trans_Oil,"@Trans_pack",Trans_pack,"@Trans_Oil_Qty",Trans_Oil_Qty,"@Toll",Toll,"@Police",Police,"@Food",Food,"@Misc",misc);                   
				MessageBox.Show("Vehicle Log Book Updated");
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnEdit_Click "+ "vehicle Daily log book for vehicle no."+vehicle_no+" updated.   Userid "+ uid );
				makeReport(); 
				//print(); 
				clear();
				getID();
				getVehicleInfo();    
				btnSave .Enabled = true;
				btnEdit1.Visible = true;
				DropVDLBID.Visible = false;
				lblVDLBID.Visible = true; 
				btnEdit.Enabled = false;
				btnDelete.Enabled = false;
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnEdit_Click "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// Method makes the report txt file to print
		/// </summary>
		public void makeReport()
		{
			try
			{
				string vehicle_no = DropVehicleNo.SelectedItem.Text .Trim();
				vehicle_no = vehicle_no.Substring(0,vehicle_no.IndexOf("VID"));  
				string route = Dropvehicleroute.SelectedItem.Text.Trim();
				if(route.Trim().Equals("Select"))
					route = "";
				string Fuel_used = Dropfuelused.SelectedItem.Text.Trim();
				string fuel_qty = "";
				if(!Fuel_used.Trim().Equals("Select"))
				{
			
					fuel_qty = txtfuelused.Text.Trim();
 
				}
				else
				{
					Fuel_used = "";
				}

				string engine_oil = Dropengineoil.SelectedItem.Text.Trim();
				string engine_qty = "";
				if(!engine_oil.Trim().Equals("Select"))
				{
			
					engine_qty = txtengineqty.Text.Trim();
 
				}
				else
				{
					engine_oil = "";
				}

				string gear_oil = Dropgearoil.SelectedItem.Text.Trim();
				string  gear_qty = "";
				if(!gear_oil.Trim().Equals("Select"))
				{
				
					gear_qty = txtGearqty.Text.Trim();
 
				}
				else
				{
					gear_oil = "";
				}

				string grease = Dropgrease.SelectedItem.Text.Trim();
				string  grease_qty = "";
				if(!grease.Trim().Equals("Select"))
				{
				
					grease_qty = txtGreaseqty.Text.Trim();
 
				}
				else
				{
					grease = "";
				}

				string brake_oil = Dropbrakeoil.SelectedItem.Text.Trim();
				string brake_qty = "";
				if(!brake_oil.Trim().Equals("Select"))
				{
				
					brake_qty = txtBrakeqty.Text.Trim();
 
				}
				else
				{
					brake_oil = "";
				}

				string coolent = Dropcoolent.SelectedItem.Text.Trim();
				string coolent_qty = "";
				if(!coolent.Trim().Equals("Select"))
				{
				
					coolent_qty = txtCoolentqty.Text.Trim();
 
				}
				else
				{
					coolent = "";
				}

				string trans = Dropcoolent.SelectedItem.Text.Trim();
				string trans_qty = "";
				if(!trans.Trim().Equals("Select"))
				{
					trans_qty = txtTranqty.Text.Trim();
 
				}
				else
				{
					trans = "";
				}
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\VehicleDailyLog.txt";
				StreamWriter sw = new StreamWriter(path);
				// Condensed
				sw.Write((char)27);
				sw.Write((char)15);
				//**********
				string des="------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("========================",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("Vehicle Daily Log Book",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("========================",des.Length));
				sw.WriteLine("");
				sw.WriteLine("+-----------------------------------------------+----------------------------------------------------------------------------------+");
				sw.WriteLine("|Vehicle No.        : {0,-26:S}|Vehicle Name          : {1,-58:S}|",vehicle_no,txtVehiclename.Text.Trim());
				sw.WriteLine("|DOE(Date Of Entry) : {0,-26:S}|Driver's Name         : {1,-58:S}|",txtDOE.Text.Trim(),txtdrivername.Text.Trim());
				sw.WriteLine("+-----------------------------------------------+----------------------------------------------------------------------------------+");
				sw.WriteLine("|Meter Reading(Prev): {0,-26:S}|Meter Reading(Current): {1,-58:S}|",txtmeterreadpre.Text.Trim(),txtmeterreadcurr.Text.Trim());
				sw.WriteLine("|Vehicle Route      : {0,-26:S}|Fuel Used             : {1,-20:S} : {2,-35:S}|",route,Fuel_used,fuel_qty);
				sw.WriteLine("+-----------------------------------------------+-------------------------------------------+--------------------------------------+");
				sw.WriteLine("|Engine Oil         : {0,-20:S} : {1,-3:S}|Gear Oil      : {2,-20:S} : {3,-4:S}|Grease    : {4,-20:S} : {5,-3:S}|",engine_oil,engine_qty,gear_oil,gear_qty,grease,grease_qty);
				sw.WriteLine("|Brake Oil          : {0,-20:S} : {1,-3:S}|Coolent       : {2,-20:S} : {3,-4:S}|Trans. Oil: {4,-20:S} : {5,-3:S}|",brake_oil,brake_qty,coolent,coolent_qty,trans,trans_qty );
				sw.WriteLine("+-----------------------------------------------+-------------------------------------------+--------------------------------------+");
				sw.WriteLine("|Other Exp. In Rs.  :-    Toll : {0,-7:s} Police : {1,-7:S} Food : {2,-7:S} Misc : {3,-51:S}|",txtTollqty.Text.Trim() ,txtPoliceqty.Text.Trim() ,txtfoodqty.Text.Trim() ,txtMiscqty.Text.Trim()  );
				sw.WriteLine("+----------------------------------------------------------------------------------------------------------------------------------+");
				// deselect Condensed
				//	sw.Write((char)18);
				//sw.Write((char)12);
				
				sw.Close();
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:makingReport() "+ "vehicle Daily log book for vehicle no."+vehicle_no+" printed.   Userid "+ uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:makingReport() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}			
		}

		/// <summary>
		/// This is used to print the file.txt & connect printserver.
		/// </summary>
		public void print()
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
				// Establish the remote endpoint for the socket.
				// The name of the
				// remote device is "host.contoso.com".
				IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress,62000);

				// Create a TCP/IP  socket.
				Socket sender1 = new Socket(AddressFamily.InterNetwork, 
					SocketType.Stream, ProtocolType.Tcp );
				CreateLogFiles.ErrorLog("Form:vehicle_logbook.aspx,Method:print"+uid);
				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\VehicleDailyLog.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:print() "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// This method is used to delete the particular vehicle log book record, who select from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropVDLBID.SelectedIndex == 0)
				{
					MessageBox.Show("Please select VDLB ID");
					return ;
				}

				string vdlb_id = DropVDLBID.SelectedItem.Text.Trim();
				int c = 0;
				dbobj.Insert_or_Update("Delete from vdlb where vdlb_id = "+vdlb_id,ref c);
				if(c > 0)
				{
					MessageBox.Show("Vehicle Log Book Deleted");
					CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnDelete_Click "+ "vehicle Daily log book for vehicle no."+DropVehicleNo.SelectedItem.Text+" deleted.   Userid "+ uid );
					clear();
					getID();
					getVehicleInfo();    
					btnSave .Enabled = true;
					btnEdit1.Visible = true;
					DropVDLBID.Visible = false;
					lblVDLBID.Visible = true; 
					btnEdit.Enabled = false;
					btnDelete.Enabled = false;
					checkPrevileges();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vehicle_logbook.aspx,Method:btnDelete_Click "+ " EXCEPTION  "+ex.Message+"   userid "+ uid );
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			print();
		}
	}
}