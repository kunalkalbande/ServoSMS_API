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
using DBOperations;
using RMG;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for Customer_Entry.
	/// </summary>
	public partial class Customer_Entry : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.TextBox TextBox1;
		string uid;
        string BaseUri = "http://localhost:64862";
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
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:onpageload" + ex.Message+" EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			//txtbeatname.Visible=false;
			if(!IsPostBack)
			{
				try
				{
                    #region Fill dropType
                    List<string> DrpType = new List<string>();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/CustomerController/GetCustomerType").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            DrpType = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    DropType.Items.Clear();
                    if (DrpType != null && DrpType.Count > 0)
                        foreach (var typr in DrpType)
                            DropType.Items.Add(typr);

					#endregion
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
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
					if(View_flag=="0")
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					if(Add_Flag=="0")
						btnUpdate.Enabled=false;
					#endregion

					for(i=1;i<=30;i++)
					{
						DropCrDay.Items.Add(i.ToString ());
					}
					GetNextCustomerID();
					getbeat();

                    #region Fetch Extra Cities from Database and add to the ComboBox
                    List<string> cities = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/OrganizationDetailsController/GetExtraCities").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            cities = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    if (cities != null && cities.Count > 0)
                    {
                        foreach (var city in cities)
                            DropCity.Items.Add(city);

                    }
                    #endregion

                    #region Fetch Extra Cities from Database and add to the ComboBox
                    List<string> states = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/OrganizationDetailsController/GetExtraStates").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            states = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    if (states != null && states.Count > 0)
                    {
                        foreach (var state in states)
                            DropState.Items.Add(state);
                    }
                    #endregion

                    #region Fetch Extra Cities from Database and add to the ComboBox
                    List<string> countries = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/OrganizationDetailsController/GetExtraCountry").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            countries = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    if (countries != null && countries.Count > 0)
                    {
                        foreach (var country in countries)
                            DropCountry.Items.Add(country);
                    }
                    #endregion

                    #region Fetch SSR Employee from Employee Table and add to the ComboBox
                    List<string> SSR = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/CustomerController/FetchSSREmployee").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            SSR = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    DropSSR.Items.Clear();
                    if (SSR != null && SSR.Count > 0)
                    {
                        foreach (var ss in SSR)
                            DropSSR.Items.Add(ss);
                    }
                    #endregion
                }
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:onpageload" + ex.Message+" EXCEPTION  "+uid);
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

		/// <summary>
		/// This is used to concatenate the city,state,country for further use in javascript.
		/// </summary>
		public void getbeat()
		{
			try
			{
                string str = "";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/OrganizationDetailsController/GetBeat").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        str = JsonConvert.DeserializeObject<string>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
				txtbeatname.Value=str;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,class:Inventoryclass.cs,method:getbeat()"+"Exception"+ex.Message+uid);
			}
		}

		/// <summary>
		/// Returns date in MM/DD/YYYY format.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];
			strarr=str.IndexOf("-")>0?str.Split(new char[]{'-'},str.Length): str.Split(new char[] { '/' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);
			return(dt);
		}

		/// <summary>
		/// Its checks the before save that the account period is inserted in organisaton table or not.
		/// </summary>
		/// <returns></returns>
		public bool checkAcc_Period()
		{
            bool c = false;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/CheckAcc_Period").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        c = JsonConvert.DeserializeObject<bool>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:checkAcc_Period(). EXCEPTION : " + ex.Message + "  User_ID: " + uid);
            }
            return c;
		}

		/// <summary>
		/// This method is used to Save the Customer Information in customer table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			//PartiesClass obj=new PartiesClass();
            try
            {
                StringBuilder errorMessage = new StringBuilder();
                if (txtTinNo.Text != string.Empty)
                {
                    string sPattern = "^[a-zA-Z0-9]+$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(txtTinNo.Text, sPattern))
                    {
                        errorMessage.Append("- Please Enter GSTIN No. in Alpha Numeric");
                        errorMessage.Append("\n");
                    }
                }
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage.ToString());
                    return;
                }
                if (!checkAcc_Period())
                {
                    MessageBox.Show("Please enter the Accounts Period from Organization Details");
                    return;
                }
                //string cname=StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim())) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() )); 
                string cname = "";
                if (txtFName.Text.Trim() != "")
                    cname = txtFName.Text.Trim();

                //Coment by vikas 16.05.09
                /*if(txtMName.Text.Trim()!="")
					cname+=" "+txtMName.Text.Trim();
				if(txtLName.Text.Trim()!="")
					cname+=" "+txtLName.Text.Trim();*/

                //((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim() ))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim() ));
                bool custExists = false;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/GetCustID?custName="+ cname.Trim()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        custExists = JsonConvert.DeserializeObject<bool>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (custExists)
                {
                    MessageBox.Show("Customer Name  " + cname + " Already Exist");
                    return;
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/GetLedger?custName=" + cname.Trim()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        custExists = JsonConvert.DeserializeObject<bool>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (custExists)
                {
                    MessageBox.Show("Ledger Name  " + cname + " Already Exist");
                    return;
                }

                if (!txtTinNo.Text.Trim().Equals(""))
                {
                    string customerID = "";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/CustomerController/GetTinNoExists?TinNo="+ txtTinNo.Text.Trim()).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            customerID = JsonConvert.DeserializeObject<string>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    if (!LblCustomerID.Text.Equals(customerID))
                    {
                        MessageBox.Show("The Tin No. " + txtTinNo.Text.Trim() + " Already Exist");
                        return;
                    }
                }
                else
                {
                    txtTinNo.Text = "Un Register";
                }
                CustomerModel customer = new CustomerModel();

                customer.CustomerID = LblCustomerID.Text;
                //obj.Cust_Name =StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim() )) +" "+ StringUtil.FirstCharUpper((txtMName.Text.ToString().Trim()))+" "+ StringUtil.FirstCharUpper((txtLName.Text.ToString().Trim())); 
                //string Name = StringUtil.FirstCharUpper((txtFName.Text.ToString().Trim() ));
                string Name = txtFName.Text.ToString().Trim();

                //Coment By vikas 16.05.09
                /*if(!txtMName.Text.ToString().Trim().Equals(""))
					Name += " "+txtMName.Text.ToString().Trim();
				if(!txtLName.Text.ToString().Trim().Equals(""))
					Name += " "+txtLName.Text.ToString().Trim();*/

                customer.CustomerName = Name;
                customer.CustomerType = DropType.SelectedItem.Value.ToString();
                customer.Address = txtAddress.Text.Trim();
                customer.City = DropCity.SelectedItem.Value.ToString();
                customer.State = DropState.SelectedItem.Value.ToString();
                customer.Country = DropCountry.SelectedItem.Value.ToString();
                if (DropSSR.SelectedIndex == 0)
                    customer.SSR = "";
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/CustomerController/GetEmpSSR?SSR="+ DropSSR.SelectedItem.Text).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            customer.SSR = JsonConvert.DeserializeObject<string>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                }

                if (txtPhoneOff.Text == "")
                    customer.Tel_Off = "0";
                else
                    customer.Tel_Off = txtPhoneOff.Text;
                if (txtPhoneRes.Text == "")
                    customer.Tel_Res = "0";
                else
                    customer.Tel_Res = txtPhoneRes.Text;
                if (txtMobile.Text == "")
                    customer.Mobile = "0";
                else
                    customer.Mobile = txtMobile.Text;
                customer.EMail = txtEMail.Text.Trim();
                customer.CR_Limit = txtCRLimit.Text;
                if (DropCrDay.SelectedIndex == 0)
                    customer.CR_Days = "0";
                else
                    customer.CR_Days = DropCrDay.SelectedItem.Value.ToString();
                if (txtOpBalance.Text == "")
                    customer.Op_Balance = "0";
                else
                    customer.Op_Balance = txtOpBalance.Text;
                customer.Balance_Type = DropBal.SelectedItem.Value.ToString();
                customer.EntryDate = ToMMddYYYY(DateTime.Now.Date.ToShortDateString()).ToString();
                customer.TinNo = txtTinNo.Text.Trim();
                if (txtcode.Text.Trim().Equals(""))
                    customer.sadbhavnacd = "0";
                else
                    customer.sadbhavnacd = txtcode.Text.Trim().ToString();
                customer.ContactPerson = txtContactPerson.Text;
                // Call to this method Inserts the customer details into the customer table.
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(customer);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/CustomerController/InsertCustomer", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }
                MessageBox.Show("Customer Saved");
                CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs: Method:btnUpdate_Click " + " Cust Name  " + customer.CustomerName + " Cust id  " + customer.CustomerName + "Cust Type    " + customer.CustomerType + "  Cust Address  " + customer.Address + " Cust City " + customer.City + " Cust State  " + customer.State + " Cust Cuntry " + ToMMddYYYY(DateTime.Now.Date.ToShortDateString()).ToShortDateString() + "obj.Country" + " Opening Balance  " + customer.Op_Balance + "  date  " + customer.EntryDate + "    IS  SAVED    User  " + uid);
                Clear();
                GetNextCustomerID();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs: Method:btnUpdate_Click  EXCEPTION " + ex.Message + "  User Type " + uid);
            }
		}

		/// <summary>
		/// Method to clear the form.
		/// </summary>
		public void Clear()
		{
			LblCustomerID.Text="";
			txtFName.Text="";
			//txtMName.Text="";
			//txtLName.Text="";
			txtEMail.Text="";
			txtOpBalance.Text="";
			txtAddress.Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry.SelectedIndex=0;
			DropType.SelectedIndex=0; 
			DropCrDay.SelectedIndex=0; 
			DropBal.SelectedIndex=0; 
			txtCRLimit.Text="";
			txtPhoneOff.Text="";
			txtPhoneRes.Text="";
			txtMobile.Text="";
			txtTinNo.Text = "";
			txtcode.Text="";
			DropSSR.SelectedIndex=0;
			txtContactPerson.Text="";
		}

		/// <summary>
		/// Returns the next Customer ID. from customer table.
		/// </summary>
		public void GetNextCustomerID()
		{
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/GetNextID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        LblCustomerID.Text = JsonConvert.DeserializeObject<string>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs: Method:GetNextCustomerID().  EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}


		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Select the state and country according to the selected city.
			/*	try
				{
				
					InventoryClass  obj=new InventoryClass ();
					SqlDataReader SqlDtr;
					string sql;
					sql="select State,Country from Beat_Master where City='"+ DropCity.SelectedItem.Value +"'" ;
					SqlDtr=obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
				
						DropState.SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByValue(SqlDtr.GetValue(0).ToString()))));
						DropCountry.SelectedIndex=(DropCountry .Items.IndexOf((DropCountry.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
				
					} 
        
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Customer_Entry.aspx,Class:PartiesClass.cs ,Method:DropCity_SelectedIndexChanged" + "EXCEPTION  "+ex.Message+uid);
				
				}*/
		}

		protected void txtAddress_TextChanged(object sender, System.EventArgs e)
		{
		}

		protected void DropType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		protected void txtFName_TextChanged(object sender, System.EventArgs e)
		{
		}
	}
}