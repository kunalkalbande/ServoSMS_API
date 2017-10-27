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
	/// Summary description for VAT_Report.
	/// </summary>
	public partial class VAT_Report : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
		public double grand_total = 0;
		public double vat_total = 0;
		public double net_total = 0;
		public double cash_discount = 0;
		public double Total_Sales=0;
		public double Total_Purchase=0;
		public double Total_PurVar=0;
		public double Total_SalVat=0;
		public string tempFromDate="",tempToDate="";
		public static ArrayList tempMonth = new ArrayList();
		public double other_discount = 0;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSumPurchase;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tempTotalPurchase;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tempTotalSale;
		protected System.Web.UI.HtmlControls.HtmlInputText txtNetAmount;
		System.Globalization.NumberFormatInfo  nfi = new System.Globalization.CultureInfo("en-US",false).NumberFormat;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
				Total_Sales=0;
				Total_Purchase=0;
				Total_PurVar=0;
				Total_SalVat=0;
				
				if(! IsPostBack)
				{
					SalesGrid.Visible=false;
					PurchaseGrid.Visible=false;
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());

					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="51";
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

					tempMonth=new ArrayList();
				}
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
                if (txtValue.Text!="")
					Total_PurVar+=double.Parse(txtValue.Text);
				BtnPrint.Attributes.Add("OnClick","CheckPurchaseSum();");
				btnExcel.Attributes.Add("OnClick","CheckPurchaseSum();");
				//cmdrpt.Attributes.Add("OnClick","CheckPurchaseSum();");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
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

		private void grdLeg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		
		/// <summary>
		/// checks the validity of form input fields.
		/// </summary>
		/// <returns></returns>
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
			/*if(DropReportType.SelectedIndex  == 0)
			{
				ErrorMessage = ErrorMessage + " - Please Select Report\n";
				flag = false;
			}*/
			/*if(DropReportType.SelectedIndex  == 0)
			{
				ErrorMessage = ErrorMessage + " - Please Select Report\n";
				flag = false;
			}*/

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
		/// return the data in MM/dd/YYYY format
		/// </summary>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0? str.Split(new char[]{'/'},str.Length) : str.Split(new char[] { '-' }, str.Length);
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
		/// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem )  )
			{
				GrandTotal(Double.Parse(e.Item.Cells[5].Text));
				CashDiscount(Double.Parse(e.Item.Cells[7].Text)); 
				VATTotal(Double.Parse(e.Item.Cells[8].Text));
				//31.12.2012 OtherDiscount(Double.Parse(e.Item.Cells[8].Text));
				//31.12.2012 NetTotal(Double.Parse(e.Item.Cells[9].Text));
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
				e.Item.Cells[5].Text =grand_total.ToString("N",nfi);   
				e.Item.Cells[7].Text = cash_discount.ToString("N",nfi); 
				e.Item.Cells[8].Text = vat_total.ToString("N",nfi);  
				//31.12.2012 e.Item.Cells[8].Text = other_discount.ToString("N",nfi);
				//31.12.2012 e.Item.Cells[9].Text = net_total.ToString("N",nfi);
				grand_total = 0;
				cash_discount = 0;
				vat_total = 0;
				other_discount = 0;
				net_total = 0;
			}
		}

		double Tot_Sale_Disc=0;
		public double Total=0;
		public string GetSaleDiscount(string Invoice_No)
		{
			Tot_Sale_Disc=0;
			double Disc=0,Cash_Disc=0,Foe_Disc=0,FoeDisc=0,SchDisc=0,G_Total=0;
			string Cash_Disc_Type="",Foe_Disc_Type="";
			InventoryClass obj = new InventoryClass();
			string sql="Select Discount,Discount_Type,Cash_Discount, Cash_Disc_Type,schdiscount,foediscount,foediscounttype,foediscountrs,Grand_total from sales_master where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and Invoice_no like '%"+Invoice_No+"'";
			SqlDataReader rdr = obj.GetRecordSet(sql);
			if(rdr.Read())
			{
				if(rdr["Discount"].ToString()!="")
					Disc=double.Parse(rdr["Discount"].ToString());

				if(rdr["Cash_Discount"].ToString()!="")
					Cash_Disc=double.Parse(rdr["Cash_Discount"].ToString());

				if(rdr["Cash_Disc_Type"].ToString()!="")
					Cash_Disc_Type=rdr["Cash_Disc_Type"].ToString();

				if(rdr["Grand_total"].ToString()!="")
					G_Total=double.Parse(rdr["Grand_total"].ToString());

				if(rdr["schdiscount"].ToString()!="")
					SchDisc=double.Parse(rdr["schdiscount"].ToString());

				if(rdr["foediscountrs"].ToString()!="")
					FoeDisc=double.Parse(rdr["foediscountrs"].ToString());
			}
			rdr.Close();
			
			if(Cash_Disc_Type=="Per")
			{
				Cash_Disc=Math.Round((G_Total*Cash_Disc)/100,2);
			}


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
			
			Total+=Tot_Sale_Disc;

			return Tot_Sale_Disc.ToString();
		}


		double Tot_Pur_Disc=0;
		public double Total_Pur=0;
		public string GetPurDiscount(string Invoice_No)
		{
			Tot_Pur_Disc=0;
			double Disc=0,Cash_Disc=0,Trade_Disc=0,EB_Disc=0,FOC_Disc=0,Fixd_Disc=0,Fixd_Disc_New=0,Fixd_Amount=0,G_Total=0,Entry_Tax=0;;
			string Disc_Type="",Cash_Disc_Type="",FOC_Disc_Type="",Fixd_Disc_Type="";
			InventoryClass obj = new InventoryClass();
			string sql="select Discount,Discount_Type,Cash_Discount,Cash_Disc_Type,Ebird_Discount,Trade_Discount,Foc_Discount,Foc_Discount_Type,Fixed_Discount,Fixed_Discount_Type,Fixed_Disc_Amount,Grand_Total,Invoice_No,Entry_Tax1 from purchase_master where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and Vndr_Invoice_No like '%"+Invoice_No+"'";
			SqlDataReader rdr = obj.GetRecordSet(sql);
			if(rdr.Read())
			{
				if(rdr["Discount"].ToString()!="")
					Disc=double.Parse(rdr["Discount"].ToString());      //1

				if(rdr["Discount_Type"].ToString()!="")
					Disc_Type=rdr["Discount_Type"].ToString();

				if(rdr["Cash_Discount"].ToString()!="")
					Cash_Disc=double.Parse(rdr["Cash_Discount"].ToString());

				if(rdr["Cash_Disc_Type"].ToString()!="")
					Cash_Disc_Type=rdr["Cash_Disc_Type"].ToString();

				if(rdr["Ebird_Discount"].ToString()!="")
					EB_Disc=double.Parse(rdr["Ebird_Discount"].ToString());    //3

				if(rdr["Trade_Discount"].ToString()!="")
					Trade_Disc=double.Parse(rdr["Trade_Discount"].ToString());    //4

				if(rdr["Foc_Discount"].ToString()!="")
					FOC_Disc=double.Parse(rdr["Foc_Discount"].ToString());

				if(rdr["Foc_Discount_Type"].ToString()!="")
					FOC_Disc_Type=rdr["Foc_Discount_Type"].ToString();

				if(rdr["Fixed_Discount"].ToString()!="")
					Fixd_Disc=double.Parse(rdr["Fixed_Discount"].ToString());    //5
				
				if(rdr["Fixed_Discount_Type"].ToString()!="")
					Fixd_Disc_Type=rdr["Fixed_Discount_Type"].ToString();      //6

				if(rdr["Fixed_Disc_Amount"].ToString()!="")
					Fixd_Amount=double.Parse(rdr["Fixed_Disc_Amount"].ToString());          //7

				if(rdr["Grand_Total"].ToString()!="")
					G_Total=double.Parse(rdr["Grand_Total"].ToString());   

				if(rdr["Invoice_No"].ToString()!="")
					Invoice_No=rdr["Invoice_No"].ToString(); 

				if(rdr["Entry_Tax1"].ToString()!="")
					Entry_Tax=double.Parse(rdr["Entry_Tax1"].ToString()); 
			}
			rdr.Close();
			
			if(Cash_Disc_Type=="Per")
			{
				Cash_Disc=Math.Round(((G_Total+Entry_Tax)*Cash_Disc)/100,2);          //2
			}

			
			double Stkt_Disc=0,temp=0,PerDisc=0,Tot_Qty=0,Qty=0,Amount=0,Discount1;
			string Stkt_Type="",Foe_Type="",PerDisc_Type="";
			sql="select Amount,Stktdisc,StktdiscType,PerDisc,PerDiscType,Discount,Total_qty,qty from purchase_details sd,Products p where sd.Prod_id=p.prod_id and Invoice_No ='"+Invoice_No+"'";
			rdr = obj.GetRecordSet(sql);
			while(rdr.Read())
			{

				if(rdr["Qty"].ToString()!="")
					Qty=double.Parse(rdr["Qty"].ToString());

				if(rdr["Total_qty"].ToString()!="")
					Tot_Qty=double.Parse(rdr["Total_qty"].ToString());

				if(rdr["Amount"].ToString()!="")
					Amount=double.Parse(rdr["Amount"].ToString());

				temp=0;
				if(rdr["Stktdisc"].ToString()!="")
					temp=double.Parse(rdr["Stktdisc"].ToString());

				if(rdr["StktdiscType"].ToString()!="")
					Stkt_Type=rdr["StktdiscType"].ToString();

				if(Stkt_Type=="Rs")
				{
					temp=Math.Round(Tot_Qty*Qty*temp);
				}
				else
				{
					temp=Math.Round((Amount*temp)/100);
				}
				Stkt_Disc+=temp;

				temp=0;
				if(rdr["PerDisc"].ToString()!="")
					temp=double.Parse(rdr["PerDisc"].ToString());

				if(rdr["PerDiscType"].ToString()!="")
					PerDisc_Type=rdr["PerDiscType"].ToString();

				if(PerDisc_Type=="Rs")
				{
					temp=Math.Round(Tot_Qty*Qty*temp);
				}
				else
				{
					temp=Math.Round((Amount*temp)/100);
				}
				PerDisc+=temp;

				if(rdr["Discount"].ToString()!="")
					Discount1=double.Parse(rdr["Discount"].ToString());

			}
			rdr.Close();

			//9.5.2013 Fixd_Disc_Type=Convert.ToString(Math.Round(double.Parse(Fixd_Disc_Type.ToString())));
			Fixd_Disc_New=Math.Round(double.Parse(Fixd_Disc_Type.ToString()));
			//Tot_Pur_Disc+=Math.Round(double.Parse(Disc+Cash_Disc+EB_Disc+Trade_Disc+Fixd_Disc+Fixd_Disc_Type+Fixd_Amount+FOC_Disc+Stkt_Disc+PerDisc),2);
			//9.5.2013 Tot_Pur_Disc+=Math.Round(double.Parse(Disc+Cash_Disc+EB_Disc+Trade_Disc+Fixd_Disc+Fixd_Disc_Type+Fixd_Amount+FOC_Disc+Stkt_Disc+PerDisc),2);
			Tot_Pur_Disc+=Math.Round(Disc+Cash_Disc+EB_Disc+Trade_Disc+Fixd_Disc+Fixd_Disc_New+Fixd_Amount+FOC_Disc+Stkt_Disc+PerDisc);
			
			Total_Pur+=Tot_Pur_Disc;

			return Tot_Pur_Disc.ToString();
		}

		/// <summary>
		/// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		/// </summary>
		public void ItemTotal1(object sender,DataGridItemEventArgs e)
		{
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem )  )
			{
				GrandTotal(Double.Parse(e.Item.Cells[5].Text));
				CashDiscount(Double.Parse(e.Item.Cells[6].Text)); 
				//VATTotal(Double.Parse(e.Item.Cells[8].Text));
				OtherDiscount(Double.Parse(e.Item.Cells[8].Text));
				NetTotal(Double.Parse(e.Item.Cells[9].Text));
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
               
				e.Item.Cells[5].Text =grand_total.ToString("N",nfi);   
				e.Item.Cells[6].Text = cash_discount.ToString("N",nfi); 
				//e.Item.Cells[8].Text = vat_total.ToString("N",nfi);  
				e.Item.Cells[8].Text = other_discount.ToString("N",nfi);
				e.Item.Cells[9].Text = net_total.ToString("N",nfi);
				grand_total = 0;
				cash_discount = 0;
				vat_total = 0;
				other_discount = 0;
				net_total = 0;
			}
		}

		/// <summary>
		/// This is used to make sorting of datagrid onclicking of datagridheader.
		/// </summary>
		string strorderby1="";
		public void sortcommand_click1(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["order"].Equals("ASC"))
					{
						strorderby1=e.SortExpression.ToString()+" DESC";
						Session["order"]="DESC";
					}
					else
					{
						strorderby1=e.SortExpression.ToString()+" ASC";
						Session["order"]="ASC";
					}
				}
				else
				{
					strorderby1=e.SortExpression.ToString()+" ASC";
					Session["order"]="ASC";
				}
				Session["column"]=e.SortExpression.ToString();
				Bindthedata1();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Vat_Report.aspx,Method:sortcommand_click1"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
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
				CreateLogFiles.ErrorLog("Form:Vat_Report.aspx,Method:sortcommand_click2"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata1()
		{
			if(DropReportType.SelectedItem.Text == "Sales Report" || DropReportType.SelectedItem.Text == "Both")
			{
				SqlConnection sqlcon1=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				int s=0;
				//int p=0;
				string sqlstr=System.Convert.ToString(Cache["str"]);
				DataSet ds=new DataSet();
				DataTable dtcustomer;
				DataView dv=new DataView();
				if(sqlstr!="")
				{
					SqlDataAdapter da=new SqlDataAdapter(sqlstr,sqlcon1);
					da.Fill(ds,"sales_master");
					dtcustomer=ds.Tables["sales_master"];
					dv=new DataView(dtcustomer);
					dv.Sort=strorderby1;
					Cache["strorderby1"]=strorderby1;
					SalesGrid.DataSource=dv;
				}
				if(dv.Count!=0)
				{
					SalesGrid.DataBind();
					SalesGrid.Visible=true;
					lblSalesHeading.Visible=true;
				}
				else
				{
					SalesGrid.Visible=false;
					lblSalesHeading.Visible=false;
					s++;
				}
				if(s>0 )
				{
					MessageBox.Show("Sales Data not available");
				}
				sqlcon1.Dispose();
			}
			else
			{
				lblSalesHeading.Visible = false; 
				SalesGrid.Visible=false;
			}
		}

		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata2()
		{
			if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
			{
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
					lblPurchaseHeading.Visible=true;
				}
				else
				{
					PurchaseGrid.Visible=false;
					lblPurchaseHeading.Visible=false;
					p++;
				}
				//sqlcon.Dispose();
				if(p>0 )
				{
					MessageBox.Show("Purchase Data not available");
				}
				sqlcon2.Dispose();
			}
			else
			{
				lblPurchaseHeading.Visible = false; 
				PurchaseGrid.Visible=false;
			}
		}
		/*	public void Bindthedata()
			{
				SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				int s=0;
				int p=0;
				string sqlstr=System.Convert.ToString(Cache["str"]);
				DataSet ds=new DataSet();
				DataTable dtcustomer;
				DataView dv=new DataView();
				if(sqlstr!="")
				{
					SqlDataAdapter da=new SqlDataAdapter(sqlstr,sqlcon);
					da.Fill(ds,"sales_master");
					dtcustomer=ds.Tables["sales_master"];
					dv=new DataView(dtcustomer);
					dv.Sort=strorderby;
					Cache["strorderby"]=strorderby;
					SalesGrid.DataSource=dv;
				}
				if(dv.Count!=0)
				{
					SalesGrid.DataBind();
					SalesGrid.Visible=true;
					lblSalesHeading.Visible=true;
				}
				else
				{
					SalesGrid.Visible=false;
					lblSalesHeading.Visible=false;
					s++;
				}
				string sqlstr1=System.Convert.ToString(Cache["str1"]);
				DataSet ds1=new DataSet();
				DataTable dtcustomer1;
				DataView dv1=new DataView();
				if(sqlstr1!="")
				{
					SqlDataAdapter dal=new SqlDataAdapter(sqlstr1,sqlcon);
					dal.Fill(ds1,"purchase_master");
					dtcustomer1=ds1.Tables["purchase_master"];
					dv1=new DataView(dtcustomer1);
					dv1.Sort=strorderby;
					Cache["strorderby"]=strorderby;
					PurchaseGrid.DataSource=dv1;
					//PurchaseGrid.Visible=true;
					//lblPurchaseHeading.Visible=true;
				}
				if(dv1.Count!=0)
				{
					PurchaseGrid.DataBind();
					PurchaseGrid.Visible=true;
					lblPurchaseHeading.Visible=true;
				}
				else
				{
					PurchaseGrid.Visible=false;
					lblPurchaseHeading.Visible=false;
					p++;

				}
				sqlcon.Dispose();
				if(p>0 && s>0)
				{
					MessageBox.Show("Data not available");
				}
				sqlcon.Dispose();
			}*/

		/// <summary>
		/// this is used to show the report with the help of Bindthedata1() and Bindthedata2() function and set 
		/// the column name with ascending order in session variable.
		/// </summary>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			//**int s = 0;
			//**	int p = 0;
			//**	SqlDataReader SqlDtr= null;
			try
			{
				if(!checkValidity())
				{
					return;
				}
				string str="";
				string str1="";

				if(RadDetails.Checked)
				{
					// if user select the report type Sales Report or Both then fire the query and fetch display the sales invoice with VAT.
					if(DropReportType.SelectedItem.Text == "Sales Report" || DropReportType.SelectedItem.Text == "Both")
					{
						//					dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, cast(Cash_Discount as varchar)+(case when cash_disc_type = 'Per' then '%' else ' Rs.' end) as Cash_Disc, VAT_Amount, cast(Discount as varchar)+(case when discount_type = 'Per' then '%' else ' Rs.' end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						if(DropReportCategory.SelectedItem.Text == "VAT")  
						{
							lblSalesHeading.Text = "Detailed VAT Report for Complete Party Wise/ Invoice Wise Sales";
							//**	dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
							//str="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							str="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						}
						if(DropReportCategory.SelectedItem.Text == "Non VAT")  
						{
							lblSalesHeading.Text = "Detailed Non VAT Report for Complete Party Wise/ Invoice Wise Sales";
							//**	dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
							str="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						}
						if(DropReportCategory.SelectedItem.Text == "Both")  
						{
							lblSalesHeading.Text = "Detailed VAT/ Non VAT Report for Complete Party Wise/ Invoice Wise Sales";
							//**	dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
							str="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						}
					}
					else
					{
						Cache["str"]="";
						//**	lblSalesHeading.Visible = false; 
						//**	SalesGrid.Visible=false;
					
					
					}
								
					Cache["str"]=str;
					strorderby1="invoice_no ASC";
					Session["Column"]="invoice_no";
					Session["order"]="ASC";
					Bindthedata1();


					// if user select the report type Purchase Report or Both then fire the query and fetch display the purchase invoice with VAT.
					if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
					{
						if(DropReportCategory.SelectedItem.Text == "VAT")  
						{
							lblPurchaseHeading.Text = "Detailed VAT Report for Complete Party Wise/ Invoice Wise Purchase";
							//	dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
							//Coment by Vikas 31.12.2012 str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							
							str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						}
						if(DropReportCategory.SelectedItem.Text == "Non VAT")  
						{
							lblPurchaseHeading.Text = "Detailed Non VAT Report for Complete Party Wise/ Invoice Wise Purchase";
							//**	dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
							
							//Coment by Vikas 31.12.2012 str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						}
						if(DropReportCategory.SelectedItem.Text == "Both")  
						{
							lblPurchaseHeading.Text = "Detailed VAT/ Non VAT Report for Complete Party Wise/ Invoice Wise Purchase";
							//**dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
							//str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							
							//Coment by Vikas 31.12.2012  str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							 str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						}
					}
					else
					{
						Cache["str1"]="";
						//**lblPurchaseHeading.Visible = false; 
						//**PurchaseGrid.Visible=false;
					}
					Cache["str1"]=str1;
					strorderby2="Vndr_Invoice_No ASC";
					Session["Column"]="Vndr_Invoice_No";
					Session["order"]="ASC";
					Bindthedata2();
				}
				else
				{
					tempMonth=new ArrayList();
					PurchaseGrid.Visible=false;
					SalesGrid.Visible=false;
					lblSalesHeading.Visible=false;
					lblPurchaseHeading.Visible=false;
					ArrayList Month = new ArrayList();
					getMonth();
				}
				//**	if(p>0 && s>0)
				//**	{
				//**		MessageBox.Show("Data not available");
				//**	}
				//**	else if(s>0 && DropReportType.SelectedItem.Text != "Both")
				//**		MessageBox.Show("Sales Data not available");
				//**	    else if(p > 0 && DropReportType.SelectedItem.Text != "Both")
				//**		  MessageBox.Show("Purchase Data not available");

				//				strorderby="invoice_no ASC";
				//				Session["Column"]="invoice_no";
				//				Session["order"]="ASC";
				//				Bindthedata();
					
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:cmdrpt_Click  EXCEPTION  "+ex.Message+"  "+ uid );
			}
		}

		/// <summary>
		/// its creates the file VAT_Report.txt for print.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{

			int s = 0;
			int p = 0;
			SqlDataReader SqlDtr= null;
			
			try
			{

				if(!checkValidity())
				{
					return;
				}
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\VAT_Report.txt";
				StreamWriter sw = new StreamWriter(path);
				string info = "",info1 = "";
				string sql="";
				string sql1="";
				/*
													   ========================================
													   VAT Report From 11/11/2000 To 11/12/2003
													   ========================================

											Detaild VAT Report for Complete Party Wise/ Invoice Wise Purchase 
											-----------------------------------------------------------------
				+-------+----------+-------------------------+--------------+----------+---------+--------+-------+--------+----------+
				|Invoice| Invoice  |    Vendor Name          |    Place     | Tin No.  | Product |  Cash  |  VAT  | Other  |Total Inv.|
				|  No.  |  Date    |                         |              |          |  Value  |Discount|       |Discount| Amount   |
				+-------+----------+-------------------------+--------------+----------+---------+--------+-------+--------+----------+
				 1001    dd/mm/yyyy 1234567890123456789012345 12345679012345 1234567890 123456.88 10.00 Rs.1234.00          1234567.99						  
  
				*/
				if(RadDetails.Checked)
				{
					/*Coment by Vikas 31.12.2012 info = " {0,-7:S} {1,-10:S} {2,-28:S} {3,-18:S} {4,-11:S} {5,12:F} {6,9:F} {7,9:F} {8,9:F} {9,13:F}";
					info1 = " {0,-10:S} {1,-10:S} {2,-25:S} {3,-18:S} {4,-11:S} {5,12:F} {6,9:F} {7,9:F} {8,9:F} {9,13:F}";*/

					info = " {0,-7:S} {1,-10:S} {2,-28:S} {3,-18:S} {4,-11:S} {5,12:F} {6,9:F} {7,9:F} {8,13:F}";
					info1 = " {0,-10:S} {1,-10:S} {2,-19:S} {3,-11:S} {4,-11:S} {5,15:F} {6,11:F} {7,11:F} {8,13:F} {9,13:F}";

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
					string des="-----------------------------------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("VAT Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("========================================",des.Length));
			
					//if type is Sales Report or both then writes the information about sales .
					if(DropReportType.SelectedItem.Text == "Sales Report" || DropReportType.SelectedItem.Text == "Both")
					{
						grand_total = 0;
						cash_discount = 0;
						vat_total = 0;
						other_discount = 0;
						net_total = 0;

						//sw.WriteLine("");
						if(DropReportCategory.SelectedItem.Text == "VAT")  
						{
						
							sw.WriteLine("                             Detaild VAT Report for Complete Party Wise/ Invoice Wise Sales");
							sw.WriteLine("		             --------------------------------------------------------------");
							//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql=sql+" order by "+Cache["strorderby1"];
							dbobj.SelectQuery(sql,ref SqlDtr);
							//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						}
						if(DropReportCategory.SelectedItem.Text == "Non VAT")  
						{
							sw.WriteLine("                             Detaild Non VAT Report for Complete Party Wise/ Invoice Wise Sales");
							sw.WriteLine("		             ------------------------------------------------------------------");
							//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql=sql+" order by "+Cache["strorderby1"];
							dbobj.SelectQuery(sql,ref SqlDtr);
							//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						}
						if(DropReportCategory.SelectedItem.Text == "Both")  
						{
							sw.WriteLine("                             Detaild VAT/ Non VAT Report for Complete Party Wise/ Invoice Wise Sales");
							sw.WriteLine("		             -----------------------------------------------------------------------");
					 
							//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql=sql+" order by "+Cache["strorderby1"];
							dbobj.SelectQuery(sql,ref SqlDtr);
							//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						}

 
						if(SqlDtr.HasRows)
						{
					
							/* Coment By Vikas 31.12.2012 sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
							sw.WriteLine("|Invoice| Invoice  |      Customer Name         |      Place       |  Tin No.  |  Product   |  Cash   | Other   |   VAT   | Total Inv.  |");
							sw.WriteLine("|  No.  |  Date    |                            |                  |           |   Value    |Discount |Discount |         |  Amount     |");
							sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
							//						               1234567 1234567890 1234567890123456789012345678 123456789012345678 12345678901 123456789012 123456789 123456789 123456789 1234567890123	*/
							
							
							sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+-------------+");
							sw.WriteLine("|Invoice| Invoice  |      Customer Name         |      Place       |  Tin No.  |  Product   | Total   |   VAT   | Total Inv.  |");
							sw.WriteLine("|  No.  |  Date    |                            |                  |           |   Value    |Discount |         |  Amount     |");
							sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+-------------+");
							//	           1234567 1234567890 1234567890123456789012345678 123456789012345678 12345678901 123456789012 123456789 123456789 1234567890123
							

							while(SqlDtr.Read())
							{
								sw.WriteLine(info,SqlDtr["Invoice_No"].ToString().Trim() ,
									GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Invoice_Date"].ToString().Trim() ) ),
									GenUtil.TrimLength(SqlDtr["Cust_Name"].ToString().Trim(),28),
									GenUtil.TrimLength(SqlDtr["City"].ToString().Trim(),18),
									SqlDtr["Tin_No"].ToString().Trim(),
									Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi) ,
									//Coment By Vikas 31.12.2012 Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi) ,
									//Coment By Vikas 31.12.2012 Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi),              
									GetSaleDiscount(SqlDtr["Invoice_No"].ToString().Trim()),
									Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi), 
									Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi)  
									);
								grand_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi));  
								cash_discount  += System.Convert.ToDouble(Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi));  
								vat_total += System.Convert.ToDouble(Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi));  
								other_discount += System.Convert.ToDouble(Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi));  
								net_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi));  
								              
							}

							sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+-------------+");
							//Coment by vikas 31.12.2012 sw.WriteLine(info ,"Total:","","","","",grand_total.ToString("N",nfi),cash_discount.ToString("N",nfi),other_discount.ToString("N",nfi),vat_total.ToString("N",nfi) ,net_total.ToString("N",nfi)); 
							sw.WriteLine(info ,"Total:","","","","",Math.Round(double.Parse(grand_total.ToString("N",nfi))),"",Math.Round(double.Parse(Total.ToString("N",nfi))),Math.Round(double.Parse(vat_total.ToString("N",nfi))),Math.Round(double.Parse(net_total.ToString("N",nfi))));
							sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+-------------+");

					
						}
						else
						{
						
							s++;
						}
						SqlDtr.Close ();
					}
			

					//if type is Purchase Report or both then writes the information about purchase .
					if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
					{
						grand_total = 0;
						cash_discount = 0;
						vat_total = 0;
						other_discount = 0;
						net_total = 0;
						//sw.WriteLine("");
						if(DropReportCategory.SelectedItem.Text == "VAT")  
						{
					
							sw.WriteLine("                             Detaild VAT Report for Complete Party Wise/ Invoice Wise Purchase");
							sw.WriteLine("		             -----------------------------------------------------------------");
					
							//sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							//Coment By Vikas 31.12.2012 sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							sql1=sql1+" order by "+Cache["strorderby2"];
							dbobj.SelectQuery(sql1,ref SqlDtr);
							//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						}
						if(DropReportCategory.SelectedItem.Text == "Non VAT")  
						{
							sw.WriteLine("                             Detaild Non VAT Report for Complete Party Wise/ Invoice Wise Purchase");
							sw.WriteLine("		             ---------------------------------------------------------------------");
							//sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							//Coment By Vikas 31.12.2012 sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							
							sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";

							sql1=sql1+" order by "+Cache["strorderby2"];
							dbobj.SelectQuery(sql1,ref SqlDtr);
							//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						}
						if(DropReportCategory.SelectedItem.Text == "Both")  
						{
							sw.WriteLine("                             Detaild VAT/ Non VAT Report for Complete Party Wise/ Invoice Wise Purchase");
							sw.WriteLine("		             --------------------------------------------------------------------------");
							//sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
							//Coment By Vikas 31.12.2012 sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";

							sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";

							sql1=sql1+" order by "+Cache["strorderby2"];
							dbobj.SelectQuery(sql1,ref SqlDtr);
							//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
						}
						if(SqlDtr.HasRows)
						{
					
							/*sw.WriteLine("+----------+----------+-------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
							sw.WriteLine("| Invoice  | Invoice  |       Vendor Name       |      Place       |  Tin No.  |  Product   |  Cash   | Other   |  VAT    |  Total Inv. |");
							sw.WriteLine("|   No.    |  Date    |                         |                  |           |   Value    |Discount |Discount |         |   Amount    |");
							sw.WriteLine("+----------+----------+-------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
							//             1234567890 dd/mm/yyyy 1234567890123456789012345 123456789012345678 12345678901 123456789012 123456789 123456789 123467890 1234567890123*/

							sw.WriteLine("+----------+----------+-------------------+-----------+------------+--------------+-----------+-----------+------------+-------------+");
							sw.WriteLine("| Invoice  | Invoice  |   Vendor Name     |   Place   |  Tin No.   |   Product    |   Entry   |   Total   |    VAT     |  Total Inv. |");
							sw.WriteLine("|   No.    |  Date    |                   |           |            |    Value     |    Tax    | Discount  |            |   Amount    |");
							sw.WriteLine("+----------+----------+-------------------+-----------+------------+--------------+-----------+-----------+------------+-------------+");
							//             1234567890 dd/mm/yyyy 1234567890123456789 12345678901 12345678901 123456789012345 12345678901 12345678901 123467890123 1234567890123
							

							while(SqlDtr.Read())
							{
								sw.WriteLine(info1,SqlDtr["Vndr_Invoice_No"].ToString().Trim() ,
									GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Invoice_Date"].ToString().Trim())),
									GenUtil.TrimLength(SqlDtr["Supp_Name"].ToString().Trim(),25) ,
									GenUtil.TrimLength(SqlDtr["City"].ToString().Trim(),19),
									SqlDtr["Tin_No"].ToString().Trim(),
									Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi) ,
									//Coment By Vikas 31.12.2012 Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi) ,
									//Coment By Vikas 31.12.2012 Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi),
									SqlDtr["Entry_Tax1"].ToString().Trim(),
									GetPurDiscount(SqlDtr["Vndr_Invoice_No"].ToString().Trim()),
									Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi), 
									Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi)  
									);
								grand_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi));  
								//Coment by vikas 31.12.2012 cash_discount  += System.Convert.ToDouble(Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi));  
								cash_discount  += System.Convert.ToDouble(Double.Parse(SqlDtr["Entry_Tax1"].ToString().Trim()).ToString("N",nfi));  
								vat_total += System.Convert.ToDouble(Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi));  
								//Coment by vikas 31.12.2012 other_discount += System.Convert.ToDouble(Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi));  
								//other_discount += System.Convert.ToDouble(Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi));  
								net_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi));  
							}
							sw.WriteLine("+----------+----------+-------------------+-----------+------------+--------------+-----------+-----------+------------+-------------+");
							//Coment By Vikas 31.12.2012 sw.WriteLine(info1 ,"Total:","","","","",grand_total.ToString("N",nfi),cash_discount.ToString("N",nfi),other_discount.ToString("N",nfi),vat_total.ToString("N",nfi) ,net_total.ToString("N",nfi)); 
							sw.WriteLine(info1 ,"Total:","","","","",Math.Round(double.Parse(grand_total.ToString("N",nfi))),Math.Round(double.Parse(cash_discount.ToString("N",nfi))),Math.Round(double.Parse(Total_Pur.ToString("N",nfi))),Math.Round(double.Parse(vat_total.ToString("N",nfi))) ,Math.Round(double.Parse(net_total.ToString("N",nfi)))); 
							sw.WriteLine("+----------+----------+-------------------+-----------+------------+--------------+-----------+-----------+------------+-------------+");
						}
						else
						{
					
							p++;
						}
						SqlDtr.Close ();
					}
				

					if(p>0 && s>0)
					{
						MessageBox.Show("Data not available");
						sw.Close(); 
						return;
					}
					else if(s>0 && DropReportType.SelectedItem.Text != "Both")
						MessageBox.Show("Sales Data not available");
					else if(p > 0 && DropReportType.SelectedItem.Text != "Both")
						MessageBox.Show("Purchase Data not available");
				}
				else
				{
					if(tempMonth.Count>0)
					{
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

						info1 = " {0,-65:S} {1,27:S}";
						string des="--------------------------------------------------------------------------------------------------";
						string Address=GenUtil.GetAddress();
						string[] addr=Address.Split(new char[] {':'},Address.Length);
						sw.WriteLine(info1," Firm Name : "+addr[0].ToString()+", "+addr[6].ToString(),"Tin No. : "+addr[3].ToString());
						sw.WriteLine();
						sw.WriteLine("Period : "+getFromMonth(txtDateFrom.Text)+" To "+getToMonth(txtDateTo.Text));
						sw.WriteLine(des);
						sw.WriteLine(GenUtil.GetCenterAddr("VAT Tax Calculation for Quarterly Return Purpose",des.Length));
						sw.WriteLine(des);
						sw.WriteLine("+----------+-----------------------------------------+--------------------------------------------+");
						sw.WriteLine("|   Month  |             Sales Accounts              |             Purchase Accounts              |");
						sw.WriteLine("+----------+-----------------------------------------+--------------------------------------------+");
						sw.WriteLine("|          |    Sales Amount    | Vat Output On Sale |  Purchase Amount   | Vat Input On Purchase |");
						sw.WriteLine("+----------+--------------------+--------------------+--------------------+-----------------------+");
						//string  =  " 1234567890 12345678901234567890 12345678901234567890 12345678901234567890 12345678901234567890123"; 
						info = "|{0,-10:S}|{1,20:S}|{2,20:S}|{3,20:S}|{4,23:S}|";
					
						for(int i=0;i<tempMonth.Count;i++)
						{
							sw.WriteLine(info,tempMonth[i].ToString(),getSales(getMonthName(tempMonth[i].ToString())),getVatSale(getMonthName(tempMonth[i].ToString())),getPurchase(getMonthName(tempMonth[i].ToString())),getVatPurchase(getMonthName(tempMonth[i].ToString())));
						}
						sw.WriteLine("+----------+--------------------+--------------------+--------------------+-----------------------+");
						sw.WriteLine(info," Total",GenUtil.strNumericFormat(Total_Sales.ToString()),GenUtil.strNumericFormat(Total_SalVat.ToString()),GenUtil.strNumericFormat(Total_Purchase.ToString()),GenUtil.strNumericFormat(Total_PurVar.ToString()));
						sw.WriteLine("+----------+--------------------+--------------------+--------------------+-----------------------+");
						string info2 = " {0,-10:S} {1,-40:S} {2,15:S}";
						if(txtValue.Text!="")
							sw.WriteLine(info2,"","Opening Input Rebate as on "+txtDateFrom.Text,GenUtil.strNumericFormat(txtValue.Text));
						else
							sw.WriteLine(info2,"","Opening Input Rebate as on "+txtDateFrom.Text,"0.00");
						sw.WriteLine(info2,"","Add : Vat Paid On Purchase",Request.Params.Get("tempTotalPurchase"));
						sw.WriteLine(info2,"","","-------------");
						sw.WriteLine(info2,"","",Request.Params.Get("txtSumPurchase"));
						sw.WriteLine();
						sw.WriteLine(info2,"","Less : Vat Payable On Sales",Request.Params.Get("tempTotalSale"));
						sw.WriteLine(info2,"","","-------------");
						sw.WriteLine(info2,"","Net Vat Payable",Request.Params.Get("txtNetAmount"));
						sw.WriteLine(info2,"","","-------------");
					}
					else
					{
						MessageBox.Show("Please Click The View Button First");
					}
				}
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
				Print(); 
					
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:BtnPrint_Click  EXCEPTION  "+ex.Message+"  "+ uid );
			}
		}
		
		/// <summary>
		/// Prepares the excel report file VatReport.xls for printing.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\VatReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",sql1="";
			//**************
			//if type is Sales Report or both then writes the information about sales .
			if(RadDetails.Checked)
			{
				if(DropReportType.SelectedItem.Text == "Sales Report" || DropReportType.SelectedItem.Text == "Both")
				{
					//grand_total = 0;
					//cash_discount = 0;
					//vat_total = 0;
					//other_discount = 0;
					//net_total = 0;

					//sw.WriteLine("");
					if(DropReportCategory.SelectedItem.Text == "VAT")  
					{
						
						sw.WriteLine("                             Detaild VAT Report for Complete Party Wise/ Invoice Wise Sales\t\t\t\t\t\t\t\t\t");
						//sw.WriteLine("		             --------------------------------------------------------------");
						//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql=sql+" order by "+Cache["strorderby1"];
						dbobj.SelectQuery(sql,ref SqlDtr);
						//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
					}
					if(DropReportCategory.SelectedItem.Text == "Non VAT")  
					{
						sw.WriteLine("                             Detaild Non VAT Report for Complete Party Wise/ Invoice Wise Sales\t\t\t\t\t\t\t\t\t");
						//sw.WriteLine("		             ------------------------------------------------------------------");
						//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql=sql+" order by "+Cache["strorderby1"];
						dbobj.SelectQuery(sql,ref SqlDtr);
						//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
					}
					if(DropReportCategory.SelectedItem.Text == "Both")  
					{
						sw.WriteLine("                             Detaild VAT/ Non VAT Report for Complete Party Wise/ Invoice Wise Sales\t\t\t\t\t\t\t\t\t");
						//sw.WriteLine("		             -----------------------------------------------------------------------");
					 
						//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total-(Discount+SchDiscount+foeDiscountrs))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, ((case when discount_type = 'Per' then (Grand_Total*Discount)/100 else Discount end)+schdiscount+foediscountrs) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql=sql+" order by "+Cache["strorderby1"];
						dbobj.SelectQuery(sql,ref SqlDtr);
						//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
					}

 
					if(SqlDtr.HasRows)
					{
					
						//sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
						//sw.WriteLine("|Invoice| Invoice  |      Customer Name         |      Place       |  Tin No.  |  Product   |  Cash   |   VAT   | Other   | Total Inv.  |");
						//sw.WriteLine("|  No.  |  Date    |                            |                  |           |   Value    |Discount |         |Discount |  Amount     |");
						//sw.WriteLine("+-------+----------+----------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
						//						               1234567 1234567890 1234567890123456789012345678 123456789012345678 12345678901 123456789012 123456789 123456789 123456789 1234567890123	
					
						//Coment by vikas 31.12.2012 sw.WriteLine("Invoice_No\tInvoice_Date\tCustomer Name\tPlace\tTin No\tProduct Value\tCash Discount\tDiscount\tVat\tTotal Invoice Amount");
						sw.WriteLine("Invoice_No\tInvoice_Date\tCustomer Name\tPlace\tTin No\tProduct Value\tTotal Discount\tVat\tTotal Invoice Amount");
						sw.WriteLine();
						while(SqlDtr.Read())
						{
							sw.WriteLine(SqlDtr["Invoice_No"].ToString().Trim()+"\t"+
								GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Invoice_Date"].ToString().Trim() ) )+"\t"+
								SqlDtr["Cust_Name"].ToString().Trim()+"\t"+
								SqlDtr["City"].ToString().Trim()+"\t"+
								SqlDtr["Tin_No"].ToString().Trim()+"\t"+
								Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi)+"\t"+
								//coment by Vikas 31.12.2012 Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi)+"\t"+
								//coment by Vikas 31.12.2012 Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi)+"\t"+
								GetSaleDiscount(SqlDtr["Invoice_No"].ToString().Trim())+"\t"+
								Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi)+"\t"+
								Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi)  
								);
							grand_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi));  
							//Coment By Vikas 31.12.2012 cash_discount  += System.Convert.ToDouble(Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi));  
							vat_total += System.Convert.ToDouble(Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi));  
							//Coment By Vikas 31.12.2012 other_discount += System.Convert.ToDouble(Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi));  
							net_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi));  
  
								              
						}
						//Coment By Vikas 31.12.2012 sw.WriteLine("Total\t\t\t\t\t"+grand_total.ToString("N",nfi)+"\t"+cash_discount.ToString("N",nfi)+"\t"+other_discount.ToString("N",nfi)+"\t"+vat_total.ToString("N",nfi) +"\t"+net_total.ToString("N",nfi)); 
						sw.WriteLine("Total\t\t\t\t\t\t"+grand_total.ToString("N",nfi)+"\t"+vat_total.ToString("N",nfi) +"\t"+net_total.ToString("N",nfi)); 
					}
					else
						MessageBox.Show("Sales Data Not Availale");
					SqlDtr.Close();
				}
				//if type is Purchase Report or both then writes the information about purchase .
				if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
				{
					grand_total = 0;
					cash_discount = 0;
					vat_total = 0;
					other_discount = 0;
					net_total = 0;
					sw.WriteLine("");
					sw.WriteLine("");
					if(DropReportCategory.SelectedItem.Text == "VAT")  
					{
					
						sw.WriteLine("                             Detaild VAT Report for Complete Party Wise/ Invoice Wise Purchase                 ");
						//sw.WriteLine("		             -----------------------------------------------------------------");
					
						//sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						//Coment By Vikas 31.12.2012 sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql1=sql1+" order by "+Cache["strorderby2"];
						dbobj.SelectQuery(sql1,ref SqlDtr);
						//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
					}
					if(DropReportCategory.SelectedItem.Text == "Non VAT")  
					{
						sw.WriteLine("                             Detaild Non VAT Report for Complete Party Wise/ Invoice Wise Purchase                 ");
						//sw.WriteLine("		             ---------------------------------------------------------------------");
						//sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						//Coment By Vikas 31.12.2012 sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql1=sql1+" order by "+Cache["strorderby2"];
						dbobj.SelectQuery(sql1,ref SqlDtr);
						//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount = 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
					}
					if(DropReportCategory.SelectedItem.Text == "Both")  
					{
						sw.WriteLine("                             Detaild VAT/ Non VAT Report for Complete Party Wise/ Invoice Wise Purchase                 ");
						//sw.WriteLine("		             --------------------------------------------------------------------------");
						//sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No,Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						//Coment By Vikas 31.12.2012 sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						sql1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No,Entry_Tax1 from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
						
						sql1=sql1+" order by "+Cache["strorderby2"];
						dbobj.SelectQuery(sql1,ref SqlDtr);
						//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime) <= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)",ref SqlDtr);
					}
					if(SqlDtr.HasRows)
					{
					
						//sw.WriteLine("+----------+----------+-------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
						//sw.WriteLine("| Invoice  | Invoice  |       Vendor Name       |      Place       |  Tin No.  |  Product   |  Cash   |  VAT    | Other   |  Total Inv. |");
						//sw.WriteLine("|   No.    |  Date    |                         |                  |           |   Value    |Discount |         |Discount |   Amount    |");
						//sw.WriteLine("+----------+----------+-------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
						//             1234567890 dd/mm/yyyy 1234567890123456789012345 123456789012345678 12345678901 123456789012 123456789 123456789	123467890 1234567890123
						sw.WriteLine();
						//Coment by vikas 31.12.2012 sw.WriteLine("Invoice_No\tInvoice_Date\tVendor Name\tPlace\tTin No\tProduct Value\tCash Discount\tDiscount\tVat\tTotal Invoice Amount");
						sw.WriteLine("Invoice_No\tInvoice_Date\tVendor Name\tPlace\tTin No\tProduct Value\tEntry Tax\tTotal Discount\tVat\tTotal Invoice Amount");
					
						while(SqlDtr.Read())
						{
							sw.WriteLine(SqlDtr["Vndr_Invoice_No"].ToString().Trim()+"\t"+
								GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Invoice_Date"].ToString().Trim()))+"\t"+
								SqlDtr["Supp_Name"].ToString().Trim()+"\t"+
								SqlDtr["City"].ToString().Trim()+"\t"+
								SqlDtr["Tin_No"].ToString().Trim()+"\t"+
								Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi)+"\t"+
								//Coment by Vikas 31.12.2012 Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi)+"\t"+
								//Coment by Vikas 31.12.2012 Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi)+"\t"+
								SqlDtr["Entry_Tax1"].ToString().Trim()+"\t"+
								GetPurDiscount(SqlDtr["Vndr_Invoice_No"].ToString().Trim())+"\t"+
								Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi)+"\t"+
								Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi)  
								);
							grand_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Grand_Total"].ToString().Trim()).ToString("N",nfi));  
							//Coment By Vikas 31.12.2012 cash_discount  += System.Convert.ToDouble(Double.Parse(SqlDtr["Cash_Disc"].ToString().Trim()).ToString("N",nfi));  
							cash_discount  += System.Convert.ToDouble(Double.Parse(SqlDtr["Entry_Tax1"].ToString().Trim()).ToString("N",nfi));  
							vat_total += System.Convert.ToDouble(Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi));  
							//Coment By Vikas 31.12.2012 other_discount += System.Convert.ToDouble(Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi));  
							net_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi));  
								              
						}
						//Coment By Vikas 31.12.2012 sw.WriteLine("Total\t\t\t\t\t"+grand_total.ToString("N",nfi)+"\t"+cash_discount.ToString("N",nfi)+"\t"+other_discount.ToString("N",nfi)+"\t"+vat_total.ToString("N",nfi)+"\t"+net_total.ToString("N",nfi)); 
						sw.WriteLine("Total\t\t\t\t\t\t"+grand_total.ToString("N",nfi)+"\t"+cash_discount.ToString("N",nfi)+"\t"+vat_total.ToString("N",nfi)+"\t"+net_total.ToString("N",nfi)); 
						//sw.WriteLine("+----------+----------+-------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
						//sw.WriteLine(info1 ,"Total:","","","","",grand_total.ToString("N",nfi),cash_discount.ToString("N",nfi),vat_total.ToString("N",nfi) ,other_discount.ToString("N",nfi),net_total.ToString("N",nfi)); 
						//sw.WriteLine("+----------+----------+-------------------------+------------------+-----------+------------+---------+---------+---------+-------------+");
					}
					else
						MessageBox.Show("Purchase Data Not Available");
					SqlDtr.Close ();
				}
			}
			else
			{
				
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(" Firm Name\t"+addr[0].ToString()+", "+addr[6].ToString(),"\tTin No.\t"+addr[3].ToString());
				sw.WriteLine();
				sw.WriteLine("Period\t"+txtDateFrom.Text+" To "+txtDateTo.Text);
				sw.WriteLine();
				sw.WriteLine("VAT Tax Calculation for Quarterly Return Purpose");
				sw.WriteLine();
				sw.WriteLine("Month\tSals Account\t\tPurchase Account");
				sw.WriteLine("\tSales Amount\tVat Output On Sales\tPurchase Amount\tVat Input On Purchase");
				for(int i=0;i<tempMonth.Count;i++)
				{
					sw.WriteLine(tempMonth[i].ToString()+"\t"+getSales(getMonthName(tempMonth[i].ToString()))+"\t"+getVatSale(getMonthName(tempMonth[i].ToString()))+"\t"+getPurchase(getMonthName(tempMonth[i].ToString()))+"\t"+getVatPurchase(getMonthName(tempMonth[i].ToString())));
				}
				sw.WriteLine(" Total"+"\t"+Total_Sales.ToString()+"\t"+Total_SalVat.ToString()+"\t"+Total_Purchase.ToString()+"\t"+Total_PurVar.ToString());
				sw.WriteLine();
				sw.WriteLine();
				if(txtValue.Text!="")
					sw.WriteLine("Opening Input Rebate as on "+txtDateFrom.Text+"\t"+txtValue.Text);
				else
					sw.WriteLine("Opening Input Rebate as on "+txtDateFrom.Text+"\t"+"0.00");
				sw.WriteLine("Add : Vat Paid On Purchase\t"+Request.Params.Get("tempTotalPurchase"));
				sw.WriteLine("\t-------------");
				sw.WriteLine("\t"+Request.Params.Get("txtSumPurchase"));
				sw.WriteLine("Less : Vat Payable On Sales\t"+Request.Params.Get("tempTotalSale"));
				sw.WriteLine("\t-------------");
				sw.WriteLine("Net Vat Payable\t"+Request.Params.Get("txtNetAmount"));
				sw.WriteLine("\t-------------");
			}
			//**************
			sw.Close();
		}
		
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

		/// <summary>
		/// sends the VAT_Report to print server.
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
					CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\VAT_Report.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					//CreateLogFiles.ErrorLog("Form:Vehiclereport.aspx,Method:print"+ "  Daily sales record  Printed   userid  "+uid);
					CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		private void DropReportCategory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Prepares the report file VatReport.txt for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(SalesGrid.Visible==true || PurchaseGrid.Visible==true || RadSummarized.Checked==true)
				{
					//if(tempMonth.Count>0)
					//{
						ConvertToExcel();
						MessageBox.Show("Successfully Convert File Into Excel Format");
						CreateLogFiles.ErrorLog("Form:Vat_Report.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Vat_Report Convert Into Excel Format, userid  "+uid);
					//}
					//else
					//{
				//		MessageBox.Show("Please Click The View Button First");
					//}
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
				CreateLogFiles.ErrorLog("Form:Vat_Report.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Vat_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This Method get only month to select the date from datepicker
		/// </summary>
		public void getMonth()
		{
			string[] FromDate = txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length);
			string[] ToDate = txtDateTo.Text.Split(new char[] {'/'},txtDateTo.Text.Length);
			string[] Mon = {"","Jan.","Feb.","Mar.","Apr.","May.","Jun.","Jul.","Aug.","Sep.","Oct.","Nov.","Dec."};
			if(FromDate[2].ToString()==ToDate[2].ToString())
			{
				while(int.Parse(FromDate[1])<=int.Parse(ToDate[1]))
				{
					tempMonth.Add(Mon[int.Parse(FromDate[1])]+FromDate[2].ToString());
					FromDate[1]=System.Convert.ToString(int.Parse(FromDate[1])+1);
				}
			}
			else if(int.Parse(FromDate[2])+1==int.Parse(ToDate[2]))
			{
				int i=1;
				while(int.Parse(FromDate[1])<=12)
				{
					tempMonth.Add(Mon[int.Parse(FromDate[1])]+FromDate[2].ToString());
					FromDate[1]=System.Convert.ToString(int.Parse(FromDate[1])+1);
				}
				while(i<=int.Parse(ToDate[1]))
				{
					tempMonth.Add(Mon[i]+ToDate[2].ToString());
					i++;
				}
			}
			else
			{
				MessageBox.Show("Invalid Date Selection");
			}
		}
		
		/// <summary>
		/// This method return the sales amount in given period.
		/// </summary>
		public string getSales(string mon)
		{
			InventoryClass obj = new InventoryClass();
			double tot=0;
			string[] Month = mon.Split(new char[] {':'},mon.Length);
			string StartDate=Month[0]+"/1/"+Month[1];
			int day=DateTime.DaysInMonth(int.Parse(Month[1]),int.Parse(Month[0]));
			string EndDate=Month[0]+"/"+day+"/"+Month[1];
			SqlDataReader rdr = obj.GetRecordSet("Select sum(net_amount-Vat_Amount) from sales_master where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString());
			}
			Total_Sales+=tot;
			return GenUtil.strNumericFormat(tot.ToString());
		}

		/// <summary>
		/// This method return the vat sales amount in given period.
		/// </summary>
		public string getVatSale(string mon)
		{
			InventoryClass obj = new InventoryClass();
			double tot=0;
			string[] Month = mon.Split(new char[] {':'},mon.Length);
			string StartDate=Month[0]+"/1/"+Month[1];
			int day=DateTime.DaysInMonth(int.Parse(Month[1]),int.Parse(Month[0]));
			string EndDate=Month[0]+"/"+day+"/"+Month[1];
			SqlDataReader rdr = obj.GetRecordSet("Select sum(Vat_Amount) from sales_master where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString());
			}
			Total_SalVat+=tot;
			return GenUtil.strNumericFormat(tot.ToString());
		}

		/// <summary>
		/// This method return the vat purchase amount in given period.
		/// </summary>
		public string getVatPurchase(string mon)
		{
			InventoryClass obj = new InventoryClass();
			double tot=0;
			string[] Month = mon.Split(new char[] {':'},mon.Length);
			string StartDate=Month[0]+"/1/"+Month[1];
			int day=DateTime.DaysInMonth(int.Parse(Month[1]),int.Parse(Month[0]));
			string EndDate=Month[0]+"/"+day+"/"+Month[1];
			SqlDataReader rdr = obj.GetRecordSet("Select sum(Vat_Amount) from Purchase_master where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString());
			}
			Total_PurVar+=tot;
			return GenUtil.strNumericFormat(tot.ToString());
		}

		/// <summary>
		/// This method return the purchase amount in given period.
		/// </summary>
		public string getPurchase(string mon)
		{
			InventoryClass obj = new InventoryClass();
			double tot=0;
			//string[] FromDay=txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length);
			//string[] ToDay=txtDateTo.Text.Split(new char[] {'/'},txtDateTo.Text.Length);
			string[] Month = mon.Split(new char[] {':'},mon.Length);
			string StartDate=Month[0]+"/1/"+Month[1];
			int day=DateTime.DaysInMonth(int.Parse(Month[1]),int.Parse(Month[0]));
			string EndDate=Month[0]+"/"+day+"/"+Month[1];
			//string EndDate=Month[0]+"/"+ToDay[0]+"/"+Month[1];
			SqlDataReader rdr = obj.GetRecordSet("Select sum(Net_Amount-Vat_Amount) from Purchase_master where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate+"'");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString());
			}
			Total_Purchase+=tot;
			return GenUtil.strNumericFormat(tot.ToString());
		}

		/// <summary>
		/// This method return the only month name.
		/// </summary>
		public string getMonthName(string Mon)
		{
			string[] Month = Mon.Split(new char[] {'.'},Mon.Length);
			if(Month[0].ToString()=="Jan")
				return "1:"+Month[1].ToString();
			else if(Month[0].ToString()=="Feb")
				return "2:"+Month[1].ToString();
			else if(Month[0].ToString()=="Mar")
				return "3:"+Month[1].ToString();
			else if(Month[0].ToString()=="Apr")
				return "4:"+Month[1].ToString();
			else if(Month[0].ToString()=="May")
				return "5:"+Month[1].ToString();
			else if(Month[0].ToString()=="Jun")
				return "6:"+Month[1].ToString();
			else if(Month[0].ToString()=="Jul")
				return "7:"+Month[1].ToString();
			else if(Month[0].ToString()=="Aug")
				return "8:"+Month[1].ToString();
			else if(Month[0].ToString()=="Sep")
				return "9:"+Month[1].ToString();
			else if(Month[0].ToString()=="Oct")
				return "10:"+Month[1].ToString();
			else if(Month[0].ToString()=="Nov")
				return "11:"+Month[1].ToString();
			else if(Month[0].ToString()=="Dec")
				return "12:"+Month[1].ToString();
			else
				return "0:0";
		}

		protected void RadSummarized_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadDetails.Checked)
				PanReport.Visible=true;
			else
				PanReport.Visible=false;
		}

		protected void RadDetails_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadDetails.Checked)
				PanReport.Visible=true;
			else
				PanReport.Visible=false;
		}

		/// <summary>
		/// This method return the month name with year in given from date.
		/// </summary>
		public string getFromMonth(string Mon)
		{
			string[] Month = Mon.Split(new char[] {'/'},Mon.Length);
			if(Month[0].ToString()=="01" || Month[0].ToString()=="1")
			{
				if(Month[1].ToString()=="1" || Month[1].ToString()=="01")
					return "January "+Month[2];
				else if(Month[1].ToString()=="2" || Month[1].ToString()=="02")
					return "February "+Month[2];
				else if(Month[1].ToString()=="3" || Month[1].ToString()=="03")
					return "March "+Month[2];
				else if(Month[1].ToString()=="4" || Month[1].ToString()=="04")
					return "April "+Month[2];
				else if(Month[1].ToString()=="5" || Month[1].ToString()=="05")
					return "May "+Month[2];
				else if(Month[1].ToString()=="6" || Month[1].ToString()=="06")
					return "June "+Month[2];
				else if(Month[1].ToString()=="7" || Month[1].ToString()=="07")
					return "July "+Month[2];
				else if(Month[1].ToString()=="8" || Month[1].ToString()=="08")
					return "August "+Month[2];
				else if(Month[1].ToString()=="9" || Month[1].ToString()=="09")
					return "September "+Month[2];
				else if(Month[1].ToString()=="10")
					return "October "+Month[2];
				else if(Month[1].ToString()=="11")
					return "November "+Month[2];
				else if(Month[1].ToString()=="12")
					return "December "+Month[2];
				else
					return Mon;
			}
			else
				return Mon;
		}

		/// <summary>
		/// This method return the month name with year in given to date.
		/// </summary>
		public string getToMonth(string Mon)
		{
			string[] Month =Mon.IndexOf("/")>0? Mon.Split(new char[] {'/'},Mon.Length): Mon.Split(new char[] { '-' }, Mon.Length);
			int day=DateTime.DaysInMonth(int.Parse(Month[2]),int.Parse(Month[1]));
			if(Month[0].ToString()==day.ToString())
			{
				if(Month[1].ToString()=="1" || Month[1].ToString()=="01")
					return "January "+Month[2];
				else if(Month[1].ToString()=="2" || Month[1].ToString()=="02")
					return "February "+Month[2];
				else if(Month[1].ToString()=="3" || Month[1].ToString()=="03")
					return "March "+Month[2];
				else if(Month[1].ToString()=="4" || Month[1].ToString()=="04")
					return "April "+Month[2];
				else if(Month[1].ToString()=="5" || Month[1].ToString()=="05")
					return "May "+Month[2];
				else if(Month[1].ToString()=="6" || Month[1].ToString()=="06")
					return "June "+Month[2];
				else if(Month[1].ToString()=="7" || Month[1].ToString()=="07")
					return "July "+Month[2];
				else if(Month[1].ToString()=="8" || Month[1].ToString()=="08")
					return "August "+Month[2];
				else if(Month[1].ToString()=="9" || Month[1].ToString()=="09")
					return "September "+Month[2];
				else if(Month[1].ToString()=="10")
					return "October "+Month[2];
				else if(Month[1].ToString()=="11")
					return "November "+Month[2];
				else if(Month[1].ToString()=="12")
					return "December "+Month[2];
				else 
					return Mon;
			}
			else
				return Mon;
		}
	}
}