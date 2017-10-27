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
	/// Summary description for SSRPerforma.
	/// </summary>
	public partial class SSRPerforma : System.Web.UI.Page
	{
		public static int View=0;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public double[] TotalAmount = new double[4];
		public string uid;
	
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
				CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:PageLoad  Exception "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				TotalAmount = new double[4];
				View=0;
				txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				//*******************
				object ob=null;
				SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//SqlCommand cmd;
				//SqlDataReader rdr1=null,rdr=null;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();

				//call this procedure ProInsertLedgerDetails insert the record in LedgerDetails table who customer balance is remaining.
				dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetails",ref ob,"@Cust_ID","");
				// To checks the user privileges from session.

				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="43";
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
				
				/*rdr = obj.GetRecordSet("select CustID,sum(creditamount) CreditAmount from customerledgertable where CreditAmount>0 group by custid order by custid");
				int i=0;
				Con.Open();
				while(rdr.Read())
				{
					i++;
					double Amount=double.Parse(rdr["CreditAmount"].ToString());
					rdr1 = obj1.GetRecordSet("select * from LedgerDetails where Cust_ID='"+rdr["Custid"].ToString()+"' and Amount>0 order by Bill_Date");
					while(rdr1.Read())
					{
						Amount=Amount-double.Parse(rdr1["Amount"].ToString());
						if(Amount>=0)
						{
							//Con.Open();
							cmd = new SqlCommand("update LedgerDetails set Amount=0 where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",Con);
							cmd.ExecuteNonQuery();
							//cmd.Dispose();
							//Con.Close();
						}
						else
						{
							//Con.Open();
							cmd = new SqlCommand("update LedgerDetails set Amount=abs("+Amount+") where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",Con);
							cmd.ExecuteNonQuery();
							//cmd.Dispose();
							//Con.Close();
							break;
						}
					}
					rdr1.Close();
				}
				//Con.Open();
				cmd = new SqlCommand("delete from LedgerDetails where amount=0",Con);
				cmd.ExecuteNonQuery();
				//cmd.Dispose();
				//Con.Close();
				*/
				//*******************
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
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
		/// This method is return the total sales in liter of passing customer id in given period.
		/// </summary>
		public string GetSalesLtr(string Cust_ID)
		{
			double sale=0;
			InventoryClass obj = new InventoryClass();
			//SqlDataReader rdr = obj.GetRecordSet("select sum(qty*total_qty) from sales_master sm,sales_details sd,products p where p.prod_id=sd.prod_id and sm.invoice_no=sd.invoice_no and cust_id='"+Cust_ID+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			SqlDataReader rdr = obj.GetRecordSet("select sum(qty*total_qty) from sales_master sm,sales_details sd,products p where sm.invoice_no=sd.invoice_no and sd.prod_id=p.prod_id and cust_id in(select cust_id from customer where ssr='"+Cust_ID+"') and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					sale=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			TotalAmount[0]+=sale;
			return GenUtil.strNumericFormat(sale.ToString());
		}

		/// <summary>
		/// This method is return the total sales amount of passing customer id in given period.
		/// </summary>
		public string GetSalesRs(string Cust_ID)
		{
			double sale=0;
			InventoryClass obj = new InventoryClass();
			//SqlDataReader rdr = obj.GetRecordSet("select sum(net_amount) from sales_master where cust_id='"+Cust_ID+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			SqlDataReader rdr = obj.GetRecordSet("select sum(net_amount) from sales_master where cust_id in(select cust_id from customer where ssr='"+Cust_ID+"') and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					sale=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			TotalAmount[1]+=sale;
			return GenUtil.strNumericFormat(sale.ToString());
		}

		/// <summary>
		/// this method is return the Total receipt of passing the customer id and calculate in given period.
		/// </summary>
		double TotReceipt=0;
		public string GetReceipt(string Cust_ID)
		{
			double sale=0;
			InventoryClass obj = new InventoryClass();
			//SqlDataReader rdr = obj.GetRecordSet("select sum(creditAmount) from customerledgertable where particular like 'payment Received%' and custid='"+Cust_ID+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			SqlDataReader rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable, customer where custid=cust_id and ssr='"+Cust_ID+"' and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					sale=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			TotalAmount[2]+=sale;
			TotReceipt=sale;          // Add by vikas By vikas 6.3.2013
			return GenUtil.strNumericFormat(sale.ToString());
		}

		/// <summary>
		/// This method return the outstanding/closing balance of passing the customer id
		/// </summary>
		public double totalOutStanding=0;
		public string GetOutstanding(string Cust_ID)
		{
			double s3=0;
			double s4=0;
			double DrTot=0,CrTot=0; 
			double sale=0;
			SqlDataReader rdr1 = null;
			InventoryClass obj1 = new InventoryClass();
			sale=0;
			//rdr1 = obj1.GetRecordSet("select sum(amount) from ledgerdetails ld,customer c where ld.cust_id=c.cust_id and ssr='"+Cust_ID+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
			//rdr1 = obj1.GetRecordSet("select sum(balance) from custout co,customer c where co.cust_id=c.cust_id and ssr='"+Cust_ID+"' and BalanceType='Dr.'");
			rdr1 = obj1.GetRecordSet("select sum(co.Op_Balance),co.Balance_Type,Sum(DebitAmount) Debit,Sum(CreditAmount) Credit from custout co,customer c where co.cust_id=c.cust_id and ssr='"+Cust_ID+"' group by co.Balance_Type");
			while(rdr1.Read())
			{
				if(rdr1["Balance_Type"].ToString()=="Dr.")
				{
					if(rdr1.GetValue(0).ToString()!=null && rdr1.GetValue(0).ToString()!="")          
						sale=double.Parse(rdr1.GetValue(0).ToString());
					DrTot+=sale;
					
				}
				else if(rdr1["Balance_Type"].ToString()=="Cr.")
				{
					if(rdr1.GetValue(0).ToString()!=null && rdr1.GetValue(0).ToString()!="")          
						sale=double.Parse(rdr1.GetValue(0).ToString());
					CrTot+=sale;
					
				}
				DrTot+=double.Parse(rdr1["Debit"].ToString());
				CrTot+=double.Parse(rdr1["Credit"].ToString());
			}
			rdr1.Close();

			if(DrTot>CrTot)
			{
				sale=DrTot-CrTot;
			}
			else
			{
				sale=CrTot-DrTot;
			}

			TotalAmount[3]+=sale;
			return GenUtil.strNumericFormat(sale.ToString());
		}

		public double baldr;
		public double balcr;
		/// <summary>
		/// This is used sum of Closing Amount.
		protected string CheckClosing(string bal,string baltype)
		{
			if(!bal.Equals(""))
			{
				if(baltype.Equals("Dr."))
				{
					baldr+= System.Convert.ToDouble(bal);
				}
				else
					balcr+= System.Convert.ToDouble(bal);
				return System.Convert.ToString(Math.Round(System.Convert.ToDouble(bal),2))+baltype; 
			}
			else
				return "0";
		}

		/// <summary>
		/// Prepares the report file SSRPerformance.txt for printing.
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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSR Perfomance Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SSRPerformance.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSR Perfomance Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSR Perfomance Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSR Perfomance Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSR Perfomance Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
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
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SSRPerformance.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			//sql="select c.cust_name,sm.cust_id,c.city from sales_master sm,customer c where sm.cust_id=c.cust_id and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by sm.cust_id,cust_name,c.city order by cust_name";
			sql="select Emp_Name,Emp_ID from Employee where Designation='Servo Sales Representative' and status=1 order by Emp_Name";
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
			string des="--------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SSR Performance From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
			//sw.WriteLine("From Date : "+txtDateFrom.Text+", To Date : "+txtDateTo.Text);
			sw.WriteLine("+----------------------------------------+--------------+--------------+-------------+-----------------+");
			sw.WriteLine("|                 SSR Name               | Sales in Ltr | Sales in Rs. |   Receipt   |   OutStanding   |");
			sw.WriteLine("+----------------------------------------+--------------+--------------+-------------+-----------------+");
			//             1234567890123456789012345678901234567890 12345678901234 12345678901234 1234567890123 12345678901234567
			if(rdr.HasRows)
			{
				info = "|{0,-40:S}|{1,14:F}|{2,14:S}|{3,13:S}|{4,17:S}|";
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["Emp_Name"].ToString(),40),
						GetSalesLtr(rdr["Emp_ID"].ToString()),
						GetSalesRs(rdr["Emp_ID"].ToString()),
						GetReceipt(rdr["Emp_ID"].ToString()),
						GetOutstanding(rdr["Emp_ID"].ToString())
						);
				}
			}
			sw.WriteLine("+----------------------------------------+--------------+--------------+-------------+-----------------+");
			sw.WriteLine(info,"      Total",
				GenUtil.strNumericFormat(TotalAmount[0].ToString()),
				GenUtil.strNumericFormat(TotalAmount[1].ToString()),
				GenUtil.strNumericFormat(TotalAmount[2].ToString()),
				GenUtil.strNumericFormat(TotalAmount[3].ToString())
				);
			sw.WriteLine("+----------------------------------------+--------------+--------------+-------------+-----------------+");
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file SSRPerformence.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SSR Perfomance Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:SSRPerforma.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SSR Perfomance Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\SSRPerformance.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			//sql="select c.cust_name,sm.cust_id,c.city from sales_master sm,customer c where sm.cust_id=c.cust_id and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by sm.cust_id,cust_name,c.city order by cust_name";
			sql="select Emp_Name,Emp_ID from Employee where Designation='Servo Sales Representative'  and status=1 order by Emp_Name";
			rdr = obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text+"\tTo Date\t"+txtDateTo.Text);
			sw.WriteLine("SSR Name\tSales In Ltr\tSales in Rs.\tReceipt\tOutStanding");
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					sw.WriteLine(GenUtil.TrimLength(rdr["Emp_Name"].ToString(),40)+"\t"+
						GetSalesLtr(rdr["Emp_ID"].ToString())+"\t"+
						GetSalesRs(rdr["Emp_ID"].ToString())+"\t"+
						GetReceipt(rdr["Emp_ID"].ToString())+"\t"+
						GetOutstanding(rdr["Emp_ID"].ToString())
						);
				}
				sw.WriteLine("      Total"+"\t"+
					GenUtil.strNumericFormat(TotalAmount[0].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[1].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[2].ToString())+"\t"+
					GenUtil.strNumericFormat(TotalAmount[3].ToString())
					);
			}
			sw.Close();
		}

		private DateTime getdate(string dat,bool to)
		{
			string[] dt=dat.IndexOf("/")>0? dat.Split(new char[]{'/'},dat.Length) : dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}

		/// <summary>
		/// This method is used to view the report. if value is 1 otherwise not show the report.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			/******************Add by vikas 6.3.2013**********************/
			object op=null;
			int x=0;
			System.Data.SqlClient.SqlDataReader rdr=null;
			dbobj.Insert_or_Update("truncate table custout", ref x);
			string sql1="";
			sql1="select cust_id from Customer order by cust_name,city,cust_type";
			dbobj.SelectQuery(sql1,ref rdr);
			while(rdr.Read())
				dbobj.ExecProc(OprType.Insert,"Sp_CustOutstanding ",ref op,"@id",Int32.Parse(rdr["cust_id"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true),"@todate",getdate(txtDateTo.Text,true));
			rdr.Close();
			/*****************Add by vikas**********************/
			View=1;
		}
	}
}