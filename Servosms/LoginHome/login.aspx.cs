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
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using DBOperations;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using RMG;
using System.Web.SessionState;

using System.Runtime.InteropServices;
using System.Management;

using System.Net;
using System.Net.Sockets;
using MySecurity;

namespace Servosms.Module.LoginHome
{
	/// <summary>
	/// Summary description for login.
	/// </summary>
	public partial class login : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				string check = "";
				Cal_Img1.Visible = false; 

				// check string gets the value from the check() method present in Security.dll;
				check = MySecurity.MySecurity.check(); 
				// If the return value is false then the activation period expired and redirect to the error.aspx
				if(check.Equals("false" ))
				{
					Response.Redirect("..\\Sysitem\\error.aspx",false);
					return;
				}
				// If the return value is Service then the Print_WindowsService is stopped and redirect to the Service.aspx
				if(check.Equals("Service"))
				{
					Response.Redirect("..\\Sysitem\\Service.aspx",false);
					return;
				}

				// If the return value is starts with P then dispaly the activation period. 
				if(!check.Equals(""))
				{
					if(!check.Equals("true") && check.StartsWith("P") )
					{
						lblMessage.Text = check.Substring(1)+" left for Activation";
						Cal_Img.Disabled = true;
						Cal_Img.Visible = false;
						Cal_Img1.Visible = true; 
						Cal_Img1.Disabled = true; 
						
					}
				}
				Session.Clear();
				if(!IsPostBack)
				{
					PetrolPumpClass obj=new PetrolPumpClass();
					SqlDataReader SqlDtr;
					string sql;
					// Fetch the roles and fills the User Type combo.
					sql="select Role_Name from Roles";
					SqlDtr=obj.GetRecordSet(sql);

					while(SqlDtr.Read())
						DropUser.Items.Add(SqlDtr.GetValue(0).ToString()); 				
					SqlDtr.Close();
					txtSetDate.Text = DateTime.Now.Day.ToString()+"/"+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString();   
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				MessageBox.Show(ex.StackTrace);
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
			this.btnSign.ServerClick += new System.Web.UI.ImageClickEventHandler(this.btnSign_ServerClick);

		}
		#endregion

