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
	/// Summary description for Schemelist.
	/// </summary>
	public partial class Schemelist : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString ());
				if(!Page.IsPostBack)
				{
					lblDroptype.Text="Scheme Type";
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="37";
					string[,] Priv=(string[,]) Session["Privileges"];
					for(i=0;i<Priv.GetLength(0);i++)
					{
						if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
						{						
							View_Flag=Priv[i,2];
							Add_Flag=Priv[i,3];
							Edit_Flag=Priv[i,4];
							Del_Flag=Priv[i,5];

							break;
						}
					}	
				
					Cache["View"]=View_Flag;
					if(Add_Flag=="0" && Edit_Flag=="0" && Del_Flag=="0" && View_Flag=="0")
					{
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					}
					if(View_Flag=="0")
						//	btnview.Enabled=false;
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					#endregion
					DataGrid1.Visible=false;
					txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					txtDateTo.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Method:Page_Load, userid  "+uid );
					GetMultiValue();
					Fill_Drop();
				}
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Method:Page_Load, EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}
		
		public void Fill_Drop()
		{
			try
			{
				/*coment by vikas 6.11.2012ArrayList Scheme=new ArrayList();
				SqlDataReader rdr=null;
				string sql="select distinct schname from oilscheme order by schname";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					Scheme.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();
			
				sql="select distinct schname from Per_Discount where schname not in (select distinct schname from oilscheme ) order by schname";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					Scheme.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				Scheme.Sort();

				 for(int i=0;i<Scheme.Count;i++)
				{
					DropSchemeType.Items.Add(Scheme[i].ToString());
				}*/


				/******************************/
				SqlDataReader rdr=null;
				string sql="select distinct schname from oilscheme order by schname";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					tempsaleScheme.Value+=rdr.GetValue(0).ToString()+":";
					//DropSchemeType.Items.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();
			
				sql="select distinct schname from Per_Discount where schname not in (select distinct schname from oilscheme ) order by schname";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					temppurScheme.Value+=rdr.GetValue(0).ToString();
				}
				rdr.Close();

				/******************************/
				sql="select distinct Ctype from foe";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					DropReSailer.Items.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();

				/*************Add by vikas 7.11.2012*************************/
				sql="select distinct Group_Name from oilscheme order by Group_name";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
						dropGroup.Items.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				/*************End*************************/
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Method:Fill_Drop(), EXCEPTION "+ex.Message+"  userid  "+uid );
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
		/// this is used to make sorting the datagrid on clicking of datagridheader.
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
				//11.09.09 Bindthedata();
				if(RadBtnScheme.Checked==true)
				{
					Bindthedata();
				}
				else
				{
					Bindthedata_New();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SchemelistReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message);
			}
		}

		/// <summary>
		/// this is used to binding the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//string  sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname='Secondry' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id";
			//string  sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname='Primary' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' >= cast(floor(cast(o.dateto as float)) as datetime)";
			string  sql;
			/*****************Coment by vikas 5.11.2012***********************
			if(DropSchemeType.SelectedItem.Text.Equals("All"))
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
				else
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
			****************End***********************/

			/*****************Add by vikas 5.11.2012***********************/
			/*coment by vikas 6.11.2012
			if(DropSchemeType.SelectedItem.Text.Equals("All"))
				sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
			else
				sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";*/
			
			/*************Add by vikas 7.11.2012*****************************/
			if(dropsalpur.SelectedIndex!=0)
			{
				if(dropsalpur.SelectedIndex==1)
				{
					if(DropSchemeType.SelectedItem.Text.Equals("All"))
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					else
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
				}
				else
				{
					if(DropSchemeType.SelectedItem.Text.Equals("All"))
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					else
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
				}
			}
			else
			{
				if(DropSchemeType.SelectedItem.Text.Equals("All"))
					//coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					//cast(o.discount as varchar)+' : '+o.discountType s5
				else
					//coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
			}
			/****************End***********************/

			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				sql += " and Prod_Code='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and Prod_ID='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and Prod_Name='"+arrname[0]+"' and Pack_Type='"+arrname[1]+"'";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and Prod_Name='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and Pack_Type='"+DropValue.Value+"'";

			/********Add by vikas 7.11.2012****************/
			if(dropGroup.SelectedIndex!=0)
			{
				sql+= " and group_name ='"+dropGroup.SelectedValue.ToString()+"'";
			}
				
			if(DropSchemeType.SelectedIndex!=0)
			{
				sql+= " and o.schname='"+DropSchemeType.SelectedValue.ToString()+"'";
			}
			/********End****************/
			//**************
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"products");
			DataTable dtcustomer=ds.Tables["products"]; 
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			DataGrid1.DataSource=dv;
			if(dv.Count!=0)
			{
				DataGrid1.DataBind();
				DataGrid1.Visible=true;
				GridReseller.Visible=false;
				Pansch.Visible=true;
				panreseller.Visible=false;
			}
			else
			{
				DataGrid1.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			
		}

		public void Bindthedata_New()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string  sql;
			string cust_name="";
			/*coment by vikas 7.11.2012
			if(DropReSailer.SelectedItem.Text.Equals("All"))
			{
				//11.09.09 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
				//coment by vikas 5.11.2012 sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
			}
			else
			{
				//11.09.09 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
				//coment by vikas 5.11.2012 sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
			}*/

			if(DropReSailer.SelectedItem.Text.Equals("All"))
			{
				//11.09.09 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
				//coment by vikas 5.11.2012 sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
			}
			else
			{
				//11.09.09 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
				//coment by vikas 5.11.2012 sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
			}

			//**************
			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				//11.09.09 sql += " and Prod_Code='"+DropValue.Value+"'";
				sql += " and p.Prod_code ='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and p.Prod_ID='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and p.Prod_Name='"+arrname[0]+"' and p.Pack_Type='"+arrname[1]+"'";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and p.Prod_Name='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and p.Pack_Type='"+DropValue.Value+"'";
			/************Add by vikas 12.09.09**************************/
			else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
			{
				cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
				sql += " and c.cust_name='"+cust_name+"'";
			}
			/*************End*************************/
				
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"products");
			DataTable dtcustomer=ds.Tables["products"]; 
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridReseller.DataSource=dv;
			if(dv.Count!=0)
			{
				GridReseller.DataBind();
				GridReseller.Visible=true;
				DataGrid1.Visible=false;
				Pansch.Visible=false;
				panreseller.Visible=true;
			}
			else
			{
				GridReseller.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}
		
		/// <summary>
		/// this is used to view the report.
		/// </summary>
		protected void btnview_Click(object sender, System.EventArgs e)
		{
			try
			{

				if(RadBtnScheme.Checked==true)
				{
					DataGrid1.Visible=true;
					strorderby="s4,s11 ASC";
					Session["Column"]="s4,s11";
					Session["order"]="ASC";
					Bindthedata();
				}
				else if(RadBtnReSale.Checked==true)
				{
					DataGrid1.Visible=true;
					strorderby="prod_code ASC";
					Session["Column"]="prod_code";
					Session["order"]="ASC";
					Bindthedata_New();
				}
				else
				{
					MessageBox.Show("Please Select either Scheme or Resailer Redio Button");
				}
				
				CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Method:btnView, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}
		
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				//				DataGrid1.CurrentPageIndex=e.NewPageIndex;
				//				SqlConnection con;
				//				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//				con.Open ();
				//				SqlDataReader dtr;
				//				SqlCommand cmd=new SqlCommand("select s.prod_id s1,p.prod_name s2,p.pack_type s3,s.schemetype s4,s.discount s5,s.discounttype s6,s.eff_date s7 from products p,schemeupdation s where s.prod_id=p.prod_id order by s.prod_id",con);
				//				dtr=cmd.ExecuteReader();
				//				DataGrid1.DataSource=dtr;
				//				DataGrid1.DataBind();
				//GridSearch.CurrentPageIndex =e.NewPageIndex;
				//PartiesClass obj=new PartiesClass();
				//DataSet ds;
				//ds=obj.ShowCustomerInfo(txtCustID.Text,txtName.Text,txtPlace.Text );
				//GridSearch.DataSource=ds;
				//GridSearch.DataBind();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog(" Form :Schemelist.aspx.cs,Method  : DataGrid1_PageIndexChanged,  Exception: "+ex.Message+" , Userid :  "+ Session["Name"].ToString()   );		
			}
		}

		protected void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
		
		public void makingReport_New()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SchemeList.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			string cust_name="";
			/*******Coment by vikas 5.11.2012*****************
			if(DropReSailer.SelectedItem.Text.Equals("All"))
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
			else
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
			**********End**************/
			
			/*******Coment by vikas 5.11.2012*****************/
			if(DropReSailer.SelectedItem.Text.Equals("All"))
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
			else
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
			/**********End**************/

			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				sql += " and p.Prod_code ='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and p.Prod_ID='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and p.Prod_Name='"+arrname[0]+"' and p.Pack_Type='"+arrname[1]+"'";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and p.Prod_Name='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and p.Pack_Type='"+DropValue.Value+"'";
			
				/************Add by vikas 12.09.09**************************/
			else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
			{
				cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
				sql += " and c.cust_name='"+cust_name+"'";
			}
			/*************End*************************/
			//**************
			sql=sql+" order by "+""+Cache["strorderby"]+"";
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
			string des="-----------------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("===========================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SCHEME REPORT FROM "+txtDateFrom.Text+" TO "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("===========================================",des.Length));
			
			//Coment by vikas 5.11.2012 sw.WriteLine("+------------------------------+-----------------------------------+-------+----------+----------------+--------------+----------+");
			//Coment by vikas 5.11.2012 sw.WriteLine("|       Customer Name          |      Product Name With Pack       | Disc. | Dis.Type | Effective from | Effective to | Cust Type ");
			//Coment by vikas 5.11.2012 sw.WriteLine("+------------------------------+-----------------------------------+-------+----------+----------------+--------------+----------+");

			sw.WriteLine("+------------------------------+-----------+-----------------------------------+-------+----------+----------------+--------------+----------+");
			sw.WriteLine("|       Customer Name          | Prod.Code |      Product Name With Pack       | Disc. | Dis.Type | Effective from | Effective to | Cust Type ");
			sw.WriteLine("+------------------------------+-----------+-----------------------------------+-------+----------+----------------+--------------+----------+");
						//0123456789012345678901234567890 01234567890 01234567890123456789012345678901234 0123456 0123456789 0123456789012345 01234567890123 0123456789
			if(rdr.HasRows)
			{
				// info : to set the format the displaying string.
				//Coment by vikas 5.11.2012 info = " {0,-30:S} {1,-35:F} {2,-7:S} {3,-10:S} {4,-16:S} {5,-14:S} {6,-10:S} "; 
				info = " {0,-30:S} {1,-11:S} {2,-35:F} {3,-7:S} {4,-10:S} {5,-16:S} {6,-14:S} {7,-10:S} "; 
				while(rdr.Read())
				{
					sw.WriteLine(info,GenUtil.TrimLength(rdr["Cust_Name"].ToString(),30),
						rdr["prod_code"].ToString(),
						GenUtil.TrimLength(rdr["Product"].ToString(),35),
						rdr["Discount"].ToString(),
						rdr["Distype"].ToString(),
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Datefrom"].ToString())),
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Dateto"].ToString())),
						rdr["ctype"].ToString())
						;
				}
			}
			//Coment by vikas 5.11.2012 sw.WriteLine("+------------------------------+-----------------------------------+-------+----------+----------------+--------------+----------+");
			sw.WriteLine("+------------------------------+-----------+-----------------------------------+-------+----------+----------------+--------------+----------+");
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// this is used to make report for printing file.txt .
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SchemeList.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			//string strDate = "";
			//*mahesh**********sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname='Primary' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id";
			//**Mahesh sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname='Primary' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' >= cast(floor(cast(o.dateto as float)) as datetime)";
			
			/********Coment by vikas 5.11.2012*******************
			if(DropSchemeType.SelectedItem.Text.Equals("All"))
				sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
			else
				sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
			************End***************/
			
			/*************Add by vikas 7.11.2012*****************************/
			if(dropsalpur.SelectedIndex!=0)
			{
				if(dropsalpur.SelectedIndex==1)
				{
					if(DropSchemeType.SelectedItem.Text.Equals("All"))
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					else
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
				}
				else
				{
					if(DropSchemeType.SelectedItem.Text.Equals("All"))
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					else
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
				}
			}
			else
			{
				if(DropSchemeType.SelectedItem.Text.Equals("All"))
					//coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					//cast(o.discount as varchar)+' : '+o.discountType s5
				else
					//coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
			}
			/****************End***********************/

			//sql="select s.prod_id s1,p.prod_name s2,p.pack_type s3,s.schemetype s4,s.discount s5,s.discounttype s6,s.eff_date s7 from products p,schemeupdation s where s.prod_id=p.prod_id order by s.prod_id";
			//**************
			if(DropSearch.SelectedIndex==1 && DropValue.Value=="All")
				sql += " and Prod_Code='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and Prod_ID='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and Prod_Name='"+arrname[0]+"' and Pack_Type='"+arrname[1]+"'";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and Prod_Name='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and Pack_Type='"+DropValue.Value+"'";
			//**************
			
			/********Add by vikas 7.11.2012****************/
			if(dropGroup.SelectedIndex!=0)
			{
				sql+= " and group_name ='"+dropGroup.SelectedValue.ToString()+"'";
			}
				
			if(DropSchemeType.SelectedIndex!=0)
			{
				sql+= " and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"'";
			}
			/********End****************/


			sql=sql+" order by "+""+Cache["strorderby"]+"";
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
			string des="-----------------------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("===========================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SCHEME REPORT FROM "+txtDateFrom.Text+" TO "+txtDateTo.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("===========================================",des.Length));
			//sw.WriteLine("Scheme Type : "+DropSchemeType.SelectedItem.Text.ToString());
			//			sw.WriteLine("+----------+--------------------+----------+-----------+----------+-------------+---------------+");
			//			sw.WriteLine("|Product Id|  Product Name      |Pack Type |Scheme Type| Discount |Discount Type|Effective Date |");
			//			sw.WriteLine("+----------+--------------------+----------+-----------+----------+-------------+---------------+");
			
			//coment by vikas 5.11.2012 sw.WriteLine("+------+------------------------------+--------------------------+----+-------+----+------------------------------+----------+----------+");
			//coment by vikas 5.11.2012 sw.WriteLine("|ProdId|          Product Name        |        SchemeType        |Disc|onEvery|Free|       Scheme Product         |Date From |  Date To  ");
			//coment by vikas 5.11.2012 sw.WriteLine("+------+------------------------------+--------------------------+----+-------+----+------------------------------+----------+----------+");
													// 0123456 012345678901234567890123456789 01234567890123456789012345 0123 0123456 0123 012345678901234567890123456789 0123456789 0123456789
			sw.WriteLine("+------+---------+------------------------------+--------------------------+----+-------+----+------------------------------+----------+----------+----------+");
			sw.WriteLine("|ProdId|Prod.Code|          Product Name        |        SchemeType        |Disc|onEvery|Free|       Scheme Product         |  Group   |Date From |  Date To  ");
			sw.WriteLine("+------+---------+------------------------------+--------------------------+----+-------+----+------------------------------+----------+----------+----------+");
						//0123456 012345678 012345678901234567890123456789 01234567890123456789012345 0123 0123456 0123 012345678901234567890123456789 0123456789 0123456789 0123456789

			if(rdr.HasRows)
			{
				// info : to set the format the displaying string.
				//coment by vikas 5.11.2012 info = " {0,-6:S} {1,-30:F} {2,-26:S} {3,-4:S} {4,-7:S} {5,-4:S} {6,-30:S} {7,-10:S} {8,-10:S}"; 
				info = " {0,-6:S} {1,-9:S} {2,-30:F} {3,-26:S} {4,-4:S} {5,-7:S} {6,-4:S} {7,-30:S} {8,-10:S} {9,-10:S} {10,-10:S}"; 
				while(rdr.Read())
				{
					sw.WriteLine(info,rdr["s1"].ToString(),
						rdr["s11"].ToString(),
						GenUtil.TrimLength(rdr["s3"].ToString(),30),
						rdr["s4"].ToString(),
						rdr["s5"].ToString(),
						rdr["s6"].ToString(),
						rdr["s7"].ToString(),
						GenUtil.TrimLength(rdr["s10"].ToString(),30),
						GenUtil.TrimLength(rdr["s12"].ToString(),10),
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["s8"].ToString())),
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["s9"].ToString())));
				}
			}
			//coment by vikas 5.11.2012 sw.WriteLine("+------+------------------------------+--------------------------+----+-------+----+------------------------------+----------+----------+");
			sw.WriteLine("+------+---------+------------------------------+--------------------------+----+-------+----+------------------------------+----------+----------+----------+");
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SchemeList.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			/**********Coment by vikas 5.11.2012*****************
			if(DropSchemeType.SelectedItem.Text.Equals("All"))
				sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
			else
				sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
			**********End*****************/
			/*************Add by vikas 7.11.2012*****************************/
			if(dropsalpur.SelectedIndex!=0)
			{
				if(dropsalpur.SelectedIndex==1)
				{
					if(DropSchemeType.SelectedItem.Text.Equals("All"))
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					else
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
				}
				else
				{
					if(DropSchemeType.SelectedItem.Text.Equals("All"))
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					else
						//Coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
						sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,Per_Discount o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
				}
			}
			else
			{
				if(DropSchemeType.SelectedItem.Text.Equals("All"))
					//coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
					//cast(o.discount as varchar)+' : '+o.discountType s5
				else
					//coment by vikas 5.3.2013 sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,o.discount s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' <= cast(floor(cast(o.dateto as float)) as datetime)";
					sql="select o.prodid s1,p.prod_name+' : '+p.pack_type s3,o.schname s4,cast(o.discount as varchar)+' '+o.discountType s5,o.onevery s6,o.freepack s7,case when o.schname like'Primary%' then (select prod_name+' : '+pack_type from products where prod_id=o.schprodid) else '' end as s10,o.datefrom s8,o.dateto s9,p.Prod_Code s11,o.group_name s12 from products p,oilscheme o where o.prodid=p.prod_id and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";
			}
			/****************End***********************/

			if(DropSearch.SelectedIndex==1 && DropValue.Value=="All")
				sql += " and Prod_Code='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and Prod_ID='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and Prod_Name='"+arrname[0]+"' and Pack_Type='"+arrname[1]+"'";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and Prod_Name='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and Pack_Type='"+DropValue.Value+"'";
			//**************

			/********Add by vikas 7.11.2012****************/
			if(dropGroup.SelectedIndex!=0)
			{
				sql+= " and group_name ='"+dropGroup.SelectedValue.ToString()+"'";
			}
				
			if(DropSchemeType.SelectedIndex!=0)
			{
				sql+= " and o.schname='"+DropSchemeType.SelectedItem.Text.ToString()+"'";
			}
			/********End****************/

			sql=sql+" order by "+""+Cache["strorderby"]+"";
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text+"\tTo Date\t"+txtDateTo.Text);
			//sw.WriteLine("To Date\t"+txtDateTo.Text);
			//sw.WriteLine();
			//coment by vikas 5.11.2012 sw.WriteLine("Product Id\tProduct Name\tScheme Type\tDisc\tonEvery\tFree\tScheme Product\tDate From\tDate To");
			sw.WriteLine("Product Id\tProduct Code\tProduct Name\tScheme Type\tDisc\tonEvery\tFree\tScheme Product\tGroup Name\tDate From\tDate To");
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					sw.WriteLine(rdr["s1"].ToString()+"\t"+
						rdr["s11"].ToString()+"\t"+        //add by vikas 5.11.2012
						rdr["s3"].ToString()+"\t"+
						rdr["s4"].ToString()+"\t"+
						rdr["s5"].ToString()+"\t"+
						rdr["s6"].ToString()+"\t"+
						rdr["s7"].ToString()+"\t"+
						rdr["s10"].ToString()+"\t"+
						rdr["s12"].ToString()+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["s8"].ToString()))+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["s9"].ToString())));

				}
			}
			rdr.Close();
			sw.Close();
		}

		public void ConvertToExcel_New()
		{
			string cust_name="";
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SchemeList.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			
			/**********Coment by vikas 5.11.2012*****************
			if(DropReSailer.SelectedItem.Text.Equals("All"))
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
			else
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
			**************End*******************************/

			/**********Add by vikas 5.11.2012*****************/
			if(DropReSailer.SelectedItem.Text.Equals("All"))
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime)";
			else
				sql="select Cust_Name,ctype,Prod_Name +':'+ Pack_Type Product,Discount,Distype ,datefrom,dateto,p.prod_code from customer c,foe f,products p where f.custid=c.cust_id and f.prodid=p.prod_id and cast(floor(cast(f.datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and  '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim())+"' <= cast(floor(cast(f.dateto as float)) as datetime) and f.ctype= '"+DropReSailer.SelectedItem.Text+"'";
			/**************End*******************************/

			if(DropSearch.SelectedIndex==1 && DropValue.Value!="All")
				sql += " and p.Prod_code ='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==2 && DropValue.Value!="All")
				sql += " and p.Prod_ID='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==3 && DropValue.Value!="All")
			{
				string name = DropValue.Value;
				string[] arrname = name.Split(new char[] {':'},name.Length);
				sql += " and p.Prod_Name='"+arrname[0]+"' and p.Pack_Type='"+arrname[1]+"'";
			}
			else if(DropSearch.SelectedIndex==4 && DropValue.Value!="All")
				sql += " and p.Prod_Name='"+DropValue.Value+"'";
			else if(DropSearch.SelectedIndex==5 && DropValue.Value!="All")
				sql += " and p.Pack_Type='"+DropValue.Value+"'";
			/************Add by vikas 12.09.09**************************/
			else if(DropSearch.SelectedIndex==6 && DropValue.Value!="All")
			{
				cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
				sql += " and c.cust_name='"+cust_name+"'";
			}
			/*************End*************************/
			//**************
			sql=sql+" order by "+""+Cache["strorderby"]+"";
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text+"\tTo Date\t"+txtDateTo.Text);
			
			//11.09.09 coment by vikas sw.WriteLine("Product Id\tProduct Name\tScheme Type\tDisc\tonEvery\tFree\tScheme Product\tDate From\tDate To");
			//coment by vikas 5.11.2012 sw.WriteLine("Customer Name\t Product Name With Pack\tDisc.\tDis.Type\tEffective from\tEffective to\tCust Type ");
			sw.WriteLine("Customer Name\tProduct Name\t Product Name With Pack\tDisc.\tDis.Type\tEffective from\tEffective to\tCust Type ");

			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					sw.WriteLine(rdr["Cust_Name"].ToString()+"\t"+
						rdr["Prod_code"].ToString()+"\t"+             //add by vikas 5.11.2012
						rdr["Product"].ToString()+"\t"+
						rdr["Discount"].ToString()+"\t"+
						rdr["Distype"].ToString()+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Datefrom"].ToString()))+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Dateto"].ToString()))+"\t"+
						rdr["ctype"].ToString())
						;

				}
			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// this is used to print the report.
		/// </summary>
		protected void btnprint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				if(RadBtnScheme.Checked==true)
				{
					makingReport();
				}
				else
				{
					makingReport_New();
				}
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
					CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Schemelist Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SchemeList.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Schemelist Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Schemelist Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Schemelist Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:Schemelist.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Schemelist Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file SchemeList.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//11.09.09 Coment by vikas if(DataGrid1.Visible==true)
				if(DataGrid1.Visible==true || GridReseller.Visible==true )
				{
					if(RadBtnScheme.Checked==true)
					{
						ConvertToExcel();
					}
					else
					{
						ConvertToExcel_New();
					}
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SchemeList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Scheme List Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:SchemeList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Scheme List Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
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
				string strProdCode="",strProdID="",strProdName="",strProdWithPack="",strPackType="",strCustName="";
				strProdCode = "select distinct Prod_Code from vw_PriceList order by Prod_Code";
				strProdID = "select distinct Prod_ID from vw_PriceList order by Prod_ID";
				strProdName = "select distinct Prod_Name+':'+Pack_Type as Prod_Name from vw_PriceList order by Prod_Name";
				strProdWithPack= "select distinct Prod_Name from vw_PriceList order by Prod_Name";
				strPackType= "select distinct Pack_Type from vw_PriceList order by Pack_Type";
				
				strCustName="select distinct c.cust_name,c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name";               //Add by vikas 12.09.09
				
				//Coment by vikas 12.09.09 string[] arrStr = {strProdCode,strProdID,strProdName,strProdWithPack,strPackType};
				string[] arrStr = {strProdCode,strProdID,strProdName,strProdWithPack,strPackType,strCustName};
				//coment by vikas 12.09.09 HtmlInputHidden[] arrCust = {tempProdCode,tempProdID,tempProdName,tempProdWithName,tempPackType};
				HtmlInputHidden[] arrCust = {tempProdCode,tempProdID,tempProdName,tempProdWithName,tempPackType,tempCustName};
				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							if(i==5)
							{
								arrCust[i].Value+=rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString()+",";
							}
							else
							{
								arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							}
							//coment by vikas 12.09.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
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

		protected void RadBtnScheme_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadBtnScheme.Checked==true)
			{
				DropSchemeType.Visible=true;
				DropReSailer.Visible=false;
				lblDroptype.Text="Scheme Type";
				dropsalpur.Visible=true;
			}
			else
			{
				DropSchemeType.Visible=false;
				DropReSailer.Visible=true;
				dropsalpur.Visible=false;
				lblDroptype.Text="Re-Seller";

			}
		}

		protected void RadBtnReSale_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadBtnScheme.Checked==true)
			{
				DropSchemeType.Visible=true;
				DropReSailer.Visible=false;
				lblDroptype.Text="Scheme Type";
				dropsalpur.Visible=true;
			}
			else
			{
				DropSchemeType.Visible=false;
				DropReSailer.Visible=true;
				lblDroptype.Text="Re-Seller";
				dropsalpur.Visible=false;

			}
		}

		protected void dropsalpur_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SqlDataReader rdr=null;
			string sql="";
			if(dropsalpur.SelectedIndex==1)
			{
				DropSchemeType.Items.Clear();
				DropSchemeType.Items.Add("Select");
				sql="select distinct schname from oilscheme order by schname";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					DropSchemeType.Items.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();
			}
			else if(dropsalpur.SelectedIndex==2)
			{
				DropSchemeType.Items.Clear();
				DropSchemeType.Items.Add("Select");
				sql="select distinct schname from Per_Discount where schname not in (select distinct schname from oilscheme ) order by schname";
				dbobj.SelectQuery(sql,ref rdr);
				while(rdr.Read())
				{
					DropSchemeType.Items.Add(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				
			}

		}
	}
}