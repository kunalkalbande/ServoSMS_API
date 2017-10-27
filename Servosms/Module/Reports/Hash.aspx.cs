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
	/// Summary description for Hash.
	/// </summary>
	public partial class Hash : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			Hashtable table=new Hashtable();
			/*
			table["name"]="vishnu";
			table["lname"]="shukla";
			table["mname"]="Chandra";
			IEnumerator ide;
			ide= table.GetEnumerator();
			//foreach(string key in table.Keys)
			//{
			DataGrid1.DataSource=ide.ToString();
			//}
			DataGrid1.DataBind();
			DataGrid1.Visible=true;*/
			SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			InventoryClass obj=new InventoryClass();
			SqlDataReader sqldtr;
			string sqlstr="select Category,Prod_Name from Products where Category='Engine Oil'";
			sqldtr=obj.GetRecordSet(sqlstr);
			Hashtable ht=new Hashtable();
			Hashtable ht1=new Hashtable();
			/*ht1.Add("Category","Category");
			ht1.Add("Prod_Name","Prod_Name");*/
			//int i=0;
			while(sqldtr.Read())
			{
				//ht["Category"] = sqldtr.GetValue(0).ToString();
                //ht["Prod_Name"] = sqldtr.GetValue(1).ToString();
				//ht["Category"]=sqldtr.GetValue(0).ToString();
				//ht["Prod_Name"]=sqldtr.GetValue(1).ToString();
				//ht1.Add(i,ht.Values);
				//i++;
				DropDownList1.Items.Add(sqldtr.GetValue(1).ToString());
			}
			DataGrid1.DataSource=ht1.Values;
			//DataGrid1.DataSource=ht;
			
			DataGrid1.DataBind();
			//DropDownList1.SelectedItem.Text
				//DropDownList1.SelectedIndex.
			//TextBox1.t
/*			DataSet ds= new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			da.Fill(ds, "Leave_Register");
			DataTable dtCustomers = ds.Tables["Leave_Register"];
			DataView dv=new DataView(dtCustomers);
			dv.Sort = strOrderBy;
			Cache["strOrderBy"]=strOrderBy;
			if(dv.Count==0)
			{
				MessageBox.Show("Data not available");
				GridReport.Visible=false;
			}
			else
			{
				GridReport.DataSource=dv;
				GridReport.DataBind();
				GridReport.Visible=true;
			}*/
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
	}
}
