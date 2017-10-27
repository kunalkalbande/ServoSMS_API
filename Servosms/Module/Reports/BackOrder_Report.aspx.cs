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
using RMG;
using System.IO;
using DBOperations;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for CreditPeriodAnalysisSheetReport.
	/// </summary>
	public partial class BackOrder_Report : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv1;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public int flage=0;
		protected System.Web.UI.WebControls.CheckBox ChkTrue;

		string uid;
		public SqlDataReader rdr;
		public string sql="";
		//public int flage=0;
		//public SqlDataReader rdr=null;
	
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
			}
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:CreditPeriodAnalysisSheetReport.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{

				//Count=0;
				txtDateFrom.Text = DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text = DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				//*******************
				SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//SqlCommand cmd;
				InventoryClass obj = new InventoryClass();
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader rdr=null;//,rdr1=null;
				rdr = obj.GetRecordSet("select Acc_Date_From from organisation");
				if(rdr.Read())
				{
					string s =GenUtil.trimDate(rdr.GetValue(0).ToString());
					string[] ss=s.IndexOf("/")>0?s.Split(new char[] {'/'},s.Length): s.Split(new char[] { '-' }, s.Length);
					tempPeriod.Value=ss[0]+":"+ss[2];
				}
				rdr.Close();
				
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="57";
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

				object ob=null;
				
				dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetails",ref ob,"@Cust_ID","");
				
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

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// This method is used to view the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			flage=1;
		}
		
		string BO_No="0";
		public string GetBackOrderNo(string Order_No, string Prod_id, string cust_id)
		{
			try
			{
				BO_No="0";
				InventoryClass  obj=new InventoryClass ();
				SqlDataReader SqlDtr;
				string sql="";
				sql=" select distinct bo_1,bo_2,bo_3 from ovd where cast(item_qty as float)>cast(sale_qty as float) and Order_id="+Order_No.ToString()+" and item_id="+Prod_id.ToString()+" and cust_id="+cust_id.ToString();
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					if(SqlDtr["Bo_3"].ToString()==null || SqlDtr["Bo_3"].ToString()=="")
					{
						if(SqlDtr["Bo_2"]==null || SqlDtr["Bo_2"].ToString()=="")
						{
							if(SqlDtr["Bo_1"].ToString()==null || SqlDtr["Bo_1"].ToString()=="")
							{
								BO_No="0";
							}
							else
							{
								if(SqlDtr["Bo_1"].ToString()!=null && SqlDtr["Bo_1"].ToString()!="")
								{
									BO_No="BO:"+SqlDtr["Bo_1"].ToString();
								}
							}
						}
						else 
						{
							if(SqlDtr["Bo_2"].ToString()!=null && SqlDtr["Bo_2"].ToString()!="")
							{
								BO_No="BO:"+SqlDtr["Bo_2"].ToString();
							}
						}
					}
					else 
					{
						if(SqlDtr["Bo_3"].ToString()!=null && SqlDtr["Bo_3"].ToString()!="")
						{
							BO_No="BO:"+SqlDtr["Bo_3"].ToString();
						}
					}
				}
				SqlDtr.Close ();
				return BO_No.ToString();
			}
			catch(Exception ex)
			{
				return BO_No.ToString();
				CreateLogFiles.ErrorLog(" Form : BackOrder_Report.aspx , Method : GetOrderInvoice,   Exception : "+ex.Message+" user : "+uid);
			}
		}

		string BO_Date="0";
		public string GetBackOrderDate(string Order_No, string Prod_id, string cust_id)
		{
			try
			{
				BO_Date="0";
				InventoryClass  obj=new InventoryClass ();
				SqlDataReader SqlDtr;
				string sql="";
				sql=" select distinct Date_Bo_1,Date_Bo_2,Date_Bo_3 from ovd where item_qty > sale_qty and Order_id="+Order_No.ToString()+" and item_id="+Prod_id.ToString()+" and cust_id="+cust_id.ToString();
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					if(SqlDtr["Date_Bo_3"].ToString()==null || SqlDtr["Date_Bo_3"].ToString()=="")
					{
						if(SqlDtr["Date_Bo_2"]==null || SqlDtr["Date_Bo_2"].ToString()=="")
						{
							if(SqlDtr["Date_Bo_1"].ToString()==null || SqlDtr["Date_Bo_1"].ToString()=="")
							{
								BO_Date="0";
							}
							else
							{
								if(SqlDtr["Date_Bo_1"].ToString()!=null && SqlDtr["Date_Bo_1"].ToString()!="")
								{
									BO_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Date_Bo_1"].ToString()));
								}
							}
						}
						else 
						{
							if(SqlDtr["Date_Bo_2"].ToString()!=null && SqlDtr["Date_Bo_2"].ToString()!="")
							{
								BO_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Date_Bo_2"].ToString()));
							}
						}
					}
					else 
					{
						if(SqlDtr["Date_Bo_3"].ToString()!=null && SqlDtr["Date_Bo_3"].ToString()!="")
						{
							BO_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Date_Bo_3"].ToString()));
						}
					}
				}
				SqlDtr.Close ();
				return BO_Date.ToString();
			}
			catch(Exception ex)
			{
				return BO_Date.ToString();
				CreateLogFiles.ErrorLog(" Form : BackOrder_Report.aspx , Method : GetOrderInvoice,   Exception : "+ex.Message+" user : "+uid);
			}
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			try
			{
				/*********Add by vikas 24.12.12********************************/
				string[] Order_No=DropValue.Value.ToString().Split(new char[] {':'});
				int Count=Order_No.Length;
				/***************************End********************************/
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2);
				string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
				Directory.CreateDirectory(strExcelPath);
				string path = home_drive+@"\Servosms_ExcelFile\Export\BackOrder_Report.xls";
				StreamWriter sw = new StreamWriter(path);

				string sql="";
				flage=1;
				InventoryClass obj=new InventoryClass();
				
				/*if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					else if(DropSearchBy.SelectedIndex==2)
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					else if(DropSearchBy.SelectedIndex==3)
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					else if(DropSearchBy.SelectedIndex==4)
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					else if(DropSearchBy.SelectedIndex==5)
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
				}
				else
				{
					sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
				}*/

				if(droptype.SelectedIndex==2)
				{
					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==2)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==3)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==4)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==5)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==6)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and (bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
					}
					else
					{
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					}
				}
				else if(droptype.SelectedIndex==1)
				{
					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)=cast(sale_qty as float) and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==2)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)=cast(sale_qty as float) and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==3)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)=cast(sale_qty as float) and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==4)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)=cast(sale_qty as float) and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==5)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)=cast(sale_qty as float) and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==6)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and (bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
					}
					else
					{
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)=cast(sale_qty as float) and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					}
				}
				else if(droptype.SelectedIndex==0)
				{
					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==2)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==3)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==4)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==5)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
						else if(DropSearchBy.SelectedIndex==6)
							sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and (bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
					}
					else
					{
						sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					}
				}

				rdr=obj.GetRecordSet(sql);
			
				int i=1;
				string Pack="";
				double ltr_qty=0;
				if(rdr.HasRows)
				{
					sw.WriteLine("SNo\tOrder No\tOrder Date\tCustomer Name,Place\tCust Type\tProduct Name\tOrder Qty Nos\tOrder Qty Ltr\tInvoice No\tInvoice Date\tSale Qty Nos\tSale QtyLtr\tBO No\tBO Date");
					
					while(rdr.Read())
					{
					
						Pack=rdr["Pack_Type"].ToString();
						string[] Arr_Pack=Pack.Split(new char[] {'X'});
						ltr_qty=double.Parse(Arr_Pack[0].ToString())*double.Parse(Arr_Pack[1].ToString());
						
						sw.WriteLine(i.ToString()+"\t"+
							rdr["Order_Id"].ToString()+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Order_Date"].ToString()))+"\t"+
							rdr["Cust_Name"].ToString()+", "+rdr["City"].ToString()+"\t"+
							rdr["Cust_Type"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+" : "+rdr["Pack_Type"].ToString()+"\t"+
							rdr["item_Qty"].ToString()+"\t"+
							Math.Round(ltr_qty*double.Parse(rdr["item_Qty"].ToString()),1)+"\t"+
							rdr["Sale_Trans_id"].ToString()+"\t"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Sale_Trans_Date"].ToString()))+"\t"+
							rdr["Sale_Qty"].ToString()+"\t"+Math.Round(ltr_qty*double.Parse(rdr["Sale_Qty"].ToString()),1)+"\t"+
							GetBackOrderNo(rdr["Order_Id"].ToString(),rdr["item_Id"].ToString(),rdr["cust_Id"].ToString())+"\t"+
						    GetBackOrderDate(rdr["Order_Id"].ToString(),rdr["item_Id"].ToString(),rdr["cust_Id"].ToString()));
						i++;
					}
					rdr.Close();
				}
				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}
	

		/// <summary>
		/// Prepares the excel report file CreditPeriodAnalysisSheetReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				flage=1;
				if(flage==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:CreditPeriodAnalysisSheetReport.aspx,Class:InventoryClass.cs,Method:btnExcel_Click   Credit Period analysis Report Convert Into Excel Format, userid  "+uid);
				}
				else
				{
					MessageBox.Show(" Please Click the View Button First ");
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method return the month name till three charector with the help of passing value.
		/// </summary>
		/// <param name="mon"></param>
		/// <returns></returns>
		public string GetMonthName(string mon)
		{
			if(mon.IndexOf(" ")>0)
			{
				string[] month=mon.Split(new char[] {' '},mon.Length);
				if(month[0].ToString()=="Jan")
					return "Dec";
				else if(month[0].ToString()=="Feb")
					return "Jan";
				else if(month[0].ToString()=="Mar")
					return "Feb";
				else if(month[0].ToString()=="Apr")
					return "Mar";
				else if(month[0].ToString()=="May")
					return "Apr";
				else if(month[0].ToString()=="Jun")
					return "May";
				else if(month[0].ToString()=="Jul")
					return "Jun";
				else if(month[0].ToString()=="Aug")
					return "Jul";
				else if(month[0].ToString()=="Sep")
					return "Aug";
				else if(month[0].ToString()=="Oct")
					return "Sep";
				else if(month[0].ToString()=="Nov")
					return "Oct";
				else if(month[0].ToString()=="Dec")
					return "Nov";
			}
			return "";
		}

		/// <summary>
		/// This method return the month name till three charector from givan from date.
		/// </summary>
		/// <returns></returns>
		public string GetMonthName1()
		{
			string[] month=txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length);
			if(month[1].ToString()=="1" || month[1].ToString()=="01")
				return "Dec";
			else if(month[1].ToString()=="2" || month[1].ToString()=="02")
				return "Jan";
			else if(month[1].ToString()=="3" || month[1].ToString()=="03")
				return "Feb";
			else if(month[1].ToString()=="4" || month[1].ToString()=="04")
				return "Mar";
			else if(month[1].ToString()=="5" || month[1].ToString()=="05")
				return "Apr";
			else if(month[1].ToString()=="6" || month[1].ToString()=="06")
				return "May";
			else if(month[1].ToString()=="7" || month[1].ToString()=="07")
				return "Jun";
			else if(month[1].ToString()=="8" || month[1].ToString()=="08")
				return "Jul";
			else if(month[1].ToString()=="9" || month[1].ToString()=="09")
				return "Aug";
			else if(month[1].ToString()=="10")
				return "Sep";
			else if(month[1].ToString()=="11")
				return "Oct";
			else if(month[1].ToString()=="12")
				return "Nov";
			else
				return "";
		}

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value from dropdownlist 
		/// with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strBackOrderNo="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012
				strDistrict = "select distinct state from customer";
				//strState = "select distinct country from customer";
				strPlace = "select distinct city from customer";
				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012
				//Coment by vikas 01.10.09 strSSR = "select emp_name from employee where emp_id in(select ssr from customer)";
				strSSR = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";
				//Coment by vikas 01.10.09 string[] arrStr = {strState,strDistrict,strPlace,strSSR};
				//Coment by vikas 01.10.09 HtmlInputHidden[] arrCust = {tempState,tempDistrict,tempPlace,tempSSR};	
				//Coment by vikas 16.11.2012 string[] arrStr = {strDistrict,strPlace,strSSR};
				//Coment by vikas 16.11.2012 HtmlInputHidden[] arrCust = {tempDistrict,tempPlace,tempSSR};	
				string[] arrStr = {strDistrict,strPlace,strSSR,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempDistrict,tempPlace,tempSSR,tempGroup,tempSubGroup};	
				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//DropValue.Items.Add(rdr.GetValue(0).ToString());
							//tempCustName.Value+=rdr.GetValue(0).ToString()+",";
							if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString() !="")
								arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
						}
					}
					rdr.Close();
				}

				/**********Add by vikas 10.12.2012***********************/
				sql=" select distinct Order_id,bo_1,bo_2,bo_3 from ovd where item_qty > sale_qty ";
				rdr=obj.GetRecordSet(sql);
				while(rdr.Read())
				{
					if(rdr["Bo_3"].ToString()==null || rdr["Bo_3"].ToString()=="")
					{
						if(rdr["Bo_2"]==null || rdr["Bo_2"].ToString()=="")
						{
							if(rdr["Bo_1"].ToString()==null || rdr["Bo_1"].ToString()=="")
							{
							}
							else
							{
								if(rdr["Bo_1"].ToString()!=null && rdr["Bo_1"].ToString()!="")
								{
									tempBackOrderNo.Value+="BO:"+rdr["Bo_1"].ToString()+",";
								}
							}
						}
						else 
						{
							if(rdr["Bo_2"].ToString()!=null && rdr["Bo_2"].ToString()!="")
							{
								tempBackOrderNo.Value+="BO:"+rdr["Bo_2"].ToString()+",";
							}
						}
					}
					else 
					{
						if(rdr["Bo_3"].ToString()!=null && rdr["Bo_3"].ToString()!="")
						{
							tempBackOrderNo.Value+="BO:"+rdr["Bo_3"].ToString()+",";
						}
					}
				}
				rdr.Close ();
				/**********End***********************/
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method return the from date that should started from 1 of passing the date.
		/// </summary>
		/// <param name="FD"></param>
		/// <returns></returns>
		public string getFromDateInPrivMonth(string FD)
		{
			string[] FromDate = FD.IndexOf("/")>0?FD.Split(new char[] {'/'},FD.Length): FD.Split(new char[] { '-' }, FD.Length);
			FromDate[1]=System.Convert.ToString(int.Parse(FromDate[1])-1);
			return "1"+"/"+FromDate[1]+"/"+FromDate[2];
		}

		/// <summary>
		/// This method return the to date that should started from day in a month of passing the date.
		/// </summary>
		/// <param name="FD"></param>
		/// <returns></returns>
		public string getToDate(string FD)
		{
			string[] FromDate = FD.Split(new char[] {'/'},FD.Length);
			int day=DateTime.DaysInMonth(int.Parse(FromDate[2]),int.Parse(FromDate[1]));
			return day.ToString()+"/"+FromDate[1]+"/"+FromDate[2];
		}

		/// <summary>
		/// This method return the from date that should started from 1 of passing the date.
		/// </summary>
		/// <param name="FD"></param>
		/// <returns></returns>
		public string getFromDate(string FD)
		{
			string[] FromDate = FD.IndexOf("/")>0?FD.Split(new char[] {'/'},FD.Length): FD.Split(new char[] { '-' }, FD.Length);
			return "1"+"/"+FromDate[1]+"/"+FromDate[2];
		}
	}
}
