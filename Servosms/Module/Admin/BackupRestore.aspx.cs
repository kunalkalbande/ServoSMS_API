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
using System.IO;
using System.Text;
using Servosms.Sysitem.Classes;
using DBOperations;
using RMG;
using System.Data.SqlClient;
//using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Servosms.Module.Admin
{
	/// <summary>
	/// Summary description for Backup_Restore.
	/// </summary>
	public partial class BackupRestore : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputFile ff1;
		string uid= "";
        string BaseUri = "http://localhost:64862";
        //static int Flag=0;

        /// <summary>
        /// This method is used for setting the Session variable for userId
        /// and also check accessing priviledges for particular user.
        /// </summary>
        protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{ 
				uid=(Session["User_Name"].ToString ());
				btnRestore.Attributes.Add("OnClick","Progressbar();");
				btnBackup.Attributes.Add("OnClick","Progressbar();");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:Page_load   EXCEPTION:  "+ex.Message+" userid  "+uid  );
				Response.Redirect("ServoSMS/System/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				btnRestore.Enabled=false;
				#region Check Privileges if user id admin then grant the access
				if(Session["User_ID"].ToString ()!="1001")
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				#endregion
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
		/// This method is used to take backup in .bak format and stored in C:\ePetroBackup\Son location by sql query.
		/// </summary>
		protected void btnBackup_Click(object sender, System.EventArgs e)
		{
			try
			{
				//SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				string main_drive = Environment.SystemDirectory;
				string drive = main_drive.Substring(0,2);
				string strGrandFather  = drive+"\\ServosmsBackup\\GrandFather\\";
				string strFather       = drive+"\\ServosmsBackup\\Father\\";
				string strSon          = drive+"\\ServosmsBackup\\Son\\";
				string strDataBase = "Servosms.bak";
				bool blnGrandFather=false, blnFather=false, blnSon=false;
				int Count=0;
				Directory.CreateDirectory(strGrandFather);
				Directory.CreateDirectory(strFather);
				Directory.CreateDirectory(strSon);
				if (File.Exists(strGrandFather + strDataBase)) 
					blnGrandFather = true;
				if (File.Exists(strFather + strDataBase)) 
					blnFather = true;
				if (File.Exists(strSon + strDataBase)) 
					blnSon = true;

				// Start Backing...

				if (blnGrandFather == true && blnFather == true && blnSon == true)
				{
					// Father ---> GrandFather
					File.Copy(strFather + strDataBase, strGrandFather + strDataBase, true);
					//File.Copy(strFather + strDBLog, strGrandFather + strDBLog, true);

					// Son ---> Father
					File.Copy(strSon + strDataBase, strFather + strDataBase, true);
					//File.Copy(strSon + strDBLog, strFather + strDBLog, true);

					// MS-SQL ---> Son
					//File.Copy(strDataBasePath + strDataBase, strSon + strDataBase, true);
					//File.Copy(strDataBasePath + strDBLog, strSon + strDBLog, true);
				}
				else
				{
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/BackupRestoreController/Backup").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                        }
                    }
                    //con.Open();
					////SqlCommand cmd = new SqlCommand("BACKUP DATABASE [Servosms] TO  DISK = N'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH NOFORMAT, NOINIT,  NAME = N'Servosms-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10",con);
					//SqlCommand cmd = new SqlCommand("BACKUP DATABASE [Servosms] TO  DISK = N'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH NOFORMAT, INIT,  NAME = N'Servosms-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10",con);
					//cmd.ExecuteNonQuery();
					//cmd.Dispose();
					//con.Close();
					//System.Threading.Thread.Sleep(1000 * 5);
					System.Threading.Thread.Sleep(1000 * 10);
					Count=1;
					//MS-SQL ---> GrandFather
					//File.Copy(strDataBasePath + strDataBase, strGrandFather + strDataBase, true);
					File.Copy(strSon + strDataBase, strGrandFather + strDataBase, true);
					//File.Copy(strDataBasePath + strDBLog, strGrandFather + strDBLog, true);
	       
					//MS-SQL ---> Father
					//File.Copy(strDataBasePath + strDataBase, strFather + strDataBase, true);
					File.Copy(strSon + strDataBase, strFather + strDataBase, true);
					//File.Copy(strDataBasePath + strDBLog, strFather + strDBLog, true);
	       
					//MS-SQL ---> Son
					//File.Copy(strDataBasePath + strDataBase, strSon + strDataBase, true);
					//File.Copy(strSon + strDataBase, strSon + strDataBase, true);
					//File.Copy(strDataBasePath + strDBLog, strSon + strDBLog, true);
				}
				//************************
				if(Count==0)
				{
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/BackupRestoreController/BackupDB").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                        }
                    }
                    // con.Open();
					////SqlCommand cmd = new SqlCommand("BACKUP DATABASE [Servosms] TO  DISK = N'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH NOFORMAT, NOINIT,  NAME = N'Servosms-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10",con);
					//SqlCommand cmd = new SqlCommand("BACKUP DATABASE [Servosms] TO  DISK = N'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH NOFORMAT, INIT,  NAME = N'Servosms-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10",con);
					//cmd.CommandTimeout=1000;
					//cmd.ExecuteNonQuery();
					//cmd.Dispose();
					//con.Close();
					System.Threading.Thread.Sleep(1000 * 5);
				}
				MessageBox.Show("Backup Complete");
				//cmd.Dispose();
				//con.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:btnBackup_Click   EXCEPTION:  "+ex.Message+" userid  "+uid  );  
				MessageBox.Show(ex.Message);
			}
		}


		/// <summary>
		/// This method is not used.
		/// </summary>
		public  string contactServer1(string key)
		{
			// Data buffer for incoming data.
			byte[] bytes = new byte[3072];
			string strID = "";
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
				Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender.Connect(remoteEP);

					// Encode the data string into a byte array.
				
					//****byte[] msg = Encoding.ASCII.GetBytes(key + "<EOF>");
					byte[] msg = Encoding.ASCII.GetBytes(key + "<EOF> "+ff1.Value);

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender.Receive(bytes);
					//Console.WriteLine("\neRetailPrintServices Server Echo = {0}", Encoding.ASCII.GetString(bytes,0,bytesRec));
					strID = Encoding.ASCII.GetString(bytes,0,bytesRec);
					// Release the socket.
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
					return strID;
				}
				catch (ArgumentNullException ane)
				{
					string str = ane.Message; // To avoid Warnings
					CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+ane.Message+" userid  "+uid  );    
					
				}
				catch (SocketException se)
				{
					string str = se.Message; // To avoid Warnings
					
					CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+se.Message+" userid  "+uid  );    
					//Response.Redirect(".\\Service.aspx",false);
				
				} 
				catch (Exception e)
				{
					string str = e.Message; // To avoid Warnings
					
					CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+e.Message+" userid  "+uid  );      
					//Response.Redirect(".\\Service.aspx",false);
				}
			} 
			catch (Exception e)
			{
				string str = e.Message; // To avoid Warnings
				CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+e.Message+" userid  "+uid  );    
			}
			return "";
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		public  string contactServer(string key)
		{
			// Data buffer for incoming data.
			byte[] bytes = new byte[3072];
			string strID = "";
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
				Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender.Connect(remoteEP);

					// Encode the data string into a byte array.
				
					//****byte[] msg = Encoding.ASCII.GetBytes(key + "<EOF>");
					byte[] msg = Encoding.ASCII.GetBytes(key + "<EOF>");

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender.Receive(bytes);
					//Console.WriteLine("\neRetailPrintServices Server Echo = {0}", Encoding.ASCII.GetString(bytes,0,bytesRec));
					strID = Encoding.ASCII.GetString(bytes,0,bytesRec);
					// Release the socket.
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
					return strID;
				}
				catch (ArgumentNullException ane)
				{
					string str = ane.Message; // To avoid Warnings
					CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+ane.Message+" userid  "+uid  );    
				}
				catch (SocketException se)
				{
					string str = se.Message; // To avoid Warnings
					CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+se.Message+" userid  "+uid  );    
					//Response.Redirect(".\\Service.aspx",false);
				} 
				catch (Exception e)
				{
					string str = e.Message; // To avoid Warnings
					CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+e.Message+" userid  "+uid  );      
					//Response.Redirect(".\\Service.aspx",false);
				}
			} 
			catch (Exception e)
			{
				string str = e.Message; // To avoid Warnings
				CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:contactServer()   EXCEPTION:  "+e.Message+" userid  "+uid  );    
			}
			return "";
		}

		/// <summary>
		/// This method is used to restore the backup data in database by sql query and take backup file
		/// from custom location.
		/// </summary>
		protected void btnRestore_Click(object sender, System.EventArgs e)
		{
			try
			{
				//**System.Threading.Thread.Sleep(20 * 1000); 
				//******************
				//******************
				
				/****************************
				if(ff1.Value!="")
				{
					string msg = "";
					msg = contactServer1("[RS]");
					if(msg.Trim().Equals("Restored")) 
					{
						MessageBox.Show("Data Restored."); 
					}
				}
				else
				{
					MessageBox.Show("Please Select The '.mdf' File");
				}
				*******************************/
				if(tempPath.Value!="")
				{
					//lblPro.Visible=true;
					string FilePath=tempPath.Value;
					FilePath+=".bak";

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/BackupRestoreController/Restore?FilePath="+ FilePath).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                        }
                    }

                    //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Master"]);
					//con.Open();
					//SqlCommand cmd = new SqlCommand("Alter DATABASE Servosms SET SINGLE_USER WITH ROLLBACK IMMEDIATE",con);
					//cmd.ExecuteNonQuery();
					//cmd.Dispose();
					//con.Close();
					//con.Open();
					//cmd = new SqlCommand("RESTORE DATABASE [Servosms] FROM  DISK = '"+FilePath+"' WITH  FILE = 1,REPLACE",con);
					////cmd = new SqlCommand("RESTORE DATABASE [Servosms] FROM  DISK = 'C:\\ServosmsBackup\\Son\\Servosms.bak' WITH  FILE = 1,REPLACE",con);
					////cmd = new SqlCommand("RESTORE DATABASE Servosms FROM DISK = 'c:\\Servosms.bak' WITH FILE = 1, RECOVERY, REPLACE;",con);
					//cmd.CommandTimeout=1000;
					//cmd.ExecuteNonQuery();
					//cmd.Dispose();
					//con.Close();
					//con.Open();
					//cmd = new SqlCommand("Alter DATABASE Servosms SET MULTI_USER",con);
					//cmd.ExecuteNonQuery();
					//btnRestore.Attributes.Add("OnClick","Progressbar();");
					
					System.Threading.Thread.Sleep(1000 * 30);
					//lblPro.Visible=false;
					//btnRestore.Attributes.Add("OnClick","Progressbar();");
					MessageBox.Show("Restore Complete");
					//cmd.Dispose();
					//con.Close();
					//Flag=1;
					//					DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
					//					object op=null;
					//					dbobj.ExecProc(OprType.Insert,"proRestore",ref op,"@ID","");
				}
				else
				{
					//MessageBox.Show("Please Select The '.mdf' File");
					MessageBox.Show("Please Select The '.bak' File");
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BackupRestore.aspx,Method:btnRestore_Click   EXCEPTION:  "+ex.Message+" userid  "+uid  );  
				MessageBox.Show(ex.Message);
			}
		}
	}
}