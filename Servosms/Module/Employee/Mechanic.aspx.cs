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
using System.Data .SqlClient ;
using Servosms.Sysitem.Classes ;
using RMG;


namespace Servosms.Module.Employee
{
	/// <summary>
	/// Summary description for Mechanic.
	/// </summary>
	public partial class Mechanic : System.Web.UI.Page
	{
		string uid="";
		string sql="";
		SqlDataReader SqlDtr;

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
			if (!IsPostBack)
			{
				EmployeeClass obj=new EmployeeClass();
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

			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Mechanic.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}

		/// <summary>
		/// This is used to fill state country according to select city from dropdownlist with the 
		/// help of java script.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InventoryClass  obj=new InventoryClass ();
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
				//CreateLogFiles.ErrorLog("Form:Mechanic.aspx,Method:DropCity_SelectedIndexChanged "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Mechanic.aspx,Method:DropCity_SelectedIndexChanged ,"+" state and country is select for city :"+  DropCity.SelectedItem.Value+  "  EXCEPTION" +ex.Message+" userid "+uid);
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

		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}