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
using DBOperations;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for ClaimAnalysis.
	/// </summary>
	public partial class ClaimAnalysis : System.Web.UI.Page
	{
		public static int View = 0;
		string uid;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId
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
				CreateLogFiles.ErrorLog("Form:ClaimAnalysis.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				View = 0;
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="6";
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
		/// This method is used to view the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			View = 1;
		}

		/// <summary>
		/// Prepares the report file ClaimAnalysis.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				Socket sender1 = new Socket(AddressFamily.InterNetwork, 
					SocketType.Stream, ProtocolType.Tcp );

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Claim Analysis Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ClaimAnalysisReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:ClaimAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Claim Analysis Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:ClaimAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Claim Analysis Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:ClaimAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Claim Analysis Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:ClaimAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Claim Analysis Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			if(View==1)
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2);
				string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
				Directory.CreateDirectory(strExcelPath);
				string path = home_drive+@"\Servosms_ExcelFile\Export\ClaimAnalysisReport.xls";
				StreamWriter sw = new StreamWriter(path);
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = null;
				string[] SameMon = {"January"+DropYearFrom.SelectedItem.Text,"February"+DropYearFrom.SelectedItem.Text,"March"+DropYearFrom.SelectedItem.Text,"April"+DropYearFrom.SelectedItem.Text,"May"+DropYearFrom.SelectedItem.Text,"June"+DropYearFrom.SelectedItem.Text,"July"+DropYearFrom.SelectedItem.Text,"August"+DropYearFrom.SelectedItem.Text,"September"+DropYearFrom.SelectedItem.Text,"October"+DropYearFrom.SelectedItem.Text,"November"+DropYearFrom.SelectedItem.Text,"December"+DropYearFrom.SelectedItem.Text};
				string[] DiffMon = {"April"+DropYearFrom.SelectedItem.Text,"May"+DropYearFrom.SelectedItem.Text,"June"+DropYearFrom.SelectedItem.Text,"July"+DropYearFrom.SelectedItem.Text,"August"+DropYearFrom.SelectedItem.Text,"September"+DropYearFrom.SelectedItem.Text,"October"+DropYearFrom.SelectedItem.Text,"November"+DropYearFrom.SelectedItem.Text,"December"+DropYearFrom.SelectedItem.Text,"January"+DropYearTo.SelectedItem.Text,"February"+DropYearTo.SelectedItem.Text,"March"+DropYearTo.SelectedItem.Text};
				int Flag = 0,Count=0;
				double Tot = 0;
				double[] TotalAmt = null;
				ArrayList Header = new ArrayList();
				for(int i=0;i<SameMon.Length;i++)
				{
					if(Flag==1)
					{
						if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
							rdr = obj.GetRecordSet("select count(*) from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
						else
							rdr = obj.GetRecordSet("select count(*) from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
						if(rdr.Read())
						{
							if(int.Parse(rdr.GetValue(0).ToString())>Count)
							{
								Count=0;
								Flag=0;
								Header = new ArrayList();
							}
						}
						rdr.Close();
					}
					if(Flag==0)
					{
						if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
						else
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
						if(rdr.HasRows)
						{
							Flag=1;
							Header.Add("Month");
							//sw.Write("Month");
							while(rdr.Read())
							{
								//sw.Write("\t"+rdr["TypeofClaim"].ToString());
								Header.Add(rdr["TypeofClaim"].ToString());
								Count++;
							}
							//sw.Write("\tTotal");
							Header.Add("Total");
							//sw.WriteLine();
						}
						rdr.Close();
					}
					//else
					//{
					//	break;
					//}
				}
				int NoofCol=Count;
				TotalAmt = new double[Count+1];
				for(int l=0;l<Header.Count;l++)
				{
					sw.Write(Header[l].ToString()+"\t");
				}
				sw.WriteLine();
				if(Flag==1)
				{
					if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
					{
						for(int i=0;i<SameMon.Length;i++)
						{
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
							if(rdr.HasRows)
							{
								Tot=0;Count=-1;
								sw.Write(SameMon[i].ToString());
								while(rdr.Read())
								{
									sw.Write("\t"+rdr["Amount"].ToString());
									Tot+=double.Parse(rdr["Amount"].ToString());
									TotalAmt[++Count]+=double.Parse(rdr["Amount"].ToString());
									//MessageBox.Show(i.ToString()+"::"+Count.ToString()+":"+TotalAmt[Count].ToString());
								}
								while(NoofCol!=Count+1)
								{
									sw.Write("\t0");
									Count++;
								}
								sw.Write("\t"+Tot.ToString());
								sw.WriteLine();
								TotalAmt[++Count]+=Tot;
							}
							else
							{
								sw.Write(SameMon[i].ToString());
								for(int k=0;k<TotalAmt.Length;k++)
								{
									sw.Write("\t0");
								}
								sw.WriteLine();
							}
							rdr.Close();
						}
					}
					else
					{
						for(int i=0;i<DiffMon.Length;i++)
						{
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
							if(rdr.HasRows)
							{
								Tot=0;Count=-1;
								sw.Write(DiffMon[i].ToString());
								while(rdr.Read())
								{
									sw.Write("\t"+rdr["Amount"].ToString());
									Tot+=double.Parse(rdr["Amount"].ToString());
									TotalAmt[++Count]+=double.Parse(rdr["Amount"].ToString());
									//MessageBox.Show(i.ToString()+"::"+Count.ToString()+":"+TotalAmt[Count].ToString());
								}
								while(NoofCol!=Count+1)
								{
									sw.Write("\t0");
									Count++;
								}
								sw.Write("\t"+Tot.ToString());
								sw.WriteLine();
								TotalAmt[++Count]+=Tot;
							}
							else
							{
								sw.Write(DiffMon[i].ToString());
								for(int k=0;k<TotalAmt.Length;k++)
								{
									sw.Write("\t0");
								}
								sw.WriteLine();
							}
							rdr.Close();
						}
					}
					sw.Write("Total");
					for(int j=0;j<TotalAmt.Length;j++)
					{
						sw.Write("\t"+TotalAmt[j].ToString());
					}
					sw.WriteLine();
				}
				else
				{
					MessageBox.Show("Data Not Available");
				}
				sw.Close();			
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			if(View==1)
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ClaimAnalysisReport.txt";
				StreamWriter sw = new StreamWriter(path);
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = null;
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
				string[] SameMon = {"January"+DropYearFrom.SelectedItem.Text,"February"+DropYearFrom.SelectedItem.Text,"March"+DropYearFrom.SelectedItem.Text,"April"+DropYearFrom.SelectedItem.Text,"May"+DropYearFrom.SelectedItem.Text,"June"+DropYearFrom.SelectedItem.Text,"July"+DropYearFrom.SelectedItem.Text,"August"+DropYearFrom.SelectedItem.Text,"September"+DropYearFrom.SelectedItem.Text,"October"+DropYearFrom.SelectedItem.Text,"November"+DropYearFrom.SelectedItem.Text,"December"+DropYearFrom.SelectedItem.Text};
				string[] DiffMon = {"April"+DropYearFrom.SelectedItem.Text,"May"+DropYearFrom.SelectedItem.Text,"June"+DropYearFrom.SelectedItem.Text,"July"+DropYearFrom.SelectedItem.Text,"August"+DropYearFrom.SelectedItem.Text,"September"+DropYearFrom.SelectedItem.Text,"October"+DropYearFrom.SelectedItem.Text,"November"+DropYearFrom.SelectedItem.Text,"December"+DropYearFrom.SelectedItem.Text,"January"+DropYearTo.SelectedItem.Text,"February"+DropYearTo.SelectedItem.Text,"March"+DropYearTo.SelectedItem.Text};
				int Flag = 0,Count=0;
				double Tot = 0;
				double[] TotalAmt = null;
				string des = "",Header = "",subHead="";
				ArrayList Header1=new ArrayList();
				for(int i=0;i<SameMon.Length;i++)
				{
					if(Flag==1)
					{
						if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
							rdr = obj.GetRecordSet("select count(*) from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
						else
							rdr = obj.GetRecordSet("select count(*) from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
						if(rdr.Read())
						{
							if(int.Parse(rdr.GetValue(0).ToString())>Count)
							{
								Count=0;
								Flag=0;
								Header1 = new ArrayList();
							}
						}
						rdr.Close();
					}
					if(Flag==0)
					{
						if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
						else
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
						if(rdr.HasRows)
						{
							Flag=1;
							//sw.Write("    Month     ");
							Header1.Add("     Month     ");
							Header="+--------------";
							des = "---------------";
							while(rdr.Read())
							{
								Header+="+------------";
								des+="-------------";
								//sw.Write(rdr["TypeofClaim"].ToString());
								string amt = rdr["TypeofClaim"].ToString();
								if(rdr["TypeofClaim"].ToString().Length>13)
								{
									amt=amt.Substring(0,12);
									//sw.Write(amt);
									Header1.Add(" "+amt);
								}
								else
								{
									//sw.Write(rdr["TypeofClaim"].ToString());
									subHead=rdr["TypeofClaim"].ToString();
									for(int m=rdr["TypeofClaim"].ToString().Length;m<13;m++)
									{
										//sw.Write(" ");
										subHead+=" ";
									}
									Header1.Add(" "+subHead);
								}
								Count++;
							}
							//sw.Write("\tTotal");
							subHead=" Total";
							for(int m=4;m<13;m++)
							{
								//sw.Write(" ");
								subHead+=" ";
							}
							Header1.Add(subHead);
							Header+="+------------+";
							des+="--------------";
							//sw.WriteLine();
						}
						rdr.Close();
					}
					//else
					//{
					//	break;
					//}
				}
				int NoofCol=Count;
				TotalAmt = new double[Count+1];
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("=========================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Claim Analysis Report From "+DropYearFrom.SelectedItem.Text+" to "+DropYearTo.SelectedItem.Text,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("=========================================",des.Length));
				sw.WriteLine(Header);
				for(int l=0;l<Header1.Count;l++)
				{
					sw.Write(Header1[l].ToString());
				}
				sw.WriteLine();
				sw.WriteLine(Header);
				if(Flag==1)
				{
					if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
					{
						
						for(int i=0;i<SameMon.Length;i++)
						{
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
							if(rdr.HasRows)
							{
								Tot=0;Count=-1;
								sw.Write(" "+SameMon[i].ToString());
								for(int m=SameMon[i].ToString().Length;m<15;m++)
								{
									sw.Write(" ");
								}
								while(rdr.Read())
								{
									sw.Write(rdr["Amount"].ToString());
									for(int m=rdr["Amount"].ToString().Length;m<13;m++)
									{
										sw.Write(" ");
									}
									Tot+=double.Parse(rdr["Amount"].ToString());
									TotalAmt[++Count]+=double.Parse(rdr["Amount"].ToString());
									//MessageBox.Show(i.ToString()+"::"+Count.ToString()+":"+TotalAmt[Count].ToString());
								}
								while(NoofCol!=Count+1)
								{
									sw.Write("0            ");
									Count++;
								}
								sw.Write(Tot.ToString());
								for(int m=Tot.ToString().Length;m<14;m++)
								{
									sw.Write(" ");
								}
								sw.WriteLine();
								TotalAmt[++Count]+=Tot;
							}
							else
							{
								sw.Write(" "+SameMon[i].ToString());
								for(int m=SameMon[i].ToString().Length;m<15;m++)
								{
									sw.Write(" ");
								}
								for(int k=0;k<TotalAmt.Length;k++)
								{
									sw.Write("0");
									for(int m=1;m<14;m++)
									{
										sw.Write(" ");
									}
								}
								sw.WriteLine();
							}
							rdr.Close();
						}
					}
					else
					{
						for(int i=0;i<DiffMon.Length;i++)
						{
							rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
							if(rdr.HasRows)
							{
								Tot=0;Count=-1;
								sw.Write(" "+DiffMon[i].ToString());
								for(int m=DiffMon[i].ToString().Length;m<15;m++)
								{
									sw.Write(" ");
								}
								while(rdr.Read())
								{
									sw.Write(rdr["Amount"].ToString());
									//for(int m=DiffMon[i].ToString().Length;m<14;m++)
									for(int m=rdr["Amount"].ToString().Length;m<13;m++)
									{
										sw.Write(" ");
									}
									Tot+=double.Parse(rdr["Amount"].ToString());
									TotalAmt[++Count]+=double.Parse(rdr["Amount"].ToString());
									//MessageBox.Show(i.ToString()+"::"+Count.ToString()+":"+TotalAmt[Count].ToString());
								}
								while(NoofCol!=Count+1)
								{
									sw.Write("0            ");
									Count++;
								}
								sw.Write(Tot.ToString());
								for(int m=Tot.ToString().Length;m<14;m++)
								{
									sw.Write(" ");
								}
								sw.WriteLine();
								TotalAmt[++Count]+=Tot;
							}
							else
							{
								sw.Write(" "+DiffMon[i].ToString());
								for(int m=DiffMon[i].ToString().Length;m<15;m++)
								{
									sw.Write(" ");
								}
								for(int k=0;k<TotalAmt.Length;k++)
								{
									sw.Write("0");
									for(int m=2;m<14;m++)
									{
										sw.Write(" ");
									}
								}
								sw.WriteLine();
							}
							rdr.Close();
						}
					}
					sw.WriteLine(Header);
					sw.Write(" Total");
					for(int m=4;m<13;m++)
					{
						sw.Write(" ");
					}
					for(int j=0;j<TotalAmt.Length;j++)
					{
						sw.Write(" "+TotalAmt[j].ToString());
						for(int m=TotalAmt[j].ToString().Length;m<12;m++)
						{
							sw.Write(" ");
						}
					}
					sw.WriteLine();
					sw.WriteLine(Header);
				}
				else
				{
					MessageBox.Show("Data Not Available");
				}
				sw.Close();			
			}
		}

		/// <summary>
		/// Prepares the excel report file ClaimAnalysis.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:Claimanalysis.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Claim Analysis Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:ClaimAnalysis.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Claim Analysis Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}