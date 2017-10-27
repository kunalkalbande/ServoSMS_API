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
	public partial class VATRDReport : System.Web.UI.Page
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
		public double[] TotalSum1 = null;
		public string[] DateTo = null;
		public static int count=0;
		public static string sql="";
		public static int View = 0;
		string UID;



		public static double[] GrantTotal = null;
		public int i=1;

		public double TotGAmount = 0;
		public double TotVATAmount = 0;
		public double TotNetAmount = 0;

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
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				try
				{
					ArrayList TotalSum = new ArrayList();
					ArrayList TotalSum1 = new ArrayList();
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
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Class:PetrolPumpClass.cs,Method: page_load, userid  "+UID);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
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
			string[] ds1 = s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
			ds1[0]="31";
			return ds1[1] + "/" + ds1[0] + "/" + ds1[2];	
		}
		
		/*public string GetSale(string Cust_ID)
		{
			string sale="";
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				
				//string sql="select count(prod_id) Total from sales_master sm,sales_details sd where sm.invoice_No=sd.invoice_No and sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[j].ToString()+"' group by sm.cust_id";
				string sql="select (Grand_Total-(Grand_Total*foediscount)/100) GAmount from sales_master sm where sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+txtDateFrom.Text.ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+txtDateTo.Text.ToString()+"'";
				rdr = obj.GetRecordSet(sql);
				while(rdr.Read())
				{
					//sale=ToString .p(Math.Round(double.Parse(rdr["GAmount"].ToString())));
				}
				rdr.Close();
				return sale.ToString(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalesReport.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Wise Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}*/

		/// <summary>
		/// this is used to view the report.
		/// </summary>
		protected void btnview1_Click(object sender, System.EventArgs e)
		{
			try
			{
				View=1;
				Bindthedata();
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:btnView, userid  "+UID);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+UID );
			}
		}
		
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
				//24.10.2013 sql="select Cust_Name,City,Tin_No,round((sum(Grand_Total)-sum(Grand_Total)*MRPDisc/100),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,MRPDisc order by Cust_Name,City,Tin_No,MRPDisc";
				sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
			else if(DropSearchBy.SelectedIndex==1)
			{
				if(DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
			}
			else if(DropSearchBy.SelectedIndex==2)
			{
				if(DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
			}
			else if(DropSearchBy.SelectedIndex==3)
			{
				if(DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and state='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
			}
			else if(DropSearchBy.SelectedIndex==4)
			{
				if(DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else
				{
					string cust_name="";
					cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				}
			}
			else if(DropSearchBy.SelectedIndex==5)
			{
				if(DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and city='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
			}
			else if(DropSearchBy.SelectedIndex==6)
			{
				if(DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
			}

            sqlcon.Open();
            SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_SaleBook");
			DataTable dtcustomer=ds.Tables["vw_SaleBook"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			DataGrid.DataSource=dv;
			if(dv.Count!=0)
			{
				DataGrid.DataBind();
				DataGrid.Visible=true;
			}
			else
			{
				DataGrid.Visible=false;
				MessageBox.Show(" Data Not Available ");
			}
            sqlcon.Close();
            sqlcon.Dispose();
		}

		double Tot_Sale_Disc=0;
		public double Total=0;
		public string GetSaleDiscount(string sale,string Invoice_No)
		{
			Tot_Sale_Disc=0;
			double Disc=0,Cash_Disc=0,Foe_Disc=0,FoeDisc=0,SchDisc=0,G_Total=0;
			string Cash_Disc_Type="",Foe_Disc_Type="";
			InventoryClass obj = new InventoryClass();
			string sql="Select Discount,Discount_Type,Cash_Discount, Cash_Disc_Type,schdiscount,foediscount,foediscounttype,foediscountrs,Grand_total from sales_master where cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString())+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString())+"' and cust_id= '"+Invoice_No+"'";
			SqlDataReader rdr = obj.GetRecordSet(sql);
			while(rdr.Read())
			{
				if(rdr["Discount"].ToString()!="")
					Disc+=double.Parse(rdr["Discount"].ToString());

				if(rdr["Cash_Discount"].ToString()!="")
					Cash_Disc+=double.Parse(rdr["Cash_Discount"].ToString());

				if(rdr["Cash_Disc_Type"].ToString()!="")
					Cash_Disc_Type=rdr["Cash_Disc_Type"].ToString();

				if(rdr["Grand_total"].ToString()!="")
					G_Total=double.Parse(rdr["Grand_total"].ToString());

				if(rdr["schdiscount"].ToString()!="")
					SchDisc+=double.Parse(rdr["schdiscount"].ToString());

				if(rdr["foediscountrs"].ToString()!="")
					FoeDisc+=double.Parse(rdr["foediscountrs"].ToString());

				if(Cash_Disc_Type=="Per")
				{
					Cash_Disc+=Math.Round((G_Total*Cash_Disc)/100,2);
				}
			}
			rdr.Close();
			
			double Scheme=0,Foe=0,SPDisc=0,Tot_Qty=0,Qty=0,Amount=0;
			string Scheme_Type="",Foe_Type="",SPDisc_Type="";
			sql="Select Amount,Scheme1,foe,SchType,FoeType,SPDisctype,SPDisc,Total_qty,qty from sales_details sd,Products p where sd.Prod_id=p.prod_id and invoice_no like '%"+Invoice_No+"'";
			rdr = obj.GetRecordSet(sql);
			while(rdr.Read())
			{
				if(rdr["Scheme1"].ToString()!="")
					Scheme=double.Parse(rdr["Scheme1"].ToString());

				if(rdr["SchType"].ToString()!="")
					Scheme_Type=rdr["SchType"].ToString();

				if(rdr["foe"].ToString()!="")
					Foe=double.Parse(rdr["foe"].ToString());

				if(rdr["FoeType"].ToString()!="")
					Foe_Type=rdr["FoeType"].ToString();

				if(rdr["SPDisc"].ToString()!="")
					SPDisc=double.Parse(rdr["SPDisc"].ToString());

				if(rdr["SPDisctype"].ToString()!="")
					SPDisc_Type=rdr["SPDisctype"].ToString();

				if(rdr["Qty"].ToString()!="")
					Qty=double.Parse(rdr["Qty"].ToString());

				if(rdr["Total_qty"].ToString()!="")
					Tot_Qty=double.Parse(rdr["Total_qty"].ToString());

				if(rdr["Amount"].ToString()!="")
					Amount=double.Parse(rdr["Amount"].ToString());

				if(Scheme_Type=="Rs")
				{
					Scheme=Math.Round(Tot_Qty*Qty*Scheme);
				}
				else
				{
					Scheme=Math.Round((Amount*Scheme)/100);
				}
				if(Foe_Type=="Rs")
				{
					Foe=Math.Round(Tot_Qty*Qty*Foe);
				}
				else
				{
					Foe=Math.Round((Amount*Foe)/100);
				}
			}
			rdr.Close();

			Tot_Sale_Disc+=Disc+SchDisc+FoeDisc+Cash_Disc+Scheme+Foe;
			
			Tot_Sale_Disc=double.Parse(sale.ToString())-Tot_Sale_Disc;

			Total+=Tot_Sale_Disc;
			
			return Tot_Sale_Disc.ToString();
		}

		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					//TotalGrossAmount(e.Item.Cells[4].Text);
					TotalVATAmount(e.Item.Cells[6].Text); 
					TotalNetAmount(e.Item.Cells[7].Text);
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					//e.Item.Cells[4].Text = TotGAmount.ToString();
					e.Item.Cells[6].Text = TotVATAmount.ToString();
					e.Item.Cells[7].Text = TotNetAmount.ToString();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ UID );
			}
		}

		protected void TotalGrossAmount(string _GAmount)
		{
			TotGAmount  += double.Parse(_GAmount); 
		}
		protected void TotalVATAmount(string _VAT)
		{
			TotVATAmount  += double.Parse(_VAT); 
		}
		protected void TotalNetAmount(string _NET)
		{
			TotNetAmount  += double.Parse(_NET); 
		}

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
				CreateLogFiles.ErrorLog("Form:SalesBookReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+UID);
			}
		}


		public void Totalforprint(string t24t,string tot)//,string oe1,string fleet1,string ibp1,string total1)
		{
			
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		string strorderby="";
		public void ConvertToExcel()
		{
			
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\VATReturnDebtorsReport.xls";
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
				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
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
				
				if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				else if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value=="All")
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					else
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					else
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					else
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and state='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					else
					{
						cust_name="";
						cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					}
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value=="All")
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					else
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and city='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value=="All")
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
					else
						sql="select c.cust_id,Cust_Name,City,Tin_No,round(sum(Grand_Total),0) Sale,round(sum(Vat_Amount),0) VAT,round(sum(Net_Amount),0) NET,(select distinct cast(VAT_Rate as varchar)+' %' from organisation) VAT_Rate from Customer c,sales_master sm where c.cust_id=sm.cust_id and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' group by Cust_Name,City,Tin_No,c.cust_id order by Cust_Name,City,Tin_No,c.cust_id";
				}

				InventoryClass obj=new InventoryClass();
				rdr=obj.GetRecordSet(sql);
				if(rdr.HasRows)
				{
					flag=1;
					sw.WriteLine("PARTY NAME\tPLACE\tTIN NO\tSALE\tVAT RATE\tVAT AMOUNT\tNET AMOUNT");
				}
				if(flag==1)
				{
					while(rdr.Read())
					{
						sw.WriteLine(rdr["Cust_Name"].ToString()+"\t"+
						rdr["City"].ToString()+"\t"+
						rdr["Tin_No"].ToString()+"\t"+
						//rdr["Sale"].ToString()+"\t"+
						GetSaleDiscount(rdr["Sale"].ToString(),rdr["Cust_id"].ToString())+"\t"+
						rdr["VAT_Rate"].ToString()+"\t"+
						rdr["VAT"].ToString()+"\t"+
						rdr["NET"].ToString());
						
						int k=-1;
						//TotGAmount+=double.Parse(rdr["Sale"].ToString());
						TotVATAmount+=double.Parse(rdr["VAT"].ToString());
						TotNetAmount+=double.Parse(rdr["NET"].ToString());
					}
					sw.WriteLine("Total\t\t\t"+Total.ToString()+"\t\t"+TotVATAmount.ToString()+"\t"+TotNetAmount);
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
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsrintServices\ReportView\CustomerSalesReport.txt";
			StreamWriter sw = new StreamWriter(path);		
			try
			{
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:btnprint, userid  "+UID );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:btnprint,   EXCEPTION "+ex.Message+"  userid  "+UID );
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
				IPEndPoint remoteEP = new IPEndPoint(ipAddress,65000);

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\EDMS\\Sysitem\\EDMSPrintServices\\ReportView\\CustomerSalesReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:print");
                
				} 
				catch (ArgumentNullException ane) 
				{
					//Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:print"+ ane.Message+". User: "+UID);
				} 
				catch (SocketException se) 
				{
					///Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:print"+ se.Message+". User: "+UID);
				} 
				catch (Exception es) 
				{
					//Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:print"+ es.Message+". User: "+UID);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Method:print"+ ex.Message+". User: "+UID);
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
					CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Sales Report Convert Into Excel Format, userid  "+UID);
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
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}
		
		/// <summary>
		/// This method is used to return the month name with year in given date.
		/// </summary>
		public string GetMonthName(string mon)
		{
			if(mon.IndexOf("/")>0 || mon.IndexOf("-")>0)
			{
				string[] month=mon.IndexOf("/")>0? mon.Split(new char[] {'/'},mon.Length): mon.Split(new char[] { '-' }, mon.Length);
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
				TotalSum1 = new double[(count+1)*2];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
				TotalSum = new double[count*2];
				TotalSum1 = new double[(count+1)*2];
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

		public void getDateNew(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count=To2-From2;
				DateFrom = new string[count+1];
				DateTo = new string[count+1];
				TotalSum = new double[(count+1)];
				TotalSum1 = new double[(count+1)];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
				TotalSum = new double[(count+1)];
				TotalSum1 = new double[(count+1)];
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
				string strGroup="",strSubGroup="";       
				strName = "select distinct c.cust_name,c.city from customer c order by c.cust_name,c.city";
				strDistrict = "select distinct state from customer c order by state";
				strPlace = "select distinct c.city from customer c order by c.city";
				strSSR = "select * from employee where Designation='Sales Representative' and status=1 order by emp_name";
				strGroup="select distinct Group_Name from customertype";            
				strSubGroup="select distinct Sub_Group_Name from customertype";		
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
				CreateLogFiles.ErrorLog("Form:VATRDReport.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}

		private void dropType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			View=0;
			
			
		}
	}
}