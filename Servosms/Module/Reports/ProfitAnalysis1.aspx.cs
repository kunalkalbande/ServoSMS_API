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
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using Servosms.Sysitem.Classes ;
using System.IO;
using System.Text;
using RMG;
namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for ProfitAnalysis1.
	/// </summary>
	public partial class ProfitAnalysis1 : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.TextBox txtDateTo1;
		protected System.Web.UI.WebControls.TextBox txtDateFrom1;
		protected System.Web.UI.WebControls.Button btnview1;
		protected System.Web.UI.WebControls.Button BtnPrint1;
		string uid="";
		public static string eicher1="0";
		public static string force1="0";
		public static string ibp1="0";
		public static double grandtotal=0;

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
				CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				try
				{
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="32";
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
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					//System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Class:DBOperation_LETEST.cs,Method:page_load().  EXCEPTION: "+ ex.Message+" User_ID: "+uid);	
				}
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
		
		// This Method multiplies the package quantity with Quantity.
		double count=1,i=1,j=2,k=3,l=4;
		public static double os=0,cs=0,sales=0,rect=0;
		
		protected double Multiply(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(count==i)
				{
					os+=ans;
					i+=4;
				}
				if(count==j)
				{
					rect+=ans;
					j+=4;
				}
				if(count==k)
				{
					sales+=ans;
					k+=4;
				}
				if(count==l)
				{
					cs+=ans;
					l+=4;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
					return System.Convert.ToDouble( mystr[0].ToString()); 
				else
					return 0;
			}
			
		}

		double countsales=1,isales=3;
		public double sales1=0;
		// This Method is to sum sales.
		protected double Multiply1(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(countsales==isales)
				{
					sales1+=ans;
					isales+=4;
				}
				countsales++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
					return System.Convert.ToDouble( mystr[0].ToString()); 
				else
					return 0;
			}
			
		}

		double countforce=1,iforce=2;
		public double force=0;
		//This is used sum force type sales.
		protected double Multiply3(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(countforce==iforce)
				{
					force+=ans;
					iforce+=4;
				}
				countforce++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
					return System.Convert.ToDouble( mystr[0].ToString()); 
				else
					return 0;
			}
			
		}

		double counteicher=1,ieicher=2;
		public double eicher=0;
		//This is used sum eicher type sales.
		protected double Multiply2(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(counteicher==ieicher)
				{
					eicher+=ans;
					ieicher+=4;
				}
				counteicher++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
					return System.Convert.ToDouble( mystr[0].ToString()); 
				else
					return 0;
			}
			
		}
		
		private DateTime getdate(string dat,bool to)
		{
			
			string[] dt=dat.IndexOf("/")>0?dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}

		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			try
			{
				os=0;cs=0;sales=0;rect=0;
				show();
				CreateLogFiles.ErrorLog("Form:ProfitAnalysis.aspx,Class:DBOperation_LETEST.cs,Method:cmdrpt_Click  Profit Analysis Report  Viewed  useried "+uid);	
			
			}
			catch(Exception ex)
			{
				
				CreateLogFiles.ErrorLog("Form:ProfitAnalysis.aspx,Class:DBOperation_LETEST.cs,Method:cmdrpt_Click,  Profit Analysis Report  Viewed  EXCEPTION "+ ex.Message+"  userid  "+uid);		
			}
		}

		//This is used to show the data in report.
		public void show()
		{
			grdLeg.Visible=true;
			int x=0;
			object op=null;	
	
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select distinct productid from stock_master";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			while(rdr.Read())
				dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@strfromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@strtodate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
			rdr.Close();
			dbobj.SelectQuery("select * from stkmv",ref rdr);
			if(rdr.HasRows)
			{
				grdLeg.DataSource=rdr;
				grdLeg.DataBind();
				grdLeg.Visible=false;
			}
			//***********************
			rdr.Close();
				
			dbobj.SelectQuery("select * from stkmv where category='Ibp grade'",ref rdr);
			if(rdr.HasRows)
			{
				Datagrid1.DataSource=rdr;
				Datagrid1.DataBind();
				Datagrid1.Visible=false;
			}
			rdr.Close();
			dbobj.SelectQuery("select * from stkmv where category='Eicher grade'",ref rdr);
			if(rdr.HasRows)
			{
				Datagrid2.DataSource=rdr;
				Datagrid2.DataBind();
				Datagrid2.Visible=false;
			}
			rdr.Close();
			dbobj.SelectQuery("select * from stkmv where category='Force grade'",ref rdr);
			if(rdr.HasRows)
			{
				Datagrid3.DataSource=rdr;
				Datagrid3.DataBind();
				Datagrid3.Visible=false;
			}
			if(eicher!=0)
				eicher1="-"+eicher;
			if(force!=0)
				force1="-"+force;
			if(sales1!=0)
				ibp1="-"+sales1;
			grandtotal=sales-sales1-eicher-force;
			//***********************
			// truncate table after use.
			dbobj.Insert_or_Update("truncate table stkmv", ref x);
		}

		// check if the products type is fuel or package is Loose Oil then return space.
		protected string Check(string cs, string type,string pack)
		{
			if(type.ToUpper().Equals("FUEL") || pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				return cs;
		}

		// if the tank the returns the tank abbrevation name, for that tank.
		protected string IsTank(string str)
		{
			string op="";
			if(Char.IsDigit(str,0))
				dbobj.SelectQuery("select top 1 prod_abbname from tank where tank_id='"+str+"'","prod_abbname",ref op);
			if(op.Length>0)
				return op;
			else
				return str;
		}

		//This is used to make the report.
		public void makingReport()
		{
			show();
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ProfitAnalysisReport.txt";
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
			string des="----------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("====================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("PROFIT ANALYSIS REPORT From "+txtDateFrom.Text.ToString()+" To "+txtDateTo.Text.ToString(),des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("====================================================",des.Length));
			sw.WriteLine("                                                                ");
			sw.WriteLine("                      Opening Stock    :   "+os.ToString()+"                                     ");
			sw.WriteLine("                      Purchase         :   "+rect.ToString()+"                                   ");
			sw.WriteLine("                      Sales            :   "+sales.ToString()+"                                  ");
			sw.WriteLine("                      Closing Stock    :   "+cs.ToString()+"                                     ");
			sw.WriteLine("                   --------------------+-----------------");
			sw.WriteLine("                      Total Sales      :   "+sales.ToString()+"                                  ");
			sw.WriteLine("                      Sales Ibp        :   "+ibp1.ToString()+"                                   ");
			sw.WriteLine("                      Purchase Eicher  :   "+eicher1.ToString()+"                                ");
			sw.WriteLine("                      Purchase Force   :   "+force1.ToString()+"                                 ");
			sw.WriteLine("                   --------------------+-----------------");
			sw.WriteLine("                      Grand Total      :   "+grandtotal.ToString()+"                             ");
			sw.WriteLine("                   --------------------+-----------------");
					
			sw.Close();
		}

		// Method to write into the excel report file to print.
		public void ConvertToExcel()
		{
			//InventoryClass obj=new InventoryClass();
			//SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\ProfitAnalysis.xls";
			StreamWriter sw = new StreamWriter(path);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+txtDateTo.Text);
			sw.WriteLine();
			sw.WriteLine("Opening Stock\t"+os.ToString());
			sw.WriteLine("Purchase\t"+rect.ToString());
			sw.WriteLine("Sales\t"+sales.ToString());
			sw.WriteLine("Closing Stock\t"+cs.ToString());
			sw.WriteLine();
			sw.WriteLine("Total Sales\t"+sales.ToString());
			sw.WriteLine("Sales Ibp\t"+ibp1.ToString());
			sw.WriteLine("Purchase Eicher\t"+eicher1.ToString());
			sw.WriteLine("Purchase Force\t"+force1.ToString());
			sw.WriteLine();
			sw.WriteLine("Grand Total\t"+grandtotal.ToString());
			sw.WriteLine();
			sw.Close();
		}

		/// <summary>
		/// This method is used to prepares the report file ProfitAnalysis.txt for printing.
		/// </summary>
		protected void prnButton_Click(object sender, System.EventArgs e)
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
				Socket sender1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);
					
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ProfitAnalysisReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Method:print");
                
				} 
				catch (ArgumentNullException ane) 
				{
					//Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Method:print"+ ane.Message+". User: "+uid);
				} 
				catch (SocketException se) 
				{
					///Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Method:print"+ se.Message+". User: "+uid);
				} 
				catch (Exception es) 
				{
					//Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Method:print"+ es.Message+". User: "+uid);
				}

			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Method:print"+ ex.Message+". User: "+uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the excel report file ProfitAnalysis.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//if(GridReport.Visible==true)
				//{
				ConvertToExcel();
				MessageBox.Show("Successfully Convert File Into Excel Format");
				CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click  ProfitAnalysis1 Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:ProfitAnalysis1.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   ProfitAnalysis1 Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}