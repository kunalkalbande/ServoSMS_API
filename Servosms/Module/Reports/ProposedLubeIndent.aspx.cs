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
using System.Data.OleDb;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for LubeIndent.
	/// </summary>
	public partial class ProposedLubeIndent : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		public int j=0;
		SqlConnection con1;
		OleDbConnection con;
		OleDbDataReader rdr=null;
		SqlDataReader rdr1=null;
		protected System.Web.UI.WebControls.Button btnShowData;
		SqlCommand cmd;
	
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
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				DataGrid1.Visible=false;
				getdata();
				DropYear.SelectedIndex=DropYear.Items.IndexOf(DropYear.Items.FindByValue(DateTime.Now.Year.ToString()));
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="33";
				string[,] Priv=(string[,]) Session["Privileges"];
								
				for(i=0;i<Priv.GetLength(0);i++)
				{
					if(Priv[i,0] == Module && Priv[i,1] == SubModule)
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
			}
		}

		/// <summary>
		/// This method is used to fatch the record from excel sheet with the help of OLEDB connection
		/// this excel sheet given in C:\Servosms_ExcelFile\Import location
		/// </summary>
		public void getdata()
		{
			try
			{
				string connstring1 = "Provider=Microsoft.Jet.OLEDB.4.0;"+ "Data Source=c:/Servosms_ExcelFile/Import/Lube_Indent.xls;"+"Extended Properties=Excel 8.0;";
				con =new OleDbConnection(connstring1);
				con1 =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				DataGrid1.DataSource=null;
				//**************************
				if(DropMonth.SelectedIndex==0)
				{
					OleDbDataAdapter mycommand = new OleDbDataAdapter("select * from [Indent Lube$]",connstring1);
					DataSet mydataset = new DataSet();
					mycommand.Fill(mydataset,"ExcelInfo");
					DataGrid1.DataSource=mydataset.Tables["ExcelInfo"].DefaultView;
					DataGrid1.DataBind();
					DataGrid1.Visible=true;
				}
				else
				{
					con1.Open();
					dbobj.SelectQuery("select * from indent_lube where ssaid="+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"",ref rdr1);
					if(rdr1.HasRows)
					{
						//if(rdr1.Read())
						//{
						DataGrid1.DataSource=rdr1;
						DataGrid1.DataBind();
						DataGrid1.Visible=true;
						//}
					}
					else
					{
						OleDbDataAdapter mycommand = new OleDbDataAdapter("select * from [Indent Lube$]",connstring1);
						DataSet mydataset = new DataSet();
						mycommand.Fill(mydataset,"ExcelInfo");
						DataGrid1.DataSource=mydataset.Tables["ExcelInfo"].DefaultView;
						DataGrid1.DataBind();
						DataGrid1.Visible=true;
					}
					con1.Close();
					rdr1.Close();
				}
				//*************************
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				MessageBox.Show("Please Store Your Excel File In C:/ServoSMSExcelFile/Import");
				DataGrid1.Visible=false;
				//Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
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
		/// This method is used to Save the data in Indent_Lube table, if data is already exist then updated the data.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			if(DataGrid1.Visible==true)
			{
				string connstring1 = "Provider=Microsoft.Jet.OLEDB.4.0;"+ "Data Source=c:/Servosms_ExcelFile/Import/Lube_Indent.xls;"+"Extended Properties=Excel 8.0;";
				con =new OleDbConnection(connstring1);
				con1 =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				int i=0;
				dbobj.SelectQuery("select * from indent_lube where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"'",ref rdr1);
				if(rdr1.HasRows)
				{
					while(rdr1.Read())
					{
						con1.Open();
						string s=Request.Params.Get("txt"+i);
						if(Request.Params.Get("txt"+i)!="")
							cmd=new SqlCommand("update indent_lube set indent='"+Request.Params.Get("txt"+i)+"' where prodcode='"+rdr1["prodcode"].ToString()+"' and skunamewithpack='"+rdr1["skunamewithpack"].ToString()+"' and ssaid='"+rdr1["ssaid"].ToString()+"'",con1);
						else
							cmd=new SqlCommand("update indent_lube set indent=0 where prodcode='"+rdr1["prodcode"].ToString()+"' and skunamewithpack='"+rdr1["skunamewithpack"].ToString()+"' and ssaid='"+rdr1["ssaid"].ToString()+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						i++;
					}
					MessageBox.Show("Record Updated Successfully");
					//return;
				}
				else
				{
				
					con.Open();
					//SqlCommand cmd;
					OleDbCommand mycommand = new OleDbCommand("select * from [Indent Lube$]",con);
					rdr=mycommand.ExecuteReader();
					while(rdr.Read())
					{
						con1.Open();
						string s=Request.Params.Get("txt"+i);
						if(Request.Params.Get("txt"+i)=="")
							cmd=new SqlCommand("insert into indent_lube(ssaid,rse,supex,retailmpso,skutype,packcode,packqty,prodcode,skunamewithpack,indent) values("+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+",'"+rdr.GetValue(0).ToString()+"','"+rdr.GetValue(1).ToString()+"','"+rdr.GetValue(2).ToString()+"','"+rdr.GetValue(3).ToString()+"','"+rdr.GetValue(4).ToString().Trim()+"','"+rdr.GetValue(5).ToString()+"','"+rdr.GetValue(6).ToString()+"','"+rdr.GetValue(7).ToString().Trim()+"','0')",con1);
						else
							cmd=new SqlCommand("insert into indent_lube(ssaid,rse,supex,retailmpso,skutype,packcode,packqty,prodcode,skunamewithpack,indent) values("+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+",'"+rdr.GetValue(0).ToString()+"','"+rdr.GetValue(1).ToString()+"','"+rdr.GetValue(2).ToString()+"','"+rdr.GetValue(3).ToString()+"','"+rdr.GetValue(4).ToString().Trim()+"','"+rdr.GetValue(5).ToString()+"','"+rdr.GetValue(6).ToString()+"','"+rdr.GetValue(7).ToString().Trim()+"',"+Request.Params.Get("txt"+i)+")",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						i++;
					}
					rdr.Close();
					MessageBox.Show("Record Save Successfully");
				}
				rdr1.Close();
				con.Close();
				getdata();
			}
			else
			{
				MessageBox.Show("Data Not Available");
			}
		}

		/// <summary>
		/// This method is used to call the getdata() funtion according to select month from dropdownlist.
		/// </summary>
		protected void DropMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			getdata();
		}

		/// <summary>
		/// This method is used to first get the indent from indent_lube table according to given SSAID, 
		/// product code and sku name(product name) and return the indent.
		/// </summary>
		public string GetIndent(string cd,string skuname)
		{
			SqlDataReader rdr=null;
			string str="";
			if(DropYear.SelectedIndex!=0 && DropMonth.SelectedIndex!=0)
			{
				dbobj.SelectQuery("select indent from indent_lube where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and prodcode='"+cd.Trim()+"' and skunamewithpack='"+skuname.Trim()+"'",ref rdr);
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						str = rdr["indent"].ToString();
					}
				}
				dbobj.Dispose();
			}
			return str;
		}
	}
}