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
	/// Summary description for ServoSadbhavnalist.
	/// </summary>
	public partial class ServoSadbhavnalist : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtanbazzar;
		protected System.Web.UI.WebControls.TextBox txtanro;
		protected System.Web.UI.WebControls.TextBox txtanoe;
		protected System.Web.UI.WebControls.TextBox txtanfleet;
		protected System.Web.UI.WebControls.TextBox txtanibp;
		protected System.Web.UI.WebControls.TextBox txtbazzar;
		protected System.Web.UI.WebControls.TextBox txtro;
		protected System.Web.UI.WebControls.TextBox txtoe;
		protected System.Web.UI.WebControls.TextBox txtfleet;
		protected System.Web.UI.WebControls.TextBox txtibp;
		protected System.Web.UI.WebControls.Panel Panel9;
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
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ServoSadbhavnalist.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				try
				{
					//Fill_Cust_Type(); coment by vikas sharma 23.05.09 
					DataGrid1.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="39";
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
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:Page_Load,  User: "+uid);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:Page_Load    EXCEPTION: "+ ex.Message+ ". User: "+uid);
				}
			}
		}

		public void Fill_Cust_Type()
		{
			PetrolPumpClass obj1=new PetrolPumpClass();
			string str="select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
			SqlDataReader sdtr1=null;
			droptype.Items.Clear();
			droptype.Items.Add("All");
			sdtr1=obj1.GetRecordSet(str);
			while(sdtr1.Read())
			{
				droptype.Items.Add(sdtr1.GetValue(0).ToString().Trim());
			}
			sdtr1.Close();
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
		/// This method is used to view the Purchase Book report with the help of Bindthedata() function and
		/// set the column name with ascending order in Session variable.
		/// </summary>
		protected void btnview_Click(object sender, System.EventArgs e)
		{
			try
			{
				BindtheData();
				strorderbyAshok="r2 ASC";
				Session["ColumnAshok"]="r2";
				Session["orderAshok"]="ASC";
				BindthedataAshok();

				strorderbyBhind="r2 ASC";
				Session["ColumnBhind"]="r2";
				Session["orderBhind"]="ASC";
				BindthedataBhind();

				strorderbyDatia="r2 ASC";
				Session["ColumnDatia"]="r2";
				Session["orderDatia"]="ASC";
				BindthedataDatia();

				strorderbyGuna="r2 ASC";
				Session["ColumnGuna"]="r2";
				Session["orderGuna"]="ASC";
				BindthedataGuna();

				strorderbyGwalior="r2 ASC";
				Session["ColumnGwalior"]="r2";
				Session["orderGwalior"]="ASC";
				BindthedataGwalior();

				strorderbyMorena="r2 ASC";
				Session["ColumnMorena"]="r2";
				Session["orderMorena"]="ASC";
				BindthedataMorena();

				strorderbySheopur="r2 ASC";
				Session["ColumnSheopur"]="r2";
				Session["orderSheopur"]="ASC";
				BindthedataSheopur();

				strorderbyShivpuri="r2 ASC";
				Session["ColumnShivpuri"]="r2";
				Session["orderShivpuri"]="ASC";
				BindthedataShivpuri();
				//************************************
				CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:btnView, userid : "+uid );
			}

			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

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
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			// Comment By Vikas Sharma 07.04.09 if(droptype.SelectedItem.Text.Equals("Both"))
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//Comment By Vikas Sharma 07.04.09 sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and( cust_type='Bazzar' or cust_type like('Ro%'))";
				//16.07.09 sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar' and( cust_type='Bazzar' or cust_type like('Ro%')or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/* // Comment By Vikas Sharma 07.04.09
			 else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and  cust_type like('Ro%')"; 
			}*/
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();
			
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel1.Visible=false;
				DataGrid1.Visible=false;
			}
			dv.Sort=strorderbyAshok;
			Cache["strorderbyAshok"]=strorderbyAshok;
			DataGrid1.DataSource=dv;
			DataGrid1.DataBind();
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataGwalior()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type='"+type+"'";
			
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and  cust_type like('Ro%')"; 
			}*/
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel5.Visible=false;
				Datagrid5.Visible=false;
			}
			dv.Sort=strorderbyGwalior;
			Cache["strorderbyGwalior"]=strorderbyGwalior;
			Datagrid5.DataSource=dv;
			Datagrid5.DataBind();
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataBhind()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type='"+type+"'";
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}


			/*// Comment By Vikas Sharma 07.04.09 
			 if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and  cust_type like('Ro%')"; 
			}*/
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel2.Visible=false;
				Datagrid2.Visible=false;
			}
			dv.Sort=strorderbyBhind;
			Cache["strorderbyBhind"]=strorderbyBhind;
			Datagrid2.DataSource=dv;
			Datagrid2.DataBind();
			sqlcon.Dispose();
		}
		
		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataDatia()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type='"+type+"'";
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and  cust_type like('Ro%')"; 
			}*/
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel3.Visible=false;
				Datagrid3.Visible=false;
			}
			dv.Sort=strorderbyDatia;
			Cache["strorderbyDatia"]=strorderbyDatia;
			Datagrid3.DataSource=dv;
			Datagrid3.DataBind();
			sqlcon.Dispose();
		}
		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataGuna()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type='"+type+"'";
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09 
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and  cust_type like('Ro%')"; 
			}*/
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel4.Visible=false;
				Datagrid4.Visible=false;
			}
			dv.Sort=strorderbyGuna;
			Cache["strorderbyGuna"]=strorderbyGuna;
			Datagrid4.DataSource=dv;
			Datagrid4.DataBind();
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataMorena()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type='"+type+"'";
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment by Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and  cust_type like('Ro%')"; 
			}*/
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel6.Visible=false;
				Datagrid6.Visible=false;
			}
			dv.Sort=strorderbyMorena;
			Cache["strorderbyMorena"]=strorderbyMorena;
			Datagrid6.DataSource=dv;
			Datagrid6.DataBind();
			sqlcon.Dispose();
		}
		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataShivpuri()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type='"+type+"'";
			
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*//
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and  cust_type like('Ro%')"; 
			}*/

			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel8.Visible=false;
				Datagrid8.Visible=false;
			}
			dv.Sort=strorderbyShivpuri;
			Cache["strorderbyShivpuri"]=strorderbyShivpuri;
			Datagrid8.DataSource=dv;
			Datagrid8.DataBind();
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindthedataSheopur()
		{
			//			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//			string  sql=sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type='"+type+"'";
			string  sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*//Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and  cust_type like('Ro%')"; 
			}*/
			
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			DataView dv=new DataView(dtcustomer);
			if(dv.Count==0)
			{
				Panel7.Visible=false;
				Datagrid7.Visible=false;
			}
			dv.Sort=strorderbySheopur;
			Cache["strorderbySheopur"]=strorderbySheopur;
			Datagrid7.DataSource=dv;
			Datagrid7.DataBind();
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to binding the datagrid.
		/// </summary>
		public void BindtheData()
		{
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
		}

		/// <summary>
		/// Prepares the report file ServoSadbhavnalistReport.txt for printing.
		/// </summary>
		public void makingReport()
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="";
			string info= "";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ServoSadbhavnalistReport.txt";
			StreamWriter sw = new StreamWriter(path);
			//******************
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09 
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and  cust_type like('Ro%')"; 
			}*/

			//****************
			//	sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar'";
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
			sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("SERVO SADBHAVNA LIST REPORT",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
			// info : to set string format.
			info = " {0,-20:S} {1,-20:S} {2,-20:S} {3,-15:S}";
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Ashok Nagar");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma date on 07.04.09 
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'";
			sql=sql+" order by "+Cache["strorderbyBhind"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Bhind");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********

			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'";
			sql=sql+" order by "+Cache["strorderbyDatia"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Datia");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}
			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'";
			sql=sql+" order by "+Cache["strorderbyGuna"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Guna");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********

			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*//Comment By Vikas Sharma Date on 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'";
			sql=sql+" order by "+Cache["strorderbyGwalior"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Gwalior");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********

			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'";
			sql=sql+" order by "+Cache["strorderbyMorena"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Morena");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********

			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}
			/*// Comment By Vikas SHarma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'";
			sql=sql+" order by "+Cache["strorderbySheopur"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Sheopur");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09 
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and  cust_type like('Ro%')"; 
			}*/
			//************
			//sql="select state r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'";
			sql=sql+" order by "+Cache["strorderbyShivpuri"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine(" District :- Shivpuri");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine("|  Firm Name         |  Place             |      Category      | SadbhavnaCode |");
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				//             1234567...........20 1234567...........20 1234567...........20 1234567......15
				while(rdr.Read())
				{
					sw.WriteLine(info,StringUtil.trimlength(rdr["r2"].ToString().Trim(),20),						
						rdr["r3"].ToString().Trim(),
						rdr["r4"].ToString().Trim(),
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine("+--------------------+--------------------+--------------------+---------------+");
				sw.WriteLine(" ");
			}
			dbobj.Dispose();
			
			sw.Close();

		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			//InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\ServoSadbhavnaList.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='AshokNagar' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*//Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Ashok Nagar' and  cust_type like('Ro%')"; 
			}*/
			sql=sql+" order by "+Cache["strorderbyAshok"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Ashok Nagar");
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}
			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Bhind' and  cust_type like('Ro%')"; 
			}*/
			sql=sql+" order by "+Cache["strorderbyBhind"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Bhind");
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*//Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Datia' and  cust_type like('Ro%')"; 
			}*/
			sql=sql+" order by "+Cache["strorderbyDatia"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Datia");
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*//Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Guna' and  cust_type like('Ro%')"; 
			}*/
			sql=sql+" order by "+Cache["strorderbyGuna"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Guna");
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}

			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Gwalior' and  cust_type like('Ro%')"; 
			}*/
			
			sql=sql+" order by "+Cache["strorderbyGwalior"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Gwalior");
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}
			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Morena' and  cust_type like('Ro%')"; 
			}*/
			
			sql=sql+" order by "+Cache["strorderbyMorena"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Morena");
				
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+	
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********
			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}
			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Sheopur' and  cust_type like('Ro%')"; 
			}*/
			
			sql=sql+" order by "+Cache["strorderbySheopur"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Sheopur");
				
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
				
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				
				sw.WriteLine();
			}
			dbobj.Dispose();
			//**********

			if(droptype.SelectedItem.Text.Equals("All"))
			{
				//16.07.09 vikas sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri'"; //and( cust_type='Bazzar' or cust_type like('Ro%'))";
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and( cust_type='Bazzar' or cust_type like('Ro%') or cust_type like('N-KSK%') or cust_type like('Essar%'))";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type like('"+droptype.SelectedValue.ToString().Trim()+"%')";
			}
			/*// Comment By Vikas Sharma 07.04.09
			if(droptype.SelectedItem.Text.Equals("Both"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and( cust_type='Bazzar' or cust_type like('Ro%'))";
			} 
			else if(droptype.SelectedItem.Text.Equals("Bazzar"))
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and cust_type='Bazzar'";
			} 
			else
			{
				sql="select sadbhavnacd r1,cust_name r2,city r3,cust_type r4 from customer where state='Shivpuri' and  cust_type like('Ro%')"; 
			}*/
			
			sql=sql+" order by "+Cache["strorderbyShivpuri"];
			dbobj.SelectQuery(sql, ref rdr);
			if(rdr.HasRows)
			{
				sw.WriteLine("District\t"+"Shivpuri");
				sw.WriteLine("Firm Name\tPlace\tCategory\tSadbhavnaCode");
			
				while(rdr.Read())
				{
					sw.WriteLine(rdr["r2"].ToString().Trim()+"\t"+
						rdr["r3"].ToString().Trim()+"\t"+
						rdr["r4"].ToString().Trim()+"\t"+
						rdr["r1"].ToString().Trim());
				}
				
				sw.WriteLine();
			}
			dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file ServoSadBhavnalistReport.txt for printing.
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
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

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ServoSadbhavnalistReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:print");
                
				} 
				catch (ArgumentNullException ane) 
				{
					//Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:print"+ ane.Message+". User: "+uid);
				} 
				catch (SocketException se) 
				{
					///Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:print"+ se.Message+". User: "+uid);
				} 
				catch (Exception es) 
				{
					//Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:print"+ es.Message+". User: "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ServoSadbhavnalistReport.aspx,Method:print"+ ex.Message+". User: "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file ServoSadBhavnaList.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DataGrid1.Visible==true || Datagrid2.Visible==true || Datagrid3.Visible==true || Datagrid4.Visible==true || Datagrid5.Visible==true || Datagrid6.Visible==true || Datagrid7.Visible==true || Datagrid8.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:ServoSadbhavnaList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   ServoSadbhavnaList Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:ServoSadbhavnaList.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    ServoSadbhavnaList Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}