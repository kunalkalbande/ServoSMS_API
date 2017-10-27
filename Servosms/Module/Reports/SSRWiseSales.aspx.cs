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
	/// Summary description for SSRWiseSales.
	/// </summary>
	public partial class SSRWiseSales : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
		public int Count=0,i=1;
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
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="44";
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
				InventoryClass obj = new InventoryClass();
				//coment by vikas 22.12.2012 SqlDataReader rdr = obj.GetRecordSet("select Emp_Name from Employee where Designation='Servo Sales Representative' order by Emp_Name");
				SqlDataReader rdr = obj.GetRecordSet("select Emp_Name from Employee where Designation='Servo Sales Representative' and status=1 order by Emp_Name");
				DropSSR.Items.Clear();
				DropSSR.Items.Add("All");
				while(rdr.Read())
				{
					DropSSR.Items.Add(rdr["Emp_Name"].ToString());
				}
				rdr.Close();
				
				//coment by vikas 25.05.09 rdr = obj.GetRecordSet("Select Cust_Name from Customer order by Cust_Name");
				rdr = obj.GetRecordSet("Select Cust_Name,city from Customer order by Cust_Name,city");
				DropCustomer.Items.Clear();
				DropCustomer.Items.Add("All");
				while(rdr.Read())
				{
					//coment by vikas 25.05.09 DropCustomer.Items.Add(rdr["Cust_Name"].ToString());
					DropCustomer.Items.Add(rdr["Cust_Name"].ToString()+":"+rdr["city"].ToString());
				}
				rdr.Close();
				datagrid11.Visible=false;
				GridDetails.Visible=false;
				txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
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

		/// <summary>
		/// To bind the datagrid and display the information by given order and display the data grid.
		/// </summary>
		public void BindTheData()
		{
			try
			{
				string cust_name="";
				SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				string sqlstr="";
				
				if(chkZeroBal.Checked!=true)
				{
					if(RadSummarized.Checked)
					{
						if(DropSSR.SelectedIndex!=0)
						{
							if(DropCustomer.SelectedIndex==0)
								//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							else
							{
								//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
								//coment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							}
						}
						else
						{
							if(DropCustomer.SelectedIndex==0)
								//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
							else
							{
								//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
								//comment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
							}
						}
					}
					else
					{
						if(DropSSR.SelectedIndex!=0)
						{
							if(DropCustomer.SelectedIndex==0)
								//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+txtDateTo.Text+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							else
							{
								//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+txtDateTo.Text+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
								//coment by vikas 25.05.09 sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							}
						}
						else
						{
							if(DropCustomer.SelectedIndex==0)
								//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							else
							{
								//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
								//Coment by vikas 25.05.09 sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							}
						}
					}
				}
				else
				{
					if(RadSummarized.Checked)
					{
						if(DropSSR.SelectedIndex!=0)
						{
							if(DropCustomer.SelectedIndex==0)
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,cust_id from customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Value.ToString()+"') order by cust_name";
							else
							{
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct cust_type m1,cust_name m2,city m3,cust_id from customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Value.ToString()+"') and cust_name='"+cust_name.ToString()+"' order by cust_name";
							}
						}
						else
						{
							if(DropCustomer.SelectedIndex==0)
								sqlstr="select cust_type m1,cust_name m2,city m3,cust_id from customer order by cust_name";
							else
							{
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select cust_type m1,cust_name m2,city m3,cust_id from customer where cust_name='"+cust_name.ToString()+"'";
							}
						}
					}
					else
					{
						if(DropSSR.SelectedIndex!=0)
						{
							if(DropCustomer.SelectedIndex==0)
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							else
							{
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							}
						}
						else
						{
							if(DropCustomer.SelectedIndex==0)
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							else
							{
								cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
								sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							}
						}
					}
				}
							
				DataSet ds= new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			
				/*16.07.09 Comment by vikas da.Fill(ds, "vw_SalesOil");
				DataTable dtCustomers = ds.Tables["vw_SalesOil"];*/

				DataTable dtCustomers=new DataTable();
				if(chkZeroBal.Checked!=true)
				{
					da.Fill(ds, "vw_SalesOil");
					dtCustomers = ds.Tables["vw_SalesOil"];
				}
				else
				{
					if(RadSummarized.Checked!=true)
					{
						da.Fill(ds, "Customer");
						dtCustomers = ds.Tables["Customer"];
					}
					else
					{
						da.Fill(ds, "vw_SalesOil");
						dtCustomers = ds.Tables["vw_SalesOil"];
					}
				}
				
				DataView dv=new DataView(dtCustomers);
				dv.Sort = strOrderBy;
				Cache["strOrderBy"]=strOrderBy;
				if(dv.Count==0)
				{
					MessageBox.Show("Data not available");
					datagrid11.Visible=false;
					GridDetails.Visible=false;
					GridSumZero.Visible=false;              //Add by vikas 16.07.09 
				}
				else
				{
					if(chkZeroBal.Checked!=true)             //Add by vikas 16.07.09 
					{
						if(RadSummarized.Checked)
						{
							datagrid11.DataSource=dv;
							datagrid11.DataBind();
							datagrid11.Visible=true;
							GridDetails.Visible=false;
							GridSumZero.Visible=false;
						}
						else
						{
							GridDetails.DataSource=dv;
							GridDetails.DataBind();
							datagrid11.Visible=false;
							GridDetails.Visible=true;
							GridSumZero.Visible=false;
						}
					}
					else													//Add by vikas 16.07.09 
					{
						if(RadSummarized.Checked)
						{
							GridSumZero.DataSource=dv;
							GridSumZero.DataBind();
							datagrid11.Visible=false;
							GridDetails.Visible=false;
							GridSumZero.Visible=true;
						}
						else
						{
							GridDetails.DataSource=dv;
							GridDetails.DataBind();
							datagrid11.Visible=false;
							GridDetails.Visible=true;
							GridSumZero.Visible=false;
						}
					}                                                      //Add by vikas 16.07.09 

					Count=1;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method : BindTheDate(),  EXCEPTION  "+ex.Message+" userid "+uid);
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
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
		}

		/// <summary>
		/// This method is used to view the report and set the column name with ascending order 
		/// in session variable.
		/// </summary>
		protected void btnview_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Coment by vikas 07.08.09 strOrderBy = "m1 ASC";
				//Coment by vikas 07.08.09 Session["Column"] = "m1";

				strOrderBy = "m2 ASC";
				Session["Column"] = "m2";
				Session["Order"] = "ASC";
				BindTheData();
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method : btnview_Click, userid "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method : btnview_Click,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
		}

		/// <summary>
		/// This method is used to calculate the total amount.
		/// </summary>
		double TotQty=0;
		public string SumTotQty(string amt)
		{
			TotQty+=System.Convert.ToDouble(amt);
			Cache["TotQty"]=TotQty;
			return GenUtil.strNumericFormat(amt);
		}

		double Tot_lub_Detail=0;
		public string Tot_Lub_Detail(string id)
		{
			string val="";
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader rdr=null;
			
				string str="select distinct sum(v.totqty), prod_name+':'+pack_type m1 from Sales_oil v,sales_master sm,products p where sm.cust_id="+id.ToString()+" and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by prod_name+':'+pack_type";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					val=rdr.GetValue(0).ToString();
				}
				else
				{
					val="0";
				}
				rdr.Close();
				Tot_lub_Detail+=System.Convert.ToDouble(val.ToString());
				Cache["Tot_lub_Detail"]=Tot_lub_Detail;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SSRWiseSalesFigure.aspx,Method : Tot_Lub,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
			return GenUtil.strNumericFormat(val);
		}

		double Tot_2t_Detail=0;
		public string Tot_2T_Detail(string id)
		{
			string val="";
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader rdr=null;
				
				string str="select distinct sum(v.oil2t), prod_name+':'+pack_type m1 from Sales_oil v,sales_master sm,products p where sm.cust_id="+id.ToString()+" and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by prod_name+':'+pack_type";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					val=rdr.GetValue(0).ToString();
				}
				else
				{
					val="0";
				}
				rdr.Close();
				Tot_2t_Detail+=System.Convert.ToDouble(val.ToString());
				Cache["Tot_2t_Detail"]=Tot_2t_Detail;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SSRWiseSalesFigure.aspx,Method : Tot_2T,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
			return GenUtil.strNumericFormat(val);
		}


		double Tot_4t_Detail=0;
		public string Tot_4T_Detail(string id)
		{
			string val="";
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader rdr=null;
				
				string str="select distinct sum(v.oil4t), prod_name+':'+pack_type m1 from Sales_oil v,sales_master sm,products p where sm.cust_id="+id.ToString()+" and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by prod_name+':'+pack_type";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					val=rdr.GetValue(0).ToString();
				}
				else
				{
					val="0";
				}
				rdr.Close();
				Tot_4t_Detail+=System.Convert.ToDouble(val.ToString());
				Cache["Tot_4t_Detail"]=Tot_4t_Detail;
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SSRWiseSalesFigure.aspx,Method : Tot_4T,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
			return GenUtil.strNumericFormat(val);
		}



		double Tot_lub=0;
		public string Tot_Lub(string id)
		{
			string val="";
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader rdr=null;
			
				string str="select distinct sum(v.totalqty) from vw_Salesoil v,sales_master sm where sm.cust_id="+id.ToString()+" and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_id";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					val=rdr.GetValue(0).ToString();
				}
				else
				{
					val="0";
				}
				rdr.Close();
				Tot_lub+=System.Convert.ToDouble(val.ToString());
				Cache["Tot_lub"]=Tot_lub;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SSRWiseSalesFigure.aspx,Method : Tot_Lub,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
			return GenUtil.strNumericFormat(val);
		}


		
		double Tot_2t=0;
		public string Tot_2T(string id)
		{
			string val="";
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader rdr=null;
				
				string str="select distinct sum(v.oil2t) from vw_Salesoil v,sales_master sm where sm.cust_id="+id.ToString()+" and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_id";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					val=rdr.GetValue(0).ToString();
				}
				else
				{
					val="0";
				}
				rdr.Close();
				Tot_2t+=System.Convert.ToDouble(val.ToString());
				Cache["Tot_2t"]=Tot_2t;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SSRWiseSalesFigure.aspx,Method : Tot_2T,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
			return GenUtil.strNumericFormat(val);
		}


		double Tot_4t=0;
		public string Tot_4T(string id)
		{
			string val="";
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader rdr=null;
				
				string str="select distinct sum(v.oil4t) from vw_Salesoil v,sales_master sm where sm.cust_id="+id.ToString()+" and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_id";
				rdr=obj.GetRecordSet(str);
				if(rdr.Read())
				{
					val=rdr.GetValue(0).ToString();
				}
				else
				{
					val="0";
				}
				rdr.Close();
				Tot_4t+=System.Convert.ToDouble(val.ToString());
				Cache["Tot_4t"]=Tot_4t;
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SSRWiseSalesFigure.aspx,Method : Tot_4T,  EXCEPTION  "+ex.Message+" userid "+uid);
			}
			return GenUtil.strNumericFormat(val);
		}


		double Oil2t=0;
		/// <summary>
		/// This method is used to calculate the total 2t oil amount.
		/// </summary>
		public string SumOil2t(string amt)
		{
			Oil2t+=System.Convert.ToDouble(amt);
			Cache["Oil2t"]=Oil2t;
			return GenUtil.strNumericFormat(amt);
		}

		double Oil4t=0;
		/// <summary>
		/// This method is used to calculate the total 4t oil amount.
		/// </summary>
		public string SumOil4t(string amt)
		{
			Oil4t+=System.Convert.ToDouble(amt);
			Cache["Oil4t"]=Oil4t;
			return GenUtil.strNumericFormat(amt);
		}

		/// <summary>
		/// Prepares the report file SSRWiseSalesReport.txt for printing.
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
					CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Mechanic Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SSRWiseSalesFigure.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRWiseSalesFigure Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRWiseSalesFigure Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRWiseSalesFigure Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    SSRWiseSalesFigure Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			InventoryClass obj1=new InventoryClass();
			//SqlDataReader SqlDtr;
			string sql="";
			string cust_name="";
			int x=0;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SSRWiseSalesFigure.xls";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;
			
			/*16.07.09 vikas if(RadSummarized.Checked)
			{
				if(DropSSR.SelectedIndex!=0)
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
					else
					{
						//coment by vikas 25.05.09 sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
					}
				}
				else
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
					else
					{
						//Coment by vikas 25.05.09 sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
					}
				}
			}
			else
			{
				if(DropSSR.SelectedIndex!=0)
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					else
					{
						//coment by vikas 25.05.09 sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					}
				}
				else
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					else
					{
						//coment by vikas 25.05.09 sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					}
				}
			}*/

			/*****************Add by vikas 16.07.09***************************************/
			if(chkZeroBal.Checked!=true)
			{
				if(RadSummarized.Checked)
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
						else
						{
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							//coment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						else
						{
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
							//comment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						}
					}
				}
				else
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+txtDateTo.Text+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+txtDateTo.Text+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							//coment by vikas 25.05.09 sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							//Coment by vikas 25.05.09 sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
				}
			}
			else
			{
				if(RadSummarized.Checked)
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select distinct cust_type m1,cust_name m2,city m3,cust_id from customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Value.ToString()+"')";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,cust_id from customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Value.ToString()+"') and cust_name='"+cust_name.ToString()+"'";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select cust_type m1,cust_name m2,city m3,cust_id from customer";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select cust_type m1,cust_name m2,city m3,cust_id from customer where cust_name='"+cust_name.ToString()+"'";
						}
					}
				}
				else
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
				}
			}
			/*****************Add by vikas 16.07.09***************************************/


			sql=sql+" order by "+Cache["strOrderBy"];
			rdr=obj.GetRecordSet(sql);
			if(chkZeroBal.Checked!=true)
			{
				if(RadSummarized.Checked)
				{
					sw.WriteLine("SNo\tCategory\tParty Name\tPlace\tTotal Lub\t2T Sales\t4T Sales");
					int i=1;
					while(rdr.Read())
					{
						sw.WriteLine(i.ToString()+"\t"+rdr["m1"].ToString()+"\t"+
							rdr["m2"].ToString()+"\t"+
							rdr["m3"].ToString()+"\t"+
							GenUtil.strNumericFormat(rdr["m4"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["m5"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["m6"].ToString())
							);
						i++;
					}
					sw.WriteLine("\tTotal\t\t\t"+GenUtil.strNumericFormat(Cache["TotQty"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil2t"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
				}
				else
				{
					sw.WriteLine("SNo\tCategory\tParty Name\tPlace\tProduct Name\tTotal Lub\t2T Sales\t4T Sales");
					int i=1;
					while(rdr.Read())
					{
						sw.WriteLine(i.ToString()+"\t"+rdr["m1"].ToString()+"\t"+
							rdr["m2"].ToString()+"\t"+
							rdr["m3"].ToString()+"\t"+
							rdr["m7"].ToString()+"\t"+
							GenUtil.strNumericFormat(rdr["m4"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["m5"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["m6"].ToString())
							);
						i++;
					}
					sw.WriteLine("\tTotal\t\t\t\t"+GenUtil.strNumericFormat(Cache["TotQty"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil2t"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
				}
			}
			else
			{
				if(RadSummarized.Checked)
				{
					sw.WriteLine("SNo\tCategory\tParty Name\tPlace\tTotal Lub\t2T Sales\t4T Sales");
					int i=1;
					while(rdr.Read())
					{
						sw.WriteLine(i.ToString()+"\t"+rdr["m1"].ToString()+"\t"+
							rdr["m2"].ToString()+"\t"+
							rdr["m3"].ToString()+"\t"+
							Tot_Lub(rdr["Cust_id"].ToString())+"\t"+
							Tot_2T(rdr["Cust_id"].ToString())+"\t"+
							Tot_4T(rdr["Cust_id"].ToString())
							);
						i++;
					}
					sw.WriteLine("\tTotal\t\t\t"+GenUtil.strNumericFormat(Cache["Tot_lub"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Tot_2t"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Tot_4t"].ToString()));
				}
				else
				{
					sw.WriteLine("SNo\tCategory\tParty Name\tPlace\tProduct Name\tTotal Lub\t2T Sales\t4T Sales");
					int i=1;
					while(rdr.Read())
					{
						sw.WriteLine(i.ToString()+"\t"+rdr["m1"].ToString()+"\t"+
							rdr["m2"].ToString()+"\t"+
							rdr["m3"].ToString()+"\t"+
							rdr["m7"].ToString()+"\t"+
							GenUtil.strNumericFormat(rdr["m4"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["m5"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["m6"].ToString())
							);
						i++;
					}
					sw.WriteLine("\tTotal\t\t\t\t"+GenUtil.strNumericFormat(Cache["TotQty"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil2t"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
				}
			}

			rdr.Close();
			sw.Close();
			// truncate table after use.
			dbobj.Insert_or_Update("truncate table stkmv", ref x);
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;//,rdr1=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SSRWiseSalesFigure.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			string cust_name="";
			
			/*16.07.09 vikas if(RadSummarized.Checked)
			{
				if(DropSSR.SelectedIndex!=0)
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
					else
					{
						//coment by vikas 25.05.09 sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
					}
				}
				else
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
					else
					{
						//coment by vikas 25.05.09 sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
					}
				}
			}
			else
			{
				if(DropSSR.SelectedIndex!=0)
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					else
					{
						//Coment by vikas 25.05.09 sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					}
				}
				else
				{
					if(DropCustomer.SelectedIndex==0)
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					else
					{
						//coment by vikas 25.05.09 sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
						sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
					}
				}
			}*/



			/*****************Add by vikas 16.07.09***************************************/
			if(chkZeroBal.Checked!=true)
			{
				if(RadSummarized.Checked)
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
						else
						{
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							//coment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by cust_type,cust_name,city,city";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						else
						{
							//sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
							//comment by vikas 25.05.09 sqlstr="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"' group by cust_type,cust_name,city,city";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_Name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(v.SaleDate as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103) group by cust_type,cust_name,city,city";
						}
					}
				}
				else
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+txtDateTo.Text+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+txtDateTo.Text+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							//coment by vikas 25.05.09 sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							//sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							//Coment by vikas 25.05.09 sqlstr="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+DropCustomer.SelectedItem.Text+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
				}
			}
			else
			{
				if(RadSummarized.Checked)
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select distinct cust_type m1,cust_name m2,city m3,cust_id from customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Value.ToString()+"')";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct cust_type m1,cust_name m2,city m3,cust_id from customer where ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Value.ToString()+"') and cust_name='"+cust_name.ToString()+"'";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select cust_type m1,cust_name m2,city m3,cust_id from customer";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select cust_type m1,cust_name m2,city m3,cust_id from customer where cust_name='"+cust_name.ToString()+"'";
						}
					}
				}
				else
				{
					if(DropSSR.SelectedIndex!=0)
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropSSR.SelectedItem.Text+"') group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
					else
					{
						if(DropCustomer.SelectedIndex==0)
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						else
						{
							cust_name=DropCustomer.SelectedValue.Substring(0,DropCustomer.SelectedValue.IndexOf(":"));
							sql="select distinct c.cust_type m1,cust_name m2,city m3,sum(v.totqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,prod_name+':'+pack_type m7 from customer c,Sales_oil v,sales_master sm,products p where c.cust_id=sm.cust_id and v.Invoice_no=sm.invoice_no and p.prod_id=v.prod_id and cust_name='"+cust_name.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type,cust_name,city,city,prod_name,pack_type";
						}
					}
				}
			}
			/*****************Add by vikas 16.07.09***************************************/


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
			string des="";
			if(RadSummarized.Checked)
				des="--------------------------------------------------------------------------------------------------------";
			else
				des="----------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("============================================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SSR Wise Sales Figure Report From "+txtDateFrom.Text+" To "+txtDateTo.Text+", SSR : "+DropSSR.SelectedItem.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("============================================================================",des.Length));
			//sw.WriteLine("SSR : "+DropSSR.SelectedItem.Text);
			if(RadSummarized.Checked)
			{
				sw.WriteLine("+-----+------------+-------------------------+---------------+-------------+-------------+-------------+");
				sw.WriteLine("| SNo |  CaTegory  |       Party Name        |     Place     |   TotLub    |  2T Sales   |  4T Sales   |");
				sw.WriteLine("+-----+------------+-------------------------+---------------+-------------+-------------+-------------+");
				//             12345 123456789012 1234567890123456789012345 123456789012345 1234567890123 1234567890123 1234567890123
				info = " {0,-5:S} {1,-12:S} {2,-25:F} {3,-15:S} {4,13:S} {5,13:S} {6,13:S}";
			}
			else
			{
				sw.WriteLine("+-----+------------+-------------------------+---------------+-------------------------------+-------------+-------------+-------------+");
				sw.WriteLine("| SNo |  CaTegory  |       Party Name        |     Place     |          Product Name         |   TotLub    |  2T Sales   |  4T Sales   |");
				sw.WriteLine("+-----+------------+-------------------------+---------------+-------------------------------+-------------+-------------+-------------+");
				//             12345 123456789012 1234567890123456789012345 123456789012345 1234567890123456789012345678901 1234567890123 1234567890123 1234567890123
				info = " {0,-5:S} {1,-12:S} {2,-25:F} {3,-15:S} {4,-31:S} {5,13:S} {6,13:S} {7,13:S}";
			}
			if(chkZeroBal.Checked!=true)
			{
				if(RadSummarized.Checked)
				{
					if(rdr.HasRows)
					{
						int i=1;
						while(rdr.Read())
						{
							sw.WriteLine(info,i.ToString(),GenUtil.TrimLength(rdr["m1"].ToString(),12),
								GenUtil.TrimLength(rdr["m2"].ToString(),25),
								GenUtil.TrimLength(rdr["m3"].ToString(),15),
								GenUtil.strNumericFormat(rdr["m4"].ToString()),
								GenUtil.strNumericFormat(rdr["m5"].ToString()),
								GenUtil.strNumericFormat(rdr["m6"].ToString())
								);
							i++;
						}
					}
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------+-------------+-------------+");
					sw.WriteLine(info,"","   Total","","",GenUtil.strNumericFormat(Cache["TotQty"].ToString()),GenUtil.strNumericFormat(Cache["Oil2t"].ToString()),GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------+-------------+-------------+");
				}
				else
				{
					if(rdr.HasRows)
					{
						int i=1;
						while(rdr.Read())
						{
							sw.WriteLine(info,i.ToString(),GenUtil.TrimLength(rdr["m1"].ToString(),12),
								GenUtil.TrimLength(rdr["m2"].ToString(),25),
								GenUtil.TrimLength(rdr["m3"].ToString(),15),
								GenUtil.TrimLength(rdr["m7"].ToString(),31),
								GenUtil.strNumericFormat(rdr["m4"].ToString()),
								GenUtil.strNumericFormat(rdr["m5"].ToString()),
								GenUtil.strNumericFormat(rdr["m6"].ToString())
								);
							i++;
						}
					}
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------------------------+-------------+-------------+-------------+");
					sw.WriteLine(info,"","   Total","","","",GenUtil.strNumericFormat(Cache["TotQty"].ToString()),GenUtil.strNumericFormat(Cache["Oil2t"].ToString()),GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------------------------+-------------+-------------+-------------+");
				}
			}
			/*********Following else part add by vikas 16.07.09************************/
			else
			{
				if(RadSummarized.Checked)
				{
					if(rdr.HasRows)
					{
						int i=1;
						while(rdr.Read())
						{
							sw.WriteLine(info,i.ToString(),GenUtil.TrimLength(rdr["m1"].ToString(),12),
								GenUtil.TrimLength(rdr["m2"].ToString(),25),
								GenUtil.TrimLength(rdr["m3"].ToString(),15),
								Tot_Lub(rdr["Cust_id"].ToString()),
								Tot_2T(rdr["Cust_id"].ToString()),
								Tot_4T(rdr["Cust_id"].ToString())
								);
							i++;
						}
					}
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------+-------------+-------------+");
					sw.WriteLine(info,"","   Total","","",GenUtil.strNumericFormat(Cache["Tot_lub"].ToString()),GenUtil.strNumericFormat(Cache["Tot_2t"].ToString()),GenUtil.strNumericFormat(Cache["Tot_4t"].ToString()));
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------+-------------+-------------+");
				}
				else
				{
					if(rdr.HasRows)
					{
						int i=1;
						while(rdr.Read())
						{
							sw.WriteLine(info,i.ToString(),GenUtil.TrimLength(rdr["m1"].ToString(),12),
								GenUtil.TrimLength(rdr["m2"].ToString(),25),
								GenUtil.TrimLength(rdr["m3"].ToString(),15),
								GenUtil.TrimLength(rdr["m7"].ToString(),31),
								GenUtil.strNumericFormat(rdr["m4"].ToString()),
								GenUtil.strNumericFormat(rdr["m5"].ToString()),
								GenUtil.strNumericFormat(rdr["m6"].ToString())
								);
							i++;
						}
					}
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------------------------+-------------+-------------+-------------+");
					sw.WriteLine(info,"","   Total","","","",GenUtil.strNumericFormat(Cache["TotQty"].ToString()),GenUtil.strNumericFormat(Cache["Oil2t"].ToString()),GenUtil.strNumericFormat(Cache["Oil4t"].ToString()));
					sw.WriteLine("+-----+------------+-------------------------+---------------+-------------------------------+-------------+-------------+-------------+");
				}
			}
			/*********End*************************************/
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file SSRWiseSalesReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//16.07.09 vikas if(datagrid11.Visible==true || GridDetails.Visible==true)
				if(datagrid11.Visible==true || GridDetails.Visible==true || GridSumZero.Visible==true)
				{
					ConvertToExcel();
					Count=1;
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SSRWiseSalesFigure Report Convert Into Excel Format ,  userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:SSRWiseSalesFigure.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SSRWiseSalesFigure Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}
	}
}