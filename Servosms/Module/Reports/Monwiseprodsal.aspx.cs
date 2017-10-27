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
	/// Summary description for Monwiseprodsal.
	/// </summary>
	public partial class Monwiseprodsal : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label jan;
		protected System.Web.UI.WebControls.Label feb;
		protected System.Web.UI.WebControls.Label mar;
		protected System.Web.UI.WebControls.Label april;
		protected System.Web.UI.WebControls.Label may;
		protected System.Web.UI.WebControls.Label june;
		protected System.Web.UI.WebControls.Label july;
		protected System.Web.UI.WebControls.Label august;
		protected System.Web.UI.WebControls.Label sept;
		protected System.Web.UI.WebControls.Label oct;
		protected System.Web.UI.WebControls.Label nav;
		protected System.Web.UI.WebControls.Label des;
		protected System.Web.UI.WebControls.Button BtnPrint1;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Panel Panel2;
		protected System.Web.UI.WebControls.Panel Panel3;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.DataGrid Datagrid3;
		protected System.Web.UI.WebControls.Panel Panel4;
		protected System.Web.UI.WebControls.DataGrid Datagrid4;
		protected System.Web.UI.WebControls.Panel Panel5;
		protected System.Web.UI.WebControls.DataGrid Datagrid5;
		protected System.Web.UI.WebControls.Panel Panel6;
		protected System.Web.UI.WebControls.DataGrid Datagrid6;
		protected System.Web.UI.WebControls.Panel Panel7;
		protected System.Web.UI.WebControls.DataGrid Datagrid7;
		protected System.Web.UI.WebControls.Panel Panel8;
		protected System.Web.UI.WebControls.DataGrid Datagrid8;
		protected System.Web.UI.WebControls.Panel Panel9;
		protected System.Web.UI.WebControls.DataGrid Datagrid9;
		protected System.Web.UI.WebControls.Panel Panel10;
		protected System.Web.UI.WebControls.DataGrid Datagrid10;
		protected System.Web.UI.WebControls.Panel Panel11;
		protected System.Web.UI.WebControls.DataGrid Datagrid11;
		protected System.Web.UI.WebControls.Panel Panel12;
		protected System.Web.UI.WebControls.DataGrid Datagrid12;
		protected System.Web.UI.WebControls.Panel Panel13;
		protected System.Web.UI.WebControls.DataGrid Datagrid13;
		protected System.Web.UI.WebControls.Panel Panel14;
		protected System.Web.UI.WebControls.DataGrid Datagrid14;
		protected System.Web.UI.WebControls.Panel Panel15;
		protected System.Web.UI.WebControls.DataGrid Datagrid15;
		protected System.Web.UI.WebControls.Panel Panel16;
		protected System.Web.UI.WebControls.DataGrid Datagrid16;
		protected System.Web.UI.WebControls.Panel Panel17;
		protected System.Web.UI.WebControls.DataGrid Datagrid17;
		protected System.Web.UI.WebControls.Panel Panel18;
		protected System.Web.UI.WebControls.DataGrid Datagrid18;
		protected System.Web.UI.WebControls.Panel Panel19;
		protected System.Web.UI.WebControls.DataGrid Datagrid19;
		protected System.Web.UI.WebControls.Panel Panel20;
		protected System.Web.UI.WebControls.DataGrid Datagrid20;
		protected System.Web.UI.WebControls.Panel Panel21;
		protected System.Web.UI.WebControls.DataGrid Datagrid21;
		protected System.Web.UI.WebControls.Panel Panel22;
		protected System.Web.UI.WebControls.DataGrid Datagrid22;
		protected System.Web.UI.WebControls.Panel Panel23;
		protected System.Web.UI.WebControls.DataGrid Datagrid23;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.Panel Panel24;
		protected System.Web.UI.WebControls.DataGrid Datagrid24;
		protected System.Web.UI.WebControls.DropDownList Dropstate;
		string UID;
		//		string qry1;
		//		string qry2;
		//		string qry3;
		//		string qry4;
		//		string qry5;
		//		string qry6;
		//		string qry7;
		//		string qry8;
		//		string qry9;
		//		string qry10;
		//		string qry11;
		//		string qry12;
		//		string qry13;
		//		string qry14;
		//		string qry15;
		//		string qry16;
		//		string qry17;
		//		string qry18;
		//		string qry19;
		//		string qry20;
		//		string qry21;
		//		string qry22;
		//		string qry23;
		//		string qry24;
		public int ds11;
		public int ds12;
		public int ds21;
		public int ds22;
		public int ds20;
		public int ds10;
		//		public double Total_baazar = 0;
		//		public double Total_ro = 0;
		//		public double Total_oe = 0;
		//		public double Total_fleet = 0;
		//		public double Total_ibp= 0;
		//		public double Total_total=0;
		//		public double baazar = 0;
		//		public double ro = 0;
		//		public double oe = 0;
		//		public double fleet = 0;
		//		public double ibp= 0;
		//		public double total=0;
		public static string[] arrProductList= new string[0];
		public static string[,] arrProduct=null;
		public static string[] DateFrom = null;
		public static string[] DateTo = null;
		public static int count=0;
		public static int View = 0;
		public static double[] GrantTotal = null;
		//public static ArrayList RowCounter = new ArrayList();
		//System.Globalization.NumberStyles  nfi = new System.Globalization.NumberStyles("en-US",false).ToString;
		System.Globalization.NumberFormatInfo  nfi = new System.Globalization.CultureInfo("en-US",false).NumberFormat;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				UID=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Monthwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method: page_load " + ex.Message+"  EXCEPTION " +" userid  "+UID);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				View=0;
				count=0;
				txtDateTo.Text=GenUtil.str2DDMMYYYY(DateTime.Now.ToShortDateString());
				txtDateFrom.Text=GenUtil.str2DDMMYYYY(DateTime.Now.ToShortDateString());
				//				Panel[] pan={Panel1,Panel2,Panel3,Panel4,Panel5,Panel6,Panel7,Panel8,Panel9,Panel10,Panel11,Panel12,Panel13,Panel14,Panel15,Panel16,Panel17,Panel18,Panel19,Panel20,Panel21,Panel22,Panel23,Panel24};
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="24";
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
					//	string msg="UnAthourized Visit to Machine Report Page";
					//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				#endregion
				//				SqlDataReader rdr=null;
				//				dbobj.SelectQuery("select distinct c.state from customer c,sales_master sm where c.cust_id=sm.cust_id",ref rdr);
				//				DropDistrict.Items.Clear();
				//				DropDistrict.Items.Add("All");
				//				while(rdr.Read())
				//				{
				//					DropDistrict.Items.Add(rdr.GetValue(0).ToString());
				//				}
				//				
				//				dbobj.Dispose();
				//				rdr.Close();
				//				for(int ii=0;ii<24;ii++)
				//				{
				//					pan[ii].Visible=false;
				//				}
				//				CreateLogFiles.ErrorLog("Form:Monthwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method: page_load,  userid  "+UID);
				//				//****************
				//				string[ , ] arrProduct = new string[200,7];
				//				//* *************
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
		/// This method is used to return the max date of the given maonth in 'MM/dd/yyyy'
		/// </summary>
		public static string month(string s1)
		{
			string[] ds1 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);

			ds1[0]="31";
			return ds1[1] + "/" + ds1[0] + "/" + ds1[2];	
		}
		
		/// <summary>
		/// This method is used to view the Report with calculate total qty in liter of all customer type 
		/// according to products with month wise in given period.
		/// </summary>
		protected void btnview1_Click(object sender, System.EventArgs e)
		{
            /*
			try
			{
				DataGrid[] dgarr={DataGrid1,Datagrid2,Datagrid3,Datagrid4,Datagrid5,Datagrid6,Datagrid7,Datagrid8,Datagrid9,Datagrid10,Datagrid11,Datagrid12};
				Panel[] pan={Panel1,Panel2,Panel3,Panel4,Panel5,Panel6,Panel7,Panel8,Panel9,Panel10,Panel11,Panel12};
				DataGrid[] dgarr1={Datagrid13,Datagrid14,Datagrid15,Datagrid16,Datagrid17,Datagrid18,Datagrid19,Datagrid20,Datagrid21,Datagrid22,Datagrid23,Datagrid24};
				Panel[] pan1={Panel13,Panel14,Panel15,Panel16,Panel17,Panel18,Panel19,Panel20,Panel21,Panel22,Panel23,Panel24};
				int[] c={0,0,0,0,0,0,0,0,0,0,0,0};
				int[] c1={0,0,0,0,0,0,0,0,0,0,0,0};
				for(int y=0;y<12;y++)
				{
					pan[y].Visible=false;
					pan1[y].Visible=false;
				}
				
				string s1="";
				string s2="";
				s1=txtDateTo1.Text;
				s2=txtDateFrom1.Text;
				string[] ds1 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length):s1.Split(new char[] {'-'},s1.Length);
				string[] ds2 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s1.Length):s2.Split(new char[] {'-'},s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					return;
				}
				if((ds22-ds12) > 1)
				{
					MessageBox.Show("Please Select date between one year");
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					return;
				}
				//********************************************
				object obj=null;
				SqlDataReader rdr1=null;
				int x1 =0;
				dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertMonthWise1",ref obj,"@ToDate",GenUtil.str2MMDDYYYY(txtDateFrom1.Text),"@FromDate",GenUtil.str2MMDDYYYY(txtDateTo1.Text),"@State",Dropstate.SelectedItem.Text);
				//for bazzar
				dbobj1.SelectQuery("select sm.invoice_no invoice_no,sd.prod_id,sum(qty) qty,sm.invoice_date invoice_date,p.Total_Qty from sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and sm.cust_id in (select cust_id from customer where cust_type like'bazzar%') and cast(floor(cast(sm.invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and cast(floor(cast(sm.invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by sd.prod_id,sm.invoice_no,sm.invoice_date,p.Total_Qty order by sd.prod_id",ref rdr1);
				while(rdr1.Read())
				{
					dbobj.Insert_or_Update("update monthwise1 set bazzar=bazzar+'"+double.Parse(rdr1["Total_Qty"].ToString())*double.Parse(rdr1["qty"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["invoice_date"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
				}
				rdr1.Close();
				//for ro
				//dbobj1.SelectQuery("select sm.invoice_no invoice_no,prod_id,sum(qty) qty,sm.invoice_date invoice_date from sales_details sd,sales_master sm where sd.invoice_no=sm.invoice_no and sm.cust_id in (select cust_id from customer where cust_type like'ro%') and sm.invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and sm.invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by prod_id,sm.invoice_no,sm.invoice_date order by prod_id",ref rdr1);
				dbobj1.SelectQuery("select sm.invoice_no invoice_no,sd.prod_id,sum(qty) qty,sm.invoice_date invoice_date,p.Total_Qty from sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and sm.cust_id in (select cust_id from customer where cust_type like'ro%') and cast(floor(cast(sm.invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and cast(floor(cast(sm.invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by sd.prod_id,sm.invoice_no,sm.invoice_date,p.Total_Qty order by sd.prod_id",ref rdr1);
				while(rdr1.Read())
				{
					//dbobj.Insert_or_Update("update monthwise1 set ro='"+GenUtil.changeqtyltrwithProdID(rdr1["prod_id"].ToString(),rdr1["qty"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+GenUtil.trimDate(rdr1["invoice_date"].ToString())+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
					dbobj.Insert_or_Update("update monthwise1 set ro=ro+'"+double.Parse(rdr1["Total_Qty"].ToString())*double.Parse(rdr1["qty"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["invoice_date"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
				}
				rdr1.Close();
				//for oe
				//dbobj1.SelectQuery("select sm.invoice_no invoice_no,prod_id,sum(qty) qty,sm.invoice_date invoice_date from sales_details sd,sales_master sm where sd.invoice_no=sm.invoice_no and sm.cust_id in (select cust_id from customer where cust_type like'oe%') and sm.invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and sm.invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by prod_id,sm.invoice_no,sm.invoice_date order by prod_id",ref rdr1);
				dbobj1.SelectQuery("select sm.invoice_no invoice_no,sd.prod_id,sum(qty) qty,sm.invoice_date invoice_date,p.Total_Qty from sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and sm.cust_id in (select cust_id from customer where cust_type like'oe%') and cast(floor(cast(sm.invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and cast(floor(cast(sm.invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by sd.prod_id,sm.invoice_no,sm.invoice_date,p.Total_Qty order by sd.prod_id",ref rdr1);
				while(rdr1.Read())
				{
					dbobj.Insert_or_Update("update monthwise1 set oe=oe+'"+double.Parse(rdr1["Total_Qty"].ToString())*double.Parse(rdr1["qty"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["invoice_date"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
				}
				rdr1.Close();
				//for fleet
				//dbobj1.SelectQuery("select sm.invoice_no invoice_no,prod_id,sum(qty) qty,sm.invoice_date invoice_date from sales_details sd,sales_master sm where sd.invoice_no=sm.invoice_no and sm.cust_id in (select cust_id from customer where cust_type like'fleet%') and sm.invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and sm.invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by prod_id,sm.invoice_no,sm.invoice_date order by prod_id",ref rdr1);
				dbobj1.SelectQuery("select sm.invoice_no invoice_no,sd.prod_id,sum(qty) qty,sm.invoice_date invoice_date,p.Total_Qty from sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and sm.cust_id in (select cust_id from customer where cust_type like'fleet%') and cast(floor(cast(sm.invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and cast(floor(cast(sm.invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by sd.prod_id,sm.invoice_no,sm.invoice_date,p.Total_Qty order by sd.prod_id",ref rdr1);
				while(rdr1.Read())
				{
					dbobj.Insert_or_Update("update monthwise1 set fleet=fleet+'"+double.Parse(rdr1["Total_Qty"].ToString())*double.Parse(rdr1["qty"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["invoice_date"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
				}
				rdr1.Close();
				//for IBP
				//dbobj1.SelectQuery("select sm.invoice_no invoice_no,prod_id,sum(qty) qty,sm.invoice_date invoice_date from sales_details sd,sales_master sm where sd.invoice_no=sm.invoice_no and sm.cust_id in (select cust_id from customer where cust_type like'ibp%') and sm.invoice_date>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and sm.invoice_date<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by prod_id,sm.invoice_no,sm.invoice_date order by prod_id",ref rdr1);
				dbobj1.SelectQuery("select sm.invoice_no invoice_no,sd.prod_id,sum(qty) qty,sm.invoice_date invoice_date,p.Total_Qty from sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and sm.cust_id in (select cust_id from customer where cust_type like'ibp%') and cast(floor(cast(sm.invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo1.Text)+"' and cast(floor(cast(sm.invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom1.Text)+"' group by sd.prod_id,sm.invoice_no,sm.invoice_date,p.Total_Qty order by sd.prod_id",ref rdr1);
				while(rdr1.Read())
				{
					dbobj.Insert_or_Update("update monthwise1 set ibp=ibp+'"+double.Parse(rdr1["Total_Qty"].ToString())*double.Parse(rdr1["qty"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["invoice_date"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
				}
				rdr1.Close();
				dbobj1.SelectQuery("select * from monthwise1",ref rdr1);
				while(rdr1.Read())
				{
					//dbobj.Insert_or_Update("update monthwise1 set tt="+double.Parse(rdr1["bazzar"].ToString())+"+"+double.Parse(rdr1["ro"].ToString())+"+"+double.Parse(rdr1["oe"].ToString())+"+"+double.Parse(rdr1["fleet"].ToString())+"+"+double.Parse(rdr1["ibp"].ToString())+" where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["date_in"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
					dbobj.Insert_or_Update("update monthwise1 set date_in='"+GenUtil.trimDate(rdr1["date_in"].ToString())+"' where invoice_no='"+rdr1["invoice_no"].ToString()+"' and date_in='"+rdr1["date_in"].ToString()+"' and prod_id='"+rdr1["prod_id"].ToString()+"'",ref x1);
				}
				rdr1.Close();
				//return;
				//********************************************
				if(ds12==ds22 && ds11 <= ds21)
				{
					if(Dropstate.SelectedItem.Text.Equals("All"))
					{
						//qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					else
					{
						//qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					if(ds11 == ds21)
					{
						pan[ds11-1].Visible=true;
					}
					else
					{
						for(int j=ds11;j<=ds21;j++)
						{
							pan[j-1].Visible=true;
						}
					}
				}

				if((ds22-ds12)==1 && ds21 <ds11)
				{
					for(int x=ds11;x<=12;x++)
					{
						pan[x-1].Visible=true;
					}
					if(Dropstate.SelectedItem.Text.Equals("All"))
					{
						//qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					else
					{
						//qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					for(int w=1;w<=ds21;w++)
					{
						pan1[w-1].Visible=true;
					}
					if(Dropstate.SelectedItem.Text.Equals("All"))
					{
						//qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						
						qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					else
					{
						//qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						//qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
						
						qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
				}
				SqlConnection con1;
				con1=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlDataReader dtr;
				con1.Open ();
				SqlCommand cmd=new SqlCommand(qry1,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[0]++;
				dgarr[0].DataSource=dtr;
				dgarr[0].DataBind();
				con1.Close();
				dtr.Close();
				cmd.Dispose();
				con1.Open ();
				cmd=new SqlCommand(qry2,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[1]++;
				dgarr[1].DataSource=dtr;
				dgarr[1].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry3,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[2]++;
				dgarr[2].DataSource=dtr;
				dgarr[2].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry4,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[3]++;
				dgarr[3].DataSource=dtr;
				dgarr[3].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry5,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[4]++;
				dgarr[4].DataSource=dtr;
				dgarr[4].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry6,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[5]++;
				dgarr[5].DataSource=dtr;
				dgarr[5].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry7,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[6]++;
				dgarr[6].DataSource=dtr;
				dgarr[6].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry8,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[7]++;
				dgarr[7].DataSource=dtr;
				dgarr[7].DataBind();
				con1.Close();
				dtr.Close();
				cmd.Dispose();
				con1.Open ();
				cmd=new SqlCommand(qry9,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[8]++;
				dgarr[8].DataSource=dtr;
				dgarr[8].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry10,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[9]++;
				dgarr[9].DataSource=dtr;
				dgarr[9].DataBind();
				con1.Close();
				dtr.Close();
				cmd.Dispose();
				con1.Open ();
				cmd=new SqlCommand(qry11,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[10]++;
				dgarr[10].DataSource=dtr;
				dgarr[10].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry12,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[11]++;
				dgarr[11].DataSource=dtr;
				dgarr[11].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();

				if((ds22-ds12)==1 && ds21 <ds11)
				{
					SqlDataReader dtr1;
					con1.Open ();
					cmd=new SqlCommand(qry13,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[0]++;
					dgarr1[0].DataSource=dtr1;
					dgarr1[0].DataBind();
					con1.Close();
					dtr1.Close();
					cmd.Dispose();
					con1.Open ();
					cmd=new SqlCommand(qry14,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[1]++;
					dgarr1[1].DataSource=dtr1;
					dgarr1[1].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry15,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[2]++;
					dgarr1[2].DataSource=dtr1;
					dgarr1[2].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry16,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[3]++;
					dgarr1[3].DataSource=dtr1;
					dgarr1[3].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry17,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[4]++;
					dgarr1[4].DataSource=dtr1;
					dgarr1[4].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry18,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[5]++;
					dgarr1[5].DataSource=dtr1;
					dgarr1[5].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry19,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[6]++;
					dgarr1[6].DataSource=dtr1;
					dgarr1[6].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry20,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[7]++;
					dgarr1[7].DataSource=dtr1;
					dgarr1[7].DataBind();
					con1.Close();
					dtr1.Close();
					cmd.Dispose();
					con1.Open ();
					cmd=new SqlCommand(qry21,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[8]++;
					dgarr1[8].DataSource=dtr1;
					dgarr1[8].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry22,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[9]++;
					dgarr1[9].DataSource=dtr1;
					dgarr1[9].DataBind();
					con1.Close();
					dtr1.Close();
					cmd.Dispose();
					con1.Open ();
					cmd=new SqlCommand(qry23,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[10]++;
					dgarr1[10].DataSource=dtr1;
					dgarr1[10].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry24,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[11]++;
					dgarr1[11].DataSource=dtr1;
					dgarr1[11].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();


				}
				
				for(int l=0;l<12;l++)
				{
					if(c[l]==0)
						pan[l].Visible=false;
					else
						dgarr[l].Visible=true;
					if(c1[l]==0)
						pan1[l].Visible=false;
					else
						dgarr1[l].Visible=true;
				}
				int yy=0;
				for(int xx=0;xx<12;xx++)
				{
					if((pan[xx].Visible.Equals(false)) && (pan1[xx].Visible.Equals(false)))
						yy++;
				}
				if(yy==12)
				{
					MessageBox.Show("Data Not Available");
					return;
				}
				// truncate table after use.
				//dbobj.Insert_or_Update("truncate table monthwise1", ref x1);
					 CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Method:btnView, userid  "+UID );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+UID );
			}*/
            try
            {
				string s1="";
				string s2="";
				s1=txtDateTo.Text;
				s2=txtDateFrom.Text;
				//SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//SqlCommand cmd;
				//RowCounter.RemoveRange(0,RowCounter.Count);
				string[] ds1 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					return;
				}
				if((ds22-ds12) > 1)
				{
					MessageBox.Show("Please Select date between one year");
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					return;
				}
				SqlDataReader rdr = null;
				
				int Count=0;
				string[] arrProductID=null;
				if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					//dbobj.SelectQuery("select count(*) from products",ref rdr);
					dbobj.SelectQuery("select count(distinct p.prod_id) from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'",ref rdr);
				else if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value=="All")
						//18.07.09 vikas dbobj.SelectQuery("select count(*) from Products",ref rdr);
						dbobj.SelectQuery("select count(distinct p.prod_id) from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'",ref rdr);
					else
						dbobj.SelectQuery("select count(*) from Products where category='"+DropValue.Value+"'",ref rdr);
						//dbobj.SelectQuery("select count(*) from products where prod_id in(select prod_id from sales_details sd,sales_master sm,customer c where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and c.state='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"')",ref rdr);
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
						//18.07.09 vikas dbobj.SelectQuery("select count(*) from Products",ref rdr);
						dbobj.SelectQuery("select count(distinct p.prod_id) from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'",ref rdr);
					else
						dbobj.SelectQuery("select count(*) from products where prod_id in(select prod_id from sales_details sd,sales_master sm,customer c where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and c.state='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"')",ref rdr);
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
						//18.07.09 vikas dbobj.SelectQuery("select count(*) from products",ref rdr);
						dbobj.SelectQuery("select count(distinct p.prod_id) from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'",ref rdr);
					else
					{
						string[] arrProd = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						dbobj.SelectQuery("select count(*) from products where Prod_Name='"+arrProd[0].ToString()+"' and Pack_Type='"+arrProd[1].ToString()+"'",ref rdr);
					}
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
						//18.07.09 vikas dbobj.SelectQuery("select count(*) from products",ref rdr);
						dbobj.SelectQuery("select count(distinct p.prod_id) from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"'",ref rdr);
					else
						//dbobj.SelectQuery("select count(*) from products where prod_id in(select prod_id from sales_details sd,sales_master sm,customer c where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and sm.under_salesman='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"')",ref rdr);
						dbobj.SelectQuery("select count(*) from products where prod_id in(select prod_id from sales_details sd,sales_master sm,customer c where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"')",ref rdr);
				}
				if(rdr.Read())
				{
					Count = int.Parse(rdr.GetValue(0).ToString());
					arrProductList = new string[Count];
					arrProductID = new string[Count];
				}
				else
				{
					MessageBox.Show("Data Not Available");
					return;
				}
				rdr.Close();
				if(Count==0)
				{
					MessageBox.Show("Data Not Available");
					return;
				}
				getDate(ds10,ds11,ds12,ds20,ds21,ds22);
				arrProduct = new string[Count,8*(DateFrom.Length)];
				for(int p=0;p<arrProduct.GetLength(0);p++)
				{
					for(int q=0;q<arrProduct.GetLength(1);q++)
					{
						arrProduct[p,q]="0";
					}
				}
				//dbobj.SelectQuery("select Prod_ID,prod_name+':'+pack_type ProdName from products order by prod_id",ref rdr);
				if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
					//dbobj.SelectQuery("select Prod_ID,prod_name+':'+pack_type ProdName from products order by Prod_Name",ref rdr);
					dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
				else if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value=="All")
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
					else
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and category='"+DropValue.Value+"' and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
					else
						//dbobj.SelectQuery("select Prod_ID,prod_name+':'+pack_type ProdName from products where prod_id in(select prod_id from sales_details sd,sales_master sm,customer c where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and c.state='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"') order by Prod_Name",ref rdr);
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where c.state='"+DropValue.Value+"' and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
				}
					
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
					else
					{
						string[] arrProd = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						//dbobj.SelectQuery("select Prod_ID,prod_name+':'+pack_type ProdName from products where Prod_Name='"+arrProd[0].ToString()+"' and Pack_Type='"+arrProd[1].ToString()+"' order by Prod_Name",ref rdr);
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where Prod_Name='"+arrProd[0].ToString()+"' and Pack_Type='"+arrProd[1].ToString()+"' and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
					}
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
					else
						//dbobj.SelectQuery("select Prod_ID,prod_name+':'+pack_type ProdName from products where prod_id in(select prod_id from sales_details sd,sales_master sm,customer c where c.cust_id=sm.cust_id and sm.invoice_no=sd.invoice_no and sm.under_salesman='"+DropValue.Value+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"') order by Prod_Name",ref rdr);
						//dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where sm.under_salesman='"+DropValue.Value+"' and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
						dbobj.SelectQuery("select distinct p.Prod_ID,prod_name+':'+pack_type ProdName from sales_master sm,customer c,sales_details sd,products p where ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()) +"' order by prodname",ref rdr);
				}
				int c=0;
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						arrProductList[c] = rdr["ProdName"].ToString();
						arrProductID[c] = rdr["Prod_ID"].ToString();
						c++;
					}
				}
				rdr.Close();
				
				GrantTotal = new double[8*(DateFrom.Length)];

				if(DropSearchBy.SelectedIndex==4 && DropValue.Value!="All")
				{

					for(int n=0;n<DateFrom.Length;n++)
					{
					
						for(int p=0;p<arrProductID.Length;p++)
						{
						
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'ksk%' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,0+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[0+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							
							}
							else
							{
								GrantTotal[0+(n*8)]+=0;
							}
							rdr.Close();
						
							dbobj.SelectQuery("select sum(qty)*p.total_qty TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'n%' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty order by sd.prod_id",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,1+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[1+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							
							}
							else
							{
								GrantTotal[1+(n*8)]+=0;
							}
							rdr.Close();
						
							dbobj.SelectQuery("select sum(qty)*p.total_qty TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'Ibp%' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty order by sd.prod_id",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,2+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[2+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							
							}
							else
							{
								GrantTotal[2+(n*8)]+=0;
							}
							rdr.Close();
						
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type='Bazzar' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,3+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[3+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
						
							}
							else
							{
								GrantTotal[3+(n*8)]+=0;
							}
							rdr.Close();
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'Ro%' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,4+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[4+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							}
							else
							{
								GrantTotal[4+(n*8)]+=0;
							}
							rdr.Close();
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'Oe%' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,5+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[5+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							}
							else
							{
								GrantTotal[5+(n*8)]+=0;
							}
							rdr.Close();
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like 'Fleet%' and sm.under_salesman=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,6+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[6+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							}
							else
							{
								GrantTotal[6+(n*8)]+=0;
							}
							rdr.Close();
							arrProduct[p,7+(n*8)]=System.Convert.ToString(double.Parse(arrProduct[p,0+(n*8)].ToString())+double.Parse(arrProduct[p,1+(n*8)].ToString())+double.Parse(arrProduct[p,2+(n*8)].ToString())+double.Parse(arrProduct[p,3+(n*8)].ToString())+double.Parse(arrProduct[p,4+(n*8)].ToString())+double.Parse(arrProduct[p,5+(n*8)].ToString())+double.Parse(arrProduct[p,6+(n*8)].ToString()));
							GrantTotal[7+(n*8)]+=double.Parse(arrProduct[p,7+(n*8)].ToString());
						
						}
					}
				}
				else
				{
					for(int n=0;n<DateFrom.Length;n++)
					{
					
						for(int p=0;p<arrProductID.Length;p++)
						{
						
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'ksk%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,0+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[0+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							
							}
							else
							{
								GrantTotal[0+(n*8)]+=0;
							}
							rdr.Close();
						
							dbobj.SelectQuery("select sum(qty)*p.total_qty TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'n%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty order by sd.prod_id",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,1+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[1+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							
							}
							else
							{
								GrantTotal[1+(n*8)]+=0;
							}
							rdr.Close();
						
							dbobj.SelectQuery("select sum(qty)*p.total_qty TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'Ibp%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty order by sd.prod_id",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,2+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[2+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							
							}
							else
							{
								GrantTotal[2+(n*8)]+=0;
							}
							rdr.Close();
						
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type='Bazzar' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,3+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[3+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
						
							}
							else
							{
								GrantTotal[3+(n*8)]+=0;
							}
							rdr.Close();
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'Ro%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,4+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[4+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							}
							else
							{
								GrantTotal[4+(n*8)]+=0;
							}
							rdr.Close();
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like'Oe%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,5+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[5+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							}
							else
							{
								GrantTotal[5+(n*8)]+=0;
							}
							rdr.Close();
							dbobj.SelectQuery("select sum(qty*p.total_qty) TotalQty from sales_master sm,customer c,sales_details sd,products p where sm.cust_id=c.cust_id and sd.invoice_no=sm.invoice_no and p.prod_id=sd.prod_id and c.cust_type like 'Fleet%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[n].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[n].ToString()+"' and sd.prod_id='"+arrProductID[p].ToString()+"' group by sd.prod_id,Total_qty",ref rdr);
							if(rdr.Read())
							{
								arrProduct[p,6+(n*8)]=rdr.GetValue(0).ToString();
								GrantTotal[6+(n*8)]+=double.Parse(rdr.GetValue(0).ToString());
							}
							else
							{
								GrantTotal[6+(n*8)]+=0;
							}
							rdr.Close();
							arrProduct[p,7+(n*8)]=System.Convert.ToString(double.Parse(arrProduct[p,0+(n*8)].ToString())+double.Parse(arrProduct[p,1+(n*8)].ToString())+double.Parse(arrProduct[p,2+(n*8)].ToString())+double.Parse(arrProduct[p,3+(n*8)].ToString())+double.Parse(arrProduct[p,4+(n*8)].ToString())+double.Parse(arrProduct[p,5+(n*8)].ToString())+double.Parse(arrProduct[p,6+(n*8)].ToString()));
							GrantTotal[7+(n*8)]+=double.Parse(arrProduct[p,7+(n*8)].ToString());
						
						}
					}
				}
				View=1;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+UID );
			}
		}

		/// <summary>
		/// This method is used to create to and from date according to select the date from date picker.
		/// </summary>
		public void getDate(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count=To2-From2;
				DateFrom = new string[count+1];
				DateTo = new string[count+1];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
			}
			int c=0;
			if(From2<=To2)
			{
				for(int i=From2,j=0;i<=To2;i++,j++)
				{
					if(c==0)
					{
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
						c=1;
					}
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					if(i==To2)
					{
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
						c=2;
					}
					else
					{
						int day=DateTime.DaysInMonth(From3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
			else
			{
				for(int i=From2,j=0;i<=12;i++,j++)
				{
					if(c==0)
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					//					if(i==12)
					//						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+From3.ToString();
					//					else
					//					{
					int day=DateTime.DaysInMonth(From3,i);
					DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+From3.ToString();
					//					}
					c++;
				}
				for(int i=1,j=c;i<=To2;i++,j++)
				{
					//					if(i==1)
					//						DateFrom[j]=From2.ToString()+"/"+From1.ToString()+"/"+To3.ToString();
					//					else
					DateFrom[j]=i.ToString()+"/"+"1"+"/"+To3.ToString();
					if(i==To2)
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
					else
					{
						int day=DateTime.DaysInMonth(To3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
		}

		//******************************************
		//		// This increment the grand total by the passing value.
		//		protected void GrandTotal(double _grandtotal)
		//		{
		//			grand_total += _grandtotal; 
		//		}
		//
		//		//This increment the vat total by passing value.
		//		protected void VATTotal(double _vattotal)
		//		{
		//			vat_total  += _vattotal; 
		//		}
		
		//This increment the net total by passing value.
		//		protected void Totalbaazar(double _Totalbaazar)
		//		{
		//			Total_baazar  += _Totalbaazar; 
		//		}
		//		// This invrement the cash discount by passing value.
		//		protected void Totalro(double _Totalro)
		//		{
		//			Total_ro  += _Totalro; 
		//		}
		//
		//		// this increment the other discount by passing value.
		//		protected void Totaloe(double _Totaloe)
		//		{
		//			Total_oe  += _Totaloe; 
		//		}
		//		// this increment the foc discount by passing value.
		//		protected void Totalfleet(double _Totalfleet)
		//		{
		//			Total_fleet  += _Totalfleet; 
		//		}
		//		// this increment the trade discount by passing value.
		//		protected void Totalibp(double _Totalibp)
		//		{
		//			Total_ibp  += _Totalibp; 
		//		}
		//		// this increment the ebird discount by passing value.
		//		protected void Totaltotal(double _Totaltotal)
		//		{
		//			Total_total  += _Totaltotal; 
		//		}

		/// <summary>
		/// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem )  )
			{
				//				Totalbaazar(Double.Parse(e.Item.Cells[1].Text));
				//				Totalro(Double.Parse(e.Item.Cells[2].Text));
				//				Totaloe(Double.Parse(e.Item.Cells[3].Text));
				//				Totalfleet(Double.Parse(e.Item.Cells[4].Text));
				//				Totalibp(Double.Parse(e.Item.Cells[5].Text));
				//				Totaltotal(Double.Parse(e.Item.Cells[6].Text));
				
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
               
				//				e.Item.Cells[1].Text =Total_baazar.ToString("N",nfi);   
				//				e.Item.Cells[2].Text = Total_ro.ToString("N",nfi);  
				//				e.Item.Cells[3].Text = Total_oe.ToString("N",nfi);
				//				e.Item.Cells[4].Text =Total_fleet.ToString("N",nfi);
				//				e.Item.Cells[5].Text =Total_ibp.ToString("N",nfi);
				//				e.Item.Cells[6].Text =Total_total.ToString("N",nfi);
				//				e.Item.Cells[1].Text =System.Convert.ToString(Math.Round(double.Parse(Total_baazar.ToString("N",nfi)),1));   
				//				e.Item.Cells[2].Text = System.Convert.ToString(Math.Round(double.Parse(Total_ro.ToString("N",nfi)),1)); 
				//				e.Item.Cells[3].Text = System.Convert.ToString(Math.Round(double.Parse(Total_oe.ToString("N",nfi)),1));
				//				e.Item.Cells[4].Text =System.Convert.ToString(Math.Round(double.Parse(Total_fleet.ToString("N",nfi)),1));
				//				e.Item.Cells[5].Text =System.Convert.ToString(Math.Round(double.Parse(Total_ibp.ToString("N",nfi)),1));
				//				e.Item.Cells[6].Text =System.Convert.ToString(Math.Round(double.Parse(Total_total.ToString("N",nfi)),1));
				
				//				Total_baazar = 0;
				//				 Total_ro = 0;
				//				 Total_oe = 0;
				//				Total_fleet = 0;
				//				Total_ibp= 0;
				//				Total_total=0;
				
			}
		
		}
		
		public void Totalforprint(string baazar1,string ro1,string oe1,string fleet1,string ibp1,string total1)
		{
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			/*	Totalbaazar(Double.Parse(baazar1));
				Totalro(Double.Parse(ro1));
				Totaloe(Double.Parse(oe1));
				Totalfleet(Double.Parse(fleet1));
				Totalibp(Double.Parse(ibp1));
				Totaltotal(Double.Parse(total1));
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
			baazar =Math.Round(double.Parse(Total_baazar.ToString("N",nfi)),1);   
			ro= Math.Round(double.Parse(Total_ro.ToString("N",nfi)),1); 
			oe= Math.Round(double.Parse(Total_oe.ToString("N",nfi)),1);
			fleet=Math.Round(double.Parse(Total_fleet.ToString("N",nfi)),1);
			ibp=Math.Round(double.Parse(Total_ibp.ToString("N",nfi)),1);
			total=Math.Round(double.Parse(Total_total.ToString("N",nfi)),1);*/
			//			Totalbaazar(Double.Parse(baazar1));
			//			Totalro(Double.Parse(ro1));
			//			Totaloe(Double.Parse(oe1));
			//			Totalfleet(Double.Parse(fleet1));
			//			Totalibp(Double.Parse(ibp1));
			//			Totaltotal(Double.Parse(total1));
			// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
			//			baazar +=Math.Round(Double.Parse(baazar1),1);   
			//			ro+= Math.Round(Double.Parse(ro1),1); 
			//			oe+= Math.Round(Double.Parse(oe1),1);
			//			fleet+=Math.Round(Double.Parse(fleet1),1);
			//			ibp+=Math.Round(Double.Parse(ibp1),1);
			//			total+=Math.Round(Double.Parse(total1),1);
				
			//				
				
			//				Total_baazar = 0;
			//				Total_ro = 0;
			//				Total_oe = 0;
			//				Total_fleet = 0;
			//				Total_ibp= 0;
			//				Total_total=0;
				
			
		
		}
        //**********************************************

        //		public string monthj1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="1";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthj2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="1";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthfeb1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="2";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthfeb2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="2";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="28";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthm1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="3";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthm2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="3";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string montha1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="4";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string montha2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="4";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="30";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthmay1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="5";
        //			if((s[1]==System.Convert.ToString(ds11)) )
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthmay2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="5";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthjune1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="6";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthjune2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="6";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="30";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthjuly1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="7";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthjuly2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="7";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthau1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="8";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthau2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="8";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthsep1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="9";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthsep2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="9";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="30";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthoct1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="10";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthoct2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="10";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthnov1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="11";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthnov2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="11";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="30";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthdec1(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="12";
        //			if((s[1]==System.Convert.ToString(ds11)))
        //			{}
        //			else
        //				s[0]="1";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}
        //		public string monthdec2(string str)
        //		{
        //			string[] s=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //			s[1]="12";
        //			if((s[1]==System.Convert.ToString(ds21)))
        //			{}
        //			else
        //				s[0]="31";
        //			return s[1]+"/"+s[0]+"/"+s[2];
        //		}


        /****
		public string monthj1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="1";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthj2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="1";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthfeb1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="2";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthfeb2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="2";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="28";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthm1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="3";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthm2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="3";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string montha1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="4";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string montha2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="4";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="30";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthmay1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="5";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthmay2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="5";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthjune1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="6";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthjune2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="6";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="30";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthjuly1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="7";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthjuly2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="7";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthau1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="8";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthau2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="8";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthsep1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="9";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthsep2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="9";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="30";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthoct1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="10";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthoct2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="10";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthnov1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="11";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthnov2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="11";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="30";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		public string monthdec1(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="12";
			if((s[1]==System.Convert.ToString(ds11)) && (s[2]==System.Convert.ToString(ds12)))
			{}
			else
				s[0]="1";
			return s[1]+"/"+s[0]+"/"+s[2];
		}

		public string monthdec2(string str)
		{
			string[] s=str.Split(new char[]{'/'},str.Length);
			s[1]="12";
			if((s[1]==System.Convert.ToString(ds21)) && (s[2]==System.Convert.ToString(ds22)))
			{}
			else
				s[0]="31";
			return s[1]+"/"+s[0]+"/"+s[2];
		}
		****/

        /// <summary>
        /// Method to write into the excel report file to print.
        /// </summary>
        public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			//SqlDataReader SqlDtr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\MonthlyProductSecondarySales.xls";
			StreamWriter sw = new StreamWriter(path);
            //***********
            /*string qry1="",qry2="",qry3="",qry4="",qry5="",qry6="",qry7="",qry8="",qry9="",qry10="",qry11="",qry12="";
			string qry13="",qry14="",qry15="",qry16="",qry17="",qry18="",qry19="",qry20="",qry21="",qry22="",qry23="",qry24="";
			string s1="";
			string s2="";
			s1=txtDateTo1.Text;
			s2=txtDateFrom1.Text;
			string[] ds1 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length):s1.Split(new char[] {'-'},s1.Length);
			string[] ds2 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s1.Length):s2.Split(new char[] {'-'},s1.Length);
			ds10=System.Convert.ToInt32(ds1[0]);
			ds20=System.Convert.ToInt32(ds2[0]);
			ds11=System.Convert.ToInt32(ds1[1]);
			ds12=System.Convert.ToInt32(ds1[2]);
			ds21=System.Convert.ToInt32(ds2[1]);
			ds22=System.Convert.ToInt32(ds2[2]);
//			if(ds12==ds22 && ds11 <= ds21)
//			{
//				if(Dropstate.SelectedItem.Text.Equals("All"))
//				{
//					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//				}
//				else
//				{
//					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//				}
//				//if(ds11 == ds21)
//				//{
//				//	pan[ds11-1].Visible=true;
//				//}
//				//else
//				//{
//				//	for(int j=ds11;j<=ds21;j++)
//				//	{
//				//		pan[j-1].Visible=true;
//				//	}
//				//}
//			}
//
//			if((ds22-ds12)==1 && ds21 <ds11)
//			{
//				//for(int x=ds11;x<=12;x++)
//				//{
//				//	pan[x-1].Visible=true;
//				//}
//				if(Dropstate.SelectedItem.Text.Equals("All"))
//				{
//					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//				}
//				else
//				{
//					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//				}
//				//for(int w=1;w<=ds21;w++)
//				//{
//				//	pan1[w-1].Visible=true;
//				//}
//				if(Dropstate.SelectedItem.Text.Equals("All"))
//				{
//					qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//				}
//				else
//				{
//					qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//				}
//			}
			if(ds12==ds22 && ds11 <= ds21)
			{
				if(Dropstate.SelectedItem.Text.Equals("All"))
				{
					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
				}
				else
				{
					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
				}
//				if(ds11 == ds21)
//				{
//					pan[ds11-1].Visible=true;
//				}
//				else
//				{
//					for(int j=ds11;j<=ds21;j++)
//					{
//						pan[j-1].Visible=true;
//					}
//				}
			}

			if((ds22-ds12)==1 && ds21 <ds11)
			{
//				for(int x=ds11;x<=12;x++)
//				{
//					pan[x-1].Visible=true;
//				}
				if(Dropstate.SelectedItem.Text.Equals("All"))
				{
					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
				}
				else
				{
					qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
				}
//				for(int w=1;w<=ds21;w++)
//				{
//					pan1[w-1].Visible=true;
//				}
				if(Dropstate.SelectedItem.Text.Equals("All"))
				{
					qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
				}
				else
				{
					qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
				}
			}
			string[] query={qry1,qry2,qry3,qry4,qry5,qry6,qry7,qry8,qry9,qry10,qry11,qry12};
			string[] query1={qry13,qry14,qry15,qry16,qry17,qry18,qry19,qry20,qry21,qry22,qry23,qry24};
			//if(ds12==ds22 && ds11 <= ds21)
			//if(ds11 <= ds21)
			//{
			int i=0,k=0;
			if(ds12==ds22)
			{
				i=ds11-1;
				k=ds21-1;
			}
			else
			{
				i=ds11-1;
				k=11;
			}
				
			while(i<=k)
			{
				SqlDtr=obj.GetRecordSet(query[i]);
				if(SqlDtr.HasRows)
				{
						
					if(i==0)
						sw.WriteLine("\t\tJanuary"+ds12);
					else if(i==1)
						sw.WriteLine("\t\tFebruary"+ds12);
					else if(i==2)
						sw.WriteLine("\t\tMarch"+ds12);
					else if(i==3)
						sw.WriteLine("\t\tApril"+ds12);
					else if(i==4)
						sw.WriteLine("\t\t"+"May"+ds12);
					else if(i==5)
						sw.WriteLine("\t\tJun"+ds12);
					else if(i==6)
						sw.WriteLine("\t\tJuly"+ds12);
					else if(i==7)
						sw.WriteLine("\t\tAugust"+ds12);
					else if(i==8)
						sw.WriteLine("\t\tSeptember"+ds12);
					else if(i==9)
						sw.WriteLine("\t\tOctober"+ds12);
					else if(i==10)
						sw.WriteLine("\t\tNovember"+ds12);
					else if(i==11)
						sw.WriteLine("\t\tDecember"+ds12);
					sw.WriteLine("Product\tBazzar\tRO\tOE\tFleet\tIBP\tTotal");
					while(SqlDtr.Read())
					{
						sw.WriteLine(SqlDtr["m1"].ToString()+"\t"+SqlDtr["bazzar"].ToString()+"\t"+SqlDtr["ro"].ToString()+"\t"+SqlDtr["oe"].ToString()+"\t"+SqlDtr["fleet"].ToString()+"\t"+SqlDtr["ibp"].ToString()+"\t"+SqlDtr["total"].ToString());
						Totalforprint(						
							SqlDtr["bazzar"].ToString().Trim(),
							SqlDtr["ro"].ToString().Trim(),
							SqlDtr["oe"].ToString().Trim(),
							SqlDtr["fleet"].ToString().Trim(),
							SqlDtr["ibp"].ToString().Trim(),
							SqlDtr["total"].ToString().Trim()
							);
					}
					sw.WriteLine("Total\t"+System.Convert.ToString(baazar)+"\t"+System.Convert.ToString(ro)+"\t"+System.Convert.ToString(oe)+"\t"+System.Convert.ToString(fleet)+"\t"+System.Convert.ToString(ibp)+"\t"+System.Convert.ToString(total));
					sw.WriteLine();
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
				}
				SqlDtr.Close();
				i++;
			}
			
			if((ds22-ds12)==1 && ds21 <ds11)
			{
				int j=ds21-1;
				while(j<=ds11-1)
				{
					SqlDtr=obj.GetRecordSet(query1[j]);
					if(SqlDtr.HasRows)
					{
						if(j==0)
							sw.WriteLine("\t\tJanuary"+ds22);
						else if(j==1)
							sw.WriteLine("\t\tFebruary"+ds22);
						else if(j==2)
							sw.WriteLine("\t\tMarch"+ds22);
						else if(j==3)
							sw.WriteLine("\t\tApril"+ds22);
						else if(j==4)
							sw.WriteLine("\t\t"+"May"+ds22);
						else if(j==5)
							sw.WriteLine("\t\tJun"+ds22);
						else if(j==6)
							sw.WriteLine("\t\tJuly"+ds22);
						else if(j==7)
							sw.WriteLine("\t\tAugust"+ds22);
						else if(j==8)
							sw.WriteLine("\t\tSeptember"+ds22);
						else if(j==9)
							sw.WriteLine("\t\tOctober"+ds22);
						else if(j==10)
							sw.WriteLine("\t\tNovember"+ds22);
						else if(j==11)
							sw.WriteLine("\t\tDecember"+ds22);
						sw.WriteLine("Product\tBazzar\tRO\tOE\tFleet\tIBP\tTotal");
						while(SqlDtr.Read())
						{
							sw.WriteLine(SqlDtr["m1"].ToString()+"\t"+SqlDtr["bazzar"].ToString()+"\t"+SqlDtr["ro"].ToString()+"\t"+SqlDtr["oe"].ToString()+"\t"+SqlDtr["fleet"].ToString()+"\t"+SqlDtr["ibp"].ToString()+"\t"+SqlDtr["total"].ToString());
							Totalforprint(						
								SqlDtr["bazzar"].ToString().Trim(),
								SqlDtr["ro"].ToString().Trim(),
								SqlDtr["oe"].ToString().Trim(),
								SqlDtr["fleet"].ToString().Trim(),
								SqlDtr["ibp"].ToString().Trim(),
								SqlDtr["total"].ToString().Trim()
								);
						}
						sw.WriteLine("Total\t"+System.Convert.ToString(baazar)+"\t"+System.Convert.ToString(ro)+"\t"+System.Convert.ToString(oe)+"\t"+System.Convert.ToString(fleet)+"\t"+System.Convert.ToString(ibp)+"\t"+System.Convert.ToString(total));
						sw.WriteLine();
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						
					}
					SqlDtr.Close();
					j++;
				}
			}
			sw.Close();*/
            //int flag1=0;
            if (View==1)
			{
				int flag=0;
				if(arrProductList.Length!=0)
				{
					flag=1;
					sw.Write("\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(GetMonthName(DateFrom[m].ToString())+"\t\t\t\t\t\t\t");
					}
					sw.WriteLine();
					sw.Write("Product");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\tKSK");
						sw.Write("\tN-KSK");
						sw.Write("\tIBP");
						sw.Write("\tBazzar");
						sw.Write("\tRo");
						sw.Write("\tOE");
						sw.Write("\tFleet");
						sw.Write("\tTotal");
					}
					sw.WriteLine();
				}
				if(flag==1)
				{
					for(int i=0;i<arrProduct.GetLength(0);i++)
					{
						sw.Write(arrProductList[i].ToString());
						for(int j=0;j<8*(DateFrom.Length);j++)
						{
							sw.Write("\t"+arrProduct[i,j].ToString());
						}
						sw.WriteLine();
					}
					sw.Write("Total");
					for(int j=0;j<GrantTotal.Length;j++)
					{
						sw.Write("\t"+GrantTotal[j]);
					}
					sw.WriteLine();
				}
			}
			sw.Close();
		}

		/// <summary>
		/// This method is used to prepares the report file monthwiseprodsale.txt for printting.
		/// </summary>
		public void makingReport()
		{
            /*
			System.Data.SqlClient.SqlDataReader rdr=null;
			string info= "";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\MonthlyProductReport.txt";
			StreamWriter sw = new StreamWriter(path);		
			try
			{
				DataGrid[] dgarr={DataGrid1,Datagrid2,Datagrid3,Datagrid4,Datagrid5,Datagrid6,Datagrid7,Datagrid8,Datagrid9,Datagrid10,Datagrid11,Datagrid12};
				Panel[] pan={Panel1,Panel2,Panel3,Panel4,Panel5,Panel6,Panel7,Panel8,Panel9,Panel10,Panel11,Panel12};
				DataGrid[] dgarr1={Datagrid13,Datagrid14,Datagrid15,Datagrid16,Datagrid17,Datagrid18,Datagrid19,Datagrid20,Datagrid21,Datagrid22,Datagrid23,Datagrid24};
				Panel[] pan1={Panel13,Panel14,Panel15,Panel16,Panel17,Panel18,Panel19,Panel20,Panel21,Panel22,Panel23,Panel24};
				int[] c={0,0,0,0,0,0,0,0,0,0,0,0};
				int[] c1={0,0,0,0,0,0,0,0,0,0,0,0};
				for(int y=0;y<12;y++)
				{
					pan[y].Visible=false;
					pan1[y].Visible=false;
				}
				
				string s1="";
				string s2="";
				s1=txtDateTo1.Text;
				s2=txtDateFrom1.Text;
				string[] ds1 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length):s1.Split(new char[] {'-'},s1.Length);
				string[] ds2 = s2.IndexOf("/")>0?s2.Split(new char[] {'/'},s1.Length):s2.Split(new char[] {'-'},s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					return;
				}
				if((ds22-ds12) > 1)
				{
					//MessageBox.Show("Please Select date between one year");
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					return;
				}
//				if(ds12==ds22 && ds11 <= ds21)
//				{
//					if(Dropstate.SelectedItem.Text.Equals("All"))
//					{
//						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					}
//					else
//					{
//						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					}
//					if(ds11 == ds21)
//					{
//						pan[ds11-1].Visible=true;
//					}
//					else
//					{
//						for(int j=ds11;j<=ds21;j++)
//						{
//							pan[j-1].Visible=true;
//						}
//					}
//				}
//
//				if((ds22-ds12)==1 && ds21 <ds11)
//				{
//					for(int x=ds11;x<=12;x++)
//					{
//						pan[x-1].Visible=true;
//					}
//					if(Dropstate.SelectedItem.Text.Equals("All"))
//					{
//						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					}
//					else
//					{
//						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					}
//					for(int w=1;w<=ds21;w++)
//					{
//						pan1[w-1].Visible=true;
//					}
//					if(Dropstate.SelectedItem.Text.Equals("All"))
//					{
//						qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					}
//					else
//					{
//						qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//						qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by c.state,p.prod_name,p.pack_type";
//					}
//				}
				if(ds12==ds22 && ds11 <= ds21)
				{
					if(Dropstate.SelectedItem.Text.Equals("All"))
					{
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					else
					{
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					if(ds11 == ds21)
					{
						pan[ds11-1].Visible=true;
					}
					else
					{
						for(int j=ds11;j<=ds21;j++)
						{
							pan[j-1].Visible=true;
						}
					}
				}

				if((ds22-ds12)==1 && ds21 <ds11)
				{
					for(int x=ds11;x<=12;x++)
					{
						pan[x-1].Visible=true;
					}
					if(Dropstate.SelectedItem.Text.Equals("All"))
					{
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					else
					{
						qry1="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry2="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry3="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry4="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry5="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry6="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry7="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry8="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry9="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry10="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry11="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry12="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateTo1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateTo1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					for(int w=1;w<=ds21;w++)
					{
						pan1[w-1].Visible=true;
					}
					if(Dropstate.SelectedItem.Text.Equals("All"))
					{
						qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
					else
					{
						qry13="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthj2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthj1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry14="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthfeb2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthfeb1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry15="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthm2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthm1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry16="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(montha2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(montha1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry17="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthmay2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthmay1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry18="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjune2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjune1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry19="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthjuly2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthjuly1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry20="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthau2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthau1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry21="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthsep2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthsep1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry22="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthoct2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthoct1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry23="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthnov2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthnov1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
						qry24="select distinct p.prod_name+':'+p.pack_type m1,sum(m.bazzar) bazzar,sum(m.ro) ro,sum(m.oe) oe,sum(m.fleet) fleet,sum(m.ibp) ibp,sum(m.bazzar+m.oe+m.ro+m.fleet+m.ibp) total from monthwise1 m,customer c,sales_master sm,products p where m.invoice_no=sm.invoice_no and sm.cust_id=c.cust_id and c.state='"+Dropstate.SelectedItem.Text.ToString()+"' and p.prod_name in(select prod_name from products where prod_id=m.prod_id) and p.pack_type in(select pack_type from products where prod_id=m.prod_id) and cast(floor(cast(date_in as float)) as datetime)<= '"+System.Convert.ToDateTime(monthdec2(txtDateFrom1.Text)).ToShortDateString()+"' and  cast(floor(cast(date_in as float)) as datetime)>='"+System.Convert.ToDateTime(monthdec1(txtDateFrom1.Text)).ToShortDateString()+"'  group by p.prod_name,p.pack_type";
					}
				}
				SqlConnection con1;
				con1=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlDataReader dtr;
				con1.Open ();
				SqlCommand cmd=new SqlCommand(qry1,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[0]++;
				dgarr[0].DataSource=dtr;
				dgarr[0].DataBind();
				con1.Close();
				dtr.Close();
				cmd.Dispose();
				con1.Open ();
				cmd=new SqlCommand(qry2,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[1]++;
				dgarr[1].DataSource=dtr;
				dgarr[1].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry3,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[2]++;
				dgarr[2].DataSource=dtr;
				dgarr[2].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry4,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[3]++;
				dgarr[3].DataSource=dtr;
				dgarr[3].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry5,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[4]++;
				dgarr[4].DataSource=dtr;
				dgarr[4].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry6,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[5]++;
				dgarr[5].DataSource=dtr;
				dgarr[5].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry7,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[6]++;
				dgarr[6].DataSource=dtr;
				dgarr[6].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry8,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[7]++;
				dgarr[7].DataSource=dtr;
				dgarr[7].DataBind();
				con1.Close();
				dtr.Close();
				cmd.Dispose();
				con1.Open ();
				cmd=new SqlCommand(qry9,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[8]++;
				dgarr[8].DataSource=dtr;
				dgarr[8].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry10,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[9]++;
				dgarr[9].DataSource=dtr;
				dgarr[9].DataBind();
				con1.Close();
				dtr.Close();
				cmd.Dispose();
				con1.Open ();
				cmd=new SqlCommand(qry11,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[10]++;
				dgarr[10].DataSource=dtr;
				dgarr[10].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();
				con1.Open ();
				cmd=new SqlCommand(qry12,con1);
				dtr=cmd.ExecuteReader();
				if(dtr.HasRows)
					c[11]++;
				dgarr[11].DataSource=dtr;
				dgarr[11].DataBind();
				con1.Close();
				cmd.Dispose();
				dtr.Close();

				if((ds22-ds12)==1 && ds21 <ds11)
				{
					SqlDataReader dtr1;
					con1.Open ();
					cmd=new SqlCommand(qry13,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[0]++;
					dgarr1[0].DataSource=dtr1;
					dgarr1[0].DataBind();
					con1.Close();
					dtr1.Close();
					cmd.Dispose();
					con1.Open ();
					cmd=new SqlCommand(qry14,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[1]++;
					dgarr1[1].DataSource=dtr1;
					dgarr1[1].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry15,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[2]++;
					dgarr1[2].DataSource=dtr1;
					dgarr1[2].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry16,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[3]++;
					dgarr1[3].DataSource=dtr1;
					dgarr1[3].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry17,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[4]++;
					dgarr1[4].DataSource=dtr1;
					dgarr1[4].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry18,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[5]++;
					dgarr1[5].DataSource=dtr1;
					dgarr1[5].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry19,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[6]++;
					dgarr1[6].DataSource=dtr1;
					dgarr1[6].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry20,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[7]++;
					dgarr1[7].DataSource=dtr1;
					dgarr1[7].DataBind();
					con1.Close();
					dtr1.Close();
					cmd.Dispose();
					con1.Open ();
					cmd=new SqlCommand(qry21,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[8]++;
					dgarr1[8].DataSource=dtr1;
					dgarr1[8].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry22,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[9]++;
					dgarr1[9].DataSource=dtr1;
					dgarr1[9].DataBind();
					con1.Close();
					dtr1.Close();
					cmd.Dispose();
					con1.Open ();
					cmd=new SqlCommand(qry23,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[10]++;
					dgarr1[10].DataSource=dtr1;
					dgarr1[10].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();
					con1.Open ();
					cmd=new SqlCommand(qry24,con1);
					dtr1=cmd.ExecuteReader();
					if(dtr1.HasRows)
						c1[11]++;
					dgarr1[11].DataSource=dtr1;
					dgarr1[11].DataBind();
					con1.Close();
					cmd.Dispose();
					dtr1.Close();


				}
				
				for(int l=0;l<12;l++)
				{
					if(c[l]==0)
						pan[l].Visible=false;
					if(c1[l]==0)
						pan1[l].Visible=false;
				}
				int yy=0;
				for(int xx=0;xx<12;xx++)
				{
					if((pan[xx].Visible.Equals(false)) && (pan1[xx].Visible.Equals(false)))
						yy++;
				}
				if(yy==12)
				{
					MessageBox.Show("Data Not Available");
					return;
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
				  string des="---------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine("=======================================================================================");
				sw.WriteLine("Month Wise / Products Wise / Channel Wise Sales Report  From "+txtDateTo1.Text.ToString()+" To "+txtDateFrom1.Text.ToString());
				sw.WriteLine("=======================================================================================");
				sw.WriteLine("  District :- "+Dropstate.SelectedItem.Text.ToString());
				// info : to set string format.
				info = " {0,-30:S} {1,10:S} {2,10:S} {3,10:S} {4,10:S} {5,10:S} {6,10:S}";
				if(DataGrid1.Visible==true)
				{
					dbobj.SelectQuery(qry1,ref rdr);
					sw.WriteLine("                                  January "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|         Product              |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				
				}
				if(Datagrid2.Visible==true)
				{
					dbobj.SelectQuery(qry2,ref rdr);
					sw.WriteLine("                                  February "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid3.Visible==true)
				{
					dbobj.SelectQuery(qry3,ref rdr);
					sw.WriteLine("                                  March "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid4.Visible==true)
				{
					dbobj.SelectQuery(qry4,ref rdr);
					sw.WriteLine("                                  April "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid5.Visible==true)
				{
					dbobj.SelectQuery(qry5,ref rdr);
					sw.WriteLine("                                  May "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid6.Visible==true)
				{
					dbobj.SelectQuery(qry6,ref rdr);
					sw.WriteLine("                                  June "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid7.Visible==true)
				{
					dbobj.SelectQuery(qry7,ref rdr);
					sw.WriteLine("                                  July "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid8.Visible==true)
				{
					dbobj.SelectQuery(qry8,ref rdr);
					sw.WriteLine("                                  August "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid9.Visible==true)
				{
					dbobj.SelectQuery(qry9,ref rdr);
					sw.WriteLine("                                  September "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),	
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid10.Visible==true)
				{
					dbobj.SelectQuery(qry10,ref rdr);
					sw.WriteLine("                                  October "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid11.Visible==true)
				{
					dbobj.SelectQuery(qry11,ref rdr);
					sw.WriteLine("                                  November "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),	
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if(Datagrid12.Visible==true)
				{
					dbobj.SelectQuery(qry12,ref rdr);
					sw.WriteLine("                                  December "+ds12.ToString());
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine("|           Product                 |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
					if(rdr.HasRows)
					{
					
						while(rdr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
							Totalforprint(						
								rdr["bazzar"].ToString().Trim(),
								rdr["ro"].ToString().Trim(),
								rdr["oe"].ToString().Trim(),
								rdr["fleet"].ToString().Trim(),
								rdr["ibp"].ToString().Trim(),
								rdr["total"].ToString().Trim());
						}
					}
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
					sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
					baazar=0;
					ro=0;
					oe=0;
					fleet=0;
					ibp=0;
					total=0;
					dbobj.Dispose();
				}
				if((ds22-ds12)==1 && ds21 <ds11)
				{
					if(Datagrid13.Visible==true)
					{
						dbobj.SelectQuery(qry13,ref rdr);
						sw.WriteLine("                                  January "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),	
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid14.Visible==true)
					{
						dbobj.SelectQuery(qry14,ref rdr);
						sw.WriteLine("                                  February "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid15.Visible==true)
					{
						dbobj.SelectQuery(qry15,ref rdr);
						sw.WriteLine("                                  March "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid16.Visible==true)
					{
						dbobj.SelectQuery(qry16,ref rdr);
						sw.WriteLine("                                  April "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid17.Visible==true)
					{
						dbobj.SelectQuery(qry17,ref rdr);
						sw.WriteLine("                                  May "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid18.Visible==true)
					{
						dbobj.SelectQuery(qry18,ref rdr);
						sw.WriteLine("                                  June "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid19.Visible==true)
					{
						dbobj.SelectQuery(qry19,ref rdr);
						sw.WriteLine("                                  July "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid20.Visible==true)
					{
						dbobj.SelectQuery(qry20,ref rdr);
						sw.WriteLine("                                  August "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|           Product            |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid21.Visible==true)
					{
						dbobj.SelectQuery(qry21,ref rdr);
						sw.WriteLine("                                  September "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid22.Visible==true)
					{
						dbobj.SelectQuery(qry22,ref rdr);
						sw.WriteLine("                                  October "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid23.Visible==true)
					{
						dbobj.SelectQuery(qry23,ref rdr);
						sw.WriteLine("                                  November "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|            Product           |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
					if(Datagrid24.Visible==true)
					{
						dbobj.SelectQuery(qry24,ref rdr);
						sw.WriteLine("                                  December "+ds22.ToString());
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine("|             Product          |  Bazzar  |    RO    |    OE    |   Fleet  |   IBP    |   Total  |");
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						//             1234567................25 123456..10 123456..10 123456..10  123456..10 123456..10 123456..10
				
						if(rdr.HasRows)
						{
					
							while(rdr.Read())
							{
								sw.WriteLine(info,GenUtil.TrimLength(rdr["m1"].ToString().Trim(),30),
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
								Totalforprint(						
									rdr["bazzar"].ToString().Trim(),
									rdr["ro"].ToString().Trim(),
									rdr["oe"].ToString().Trim(),
									rdr["fleet"].ToString().Trim(),
									rdr["ibp"].ToString().Trim(),
									rdr["total"].ToString().Trim());
							}
						}
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						sw.WriteLine(info,"",System.Convert.ToString(baazar),System.Convert.ToString(ro),System.Convert.ToString(oe),System.Convert.ToString(fleet),System.Convert.ToString(ibp),System.Convert.ToString(total));
						sw.WriteLine("+------------------------------+----------+----------+----------+----------+----------+----------+");
						baazar=0;
						ro=0;
						oe=0;
						fleet=0;
						ibp=0;
						total=0;
						dbobj.Dispose();
					}
				}
				sw.Close();
				CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Method:btnprint, userid  "+UID );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Method:btnprint,   EXCEPTION "+ex.Message+"  userid  "+UID );
			}*/
        }

        /// <summary>
        /// contacts the print server and sends the Monthwiseprodsale.txt file name to print.
        /// </summary>
        private void BtnPrint1_Click(object sender, System.EventArgs e)
		{
			//*************************
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
					CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonthlyproductsalesReport  Printed"+"  userid  " +UID);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MonthlyProductReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonwiseprodsalReport  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +UID);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonwiseprodsalReport  Printed"+"  EXCEPTION "+se.Message+"  userid  " +UID);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonwiseprodsalReport  Printed"+"  EXCEPTION "+es.Message+"  userid  " +UID);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:Monwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonwiseprodsalReport  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +UID);
			}
		}

		/// <summary>
		/// This method is used to prepares the excel report file MonthWiseProdSales.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//				Panel[] pan={Panel1,Panel2,Panel3,Panel4,Panel5,Panel6,Panel7,Panel8,Panel9,Panel10,Panel11,Panel12,Panel13,Panel14,Panel15,Panel16,Panel17,Panel18,Panel19,Panel20,Panel21,Panel22,Panel23,Panel24};
				//				int Flag=0;
				//				for(int ii=0;ii<24;ii++)
				//				{
				//					if(pan[ii].Visible==true)
				//						Flag=1;
				//				}
				//if(Flag==1)
				if(View==1)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert 'Mont.Prod.Secon.Sales' File Into Excel Fromat");
					CreateLogFiles.ErrorLog("Form:monthwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Mont.Prod.Secon.Sales Report Convert Into Excel Format, userid  "+UID);
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
				CreateLogFiles.ErrorLog("Form:monthwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Mont.Prod.Secon.Sales Report Viewed, EXCEPTION  "+ ex.Message+ " userid  "+UID);
			}
		}

		/// <summary>
		/// This method return the month name with year.
		/// </summary>
		public string GetMonthName(string mon)
		{
			if(mon.IndexOf("/")>0 || mon.IndexOf("-")>0)
			{
				string[] month=mon.IndexOf("/")>0?mon.Split(new char[] {'/'},mon.Length): mon.Split(new char[] { '-' }, mon.Length);
				if(month[0].ToString()=="1")
					return "January "+month[2].ToString();
				else if(month[0].ToString()=="2")
					return "Fabruary "+month[2].ToString();
				else if(month[0].ToString()=="3")
					return "March "+month[2].ToString();
				else if(month[0].ToString()=="4")
					return "April "+month[2].ToString();
				else if(month[0].ToString()=="5")
					return "May "+month[2].ToString();
				else if(month[0].ToString()=="6")
					return "June "+month[2].ToString();
				else if(month[0].ToString()=="7")
					return "July "+month[2].ToString();
				else if(month[0].ToString()=="8")
					return "August "+month[2].ToString();
				else if(month[0].ToString()=="9")
					return "September "+month[2].ToString();
				else if(month[0].ToString()=="10")
					return "October "+month[2].ToString();
				else if(month[0].ToString()=="11")
					return "November "+month[2].ToString();
				else if(month[0].ToString()=="12")
					return "December "+month[2].ToString();
			}
			return "";
		}

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//			try
			//			{
			//				InventoryClass obj = new InventoryClass();
			//				SqlDataReader rdr = null;
			//				string sql="";
			//				DropValue.Items.Clear();
			//				DropValue.Items.Add("All");
			//				if(DropSearchBy.SelectedIndex!=0)
			//				{
			//					if(DropSearchBy.SelectedIndex==1)
			//						sql="select distinct category from products order by category";
			//					else if(DropSearchBy.SelectedIndex==2)
			//						sql="select distinct state from customer where state<>'' order by state";
			//					else if(DropSearchBy.SelectedIndex==3)
			//						sql="select Prod_Name+':'+pack_Type from products order by Prod_Name";
			//					else if(DropSearchBy.SelectedIndex==4)
			//						sql="select distinct Under_Salesman from sales_master order by under_salesman";
			//					rdr = obj.GetRecordSet(sql);
			//					if(rdr.HasRows)
			//					{
			//						while(rdr.Read())
			//						{
			//							DropValue.Items.Add(rdr.GetValue(0).ToString());
			//						}
			//					}
			//					else
			//					{
			//						MessageBox.Show("Data Not Available");
			//						return;
			//					}
			//				}
			//			}
			//			catch(Exception ex)
			//			{
			//				CreateLogFiles.ErrorLog("Form:Salsereport1,Method: DropSearchBy_Click,Class:PetrolPumpClass "+"  Sales Report "+"  EXCEPTION   "+ex.Message+"  userid  "+UID);
			//			}
		}

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value from dropdownlist with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strCategory="",strNameWithPack="",strDistrict="",strSSR="";
				strCategory="select distinct category from products order by category";
				strDistrict="select distinct state from customer where state<>'' order by state";
				strNameWithPack="select Prod_Name+':'+pack_Type from products order by Prod_Name";
				//strSSR="select distinct Under_Salesman from sales_master order by under_salesman";				
				strSSR="select Emp_Name from Employee where Designation='Servo Sales Representative' and status=1 order by Emp_Name";
				string[] arrStr = {strCategory,strDistrict,strNameWithPack,strSSR};
				HtmlInputHidden[] arrCust = {tempCategory,tempDistrict,tempNameWithPack,tempSSR};	
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
				CreateLogFiles.ErrorLog("Form:Monthwiseprodsal.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Month wise Product Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+UID);
			}
		}
	}
}