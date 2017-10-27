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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using DBOperations; 

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for Price_Calculation.
	/// </summary>
	public partial class PriceCalculation : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton radSale;
		string uid;
		DBUtil dbobj = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
	
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
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs ,Method:Pageload   EXCEPTION: "+ex.Message+".  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="26";
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
				GridSalesSummarized.Visible=false;
				GridPurchaseSummarized.Visible=false;
				GridSummarizedSummarized.Visible=false;
				gridCurrPrice.Visible=false;
				GetMultiValue();
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select vat_rate,entrytax from organisation");
				btnShow.Enabled=true;
				BtnPrint.Enabled=true;
				btnExcel.Enabled=true;
				if(rdr.Read())
				{
					tempVat.Value=rdr.GetValue(0).ToString();
					tempET.Value=rdr.GetValue(1).ToString();
				}
				else
				{
					MessageBox.Show("Fill Organisation Form First");
					btnShow.Enabled=false;
					BtnPrint.Enabled=false;
					btnExcel.Enabled=false;
					return;
				}
				txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
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
		/// This method is used to view the report with the help of Bindthedata() function.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
				strOrderBy = "Prod_Code";
				Session["Column"] = "Prod_Code";
				Session["Order"] = "ASC";
				BindTheData();
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Method:btnShow_Click"+ " Price calculation Report Viewed "+" userid "+ uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Method:btnShow_Click"+ " Price calculation Report, Exception : "+ex.Message+" userid "+ uid);
			}
		}

		/// <summary>
		/// This method is used to bind the datagrid and display the information by given order and display the data grid.
		/// </summary>
		public void BindTheData()
		{
			SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string sqlstr="";
			if(RadSale.Checked)
			{
				if(RadSummarize.Checked)
				{
					if(chkCurrPrice.Checked)
					{
						sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as sal_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+="group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp order by p.prod_id";
					}
				}
				else
				{
					if(chkCurrPrice.Checked)
					{
						sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,sal_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,sal_rate,mrp,eff_date order by p.prod_id";
					}
				}
			}
			else if(RadPurchase.Checked)
			{
				if(RadSummarize.Checked)
				{
					if(chkCurrPrice.Checked)
					{
						sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as pur_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp order by p.prod_id";
					}
				}
				else
				{
					if(chkCurrPrice.Checked)
					{
						sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,pur_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,pur_rate,mrp,eff_date order by p.prod_id";
					}
				}
			}
			else
			{
				if(RadSummarize.Checked)
				{
					if(chkCurrPrice.Checked)
					{
						sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as pur_rate,'' as sal_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp order by p.prod_id";
					}
				}
				else
				{
					if(chkCurrPrice.Checked)
					{
						sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"'";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,pur_rate,sal_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,pur_rate,sal_rate,mrp,eff_date order by p.prod_id";
					}
				}
			}
			DataSet ds= new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			da.Fill(ds, "Products");
			DataTable dtCustomers = ds.Tables["Products"];
			DataView dv=new DataView(dtCustomers);
			dv.Sort = strOrderBy;
			Cache["strOrderBy"]=strOrderBy;
			GridSalesSummarized.Visible=false;
			GridPurchaseSummarized.Visible=false;
			GridSummarizedSummarized.Visible=false;
			gridCurrPrice.Visible=false;
			if(dv.Count==0)
			{
				MessageBox.Show("Data not available");
			}
			else
			{
				if(chkCurrPrice.Checked)
				{
					gridCurrPrice.DataSource=dv;
					gridCurrPrice.DataBind();
					gridCurrPrice.Visible=true;
				}
				else
				{
					if(RadSale.Checked)
					{
						GridSalesSummarized.DataSource=dv;
						GridSalesSummarized.DataBind();
						GridSalesSummarized.Visible=true;
					}
					else if(RadPurchase.Checked)
					{
						GridPurchaseSummarized.DataSource=dv;
						GridPurchaseSummarized.DataBind();
						GridPurchaseSummarized.Visible=true;
					}
					else
					{
						GridSummarizedSummarized.DataSource=dv;
						GridSummarizedSummarized.DataBind();
						GridSummarizedSummarized.Visible=true;
					}
				}
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
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid ");
			}
		}

		/// <summary>
		/// This method is used to return the sales rate from price updation table
		/// </summary>
		public string GetBasicRate(string Prod_ID, string sal_rate)
		{
			if(sal_rate=="")
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select top 1 sal_rate from price_updation where prod_id='"+Prod_ID+"' order by eff_date desc");
				if(rdr.Read())
				{
					return GenUtil.strNumericFormat(rdr.GetValue(0).ToString());
				}
				else
				{
					return "0.00";
				}
				//rdr.Close();
			}
			else
				return GenUtil.strNumericFormat(sal_rate);
		}

		/// <summary>
		/// This method is used to return the purchase rate from price updation table
		/// </summary>
		public string GetBasicPurRate(string Prod_ID, string pur_rate)
		{
			if(pur_rate=="")
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select top 1 pur_rate from price_updation where prod_id='"+Prod_ID+"' order by eff_date desc");
				if(rdr.Read())
				{
					return GenUtil.strNumericFormat(rdr.GetValue(0).ToString());
				}
				else
				{
					return "0.00";
				}
				//rdr.Close();
			}
			else
				return GenUtil.strNumericFormat(pur_rate);
		}

		/// <summary>
		/// This method is used to return the top sales rate with vat from price updation table
		/// </summary>
		public string GetSalesRate(string Prod_ID, string sal_rate)
		{
			if(sal_rate=="")
			{
				double SalesRate=0;
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select top 1 sal_rate from price_updation where prod_id='"+Prod_ID+"' order by eff_date desc");
				if(rdr.Read())
				{
					SalesRate=double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				double Vat=SalesRate*double.Parse(tempVat.Value)/100;
				SalesRate=SalesRate+Vat;
				return GenUtil.strNumericFormat(SalesRate.ToString());
			}
			else
			{
				double Vat = double.Parse(sal_rate)*double.Parse(tempVat.Value)/100;
				double SalesRate=double.Parse(sal_rate)+Vat;
				return GenUtil.strNumericFormat(SalesRate.ToString());
			}
		}

		/// <summary>
		/// This method is used to return the top purchase rate with entry tax from price updation table
		/// </summary>
		public string GetPurRate(string Prod_ID, string pur_rate)
		{
			if(pur_rate=="")
			{
				double PurRate=0;
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select top 1 pur_rate from price_updation where prod_id='"+Prod_ID+"' order by eff_date desc");
				if(rdr.Read())
				{
					PurRate=double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				double ET=PurRate*double.Parse(tempET.Value)/100;
				PurRate=PurRate+ET;
				double Vat=PurRate*double.Parse(tempVat.Value)/100;
				PurRate=PurRate+Vat;
				return GenUtil.strNumericFormat(PurRate.ToString());
			}
			else
			{
				double ET=double.Parse(pur_rate)*double.Parse(tempET.Value)/100;
				double PurRate=double.Parse(pur_rate)+ET;
				double Vat=PurRate*double.Parse(tempVat.Value)/100;
				PurRate=PurRate+Vat;
				return GenUtil.strNumericFormat(PurRate.ToString());
			}
		}

		/// <summary>
		/// This method return the effected date.
		/// </summary>
		public string GetEffDate(string Prod_ID, string eff_date)
		{
			if(eff_date=="")
			{
				string effDate="";
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = obj.GetRecordSet("select top 1 eff_date from price_updation where prod_id='"+Prod_ID+"' order by eff_date desc");
				if(rdr.Read())
				{
					effDate = GenUtil.str2MMDDYYYY(GenUtil.trimDate(rdr.GetValue(0).ToString()));
				}
				rdr.Close();
				return effDate;
			}
			else
			{
				return GenUtil.str2MMDDYYYY(GenUtil.trimDate(eff_date));
			}
		}

		/// <summary>
		/// This method return the calculated vat rate
		/// </summary>
		public string GetVatRate(string Basic)
		{
			double basicRate=double.Parse(Basic)*double.Parse(tempVat.Value)/100;
			return GenUtil.strNumericFormat(basicRate.ToString());
		}
		
		public double DLP=0;
		public string GetVatRateNew()
		{
			double basicRate=SaleRate*double.Parse(tempVat.Value)/100;
			DLP=SaleRate+basicRate;
			return GenUtil.strNumericFormat(basicRate.ToString());
		}
		/// <summary>
		/// This method return the calculated Entry tax rate
		/// </summary>
		public double SaleRate=0;
		public string GetETRate(string Basic)
		{
			double basicRate=double.Parse(Basic)*double.Parse(tempET.Value)/100;
			SaleRate=basicRate+double.Parse(Basic);
			return GenUtil.strNumericFormat(basicRate.ToString());
		}

		public string GetSaleRateNew()
		{
			//SaleRate=basicRate+double.Parse(Basic);
			return GenUtil.strNumericFormat(SaleRate.ToString());
		}

		public string GetDLPNew()
		{
			//SaleRate=basicRate+double.Parse(Basic);
			return GenUtil.strNumericFormat(DLP.ToString());
		}
		/// <summary>
		/// This method return the calculated purchase vat rate
		/// </summary>
		public string GetVatPerRate(string Basic, string ET)
		{
			double basicRate=(double.Parse(Basic)+double.Parse(ET))*double.Parse(tempVat.Value)/100;
			return GenUtil.strNumericFormat(basicRate.ToString());
		}

		/// <summary>
		/// This method return the calculated net amount rate
		/// </summary>
		public string GetNetAmount(string Basic, string Vat)
		{
			double NetAmt = double.Parse(Basic)+double.Parse(Vat);
			return GenUtil.strNumericFormat(NetAmt.ToString());
		}
		
		/// <summary>
		/// This method return the calculated net amount rate
		/// </summary>
		public string GetNetAmount(string Basic, string Vat, string ET)
		{
			double NetAmt = double.Parse(Basic)+double.Parse(Vat)+double.Parse(ET);
			return GenUtil.strNumericFormat(NetAmt.ToString());
		}

		/// <summary>
		/// This method is used to return the difference between sales rate and purchase rate.
		/// </summary>
		public string GetDiff(string PurRate, string SalesRate)
		{
			double Diff = double.Parse(SalesRate)-double.Parse(PurRate);
			return GenUtil.strNumericFormat(Diff.ToString());
		}

		/// <summary>
		/// Prepares the report file PriceCalculation.txt for printing.
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
					CreateLogFiles.ErrorLog("Form:MechanicReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PriceCalculation Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PriceCalculation.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PriceCalculation Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PriceCalculation Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PriceCalculation Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    PriceCalculation Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PriceCalculation1.txt";
			StreamWriter sw = new StreamWriter(path);
			string sqlstr="";
			string Type="";
			if(chkCurrPrice.Checked)
			{
				sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
				if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				{
					string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
					sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
				}
				else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
					sqlstr+="and Prod_Name='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
					sqlstr+="and Category='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sqlstr+="and Prod_Code='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sqlstr+="and Pack_Type='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
					sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
			}
			else
			{
				if(RadSale.Checked)
				{
					if(RadSummarize.Checked)
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as sal_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+="group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp";
						Type="Summarized";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,sal_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,sal_rate,mrp,eff_date";
						Type="Complete";
					}
				}
				else if(RadPurchase.Checked)
				{
					if(RadSummarize.Checked)
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as pur_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp";
						Type="Summarized";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,pur_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,pur_rate,mrp,eff_date";
						Type="Complete";
					}
				}
				else
				{
					if(RadSummarize.Checked)
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as pur_rate,'' as sal_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp";
						Type="Summarized";
					}
					else
					{
						sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,pur_rate,sal_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
						if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
						{
							string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
						}
						else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
							sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
							sqlstr+="and Category='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
							sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
							sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
						else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
							sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
						sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,pur_rate,sal_rate,mrp,eff_date";
						Type="Complete";
					}
				}
			}
			sqlstr=sqlstr+" order by "+Cache["strOrderBy"];
			dbobj.SelectQuery(sqlstr,ref rdr);
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
			string des="";//,Name="";
			if(chkCurrPrice.Checked)
			{
				des="------------------------------------------------------------------------------------------------------------------------";
			}
			else if(RadSale.Checked)
			{
				des="--------------------------------------------------------------------------------------------------------------------------------------";
				//Name = "Sales";
			}
			else if(RadPurchase.Checked)
			{
				//Name = "Purchase";
				des="---------------------------------------------------------------------------------------------------------------------------------";
			}
			else
			{
				//Name = "Summarized";
				des="------------------------------------------------------------------------------------------------------------------------";
			}
			
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			sw.WriteLine(GenUtil.GetCenterAddr("==========================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("PRICE CALCULATION REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("==========================",des.Length));
			string info = "";
			//sw.WriteLine("");
			if(chkCurrPrice.Checked)
			{
				//sw.WriteLine(" Current Price Of The All Products");
				//sw.WriteLine(" Search By : "+DropSearch.SelectedItem.Text+", Concerning Value : "+DropValue.Value);
				sw.WriteLine("+------------+-------------------------+-----------+----------+-----------------+----------------+----------+----------+");
				sw.WriteLine("|Product Code|      Product Name       | Pack Type |   MRP    |   Sales Price   | Purchase Price |Differance| Eff Date |");
				sw.WriteLine("+------------+-------------------------+-----------+----------+-----------------+----------------+----------+----------+");
				//             123456789012 1234567890123456789012345 12345678901 1234567890 12345678901234567 1234567890123456 1234567890 1234567890
				info = "|{0,-12:S}|{1,-25:F}|{2,-11:F}|{3,10:F}|{4,17:F}|{5,16:F}|{6,10:F}|{7,-10:F}|";
			}
            else if(RadSale.Checked)
			{
				
				sw.WriteLine("+------------+-------------------------+-----------+----------+-------------+-----------+-----------+-----------+--------------+----------+");
				sw.WriteLine("|Product Code|      Product Name       | Pack Type |   MRP    |Purchase Rate| Entry Tax | Sale Rate |    Vat    |      DLP     | Eff Date |");
				sw.WriteLine("+------------+-------------------------+-----------+----------+-------------+-----------+-----------+-----------+--------------+----------+");
				//             123456789012 1234567890123456789012345 12345678901 1234567890 1234567890123 12345678901 12345678901 12345678901 12345678901234 1234567890
				info = "|{0,-12:S}|{1,-25:F}|{2,-11:F}|{3,10:F}|{4,13:F}|{5,11:F}|{6,11:F}|{7,-11:F}|{8,-14:F}|{9,-10:F}|";
			}
			else if(RadPurchase.Checked)
			{
				//sw.WriteLine(" Price Calculation For : Purchase, Report Type : "+Type);
				//sw.WriteLine(" Search By : "+DropSearch.SelectedItem.Text+", Concerning Value : "+DropValue.Value);
				sw.WriteLine("+------------+-------------------------+-----------+----------+-------------+-----------+-----------+----------------+----------+");
				sw.WriteLine("|Product Code|      Product Name       | Pack Type |   MRP    | Basic Price |    ET     |    Vat    | Purchase Price | Eff Date |");
				sw.WriteLine("+------------+-------------------------+-----------+----------+-------------+-----------+-----------+----------------+----------+");
				//             123456789012 1234567890123456789012345 12345678901 1234567890 1234567890123 12345678901 12345678901 1234567890123456 1234567890
				info = "|{0,-12:S}|{1,-25:F}|{2,-11:F}|{3,10:F}|{4,13:F}|{5,11:F}|{6,11:F}|{7,16:F}|{8,-10:F}|";
			}
			else
			{
				//sw.WriteLine(" Price Calculation For : Summarized, Report Type : "+Type);
				//sw.WriteLine(" Search By : "+DropSearch.SelectedItem.Text+", Concerning Value : "+DropValue.Value);
				sw.WriteLine("+------------+-------------------------+-----------+----------+-----------------+----------------+----------+----------+");
				sw.WriteLine("|Product Code|      Product Name       | Pack Type |   MRP    |   Sales Price   | Purchase Price |Differance| Eff Date |");
				sw.WriteLine("+------------+-------------------------+-----------+----------+-----------------+----------------+----------+----------+");
				//             123456789012 1234567890123456789012345 12345678901 1234567890 12345678901234567 1234567890123456 1234567890 1234567890
				info = "|{0,-12:S}|{1,-25:F}|{2,-11:F}|{3,10:F}|{4,17:F}|{5,16:F}|{6,10:F}|{7,-10:F}|";
			}
			if(rdr.HasRows)
			{
				if(chkCurrPrice.Checked)
				{
					InventoryClass obj1 = new InventoryClass();
					SqlDataReader rdr1=null;
					while(rdr.Read())
					{
						string Sal="",Pur="",Diff="",EffDate="";
						rdr1 = obj1.GetRecordSet("select top 1 * from price_updation where prod_id=(select Prod_ID from Products where prod_name='"+rdr["Prod_Name"].ToString()+"' and Pack_Type='"+rdr["Pack_Type"].ToString()+"') order by eff_date desc");
						if(rdr1.Read())
						{
							Sal=GenUtil.strNumericFormat(rdr1["Sal_Rate"].ToString());
							Diff=GenUtil.strNumericFormat(System.Convert.ToString(double.Parse(rdr1["Sal_Rate"].ToString())-double.Parse(rdr1["Pur_Rate"].ToString())));
							Pur=GenUtil.strNumericFormat(rdr1["Pur_Rate"].ToString());
							EffDate=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr1["Eff_Date"].ToString()));
						}
						else
						{
							Sal="";
							Diff="";
							Pur="";
							EffDate="";
						}
						rdr1.Close();
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString(),25),
							rdr["Pack_Type"].ToString(),
							rdr["MRP"].ToString(),
							Sal,Pur,Diff,EffDate
							);
					}
				}
				else if(RadSale.Checked)
				{
					while(rdr.Read())
					{
						/* 5.4.2013 sw.WriteLine(info,rdr["Prod_Code"].ToString(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString(),25),
							rdr["Pack_Type"].ToString(),
							rdr["MRP"].ToString(),
							GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()),
							GetVatRate(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())),
							GetNetAmount(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()),GetVatRate(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()))),
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);*/

						sw.WriteLine(info,rdr["Prod_Code"].ToString(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString(),25),
							rdr["Pack_Type"].ToString(),
							rdr["MRP"].ToString(),
							GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()),
							GetETRate(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())),
							GetSaleRateNew(),
							GetVatRateNew(),
							GetDLPNew(),
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);
					}
				}
				else if(RadPurchase.Checked)
				{
					while(rdr.Read())
					{
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString(),25),
							rdr["Pack_Type"].ToString(),
							rdr["MRP"].ToString(),
							GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),
							GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString())),
							GetVatPerRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()))),
							GetNetAmount(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetVatPerRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()))),GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()))),
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);
					}
				}
				else
				{
					while(rdr.Read())
					{
						sw.WriteLine(info,rdr["Prod_Code"].ToString(),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString(),25),
							rdr["Pack_Type"].ToString(),
							rdr["MRP"].ToString(),
							GetSalesRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()),
							GetPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),
							GetDiff(GetPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetSalesRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())),
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);
					}
				}
			}
			if(chkCurrPrice.Checked)
				sw.WriteLine("+------------+-------------------------+-----------+----------+-----------------+----------------+----------+----------+");
			else if(RadSale.Checked)
				sw.WriteLine("+------------+-------------------------+-----------+----------+-------------+-----------+-----------+-----------+--------------+----------+");
			else if(RadPurchase.Checked)
				sw.WriteLine("+------------+-------------------------+-----------+----------+-------------+-----------+-----------+----------------+----------+");
			else
				sw.WriteLine("+------------+-------------------------+-----------+----------+-----------------+----------------+----------+----------+");
			dbobj.Dispose();
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\PriceCalculaion.xls";
			StreamWriter sw = new StreamWriter(path);
			string sqlstr="";
			
			string Type="";
			if(chkCurrPrice.Checked)
			{
				sqlstr="select prod_id,prod_code,Prod_Name,Pack_Type,mrp from products where prod_id<>'' ";
				if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				{
					string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
					sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"'";
				}
				else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
					sqlstr+="and Prod_Name='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
					sqlstr+="and Category='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
					sqlstr+="and Prod_Code='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
					sqlstr+="and Pack_Type='"+DropValue.Value+"'";
				else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
					sqlstr+="and Pack_Unit='"+DropValue.Value+"'";
			}
			else if(RadSale.Checked)
			{
				if(RadSummarize.Checked)
				{
					sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as sal_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
					if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					{
						string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
					}
					else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
						sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
						sqlstr+="and Category='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
						sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
						sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
						sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
					sqlstr+="group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp";
                    Type="Summarized";
				}
				else
				{
					sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,sal_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
					if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					{
						string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
					}
					else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
						sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
						sqlstr+="and Category='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
						sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
						sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
						sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
					sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,sal_rate,mrp,eff_date";
					Type="Complete";
				}
			}
			else if(RadPurchase.Checked)
			{
				if(RadSummarize.Checked)
				{
					sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as pur_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
					if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					{
						string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
					}
					else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
						sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
						sqlstr+="and Category='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
						sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
						sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
						sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
					sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp";
					Type="Summarized";
				}
				else
				{
					sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,pur_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
					if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					{
						string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
					}
					else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
						sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
						sqlstr+="and Category='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
						sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
						sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
						sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
					sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,pur_rate,mrp,eff_date";
					Type="Complete";
				}
			}
			else
			{
				if(RadSummarize.Checked)
				{
					sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,'' as pur_rate,'' as sal_rate,'' as eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
					if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					{
						string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
					}
					else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
						sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
						sqlstr+="and Category='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
						sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
						sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
						sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
					sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp";
					Type="Summarized";
				}
				else
				{
					sqlstr="select p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,mrp,pur_rate,sal_rate,eff_date from price_updation pu,products p where p.prod_id=pu.prod_id and cast(floor(cast(Eff_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Eff_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' ";
					if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
					{
						string[] strprod=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sqlstr+="and Prod_Name='"+strprod[0]+"' and Pack_Type='"+strprod[1]+"' ";
					}
					else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
						sqlstr+="and Prod_Name='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
						sqlstr+="and Category='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
						sqlstr+="and Prod_Code='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
						sqlstr+="and Pack_Type='"+DropValue.Value+"' ";
					else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
						sqlstr+="and Pack_Unit='"+DropValue.Value+"' ";
					sqlstr+=" group by p.prod_id,p.prod_code,p.Prod_Name,p.Pack_Type,pur_rate,sal_rate,mrp,eff_date";
					Type="Complete";
				}
			}
			sqlstr=sqlstr+" order by "+Cache["strOrderBy"];
			dbobj.SelectQuery(sqlstr,ref rdr);
			
			if(chkCurrPrice.Checked)
			{
				sw.WriteLine("Product Code\tProduct Name\tPack Type\tMRP\tSales Price\tPurchase Price\tDifferance\tEff. Date");
			}
			else if(RadSale.Checked)
			{
				sw.WriteLine("Product Code\tProduct Name\tPack Type\tMRP\tPurchase Rate\tEntry Tax\tSale Rate\tVat\tDLP\tEff. Date");
			}
			else if(RadPurchase.Checked)
			{
				sw.WriteLine("Product Code\tProduct Name\tPack Type\tMRP\tBasic Price\tET\tVat\tPurchase Price\tEff. Date");
			}
			else
			{
				sw.WriteLine("Product Code\tProduct Name\tPack Type\tMRP\tSales Price\tPurchase Price\tDifferance\tEff. Date");
			}
			if(rdr.HasRows)
			{
				if(chkCurrPrice.Checked)
				{
					InventoryClass obj1 = new InventoryClass();
					SqlDataReader rdr1=null;
					while(rdr.Read())
					{
						string Sal="",Pur="",Diff="",EffDate="";
						rdr1 = obj1.GetRecordSet("select top 1 * from price_updation where prod_id=(select Prod_ID from Products where prod_name='"+rdr["Prod_Name"].ToString()+"' and Pack_Type='"+rdr["Pack_Type"].ToString()+"') order by eff_date desc");
						if(rdr1.Read())
						{
							Sal=GenUtil.strNumericFormat(rdr1["Sal_Rate"].ToString());
							Diff=GenUtil.strNumericFormat(System.Convert.ToString(double.Parse(rdr1["Sal_Rate"].ToString())-double.Parse(rdr1["Pur_Rate"].ToString())));
							Pur=GenUtil.strNumericFormat(rdr1["Pur_Rate"].ToString());
							EffDate=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr1["Eff_Date"].ToString()));
						}
						else
						{
							Sal="";
							Diff="";
							Pur="";
							EffDate="";
						}
						rdr1.Close();
						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							GenUtil.TrimLength(rdr["Prod_Name"].ToString(),25)+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["MRP"].ToString()+"\t"+
							Sal+"\t"+Pur+"\t"+Diff+"\t"+EffDate
							);
					}
				}
				else if(RadSale.Checked)
				{
					while(rdr.Read())
					{
						/*sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["MRP"].ToString()+"\t"+
							GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())+"\t"+
							GetVatRate(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()))+"\t"+
							GetNetAmount(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()),GetVatRate(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())))+"\t"+
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);*/

						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["MRP"].ToString()+"\t"+
							GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())+"\t"+
							GetETRate(GetBasicRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()))+"\t"+
							GetSaleRateNew()+"\t"+
							GetVatRateNew()+"\t"+
							GetDLPNew()+"\t"+
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);

					}
				}
				else if(RadPurchase.Checked)
				{
					while(rdr.Read())
					{
						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["MRP"].ToString()+"\t"+
							GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString())+"\t"+
							GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()))+"\t"+
							GetVatPerRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString())))+"\t"+
							GetNetAmount(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetVatPerRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()))),GetETRate(GetBasicPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString())))+"\t"+
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);

					}
				}
				else
				{
					while(rdr.Read())
					{
						sw.WriteLine(rdr["Prod_Code"].ToString()+"\t"+
							rdr["Prod_Name"].ToString()+"\t"+
							rdr["Pack_Type"].ToString()+"\t"+
							rdr["MRP"].ToString()+"\t"+
							GetSalesRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString())+"\t"+
							GetPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString())+"\t"+
							GetDiff(GetPurRate(rdr["Prod_ID"].ToString(),rdr["Pur_Rate"].ToString()),GetSalesRate(rdr["Prod_ID"].ToString(),rdr["Sal_Rate"].ToString()))+"\t"+
							GetEffDate(rdr["Prod_ID"].ToString(),rdr["eff_date"].ToString())
							);

					}
				}
			}
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Prepares the excel report file PriceCalculation.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridSalesSummarized.Visible==true || GridPurchaseSummarized.Visible==true || GridSummarizedSummarized.Visible==true || gridCurrPrice.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Price Calculation Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Price Calculation Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
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
				string strProdCode="",strProdWithPack="",strProdName="",strProdType="",strPackType="",strPackUnit="";
				strProdCode = "select distinct Prod_Code from Products";
				strProdWithPack = "select distinct Prod_Name+':'+Pack_Type as Prod_Name from Products order by Prod_Name";
				strProdName = "select distinct Prod_Name from Products order by Prod_Name";
				strProdType = "select distinct Category from Products order by Category";
				strPackType = "select distinct Pack_Type from Products order by Pack_Type";
				strPackUnit = "select distinct Pack_Unit from Products order by Pack_unit";
				string[] arrStr = {strProdWithPack,strProdName,strProdType,strProdCode,strPackType,strPackUnit};
				HtmlInputHidden[] arrCust = {tempProdWithPack,tempProdName,tempProdType,tempProdCode,tempPackType,tempPackUnit};
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
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()   Price List Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
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
					rdr = obj.GetRecordSet("select top 1 * from price_updation where prod_id=(select Prod_ID from Products where prod_name='"+e.Item.Cells[1].Text+"' and Pack_Type='"+e.Item.Cells[2].Text+"') order by eff_date desc");
					if(rdr.Read())
					{
						e.Item.Cells[4].Text=GenUtil.strNumericFormat(rdr["Sal_Rate"].ToString());
						e.Item.Cells[6].Text=GenUtil.strNumericFormat(System.Convert.ToString(double.Parse(rdr["Sal_Rate"].ToString())-double.Parse(rdr["Pur_Rate"].ToString())));
						e.Item.Cells[5].Text=GenUtil.strNumericFormat(rdr["Pur_Rate"].ToString());
						e.Item.Cells[7].Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Eff_Date"].ToString()));
					}
					else
					{
						e.Item.Cells[4].Text="";
						e.Item.Cells[6].Text="";
						e.Item.Cells[5].Text="";
						e.Item.Cells[7].Text="";
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PriceCalculation.aspx,Method:ItemDataBound()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}
	}
}