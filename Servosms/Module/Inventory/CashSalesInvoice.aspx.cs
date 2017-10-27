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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using DBOperations;
using System.Data.SqlClient;  

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for CashSalesInvoice.
	/// </summary>
	public partial class CashSalesInvoice : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);	
	
		string uid="";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
		
				//uid=(Session["User_Name"].ToString());
				
				
				//GetProducts();
				//FetchData();
				
			}
			catch(Exception ex)
			{				
				CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:pageload"+ ex.Message+"  EXCEPTION "+"   "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				try
				{
				//	checkPrevileges();
					lblInvoiceDate.Text=GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());  
					lblEntryTime.Text=DateTime.Now.ToString ();
					//lblEntryBy.Text =Session["User_Name"].ToString();
					
					InventoryClass  obj=new InventoryClass ();
//					SqlDataReader SqlDtr;
//					string sql;

					//GetNextInvoiceNo();
				
//					#region Fetch the Product Types and fill in the ComboBoxes
//					sql="select distinct Category from Products";
//					for(int j=0;j<ProductType.Length;j++)
//					{
//						SqlDtr = obj.GetRecordSet(sql); 
//						while(SqlDtr.Read())
//						{				
//							ProductType[j].Items.Add(SqlDtr.GetValue(0).ToString());  
//						}					
//						SqlDtr.Close();
//					}
//					#endregion

//					#region Fetch All Customer ID and fill in the ComboBox
//					sql="select Cust_Name from Customer order by Cust_Name";
//					SqlDtr=obj.GetRecordSet(sql);
//					while(SqlDtr.Read())
//					{
//						DropCustName.Items.Add (SqlDtr.GetValue(0).ToString ());				
//					}
//					SqlDtr.Close ();		
//					#endregion

					//FetchData();

                 

		

					/////////////////////////////////////////////////////////
				
					
					//GetProducts();
					//FetchData();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:pageload.   EXCEPTION: "+ ex.Message+"  User_ID: "+uid);   
				}
				
				//////////////////////////////////////////////////////////
				///

				// This block of code first time on page load checks the pre print template file available or not according to it displays the warning message, and disables the pre print button.
				try
				{
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					string path = home_drive+@"\Inetpub\wwwroot\Servosms\PrePrintTemplate.INI";
					StreamReader  sr = new StreamReader(path);
					Button1.Enabled = true; 
					sr.Close();
				}
				catch(System.IO.FileNotFoundException)
				{

					MessageBox.Show("If you want to use Pre Print service then you have to execute PrintWizard\nto generate the Pre Print Template.");
					Button1.Enabled = false; 
					
				}
			
			}
		}
        //generet invoice no automatic
		public void GetNextInvoiceNo()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
	
			#region Fetch the Next Invoice Number
			sql="select max(Invoice_No)+1 from Sales_Master";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				lblInvoiceNo.Text =SqlDtr.GetValue(0).ToString ();				
				if(lblInvoiceNo.Text=="")
					lblInvoiceNo.Text ="1001";
			}
			SqlDtr.Close ();		
			#endregion
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

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
