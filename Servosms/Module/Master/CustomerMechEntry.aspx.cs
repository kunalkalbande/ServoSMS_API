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
using Servosms.Classes;
using RMG;
namespace Servosms.Forms.Parties
{
	/// <summary>
	/// Summary description for CustomerMechEntry.
	/// </summary>
	public class CustomerMechEntry : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.DropDownList DropID;
		protected System.Web.UI.WebControls.DropDownList DropCustomerName;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Button btnselect;
		protected System.Web.UI.WebControls.TextBox txtname1;
		protected System.Web.UI.WebControls.DropDownList Droptype1;
		protected System.Web.UI.WebControls.DropDownList Dropcity1;
		protected System.Web.UI.WebControls.TextBox txtname2;
		protected System.Web.UI.WebControls.DropDownList Droptype2;
		protected System.Web.UI.WebControls.DropDownList Dropcity2;
		protected System.Web.UI.WebControls.TextBox txtname3;
		protected System.Web.UI.WebControls.DropDownList Droptype3;
		protected System.Web.UI.WebControls.DropDownList Dropcity3;
		protected System.Web.UI.WebControls.TextBox txtname4;
		protected System.Web.UI.WebControls.DropDownList Droptype4;
		protected System.Web.UI.WebControls.DropDownList Dropcity4;
		protected System.Web.UI.WebControls.TextBox txtname5;
		protected System.Web.UI.WebControls.DropDownList Droptype5;
		protected System.Web.UI.WebControls.DropDownList Dropcity5;
		protected System.Web.UI.WebControls.TextBox txtname6;
		protected System.Web.UI.WebControls.DropDownList Droptype6;
		protected System.Web.UI.WebControls.DropDownList Dropcity6;
		protected System.Web.UI.WebControls.TextBox txtname7;
		protected System.Web.UI.WebControls.DropDownList Droptype7;
		protected System.Web.UI.WebControls.DropDownList Dropcity7;
		protected System.Web.UI.WebControls.TextBox txtname8;
		protected System.Web.UI.WebControls.DropDownList Droptype8;
		protected System.Web.UI.WebControls.DropDownList Dropcity8;
		protected System.Web.UI.WebControls.TextBox txtname9;
		protected System.Web.UI.WebControls.DropDownList Droptype9;
		protected System.Web.UI.WebControls.DropDownList Dropcity9;
		protected System.Web.UI.WebControls.TextBox txtname10;
		protected System.Web.UI.WebControls.DropDownList Droptype10;
		protected System.Web.UI.WebControls.DropDownList Dropcity10;
		protected System.Web.UI.WebControls.Button btnadd;
		protected System.Web.UI.WebControls.Button btnedit;
		protected System.Web.UI.WebControls.TextBox txtcustid;
	DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				fillID();
			}
		}

		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
//			//
//			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			///this.btnselect.Click += new System.EventHandler(this.btnselect_Click);
			//this.Droptype2.SelectedIndexChanged += new System.EventHandler(this.Droptype2_SelectedIndexChanged);
			//this.Dropcity3.SelectedIndexChanged += new System.EventHandler(this.Dropcity3_SelectedIndexChanged);
			//this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//This is used to generate ID auto.
		private void fillID()
		{
			SqlConnection con;
			SqlCommand cmd;

			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				
				cmd=new SqlCommand("select max(customermechid)+1 from Customermechanicentry",con);
				SqlDtr=cmd.ExecuteReader();
				
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						txtcustid.Text=SqlDtr.GetValue(0).ToString ();
						if(txtcustid.Text.Trim().Equals(""))
							txtcustid.Text="1";
					}
				}
				
				
				SqlDtr.Close (); 
				con.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		
		}
	}

