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
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using DBOperations;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using RMG;
using System.Web.SessionState;
using System.Runtime.InteropServices;
using System.Management;
using System.Net;
using System.Net.Sockets;
using MySecurity;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SadbhavnaSchemeYearWise_aspx.
	/// </summary>
	public partial class Reward_Report : System.Web.UI.Page
	{
		//protected System.Web.UI.WebControls.TextBox txtDateFrom;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateFrom;
		//protected System.Web.UI.WebControls.TextBox Textbox1;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateTo;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public string[] DateFrom = null;
		public string[] DateFrom1 = null;
		public string[] DateTo = null;
		public string[] DateTo1 = null;
		public double check;
		string uid="";
		public SqlDataReader dtr;
		public static int count1=0;
		public ArrayList Min=new ArrayList();
		public static int View = 0;
		public int ds11=0;
		public int ds12=0;
		public int ds21=0;
		public int ds22=0;
		public int ds10=0;
		public int ds20=0;
		public string s1="";
		public string s2="";
		public double[] TotalSum = null;
		// protected System.Web.UI.WebControls.DropDownList DropCtype;
		public double Xtra_Points12;
		public double[] TotalSum1 = null;
		public static string sql;
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
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
				
			}

			try
			{
				if(!Page.IsPostBack)
				{   
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(DateTime.Now.ToShortDateString());
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
			}
			
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="56";
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
				//dropyear.SelectedIndex=dropyear.Items.IndexOf(dropyear.Items.FindByValue(DateTime.Now.Year.ToString()));
				//**txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				//**Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				GetMultiValue();
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strState="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="",strCustName="";       //Add by vikas 16.11.2012

				strDistrict = "select distinct state from customer";
				strState = "select distinct country from customer";
				strPlace = "select distinct city from customer";

				//coment by vikas 20.12.2012 strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				
				//coment by vikas 20.12.2012 strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012
				
				strCustName=" Select Cust_Name from Customer order by Cust_Name ";

				strSSR = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";
				string[] arrStr = {strDistrict,strPlace,strSSR,strCustName};
				HtmlInputHidden[] arrCust = {tempDistrict,tempPlace,tempSSR,tempCustName};	

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString() !="")
								arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to return the date in MM/dd/yyyy format.
		/// </summary>
		
		# region DateTime Function...
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr= str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}
		# endregion 
		
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
		/// this is used to make sorting the datagrid on clicking of the datagridheader.
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
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  ");
			}
		}

		/// <summary>
		/// This method is used to view the report and set the column name with ascending order in 
		/// session variable.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public int flage=0;
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			flage=1;
			try
			{
				Bindddata_New();
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:btnShow_Click  SadbhavnaSchemeYearWise   Viewed "+"  userid  ");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:btnShow_Click  SadbhavnaSchemeYearWise   Viewed "+"  EXCEPTION  "+ ex.Message+"  userid  ");
			}
		}
		
		public void Bindddata_New()
		{
			try
			{

				s1="";
				s2="";
				s1=txtDateTo.Text;
				s2=txtDateFrom.Text;
				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length) : s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length) : s1.Split(new char[] { '-' }, s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				int Toyear=Convert.ToInt32(ds2[2]);
				int Fromyear=Convert.ToInt32(ds1[2]);
				Toyear--;
				Fromyear--;
				s1=ds1[1]+"/"+ds1[0]+"/"+Fromyear.ToString();
				int day=DateTime.DaysInMonth(Toyear,Convert.ToInt32(ds2[1]));
				s2=ds2[1]+"/"+day+"/"+Toyear.ToString();



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
				getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22); 
				InventoryClass obj=new InventoryClass();
				sql="";
				if(dropType.SelectedIndex==0)
				{
					if(DropSearchBy.SelectedIndex==0)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%RO%') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==1)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_name ='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==2)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%RO%') and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==3)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%RO%') and state='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==4)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%RO%') and ssr= (select Emp_id from Employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
				}
				else
				{
					if(DropSearchBy.SelectedIndex==0)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%Bazzar1%') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==1)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_name ='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==2)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%Bazzar1%') and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==3)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%Bazzar1%') and state='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==4)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%Bazzar1%') and ssr= (select Emp_id from Employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
				}
				//dtr=obj.GetRecordSet(sql);
				/*if(!dtr.HasRows)
				{
					flage=0;
					MessageBox.Show("Data Not Available");
					return;
				}*/
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		public static int count=0;
		public void getDate(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count=To2-From2;
				DateFrom = new string[count+1];
				DateTo = new string[count+1];
				TotalSum = new double[(count+1)];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
				TotalSum = new double[count];
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

		public void getDateLastYear(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count1=To2-From2;
				DateFrom1 = new string[count+1];
				DateTo1 = new string[count+1];
				TotalSum1 = new double[(count+1)];
			}
			else
			{
				count1=13-From2;
				count1+=To2;
				DateFrom1 = new string[count];
				DateTo1 = new string[count];
				TotalSum1 = new double[count];
			}
			From3--;
			To3--;
			int c=0;
			if(From2<=To2)
			{
				for(int i=From2,j=0;i<=To2;i++,j++)
				{
					if(c==0)
					{
						
						DateFrom1[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
						c=1;
					}
					else
						DateFrom1[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					
					int day=DateTime.DaysInMonth(From3,i);
					DateTo1[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
				
					//MessageBox.Show("From Date "+DateFrom1[j].ToString()+" : ToDate "+DateTo1[j].ToString());
				}
			}
			else
			{
				for(int i=From2,j=0;i<=12;i++,j++)
				{
					if(c==0)
						DateFrom1[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
					else
						DateFrom1[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					int day=DateTime.DaysInMonth(From3,i);
					DateTo1[j]=i.ToString()+"/"+day.ToString()+"/"+From3.ToString();
					c++;
					//MessageBox.Show("From Date "+DateFrom1[j].ToString()+" : ToDate "+DateTo1[j].ToString());
				}
				for(int i=1,j=c;i<=To2;i++,j++)
				{
					DateFrom1[j]=i.ToString()+"/"+"1"+"/"+To3.ToString();
					if(i==To2)
						DateTo1[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
					else
					{
						int day=DateTime.DaysInMonth(To3,i);
						DateTo1[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
					//MessageBox.Show("From Date "+DateFrom1[j].ToString()+" : ToDate "+DateTo1[j].ToString());
				}
			}
		}

		/// <summary>
		/// This method is used to prepare the report file.
		/// </summary>
		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				sql="";
				string info = "";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\Reward_Report1.txt";
				StreamWriter sw = new StreamWriter(path);
				
				/*if(DropSearchBy.SelectedIndex==0)
				{
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==1)
				{
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==2)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"')  and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==3)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==4)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==5)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
			
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
				string des="------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				//sw.WriteLine(GenUtil.GetCenterAddr("Sadbhavna Scheme YearWise From "+dropyear.SelectedItem.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine("+--------------------+---------+--------+---------+-----+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+-------+-----+----+---------+");
				sw.WriteLine("|    Customer Name   |  Place  |Customer| Unique  |St.Up|  APR  |  MAY  | JUNE  | JULY  | AUGT  | SEPT  | OCT   | NOV   | DEC   | JAN   | FEB   | MAR   | POINT  |  COMM | P.X.|P.X.|   Pt.   |");
				sw.WriteLine("|                    |         | Type   |  Code   |Point|       |       |       |       |       |       |       |       |       |       |       |       | COMM   |  SALE | ACH |REG |  TOTAL  |");
				sw.WriteLine("+--------------------+---------+--------+---------+-----+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+-------+-----+----+---------+");
				// 12345678901234567890 123456789 12345678 123456789 12345678 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 12345678 1234567 12345 1234 123456789
				if(rdr.HasRows)
				{
					info = " {0,-20:S} {1,-9:S} {2,-8:S} {3,-8:S} {4,-5:S} {5,-7:S} {6,-7:S} {7,-7:S} {8,-7:S} {9,-7:S} {10,-7:S} {11,-7:S} {12,-7:S} {13,-7:S} {14,-7:S} {15,-7:S} {16,-7:S} {17,-8:S} {18,-7:S} {19,-5:S} {20,-4:S} {21,-9:S}";
					while(rdr.Read())
					{
					
					}
				}
				sw.WriteLine("+--------------------+---------------+-------------+--------+--------+------+------+------+------+------+------+------+------+------+------+------+------+------+------+-------+-------+-------+");
				string info1= " {0,-50:S} {1,-7:S} {2,-7:S} {3,-7:S} {4,-7:S} {5,-7:S} {6,-7:S} {7,-7:S} {8,-7:S} {9,-7:S} {10,-7:S} {11,-7:S} {12,-7:S} {13,-8:S} {14,-7:f} {15,-7:S} {16,-7:S} {17,-9:S} ";// 19.08.09 Vikas sharma
				sw.WriteLine("+--------------------+---------------+-------------+--------+--------+------+------+------+------+------+------+------+------+------+------+------+------+------+------+-------+-------+-------+");
				dbobj.Dispose();
				sw.Close();*/

				s1="";
				s2="";
				s1=txtDateTo.Text;
				s2=txtDateFrom.Text;
				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length) : s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length) : s2.Split(new char[] { '-' }, s2.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				int Toyear=Convert.ToInt32(ds2[2]);
				int Fromyear=Convert.ToInt32(ds1[2]);
				Toyear--;
				Fromyear--;
				s1=ds1[1]+"/"+ds1[0]+"/"+Fromyear.ToString();
				int day=DateTime.DaysInMonth(Toyear,Convert.ToInt32(ds2[1]));
				s2=ds2[1]+"/"+day+"/"+Toyear.ToString();



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
				getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22); 

			
				if(dropType.SelectedIndex==0)
				{
					if(DropSearchBy.SelectedIndex==0)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type like '%RO%' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					/*else if (DropSearchBy.SelectedIndex==1)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==2)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}*/
					else if (DropSearchBy.SelectedIndex==1)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_name ='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==2)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where City='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==3)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where state='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==4)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where ssr= (select Emp_id from Employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
				}
				else
				{
					if(DropSearchBy.SelectedIndex==0)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_type like '%Bazzar%' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					/*else if (DropSearchBy.SelectedIndex==1)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==2)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}*/
					else if (DropSearchBy.SelectedIndex==1)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_name ='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==2)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where City='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==3)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where state='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
					else if (DropSearchBy.SelectedIndex==4)
					{
						sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where ssr= (select Emp_id from Employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
					}
				}
				InventoryClass obj_New=new InventoryClass();
				dtr=obj_New.GetRecordSet(sql);
				//dtr=obj1.GetRecordSet(sql);
				//sw.WriteLine("SNo.\tCustomer Name-Place\tSale LY\tSale CY\t2T+4T Sale in CY\tMS+XP Sale CY\tHSD+XM Sale CY\tNo. Of Month Uplifted\t% Growth in average Monthly Lube Sale **\tLFR\t2T+4T to MS Ratio\tFinal Rating On Growth\tLFR/2T MS Ratio Rating\tConsistency Rating\tTotal Rating");

				int i=1;

				if(dropType.SelectedIndex==0)
				{
					try
					{
						//sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
						//sw.WriteLine(GenUtil.GetCenterAddr("Sadbhavna Scheme YearWise From "+dropyear.SelectedItem.Text.ToString(),des.Length));
						//sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
						sw.WriteLine("+--------------------+---------+-----+-----+----------+-------+--------+--------+---------+-----+--------+------------+---------+------------+-------+-------+-------+--------+-------+-----+----+---------+");
						sw.WriteLine("|    Customer Name   |  Place  | Sale| Sale|2T+4T Sale|MS + XP|HSD + XM|Uplifted|Lube Sale| LFR |2T+4T MS|Final Rating|LFR/2T MS|Consistency | JAN   | FEB   | MAR   | POINT  |  COMM | P.X.|P.X.|   Pt.   |");
						sw.WriteLine("|                    |         |  LY |  CY |   In CY  |Sale CY| Sale CY|  Month |  Growth |     |  Ratio |  On Growth |  Ratio  |   Rating   |       |       |       | COMM   |  SALE | ACH |REG |  TOTAL  |");
						sw.WriteLine("+--------------------+---------+-----+-----+----------+-------+--------+--------+---------+-----+--------+------------+---------+------------+-------+-------+-------+--------+-------+-----+----+---------+");
						// 12345678901234567890 123456789 12345678 123456789 12345678 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 12345678 1234567 12345 1234 123456789
						if(dtr.HasRows)
						{
							info = " {0,-20:S} {1,-9:S} {2,-8:S} {3,-8:S} {4,-5:S} {5,-7:S} {6,-7:S} {7,-7:S} {8,-7:S} {9,-7:S} {10,-7:S} {11,-7:S} {12,-7:S} {13,-7:S} {14,-7:S} {15,-7:S} {16,-7:S} {17,-8:S} {18,-7:S} {19,-5:S} {20,-4:S} {21,-9:S}";
						}

						InventoryClass obj1=new InventoryClass();
						SqlDataReader dtr1;
						int Uplifted=0;
						double Diff_Sale=0;
						double LYSale=0,CYSale=0,Sale_2t_4t=0;
						while(dtr.Read())
						{
							Uplifted=0;
							sw.Write(i.ToString()+"\t");
							sw.Write(dtr["cust_name"].ToString()+","+dtr["city"].ToString()+"\t");
							for(int j=0;j<DateFrom.Length;j++)
							{	
								sql="select * from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+DateFrom[j].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[j].ToString()+"'";
								dtr1=obj1.GetRecordSet(sql);
								if(dtr1.HasRows)
								{
									Uplifted++;
								}
								dtr1.Close();
							}
							sql="select sum(quant*total_qty) LY_Sale from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
							dtr1=obj1.GetRecordSet(sql);
							if(dtr1.HasRows)
							{
								while(dtr1.Read())
								{
									if(dtr1["LY_Sale"].ToString()!=null && dtr1["LY_Sale"].ToString()!="")
									{
										sw.Write(Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2)+"\t");
										LYSale=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2);
									}
									else
									{
										sw.Write("\t");
									}
								}
							}
							else
							{
								sw.Write("\t");
							}
							dtr1.Close();
							if(dtr["total_sale"].ToString()!=null && dtr["total_sale"].ToString()!="")
							{
								sw.Write(Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2)+"\t");
								CYSale=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2);
							}
							else
							{
								sw.Write("\t");
							}
							sql="select sum(quant*total_qty) tf_Sale from vw_SaleBook where (category like '4t%' or category like '2t%' ) and cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
							dtr1=obj1.GetRecordSet(sql);
							if(dtr1.HasRows)
							{
								while(dtr1.Read())
								{
									if(dtr1["tf_Sale"].ToString()!=null && dtr1["tf_Sale"].ToString()!="")
									{
										sw.Write(Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2)+"\t");
										Sale_2t_4t=Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2);
									}
									else
									{
										Sale_2t_4t=0;
										sw.Write("\t");
									}
								}
							}
							else
							{
								Sale_2t_4t=0;
								sw.Write("\t");
							}
							dtr1.Close();
							sw.Write("\t");
							sw.Write("\t");
							sw.Write(Uplifted.ToString()+"\t");
							Diff_Sale=CYSale-LYSale;
							if(LYSale!=0)
							{
								Diff_Sale=Math.Round((Diff_Sale*100)/LYSale);
							}
							else
							{
								Diff_Sale=0;
							}
							sw.Write(Diff_Sale.ToString()+"%\t");
							Diff_Sale=0;
							Diff_Sale=Math.Round(CYSale*100);
							sw.Write(Diff_Sale.ToString()+"\t");
							Diff_Sale=0;
							Diff_Sale=Math.Round(Sale_2t_4t*100);
							sw.Write(Diff_Sale.ToString()+"\t");
							sw.Write("0\t");
							sw.Write("0\t");
							sw.Write("0\t");
							sw.Write("0\t");
							sw.WriteLine();
							i++;
						}
						dtr.Close();
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message.ToString());	
					}
				}
				else
				{
					sw.WriteLine("SNo.\tCustomer Name-Place\tSale LY\tSale CY\tNo. of SKU'S\tNo. of Month Uplifted More Then 200 Ltr.\tGrowth in Average Monthly Lube Sale**\tRating On Growth\t\tFinal Rating On Growth\tConsistency Rating\tRating On No of SKU'S\tTotal Rating");
					try
					{
						InventoryClass obj1=new InventoryClass();
						SqlDataReader dtr1;
						int Uplifted=0;
						double Diff_Sale=0;
						double LYSale=0,CYSale=0,Sale_2t_4t=0;
						while(dtr.Read())
						{
							Uplifted=0;
							sw.Write(i.ToString()+"\t");
							sw.Write(dtr["cust_name"].ToString()+","+dtr["city"].ToString()+"\t");
							for(int j=0;j<DateFrom.Length;j++)
							{	
								sql="select * from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and (quant*total_qty )>='200' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+DateFrom[j].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[j].ToString()+"'";
								dtr1=obj1.GetRecordSet(sql);
								if(dtr1.Read())
								{
									Uplifted++;
								}
								dtr1.Close();	
							}
							sql="select sum(quant*total_qty) LY_Sale from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
							dtr1=obj1.GetRecordSet(sql);
							if(dtr1.HasRows)
							{
								while(dtr1.Read())
								{
									if(dtr1["LY_Sale"].ToString()!=null && dtr1["LY_Sale"].ToString()!="")
									{
										sw.Write(Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2)+"\t");
										LYSale=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2);
									}
									else
									{
										sw.Write("\t");
									}
								}
							}
							else
							{
								sw.Write("\t");
							}
							dtr1.Close();
							if(dtr["total_sale"].ToString()!=null && dtr["total_sale"].ToString()!="")
							{
								sw.Write(Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2)+"\t");
								CYSale=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2);
							}
							else
							{
							}
							sql="select sum(quant*total_qty) tf_Sale from vw_SaleBook where (category like '4t%' or category like '2t%' ) and cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
							dtr1=obj1.GetRecordSet(sql);
							if(dtr1.HasRows)
							{
								while(dtr1.Read())
								{
									if(dtr1["tf_Sale"].ToString()!=null && dtr1["tf_Sale"].ToString()!="")
									{
										sw.Write(Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2)+"\t");
										Sale_2t_4t=Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2);
									}
									else
									{
										Sale_2t_4t=0;
										sw.Write("\t");
									}
								}
							}
							else
							{
								Sale_2t_4t=0;
								sw.Write("\t");
							}
							dtr1.Close();
							sw.Write("\t");
							sw.Write("\t");
							sw.Write(Uplifted.ToString()+"\t");
							Diff_Sale=CYSale-LYSale;
							if(LYSale!=0)
							{
								Diff_Sale=Math.Round((Diff_Sale*100)/LYSale);
							}
							else
							{
								Diff_Sale=0;
							}
							sw.Write(Diff_Sale.ToString()+"\t");
							Diff_Sale=0;
							Diff_Sale=Math.Round(CYSale*100);
							sw.Write(Diff_Sale.ToString()+"\t");
							Diff_Sale=0;
							Diff_Sale=Math.Round(Sale_2t_4t*100);
							sw.Write(Diff_Sale.ToString()+"\t");
							sw.Write("0\t");
							sw.Write("0\t");
							sw.Write("0\t");
							sw.Write("0\t");
							i++;
							sw.WriteLine();
						}
						dtr.Close();
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message.ToString());	
					}
				}
			}
			catch(Exception ex)
			{
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		

		public void ConvertToExcel_New()
		{
			InventoryClass obj=new InventoryClass();
			sql="";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\Reward_Report.xls";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;

			
			s1="";
			s2="";
			s1=txtDateTo.Text;
			s2=txtDateFrom.Text;
			string[] ds1 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
			string[] ds2 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length) : s1.Split(new char[] { '-' }, s1.Length);
			ds10=System.Convert.ToInt32(ds1[0]);
			ds20=System.Convert.ToInt32(ds2[0]);
			ds11=System.Convert.ToInt32(ds1[1]);
			ds12=System.Convert.ToInt32(ds1[2]);
			ds21=System.Convert.ToInt32(ds2[1]);
			ds22=System.Convert.ToInt32(ds2[2]);
			int Toyear=Convert.ToInt32(ds2[2]);
			int Fromyear=Convert.ToInt32(ds1[2]);
			Toyear--;
			Fromyear--;
			s1=ds1[1]+"/"+ds1[0]+"/"+Fromyear.ToString();
			int day=DateTime.DaysInMonth(Toyear,Convert.ToInt32(ds2[1]));
			s2=ds2[1]+"/"+day+"/"+Toyear.ToString();

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
			getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22); 

			
			if(dropType.SelectedIndex==0)
			{
				if(DropSearchBy.SelectedIndex==0)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_type in (select customertypename from customertype where group_name like '%RO%') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==1)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where cust_name ='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==2)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where City='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==3)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where state='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==4)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale from vw_SaleBook where ssr= (select Emp_id from Employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
			}
			else
			{
				if(DropSearchBy.SelectedIndex==0)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku  from vw_SaleBook where cust_type like '%Bazzar%' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==1)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku from vw_SaleBook where cust_name ='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==2)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku  from vw_SaleBook where City='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==3)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku  from vw_SaleBook where state='"+DropValue.Value.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
				else if (DropSearchBy.SelectedIndex==4)
				{
					sql="select cust_id,cust_name,city,sum(quant*total_qty) total_sale, count(prod_name) sku  from vw_SaleBook where ssr= (select Emp_id from Employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by cust_id,cust_name,city order by cust_id,cust_name,city";
				}
			}

			dtr=obj.GetRecordSet(sql);
			string sql1="";
			int i=1;
			if(dropType.SelectedIndex==0)
			{
				try
				{
					sw.WriteLine("SNo.\tCustomer Name-Place\tSale LY\tSale CY\t2T+4T Sale in CY\tMS+XP Sale CY\tHSD+XM Sale CY\tNo. Of Month Uplifted\t% Growth in average Monthly Lube Sale **\tLFR\t2T+4T to MS Ratio\tFinal Rating On Growth\tLFR/2T MS RAtio Rating\tConsistency Rating\tTotal Rating");
					InventoryClass obj1=new InventoryClass();
					SqlDataReader dtr1;
					int Uplifted=0;
					double Diff_Sale=0;
					double LYSale=0,CYSale=0,Sale_2t_4t=0;
					double MS=0,HSD=0;
					while(dtr.Read())
					{
						Uplifted=0;
						sw.Write(i.ToString()+"\t");
						sw.Write(dtr["cust_name"].ToString()+","+dtr["city"].ToString()+"\t");
						for(int j=0;j<DateFrom.Length;j++)
						{	
							sql1="select * from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+DateFrom[j].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[j].ToString()+"'";
							dtr1=obj1.GetRecordSet(sql);
							if(dtr1.HasRows)
							{
								Uplifted++;
							}
							dtr1.Close();
						}
						sql1="select sum(quant*total_qty) LY_Sale from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
						dtr1=obj1.GetRecordSet(sql1);
						if(dtr1.HasRows)
						{
							while(dtr1.Read())
							{
								if(dtr1["LY_Sale"].ToString()!=null && dtr1["LY_Sale"].ToString()!="")
								{
									sw.Write(Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2)+"\t");
									LYSale=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2);
								}
								else
								{
									sw.Write("\t");
								}
							}
						}
						else
						{
							sw.Write("\t");
						}
						dtr1.Close();
						if(dtr["total_sale"].ToString()!=null && dtr["total_sale"].ToString()!="")
						{
							sw.Write(Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2)+"\t");
							CYSale=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2);
						}
						else
						{
							sw.Write("\t");
						}
						sql1="select sum(quant*total_qty) tf_Sale from vw_SaleBook where (category like '4t%' or category like '2t%' ) and cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
						dtr1=obj1.GetRecordSet(sql1);
						if(dtr1.HasRows)
						{
							while(dtr1.Read())
							{
								if(dtr1["tf_Sale"].ToString()!=null && dtr1["tf_Sale"].ToString()!="")
								{
									sw.Write(Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2)+"\t");
									Sale_2t_4t=Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2);
								}
								else
								{
									Sale_2t_4t=0;
									sw.Write("\t");
								}
							}
						}
						else
						{
							Sale_2t_4t=0;
							sw.Write("\t");
						}
						dtr1.Close();

						sql1="select * from Cust_Sale_MS_HSD where cust_id='"+dtr["cust_id"].ToString()+"'";
						dtr1=obj1.GetRecordSet(sql1);
						if(dtr1.HasRows)
						{
							if(dtr1.Read())
							{
								MS=Math.Round(double.Parse(dtr1["MS"].ToString()),2);
								HSD=Math.Round(double.Parse(dtr1["HSD"].ToString()),2);
								sw.Write(MS.ToString()+"\t");
								sw.Write(HSD.ToString()+"\t");
							}
						}
						else
						{
							sw.Write("0\t");
							sw.Write("0\t");
						}
						dtr1.Close();

						sw.Write(Uplifted.ToString()+"\t");
						
						Diff_Sale=CYSale-LYSale;
						double G_Lube_Sale=0;
						if(LYSale!=0)
						{
							G_Lube_Sale=Math.Round(((Diff_Sale*100)/LYSale),2);
						}
						else
						{
							G_Lube_Sale=0;
						}
						sw.Write(G_Lube_Sale.ToString()+"\t");
						double LFR=0;
						LFR=Math.Round((CYSale*100)/(MS+HSD),2);
																	
						sw.Write(LFR.ToString()+"\t");
						double MS_Ratio=0;
						MS_Ratio=Math.Round(((Sale_2t_4t*100)/MS),2);
						
						sw.Write(MS_Ratio.ToString()+"\t");
						
						double Rat_on_Growth=0;
						//IF(I2<=10, I2*4/10, (40+(I2-10)*1))
						if(G_Lube_Sale<=10)
						{
							Rat_on_Growth=G_Lube_Sale*4/10;
						}
						else
						{
							Rat_on_Growth=Math.Round(Double.Parse((40+(G_Lube_Sale-10)*1).ToString()),2);
						}
																	
						double Final_Rat_on_Growth=0;
						//IF(L2<=45,L2,45)
						if(Rat_on_Growth<=45)
						{
							Final_Rat_on_Growth=Rat_on_Growth;
						}
						else
						{
							Final_Rat_on_Growth=45;
						}
						sw.Write(Final_Rat_on_Growth.ToString()+"\t");
						double LFR_MS_Ratio=0;
						//IF(J2>K2,J2,K2)
						if(LFR>MS_Ratio)
						{
							LFR_MS_Ratio=LFR;
						}
						else
						{
							LFR_MS_Ratio=MS_Ratio;
						}
																	
						double LFR_MS_Rating=0;
						//IF(N2<=0.25,0,IF(N2<=0.75,(N2-0.25)*80,IF(N2<=1,40+(N2-0.75)*20,45)))
						if(LFR_MS_Ratio<=0.25)
						{
							LFR_MS_Rating=0;
						}
						else if(LFR_MS_Ratio<=0.75)
						{
							LFR_MS_Rating=Math.Round(double.Parse(((LFR_MS_Ratio-0.25)*80).ToString()),2);
						}
						else if(LFR_MS_Ratio<=1)
						{
							LFR_MS_Rating=40+(LFR_MS_Ratio-0.75)*20;
						}
						else
						{
							LFR_MS_Rating=45;
						}
						sw.Write(LFR_MS_Rating.ToString()+"\t");
																	
						double Consistency_Rating=0;
						//20*H2/12
						Consistency_Rating=(20*Uplifted)/12;
						sw.Write(Consistency_Rating.ToString()+"\t");
						//M2+O2+P2
						double Tot_Rating=0;
						Tot_Rating=Final_Rat_on_Growth+LFR_MS_Rating+Consistency_Rating;
						sw.Write(Tot_Rating.ToString()+"\t");
						sw.WriteLine();
						/*Diff_Sale=CYSale-LYSale;
						if(LYSale!=0)
						{
							Diff_Sale=Math.Round((Diff_Sale*100)/LYSale);
						}
						else
						{
							Diff_Sale=0;
						}
						sw.Write(Diff_Sale.ToString()+"%\t");
						Diff_Sale=0;
						Diff_Sale=Math.Round(CYSale*100);
						sw.Write(Diff_Sale.ToString()+"\t");
						Diff_Sale=0;
						Diff_Sale=Math.Round(Sale_2t_4t*100);
						sw.Write(Diff_Sale.ToString()+"\t");
						sw.Write("0\t");
						sw.Write("0\t");
						sw.Write("0\t");
						sw.Write("0\t");
						sw.WriteLine();*/
						i++;
						
					}
					//dtr.Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message.ToString());	
				}
			}
			else
			{
				sw.WriteLine("SNo.\tCustomer Name-Place\tSale LY\tSale CY\tNo. of SKU'S\tNo. of Month Uplifted More Then 200 Ltr.\tGrowth in Average Monthly Lube Sale**\tRating On Growth\tFinal Rating On Growth\tConsistency Rating\tRating On No of SKU'S\tTotal Rating");
				try
				{
					InventoryClass obj1=new InventoryClass();
					SqlDataReader dtr1;
					int Uplifted=0;
					double Diff_Sale=0;
					double LYSale=0,CYSale=0,Sale_2t_4t=0,SKU=0;
					while(dtr.Read())
					{
						Uplifted=0;
						sw.Write(i.ToString()+"\t");
						sw.Write(dtr["cust_name"].ToString()+","+dtr["city"].ToString()+"\t");
						for(int j=0;j<DateFrom.Length;j++)
						{	
							sql1="select * from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and (quant*total_qty )>='200' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+DateFrom[j].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[j].ToString()+"'";
							dtr1=obj1.GetRecordSet(sql1);
							if(dtr1.Read())
							{
								Uplifted++;
							}
							dtr1.Close();	
						}
						sql1="select sum(quant*total_qty) LY_Sale from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
						dtr1=obj1.GetRecordSet(sql1);
						if(dtr1.HasRows)
						{
							while(dtr1.Read())
							{
								if(dtr1["LY_Sale"].ToString()!=null && dtr1["LY_Sale"].ToString()!="")
								{
									sw.Write(Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2)+"\t");
									LYSale=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2);
								}
								else
								{
									sw.Write("\t");
								}
							}
						}
						else
						{
							sw.Write("\t");
						}
						dtr1.Close();
						if(dtr["total_sale"].ToString()!=null && dtr["total_sale"].ToString()!="")
						{
							sw.Write(Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2)+"\t");
							CYSale=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2);
						}
						else
						{
						}
						SKU=double.Parse(dtr["sku"].ToString());
						sw.Write(SKU+"\t");
						sw.Write(Uplifted.ToString()+"\t");
						Diff_Sale=CYSale-LYSale;
						if(LYSale!=0)
						{
							Diff_Sale=Math.Round((Diff_Sale*100)/LYSale);
						}
						else
						{
							Diff_Sale=0;
						}
						sw.Write(Diff_Sale.ToString()+" % \t");
						double Rat_Growth=0;
						if(Diff_Sale<=10)
						{
							Rat_Growth=Math.Round((Diff_Sale*4/10),2);
						}
						else
						{
							Rat_Growth=Math.Round((40+(Diff_Sale-10)*1),2);
						}
						sw.Write(Rat_Growth.ToString()+"\t");
															
						double F_Rat_Growth=0;
															
						if(Rat_Growth<=45)
						{
							F_Rat_Growth=Math.Round(Rat_Growth,2);
						}
						else
						{
							F_Rat_Growth=45;
						}
						sw.Write(F_Rat_Growth.ToString()+"\t");
															
						double Con_Rating=0;
															
						Con_Rating=Math.Round(double.Parse((Uplifted*20/12).ToString()),2);
						sw.Write(Con_Rating.ToString()+"\t");								
															
						double Rat_SKU=0;
						if(SKU<=20)
						{
							Rat_SKU=0;
						}
						else if(SKU<=50)
						{
							Rat_SKU=Math.Round(((SKU-20)*1.33),2);
						}
						else if(SKU<=60)
						{
							Rat_SKU=Math.Round((40+(SKU-50)*0.5),2);
						}
						else
						{
							Rat_SKU=45;
						}
						sw.Write(Rat_SKU.ToString()+"\t");	
						double Tot_Rating=0;
															
						Tot_Rating=F_Rat_Growth+Rat_SKU+Con_Rating;
						sw.Write(Tot_Rating.ToString()+"\t");
															
						
						/*Diff_Sale=CYSale-LYSale;
						if(LYSale!=0)
						{
							Diff_Sale=Math.Round((Diff_Sale*100)/LYSale);
						}
						else
						{
							Diff_Sale=0;
						}
						sw.Write(Diff_Sale.ToString()+"\t");
						Diff_Sale=0;
						Diff_Sale=Math.Round(CYSale*100);
						sw.Write(Diff_Sale.ToString()+"\t");
						Diff_Sale=0;
						Diff_Sale=Math.Round(Sale_2t_4t*100);
						sw.Write(Diff_Sale.ToString()+"\t");
						sw.Write("0\t");
						sw.Write("0\t");
						sw.Write("0\t");
						sw.Write("0\t");*/
						i++;
						sw.WriteLine();
					}
					dtr.Close();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message.ToString());	
				}
															
			}
			sw.Close();
		}
		/// <summary>
		/// Prepares the report file SadBhavnaschemeYearWise.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			flage=1;
			//if(dropType.SelectedIndex==0)
			//{
				makingReport();
			//}

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SadbhavnaSchemeYearWise.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click  SadbhavnaSchemeYearWise   userid  ");
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeYearWise Printed    EXCEPTION  "+ ane.Message+" userid  ");
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeYearWise Printed  EXCEPTION  "+ se.Message+"  userid  ");
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed   EXCEPTION "+es.Message+"  userid  ");
				}
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed  EXCEPTION   "+ es.Message+"  userid  ");
			}
		}

		/// <summary>
		/// Prepares the excel report file SadBhavnaSchemeYearWise.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ConvertToExcel_New();
				flage=1;
				MessageBox.Show("Successfully Convert File into Excel Format");
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SadbhavnaSchemeYearWise Report Convert Into Excel Format ,  userid  "+uid);
					
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SadbhavnaSchemeYearWise Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}
	}
}