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
	/// Summary description for PurchaseListIOCL.
	/// </summary>
	public partial class PurchaseListIOCL : System.Web.UI.Page
	{
		public System.Globalization.NumberFormatInfo  nfi = new System.Globalization.CultureInfo("en-US",false).NumberFormat;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
		public double grand_total = 0;
		public double vat_total = 0;
		public double net_total = 0;
		public double cash_discount = 0;
		public double other_discount = 0;
		public double ebird = 0;
		public double trade = 0;
		public double Qty = 0;
		public double foc = 0;
		public double TotDis = 0;
		public double DiscPer = 0;
		public double DiscPer_Add = 0;
		
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
				CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			try
			{
				//	uid=(Session["User_Name"].ToString());
				if(! IsPostBack)
				{
					PurchaseGrid.Visible=false;
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="35";
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
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
			}
		}

		/// <summary>
		/// This method is used to checks the validity of form input fields.
		/// </summary>
		public bool checkValidity()
		{
			string ErrorMessage = "";
			bool flag = true;
			
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
		/// This method is used to return the date in MM/dd/YYYY format
		/// </summary>
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
		/// This increment the grand total by the passing value.
		/// </summary>
		protected void GrandTotal(double _grandtotal)
		{
			grand_total += _grandtotal; 
		}

		/// <summary>
		/// This increment the vat total by passing value.
		/// </summary>
		protected void VATTotal(double _vattotal)
		{
			vat_total  += _vattotal; 
		}
		
		/// <summary>
		/// This increment the net total by passing value.
		/// </summary>
		protected void NetTotal(double _nettotal)
		{
			net_total  += _nettotal; 
		}

		/// <summary>
		/// This invrement the cash discount by passing value.
		/// </summary>
		protected void CashDiscount(double _cashdiscount)
		{
			cash_discount  += _cashdiscount; 
		}

		/// <summary>
		/// this increment the other discount by passing value.
		/// </summary>
		protected void OtherDiscount(double _otherdiscount)
		{
			other_discount  += _otherdiscount; 
		}

		/// <summary>
		/// This increment the FOC discount by passing value.
		/// </summary>
		protected void focDiscount(double _focdiscount)
		{
			foc  += _focdiscount; 
		}

		/// <summary>
		/// this increment the trade discount by passing value.
		protected void tradeDiscount(double _tradediscount)
		{
			trade  += _tradediscount; 
		}
		
		/// <summary>
		/// this increment the ebird discount by passing value.
		/// </summary>
		protected void ebirdDiscount(double _ebirddiscount)
		{
			ebird  += _ebirddiscount; 
		}

		protected void DiscountPer(double _DiscountPer)
		{
			DiscPer  += _DiscountPer; 
		}

		protected void DiscountPer_Add(double _DiscountPer_Add)
		{
			DiscPer_Add  += _DiscountPer_Add; 
		}
		
		/// <summary>
		/// This method is used to calculate the qty
		/// </summary>
		protected void QtyLtr(double _Qty)
		{
			Qty  += _Qty; 
		}
		
		/// <summary>
		/// This method is used to calculate the total discount.
		/// </summary>
		protected void TotalDisc(double _totaldis)
		{
			TotDis  += _totaldis; 
		}

		/// <summary>
		/// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ))
			{
				GrandTotal(Double.Parse(e.Item.Cells[3].Text));
				QtyLtr(Double.Parse(e.Item.Cells[2].Text));
				tradeDiscount(Double.Parse(e.Item.Cells[4].Text));
				ebirdDiscount(Double.Parse(e.Item.Cells[6].Text));
				NetTotal(Double.Parse(e.Item.Cells[3].Text));
				DiscountPer(Double.Parse(e.Item.Cells[8].Text));
				DiscountPer_Add(Double.Parse(e.Item.Cells[9].Text)); //Add by vikas 08.07.09
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				e.Item.Cells[3].Text =grand_total.ToString();   
				Cache["gt"]=grand_total.ToString(); 
				e.Item.Cells[2].Text =Qty.ToString();
				Cache["Qty"]=Qty.ToString();
				e.Item.Cells[3].Text = net_total.ToString();
				Cache["nt"]=net_total.ToString();
				e.Item.Cells[6].Text =ebird.ToString();
				Cache["eb"]=ebird.ToString();
				e.Item.Cells[4].Text =trade.ToString();
				Cache["tr"]=trade.ToString();
				e.Item.Cells[10].Text=TotDis.ToString();
				Cache["DiscPer"]=DiscPer.ToString();
				e.Item.Cells[8].Text=DiscPer.ToString();

				Cache["DiscPer_Add"]=DiscPer_Add.ToString();        //Add by vikas 08.07.09
				e.Item.Cells[9].Text=DiscPer_Add.ToString();        //Add by vikas 08.07.09
				grand_total = 0;
				cash_discount = 0;
				other_discount = 0;
				net_total = 0;
				ebird=0;
				trade=0;
				foc=0;
				Qty=0;
			}
		
		}

		/// <summary>
		/// This method is used to calculate the vat amount.
		/// </summary>
		public double CalVat(double vat)
		{
			vat_total+=Math.Round(vat);
			Cache["vt"]=vat_total.ToString();
			return System.Convert.ToDouble(Math.Round(vat));
		}

		/// <summary>
		/// This is used to make sorting of datagrid onclicking of datagridheader.
		/// </summary>
		string strorderby2="";
		public void sortcommand_click2(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["order"].Equals("ASC"))
					{
						strorderby2=e.SortExpression.ToString()+" DESC";
						Session["order"]="DESC";
					}
					else
					{
						strorderby2=e.SortExpression.ToString()+" ASC";
						Session["order"]="ASC";
					}
				}
				else
				{
					strorderby2=e.SortExpression.ToString()+" ASC";
					Session["order"]="ASC";
				}
				Session["column"]=e.SortExpression.ToString();
				Bindthedata2();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:sortcommand_click2"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata2()
		{
			//			if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
			//			{
			int p=0;
			SqlConnection sqlcon2=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string sqlstr1=System.Convert.ToString(Cache["str1"]);
			DataSet ds1=new DataSet();
			DataTable dtcustomer1;
			DataView dv1=new DataView();
			if(sqlstr1!="")
			{
				SqlDataAdapter dal=new SqlDataAdapter(sqlstr1,sqlcon2);
				dal.Fill(ds1,"purchase_master");
				dtcustomer1=ds1.Tables["purchase_master"];
				dv1=new DataView(dtcustomer1);
				dv1.Sort=strorderby2;
				Cache["strorderby2"]=strorderby2;
				PurchaseGrid.DataSource=dv1;
				//PurchaseGrid.Visible=true;
				//lblPurchaseHeading.Visible=true;
			}
			if(dv1.Count!=0)
			{
				PurchaseGrid.DataBind();
				PurchaseGrid.Visible=true;
				//lblPurchaseHeading.Visible=true;
			}
			else
			{
				PurchaseGrid.Visible=false;
				//lblPurchaseHeading.Visible=false;
				p++;

			}
			//sqlcon.Dispose();
			if(p>0 )
			{
				MessageBox.Show("Purchase Data not available");
			}
			sqlcon2.Dispose();
			//			}
			//			else
			//			{
			//				lblPurchaseHeading.Visible = false; 
			//				PurchaseGrid.Visible=false;
			//			}
			
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
		/// this method is used to view the report.
		/// </summary>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			try
			{
				//this function is used to check the validity of form.
				if(!checkValidity())
				{
					return;
				}
				//string str="";
				string str1="";

				// 07.04.09 Add By Vikas Sharma str1="select invoice_no invoice_no, invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				// 08.07.09 str1="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				//str1="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type,fixed_discount,Promo_Scheme from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";

				str1="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type,fixed_discount,(case when Promo_Scheme = '' then '0' else Promo_Scheme end) Promo_Scheme from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				
				Cache["str1"]=str1;
				strorderby2="vndr_invoice_no ASC";
				Session["Column"]="vndr_invoice_no";
				Session["order"]="ASC";
				Bindthedata2();
					
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:cmdrpt_Click  EXCEPTION  "+ex.Message+"  "+ uid );

			}
		}

		
		
		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		public string discountfocDisc1(string invoice)
		{
			
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				
				//**Mahesh fixedDisc=System.Convert.ToDouble(rdr.GetValue(26).ToString());
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());

				/*if(rdr.GetValue(11).ToString()!=null && rdr.GetValue(11).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(11).ToString());*/

				//**Mahesh if(rdr.GetValue(27).ToString().Equals("Per"))
				//**Mahesh	fixedDisc=grandtot*fixedDisc/100; 

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					Disc=grandtot*Disc/100 ;
				}	
				
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				double ETFOC = (focDisc*2)/100;
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					etax=grandtot*etax/100 ;
				}
				
				CashDisc=System.Convert.ToDouble(GenUtil.strNumericFormat(rdr.GetValue(15).ToString()));
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc+ETFOC);
					//GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
			
			//			Cache["focDisc"]=focDisc;
			//			focDisctotal+=focDisc;
			//			Cache["focDisctotal"]=Math.Round(focDisctotal);
			
			//Cache["fixed"]=fixedDisc;
			//fixedDisctotal+=fixedDisc;
			//Cache["fixedDisctotal"]=Math.Round(fixedDisctotal);
			
			//Cache["Disc"]=Disc;
			//Disctotal+=Disc;
			//Cache["Disctotal"]=Math.Round(Disctotal);	
			
			//CashDisctotal+=Math.Round(CashDisc);
			//Cache["CashDisctotal"]=Math.Round(CashDisctotal);
			//return System.Convert.ToDouble(Math.Round(CashDisc));
			return System.Convert.ToString(Math.Round(CashDisc));
			//else if(discounttype.Equals("cash"))
			//	return System.Convert.ToDouble(Math.Round(CashDisc,2));
			//else
			//return 0 ;
		}

		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		double fixedDisctotal=0;
		double focDisctotal=0;
		double CashDisctotal=0;
		double Disctotal=0;
		double Fix_Dis_tot=0;
		//public double discountfocDisc(string invoice)
		public string discountfocDisc(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				
				//**Mahesh fixedDisc=System.Convert.ToDouble(rdr.GetValue(26).ToString());
				//Coment BY vikas 19.4.2013 fixedDisc=System.Convert.ToDouble(rdr.GetValue(27).ToString());
				
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());

				//**Mahesh if(rdr.GetValue(27).ToString().Equals("Per"))
				//**Mahesh	fixedDisc=grandtot*fixedDisc/100; 

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					Disc=grandtot*Disc/100 ;
				}	
				
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				double ETFOC = (focDisc*2)/100;
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					etax=grandtot*etax/100 ;
				}
				
				//CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				CashDisc=System.Convert.ToDouble(GenUtil.strNumericFormat(rdr.GetValue(15).ToString()));
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc+ETFOC);
					//GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
			
			//			Cache["focDisc"]=focDisc;
			//			focDisctotal+=focDisc;
			//			Cache["focDisctotal"]=Math.Round(focDisctotal);
			
			//Cache["fixed"]=fixedDisc;
			fixedDisctotal+=fixedDisc;
			Cache["fixedDisctotal"]=Math.Round(fixedDisctotal);
			
			//Cache["Disc"]=Disc;
			//Disctotal+=Disc;
			//Cache["Disctotal"]=Math.Round(Disctotal);	
			
			CashDisctotal+=Math.Round(CashDisc);
			Cache["CashDisctotal"]=Math.Round(CashDisctotal);
			//return System.Convert.ToDouble(Math.Round(CashDisc));
			return System.Convert.ToString(Math.Round(CashDisc));
			//else if(discounttype.Equals("cash"))
			//	return System.Convert.ToDouble(Math.Round(CashDisc,2));
			//else
			//return 0 ;
		}
		//*****************
		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		protected string discountfocDiscA(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				
				//fixedDisc=System.Convert.ToDouble(rdr.GetValue(26).ToString());
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				//if(rdr.GetValue(27).ToString().Equals("Per"))
				//	fixedDisc=grandtot*fixedDisc/100; 

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					Disc=grandtot*Disc/100 ;
				}	
				
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					etax=grandtot*etax/100 ;
				}
				
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
			
			//			Cache["focDisc"]=focDisc;
			//			focDisctotal+=focDisc;
			//			Cache["focDisctotal"]=Math.Round(focDisctotal,2);
			
			//			Cache["fixedA"]=fixedDisc;
			//			fixedDisctotal+=fixedDisc;
			//			Cache["fixedDisctotal"]=Math.Round(fixedDisctotal,2);
			//			
			//			Cache["Disc"]=Disc;
			//			Disctotal+=Disc;
			//			Cache["Disctotal"]=Math.Round(Disctotal,2);	
			//			
			//			CashDisctotal+=CashDisc;
			//			Cache["CashDisctotal"]=Math.Round(CashDisctotal,2);
			//return System.Convert.ToDouble(Math.Round(fixedDisc));
			return System.Convert.ToString(Math.Round(fixedDisc));
			
		}

		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		protected string discountfocDiscA1(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					Disc=grandtot*Disc/100 ;
				}	
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					etax=grandtot*etax/100 ;
				}
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
			}
			rdr.Close();
			return System.Convert.ToString(Math.Round(fixedDisc));
		}

		public double foc_qty_tot=0;
		protected string foc_qty(string invoice)
		{
			SqlDataReader rdr=null;
			string prod_id="";
			string qty="";
			string[] prod_id_ar=new string[20] ; //add by vikas 08.07.09 
			string[] qty_ar=new string[20] ;     //add by vikas 08.07.09 
			string pack_type="";
			double tot_ltr=0;
			double foc_ltr=0;
			double foc_tot=0;         //add by vikas 08.07.09 
			int flage=0,i=0;
			string sql="select prod_id,qty from purchase_details where invoice_no='"+invoice+"' and foc=1";
			dbobj.SelectQuery(sql,ref rdr);
			//07.07.09 if(rdr.Read())
			while(rdr.Read())
			{
				//07.07.09 prod_id=rdr.GetValue(0).ToString();
				//07.07.09 qty=rdr.GetValue(1).ToString();
				prod_id_ar[i]=rdr.GetValue(0).ToString();
				qty_ar[i]=rdr.GetValue(1).ToString();
				i++;
				flage=1;
			}
			rdr.Close();
			if(flage==1)
			{
				foc_tot=0;

				for(int j=0;j<i;j++)    //add by vikas 07.07.09 
				{
					//coment by vikas 07.07.09 sql="select prod_name,pack_type from products where prod_id="+prod_id.ToString();
					sql="select prod_name,pack_type from products where prod_id="+prod_id_ar[j].ToString();
					dbobj.SelectQuery(sql,ref rdr);
					if(rdr.Read())
					{
						pack_type=rdr.GetValue(1).ToString();
					}
					rdr.Close();
			
					if(pack_type!="" && pack_type !=null)
					{
						string[] pack=pack_type.Split(new char[] {'X'});
						tot_ltr=Convert.ToDouble(pack[0].ToString())*Convert.ToDouble(pack[1].ToString());
						//07.07.09 foc_ltr=Convert.ToDouble(qty.ToString())*tot_ltr;
						foc_ltr=Convert.ToDouble(qty_ar[j].ToString())*tot_ltr;
						foc_qty_tot+=foc_ltr;
						foc_tot+=foc_ltr;
						Cache["foc_qty_tot"]=Math.Round(foc_qty_tot);
					}
				}
			}
			//08.07.09 return System.Convert.ToString(Math.Round(foc_ltr));
			return System.Convert.ToString(Math.Round(foc_tot));
		}

		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		protected string discountfocDiscB(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				
				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					Disc=grandtot*Disc/100 ;
				}	
				
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					etax=grandtot*etax/100 ;
				}
				
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
						
			focDisctotal+=focDisc;
			Cache["focDisctotal"]=Math.Round(focDisctotal);
						
			return System.Convert.ToString(Math.Round(focDisc));
		}

		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		protected string discountfocDiscB1(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());

				
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					Disc=grandtot*Disc/100 ;
				}	
				
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					etax=grandtot*etax/100 ;
				}
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
			}
			rdr.Close();
			return System.Convert.ToString(Math.Round(focDisc));
		}

		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		protected string discountfocDiscC(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					etax=grandtot*etax/100 ;
				}
				
				
				//fixedDisc=System.Convert.ToDouble(rdr.GetValue(26).ToString());
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				
				//if(rdr.GetValue(27).ToString().Equals("Per"))
				//	fixedDisc=grandtot*fixedDisc/100; 

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					Disc=grandtot*Disc/100 ;
				}	
				
				
				
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
			
			//			Cache["focDisc"]=focDisc;
			//			focDisctotal+=focDisc;
			//			Cache["focDisctotal"]=Math.Round(focDisctotal,2);
			
			//			Cache["fixedA"]=fixedDisc;
			//			fixedDisctotal+=fixedDisc;
			//			Cache["fixedDisctotal"]=Math.Round(fixedDisctotal,2);
			//			
			//			Cache["Disc"]=Disc;
			Disctotal+=Disc;
			Cache["Disctotal"]=Math.Round(Disctotal);	
			//			
			//			CashDisctotal+=CashDisc;
			//			Cache["CashDisctotal"]=Math.Round(CashDisctotal,2);
			//return System.Convert.ToDouble(Math.Round(Disc));
			return System.Convert.ToString(Math.Round(Disc));
		}
		
		double fixedDisc=0;
		protected string discountfixdDisc(string invoice)
		{
			//double fixedDisc=0;
			fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					etax=grandtot*etax/100 ;
				}
			
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				
				else
					fixedDisc=0;

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					Disc=grandtot*Disc/100 ;
				}	
				
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
			}
			rdr.Close();
			Fix_Dis_tot+=fixedDisc;
			Cache["fixd_Disctotal"]=Math.Round(Fix_Dis_tot);	
			return System.Convert.ToString(Math.Round(fixedDisc));

		}

		/// <summary>
		/// This method is used to return the calculated discount
		/// </summary>
		protected string discountfocDiscC1(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double etax=System.Convert.ToDouble(rdr.GetValue(22).ToString());
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				if(rdr.GetValue(23).ToString().Equals("Per"))
				{
					etax=grandtot*etax/100 ;
				}
				if(rdr.GetValue(30).ToString()!=null && rdr.GetValue(30).ToString()!="")
					fixedDisc=System.Convert.ToDouble(rdr.GetValue(30).ToString());
				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				Disc=System.Convert.ToDouble(rdr.GetValue(8).ToString());
				if(rdr.GetValue(9).ToString().Equals("Per"))
				{
					Disc=grandtot*Disc/100 ;
				}	
				CashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double GT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc);
					CashDisc=GT*CashDisc/100;
				}
			}
			rdr.Close();
			return System.Convert.ToString(Math.Round(Disc));
		}
		//******************


		/// <summary>
		/// Its remove the time from data field & only returns the date.
		/// </summary>
		public string trimDate(string strDate)
		{
			int pos = strDate.IndexOf(" ");
				
			if(pos != -1)
			{
				strDate = strDate.Substring(0,pos);
			}
			else
			{
				strDate = "";					
			}
			return strDate;
		}

		private void DropReportCategory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		//*************
		/// <summary>
		/// Method to prepare the report file.
		/// </summary>
		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "",CustName="";
				string strDate="";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PurchaseioclReport.txt";
				StreamWriter sw = new StreamWriter(path);
			
				//**Mahesh sql="select vndr_invoice_no invoice_no, vndr_invoice_date invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				//sql="select invoice_no invoice_no, vndr_invoice_date invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				// Comment by Vikas Sharma Date On 07.04.09 sql="select invoice_no invoice_no, invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				//coment by vikas 08.07.09 sql="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				sql="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type,fixed_discount,fixed_Disc_Amount from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";

				sql=sql+" order by "+Cache["strorderby2"];
				dbobj.SelectQuery(sql,ref rdr);
				sw.Write((char)27);			//added by vishnu
				sw.Write((char)67);			//added by vishnu
				sw.Write((char)0);			//added by vishnu
				sw.Write((char)12);			//added by vishnu
				sw.Write((char)27);			//added by vishnu
				sw.Write((char)78);			//added by vishnu
				sw.Write((char)5);			//added by vishnu
				sw.Write((char)27);			//added by vishnu
				sw.Write((char)15);
				string des="---------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("PURCHASE IOCL REPORT From "+txtDateFrom.Text.ToString()+" To "+txtDateTo.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));

				/*******Add by vikas 5.12.12 ******************/
							 
				sw.WriteLine("+---------+----------+--------+------------+----------+----------+----------+----------+----------+--------+-------+----------+----------+-------+");
				sw.WriteLine("| Invoice | Inv.Date | Qty In |   Total    |  Trade   |   Cash   |  Ebird   |   FOC    | Discount |  Add   |OldRate|  Fixed   |  Total   |  FOC  |");
				sw.WriteLine("|   No    |          |   Ltr  |   Amount   | Discount | Discount | Discount | Discount |   (%)    |Discount| Disc. | Discount | Discount |  Qty. |");
				sw.WriteLine("+---------+----------+--------+------------+----------+----------+----------+----------+----------+--------+-------+----------+----------+-------+");
				//             123456789 1234567890 12345678 123456789012 1234567890 1234567890 1234567890 1234567890 1234567890 12345678 1234567 1234567890 1234567890 1234567
				/*******end*************************************/

				if(rdr.HasRows)
				{
					// info : to set the string format.
					//info = "|{0,-9:S}|{1,-10:S}|{2,-17:S}|{3,12:S}|{4,9:S}|{5,10:S}|{6,7:S}|{7,8:S}|{8,8:S}|{9,8:S}|{10,8:F}|{11,9:S}|{12,9:S}|";
					//08.07.09 info = "|{0,-9:S}|{1,-10:S}|{2,8:S}|{3,12:S}|{4,10:S}|{5,10:S}|{6,10:S}|{7,10:S}|{8,11:S}|{9,7:S}|{10,10:S}|{11,17:F}|";
					//Add by vikas 5.12.12 info = "|{0,-9:S}|{1,-10:S}|{2,8:S}|{3,12:S}|{4,10:S}|{5,10:S}|{6,10:S}|{7,10:S}|{8,10:S}|{9,8:S}|{10,7:S}|{11,10:F}|{12,7:F}|";
					info = "|{0,-9:S}|{1,-10:S}|{2,8:S}|{3,12:S}|{4,10:S}|{5,10:S}|{6,10:S}|{7,10:S}|{8,10:S}|{9,8:S}|{10,7:S}|{11,10:F}|{12,10:F}|{13,7:F}|";

					while(rdr.Read())
					{					
						//Comment By Vikas Sharma 07.04.09 strDate = rdr["invoice_date"].ToString().Trim();
						strDate = rdr["vndr_invoice_date"].ToString().Trim();
						int pos = strDate.IndexOf(" ");
				
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}
						//19.4.2013 double total=double.Parse(discountfocDisc(rdr["invoice_no"].ToString()))+double.Parse(discountfocDisc1(rdr["invoice_no"].ToString()))+double.Parse(rdr["Trade_discount"].ToString())+double.Parse(discountfocDisc1(rdr["invoice_no"].ToString()))+double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["fixed_discount_type"].ToString());
						//double total=double.Parse(discountfocDisc(rdr["invoice_no"].ToString()))+double.Parse(discountfocDisc1(rdr["invoice_no"].ToString()))+double.Parse(rdr["Trade_discount"].ToString())+double.Parse(discountfocDisc1(rdr["invoice_no"].ToString()))+double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["fixed_Disc_Amount"].ToString());
						//double total=double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["trade_discount"].ToString())+double.Parse(discountfocDisc(rdr["invoice_no"].ToString()))+double.Parse(rdr["fixed_Disc_Amount"].ToString());
						double fixed_Disc_Amount=0;
						if(rdr["fixed_Disc_Amount"].ToString()!=null && rdr["fixed_Disc_Amount"].ToString()!="")
							fixed_Disc_Amount=double.Parse(rdr["fixed_Disc_Amount"].ToString());

						double total=double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["trade_discount"].ToString())+double.Parse(discountfocDisc(rdr["invoice_no"].ToString()))+fixed_Disc_Amount+double.Parse(rdr["fixed_discount_type"].ToString())+double.Parse(rdr["fixed_discount"].ToString());

						CustName=rdr["Supp_Name"].ToString().Trim();
						if(CustName.StartsWith("Indian oil corporation"))
							CustName="CFA "+GenUtil.TrimLength(rdr["City"].ToString().Trim(),11);
						sw.WriteLine(info,rdr["vndr_invoice_no"].ToString().Trim(),
							GenUtil.str2DDMMYYYY(strDate),
							rdr["totalqtyltr"].ToString().Trim(),
							rdr["Net_Amount"].ToString().Trim(),
							rdr["Trade_discount"].ToString().Trim(),
							discountfocDisc(rdr["invoice_no"].ToString().Trim()).ToString(),
							rdr["ebird_discount"].ToString().Trim(),
							//GenUtil.TrimLength(rdr["City"].ToString().Trim(),11),
							//GenUtil.strNumericFormat(rdr["Grand_Total"].ToString().Trim()),
							discountfocDiscB(rdr["invoice_no"].ToString().Trim()).ToString(),
							rdr["Fixed_Discount_Type"].ToString().Trim(),
							rdr["Fixed_Discount"].ToString().Trim(),                         //Add by vikas 08.07.09
							discountfocDiscC(rdr["invoice_no"].ToString().Trim()).ToString(),
							discountfixdDisc(rdr["invoice_no"].ToString().Trim()).ToString(),
							//discountfocDiscA(rdr["invoice_no"].ToString().Trim()).ToString(),
							//System.Convert.ToString(CalVat(double.Parse(rdr["VAT_Amount"].ToString().Trim()))),
							total.ToString(),
							//coment by vikas 27.05.09 StringUtil.trimlength(CustName,17)
							foc_qty(rdr["invoice_no"].ToString())
							);
					}
				}
				//08.07.09 sw.WriteLine("+---------+----------+--------+------------+----------+----------+----------+----------+-----------+-------+----------+-----------------+");
				sw.WriteLine("+---------+----------+--------+------------+----------+----------+----------+----------+----------+--------+-------+----------+----------+-------+");
				//sw.WriteLine(info,"Total:","","","","",Cache["CashDisctotal"].ToString(),"","","","","","","");
				//coment by vikas 27.05.09 sw.WriteLine(info,"Total:","",Cache["Qty"].ToString(),Cache["nt"].ToString(),Cache["tr"],Cache["CashDisctotal"].ToString(),Cache["eb"],Cache["focDisctotal"].ToString(),Cache["DiscPer"].ToString(),Cache["Disctotal"].ToString(),Cache["TotDis"].ToString(),"");
				//coment by vikas 08.07.09 sw.WriteLine(info,"Total:","",Cache["Qty"].ToString(),Cache["nt"].ToString(),Cache["tr"],Cache["CashDisctotal"].ToString(),Cache["eb"],Cache["focDisctotal"].ToString(),Cache["DiscPer"].ToString(),Cache["Disctotal"].ToString(),Cache["TotDis"].ToString(),Cache["foc_qty_tot"]);
				sw.WriteLine(info,"Total:","",Cache["Qty"].ToString(),Cache["nt"].ToString(),Cache["tr"].ToString(),Cache["CashDisctotal"].ToString(),Cache["eb"].ToString(),Cache["focDisctotal"].ToString(),Cache["DiscPer"].ToString(),Cache["DiscPer_Add"].ToString(),Cache["Disctotal"].ToString(),Cache["fixd_Disctotal"].ToString(),Cache["TotDis"],Cache["foc_qty_tot"]);
				sw.WriteLine("+---------+----------+--------+------------+----------+----------+----------+----------+----------+--------+-------+----------+----------+-------+");
				dbobj.Dispose();
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\PurchaseListIOCL.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",CustName="",strDate="";
			//sql="select invoice_no invoice_no, vndr_invoice_date invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			// Comment By Vikas Sharma 07.04.09 sql="select invoice_no invoice_no, invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			// Comment By Vikas Sharma 08.07.09 sql="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			sql="select invoice_no invoice_no, vndr_invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,vndr_invoice_no,totalqtyltr,fixed_discount_type,fixed_discount,fixed_Disc_Amount from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			sql=sql+" order by "+Cache["strorderby2"];
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+txtDateTo.Text);
			sw.WriteLine();
			//coment by vikas 27.05.09 sw.WriteLine("Invoice No\tInvoice Date\tQty In Ltr\tInvoice Amount\tTrade Discount\tCash Discount\tEbird Discount\tFOC Discount\tDiscount(%)\tOldRate Discount\tTotal Discount\tVender Name");
			//coment by vikas 27.05.09 sw.WriteLine("Invoice No\tInvoice Date\tQty In Ltr\tInvoice Amount\tTrade Discount\tCash Discount\tEbird Discount\tFOC Discount\tDiscount(%)\tOldRate Discount\tTotal Discount\tFOC Quantity");
			//coment by vikas 6.12.12 sw.WriteLine("Invoice No\tInvoice Date\tQty In Ltr\tInvoice Amount\tTrade Discount\tCash Discount\tEbird Discount\tFOC Discount\tDiscount(%)\tAdd. Discount\tOldRate Discount\tTotal Discount\tFOC Quantity\tVender Name");
			sw.WriteLine("Invoice No\tInvoice Date\tQty In Ltr\tInvoice Amount\tTrade Discount\tCash Discount\tEbird Discount\tFOC Discount\tDiscount(%)\tAdd. Discount\tOldRate Discount\tFixed Discount\tTotal Discount\tFOC Quantity\tVender Name");
			while(rdr.Read())
			{
				//Comment By Vikas Sharma 07.04.09 strDate = rdr["invoice_date"].ToString().Trim();
				strDate = rdr["vndr_invoice_date"].ToString().Trim();
				int pos = strDate.IndexOf(" ");
				
				if(pos != -1)
				{
					strDate = strDate.Substring(0,pos);
				}
				else
				{
					strDate = "";					
				}
				CustName=rdr["Supp_Name"].ToString().Trim();
				//double total=double.Parse(discountfocDiscC1(rdr["invoice_no"].ToString()))+double.Parse(discountfocDiscB1(rdr["invoice_no"].ToString()))+double.Parse(rdr["Trade_discount"].ToString())+double.Parse(discountfocDisc1(rdr["invoice_no"].ToString()))+double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["fixed_discount_type"].ToString());
				//double total=double.Parse(GetTotal(rdr["trade_discount"].ToString(),discountfocDisc1(rdr["invoice_no"].ToString()),rdr["ebird_discount"].ToString(),discountfocDiscB1(rdr["invoice_no"].ToString()),discountfocDiscC1(rdr["invoice_no"].ToString()),rdr["fixed_discount_type"].ToString()));
				
				double fixed_Disc_Amount=0;
				if(rdr["fixed_Disc_Amount"].ToString()!=null && rdr["fixed_Disc_Amount"].ToString()!="")
					fixed_Disc_Amount=double.Parse(rdr["fixed_Disc_Amount"].ToString());

				double total=double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["trade_discount"].ToString())+double.Parse(discountfocDisc(rdr["invoice_no"].ToString()))+fixed_Disc_Amount+double.Parse(rdr["fixed_discount_type"].ToString())+double.Parse(rdr["fixed_discount"].ToString());

				//double total=double.Parse(rdr["ebird_discount"].ToString())+double.Parse(rdr["trade_discount"].ToString())+double.Parse(discountfocDisc(rdr["invoice_no"].ToString()))+double.Parse(rdr["fixed_Disc_Amount"].ToString())+double.Parse(rdr["fixed_discount_type"].ToString())+double.Parse(rdr["fixed_discount"].ToString());
				
				if(CustName.StartsWith("Indian oil corporation"))
					CustName="CFA "+GenUtil.TrimLength(rdr["City"].ToString().Trim(),11);
				sw.WriteLine(rdr["vndr_invoice_no"].ToString().Trim()+"\t"+
					GenUtil.str2DDMMYYYY(strDate)+"\t"+
					//***********
					rdr["totalqtyltr"].ToString().Trim()+"\t"+
					rdr["Net_Amount"].ToString().Trim()+"\t"+
					rdr["Trade_discount"].ToString().Trim()+"\t"+
					discountfocDisc(rdr["invoice_no"].ToString().Trim()).ToString()+"\t"+
					rdr["ebird_discount"].ToString().Trim()+"\t"+
					//GenUtil.TrimLength(rdr["City"].ToString().Trim(),11),
					//GenUtil.strNumericFormat(rdr["Grand_Total"].ToString().Trim()),
					discountfocDiscB(rdr["invoice_no"].ToString().Trim()).ToString()+"\t"+
					rdr["fixed_discount_type"].ToString().Trim()+"\t"+
					rdr["fixed_discount"].ToString().Trim()+"\t"+        //Add by vikas 08.07.09
					discountfocDiscC(rdr["invoice_no"].ToString().Trim()).ToString()+"\t"+
					discountfixdDisc(rdr["invoice_no"].ToString().Trim()).ToString()+"\t"+
					//discountfocDiscA(rdr["invoice_no"].ToString().Trim()).ToString(),
					//System.Convert.ToString(CalVat(double.Parse(rdr["VAT_Amount"].ToString().Trim()))),        //coment by vikas 27.05.09 CustName
					total.ToString()+"\t"+
					foc_qty(rdr["invoice_no"].ToString())+"\t"+
					rdr["Supp_Name"].ToString().Trim()
					);
			}
			//sw.WriteLine("Total\t\t"+GenUtil.strNumericFormat(Cache["gt"].ToString())+"\t"+Cache["Qty"].ToString()+"\t"+Cache["CashDisctotal"].ToString()+"\t"+Cache["Disctotal"].ToString()+"\t"+Cache["tr"]+"\t"+Cache["eb"]+"\t"+Cache["focDisctotal"].ToString()+"\t"+Cache["fixedDisctotal"].ToString()+"\t"+Cache["vt"]+"\t"+Cache["nt"].ToString());
			//Coment by vikas08.07.09 sw.WriteLine("Total\t\t"+Cache["Qty"].ToString()+"\t"+Cache["nt"].ToString()+"\t"+Cache["tr"]+"\t"+Cache["CashDisctotal"].ToString()+"\t"+Cache["eb"]+"\t"+Cache["focDisctotal"].ToString()+"\t"+Cache["DiscPer"].ToString()+"\t"+Cache["Disctotal"].ToString()+"\t"+Cache["TotDis"].ToString()+"\t"+Cache["foc_qty_tot"].ToString());
			sw.WriteLine("Total\t\t"+Cache["Qty"]+"\t"+Cache["nt"]+"\t"+Cache["tr"]+"\t"+Cache["CashDisctotal"]+"\t"+Cache["eb"]+"\t"+Cache["focDisctotal"]+"\t"+Cache["DiscPer"]+"\t"+Cache["DiscPer_Add"]+"\t"+Cache["Disctotal"]+"\t"+Cache["fixd_Disctotal"]+"\t"+Cache["TotDis"]+"\t"+Cache["foc_qty_tot"]+"\t");
			rdr.Close();																																			
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file PurchaseListioclReport.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			makingReport();

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PurchaseioclReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:BtnPrint_Click  PurchaseioclReport   userid  "+uid);
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:BtnPrint_Click, PurchaseioclReport Printed    EXCEPTION  "+ ane.Message+" userid  "+  uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:BtnPrint_Click, PurchaseioclReport Printed  EXCEPTION  "+ se.Message+"  userid  "+  uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
	
					CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:BtnPrint_Click, PurchaseioclReport Printed   EXCEPTION "+es.Message+"  userid  "+  uid);
				}

			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:PurchaseioclReport.aspx,Method:BtnPrint_Click, PurchaseioclReport Printed  EXCEPTION   "+ es.Message+"  userid  "+  uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file PurchaseListioclReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(PurchaseGrid.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:PurchaseLiseIOCL.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   PurchaseLiseIOCL Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:PurchaseLiseIOCL.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    PurchaseLiseIOCL Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to return count the total discount
		/// </summary>
		public string GetTotal(string td,string cd,string ed,string fd,string od,string dp)
		{
			//coment by vikas 6.12.2012 double Tot = double.Parse(td)+double.Parse(cd)+double.Parse(ed)+double.Parse(fd)+double.Parse(od)+double.Parse(dp);
			 //double Tot = double.Parse(td)+double.Parse(cd)+double.Parse(ed)+double.Parse(fd)+double.Parse(od)+double.Parse(dp)+double.Parse(fixedDisc.ToString());
			double Tot = double.Parse(td)+double.Parse(cd)+double.Parse(ed)+double.Parse(fd)+double.Parse(od)+double.Parse(dp)+double.Parse(fixedDisc.ToString());
			
			TotDis += Tot;
			Cache["TotDis"]=TotDis.ToString();
			return Tot.ToString();
		}
	}
}