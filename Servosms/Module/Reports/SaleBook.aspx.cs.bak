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
using RMG;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SaleBook.
	/// </summary>
	public partial class SaleBook : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);

		public double TotalQty_Ltr = 0;
		public double TotalQty_No = 0;
		public double TotalNet_Amount = 0;
		
		public double TotalQty_Ltr_Anil = 0;
		public double TotalQty_No_Anil = 0;
		public double TotalNet_Amount_Anil = 0;
		
		public double TotalQty_Ltr_Gopal = 0;
		public double TotalQty_No_Gopal = 0;
		public double TotalNet_Amount_Gopal = 0;

		public double TotalQty_Ltr_chauhan = 0;
		public double TotalQty_No_chauhan = 0;
		public double TotalNet_Amount_chauhan = 0;

		public double TotalQty_Ltr_Rajkumar = 0;
		public double TotalQty_No_Rajkumar = 0;
		public double TotalNet_Amount_Rajkumar = 0;

		public double TotalQty_Ltr_KeyCust = 0;
		public double TotalQty_No_KeyCust = 0;
		public double TotalNet_Amount_KeyCust = 0;

		public double Grand_TotalQty_Ltr = 0;
		public double Grand_TotalQty_No = 0;
		public double Grand_TotalNet_Amount = 0;

		public int flage=0;



		string uid;
	
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
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				GridSalesReport.Visible=false;
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="36";
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
				Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				GetMultiValue();
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["txtDateFrom"].ToString();
            Textbox1.Text = Request.Form["Textbox1"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["Textbox1"].ToString();
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
		/// This method is used to return the date in DD/mm/yyyy format.
		/// </summary>
		# region DateTime Function...
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
		# endregion 
		
		//************
		/// <summary>
		/// This Method is used to multiplies the package quantity with Quantity.
		/// </summary>
		public double totltr=0;
		protected double Multiply(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
						
				}
				totltr+=ans;
				Cache["totltr"]=totltr;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totltr+=System.Convert.ToDouble( mystr[0].ToString()); 
					Cache["totltr"]=totltr;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
				{
					Cache["totltr"]="0";
					return 0;
				}
			}
		}

		/// <summary>
		/// This Method is used to multiplies the package quantity with Quantity.
		/// </summary>
		public double in_amt=0,am=0;
		//double amt=0;
		double amt1=0;//,amt2=0;
		int count=0,i=0,status=0,Flag=0;
		protected string Multiply1(string inv_no, string Net_Amount)
		{
			/*** Comment By Mahesh on 04.10.008
			//PetrolPumpClass  obj=new PetrolPumpClass();
			//SqlDataReader SqlDtr;
			//string sql;
			//******/
			in_amt=0;
			
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
				/********** Comment By Mahesh on 04.10.008 b'coz this query take more time compare to excutescaler
				sql = "select count(*) from vw_SaleBook  where Invoice_No="+Cache["Invoice_No"].ToString()+"";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					count += int.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				***********/
				dbobj.ExecuteScalar("select count(*) from vw_SaleBook  where Invoice_No="+Cache["Invoice_No"].ToString()+"",ref count);
				status=1;
			}
			if(i<count)
			{
				Flag=2;
				i++;
			}
			if(i==count)
			{
				//amt1=amt;
				//sql = "select Net_amount from Sales_Master where Invoice_No="+Cache["Invoice_No"].ToString()+"";
				/***************************** Comment By Mahesh on 04.10.008 b'coz Net_Amount direct pass by function 
				sql = "select Net_amount from vw_SaleBook where Invoice_No="+Cache["Invoice_No"].ToString()+"";
				SqlDtr =obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{
					amt1 = double.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				******************************/
				amt1=double.Parse(Net_Amount);
				//amt=0;
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
				return "---";
			else if(Flag==3)
			{
				am+=amt1;
				Cache["am"]=am;
				return GenUtil.strNumericFormat(amt1.ToString());
			}
			return "";
		}
		//***************
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

				if(chkDailySales.Checked == true)
				{
					Bindthedata_Anil();
					Bindthedata_Chauhan();
					Bindthedata_GopalPraj();
					Bindthedata_RajKumar();
					Bindthedata_KeyCust();
				}
				else
				{
					Bindthedata();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesBookReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// this is used to make sorting the datagrid on clicking of the datagridheader.
		/// </summary>
		
		public void sortcommand_click_Monthly(object sender,DataGridSortCommandEventArgs e)
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
				
				Bindthedata_Monthly();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesBookReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}


		public void Bindthedata_Daily()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			if(RadioDetails.Checked)
			{
				string sql="";
				if(chkDiscount.Checked)
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
				else
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				if(DropSearchBy.SelectedIndex!=0)
				{
					/********Add by vikas 16.11.2012*****************/
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					} /********End*****************/
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							//Coment By vikas 3.5.2013 string cust_name="";
							//Coment By vikas 3.5.2013 cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							//Coment By vikas 3.5.2013 sql=sql+" and cust_Name='"+cust_name.ToString()+"'";

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
						}
						
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							//Coment By vikas 3.5.2013 string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							//Coment By vikas 3.5.2013 sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							else
								sql=sql+" and Prod_Name like '%"+str[0]+"%'";

						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}
				}
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();	
				da.Fill(ds,"Vw_4tZoom");
				DataTable dtcustomer=ds.Tables["Vw_4tZoom"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridDaily.DataSource=dv;
				if(dv.Count!=0)
				{
					GridDaily.DataBind();
					GridDaily.Visible=true;
					panDaily.Visible=true;
				}
				else
				{
					panDaily.Visible=false;
					GridDaily.Visible=false;
					MessageBox.Show("Data Not Available");
				}
				sqlcon.Dispose();
				GridSalesSummerized.Visible=false;
				GridSalesReport.Visible=false;
			}
		}


		/// <summary>
		/// This method is used to bind the datagrid with the help of DataView.
		/// </summary>
		public void Bindthedata_Monthly()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			if(RadioDetails.Checked)
			{
				string sql="";
				
				//sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
						{
							sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							//Coment By vikas 3.5.2013 string cust_name="";
							//Coment By vikas 3.5.2013 cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							//Coment By vikas 3.5.2013 sql=sql+" and cust_Name='"+cust_name.ToString()+"'";

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							//Coment By vikas 3.5.2013 string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							//Coment By vikas 3.5.2013 sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							else
								sql=sql+" and Prod_Name like '%"+str[0]+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							//sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
							sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}
				}
				sql+=" group by prod_name,pack_type,cust_name,city order by cust_name";
				//**********************
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();	
				da.Fill(ds,"Vw_4tZoom");
				DataTable dtcustomer=ds.Tables["Vw_4tZoom"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridSaleMonthly.DataSource=dv;
				if(dv.Count!=0)
				{
					GridSaleMonthly.DataBind();
					GridSaleMonthly.Visible=true;
				}
				else
				{
					GridSaleMonthly.Visible=false;
					MessageBox.Show("Data Not Available");
				}
				sqlcon.Dispose();
				GridSalesSummerized.Visible=false;
				GridSalesReport.Visible=false;
			}
		}

		/// <summary>
		/// This method is used to bind the datagrid with the help of DataView.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

			/*************************************/
			ArrayList PackVal = new ArrayList(); 
			ArrayList PackVal1 = new ArrayList(); 
			int last_index=0;
			string Spc_Pack="";
			if(DropSearchBy.SelectedIndex==5)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				string[] Totarr=Spc_Pack.Split(new char[] {','});
				for(int i=0;Totarr.Length-1>i;i++)
				{
					string[] Packarr=Totarr[i].Split(new char[] {'X'});
					Spc_Pack=Packarr[1].ToString();
					PackVal.Add(double.Parse(Packarr[1]));
				}
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==6)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==7)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==8)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}

			if(RadioDetails.Checked)
			{
				string sql="";
				if(chkDiscount.Checked)
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
				else
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							//16.3.2014 sql=sql+" and Pack_Type='"+DropValue.Value+"'";
							sql=sql+" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							//16.3.2014 sql=sql+" and Category='"+DropValue.Value+"'";
							sql=sql+" and Category in ("+Spc_Pack.ToString()+")";
							
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							/*16.3.2014  string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							else
								sql=sql+" and Prod_Name like '%"+str[0]+"%'";*/
							sql=sql+"and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							//16.3.2014 sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
							sql=sql+" and ssr in ("+Spc_Pack.ToString()+")";
					}
				}
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();	
				da.Fill(ds,"Vw_4tZoom");
				DataTable dtcustomer=ds.Tables["Vw_4tZoom"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridSalesReport.DataSource=dv;
				if(dv.Count!=0)
				{
					GridSalesReport.DataBind();
					GridSalesReport.Visible=true;
				}
				else
				{
					GridSalesReport.Visible=false;
					MessageBox.Show(" Data Not Available ");
				}
				sqlcon.Dispose();
				GridSalesSummerized.Visible=false;
			}
			else
			{
				string sql = "select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
							sql=" select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
							sql=" select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub.group_name='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
							sql = "select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where Invoice_No='"+DropValue.Value+"'";
					}
					/*16.3.2014 else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}*/
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category in ("+Spc_Pack.ToString()+")";
							
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							sql=sql+"and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr in ("+Spc_Pack.ToString()+")";
					}
				}
				sql += " group by invoice_no,cust_name,city,invoice_date,discount,net_amount";
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();
				da.Fill(ds,"vw_SaleBook");
				DataTable dtcustomer=ds.Tables["vw_SaleBook"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridSalesSummerized.DataSource=dv;
				if(dv.Count!=0)
				{
					GridSalesSummerized.DataBind();
					GridSalesSummerized.Visible=true;
				}
				else
				{
					GridSalesSummerized.Visible=false;
					MessageBox.Show("Data Not Available");
				}
				sqlcon.Dispose();
				GridSalesReport.Visible=false;
			}
		}


		public void Bindthedata_Anil()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

			string sql="";            
            sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1001' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";            
            SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_SaleBook");
			DataTable dtcustomer=ds.Tables["vw_SaleBook"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridAnil.DataSource=dv;
			if(dv.Count!=0)
			{
				GridAnil.DataBind();
				panAnil.Visible=true;
				flage=1;
			}
			else
			{
				panAnil.Visible=false;
				//MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			GridSalesSummerized.Visible=false;
		}

		public void Bindthedata_Chauhan()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

			string sql="";
				
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1002' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_SaleBook");
			DataTable dtcustomer=ds.Tables["vw_SaleBook"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridChauhan.DataSource=dv;
			if(dv.Count!=0)
			{
				GridChauhan.DataBind();
				PanChauhan.Visible=true;
				flage=1;
			}
			else
			{
				PanChauhan.Visible=false;
				//MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			GridSalesSummerized.Visible=false;
		}
		
		public void Bindthedata_GopalPraj()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

			string sql="";
				
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1004' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_SaleBook");
			DataTable dtcustomer=ds.Tables["vw_SaleBook"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridGopal.DataSource=dv;
			if(dv.Count!=0)
			{
				GridGopal.DataBind();
				PanGopal.Visible=true;
				flage=1;
			}
			else
			{
				PanGopal.Visible=false;
				//MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			GridSalesSummerized.Visible=false;
		}

		public void Bindthedata_RajKumar()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

			string sql="";
				
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1003' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_SaleBook");
			DataTable dtcustomer=ds.Tables["vw_SaleBook"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridRajKumar.DataSource=dv;
			if(dv.Count!=0)
			{
				GridRajKumar.DataBind();
				PanRajKumar.Visible=true;
				flage=1;
			}
			else
			{
				PanRajKumar.Visible=false;
				//MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			GridSalesSummerized.Visible=false;
		}

		public void Bindthedata_KeyCust()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

			string sql="";
				
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1006' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_SaleBook");
			DataTable dtcustomer=ds.Tables["vw_SaleBook"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridKeyCustomer.DataSource=dv;
			if(dv.Count!=0)
			{
				GridKeyCustomer.DataBind();
				PanKeyCustomer.Visible=true;
				flage=1;
			}
			else
			{
				PanKeyCustomer.Visible=false;
				//MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			GridSalesSummerized.Visible=false;
		}


		/// <summary>
		/// This method is used to view the report and set the column name with ascending order 
		/// in session variable.
		/// </summary>
		# region Show Button...
		protected void btnShow_Click(object sender, System.EventArgs e)
		{   
			try
			{
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
				{
					MessageBox.Show("Date From Should be less than Date To");
					GridSalesReport.Visible=false;
				}
				else
				{
					if(ChkMonthWise.Checked==false)
					{
					
						if(ChkDaily.Checked==false)
						{
							if(chkDailySales.Checked == true)                           //This condition add by vikas 01.09.09
							{
								strorderby="cust_name ASC";
								Session["Column"]="cust_name";
								Session["order"]="ASC";
						
								GridSalesReport.Visible=false;						
								GridSalesSummerized.Visible=false;
								GridDaily.Visible=false;
								GridSaleMonthly.Visible=false;

								Bindthedata_Anil();
								Bindthedata_Chauhan();
								Bindthedata_GopalPraj();
								Bindthedata_RajKumar();
								Bindthedata_KeyCust();
								if(flage == 0)
								{
									MessageBox.Show("Data Not Available");
								}
							}
							else
							{
								panAnil.Visible=false;
								PanChauhan.Visible=false;
								PanRajKumar.Visible=false;
								PanGopal.Visible=false;
								PanKeyCustomer.Visible=false;
								GridDaily.Visible=false;
								GridSaleMonthly.Visible=false;

								strorderby="invoice_no ASC";
								Session["Column"]="invoice_no";
								Session["order"]="ASC";
								Bindthedata();
							}
						}
						else
						{
							GridSalesReport.Visible=false;						
							GridSalesSummerized.Visible=false;
							panAnil.Visible=false;
							PanChauhan.Visible=false;
							PanRajKumar.Visible=false;
							PanGopal.Visible=false;
							PanKeyCustomer.Visible=false;
							GridDaily.Visible=true;
							GridSaleMonthly.Visible=false;

							strorderby="invoice_no ASC";
							Session["Column"]="invoice_no";
							Session["order"]="ASC";
							Bindthedata_Daily();
						}
					}
					else
					{
						GridSalesReport.Visible=false;						
						GridSalesSummerized.Visible=false;
						GridDaily.Visible=false;
						panAnil.Visible=false;
						PanChauhan.Visible=false;
						PanRajKumar.Visible=false;
						PanGopal.Visible=false;
						PanKeyCustomer.Visible=false;
						GridDaily.Visible=false;
						
						strorderby="cust_name ASC";
						Session["Column"]="cust_name";
						Session["order"]="ASC";

						Bindthedata_Monthly();
					}
				}
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:btnShow_Click  Sale Book Report   Viewed "+"  userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:btnShow_Click  Sale Book Report   Viewed "+"  EXCEPTION  "+ ex.Message+"  userid  "+uid);
			}
		}
		# endregion  

		/// <summary>
		/// This method is not used.
		/// </summary>
		private string GetString(string str,string spc)
		{
			if(str.Length>spc.Length)
				return str;
			else
				return str+spc.Substring(0,spc.Length-str.Length)+"  ";			
		}
				
		/// <summary>
		/// This method is not used.
		/// </summary>
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6,ref int len7,ref int len8, ref int len9,ref int len10,ref int len11,ref int len12,ref int len13,ref int len14)
		{
			while(rdr.Read())
			{
				if(rdr["Cust_ID"].ToString().Trim().Length>len1)
					len1=rdr["Cust_ID"].ToString().Trim().Length;					
				if(rdr["Cust_Name"].ToString().Trim().Length>len2)
					len2=rdr["Cust_Name"].ToString().Trim().Length;					
				if(rdr["City"].ToString().Trim().Length>len3)
					len3=rdr["City"].ToString().Trim().Length;
				if(rdr["Cust_Type"].ToString().Trim().Length>len4)
					len4=rdr["Cust_Type"].ToString().Trim().Length;					
				if(rdr["Invoice_No"].ToString().Trim().Length>len5)
					len5=rdr["Invoice_No"].ToString().Trim().Length;					
				if(rdr["Invoice_Date"].ToString().Trim().Length>len6)
					len6=rdr["Invoice_Date"].ToString().Trim().Length;	
				if(rdr["Under_SalesMan"].ToString().Trim().Length>len7)
					len7=rdr["Under_SalesMan"].ToString().Trim().Length;	
				if(rdr["Pack_Type"].ToString().Trim().Length>len8)
					len8=rdr["Pack_Type"].ToString().Trim().Length;	
				if(rdr["Prod_Name"].ToString().Trim().Length>len9)
					len9=rdr["Prod_Name"].ToString().Trim().Length;	
				if(rdr["Qty"].ToString().Trim().Length>len10)
					len10=rdr["Qty"].ToString().Trim().Length;	
				if(rdr["Rate"].ToString().Trim().Length>len11)
					len11=rdr["Rate"].ToString().Trim().Length;	
				if(rdr["Discount"].ToString().Trim().Length>len12)
					len12=rdr["Discount"].ToString().Trim().Length;	
				if(rdr["Promo_Scheme"].ToString().Trim().Length>len13)
					len13=rdr["Promo_Scheme"].ToString().Trim().Length;	
				if(rdr["Cr_Days"].ToString().Trim().Length>len14)
					len14=rdr["Cr_Days"].ToString().Trim().Length;	
			}
		}
		
		/// <summary>
		/// This method is not used.
		/// </summary>
		private string GetString(string str,int maxlen,string spc)
		{		
			return str+spc.Substring(0,maxlen>str.Length?maxlen-str.Length:str.Length-maxlen);
		}
		
		/// <summary>
		/// This method is not used.
		/// </summary>
		private string MakeString(int len)
		{
			string spc="";
			for(int x=0;x<len;x++)
				spc+=" ";
			return spc;
		}
	
		/// <summary>
		/// Method to prepare the report file.
		/// </summary>
		public void makingReport()
		{
			/*
																	=========================================
																	SALES REPORT From 01/07/2006 To 9/7/2006
																	=========================================
+----+--------------------+---------------+-------------+------+----------+--------------------+-------+---------------+--------+------+--------+--------------------+----+----------+
|Cust|Customer Name       |     City      |Customer Type|Inv.No|Inv. Date |   Under Salesman   | Pack. | Product Name  |Quantity| Rate |Discount|    Promo Scheme    |Cr. | Due Date |
| ID |                    |               |             |      |          |                    | Type  |               |        |      |        |                    |Days|          |
+----+--------------------+---------------+-------------+------+----------+--------------------+-------+---------------+--------+------+--------+--------------------+----+----------+
 1234 12345678901234567890 123456789012345 1234567890123 123456 1234567890 12345678901234567890 1234567 123456789012345 12345678 123456 12345678 12345678901234567890 1234 1234567890
 */
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				string strDate="";
				//string strDueDate="";
				//string promo = "";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesBookReport.txt";
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
				//**********
				if(RadioDetails.Checked)
				{
					//					sql="select invoice_no,cust_name,city,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,due_date,promo_scheme from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast (invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					//					//**********************
					//					if(DropSearchBy.SelectedIndex!=0)
					//					{
					//						if(DropSearchBy.SelectedIndex==1)
					//						{
					//							if(DropValue.Value!="All")
					//								sql=sql+" and cust_type like '"+DropValue.Value+"%'";
					//						}
					//						else if(DropSearchBy.SelectedIndex==2)
					//						{
					//							if(DropValue.Value!="All")
					//								sql=sql+" and cust_Name='"+DropValue.Value+"'";
					//						}
					//						else if(DropSearchBy.SelectedIndex==3)
					//						{
					//							if(DropValue.Value!="All")
					//								sql=sql+" and Invoice_No='"+DropValue.Value+"'";
					//						}
					//						else if(DropSearchBy.SelectedIndex==4)
					//						{
					//							if(DropValue.Value!="All")
					//								sql=sql+" and Category='"+DropValue.Value+"'";
					//						}
					//						else if(DropSearchBy.SelectedIndex==5)
					//						{
					//							if(DropValue.Value!="All")
					//							{
					//								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
					//								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
					//							}
					//						}
					//						else if(DropSearchBy.SelectedIndex==6)
					//						{
					//							if(DropValue.Value!="All")
					//								sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
					//						}
					//					}
					//					//**********************
					//					sql=sql+" order by "+Cache["strorderby"];
					//					dbobj.SelectQuery(sql,ref rdr);
					//					string des="-----------------------------------------------------------------------------------------------------------------------------------------";
					//					string Address=GenUtil.GetAddress();
					//					string[] addr=Address.Split(new char[] {':'},Address.Length);
					//					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					//					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					//					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					//					sw.WriteLine(des);
					//					//**********
					//					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					//					sw.WriteLine(GenUtil.GetCenterAddr("SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
					//					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					//					//				sw.WriteLine("+----+--------------------+---------------+-------------+------+----------+--------------------+----------+---------------+--------+------+--------+--------------------+----+----------+");
					//					//				sw.WriteLine("|Cust|  Customer Name     |     City      |Customer Type|Inv.No|Inv. Date |   Under Salesman   |Pack. Type| Product Name  |Quantity| Rate |Discount|    InvoiceAmount   |Cr. | Due Date |");
					//					//				sw.WriteLine("| ID |                    |               |             |      |          |                    |          |               |        |      |        |                    |Days|          |");
					//					//				sw.WriteLine("+----+--------------------+---------------+-------------+------+----------+--------------------+----------+---------------+--------+------+--------+--------------------+----+----------+"); 
					//					// 1234 12345678901234567890 123456789012345 1234567890123 123456 1234567890 12345678901234567890 1234567890 123456789012345 12345678 123456 12345678 12345678901234567890 1234 1234567890
					//					//sw.WriteLine("+-------------+---------+---------+-----+----------+-----------+--------+------------+------+--------+--------+---------+----+----------+");
					//					//sw.WriteLine("|Customer Name|  Place  |Customer |Invo.| Invoice  |  Under    | Pack.  |Product Name|Qty in| Price  |Discount| Invoice |Cr. | Due Date |");
					//					//sw.WriteLine("|             |         |Category | No  |  Date    | Salesman  | Type   |            | ltr. |        |        | Amount  |Days|          |");
					//					//sw.WriteLine("+-------------+---------+---------+-----+----------+-----------+--------+------------+------+--------+--------+---------+----+----------+"); 
					//					//             1234567890123 123456789 123456789 12345 1234567890 12345678901 12345678 123456789012 123456 12345678 12345678 123456789 1234 1234567890
					//					sw.WriteLine("+-------------------+---------------+-----+----------+--------+---------------------------------+------+---------+--------+-------------+");
					//					sw.WriteLine("|   Customer Name   |     Place     |Invo.| Invoice  | Pack.  |          Product Name           |Qty in|  Price  |Discount|   Invoice   |");
					//					sw.WriteLine("|                   |               | No  |  Date    | Type   |                                 | ltr. |         |        |   Amount    |");
					//					sw.WriteLine("+-------------------+---------------+-----+----------+--------+---------------------------------+------+---------+--------+-------------+"); 
					//					//             1234567890123456789 123456789012345 12345 1234567890 12345678 123456789012345678901234567890123 123456 123456789 12345678 1234567890123
					//					info = " {0,-19:S} {1,-15:S} {2,-5:S} {3,-10:S} {4,-8:S} {5,-33:S} {6,6:S} {7,9:S} {8,8:S} {9,13:S}";
					//					if(rdr.HasRows)
					//					{
					//						// info : to set the string format.
					//						//info = " {0,-4:S} {1,-20:S} {2,-15:S} {3,-13:S} {4,-6:S} {5,-10:S} {6,-20:S} {7,-10:S} {8,-15:S} {9,-8:S} {10,6:F} {11,-8:S} {12,-20:S} {13,-4:S} {14,-10:S}";
					//						
					//						while(rdr.Read())
					//						{					
					//							strDate = rdr["Invoice_Date"].ToString().Trim();
					//							int pos = strDate.IndexOf(" ");
					//				
					//							if(pos != -1)
					//							{
					//								strDate = strDate.Substring(0,pos);
					//							}
					//							else
					//							{
					//								strDate = "";					
					//							}
					//                    
					//							strDueDate = rdr["Due_date"].ToString().Trim();
					//							pos = -1;
					//							pos = strDueDate.IndexOf(" ");
					//				
					//							if(pos != -1)
					//							{
					//								strDueDate = strDueDate.Substring(0,pos);
					//							}
					//							else
					//							{
					//								strDueDate = "";					
					//							}
					//					
					//							promo = rdr["Promo_Scheme"].ToString().Trim();
					//
					//							if (promo.Length > 20)
					//							{
					//								promo = promo.Substring(0,20);
					//							}
					//
					//							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),19),
					//								GenUtil.TrimLength(rdr["City"].ToString().Trim(),15),
					//								//GenUtil.TrimLength(rdr["Cust_Type"].ToString().Trim(),9),
					//								GenUtil.TrimLength(rdr["Invoice_No"].ToString().Trim(),5),
					//								GenUtil.str2DDMMYYYY(strDate),
					//								//GenUtil.TrimLength(rdr["Under_SalesMan"].ToString().Trim(),11),
					//								GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),8),
					//								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),33),
					//								//Multiply(rdr["quant"].ToString().Trim()+"X" +rdr["Pack_Type"].ToString().Trim()).ToString(),
					//								//TotalQtyinLtr1(rdr["quant"].ToString(),rdr["total_qty"].ToString()),
					//								rdr["total_qty"].ToString(),
					//								//rdr["Qty"].ToString().Trim(),
					//								GenUtil.strNumericFormat(rdr["Rate"].ToString().Trim()),
					//								rdr["Discount"].ToString().Trim(),
					//								Multiply1(rdr["Invoice_No"].ToString().Trim(),rdr["Net_Amount"].ToString()).ToString()
					//								//	promo,
					//								//rdr["Cr_Days"].ToString().Trim(),
					//								//GenUtil.str2DDMMYYYY(strDueDate)
					//								);
					//								TotalQty_Ltr+=double.Parse(rdr["Total_Qty"].ToString());
					//						}
					//					}
					//					sw.WriteLine("+-------------------+---------------+-----+----------+--------+---------------------------------+------+---------+--------+-------------+");
					//					sw.WriteLine(info,"Total:","","","","","",TotalQty_Ltr.ToString(),"","",GenUtil.strNumericFormat(Cache["am"].ToString()));
					//					sw.WriteLine("+-------------------+---------------+-----+----------+--------+---------------------------------+------+---------+--------+-------------+");
					/* add by Mahesh on 14.11.008
					 * change printing code when select the details option
					 */
					//sql="select invoice_no,cust_name,city,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,due_date,promo_scheme from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast (invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					/****
					if(chkDiscount.Checked)
						sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and sb.discount>0 and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
					else
						sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					*****/
					if(chkDiscount.Checked)
						sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and schname='Primary(LTR&% Scheme)' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
					else
						sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					//**********************
					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							if(DropValue.Value!="All")
							{
								//coment by vikas 25.05.09 sql=sql+" and cust_Name='"+DropValue.Value+"'";
								/*3.5.2013 string cust_name="";
								cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
								sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
								else
									sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
							}
								
						}
						else if(DropSearchBy.SelectedIndex==4)
						{
							/*if(DropValue.Value!="All")
								sql=sql+" and Invoice_No='"+DropValue.Value+"'";*/
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
							}
			
						}
						else if(DropSearchBy.SelectedIndex==5)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Pack_Type='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==6)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Category='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==7)
						{
							if(DropValue.Value!="All")
							{
								/*3.5.2013 string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
								else
									sql=sql+" and Prod_Name like '%"+str[0]+"%'";
							}
						}
						else if(DropSearchBy.SelectedIndex==8)
						{
							if(DropValue.Value!="All")
								//sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
								sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
						}
					}
					//**********************
					sql=sql+" order by "+Cache["strorderby"];
					dbobj.SelectQuery(sql,ref rdr);
					string des="-----------------------------------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					//				sw.WriteLine("+----+--------------------+---------------+-------------+------+----------+--------------------+----------+---------------+--------+------+--------+--------------------+----+----------+");
					//				sw.WriteLine("|Cust|  Customer Name     |     City      |Customer Type|Inv.No|Inv. Date |   Under Salesman   |Pack. Type| Product Name  |Quantity| Rate |Discount|    InvoiceAmount   |Cr. | Due Date |");
					//				sw.WriteLine("| ID |                    |               |             |      |          |                    |          |               |        |      |        |                    |Days|          |");
					//				sw.WriteLine("+----+--------------------+---------------+-------------+------+----------+--------------------+----------+---------------+--------+------+--------+--------------------+----+----------+"); 
					// 1234 12345678901234567890 123456789012345 1234567890123 123456 1234567890 12345678901234567890 1234567890 123456789012345 12345678 123456 12345678 12345678901234567890 1234 1234567890
					//sw.WriteLine("+-------------+---------+---------+-----+----------+-----------+--------+------------+------+--------+--------+---------+----+----------+");
					//sw.WriteLine("|Customer Name|  Place  |Customer |Invo.| Invoice  |  Under    | Pack.  |Product Name|Qty in| Price  |Discount| Invoice |Cr. | Due Date |");
					//sw.WriteLine("|             |         |Category | No  |  Date    | Salesman  | Type   |            | ltr. |        |        | Amount  |Days|          |");
					//sw.WriteLine("+-------------+---------+---------+-----+----------+-----------+--------+------------+------+--------+--------+---------+----+----------+"); 
					//             1234567890123 123456789 123456789 12345 1234567890 12345678901 12345678 123456789012 123456 12345678 12345678 123456789 1234 1234567890
					// string des="-----------------------------------------------------------------------------------------------------------------------------------------";
					sw.WriteLine("+---------------+-----+----------+-----------------+--------+----+-----+---------+-------+------+------+-------+--------+-------+-------+");
					sw.WriteLine("| Customer Name |Invo.| Invoice  |  Product Name   | Pack.  |Qty | Qty |  Total  | Secon.|Disc. |F/Oe  | Cash  |   Vat  | Total |Total  |");
					sw.WriteLine("|               | No  |  Date    |                 | Type   |Pkg | Ltr |   RSP   | Disc. | (%)  |Disc. | Disc. |        |       |Amount |");
					sw.WriteLine("+---------------+-----+----------+-----------------+--------+----+-----+---------+-------+------+------+-------+--------+-------+-------+"); 
					//             123456789012345 12345 1234567890 12345678901234567 12345678 1234 12345 123456789 1234567 123456 123456 1234567 12345678 1234567 1234567
					info = "|{0,-15:S}|{1,-5:S}|{2,-10:S}|{3,-17:S}|{4,-8:S}|{5,4:S}|{6,5:S}|{7,9:S}|{8,7:S}|{9,6:S}|{10,6:S}|{11,7:S}|{12,8:S}|{13,7:S}|{14,7:S}|";
					if(rdr.HasRows)
					{
						// info : to set the string format.
						//info = " {0,-4:S} {1,-20:S} {2,-15:S} {3,-13:S} {4,-6:S} {5,-10:S} {6,-20:S} {7,-10:S} {8,-15:S} {9,-8:S} {10,6:F} {11,-8:S} {12,-20:S} {13,-4:S} {14,-10:S}";
						
						while(rdr.Read())
						{					
							//							strDate = rdr["Invoice_Date"].ToString().Trim();
							//							int pos = strDate.IndexOf(" ");
							//				
							//							if(pos != -1)
							//							{
							//								strDate = strDate.Substring(0,pos);
							//							}
							//							else
							//							{
							//								strDate = "";					
							//							}
							//                    
							//							strDueDate = rdr["Due_date"].ToString().Trim();
							//							pos = -1;
							//							pos = strDueDate.IndexOf(" ");
							//				
							//							if(pos != -1)
							//							{
							//								strDueDate = strDueDate.Substring(0,pos);
							//							}
							//							else
							//							{
							//								strDueDate = "";					
							//							}
							//					
							//							promo = rdr["Promo_Scheme"].ToString().Trim();
							//
							//							if (promo.Length > 20)
							//							{
							//								promo = promo.Substring(0,20);
							//							}
							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),15),
								GenUtil.TrimLength(rdr["Invoice_No"].ToString().Trim(),5),
								GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString())),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),17),
								GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),8),
								rdr["quant"].ToString(),
								rdr["total_qty"].ToString(),
								//System.Convert.ToString(Math.Round(double.Parse(rdr["Rate"].ToString()),1)),
								System.Convert.ToString(Math.Round(double.Parse(rdr["TotalRate"].ToString()),1)),
								GetSchDiscount(rdr["Prod_Name"].ToString(),rdr["Pack_type"].ToString(),rdr["Total_Qty"].ToString(),rdr["TotalRate"].ToString(),rdr["Cust_ID"].ToString(),rdr["Sch_Type"].ToString()),
								//GetPerDiscount(rdr["Discount"].ToString(),rdr["Discount_Type"].ToString(),rdr["TotalRate"].ToString(),rdr["foe"].ToString(),rdr["Total_Qty"].ToString(),rdr["InvoiceNo"].ToString()),
								GetPerDiscount(rdr["Prod_Name"].ToString(),rdr["Pack_type"].ToString(),rdr["Total_Qty"].ToString(),rdr["TotalRate"].ToString(),rdr["Cust_ID"].ToString(),rdr["Sch_Type"].ToString()),
								GetFleetOeDiscount(rdr["foe"].ToString(),rdr["Total_Qty"].ToString()),
								GetCashDiscount(rdr["cash_discount"].ToString(),rdr["cash_disc_type"].ToString(),rdr["TotalRate"].ToString()),
								GetVat(rdr["Invoice_No"].ToString()),
								GetTotal(rdr["Invoice_No"].ToString()),
								GetTotalAmount(rdr["Invoice_No"].ToString(),rdr["Net_Amount"].ToString())
								);
							TotalQty_Ltr+=double.Parse(rdr["TotalRate"].ToString());
							TotalQtyLtr+=double.Parse(rdr["Total_Qty"].ToString());
							TotalQty+=double.Parse(rdr["quant"].ToString());
						}
					}
					sw.WriteLine("+---------------+-----+----------+-----------------+--------+----+-----+---------+-------+------+------+-------+--------+-------+-------+"); 
					sw.WriteLine(info,"Total","","","","",TotalQty.ToString(),System.Convert.ToString(Math.Round(TotalQtyLtr)),System.Convert.ToString(Math.Round(TotalQty_Ltr,1)),System.Convert.ToString(Math.Round(total_SchDisc)),System.Convert.ToString(Math.Round(total_PerDisc)),System.Convert.ToString(Math.Round(total_FleetOe,1)),GenUtil.strNumericFormat(total_CashDisc.ToString()),System.Convert.ToString(Math.Round(total_Vat,1)),total_Total.ToString(),total_TotalAmount.ToString());
					//sw.WriteLine(info,"Total","","","","",TotalQty.ToString(),TotalQtyLtr.ToString(),"",TotalQty_Ltr.ToString(),total_SchDisc.ToString(),total_CashDisc.ToString(),total_FleetOe.ToString(),GenUtil.strNumericFormat(total_Vat.ToString()),total_Total.ToString(),total_TotalAmount.ToString());
					sw.WriteLine("+---------------+-----+----------+-----------------+--------+----+-----+---------+-------+------+------+-------+--------+-------+-------+"); 
				}
				else
				{
					sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					//**********************
					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							if(DropValue.Value!="All")
								sql=" select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							if(DropValue.Value!="All")
								sql=" select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub.group_name='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							if(DropValue.Value!="All")
							{
								//comment by vikas 25.05.09 sql=sql+" and cust_Name='"+DropValue.Value+"'";
								/*string cust_name="";
								cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
								sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
								else
									sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
							}
								
						}
						else if(DropSearchBy.SelectedIndex==4)
						{
							/*if(DropValue.Value!="All")
								sql=sql+" and Invoice_No='"+DropValue.Value+"'";*/
							if(DropValue.Value!="All")
								sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where Invoice_No='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==5)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Category='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==6)
						{
							if(DropValue.Value!="All")
							{
								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==7)
						{
							if(DropValue.Value!="All")
								//sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
								sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
						}
					}
					//**********************
					sql+=" group by invoice_no,cust_name,city,invoice_date,discount,net_amount";
					sql=sql+" order by "+Cache["strorderby"];
					dbobj.SelectQuery(sql,ref rdr);
					string des="-----------------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					//				sw.WriteLine("+----+--------------------+---------------+-------------+------+----------+--------------------+----------+---------------+--------+------+--------+--------------------+----+----------+");
					//				sw.WriteLine("|Cust|  Customer Name     |     City      |Customer Type|Inv.No|Inv. Date |   Under Salesman   |Pack. Type| Product Name  |Quantity| Rate |Discount|    InvoiceAmount   |Cr. | Due Date |");
					//				sw.WriteLine("| ID |                    |               |             |      |          |                    |          |               |        |      |        |                    |Days|          |");
					//				sw.WriteLine("+----+--------------------+---------------+-------------+------+----------+--------------------+----------+---------------+--------+------+--------+--------------------+----+----------+"); 
					// 1234 12345678901234567890 123456789012345 1234567890123 123456 1234567890 12345678901234567890 1234567890 123456789012345 12345678 123456 12345678 12345678901234567890 1234 1234567890
					//sw.WriteLine("+-------------+---------+---------+-----+----------+-----------+--------+------------+------+--------+--------+---------+----+----------+");
					//sw.WriteLine("|Customer Name|  Place  |Customer |Invo.| Invoice  |  Under    | Pack.  |Product Name|Qty in| Price  |Discount| Invoice |Cr. | Due Date |");
					//sw.WriteLine("|             |         |Category | No  |  Date    | Salesman  | Type   |            | ltr. |        |        | Amount  |Days|          |");
					//sw.WriteLine("+-------------+---------+---------+-----+----------+-----------+--------+------------+------+--------+--------+---------+----+----------+"); 
					//             1234567890123 123456789 123456789 12345 1234567890 12345678901 12345678 123456789012 123456 12345678 12345678 123456789 1234 1234567890
					sw.WriteLine("+---------------------------+-----------------+-------+----------+----------+------------+----------+-----------------+");
					sw.WriteLine("|       Customer Name       |      Place      | Invo. | Invoice  |  Qty in  |   Qty in   | Discount |     Invoice     |");
					sw.WriteLine("|                           |                 |  No   |  Date    |    No.   |   Ltr.     |          |     Amount      |");
					sw.WriteLine("+---------------------------+-----------------+-------+----------+----------+------------+----------+-----------------+"); 
					//             123456789012345678901234567 12345678901234567 1234567 1234567890 1234567890 123456789012 1234567890 12345678901234567
					info = " {0,-27:S} {1,-17:S} {2,-7:S} {3,-10:S} {4,10:S} {5,12:S} {6,10:S} {7,17:S}";
				
				
					if(rdr.HasRows)
					{
						// info : to set the string format.
						//info = " {0,-4:S} {1,-20:S} {2,-15:S} {3,-13:S} {4,-6:S} {5,-10:S} {6,-20:S} {7,-10:S} {8,-15:S} {9,-8:S} {10,6:F} {11,-8:S} {12,-20:S} {13,-4:S} {14,-10:S}";
						while(rdr.Read())
						{					
							strDate = rdr["Invoice_Date"].ToString().Trim();
							int pos = strDate.IndexOf(" ");
				
							if(pos != -1)
							{
								strDate = strDate.Substring(0,pos);
							}
							else
							{
								strDate = "";					
							}
                    
							//							strDueDate = rdr["Due_date"].ToString().Trim();
							//							pos = -1;
							//							pos = strDueDate.IndexOf(" ");
							//				
							//							if(pos != -1)
							//							{
							//								strDueDate = strDueDate.Substring(0,pos);
							//							}
							//							else
							//							{
							//								strDueDate = "";					
							//							}
							//					
							//							promo = rdr["Promo_Scheme"].ToString().Trim();
							//
							//							if (promo.Length > 20)
							//							{
							//								promo = promo.Substring(0,20);
							//							}

							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),27),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),17),
								//GenUtil.TrimLength(rdr["Cust_Type"].ToString().Trim(),9),
								GenUtil.TrimLength(rdr["Invoice_No"].ToString().Trim(),7),
								GenUtil.str2DDMMYYYY(strDate),
								//GenUtil.TrimLength(rdr["Under_SalesMan"].ToString().Trim(),11),
								//GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),8),
								//GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),33),
								rdr["quant"].ToString().Trim(),
								rdr["totQty"].ToString().Trim(),
								//GenUtil.strNumericFormat(rdr["Rate"].ToString().Trim()),
								rdr["Discount"].ToString().Trim(),
								rdr["Net_Amount"].ToString().Trim()
								//	promo,
								//rdr["Cr_Days"].ToString().Trim(),
								//GenUtil.str2DDMMYYYY(strDueDate)
								);
							TotalQty_No+=double.Parse(rdr["quant"].ToString());
							TotalQty_Ltr+=double.Parse(rdr["TotQty"].ToString());
							TotalNet_Amount+=double.Parse(rdr["Net_Amount"].ToString());
						}
					}
					sw.WriteLine("+---------------------------+-----------------+-------+----------+----------+------------+----------+-----------------+"); 
					sw.WriteLine(info,"Total:","","","",TotalQty_No.ToString(),TotalQty_Ltr.ToString(),"",TotalNet_Amount.ToString());
					sw.WriteLine("+---------------------------+-----------------+-------+----------+----------+------------+----------+-----------------+"); 
				}
				
		
				dbobj.Dispose();
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
				//******Hide by Mahesh, not show the SalesBook_PrintPreview.aspx form on run time.
				//Session["From_Date"] = txtDateFrom.Text;
				//Session["To_Date"] = Textbox1.Text;
				//Response.Redirect("SalesBook_PrintPreview.aspx",false); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}


		/// <summary>
		/// Method to prepare the report file.
		/// </summary>
		public void makingReport_Monthly()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				string strDate="";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesBookReport.txt";
				StreamWriter sw = new StreamWriter(path);
				
				sw.Write((char)27);//added by vishnu
				sw.Write((char)67);//added by vishnu
				sw.Write((char)0);//added by vishnu
				sw.Write((char)12);//added by vishnu
			
				sw.Write((char)27);//added by vishnu
				sw.Write((char)78);//added by vishnu
				sw.Write((char)5);//added by vishnu
							
				sw.Write((char)27);//added by vishnu
				sw.Write((char)15);
				if(RadioDetails.Checked)
				{
					sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";

					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							if(DropValue.Value!="All")
							{
								sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							if(DropValue.Value!="All")
							{
								sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							if(DropValue.Value!="All")
							{
								/*string cust_name="";
								cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
								sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
								else
									sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
							}
								
						}
						else if(DropSearchBy.SelectedIndex==4)
						{
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
							}
			
						}
						else if(DropSearchBy.SelectedIndex==5)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Pack_Type='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==6)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Category='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==7)
						{
							if(DropValue.Value!="All")
							{
								/*string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
								else
									sql=sql+" and Prod_Name like '%"+str[0]+"%'";
							}
						}
						else if(DropSearchBy.SelectedIndex==8)
						{
							if(DropValue.Value!="All")
								sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
						}
					}
					sql=sql+"  group by prod_name,pack_type,cust_name,city order by "+Cache["strorderby"];
					dbobj.SelectQuery(sql,ref rdr);
					string des="---------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("SSR MONTHLY SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					
					sw.WriteLine("+--------------------------------------------------+------------------------------+--------+--------+---------+");
					sw.WriteLine("|                    Customer Name                 |        Product Name          | Pack.  |  Qty   |   Qty   |");
					sw.WriteLine("|                                                  |                              | Type   |  Pkg   |   Ltr   |");
					sw.WriteLine("+--------------------------------------------------+------------------------------+--------+--------+---------+"); 
					//             12345678901234567890123456789012345678901234567890 123456789012345678901234567890 12345678 12345678 123456789 
					info = "|{0,-50:S}|{1,-30:S}|{2,-8:S}|{3,8:S}|{4,9:S}|";
					if(rdr.HasRows)
					{
						while(rdr.Read())
						{					
							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),50),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),30),
								GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),8),
								rdr["quant"].ToString(),
								rdr["total_qty"].ToString()
								);
							TotalQtyLtr+=double.Parse(rdr["Total_Qty"].ToString());
							TotalQty+=double.Parse(rdr["quant"].ToString());
						}
					}
					sw.WriteLine("+--------------------------------------------------+------------------------------+--------+--------+---------+"); 
					sw.WriteLine(info,"Total","","",TotalQty.ToString(),System.Convert.ToString(Math.Round(TotalQtyLtr)));
					sw.WriteLine("+--------------------------------------------------+------------------------------+--------+--------+---------+"); 
				}
				dbobj.Dispose();
				sw.Close();
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}
		public void makingReport_Daily()
		{
			
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				string strDate="";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesBookReport.txt";
				StreamWriter sw = new StreamWriter(path);
				
				sw.Write((char)27);//added by vishnu
				sw.Write((char)67);//added by vishnu
				sw.Write((char)0);//added by vishnu
				sw.Write((char)12);//added by vishnu
			
				sw.Write((char)27);//added by vishnu
				sw.Write((char)78);//added by vishnu
				sw.Write((char)5);//added by vishnu
							
				sw.Write((char)27);//added by vishnu
				sw.Write((char)15);
				if(RadioDetails.Checked)
				{
					if(chkDiscount.Checked)
						sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and schname='Primary(LTR&% Scheme)' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
					else
						sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					if(DropSearchBy.SelectedIndex!=0)
					{
						/********Add by vikas 16.11.2012*****************/
						if(DropSearchBy.SelectedIndex==1)
						{
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
							}
						} /********End*****************/
						else if(DropSearchBy.SelectedIndex==3)
						{
							if(DropValue.Value!="All")
							{
								/*3.5.2013 string cust_name="";
								cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
								sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
								else
									sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
							}
								
						}
						else if(DropSearchBy.SelectedIndex==4)
						{
							if(DropValue.Value!="All")
							{
								if(chkDiscount.Checked)
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
								else
									sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
							}
			
						}
						else if(DropSearchBy.SelectedIndex==5)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Pack_Type='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==6)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Category='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==7)
						{
							if(DropValue.Value!="All")
							{
								/*string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";*/

								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								if(str.Length==2)
									sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
								else
									sql=sql+" and Prod_Name like '%"+str[0]+"%'";
							}
						}
						else if(DropSearchBy.SelectedIndex==8)
						{
							if(DropValue.Value!="All")
								sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
						}
					}
					sql=sql+" order by "+Cache["strorderby"];
					dbobj.SelectQuery(sql,ref rdr);
					string des="-------------------------------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("GODOWN SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					
					/*Coment by vikas 04.11.09 sw.WriteLine("+---------------+-----+----------+-----------------+--------+----+-----+-------+-------+");
					sw.WriteLine("| Customer Name |Invo.| Invoice  |  Product Name   | Pack.  |Qty | Qty | Total |Total  |");
					sw.WriteLine("|               | No  |  Date    |                 | Type   |Pkg | Ltr |       |Amount |");
					sw.WriteLine("+---------------+-----+----------+-----------------+--------+----+-----+-------+-------+"); 
					//             123456789012345 12345 1234567890 12345678901234567 12345678 1234 12345 123456789 1234567 123456 123456 1234567 12345678 1234567 1234567*/

					sw.WriteLine("+---------------------------------------------+-----+----------+------------------------------+--------+----+-----+---------+-------+");
					sw.WriteLine("|                Customer Name                |Invo.| Invoice  |         Product Name         | Pack.  |Qty | Qty |  Total  |Total  |");
					sw.WriteLine("|                                             | No  |  Date    |                              | Type   |Pkg | Ltr |         |Amount |");
					sw.WriteLine("+---------------------------------------------+-----+----------+------------------------------+--------+----+-----+---------+-------+"); 
					//             123456789012345678901234567890123456789012345 12345 1234567890 123456789012345678901234567890 12345678 1234 12345 123456789 1234567
					info = "|{0,-45:S}|{1,-5:S}|{2,-10:S}|{3,-30:S}|{4,-8:S}|{5,4:S}|{6,5:S}|{7,9:S}|{8,7:S}|";
					if(rdr.HasRows)
					{
											
						while(rdr.Read())
						{					
							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),45),
								GenUtil.TrimLength(rdr["Invoice_No"].ToString().Trim(),5),
								GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString())),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),30),
								GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),8),
								rdr["quant"].ToString(),
								rdr["total_qty"].ToString(),
								System.Convert.ToString(Math.Round(double.Parse(rdr["TotalRate"].ToString()),1)),
								//GetTotal(rdr["Invoice_No"].ToString()),
								GetTotal_Daily(rdr["Invoice_No"].ToString(),rdr["Prod_Name"].ToString(),rdr["Pack_type"].ToString(),rdr["Total_Qty"].ToString(),rdr["TotalRate"].ToString(),rdr["Cust_id"].ToString(),rdr["Sch_Type"].ToString(),rdr["foe"].ToString(),rdr["cash_discount"].ToString(),rdr["cash_disc_type"].ToString()),
								GetTotalAmount(rdr["Invoice_No"].ToString(),rdr["Net_Amount"].ToString())
								);
							TotalQty_Ltr+=double.Parse(rdr["TotalRate"].ToString());
							TotalQtyLtr+=double.Parse(rdr["Total_Qty"].ToString());
							TotalQty+=double.Parse(rdr["quant"].ToString());
						}
					}
					sw.WriteLine("+---------------------------------------------+-----+----------+------------------------------+--------+----+-----+---------+-------+"); 
					sw.WriteLine(info,"Total","","","","",TotalQty.ToString(),System.Convert.ToString(Math.Round(TotalQtyLtr)),System.Convert.ToString(Math.Round(TotalQty_Ltr,1)),total_Total.ToString(),total_TotalAmount.ToString());
					sw.WriteLine("+---------------------------------------------+-----+----------+------------------------------+--------+----+-----+---------+-------+"); 
				}
				dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}

		public void makingReport_Sales()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				string info1 = "";
				string strDate="";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesBookReport.txt";
				StreamWriter sw = new StreamWriter(path);
				// Condensed
				sw.Write((char)27);      //added by vishnu
				sw.Write((char)67);      //added by vishnu
				sw.Write((char)0);       //added by vishnu
				sw.Write((char)12);      //added by vishnu
				sw.Write((char)27);      //added by vishnu
				sw.Write((char)78);      //added by vishnu
				sw.Write((char)5);       //added by vishnu
				sw.Write((char)27);      //added by vishnu
				sw.Write((char)15);
				
					string des="-----------------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					
					sw.WriteLine("SSR : Anil Agarwal");
					sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
					sw.WriteLine("|          Customer Name & Place         | Category  |   Product Name With Pack     | Qty Nos.| Qty Ltr.| Total Amount |");
					sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
					//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
					info = " {0,-40:S} {1,-11:S} {2,-30:S} {3,9:S} {4,9:S} {5,14:S} ";
					
					info1 = " {0,-81:S} {1,9:S} {2,9:S} {3,14:S} ";
					
					/***********For Anil*****************/
					sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1001' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
					//sql=sql+" order by "+Cache["strorderby"];
					dbobj.SelectQuery(sql,ref rdr);
					if(rdr.HasRows)
					{
						while(rdr.Read())
						{					
							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),40),
								GenUtil.TrimLength(rdr["Cust_type"].ToString().Trim(),11),
								GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),30),
								rdr["quant"].ToString().Trim(),
								rdr["total_qty"].ToString().Trim(),
								rdr["TotalRate"].ToString().Trim()
								);
							TotalQty_No_Anil+=double.Parse(rdr["quant"].ToString());
							TotalQty_Ltr_Anil+=double.Parse(rdr["total_qty"].ToString());
							TotalNet_Amount_Anil+=double.Parse(rdr["TotalRate"].ToString());
											
						}
					}
					sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
					sw.WriteLine(info1,"Total Sales SSR Anil Agarwal for the period of "+txtDateFrom.Text+" to "+Textbox1.Text,TotalQty_No_Anil.ToString(),TotalQty_Ltr_Anil.ToString(),TotalNet_Amount_Anil.ToString());
					sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
					//07.09.09 sw.WriteLine("Total Sales SSR Anil Agarwal for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
				/******************End*******************************/

				sw.WriteLine();
				/***********For Anil*****************/
				sw.WriteLine("SSR : Gopal Prajapati");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
				sw.WriteLine("|          Customer Name & Place         | Category  |   Product Name With Pack     | Qty Nos.| Qty Ltr.| Total Amount |");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 

				sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1004' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				//sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				if(rdr.HasRows)
				{
						
					while(rdr.Read())
					{					
						sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),40),
							GenUtil.TrimLength(rdr["Cust_type"].ToString().Trim(),11),
							GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),30),
							rdr["quant"].ToString().Trim(),
							rdr["total_qty"].ToString().Trim(),
							rdr["TotalRate"].ToString().Trim()
							);
						TotalQty_No_Gopal+=double.Parse(rdr["quant"].ToString());
						TotalQty_Ltr_Gopal+=double.Parse(rdr["total_qty"].ToString());
						TotalNet_Amount_Gopal+=double.Parse(rdr["TotalRate"].ToString());
					}
				}
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				sw.WriteLine(info1,"Total Sales SSR Gopal Prajapati for the period of "+txtDateFrom.Text+" to "+Textbox1.Text,TotalQty_No_Gopal.ToString(),TotalQty_Ltr_Gopal.ToString(),TotalNet_Amount_Gopal.ToString());
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//07.09.09 vikas sw.WriteLine("Total Sales SSR Gopal Prajapati for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
				/******************End*******************************/
				sw.WriteLine();
				/***********For Anil*****************/
				sw.WriteLine("SSR : Surendra Chauhan");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
				sw.WriteLine("|          Customer Name & Place         | Category  |   Product Name With Pack     | Qty Nos.| Qty Ltr.| Total Amount |");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
				sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1002' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				//sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				if(rdr.HasRows)
				{
						
					while(rdr.Read())
					{					
						sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),40),
							GenUtil.TrimLength(rdr["Cust_type"].ToString().Trim(),11),
							GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),30),
							rdr["quant"].ToString().Trim(),
							rdr["total_qty"].ToString().Trim(),
							rdr["TotalRate"].ToString().Trim()
							);
						TotalQty_No_chauhan+=double.Parse(rdr["quant"].ToString());
						TotalQty_Ltr_chauhan+=double.Parse(rdr["total_qty"].ToString());
						TotalNet_Amount_chauhan+=double.Parse(rdr["TotalRate"].ToString());
					}
				}
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				sw.WriteLine(info1,"Total Sales SSR Surendra Chauhan for the period of "+txtDateFrom.Text+" to "+Textbox1.Text,TotalQty_No_chauhan.ToString(),TotalQty_Ltr_chauhan.ToString(),TotalNet_Amount_chauhan.ToString());
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//07.09.09 vikas sw.WriteLine("Total Sales SSR Surendra Chauhan for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
				/******************End*******************************/
				sw.WriteLine();
				/***********For Anil*****************/
				sw.WriteLine("SSR : Rajkumar Garg");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
				sw.WriteLine("|          Customer Name & Place         | Category  |   Product Name With Pack     | Qty Nos.| Qty Ltr.| Total Amount |");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
				sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1003' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				//sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{					
						sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),40),
							GenUtil.TrimLength(rdr["Cust_type"].ToString().Trim(),11),
							GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),30),
							rdr["quant"].ToString().Trim(),
							rdr["total_qty"].ToString().Trim(),
							rdr["TotalRate"].ToString().Trim()
							);
						TotalQty_No_Rajkumar+=double.Parse(rdr["quant"].ToString());
						TotalQty_Ltr_Rajkumar+=double.Parse(rdr["total_qty"].ToString());
						TotalNet_Amount_Rajkumar+=double.Parse(rdr["TotalRate"].ToString());
					}
				}
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				sw.WriteLine(info1,"Total Sales SSR Rajkumar Garg for the period of "+txtDateFrom.Text+" to "+Textbox1.Text,TotalQty_No_Rajkumar.ToString(),TotalQty_Ltr_Rajkumar.ToString(),TotalNet_Amount_Rajkumar.ToString());
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//07.09.09 vikas sw.WriteLine("Total Sales SSR Rajkumar Garg for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
				/******************End*******************************/
				sw.WriteLine();
				/***********For Anil*****************/
				sw.WriteLine("SSR : Key Customer");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
				sw.WriteLine("|          Customer Name & Place         | Category  |   Product Name With Pack     | Qty Nos.| Qty Ltr.| Total Amount |");
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
				sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1006' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
				//sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				if(rdr.HasRows)
				{
						
					while(rdr.Read())
					{					
						sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),40),
							GenUtil.TrimLength(rdr["Cust_type"].ToString().Trim(),11),
							GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),30),
							rdr["quant"].ToString().Trim(),
							rdr["total_qty"].ToString().Trim(),
							rdr["TotalRate"].ToString().Trim()
							);
						TotalQty_No_KeyCust+=double.Parse(rdr["quant"].ToString());
						TotalQty_Ltr_KeyCust+=double.Parse(rdr["total_qty"].ToString());
						TotalNet_Amount_KeyCust+=double.Parse(rdr["TotalRate"].ToString());
					}
				}
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				sw.WriteLine(info1,"Total Sales SSR Key Customer for the period of "+txtDateFrom.Text+" to "+Textbox1.Text,TotalQty_No_KeyCust.ToString(),TotalQty_Ltr_KeyCust.ToString(),TotalNet_Amount_KeyCust.ToString());
				sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
				//07.09.09 vikas sw.WriteLine("Total Sales SSR Key Customer for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
				/******************End*******************************/
				Grand_TotalNet_Amount=TotalNet_Amount_Anil+TotalNet_Amount_Gopal+TotalNet_Amount_Rajkumar+TotalNet_Amount_chauhan+TotalNet_Amount_KeyCust;
				Grand_TotalQty_Ltr=TotalQty_Ltr_Anil+TotalQty_Ltr_Gopal+TotalQty_Ltr_Rajkumar+TotalQty_Ltr_chauhan+TotalQty_Ltr_KeyCust;
				Grand_TotalQty_No=TotalQty_No_Anil+TotalQty_No_Gopal+TotalQty_No_Rajkumar+TotalQty_No_chauhan+TotalQty_No_KeyCust;

				sw.WriteLine();
				//07.09.09 vikas sw.WriteLine(info,"Total:","","",Grand_TotalQty_No.ToString(),Grand_TotalQty_Ltr.ToString(),Grand_TotalNet_Amount.ToString());
				sw.WriteLine(info1,"Grand Total for the  Period of date from "+txtDateFrom.Text.ToString()+" date to "+Textbox1.Text.ToString()+" Of All SSR",Grand_TotalQty_No.ToString(),Grand_TotalQty_Ltr.ToString(),Grand_TotalNet_Amount.ToString());
				
				dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}

		public void ConvertToExcel_dailySales()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SaleBook.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",strDate="";
			string des="-----------------------------------------------------------------------------------------------------------------------";
			//sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
			//sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
					
			sw.WriteLine("SSR : Anil Agarwal");
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
			sw.WriteLine("Customer Name & Place\tCategory\tProduct Name With Pack\tQty Nos.\tQty Ltr.\tTotal Amount");
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
			//info = " {0,-40:S} {1,-11:S} {2,-30:S} {3,9:S} {4,9:S} {5,14:S} ";
					
			/***********For Anil*****************/
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1001' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
			//sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{					
					sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["Cust_type"].ToString().Trim()+"\t"+
						rdr["prod_name"].ToString().Trim()+"\t"+
						rdr["quant"].ToString().Trim()+"\t"+
						rdr["total_qty"].ToString().Trim()+"\t"+
						rdr["TotalRate"].ToString().Trim()
						);
					TotalQty_No_Anil+=double.Parse(rdr["quant"].ToString());
					TotalQty_Ltr_Anil+=double.Parse(rdr["total_qty"].ToString());
					TotalNet_Amount_Anil+=double.Parse(rdr["TotalRate"].ToString());
											
				}
			}
			sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			sw.WriteLine("Total Sales SSR Anil Agarwal for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text+"\t\t\t"+TotalQty_No_Anil.ToString()+"\t"+TotalQty_Ltr_Anil.ToString()+"\t"+TotalNet_Amount_Anil.ToString());
			sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			
			//07.09.09 sw.WriteLine("Total Sales SSR Anil Agarwal for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
			/******************End*******************************/

			sw.WriteLine();
			/***********For Anil*****************/
			sw.WriteLine("SSR : Gopal Prajapati");
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
			sw.WriteLine("Customer Name & Place\tCategory\tProduct Name With Pack\tQty Nos.\tQty Ltr.\tTotal Amount");
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 

			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1004' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
			//sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{					
					sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["Cust_type"].ToString().Trim()+"\t"+
						rdr["prod_name"].ToString().Trim()+"\t"+
						rdr["quant"].ToString().Trim()+"\t"+
						rdr["total_qty"].ToString().Trim()+"\t"+
						rdr["TotalRate"].ToString().Trim()
						);
					TotalQty_No_Gopal+=double.Parse(rdr["quant"].ToString());
					TotalQty_Ltr_Gopal+=double.Parse(rdr["total_qty"].ToString());
					TotalNet_Amount_Gopal+=double.Parse(rdr["TotalRate"].ToString());
				}
			}
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			sw.WriteLine("Total Sales SSR Gopal Prajapati for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text+"\t\t\t"+TotalQty_No_Gopal.ToString()+"\t"+TotalQty_Ltr_Gopal.ToString()+"\t"+TotalNet_Amount_Gopal.ToString());
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//07.09.09 sw.WriteLine("Total Sales SSR Gopal Prajapati for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
			/******************End*******************************/
			sw.WriteLine();
			/***********For Anil*****************/
			sw.WriteLine("SSR : Surendra Chauhan");
			sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
			sw.WriteLine("Customer Name & Place \t Category\t Product Name With Pack\tQty Nos.\t Qty Ltr.\tTotal Amount |");
			sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1002' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
			//sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.HasRows)
			{
						
				while(rdr.Read())
				{					
					sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["Cust_type"].ToString().Trim()+"\t"+
						rdr["prod_name"].ToString().Trim()+"\t"+
						rdr["quant"].ToString().Trim()+"\t"+
						rdr["total_qty"].ToString().Trim()+"\t"+
						rdr["TotalRate"].ToString().Trim()
						);
					TotalQty_No_chauhan+=double.Parse(rdr["quant"].ToString());
					TotalQty_Ltr_chauhan+=double.Parse(rdr["total_qty"].ToString());
					TotalNet_Amount_chauhan+=double.Parse(rdr["TotalRate"].ToString());
				}
			}
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			sw.WriteLine("Total Sales SSR Surendra Chauhan for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text+"\t\t\t"+TotalQty_No_chauhan.ToString()+"\t"+TotalQty_Ltr_chauhan.ToString()+"\t"+TotalNet_Amount_chauhan.ToString());
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//07.09.09 sw.WriteLine("Total Sales SSR Surendra Chauhan for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
			/******************End*******************************/
			sw.WriteLine();
			/***********For Anil*****************/
			sw.WriteLine("SSR : Rajkumar Garg");
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
			sw.WriteLine(" Customer Name & Place\t Category \t Product Name With Pack \t Qty Nos.\t Qty Ltr.\t Total Amount");
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1003' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
			//sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{					
					sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["Cust_type"].ToString().Trim()+"\t"+
						rdr["prod_name"].ToString().Trim()+"\t"+
						rdr["quant"].ToString().Trim()+"\t"+
						rdr["total_qty"].ToString().Trim()+"\t"+
						rdr["TotalRate"].ToString().Trim()
						);
					TotalQty_No_Rajkumar+=double.Parse(rdr["quant"].ToString());
					TotalQty_Ltr_Rajkumar+=double.Parse(rdr["total_qty"].ToString());
					TotalNet_Amount_Rajkumar+=double.Parse(rdr["TotalRate"].ToString());
				}
			}
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			sw.WriteLine("Total Sales SSR Rajkumar Garg for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text+"\t\t\t"+TotalQty_No_Rajkumar.ToString()+"\t"+TotalQty_Ltr_Rajkumar.ToString()+"\t"+TotalNet_Amount_Rajkumar.ToString());
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//07.08.09 sw.WriteLine("Total Sales SSR Rajkumar Garg for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
			/******************End*******************************/
			sw.WriteLine();
			/***********For Anil*****************/
			sw.WriteLine("SSR : Key Customer");
			sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+");
			sw.WriteLine("Customer Name & Place\tCategory\tProduct Name With Pack \tQty Nos.\tQty Ltr.\tTotal Amount");
			sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//             1234567890123456789012345678901234567890 12345678901 123456789012345678901234567890 123456789 123456789 12345678901234 
			sql="select sb.cust_name+', '+sb.city as cust_name,Cust_type,prod_name+','+pack_type as prod_name,quant,quant*total_qty as total_qty,rate,Quant*rate TotalRate from vw_SaleBook sb, employee e where under_salesman=e.emp_id  and under_salesman='1006' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ToMMddYYYY(Textbox1.Text)+"'";
			//sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.HasRows)
			{
						
				while(rdr.Read())
				{					
					sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["Cust_type"].ToString().Trim()+"\t"+
						rdr["prod_name"].ToString().Trim()+"\t"+
						rdr["quant"].ToString().Trim()+"\t"+
						rdr["total_qty"].ToString().Trim()+"\t"+
						rdr["TotalRate"].ToString().Trim()
						);
					TotalQty_No_KeyCust+=double.Parse(rdr["quant"].ToString());
					TotalQty_Ltr_KeyCust+=double.Parse(rdr["total_qty"].ToString());
					TotalNet_Amount_KeyCust+=double.Parse(rdr["TotalRate"].ToString());
				}
			}
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			sw.WriteLine("Total Sales SSR Key Customer for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text+"\t\t\t"+TotalQty_No_KeyCust.ToString()+"\t"+TotalQty_Ltr_KeyCust.ToString()+"\t"+TotalNet_Amount_KeyCust.ToString());
			//sw.WriteLine("+----------------------------------------+-----------+------------------------------+---------+---------+--------------+"); 
			//07.09.09 vikas sw.WriteLine("Total Sales SSR Key Customer for the period of date from"+txtDateFrom.Text+" date to "+Textbox1.Text);
			/******************End*******************************/
			Grand_TotalNet_Amount=TotalNet_Amount_Anil+TotalNet_Amount_Gopal+TotalNet_Amount_Rajkumar+TotalNet_Amount_chauhan+TotalNet_Amount_KeyCust;
			Grand_TotalQty_Ltr=TotalQty_Ltr_Anil+TotalQty_Ltr_Gopal+TotalQty_Ltr_Rajkumar+TotalQty_Ltr_chauhan+TotalQty_Ltr_KeyCust;
			Grand_TotalQty_No=TotalQty_No_Anil+TotalQty_No_Gopal+TotalQty_No_Rajkumar+TotalQty_No_chauhan+TotalQty_No_KeyCust;

			sw.WriteLine();
			//07.09.09 vikas sw.WriteLine("Total:\t\t\t"+Grand_TotalQty_No.ToString()+"\t"+Grand_TotalQty_Ltr.ToString()+"\t"+Grand_TotalNet_Amount.ToString());
			sw.WriteLine("Grand Total for the  Period of date from "+txtDateFrom.Text.ToString()+" date to "+Textbox1.Text.ToString()+" Of All SSR \t\t\t"+Grand_TotalQty_No.ToString()+"\t"+Grand_TotalQty_Ltr.ToString()+"\t"+Grand_TotalNet_Amount.ToString());
			dbobj.Dispose();
			sw.Close();

		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel_Daily()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SaleBook.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",strDate="";//,strDueDate="";
			if(RadioDetails.Checked)
			{
				if(chkDiscount.Checked)
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and schname='Primary(LTR&% Scheme)' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
				else
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				if(DropSearchBy.SelectedIndex!=0)
				{
					/********Add by vikas 16.11.2012*****************/
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					} /********End*****************/
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							/*string cust_name="";
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";

						}
							
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							/*string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";*/

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							else
								sql=sql+" and Prod_Name like '%"+str[0]+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}
				}
				sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				sw.WriteLine("Search by\t"+DropSearchBy.SelectedItem.Text+"\t Concerning Value\t"+DropValue.Value);
				sw.WriteLine("Customer Name\tInvoice No\tInvoice Date\tProduct Name\tPack Type\tQty in Pkg\tQty in Ltr\tTotal\tTotal Amount");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{					
						sw.WriteLine(rdr["Cust_Name"].ToString()+"\t"+
							rdr["Invoice_No"].ToString()+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["quant"].ToString()+"\t"+
							rdr["total_qty"].ToString()+"\t"+
							System.Convert.ToString(Math.Round(double.Parse(rdr["Rate"].ToString()),2))+"\t"+
							//02.10.09 Coment by vikas GetTotal(rdr["Invoice_No"].ToString())+"\t"+
							GetTotal_Daily(rdr["Invoice_No"].ToString(),rdr["Prod_Name"].ToString(),rdr["Pack_type"].ToString(),rdr["Total_Qty"].ToString(),rdr["TotalRate"].ToString(),rdr["Cust_id"].ToString(),rdr["Sch_Type"].ToString(),rdr["foe"].ToString(),rdr["cash_discount"].ToString(),rdr["cash_disc_type"].ToString()),
							GetTotalAmount(rdr["Invoice_No"].ToString(),rdr["Net_Amount"].ToString())
							);
						TotalQty_Ltr+=double.Parse(rdr["TotalRate"].ToString());
						TotalQtyLtr+=double.Parse(rdr["Total_Qty"].ToString());
						TotalQty+=double.Parse(rdr["quant"].ToString());
					}
				}
				sw.WriteLine("Total\t\t\t\t\t"+TotalQty.ToString()+"\t"+TotalQtyLtr.ToString()+"\t"+total_Total.ToString()+"\t"+total_TotalAmount.ToString());
			}
			sw.Close();
		}

		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SaleBook.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",strDate="";

			/*************************************/
			ArrayList PackVal = new ArrayList(); 
			ArrayList PackVal1 = new ArrayList(); 
			int last_index=0;
			string Spc_Pack="";
			if(DropSearchBy.SelectedIndex==5)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				string[] Totarr=Spc_Pack.Split(new char[] {','});
				for(int i=0;Totarr.Length-1>i;i++)
				{
					string[] Packarr=Totarr[i].Split(new char[] {'X'});
					Spc_Pack=Packarr[1].ToString();
					PackVal.Add(double.Parse(Packarr[1]));
				}
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==6)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==7)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==8)
			{
				Spc_Pack=Session["Spc_PacksSales"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}

			if(RadioDetails.Checked)
			{
				if(chkDiscount.Checked)
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and schname='Primary(LTR&% Scheme)' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text) +"'";
				else
					sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,sb.pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)  +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Textbox1.Text)+"'";
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
							
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							//16.3.2014 sql=sql+" and Pack_Type='"+DropValue.Value+"'";
							sql=sql+" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							//16.3.2014 sql=sql+" and Category='"+DropValue.Value+"'";
							sql=sql+" and Category in ("+Spc_Pack.ToString()+")";
							
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							/*16.3.2014  string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							else
								sql=sql+" and Prod_Name like '%"+str[0]+"%'";*/
							sql=sql+"and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							//16.3.2014 sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
							sql=sql+" and ssr in ("+Spc_Pack.ToString()+")";
					}
				}
				sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				sw.WriteLine("Search by\t"+DropSearchBy.SelectedItem.Text+"\t Concerning Value\t"+DropValue.Value);
				sw.WriteLine("Customer Name\tInvoice No\tInvoice Date\tProduct Name\tPack Type\tQty in Pkg\tQty in Ltr\tRSP\tTotal RSP\tSecon. Discount\tDiscount(%)\tFleet/Oe Discount\tCash Discount\tVat\tTotal\tTotal Amount");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{					
						sw.WriteLine(rdr["Cust_Name"].ToString()+"\t"+
							rdr["Invoice_No"].ToString()+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["quant"].ToString()+"\t"+
							rdr["total_qty"].ToString()+"\t"+
							System.Convert.ToString(Math.Round(double.Parse(rdr["Rate"].ToString()),2))+"\t"+
							rdr["TotalRate"].ToString().Trim()+"\t"+
							GetSchDiscount(rdr["Prod_Name"].ToString(),rdr["Pack_type"].ToString(),rdr["Total_Qty"].ToString(),rdr["TotalRate"].ToString(),rdr["Cust_ID"].ToString(),rdr["Sch_Type"].ToString())+"\t"+
							//GetPerDiscount(rdr["Discount"].ToString(),rdr["Discount_Type"].ToString(),rdr["TotalRate"].ToString(),rdr["foe"].ToString(),rdr["Total_Qty"].ToString(),rdr["InvoiceNo"].ToString())+"\t"+
							GetPerDiscount(rdr["Prod_Name"].ToString(),rdr["Pack_type"].ToString(),rdr["Total_Qty"].ToString(),rdr["TotalRate"].ToString(),rdr["Cust_ID"].ToString(),rdr["Sch_Type"].ToString())+"\t"+
							GetFleetOeDiscount(rdr["foe"].ToString(),rdr["Total_Qty"].ToString())+"\t"+
							GetCashDiscount(rdr["cash_discount"].ToString(),rdr["cash_disc_type"].ToString(),rdr["TotalRate"].ToString())+"\t"+
							GetVat(rdr["Invoice_No"].ToString())+"\t"+
							GetTotal(rdr["Invoice_No"].ToString())+"\t"+
							GetTotalAmount(rdr["Invoice_No"].ToString(),rdr["Net_Amount"].ToString())
							);
						TotalQty_Ltr+=double.Parse(rdr["TotalRate"].ToString());
						TotalQtyLtr+=double.Parse(rdr["Total_Qty"].ToString());
						TotalQty+=double.Parse(rdr["quant"].ToString());
					}
				}
				sw.WriteLine("Total\t\t\t\t\t"+TotalQty.ToString()+"\t"+TotalQtyLtr.ToString()+"\t\t"+TotalQty_Ltr.ToString()+"\t"+total_SchDisc.ToString()+"\t"+total_PerDisc.ToString()+"\t"+total_FleetOe.ToString()+"\t"+GenUtil.strNumericFormat(total_CashDisc.ToString())+"\t"+GenUtil.strNumericFormat(total_Vat.ToString())+"\t"+total_Total.ToString()+"\t"+total_TotalAmount.ToString());
			}
			else
			{
				sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				//**********************
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
							sql=" select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
							sql=" select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub.group_name='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							//coment by vikas 25.05.09 sql=sql+" and cust_Name='"+DropValue.Value+"'";
							/*string cust_name="";
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/
							
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
							
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						/*if(DropValue.Value!="All")
							sql=sql+" and Invoice_No='"+DropValue.Value+"'";*/
						if(DropValue.Value!="All")
							sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where Invoice_No='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category in ("+Spc_Pack.ToString()+")";
							
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							sql=sql+"and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr in ("+Spc_Pack.ToString()+")";
					}
				}
				//**********************
				sql+=" group by invoice_no,cust_name,city,invoice_date,discount,net_amount";
				sql=sql+" order by "+Cache["strorderby"];
				rdr=obj.GetRecordSet(sql);
				sw.WriteLine("From Date\t"+txtDateFrom.Text);
				sw.WriteLine("To Date\t"+Textbox1.Text);
				sw.WriteLine();
				sw.WriteLine("Customer Name\tPlace\tInvoice No\tInvoice Date\tQty in No.\tQty in ltr.\tDiscount\tInvoice Amount");
				while(rdr.Read())
				{
					strDate = rdr["Invoice_Date"].ToString().Trim();
					int pos = strDate.IndexOf(" ");
				
					if(pos != -1)
					{
						strDate = strDate.Substring(0,pos);
					}
					else
					{
						strDate = "";					
					}
                    
					//					strDueDate = rdr["Due_date"].ToString().Trim();
					//					pos = -1;
					//					pos = strDueDate.IndexOf(" ");
					//				
					//					if(pos != -1)
					//					{
					//						strDueDate = strDueDate.Substring(0,pos);
					//					}
					//					else
					//					{
					//						strDueDate = "";					
					//					}
					
					sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["City"].ToString().Trim()+"\t"+
						//rdr["Cust_Type"].ToString().Trim()+"\t"+
						rdr["Invoice_No"].ToString().Trim()+"\t"+
						GenUtil.str2DDMMYYYY(strDate)+"\t"+
						//rdr["Under_SalesMan"].ToString().Trim()+"\t"+
						//rdr["Pack_Type"].ToString().Trim()+"\t"+
						//rdr["Prod_Name"].ToString().Trim()+"\t"+
						rdr["quant"].ToString()+"\t"+
						rdr["TotQty"].ToString()+"\t"+
						//GenUtil.strNumericFormat(rdr["Rate"].ToString().Trim())+"\t"+
						rdr["Discount"].ToString().Trim()+"\t"+
						rdr["Net_Amount"].ToString()
						//	promo,
						//rdr["Cr_Days"].ToString().Trim()+"\t"+
						//GenUtil.str2DDMMYYYY(strDueDate)
						);
					TotalQty_No+=double.Parse(rdr["quant"].ToString());
					TotalQty_Ltr+=double.Parse(rdr["totQty"].ToString());
					TotalNet_Amount+=double.Parse(rdr["Net_Amount"].ToString());
				}
				sw.WriteLine("Total\t\t\t\t"+TotalQty_No.ToString()+"\t"+TotalQty_Ltr.ToString()+"\t\t"+TotalNet_Amount.ToString());
			}
			sw.Close();
		}

		public void ConvertToExcel_Monthly()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SaleBook.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",strDate="";//,strDueDate="";
			if(RadioDetails.Checked)
			{
				sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
						{
							sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							sql="select cust_name+', '+city as cust_name,prod_name,pack_type,sum(quant) quant,sum(quant*total_qty) as total_qty from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text)+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
						{
							/*string cust_name="";
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql=sql+" and cust_Name='"+cust_name.ToString()+"'";*/

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and cust_Name ='"+str[0].ToString()+"'";
							else
								sql=sql+" and cust_Name like '%"+str[0].ToString()+"%'";
						}
							
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
						{
							if(chkDiscount.Checked)
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,sb.discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook sb,oilscheme os where prod_id=prodid and discounttype='%' and scheme1>0 and (schname='Primary(LTR&% Scheme)' or schname='Secondry(LTR Scheme)') and Invoice_No='"+DropValue.Value+"'"; 
							else
								sql="select invoice_no,cust_name+', '+city as cust_name,Cust_ID,invoice_date,quant,quant*total_qty as total_qty,discount,net_amount,prod_name,pack_type,rate,Quant*rate TotalRate,discount_type,InvoiceNo,foe,cash_discount,cash_disc_type,sch_type,scheme1 from vw_SaleBook where Invoice_No='"+DropValue.Value+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
						{
							/*string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";*/

							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							if(str.Length==2)
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							else
								sql=sql+" and Prod_Name like '%"+str[0]+"%'";
						}
					}
					else if(DropSearchBy.SelectedIndex==8)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}
				}
				sql=sql+"  group by prod_name,pack_type,cust_name,city order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				sw.WriteLine("Search by\t"+DropSearchBy.SelectedItem.Text+"\t Concerning Value\t"+DropValue.Value);
				sw.WriteLine("Customer Name\tProduct Name\tPack Type\tQty in Pkg\tQty in Ltr");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{					
						sw.WriteLine(rdr["Cust_Name"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["quant"].ToString()+"\t"+
							rdr["total_qty"].ToString()
							);
						TotalQtyLtr+=double.Parse(rdr["Total_Qty"].ToString());
						TotalQty+=double.Parse(rdr["quant"].ToString());
					}
				}
				sw.WriteLine("Total\t\t\t"+TotalQty.ToString()+"\t"+TotalQtyLtr.ToString());
			}
			sw.Close();
		}

		/// <summary>
		/// This method is used to print the report.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			if(ChkMonthWise.Checked==false)
			{
				if(ChkDaily.Checked==false)
				{
					if(chkDailySales.Checked==true)               //This condition add by vikas 01.09.09
					{
						makingReport_Sales();
						flage=1;
					}
					else
					{
						makingReport();
					}
				}
				else
				{
					makingReport_Daily();
				}
			}
			else
			{
				makingReport_Monthly();
			}

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SalesBookReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click   Sale Book Report   userid  "+uid);
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed    EXCEPTION  "+ ane.Message+" userid  "+  uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed  EXCEPTION  "+ se.Message+"  userid  "+  uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed   EXCEPTION "+es.Message+"  userid  "+  uid);
				}
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed  EXCEPTION   "+ es.Message+"  userid  "+  uid);
			}
		}

		private void GridSalesReport_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This method is used to prepares the excel report file SaleBook.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Coment by vikas 01.09.09 if(GridSalesReport.Visible==true || GridSalesSummerized.Visible==true)
				//Coment by vikas 01.09.09{

				if(ChkMonthWise.Checked==false)
				{
					if(ChkDaily.Checked==false)
					{
						if(chkDailySales.Checked==true)            //This condition add by vikas 01.09.09
						{
							ConvertToExcel_dailySales();
							flage=1;
						}
						else
						{
							ConvertToExcel();
						}
					}
					else
					{
						ConvertToExcel_Daily();
					}
				}
				else
				{
					ConvertToExcel_Monthly();
				}
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Convert Into Excel Format, userid  "+uid);
				//Coment by vikas 01.09.09}
				//Coment by vikas 01.09.09else
				//Coment by vikas 01.09.09{
				//Coment by vikas 01.09.09	MessageBox.Show("Please Click the View Button First");
				//Coment by vikas 01.09.09	return;
				//Coment by vikas 01.09.09}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo(e.Item.Cells[4].Text);
					TotalQtyinLtr(e.Item.Cells[5].Text); 
					TotalNetAmount(e.Item.Cells[7].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_No.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(TotalQty_Ltr.ToString());
					e.Item.Cells[7].Text = GenUtil.strNumericFormat(TotalNet_Amount.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemTotal_Monthly(object sender,DataGridItemEventArgs e)
		{
			try
			{
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo(e.Item.Cells[3].Text);    
					TotalQtyinLtr(e.Item.Cells[4].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[3].Text = GenUtil.strNumericFormat(TotalQty_No.ToString());
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_Ltr.ToString());
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		public void ItemTotal_anil(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo_Anil(e.Item.Cells[3].Text);
					TotalQtyinLtr_Anil(e.Item.Cells[4].Text); 
					TotalNetAmount_Anil(e.Item.Cells[5].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[3].Text = GenUtil.strNumericFormat(TotalQty_No_Anil.ToString());
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_Ltr_Anil.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(TotalNet_Amount_Anil.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}
		public void ItemTotal_Gopal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo_Gopal(e.Item.Cells[3].Text);
					TotalQtyinLtr_Gopal(e.Item.Cells[4].Text); 
					TotalNetAmount_Gopal(e.Item.Cells[5].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[3].Text = GenUtil.strNumericFormat(TotalQty_No_Gopal.ToString());
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_Ltr_Gopal.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(TotalNet_Amount_Gopal.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		public void ItemTotal_Chauhan(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo_Chauhan(e.Item.Cells[3].Text);
					TotalQtyinLtr_Chauhan(e.Item.Cells[4].Text); 
					TotalNetAmount_Chauhan(e.Item.Cells[5].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[3].Text = GenUtil.strNumericFormat(TotalQty_No_chauhan.ToString());
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_Ltr_chauhan.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(TotalNet_Amount_chauhan.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		public void ItemTotal_Rajkumar(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo_Rajkumar(e.Item.Cells[3].Text);
					TotalQtyinLtr_Rajkumar(e.Item.Cells[4].Text); 
					TotalNetAmount_Rajkumar(e.Item.Cells[5].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[3].Text = GenUtil.strNumericFormat(TotalQty_No_Rajkumar.ToString());
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_Ltr_Rajkumar.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(TotalNet_Amount_Rajkumar.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		public void ItemTotal_KeyCustmer(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo_KeyCust(e.Item.Cells[3].Text);
					TotalQtyinLtr_KeyCust(e.Item.Cells[4].Text); 
					TotalNetAmount_KeyCust(e.Item.Cells[5].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[3].Text = GenUtil.strNumericFormat(TotalQty_No_KeyCust.ToString());
					e.Item.Cells[4].Text = GenUtil.strNumericFormat(TotalQty_Ltr_KeyCust.ToString());
					e.Item.Cells[5].Text = GenUtil.strNumericFormat(TotalNet_Amount_KeyCust.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemTotal1(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinLtr(e.Item.Cells[7].Text); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[7].Text = GenUtil.strNumericFormat(TotalQty_Ltr.ToString());
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This method is used to calculates the total Qty in No. by passing value 
		/// </summary>
		protected void TotalQtyinNo(string _QtyInNo)
		{
			TotalQty_No  += double.Parse(_QtyInNo); 
		}


		/***********Start Add by vikas 31.08.09******************************************/
		protected void TotalQtyinNo_Anil(string _QtyInNo)
		{
			TotalQty_No_Anil  += double.Parse(_QtyInNo); 
			Grand_TotalQty_No += double.Parse(_QtyInNo); 
		}

		protected void TotalQtyinNo_Gopal(string _QtyInNo)
		{
			TotalQty_No_Gopal  += double.Parse(_QtyInNo); 
			Grand_TotalQty_No += double.Parse(_QtyInNo); 
		}

		protected void TotalQtyinNo_Chauhan(string _QtyInNo)
		{
			TotalQty_No_chauhan  += double.Parse(_QtyInNo); 
			Grand_TotalQty_No += double.Parse(_QtyInNo); 
		}

		protected void TotalQtyinNo_Rajkumar(string _QtyInNo)
		{
			TotalQty_No_Rajkumar  += double.Parse(_QtyInNo); 
			Grand_TotalQty_No += double.Parse(_QtyInNo); 
		}

		protected void TotalQtyinNo_KeyCust(string _QtyInNo)
		{
			TotalQty_No_KeyCust  += double.Parse(_QtyInNo); 
			Grand_TotalQty_No += double.Parse(_QtyInNo); 
		}
		/******************End**************************************/


		/// <summary>
		/// This method is used to calculates total Qty in Ltr by passing value 
		/// </summary>
		
		/******Add by vikas 31.08.09***Start*****************************/
		protected void TotalQtyinLtr_Anil(string _QtyInLtr)
		{
			TotalQty_Ltr_Anil  += double.Parse(_QtyInLtr);
			Grand_TotalQty_Ltr += double.Parse(_QtyInLtr); 
		}

		protected void TotalQtyinLtr_Gopal(string _QtyInLtr)
		{
			TotalQty_Ltr_Gopal  += double.Parse(_QtyInLtr); 
			Grand_TotalQty_Ltr += double.Parse(_QtyInLtr); 
		}

		protected void TotalQtyinLtr_Chauhan(string _QtyInLtr)
		{
			TotalQty_Ltr_chauhan  += double.Parse(_QtyInLtr); 
			Grand_TotalQty_Ltr += double.Parse(_QtyInLtr); 
		}

		protected void TotalQtyinLtr_Rajkumar(string _QtyInLtr)
		{
			TotalQty_Ltr_Rajkumar  += double.Parse(_QtyInLtr); 
			Grand_TotalQty_Ltr += double.Parse(_QtyInLtr); 
		}

		protected void TotalQtyinLtr_KeyCust(string _QtyInLtr)
		{
			TotalQty_Ltr_KeyCust  += double.Parse(_QtyInLtr); 
			Grand_TotalQty_Ltr += double.Parse(_QtyInLtr); 
		}
		/*************End*************************/
		
		
		protected void TotalQtyinLtr(string _QtyInLtr)
		{
			TotalQty_Ltr  += double.Parse(_QtyInLtr); 
		}
		/// <summary>
		/// This method is used to calculates total Qty in Ltr by passing value 
		/// </summary>
		public string TotalQtyinLtr1(string Qty, string TotQty)
		{
			double Tot  = double.Parse(Qty)*double.Parse(TotQty); 
			TotalQty_Ltr += Tot;
			return Tot.ToString();
		}
		
		/// <summary>
		/// This method is used to calculates total Net Amount by passing value 
		/// </summary>
		protected void TotalNetAmount(string _NetAmount)
		{
			TotalNet_Amount  += double.Parse(_NetAmount); 
		}


		protected void TotalNetAmount_Anil(string _NetAmount)
		{
			TotalNet_Amount_Anil  += double.Parse(_NetAmount); 
			
			Grand_TotalNet_Amount += double.Parse(_NetAmount); 
		}
		protected void TotalNetAmount_Gopal(string _NetAmount)
		{
			TotalNet_Amount_Gopal  += double.Parse(_NetAmount);
			Grand_TotalNet_Amount += double.Parse(_NetAmount); 
		}
		protected void TotalNetAmount_Chauhan(string _NetAmount)
		{
			TotalNet_Amount_chauhan  += double.Parse(_NetAmount); 
			Grand_TotalNet_Amount += double.Parse(_NetAmount); 
		}
		protected void TotalNetAmount_Rajkumar(string _NetAmount)
		{
			TotalNet_Amount_Rajkumar  += double.Parse(_NetAmount); 
			Grand_TotalNet_Amount += double.Parse(_NetAmount); 
		}
		protected void TotalNetAmount_KeyCust(string _NetAmount)
		{
			TotalNet_Amount_KeyCust  += double.Parse(_NetAmount); 
			Grand_TotalNet_Amount += double.Parse(_NetAmount); 
		}
		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value from dropdownlist.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strName="",strInvoiceNo="",strType="",strProductGroup="",strProdWithPack="",strSSR="",strPackType="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012

				//strType = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				//coment by vikas 16.11.2012 strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				
				//coment by vikas 25.05.09 strName="select distinct Cust_Name from vw_salebook order by cust_Name";
				strName="select distinct Cust_Name,city from vw_salebook order by cust_Name,city";
				
				//strInvoiceNo="select distinct Invoice_No from vw_salebook order by invoice_no"; //Comment By Vikas Sharma 16.04.09
				strInvoiceNo="select distinct cast(Invoice_No as int) Invoice_No from vw_salebook order by invoice_no"; //Add By Vikas Sharma 16.04.09

				strProductGroup="select distinct category from vw_salebook order by category";
				strPackType="select distinct Pack_Type from vw_salebook order by Pack_Type";
				strProdWithPack="select distinct Prod_Name+':'+pack_type from vw_salebook order by prod_name+':'+pack_type";
				//strSSR="select distinct Under_Salesman from vw_salebook order by under_salesman";
				strSSR="select Emp_Name from Employee where designation='Servo Sales representative' and status=1 order by Emp_name";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012

				//Coment by vikas 16.11.2012 string[] arrStr = {strName,strType,strInvoiceNo,strPackType,strProductGroup,strProdWithPack,strSSR,strGroup,strSubGroup};
				//Coment by vikas 16.11.2012 HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempInvoiceNo,tempPackType,tempProductGroup,tempProdWithPack,tempSSR};

				string[] arrStr = {strName,strInvoiceNo,strPackType,strProductGroup,strProdWithPack,strSSR,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempCustName,tempInvoiceNo,tempPackType,tempProductGroup,tempProdWithPack,tempSSR,tempGroup,tempSubGroup};

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
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
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Sale Book Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
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
				SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"'");
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
		/// This method return the calculated discount.
		/// </summary>
		public double total_SchDisc=0,total_PerDisc=0,total_FleetOe=0,total_Vat=0,total_Total=0,total_TotalAmount=0,total_CashDisc=0;
		//public string GetPerDiscount(string Disc,string DiscType,string TotalRate,string foe,string TotalQty,string InvoiceNo)
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
				//SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"'");
				SqlDataReader rdr = obj.GetRecordSet("select discount,discounttype from oilscheme where schprodid=0 and prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"'");
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
			/*double localval=0;
			int count=0;
			if(double.Parse(Disc)>0)
			{
				if(DiscType=="Rs")
				{
					dbobj.ExecuteScalar("select count(*) from sales_details where invoice_no='"+InvoiceNo+"'",ref count);
					localval=double.Parse(Disc)/count;
				}
				else
				{
					string FleetOe=GetTempFleetOeDiscount(foe,TotalQty);
					localval=(double.Parse(TotalRate)-double.Parse(FleetOe)-SchDisc)*double.Parse(Disc)/100;
				}
			}
			PerDisc=localval;
			total_PerDisc+=localval;
			TotalVat+=double.Parse(TotalRate);
			//string FleetOe=GetTempFleetOeDiscount(ProdName,PackType,CustID,TotalQty);
			//localval=((localval-SchDisc)*double.Parse(TotalRate))/100;
			//localval=(localval*double.Parse(TotalRate))/100;
			TotalVat-=localval;
			return System.Convert.ToString(Math.Round(localval,1));*/
		}

		/// <summary>
		/// This method return the calculated FOE discount.
		/// </summary>
		public string GetFleetOeDiscount(string foe,string TotalQty)
		{
			//			InventoryClass obj = new InventoryClass();
			double localval=0;
			//			//SqlDataReader rdr = obj.GetRecordSet("select discount from foe where prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and CustID='"+CustID+"' and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"'");
			//			SqlDataReader rdr = obj.GetRecordSet("select discount from foe where prodid=(select prod_id from products where prod_Name='"+ProdName+"' and pack_type='"+PackType+"') and CustID='"+CustID+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' or cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"') and (cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"' or cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"')");
			//			if(rdr.Read())
			//			{
			//				localval=double.Parse(rdr["discount"].ToString());
			//			}
			//			rdr.Close();
			//			localval=localval*double.Parse(TotalQty);
			localval=double.Parse(foe)*double.Parse(TotalQty);
			FleetoeDisc=localval;
			total_FleetOe+=localval;
			TotalVat-=localval;
			return System.Convert.ToString(Math.Round(localval,1));
		}

		/// <summary>
		/// This method is temporary used to return the calculated FOE discount.
		/// </summary>
		public string GetTempFleetOeDiscount(string foe,string TotalQty)
		{
			double localval=0;
			localval=double.Parse(foe)*double.Parse(TotalQty);
			return GenUtil.strNumericFormat(localval.ToString());
		}

		/// <summary>
		/// This method calculate the cash discount.
		/// </summary>
		public string GetCashDiscount(string cashdisc,string cashdisctype,string TotalRate)
		{
			double localval=0;
			if(double.Parse(cashdisc)>0)
			{
				if(cashdisctype=="Rs")
					localval=double.Parse(cashdisc);
				else
				{
					localval=double.Parse(TotalRate)-(SchDisc+FleetoeDisc+PerDisc);
					localval=localval*double.Parse(cashdisc)/100;
				}
				
			}
			else
				localval=double.Parse(cashdisc);
			total_CashDisc+=localval;
			TotalVat-=localval;
			//return System.Convert.ToString(Math.Round(localval,1));
			return GenUtil.strNumericFormat(localval.ToString());
		}

		/// <summary>
		/// This method return the calculated vat.
		/// </summary>
		public string GetVat(string InvoiceNo)
		{
			double localval=0;
			localval=TotalVat*12.5/100;
			TotalVat+=localval;
			total_Vat+=localval;
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
		/// This method is used to calculate the total vat value.
		/// </summary>
		//Invoice_No,Prod_Name,Pack_type,Total_Qty,TotalRate,Cust_id,Sch_Type,foe,cash_discount,cash_disc_type
		public string GetTotal_Daily(string Invoice_No,string Prod_Name,string Pack_type,string Total_Qty,string TotalRate,string Cust_id,string Sch_Type,string foe,string cash_discount,string cash_disc_type)
		{
			string a="",b="",c="",d="",e="";
			
			a=GetSchDiscount(Prod_Name,Pack_type,Total_Qty,TotalRate,Cust_id,Sch_Type);

			b=GetPerDiscount(Prod_Name,Pack_type,Total_Qty,TotalRate,Cust_id,Sch_Type);

			c=GetFleetOeDiscount(foe,Total_Qty);

			d=GetCashDiscount(cash_discount,cash_disc_type,TotalRate);

			e=GetVat(Invoice_No);

			total_Total+=Math.Round(TotalVat);
			return System.Convert.ToString(Math.Round(TotalVat));
		}

		/// <summary>
		/// This method is used to calculate the total net amount.
		/// </summary>
		string Invoice_No="";
		public string GetTotalAmount(string InvoiceNo,string NetAmount)
		{
			if(Invoice_No!=InvoiceNo)
			{
				Invoice_No=InvoiceNo;
				total_TotalAmount+=double.Parse(NetAmount);
				return NetAmount;
			}
			else
				return "";
		}

		/// <summary>
		/// This method is used to calculate the total qty
		/// </summary>
		public double TotalQty=0;
		public string TotalSumQty(string qty)
		{
			TotalQty+=double.Parse(qty);
			return qty;
		}

		/// <summary>
		/// This method is used to calculate the total qty in liter
		/// </summary>
		public double TotalQtyLtr=0;
		public string TotalSumQtyinLtr(string qtyltr)
		{
			TotalQtyLtr+=double.Parse(qtyltr);
			return qtyltr;
		}
	}
}