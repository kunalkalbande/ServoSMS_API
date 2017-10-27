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
using Servosms.Sysitem.Classes;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using RMG;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for TaxReport.
	/// </summary>
	public partial class TaxReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
          
		string uid;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TaxReport.aspx,Class:PetrolPumpClass.cs ,Method:Pageload   EXCEPTION: "+ex.Message+".  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="18";
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
		/// This method is used to view the report with the help of datagrid.
		/// </summary>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			try
			{
				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("select p.prod_name, cast(Reduction as varchar)+' '+Unit_rdc as Reduction,cast(entry_tax as varchar)+' '+unit_etax as Entry_Tax, cast(rpg_charge as varchar)+' '+Unit_rpgchg as rpg_charge,cast(rpg_surcharge as varchar)+' '+Unit_rpgschg as rpg_surcharge,cast(LT_charge as varchar)+' '+Unit_ltchg as LT_Charge,cast(tran_charge as varchar)+' '+Unit_tchg as trans_charge,cast(Other_Lvy as varchar)+' '+Unit_olvy as Other_Lvy,cast(LST as varchar)+' '+Unit_LST as LST, cast(LST_Surcharge as varchar)+' '+Unit_lstschg as LST_Surcharge,cast(LF_Recov as varchar)+' '+Unit_lfrecov as LF_Recov, cast(dofobc_Charge as varchar)+' '+Unit_dochg as dofobc_Charge  from tax_entry t, Products p where p.Prod_ID =  t.ProductID ",ref SqlDtr); 
				GridTaxReport.DataSource=SqlDtr;
				GridTaxReport.DataBind();
				if(GridTaxReport.Items.Count == 0 )
				{
					GridTaxReport.Visible = false;
					MessageBox.Show("Data not available");
					return;
				}
				else
				{
					GridTaxReport.Visible = true;                  
				} 
				SqlDtr.Close(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TaxReport.aspx,Method:btnView_Click()   EXCEPTION: "+ex.Message+".  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the report file TaxReport.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				/*
												  ==========
												  Tax Report
												  ==========

+------------+---------+-------+-------+---------+---------+---------+--------+-------+---------+--------+--------+
|  Product   |Reduction| Entry |  RPG  |   RPG   |  Local  |Transport| Other  | Local |   LST   |License |DO/FO/BC|
|   Name     |         |  Tax  |Charges|Surcharge|Transport| Charge  | Levies | Sales |Surcharge| Free   |Charges |
|            |         |       |       |         | Charge  |         | Value  |  Tax  |         |Recovery|        |
+------------+---------+-------+-------+---------+---------+---------+--------+-------+---------+--------+--------+
 123456789012 123456789 1234567 1234567 123456789 123456789 123456789 12345678 1234567 123456789 12345678 12345678				 
				 */
				SqlDataReader SqlDtr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\TaxReport.txt";
				StreamWriter sw = new StreamWriter(path);
				// Condensed
				sw.Write((char)27);//added by vishnu
				sw.Write((char)67);//added by vishnu
				sw.Write((char)0);//added by vishnu
				sw.Write((char)12);//added by vishnu
			
				sw.Write((char)27);//added by vishnu
				sw.Write((char)78);//added by vishnu
				sw.Write((char)5);//added by vishnu
							
				sw.Write((char)27);//added by vishnu
				sw.Write((char)15);
				//**********
				string des="-------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==========",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("Tax Report",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==========",des.Length));
				sw.WriteLine("");
				sw.WriteLine("+------------+---------+-------+---------+-----------+-----------+-----------+----------+---------+-----------+----------+----------+");
				sw.WriteLine("|  Product   |Reduction| Entry |   RPG   |    RPG    |   Local   | Transport |  Other   |  Local  |    LST    | License  | DO/FO/BC |");
				sw.WriteLine("|   Name     |         |  Tax  | Charges | Surcharge | Transport |  Charge   |  Levies  |  Sales  | Surcharge |  Free    | Charges  |");
				sw.WriteLine("|            |         |       |         |           |  Charge   |           |  Value   |   Tax   |           | Recovery |          |");
				sw.WriteLine("+------------+---------+-------+---------+-----------+-----------+-----------+----------+---------+-----------+----------+----------+");
				//             123456789012 123456789 1234567 1234567 123456789 123456789 123456789 12345678 1234567 123456789 12345678 12345678				        
				string info = " {0,-12:S} {1,9:S} {2,7:S} {3,9:S} {4,11:S} {5,11:S} {6,11:S} {7,10:S} {8,9:S} {9,11:S} {10,10:S} {11,10:S}"; 
				dbobj.SelectQuery("select p.prod_name, cast(Reduction as varchar)+' '+Unit_rdc as Reduction,cast(entry_tax as varchar)+' '+unit_etax as Entry_Tax, cast(rpg_charge as varchar)+' '+Unit_rpgchg as rpg_charge,cast(rpg_surcharge as varchar)+' '+Unit_rpgschg as rpg_surcharge,cast(LT_charge as varchar)+' '+Unit_ltchg as LT_Charge,cast(tran_charge as varchar)+' '+Unit_tchg as trans_charge,cast(Other_Lvy as varchar)+' '+Unit_olvy as Other_Lvy,cast(LST as varchar)+' '+Unit_LST as LST, cast(LST_Surcharge as varchar)+' '+Unit_lstschg as LST_Surcharge,cast(LF_Recov as varchar)+' '+Unit_lfrecov as LF_Recov, cast(dofobc_Charge as varchar)+' '+Unit_dochg as dofobc_Charge  from tax_entry t, Products p where p.Prod_ID =  t.ProductID ",ref SqlDtr); 
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						sw.WriteLine(info,  SqlDtr.GetValue(0).ToString(),
							SqlDtr.GetValue(1).ToString(), 
							SqlDtr.GetValue(2).ToString(), 
							SqlDtr.GetValue(3).ToString(), 
							SqlDtr.GetValue(4).ToString(), 
							SqlDtr.GetValue(5).ToString(), 
							SqlDtr.GetValue(6).ToString(), 
							SqlDtr.GetValue(7).ToString(), 
							SqlDtr.GetValue(8).ToString(), 
							SqlDtr.GetValue(9).ToString(), 
							SqlDtr.GetValue(10).ToString(), 
							SqlDtr.GetValue(11).ToString());
					}

				}
				else
				{
					MessageBox.Show("Data not available"); 
					sw.Close();
					return;
				}

				SqlDtr.Close();
				sw.WriteLine("+------------+---------+-------+---------+-----------+-----------+-----------+----------+---------+-----------+----------+----------+");
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close(); 
				Print();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TaxReport.aspx,Method:BtnPrint_Click()   EXCEPTION: "+ex.Message+".  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the TaxReport.txt file name to print.
		/// </summary>
		public void Print()
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

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);

					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\TaxReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:TaxReport.aspx Method:Print()    Tax Report  Printed   "+"  userid "+uid);
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:TaxReport.aspx Method:Print()    EXCEPTION: "+ane.Message+"  userid "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:TaxReport.aspx Method:Print()     EXCEPTION: "+se.Message+" userid "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:TaxReport.aspx Method:Print()   EXCEPTION: "+es.Message+"   userid "+uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:TaxReport.aspx Method:Print()   EXCEPTION : "+ex.Message+"  userid "+uid);
			}
		}
	}
}