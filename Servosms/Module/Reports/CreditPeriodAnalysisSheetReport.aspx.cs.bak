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
using System.IO;
using DBOperations;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for CreditPeriodAnalysisSheetReport.
	/// </summary>
	public partial class CreditPeriodAnalysisSheetReport : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv1;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public static int Count=0;

		string uid;
		//public SqlDataReader rdr=null;
	
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
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:CreditPeriodAnalysisSheetReport.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				Count=0;
				txtDateFrom.Text = DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text = DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				//*******************
				SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//SqlCommand cmd;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader rdr=null;//,rdr1=null;
				rdr = obj.GetRecordSet("select Acc_Date_From from organisation");
				if(rdr.Read())
				{
					string s =GenUtil.trimDate(rdr.GetValue(0).ToString());
					string[] ss=s.Split(new char[] {'/'},s.Length);
					tempPeriod.Value=ss[0]+":"+ss[2];
				}
				rdr.Close();
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="7";
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
				//*******************
				object ob=null;
				// call this procedure for fill the record in ledgerdetails table before deducting all debit amount
				// and show only remaining amount means balance and after delete all zero balance amount.
				//Coment By vikas dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetails",ref ob,"@Cust_ID","","@fromdate",getdate(txtDateTo.Text,true),"@todate",getdate(txtDateFrom.Text,true));
				
				//dbobj.ExecProc(OprType.Insert,"Sp_CustOutstanding ",ref op,"@id",Int32.Parse(rdr["cust_id"].ToString()),"@fromdate",getdate(txtDateTo.Text,true),"@todate",getdate(txtDateFrom.Text,true));
				//dbobj.ExecProc(DBOperations.OprType.Update,"UpdateLedgerDetailsTesting",ref ob,"@Ledger_ID","0");
				//rdr = obj.GetRecordSet("select CustID,sum(creditamount) CreditAmount from customerledgertable where (particular like 'Payment Received%' or particular ='Opening Balance') group by custid order by custid");
				/*rdr = obj.GetRecordSet("select CustID,sum(creditamount) CreditAmount from customerledgertable where CreditAmount>0 group by custid order by custid");
				//int c=0;
				//if(rdr.HasRows)
				//{
				int i=0;
				while(rdr.Read())
				{
					i++;
					double Amount=double.Parse(rdr["CreditAmount"].ToString());
					rdr1 = obj1.GetRecordSet("select * from LedgerDetails where Cust_ID='"+rdr["Custid"].ToString()+"' and Amount>0 order by Bill_Date");
					while(rdr1.Read())
					{
						Amount=Amount-double.Parse(rdr1["Amount"].ToString());
						if(Amount>=0)
						{
							//dbobj.Insert_or_Update("update LedgerDetails set Amount=0 where Cust_ID='"+rdr["CustID"].ToString()+"' and convert(varchar,Bill_Date,2)=convert(varchar,cast('"+rdr1["Bill_Date"].ToString()+"' as datetime),2)",ref c);
							//dbobj.Insert_or_Update("update LedgerDetails set Amount=0 where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",ref c);
							Con.Open();
							cmd = new SqlCommand("update LedgerDetails set Amount=0 where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",Con);
							cmd.ExecuteNonQuery();
							cmd.Dispose();
							Con.Close();
						}
						else
						{
							//dbobj.Insert_or_Update("update LedgerDetails set Amount=abs("+Amount+") where Cust_ID='"+rdr["CustID"].ToString()+"' and convert(varchar,Bill_Date,2)=convert(varchar,cast('"+rdr1["Bill_Date"].ToString()+"' as datetime),2)",ref c);
							//dbobj.Insert_or_Update("update LedgerDetails set Amount=abs("+Amount+") where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",ref c);
							Con.Open();
							cmd = new SqlCommand("update LedgerDetails set Amount=abs("+Amount+") where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",Con);
							cmd.ExecuteNonQuery();
							cmd.Dispose();
							Con.Close();
							break;
						}
					}
					rdr1.Close();
				}
				Con.Open();
				cmd = new SqlCommand("delete from LedgerDetails where amount=0",Con);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				Con.Close();
				
				//}
				//*******************/
				GetMultiValue();
			}
		}

		private DateTime getdate(string dat,bool to)
		{
			string[] dt=dat.Split(new char[]{'/'},dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
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

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			/*if(DropSearchBy.SelectedIndex!=0)
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				DropValue.Items.Clear();
				DropValue.Items.Add("All");
				if(DropSearchBy.SelectedItem.Text=="SSR")
					rdr = obj.GetRecordSet("Select Emp_Name from Employee where Designation = 'Servo Sales Representative' order by Emp_Name");
				else if(DropSearchBy.SelectedItem.Text=="City")
					rdr = obj.GetRecordSet("Select distinct City from Beat_Master order by City");
				else if(DropSearchBy.SelectedItem.Text=="District")
					rdr = obj.GetRecordSet("Select distinct state from Beat_Master order by state");
				else if(DropSearchBy.SelectedItem.Text=="State")
					rdr = obj.GetRecordSet("Select distinct country from Beat_Master order by country");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						DropValue.Items.Add(rdr.GetValue(0).ToString());
					}
				}
				else
				{
					MessageBox.Show("Data Not Available");
				}
			}
			else
			{
				DropValue.Items.Clear();
				DropValue.Items.Add("All");
			}*/
		}

		/// <summary>
		/// This method is used to view the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			string cust_name="";
			string s1="";
			string s2="";
			s1=txtDateTo.Text;
			s2=txtDateFrom.Text;
			string[] ds1 = s2.Split(new char[] {'/'},s2.Length);
			string[] ds2 = s1.Split(new char[] {'/'},s1.Length);
		    int ds10=System.Convert.ToInt32(ds1[0]);
			int	ds20=System.Convert.ToInt32(ds2[0]);
			int	ds11=System.Convert.ToInt32(ds1[1]);
			int	ds12=System.Convert.ToInt32(ds1[2]);
			int	ds21=System.Convert.ToInt32(ds2[1]);
			int	ds22=System.Convert.ToInt32(ds2[2]);
			if(ds12==ds22 && ds11 > ds21)
			{
				MessageBox.Show("Please Select Greater Month in DateTo");
				Count=0;
				return;
			}
			if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
			{
				MessageBox.Show("Please Select Greater Date");
				Count=0;
				return;
			}
			if((ds22-ds12) > 1)
			{
				MessageBox.Show("Please Select date between one year");
				Count=0;
				return;
			}
			if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
			{
				MessageBox.Show("Please Select date between one year");
				Count=0;
				return;
			}
			Count=1;

			object ob=null;
			dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetailsCAS",ref ob,"@Cust_ID","","@todate",getdate(txtDateTo.Text,true));
			
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
 		public void ConvertToExcel()
		{
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass();
			ArrayList arrMonth = new ArrayList();
			double[] arrAmount=null;
			double[] arrPriviousAmt=new double[1];
			double GTotal=0;
			SqlDataReader rdr=null,rdr1=null;
			int Count_Flage=0;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\CreditPeriodAnalysisSheetReport.xls";
			StreamWriter sw = new StreamWriter(path);

			if(DropSearchBy.SelectedIndex==0)
			{
				//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and bill_no<>'O/B' and amount<>0 group by l.cust_id,c.cust_name,c.city order by cust_name");
				//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,ssr from customer c,ledgerdetails l where c.cust_id=l.cust_id  and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,ssr order by cust_name");
				//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,ssr from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,ssr order by cust_name");
				rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,ssr,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,ssr,contactperson,mobile order by cust_name");
				rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
			}
			else
			{
				if(DropValue.Value=="All")
				{
					//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and bill_no<>'O/B' and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
					//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
					//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
					rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
					rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
				}
				else
				{
					if(DropSearchBy.SelectedItem.Text=="SSR")
					{
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and bill_no<>'O/B' and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by city,cust_name");
						rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by city,cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
						
						Count_Flage=1;
					}
					else if(DropSearchBy.SelectedItem.Text=="City")
					{
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and bill_no<>'O/B' and c.city='"+DropValue.Value+"' and amount<>0 group by l.cust_id,c.cust_name,c.city order by cust_name");
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.city='"+DropValue.Value+"' and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.city='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.city='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.city='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
					}
					else if(DropSearchBy.SelectedItem.Text=="District")
					{
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and bill_no<>'O/B' and c.state='"+DropValue.Value+"' and amount<>0 group by l.cust_id,c.cust_name,c.city order by cust_name");
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.state='"+DropValue.Value+"' and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
					}
					else if(DropSearchBy.SelectedItem.Text=="State")
					{
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and bill_no<>'O/B' and c.country='"+DropValue.Value+"' and amount<>0 group by l.cust_id,c.cust_name,c.city order by cust_name");
						//rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.country='"+DropValue.Value+"' and amount<>0 and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDateInPrivMonth(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
						rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
					}/*****Add by vikas 16.11.2012****************/
					else if(DropSearchBy.SelectedItem.Text=="Customer Group")
					{
						string Spc_Pack="";
						Spc_Pack=Session["group"].ToString();
						Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
						rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.cust_type in (select customertypename from customertype where group_name in("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.cust_type in (select customertypename from customertype where group_name in("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");

						/*7.6.2013 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");*/
					}
					else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
					{
						rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
						rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
					}/*****End****************/
				}
			}
			int i=1;
			int flag=0;
			string strBillDate="";
			sw.WriteLine("Search By\t"+DropSearchBy.SelectedItem.Text+"\tConcerning Value\t"+DropValue.Value);
			if(Count_Flage==0)
			{
				if(rdr.HasRows)
				{
					int xx=0;

					/*Coment by vikas 09.09.09 if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
						sw.Write("S.No\tParty Name\tPlace\tSSR\tBill Date\t");
					else
						sw.Write("S.No\tParty Name\tPlace\tBill Date\t");*/

					if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					{
						if(ChkTrue.Checked==true)
							sw.Write("S.No\tParty Name\tPlace\tContact Person\tMobile\tSSR\tBill Date\t");
						else
							sw.Write("S.No\tParty Name\tPlace\tSSR\tBill Date\t");
					}
					else
					{
						if(ChkTrue.Checked==true)
							sw.Write("S.No\tParty Name\tPlace\tContact Person\tMobile\tBill Date\t");
						else
							sw.Write("S.No\tParty Name\tPlace\tBill Date\t");
					}

					if(rdr1.HasRows)
					{
						while(rdr1.Read())
						{
							if(xx==0)
							{
								sw.Write("Previous Dues "+GetMonthName(rdr1.GetValue(2).ToString()));
								xx=1;
							}
							sw.Write("\t"+rdr1.GetValue(2).ToString());
							arrMonth.Add(rdr1.GetValue(3).ToString());
						}
					}
					else
					{
						sw.Write("Previous Dues "+GetMonthName1());
					}
					rdr1.Close();
					sw.Write("\tTotal");
					sw.WriteLine();
					arrAmount=new double[arrMonth.Count];
					while(rdr.Read())
					{
						sw.Write(i++);
						sw.Write("\t");
						sw.Write(rdr["Cust_Name"].ToString()+"\t");
						sw.Write(rdr["City"].ToString()+"\t");
						
						if(ChkTrue.Checked==true)
						{
							sw.Write(rdr["ContactPerson"].ToString()+"\t"+rdr["Mobile"].ToString()+"\t");
						}
						/*Coment by Vikas Sharma 01.10.09 else
						{
							sw.Write("\t\t");
						}*/

						if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
						{
							rdr1=obj1.GetRecordSet("select Emp_Name from employee where Emp_ID='"+rdr["SSR"].ToString()+"'");
							if(rdr1.Read())
							{
								//sw.Write(rdr["SSR"].ToString()+"\t");
								sw.Write(rdr1["Emp_Name"].ToString()+"\t");
							}
							else
							{
								sw.Write("\t");
							}
							rdr1.Close();
						}
						strBillDate="";
	
						if(DropSearchBy.SelectedIndex==0)
							//rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where bill_no<>'o/b'  and cust_id='"+rdr["Cust_id"].ToString()+"' group by cust_id,bill_no,Bill_Date");
							rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where cust_id='"+rdr["Cust_id"].ToString()+"' and bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
						else
						{
							if(DropValue.Value=="All")
								rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where bill_no<>'o/b' and Amount<>0 and cust_id='"+rdr["Cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
							else
							{
								if(DropSearchBy.SelectedItem.Text=="SSR")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="City")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.City='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="District")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="State")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="Customer Group")/*****Add by vikas 17.11.2012****************/
								{
									string Spc_Pack="";
									Spc_Pack=Session["group"].ToString();
									Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);

									//coment by vikas 11.6.2013 rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								}
								else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								/*****End****************/
							}
						}
						while(rdr1.Read())
						{
							strBillDate+=" "+rdr1.GetValue(0).ToString()+"-"+rdr1.GetValue(1).ToString()+",";
						}
						rdr1.Close();
						if(strBillDate.Length>1)
						{
							strBillDate=strBillDate.Substring(0,strBillDate.Length-1);
						}
						if(strBillDate!="")
							sw.Write(strBillDate);
						else
							sw.Write("");
						//**********
						double PreviousDue = 0;
						//rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<'"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"'");
						string[] ss=txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length);
						string[] s=null;
						if(tempPeriod.Value!="")
							s=tempPeriod.Value.Split(new char[] {':'},tempPeriod.Value.Length);
						if(int.Parse(s[0])==int.Parse(ss[1].ToString()) && int.Parse(s[1])==int.Parse(ss[2]))
							rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateFrom.Text))+"'");
						else
							rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<'"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"'");
						if(rdr1.Read())
						{
							if(rdr1["amount"].ToString()!="")
							{
								sw.Write("\t"+Math.Round(double.Parse(rdr1["amount"].ToString())));
								arrPriviousAmt[0]+=double.Parse(rdr1["amount"].ToString());
								GTotal+=double.Parse(rdr1["amount"].ToString());
								PreviousDue=double.Parse(rdr1["amount"].ToString());
							}
							else
								sw.Write("\t0");
						}
						else
						{
							sw.Write("0");
							arrPriviousAmt[0]+=0;
						}
						rdr1.Close();
						//**********
						for(int j=0;j<arrMonth.Count;j++)
						{
							if(DropSearchBy.SelectedIndex==0)
								//rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
								rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
							else
							{
								if(DropValue.Value=="All")
									//rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
									rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
								else
								{
									if(DropSearchBy.SelectedItem.Text=="SSR")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="City")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.city='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="District")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.state='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="State")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.country='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="Customer Group")/*****Add by vikas 17.11.2012****************/
									{
										string Spc_Pack="";
										Spc_Pack=Session["group"].ToString();
										Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");

										//coment by vikas 11.6.2013 rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									}
									else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									/***End***********/
									
								}
							}
							while(rdr1.Read())
							{
								if(arrMonth[j].ToString()==rdr1.GetValue(1).ToString())
								{
									flag=1;
									if(rdr1.GetValue(1).ToString()!="" && rdr1.GetValue(1).ToString()!=null)
										arrAmount[j]=arrAmount[j]+double.Parse(GenUtil.strNumericFormat(rdr1.GetValue(0).ToString()));
									break;
								}
								else
								{
									flag=0;
								}
							}
							if(flag==1)
							{
								sw.Write("\t"+Math.Round(double.Parse(rdr1.GetValue(0).ToString())));
								flag=0;
							}
							else
							{
								flag=0;
								sw.Write("\t0");
							}
							rdr1.Close();
						}//rdr1.Close();
						//rdr1=obj1.GetRecordSet("select sum(amount) from ledgdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' group by cust_id");
						rdr1=obj1.GetRecordSet("select sum(amount) from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id");
						if(rdr1.Read())
						{
							sw.Write("\t"+Math.Round(double.Parse(rdr1.GetValue(0).ToString())+PreviousDue));
							GTotal+=double.Parse(rdr1.GetValue(0).ToString());
						}
						else
						{
							sw.Write("\t"+Math.Round(PreviousDue));
						}
						rdr1.Close();
						sw.WriteLine();
					}
					
					/*Coment by vikas 09.09.09 if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
						sw.Write("\t\t\t\t\t");
					else
						sw.Write("\t\t\t\t");*/

					/**************Add by vikas 09.09.09*****************************/
					if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					{
						if(ChkTrue.Checked==true)
                            sw.Write("\t\t\t\t\t\t\t");
						else
							sw.Write("\t\t\t\t\t");
					}
					else
					{
						if(ChkTrue.Checked==true)
							sw.Write("\t\t\t\t\t\t");
						else
							sw.Write("\t\t\t\t");
					}
					/***************end****************************/

					sw.Write(Math.Round(arrPriviousAmt[0]));
					for(int k=0;k<arrAmount.Length;k++)
					{
						sw.Write("\t"+Math.Round(double.Parse(arrAmount[k].ToString())));
					}
					sw.Write("\t"+Math.Round(double.Parse(GTotal.ToString())));
				}
				else
				{
					MessageBox.Show("Data Not Available");
				}
				sw.Close();
			}
			else
			{
				if(rdr.HasRows)
				{
					int xx=0;
					/*Coment by vikas 09.09.09 if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
						sw.Write("S.No\tParty Name\tPlace\tSSR\tBill Date\t");
					else
						sw.Write("S.No\tParty Name\tPlace\tBill Date\t");*/

					if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					{
						if(ChkTrue.Checked==true)
							sw.Write("S.No\tParty Name\tPlace\tContact Person\tMobile\tSSR\tBill Date\t");
						else
							sw.Write("S.No\tParty Name\tPlace\tSSR\tBill Date\t");
					}
					else
					{
						if(ChkTrue.Checked==true)
							sw.Write("S.No\tParty Name\tPlace\tContact Person\tMobile\tBill Date\t");
						else
							sw.Write("S.No\tParty Name\tPlace\tBill Date\t");
					}

					if(rdr1.HasRows)
					{
						while(rdr1.Read())
						{
							if(xx==0)
							{
								sw.Write("Previous Dues "+GetMonthName(rdr1.GetValue(2).ToString()));
								xx=1;
							}
							sw.Write("\t"+rdr1.GetValue(2).ToString());
							arrMonth.Add(rdr1.GetValue(3).ToString());
						}
					}
					else
					{
						sw.Write("Previous Dues "+GetMonthName1());
					}
					rdr1.Close();
					sw.Write("\tTotal");
					sw.WriteLine();
					arrAmount=new double[arrMonth.Count];
					string city="";
					while(rdr.Read())
					{
						if(city!=rdr["City"].ToString())
						{
							if(city!="")
							{
								city=rdr["City"].ToString();
								sw.Write("\t\t\t\t\t");
								for(int f=0;f<arrMonth.Count;f++)
								{
									sw.Write("\t");
								}
								sw.Write("\t");
								sw.WriteLine();
								
							}
						}
						city=rdr["City"].ToString();
						sw.Write(i++);
						sw.Write("\t");
						sw.Write(rdr["Cust_Name"].ToString()+"\t");
						sw.Write(rdr["City"].ToString()+"\t");
						
						if(ChkTrue.Checked==true)
						{
							sw.Write(rdr["ContactPerson"].ToString()+"\t"+rdr["Mobile"].ToString()+"\t");
						}
						/*Coment by vikas 01.10.09 else
						{
							sw.Write("\t\t");
						}*/

						if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
						{
							rdr1=obj1.GetRecordSet("select Emp_Name from employee where Emp_ID='"+rdr["SSR"].ToString()+"'");
							if(rdr1.Read())
							{
								//sw.Write(rdr["SSR"].ToString()+"\t");
								sw.Write(rdr1["Emp_Name"].ToString()+"\t");
							}
							else
							{
								sw.Write("\t");
							}
							rdr1.Close();
						}
						strBillDate="";
	
						if(DropSearchBy.SelectedIndex==0)
							//rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where bill_no<>'o/b'  and cust_id='"+rdr["Cust_id"].ToString()+"' group by cust_id,bill_no,Bill_Date");
							rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where cust_id='"+rdr["Cust_id"].ToString()+"' and bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
						else
						{
							if(DropValue.Value=="All")
								rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where bill_no<>'o/b' and Amount<>0 and cust_id='"+rdr["Cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
							else
							{
								if(DropSearchBy.SelectedItem.Text=="SSR")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="City")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.City='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="District")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="State")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								else if(DropSearchBy.SelectedItem.Text=="Customer Group")/******Add by vikas 17.11.2012********************/
								{
									string Spc_Pack="";
									Spc_Pack=Session["group"].ToString();
									Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
									
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name in("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
									
									//Coment By vikas 11.6.2013 rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								}
								else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
									rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
								/********End********************/
							}
						}
						while(rdr1.Read())
						{
							strBillDate+=" "+rdr1.GetValue(0).ToString()+"-"+rdr1.GetValue(1).ToString()+",";
						}
						rdr1.Close();
						if(strBillDate.Length>1)
						{
							strBillDate=strBillDate.Substring(0,strBillDate.Length-1);
						}
						if(strBillDate!="")
							sw.Write(strBillDate);
						else
							sw.Write("");
						//**********
						double PreviousDue = 0;
						//rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<'"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"'");
						string[] ss=txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length);
						string[] s=null;
						if(tempPeriod.Value!="")
							s=tempPeriod.Value.Split(new char[] {':'},tempPeriod.Value.Length);
						if(int.Parse(s[0])==int.Parse(ss[1].ToString()) && int.Parse(s[1])==int.Parse(ss[2]))
							rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateFrom.Text))+"'");
						else
							rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<'"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"'");
						if(rdr1.Read())
						{
							if(rdr1["amount"].ToString()!="")
							{
								sw.Write("\t"+Math.Round(double.Parse(rdr1["amount"].ToString())));
								arrPriviousAmt[0]+=double.Parse(rdr1["amount"].ToString());
								GTotal+=double.Parse(rdr1["amount"].ToString());
								PreviousDue=double.Parse(rdr1["amount"].ToString());
							}
							else
								sw.Write("\t0");
						}
						else
						{
							sw.Write("0");
							arrPriviousAmt[0]+=0;
						}
						rdr1.Close();
						//**********
						for(int j=0;j<arrMonth.Count;j++)
						{
							if(DropSearchBy.SelectedIndex==0)
								//rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
								rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
							else
							{
								if(DropValue.Value=="All")
									//rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
									rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
								else
								{
									if(DropSearchBy.SelectedItem.Text=="SSR")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="City")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.city='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="District")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.state='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="State")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.country='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									else if(DropSearchBy.SelectedItem.Text=="Customer Group")
									{
										string Spc_Pack="";
										Spc_Pack=Session["group"].ToString();
										Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);

										//Coment By vikas 11.6.2013 rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name in("+Spc_Pack+")) and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
									}
									else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
										rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
								}
							}
							while(rdr1.Read())
							{
								if(arrMonth[j].ToString()==rdr1.GetValue(1).ToString())
								{
									flag=1;
									if(rdr1.GetValue(1).ToString()!="" && rdr1.GetValue(1).ToString()!=null)
										arrAmount[j]=arrAmount[j]+double.Parse(GenUtil.strNumericFormat(rdr1.GetValue(0).ToString()));
									break;
								}
								else
								{
									flag=0;
								}
							}
							if(flag==1)
							{
								sw.Write("\t"+Math.Round(double.Parse(rdr1.GetValue(0).ToString())));
								flag=0;
							}
							else
							{
								flag=0;
								sw.Write("\t0");
							}
							rdr1.Close();
						}//rdr1.Close();
						//rdr1=obj1.GetRecordSet("select sum(amount) from ledgdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' group by cust_id");
						rdr1=obj1.GetRecordSet("select sum(amount) from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id");
						if(rdr1.Read())
						{
							sw.Write("\t"+Math.Round(double.Parse(rdr1.GetValue(0).ToString())+PreviousDue));
							GTotal+=double.Parse(rdr1.GetValue(0).ToString());
						}
						else
						{
							sw.Write("\t"+Math.Round(PreviousDue));
						}
						rdr1.Close();
						sw.WriteLine();
					}
					
					/*Coment by vikas 09.09.09 if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
						sw.Write("\t\t\t\t\t");
					else
						sw.Write("\t\t\t\t");*/

					/**************Add by vikas 09.09.09*****************************/
					if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					{
						if(ChkTrue.Checked==true)
							sw.Write("\t\t\t\t\t\t\t");
						else
							sw.Write("\t\t\t\t\t");
					}
					else
					{
						if(ChkTrue.Checked==true)
							sw.Write("\t\t\t\t\t\t");
						else
							sw.Write("\t\t\t\t");
					}
					/***************end****************************/

					sw.Write(Math.Round(arrPriviousAmt[0]));
					for(int k=0;k<arrAmount.Length;k++)
					{
						sw.Write("\t"+Math.Round(double.Parse(arrAmount[k].ToString())));
					}
					sw.Write("\t"+Math.Round(double.Parse(GTotal.ToString())));
				}
				else
				{
					MessageBox.Show("Data Not Available");
				}
				sw.Close();
			}
		}

		/// <summary>
		/// Prepares the excel report file CreditPeriodAnalysisSheetReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Count==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:CreditPeriodAnalysisSheetReport.aspx,Class:InventoryClass.cs,Method:btnExcel_Click   Credit Period analysis Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method return the month name till three charector with the help of passing value.
		/// </summary>
		/// <param name="mon"></param>
		/// <returns></returns>
		public string GetMonthName(string mon)
		{
			if(mon.IndexOf(" ")>0)
			{
				string[] month=mon.Split(new char[] {' '},mon.Length);
				if(month[0].ToString()=="Jan")
					return "Dec";
				else if(month[0].ToString()=="Feb")
					return "Jan";
				else if(month[0].ToString()=="Mar")
					return "Feb";
				else if(month[0].ToString()=="Apr")
					return "Mar";
				else if(month[0].ToString()=="May")
					return "Apr";
				else if(month[0].ToString()=="Jun")
					return "May";
				else if(month[0].ToString()=="Jul")
					return "Jun";
				else if(month[0].ToString()=="Aug")
					return "Jul";
				else if(month[0].ToString()=="Sep")
					return "Aug";
				else if(month[0].ToString()=="Oct")
					return "Sep";
				else if(month[0].ToString()=="Nov")
					return "Oct";
				else if(month[0].ToString()=="Dec")
					return "Nov";
			}
			return "";
		}

		/// <summary>
		/// This method return the month name till three charector from givan from date.
		/// </summary>
		/// <returns></returns>
		public string GetMonthName1()
		{
			string[] month=txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length);
			if(month[1].ToString()=="1" || month[1].ToString()=="01")
				return "Dec";
			else if(month[1].ToString()=="2" || month[1].ToString()=="02")
				return "Jan";
			else if(month[1].ToString()=="3" || month[1].ToString()=="03")
				return "Feb";
			else if(month[1].ToString()=="4" || month[1].ToString()=="04")
				return "Mar";
			else if(month[1].ToString()=="5" || month[1].ToString()=="05")
				return "Apr";
			else if(month[1].ToString()=="6" || month[1].ToString()=="06")
				return "May";
			else if(month[1].ToString()=="7" || month[1].ToString()=="07")
				return "Jun";
			else if(month[1].ToString()=="8" || month[1].ToString()=="08")
				return "Jul";
			else if(month[1].ToString()=="9" || month[1].ToString()=="09")
				return "Aug";
			else if(month[1].ToString()=="10")
				return "Sep";
			else if(month[1].ToString()=="11")
				return "Oct";
			else if(month[1].ToString()=="12")
				return "Nov";
			else
				return "";
		}

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value from dropdownlist 
		/// with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strState="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012

				strDistrict = "select distinct state from customer";
				strState = "select distinct country from customer";
				strPlace = "select distinct city from customer";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012

				//Coment by vikas 01.10.09 strSSR = "select emp_name from employee where emp_id in(select ssr from customer)";
				strSSR = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";
				//Coment by vikas 01.10.09 string[] arrStr = {strState,strDistrict,strPlace,strSSR};
				//Coment by vikas 01.10.09 HtmlInputHidden[] arrCust = {tempState,tempDistrict,tempPlace,tempSSR};	

				//Coment by vikas 16.11.2012 string[] arrStr = {strDistrict,strPlace,strSSR};
				//Coment by vikas 16.11.2012 HtmlInputHidden[] arrCust = {tempDistrict,tempPlace,tempSSR};	

				string[] arrStr = {strDistrict,strPlace,strSSR,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempDistrict,tempPlace,tempSSR,tempGroup,tempSubGroup};	

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//DropValue.Items.Add(rdr.GetValue(0).ToString());
							//tempCustName.Value+=rdr.GetValue(0).ToString()+",";
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
		/// This method return the from date that should started from 1 of passing the date.
		/// </summary>
		/// <param name="FD"></param>
		/// <returns></returns>
		public string getFromDateInPrivMonth(string FD)
		{
			string[] FromDate = FD.Split(new char[] {'/'},FD.Length);
			FromDate[1]=System.Convert.ToString(int.Parse(FromDate[1])-1);
			return "1"+"/"+FromDate[1]+"/"+FromDate[2];
		}

		/// <summary>
		/// This method return the to date that should started from day in a month of passing the date.
		/// </summary>
		/// <param name="FD"></param>
		/// <returns></returns>
		public string getToDate(string FD)
		{
			string[] FromDate = FD.Split(new char[] {'/'},FD.Length);
			int day=DateTime.DaysInMonth(int.Parse(FromDate[2]),int.Parse(FromDate[1]));
			return day.ToString()+"/"+FromDate[1]+"/"+FromDate[2];
		}

		/// <summary>
		/// This method return the from date that should started from 1 of passing the date.
		/// </summary>
		/// <param name="FD"></param>
		/// <returns></returns>
		public string getFromDate(string FD)
		{
			string[] FromDate = FD.Split(new char[] {'/'},FD.Length);
			return "1"+"/"+FromDate[1]+"/"+FromDate[2];
		}
	}
}
