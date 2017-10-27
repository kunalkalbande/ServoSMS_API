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
	/// Summary description for PartyWiseSalesFigure.
	/// </summary>
	public partial class PartyWiseSalesFigure : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
		public int Count=0;
		public int i=1;
		string uid;
		protected System.Web.UI.HtmlControls.HtmlInputText Text1;
		protected System.Web.UI.WebControls.DropDownList DropOther;


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
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="25";
				string[,] Priv=(string[,]) Session["Privileges"];
				
				for(i=0;i<Priv.GetLength(0);i++)
				{
					if(Priv[i,0] == Module && Priv[i,1] == SubModule)
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
				datagrid11.Visible=false;
				panTotalSales.Visible=false;
				txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				txtDateTo.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;			
				GetMultiValue();
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
		/// This method is used to view the report with the help of Bindthedata() function and set the column name 
		/// with ascending order in session variable. 
		/// </summary>
		protected void btnview_Click(object sender, System.EventArgs e)
		{
			try
			{
				strOrderBy = "m1 ASC";
				Session["Column"] = "m1";
				Session["Order"] = "ASC";
				BindTheData();
				panTotalSales.Visible=true;
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method : btnview_Click, userid "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method : btnview_Click,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
		}

		/// <summary>
		/// This method is used to bind the datagrid and display the information by given order and display the data grid.
		/// </summary>
		public void BindTheData()
		{
			try
			{
				SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				string sqlstr="";
				string cust_name="";
				string cust_name1="";
				
				if(DropSelectOption.SelectedItem.Text.Equals("Place"))
				{
					if(DropValue.Value!="All")
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)  group by cust_type,cust_name,city,city,ssr";

						/*********This Condition Add by vikas 03.08.09******************************/
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)  group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue1.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)  group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue1.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103)  and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_name='"+cust_name1.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)  group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and state ='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
					}
					else
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/
				}
				else if(DropSelectOption.SelectedItem.Text.Equals("Group"))
				{
					if(DropValue.Value!="All")
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						//coment by vikas 17.11.2012 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						/********* This Condition Add by vikas 03.08.09 ******************************/
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and city='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103)  and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}/****Add by vikas 17.11.2012********************/
						else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}/****End*****************/
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and state ='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						/***********End****************************/
					}
					else
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else if(DropSelectOption.SelectedItem.Text.Equals("SubGroup"))
				{
					if(DropValue.Value!="All")
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						/********* This Condition Add by vikas 03.08.09 ******************************/
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and city='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Category"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and state ='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						/***********End****************************/
					}
					else
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else if(DropSelectOption.SelectedItem.Text.Equals("Name"))
				{
					if(DropValue.Value!="All")
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_name='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						//coment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						/********* This Condition Add by vikas 03.08.09 ******************************/
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and city='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and state ='"+DropValue1.Value+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+ "' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						/***********End****************************/
					}
					else
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else if(DropSelectOption.SelectedItem.Text.Equals("SSR"))
				{
					if(DropValue.Value!="All")
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+ "') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


						/********* This Condition Add by vikas 03.08.09 ******************************/
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
					
						/***********End****************************/
					}
					else
					{
						//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
					/************Add by vikas 01.08.09***************************/
				else if(DropSelectOption.SelectedItem.Text.Equals("District"))
				{
					if(DropValue.Value!="All")
					{
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";

						/********* This Condition Add by vikas 03.08.09 ******************************/
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						/***********End****************************/
					}
					else
					{
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else if(DropSelectOption.SelectedItem.Text.Equals("Customer Type"))
				{
					if(DropValue.Value!="All")
					{
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
						{
							if(DropValue1.Value!="All")
							{
								cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							}
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
						{
							if(DropValue1.Value!="All")
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and (c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' or c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
							else
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
					}
					else
					{
						sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}	
				else
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sqlstr= "select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				
				DataSet ds= new DataSet();
				
				SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
				da.Fill(ds, "vw_SalesOil");
				DataTable dtCustomers = ds.Tables["vw_SalesOil"];
				DataView dv=new DataView(dtCustomers);
				dv.Sort = strOrderBy;
				Cache["strOrderBy"]=strOrderBy;
				if(dv.Count==0)
				{
					MessageBox.Show("Data not available");
					datagrid11.Visible=false;
				}
				else
				{
					datagrid11.DataSource=dv;
					datagrid11.DataBind();
					datagrid11.Visible=true;
					Count=1;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method : BindTheDate(),  EXCEPTION  "+ex.Message+" userid "+uid);
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
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
		}

		/// <summary>
		/// This method is used to calculate the total amount and store in Cache variable.
		/// </summary>
		double TotQty=0;
		public string SumTotQty(string amt)
		{
			TotQty+=System.Convert.ToDouble(amt);
			Cache["TotQty"]=TotQty;
			return GenUtil.strNumericFormat(amt);
		}

		/// <summary>
		/// This method is used to calculate the total 2t qty and store in Cache variable.
		/// </summary>
		double Oil2t=0;
		public string SumOil2t(string amt)
		{
			Oil2t+=System.Convert.ToDouble(amt);
			Cache["Oil2t"]=Oil2t;
			return GenUtil.strNumericFormat(amt);
		}

		double LubeSale=0;
		public string SumLubeSale(string amt)
		{
			LubeSale+=System.Convert.ToDouble(amt);
			Cache["LubeSale"]=LubeSale;
			return GenUtil.strNumericFormat(amt);
		}

		/// <summary>
		/// This method is used to calculate the total 4t qty and store in Cache variable.
		/// </summary>
		double Oil4t=0;
		public string SumOil4t(string amt)
		{
			Oil4t+=System.Convert.ToDouble(amt);
			Cache["Oil4t"]=Oil4t;
			return GenUtil.strNumericFormat(amt);
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the PartyWiseSalesFigure.txt file name to print.
		/// </summary>
		protected void btnprint_Click(object sender, System.EventArgs e)
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
					CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PartyWiseSalesFigure.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PartyWiseSalesFigure Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PartyWiseSalesFigure Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PartyWiseSalesFigure Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PartyWiseSalesFigure Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
			i=1;
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			InventoryClass obj1=new InventoryClass();
			//SqlDataReader SqlDtr;
			string sql="";
			int x=0;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\PartyWiseSalesFigure.xls";
			StreamWriter sw = new StreamWriter(path);
			panTotalSales.Visible=true;
			System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
			
			string cust_name="";
			string cust_name1="";

			if(DropSelectOption.SelectedItem.Text.Equals("Place"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					/*********This Condition Add by vikas 03.08.09******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
				/***********End****************************/
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("Group"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					
					/***********End****************************/

				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("SubGroup"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Category"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/

				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("Name"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_name='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					//coment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";



					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/

				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("SSR"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/
				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}

				/************Add by vikas 01.08.09***************************/
			else if(DropSelectOption.SelectedItem.Text.Equals("District"))
			{
				if(DropValue.Value!="All")
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";



					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/


				}
				else
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("Customer Type"))
			{
				if(DropValue.Value!="All")
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and (c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' or c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}	/************End****01.08.09 ***********************/
			else
				//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
				sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";

			sql=sql+" order by "+Cache["strOrderBy"];
			rdr=obj.GetRecordSet(sql);
			//sw.WriteLine("|  CaTegory  |       Party Name        |     Place     |   TotLub    |  2T Sales   |  4T Sales   |");
			sw.WriteLine("SN\tCategory_Grp\tCategory_Sub_Grp\tParty Name\tPlace\tTotal Sale\tLube Sale\t2T Sales\t4T Sales");
			while(rdr.Read())
			{
				sw.WriteLine(i.ToString()+"\t"+
					rdr["m1"].ToString()+"\t"+
					GetSSRName(rdr["m7"].ToString())+"\t"+
					rdr["m2"].ToString()+"\t"+
					rdr["m3"].ToString()+"\t"+
					GenUtil.strNumericFormat(rdr["m4"].ToString())+"\t"+
					GenUtil.strNumericFormat(rdr["m8"].ToString())+"\t"+
					GenUtil.strNumericFormat(rdr["m5"].ToString())+"\t"+
					GenUtil.strNumericFormat(rdr["m6"].ToString())
					);
				i++;
			}
			sw.WriteLine("\tTotal\t\t\t\t"+GenUtil.strNumericFormat(Cache["TotQty"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["LubeSale"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil2t"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
			rdr.Close();
			//************************
			Count=1;
			
			string str,str8,str9;
			
			if(DropSelectOption.SelectedIndex==4)
			{
				if(DropValue.Value!="All")
					str= "select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_id from employee where emp_name='"+DropValue.Value+"') group by c.cust_type order by c.cust_type";
				else
					str= "select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by c.cust_type order by c.cust_type";
			}
			else
				str= "select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by c.cust_type order by c.cust_type";
			if(DropSelectOption.SelectedIndex==4)
			{
				if(DropValue.Value!="All")
					str8= "select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and ssr=(select Emp_id from employee where emp_name='" + DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
				else
					str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			}
			else
				str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			
			str9="select sum(qty*total_qty) from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and p.prod_id=pd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			
			dbobj.SelectQuery(str,ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					int nos=0;
					if(DropSelectOption.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
							rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
						else
							rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
					}
					else
						rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
					if(rdr1.Read())
					{
						nos=int.Parse(rdr1.GetValue(0).ToString());
					}
					rdr1.Close();
					//12.4.2013 sw.WriteLine(""+"\t\t"+"Total "+rdr.GetValue(0).ToString()+" Sales"+"\t"+nos.ToString()+"\t"+GenUtil.strNumericFormat(rdr.GetValue(1).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(2).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(3).ToString()));
					sw.WriteLine(""+"\t\t\t"+"Total "+rdr.GetValue(0).ToString()+" Sales"+"\t"+nos.ToString()+"\t"+GenUtil.strNumericFormat(rdr.GetValue(1).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(4).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(2).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(3).ToString()));
				}
			}
			dbobj.Dispose();
			dbobj.SelectQuery(str8,ref rdr);
			int TotalNos=0;
			if(rdr.Read())
			{
				if(DropSelectOption.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
					else
						rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
				}
				else
					rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
				if(rdr1.Read())
				{
					TotalNos=int.Parse(rdr1.GetValue(0).ToString());
				}
				rdr1.Close();
				//12.4.2013 sw.WriteLine(""+"\t\t"+"Total Secondary Lube Sales"+"\t"+TotalNos.ToString()+"\t"+GenUtil.strNumericFormat(rdr.GetValue(0).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(1).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(2).ToString()));
				sw.WriteLine(""+"\t\t\t"+"Total Secondary Lube Sales"+"\t"+TotalNos.ToString()+"\t"+GenUtil.strNumericFormat(rdr.GetValue(0).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(3).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(1).ToString())+"\t"+GenUtil.strNumericFormat(rdr.GetValue(2).ToString()));
			}
			else
				//12.4.2013 sw.WriteLine(""+"\t\t"+"Total Secondary Lube Sales"+"\t"+TotalNos.ToString()+"\t"+"0.00"+"\t"+"0.00"+"\t"+"0.00");
				sw.WriteLine(""+"\t\t\t"+"Total Secondary Lube Sales"+"\t"+TotalNos.ToString()+"\t"+"0.00"+"\t"+"0.00"+"\t"+"0.00"+"\t"+"0.00");
			dbobj.Dispose();
			double primary=0.00;
			dbobj.SelectQuery(str9,ref rdr);
			if(rdr.Read())
			{
				//primary+=double.Parse(GenUtil.changeqtyltr(rdr.GetValue(1).ToString(),int.Parse(rdr.GetValue(0).ToString())));
				if(rdr.GetValue(0).ToString()!="")
					primary+=double.Parse(rdr.GetValue(0).ToString());
			}
			//12.4.2013 sw.WriteLine(""+"\t\t"+"Total Primary Sales"+"\t"+""+"\t"+""+"\t"+""+"\t"+GenUtil.strNumericFormat(primary.ToString()));
			sw.WriteLine(""+"\t\t\t"+"Total Primary Sales"+"\t"+""+"\t"+""+"\t"+""+"\t"+""+"\t"+GenUtil.strNumericFormat(primary.ToString()));
			//else
			//	sw.WriteLine(""+"\t"+"Total Primary Sales"+"\t"+""+"\t"+""+"\t"+""+"\t"+"");
			dbobj.Dispose();
			//************************
			sw.Close();
			// truncate table after use.
			dbobj.Insert_or_Update("truncate table stkmv", ref x);
		}

		/// <summary>
		/// This method is used to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PartyWiseSalesFigure.txt";
			StreamWriter sw = new StreamWriter(path);
			panTotalSales.Visible=true;
			string sql="";
			string info = "";
			
			string cust_name="";
			string cust_name1="";

			if(DropSelectOption.SelectedItem.Text.Equals("Place"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";

					/*********This Condition Add by vikas 03.08.09******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_type like'"+DropValue1.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_type like'"+DropValue1.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and city='"+DropValue.Value+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
				/***********End****************************/
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("Group"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/

				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("SubGroup"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like'"+DropValue.Value+"%' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Category"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name1=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/

				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("Name"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_name='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					//coment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";



					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/

				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("SSR"))
			{
				if(DropValue.Value!="All")
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";


					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/
				}
				else
				{
					//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}

				/************Add by vikas 01.08.09***************************/
			else if(DropSelectOption.SelectedItem.Text.Equals("District"))
			{
				if(DropValue.Value!="All")
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";



					/********* This Condition Add by vikas 03.08.09 ******************************/
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cust_name='"+cust_name1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and state ='"+DropValue.Value+"' and c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					/***********End****************************/


				}
				else
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}
			else if(DropSelectOption.SelectedItem.Text.Equals("Customer Type"))
			{
				if(DropValue.Value!="All")
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					if(DropSelectOption1.SelectedItem.Text.Equals("Place"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and city='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Group"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SubGroup"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Name"))
					{
						if(DropValue1.Value!="All")
						{
							cust_name=DropValue1.Value.Substring(0,DropValue1.Value.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						}
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("SSR"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and SSR=(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("District"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' and state ='"+DropValue1.Value+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
					else if(DropSelectOption1.SelectedItem.Text.Equals("Customer Type"))
					{
						if(DropValue1.Value!="All")
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and (c.cust_Type ='"+DropValue.Value.ToString().Trim()+"' or c.cust_Type ='"+DropValue1.Value.ToString().Trim()+"') and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
						else
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
					}
				}
				else
				{
					sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";
				}
			}

			else
				//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
				sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,ssr m7,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city,ssr";




			sql=sql+" order by "+Cache["strOrderBy"];
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
			  string des="------------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Party Wise Sales Figure Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
			//sw.WriteLine("Search By : "+DropSelectOption.SelectedItem.Text);
			//sw.WriteLine("Option    : "+DropValue.Value);
			
			/*Coment by vikas 23.10.09
			sw.WriteLine("+------------+----------------+-------------------------+---------------+-------------+-------------+-------------+");
			sw.WriteLine("|Category_Grp|Category_Sub_Grp|       Party Name        |     Place     |   TotLub    |  2T Sales   |  4T Sales   |");
			sw.WriteLine("+------------+----------------+-------------------------+---------------+-------------+-------------+-------------+");
			*/
			sw.WriteLine("+---+------------+----------------+-------------------------+---------------+-------------+-------------+-------------+-------------+");
			sw.WriteLine("|SN |Category_Grp|Category_Sub_Grp|       Party Name        |     Place     |   TotLub    |  Lube Sale  |  2T Sales   |  4T Sales   |");
			sw.WriteLine("+---+------------+----------------+-------------------------+---------------+-------------+-------------+-------------+-------------+");

			//             123 123456789012 1234567890123456 1234567890123456789012345 123456789012345 1234567890123 1234567890123 1234567890123 1234567890123
			if(rdr.HasRows)
			{
				//23.10.09 coment by vikas foe add serial number info = " {0,-12:S} {1,-16:F} {2,-25:F} {3,-15:S} {4,13:S} {5,13:S} {6,13:S}"; 
				info = " {0,-3:S} {1,-12:S} {2,-16:F} {3,-25:F} {4,-15:S} {5,13:S} {6,13:S} {7,13:S} {8,13:S} "; 
				while(rdr.Read())
				{
					sw.WriteLine(info,i.ToString(),GenUtil.TrimLength(rdr["m1"].ToString(),12),
						GenUtil.TrimLength(GetSSRName(rdr["m7"].ToString()),16),
						GenUtil.TrimLength(rdr["m2"].ToString(),25),
						GenUtil.TrimLength(rdr["m3"].ToString(),15),
						GenUtil.strNumericFormat(rdr["m4"].ToString()),
						GenUtil.strNumericFormat(rdr["m8"].ToString()),
						GenUtil.strNumericFormat(rdr["m5"].ToString()),
						GenUtil.strNumericFormat(rdr["m6"].ToString())
						);
					i++;
				}
			}
			sw.WriteLine("+---+------------+----------------+-------------------------+---------------+-------------+-------------+-------------+-------------+");
			sw.WriteLine(info,"","   Toatl","","","",GenUtil.strNumericFormat(Cache["TotQty"].ToString()),GenUtil.strNumericFormat(Cache["LubeSale"].ToString()),GenUtil.strNumericFormat(Cache["Oil2t"].ToString()),GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
			sw.WriteLine("+---+------------+----------------+-------------------------+---------------+-------------+-------------+-------------+-------------+");
			dbobj.Dispose();
			//************************
			Count=1;
			//12.4.2013 string info1 = " {0,-60:S} {1,10:S} {2,13:F} {3,13:S} {4,13:S}"; 
			string info1 = " {0,-59:S} {1,15:S} {2,13:F} {3,13:S} {4,13:S} {5,13:S}"; 
							//"{0,-45:S} {1,-15:S} {2,13:S} {3,13:S} {4,13:S} {5,13:S} ";
			string str,str8,str9;							//,str1,str2,str3,str4,str5,str6,str7
			/*
			str="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'bazzar%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str1="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'oe%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str2="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'fleet%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str3="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'ro-1%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str4="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'ro-2%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str5="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'ro-3%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str6="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'ro-4%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str7="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cust_type like 'ibp%'and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			*/
			if(DropSelectOption.SelectedIndex==4)
			{
				if(DropValue.Value!="All")
					str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_id from employee where emp_name='"+DropValue.Value+"') group by c.cust_type order by c.cust_type";
				else
					str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by c.cust_type order by c.cust_type";
			}
			else
				str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by c.cust_type order by c.cust_type";
			if(DropSelectOption.SelectedIndex==4)
			{
				if(DropValue.Value!="All")
					str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and ssr=(select Emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
				else
					str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			}
			else
				str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			//str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			str9="select sum(qty*total_qty) from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and p.prod_id=pd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)";
			
			InventoryClass obj1 = new InventoryClass();
			dbobj.SelectQuery(str,ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					int nos=0;
					//string ss="select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
					if(DropSelectOption.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
							rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
						else
							rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
					}
					else
						rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+rdr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
					if(rdr1.Read())
					{
						nos=int.Parse(rdr1.GetValue(0).ToString());
					}
					rdr1.Close();
					//sw.WriteLine(info1,"Total "+rdr.GetValue(0).ToString()+" Sales",nos.ToString(),GenUtil.strNumericFormat(rdr.GetValue(1).ToString()),GenUtil.strNumericFormat(rdr.GetValue(2).ToString()),GenUtil.strNumericFormat(rdr.GetValue(3).ToString()));
					sw.WriteLine(info1,"Total "+rdr.GetValue(0).ToString()+" Sales",nos.ToString(),GenUtil.strNumericFormat(rdr.GetValue(1).ToString()),GenUtil.strNumericFormat(rdr.GetValue(4).ToString()),GenUtil.strNumericFormat(rdr.GetValue(2).ToString()),GenUtil.strNumericFormat(rdr.GetValue(3).ToString()));
				}
			}
			dbobj.Dispose();
			//sw.WriteLine(des);
			sw.WriteLine("+-----------------------------------------------------------+---------------+-------------+-------------+-------------+-------------+");
			dbobj.SelectQuery(str8,ref rdr);
			int TotalNos=0;
			if(rdr.Read())
			{
				if(DropSelectOption.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
					else
						rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
				}
				else
					rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
				//rdr1 = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))");
				if(rdr1.Read())
				{
					TotalNos=int.Parse(rdr1.GetValue(0).ToString());
				}
				rdr1.Close();
				//12.4.2013 sw.WriteLine(info1,"Total Secondary Lube Sales",TotalNos.ToString(),GenUtil.strNumericFormat(rdr.GetValue(0).ToString()),GenUtil.strNumericFormat(rdr.GetValue(1).ToString()),GenUtil.strNumericFormat(rdr.GetValue(2).ToString()));
				sw.WriteLine(info1,"Total Secondary Lube Sales",TotalNos.ToString(),GenUtil.strNumericFormat(rdr.GetValue(0).ToString()),GenUtil.strNumericFormat(rdr.GetValue(3).ToString()),GenUtil.strNumericFormat(rdr.GetValue(1).ToString()),GenUtil.strNumericFormat(rdr.GetValue(2).ToString()));
			}
			else
				//12.4.2013 sw.WriteLine(info1,"Total Secondary Lube Sales",TotalNos.ToString(),"0.00","0.00","0.00");
				sw.WriteLine(info1,"Total Secondary Lube Sales",TotalNos.ToString(),"0.00","0.00","0.00","0.00");
			dbobj.Dispose();
			double primary=0.00;
			dbobj.SelectQuery(str9,ref rdr);
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					primary+=double.Parse(rdr.GetValue(0).ToString());
			}
			//12.4.2013 sw.WriteLine(info1,"Total Primary Sales","","","",GenUtil.strNumericFormat(primary.ToString()));
			sw.WriteLine(info1,"Total Primary Sales","","","","",GenUtil.strNumericFormat(primary.ToString()));
			
			dbobj.Dispose();
			//************************
			sw.Close();
		}

		protected void DropSelectOption_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//			InventoryClass obj=new InventoryClass();
			//			SqlDataReader SqlDtr;
			//			string str="";
			//			DropOption.Items.Clear();
			//			DropOption.Items.Add("All");
			//			if(DropSelectOption.SelectedItem.Text.Equals("Category"))
			//			{
			//				str="select distinct Cust_Type from customer order by cust_type";
			//				SqlDtr=obj.GetRecordSet(str);
			//				while(SqlDtr.Read())
			//				{
			//					DropOption.Items.Add(SqlDtr.GetValue(0).ToString());
			//				}
			//				SqlDtr.Close();
			//			}
			//			else if(DropSelectOption.SelectedItem.Text.Equals("Place"))
			//			{
			//				str="select distinct city from customer order by city";
			//				SqlDtr=obj.GetRecordSet(str);
			//				while(SqlDtr.Read())
			//				{
			//					DropOption.Items.Add(SqlDtr.GetValue(0).ToString());
			//				}
			//				SqlDtr.Close();
			//			}
			
		}

		/// <summary>
		/// Prepares the excel report file PartyWiseSalesFigure.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(datagrid11.Visible==true)
				{
					ConvertToExcel();
					Count=1;
					i=1;
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" PartyWiseSalesFigure Report Convert Into Excel Format ,  userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" PartyWiseSalesFigure Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
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
				string strName="",strCategory="",strPlace="",SSR="",District="";
				string strGroup="",strSubGroup="",strCustType="";       //Add by vikas 16.11.2012
				//strName = "select distinct c.cust_name from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name";
				//strDistrict = "select distinct state from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by state";
				//strPlace = "select distinct c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.city";
				//strCategory="select distinct Cust_Type from customer order by cust_type";
				strCategory = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				strPlace="select distinct city from customer order by city";
				
				//coment by vikas 25.05.09 strName="select distinct Cust_Name from customer order by Cust_Name";
				strName="select distinct Cust_Name,city from customer order by Cust_Name,city";

				SSR="select Emp_Name from Employee where Designation='Servo Sales Representative' and status=1 order by emp_name";
				
				District="select distinct state from customer order by state";

				//Coment by vikas 01.08.09 string[] arrStr = {strCategory,strName,strPlace,SSR};
				//Coment by vikas 01.08.09 HtmlInputHidden[] arrCust = {tempCustType,tempCustName,tempPlace,tempSSR};

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 17.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 17.11.2012
				
				//Coment by vikas 17.11.2012 string[] arrStr = {strCategory,strName,strPlace,SSR,District};
				//Coment by vikas 17.11.2012 HtmlInputHidden[] arrCust = {tempCustType,tempCustName,tempPlace,tempSSR,tempDist};
				
				strCustType="select distinct CustomertypeName from customertype";
				
				string[] arrStr = {strName,strPlace,SSR,District,strGroup,strSubGroup,strCustType};
				HtmlInputHidden[] arrCust = {tempCustName,tempPlace,tempSSR,tempDist,tempGroup,tempSubGroup,tempCustType};

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							/********Start Add by vikas 25.05.09***************/
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
							/********End**************************************/
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Party Wise Sales Figure Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public string GetSSRName(string SSR)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select Emp_Name from Employee where Emp_ID='"+SSR+"'");
			string sub="";
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString().IndexOf(" ")>0)
				{
					sub=rdr.GetValue(0).ToString();
					sub=sub.Substring(0,sub.IndexOf(" "));
					return sub;
				}
				else
                    return rdr.GetValue(0).ToString();
			}
			else
			{
				return "";
			}
			//rdr.Close();
		}
	}
}