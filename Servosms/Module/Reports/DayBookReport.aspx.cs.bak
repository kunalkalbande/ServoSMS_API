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
	/// Summary description for DayBookReport.
	/// </summary>
	public partial class DayBookReport : System.Web.UI.Page
	{
		string uid,msg="";
		double debit_total = 0;
		double credit_total = 0;
		string strorderby="";
		static string FromDate="",ToDate="";
	
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
				if(!Page.IsPostBack)
				{
					GrdDayBook.Visible=false;
					txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
					txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:Pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Get FromDate and ToDate From Organisation
				InventoryClass obj1=new InventoryClass();
				SqlDataReader rdr1=null;
				rdr1=obj1.GetRecordSet("select * from organisation");
				if(rdr1.Read())
				{
					FromDate=GetYear(GenUtil.trimDate(rdr1["Acc_date_from"].ToString()));
					ToDate=GetYear(GenUtil.trimDate(rdr1["Acc_date_To"].ToString()));
				}
				#endregion
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="11";
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
		/// This method return the query string for apply many condition when u select.
		/// </summary>
		public string GetQuery()
		{
			
			int Flag=0;
			string sql="";
			msg="";
			if(chkReceipt.Checked==true || chkPayment.Checked==true || chkContra.Checked==true || chkJournel.Checked==true || chkCN.Checked==true || chkDN.Checked==true || chkCS.Checked==true || chkVan.Checked==true || chkCredit.Checked==true || chkOtherPer.Checked==true)
			{
				sql="select * from AccountsLedgerTable alt,Ledger_Master lm where lm.ledger_id!=(select ledger_id from Ledger_Master where sub_grp_id=118) and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' and (";
				if(chkReceipt.Checked)
				{
					if(Flag==0)
						sql+=" Particulars like 'Receipt%'";
					else
						sql+=" or Particulars like 'Receipt%'";
					Flag=1;
					msg+="Receipt, ";
				}
				if(chkPayment.Checked)
				{
					if(Flag==0)
						sql+=" Particulars like 'Payment%'";
					else
						sql+=" or Particulars like 'Payment%'";
					Flag=1;
					msg+="Payment, ";
				}
				if(chkContra.Checked)
				{
					if(Flag==0)
						sql+=" Particulars like 'Contra%'";
					else
						sql+=" or Particulars like 'Contra%'";
					Flag=1;
					msg+="Contra, ";
				}
				if(chkJournel.Checked)
				{
					if(Flag==0)
						sql+=" Particulars like 'Journal%'";
					else
						sql+=" or Particulars like 'Journal%'";
					Flag=1;
					msg+="Journel, ";
				}
				if(chkCN.Checked)
				{
					if(Flag==0)
						sql+=" Particulars like 'Credit Note%'";
					else
						sql+=" or Particulars like 'Credit Note%'";
					Flag=1;
					msg+="CN, ";
				}
				if(chkDN.Checked)
				{
					if(Flag==0)
						sql+=" Particulars like 'Debit Note%'";
					else
						sql+=" or Particulars like 'Debit Note%'";
					Flag=1;
					msg+="DN, ";
				}
				if(chkCredit.Checked)
				{
					if(Flag==0)
						//sql+=" Particulars in (select 'Sales Invoice ('+cast(Invoice_No as varchar)+')' from Sales_Master where sales_type='Credit' and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ ToMMddYYYY(txtDateTo.Text) +"')";
						sql+=" Particulars in (select 'Sales Invoice ('+cast(Invoice_No as varchar)+')' from Sales_Master where sales_type='Credit' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"')";
					else
						//sql+=" or Particulars in (select 'Sales Invoice ('+cast(Invoice_No as varchar)+')' from Sales_Master where sales_type='Credit'  and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ ToMMddYYYY(txtDateTo.Text) +"')";
						sql+=" or Particulars in (select 'Sales Invoice ('+cast(Invoice_No as varchar)+')' from Sales_Master where sales_type='Credit'  and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"')";
					Flag=1;
					msg+="SWS, ";
				}
				if(chkCS.Checked)
				{
					if(Flag==0)
						//sql+=" Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Cash' and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ ToMMddYYYY(txtDateTo.Text) +"')";
						sql+=" Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Cash' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"')";
					else
						//sql+=" or Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Cash' and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ ToMMddYYYY(txtDateTo.Text) +"')";
						sql+=" or Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Cash' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"')";
					Flag=1;
					msg+="CS, ";
				}
				if(chkVan.Checked)
				{
					if(Flag==0)
						//sql+=" Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Van' and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ ToMMddYYYY(txtDateTo.Text) +"')";
						sql+=" Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Van' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"')";
					else
						//sql+=" or Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Van' and cast(floor(cast(Entry_Date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Entry_Date as float)) as datetime)<='"+ ToMMddYYYY(txtDateTo.Text) +"')";
						sql+=" or Particulars in (select 'Sales Invoice ('+cast(invoice_no as varchar)+')' from sales_master where sales_type='Van' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"')";
					Flag=1;
					msg+="FCS, ";
				}
				if(chkOtherPer.Checked)
				{
					if(Flag==0)
						sql+=" Particulars in (select distinct 'Purchase Invoice ('+cast(pm.Invoice_No as varchar)+')' from Purchase_Master pm,Purchase_Details pd where pd.Invoice_No=pm.Invoice_No)";
					else
						sql+=" or Particulars in (select distinct 'Purchase Invoice ('+cast(pm.Invoice_No as varchar)+')' from Purchase_Master pm,Purchase_Details pd where pd.Invoice_No=pm.Invoice_No)";
					Flag=1;
					msg+="Other Pur., ";
				}
				if(chkCS.Checked)
					sql+=") and alt.Ledger_ID=lm.Ledger_ID order by particulars";
					//sql+=")";// and alt.Ledger_ID=lm.Ledger_ID order by particulars";
				else
					sql+=") and lm.ledger_id!=1054 and alt.Ledger_ID=lm.Ledger_ID order by particulars";
				//sql+=")"; //and lm.ledger_id!=1054 and alt.Ledger_ID=lm.Ledger_ID order by particulars";
			}
			else
			{
				MessageBox.Show("Please Select Atleast One CheckBox");
				GrdDayBook.Visible=false;
				//return;
			}
			if(Flag==0)
				return "";
			else
				return sql;
		}

		/// <summary>
		/// This method is used to binding the datagrid.
		/// </summary>
		public void BindTheData()
		{
			try
			{
				SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				string sql=GetQuery();
				DataSet ds= new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(sql, SqlCon);
				da.Fill(ds,"accountsledgertable");
				DataTable dtCustomers = ds.Tables[0];
				DataView dv=new DataView(dtCustomers);
				dv.Sort = strorderby;
				Cache["SalesBook"]=strorderby;
				if(dv.Count!=0)
				{
					GrdDayBook.DataSource = dv;
					GrdDayBook.DataBind();
					GrdDayBook.Visible=true;
				}
				else
				{
					MessageBox.Show("Data not available");
					GrdDayBook.Visible=false;
				}
				SqlCon.Dispose();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:BindThedata()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// this method is used to view the report and set value in session variable
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			try
			{
				strorderby="Entry_Date ASC";
				Session["Column"]="Entry_Date";
				Session["order"]="ASC";
				BindTheData();
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Class:DBOperation_LETEST.cs,Method:btnView_Click  Day Book Report  Viewed  useried "+uid);	
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:btnView  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		# region DateTime Function...
		//This function return date mm/dd/yyyy format.
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
		# endregion

		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					string str = e.Item.Cells[0].Text;
					string trans_no = "";
					// if transaction type is Opening Balance then show the blank value in transaction no.
					//if(str.StartsWith("Opening"))
					//{
					//	e.Item.Cells[0].Text = "&nbsp;";
					//}
					//else
					//{
					// else show take the substring and display the no. in transaction no. and assign the remaining substring to transaction type.
					trans_no = str.Substring(str.IndexOf("(")+1);
					if(str.StartsWith("Sales Invoice"))
					{
						if(trans_no.IndexOf(")")>0)
							trans_no = trans_no.Substring(0,trans_no.Length-1);
						if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
							trans_no=trans_no.Substring(4);
						else
							trans_no=trans_no.Substring(3);
					}
					else
						trans_no = trans_no.Substring(0,trans_no.Length-1);
					str = str.Substring(0,str.IndexOf("("));
					e.Item.Cells[1].Text = trans_no ;
					e.Item.Cells[0].Text = str.Trim();
					//}
					// Calls the Totaldebit() and TotalCredit() function to increment the total values for each row.
					//This function hidden by mahesh (08/11/06)
					TotalDebit(Double.Parse(e.Item.Cells[4].Text));
					TotalCredit(Double.Parse(e.Item.Cells[5].Text)); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					//sum of cell[3],cell[4] hidden by mahesh (08/11/06)
					//e.Item.Cells[3].Text = debit_total.ToString("N",nfi);
					//e.Item.Cells[4].Text = credit_total.ToString("N",nfi);
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(debit_total.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(credit_total.ToString());
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// calculates the total debit amount by passing value
		/// </summary>
		protected void TotalDebit(double _debittotal)
		{
			debit_total  += _debittotal;
		}

		/// <summary>
		/// calculates total credit amount by passing value
		/// </summary>
		protected void TotalCredit(double _credittotal)
		{
			credit_total  += _credittotal;
		}

		/// <summary>
		/// Prepares the report file DayBookReport.txt for printing.
		/// </summary>
		protected void Btnprint_Click(object sender, System.EventArgs e)
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader rdr;//,rdr1=null;

				// Get the home drive and opens the file CustomerLedger.txt in ServosmsPrintServices folder.
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				//string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\CustomerLedgerReport.txt";
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\DayBookReport.txt";
				//StreamWriter sw = new StreamWriter(path);
				StreamWriter sw = new StreamWriter(path);

				sw.Write((char)27);//added by vishnu
				sw.Write((char)67);//added by vishnu
				sw.Write((char)0);//added by vishnu
				sw.Write((char)12);//added by vishnu
			
				sw.Write((char)27);//added by vishnu
				sw.Write((char)78);//added by vishnu
				sw.Write((char)5);//added by vishnu
				
				// Condensed
				sw.Write((char)27);//added by vishnu
				sw.Write((char)15);
				sw.WriteLine("");

				string sql=GetQuery();
				rdr=obj.GetRecordSet(sql);
				if(rdr.HasRows)
				{
					string des="----------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Day Book Report "+txtDateFrom.Text+" to "+txtDateTo.Text,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("========================================",des.Length));
					//sw.WriteLine(" Type : "+msg);
					sw.WriteLine("+--------+--------------------+----------+-------------------------+------------+------------+");
					sw.WriteLine("|Trans.ID|  Transaction Name  |   Date   |      Account Name       |   Debit    |   Credit   |");
					sw.WriteLine("+--------+--------------------+----------+-------------------------+------------+------------+");
					//             12345678 12345678901234567890 mm/dd/yyyy 1234567890123456789012345 123456789012 123456789012
					string info=" {0,-8:S} {1,-20:S} {2,-10:S} {3,-25:S} {4,12:S} {5,12:S}";
					string trans_type="",trans_id="";
					double debit=0,credit=0;
					
					while(rdr.Read())
					{
						// if transaction type is opening balane then display the blank value in transaction ID.
						trans_type = rdr["Particulars"].ToString();
						trans_id = trans_type.Substring(trans_type.IndexOf("(")+1);
						if(trans_type.StartsWith("Sales Invoice"))
						{
							if(trans_id.IndexOf(")")>0)
								trans_id = trans_id.Substring(0,trans_id.Length-1);
							int len = System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length;
							if(len>3)
								trans_id=trans_id.Substring(4);
							else
								trans_id=trans_id.Substring(3);
						}
						else
							trans_id = trans_id.Substring(0,trans_id.Length-1);
						trans_type = trans_type.Substring(0,trans_type.IndexOf("(")).Trim();  
				
						// Calculate the total debit and credit and set the last value of balance and balance type into Bal.
						debit += Double.Parse(rdr["Debit_Amount"].ToString());  
						credit += Double.Parse(rdr["Credit_Amount"].ToString());
						sw.WriteLine(info,trans_id,GenUtil.TrimLength(trans_type,20),
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Entry_Date"].ToString())),
							GenUtil.TrimLength(rdr["Ledger_Name"].ToString(),25),
							GenUtil.strNumericFormat(rdr["Debit_Amount"].ToString()),
							GenUtil.strNumericFormat(rdr["Credit_Amount"].ToString()));
					}
					sw.WriteLine("+--------+--------------------+----------+-------------------------+------------+------------+");
					sw.WriteLine(info,"Total","","","",GenUtil.strNumericFormat(debit.ToString()),GenUtil.strNumericFormat(credit.ToString()));
					sw.WriteLine("+--------+--------------------+----------+-------------------------+------------+------------+");
				}
				rdr.Close();
				sw.Close();
				Print();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:btnPrint_Click  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr;

			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\DayBookReport.xls";
			StreamWriter sw = new StreamWriter(path);

			string sql=GetQuery();
			rdr=obj.GetRecordSet(sql);
			//sw.WriteLine("Type\t"+msg);
			sw.WriteLine("Trans.ID\tTransaction Name\tDate\tAccount Name\tDebit\tCredit");
			if(rdr.HasRows)
			{
				string trans_type="",trans_id="";
				double debit=0,credit=0;
				while(rdr.Read())
				{
					// if transaction type is opening balane then display the blank value in transaction ID.
					trans_type = rdr["Particulars"].ToString();
					trans_id = trans_type.Substring(trans_type.IndexOf("(")+1);
					if(trans_type.StartsWith("Sales Invoice"))
					{
						if(trans_id.IndexOf(")")>0)
							trans_id = trans_id.Substring(0,trans_id.Length-1);
						int len = System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length;
						if(len>3)
							trans_id=trans_id.Substring(4);
						else
							trans_id=trans_id.Substring(3);
					}
					else
						trans_id = trans_id.Substring(0,trans_id.Length-1);
					trans_type = trans_type.Substring(0,trans_type.IndexOf("(")).Trim();  
				
					// Calculate the total debit and credit and set the last value of balance and balance type into Bal.
					debit += Double.Parse(rdr["Debit_Amount"].ToString());  
					credit += Double.Parse(rdr["Credit_Amount"].ToString());
					sw.WriteLine(trans_id+"\t"+GenUtil.TrimLength(trans_type,20)+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Entry_Date"].ToString()))+"\t"+
						rdr["Ledger_Name"].ToString()+"\t"+
						rdr["Debit_Amount"].ToString()+"\t"+
						rdr["Credit_Amount"].ToString());
				}
				sw.WriteLine("Total\t\t\t\t"+GenUtil.strNumericFormat(debit.ToString())+"\t"+GenUtil.strNumericFormat(credit.ToString()));
			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file DayBookReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GrdDayBook.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method: btnExcel_Click, DayBook Report Convert Into Excel Format ,  userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:btnExcel_Click   DayBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// contacst the Print_WiindowServices via socket and sends the CustomerLedger.txt file name to print.
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
					
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\DayBookReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());

					 
					CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DayBookReport.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of the datagridheader.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				BindTheData();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method return only year part of passing the date.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetYear(string dt)
		{
			if(dt!="")
			{
				string[] year=dt.IndexOf("/")>0?dt.Split(new char[] {'/'},dt.Length): dt.Split(new char[] { '-' }, dt.Length);
				string yr=year[2].Substring(2);	
				return(yr);
			}
			else
				return "";
		}
	}
}