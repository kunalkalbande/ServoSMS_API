
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
using RMG;
# endregion

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
	
		/// <summary>
		/// Put user code to initialize the page here
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		# region Page Load...
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
					SqlConnection con;
					SqlCommand cmdselect;
					SqlDataReader dtrdrive;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					cmdselect = new SqlCommand( "Select Route_name  From Route", con );
					dtrdrive = cmdselect.ExecuteReader();
					DropDownList1.Items.Add("Select");
					while (dtrdrive.Read()) 
					{
						DropDownList1.Items.Add(dtrdrive.GetString(0));
					}
					dtrdrive.Close();
					con.Close ();
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
					RouteName="";
					btnDel.Enabled=true;
					btnsave.Enabled=true;
					SqlConnection con44;
					SqlCommand cmdselect44;
					SqlDataReader dtrdrive44;
					con44=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con44.Open ();
					cmdselect44 = new SqlCommand( "Select Route_name,Route_km  From Route where Route_name=@Route_name", con44 );
					cmdselect44.Parameters .Add ("@Route_name",DropDownList1.SelectedItem .Text.ToString());
					dtrdrive44 = cmdselect44.ExecuteReader();
					while (dtrdrive44.Read()) 
					{
						txtrname.Text=dtrdrive44.GetString(0);
						RouteName=dtrdrive44.GetString(0);
						txtrkm.Text =dtrdrive44.GetString(1);
					}
					dtrdrive44.Close();
					con44.Close ();
				
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
				//				if(txtrname.Text=="")
				//				{
				//					MessageBox.Show("Please enter the Route Name");
				//				}
				//				else if(txtrkm.Text=="")
				//				{
				//					MessageBox.Show("Please enter KM");
				//				}
				//				else
				//				{
				
				string sRName=txtrname.Text.ToString().Trim();
				SqlConnection con2;
				SqlCommand cmdselect2;
				SqlDataReader dtredit2;
				con2=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				int iCount=0;
				if(sRName.Trim()!=RouteName.Trim())
				{
					con2.Open ();
					cmdselect2=new SqlCommand("Select Count(Route_name) from Route where Route_name='" + sRName +"'",con2);
					dtredit2=cmdselect2.ExecuteReader();
					if(dtredit2.Read())
					{
						iCount=Convert.ToInt32(dtredit2.GetSqlValue(0).ToString());
					}
					dtredit2.Close();
					con2.Close();
				}
				if(iCount==0)
				{
					Button1.Visible=true;
					btnDel.Enabled=true;
					btnsave.Visible=false;
					btnEdit.Visible=true;
				
					//SqlConnection con2;
					//SqlCommand cmdselect2;
					//SqlDataReader dtredit2;
					string strUpdate;
					//con2=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con2.Open ();
       
					strUpdate = "Update Route set Route_name=@Route_name,Route_km=@Route_km where Route_name=@Route2";
					cmdselect2 = new SqlCommand( strUpdate, con2);
					if(txtrname.Text=="")
						cmdselect2.Parameters .Add ("@Route_name","");
					else
						cmdselect2.Parameters .Add ("@Route_name",txtrname.Text.Trim());
					if(txtrkm .Text=="")
						cmdselect2.Parameters .Add ("@Route_km","");
					else
						cmdselect2.Parameters .Add ("@Route_km",txtrkm.Text.Trim());
					if(DropDownList1.SelectedIndex==0)
						cmdselect2.Parameters .Add ("@Route2","");
					else
						cmdselect2.Parameters .Add ("@Route2",DropDownList1.SelectedItem.Text.ToString());
					dtredit2 = cmdselect2.ExecuteReader();
					MessageBox.Show("Route Updated");
					CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btnsave_Click "+ " Route Name  "+txtrname.Text.Trim().ToString ()+" updated. userid "+ uid );
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
				string sRName=txtrname.Text.ToString().Trim();
				SqlConnection scon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				scon.Open();
				SqlCommand scom=new SqlCommand("Select Count(Route_name) from Route where Route_name='" + sRName +"'",scon);
				SqlDataReader sdtr=scom.ExecuteReader();
				int iCount=0;
				if(sdtr.Read())
				{
					iCount=Convert.ToInt32(sdtr.GetSqlValue(0).ToString());
				}
				sdtr.Close();
				if(iCount==0)
				{
					SqlConnection con4;
					string strInsert4;
					SqlCommand cmdInsert4;
					con4=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con4.Open ();
					strInsert4 = "Insert Route(Route_name,Route_km)values (@Route_name,@Route_km)";
					cmdInsert4=new SqlCommand (strInsert4,con4);
					cmdInsert4.Parameters .Add ("@Route_name",txtrname.Text.Trim().ToString () );
					cmdInsert4.Parameters .Add ("@Route_km",txtrkm.Text.Trim().ToString () );
					cmdInsert4.ExecuteNonQuery();
					con4.Close ();
					MessageBox.Show("Route details Saved");
					CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:Button1_Click "+ " New Route Name  "+txtrname.Text.Trim().ToString ()+" saved   userid "+ uid );
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
		/// This method is used to fatch all the Route Name from database and fill the dropdownlist.
		/// </summary>
		# region Fill Function...
		public void fill()
		{
			try
			{
				SqlConnection con;
				SqlCommand SqlCmd;
				SqlDataReader dtrdrive;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				DropDownList1.Items.Clear();
				SqlCmd = new SqlCommand( "Select route_name  From route", con );
				dtrdrive = SqlCmd.ExecuteReader();
				DropDownList1.Items.Add("Select");
				while (dtrdrive.Read()) 
				{
					DropDownList1.Items.Add(dtrdrive.GetString(0));
				}
				dtrdrive.Close();
				con.Close ();
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
					SqlConnection con10;
					SqlCommand cmdselect10;
					SqlDataReader dtredit10;
					string strdelete10;
					con10=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con10.Open ();

					strdelete10 = "Delete Route where Route_name =@Route_name";
					cmdselect10 = new SqlCommand( strdelete10, con10);
					cmdselect10.Parameters .Add ("@Route_name",txtrname.Text .ToString ());
					dtredit10 = cmdselect10.ExecuteReader();
					MessageBox.Show("Route Deleted");
					CreateLogFiles.ErrorLog("Form:Routeedit.aspx,Method:btn_Delete "+ " Route Name  "+txtrname.Text.Trim().ToString ()+" deleted   userid "+ uid );
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
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string sql;

			#region Fetch the Next Route ID
			sql="select Max(Route_ID)+1 from Route";
			SqlDtr=obj.GetRecordSet(sql);
			if(SqlDtr.Read())
			{
				lblRouteID.Text=SqlDtr.GetValue(0).ToString();
				if(lblRouteID.Text=="")
				{
					lblRouteID.Text="1";
				}
			}
			else
				lblRouteID.Text="1";
			SqlDtr.Close ();		
			#endregion
		}
	}
}