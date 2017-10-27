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
	public partial class Return_Report : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
		public double Qty_total = 0;
		public double VAT_total = 0;
		public double NET_total = 0;
		public double Ltr_Total = 0;
		public double Total_Sales=0;
		public double Total_Purchase=0;
		public double Total_PurVar=0;
		public double Total_SalVat=0;
		public string tempFromDate="",tempToDate="";
		public static ArrayList tempMonth = new ArrayList();
		public double other_discount = 0;
		protected System.Web.UI.WebControls.TextBox txtValue;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSumPurchase;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tempTotalPurchase;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tempTotalSale;
		protected System.Web.UI.HtmlControls.HtmlInputText txtNetAmount;
		protected System.Web.UI.WebControls.RadioButton RadDetails;
		protected System.Web.UI.WebControls.RadioButton RadSummarized;
		protected System.Web.UI.WebControls.DropDownList DropReportCategory;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Panel PanReport;
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
					string SubModule="55";
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
                /*if(txtValue.Text!="")
					Total_PurVar+=double.Parse(txtValue.Text);*/
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
			strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length) : str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// This increment the grand total by the passing value.
		/// </summary>
		protected void QtyTotal(double _grandtotal)
		{
			Qty_total += _grandtotal; 
		}

		/// <summary>
		/// This increment the vat total by passing value.
		/// </summary>
		protected void VATTotal(double _vattotal)
		{
			VAT_total  += _vattotal; 
		}
		
		/// <summary>
		/// This increment the net total by passing value.
		/// </summary>
		protected void NetTotal(double _nettotal)
		{
			NET_total  += _nettotal; 
		}

		/// <summary>
		/// This invrement the cash discount by passing value.
		/// </summary>
		protected void LtrTotal(double _cashdiscount)
		{
			Ltr_Total  += _cashdiscount; 
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
				QtyTotal(Double.Parse(e.Item.Cells[4].Text));
				LtrTotal(Double.Parse(e.Item.Cells[5].Text)); 
				//VATTotal(Double.Parse(e.Item.Cells[7].Text));
				//OtherDiscount(Double.Parse(e.Item.Cells[8].Text));
				//NetTotal(Double.Parse(e.Item.Cells[8].Text));
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
				e.Item.Cells[4].Text =Qty_total.ToString("N",nfi);   
				e.Item.Cells[5].Text = Ltr_Total.ToString("N",nfi); 
				//e.Item.Cells[7].Text = vat_total.ToString("N",nfi);  
				//e.Item.Cells[8].Text = net_total.ToString("N",nfi);
				//e.Item.Cells[9].Text = net_total.ToString("N",nfi);
				Qty_total = 0;
				Ltr_Total = 0;
				//vat_total = 0;
				//other_discount = 0;
				//net_total = 0;
			}
		}

		/// <summary>
		/// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		/// </summary>
		public void ItemTotal1(object sender,DataGridItemEventArgs e)
		{
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem )  )
			{
				QtyTotal(Double.Parse(e.Item.Cells[4].Text));
				LtrTotal(Double.Parse(e.Item.Cells[5].Text)); 
				//VATTotal(Double.Parse(e.Item.Cells[7].Text));
				//OtherDiscount(Double.Parse(e.Item.Cells[8].Text));
				//NetTotal(Double.Parse(e.Item.Cells[8].Text));
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
               
				e.Item.Cells[4].Text =Qty_total.ToString("N",nfi);   
				e.Item.Cells[5].Text = Ltr_Total.ToString("N",nfi); 
				//e.Item.Cells[7].Text = vat_total.ToString("N",nfi);  
				//e.Item.Cells[8].Text = net_total.ToString("N",nfi);
				//e.Item.Cells[9].Text = net_total.ToString("N",nfi);
				Qty_total = 0;
				Ltr_Total = 0;
				//vat_total = 0;
				//other_discount = 0;
				//net_total = 0;
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
			if(DropReportType.SelectedItem.Text == "Sales Return" || DropReportType.SelectedItem.Text == "Both")
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
					da.Fill(ds,"View_Sales_Return");
					dtcustomer=ds.Tables["View_Sales_Return"];
					dv=new DataView(dtcustomer);
					dv.Sort=strorderby1;
					Cache["strorderby1"]=strorderby1;
					SalesGrid.DataSource=dv;
				}
				if(dv.Count!=0)
				{
					SalesGrid.DataBind();
					SalesGrid.Visible=true;
					lblSales.Visible=true;
					lblSales.Text="Sales Return";
				}
				else
				{
					SalesGrid.Visible=false;
					lblSales.Visible=false;
					lblSales.Text="";
					s++;
				}
				if(s>0 )
				{
					MessageBox.Show("Sales Return Data not Available");
				}
				sqlcon1.Dispose();
			}
			else
			{
				lblSales.Visible = false; 
				SalesGrid.Visible=false;
			}
		}

		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata2()
		{
			if(DropReportType.SelectedItem.Text == "Purchase Return" || DropReportType.SelectedItem.Text == "Both")
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
					dal.Fill(ds1,"View_Purchase_Return");
					dtcustomer1=ds1.Tables["View_Purchase_Return"];
					dv1=new DataView(dtcustomer1);
					dv1.Sort=strorderby2;
					Cache["strorderby2"]=strorderby2;
					PurchaseGrid.DataSource=dv1;
				}
				if(dv1.Count!=0)
				{
					PurchaseGrid.DataBind();
					PurchaseGrid.Visible=true;
					lblPurchas.Visible=true;
					lblPurchas.Text="Purchase Return";
				}
				else
				{
					PurchaseGrid.Visible=false;
					lblPurchas.Visible=false;
					lblPurchas.Text="";
					p++;
				}
				//sqlcon.Dispose();
				if(p>0 )
				{
					MessageBox.Show("Purchase Return Data not Available");
				}
				sqlcon2.Dispose();
			}
			else
			{
				lblPurchas.Visible = false; 
				PurchaseGrid.Visible=false;
			}
		}
		public double am1=0;
		public double am2=0;
		public double am3=0;
		double amt1=0;  
		int count=0,i=0,status=0,Flag=0;
		/// <summary>
		/// This Method is used to multiplies the package quantity with Quantity in every product according 
		/// to given in database.
		/// </summary>
		protected string Multiply1(string inv_no,string inv,string Net_Amount)
		{
			//in_amt=0;
			if(Flag==0)
			{
				Cache["Invoice_No"]=inv_no;
				Flag=1;
			}
			else if(Flag==3)
			{
				Cache["Invoice_No"] = inv_no;
			}
			if(status==0)
			{
				//dbobj.ExecuteScalar("select count(*) from View_Purchase_Return  where Invoice_No="+Cache["Invoice_No"].ToString()+" and cast(floor(cast(entry_time as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(entry_time as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"'",ref count);
				dbobj.ExecuteScalar("select count(*) from View_Sales_Return  where Invoice_No="+Cache["Invoice_No"].ToString(),ref count);
				status=1;
			}
			if(i<count)
			{
				Flag=2;
				i++;
			}
			if(i==count)
			{
				amt1=0;
				amt1=double.Parse(Net_Amount);
				status=0;
				i=0;
				Flag=3;
				count=0;
			}
			else
			{
				amt1=0;
				Flag=4;
			}
			if(Flag==4)
				return " ";
			else if(Flag==3)
			{
				am1+=amt1;
				Cache["Saleam"]=am1;
				return GenUtil.strNumericFormat(amt1.ToString());
			}
			return "";
		}

		protected string Multiply2(string inv_no,string inv,string Net_Amount)
		{
			//in_amt=0;
			if(Flag==0)
			{
				Cache["Invoice_No"]=inv_no;
				Flag=1;
			}
			else if(Flag==3)
			{
				Cache["Invoice_No"] = inv_no;
			}
			if(status==0)
			{
				//dbobj.ExecuteScalar("select count(*) from View_Purchase_Return  where Invoice_No="+Cache["Invoice_No"].ToString()+" and cast(floor(cast(entry_time as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(entry_time as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"'",ref count);
				dbobj.ExecuteScalar("select count(*) from View_Purchase_Return  where Invoice_No="+Cache["Invoice_No"].ToString(),ref count);
				status=1;
			}
			if(i<count)
			{
				Flag=2;
				i++;
			}
			if(i==count)
			{
				amt1=0;
				amt1=double.Parse(Net_Amount);
				status=0;
				i=0;
				Flag=3;
				count=0;
			}
			else
			{
				amt1=0;
				Flag=4;
			}
			if(Flag==4)
				return " ";
			else if(Flag==3)
			{
				am2+=amt1;
				Cache["am"]=am2;
				return GenUtil.strNumericFormat(amt1.ToString());
			}
			return "";
		}

		protected string Multiply3(string inv_no,string inv,string Net_Amount)
		{
			//in_amt=0;
			if(Flag==0)
			{
				Cache["Invoice_No"]=inv_no;
				Flag=1;
			}
			else if(Flag==3)
			{
				Cache["Invoice_No"] = inv_no;
			}
			if(status==0)
			{
				//dbobj.ExecuteScalar("select count(*) from View_Purchase_Return  where Invoice_No="+Cache["Invoice_No"].ToString()+" and cast(floor(cast(entry_time as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(entry_time as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"'",ref count);
				dbobj.ExecuteScalar("select count(*) from View_Purchase_Return  where Invoice_No="+Cache["Invoice_No"].ToString(),ref count);
				status=1;
			}
			if(i<count)
			{
				Flag=2;
				i++;
			}
			if(i==count)
			{
				amt1=0;
				amt1=double.Parse(Net_Amount);
				status=0;
				i=0;
				Flag=3;
				count=0;
			}
			else
			{
				amt1=0;
				Flag=4;
			}
			if(Flag==4)
				return " ";
			else if(Flag==3)
			{
				am3+=amt1;
				Cache["VAT"]=am3;
				return GenUtil.strNumericFormat(amt1.ToString());
			}
			return "";
		}

		/// <summary>
		/// this is used to show the report with the help of Bindthedata1() and Bindthedata2() function and set 
		/// the column name with ascending order in session variable.
		/// </summary>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!checkValidity())
				{
					return;
				}
				string str="";
				string str1="";

				// if user select the report type Sales Report or Both then fire the query and fetch display the sales invoice with VAT.
				if(DropReportType.SelectedItem.Text == "Sales Return" || DropReportType.SelectedItem.Text == "Both")
				{
					//str1="select p.Prod_Name+':'+p.Pack_Type Product ,prm.Invoice_No,Supp_Name,prd.qty,(prd.qty*p.total_qty) Ltr,prd.Rate  from Purchase_Return_Details PRD,Purchase_Return_Master PRM, Products P,purchase_master pm,Supplier S where PRD.Pr_ID=PRM.Pr_id and Prd.Prod_id=p.prod_id and prm.invoice_no=pm.invoice_no and pm.vendor_id=s.Supp_id and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
					str="select Prod_Name+':'+Pack_Type Product,Invoice_No,Cust_Name,qty,(qty*total_qty) Ltr,Rate,entry_time,Net_Amount,VAT_Amount from View_Sales_Return where cast(floor(cast(entry_time as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(entry_time as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by Invoice_No";
				}
				Cache["str"]=str;
				strorderby1="Invoice_No ASC";
				Session["Column"]="Invoice_No";
				Session["order"]="ASC";
				Bindthedata1();
				
				// if user select the report type Purchase Report or Both then fire the query and fetch display the purchase invoice with VAT.
				if(DropReportType.SelectedItem.Text == "Purchase Return" || DropReportType.SelectedItem.Text == "Both")
				{
					//str1="select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then ((Grand_Total+Entry_tax1-(Trade_Discount+foc_Discount+Fixed_Discount+Discount))*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No, Vndr_Invoice_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				str1="select Prod_Name+':'+pack_Type Product,Invoice_No,Supp_Name,qty,(qty*total_qty) Ltr,Rate,entry_time,Vndr_Invoice_No,Net_Amount,VAT_Amount from View_Purchase_Return where cast(floor(cast(entry_time as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(entry_time as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by Invoice_No";
				}
				Cache["str1"]=str1;
				strorderby2="Invoice_No ASC";
				Session["Column"]="Invoice_No";
				Session["order"]="ASC";
				Bindthedata2();
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:VAT_Report.aspx,Method:cmdrpt_Click  EXCEPTION  "+ex.Message+"  "+ uid );
			}
		}

		/// <summary>
		/// This method return the calculated vat.
		/// </summary>
		public string GetVat(string qty,string price)
		{
			double localval=0;
			localval=double.Parse(qty.ToString())*double.Parse(price.ToString());
			localval=(localval*13)/100;
			TotalVat+=localval;
			total_Vat+=localval;
			return System.Convert.ToString(Math.Round(localval,1));
		}

		/// <summary>
		/// This method return the calculated discount.
		/// </summary>
		public double total_SchDisc=0,total_PerDisc=0,total_FleetOe=0,total_Vat=0,total_Total=0,total_TotalAmount=0,total_CashDisc=0;
		public string GetPerDiscount(string ProdName,string PackType,string TotalQty,string TotalRate,string CustID,string SchType)
		{
			//TotalVat=0;
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
				//SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"'");
				SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'");
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
			TotalVat+=double.Parse(TotalRate);
			total_PerDisc+=localval;
			PerDisc=localval;
			TotalVat-=localval;
			return System.Convert.ToString(Math.Round(localval,1));
			
		}
		/// <summary>
		/// This method return the calculated scheme discount.
		/// </summary>
		double TotalVat=0,SchDisc=0,PerDisc=0,FleetoeDisc=0;
		public string GetSchDiscount(string ProdName,string PackType,string TotalQty,string TotalRate,string CustID,string SchType)
		{
			TotalVat=0;
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
				SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'");
				if(rdr.Read())
				{
					localper=rdr["DiscountType"].ToString();
					localval=double.Parse(rdr["discount"].ToString());
				}
				rdr.Close();
				/*if(localper=="%")
					localval=localval*double.Parse(TotalRate)/100;
				else
					localval=localval*double.Parse(TotalQty);*/
				if(localper=="Rs")
					localval=localval*double.Parse(TotalQty);
				else
					localval=0;
			}
			rdr1.Close();
			total_SchDisc+=localval;
			SchDisc=localval;
			TotalVat-=localval;
			return System.Convert.ToString(Math.Round(localval,1));
		}

		/// <summary>
		/// This method is used to calculate the total vat value.
		/// </summary>
		public string GetTotal(string InvoiceNo)
		{
			total_Total+=Math.Round(TotalVat);
			return System.Convert.ToString(Math.Round(TotalVat));
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
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalePurchaseReturn_Report1.txt";
				StreamWriter sw = new StreamWriter(path);
				string info = "",info1 = "";
				string sql="";
				string sql1="";
				
				info = " {0,-7:S} {1,-10:S} {2,-28:S} {3,-37:S} {4,-6:S} {5,6:F} {6,9:F} {7,9:F} {8,13:F}";
				info1 = " {0,-10:S} {1,-10:S} {2,-25:S} {3,-37:S} {4,-6:S} {5,6:F} {6,9:F} {7,9:F} {8,13:F}";

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
				sw.WriteLine(GenUtil.GetCenterAddr("Return Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("========================================",des.Length));
			
				//if type is Sales Report or both then writes the information about sales .
				if(DropReportType.SelectedItem.Text == "Sales Report" || DropReportType.SelectedItem.Text == "Both")
				{
					Qty_total = 0;
					Ltr_Total = 0;
											
					sw.WriteLine("                             Sales Return                                        ");
					sw.WriteLine("---------------------------------------------------------------------------------");
					//sql="select substring(cast(Invoice_No as varchar),4,10) as Invoice_No, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
					sql="select Prod_Name+':'+Pack_Type Product ,Invoice_No,Cust_Name,qty,(qty*total_qty) Ltr,Rate,entry_time,Net_Amount,VAT_Amount from View_Sales_Return where cast(floor(cast(entry_time as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(entry_time as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by invoice_no";
					sql=sql+" order by "+Cache["strorderby1"];
					dbobj.SelectQuery(sql,ref SqlDtr);
					//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, c.Cust_Name, c.City, c.Tin_No from Sales_Master s, Customer c where c.Cust_ID = s.Cust_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'",ref SqlDtr);

 
					if(SqlDtr.HasRows)
					{
					
						sw.WriteLine("+-------+----------+----------------------------+-------------------------------------+------+------+---------+---------+-------------+");
						sw.WriteLine("|Invoice| Invoice  |      Customer Name         |          Product Name               |Qty In|Qty In|  Price  |   VAT   | Total Inv.  |");
						sw.WriteLine("|  No.  |  Date    |                            |                                     | Nos  | Ltr  |         |         |  Amount     |");
						sw.WriteLine("+-------+----------+----------------------------+-------------------------------------+------+------+---------+---------+-------------+");
						//	           1234567 1234567890 1234567890123456789012345678 1234567890123456789012345678901234567 123456 123456 123456789 123456789 1234567890123	
						
						while(SqlDtr.Read())
						{
							sw.WriteLine(info,SqlDtr["Invoice_No"].ToString().Trim() ,
								GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Entry_time"].ToString().Trim() ) ),
								GenUtil.TrimLength(SqlDtr["Cust_Name"].ToString().Trim(),28),
								GenUtil.TrimLength(SqlDtr["Product"].ToString().Trim(),18),
								SqlDtr["Qty"].ToString().Trim(),
								SqlDtr["Ltr"].ToString().Trim(),
								SqlDtr["Rate"].ToString().Trim(),
								GetVat(SqlDtr["qty"].ToString(),SqlDtr["Rate"].ToString()),
								Multiply1(SqlDtr["Invoice_No"].ToString(),SqlDtr["Invoice_No"].ToString(),SqlDtr["Net_Amount"].ToString())
								);
								
							Qty_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Qty"].ToString().Trim()).ToString("N",nfi));  
							Ltr_Total  += System.Convert.ToDouble(Double.Parse(SqlDtr["Ltr"].ToString().Trim()).ToString("N",nfi));  
							//vat_total += System.Convert.ToDouble(Double.Parse(SqlDtr["VAT_Amount"].ToString().Trim()).ToString("N",nfi));  
							//other_discount += System.Convert.ToDouble(Double.Parse(SqlDtr["Disc"].ToString().Trim()).ToString("N",nfi));  
							//net_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Net_Amount"].ToString().Trim()).ToString("N",nfi));  
								              
						}

						sw.WriteLine("+-------+----------+----------------------------+-------------------------------------+------+------+---------+---------+-------------+");
						//sw.WriteLine(info ,"Total:","","","","",Qty_total.ToString("N",nfi),Ltr_Total.ToString("N",nfi),total_Vat.ToString("N",nfi)); 
						sw.WriteLine("+-------+----------+----------------------------+-------------------------------------+------+------+---------+---------+-------------+");
				
					}
					else
					{
						
						s++;
					}
					SqlDtr.Close ();
				}
				sw.WriteLine();
				sw.WriteLine();
				//if type is Purchase Report or both then writes the information about purchase .
				if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
				{
					Qty_total = 0;
					Ltr_Total = 0;
					//vat_total = 0;
					//other_discount = 0;
					//net_total = 0;
						
					
					sw.WriteLine("                             Purchase Return                                        ");
					sw.WriteLine("------------------------------------------------------------------------------------");
					sql1="select Prod_Name+':'+Pack_Type Product ,Invoice_No,Supp_Name,qty,(qty*total_qty) Ltr,Rate,entry_time,Vndr_Invoice_No,Net_Amount,VAT_Amount from View_Purchase_Return where cast(floor(cast(entry_time as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(entry_time as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by invoice_no";
					sql1=sql1+" order by "+Cache["strorderby2"];
					dbobj.SelectQuery(sql1,ref SqlDtr);
						
					if(SqlDtr.HasRows)
					{
					
						sw.WriteLine("+----------+----------+-------------------------+-------------------------------------+------+------+---------+---------+-------------+");
						sw.WriteLine("|Invoice   | Invoice  |    Vendor Name          |          Product Name               |Qty In|Qty In|  Price  |   VAT   | Total Inv.  |");
						sw.WriteLine("|  No.     |  Date    |                         |                                     | Nos  | Ltr  |         |         |  Amount     |");
						sw.WriteLine("+----------+----------+-------------------------+-------------------------------------+------+------+---------+---------+-------------+");
						//	           1234567890 1234567890 1234567890123456789012345 1234567890123456789012345678901234567 123456 123456 123456789 123456789 1234567890123
						while(SqlDtr.Read())
						{
							sw.WriteLine(info1,SqlDtr["Vndr_Invoice_No"].ToString().Trim() ,
								GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Entry_time"].ToString().Trim())),
								GenUtil.TrimLength(SqlDtr["Supp_Name"].ToString().Trim(),25) ,
								GenUtil.TrimLength(SqlDtr["Product"].ToString().Trim(),18),
								SqlDtr["Qty"].ToString().Trim(),
								SqlDtr["Ltr"].ToString().Trim(),
								SqlDtr["Rate"].ToString().Trim(),
								Multiply1(SqlDtr["Vndr_Invoice_No"].ToString(),SqlDtr["Invoice_No"].ToString(),SqlDtr["VAT_Amount"].ToString()),
								Multiply1(SqlDtr["Vndr_Invoice_No"].ToString(),SqlDtr["Invoice_No"].ToString(),SqlDtr["Net_Amount"].ToString())
								);
							Qty_total += System.Convert.ToDouble(Double.Parse(SqlDtr["Qty"].ToString().Trim()).ToString("N",nfi));  
							Ltr_Total  += System.Convert.ToDouble(Double.Parse(SqlDtr["Ltr"].ToString().Trim()).ToString("N",nfi)); 
						}
						sw.WriteLine("+----------+----------+-------------------------+-------------------------------------+------+------+---------+---------+-------------+");
						//sw.WriteLine(info1 ,"","Total:","","",Qty_total.ToString("N",nfi),Ltr_Total.ToString("N",nfi),"",other_discount.ToString("N",nfi),VAT_total.ToString("N",nfi) ,NET_total.ToString("N",nfi)); 
						sw.WriteLine("+----------+----------+-------------------------+-------------------------------------+------+------+---------+---------+-------------+");
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\SalePurchaseReturn_Report.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",sql1="";
			//**************
			//if type is Sales Report or both then writes the information about sales .
			
			if(DropReportType.SelectedItem.Text == "Sales Report" || DropReportType.SelectedItem.Text == "Both")
			{
				sw.WriteLine("                             Sales Return\t\t\t\t\t\t\t\t\t");
				sql="select Prod_Name+':'+Pack_Type Product ,Invoice_No,Cust_Name,qty,(qty*total_qty) Ltr,Rate,entry_time,Net_Amount,VAT_Amount from View_Sales_Return where cast(floor(cast(entry_time as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(entry_time as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by invoice_no";
				sql=sql+" order by "+Cache["strorderby1"];
				dbobj.SelectQuery(sql,ref SqlDtr);
				if(SqlDtr.HasRows)
				{
					sw.WriteLine("Invoice_No\tDate\tCustomer Name\tProduct Name\tQty in Nos\tQty in Ltr\tVat\tTotal Invoice Amount");
					sw.WriteLine();
					while(SqlDtr.Read())
					{
						sw.WriteLine(SqlDtr["Invoice_No"].ToString().Trim()+"\t"+
							GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Entry_Time"].ToString().Trim() ) )+"\t"+
							SqlDtr["Cust_Name"].ToString().Trim()+"\t"+
							SqlDtr["Product"].ToString().Trim()+"\t"+
							SqlDtr["Qty"].ToString().Trim()+"\t"+
							SqlDtr["Ltr"].ToString().Trim()+"\t"+
							SqlDtr["Rate"].ToString().Trim()+"\t"+
							GetVat(SqlDtr["qty"].ToString(),SqlDtr["Rate"].ToString())
							);
					}
					//sw.WriteLine("Total\t\t\t\t\t"+Qty_total.ToString("N",nfi)+"\t"+Ltr_Total.ToString("N",nfi)+"\t"+other_discount.ToString("N",nfi)+"\t"+VAT_total.ToString("N",nfi) +"\t"+NET_total.ToString("N",nfi)); 
				}
				else
					MessageBox.Show("Sales Return Data Not Availale");
				SqlDtr.Close();
			}
			//if type is Purchase Report or both then writes the information about purchase .
			if(DropReportType.SelectedItem.Text == "Purchase Report" || DropReportType.SelectedItem.Text == "Both")
			{
				Qty_total = 0;
				Ltr_Total = 0;
				//vat_total = 0;
				//other_discount = 0;
				//net_total = 0;
				sw.WriteLine("");
				sw.WriteLine("");
					
				sw.WriteLine("                Purchase Return");
				sql1="select Prod_Name+':'+Pack_Type Product ,Invoice_No,Supp_Name,qty,(qty*total_qty) Ltr,Rate,entry_time,Vndr_Invoice_No,Net_Amount,VAT_Amount from View_Purchase_Return where cast(floor(cast(entry_time as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(entry_time as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by invoice_no";
				sql1=sql1+" order by "+Cache["strorderby2"];
				dbobj.SelectQuery(sql1,ref SqlDtr);
				//dbobj.SelectQuery("select invoice_no, invoice_date,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and VAT_Amount != 0 and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'",ref SqlDtr);
				if(SqlDtr.HasRows)
				{
					
					sw.WriteLine();
					sw.WriteLine("Invoice_No\tDate\tVendor Name\tProduct Name\tQty in Nos\tQty in Ltr\tVat\tTotal Invoice Amount");
					
					while(SqlDtr.Read())
					{
						sw.WriteLine(SqlDtr["Vndr_Invoice_No"].ToString().Trim()+"\t"+
							GenUtil.str2DDMMYYYY(trimDate(SqlDtr["Entry_Time"].ToString().Trim()))+"\t"+
							SqlDtr["Supp_Name"].ToString().Trim()+"\t"+
							SqlDtr["Product"].ToString().Trim()+"\t"+
							SqlDtr["qty"].ToString().Trim()+"\t"+
							SqlDtr["Ltr"].ToString().Trim()+"\t"+
							Multiply1(SqlDtr["Vndr_Invoice_No"].ToString(),SqlDtr["Invoice_No"].ToString(),SqlDtr["VAT_Amount"].ToString())+"\t"+
							Multiply1(SqlDtr["Vndr_Invoice_No"].ToString(),SqlDtr["Invoice_No"].ToString(),SqlDtr["Net_Amount"].ToString())
							);
					}
					//sw.WriteLine("Total\t\t\t\t\t"+Qty_total.ToString("N",nfi)+"\t"+Ltr_Total.ToString("N",nfi)+"\t"+other_discount.ToString("N",nfi)+"\t"+VAT_total.ToString("N",nfi)+"\t"+NET_total.ToString("N",nfi)); 
				}
				else
					MessageBox.Show("Purchase Return Data Not Available");
				SqlDtr.Close ();
			}
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
				if(SalesGrid.Visible==true || PurchaseGrid.Visible==true)
				{
					
						ConvertToExcel();
						MessageBox.Show("Successfully Convert File Into Excel Format");
						CreateLogFiles.ErrorLog("Form:Vat_Report.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Vat_Report Convert Into Excel Format, userid  "+uid);
					
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
			string[] FromDate =txtDateFrom.Text.IndexOf("/")>0? txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length) : txtDateFrom.Text.Split(new char[] { '-' }, txtDateFrom.Text.Length);
			string[] ToDate =txtDateTo.Text.IndexOf("/")>0? txtDateTo.Text.Split(new char[] {'/'},txtDateTo.Text.Length) : txtDateTo.Text.Split(new char[] { '-' }, txtDateTo.Text.Length);
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

		/*private void RadSummarized_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadDetails.Checked)
				PanReport.Visible=true;
			else
				PanReport.Visible=false;
		}

		private void RadDetails_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadDetails.Checked)
				PanReport.Visible=true;
			else
				PanReport.Visible=false;
		}*/

		/// <summary>
		/// This method return the month name with year in given from date.
		/// </summary>
		public string getFromMonth(string Mon)
		{
			string[] Month =Mon.IndexOf("/")>0? Mon.Split(new char[] {'/'},Mon.Length) : Mon.Split(new char[] { '-' }, Mon.Length);
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
			string[] Month =Mon.IndexOf("/")>0? Mon.Split(new char[] {'/'},Mon.Length) : Mon.Split(new char[] { '-' }, Mon.Length);
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