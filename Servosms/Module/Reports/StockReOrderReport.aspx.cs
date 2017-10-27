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
	/// Summary description for StockReOrderReport.
	/// </summary>
	public partial class StockReOrderReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
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
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockReorderReport.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
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
				string SubModule="47";
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
				txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
			}
            txtDateFrom.Text= Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
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
		/// This method is used to binding the datagrid.
		/// </summary>
		public void BindTheData()
		{
			SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();
			string sqlstr="select Prod_ID,Prod_Code,Prod_Name+' : '+Pack_Type Prod_Name,minlabel,reorderlable,maxlabel from Products";
			DataSet ds= new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			da.Fill(ds, "Products");
			DataTable dtCustomers = ds.Tables["Products"];
			DataView dv=new DataView(dtCustomers);
			dv.Sort = strOrderBy;
			Cache["strOrderBy"]=strOrderBy;
			if(dv.Count==0)
			{
				MessageBox.Show("Data not available");
				GridReport.Visible=false;
			}
			else
			{
				GridReport.DataSource=dv;
				GridReport.DataBind();
				GridReport.Visible=true;
			}
			SqlCon.Close();
		}

		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of the datagridheader.
		/// </summary>
		public void SortCommand_Click(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				//Check to see if same column clicked again
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["Order"].Equals("ASC"))
					{
						strOrderBy=e.SortExpression.ToString() +" DESC";
						Session["Order"]="DESC";
					}
					else
					{
						strOrderBy=e.SortExpression.ToString() +" ASC";
						Session["Order"]="ASC";
					}
				}
					//Different column selected, so default to ascending order
				else
				{
					strOrderBy = e.SortExpression.ToString() +" ASC";
					Session["Order"] = "ASC";
				}
				Session["Column"] = e.SortExpression.ToString();
				BindTheData();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid ");
			}
		}

		/// <summary>
		/// Returns date in MM/DD/YYYY format.
		/// </summary>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// This method is used to view the report and set the column name with ascending order 
		/// in session variable.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
				strOrderBy = "Prod_Code ASC";
				Session["Column"] = "Prod_Code";
				Session["Order"] = "ASC";
				BindTheData();
				
				CreateLogFiles.ErrorLog("Form:StockReOrderingReportReport.aspx,Method:btnShow_Click"+ " StockReOrdering Report Viewed "+" userid "+ uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Method:btnShow_Click"+  " StockReOrdering Report Viewed "+"  EXCEPTION "+ex.Message+" userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the report file StockReOrderReport.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
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
					CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    StockReOrdering Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\StockReOrderingReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    StockReOrdering Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    StockReOrdering Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    StockReOrdering Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    StockReOrdering Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\StockReOrderingReport.txt";
				StreamWriter sw = new StreamWriter(path);
				string sql="";
				string info = "";
				//string strDate = "";
				//sql="select lr.Emp_ID r1,e.Emp_Name r2,lr.Date_From r3,lr.Date_To r4,lr.Reason r5,lr.isSanction r6 from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				sql="select Prod_ID,Prod_Name+':'+Pack_Type Prod_Name,Prod_Code,minlabel,maxlabel,reorderlable from Products";
				sql=sql+" order by "+Cache["strOrderBy"];
				dbobj.SelectQuery(sql,ref rdr);
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
				string des="-----------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("=====================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Stock ReOrdering Report as ",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("=====================================",des.Length));
				sw.WriteLine("+------------+--------------------------------------------------+---------------+-----------+-------------+-----------+");
				sw.WriteLine("|Product Code|                  Product Name                    |  Stock as On  | Min Level |ReOrder Level| Max Level |");
				sw.WriteLine("+------------+--------------------------------------------------+---------------+-----------+-------------+-----------+");
				//             123456789012 12345678901234567890123456789012345678901234567890 123456789012345 12345678901 1234567890123 12345678901
				//string str="";
				
				if(rdr.HasRows)
				{
					info = " {0,-12:S} {1,-50:S} {2,15:S} {3,11:S} {4,13:S} {5,11:S}"; 
					while(rdr.Read())
					{
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),50),
							GetStock(rdr["Prod_ID"].ToString()),
							rdr["minlabel"].ToString(),
							rdr["reorderlable"].ToString(),
							rdr["maxlabel"].ToString()
							);
						
					}
				}
				sw.WriteLine("+------------+--------------------------------------------------+---------------+-----------+-------------+-----------+");
				dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Prepares the excel report file StockReOrderReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridReport.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   StockReOrdering Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:StockReOrderingReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    StockReOrdering Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\StockReorderingReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			sql="select Prod_ID,Prod_Name+':'+Pack_Type Prod_Name,Prod_Code,minlabel,reorderlable,maxlabel from Products";
			sql=sql+" order by "+Cache["strOrderBy"];
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("Product Code\tSKU Name With Pack\tStock as On\tMin Level\tReorder Level\tMax Level");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
					rdr["Prod_Name"].ToString().Trim()+"\t"+
					GetStock(rdr["Prod_ID"].ToString())+"\t"+
					rdr["minlabel"].ToString()+"\t"+
					rdr["reorderlable"].ToString()+"\t"+
					rdr["maxlabel"].ToString()
					);
			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method return the closing stock of given product id.
		/// </summary>
		public string GetStock(string id)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr;
			rdr = obj.GetRecordSet("select top 1(Closing_Stock) from stock_Master where cast(floor(cast(stock_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and Productid='"+id+"' order by stock_date desc");
			if(rdr.Read())
				return rdr["closing_stock"].ToString();
			else
				return "0";
		}
	}
}