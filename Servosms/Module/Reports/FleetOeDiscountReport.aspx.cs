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
using RMG;
using DBOperations;
using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for FleetOeDiscount.
	/// </summary>
	public partial class FleetOeDiscount : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		static string FromDate="",ToDate="";

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
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="14";
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
				Datagrid1.Visible=false;
				txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				#region Get FromDate and ToDate From Organisation
				InventoryClass obj1=new InventoryClass();
				SqlDataReader rdr1=null;
				rdr1=obj1.GetRecordSet("select * from organisation");
				if(rdr1.Read())
				{
					FromDate=GetYear(GenUtil.trimDate(rdr1["Acc_date_from"].ToString()));
					ToDate=GetYear(GenUtil.trimDate(rdr1["Acc_date_To"].ToString()));
				}
				else
				{
					MessageBox.Show("Please Fill The Organisation Details First");
					return;
				}
				#endregion
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            Textbox1.Text = Request.Form["Textbox1"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["Textbox1"].ToString().Trim();
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
		# region DateTime Function...
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}
		# endregion

		/// <summary>
		/// This is used to make sorting the datagrid on clicking of the datagridheader.
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
				CreateLogFiles.ErrorLog("Form:Fleet/OE discountReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to return the calculate discount with given qty and ltr.
		/// </summary>
		public string DisGiven(string ltr,string qty)
		{
			string Tot="";
			//Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)/System.Convert.ToDouble(qty));
			//comment by vikas sharma 21.05.09 Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty));
			Tot=System.Convert.ToString( Math.Round(((System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty))/100),2));
			return Tot;
		}

		double Dis=0;
		/// <summary>
		/// This method is used to return the calculate discount with given qty and ltr and also calculate the total discount.
		/// </summary>
		public string DisGiven1(string ltr,string qty)
		{
			string Tot="0";
			//Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)/System.Convert.ToDouble(qty));
			//Comment by vikas sharma 21.05.09 Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty));
			Tot=System.Convert.ToString(Math.Round((System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty)/100),2));
			Dis+=double.Parse(Tot);
			Cache["Dis"]=Dis.ToString();
			return Tot;
		}
		
		double Dis3=0;
		/// <summary>
		/// This method is used to return the calculate discount with given qty and ltr and also calculate the total discount.
		/// </summary>
		public string DisGiven3(string ltr,string qty,string prodval,string foetype)
		{
			string Tot="0";
           
			if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				Tot=System.Convert.ToString(Math.Round((System.Convert.ToDouble(ltr)*System.Convert.ToDouble(prodval)/100),2));
			else
				Tot=System.Convert.ToString(Math.Round(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty),2));
			
			Dis3+=double.Parse(Tot);
			Cache["Dis"]=Dis3.ToString();
			return Tot;
		}

		double Disfleet=0;
		/// <summary>
		/// This method is used to return the calculate discount with given qty and ltr and also calculate the total discount.
		/// </summary>
		public string DisGivenfleet(string ltr,string qty,string prodval,string foetype)
		{
			string Tot="0";
           
			if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				Tot=System.Convert.ToString(Math.Round((System.Convert.ToDouble(ltr)*System.Convert.ToDouble(prodval)/100),2));
			else
				Tot=System.Convert.ToString(Math.Round(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty),2));
			
			Disfleet+=double.Parse(Tot);
			Cache["Disfleet"]=Disfleet.ToString();
			return Tot;
		}

		double Disoe=0;
		/// <summary>
		/// This method is used to return the calculate discount with given qty and ltr and also calculate the total discount.
		/// </summary>
		public string DisGivenoe(string ltr,string qty,string prodval,string foetype)
		{
			string Tot="0";
           
			if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				Tot=System.Convert.ToString(Math.Round((System.Convert.ToDouble(ltr)*System.Convert.ToDouble(prodval)/100),2));
			else
				Tot=System.Convert.ToString(Math.Round(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty),2));
			
			Disoe+=double.Parse(Tot);
			Cache["Disoe"]=Disoe.ToString();
			return Tot;
		}


		double Dis1=0;
		/// <summary>
		/// This method is used to return the calculate discount with given qty and ltr and also calculate the total discount.
		/// </summary>
		public string DisGiven2(string ltr,string qty)
		{
			string Tot="";
			//Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)/System.Convert.ToDouble(qty));
			Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)*System.Convert.ToDouble(qty));
			Dis1+=double.Parse(Tot);
			Cache["Dis1"]=Dis1.ToString();
			return Tot;
		}
		
		/// <summary>
		/// This method is used to bind the datagrid with the help of query.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			//string sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and  (cust_type='Fleet' or cust_type like('Oe%'))";
			//Coment by vikas sharma 21.05.09 string sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cust_type='Fleet'";
			string sql= "select * from Vw_fleetoediscountReport where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103) and cust_type='Fleet'";
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			//coment by vikas 26.05.09 da.Fill(ds,"Vw_fleetoediscount");
			da.Fill(ds,"Vw_fleetoediscountReport");
			//coment by vikas 26.05.09 DataTable dtcustomer=ds.Tables["Vw_fleetoediscount"];
			DataTable dtcustomer=ds.Tables["Vw_fleetoediscountReport"];
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
				MessageBox.Show(" Fleet Data Not Available");
			}
			sqlcon.Dispose();
		}
		
		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of the datagridheader.
		/// </summary>
		string strorderby1="";
		public void sortcommand_click1(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["Column1"]))
				{
					if(Session["order1"].Equals("ASC"))
					{
						strorderby1=e.SortExpression.ToString()+" DESC";
						Session["order1"]="DESC";
					}
					else
					{
						strorderby1=e.SortExpression.ToString()+" ASC";
						Session["order1"]="ASC";
					}
				}
				else
				{
					strorderby1=e.SortExpression.ToString()+" ASC";
					Session["order1"]="ASC";
				}
				Session["column1"]=e.SortExpression.ToString();
				Bindthedata1();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Fleet/OE discountReport.aspx,Method:sortcommand_click1"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}
		
		/// <summary>
		/// this is used to bind the datagrid with the help of query.
		/// </summary>
		public void Bindthedata1()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			//string sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and  (cust_type='Fleet' or cust_type like('Oe%'))";
			//coment by vikas sharma 21.05.09 string sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cust_type like('Oe%')";
			string sql= "select * from Vw_fleetoediscountReport where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103) and cust_type like('Oe%')";
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			//coment by vikas 26.05.09 da.Fill(ds,"Vw_fleetoediscount");
			da.Fill(ds,"Vw_fleetoediscountReport");
			//coment by vikas 26.05.09 DataTable dtcustomer=ds.Tables["Vw_fleetoediscount"];
			DataTable dtcustomer=ds.Tables["Vw_fleetoediscountReport"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby1;
			Cache["strorderby1"]=strorderby1;
			Datagrid1.DataSource=dv;
			if(dv.Count!=0)
			{
				Datagrid1.DataBind();
				Datagrid1.Visible=true;
			}
			else
			{
				Datagrid1.Visible=false;
				MessageBox.Show("OE Data Not Available");
			}
			sqlcon.Dispose();
		}
		/*
		public void ItemDataBound(object sender,DataGridItemEventArgs e)
		{
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
			{
				string str = "";
				str=e.Item.Cells[7].Text;
				str=e.Item.Cells[10].Text;
			}
		}
		*/
		/// <summary>
		/// This method is used to show the report with the help of two function Bindthedata() and Bindthedata1()
		/// and set the value in session variable.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
                CreateLogFiles.ErrorLog("Error occured Kunal K." + Request.Form["txtDateFrom"].ToString());
                CreateLogFiles.ErrorLog("Error occured Textbox 1 Kunal K." + Request.Form["Textbox1"].ToString());
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {                 
					MessageBox.Show("Date From Should be less than Date To");
					GridSalesReport.Visible=false;
				}
				else
				{
					if(droptype.SelectedItem.Text.Equals("Fleet")||droptype.SelectedItem.Text.Equals("Both"))
					{
						strorderby="Cust_ID ASC";
						Session["Column"]="Cust_ID";
						Session["order"]="ASC";
						Bindthedata();

					}
					if(droptype.SelectedItem.Text.Equals("OE")||droptype.SelectedItem.Text.Equals("Both"))
					{
						strorderby1="Cust_ID ASC";
						Session["Column1"]="Cust_ID";
						Session["order1"]="ASC";
						Bindthedata1();
					}
					if(droptype.SelectedItem.Text.Equals("OE"))
					{
							Datagrid1.Visible=true;
						GridSalesReport.Visible=false;
					}
					if(droptype.SelectedItem.Text.Equals("Fleet"))
					{
						Datagrid1.Visible=false;
						GridSalesReport.Visible=true;
					}
				}
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:btnShow_Click  Fleet/OE Discount Report   Viewed "+"  userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:btnShow_Click  Sale Book Report   Viewed "+"  EXCEPTION  "+ ex.Message+"  userid  "+uid);
			}
		}
		
		double total1;
		/// <summary>
		/// This method is used to calculate the scheme discount.
		/// </summary>
		public double discountdis1(double qty,string dis,double sch)
		{
			double tot1;
			double scheme1;
			
			if(dis.Equals("0")||dis.Equals(""))
				tot1=0;
			else
				tot1=System.Convert.ToDouble(qty)*System.Convert.ToDouble(dis);
			
			if(sch.Equals("0")||sch.Equals(""))
				scheme1=0;
			else
				scheme1 =System.Convert.ToDouble(qty)*System.Convert.ToDouble(sch);
			//total+=(tot-scheme);
			total1+=(scheme1-tot1);
			Cache["total1"]=total1;
			//return (tot-scheme);
			return (scheme1-tot1);
		}

		double total11;
		/// <summary>
		/// This method is used to FOE scheme discount
		/// </summary>
		public double discountdis11(string foe,double sch)
		{
			double tot=0;
			if(System.Convert.ToDouble(foe)>sch)
				tot=System.Convert.ToDouble(foe)-sch;
			else
				tot=sch-System.Convert.ToDouble(foe);
			total11+=tot;
			Cache["total11"]=total11;
			//return (tot-scheme);
			return tot;
		}

		double total14;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double discountdis14(string DisGiv,double foc,double sch)
		{
			double tot=0;
			//if(System.Convert.ToDouble(DisGiv)>(sch+foc))
			tot=Math.Round(System.Convert.ToDouble(DisGiv)-(sch+foc),2);
			//else
			//tot=(foc+sch)-System.Convert.ToDouble(DisGiv);
			total14+=tot;
			Cache["total14"]=total14;
			//return (tot-scheme);
			return tot;
		}

		double total12;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double discountdis12(double foe,double sch)
		{
			double tot=0;
			if(foe>sch)
				tot=foe-sch;
			else
				tot=sch-foe;
			total12+=tot;
			Cache["total12"]=total12;
			//return (tot-scheme);
			return tot;
		}

		double total13;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double discountdis13(string Disgiv,double foc,double sch)
		{
			double tot=0;
			//if(System.Convert.ToDouble(Disgiv)>(foc+sch))
			tot=System.Convert.ToDouble(Disgiv)-(foc+sch);
			//else
			//tot=(foc+sch)-System.Convert.ToDouble(Disgiv);
			total13+=tot;
			Cache["total13"]=total13;
			//return (tot-scheme);
			return Math.Round(tot,2);
		}


		double total13oe;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double discountdis13oe(string Disgiv,double foc,double sch)
		{
			double tot=0;
			//if(System.Convert.ToDouble(Disgiv)>(foc+sch))
			tot=System.Convert.ToDouble(Disgiv)-(foc+sch);
			//else
			//tot=(foc+sch)-System.Convert.ToDouble(Disgiv);
			total13oe+=tot;
			Cache["total13oe"]=total13oe;
			//return (tot-scheme);
			return Math.Round(tot,2);
		}

		double total15;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double discountdis15(string Disgiv,double foc,double sch)
		{
			double tot=0;
			//if(System.Convert.ToDouble(Disgiv)>(foc+sch))
			tot=System.Convert.ToDouble(Disgiv)-(foc+sch);
			//else
			//tot=(foc+sch)-System.Convert.ToDouble(Disgiv);
			total15+=tot;
			Cache["total15"]=total15;
			//return (tot-scheme);
			return tot;
		}

		double total;
		/// <summary>
		/// This method is used to calculate the discount of given some discount.
		/// </summary>
		public double discountdis(double qty,string dis,double sch)
		{
				double tot;
			double scheme;
			
			if(dis.Equals("0")||dis.Equals(""))
				tot=0;
			else
				tot=System.Convert.ToDouble(qty)*System.Convert.ToDouble(dis);
			
			if(sch.Equals("0")||sch.Equals(""))
				scheme=0;
			else
				scheme =System.Convert.ToDouble(qty)*System.Convert.ToDouble(sch);
			//total+=(tot-scheme);
			total+=(scheme-tot);
			Cache["total"]=total;
			//return (tot-scheme);
			return (scheme-tot);
		}

		public string addfoetype(string foe,string foetype)
		{
			string foe1=foe;
			string foetype1=foetype;
			if(foetype1.ToString().Trim()!="Rs.")
			{
				foe1+=foetype1;
			}
			return foe1;
		}

		/// <summary>
		/// This method is used to calculate the scheme discount of given product name and pack type.
		/// </summary>
		public double schdiscount(string prodname,string packtype)
		{
			//string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			double dis;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			//coment by vikas sharma 22.05.09 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select discount,discountType from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
			{
				dis=0;
			}
			rdr.Close();
			return dis;
		}

		/// <summary>
		/// This method is used to calculate the scheme discount of given product name and pack type.
		/// </summary>
		public string schdiscount1(string prodname,string packtype)
		{
			//string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			string dis_type;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			//coment by vikas sharma 22.05.09 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select discountType from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis_type=rdr.GetValue(0).ToString();
			}
			else
			{
				dis_type="";
			}
			rdr.Close();
			
			if(dis_type.ToString().Trim()=="Rs")
				dis_type="";

			return dis_type;
		}


		/// <summary>
		/// This method is used to calculate the FOC discount of given product name and pack type.
		/// </summary>
		public double FOCdiscount(string prodname,string packtype,string qty)
		{
			//string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			double free=0,onevery=0;
			int Tot=0;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			//string sql1="select Top 1(sal_rate) from price_updation where prod_id='"+prodid+"' order by eff_date desc";
			//**string sql1="select freepack,onevery from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select freepack,onevery from oilscheme where prodid='"+prodid+"' and  cast(floor(cast(cast(datefrom as datetime) as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(cast(dateto as datetime) as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"' and schprodid<>0";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				free=System.Convert.ToDouble(rdr.GetValue(0).ToString());
				onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
			}
			rdr.Close();
			if(free>0)
			{
				if(System.Convert.ToDouble(qty)>onevery)
				{
					Tot=System.Convert.ToInt32(qty)/System.Convert.ToInt32(onevery);
				}
			}
			/*
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis=0;
				*/
			
			return System.Convert.ToDouble(Tot);
		}
		
		//<!Multiply(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))-->
		double totalltr=0;
		/// <summary>
		/// This method is used to return the total qty in liter.
		/// </summary>
		protected double Multiply11(string str)
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
				totalltr+=ans;
				//Cache["totalltr"]=totalltr/2;
				Cache["totalltr"]=totalltr;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltr+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr"]=totalltr/2;
					Cache["totalltr"]=totalltr;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}

		double totalltroe=0;
		/// <summary>
		/// This method is used to return the total qty in liter.
		/// </summary>
		protected double Multiply11oe(string str)
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
				totalltroe+=ans;
				//Cache["totalltr"]=totalltr/2;
				Cache["totalltroe"]=totalltroe;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltroe+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr"]=totalltr/2;
					Cache["totalltroe"]=totalltroe;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}


		double totalltrqty=0;
		/// <summary>
		/// This method is used to return the total qty in liter.
		/// </summary>
		protected double Multiply11qty(string str)
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
				totalltrqty+=ans;
				//Cache["totalltr"]=totalltr/2;
				Cache["totalltrqty"]=totalltrqty;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltrqty+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr"]=totalltr/2;
					Cache["totalltrqty"]=totalltrqty;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}

		/// <summary>
		/// This method is used to return the total qty in liter.
		/// </summary>
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
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//totalltr+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr"]=totalltr/2;
					//Cache["totalltr"]=totalltr;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}

		double totalltr1=0;
		/// <summary>
		/// This method is used to return the total qty in liter.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected double Multiply1(string str)
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
				totalltr1+=ans;
				//Cache["totalltr1"]=totalltr1/2;
				Cache["totalltr1"]=totalltr1;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltr1+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr1"]=totalltr1/2;
					//Cache["totalltr1"]=totalltr1;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}

		/// <summary>
		/// This Method is used to multiplies the package quantity with Quantity.
		/// </summary>
		//double totalltr1=0;
		protected double Multiply3(string str)
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
				//totalltr1+=ans;
				//Cache["totalltr1"]=totalltr1/2;
				//Cache["totalltr1"]=totalltr1;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//totalltr1+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr1"]=totalltr1/2;
					//Cache["totalltr1"]=totalltr1;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}
		
		//		public string DisTot(string schdis)
		//		{
		//			Dis+=System.Convert.ToDouble(schdis);
		//			Cache["Dis"]=Dis;
		//			return schdis;
		//		}
		//		double Dis1=0;
		//		public string DisTot1(string schdis)
		//		{
		//			Dis1+=System.Convert.ToDouble(schdis);
		//			Cache["Dis1"]=Dis1;
		//			return schdis;
		//		}

		/// <summary>
		/// This Method is used to get FOC cost of given product name and pack type and calculate the amount according to given qty.
		/// </summary>
		double FOC=0;
		protected double FOCcost12(string prodname,string packtype,double qty)
		{
			string prodid="",schprodid="";
			double free=0,onevery=0,Tot=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
			string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			//string sql1="select Top 1(sal_rate) from price_updation where prod_id='"+prodid+"' order by eff_date desc";
			//**string sql1="select freepack,onevery,schprodid from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select freepack,onevery,schprodid from oilscheme where prodid='"+prodid+"' and  cast(floor(cast(datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"' and schprodid<>0";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				free=System.Convert.ToDouble(rdr.GetValue(0).ToString());
				onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
				schprodid=rdr.GetValue(2).ToString();
			}
			rdr.Close();
			if(free>0)
			{
				//string sql1="select freepack,onevery from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
				sql1="select Top 1(sal_rate) from price_updation where prod_id='"+schprodid+"' order by eff_date desc";
				// Calls the sp_stockmovement for each product and create one stkmv temp. table.
				dbobj.SelectQuery(sql1,ref rdr1);
				if(rdr1.Read())
				{
					Tot=System.Convert.ToDouble(rdr1.GetValue(0).ToString())*qty;
					//onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
				}
				rdr1.Close();
			}
			/*
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis=0;
				*/
			FOC+=Tot;
			//MessageBox.Show("FOCCOst : "+FOC.ToString());
			Cache["FOCcost"]=FOC;
			return System.Convert.ToDouble(Tot);
			
		}

		/// <summary>
		/// This Method get FOC cost of given product name and pack type after that calculate the amount according to given qty.
		/// </summary>
		protected double FOCcost(string prodname,string packtype,double qty)
		{
			string prodid="",schprodid="";
			double free=0,onevery=0,Tot=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
			string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			//string sql1="select Top 1(sal_rate) from price_updation where prod_id='"+prodid+"' order by eff_date desc";
			string sql1="select freepack,onevery,schprodid from oilscheme where prodid='"+prodid+"' and  cast(floor(cast(datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"' and schprodid<>0";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				free=System.Convert.ToDouble(rdr.GetValue(0).ToString());
				onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
				schprodid=rdr.GetValue(2).ToString();
			}
			rdr.Close();
			if(free>0)
			{
				//string sql1="select freepack,onevery from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
				sql1="select Top 1(sal_rate) from price_updation where prod_id='"+schprodid+"' order by eff_date desc";
				// Calls the sp_stockmovement for each product and create one stkmv temp. table.
				dbobj.SelectQuery(sql1,ref rdr1);
				if(rdr1.Read())
				{
					Tot=System.Convert.ToDouble(rdr1.GetValue(0).ToString())*qty;
					//onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
				}
				rdr1.Close();
			}
			/*
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis=0;
				*/
			/*FOC+=Tot;
			MessageBox.Show("FOCCOst : "+FOC.ToString());
			Cache["FOCcost"]=FOC;*/
			return System.Convert.ToDouble(Tot);
			
		}

		/// <summary>
		/// This Method get FOC cost of given product name, pack type and calculate amount with given qty.
		/// </summary>
		double FOC1=0;
		protected double FOCcost1(string prodname,string packtype,double qty)
		{
			string prodid="",schprodid="";
			double free=0,onevery=0,Tot=0;
			
			System.Data.SqlClient.SqlDataReader rdr=null,rdr1=null;
			string sql="select prod_id from products where prod_name='"+prodname+"' and pack_type='"+packtype+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			//string sql1="select Top 1(sal_rate) from price_updation where prod_id='"+prodid+"' order by eff_date desc";
			//**string sql1="select freepack,onevery,schprodid from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			string sql1="select freepack,onevery,schprodid from oilscheme where prodid='"+prodid+"' and  cast(floor(cast(datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"' and schprodid<>0";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				free=System.Convert.ToDouble(rdr.GetValue(0).ToString());
				onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
				schprodid=rdr.GetValue(2).ToString();
			}
			rdr.Close();
			if(free>0)
			{
				//string sql1="select freepack,onevery from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
				sql1="select Top 1(sal_rate) from price_updation where prod_id='"+schprodid+"' order by eff_date desc";
				// Calls the sp_stockmovement for each product and create one stkmv temp. table.
				dbobj.SelectQuery(sql1,ref rdr1);
				if(rdr1.Read())
				{
					Tot=System.Convert.ToDouble(rdr1.GetValue(0).ToString())*qty;
					//onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
				}
				rdr1.Close();
			}
			/*
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis=0;
				*/
			FOC1+=Tot;
			Cache["FOC1"]=FOC1;
			return System.Convert.ToDouble(Tot);
			
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		//double Mul2=0;
		protected double Multiply2(string str,string qty)
		{
			
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			double ans=1;
			if(str.Trim().IndexOf("Loose") == -1)
			{
				ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
					
					
				}
				//Mul2+=ans;
				//Cache["Mul2"]=Mul2;
				//*return ans;
			}
			return ans*double.Parse(qty);	

			/*
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltr1+=System.Convert.ToDouble( mystr[0].ToString()); 
					Cache["totalltr1"]=totalltr1/2;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
			*/
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		double Mul21=0;
		protected double Multiply21(string str,string qty)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			double ans=1;
			if(str.Trim().IndexOf("Loose") == -1)
			{
				ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
					
					
				}
				Mul21+=ans*double.Parse(qty);
				Cache["Mul21"]=Mul21;
				//*return ans;
			}
			return ans*double.Parse(qty);	

			/*
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltr1+=System.Convert.ToDouble( mystr[0].ToString()); 
					Cache["totalltr1"]=totalltr1/2;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
			*/
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		double Mul22=0;
		protected double Multiply22(string str,string qty)
		{
			//********************start***Comment by vikas sharma 21.05.09
			/*string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			double ans=1;
			if(str.Trim().IndexOf("Loose") == -1)
			{
				ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//Mul22+=ans;
				Mul22+=ans*double.Parse(qty);
				Cache["Mul22"]=Mul22;
				//*return ans;
			}
			Mul22+=Tot*double.Parse(qty);
			Cache["Mul22"]=Mul22;
			return ans*double.Parse(qty);*/
			//****************end***********************************

			string ans="0";
			//Tot=System.Convert.ToString(System.Convert.ToDouble(ltr)/System.Convert.ToDouble(qty));
			ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(qty)/100),2));
			//Dis+=double.Parse(Tot);
			Mul22+=Convert.ToDouble(ans.ToString());
			Cache["Mul22"]=ans;
			return Convert.ToDouble(ans.ToString());

			/*
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totalltr1+=System.Convert.ToDouble( mystr[0].ToString()); 
					Cache["totalltr1"]=totalltr1/2;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
			*/
		}

		double Mul222=0;
		protected double Multiply222(string str,string qty,string proval,string foetype)
		{
			string ans="0";

			/*string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				Mul22+=ans*double.Parse(qty);
				Cache["Mul22"]=Mul22;
			}

			Mul22+=Tot*double.Parse(qty);
			Cache["Mul22"]=Mul22;
			return ans*double.Parse(qty);*/

			if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(proval)/100),2));
			else
				ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(qty)),2));
			
			Mul222+=Convert.ToDouble(ans.ToString());
			Cache["Mul22"]=ans;
			return Convert.ToDouble(ans.ToString());
		}

		double Mulclaim=0;
		protected double Multiplyclaim(string str,string qty,string proval,string foetype)
		{
			string ans="0";
			if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(proval)/100),2));
			else
				ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(qty)),2));
			
			Mulclaim+=Convert.ToDouble(ans.ToString());
			Cache["Mulclaim"]=Mulclaim;
			return Convert.ToDouble(ans.ToString());
		}

		double Mulclaimoe=0;
		protected double Multiplyclaimoe(string str,string qty,string proval,string foetype)
		{
			string ans="0";
			if(foetype.ToString().Trim()!="Rs." && foetype.ToString().Trim()!="")
				ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(proval)/100),2));
			else
				ans=Convert.ToString(Math.Round((System.Convert.ToDouble(str)*System.Convert.ToDouble(qty)),2));
			
			Mulclaimoe+=Convert.ToDouble(ans.ToString());
			Cache["Mulclaimoe"]=Mulclaimoe;
			return Convert.ToDouble(ans.ToString());
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
		/// This Method is used to prepare the report file.
		/// </summary>
		public void makingReport()
		{
			
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "",info1 = "";
				string strDate="";
				Cache["totalltr"]="0";
				Cache["total"]="0";
				Cache["total1"]="0";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\FleetOEDiscountReport.txt";
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
				//string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("FLEET OE REPORT From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==================================================",des.Length));
				if(droptype.SelectedItem.Text.Equals("Fleet")||droptype.SelectedItem.Text.Equals("Both"))
				{
					//coment by vikas sharma 21.05.09 sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cust_type='Fleet'";
					sql="select * from Vw_fleetoediscountReport where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103) and cust_type='Fleet'";
					sql=sql+" order by "+Cache["strorderby"];
					dbobj.SelectQuery(sql,ref rdr);
										
					if(rdr.HasRows)
					{
						//string des="-----------------------------------------------------------------------------------------------------------------------------------------";
						sw.WriteLine("                 Fleet Discount Report For The Period Of "+txtDateFrom.Text+" To "+Textbox1.Text);
						
						//************start***********************comment by vikas 23.05.09
						/*sw.WriteLine("+-----+----------+--------+--------+--------+-------+-------+---------+----------+------------+------------+------------+---------------+");
						sw.WriteLine("| Inv | Inv.Date |Customer|Customer| Place  |Product| Pack. |   Qty   | Product  |Disc. Given | FOC Disc.  |Scheme Disc.| Claim Amount  |");
						sw.WriteLine("| .No |          |  Type  |  Name  |        | Name  | Type  |No.| Ltr |  Value   | Ltr | Rs.  | Ltr | Rs.  | Ltr | Rs.  | Ltr |   Rs.   |");
						sw.WriteLine("+-----+----------+--------+--------+--------+-------+-------+---+-----+----------+-----+------+-----+------+-----+------+-----+---------+"); 
						//			   12345 1234567890 12345678 12345678 12345678 1234567 1234567 123 12345 1234567890 12345 123456 12345 123456 12345 123456 12345 123456789*/
						//************end*******************************
			
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---------+----------+---------------+------------+---------------+-----------------+");
						sw.WriteLine("| Inv | Inv.Date |Customer| Place  |Product| Pack. |   Qty   | Product  | Disc. Given   | FOC Disc.  |Scheme  Disc.  | Claim   Amount  |");
						sw.WriteLine("| .No |          |  Name  |        | Name  | Type  |No.| Ltr |  Value   |  Ltr | Rs.    | Ltr | Rs.  | Ltr  | Rs.    | Ltr |     Rs.   |");
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---+-----+----------+------+--------+-----+------+------+--------+-----+-----------+"); 
						//			   12345 1234567890 12345678 12345678 1234567 1234567 123 12345 1234567890 123456 12345678 12345 123456 123456 12345678 12345 12345678901


					
						// info : to set the string format.
						//info = " {0,-4:S} {1,-20:S} {2,-15:S} {3,-13:S} {4,-9:S} {5,-13:S} {6,-11:S} {7,-9:S} {8,-10:S} {9,-8:S} {10,8:F} {11,-9:S}";
						//coment by vikas 23.05.09 info = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-8:S} {5,-7:S} {6,-7:S} {7,3:S} {8,5:S} {9,10:F} {10,5:S} {11,6:F} {12,5:S} {13,6:S} {14,5:S} {15,6:S} {16,5:S} {17,9:S}";
						info = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-7:S} {5,-7:S} {6,3:S} {7,5:S} {8,10:F} {9,6:S} {10,8:F} {11,5:S} {12,6:S} {13,6:S} {14,8:S} {15,5:S} {16,11:S}";
						//coment by vikas 23.05.09 info1 = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-8:S} {5,-7:S} {6,-7:S} {7,9:S} {8,10:S} {9,12:F} {10,12:S} {11,12:F} {12,15:S}";
						info1 = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-7:S} {5,-7:S} {6,9:S} {7,10:S} {8,15:F} {9,12:S} {10,15:F} {11,17:S}";
						while(rdr.Read())
						{					
							strDate = rdr["Invoice_Date"].ToString().Trim();
							int pos = strDate.IndexOf(" ");
				
							if(pos != -1)
							{
								strDate = strDate.Substring(0,pos);
							}
							else
							{
								strDate = "";					
							}
							
							string InvNo=rdr["Invoice_No"].ToString();
							if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
								InvNo=InvNo.Substring(4);
							else
								InvNo=InvNo.Substring(3);
							
							sw.WriteLine(info,InvNo,
								GenUtil.trimDate(GenUtil.str2DDMMYYYY(rdr["Invoice_Date"].ToString())),
								//GenUtil.TrimLength(rdr["Cust_Type"].ToString().Trim(),8),
								GenUtil.TrimLength(rdr["Cust_Name"].ToString().Trim(),8),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),8),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),7),
								GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),7),
								rdr["Qty"].ToString().Trim(),
								System.Convert.ToString(Multiply11(rdr["Qty"].ToString().Trim()+"X"+rdr["Pack_Type"].ToString().Trim()).ToString()),
															
								GetProductValue(rdr["Rate"].ToString(),rdr["Qty"].ToString()),
								
								//coment by vikas 22.05.09 rdr["foe"].ToString(),
								addfoetype(rdr["foe"].ToString(),rdr["FoeType"].ToString()),

								//coment by vikas 22.05.09 DisGiven(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),
								//comment by vikas 23.05.09 DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()),
								DisGivenfleet(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()),

								System.Convert.ToString(FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),
								System.Convert.ToString(FOCcost12(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()))),
								
								//coment by vikas 22.05.09 System.Convert.ToString(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())),
								//coment by vikas 23.05.09 addfoetype(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),rdr["FoeType"].ToString()),
								schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()),
								
								//Coment by vikas 22.05.09 System.Convert.ToString(Multiply22(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),
								//Coment by vikas 23.05.09 System.Convert.ToString(Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString())),
								System.Convert.ToString(Multiplyclaim(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString())),
								
								System.Convert.ToString(discountdis14(rdr["foe"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()),schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()))),
																
								//coment by vikas sharma 22.05.09 System.Convert.ToString(discountdis13(DisGiven (rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply2  (schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())))
								//coment by vikas sharma 23.05.09 System.Convert.ToString(discountdis13(DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString())))
								System.Convert.ToString(discountdis13(DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString())))
								);
						}
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---+-----+----------+------+--------+-----+------+------+--------+-----+-----------+");
						//coment by vikas 23.05.09 sw.WriteLine(info1,"","Total:","","","","","",Cache["totalltr"].ToString(),Cache["ProdValue"].ToString(),Cache["Dis"].ToString(),Cache["FOCcost"].ToString(),Cache["Mul22"].ToString(),Cache["total13"].ToString());
						//sw.WriteLine("Total\t\t\t\t\t\t\t\t"+Cache["totalltrqty"].ToString()+"\t"+Cache["ProdValue2"].ToString()+"\t\t"+Cache["Disfleet"].ToString()+"\t\t"+Cache["FOCcost"].ToString()+"\t\t"+Cache["Mulclaim"].ToString()+"\t\t"+Cache["total13"].ToString());
						sw.WriteLine(info1,"","Total:","","","","",Cache["totalltrqty"].ToString(),Cache["ProdValue2"].ToString(),Cache["Disfleet"].ToString(),Cache["FOCcost"].ToString(),Cache["Mulclaim"].ToString(),Cache["total13"].ToString());
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---+-----+----------+------+--------+-----+------+------+--------+-----+-----------+");
					}
					dbobj.Dispose();
				}
				if(droptype.SelectedItem.Text.Equals("OE")||droptype.SelectedItem.Text.Equals("Both"))
				{
					//coment by vikas sharma 21.05.09 sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cust_type like('Oe%')";
					sql="select * from Vw_fleetoediscountReport where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103) and cust_type like('Oe%')";
					sql=sql+" order by "+Cache["strorderby1"];
					dbobj.SelectQuery(sql,ref rdr);
					// Condensed
					if(rdr.HasRows)
					{
						sw.WriteLine("                 OE Discount Report For The Period Of "+txtDateFrom.Text+" To "+Textbox1.Text);
						/**************start coment by vikas 23.05.09*************************************/
						/*sw.WriteLine("+-----+----------+--------+--------+--------+-------+-------+---------+----------+------------+------------+------------+---------------+");
						sw.WriteLine("| Inv | Inv.Date |Customer|Customer| Place  |Product| Pack. |   Qty   | Product  |Disc. Given | FOC Disc.  |Scheme disc.| Claim Amount  |");
						sw.WriteLine("| .No |          |  Type  |  Name  |        | Name  | Type  |No.| Ltr |  Value   | Ltr | Rs.  | Ltr | Rs.  | Ltr | Rs.  | Ltr |   Rs.   |");
						sw.WriteLine("+-----+----------+--------+--------+--------+-------+-------+---+-----+----------+-----+------+-----+------+-----+------+-----+---------+"); */
						/**************end coment by vikas 23.05.09*************************************/
						
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---------+----------+---------------+------------+---------------+-----------------+");
						sw.WriteLine("| Inv | Inv.Date |Customer| Place  |Product| Pack. |   Qty   | Product  | Disc. Given   | FOC Disc.  |Scheme  Disc.  | Claim   Amount  |");
						sw.WriteLine("| .No |          |  Name  |        | Name  | Type  |No.| Ltr |  Value   |  Ltr | Rs.    | Ltr | Rs.  | Ltr  | Rs.    | Ltr |     Rs.   |");
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---+-----+----------+------+--------+-----+------+------+--------+-----+-----------+"); 
						//			   12345 1234567890 12345678 12345678 1234567 1234567 123 12345 1234567890 123456 12345678 12345 123456 123456 12345678 12345 12345678901
						
						//coment by vikas 23.05.09 info = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-8:S} {5,-7:S} {6,-7:S} {7,3:S} {8,5:S} {9,10:F} {10,5:S} {11,6:F} {12,5:S} {13,6:S} {14,5:S} {15,6:S} {16,5:S} {17,9:S}";
						info = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-7:S} {5,-7:S} {6,3:S} {7,5:S} {8,10:F} {9,6:S} {10,8:F} {11,5:S} {12,6:S} {13,6:S} {14,8:S} {15,5:S} {16,11:S}";
						//coment by vikas 23.05.09 info1 = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-8:S} {5,-7:S} {6,-7:S} {7,9:S} {8,10:S} {9,12:F} {10,12:S} {11,12:F} {12,15:S}";
						info1 = " {0,-5:S} {1,-10:S} {2,-8:S} {3,-8:S} {4,-7:S} {5,-7:S} {6,9:S} {7,10:S} {8,15:F} {9,12:S} {10,15:F} {11,17:S}";
		  
						while(rdr.Read())
						{					
							strDate = rdr["Invoice_Date"].ToString().Trim();
							int pos = strDate.IndexOf(" ");
				
							if(pos != -1)
							{
								strDate = strDate.Substring(0,pos);
							}
							else
							{
								strDate = "";					
							}
							
							string InvNo=rdr["Invoice_No"].ToString();
							if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
								InvNo=InvNo.Substring(4);
							else
								InvNo=InvNo.Substring(3);
							
							sw.WriteLine(info,InvNo,
								GenUtil.trimDate(GenUtil.str2DDMMYYYY(rdr["Invoice_Date"].ToString())),
								//coment by vikas 23.05.09 GenUtil.TrimLength(rdr["Cust_Type"].ToString().Trim(),8),
								GenUtil.TrimLength(rdr["Cust_Name"].ToString().Trim(),8),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),8),
								GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),7),
								GenUtil.TrimLength(rdr["Pack_Type"].ToString().Trim(),7),
								
								rdr["Qty"].ToString().Trim(),
								System.Convert.ToString(Multiply1(rdr["Qty"].ToString().Trim()+"X"+rdr["Pack_Type"].ToString().Trim()).ToString()),
								GetProductValue1(rdr["Rate"].ToString(),rdr["Qty"].ToString()),
								
								//comment by vikas sharma 23.05.09 rdr["foe"].ToString(),
								addfoetype(rdr["foe"].ToString(),rdr["FoeType"].ToString()),
								
								//comment by vikas sharma 23.05.09 DisGiven(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),
								DisGivenoe(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValueoe"].ToString(),rdr["FoeType"].ToString()),
								
								System.Convert.ToString(FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),
								System.Convert.ToString(FOCcost1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()))),
								
								//coment by vikas 23.05.09 System.Convert.ToString(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())),
								System.Convert.ToString(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())),
								
								//comment by vikas 23.05.09 System.Convert.ToString(Multiply21(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),
								System.Convert.ToString(Multiplyclaimoe(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11oe(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValueoe"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString())),
								
								System.Convert.ToString(discountdis14(rdr["foe"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()),schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()))),

								//comment by vikas 23.05.09 System.Convert.ToString(discountdis15(DisGiven(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply2(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())))
								System.Convert.ToString(discountdis13oe(DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValueoe"].ToString(),rdr["FoeType"].ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValueoe"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()))))
								);
						}
					

						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---+-----+----------+------+--------+-----+------+------+--------+-----+-----------+");
						//comment by vikas 23.05.09 sw.WriteLine(info1,"","Total:","","","","","",Cache["totalltr1"].ToString(),Cache["ProdValue1"].ToString(),Cache["Dis1"].ToString(),Cache["FOC1"].ToString(),Cache["Mul21"].ToString(),Cache["total15"].ToString());
						sw.WriteLine(info1,"","Total:","","","","",Cache["totalltr1"].ToString(),Cache["ProdValue1"].ToString(),Cache["Disoe"].ToString(),Cache["FOC1"].ToString(),Cache["Mulclaimoe"].ToString(),Cache["total13oe"].ToString());
						//sw.WriteLine("Total\t\t\t\t\t\t\t\t"+Cache["totalltr1"].ToString()+"\t"+Cache["ProdValue1"].ToString()+"\t\t"+Cache["Disoe"].ToString()+"\t\t"+Cache["FOC1"].ToString()+"\t\t"+Cache["Mulclaimoe"].ToString()+"\t\t"+Cache["total13oe"].ToString());
						sw.WriteLine("+-----+----------+--------+--------+-------+-------+---+-----+----------+------+--------+-----+------+------+--------+-----+-----------+");
					}
		
					dbobj.Dispose();
				
				}
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
				//Session["From_Date"] = txtDateFrom.Text;
				//Session["To_Date"] = Textbox1.Text;
				//	Response.Redirect("SalesBook_PrintPreview.aspx",false); 

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\FleetOeDiscountReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			Cache["totalltr"]="0";
			Cache["total"]="0";
			//Cache["totalltr1"]="0";
			Cache["total1"]="0";
			if(droptype.SelectedItem.Text.Equals("Fleet")||droptype.SelectedItem.Text.Equals("Both"))
			{
				//Comment by vikas sharma 21.05.09 sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cust_type='Fleet'";
				sql= "select * from Vw_fleetoediscountReport where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103) and cust_type='Fleet'";
				sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
					
				if(rdr.HasRows)
				{
					sw.WriteLine("Invoice No\tInvoice Date\tCustomer Type\tCustomer Name\tPlace\tProduct Name\tPack Type\tQuantity\tQtyLtr\tProduct Value\tDiscount Given(Ltr)\tDiscount Given(Rs.)\tFOC(Ltr)\tFOC(Rs.)\tScheme(Ltr)\tScheme(Rs.)\tClaim Amount(Ltr)\tClaim Amount(Rs.)");
					while(rdr.Read())
					{					
							string s = rdr["Invoice_No"].ToString();
						s = s.Substring(3);
						//sw.WriteLine(rdr["Invoice_No"].ToString().Trim()+"\t"+
						sw.WriteLine(s+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
							rdr["Cust_Type"].ToString().Trim()+"\t"+
							rdr["Cust_Name"].ToString().Trim()+"\t"+
							rdr["City"].ToString().Trim()+"\t"+
							rdr["Prod_Name"].ToString().Trim()+"\t"+
							rdr["Pack_Type"].ToString().Trim()+"\t"+
							rdr["Qty"].ToString().Trim()+"\t"+
							Multiply11(rdr["Qty"].ToString().Trim()+"X"+rdr["Pack_Type"].ToString().Trim()).ToString()+"\t"+
							GetProductValue(rdr["Rate"].ToString(),rdr["Qty"].ToString())+"\t"+
							
							//comment by vikas 22.05.09 rdr["foe"].ToString()+"\t"+
							addfoetype(rdr["foe"].ToString(),rdr["FoeType"].ToString())+"\t"+
							
							//DisGiven(rdr["schdiscount"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString())+"\t"+
							
							//coment by vikas 22.05.09 DisGiven(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString())+"\t"+
							DisGivenfleet(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString())+"\t"+
							
							FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())+"\t"+
							FOCcost12(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()))+"\t"+
							
							//comment by vikas sharma 22.05.09 schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"\t"+
							//comment by vikas 23.05.09 addfoetype(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),rdr["FoeType"].ToString())+"\t"+
							schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"\t"+
							
							//comment by vikas 22.05.09 Multiply22(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())+"\t"+
							//coment by vikas sharma 23.05.09 System.Convert.ToString(Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()))+"\t"+
							System.Convert.ToString(Multiplyclaim(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString()))+"\t"+
							
							//discountdis14(DisGiven(rdr["schdiscount"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()),schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()))+"\t"+
							discountdis14(rdr["foe"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()),schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()))+"\t"+
							
							//Comment by vikas 22.05.09 discountdis13(DisGiven(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply2(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()))
							//comment by vikas 22.05.09 System.Convert.ToString(discountdis13(DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString())))
							System.Convert.ToString(discountdis13(DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValue"].ToString(),rdr["FoeType"].ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValue"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString())))
							);																																																																																																																																																		   																			
					}
					//comment by vikas 23.05.09 sw.WriteLine("Total\t\t\t\t\t\t\t\t"+Cache["totalltr"].ToString()+"\t"+Cache["ProdValue"].ToString()+"\t\t"+Cache["Dis"].ToString()+"\t\t"+Cache["FOCcost"].ToString()+"\t\t"+Cache["Mul22"].ToString()+"\t\t"+Cache["total13"].ToString());
					sw.WriteLine("Total\t\t\t\t\t\t\t\t"+Cache["totalltrqty"].ToString()+"\t"+Cache["ProdValue2"].ToString()+"\t\t"+Cache["Disfleet"].ToString()+"\t\t"+Cache["FOCcost"].ToString()+"\t\t"+Cache["Mulclaim"].ToString()+"\t\t"+Cache["total13"].ToString());
				}
				dbobj.Dispose();
			}
			if(droptype.SelectedItem.Text.Equals("OE")||droptype.SelectedItem.Text.Equals("Both"))
			{
				//Comment by vikas sharma 21.05.09 sql="select * from Vw_fleetoediscount where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"' and cust_type like('Oe%')";
				sql="select * from Vw_fleetoediscountReport where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()) + "',103) and cust_type like('Oe%')";
				sql=sql+" order by "+Cache["strorderby1"];
				dbobj.SelectQuery(sql,ref rdr);
				
				if(rdr.HasRows)
				{
					sw.WriteLine();
					sw.WriteLine("Invoice No\tInvoice Date\tCustomer Type\tCustomer Name\tPlace\tProduct Name\tPack Type\tQuantity\tQtyLtr\tProduct Value\tDiscount Given(Rs.)\tDiscount Given(Rs./Ltr)\tFOC(Rs.)\tFOC(Rs./Ltr)\tScheme(Rs.)\tScheme(Rs./Ltr)\tClaim Amount(Rs.)\tClaim Amount(Rs./Ltr)");
					while(rdr.Read())
					{	
						string s = rdr["Invoice_No"].ToString();
						s = s.Substring(3);
						//sw.WriteLine(rdr["Invoice_No"].ToString().Trim()+"\t"+
						sw.WriteLine(s+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
							rdr["Cust_Type"].ToString().Trim()+"\t"+
							rdr["Cust_Name"].ToString().Trim()+"\t"+
							rdr["City"].ToString().Trim()+"\t"+
							rdr["Prod_Name"].ToString().Trim()+"\t"+
							rdr["Pack_Type"].ToString().Trim()+"\t"+
							rdr["Qty"].ToString().Trim()+"\t"+
							Multiply1(rdr["Qty"].ToString().Trim()+"X"+rdr["Pack_Type"].ToString().Trim()).ToString()+"\t"+
							
							GetProductValue1(rdr["Rate"].ToString(),rdr["Qty"].ToString())+"\t"+
							
							//comment by vikas sharma 23.05.09 rdr["foe"].ToString()+"\t"+
							addfoetype(rdr["foe"].ToString(),rdr["FoeType"].ToString())+"\t"+
							
							//comment by vikas 23.05.09 DisGiven(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString())+"\t"+
							DisGivenoe(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValueoe"].ToString(),rdr["FoeType"].ToString())+"\t"+

							FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())+"\t"+
							FOCcost1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()))+"\t"+
							
							//comment by vikas 23.05.09 schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"\t"+
							schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"\t"+

							//comment by vikas sharma 22.05.09 Multiply21(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())+"\t"+
							Multiplyclaimoe(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11oe(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValueoe"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString())+"\t"+
							
							discountdis14(rdr["foe"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()),schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()))+"\t"+
							
							//comment by vikas 22.05.09 discountdis15(DisGiven (rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply2  (schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())+"X"+rdr["Pack_Type"].ToString(),rdr["Qty"].ToString()))
							discountdis13oe(DisGiven3(rdr["foe"].ToString(),(Multiply(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString())).ToString(),Cache["ProdValueoe"].ToString(),rdr["FoeType"].ToString()),FOCcost(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),FOCdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString(),rdr["Qty"].ToString())),Multiply222(schdiscount(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString()).ToString(),Multiply11(rdr["Qty"].ToString()+"X"+rdr["Pack_Type"].ToString()).ToString(),Cache["ProdValueoe"].ToString(),schdiscount1(rdr["Prod_Name"].ToString(),rdr["Pack_Type"].ToString())))
							);
					}
					//comment by vikas 23.05.09 sw.WriteLine("Total\t\t\t\t\t\t\t\t"+Cache["totalltr1"].ToString()+"\t"+Cache["ProdValue1"].ToString()+"\t\t"+Cache["Dis1"].ToString()+"\t\t"+Cache["FOC1"].ToString()+"\t\t"+Cache["Mul21"].ToString()+"\t\t"+Cache["total15"].ToString());
					sw.WriteLine("Total\t\t\t\t\t\t\t\t"+Cache["totalltr1"].ToString()+"\t"+Cache["ProdValue1"].ToString()+"\t\t"+Cache["Disoe"].ToString()+"\t\t"+Cache["FOC1"].ToString()+"\t\t"+Cache["Mulclaimoe"].ToString()+"\t\t"+Cache["total13oe"].ToString());
					
				}
				dbobj.Dispose();
			}
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file FleetOeDiscountReport.txt for printing.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			makingReport();

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\FleetOEDiscountReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:BtnPrint_Click  Fleet/OE discount Report   userid  "+uid);
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:BtnPrint_Click, Fleet/OE discount Report Printed    EXCEPTION  "+ ane.Message+" userid  "+  uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:BtnPrint_Click, Fleet/OE discount Report Printed  EXCEPTION  "+ se.Message+"  userid  "+  uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
	
					CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:BtnPrint_Click, Fleet/OE discount Report Printed   EXCEPTION "+es.Message+"  userid  "+  uid);
				}

			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:BtnPrint_Click, Fleet/OE discount Report Printed  EXCEPTION   "+ es.Message+"  userid  "+  uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file FleetOeDiscountReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridSalesReport.Visible==true || Datagrid1.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:FleetOeDiscountReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click  FleetOeDiscountReport Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:FleetOeDiscountReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click  FleetOeDiscountReport Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
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
					string trans_no = "";
					trans_no = e.Item.Cells[0].Text;
					if(FromDate!="")
					{
						if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
							trans_no=trans_no.Substring(4);
						else
							trans_no=trans_no.Substring(3);
						e.Item.Cells[0].Text=trans_no;
					}
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This method is used to return only year of passing date
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetYear(string dt)
		{
             if (dt != "")
                {
                    string[] year = dt.IndexOf('-') > 0 ? dt.Split(new char[] { '-' }, dt.Length) : dt.Split(new char[] { '/' }, dt.Length);
                    string yr = year[2].Substring(2);
                    return (yr);
                }
                else
                    return "";
         
         
		}

		/// <summary>
		/// This method return the amount of product
		/// </summary>
		double ProdValue=0,ProdValue1=0;
		public string GetProductValue(string rate,string qty)
		{
			double Tot=0;
			Tot=double.Parse(rate)*double.Parse(qty);
			ProdValue+=Tot;
			//comment by vikas sharma 21.05.09 Cache["ProdValue"]=ProdValue.ToString();
			Cache["ProdValue2"]=ProdValue.ToString();
			Cache["ProdValue"]=Tot.ToString();
			return Tot.ToString();
		}

		/// <summary>
		/// This method return the amount of product
		/// </summary>
		public string GetProductValue1(string rate,string qty)
		{
			double Tot=0;
			Tot=double.Parse(rate)*double.Parse(qty);
			ProdValue1+=Tot;
			Cache["ProdValueoe"]=Tot.ToString();
			Cache["ProdValue1"]=ProdValue1.ToString();
			return Tot.ToString();
		}
	}
}