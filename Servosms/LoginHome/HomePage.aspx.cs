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
using Servosms.Sysitem.Classes;
using RMG;

namespace Servosms.Module.LoginHome
{
	/// <summary>
	/// Summary description for HomePage.
	/// </summary>
	public partial class HomePage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.HyperLink HyperLink49;
		protected System.Web.UI.WebControls.HyperLink Hyperlink111187;
		protected System.Web.UI.WebControls.HyperLink Hyperlink114;
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{   
			try
			{
				uid=Cache["User_Name"].ToString();
				Session["User_Name"]=Cache["User_Name"].ToString();
				Session["Privileges"]=Cache["Privileges"];
				CreateLogFiles.ErrorLog("Form:Homepage.aspx,Method:Page_Load,  userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Homepage.aspx,Method:Page_Load,   EXCEPTION "+ex.Message+"  userid  "+uid );
				Response.Redirect("Sysitem/ErrorPage.aspx",false);
				return;
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
	
	}
}
