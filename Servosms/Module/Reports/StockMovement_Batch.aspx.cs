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
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using DBOperations;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
//using System.Security.Permissions;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for StockView.
	/// </summary>
	public partial class StockMovement_Batch : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		ArrayList custAccount = new ArrayList();

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
				CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				try
				{
					grdLeg.Visible=false;
					gridSJ.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="46";
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
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
					// Fetch store_in locations from products tables, if store_in not available then get the tank.
					dbobj.SelectQuery("select distinct store_in from products",ref rdr);
					while(rdr.Read())
					{
						if(Char.IsDigit(rdr["store_in"].ToString(),0))
						{
							dbobj.SelectQuery("select tank_id,tank_name,prod_name from tank where tank_id="+rdr["store_in"].ToString(),ref rdr1);
							if(rdr1.Read())
							{
							
								drpstore.Items.Add(rdr1["tank_name"].ToString()+":"+rdr1["prod_name"].ToString());					
							}
						}
						else
							drpstore.Items.Add(rdr["store_in"].ToString());					
					}

			
					drpstore.Items.Insert(0,"All");
					dbobj.Dispose();
					//*********************
					SqlDataReader rdr2=null;
					dbobj.SelectQuery("select distinct category from products",ref rdr2);
					while(rdr2.Read())
					{
						dropcategory.Items.Add(rdr2.GetValue(0).ToString());
					}
					dropcategory.Items.Insert(0,"All");
					dbobj.Dispose();
					
					//********************
					SqlDataReader rdr3=null;
					dbobj.SelectQuery("select distinct Pack_Type from products",ref rdr3);
					while(rdr3.Read())
					{
						dropPackType.Items.Add(rdr3.GetValue(0).ToString());
					}
					dropPackType.Items.Insert(0,"All");
					dbobj.Dispose();
					//*********************

					//Coment by vikas 02.07.09 object ob=null;
					//Coment by vikas 02.07.09 dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertStockMaster",ref ob,"@ProductID","");
					//coment by vikas 06.07.09 Stock_Movement();
					CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Class:DBOperation_LETEST.cs,Method:page_load(). User_ID: "+uid);	
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Class:DBOperation_LETEST.cs,Method:page_load().  EXCEPTION: "+ ex.Message+" User_ID: "+uid);	
				}
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		public void Stock_Movement()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader SqlDtr = null,rdr1 = null;

				string[] strName = new string[2];
			
				//if(drpProductName.SelectedItem.Text.IndexOf(":")>0)
				if(dropPackType.SelectedItem.Text.IndexOf(":")>0)
					strName = dropPackType.SelectedItem.Text.Split(new char[] {':'},dropPackType.SelectedItem.Text.Length);
				else
					strName[0]=dropPackType.SelectedItem.Text;

				string Op_st_No="",Op_st_Ltr="",Cl_st_No="",Cl_st_Ltr="",Bat_ID="";
				int x=0;
				double Bal_No=0;
				dbobj.Insert_or_Update("truncate table tempStockmaster_Batch",ref x);
				SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch sb,products p where sb.productid=p.prod_id order by batch_id");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						Op_st_No="";
						Bal_No=0;
						Bat_ID="";

						 SqlDtr = obj1.GetRecordSet("select top 1 opening_stock,batch_id from stockmaster_batch where cast(floor(cast(stock_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and batch_id='"+rdr["batch_id"].ToString()+"' and productid='"+rdr["productid"].ToString()+"' order by Stock_Date");
						//temp SqlDtr = obj1.GetRecordSet("select top 1 opening_stock,batch_id from stockmaster_batch where cast(floor(cast(stock_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and batch_id='"+rdr["batch_id"].ToString()+"' and productid='"+rdr["productid"].ToString()+"' order by Stock_Date");
						if(SqlDtr.Read())
						{
							Op_st_No=SqlDtr.GetValue(0).ToString();
							Bal_No=double.Parse(SqlDtr.GetValue(0).ToString());
							Bat_ID=SqlDtr.GetValue(1).ToString();
							
							dbobj.Insert_or_Update("insert into tempStockmaster_Batch values('"+rdr["ProductID"].ToString()+"','"+Bat_ID+"','"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"','"+Op_st_No+"','0','0','"+Op_st_No+"','0','0')",ref x); //add by vikas 06.07.09
						}
						else
						{
							dbobj.SelectQuery("select top 1 closing_stock,batch_id from stockmaster_batch where productID = '"+rdr["ProductID"].ToString()+"' and cast(floor(cast(stock_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and batch_id='"+rdr["batch_id"].ToString()+"' and productid='"+rdr["productid"].ToString()+"' order by stock_date desc",ref rdr1);
							if(rdr1.Read())
							{
								Op_st_No=rdr1.GetValue(0).ToString();
								Bal_No=double.Parse(rdr1.GetValue(0).ToString());
								Bat_ID=rdr1.GetValue(1).ToString();

								dbobj.Insert_or_Update("insert into tempStockmaster_Batch values('"+rdr["ProductID"].ToString()+"','"+Bat_ID+"','"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"','"+Op_st_No+"','0','0','"+Op_st_No+"','0','0')",ref x); //add by vikas 06.07.09
							}
							rdr1.Close();
						}
						SqlDtr.Close();
						//Op_st_Ltr=GenUtil.changeqtyltr(strName[1].ToString(),int.Parse(Op_st_No));
						//02.07.09 Bal_Ltr=double.Parse(Op_st_Ltr.ToString());
						//06.07.09 dbobj.Insert_or_Update("insert into tempStockmaster_Batch values('"+rdr["ProductID"].ToString()+"','"+Bat_ID+"','"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"','"+Op_st_No+"','0','0','"+Op_st_No+"','0','0')",ref x);
									
					
						//****02.07.09***************************
						SqlDtr = obj1.GetRecordSet("select qty,trans_ID,batch_id,trans_date from batch_transaction where prod_ID = '"+rdr["ProductID"].ToString()+"' and cast(floor(cast(trans_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(trans_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and batch_id='"+rdr["batch_id"].ToString()+"' and trans_type='Purchase Invoice'");
						if(SqlDtr.HasRows)
						{
							while(SqlDtr.Read())
							{
								Bal_No+=double.Parse(SqlDtr.GetValue(0).ToString());
								//04.07.09 string Qty_Ltr=GenUtil.changeqtyltr(strName[1].ToString(),int.Parse(SqlDtr["Qty"].ToString()));
								//04.07.09 Bal_Ltr+=double.Parse(Qty_Ltr);
								//dbobj.Insert_or_Update("insert into StockLedger_Batch values('Purchase Invoice','"+SqlDtr["Trans_ID"].ToString()+"','"+SqlDtr["Trans_Date"].ToString()+"','"+SqlDtr["Batch_ID"].ToString()+"','"+SqlDtr["Qty"].ToString()+"','"+Qty_Ltr+"',0,0,'"+Bal_No.ToString()+"','"+Bal_Ltr.ToString()+"')",ref x);
								dbobj.Insert_or_Update("insert into tempStockmaster_Batch values('"+rdr["ProductID"].ToString()+"','"+SqlDtr["Batch_ID"].ToString()+"','"+SqlDtr["Trans_Date"].ToString()+"','0','"+SqlDtr["Qty"].ToString()+"',0,'"+Bal_No.ToString()+"','0','0')",ref x);
							
							}
						}
						SqlDtr.Close();
						//*******************************
						SqlDtr = obj1.GetRecordSet("select qty,trans_ID,batch_id,trans_date from batch_transaction where prod_ID = '"+rdr["ProductID"].ToString()+"' and cast(floor(cast(trans_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(trans_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and batch_id='"+rdr["batch_id"].ToString()+"' and trans_type='Sales Invoice'");
						if(SqlDtr.HasRows)
						{
							while(SqlDtr.Read())
							{
								
								Bal_No-=double.Parse(SqlDtr.GetValue(0).ToString());
								//04.07.09 string Qty_Ltr=GenUtil.changeqtyltr(strName[1].ToString(),int.Parse(SqlDtr["Qty"].ToString()));
								//04.07.09 Bal_Ltr-=double.Parse(Qty_Ltr);
								//dbobj.Insert_or_Update("insert into StockLedger_Batch values('Sales Invoice','"+SqlDtr["Trans_ID"].ToString()+"','"+SqlDtr["Trans_Date"].ToString()+"','"+SqlDtr["Batch_ID"].ToString()+"',0,0,'"+SqlDtr["Qty"].ToString()+"','"+Qty_Ltr+"','"+Bal_No.ToString()+"','"+Bal_Ltr.ToString()+"')",ref x);
								dbobj.Insert_or_Update("insert into tempStockmaster_Batch values('"+rdr["ProductID"].ToString()+"','"+SqlDtr["Batch_ID"].ToString()+"','"+SqlDtr["Trans_Date"].ToString()+"','0',0,'"+SqlDtr["Qty"].ToString()+"','"+Bal_No.ToString()+"','0','0')",ref x);
							}
						}
						SqlDtr.Close();
					
						/***02.07.09**********************************
						SqlDtr = obj1.GetRecordSet("select top 1 closing_stock,batch_id from stockmaster_batch where productID = '"+rdr["ProductID"].ToString()+"' and cast(floor(cast(stock_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and batch_id='"+rdr["batch_id"].ToString()+"' order by stock_date desc");
						if(SqlDtr.Read())
						{
							Cl_st_No=SqlDtr.GetValue(0).ToString();
							Bat_ID=SqlDtr.GetValue(1).ToString();
						}
						SqlDtr.Close();
						//Cl_st_Ltr=GenUtil.changeqtyltr(strName[1].ToString(),int.Parse(Cl_st_No));
						//Bal_Ltr+=double.Parse(Op_st_Ltr.ToString());
						dbobj.Insert_or_Update("insert into StockLedger_Batch values('Closing Balance','','"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"','"+Bat_ID+"',0,0,0,0,'"+Cl_st_No+"','"+Cl_st_Ltr+"')",ref x);
						****************************************/

					}
					int last_min=0,last_hour=0;
					DateTime last_date=System.Convert.ToDateTime("1/1/1900");
					dbobj.SelectQuery("Select top 1 datepart(Minute,stock_Date) from tempstockmaster_batch Order by stock_Date desc",ref rdr1);
					if(rdr1.Read())
					{
						last_min=int.Parse(rdr1.GetValue(0).ToString());
					}
					rdr1.Close();
					dbobj.SelectQuery("Select top 1 datepart(Hour,stock_Date) from tempstockmaster_batch Order by stock_Date desc",ref rdr1);
					if(rdr1.Read())
					{
						last_hour=int.Parse(rdr1.GetValue(0).ToString());
					}
					rdr1.Close();
					/*04.07.09 dbobj.SelectQuery("Select top 1 stock_Date from tempstockmaster_batch where Trans_Type='Closing Balance'",ref rdr1);
					if(rdr1.Read())
					{
						last_date=System.Convert.ToDateTime(rdr1.GetValue(0).ToString());
					}
					rdr1.Close();*/
					//02.07.09 dbobj.Insert_or_Update("update tempstocklmaster_batch set trans_date = dateAdd(Minute,("+last_min+"+1),'"+last_date+"') where Trans_type = 'Closing Balance'",ref x);
					//02.07.09 dbobj.Insert_or_Update("update tempstocklmaster_batch set trans_date = dateAdd(Hour,("+last_hour+"+1),'"+last_date+"') where Trans_type = 'Closing Balance'",ref x);
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Class:DBOperation_LETEST.cs,Method:Stock_Movement().  EXCEPTION: "+ ex.Message+" User_ID: "+uid);	
			}
		}
	

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		double count=1,i=1,j=2,k=3,l=4,m=5,n=6;
		public double os=0,cs=0,sales=0,rect=0,fsale=0,fpur=0;
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
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=4;
				}
				if(count==j)
				{
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=4;
				}
				if(count==k)
				{
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=4;
				}
				if(count==l)
				{
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=4;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=4;
					}
					if(count==j)
					{
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=4;
					}
					if(count==k)
					{
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=4;
					}
					if(count==l)
					{
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=4;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		protected double MultiplySJ(string str, string Prod_ID)
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
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=6;
				}
				if(count==j)
				{
					double tot=0;
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					if(rdr.Read())
					{
						if(rdr.GetValue(0).ToString()!="")
							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					}
					rdr.Close();
					ans-=tot;
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=6;
				}
				if(count==k)
				{
					//					double tot=0;
					//					InventoryClass obj = new InventoryClass();
					//					SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where inprod_id=prod_id and inProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					//					if(rdr.Read())
					//					{
					//						if(rdr.GetValue(0).ToString()!="")
					//							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					//					}
					//					rdr.Close();
					//					ans-=tot;
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=6;
				}
				if(count==l)
				{
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=6;
				}
				if(count==m)
				{
					fsale+=ans;
					Cache["fsale"]=System.Convert.ToString(fsale);
					m+=6;
				}
				if(count==n)
				{
					fpur+=ans;
					Cache["fpur"]=System.Convert.ToString(fpur);
					n+=6;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=6;
					}
					if(count==j)
					{
						double tot=0;
						InventoryClass obj = new InventoryClass();
						SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						}
						rdr.Close();
						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=6;
					}
					if(count==k)
					{
						//						double tot=0;
						//						InventoryClass obj = new InventoryClass();
						//						SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where inprod_id=prod_id and inProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						//						if(rdr.Read())
						//						{
						//							if(rdr.GetValue(0).ToString()!="")
						//								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						//						}
						//						rdr.Close();
						//						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=6;
					}
					if(count==l)
					{
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=6;
					}
					if(count==m)
					{
						fsale+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fsale"]=System.Convert.ToString(fsale);
						m+=6;
					}
					if(count==n)
					{
						fpur+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fpur"]=System.Convert.ToString(fpur);
						n+=6;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
			
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		protected double MultiplySJ1(string str,string strfoc,string foc,string Prod_ID)
		{
			//string[] mystr=str.Split(new char[]{'X'},str.Length);
			//*************
			string[] mystr=null;
			if(double.Parse(foc)==0)
				mystr=str.Split(new char[]{'X'},str.Length);
			else
				mystr=strfoc.Split(new char[]{'X'},str.Length);
			//*************
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=6;
				}
				if(count==j)
				{
					//					double tot=0;
					//					InventoryClass obj = new InventoryClass();
					//					SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					//					if(rdr.Read())
					//					{
					//						if(rdr.GetValue(0).ToString()!="")
					//							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					//					}
					//					rdr.Close();
					//					ans-=tot;
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=6;
				}
				if(count==k)
				{
					//					double tot=0;
					//					InventoryClass obj = new InventoryClass();
					//					SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					//					if(rdr.Read())
					//					{
					//						if(rdr.GetValue(0).ToString()!="")
					//							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					//					}
					//					rdr.Close();
					//					ans-=tot;
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=6;
				}
				if(count==l)
				{
					double tot=0;
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where Outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					if(rdr.Read())
					{
						if(rdr.GetValue(0).ToString()!="")
							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					}
					rdr.Close();
					ans-=tot;
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=6;
				}
				if(count==m)
				{
					fsale+=ans;
					Cache["fsale"]=System.Convert.ToString(fsale);
					m+=6;
				}
				if(count==n)
				{
					fpur+=ans;
					Cache["fpur"]=System.Convert.ToString(fpur);
					n+=6;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=6;
					}
					if(count==j)
					{
						//						double tot=0;
						//						InventoryClass obj = new InventoryClass();
						//						SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						//						if(rdr.Read())
						//						{
						//							if(rdr.GetValue(0).ToString()!="")
						//								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						//						}
						//						rdr.Close();
						//						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=6;
					}
					if(count==k)
					{
						//						double tot=0;
						//						InventoryClass obj = new InventoryClass();
						//						SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						//						if(rdr.Read())
						//						{
						//							if(rdr.GetValue(0).ToString()!="")
						//								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						//						}
						//						rdr.Close();
						//						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=6;
					}
					if(count==l)
					{
						double tot=0;
						InventoryClass obj = new InventoryClass();
						SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where Outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						}
						rdr.Close();
						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=6;
					}
					if(count==m)
					{
						fsale+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fsale"]=System.Convert.ToString(fsale);
						m+=6;
					}
					if(count==n)
					{
						fpur+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fpur"]=System.Convert.ToString(fpur);
						n+=6;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
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
		
		/// <summary>
		/// This is used to split the date.
		/// </summary>
		private DateTime getdate(string dat,bool to)
		{
			
			string[] dt=dat.IndexOf("/")>0? dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}
		
		/// <summary>
		/// This is used to make sorting the datagrid on clicking of datagridheader.
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
				CreateLogFiles.ErrorLog("Form:StockmovementReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
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
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata()
		{

			Stock_Movement();     //Add by vikas 06.07.09 

			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//***********
			grdLeg.Visible=true;
			int x=0;
			object op=null;	
			int count=0;
			string sql="";
			System.Data.SqlClient.SqlDataReader rdr=null;
			
			if(dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
			{
				//sql="select distinct productid from stock_master s,products p where p.prod_id=s.productid and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				//06.07.09 sql="select distinct productid,p.batch_id from tempstockmaster_batch s,batchno p where p.prod_id=s.productid and p.batch_id=s.batch_id";
				sql="select distinct p.productid,p.batch_id from tempstockmaster_batch s,stockmaster_batch p where p.productid=s.productid and p.batch_id=s.batch_id";
			}
			else if(!dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
			{
				//04.07.09 sql="select distinct productid from tempstockmaster_batch s,products p where p.prod_id=s.productid and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				
				
			}
			else if(dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
			{
				//04.07.09 sql="select distinct productid from tempstockmaster_batch s,products p where p.prod_id=s.productid and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
				//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
				sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
			}
			else if(!dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
			{
				//04.07.09 sql="select distinct productid from tempstockmaster_batch s,products p where p.prod_id=s.productid and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";// and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			}
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			try
			{
				while(rdr.Read())
				{
					//06.07.09 dbobj.ExecProc(OprType.Insert,"sp_stockmovement_batch",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
					dbobj.ExecProc(OprType.Insert,"sp_stockmovement_batch",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@batch",Int32.Parse(rdr["batch_id"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
					//dbobj.ExecProc(OprType.Insert,"sp_tempstockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
					count++;
				}
				rdr.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("stock : "+ex+"  "+count);
			}
			
			if(RadSM.Checked)
			{
				if(drpstore.SelectedIndex>0)
				{
					//if(drpstore.SelectedItem.Text.IndexOf(":")>0)
					//{
					//	string[] stor=drpstore.SelectedItem.Text.Split(new char []{':'},drpstore.SelectedItem.Text.Length);
					//	string tid="";
					//	dbobj.SelectQuery("select tank_id  from tank where tank_name='"+stor[0]+"' and prod_name like '"+stor[1]+"'","tank_id",ref tid);
					//	sql="select * from stkmv where Location='"+tid+"'";
				
					//}
					//else
					//{
					if(chkZeroBal.Checked==true)
					{
						//sql="select * from stkmv where Location='"+drpstore.SelectedItem.Value +"' and op=0 and sales=0 and rcpt=0 and cs=0";
						sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"'";
					}
					else
					{
						//sql="select * from stkmv where Location='"+drpstore.SelectedItem.Value +"'";
						sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
					}
					//	}
					//	dbobj.SelectQuery(sql,ref rdr);
				}
				else
				{
					if(chkZeroBal.Checked==true)
						//sql="select * from stkmv where op=0 and sales=0 and rcpt=0 and cs=0";
						sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id";
					else
						//sql="select * from stkmv";
						sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
					//	dbobj.SelectQuery("select * from stkmv",ref rdr);
				}         
			}
			else
			{
				if(drpstore.SelectedIndex>0)
				{
					if(chkZeroBal.Checked==true)
					{
						//sql="select * from stkmv s,products p where p.prod_id=s.prod_id Location='"+drpstore.SelectedItem.Value +"'";
						sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"'";
					}
					else
					{
						//sql="select * from stkmv s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
						sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
					}
				}
				else
				{
					if(chkZeroBal.Checked==true)
						sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id";
					else
						sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
				}
			}

			//***********
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();
			da.Fill(ds,"stkmv_batch");
			DataTable dtcustomer=ds.Tables["stkmv_batch"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			if(RadSM.Checked)
			{
				grdLeg.DataSource=dv;
				gridSJ.Visible=false;
				if(dv.Count!=0)
				{
					grdLeg.DataBind();
					grdLeg.Visible=true;
				}
				else
				{
					grdLeg.Visible=false;
					MessageBox.Show("Data Not Available");
				}
			}
			else
			{
				gridSJ.DataSource=dv;
				grdLeg.Visible=false;
				if(dv.Count!=0)
				{
					gridSJ.DataBind();
					gridSJ.Visible=true;
				}
				else
				{
					gridSJ.Visible=false;
					MessageBox.Show("Data Not Available");
				}
			}
			sqlcon.Dispose();
			// truncate table after use.
			dbobj.Insert_or_Update("truncate table stkmv_batch", ref x);

		}

		/// <summary>
		/// This is used to show the report.
		/// </summary>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			try
			{
				strorderby="Prod_Code ASC";
				Session["Column"]="Prod_Code";
				Session["order"]="ASC";
				Bindthedata();
				CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Class:DBOperation_LETEST.cs,Method:cmdrpt_Click  Stock Movement Report  Viewed  useried "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Class:DBOperation_LETEST.cs,Method:cmdrpt_Click,  Stock Movement Report  Viewed  EXCEPTION "+ ex.Message+"  userid  "+uid);		
			}
		}

		/// <summary>
		/// check if the products type is fuel or package is Loose Oil then return space.
		/// </summary>
		double count1=1,i1=1,j1=2,k1=3,l1=4;
		public double osp=0,csp=0,salesp=0,rectp=0;
		protected string Check(string cs, string type,string pack)
		{
			if(type.ToUpper().Equals("FUEL") || pack.IndexOf("Loose")!= -1)
				return "&nbsp;";
			else
				//**********************
				if(count1==i1)
			{
				osp+=System.Convert.ToDouble(cs);
				Cache["osp"]=System.Convert.ToString(osp);
				i1+=4;
			}
			if(count1==j1)
			{
				rectp+=System.Convert.ToDouble(cs);
				Cache["rectp"]=System.Convert.ToString(rectp);
				j1+=4;
			}
			if(count1==k1)
			{
				salesp+=System.Convert.ToDouble(cs);
				Cache["salesp"]=System.Convert.ToString(salesp);
				k1+=4;
			}
			if(count1==l1)
			{
				csp+=System.Convert.ToDouble(cs);
				Cache["csp"]=System.Convert.ToString(csp);
				l1+=4;
			}
			count1++;
			//**********************
			return cs;
		}

		/// <summary>
		/// if the tank the returns the tank abbrevation name, for that tank.
		/// this method is not used.
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

		private void grdLeg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
		
		/// <summary>
		/// Contacts the server and sends the StockMovementReport.txt file name to print.
		/// </summary>
		public void print()
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

					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\StockMovementReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Method:print EXCEPTION  "+ane.Message+" userid "+ uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Method:print EXCEPTION  "+se.Message+" userid "+ uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
				}
				//CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);


			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);

			}
		}
		
		/// <summary>
		/// This is used to print report StockMovement.txt .
		/// </summary>
		protected void prnButton_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				
				Stock_Movement(); //add by vikas 06.07.09

				string sql         = "";
				string strPackOp = "";
				string strPackRc = "";
				string strPackSl = "";
				string strPackCl = "";
				string[] strSplit;
				string pack = "";
				os=0;cs=0;sales=0;rect=0;fsale=0;fpur=0;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\StockMovementReport.txt";
				StreamWriter sw = new StreamWriter(path);
				System.Data.SqlClient.SqlDataReader rdr=null;
							
				if(dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
				{
					//06.07.09 sql="select distinct productid,p.batch_id from tempstockmaster_batch s,batchno p where p.prod_id=s.productid and p.batch_id=s.batch_id";
					sql="select distinct p.productid,p.batch_id from tempstockmaster_batch s,stockmaster_batch p where p.productid=s.productid and p.batch_id=s.batch_id";
				}
				else if(!dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
				{
					//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
					sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				}
				else if(dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
				{
					//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
					sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
				}
				else if(!dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
				{
					//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
					sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				}

				dbobj.SelectQuery(sql,ref rdr);
				object op=null;
				while(rdr.Read())
					//06.07.09 dbobj.ExecProc(OprType.Insert,"sp_stockmovement_batch",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
					dbobj.ExecProc(OprType.Insert,"sp_stockmovement_batch",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@batch",Int32.Parse(rdr["batch_id"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
				rdr.Close();
				if(RadSM.Checked)
				{
					if(drpstore.SelectedIndex>0)
					{
						
						if(chkZeroBal.Checked==true)
							sql="select * from stkmv_batch s,products p where Location='"+drpstore.SelectedItem.Value +"' and p.prod_id=s.prod_id";
						else
							sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
					}
					else
					{
						if(chkZeroBal.Checked==true)
							sql="select * from stkmv_batch s,products p where s.prod_id=p.prod_id";
						else
							sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
					}
				}
				else
				{
					if(drpstore.SelectedIndex>0)
					{
						if(chkZeroBal.Checked==true)
							sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"'";
						else
							sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
					}
					else
					{
						if(chkZeroBal.Checked==true)
							sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id";
						else
							sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
					}
				}
				if(Cache["strorderby"].ToString().IndexOf("Prod_Name")!=-1)
					sql=sql+" order by p."+Cache["strorderby"];
				else
					sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				// Condensed
				sw.Write((char)27);          //added by vishnu
				sw.Write((char)67);          //added by vishnu
				sw.Write((char)0);           //added by vishnu
				sw.Write((char)12);          //added by vishnu
				sw.Write((char)27);          //added by vishnu
				sw.Write((char)78);          //added by vishnu
				sw.Write((char)5);           //added by vishnu
				sw.Write((char)27);          //added by vishnu
				sw.Write((char)15);
				//**********
				string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				sw.WriteLine(GenUtil.GetCenterAddr("===================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Stock Movement Report From "+txtDateFrom.Text+" To "+txtDateTo.Text,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("===================================================",des.Length));
				sw.WriteLine();
				string info="",info1="";
				//string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				if(RadSM.Checked)
				{
					sw.WriteLine("+---------+-------------------------------------+---------------+-----------------+-----------------+-----------------+-----------------+");
					sw.WriteLine("| Product |                                     |               |  Opening Stock  |    Receipt      |     Sales       |  Closing Stock  |");
					sw.WriteLine("|  Code   |         Product Name                |   Location    |------+----------|------+----------|------+----------|------+----------|");
					sw.WriteLine("|         |                                     |               | Pkg. |  Lt./Kg  | Pkg. |  Lt./Kg  | Pkg. |  Lt./Kg  | Pkg. |  Lt./Kg  |"); 
					sw.WriteLine("+---------+-------------------------------------+---------------+------+----------+------+----------+------+----------+------+----------+");
					//             123456789 1234567890123456789012345678901234567 123456789012345 123456 1234567890 123456 1234567890 123456 1234567890 123456 1234567890
					info = " {0,-9:S} {1,-37:S} {2,-15:S} {3,6:F} {4,10:F} {5,6:F} {6,10:F} {7,6:F} {8,10:F} {9,6:F} {10,10:F}";
					info1 =" {0,-9:S} {1,-37:S} {2,-15:S} {3,6:F} {4,10:F} {5,6:F} {6,10:F} {7,6:F} {8,10:F} {9,6:F} {10,10:F}";
				}
				else
				{
					sw.WriteLine("+---------+------------------------------+-----------+-----------+-----------+----------+-----------+-----------+----------+------------+");
					sw.WriteLine("| Product |        Product Name          |  Opening  |  Receipt  |  Receipt  |  SJ(In)  |   Sales   |   Sales   |  SJ(Out) |  Closing   |");
					sw.WriteLine("|  Code   |                              |  Stock    |           |    Foc    |          |           |    Foc    |          |   Stock    |");
					sw.WriteLine("+---------+------------------------------+-----------+-----------+-----------+----------+-----------+-----------+----------+------------+");
					//             123456789 123456789012345678901234567890 12345678901 12345678901 12345678901 1234567890 12345678901 12345678901 1234567890 123456789012
					info = " {0,-9:S} {1,-30:S} {2,11:S} {3,11:F} {4,11:F} {5,10:F} {6,11:F} {7,11:F} {8,10:F} {9,11:F}";
					//info1 =" {0,-9:S} {1,-37:S} {2,-15:S} {3,6:F} {4,10:F} {5,6:F} {6,10:F} {7,6:F} {8,10:F} {9,6:F} {10,10:F}";
				}
			 
				if(rdr.HasRows)
				{
					if(RadSM.Checked)
					{
						while(rdr.Read())
						{
							strPackOp = "";
							strPackRc = "";
							strPackSl = "";
							strPackCl = "";
							pack = "";
							string pabbName="";
							// check if the product is type of fuel then do not display the package , and displays the tank abbrevation name.
							if(rdr["category"].ToString().ToUpper().Equals("FUEL") )
							{
								dbobj.SelectQuery("select prod_AbbName from tank where tank_id="+rdr["location"].ToString().Trim(),"prod_AbbName",ref pabbName);
								sw.WriteLine(info1,rdr["prod_code"].ToString().Trim(),GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),37),pabbName,"",rdr["op"].ToString().Trim(),"",rdr["rcpt"].ToString().Trim(),"",rdr["sales"].ToString().Trim(),"",rdr["cs"].ToString().Trim());
                      	
							}
								// if package is Loose Oil then do not package .
							else if(rdr["pack_type"].ToString().IndexOf("Loose") != -1)
							{
								sw.WriteLine(info1,rdr["prod_code"].ToString().Trim(),GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),37),rdr["location"].ToString().Trim(),"",rdr["op"].ToString().Trim(),"",rdr["rcpt"].ToString().Trim(),"",rdr["sales"].ToString().Trim(),"",rdr["cs"].ToString().Trim());
							}
							else
							{                      	
								pack = rdr["pack_type"].ToString().Trim();
								if (pack.IndexOf("X")<0 || pack.Equals("") )
								{
									strPackOp = rdr["op"].ToString().Trim();
									strPackRc = rdr["rcpt"].ToString().Trim();
									strPackSl= rdr["Sales"].ToString().Trim();
									strPackCl = rdr["cs"].ToString().Trim();

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
									strPackOp = rdr["op"].ToString().Trim();
									if(!strPackOp.Equals("")) 
									{
										strPackOp = ""+System.Convert.ToDouble(strPackOp)*d1*d2 ;
									}
									strPackRc = rdr["rcpt"].ToString().Trim();
									if(!strPackRc.Equals(""))
									{
										strPackRc = ""+System.Convert.ToDouble(strPackRc)*d1*d2 ;
									}
									strPackSl = rdr["Sales"].ToString().Trim();
									if(!strPackSl.Equals(""))
									{
										strPackSl = ""+System.Convert.ToDouble(strPackSl)*d1*d2 ;
									}
									strPackCl = rdr["cs"].ToString().Trim();
									if(!strPackCl.Equals(""))
									{
										strPackCl = ""+System.Convert.ToDouble(strPackCl)*d1*d2 ;            
									}
								}
								//06.07.09 sw.WriteLine(info,rdr["prod_code"].ToString().Trim(),GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),37),rdr["location"].ToString().Trim(),rdr["op"].ToString().Trim(),strPackOp,rdr["rcpt"].ToString().Trim(),strPackRc,rdr["sales"].ToString().Trim(),strPackSl,rdr["cs"].ToString().Trim(),strPackCl);
								sw.WriteLine(info,rdr["prod_code"].ToString().Trim(),
									GenUtil.TrimLength(rdr["prod_name"].ToString().Trim(),37),
									batch_name(rdr["Batch_id"].ToString()).ToString(),
									rdr["op"].ToString().Trim(),
									strPackOp,rdr["rcpt"].ToString().Trim(),
									strPackRc,
									rdr["sales"].ToString().Trim(),
									strPackSl,
									rdr["cs"].ToString().Trim(),
									strPackCl
									);
							}
					
						}
						sw.WriteLine("+---------+-------------------------------------+---------------+------+----------+------+----------+------+----------+------+----------+");
						sw.WriteLine(info,"Total:","","",Cache["osp"],Cache["os"],Cache["rectp"],Cache["rect"],Cache["salesp"],Cache["sales"],Cache["csp"],Cache["cs"]);
						sw.WriteLine("+---------+-------------------------------------+---------------+------+----------+------+----------+------+----------+------+----------+");
					}
					else
					{
						count=2;
						while(rdr.Read())
						{
							sw.WriteLine(info,rdr["prod_code"].ToString().Trim(),
								GenUtil.TrimLength(rdr["prod_name"].ToString(),30),
								System.Convert.ToString(double.Parse(rdr["op"].ToString())*double.Parse(rdr["total_qty"].ToString())),
								MultiplySJ(rdr["rcpt"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString()).ToString(),
								MultiplySJ(rdr["rcptfoc"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString()).ToString(),
								getSJIn(rdr["prod_id"].ToString()).ToString(),
								MultiplySJ1(rdr["sales"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["salesfoc"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["salesfoc"].ToString(),rdr["prod_id"].ToString()).ToString(),
								MultiplySJ(rdr["salesfoc"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString()).ToString(),
								getSJOut(rdr["prod_id"].ToString()).ToString(),
								MultiplySJ(rdr["cs"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString()).ToString()
								);
							count++;
						}
						sw.WriteLine("+---------+------------------------------+-----------+-----------+-----------+----------+-----------+-----------+----------+------------+");
						sw.WriteLine(info,"Total","",Cache["os"],Cache["rect"],Cache["sales"],SJOut.ToString(),Cache["cs"],Cache["fsale"],SJin.ToString(),Cache["fpur"]);
						sw.WriteLine("+---------+------------------------------+-----------+-----------+-----------+----------+-----------+-----------+----------+------------+");
					}
				}
			
				
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
			
				dbobj.Dispose();
				rdr.Close();

				sw.Close();
				
				print();
				int x=0;
				// truncate table after use.
				dbobj.Insert_or_Update("truncate table stkmv_batch", ref x);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockMovement_Batch.aspx,Method:prnButton_Click(). EXCEPTION  "+ex.Message+" userid "+ uid);
			}
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\StockMovementReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string strPackOp = "";
			string strPackRc = "";
			string strPackSl = "";
			string strPackCl = "";
			string[] strSplit;
			os=0;cs=0;sales=0;rect=0;fsale=0;fpur=0;
			string pack = "",sql="";;
			/*06.07.09 coment by vikas 
			if(dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
				sql="select distinct productid from stock_master s,products p where p.prod_id=s.productid";// and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			else if(!dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
				sql="select distinct productid from stock_master s,products p where p.prod_id=s.productid and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			else if(dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
				sql="select distinct productid from stock_master s,products p where p.prod_id=s.productid and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";// and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			else if(!dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
				sql="select distinct productid from stock_master s,products p where p.prod_id=s.productid and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";// and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			dbobj.SelectQuery(sql,ref rdr);
			object op=null;
			while(rdr.Read())
				//dbobj.ExecProc(OprType.Insert,"sp_tempstockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
				dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
			rdr.Close();*/

			if(dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
			{
				//06.07.09 sql="select distinct productid,p.batch_id from tempstockmaster_batch s,batchno p where p.prod_id=s.productid and p.batch_id=s.batch_id";
				sql="select distinct p.productid,p.batch_id from tempstockmaster_batch s,stockmaster_batch p where p.productid=s.productid and p.batch_id=s.batch_id";
			}
			else if(!dropcategory.SelectedItem.Text.Equals("All") && dropPackType.SelectedItem.Text.Equals("All"))
			{
				//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			}
			else if(dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
			{
				//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
				sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"'";
			}
			else if(!dropcategory.SelectedItem.Text.Equals("All") && !dropPackType.SelectedItem.Text.Equals("All"))
			{
				//06.07.09 sql="select distinct productid,b.batch_id from tempstockmaster_batch s,products p,batchno b where p.prod_id=s.productid and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
				sql="select distinct b.productid,b.batch_id from tempstockmaster_batch s,products p,stockmaster_batch b where p.prod_id=s.productid and b.productid=p.prod_id and b.batch_id=s.batch_id and p.Pack_Type='"+dropPackType.SelectedItem.Text.ToString()+"' and p.category='"+dropcategory.SelectedItem.Text.ToString()+"'";
			}

			dbobj.SelectQuery(sql,ref rdr);
			object op=null;
			while(rdr.Read())
				//06.07.09 dbobj.ExecProc(OprType.Insert,"sp_stockmovement_batch",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
				dbobj.ExecProc(OprType.Insert,"sp_stockmovement_batch",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@batch",Int32.Parse(rdr["batch_id"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
			rdr.Close();
			
			if(RadSM.Checked)
			{
				if(drpstore.SelectedIndex>0)
				{
					if(chkZeroBal.Checked==true)
						sql="select * from stkmv_batch s,products p where s.prod_id=p.prod_id and Location='"+drpstore.SelectedItem.Value +"'";
					else
						sql="select * from stkmv_batch s,products p where s.prod_id=p.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
				}
				else
				{
					if(chkZeroBal.Checked==true)
						sql="select * from stkmv_batch s,products p where s.prod_id=p.prod_id";
					else
						sql="select * from stkmv_batch s,products p where s.prod_id=p.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0)";
				}
			}
			else
			{
				if(drpstore.SelectedIndex>0)
				{
					if(chkZeroBal.Checked==true)
						sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"'";
					else
						sql="select * from stkmv_batch s,products p where p.prod_id=s.prod_id and Location='"+drpstore.SelectedItem.Value +"' and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
				}
				else
				{
					if(chkZeroBal.Checked==true)
						sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id";
					else
						sql="select * from stkmv_batch s,products p where p.Prod_id=s.Prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
				}
			}
			if(Cache["strorderby"].ToString().IndexOf("Prod_Name")!=-1)
				sql=sql+" order by p."+Cache["strorderby"];
			else
				sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+txtDateTo.Text);
			sw.WriteLine();
			if(RadSM.Checked)
			{
				sw.WriteLine("Product Code\tProduct Name\tBatch Name\tOpening Stock(Pkg.)\tOpening Stock(Ltr/kg)\tReceipt(Pkg.)\tReceipt(Ltr/kg)\tSales(Pkg.)\tSales(Ltr/kg)\tClosing Stock(Pkg.)\tClosing Stock(Ltr/kg)");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						strPackOp = "";
						strPackRc = "";
						strPackSl = "";
						strPackCl = "";
						pack = "";
						
						if(rdr["pack_type"].ToString().IndexOf("Loose") != -1)
							sw.WriteLine(rdr["prod_code"].ToString().Trim()+"\t"+rdr["prod_name"].ToString().Trim()+"\t"+rdr["location"].ToString().Trim()+"\t"+""+"\t"+rdr["op"]+"\t"+ToString().Trim()+"\t"+""+"\t"+rdr["rcpt"].ToString().Trim()+"\t"+""+"\t"+rdr["sales"].ToString().Trim()+"\t"+""+"\t"+rdr["cs"].ToString().Trim());
						else
						{                      	
							pack = rdr["pack_type"].ToString().Trim();
							if (pack.IndexOf("X")<0 || pack.Equals("") )
							{
								strPackOp = rdr["op"].ToString().Trim();
								strPackRc = rdr["rcpt"].ToString().Trim();
								strPackSl= rdr["Sales"].ToString().Trim();
								strPackCl = rdr["cs"].ToString().Trim();
							}
							else
							{
								strSplit = pack.Split(new char []{'X'},pack.Length);
								double d1 = 1;
								double d2 = 1;
								if(!strSplit[0].Trim().Equals(""))
									d1 = System.Convert.ToDouble(strSplit[0]);
								if(!strSplit[1].Trim().Equals (""))
									d2 = System.Convert.ToDouble(strSplit[1]);
								strPackOp = rdr["op"].ToString().Trim();
								if(!strPackOp.Equals("")) 
									strPackOp = ""+System.Convert.ToDouble(strPackOp)*d1*d2 ;
								strPackRc = rdr["rcpt"].ToString().Trim();
								if(!strPackRc.Equals(""))
									strPackRc = ""+System.Convert.ToDouble(strPackRc)*d1*d2 ;
								strPackSl = rdr["Sales"].ToString().Trim();
								if(!strPackSl.Equals(""))
									strPackSl = ""+System.Convert.ToDouble(strPackSl)*d1*d2 ;
								strPackCl = rdr["cs"].ToString().Trim();
								if(!strPackCl.Equals(""))
									strPackCl = ""+System.Convert.ToDouble(strPackCl)*d1*d2 ;            
							}
							//06.07.09 sw.WriteLine(rdr["prod_code"].ToString().Trim()+"\t"+rdr["prod_name"].ToString().Trim()+"\t"+rdr["location"].ToString().Trim()+"\t"+rdr["op"].ToString().Trim()+"\t"+strPackOp+"\t"+rdr["rcpt"].ToString().Trim()+"\t"+strPackRc+"\t"+rdr["sales"].ToString().Trim()+"\t"+strPackSl+"\t"+rdr["cs"].ToString().Trim()+"\t"+strPackCl);
							sw.WriteLine(rdr["prod_code"].ToString().Trim()+"\t"+
								rdr["prod_name"].ToString().Trim()+"\t"+
								batch_name(rdr["batch_id"].ToString()).ToString().Trim()+"\t"+
								rdr["op"].ToString().Trim()+"\t"+
								strPackOp+"\t"+
								rdr["rcpt"].ToString().Trim()+"\t"+
								strPackRc+"\t"+
								rdr["sales"].ToString().Trim()+"\t"+
								strPackSl+"\t"+
								rdr["cs"].ToString().Trim()+"\t"+
								strPackCl);
						}
					}
				}
				sw.WriteLine("Total\t\t\t"+Cache["osp"]+"\t"+Cache["os"]+"\t"+Cache["rectp"]+"\t"+Cache["rect"]+"\t"+Cache["salesp"]+"\t"+Cache["sales"]+"\t"+Cache["csp"]+"\t"+Cache["cs"]);
			}
			else
			{
				sw.WriteLine("Product Code\tProduct Name\tOpening Stock\tReceipt\tReceipt Foc\tSJ (In)\tSales\tSales Foc\tSJ (Out)\tClosing Stock");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						sw.WriteLine(rdr["prod_code"].ToString().Trim()+"\t"+
							rdr["prod_name"].ToString()+"\t"+
							MultiplySJ(rdr["op"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString())+"\t"+
							MultiplySJ(rdr["rcpt"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString())+"\t"+
							MultiplySJ(rdr["rcptfoc"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString())+"\t"+
							getSJOut(rdr["prod_id"].ToString())+"\t"+
							MultiplySJ1(rdr["sales"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["salesfoc"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["salesfoc"].ToString(),rdr["prod_id"].ToString())+"\t"+
							MultiplySJ(rdr["salesfoc"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString())+"\t"+
							getSJIn(rdr["prod_id"].ToString())+"\t"+
							MultiplySJ(rdr["cs"].ToString()+"X"+rdr["pack_type"].ToString(),rdr["prod_id"].ToString())
							);
					}
					sw.WriteLine("Total"+"\t"+""+"\t"+Cache["os"]+"\t"+Cache["rect"]+"\t"+Cache["sales"]+"\t"+SJin.ToString()+"\t"+Cache["cs"]+"\t"+Cache["fsale"]+"\t"+SJOut.ToString()+"\t"+Cache["fpur"]);
				}
			}
			dbobj.Dispose();
			rdr.Close();
			sw.Close();
			int x=0;
			// truncate table after use.
			dbobj.Insert_or_Update("truncate table stkmv", ref x);
		}

		/// <summary>
		/// Prepares the excel report file StockMovement.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(grdLeg.Visible==true || gridSJ.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:StockMovementReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    StockMovementReport Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:StockMovementReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    StockMovementReport Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		public void SeqStockMaster()
		{
			InventoryClass obj = new InventoryClass();
			SqlCommand cmd;
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlDataReader rdr1=null;
			for(int i=0;i<custAccount.Count;i++)
			{
				string str="select * from tempStock_Master where Productid='"+custAccount[i].ToString()+"' order by Stock_date";
				rdr1=obj.GetRecordSet(str);
				double OS=0,CS=0,k=0;
				while(rdr1.Read())
				{
					if(k==0)
					{
						OS=double.Parse(rdr1["opening_stock"].ToString());
						k++;
					}
					else
						OS=CS;
					//					//********
					//					if(OS!=double.Parse(rdr1["opening_stock"].ToString()))
					//						OS+=double.Parse(rdr1["opening_stock"].ToString());
					//					//******
					CS=OS+double.Parse(rdr1["receipt"].ToString())-(double.Parse(rdr1["sales"].ToString())+double.Parse(rdr1["salesfoc"].ToString()));
					Con.Open();
					cmd = new SqlCommand("update tempStock_Master set opening_stock='"+OS.ToString()+"', Closing_Stock='"+CS.ToString()+"' where ProductID='"+rdr1["Productid"].ToString()+"' and Stock_Date='"+rdr1["stock_date"].ToString()+"'",Con);
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					Con.Close();
				}
				rdr1.Close();
			}
		}

		public double SJin=0,SJOut=0;
		/// <summary>
		/// This method return the total stock adjustment out qty in given period.
		/// </summary>
		public string getSJOut(string Prod_ID)
		{
			double tot=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where prod_id=inprod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
			}
			rdr.Close();
			SJOut+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method return the total stock adjustment in qty in given period.
		/// </summary>
		public string getSJIn(string Prod_ID)
		{
			double tot=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where prod_id=outprod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
			}
			rdr.Close();
			SJin+=tot;
			return tot.ToString();
		}
	}
}