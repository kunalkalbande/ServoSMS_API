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
	/// Summary description for Salesreport1.
	/// </summary>
	public partial class Salesreport1 : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		System.Globalization.NumberFormatInfo  nfi = new System.Globalization.CultureInfo("en-US",false).NumberFormat;
		public int ds11=0;
		public int ds12=0;
		public int ds21=0;
		public int ds22=0;
		public int ds10=0;
		public int ds20=0;
		public string[] DateFrom = null;
		public double[] TotalSum = null;
		public string[] DateTo = null;
		public static int count=0;
		public static string sql="";
		public static int View = 0;
		string UID;


		public static double[] GrantTotal = null;
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				UID=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				try
				{
					ArrayList TotalSum = new ArrayList();
					View=0;
					count=0;
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="10";
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
				
					GetMultiValue();
					CreateLogFiles.ErrorLog("Form:Salesreport.aspx,Class:PetrolPumpClass.cs,Method: page_load, userid  "+UID);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Salesreport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
				}
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
		/// this is used to split the date.
		/// </summary>
		public static string month(string s1)
		{
			string[] ds1 = s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length) : s1.Split(new char[] { '-' }, s1.Length);
			ds1[0]="31";
			return ds1[1] + "/" + ds1[0] + "/" + ds1[2];	
		}
		
		/// <summary>
		/// this is used to view the report.
		/// </summary>
		protected void btnview1_Click(object sender, System.EventArgs e)
		{
			try
			{
				string cust_name="";
				string s1="";
				string s2="";
				s1=Request.Form["txtDateTo"].ToString();
				s2= Request.Form["txtDateFrom"].ToString();
				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length) : s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length) : s2.Split(new char[] { '-' }, s2.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					View=0;
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					View=0;
					return;
				}
				if((ds22-ds12) > 1)
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}

				getDate(ds10,ds11,ds12,ds20,ds21,ds22);//add by mahesh
				if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					//dbobj.SelectQuery("select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer order by state,sadbhavnacd,cust_name,cust_type,city",ref rdr);
					sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
				else if(DropSearchBy.SelectedIndex==1)/*******Add by vikas 17.11.2012*****************/
				{
					if(DropValue.Value=="All")
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					else
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer where cust_type like '"+DropValue.Value+"%' order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					else
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
				}/*****End***********/
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					else
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer where state='"+DropValue.Value+"' order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where state='"+DropValue.Value+"'and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					else
					{
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer where cust_name='"+DropValue.Value+"' order by state,sadbhavnacd,cust_name,cust_type,city";
						
						//coment by vikas 25.05.09 sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where cust_name='"+DropValue.Value+"' and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
						cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where cust_name='"+cust_name.ToString()+"' and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					}
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value=="All")
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					else
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer where city='"+DropValue.Value+"' order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where city='"+DropValue.Value+"' and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value=="All")
						//sql="select Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer order by state,sadbhavnacd,cust_name,cust_type,city";
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
					else
						
						sql="select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city";
				}
				View=1;
				CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:btnView, userid  "+UID);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+UID );
			}
		}
		
		public void Totalforprint(string t24t,string tot)//,string oe1,string fleet1,string ibp1,string total1)
		{
			
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\CustomerSalesReport.xls";
			StreamWriter sw = new StreamWriter(path);
			if(View==1)
			{
				SqlDataReader rdr = null,rdr1=null;
				dbobj.SelectQuery(sql,ref rdr);
				int flag=0;
				string cust_name="";
				string s1="";
				string s2="";
				s1=txtDateTo.Text;
				s2=txtDateFrom.Text;
				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length) : s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 =s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length) : s1.Split(new char[] { '-' }, s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					View=0;
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					View=0;
					return;
				}
				if((ds22-ds12) > 1)
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}
				getDate(ds10,ds11,ds12,ds20,ds21,ds22);
				if(rdr.HasRows)
				{
					flag=1;
					sw.Write("\t\t\t\t\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(GetMonthName(DateFrom[m].ToString())+"\t\t");
					}
					sw.WriteLine();
					sw.Write("District\t");
					sw.Write("SSC\t");
					sw.Write("Customer Name\t");
					sw.Write("Type\t");
					sw.Write("Place");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\tTotal");
						sw.Write("\t2T/4T");
					}
					sw.WriteLine();
				}
				if(flag==1)
				{
					while(rdr.Read())
					{
						sw.Write(rdr["state"].ToString()+"\t");
						sw.Write(rdr["sadbhavnacd"].ToString()+"\t");
						sw.Write(rdr["Cust_Name"].ToString()+":"+rdr["Cust_ID"].ToString()+"\t");
						sw.Write(rdr["Cust_Type"].ToString()+"\t");
						sw.Write(rdr["City"].ToString());
						int k=-1;
						for(int j=0;j<DateFrom.Length;j++)
						{
							string Cust_ID=rdr["Cust_ID"].ToString();
							dbobj.SelectQuery("select sum(totalqty) total,sum(oil2t+oil4t) t2t4 from vw_salesoil v,sales_master sm where sm.invoice_no=v.invoice_no and sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[j].ToString()+"' group by sm.cust_id",ref rdr1);
							if(rdr1.Read())
							{
								sw.Write("\t"+rdr1["Total"].ToString());
								sw.Write("\t"+rdr1["t2t4"].ToString());
								TotalSum[++k]+=double.Parse(rdr1["Total"].ToString());
								TotalSum[++k]+=double.Parse(rdr1["t2t4"].ToString());
							}
							else
							{
								sw.Write("\t0");
								sw.Write("\t0");
								k+=2;
							}
							rdr1.Close();
						}
						sw.WriteLine();
					}
					sw.Write("Total\t\t\t\t");
					for(int j=0;j<TotalSum.Length;j++)	
					{
						sw.Write("\t"+TotalSum[j].ToString());
					}
					sw.WriteLine();
				}
			}
			sw.Close();
		}

		/// <summary>
		/// Making the report for print file.txt .
		/// </summary>
		public void makingReport()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\CustomerSalesReport.txt";
			StreamWriter sw = new StreamWriter(path);		
			try
			{
				CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:btnprint, userid  "+UID );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:btnprint,   EXCEPTION "+ex.Message+"  userid  "+UID );
			}
		}
		
		/// <summary>
		/// this is used to print report on clicking .
		/// </summary>
		private void BtnPrint1_Click(object sender, System.EventArgs e)
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

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\CustomerSalesReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:print");
                
				} 
				catch (ArgumentNullException ane) 
				{
					//Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:print"+ ane.Message+". User: "+UID);
				} 
				catch (SocketException se) 
				{
					///Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:print"+ se.Message+". User: "+UID);
				} 
				catch (Exception es) 
				{
					//Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:print"+ es.Message+". User: "+UID);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Method:print"+ ex.Message+". User: "+UID);
			}
		}
		
		/// <summary>
		/// Prepares the excel report file CustomerSalesReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SalesReport1.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Sales Report Convert Into Excel Format, userid  "+UID);
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
				View=0;
				CreateLogFiles.ErrorLog("Form:SalesReport1.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}
		
		/// <summary>
		/// This method is used to return the month name with year in given date.
		/// </summary>
		public string GetMonthName(string mon)
		{
			if(mon.IndexOf("/")>0 || mon.IndexOf("-")>0)
			{
				string[] month=mon.IndexOf("/")>0?mon.Split(new char[] {'/'},mon.Length): mon.Split(new char[] { '-' }, mon.Length);
				if(month[0].ToString()=="1")
					return "January "+month[2].ToString();
				else if(month[0].ToString()=="2")
					return "Fabruary "+month[2].ToString();
				else if(month[0].ToString()=="3")
					return "March "+month[2].ToString();
				else if(month[0].ToString()=="4")
					return "April "+month[2].ToString();
				else if(month[0].ToString()=="5")
					return "May "+month[2].ToString();
				else if(month[0].ToString()=="6")
					return "June "+month[2].ToString();
				else if(month[0].ToString()=="7")
					return "July "+month[2].ToString();
				else if(month[0].ToString()=="8")
					return "August "+month[2].ToString();
				else if(month[0].ToString()=="9")
					return "September "+month[2].ToString();
				else if(month[0].ToString()=="10")
					return "October "+month[2].ToString();
				else if(month[0].ToString()=="11")
					return "November "+month[2].ToString();
				else if(month[0].ToString()=="12")
					return "December "+month[2].ToString();
			}
			return "";
		}
		
		/// <summary>
		/// This method is used to get the to and from date.
		/// </summary>
		public void getDate(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count=To2-From2;
				DateFrom = new string[count+1];
				DateTo = new string[count+1];
				TotalSum = new double[(count+1)*2];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
				TotalSum = new double[count*2];
			}
			int c=0;
			if(From2<=To2)
			{
				for(int i=From2,j=0;i<=To2;i++,j++)
				{
					if(c==0)
					{
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
						c=1;
					}
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					if(i==To2)
					{
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
						c=2;
					}
					else
					{
						int day=DateTime.DaysInMonth(From3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
			else
			{
				for(int i=From2,j=0;i<=12;i++,j++)
				{
					if(c==0)
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					int day=DateTime.DaysInMonth(From3,i);
					DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+From3.ToString();
					c++;
				}
				for(int i=1,j=c;i<=To2;i++,j++)
				{
					DateFrom[j]=i.ToString()+"/"+"1"+"/"+To3.ToString();
					if(i==To2)
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
					else
					{
						int day=DateTime.DaysInMonth(To3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
		}

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value
		/// from dropdownlist with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strName="",strType="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012
				
				//coment by vikas 25.05.09 strName = "select distinct c.cust_name from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name";
				strName = "select distinct c.cust_name,c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name,c.city";
				
				//strType = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				
				//coment by vikas 17.11.2012 strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				strDistrict = "select distinct state from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by state";
				strPlace = "select distinct c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.city";
				strSSR = "select emp_name from employee where emp_id in(select ssr from vw_cust_ageing a,customer c where c.cust_id=a.cust_id) and status=1 order by emp_name";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012
				
				//comentby vikas 17.11.2012 string[] arrStr = {strName,strType,strDistrict,strPlace,strSSR};
				//comentby vikas 17.11.2012 HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempDistrict,tempPlace,tempSSR};

				string[] arrStr = {strName,strDistrict,strPlace,strSSR,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempCustName,tempDistrict,tempPlace,tempSSR,tempGroup,tempSubGroup};

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//Coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
							{
								if(i==0)
								{
									arrCust[i].Value+=rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString()+",";
								}
								else
								{
									arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
								}
							}
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salesreport1.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}
	}
}