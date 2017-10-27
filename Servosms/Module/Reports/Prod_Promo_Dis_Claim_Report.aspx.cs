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
using System.Data.SqlClient;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using RMG;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for Prod_Promo_Dis_Claim_Report.
	/// </summary>
	public partial class Prod_Promo_Dis_Claim_Report : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		public static int flage=0;
		public double Qty_Tot=0;
		public double Qty_Tot_ltr=0;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Prod_Promo_Dis_Claim_Report.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="44";
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
				flage=0;
				txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				txtDateTo.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				
				InventoryClass obj=new InventoryClass();
				SqlDataReader dtr=null; 
				string sql="select distinct schname from Prod_Promo_Grade_Entry";
				dtr=obj.GetRecordSet(sql);
				DropSchemName.Items.Clear();
				DropSchemName.Items.Add("All");
				while(dtr.Read())
				{
					DropSchemName.Items.Add(dtr.GetValue(0).ToString());
				}
				dtr.Close();
							
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

		protected void btnview_Click(object sender, System.EventArgs e)
		{
			Qty_Tot_ltr=0;
			flage=1;
			
		}

		public string Qtyinltr(string type, string qty)
		{
			
			string[] p_name=type.Split(new char[] {':'});
			string[] p_type=p_name[1].Split(new char[] {'X'});
			double type1=Convert.ToDouble(p_type[0].ToString())*Convert.ToDouble(p_type[1].ToString());
			double total_qty=type1*Convert.ToDouble(qty.ToString());
			Qty_Tot_ltr+=total_qty;
			return Convert.ToString((total_qty));

		}

		protected void btnprint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
				if(flage==1)
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\Prod_Promo_Dis_Claim_Report.txt<EOF>");

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
				CreateLogFiles.ErrorLog("Form:Prod_Promo_Dis_Claim_Report.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSR Perfomance Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		public void makingReport()
		{
			Qty_Tot_ltr=0;
			InventoryClass obj = new InventoryClass();
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\Prod_Promo_Dis_Claim_Report.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			//05.06.09 sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where datefrom>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and dateto<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"') order by p.prod_name,p.pack_type";
			if(DropSchemName.SelectedIndex==0)
				sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where datefrom>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and dateto<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"') order by p.prod_name,p.pack_type";
			else
				sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where schname='"+DropSchemName.SelectedItem.Value.ToString()+"' and datefrom>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and dateto<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"') order by p.prod_name,p.pack_type";

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
			string des="--------------------------------------------------------------------------------------------------------------";
			sw.WriteLine(des);
			//******S***
			sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Product Promotion Scheme Discount Claim Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
			//sw.WriteLine("From Date : "+txtDateFrom.Text+", To Date : "+txtDateTo.Text);
			sw.WriteLine("+--------------------+------------+--------------+------------------------------+------------+------------+");
			sw.WriteLine("|     Vendor Name    |Invoice No. | Invoice Date |      Product Name            | Qty in Nos.| Qty in ltr.|");
			sw.WriteLine("+--------------------+------------+--------------+------------------------------+------------+------------+");
			//             12345678901234567890 123456789012 12345678901234 123456789012345678901234567890 123456789012 123456789012
			info = " {0,-20:S} {1,12:F} {2,14:S} {3,-30:S} {4,12:S} {5,12:S} ";
			string info1 = " {0,-20:S} {1,12:F} {2,14:S} {3,-30:S} {4,12:S} {5,12:S} ";

			while(rdr.Read())
			{
				sw.WriteLine(info,
					rdr.GetValue(0).ToString(),
					rdr.GetValue(1).ToString(),
					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr.GetValue(2).ToString())),
					rdr.GetValue(3).ToString(),rdr.GetValue(4).ToString(),
					Qtyinltr(rdr.GetValue(3).ToString(),rdr.GetValue(4).ToString()));
					Qty_Tot+=Convert.ToDouble(rdr.GetValue(4).ToString());

			}
			rdr.Close();
			sw.WriteLine("+--------------------+------------+--------------+------------------------------+------------+------------+");
			sw.WriteLine(info1,"Total","","","",Qty_Tot.ToString(),Qty_Tot_ltr.ToString());
			sw.WriteLine("+--------------------+------------+--------------+------------------------------+------------+------------+");
			sw.Close();
		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{			
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:Prod_Promo_Dis_Claim_Report.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   PurchaseBook Report Convert Into Excel Format, userid  "+uid);
				
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:Prod_Promo_Dis_Claim_Report.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    PurchaseBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public void ConvertToExcel()
		{
			InventoryClass obj = new InventoryClass();
			System.Data.SqlClient.SqlDataReader rdr=null;
						string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\Prod_Promo_Dis_Claim_Report.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			//05.06.09 sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where datefrom>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and dateto<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"') order by p.prod_name,p.pack_type";

			if(DropSchemName.SelectedIndex==0)
				sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where datefrom>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])+"' and dateto<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"])+"') order by p.prod_name,p.pack_type";
			else
				sql="select sp.supp_name,pm.vndr_invoice_no,pm.vndr_invoice_date,p.prod_name+':'+p.pack_type,pd.qty from supplier sp,purchase_master pm,purchase_details pd,products p where pm.invoice_no=pd.invoice_no and sp.supp_id=pm.vendor_id and p.prod_id=pd.prod_id  and pm.vndr_invoice_date>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"' and pm.vndr_invoice_date<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"]) +"' and pd.prod_id in (select prodid from Prod_Promo_Grade_Entry where schname='"+DropSchemName.SelectedItem.Value.ToString()+"' and datefrom>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"' and dateto<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"]) +"') order by p.prod_name,p.pack_type";


			rdr = obj.GetRecordSet(sql);
			string des="--------------------------------------------------------------------------------------------------------------";
			sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Product Promotion Scheme Discount Claim Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
			//sw.WriteLine("From Date : "+txtDateFrom.Text+", To Date : "+txtDateTo.Text);
			sw.WriteLine("Vendor Name\tInvoice No.\tInvoice Date\tProduct Name\tQty in Nos.\tQty in ltr.");
			sw.WriteLine();
			while(rdr.Read())
			{
				sw.WriteLine(rdr.GetValue(0).ToString()+"\t"+
					rdr.GetValue(1).ToString()+"\t"+
					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr.GetValue(2).ToString()))+"\t"+
					rdr.GetValue(3).ToString()+"\t"+rdr.GetValue(4).ToString()+"\t"+
					Qtyinltr(rdr.GetValue(3).ToString(),rdr.GetValue(4).ToString()));
				Qty_Tot+=Convert.ToDouble(rdr.GetValue(4).ToString());

			}
			rdr.Close();
			sw.WriteLine();
			sw.WriteLine("Total"+"\t"+""+"\t"+""+"\t"+""+"\t"+Qty_Tot.ToString()+"\t"+Qty_Tot_ltr.ToString());
			sw.WriteLine();
			sw.Close();
		}
	}
}
