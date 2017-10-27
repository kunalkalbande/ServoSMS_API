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
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using DBOperations;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using RMG;
using System.Web.SessionState;
using System.Runtime.InteropServices;
using System.Management;
using System.Net;
using System.Net.Sockets;
using MySecurity;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SadbhavnaSchemeYearWise_aspx.
	/// </summary>
	public partial class SadbhavnaSchemeYearWise : System.Web.UI.Page
	{
		//protected System.Web.UI.WebControls.TextBox txtDateFrom;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateFrom;
		//protected System.Web.UI.WebControls.TextBox Textbox1;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateTo;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public string sql;
		public string str1;
		public string str2;
		public string str3;
		public double check1;
		public double check2;
		public double check3;
		public double check4;
		public double check5;
		public double check6;
		public double check7;
		public double check8;
		public double check9;
		public double check10;
		public double check11;
		public double check12;
		public double check;
		string uid="";
		public SqlDataReader dtr;
		
		public ArrayList Min=new ArrayList();
		
		public double Tot_CBPoints;
		public double Tot_CBPoints1;
		public double Tot_CBPoints2;
		public double Tot_CBPoints3;
		public double Tot_CBPoints4;
		public double Tot_CBPoints5;
		public double Tot_CBPoints6;
		public double Tot_CBPoints7;
		public double Tot_CBPoints8;
		public double Tot_CBPoints9;
		public double Tot_CBPoints10;
		public double Tot_CBPoints11;
		public double Tot_CBPoints12;

		public double CBPoints1;
		public double CBPoints2;
		public double CBPoints3;
		public double CBPoints4;
		public double CBPoints5;
		public double CBPoints6;
		public double CBPoints7;
		public double CBPoints8;
		public double CBPoints9;
		public double CBPoints10;
		public double CBPoints11;
		public double CBPoints12;

		public double Bonus_Points1;
		public double Bonus_Points2;
		public double Bonus_Points3;
		public double Bonus_Points4;
		public double Bonus_Points5;
		public double Bonus_Points6;
		public double Bonus_Points7;
		public double Bonus_Points8;
		public double Bonus_Points9;
		public double Bonus_Points10;
		public double Bonus_Points11;
		public double Bonus_Points12;
		
		public double Xtra_Points1;
		public double Xtra_Points2;
		public double Xtra_Points3;
		public double Xtra_Points4;
		public double Xtra_Points5;
		public double Xtra_Points6;
		public double Xtra_Points7;
		public double Xtra_Points8;
		public double Xtra_Points9;
		public double Xtra_Points10;
		public double Xtra_Points11;
		// protected System.Web.UI.WebControls.DropDownList DropCtype;
		public double Xtra_Points12;
		

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
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
				
			}

			try
			{
				if(!Page.IsPostBack)
				{   
					
					/*      Coment by vikas 23.05.09
					//*******************************Add Vikas Sharma 06.04.09****************************************
					PetrolPumpClass obj1=new PetrolPumpClass();
					//string str="Select Distinct Cust_Type from Customer Order by Cust_Type";
					string str="select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
					//select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer
					SqlDataReader sdtr1=null;
					DropCtype.Items.Clear();
					DropCtype.Items.Add("Select");
					sdtr1=obj1.GetRecordSet(str);
					while(sdtr1.Read())
					{
						DropCtype.Items.Add(sdtr1.GetValue(0).ToString().Trim());
					}
					sdtr1.Close();
					//*******************************************End****************************************/
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
			}

			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();

			string sql="Select Dealername,dealership,address,foodlicno,wm from organisation where CompanyID = 1001";
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.Read())
			{
				str1 = SqlDtr.GetValue(0).ToString()+","+SqlDtr.GetValue(1).ToString()+SqlDtr.GetValue(2).ToString();  
				str2 = SqlDtr.GetValue(3).ToString();
				str3 = SqlDtr.GetValue(4).ToString();
			}
			else
			{
				str1 = "";
				str2 = "";
				str3 ="";
			}
			SqlDtr.Close();
			
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="41";
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
				GridSalesReport.Visible=false;
				dropyear.SelectedIndex=dropyear.Items.IndexOf(dropyear.Items.FindByValue(DateTime.Now.Year.ToString()));
				//**txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				//**Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				GetMultiValue();
			}
		}

		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strState="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012

				strDistrict = "select distinct state from customer";
				strState = "select distinct country from customer";
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
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to return the date in MM/dd/yyyy format.
		/// </summary>
		
		# region DateTime Function...
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length) : str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}
		# endregion 
		
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

		public string totap="0";
		protected string april(string customerid,string mon )
		{
			int month=System.Convert.ToInt32(mon);
			//vikas 24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());

			totap="0";
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(year,month) +"/"+year+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				if(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totap=	SqlDtr.GetValue(0).ToString();
				}
			}
			else
				totap= "0" ;
			if(mon.Equals("4"))
			{
				check4+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("5"))
			{
				check5+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("6"))
			{
				check6+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("7"))
			{
				check7+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("8"))
			{
				check8+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("9"))
			{
				check9+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("10"))
			{
				check10+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("11"))
			{
				check11+=System.Convert.ToDouble(totap);
			}
			if(mon.Equals("12"))
			{
				check12+=System.Convert.ToDouble(totap);
			}
			SqlDtr.Close();
			return totap;
		}

		public string totjn="0";
		protected string jan(string customerid,string mon )
		{
			int month=System.Convert.ToInt32(mon);
			//24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			year=year+1;
			totjn="0";
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex+1,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(year,month) +"/"+year+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				if(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totjn=	SqlDtr.GetValue(0).ToString();
				}
			}
			else
				totjn= "0" ;
			if(mon.Equals("1"))
			{
				check1+=System.Convert.ToDouble(totjn);
			}
			if(mon.Equals("2"))
			{
				check2+=System.Convert.ToDouble(totjn);
			}
			if(mon.Equals("3"))
			{
				check3+=System.Convert.ToDouble(totjn);
			}
			SqlDtr.Close();
			return totjn;
		}
		//*************points************************************
		public double total;
		public double total2;
		protected double janpt(string customerid,string mon )
		{
			int month=System.Convert.ToInt32(mon);
			//vikas 24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			year=year+1;
			total=0;
			
			double totpt=0;
			double total1=0;
			total2=0;
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex+1,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			//Coment by vikas 20.08.09 string sql="select sm.Net_Amount  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(year,month) +"/"+year+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(year,month) +"/"+year+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				while(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totpt =	System.Convert.ToDouble(SqlDtr.GetValue(0).ToString());
					
					/*Coment by vikas 19.08.09 
					total=0;
					total1=0;
					bonus=Math.Round(totpt/100,2);
					if(totpt >=100000)
					{
						total1=Math.Round(bonus*.2,2);
					}
					else if(totpt >=50000)
					{
						total1=Math.Round(bonus*.1,2);
					}
					else if(totpt >=10000)
					{
						total1=Math.Round(bonus*.05,2);
					}
					else 
					{
						total1=0;
					}
					total=Math.Round(bonus+total1);
					total2+=total;*/
					
					total=0;
					total=Math.Round(totpt);
					total2+=total;
				}
			}
			else
				total2= 0 ;
			if(mon.Equals("1"))
			{
				check1+=System.Convert.ToDouble(total2);
			}
			else if(mon.Equals("2"))
			{
				check2+=System.Convert.ToDouble(total2);
			}
			else if(mon.Equals("3"))
			{
				check3+=System.Convert.ToDouble(total2);
			}
			SqlDtr.Close();
			return total2;
		}

		//******************
		//public double total;
		public double total3;
		protected double aprpt(string customerid,string mon )
		{
			int month=System.Convert.ToInt32(mon);
			//coment by vikas 24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			
			double total=0;
			
			double totpt=0;
			double total1=0;
			total3=0;
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex+1,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			//Coment by vikas 20.08.09 string sql="select sm.Net_Amount  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(year,month) +"/"+year+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(year,month) +"/"+year+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				while(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totpt =	System.Convert.ToDouble(SqlDtr.GetValue(0).ToString());
					//*************
				
					total=0;
					total=Math.Round(totpt);
					total3+=total;
				}
			}
			else
				total3= 0 ;
			if(mon.Equals("4"))
			{
				check4+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("5"))
			{
				check5+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("6"))
			{
				check6+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("7"))
			{
				check7+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("8"))
			{
				check8+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("9"))
			{
				check9+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("10"))
			{
				check10+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("11"))
			{
				check11+=System.Convert.ToDouble(total3);
			}
			else if(mon.Equals("12"))
			{
				check12+=System.Convert.ToDouble(total3);
			}
			SqlDtr.Close();
			return total3;
		}

		public double totliter=0;
		protected string liter(string customerid )
		{
			//int month=System.Convert.ToInt32(mon);
			//24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			int nextyear=year+1;
			totjn="0";
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex+1,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/"+DateTime.DaysInMonth(nextyear,3) +"/"+nextyear+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				if(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totjn=	SqlDtr.GetValue(0).ToString();
				}
			}
			else
				totjn="0";
			totliter+=Math.Round(System.Convert.ToDouble(totjn.ToString()));
			SqlDtr.Close();
			//Coment by vikas 20.08.09 return System.Convert.ToString(Math.Round(double.Parse(totjn.ToString()),2));
			return System.Convert.ToString(Math.Round(double.Parse(totjn.ToString())));
		}
		
		public double totalpoints;
		public double point;
		public double point2;
		protected double points(string customerid )
		{
			//24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			int nextyear=year+1;
			point2=0;
			point=0;
			
			double totpt=0;
			double total1=0;
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex+1,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			//Coment by vikas 20.08.09 string sql="select sm.Net_Amount  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/"+DateTime.DaysInMonth(nextyear,3) +"/"+nextyear+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/"+DateTime.DaysInMonth(nextyear,3) +"/"+nextyear+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				while(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totpt =	System.Convert.ToDouble(SqlDtr.GetValue(0).ToString());
					
					/*Coment by vikas 19.08.09
					point=0;
					bonus=Math.Round(totpt/100,2);
					if(totpt >=100000)
					{
						total1=Math.Round(bonus*.2,2);
					}
					else if(totpt >=50000)
					{
						total1=Math.Round(bonus*.1,2);
					}
					else if(totpt >=10000)
					{
						total1=Math.Round(bonus*.05,2);
					}
					else 
					{
						total1=0;
					}
					point=Math.Round(bonus+total1);
					point2+=point;
					*/
					point=Math.Round(totpt);
					point2+=point;
				}
			}
			else
				point2= 0 ;
			totalpoints+=point2;
			totalpoints=Math.Round(totalpoints,2);
			SqlDtr.Close();
			return point2;
		}

		public double totalpoints_new;
		public double point_new;
		public double point2_new;
		protected double points_new(string customerid )
		{
			//24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			int nextyear=year+1;
			point2_new=0;
			point_new=0;
			
			double totpt=0;
			double total1=0;
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//Coment by vikas 20.08.09 string sql="select sm.Net_Amount  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/"+DateTime.DaysInMonth(nextyear,3) +"/"+nextyear+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/"+DateTime.DaysInMonth(nextyear,3) +"/"+nextyear+"'and sm.Cust_ID='"+customerid+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				while(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totpt =	System.Convert.ToDouble(SqlDtr.GetValue(0).ToString());
					
					point_new=Math.Round(totpt);
					point2_new+=point_new;
				}
			}
			else
				point2_new= 0 ;
			totalpoints_new+=point2_new;
			totalpoints_new=Math.Round(totalpoints_new,2);
			SqlDtr.Close();
			return point2_new;
		}


		//******************
		// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem )  )
			{
				check+=Double.Parse(e.Item.Cells[14].Text);
					
				   
			
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
               
				e.Item.Cells[14].Text =check.ToString();   
				//				 
				//				e.Item.Cells[11].Text = vat_total.ToString("N",nfi);
				//				Cache["vt"]=vat_total.ToString("N",nfi);
				//				
				//				e.Item.Cells[12].Text = net_total.ToString("N",nfi);
				//				Cache["nt"]=net_total.ToString("N",nfi);
				//				e.Item.Cells[8].Text =ebird.ToString("N",nfi);
				//				Cache["eb"]=ebird.ToString("N",nfi);
				//				e.Item.Cells[7].Text =trade.ToString("N",nfi);
				//				Cache["tr"]=trade.ToString("N",nfi);
				
			}
		
		}

		/// <summary>
		/// This method is used to bind the datagrid with the help of sql query.
		/// </summary>
		public void Bindthedata()
		{
			string sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			//int month=System.Convert.ToInt32(mon);
			//coment by vikas 34.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			int toyear=year+1;
			//string sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3, dbo.Sales_Master.Invoice_No s5,dbo.Sales_Master.Invoice_Date s6, dbo.Sales_Master.Under_SalesMan, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master INNER JOIN  dbo.Customer ON dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID where (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			// string sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ro%')"; //30.03.09 Vikas Sharma 
			// string sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')"; //30.03.09 Vikas Sharma
			
			//coment by vikas 23.05.09 sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%')";

			/****************Start Add by vikas 26.11.2012**************************************/
			if(DropSearchBy.SelectedIndex==0)
			{
				//coment by vikas for remove 0 balance row date on 26.05.09  sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')"; 
				//15.07.09 coment by vikas sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
				//Coment by vikas 20.08.09 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
				//08.09.09 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				//coment by vikas 24.11.2012 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==1)
			{
				//coment by vikas for remove 0 balance row date on 26.05.09 sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%')";
				//Coment by vikas 20.08.09 sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
				//coment by vikas 24.11.2012 sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==2)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"')  and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				
			}
			else if (DropSearchBy.SelectedIndex==3)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==4)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==5)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			/****************End**************************************/
			
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"Customer");
			DataTable dtcustomer=ds.Tables["Customer"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridSalesReport.DataSource=dv;
			if(dv.Count!=0)
			{
				GridSalesReport.DataBind();
				GridSalesReport.Visible=true;
			}
			else
			{
				GridSalesReport.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}

		/// <summary>
		/// this is used to make sorting the datagrid on clicking of the datagridheader.
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
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  ");
			}
		}

		/// <summary>
		/// This method is used to view the report and set the column name with ascending order in 
		/// session variable.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public int flage=0;
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			flage=1;
			try
			{
				if(dropType.SelectedIndex==0)
				{
					//				if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(Textbox1.Text))>0)
					//				{
					//					MessageBox.Show("Date From Should be less than Date To");
					//					GridSalesReport.Visible=false;
					//				}
					//				else
					//				{
					strorderby="s3 ASC";
					Session["Column"]="s3";
					Session["order"]="ASC";
					Bindthedata();
					//					#endregion
					//}
				}
				else
				{
					Bindddata_New();
					
				}
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:btnShow_Click  SadbhavnaSchemeYearWise   Viewed "+"  userid  ");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:btnShow_Click  SadbhavnaSchemeYearWise   Viewed "+"  EXCEPTION  "+ ex.Message+"  userid  ");
			}
		}

		public void Bindddata_New()
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				string sql="";
				//SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//coment by vikas 24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
				int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
				int toyear=year+1;

				/****************Start Add by vikas 26.11.2012**************************************/
				if(DropSearchBy.SelectedIndex==0)
				{
					if(chkboxNew.Checked==false)
					{
						sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
					else
					{
						sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
				}
				else if (DropSearchBy.SelectedIndex==1)
				{
					if(chkboxNew.Checked==false)
					{
						sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
					else
					{
						sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
				}
				else if (DropSearchBy.SelectedIndex==2)
				{
					if(chkboxNew.Checked==false)
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
					else
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"')  and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
				}
				else if (DropSearchBy.SelectedIndex==3)
				{
					if(chkboxNew.Checked==false)
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
					else
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
				}
				else if (DropSearchBy.SelectedIndex==4)
				{
					if(chkboxNew.Checked==false)
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
					else
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
				}
				else if (DropSearchBy.SelectedIndex==5)
				{
					if(chkboxNew.Checked==false)
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
					else
					{
						sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
					}
				}
			
				//if(chkboxNew.Checked==false)
				//{
				//  sql+=" and sm.totalqtyltr>=500";
				//}

				//sql+=" group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				/****************End**************************************/
				dtr=obj.GetRecordSet(sql);
				
			
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		/// <summary>
		/// This method is used to prepare the report file.
		/// </summary>
		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				//vikas 24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
				int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
				int toyear=year+1;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SadbhavnaSchemeYearWise.txt";
				StreamWriter sw = new StreamWriter(path);
				//sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ro%')"; //30.03.09 Vikas sharma 
				//sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')"; //30.03.09 Vikas Sharma
				
				//coment by vikas 23.05.09 sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%')";
				
				//sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, dbo.Sales_Master.Invoice_No s5,dbo.Sales_Master.Invoice_Date s6, dbo.Sales_Master.Under_SalesMan, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master INNER JOIN  dbo.Customer ON dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID where (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";		
				//sql="select vndr_invoice_no invoice_no, vndr_invoice_date invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				//sql=sql+" order by "+Cache["strorderby2"];
				
				/****************Start Add by vikas 26.11.2012**************************************/
				if(DropSearchBy.SelectedIndex==0)
				{
					//coment by vikas for remove 0 balance row date on 26.05.09  sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')"; 
					//15.07.09 coment by vikas sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
					//Coment by vikas 20.08.09 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
					//08.09.09 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
					//coment by vikas 24.11.2012 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==1)
				{
					//coment by vikas for remove 0 balance row date on 26.05.09 sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%')";
					//Coment by vikas 20.08.09 sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
					//coment by vikas 24.11.2012 sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==2)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"')  and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				
				}
				else if (DropSearchBy.SelectedIndex==3)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==4)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
				else if (DropSearchBy.SelectedIndex==5)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				}
			
				/****************End**************************************/

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
				string des="------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Sadbhavna Scheme YearWise From "+dropyear.SelectedItem.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				
				//Coment by vikas 19.08.09 sw.WriteLine("+--------------------+---------------+-------------+--------+--------+------+------+------+------+------+------+------+------+------+------+------+------+------+------+-------+-------+-------+");
				//Coment by vikas 19.08.09 sw.WriteLine("|    Customer Name   |     Place     |Customer Type|Unique  |StartUp | APR  | MAY  |JUNE  |JULY  | AUGT |SEPT  |OCT   |NOV   |DEC   |JAN   |FEB   |MAR   |POINT | COMM |Pt.XTRA|Pt.XTRA|  Pt.  |");
				//Coment by vikas 19.08.09 sw.WriteLine("|                    |               |             | Code   |Points  |      |      |      |      |      |      |      |      |      |      |      |      |COMM  | SALE |  ACH  |  REG  | TOTAL |");
				//Coment by vikas 19.08.09 sw.WriteLine("+--------------------+---------------+-------------+--------+--------+------+------+------+------+------+------+------+------+------+------+------+------+------+------+-------+-------+-------+");
				//Coment by vikas 19.08.09 // 12345678901234567890 123456789012345 1234567890123 12345678 12345678 123456 123456 123456 123456 123456 123456 123456 123456 123456 123456 123456 123456 123456 123456 1234567 1234567 1234567
			
				sw.WriteLine("+--------------------+---------+--------+---------+-----+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+-------+-----+----+---------+");
				sw.WriteLine("|    Customer Name   |  Place  |Customer| Unique  |St.Up|  APR  |  MAY  | JUNE  | JULY  | AUGT  | SEPT  | OCT   | NOV   | DEC   | JAN   | FEB   | MAR   | POINT  |  COMM | P.X.|P.X.|   Pt.   |");
				sw.WriteLine("|                    |         | Type   |  Code   |Point|       |       |       |       |       |       |       |       |       |       |       |       | COMM   |  SALE | ACH |REG |  TOTAL  |");
				sw.WriteLine("+--------------------+---------+--------+---------+-----+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+-------+--------+-------+-----+----+---------+");
				// 12345678901234567890 123456789 12345678 123456789 12345678 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 1234567 12345678 1234567 12345 1234 123456789


				if(rdr.HasRows)
				{
					// info : to set the string format.
					//Coment by vikas 19.08.09 info = " {0,-20:S} {1,-15:S} {2,-13:S} {3,-8:S} {4,-8:S} {5,-6:S} {6,-6:S} {7,-6:S} {8,-6:S} {9,-6:S} {10,-6:S} {11,-6:S} {12,-6:S} {13,-6:S} {14,-6:S} {15,-6:S} {16,-6:S} {17,-6:S} {18,-6:S} {19,-7:S} {20,-7:S} {21,-7:S}";
					info = " {0,-20:S} {1,-9:S} {2,-8:S} {3,-8:S} {4,-5:S} {5,-7:S} {6,-7:S} {7,-7:S} {8,-7:S} {9,-7:S} {10,-7:S} {11,-7:S} {12,-7:S} {13,-7:S} {14,-7:S} {15,-7:S} {16,-7:S} {17,-8:S} {18,-7:S} {19,-5:S} {20,-4:S} {21,-9:S}";
					while(rdr.Read())
					{					
						sw.WriteLine(info,StringUtil.trimlength(rdr["s1"].ToString().Trim(),20),
							StringUtil.trimlength(rdr["s2"].ToString().Trim(),9),
							StringUtil.trimlength(rdr["s3"].ToString().Trim(),8),
							rdr["s4"].ToString().Trim(),
							"",
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"4" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"5" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"6" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"7" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"8" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"9" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"10" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"11" ).ToString(),7),
							StringUtil.trimlength(aprpt(rdr["s5"].ToString().Trim(),"12" ).ToString(),7),
							StringUtil.trimlength(janpt(rdr["s5"].ToString().Trim(),"1" ).ToString(),7),
							StringUtil.trimlength(janpt(rdr["s5"].ToString().Trim(),"2" ).ToString(),7),
							StringUtil.trimlength(janpt(rdr["s5"].ToString().Trim(),"3" ).ToString(),7),
							StringUtil.trimlength(points(rdr["s5"].ToString().Trim()).ToString(),8),
							StringUtil.trimlength(liter(rdr["s5"].ToString().Trim() ),7),
							"",
							"",
							//Coment by vikas 20.08.09 points(rdr["s5"].ToString().Trim()).ToString() );
							points_new(rdr["s5"].ToString().Trim()).ToString() );
					}
				}
				sw.WriteLine("+--------------------+---------------+-------------+--------+--------+------+------+------+------+------+------+------+------+------+------+------+------+------+------+-------+-------+-------+");
				//sw.WriteLine(info,"Total:","","","","",check4.ToString(),check5.ToString(),check6.ToString(),check7.ToString(),check8.ToString(),check9.ToString(),check10.ToString(),check11.ToString(),check12.ToString(),check1.ToString(),check2.ToString(),check3.ToString(),totalpoints.ToString(),Math.Round(totliter,1),"","",totalpoints.ToString());  //Vikas sharma 31.03.09
				//Coment by vikas 19.08.09 string info1= " {0,-68:S} {1,-6:S} {2,-6:S} {3,-6:S} {4,-6:S} {5,-6:S} {6,-6:S} {7,-6:S} {8,-6:S} {9,-6:S} {10,-6:S} {11,-6:S} {12,-6:S} {13,-6:S} {14,-6:f} {15,-6:S} {16,-7:S} {17,-7:S} ";// 31.03.09 Vikas sharma
				
				string info1= " {0,-50:S} {1,-7:S} {2,-7:S} {3,-7:S} {4,-7:S} {5,-7:S} {6,-7:S} {7,-7:S} {8,-7:S} {9,-7:S} {10,-7:S} {11,-7:S} {12,-7:S} {13,-8:S} {14,-7:f} {15,-7:S} {16,-7:S} {17,-9:S} ";// 19.08.09 Vikas sharma
				sw.WriteLine(info1,"Total:",check4.ToString(),check5.ToString(),check6.ToString(),check7.ToString(),check8.ToString(),check9.ToString(),check10.ToString(),check11.ToString(),check12.ToString(),check1.ToString(),check2.ToString(),check3.ToString(),totalpoints.ToString(),Math.Round(totliter),"","",totalpoints_new.ToString()); //
				//sw.WriteLine("Total                                                                 "+check4.ToString()+check5.ToString()+check6.ToString()+check7.ToString()+check8.ToString()+check9.ToString()+check10.ToString()+check11.ToString()+check12.ToString()+check1.ToString()+check2.ToString()+check3.ToString()+totalpoints.ToString()+Math.Round(totliter,1)+""+""+totalpoints.ToString()); 
				sw.WriteLine("+--------------------+---------------+-------------+--------+--------+------+------+------+------+------+------+------+------+------+------+------+------+------+------+-------+-------+-------+");
				dbobj.Dispose();
				//deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid ");
			}

		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			//SqlDataReader SqlDtr;
			string sql="";
			//int x=0;
			//24.11.2012 int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			int toyear=year+1;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\ServoSadbhavnaSchemeYearWiseReport.xls";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;
			//sql="select dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ro%') "; //30.03.09 Vikas Sharma
			//sql="select dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%') "; // 30.03.09 Vikas Sharma
			
			//coment by vikas 23.05.09 sql="select dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') ";
			
			/****************Start Add by vikas 26.11.2012**************************************/
			if(DropSearchBy.SelectedIndex==0)
			{
				//coment by vikas for remove 0 balance row date on 26.05.09  sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')"; 
				//15.07.09 coment by vikas sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
				//Coment by vikas 20.08.09 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
				//08.09.09 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				//coment by vikas 24.11.2012 sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==1)
			{
				//coment by vikas for remove 0 balance row date on 26.05.09 sql="SELECT  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3 FROM Customer where Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%')";
				//Coment by vikas 20.08.09 sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.Net_Amount) FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type";
				//coment by vikas 24.11.2012 sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and Cust_Type like('"+DropCtype.SelectedItem.Value.ToString()+"%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				
				sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==2)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"')  and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
				
			}
			else if (DropSearchBy.SelectedIndex==3)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==4)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			else if (DropSearchBy.SelectedIndex==5)
			{
				sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6";
			}
			/****************End**************************************/
					
			rdr=obj.GetRecordSet(sql);
			//sw.WriteLine("|    Customer Name   |     Place     |Customer Type|Unique  |StartUp | APR  | MAY  |JUNE  |JULY  | AUGT |SEPT  |OCT   |NOV   |DEC   |JAN   |FEB   |MAR   |POINT | COMM |Pt.XTRA|Pt.XTRA|  Pt.  |");
			//sw.WriteLine("|                    |               |             | Code   |Points  |      |      |      |      |      |      |      |      |      |      |      |      |COMM  | SALE |  ACH  |  REG  | TOTAL |");
			sw.WriteLine("Customer Name\tPlace\tCustomer Type\tUnique Code\tStartUp Point\tAPR\tMAY\tJUNE\tJULY\tAUGT\tSEPT\tOCT\tNOV\tDEC\tJAN\tFEB\tMAR\tPoint Comm.\tComm. Sale\tPoint Xtra ACH\tPoint Xtra REG\tTotal Point");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["s1"].ToString().Trim()+"\t"+
					rdr["s2"].ToString().Trim()+"\t"+
					rdr["s3"].ToString().Trim()+"\t"+
					rdr["s4"].ToString().Trim()+"\t"+
					""+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"4" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"5" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"6" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"7" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"8" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"9" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"10" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"11" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"12" ).ToString()+"\t"+
					janpt(rdr["s5"].ToString().Trim(),"1" ).ToString()+"\t"+
					janpt(rdr["s5"].ToString().Trim(),"2" ).ToString()+"\t"+
					janpt(rdr["s5"].ToString().Trim(),"3" ).ToString()+"\t"+
					points(rdr["s5"].ToString().Trim()).ToString()+"\t"+
					liter(rdr["s5"].ToString().Trim() )+"\t\t\t"+
					//Coment by vikas 20.08.09 points(rdr["s5"].ToString().Trim()).ToString() );
					points_new(rdr["s5"].ToString().Trim()).ToString() );

			}
			sw.WriteLine("Total\t\t\t\t"+"\t"+check4.ToString()+"\t"+check5.ToString()+"\t"+check6.ToString()+"\t"+check7.ToString()+"\t"+check8.ToString()+"\t"+check9.ToString()+"\t"+check10.ToString()+"\t"+check11.ToString()+"\t"+check12.ToString()+"\t"+check1.ToString()+"\t"+check2.ToString()+"\t"+check3.ToString()+"\t"+totalpoints.ToString()+"\t"+totliter.ToString()+"\t\t\t"+totalpoints_new.ToString());
			//sw.WriteLine("Total\t\t\t\t\t"+check4.ToString()+"\t"+check5.ToString(),check6.ToString()+"\t"+check7.ToString()+"\t"+check8.ToString()+"\t"+check9.ToString()+"\t"+check10.ToString()+"\t"+check11.ToString()+"\t"+check12.ToString()+"\t"+check1.ToString()+"\t"+check2.ToString()+"\t"+check3.ToString()+"\t"+totalpoints.ToString()+"\t"+totliter.ToString()+"\t\t\t"+totalpoints.ToString());
			rdr.Close();
			sw.Close();
		}

		public void ConvertToExcel_New()
		{
			InventoryClass obj=new InventoryClass();
			string sql="";
			int year=System.Convert.ToInt32(dropyear.SelectedItem.Value.ToString());
			int toyear=year+1;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\ServoSadbhavnaSchemeYearWiseReport1.xls";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;

			/****************Start Add by vikas 26.11.2012**************************************/
			if(DropSearchBy.SelectedIndex==0)
			{
				if(chkboxNew.Checked==false)
				{
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
				else
				{
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
			}
			else if (DropSearchBy.SelectedIndex==1)
			{
				if(chkboxNew.Checked==false)
				{
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
				else
				{
					sql="SELECT distinct dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
			}
			else if (DropSearchBy.SelectedIndex==2)
			{
				if(chkboxNew.Checked==false)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
				else
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value.ToString()+"')  and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
			}
			else if (DropSearchBy.SelectedIndex==3)
			{
				if(chkboxNew.Checked==false)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
				else
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and city='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
			}
			else if (DropSearchBy.SelectedIndex==4)
			{
				if(chkboxNew.Checked==false)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
				else
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and state='"+DropValue.Value.ToString()+"' and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
			}
			else if (DropSearchBy.SelectedIndex==5)
			{
				if(chkboxNew.Checked==false)
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and sm.totalqtyltr>=500 and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
				else
				{
					sql="select distinct  dbo.Customer.Cust_ID s5, dbo.Customer.sadbhavnacd s4, dbo.Customer.Cust_Name s1, dbo.Customer.City s2, dbo.Customer.Cust_Type s3,sum(sm.totalqtyltr) s6 FROM Customer,Sales_Master sm where dbo.customer.cust_id=sm.cust_id and (Cust_Type like 'Bazzar' or Cust_Type like 'Ksk%' or Cust_Type like 'N-Ksk%' or Cust_Type like 'Essar ro%') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value.ToString()+"') and sm.Net_Amount<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '4/1/"+year+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '3/31/"+toyear+"' group by dbo.Customer.Cust_ID,dbo.Customer.sadbhavnacd,dbo.Customer.Cust_Name,dbo.Customer.City,dbo.Customer.Cust_Type order by s6 desc";
				}
			}

			dtr=obj.GetRecordSet(sql);
			sw.WriteLine("SNo.\tCustomer Name-Place\tUnique Code\tCustomer Type\t\tAPR\tMAY\tJUNE\tJULY\tAUGT\tSEPT\tOCT\tNOV\tDEC\tJAN\tFEB\tMAR\tTotal\tStartup Points\tBasic Points\tBonus Points\tXtra Points\tTotal Point");

			int i=1;
			double Tot_Xtra_Points=0;
			double Tot_Basic_Points=0;
			double Tot_Grand=0;							
			double Tot_Net=0;
			while(dtr.Read())
			{
				Tot_Xtra_Points=0;
				Tot_Basic_Points=0;
														
				sw.Write(i.ToString()+"\t");      //1
				sw.Write(dtr["s1"].ToString()+","+dtr["s2"].ToString()+"\t");     //2
				sw.Write(dtr["s4"].ToString()+"\t"+
				dtr["s3"].ToString()+"\tBasic Points\t");  //3
				CBPoints4=aprpt(dtr["s5"].ToString(),"4");
				Tot_CBPoints+=CBPoints4;
				Tot_CBPoints4=Tot_CBPoints;
				sw.Write(CBPoints4+"\t");
				
				CBPoints5=aprpt(dtr["s5"].ToString(),"5");
				Tot_CBPoints+=CBPoints5;
				Tot_CBPoints5=Tot_CBPoints;
				sw.Write(CBPoints5+"\t");
				CBPoints6=aprpt(dtr["s5"].ToString(),"6");
				Tot_CBPoints+=CBPoints6;
				Tot_CBPoints6=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points6=CBPoints6*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points6=CBPoints6*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points6=CBPoints6*0.03;
				}
				else
				{
					Bonus_Points6=0;
				}
				Min.Clear();
				Min.Add(CBPoints4);
				Min.Add(CBPoints5);
				Min.Add(CBPoints6);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points6=CBPoints6*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points6=CBPoints6*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points6=CBPoints6*0.02;
				}
				else
				{
					Xtra_Points6=0;
				}
				Tot_Basic_Points+=Bonus_Points6;
				Tot_Xtra_Points+=Xtra_Points6;
				sw.Write(CBPoints6+"\t");
				
				CBPoints7=aprpt(dtr["s5"].ToString(),"7");
				Tot_CBPoints+=CBPoints7;
				Tot_CBPoints7=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points7=CBPoints7*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points7=CBPoints7*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points7=CBPoints7*0.03;
				}
				else
				{
					Bonus_Points7=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints5);
				Min.Add(CBPoints6);
				Min.Add(CBPoints7);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points7=CBPoints7*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points7=CBPoints7*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points7=CBPoints7*0.02;
				}
				else
				{
					Xtra_Points7=0;
				}
													
				Tot_Basic_Points+=Bonus_Points7;
				Tot_Xtra_Points+=Xtra_Points7;
				sw.Write(CBPoints7+"\t");
				CBPoints8=aprpt(dtr["s5"].ToString(),"8");
				Tot_CBPoints+=CBPoints8;
				Tot_CBPoints8=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points8=CBPoints8*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points8=CBPoints8*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points8=CBPoints8*0.03;
				}
				else
				{
					Bonus_Points8=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints6);
				Min.Add(CBPoints7);
				Min.Add(CBPoints8);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points8=CBPoints8*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points8=CBPoints8*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points8=CBPoints8*0.02;
				}
				else
				{
					Xtra_Points8=0;
				}
													
				Tot_Basic_Points+=Bonus_Points8;
				Tot_Xtra_Points+=Xtra_Points8;
				sw.Write(CBPoints8+"\t");
				CBPoints9=aprpt(dtr["s5"].ToString(),"9");
				Tot_CBPoints+=CBPoints9;
				Tot_CBPoints9=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points9=CBPoints9*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points9=CBPoints9*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points9=CBPoints9*0.03;
				}
				else
				{
					Bonus_Points9=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints7);
				Min.Add(CBPoints8);
				Min.Add(CBPoints9);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points9=CBPoints9*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points9=CBPoints9*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points9=CBPoints9*0.02;
				}
				else
				{
					Xtra_Points9=0;
				}
													
				Tot_Basic_Points+=Bonus_Points9;
				Tot_Xtra_Points+=Xtra_Points9;
				sw.Write(CBPoints9+"\t");
				CBPoints10=aprpt(dtr["s5"].ToString(),"10");
				Tot_CBPoints+=CBPoints10;
				Tot_CBPoints10=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points10=CBPoints10*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points10=CBPoints10*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points10=CBPoints10*0.03;
				}
				else
				{
					Bonus_Points10=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints8);
				Min.Add(CBPoints9);
				Min.Add(CBPoints10);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points10=CBPoints10*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points10=CBPoints10*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points10=CBPoints10*0.02;
				}
				else
				{
					Xtra_Points10=0;
				}
													
				Tot_Basic_Points+=Bonus_Points10;
				Tot_Xtra_Points+=Xtra_Points10;
				sw.Write(CBPoints10+"\t");
				CBPoints11=aprpt(dtr["s5"].ToString(),"11");
				Tot_CBPoints+=CBPoints11;
				Tot_CBPoints11=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points11=CBPoints11*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points11=CBPoints11*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points11=CBPoints11*0.03;
				}
				else
				{
					Bonus_Points11=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints9);
				Min.Add(CBPoints10);
				Min.Add(CBPoints11);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points11=CBPoints11*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points11=CBPoints11*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points11=CBPoints11*0.02;
				}
				else
				{
					Xtra_Points11=0;
				}
													
				Tot_Basic_Points+=Bonus_Points11;
				Tot_Xtra_Points+=Xtra_Points11;
				sw.Write(CBPoints11+"\t");
				CBPoints12=aprpt(dtr["s5"].ToString(),"12");
				Tot_CBPoints+=CBPoints12;
				Tot_CBPoints12=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points12=CBPoints12*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points12=CBPoints12*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points12=CBPoints12*0.03;
				}
				else
				{
					Bonus_Points12=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints10);
				Min.Add(CBPoints11);
				Min.Add(CBPoints12);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points12=CBPoints12*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points12=CBPoints12*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points12=CBPoints12*0.02;
				}
				else
				{
					Xtra_Points12=0;
				}
													
				Tot_Basic_Points+=Bonus_Points12;
				Tot_Xtra_Points+=Xtra_Points12;
				sw.Write(CBPoints12+"\t");
				CBPoints1=janpt(dtr["s5"].ToString(),"1");
				Tot_CBPoints+=CBPoints1;
				Tot_CBPoints1=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points1=CBPoints1*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points1=CBPoints1*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points1=CBPoints1*0.03;
				}
				else
				{
					Bonus_Points1=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints11);
				Min.Add(CBPoints12);
				Min.Add(CBPoints1);
				Min.Sort();
													
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points1=CBPoints1*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points1=CBPoints1*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points1=CBPoints1*0.02;
				}
				else
				{
					Xtra_Points1=0;
				}
													
				Tot_Basic_Points+=Bonus_Points1;
				Tot_Xtra_Points+=Xtra_Points1;
													
				sw.Write(CBPoints1+"\t");
				CBPoints2=janpt(dtr["s5"].ToString(),"2");
				Tot_CBPoints+=CBPoints2;
				Tot_CBPoints2=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points2=CBPoints2*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points2=CBPoints2*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points2=CBPoints2*0.03;
				}
				else
				{
					Bonus_Points2=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints12);
				Min.Add(CBPoints1);
				Min.Add(CBPoints2);
				Min.Sort();
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points2=CBPoints2*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points2=CBPoints2*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points2=CBPoints2*0.02;
				}
				else
				{
					Xtra_Points2=0;
				}
													
				Tot_Basic_Points+=Bonus_Points2;
				Tot_Xtra_Points+=Xtra_Points2;
				sw.Write(CBPoints2+"\t");
				CBPoints3=janpt(dtr["s5"].ToString(),"3");
				Tot_CBPoints+=CBPoints3;
				Tot_CBPoints3=Tot_CBPoints;
				if(Tot_CBPoints>=25000)
				{
					Bonus_Points3=CBPoints3*0.1;
				}
				else if(Tot_CBPoints>=12000)
				{
					Bonus_Points3=CBPoints3*0.05;
				}
				else if(Tot_CBPoints>=5000)
				{
					Bonus_Points3=CBPoints3*0.03;
				}
				else
				{
					Bonus_Points3=0;
				}
													
				Min.Clear();
				Min.Add(CBPoints1);
				Min.Add(CBPoints2);
				Min.Add(CBPoints3);
				Min.Sort();
				if(int.Parse(Min[0].ToString())>=1000)
				{
					Xtra_Points3=CBPoints3*0.08;
				}
				else if(int.Parse(Min[0].ToString())>=500)
				{
					Xtra_Points3=CBPoints3*0.04;
				}
				else if(int.Parse(Min[0].ToString())>=100)
				{
					Xtra_Points3=CBPoints3*0.02;
				}
				else
				{
					Xtra_Points3=0;
				}
													
				Tot_Basic_Points+=Bonus_Points3;
				Tot_Xtra_Points+=Xtra_Points3;
				sw.Write(CBPoints3+"\t");
				sw.Write(points(dtr["s5"].ToString())+"\t");
				sw.Write("\t");
				sw.Write(points(dtr["s5"].ToString())+"\t");
				sw.Write(Tot_Basic_Points+"\t");
				sw.Write(Tot_Xtra_Points+"\t");
			    
				double G_tot=Tot_Basic_Points+Tot_Xtra_Points+double.Parse(points(dtr["s5"].ToString()).ToString());
				sw.Write(G_tot.ToString());
				Tot_Grand+=double.Parse(points(dtr["s5"].ToString()).ToString());
				Tot_Net+=G_tot;
				sw.WriteLine();
				Tot_CBPoints=0;
                
				sw.WriteLine("\t\t\t\tComm. Basic Points\t"+
					Tot_CBPoints4.ToString()+"\t"+
					Tot_CBPoints5.ToString()+"\t"+
					Tot_CBPoints6.ToString()+"\t"+
					Tot_CBPoints7.ToString()+"\t"+
					Tot_CBPoints8.ToString()+"\t"+
					Tot_CBPoints9.ToString()+"\t"+
					Tot_CBPoints10.ToString()+"\t"+
					Tot_CBPoints11.ToString()+"\t"+
					Tot_CBPoints12.ToString()+"\t"+
					Tot_CBPoints1.ToString()+"\t"+
					Tot_CBPoints2.ToString()+"\t"+
					Tot_CBPoints3.ToString());
		
				
				sw.WriteLine("\t\t\t\tBonus Points\t\t\t"+
					Bonus_Points6.ToString()+"\t"+
					Bonus_Points7.ToString()+"\t"+
					Bonus_Points8.ToString()+"\t"+
					Bonus_Points9.ToString()+"\t"+
					Bonus_Points10.ToString()+"\t"+
					Bonus_Points11.ToString()+"\t"+
					Bonus_Points12.ToString()+"\t"+
					Bonus_Points1.ToString()+"\t"+
					Bonus_Points2.ToString()+"\t"+
					Bonus_Points3.ToString());

				sw.WriteLine("\t\t\t\tXtra Points\t\t\t"+
					Xtra_Points6.ToString()+"\t"+
					Xtra_Points7.ToString()+"\t"+
					Xtra_Points8.ToString()+"\t"+
					Xtra_Points9.ToString()+"\t"+
					Xtra_Points10.ToString()+"\t"+
					Xtra_Points11.ToString()+"\t"+
					Xtra_Points12.ToString()+"\t"+
					Xtra_Points1.ToString()+"\t"+
					Xtra_Points2.ToString()+"\t"+
					Xtra_Points3.ToString());
				i++;
			}
			sw.WriteLine("\t\tTotal\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"+Tot_Grand.ToString()+"\t\t\t\t\t"+Tot_Net.ToString());
			
			/*vikas 26.11.2012 while(rdr.Read())
			{
				sw.WriteLine(rdr["s1"].ToString().Trim()+"\t"+
					rdr["s2"].ToString().Trim()+"\t"+
					rdr["s3"].ToString().Trim()+"\t"+
					rdr["s4"].ToString().Trim()+"\t"+
					""+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"4" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"5" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"6" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"7" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"8" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"9" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"10" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"11" ).ToString()+"\t"+
					aprpt(rdr["s5"].ToString().Trim(),"12" ).ToString()+"\t"+
					janpt(rdr["s5"].ToString().Trim(),"1" ).ToString()+"\t"+
					janpt(rdr["s5"].ToString().Trim(),"2" ).ToString()+"\t"+
					janpt(rdr["s5"].ToString().Trim(),"3" ).ToString()+"\t"+
					points(rdr["s5"].ToString().Trim()).ToString()+"\t"+
					liter(rdr["s5"].ToString().Trim() )+"\t\t\t"+
					points_new(rdr["s5"].ToString().Trim()).ToString() );

			}*/
			//sw.WriteLine("Total\t\t\t\t"+"\t"+check4.ToString()+"\t"+check5.ToString()+"\t"+check6.ToString()+"\t"+check7.ToString()+"\t"+check8.ToString()+"\t"+check9.ToString()+"\t"+check10.ToString()+"\t"+check11.ToString()+"\t"+check12.ToString()+"\t"+check1.ToString()+"\t"+check2.ToString()+"\t"+check3.ToString()+"\t"+totalpoints.ToString()+"\t"+totliter.ToString()+"\t\t\t"+totalpoints_new.ToString());
			//rdr.Close();
			sw.Close();
		}
		/// <summary>
		/// Prepares the report file SadBhavnaschemeYearWise.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			flage=1;
			if(dropType.SelectedIndex==0)
			{
				makingReport();
			}

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SadbhavnaSchemeYearWise.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click  SadbhavnaSchemeYearWise   userid  ");
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeYearWise Printed    EXCEPTION  "+ ane.Message+" userid  ");
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeYearWise Printed  EXCEPTION  "+ se.Message+"  userid  ");
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed   EXCEPTION "+es.Message+"  userid  ");
				}
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed  EXCEPTION   "+ es.Message+"  userid  ");
			}
		}

		/// <summary>
		/// Prepares the excel report file SadBhavnaSchemeYearWise.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				flage=0;
				if(dropType.SelectedIndex==0)
				{
					if(GridSalesReport.Visible==true)
					{
						ConvertToExcel();
						MessageBox.Show("Successfully Convert File into Excel Format");
						CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SadbhavnaSchemeYearWise Report Convert Into Excel Format ,  userid  "+uid);
					}
					else
					{
						MessageBox.Show("Please Click the View Button First");
						return;
					}
				}
				else
				{
					ConvertToExcel_New();
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SadbhavnaSchemeYearWise Report Convert Into Excel Format ,  userid  "+uid);
					
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeYearWise.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" SadbhavnaSchemeYearWise Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}
	}
}