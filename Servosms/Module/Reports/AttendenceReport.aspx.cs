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
using RMG;
using System.Data .SqlClient ;
using System.Net; 
using System.Net.Sockets ;
using System.IO ;
using System.Text;
using DBOperations;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class AttendenceReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public static string[] date;
		public static int Month;
		public static int Day;
		string uid;
		public static int Flag=0;
	
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
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:pageload"+ ex.Message+ "  EXCEPTION" +uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(DropYear.SelectedIndex!=0 && DropMonth.SelectedIndex!=0)
			{
				//btnExcel.Visible=false;
				Month = DropMonth.SelectedIndex;
				Day = DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),Month);
				//date = new string[Day];
				//DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text
				
			}

			
			if(!Page.IsPostBack)
			{
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="1";
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
		/// This method is used to view the report, if Flag = 1 then view the report otherwise not view the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			
			Flag=1;
//			string[] arr1 = new string[Day];
//			string[] arr2 = new string[Day];
//			string FromDate=DropMonth.SelectedIndex+"/1/"+DropYear.SelectedItem.Text;
//			string ToDate=DropMonth.SelectedIndex+"/"+Day.ToString()+"/"+DropYear.SelectedItem.Text;
//			InventoryClass obj = new InventoryClass();
//			SqlDataReader rdr,rdr1=null;
//			
//			for(int i=0,j=1;i<Day;i++,j++)
//			{
//				arr1[i]=DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text;
//				arr2[i]="A";
//			}
//			dbobj.SelectQuery("select emp_id from employee",ref rdr1);
//			while(rdr1.Read())
//			{
//				rdr = obj.GetRecordSet("select * from attandance_register where att_date>='"+FromDate+"' and att_date<='"+ToDate+"' and emp_id='"+rdr1.GetValue(0).ToString()+"' order by att_date");
//				//rdr = obj.GetRecordSet("select * from attandance_register where att_date>='9/1/2007' and att_date<='9/31/2007' and emp_id=100002 order by att_date");
//				while(rdr.Read())
//				{
//					for(int i=0;i<arr1.Length;i++)
//					{
//						if(rdr.GetValue(0).ToString().Equals(arr1[i].ToString()))
//						{
//							arr2[i]="P";
//							break;
//						}
//					}
//				}
//				rdr.Close();
//			}
//			for(int i=0;i<arr1.Length;i++)
//			{
//				MessageBox.Show(arr1[i].ToString()+" : "+arr2[i].ToString());
//			}
		}

		/// <summary>
		/// Prepares the report file AttendenceReport.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			if(Flag==0)
			{
				MessageBox.Show("Please Click The View Button First");
				return;
			}
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
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt   Attendence Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\AttendenceReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Attendence Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Attendence Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Attendence Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt   Attendence Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to prepare the report file .txt to print
		/// </summary>
		public void makingReport()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\AttendenceReport.txt";
			StreamWriter sw = new StreamWriter(path);

			//***added by vishnu ***//
			sw.Write((char)27);
			sw.Write((char)67);
			sw.Write((char)0);
			sw.Write((char)12);

			sw.Write((char)27);
			sw.Write((char)78);
			sw.Write((char)5);

			sw.Write((char)27); //added by vishnu for condensed
			sw.Write((char)15);//
			
			sw.WriteLine("");
			//**********

			/**********Add by vikas 20.12.2012***********************/
			string[] DayName = new string[Day];   //add by vikas 20.12.2012
			for(int i=0,j=1;i<Day;i++,j++)
			{
				DateTime D_Name=new DateTime(int.Parse(DropYear.SelectedItem.Text),DropMonth.SelectedIndex,j);
				string Days=D_Name.DayOfWeek.ToString();
				//Days=Days.Substring(0,1);
				DayName[i]=Days;
			}
			/**********End***********************/

			string Head="|                         |";
			string msg="|   Employee Name / Day   |";
			string des="+-------------------------+";
			int k=0;
			for(int i=1;i<=Day;i++)
			{
				if(i.ToString().Length!=1)
					msg+=i.ToString()+"|";
				else
					msg+=i.ToString()+" |";
				des+="--+";

				/**********add by vikas 20.12.2012**********************/
				string Days=DayName[k].Substring(0,1);
				if(i.ToString().Length!=1)
					Head+=Days.ToString()+" |";
				else
					Head+=Days.ToString()+" |";
				k++;
				/**********End**********************/
			}
			msg+="Total P|Total A|";
			des+="-------+-------+";
		    Head+="                ";
			//**********
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			for(int i=0;i<des.Length;i++)
			{
				sw.Write("-");
			}
			sw.WriteLine();
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("-------------------",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("ATTENDENCE REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("-------------------",des.Length));
			sw.WriteLine(" Month : "+DropMonth.SelectedItem.Text+", Year : "+DropYear.SelectedItem.Text);
			sw.WriteLine(des);
			sw.WriteLine(msg);
			sw.WriteLine(Head);           // add by vikas 20.12.2012
			sw.WriteLine(des);
			//***********************************
			string[] arr1 = new string[Day];
			string[] arr2 = new string[Day];

			string FromDate=DropMonth.SelectedIndex+"/1/"+DropYear.SelectedItem.Text;
			string ToDate=DropMonth.SelectedIndex+"/"+Day.ToString()+"/"+DropYear.SelectedItem.Text;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr,rdr1=null;
			for(int i=0,j=1;i<Day;i++,j++)
			{
				arr1[i]=DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text;
				//arr2[i]="A";
			}
			string emp="";
			//coment by vikas 20.12.2012 dbobj.SelectQuery("select emp_id,emp_name from employee",ref rdr1);
			dbobj.SelectQuery("select emp_id,emp_name from employee where status=1",ref rdr1);
			while(rdr1.Read())
			{
				for(int i=0,j=1;i<Day;i++,j++)
				{
					//arr1[i]=DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text;
					//add by vikas 20.12.2012 arr2[i]="A";
					if(DayName[i]=="Sunday")
						arr2[i]="S";
					else
						arr2[i]="A";
				}
				int countP=0,countA=0;
				emp=rdr1.GetValue(1).ToString();
				rdr = obj.GetRecordSet("select * from attandance_register where att_date>='"+FromDate+"' and att_date<='"+ToDate+"' and emp_id='"+rdr1.GetValue(0).ToString()+"' and status=1 order by att_date");
				while(rdr.Read())
				{
					for(int i=0;i<arr1.Length;i++)
					{
						
						//if(rdr.GetValue(0).ToString().Equals(arr1[i].ToString()))
						if(GenUtil.trimDate(rdr.GetValue(0).ToString()).Equals(arr1[i].ToString()))
						{
							arr2[i]="P";
							countP++;
							break;
						}
					}
				}
				rdr.Close();
				countA=Day-countP;
				sw.Write(" "+emp.ToString());
				//for(int i=0;i<=30-emp.Length;i++)
				for(int i=0;i<=25-emp.Length;i++)
				{
					sw.Write(" ");
				}
				for(int i=0;i<arr1.Length;i++)
				{
					sw.Write(arr2[i].ToString()+"  ");
				}
				sw.Write(" "+countP.ToString());
				for(int i=0;i<=6-countP.ToString().Length;i++)
				{
					sw.Write(" ");
				}
				sw.Write(" "+countA.ToString());
				for(int i=0;i<=6-countP.ToString().Length;i++)
				{
					sw.Write(" ");
				}
				sw.WriteLine();
			}
			sw.WriteLine(des);
			//***********************************
			//deselect condensed
			sw.Write((char)27);
			sw.Write((char)18);//ad
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertIntoExcel()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\AttendenceReport1.xls";
			StreamWriter sw = new StreamWriter(path);


			/**********Add by vikas 20.12.2012***********************/
			string[] DayName = new string[Day];   //add by vikas 20.12.2012
			for(int i=0,j=1;i<Day;i++,j++)
			{
				DateTime D_Name=new DateTime(int.Parse(DropYear.SelectedItem.Text),DropMonth.SelectedIndex,j);
				string Days=D_Name.DayOfWeek.ToString();
				//Days=Days.Substring(0,1);
				DayName[i]=Days;
			}
			/**********End***********************/
			int k=0;
			string Head="\t";
			string msg="Employee Name / Day\t";
			for(int i=1;i<=Day;i++)
			{
				if(i.ToString().Length!=1)
					msg+=i.ToString()+"\t";
				else
					msg+=i.ToString()+"\t";

				/**********add by vikas 20.12.2012**********************/
				string Days=DayName[k].Substring(0,1);
				Head+=Days.ToString()+"\t";
				k++;
				/**********End**********************/
			}
			msg+="Total P\tTotal A\t";
			sw.WriteLine("Month\t"+DropMonth.SelectedItem.Text+"\tYear\t"+DropYear.SelectedItem.Text);
			sw.WriteLine(msg);
			sw.WriteLine(Head);
			//***********************************
			string[] arr1 = new string[Day];
			string[] arr2 = new string[Day];
			string FromDate=DropMonth.SelectedIndex+"/1/"+DropYear.SelectedItem.Text;
			string ToDate=DropMonth.SelectedIndex+"/"+Day.ToString()+"/"+DropYear.SelectedItem.Text;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr,rdr1=null;
			for(int i=0,j=1;i<Day;i++,j++)
			{
				arr1[i]=DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text;
				//arr2[i]="A";
			}
			string emp="";
			//coment by vikas 20.12.2012 dbobj.SelectQuery("select emp_id,emp_name from employee",ref rdr1);
			dbobj.SelectQuery("select emp_id,emp_name from employee where status=1",ref rdr1);
			while(rdr1.Read())
			{
				for(int i=0,j=1;i<Day;i++,j++)
				{
					//arr1[i]=DropMonth.SelectedIndex+"/"+j+"/"+DropYear.SelectedItem.Text;
					//arr2[i]="A";
					if(DayName[i]=="Sunday")
						arr2[i]="S";
					else
						arr2[i]="A";
				}
				int countP=0,countA=0;
				emp=rdr1.GetValue(1).ToString();
				rdr = obj.GetRecordSet("select * from attandance_register where att_date>='"+FromDate+"' and att_date<='"+ToDate+"' and emp_id='"+rdr1.GetValue(0).ToString()+"' and status=1 order by att_date");
				while(rdr.Read())
				{
					for(int i=0;i<arr1.Length;i++)
					{
						//if(rdr.GetValue(0).ToString().Equals(arr1[i].ToString()))
						if(GenUtil.trimDate(rdr.GetValue(0).ToString()).Equals(arr1[i].ToString()))
						{
							arr2[i]="P";
							countP++;
							break;
						}
					}
				}
				rdr.Close();
				countA=Day-countP;
				sw.Write(emp.ToString()+"\t");
				for(int i=0;i<arr1.Length;i++)
				{
					sw.Write(arr2[i].ToString()+"\t");
				}
				sw.Write(countP.ToString()+"\t");
				sw.Write(countA.ToString());
				sw.WriteLine();
			}
			//***********************************
			//deselect condensed
			sw.Write((char)27);
			sw.Write((char)18);//ad
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file AttendenceReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Flag==1)
				{
					ConvertIntoExcel();
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Method: btnExcel_Click, Attendence Report Convert Into Excel Format ,  userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:AttendenceReport.aspx,Method:btnExcel_Click   Attendence Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}
