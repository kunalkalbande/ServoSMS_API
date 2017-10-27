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
using System.Data .SqlClient ;
using Servosms.Sysitem.Classes ;
using System.Net; 
using System.Net.Sockets ;
using System.IO ;
using System.Text;
using DBOperations;
using RMG;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for VehicleLogReport.
	/// </summary>
	public partial class VehicleLogReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid="";
		public bool f= true;
	
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
				if(! IsPostBack)
				{
					getVehicleNo();
					grdLog.Visible=false;
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="52";
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
				}
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:vehicle Dialy Log Report.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}
		
		/// <summary>
		/// Fetch the vehicle no and id from vehicleentry table and fills the combo
		/// </summary>
		public void getVehicleNo()
		{
			SqlDataReader SqlDtr = null;
			dbobj.SelectQuery("Select vehicle_no+' VID '+cast(vehicledetail_id as varchar) from vehicleentry",ref SqlDtr);
			Dropvehicleno.Items.Clear();
			Dropvehicleno.Items.Add("Select");  
			while(SqlDtr.Read())
			{
				Dropvehicleno.Items.Add(SqlDtr.GetValue(0).ToString());
			}
			SqlDtr.Close(); 
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

		protected void grdLog_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Returns the date in MM/DD/YYYY format.
		/// </summary>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.Split(new char[]{'/'},str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// returns the route name for passing route Id.
		/// </summary>
		public string getRoute(string str)
		{
			SqlDataReader SqlDtr = null;
			dbobj.SelectQuery("Select Route_name from Route where Route_Id = "+str.Trim(),ref SqlDtr);
			if(SqlDtr.Read())
			{
				str = SqlDtr.GetValue(0).ToString();  
			}
			else
			{
				str = "";
			} 
			return str;
		}

		/// <summary>
		/// Checks the validity of the form .. all the fields are properly filled or not.
		/// </summary>
		public bool checkValidity()
		{
			string ErrorMessage = "";
			bool flag = true;
			if(Dropvehicleno.SelectedIndex  == 0)
			{
				ErrorMessage = ErrorMessage + " - Please Select Vehicle No.\n";
				flag = false;
			}
			if(txtDateFrom.Text.Trim().Equals(""))
			{
				ErrorMessage = ErrorMessage + " - Please Select From Date\n";
				flag = false;
			}
			if(txtDateTo.Text.Trim().Equals(""))
			{
				ErrorMessage = ErrorMessage + " - Please Select To Date\n";
				flag = false;
			}
			
			if(flag == false)
			{
				MessageBox.Show(ErrorMessage);
				return false;
			}
			

			if(System.DateTime.Compare(ToMMddYYYY(txtDateFrom.Text.Trim()),ToMMddYYYY(txtDateTo.Text.Trim())) > 0)
			{
				MessageBox.Show("Date From Should be less than Date To");
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// This is used to view the report.
		/// </summary>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!checkValidity()) 
				{
					return;
				}

				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("Select *,(Meter_reading_Cur - Meter_Reading_Pre) as KM,((Meter_reading_Cur - Meter_Reading_Pre)/Fuel_Used_Qty) as Mileage from VDLB where vehicle_no = right('"+Dropvehicleno.SelectedItem.Text.Trim()+"',4) and cast(floor(cast(DOE as float)) as datetime) >= '"+ToMMddYYYY(txtDateFrom.Text.Trim())+"' and  cast(floor(cast(DOE as float)) as datetime) <= '"+ToMMddYYYY(txtDateTo.Text.Trim())+"'",ref SqlDtr);
				grdLog.DataSource = SqlDtr;
				grdLog.DataBind();
				if(grdLog.Items.Count==0)
				{
					MessageBox.Show("Data not available");
					grdLog.Visible=false;
				}
				else
				{
					grdLog.Visible=true;
				}
				SqlDtr.Close ();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:vehicleLogReport.aspx,Method:btnView_Click "+ " EXCEPTION  "+ex.Message+"  "+ uid ); 
			}			 
		}

		/// <summary>
		/// This is used to print the file.txt with the contact of printServer.
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			// if f is true then call the print else return;
			makingReport();
			if(f)
				Print();
			else
				return;
		}

		/// <summary>
		/// Method to prepare the .txt report file.
		/// </summary>
		public void makingReport()
		{

			/*
													=====================================================
													Vehicle Log Book Report From mm/dd/yyyy To mm/dd/yyyy
													=====================================================
			Vehicle No. : MH 09 78787

			+------+-----------+------+-----+------+-----+-------+------+-----------------------------+--------+--------+------+-------+
			| Fuel |  Vehicle  |Engine|Gear |Grease|Brake|Coolent|Trans.|      Expenses (In Rs.)      |Opening |Closing |  KM. |       |
			|Inward|   Route   |Oil   |Oil  | Used |Oil  | Used  |Oil   |------+--------+------+------| Meter  | Meter  | Move |Mileage|
			|      |           |Used  |Used |      |Used |       |Used  | Toll | Police | Food | Misc.|Reading |Reading |      |       |
			+------+-----------+------+-----+------+-----+-------+------+------+--------+------+------+--------+--------+------+-------+
			 123456 ########### 123456 12345 123456 12345 1234567 123456 123456 12345678 123456 123456 12345678 12345678 123456 1234.00
			*/
			try
			{
				
				if(!checkValidity()) 
				{
					f = false; 
					return;
				}
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\VehicleLogBookReport.txt";
				StreamWriter sw = new StreamWriter(path);
				SqlDataReader SqlDtr = null;
				SqlDataReader SqlDtr1 = null;
				//Mahesh 05.05.007 Start
				/*
				sw.Write((char)27);
				sw.Write('O');
				sw.Write('P');
				sw.Write((char)15);
				*/
				//Mahesh 05.05.007 End
				// Condensed
				sw.Write((char)27);
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
				sw.WriteLine(GenUtil.GetCenterAddr("=====================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Vehicle Log Book Report From "+txtDateFrom.Text.Trim()+" To "+txtDateTo.Text.Trim(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("=====================================================",des.Length));
				sw.WriteLine("Vehicle No. : "+Dropvehicleno.SelectedItem.Text.Substring(0,Dropvehicleno.SelectedItem.Text.IndexOf("VID")).Trim());
				//sw.WriteLine("");
				sw.WriteLine("+------+-----------------------+------+-----+------+-----+-------+------+-----------------------------+--------+--------+------+-------+");
				sw.WriteLine("|      |                       |Engine|Gear |Grease|Brake|Coolent|Trans.|      Expenses (In Rs.)      |Opening |Closing |      |       |");
				sw.WriteLine("| Fuel |    Vehicle Route      |Oil   |Oil  | Used |Oil  | Used  |Oil   |------+--------+------+------| Meter  | Meter  |  KM. |Mileage|");
				sw.WriteLine("|Inward|                       |Used  |Used |      |Used |       |Used  | Toll | Police | Food | Misc.|Reading |Reading | Move |       |");
				sw.WriteLine("+------+-----------------------+------+-----+------+-----+-------+------+------+--------+------+------+--------+--------+------+-------+");
				//             123456 ########### 123456 12345 123456 12345 1234567 123456 123456 12345678 123456 123456 12345678 12345678 123456 1234.00
				//  0        1           2    3     4       5     6       7      8     9        10     11     12         13     14     15

				//info : to set string format.
				string info =" {0,6:f} {1,-23:S} {2,6:f} {3,5:f} {4,6:f} {5,5:f} {6,7:f} {7,6:f} {8,6:f} {9,8:f} {10,6:f} {11,6:f} {12,8:f} {13,8:f} {14,6:f} {15,7:f}";
				string route = "";
				dbobj.SelectQuery("Select *,(Meter_reading_Cur - Meter_Reading_Pre) as KM,((Meter_reading_Cur - Meter_Reading_Pre)/Fuel_Used_Qty) as Mileage from VDLB where vehicle_no = right('"+Dropvehicleno.SelectedItem.Text.Trim()+"',4) and cast(floor(cast(DOE as float)) as datetime) >= '"+ToMMddYYYY(txtDateFrom.Text.Trim())+"' and  cast(floor(cast(DOE as float)) as datetime) <= '"+ToMMddYYYY(txtDateTo.Text.Trim())+"'",ref SqlDtr);
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						route = "";
						dbobj.SelectQuery("Select route_name from route where route_id ="+SqlDtr["Vehicle_Route"].ToString().Trim(),ref SqlDtr1);
						if(SqlDtr1.Read())
						{
							route = SqlDtr1.GetValue(0).ToString();  
						}
						SqlDtr1.Close();
						sw.WriteLine(info,SqlDtr["Fuel_Used_Qty"].ToString().Trim(),
							route,
							SqlDtr["Engine_Oil_Qty"].ToString().Trim(),
							SqlDtr["Gear_Oil_Qty"].ToString().Trim(),
							SqlDtr["Grease_Qty"].ToString().Trim(),
							SqlDtr["Brake_Oil_Qty"].ToString().Trim(),
							SqlDtr["Coolent_Qty"].ToString().Trim(),
							SqlDtr["Trans_Oil_Qty"].ToString().Trim(),
							SqlDtr["Toll"].ToString().Trim(),
							SqlDtr["Police"].ToString().Trim(),
							SqlDtr["Food"].ToString().Trim(),
							SqlDtr["Misc"].ToString().Trim(),
							SqlDtr["Meter_Reading_Pre"].ToString().Trim(),
							SqlDtr["Meter_Reading_Cur"].ToString().Trim(),
							SqlDtr["KM"].ToString().Trim(),
							GenUtil.strNumericFormat(SqlDtr["Mileage"].ToString().Trim()));
						 
						               
					
					}
				}
				else
				{
					MessageBox.Show("Data not available");
					SqlDtr.Close();
					sw.Close();
					f = false; 
					return;
				}
				SqlDtr.Close();
				sw.WriteLine("+------+-----------------------+------+-----+------+-----+-------+------+------+--------+------+------+--------+--------+------+-------+");
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:vehicleLogReport.aspx,Method:makingReport() "+ " EXCEPTION  "+ex.Message+"  "+ uid ); 
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			//InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null,SqlDtr1=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\VehicleLogReport.xls";
			StreamWriter sw = new StreamWriter(path);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+txtDateTo.Text);
			sw.WriteLine("Vehicle No\t"+Dropvehicleno.SelectedItem.Text);
			sw.WriteLine();
			sw.WriteLine("Fuel Inward\tVehicle Route\tEngine Oil Used\tGear Oil Used\tGrease Used\tBrake Oil Used\tCoolent Used\ttansaction Oil Used\tExp. Toll\tExp. Police\tExp. Food\tExp. Misc\tOpening Meter Reading\tClosing Meter Reading\tKM.\tMileage");
			string route = "";
			dbobj.SelectQuery("Select *,(Meter_reading_Cur - Meter_Reading_Pre) as KM,((Meter_reading_Cur - Meter_Reading_Pre)/Fuel_Used_Qty) as Mileage from VDLB where vehicle_no = right('"+Dropvehicleno.SelectedItem.Text.Trim()+"',4) and cast(floor(cast(DOE as float)) as datetime) >= '"+ToMMddYYYY(txtDateFrom.Text.Trim())+"' and  cast(floor(cast(DOE as float)) as datetime) <= '"+ToMMddYYYY(txtDateTo.Text.Trim())+"'",ref SqlDtr);
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					route = "";
					dbobj.SelectQuery("Select route_name from route where route_id ="+SqlDtr["Vehicle_Route"].ToString().Trim(),ref SqlDtr1);
					if(SqlDtr1.Read())
					{
						route = SqlDtr1.GetValue(0).ToString();  
					}
					SqlDtr1.Close();
					sw.WriteLine(SqlDtr["Fuel_Used_Qty"].ToString().Trim()+"\t"+
						route+"\t"+
						SqlDtr["Engine_Oil_Qty"].ToString().Trim()+"\t"+
						SqlDtr["Gear_Oil_Qty"].ToString().Trim()+"\t"+
						SqlDtr["Grease_Qty"].ToString().Trim()+"\t"+
						SqlDtr["Brake_Oil_Qty"].ToString().Trim()+"\t"+
						SqlDtr["Coolent_Qty"].ToString().Trim()+"\t"+
						SqlDtr["Trans_Oil_Qty"].ToString().Trim()+"\t"+
						SqlDtr["Toll"].ToString().Trim()+"\t"+
						SqlDtr["Police"].ToString().Trim()+"\t"+
						SqlDtr["Food"].ToString().Trim()+"\t"+
						SqlDtr["Misc"].ToString().Trim()+"\t"+
						SqlDtr["Meter_Reading_Pre"].ToString().Trim()+"\t"+
						SqlDtr["Meter_Reading_Cur"].ToString().Trim()+"\t"+
						SqlDtr["KM"].ToString().Trim()+"\t"+
						GenUtil.strNumericFormat(SqlDtr["Mileage"].ToString().Trim()));
				}
			}
			else
			{
				MessageBox.Show("Data not available");
				SqlDtr.Close();
				sw.Close();
				return;
			}
			SqlDtr.Close();
			sw.Close();
		}

		/// <summary>
		/// this is used to print the file.txt .
		/// </summary>
		public void Print()
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
					CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\VehicleLogBookReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					//CreateLogFiles.ErrorLog("Form:Vehiclereport.aspx,Method:print"+ "  Daily sales record  Printed   userid  "+uid);
					CreateLogFiles.ErrorLog("Form:Vehicle_report.aspx,Method:print"+ " Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Method:print"+ " EXCEPTION "  +ane.Message+" userid "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Method:print"+ " EXCEPTION "  +se.Message+" userid "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Method:print"+ " EXCEPTION "  +es.Message+" userid "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Method:print"+ " EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel file VehicleLogReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(grdLog.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    VehicleLogReport Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:VehicleLogReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    VehicleLogReport Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}