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
using System.Data.SqlClient;
using System.Drawing;
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for ModvatCenvatInvoice.
	/// </summary>
	public partial class ModvatCenvatInvoice : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);	
		string uid;
		public bool  address = false;
		static string NetAmount = "0";
		//static string FromDate="",ToDate="";
		static string CustID="";
		public static string val="";
		public int flag = 0;
		public float Header1Height = 0;
		public float Header2Height = 0;
		public float BodyHeight = 0;
		public float Footer1Height = 0;
		public float Footer2Height = 0;
		public float RateRs = 0;
		public float BillQty = 0;
		public float AmountRs = 0;
		public float BatchNo = 0;
		public float GradePackName = 0;
		public float FreeQty = 0;
		public float DisQty = 0;
		public float LtrKg = 0;
		public float SchDis = 0;
		public float RupeesinWords = 0;
		public float ProvisionalBalance = 0;
		public float Remarks = 0;
		public float Position1 = 0;
		public float Position2 = 0;
		public bool  PartyName = false;
		public bool  Address = false;
		public bool  City = false;
		public bool  Tin_No = false;
		public bool  DocumentNo = false;
		public bool  Date = false;
		public bool  DtTime = false;
		public bool  DueDate = false;
		public bool  Time = false;
		public bool  Blank = false;
		public bool  Blank1 = false;
		public bool  VehicleNo = false;
		public static bool FlagPrint=false;
		static string[] ProductType = new string[12];
		static string[] ProductName = new string[12];
		static string[] ProductPack = new string[12];
		static string[] ProductQty = new string[12];
		static string[] SchProductType = new string[12];
		static string[] SchProductName = new string[12];
		static string[] SchProductPack = new string[12];
		static string[] SchProductQty = new string[12];
		static string Order_Date ="";
	
		/// <summary>
		/// Put user code to initialize the page here
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
				txtMessage.Text =(Session["Message"].ToString());
				txtVatRate.Value  = (Session["VAT_Rate"].ToString());
			}
			catch(Exception ex)
			{				
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:pageload"+ ex.Message+"  EXCEPTION "+"   "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(tempDelinfo.Value=="Yes")
			{
				DeleteTheRec();
				//UpdateBatchNo();
			}
			if(!IsPostBack)
			{
				try
				{
					Order_Date="";
					checkPrevileges();
					lblInvoiceDate.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
					//lblEntryTime.Text=DateTime.Now.ToString ();
					//lblEntryBy.Text =Session["User_Name"].ToString();
					InventoryClass  obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql;
					GetNextOrderNo();
					#region Fetch the Product Types and fill in the ComboBoxes
					sql="select distinct Prod_name,pack_type from Products p, Price_Updation pu where p.Prod_ID=pu.Prod_ID";
					SqlDtr = obj.GetRecordSet(sql);
					if(SqlDtr.HasRows)
					{
						texthiddenprod.Value="Type,";
						while(SqlDtr.Read())
						{
							texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString()+",";
						}
					}
					SqlDtr.Close();
					#endregion

					#region Fetch All SalesMan and Fill in the ComboBox
					sql = "Select Emp_Name from Employee where Designation ='Servo Sales Representative' order by Emp_Name";
					SqlDtr = obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
						DropUnderSalesMan.Items.Add (SqlDtr.GetValue(0).ToString ());				
					}
					SqlDtr.Close ();		
					#endregion
					
					#region fill hiddentext 
					sql="select cust_name from customer order by cust_name ";
					SqlDtr=obj.GetRecordSet(sql);
					int i=0;
					while(SqlDtr.Read())
					{
						texthidden.Value+=SqlDtr.GetValue(0).ToString()+",";
						val+=SqlDtr.GetValue(0).ToString()+",";
						i++;
					}
					SqlDtr.Close();
					#endregion

					#region Fetch All Discount and fill in the textbox
					sql="select * from SetDis";
					SqlDtr=obj.GetRecordSet(sql);
					if(SqlDtr.Read())
					{
						if(SqlDtr["CashDisModVatStatus"].ToString()=="1")
						{
							txtCashDisc.Text=SqlDtr["CashDisModVat"].ToString();
							if(SqlDtr["CashDisLtrModVat"].ToString()=="Rs.")
								DropCashDiscType.SelectedIndex=0;
							else
								DropCashDiscType.SelectedIndex=1;
						}
						else
							txtCashDisc.Text="";
						if(SqlDtr["DiscountModVatStatus"].ToString()=="1")
						{
							txtDisc.Text=SqlDtr["DiscountModVat"].ToString();
							if(SqlDtr["DisLtrModVat"].ToString()=="Rs.")
								DropDiscType.SelectedIndex=0;
							else
								DropDiscType.SelectedIndex=1;
						}
						else
							txtDisc.Text="";
					}
					else
					{
						txtDisc.Text="";
						txtCashDisc.Text="";
						DropCashDiscType.SelectedIndex=0;
						DropDiscType.SelectedIndex=0;
					}
					SqlDtr.Close ();		
					#endregion
					GetProducts();
					FetchData();
					GetFOECust();
					getscheme();
					getscheme1();
					getschemefoe();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:pageload.   EXCEPTION: "+ ex.Message+"  User_ID: "+uid);   
				}
			}
            lblInvoiceDate.Text = Request.Form["lblInvoiceDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["lblInvoiceDate"].ToString().Trim();
            txtChallanDate.Text = Request.Form["txtChallanDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtChallanDate"].ToString().Trim();
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
		/// This method checks the price updation for all the products is available or not?
		/// </summary>
		public void GetProducts()
		{
			try
			{
				InventoryClass  obj=new InventoryClass ();
				InventoryClass  obj1=new InventoryClass ();
				SqlDataReader SqlDtr;
				string sql;
				SqlDataReader rdr=null; 
				int count = 0;
				int count1 = 0;
				dbobj.ExecuteScalar("Select Count(Prod_id) from  products",ref count);
				dbobj.ExecuteScalar("select count(distinct p.Prod_ID ) from products p, Price_Updation pu where p.Prod_id = pu.Prod_id",ref count1);
				if(count != count1)
				{
					lblMessage.Text = "Price updation not available for some products";
				}

				#region Fetch the Product Types and fill in the ComboBoxes
				string str="",MinMax="";
				sql="select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit,minlabel,maxlabel,reorderlable from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					#region Fetch Sales Rate
					
					sql= "select top 1 Sal_Rate from Price_Updation where Prod_ID="+SqlDtr["Prod_ID"]+" order by eff_date desc";
					rdr = obj1.GetRecordSet(sql);
					if(rdr.Read())
					{
						if(double.Parse(rdr["Sal_Rate"].ToString())!=0)
						{
							str=str+ SqlDtr["Category"]+":"+SqlDtr["Prod_Name"]+":"+SqlDtr["Pack_Type"];
							str=str+":"+rdr["Sal_Rate"];
						}
						else
						{
							rdr.Close();
							continue;
						}
					}
					else
						str=str+":0";
					rdr.Close();
					MinMax=MinMax+SqlDtr["Prod_Name"]+":"+SqlDtr["Pack_Type"]+":"+SqlDtr["minlabel"]+":"+SqlDtr["maxlabel"]+":"+SqlDtr["reorderlable"]+"~";
					#endregion

					#region Fetch Closing Stock
					sql="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr["Prod_ID"]+" order by stock_date desc";
					rdr = obj1.GetRecordSet(sql);
					if(rdr.Read())
						str=str+":"+rdr["Closing_Stock"]+":"+SqlDtr["Unit"];
					else
						str=str+":0"+":"+SqlDtr["Unit"];
					rdr.Close();
					#endregion
					#region Fetch Scheme 
					sql="select discount from oilscheme where ProdID="+SqlDtr["Prod_ID"]+"";
					rdr = obj1.GetRecordSet(sql);
					if(rdr.Read())
						str=str+":"+rdr["discount"]+",";
					else
						str=str+":0"+",";
					rdr.Close();
					#endregion
				}
				SqlDtr.Close();
				temptext.Value=str;
				tempminmax.Value=MinMax;
				#endregion		 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:GetProducts().  EXCEPTION: "+ ex.Message+"  user "+uid);
			}
		}

		/// <summary>
		/// This method get only fleet or oe type customer.
		/// </summary>
		public void GetFOECust()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string str="";
			string sql="select cust_Name from customer  where cust_type='Fleet' or cust_type like('Oe%')  order by cust_Name";
			SqlDtr = obj.GetRecordSet (sql);
			while(SqlDtr.Read ())
			{
				str=str+","+SqlDtr.GetValue(0).ToString().Trim();
			}
			SqlDtr.Close();
			temptext13.Value=str;
		}

		/// <summary>
		/// This method stored given scheme of the products and closing stock also.
		/// </summary>
		public void getscheme()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string str="";
			SqlDataReader rdr=null; 
			 
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"];
				string sql1="select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack freep,o.datefrom df,o.dateto dt,p.unit unit from products p,oilscheme o where p.prod_id='"+SqlDtr["sch"]+"'";
				dbobj.SelectQuery(sql1,ref rdr); 
				string unit="";
				if(rdr.Read())
				{
					str=str+":"+rdr["cat1"]+":"+rdr["pname1"]+":"+rdr["ptype1"]+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
					unit =rdr["unit"].ToString();
				}
				else
				{
					str=str+":"+0+":"+0+":"+0+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
					unit ="";
				}
				rdr.Close();

				#region Fetch Closing Stock
				string sql2="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr["sch"]+" order by stock_date desc";
				dbobj.SelectQuery(sql2,ref rdr); 
				if(rdr.Read())
					str=str+":"+rdr["Closing_Stock"]+":"+unit+":"+SqlDtr["dis"];
				else
					str=str+":0"+":"+unit+":"+SqlDtr["dis"];
				rdr.Close();
				str=str+":"+SqlDtr["scheme"]+",";
				#endregion
			}
			SqlDtr.Close();
			temptext11.Value=str;
		}

		/// <summary>
		/// This method stored only liter scheme information.
		/// </summary>
		public void getscheme1()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string str="";
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"'";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";
			}
			SqlDtr.Close();
			temptext12.Value=str;
		}

		/// <summary>
		/// This method read the pre print template and sets the  values in global variables.
		/// </summary>
		public void getTemplateDetails()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\InvoiceDesigner\SalesInvoicePrePrintTemplate.INI";
			StreamReader  sr = new StreamReader(path);
			string[] data = new string[40];
			int n = 0;
			string info = "";
			while (sr.Peek() >= 0)
			{
				info = sr.ReadLine();
				if (info.StartsWith("[") || info.StartsWith("#"))
				{
					continue;
				}
				else
				{
					data[n++] = info;
				}
			}
			sr.Close();
			Header1Height = float.Parse(data[0].ToString().Trim());
			Header2Height = float.Parse(data[1].ToString().Trim()); 
			BodyHeight = float.Parse(data[2].ToString().Trim());
			Footer1Height = float.Parse(data[3].ToString().Trim());
			Footer2Height = float.Parse(data[4].Trim());
			RateRs = float.Parse(data[5].Trim());
			BillQty = float.Parse(data[6].Trim());
			AmountRs = float.Parse(data[7].Trim());
			BatchNo = float.Parse(data[8].Trim());
			FreeQty = float.Parse(data[9].Trim());
			DisQty = float.Parse(data[10].Trim());
			GradePackName = float.Parse(data[11].Trim());
			LtrKg = float.Parse(data[12].Trim());
			SchDis = float.Parse(data[13].Trim());
			RupeesinWords = float.Parse(data[14].Trim());
			ProvisionalBalance = float.Parse(data[15].Trim());
			Remarks = float.Parse(data[16].Trim());
			Position1 = float.Parse(data[17].Trim());
			Position2 = float.Parse(data[18].Trim());

			if(data[19].Trim().Equals("True"))
			{
				PartyName = true;
			}
			else
			{
				PartyName = false;
			}
			if(data[20].Trim().Equals("True"))
			{
				Date = true;
			}
			else
			{
				Date = false;
			}
			if(data[21].Trim().Equals("True"))
			{
				VehicleNo = true;
			}
			else
			{
				VehicleNo = false;
			}
			if(data[22].Trim().Equals("True"))
			{
				Address = true;
			}
			else
			{
				Address = false;
			}
			if(data[23].Trim().Equals("True"))
			{
				City = true;
			}
			else
			{
				City = false;
			}
			if(data[24].Trim().Equals("True"))
			{
				Tin_No = true;
			}
			else
			{
				Tin_No = false;
			}
			if(data[25].Trim().Equals("True"))
			{
				Blank = true;
			}
			else
			{
				Blank = false;
			}
			if(data[26].Trim().Equals("True"))
			{
				DocumentNo = true;
			}
			else
			{
				DocumentNo = false;
			}
			if(data[27].Trim().Equals("True"))
			{
				DtTime = true;
			}
			else
			{
				DtTime = false;
			}
			if(data[28].Trim().Equals("True"))
			{
				DueDate = true;
			}
			else
			{
				DueDate = false;
			}
			if(data[29].Trim().Equals("True"))
			{
				Blank1 = true;
			}
			else
			{
				Blank1 = false;
			}
			if(data[30].Trim().Equals("True"))
			{
				Time = true;
			}
			else
			{
				Time = false;
			}
		}

		/// <summary>
		/// This method fatch the only year according to passing date.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetYear(string dt)
		{
			if(dt!="")
			{
				string[] year= dt.IndexOf("/")>0? dt.Split(new char[] {'/'},dt.Length): dt.Split(new char[] { '-' }, dt.Length);
				string yr=year[2].Substring(2);	
				return(yr);
			}
			else
				return "";
		}

		/// <summary>
		/// This method checks the user privileges from session.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="4";
			string SubModule="13";
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
			if(View_flag =="0")
			{
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}
			if(Edit_Flag=="0" )
				btnEdit.Enabled=false; 
			if(Del_Flag=="0" )
				btnDelete.Enabled=false;
			if(Add_Flag=="0")
			{
				btnSave.Enabled = false; 
				Button1.Enabled = false; 
			}
			#endregion				
		}

		/// <summary>
		/// This method delete the particular invoice no from all tables but some information contain in master table.
		/// </summary>
		public void DeleteTheRec()
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				//SqlDataReader rdr=null;
				SqlCommand cmd;
				SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				Con.Open();
				cmd = new SqlCommand("delete from Order_Col_Master where Order_No='"+dropInvoiceNo.SelectedItem.Text+"'",Con);
				cmd.ExecuteNonQuery();
				Con.Close();
				cmd.Dispose();
				Con.Open();
				MessageBox.Show("Sales Transaction Deleted");
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:btnDelete_Click - OrderNo : " + dropInvoiceNo.SelectedItem.Text+" Deleted, user : "+uid);
				Clear();
				clear1();
				GetNextOrderNo();
				//GetProducts();
				//FetchData();
				//getschemefoe();
				//getscheme();
				//getscheme1();
				lblInvoiceNo.Visible=true;
				dropInvoiceNo.Visible=false;
				btnEdit.Visible=true;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:btnDelete_Click - OrderNo : " + dropInvoiceNo.SelectedItem.Text+" ,Exception : "+ex.Message+" user : "+uid);
			}
		}

		/// <summary>
		/// This method store all foe discount in a hidden textbox.
		/// </summary>
		public void getschemefoe()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string sql1;
			string str="";
			SqlDataReader rdr=null;
			 
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust  from products p,foe o where p.prod_id=o.prodid  and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"'";
			SqlDtr=obj.GetRecordSet(sql);
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					str=str+":"+SqlDtr["cat"].ToString().Trim()+":"+SqlDtr["pname"].ToString().Trim()+":"+SqlDtr["ptype"].ToString().Trim()+":"+SqlDtr["dis"].ToString();
					sql1="select cust_name from customer where cust_id='"+SqlDtr["cust"]+"'";
					dbobj.SelectQuery(sql1,ref rdr); 
					if(rdr.Read())
					{
						str=str+":"+rdr["cust_name"].ToString().Trim()+",";
					}
					rdr.Close();
				}
				SqlDtr.Close();
			}
			
			string sql2="select p.cust_name cust,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust  from customer p,foe o where p.cust_id=o.custid and o.prodid='0'  and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"'";
			dbobj.SelectQuery(sql2,ref rdr);
			while(rdr.Read())
			{
				str=str+":"+"0"+":"+"0"+":"+"0"+":"+rdr["dis"]+":"+rdr["cust"].ToString().Trim()+",";
			}
			rdr.Close();
			temptextfoe.Value=str;
		}

		/// <summary>
		/// This method store all customer information in a hidden textbox.
		/// </summary>
		public void FetchData()
		{
			InventoryClass obj=new InventoryClass ();
			InventoryClass obj1=new InventoryClass ();
			SqlDataReader rdr1=null;
			SqlDataReader rdr3=null;
			string str1="";
			DateTime duedate;
			string duedatestr ="";
			
			rdr3 = obj.GetRecordSet("select c.City,CR_Days,Curr_Credit,Cust_ID,SSR,Cust_Name,Emp_Name  from Customer c,Employee e where e.Emp_ID=c.SSR order by Cust_Name");
			while(rdr3.Read())
			{
				duedate=DateTime.Now.AddDays(System.Convert.ToDouble(rdr3["CR_Days"]));
				duedatestr=duedate.ToShortDateString();
				str1 = str1+rdr3["Cust_Name"].ToString()+"~"+rdr3["City"].ToString().Trim()+"~"+GenUtil.str2DDMMYYYY(duedatestr.Trim())+"~"+GenUtil.strNumericFormat(rdr3["Curr_Credit"].ToString().Trim())+"~";
				//rdr1 = obj1.GetRecordSet("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr3["Cust_ID"]+" order by EntryDate Desc");
				dbobj.SelectQuery("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr3["Cust_ID"]+" order by EntryDate Desc",ref rdr1);
				if(rdr1.Read())
				{
					str1 = str1+GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim())+"~"+rdr1["BalanceType"].ToString().Trim()+"~";	
				}
				else
				{
					str1 = str1+"0"+"~"+" "+"~";	
				}
				rdr1.Close();
				str1+=rdr3["Emp_Name"].ToString()+"#";
			}
			rdr3.Close();
			TxtVen.Value =str1; 
		}

		/// <summary>
		/// This method returns the next Order No from Order_Master table.
		/// </summary>
		public void GetNextOrderNo()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;//,rdr=null;
			string sql;
	
			#region Fetch the Next Invoice Number
			sql="select max(Order_No)+1 from Order_Col_Master";
			SqlDtr=obj.GetRecordSet(sql);
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					lblInvoiceNo.Text=SqlDtr.GetValue(0).ToString();
					if(lblInvoiceNo.Text=="")
						lblInvoiceNo.Text="1";
				}
			}
			else
				lblInvoiceNo.Text="1";
			SqlDtr.Close ();		
			#endregion
		}

		/// <summary>
		/// This method clears the form.
		/// </summary>
		public void Clear()
		{
			tempcashdis.Value="";
			NetAmount="0";
			Order_Date="";
			CustID="";
			txtCashDiscount.Text="";
			txtDiscount.Text="";
			tempInvoiceDate.Value="";
			tempDelinfo.Value="";
			txtChallanNo.Text="";
			DropSalesType.SelectedIndex=0;
			TxtCrLimit.Value="";
			DropUnderSalesMan.SelectedIndex=0;
			text1.Value="Select";
			lblPlace .Value="";
			lblDueDate.Value="";
			lblCurrBalance .Value="";
			txtVehicleNo.Text="";
			DropVehicleNo.Visible = false;
			txtVehicleNo.Visible = true;
			txtPromoScheme.Text="";
			txtRemark.Text="";
			txtGrandTotal.Text="";
			lblCreditLimit.Value = "";
			txtDisc.Text="";
			txtNetAmount.Text="";
			DropDiscType.SelectedIndex=0;
			txtVAT.Text = "";
			txtCashDisc.Text = "";
			DropCashDiscType.SelectedIndex = 0;
			Yes.Checked = true;
			No.Checked = false;
			txtfleetoediscount.Text="";
			dropfleetoediscount.SelectedIndex=0;
			txtfleetoediscountRs.Text="";
			txtliter.Text="";
			txtschemetotal.Text="";

			#region Clear Hidden TextBoxex
			totalltr.Value="";
			tempdiscount.Value="";
			tempcashdis.Value="";
			HtmlInputText[] DropType = {DropType1,DropType2,DropType3,DropType4,DropType5,DropType6,DropType7,DropType8,DropType9,DropType10,DropType11,DropType12};
			TextBox[] Qty = {txtQty1,txtQty2,txtQty3,txtQty4,txtQty5,txtQty6,txtQty7,txtQty8,txtQty9,txtQty10,txtQty11,txtQty12};
			TextBox[] TextRate = {txtRate1,txtRate2,txtRate3,txtRate4,txtRate5,txtRate6,txtRate7,txtRate8,txtRate9,txtRate10,txtRate11,txtRate12};
			TextBox[] TextAmount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8,txtAmount9,txtAmount10,txtAmount11,txtAmount12};
			TextBox[] Scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};
			TextBox[] Foe = {txtfoe1,txtfoe2,txtfoe3,txtfoe4,txtfoe5,txtfoe6,txtfoe7,txtfoe8,txtfoe9,txtfoe10,txtfoe11,txtfoe12};
			TextBox[] AVStock = {txtAvStock1,txtAvStock2,txtAvStock3,txtAvStock4,txtAvStock5,txtAvStock6,txtAvStock7,txtAvStock8,txtAvStock9,txtAvStock10,txtAvStock11,txtAvStock12};
			TextBox[] TempQty = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12};
			HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12};
			TextBox[] ProdTypesch={txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12}; 
			TextBox[] Avlsch={txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12}; 
			TextBox[] Qtysch={txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12}; 
			HtmlInputHidden[] tmpSchType={tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12}; 
			TextBox[]  SchQuantity = {txtTempSchQty1,txtTempSchQty2,txtTempSchQty3,txtTempSchQty4,txtTempSchQty5,txtTempSchQty6,txtTempSchQty7,txtTempSchQty8 ,txtTempSchQty9,txtTempSchQty10,txtTempSchQty11,txtTempSchQty12};
			for(int ii=0;ii<ProdTypesch.Length;ii++)
			{
				//ProdNamesch[ii].Text="";
				ProdTypesch[ii].Text="";
				Avlsch[ii].Text="";
				//PackTypesch[ii].Text="";
				Qtysch[ii].Text="";
				SchQuantity[ii].Text="";
				ProductType[ii]="";
				ProductName[ii]="";
				ProductPack[ii]="";
				ProductQty[ii]="";
				SchProductType[ii]="";
				SchProductName[ii]="";
				SchProductPack[ii]="";
				SchProductQty[ii]="";
				tmpSchType[ii].Value="";
				DropType[ii].Value="Type";
				Qty[ii].Text="";
				TextRate[ii].Text="";
				TextAmount[ii].Text="";
				Scheme[ii].Text="";
				Foe[ii].Text="";
				AVStock[ii].Text="";
				TempQty[ii].Text="";
				tmpQty[ii].Value="";
			}
			#endregion

		}

		/// <summary>
		/// This method clears the form.
		/// </summary>
		public void clear1()
		{
			TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
			TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
			TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
			TextBox[]  AvStock = {txtAvStock1,txtAvStock2,txtAvStock3,txtAvStock4,txtAvStock5,txtAvStock6,txtAvStock7,txtAvStock8,txtAvStock9,txtAvStock10,txtAvStock11,txtAvStock12};
			
			for (int i=0;i<Qty.Length;i++) 
			{
				Qty[i].Enabled = true;
				Rate[i].Enabled = true;
				Amount[i].Enabled = true;
				AvStock[i].Enabled = true;
			}
			lblInvoiceDate.Text=GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
		}

		/// <summary>
		/// This method fatching all invoice no and fill into the dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			lblInvoiceNo.Visible=false;
			btnEdit.Visible=false;
			dropInvoiceNo.Visible=true;
			btnSave.Enabled = true;
			Button1.Enabled = true;
			DropSalesType.Enabled=false;
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string sql;
			#region Fetch the All Invoice Number and fill in Combo
			dropInvoiceNo.Items.Clear();
			dropInvoiceNo.Items.Add("Select");
			sql="select Order_No from Order_Col_Master order by Order_No";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				dropInvoiceNo.Items.Add(SqlDtr.GetValue(0).ToString());
			}
			SqlDtr.Close ();
			#endregion
		}

		/// <summary>
		/// This method fatch the all required information according to invoice no who select from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
 
		protected void dropInvoiceNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TextSelect.Text=dropInvoiceNo.SelectedItem.Value.ToString();
			try
			{
				if(TextSelect.Text=="Select")
				{
					MessageBox.Show("Please Select Invoice No");
				}
				else
				{
					Clear();
					HtmlInputText[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12};
					TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
					TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
					TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
					TextBox[]  AvStock = {txtAvStock1,txtAvStock2,txtAvStock3,txtAvStock4,txtAvStock5,txtAvStock6,txtAvStock7,txtAvStock8,txtAvStock9,txtAvStock10,txtAvStock11,txtAvStock12};
					TextBox[]  tempQty = {txtTempQty1, txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12}; 
					TextBox[]  tempSchQty = {txtTempSchQty1,txtTempSchQty2,txtTempSchQty3,txtTempSchQty4,txtTempSchQty5,txtTempSchQty6,txtTempSchQty7,txtTempSchQty8 ,txtTempSchQty9,txtTempSchQty10,txtTempSchQty11,txtTempSchQty12};
					HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12};
					HtmlInputHidden[] tmpSchType = {tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12};
					TextBox[] pid={txtpname1,txtpname2,txtpname3,txtpname4,txtpname5,txtpname6,txtpname7,txtpname8,txtpname9,txtpname10,txtpname11,txtpname12};
					TextBox[] pid1={txtmwid1,txtmwid2,txtmwid3,txtmwid4,txtmwid5,txtmwid6,txtmwid7,txtmwid8,txtmwid9,txtmwid10,txtmwid11,txtmwid12};
					TextBox[]  scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};
					TextBox[]  foe = {txtfoe1,txtfoe2,txtfoe3,txtfoe4,txtfoe5,txtfoe6,txtfoe7,txtfoe8,txtfoe9,txtfoe10,txtfoe11,txtfoe12};
					TextBox[] ProdType1={txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12}; 
					TextBox[] Qty1={txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12}; 
					TextBox[] stk1={txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12}; 
					InventoryClass  obj=new InventoryClass ();
					InventoryClass  obj1=new InventoryClass ();
					SqlDataReader SqlDtr;
					string sql,sql1;
					SqlDataReader rdr=null,rdr1=null,rdr2=null,rdr3=null;
					int i=0;
					FlagPrint=false;
					Button1.CausesValidation=true;
					
					#region Get Data from Sales Master Table regarding Invoice No.
					sql="select * from Order_Col_Master where Order_No='"+dropInvoiceNo.SelectedItem.Value +"'" ;
					SqlDtr=obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
						Order_Date = SqlDtr.GetValue(1).ToString();
						string strDate = SqlDtr.GetValue(1).ToString().Trim();
						int pos = strDate.IndexOf(" ");
				
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}
						
						lblInvoiceDate.Text =GenUtil.str2DDMMYYYY(strDate);  
						tempInvoiceDate.Value=GenUtil.str2DDMMYYYY(strDate);
						DropSalesType.SelectedIndex=(DropSalesType.Items.IndexOf((DropSalesType.Items.FindByValue (SqlDtr.GetValue(2).ToString()))));
						DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr.GetValue(4).ToString()))));
						if(getCustomerVehicles(SqlDtr["Cust_ID"].ToString()) == true)
						{
							DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(SqlDtr.GetValue(5).ToString().Trim()));   
						}
						else
						{
							txtVehicleNo.Text=SqlDtr.GetValue(5).ToString();
						}
						txtGrandTotal.Text=SqlDtr.GetValue(6).ToString();
						txtGrandTotal.Text = GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()); 
						txtDisc.Text=SqlDtr.GetValue(7).ToString(); 
						txtDisc.Text = GenUtil.strNumericFormat(txtDisc.Text.ToString()); 
						DropDiscType.SelectedIndex= DropDiscType.Items.IndexOf((DropDiscType.Items.FindByValue(SqlDtr.GetValue(8).ToString())));
						txtNetAmount.Text =SqlDtr.GetValue(9).ToString(); 
						txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
						NetAmount=GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
						txtPromoScheme.Text= SqlDtr.GetValue(10).ToString(); 
						txtRemark.Text=SqlDtr.GetValue(11).ToString();  
						if(SqlDtr["Discount_type"].ToString()=="Per")
						{
							txtDiscount.Text=System.Convert.ToString((double.Parse(SqlDtr["Grand_Total"].ToString())-double.Parse(SqlDtr["schdiscount"].ToString()))*double.Parse(SqlDtr["discount"].ToString())/100);
							txtDiscount.Text=System.Convert.ToString(Math.Round(double.Parse(txtDiscount.Text),2));
						}
						else
							txtDiscount.Text="";
						if(SqlDtr["cash_Disc_type"].ToString()=="Per")
						{
							double tot =0;
							if(txtDiscount.Text!="")
								tot = double.Parse(SqlDtr["Grand_Total"].ToString())-(double.Parse(SqlDtr["schdiscount"].ToString())+double.Parse(SqlDtr["foediscount"].ToString())+double.Parse(txtDiscount.Text));
							else
								tot = double.Parse(SqlDtr["Grand_Total"].ToString())-(double.Parse(SqlDtr["schdiscount"].ToString())+double.Parse(SqlDtr["foediscount"].ToString()));
							txtCashDiscount.Text=System.Convert.ToString(tot*double.Parse(SqlDtr["Cash_Discount"].ToString())/100);
							txtCashDiscount.Text=System.Convert.ToString(Math.Round(double.Parse(txtCashDiscount.Text),2));
							tempcashdis.Value=txtCashDiscount.Text;
						}
						else
							txtCashDiscount.Text="";
						txtCashDisc.Text=SqlDtr.GetValue(15).ToString(); 
						txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString()); 
						DropCashDiscType.SelectedIndex= DropCashDiscType.Items.IndexOf((DropCashDiscType.Items.FindByValue(SqlDtr.GetValue(16).ToString())));
						txtVAT.Text =  SqlDtr.GetValue(17).ToString();
						txtschemetotal.Text=SqlDtr.GetValue(18).ToString();
						txtfleetoediscount.Text=SqlDtr.GetValue(19).ToString();
						dropfleetoediscount.SelectedIndex= dropfleetoediscount.Items.IndexOf((dropfleetoediscount.Items.FindByValue(SqlDtr.GetValue(20).ToString())));
						txtfleetoediscountRs.Text=SqlDtr.GetValue(21).ToString();
						txtliter.Text=SqlDtr.GetValue(22).ToString();
						if(SqlDtr["ChallanNo"].ToString()=="0")
							txtChallanNo.Text="";
						else
							txtChallanNo.Text=SqlDtr["ChallanNo"].ToString();
						if(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString())=="1/1/1900")
							txtChallanDate.Text="";
						else
							txtChallanDate.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()));
						if(txtVAT.Text.Trim() == "0")
						{
							Yes.Checked = false;
							No.Checked = true;
						}
						else
						{
							No.Checked = false;
							Yes.Checked = true;
						}
					}
					SqlDtr.Close();
					#endregion
		
					#region Get Customer name and place regarding Customer ID
					sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID from Customer as c, Order_Col_Master as s where c.Cust_ID= s.Cust_ID and s.Order_No='"+dropInvoiceNo.SelectedValue +"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						texthidden1.Value=SqlDtr.GetValue(0).ToString();
						text1.Value=SqlDtr.GetValue(0).ToString();
						CustID=SqlDtr["Cust_ID"].ToString();
						lblPlace.Value=SqlDtr.GetValue(1).ToString();
						DateTime duedate=DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
						string duedatestr=(duedate.ToShortDateString());
						lblDueDate.Value =GenUtil.str2DDMMYYYY(duedatestr);
						lblCurrBalance.Value=GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());
						TxtCrLimit.Value = SqlDtr.GetValue(4).ToString();
						lblCreditLimit.Value  = SqlDtr.GetValue(4).ToString();
						txtcusttype.Text = SqlDtr.GetValue(5).ToString();
					}
					SqlDtr.Close();
					sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_Master as s where c.CustID= s.Cust_ID and s.Order_No='"+dropInvoiceNo.SelectedValue+"' order by entrydate desc";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
				
						lblCurrBalance.Value=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString())+" "+SqlDtr.GetValue(1).ToString();
						
					}
					SqlDtr.Close();
					#endregion
					
					#region Get Data from Sales Details Table regarding Invoice No.
					sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date"+
						" from Products p, Order_Col_Details sd,Order_Col_Master sm"+
						" where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='"+dropInvoiceNo.SelectedItem.Value +"' order by sd.sno" ;
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						Qty[i].Enabled = true;
						Rate[i].Enabled = true;
						Amount[i].Enabled = true;
						AvStock[i].Enabled = true;
						ProdType[i].Value=SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();
						Qty[i].Text=SqlDtr.GetValue(3).ToString();
						ProductType[i]=SqlDtr.GetValue(0).ToString ();
						ProductName[i]=SqlDtr.GetValue(1).ToString ();
						ProductPack[i]=SqlDtr.GetValue(2).ToString ();
						ProductQty[i]=SqlDtr.GetValue(3).ToString();
						scheme[i].Text=SqlDtr["scheme1"].ToString();
						tempQty[i].Text   = Qty[i].Text ;
						tmpQty[i].Value  = SqlDtr.GetValue(3).ToString();  
						Rate[i].Text=SqlDtr.GetValue(4).ToString();
						Amount[i].Text=SqlDtr.GetValue(5).ToString();
						pid[i].Text=SqlDtr.GetValue(6).ToString();
						pid1[i].Text=SqlDtr.GetValue(6).ToString();
						foe[i].Text=SqlDtr.GetValue(9).ToString();
						sql1="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr.GetValue(6).ToString()+" order by stock_date desc";
						dbobj.SelectQuery(sql1,ref rdr); 
						if(rdr.Read())
						{
							AvStock [i].Text =rdr["Closing_Stock"]+" "+SqlDtr.GetValue(7).ToString();
						}	
						else
						{
							AvStock [i].Text ="0"+" "+SqlDtr.GetValue(7).ToString();
						}
						Qty[i].ToolTip = "Actual Available Stock = "+Qty[i].Text.ToString()+" + "+ AvStock[i].Text.ToString();
						string	sql11="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit"+
							" from Products p, Order_Col_Details sd"+
							" where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno="+i+" and sd.Order_No='"+dropInvoiceNo.SelectedItem.Value +"'" ;
						dbobj.SelectQuery(sql11,ref rdr2);
						
						if(rdr2.HasRows)
						{
							while(rdr2.Read())
							{
								ProdType1[i].Text=rdr2.GetValue(1).ToString()+":"+rdr2.GetValue(2).ToString();
								Qty1[i].Text=rdr2.GetValue(3).ToString();
								SchProductType[i]=rdr2.GetValue(0).ToString();
								SchProductName[i]=rdr2.GetValue(1).ToString();
								SchProductPack[i]=rdr2.GetValue(2).ToString();
								SchProductQty[i]=rdr2.GetValue(3).ToString();
								tempSchQty[i].Text=rdr2.GetValue(3).ToString();
								string sql12="select top 1 Closing_Stock from Stock_Master where productid="+rdr2.GetValue(4).ToString()+" order by stock_date desc";
								dbobj.SelectQuery(sql12,ref rdr1); 
								if(rdr1.Read())
								{
									stk1[i].Text =rdr1["Closing_Stock"]+" "+rdr2.GetValue(5).ToString();
								}	
								else
								{
									stk1[i].Text ="0"+" "+rdr2.GetValue(5).ToString();
								}
								rdr3 = obj1.GetRecordSet("select o.discounttype from Order_Col_Details sd,oilscheme o,Order_Col_Master sm where o.prodid=sd.prod_id and sm.Order_No=sd.Order_No and sd.Order_No='"+SqlDtr["Order_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(SqlDtr["Order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(SqlDtr["Order_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
								if(rdr3.HasRows)
								{
									if(rdr3.Read())
									{
										tmpSchType[i].Value=rdr3.GetValue(0).ToString();
									}
								}
								rdr3.Close();
							}
							rdr1.Close();
						}
						rdr2.Close();
						rdr.Close();
						i++;
					}
					SqlDtr.Close();
					#endregion
				
				}
				CreateLogFiles.ErrorLog("Form:Sales Invoisee.aspx,Method:dropInvoiceNo_SelectedIndexChanged " +" Sales invoice is viewed for invoice no: "+dropInvoiceNo.SelectedItem.Value.ToString()+" userid "+"   "+"   "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:dropInvoiceNo_SelectedIndexChanged " +" Sales invoise is update for invoise no: "+dropInvoiceNo.SelectedItem.Value.ToString()+" EXCEPTION  "+ex.Message+"  userid "+"   "+"   "+uid);
			}
		}

		/// <summary>
		/// This method fatch the customer vehicle info. according to passing value.
		/// </summary>
		/// <param name="cust_id"></param>
		/// <returns></returns>
		public bool getCustomerVehicles(string cust_id)
		{
			try
			{
				SqlDataReader SqlDtr =null;
				dbobj.SelectQuery("Select * from Customer_Vehicles where Cust_ID ="+cust_id,ref SqlDtr); 
				if(SqlDtr.HasRows)
				{
					DropVehicleNo.Visible = true;
					txtVehicleNo.Visible = false; 
					RequiredFieldValidator1.Visible = false;
					RequiredFieldValidator3.Visible = true; 
					DropVehicleNo.Items.Clear();
					DropVehicleNo.Items.Add("Select");
					while(SqlDtr.Read())
					{
						if(!SqlDtr.GetValue(2).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(2).ToString());         
						if(!SqlDtr.GetValue(3).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(3).ToString()); 
						if(!SqlDtr.GetValue(4).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(4).ToString()); 
						if(!SqlDtr.GetValue(5).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(5).ToString()); 
						if(!SqlDtr.GetValue(6).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(6).ToString()); 
						if(!SqlDtr.GetValue(7).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(7).ToString()); 
						if(!SqlDtr.GetValue(8).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(8).ToString()); 
						if(!SqlDtr.GetValue(9).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(9).ToString()); 
						if(!SqlDtr.GetValue(10).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(10).ToString()); 
						if(!SqlDtr.GetValue(11).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add(SqlDtr.GetValue(11).ToString()); 
					}
					SqlDtr.Close();
					return true;
				}
				else
				{
					DropVehicleNo.Visible = false;
					txtVehicleNo.Visible  =true;
					RequiredFieldValidator1.Visible = true;
					RequiredFieldValidator3.Visible = false; 
					txtVehicleNo.Text = "";
					return false;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:getCustomerVehicles().  EXCEPTION  "+ex.Message+"  userid "+"   "+"   "+uid);
			}
			return true;
		}

		/// <summary>
		/// It calls the save_updateInvoice() function to save or update the Invoice Details and calls the reportmaking4() fucntion to creates the print file and calls the print() code fire the print of passing file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			save_updateInvoive();
			if(flag == 0)
			{
				Clear();
				clear1();
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:btnSave_Click - OrderNo : " + lblInvoiceNo.Text  );
				GetNextOrderNo();
				//GetProducts();
				//FetchData();
				//getschemefoe();
				//getscheme();
				//getscheme1();
				lblInvoiceNo.Visible=true; 
				dropInvoiceNo.Visible=false;
				btnEdit.Visible=true;
				Button1.Enabled = true;  
			}
			else
			{
				flag = 0;
				return;
			}
		}
		
		/// <summary>
		/// This method save or update the sales information.
		/// </summary>
		public void save_updateInvoive()
		{
			InventoryClass  obj=new InventoryClass();
			try
			{
				if(lblInvoiceNo.Visible==true)
				{				
					int count = 0;
					// This part of code is use to solve the double click problem, Its checks the sales invoice no. and display the popup, that it is saved.
					dbobj.ExecuteScalar("Select count(Order_No) from Order_Col_Master where Order_No = "+lblInvoiceNo.Text.Trim(),ref count);
					if(count > 0)
					{
						MessageBox.Show("Order Collection Saved");
						Clear();
						clear1();
						GetNextOrderNo();
						//GetProducts();
						//FetchData();
						//getschemefoe();
						//getscheme();
						//getscheme1();
						lblInvoiceNo.Visible=true; 
						dropInvoiceNo.Visible=false;
						btnEdit.Visible=true;
						btnSave.Enabled = true;
						Button1.Enabled = true; 
						flag = 1;
						return ;
					}
				}
				
				obj.Order_Date = System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString());
				obj.Sales_Type=DropSalesType.SelectedItem.Value;
				obj.Under_SalesMan =DropUnderSalesMan.SelectedItem.Value;
				obj.Customer_Name =text1.Value ;
				obj.Place =lblPlace.Value.ToString();
				if(DropVehicleNo.Visible == true)
					obj.Vehicle_No=DropVehicleNo.SelectedItem.Text  ;
				else
					obj.Vehicle_No=txtVehicleNo.Text ;
				obj.Grand_Total =txtGrandTotal.Text ;
				if(txtDisc.Text=="")
					obj.Discount ="0.0";
				else
					obj.Discount =txtDisc.Text;
				obj.Discount_Type=DropDiscType.SelectedItem.Value;
				obj.Net_Amount =txtNetAmount.Text;
				obj.Promo_Scheme=txtPromoScheme.Text;
				obj.Remerk =txtRemark.Text;
				obj.Entry_By ="";
				obj.EntryTime ="";
				if(txtCashDisc.Text.Trim() =="")
					obj.Cash_Discount  ="0.0";
				else
					obj.Cash_Discount = txtCashDisc.Text.Trim() ;
				obj.Cash_Disc_Type =DropCashDiscType.SelectedItem.Value ;
				obj.VAT_Amount = txtVAT.Text.Trim();
				obj.Slip_No="0"; 
				obj.Cr_Plus="0";
				obj.Dr_Plus=txtNetAmount.Text;	
				obj.Credit_Limit = lblCreditLimit.Value.ToString();
				obj.schdiscount=txtschemetotal.Text.Trim();
				if(txtfleetoediscount.Text.Equals(""))
					obj.foediscount="0";
				else
					obj.foediscount=txtfleetoediscount.Text.Trim();
				obj.foediscounttype=dropfleetoediscount.SelectedItem.Text.ToString();
				obj.foediscountrs=txtfleetoediscountRs.Text.Trim();
				obj.totalqtyltr=txtliter.Text.Trim().ToString();
				if(txtChallanDate.Text=="")
					obj.ChallanDate="";
				else
					obj.ChallanDate=GenUtil.str2MMDDYYYY(txtChallanDate.Text);
				if(txtChallanNo.Text=="")
					obj.ChallanNo="0";
				else
					obj.ChallanNo=txtChallanNo.Text;
				if(lblInvoiceNo.Visible==true)
				{	
					if(DropSalesType.SelectedItem.Text.Equals("Credit"))
					{ 
						double net;
						string s=TxtCrLimit.Value.ToString();
						string s1=TxtCrLimit1.Text.ToString();
						double cr =System.Convert.ToDouble(TxtCrLimit.Value.ToString());
						string str=lblCurrBalance.Value.ToString();
						string[] str1=str.Split(new char[]{' '},str.Length);
						if(str1[1].Equals("Cr."))
							net=System.Convert.ToDouble(txtNetAmount.Text.ToString());
						else
							net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
						if(cr>=net)
						{
							obj.Order_No =lblInvoiceNo.Text;
							obj.InsertOrderMaster();
						}
						else
						{
							MessageBox.Show("Credit Limit is less than Net Amount");
							return ;
						}
					}
					else
					{
						obj.Order_No =lblInvoiceNo.Text;
						obj.InsertOrderMaster();
					}
				}
				else
				{
					if(DropSalesType.SelectedItem.Text.Equals("Credit"))
					{
						double net;
						string s=TxtCrLimit.Value.ToString();
						string s1=TxtCrLimit1.Text.ToString();
						double cr =System.Convert.ToDouble(TxtCrLimit.Value.ToString());
						string str=lblCurrBalance.Value.ToString();
						string[] str1=str.Split(new char[]{' '},str.Length);
						if(str1[1].Equals("Cr."))
							net=System.Convert.ToDouble(txtNetAmount.Text.ToString());
						else
							net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
						if(cr<net)
						{
							MessageBox.Show("Credit Limit is less than Net Amount");
							return;
						}
					}
					if(DropSalesType.SelectedItem.Text.Equals("Credit"))// || DropSalesType.SelectedItem.Text.Equals("Cash"))
					{ 
						double net;
						string s=TxtCrLimit.Value.ToString();
						string s1=TxtCrLimit1.Text.ToString();
						double cr =System.Convert.ToDouble(TxtCrLimit.Value.ToString());
						string str=lblCurrBalance.Value.ToString();
						string[] str1=str.Split(new char[]{' '},str.Length);
						if(str1[1].Equals("Cr."))
							net=System.Convert.ToDouble(txtNetAmount.Text.ToString());
						else
							net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
						if(cr>=net)
						{
							obj.Order_No=dropInvoiceNo.SelectedItem.Value;
							obj.UpdateOrderMaster();
						}
						else
						{
							MessageBox.Show("Credit Limit is less than Net Amount");
							return;
						}
					}
					else
					{
						obj.Order_No=dropInvoiceNo.SelectedItem.Value;
						obj.UpdateOrderMaster();
					}
					DropSalesType.Enabled=true;
				}
				
				string temp,Schtemp;
				
				HtmlInputText[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12};
				TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
				TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
				TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
				TextBox[]  scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};	
				TextBox[]  Quantity = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8 ,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12};
				TextBox[]  SchQuantity = {txtTempSchQty1,txtTempSchQty2,txtTempSchQty3,txtTempSchQty4,txtTempSchQty5,txtTempSchQty6,txtTempSchQty7,txtTempSchQty8 ,txtTempSchQty9,txtTempSchQty10,txtTempSchQty11,txtTempSchQty12};
				TextBox[] ProdType1={txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12}; 
				TextBox[] Qty1={txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12}; 
				TextBox[]  foe = {txtfoe1,txtfoe2,txtfoe3,txtfoe4,txtfoe5,txtfoe6,txtfoe7,txtfoe8,txtfoe9,txtfoe10,txtfoe11,txtfoe12};
				for(int j=0;j<ProdType.Length;j++)
				{
					if(Rate[j].Text==""||Rate[j].Text=="0")
						continue;
					
					if(lblInvoiceNo.Visible==true || Quantity[j].Text =="")
					{
						temp = Qty[j].Text;
						Schtemp = Qty1[j].Text;
					}
					else
					{
						temp = Qty[j].Text;
						Schtemp=Qty1[j].Text;
					}
					string[] arrName=new string[2];
					if(ProdType[j].Value.IndexOf(":")>0)
						arrName=ProdType[j].Value.Split(new char[] {':'},ProdType[j].Value.Length);
					else
					{
						arrName[0]="";
						arrName[1]="";
					}
					
					Save(arrName[0].ToString(),arrName[1].ToString(),Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.ToString())+" "+DateTime.Now.TimeOfDay.ToString(),scheme[j].Text.ToString (),foe[j].Text.ToString (),j);
					if((Qty1[j].Text=="" || Qty1[j].Text=="0") && (ProdType1[j].Text==""))
						continue;
						
					string[] arrschName=new string[2];
					if(ProdType1[j].Text.IndexOf(":")>0)
						arrschName=ProdType1[j].Text.Split(new char[] {':'},ProdType1[j].Text.Length);
					else
					{
						arrschName[0]="";
						arrschName[1]="";
					}
					Save1(arrschName[0].ToString(),arrschName[1].ToString(),Qty1[j].Text.ToString(),GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.ToString())+" "+DateTime.Now.TimeOfDay.ToString(),scheme[j].Text.ToString (),j,Schtemp);
				}
				
				PrePrintReport();
				if(lblInvoiceNo.Visible==true)
				{
					MessageBox.Show("Order collection Saved");
				}
				else
				{
					MessageBox.Show("Order Collection Updated");
				}
				FlagPrint=false;
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:save_updateInvoice()"+" Collect Order No."+obj.Order_No+" ,"+"for Customer Name  "+obj.Customer_Name+  "on Date "+obj.Customer_Name+" and NetAmount  "+obj.Net_Amount+"  is Saved "+" userid "+"   "+"   "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:save_updateInvoice(),Class:InventoryClass"+"  Order No."+obj.Order_No+" ,"+"for Customer Name  "+obj.Customer_Name+ "  Under Salesman "+obj.Under_SalesMan+" and NetAmount  "+obj.Net_Amount+"  is Saved "+"  EXCEPTION  "+ex.Message+" userid "+"   "+"   "+uid);
			}
		}

		/// <summary>
		/// Its calls the save_updateInvoice() fucntion to save or update invoice details and calls the prePrint() and Print() fucntion to create and print the SalesInvoicePrePrintReport.txt file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{
			try 
			{
				if(FlagPrint==false)
					save_updateInvoive(); 
				else
				{
					Button1.CausesValidation=true;
					FlagPrint=false;
				}

				if(flag == 0)
				{
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2);
					print(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ModvatCenvatInvoice.txt");
					Clear();
					clear1();
					CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:Button1_Click - InvoiceNo : " + lblInvoiceNo.Text  );
					GetNextOrderNo();
					//GetProducts();
					//getschemefoe();
					//getscheme();
					//getscheme1();
					//FetchData();
					lblInvoiceNo.Visible=true; 
					dropInvoiceNo.Visible=false;
					btnEdit.Visible=true;
					Button1.Enabled = true;  
				}
				else
				{
					flag = 0;
					return;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:Button1_Click  EXCEPTION :  "+ ex.Message+"   "+uid);
			}
		}

		/// <summary>
		/// contacts the print server and sends the SalesInvoicePrePrintReport.txt file name to print.
		/// </summary>
		/// <param name="fileName"></param>
		public void print(string fileName)
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
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:print"+" Sales Invoise is Print  userid   "+"   "+uid);
				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());
					// Encode the data string into a byte array.
					byte[] msg = Encoding.ASCII.GetBytes(fileName+"<EOF>");
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
					CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:print"+ ane.Message+"  EXCEPTION "+" user "+uid);
				}
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:print"+se.Message+"  EXCEPTION "+" user "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:print"+ es.Message+"  EXCEPTION "+" user "+uid);
				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:print"+ ex.Message+"  EXCEPTION "+" user "+uid);
			}
		}

		/// <summary>
		/// This Method to write into the report file to print.
		/// </summary>
		public void PrePrintReport()
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				SqlDataReader SqlDtr=null;
				//int NOC = 7;//18  1 inche = 7 characters
				getTemplateDetails();
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2);
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ModvatCenvatInvoice.txt";
				StreamWriter sw = new StreamWriter(path);
				HtmlInputText[] ProdCat={DropType1,DropType2,DropType3,DropType4,DropType5,DropType6,DropType7,DropType8,DropType9,DropType10,DropType11,DropType12}; 
				TextBox[] foe = {txtfoe1,txtfoe2,txtfoe3,txtfoe4,txtfoe5,txtfoe6,txtfoe7,txtfoe8,txtfoe9,txtfoe10,txtfoe11,txtfoe12};
				TextBox[] Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
				TextBox[] Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
				TextBox[] Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
				TextBox[] scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};	
				TextBox[] schProdType={txtTypesch1,txtTypesch2,txtTypesch3,txtTypesch4,txtTypesch5,txtTypesch6,txtTypesch7,txtTypesch8,txtTypesch9,txtTypesch10,txtTypesch11,txtTypesch12};
				TextBox[] schQty={txtQtysch1,txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12};
				string[] DespQty=new string[12];
				string[] freeDespQty=new string[12];
				string[] ProdCode=new string[12];
				string[] schProdCode=new string[12];
				string[] schPName=new string[12];
				string[] schPPack=new string[12];
				string[] schPQty=new string[12];
				string[] PackType=new string[12];
				string[] schProdName=new string[12];
				
				int h1 = System.Convert.ToInt32(Math.Floor((Header1Height * 25)/4.05));
				int h2 = System.Convert.ToInt32(Math.Floor((Header2Height * 25)/4.05));
				int bh = System.Convert.ToInt32(Math.Floor((BodyHeight * 25)/4.05));
				int f1 = System.Convert.ToInt32(Math.Floor((Footer1Height * 25)/4.05));
				int f2 = System.Convert.ToInt32(Math.Floor((Footer2Height * 25)/4.05));
				int pn = System.Convert.ToInt32(Math.Floor((Position1 * 25)/1.53));
				int sp = 50;
				int dn = System.Convert.ToInt32(Math.Floor((Position2 * 25)/1.53));
				dn = dn - (pn+50);
				int sp1 = 10;
				string info21 = " {0,-" + pn + ":S} {1,-" + sp + ":F} {2," + dn + ":F} {3,-" + sp1 + ":F}";
				int pc = 10;
				int bn = System.Convert.ToInt32(Math.Floor((BatchNo * 25)/1.53));
				int gpn = System.Convert.ToInt32(Math.Floor((GradePackName * 25)/1.53));
				int bq = System.Convert.ToInt32(Math.Floor((BillQty * 25)/1.53));
				int fq = System.Convert.ToInt32(Math.Floor((FreeQty * 25)/1.53));
				int dq = System.Convert.ToInt32(Math.Floor((DisQty * 25)/1.53));
				int lkg = System.Convert.ToInt32(Math.Floor((LtrKg * 25)/1.53));
				int rt = System.Convert.ToInt32(Math.Floor((RateRs * 25)/1.53));
				int sd = System.Convert.ToInt32(Math.Floor((SchDis * 25)/1.53));
				int am = System.Convert.ToInt32(Math.Floor((AmountRs * 25)/1.53));
				string info31 = " {0,-" + pc + ":S} {1,-" + bn + ":F} {2,-" + gpn + ":F} {3," + bq + ":F} {4," + fq + ":F} {5," + dq + ":F} {6," + lkg + ":F} {7," + rt + ":F} {8," + sd + ":F} {9," + am + ":F}";
				int rinw = System.Convert.ToInt32(Math.Floor((RupeesinWords * 25)/1.53));
				int pb = System.Convert.ToInt32(Math.Floor((ProvisionalBalance * 25)/1.53));
				int rem = System.Convert.ToInt32(Math.Floor((Remarks * 25)/1.53));
				string info51 = " {0,-" + rinw + ":S} {1,-" + 100 + ":S}";
				string info61 = " {0,-" + pb + ":S} {1,-10:S} {2,-40:S}";
				string info71 = " {0,-" + rem + ":S} {1,-80:S}";
				Double TotalQtyPack=0,TotalQtyfoe=0,TotalfoeLtr=0;
				int k=0;
				string info4="",str="",InDate="";//info2="",info3="",,info5="",info6="",info7=""
				//info2=" {0,-16:S} {1,-50:S} {2,20:S} {3,-44:S}";//Party Name & Address
				info4=" {0,-20:S} {1,20:S} {2,20:S} {3,55:S} {4,15:S}";//Party Name & Address
				//info3=" {0,-10:S} {1,-19:S} {2,-35:S} {3,5:S} {4,5:S} {5,5:S} {6,10:S} {7,12:S} {8,10:S} {9,15:S}";//Item Code
				//info5=" {0,18:S} {1,-112:S}";
				//info7=" {0,10:S} {1,-120:S}";
				//info6=" {0,46:S} {1,-10:S} {2,-74:S}";
				string curbal=lblCurrBalance.Value;
				string[] CurrBal=new string[2];
				string[] InvoiceDate=new string[2];
			
				if(lblInvoiceNo.Visible==true)
					str="select Order_Date from Order_Col_Master where Order_No="+lblInvoiceNo.Text+"";
				else
					str="select Order_Date from Order_Col_Master where Order_No="+dropInvoiceNo.SelectedItem.Text.Trim()+"";
				SqlDtr=obj.GetRecordSet(str);
				if(SqlDtr.Read())
					InDate=SqlDtr.GetValue(0).ToString();
				else
					InDate="";
				SqlDtr.Close();
				if(InDate!="")
					InvoiceDate=InDate.Split(new char[] {' '},InDate.Length);
				else
					InvoiceDate[1]="";

				if(curbal != "")
				{
					CurrBal=curbal.Split(new char[] {' '},curbal.Length);
				}
				if(CurrBal[1].Equals("Dr."))
				{
					if(txtNetAmount.Text != "")
						CurrBal[0]=System.Convert.ToString(System.Convert.ToDouble(CurrBal[0])+System.Convert.ToDouble(txtNetAmount.Text));
					else
						CurrBal[0]="";
				}
				else
				{
					if(txtNetAmount.Text != "")
					{
						if(System.Convert.ToDouble(txtNetAmount.Text) > System.Convert.ToDouble(CurrBal[0]))
							CurrBal[0]=System.Convert.ToString(System.Convert.ToDouble(txtNetAmount.Text)-System.Convert.ToDouble(CurrBal[0]));
						else
							CurrBal[0]=System.Convert.ToString(System.Convert.ToDouble(CurrBal[0])-System.Convert.ToDouble(txtNetAmount.Text));
					}
					else
						CurrBal[0]="";
				}
				string[] arrProdType = new string[2];
				for(int p=0;p<12;p++)
				{
					if(ProdCat[p].Value.IndexOf(":")>0)
						arrProdType=ProdCat[p].Value.Split(new char[] {':'},ProdCat[p].Value.Length);
					else
					{
						arrProdType[0]="";
						arrProdType[1]="";
					}
					str="select Prod_Code,Total_Qty from Products where Prod_Name='"+arrProdType[0].ToString()+"' and Pack_Type='"+arrProdType[1].ToString()+"'";
					SqlDtr = obj.GetRecordSet(str);
					if(SqlDtr.Read())
					{
						ProdCode[p]=SqlDtr.GetValue(0).ToString();
						PackType[p]=SqlDtr.GetValue(1).ToString();
					}
					else
					{
						ProdCode[p]="";
						PackType[p]="";
					}
					SqlDtr.Close();
				}
				int p1=0;
				string[] arrProdSchType = new string[2];
				for(int p=0;p<12;p++)
				{
					if(schProdType[p].Text.IndexOf(":")>0)
						arrProdSchType=schProdType[p].Text.Split(new char[] {':'},schProdType[p].Text.Length);
					else
					{
						arrProdSchType[0]="";
						arrProdSchType[1]="";
					}
					str="select Prod_Code from Products where Prod_Name='"+arrProdSchType[0].ToString()+"' and Pack_Type='"+arrProdSchType[1].ToString()+"'";
					SqlDtr = obj.GetRecordSet(str);
					if(SqlDtr.Read())
					{
						schProdCode[p1]=SqlDtr.GetValue(0).ToString();
						p1++;
					}
					SqlDtr.Close();
				}
				int jj1=0;
				string[] arrProdSchType1 = new string[2];
				for(int jj=0;jj<12;jj++)
				{
					if(schProdType[jj].Text.IndexOf(":")>0)
						arrProdSchType1=schProdType[jj].Text.Split(new char[] {':'},schProdType[jj].Text.Length);
					else
					{
						arrProdSchType1[0]="";
						arrProdSchType1[1]="";
					}
					if(!arrProdSchType1[0].ToString().Equals("") && !schQty[jj].Text.Equals(""))
					{
						schPName[jj1]="(FREE) "+schProdType[jj].Text;
						schPQty[jj1]=schQty[jj].Text;
						schProdName[jj1]=schProdType[jj].Text;
						schPPack[jj1]=arrProdSchType1[1].ToString();
						jj1++;
					}
				}
				for(int jj=jj1;jj<12;jj++)
				{
					schPQty[jj]="";
				}
				for(int j=0;j<12;j++)
				{
					if(!Qty[j].Text.Equals(""))
					{
						TotalQtyPack=TotalQtyPack+System.Convert.ToDouble(Qty[j].Text);
						DespQty[j]=Qty[j].Text;
					}
					else
						DespQty[j]="";
					if(!schQty[j].Text.Equals(""))
					{
						TotalQtyfoe=TotalQtyfoe+System.Convert.ToDouble(schQty[j].Text);
						freeDespQty[j]=schQty[j].Text;
					}
					else
						freeDespQty[j]="";
				}
				string[] arrProdSchType2 = new string[2];
				for(int i=0;i<12;i++)
				{
					if(schProdType[i].Text.IndexOf(":")>0)
						arrProdSchType2=schProdType[i].Text.Split(new char[] {':'},schProdType[i].Text.Length);
					else
					{
						arrProdSchType2[0]="";
						arrProdSchType2[1]="";
					}
					if(arrProdSchType2[1].ToString() != "" && schQty[i].Text != "")
					{
						TotalfoeLtr=TotalfoeLtr+System.Convert.ToDouble(GenUtil.changeqtyltr(arrProdSchType2[1].ToString(),int.Parse(schQty[i].Text)));
					}
				}	

				ArrayList arrProdCode = new ArrayList();
				ArrayList arrProdName = new ArrayList();
				ArrayList arrBatchNo = new ArrayList();
				ArrayList arrBillQty = new ArrayList();
				ArrayList arrFreeQty = new ArrayList();
				ArrayList arrDespQty = new ArrayList();
				ArrayList arrLtrkg = new ArrayList();
				ArrayList arrProdRate = new ArrayList();
				ArrayList arrProdScheme = new ArrayList();
				ArrayList arrProdAmount = new ArrayList();
			
				for(int p=0;p<=Qty.Length-1;p++)
				{
					if(Qty[p].Text!="")
					{
						string[] arrProdCat = ProdCat[p].Value.Split(new char[] {':'},ProdCat[p].Value.Length);
						if(lblInvoiceNo.Visible==true)
							str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+ProdCode[p].ToString()+"' and Prod_Name='"+arrProdCat[0].ToString()+"' and Pack_Type='"+arrProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+lblInvoiceNo.Text+"' and trans_type='Sales Invoice'";
						else
							str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+ProdCode[p].ToString()+"' and Prod_Name='"+arrProdCat[0].ToString()+"' and Pack_Type='"+arrProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Sales Invoice'";
						SqlDtr = obj.GetRecordSet(str);
						if(SqlDtr.HasRows)
						{
							while(SqlDtr.Read())
							{
								arrProdCode.Add(ProdCode[p].ToString());
								arrProdName.Add(ProdCat[p].Value);
								arrBatchNo.Add(SqlDtr.GetValue(0).ToString());
								arrBillQty.Add(SqlDtr.GetValue(1).ToString());
								arrDespQty.Add(SqlDtr.GetValue(1).ToString());
								arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString())*double.Parse(SqlDtr.GetValue(1).ToString())));
								arrProdRate.Add(Rate[p].Text);
								arrProdScheme.Add(scheme[p].Text);
								arrProdAmount.Add(System.Convert.ToString(double.Parse(SqlDtr.GetValue(1).ToString())*double.Parse(Rate[p].Text)));
								arrFreeQty.Add("");
							}
						}
						else
						{
							arrProdCode.Add(ProdCode[p].ToString());
							arrProdName.Add(ProdCat[p].Value);
							arrBatchNo.Add("");
							arrBillQty.Add(Qty[p].Text);
							arrDespQty.Add(DespQty[p].ToString());
							arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString())*double.Parse(Qty[p].Text)));
							arrProdRate.Add(Rate[p].Text);
							arrProdScheme.Add(scheme[p].Text);
							arrProdAmount.Add(Amount[p].Text);
							arrFreeQty.Add("");
						}
						SqlDtr.Close();
					}
				}
				for(int p=0;p<=schPQty.Length-1;p++)
				{
					string s=schPQty[p].ToString();
					if(schPQty[p].ToString()!="")
					{
						string[] arrschProdCat = schProdName[p].Split(new char[] {':'},schProdName[p].Length);
						if(lblInvoiceNo.Visible==true)
							str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+schProdCode[p].ToString()+"' and Prod_Name='"+arrschProdCat[0].ToString()+"' and Pack_Type='"+arrschProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+lblInvoiceNo.Text+"' and trans_type='Sales Invoice'";
						else
							str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+schProdCode[p].ToString()+"' and Prod_Name='"+arrschProdCat[0].ToString()+"' and Pack_Type='"+arrschProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Sales Invoice'";
						SqlDtr = obj.GetRecordSet(str);
						double totalqty=0;
						if(SqlDtr.HasRows)
						{
							while(SqlDtr.Read())
							{
								arrProdCode.Add(schProdCode[p].ToString());
								arrProdName.Add("(FREE) "+schProdType[p].Text);
								arrBatchNo.Add(SqlDtr.GetValue(0).ToString());
								arrBillQty.Add("");
								if(SqlDtr.GetValue(1).ToString()=="" || SqlDtr.GetValue(1).ToString()==null)
									totalqty=0;
								else
									totalqty=double.Parse(SqlDtr.GetValue(1).ToString());
								arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(),int.Parse(totalqty.ToString())));
								arrDespQty.Add(SqlDtr.GetValue(1).ToString());
								arrProdRate.Add("");
								arrProdScheme.Add("");
								arrProdAmount.Add("");
								arrFreeQty.Add(SqlDtr.GetValue(1).ToString());
							}
						}
						else
						{
							arrProdCode.Add(schProdCode[p].ToString());
							arrProdName.Add("(FREE) "+schProdName[p].ToString());
							arrBatchNo.Add("");
							arrBillQty.Add("");
							if(schPQty[p].ToString()=="" || schPQty[p].ToString()==null)
								totalqty=0;
							else
								totalqty=double.Parse(schPQty[p].ToString());
							arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(),int.Parse(totalqty.ToString())));
							arrDespQty.Add(schPQty[p].ToString());
							arrProdRate.Add("");
							arrProdScheme.Add("");
							arrProdAmount.Add("");
							arrFreeQty.Add(schPQty[p].ToString());
						}
						SqlDtr.Close();
					}
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

				int arrCount=0;//,arrCon=0;
				double Space=0,SpaceCount=arrBillQty.Count;
				bool FlagCount=false;
			
				do 
				{
					FlagCount=false;
					sw.WriteLine("                                                DELIVERY CHALLAN COM INVOICE");
					for(int i=0;i<h1-1;i++)//Old
					{
						sw.WriteLine("");
					}
					string addr="",ssc="",TinNo="";
					dbobj.SelectQuery("select * from customer where cust_name='"+text1.Value+"'",ref SqlDtr);
					if(SqlDtr.Read())
					{
						addr=SqlDtr["Address"].ToString();
						ssc=SqlDtr["sadbhavnacd"].ToString();
						TinNo=SqlDtr["Tin_No"].ToString();
					}
					addr=addr.ToUpper();
					if(addr.Length>50)
						addr=addr.Substring(0,49);
			
					if(PartyName)
					{
						if(DocumentNo)
						{
							if(lblInvoiceNo.Visible==true)
							{
								if(ssc!="")
									sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","",lblInvoiceNo.Text);
								else
									sw.WriteLine(info21,"",text1.Value.ToUpper(),"",lblInvoiceNo.Text);
							}
							else
							{
								if(ssc!="")	
									sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","",dropInvoiceNo.SelectedItem.Text);
								else
									sw.WriteLine(info21,"",text1.Value.ToUpper(),"",dropInvoiceNo.SelectedItem.Text);
							}
						}
						else
						{
							if(lblInvoiceNo.Visible==true)
							{
								if(ssc!="")
									sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","","");
								else
									sw.WriteLine(info21,"",text1.Value.ToUpper(),"","");
							}
							else
							{
								if(ssc!="")	
									sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","","");
								else
									sw.WriteLine(info21,"",text1.Value.ToUpper(),"","");
							}
						}
					}
					else if(DocumentNo)
					{
						if(lblInvoiceNo.Visible==true)
							sw.WriteLine(info21,"","","",lblInvoiceNo.Text);
						else
							sw.WriteLine(info21,"","","",dropInvoiceNo.SelectedItem.Text);
					}
					if(Address)
					{
						if(DtTime)
							sw.WriteLine(info21,"",addr,"",lblInvoiceDate.Text+" "+InvoiceDate[1]);
						else if(Date)
							sw.WriteLine(info21,"",addr,"",lblInvoiceDate.Text);
						else
							sw.WriteLine(info21,"",addr,"","");
					}
					else if(DtTime)
						sw.WriteLine(info21,"","","",lblInvoiceDate.Text+" "+InvoiceDate[1]);
					if(Time)
						sw.WriteLine(info21,"","","",InvoiceDate[1]);
					if(City)
					{
						if(DueDate)
							sw.WriteLine(info21,"City",lblPlace.Value.ToUpper(),"",lblDueDate.Value);
						else
							sw.WriteLine(info21,"City",lblPlace.Value.ToUpper(),"","");
					}
					else if(DueDate)
						sw.WriteLine(info21,"","","",lblDueDate.Value);
					if(Tin_No)
					{
						if(VehicleNo)
							sw.WriteLine(info21,"Tin No",TinNo,"",txtVehicleNo.Text);
						else
							sw.WriteLine(info21,"Tin No",TinNo,"","");
					}
					else if(VehicleNo)
						sw.WriteLine(info21,"","","",txtVehicleNo.Text);
					if(Blank)
						sw.WriteLine();
					if(Blank1)
						sw.WriteLine();
					sw.WriteLine(info31,"P-Code","  Batch No"," Grade/Package Name","B-Qty","F-Qty"," D-Qty"," Ltr/Kg"," Rate Rs."," Sch Disc."," Amount (Rs.)");
					sw.WriteLine("");
					for(k=arrCount;k<arrBillQty.Count;k++,arrCount++)
					{
						sw.WriteLine(info31,arrProdCode[k].ToString(),arrBatchNo[k].ToString(),GenUtil.TrimLength(arrProdName[k].ToString(),34),arrBillQty[k].ToString(),arrFreeQty[k].ToString(),arrDespQty[k].ToString(),arrLtrkg[k].ToString(),arrProdRate[k].ToString(),arrProdScheme[k].ToString(),arrProdAmount[k].ToString());
						if(k==bh-10 && arrBillQty.Count<bh-2)
						{
							FlagCount=true;
						}
						if(k==bh-2)
						{
							FlagCount=true;
							arrCount++;
							break;
						}
						if(k==(bh*2)-3)
						{
							FlagCount=true;
							arrCount++;
							break;
						}
					}
				
					Space=SpaceCount-(bh-2);
					if(Space>0)
					{
						SpaceCount-=(bh-2);
						for(int r=0;r<=(bh-10);r++)
						{
							sw.WriteLine();
						}
					}
					else
					{
						Space=Math.Abs(Space);
						if(Space>=8)
						{
							for(int r=8;r<=Space;r++)
							{
								sw.WriteLine();
							}
						}
						else
						{
							for(int r=0;r<=Space+f2;r++)
							{
								sw.WriteLine();
							}
						}
						SpaceCount=0;
					}
				}while(FlagCount==true);
				sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
				sw.WriteLine(info4,"","Packs","Ltrs","GROSS AMOUNT         : ",txtGrandTotal.Text);
				sw.WriteLine(info4,"","----------","----------","FREE/SCH DISC        : ","-"+txtschemetotal.Text);
				if(txtfleetoediscountRs.Text=="" || txtfleetoediscountRs.Text=="0")
					sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txtliter.Text,"","");
				else
					sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txtliter.Text,"Oe/Fleet Discount    : ","-"+txtfleetoediscountRs.Text);
				if(txtDisc.Text=="" || txtDisc.Text=="0")
					sw.WriteLine(info4,"","","","Discount             : ","0");
				else
				{
					if(DropDiscType.SelectedItem.Text.Equals("%"))
						sw.WriteLine(info4,"","","","Discount("+txtDisc.Text+DropDiscType.SelectedItem.Text+")      : ","-"+tempdiscount.Value);
					else
						sw.WriteLine(info4,"","","","Discount("+DropDiscType.SelectedItem.Text+")        : ","-"+txtDisc.Text);
				}
				if(txtCashDisc.Text=="" || txtCashDisc.Text=="0")
					sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount        : ","0");
				else
				{
					if(DropCashDiscType.SelectedItem.Text.Equals("%"))
						sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount("+txtCashDisc.Text+DropCashDiscType.SelectedItem.Text+") : ","-"+tempcashdis.Value);
					else
						sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount("+DropCashDiscType.SelectedItem.Text+")   : ","-"+txtCashDisc.Text);
				}
				sw.WriteLine(info4,"","----------","----------","Vat Amount(@"+txtVatRate.Value+")    : ",txtVAT.Text);
				sw.WriteLine(info4,"Total Qty",System.Convert.ToString(TotalQtyfoe+TotalQtyPack),System.Convert.ToString(System.Convert.ToDouble(txtliter.Text)+TotalfoeLtr),"Net Amount           : ",txtNetAmount.Text);
				sw.WriteLine(info51,"",GenUtil.ConvertNoToWord(txtNetAmount.Text));
				sw.WriteLine(info61,"",CurrBal[0],"(INCLUDING CURRENT INVOICE AMOUNT)");
				sw.WriteLine(info71,"",txtRemark.Text);
				sw.Close();		
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ModvatCenvatInvoice.aspx,Method:PrePrintReport().  EXCEPTION: "+ ex.Message+"  user "+uid);
			}
		}

		/// <summary>
		/// This method saves the sales details into Order_Col_Details table.
		/// </summary>
		/// <param name="ProdName"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		/// <param name="Rate"></param>
		/// <param name="Amount"></param>
		/// <param name="Qty1"></param>
		/// <param name="Order_Date"></param>
		/// <param name="scheme"></param>
		/// <param name="foe"></param>
		/// <param name="sno"></param>
		public void Save(string ProdName,string PackType, string Qty, string Rate,string Amount,string Qty1,string Order_Date,string scheme,string foe,int sno)
		{
			InventoryClass obj=new InventoryClass();
			obj.Product_Name=ProdName ;
			obj.Package_Type=PackType ;
			obj.Qty=Qty;
			obj.QtyTemp = Qty1; 
			obj.Rate=Rate;
			obj.sno=sno+=1;
			obj.Amount=Amount;
			obj.Order_Date= System.Convert.ToDateTime(Order_Date);
			obj.sch=scheme;
			if(foe=="")
				obj.foe="0";
			else
				obj.foe=foe;
			if(lblInvoiceNo.Visible==true)
			{
				obj.Order_No=lblInvoiceNo.Text;
				obj.InsertOrderDetail();
			}
			else
			{
				obj.Order_No=dropInvoiceNo.SelectedItem.Value;
				obj.InsertOrderDetail(); 
			}
		}

		/// <summary>
		/// This method saves the FOC products sales info into Sales_Details table.
		/// </summary>
		/// <param name="ProdName"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		/// <param name="Order_Date"></param>
		/// <param name="scheme"></param>
		/// <param name="sno"></param>
		/// <param name="Qty1"></param>
		public void Save1(string ProdName,string PackType, string Qty,string Order_Date,string scheme,int sno,string Qty1)
		{
			InventoryClass obj=new InventoryClass();
			obj.Product_Name=ProdName ;
			obj.Package_Type=PackType ;
			obj.sno=sno;
			if(Qty.Equals(""))
				obj.Qty="0";
			else
				obj.Qty=Qty;
			//obj.tempQty=Qty1;
			obj.QtyTemp=Qty1;
			obj.Rate="";
			obj.Amount="";
			obj.sch="";
			obj.foe="";
			obj.Order_Date = System.Convert.ToDateTime(Order_Date);
			if(lblInvoiceNo.Visible==true)
			{
				obj.Order_No=lblInvoiceNo.Text;
				obj.InsertOrderDetail();
			}
			else
			{
				obj.Order_No=dropInvoiceNo.SelectedItem.Value;
				obj.InsertOrderDetail();
			}
		}
	}
}
