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
	/// Summary description for LubeIndent1.
	/// </summary>
	public partial class LubeIndent : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		//string strOrderBy="";
		string StartDate="",EndDate="";
		public string uid;

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
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(DropMonth.SelectedIndex!=0)
			{
				int i=DropMonth.SelectedIndex;
				StartDate=i+"/1/"+DropYear.SelectedItem.Text;
				int day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),i);
				EndDate=i+"/"+day+"/"+DropYear.SelectedItem.Text;
			}
			if(!IsPostBack)
			{
				DropYear.SelectedIndex=DropYear.Items.IndexOf(DropYear.Items.FindByValue(DateTime.Now.Year.ToString()));
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="17";
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
				//				GridReport.Visible=false;
				//				txtDateFrom.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
				//				Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
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

		//		private void btnPrint_Click(object sender, System.EventArgs e)
		//		{
		//			byte[] bytes = new byte[1024];
		//			// Connect to a remote device.
		//			try 
		//			{
		//				makingReport();
		//				// Establish the remote endpoint for the socket.
		//				// The name of the
		//				// remote device is "host.contoso.com".
		//				IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
		//				IPAddress ipAddress = ipHostInfo.AddressList[0];
		//				IPEndPoint remoteEP = new IPEndPoint(ipAddress,62000);
		//				// Create a TCP/IP  socket.
		//				Socket sender1 = new Socket(AddressFamily.InterNetwork, 
		//					SocketType.Stream, ProtocolType.Tcp );
		//
		//				// Connect the socket to the remote endpoint. Catch any errors.
		//				try 
		//				{
		//					sender1.Connect(remoteEP);
		//					Console.WriteLine("Socket connected to {0}",
		//						sender1.RemoteEndPoint.ToString());
		//					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    LubeIndent Report  Printed"+"  userid  " +uid);
		//					// Encode the data string into a byte array.
		//					string home_drive = Environment.SystemDirectory;
		//					home_drive = home_drive.Substring(0,2); 
		//					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\LubeIndent.txt<EOF>");
		//
		//					// Send the data through the socket.
		//					int bytesSent = sender1.Send(msg);
		//
		//					// Receive the response from the remote device.
		//					int bytesRec = sender1.Receive(bytes);
		//					Console.WriteLine("Echoed test = {0}",
		//						Encoding.ASCII.GetString(bytes,0,bytesRec));
		//
		//					// Release the socket.
		//					sender1.Shutdown(SocketShutdown.Both);
		//					sender1.Close();
		//                
		//				} 
		//				catch (ArgumentNullException ane) 
		//				{
		//					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
		//					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    LubeIndent Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
		//				} 
		//				catch (SocketException se) 
		//				{
		//					Console.WriteLine("SocketException : {0}",se.ToString());
		//					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    LubeIndent Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
		//				} 
		//				catch (Exception es) 
		//				{
		//					Console.WriteLine("Unexpected exception : {0}", es.ToString());
		//					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    LubeIndent Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
		//				}
		//
		//			} 
		//			catch (Exception ex) 
		//			{
		//				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    LubeIndent Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
		//			}
		//		}
		//		public void makingReport()
		//		{
		//			System.Data.SqlClient.SqlDataReader rdr=null;
		//			string home_drive = Environment.SystemDirectory;
		//			home_drive = home_drive.Substring(0,2); 
		//			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LubeIndent.txt";
		//			StreamWriter sw = new StreamWriter(path);
		//			string sql="select * from Indent_Lube il,products p where ssaid='"+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code";
		//			
		//			string info = "";
		//			sw.Write((char)27);//added by vishnu
		//			sw.Write((char)67);//added by vishnu
		//			sw.Write((char)0);//added by vishnu
		//			sw.Write((char)12);//added by vishnu
		//			
		//			sw.Write((char)27);//added by vishnu
		//			sw.Write((char)78);//added by vishnu
		//			sw.Write((char)5);//added by vishnu
		//							
		//			sw.Write((char)27);//added by vishnu
		//			sw.Write((char)15);
		//			//**********
		//			string des="-----------------------------------------------------------------------------------------------------------------------------------------";
		//			string Address=GenUtil.GetAddress();
		//			string[] addr=Address.Split(new char[] {':'},Address.Length);
		//			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
		//			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
		//			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
		//			sw.WriteLine(des);
		//			//******S***
		//			sw.WriteLine(GenUtil.GetCenterAddr("====================",des.Length));
		//			sw.WriteLine(GenUtil.GetCenterAddr("Lube Indent Report For "+DropMonth.SelectedItem.Text,des.Length));
		//			sw.WriteLine(GenUtil.GetCenterAddr("====================",des.Length));
		//			sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
		//			sw.WriteLine("|  RSE  |SUP.EX.|RETAIL MPSO| SKY TYPE |PACK TYPE|PACK QTY|PRODUCT CODE| SKU NAME WITH PACK |INDENT|RECEIPT|DIFFRENT|    REMARK         |");
		//			sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
		//			//             12345678901 12345678901234567890 1234567890 1234567890 12345678901234567890 12345678
		//			int i=0;
		//			if(rdr.HasRows)
		//			{
		//				info = "{0,-7:S} {1,-7:S} {2,-11:S} {3,-10:S} {4,-9:S} {5,-8:S} {6,-12:S} {7,-20:S} {8,-6:S} {9,-7:S} {10,-8:S} {11,-19:S}";
		//				while(rdr.Read())
		//				{
		//					sw.WriteLine(info,GenUtil.TrimLength(rdr["rse"].ToString(),7),
		//						GenUtil.TrimLength(rdr["supex"].ToString(),7),
		//						GenUtil.TrimLength(rdr["retailmpso"].ToString(),11),
		//						GenUtil.TrimLength(rdr["skutype"].ToString(),10),
		//						rdr["packcode"].ToString(),
		//						rdr["packqty"].ToString(),
		//						rdr["prodcode"].ToString(),
		//						GenUtil.TrimLength(rdr["skuname"].ToString(),20),
		//						rdr["indent"].ToString(),
		//						getReceipt(rdr["prodcode"].ToString()),
		//						getDiff(getReceipt(rdr["prodcode"].ToString()),rdr["Indent"].ToString()),
		//						GenUtil.TrimLength(Request.Params.Get("txtRemark"+i++),19)
		//						);
		//				}
		//			}
		//			sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
		//			dbobj.Dispose();
		//			sw.Close();
		//		}
		//		public void ConvertToExcel()
		//		{
		//			InventoryClass obj=new InventoryClass();
		//			SqlDataReader rdr;
		//			string home_drive = Environment.SystemDirectory;
		//			home_drive = home_drive.Substring(0,2);
		//			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\";
		//			Directory.CreateDirectory(strExcelPath);
		//			string path = home_drive+@"\Servosms_ExcelFile\LubeIndent.xls";
		//			StreamWriter sw = new StreamWriter(path);
		//			string sql="";
		//			//sql="select lr.Emp_ID,e.Emp_Name,lr.Date_From,lr.Date_To,lr.Reason,lr.isSanction from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
		//			sql=sql+" order by "+Cache["strOrderBy"];
		//			
		//			rdr=obj.GetRecordSet(sql);
		//			sw.WriteLine("Employee ID\tEmployee Name\tFrom Date\tTo Date\tReason\tApproved");
		//			while(rdr.Read())
		//			{
		//				sw.WriteLine(rdr["Emp_ID"].ToString()+"\t"+
		//					rdr["Emp_Name"].ToString()+"\t"+
		//					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_From"].ToString()))+"\t"+
		//					GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Date_To"].ToString()))+"\t"+
		//					rdr["Reason"].ToString()
		//					//Approved(rdr["isSanction"].ToString())
		//					);
		//			}
		//			sw.Close();
		//		}
		//
		//		private void btnExcel_Click(object sender, System.EventArgs e)
		//		{
		//			try
		//			{
		//				//if(GridReport.Visible==true)
		//				//{
		//				ConvertToExcel();
		//				MessageBox.Show("Successfully Convert File Into Excel Format");
		//				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    LubeIndent Report Convert Into Excel Format, userid  "+uid);
		//				//}
		//				//else
		//				//{
		//				MessageBox.Show("Please Click the View Button First");
		//				return;
		//				//}
		//			}
		//			catch(Exception ex)
		//			{
		//				MessageBox.Show("First Close The Open Excel File");
		//				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    LubeIndent Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
		//			}
		//		}

		/// <summary>
		/// This method return the total receipt of given this period.
		/// </summary>
		public string getReceipt(string rec,string packqty)
		{
			InventoryClass obj = new InventoryClass();
			//SqlDataReader rdr=obj.GetRecordSet("select sum(qty) from purchase_details where prod_id in(select p.prod_id from indent_lube pm,products p where p.prod_code=pm.prodcode and pm.prodcode='"+rec+"') group by prod_id");
			SqlDataReader rdr = obj.GetRecordSet("select sum(qty) from purchase_details pd,purchase_master pm where pd.invoice_no=pm.invoice_no and pd.prod_id =(select p.prod_id from indent_lube il,products p where p.prod_code=il.prodcode and il.packqty=p.total_qty and ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex.ToString()+"' and il.prodcode='"+rec+"' and il.packqty='"+packqty+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate+"' group by prod_id");
			if(rdr.Read())
			{
				return rdr.GetValue(0).ToString();
			}
			else
			{
				return "0";
			}
			//rdr.Close();
		}
		
		/// <summary>
		/// This method is used to return the difference between given rec value and indent vaue.
		/// </summary>
		public string getDiff(string rec,string indent)
		{
			double diff=0;
			diff=double.Parse(rec)-double.Parse(indent);
			return diff.ToString();
		}

		/// <summary>
		/// Prepares the excel report file LubeIndent.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ConvertToExcel();
				MessageBox.Show("Successfully Convert File into Excel Format");
				CreateLogFiles.ErrorLog("Form:ClaimSheet.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Secon. Sales Claim Sheet Report Convert Into Excel Format ,  userid  "+uid);
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Lube Indent Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\LubeIndent.xls";
			StreamWriter sw = new StreamWriter(path);
			if(DropMonth.SelectedIndex!=0)
			{
				int k=DropMonth.SelectedIndex;
				StartDate=k+"/1/"+DropYear.SelectedItem.Text;
				int day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),k);
				EndDate=k+"/"+day+"/"+DropYear.SelectedItem.Text;
			}
			string sql="";
			sql="select * from Indent_Lube il,products p where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code order by prodcode";
			//sql=sql+" order by "+Cache["strOrderBy"];
			int i=0;
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("RSE\tSUP.EX.\tRETAIL MPSO\tSKY TYPE\tPACK TYPE\tPACK QTY\tPRODUCT CODE\tSKY NAME WITH PACK\tINDENT\tRECEIPT\tDIFFRENT\tREMARK");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["rse"].ToString()+"\t"+
					rdr["supex"].ToString()+"\t"+
					rdr["retailmpso"].ToString()+"\t"+
					rdr["skutype"].ToString()+"\t"+
					rdr["packcode"].ToString()+"\t"+
					rdr["packqty"].ToString()+"\t"+
					rdr["prodcode"].ToString()+"\t"+
					rdr["skunamewithpack"].ToString()+"\t"+
					rdr["indent"].ToString()+"\t"+
					getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString())+"\t"+
					getDiff(getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),rdr["Indent"].ToString())+"\t"+
					Request.Params.Get("txtRemark"+i++)
					);
			}
			sw.Close();
		}

		/// <summary>
		/// This method is used to write into the report file to print.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				System.Data.SqlClient.SqlDataReader rdr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LubeIndent.txt";
				StreamWriter sw = new StreamWriter(path);
				string sql="select * from Indent_Lube il,products p where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code order by prodcode";
				string info = "";
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
				string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//******S***
				sw.WriteLine(GenUtil.GetCenterAddr("=================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Lube Indent Report For "+DropMonth.SelectedItem.Text,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("=================================",des.Length));
				sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
				sw.WriteLine("|  RSE  |SUP.EX.|RETAIL MPSO| SKY TYPE |PACK CODE|PACK QTY|PRODUCT CODE| SKU NAME WITH PACK |INDENT|RECEIPT|DIFFRENT|    REMARK         |");
				sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
				//             1234567 1234567 12345678901 1234567890 123456789 12345678 123456789012 12345678901234567890 123456 1234567 12345678 1234567890123456789
				int i=0;
				if(DropMonth.SelectedIndex!=0)
				{
					int k=DropMonth.SelectedIndex;
					StartDate=k+"/1/"+DropYear.SelectedItem.Text;
					int day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),k);
					EndDate=k+"/"+day+"/"+DropYear.SelectedItem.Text;
				}
				info = " {0,-7:S} {1,-7:S} {2,-11:S} {3,-10:S} {4,-9:S} {5,-8:S} {6,-12:S} {7,-20:S} {8,6:S} {9,7:S} {10,8:S} {11,-19:S}";
				rdr=obj.GetRecordSet(sql);
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						sw.WriteLine(info,GenUtil.TrimLength(rdr["rse"].ToString(),7),
							GenUtil.TrimLength(rdr["supex"].ToString(),7),
							GenUtil.TrimLength(rdr["retailmpso"].ToString(),11),
							GenUtil.TrimLength(rdr["skutype"].ToString(),10),
							rdr["packcode"].ToString(),
							rdr["packqty"].ToString(),
							rdr["prodcode"].ToString(),
							GenUtil.TrimLength(rdr["skunamewithpack"].ToString(),20),
							rdr["indent"].ToString(),
							getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),
							getDiff(getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),rdr["Indent"].ToString()),
							GenUtil.TrimLength(Request.Params.Get("txtRemark"+i++),19)
							);
					}
				}
				sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
				//dbobj.Dispose();
				sw.Close();
				print();
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:Print() Indent Updated For "+DropMonth.SelectedItem.Text+" 2007");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:update().   EXCEPTION " +ex.Message );
			}
		}

		/// <summary>
		/// Contacts the server and sends the StockMovementReport.txt file name to print.
		/// </summary>
		public void print()
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

					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\LubeIndent.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print, Lube Indent Report are Printed,  userid "+ uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+ane.Message+" userid "+ uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+se.Message+" userid "+ uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
				}
				//CreateLogFiles.ErrorLog("Form:StockMovement.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
			}
		}
	}
}