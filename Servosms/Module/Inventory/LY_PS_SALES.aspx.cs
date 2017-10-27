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
	/// Summary description for LY_PS_SALES.
	/// </summary>
	public partial class LY_PS_SALES : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtID;
		public static int View=0;
		public static string ColumnName = "";
	
		/// <summary>
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
				string pass;
				pass=(Session["User_Name"].ToString());
			}
			catch
			{
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				ColumnName="";
				View=0;
				//GetNextID();
				DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="4";
				string SubModule="11";
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
				if(Add_Flag=="0" && Edit_Flag=="0" && View_flag=="0")
				{
					//string msg="UnAthourized Visit to Price Updation Page";
					//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				if(Add_Flag=="0")
				{
					btnSubmit.Enabled=false;
				}
				//if(Add_Flag =="0" && Edit_Flag == "0")
				//	Btnsubmit1.Enabled = false; 
				#endregion
				
				#region Testing Purpose
				InventoryClass obj = new InventoryClass();
				SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				ArrayList arrHeaderName = new ArrayList();
				ArrayList arrColName = new ArrayList();
				ArrayList arrRemName = new ArrayList();
				object ob=null;
				dbobj.ExecProc(DBOperations.OprType.Insert,"ProUpdateCustomerType",ref ob,"@Cust_ID","");
				SqlDataReader rdr = null;
				SqlCommand cmd;
				//rdr = obj.GetRecordSet("select distinct case when customertypename like 'oe%' then 'Oe' else customertypename end as customertypename from customertype order by customertypename");
				rdr = obj.GetRecordSet("select distinct custtype,custtypeid from tempcustomertype order by custtypeid");
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						string name = rdr.GetValue(0).ToString().ToLower();
						name = name.Replace("/","");
						name = name.Replace(" ","");
						name = name.Replace("-","");
						if(rdr.GetValue(0).ToString().ToLower().StartsWith("ro"))
						{
							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"lube");
							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"2t4t");
							arrColName.Add(name+"lube");
							arrColName.Add(name+"2t4t");
						}
						else if(rdr.GetValue(0).ToString().ToLower().StartsWith("bazar") || rdr.GetValue(0).ToString().ToLower().StartsWith("bazzar"))
						{
							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"lube");
							//arrColName.Add(rdr.GetValue(0).ToString().ToLower()+"2t4t");
							arrColName.Add(name+"lube");
							arrColName.Add(name+"2t4t");
						}
						else
							//arrColName.Add(rdr.GetValue(0).ToString().ToLower());
							arrColName.Add(name);
					}
				}
				rdr.Close();
				
				rdr = obj.GetRecordSet("select * from ly_ps_sale");
				int n = rdr.FieldCount;
				//ColumnName="ly_ps_sales,month,tot_pur,pur_foc,gen_oil,grease,";
				//for(int p=0,m=7;m<n;m++,p++)
				for(int p=0,m=9;m<n;m++,p++)
				{
					arrHeaderName.Add(rdr.GetName(m));
					//ColumnName+=rdr.GetName(m)+",";
				}
				//ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
				rdr.Close();
				if(arrColName.Count==arrHeaderName.Count)
				{
					rdr = obj.GetRecordSet("select * from ly_ps_sale");
					n = rdr.FieldCount;
					ColumnName="ly_ps_sales,discription,month,tot_pur,pur_foc,gen_oil,grease,total_purchase,";
					for(int p=0,m=9;m<n;m++,p++)
					{
						ColumnName+=rdr.GetName(m)+",";
					}
					ColumnName+="total_sales,";
					ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
					rdr.Close();
					return;
				}
				
				if(arrColName.Count>=arrHeaderName.Count)
				{
					for(int r=0;r<arrColName.Count;r++)
					{
						arrRemName.Add(arrColName[r]);
					}
					for(int q=0;q<arrHeaderName.Count;q++)
					{
						arrRemName.Remove(arrHeaderName[q]);
					}
					if(arrRemName.Count>0)
					{
						for(int k=0;k<arrRemName.Count;k++)
						{
							con.Open();
							string name = arrRemName[k].ToString();
							name = name.Replace("/","");
							name = name.Replace(" ","");
							name = name.Replace("-","");
							string str = "alter table ly_ps_sale add "+name+" float";
							cmd = new SqlCommand(str,con);
							cmd.ExecuteNonQuery();
							cmd.Dispose();
							con.Close();
						}
					}
				}
				else
				{
					for(int q=0;q<arrHeaderName.Count;q++)
					{
						arrRemName.Add(arrHeaderName[q]);
					}
					for(int q=0;q<arrColName.Count;q++)
					{
						arrRemName.Remove(arrColName[q]);
					}
					if(arrRemName.Count>0)
					{
						for(int k=0;k<arrRemName.Count;k++)
						{
							con.Open();
							string name = arrRemName[k].ToString();
							name = name.Replace("/","");
							name = name.Replace(" ","");
							name = name.Replace("-","");
							string str = "alter table ly_ps_sale drop column "+name+"";
							cmd = new SqlCommand(str,con);
							cmd.ExecuteNonQuery();
							cmd.Dispose();
							con.Close();
						}
					}					
				}
								
				rdr = obj.GetRecordSet("select * from ly_ps_sale");
				n = rdr.FieldCount;
				ColumnName="ly_ps_sales,discription,month,tot_pur,pur_foc,gen_oil,grease,total_purchase,";
				for(int p=0,m=9;m<n;m++,p++)
				{
					ColumnName+=rdr.GetName(m)+",";
				}
				ColumnName+="total_sales,";
				ColumnName=ColumnName.Substring(0,ColumnName.Length-1);
				rdr.Close();
				#endregion
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
		/// This method is used to show the data on form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			txtDiscription.Text="";
			if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
			{
				MessageBox.Show("Year Can Not Be Same");
				View=0;
				return;
			}
			else if(DropYearFrom.SelectedIndex+1==DropYearTo.SelectedIndex)
			{
				View = 1;
			}
			else
			{
				MessageBox.Show("Invalid Year Selection");
				View=0;
				return;
			}
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select discription from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'");
			if(rdr.Read())
			{
				txtDiscription.Text=rdr["Discription"].ToString();
			}
			else
			{
				txtDiscription.Text="";
			}
		}

		/// <summary>
		/// This method is used to save the record and add or remeve the column in Ly_Ps_sales table dynamically.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(View==1)
				{
					InventoryClass obj=new InventoryClass(); 
					SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					SqlCommand cmd;
					SqlDataReader rdr =null;
					string str = "";
					int count = int.Parse(Request.Params.Get("M"));
					int col_count = int.Parse(Request.Params.Get("col"));
					if(RadDetails.Checked)
					{
						con.Open();
						str = "delete from Ly_Ps_Sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'";
						cmd = new SqlCommand(str,con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						con.Close();
						//int j=0,m=1;
						//GetNextID();
						//for(int i=0;i<count;i++)
						for(int i=0;i<count;i++)
						{
							string colname="";
							//j=1;
							//obj.foid=NextID.ToString();
							colname = "'"+System.Convert.ToString(DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text)+"',";
							colname += "'"+txtDiscription.Text+"',";
							colname+="'"+Request.Params.Get("txtTotalPurchase"+i)+"',";
							colname+="'"+Request.Params.Get("txtTotalSales"+i)+"',";
							colname += "'"+Request.Params.Get("Month"+i)+"',";
							for(int k=1;k<col_count-1;k++)
							{
								if(k!=5)
									colname+="'"+Request.Params.Get("txt"+i+"To"+k)+"',";
							}
							colname=colname.Substring(0,colname.Length-1);
							con.Open();
							str = "insert into ly_ps_sale values("+colname+")";
							cmd = new SqlCommand(str,con);
							cmd.ExecuteNonQuery();
							cmd.Dispose();
							con.Close();
						}
					}
					else
					{
						int Flag=0;
						rdr = obj.GetRecordSet("select * from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'");
						if(rdr.HasRows)
						{
							Flag=1;
						}
						rdr.Close();
						if(Flag==1)
						{
							for(int i=0;i<count;i++)
							{
								con.Open();
								str = "update ly_ps_sale set discription='"+txtDiscription.Text+"', total_purchase='"+Request.Params.Get("txtTotalPurchase"+i)+"', total_sales='"+Request.Params.Get("txtTotalSales"+i)+"' where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"' and Month='"+Request.Params.Get("Month"+i)+"'";
								cmd = new SqlCommand(str,con);
								cmd.ExecuteNonQuery();
								cmd.Dispose();
								con.Close();
							}
						}
						else
						{
							for(int i=0;i<count;i++)
							{
								con.Open();
								str = "insert into ly_ps_sale(ly_ps_sales,Month,discription,total_purchase,total_sales) values('"+System.Convert.ToString(DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text)+"','"+Request.Params.Get("Month"+i)+"','"+txtDiscription.Text+"','"+Request.Params.Get("txtTotalPurchase"+i)+"','"+Request.Params.Get("txtTotalSales"+i)+"')";
								cmd = new SqlCommand(str,con);
								cmd.ExecuteNonQuery();
								cmd.Dispose();
								con.Close();
							}
						}
					}
					MessageBox.Show("Data Inserted Successfully");
					//GetNextID();
					CreateLogFiles.ErrorLog("Form:LY_PS_SALES.aspx,Method:update().  Data Inserted, User_ID: "+Session["User_Name"].ToString());
				}
				else
				{
					MessageBox.Show("Please Click The View Button First");
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LY_PS_SALES.aspx,Method:update().   EXCEPTION " +ex.Message +"  User_ID : "+ Session["User_Name"].ToString());
			}
		}

		/// <summary>
		/// This method is used to fatch the next id from Ly_Ps_sales table for save the record.
		/// </summary>
		public void GetNextID()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string str="select max(LY_PS_SALES)+1 from LY_PS_SALES";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.Read())
			{
				txtID.Text=SqlDtr.GetValue(0).ToString();
			}
			if(txtID.Text=="")
				txtID.Text="1";
		}
	}
}
