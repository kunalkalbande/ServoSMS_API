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

using System.Security.Permissions;
using System.Text;
using RMG;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for PriceList.
	/// </summary>
	public partial class PriceList : System.Web.UI.Page
	{
		
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.DropDownList DropSearchBy;
		string uid;
		public int ds20=0;

		public string[] DateFrom = null;
		public double[] TotalSum = null;
		public string[] DateTo = null;
		public int ds11=0;
		public int ds12=0;
		public int ds21=0;
		public int ds22=0;
		public int ds10=0;
		public static int count=0;
public static int View = 0;
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
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:pageload"+ ex.Message+ "  EXCEPTION" +uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				GridReport.Visible=false;
				GridCurrReport.Visible=false;
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="27";
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
				txtDateTo.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				GetMultiValue();
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
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
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6)
		{
			while(rdr.Read())
			{
				if(rdr["Prod_ID"].ToString().Trim().Length>len1)
					len1=rdr["Prod_ID"].ToString().Trim().Length;					
				if(rdr["Prod_Name"].ToString().Trim().Length>len2)
					len2=rdr["Prod_Name"].ToString().Trim().Length;					
				if(rdr["Pack_Type"].ToString().Trim().Length>len3)
					len3=rdr["Pack_Type"].ToString().Trim().Length;
				if(rdr["Pur_Rate"].ToString().Trim().Length>len4)
					len4=rdr["Pur_Rate"].ToString().Trim().Length;					
				if(rdr["Sal_Rate"].ToString().Trim().Length>len5)
					len5=rdr["Sal_Rate"].ToString().Trim().Length;					
				if(rdr["Eff_Date"].ToString().Trim().Length>len6)
					len6=rdr["Eff_Date"].ToString().Trim().Length;	
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
		/// This method is used to Prepares the excel report file PriceList.xls for printing.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			string path = home_drive+@"\Servosms_ExcelFile\Export\PriceList.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			//sql="select * from vw_PriceList where cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
			if(chkCurrPrice.Checked)
			{
				if(DropSearch.SelectedIndex==0 && DropValue.Value=="All")
					sql = "select * from Products";
				else if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					sql = "select * from products where Prod_Code='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
					sql = "select * from products where Prod_ID='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
				{
					string name = DropValue.Value;
					string[] arrname = name.Split(new char[] {':'},name.Length);
					sql = "select * from products where Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"'";
				}
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sql = "select * from products where Prod_Name='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sql = "select * from products where Pack_Type='"+DropValue.Value+"'";
				sql=sql+" order by "+""+Cache["strorderby"]+"";
				SqlDtr=obj.GetRecordSet(sql);
			}
			else
			{
				if(DropSearch.SelectedIndex==0)
					sql = "select * from vw_PriceList where cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==1)
					sql = "select * from vw_PriceList where Prod_Code='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==2)
					sql = "select * from vw_PriceList where Prod_ID='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==3)
				{
					string name = DropValue.Value;
					string[] arrname = name.Split(new char[] {':'},name.Length);
					sql = "select * from vw_PriceList where Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
					//sql = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				}
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sql = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sql = "select * from vw_PriceList where Pack_Type='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				sql=sql+" order by "+""+Cache["strorderby"]+"";
				SqlDtr=obj.GetRecordSet(sql);
			}
			sw.WriteLine("FromDate\t"+txtDateFrom.Text+"\tToDate\t"+txtDateTo.Text);
			sw.WriteLine("Search By\t"+DropSearch.SelectedItem.Text);
			sw.WriteLine("Search By Item\t"+DropValue.Value);
			sw.WriteLine("Product Code\tProduct ID\tProduct Name\tPack Type\tPurchase Rate\tSale Rate");
			if(chkCurrPrice.Checked)
			{
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						string Sal_Rate="", Pur_Rate="", Eff_Date="";
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr=null;
						rdr = obj1.GetRecordSet("select top 1 * from price_updation where prod_id='"+SqlDtr["Prod_ID"].ToString()+"' order by eff_date desc");
						if(rdr.Read())
						{
							Pur_Rate=rdr["Pur_Rate"].ToString();
							Sal_Rate=rdr["Sal_Rate"].ToString();
							Eff_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Eff_Date"].ToString()));
						}
						else
						{
							Sal_Rate="";
							Pur_Rate="";
							Eff_Date="";
						}
						rdr.Close();
						sw.WriteLine(SqlDtr["Prod_Code"].ToString()+"\t"+
							SqlDtr["Prod_ID"].ToString()+"\t"+
							SqlDtr["Prod_Name"].ToString()+"\t"+
							SqlDtr["Pack_Type"].ToString()+"\t"+
							Pur_Rate+"\t"+Sal_Rate+"\t"+Eff_Date);
					}
				}
			}
			else
			{
				while(SqlDtr.Read())
				{
					sw.WriteLine(SqlDtr["Prod_Code"].ToString()+"\t"+SqlDtr["Prod_ID"].ToString()+"\t"+SqlDtr["Prod_Name"].ToString()+"\t"+SqlDtr["Pack_Type"].ToString()+"\t"+SqlDtr["Pur_Rate"].ToString()+"\t"+SqlDtr["sal_Rate"].ToString());
				}
				sw.Close();
			}
		}

		public void ConvertToExcelNew()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			string path = home_drive+@"\Servosms_ExcelFile\Export\PriceList.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			//sql="select * from vw_PriceList where cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
			//	SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//string sqlstr="";
			object op= null;

			dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());

			if(DropSearch1.SelectedIndex==0)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate ";
			else if(DropSearch1.SelectedIndex==1)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			else if(DropSearch1.SelectedIndex==2)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			else if(DropSearch1.SelectedIndex==3)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";

			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				sql += " and a.Prod_Code='"+DropValue.Value+"' ";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and a.Prod_ID='"+DropValue.Value+"' ";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and a.Product='"+arrname[0].ToString()+" "+arrname[1].ToString()+"' ";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and a.Product like '"+DropValue.Value+"%' ";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and a.Pack_Type='"+DropValue.Value+"' ";

			sql=sql+" order by "+""+Cache["strorderby"]+"";
			SqlDtr=obj.GetRecordSet(sql);
			sw.WriteLine("FromDate\t"+txtDateFrom.Text+"\tToDate\t"+txtDateTo.Text);
			sw.WriteLine("Search By\t"+DropSearch.SelectedItem.Text);
			sw.WriteLine("Search By Item\t"+DropValue.Value);
			sw.WriteLine("Product Code\tProduct ID\tProduct Name\tPurchase Rate\tSale Rate\tEffective Date\tAvail Stcok\tAvg. Sale (Ltr.)");
			
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					double Sale=0,AvgSale=0;
					string Sal_Rate="", Pur_Rate="", Eff_Date="";
					InventoryClass obj1 = new InventoryClass();
					SqlDataReader rdr=null;
					rdr = obj1.GetRecordSet("select top 1 * from price_updation where prod_id='"+SqlDtr["Prod_ID"].ToString()+"' order by eff_date desc");
					if(rdr.Read())
					{
						Pur_Rate=rdr["Pur_Rate"].ToString();
						Sal_Rate=rdr["Sal_Rate"].ToString();
						Eff_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Eff_Date"].ToString()));
					}
					else
					{
						Sal_Rate="";
						Pur_Rate="";
						Eff_Date="";
					}
					rdr.Close();

					if(DropSearch1.SelectedIndex==2)
						rdr = obj1.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+SqlDtr["Prod_ID"].ToString()+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ DateFrom[0].ToString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ DateTo[0].ToString()+"'");
					else if(DropSearch1.SelectedIndex==3)
						rdr = obj1.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+SqlDtr["Prod_ID"].ToString()+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ DateFrom[0].ToString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ DateTo[0].ToString()+"'");
					else
						rdr = obj1.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+SqlDtr["Prod_ID"].ToString()+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text)  +"'");
					
					if(rdr.Read())
					{
						if(rdr["Ltr"].ToString()!="" && rdr["Ltr"].ToString()!=null)
							Sale=double.Parse(rdr["Ltr"].ToString());
					}
					else
					{
						Sale=0;
					}
					rdr.Close();
						
					if(DropSearch1.SelectedIndex==2)
						AvgSale=Sale/3;
					else if(DropSearch1.SelectedIndex==3)
						AvgSale=Sale/12;
					else
						AvgSale=Sale;

					AvgSale=Math.Round(AvgSale);
					Tot_AvgSale+=AvgSale;
					
					Tot_Stock+=Double.Parse(SqlDtr["Closing_Stock"].ToString());

					sw.WriteLine(SqlDtr["Prod_Code"].ToString()+"\t"+
						SqlDtr["Prod_ID"].ToString()+"\t"+
						SqlDtr["product"].ToString()+"\t"+
						Pur_Rate+"\t"+
						Sal_Rate+"\t"+
						Eff_Date+"\t"+
						SqlDtr["Closing_Stock"].ToString()+"\t"+
						AvgSale.ToString());
				}
			}
			sw.WriteLine("\tTotal\t\t\t\t\t"+Tot_Stock.ToString()+"\t"+Tot_AvgSale.ToString());
			sw.Close();
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		public void Move(string s,string d)
		{
			try 
			{
				Directory.Move(s, d);
			}
			catch (ArgumentNullException) 
			{
				MessageBox.Show("Path is a null reference.");
			}
			catch (System.Security.SecurityException) 
			{
				MessageBox.Show("The caller does not have the " +
					"required permission.");
			}
			catch (ArgumentException) 
			{
				MessageBox.Show("Path is an empty string, " +
					"contains only white spaces, " + 
					"or contains invalid characters.");	
			}
			catch (System.IO.IOException) 
			{
				MessageBox.Show("An attempt was made to move a " +
					"directory to a different " +
					"volume, or destDirName " +
					"already exists."); 
			}
		}

		/// <summary>
		/// Method to prepare the report file .txt to print
		/// </summary>
		public void makingReport()
		{
			/*
										======================                              
										   PRICE LIST REPORT                                 
										======================                              
+-------+----------------------+-----------+-----------+-----------+----------+
	  |Prod_ID|  Product Name           | Pack_Type | Pur_Rate  | Sal_Rate  |Eff_Date  |
	  +-------+-------------------------+-----------+-----------+-----------+----------+
	   1001    1234567890123456789012345 1X20777     12345678.00 12345678.00 DD/MM/YYYY
			*/
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PriceListReport.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			string strDate = "";

			//sql="select * from vw_PriceList order by Prod_id";
			//sql="select * from vw_PriceList where cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
			if(chkCurrPrice.Checked)
			{
				if(DropSearch.SelectedIndex==0 && DropValue.Value=="All")
					sql = "select * from Products";
				else if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					sql = "select * from Products where Prod_Code='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
					sql = "select * from Products where Prod_ID='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
				{
					string name = DropValue.Value;
					string[] arrname = name.Split(new char[] {':'},name.Length);
					sql = "select * from Products where Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"'";
				}
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sql = "select * from Products where Prod_Name='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sql = "select * from Products where Pack_Type='"+DropValue.Value+"'";
				sql=sql+" order by "+""+Cache["strorderby"]+"";
				dbobj.SelectQuery(sql,ref rdr);
			}
			else
			{
				if(DropSearch.SelectedIndex==0)
					sql = "select * from vw_PriceList where cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==1)
					sql = "select * from vw_PriceList where Prod_Code='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==2)
					sql = "select * from vw_PriceList where Prod_ID='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==3)
				{
					string name = DropValue.Value;
					string[] arrname = name.Split(new char[] {':'},name.Length);
					sql = "select * from vw_PriceList where Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
					//sql = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				}
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sql = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sql = "select * from vw_PriceList where Pack_Type='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"'";
				sql=sql+" order by "+""+Cache["strorderby"]+"";
				dbobj.SelectQuery(sql,ref rdr);
			}
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
			string des="---------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=========================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("PRICE LIST REPORT From date "+txtDateFrom.Text+" To Date "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=========================================================",des.Length));
			sw.WriteLine("Search By : "+DropSearch.SelectedItem.Text+", Search By Item : "+DropValue.Value);
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+-----------+----------+");
			sw.WriteLine("|Product Code|Prod.ID|      Product Name            | Pack.Type | Pur.Rate  |Sales Rate |Eff. Date |");
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+-----------+----------+");
			//             123456789012 1001567 12345678901234567890123 12345678901 12345678.00 12345678.00 DD/MM/YYYY
			// info : to set the format the displaying string.
			info = " {0,-12:S} {1,-7:S} {2,-30:S} {3,-11:S} {4,11:F} {5,11:F} {6,-10:S}"; 
			if(chkCurrPrice.Checked)
			{
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						string Sal_Rate="", Pur_Rate="", Eff_Date="";
						InventoryClass obj = new InventoryClass();
						SqlDataReader rdr1=null;
						rdr1 = obj.GetRecordSet("select top 1 * from price_updation where prod_id='"+rdr["Prod_ID"].ToString()+"' order by eff_date desc");
						if(rdr1.Read())
						{
							Pur_Rate=rdr1["Pur_Rate"].ToString();
							Sal_Rate=rdr1["Sal_Rate"].ToString();
							Eff_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr1["Eff_Date"].ToString()));
						}
						else
						{
							Sal_Rate="";
							Pur_Rate="";
							Eff_Date="";
						}
						rdr1.Close();
						sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),rdr["Prod_ID"].ToString().Trim(),GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),30),rdr["Pack_Type"].ToString(),Pur_Rate,Sal_Rate,Eff_Date);
					}
				}
			}
			else
			{
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						strDate = rdr["Eff_Date"].ToString().Trim();
						int pos = strDate.IndexOf(" ");
				
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}
						sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),
							rdr["Prod_ID"].ToString().Trim(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),30),
							rdr["Pack_Type"].ToString(),
							GenUtil.strNumericFormat(rdr["Pur_Rate"].ToString().Trim()),
							GenUtil.strNumericFormat(rdr["sal_Rate"].ToString().Trim()),
							GenUtil.str2DDMMYYYY(strDate));

					}
				}
			}
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+-----------+----------+");
			dbobj.Dispose();
			sw.Close();
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


		public void makingReportNew()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PriceListReport.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			string strDate = "";
			object op= null;

			dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());

			if(DropSearch1.SelectedIndex==0)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate ";
			else if(DropSearch1.SelectedIndex==1)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			else if(DropSearch1.SelectedIndex==2)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			else if(DropSearch1.SelectedIndex==3)
				sql = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";

			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				sql += " and a.Prod_Code='"+DropValue.Value+"' ";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and a.Prod_ID='"+DropValue.Value+"' ";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and a.Product='"+arrname[0].ToString()+" "+arrname[1].ToString()+"' ";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and a.Product like '"+DropValue.Value+"%' ";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and a.Pack_Type='"+DropValue.Value+"' ";
			
			sql=sql+" order by "+""+Cache["strorderby"]+"";
			dbobj.SelectQuery(sql,ref rdr);
			
			sw.Write((char)27);	//added by vishnu
			sw.Write((char)67);	//added by vishnu
			sw.Write((char)0);	//added by vishnu
			sw.Write((char)12);	//added by vishnu
			
			sw.Write((char)27);	//added by vishnu
			sw.Write((char)78);	//added by vishnu
			sw.Write((char)5);	//added by vishnu
							
			sw.Write((char)27);	//added by vishnu
			sw.Write((char)15);
			//**********
			string des="--------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=========================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("PRICE LIST REPORT From date "+txtDateFrom.Text+" To Date "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=========================================================",des.Length));
			//sw.WriteLine("Search By : "+DropSearch.SelectedItem.Text+", Search By Item : "+DropValue.Value);
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+----------+-----------+--------------+");
			sw.WriteLine("|Product Code|Prod.ID|      Product Name            | Pur.Rate  |Sales Rate |Eff. Date |Avil. Stock|Avg. Sale(Ltr)|");
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+----------+-----------+--------------+");
			//             123456789012 1001567 123456789012345678901234567890 12345678901 12345678901 12345678901 1234567890 12345678901 12345678901234
			info = " {0,-12:S} {1,-7:S} {2,-30:S} {3,-11:S} {4,11:F} {5,10:F} {6,11:S} {7,14:F} "; 
			
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					double Sale=0,AvgSale=0;
					string Sal_Rate="", Pur_Rate="", Eff_Date="";
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr1=null;
					rdr1 = obj.GetRecordSet("select top 1 * from price_updation where prod_id='"+rdr["Prod_ID"].ToString()+"' order by eff_date desc");
					if(rdr1.Read())
					{
						Pur_Rate=rdr1["Pur_Rate"].ToString();
						Sal_Rate=rdr1["Sal_Rate"].ToString();
						Eff_Date=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr1["Eff_Date"].ToString()));
					}
					else
					{
						Sal_Rate="";
						Pur_Rate="";
						Eff_Date="";
					}
					rdr1.Close();

					if(DropSearch1.SelectedIndex==2)
						rdr1 = obj.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+rdr["Prod_ID"].ToString()+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ DateFrom[0].ToString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ DateTo[0].ToString()+"'");
					else if(DropSearch1.SelectedIndex==3)
						rdr1 = obj.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+rdr["Prod_ID"].ToString()+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ DateFrom[0].ToString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ DateTo[0].ToString()+"'");
					else
						rdr1 = obj.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+rdr["Prod_ID"].ToString()+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text)  +"'");
					
					if(rdr1.Read())
					{
						if(rdr1["Ltr"].ToString()!="" && rdr1["Ltr"].ToString()!=null)
							Sale=double.Parse(rdr1["Ltr"].ToString());
					}
					else
					{
						Sale=0;
					}
					rdr1.Close();
						
					if(DropSearch1.SelectedIndex==2)
						AvgSale=Sale/3;
					else if(DropSearch1.SelectedIndex==3)
						AvgSale=Sale/12;
					else
						AvgSale=Sale;

					AvgSale=Math.Round(AvgSale);
					Tot_AvgSale+=AvgSale;
					
					Tot_Stock+=Double.Parse(rdr["Closing_Stock"].ToString());

					sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),rdr["Prod_ID"].ToString().Trim(),GenUtil.TrimLength(rdr["Product"].ToString().Trim(),30),Pur_Rate,Sal_Rate,Eff_Date,rdr["Closing_Stock"].ToString(),AvgSale.ToString());
				}
			}
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+----------+-----------+--------------+");
			sw.WriteLine(info,"","","","","","",Tot_Stock.ToString(),Tot_AvgSale.ToString());
			sw.WriteLine("+------------+-------+------------------------------+-----------+-----------+----------+-----------+--------------+");
			dbobj.Dispose();
			sw.Close();
		}


		/// <summary>
		/// Prepares the report file PriceList.txt for printing.
		/// </summary>
		protected void Btnprint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				//if(DropType.SelectedIndex==0)
				//	makingReport();
				//else
				//{
					string s1="";
					string s2="";
					string cust_name="";
					s1=txtDateTo.Text;
					s2=txtDateFrom.Text;
					string[] ds1 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
					string[] ds2 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
					ds10=System.Convert.ToInt32(ds1[0]);
					ds20=System.Convert.ToInt32(ds2[0]);
					ds11=System.Convert.ToInt32(ds1[1]);
					ds12=System.Convert.ToInt32(ds1[2]);
					ds21=System.Convert.ToInt32(ds2[1]);
					ds22=System.Convert.ToInt32(ds2[2]);

					if(DropSearch1.SelectedIndex==2)
						getDateThreeMonth(ds10,ds11,ds12,ds20,ds21,ds22);
					else if(DropSearch1.SelectedIndex==3)
						getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22);
					makingReportNew();
				//}
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
					CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Price List Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PriceListReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Price List Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Price List Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Price List Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Price List Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}

		}

		private void GridReport_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
		
		string strorderby="";
		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of the datagridheader.
		/// </summary>
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
				//if(DropType.SelectedIndex==0)
				//{
				//	strorderby="Prod_ID ASC";
				//	Session["Column"]="Prod_ID";
				//	Session["order"]="ASC";
				//	Bindthedata();
				//}
				//else
				//{
					//strorderby="Prod_Code ASC";
					//Session["Column"]="Prod_Code";
					//Session["order"]="ASC";
					Bindthedata_New();
				//}
				//Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PricelistReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}
		
		/// <summary>
		/// This method is used to binding the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string sqlstr="";
			if(chkCurrPrice.Checked)
			{
				if(DropSearch.SelectedIndex==0 && DropValue.Value=="All")
					sqlstr = "select * from Products order by Prod_Name";
				else if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					sqlstr = "select * from products where Prod_Code='"+DropValue.Value+"' order by Prod_Code";
				else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
					sqlstr = "select * from products where Prod_ID='"+DropValue.Value+"' order by Prod_ID";
				else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
				{
					string name = DropValue.Value;
					string[] arrname = name.Split(new char[] {':'},name.Length);
					sqlstr = "select * from products where Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"' order by Prod_Name";
					//sqlstr = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_Name";
				}
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sqlstr = "select * from products where Prod_Name='"+DropValue.Value+"' order by Prod_Name";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sqlstr = "select * from products where Pack_Type='"+DropValue.Value+"' order by Pack_Type";
				else
				{
					MessageBox.Show("Please Select The Right Option");
					return;
				}
				SqlDataAdapter da=new SqlDataAdapter(sqlstr,sqlcon);
				DataSet ds=new DataSet();	
				da.Fill(ds,"Products");
				DataTable dtcustomer=ds.Tables["Products"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridCurrReport.DataSource=dv;
				if(dv.Count!=0)
				{
					GridCurrReport.DataBind();
					GridCurrReport.Visible=true;
					GridReport.Visible=false;
				}
				else
				{
					GridReport.Visible=false;
					GridCurrReport.Visible=false;
					//panOld.Visible=true;
					//panNew.Visible=false;
					MessageBox.Show("Data Not Available");
				}
			}
			else
			{
				if(DropSearch.SelectedIndex==0 && DropValue.Value=="All")
					sqlstr = "select * from vw_PriceList where cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_id";
				else if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					sqlstr = "select * from vw_PriceList where Prod_Code='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_Code";
				else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
					sqlstr = "select * from vw_PriceList where Prod_ID='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_ID";
				else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
				{
					string name = DropValue.Value;
					string[] arrname = name.Split(new char[] {':'},name.Length);
					sqlstr = "select * from vw_PriceList where Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_Name";
					//sqlstr = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_Name";
				}
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sqlstr = "select * from vw_PriceList where Prod_Name='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_Name";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sqlstr = "select * from vw_PriceList where Pack_Type='"+DropValue.Value+"' and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Pack_Type";
				else
				{
					MessageBox.Show("Please Select The Right Option");
					return;
				}
				SqlDataAdapter da=new SqlDataAdapter(sqlstr,sqlcon);
				DataSet ds=new DataSet();	
				da.Fill(ds,"vw_PriceList");
				DataTable dtcustomer=ds.Tables["vw_PriceList"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridReport.DataSource=dv;
				if(dv.Count!=0)
				{
					GridReport.DataBind();
					GridReport.Visible=true;
					GridCurrReport.Visible=false;
				}
				else
				{
					GridReport.Visible=false;
					GridCurrReport.Visible=false;
					MessageBox.Show("Data Not Available");
				}
			}
			sqlcon.Dispose();
		}

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

		public void Bindthedata_New()
		{
			string s1="";
			string s2="";
			string cust_name="";
			s1=txtDateTo.Text;
			s2=txtDateFrom.Text;
			string[] ds1 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
			string[] ds2 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
			ds10=System.Convert.ToInt32(ds1[0]);
			ds20=System.Convert.ToInt32(ds2[0]);
			ds11=System.Convert.ToInt32(ds1[1]);
			ds12=System.Convert.ToInt32(ds1[2]);
			ds21=System.Convert.ToInt32(ds2[1]);
			ds22=System.Convert.ToInt32(ds2[2]);
			if(DropSearch1.SelectedIndex==2)
				getDateThreeMonth(ds10,ds11,ds12,ds20,ds21,ds22);
			else if(DropSearch1.SelectedIndex==3)
				getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22);

			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string sqlstr="";
			object op= null;

			dbobj.ExecProc(OprType.Insert,"sp_stock",ref op,"@fromdate",System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString());

			if(DropSearch1.SelectedIndex==0)
				//sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate order by Prod_id";
				sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate ";
			else if(DropSearch1.SelectedIndex==1)
				//sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0  and cast(floor(cast(Eff_Date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text) +"' order by Prod_id";
				//sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 order by Prod_id";
				sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			else if(DropSearch1.SelectedIndex==2)
				//sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 order by Prod_id";
				sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			else if(DropSearch1.SelectedIndex==3)
				//sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 order by Prod_id";
				sqlstr = "select distinct a.product, a.pack_type, a.store_in, a.closing_stock, a.category, a.totalqty,a.Prod_Code,a.mrp,a.prod_id,(select top 1 pur_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Pur_Rate,(select top 1 Sal_Rate from price_updation where prod_id=a.prod_id order by eff_date desc) Sal_Rate,(select top 1 eff_date from price_updation where prod_id=a.prod_id order by eff_date desc) eff_date from vw_stockreport a , stk b, vw_PriceList c where c.prod_id=a.prod_id and a.product=b.product and a.stock_date=b.sdate and closing_Stock!=0 ";
			
			/***************************/

			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				sqlstr += " and a.Prod_Code='"+DropValue.Value+"' ";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sqlstr += " and a.Prod_ID='"+DropValue.Value+"' ";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				//sqlstr += " and a.Prod_Name='"+arrname[0].ToString()+"' and Pack_Type='"+arrname[1].ToString()+"' order by Prod_Name";
				sqlstr += " and a.Product='"+arrname[0].ToString()+" "+arrname[1].ToString()+"' ";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sqlstr += " and a.Product like '"+DropValue.Value+"%' ";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sqlstr += " and a.Pack_Type='"+DropValue.Value+"' ";

			//sqlstr +="order by Prod_Code";
			/***************************/
			SqlDataAdapter da=new SqlDataAdapter(sqlstr,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"Products");
			DataTable dtcustomer=ds.Tables["Products"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridNew.DataSource=dv;
			if(dv.Count!=0)
			{
				GridNew.DataBind();
				GridNew.Visible=true;
				GridReport.Visible=false;
				GridCurrReport.Visible=false;
			}
			else
			{
				GridCurrReport.Visible=false;
				GridReport.Visible=false;
				GridNew.Visible=false;
				//panOld.Visible=false;
				//panNew.Visible=true;
				MessageBox.Show("Data Not Available");
			}
			
		}
		
		/// <summary>
		/// This method is used to view the report withe the help of Bindthedata() function and set the 
		/// column name with ascending order in session variable.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Button1_Click(object sender, System.EventArgs e)
		{ 
			try
			{

				PetrolPumpClass  obj=new PetrolPumpClass();
				//SqlDataReader SqlDtr;
				//string sql;

				#region Bind DataGrid

				//if(DropType.SelectedIndex==0)
				//{
				//	strorderby="Prod_ID ASC";
				//	Session["Column"]="Prod_ID";
				//	Session["order"]="ASC";
				//	Bindthedata();
				//}
				//else
				//{
					strorderby="Prod_Code ASC";
					Session["Column"]="Prod_Code";
					Session["order"]="ASC";
					Bindthedata_New();
				//}
				#endregion
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:Button1_Click    Price List Report Viewed   "+  " userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:Button1_Click    Price List Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public void getDateThreeMonth(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			DateFrom = new string[1];
			DateTo = new string[1];
				
			if(From2==1 || From2==2)
			{
				if(From2==1)
				{
					From2=10;
				}
				else
				{
					From2=11;
				}
				From3--;
				DateFrom[0]=From2.ToString()+"/1/"+From3.ToString();
			}
			else
			{
				From2=From2-2;
				DateFrom[0]=From2.ToString()+"/1/"+From3.ToString();
			}

			To1=DateTime.DaysInMonth(To3,To2);
			DateTo[0]=To2+"/"+To1.ToString()+"/"+To3.ToString();
		}

		public void getDateLastYear(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			DateFrom = new string[1];
			DateTo = new string[1];

			DateFrom = new string[count+1];
			DateTo = new string[count+1];
			From3--;
			DateFrom[0]=From2.ToString()+"/1/"+From3.ToString();
			To1=DateTime.DaysInMonth(To3,To2);
			DateTo[0]=To2+"/"+To1.ToString()+"/"+To3.ToString();
		}

		/// <summary>
		/// This method is used to prepares the excel report file PriceList.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//if(DropType.SelectedIndex==0)
				//{
				//	if(GridReport.Visible==true || GridCurrReport.Visible==true)
				//	{
					
				//		ConvertToExcel();
				//		MessageBox.Show("Successfully Convert File Into Excel Format");
				//		CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Price List Report Convert Into Excel Format, userid  "+uid);
				
				//	}
				//	else
				//	{
				//		MessageBox.Show("Please Click the View Button First");
				//		return;
				//	}
				//}
				//else
				//{
					string s1="";
					string s2="";
					string cust_name="";
					s1=txtDateTo.Text;
					s2=txtDateFrom.Text;
					string[] ds1 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
					string[] ds2 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
					ds10=System.Convert.ToInt32(ds1[0]);
					ds20=System.Convert.ToInt32(ds2[0]);
					ds11=System.Convert.ToInt32(ds1[1]);
					ds12=System.Convert.ToInt32(ds1[2]);
					ds21=System.Convert.ToInt32(ds2[1]);
					ds22=System.Convert.ToInt32(ds2[2]);

					if(DropSearch1.SelectedIndex==2)
						getDateThreeMonth(ds10,ds11,ds12,ds20,ds21,ds22);
					else if(DropSearch1.SelectedIndex==3)
						getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22);

				    ConvertToExcelNew();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Price List Report Convert Into Excel Format, userid  "+uid);
				//}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Price List Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		protected void DropSearch_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//			try
			//			{
			//				if(DropSearch.SelectedIndex!=0)
			//				{
			//					InventoryClass obj = new InventoryClass();
			//					SqlDataReader rdr = null;
			//					string str = "";
			//					if(DropSearch.SelectedIndex==1)
			//						str = "select distinct Prod_Code from vw_PriceList order by Prod_Code";
			//					else if(DropSearch.SelectedIndex==2)
			//						str = "select distinct Prod_ID from vw_PriceList order by Prod_ID";
			//					else if(DropSearch.SelectedIndex==3)
			//						str = "select distinct Prod_Name,Pack_Type from vw_PriceList order by Prod_Name";
			//					rdr = obj.GetRecordSet(str);
			//					DropSearchBy.Items.Clear();
			//					DropSearchBy.Items.Add("Select");
			//					if(DropSearch.SelectedIndex!=3)
			//					{
			//						DropSearchBy.Width=130;
			//						while(rdr.Read())
			//						{
			//							DropSearchBy.Items.Add(rdr.GetValue(0).ToString());
			//						}
			//						rdr.Close();
			//					}
			//					else
			//					{
			//						DropSearchBy.Width=356;
			//						while(rdr.Read())
			//						{
			//							DropSearchBy.Items.Add(rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString());
			//						}
			//						rdr.Close();
			//					}
			////					else if(DropSearch.SelectedIndex==3)
			////					{
			////						DropSearchBy.Width=350;
			////						while(rdr.Read())
			////						{
			////							DropSearchBy.Items.Add(rdr["Prod_Name"].ToString()+":"+rdr["Pack_Type"].ToString());
			////						}
			////						rdr.Close();
			////					}
			//				
			//				}
			//				else
			//				{
			//					DropSearchBy.Width=111;
			//					DropSearchBy.Items.Clear();
			//					DropSearchBy.Items.Add("All");
			//				}
			//				GridReport.Visible=false;
			//			}
			//			catch(Exception ex)
			//			{
			//				MessageBox.Show(ex.Message);
			//			}
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
				string strProdCode="",strProdID="",strProdName="",strProdWithPack="",strPackType="";
				strProdCode = "select distinct Prod_Code from vw_PriceList order by Prod_Code";
				strProdID = "select distinct Prod_ID from vw_PriceList order by Prod_ID";
				strProdName = "select distinct Prod_Name+':'+Pack_Type as Prod_Name from vw_PriceList order by Prod_Name";
				strProdWithPack= "select distinct Prod_Name from vw_PriceList order by Prod_Name";
				strPackType= "select distinct Pack_Type from vw_PriceList order by Pack_Type";
				string[] arrStr = {strProdCode,strProdID,strProdName,strProdWithPack,strPackType};
				HtmlInputHidden[] arrCust = {tempProdCode,tempProdID,tempProdName,tempProdWithName,tempPackType};
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
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()   Price List Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public string GetPurchaseRate(string ProdID)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=null;
			string sql="select top 1 * from price_updation where prod_id='"+ProdID+"' order by eff_date desc";
			rdr = obj.GetRecordSet(sql);
			if(rdr.Read())
			{
				return rdr["Pur_Rate"].ToString();
			}
			else
			{
				return "";
			}
			//rdr.Close();
		}

		public string GetSellingRate(string ProdID)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=null;
			string sql="select top 1 Sal_Rate from price_updation where prod_id='"+ProdID+"' order by eff_date desc";
			rdr = obj.GetRecordSet(sql);
			if(rdr.Read())
			{
				return rdr["Sal_Rate"].ToString();
			}
			else
			{
				return "";
			}
			//rdr.Close();
		}

		public string GetEffDate(string ProdID)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr=null;
			string sql="select top 1 Eff_Date from price_updation where prod_id='"+ProdID+"' order by eff_date desc";
			rdr = obj.GetRecordSet(sql);
			if(rdr.Read())
			{
				return rdr["Eff_Date"].ToString();
			}
			else
			{
				return "";
			}
			//rdr.Close();
		}

		
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemDataBound(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr=null;
					rdr = obj.GetRecordSet("select top 1 * from price_updation where prod_id='"+e.Item.Cells[1].Text+"' order by eff_date desc");
					if(rdr.Read())
					{
						e.Item.Cells[4].Text=rdr["Pur_Rate"].ToString();
						e.Item.Cells[5].Text=rdr["Sal_Rate"].ToString();
						e.Item.Cells[6].Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Eff_Date"].ToString()));
					}
					else
					{
						e.Item.Cells[4].Text="";
						e.Item.Cells[5].Text="";
						e.Item.Cells[6].Text="";
					}
					rdr.Close();
				}

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Method:ItemDataBound()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}
		
		public double Tot_Stock=0,Tot_AvgSale=0;
		public void ItemDataBoundNew(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				/*if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr=null;
					rdr = obj.GetRecordSet("select top 1 * from price_updation where prod_id='"+e.Item.Cells[1].Text+"' order by eff_date desc");
					if(rdr.Read())
					{
						e.Item.Cells[3].Text=rdr["Pur_Rate"].ToString();
						e.Item.Cells[4].Text=rdr["Sal_Rate"].ToString();
						e.Item.Cells[5].Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Eff_Date"].ToString()));
					}
					else
					{
						e.Item.Cells[3].Text="";
						e.Item.Cells[4].Text="";
						e.Item.Cells[5].Text="";
					}
					rdr.Close();
				}*/

				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr=null;
					double Sale=0,AvgSale=0;
					
					if(DropSearch1.SelectedIndex==2)
						rdr = obj.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+e.Item.Cells[1].Text+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ DateFrom[0].ToString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ DateTo[0].ToString()+"'");
					else if(DropSearch1.SelectedIndex==3)
						rdr = obj.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+e.Item.Cells[1].Text+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ DateFrom[0].ToString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ DateTo[0].ToString()+"'");
					else
						rdr = obj.GetRecordSet("select sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(p.total_qty as float)) as Ltr from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and sd.prod_id="+e.Item.Cells[1].Text+" and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text)  +"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(txtDateTo.Text)  +"'");
					
					if(rdr.Read())
					{
						if(rdr["Ltr"].ToString()!="" && rdr["Ltr"].ToString()!=null)
							Sale=double.Parse(rdr["Ltr"].ToString());
					}
					else
					{
						Sale=0;
					}
					rdr.Close();
						
					if(DropSearch1.SelectedIndex==2)
						AvgSale=Sale/3;
					else if(DropSearch1.SelectedIndex==3)
						AvgSale=Sale/12;
					else
						AvgSale=Sale;

					AvgSale=Math.Round(AvgSale);
					e.Item.Cells[7].Text=AvgSale.ToString();
					
					Tot_AvgSale+=AvgSale;
					
					Tot_Stock+=Double.Parse(e.Item.Cells[6].Text.ToString());

				}

				if((e.Item.ItemType == ListItemType.Footer) || (e.Item.ItemType == ListItemType.Footer) || (e.Item.ItemType == ListItemType.Footer)  )
				{
					e.Item.Cells[7].Text=Tot_AvgSale.ToString();
					e.Item.Cells[6].Text=Tot_Stock.ToString();
				}

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PriceList.aspx,Method:ItemDataBound()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		//private void DropType_SelectedIndexChanged(object sender, System.EventArgs e)
		//{
			//if(DropType.SelectedIndex==0)
			//{
			//	panOld.Visible=true;
			//	panNew.Visible=false;
			//}
			//else
			//{
			//	panNew.Visible=true;
			//	panOld.Visible=false;
			//}
		//}
	}
}