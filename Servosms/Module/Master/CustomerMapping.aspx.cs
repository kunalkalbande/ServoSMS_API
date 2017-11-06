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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using RMG;
using DBOperations;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Master
{
    /// <summary>
    /// Summary description for CustomerMapping.
    /// </summary>
    public partial class CustomerMapping : System.Web.UI.Page
    {
        DBOperations.DBUtil dbobj = new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        string uid = "";
        string BaseUri = "http://localhost:64862";
        /// <summary>
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values and also 
        /// check accessing priviledges for particular user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                uid = (Session["User_Name"].ToString());
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:pageload" + ex.Message + "  EXCEPTION  " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }
            try
            {
                if (!Page.IsPostBack)
                {
                    /*InventoryClass obj = new InventoryClass();
					SqlDataReader rdr=obj.GetRecordSet("select Emp_Name from Employee where Designation='Servo Sales Representative'");
					DropSSRName.Items.Clear();
					DropSSRName.Items.Add("Select");
					while(rdr.Read())
					{
						DropSSRName.Items.Add(rdr["Emp_Name"].ToString());
					}
					rdr.Close();*/
                    #region Check Privileges
                    int i;
                    string View_Flag = "0", Add_Flag = "0", Edit_Flag = "0", Del_Flag = "0";
                    string Module = "3";
                    string SubModule = "5";
                    string[,] Priv = (string[,])Session["Privileges"];
                    for (i = 0; i < Priv.GetLength(0); i++)
                    {
                        if (Priv[i, 0] == Module && Priv[i, 1] == SubModule)
                        {
                            View_Flag = Priv[i, 2];
                            Add_Flag = Priv[i, 3];
                            Edit_Flag = Priv[i, 4];
                            Del_Flag = Priv[i, 5];
                            break;
                        }
                    }
                    Cache["Add"] = Add_Flag;
                    Cache["View"] = View_Flag;
                    Cache["Edit"] = Edit_Flag;
                    Cache["Del"] = Del_Flag;
                    if (View_Flag == "0")
                    {
                        Response.Redirect("../../Sysitem/AccessDeny.aspx", false);
                        return;
                    }
                    if (Add_Flag == "0")
                        btnSubmit.Enabled = false;
                    if (Edit_Flag == "0")
                        DropSSRName.Enabled = false;
                    #endregion
                    FillList();
                    FillCustomer();
                }
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:PageLoad,     User  " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:PageLoad,  EXCEPTION " + ex.Message + "  User  " + uid);
            }
        }

        /// <summary>
        /// This method is used to fatch the Employee name whose designation have Servo Sales Representative
        /// and fill the dropdownlist when page is loaded.
        /// </summary>
        public void FillCustomer()
        {
            try
            {
                List<string> employees = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/FetchEmployeeName").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        employees = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                DropSSRName.Items.Clear();
                DropSSRName.Items.Add("Select");
                if (employees != null && employees.Count > 0)
                {
                    foreach (var employee in employees)
                        DropSSRName.Items.Add(employee);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:FillCustomer" + ex.Message + "  EXCEPTION  " + uid);
            }
        }

        /// <summary>
        /// This method is used to fatch the all customer from customer table and fill the list.
        /// </summary>
        public void FillList()
        {
            try
            {
                List<string> Customers = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/FetchAllCustomers").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        Customers = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                ListCustomer.Items.Clear();
                ListCustomer.Items.Add("Select");
                if (Customers != null && Customers.Count > 0)
                {
                    foreach (var Customer in Customers)
                        ListCustomer.Items.Add(Customer);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:FillList" + ex.Message + "  EXCEPTION  " + uid);
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
        /// This method is used to move the customer from one list to anather.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIn_Click(object sender, System.EventArgs e)
        {
            try
            {
                while (ListCustomer.SelectedItem.Selected)
                {
                    ListassignCustomer.Items.Add(ListCustomer.SelectedItem.Value);
                    ListCustomer.Items.Remove(ListCustomer.SelectedItem.Value);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// This method is used to move the customer from one list to anather.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnout_Click(object sender, System.EventArgs e)
        {
            try
            {
                while (ListassignCustomer.SelectedItem.Selected)
                {
                    ListCustomer.Items.Add(ListassignCustomer.SelectedItem.Value);
                    ListassignCustomer.Items.Remove(ListassignCustomer.SelectedItem.Value);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// This method is used to move all customer from assign customer list and remove the all customer
        /// from customer list vice versa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn1_Click(object sender, System.EventArgs e)
        {
            if (btn1.Text.Trim().Equals(">>"))
            {
                try
                {
                    btn1.Text = "<<";
                    foreach (System.Web.UI.WebControls.ListItem lst in ListCustomer.Items)
                        ListassignCustomer.Items.Add(lst);
                    ListCustomer.Items.Clear();
                }
                catch (Exception ex)
                {
                    CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:cmdrpt_Click" + ex.Message);
                }
            }
            else
            {
                try
                {
                    btn1.Text = ">>";
                    foreach (System.Web.UI.WebControls.ListItem lst in ListassignCustomer.Items)
                        ListCustomer.Items.Add(lst);
                    ListassignCustomer.Items.Clear();

                }
                catch (Exception ex)
                {
                    CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:btnOut_Click  EXCEPTION " + ex.Message + "  User  " + uid);
                }
            }
        }

        /// <summary>
        /// This method is used to fatch the all customer whose ssr is select from dropdownlist
        /// and fill the data in both list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropSSRName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                List<string> Customers = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/FetchCustomerBySSR?SSR=" + DropSSRName.SelectedItem.Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        Customers = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                ListassignCustomer.Items.Clear();
                FillList();
                if (Customers != null && Customers.Count > 0)
                {
                    foreach (var customer in Customers)
                    {
                        ListassignCustomer.Items.Add(customer);
                        ListCustomer.Items.Remove(customer);
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:DropSSRName_SelectedIndexChanged,   EXCEPTION " + ex.Message + "  User  " + uid);
            }
        }

        /// <summary>
        /// This method is used to save or update the customer according to ssr in customer table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {
            try
            {
                int x = 0;
                string EmpID = "";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/CustomerController/GetEmpSSR?SSR=" + DropSSRName.SelectedItem.Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        EmpID = JsonConvert.DeserializeObject<string>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                for (int i = 0; i < ListassignCustomer.Items.Count; ++i)
                {
                    ListassignCustomer.SelectedIndex = i;
                    string pname = ListassignCustomer.SelectedItem.Value;
                    CustomerModel customer = new CustomerModel();
                    customer.CustomerID = EmpID;
                    customer.CustomerName = pname;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(customer);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/CustomerController/InsertorUpdateCustomerMapping", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var msg = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                        }
                        else
                            response.EnsureSuccessStatusCode();
                    }
                }
                MessageBox.Show("Customer Mapping Update");
                FillList();
                DropSSRName.SelectedIndex = 0;
                ListassignCustomer.Items.Clear();
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:btnSubmit_Click,  Update Customer Mapping,  User  " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:CustomerMapping.aspx,Method:btnSubmit_Click  EXCEPTION " + ex.Message + "  User  " + uid);
            }
        }
    }
}