		/// <summary>
		/// This method is used to check the user is valid or not after that check permission of that user from database.
		/// </summary>
		private void btnSign_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			CreateLogFiles.ErrorLog("Form:Login.aspx,Method: btnSign_Click, Login Type "+ DropUser.SelectedItem.Text    +" and  Login User   "+ txtUserLogin.Text );
			PetrolPumpClass obj=new PetrolPumpClass();
			try
			{
				SqlDataReader SqlDtr; 
				string sql;
				string User_ID="";
				string[,] Privileges=new string[98,6];
				/****add-bhal****/ Session["CurrentDate"]=txtSetDate.Text.ToString();

				#region Check for Valid User
				string pwd="";
				string epassword="";
				sql="select Password from User_Master where LoginName='"+ txtUserLogin.Text  +"'"; 
				SqlDtr =obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{ 
					pwd =MySecurity.MySecurity.Decrypt(SqlDtr.GetValue(0).ToString(),"!@#$%^");
				
					if(txtPasswd.Text == pwd)
					{
						epassword = SqlDtr.GetValue(0).ToString();
						SqlDtr.Close();
					}
					else
					{
						RMG.MessageBox.Show("Invalid User Login Name or Password");			
						return;
					}
				}
				else
				{
					RMG.MessageBox.Show("Invalid User Login Name or Password");			
					return;
				}
				SqlDtr.Close();

				// Calls the method contactServer by passing the selected date to set the system date as a selected date.
				string ss=MySecurity.MySecurity.contactServer("[CD]"+convertDate(txtSetDate.Text));   
				//	contactServer("[CD]"+convertDate(TxtDateFrom.Text));   
				#region get the message from Organisation table and put into session to display in all the invoices
				dbobj.SelectQuery("Select Message from organisation where CompanyID = 1001",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Session["Message"] = SqlDtr.GetValue(0).ToString();  
				}
				else
				{
					Session["Message"] = "";
				}
				SqlDtr.Close();
				#endregion

				#region get the VAT_Rate from Organisation table and put into session to access in Sales and Purchase Invoice.
				dbobj.SelectQuery("Select VAT_Rate from organisation where CompanyID = 1001",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Session["VAT_Rate"] = SqlDtr.GetValue(0).ToString();  
				}
				else
				{
					Session["VAT_Rate"] = "";
				}
				SqlDtr.Close();
				#endregion

				#region get the EntryTax from Organisation table and put into session to access in Sales and Purchase Invoice.
				dbobj.SelectQuery("Select Entrytax from organisation where CompanyID = 1001",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Session["Entrytax"] = SqlDtr.GetValue(0).ToString();  
				}
				else
				{
					Session["Entrytax"] = "";
				}
				SqlDtr.Close();
				#endregion		


				#region select the user id ,password compare and stored in a session variable.	
				sql="select UserID, LoginName,password,Role_Name from User_Master um, Roles r where um.role_ID=r.role_ID and um.LoginName='"+ txtUserLogin.Text  +"' and password='"+epassword+"' and r.Role_ID=(select Role_ID from Roles where Role_Name='"+ DropUser.SelectedItem.Value  +"')";
			
				SqlDtr=obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{
			
					User_ID=SqlDtr.GetValue(0).ToString();
					Session["User_ID"] = User_ID;
					//string sss=SqlDtr.GetValue(1).ToString();
					Session["User_Name"]=(SqlDtr.GetValue(1).ToString());
					//string sss1=(Session["User_Name"].ToString());
					Cache["User_Name"]=(SqlDtr.GetValue(1).ToString());
					Session["PASSWORD"]=SqlDtr.GetValue(2).ToString();
					Session["User_Type"]=SqlDtr.GetValue(3).ToString();
					SqlDtr.Close();
				}

				else
				{
					RMG.MessageBox.Show("Invalid User Login Name or Password");			
					return;
				}
				SqlDtr.Close();
				#endregion
				#endregion

				if(User_ID!="")
				{
					#region Get The User Permission
					sql="select * from Privileges where User_ID='"+ User_ID +"'";
					SqlDtr=obj.GetRecordSet(sql);
					for(int i=0;SqlDtr.Read();i++)
					{
						for(int j=0;j<6;j++)	
						{
							Privileges[i,j]=SqlDtr.GetValue(j+1).ToString(); 
						}
					}
					SqlDtr.Close();
					//Session["Privileges"]=Privileges;
					Cache["Privileges"]=Privileges;
					#endregion
					Response.Redirect("HomePage.aspx",false);
				}
				else
				{
					RMG.MessageBox.Show("Invalid User Login Name or Password");			
					return;
				}
				txtUserLogin.Enabled=true;
				txtPasswd.Enabled=true;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Login.aspx,Method: btnSign_Click, Login Type "+ DropUser.SelectedItem.Text    +"  EXCEPTION   "+ex.ToString()+"  and  Login User   "+ txtUserLogin.Text );
			}
		}

		/// <summary>
		/// This method is used to split the given date and return in MM/dd/yyyy format.
		/// </summary>
		public string convertDate(string strDate)
		{
			string[] strArr = strDate.IndexOf("/")>0?strDate.Split(new char[] {'/'} , strDate.Length): strDate.Split(new char[] { '-' }, strDate.Length);
			return strArr[1]+"-"+strArr[0]+"-"+strArr[2];
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private bool IsExpired()
		{
			int x=0;
			bool Status=false;

			dbobj.ExecuteScalar("select count(*) from SysTable",ref x);
			if(x<=0)
			{
				dbobj.Insert_or_Update("insert into SysTable(SysDate1,SysDate2,allow) values(getdate(),dateadd(dd,30,getdate()),'f')",ref x);
			}
			else
			{
				dbobj.Insert_or_Update("if((select SysDate2 from SysTable)<(select getdate())) update SysTable set allow='n'",ref x);
			}
			System.Data.SqlClient.SqlDataReader rdr=null;
			dbobj.SelectQuery("select allow from SysTable",ref rdr);
			if(rdr.Read())
			{
				if(rdr["allow"].Equals("n"))
					Status=true;
			}
			return Status;
		}

		private void DropUser_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

        protected void txtSetDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}