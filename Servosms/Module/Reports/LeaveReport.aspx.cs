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

//using System.Windows.Forms;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for LeaveReport.
	/// </summary>
	public partial class LeaveReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
		string uid;

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
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="15";
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
				GridReport.Visible=false;
				txtDateFrom.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
				Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            Textbox1.Text = Request.Form["Textbox1"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["Textbox1"].ToString().Trim();
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
		
		// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		/// <summary>
		/// This method is used to view the report with the help of BindtheData() function and set the value in 
		/// session variable for ascending or desending the data in run time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {                
					MessageBox.Show("Date From Should be less than Date To");
					GridReport.Visible=false;
				}
				else
				{
					strOrderBy = "Emp_Name ASC";
					Session["Column"] = "Emp_Name";
					Session["Order"] = "ASC";
					BindTheData();
				}
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Method:btnShow_Click"+ " LeaveReport Viewed "+" userid "+ uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Method:btnShow_Click"+  " LeaveReport Viewed "+"  EXCEPTION "+ex.Message+" userid  "+uid);
			}
		}
		

		/// <summary>
		/// This method is used to bind the datagrid and display the information by given order and display 
		/// the data grid.
		/// </summary>
		public void BindTheData()
		{
			SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string sqlstr="select lr.Emp_ID,e.Emp_Name,lr.Date_From,lr.Date_To,lr.Reason,lr.isSanction from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(lr.Date_To as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103)";
			DataSet ds= new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			da.Fill(ds, "Leave_Register");
			DataTable dtCustomers = ds.Tables["Leave_Register"];
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
		}
		
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnSortCommand"
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
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid ");
			}
		}

		/// <summary>
		/// Reurns date in MM/DD/YYYY format.
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

		public string Approved(string str)
		{
			if(str=="0")
				str="No";
			else
				str="Yes";
			return str;
		}

		/// <summary>
		/// Prepares the report file LeaveReport.txt for printing.
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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Leave Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\LeaveReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Leave Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Leave Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Leave Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Leave Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LeaveReport.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			//string strDate = "";
			//sql="select lr.Emp_ID r1,e.Emp_Name r2,lr.Date_From r3,lr.Date_To r4,lr.Reason r5,lr.isSanction r6 from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			sql="select lr.Emp_ID,e.Emp_Name,lr.Date_From,lr.Date_To,lr.Reason,lr.isSanction from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(lr.Date_To as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103)";
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
			string des="--------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			sw.WriteLine(GenUtil.GetCenterAddr("================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("LEAVE REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("================",des.Length));
			sw.WriteLine("+-----------+--------------------+----------+----------+--------------------+--------+");
			sw.WriteLine("|Employee ID|    Employee Name   |From Date | To Date  |       Reason       |Approved|");
			sw.WriteLine("+-----------+--------------------+----------+----------+--------------------+--------+");
			//             12345678901 12345678901234567890 1234567890 1234567890 12345678901234567890 12345678
			if(rdr.HasRows)
			{
				info = " {0,-11:S} {1,-20:F} {2,-10:S} {3,-10:S} {4,-20:S}   {5,-6:S}"; 
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["Emp_ID"].ToString(),10),
						GenUtil.TrimLength(rdr["Emp_Name"].ToString(),20),
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_From"].ToString())),
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_To"].ToString())),
						GenUtil.TrimLength(rdr["Reason"].ToString(),20),
						Approved(rdr["isSanction"].ToString())
						);

				}
			}
			sw.WriteLine("+-----------+--------------------+----------+----------+--------------------+--------+");
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\LeaveReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			sql="select lr.Emp_ID,e.Emp_Name,lr.Date_From,lr.Date_To,lr.Reason,lr.isSanction from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(lr.Date_To as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103)";
			sql=sql+" order by "+Cache["strOrderBy"];
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("Employee ID\tEmployee Name\tFrom Date\tTo Date\tReason\tApproved");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["Emp_ID"].ToString()+"\t"+
					rdr["Emp_Name"].ToString()+"\t"+
					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_From"].ToString()))+"\t"+
					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_To"].ToString()))+"\t"+
					rdr["Reason"].ToString()+"\t"+
					Approved(rdr["isSanction"].ToString())
					);
			}
			sw.Close();
		}

		/// <summary>
		/// This method is used to Prepares the excel report file LeaveReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridReport.Visible==true)
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