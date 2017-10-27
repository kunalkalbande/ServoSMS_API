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
using System.Data .SqlClient ;
using System.Net; 
using System.Net.Sockets ;
using System.IO ;
using System.Text;
using DBOperations;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for TradingAccount.
	/// </summary>
	public partial class TradingAccount : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
	
		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid = (Session["User_Name"].ToString());  
				// Put user code to initialize the page here
				if(!IsPostBack )
				{
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="50";
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
						return;
					}
					#endregion 
					Table1.Visible  = false;
				}
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
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

		/// <summary>
		/// This method is used to returns the date in MM/DD/YYYY format
		/// </summary>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0? str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// This funstion calls the procedure getProfitLoss and displays the calculated values on the screen.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {                  
					MessageBox.Show("Date From Should be less than Date To");
					return;
				}
				Table1.Visible = true; 
				SqlConnection con = null;
				SqlCommand cmd= null;
				SqlDataReader SqlDtr = null;
				string gross_profit= "";
				double gross_pro = 0;
				string Net_Profit = "";
				double Net_Pro = 0;
				double total1 = 0;
				double total2 = 0;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
                cmd = new SqlCommand( "exec getProfitLoss '"+GenUtil.str2DDMMYYYY(txtDateFrom.Text)+"','"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"'", con );
                //cmd = new SqlCommand("exec getProfitLoss '" + GenUtil.str2MMDDYYYY(txtDateFrom.Text) + "','" + GenUtil.str2MMDDYYYY(txtDateTo.Text) + "'", con);
                SqlDtr = cmd.ExecuteReader();
				if(SqlDtr.Read())
				{
					lblOpeningStockValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
					lblPurchaseValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString());
					lblSalesValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString());
					lblClossingStockValue .Text = GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());    
					lblDirectExpensesValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString());         
					lblDirectIncomeValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString());
					gross_profit  = GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString());
					if(!gross_profit.Equals("")) 
					{
						gross_pro = System.Convert.ToDouble(gross_profit);
						// if the gross_profit is -ve then display on gross loss side else display in gross profit.
						if(gross_pro < 0)
						{
							lblGrossProfit.Visible = false;
							lblGrossProfitValue.Visible = false; 
							lblGrossProfit1.Visible = false;
							lblGrossProfit1Value.Visible = false; 
							lblGrossLoss.Visible = true;
							lblGrossLossAmount.Visible = true;
							lblGrossLoss1.Visible = true;
							lblGrossLoss1Amount.Visible = true;
							gross_pro = (gross_pro * -1);
							lblGrossLossAmount.Text = GenUtil.strNumericFormat(gross_pro.ToString()); 
							lblGrossLoss1Amount.Text = GenUtil.strNumericFormat(gross_pro.ToString());  
							total1 = gross_pro;
						}
						else
						{
							lblGrossProfit.Visible = true;
							lblGrossProfitValue.Visible = true; 
							lblGrossProfit1.Visible = true;
							lblGrossProfit1Value.Visible = true; 
							lblGrossLoss.Visible = false;
							lblGrossLossAmount.Visible = false;
							lblGrossLoss1.Visible = false;
							lblGrossLoss1Amount.Visible = false;
							lblGrossProfitValue.Text = GenUtil.strNumericFormat(gross_pro.ToString()); 
							lblGrossProfit1Value.Text = GenUtil.strNumericFormat(gross_pro.ToString()); 
							total2 = gross_pro;
						}
					}
					lblDebitTotalValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString());
					lblCreditTotalValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()); 
					lblIndirectExpensesValue.Text =  GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString()); 
					lblIndirectIncomeValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString());  

					Net_Profit  = GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString());
					if(!Net_Profit.Equals("")) 
					{
						Net_Pro = System.Convert.ToDouble(Net_Profit);
						// if Net Profit is -ve then display in Net Loss  else display in Net Profit.
						if(Net_Pro < 0)
						{
							lblNetProfit.Visible = false;
							lblNetProfitValue.Visible = false; 
							lblNetLoss.Visible = true;
							lblNetLossAmount.Visible = true;
							Net_Pro = (Net_Pro * -1);
							lblNetLossAmount.Text = GenUtil.strNumericFormat(Net_Pro.ToString()); 
							total2  = total2 + Net_Pro;
						}
						else
						{
							lblNetProfit.Visible = true;
							lblNetProfitValue.Visible = true; 
							lblNetLoss.Visible = false;
							lblNetLossAmount.Visible = false;
							lblNetProfitValue.Text = GenUtil.strNumericFormat(Net_Pro.ToString()); 
							total1 = total1 + Net_Pro;
						}
					}
					total1 = total1 + System.Convert.ToDouble(lblIndirectExpensesValue.Text);
					total2 = total2 + System.Convert.ToDouble(lblIndirectIncomeValue.Text);
					lblTotal1Value.Text = GenUtil.strNumericFormat(total1.ToString());  
					lblTotal2Value.Text = GenUtil.strNumericFormat(total2.ToString());                 
				}
				SqlDtr.Close();
				con.Close();  
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:btnShow_Click.  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This fucntion does the same and calls the getProfitLoss procedure and writes the values in to TradingAccount.txt file to print.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(txtDateTo.Text))>0)
				{
					MessageBox.Show("Date From Should be less than Date To");
					return;
				}
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\TradingAccount.txt";
				StreamWriter sw = new StreamWriter(path);
				/*

															===============
									Trading Account
										===============

				From Date : mm/dd/yyyy
				To Date   : mm/dd/yyyy

				+----------------------------------+----------------------------------+
				|         Debit Side               |           Credit Side            |
				+----------------------------------+----------------------------------+
				|Opening Stock     12              |Sales             12              | 
				|Purchase          12              |Closing Stock     12              |
				|Direct Expenses   12              |Direct Income     12              | 
				|                                  |                  12              | 
				|Gross Profit      12              |Gross Loss        12              |
				|----------------- 123456789012.00 |----------------- 12              | 
				|Total             123456789012.00 |Total             123456789012.00 | 
				|-----------------                 |-----------------                 |  
				|                                  |                                  | 
				+----------------------------------+----------------------------------+
				|                      Profit & Loss Account                          |
				+----------------------------------+----------------------------------+
				|Gross Loss        12              |Gross Profit      12              |
				|Indirect Expenses 12              |Indirect Income   12              |
				|Net Profit        12              | Net Loss         12              |
				|                                  |                  12              |
				|-----------------                 |-----------------                 | 
				|Total             12              |Total             12              |
				|-----------------                 |-----------------                 |
				|                                  |                                  |
				+----------------------------------+----------------------------------+
				*/
				
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
				string des="-----------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("=================",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("Trading Account",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("=================",des.Length)); 
				sw.WriteLine("From Date : "+txtDateFrom.Text ); 
				sw.WriteLine("To Date   : "+txtDateTo.Text ); 
				sw.WriteLine(""); 
				sw.WriteLine("+----------------------------------+----------------------------------+"); 
				sw.WriteLine("|         Debit Side               |           Credit Side            |"); 
				sw.WriteLine("+----------------------------------+----------------------------------+"); 
				          
			
				SqlConnection con = null;
				SqlCommand cmd= null;
				SqlDataReader SqlDtr = null;
				string gross_profit= "";
				double gross_pro = 0;
				string Net_Profit = "";
				double Net_Pro = 0;
				double total1 = 0;
				double total2 = 0;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				cmd = new SqlCommand( "exec getProfitLoss '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"','"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'", con );
				SqlDtr = cmd.ExecuteReader();
				if(SqlDtr.Read())
				{
					sw.WriteLine("|Opening Stock     {0,15:F} |Sales             {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString())); 
					sw.WriteLine("|Purchase          {0,15:F} |Closing Stock     {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString()));
					sw.WriteLine("|Direct Expenses   {0,15:F} |Direct Income     {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString()));              
					sw.WriteLine("|                                  |                                  |"); 
					gross_profit  = GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString());
					if(!gross_profit.Equals("")) 
					{
						gross_pro = System.Convert.ToDouble(gross_profit);
						if(gross_pro < 0)
						{
							gross_pro = (gross_pro * -1);
							total1 = gross_pro;
							sw.WriteLine("|                                  |Gross Loss        {0,15:F} |",GenUtil.strNumericFormat(gross_pro.ToString())); 
						}
						else
						{
							total2 = gross_pro;
							sw.WriteLine("|Gross Profit      {0,15:F} |                                  |",GenUtil.strNumericFormat(gross_pro.ToString())); 
						}
					}
					sw.WriteLine("|-----------------                 |-----------------                 |"); 
					sw.WriteLine("|Total             {0,15:F} |Total             {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString())); 
					sw.WriteLine("|-----------------                 |-----------------                 |"); 
					sw.WriteLine("|                                  |                                  |"); 
					sw.WriteLine("+----------------------------------+----------------------------------+"); 
					sw.WriteLine("|                      Profit & Loss Account                          |"); 
					sw.WriteLine("+----------------------------------+----------------------------------+"); 
					if(!gross_profit.Equals("")) 
					{
						gross_pro = System.Convert.ToDouble(gross_profit);
						if(gross_pro < 0)
						{
							gross_pro = (gross_pro * -1);
							sw.WriteLine("|Gross Loss        {0,15:F} |                                  |",GenUtil.strNumericFormat(gross_pro.ToString()));
						}
						else
						{
							sw.WriteLine("|                                  |Gross Profit      {0,15:F} |",GenUtil.strNumericFormat(gross_pro.ToString()));
						}
					}
					sw.WriteLine("|Indirect Expenses {0,15:F} |Indirect Income   {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));  
					
					Net_Profit  = GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString());
					if(!Net_Profit.Equals("")) 
					{
						Net_Pro = System.Convert.ToDouble(Net_Profit);
						if(Net_Pro < 0)
						{
							Net_Pro = (Net_Pro * -1);
							total2  = total2 + Net_Pro;
							sw.WriteLine("|                                  | Net Loss         {0,15:F} |",GenUtil.strNumericFormat(Net_Pro.ToString())); 
						}
						else
						{
							total1 = total1 + Net_Pro;
							sw.WriteLine("|Net Profit        {0,15:F} |                                  |",GenUtil.strNumericFormat(Net_Pro.ToString())); 
						}
					}
					total1 = total1 + System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString()));
					total2 = total2 + System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));
			
					sw.WriteLine("|                                  |                                  |"); 
					sw.WriteLine("|-----------------                 |-----------------                 |"); 
					sw.WriteLine("|Total             {0,15:F} |Total             {1,15:F} |",GenUtil.strNumericFormat(total1.ToString()),GenUtil.strNumericFormat(total2.ToString())); 
					sw.WriteLine("|-----------------                 |-----------------                 |"); 
					sw.WriteLine("|                                  |                                  |"); 
					sw.WriteLine("+----------------------------------+----------------------------------+"); 
				}
				SqlDtr.Close();
				con.Close();  
				sw.Close(); 
				Print(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:BtnPrint_Click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\TradingAccount.xls";
			StreamWriter sw = new StreamWriter(path);
			
			SqlConnection con = null;
			SqlCommand cmd= null;
			SqlDataReader SqlDtr = null;
			string gross_profit= "";
			double gross_pro = 0;
			string Net_Profit = "";
			double Net_Pro = 0;
			double total1 = 0;
			double total2 = 0;
			con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			con.Open ();
			cmd = new SqlCommand( "exec getProfitLoss '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"','"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'", con );
			SqlDtr = cmd.ExecuteReader();
			if(SqlDtr.Read())
			{
				sw.WriteLine("From Date\t"+txtDateFrom.Text);
				sw.WriteLine("To Date\t"+txtDateTo.Text);
				sw.WriteLine();
				sw.WriteLine("Debit Side\t\tCredit Side"); 	
				sw.WriteLine("Opening Stock\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString())+"\tSales\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString())); 
				sw.WriteLine("Purchase\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString())+"\tClosing Stock\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString()));
				sw.WriteLine("Direct Expenses\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString())+"\tDirect Income\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString()));              
				gross_profit  = GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString());
				if(!gross_profit.Equals("")) 
				{
					gross_pro = System.Convert.ToDouble(gross_profit);
					if(gross_pro < 0)
					{
						gross_pro = (gross_pro * -1);
						total1 = gross_pro;
						sw.WriteLine("\t\tGross Loss\t"+GenUtil.strNumericFormat(gross_pro.ToString())); 
					}
					else
					{
						total2 = gross_pro;
						sw.WriteLine("Gross Profit\t"+GenUtil.strNumericFormat(gross_pro.ToString())); 
  
					}

				}
				sw.WriteLine("Total\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString())+"\tTotal\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString())); 
				sw.WriteLine(); 
				sw.WriteLine(); 
				sw.WriteLine("Profit & Loss Account"); 
				if(!gross_profit.Equals("")) 
				{
					gross_pro = System.Convert.ToDouble(gross_profit);
					if(gross_pro < 0)
					{
						gross_pro = (gross_pro * -1);
						sw.WriteLine("Gross Loss\t"+GenUtil.strNumericFormat(gross_pro.ToString()));
					}
					else
					{
						sw.WriteLine("\t\tGross Profit\t"+GenUtil.strNumericFormat(gross_pro.ToString()));

					}
				}
				sw.WriteLine("Indirect Expenses\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString())+"\tIndirect Income\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));  
					
				Net_Profit  = GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString());
				if(!Net_Profit.Equals("")) 
				{
					Net_Pro = System.Convert.ToDouble(Net_Profit);
					if(Net_Pro < 0)
					{
						Net_Pro = (Net_Pro * -1);
						total2  = total2 + Net_Pro;
						sw.WriteLine("\t\tNet Loss\t"+GenUtil.strNumericFormat(Net_Pro.ToString())); 
					}
					else
					{
						total1 = total1 + Net_Pro;
						sw.WriteLine("Net Profit\t"+GenUtil.strNumericFormat(Net_Pro.ToString())); 
					}
				}
				total1 = total1 + System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString()));
				total2 = total2 + System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));
				sw.WriteLine("Total\t"+GenUtil.strNumericFormat(total1.ToString())+"\tTotal\t"+GenUtil.strNumericFormat(total2.ToString())); 
			}
			SqlDtr.Close();
			con.Close();  
			sw.Close();
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the Trading.txt file name to print.
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
					CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\TradingAccount.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					//CreateLogFiles.ErrorLog("Form:Vehiclereport.aspx,Method:print"+ "  Daily sales record  Printed   userid  "+uid);
					CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file TradingAccounts.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//if(GridReport.Visible==true)
				//{
				ConvertToExcel();
				MessageBox.Show("Successfully Convert File Into Excel Format");
				CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    TradingAccount Report Convert Into Excel Format, userid  "+uid);
				//}
				//else
				//{
				//	MessageBox.Show("Please Click the View Button First");
				//	return;
				//}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:TradingAccount.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    TradingAccount Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}