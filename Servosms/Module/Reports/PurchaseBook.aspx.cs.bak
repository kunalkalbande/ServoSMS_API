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
using RMG;
using Servosms.Sysitem.Classes;
using System.Data.SqlClient;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for PurchaseBook.
	/// </summary>
	public partial class PurchaseBook : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		//**********
		//string strOrderBy="";
		public string totinv_no="";
		public double[] TotalDisc = new double[7];
		//***************
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
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				grdDetails.Visible=false;
				GridReport.Visible=false;
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="34";
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
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            Textbox1.Text = Request.Form["Textbox1"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["Textbox1"].ToString().Trim();
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
		/// This method is used to first split the given date and return the date in 'MM/dd/yyyy' format
		/// </summary>
		# region DateTime Function...
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.Split(new char[]{'/'},str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}
		# endregion

		/// <summary>
		/// This is used to make sorting the datagrid on clicking of the datagridheader.
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
				Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		public double Elgible_Discount(string prod_id)
		{
			//string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid=prod_id;
			double dis;
			System.Data.SqlClient.SqlDataReader rdr=null;
			
			/*string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();*/
			
			//coment by vikas sharma 22.05.09 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select discount,discountType from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
			{
				dis=0;
			}
			rdr.Close();
			return dis;
		}

		public string Elgible_Discount_Type(string prod_id )
		{
			//string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid=prod_id;
			string dis;
			System.Data.SqlClient.SqlDataReader rdr=null;
			
			//coment by vikas sharma 22.05.09 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select discount,discountType from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				if(rdr.GetValue(1).ToString()=="%")
					dis=rdr.GetValue(1).ToString();
				else
					dis="";
			}
			else
			{
				dis="";
			}
			rdr.Close();
			return dis;
		}
		double Disoe=0;
		public string Dis_in_Amount(string RSP,string Discount)
		{
			string Tot="0";
			//if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				Tot=System.Convert.ToString(Math.Round((System.Convert.ToDouble(RSP)*System.Convert.ToDouble(Discount)/100),2));
		//	else
			//	Tot=System.Convert.ToString(Math.Round(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty),2));
			Disoe+=double.Parse(Tot);
			Cache["Disoe"]=Disoe.ToString();
			return Tot;
		}
		//double Disoe1=0;
		public string Dis_in_Amount1(string RSP,string Discount)
		{
			string Tot="0";
			//if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
			Tot=System.Convert.ToString(Math.Round((System.Convert.ToDouble(RSP)*System.Convert.ToDouble(Discount)/100),2));
			//	else
			//	Tot=System.Convert.ToString(Math.Round(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty),2));
			//Disoe1+=double.Parse(Tot);
			//Cache["Disoe"]=Disoe.ToString();
			return Tot;
		}

		double total13oe;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double Claim_Amount(string D_Amount,string R_CFA)
		{
			double tot=0;
			//if(System.Convert.ToDouble(Disgiv)>(foc+sch))
			tot=System.Convert.ToDouble(D_Amount)-System.Convert.ToDouble(R_CFA);
			total13oe+=tot;
			Cache["total13oe"]=total13oe;
			return Math.Round(tot,2);
		}
		
		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			//			string sql="(select * from vw_PurchaseBook1 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"') union (select * from vw_PurchaseBook2 where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(txtDateFrom.Text).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime)<= '"+ ToMMddYYYY(Textbox1.Text)+"')";		
			string sql="";
			if(RadioSumrized.Checked)
				sql="select * from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"' ";		
			
				/******* add by vikas 04.06.09**********/
			else if(RadPerclaim.Checked)
			{
				//04.06.09 sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				 //coment by vikas 27.12.2012 sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc,cast(perdisc as varchar)+perdisctype DiscType,((perdisc*qty*price)/100) Disc_Amount from vw_PurchaseBook3 where prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				//prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and
			}
				/******** end *********/

			else
			{
				if(chkDiscount.Checked)
					//sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_Invoice_Date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then qty*total_qty*tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount from vw_PurchaseBook3,per_discount where fixed_discount_type>'0' and prod_id=prodid and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
					sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_Invoice_Date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3,per_discount where fixed_discount_type>'0' and prod_id=prodid and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				else
					//sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then qty*total_qty*tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
					sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
			}
			if(DropSearchBy.SelectedIndex!=0)
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Vendor_Name like '"+DropValue.Value+"%'";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					/*if(DropValue.Value!="All")
						sql=sql+" and Vndr_Invoice_No='"+DropValue.Value+"'";*/ // Comment By Vikas Sharma 16.04.09
					if(DropValue.Value!="All")
					{
						if(RadioSumrized.Checked)
							sql="select * from vw_PurchaseBook3 where Vndr_Invoice_No='"+DropValue.Value+"'";		
						else
						{
							if(chkDiscount.Checked)
								sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_Invoice_Date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3,per_discount where fixed_discount_type>'0' and prod_id=prodid and Vndr_Invoice_No='"+DropValue.Value+"'";
							else
								sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where Vndr_Invoice_No='"+DropValue.Value+"'";
						}
					}
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Pack_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value!="All")
					{
						//string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						//sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
						//Coment By vikas 3.5.2013 sql=sql+" and Prod_Name='"+DropValue.Value+"'";
						sql=sql+" and Prod_Name like '%"+DropValue.Value+"%'";
					}
				}
				
			}
			sql+=" order by Prod_name,Vndr_Invoice_No";
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();
			//da.Fill(ds,"vw_PurchaseBook1");
			da.Fill(ds,"vw_PurchaseBook3");
			//DataTable dtcustomer=ds.Tables["vw_PurchaseBook1"];
			DataTable dtcustomer=ds.Tables["vw_PurchaseBook3"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			if(RadioSumrized.Checked)
				GridReport.DataSource=dv;
				/********Coment by vikas 04.06.09 ****************/
			else if(RadPerclaim.Checked)
				GridClaim.DataSource=dv;
				/**********end**************/
			else
				grdDetails.DataSource=dv;
			if(dv.Count!=0)
			{
				if(RadioSumrized.Checked)
				{
					GridReport.DataBind();
					GridReport.Visible=true;
					grdDetails.Visible=false;
					GridClaim.Visible=false;
				}
					/********Coment by vikas 04.06.09 ****************/
				else if(RadPerclaim.Checked)
				{
					GridClaim.DataBind();
					GridClaim.Visible=true;
					GridReport.Visible=false;
					grdDetails.Visible=false;
					/**********end**************/
				}
				else
				{
					grdDetails.DataBind();
					GridReport.Visible=false;
					grdDetails.Visible=true;
					GridClaim.Visible=false;
				}
			}
			else
			{
				GridReport.Visible=false;
				grdDetails.Visible=false;
				GridClaim.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}

		/// <summary>
		/// This method is used to view the Purchase Book report with the help of Bindthedata() function and
		/// set the column name with ascending order in Session variable.
		/// </summary>
		# region Show Report Button...
		protected void btnShow_Click(object sender, System.EventArgs e)
		{    
			try
			{
				if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(Textbox1.Text))>0)
				{
					MessageBox.Show("Date From Should be less than Date To");
					GridReport.Visible=false;
				}
				else
				{
					//				PetrolPumpClass  obj=new PetrolPumpClass();
					//				SqlDataReader SqlDtr;
					//				string sql;
					//				#region Bind DataGrid
					//				sql="(select * from vw_PurchaseBook1 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"') union (select * from vw_PurchaseBook2 where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(txtDateFrom.Text).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime)<= '"+ ToMMddYYYY(Textbox1.Text)+"')";		
					//
					//				SqlDtr =obj.GetRecordSet(sql);
					//				GridReport.DataSource=SqlDtr;
					//				GridReport.DataBind();
					//				if(GridReport.Items.Count==0)
					//				{
					//					MessageBox.Show("Data not available");
					//					GridReport.Visible=false;
					//				}
					//				else
					//				{
					//				   GridReport.Visible=true;
					//				}
					//				SqlDtr.Close();
					//				#endregion
					strorderby="Vndr_Invoice_no ASC";
					Session["Column"]="Vndr_Invoice_no";
					Session["order"]="ASC";
					Bindthedata();
				}
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:btnShow_Click,Class:DbOperation_LETEST.cs ,   Purchase Book Report Viewed  usrid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:btnShow_Click,Class:DbOperation_LETEST.cs , Purchase Book Report Viewed  EXCEPTION  " + ex.Message+" userid "+uid);
			}
		}
		#endregion  
		//**************
		
		/// <summary>
		/// This method is used to calculate total invoice no.
		/// </summary>
		public void totalinvoiceno()
		{
			PetrolPumpClass  obj=new PetrolPumpClass();
			SqlDataReader SqlDtr;
			string sql;
			//int count1=0;
			//**sql="(select count(Invoice_No) from vw_PurchaseBook1 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"') union (select * from vw_PurchaseBook2 where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(txtDateFrom.Text).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime)<= '"+ ToMMddYYYY(Textbox1.Text)+"')";		
			sql="select count(Invoice_No) from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"'";		
			SqlDtr =obj.GetRecordSet(sql);
			/*
			while(
			if(count>0)
			{
				Cache["totin_no"]=count;
			}
			*/
			SqlDtr.Close();
		}
		//***************
		
		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		public double os=0,os1=0,in_amt=0;
		protected double Multiply(string str)
		{
			//*******
			string[] str1=str.Split(new char[] {':'},str.Length);
			//*******
			string[] mystr=str1[1].Split(new char[]{'X'},str1[1].Length);
			// check the package type is loose or not.
			if(str1[0].Trim().IndexOf("Fuel") == -1)
			{
				if(str.Trim().IndexOf("Loose") == -1)
				{
					double ans=1;
					foreach(string val in mystr)
					{
						if(val.Length>0 && !val.Trim().Equals(""))
							ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
					}
					//******
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					//******
					return ans;
				}
				else
				{
					if(!mystr[1].Trim().Equals(""))
					{
						//*******
						os+=System.Convert.ToDouble( mystr[1].ToString());
						Cache["os"]=System.Convert.ToString(os);
						//*******
						return System.Convert.ToDouble( mystr[1].ToString()); 
					}
					else
					{
						os=0;
						Cache["os"]=System.Convert.ToString("0");
						return 0;
					}
					
				}
			}
			else
			{
				os+=System.Convert.ToDouble( mystr[1].ToString())*1000;
				Cache["os"]=System.Convert.ToString(os);
				//return System.Convert.ToDouble( mystr[1].ToString()); 	
				return os;
			}
		}
		
		public double am=0;
		//double amt=0;
		double amt1=0;//,amt2=0;
		int count=0,i=0,status=0,Flag=0;
		/// <summary>
		/// This Method is used to multiplies the package quantity with Quantity in every product according 
		/// to given in database.
		/// </summary>
		protected string Multiply1(string inv_no,string inv,string Net_Amount)
		{
			//PetrolPumpClass  obj=new PetrolPumpClass();
			//SqlDataReader SqlDtr;
			//string sql;
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
				/*sql = "select count(*) from vw_PurchaseBook3  where Vndr_Invoice_No="+Cache["Invoice_No"].ToString()+" and cast(floor(cast(vndr_invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(vndr_invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"'";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					count += int.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				*/
				dbobj.ExecuteScalar("select count(*) from vw_PurchaseBook3  where Vndr_Invoice_No="+Cache["Invoice_No"].ToString()+" and cast(floor(cast(vndr_invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(vndr_invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"'",ref count);
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
				amt1=0;
				/*
				sql = "select Net_amount from Purchase_master where vndr_Invoice_No="+Cache["Invoice_No"].ToString()+" and cast(floor(cast(vndr_invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(vndr_invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"' ";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					amt1 += double.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				*/
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
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6,ref int len7,ref int len8,ref int len9,ref int len10,ref int len11,ref int len12,ref int len13)
		{
			while(rdr.Read())
			{
				if(rdr["Vendor_ID"].ToString().Trim().Length>len1)
					len1=rdr["Vendor_ID"].ToString().Trim().Length;					
				if(rdr["Vendor_Name"].ToString().Trim().Length>len2)
					len2=rdr["Vendor_Name"].ToString().Trim().Length;					
				if(rdr["Place"].ToString().Trim().Length>len3)
					len3=rdr["Place"].ToString().Trim().Length;
				if(rdr["Vendor_Type"].ToString().Trim().Length>len4)
					len4=rdr["Vendor_Type"].ToString().Trim().Length;					
				if(rdr["Invoice_No"].ToString().Trim().Length>len5)
					len5=rdr["Invoice_No"].ToString().Trim().Length;					
				if(rdr["Invoice_Date"].ToString().Trim().Length>len6)
					len6=rdr["Invoice_Date"].ToString().Trim().Length;	
				if(rdr["Prod_Type"].ToString().Trim().Length>len7)
					len7=rdr["Prod_Type"].ToString().Trim().Length;	
				if(rdr["Prod_Name"].ToString().Trim().Length>len8)
					len8=rdr["Prod_Name"].ToString().Trim().Length;	
				if(rdr["Qty"].ToString().Trim().Length>len9)
					len9=rdr["Qty"].ToString().Trim().Length;	
				if(rdr["Price"].ToString().Trim().Length>len10)
					len10=rdr["Price"].ToString().Trim().Length;	
				if(rdr["Discount"].ToString().Trim().Length>len11)
					len11=rdr["Discount"].ToString().Trim().Length;	
				if(rdr["Promo_Scheme"].ToString().Trim().Length>len12)
					len12=rdr["Promo_Scheme"].ToString().Trim().Length;	
				if(rdr["Cr_Days"].ToString().Trim().Length>len13)
					len13=rdr["Cr_Days"].ToString().Trim().Length;	
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
		/// Method to prepare the report file .txt.
		/// </summary>
		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PurchaseBookReport.txt";
				StreamWriter sw = new StreamWriter(path);

				string sql="";
				string info = "";
				string strDate    = "";
				string strDueDate = "";
				//string promo = "";
			
				//	sql="(select * from vw_PurchaseBook1 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"') union (select * from vw_PurchaseBook2 where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(txtDateFrom.Text).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime)<= '"+ ToMMddYYYY(Textbox1.Text)+"')";		
				//*******
				//	sql="(select * from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"') union (select * from vw_PurchaseBook2 where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(txtDateFrom.Text).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime)<= '"+ ToMMddYYYY(Textbox1.Text)+"')";		
				if(RadioSumrized.Checked)
					sql="select * from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"' ";	
					
					/******* add by vikas 04.06.09**********/
				else if(RadPerclaim.Checked)
				{
					//coment by vikas 27.12.2012 sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
					sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc,cast(perdisc as varchar)+' '+perdisctype DiscType,((perdisc*qty*price)/100) Disc_Amount from vw_PurchaseBook3 where prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				}
					/******** end *********/

				else
				{
					if(chkDiscount.Checked)
						sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,trade_discount,foc from vw_PurchaseBook3,per_discount where prod_id=prodid and fixed_discount_type>'0' and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
					else
						sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,trade_discount,foc from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				}
				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Vendor_Name like '"+DropValue.Value+"%'";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						/*if(DropValue.Value!="All")
						sql=sql+" and Vndr_Invoice_No='"+DropValue.Value+"'";*/ // Comment By Vikas Sharma 16.04.09
						if(DropValue.Value!="All")
						{
							if(RadioSumrized.Checked)
								sql="select * from vw_PurchaseBook3 where Vndr_Invoice_No='"+DropValue.Value+"'";		
							else
							{
								if(chkDiscount.Checked)
									sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_Invoice_Date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3,per_discount where fixed_discount_type>'0' and prod_id=prodid and Vndr_Invoice_No='"+DropValue.Value+"'";
								else
									sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where Vndr_Invoice_No='"+DropValue.Value+"'";
							}
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Prod_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
						{
							//Coment By vikas 3.5.2013 sql=sql+" and Prod_Name='"+DropValue.Value+"'";
							sql=sql+" and Prod_Name like '%"+DropValue.Value+"%'";
						}
					}
				}
				sql=sql+" order by "+Cache["strorderby"];
				//*******
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
				string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				//sw.WriteLine("+-------------+---------+-----------+------+----------+---------+--------------+----------+--------+-------+------------+----+----------+");
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("PURCHASE BOOK REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				//				sw.WriteLine("+------+---------------------------+---------------+-----------+------+----------+----------------------+------------------------------+--------+-----------+--------+--------------------+-------+----------+");
				//				sw.WriteLine("|Ven.ID|       Vendor Name         |    Place      |Vendor Type|Inv.No|Inv. Date |     Product Type     |        Product Name          |Quantity|  Price    | Disc.  |   Invoice Amount   |Cr.Days| Due Date |");
				//				sw.WriteLine("+------+---------------------------+---------------+-----------+------+----------+----------------------+------------------------------+--------+-----------+--------+--------------------+-------+----------+");
				//				//             123456 1234567890123456789012345 123456789012345 12345678901 123456 1234567890 123456789012 1234567890123456789012345 12345678 12345678901 12345678 12345678901234567890 1234567 1234567890
				if(RadioSumrized.Checked)
				{
					sw.WriteLine("+-------------+---------+-----+---------+----------+---------+--------------+----------+--------+----------+------------+----+----------+");
					sw.WriteLine("| Vendor Name |  Place  |Ven. | Inv.No  | Invoice  | Product |   Product    | Quantity | Price  | Discount |  Invoice   | Cr.| Due Date |");
					sw.WriteLine("|             |         |Type |         |  Date    |  Type   |    Name      |  in ltr. |        |          |  Amount    |Days|          |");                      
					sw.WriteLine("+-------------+---------+-----+---------+----------+---------+--------------+----------+--------+----------+------------+----+----------+");
					//             1234567890123 123456789 12345 123456789 1234567890 123456789 12345678901234 1234567890 12345678 1234567890 123456789012 1234 1234567890
					// info : to set the fields format to display in reports.
					info = " {0,-13:S} {1,-9:S} {2,-5:S} {3,9:S} {4,10:F} {5,-9:S} {6,-14:S} {7,10:S} {8,8:F} {9,10:F} {10,12:S} {11,4:S} {12,10:D}";
				}
				/*********Start Add by vikas 05.06.09**********************/
				else if(RadPerclaim.Checked)
				{
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+----------+-----------+-----------+------------+------------+");
					sw.WriteLine("| Vendor Name | Inv. No | Invoice  | Product | Product Name |Qty | Qty  |  Price   | Eligible  | Eli.Disc. |  Discount  |  Claimable |");
					sw.WriteLine("|             |         |   Date   |  Type   |              |No. | Ltr  |          | Dis.in %  | in Amount |  frm. CFA  |   Discount |");                      
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+----------+-----------+-----------+------------+------------+");
					//             1234567890123 123456789 1234567890 123456789 12345678901234 1234 123456 1234567890 12345678901 12345678901 123456789012 123456789012 
					info = " {0,-13:S} {1,-9:S} {2,-10:S} {3,-9:S} {4,-14:S} {5,4:F} {6,6:S} {7,10:S} {8,11:S} {9,11:S} {10,12:S} {11,12:S} ";
				}
				/*******************************/
				else
				{
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+--------+--------+-------+--------+---------+-------+----------+");
					sw.WriteLine("| Vendor Name | Inv. No | Invoice  | Product | Product Name |Qty | Qty  | Price  | Trade  | Early |  FOC   |  Cash   | Disc. |  Total   |");
					sw.WriteLine("|             |         |   Date   |  Type   |              |No. | Ltr  |        |Discount| Bird  |Discount|Discount |  (%)  | Discount |");                      
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+--------+--------+-------+--------+---------+-------+----------+");
					//             1234567890123 123456789 1234567890 123456789 12345678901234 1234 123456 12345678 12345678 1234567 12345678 123456789 1234567 1234567890
					// info : to set the fields format to display in reports.
					info = " {0,-13:S} {1,-9:S} {2,-10:S} {3,-9:S} {4,-14:S} {5,4:F} {6,6:S} {7,8:S} {8,8:S} {9,7:F} {10,8:F} {11,9:S} {12,7:S} {13,10:D}";
				}
			
				
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						if(RadioSumrized.Checked)
						{
							// Trime the Time part from Date
							//strDate = rdr["Invoice_Date"].ToString().Trim();
							strDate = rdr["vndr_Invoice_Date"].ToString().Trim();
							int pos = strDate.IndexOf(" ");
				
							if(pos != -1)
							{
								strDate = strDate.Substring(0,pos);
							}
							else
							{
								strDate = "";					
							}
							//						promo = rdr["Promo_Scheme"].ToString().Trim();
							//						if (promo.Length > 20)
							//						{
							//							promo = promo.Substring(0,20);
							//						}

							// Calculate Due Date

							DateTime dt = System.Convert.ToDateTime(strDate);
							int crDays  = System.Convert.ToInt32(rdr["Cr_Days"].ToString().Trim());
							strDueDate =  dt.AddDays(crDays).ToShortDateString(); // Zero-padding required

						
							sw.WriteLine(info,
								GenUtil.TrimLength(rdr["Vendor_Name"].ToString().Trim(),13),
								GenUtil.TrimLength(rdr["Place"].ToString(),9),
								GenUtil.TrimLength(rdr["Vendor_Type"].ToString().Trim(),8),
								rdr["vndr_Invoice_No"].ToString().Trim(),
								GenUtil.str2DDMMYYYY(strDate),						            
								GenUtil.TrimLength(rdr["Prod_Type"].ToString().Trim(),9),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),14),
								GenUtil.strNumericFormat((Multiply(rdr["Prod_Type"].ToString().Trim()+"X"+rdr["Prod_Name"].ToString().Trim()+"X"+rdr["Qty"].ToString().Trim())).ToString()),
								//rdr["Qty"].ToString().Trim(),
								GenUtil.strNumericFormat(rdr["Price"].ToString().Trim()),
								rdr["Discount"].ToString().Trim(),
								Multiply1(rdr["Vndr_Invoice_No"].ToString().Trim(),rdr["Invoice_No"].ToString().Trim(),rdr["Net_Amount"].ToString()),							
								//promo,Vndr_Invoice_No
								rdr["Cr_Days"].ToString().Trim(),
								GenUtil.str2DDMMYYYY(strDueDate)
								);
						}
						else if(RadPerclaim.Checked)
						{
							sw.WriteLine(info,
								GenUtil.TrimLength(rdr["Vendor_Name"].ToString().Trim(),13),
								GenUtil.TrimLength(rdr["Vndr_Invoice_No"].ToString(),9),
								GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Vndr_Invoice_Date"].ToString())),
								GenUtil.TrimLength(rdr["Prod_Type"].ToString().Trim(),9),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),14),
								rdr["Qty"].ToString().Trim(),
								rdr["QtyLtr"].ToString(),
								GenUtil.TrimLength(rdr["RSP"].ToString().Trim(),8),
								Convert.ToString(Elgible_Discount(rdr["Prod_ID"].ToString())+Elgible_Discount_Type(rdr["Prod_ID"].ToString())),
								Convert.ToString(Dis_in_Amount(rdr["RSP"].ToString(),Elgible_Discount(rdr["Prod_ID"].ToString()).ToString())),
								Convert.ToString(GenUtil.TrimLength(GetDiscountInPer(rdr["Prod_ID"].ToString(),rdr["RSP"].ToString(),rdr["Invoice_Date"].ToString()),9)),
								Convert.ToString(Claim_Amount(Dis_in_Amount1(rdr["RSP"].ToString(), Elgible_Discount(rdr["Prod_ID"].ToString()).ToString()).ToString(),GetDiscountInPer1(rdr["Prod_ID"].ToString(),rdr["RSP"].ToString(),rdr["Invoice_Date"].ToString()).ToString()))
								);
							GetTotalDiscount(rdr["Trade"].ToString(),rdr["EarlyBird"].ToString(),rdr["FOC"].ToString(),rdr["Discount"].ToString(),rdr["Qty"].ToString(),rdr["QtyLtr"].ToString(),rdr["RSP"].ToString());
						}
						else
						{
							sw.WriteLine(info,
								GenUtil.TrimLength(rdr["Vendor_Name"].ToString().Trim(),13),
								GenUtil.TrimLength(rdr["Vndr_Invoice_No"].ToString(),9),
								GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Vndr_Invoice_Date"].ToString())),
								GenUtil.TrimLength(rdr["Prod_Type"].ToString().Trim(),9),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),14),
								rdr["Qty"].ToString().Trim(),
								rdr["QtyLtr"].ToString(),
								GenUtil.TrimLength(rdr["RSP"].ToString().Trim(),8),
								GetTradeDisc(rdr["RSP"].ToString(),rdr["Trade"].ToString(),rdr["Trade_Discount"].ToString(),rdr["Prod_ID"].ToString(),rdr["qtyltr"].ToString(),rdr["Invoice_Date"].ToString(),rdr["foc"].ToString()),
								rdr["EarlyBird"].ToString(),
								rdr["FOC"].ToString(),
								GetCashDisc(rdr["RSP"].ToString(),rdr["Trade"].ToString(),rdr["EarlyBird"].ToString(),rdr["ETFOC"].ToString(),rdr["FOC"].ToString(),rdr["Discount"].ToString(),rdr["Prod_ID"].ToString(),rdr["Invoice_Date"].ToString(),rdr["Cash_Discount"].ToString(),rdr["Cash_Disc_Type"].ToString()),
								GenUtil.TrimLength(GetDiscountInPer(rdr["Prod_ID"].ToString(),rdr["RSP"].ToString(),rdr["Invoice_Date"].ToString()),7),
								GetTotalDiscount(rdr["Trade"].ToString(),rdr["EarlyBird"].ToString(),rdr["FOC"].ToString(),rdr["Discount"].ToString(),rdr["Qty"].ToString(),rdr["QtyLtr"].ToString(),rdr["RSP"].ToString())
								);
						}
					}
				}
				if(RadioSumrized.Checked)
				{
					sw.WriteLine("+-------------+---------+-----+---------+----------+---------+--------------+----------+--------+----------+------------+----+----------+");
					sw.WriteLine(info,"   Total","","","","","","",GenUtil.strNumericFormat(Cache["os"].ToString()),"","",GenUtil.strNumericFormat(Cache["am"].ToString()),"","");
					sw.WriteLine("+-------------+---------+-----+---------+----------+---------+--------------+----------+--------+----------+------------+----+----------+");
				}
				else if(RadPerclaim.Checked)
				{
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+----------+-----------+-----------+------------+------------+");
					//Coment by Vikas 31.07.09 sw.WriteLine(info,"   Total","","","","","",GenUtil.TrimLength(TotalDisc[0].ToString(),6),"",GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[1].ToString()),8),GenUtil.TrimLength(GenUtil.strNumericFormat(Cache["Disoe"].ToString()),9),GenUtil.TrimLength(GenUtil.strNumericFormat(Convert.ToString(Math.Round(Convert.ToDouble(Cache["DiscInPer"].ToString()),2))),7),GenUtil.TrimLength(GenUtil.strNumericFormat(Cache["total13oe"].ToString()),10));
					sw.WriteLine(info,"   Total","","","","","",GenUtil.TrimLength(TotalDisc[0].ToString(),6),"","",GenUtil.TrimLength(GenUtil.strNumericFormat(Cache["Disoe"].ToString()),9),GenUtil.TrimLength(GenUtil.strNumericFormat(Convert.ToString(Math.Round(Convert.ToDouble(Cache["DiscInPer"].ToString()),2))),7),GenUtil.TrimLength(GenUtil.strNumericFormat(Cache["total13oe"].ToString()),10));
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+----------+-----------+-----------+------------+------------+");
				}
				else
				{
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+--------+--------+-------+--------+---------+-------+----------+");
					sw.WriteLine(info,"   Total","","","","","",GenUtil.TrimLength(TotalDisc[0].ToString(),6),"",GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[1].ToString()),8),GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[2].ToString()),7),GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[3].ToString()),8),GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[4].ToString()),9),GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[5].ToString()),7),GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[6].ToString()),10));
					sw.WriteLine("+-------------+---------+----------+---------+--------------+----+------+--------+--------+-------+--------+---------+-------+----------+");
				}
				dbobj.Dispose();

				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);

				sw.Close();

				//				**Session["From_Date"] = txtDateFrom.Text;
				//				**Session["To_Date"] = Textbox1.Text;
				//				**Response.Redirect("PurchaseBook_PrintPreview.aspx",false); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:makingReport();  EXCEPTION: "+ ex.Message+".   userid  "+uid);
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\PurchaseBook1.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string strDueDate = "",strDate="";
			if(RadioSumrized.Checked)
				sql="select * from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ ToMMddYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ ToMMddYYYY(Textbox1.Text)+"' ";	
					
				/******* add by vikas 05.06.09**********/
			else if(RadPerclaim.Checked)
			{
				//Coment by vikas 27.12.2012 sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc,cast(perdisc as varchar)+' '+perdisctype DiscType,((perdisc*qty*price)/100) Disc_Amount from vw_PurchaseBook3 where prod_id in(select prodid from oilscheme where discounttype='%' and vw_PurchaseBook3.prod_id=oilscheme.prodid ) and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
			}
				/******** end *********/
			else
			{
				if(chkDiscount.Checked)
					//sql="select vendor_name,vndr_invoice_no,Vndr_Invoice_Date,invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then qty*total_qty*tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount from vw_PurchaseBook3,per_discount where prod_id=prodid and fixed_discount_type>'0' and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
					sql="select vendor_name,vndr_invoice_no,Vndr_Invoice_Date,invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,trade_Discount,foc from vw_PurchaseBook3,per_discount where prod_id=prodid and fixed_discount_type>'0' and cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
				else
					sql="select vendor_name,vndr_invoice_no,Vndr_Invoice_Date,invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,trade_discount,foc from vw_PurchaseBook3 where cast(floor(cast(invoice_date as float)) as datetime) >=  '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) +"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+ GenUtil.str2MMDDYYYY(Textbox1.Text)+"'";
			}
			if(DropSearchBy.SelectedIndex!=0)
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Vendor_Name like '"+DropValue.Value+"%'";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					/*if(DropValue.Value!="All")
						sql=sql+" and Vndr_Invoice_No='"+DropValue.Value+"'";*/ // Comment By Vikas Sharma 16.04.09
					if(DropValue.Value!="All")
					{
						if(RadioSumrized.Checked)
							sql="select * from vw_PurchaseBook3 where Vndr_Invoice_No='"+DropValue.Value+"'";		
						else
						{
							if(chkDiscount.Checked)
								sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_Invoice_Date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3,per_discount where fixed_discount_type>'0' and prod_id=prodid and Vndr_Invoice_No='"+DropValue.Value+"'";
							else
								sql="select vendor_name,vndr_invoice_no,invoice_date,vndr_invoice_date,prod_type,Prod_name,qty,qty*total_qty qtyltr,qty*price RSP,prod_id,((qty*price)*2)/100 ET,case when foc=0 then tradeval else '0' end as Trade,case when foc=0 then qty*total_qty*ebird else '0' end as EarlyBird,foc*qty*price FOC,case when discount_Type='Per' then qty*price*Disc/100 else Disc end as Discount,foc*qty*price*2/100 ETFOC,discount_type,cash_Discount,cash_disc_type,Net_Amount,Trade_Discount,foc from vw_PurchaseBook3 where Vndr_Invoice_No='"+DropValue.Value+"'";
						}
					}
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Pack_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value!="All")
					{
						//sql=sql+" and Prod_Name='"+DropValue.Value+"'";
						sql=sql+" and Prod_Name like '%"+DropValue.Value+"%'";
					}
				}
			}
			sql=sql+" order by "+Cache["strorderby"];
			rdr=obj.GetRecordSet(sql);
			
			/********Add by vikas 27.12.2012******************/
			if(RadPerclaim.Checked)
			{
				sw.WriteLine("\tName of the SSA : Maa Dhumavati Oil Company, 38, Anand Nagar, Opp. Transport Nagar, Gwalior(MP)\t");
				sw.WriteLine("\tDiffrence Trade Discount and Promotional Discount Claim from date "+txtDateFrom.Text.ToString()+" to Date"+Textbox1.Text.ToString()+"\t");
				sw.WriteLine("\t\tHO CIRCULAR NO.: HO/LUBES/SALE/2012-13\t\t");
			}
			else
			{
				sw.WriteLine("From Date\t"+txtDateFrom.Text);
				sw.WriteLine("To Date\t"+Textbox1.Text);
			}
			/***********End***************/
			sw.WriteLine();
			if(RadioSumrized.Checked)
				sw.WriteLine("Vendor Name\tPlace\tVendor Type\tInvoice No\tInvoice Date\tProduct Type\tProduct Name\tQuantity in Ltr.\tPrice\tDiscount\tInvoice Amount\tCr. Days\tDue Date");
				/*********Start Add by vikas 05.06.09**********************/
			else if(RadPerclaim.Checked)
			{
				//Coment By vikas 27.12.2012 sw.WriteLine("Vendor Name\tInvoice No\tInvoice Date\tProduct Type\tProduct Name\tQty No.\tQty Ltr\tPrice\tEligible Discount in %\tEligible Discount in Amount\tDiscount from CFA\tClaimable Discount");
				sw.WriteLine("Vendor Name\tInvoice No\tInvoice Date\tProduct Type\tProduct Name\tQty No.\tQty Ltr\tPrice\tEligible Trade Discount\tEligible Trade Discount in Amount\tDeduction in Purchase Invoice\tEligible Prom Disc.\tEligible Prom Disc. Amount\tDeduction in Purchase Invoice\tTotal Disc. Claim");
			}
				/*******************************/
			else
				sw.WriteLine("Vendor Name\tInvoice No\tInvoice Date\tProduct Type\tProduct Name\tQty(No.)\tQty(Ltr)\tPrice\tTrade Disc.\tEarlyBird Disc.\tFOC Disc.\tCash Disc\tDiscount(%)\tDisocunt\tTotal Discount");
			
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					if(RadioSumrized.Checked)
					{
						// Trime the Time part from Date
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

						// Calculate Due Date
						DateTime dt = System.Convert.ToDateTime(strDate);
						int crDays  = System.Convert.ToInt32(rdr["Cr_Days"].ToString().Trim());
						strDueDate =  dt.AddDays(crDays).ToShortDateString(); // Zero-padding required

						sw.WriteLine(rdr["Vendor_Name"].ToString().Trim()+"\t"+
							rdr["Place"].ToString()+"\t"+
							rdr["Vendor_Type"].ToString().Trim()+"\t"+
							rdr["vndr_Invoice_No"].ToString().Trim()+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Vndr_Invoice_Date"].ToString()))+"\t"+
							rdr["Prod_Type"].ToString().Trim()+"\t"+
							rdr["Prod_Name"].ToString().Trim()+"\t"+
							GenUtil.strNumericFormat((Multiply(rdr["Prod_Type"].ToString().Trim()+"X"+rdr["Prod_Name"].ToString().Trim()+"X"+rdr["Qty"].ToString().Trim())).ToString())+"\t"+
							//							rdr["Qty"].ToString().Trim(),
							GenUtil.strNumericFormat(rdr["Price"].ToString().Trim())+"\t"+
							rdr["Discount"].ToString().Trim()+"\t"+
							Multiply1(rdr["Vndr_Invoice_No"].ToString().Trim(),rdr["Invoice_No"].ToString().Trim(),rdr["Net_Amount"].ToString())+"\t"+
							//promo,Vndr_Invoice_No
							rdr["Cr_Days"].ToString().Trim()+"\t"+
							GenUtil.str2DDMMYYYY(strDueDate)
							);
					}
					else if(RadPerclaim.Checked)
					{
						sw.WriteLine(
							GenUtil.TrimLength(rdr["Vendor_Name"].ToString().Trim(),13)+"\t"+
							GenUtil.TrimLength(rdr["Vndr_Invoice_No"].ToString(),9)+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Vndr_Invoice_Date"].ToString()))+"\t"+
							GenUtil.TrimLength(rdr["Prod_Type"].ToString().Trim(),9)+"\t"+
							GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),14)+"\t"+
							rdr["Qty"].ToString().Trim()+"\t"+
							rdr["QtyLtr"].ToString()+"\t"+
							GenUtil.TrimLength(rdr["RSP"].ToString().Trim(),8)+"\t"+
							/************Add by vikas 27.12.2012********************/
							rdr["DiscType"].ToString().Trim()+"\t"+
							rdr["Disc_Amount"].ToString()+"\t"+
							rdr["Disc_Amount"].ToString()+"\t"+
							/************End********************/
							Convert.ToString(Elgible_Discount(rdr["Prod_ID"].ToString())+Elgible_Discount_Type(rdr["Prod_ID"].ToString()))+"\t"+
							Convert.ToString(Dis_in_Amount(rdr["RSP"].ToString(),Elgible_Discount(rdr["Prod_ID"].ToString()).ToString()))+"\t"+
							Convert.ToString(GenUtil.TrimLength(GetDiscountInPer(rdr["Prod_ID"].ToString(),rdr["RSP"].ToString(),rdr["Invoice_Date"].ToString()),9))+"\t"+
							Convert.ToString(Claim_Amount(Dis_in_Amount1(rdr["RSP"].ToString(), Elgible_Discount(rdr["Prod_ID"].ToString()).ToString()).ToString(),GetDiscountInPer1(rdr["Prod_ID"].ToString(),rdr["RSP"].ToString(),rdr["Invoice_Date"].ToString()).ToString()))
							);
						GetTotalDiscount(rdr["Trade"].ToString(),rdr["EarlyBird"].ToString(),rdr["FOC"].ToString(),rdr["Discount"].ToString(),rdr["Qty"].ToString(),rdr["QtyLtr"].ToString(),rdr["RSP"].ToString());
					}

					else
					{
						sw.WriteLine(rdr["Vendor_Name"].ToString()+"\t"+
							rdr["Vndr_Invoice_No"].ToString()+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Vndr_Invoice_Date"].ToString()))+"\t"+
							rdr["Prod_Type"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Qty"].ToString()+"\t"+
							rdr["QtyLtr"].ToString()+"\t"+
							rdr["RSP"].ToString().Trim()+"\t"+
							//rdr["Trade"].ToString()+"\t"+
							GetTradeDisc(rdr["RSP"].ToString(),rdr["Trade"].ToString(),rdr["Trade_Discount"].ToString(),rdr["Prod_ID"].ToString(),rdr["qtyltr"].ToString(),rdr["Invoice_Date"].ToString(),rdr["foc"].ToString())+"\t"+
							rdr["EarlyBird"].ToString()+"\t"+
							rdr["FOC"].ToString()+"\t"+
							GetCashDisc(rdr["RSP"].ToString(),rdr["Trade"].ToString(),rdr["EarlyBird"].ToString(),rdr["ETFOC"].ToString(),rdr["FOC"].ToString(),rdr["Discount"].ToString(),rdr["Prod_ID"].ToString(),rdr["Invoice_Date"].ToString(),rdr["Cash_Discount"].ToString(),rdr["Cash_Disc_Type"].ToString())+"\t"+
							GetDiscountInPer(rdr["Prod_ID"].ToString(),rdr["RSP"].ToString(),rdr["Invoice_Date"].ToString())+"\t"+
							rdr["Discount"].ToString()+"\t"+
							GetTotalDiscount(rdr["Trade"].ToString(),rdr["EarlyBird"].ToString(),rdr["FOC"].ToString(),rdr["Discount"].ToString(),rdr["Qty"].ToString(),rdr["QtyLtr"].ToString(),rdr["RSP"].ToString())
							);
					}
				}
			}
			if(RadioSumrized.Checked)
				sw.WriteLine("Total:\t\t\t\t\t\t\t"+GenUtil.strNumericFormat(Cache["os"].ToString())+"\t\t\t"+GenUtil.strNumericFormat(Cache["am"].ToString()));
			else if(RadPerclaim.Checked)
				//coment by vikas 27.12.2012 sw.WriteLine("   Total\t\t\t\t\t"+TotalDisc[0].ToString()+"\t\t\t"+GenUtil.strNumericFormat(TotalDisc[2].ToString())+"\t"+GenUtil.strNumericFormat(TotalDisc[3].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Disoe"].ToString())+"\t"+Math.Round(Convert.ToDouble(Cache["DiscInPer"].ToString()),2)+"\t"+GenUtil.strNumericFormat(Cache["total13oe"].ToString()));
				sw.WriteLine("   Total\t\t\t\t\t\t"+TotalDisc[0].ToString()+"\t\t\t\t"+GenUtil.strNumericFormat(TotalDisc[2].ToString())+"\t"+GenUtil.strNumericFormat(TotalDisc[3].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Disoe"].ToString())+"\t"+Math.Round(Convert.ToDouble(Cache["DiscInPer"].ToString()),2)+"\t"+GenUtil.strNumericFormat(Cache["total13oe"].ToString()));
				//sw.WriteLine(info,"   Total","","","","","",GenUtil.TrimLength(TotalDisc[0].ToString(),6),"",GenUtil.TrimLength(GenUtil.strNumericFormat(TotalDisc[1].ToString()),8),GenUtil.TrimLength(GenUtil.strNumericFormat(Cache["Disoe"].ToString()),9),GenUtil.TrimLength(GenUtil.strNumericFormat(Convert.ToString(Math.Round(Convert.ToDouble(Cache["DiscInPer"].ToString()),2))),7),GenUtil.TrimLength(GenUtil.strNumericFormat(Cache["total13oe"].ToString()),10));
			else
				sw.WriteLine("   Total\t\t\t\t\t\t"+TotalDisc[0].ToString()+"\t\t"+TotalDisc[1].ToString()+"\t"+GenUtil.strNumericFormat(TotalDisc[2].ToString())+"\t"+GenUtil.strNumericFormat(TotalDisc[3].ToString())+"\t"+GenUtil.strNumericFormat(TotalDisc[4].ToString())+"\t"+GenUtil.strNumericFormat(TotalDisc[5].ToString())+"\t\t"+GenUtil.strNumericFormat(TotalDisc[6].ToString()));
			sw.Close();
		}
		
		/// <summary>
		/// This is used to print the report.
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PurchaseBookReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:purchaseBook.aspx,Method:BtnPrint_Click   Purchase Book Report Printed  userid "+uid);
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:purchaseBook.aspx,Method:BtnPrint_Click   Purchase Book Report Printed  "+ ane.Message+"  EXCEPTION"+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:purchaseBook.aspx,Method:BtnPrint_Click   Purchase Book Report Printed  "+ se.Message+"  EXCEPTION"+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:purchaseBook.aspx,Method:BtnPrint_Click   Purchase Book Report Printed  "+ es.Message+"  EXCEPTION"+uid);
				}
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:purchaseBook.aspx,Method:BtnPrint_Click   Purchase Book Report Printed  "+ es.Message+"  EXCEPTION"+uid);
			}
		}

		protected void GridReport_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Prepares the excel report file PurchaseBook.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridReport.Visible==true || grdDetails.Visible==true || GridClaim.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   PurchaseBook Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    PurchaseBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
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
				string strName="",strInvoiceNo="",strProductGroup="",strProdWithPack="",strSSR="",strPackType="";//,strType=""

				//strType = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				//strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				strName="select distinct Vendor_Name from vw_PurchaseBook3 order by Vendor_Name";
				// Comment By Vikas Sharma 16.04.09 strInvoiceNo="select distinct Invoice_No from vw_salebook order by invoice_no";
				strInvoiceNo="select distinct Vndr_Invoice_No  InvoiceNo from Purchase_Master order by Vndr_Invoice_No";  // Add By Vikas Sharma
				strProductGroup="select distinct category from vw_salebook order by category";
				strPackType="select distinct Pack_Type from vw_salebook order by Pack_Type";
				strProdWithPack="select distinct Prod_Name+':'+pack_type from vw_salebook order by prod_name+':'+pack_type";
				//strSSR="select distinct Under_Salesman from vw_salebook order by under_salesman";
				strSSR="select Emp_Name from Employee where designation='Servo Sales representative' order by Emp_name";

				string[] arrStr = {strName,strInvoiceNo,strPackType,strProductGroup,strProdWithPack,strSSR};
				HtmlInputHidden[] arrCust = {tempVendorName,tempInvoiceNo,tempPackType,tempProductGroup,tempProdWithPack,tempSSR};
				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Purchase Book Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}	

		/// <summary>
		/// This method return the calculated cash discount
		/// </summary>
		double CashDiscount =0;
		public string GetCashDisc(string RSP,string Trade,string EarlyBird,string ETFOC,string FOC,string Disc,string Prod_ID,string Invoice_Date,string cashdisc,string cashdisctype)
		{
			CashDiscount=0;
			if(FOC=="0")
			{
				if(cashdisctype=="Per")
				{
					string DiscPer = GetDiscountInPertemp(Prod_ID,RSP,Invoice_Date);
					double Tot=double.Parse(RSP)+((double.Parse(RSP)-double.Parse(DiscPer))*2)/100-(double.Parse(Trade)+double.Parse(FOC)+double.Parse(Disc)+double.Parse(DiscPer)+double.Parse(EarlyBird)+double.Parse(ETFOC));
					Tot=Tot*double.Parse(cashdisc)/100;
					CashDiscount=Tot;
					return GenUtil.strNumericFormat(Tot.ToString());
				}
				else
				{
					CashDiscount=double.Parse(cashdisc);
					return cashdisc;
				}
			}
			else
				return "0";
		}
		
		double DiscInPer = 0,TradeDisc=0,DiscInPer1=0;
		/// <summary>
		/// This method is used to fatch the discount and disocunt type from per_discount table according to 
		/// product Id and calculate with given RSP
		/// </summary>
		public string GetDiscountInPer(string Prod_ID,string RSP,string Invoice_Date)
		{
			//coment by vikas 31.07.09 double Tot=0; 
			double[] Tot=new double[10];
			int i=0;
			double Tot1=0;
			DiscInPer=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=obj.GetRecordSet("select Discount,DiscountType from per_discount where prodid = '"+ Prod_ID +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.trimDate(Invoice_Date)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.trimDate(Invoice_Date)+"'");
			
			/*Coment by vikas 31.07.09
			if(rdr.Read())
			{
				if(rdr["DiscountType"].ToString()=="%")
					Tot=(double.Parse(RSP)*double.Parse(rdr["Discount"].ToString()))/100;
				else
					Tot=double.Parse(rdr["Discount"].ToString());
			}
			rdr.Close();
			DiscInPer=Tot;
			DiscInPer1+=Tot;*/

			while(rdr.Read())
			{
				if(rdr["DiscountType"].ToString()=="%")
					Tot[i]=(double.Parse(RSP)*double.Parse(rdr["Discount"].ToString()))/100;
				else
					Tot[i]=double.Parse(rdr["Discount"].ToString());
				i++;
			}
			rdr.Close();

			for(int j=0;j<i;j++)
			{
				DiscInPer=Tot[j];
				Tot1+=Tot[j];
				DiscInPer1+=Tot[j];
			}

			Cache["DiscInPer"]=DiscInPer1;
			//Coment by vikas 31.07.09 return GenUtil.strNumericFormat(Tot.ToString()); 
			return GenUtil.strNumericFormat(Tot1.ToString());
		}

		double DiscInPer2 = 0,TradeDisc1=0;
		/// <summary>
		/// This method is used to fatch the discount and disocunt type from per_discount table according to 
		/// product Id and calculate with given RSP
		/// </summary>
		public string GetDiscountInPer1(string Prod_ID,string RSP,string Invoice_Date)
		{
			double[] Tot=new double[10];
			int i=0;
			double Tot1=0;
			//Coment by vikas 31.07.09 double Tot=0;
			DiscInPer2=0;
			InventoryClass obj = new InventoryClass();
			
			SqlDataReader rdr=obj.GetRecordSet("select Discount,DiscountType from per_discount where prodid = '"+ Prod_ID +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.trimDate(Invoice_Date)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.trimDate(Invoice_Date)+"'");
			
			/*Comment by vikas 31.07.09 if(rdr.Read())
			{
				if(rdr["DiscountType"].ToString()=="%")
					Tot=(double.Parse(RSP)*double.Parse(rdr["Discount"].ToString()))/100;
				else
					Tot=double.Parse(rdr["Discount"].ToString());
			}
			rdr.Close();
			DiscInPer2=Tot;
			//DiscInPer1+=Tot;
			//Cache["DiscInPer"]=DiscInPer1;*/

			while(rdr.Read())
			{
				if(rdr["DiscountType"].ToString()=="%")
					Tot[i]=(double.Parse(RSP)*double.Parse(rdr["Discount"].ToString()))/100;
				else
					Tot[i]=double.Parse(rdr["Discount"].ToString());
				i++;
			}
			rdr.Close();

			for(int j=0;j<i;j++)
			{
				DiscInPer2+=Tot[j];
				Tot1+=Tot[j];
			}


			//Coment by vikas 31.07.09 return GenUtil.strNumericFormat(Tot.ToString());
			return GenUtil.strNumericFormat(Tot1.ToString());
		}

		/// <summary>
		/// This method is used to fatch the stockist discount and disocunt type from StktSchDiscount table according to 
		/// product Id and calculate with given RSP
		/// </summary>
		public string GetTradeDisc(string RSP,string Tradeval,string Trade_Disc,string Prod_ID,string QtyLtr,string Invoice_Date,string foc)
		{
			double Tot=0;
			TradeDisc=0;
			if(double.Parse(foc)==0)
			{
				if(double.Parse(Tradeval)==0)
				{
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr=obj.GetRecordSet("select Discount,DiscountType from stktschdiscount where prodid = '"+ Prod_ID +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.trimDate(Invoice_Date)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.trimDate(Invoice_Date)+"'");
					if(rdr.Read())
					{
						if(rdr["DiscountType"].ToString()=="%")
							Tot=(double.Parse(RSP)*double.Parse(rdr["Discount"].ToString()))/100;
						else
							//Tot=double.Parse(rdr["Discount"].ToString());
							Tot=double.Parse(rdr["Discount"].ToString())*double.Parse(QtyLtr);
					}
					rdr.Close();
				}
				else
				{
					Tot=double.Parse(Tradeval)*double.Parse(QtyLtr);
				}
			}
			TradeDisc=Tot;
			return GenUtil.strNumericFormat(Tot.ToString());
		}

		/// <summary>
		/// This method is temporary used to fatch the discount and disocunt type from per_discount table according to 
		/// product Id and calculate with given RSP 
		/// </summary>
		public string GetDiscountInPertemp(string Prod_ID,string RSP,string Invoice_Date)
		{
			double Tot=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=obj.GetRecordSet("select Discount,DiscountType from per_discount where prodid = '"+ Prod_ID +"' and cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.trimDate(Invoice_Date)+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.trimDate(Invoice_Date)+"'");
			if(rdr.Read())
			{
				if(rdr["DiscountType"].ToString()=="%")
					Tot=(double.Parse(RSP)*double.Parse(rdr["Discount"].ToString()))/100;
				else
					Tot=double.Parse(rdr["Discount"].ToString());
			}
			rdr.Close();
			return GenUtil.strNumericFormat(Tot.ToString());
		}

		/// <summary>
		/// This method is used to calculate the total discount value.
		/// </summary>
		
		public string GetTotalDiscount(string Trade,string EarlyBird,string FOC,string Discount,string Qty,string QtyLtr,string RSP)
		{
			int i=0;
			//**TotalDiscount=double.Parse(Trade)+double.Parse(EarlyBird)+double.Parse(FOC)+double.Parse(Discount)+CashDiscount+DiscInPer;
			TotalDiscount=TradeDisc+double.Parse(EarlyBird)+double.Parse(FOC)+double.Parse(Discount)+CashDiscount+DiscInPer;
			//TotalDisc[i++]+=double.Parse(Qty);
			TotalDisc[i++]+=double.Parse(QtyLtr);
			//TotalDisc[i++]+=double.Parse(RSP);
			//**TotalDisc[i++]+=double.Parse(Trade);
			TotalDisc[i++]+=TradeDisc;
			TotalDisc[i++]+=double.Parse(EarlyBird);
			TotalDisc[i++]+=double.Parse(FOC);
			TotalDisc[i++]+=CashDiscount;
			TotalDisc[i++]+=DiscInPer;
			//TotalDisc[i++]+=double.Parse(Discount);
			TotalDisc[i++]+=TotalDiscount;
			

			return GenUtil.strNumericFormat(TotalDiscount.ToString());
		}
		
		double TotalDiscount=0;
		public void ItemDataBound1(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				//if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem))
				if(e.Item.ItemType == ListItemType.Item)
				{
					tot_qty_ltr+=Convert.ToDouble(e.Item.Cells[6].Text);
				}
				if(e.Item.ItemType == ListItemType.Footer)
				{
					//e.Item.Cells[5].Text=GenUtil.strNumericFormat(TotalDisc[0].ToString());
					e.Item.Cells[6].Text=tot_qty_ltr.ToString();
					//e.Item.Cells[7].Text=GenUtil.strNumericFormat(TotalDisc[2].ToString());
					//Coment by vikas 31.07.09 e.Item.Cells[8].Text=GenUtil.strNumericFormat(TotalDisc[1].ToString());
					e.Item.Cells[9].Text=GenUtil.strNumericFormat(TotalDisc[2].ToString());
					e.Item.Cells[10].Text=GenUtil.strNumericFormat(TotalDisc[3].ToString());
					e.Item.Cells[11].Text=GenUtil.strNumericFormat(TotalDisc[4].ToString());
					e.Item.Cells[12].Text=GenUtil.strNumericFormat(TotalDisc[5].ToString());
					//e.Item.Cells[13].Text=GenUtil.strNumericFormat(TotalDisc[6].ToString());
					e.Item.Cells[14].Text=GenUtil.strNumericFormat(TotalDisc[6].ToString());
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		double tot_qty_ltr=0;
		public void ItemDataBound(object sender,DataGridItemEventArgs e)
		{
			try
			{
				
				// If datagrid item is a bound column other than header and footer
				//if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem))
				
				if(e.Item.ItemType == ListItemType.Footer)
				{
					//e.Item.Cells[5].Text=GenUtil.strNumericFormat(TotalDisc[0].ToString());
					//Coment by vikas e.Item.Cells[6].Text=TotalDisc[0].ToString();
					e.Item.Cells[6].Text=TotalDisc[0].ToString();
					//e.Item.Cells[7].Text=GenUtil.strNumericFormat(TotalDisc[2].ToString());
					e.Item.Cells[8].Text=GenUtil.strNumericFormat(TotalDisc[1].ToString());
					e.Item.Cells[9].Text=GenUtil.strNumericFormat(TotalDisc[2].ToString());
					e.Item.Cells[10].Text=GenUtil.strNumericFormat(TotalDisc[3].ToString());
					e.Item.Cells[11].Text=GenUtil.strNumericFormat(TotalDisc[4].ToString());
					e.Item.Cells[12].Text=GenUtil.strNumericFormat(TotalDisc[5].ToString());
					//e.Item.Cells[13].Text=GenUtil.strNumericFormat(TotalDisc[6].ToString());
					e.Item.Cells[14].Text=GenUtil.strNumericFormat(TotalDisc[6].ToString());
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		//		public string GetDiscount(string RSP,string Disc,string DiscType)
		//		{
		//			double Tot = 0;
		//			if(DiscType=="Per")
		//				Tot=double.Parse(RSP)*double.Parse(Disc)/100;
		//			else
		//				Tot=double.Parse(Disc);
		//			return GenUtil.strNumericFormat(Tot.ToString());
		//		}
	}
}