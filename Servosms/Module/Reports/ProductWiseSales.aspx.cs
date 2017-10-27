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
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using Servosms.Sysitem.Classes ;
using System.IO;
using System.Text;
using RMG;


namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for ProductWiseSales.
	/// </summary>
	public partial class ProductWiseSales : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";

		public int i=1;

		
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
				CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				grdLegAmount.Visible=false;
				grdLegNonAmount.Visible=false;
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="31";
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
				txtDateTo.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				GetMultiValue();
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		/// <summary>
		/// This method is used to create  the report file ProductWiseReport.txt to print.
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="";
			string info= "";
			string strPack = "";
			string strSales = "";
			string[] strParts;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ProductWiseReport.txt";
			StreamWriter sw = new StreamWriter(path);		
		   		
			/*************************************/
			ArrayList PackVal = new ArrayList(); 
			int last_index=0;
			if(chkChild.Checked==true)
			{
				
				string Spc_Pack=Session["Spc_Packs"].ToString();
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
			/*************************************/

			if(!RadPurchase.Checked)
			{
				if(chkChild.Checked==true)
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
				}
				else
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"'";
				}
				if(DropValue.Value!="All")
				{
					/********Add by vikas 16.11.2012***************/
					if(DropSearchBy.SelectedIndex==1)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"')";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"')";
					}/********End***************/
					else if(DropSearchBy.SelectedIndex==3)
					{
						string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
						sql+=" and p.Pack_Type='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==6)
						sql+=" and p.Category='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==7)																	   //Add by vikas 17.07.09
						sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";     //Add by vikas 17.07.09
				}
				/*if(DropParty.Value!="All")
				{
					//coment by vikas 25.05.09 sql+=" and cust_id=(select cust_id from customer where cust_name='"+DropParty.Value+"')";
					string cust_name="";
					cust_name=DropParty.Value.Substring(0,DropParty.Value.IndexOf(":"));
					sql+=" and cust_id=(select cust_id from customer where cust_name='"+cust_name.ToString()+"')";
				}
				if(DropType.Value!="All")
				{
					sql+=" and cust_id in(select cust_id from customer where cust_type like'"+DropType.Value+"%')";
				}*/
				sql+=" group by p.Prod_Name, p.Pack_Type,Prod_Code";
			}
			else
			{
				if(chkChild.Checked==true)
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
				}
				else
				{
					//sql="select  p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,sum(cast(sd.qty as float)) as Sales,sd.Prod_id,Total_Qty from Products p,Sales_Details sd,sales_master sm,purchase_master pm,purchase_details pd where p.Prod_ID = sd.Prod_ID and p.Prod_ID = pd.Prod_ID and pm.invoice_no=pd.invoice_no and sm.invoice_no=sd.invoice_no and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' and cast(floor(cast(pm.Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(pm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' group by p.Prod_Name, p.Pack_Type,sd.Prod_id,Total_Qty order by Prod_Name,Pack_Type";
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
				}
				//sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
				if(DropValue.Value!="All")
				{
					if(DropSearchBy.SelectedIndex==3)
					{
						string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
						sql+=" and p.Pack_Type='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==6)
						sql+=" and p.Category='"+DropValue.Value+"'";

					sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code";
				}
				else
				{
					if(chkChild.Checked==true)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
					else
					{
						sql="";
						//20.06.09 sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,sum(cast(sd.qty as float)) as Sales,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select distinct p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Total_Qty,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id,Total_Qty";
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
				}
				
				//20.06.09 sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code";
			}
			sql=sql+" order by "+Cache["strorderby"];
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
			if(RadPurchase.Checked)
				des="--------------------------------------------------------------------------------------------------------";
			else if(RadAmount.Checked)
				des="-------------------------------------------------------------------------------------------";
			else
				des="-----------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("PRODUCT SALES REPORT From "+txtDateFrom.Text.ToString()+" To "+txtDateTo.Text.ToString(),des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=================================================",des.Length));
			if(RadPurchase.Checked)
			{
				sw.WriteLine("+----------+----------------------------------------+-----------------------+--------------------------+");
				sw.WriteLine("| Product  |                                        |      Sales.Type       |      Purchase Type       |");
				sw.WriteLine("| Code     |              Product Name              +-----------+-----------+------------+-------------+");
				sw.WriteLine("|          |                                        |    Pkg    |  Ltr/Kg   |    Pkg     |   Ltr/Kg    |");
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+------------+-------------+");
				//             1234567890 1234567890123456789012345678901234567890 12345678901 12345678901 123456789012 1234567890123	
			}
			else if(RadAmount.Checked)
			{
				sw.WriteLine("+----------+----------------------------------------+-----------------------+-------------+");
				sw.WriteLine("| Product  |                                        |      Sales.Type       |             | ");
				sw.WriteLine("| Code     |              Product Name              |-----------+-----------|   Amount    |");
				sw.WriteLine("|          |                                        |    Pkg    |  Ltr/Kg   |             |");
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+-------------+");
				//             1234567890 1234567890123456789012345678901234567890 12345678901 12345678901 1234567890123	
			}
			else
			{
				sw.WriteLine("+----------+----------------------------------------+-----------------------+");
				sw.WriteLine("| Product  |                                        |      Sales.Type       |");
				sw.WriteLine("| Code     |              Product Name              |-----------+-----------|");
				sw.WriteLine("|          |                                        |    Pkg    |  Ltr/Kg   |");
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+");
				//             1234567890 1234567890123456789012345678901234567890 12345678901 12345678901 
			}
			//double total_pkg=0,total_ltr=0;
			if(rdr.HasRows)
			{
				// info : to set string format.
				if(RadPurchase.Checked)
					info = " {0,-10:S} {1,-40:S} {2,11:S} {3,11:S} {4,12:F} {5,13:F}";
				else if(RadAmount.Checked)
					info = " {0,-10:S} {1,-40:S} {2,11:S} {3,11:S} {4,13:F}";
				else
					info = " {0,-10:S} {1,-40:S} {2,11:S} {3,11:S}";
				
				while(rdr.Read())
				{

					if(RadPurchase.Checked)
					{
						double Totqty=0;
						
						string[] tot_qty=rdr["pack_type"].ToString().Split(new char[]{'X'});
			
						Totqty=Convert.ToDouble(tot_qty[0].ToString())*Convert.ToDouble(tot_qty[1].ToString());

						strPack="0";
						strSales="0";
						SqlDataReader rdr1 = null;
						dbobj.SelectQuery("select sum(cast(qty as float)) as Sale from Sales_master sm,Sales_details sd where sm.invoice_no=sd.invoice_no and prod_id='"+rdr["Prod_ID"].ToString()+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						if(rdr1.Read())
						{
							if(rdr1["Sale"].ToString()!="" && rdr1["Sale"].ToString()!=null)
								strPack=rdr1["Sale"].ToString();
						}
						rdr1.Close();
						if(rdr["Pack_Type"].ToString()!="")
							//20.06.09 strSales=System.Convert.ToString(double.Parse(strPack)*double.Parse(rdr["Total_Qty"].ToString()));
							strSales=System.Convert.ToString(double.Parse(strPack)*double.Parse(Totqty.ToString()));
							
						
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),40),
							//Check2(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString()), //add by vikas 20.06.09 
							//Multiply22(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString()), //add by vikas 20.06.09
							strPack,
							strSales,
							Check1(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString()),
							//Coment by vikas 20.06.09 Multiply1(rdr["Total_Qty"].ToString(),rdr["Prod_ID"].ToString())
							Multiply11(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString())
							);
					}
					else if(RadAmount.Checked)
					{
						strSales = rdr["Sales"].ToString().Trim();
						if(rdr["Pack_Type"].ToString().Trim().Equals("") || rdr["Pack_Type"].ToString().Trim().IndexOf("Loose") > -1  )
						{
							strPack  = strSales;  
							strSales = "";
						}
						else
						{            
							strParts = rdr["Pack_Type"].ToString().Trim().Split(new char[] {'X'},rdr["Pack_Type"].ToString().Trim().Length);
							double tot = System.Convert.ToDouble(strParts[0]) *  System.Convert.ToDouble(strParts[1]) * System.Convert.ToDouble(strSales);
							strPack = "" + tot;
						}
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),40),						
							strSales,
							strPack,				
							GenUtil.strNumericFormat(rdr["Amount"].ToString()));
					}
					else
					{
						strSales = rdr["Sales"].ToString().Trim();
						if(rdr["Pack_Type"].ToString().Trim().Equals("") || rdr["Pack_Type"].ToString().Trim().IndexOf("Loose") > -1  )
						{
							strPack  = strSales;  
							strSales = "";
						}
						else
						{            
							strParts = rdr["Pack_Type"].ToString().Trim().Split(new char[] {'X'},rdr["Pack_Type"].ToString().Trim().Length);
							double tot = System.Convert.ToDouble(strParts[0]) *  System.Convert.ToDouble(strParts[1]) * System.Convert.ToDouble(strSales);
							strPack = "" + tot;
						}
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),40),						
							strSales,
							strPack);
					}
				}
			}
			if(RadPurchase.Checked)
			{
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+------------+-------------+");
				sw.WriteLine(info,"Total","",Cache["SalePKG"].ToString(),Cache["SaleKG"].ToString(),Cache["PurchasePKG"].ToString(),Cache["PurchaseKG"].ToString());
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+------------+-------------+");
			}
			else if(RadAmount.Checked)
			{
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+-------------+");
				sw.WriteLine(info,"Total","",Cache["SalesPKG"].ToString(),Cache["SalesKG"].ToString(),GenUtil.strNumericFormat(Cache["CS"].ToString()));
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+-------------+");
			}
			else
			{
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+");
				sw.WriteLine(info,"Total","",Cache["SalesPKG"].ToString(),Cache["SalesKG"].ToString());
				sw.WriteLine("+----------+----------------------------------------+-----------+-----------+");
			}
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// This method is used to write into the excel report file ProductWiseReport.xls to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string strPack = "";
			string strSales = "";
			string[] strParts;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\ProductWiseSales1.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			
			/*************************************/
			ArrayList PackVal = new ArrayList(); 
			ArrayList PackVal1 = new ArrayList(); 
			int last_index=0,last_index1=0;
			string Spc_Pack="",Spc_Pack1="";
			if(DropSearchBy.SelectedIndex==5)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
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
			else if(DropSearchBy.SelectedIndex==7)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==3)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==6)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}


			if(DropSelectOption1.SelectedIndex==5)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				string[] Totarr=Spc_Pack1.Split(new char[] {','});
				for(int i=0;Totarr.Length-1>i;i++)
				{
					string[] Packarr=Totarr[i].Split(new char[] {'X'});
					Spc_Pack1=Packarr[1].ToString();
					PackVal1.Add(double.Parse(Packarr[1]));
				}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			else if(DropSelectOption1.SelectedIndex==7)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				Spc_Pack1=Spc_Pack1.Substring(0,Spc_Pack1.Length-1);
				//string[] Totarr=Spc_Pack.Substring(0,Spc_Pack.Length);
				//for(int i=0;Totarr.Length-1>i;i++)
				//{
				//string[] Packarr=Totarr[i].Split(new char[] {'X'});
				//Spc_Pack=Totarr[i].ToString();
				//	Spc_Pack=Totarr[i].ToString();
				//}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			else if(DropSelectOption1.SelectedIndex==3)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				Spc_Pack1=Spc_Pack1.Substring(0,Spc_Pack1.Length-1);
				//string[] Totarr=Spc_Pack.Substring(0,Spc_Pack.Length);
				//for(int i=0;Totarr.Length-1>i;i++)
				//{
				//string[] Packarr=Totarr[i].Split(new char[] {'X'});
				//Spc_Pack=Totarr[i].ToString();
				//	Spc_Pack=Totarr[i].ToString();
				//}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			else if(DropSelectOption1.SelectedIndex==6)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				Spc_Pack1=Spc_Pack1.Substring(0,Spc_Pack1.Length-1);
				//string[] Totarr=Spc_Pack.Substring(0,Spc_Pack.Length);
				//for(int i=0;Totarr.Length-1>i;i++)
				//{
				//string[] Packarr=Totarr[i].Split(new char[] {'X'});
				//Spc_Pack=Totarr[i].ToString();
				//	Spc_Pack=Totarr[i].ToString();
				//}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			/**************Coment by vikas***********************/
			if(!RadPurchase.Checked)
			{
				if(DropSearchBy.SelectedIndex==5)
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
				}
				else if(DropSearchBy.SelectedIndex==7)
				{
					//Coment by vikas 8.6.2013 sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' and sm.Under_salesman in ("+Spc_Pack.ToString()+")" ;
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(p.quant as float)) as Sales, sum(cast(p.quant as float)*cast(p.Rate as float)) as Amount,Prod_Code from vw_salebook p where cast(floor(cast(p.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(p.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and p.SSR in ("+Spc_Pack.ToString()+")";
				}
				else
				{
					//Coment by vikas 8.6.2013 sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"'";
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(p.quant as float)) as Sales, sum(cast(p.quant as float)*cast(p.Rate as float)) as Amount,Prod_Code from vw_salebook p where cast(floor(cast(p.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(p.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"'";
				}
				if(DropValue.Value!="All")
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"')";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"')";
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						//5.4.2013 string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
						sql+=" and p.Prod_id in ("+Spc_Pack.ToString()+")";
					}
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
					{
						//sql+=" and p.Pack_Type='"+DropValue.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==6)
						//5.4.2013 sql+=" and p.Category='"+DropValue.Value+"'";
						sql+=" and p.Category in ("+Spc_Pack.ToString()+")";
					else if(DropSearchBy.SelectedIndex==7)																	  
						//8.6.2013 sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";     //Add by vikas 17.07.09
						sql+="and p.SSR in ("+Spc_Pack.ToString()+")";
				}
				if(DropValue1.Value!="All")
				{
					if(DropSelectOption1.SelectedIndex==1)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue1.Value.ToString().Trim()+"')";
					}
					else if(DropSelectOption1.SelectedIndex==2)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue1.Value.ToString().Trim()+"')";
					}
					else if(DropSelectOption1.SelectedIndex==3)
					{
						//5.4.2013 string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
						//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
						sql+=" and p.Prod_id in ("+Spc_Pack1.ToString()+")";
					}
					else if(DropSelectOption1.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
					else if(DropSelectOption1.SelectedIndex==5)
						//Coment by vikas 24.3.2013 sql+=" and p.Pack_Type='"+DropValue1.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal1[0]+" and "+PackVal1[last_index1];
					else if(DropSelectOption1.SelectedIndex==6)
						//sql+=" and p.Category='"+DropValue1.Value+"'";
						sql+=" and p.Category in ("+Spc_Pack1.ToString()+")";
					else if(DropSelectOption1.SelectedIndex==7)																	   //Add by vikas 17.07.09
						//Coment by vikas 24.3.2013  sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"')";     //Add by vikas 17.07.09
						//8.6.2013 sql+=" and sm.Under_salesman in ("+Spc_Pack1.ToString()+")" ;
						sql+="and p.SSR in ("+Spc_Pack1.ToString()+")";
				}
				/*if(DropType.Value!="All")
				{
					sql+=" and cust_id in(select cust_id from customer where cust_type like'"+DropType.Value+"%')";
				}*/
				sql+=" group by p.Prod_Name, p.Pack_Type,Prod_Code";
			}
			else
			{
				/********Add by vikas 25.3.2013 *************************/
				if(DropSearchBy.SelectedIndex==0 && DropSelectOption1.SelectedIndex==0)
				{
					sql="";
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
				}
				else
				{
					sql="";
					if(DropSearchBy.SelectedIndex==5)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and sm.Under_salesman in ("+Spc_Pack.ToString()+")" ;
					}
					else
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"'";
					}
					if(DropValue.Value!="All")
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"')";
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"')";
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
						else if(DropSearchBy.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue.Value+"'";
						else if(DropSearchBy.SelectedIndex==5)
						{
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
						}
						else if(DropSearchBy.SelectedIndex==6)
							//5.4.2013 sql+=" and p.Category='"+DropValue.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack.ToString()+")";
						else if(DropSearchBy.SelectedIndex==7)																	  
							sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";     //Add by vikas 17.07.09
					}
					if(DropValue1.Value!="All")
					{
						if(DropSelectOption1.SelectedIndex==1)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue1.Value.ToString().Trim()+"')";
						}
						else if(DropSelectOption1.SelectedIndex==2)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue1.Value.ToString().Trim()+"')";
						}
						else if(DropSelectOption1.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack1.ToString()+")";
						}
						else if(DropSelectOption1.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==5)
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal1[0]+" and "+PackVal1[last_index1];
						else if(DropSelectOption1.SelectedIndex==6)
							//5.4.2013 sql+=" and p.Category='"+DropValue1.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack1.ToString()+")";
						else if(DropSelectOption1.SelectedIndex==7)																	   //Add by vikas 17.07.09
							sql+=" and sm.Under_salesman in ("+Spc_Pack1.ToString()+")" ;
					}
					sql+=" group by p.Prod_Name, p.Pack_Type,Prod_Code,p.Prod_id union ";

					if(DropSearchBy.SelectedIndex==5)
					{
						sql+="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else
					{
						sql+="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					}
					if(DropValue.Value!="All")
					{
						if(DropSearchBy.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
						else if(DropSearchBy.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue.Value+"'";
						else if(DropSearchBy.SelectedIndex==5)
						{
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
						}
						else if(DropSearchBy.SelectedIndex==6)
							//5.4.2013 sql+=" and p.Category='"+DropValue.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack.ToString()+")";
					}
					if(DropValue1.Value!="All")
					{
						if(DropSelectOption1.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack1.ToString()+")";
						}
						else if(DropSelectOption1.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==5)
							//sql+=" and p.Pack_Type='"+DropValue1.Value+"'";
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal1[0]+" and "+PackVal1[last_index1];
						else if(DropSelectOption1.SelectedIndex==6)
							//sql+=" and p.Category='"+DropValue1.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack1.ToString()+")";
					}
					sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,prod_code";
					
				}
				/*********End************************/

				/*if(DropSearchBy.SelectedIndex==5)
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
				}
				else
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
				}
				if(DropValue.Value!="All")
				{
					if(DropSearchBy.SelectedIndex==3)
					{
						string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
					{
						//sql+=" and p.Pack_Type='"+DropValue.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==6)
						sql+=" and p.Category='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==7)													
						sql+=" and p.Category='"+DropValue.Value+"'";
				
					//sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,prod_code";
					if(DropValue1.Value!="All")
					{
						if(DropSelectOption1.SelectedIndex==3)
						{
							string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
							sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
						}
						else if(DropSelectOption1.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==5)
							sql+=" and p.Pack_Type='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==6)
							sql+=" and p.Category='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==7)													
							sql+=" and p.Category='"+DropValue1.Value+"'";
					}
					sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,prod_code";
				}
				else
				{
					if(DropSearchBy.SelectedIndex==5)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
					else
					{
						sql="";
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
				}*/
			}

			sql=sql+" order by "+Cache["strorderby"];
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+txtDateTo.Text);
			if(RadPurchase.Checked)
				sw.WriteLine("Product Code\tProduct Name\tSales Type\t\tPurchase Type");
			else if(RadAmount.Checked)
				sw.WriteLine("Product Code\tProduct Name\tSales Type\t\tAmount");
			else
				sw.WriteLine("Product Code\tProduct Name\tSales Type");
			if(RadPurchase.Checked)
				sw.WriteLine("\t\tPkg\tLtr/Kg\tPkg\tLtr/Kg");
			else if(RadAmount.Checked)
				sw.WriteLine("\t\tPkg\tLtr/Kg\t");
			else
				sw.WriteLine("\t\tPkg\tLtr/Kg\t");
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{				
					if(RadPurchase.Checked)
					{
						double Totqty=0;
						
						string[] tot_qty=rdr["pack_type"].ToString().Split(new char[]{'X'});
			
						Totqty=Convert.ToDouble(tot_qty[0].ToString())*Convert.ToDouble(tot_qty[1].ToString());

						strPack="0";
						strSales="0";
						SqlDataReader rdr1 = null;
						dbobj.SelectQuery("select sum(cast(qty as float)) as Sale from Sales_master sm,Sales_details sd where sm.invoice_no=sd.invoice_no and prod_id='"+rdr["Prod_ID"].ToString()+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr1);
						if(rdr1.Read())
						{
							if(rdr1["Sale"].ToString()!="" && rdr1["Sale"].ToString()!=null)
								strPack=rdr1["Sale"].ToString();
						}
						rdr1.Close();
						if(rdr["Pack_Type"].ToString()!="")
							//20.06.09 strSales=System.Convert.ToString(double.Parse(strPack)*double.Parse(rdr["Total_Qty"].ToString()));
							strSales=System.Convert.ToString(double.Parse(strPack)*double.Parse(Totqty.ToString()));

						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString().Trim()+"\t"+
							strPack+"\t"+
							strSales+"\t"+
							//Check2(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString())+"\t"+
							//Multiply2(rdr["Total_Qty"].ToString(),rdr["Prod_ID"].ToString())+"\t"+
							Check1(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString())+"\t"+
							//20.06.09 Multiply11(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString())
							Multiply11(rdr["Pack_Type"].ToString(),rdr["Prod_ID"].ToString())
							);
					}
					else if(RadAmount.Checked)
					{
						strSales = rdr["Sales"].ToString().Trim();
						if(rdr["Pack_Type"].ToString().Trim().Equals("") || rdr["Pack_Type"].ToString().Trim().IndexOf("Loose") > -1  )
						{
							strPack  = strSales;  
							strSales = "";
						}
						else
						{            
							strParts = rdr["Pack_Type"].ToString().Trim().Split(new char[] {'X'},rdr["Pack_Type"].ToString().Trim().Length);
							double tot = System.Convert.ToDouble(strParts[0]) *  System.Convert.ToDouble(strParts[1]) * System.Convert.ToDouble(strSales);
							strPack = "" + tot;
						}
						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString().Trim()+"\t"+						
							strSales+"\t"+
							strPack+"\t"+			
							GenUtil.strNumericFormat(rdr["Amount"].ToString()));
					}
					else
					{
						strSales = rdr["Sales"].ToString().Trim();
						if(rdr["Pack_Type"].ToString().Trim().Equals("") || rdr["Pack_Type"].ToString().Trim().IndexOf("Loose") > -1  )
						{
							strPack  = strSales;  
							strSales = "";
						}
						else
						{            
							strParts = rdr["Pack_Type"].ToString().Trim().Split(new char[] {'X'},rdr["Pack_Type"].ToString().Trim().Length);
							double tot = System.Convert.ToDouble(strParts[0]) *  System.Convert.ToDouble(strParts[1]) * System.Convert.ToDouble(strSales);
							strPack = "" + tot;
						}
						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString().Trim()+"\t"+						
							strSales+"\t"+
							strPack);
					}
				}
				if(RadPurchase.Checked)
					sw.WriteLine("Total"+"\t\t"+Cache["SalePKG"].ToString()+"\t"+Cache["SaleKG"].ToString()+"\t"+Cache["PurchasePKG"].ToString()+"\t"+Cache["PurchaseKG"].ToString());
				else if(RadAmount.Checked)
					sw.WriteLine("Total"+"\t\t"+Cache["SalesPKG"].ToString()+"\t"+Cache["SalesKG"].ToString()+"\t"+Cache["CS"].ToString());
				else
					sw.WriteLine("Total"+"\t\t"+Cache["SalesPKG"].ToString()+"\t"+Cache["SalesKG"].ToString());
			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method multiplies the package with actual qty. called from .aspx.
		/// </summary>
		double Total=0,TotalPur=0,TotalPur1=0;
		protected double Multiply(string str)
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
				Total+=ans;
				Cache["SalesKG"]=Total.ToString();
				return ans;
			}
			else
			{
				if(!mystr[1].Trim().Equals(""))
					return System.Convert.ToDouble( mystr[1].ToString()); 
				else
					return 0;
			}
		}

		/// <summary>
		/// This method multiplies the package with actual qty. called from .aspx.
		/// </summary>
		protected double Multiply1(string Totqty,string Prod_id)
		{
			SqlDataReader rdr = null;
			double tot=0;
			dbobj.SelectQuery("select sum(cast(qty as float)) as purchase from purchase_master pm,purchase_details pd where pm.invoice_no=pd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["Purchase"].ToString()!="" && rdr["Purchase"].ToString()!=null)
					tot=double.Parse(rdr["purchase"].ToString());
			}
			rdr.Close();
			if(Totqty!="")
				tot=tot*double.Parse(Totqty);
			TotalPur+=tot;
			Cache["PurchaseKG"]=TotalPur.ToString();
			return tot;
		}

		protected double Multiply11(string type,string Prod_id)
		{
			double Totqty=0;
			
			string[] tot_qty=type.Split(new char[]{'X'});
			
			Totqty=Convert.ToDouble(tot_qty[0].ToString())*Convert.ToDouble(tot_qty[1].ToString());

			SqlDataReader rdr = null;
			double tot=0;
			dbobj.SelectQuery("select sum(cast(qty as float)) as purchase from purchase_master pm,purchase_details pd where pm.invoice_no=pd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["Purchase"].ToString()!="" && rdr["Purchase"].ToString()!=null)
					tot=double.Parse(rdr["purchase"].ToString());
			}
			rdr.Close();
			if(Totqty!=0)
				tot=tot*Totqty;
			TotalPur+=tot;
			Cache["PurchaseKG"]=TotalPur.ToString();
			return tot;
		}


		/// <summary>
		/// This method multiplies the package with actual qty. called from .aspx.
		/// </summary>
		protected double Multiply2(string Totqty,string Prod_id)
		{
			SqlDataReader rdr = null;
			double tot=0;
			dbobj.SelectQuery("select sum(cast(qty as float)) as Sale from sales_master sm,Sales_details sd where sm.invoice_no=sd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			//dbobj.SelectQuery("select sum(cast(qty as float)) as purchase from purchase_master pm,purchase_details pd where pm.invoice_no=pd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["sale"].ToString()!="" && rdr["sale"].ToString()!=null)
					tot=double.Parse(rdr["sale"].ToString());
			}
			rdr.Close();
			if(Totqty!="")
				tot=tot*double.Parse(Totqty);
			TotalPur1+=tot;
			Cache["SaleKG"]=TotalPur1.ToString();
			return tot;
		}
		
		
		protected double Multiply22(string type,string Prod_id)
		{
            double Totqty=0;
			
			string[] tot_qty=type.Split(new char[]{'X'});
			
			Totqty=Convert.ToDouble(tot_qty[0].ToString())*Convert.ToDouble(tot_qty[1].ToString());

			SqlDataReader rdr = null;
			double tot=0;
			dbobj.SelectQuery("select sum(cast(qty as float)) as Sale from sales_master sm,Sales_details sd where sm.invoice_no=sd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			//dbobj.SelectQuery("select sum(cast(qty as float)) as purchase from purchase_master pm,purchase_details pd where pm.invoice_no=pd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["sale"].ToString()!="" && rdr["sale"].ToString()!=null)
					tot=double.Parse(rdr["sale"].ToString());
			}
			rdr.Close();
			if(Totqty!=0)
				tot=tot*Totqty;
			TotalPur1+=tot;
			Cache["SaleKG"]=TotalPur1.ToString();
			return tot;
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
		/// This method is used to split the date and return in MM/dd/yyyy format.
		/// </summary>
		private DateTime getdate(string dat,bool to)
		{
			//int dd=mm=yy=0;
			string[] dt=dat.IndexOf("/")>0?dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0])+1);			
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));			
		}

		/// <summary>
		/// This method is used to check the package type if loose then return blank otherwise return given closing stock.
		/// </summary>
		double Tot=0,TotPur=0,TotPur1=0;
		protected string Check(string cs,string type)
		{
			if(type.ToUpper().Length==0 || type.IndexOf("Loose") > -1 )
				return "&nbsp;";
			else
			{
				Tot+=double.Parse(cs);
				Cache["SalesPKG"]=Tot.ToString();
				return cs;
			}
		}

		/// <summary>
		/// This method is used to first get the purchase qty according to given Prod_ID from 
		/// purchase master table and check the product type if product type is blank or null 
		/// then return the blank otherwise return the Purchase qty.
		/// </summary>
		protected string Check1(string type, string Prod_id)
		{
			SqlDataReader rdr = null;
			double tot=0;
			dbobj.SelectQuery("select sum(cast(qty as float)) as purchase from purchase_master pm,purchase_details pd where pm.invoice_no=pd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["Purchase"].ToString()!="" && rdr["Purchase"].ToString()!=null)
					tot=double.Parse(rdr["purchase"].ToString());
			}
			rdr.Close();
			if(type.ToUpper().Length==0 || type.IndexOf("Loose") > -1 )
				return "&nbsp;";
			else
			{
				//Tot+=double.Parse(cs);
				TotPur+=tot;
				Cache["PurchasePKG"]=TotPur.ToString();
				return tot.ToString();
			}
		}

		/// <summary>
		/// This method is used to first get the sales qty according to given Prod_ID from sales master table and check 
		/// the product type if product type is blank or null then return the blank otherwise return the sales qty.
		/// </summary>
		protected string Check2(string type, string Prod_id)
		{
			SqlDataReader rdr = null;
			double tot=0;
			dbobj.SelectQuery("select sum(cast(qty as float)) as Sale from Sales_master sm,Sales_details sd where sm.invoice_no=sd.invoice_no and prod_id='"+Prod_id+"' and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["Sale"].ToString()!="" && rdr["Sale"].ToString()!=null)
					tot=double.Parse(rdr["Sale"].ToString());
			}
			rdr.Close();
			if(type.ToUpper().Length==0 || type.IndexOf("Loose") > -1 )
				return "&nbsp;";
			else
			{
				//Tot+=double.Parse(cs);
				TotPur1+=tot;
				Cache["SalePKG"]=TotPur1.ToString();
				return tot.ToString();
			}
		}

		
		double CS=0;
		/// <summary>
		/// This method is used to count the total amount.
		/// </summary>
		protected string GetCS(string amt)
		{
			CS+=double.Parse(amt);
			Cache["CS"]=CS.ToString();
			return amt;
		}

		/// <summary>
		/// This method is used to return date in mm/DD/YYYY format.
		/// </summary>
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
				CreateLogFiles.ErrorLog("Form:ProductwiseSales.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string  sql="";
			
			/*************************************/
			ArrayList PackVal = new ArrayList(); 
			ArrayList PackVal1 = new ArrayList(); 
			int last_index=0,last_index1=0;
			string Spc_Pack="",Spc_Pack1="";
			if(DropSearchBy.SelectedIndex==5)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
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
			else if(DropSearchBy.SelectedIndex==7)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==3)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}
			else if(DropSearchBy.SelectedIndex==6)
			{
				Spc_Pack=Session["Spc_Packs"].ToString();
				Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
				PackVal.Sort();
				last_index=PackVal.Count-1;
			}


			if(DropSelectOption1.SelectedIndex==5)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				string[] Totarr=Spc_Pack1.Split(new char[] {','});
				for(int i=0;Totarr.Length-1>i;i++)
				{
					string[] Packarr=Totarr[i].Split(new char[] {'X'});
					Spc_Pack1=Packarr[1].ToString();
					PackVal1.Add(double.Parse(Packarr[1]));
				}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			else if(DropSelectOption1.SelectedIndex==7)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				Spc_Pack1=Spc_Pack1.Substring(0,Spc_Pack1.Length-1);
				//string[] Totarr=Spc_Pack.Substring(0,Spc_Pack.Length);
				//for(int i=0;Totarr.Length-1>i;i++)
				//{
				//string[] Packarr=Totarr[i].Split(new char[] {'X'});
				//Spc_Pack=Totarr[i].ToString();
				//	Spc_Pack=Totarr[i].ToString();
				//}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			else if(DropSelectOption1.SelectedIndex==3)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				Spc_Pack1=Spc_Pack1.Substring(0,Spc_Pack1.Length-1);
				//string[] Totarr=Spc_Pack.Substring(0,Spc_Pack.Length);
				//for(int i=0;Totarr.Length-1>i;i++)
				//{
				//string[] Packarr=Totarr[i].Split(new char[] {'X'});
				//Spc_Pack=Totarr[i].ToString();
				//	Spc_Pack=Totarr[i].ToString();
				//}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}
			else if(DropSelectOption1.SelectedIndex==6)
			{
				Spc_Pack1=Session["Spc_Packs1"].ToString();
				Spc_Pack1=Spc_Pack1.Substring(0,Spc_Pack1.Length-1);
				//string[] Totarr=Spc_Pack.Substring(0,Spc_Pack.Length);
				//for(int i=0;Totarr.Length-1>i;i++)
				//{
				//string[] Packarr=Totarr[i].Split(new char[] {'X'});
				//Spc_Pack=Totarr[i].ToString();
				//	Spc_Pack=Totarr[i].ToString();
				//}
				PackVal1.Sort();
				last_index1=PackVal1.Count-1;
			}

			/**************Coment by vikas***********************/
			if(!RadPurchase.Checked)
			{
				if(DropSearchBy.SelectedIndex==5)
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
				}
				else if(DropSearchBy.SelectedIndex==7)
				{
					//Coment by vikas 8.6.2013 sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' and sm.Under_salesman in ("+Spc_Pack.ToString()+")" ;
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(p.quant as float)) as Sales, sum(cast(p.quant as float)*cast(p.Rate as float)) as Amount,Prod_Code from vw_salebook p where cast(floor(cast(p.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(p.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and p.SSR in ("+Spc_Pack.ToString()+")";
				}
				else
				{
					//Coment by vikas 8.6.2013 sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(sd.qty as float)) as Sales, sum(cast(sd.qty as float)*cast(sd.Rate as float)) as Amount,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"'";
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type, sum(cast(p.quant as float)) as Sales, sum(cast(p.quant as float)*cast(p.Rate as float)) as Amount,Prod_Code from vw_salebook p where cast(floor(cast(p.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(p.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"'";
				}
				if(DropValue.Value!="All")
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"')";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"')";
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						//5.4.2013 string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
						sql+=" and p.Prod_id in ("+Spc_Pack.ToString()+")";
					}
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
					{
						//sql+=" and p.Pack_Type='"+DropValue.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==6)
						//5.4.2013 sql+=" and p.Category='"+DropValue.Value+"'";
						sql+=" and p.Category in ("+Spc_Pack.ToString()+")";
					else if(DropSearchBy.SelectedIndex==7)																	  
						//Coment by 8.6.2013 sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";     //Add by vikas 17.07.09
						sql+="and p.SSR in ("+Spc_Pack.ToString()+")";
				}
				if(DropValue1.Value!="All")
				{
					if(DropSelectOption1.SelectedIndex==1)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue1.Value.ToString().Trim()+"')";
					}
					else if(DropSelectOption1.SelectedIndex==2)
					{
						sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue1.Value.ToString().Trim()+"')";
					}
					else if(DropSelectOption1.SelectedIndex==3)
					{
						//5.4.2013 string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
						//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
						sql+=" and p.Prod_id in ("+Spc_Pack1.ToString()+")";
					}
					else if(DropSelectOption1.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
					else if(DropSelectOption1.SelectedIndex==5)
						//Coment by vikas 24.3.2013 sql+=" and p.Pack_Type='"+DropValue1.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal1[0]+" and "+PackVal1[last_index1];
					else if(DropSelectOption1.SelectedIndex==6)
						//sql+=" and p.Category='"+DropValue1.Value+"'";
						sql+=" and p.Category in ("+Spc_Pack1.ToString()+")";
					else if(DropSelectOption1.SelectedIndex==7)																	   //Add by vikas 17.07.09
						//Coment by vikas 24.3.2013  sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue1.Value+"')";     //Add by vikas 17.07.09
						//Coment by 8.6.2013 sql+=" and sm.Under_salesman in ("+Spc_Pack1.ToString()+")" ;
					sql+="and p.SSR in ("+Spc_Pack1.ToString()+")";
				}
				/*if(DropType.Value!="All")
				{
					sql+=" and cust_id in(select cust_id from customer where cust_type like'"+DropType.Value+"%')";
				}*/
				sql+=" group by p.Prod_Name, p.Pack_Type,Prod_Code";
			}
			else
			{
				/********Add by vikas 25.3.2013 *************************/
				if(DropSearchBy.SelectedIndex==0 && DropSelectOption1.SelectedIndex==0)
				{
					sql="";
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
				}
				else
				{
					sql="";
					if(DropSearchBy.SelectedIndex==5)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"' and sm.Under_salesman in ("+Spc_Pack.ToString()+")" ;
					}
					else
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(txtDateTo.Text))+"'";
					}
					if(DropValue.Value!="All")
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"')";
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"')";
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
						else if(DropSearchBy.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue.Value+"'";
						else if(DropSearchBy.SelectedIndex==5)
						{
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
						}
						else if(DropSearchBy.SelectedIndex==6)
							//5.4.2013 sql+=" and p.Category='"+DropValue.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack.ToString()+")";
						else if(DropSearchBy.SelectedIndex==7)																	  
							sql+=" and sm.Under_salesman =(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";     //Add by vikas 17.07.09
					}
					if(DropValue1.Value!="All")
					{
						if(DropSelectOption1.SelectedIndex==1)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.group_name='"+DropValue1.Value.ToString().Trim()+"')";
						}
						else if(DropSelectOption1.SelectedIndex==2)
						{
							sql+=" and cust_id in (select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue1.Value.ToString().Trim()+"')";
						}
						else if(DropSelectOption1.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack1.ToString()+")";
						}
						else if(DropSelectOption1.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==5)
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal1[0]+" and "+PackVal1[last_index1];
						else if(DropSelectOption1.SelectedIndex==6)
							//5.4.2013 sql+=" and p.Category='"+DropValue1.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack1.ToString()+")";
						else if(DropSelectOption1.SelectedIndex==7)																	   //Add by vikas 17.07.09
							sql+=" and sm.Under_salesman in ("+Spc_Pack1.ToString()+")" ;
					}
					sql+=" group by p.Prod_Name, p.Pack_Type,Prod_Code,p.Prod_id union ";

					if(DropSearchBy.SelectedIndex==5)
					{
						sql+="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else
					{
						sql+="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
					}
					if(DropValue.Value!="All")
					{
						if(DropSearchBy.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack.ToString()+")";
						}
						else if(DropSearchBy.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue.Value+"'";
						else if(DropSearchBy.SelectedIndex==5)
						{
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
						}
						else if(DropSearchBy.SelectedIndex==6)
							//5.4.2013 sql+=" and p.Category='"+DropValue.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack.ToString()+")";
					}
					if(DropValue1.Value!="All")
					{
						if(DropSelectOption1.SelectedIndex==3)
						{
							//5.4.2013 string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
							//5.4.2013 sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
							sql+=" and p.Prod_id in ("+Spc_Pack1.ToString()+")";
						}
						else if(DropSelectOption1.SelectedIndex==4)
							sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
						else if(DropSelectOption1.SelectedIndex==5)
							//sql+=" and p.Pack_Type='"+DropValue1.Value+"'";
							sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal1[0]+" and "+PackVal1[last_index1];
						else if(DropSelectOption1.SelectedIndex==6)
							//sql+=" and p.Category='"+DropValue1.Value+"'";
							sql+=" and p.Category in ("+Spc_Pack1.ToString()+")";
					}
					sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,prod_code";
					
				}
				/*********End************************/

				/*if(DropSearchBy.SelectedIndex==5)
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
				}
				else
				{
					sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,Prod_Code from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
				}
				if(DropValue.Value!="All")
				{
					if(DropSearchBy.SelectedIndex==3)
					{
						string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
					{
						//sql+=" and p.Pack_Type='"+DropValue.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					}
					else if(DropSearchBy.SelectedIndex==6)
						sql+=" and p.Category='"+DropValue.Value+"'";
				}
				//sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,prod_code";
				if(DropValue1.Value!="All")
				{
					if(DropSelectOption1.SelectedIndex==3)
					{
						string[] pname=DropValue1.Value.Split(new char[] {':'},DropValue1.Value.Length);
						sql+=" and p.prod_name='"+pname[0].ToString()+"' and p.pack_type='"+pname[1].ToString()+"'";
					}
					else if(DropSelectOption1.SelectedIndex==4)
						sql+=" and p.Prod_Name='"+DropValue1.Value+"'";
					else if(DropSelectOption1.SelectedIndex==5)
						//sql+=" and p.Pack_Type='"+DropValue1.Value+"'";
						sql+=" and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index];
					else if(DropSelectOption1.SelectedIndex==6)
						sql+=" and p.Category='"+DropValue1.Value+"'";
				}
				sql+=" group by p.Prod_Name, p.Pack_Type,pd.Prod_id,Total_Qty,prod_code";
				
				if(DropValue.Value=="All" && DropValue1.Value=="All")
				{
					if(DropSearchBy.SelectedIndex==5)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
					else
					{
						sql="";
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
				}
				else
				{
				
					if(DropSearchBy.SelectedIndex==5)
					{
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' and cast(substring(Pack_Type, CHARINDEX('X', Pack_Type)+1, len(Pack_Type)-(CHARINDEX('X', Pack_Type)-1)) as float) between "+PackVal[0]+" and "+PackVal[last_index]+" group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
					else
					{
						sql="";
						sql="select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,p.prod_id,Prod_Code from Products p, Sales_Master sm, Sales_Details sd where p.Prod_ID = sd.Prod_ID and sm.Invoice_No=sd.Invoice_No and cast(floor(cast(sm.Invoice_Date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(sm.Invoice_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by p.Prod_Name, p.Pack_Type,prod_code,p.prod_id union select p.Prod_Name + ' ' + p.Pack_Type AS Prod_Name, p.Pack_Type,pd.prod_id,Prod_Code  from products p,purchase_master pm,purchase_details pd where pd.prod_id=p.prod_id and pm.invoice_no=pd.invoice_no and cast(floor(cast(Invoice_Date as float)) as datetime)>= '"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and  cast(floor(cast(Invoice_Date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateTo.Text)).ToShortDateString()+"' group by p.Prod_Name, p.Pack_Type,prod_code,pd.prod_id";
					}
				}*/
			}

			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"Products");
			DataTable dtcustomer=ds.Tables["Products"]; 
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			if(RadPurchase.Checked)
				GrdSalesPurchase.DataSource=dv;
			else if(RadAmount.Checked)
				grdLegAmount.DataSource=dv;
			else
				grdLegNonAmount.DataSource=dv;
			if(dv.Count!=0)
			{
				grdLegAmount.Visible=false;
				grdLegNonAmount.Visible=false;
				GrdSalesPurchase.Visible=false;
				if(RadPurchase.Checked)
				{
					GrdSalesPurchase.DataBind();
					GrdSalesPurchase.Visible=true;
				}
				else if(RadAmount.Checked)
				{
					grdLegAmount.DataBind();
					grdLegAmount.Visible=true;
				}
				else
				{
					grdLegNonAmount.DataBind();
					grdLegNonAmount.Visible=true;
				}
			}
			else
			{
				grdLegAmount.Visible=false;
				grdLegNonAmount.Visible=false;
				GrdSalesPurchase.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
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
				CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:cmdrpt_Click");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:cmdrpt_Click"+ ex.Message+". User: "+uid);
			}
		}
		
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
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len3)
		{
			while(rdr.Read())
			{
				if(rdr["Prod_Name"].ToString().Trim().Length>len1)
					len1=rdr["Prod_Name"].ToString().Trim().Length;					
				if(rdr["Amount"].ToString().Trim().Length>len3)
					len3=rdr["Amount"].ToString().Trim().Length;
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

		protected void grdLeg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		private void Button1_Click(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the ProductWiseSales.txt file name to print.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ProductWiseReport.txt<EOF>");
					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);
					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:print");
				} 
				catch (ArgumentNullException ane) 
				{
					//Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:print"+ ane.Message+". User: "+uid);
				} 
				catch (SocketException se) 
				{
					///Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:print"+ se.Message+". User: "+uid);
				} 
				catch (Exception es) 
				{
					//Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:print"+ es.Message+". User: "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Method:print"+ ex.Message+". User: "+uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the excel report file ProductWiseSales.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(grdLegAmount.Visible==true || grdLegNonAmount.Visible==true || GrdSalesPurchase.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   ProductWiseSales Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:ProductWiseSales.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    ProductWiseSales Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
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
				string strProductGroup="",strProdWithPack="",strProduct="",strPack="",strPartyName="",strPartyType="";
				string strSsrName="",strGroup="",strSubGroup="";

				strProductGroup="select distinct category from Products";
				strProdWithPack="select distinct Prod_Name+':'+pack_type from Products";
				strProduct="select distinct Prod_Name from Products";
				strPack="select distinct pack_type from Products";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012
				
				//coment by vikas 25.05.09 strPartyName="Select Cust_Name from customer order by Cust_Name";
				strPartyName="Select Cust_Name,city from customer order by Cust_Name,city";
				
				strPartyType="select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
			
				strSsrName="Select Distinct emp_name from employee where designation='Servo Sales Representative' and status=1 order by Emp_name";

				//17.07.09 vikas string[] arrStr = {strPartyName,strPartyType,strProdWithPack,strProduct,strPack,strProductGroup};
				
				//coment by vikas 16.11.2012 string[] arrStr = {strPartyName,strPartyType,strProdWithPack,strProduct,strPack,strProductGroup,strSsrName};
				

				//17.07.09 vikas HtmlInputHidden[] arrCust = {tempPartyName,tempPartyType,tempProdWithPack,tempProduct,tempPack,tempProductGroup};
				
				//coment by vikas 16.11.2012 HtmlInputHidden[] arrCust = {tempPartyName,tempPartyType,tempProdWithPack,tempProduct,tempPack,tempProductGroup,tempSsrName};
				
				string[] arrStr = {strPartyName,strPartyType,strProdWithPack,strProduct,strPack,strProductGroup,strSsrName,strGroup, strSubGroup};
				HtmlInputHidden[] arrCust = {tempPartyName,tempPartyType,tempProdWithPack,tempProduct,tempPack,tempProductGroup,tempSsrName,tempGroup,tempSubGroup};

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							/******Add by vikas 25.05.09 ***********/
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
							/******End****************************/
						}
					}
					rdr.Close();
					if(i==0)
						texthiddenpartyname.Value=tempPartyName.Value;
					if(i==1)
						texthiddenpartytype.Value=tempPartyType.Value;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalesReport.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Wise Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}