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
using System.Data.SqlClient;
using System.Drawing;
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for LY_PS_SalesReport.
	/// </summary>
	public partial class LY_PS_SalesReport : System.Web.UI.Page
	{
		string uid;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public static int View=0;
		public static string ColumnName = "";

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
				CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				View=0;
				ColumnName="";
				//				InventoryClass obj =new InventoryClass();
				//				DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				//				SqlDataReader rdr = obj.GetRecordSet("select * from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'");
				//				int n = rdr.FieldCount;
				//				ColumnName="ly_ps_sales,discription,month,tot_pur,pur_foc,gen_oil,grease,";
				//				for(int p=0,m=7;m<n;m++,p++)
				//				{
				//					ColumnName+=rdr.GetName(m)+",";
				//				}
				//				ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
				//				rdr.Close();
				#region Testing Purpose
				//				InventoryClass obj = new InventoryClass();
				//				SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//				ArrayList arrHeaderName = new ArrayList();
				//				ArrayList arrColName = new ArrayList();
				//				ArrayList arrRemName = new ArrayList();
				//				object ob=null;
				//				dbobj.ExecProc(DBOperations.OprType.Insert,"ProUpdateCustomerType",ref ob,"@Cust_ID","");
				//				SqlDataReader rdr = null;
				//				SqlCommand cmd;
				//				//rdr = obj.GetRecordSet("select distinct case when customertypename like 'oe%' then 'Oe' else customertypename end as customertypename from customertype order by customertypename");
				//				rdr = obj.GetRecordSet("select distinct custtype,custtypeid from tempcustomertype order by custtypeid");
				//				if(rdr.HasRows)
				//				{
				//					while(rdr.Read())
				//					{
				//						string name = rdr.GetValue(0).ToString().ToLower();
				//						name = name.Replace("/","");
				//						name = name.Replace(" ","");
				//						name = name.Replace("-","");
				//						if(rdr.GetValue(0).ToString().ToLower().StartsWith("ro"))
				//						{
				//							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"lube");
				//							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"2t4t");
				//							arrColName.Add(name+"lube");
				//							arrColName.Add(name+"2t4t");
				//						}
				//						else if(rdr.GetValue(0).ToString().ToLower().StartsWith("bazar") || rdr.GetValue(0).ToString().ToLower().StartsWith("bazzar"))
				//						{
				//							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"lube");
				//							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"2t4t");
				//							arrColName.Add(name+"lube");
				//							arrColName.Add(name+"2t4t");
				//						}
				//						else
				//							//arrColName.Add(rdr.GetValue(0).ToString().ToLower());
				//							arrColName.Add(name);
				//					}
				//				}
				//				rdr.Close();
				//				
				//				rdr = obj.GetRecordSet("select * from ly_ps_sale");
				//				int n = rdr.FieldCount;
				//				//ColumnName="ly_ps_sales,month,tot_pur,pur_foc,gen_oil,grease,";
				//				//for(int p=0,m=7;m<n;m++,p++)
				//				for(int p=0,m=9;m<n;m++,p++)
				//				{
				//					arrHeaderName.Add(rdr.GetName(m));
				//					//ColumnName+=rdr.GetName(m)+",";
				//				}
				//				//ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
				//				rdr.Close();
				//				if(arrColName.Count==arrHeaderName.Count)
				//				{
				//					rdr = obj.GetRecordSet("select * from ly_ps_sale");
				//					n = rdr.FieldCount;
				//					ColumnName="ly_ps_sales,discription,month,tot_pur,pur_foc,gen_oil,grease,total_purchase,";
				//					for(int p=0,m=9;m<n;m++,p++)
				//					{
				//						ColumnName+=rdr.GetName(m)+",";
				//					}
				//					ColumnName+="total_sales,";
				//					ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
				//					rdr.Close();
				//					return;
				//				}
				//				
				//				if(arrColName.Count>=arrHeaderName.Count)
				//				{
				//					for(int r=0;r<arrColName.Count;r++)
				//					{
				//						arrRemName.Add(arrColName[r]);
				//					}
				//					for(int q=0;q<arrHeaderName.Count;q++)
				//					{
				//						arrRemName.Remove(arrHeaderName[q]);
				//					}
				//					if(arrRemName.Count>0)
				//					{
				//						for(int k=0;k<arrRemName.Count;k++)
				//						{
				//							con.Open();
				//							string name = arrRemName[k].ToString();
				//							name = name.Replace("/","");
				//							name = name.Replace(" ","");
				//							name = name.Replace("-","");
				//							string str = "alter table ly_ps_sale add "+name+" float";
				//							cmd = new SqlCommand(str,con);
				//							cmd.ExecuteNonQuery();
				//							cmd.Dispose();
				//							con.Close();
				//						}
				//					}
				//				}
				//				else
				//				{
				//					for(int q=0;q<arrHeaderName.Count;q++)
				//					{
				//						arrRemName.Add(arrHeaderName[q]);
				//					}
				//					for(int q=0;q<arrColName.Count;q++)
				//					{
				//						arrRemName.Remove(arrColName[q]);
				//					}
				//					if(arrRemName.Count>0)
				//					{
				//						for(int k=0;k<arrRemName.Count;k++)
				//						{
				//							con.Open();
				//							string name = arrRemName[k].ToString();
				//							name = name.Replace("/","");
				//							name = name.Replace(" ","");
				//							name = name.Replace("-","");
				//							string str = "alter table ly_ps_sale drop column "+name+"";
				//							cmd = new SqlCommand(str,con);
				//							cmd.ExecuteNonQuery();
				//							cmd.Dispose();
				//							con.Close();
				//						}
				//					}					
				//				}
				InventoryClass obj = new InventoryClass();			
				SqlDataReader rdr = obj.GetRecordSet("select * from ly_ps_sale");
				int n = rdr.FieldCount;
				ColumnName="ly_ps_sales,discription,month,tot_pur,pur_foc,gen_oil,grease,total_purchase,";
				for(int p=0,m=9;m<n;m++,p++)
				{
					ColumnName+=rdr.GetName(m)+",";
				}
				ColumnName+="total_sales,";
				ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
				rdr.Close();
				#endregion
				#region Check Privileges
				
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="18";
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
				if(Add_Flag=="0" && Edit_Flag=="0" && View_flag=="0")
				{
					//string msg="UnAthourized Visit to Price Updation Page";
					//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				//if(Add_Flag =="0" && Edit_Flag == "0")
				//	Btnsubmit1.Enabled = false; 
				#endregion
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
		/// This method is used to prepares the report file Ly_Ps_SalesReport.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, System.EventArgs e)
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
					CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:InventoryClass.cs,Method:btnPrint_Click    LY_PS_Sales Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\LY_PS_SalesReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:InventoryClass.cs,Method:btnprint_Clickt    LY_PS_Sales Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:InventoryClass.cs,Method:btnprint_Clickt   LY_PS_Sales Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:InventoryClass.cs,Method:btnprint_Clickt   LY_PS_Sales Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:InventoryClass.cs,Method:btnprint_Clickt  LY_PS_Sales Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
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
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LY_PS_SalesReport.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			//string strDate = "";
			//sql="select lr.Emp_ID r1,e.Emp_Name r2,lr.Date_From r3,lr.Date_To r4,lr.Reason r5,lr.isSanction r6 from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			sql="select * from LY_PS_SALES";
			//sql=sql+" order by "+Cache["strOrderBy"];
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
			string des="----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			/*
			sw.WriteLine(GenUtil.GetCenterAddr("====================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("LY_PS_Sales REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("====================",des.Length));
			
			sw.WriteLine("+------+-------------------------------+-----------------------------------------------------------------------------------------------------------------------------------------------+");
			sw.WriteLine("|Month |         Primary Sales         |                                                          Secondary Sales                                                                      |");
			sw.WriteLine("+------+-------------------------------+-----------------------------------------------------------------------------------------------------------------------------------------------+");
			sw.WriteLine("|      | Total |  Pur  |  Gen  |Greases|      RO1      |      RO2      |      RO3      |      RO4      |      RO5      |  IBP  |Bazzar |  OE   | Fleet |Maruti |Eicher |Hyundai| Total |");
			sw.WriteLine("|      |  Pur  |  FOC  |  Oils |       | Lube  | 2T/4T | Lube  | 2T/4T | Lube  | 2T/4T | Lube  | 2T/4T | Lube  | 2T/4T |       |       |       |       |       |       |       | Sales |");
			sw.WriteLine("+------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+");
			//             123456 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567
			*/
			if(rdr.HasRows)
			{
				sw.WriteLine(GenUtil.GetCenterAddr("====================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("LY_PS_Sales REPORT",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("====================",des.Length));
			
				sw.WriteLine("+------+-------------------------------+-----------------------------------------------------------------------------------------------------------------------------------------------+");
				sw.WriteLine("|Month |         Primary Sales         |                                                          Secondary Sales                                                                      |");
				sw.WriteLine("+------+-------------------------------+-----------------------------------------------------------------------------------------------------------------------------------------------+");
				sw.WriteLine("|      | Total |  Pur  |  Gen  |Greases|      RO1      |      RO2      |      RO3      |      RO4      |      RO5      |  IBP  |Bazzar |  OE   | Fleet |Maruti |Eicher |Hyundai| Total |");
				sw.WriteLine("|      |  Pur  |  FOC  |  Oils |       | Lube  | 2T/4T | Lube  | 2T/4T | Lube  | 2T/4T | Lube  | 2T/4T | Lube  | 2T/4T |       |       |       |       |       |       |       | Sales |");
				sw.WriteLine("+------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+");
				//             123456 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567
				int i=0;
				info = " {0,-6:S} {1,7:F} {2,7:S} {3,7:S} {4,7:S} {5,7:S} {6,7:S} {7,7:S} {8,7:S} {9,7:S} {10,7:S} {11,7:S} {12,7:S} {13,7:S} {14,7:S} {15,7:S} {16,7:S} {17,7:S} {18,7:S} {19,7:S} {20,7:S} {21,7:S} {22,7:S}"; 
				while(rdr.Read())
				{
					if(i < 12)
					{
						sw.WriteLine(info,rdr.GetValue(1).ToString(),
							rdr.GetValue(2).ToString(),
							rdr.GetValue(3).ToString(),
							rdr.GetValue(4).ToString(),
							rdr.GetValue(5).ToString(),
							rdr.GetValue(6).ToString(),
							rdr.GetValue(7).ToString(),
							rdr.GetValue(8).ToString(),
							rdr.GetValue(9).ToString(),
							rdr.GetValue(10).ToString(),
							rdr.GetValue(11).ToString(),
							rdr.GetValue(12).ToString(),
							rdr.GetValue(13).ToString(),
							rdr.GetValue(14).ToString(),
							rdr.GetValue(15).ToString(),
							rdr.GetValue(16).ToString(),
							rdr.GetValue(17).ToString(),
							rdr.GetValue(18).ToString(),
							rdr.GetValue(19).ToString(),
							rdr.GetValue(20).ToString(),
							rdr.GetValue(21).ToString(),
							rdr.GetValue(22).ToString(),
							rdr.GetValue(23).ToString()
							);
					}
					else
					{
						sw.WriteLine("+------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+");
						
						sw.WriteLine(info,rdr.GetValue(1).ToString(),
							rdr.GetValue(2).ToString(),
							rdr.GetValue(3).ToString(),
							rdr.GetValue(4).ToString(),
							rdr.GetValue(5).ToString(),
							rdr.GetValue(6).ToString(),
							rdr.GetValue(7).ToString(),
							rdr.GetValue(8).ToString(),
							rdr.GetValue(9).ToString(),
							rdr.GetValue(10).ToString(),
							rdr.GetValue(11).ToString(),
							rdr.GetValue(12).ToString(),
							rdr.GetValue(13).ToString(),
							rdr.GetValue(14).ToString(),
							rdr.GetValue(15).ToString(),
							rdr.GetValue(16).ToString(),
							rdr.GetValue(17).ToString(),
							rdr.GetValue(18).ToString(),
							rdr.GetValue(19).ToString(),
							rdr.GetValue(20).ToString(),
							rdr.GetValue(21).ToString(),
							rdr.GetValue(22).ToString(),
							rdr.GetValue(23).ToString()
							);
						sw.WriteLine("+------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+");
					}
					i++;
				}
				
			}
			else
			{
				MessageBox.Show("Data Not Available");
				return;
			}
			//sw.WriteLine("+------+-------+-------+-------+-------+----------+----------+----------+----------+----------+-----+-----+-----+-----+-----+-----+-----+");
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			InventoryClass obj1=new InventoryClass();
			SqlDataReader rdr, SqlDtr = null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\LY_PS_SalesReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			//			sql="select * from LY_PS_SALES";
						
			string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
			sql="select "+ColumnName+" from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'";
			rdr=obj.GetRecordSet(sql);
			
			if(rdr.HasRows)
			{
				sw.WriteLine("Month\tPrimary Sales\t\t\t\tSecondary Sales");
				sw.Write("\tTotal Purchase\tPurchase FOC\tGenuine Oils\tGrease");
				int count=5;
				ArrayList arrstr = new ArrayList();
				string str="select distinct custtype,custtypeid from tempcustomertype order by custtypeid";
				SqlDtr=obj1.GetRecordSet(str);
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						arrstr.Add(SqlDtr.GetValue(0).ToString());
						if(SqlDtr.GetValue(0).ToString().ToLower().StartsWith("ro") || SqlDtr.GetValue(0).ToString().ToLower().StartsWith("bazzar") || SqlDtr.GetValue(0).ToString().ToLower().StartsWith("bazar"))
						{
							sw.Write("\t"+SqlDtr.GetValue(0).ToString()+"\t");
						}
						else
						{
							sw.Write("\t"+SqlDtr.GetValue(0).ToString());
						}
					}
					sw.WriteLine();
				}
				else
				{
					sw.WriteLine();
				}
				SqlDtr.Close();
				//*********************************
				sw.Write("\t\t\t\t");
				for(int n=0;n<arrstr.Count;n++)
				{
					if(arrstr[n].ToString().ToLower().StartsWith("ro") || arrstr[n].ToString().ToLower().StartsWith("bazzar") || arrstr[n].ToString().ToLower().StartsWith("bazar"))
					{
						count+=2;
						sw.Write("\tLube");
						sw.Write("\t2T/4T");
					}
					else
					{
						count++;
					}
				}
				sw.WriteLine();
				//***********************************
				while(rdr.Read())
				{
					sw.Write(rdr.GetValue(2).ToString());
					for(int j=1;j<count;j++)
					{
						sw.Write("\t"+rdr.GetValue(j+2).ToString());
					}
					sw.WriteLine();
				}
				
				//				sw.WriteLine("Month\tPrimary Sales\t\t\t\tSecondary Sales");
				//				sw.WriteLine("\tTotal\tPur.\tGen\tGreases\tRO1\t\tRO2\t\tRO3\t\tRO4\t\tRO5\t\tIBP\tBazzar\tOE\tFleet\tMaruti\tEicher\tHyundai\tTotal");
				//				sw.WriteLine("\tPur.\tFOC\tOils\t\tLube\t2T/4T\tLube\t2T/4T\tLube\t2T/4T\tLube\t2T/4T\tLube\t2T/4T\t\t\t\t\t\t\t\tSales");
				//				int i=0;
				//				while(rdr.Read())
				//				{
				//					if(i < 12)
				//					{
				//						sw.WriteLine(rdr.GetValue(1).ToString()+"\t"+
				//							rdr.GetValue(2).ToString()+"\t"+
				//							rdr.GetValue(3).ToString()+"\t"+
				//							rdr.GetValue(4).ToString()+"\t"+
				//							rdr.GetValue(5).ToString()+"\t"+
				//							rdr.GetValue(6).ToString()+"\t"+
				//							rdr.GetValue(7).ToString()+"\t"+
				//							rdr.GetValue(8).ToString()+"\t"+
				//							rdr.GetValue(9).ToString()+"\t"+
				//							rdr.GetValue(10).ToString()+"\t"+
				//							rdr.GetValue(11).ToString()+"\t"+
				//							rdr.GetValue(12).ToString()+"\t"+
				//							rdr.GetValue(13).ToString()+"\t"+
				//							rdr.GetValue(14).ToString()+"\t"+
				//							rdr.GetValue(15).ToString()+"\t"+
				//							rdr.GetValue(16).ToString()+"\t"+
				//							rdr.GetValue(17).ToString()+"\t"+
				//							rdr.GetValue(18).ToString()+"\t"+
				//							rdr.GetValue(19).ToString()+"\t"+
				//							rdr.GetValue(20).ToString()+"\t"+
				//							rdr.GetValue(21).ToString()+"\t"+
				//							rdr.GetValue(22).ToString()+"\t"+
				//							rdr.GetValue(23).ToString()
				//							);
				//					}
				//					else
				//					{
				//						sw.WriteLine(rdr.GetValue(1).ToString()+"\t"+
				//							rdr.GetValue(2).ToString()+"\t"+
				//							rdr.GetValue(3).ToString()+"\t"+
				//							rdr.GetValue(4).ToString()+"\t"+
				//							rdr.GetValue(5).ToString()+"\t"+
				//							rdr.GetValue(6).ToString()+"\t"+
				//							rdr.GetValue(7).ToString()+"\t"+
				//							rdr.GetValue(8).ToString()+"\t"+
				//							rdr.GetValue(9).ToString()+"\t"+
				//							rdr.GetValue(10).ToString()+"\t"+
				//							rdr.GetValue(11).ToString()+"\t"+
				//							rdr.GetValue(12).ToString()+"\t"+
				//							rdr.GetValue(13).ToString()+"\t"+
				//							rdr.GetValue(14).ToString()+"\t"+
				//							rdr.GetValue(15).ToString()+"\t"+
				//							rdr.GetValue(16).ToString()+"\t"+
				//							rdr.GetValue(17).ToString()+"\t"+
				//							rdr.GetValue(18).ToString()+"\t"+
				//							rdr.GetValue(19).ToString()+"\t"+
				//							rdr.GetValue(20).ToString()+"\t"+
				//							rdr.GetValue(21).ToString()+"\t"+
				//							rdr.GetValue(22).ToString()+"\t"+
				//							rdr.GetValue(23).ToString()
				//							);
				//					}
				//					i++;
				//				}
				//				
			}
			else
			{
				MessageBox.Show("Data Not Available");
				return;
			}
			rdr.Close();
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// This method is used to prepares the excel report file Ly_Ps_salesReport.xls for printing.
		/// </summary>
		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View == 1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    LY_PS_Sales Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:LY_PS_SalesReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   LY_PS_Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to show the report.
		/// </summary>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			txtDiscription.Text="";
			if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
			{
				MessageBox.Show("Year Can Not Be Same");
				View=0;
				return;
			}
			else if(DropYearFrom.SelectedIndex+1==DropYearTo.SelectedIndex)
			{
				View = 1;
			}
			else
			{
				MessageBox.Show("Invalid Year Selection");
				View=0;
				return;
			}
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select discription from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'");
			if(rdr.Read())
			{
				txtDiscription.Text=rdr["Discription"].ToString();
			}
			else
			{
				txtDiscription.Text="";
				MessageBox.Show("Data Not Available");
				View=0;
			}
		}
	}
}