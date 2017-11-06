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
using DBOperations;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for View_Customer.
	/// </summary>
	public partial class Customer_List : System.Web.UI.Page
	{
		string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
		string uid;
        string BaseUri = "http://localhost:64862";
        /// <summary>
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
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
				CreateLogFiles.ErrorLog("Form:Customer_list.aspx,Method:on_pageload  "+ " EXCEPTION "+uid +ex.Message);
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
		/// This checks the permissions.
		/// and this method checks the user privileges from session.
		/// </summary>
		public void checkPrivileges()
		{
			int i;
			string Module="3";
			string SubModule="2";
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
		/// This is used to binding the grid.
		/// </summary>
		public void gridInit()
		{
			try
			{
                PartiesClass obj = new PartiesClass();
                CustomerModel customer = new CustomerModel();
                DataSet ds;
                ds = obj.ShowCustomerInfo(txtCustID.Text, txtName.Text, txtPlace.Text);
                //****
                DataTable dt = ds.Tables[0];
                DataView dv=new DataView(dt);
                customer.CustomerID = txtCustID.Text;
                customer.CustomerName = txtName.Text;
                customer.Place = txtPlace.Text;

                //dv.Sort = System.Convert.ToString(Cache["strorderby"]);
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(BaseUri);
                //    var myContent = JsonConvert.SerializeObject(customer);
                //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                //    var byteContent = new ByteArrayContent(buffer);
                //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //    client.DefaultRequestHeaders.Accept.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //    var response = client.PostAsync("api/CustomerController/GetCustomersData", byteContent).Result;
                //    if (response.IsSuccessStatusCode)
                //    {
                //        string responseString = response.Content.ReadAsStringAsync().Result;
                //        dv = JsonConvert.DeserializeObject<DataView>(responseString);
                //    }
                //    else
                //        response.EnsureSuccessStatusCode();
                //}
                //dv.Sort = System.Convert.ToString(Cache["strorderby"]);
                if (dv.Count>0)
				{
					GridSearch.DataSource=dv;
					GridSearch.DataBind();
					GridSearch.Visible=true;
				}
				else
				{
					MessageBox.Show("Customer not Found");
					GridSearch.Visible=false;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_list.aspx,Method:on_pageload  EXCEPTION: "+ex.Message+" User_ID: "+uid);
			}
		}

		/// <summary>
		/// This method is used to search the particular from customer table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			GridSearch.CurrentPageIndex=0;
			Cache["strorderby"]="Cust_ID ASC";
			Session["Column"]="Cust_ID";
			Session["order"]="ASC";
			gridInit();
		}
		
		/// <summary>
		/// This is used to make sorting the grid onclicking of datagridheader.
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
				gridInit();
			}
			catch(Exception )
			{

			}
		}

		/// <summary>
		/// This is used to make paging the grid onclicking of datagridheader.
		/// only five record show at a time.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void GridSearch_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			try
			{
				GridSearch.CurrentPageIndex =e.NewPageIndex;
				gridInit();
				//				PartiesClass obj=new PartiesClass();
				//				DataSet ds;
				//				ds=obj.ShowCustomerInfo(txtCustID.Text,txtName.Text,txtPlace.Text );
				//				GridSearch.DataSource=ds;
				//				GridSearch.DataBind();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_list.aspx,Method:GridSearch_PageIndexChanged  EXCEPTION "+ex.Message+" User_ID: "+uid);
			}
		}

		/// <summary>
		/// This method is used to delete the particular record.
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/GetCount?Customer="+ e.Item.Cells[0].Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        Count = JsonConvert.DeserializeObject<int>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (Count>1)
				{
					MessageBox.Show("Please Remove The All Transaction Concerning Customer");
					return;
				}
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/DeleteCustomer?Customer="+ e.Item.Cells[1].Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        var mesg = JsonConvert.DeserializeObject<string>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/DeleteLedger?Customer="+ e.Item.Cells[0].Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        var mesg = JsonConvert.DeserializeObject<string>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                //				sqlCmd.CommandText="Delete from LedgDetails Where Cust_ID='"+e.Item.Cells[1].Text+"'";
                //				sqlConn.ConnectionString=strCon;
                //				sqlConn.Open();
                //				sqlCmd.Connection=sqlConn;
                //				sqlCmd.ExecuteNonQuery();
                //				sqlConn.Close();
                //				sqlCmd.Dispose();
                //				sqlCmd.CommandText="Delete from Invoice_Transaction Where Cust_ID='"+e.Item.Cells[1].Text+"'";
                //				sqlConn.ConnectionString=strCon;
                //				sqlConn.Open();
                //				sqlCmd.Connection=sqlConn;
                //				sqlCmd.ExecuteNonQuery();
                //				sqlConn.Close();
                //				sqlCmd.Dispose();
                CreateLogFiles.ErrorLog("Form:Customer_list.aspx,Method:GridSearch_DeleteCommand"+"  Customer ID  "+e.Item.Cells[1].Text+"  IS DELETED   "+" USERID      "+uid );
				MessageBox.Show("Customer Deleted");
				gridInit();
				Response.Redirect("Customer_List.aspx",false);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_list.aspx,Method:GridSearch_DeleteCommand"+"  Customer ID  "+e.Item.Cells[1].Text+"  IS DELETED   "+" USERID "+uid +"      "+"EXCEPTION   "+"   "+ex.Message);
			}
		}

		protected void GridSearch_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
	}
}