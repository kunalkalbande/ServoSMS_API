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
	/// Summary description for PrimarySecSalesAnalysis.
	/// </summary>
	public partial class PrimarySecSalesAnalysis : System.Web.UI.Page
	{
		public static int View = 0;
		//double Totalltr = new double[12];
		public double TotalSecSales = 0, TotalPrimarySales = 0,SecSales = 0, PrimarySales = 0;
		string uid;
	
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
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:PrimarysecSalesanalysis.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
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
				string SubModule="28";
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
				CreateLogFiles.ErrorLog("Form:PrimarysecSalesanalysis.aspx,Method:Page_Load  userid "+  uid);
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
		/// This method is used to view the report and set the value in static variable
		/// if 1 then view the report otherwise show the popup 'Data Not Available'.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
				View = 1;
			else if(DropYearFrom.SelectedIndex+1==DropYearTo.SelectedIndex)
				View = 1;
			else
			{
				MessageBox.Show("Invalid Year Selection");
				View = 0;
			}
			CreateLogFiles.ErrorLog("Form:PrimarysecSalesanalysis.aspx,Method:btnView_Click, View the Primary SacSales Analysis Report,    userid "+  uid);
		}

		/// <summary>
		/// This method return the total primary sales in liter of given period.
		/// </summary>
		public string GetPrimaryLubeSales(string Month)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader rdr1 = null;
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
						rdr1 = obj1.GetRecordSet("select total_Purchase from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'");
						if(rdr1.Read())
						{
							if(rdr1.GetValue(0).ToString()!="")
								Tot = double.Parse(rdr1.GetValue(0).ToString());
						}
						rdr1.Close();
					}
				}
				rdr.Close();
				TotalPrimarySales=Tot;
				return GenUtil.strNumericFormat(Tot.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetPrimaryLubeSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0.00";
			}
		}

		/// <summary>
		/// This method return the total primary sales in liter of given period.
		/// </summary>
		public string GetPrimarySales(string Cat,string Month)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(pd.qty*p.Total_Qty) from products p,purchase_master pm,purchase_details pd where p.Prod_id=pd.prod_id and pm.invoice_no=pd.invoice_no and p.category like '"+Cat+"%' and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				//TotalPrimarySales+=Tot;
				return GenUtil.strNumericFormat(Tot.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetPrimarySales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0.00";
			}
		}

		/// <summary>
		/// This method return the total primary sales in liter of given period.
		/// </summary>
		public string GetPrimarySalesGen(string Month)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(pd.qty*p.Total_Qty) from products p,purchase_master pm,purchase_details pd where p.Prod_id=pd.prod_id and pm.invoice_no=pd.invoice_no and (p.category = 'Maruti oil' or p.category = 'Swaraj' or p.category = 'Htm') and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(pm.invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				//TotalPrimarySales+=Tot;
				return GenUtil.strNumericFormat(Tot.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetPrimarySales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0.00";
			}
		}

		/// <summary>
		/// This method return the total secondary sales in liter of given period.
		/// </summary>
		public string GetSecLubeSales(string Month)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader rdr1 = null;
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
						rdr1 = obj1.GetRecordSet("select total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and month='"+Month+"'");
						if(rdr1.Read())
						{
							if(rdr1.GetValue(0).ToString()!="")
								Tot = double.Parse(rdr1.GetValue(0).ToString());
						}
						rdr1.Close();
					}
				}
				rdr.Close();
				TotalSecSales=Tot;
				return GenUtil.strNumericFormat(Tot.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetSecLubeSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0.00";
			}
		}

		/// <summary>
		/// This method return the total secondary sales in liter of given period.
		/// </summary>
		public string GetSecSales(string Cat,string Month)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(sd.qty*p.Total_Qty) from products p,sales_master sm,sales_details sd where p.Prod_id=sd.prod_id and sm.invoice_no=sd.invoice_no and p.category like '"+Cat+"%' and cast(floor(cast(cast(Invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				//TotalSecSales+=Tot;
				return GenUtil.strNumericFormat(Tot.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetSecSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0.00";
			}
		}

		/// <summary>
		/// This method return the total secondary sales in liter in given period
		/// </summary>
		public string GetSecSalesGen(string Month)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				double Tot = 0;
				string DT = GetDate(Month);
				string[] FTDT = DT.Split(new char[] {':'},DT.Length);
				SqlDataReader rdr = obj.GetRecordSet("select sum(sd.qty*p.Total_Qty) from products p,sales_master sm,sales_details sd where p.Prod_id=sd.prod_id and sm.invoice_no=sd.invoice_no and (p.category = 'Maruti oil' or p.category = 'Swaraj' or p.category = 'Htm') and cast(floor(cast(cast(Invoice_date as datetime) as float)) as datetime)>='"+FTDT[0]+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+FTDT[1]+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						Tot = double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				//TotalSecSales+=Tot;
				return GenUtil.strNumericFormat(Tot.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecSalesAnalysis.aspx,Method:GetSecSales ,  EXCEPTION "+ ex.Message+", userid "+  uid);
				return "0.00";
			}
		}

		/// <summary>
		/// This method is used to return the starting date of the given month.
		/// </summary>
		public string GetDate(string MonYear)
		{
			string[] DT = MonYear.Split(new char[] {'.'},MonYear.Length);
			int Mon = GetMonth(DT[0].ToString());
			int Day = DateTime.DaysInMonth(int.Parse(DT[1].ToString()),Mon);
			return Mon.ToString()+"/1/"+DT[1]+":"+Mon.ToString()+"/"+Day.ToString()+"/"+DT[1];
		}

		/// <summary>
		/// This method return the month no of given month name.
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
			else if(Mon == "jan")
				return 1;
			else if(Mon == "Feb")
				return 2;
			else
				return 3;
		}

		/// <summary>
		/// This method is used to prepares the report file PrimarySecSalesAnalysis.txt for printing.
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
					CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PrimarySecAnalysis Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PrimarySecAnalysis.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PrimarySecAnalysis Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PrimarySecAnalysis Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PrimarySecAnalysis Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PrimarySecAnalysis Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the report file PrimarySecSalesAnalysys.txt for printing.
		/// </summary>
		public void makingReport()
		{
			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PrimarySecAnalysis.txt";
				StreamWriter sw = new StreamWriter(path);
				string info = "";
				
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
				string des="----------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("===================================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Primary Secondary Sales Analysis Report From Year "+DropYearFrom.SelectedItem.Text+" To Year "+DropYearTo.SelectedItem.Text,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("===================================================================",des.Length));
				//sw.WriteLine(" From Year : "+DropYearFrom.SelectedItem.Text+", To Year : "+DropYearTo.SelectedItem.Text);
				sw.WriteLine("+----------+-------------------------------------------------------------+-------------------------------------------------------------+");
				sw.WriteLine("|          |                       Primary Sales                         |                      Secondary Sales                        |");
				sw.WriteLine("+----------+----------+---------+---------+---------+---------+----------+----------+---------+---------+---------+---------+----------+");
				sw.WriteLine("|  Month   |   Lube   |   2T    |   4T    | Grease  |gen. Oils|  Total   |   Lube   |   2T    |   4T    | Grease  |gen. Oils|  Total   |");
				sw.WriteLine("+----------+----------+---------+---------+---------+---------+----------+----------+---------+---------+---------+---------+----------+");
				//             1234567890 123456789 123456789 123456789 123456789 123456789 1234567890 123456789 123456789 123456789 123456789 123456789 1234567890 
				info = " {0,-10:S} {1,10:F} {2,9:S} {3,9:S} {4,9:S} {5,9:S} {6,10:S} {7,10:F} {8,9:S} {9,9:S} {10,9:S} {11,9:S} {12,10:S}";
				string[] DiffMon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
				string[] SameMon={"Jan."+DropYearFrom.SelectedItem.Text,"Feb."+DropYearFrom.SelectedItem.Text,"Mar."+DropYearFrom.SelectedItem.Text,"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text};
				double[] TotalAmt = new double[8];
				int k=0;
				SecSales=0;
				PrimarySales=0;
				if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
				{
					for(int i=0;i<SameMon.Length;i++)
					{
						TotalPrimarySales=0;
						TotalSecSales=0;
						k=0;
						sw.WriteLine(info,SameMon[i].ToString(),GetPrimaryLubeSales(SameMon[i].ToString()),GetPrimarySales("2T",SameMon[i].ToString()),GetPrimarySales("4T",SameMon[i].ToString()),GetPrimarySales("Grease",SameMon[i].ToString()),GetPrimarySalesGen(SameMon[i].ToString()),GenUtil.strNumericFormat(TotalPrimarySales.ToString()),GetSecLubeSales(SameMon[i].ToString()),GetSecSales("2T",SameMon[i].ToString()),GetSecSales("4T",SameMon[i].ToString()),GetSecSales("Grease",SameMon[i].ToString()),GetSecSalesGen(SameMon[i].ToString()),GenUtil.strNumericFormat(TotalSecSales.ToString()));
						PrimarySales+=TotalPrimarySales;
						SecSales+=TotalSecSales;
						TotalAmt[k]+=double.Parse(GetPrimarySales("2T",SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetPrimarySales("4T",SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetPrimarySales("Grease",SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetPrimarySalesGen(SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSales("2T",SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSales("4T",SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSales("Grease",SameMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSalesGen(SameMon[i].ToString()));
					}
				}
				else
				{
					for(int i=0;i<SameMon.Length;i++)
					{
						TotalPrimarySales=0;
						TotalSecSales=0;
						k=0;
						sw.WriteLine(info,DiffMon[i].ToString(),GetPrimaryLubeSales(DiffMon[i].ToString()),GetPrimarySales("2T",DiffMon[i].ToString()),GetPrimarySales("4T",DiffMon[i].ToString()),GetPrimarySales("Grease",DiffMon[i].ToString()),GetPrimarySalesGen(DiffMon[i].ToString()),GenUtil.strNumericFormat(TotalPrimarySales.ToString()),GetSecLubeSales(DiffMon[i].ToString()),GetSecSales("2T",DiffMon[i].ToString()),GetSecSales("4T",DiffMon[i].ToString()),GetSecSales("Grease",DiffMon[i].ToString()),GetSecSalesGen(DiffMon[i].ToString()),GenUtil.strNumericFormat(TotalSecSales.ToString()));
						PrimarySales+=TotalPrimarySales;
						SecSales+=TotalSecSales;
						TotalAmt[k]+=double.Parse(GetPrimarySales("2T",DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetPrimarySales("4T",DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetPrimarySales("Grease",DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetPrimarySalesGen(DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSales("2T",DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSales("4T",DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSales("Grease",DiffMon[i].ToString()));
						TotalAmt[++k]+=double.Parse(GetSecSalesGen(DiffMon[i].ToString()));
					}
				}
				k=0;
				sw.WriteLine("+----------+----------+---------+---------+---------+---------+----------+----------+---------+---------+---------+---------+----------+");
				sw.WriteLine(info,"  Total",GenUtil.strNumericFormat(PrimarySales.ToString()),GenUtil.strNumericFormat(TotalAmt[k].ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(PrimarySales.ToString()),GenUtil.strNumericFormat(SecSales.ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(TotalAmt[++k].ToString()),GenUtil.strNumericFormat(SecSales.ToString()));
				sw.WriteLine("+----------+----------+---------+---------+---------+---------+----------+----------+---------+---------+---------+---------+----------+");
				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\PrimarySecAnalysis.xls";
			StreamWriter sw = new StreamWriter(path);
			string[] DiffMon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
			string[] SameMon={"Jan."+DropYearFrom.SelectedItem.Text,"Feb."+DropYearFrom.SelectedItem.Text,"Mar."+DropYearFrom.SelectedItem.Text,"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text};
			double[] TotalAmt = new double[8];
			sw.WriteLine("From Year\t"+DropYearFrom.SelectedItem.Text+"\tTo Year\t"+DropYearTo.SelectedItem.Text);
			sw.WriteLine("Month\tPrimary Sales\t\t\t\t\t\tSecondary Sales");
			sw.WriteLine("\tLube\t2T\t4T\tGrease\tGen. Oils\tTotal\tLube\t2T\t4T\tGrease\tGen. Oils\tTotal");
			int k=0;
			SecSales=0;
			PrimarySales=0;
			if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
			{
				for(int i=0;i<SameMon.Length;i++)
				{
					TotalPrimarySales=0;
					TotalSecSales=0;
					k=0;
					sw.WriteLine(SameMon[i].ToString()+"\t"+GetPrimaryLubeSales(SameMon[i].ToString())+"\t"+GetPrimarySales("2T",SameMon[i].ToString())+"\t"+GetPrimarySales("4T",SameMon[i].ToString())+"\t"+GetPrimarySales("Grease",SameMon[i].ToString())+"\t"+GetPrimarySalesGen(SameMon[i].ToString())+"\t"+GenUtil.strNumericFormat(TotalPrimarySales.ToString())+"\t"+GetSecLubeSales(SameMon[i].ToString())+"\t"+GetSecSales("2T",SameMon[i].ToString())+"\t"+GetSecSales("4T",SameMon[i].ToString())+"\t"+GetSecSales("Grease",SameMon[i].ToString())+"\t"+GetSecSalesGen(SameMon[i].ToString())+"\t"+GenUtil.strNumericFormat(TotalSecSales.ToString()));
					PrimarySales+=TotalPrimarySales;
					SecSales+=TotalSecSales;
					TotalAmt[k]+=double.Parse(GetPrimarySales("2T",SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetPrimarySales("4T",SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetPrimarySales("Grease",SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetPrimarySalesGen(SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSales("2T",SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSales("4T",SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSales("Grease",SameMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSalesGen(SameMon[i].ToString()));
				}
			}
			else
			{
				for(int i=0;i<SameMon.Length;i++)
				{
					TotalPrimarySales=0;
					TotalSecSales=0;
					k=0;
					sw.WriteLine(DiffMon[i].ToString()+"\t"+GetPrimaryLubeSales(DiffMon[i].ToString())+"\t"+GetPrimarySales("2T",DiffMon[i].ToString())+"\t"+GetPrimarySales("4T",DiffMon[i].ToString())+"\t"+GetPrimarySales("Grease",DiffMon[i].ToString())+"\t"+GetPrimarySalesGen(DiffMon[i].ToString())+"\t"+GenUtil.strNumericFormat(TotalPrimarySales.ToString())+"\t"+GetSecLubeSales(DiffMon[i].ToString())+"\t"+GetSecSales("2T",DiffMon[i].ToString())+"\t"+GetSecSales("4T",DiffMon[i].ToString())+"\t"+GetSecSales("Grease",DiffMon[i].ToString())+"\t"+GetSecSalesGen(DiffMon[i].ToString())+"\t"+GenUtil.strNumericFormat(TotalSecSales.ToString()));
					PrimarySales+=TotalPrimarySales;
					SecSales+=TotalSecSales;
					TotalAmt[k]+=double.Parse(GetPrimarySales("2T",DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetPrimarySales("4T",DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetPrimarySales("Grease",DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetPrimarySalesGen(DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSales("2T",DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSales("4T",DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSales("Grease",DiffMon[i].ToString()));
					TotalAmt[++k]+=double.Parse(GetSecSalesGen(DiffMon[i].ToString()));
				}
			}
			k=0;
			sw.WriteLine("Total\t"+GenUtil.strNumericFormat(PrimarySales.ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[k].ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(PrimarySales.ToString())+"\t"+GenUtil.strNumericFormat(SecSales.ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(TotalAmt[++k].ToString())+"\t"+GenUtil.strNumericFormat(SecSales.ToString()));
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file PrimariSecSalesAnalysis.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View == 1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   PrimarySecAnalysis Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:PrimarySecAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    PrimarySecAnalysis Report Viewed on Excel Sheet  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}