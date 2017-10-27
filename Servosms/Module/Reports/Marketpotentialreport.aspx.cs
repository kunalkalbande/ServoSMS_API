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
	/// Summary description for Marketpotentialreport.
	/// </summary>
	public partial class Marketpotentialreport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
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
				
				CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				try
				{
					Gridmpr.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="19";
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
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method: page_load, userid  "+uid);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+uid);
				}
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
		/// This metod is used to make sorting datagrid on clicking of datagridheader.
		/// </summary>
		string strorderby="";
		public void sortcommand_click(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["order"].Equals("ASC"))
					{
						strorderby=e.SortExpression.ToString()+" DESC";
						Session["order"]="DESC";
					}
					else
					{
						strorderby=e.SortExpression.ToString()+" ASC";
						Session["order"]="ASC";
					}
				}
				else
				{
					strorderby=e.SortExpression.ToString()+" ASC";
					Session["order"]="ASC";
				}
				Session["column"]=e.SortExpression.ToString();
				Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketpotentialReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// this is used to bind the datagrid with the help of making query.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string  sql="select firmname m1,place m2,contactper m3,teleno m4,type m5,regcustomer m6,potential m7,servo m8,castrol m9,shell m10,bpcl m11,veedol m12,elf m13,hpcl m14,pennzoil m15,spurious m16 from marketcustomerentry1";	
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds," marketcustomerentry1");
			DataTable dtcustomer=ds.Tables[" marketcustomerentry1"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			Gridmpr.DataSource=dv;
			if(dv.Count!=0)
			{
				Gridmpr.DataBind();
				Gridmpr.Visible=true;
			}
			else
			{
				Gridmpr.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}
	
		/// <summary>
		/// This method is used to view the report.
		/// </summary>
		protected void btnview_Click(object sender, System.EventArgs e)
		{
			try
			{
				PetrolPumpClass  obj=new PetrolPumpClass();
				//SqlDataReader SqlDtr;
				//***string sql;
				//**	sql="select firmname m1,place m2,contactper m3,teleno m4,type m5,regcustomer m6,potential m7,servo m8,castrol m9,shell m10,bpcl m11,veedol m12,elf m13,hpcl m14,pennzoil m15,spurious m16 from marketcustomerentry1";
				//**	SqlDtr =obj.GetRecordSet(sql);
				//**	Gridmpr.DataSource=SqlDtr;
				//**	if(SqlDtr.HasRows)
				//**	{
				//**		Gridmpr.DataBind();
				//**		Gridmpr.Visible=true;
				//**	}
				//**	else
				//**	{
				//**		MessageBox.Show("Data not available");
				//**		Gridmpr.Visible=false;
				//**		return;
				//**	}
				//**	SqlDtr.Close();

				strorderby="m1 ASC";
				Session["Column"]="m1";
				Session["order"]="ASC";
				Bindthedata();

				CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnview_Click" +"  Marketpotentialreport Viewed  "+" userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method: btnview_Click " +"  Marketpotentialreport Viewed  "+ ex.Message+"  EXCEPTION  " +" userid  "+uid);
			}
		}
			
		/// <summary>
		/// This method is used to make the report for printing.
		/// </summary>
		public void makingReport()
		{
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\MarketPotentialReport.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			//string strDate = "";

			sql="select firmname m1,place m2,contactper m3,teleno m4,type m5,regcustomer m6,potential m7,servo m8,castrol m9,shell m10,bpcl m11,veedol m12,elf m13,hpcl m14,pennzoil m15,spurious m16 from marketcustomerentry1";
			sql=sql+" order by "+""+Cache["strorderby"]+"";
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
			string des="-----------------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=========================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Market Potential REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=========================",des.Length));
			sw.WriteLine("Note --> Reg :- Reguler Customer, Potl :- Potential, pnzoil :- Pennzoil, Spurs :- Spurious");
			//sw.WriteLine("+---------------+---------------+---------------+-----------+-------------+-------+---------+-----+-------+-----+-----+------+-----+-----+--------+--------+");
			//			sw.WriteLine("|   Firm Name   |     Place     |Contact Person |  Tele No  |     Type    |Regular|Potential|Servo|Castrol|Shell|BPCL |Veedol| ELF |HPCL |Pennzoil|Spurious");
			//			sw.WriteLine("+---------------+---------------+---------------+-----------+-------------+-------+---------+-----+-------+-----+-----+------+-----+-----+--------+--------+");
			
			sw.WriteLine("+---------------+---------------+---------------+----------+-------------+---+----+-----+-------+-----+----+------+---+----+------+-----+");
			sw.WriteLine("|   Firm Name   |     Place     |Contact Person |  Tele No |     Type    |Reg|Potl|Servo|Castrol|Shell|BPCL|Veedol|ELF|HPCL|Pnzoil|Spurs");
			sw.WriteLine("+---------------+---------------+---------------+----------+-------------+---+----+-----+-------+-----+----+------+---+----+------+-----+");
        
			if(rdr.HasRows)
			{
				// info : to set the format the displaying string.
				info = " {0,-15:S} {1,-15:S} {2,-15:S} {3,10:S} {4,-13:S} {5,-3:S} {6,4:S} {7,5:S} {8,7:S} {9,5:S} {10,4:S} {11,6:S} {12,3:S} {13,4:S} {14,6:S} {15,5:S}"; 
				while(rdr.Read())
				{
										                                         
					/*sw.WriteLine(info,rdr["Prod_ID"].ToString().Trim(),
						rdr["Prod_Name"].ToString().Trim(),
						rdr["Pack_Type"].ToString(),
						GenUtil.strNumericFormat(rdr["Pur_Rate"].ToString().Trim()),
						GenUtil.strNumericFormat(rdr["sal_Rate"].ToString().Trim()),
						GenUtil.str2DDMMYYYY(strDate));*/
					sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString(),15),
						GenUtil.TrimLength(rdr["m2"].ToString(),15),
						GenUtil.TrimLength(rdr["m3"].ToString(),15),
						rdr["m4"].ToString(),
						GenUtil.TrimLength(rdr["m5"].ToString(),13),
						rdr["m6"].ToString(),
						rdr["m7"].ToString(),
						rdr["m8"].ToString(),
						rdr["m9"].ToString(),
						rdr["m10"].ToString(),
						rdr["m11"].ToString(),
						rdr["m12"].ToString(),
						rdr["m13"].ToString(),
						rdr["m14"].ToString(),
						rdr["m15"].ToString(),
						rdr["m16"].ToString());
				}
			}
			//sw.WriteLine("+---------------+---------------+---------------+-----------+-------------+-------+---------+-----+-------+-----+-----+------+-----+-----+--------+--------+");
			sw.WriteLine("+---------------+---------------+---------------+----------+-------------+---+----+-----+-------+-----+----+------+---+----+------+-----+");
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\MarketPotentialReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			sql="select firmname m1,place m2,contactper m3,teleno m4,type m5,regcustomer m6,potential m7,servo m8,castrol m9,shell m10,bpcl m11,veedol m12,elf m13,hpcl m14,pennzoil m15,spurious m16 from marketcustomerentry1";
			sql=sql+" order by "+""+Cache["strorderby"]+"";
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("Firm Name\tPlace\tContact Person\tTele No\tType\tReg\tPotl\tServo\tCastrol\tShell\tBPCL\tVeedol\tELF\tHPCL\tPnzoil\tSpurs");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["m1"].ToString()+"\t"+
					rdr["m2"].ToString()+"\t"+
					rdr["m3"].ToString()+"\t"+
					rdr["m4"].ToString()+"\t"+
					rdr["m5"].ToString()+"\t"+
					rdr["m6"].ToString()+"\t"+
					rdr["m7"].ToString()+"\t"+
					rdr["m8"].ToString()+"\t"+
					rdr["m9"].ToString()+"\t"+
					rdr["m10"].ToString()+"\t"+
					rdr["m11"].ToString()+"\t"+
					rdr["m12"].ToString()+"\t"+
					rdr["m13"].ToString()+"\t"+
					rdr["m14"].ToString()+"\t"+
					rdr["m15"].ToString()+"\t"+
					rdr["m16"].ToString());

			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method is used to print the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnprint_Click(object sender, System.EventArgs e)
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
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Marketpotentialreport  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MarketPotentialReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Marketpotentialreport  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Marketpotentialreport  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Marketpotentialreport  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Marketpotentialreport  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the excel report file MarketPotentialReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Gridmpr.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   Marketpotentialreport Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:Marketpotentialreport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Marketpotentialreport Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}