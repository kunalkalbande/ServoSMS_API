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
	/// Summary description for DistrictWiseChannelSales.
	/// </summary>
	public partial class DistrictWiseChannelSales : System.Web.UI.Page
	{
		string uid;
		public static int View=0,Count=0;
	
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
				CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSales.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				View=0;
				Count=0;
				txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="12";
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

		/// <summary>
		/// This method is used to show the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(txtDateTo.Text))>0)
			{
				MessageBox.Show("Date From Should be less than Date To");
				View=0;
			}
			else
			{
				View=1;
				GetMonth();
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\DistrictWiseChannelSalesReport.txt";
			StreamWriter sw = new StreamWriter(path);
			DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr1=null;
			ArrayList arrType = new ArrayList();
			ArrayList arrState = new ArrayList();
			double[] arrTotal = null,arrTotal1 = null;
			string Header="",desdes="",des="";
			string Header1="",desdes1="",des1="";
				
			//dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
			if(radDetails.Checked)
				dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select customertypename from customertype where (substring(customertypename,1,2)!='Ro' and substring(customertypename,1,2)!='oe') group by customertypename)",ref rdr1);
			else
				dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' when substring(cust_type,1,2)='Ks' then 'KSK' when substring(cust_type,1,2)='N-' then 'N-KSK' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe' or substring(cust_type,1,2)='Ks' or substring(cust_type,1,2)='n-') group by substring(cust_type,1,2)) union (select customertypename from customertype where (substring(customertypename,1,2)!='Ro' and substring(customertypename,1,2)!='oe' and substring(customertypename,1,2)!='ks' and substring(customertypename,1,2)!='N-') group by customertypename)",ref rdr1);
			if(rdr1.HasRows)
			{
				int cc=0;
				Header="| Distict/Type ";
				desdes="+--------------";
				des="---------------";
				Header1="| Distict/Type ";
				desdes1="+--------------";
				des1="---------------";
				while(rdr1.Read())
				{
					if(cc>10)
					{
						Header1+="| "+rdr1.GetValue(0).ToString();
						for(int i=rdr1.GetValue(0).ToString().Length+1;i<10;i++)
						{
							Header1+=" ";
						}
						desdes1+="+----------";
						des1+="-----------";
						
					}
					else
					{
						Header+="| "+rdr1.GetValue(0).ToString();
						for(int i=rdr1.GetValue(0).ToString().Length+1;i<10;i++)
						{
							Header+=" ";
						}
						desdes+="+----------";
						des+="-----------";
					}
					arrType.Add(rdr1.GetValue(0).ToString());
					cc++;
				}
				if(cc<9)
				{
					Header+="| Total Sales | Monthly Avr. |";
					desdes+="+-------------+--------------+";
					des+="------------------------------";
				}
				else
				{
					Header1+="| Total Sales | Monthly Avr. |";
					desdes1+="+-------------+--------------+";
					des1+="------------------------------";
				}
			}
			rdr1.Close();
			//*********************************
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
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			sw.WriteLine(GenUtil.GetCenterAddr("-------------------------------------------------------------------------------------------",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("District / Channel Wise Summerized Sales Report Form Date : "+txtDateFrom.Text+" To Date : "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("-------------------------------------------------------------------------------------------",des.Length));
			//sw.WriteLine(" From Date : "+txtDateFrom.Text+" , To Date : "+txtDateTo.Text);
			//*********************************
			
			sw.WriteLine(desdes);
			sw.WriteLine(Header);
			sw.WriteLine(desdes);
			
			arrTotal = new double[arrType.Count];
			double Total=0,GTotal=0,GAvrTotal=0;
			//dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
			dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
			while(rdr1.Read())
			{
				arrState.Add(rdr1.GetValue(0).ToString());
			}
			rdr1.Close();
			arrTotal1 = new double[arrState.Count];
			int Flag = 0;
			for(int i=0;i<arrState.Count;i++)
			{
				Total=0;
				sw.Write(" "+arrState[i].ToString());
				//for(int k=arrState[i].ToString().Length;k<=16;k++)
				for(int k=arrState[i].ToString().Length;k<=14;k++)
				{
					sw.Write(" ");
				}
				for(int j=0;j<arrType.Count;j++)
				{
					//dbobj2.SelectQuery("select case when sum(net_amount) is null then '0' else sum(net_amount) end from sales_master sm,customer c where sm.cust_id=c.cust_id and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					//dbobj2.SelectQuery("select case when sum(qty) is null then '0' else sum(qty) end from sales_master sm,customer c,sales_details sd where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					if(j==8)
						Flag=1;
					if(j==11)
						break;
					if(radDetails.Checked)
					{
						if(arrType[j].ToString().ToLower()=="ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type ='"+arrType[j].ToString()+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					}
					else
					{
						if(arrType[j].ToString().ToLower()=="ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else if(arrType[j].ToString().ToLower()=="n-ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					}
					if(rdr1.Read())
					{
						arrTotal[j]+=double.Parse(rdr1.GetValue(0).ToString());
						sw.Write(rdr1.GetValue(0).ToString());
						for(int k=rdr1.GetValue(0).ToString().Length;k<=10;k++)
						{
							sw.Write(" ");
						}
						Total+=double.Parse(rdr1.GetValue(0).ToString());
					}
				}
				arrTotal1[i]=Total;
				if(Flag==0)
				{
					sw.Write(Total.ToString());
					for(int k=Total.ToString().Length;k<=13;k++)
					{
						sw.Write(" ");
					}
					string GAvr=System.Convert.ToString(Total/double.Parse(Count.ToString()));
					sw.Write(GenUtil.strNumericFormat(GAvr));
					for(int k=GAvr.Length;k<=14;k++)
					{
						sw.Write(" ");
					}
					GTotal+=Total;
					GAvrTotal+=double.Parse(GAvr);
					
				}
				sw.WriteLine();
			}
			
			sw.WriteLine(desdes);
			string str=" Total";
			sw.Write(str);
			for(int k=str.ToString().Length;k<=15;k++)
			{
				sw.Write(" ");
			}
			for(int n=0;n<arrTotal.Length;n++)
			{
				if(n>10)
					break;
				sw.Write(arrTotal[n].ToString());
				for(int k=arrTotal[n].ToString().Length;k<=10;k++)
				{
					sw.Write(" ");
				}
			}
			if(Flag==0)
			{
				sw.Write(GTotal.ToString());
				for(int k=GTotal.ToString().Length;k<=13;k++)
				{
					sw.Write(" ");
				}
				sw.Write(GenUtil.strNumericFormat(GAvrTotal.ToString()));
				for(int k=GAvrTotal.ToString().Length;k<=14;k++)
				{
					sw.Write(" ");
				}
				
			}
			sw.WriteLine();
			sw.WriteLine(desdes);

			if(Flag==1)
			{
				sw.WriteLine();
				sw.WriteLine();
				sw.WriteLine(desdes1);
				sw.WriteLine(Header1);
				sw.WriteLine(desdes1);
				
				for(int i=0;i<arrState.Count;i++)
				{
					//Total=0;
					Total=arrTotal1[i];
					sw.Write(" "+arrState[i].ToString());
					//for(int k=arrState[i].ToString().Length;k<=16;k++)
					for(int k=arrState[i].ToString().Length;k<=14;k++)
					{
						sw.Write(" ");
					}
					
					for(int j=11;j<arrType.Count;j++)
					{
						//dbobj2.SelectQuery("select case when sum(net_amount) is null then '0' else sum(net_amount) end from sales_master sm,customer c where sm.cust_id=c.cust_id and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						//dbobj2.SelectQuery("select case when sum(qty) is null then '0' else sum(qty) end from sales_master sm,customer c,sales_details sd where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
//						if(arrType[j].ToString().ToLower()=="ksk")
//							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type ='"+arrType[j].ToString()+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
//						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						if(rdr1.Read())
						{
							arrTotal[j]+=double.Parse(rdr1.GetValue(0).ToString());
							sw.Write(rdr1.GetValue(0).ToString());
							for(int k=rdr1.GetValue(0).ToString().Length;k<=10;k++)
							{
								sw.Write(" ");
							}
							Total+=double.Parse(rdr1.GetValue(0).ToString());
						}
							
					}
					sw.Write(Total.ToString());
					for(int k=Total.ToString().Length;k<=13;k++)
					{
						sw.Write(" ");
					}
					string GAvr=System.Convert.ToString(Total/double.Parse(Count.ToString()));
					sw.Write(GenUtil.strNumericFormat(GAvr));
					for(int k=GAvr.Length;k<=14;k++)
					{
						sw.Write(" ");
					}
					GTotal+=Total;
					GAvrTotal+=double.Parse(GAvr);
					sw.WriteLine();
						
				}
				//				if(Flag!=2)
				//				{
				//					Flag=3;
				//					for(int i=0;i<arrState.Count;i++)
				//					{
				//						sw.Write(Total.ToString());
				//						for(int k=Total.ToString().Length;k<=13;k++)
				//						{
				//							sw.Write(" ");
				//						}
				//						string GAvr=System.Convert.ToString(Total/double.Parse(Count.ToString()));
				//						sw.Write(GenUtil.strNumericFormat(GAvr));
				//						for(int k=GAvr.Length;k<=14;k++)
				//						{
				//							sw.Write(" ");
				//						}
				//						GTotal+=Total;
				//						GAvrTotal+=double.Parse(GAvr);
				//						sw.WriteLine();
				//					}
				//				}
				//***********
				sw.WriteLine(desdes1);
				str=" Total";
				sw.Write(str);
				for(int k=str.ToString().Length;k<=15;k++)
				{
					sw.Write(" ");
				}
				for(int n=11;n<arrTotal.Length;n++)
				{
					sw.Write(arrTotal[n].ToString());
					for(int k=arrTotal[n].ToString().Length;k<=10;k++)
					{
						sw.Write(" ");
					}
				}
				sw.Write(GTotal.ToString());
				for(int k=GTotal.ToString().Length;k<=13;k++)
				{
					sw.Write(" ");
				}
				sw.Write(GenUtil.strNumericFormat(GAvrTotal.ToString()));
				for(int k=GAvrTotal.ToString().Length;k<=14;k++)
				{
					sw.Write(" ");
				}
				sw.WriteLine();
				sw.WriteLine(desdes1);
			}
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file DistrictWiseChannelSales.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
				if(View==1)
					makingReport();
				else
				{
					MessageBox.Show("Please Click The View Button First");
					return;
				}
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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    DistrictWiseChannelSales Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\DistrictWiseChannelSalesReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSales.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    DistrictWiseChannelSales Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSales.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    DistrictWiseChannelSales Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSales.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    DistrictWiseChannelSales Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSales.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    DistrictWiseChannelSales Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\DistrictWiseChannelSalesReport.xls";
			StreamWriter sw = new StreamWriter(path);
			DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr1=null;
			ArrayList arrType = new ArrayList();
			ArrayList arrState = new ArrayList();
			double[] arrTotal = null;
			string Header="";
				
			//dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
			if(radDetails.Checked)
				dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select customertypename from customertype where (substring(customertypename,1,2)!='Ro' and substring(customertypename,1,2)!='oe') group by customertypename)",ref rdr1);
			else
				dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' when substring(cust_type,1,2)='Ks' then 'KSK' when substring(cust_type,1,2)='N-' then 'N-KSK' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe' or substring(cust_type,1,2)='Ks' or substring(cust_type,1,2)='n-') group by substring(cust_type,1,2)) union (select customertypename from customertype where (substring(customertypename,1,2)!='Ro' and substring(customertypename,1,2)!='oe' and substring(customertypename,1,2)!='ks' and substring(customertypename,1,2)!='N-') group by customertypename)",ref rdr1);
			if(rdr1.HasRows)
			{
				Header="Distict / Type";
				while(rdr1.Read())
				{
					arrType.Add(rdr1.GetValue(0).ToString());
					Header+="\t"+rdr1.GetValue(0).ToString();
				}
				Header+="\tTotal Sales\tMonthly Avr.";
			}
			rdr1.Close();
			sw.WriteLine("From Date\t"+txtDateFrom.Text+"\tTo Date\t"+txtDateTo.Text);
			//*********************************
			sw.WriteLine(Header);
			arrTotal=new double[arrType.Count];
			double Total=0,GTotal=0,GAvrTotal=0;
			//dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
			dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
			while(rdr1.Read()){arrState.Add(rdr1.GetValue(0).ToString());}rdr1.Close();
			for(int i=0;i<arrState.Count;i++)
			{
				Total=0;
				sw.Write(arrState[i].ToString());
				for(int j=0;j<arrType.Count;j++)
				{
					//dbobj2.SelectQuery("select case when sum(net_amount) is null then '0' else sum(net_amount) end from sales_master sm,customer c where sm.cust_id=c.cust_id and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					//dbobj2.SelectQuery("select case when sum(qty) is null then '0' else sum(qty) end from sales_master sm,customer c,sales_details sd where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					if(radDetails.Checked)
					{
						if(arrType[j].ToString().ToLower()=="ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type ='"+arrType[j].ToString()+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					}
					else
					{
						if(arrType[j].ToString().ToLower()=="ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else if(arrType[j].ToString().ToLower()=="n-ksk")
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						else
							dbobj2.SelectQuery("select case when sum(Total_Qty*Qty) is null then '0' else sum(Total_Qty*Qty) end from sales_master sm,customer c,sales_details sd,Products p where p.Prod_ID=sd.Prod_ID and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and c.state='"+arrState[i].ToString()+"' and cust_type like'"+arrType[j].ToString()+"%' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(invoice_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
					}
					if(rdr1.Read())
					{
						arrTotal[j]+=double.Parse(rdr1.GetValue(0).ToString());
						sw.Write("\t"+rdr1.GetValue(0).ToString());
						Total+=double.Parse(rdr1.GetValue(0).ToString());
					}
				}
				sw.Write("\t"+Total.ToString());
				string GAvr=System.Convert.ToString(Total/double.Parse(Count.ToString()));
				sw.Write("\t"+GenUtil.strNumericFormat(GAvr.ToString()));
				GTotal+=Total;
				GAvrTotal+=double.Parse(GAvr);
				sw.WriteLine();
				
			}
			string str="Total";
			sw.Write(str);
			for(int n=0;n<arrTotal.Length;n++)
			{
				sw.Write("\t"+arrTotal[n].ToString());
			}
			sw.Write("\t"+GTotal.ToString());
			sw.Write("\t"+GenUtil.strNumericFormat(GAvrTotal.ToString()));
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file DistrictWiseChannelSales.xls for printing.
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
					CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSalesReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    DistrictWiseChannelSales Report Convert Into Excel Format, userid  "+uid);
				}
				else
				{
					MessageBox.Show(" Please Click the View Button First ");
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:DistrictWiseChannelSales.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    DistrictWiseChannelSales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Reurns date in MM/DD/YYYY format.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
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
		/// This method is used to count the difference from and to date
		/// </summary>
		public void GetMonth()
		{
			string[] arrDateFrom= txtDateFrom.Text.IndexOf("/")>0?txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length): txtDateFrom.Text.Split(new char[] { '-' }, txtDateFrom.Text.Length);
			string[] arrDateTo= txtDateTo.Text.IndexOf("/")>0?txtDateTo.Text.Split(new char[] {'/'},txtDateTo.Text.Length): txtDateTo.Text.Split(new char[] { '-' }, txtDateTo.Text.Length);
			int MonthFrom=int.Parse(arrDateFrom[1].ToString());
			int YearFrom=int.Parse(arrDateFrom[2].ToString());
			int MonthTo=int.Parse(arrDateTo[1].ToString());
			int YearTo=int.Parse(arrDateTo[2].ToString());
			Count=0;
			if(YearFrom==YearTo)
			{
				for(int i=MonthFrom;i<=MonthTo;i++)
				{
					Count++;
				}
			}
			else
			{
				for(int i=MonthFrom;i<=12;i++)
				{
					Count++;
				}
				for(int i=1;i<=MonthTo;i++)
				{
					Count++;
				}
			}
		}
	}
}