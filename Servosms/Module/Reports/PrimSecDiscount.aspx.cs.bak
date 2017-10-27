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
using RMG;
using DBOperations;
using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for PrimSecDiscount.
	/// </summary>
	public partial class PrimSecDiscount : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid; 

		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="29";
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
				txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				TextBox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["txtDateFrom"].ToString();
            TextBox1.Text = Request.Form["Textbox1"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["Textbox1"].ToString();
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
		/// This method is used to return the data in MM/dd/YYYY format
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

		public void Pre_Discount()
		{
			string ProdName="",PackType="",TotalQty="",TotalRate="",CustID="",SchType="";
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			//coment by vikas 18.11.2012 string sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(TextBox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(TextBox1.Text) +"'";
			string sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,os.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(TextBox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(TextBox1.Text) +"'";

			rdr=obj.GetRecordSet(sql);
			while(rdr.Read())
			{
				ProdName=rdr["prod_name"].ToString();
				PackType=rdr["pack_type"].ToString();
				TotalQty=rdr["total_qty"].ToString();
				TotalRate=rdr["TotalRate"].ToString();
				CustID=rdr["Cust_ID"].ToString();
				SchType=rdr["sch_type"].ToString();
				GetPerDiscount(ProdName,PackType,TotalQty,TotalRate,CustID,SchType);
			}
			rdr.Close();

			lblperdis.Text=GenUtil.strNumericFormat(Convert.ToString(total_PerDisc));

		}

		public double total_SchDisc=0,total_PerDisc=0,total_FleetOe=0,total_Vat=0,total_Total=0,total_TotalAmount=0,total_CashDisc=0;
		public void GetPerDiscount(string ProdName,string PackType,string TotalQty,string TotalRate,string CustID,string SchType)
		{
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass();
			double localval=0;
			string localper="";
			SqlDataReader rdr1 = obj1.GetRecordSet("select * from customer where cust_id='"+CustID+"' and (cust_type like'fleet%' or cust_type like 'oe%')");
			if(rdr1.HasRows)
			{
			}
			else
			{
				SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(TextBox1.Text)+"'");
				if(rdr.Read())
				{
					localper=rdr["DiscountType"].ToString();
					localval=double.Parse(rdr["discount"].ToString());
				}
				rdr.Close();
				if(localper=="%")
					localval=localval*double.Parse(TotalRate)/100;
				else
					localval=0;
			}
			rdr1.Close();
			//TotalVat+=double.Parse(TotalRate);
			total_PerDisc+=localval;
			//PerDisc=localval;
			//TotalVat-=localval;

			Cache["total_PerDisc"]=total_PerDisc;

			
			//return System.Convert.ToString(Math.Round(localval,1));
		}


		/// <summary>
		/// This method is used to view the report.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{

                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["TextBox1"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {
                    MessageBox.Show("Date From Should be less than Date To");
                }
                else
                {
                    Pre_Discount();      //Add by vikas 14.08.09

                    double tradetotal = 0;
                    double ebirdtotal = 0;
                    double schemetotal = 0;
                    double fleettotal = 0;
                    double oetotal = 0;
                    double totalsalltr = 0;
                    double totalpurltr = 0;


                    //*****************
                    InventoryClass obj = new InventoryClass();
                    SqlDataReader SqlDtr;
                    //SqlDataReader SqlDtr1;
                    //string sql;
                    string sql1;
                    //SqlDataReader rdr; 

                    //cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
                    sql1 = "select * from Purchase_master  where cast(floor(cast(Invoice_date as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(TextBox1.Text.Trim()) + "'";
                    //sql="select  from Purchase_master  where  cast(floor(cast(vndr_invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(vndr_invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(TextBox1.Text.Trim()) +"'";
                    SqlDtr = obj.GetRecordSet(sql1);
                    if (SqlDtr.HasRows)
                    {
                        while (SqlDtr.Read())
                        {
                            discountpurchase(SqlDtr.GetValue(0).ToString());
                            tradetotal += System.Convert.ToDouble(SqlDtr.GetValue(19).ToString());
                            ebirdtotal += System.Convert.ToDouble(SqlDtr.GetValue(21).ToString());
                            totalpurltr += System.Convert.ToDouble(SqlDtr.GetValue(28).ToString());

                        }
                    }
                    else
                    {
                        Cache["focDisctotal"] = "0";
                        Cache["fixedDisctotal"] = "0";
                        Cache["CashDisctotal"] = "0";
                    }
                    //*********************
                    //txttotalpurltr.Text=totalpurltr.ToString();
                    //totalpurltr=Math.Round(totalpurltr,2);
                    lbltotalpurltr.Text = GenUtil.strNumericFormat(totalpurltr.ToString());
                    //txttrade.Text=tradetotal.ToString();
                    //tradetotal=Math.Round(tradetotal,2);
                    lbltrade.Text = GenUtil.strNumericFormat(tradetotal.ToString());
                    //txtebird.Text=ebirdtotal.ToString();
                    //ebirdtotal=Math.Round(ebirdtotal,2);
                    lblebird.Text = GenUtil.strNumericFormat(ebirdtotal.ToString());
                    //				txtfoc.Text=Cache["focDisctotal"].ToString();
                    //				txtfixed.Text=Cache["fixedDisctotal"].ToString();
                    //				txtcash.Text=Cache["CashDisctotal"].ToString();
                    //				double s1=	System.Convert.ToDouble(txtfoc.Text.ToString())+System.Convert.ToDouble(txtfixed.Text.ToString())+System.Convert.ToDouble(txtcash.Text.ToString())+tradetotal+ebirdtotal;
                    lblfoc.Text = Cache["focDisctotal"].ToString();
                    lblfixed.Text = Cache["fixedDisctotal"].ToString();
                    lblcash.Text = Cache["CashDisctotal"].ToString();



                    SqlDtr.Close();
                    //********************

                    SqlDataReader SqlDtr1;
                    string sql3;
                    string sql2;
                    SqlDataReader rdr = null;
                    //SqlDataReader rdr; 
                    //*********************************
                    //sql2="select sum(total_qty*sales*o.discount) from products p,oilscheme o,stock_master ss where ss.productid=p.prod_id and ss.productid=o.prodid and schname='Secondry(LTR Scheme)' and cast(floor(cast(stock_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(stock_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(TextBox1.Text)+"' and cast(floor(cast(datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(TextBox1.Text)+"'";
                    sql2 = "select sum(schdiscount) from sales_master where cast(floor(cast(invoice_date as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(txtDateFrom.Text) + "' and cast(floor(cast(invoice_date as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(TextBox1.Text) + "'";
                    SqlDtr1 = obj.GetRecordSet(sql2);
                    if (SqlDtr1.Read())
                    {
                        if (SqlDtr1.GetValue(0).ToString() != "" && SqlDtr1.GetValue(0).ToString() != null)
                            schemetotal = double.Parse(SqlDtr1.GetValue(0).ToString());
                    }
                    SqlDtr1.Close();
                    //*********************************

                    //**********Add by Vikas Sharma 15.04.09*********************** 
                    //sql2="select sum(schdiscount) from sales_master where cast(floor(cast(invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(TextBox1.Text)+"'";

                    sql2 = "select sum((case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end)) as Disc from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(txtDateFrom.Text) + "' and cast(floor(cast(invoice_date as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(TextBox1.Text) + "'";
                    double Old_Disc = 0;
                    SqlDtr1 = obj.GetRecordSet(sql2);
                    if (SqlDtr1.Read())
                    {
                        if (SqlDtr1.GetValue(0).ToString() != "" && SqlDtr1.GetValue(0).ToString() != null)
                            Old_Disc = double.Parse(SqlDtr1.GetValue(0).ToString());
                    }
                    SqlDtr1.Close();
                    lblold.Text = GenUtil.strNumericFormat(Old_Disc.ToString());

                    double s1 = System.Convert.ToDouble(lblfoc.Text.ToString()) + System.Convert.ToDouble(lblfixed.Text.ToString()) + System.Convert.ToDouble(lblcash.Text.ToString()) + tradetotal + ebirdtotal + Old_Disc;
                    s1 = Math.Round(s1, 2);
                    lblpurtotal.Text = s1.ToString();
                    //**********end*************************************************

                    //cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
                    sql2 = "select * from sales_master  where cast(floor(cast(Invoice_date as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(TextBox1.Text.Trim()) + "'";
                    //sql="select  from Purchase_master  where  cast(floor(cast(vndr_invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(vndr_invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(TextBox1.Text.Trim()) +"'";
                    SqlDtr1 = obj.GetRecordSet(sql2);
                    if (SqlDtr1.HasRows)
                    {
                        while (SqlDtr1.Read())
                        {
                            discountsale(SqlDtr1.GetValue(0).ToString());
                            //schemetotal+=System.Convert.ToDouble(SqlDtr1.GetValue(18).ToString());
                            totalsalltr += System.Convert.ToDouble(SqlDtr1.GetValue(22).ToString());
                            //*********
                            sql3 = "select cust_type from customer  where cust_id='" + SqlDtr1.GetValue(3).ToString() + "'";
                            dbobj.SelectQuery(sql3, ref rdr);
                            if (rdr.Read())
                            {
                                string type = rdr.GetValue(0).ToString();
                                type = type.Substring(0, 2);
                                if (type.ToLower().Equals("fl"))
                                {
                                    fleettotal += System.Convert.ToDouble(SqlDtr1.GetValue(21).ToString());
                                }
                                else if (type.ToLower().Equals("oe"))
                                    oetotal += System.Convert.ToDouble(SqlDtr1.GetValue(21).ToString());
                            }
                            // rdr.Close();
                            //******************
                            //**		fleettotal+=System.Convert.ToDouble(SqlDtr1.GetValue(21).ToString());

                        }
                    }
                    else
                        Cache["saleCashDisctotal"] = "0";
                    //SqlDtr1.Close();
                    //*********************

                    //				txtoesale.Text=oetotal.ToString();
                    //				txtfleetsale.Text=fleettotal.ToString();
                    //				txtsecsale.Text=schemetotal.ToString();
                    //				//txtdiscountsale.Text=Cache["saleDisctotal"].ToString();
                    //				txtcashsale.Text=Cache["saleCashDisctotal"].ToString();
                    //				double s2=	System.Convert.ToDouble(txtfleetsale.Text.ToString())+System.Convert.ToDouble(txtoesale.Text.ToString())+System.Convert.ToDouble(txtsecsale.Text.ToString())+System.Convert.ToDouble(txtcashsale.Text.ToString());
                    //				txtsaletotal.Text=s2.ToString();
                    //totalsalltr=Math.Round(totalsalltr,2);
                    lbltotalsalltr.Text = GenUtil.strNumericFormat(totalsalltr.ToString());
                    //oetotal=Math.Round(oetotal,2);
                    lbloesale.Text = GenUtil.strNumericFormat(oetotal.ToString());
                    //fleettotal=Math.Round(fleettotal,2);
                    lblfleetsale.Text = GenUtil.strNumericFormat(fleettotal.ToString());
                    //schemetotal=Math.Round(schemetotal,2);
                    lblsecsale.Text = GenUtil.strNumericFormat(schemetotal.ToString());
                    //txtdiscountsale.Text=Cache["saleDisctotal"].ToString();
                    lblcashsale.Text = Cache["saleCashDisctotal"].ToString();
                    if (lblcashsale.Text != "")
                        lblcashsale.Text = System.Convert.ToString(Math.Round(double.Parse(lblcashsale.Text), 2));

                    //Coment by vikas 14.08.09 double s2=	System.Convert.ToDouble(lblfleetsale.Text.ToString())+System.Convert.ToDouble(lbloesale.Text.ToString())+System.Convert.ToDouble(lblsecsale.Text.ToString())+System.Convert.ToDouble(lblcashsale.Text.ToString());

                    double s2 = System.Convert.ToDouble(lblfleetsale.Text.ToString()) + System.Convert.ToDouble(lbloesale.Text.ToString()) + System.Convert.ToDouble(lblsecsale.Text.ToString()) + System.Convert.ToDouble(lblcashsale.Text.ToString()) + System.Convert.ToDouble(lblperdis.Text.ToString());

                    //s2=Math.Round(s2,2);
                    lblsaletotal.Text = GenUtil.strNumericFormat(s2.ToString());
                    //**********************
                    CreateLogFiles.ErrorLog("Form:PrimSecDiscount.aspx,Method:btnShow_Click  PrimSecDiscount   Viewed " + "  userid  " + uid);
                }
                
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimSecDiscount.aspx,Method:btnShow_Click  PrimSecDiscount   Viewed "+"  EXCEPTION  "+ ex.Message+"  userid  "+uid);
			}
		}
		//**********
		
		double fixedDisctotal=0;
		double focDisctotal=0;
		double CashDisctotal=0;
		double Disctotal=0;
		/// <summary>
		/// This method return the calculate the purchase discount.
		/// </summary>
		public double discountpurchase(string invoice)
		{
			double fixedDisc=0;
			double focDisc=0;
			double CashDisc=0;
			double Disc=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select * from purchase_master where invoice_no='"+invoice+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double grandtot=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				
				//fixedDisc=System.Convert.ToDouble(rdr.GetValue(26).ToString());
				fixedDisc=System.Convert.ToDouble(rdr.GetValue(27).ToString());
				//if(rdr.GetValue(27).ToString().Equals("Per"))
				//	fixedDisc=grandtot*fixedDisc/100; 

				focDisc=System.Convert.ToDouble(rdr.GetValue(24).ToString());
				if(rdr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				double ETFOC=(focDisc*2)/100;
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
					GT=grandtot+ etax-(System.Convert.ToDouble(rdr.GetValue(21).ToString())+System.Convert.ToDouble(rdr.GetValue(19).ToString())+focDisc+Disc+fixedDisc+ETFOC);
					CashDisc=GT*CashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
			
			Cache["focDisc"]=focDisc;
			focDisctotal+=focDisc;
			//Cache["focDisctotal"]=Math.Round(focDisctotal,2);
			Cache["focDisctotal"]=GenUtil.strNumericFormat(focDisctotal.ToString());
			
			Cache["fixed"]=fixedDisc;
			fixedDisctotal+=fixedDisc;
			//Cache["fixedDisctotal"]=Math.Round(fixedDisctotal,2);
			Cache["fixedDisctotal"]=GenUtil.strNumericFormat(fixedDisctotal.ToString());
			
			Cache["Disc"]=Disc;
			Disctotal+=Disc;
			//Cache["Disctotal"]=Math.Round(Disctotal,2);	
			Cache["Disctotal"]=GenUtil.strNumericFormat(Disctotal.ToString());	
			
			CashDisctotal+=CashDisc;
			//Cache["CashDisctotal"]=Math.Round(CashDisctotal,2);
			Cache["CashDisctotal"]=GenUtil.strNumericFormat(CashDisctotal.ToString());
			//return System.Convert.ToDouble(Math.Round(CashDisc,2));
			return System.Convert.ToDouble(GenUtil.strNumericFormat(CashDisc.ToString()));
		}
		//**************
		
		double saleCashDisctotal=0;
		double saleDisctotal=0;
		/// <summary>
		/// This method calculate the sales discount.
		/// </summary>
		public double discountsale(string invoice)
		{
			double saleCashDisc=0;
			double saleDisc=0;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select* from sales_master where invoice_no='"+invoice+"'";
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				double salegrandtot=System.Convert.ToDouble(rdr.GetValue(6).ToString());
				saleDisc=System.Convert.ToDouble(rdr.GetValue(7).ToString());
				if(rdr.GetValue(8).ToString().Equals("Per"))
				{
					// double Dt=System.Convert.ToDouble(rdr.GetValue(7).ToString());
					saleDisc=salegrandtot*saleDisc/100 ;
				}	
				saleCashDisc=System.Convert.ToDouble(rdr.GetValue(15).ToString());
				double saleGT=0;
				if(rdr.GetValue(16).ToString().Equals("Per"))
				{  		
					saleGT=salegrandtot-(System.Convert.ToDouble(rdr.GetValue(18).ToString())+System.Convert.ToDouble(rdr.GetValue(21).ToString())+saleDisc);
					saleCashDisc=saleGT*saleCashDisc/100;
				}
				//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc))
			}
			rdr.Close();
			Cache["saleDisc"]=saleDisc;
			saleDisctotal+=saleDisc;
			//Cache["saleDisctotal"]=Math.Round(saleDisctotal,2);	
			Cache["saleDisctotal"]=GenUtil.strNumericFormat(saleDisctotal.ToString());	
			saleCashDisctotal+=saleCashDisc;
			//Cache["saleCashDisctotal"]=Math.Round(saleCashDisctotal,2);
			Cache["saleCashDisctotal"]=GenUtil.strNumericFormat(saleCashDisctotal.ToString());
			//return System.Convert.ToDouble(Math.Round(saleCashDisctotal,2));
			return System.Convert.ToDouble(GenUtil.strNumericFormat(saleCashDisctotal.ToString()));
		}

		/// <summary>
		/// This method is used to prepares the report file PrimSecDiscountReport.txt for printing.
		/// </summary>
		public void makingReport()
		{
			try
			{
				//System.Data.SqlClient.SqlDataReader rdr=null;
				string info = "";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PrimSecDiscountReport.txt";
				StreamWriter sw = new StreamWriter(path);
				
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

				info = " {0,-40:S} {1,-5:S} {2,14:S} {3,16:S} {4,-7:S} ";
				//**********
				string des="----------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Primary Secondry Discount Report From "+txtDateFrom.Text.ToString()+" To "+TextBox1.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine("+----------------------------------------+-----+--------------+----------------+-------+");
				sw.WriteLine(info,"Primary Sales Discount","","Purchase:",lbltotalpurltr.Text.ToString(),"Ltr./Kg");
				sw.WriteLine(info,"1.Early Bird Discount","","",lblebird.Text.ToString(),"Rs.");
				sw.WriteLine(info,"2.Trade Discount","","",lbltrade.Text.ToString(),"Rs.");
				sw.WriteLine(info,"3.Cash Discount","","",lblcash.Text.ToString(),"Rs.");
				sw.WriteLine(info,"4.Fixed (Transport Subcidy) Discount","","",lblfixed.Text.ToString(),"Rs.");
				sw.WriteLine(info,"5.FOC Discount","","",lblfoc.Text.ToString(),"Rs.");
				sw.WriteLine(info,"6.Old Rate Discount","","",lblold.Text.ToString(),"Rs."); //Add By Vikas Sharma 16.04.09 
				sw.WriteLine("+----------------------------------------+-----+--------------+----------------+-------+"); 
				sw.WriteLine(info,"Total Primary Discount Recieved","","",lblpurtotal.Text.ToString(),"Rs.");	           
				//  1234567890123456789012345678901234567890 12345 123456789012345 123456789012345 1234567   12345678 12345678 123456789
				sw.WriteLine("+----------------------------------------+-----+--------------+----------------+-------+"); 
				sw.WriteLine(" "); 	
				sw.WriteLine(" "); 		
				sw.WriteLine("+----------------------------------------+-----+--------------+----------------+-------+");
				sw.WriteLine(info,"Secondry Sales Discount","","Sales:",GenUtil.strNumericFormat(lbltotalsalltr.Text.ToString()),"Ltr./Kg");
				sw.WriteLine(info,"1.Secondry Sales Scheme Discount","","",lblsecsale.Text.ToString(),"Rs.");
				sw.WriteLine(info,"2.Primary Sales Scheme Discount","","",lblprimsale.Text.ToString(),"Rs.");
				sw.WriteLine(info,"3.Cash Discount","","",lblcashsale.Text.ToString(),"Rs.");
				sw.WriteLine(info,"4.Fleet Discount","","",lblfleetsale.Text.ToString(),"Rs.");
				sw.WriteLine(info,"5.OE Discount","","",lbloesale.Text.ToString(),"Rs.");
				sw.WriteLine(info,"6.(%) Discount","","",lblperdis.Text.ToString(),"Rs.");        //Add by vikas 14.08.09
				sw.WriteLine("+----------------------------------------+-----+--------------+----------------+-------+"); 
				sw.WriteLine(info,"Total Secondry Discount Passed","","",lblsaletotal.Text.ToString(),"Rs.");	           
				//  1234567890123456789012345678901234567890 12345 123456789012345 123456789012345 1234567   12345678 12345678 123456789
				sw.WriteLine("+----------------------------------------+-----+--------------+----------------+-------+"); 
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PrimSecDiscountReport.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			//InventoryClass obj=new InventoryClass();
			//SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\PrimSecDiscount.xls";
			StreamWriter sw = new StreamWriter(path);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+TextBox1.Text);
			sw.WriteLine("Primary Sales Discount\t"+"Purchase:\t"+lbltotalpurltr.Text.ToString()+"\tLtr./Kg");
			sw.WriteLine("1.Early Bird Discount\t\t"+lblebird.Text.ToString()+"\tRs.");
			sw.WriteLine("2.Trade Discount\t\t"+lbltrade.Text.ToString()+"\tRs.");
			sw.WriteLine("3.Cash Discount\t\t"+lblcash.Text.ToString()+"\tRs.");
			sw.WriteLine("4.Fixed (Transport Subcidy) Discount\t\t"+lblfixed.Text.ToString()+"\tRs.");
			sw.WriteLine("5.FOC Discount\t\t"+lblfoc.Text.ToString()+"\tRs.");
			sw.WriteLine("6.Old Rate Discount\t\t"+lblold.Text.ToString()+"\tRs.");
			sw.WriteLine("Total Primary Discount Recieved\t\t"+lblpurtotal.Text.ToString()+"\tRs.");	           
			//  1234567890123456789012345678901234567890 12345 123456789012345 123456789012345 1234567   12345678 12345678 123456789
			sw.WriteLine(); 	
			sw.WriteLine(); 		
			sw.WriteLine("Secondry Sales Discount\t"+"Sales:\t"+GenUtil.strNumericFormat(lbltotalsalltr.Text.ToString())+"\tLtr./Kg");
			sw.WriteLine("1.Secondry Sales Scheme Discount\t\t"+lblsecsale.Text.ToString()+"\tRs.");
			sw.WriteLine("2.Primary Sales Scheme Discount\t\t"+lblprimsale.Text.ToString()+"\tRs.");
			sw.WriteLine("3.Cash Discount\t\t"+lblcashsale.Text.ToString()+"\tRs.");
			sw.WriteLine("4.Fleet Discount\t\t"+lblfleetsale.Text.ToString()+"\tRs.");
			sw.WriteLine("5.OE Discount\t\t"+lbloesale.Text.ToString()+"\tRs.");
			sw.WriteLine("6.(%) Discount\t\t"+lblperdis.Text.ToString()+"\tRs.");
			sw.WriteLine("Total Secondry Discount Passed\t\t"+lblsaletotal.Text.ToString()+"\tRs.");	           
			sw.Close();
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the PrimarySecDiscount.txt file name to print.
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PrimSecDiscountReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:PrimSecDiscountReport.aspx,Method:BtnPrint_Click  PrimSecDiscountReport Report   userid  "+uid);
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:PrimSecDiscountReport.aspx,Method:BtnPrint_Click, PrimSecDiscountReport Report Printed    EXCEPTION  "+ ane.Message+" userid  "+  uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:PrimSecDiscountReport.aspx,Method:BtnPrint_Click, PrimSecDiscountReport Report Printed  EXCEPTION  "+ se.Message+"  userid  "+  uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
	
					CreateLogFiles.ErrorLog("Form:PrimSecDiscountReport.aspx,Method:BtnPrint_Click, PrimSecDiscountReport Printed   EXCEPTION "+es.Message+"  userid  "+  uid);
				}

			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:PrimSecDiscountReport.aspx,Method:BtnPrint_Click, PrimSecDiscount Report Printed  EXCEPTION   "+ es.Message+"  userid  "+  uid);
			}
		}

		// This method is used to prepares the excel report file PrimarySecDiscount.xls for printing.
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//if(GridReport.Visible==true)
				//{
				ConvertToExcel();
				MessageBox.Show("Successfully Convert File Into Excel Format");
				CreateLogFiles.ErrorLog("Form:PrimSecDiscount.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   PrimSecDiscount Convert Into Excel Format, userid  "+uid);
				//}
				//else
				//{
				//	MessageBox.Show("Please Click the View Button First");
				//	return;
				//}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:PrimSecDiscount.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    PrimSecDiscount Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}