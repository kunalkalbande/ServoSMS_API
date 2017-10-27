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
	/// Summary description for DocumentCancelReport.
	/// </summary>
	public partial class DocumentCancelReport : System.Web.UI.Page
	{
		string uid;
		public static ArrayList Purchase = new ArrayList();
		public static ArrayList Sales = new ArrayList();
		public static ArrayList Payment = new ArrayList();
		public static ArrayList Receipt = new ArrayList();
		public static int Flag=0;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				//string uid;
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				string str = ex.ToString();
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="13";
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
				Flag=0;
				Purchase = new ArrayList();
				Sales = new ArrayList();
				Payment = new ArrayList();
				Receipt = new ArrayList();
				txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				//dateValidate();
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

		//this method is used to show the report.
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			Purchase = new ArrayList();
			Sales = new ArrayList();
			Payment = new ArrayList();
			Receipt = new ArrayList();
			PartiesClass obj = new PartiesClass();
			obj.DateFrom=GenUtil.str2MMDDYYYY(txtDateFrom.Text);
			obj.DateTo=GenUtil.str2MMDDYYYY(txtDateTo.Text);
			string str = obj.FatchCancelPurchaseInvoice();
			if(str!="")
			{
				string[] strPur=str.Split(new char[] {','},str.Length);
				for(int i=0;i<strPur.Length;i++)
				{
					Purchase.Add(strPur[i].ToString());
				}
			}
			str="";
			str = obj.FatchCancelSalesInvoice();
			if(str!="")
			{
				string[] strPur1=str.Split(new char[] {','},str.Length);
				//10.07.09 for(int i=0;i<strPur1.Length;i++)
				for(int i=1;i<strPur1.Length;i++)
				{
					//10.07.09 Sales.Add(strPur1[i].ToString());
					string sales_inv=strPur1[i].ToString();
					if(sales_inv.ToString()!="" && sales_inv.ToString()!=null)
                        sales_inv=sales_inv.Substring(3,(sales_inv.Length)-3);

					Sales.Add(sales_inv.ToString());
					
				}
			}
			str="";
			str = obj.FatchCancelPaymentInvoice();
			if(str!="")
			{
				string[] strPur2=str.Split(new char[] {','},str.Length);
				for(int i=0;i<strPur2.Length;i++)
				{
					Payment.Add(strPur2[i].ToString());
				}
			}
			str="";
			//10.07.09 Comented by vikas str = obj.FatchCancelReceiptNo();
			str =FatchCancelReceiptNo1();           //10.07.09 Add by vikas 
			if(str!="")
			{
				string[] strPur2=str.Split(new char[] {','},str.Length);
				for(int i=0;i<strPur2.Length;i++)
				{
					Receipt.Add(strPur2[i].ToString());
				}
			}
			Flag=1;
			/*SqlDataReader rdr = obj.GetRecordSet("select receipt_no from Payment_receipt where  subreceiptno='Deleted' and cast(floor(cast(cast(receipt_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(receipt_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by receipt_no");
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					Receipt.Add(rdr.GetValue(0).ToString());
				}
			}
			rdr.Close();*/
		}

		public string FatchCancelSalesInvoice1()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			int max=0,min=0;
			string deleted_invoice="";
			string str="SELECT max(receipt_no) from Sales_Master where cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString())+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString())+"' and subreceiptno<>'Deleted'";
			rdr=obj.GetRecordSet(str);
			if(rdr.Read())
			{
				max=Convert.ToInt32(rdr.GetValue(0));
			}
			rdr.Close();

			str="SELECT min(receipt_no) from Payment_Receipt where cast(floor(cast(cast(Receipt_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString())+"' and cast(floor(cast(cast(Receipt_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString())+"' and subreceiptno<>'Deleted'";
			rdr=obj.GetRecordSet(str);
			if(rdr.Read())
			{
				min=Convert.ToInt32(rdr.GetValue(0));
			}
			rdr.Close();

			for(int i=min;i<=max;i++)
			{
				int count=0;
				str="SELECT count(receipt_no) from payment_receipt where receipt_no="+i.ToString()+" and subreceiptno<>'deleted'";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					count=Convert.ToInt32(rdr.GetValue(0));
				}
				rdr.Close();

				if(count==0)
				{
					deleted_invoice+=i+",";
				}
			}

			return deleted_invoice;
		}

		public string FatchCancelReceiptNo1()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			int max=0,min=0;
			string deleted_invoice="";
			string str="SELECT max(receipt_no) from Payment_Receipt where cast(floor(cast(cast(Receipt_date as datetime) as float)) as datetime)>=Convert(datetime,'"+GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString())+"',103) and cast(floor(cast(cast(Receipt_date as datetime) as float)) as datetime)<=Convert(datetime,'"+GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString())+"',103) and subreceiptno<>'Deleted'";
			rdr=obj.GetRecordSet(str);
			if(rdr.Read())
			{
				max=Convert.ToInt32(rdr.GetValue(0));
			}
			rdr.Close();

            str = "SELECT min(receipt_no) from Payment_Receipt where cast(floor(cast(cast(Receipt_date as datetime) as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(cast(Receipt_date as datetime) as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and subreceiptno<>'Deleted'";
            rdr =obj.GetRecordSet(str);
			if(rdr.Read())
			{
				min=Convert.ToInt32(rdr.GetValue(0));
			}
			rdr.Close();

			for(int i=min;i<=max;i++)
			{
				int count=0;
				str="SELECT count(receipt_no) from payment_receipt where receipt_no="+i.ToString()+" and subreceiptno<>'deleted'";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					count=Convert.ToInt32(rdr.GetValue(0));
				}
				rdr.Close();

				if(count==0)
				{
					deleted_invoice+=i+",";
				}
			}

			return deleted_invoice;
		}



		/// <summary>
		/// Prepares the report file DocumentCancelReport.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			//10.07.09 MessageBox.Show("This Task is under Proccess...");
			//10.07.09 return;
			if(Flag==0)
			{
				MessageBox.Show("Please Click The View Button First");
				return;
			}
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				makingReport();
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
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt   Attendence Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\AttendenceReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Attendence Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Attendence Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Attendence Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt   Attendence Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\DocumentCancelReport.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			//string strDate = "";
			//sql="select lr.Emp_ID r1,e.Emp_Name r2,lr.Date_From r3,lr.Date_To r4,lr.Reason r5,lr.isSanction r6 from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			
			
			/*10.07.09 sql="select lr.Emp_ID,e.Emp_Name,lr.Date_From,lr.Date_To,lr.Reason,lr.isSanction from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			sql=sql+" order by "+Cache["strOrderBy"];
			dbobj.SelectQuery(sql,ref rdr);*/
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
			/*10.07.09 Purchase = new ArrayList();
			Sales = new ArrayList();
			Payment = new ArrayList();
			Receipt = new ArrayList();*/
			
			ArrayList counter=new ArrayList();
			   
			counter.Add(Purchase.Count.ToString());
			counter.Add(Sales.Count.ToString());
			counter.Add(Payment.Count.ToString());
			counter.Add(Receipt.Count.ToString());
			
			counter.Sort(); 
			
			int max=Convert.ToInt32(counter[3].ToString());

			string des="-----------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			string Purchase_="",Sales_="",Payment_="",Receipt_="";
			sw.WriteLine(GenUtil.GetCenterAddr("------------------------------",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("DOCUMENT CANCELLATION REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("------------------------------",des.Length));
			if(Flag != 0)
			{
				sw.WriteLine("+----------+------------+----------+----------+");
				sw.WriteLine("| Purchase |   Sales    | Receipt  | Payment  |");
				sw.WriteLine("+----------+------------+----------+----------+");
				//             1234567890 123456789012 1234567890 1234567890
				info=" {0,-10:S} {1,-12:S} {2,-10:S} {3,-10:S} ";
				int Count=0;

				for(int i=0;i<max;i++)
				{
					if(Purchase.Count!=0)
					{
						if(Purchase.Count<=i)
						{
							Purchase_="";
						}
						else
						{
							if(Convert.ToString(Purchase[i])!=null && Convert.ToString(Purchase[i])!="")
								Purchase_=Convert.ToString(Purchase[i]);
							else
								Purchase_="";
						}
					}
					else
					{
						Purchase_="";
					}
					if(Sales.Count!=0)
					{
						if(Sales.Count<=i)
						{
							Sales_="";
						}
						else
						{
							if(Convert.ToString(Sales[i])!=null && Convert.ToString(Sales[i])!="")
								Sales_=Convert.ToString(Sales[i]);
							else
								Sales_="";
						}
					}
					else
					{
						Sales_="";
					}
				
					if(Receipt.Count!=0)
					{
						if(Receipt.Count<=i)
						{
							Receipt_="";
						}
						else
						{
							if(Convert.ToString(Receipt[i])!=null && Convert.ToString(Receipt[i])!="")
								Receipt_=Convert.ToString(Receipt[i]);
							else
								Receipt_="";
						}
					}
					else
					{
						Receipt_="";
					}
				
					if(Payment.Count!=0)
					{
						if(Payment.Count<=i)
						{
								Payment_="";
						}
						else
						{
							if(Convert.ToString(Payment[i])!=null && Convert.ToString(Payment[i])!="")
								Payment_=Convert.ToString(Payment[i]);
							else
								Payment_="";
						}
					}
					else
					{
						Payment_="";
					}
			
					sw.WriteLine(info,Purchase_,Sales_,Receipt_,Payment_);
				}
				sw.WriteLine("+----------+------------+----------+----------+");
			}
			//10.07.09 vikas dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			string Purchase_="",Sales_="",Payment_="",Receipt_="";
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\DocumentCancle_Report.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			/*11.07.09 vikas Purchase = new ArrayList();
			Sales = new ArrayList();
			Payment = new ArrayList();
			Receipt = new ArrayList();*/
			//11.07.09 vikas sql="select lr.Emp_ID,e.Emp_Name,lr.Date_From,lr.Date_To,lr.Reason,lr.isSanction from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			//11.07.09 vikas sql=sql+" order by "+Cache["strOrderBy"];
			

			string des="-----------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));

			//sw.WriteLine(GenUtil.GetCenterAddr("------------------------------",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("DOCUMENT CANCELLATION REPORT",des.Length));
			//sw.WriteLine(GenUtil.GetCenterAddr("------------------------------",des.Length));

			ArrayList counter=new ArrayList();
			   
			counter.Add(Purchase.Count.ToString());
			counter.Add(Sales.Count.ToString());
			counter.Add(Payment.Count.ToString());
			counter.Add(Receipt.Count.ToString());
			
			counter.Sort(); 
			
			int max=Convert.ToInt32(counter[3].ToString());

			sw.WriteLine("Purchase\tSales\tReceipt\tPayment");

			for(int i=0;i<max;i++)
			{
				if(Purchase.Count!=0)
				{
					if(Purchase.Count<=i)
					{
						Purchase_="";
					}
					else
					{
						if(Convert.ToString(Purchase[i])!=null && Convert.ToString(Purchase[i])!="")
							Purchase_=Convert.ToString(Purchase[i]);
						else
							Purchase_="";
					}
				}
				else
				{
					Purchase_="";
				}
				if(Sales.Count!=0)
				{
					if(Sales.Count<=i)
					{
						Sales_="";
					}
					else
					{
						if(Convert.ToString(Sales[i])!=null && Convert.ToString(Sales[i])!="")
							Sales_=Convert.ToString(Sales[i]);
						else
							Sales_="";
					}
				}
				else
				{
					Sales_="";
				}
				
				if(Receipt.Count!=0)
				{
					if(Receipt.Count<=i)
					{
						Receipt_="";
					}
					else
					{
						if(Convert.ToString(Receipt[i])!=null && Convert.ToString(Receipt[i])!="")
							Receipt_=Convert.ToString(Receipt[i]);
						else
							Receipt_="";
					}
				}
				else
				{
					Receipt_="";
				}
				
				if(Payment.Count!=0)
				{
					if(Payment.Count<=i)
					{
						Payment_="";
					}
					else
					{
						if(Convert.ToString(Payment[i])!=null && Convert.ToString(Payment[i])!="")
							Payment_=Convert.ToString(Payment[i]);
						else
							Payment_="";
					}
				}
				else
				{
					Payment_="";
				}
			
				sw.WriteLine(Purchase_+"\t"+Sales_+"\t"+Receipt_+"\t"+Payment_);
			}
			sw.Close();
			/*11.07.09 vikas rdr=obj.GetRecordSet(sql);
			sw.WriteLine("Employee ID\tEmployee Name\tFrom Date\tTo Date\tReason\tApproved");
			while(rdr.Read())
			{
				11.07.09 vikas sw.WriteLine(rdr["Emp_ID"].ToString()+"\t"+
					rdr["Emp_Name"].ToString()+"\t"+
					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_From"].ToString()))+"\t"+
					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_To"].ToString()))+"\t"+
					rdr["Reason"].ToString()+"\t"+
					Approved(rdr["isSanction"].ToString())
					);
			}
			sw.Close();*/
		}

		/// <summary>
		/// Prepares the excel report file DocumentCancelationReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			//10.07.09 vikas MessageBox.Show("This Task is Under Proccess");
			//10.07.09 vikas return;
			try
			{
				if(Flag==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Leave Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Leave Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}