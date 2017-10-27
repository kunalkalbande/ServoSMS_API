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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using DBOperations; 

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SchemeSecondrySales.
	/// </summary>
	public partial class SchemeSecondrySales : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
		string uid;
		public int i=1;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SchemeSecondrySales.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="61";
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
				gridSchemeSecondry.Visible=false;
				txtDateFrom.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
				txtDateTo.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
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

		public string Discount(string Prod_id)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select distinct discount from oilscheme where schname='Secondry(LTR Scheme)' and prodid="+Prod_id+" and (cast(floor(cast(datefrom as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(dateto as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) or cast(floor(cast(datefrom as float)) as datetime) between Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) or cast(floor(cast(dateto as float)) as datetime) between Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
			string sub="";
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString().IndexOf(" ")>0)
				{
					sub=rdr.GetValue(0).ToString();
					return sub;
				}
				else
					return rdr.GetValue(0).ToString();
			}
			else
			{
				return "0";
			}
			//rdr.Close();
		}

		public double TotClaim=0;
		public string DiscAmt(string Prod_id,string Qty)
		{
			double ltr=double.Parse(Qty.ToString());
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select distinct discount from oilscheme where schname='Secondry(LTR Scheme)' and prodid="+Prod_id+" and (cast(floor(cast(datefrom as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(dateto as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) or cast(floor(cast(datefrom as float)) as datetime) between Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) or cast(floor(cast(dateto as float)) as datetime) between Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
			string sub="";
			if(rdr.Read())
			{
				//if(rdr.GetValue(0).ToString().IndexOf(" ")>0)
				if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
				{
					sub=rdr.GetValue(0).ToString();
					ltr=Math.Round(ltr*double.Parse(sub));
					sub=ltr.ToString();
					TotClaim+=double.Parse(sub);
					return sub;
				}
				else
					return rdr.GetValue(0).ToString();
			}
			else
			{
				return "0";
			}
			rdr.Close();
		}
		

		/// <summary>
		/// This method is used to view the Purchase Book report with the help of Bindthedata() function and
		/// set the column name with ascending order in Session variable.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {
					MessageBox.Show("Date From Should be less than Date To");
					gridSchemeSecondry.Visible=false;
				}
				else
				{
					strOrderBy = "Invoice_No ASC";
					Session["Column"] = "Invoice_No";
					Session["Order"] = "ASC";
					BindTheData();
				}
				CreateLogFiles.ErrorLog("Form:SchemeSecondrySales.aspx,Method:btnShow_Click"+ " SchemeSecondrySales Viewed "+" userid "+ uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SchemeSecondrySales.aspx,Method:btnShow_Click"+  " SchemeSecondrySales Viewed "+"  EXCEPTION "+ex.Message+" userid  "+uid);
			}
		}

		/// <summary>
		/// To bind the datagrid and display the information by given order and display the data grid.
		/// </summary>
		public void BindTheData()
		{
			string sqlstr="";
			SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			if(chkFOC.Checked)
				sqlstr="select distinct cust_name,p.Prod_ID,prod_name+':'+pack_type ProdName,Pack_Type,Prod_Code,cust_type,substring(cast(sm.invoice_no as varchar),4,10) Invoice_No,sm.invoice_date,qty*total_qty as qty,Amount,c.cust_id,qty ltr from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) order by invoice_No";
			else
				sqlstr="select distinct cust_name,p.Prod_ID,prod_name+':'+pack_type ProdName,Pack_Type,Prod_Code,cust_type,substring(cast(sm.invoice_no as varchar),4,10) Invoice_No,sm.invoice_date,qty*total_qty as qty,Amount,c.cust_id,qty ltr from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and amount>0 and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) order by invoice_No";
			DataSet ds= new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			da.Fill(ds, "Sales_Master");
			DataTable dtCustomers = ds.Tables["Sales_Master"];
			DataView dv=new DataView(dtCustomers);
			dv.Sort = strOrderBy;
			Cache["strOrderBy"]=strOrderBy;
			if(droptype.SelectedIndex==0)
			{
				if(dv.Count==0)
				{
					MessageBox.Show("Data not available");
					gridSchemeSecondry.Visible=false;
				}
				else
				{
					gridSchemeSecondry.DataSource=dv;
					gridSchemeSecondry.DataBind();
					gridSchemeSecondry.Visible=true;
					DatagPWSDC.Visible=false;
				}
			}
			else
			{
				/*//if(chkFOC.Checked)
					sqlstr="select distinct cust_name,p.Prod_ID,prod_name+':'+pack_type ProdName,Pack_Type,Prod_Code,cust_type,substring(cast(sm.invoice_no as varchar),4,10) Invoice_No,sm.invoice_date,qty*total_qty as qty,Amount,c.cust_id from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) order by invoice_No";
				//else
				//	sqlstr="select distinct cust_name,p.Prod_ID,prod_name+':'+pack_type ProdName,Pack_Type,Prod_Code,cust_type,substring(cast(sm.invoice_no as varchar),4,10) Invoice_No,sm.invoice_date,qty*total_qty as qty,Amount,c.cust_id from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and amount>0 and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by invoice_No";
				DataSet ds= new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
				da.Fill(ds, "Sales_Master");
				DataTable dtCustomers = ds.Tables["Sales_Master"];
				DataView dv=new DataView(dtCustomers);
				dv.Sort = strOrderBy;
				Cache["strOrderBy"]=strOrderBy;*/
				if(dv.Count==0)
				{
					MessageBox.Show("Data not available");
					DatagPWSDC.Visible=false;
				}
				else
				{
					DatagPWSDC.DataSource=dv;
					DatagPWSDC.DataBind();
					DatagPWSDC.Visible=true;
					gridSchemeSecondry.Visible=false;
				}
			}
		}
		
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnSortCommand"
		/// </summary>
		public void SortCommand_Click(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				//Check to see if same column clicked again
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["Order"].Equals("ASC"))
					{
						strOrderBy=e.SortExpression.ToString() +" DESC";
						Session["Order"]="DESC";
					}
					else
					{
						strOrderBy=e.SortExpression.ToString() +" ASC";
						Session["Order"]="ASC";
					}
				}
					//Different column selected, so default to ascending order
				else
				{
					strOrderBy = e.SortExpression.ToString() +" ASC";
					Session["Order"] = "ASC";
				}
				Session["Column"] = e.SortExpression.ToString();
				BindTheData();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SchemeSecondrySales.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid ");
			}
		}

		/// <summary>
		/// return date in MM/DD/YYYY format.
		/// </summary>
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

		public double TotalRo=0,TotalBazar=0,TotalFleet=0,TotalOE=0,TotalOthers=0,TotalAmount=0,TotalQty=0;
		/// <summary>
		/// This method returns only ro,ksk,n-ksk and ibp type of customer value.
		/// </summary>
		public string GetRO(string Cust_Type,string Qty)
		{
			double tot=0;
			if(Cust_Type.ToLower().StartsWith("ro") || Cust_Type.ToLower().StartsWith("ksk") || Cust_Type.ToLower().StartsWith("n-ksk") || Cust_Type.ToLower().StartsWith("ibp"))
				tot=double.Parse(Qty);
			TotalRo+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method return only Bazar typ of customer value.
		/// </summary>
		public string GetBazar(string Cust_Type,string Qty)
		{
			double tot=0;
			if(Cust_Type.ToLower().StartsWith("baz"))
				tot=double.Parse(Qty);
			TotalBazar+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method return only Fleet type of customer value.
		/// </summary>
		public string GetFleet(string Cust_Type,string Qty)
		{
			double tot=0;
			if(Cust_Type.ToLower().StartsWith("fleet"))
				tot=double.Parse(Qty);
			TotalFleet+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// this method return only OE type of customer value.
		/// </summary>
		public string GetOE(string Cust_Type,string Qty)
		{
			double tot=0;
			if(Cust_Type.ToLower().StartsWith("oe"))
				tot=double.Parse(Qty);
			TotalOE+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method return the only sko customer type value.
		/// </summary>
		public string GetOthers(string Cust_Type,string Qty)
		{
			double tot=0;
			if(Cust_Type.ToLower().StartsWith("sko"))
				tot=double.Parse(Qty);
			TotalOthers+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method is used to return the discount value with the help of given product id and date.
		/// </summary>
		double DiscPerLtr=0;
		public string GetDiscount(string ProdID,string Dt)
		{
			double tot = 0;
			DiscPerLtr=0;
			InventoryClass obj = new InventoryClass();

			SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schname='Secondry SP(LTRSP Scheme)' and prodid='"+ProdID+ "' and cast(floor(cast(datefrom as float)) as datetime)<=Convert(datetime,'" + Dt + "',103) and cast(floor(cast(dateto as float)) as datetime)>=Convert(datetime,'" + Dt + "',103)");
			if(rdr.Read())
			{
				tot=double.Parse(rdr.GetValue(0).ToString());
			}
			DiscPerLtr=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method return the value of multiply the qty and discount per liter value.
		/// </summary>
		public string GetAmount(string Amt)
		{
			double tot=0;
			tot=double.Parse(Amt)*DiscPerLtr;
			TotalAmount+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// Prepares the excel report file SchemeSecondrySales.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(gridSchemeSecondry.Visible==true || DatagPWSDC.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SchemeSecondrySales.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SchemeSecondrySales Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:SchemeSecondrySales.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SchemeSecondrySales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			i=1;
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SchemeSecondrySalesReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			if(chkFOC.Checked)
				sql="select distinct cust_name,p.Prod_ID,prod_name+':'+pack_type ProdName,Pack_Type,Prod_Code,cust_type,substring(cast(sm.invoice_no as varchar),4,10) Invoice_No,sm.invoice_date,qty*total_qty as qty,Amount,c.cust_id,qty ltr from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			else
				sql="select distinct cust_name,p.Prod_ID,prod_name+':'+pack_type ProdName,Pack_Type,Prod_Code,cust_type,substring(cast(sm.invoice_no as varchar),4,10) Invoice_No,sm.invoice_date,qty*total_qty as qty,Amount,c.cust_id,qty ltr from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and amount>0 and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			sql=sql+" order by "+Cache["strOrderBy"];
			
			rdr=obj.GetRecordSet(sql);
			if(droptype.SelectedIndex==0)
			{
				sw.WriteLine("Product\tPack Type\tMaterial Code\tCustomer Name\tName of Customer Category\t\t\t\t\tCustomer Category\tInvoice No\tInvoice Date\tLtr/Discount\tDiscount allowed since previous report");
				sw.WriteLine("\t\t\t\tRO\tBazar\tFleet\tOE\tOthers\t\t\t\t\tQty(KL)\tAmount(Rs.)");
			}
			else
			{
				sw.WriteLine("SN\tDate\tBill No\tCust Type\tName Of Customer\tProduct Code\tProd Name\tPack TYpe\tQty\tLTR\tDiscount\tSch Disc Total");
			}
			while(rdr.Read())
			{
				if(droptype.SelectedIndex==0)
				{
					sw.WriteLine(rdr["ProdName"].ToString()+"\t"+
						rdr["Pack_type"].ToString()+"\t"+
						rdr["Prod_Code"].ToString()+"\t"+
						rdr["Cust_Name"].ToString()+"\t"+
						GetRO(rdr["Cust_Type"].ToString(),rdr["qty"].ToString())+"\t"+
						GetBazar(rdr["Cust_Type"].ToString(),rdr["qty"].ToString())+"\t"+
						GetFleet(rdr["Cust_Type"].ToString(),rdr["qty"].ToString())+"\t"+
						GetOE(rdr["Cust_Type"].ToString(),rdr["qty"].ToString())+"\t"+
						GetOthers(rdr["Cust_Type"].ToString(),rdr["qty"].ToString())+"\t"+
						rdr["Cust_Type"].ToString()+"\t"+
						rdr["Invoice_No"].ToString()+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
						GetDiscount(rdr["Prod_ID"].ToString(),rdr["Invoice_Date"].ToString())+"\t"+
						rdr["Qty"].ToString()+"\t"+
						GetAmount(rdr["Qty"].ToString())
						);
				}
				else
				{
					sw.WriteLine(i.ToString()+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
						rdr["Invoice_No"].ToString()+"\t"+
						rdr["Cust_Type"].ToString()+"\t"+
						rdr["Cust_Name"].ToString()+"\t"+
						rdr["Prod_Code"].ToString()+"\t"+
						rdr["ProdName"].ToString()+"\t"+
						rdr["Pack_type"].ToString()+"\t"+
						rdr["qty"].ToString()+"\t"+
						rdr["ltr"].ToString()+"\t"+
						Discount(rdr["Prod_id"].ToString())+"\t"+
						DiscAmt(rdr["Prod_id"].ToString(),rdr["Qty"].ToString())
						);
					i++;
				}
			}
			if(droptype.SelectedIndex==0)
			{
				sw.WriteLine("  Total\t\t\t\t"+TotalRo.ToString()+"\t"+TotalBazar.ToString()+"\t"+TotalFleet.ToString()+"\t"+TotalOE.ToString()+"\t"+TotalOthers.ToString()+"\t\t\t\t\t\t"+TotalAmount.ToString());
			}
			else
			{
				sw.WriteLine("\tTotal\t\t\t\t\t\t\t\t\t\t"+TotClaim.ToString());
			}
			sw.Close();
		}
	}
}