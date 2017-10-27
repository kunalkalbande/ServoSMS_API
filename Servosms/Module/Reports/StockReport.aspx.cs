/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
using DBOperations;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Sockets;
using Servosms.Sysitem.Classes ;
using System.IO;
using System.Text;
using RMG;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for StockReport.
	/// </summary>
	public partial class StockReport : System.Web.UI.Page
	{
	
	
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
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
				CreateLogFiles.ErrorLog("Form:StockReport.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{	
				try
				{
					grdLeg.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="48";
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
					txtDateTo.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					System.Data.SqlClient.SqlDataReader rdr=null;//,rdr1=null;
					// Fetch the store location or tank id according to the product type and fill the combo.
					dbobj.SelectQuery("select distinct store_in from products",ref rdr);
					while(rdr.Read())
					{
						/*Hide by Mahesh because delete the table of tank from the database.
						dbobj.SelectQuery("select tank_id,tank_name,prod_name from tank where tank_id like'"+rdr["store_in"].ToString()+"'",ref rdr1);
						if(rdr1.Read())
						{
							drpstore.Items.Add(rdr1["tank_name"].ToString()+":"+rdr1["prod_name"].ToString());					
						}
						else
						*/
						drpstore.Items.Add(rdr["store_in"].ToString());					
					}
					drpstore.Items.Insert(0,"All");
					dbobj.Dispose();
					
					SqlDataReader rdr3=null;
					dbobj.SelectQuery("select distinct Pack_Type from vw_StockReport",ref rdr3);
					while(rdr3.Read())
					{
						DropPackType.Items.Add(rdr3.GetValue(0).ToString());
					}
					DropPackType.Items.Insert(0,"All");
					rdr3.Close();
					dbobj.Dispose();
					dbobj.SelectQuery("select distinct category from vw_StockReport order by category",ref rdr3);
					while(rdr3.Read())
					{
						DropProdGroup.Items.Add(rdr3.GetValue(0).ToString());
					}
					rdr3.Close();
					dbobj.Dispose();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:StockReport.aspx,Method:page_load EXCEPTION  "+ex.Message+" userid "+ uid);
				}
			}
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }
		
		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of the datagridheader.
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
				CreateLogFiles.ErrorLog("Form:StockReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to binding the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//***********
			grdLeg.Visible=true;
			//System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="";
			object op= null;
			//call the procedure and create the temp table. stk.
			dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());
			if(txtDateTo.Text==System.DateTime.Now.ToShortDateString())
			{
				if(drpstore.SelectedIndex>0)
				{
					if(drpstore.SelectedItem.Text.IndexOf(":")>0)
					{
						string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
						string tid="";
						//** Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
						if(DropPackType.SelectedItem.Text.Equals("All"))
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
						else
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
					}
					else 
					{
						if(DropPackType.SelectedItem.Text.Equals("All"))
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
						else
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
					}
				}
				else
				{
					//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
			}
			else if(drpstore.SelectedIndex>0)
			{
				if(drpstore.SelectedItem.Text.IndexOf(":")>0)
				{
					string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
					string tid="";
					//** Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}

				}
				else 
				{
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
				
			}
			else 
			{
				if(DropPackType.SelectedItem.Text.Equals("All"))
				{
					if(DropProdGroup.SelectedIndex==0)
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.category='"+DropProdGroup.SelectedItem.Text+"'";
				}
				else
				{
					if(DropProdGroup.SelectedIndex==0)
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
				}
				//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
				Trace.Write(sql);
			}
			if(chkZeroStock.Checked==false)
				sql+=" and a.Closing_Stock!=0";
			//***********
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_stockreport");
			DataTable dtcustomer=ds.Tables["vw_stockreport"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			if(chkAll.Checked)
				grdMRPAmount.DataSource=dv;
			else if(chkMRP.Checked)
				grdMRP.DataSource=dv;
			else if(chkAmount.Checked)
				grdAmount.DataSource=dv;
			else
				grdLeg.DataSource=dv;
			if(dv.Count!=0)
			{
				if(chkAll.Checked)
				{
					grdMRPAmount.DataBind();
					grdMRPAmount.Visible=true;
					grdLeg.Visible=false;
					grdMRP.Visible=false;
					grdAmount.Visible=false;
				}
				else if(chkMRP.Checked)
				{
					grdMRP.DataBind();
					grdMRP.Visible=true;
					grdMRPAmount.Visible=false;
					grdLeg.Visible=false;
					grdAmount.Visible=false;
				}
				else if(chkAmount.Checked)
				{
					grdAmount.DataBind();
					grdAmount.Visible=true;
					grdLeg.Visible=false;
					grdMRP.Visible=false;
					grdMRPAmount.Visible=false;
				}
				else 
				{
					grdLeg.DataBind();
					grdAmount.Visible=false;
					grdLeg.Visible=true;
					grdMRP.Visible=false;
					grdMRPAmount.Visible=false;
				}
			}
			else
			{
				grdLeg.Visible=false;
				grdMRP.Visible=false;
				grdMRPAmount.Visible=false;
				grdAmount.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
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
		/// Prepares the report file StockReport.txt for printing.
		/// </summary>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{  
			try
			{
				/*************bhal com***********************
				grdLeg.Visible=true;
				
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";

				object op= null;
				//call the procedure and create the temp table. stk.
				dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());
	
				if(txtDateTo.Text==System.DateTime.Now.ToShortDateString())
				{
					if(drpstore.SelectedIndex>0)
					{
						if(drpstore.SelectedItem.Text.IndexOf(":")>0)
						{
							string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
							string tid="";
							dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";

						}
						else 
						{
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						}
					}
					else
					sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
				}
				else if(drpstore.SelectedIndex>0)
				{
					if(drpstore.SelectedItem.Text.IndexOf(":")>0)
					{
						string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
						string tid="";
						dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";

					}
					else 
					{
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
					}
				
				}
				else 
				{
					sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
					Trace.Write(sql);
				}
		                
				dbobj.SelectQuery(sql,ref rdr);
				if(rdr.HasRows)
				{
					grdLeg.DataSource=rdr;
					grdLeg.DataBind();
				}
				else
				{
					RMG.MessageBox.Show("Data not available");
					grdLeg.Visible=false;
				}
				rdr.Close();
********************bhal com end*******************/	
				strorderby="Prod_Code ASC";
				Session["Column"]="Prod_Code";
				Session["order"]="ASC";
				Bindthedata();
		   
				CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:cmdrpt_Click  Stock Report Viewed   userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:cmdrpt_Click,   Stock Report Viewed  EXCEPTION  "+ex.Message+" userid "+uid);		
			}
		}

		/// <summary>
		/// Method to prepare the report file .txt
		/// </summary>
		public void makingReport()
		{
			/*
						================                                
						  STOCK REPORT                                  
						================                                
+--------------------+---------------+-------------------------+
|                    |               |      Closing Stock      |
|     Product        |    Location   |-------------+-----------|
|                    |               |    Pkg      |  Lt/kg    |	
+--------------------+---------------+-------------+-----------+
 12345678901234567890 123456789012345 12345678.00   12345678.00  
			 */
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="";
			string info="",infoAll="",infoAmount="",infoMRP="";

			object op= null;
			dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());
			if(txtDateTo.Text==System.DateTime.Now.ToShortDateString())
			{
				if(drpstore.SelectedIndex>0)
				{
					if(drpstore.SelectedItem.Text.IndexOf(":")>0)
					{
						string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
						string tid="";
						//**Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
						if(DropPackType.SelectedItem.Text.Equals("All"))
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
						else
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
					}
					else 
					{
						if(DropPackType.SelectedItem.Text.Equals("All"))
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
						else
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
					}
				}
				else
				{
					//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}

				}
				
			}
			else if(drpstore.SelectedIndex>0)
			{
					
				if(drpstore.SelectedItem.Text.IndexOf(":")>0)
				{
					string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
					string tid="";
					//**Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
				else 
				{
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
			}
			else 
			{
				//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
				if(DropPackType.SelectedItem.Text.Equals("All"))
				{
					if(DropProdGroup.SelectedIndex==0)
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate ";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate  and a.category='"+DropProdGroup.SelectedItem.Text+"'";
				}
				else
				{
					if(DropProdGroup.SelectedIndex==0)
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
				}
				Trace.Write(sql);
			}
			if(chkZeroStock.Checked==false)
				sql+=" and a.Closing_Stock!=0";
			sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\StockReport.txt";
			//string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\StockReport.xls";
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
			string des="";
			if(chkAll.Checked)
				des="-----------------------------------------------------------------------------------------------------------------------";
			else if(chkMRP.Checked)
				des="----------------------------------------------------------------------------------------------------------";
			else if(chkAmount.Checked)
				des="---------------------------------------------------------------------------------------------------------------";
			else
				des="--------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("===============================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("STOCK REPORT AS ON "+ txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("===============================",des.Length));
			//sw.WriteLine("Date : "+txtDateTo.Text);
			//sw.WriteLine("Location : "+drpstore.SelectedItem.Text+",  Package Type : "+DropPackType.SelectedItem.Text+",  Product Group : "+DropProdGroup.SelectedItem.Text);
			//sw.WriteLine("Package Type : "+DropPackType.SelectedItem.Text);
			if(chkAll.Checked)
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------------------+-------+------------+");
				sw.WriteLine("| Product |                                     |        |      Product       |      Closing Stock      |       |            |");
				sw.WriteLine("|  Code   |           Product                   |Location|       Group        |-------------+-----------|  MRP  |   Amount   |");
				sw.WriteLine("|         |                                     |        |                    |    Pkg      |  Lt/kg    |       |            |");
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+-------+------------+");
				//             123456789 1234567890123456789012345678901234567 12345678 12345678901234567890 1234567890123 12345678901 1234567 123456789012
			}
			else if(chkMRP.Checked)
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------------------+-------+");
				sw.WriteLine("| Product |                                     |        |      Product       |      Closing Stock      |       |");
				sw.WriteLine("|  Code   |           Product                   |Location|       Group        |-------------+-----------|  MRP  |");
				sw.WriteLine("|         |                                     |        |                    |    Pkg      |  Lt/kg    |       |");
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+-------+");
				//             123456789 1234567890123456789012345678901234567 12345678 12345678901234567890 1234567890123 12345678901 1234567
			}
			else if(chkAmount.Checked)
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------------------+------------+");
				sw.WriteLine("| Product |                                     |        |      Product       |      Closing Stock      |            |");
				sw.WriteLine("|  Code   |           Product                   |Location|       Group        |-------------+-----------|   Amount   |");
				sw.WriteLine("|         |                                     |        |                    |    Pkg      |  Lt/kg    |            |");
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+------------+");
				//             123456789 1234567890123456789012345678901234567 12345678 12345678901234567890 1234567890123 12345678901 123456789012
			}
			else
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------------------+");
				sw.WriteLine("| Product |                                     |        |      Product       |      Closing Stock      |");
				sw.WriteLine("|  Code   |           Product                   |Location|       Group        |-------------+-----------|");
				sw.WriteLine("|         |                                     |        |                    |    Pkg      |  Lt/kg    |");
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+");
				//             123456789 1234567890123456789012345678901234567 12345678 12345678901234567890 1234567890123 12345678901 
			}
			string pack;
			string strPackCl;
			string[] strSplit;
			if(rdr.HasRows)
			{
				// info : set format of the string to display.
				info = " {0,-9:S} {1,-37:S} {2,-8:S} {3,-20:S} {4,13:S} {5,11:F}";
				infoAmount = " {0,-9:S} {1,-37:S} {2,-8:S} {3,-20:S} {4,13:S} {5,11:F} {6,12:F}";
				infoMRP = " {0,-9:S} {1,-37:S} {2,-8:S} {3,-20:S} {4,13:S} {5,11:F} {6,7:F}";
				infoAll = " {0,-9:S} {1,-37:S} {2,-8:S} {3,-20:S} {4,13:S} {5,11:F} {6,7:F} {7,12:F}";
				while(rdr.Read())
				{
					pack="";
					strPackCl="";
					// if product category is fuel hen not dipslay the package, display the tank abbr name. 
					if(rdr["category"].ToString().ToUpper().Equals("FUEL"))
					{
						string prod_name="";
						dbobj.SelectQuery("select Prod_AbbName from tank where tank_id='"+rdr["store_in"].ToString().Trim()+"'","Prod_AbbName",ref prod_name);
						sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),prod_name,"",rdr["closing_stock"].ToString(),rdr["MRP"].ToString()); 
					}
						// if package is Loose Oil the not display the package type.
					else if(rdr["pack_type"].ToString().IndexOf("Loose") != -1)
					{
						sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),rdr["store_in"].ToString().Trim(),"",rdr["closing_stock"].ToString(),rdr["MRP"].ToString()); 
					}
					else
					{
						pack = rdr["pack_type"].ToString().Trim();

						if (pack.IndexOf("X")<0 || pack.Equals("") )
						{
							strPackCl = rdr["closing_stock"].ToString().Trim();
							
						}
						else
						{
							strSplit = pack.Split(new char []{'X'},pack.Length);
							double d1 = 1;
							double d2 = 1;
							if(!strSplit[0].Trim().Equals (""))
								d1 = System.Convert.ToDouble(strSplit[0]);
							if(!strSplit[1].Trim().Equals (""))
								d2 = System.Convert.ToDouble(strSplit[1]);
							strPackCl = rdr["closing_stock"].ToString().Trim();
							if(!strPackCl.Equals(""))
							{
								strPackCl = ""+System.Convert.ToDouble(strPackCl)*d1*d2 ;            
							}
						}
						if(chkAll.Checked)
							sw.WriteLine(infoAll,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),37),rdr["store_in"].ToString().Trim(),rdr["category"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl,rdr["MRP"].ToString(),GetAmount(rdr["Prod_ID"].ToString(),rdr["closing_stock"].ToString()));
						else if(chkAmount.Checked)
							sw.WriteLine(infoAmount,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),37),rdr["store_in"].ToString().Trim(),rdr["category"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl,GetAmount(rdr["Prod_ID"].ToString(),rdr["closing_stock"].ToString())); 
						else if(chkMRP.Checked)
							sw.WriteLine(infoMRP,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),37),rdr["store_in"].ToString().Trim(),rdr["category"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl,rdr["MRP"].ToString());
						else
							sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),37),rdr["store_in"].ToString().Trim(),rdr["category"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl);
					}
				}
			}
			if(chkAll.Checked)
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+-------+------------+");
				sw.WriteLine(infoAll,"Total:","","","",Cache["csp"],Cache["cs"],"",GenUtil.strNumericFormat(Cache["Amount"].ToString()));
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+-------+------------+");
			}
			else if(chkAmount.Checked)
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+------------+");
				sw.WriteLine(infoAmount,"Total:","","","",Cache["csp"],Cache["cs"],GenUtil.strNumericFormat(Cache["Amount"].ToString()));
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+------------+");
			}
			else if(chkMRP.Checked)
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+-------+");
				sw.WriteLine(infoMRP,"Total:","","","",Cache["csp"],Cache["cs"],"");
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+-------+");
			}
			else
			{
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+");
				sw.WriteLine(info,"Total:","","","",Cache["csp"],Cache["cs"]);
				sw.WriteLine("+---------+-------------------------------------+--------+--------------------+-------------+-----------+");
			}
			dbobj.Dispose();
			rdr.Close();
			sw.Close(); 
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\StockReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			object op= null;
			dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());
			if(txtDateTo.Text==System.DateTime.Now.ToShortDateString())
			{
				if(drpstore.SelectedIndex>0)
				{
					if(drpstore.SelectedItem.Text.IndexOf(":")>0)
					{
						string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
						string tid="";
						//**Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
						if(DropPackType.SelectedItem.Text.Equals("All"))
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
						else
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
					}
					else 
					{
						if(DropPackType.SelectedItem.Text.Equals("All"))
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
						else
						{
							if(DropProdGroup.SelectedIndex==0)
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
							else
								sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
						}
					}
				}
				else
				{
					//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
			}
			else if(drpstore.SelectedIndex>0)
			{
				if(drpstore.SelectedItem.Text.IndexOf(":")>0)
				{
					string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
					string tid="";
					//**Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
				else 
				{
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
					else
					{
						if(DropProdGroup.SelectedIndex==0)
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
					}
				}
			}
			else 
			{
				//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
				if(DropPackType.SelectedItem.Text.Equals("All"))
				{
					if(DropProdGroup.SelectedIndex==0)
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate ";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate  and a.category='"+DropProdGroup.SelectedItem.Text+"'";
				}
				else
				{
					if(DropProdGroup.SelectedIndex==0)
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"' and a.category='"+DropProdGroup.SelectedItem.Text+"'";
				}
				Trace.Write(sql);
			}
			if(chkZeroStock.Checked==false)
				sql+=" and a.Closing_Stock!=0";
			sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			sw.WriteLine("Date\t"+txtDateTo.Text);
			//sw.WriteLine("Location\t"+drpstore.SelectedItem.Text);
			//sw.WriteLine("Product Group\t"+DropProdGroup.SelectedItem.Text);
			//sw.WriteLine("Package Type\t"+DropPackType.SelectedItem.Text);
			sw.WriteLine();
			if(chkAll.Checked)
				sw.WriteLine("Product Code\tProduct Name\tLocation\tProduct Group\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)\tMRP\tAmount");
			else if(chkMRP.Checked)
				sw.WriteLine("Product Code\tProduct Name\tLocation\tProduct Group\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)\tMRP");
			else if(chkAmount.Checked)
				sw.WriteLine("Product Code\tProduct Name\tLocation\tProduct Group\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)\tAmount");
			else
				sw.WriteLine("Product Code\tProduct Name\tLocation\tProduct Group\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)");
			string pack;
			string strPackCl;
			string[] strSplit;
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					pack="";
					strPackCl="";
					// if product category is fuel hen not dipslay the package, display the tank abbr name. 
					//if(rdr["category"].ToString().ToUpper().Equals("FUEL"))
					//{
					//	string prod_name="";
					//	dbobj.SelectQuery("select Prod_AbbName from tank where tank_id='"+rdr["store_in"].ToString().Trim()+"'","Prod_AbbName",ref prod_name);
					//	sw.WriteLine(rdr["Product"].ToString().Trim()+"\t"+prod_name+"\t"+""+"\t"+rdr["closing_stock"].ToString()); 
					//}
					// if package is Loose Oil the not display the package type.
					//else if(rdr["pack_type"].ToString().IndexOf("Loose") != -1)
					if(rdr["pack_type"].ToString().IndexOf("Loose") != -1)
						sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["store_in"].ToString().Trim()+"\t"+""+"\t"+rdr["closing_stock"].ToString()+"\t"+rdr["MRP"].ToString()); 
					else
					{
						pack = rdr["pack_type"].ToString().Trim();
						if (pack.IndexOf("X")<0 || pack.Equals("") )
							strPackCl = rdr["closing_stock"].ToString().Trim();
						else
						{
							strSplit = pack.Split(new char []{'X'},pack.Length);
							double d1 = 1;
							double d2 = 1;
							if(!strSplit[0].Trim().Equals (""))
								d1 = System.Convert.ToDouble(strSplit[0]);
							if(!strSplit[1].Trim().Equals (""))
								d2 = System.Convert.ToDouble(strSplit[1]);
							strPackCl = rdr["closing_stock"].ToString().Trim();
							if(!strPackCl.Equals(""))
								strPackCl = ""+System.Convert.ToDouble(strPackCl)*d1*d2 ;            
						}
						if(chkAll.Checked)
							sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["store_in"].ToString().Trim()+"\t"+rdr["category"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl+"\t"+rdr["MRP"].ToString()+"\t"+GetAmount(rdr["Prod_ID"].ToString(),rdr["Closing_Stock"].ToString())); 
						else if(chkMRP.Checked)
							sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["store_in"].ToString().Trim()+"\t"+rdr["category"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl+"\t"+rdr["MRP"].ToString()); 
						else if(chkAmount.Checked)
							sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["store_in"].ToString().Trim()+"\t"+rdr["category"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl+"\t"+GetAmount(rdr["Prod_ID"].ToString(),rdr["Closing_Stock"].ToString())); 
						else
							sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["store_in"].ToString().Trim()+"\t"+rdr["category"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl); 
					}
				}
			}
			if(chkAll.Checked)
				sw.WriteLine("Total\t\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]+"\t\t"+GenUtil.strNumericFormat(Cache["Amount"].ToString()));
			else if(chkMRP.Checked)
				sw.WriteLine("Total\t\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]);
			else if(chkAmount.Checked)
				sw.WriteLine("Total\t\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]+"\t"+GenUtil.strNumericFormat(Cache["Amount"].ToString()));
			else
				sw.WriteLine("Total\t\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]);
			dbobj.Dispose();
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method is not used
		/// </summary>
		private string GetString(string str,string spc)
		{
			if(str.Length>spc.Length)
				return str;
			else
				return str+spc.Substring(0,spc.Length-str.Length)+"  ";			
		}
		/// <summary>
		/// This method is not used
		/// </summary>
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5)
		{
			while(rdr.Read())
			{
				if(rdr["product"].ToString().Trim().Length>len1)
					len1=rdr["product"].ToString().Trim().Length;					
				if(IsTank(rdr["store_in"].ToString().Trim()).Length>len2)
					len2=IsTank(rdr["store_in"].ToString().Trim()).Length;					
				if(rdr["pack_type"].ToString().Trim().Length>len3)
					len3=22;
				if(rdr["closing_stock"].ToString().Trim().Length>len4)
					len4=rdr["closing_stock"].ToString().Trim().Length;					
				if(rdr["category"].ToString().Trim().Length>len5)
					len5=rdr["category"].ToString().Trim().Length;					
			}
		}
		/// <summary>
		/// This method is not used
		/// </summary>
		private string GetString(string str,int maxlen,string spc)
		{		
			return str+spc.Substring(0,maxlen>str.Length?maxlen-str.Length:str.Length-maxlen);
		}
		/// <summary>
		/// This method is not used
		/// </summary>
		private string MakeString(int len)
		{
			string spc="";
			for(int x=0;x<len;x++)
				spc+=" ";
			return spc;
		}

		/// <summary>
		/// This method return the tank sort name if available in data base.
		/// </summary>
		protected string IsTank(string str)
		{
			string op="";
			if(Char.IsDigit(str,0))
				dbobj.SelectQuery("select top 1 prod_abbname from tank where tank_id='"+str+"'","prod_abbname",ref op);
			if(op.Length>0)
				return op;
			else
				return str;
		}

		/// <summary>
		/// This is used to trim the date.
		/// </summary>
		private DateTime getdate(string dat,bool to)
		{
			//int dd=mm=yy=0;
			string[] dt=dat.IndexOf("/")>0? dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}

		/// <summary>
		/// method to multiply package qty. with actual qty. called from .aspx
		/// </summary>
		//double count=1;
		//double i=1;
		public double cs=0;
		protected string Multiply(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				//if(count==i)
				cs+=ans;
				Cache["cs"]=System.Convert.ToString(cs);
				//i++;
				//count++;
				//**********************
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//******
					cs+=System.Convert.ToDouble(mystr[0].ToString());
					Cache["cs"]=System.Convert.ToString(cs);
					//********
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}

		/// <summary>
		/// method to check the category of product is fuel or packege is Loose Oil then return only space,,(Called from .aspx);
		/// </summary>
		double count1=1,i1=1;
		public double csp=0;
		protected string Check(string cs, string type,string pack)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				//**********************
				if(count1==i1)
				csp+=System.Convert.ToDouble(cs);
			Cache["csp"]=System.Convert.ToString(csp);
			i1++;
			count1++;
			//**********************
			return cs;
		}
		
		/// <summary>
		/// this method check the product type id Fuel or Loose then return the blank other wise return closing stock.
		/// </summary>
		protected string Check1(string cs, string type,string pack)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				//**********************
				if(count1==i1)
				csp+=System.Convert.ToDouble(cs);
			//Cache["csp"]=System.Convert.ToString(csp);
			i1++;
			count1++;
			//**********************
			return cs;
		}
		
		/// <summary>
		/// Returns the date in MM/DD/YYYY format.
		/// </summary>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0? str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		protected void grdLeg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		
		/// <summary>
		/// This is used to print the file StockReport.txt .
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
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

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\StockReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  Stock Report Printed  userid  "+uid);
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  Stock Report Printed  EXCEPTION   "+ane.Message +"  userid "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  Stock Report Printed  EXCEPTION   "+se.Message +"  userid "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  Stock Report Printed  EXCEPTION   "+es.Message  +"  userid "+uid);
				}

			} 
			catch (Exception es) 
			{						
				CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  Stock Report Printed  EXCEPTION   "+es.Message+"  userid "+uid);
			}

		}

		/// <summary>
		/// Prepares the excel report file StockReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(grdLeg.Visible==true || grdMRP.Visible==true || grdMRPAmount.Visible==true || grdAmount.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:StockReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Stock Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:StockReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Stock Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		double Amount=0;
		/// <summary>
		/// This method return the amount of total remainning product closing stock.
		/// </summary>
		public string GetAmount(string pid,string cs)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=obj.GetRecordSet("select top 1 sal_rate from Price_Updation where Prod_ID='"+pid+"' order by Eff_Date desc");
			if(rdr.Read())
			{
				double amt=((double.Parse(rdr["sal_rate"].ToString())+((double.Parse(rdr["sal_rate"].ToString())*12.5)/100)))*double.Parse(cs);
				Amount+=amt;
				Cache["Amount"]=Amount.ToString();
				return GenUtil.strNumericFormat(amt.ToString());
			}
			else
			{
				return "0";
			}
			//rdr.Close();
		}

		protected void grdMRP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}