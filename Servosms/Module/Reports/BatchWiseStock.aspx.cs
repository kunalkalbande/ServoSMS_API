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
	/// Summary description for BatchWiseStock.
	/// </summary>
	public partial class BatchWiseStock : System.Web.UI.Page
	{
		string uid;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year : Request.Form["txtDateTo"].ToString();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BatchWiseStock.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{	
				try
				{
					grdLeg.Visible=false;
					grdMRP.Visible=false;
					grdMRPAmount.Visible=false;
					grdAmount.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="4";
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
					dbobj.SelectQuery("select distinct Pack_Type from vw_batchstock",ref rdr3);
					while(rdr3.Read())
					{
						DropPackType.Items.Add(rdr3.GetValue(0).ToString());
					}
					DropPackType.Items.Insert(0,"All");
					dbobj.Dispose();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:BatchWiseStock.aspx,Method:page_load EXCEPTION  "+ex.Message+" userid "+ uid);
				}
			}
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
		/// This is used to make sorting the datagrid onclicking of the datagridheader.
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
				CreateLogFiles.ErrorLog("Form:BatchWiseStock.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to binding the datagrid with the help of query.
		/// </summary>
		public void Bindthedata()
		{
            try
            {
                SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                //***********
                grdLeg.Visible = true;
                //System.Data.SqlClient.SqlDataReader rdr=null;
                string sql = "";
                object op = null;
                int flage = 0;
                /****Add by vikas 26.06.09****************/
                dbobj.ExecProc(OprType.Insert, "sp_stock", ref op, "@fromdate", GenUtil.str2DDMMYYYY(txtDateTo.Text));
                /*****end***************/

                //call the procedure and create the temp table. stk1.
                dbobj.ExecProc(OprType.Insert, "sp_batchstock", ref op, "@fromdate", GenUtil.str2DDMMYYYY(txtDateTo.Text));
                if (txtDateTo.Text == System.DateTime.Now.ToShortDateString())
                {
                    if (drpstore.SelectedIndex > 0)
                    {
                        if (drpstore.SelectedItem.Text.IndexOf(":") > 0)
                        {
                            string[] stor = drpstore.SelectedItem.Text.Split(new char[] { ':' }, drpstore.SelectedItem.Text.Length);
                            string tid = "";
                            //** Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
                            if (DropPackType.SelectedItem.Text.Equals("All"))
                                sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + tid + "'";
                            else
                                sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + tid + "' and a.Pack_Type='" + DropPackType.SelectedItem.Text.ToString() + "'";
                        }
                        else
                        {
                            if (DropPackType.SelectedItem.Text.Equals("All"))
                                sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + drpstore.SelectedItem.Text + "'";
                            else
                                sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + drpstore.SelectedItem.Text + "' and a.Pack_Type='" + DropPackType.SelectedItem.Text.ToString() + "'";
                        }
                    }
                    else
                    {
                        //sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
                        if (DropPackType.SelectedItem.Text.Equals("All"))
                            sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
                        else
                            sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='" + DropPackType.SelectedItem.Text.ToString() + "'";
                    }
                }
                else if (drpstore.SelectedIndex > 0)
                {
                    if (drpstore.SelectedItem.Text.IndexOf(":") > 0)
                    {
                        string[] stor = drpstore.SelectedItem.Text.Split(new char[] { ':' }, drpstore.SelectedItem.Text.Length);
                        string tid = "";
                        //** Mahesh dbobj.SelectQuery("select tank_id from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
                        if (DropPackType.SelectedItem.Text.Equals("All"))
                            sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + tid + "'";
                        else
                            sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + tid + "' a.Pack_Type='" + DropPackType.SelectedItem.Text.ToString() + "'";

                    }
                    else
                    {
                        if (DropPackType.SelectedItem.Text.Equals("All"))
                        {
                            //27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
                            sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + drpstore.SelectedItem.Text + "'";
                            flage = 1;
                        }
                        else
                        {
                            //2706.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
                            sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='" + drpstore.SelectedItem.Text + "' and a.Pack_Type='" + DropPackType.SelectedItem.Text.ToString() + "'";
                            flage = 1;
                        }
                    }

                }
                else
                {
                    if (DropPackType.SelectedItem.Text.Equals("All"))
                    {
                        //coment by vikas 26.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
                        //coment by vikas 01.07.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
                        sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
                        flage = 1;
                    }
                    else
                    {
                        // 27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
                        sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='" + DropPackType.SelectedItem.Text.ToString() + "'";
                        flage = 1;
                    }
                    //sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
                    Trace.Write(sql);

                    //sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate";
                }
                /*//27.06.09 	if(chkZeroStock.Checked==false)
                    {*/
                if (flage != 1)
                    sql += " and a.Closing_Stock!=0";
                else
                    sql += " and a.Closing_Stock!=0  group by a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,a.batch_id order by product,batch_id desc";
                //}
                //***********
                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds, "vw_batchstock");
                DataTable dtcustomer = ds.Tables["vw_batchstock"];
                DataView dv = new DataView(dtcustomer);
                dv.Sort = strorderby;
                Cache["strorderby"] = strorderby;
                //27.06.09 if(chkAll.Checked)
                //27.06.09grdMRPAmount.DataSource=dv;
                //27.06.09else if(chkMRP.Checked)
                //27.06.09	grdMRP.DataSource=dv;
                //27.06.09else if(chkAmount.Checked)
                //27.06.09	grdAmount.DataSource=dv;
                //27.06.09else
                grdLeg.DataSource = dv;
                if (dv.Count != 0)
                {
                    //start27.06.09 				if(chkAll.Checked)
                    //				{
                    //					grdMRPAmount.DataBind();
                    //					grdMRPAmount.Visible=true;
                    //					grdLeg.Visible=false;
                    //					grdMRP.Visible=false;
                    //					grdAmount.Visible=false;
                    //				}
                    //				else if(chkMRP.Checked)
                    //				{
                    //					grdMRP.DataBind();
                    //					grdMRP.Visible=true;
                    //					grdMRPAmount.Visible=false;
                    //					grdLeg.Visible=false;
                    //					grdAmount.Visible=false;
                    //				}
                    //				else if(chkAmount.Checked)
                    //				{
                    //					grdAmount.DataBind();
                    //					grdAmount.Visible=true;
                    //					grdLeg.Visible=false;
                    //					grdMRP.Visible=false;
                    //					grdMRPAmount.Visible=false;
                    //				}
                    //				else 
                    //end27.06.09 				{
                    grdLeg.DataBind();
                    grdAmount.Visible = false;
                    grdLeg.Visible = true;
                    grdMRP.Visible = false;
                    grdMRPAmount.Visible = false;
                    //start 27.06.09 				}
                    //			}
                    //			else
                    //			{
                    //				grdLeg.Visible=false;
                    //				grdMRP.Visible=false;
                    //				grdMRPAmount.Visible=false;
                    //				grdAmount.Visible=false;
                    //				MessageBox.Show("Data Not Available");
                    //end 27.06.09 			}
                    sqlcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:BatchWiseStock.aspx,Method:Bindthedata" + "EXCEPTION  " + ex.Message + uid);
            }
        }

		/// <summary>
		/// This method is used to view the report with the help of Bindthedata() function and also set the session variable.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
            try
            {
                strorderby = "Prod_Code ASC";
                Session["Column"] = "Prod_Code";
                Session["order"] = "ASC";
                Bindthedata();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:BatchStokReport,Class:DBoperation_LETEST + Method:cmdrpt_Click  Batch WiseStock Report Viewed   userid  " + uid);
            }            
		}

		/// <summary>
		/// This method is used to prepare the report file .txt
		/// </summary>
		public void makingReport()
		{
					
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="";
			string info="",infoAll="",infoAmount="",infoMRP="";
			int flage=0;
			object op= null;
			dbobj.ExecProc(OprType.Insert,"sp_batchstock",ref op,"@fromdate",GenUtil.str2DDMMYYYY(txtDateTo.Text));
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
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					}
					else 
					{
						if(DropPackType.SelectedItem.Text.Equals("All"))
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					}
				}
				else
				{
					//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
					if(DropPackType.SelectedItem.Text.Equals("All"))
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";

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
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"'";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
				}
				else 
				{
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						flage=1;
					}
					else
					{
						//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						flage=1;
					}
				}
			}
			else 
			{
				//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
				if(DropPackType.SelectedItem.Text.Equals("All"))
				{
					//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
					sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
					flage=1;
				}
				else
				{
					//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					flage=1;
				}
				Trace.Write(sql);
			}
			//27.06.09 if(chkZeroStock.Checked==false)
				//27.06.09 sql+=" and a.Closing_Stock!=0";
			if(flage!=1)
				sql+=" and a.Closing_Stock!=0";
			else
				sql+=" and a.Closing_Stock!=0 order by product,batch_id desc";

			//27.06.09 sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\BatchWiseStock.txt";
			//string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\BatchWiseStock.xls";
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
			/*//27.06.09 if(chkAll.Checked)
				des="---------------------------------------------------------------------------------------------------------";
			else if(chkMRP.Checked)
				des="--------------------------------------------------------------------------------------------";
			else if(chkAmount.Checked)
				des="-------------------------------------------------------------------------------------------------";
			else*/
				des="----------------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("===============================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("BATCH WISE STOCK REPORT AS ON "+ txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("===============================",des.Length));
			sw.WriteLine("Date : "+txtDateTo.Text);
			sw.WriteLine("Location : "+drpstore.SelectedItem.Text);
			sw.WriteLine("Package Type : "+DropPackType.SelectedItem.Text);
			/*//27.06.09 if(chkAll.Checked)
			{
				sw.WriteLine("+---------+------------------------------+---------------+-------------------------+-------+------------+");
				sw.WriteLine("| Product |                              |               |      Closing Stock      |       |            |");
				sw.WriteLine("|  Code   |           Product            |    Batch No   |-------------+-----------|  MRP  |   Amount   |");
				sw.WriteLine("|         |                              |               |    Pkg      |  Lt/kg    |       |            |");
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------+------------+");
				//             123456789 123456789012345678901234567890 123456789012345 1234567890123 12345678901 1234567 12345678901
			}
			else if(chkMRP.Checked)
			{
				sw.WriteLine("+---------+------------------------------+---------------+-------------------------+-------+");
				sw.WriteLine("| Product |                              |               |      Closing Stock      |       |");
				sw.WriteLine("|  Code   |           Product            |    Batch No   |-------------+-----------|  MRP  |");
				sw.WriteLine("|         |                              |               |    Pkg      |  Lt/kg    |       |");
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------+");
				//             123456789 123456789012345678901234567890 123456789012345 1234567890123 12345678901 1234567
			}
			else if(chkAmount.Checked)
			{
				sw.WriteLine("+---------+------------------------------+---------------+-------------------------+------------+");
				sw.WriteLine("| Product |                              |               |      Closing Stock      |            |");
				sw.WriteLine("|  Code   |           Product            |    Batch No   |-------------+-----------|   Amount   |");
				sw.WriteLine("|         |                              |               |    Pkg      |  Lt/kg    |            |");
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+------------+");
				//             123456789 123456789012345678901234567890 123456789012345 1234567890123 12345678901 123456789012
			}
			else
			{*/
				sw.WriteLine("+---------+------------------------------+---------------+-------------------------+-------------------------+-------------------------+");
				sw.WriteLine("| Product |                              |               |  Batch wise Clo.Stock   |   Book wise Clo.Stock   |    Diff. in Clo.Stock   |");
				sw.WriteLine("|  Code   |           Product            |    Batch No   |-------------+-----------|-------------+-----------|-------------+-----------|");
				sw.WriteLine("|         |                              |               |    Pkg      |  Lt/kg    |    Pkg      |  Lt/kg    |    Pkg      |  Lt/kg    |");
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------------+-----------+-------------+-----------+");
				//             123456789 123456789012345678901234567890 123456789012345 1234567890123 12345678901 1234567890123 12345678901 1234567890123 12345678901
			//}
			string pack;
			string strPackCl;
			string[] strSplit;
			if(rdr.HasRows)
			{
				// info : set format of the string to display.
				info = " {0,-9:S} {1,-30:S} {2,-15:S} {3,13:S} {4,11:F} {5,13:S} {6,11:F} {7,13:S} {8,11:F}";
				//27.06.09 info = " {0,-9:S} {1,-30:S} {2,-15:S} {3,13:S} {4,11:F} ";
				infoAmount = " {0,-9:S} {1,-30:S} {2,-15:S} {3,13:S} {4,11:F} {5,12:F}";
				infoMRP = " {0,-9:S} {1,-30:S} {2,-15:S} {3,13:S} {4,11:F} {5,7:F}";
				infoAll = " {0,-9:S} {1,-30:S} {2,-15:S} {3,13:S} {4,11:F} {5,7:F} {6,12:F}";
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
						sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),rdr["Batch_No"].ToString().Trim(),"",rdr["closing_stock"].ToString(),rdr["MRP"].ToString()); 
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
						/*//27.06.09 if(chkAll.Checked)
							sw.WriteLine(infoAll,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),rdr["Batch_No"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl,rdr["MRP"].ToString(),GetAmount(rdr["Prod_ID"].ToString(),rdr["closing_stock"].ToString()));
						else if(chkAmount.Checked)
							sw.WriteLine(infoAmount,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),rdr["Batch_No"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl,GetAmount(rdr["Prod_ID"].ToString(),rdr["closing_stock"].ToString())); 
						else if(chkMRP.Checked)
							sw.WriteLine(infoMRP,rdr["Prod_Code"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),rdr["Batch_No"].ToString().Trim(),rdr["closing_stock"].ToString(),strPackCl,rdr["MRP"].ToString());
						else*/
							sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),
								GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),
								//27.06.09 rdr["Batch_No"].ToString().Trim(),
								batch_name(rdr["Batch_id"].ToString().Trim()),
								rdr["closing_stock"].ToString(),
								strPackCl,
								Convert.ToString(Check_Book_P(rdr["closing_stock"].ToString(),rdr["Category"].ToString(),rdr["pack_type"].ToString(),rdr["prod_id"].ToString(),rdr["Batch_id"].ToString())),
								Convert.ToString(Multiply_Book_P(Cache["clkp"].ToString()+"X"+rdr["pack_type"].ToString())),
								Check_Diff_p(rdr["closing_stock"].ToString(),rdr["Category"].ToString(),rdr["pack_type"].ToString(),rdr["prod_id"].ToString(),rdr["Batch_id"].ToString()),
								Multiply_Diff_p(Cache["clkp1"].ToString()+"X"+rdr["pack_type"].ToString())
								);
					}
				}
			}
			/*//27.06.09 if(chkAll.Checked)
			{
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------+------------+");
				sw.WriteLine(infoAll,"Total:","","",Cache["csp"],Cache["cs"],"",GenUtil.strNumericFormat(Cache["Amount"].ToString()));
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------+------------+");
			}
			else if(chkAmount.Checked)
			{
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+------------+");
				sw.WriteLine(infoAmount,"Total:","","",Cache["csp"],Cache["cs"],GenUtil.strNumericFormat(Cache["Amount"].ToString()));
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+------------+");
			}
			else if(chkMRP.Checked)
			{
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------+");
				sw.WriteLine(infoMRP,"Total:","","",Cache["csp"],Cache["cs"],"");
				sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+-------+");
			}
			else
			{*/
				//27.06.09 sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+");
				//27.06.09 sw.WriteLine(info,"Total:","","",Cache["csp"],Cache["cs"]);
				//27.06.09 sw.WriteLine("+---------+------------------------------+---------------+-------------+-----------+");
		//	}
			dbobj.Dispose();
			rdr.Close();
			sw.Close(); 
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\BatchWiseStock.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			object op= null;
			int flage=0;
			dbobj.ExecProc(OprType.Insert,"sp_batchstock",ref op,"@fromdate",GenUtil.str2DDMMYYYY(txtDateTo.Text));
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
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					}
					else 
					{
						if(DropPackType.SelectedItem.Text.Equals("All"))
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						else
							sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					}
				}
				else
				{
					//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
					if(DropPackType.SelectedItem.Text.Equals("All"))
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
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
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"'";
					else
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ tid  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
				}
				else 
				{
					if(DropPackType.SelectedItem.Text.Equals("All"))
					{
						//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"'";
						flage=1;
					}
					else
					{
						//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and Store_in='"+ drpstore.SelectedItem.Text  +"' and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
						flage=1;
					}
				}
			}
			else 
			{
				//sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate order by a.store_in";
				if(DropPackType.SelectedItem.Text.Equals("All"))
				{
					//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
					sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id";
					flage=1;
				}
				else
				{
					//27.06.09 sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_no from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					sql = "select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.Prod_id,a.batch_id from vw_batchstock a , stk1 b where a.product=b.product and a.stock_date=b.sdate and a.batch_id=b.batch_id and a.Pack_Type='"+DropPackType.SelectedItem.Text.ToString()+"'";
					flage=1;
				}
				Trace.Write(sql);
			}
		//27.06.09 	if(chkZeroStock.Checked==false)
				//27.06.09 sql+=" and a.Closing_Stock!=0";
			
			if(flage!=1)
				sql+=" and a.Closing_Stock!=0";
			else
				sql+=" and a.Closing_Stock!=0 order by product,batch_id desc";

			//27.06.09 sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			sw.WriteLine("Date\t"+txtDateTo.Text);
			sw.WriteLine("Location\t"+drpstore.SelectedItem.Text);
			sw.WriteLine("Package Type\t"+DropPackType.SelectedItem.Text);
			sw.WriteLine();
			//27.06.09 if(chkAll.Checked)
			//27.06.09 	sw.WriteLine("Product Code\tProduct Name\tBatch_No\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)\tMRP\tAmount");
			//27.06.09 else if(chkMRP.Checked)
			//27.06.09 	sw.WriteLine("Product Code\tProduct Name\tBatch_No\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)\tMRP");
			//27.06.09 else if(chkAmount.Checked)
			//27.06.09 	sw.WriteLine("Product Code\tProduct Name\tBatch_No\tClosing Stock(Pkg)\tClosing Stock(Ler/kg)\tAmount");
			//27.06.09 else
			 	sw.WriteLine("Product Code\tProduct Name\tBatch_No\tClosing Stock(Pkg)\tBatch Wise Closing Stock(Ler/kg)\tBook Wise Closing Stock(Ler/kg)\t Diff. in Closing Stock(Ler/kg)");
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
						sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["Batch_No"].ToString().Trim()+"\t"+""+"\t"+rdr["closing_stock"].ToString()+"\t"+rdr["MRP"].ToString()); 
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
						//27.06.09 if(chkAll.Checked)
						//27.06.09 	sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["Batch_No"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl+"\t"+rdr["MRP"].ToString()+"\t"+GetAmount(rdr["Prod_ID"].ToString(),rdr["Closing_Stock"].ToString())); 
						//27.06.09 else if(chkMRP.Checked)
						//27.06.09 		sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["Batch_No"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl+"\t"+rdr["MRP"].ToString()); 
						//27.06.09 	else if(chkAmount.Checked)
						//27.06.09 		sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["Batch_No"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl+"\t"+GetAmount(rdr["Prod_ID"].ToString(),rdr["Closing_Stock"].ToString())); 
						//27.06.09 	else
							//27.06.09 sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+rdr["Product"].ToString().Trim()+"\t"+rdr["Batch_No"].ToString().Trim()+"\t"+rdr["closing_stock"].ToString()+"\t"+strPackCl); 
						sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+
							rdr["Product"].ToString().Trim()+"\t"+
							batch_name(rdr["Batch_id"].ToString().Trim())+"\t"+
							rdr["closing_stock"].ToString()+"\t"+
							strPackCl+"\t"+
							Convert.ToString(Check_Book_xl(rdr["closing_stock"].ToString(),rdr["Category"].ToString(),rdr["pack_type"].ToString(),rdr["prod_id"].ToString(),rdr["Batch_id"].ToString()))+"\t"+
								Convert.ToString(Multiply_Book_xl(Cache["clk_xl"].ToString()+"X"+rdr["pack_type"].ToString()))+"\t"+
								Check_Diff_xl(rdr["closing_stock"].ToString(),rdr["Category"].ToString(),rdr["pack_type"].ToString(),rdr["prod_id"].ToString(),rdr["Batch_id"].ToString())+"\t"+
								Multiply_Diff_xl(Cache["clkxl1"].ToString()+"X"+rdr["pack_type"].ToString())); 
					}
				}
			}
			//27.06.09 if(chkAll.Checked)
			//27.06.09 	sw.WriteLine("Total\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]+"\t\t"+GenUtil.strNumericFormat(Cache["Amount"].ToString()));
			//27.06.09 else if(chkMRP.Checked)
			//27.06.09 	sw.WriteLine("Total\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]);
			//27.06.09 else if(chkAmount.Checked)
			//27.06.09 	sw.WriteLine("Total\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]+"\t"+GenUtil.strNumericFormat(Cache["Amount"].ToString()));
			//27.06.09 else
			//27.06.09 	sw.WriteLine("Total\t\t\t"+Cache["csp"]+"\t"+Cache["cs"]);
			dbobj.Dispose();
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method is not used.
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

		private DateTime getdate(string dat,bool to)
		{
			//int dd=mm=yy=0;
			string[] dt=dat.IndexOf("/")>0?dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}

		public string batch_name(string batch_id)
		{
			string op="With Out Batch";
			InventoryClass obj=new InventoryClass();
			SqlDataReader dtr=null;
			string sql="select batch_no from batchno where batch_id="+batch_id.ToString();
			dtr=obj.GetRecordSet(sql);
			if(dtr.Read())
			{
				op=dtr.GetValue(0).ToString();
			}
			dtr.Close();
			return op;
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


		public double cs1=0;
		protected string Multiply_Book(string str)
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
				cs1+=ans;
				Cache["cs1"]=System.Convert.ToString(cs1);
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					cs1+=System.Convert.ToDouble(mystr[0].ToString());
					Cache["cs1"]=System.Convert.ToString(cs1);
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}


		public double cs_p=0;
		protected string Multiply_Book_P(string str)
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
				cs_p+=ans;
				Cache["cs_p"]=System.Convert.ToString(cs_p);
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					cs_p+=System.Convert.ToDouble(mystr[0].ToString());
					Cache["cs_p"]=System.Convert.ToString(cs_p);
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}

		public double cs_xl=0;
		protected string Multiply_Book_xl(string str)
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
				cs_xl+=ans;
				Cache["cs_xl"]=System.Convert.ToString(cs_xl);
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					cs_xl+=System.Convert.ToDouble(mystr[0].ToString());
					Cache["cs_xl"]=System.Convert.ToString(cs_xl);
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}


		public double cs2=0;
		protected string Multiply_Diff(string str)
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
				cs2+=ans;
				//27.06.09 Cache["cs2"]=System.Convert.ToString(cs1);
				
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					
					cs2+=System.Convert.ToDouble(mystr[0].ToString());
					//27.06.09 Cache["cs2"]=System.Convert.ToString(cs1);
					
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}

		public double cs_p2=0;
		protected string Multiply_Diff_p(string str)
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
				cs_p2+=ans;
				//27.06.09 Cache["cs2"]=System.Convert.ToString(cs1);
				
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					
					cs_p2+=System.Convert.ToDouble(mystr[0].ToString());
					//27.06.09 Cache["cs2"]=System.Convert.ToString(cs1);
					
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}

		public double cs_xl2=0;
		protected string Multiply_Diff_xl(string str)
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
				cs_xl2+=ans;
				//27.06.09 Cache["cs2"]=System.Convert.ToString(cs1);
				
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					
					cs_xl2+=System.Convert.ToDouble(mystr[0].ToString());
					//27.06.09 Cache["cs2"]=System.Convert.ToString(cs1);
					
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}


		public double Diff_Tot(double obj1,double obj2)
		{
			double tot=obj2-obj1;

			return tot;
			
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

		double count2=1,i2=1;
		public double csp1=0,tot_cs=0;
		protected string Check_Book(string cs, string type,string pack,string Prod_id, string Batch_id)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count2==i2)
				{
					csp1+=System.Convert.ToDouble(cs);
					tot_cs+=System.Convert.ToDouble(cs);
				}
			//Cache["csp1"]=System.Convert.ToString(csp1);
			i2++;
			count2++;
			//**********************
			if(Batch_id=="0")
			{
				double clstk=0;
				string sql="select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.prod_id="+Prod_id;
				SqlDataReader rdr=null;
				InventoryClass obj=new InventoryClass();
				double dif_in_stk=0;
				rdr=obj.GetRecordSet(sql);
				if(rdr.Read())
				{
					clstk+=Convert.ToDouble(rdr.GetValue(4).ToString());
				}
				rdr.Close();
			
				dif_in_stk=clstk-tot_cs;
				csp1+=dif_in_stk;
				cs=Convert.ToString(Convert.ToDouble(cs.ToString())+dif_in_stk);
				tot_cs=0;
				Cache["clk"]=cs;
				
			}
			Cache["clk"]=cs;
			//csp1+=System.Convert.ToDouble(cs);
			Cache["csp1"]=System.Convert.ToString(csp1);
			return cs;
			//return Convert.ToString(dif_in_stk);
		}
 
		double countpr=1,ipr=1;
		public double csppr=0,tot_cspr=0;
		protected string Check_Book_P(string cs, string type,string pack,string Prod_id, string Batch_id)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(countpr==ipr)
			{
				csppr+=System.Convert.ToDouble(cs);
				tot_cspr+=System.Convert.ToDouble(cs);
			}
			//Cache["csp1"]=System.Convert.ToString(csp1);
			ipr++;
			countpr++;
			//**********************
			if(Batch_id=="0")
			{
				double clstk=0;
				string sql="select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.prod_id="+Prod_id;
				SqlDataReader rdr=null;
				InventoryClass obj=new InventoryClass();
				double dif_in_stk=0;
				rdr=obj.GetRecordSet(sql);
				if(rdr.Read())
				{
					clstk+=Convert.ToDouble(rdr.GetValue(4).ToString());
				}
				rdr.Close();
			
				dif_in_stk=clstk-tot_cspr;
				csppr+=dif_in_stk;
				cs=Convert.ToString(Convert.ToDouble(cs.ToString())+dif_in_stk);
				tot_cspr=0;
				Cache["clkp"]=cs;
				
			}
			Cache["clkp"]=cs;
			//csp1+=System.Convert.ToDouble(cs);
			Cache["cspp"]=System.Convert.ToString(csppr);
			return cs;
			//return Convert.ToString(dif_in_stk);
		}


		double count_xl=1,i_xl=1;
		public double csp_xl=0,tot_cs_xl=0;
		protected string Check_Book_xl(string cs, string type,string pack,string Prod_id, string Batch_id)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(countpr==ipr)
			{
				csp_xl+=System.Convert.ToDouble(cs);
				tot_cs_xl+=System.Convert.ToDouble(cs);
			}
			//Cache["csp1"]=System.Convert.ToString(csp1);
			i_xl++;
			count_xl++;
			//**********************
			if(Batch_id=="0")
			{
				double clstk=0;
				string sql="select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.prod_id="+Prod_id;
				SqlDataReader rdr=null;
				InventoryClass obj=new InventoryClass();
				double dif_in_stk=0;
				rdr=obj.GetRecordSet(sql);
				if(rdr.Read())
				{
					clstk+=Convert.ToDouble(rdr.GetValue(4).ToString());
				}
				rdr.Close();
			
				dif_in_stk=clstk-tot_cs_xl;
				csp_xl+=dif_in_stk;
				cs=Convert.ToString(Convert.ToDouble(cs.ToString())+dif_in_stk);
				tot_cs_xl=0;
				Cache["clk_xl"]=cs;
				
			}
			Cache["clk_xl"]=cs;
			//csp1+=System.Convert.ToDouble(cs);
			Cache["csp_xl"]=System.Convert.ToString(csp_xl);
			return cs;
			//return Convert.ToString(dif_in_stk);
		}



		double count3=1,i3=1,count4=1,i4=1;
		public double csp2=0,csp3=0,tot_cs1=0;
		protected string Check_Diff(string cs, string type,string pack,string Prod_id, string Batch_id)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count3==i3)
					csp2+=System.Convert.ToDouble(cs);
			i3++;
			count3++;
			
			//********Add by vikas 27.06.09**************
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count4==i4)
				csp3+=System.Convert.ToDouble(cs);
			tot_cs1+=System.Convert.ToDouble(cs);
			Cache["csp2"]=System.Convert.ToString(csp3);
			i4++;
			count4++;

			cs=Convert.ToString(csp2-csp3);
			Cache["clk1"]=cs;
			if(Batch_id=="0")
			{
				double clstk=0;
				string sql="select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.prod_id="+Prod_id;
				SqlDataReader rdr=null;
				InventoryClass obj=new InventoryClass();
				double dif_in_stk=0;
				rdr=obj.GetRecordSet(sql);
				if(rdr.Read())
				{
					clstk+=Convert.ToDouble(rdr.GetValue(4).ToString());
				}
				rdr.Close();
			
				dif_in_stk=clstk-tot_cs1;
				cs=dif_in_stk.ToString();
				//cs=Convert.ToString(Convert.ToDouble(cs.ToString())+dif_in_stk);
				tot_cs1=0;
				Cache["clk1"]=cs;
			}
			//Cache["clk1"]=cs;
			//Cache["csp2"]=System.Convert.ToString(csp3);
			//**********************
			
			return cs;
		}

		double count_p3=1,i_p3=1,count_p4=1,i_p4=1;
		public double csp_p2=0,csp_p3=0,tot_cs_p1=0;
		protected string Check_Diff_p(string cs, string type,string pack,string Prod_id, string Batch_id)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count_p3==i_p3)
				csp_p2+=System.Convert.ToDouble(cs);
			i_p3++;
			count_p3++;
			
			//********Add by vikas 27.06.09**************
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count_p4==i_p4)
				csp_p3+=System.Convert.ToDouble(cs);
			tot_cs_p1+=System.Convert.ToDouble(cs);
			Cache["cspp2"]=System.Convert.ToString(csp_p3);
			i_p4++;
			count_p4++;

			cs=Convert.ToString(csp_p2-csp_p3);
			Cache["clkp1"]=cs;
			if(Batch_id=="0")
			{
				double clstk=0;
				string sql="select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.prod_id="+Prod_id;
				SqlDataReader rdr=null;
				InventoryClass obj=new InventoryClass();
				double dif_in_stk=0;
				rdr=obj.GetRecordSet(sql);
				if(rdr.Read())
				{
					clstk+=Convert.ToDouble(rdr.GetValue(4).ToString());
				}
				rdr.Close();
			
				dif_in_stk=clstk-tot_cs_p1;
				cs=dif_in_stk.ToString();
				//cs=Convert.ToString(Convert.ToDouble(cs.ToString())+dif_in_stk);
				tot_cs_p1=0;
				Cache["clkp1"]=cs;
			}
						
			return cs;
		}


		double count_xl3=1,i_xl3=1,count_xl4=1,i_xl4=1;
		public double csp_xl2=0,csp_xl3=0,tot_cs_xl1=0;
		protected string Check_Diff_xl(string cs, string type,string pack,string Prod_id, string Batch_id)
		{
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count_xl3==i_xl3)
				csp_xl2+=System.Convert.ToDouble(cs);
			i_xl3++;
			count_xl3++;
			
			//********Add by vikas 27.06.09**************
			if(type.ToUpper().Equals("FUEL")|| pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				if(count_xl4==i_xl4)
				csp_xl3+=System.Convert.ToDouble(cs);
			tot_cs_xl1+=System.Convert.ToDouble(cs);
			Cache["cspp2"]=System.Convert.ToString(csp_xl3);
			i_xl4++;
			count_xl4++;

			cs=Convert.ToString(csp_xl2-csp_xl3);
			Cache["clkxl1"]=cs;
			if(Batch_id=="0")
			{
				double clstk=0;
				string sql="select a.stock_date,a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate and a.prod_id="+Prod_id;
				SqlDataReader rdr=null;
				InventoryClass obj=new InventoryClass();
				double dif_in_stk=0;
				rdr=obj.GetRecordSet(sql);
				if(rdr.Read())
				{
					clstk+=Convert.ToDouble(rdr.GetValue(4).ToString());
				}
				rdr.Close();
			
				dif_in_stk=clstk-tot_cs_xl1;
				cs=dif_in_stk.ToString();
				//cs=Convert.ToString(Convert.ToDouble(cs.ToString())+dif_in_stk);
				tot_cs_xl1=0;
				Cache["clkxl1"]=cs;
			}
						
			return cs;
		}


		/// <summary>
		/// This method is used to check the type of products if fuel then return blank otherwise return closing stock.
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
		/// <param name="str"></param>
		/// <returns></returns>
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
		/// Prepares the report file BatchWiseStock.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\BatchWiseStock.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:BatchWiseStok,Class:DBoperation_LETEST + Method:btnPrint_Click,  BatchWiseStok Report Printed  userid  "+uid);
				}
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  BatchWiseStock Report Printed  EXCEPTION   "+ane.Message +"  userid "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  BatchWiseStock Report Printed  EXCEPTION   "+se.Message +"  userid "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  BatchWiseStock Report Printed  EXCEPTION   "+es.Message  +"  userid "+uid);
				}

			} 
			catch (Exception es) 
			{						
				CreateLogFiles.ErrorLog("Form:StokReport,Class:DBoperation_LETEST + Method:btnPrint_Click,  BatchWiseStock Report Printed  EXCEPTION   "+es.Message+"  userid "+uid);
			}

		}

		/// <summary>
		/// This method is used to return the sales rate of the given product id
		/// </summary>
		double Amount=0;
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

		/// <summary>
		/// Prepares the excel report file BatchWiseStock.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(grdLeg.Visible==true || grdMRP.Visible==true || grdMRPAmount.Visible==true || grdAmount.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:BatchWiseStock.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    BatchWiseStock Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:bATCHwISEStock.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    BatchWiseStock Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}