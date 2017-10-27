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
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Text;
using RMG;
using DBOperations;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for MechanicReport.
	/// </summary>
	public partial class MechanicReport : System.Web.UI.Page
	{
		string uid;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString ());
				if(!Page.IsPostBack)
				{
					DataGrid1.Visible=false;
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="21";
					string[,] Priv=(string[,]) Session["Privileges"];
					for(i=0;i<Priv.GetLength(0);i++)
					{
						if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
						{						
							View_Flag=Priv[i,2];
							Add_Flag=Priv[i,3];
							Edit_Flag=Priv[i,4];
							Del_Flag=Priv[i,5];

							break;
						}
					}	
					Cache["View"]=View_Flag;
					if(Add_Flag=="0" && Edit_Flag=="0" && Del_Flag=="0" && View_Flag=="0")
					{
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					}
					if(View_Flag=="0")
						//btnview.Enabled=false;
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					#endregion
				
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Method:Page_Load, userid  "+uid );
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Method:Page_Load, EXCEPTION "+ex.Message+"  userid  "+uid );
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
		/// This is a method to Make sorting on the clicking of headertext of datagrid. 
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
				CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Method:SortCommand_Click"+" Mechanic Report "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		/// <summary>
		/// This method is used to bind the datagrid with the help of making query.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string  sql="select distinct b.state r1,me.mccd r2,me.mcname r3,me.mctype r4,me.place r5,cme.customername r6,c.cust_type r7 from beat_master b,machanic_entry me, customermechanicentry cme,customer c where b.state in(select state from beat_master where city =me.place) and cme.customername in(select customername from customermechanicentry where customermechid=me.custid) and c.cust_type in(select cust_type from customer where cust_name=cme.customername)";
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customermechanicentry");
			DataTable dtcustomer=ds.Tables["customermechanicentry"]; 
			//da.Fill(ds,"beat_master","machanic_entry","customermechanicentry ","customer");
			//DataTable dtcustomer=ds.Tables["beat_master","machanic_entry","customermechanicentry ","customer"]; 
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			DataGrid1.DataSource=dv;
			if(dv.Count!=0)
			{
				DataGrid1.DataBind();
				DataGrid1.Visible=true;
			}
			else
			{
				DataGrid1.Visible=false;
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
				
				//**	SqlConnection con;
				//**	con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//**	con.Open ();
				//**	SqlDataReader dtr;
				//**	SqlCommand cmd=new SqlCommand("select distinct b.state r1,me.mccd r2,me.mcname r3,me.mctype r4,me.place r5,cme.customername r6,c.cust_type r7 from beat_master b,machanic_entry me, customermechanicentry cme,customer c where b.state in(select state from beat_master where city =me.place) and cme.customername in(select customername from customermechanicentry where customermechid=me.custid) and c.cust_type in(select cust_type from customer where cust_name=cme.customername)",con);
				//**	dtr=cmd.ExecuteReader();
				//**	DataGrid1.DataSource=dtr;
				//**	if(dtr.HasRows)
				//**	{
				//**		DataGrid1.DataBind();
				//**		DataGrid1.Visible=true;
				//**	}
				//**	else
				//**	{
				//**		MessageBox.Show("Data not available");
				//**		DataGrid1.Visible=false;
				//**		return;
				//**	}
				strorderby="r1 ASC";
				Session["Column"]="r1";
				Session["order"]="ASC";
				Bindthedata();
				
				CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Method:btnView, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// This method is used to make the format for print .
		/// </summary>
		public void makingReport()
		{
			/*
										======================                              
										   PRICE LIST REPORT                                 
										======================                              
+-------+----------------------+-----------+-----------+-----------+----------+
	  |Prod_ID|  Product Name           | Pack_Type | Pur_Rate  | Sal_Rate  |Eff_Date  |
	  +-------+-------------------------+-----------+-----------+-----------+----------+
	   1001    1234567890123456789012345 1X20777     12345678.00 12345678.00 DD/MM/YYYY
			*/
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\MechanicReport.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			//string strDate = "";

			sql="select distinct b.state r1,me.mccd r2,me.mcname r3,me.mctype r4,me.place r5,cme.customername r6,c.cust_type r7 from beat_master b,machanic_entry me, customermechanicentry cme,customer c where b.state in(select state from beat_master where city =me.place) and cme.customername in(select customername from customermechanicentry where customermechid=me.custid) and c.cust_type in(select cust_type from customer where cust_name=cme.customername)";
			sql=sql+" order by "+Cache["strorderby"];
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
			string des="---------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("MECHANIC REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=================",des.Length));
			sw.WriteLine("+----------+-------------+---------------+----------+-------------------------+---------------+---------------+");
			sw.WriteLine("| District |Mechanic Code|     Name      |   Type   |      Under Firm         |   Firm Type   |    Place      |");
			sw.WriteLine("+----------+-------------+---------------+----------+-------------------------+---------------+---------------+");
			//             1001567 12345678901234567890123 12345678901 12345678.00 12345678.00 DD/MM/YYYY
        
			if(rdr.HasRows)
			{
				// info : to set the format the displaying string.
				info = " {0,-10:S}  {1,-13:F} {2,-15:S} {3,-10:S} {4,-25:S} {5,-15:S} {6,-15:S}"; 
				while(rdr.Read())
				{
										                                         
					/*sw.WriteLine(info,rdr["Prod_ID"].ToString().Trim(),
						rdr["Prod_Name"].ToString().Trim(),
						rdr["Pack_Type"].ToString(),
						GenUtil.strNumericFormat(rdr["Pur_Rate"].ToString().Trim()),
						GenUtil.strNumericFormat(rdr["sal_Rate"].ToString().Trim()),
						GenUtil.str2DDMMYYYY(strDate));*/
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString(),10),
						rdr["r2"].ToString(),
						GenUtil.TrimLength(rdr["r3"].ToString(),15),
						GenUtil.TrimLength(rdr["r4"].ToString(),10),
						GenUtil.TrimLength(rdr["r6"].ToString(),25),
						GenUtil.TrimLength(rdr["r7"].ToString(),15),
						GenUtil.TrimLength(rdr["r5"].ToString(),15));

				}
			}
			sw.WriteLine("+----------+-------------+---------------+----------+-------------------------+---------------+---------------+");
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\MechanicReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			sql="select distinct b.state r1,me.mccd r2,me.mcname r3,me.mctype r4,me.place r5,cme.customername r6,c.cust_type r7 from beat_master b,machanic_entry me, customermechanicentry cme,customer c where b.state in(select state from beat_master where city =me.place) and cme.customername in(select customername from customermechanicentry where customermechid=me.custid) and c.cust_type in(select cust_type from customer where cust_name=cme.customername)";
			sql=sql+" order by "+Cache["strorderby"];
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("District\tMechanic Code\tName\tType\tUnder Firm\tFirm Type\tPlace");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["r1"].ToString()+"\t"+
					rdr["r2"].ToString()+"\t"+
					rdr["r3"].ToString()+"\t"+
					rdr["r4"].ToString()+"\t"+
					rdr["r6"].ToString()+"\t"+
					rdr["r7"].ToString()+"\t"+
					rdr["r5"].ToString());

			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This is used to print MechanicReport.txt file before preparing the txt file with the 
		/// help of makingReport() function.
		/// </summary>
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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MechanicReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the excel report file MachenicReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DataGrid1.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   Mechanic Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   Mechanic Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}