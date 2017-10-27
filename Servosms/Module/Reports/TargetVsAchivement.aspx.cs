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
	/// Summary description for TargetVsAchivement.
	/// </summary>
	public partial class TargetVsAchivement : System.Web.UI.Page
	{
		public static int View = 0;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public double TotalSecSales = 0, TotalPrimarySales = 0,TotalSecSales1 = 0, TotalPrimarySales1 = 0,SecSales = 0, PrimarySales = 0,SecSales1 = 0, PrimarySales1 = 0;
		public double TargetSSA = 0,TargetLtr = 0, TargetAch = 0, TargetMinus = 0, Uplft = 0, Sales = 0;
		public double TargetLtr1 = 0, TargetAch1 = 0, TargetMinus1 = 0;
		string uid;
			
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
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
				CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				//string[] DiffMon={"Apr."+DropYearFrom.SelectedItem.Text,"Apr."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"May."+DropYearFrom.SelectedItem.Text,"May."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jun."+DropYearFrom.SelectedItem.Text,"Jun."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jul."+DropYearFrom.SelectedItem.Text,"Jul."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Aug."+DropYearFrom.SelectedItem.Text,"Aug."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Sep."+DropYearFrom.SelectedItem.Text,"Sep."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Oct."+DropYearFrom.SelectedItem.Text,"Oct."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Nov."+DropYearFrom.SelectedItem.Text,"Nov."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Dec."+DropYearFrom.SelectedItem.Text,"Dec."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jan."+DropYearTo.SelectedItem.Text,"Jan."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1),"Feb."+DropYearTo.SelectedItem.Text,"Feb."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1),"Mar."+DropYearTo.SelectedItem.Text,"Mar."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1)};
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="49";
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
				View = 0;
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
		/// This method is used to show the report.
		/// </summary>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
				View = 1;
			else if(DropYearFrom.SelectedIndex+1==DropYearTo.SelectedIndex)
				View = 1;
			else
			{
				View = 0;
				MessageBox.Show("Invalid Selection");
				return;
			}
		}

		/// <summary>
		/// this method return the total primary sales in liter of given period.
		/// </summary>
		public string GetPrimarySales(string Month)
		{
			try
			{
				SqlDataReader rdr1 = null;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(pd.qty*p.Total_Qty) from products p,purchase_master pm,purchase_details pd where p.Prod_id=pd.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
					else
					{
						//dbobj.SelectQuery("select total_purchase from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'",ref rdr1);
						rdr1 = obj1.GetRecordSet("select total_purchase from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'");
						if(rdr1.Read())
						{
							if(rdr1.GetValue(0).ToString()!="")
								Tot = double.Parse(rdr1.GetValue(0).ToString());
						}
						rdr1.Close();
					}
				}
				rdr.Close();
				PrimarySales=Tot;
				return System.Convert.ToString(Math.Round(Tot));
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetPrimaryLubeSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0";
			}
		}

		/// <summary>
		/// this method return the total primary sales in liter of given period.
		/// </summary>
		public string GetPrimarySales1(string Month)
		{
			try
			{
				SqlDataReader rdr1 = null;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(pd.qty*p.Total_Qty) from products p,purchase_master pm,purchase_details pd where p.Prod_id=pd.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
					else
					{
						//dbobj.SelectQuery("select total_purchase from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'",ref rdr1);
						rdr1 = obj1.GetRecordSet("select total_purchase from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'");
						if(rdr1.Read())
						{
							if(rdr1.GetValue(0).ToString()!="")
								Tot = double.Parse(rdr1.GetValue(0).ToString());
						}
						rdr1.Close();
					}
				}
				rdr.Close();
				PrimarySales1=Tot;
				return System.Convert.ToString(Math.Round(Tot));
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetPrimaryLubeSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0";
			}
		}

		/// <summary>
		/// this method return the total sales in liter of given period.
		/// </summary>
		public string GetSecSales(string Month)
		{
			try
			{
				SqlDataReader rdr1 = null;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(sd.qty*p.Total_Qty) from products p,sales_master sm,sales_details sd where p.Prod_id=sd.prod_id and sm.invoice_no=sd.invoice_no and cast(floor(cast(cast(Invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
					else
					{
						//dbobj.SelectQuery("select total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'",ref rdr1);
						rdr1 = obj1.GetRecordSet("select total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'");
						if(rdr1.Read())
						{
							Tot = double.Parse(rdr1.GetValue(0).ToString());
						}
						rdr1.Close();
					}
				}
				
				rdr.Close();
				SecSales=Tot;
				return System.Convert.ToString(Math.Round(Tot));
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetSecLubeSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0";
			}
		}

		/// <summary>
		/// this method return the total sales in liter of given period.
		/// </summary>
		public string GetSecSales1(string Month)
		{
			try
			{
				SqlDataReader rdr1=null;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(sd.qty*p.Total_Qty) from products p,sales_master sm,sales_details sd where p.Prod_id=sd.prod_id and sm.invoice_no=sd.invoice_no and cast(floor(cast(cast(Invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
					else
					{
						//dbobj.SelectQuery("select total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'",ref rdr1);
						rdr1 = obj1.GetRecordSet("select total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'");
						if(rdr1.Read())
						{
							if(rdr1.GetValue(0).ToString()!="")
								Tot = double.Parse(rdr1.GetValue(0).ToString());
						}
						rdr1.Close();
						//InventoryClass obj=new InventoryClass ();
						
					}
				}
				rdr.Close();
				SecSales1=Tot;
				return System.Convert.ToString(Math.Round(Tot));
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetSecLubeSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0";
			}
		}

		/// <summary>
		/// this method return the date, create a date with the help of given month and year.
		/// </summary>
		public string GetDate(string MonYear)
		{
			string[] DT = MonYear.Split(new char[] {'.'},MonYear.Length);
			int Mon = GetMonth(DT[0].ToString());
			int Day = DateTime.DaysInMonth(int.Parse(DT[1].ToString()),Mon);
			return Mon.ToString()+"/1/"+DT[1]+":"+Mon.ToString()+"/"+Day.ToString()+"/"+DT[1];
		}

		/// <summary>
		/// This method return the month no of given month
		/// </summary>
		public int GetMonth(string Mon)
		{
			if(Mon == "Apr")
				return 4;
			else if(Mon == "May")
				return 5;
			else if(Mon == "Jun")
				return 6;
			else if(Mon == "Jul")
				return 7;
			else if(Mon == "Aug")
				return 8;
			else if(Mon == "Sep")
				return 9;
			else if(Mon == "Oct")
				return 10;
			else if(Mon == "Nov")
				return 11;
			else if(Mon == "Dec")
				return 12;
			else if(Mon == "Jan")
				return 1;
			else if(Mon == "Feb")
				return 2;
			else
				return 3;
		}

		/// <summary>
		/// Prepares the report file TargetVsAchievement.txt for printing.
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
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
					CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    TargetVsAchivement Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\TargetVsAchivement.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    TargetVsAchivement Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    TargetVsAchivement Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    TargetVsAchivement Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    TargetVsAchivement Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file TargetVsAchiement.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View == 1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   TargetVsAchivement Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:TargetVsAchivement.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    TargetVsAchivement Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\TargetVsAchivement.xls";
			StreamWriter sw = new StreamWriter(path);
			string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"Apr."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"May."+DropYearFrom.SelectedItem.Text,"May."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jun."+DropYearFrom.SelectedItem.Text,"Jun."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jul."+DropYearFrom.SelectedItem.Text,"Jul."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Aug."+DropYearFrom.SelectedItem.Text,"Aug."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Sep."+DropYearFrom.SelectedItem.Text,"Sep."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Oct."+DropYearFrom.SelectedItem.Text,"Oct."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Nov."+DropYearFrom.SelectedItem.Text,"Nov."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Dec."+DropYearFrom.SelectedItem.Text,"Dec."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jan."+DropYearTo.SelectedItem.Text,"Jan."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1),"Feb."+DropYearTo.SelectedItem.Text,"Feb."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1),"Mar."+DropYearTo.SelectedItem.Text,"Mar."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1)};
		
			TotalPrimarySales = 0;
			TotalSecSales = 0;
			TargetSSA = 0;
			TargetLtr = 0;
			TargetAch = 0;
			TargetMinus = 0;
			TargetLtr1 = 0;
			TargetAch1 = 0;
			TargetMinus1 = 0;
			Uplft = 0;
			Sales = 0;
			sw.WriteLine("FY\tTotal\tTotal\tTarget\tNegetive\tTarget\tTarget\tFY\tTotal\tTotal\tTarget\tNegetive\tTarget\tTarget\tNegetive in Ly Vs Cy");
			sw.WriteLine(DropYearFrom.SelectedItem.Text+"-"+DropYearTo.SelectedItem.Text+"\tSales\tPurch.\tSSA\tin Target\tAch.\tMinus\t"+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1)+"-"+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1)+"\tSales\tPurch.\tSSA\tin Target\tAch.\tMinus\tSales\tUplft");
			sw.WriteLine("\tSec.\tPrimary\t\tin Ltr\tin %\tin %\t\tSec.\tPrimary\t\tin Ltr\tin %\tin %\tIN LITERS");
			for(int i=0;i<Mon.Length;i++)
			{
				PrimarySales = 0;
				PrimarySales1 = 0;
				SecSales = 0;
				SecSales1 = 0;
				sw.WriteLine(Mon[i].ToString()+"\t"+
					GetSecSales(Mon[i].ToString())+"\t"+
					GetPrimarySales(Mon[i].ToString())+"\t"+
					txtTargetSSA.Text+"\t"+
					Math.Round(PrimarySales-double.Parse(txtTargetSSA.Text))+"\t"+
					Math.Round((PrimarySales/double.Parse(txtTargetSSA.Text))*100)+"\t"+
					Math.Round(((PrimarySales-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100)+"\t"+
					Mon[++i].ToString()+"\t"+
					GetSecSales1(Mon[i].ToString())+"\t"+
					GetPrimarySales1(Mon[i].ToString())+"\t"+
					txtTargetSSA.Text+"\t"+
					Math.Round(PrimarySales1-double.Parse(txtTargetSSA.Text))+"\t"+
					Math.Round((PrimarySales1/double.Parse(txtTargetSSA.Text))*100)+"\t"+
					Math.Round(((PrimarySales1-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100)+"\t"+
					Math.Round(SecSales-SecSales1)+"\t"+
					Math.Round(PrimarySales-PrimarySales1));
				TargetSSA+=double.Parse(txtTargetSSA.Text);
				TargetLtr+=PrimarySales-double.Parse(txtTargetSSA.Text);
				TargetAch+=(PrimarySales/double.Parse(txtTargetSSA.Text))*100;
				TargetMinus+=((PrimarySales-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100;
				TargetLtr1+=PrimarySales1-double.Parse(txtTargetSSA.Text);
				TargetAch1+=(PrimarySales1/double.Parse(txtTargetSSA.Text))*100;
				TargetMinus1+=((PrimarySales1-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100;
				TotalPrimarySales+=PrimarySales;
				TotalPrimarySales1+=PrimarySales1;
				TotalSecSales+=SecSales;
				TotalSecSales1+=SecSales1;
				Uplft+=PrimarySales-PrimarySales1;
				Sales+=SecSales-SecSales1;
		
			}
			sw.WriteLine("Total"+"\t"+Math.Round(TotalSecSales)+"\t"+Math.Round(TotalPrimarySales)+"\t"+TargetSSA.ToString()+"\t"+Math.Round(TargetLtr)+"\t"+Math.Round(TargetAch)+"\t"+Math.Round(TargetMinus)+"\t"+"Total"+"\t"+Math.Round(TotalSecSales1)+"\t"+Math.Round(TotalPrimarySales1)+"\t"+TargetSSA.ToString()+"\t"+Math.Round(TargetLtr1)+"\t"+Math.Round(TargetAch1)+"\t"+Math.Round(TargetMinus1)+"\t"+Math.Round(Sales)+"\t"+Math.Round(Uplft));
			sw.Close();
		}

		public void makingReport()
		{
		}
	}
}