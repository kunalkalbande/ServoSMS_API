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
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Text;
using RMG;
using DBOperations;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for districtWiseReport.
	/// </summary>
	public partial class districtWiseReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ArrayList arrType = new ArrayList();
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				try
				{
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="20";
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
					CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Method:Page_Load,  User: "+uid);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				}
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
		


		//declaration
		int k=0;//,j=0,i=0,l=0,m=0;
		//int i1=0,j1=0,k1=0,l1=0,m1=0;
		//int i2=0,j2=0,k2=0,l2=0,m2=0;
		//int i3=0,j3=0,k3=0,l3=0,m3=0;
		//int i4=0,j4=0,k4=0,l4=0,m4=0;
		//int i5=0,j5=0,k5=0,l5=0,m5=0;
		//int i6=0,j6=0,k6=0,l6=0,m6=0;
		//int i7=0,j7=0,k7=0,l7=0,m7=0;
		//int i8=0,j8=0,k8=0,l8=0,m8=0;
		/// <summary>
		/// This is used to sorting the datagrid on click of datagridheader
		/// </summary>
		string strorderbyAshok="";
		public void sortcommand_clickAshok(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnAshok"]))
				{
					if(Session["orderAshok"].Equals("ASC"))
					{
						strorderbyAshok=e.SortExpression.ToString()+" DESC";
						Session["orderAshok"]="DESC";
					}
					else
					{
						strorderbyAshok=e.SortExpression.ToString()+" ASC";
						Session["orderAshok"]="ASC";
					}
				}
				else
				{
					strorderbyAshok=e.SortExpression.ToString()+" ASC";
					Session["orderAshok"]="ASC";
				}
				Session["columnAshok"]=e.SortExpression.ToString();
				BindthedataAshok();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbyBhind="";
		public void sortcommand_clickBhind(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnBhind"]))
				{
					if(Session["orderBhind"].Equals("ASC"))
					{
						strorderbyBhind=e.SortExpression.ToString()+" DESC";
						Session["orderBhind"]="DESC";
					}
					else
					{
						strorderbyBhind=e.SortExpression.ToString()+" ASC";
						Session["orderBhind"]="ASC";
					}
				}
				else
				{
					strorderbyBhind=e.SortExpression.ToString()+" ASC";
					Session["orderBhind"]="ASC";
				}
				Session["columnBhind"]=e.SortExpression.ToString();
				BindthedataBhind();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbyDatia="";
		public void sortcommand_clickDatia(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnDatia"]))
				{
					if(Session["orderDatia"].Equals("ASC"))
					{
						strorderbyDatia=e.SortExpression.ToString()+" DESC";
						Session["orderDatia"]="DESC";
					}
					else
					{
						strorderbyDatia=e.SortExpression.ToString()+" ASC";
						Session["orderDatia"]="ASC";
					}
				}
				else
				{
					strorderbyDatia=e.SortExpression.ToString()+" ASC";
					Session["orderDatia"]="ASC";
				}
				Session["columnDatia"]=e.SortExpression.ToString();
				BindthedataDatia();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbyGuna="";
		public void sortcommand_clickGuna(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnGuna"]))
				{
					if(Session["orderGuna"].Equals("ASC"))
					{
						strorderbyGuna=e.SortExpression.ToString()+" DESC";
						Session["orderGuna"]="DESC";
					}
					else
					{
						strorderbyGuna=e.SortExpression.ToString()+" ASC";
						Session["orderGuna"]="ASC";
					}
				}
				else
				{
					strorderbyGuna=e.SortExpression.ToString()+" ASC";
					Session["orderGuna"]="ASC";
				}
				Session["columnGuna"]=e.SortExpression.ToString();
				BindthedataGuna();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbyGwalior="";
		public void sortcommand_clickGwalior(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnGwalior"]))
				{
					if(Session["orderGwalior"].Equals("ASC"))
					{
						strorderbyGwalior=e.SortExpression.ToString()+" DESC";
						Session["orderGwalior"]="DESC";
					}
					else
					{
						strorderbyGwalior=e.SortExpression.ToString()+" ASC";
						Session["orderGwalior"]="ASC";
					}
				}
				else
				{
					strorderbyGwalior=e.SortExpression.ToString()+" ASC";
					Session["orderGwalior"]="ASC";
				}
				Session["columnGwalior"]=e.SortExpression.ToString();
				BindthedataGwalior();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbyMorena="";
		public void sortcommand_clickMorena(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnMorena"]))
				{
					if(Session["orderMorena"].Equals("ASC"))
					{
						strorderbyMorena=e.SortExpression.ToString()+" DESC";
						Session["orderMorena"]="DESC";
					}
					else
					{
						strorderbyMorena=e.SortExpression.ToString()+" ASC";
						Session["orderMorena"]="ASC";
					}
				}
				else
				{
					strorderbyMorena=e.SortExpression.ToString()+" ASC";
					Session["orderMorena"]="ASC";
				}
				Session["columnMorena"]=e.SortExpression.ToString();
				BindthedataMorena();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbySheopur="";
		public void sortcommand_clickSheopur(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnSheopur"]))
				{
					if(Session["orderSheopur"].Equals("ASC"))
					{
						strorderbySheopur=e.SortExpression.ToString()+" DESC";
						Session["orderSheopur"]="DESC";
					}
					else
					{
						strorderbySheopur=e.SortExpression.ToString()+" ASC";
						Session["orderSheopur"]="ASC";
					}
				}
				else
				{
					strorderbySheopur=e.SortExpression.ToString()+" ASC";
					Session["orderSheopur"]="ASC";
				}
				Session["columnSheopur"]=e.SortExpression.ToString();
				BindthedataSheopur();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to sorting on click of datagridheader
		/// </summary>
		string strorderbyShivpuri="";
		public void sortcommand_clickShivpuri(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["ColumnShivpuri"]))
				{
					if(Session["orderShivpuri"].Equals("ASC"))
					{
						strorderbyShivpuri=e.SortExpression.ToString()+" DESC";
						Session["orderShivpuri"]="DESC";
					}
					else
					{
						strorderbyShivpuri=e.SortExpression.ToString()+" ASC";
						Session["orderShivpuri"]="ASC";
					}
				}
				else
				{
					strorderbyShivpuri=e.SortExpression.ToString()+" ASC";
					Session["orderShivpuri"]="ASC";
				}
				Session["columnShivpuri"]=e.SortExpression.ToString();
				BindthedataShivpuri();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataAshok()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyAshok;
			//			Cache["strorderbyAshok"]=strorderbyAshok;
			//			DataGrid1.DataSource=dv;
			//			DataGrid1.DataBind();
			//		    sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataGwalior()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyGwalior;
			//			Cache["strorderbyGwalior"]=strorderbyGwalior;
			//			Datagrid5.DataSource=dv;
			//			Datagrid5.DataBind();
			//			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataBhind()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyBhind;
			//			Cache["strorderbyBhind"]=strorderbyBhind;
			//			Datagrid2.DataSource=dv;
			//			Datagrid2.DataBind();
			//			sqlcon.Dispose();
		}
		
		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataDatia()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyDatia;
			//			Cache["strorderbyDatia"]=strorderbyDatia;
			//			Datagrid3.DataSource=dv;
			//			Datagrid3.DataBind();
			//			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataGuna()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyGuna;
			//			Cache["strorderbyGuna"]=strorderbyGuna;
			//			Datagrid4.DataSource=dv;
			//			Datagrid4.DataBind();
			//			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataMorena()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyMorena;
			//			Cache["strorderbyMorena"]=strorderbyMorena;
			//			Datagrid6.DataSource=dv;
			//			Datagrid6.DataBind();
			//			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataShivpuri()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbyShivpuri;
			//			Cache["strorderbyShivpuri"]=strorderbyShivpuri;
			//			Datagrid8.DataSource=dv;
			//			Datagrid8.DataBind();
			//			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataSheopur()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'";
			//			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			//			DataSet ds=new DataSet();	
			//			da.Fill(ds,"customer");
			//			DataTable dtcustomer=ds.Tables["customer"]; 
			//			DataView dv=new DataView(dtcustomer);
			//			dv.Sort=strorderbySheopur;
			//			Cache["strorderbySheopur"]=strorderbySheopur;
			//			Datagrid7.DataSource=dv;
			//			Datagrid7.DataBind();
			//			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrids according to cities.
		/// </summary>
		public void BindtheData()
		{
			/*
			Panel1.Visible=true;
			Panel5.Visible=true;
			DataGrid1.Visible=true;
			Datagrid5.Visible=true;
			Datagrid2.Visible=true;
			Panel2.Visible=true;
			Datagrid3.Visible=true;
			Panel3.Visible=true;
			Datagrid4.Visible=true;
			Panel4.Visible=true;
			Datagrid6.Visible=true;
			Panel6.Visible=true;
			Datagrid7.Visible=true;
			Panel7.Visible=true;
			Datagrid8.Visible=true;
			Panel8.Visible=true;
			Panel9.Visible=true;
			SqlConnection con;
			con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			con.Open ();
			//SqlDataReader dtr;
//****		BindthedataAshok();
//							SqlCommand cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar'",con);
//							dtr=cmd.ExecuteReader();
//							DataGrid1.DataSource=dtr;
//							DataGrid1.DataBind();
//							dtr.Close();
			
			SqlDataReader dtr1;
			con.Close();
			con.Open();
			SqlCommand cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k++;
					//****com*****				if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//****com*****					l++;
					//******add*******			//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString()=="Oe(others)")
													//******add*******				//l++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m++;
				}
			}
			else
			{
				Panel1.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtanbazzar.Text=System.Convert.ToString(i);
			txtanro.Text=System.Convert.ToString(k);
			txtanoe.Text=System.Convert.ToString(l);
			txtanfleet.Text=System.Convert.ToString(m);
			txtanibp.Text=System.Convert.ToString(j);
			//****************************************************************************************
				
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid5.DataSource=dtr;
//			Datagrid5.DataBind();
//			dtr.Close();
//          con.Close();
//****		BindthedataGwalior();
			
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i5++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j5++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k5++;
					//*****com********		if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//******com*******			l5++;
					//******add*******		//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString()=="Oe(others)")
												//******add*******			//l5++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l5++;
					
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m5++;
				}
			}
			else
			{
				Panel5.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtgwlbazzar.Text=System.Convert.ToString(i5);
			txtgwlro.Text=System.Convert.ToString(k5);
			txtgwloe.Text=System.Convert.ToString(l5);
			txtgwlfleet.Text=System.Convert.ToString(m5);
			txtgwlibp.Text=System.Convert.ToString(j5);
			//************************
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid2.DataSource=dtr;
//			Datagrid2.DataBind();
//			dtr.Close();
//			con.Close();
//****			BindthedataBhind();
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i2++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j2++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k2++;
					//****com***			if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//****com****				l2++;
					//*****add*********	//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString()=="Oe(others)")
											//*****add*********		//l2++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l2++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m2++;
				}
			}
			else
			{
				Panel2.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtbhbazzar.Text=System.Convert.ToString(i2);
			txtbhro.Text=System.Convert.ToString(k2);
			txtbhoe.Text=System.Convert.ToString(l2);
			txtbhfleet.Text=System.Convert.ToString(m2);
			txtbhibp.Text=System.Convert.ToString(j2);
			//**************************
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid3.DataSource=dtr;
//			Datagrid3.DataBind();
//			dtr.Close();
//			con.Close();
//****			BindthedataDatia();
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i3++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j3++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k3++;
					//***com****			if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//***com*****				l3++;
					//*****add*******		//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString()=="Oe(others)")
												//****add*******			//l3++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l3++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m3++;
				}
			}
			else
			{
				Panel3.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtdtbazzar.Text=System.Convert.ToString(i3);
			txtdtro.Text=System.Convert.ToString(k3);
			txtdtoe.Text=System.Convert.ToString(l3);
			txtdtfleet.Text=System.Convert.ToString(m3);
			txtdtibp.Text=System.Convert.ToString(j3);
			//********************************
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid4.DataSource=dtr;
//			Datagrid4.DataBind();
//			dtr.Close();
//			con.Close();
//****			BindthedataGuna();
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i4++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j4++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k4++;
					//****com*****			if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//****com****				l4++;
					//*******add*****		//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString() =="Oe(others)")
												//*******add****			//l4++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l4++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m4++;
				}
			}
			else
			{
				Panel4	.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtgnbazzar.Text=System.Convert.ToString(i4);
			txtgnro.Text=System.Convert.ToString(k4);
			txtgnoe.Text=System.Convert.ToString(l4);
			txtgnfleet.Text=System.Convert.ToString(m4);
			txtgnibp.Text=System.Convert.ToString(j4);
			//************************************
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid6.DataSource=dtr;
//			Datagrid6.DataBind();
//			dtr.Close();
//			con.Close();
//****			BindthedataMorena();
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i6++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j6++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k6++;
					//***com*****			if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//**com******				l6++;
					//***add****			//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString()=="Oe(others)")
												//***add***				//l6++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l6++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m6++;
				}
			}
			else
			{
				Panel6.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtmobazzar.Text=System.Convert.ToString(i6);
			txtmoro.Text=System.Convert.ToString(k6);
			txtmooe.Text=System.Convert.ToString(l6);
			txtmofleet.Text=System.Convert.ToString(m6);
			txtmoibp.Text=System.Convert.ToString(j6);
			//************************************
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid7.DataSource=dtr;
//			Datagrid7.DataBind();
//			dtr.Close();
//			con.Close();
//****			BindthedataSheopur();
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i7++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j7++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k7++;
					//***com*****			if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//***com*****				l7++;
					//****add******		//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString() =="Oe(others)")
											//*****add*********		//l7++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l7++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m7++;
				}
			}
			else
			{
				Panel7.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtspbazzar.Text=System.Convert.ToString(i7);
			txtspro.Text=System.Convert.ToString(k7);
			txtspoe.Text=System.Convert.ToString(l7);
			txtspfleet.Text=System.Convert.ToString(m7);
			txtspibp.Text=System.Convert.ToString(j7);
			//************************************
//			con.Open();
//			cmd=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'",con);
//			dtr=cmd.ExecuteReader();
//			Datagrid8.DataSource=dtr;
//			Datagrid8.DataBind();
//			dtr.Close();
//			con.Close();
//****			BindthedataShivpuri();
			con.Open();
			cmd1=new SqlCommand("select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'",con);
			dtr1=cmd1.ExecuteReader();
			if(dtr1.HasRows)
			{
				while(dtr1.Read())
				{
					if(dtr1.GetValue(3).ToString()=="Bazzar")
						i8++;
					if(dtr1.GetValue(3).ToString()=="Ibp")
						j8++;
					//if(dtr1.GetValue(3).ToString()=="Ro-1" || dtr1.GetValue(3).ToString()=="Ro-2" || dtr1.GetValue(3).ToString()=="Ro-3")
					if(dtr1.GetValue(3).ToString().StartsWith("Ro"))
						k8++;
					//****com******		if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)")
					//****com******			l8++;
					//*****add******		//if(dtr1.GetValue(3).ToString()=="Oe(hcv)" || dtr1.GetValue(3).ToString()=="Oe(muv)" || dtr1.GetValue(3).ToString()=="Oe(lcv)" || dtr1.GetValue(3).ToString()=="Oe(tractor)" || dtr1.GetValue(3).ToString()=="Oe(maruti)" || dtr1.GetValue(3).ToString()=="Oe(hyundai)" || dtr1.GetValue(3).ToString()=="Oe(force)" || dtr1.GetValue(3).ToString()=="Oe(eicher)" || dtr1.GetValue(3).ToString()=="Oe(garage)"|| dtr1.GetValue(3).ToString()=="Oe(others)")
												//******add******			//l8++;
					if(dtr1.GetValue(3).ToString().StartsWith("Oe"))
						l8++;
					if(dtr1.GetValue(3).ToString()=="Fleet")
						m8++;
				}
			}
			else
			{
				Panel8.Visible=false;
			}
			dtr1.Close();
			con.Close();
			txtsvbazzar.Text=System.Convert.ToString(i8);
			txtsvro.Text=System.Convert.ToString(k8);
			txtsvoe.Text=System.Convert.ToString(l8);
			txtsvfleet.Text=System.Convert.ToString(m8);
			txtsvibp.Text=System.Convert.ToString(j8);
			//************************************
			txtbazzar.Text=System.Convert.ToString(System.Convert.ToInt32(txtanbazzar.Text)+
				System.Convert.ToInt32(txtbhbazzar.Text)+
				System.Convert.ToInt32(txtdtbazzar.Text)+
				System.Convert.ToInt32(txtgnbazzar.Text)+
				System.Convert.ToInt32(txtgwlbazzar.Text)+
				System.Convert.ToInt32(txtmobazzar.Text)+
				System.Convert.ToInt32(txtspbazzar.Text)+
				System.Convert.ToInt32(txtsvbazzar.Text));
			txtro.Text=System.Convert.ToString(System.Convert.ToInt32(txtanro.Text)+
				System.Convert.ToInt32(txtbhro.Text)+
				System.Convert.ToInt32(txtdtro.Text)+
				System.Convert.ToInt32(txtgnro.Text)+
				System.Convert.ToInt32(txtgwlro.Text)+
				System.Convert.ToInt32(txtmoro.Text)+
				System.Convert.ToInt32(txtspro.Text)+
				System.Convert.ToInt32(txtsvro.Text));
			txtoe.Text=System.Convert.ToString(System.Convert.ToInt32(txtanoe.Text)+
				System.Convert.ToInt32(txtbhoe.Text)+
				System.Convert.ToInt32(txtdtoe.Text)+
				System.Convert.ToInt32(txtgnoe.Text)+
				System.Convert.ToInt32(txtgwloe.Text)+
				System.Convert.ToInt32(txtmooe.Text)+
				System.Convert.ToInt32(txtspoe.Text)+
				System.Convert.ToInt32(txtsvoe.Text));
			txtfleet.Text=System.Convert.ToString(System.Convert.ToInt32(txtanfleet.Text)+
				System.Convert.ToInt32(txtbhfleet.Text)+
				System.Convert.ToInt32(txtdtfleet.Text)+
				System.Convert.ToInt32(txtgnfleet.Text)+
				System.Convert.ToInt32(txtgwlfleet.Text)+
				System.Convert.ToInt32(txtmofleet.Text)+
				System.Convert.ToInt32(txtspfleet.Text)+
				System.Convert.ToInt32(txtsvfleet.Text));
			txtibp.Text=System.Convert.ToString(System.Convert.ToInt32(txtanibp.Text)+
				System.Convert.ToInt32(txtbhibp.Text)+
				System.Convert.ToInt32(txtdtibp.Text)+
				System.Convert.ToInt32(txtgnibp.Text)+
				System.Convert.ToInt32(txtgwlibp.Text)+
				System.Convert.ToInt32(txtmoibp.Text)+
				System.Convert.ToInt32(txtspibp.Text)+
				System.Convert.ToInt32(txtsvibp.Text));
			*/
		}

		/// <summary>
		/// This is to  view report of all cities.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnview_Click(object sender, System.EventArgs e)
		{
			/*
			try
			{
				BindtheData();
				strorderbyAshok="r4 ASC";
				Session["ColumnAshok"]="r4";
				Session["orderAshok"]="ASC";
				BindthedataAshok();

				strorderbyBhind="r4 ASC";
				Session["ColumnBhind"]="r4";
				Session["orderBhind"]="ASC";
				BindthedataBhind();

				strorderbyDatia="r4 ASC";
				Session["ColumnDatia"]="r4";
				Session["orderDatia"]="ASC";
				BindthedataDatia();

				strorderbyGuna="r4 ASC";
				Session["ColumnGuna"]="r4";
				Session["orderGuna"]="ASC";
				BindthedataGuna();

				strorderbyGwalior="r4 ASC";
				Session["ColumnGwalior"]="r4";
				Session["orderGwalior"]="ASC";
				BindthedataGwalior();

				strorderbyMorena="r4 ASC";
				Session["ColumnMorena"]="r4";
				Session["orderMorena"]="ASC";
				BindthedataMorena();

				strorderbySheopur="r4 ASC";
				Session["ColumnSheopur"]="r4";
				Session["orderSheopur"]="ASC";
				BindthedataSheopur();

				strorderbyShivpuri="r4 ASC";
				Session["ColumnShivpuri"]="r4";
				Session["orderShivpuri"]="ASC";
				BindthedataShivpuri();
				//************************************
				CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Method:btnView, userid : "+uid );
			}

			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
			*/
		}
		/// <summary>
		/// This function is used to make the report.
		/// </summary>
		public void makingReport()
		{
			/*
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="";
			string info= "";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\DistrictWiseReport.txt";
			StreamWriter sw = new StreamWriter(path);		
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar'";
			sql=sql+" order by "+Cache["strorderbyAshok"];
			dbobj.SelectQuery(sql, ref rdr);
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
			  string des="--------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=========================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("DISTRICTWISE REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=========================",des.Length));
			// info : to set string format.
			info = " {0,-20:S} {1,-20:S} {2,-20:S} {3,-15:S}";
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Ashok Nagar");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtanbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtanro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtanoe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtanfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtanibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'";
			sql=sql+" order by "+Cache["strorderbyBhind"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Bhind");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtbhbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtbhro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtbhoe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtbhfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtbhibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'";
			sql=sql+" order by "+Cache["strorderbyDatia"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Datia");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtdtbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtdtro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtdtoe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtdtfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtdtibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'";
			sql=sql+" order by "+Cache["strorderbyGuna"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Guna");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtgnbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtgnro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtgnoe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtgnfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtgnibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'";
			sql=sql+" order by "+Cache["strorderbyGwalior"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Gwalior");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtgwlbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtgwlro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtgwloe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtgwlfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtgwlibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'";
			sql=sql+" order by "+Cache["strorderbyMorena"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Morena");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtmobazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtmoro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtmooe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtmofleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtmoibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'";
			sql=sql+" order by "+Cache["strorderbySheopur"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Sheopur");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtspbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtspro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtspoe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtspfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtspibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'";
			sql=sql+" order by "+Cache["strorderbyShivpuri"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Shivpuri");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  District          |  Firm Name         |  Place             |  Category     |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["r1"].ToString().Trim(),20),						
						GenUtil.TrimLength(rdr["r2"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r3"].ToString().Trim(),20),
						GenUtil.TrimLength(rdr["r4"].ToString().Trim(),15));
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("                       Total Bazzar       : "+txtsvbazzar.Text.Trim());
				sw.WriteLine("                       Total Ro           : "+txtsvro.Text.Trim());
				sw.WriteLine("                       Total Oe           : "+txtsvoe.Text.Trim());
				sw.WriteLine("                       Total Fleet        : "+txtsvfleet.Text.Trim());
				sw.WriteLine("                       Total Ibp          : "+txtsvibp.Text.Trim());
				sw.WriteLine("  ");
			}
			dbobj.Dispose();
			sw.WriteLine("+------------+-----------+-----+-----+----+-------+------+-------+--------+-----+");
			sw.WriteLine("             |Ashok Nagar|Bhind|Datia|Guna|Gwalior|Morena|Sheopur|Shivpuri|Total|");
			sw.WriteLine("+------------+-----------+-----+-----+----+-------+------+-------+--------+-----+");
			info = " {0,-12:S} {1,11:S} {2,5:S} {3,5:S} {4,4:S} {5,7:S} {6,6:S} {7,7:S} {8,8:S} {9,5:S}";
			sw.WriteLine(info,"Total Bazzar",txtanbazzar.Text.Trim(),txtbhbazzar.Text.Trim(),txtdtbazzar.Text.Trim(),txtgnbazzar.Text.Trim(),txtgwlbazzar.Text.Trim(),txtmobazzar.Text.Trim(),txtspbazzar.Text.Trim(),txtsvbazzar.Text.Trim(),txtbazzar.Text.Trim());
			sw.WriteLine(info,"Total Ro",txtanro.Text.Trim(),txtbhro.Text.Trim(),txtdtro.Text.Trim(),txtgnro.Text.Trim(),txtgwlro.Text.Trim(),txtmoro.Text.Trim(),txtspro.Text.Trim(),txtsvro.Text.Trim(),txtro.Text.Trim());
			sw.WriteLine(info,"Total Oe",txtanoe.Text.Trim(),txtbhoe.Text.Trim(),txtdtoe.Text.Trim(),txtgnoe.Text.Trim(),txtgwloe.Text.Trim(),txtmooe.Text.Trim(),txtspoe.Text.Trim(),txtsvoe.Text.Trim(),txtoe.Text.Trim());
			sw.WriteLine(info,"Total Fleet",txtanfleet.Text.Trim(),txtbhfleet.Text.Trim(),txtdtfleet.Text.Trim(),txtgnfleet.Text.Trim(),txtgwlfleet.Text.Trim(),txtmofleet.Text.Trim(),txtspfleet.Text.Trim(),txtsvfleet.Text.Trim(),txtfleet.Text.Trim());
			sw.WriteLine(info,"Total Ibp",txtanibp.Text.Trim(),txtbhibp.Text.Trim(),txtdtibp.Text.Trim(),txtgnibp.Text.Trim(),txtgwlibp.Text.Trim(),txtmoibp.Text.Trim(),txtspibp.Text.Trim(),txtsvibp.Text.Trim(),txtibp.Text.Trim());
			sw.WriteLine("+------------+-----------+-----+-----+----+-------+------+-------+--------+-----+");
			sw.Close();
		*/
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			/*
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\DistrictWiseReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar'";
			sql=sql+" order by "+Cache["strorderbyAshok"];
			dbobj.SelectQuery(sql, ref rdr);
			
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Ashok Nagar");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtanbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtanro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtanoe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtanfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtanibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'";
			sql=sql+" order by "+Cache["strorderbyBhind"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Bhind");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtbhbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtbhro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtbhoe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtbhfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtbhibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'";
			sql=sql+" order by "+Cache["strorderbyDatia"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Datia");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtdtbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtdtro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtdtoe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtdtfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtdtibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'";
			sql=sql+" order by "+Cache["strorderbyGuna"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Guna");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtgnbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtgnro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtgnoe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtgnfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtgnibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'";
			sql=sql+" order by "+Cache["strorderbyGwalior"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Gwalior");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory\t");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtgwlbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtgwlro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtgwloe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtgwlfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtgwlibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'";
			sql=sql+" order by "+Cache["strorderbyMorena"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Morena");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtmobazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtmoro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtmooe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtmofleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtmoibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'";
			sql=sql+" order by "+Cache["strorderbySheopur"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Sheopur");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtspbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtspro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtspoe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtspfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtspibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'";
			sql=sql+" order by "+Cache["strorderbyShivpuri"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Shivpuri");
				sw.WriteLine("District\tFirm Name\tPlace\tCategory");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r1"].ToString().Trim()+"\t"+
						rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim());
				}
				sw.WriteLine("Total Bazzar\t"+txtsvbazzar.Text.Trim());
				sw.WriteLine("Total Ro\t"+txtsvro.Text.Trim());
				sw.WriteLine("Total Oe\t"+txtsvoe.Text.Trim());
				sw.WriteLine("Total Fleet\t"+txtsvfleet.Text.Trim());
				sw.WriteLine("Total Ibp\t"+txtsvibp.Text.Trim());
				sw.WriteLine();
			}
			dbobj.Dispose();
			sw.WriteLine("\tAshok Nagar\tBhind\tDatia\tGuna\tGwalior\tMorena\tSheopur\tShivpuri\tTotal");
			sw.WriteLine("Total Bazzar\t"+txtanbazzar.Text.Trim()+"\t"+txtbhbazzar.Text.Trim()+"\t"+txtdtbazzar.Text.Trim()+"\t"+txtgnbazzar.Text.Trim()+"\t"+txtgwlbazzar.Text.Trim()+"\t"+txtmobazzar.Text.Trim()+"\t"+txtspbazzar.Text.Trim()+"\t"+txtsvbazzar.Text.Trim()+"\t"+txtbazzar.Text.Trim());
			sw.WriteLine("Total Ro\t"+txtanro.Text.Trim()+"\t"+txtbhro.Text.Trim()+"\t"+txtdtro.Text.Trim()+"\t"+txtgnro.Text.Trim()+"\t"+txtgwlro.Text.Trim()+"\t"+txtmoro.Text.Trim()+"\t"+txtspro.Text.Trim()+"\t"+txtsvro.Text.Trim()+"\t"+txtro.Text.Trim());
			sw.WriteLine("Total Oe\t"+txtanoe.Text.Trim()+"\t"+txtbhoe.Text.Trim()+"\t"+txtdtoe.Text.Trim()+"\t"+txtgnoe.Text.Trim()+"\t"+txtgwloe.Text.Trim()+"\t"+txtmooe.Text.Trim()+"\t"+txtspoe.Text.Trim()+"\t"+txtsvoe.Text.Trim()+"\t"+txtoe.Text.Trim());
			sw.WriteLine("Total Fleet\t"+txtanfleet.Text.Trim()+"\t"+txtbhfleet.Text.Trim()+"\t"+txtdtfleet.Text.Trim()+"\t"+txtgnfleet.Text.Trim()+"\t"+txtgwlfleet.Text.Trim()+"\t"+txtmofleet.Text.Trim()+"\t"+txtspfleet.Text.Trim()+"\t"+txtsvfleet.Text.Trim()+"\t"+txtfleet.Text.Trim());
			sw.WriteLine("Total Ibp\t"+txtanibp.Text.Trim()+"\t"+txtbhibp.Text.Trim()+"\t"+txtdtibp.Text.Trim()+"\t"+txtgnibp.Text.Trim()+"\t"+txtgwlibp.Text.Trim()+"\t"+txtmoibp.Text.Trim()+"\t"+txtspibp.Text.Trim()+"\t"+txtsvibp.Text.Trim()+"\t"+txtibp.Text.Trim());
			sw.Close();
			*/
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport1()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\DistrictWiseReport.txt";
			StreamWriter sw = new StreamWriter(path);
			string info = "";
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
			string des="----------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//******S***
			string desdes="+--------------------+-----------------------------------+--------------------+--------------------+";
			string Name="|      District      |             Firm Name             |        Place       |      Category      |";
			sw.WriteLine(GenUtil.GetCenterAddr("----------------------",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("District Wise Report",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("----------------------",des.Length));
			info = " {0,-20:S} {1,-35:F} {2,-20:S} {3,-20:S}";
			DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr=null,rdr1=null,SqlDtr=null;
			dbobj.SelectQuery("select distinct state from customer order by state",ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					dbobj1.SelectQuery("select * from customer where state='"+rdr["State"].ToString()+"' order by cust_type",ref SqlDtr);
					if(SqlDtr.HasRows)
					{
						sw.WriteLine("District : "+rdr["State"].ToString());
						sw.WriteLine(desdes);
						sw.WriteLine(Name);
						sw.WriteLine(desdes);
						while(SqlDtr.Read())
						{
							sw.WriteLine(info,SqlDtr["State"].ToString(),SqlDtr["Cust_Name"].ToString(),SqlDtr["City"].ToString(),SqlDtr["Cust_Type"].ToString());
						}
						dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' group by cust_type)",ref rdr1);
						if(rdr1.HasRows)
						{
							sw.WriteLine(desdes);
							while(rdr1.Read())
							{
								sw.WriteLine(info,"","Total "+rdr1.GetValue(0).ToString(),rdr1.GetValue(1).ToString(),"");
							}
							sw.WriteLine(desdes);
						}
						rdr1.Close();
					}
					SqlDtr.Close();
				}
			}
			else
			{
				MessageBox.Show("Data Not Available");
				return;
			}
			ArrayList arrType = new ArrayList();
			ArrayList arrState = new ArrayList();
			dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
			string des1="",Name1="";
			if(rdr1.HasRows)
			{
				des1="+--------------+";
				Name1="|Type / Distict|";
				while(rdr1.Read())
				{
					arrState.Add(rdr1.GetValue(0).ToString());
					Name1+=rdr1["State"].ToString()+"|";
					for(int i=0;i<rdr1["State"].ToString().Length;i++)
					{
						des1+="-";
					}
					des1+="+";
				}
				des1+="-----+";
				Name1+="Total|";
			}
			rdr1.Close();
			sw.WriteLine(des1);
			sw.WriteLine(Name1);
			sw.WriteLine(des1);
			
			double[] Counter=new double[104];    //Add by vikas 07.08.09
			int count=0;                         //Add by vikas 07.08.09

			double Total=0;
			dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
			while(rdr1.Read())
			{
				arrType.Add(rdr1.GetValue(0).ToString());
			}
			rdr1.Close();
			for(int i=0;i<arrType.Count;i++)
			{
				Total=0;
				string str="|Total "+arrType[i].ToString();
				sw.Write("|Total "+arrType[i].ToString());
				for(k=str.ToString().Length;k<15;k++)
				{
					sw.Write(" ");
				}
				for(int j=0;j<arrState.Count;j++)
				{
					dbobj2.SelectQuery("select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"'",ref rdr1);
					if(rdr1.Read())
					{
						sw.Write("|"+rdr1.GetValue(0).ToString());
						for(k=rdr1.GetValue(0).ToString().Length;k<arrState[j].ToString().Length;k++)
						{
							sw.Write(" ");
						}
						Total+=double.Parse(rdr1.GetValue(0).ToString());
						
						Counter[count++]=double.Parse(rdr1.GetValue(0).ToString()); //Coment by Vikas 07.08.09
					}
				}
				sw.Write("|"+Total.ToString());
				for(k=Total.ToString().Length;k<5;k++)
				{
					sw.Write(" ");
				}
				sw.Write("|");
				sw.WriteLine();

				
							
			}
			sw.WriteLine(des1);

			double[] Tot_Counter=new double[8];
			double Tot_=0;
			for(int i=0;i<104;)
			{
				Tot_+=Counter[i];
				Tot_Counter[0]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[1]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[2]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[3]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[4]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[5]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[6]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[7]+=Counter[i++];
			}	

			string s="+--------------+----------+-----+-----+----+-------+------+-------+--------+-----+";
			//         01234567890123 0123456789 01234 01234 0123 0123456 012345 0123456 01234567 01234 
			string info1 = " {0,-14:S} {1,-10:S} {2,-5:S} {3,-5:S} {4,-4:S} {5,-7:S} {6,-6:S} {7,-7:S} {8,-8:S} {9,-5:S}";

			sw.WriteLine(info1,"ToTal",Tot_Counter[0].ToString(),Tot_Counter[1].ToString(),Tot_Counter[2].ToString(),Tot_Counter[3].ToString(),Tot_Counter[4].ToString(),Tot_Counter[5].ToString(),Tot_Counter[6].ToString(),Tot_Counter[7].ToString(),Tot_.ToString());
			sw.WriteLine(des1);
			sw.Close();
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel1()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\DistrictWiseReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string Name="District\tFirm Name\tPlace\tCategory";
			DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr=null,rdr1=null,SqlDtr=null;
			dbobj.SelectQuery("select distinct state from customer order by state",ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					dbobj1.SelectQuery("select * from customer where state='"+rdr["State"].ToString()+"' order by cust_type",ref SqlDtr);
					if(SqlDtr.HasRows)
					{
						sw.WriteLine("District : "+"\t"+rdr["State"].ToString());
						sw.WriteLine(Name);
						while(SqlDtr.Read())
						{
							sw.WriteLine(SqlDtr["State"].ToString()+"\t"+SqlDtr["Cust_Name"].ToString()+"\t"+SqlDtr["City"].ToString()+"\t"+SqlDtr["Cust_Type"].ToString());
						}
						dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' group by cust_type)",ref rdr1);
						if(rdr1.HasRows)
						{
							
							while(rdr1.Read())
							{
								sw.WriteLine("\tTotal "+rdr1.GetValue(0).ToString()+"\t"+rdr1.GetValue(1).ToString(),"");
							}
							
						}
						rdr1.Close();
					}
					SqlDtr.Close();
				}
			}
			else
			{
				MessageBox.Show("Data Not Available");
				return;
			}
			ArrayList arrType = new ArrayList();
			ArrayList arrState = new ArrayList();
			dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
			string Name1="";
			if(rdr1.HasRows)
			{
				Name1="Type / Distict\t";
				while(rdr1.Read())
				{
					arrState.Add(rdr1.GetValue(0).ToString());
					Name1+=rdr1["State"].ToString()+"\t";
				}
				Name1+="Total";
			}
			rdr1.Close();
			sw.WriteLine();
			sw.WriteLine(Name1);
			
			double[] Counter=new double[104];        //Add by vikas 07.08.09
			int count=0;                             //Add by vikas 07.08.09

			double Total=0;
			dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
			while(rdr1.Read())
			{
				arrType.Add(rdr1.GetValue(0).ToString());
			}
			rdr1.Close();
			for(int i=0;i<arrType.Count;i++)
			{
				Total=0;
				sw.Write("Total "+arrType[i].ToString());
				for(int j=0;j<arrState.Count;j++)
				{
					dbobj2.SelectQuery("select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"'",ref rdr1);
					if(rdr1.Read())
					{
						sw.Write("\t"+rdr1.GetValue(0).ToString());
						Total+=double.Parse(rdr1.GetValue(0).ToString());

						Counter[count++]=double.Parse(rdr1.GetValue(0).ToString()); //Coment by Vikas 07.08.09
					}
				}
				sw.Write("\t"+Total.ToString());
				sw.WriteLine();
			}
			double[] Tot_Counter=new double[8];
			double Tot_=0;
			for(int i=0;i<104;)
			{
				Tot_+=Counter[i];
				Tot_Counter[0]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[1]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[2]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[3]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[4]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[5]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[6]+=Counter[i++];
				Tot_+=Counter[i];
				Tot_Counter[7]+=Counter[i++];
			}	

			string s="+--------------+----------+-----+-----+----+-------+------+-------+--------+-----+";
			//         01234567890123 0123456789 01234 01234 0123 0123456 012345 0123456 01234567 01234 
			string info1 = " {0,-14:S} {1,-10:S} {2,-5:S} {3,-5:S} {4,-4:S} {5,-7:S} {6,-6:S} {7,-7:S} {8,-8:S} {9,-5:S}";

			sw.WriteLine("ToTal\t"+Tot_Counter[0].ToString()+"\t"+Tot_Counter[1].ToString()+"\t"+Tot_Counter[2].ToString()+"\t"+Tot_Counter[3].ToString()+"\t"+Tot_Counter[4].ToString()+"\t"+Tot_Counter[5].ToString()+"\t"+Tot_Counter[6].ToString()+"\t"+Tot_Counter[7].ToString()+"\t"+Tot_.ToString());
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file DistrictWiseReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
				makingReport1();
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\DistrictWiseReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:DistrictWiseReport.aspx,Method:print");
				} 
				catch (ArgumentNullException ane) 
				{
					//Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:DistrictWiseReport.aspx,Method:print"+ ane.Message+". User: "+uid);
				} 
				catch (SocketException se) 
				{
					///Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:DistrictWiseReport.aspx,Method:print"+ se.Message+". User: "+uid);
				} 
				catch (Exception es) 
				{
					//Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:DistrictWiseReport.aspx,Method:print"+ es.Message+". User: "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:DistrictWiseReport.aspx,Method:print"+ ex.Message+". User: "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file DistrictWiseReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ConvertToExcel1();
				MessageBox.Show("Successfully Convert File Into Excel Format");
				CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    districtWiseReport Convert Into Excel Format, userid  "+uid);
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:districtWiseReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    districtWiseReport Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}