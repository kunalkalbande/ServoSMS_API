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
using RMG;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using System.Data.SqlClient;
using System.Text;

namespace Servosms.Module.Inventory
{
    /// <summary>
    /// Summary description for Price_Updation.
    /// </summary>
    public partial class Price_Updation : System.Web.UI.Page
    {
      public  StringBuilder strHsnCodes = new StringBuilder();
        /// <summary>
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            try
            {
                string pass;
                pass = (Session["User_Name"].ToString());
            }
            catch
            {
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }
            if (!IsPostBack)
            {
                DBOperations.DBUtil dbobj = new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
                #region Check Privileges
                int i;
                string View_flag = "0", Add_Flag = "0", Edit_Flag = "0", Del_Flag = "0";
                string Module = "4";
                string SubModule = "2";
                string[,] Priv = (string[,])Session["Privileges"];
                for (i = 0; i < Priv.GetLength(0); i++)
                {
                    if (Priv[i, 0] == Module && Priv[i, 1] == SubModule)
                    {
                        View_flag = Priv[i, 2];
                        Add_Flag = Priv[i, 3];
                        Edit_Flag = Priv[i, 4];
                        Del_Flag = Priv[i, 5];
                        break;
                    }
                }
                if (Add_Flag == "0" && Edit_Flag == "0" && View_flag == "0")
                {
                    //string msg="UnAthourized Visit to Price Updation Page";
                    //	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
                    Response.Redirect("../../Sysitem/AccessDeny.aspx", false);
                }
                //if(Add_Flag =="0" && Edit_Flag == "0")
                //	Btnsubmit1.Enabled = false; 
                #endregion
            }
            getdbHsnValues();


        }

        public void getdbHsnValues()
        {

          
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
