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
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using DBOperations; 

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SSRIncentiveSheet.
	/// </summary>
	public partial class SSRIncentiveSheet : System.Web.UI.Page
	{
		string uid;
		public static int View = 0;
		public double[] TotalAmount = new double[10];
		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				try
				{
					GetSSRInc();
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="42";
					string[,] Priv=(string[,]) Session["Privileges"];
									
					for(i=0;i<Priv.GetLength(0);i++)
					{
						if(Priv[i,0] == Module && Priv[i,1] == SubModule)
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
					txtDateFrom.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
					txtDateTo.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					View=0;
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				}
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		public void GetSSRInc()
		{
			/******* Start add by vikas 02.06.09 ******************/
			
			
			SqlDataReader dtr=null;
			string sql="select * from setDis";
			InventoryClass obj = new InventoryClass();
			dtr=obj.GetRecordSet(sql);
			while(dtr.Read())
			{
				if(dtr["SSRincentiveStatus"].ToString()!="0")
					Cache["ssrinc"]=dtr["SSRincentive"].ToString();
				else
					Cache["ssrinc"]=0;
			}
			dtr.Close();
						
			/**********End***************/
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

		
		public double TotalReceipt = 0;
		
		/// <summary>
		/// This method is used to get sum of total receipt amount from customerledgertable in given date.
		/// </summary>
		public string GetReceipt(string Emp_ID)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_ID+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
				else
					TotalReceipt=0;
			}
			rdr.Close();
			TotalAmount[0]+=TotalReceipt;
			return GenUtil.strNumericFormat(TotalReceipt.ToString());
		}

		//public double BasicSalary = 0;
		//public double[] TotalSalary = new double[7];
		/// <summary>
		/// This method is used to get sum of total receipt amount from customerledgertable in given date.
		/// </summary>
		public string GetBasicSalary(string Emp_ID)
		{
			double BasicSalary = 0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select salary from employee where emp_id="+Emp_ID);
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					BasicSalary = double.Parse(rdr.GetValue(0).ToString());
				else
					BasicSalary=0;
			}
			rdr.Close();
			TotalAmount[7]+=BasicSalary;
			return GenUtil.strNumericFormat(BasicSalary.ToString());
		}

		/// <summary>
		/// This method is used to get sum of total cheque bounce amount from customerledgertable in given date.
		/// </summary>
		public string GetBounce(string Emp_ID)
		{
			double bounce=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_ID+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					bounce = double.Parse(rdr.GetValue(0).ToString());
				else
					bounce=0;
			}
			rdr.Close();
			TotalReceipt-=bounce;
			TotalAmount[4]+=bounce;
			return GenUtil.strNumericFormat(bounce.ToString());
		}

		/// <summary>
		/// This method is used to get sum of total cash discount amount from Accountsledgertable in given date.
		/// </summary>
		public string GetCashDiscount(string Emp_ID)
		{
			double cd =0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					cd=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			TotalReceipt-=cd;
			TotalAmount[1]+=cd;
			return GenUtil.strNumericFormat(cd.ToString());
		}

		/// <summary>
		/// This method is used to get sum of total spacial discount amount from Accountsledgertable in given date.
		/// </summary>
		public string GetSpacialDiscount(string Emp_ID)
		{
			double sd =0;
			InventoryClass obj = new InventoryClass();
			//SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and particulars like 'Receipt_sd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)");
			SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					sd=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			TotalReceipt-=sd;
			TotalAmount[2]+=sd;
			return GenUtil.strNumericFormat(sd.ToString());
		}

		/// <summary>
		/// This method is used to get sum of total credit note amount from Customerledgertable in given date.
		/// </summary>
		public string GetCreditNote(string Emp_ID)
		{
			double cn=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_ID+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					cn=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			TotalReceipt-=cn;
			TotalAmount[3]+=cn;
			return GenUtil.strNumericFormat(cn.ToString());
		}

		/// <summary>
		/// This method is used to get 0.5 incentive in given incentive amount.
		/// </summary>
		public string GetIncentive(string Inc)
		{
			//coment by vikas 02.06.09 double totInc=double.Parse(Inc)*.50/100;
			
			/******* Start add by vikas 02.06.09 ******************/
			
			double ssrinc=Convert.ToDouble(Cache["ssrinc"].ToString());

			/*SqlDataReader dtr=null;
			string sql="select * from setDis";
			InventoryClass obj = new InventoryClass();
			dtr=obj.GetRecordSet(sql);
			while(dtr.Read())
			{
				if(dtr["SSRincentiveStatus"].ToString()!="0")
					ssrinc=Convert.ToDouble(dtr["SSRincentive"].ToString());
				else
					ssrinc=0;
			}
			dtr.Close();*/
			double totInc=double.Parse(Inc)*ssrinc/100;
			
			/**********End***************/

			TotalAmount[6]+=totInc;
			return GenUtil.strNumericFormat(totInc.ToString());
		}

		public string GetSalaryIncentive(string Inc, string Emp_ID)
		{
			
			double ssrinc=Convert.ToDouble(Cache["ssrinc"].ToString());
			double totInc=double.Parse(Inc)*ssrinc/100;

			double BasicSalary = 0; double TotalSalInc=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select salary from employee where emp_id="+Emp_ID);
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					BasicSalary = double.Parse(rdr.GetValue(0).ToString());
				else
					BasicSalary=0;
			}
			rdr.Close();
			TotalSalInc=BasicSalary+totInc;
			TotalAmount[8]+=TotalSalInc;
			return GenUtil.strNumericFormat(TotalSalInc.ToString());

			
			
			/**********End***************/

			//TotalAmount[6]+=totInc;
			//return GenUtil.strNumericFormat(totInc.ToString());
		}

		/// <summary>
		/// This method is used to show the report
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			View=1;
		}

		/// <summary>
		/// Prepares the report file SSRIncentiveSheet.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
				if(View==1)
					makingReport();
				else
				{
					MessageBox.Show("Please Click The View Button Fisrt");
					return;
				}
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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRIncentiveSheet Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SSRIncentiveSheet.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRIncentiveSheet Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRIncentiveSheet Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRIncentiveSheet Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRIncentiveSheet Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			InventoryClass obj = new InventoryClass();
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SSRIncentiveSheet.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			sql="select Emp_ID,Emp_Name from Employee where Designation='Servo Sales Representative'";
			rdr = obj.GetRecordSet(sql);
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
			string des="---------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			sw.WriteLine(GenUtil.GetCenterAddr("========================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SSR Incentive Sheet Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("========================================================",des.Length));
			//sw.WriteLine("From Date : "+txtDateFrom.Text+", To Date : "+txtDateTo.Text);
			
			// coment by vikas 26.10.2012 sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+");
			// coment by vikas 26.10.2012 sw.WriteLine("|         SSR Name        |   Receipt   | Cash Disc.  |  Spa. Disc  | Credit Note | Cheque Bounce | Total Receipt |  Incentive  |");
			// coment by vikas 26.10.2012 sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+");

			sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+--------+----------+");
			sw.WriteLine("|         SSR Name        |   Receipt   | Cash Disc.  |  Spa. Disc  | Credit Note | Cheque Bounce | Total Receipt |  Incentive  | Salary |  Sal+Inc |");
			sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+--------+----------+");
			//             1234567890123456789012345 1234567890123 1234567890123 1234567890123 1234567890123 123456789012345 123456789012345 1234567890123 12345678 1234567890
			if(rdr.HasRows)
			{
				//coment by vikas 26.10.2012 info = " {0,-25:S} {1,13:F} {2,13:S} {3,13:S} {4,13:S} {5,15:S} {6,15:S} {7,13:S}";
				info = " {0,-25:S} {1,13:F} {2,13:S} {3,13:S} {4,13:S} {5,15:S} {6,15:S} {7,13:S} {8,8:S} {9,10:S}";
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["Emp_Name"].ToString(),25),
						GetReceipt(rdr["Emp_ID"].ToString()),
						GetCashDiscount(rdr["Emp_ID"].ToString()),
						GetSpacialDiscount(rdr["Emp_ID"].ToString()),
						GetCreditNote(rdr["Emp_ID"].ToString()),
						GetBounce(rdr["Emp_ID"].ToString()),
						GenUtil.strNumericFormat(TotalReceipt.ToString()),
						GetIncentive(TotalReceipt.ToString()),								             
						GetBasicSalary(rdr["Emp_ID"].ToString()),										// add by vikas 26.10.2012
						GetSalaryIncentive(TotalReceipt.ToString(),rdr["Emp_ID"].ToString())            // add by vikas 26.10.2012
						);
					TotalAmount[5]+=TotalReceipt;
				}
				
			}
			//coment by vikas 26.10.2012 sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+");
			sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+--------+----------+");
			
			sw.WriteLine(info,"  total",
				GenUtil.strNumericFormat(TotalAmount[0].ToString()),
				GenUtil.strNumericFormat(TotalAmount[1].ToString()),
				GenUtil.strNumericFormat(TotalAmount[2].ToString()),
				GenUtil.strNumericFormat(TotalAmount[3].ToString()),
				GenUtil.strNumericFormat(TotalAmount[4].ToString()),
				GenUtil.strNumericFormat(TotalAmount[5].ToString()),
				GenUtil.strNumericFormat(TotalAmount[6].ToString()),
				GenUtil.strNumericFormat(TotalAmount[7].ToString()),                                            // add by vikas 26.10.2012
				GenUtil.strNumericFormat(TotalAmount[8].ToString())                                             // add by vikas 26.10.2012
				);
			//coment by vikas 26.10.2012 sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+");
			sw.WriteLine("+-------------------------+-------------+-------------+-------------+-------------+---------------+---------------+-------------+--------+----------+");
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file SSRIncentiveSheet.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SSRIncentiveSheet Report Convert Into Excel Format, userid  "+uid);
				}
				else
				{
					MessageBox.Show("Please Click the View Button First");
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:SSRIncentiveSheet.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SSRIncentiveSheet Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SSRIncentiveSheet.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			sql="select Emp_ID,Emp_Name from Employee where Designation='Servo Sales Representative'";
			rdr = obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text+"\tTo Date\t"+txtDateTo.Text);
			//Coment by vikas 27.10.2012 sw.WriteLine("SSR Name\tReceipt\tCash Disc.\tSpa. Disc\tCredit Note\tCheque Bounce\tTotal Receipt\tIncentive");
			sw.WriteLine("SSR Name\tReceipt\tCash Disc.\tSpa. Disc\tCredit Note\tCheque Bounce\tTotal Receipt\tIncentive\tBasic Salary\tSalary + Incentive");
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					sw.WriteLine(rdr["Emp_Name"].ToString()+"\t"+
						GetReceipt(rdr["Emp_ID"].ToString())+"\t"+
						GetCashDiscount(rdr["Emp_ID"].ToString())+"\t"+
						GetSpacialDiscount(rdr["Emp_ID"].ToString())+"\t"+
						GetCreditNote(rdr["Emp_ID"].ToString())+"\t"+
						GetBounce(rdr["Emp_ID"].ToString())+"\t"+
						GenUtil.strNumericFormat(TotalReceipt.ToString())+"\t"+
						GetIncentive(TotalReceipt.ToString())+"\t"+
						GetBasicSalary(rdr["Emp_ID"].ToString())+"\t"+											// add by vikas 27.10.2012
						GetSalaryIncentive(TotalReceipt.ToString(),rdr["Emp_ID"].ToString())					// add by vikas 27.10.2012
						);
					TotalAmount[5]+=TotalReceipt;
				}
				sw.WriteLine("  total"+"\t"+
					GenUtil.strNumericFormat(TotalAmount[0].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[1].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[2].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[3].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[4].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[5].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[6].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[7].ToString())+"\t"+									// add by vikas 27.10.2012
					GenUtil.strNumericFormat(TotalAmount[8].ToString())											// add by vikas 27.10.2012
					);
			}
			sw.Close();
		}
	}
}