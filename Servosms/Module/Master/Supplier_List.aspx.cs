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
using Servosms.Sysitem.Classes ;
using RMG;
using DBOperations;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for Supplier_List.
	/// </summary>
	public partial class Supplier_List : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
		string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";

		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user
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
				CreateLogFiles.ErrorLog("Form:Supplier_List.aspx,Method:page_load,Class:PartiesClass.cs "+ ex.Message+" EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				#region Check Privileges
				checkPrivileges(); 
				
				if(View_flag=="0")
				{
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				#endregion
			}
		}

		/// <summary>
		/// This method is used check rhe permission for particular user.
		/// </summary>
		public void checkPrivileges()
		{
			int i;
			string Module="3";
			string SubModule="12";
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
			this.GridSearch.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.GridSearch_PageIndexChanged);
			this.GridSearch.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.GridSearch_DeleteCommand);

		}
		#endregion

		/// <summary>
		/// This method is used to bindind the datagrid.
		/// </summary>
		public void initGrid()
		{
			try
			{
				PartiesClass obj=new PartiesClass();
				DataSet ds;
				ds=obj.ShowSupplierInfo(txtSuppID.Text ,txtName.Text,txtPlace.Text );
				
				//******
				DataTable dt=ds.Tables[0];
				DataView dv=new DataView(dt);
				dv.Sort=System.Convert.ToString(Cache["strorderby"]);
				//******
				if(dv.Count>0)
					//		if(ds.Tables[0].Rows.Count>0)
				{
					GridSearch.DataSource=dv;
					GridSearch.DataBind();
					GridSearch.Visible=true;
				}
				else
				{
					MessageBox.Show("Vendor not Found");
					GridSearch.Visible=false;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Supplier_List.aspx,Method:initGrid(),Class:PartiesClass.cs.  EXCEPTION: "+ ex.Message+"  User_ID: "+uid);
			}
		}
		
		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of datagridheader.
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
				Cache["strorderby"]=strorderby;
				initGrid();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This method is used to search the record according to name, vendor ID and place with the 
		/// help of initGrid() function.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			GridSearch.CurrentPageIndex =0; 
			Cache["strorderby"]="Supp_ID ASC";
			Session["Column"]="Supp_ID";
			Session["order"]="ASC";
			initGrid();
		}
		
		/// <summary>
		/// This is used to go on the selected page index.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void GridSearch_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				GridSearch.CurrentPageIndex =e.NewPageIndex ; 
				initGrid();
				//				PartiesClass obj=new PartiesClass();
				//				DataSet ds;
				//				ds=obj.ShowSupplierInfo(txtSuppID.Text ,txtName.Text,txtPlace.Text );
				//				GridSearch.DataSource=ds;
				//				GridSearch.DataBind();
				CreateLogFiles.ErrorLog("Form:Supplier_List.aspx,Method:GridSearch_PageIndexChanged,Class:PartiesClass.cs "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Supplier_List.aspx,Method:GridSearch_PageIndexChanged,Class:PartiesClass.cs EXCEPTION "+ ex.Message+"  User_ID: "+uid);
			}
		}
		
		/// <summary>
		/// This method is used to delete the particular record from the supplier table and also delete the 
		/// record from ledger Master table.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void GridSearch_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				checkPrivileges();
				if(Del_Flag =="0")
				{
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				int Count=0;
				SqlDataReader rdr=null;
				dbobj.SelectQuery("select count(*) from AccountsLedgerTable where Ledger_ID='"+e.Item.Cells[0].Text+"'",ref rdr);
				if(rdr.Read())
				{
					Count=int.Parse(rdr.GetValue(0).ToString());
				}
				if(Count>1)
				{
					MessageBox.Show("Please Remove The All Transaction Concerning Vendor");
					return;
				}
				SqlConnection sqlConn=new SqlConnection();
				string strCon=System.Configuration.ConfigurationSettings.AppSettings["Servosms"];
				SqlCommand sqlCmd=new SqlCommand();
				sqlCmd.CommandText="Delete from Supplier Where Supp_ID='"+e.Item.Cells[1].Text+"'";
				sqlConn.ConnectionString=strCon;
				sqlConn.Open();
				sqlCmd.Connection=sqlConn;
				sqlCmd.ExecuteNonQuery();
				//************
				sqlConn.Close();
				sqlCmd.Dispose();
				sqlCmd.CommandText="Delete from Ledger_Master Where Ledger_ID='"+e.Item.Cells[0].Text+"'";
				sqlConn.ConnectionString=strCon;
				sqlConn.Open();
				sqlCmd.Connection=sqlConn;
				sqlCmd.ExecuteNonQuery();
				sqlConn.Close();
				sqlCmd.Dispose();
				//************
				MessageBox.Show("Vendor Deleted");
				CreateLogFiles.ErrorLog("Form:Supplier_List.aspx,Method:GridSearch_DeleteCommand"+"  Supplier ID  "+e.Item.Cells[1].Text+"  IS DELETED   "+" USERID      "+uid );
				initGrid();
				Response.Redirect("Supplier_List.aspx",false);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Supplier_List.aspx,Method:GridSearch_DeleteCommand,Class:PartiesClass.cs  EXCEPTION: "+ ex.Message+"  User_ID: "+uid);
			}
		}

		protected void GridSearch_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}