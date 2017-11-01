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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Servo_API.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace Servosms.Module.Inventory
{
    /// <summary>
    /// Summary description for SalesInvoice.
    /// </summary>
    public partial class SalesInvoice : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.HyperLink HLinkHome;
        protected System.Web.UI.WebControls.DropDownList DropCustName;
        protected System.Web.UI.WebControls.Label hiddenCustID;
        protected System.Web.UI.WebControls.CompareValidator CompareValidator1;
        protected System.Web.UI.WebControls.CompareValidator sCompareValidator3;
        protected System.Web.UI.WebControls.CompareValidator CompareValidator4;
        protected System.Web.UI.WebControls.Label lblDate;
        DBUtil dbobj = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        string uid;
        string baseUri = "http://localhost:64862";

        public int flag = 0;
        public float Header1Height = 0;
        public float Header2Height = 0;
        public float BodyHeight = 0;
        public float Footer1Height = 0;
        public float Footer2Height = 0;
        public float RateRs = 0;
        public float BillQty = 0;
        public float AmountRs = 0;
        public float Igst = 0;
        public float Cgst = 0;
        public float Sgst = 0;
        public float BatchNo = 0;
        public float GradePackName = 0;
        public float FreeQty = 0;
        public float DisQty = 0;
        public float LtrKg = 0;
        public float SchDis = 0;
        public float SSpDis = 8;
        public float RupeesinWords = 0;
        public float ProvisionalBalance = 0;
        public float Remarks = 0;
        public float Position1 = 0;
        public float Position2 = 0;
        public bool PartyName = false;
        public bool Address = false;
        public bool City = false;
        public bool Tin_No = false;
        public bool DocumentNo = false;
        public bool Date = false;
        public bool DtTime = false;
        public bool DueDate = false;
        public bool Time = false;
        public bool Blank = false;
        public bool Blank1 = false;
        public bool VehicleNo = false;
        public static bool FlagPrint = false;
        //***********
        static string[] ProductType = new string[12];
        static string[] ProductName = new string[12];
        static string[] ProductPack = new string[12];
        static string[] ProductQty = new string[12];
        static string[] SchProductType = new string[12];
        static string[] SchProductName = new string[12];
        static string[] SchProductPack = new string[12];
        static string[] SchProductQty = new string[12];
        public bool address = false;
        static string NetAmount = "0";
        static string FromDate = "", ToDate = "";
        string strInvoiceFromDate = string.Empty;
        string strInvoiceToDate = string.Empty;
        static string CustID = "";
        public static string val = "";
        
        protected System.Web.UI.HtmlControls.HtmlButton btndrop;        

        protected System.Web.UI.WebControls.CheckBox chkScheme;
        static string Invoice_Date = "";
        bool isDeleteClicked = false;

        /// <summary>
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// and generate the next ID also.
        /// </summary>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (hidInvoiceFromDate.Value != "" && hidInvoiceToDate.Value != "")
                {
                    fillInvoceNoDropdown();
                }
                
                uid = (Session["User_Name"].ToString());
                txtMessage.Text = (Session["Message"].ToString());                

            }

            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:pageload" + ex.Message + "  EXCEPTION " + "   " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }
            //if click the delete button then fire the function
            if (tempDelinfo.Value == "Yes")
            {                
                DeleteSalesInvoice();

                UpdateBatchNo();
            }

            if (!IsPostBack)
            {
                try
                {
                    lblInvoiceDate.Attributes.Add("readonly", "readonly");
                    tempEdit.Value = "True";           
                    Invoice_Date = "";
                    checkPrevileges();
                    lblInvoiceDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    txtChallanDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    
                    lblEntryTime.Text = DateTime.Now.ToString();
                    lblEntryBy.Text = Session["User_Name"].ToString();
                    

                    InventoryClass obj = new InventoryClass();                    

                    #region Fetch the From and To Date From OrganisationDatail table.
                    List<string> resFromDateToDate = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetFromDateToDateData").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            resFromDateToDate = JsonConvert.DeserializeObject<List<string>>(result);
                        }
                    }

                    if (resFromDateToDate != null)
                    {
                        FromDate = resFromDateToDate[0];
                        ToDate = resFromDateToDate[1];
                    }
                    else
                    {
                        MessageBox.Show("Please Fill The Organization Form First");
                        return;
                    }
                    
                    #endregion

                    GetNextInvoiceNo();

                    #region Fetch the Product Types and fill in the ComboBoxes                    
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var response = client.GetAsync("/api/sales/GetProductTypes").Result;
                        string res = "";

                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                // ... Read the string.
                                Task<string> result = content.ReadAsStringAsync();
                                res = result.Result;
                                texthiddenprod.Value = res;
                            }
                        }
                    }                    
                    #endregion
                    
                    #region Fetch All SalesMan and Fill in the ComboBox
                    List<string> salesMan = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetSalesManData").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            salesMan = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                    }

                    if (salesMan != null)
                    {
                        foreach (var man in salesMan)
                            DropUnderSalesMan.Items.Add(man);
                    }

                    //               sql = "Select Emp_Name from Employee where Designation ='Servo Sales Representative' and status=1 order by Emp_Name";
                    //SqlDtr = obj.GetRecordSet(sql); 
                    //while(SqlDtr.Read())
                    //{
                    //	DropUnderSalesMan.Items.Add (SqlDtr.GetValue(0).ToString ());				
                    //}
                    //SqlDtr.Close ();		
                    #endregion

                    #region fill hiddentext 
                    List<string> custNameCity = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetCustNameCityData").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            custNameCity = JsonConvert.DeserializeObject<List<string>>(result);
                        }
                    }
                    if (custNameCity != null)
                    {
                        texthidden.Value = custNameCity[0];
                        val = custNameCity[1];
                    }

                    //               //sql="select cust_name from customer order by cust_name ";  //Comment by vikas sharma 27.04.09
                    //               sql ="select cust_name,City from customer order by cust_name ";
                    ////sql="select cust_name from customer where cust_type='Ro-1' order by cust_name ";
                    //SqlDtr=obj.GetRecordSet(sql);
                    //int i=0;
                    //val="";
                    //while(SqlDtr.Read())
                    //{
                    //	//texthidden.Value+=SqlDtr.GetValue(0).ToString()+","; //Comment by vikas sharma 27.04.09
                    //	texthidden.Value+=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString()+",";
                    //	//MessageBox.Show(texthidden.Value);
                    //	//Request.Params.Set("DropCustName",SqlDtr.GetValue(0).ToString());
                    //	val+=SqlDtr.GetValue(0).ToString()+",";

                    //	//MessageBox.Show(val);
                    //	i++;
                    //}
                    //SqlDtr.Close();
                    #endregion

                    #region Fetch All Discount and fill in the textbox
                    List<string> discount = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetDiscountData").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            discount = JsonConvert.DeserializeObject<List<string>>(result);
                        }
                    }

                    if (discount != null)
                    {
                        txtCashDisc.Text = discount[0];
                        if (discount[1] != null)
                        {
                            if (discount[1].ToString() == "0" || discount[1].ToString() == "1")
                            {
                                DropCashDiscType.SelectedIndex = Int32.Parse(discount[1]);
                            }
                        }
                        txtDisc.Text = discount[2];
                        if (discount[3] != null)
                        {
                            if (discount[3].ToString() == "0" || discount[3].ToString() == "1")
                            {
                                DropDiscType.SelectedIndex = Int32.Parse(discount[3]);
                            }
                        }

                    }

                    //               sql ="select * from SetDis";
                    //SqlDtr=obj.GetRecordSet(sql);
                    //if(SqlDtr.Read())
                    //{
                    //	if(SqlDtr["CashDisSalesStatus"].ToString()=="1")
                    //	{
                    //		txtCashDisc.Text=SqlDtr["CashDisSales"].ToString();
                    //		if(SqlDtr["CashDisLtrSales"].ToString()=="Rs.")
                    //			DropCashDiscType.SelectedIndex=0;
                    //		else
                    //			DropCashDiscType.SelectedIndex=1;
                    //	}
                    //	else
                    //		txtCashDisc.Text="0";
                    //	if(SqlDtr["DiscountSalesStatus"].ToString()=="1")
                    //	{
                    //		txtDisc.Text=SqlDtr["DiscountSales"].ToString();
                    //		if(SqlDtr["DisLtrSales"].ToString()=="Rs.")
                    //			DropDiscType.SelectedIndex=0;
                    //		else
                    //			DropDiscType.SelectedIndex=1;
                    //	}
                    //	else
                    //		txtDisc.Text="0";
                    //}
                    //else
                    //{
                    //	txtDisc.Text="0";
                    //	txtCashDisc.Text="0";
                    //	DropCashDiscType.SelectedIndex=0;
                    //	DropDiscType.SelectedIndex=0;
                    //}

                    //SqlDtr.Close ();
                    #endregion
                    //getvalue();
                    PriceUpdation();

                    GetProducts();
                    FetchData();
                    GetFOECust();
                    getscheme();
                    getscheme1();
                    getschemefoe();
                    GetOrderInvoice();
                    getschemeSecSP();
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:Page_Load,  User_ID: " + uid);
                }
                catch (Exception ex)
                {
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:pageload.   EXCEPTION: " + ex.Message + "  User_ID: " + uid);
                }

                // This block of code first time on page load checks the pre print template file available or not according to it displays the warning message, and disables the pre print button.
                /*
					try
					{
						string home_drive = Environment.SystemDirectory;
						home_drive = home_drive.Substring(0,2); 
						string path = home_drive+@"\Inetpub\wwwroot\Servosms\PrePrintTemplate.INI";
						StreamReader  sr = new StreamReader(path);
						Button1.Enabled = true; 
						sr.Close();
					}
					catch(System.IO.FileNotFoundException)
					{
						MessageBox.Show("If you want to use Pre Print service then you have to execute PrintWizard\nto generate the Pre Print Template.");
						Button1.Enabled = false; 
					}
					*/
                //FetchData();
                //getscheme();
                //GetFOECust();
                //getscheme1();
                //getschemefoe();
            }

            if(!isDeleteClicked)
            {
                SaveDataInControlsOnPageLoad();
            }

            isDeleteClicked = false;
            txtVehicleNo.Attributes.Add("onmousemove", "getScheme_New();");
        }

        public void fillInvoceNoDropdown()
        {
            try
            {
                strInvoiceFromDate = hidInvoiceFromDate.Value;
                strInvoiceToDate = hidInvoiceToDate.Value;

                lblInvoiceNo.Visible = false;
                //btnEdit.Visible = false;
                dropInvoiceNo.Visible = true;
                btnSave.Enabled = true;
                Button1.Enabled = true;
                //Coment by vikas 06.09 DropSalesType.Enabled=false;
                DropOrderInvoice.SelectedIndex = 0;
                DropOrderInvoice.Enabled = false;
                //checkPrePrint();
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                #region Fetch the All Invoice Number and fill in Combo
                dropInvoiceNo.Items.Clear();
                dropInvoiceNo.Items.Add("Select");

                List<string> dates = new List<string>();
                dates.Add(strInvoiceFromDate);
                dates.Add(strInvoiceToDate);

                if (FromDate != "")
                {
                    //string strInvFromDate = GenUtil.str2MMDDYYYY(strInvoiceFromDate);
                    List<string> invoiceNo = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var Res = client.GetAsync("api/sales/FillInvoceNoDropdown?invoiceFromDate=" + strInvoiceFromDate + "&invoiceToDate=" + strInvoiceToDate).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            invoiceNo = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                    }

                    if (invoiceNo != null)
                    {
                        foreach (var nos in invoiceNo)
                            dropInvoiceNo.Items.Add(nos);
                    }

                    //sql = "select Invoice_No from Sales_Master where cast(floor(cast(Invoice_Date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(strInvoiceFromDate) +"' and cast(floor(cast(Invoice_Date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(strInvoiceToDate) + "' and Invoice_No like '" + FromDate + ToDate + "%' order by Invoice_No";
                    //SqlDtr = obj.GetRecordSet(sql);
                    //while (SqlDtr.Read())
                    //{
                    //    if (FromDate.StartsWith("0"))
                    //        dropInvoiceNo.Items.Add(SqlDtr.GetValue(0).ToString().Substring(2));
                    //    else
                    //        dropInvoiceNo.Items.Add(SqlDtr.GetValue(0).ToString().Substring(3));
                    //}
                    //SqlDtr.Close();
                    hidInvoiceFromDate.Value = "";
                    hidInvoiceToDate.Value = "";
                }
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : fillInvoceNoDropdown  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }
        //public void getvalue()
        //{
        //    InventoryClass obj = new InventoryClass();
        //    SqlDataReader SqlDtr = obj.GetRecordSet("select * from SetDis");
        //    if (SqlDtr.Read())
        //    {
        //        if (SqlDtr["IGSTSalesStatus"].ToString() == "1") { }
        //            //txtVatRate.Value = SqlDtr["IGSTSales"].ToString();
        //        else
        //            txtVatRate.Value = "0";
        //        if (SqlDtr["CGSTSalesStatus"].ToString() == "1")
        //            Tempcgstrate.Value = SqlDtr["CGSTSales"].ToString();
        //        else
        //            Tempcgstrate.Value = "0";
        //        if (SqlDtr["SGSTSalesStatus"].ToString() == "1")
        //            Tempsgstrate.Value = SqlDtr["SGSTSales"].ToString();
        //        else
        //            Tempsgstrate.Value = "0";

        //    }
        //}
        public void PriceUpdation()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/api/sales/PriceUpdation").Result;
                string res = "";

                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        // ... Read the string.
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                        txtMainIGST.Value = res;
                    }
                }
            }

            // InventoryClass obj = new InventoryClass();         
            //var dsPriceUpdation= obj.ProPriceUpdation();
            // var dtTable = dsPriceUpdation.Tables[0];
            // for (int i = 0; i < dtTable.Rows.Count; i++)
            // {
            //     txtMainIGST.Value = txtMainIGST.Value+ dtTable.Rows[i][0].ToString();//ProductCode
            //     txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][1];//ProductName 
            //     txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][2];//ProductId
            //     txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][3];//IGST
            //     txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][4];//cGST
            //     txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][5];//sGST
            //     txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][6];//HSN
            //     txtMainIGST.Value = txtMainIGST.Value + "~";


            // }
            // txtMainIGST.Value= txtMainIGST.Value.Substring(0,txtMainIGST.Value.LastIndexOf("~"));
        }

        public void SaveDataInControlsOnPageLoad()
        {
            lblInvoiceDate.Text = Request.Form["lblInvoiceDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["lblInvoiceDate"].ToString().Trim();
            txtChallanDate.Text = Request.Form["txtChallanDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtChallanDate"].ToString().Trim();

            txtChallanNo.Text = Request.Form["txtChallanNo"] == null ? null : Request.Form["txtChallanNo"].ToString().Trim();
            txtVehicleNo.Text = Request.Form["txtVehicleNo"] == null ? null : Request.Form["txtVehicleNo"].ToString().Trim();

            txtPromoScheme.Text = Request.Form["txtPromoScheme"] == null ? null : Request.Form["txtPromoScheme"].ToString().Trim();
            txtschemetotal.Text = Request.Form["txtschemetotal"] == null ? null : Request.Form["txtschemetotal"].ToString().Trim();
            txtliter.Text = Request.Form["txtliter"] == null ? null : Request.Form["txtliter"].ToString().Trim();
            txtfleetoediscount.Text = Request.Form["txtfleetoediscount"] == null ? null : Request.Form["txtfleetoediscount"].ToString().Trim();
            txtfleetoediscountRs.Text = Request.Form["txtfleetoediscountRs"] == null ? null : Request.Form["txtfleetoediscountRs"].ToString().Trim();
            txtSecondrySpDisc.Text = Request.Form["txtSecondrySpDisc"] == null ? null : Request.Form["txtSecondrySpDisc"].ToString().Trim();
            txtMessage.Text = txtMessage.Text;// Request.Form["txtMessage"] == null ? null : Request.Form["txtMessage"].ToString().Trim();
            txtRemark.Text = Request.Form["txtRemark"] == null ? null : Request.Form["txtRemark"].ToString().Trim();

            txtGrandTotal.Text = Request.Form["txtGrandTotal"] == null ? null : Request.Form["txtGrandTotal"].ToString().Trim();
            txtDisc.Text = txtDisc.Text;
            txtDiscount.Text = txtDiscount.Text;
            txtCashDisc.Text = txtCashDisc.Text;
            txtCashDiscount.Text = txtCashDiscount.Text;
            txtVAT.Text = Request.Form["txtVAT"] == null ? null : Request.Form["txtVAT"].ToString().Trim();
            txtNetAmount.Text = Request.Form["txtNetAmount"] == null ? null : Request.Form["txtNetAmount"].ToString().Trim();

            txtRate1.Text = Request.Form["txtRate1"] == null ? null : Request.Form["txtRate1"].ToString().Trim();
            txtRate2.Text = Request.Form["txtRate2"] == null ? null : Request.Form["txtRate2"].ToString().Trim();
            txtRate3.Text = Request.Form["txtRate3"] == null ? null : Request.Form["txtRate3"].ToString().Trim();
            txtRate4.Text = Request.Form["txtRate4"] == null ? null : Request.Form["txtRate4"].ToString().Trim();
            txtRate5.Text = Request.Form["txtRate5"] == null ? null : Request.Form["txtRate5"].ToString().Trim();
            txtRate6.Text = Request.Form["txtRate6"] == null ? null : Request.Form["txtRate6"].ToString().Trim();
            txtRate7.Text = Request.Form["txtRate7"] == null ? null : Request.Form["txtRate7"].ToString().Trim();
            txtRate8.Text = Request.Form["txtRate8"] == null ? null : Request.Form["txtRate8"].ToString().Trim();
            txtRate9.Text = Request.Form["txtRate9"] == null ? null : Request.Form["txtRate9"].ToString().Trim();
            txtRate10.Text = Request.Form["txtRate10"] == null ? null : Request.Form["txtRate10"].ToString().Trim();
            txtRate11.Text = Request.Form["txtRate11"] == null ? null : Request.Form["txtRate11"].ToString().Trim();
            txtRate12.Text = Request.Form["txtRate12"] == null ? null : Request.Form["txtRate12"].ToString().Trim();

            txtAmount1.Text = Request.Form["txtAmount1"] == null ? null : Request.Form["txtAmount1"].ToString().Trim();
            txtAmount2.Text = Request.Form["txtAmount2"] == null ? null : Request.Form["txtAmount2"].ToString().Trim();
            txtAmount3.Text = Request.Form["txtAmount3"] == null ? null : Request.Form["txtAmount3"].ToString().Trim();
            txtAmount4.Text = Request.Form["txtAmount4"] == null ? null : Request.Form["txtAmount4"].ToString().Trim();
            txtAmount5.Text = Request.Form["txtAmount5"] == null ? null : Request.Form["txtAmount5"].ToString().Trim();
            txtAmount6.Text = Request.Form["txtAmount6"] == null ? null : Request.Form["txtAmount6"].ToString().Trim();
            txtAmount7.Text = Request.Form["txtAmount7"] == null ? null : Request.Form["txtAmount7"].ToString().Trim();
            txtAmount8.Text = Request.Form["txtAmount8"] == null ? null : Request.Form["txtAmount8"].ToString().Trim();
            txtAmount9.Text = Request.Form["txtAmount9"] == null ? null : Request.Form["txtAmount9"].ToString().Trim();
            txtAmount10.Text = Request.Form["txtAmount10"] == null ? null : Request.Form["txtAmount10"].ToString().Trim();
            txtAmount11.Text = Request.Form["txtAmount11"] == null ? null : Request.Form["txtAmount11"].ToString().Trim();
            txtAmount12.Text = Request.Form["txtAmount12"] == null ? null : Request.Form["txtAmount12"].ToString().Trim();
        }

        /// <summary>
        /// This method returns the next Order No from Order_Master table.
        /// </summary>
        public void GetOrderInvoice()
        {
            try
            {
                List<string> orderInvoice = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/sales/GetOrderInvoice").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        orderInvoice = JsonConvert.DeserializeObject<List<string>>(id);
                    }
                }

                if (orderInvoice != null)
                {
                    foreach (var order in orderInvoice)
                        DropOrderInvoice.Items.Add(order);
                }

                //InventoryClass  obj=new InventoryClass ();
                //SqlDataReader SqlDtr;						//rdr=null;
                //string sql;
                //ArrayList order_no=new ArrayList();         // add by vikas 10.12.2012
                //#region Fetch the Next Invoice Number
                //DropOrderInvoice.Items.Clear();
                //DropOrderInvoice.Items.Add("Select");
                //sql="select Order_No from Order_col_Master where status=0 order by Order_No";
                //SqlDtr=obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	DropOrderInvoice.Items.Add(SqlDtr.GetValue(0).ToString());
                //	//coment by vikas 10.12.2012 order_no.Add(SqlDtr.GetValue(0).ToString());
                //}
                //SqlDtr.Close ();
                ///**********Add by vikas 10.12.2012***********************/
                //sql=" select distinct Order_id,bo_1,bo_2,bo_3 from ovd where cast(item_qty as float)>cast(sale_qty as float) ";
                //SqlDtr=obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	if(SqlDtr["Bo_3"].ToString()==null || SqlDtr["Bo_3"].ToString()=="")
                //	{
                //		if(SqlDtr["Bo_2"]==null || SqlDtr["Bo_2"].ToString()=="")
                //		{
                //			if(SqlDtr["Bo_1"].ToString()==null || SqlDtr["Bo_1"].ToString()=="")
                //			{
                //			}
                //			else
                //			{
                //				if(SqlDtr["Bo_1"].ToString()!=null && SqlDtr["Bo_1"].ToString()!="")
                //				{
                //					DropOrderInvoice.Items.Add("BO:"+SqlDtr["Bo_1"].ToString());
                //				}
                //			}
                //		}
                //		else 
                //		{
                //			if(SqlDtr["Bo_2"].ToString()!=null && SqlDtr["Bo_2"].ToString()!="")
                //			{
                //				DropOrderInvoice.Items.Add("BO:"+SqlDtr["Bo_2"].ToString());
                //			}
                //		}
                //	}
                //	else 
                //	{
                //		if(SqlDtr["Bo_3"].ToString()!=null && SqlDtr["Bo_3"].ToString()!="")
                //		{
                //			DropOrderInvoice.Items.Add("BO:"+SqlDtr["Bo_3"].ToString());
                //		}
                //	}
                //}
                //SqlDtr.Close ();
                /**********End***********************/
                //#endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:GetOrderInvoice,   Exception : " + ex.Message + " user : " + uid);
            }
        }

        public void DeleteSalesInvoice()
        {
            try
            {
                SalesModel sales = new SalesModel();
                string txtval = "";
                //DropDownList[] DropType={DropType1,DropType2,DropType3,DropType4,DropType5,DropType6,DropType7,DropType8,DropType9,DropType10,DropType11,DropType12};
                HtmlInputText[] DropType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };

                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                TextBox[] ProdType = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                TextBox[] ProdName1 = { txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12 };
                TextBox[] PackType1 = { txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12 };
                TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };

                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr = null;
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                //Con.Open();

                string strInvoiceNo = FromDate + ToDate + dropInvoiceNo.SelectedItem.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(strInvoiceNo);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/DeleteSalesMasterData?id=" + strInvoiceNo, byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        //var prodd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                    }
                }


                txtval = text1.Value.Substring(0, text1.Value.IndexOf(":"));


                sales.Invoice_ID = Int32.Parse(strInvoiceNo);
                sales.Cust_Name = txtval;
                sales.Net_Amount = float.Parse(txtNetAmount.Text);
                sales.Place = lblPlace.Value;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/UpdateAccountsLedgerCustomerLedger", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                List<string> ProductType1 = new List<string>();
                List<string> ProductName1 = new List<string>();
                List<string> ProductPack1 = new List<string>();
                List<string> ProductQty1 = new List<string>();

                List<string> SchProductType1 = new List<string>();
                List<string> SchProductName1 = new List<string>();
                List<string> SchProductPack1 = new List<string>();
                List<string> SchProductQty1 = new List<string>();

                SalesModel sales1 = new SalesModel();

                for (int i = 0; i < 12; i++)
                {
                    if (ProductType[i].ToString().Equals(""))
                        continue;
                    else
                    {
                        ProductQty1.Add(ProductQty[i].ToString());
                        ProductType1.Add(ProductType[i].ToString());
                        ProductName1.Add(ProductName[i].ToString());
                        ProductPack1.Add(ProductPack[i].ToString());
                        Invoice_Date = GenUtil.str2DDMMYYYY(Request.Form["tempInvoiceDate"].ToString());
                    }


                    if (ProdType[i].Text == "")
                        continue;
                    else
                    {

                        SchProductQty1.Add(SchProductQty[i].ToString());
                        SchProductType1.Add(SchProductType[i].ToString());
                        SchProductName1.Add(SchProductName[i].ToString());
                        SchProductPack1.Add(SchProductPack[i].ToString());
                    }
                }

                sales1.ProductType = ProductType1;
                sales1.ProductQty = ProductQty1;
                sales1.ProductName = ProductName1;
                sales1.ProductPack = ProductPack1;

                sales1.SchProductQty = SchProductQty1;
                sales1.SchProductType = SchProductType1;
                sales1.SchProductName = SchProductName1;
                sales1.SchProductPack = SchProductPack1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales1);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/UpdateStock_Master_SetSales", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales1);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/UpdateStock_Master_SetSalesFOC", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales1);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/Update_OVD_Sale_Trans_Id?id=" + strInvoiceNo, byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                /***********Add by vikas 16.11.2012*****************/

                //Con.Open();
                //cmd = new SqlCommand("Update OVD set Sale_Trans_Id='0' , sale_qty='0' where Sale_Trans_Id='" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + "'", Con);
                //cmd.ExecuteNonQuery();
                //Con.Close();
                //cmd.Dispose();

                /***********End*****************/
                SeqStockMaster(sales1);
                MessageBox.Show("Sales Transaction Deleted");
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " Deleted, user : " + uid);
                Clear();
                clear1();
                GetNextInvoiceNo();
                GetProducts();
                FetchData();
                //getschemefoe();
                getscheme();
                //getscheme1();
                lblInvoiceNo.Visible = true;
                dropInvoiceNo.Visible = false;
                btnEdit.Visible = true;
                isDeleteClicked = true;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " ,Exception : " + ex.Message + " user : " + uid);
            }
        }

        /// <summary>
        /// This method delete the particular invoice no from all tables but some information contain in master table.
        /// </summary>
        public void DeleteTheRec()
        {
            try
            {
                string txtval = "";
                //DropDownList[] DropType={DropType1,DropType2,DropType3,DropType4,DropType5,DropType6,DropType7,DropType8,DropType9,DropType10,DropType11,DropType12};
                HtmlInputText[] DropType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
                //***HtmlInputHidden[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12}; 
                //***HtmlInputHidden[] PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                TextBox[] ProdType = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                TextBox[] ProdName1 = { txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12 };
                TextBox[] PackType1 = { txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12 };
                TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };

                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr = null;
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                Con.Open();
                cmd = new SqlCommand("delete from Sales_Master where Invoice_No='" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                //				Con.Open();
                //				cmd = new SqlCommand("delete from monthwise1 where Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+"'",Con);
                //				cmd.ExecuteNonQuery();
                //				Con.Close();
                //				cmd.Dispose();
                Con.Open();
                cmd = new SqlCommand("delete from Sales_Oil where Invoice_No='" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                Con.Open();
                cmd = new SqlCommand("delete from Accountsledgertable where Particulars='Sales Invoice (" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                //string str="select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master where Ledger_Name='"+DropCustName.SelectedItem.Text+"') order by entry_date";
                txtval = text1.Value.Substring(0, text1.Value.IndexOf(":")); //Add by vikas sharma 
                                                                             //Comment by vikas 1.05.09 string str="select * from AccountsLedgerTable where Ledger_ID = (select Ledger_ID from Ledger_Master where Ledger_Name='"+text1.Value+"') order by entry_date";
                string str = "select * from AccountsLedgerTable where Ledger_ID = (select Ledger_ID from Ledger_Master where Ledger_Name='" + txtval + "') order by entry_date";
                rdr = obj.GetRecordSet(str);
                double Bal = 0;
                while (rdr.Read())
                {
                    if (rdr["Bal_Type"].ToString().Equals("Dr"))
                        Bal += double.Parse(rdr["Debit_Amount"].ToString()) - double.Parse(rdr["Credit_Amount"].ToString());
                    else
                        Bal += double.Parse(rdr["Credit_Amount"].ToString()) - double.Parse(rdr["Debit_Amount"].ToString());
                    if (Bal.ToString().StartsWith("-"))
                        Bal = double.Parse(Bal.ToString().Substring(1));
                    Con.Open();
                    cmd = new SqlCommand("update AccountsLedgerTable set Balance='" + Bal.ToString() + "' where Ledger_ID='" + rdr["Ledger_ID"].ToString() + "' and Particulars='" + rdr["Particulars"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    cmd.Dispose();
                }
                rdr.Close();
                Con.Open();
                cmd = new SqlCommand("delete from Customerledgertable where Particular='Sales Invoice (" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + ")'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                //string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"') order by entrydate";

                //Comment by vikas sharma 1.05.09 string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+text1.Value+"') order by entrydate";
                string str1 = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='" + txtval + "') order by entrydate";
                rdr = obj.GetRecordSet(str1);
                Bal = 0;
                while (rdr.Read())
                {
                    if (rdr["BalanceType"].ToString().Equals("Dr."))
                        Bal += double.Parse(rdr["DebitAmount"].ToString()) - double.Parse(rdr["CreditAmount"].ToString());
                    else
                        Bal += double.Parse(rdr["CreditAmount"].ToString()) - double.Parse(rdr["DebitAmount"].ToString());
                    if (Bal.ToString().StartsWith("-"))
                        Bal = double.Parse(Bal.ToString().Substring(1));
                    Con.Open();
                    cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + Bal.ToString() + "' where CustID='" + rdr["CustID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    cmd.Dispose();
                }
                rdr.Close();
                //				Con.Open();
                //				cmd = new SqlCommand("delete from LedgDetails where Bill_No='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+"'",Con);
                //				cmd.ExecuteNonQuery();
                //				Con.Close();
                //				cmd.Dispose();
                //				Con.Open();
                //				cmd = new SqlCommand("delete from Invoice_Transaction where Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+"'",Con);
                //				cmd.ExecuteNonQuery();
                //				Con.Close();
                //				cmd.Dispose();
                Con.Open();
                //cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'"+double.Parse(txtNetAmount.Text)+"' where Cust_ID = (select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"' and city='"+lblPlace.Value+"')",Con);

                //Comment by vikas sharma 1.05.09 cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'"+double.Parse(txtNetAmount.Text)+"' where Cust_ID = (select Cust_ID from Customer where Cust_Name='"+text1.Value+"' and city='"+lblPlace.Value+"')",Con);
                cmd = new SqlCommand("Update Customer_Balance set DR_Amount = DR_Amount-'" + double.Parse(txtNetAmount.Text) + "' where Cust_ID = (select Cust_ID from Customer where Cust_Name='" + txtval + "' and city='" + lblPlace.Value + "')", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();
                for (int i = 0; i < 12; i++)
                {
                    //if(DropType[i].SelectedItem.Text.Equals("Type") || ProdName[i].Value=="" || PackType[i].Value=="")
                    //if(DropType[i].SelectedItem.Text.Equals("Type"))
                    if (ProductType[i].ToString().Equals(""))
                        continue;
                    else
                    {
                        Con.Open();
                        //cmd = new SqlCommand("update Stock_Master set sales=sales-'"+double.Parse(Qty[i].Text)+"',closing_stock=closing_stock+'"+double.Parse(Qty[i].Text)+"' where ProductID=(select Prod_ID from Products where Category='"+DropType[i].SelectedItem.Text+"' and Prod_Name='"+ProdName[i].Value+"' and Pack_Type='"+PackType[i].Value+"') and cast(stock_date as smalldatetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
                        cmd = new SqlCommand("update Stock_Master set sales=sales-'" + double.Parse(ProductQty[i].ToString()) + "',closing_stock=closing_stock+'" + double.Parse(ProductQty[i].ToString()) + "' where ProductID=(select Prod_ID from Products where Category='" + ProductType[i].ToString() + "' and Prod_Name='" + ProductName[i].ToString() + "' and Pack_Type='" + ProductPack[i].ToString() + "') and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["tempInvoiceDate"].ToString()) + "',103)", Con);
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        cmd.Dispose();
                    }
                    //if(ProdType[i].Text=="" || ProdName1[i].Text=="" || PackType1[i].Text=="")
                    if (ProdType[i].Text == "")
                        continue;
                    else
                    {
                        Con.Open();
                        //cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-'"+double.Parse(Qty1[i].Text)+"',closing_stock=closing_stock+'"+double.Parse(Qty1[i].Text)+"' where ProductID=(select Prod_ID from Products where Category='"+ProdType[i].Text+"' and Prod_Name='"+ProdName1[i].Text+"' and Pack_Type='"+PackType1[i].Text+"') and cast(stock_date as smalldatetime)='"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+"'",Con);
                        cmd = new SqlCommand("update Stock_Master set salesfoc=salesfoc-'" + double.Parse(SchProductQty[i].ToString()) + "',closing_stock=closing_stock+'" + double.Parse(SchProductQty[i].ToString()) + "' where ProductID=(select Prod_ID from Products where Category='" + SchProductType[i].ToString() + "' and Prod_Name='" + SchProductName[i].ToString() + "' and Pack_Type='" + SchProductPack[i].ToString() + "') and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["tempInvoiceDate"].ToString()) + "',103)", Con);
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        cmd.Dispose();
                    }
                }

                /***********Add by vikas 16.11.2012*****************/

                Con.Open();
                cmd = new SqlCommand("Update OVD set Sale_Trans_Id='0' , sale_qty='0' where Sale_Trans_Id='" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + "'", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                cmd.Dispose();

                /***********End*****************/
                SeqStockMaster();
                MessageBox.Show("Sales Transaction Deleted");
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " Deleted, user : " + uid);
                Clear();
                clear1();
                GetNextInvoiceNo();
                GetProducts();
                FetchData();
                //getschemefoe();
                getscheme();
                //getscheme1();
                lblInvoiceNo.Visible = true;
                dropInvoiceNo.Visible = false;
                btnEdit.Visible = true;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " ,Exception : " + ex.Message + " user : " + uid);
            }
        }

        /// <summary>
        /// This method store all foe discount in a hidden textbox.
        /// </summary>
        public void getschemefoe()
        {
            try
            {
                string strInvoiceDate = lblInvoiceDate.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("/api/sales/Getschemefoe?invoiceDate=" + strInvoiceDate).Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                            temptextfoe.Value = res;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:getschemefoe() - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " ,Exception : " + ex.Message + " user : " + uid);
            }
        }

        /// <summary>
        /// This method store all Slip information (if given in customer) in a hidden textbox.
        /// </summary>
        public void getSlips()
        {
            try
            {
                SqlDataReader SqlDtr = null;
                string sql = "Select Slip_No from Sales_Master";
                dbobj.SelectQuery(sql, ref SqlDtr);
                string temp = "";
                while (SqlDtr.Read())
                {
                    temp = temp + SqlDtr.GetValue(0).ToString() + "#";
                }
                SqlDtr.Close();
                txtSlipTemp.Value = temp;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:getSlips() - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " ,Exception : " + ex.Message + " user : " + uid);
            }
        }

        /// <summary>
        /// This method checks the pre print template file and disables the pre print button.
        /// </summary>
        public void checkPrePrint()
        {
            try
            {
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\PrePrintTemplate.INI";
                StreamReader sr = new StreamReader(path);
                Button1.Enabled = true;
                sr.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                Button1.Enabled = false;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:checkPrePrint() - InvoiceNo : " + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text + " ,Exception : " + ex.Message + " user : " + uid);
            }
        }

        /// <summary>
        /// This method checks the user privileges from session.
        /// </summary>
        public void checkPrevileges()
        {
            #region Check Privileges
            int i;
            string View_flag = "0", Add_Flag = "0", Edit_Flag = "0", Del_Flag = "0";
            string Module = "4";
            string SubModule = "4";
            string[,] Priv = (string[,])Session["Privileges"];
            for (i = 0; i < Priv.GetLength(0); i++)
            {
                if (Priv[i, 0] == Module && Priv[i, 1] == SubModule)
                {
                    View_flag = Priv[i, 2];
                    Add_Flag = Priv[i, 3];
                    Edit_Flag = Priv[i, 4];
                    Del_Flag = Priv[i, 5];
                    break;
                }
            }
            if (View_flag == "0")
            {
                Response.Redirect("../../Sysitem/AccessDeny.aspx", false);
                return;
            }
            if (Edit_Flag == "0")
                btnEdit.Enabled = false;
            if (Del_Flag == "0")
                btnDelete.Enabled = false;
            if (Add_Flag == "0")
            {
                btnSave.Enabled = false;
                Button1.Enabled = false;
            }
            #endregion
        }
        // Fetch the customer information and put it into a hiddent fields for java script.
        //		public void FetchData()
        //		{
        //			InventoryClass obj=new InventoryClass ();
        //			InventoryClass obj1=new InventoryClass ();
        //			SqlDataReader rdr=null;
        //			SqlDataReader rdr1=null;
        //			SqlDataReader rdr2=null;
        //			//SqlDataReader rdr3=null;
        //			string str1="";
        //			//string str2 ="";
        //			DateTime duedate;
        //			string duedatestr ="";
        //			IEnumerator enum1=DropCustName.Items.GetEnumerator();
        //			enum1.MoveNext(); 
        //			while(enum1.MoveNext())
        //			{
        //				string s=enum1.Current.ToString(); 
        //				dbobj.SelectQuery("select City,CR_Days,Curr_Credit,Cust_ID,SSR  from Customer where Cust_Name='"+s+"'",ref rdr);
        //				if(rdr.Read())
        //				{
        //					duedate=DateTime.Now.AddDays(System.Convert.ToDouble(rdr["CR_Days"]));
        //					duedatestr=(duedate.ToShortDateString());
        //					//str1 = str1+s.Trim()+"~"+rdr["City"].ToString().Trim()+"~"+GenUtil.str2DDMMYYYY(duedatestr.Trim())+"~"+GenUtil.strNumericFormat(rdr["Curr_Credit"].ToString().Trim())+"~"+rdr["Cust_ID"].ToString().Trim()+"~";
        //					str1 = str1+s.Trim()+"~"+rdr["City"].ToString().Trim()+"~"+GenUtil.str2DDMMYYYY(duedatestr.Trim())+"~"+GenUtil.strNumericFormat(rdr["Curr_Credit"].ToString().Trim())+"~";
        //					dbobj.SelectQuery("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr["Cust_ID"]+" order by EntryDate Desc", ref rdr1);
        //					if(rdr1.Read())
        //					{
        //						str1 = str1+GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim())+"~"+rdr1["BalanceType"].ToString().Trim()+"~";	
        //						//str1 = str1+GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim())+"~"+rdr1["BalanceType"].ToString().Trim()+"#";	
        //					}
        //					else
        //					{
        //						str1 = str1+"0"+"~"+" "+"~";
        //						//str1 = str1+"0"+"~"+" "+"#";	
        //					}
        //					rdr2=obj1.GetRecordSet("select Emp_Name  from  Employee where Emp_ID='"+rdr["SSR"]+"'");
        //					if(rdr2.Read())
        //						str1+=rdr2["Emp_Name"].ToString()+"#";
        //					else
        //						str1+=" #";
        //					rdr2.Close();
        //					rdr1.Close();
        //					/**
        //					dbobj.SelectQuery("select Start_No,End_No  from  Slip where Cust_ID='"+rdr["Cust_ID"]+"'",ref rdr2);
        //					if(rdr2.Read())
        //					{
        //						if(rdr2["Start_No"].ToString()!="" && rdr2["End_No"].ToString()!="")
        //						{  
        //							str1 = str1+rdr2["Start_No"].ToString().Trim()+"~"+rdr2["End_No"].ToString().Trim()+"#"; 
        //						}
        //						else
        //						{ 
        //							str1 = str1+"0~0#";
        //						}
        //					}
        //					else
        //					{
        //						str1 = str1+"0~0#";
        //					} 
        //					rdr2.Close();
        //
        //					dbobj.SelectQuery("Select * from Customer_Vehicles where Cust_ID="+rdr["Cust_ID"],ref rdr3);  
        //					if(rdr3.HasRows)
        //					{
        //						str2 = str2 + s+"~";
        //						while(rdr3.Read())
        //						{
        //							if(!rdr3["Vehicle_No1"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No1"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No2"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No2"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No3"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No3"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No4"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No4"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No5"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No5"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No6"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No6"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No7"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No7"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No8"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No8"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No9"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No9"].ToString().Trim()+"~";
        //							if(!rdr3["Vehicle_No10"].ToString().Trim().Equals(""))
        //								str2 = str2 + rdr3["Vehicle_No10"].ToString().Trim()+"~";
        //						}
        //						str2 = str2 + "#";
        //					}
        //					rdr3.Close(); 
        //					*/
        //				}
        //				rdr.Close();
        //				
        //			}
        //			TxtVen.Value =str1; 
        //			//MessageBox.Show(str1);
        //			//lblVehicleNo.Value = str2;
        //
        //			//Response.Write(str2); 
        //		}

        /// <summary>
        /// This method store all customer information in a hidden textbox.
        /// </summary>
        public void FetchData()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("api/sales/GetFetchData").Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;

                            //sales = JsonConvert.DeserializeObject<SalesModel>(res);
                            TxtVen.Value = res;
                        }
                    }
                }
                //InventoryClass obj=new InventoryClass();
                //InventoryClass obj1=new InventoryClass();
                //SqlDataReader rdr1=null;
                //SqlDataReader rdr3=null;
                //string str1="";
                //DateTime duedate;
                //string duedatestr ="";

                ////coment by vikas 25.10.2012 rdr3 = obj.GetRecordSet("select c.City,CR_Days,Curr_Credit,Cust_ID,SSR,Cust_Name,Emp_Name  from Customer c,Employee e where e.Emp_ID=c.SSR order by Cust_Name");

                //rdr3 = obj.GetRecordSet("select c.City,CR_Days,Curr_Credit,Cust_ID,SSR,Cust_Name,Emp_Name,ct.group_name  from Customer c,Employee e,customertype ct where e.Emp_ID=c.SSR and c.cust_type=ct.customertypename order by Cust_Name");
                //while(rdr3.Read())
                //{
                //	duedate=DateTime.Now.AddDays(System.Convert.ToDouble(rdr3["CR_Days"]));
                //	duedatestr=duedate.ToShortDateString();
                //	str1 = str1+rdr3["Cust_Name"].ToString()+"~"+rdr3["City"].ToString().Trim()+"~"+GenUtil.str2DDMMYYYY(duedatestr.Trim())+"~"+GenUtil.strNumericFormat(rdr3["Curr_Credit"].ToString().Trim())+"~";
                //	rdr1 = obj1.GetRecordSet("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr3["Cust_ID"]+" order by EntryDate Desc");
                //	//dbobj.SelectQuery("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr3["Cust_ID"]+" order by EntryDate Desc",ref rdr1);
                //	if(rdr1.Read())
                //	{
                //		string str15 = GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim())+"~"+rdr1["BalanceType"].ToString().Trim()+"~";	
                //		str1 = str1+GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim())+"~"+rdr1["BalanceType"].ToString().Trim()+"~";	
                //	}
                //	else
                //	{
                //		str1 = str1+"0"+"~"+" "+"~";	
                //	}
                //	rdr1.Close();
                //	//coment by vikas 25.10.2012 str1+=rdr3["Emp_Name"].ToString()+"#";
                //	str1+=rdr3["Emp_Name"].ToString()+"~"+rdr3["group_name"].ToString()+"#";
                //}
                //rdr3.Close();
                //TxtVen.Value =str1; 
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method : FetchData()  EXCEPTION :  " + ex.Message + "   " + uid);
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

        public void Type_Changed(DropDownList ddType, DropDownList ddProd, DropDownList ddPack)
        {
            try
            {
                ddProd.Items.Clear();
                ddProd.Items.Add("Select");
                ddPack.Items.Clear();
                if (ddType.SelectedItem.Value.ToUpper() == "FUEL")
                    ddPack.Enabled = false;
                else
                {
                    ddPack.Enabled = true;
                    ddPack.Items.Add("Select");
                }
                if (ddType.SelectedIndex == 0)
                    return;
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;

                #region Fetch Product Name and fill in the ComboBox
                sql = "select distinct p.Prod_Name from Products p,Price_Updation pu where p.Category='" + ddType.SelectedItem.Value + "' and p.Prod_ID=pu.Prod_ID";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    ddProd.Items.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:Type_Changed().   EXCEPTION: " + ex.Message + "  User_ID: " + uid);
            }
        }

        private void DropType1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType4_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType5_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType6_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType7_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropType8_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// Change the package type according to product and fatch the saleRate of the product from price updation table.
        /// </summary>
        /// <param name="ddType"></param>
        /// <param name="ddProd"></param>
        /// <param name="ddPack"></param>
        /// <param name="txtPurRate"></param>
        public void Prod_Changed(DropDownList ddType, DropDownList ddProd, DropDownList ddPack, TextBox txtPurRate)
        {
            try
            {
                ddPack.Items.Clear();
                txtPurRate.Text = "";
                if (ddProd.SelectedIndex == 0)
                    return;
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                #region Fetch Package Types Regarding Product Name			
                sql = "Select Pack_Type from Products where Prod_Name='" + ddProd.SelectedItem.Value + "' and Category='" + ddType.SelectedItem.Value + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    ddPack.Items.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                #endregion

                #region Fetch Sales Rate Regarding Product Name		
                sql = "select top 1 Pur_Rate from Price_Updation where Prod_ID=(select  Prod_ID from Products where Prod_Name='" + ddProd.SelectedItem.Value + "' and Pack_Type='" + ddPack.SelectedItem.Value + "') order by eff_date desc";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    txtPurRate.Text = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method : FetchData()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }
        private void DropProd1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd4_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd5_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd6_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd7_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd8_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// This function are not used.
        /// </summary>
        private string GetString(string str, string spc)
        {
            if (str.Length > spc.Length)
                return str;
            else
                return str + spc.Substring(0, spc.Length - str.Length) + "  ";
        }

        //This function are not used.
        private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr, ref int len1, ref int len2, ref int len3, ref int len4, ref int len5, ref int len6, ref int len7, ref int len8, ref int len9, ref int len10, ref int len11, ref int len12, ref int len13, ref int len14, ref int len15, ref int len16, ref int len17, ref int len18, ref int len19, ref int len20, ref int len21, ref int len22, ref int len23, ref int len24, ref int len25, ref int len26, ref int len27, ref int len28, ref int len29, ref int len30, ref int len31, ref int len32, ref int len33, ref int len34, ref int len35, ref int len36, ref int len37, ref int len38, ref int len39, ref int len40, ref int len41, ref int len42)
        {
            try
            {
                while (rdr.Read())
                {
                    if (rdr["InvoiceNo"].ToString().Trim().Length > len1)
                        len1 = rdr["InvoiceNo"].ToString().Trim().Length;
                    if (rdr["ToDate"].ToString().Trim().Length > len2)
                        len2 = rdr["ToDate"].ToString().Trim().Length;
                    if (rdr["CustomerName"].ToString().Trim().Length > len3)
                        len3 = rdr["CustomerName"].ToString().Trim().Length;
                    if (rdr["Place"].ToString().Trim().Length > len4)
                        len4 = rdr["Place"].ToString().Trim().Length;
                    if (rdr["DueDate"].ToString().Trim().Length > len5)
                        len5 = rdr["DueDate"].ToString().Trim().Length;
                    if (rdr["CurrentBalance"].ToString().Trim().Length > len6)
                        len6 = rdr["CurrentBalance"].ToString().Trim().Length;
                    if (rdr["VehicleNo"].ToString().Trim().Length > len7)
                        len7 = rdr["VehicleNo"].ToString().Trim().Length;
                    if (rdr["Prod1"].ToString().Trim().Length > len8)
                        len8 = rdr["Prod1"].ToString().Trim().Length;
                    if (rdr["Prod2"].ToString().Trim().Length > len9)
                        len9 = rdr["Prod2"].ToString().Trim().Length;
                    if (rdr["Prod3"].ToString().Trim().Length > len10)
                        len10 = rdr["Prod3"].ToString().Trim().Length;
                    if (rdr["Prod4"].ToString().Trim().Length > len11)
                        len11 = rdr["Prod4"].ToString().Trim().Length;
                    if (rdr["Prod5"].ToString().Trim().Length > len12)
                        len12 = rdr["Prod5"].ToString().Trim().Length;
                    if (rdr["Prod6"].ToString().Trim().Length > len13)
                        len13 = rdr["Prod6"].ToString().Trim().Length;
                    if (rdr["Prod7"].ToString().Trim().Length > len41)
                        len41 = rdr["Prod7"].ToString().Trim().Length;
                    if (rdr["Prod8"].ToString().Trim().Length > len42)
                        len42 = rdr["Prod8"].ToString().Trim().Length;
                    if (rdr["Qty1"].ToString().Trim().Length > len14)
                        len14 = rdr["Qty1"].ToString().Trim().Length;
                    if (rdr["Qty2"].ToString().Trim().Length > len15)
                        len15 = rdr["Qty2"].ToString().Trim().Length;
                    if (rdr["Qty3"].ToString().Trim().Length > len16)
                        len16 = rdr["Qty3"].ToString().Trim().Length;
                    if (rdr["Qty4"].ToString().Trim().Length > len17)
                        len17 = rdr["Qty4"].ToString().Trim().Length;
                    if (rdr["Qty5"].ToString().Trim().Length > len18)
                        len18 = rdr["Qty5"].ToString().Trim().Length;
                    if (rdr["Qty6"].ToString().Trim().Length > len19)
                        len19 = rdr["Qty6"].ToString().Trim().Length;
                    if (rdr["Qty7"].ToString().Trim().Length > len20)
                        len20 = rdr["Qty7"].ToString().Trim().Length;
                    if (rdr["Qty8"].ToString().Trim().Length > len21)
                        len21 = rdr["Qty8"].ToString().Trim().Length;
                    if (rdr["Rate1"].ToString().Trim().Length > len22)
                        len22 = rdr["Rate1"].ToString().Trim().Length;
                    if (rdr["Rate2"].ToString().Trim().Length > len23)
                        len23 = rdr["Rate2"].ToString().Trim().Length;
                    if (rdr["Rate3"].ToString().Trim().Length > len24)
                        len24 = rdr["Rate3"].ToString().Trim().Length;
                    if (rdr["Rate4"].ToString().Trim().Length > len25)
                        len25 = rdr["Rate4"].ToString().Trim().Length;
                    if (rdr["Rate5"].ToString().Trim().Length > len26)
                        len26 = rdr["Rate5"].ToString().Trim().Length;
                    if (rdr["Rate6"].ToString().Trim().Length > len27)
                        len27 = rdr["Rate6"].ToString().Trim().Length;
                    if (rdr["Rate7"].ToString().Trim().Length > len28)
                        len28 = rdr["Rate7"].ToString().Trim().Length;
                    if (rdr["Rate8"].ToString().Trim().Length > len29)
                        len29 = rdr["Rate8"].ToString().Trim().Length;
                    if (rdr["Amt1"].ToString().Trim().Length > len30)
                        len30 = rdr["Amt1"].ToString().Trim().Length;
                    if (rdr["Amt2"].ToString().Trim().Length > len31)
                        len31 = rdr["Amt2"].ToString().Trim().Length;
                    if (rdr["Amt3"].ToString().Trim().Length > len32)
                        len32 = rdr["Amt3"].ToString().Trim().Length;
                    if (rdr["Amt4"].ToString().Trim().Length > len33)
                        len33 = rdr["Amt4"].ToString().Trim().Length;
                    if (rdr["Amt5"].ToString().Trim().Length > len34)
                        len34 = rdr["Amt5"].ToString().Trim().Length;
                    if (rdr["Amt6"].ToString().Trim().Length > len35)
                        len35 = rdr["Amt6"].ToString().Trim().Length;
                    if (rdr["Amt7"].ToString().Trim().Length > len36)
                        len36 = rdr["Amt7"].ToString().Trim().Length;
                    if (rdr["Amt8"].ToString().Trim().Length > len37)
                        len37 = rdr["Amt8"].ToString().Trim().Length;
                    if (rdr["Total"].ToString().Trim().Length > len38)
                        len38 = rdr["Total"].ToString().Trim().Length;
                    if (rdr["Promo"].ToString().Trim().Length > len39)
                        len39 = rdr["Promo"].ToString().Trim().Length;
                    if (rdr["Remarks"].ToString().Trim().Length > len40)
                        len40 = rdr["Remarks"].ToString().Trim().Length;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method : getMaxLen()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        private string GetString(string str, int maxlen, string spc)
        {
            return str + spc.Substring(0, maxlen > str.Length ? maxlen - str.Length : str.Length - maxlen);
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        private string MakeString(int len)
        {
            string spc = "";
            for (int x = 0; x < len; x++)
                spc += " ";
            return spc;
        }

        /// <summary>
        /// This method writes a line to a report file.
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="info"></param>
        public void Write2File(StreamWriter sw, string info)
        {
            sw.WriteLine(info);
        }

        /// <summary>
        /// This method saves the products sales info into Sales_Details table.
        /// </summary>
        /// <param name="ProdName"></param>
        /// <param name="PackType"></param>
        /// <param name="Qty"></param>
        /// <param name="Rate"></param>
        /// <param name="Amount"></param>
        /// <param name="Qty1"></param>
        /// <param name="Invoice_date"></param>
        /// <param name="scheme"></param>
        /// <param name="foe"></param>
        /// <param name="sno"></param>
        public void Save(string ProdName, string PackType, string Qty, string Rate, string Amount, string Qty1, string Invoice_date, string scheme, string foe, int sno, string SecSPType, string SecSP, string FoeType, string SchType)
        {
            try
            {
                SalesDetailsModel obj = new SalesDetailsModel();
                //obj.Prod_ID = 
                obj.Product_Name = ProdName;
                obj.Package_Type = PackType;
                obj.Qty = Qty;
                obj.QtyTemp = Qty1;
                obj.Rate = Rate;
                obj.sno = sno += 1;
                //obj.prod_id=prodid;
                //obj.tempQty=OldQty;
                obj.Amount = Amount;
                //obj.Inv_date = Invoice_date;
                obj.Invoice_Date = System.Convert.ToDateTime(Invoice_date);
                obj.sch = scheme;
                obj.schtype = SchType;
                obj.SecSPDisc = SecSP;
                obj.foediscounttype = FoeType;
                obj.SecSPDiscType = SecSPType;
                if (foe == "")
                    obj.foe = "0";
                else
                    obj.foe = foe;
                if (lblInvoiceNo.Visible == true)
                {
                    obj.Invoice_No = FromDate + ToDate + lblInvoiceNo.Text;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(obj);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/sales/InsertSalesDetail", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    //obj.InsertSalesDetail();
                }
                else
                {
                    obj.Invoice_No = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(obj);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/sales/InsertSalesDetail", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    //obj.InsertSalesDetail(); 
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : Save()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        //This Function add by vikas 12.11.2012
        public void Save_OVD(string ProdName, string PackType, string Qty, string Rate, string Amount, string Qty1, string Invoice_date, string scheme, string foe, int sno, string SecSPType, string SecSP, string FoeType, string SchType)
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                obj.Product_Name = ProdName;
                obj.Package_Type = PackType;
                obj.Qty = Qty;
                obj.QtyTemp = Qty1;
                obj.Rate = Rate;
                obj.sno = sno += 1;
                obj.Amount = Amount;
                obj.Invoice_Date = System.Convert.ToDateTime(Invoice_date);
                obj.sch = scheme;
                obj.schtype = SchType;
                obj.SecSPDisc = SecSP;
                obj.foediscounttype = FoeType;
                obj.SecSPDiscType = SecSPType;
                if (foe == "")
                    obj.foe = "0";
                else
                    obj.foe = foe;
                if (lblInvoiceNo.Visible == true)
                {
                    obj.Invoice_No = FromDate + ToDate + lblInvoiceNo.Text;
                    obj.InsertSalesDetail();
                }
                else
                {
                    obj.Invoice_No = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;
                    obj.InsertSalesDetail();
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : Save()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        //*****bhal****
        /// <summary>
        /// This method saves the FOC products sales info into Sales_Details table.
        /// </summary>
        public void Save1(string ProdName, string PackType, string Qty, string Invoice_date, string scheme, int sno, string Qty1)
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                obj.Product_Name = ProdName;
                obj.Package_Type = PackType;
                obj.sno = sno;
                if (Qty.Equals(""))
                    obj.Qty = "0";
                else
                    obj.Qty = Qty;
                obj.tempQty = Qty1;
                //obj.sch=scheme;
                //obj.Inv_date = Invoice_date;
                obj.Invoice_Date = System.Convert.ToDateTime(Invoice_date);
                if (lblInvoiceNo.Visible == true)
                {
                    obj.Invoice_No = FromDate + ToDate + lblInvoiceNo.Text;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(obj);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/sales/InsertSaleSchemeDetail", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    //obj.InsertSaleSchemeDetail();
                }
                else
                {
                    obj.Invoice_No = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(obj);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/sales/InsertSaleSchemeDetail", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    //obj.InsertSaleSchemeDetail();
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : Save1()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }
        //*********

        /// <summary>
        /// this method stored starting and end slip no information in every customer.
        /// </summary>
        public void compareSlipNo()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                sql = "select Start_No,End_No  from  Slip where Cust_ID='" + TextBox1 + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    Txtstart.Value = SqlDtr.GetValue(0).ToString();
                    TxtEnd.Value = SqlDtr.GetValue(1).ToString();
                }
                SqlDtr.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : compareSlipNo()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// It calls the save_updateInvoice() function to save or update the Invoice Details and calls the reportmaking4() fucntion to creates the print file and calls the print() code fire the print of passing file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            InventoryClass obj = new InventoryClass();
            try
            {
                /***********Add by vikas 15.07.09 ***********************************/
                if (btnEdit.Visible == true)
                {
                    if (DropSalesType.SelectedItem.Text.Equals("Cash"))
                    {
                        double net = 0;
                        string str = lblCurrBalance.Value.ToString();
                        string[] str1 = str.Split(new char[] { ' ' }, str.Length);
                        if (str1[1].Equals("Cr."))
                        {
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Current Balance is less than Net Amount");
                            return;
                        }

                        double cr = System.Convert.ToDouble(str1[0].ToString());
                        if (cr > net)
                        {
                            save_updateInvoive();            //Add by Vikas 15.07..09
                        }
                        else
                        {
                            MessageBox.Show("Current Balance is less than Net Amount");
                            return;
                        }
                    }
                    else if (DropSalesType.SelectedItem.Text.Equals("Van"))      //Add by vikas 15.07.09
                    {
                        if (txtChallanNo.Text != "")
                        {
                            save_updateInvoive();
                        }
                        else
                        {
                            MessageBox.Show("Challan No can not be blank");
                            return;
                        }
                    }
                    if (DropSalesType.SelectedItem.Text.Equals("Credit"))
                    {

                        double net;
                        string s = TxtCrLimit.Value.ToString();
                        string s1 = TxtCrLimit1.Text.ToString();
                        double cr = System.Convert.ToDouble(TxtCrLimit.Value.ToString());
                        string str = lblCurrBalance.Value.ToString();
                        string[] str1 = str.Split(new char[] { ' ' }, str.Length);
                        if (str1[1].Equals("Cr."))
                            //13.07.09 vikas net=System.Convert.ToDouble(txtNetAmount.Text.ToString());
                            //21.3.2013 Vikas net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString()) - System.Convert.ToDouble(str1[0].ToString());
                        else
                            //13.07.09 vikas net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
                            //21.3.2013 net=System.Convert.ToDouble(txtNetAmount.Text.ToString());
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString());
                        if (cr >= net)
                        {
                            save_updateInvoive();            //Add by Vikas 13.07..09
                        }
                        else
                        {
                            MessageBox.Show("Credit Limit is less than Net Amount");
                            return;
                        }
                    }
                }
                else
                {
                    save_updateInvoive();
                }
                //save_updateInvoive();

                tempEdit.Value = "True";           //Add by vikas 14.07.09
                if (flag == 0)
                {
                    Clear();
                    clear1();
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:btnSave_Click - InvoiceNo : " + FromDate + ToDate + lblInvoiceNo.Text);
                    GetNextInvoiceNo();
                    GetProducts();
                    FetchData();
                    getscheme();
                    lblInvoiceNo.Visible = true;
                    dropInvoiceNo.Visible = false;
                    btnEdit.Visible = true;
                    Button1.Enabled = true;
                }
                else
                {
                    flag = 0;
                    return;
                }
                GetOrderInvoice();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog(" Form : SalesInvoise.aspx ,Method : save_updateInvoice() ,Class:InventoryClass" + "  Sales Invoise for  Invoice No." + obj.Invoice_No + " ," + "for Customer Name  " + obj.Customer_Name + "  Under Salesman " + obj.Under_SalesMan + " and NetAmount  " + obj.Net_Amount + "  is Saved " + "  EXCEPTION  " + ex.Message + " userid " + "   " + "   " + uid);
            }
        }


        /// This method save or update the sales information.

        public void save_updateInvoive()
        {
            SalesSaveDetailsModel obj = new SalesSaveDetailsModel();
            try
            {
                if (lblInvoiceNo.Visible == true)
                {
                    int count = 0;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        string date = FromDate + ToDate + lblInvoiceNo.Text.Trim();

                        var Res = client.GetAsync("api/sales/GetCountofInvoiceNo?invoiceNo=" + date).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            count = JsonConvert.DeserializeObject<int>(id);
                        }
                    }

                    // This part of code is use to solve the double click problem, Its checks the sales invoice no. and display the popup, that it is saved.
                    //dbobj.ExecuteScalar("Select count(Invoice_No) from Sales_Master where Invoice_No = "+FromDate+ToDate+lblInvoiceNo.Text.Trim(),ref count);
                    if (count > 0)
                    {
                        MessageBox.Show("Sales Invoice Saved");
                        Clear();
                        clear1();
                        GetNextInvoiceNo();
                        GetProducts();
                        FetchData();
                        getscheme();
                        lblInvoiceNo.Visible = true;
                        dropInvoiceNo.Visible = false;
                        btnEdit.Visible = true;
                        btnSave.Enabled = true;
                        Button1.Enabled = true;
                        flag = 1;
                        return;
                    }
                }

                obj.Invoice_Date = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()));//GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString())

                //Coment by vikas 11.12.2012 if(DropOrderInvoice.SelectedItem.Text=="Select")
                //Coment by vikas 11.12.2012 	obj.Order_No="0";
                //Coment by vikas 11.12.2012 else
                //Coment by vikas 11.12.2012 	obj.Order_No=DropOrderInvoice.SelectedItem.Text;*/

                /*********Add by vikas 11.12.12********************************/
                string[] Order_No = DropOrderInvoice.SelectedItem.Text.Split(new char[] { ':' });
                int Count = Order_No.Length;
                if (Count == 1)
                {
                    if (DropOrderInvoice.SelectedItem.Text == "Select")
                        obj.Order_No = "0";
                    else
                        obj.Order_No = DropOrderInvoice.SelectedItem.Text;
                }
                else
                {
                    obj.Order_No = "0";
                }
                /***************************End********************************/



                obj.Sales_Type = DropSalesType.SelectedItem.Value;
                obj.Under_SalesMan = DropUnderSalesMan.SelectedItem.Value;

                //obj.Customer_Name =text1.Value ;  //Comment By Vikas sharma 27.04.09

                /***************Add By Vikas sharma 27.04.09******************************/
                string txtval = text1.Value;
                txtval = txtval.Substring(0, txtval.IndexOf(":"));
                obj.Customer_Name = txtval;
                /* ***************end *****************************/

                obj.Place = lblPlace.Value.ToString();
                if (DropVehicleNo.Visible == true)
                    obj.Vehicle_No = DropVehicleNo.SelectedItem.Text;
                else
                    obj.Vehicle_No = txtVehicleNo.Text;
                obj.Grand_Total = Request.Form["txtGrandTotal"].ToString();
                if (txtDisc.Text == "")
                    obj.Discount = "0.0";
                else
                    obj.Discount = txtDisc.Text;
                obj.Discount_Type = DropDiscType.SelectedItem.Value;
                obj.Net_Amount = Request.Form["txtNetAmount"].ToString();
                obj.Promo_Scheme = txtPromoScheme.Text;
                obj.Remerk = txtRemark.Text;
                obj.Entry_By = lblEntryBy.Text;
                obj.Entry_Time = DateTime.Parse(lblEntryTime.Text);
                if (txtCashDisc.Text.Trim() == "")
                    obj.Cash_Discount = "0.0";
                else
                    obj.Cash_Discount = txtCashDisc.Text.Trim();
                obj.Cash_Disc_Type = DropCashDiscType.SelectedItem.Value;
                obj.VAT_Amount = txtVAT.Text.Trim();
                obj.CGST_Amount = Request.Form["Textcgst"];
                obj.SGST_Amount = Request.Form["Textsgst"];
                obj.Slip_No = "0";
                obj.Cr_Plus = "0";
                obj.Dr_Plus = Request.Form["txtNetAmount"].ToString();
                obj.Credit_Limit = lblCreditLimit.Value.ToString();
                obj.schdiscount = txtschemetotal.Text.Trim();
                if (txtfleetoediscount.Text.Equals(""))
                    obj.foediscount = "0";
                else
                    obj.foediscount = txtfleetoediscount.Text.Trim();
                obj.foediscounttype = dropfleetoediscount.SelectedItem.Text.ToString();
                obj.foediscountrs = txtfleetoediscountRs.Text.Trim();
                obj.totalqtyltr = txtliter.Text.Trim().ToString();
                if (txtChallanDate.Text == "")
                    obj.ChallanDate = "";
                else
                    obj.ChallanDate = GenUtil.str2DDMMYYYY(Request.Form["txtChallanDate"].ToString());
                if (txtChallanNo.Text == "")
                    obj.ChallanNo = "0";
                else
                    obj.ChallanNo = txtChallanNo.Text;
                if (txtSecondrySpDisc.Text == "")
                    obj.SecSPDisc = "0";
                else
                    obj.SecSPDisc = txtSecondrySpDisc.Text;
                if (lblInvoiceNo.Visible == true)
                {
                    //if(DropSalesType.SelectedItem.Text.Equals("Credit") || DropSalesType.SelectedItem.Text.Equals("Cash"))
                    if (DropSalesType.SelectedItem.Text.Equals("Credit"))
                    {
                        double net;
                        string s = TxtCrLimit.Value.ToString();
                        string s1 = TxtCrLimit1.Text.ToString();
                        double cr = System.Convert.ToDouble(TxtCrLimit.Value.ToString());
                        string str = lblCurrBalance.Value.ToString();
                        string[] str1 = str.Split(new char[] { ' ' }, str.Length);
                        if (str1[1].Equals("Cr."))
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString()) - System.Convert.ToDouble(str1[0].ToString());
                        else
                            //net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString());

                        if (cr >= net)
                        {
                            obj.Invoice_No = FromDate + ToDate + lblInvoiceNo.Text;

                            //Insert data to SalesMaster
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(obj);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/InsertSalesMaster", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //Update data to CustomerBalance
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(obj);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/UpdateCustomerBalance", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //obj.InsertSalesMaster();
                            //obj.UpdateCustomerBalance(); 
                            insertSalesOil();
                        }
                        else
                        {
                            MessageBox.Show("Credit Limit is less than Net Amount");
                            return;
                        }
                    }
                    else
                    {
                        obj.Invoice_No = FromDate + ToDate + lblInvoiceNo.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(baseUri);
                            var myContent = JsonConvert.SerializeObject(obj);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/sales/InsertSalesMaster", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                            }
                        }

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(baseUri);
                            var myContent = JsonConvert.SerializeObject(obj);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/sales/UpdateCustomerBalance", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                            }
                        }

                        //obj.InsertSalesMaster();
                        //obj.UpdateCustomerBalance();
                        insertSalesOil();
                    }
                }
                else
                {
                    if (DropSalesType.SelectedItem.Text.Equals("Credit"))
                    {
                        double net;
                        string s = TxtCrLimit.Value.ToString();
                        string s1 = TxtCrLimit1.Text.ToString();
                        double cr = System.Convert.ToDouble(TxtCrLimit.Value.ToString());
                        string str = lblCurrBalance.Value.ToString();
                        string[] str1 = str.Split(new char[] { ' ' }, str.Length);

                        cr += System.Convert.ToDouble(str1[0].ToString());                          //Add by vikas 15.09.09

                        if (str1[1].Equals("Cr."))
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString()) - System.Convert.ToDouble(str1[0].ToString());
                        else
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString());

                        if (cr < net)
                        {
                            MessageBox.Show("Credit Limit is less than Net Amount");
                            return;
                        }
                    }
                    int x = 0;

                    List<string> sales = new List<string>();
                    sales.Add(FromDate.ToString() + ToDate + dropInvoiceNo.SelectedItem.Text);
                    sales.Add(NetAmount);
                    sales.Add(CustID);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        var myContent = JsonConvert.SerializeObject(sales);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/sales/InsUpdateCustLedgerAcctLedgerCustomer", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    //               dbobj.Insert_or_Update("delete from customerledgertable where particular='Sales Invoice ("+FromDate.ToString()+ToDate+dropInvoiceNo.SelectedItem.Text+")'",ref x);
                    //dbobj.Insert_or_Update("delete from AccountsLedgerTable where particulars='Sales Invoice ("+FromDate.ToString()+ToDate+dropInvoiceNo.SelectedItem.Text+")'",ref x);
                    //dbobj.Insert_or_Update("update customer set Curr_Credit=Curr_Credit+"+NetAmount+" where Cust_ID='"+CustID+"'",ref x);

                    if (DropSalesType.SelectedItem.Text.Equals("Credit"))
                    {
                        double net;
                        string s = TxtCrLimit.Value.ToString();
                        string s1 = TxtCrLimit1.Text.ToString();
                        double cr = System.Convert.ToDouble(TxtCrLimit.Value.ToString());
                        string str = lblCurrBalance.Value.ToString();
                        string[] str1 = str.Split(new char[] { ' ' }, str.Length);
                        cr += System.Convert.ToDouble(str1[0].ToString());                    //Add by vikas 15.09.09
                        if (str1[1].Equals("Cr."))
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString()) - System.Convert.ToDouble(str1[0].ToString());
                        else
                            net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString()) + System.Convert.ToDouble(str1[0].ToString());

                        if (cr >= net)
                        {
                            obj.Invoice_No = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;
                            UpdateProductQty();
                            UpdateBatchNo();

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(obj);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/UpdateSalesMaster", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //Add by vikas 16.09.09
                            //obj.UpdateSalesMaster();

                            List<string> sales1 = new List<string>();
                            sales1.Add(FromDate.ToString() + ToDate + dropInvoiceNo.SelectedItem.Text);
                            sales1.Add(NetAmount);
                            sales1.Add(CustID);

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(sales1);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/Update_Customer_Balance", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //dbobj.Insert_or_Update("update customer_balance set DR_Amount=DR_Amount-"+NetAmount+" where cust_id='"+CustID+"'",ref x);

                            CustomerUpdate();

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(obj);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/ProUpdate_Customer_Balance", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }
                            //obj.UpdateCustomerBalance();

                        }
                        else
                        {
                            MessageBox.Show("Credit Limit is less than Net Amount");
                            return;
                        }
                    }
                    else
                    {
                        obj.Invoice_No = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;
                        UpdateProductQty();
                        UpdateBatchNo();

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(baseUri);
                            var myContent = JsonConvert.SerializeObject(obj);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/sales/UpdateSalesMaster", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                            }
                        }

                        //obj.UpdateSalesMaster();
                        CustomerUpdate();
                    }

                    insertSalesOil();
                    DropSalesType.Enabled = true;
                }
                string temp, Schtemp;
                HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };

                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
                TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
                TextBox[] scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
                TextBox[] Quantity = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12 };
                TextBox[] SchQuantity = { txtTempSchQty1, txtTempSchQty2, txtTempSchQty3, txtTempSchQty4, txtTempSchQty5, txtTempSchQty6, txtTempSchQty7, txtTempSchQty8, txtTempSchQty9, txtTempSchQty10, txtTempSchQty11, txtTempSchQty12 };
                TextBox[] ProdType1 = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                HtmlInputHidden[] foe = { temfoe1, temfoe2, temfoe3, temfoe4, temfoe5, temfoe6, temfoe7, temfoe8, temfoe9, temfoe10, temfoe11, temfoe12 };
                HtmlInputHidden[] SecSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
                HtmlInputHidden[] tmpSecSPType = { tmpSecSPType1, tmpSecSPType2, tmpSecSPType3, tmpSecSPType4, tmpSecSPType5, tmpSecSPType6, tmpSecSPType7, tmpSecSPType8, tmpSecSPType9, tmpSecSPType10, tmpSecSPType11, tmpSecSPType12 };
                HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
                HtmlInputHidden[] tmpFoeType = { tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12 };

                for (int j = 0; j < ProdType.Length; j++)
                {

                    /**********Add by vikas 16.11.2012*********************/
                    //if(DropOrderInvoice.SelectedIndex!=0)
                    //{
                    if (ProdType[j].Value.ToString() != "Type" && Qty[j].Text != "" && DropOrderInvoice.SelectedValue.ToString() != "Select")
                        Insert_Ovd(ProdType[j].Value.ToString(), Qty[j].Text.ToString());
                    //}
                    /*********End**********************/

                    if (Request.Form[Rate[j].ID].ToString() == "" || Request.Form[Rate[j].ID].ToString() == "0")
                        continue;
                    if (lblInvoiceNo.Visible == true || Quantity[j].Text == "")
                    {
                        temp = Request.Form[Qty[j].ID].ToString();
                        Schtemp = Request.Form[Qty1[j].ID].ToString();
                    }
                    else
                    {
                        temp = Request.Form[Qty[j].ID].ToString();
                        Schtemp = Request.Form[Qty1[j].ID].ToString();
                    }
                    //Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString()) );
                    //*bhal*/	Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString()),scheme[j].Text.ToString () );
                    //Mahesh11.04007 Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString()),scheme[j].Text.ToString (),foe[j].Text.ToString () );
                    //Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()),scheme[j].Text.ToString (),foe[j].Text.ToString (),j,ProductID,ProductQty[j]);
                    //Mahesh 5.11.07 Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()),scheme[j].Text.ToString (),foe[j].Text.ToString (),j);
                    //vikas sharma 22.04.09  string[] arrName=new string[2];
                    //if(ProdType[j].SelectedItem.Text.IndexOf(":")>0)
                    string[] arrName = new string[3];
                    if (ProdType[j].Value.IndexOf(":") > 0)
                        //arrName=ProdType[j].SelectedItem.Text.Split(new char[] {':'},ProdType[j].SelectedItem.Text.Length);
                        arrName = ProdType[j].Value.Split(new char[] { ':' }, ProdType[j].Value.Length);
                    else
                    {
                        arrName[0] = "";
                        arrName[1] = "";
                        arrName[2] = "";
                    }

                    //**Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString())+" "+DateTime.Now.TimeOfDay.ToString(),scheme[j].Text.ToString (),foe[j].Text.ToString (),j);
                    //vikas sharma 22.04.09 Save(arrName[0].ToString(),arrName[1].ToString(),Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),temp,GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString())+" "+DateTime.Now.TimeOfDay.ToString(),scheme[j].Text.ToString (),foe[j].Text.ToString (),j,tmpSecSPType[j].Value,SecSP[j].Value,tmpFoeType[j].Value,tmpSchType[j].Value);
                    Save(arrName[1].ToString(), arrName[2].ToString(), Request.Form[Qty[j].ID].ToString(), Request.Form[Rate[j].ID].ToString(), Request.Form[Amount[j].ID].ToString(), temp, GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()), Request.Form[scheme[j].ID].ToString(), foe[j].Value.ToString(), j, tmpSecSPType[j].Value, SecSP[j].Value, tmpFoeType[j].Value, tmpSchType[j].Value);
                    //for(int i=0;i<ProdName1.Length ;i++)
                    //{
                    //****************
                    //InsertBatchNo(ProdName[j].Value,PackType[j].Value,Qty[j].Text);//hide this code for some time
                    //vikas sharma 22.04.09 InsertBatchNo(arrName[0].ToString(),arrName[1].ToString(),Qty[j].Text);//hide this code for some time
                    InsertBatchNo(arrName[1].ToString(), arrName[2].ToString(), Qty[j].Text);  //hide this code for some time
                                                                                               //****************
                    if ((Request.Form[Qty1[j].ID].ToString() == "" || Request.Form[Qty1[j].ID].ToString() == "0") && (ProdType1[j].Text == ""))
                        //if(Qty1[j].Text=="" || ProdType1[j].Text=="")
                        continue;

                    //Mahesh11.04.007 Save1(ProdName1[j].Text.ToString(),PackType1[j].Text.ToString(),Qty1[j].Text.ToString(),GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString()),scheme[j].Text.ToString ());
                    //Save1(ProdName1[j].Text.ToString(),PackType1[j].Text.ToString(),Qty1[j].Text.ToString(),GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()),scheme[j].Text.ToString (),j,Schtemp);
                    //****Save1(ProdName1[j].Text.ToString(),PackType1[j].Text.ToString(),Qty1[j].Text.ToString(),GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.ToString())+" "+DateTime.Now.TimeOfDay.ToString(),scheme[j].Text.ToString (),j,Schtemp);
                    string[] arrschName = new string[2];
                    if (ProdType1[j].Text.IndexOf(":") > 0)
                        arrschName = ProdType1[j].Text.Split(new char[] { ':' }, ProdType1[j].Text.Length);
                    else
                    {
                        arrschName[0] = "";
                        arrschName[1] = "";
                    }
                    Save1(arrschName[0].ToString(), arrschName[1].ToString(), Request.Form[Qty1[j].ID].ToString(), GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()), Request.Form[scheme[j].ID].ToString(), j, Schtemp);
                    InsertBatchNo(arrschName[0].ToString(), arrschName[1].ToString(), Request.Form[Qty1[j].ID].ToString());
                    //}

                }

                PrePrintReport();
                GetOrderInvoice();
                //*****************************************************************************
                if (lblInvoiceNo.Visible == true)
                {
                    MessageBox.Show("Sales Invoice Saved");
                }
                else
                {
                    List<string> ProductType1 = new List<string>();
                    List<string> ProductName1 = new List<string>();
                    List<string> ProductPack1 = new List<string>();
                    List<string> ProductQty1 = new List<string>();

                    List<string> SchProductType1 = new List<string>();
                    List<string> SchProductName1 = new List<string>();
                    List<string> SchProductPack1 = new List<string>();
                    List<string> SchProductQty1 = new List<string>();

                    SalesModel sales1 = new SalesModel();

                    for (int i = 0; i < 12; i++)
                    {
                        if (ProductType[i].ToString().Equals(""))
                            continue;
                        else
                        {
                            ProductQty1.Add(ProductQty[i].ToString());
                            ProductType1.Add(ProductType[i].ToString());
                            ProductName1.Add(ProductName[i].ToString());
                            ProductPack1.Add(ProductPack[i].ToString());
                            Invoice_Date = GenUtil.str2DDMMYYYY(Request.Form["tempInvoiceDate"].ToString());
                        }


                        if (ProdType1[i].Text == "")
                            continue;
                        else
                        {

                            SchProductQty1.Add(SchProductQty[i].ToString());
                            SchProductType1.Add(SchProductType[i].ToString());
                            SchProductName1.Add(SchProductName[i].ToString());
                            SchProductPack1.Add(SchProductPack[i].ToString());
                        }
                    }

                    sales1.ProductType = ProductType1;
                    sales1.ProductQty = ProductQty1;
                    sales1.ProductName = ProductName1;
                    sales1.ProductPack = ProductPack1;

                    sales1.SchProductQty = SchProductQty1;
                    sales1.SchProductType = SchProductType1;
                    sales1.SchProductName = SchProductName1;
                    sales1.SchProductPack = SchProductPack1;
                    SeqStockMaster(sales1);
                    MessageBox.Show("Sales Invoice Updated");
                }
                FlagPrint = false;
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:save_updateInvoice()" + " Sales Invoice No." + obj.Invoice_No + " ," + "for Customer Name  " + obj.Customer_Name + "on Date " + obj.Invoice_Date + " and NetAmount  " + obj.Net_Amount + "  is Saved " + " userid " + "   " + "   " + uid);
                //Mahesh}	
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoise.aspx,Method:save_updateInvoice(),Class:InventoryClass" + "  Sales Invoise for  Invoice No." + obj.Invoice_No + " ," + "for Customer Name  " + obj.Customer_Name + " and NetAmount  " + obj.Net_Amount + "  is Saved " + "  EXCEPTION  " + ex.Message + " userid " + "   " + "   " + uid);
            }
        }


        public void Insert_Ovd(string Product, string Qty)
        {
            try
            {
                string[] Prod = Product.Split(new char[] { ':' }, Product.Length);
                int x = 0;
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;

                string Ovd_No = "", cust_id = "", prod_id = "", Invoice_no = "", Invoice_date = "", Invoice_qty = "";
                int ovd_sale_qty = 0;           //add by vikas 11.12.2012
                string txtval = text1.Value;
                string[] Cust_Name = txtval.Split(new char[] { ':' }, txtval.Length);
                string sql = "select Cust_ID from Customer where Cust_Name='" + Cust_Name[0] + "' and City='" + Cust_Name[1] + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if (SqlDtr["Cust_Id"] != null && SqlDtr["Cust_Id"].ToString() != "")
                        cust_id = SqlDtr["Cust_Id"].ToString();
                }
                SqlDtr.Close();

                sql = "select prod_id from Products where Prod_Name='" + Prod[1] + "' and Pack_Type='" + Prod[2] + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    if (SqlDtr["prod_id"] != null && SqlDtr["prod_id"].ToString() != "")
                        prod_id = SqlDtr["prod_id"].ToString();
                }
                SqlDtr.Close();

                if (lblInvoiceNo.Visible == true)
                {
                    /*********Add by vikas 11.12.12********************************/
                    string[] Order_No = DropOrderInvoice.SelectedValue.ToString().Split(new char[] { ':' });
                    int Count = Order_No.Length;

                    /***************************End********************************/

                    //coment by vikas 11.12.2012 sql="select ovd_id from ovd where cust_id='"+cust_id+"' and order_id='"+DropOrderInvoice.SelectedValue.ToString()+"' and item_id='"+prod_id+"'";
                    if (Count == 1)
                    {
                        sql = "select ovd_id,sale_qty from ovd where cust_id='" + cust_id + "' and order_id='" + DropOrderInvoice.SelectedValue.ToString() + "' and item_id='" + prod_id + "'";
                    }
                    else
                    {
                        sql = "select ovd_id,sale_qty from ovd where cust_id='" + cust_id + "' and Order_id=(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ") and item_id='" + prod_id + "'";//
                    }

                    SqlDtr = obj.GetRecordSet(sql);
                    while (SqlDtr.Read())
                    {
                        if (SqlDtr["ovd_id"] != null && SqlDtr["ovd_id"].ToString() != "")
                            Ovd_No = SqlDtr["ovd_id"].ToString();

                        /**************Add by vikas 11.12.2012******************************/
                        if (SqlDtr["sale_qty"] != null && SqlDtr["sale_qty"].ToString() != "")
                            ovd_sale_qty = int.Parse(SqlDtr["sale_qty"].ToString());
                        else
                            ovd_sale_qty = 0;
                        /**************End******************************/
                    }
                    SqlDtr.Close();
                    //Invoice_no =FromDate+ToDate+lblInvoiceNo.Text.ToString().Trim();
                    Invoice_no = lblInvoiceNo.Text.ToString().Trim();
                }
                else
                {
                    sql = "select ovd_id,sale_qty from ovd where cust_id='" + cust_id + "' and Sale_Trans_Id='" + dropInvoiceNo.SelectedItem.Value.ToString() + "' and item_id='" + prod_id + "'";
                    SqlDtr = obj.GetRecordSet(sql);
                    while (SqlDtr.Read())
                    {
                        if (SqlDtr["ovd_id"] != null && SqlDtr["ovd_id"].ToString() != "")
                            Ovd_No = SqlDtr["ovd_id"].ToString();

                        /**************Add by vikas 11.12.2012******************************/
                        if (SqlDtr["sale_qty"] != null && SqlDtr["sale_qty"].ToString() != "")
                            ovd_sale_qty = int.Parse(SqlDtr["sale_qty"].ToString());
                        else
                            ovd_sale_qty = 0;
                        /**************End******************************/
                    }
                    SqlDtr.Close();
                    //Invoice_no =FromDate+ToDate+dropInvoiceNo.SelectedItem.Value.ToString().Trim();
                    Invoice_no = dropInvoiceNo.SelectedItem.Value.ToString().Trim();
                }

                Invoice_date = (System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text) + " " + DateTime.Now.TimeOfDay.ToString())).ToString();

                Qty = Convert.ToString(ovd_sale_qty + int.Parse(Qty.ToString()));   //Add by vikas 11.12.2012

                Invoice_qty = Qty;

                sql = "update ovd set Sale_Trans_Date=Convert(datetime,'" + Invoice_date + "',103), Sale_Trans_Id='" + Invoice_no + "',sale_qty='" + Invoice_qty + "' where Ovd_id='" + Ovd_No + "'";

                dbobj.Insert_or_Update(sql, ref x);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form: SalesInvoice.aspx,Method: Insert_Ovd(), EXCEPTION  " + ex.Message + " userid " + "   " + "   " + uid);
            }
        }

        public void UpdateCustomerLedger()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader rdr;
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                //string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+DropCustName.SelectedItem.Text+"') order by entrydate";
                string txtval = text1.Value.Substring(0, text1.Value.IndexOf(":"));// add by vikas 1.05.09
                                                                                   // comment by vikas sharma 1.05.09 string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='"+text1.Value+"') order by entrydate";
                string str1 = "select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer where Cust_Name='" + txtval + "') order by entrydate";
                rdr = obj.GetRecordSet(str1);
                double Bal = 0;
                while (rdr.Read())
                {
                    if (rdr["BalanceType"].ToString().Equals("Dr."))
                        Bal += double.Parse(rdr["DebitAmount"].ToString()) - double.Parse(rdr["CreditAmount"].ToString());
                    else
                        Bal += double.Parse(rdr["CreditAmount"].ToString()) - double.Parse(rdr["DebitAmount"].ToString());
                    if (Bal.ToString().StartsWith("-"))
                        Bal = double.Parse(Bal.ToString().Substring(1));
                    Con.Open();
                    cmd = new SqlCommand("update CustomerLedgerTable set Balance='" + Bal.ToString() + "' where CustID='" + rdr["CustID"].ToString() + "' and Particular='" + rdr["Particular"].ToString() + "'", Con);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    cmd.Dispose();
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : UpdateCustomerLedger()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// this method are not used.
        /// </summary>
        public void InsertSalesOils()
        {
            //			InventoryClass obj = new InventoryClass();
            //			SqlDataReader SqlDtr;
            //			SqlConnection con1;
            //			SqlCommand cmd;
            //			HtmlInputHidden[] PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
            //			TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
            //			HtmlInputHidden[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12}; 
            //			string str="",cat="",prodid="";
            //			for(int i=0;i<ProdName.Length;i++)
            //			{
            //				str = "select prod_id,category from Products where prod_name='"+ProdName[i]+"' and pack_type='"+PackType[i]+"'";
            //				SqlDtr = obj.GetRecordSet(str);
            //				if(SqlDtr.Read())
            //				{
            //					prodid = SqlDtr.GetValue(0).ToString();
            //						cat = SqlDtr.GetValue(1).ToString();
            //				}
            //				SqlDtr.Close();
            //				con1=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //				if(lblInvoiceNo.Visible==false)
            //				{
            //					con1.Open ();
            //					cmd=new SqlCommand("delete from Sales_Oil where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prodid+"'",con1);
            //					cmd.ExecuteNonQuery();
            //					con1.Close();
            //					cmd.Dispose();
            //					con1.Open ();
            //					if(cat=="")
            //						cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prodid+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
            //					else if(cat.StartsWith("2t") || cat.StartsWith("2T"))
            //						cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prodid+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+","+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
            //					else if(cat.StartsWith("4t") || cat.StartsWith("4T"))
            //						cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prodid+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+",0,"+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
            //
            //					cmd.ExecuteNonQuery();
            //					con1.Close();
            //					cmd.Dispose();
            //				}
            //				else
            //				{
            //					con1.Open ();
            //					if(cat=="")
            //						cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prodid+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
            //					else if(cat.StartsWith("2t") || cat.StartsWith("2T"))
            //						cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prodid+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[i].Value,int.Parse (Qty[i].Text))+","+GenUtil.changeqty(PackType[i].Value,int.Parse (Qty[i].Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
            //					else if(cat.StartsWith("4t") || cat.StartsWith("4T"))
            //						cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prodid+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[i].Value,int.Parse (Qty[i].Text))+",0,"+GenUtil.changeqty(PackType[i].Value,int.Parse(Qty[i].Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
            //					cmd.ExecuteNonQuery();
            //					con1.Close();
            //					cmd.Dispose();
            //				}
            //			}
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        public void insertmonthwise1()
        {
            /*try
			{
				SqlConnection con1;
				SqlCommand cmd;
				con1=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//***********This code add for update invoice no from 3010
//				int invoice=0;
//				if(dropInvoiceNo.Visible==true)
//				{
//					invoice=System.Convert.ToInt32(dropInvoiceNo.SelectedItem.Text);
//					invoice=invoice+2009;
//				}
				//****
				if(txtProdName1.Value =="" && txtQty1.Text == "" )
				{
				}
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName1.Value+"' and pack_type='"+txtPack1.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid1.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						if(SqlDtr.GetValue(1).ToString().StartsWith("2T/4T") || SqlDtr.GetValue(1).ToString().StartsWith("2t/4t"))
							cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[0]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[0]+"' and pack_type='"+ProductPack[0]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty1.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty1.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty1.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty1.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty1.Text);
					//****************
					//if(DropType1.SelectedItem.Text.Equals(cat))// || DropType1.SelectedItem.Text.Equals("2T/4T"))
					if(DropType1.ToString().Equals(cat))// || DropType1.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty1.Text);
					
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						//Mahesh 05.11.07 cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName2.Value =="" && txtQty2.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string Oldprod_id="";
					string sql,cat="";
					string prod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName2.Value+"' and pack_type='"+txtPack2.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid2.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[1]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[1]+"' and pack_type='"+ProductPack[1]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty2.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty2.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty2.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty2.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty2.Text);
					//****************
					if(DropType2.Value.Equals(cat))// || DropType2.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty2.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid2.Text+"',"+GenUtil.changeqty(txtPack2.Value,bi)+","+GenUtil.changeqty(txtPack2.Value,ri)+","+GenUtil.changeqty(txtPack2.Value,oi)+","+GenUtil.changeqty(txtPack2.Value,fi)+","+GenUtil.changeqty(txtPack2.Value,ii)+","+GenUtil.changeqty(txtPack2.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid2.Text+"',"+GenUtil.changeqty(txtPack2.Value,bi)+","+GenUtil.changeqty(txtPack2.Value,ri)+","+GenUtil.changeqty(txtPack2.Value,oi)+","+GenUtil.changeqty(txtPack2.Value,fi)+","+GenUtil.changeqty(txtPack2.Value,ii)+","+GenUtil.changeqty(txtPack2.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid2.Text+"',"+GenUtil.changeqty(txtPack2.Value,bi)+","+GenUtil.changeqty(txtPack2.Value,ri)+","+GenUtil.changeqty(txtPack2.Value,oi)+","+GenUtil.changeqty(txtPack2.Value,fi)+","+GenUtil.changeqty(txtPack2.Value,ii)+","+GenUtil.changeqty(txtPack2.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid2.Text+"',"+GenUtil.changeqty(txtPack2.Value,bi)+","+GenUtil.changeqty(txtPack2.Value,ri)+","+GenUtil.changeqty(txtPack2.Value,oi)+","+GenUtil.changeqty(txtPack2.Value,fi)+","+GenUtil.changeqty(txtPack2.Value,ii)+","+GenUtil.changeqty(txtPack2.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName3.Value =="" && txtQty3.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string Oldprod_id="";
					string sql,cat="";
					string prod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName3.Value+"' and pack_type='"+txtPack3.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid3.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[2]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[2]+"' and pack_type='"+ProductPack[2]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty3.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty3.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty3.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty3.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty3.Text);
					//****************
					if(DropType3.Value.Equals(cat))// || DropType3.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty3.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid3.Text+"',"+GenUtil.changeqty(txtPack3.Value,bi)+","+GenUtil.changeqty(txtPack3.Value,ri)+","+GenUtil.changeqty(txtPack3.Value,oi)+","+GenUtil.changeqty(txtPack3.Value,fi)+","+GenUtil.changeqty(txtPack3.Value,ii)+","+GenUtil.changeqty(txtPack3.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid3.Text+"',"+GenUtil.changeqty(txtPack3.Value,bi)+","+GenUtil.changeqty(txtPack3.Value,ri)+","+GenUtil.changeqty(txtPack3.Value,oi)+","+GenUtil.changeqty(txtPack3.Value,fi)+","+GenUtil.changeqty(txtPack3.Value,ii)+","+GenUtil.changeqty(txtPack3.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid3.Text+"',"+GenUtil.changeqty(txtPack3.Value,bi)+","+GenUtil.changeqty(txtPack3.Value,ri)+","+GenUtil.changeqty(txtPack3.Value,oi)+","+GenUtil.changeqty(txtPack3.Value,fi)+","+GenUtil.changeqty(txtPack3.Value,ii)+","+GenUtil.changeqty(txtPack3.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid3.Text+"',"+GenUtil.changeqty(txtPack3.Value,bi)+","+GenUtil.changeqty(txtPack3.Value,ri)+","+GenUtil.changeqty(txtPack3.Value,oi)+","+GenUtil.changeqty(txtPack3.Value,fi)+","+GenUtil.changeqty(txtPack3.Value,ii)+","+GenUtil.changeqty(txtPack3.Value,ti)+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName4.Value =="" && txtQty4.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName4.Value+"' and pack_type='"+txtPack4.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid4.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[3]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[3]+"' and pack_type='"+ProductPack[3]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty4.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty4.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty4.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty4.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty4.Text);
					//****************
					if(DropType4.Value.Equals(cat))// || DropType4.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty4.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid4.Text+"',"+GenUtil.changeqty(txtPack4.Value,bi)+","+GenUtil.changeqty(txtPack4.Value,ri)+","+GenUtil.changeqty(txtPack4.Value,oi)+","+GenUtil.changeqty(txtPack4.Value,fi)+","+GenUtil.changeqty(txtPack4.Value,ii)+","+GenUtil.changeqty(txtPack4.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid4.Text+"',"+GenUtil.changeqty(txtPack4.Value,bi)+","+GenUtil.changeqty(txtPack4.Value,ri)+","+GenUtil.changeqty(txtPack4.Value,oi)+","+GenUtil.changeqty(txtPack4.Value,fi)+","+GenUtil.changeqty(txtPack4.Value,ii)+","+GenUtil.changeqty(txtPack4.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid4.Text+"',"+GenUtil.changeqty(txtPack4.Value,bi)+","+GenUtil.changeqty(txtPack4.Value,ri)+","+GenUtil.changeqty(txtPack4.Value,oi)+","+GenUtil.changeqty(txtPack4.Value,fi)+","+GenUtil.changeqty(txtPack4.Value,ii)+","+GenUtil.changeqty(txtPack4.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid4.Text+"',"+GenUtil.changeqty(txtPack4.Value,bi)+","+GenUtil.changeqty(txtPack4.Value,ri)+","+GenUtil.changeqty(txtPack4.Value,oi)+","+GenUtil.changeqty(txtPack4.Value,fi)+","+GenUtil.changeqty(txtPack4.Value,ii)+","+GenUtil.changeqty(txtPack4.Value,ti)+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName5.Value =="" && txtQty5.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName5.Value+"' and pack_type='"+txtPack5.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid5.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[4]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[4]+"' and pack_type='"+ProductPack[4]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty5.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty5.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty5.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty5.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty5.Text);
					//****************
					if(DropType5.Value.Equals(cat))// || DropType5.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty5.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid5.Text+"',"+GenUtil.changeqty(txtPack5.Value,bi)+","+GenUtil.changeqty(txtPack5.Value,ri)+","+GenUtil.changeqty(txtPack5.Value,oi)+","+GenUtil.changeqty(txtPack5.Value,fi)+","+GenUtil.changeqty(txtPack5.Value,ii)+","+GenUtil.changeqty(txtPack5.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid5.Text+"',"+GenUtil.changeqty(txtPack5.Value,bi)+","+GenUtil.changeqty(txtPack5.Value,ri)+","+GenUtil.changeqty(txtPack5.Value,oi)+","+GenUtil.changeqty(txtPack5.Value,fi)+","+GenUtil.changeqty(txtPack5.Value,ii)+","+GenUtil.changeqty(txtPack5.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid5.Text+"',"+GenUtil.changeqty(txtPack5.Value,bi)+","+GenUtil.changeqty(txtPack5.Value,ri)+","+GenUtil.changeqty(txtPack5.Value,oi)+","+GenUtil.changeqty(txtPack5.Value,fi)+","+GenUtil.changeqty(txtPack5.Value,ii)+","+GenUtil.changeqty(txtPack5.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid5.Text+"',"+GenUtil.changeqty(txtPack5.Value,bi)+","+GenUtil.changeqty(txtPack5.Value,ri)+","+GenUtil.changeqty(txtPack5.Value,oi)+","+GenUtil.changeqty(txtPack5.Value,fi)+","+GenUtil.changeqty(txtPack5.Value,ii)+","+GenUtil.changeqty(txtPack5.Value,ti)+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName6.Value =="" && txtQty6.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName6.Value+"' and pack_type='"+txtPack6.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid6.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[5]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[5]+"' and pack_type='"+ProductPack[5]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty6.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty6.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty6.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty6.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty6.Text);
					//****************
					if(DropType6.Value.Equals(cat))// || DropType6.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty6.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid6.Text+"',"+GenUtil.changeqty(txtPack6.Value,bi)+","+GenUtil.changeqty(txtPack6.Value,ri)+","+GenUtil.changeqty(txtPack6.Value,oi)+","+GenUtil.changeqty(txtPack6.Value,fi)+","+GenUtil.changeqty(txtPack6.Value,ii)+","+GenUtil.changeqty(txtPack6.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid6.Text+"',"+GenUtil.changeqty(txtPack6.Value,bi)+","+GenUtil.changeqty(txtPack6.Value,ri)+","+GenUtil.changeqty(txtPack6.Value,oi)+","+GenUtil.changeqty(txtPack6.Value,fi)+","+GenUtil.changeqty(txtPack6.Value,ii)+","+GenUtil.changeqty(txtPack6.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid6.Text+"',"+GenUtil.changeqty(txtPack6.Value,bi)+","+GenUtil.changeqty(txtPack6.Value,ri)+","+GenUtil.changeqty(txtPack6.Value,oi)+","+GenUtil.changeqty(txtPack6.Value,fi)+","+GenUtil.changeqty(txtPack6.Value,ii)+","+GenUtil.changeqty(txtPack6.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid6.Text+"',"+GenUtil.changeqty(txtPack6.Value,bi)+","+GenUtil.changeqty(txtPack6.Value,ri)+","+GenUtil.changeqty(txtPack6.Value,oi)+","+GenUtil.changeqty(txtPack6.Value,fi)+","+GenUtil.changeqty(txtPack6.Value,ii)+","+GenUtil.changeqty(txtPack6.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName7.Value =="" && txtQty7.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName7.Value+"' and pack_type='"+txtPack7.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid7.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[6]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[6]+"' and pack_type='"+ProductPack[6]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty7.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty7.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty7.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty7.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty7.Text);
					//****************
					if(DropType7.Value.Equals(cat))// || DropType7.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty7.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid7.Text+"',"+GenUtil.changeqty(txtPack7.Value,bi)+","+GenUtil.changeqty(txtPack7.Value,ri)+","+GenUtil.changeqty(txtPack7.Value,oi)+","+GenUtil.changeqty(txtPack7.Value,fi)+","+GenUtil.changeqty(txtPack7.Value,ii)+","+GenUtil.changeqty(txtPack7.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid7.Text+"',"+GenUtil.changeqty(txtPack7.Value,bi)+","+GenUtil.changeqty(txtPack7.Value,ri)+","+GenUtil.changeqty(txtPack7.Value,oi)+","+GenUtil.changeqty(txtPack7.Value,fi)+","+GenUtil.changeqty(txtPack7.Value,ii)+","+GenUtil.changeqty(txtPack7.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid7.Text+"',"+GenUtil.changeqty(txtPack7.Value,bi)+","+GenUtil.changeqty(txtPack7.Value,ri)+","+GenUtil.changeqty(txtPack7.Value,oi)+","+GenUtil.changeqty(txtPack7.Value,fi)+","+GenUtil.changeqty(txtPack7.Value,ii)+","+GenUtil.changeqty(txtPack7.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid7.Text+"',"+GenUtil.changeqty(txtPack7.Value,bi)+","+GenUtil.changeqty(txtPack7.Value,ri)+","+GenUtil.changeqty(txtPack7.Value,oi)+","+GenUtil.changeqty(txtPack7.Value,fi)+","+GenUtil.changeqty(txtPack7.Value,ii)+","+GenUtil.changeqty(txtPack7.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName8.Value =="" && txtQty8.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName8.Value+"' and pack_type='"+txtPack8.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid8.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[7]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[7]+"' and pack_type='"+ProductPack[7]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty8.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty8.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty8.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty8.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty8.Text);
					//****************
					if(DropType8.Value.Equals(cat))// || DropType8.Value.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty8.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid8.Text+"',"+GenUtil.changeqty(txtPack8.Value,bi)+","+GenUtil.changeqty(txtPack8.Value,ri)+","+GenUtil.changeqty(txtPack8.Value,oi)+","+GenUtil.changeqty(txtPack8.Value,fi)+","+GenUtil.changeqty(txtPack8.Value,ii)+","+GenUtil.changeqty(txtPack8.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid8.Text+"',"+GenUtil.changeqty(txtPack8.Value,bi)+","+GenUtil.changeqty(txtPack8.Value,ri)+","+GenUtil.changeqty(txtPack8.Value,oi)+","+GenUtil.changeqty(txtPack8.Value,fi)+","+GenUtil.changeqty(txtPack8.Value,ii)+","+GenUtil.changeqty(txtPack8.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid8.Text+"',"+GenUtil.changeqty(txtPack8.Value,bi)+","+GenUtil.changeqty(txtPack8.Value,ri)+","+GenUtil.changeqty(txtPack8.Value,oi)+","+GenUtil.changeqty(txtPack8.Value,fi)+","+GenUtil.changeqty(txtPack8.Value,ii)+","+GenUtil.changeqty(txtPack8.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid8.Text+"',"+GenUtil.changeqty(txtPack8.Value,bi)+","+GenUtil.changeqty(txtPack8.Value,ri)+","+GenUtil.changeqty(txtPack8.Value,oi)+","+GenUtil.changeqty(txtPack8.Value,fi)+","+GenUtil.changeqty(txtPack8.Value,ii)+","+GenUtil.changeqty(txtPack8.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				//*************************
				if(txtProdName9.Value =="" && txtQty9.Text == "")
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName9.Value+"' and pack_type='"+txtPack9.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid9.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[8]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[8]+"' and pack_type='"+ProductPack[8]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty9.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty9.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty9.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty9.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty9.Text);
					//****************
					//if(DropType9.SelectedItem.Text.Equals(cat))// || DropType9.SelectedItem.Text.Equals("2T/4T"))
					if(DropType9.Value.Equals(cat))// || DropType9.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty9.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid9.Text+"',"+GenUtil.changeqty(txtPack9.Value,bi)+","+GenUtil.changeqty(txtPack9.Value,ri)+","+GenUtil.changeqty(txtPack9.Value,oi)+","+GenUtil.changeqty(txtPack9.Value,fi)+","+GenUtil.changeqty(txtPack9.Value,ii)+","+GenUtil.changeqty(txtPack9.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid9.Text+"',"+GenUtil.changeqty(txtPack9.Value,bi)+","+GenUtil.changeqty(txtPack9.Value,ri)+","+GenUtil.changeqty(txtPack9.Value,oi)+","+GenUtil.changeqty(txtPack9.Value,fi)+","+GenUtil.changeqty(txtPack9.Value,ii)+","+GenUtil.changeqty(txtPack9.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid9.Text+"',"+GenUtil.changeqty(txtPack9.Value,bi)+","+GenUtil.changeqty(txtPack9.Value,ri)+","+GenUtil.changeqty(txtPack9.Value,oi)+","+GenUtil.changeqty(txtPack9.Value,fi)+","+GenUtil.changeqty(txtPack9.Value,ii)+","+GenUtil.changeqty(txtPack9.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid9.Text+"',"+GenUtil.changeqty(txtPack9.Value,bi)+","+GenUtil.changeqty(txtPack9.Value,ri)+","+GenUtil.changeqty(txtPack9.Value,oi)+","+GenUtil.changeqty(txtPack9.Value,fi)+","+GenUtil.changeqty(txtPack9.Value,ii)+","+GenUtil.changeqty(txtPack9.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName10.Value =="" && txtQty10.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string Oldprod_id="";
					string prod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName10.Value+"' and pack_type='"+txtPack10.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid10.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[9]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[9]+"' and pack_type='"+ProductPack[9]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty10.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty10.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty10.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty10.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty10.Text);
					//****************
					if(DropType10.Value.Equals(cat))// || DropType10.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty10.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid10.Text+"',"+GenUtil.changeqty(txtPack10.Value,bi)+","+GenUtil.changeqty(txtPack10.Value,ri)+","+GenUtil.changeqty(txtPack10.Value,oi)+","+GenUtil.changeqty(txtPack10.Value,fi)+","+GenUtil.changeqty(txtPack10.Value,ii)+","+GenUtil.changeqty(txtPack10.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid10.Text+"',"+GenUtil.changeqty(txtPack10.Value,bi)+","+GenUtil.changeqty(txtPack10.Value,ri)+","+GenUtil.changeqty(txtPack10.Value,oi)+","+GenUtil.changeqty(txtPack10.Value,fi)+","+GenUtil.changeqty(txtPack10.Value,ii)+","+GenUtil.changeqty(txtPack10.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid10.Text+"',"+GenUtil.changeqty(txtPack10.Value,bi)+","+GenUtil.changeqty(txtPack10.Value,ri)+","+GenUtil.changeqty(txtPack10.Value,oi)+","+GenUtil.changeqty(txtPack10.Value,fi)+","+GenUtil.changeqty(txtPack10.Value,ii)+","+GenUtil.changeqty(txtPack10.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid10.Text+"',"+GenUtil.changeqty(txtPack10.Value,bi)+","+GenUtil.changeqty(txtPack10.Value,ri)+","+GenUtil.changeqty(txtPack10.Value,oi)+","+GenUtil.changeqty(txtPack10.Value,fi)+","+GenUtil.changeqty(txtPack10.Value,ii)+","+GenUtil.changeqty(txtPack10.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName11.Value =="" && txtQty11.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName11.Value+"' and pack_type='"+txtPack11.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid11.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[10]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[10]+"' and pack_type='"+ProductPack[10]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty11.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty11.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty11.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty11.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty11.Text);
					//****************
					if(DropType11.Value.Equals(cat))// || DropType11.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty11.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid11.Text+"',"+GenUtil.changeqty(txtPack11.Value,bi)+","+GenUtil.changeqty(txtPack11.Value,ri)+","+GenUtil.changeqty(txtPack11.Value,oi)+","+GenUtil.changeqty(txtPack11.Value,fi)+","+GenUtil.changeqty(txtPack11.Value,ii)+","+GenUtil.changeqty(txtPack11.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid11.Text+"',"+GenUtil.changeqty(txtPack11.Value,bi)+","+GenUtil.changeqty(txtPack11.Value,ri)+","+GenUtil.changeqty(txtPack11.Value,oi)+","+GenUtil.changeqty(txtPack11.Value,fi)+","+GenUtil.changeqty(txtPack11.Value,ii)+","+GenUtil.changeqty(txtPack11.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid11.Text+"',"+GenUtil.changeqty(txtPack11.Value,bi)+","+GenUtil.changeqty(txtPack11.Value,ri)+","+GenUtil.changeqty(txtPack11.Value,oi)+","+GenUtil.changeqty(txtPack11.Value,fi)+","+GenUtil.changeqty(txtPack11.Value,ii)+","+GenUtil.changeqty(txtPack11.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid11.Text+"',"+GenUtil.changeqty(txtPack11.Value,bi)+","+GenUtil.changeqty(txtPack11.Value,ri)+","+GenUtil.changeqty(txtPack11.Value,oi)+","+GenUtil.changeqty(txtPack11.Value,fi)+","+GenUtil.changeqty(txtPack11.Value,ii)+","+GenUtil.changeqty(txtPack11.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				if(txtProdName12.Value =="" && txtQty12.Text == "" )
				{
				}	
				else
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql,cat="";
					string prod_id="";
					string Oldprod_id="";
					sql="select prod_id,category from Products where prod_name='"+txtProdName12.Value+"' and pack_type='"+txtPack12.Value+"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						txtmwid12.Text=SqlDtr.GetValue(0).ToString();
						prod_id=SqlDtr.GetValue(0).ToString();
						cat=SqlDtr.GetValue(1).ToString();
					}
					SqlDtr.Close ();
					int flag=0;
					if(ProductName[11]!=null)
					{
						sql="select prod_id from Products where prod_name='"+ProductName[11]+"' and pack_type='"+ProductPack[11]+"'";
						SqlDtr=obj.GetRecordSet(sql);
						while(SqlDtr.Read())
						{
							Oldprod_id=SqlDtr.GetValue(0).ToString();
							flag=1;
						}
						SqlDtr.Close ();
					}
					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
					if(txtcusttype.Text=="Bazzar")
						bi=System.Convert.ToInt32(txtQty12.Text);
					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
						ri=System.Convert.ToInt32(txtQty12.Text);
					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
						oi=System.Convert.ToInt32(txtQty12.Text);
					if(txtcusttype.Text=="Fleet")
						fi=System.Convert.ToInt32(txtQty12.Text);
					if(txtcusttype.Text=="Ibp")
						ii=System.Convert.ToInt32(txtQty12.Text);
					//****************
					if(DropType12.Value.Equals(cat))// || DropType12.SelectedItem.Text.Equals("2T/4T"))
						ti=System.Convert.ToInt32(txtQty12.Text);
					//********************
					if(lblInvoiceNo.Visible==false)
					{
						con1.Open ();
						//cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						if(flag==0)
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
						else
							cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
						con1.Open ();
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid12.Text+"',"+GenUtil.changeqty(txtPack12.Value,bi)+","+GenUtil.changeqty(txtPack12.Value,ri)+","+GenUtil.changeqty(txtPack12.Value,oi)+","+GenUtil.changeqty(txtPack12.Value,fi)+","+GenUtil.changeqty(txtPack12.Value,ii)+","+GenUtil.changeqty(txtPack12.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						//cmd=new SqlCommand("insert into monthwise1 values("+invoice+",'"+txtmwid12.Text+"',"+GenUtil.changeqty(txtPack12.Value,bi)+","+GenUtil.changeqty(txtPack12.Value,ri)+","+GenUtil.changeqty(txtPack12.Value,oi)+","+GenUtil.changeqty(txtPack12.Value,fi)+","+GenUtil.changeqty(txtPack12.Value,ii)+","+GenUtil.changeqty(txtPack12.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
					else
					{
						con1.Open ();
						//cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid12.Text+"',"+GenUtil.changeqty(txtPack12.Value,bi)+","+GenUtil.changeqty(txtPack12.Value,ri)+","+GenUtil.changeqty(txtPack12.Value,oi)+","+GenUtil.changeqty(txtPack12.Value,fi)+","+GenUtil.changeqty(txtPack12.Value,ii)+","+GenUtil.changeqty(txtPack12.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
						cmd=new SqlCommand("insert into monthwise1 values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+txtmwid12.Text+"',"+GenUtil.changeqty(txtPack12.Value,bi)+","+GenUtil.changeqty(txtPack12.Value,ri)+","+GenUtil.changeqty(txtPack12.Value,oi)+","+GenUtil.changeqty(txtPack12.Value,fi)+","+GenUtil.changeqty(txtPack12.Value,ii)+","+GenUtil.changeqty(txtPack12.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
						cmd.ExecuteNonQuery();
						con1.Close();
						cmd.Dispose();
					}
				}
				//*************************
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,EXCEPTION  "+ex.Message+" userid "+"   "+"   "+uid);
			}*/
        }

        /// <summary>
        /// This method save the total qty of the product and also save the 2t oils and 4t oils info in sales_oil table.
        /// </summary>
        public void insertSalesOil()
        {
            try
            {

                SqlConnection con1;
                SqlCommand cmd = new SqlCommand();
                /**************This code use for update the invoice no 1001 to 3010.
				int invoice=0;
				if(dropInvoiceNo.Visible==true)
				{
					invoice=System.Convert.ToInt32(dropInvoiceNo.SelectedItem.Text);
					invoice=invoice+2009;
				}
				*************/
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                con1 = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

                HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };

                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };

                TextBox[] ProdType1 = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };

                TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                int Flag = 0;
                string[] arrProdName = new string[3];
                string[] arrProdschName1 = new string[2];
                for (int cc = 0; cc < ProdType.Length; cc++)
                {
                    //if(ProdType[cc].SelectedItem.Text.IndexOf(":")>0) //Not Used

                    /****************This array use for Selected Products by dropdown********************/
                    if (ProdType[cc].Value.IndexOf(":") > 0)
                        //arrProdName=ProdType[cc].SelectedItem.Text.Split(new char[] {':'},ProdType[cc].SelectedItem.Text.Length);
                        arrProdName = ProdType[cc].Value.Split(new char[] { ':' }, ProdType[cc].Value.Length);
                    else
                    {
                        arrProdName[0] = "";
                        arrProdName[1] = "";
                        arrProdName[2] = "";       // add by vikas sharma 22.04.09
                    }
                    /****************End********************/

                    /****************This array use for FOC Products by Fixed textbox********************/
                    if (ProdType1[cc].Text.IndexOf(":") > 0)
                        arrProdschName1 = ProdType1[cc].Text.Split(new char[] { ':' }, ProdType1[cc].Text.Length);
                    else
                    {
                        arrProdschName1[0] = "";
                        arrProdschName1[1] = "";
                    }
                    /****************End********************/
                    if (arrProdName[0].ToString() != "" && Qty[cc].Text != "")
                    {
                        //					}	
                        //					else
                        //					{
                        string sql, cat = "";
                        string prod_id = "";

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(baseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var Res = client.GetAsync("api/sales/GetCategory?prodName1=" + arrProdName[1].ToString() + "&prodName2=" + arrProdName[2].ToString()).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                cat = JsonConvert.DeserializeObject<string>(id);
                            }
                        }

                        //                  sql ="select prod_id,category from Products where prod_name='"+arrProdName[1].ToString()+"' and pack_type='"+arrProdName[2].ToString()+"'";
                        //SqlDtr=obj.GetRecordSet(sql);
                        //while(SqlDtr.Read())
                        //{
                        //	//txtmwid1.Text=SqlDtr.GetValue(0).ToString();
                        //	prod_id=SqlDtr.GetValue(0).ToString();
                        //	if(SqlDtr.GetValue(1).ToString().StartsWith("2T") || SqlDtr.GetValue(1).ToString().StartsWith("4T") || SqlDtr.GetValue(1).ToString().StartsWith("2t") || SqlDtr.GetValue(1).ToString().StartsWith("4t"))                        
                        //		cat=SqlDtr.GetValue(1).ToString();
                        //}
                        //SqlDtr.Close ();

                        /*****This code is hide bcoz deleted all invoice_no according to select the dropdown list
                        int flag=0;
                        if(ProductName[cc]!=null && ProductName[cc]!="")
                        {
                            sql="select prod_id from Products where prod_name='"+ProductName[cc]+"' and pack_type='"+ProductPack[cc]+"'";
                            SqlDtr=obj.GetRecordSet(sql);
                            while(SqlDtr.Read())
                            {
                                Oldprod_id=SqlDtr.GetValue(0).ToString();
                                flag=1;
                            }
                            SqlDtr.Close ();
                        }
                        */

                        if (lblInvoiceNo.Visible == false)
                        {
                            if (Flag == 0)
                            {
                                using (var client = new HttpClient())
                                {
                                    client.BaseAddress = new Uri(baseUri);
                                    var myContent = JsonConvert.SerializeObject(FromDate + ToDate + dropInvoiceNo.SelectedItem.Text.Trim());
                                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                    var byteContent = new ByteArrayContent(buffer);
                                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                    client.DefaultRequestHeaders.Accept.Clear();
                                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                    var response = client.PostAsync("api/sales/DeleteSales_OilData?invoiceNo=" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Text.Trim(), byteContent).Result;
                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseString = response.Content.ReadAsStringAsync().Result;
                                    }
                                }

                                //                        con1.Open ();
                                ////cmd=new SqlCommand("delete from Sales_Oil where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                                ///***
                                //if(flag==0)
                                //	cmd=new SqlCommand("delete from Sales_Oil where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                                //else
                                //	cmd=new SqlCommand("delete from Sales_Oil where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+Oldprod_id+"'",con1);
                                //*/	
                                //cmd=new SqlCommand("delete from Sales_Oil where invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"'",con1);
                                //cmd.ExecuteNonQuery();
                                //con1.Close();
                                //cmd.Dispose();

                                Flag = 1;
                            }

                            List<string> sales = new List<string>();

                            sales.Add(cat);
                            sales.Add(FromDate + ToDate + dropInvoiceNo.SelectedItem.Text.Trim());
                            sales.Add(prod_id);
                            sales.Add(txtcusttype.Text);
                            sales.Add(arrProdName[2].ToString());
                            sales.Add(Qty[cc].Text);
                            sales.Add(lblInvoiceDate.Text.Trim());

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(sales);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/InsertSalesOil", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            con1.Open();

                            ////cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
                            //if(cat=="")
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);                                  
                            //	//Mahesh11.04.007 cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+invoice+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
                            //	//Mahesh 05.11.007 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);

                            //	//Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);

                            //else if(cat.StartsWith("2t") || cat.StartsWith("2T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+","+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//Mahesh11.04.007 cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+invoice+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);
                            //	//**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                            //	//Comment by vikas 1.05.09cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);

                            //else if(cat.StartsWith("4t") || cat.StartsWith("4T"))
                            //	// cmd=new SqlCommand("insert into Sales_Oil values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+invoice+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()))+"')",con1);

                            //	// Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);

                            //cmd.ExecuteNonQuery();
                            //con1.Close();
                            //cmd.Dispose();
                        }
                        else
                        {
                            List<string> sales = new List<string>();

                            sales.Add(cat);
                            sales.Add(FromDate + ToDate + lblInvoiceNo.Text);
                            sales.Add(prod_id);
                            sales.Add(txtcusttype.Text);
                            sales.Add(arrProdName[2].ToString());
                            sales.Add(Qty[cc].Text);
                            sales.Add(lblInvoiceDate.Text.Trim());

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(sales);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/UpdateSalesOil", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //                     con1.Open ();
                            ////cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid1.Text+"',"+GenUtil.changeqty(txtPack1.Value,bi)+","+GenUtil.changeqty(txtPack1.Value,ri)+","+GenUtil.changeqty(txtPack1.Value,oi)+","+GenUtil.changeqty(txtPack1.Value,fi)+","+GenUtil.changeqty(txtPack1.Value,ii)+","+GenUtil.changeqty(txtPack1.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //if(cat=="")
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                            //	// Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);                                

                            //else if (cat.StartsWith("2t") || cat.StartsWith("2T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+","+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                            //	//Comment by vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+","+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);

                            //else if(cat.StartsWith("4t") || cat.StartsWith("4T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",0,"+GenUtil.changeqty(txtPack1.Value,int.Parse(txtQty1.Text))+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                            //	//**cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(PackType[cc].Value,int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                            //	//Comment by vikas 1.05.09cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(arrProdName[1].ToString(),int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",0,"+GenUtil.changeqty(arrProdName[2].ToString(),int.Parse(Qty[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);

                            //cmd.ExecuteNonQuery();
                            //con1.Close();
                            //cmd.Dispose();
                        }
                    }
                    //if(ProdName1[cc].Text != "" && Qty1[cc].Text != "")
                    if (arrProdschName1[0].ToString() != "" && Qty1[cc].Text != "")
                    {
                        string sql, cat = "";
                        string prod_id = "";

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(baseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var Res = client.GetAsync("api/sales/GetCategory?prodName1=" + arrProdschName1[0].ToString() + "&prodName2=" + arrProdschName1[1].ToString()).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                cat = JsonConvert.DeserializeObject<string>(id);
                            }
                        }

                        //                  //sql="select prod_id,category from Products where prod_name='"+ProdName1[cc].Text+"' and pack_type='"+PackType1[cc].Text+"'";
                        //                  sql ="select prod_id,category from Products where prod_name='"+arrProdschName1[0].ToString()+"' and pack_type='"+arrProdschName1[1].ToString()+"'";
                        //SqlDtr=obj.GetRecordSet(sql);
                        //while(SqlDtr.Read())
                        //{
                        //	prod_id=SqlDtr.GetValue(0).ToString();
                        //	if(SqlDtr.GetValue(1).ToString().StartsWith("2T") || SqlDtr.GetValue(1).ToString().StartsWith("4T") || SqlDtr.GetValue(1).ToString().StartsWith("2t") || SqlDtr.GetValue(1).ToString().StartsWith("4t"))
                        //		cat=SqlDtr.GetValue(1).ToString();
                        //}
                        //SqlDtr.Close ();

                        if (lblInvoiceNo.Visible == false)
                        {
                            List<string> sales = new List<string>();

                            sales.Add(cat);
                            sales.Add(FromDate + ToDate + dropInvoiceNo.SelectedItem.Text.Trim());
                            sales.Add(prod_id);
                            sales.Add(txtcusttype.Text);
                            sales.Add(arrProdschName1[1].ToString());
                            sales.Add(Qty[cc].Text);
                            sales.Add(lblInvoiceDate.Text.Trim());

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(sales);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/InsertSalesOilProdschName", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //                     con1.Open ();
                            //if(cat=="")
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);

                            //	//Comment By vikas 1.05.09 cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);

                            //else if(cat.StartsWith("2t") || cat.StartsWith("2T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+","+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+","+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);
                            //else if(cat.StartsWith("4t") || cat.StartsWith("4T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);
                            //cmd.ExecuteNonQuery();
                            //con1.Close();
                            //cmd.Dispose();
                        }
                        else
                        {
                            List<string> sales = new List<string>();

                            sales.Add(cat);
                            sales.Add(FromDate + ToDate + lblInvoiceNo.Text);
                            sales.Add(prod_id);
                            sales.Add(txtcusttype.Text);
                            sales.Add(arrProdschName1[1].ToString());
                            sales.Add(Qty[cc].Text);
                            sales.Add(lblInvoiceDate.Text.Trim());

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                var myContent = JsonConvert.SerializeObject(sales);
                                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                                var byteContent = new ByteArrayContent(buffer);
                                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                var response = client.PostAsync("api/sales/UpdateSalesOilProdschName", byteContent).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    string responseString = response.Content.ReadAsStringAsync().Result;
                                }
                            }

                            //                     con1.Open ();
                            //if(cat=="")
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+","+prod_id+",'"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);
                            //else if(cat.StartsWith("2t") || cat.StartsWith("2T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+","+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+","+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);
                            //else if(cat.StartsWith("4t") || cat.StartsWith("4T"))
                            //	//cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",0,"+GenUtil.changeqty(PackType1[cc].Text,int.Parse(Qty1[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103))",con1);
                            //	cmd=new SqlCommand("insert into Sales_Oil values("+FromDate+ToDate+lblInvoiceNo.Text+",'"+prod_id+"','"+txtcusttype.Text+"',"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",0,"+GenUtil.changeqty(arrProdschName1[1].ToString(),int.Parse(Qty1[cc].Text))+",Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103))",con1);
                            //cmd.ExecuteNonQuery();
                            //con1.Close();
                            //cmd.Dispose();
                        }
                    }
                }
                //				if(txtProdName2.Value =="" && txtQty2.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr; 
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName2.Value+"' and pack_type='"+txtPack2.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid2.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty2.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty2.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty2.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty2.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty2.Text);
                //					//****************
                //					if(DropType2.SelectedItem.Text.Equals(cat))// || DropType2.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty2.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid2.Text+"',"+GenUtil.changeqty(txtPack2.Value,bi)+","+GenUtil.changeqty(txtPack2.Value,ri)+","+GenUtil.changeqty(txtPack2.Value,oi)+","+GenUtil.changeqty(txtPack2.Value,fi)+","+GenUtil.changeqty(txtPack2.Value,ii)+","+GenUtil.changeqty(txtPack2.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid2.Text+"',"+GenUtil.changeqty(txtPack2.Value,bi)+","+GenUtil.changeqty(txtPack2.Value,ri)+","+GenUtil.changeqty(txtPack2.Value,oi)+","+GenUtil.changeqty(txtPack2.Value,fi)+","+GenUtil.changeqty(txtPack2.Value,ii)+","+GenUtil.changeqty(txtPack2.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName3.Value =="" && txtQty3.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName3.Value+"' and pack_type='"+txtPack3.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid3.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty3.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty3.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty3.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty3.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty3.Text);
                //					//****************
                //					if(DropType3.SelectedItem.Text.Equals(cat))// || DropType3.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty3.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid3.Text+"',"+GenUtil.changeqty(txtPack3.Value,bi)+","+GenUtil.changeqty(txtPack3.Value,ri)+","+GenUtil.changeqty(txtPack3.Value,oi)+","+GenUtil.changeqty(txtPack3.Value,fi)+","+GenUtil.changeqty(txtPack3.Value,ii)+","+GenUtil.changeqty(txtPack3.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid3.Text+"',"+GenUtil.changeqty(txtPack3.Value,bi)+","+GenUtil.changeqty(txtPack3.Value,ri)+","+GenUtil.changeqty(txtPack3.Value,oi)+","+GenUtil.changeqty(txtPack3.Value,fi)+","+GenUtil.changeqty(txtPack3.Value,ii)+","+GenUtil.changeqty(txtPack3.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName4.Value =="" && txtQty4.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName4.Value+"' and pack_type='"+txtPack4.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid4.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty4.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty4.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty4.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty4.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty4.Text);
                //					//****************
                //					if(DropType4.SelectedItem.Text.Equals(cat))// || DropType4.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty4.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid4.Text+"',"+GenUtil.changeqty(txtPack4.Value,bi)+","+GenUtil.changeqty(txtPack4.Value,ri)+","+GenUtil.changeqty(txtPack4.Value,oi)+","+GenUtil.changeqty(txtPack4.Value,fi)+","+GenUtil.changeqty(txtPack4.Value,ii)+","+GenUtil.changeqty(txtPack4.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid4.Text+"',"+GenUtil.changeqty(txtPack4.Value,bi)+","+GenUtil.changeqty(txtPack4.Value,ri)+","+GenUtil.changeqty(txtPack4.Value,oi)+","+GenUtil.changeqty(txtPack4.Value,fi)+","+GenUtil.changeqty(txtPack4.Value,ii)+","+GenUtil.changeqty(txtPack4.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName5.Value =="" && txtQty5.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName5.Value+"' and pack_type='"+txtPack5.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid5.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty5.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty5.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty5.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty5.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty5.Text);
                //					//****************
                //					if(DropType5.SelectedItem.Text.Equals(cat))// || DropType5.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty5.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid5.Text+"',"+GenUtil.changeqty(txtPack5.Value,bi)+","+GenUtil.changeqty(txtPack5.Value,ri)+","+GenUtil.changeqty(txtPack5.Value,oi)+","+GenUtil.changeqty(txtPack5.Value,fi)+","+GenUtil.changeqty(txtPack5.Value,ii)+","+GenUtil.changeqty(txtPack5.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid5.Text+"',"+GenUtil.changeqty(txtPack5.Value,bi)+","+GenUtil.changeqty(txtPack5.Value,ri)+","+GenUtil.changeqty(txtPack5.Value,oi)+","+GenUtil.changeqty(txtPack5.Value,fi)+","+GenUtil.changeqty(txtPack5.Value,ii)+","+GenUtil.changeqty(txtPack5.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName6.Value =="" && txtQty6.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName6.Value+"' and pack_type='"+txtPack6.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid6.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty6.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty6.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty6.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty6.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty6.Text);
                //					//****************
                //					if(DropType6.SelectedItem.Text.Equals(cat))// || DropType6.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty6.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid6.Text+"',"+GenUtil.changeqty(txtPack6.Value,bi)+","+GenUtil.changeqty(txtPack6.Value,ri)+","+GenUtil.changeqty(txtPack6.Value,oi)+","+GenUtil.changeqty(txtPack6.Value,fi)+","+GenUtil.changeqty(txtPack6.Value,ii)+","+GenUtil.changeqty(txtPack6.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid6.Text+"',"+GenUtil.changeqty(txtPack6.Value,bi)+","+GenUtil.changeqty(txtPack6.Value,ri)+","+GenUtil.changeqty(txtPack6.Value,oi)+","+GenUtil.changeqty(txtPack6.Value,fi)+","+GenUtil.changeqty(txtPack6.Value,ii)+","+GenUtil.changeqty(txtPack6.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName7.Value =="" && txtQty7.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName7.Value+"' and pack_type='"+txtPack7.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid7.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty7.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty7.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty7.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty7.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty7.Text);
                //					//****************
                //					if(DropType7.SelectedItem.Text.Equals(cat))// || DropType7.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty7.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid7.Text+"',"+GenUtil.changeqty(txtPack7.Value,bi)+","+GenUtil.changeqty(txtPack7.Value,ri)+","+GenUtil.changeqty(txtPack7.Value,oi)+","+GenUtil.changeqty(txtPack7.Value,fi)+","+GenUtil.changeqty(txtPack7.Value,ii)+","+GenUtil.changeqty(txtPack7.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid7.Text+"',"+GenUtil.changeqty(txtPack7.Value,bi)+","+GenUtil.changeqty(txtPack7.Value,ri)+","+GenUtil.changeqty(txtPack7.Value,oi)+","+GenUtil.changeqty(txtPack7.Value,fi)+","+GenUtil.changeqty(txtPack7.Value,ii)+","+GenUtil.changeqty(txtPack7.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName8.Value =="" && txtQty8.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName8.Value+"' and pack_type='"+txtPack8.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid8.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty8.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty8.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty8.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty8.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty8.Text);
                //					//****************
                //					if(DropType8.SelectedItem.Text.Equals(cat) || DropType8.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty8.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid8.Text+"',"+GenUtil.changeqty(txtPack8.Value,bi)+","+GenUtil.changeqty(txtPack8.Value,ri)+","+GenUtil.changeqty(txtPack8.Value,oi)+","+GenUtil.changeqty(txtPack8.Value,fi)+","+GenUtil.changeqty(txtPack8.Value,ii)+","+GenUtil.changeqty(txtPack8.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid8.Text+"',"+GenUtil.changeqty(txtPack8.Value,bi)+","+GenUtil.changeqty(txtPack8.Value,ri)+","+GenUtil.changeqty(txtPack8.Value,oi)+","+GenUtil.changeqty(txtPack8.Value,fi)+","+GenUtil.changeqty(txtPack8.Value,ii)+","+GenUtil.changeqty(txtPack8.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				//*************************
                //				if(txtProdName9.Value =="" && txtQty9.Text == "")
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName9.Value+"' and pack_type='"+txtPack9.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid9.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty9.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty9.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty9.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty9.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty9.Text);
                //					//****************
                //					if(DropType9.SelectedItem.Text.Equals(cat))// || DropType9.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty9.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid9.Text+"',"+GenUtil.changeqty(txtPack9.Value,bi)+","+GenUtil.changeqty(txtPack9.Value,ri)+","+GenUtil.changeqty(txtPack9.Value,oi)+","+GenUtil.changeqty(txtPack9.Value,fi)+","+GenUtil.changeqty(txtPack9.Value,ii)+","+GenUtil.changeqty(txtPack9.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid9.Text+"',"+GenUtil.changeqty(txtPack9.Value,bi)+","+GenUtil.changeqty(txtPack9.Value,ri)+","+GenUtil.changeqty(txtPack9.Value,oi)+","+GenUtil.changeqty(txtPack9.Value,fi)+","+GenUtil.changeqty(txtPack9.Value,ii)+","+GenUtil.changeqty(txtPack9.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName10.Value =="" && txtQty10.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName10.Value+"' and pack_type='"+txtPack10.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid10.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty10.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty10.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty10.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty10.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty10.Text);
                //					//****************
                //					if(DropType10.SelectedItem.Text.Equals(cat))// || DropType10.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty10.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid10.Text+"',"+GenUtil.changeqty(txtPack10.Value,bi)+","+GenUtil.changeqty(txtPack10.Value,ri)+","+GenUtil.changeqty(txtPack10.Value,oi)+","+GenUtil.changeqty(txtPack10.Value,fi)+","+GenUtil.changeqty(txtPack10.Value,ii)+","+GenUtil.changeqty(txtPack10.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid10.Text+"',"+GenUtil.changeqty(txtPack10.Value,bi)+","+GenUtil.changeqty(txtPack10.Value,ri)+","+GenUtil.changeqty(txtPack10.Value,oi)+","+GenUtil.changeqty(txtPack10.Value,fi)+","+GenUtil.changeqty(txtPack10.Value,ii)+","+GenUtil.changeqty(txtPack10.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName11.Value =="" && txtQty11.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName11.Value+"' and pack_type='"+txtPack11.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid11.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty11.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty11.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty11.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty11.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty11.Text);
                //					//****************
                //					if(DropType11.SelectedItem.Text.Equals(cat))// || DropType11.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty11.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid11.Text+"',"+GenUtil.changeqty(txtPack11.Value,bi)+","+GenUtil.changeqty(txtPack11.Value,ri)+","+GenUtil.changeqty(txtPack11.Value,oi)+","+GenUtil.changeqty(txtPack11.Value,fi)+","+GenUtil.changeqty(txtPack11.Value,ii)+","+GenUtil.changeqty(txtPack11.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid11.Text+"',"+GenUtil.changeqty(txtPack11.Value,bi)+","+GenUtil.changeqty(txtPack11.Value,ri)+","+GenUtil.changeqty(txtPack11.Value,oi)+","+GenUtil.changeqty(txtPack11.Value,fi)+","+GenUtil.changeqty(txtPack11.Value,ii)+","+GenUtil.changeqty(txtPack11.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //				if(txtProdName12.Value =="" && txtQty12.Text == "" )
                //				{
                //				}	
                //				else
                //				{
                //					InventoryClass obj=new InventoryClass();
                //					SqlDataReader SqlDtr;
                //					string sql,cat="";
                //					string prod_id="";
                //					sql="select prod_id,category from Products where prod_name='"+txtProdName12.Value+"' and pack_type='"+txtPack12.Value+"'";
                //					SqlDtr=obj.GetRecordSet(sql);
                //					while(SqlDtr.Read())
                //					{
                //						txtmwid12.Text=SqlDtr.GetValue(0).ToString();
                //						prod_id=SqlDtr.GetValue(0).ToString();
                //						cat=SqlDtr.GetValue(1).ToString();
                //					}
                //					SqlDtr.Close ();
                //					int bi=0,ri=0,oi=0,ii=0,fi=0,ti=0;
                //					if(txtcusttype.Text=="Bazzar")
                //						bi=System.Convert.ToInt32(txtQty12.Text);
                //					if(txtcusttype.Text=="Ro-1" || txtcusttype.Text=="Ro-2" || txtcusttype.Text=="Ro-3")
                //						ri=System.Convert.ToInt32(txtQty12.Text);
                //					if(txtcusttype.Text=="Oe(muv)"  || txtcusttype.Text=="Oe(lcv)" || txtcusttype.Text=="Oe(tractor)" || txtcusttype.Text=="Oe(hcv)" || txtcusttype.Text=="Oe(maruti)" || txtcusttype.Text=="Oe(hyundai)" || txtcusttype.Text=="Oe(force)" || txtcusttype.Text=="Oe(eicher)" || txtcusttype.Text=="Oe(garage)"|| txtcusttype.Text=="Oe(others)")
                //						oi=System.Convert.ToInt32(txtQty12.Text);
                //					if(txtcusttype.Text=="Fleet")
                //						fi=System.Convert.ToInt32(txtQty12.Text);
                //					if(txtcusttype.Text=="Ibp")
                //						ii=System.Convert.ToInt32(txtQty12.Text);
                //					//****************
                //					if(DropType12.SelectedItem.Text.Equals(cat))// || DropType12.SelectedItem.Text.Equals("2T/4T"))
                //						ti=System.Convert.ToInt32(txtQty12.Text);
                //					//********************
                //					if(lblInvoiceNo.Visible==false)
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("delete from monthwise1 where invoice_no='"+dropInvoiceNo.SelectedItem.Text.Trim()+"' and prod_id='"+prod_id+"'",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+dropInvoiceNo.SelectedItem.Text.Trim()+",'"+txtmwid12.Text+"',"+GenUtil.changeqty(txtPack12.Value,bi)+","+GenUtil.changeqty(txtPack12.Value,ri)+","+GenUtil.changeqty(txtPack12.Value,oi)+","+GenUtil.changeqty(txtPack12.Value,fi)+","+GenUtil.changeqty(txtPack12.Value,ii)+","+GenUtil.changeqty(txtPack12.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //					else
                //					{
                //						con1.Open ();
                //						cmd=new SqlCommand("insert into monthwise1 values("+lblInvoiceNo.Text+",'"+txtmwid12.Text+"',"+GenUtil.changeqty(txtPack12.Value,bi)+","+GenUtil.changeqty(txtPack12.Value,ri)+","+GenUtil.changeqty(txtPack12.Value,oi)+","+GenUtil.changeqty(txtPack12.Value,fi)+","+GenUtil.changeqty(txtPack12.Value,ii)+","+GenUtil.changeqty(txtPack12.Value,ti)+",'"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))+"')",con1);
                //						cmd.ExecuteNonQuery();
                //						con1.Close();
                //						cmd.Dispose();
                //					}
                //				}
                //*************************
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:insertSalesOil(), EXCEPTION  " + ex.Message + " userid " + "   " + "   " + uid);
            }
        }

        /// <summary>
        /// This method are not used in this project.
        /// </summary>
        public void SaveForReport()
        {
            try
            {
                Sysitem.Classes.InventoryClass obj = new InventoryClass();
                obj.InvoiceNo = FromDate + ToDate + lblInvoiceNo.Text.ToString();
                //Mahesh11.04.007 
                obj.ToDate = lblInvoiceDate.Text;
                //obj.ToDate= Session["CurrentDate"].ToString();
                //obj.CustomerName=DropCustName.SelectedItem.Value.ToString();

                /*************Start*************************/
                //Coment by vikas 1.05.09 obj.CustomerName=text1.Value;
                string txtval = text1.Value.Substring(0, text1.Value.IndexOf(":"));// add by vikas 1.05.09
                obj.CustomerName = txtval;
                /******************end********************/

                obj.Place = lblPlace.Value.ToString();
                obj.DueDate = lblDueDate.Value.ToString();
                obj.CurrentBalance = lblCurrBalance.Value.ToString();
                obj.VehicleNo = txtVehicleNo.Text.ToString();
                /*obj.Prod1=txtProdName1.Value.ToString(); 
				obj.Prod2=txtProdName2.Value.ToString();
				obj.Prod3=txtProdName3.Value.ToString();
				obj.Prod4=txtProdName4.Value.ToString();
				obj.Prod5=txtProdName5.Value.ToString();
				obj.Prod6=txtProdName6.Value.ToString();
				obj.Prod7=txtProdName7.Value.ToString();
				obj.Prod8=txtProdName8.Value.ToString();*/
                obj.Qty1 = txtQty1.Text.ToString();
                obj.Qty2 = txtQty2.Text.ToString();
                obj.Qty3 = txtQty3.Text.ToString();
                obj.Qty4 = txtQty4.Text.ToString();
                obj.Qty5 = txtQty5.Text.ToString();
                obj.Qty6 = txtQty6.Text.ToString();
                obj.Qty7 = txtQty7.Text.ToString();
                obj.Qty8 = txtQty8.Text.ToString();
                obj.Rate1 = txtRate1.Text.ToString();
                obj.Rate2 = txtRate2.Text.ToString();
                obj.Rate3 = txtRate3.Text.ToString();
                obj.Rate4 = txtRate4.Text.ToString();
                obj.Rate5 = txtRate5.Text.ToString();
                obj.Rate6 = txtRate6.Text.ToString();
                obj.Rate7 = txtRate7.Text.ToString();
                obj.Rate8 = txtRate8.Text.ToString();
                obj.Amt1 = txtAmount1.Text.ToString();
                obj.Amt2 = txtAmount2.Text.ToString();
                obj.Amt3 = txtAmount3.Text.ToString();
                obj.Amt4 = txtAmount4.Text.ToString();
                obj.Amt5 = txtAmount5.Text.ToString();
                obj.Amt6 = txtAmount6.Text.ToString();
                obj.Amt7 = txtAmount7.Text.ToString();
                obj.Amt8 = txtAmount8.Text.ToString();
                obj.Total = Request.Form["txtNetAmount"].ToString();
                obj.Promo = txtPromoScheme.Text.ToString();
                obj.Remarks = txtRemark.Text.ToString();
                obj.InsertSalesInvoiceDuplicate();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : SaveForReport()  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// Sales Typ is credit then display the Text field to enter the slip no. else hide it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropSalesType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //			if(DropSalesType.SelectedIndex==2)
            //			{
            //				lblSlipNo.Visible=true;
            //				txtSlipNo.Visible=true;
            //				Requiredfieldvalidator2.Visible = true; 
            //			}
            //			else
            //			{
            //				lblSlipNo.Visible=false;
            //				txtSlipNo.Visible=false;
            //				Requiredfieldvalidator2.Visible = false; 
            //			}
        }

        ///// <summary>
        ///// This method fatching all invoice no and fill into the dropdownlist.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnEdit_Click(object sender, System.EventArgs e)
        //{           
        //          //try
        //          //{

        //          //	tempEdit.Value="False";          //Add by vikas 14.07.09

        //          //	lblInvoiceNo.Visible=false;
        //          //	btnEdit.Visible=false;
        //          //	dropInvoiceNo.Visible=true;
        //          //	btnSave.Enabled = true;
        //          //	Button1.Enabled = true;
        //          //	//Coment by vikas 06.09 DropSalesType.Enabled=false;
        //          //	DropOrderInvoice.SelectedIndex=0;
        //          //	DropOrderInvoice.Enabled=false;
        //          //	//checkPrePrint();
        //          //	InventoryClass obj=new InventoryClass();
        //          //	SqlDataReader SqlDtr;
        //          //	string sql;
        //          //	#region Fetch the All Invoice Number and fill in Combo
        //          //	dropInvoiceNo.Items.Clear();
        //          //	dropInvoiceNo.Items.Add("Select");
        //          //	if(FromDate!="")
        //          //	{
        //          //		sql="select Invoice_No from Sales_Master where Invoice_No like '"+FromDate+ToDate+"%' order by Invoice_No";
        //          //		SqlDtr=obj.GetRecordSet(sql);
        //          //		while(SqlDtr.Read())
        //          //		{
        //          //			if(FromDate.StartsWith("0"))
        //          //				dropInvoiceNo.Items.Add(SqlDtr.GetValue(0).ToString().Substring(2));
        //          //			else
        //          //				dropInvoiceNo.Items.Add(SqlDtr.GetValue(0).ToString().Substring(3));
        //          //		}
        //          //		SqlDtr.Close ();
        //          //	}
        //          //	#endregion
        //          //}
        //          //catch(Exception ex)
        //          //{
        //          //	CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : btnEdit_Click  EXCEPTION :  "+ ex.Message+"   "+uid);
        //          //}
        //      }




        /// <summary>
        /// This method fatch the all required information according to invoice no who select from dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropInvoiceNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {


            SalesModel sales = new SalesModel();

            string str = sales.Invoice_Date;
            TextSelect.Text = dropInvoiceNo.SelectedItem.Value.ToString();
            try
            {
                DropInvoiceNo_Selected();
                //if(TextSelect.Text=="Select")
                //{
                //	MessageBox.Show("Please Select Invoice No");
                //}
                //else
                //{					

                //	Clear();                    

                //	HtmlInputText[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12};                    

                //	TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
                //	TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
                //	TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
                //	TextBox[]  AvStock = {txtAvStock1,txtAvStock2,txtAvStock3,txtAvStock4,txtAvStock5,txtAvStock6,txtAvStock7,txtAvStock8,txtAvStock9,txtAvStock10,txtAvStock11,txtAvStock12};
                //	TextBox[]  tempQty = {txtTempQty1, txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12}; 
                //	TextBox[]  tempSchQty = {txtTempSchQty1,txtTempSchQty2,txtTempSchQty3,txtTempSchQty4,txtTempSchQty5,txtTempSchQty6,txtTempSchQty7,txtTempSchQty8 ,txtTempSchQty9,txtTempSchQty10,txtTempSchQty11,txtTempSchQty12};
                //	HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12};
                //	HtmlInputHidden[] tmpSchType = {tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12};
                //	HtmlInputHidden[] tmpFoeType = {tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12};
                //	HtmlInputHidden[]  SchSPType = {tmpSecSPType1,tmpSecSPType2,tmpSecSPType3,tmpSecSPType4,tmpSecSPType5,tmpSecSPType6,tmpSecSPType7,tmpSecSPType8,tmpSecSPType9,tmpSecSPType10,tmpSecSPType11,tmpSecSPType12};
                //	HtmlInputHidden[]  SchSP = {txtTempSecSP1,txtTempSecSP2,txtTempSecSP3,txtTempSecSP4,txtTempSecSP5,txtTempSecSP6,txtTempSecSP7,txtTempSecSP8,txtTempSecSP9,txtTempSecSP10,txtTempSecSP11,txtTempSecSP12};
                //	//****
                //	TextBox[] pid={txtpname1,txtpname2,txtpname3,txtpname4,txtpname5,txtpname6,txtpname7,txtpname8,txtpname9,txtpname10,txtpname11,txtpname12};
                //	TextBox[] pid1={txtmwid1,txtmwid2,txtmwid3,txtmwid4,txtmwid5,txtmwid6,txtmwid7,txtmwid8,txtmwid9,txtmwid10,txtmwid11,txtmwid12};
                //	TextBox[]  scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};
                //	//****************************
                //	TextBox[]  foe = {txtfoe1,txtfoe2,txtfoe3,txtfoe4,txtfoe5,txtfoe6,txtfoe7,txtfoe8,txtfoe9,txtfoe10,txtfoe11,txtfoe12};
                //	//***************************
                //	//*************************
                //	TextBox[] ProdType1={txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12}; 
                //	//**TextBox[] ProdName1={txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12}; 
                //	//**TextBox[] PackType1={txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12}; 
                //	TextBox[] Qty1={txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12}; 
                //	TextBox[] stk1={txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12}; 
                //	//**************************

                //	//**********end*************
                //	InventoryClass  obj=new InventoryClass ();
                //	InventoryClass  obj1=new InventoryClass ();
                //	SqlDataReader SqlDtr;
                //	string sql,sql1;
                //	SqlDataReader rdr=null,rdr1=null,rdr2=null,rdr3=null;
                //	int i=0;
                //	FlagPrint=false;
                //	Button1.CausesValidation=true;

                //	#region Get Data from Sales Master Table regarding Invoice No.
                //	if(FromDate!="")
                //		//sql="select * from Sales_Master where Invoice_No='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value +"'" ;
                //		sql="select * from Sales_Master sm,employee e where Under_SalesMan=emp_id and Invoice_No='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value +"'" ;
                //	else
                //	{
                //		MessageBox.Show("Please Fill The Organisation Form First");
                //		return;
                //	}
                //	SqlDtr=obj.GetRecordSet(sql); 
                //	while(SqlDtr.Read())
                //	{
                //		Invoice_Date = SqlDtr.GetValue(1).ToString();
                //		string strDate = SqlDtr.GetValue(1).ToString().Trim();
                //		int pos = strDate.IndexOf(" ");
                //		if(pos != -1)
                //		{
                //			strDate = strDate.Substring(0,pos);
                //		}
                //		else
                //		{
                //			strDate = "";					
                //		}
                //		lblInvoiceDate.Text =GenUtil.str2DDMMYYYY(strDate);
                //		tempInvoiceDate.Value=GenUtil.str2DDMMYYYY(strDate);
                //		DropSalesType.SelectedIndex=(DropSalesType.Items.IndexOf((DropSalesType.Items.FindByValue (SqlDtr.GetValue(2).ToString()))));
                //		//DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr.GetValue(4).ToString()))));
                //		DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr["Emp_Name"].ToString()))));
                //		if(getCustomerVehicles(SqlDtr["Cust_ID"].ToString()) == true)
                //		{
                //			DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(SqlDtr.GetValue(5).ToString().Trim()));
                //		}
                //		else
                //		{
                //			txtVehicleNo.Text=SqlDtr.GetValue(5).ToString();
                //		}
                //		//txtGrandTotal.Text=SqlDtr.GetValue(6).ToString();
                //		//txtGrandTotal.Text = GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()); 
                //		txtDisc.Text=SqlDtr.GetValue(7).ToString(); 
                //		txtDisc.Text = GenUtil.strNumericFormat(txtDisc.Text.ToString()); 
                //		DropDiscType.SelectedIndex= DropDiscType.Items.IndexOf((DropDiscType.Items.FindByValue(SqlDtr.GetValue(8).ToString())));
                //		txtNetAmount.Text =SqlDtr.GetValue(9).ToString(); 
                //		txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
                //		tempNetAmount.Value=SqlDtr.GetValue(9).ToString();                               //Add by vikas 14.07.09
                //		tempNetAmount.Value=GenUtil.strNumericFormat(tempNetAmount.Value.ToString());     //Add by vikas 14.07.09

                //		NetAmount=GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
                //		txtPromoScheme.Text= SqlDtr.GetValue(10).ToString(); 
                //		txtRemark.Text=SqlDtr.GetValue(11).ToString();  
                //		lblEntryBy.Text=SqlDtr.GetValue(12).ToString();  
                //		lblEntryTime.Text= SqlDtr.GetValue(13).ToString();  
                //		txtSecondrySpDisc.Text=SqlDtr["SecSPDisc"].ToString();
                //                    //******************
                //                    if (SqlDtr["Discount_type"].ToString() == "Per")
                //                    {
                //                        txtDiscount.Text = System.Convert.ToString((double.Parse(SqlDtr["Grand_Total"].ToString()) - double.Parse(SqlDtr["schdiscount"].ToString())) * double.Parse(SqlDtr["discount"].ToString()) / 100);
                //                        txtDiscount.Text = System.Convert.ToString(Math.Round(double.Parse(txtDiscount.Text), 2));
                //                    }
                //                    else
                //                    {
                //                        double Discount = double.Parse(GenUtil.strNumericFormat(SqlDtr["Discount"].ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));
                //                        txtDiscount.Text = GenUtil.strNumericFormat(Discount.ToString());
                //                    }


                //                    if (SqlDtr["cash_Disc_type"].ToString() == "Per")
                //                    {
                //                        double tot = 0;
                //                        if (txtDiscount.Text != "")
                //                            tot = double.Parse(SqlDtr["Grand_Total"].ToString()) - (double.Parse(SqlDtr["schdiscount"].ToString()) + double.Parse(SqlDtr["foediscount"].ToString()) + double.Parse(txtDiscount.Text));
                //                        else
                //                            tot = double.Parse(SqlDtr["Grand_Total"].ToString()) - (double.Parse(SqlDtr["schdiscount"].ToString()) + double.Parse(SqlDtr["foediscount"].ToString()));
                //                        txtCashDiscount.Text = System.Convert.ToString(tot * double.Parse(SqlDtr["Cash_Discount"].ToString()) / 100);
                //                        txtCashDiscount.Text = System.Convert.ToString(Math.Round(double.Parse(txtCashDiscount.Text), 2));
                //                        tempcashdis.Value = txtCashDiscount.Text;
                //                    }
                //                    else
                //                    {
                //                        double cashDiscount = double.Parse(GenUtil.strNumericFormat(SqlDtr["Cash_Discount"].ToString())) * double.Parse(GenUtil.strNumericFormat(SqlDtr["totalqtyltr"].ToString()));
                //                        txtCashDiscount.Text = GenUtil.strNumericFormat(cashDiscount.ToString());
                //                    }

                //                    txtCashDisc.Text=SqlDtr.GetValue(15).ToString();
                //		txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString());
                //		DropCashDiscType.SelectedIndex= DropCashDiscType.Items.IndexOf((DropCashDiscType.Items.FindByValue(SqlDtr.GetValue(16).ToString())));
                //		txtVAT.Text =  SqlDtr.GetValue(17).ToString();
                //		txtschemetotal.Text=SqlDtr.GetValue(18).ToString();
                //		txtfleetoediscount.Text=SqlDtr.GetValue(19).ToString();
                //		dropfleetoediscount.SelectedIndex= dropfleetoediscount.Items.IndexOf((dropfleetoediscount.Items.FindByValue(SqlDtr.GetValue(20).ToString())));
                //		txtfleetoediscountRs.Text=SqlDtr.GetValue(21).ToString();
                //		txtliter.Text=SqlDtr.GetValue(22).ToString();
                //                    Textcgst.Text = SqlDtr.GetValue(27).ToString();
                //                    Textsgst.Text = SqlDtr.GetValue(26).ToString();
                //		if(SqlDtr["ChallanNo"].ToString()=="0")
                //			txtChallanNo.Text="";
                //		else
                //			txtChallanNo.Text=SqlDtr["ChallanNo"].ToString();
                //		if(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString())=="1/1/1900")
                //			txtChallanDate.Text="";
                //		else
                //			txtChallanDate.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()));
                //		if(txtVAT.Text.Trim() == "0")
                //		{
                //			Yes.Checked = false;
                //			No.Checked = true;
                //		}
                //		else
                //		{
                //			No.Checked = false;
                //			Yes.Checked = true;
                //		}
                //	}
                //	SqlDtr.Close();
                //	#endregion

                //	#region Get Customer name and place regarding Customer ID
                //	//*****add Cust_type
                //	//coment by vikas 7.11.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID from Customer as c, sales_master as s where c.Cust_ID= s.Cust_ID and s.Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedValue +"'";
                //	sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, sales_master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedValue +"'";
                //	SqlDtr=obj.GetRecordSet(sql);
                //	while(SqlDtr.Read())
                //	{
                //		//**DropCustName.SelectedIndex=DropCustName.Items.IndexOf(DropCustName.Items.FindByValue(SqlDtr.GetValue(0).ToString()));

                //		//texthidden1.Value=SqlDtr.GetValue(0).ToString(); //Comment by vikas sharma 27.04.09
                //		//text1.Value=SqlDtr.GetValue(0).ToString(); //Comment by vikas sharma 27.04.09

                //		texthidden1.Value=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString(); //Add by vikas sharma 27.04.09
                //		text1.Value=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString(); //Add by vikas sharma 27.04.09

                //		//Cache["CustName"]=SqlDtr.GetValue(0).ToString();
                //		CustID=SqlDtr["Cust_ID"].ToString();

                //		lblPlace.Value=SqlDtr.GetValue(1).ToString();
                //		DateTime duedate=DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
                //		string duedatestr=(duedate.ToShortDateString());
                //		lblDueDate.Value =GenUtil.str2DDMMYYYY(duedatestr);
                //		lblCurrBalance.Value=GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());
                //		TxtCrLimit.Value = SqlDtr.GetValue(4).ToString();
                //		lblCreditLimit.Value  = SqlDtr.GetValue(4).ToString();
                //		txtcusttype.Text = SqlDtr.GetValue(5).ToString();
                //		/***Add by vikas 10.11.2012 *********************/
                //		if(SqlDtr["Group_Name"].ToString()!=null && SqlDtr["Group_Name"].ToString()!="")
                //			tempCustGroup.Value=SqlDtr["Group_Name"].ToString();
                //		/*********************************************/
                //	}
                //	SqlDtr.Close();

                //	//Coment by vikas 06.08.09 sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, sales_master as s where c.CustID= s.Cust_ID and s.Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedValue+"' order by entrydate desc";
                //	sql="select top 1 Balance,BalanceType from customerledgertable where CustID="+CustID+" order by EntryDate Desc";
                //	SqlDtr=obj.GetRecordSet(sql);
                //	while(SqlDtr.Read())
                //	{
                //		lblCurrBalance.Value=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString())+" "+SqlDtr.GetValue(1).ToString();
                //	}
                //	SqlDtr.Close();

                //	#endregion

                //	#region Get Customer Slip
                //	/* Mahesh
                //	sql="select start_no, end_no from slip,  sales_master as sm where slip.Cust_ID = sm.Cust_ID and Invoice_No='"+dropInvoiceNo.SelectedValue +"'";
                //	SqlDtr=obj.GetRecordSet(sql);
                //	if(SqlDtr.Read())
                //	{

                //		Txtstart.Value = SqlDtr.GetValue(0).ToString();
                //		TxtEnd.Value  =  SqlDtr.GetValue(1).ToString();
                //		//*bhal*	txtSlipNo.Visible=false;
                //	}
                //	else
                //	{
                //		Txtstart.Value = "0";
                //		TxtEnd.Value  =  "0";
                //	//*bhal*	txtSlipNo.Visible=false;
                //	}
                //	SqlDtr.Close();
                //	Mahesh */
                //	#endregion

                //	#region Get Data from Sales Details Table regarding Invoice No.
                //	//					sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe"+
                //	//						" from Products p, sales_Details sd"+
                //	//						" where p.Prod_ID=sd.prod_id and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sd.sno" ;

                //	/* **************Start Comment By Vikas Sharma 21.04.09***************************************************
                //	 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.invoice_no,sm.invoice_date,sm.cust_id,sd.SchType,sd.FoeType,sd.SPDiscType,sd.SPDisc"+
                //		" from Products p, sales_Details sd,sales_master sm"+
                //		" where p.Prod_ID=sd.prod_id and sd.invoice_no=sm.invoice_no and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sd.sno" ;
                //		************end************************************************/
                //	/* **************Start Add By Vikas Sharma 21.04.09***/
                //	sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.invoice_no,sm.invoice_date,sm.cust_id,sd.SchType,sd.FoeType,sd.SPDiscType,sd.SPDisc,p.Prod_Code"+
                //		" from Products p, sales_Details sd,sales_master sm"+
                //		" where p.Prod_ID=sd.prod_id and sd.invoice_no=sm.invoice_no and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sd.sno" ;
                //	/* **********end***************************************/


                //	SqlDtr=obj.GetRecordSet(sql);
                //	while(SqlDtr.Read())
                //	{
                //		//ProdType[i].Enabled = true;
                //		//**ProdName[i].Enabled = true;
                //		//**PackType[i].Enabled = true;
                //		Qty[i].Enabled = true;

                //		Rate[i].Enabled = true;
                //		Amount[i].Enabled = true;
                //		AvStock[i].Enabled = true;
                //		//**ProdType[i].SelectedIndex=ProdType[i].Items.IndexOf(ProdType[i].Items.FindByValue(SqlDtr.GetValue(0).ToString ()));
                //		//ProdType[i].SelectedIndex=ProdType[i].Items.IndexOf(ProdType[i].Items.FindByValue(SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ()));

                //		//ProdType[i].Value=SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();  //Comment by vikas sharma 21.04.09
                //		ProdType[i].Value=SqlDtr.GetValue(17).ToString ()+":"+SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();  //Add by vikas sharma 21.04.09

                //		//**Type_Changed(ProdType[i] ,ProdName[i] ,PackType[i] );  
                //		//**ProdName[i].SelectedIndex=ProdName[i].Items.IndexOf(ProdName[i].Items.FindByValue(SqlDtr.GetValue(1).ToString ()));
                //		//**Prod_Changed(ProdType[i], ProdName[i] ,PackType[i] ,Rate[i]);    
                //		//**Name[i].Value=SqlDtr.GetValue(1).ToString();   
                //		//**PackType[i].SelectedIndex=PackType[i].Items.IndexOf(PackType[i].Items.FindByValue(SqlDtr.GetValue(2).ToString ()));
                //		//**Type[i].Value=SqlDtr.GetValue(2).ToString();   
                //		Qty[i].Text=SqlDtr.GetValue(3).ToString();
                //		//*************
                //		ProductType[i]=SqlDtr.GetValue(0).ToString ();
                //		ProductName[i]=SqlDtr.GetValue(1).ToString ();
                //		ProductPack[i]=SqlDtr.GetValue(2).ToString ();
                //		ProductQty[i]=SqlDtr.GetValue(3).ToString();
                //		//string pt=ProductType[i];
                //		//string pn=ProductName[i];
                //		//string pp=ProductPack[i];
                //		//string pq=ProductQty[i];
                //		//*************
                //		tempQty[i].Text   = Qty[i].Text ;
                //		tmpQty[i].Value  = SqlDtr.GetValue(3).ToString();  
                //		Rate[i].Text=SqlDtr.GetValue(4).ToString();
                //		Amount[i].Text=SqlDtr.GetValue(5).ToString();
                //		//********
                //		pid[i].Text=SqlDtr.GetValue(6).ToString();
                //		pid1[i].Text=SqlDtr.GetValue(6).ToString();
                //		/*bhal*/			scheme[i].Text=SqlDtr.GetValue(8).ToString();
                //		foe[i].Text=SqlDtr.GetValue(9).ToString();
                //		//********
                //		sql1="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr.GetValue(6).ToString()+" order by stock_date desc";
                //		dbobj.SelectQuery(sql1,ref rdr); 
                //		if(rdr.Read())
                //		{
                //			AvStock [i].Text =rdr["Closing_Stock"]+" "+SqlDtr.GetValue(7).ToString();
                //		}	
                //		else
                //		{
                //			AvStock [i].Text ="0"+" "+SqlDtr.GetValue(7).ToString();
                //		}
                //		Qty[i].ToolTip = "Actual Available Stock = "+Qty[i].Text.ToString()+" + "+ AvStock[i].Text.ToString();
                //		//rdr.Close();
                //		//string strstrste="select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'";
                //		if(SqlDtr["SPDiscType"].ToString()=="")
                //		{
                //			rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                //			if(rdr3.HasRows)
                //			{
                //				if(rdr3.Read())
                //				{
                //					SchSPType[i].Value=rdr3.GetValue(0).ToString();
                //					SchSP[i].Value=rdr3.GetValue(1).ToString();
                //				}
                //			}
                //			rdr3.Close();
                //		}
                //		else
                //		{
                //			SchSPType[i].Value=SqlDtr["SPDiscType"].ToString();
                //			SchSP[i].Value=SqlDtr["SPDisc"].ToString();
                //		}
                //		//strstrste="select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='1470' and sd.prod_id='1037' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'";
                //		if(SqlDtr["FoeType"].ToString()=="")
                //		{
                //			rdr3 = obj1.GetRecordSet("select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='"+SqlDtr["Cust_ID"].ToString()+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'");
                //			if(rdr3.HasRows)
                //			{
                //				if(rdr3.Read())
                //				{
                //					tmpFoeType[i].Value=rdr3.GetValue(0).ToString();
                //				}
                //			}
                //			rdr3.Close();
                //		}
                //		else
                //			tmpFoeType[i].Value=SqlDtr["FoeType"].ToString();
                //		//*************
                //		if(SqlDtr["SchType"].ToString()=="")
                //		{
                //			string ssssss="select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'";
                //			//rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                //			rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                //			if(rdr3.HasRows)
                //			{
                //				if(rdr3.Read())
                //				{
                //					tmpSchType[i].Value=rdr3.GetValue(0).ToString();
                //				}
                //			}
                //			rdr3.Close();
                //		}
                //		else
                //			tmpSchType[i].Value=SqlDtr["SchType"].ToString();
                //		//*************
                //		string	sql11="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit"+
                //			" from Products p, sales_Details sd"+
                //			" where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno="+i+" and sd.invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Value +"'" ;
                //		dbobj.SelectQuery(sql11,ref rdr2);

                //		if(rdr2.HasRows)
                //		{
                //			while(rdr2.Read())
                //			{
                //				//ProdType1[i].Text=rdr2.GetValue(0).ToString();
                //				ProdType1[i].Text=rdr2.GetValue(1).ToString()+":"+rdr2.GetValue(2).ToString();
                //				//**ProdName1[i].Text=rdr2.GetValue(1).ToString();
                //				//**PackType1[i].Text=rdr2.GetValue(2).ToString();
                //				Qty1[i].Text=rdr2.GetValue(3).ToString();
                //				//*************
                //				SchProductType[i]=rdr2.GetValue(0).ToString();
                //				SchProductName[i]=rdr2.GetValue(1).ToString();
                //				SchProductPack[i]=rdr2.GetValue(2).ToString();
                //				SchProductQty[i]=rdr2.GetValue(3).ToString();
                //				//**************
                //				tempSchQty[i].Text=rdr2.GetValue(3).ToString();
                //				string sql12="select top 1 Closing_Stock from Stock_Master where productid="+rdr2.GetValue(4).ToString()+" order by stock_date desc";
                //				dbobj.SelectQuery(sql12,ref rdr1); 
                //				if(rdr1.Read())
                //				{
                //					stk1[i].Text =rdr1["Closing_Stock"]+" "+rdr2.GetValue(5).ToString();
                //				}	
                //				else
                //				{
                //					stk1[i].Text ="0"+" "+rdr2.GetValue(5).ToString();
                //				}
                //				/************
                //				if(SqlDtr["SchType"].ToString()=="")
                //				{
                //					string ssssss="select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'";
                //					rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                //					if(rdr3.HasRows)
                //					{
                //						if(rdr3.Read())
                //						{
                //							tmpSchType[i].Value=rdr3.GetValue(0).ToString();
                //						}
                //					}
                //					rdr3.Close();
                //				}
                //				else
                //					tmpSchType[i].Value=SqlDtr["SchType"].ToString();
                //				******/
                //				rdr3 = obj1.GetRecordSet("select o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                //				if(rdr3.HasRows)
                //				{
                //					if(rdr3.Read())
                //					{
                //						tmpFoeType[i].Value=rdr3.GetValue(0).ToString();
                //					}
                //				}
                //				rdr3.Close();
                //				//j++;
                //			}
                //			rdr1.Close();
                //		}
                //		rdr2.Close();
                //		rdr.Close();
                //		//string mahesh=tmpSchType[i].Value;
                //		//*************
                //		i++;
                //	}

                //	SqlDtr.Close();
                //	#endregion

                //}



                //            //string strInvoiceNo = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;
                //            using (var client = new HttpClient())
                //            {
                //                client.BaseAddress = new Uri("http://localhost:55251/api/roles");
                //                client.DefaultRequestHeaders.Accept.Clear();
                //                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //                //HttpResponseMessage response = await client.GetAsync("http://localhost:55251/api/roles/GetDataSelectedSalesInvoice/");
                //                var response = client.GetAsync("http://localhost:55251/api/sales/GetDataSelectedSalesInvoice?id=" + dropInvoiceNo.SelectedItem.Value).Result;
                //                string res = "";

                //                if (response.IsSuccessStatusCode)
                //                {
                //                    using (HttpContent content = response.Content)
                //                    {
                //                        // ... Read the string.
                //                        Task<string> result = content.ReadAsStringAsync();
                //                        res = result.Result;

                //                        sales = JsonConvert.DeserializeObject<SalesModel>(res);
                //                    }
                //                }
                //            }
                //            txtGrandTotal.Text = GenUtil.strNumericFormat(sales.Grand_Total);
                //            //txt

                //            CreateLogFiles.ErrorLog("Form:Sales Invoisee.aspx,Method:dropInvoiceNo_SelectedIndexChanged " +" Sales invoice is viewed for invoice no: "+dropInvoiceNo.SelectedItem.Value.ToString()+" userid "+"   "+"   "+uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoise is update for invoise no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
        }

        public void DropInvoiceNo_Selected()
        {
            SalesModel sales = new SalesModel();

            string str = sales.Invoice_Date;
            TextSelect.Text = dropInvoiceNo.SelectedItem.Value.ToString();
            try
            {
                if (TextSelect.Text == "Select")
                {
                    MessageBox.Show("Please Select Invoice No");
                }
                else
                {

                    Clear();

                    using (var client = new HttpClient())
                    {
                        //client.BaseAddress = new Uri("http://localhost:55251/api/roles");
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        //HttpResponseMessage response = await client.GetAsync("http://localhost:55251/api/roles/GetDataSelectedSalesInvoice/");
                        //var response = client.GetAsync("http://localhost:55251/api/sales/GetDataSelectedSalesInvoice?id=" + dropInvoiceNo.SelectedItem.Value).Result;
                        var response = client.GetAsync("/api/sales/GetDataSelectedSalesInvoice?id=" + dropInvoiceNo.SelectedItem.Value).Result;
                        string res = "";

                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                //var Res = client.GetAsync("api/sales/GetFromDateToDateData").Result;
                                //if (Res.IsSuccessStatusCode)
                                //{
                                //    var result = Res.Content.ReadAsStringAsync().Result;
                                //    resFromDateToDate = JsonConvert.DeserializeObject<List<string>>(result);
                                //}
                                var result = response.Content.ReadAsStringAsync().Result;
                                sales = JsonConvert.DeserializeObject<SalesModel>(result);

                                // ... Read the string.
                                //Task<string> result = content.ReadAsStringAsync();
                                //res = result.Result;

                                //sales = JsonConvert.DeserializeObject<SalesModel>(res);
                            }
                        }
                    }

                    HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };

                    TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                    TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
                    TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
                    TextBox[] AvStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };
                    TextBox[] tempQty = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12 };
                    TextBox[] tempSchQty = { txtTempSchQty1, txtTempSchQty2, txtTempSchQty3, txtTempSchQty4, txtTempSchQty5, txtTempSchQty6, txtTempSchQty7, txtTempSchQty8, txtTempSchQty9, txtTempSchQty10, txtTempSchQty11, txtTempSchQty12 };
                    HtmlInputHidden[] tmpQty = { tmpQty1, tmpQty2, tmpQty3, tmpQty4, tmpQty5, tmpQty6, tmpQty7, tmpQty8, tmpQty9, tmpQty10, tmpQty11, tmpQty12 };
                    HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
                    HtmlInputHidden[] tmpFoeType = { tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12 };
                    HtmlInputHidden[] SchSPType = { tmpSecSPType1, tmpSecSPType2, tmpSecSPType3, tmpSecSPType4, tmpSecSPType5, tmpSecSPType6, tmpSecSPType7, tmpSecSPType8, tmpSecSPType9, tmpSecSPType10, tmpSecSPType11, tmpSecSPType12 };
                    HtmlInputHidden[] SchSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
                    //****
                    TextBox[] pid = { txtpname1, txtpname2, txtpname3, txtpname4, txtpname5, txtpname6, txtpname7, txtpname8, txtpname9, txtpname10, txtpname11, txtpname12 };
                    TextBox[] pid1 = { txtmwid1, txtmwid2, txtmwid3, txtmwid4, txtmwid5, txtmwid6, txtmwid7, txtmwid8, txtmwid9, txtmwid10, txtmwid11, txtmwid12 };
                    TextBox[] scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
                    //****************************
                    TextBox[] foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
                    //***************************
                    //*************************
                    TextBox[] ProdType1 = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                    //**TextBox[] ProdName1={txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12}; 
                    //**TextBox[] PackType1={txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12}; 
                    TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                    TextBox[] stk1 = { txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12 };
                    //**************************

                    //**********end*************
                    InventoryClass obj = new InventoryClass();
                    InventoryClass obj1 = new InventoryClass();
                    //SqlDataReader SqlDtr;
                    string sql, sql1;
                    SqlDataReader rdr = null, rdr1 = null, rdr2 = null, rdr3 = null;
                    int i = 0;
                    FlagPrint = false;
                    Button1.CausesValidation = true;

                    #region Get Data from Sales Master Table regarding Invoice No.
                    //if (FromDate != "")
                    //    //sql="select * from Sales_Master where Invoice_No='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value +"'" ;
                    //    sql = "select * from Sales_Master sm,employee e where Under_SalesMan=emp_id and Invoice_No='" + int.Parse(FromDate) + ToDate + dropInvoiceNo.SelectedItem.Value + "'";
                    //else
                    //{
                    //    MessageBox.Show("Please Fill The Organisation Form First");
                    //    return;
                    //}
                    //SqlDtr = obj.GetRecordSet(sql);
                    if (sales != null)
                    {
                        //Invoice_Date = SqlDtr.GetValue(1).ToString();
                        //string strDate = SqlDtr.GetValue(1).ToString().Trim();
                        //int pos = strDate.IndexOf(" ");
                        //if (pos != -1)
                        //{
                        //    strDate = strDate.Substring(0, pos);
                        //}
                        //else
                        //{
                        //    strDate = "";
                        //}
                        lblInvoiceDate.Text = sales.Invoice_Date;
                        tempInvoiceDate.Value = sales.Invoice_Date;
                        DropSalesType.SelectedIndex = (DropSalesType.Items.IndexOf((DropSalesType.Items.FindByValue(sales.Sales_Type))));
                        DropUnderSalesMan.SelectedIndex = (DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(sales.Under_SalesMan))));
                        if (getCustomerVehicles(sales.Cust_ID.ToString()) == true)
                        {
                            DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(sales.Vehicle_No));
                        }
                        else
                        {
                            txtVehicleNo.Text = sales.Vehicle_No;
                        }
                        //txtGrandTotal.Text=SqlDtr.GetValue(6).ToString();
                        //txtGrandTotal.Text = GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()); 
                        //txtDisc.Text = sales.Discount.ToString();
                        txtDisc.Text = GenUtil.strNumericFormat(sales.Discount.ToString());
                        DropDiscType.SelectedIndex = DropDiscType.Items.IndexOf((DropDiscType.Items.FindByValue(sales.Discount_Type.ToString())));

                        txtNetAmount.Text = GenUtil.strNumericFormat(sales.Net_Amount.ToString());
                        //Add by vikas 14.07.09
                        tempNetAmount.Value = GenUtil.strNumericFormat(sales.Net_Amount.ToString());     //Add by vikas 14.07.09

                        NetAmount = GenUtil.strNumericFormat(sales.Net_Amount.ToString());
                        txtPromoScheme.Text = sales.Promo_Scheme;
                        txtRemark.Text = sales.Remark;
                        lblEntryBy.Text = sales.Entry_By;
                        lblEntryTime.Text = sales.Entry_Time.ToString();
                        txtSecondrySpDisc.Text = sales.SecSPDisc.ToString();
                        //******************
                        if (sales.Discount_Type.ToString() == "Per")
                        {
                            txtDiscount.Text = System.Convert.ToString((double.Parse(sales.Grand_Total.ToString()) - double.Parse(sales.Scheme_Discount.ToString())) * double.Parse(sales.Discount.ToString()) / 100);
                            txtDiscount.Text = System.Convert.ToString(Math.Round(double.Parse(txtDiscount.Text), 2));
                        }
                        else
                        {
                            double Discount = double.Parse(GenUtil.strNumericFormat(sales.Discount.ToString())) * double.Parse(GenUtil.strNumericFormat(sales.Total_Qty_Ltr.ToString()));
                            txtDiscount.Text = GenUtil.strNumericFormat(sales.Discount.ToString());
                        }


                        if (sales.Cash_Disc_Type.ToString() == "Per")
                        {
                            double tot = 0;
                            if (txtDiscount.Text != "")
                                tot = double.Parse(sales.Grand_Total.ToString()) - (double.Parse(sales.Scheme_Discount.ToString()) + double.Parse(sales.FOE_Discount.ToString()) + double.Parse(txtDiscount.Text));
                            else
                                tot = double.Parse(sales.Grand_Total.ToString()) - (double.Parse(sales.Scheme_Discount.ToString()) + double.Parse(sales.FOE_Discount.ToString()));
                            txtCashDiscount.Text = System.Convert.ToString(tot * double.Parse(sales.Cash_Discount.ToString()) / 100);
                            txtCashDiscount.Text = System.Convert.ToString(Math.Round(double.Parse(txtCashDiscount.Text), 2));
                            tempcashdis.Value = txtCashDiscount.Text;
                        }
                        else
                        {
                            double cashDiscount = double.Parse(GenUtil.strNumericFormat(sales.Cash_Discount.ToString())) * double.Parse(GenUtil.strNumericFormat(sales.Total_Qty_Ltr.ToString()));
                            txtCashDiscount.Text = GenUtil.strNumericFormat(cashDiscount.ToString());
                        }

                        txtCashDisc.Text = sales.Cash_Discount.ToString();
                        txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString());
                        DropCashDiscType.SelectedIndex = DropCashDiscType.Items.IndexOf((DropCashDiscType.Items.FindByValue(sales.Cash_Disc_Type.ToString())));
                        txtVAT.Text = sales.IGST_Amount.ToString();
                        txtschemetotal.Text = sales.Scheme_Discount.ToString();
                        txtfleetoediscount.Text = sales.FOE_Discount.ToString();
                        dropfleetoediscount.SelectedIndex = dropfleetoediscount.Items.IndexOf((dropfleetoediscount.Items.FindByValue(sales.FOE_Discounttype.ToString())));
                        txtfleetoediscountRs.Text = sales.FOE_Discountrs.ToString();
                        txtliter.Text = sales.Total_Qty_Ltr.ToString();
                        Textcgst.Text = sales.CGST_Amount.ToString();
                        Textsgst.Text = sales.SGST_Amount.ToString();
                        if (sales.ChallanNo == null)
                        {
                            txtChallanNo.Text = "";
                        }
                        else if (sales.ChallanNo.ToString() == "0")
                        {
                            txtChallanNo.Text = "";
                        }
                        else
                        {
                            txtChallanNo.Text = sales.ChallanNo.ToString();
                        }

                        if (GenUtil.trimDate(sales.ChallanDate.ToString()) == "1/1/1900")
                            txtChallanDate.Text = "";
                        else
                            txtChallanDate.Text = GenUtil.str2DDMMYYYY(GenUtil.trimDate(sales.ChallanDate.ToString()));
                        if (txtVAT.Text.Trim() == "0")
                        {
                            Yes.Checked = false;
                            No.Checked = true;
                        }
                        else
                        {
                            No.Checked = false;
                            Yes.Checked = true;
                        }
                    }
                    //SqlDtr.Close();
                    #endregion

                    #region Get Customer name and place regarding Customer ID
                    //*****add Cust_type
                    //coment by vikas 7.11.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID from Customer as c, sales_master as s where c.Cust_ID= s.Cust_ID and s.Invoice_No='"+FromDate+ToDate+dropInvoiceNo.SelectedValue +"'";
                    //sql = "select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, sales_master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Invoice_No='" + FromDate + ToDate + dropInvoiceNo.SelectedValue + "'";
                    //SqlDtr = obj.GetRecordSet(sql);
                    if (sales != null)
                    {
                        if (sales.Cust_Name != null)
                        {
                            texthidden1.Value = sales.Cust_Name;
                            text1.Value = sales.Cust_Name;
                        }

                        //Cache["CustName"]=SqlDtr.GetValue(0).ToString();
                        CustID = sales.Cust_ID.ToString();

                        lblPlace.Value = sales.Place.ToString();
                        //DateTime duedate;
                        //string duedatestr = (sales.duedate.ToShortDateString());
                        lblDueDate.Value = GenUtil.str2DDMMYYYY(sales.DueDate.ToShortDateString());
                        lblCurrBalance.Value = GenUtil.strNumericFormat(sales.Current_Balance.ToString());
                        TxtCrLimit.Value = sales.Credit_Limit.ToString();
                        lblCreditLimit.Value = sales.Credit_Limit.ToString();
                        if (sales.CustomerType != null)
                        {
                            txtcusttype.Text = sales.CustomerType.ToString();
                        }

                        /***Add by vikas 10.11.2012 *********************/
                        if (sales.Group_Name.ToString() != null && sales.Group_Name.ToString() != "")
                            tempCustGroup.Value = sales.Group_Name.ToString();
                        /*********************************************/
                    }

                    if (sales != null)
                    {
                        lblCurrBalance.Value = GenUtil.strNumericFormat(sales.Balance.ToString()) + " " + sales.BalanceType.ToString();
                    }

                    #endregion

                    #region Get Data from Sales Details Table regarding Invoice No.
                    //					sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe"+
                    //						" from Products p, sales_Details sd"+
                    //						" where p.Prod_ID=sd.prod_id and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sd.sno" ;

                    /* **************Start Comment By Vikas Sharma 21.04.09***************************************************
					 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.invoice_no,sm.invoice_date,sm.cust_id,sd.SchType,sd.FoeType,sd.SPDiscType,sd.SPDisc"+
						" from Products p, sales_Details sd,sales_master sm"+
						" where p.Prod_ID=sd.prod_id and sd.invoice_no=sm.invoice_no and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='"+FromDate+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sd.sno" ;
						************end************************************************/
                    /* **************Start Add By Vikas Sharma 21.04.09***/
                    //sql = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.invoice_no,sm.invoice_date,sm.cust_id,sd.SchType,sd.FoeType,sd.SPDiscType,sd.SPDisc,p.Prod_Code" +
                    //    " from Products p, sales_Details sd,sales_master sm" +
                    //    " where p.Prod_ID=sd.prod_id and sd.invoice_no=sm.invoice_no and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Value + "' order by sd.sno";
                    /* **********end***************************************/


                    //SqlDtr = obj.GetRecordSet(sql);
                    //int countProduct = 0;
                    while (i < sales.ProductName.Count)
                    {
                        //ProdType[i].Enabled = true;
                        //**ProdName[i].Enabled = true;
                        //**PackType[i].Enabled = true;
                        Qty[i].Enabled = true;

                        Rate[i].Enabled = true;
                        Amount[i].Enabled = true;
                        AvStock[i].Enabled = true;
                        //**ProdType[i].SelectedIndex=ProdType[i].Items.IndexOf(ProdType[i].Items.FindByValue(SqlDtr.GetValue(0).ToString ()));
                        //ProdType[i].SelectedIndex=ProdType[i].Items.IndexOf(ProdType[i].Items.FindByValue(SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ()));

                        //ProdType[i].Value=SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();  //Comment by vikas sharma 21.04.09
                        ProdType[i].Value = sales.ProdType[i].ToString();// SqlDtr.GetValue(17).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString();  //Add by vikas sharma 21.04.09

                        //**Type_Changed(ProdType[i] ,ProdName[i] ,PackType[i] );  
                        //**ProdName[i].SelectedIndex=ProdName[i].Items.IndexOf(ProdName[i].Items.FindByValue(SqlDtr.GetValue(1).ToString ()));
                        //**Prod_Changed(ProdType[i], ProdName[i] ,PackType[i] ,Rate[i]);    
                        //**Name[i].Value=SqlDtr.GetValue(1).ToString();   
                        //**PackType[i].SelectedIndex=PackType[i].Items.IndexOf(PackType[i].Items.FindByValue(SqlDtr.GetValue(2).ToString ()));
                        //**Type[i].Value=SqlDtr.GetValue(2).ToString();   
                        Qty[i].Text = sales.ProductQty[i].ToString();
                        //*************
                        ProductType[i] = sales.ProductType[i].ToString();
                        ProductName[i] = sales.ProductName[i].ToString();
                        ProductPack[i] = sales.ProductPack[i].ToString();
                        ProductQty[i] = sales.ProductQty[i].ToString();
                        //string pt=ProductType[i];
                        //string pn=ProductName[i];
                        //string pp=ProductPack[i];
                        //string pq=ProductQty[i];
                        //*************
                        tempQty[i].Text = Qty[i].Text;
                        tmpQty[i].Value = sales.ProductQty[i].ToString();
                        Rate[i].Text = sales.Rate[i].ToString();
                        Amount[i].Text = sales.Amount[i].ToString();
                        //********
                        pid[i].Text = sales.PID[i].ToString();
                        pid1[i].Text = sales.PID1[i].ToString();
                        /*bhal*/
                        scheme[i].Text = sales.scheme[i].ToString();

                        foe[i].Text = sales.Details_foe[i].ToString();

                        AvStock[i].Text = sales.Av_Stock[i].ToString();

                        Qty[i].ToolTip = "Actual Available Stock = " + Qty[i].Text.ToString() + " + " + AvStock[i].Text.ToString();


                        //rdr.Close();
                        //string strstrste="select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'";
                        if (sales.SPDiscType != null)
                        {
                            if (sales.SPDiscType.ToString() == "")
                            {
                                //rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                                //if (rdr3.HasRows)
                                //{
                                //    if (rdr3.Read())
                                //    {
                                SchSPType[i].Value = sales.SchSPType[i].ToString();
                                SchSP[i].Value = sales.SchSP[i].ToString();
                                //    }
                                //}
                                //rdr3.Close();
                            }
                            else
                            {
                                SchSPType[i].Value = sales.SchSPType[i].ToString();
                                SchSP[i].Value = sales.SchSP[i].ToString();
                            }
                        }

                        //strstrste="select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='1470' and sd.prod_id='1037' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'";
                        if (sales.FoeType != null)
                        {
                            if (sales.FoeType.ToString() == "")
                            {
                                //rdr3 = obj1.GetRecordSet("select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='" + SqlDtr["Cust_ID"].ToString() + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "'");
                                //if (rdr3.HasRows)
                                //{
                                //    if (rdr3.Read())
                                //    {
                                tmpFoeType[i].Value = sales.tmpSchType[i].ToString();
                                //    }
                                //}
                                //rdr3.Close();
                            }
                            else
                            {
                                tmpFoeType[i].Value = sales.FoeType.ToString();
                            }
                        }

                        //*************
                        if (sales.SchType != null)
                        {
                            if (sales.SchType.ToString() == "")
                            {
                                //string ssssss = "select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'";
                                ////rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                                //rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                                //if (rdr3.HasRows)
                                //{
                                //    if (rdr3.Read())
                                //    {
                                tmpSchType[i].Value = sales.tmpSchType[i].ToString();
                                //    }
                                //}
                                //rdr3.Close();
                            }
                            else
                            {
                                tmpSchType[i].Value = sales.SchType.ToString();
                            }
                        }
                        //*************
                        //string sql11 = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit" +
                        //    " from Products p, sales_Details sd" +
                        //    " where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno=" + i + " and sd.invoice_no='" + FromDate + ToDate + dropInvoiceNo.SelectedItem.Value + "'";
                        //dbobj.SelectQuery(sql11, ref rdr2);

                        if (sales != null)
                        {
                            //while (i < sales.ProductName.Count)
                            //{
                            if (sales.ProdType1.Count != 0)
                                ProdType1[i].Text = sales.ProdType1.ToString();
                            //**ProdName1[i].Text=rdr2.GetValue(1).ToString();
                            //**PackType1[i].Text=rdr2.GetValue(2).ToString();
                            if (sales.SalesQty1.Count != 0)
                                Qty1[i].Text = sales.SalesQty1.ToString();
                            //*************
                            if (sales.SchProductType.Count != 0)
                                SchProductType[i] = sales.SchProductType[i].ToString();

                            if (sales.SchProductName.Count != 0)
                                SchProductName[i] = sales.SchProductName[i].ToString();

                            if (sales.SchProductPack.Count != 0)
                                SchProductPack[i] = sales.SchProductPack[i].ToString();

                            if (sales.SchProductQty.Count != 0)
                                SchProductQty[i] = sales.SchProductQty[i].ToString();
                            //**************
                            if (sales.tempSchQty.Count != 0)
                                tempSchQty[i].Text = sales.tempSchQty[i].ToString();


                            //string sql12 = "select top 1 Closing_Stock from Stock_Master where productid=" + rdr2.GetValue(4).ToString() + " order by stock_date desc";
                            //dbobj.SelectQuery(sql12, ref rdr1);
                            //if (rdr1.Read())
                            //{
                            if (sales.stk1.Count != 0)
                                stk1[i].Text = sales.stk1[i].ToString();
                            //}
                            //else
                            //{
                            //    stk1[i].Text = "0" + " " + rdr2.GetValue(5).ToString();
                            //}
                            /************
                            if(SqlDtr["SchType"].ToString()=="")
                            {
                                string ssssss="select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'";
                                rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                                if(rdr3.HasRows)
                                {
                                    if(rdr3.Read())
                                    {
                                        tmpSchType[i].Value=rdr3.GetValue(0).ToString();
                                    }
                                }
                                rdr3.Close();
                            }
                            else
                                tmpSchType[i].Value=SqlDtr["SchType"].ToString();
                            ******/
                            //rdr3 = obj1.GetRecordSet("select o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + rdr2["Prod_ID"].ToString() + "'");
                            //if (rdr3.HasRows)
                            //{
                            //    if (rdr3.Read())
                            //    {
                            if (sales.tmpFoeType.Count != 0)
                                tmpFoeType[i].Value = sales.tmpFoeType[i].ToString();
                            //            }
                            //        }
                            //        rdr3.Close();
                            //        //j++;
                            //    }
                            //    rdr1.Close();
                            //}
                            //rdr2.Close();
                            //rdr.Close();
                            //string mahesh=tmpSchType[i].Value;
                            //*************
                            i++;
                            //}

                            //SqlDtr.Close();
                            #endregion

                        }


                    }
                }
                //string strInvoiceNo = FromDate + ToDate + dropInvoiceNo.SelectedItem.Value;

                txtGrandTotal.Text = GenUtil.strNumericFormat(sales.Grand_Total);
                //txt

                CreateLogFiles.ErrorLog("Form:Sales Invoisee.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoice is viewed for invoice no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " userid " + "   " + "   " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoise is update for invoise no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
        }
        /// <summary>
        /// This method are not used in this project.
        /// </summary>
        /// <param name="ddProd"></param>
        /// <param name="ddPack"></param>
        /// <param name="txtPurRate"></param>
        public void Pack_Changed(DropDownList ddProd, DropDownList ddPack, TextBox txtPurRate)
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                #region Fetch Sales Rate Regarding Product Name		
                sql = "select top 1 Pur_Rate from Price_Updation where Prod_ID=(select  Prod_ID from Products where Prod_Name='" + ddProd.SelectedItem.Value + "' and Pack_Type='" + ddPack.SelectedItem.Value + "') order by eff_date desc";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    txtPurRate.Text = SqlDtr.GetValue(0).ToString();
                }
                SqlDtr.Close();
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : Pack_Changed  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method fatch the required customer info according to select customer name from dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropCustName_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // This displays the customer information afterb selecttion of a customer, as well display the vehicle nos of a customer if entered by the Customer vehicle entry form into drop down if present else display the text field to enter the vehicle no.
            try
            {
                /* Mahesh
				if(DropCustName.SelectedIndex == 0)
				{
					MessageBox.Show("Please Select Customer Name"); 
					return;
				}
				string cust_id = "";
				lblPlace.Value = "";
				lblDueDate.Value = "";
				TxtCrLimit.Value = "";
				lblCreditLimit.Value = "";
				lblCurrBalance.Value = "";
				Txtstart.Value = "";
				TxtEnd.Value = "";
				FlagPrint=false;
				Button1.CausesValidation=true;
				SqlDataReader SqlDtr = null;
				DateTime duedate = DateTime.Now ;
				string duedatestr = "";
				dbobj.SelectQuery("Select City, Cr_Days, Curr_Credit, Cust_ID,Cust_Type from Customer where Cust_Name='"+DropCustName.SelectedItem.Text.Trim()+"'",ref SqlDtr); 
				if(SqlDtr.Read())
				{
					lblPlace.Value  = SqlDtr.GetValue(0).ToString();
					duedate=DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr["CR_Days"]));
					duedatestr=(duedate.ToShortDateString());
					lblDueDate.Value  = GenUtil.str2DDMMYYYY(duedatestr);  
					TxtCrLimit.Value  = GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString());  
					lblCreditLimit.Value  = GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString());
					cust_id = SqlDtr.GetValue(3).ToString();				
					txtcusttype.Text=SqlDtr.GetValue(4).ToString();
				}
				SqlDtr.Close();
				dbobj.SelectQuery("Select top 1 Balance,BalanceType from CustomerLedgerTable where CustID="+cust_id+" order by EntryDate Desc",ref SqlDtr); 
				if(SqlDtr.Read())
				{
					lblCurrBalance.Value = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString())+" "+SqlDtr.GetValue(1).ToString();  
				}
				SqlDtr.Close();
				
				dbobj.SelectQuery("Select Start_no, End_No from Slip where Cust_ID = "+cust_id,ref SqlDtr);
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))    
						Txtstart.Value = SqlDtr.GetValue(0).ToString();
						else
						  Txtstart.Value = "0";
						if(!SqlDtr.GetValue(1).ToString().Trim().Equals(""))    
						TxtEnd.Value = SqlDtr.GetValue(1).ToString();
						else
							TxtEnd.Value = "0";
					}
				}
				else
				{
					Txtstart.Value = "0";
					TxtEnd.Value = "0";
				}
				SqlDtr.Close();
				getCustomerVehicles(cust_id); 
				Mahesh */
                //******************bhal *******************

                /*	InventoryClass  obj=new InventoryClass ();
					SqlDataReader SqlDtrfoe;
					string sql;
					//string str="";
					//SqlDataReader rdr=null; 
					sql="select discount,discount_type from fleetoe_discount where cust_id='"+cust_id+"' and cast(floor(cast(datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
					SqlDtrfoe=obj.GetRecordSet(sql);
					while(SqlDtrfoe.Read())
					{
						 txtfleetoediscount.Text=SqlDtrfoe.GetValue(0).ToString();
						dropfleetoediscount.SelectedIndex=(dropfleetoediscount.Items.IndexOf((dropfleetoediscount.Items.FindByValue(SqlDtrfoe.GetValue(1).ToString()))));
				
					}
					SqlDtrfoe.Close();
					*/
            }
            //*******************************************
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:DropCustName_SelectedIndexChanged().  EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
        }


        /// <summary>
        /// This method fatch the customer vehicle info. according to passing value.
        /// </summary>
        /// <param name="cust_id"></param>
        /// <returns></returns>
        public bool getCustomerVehicles(string cust_id)
        {
            try
            {

                //SqlDataReader SqlDtr = null;
                //dbobj.SelectQuery("Select * from Customer_Vehicles where Cust_ID =" + cust_id, ref SqlDtr);
                List<string> vehNos = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("/api/sales/GetCustomerVehicles?cust_id=" + cust_id).Result;
                    //string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            //var Res = client.GetAsync("api/sales/GetFromDateToDateData").Result;
                            //if (Res.IsSuccessStatusCode)
                            //{
                            //    var result = Res.Content.ReadAsStringAsync().Result;
                            //    resFromDateToDate = JsonConvert.DeserializeObject<List<string>>(result);
                            //}
                            var result = response.Content.ReadAsStringAsync().Result;
                            vehNos = JsonConvert.DeserializeObject<List<string>>(result);

                            // ... Read the string.
                            //Task<string> result = content.ReadAsStringAsync();
                            //res = result.Result;

                            //sales = JsonConvert.DeserializeObject<SalesModel>(res);
                        }
                    }
                }

                if (vehNos != null && vehNos.Count > 1)
                {
                    DropVehicleNo.Visible = true;
                    txtVehicleNo.Visible = false;
                    RequiredFieldValidator1.Visible = false;
                    RequiredFieldValidator3.Visible = true;
                    DropVehicleNo.Items.Clear();
                    DropVehicleNo.Items.Add("Select");

                    foreach (var number in vehNos)
                        DropVehicleNo.Items.Add(number);
                    //while (SqlDtr.Read())
                    //{
                    //    if (!SqlDtr.GetValue(2).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(2).ToString());
                    //    if (!SqlDtr.GetValue(3).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(3).ToString());
                    //    if (!SqlDtr.GetValue(4).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(4).ToString());
                    //    if (!SqlDtr.GetValue(5).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(5).ToString());
                    //    if (!SqlDtr.GetValue(6).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(6).ToString());
                    //    if (!SqlDtr.GetValue(7).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(7).ToString());
                    //    if (!SqlDtr.GetValue(8).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(8).ToString());
                    //    if (!SqlDtr.GetValue(9).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(9).ToString());
                    //    if (!SqlDtr.GetValue(10).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(10).ToString());
                    //    if (!SqlDtr.GetValue(11).ToString().Trim().Equals(""))
                    //        DropVehicleNo.Items.Add(SqlDtr.GetValue(11).ToString());
                    //}
                    //SqlDtr.Close();
                    return true;
                }
                else
                {
                    DropVehicleNo.Visible = false;
                    txtVehicleNo.Visible = true;
                    RequiredFieldValidator1.Visible = true;
                    RequiredFieldValidator3.Visible = false;
                    txtVehicleNo.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:getCustomerVehicles().  EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
            return true;
        }

        /// <summary>
        /// This method fatch the customer vehicle info. according to passing value.
        /// </summary>
        /// <param name="cust_id"></param>
        /// <returns></returns>
        public bool getCustomerVehiclesNos(string cust_id)
        {
            //VehicleNo vehNos = new VehicleNo();
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:55251/api/roles");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    var response = client.GetAsync("http://localhost:55251/api/sales/GetCustomerVehicles?id=" + cust_id).Result;
            //    string res = "";

            //    if (response.IsSuccessStatusCode)
            //    {
            //        using (HttpContent content = response.Content)
            //        {
            //            // ... Read the string.
            //            Task<string> result = content.ReadAsStringAsync();
            //            res = result.Result;

            //            vehNos = JsonConvert.DeserializeObject<VehicleNo>(res);
            //        }
            //    }
            //}

            //try
            //{
            //    SqlDataReader SqlDtr = null;
            //    dbobj.SelectQuery("Select * from Customer_Vehicles where Cust_ID =" + cust_id, ref SqlDtr);
            //    if (vehNos.VehicleNos == null)
            //    {
            //        DropVehicleNo.Visible = false;
            //        txtVehicleNo.Visible = true;
            //        RequiredFieldValidator1.Visible = true;
            //        RequiredFieldValidator3.Visible = false;
            //        txtVehicleNo.Text = "";
            //        return false;
            //    }
            //    else if (vehNos.VehicleNos.Count > 0)
            //    {
            //        DropVehicleNo.Visible = true;
            //        txtVehicleNo.Visible = false;
            //        RequiredFieldValidator1.Visible = false;
            //        RequiredFieldValidator3.Visible = true;
            //        DropVehicleNo.Items.Clear();
            //        DropVehicleNo.Items.Add("Select");
            //        int i = 0;
            //        while (i < vehNos.VehicleNos.Count)
            //        {
            //            DropVehicleNo.Items.Add(vehNos.VehicleNos[i].ToString());
            //            i++;
            //        }
            //        SqlDtr.Close();
            //        return true;
            //    }
            //    else
            //    {
            //        DropVehicleNo.Visible = false;
            //        txtVehicleNo.Visible = true;
            //        RequiredFieldValidator1.Visible = true;
            //        RequiredFieldValidator3.Visible = false;
            //        txtVehicleNo.Text = "";
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:getCustomerVehicles().  EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            //}
            return true;
        }

        /// <summary>
        /// This method clears the form.
        /// </summary>
        public void Clear()
        {
            //jk=0;
            txtSecondrySpDisc.Text = "";
            txtChallanDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            tempcashdis.Value = "";
            NetAmount = "0";
            Invoice_Date = "";
            CustID = "";
            txtCashDiscount.Text = "";
            txtDiscount.Text = "";
            //Cache["CustName"]="";
            tempInvoiceDate.Value = "";
            tempDelinfo.Value = "";
            txtChallanNo.Text = "";
            //15.07.09 vikas DropSalesType.SelectedIndex=0;
            DropSalesType.SelectedIndex = 1;
            //Mahesh txtSlipNo.Text="";
            TxtCrLimit.Value = "";
            DropUnderSalesMan.SelectedIndex = 0;
            //DropCustName.SelectedIndex=0;
            text1.Value = "Select";
            lblPlace.Value = "";
            lblDueDate.Value = "";
            lblCurrBalance.Value = "";
            txtVehicleNo.Text = "";
            DropVehicleNo.Visible = false;
            txtVehicleNo.Visible = true;
            txtPromoScheme.Text = "";
            txtRemark.Text = "";
            txtGrandTotal.Text = "";
            lblCreditLimit.Value = "";
            txtDisc.Text = "";
            txtNetAmount.Text = "";
            DropDiscType.SelectedIndex = 0;
            txtVAT.Text = "";
            Textcgst.Text = "";
            Textsgst.Text = "";
            txtCashDisc.Text = "";
            DropCashDiscType.SelectedIndex = 0;
            Yes.Checked = true;
            No.Checked = false;
            txtfleetoediscount.Text = "";
            dropfleetoediscount.SelectedIndex = 0;
            txtfleetoediscountRs.Text = "";
            txtliter.Text = "";
            #region Clear All Products Details

            /*
				DropType1.SelectedIndex=0;
				DropType2.SelectedIndex=0;
				DropType3.SelectedIndex=0;
				DropType4.SelectedIndex=0;
				DropType5.SelectedIndex=0;
				DropType6.SelectedIndex=0;
				DropType7.SelectedIndex=0;
				DropType8.SelectedIndex=0;
				DropType9.SelectedIndex=0;
				DropType10.SelectedIndex=0;
				DropType11.SelectedIndex=0;
				DropType12.SelectedIndex=0;
				*/
            /*DropType1.Value="Type";
				DropType2.Value="Type";
				DropType3.Value="Type";
				DropType4.Value="Type";
				DropType5.Value="Type";
				DropType6.Value="Type";
				DropType7.Value="Type";
				DropType8.Value="Type";
				DropType9.Value="Type";
				DropType10.Value="Type";
				DropType11.Value="Type";
				DropType12.Value="Type";*/
            /*DropProd1.SelectedIndex=0;
			DropProd2.SelectedIndex=0;
			DropProd3.SelectedIndex=0;
			DropProd4.SelectedIndex=0;
			DropProd5.SelectedIndex=0;
			DropProd6.SelectedIndex=0;
			DropProd7.SelectedIndex=0;
			DropProd8.SelectedIndex=0;
			DropProd9.SelectedIndex=0;
			DropProd10.SelectedIndex=0;
			DropProd11.SelectedIndex=0;
			DropProd12.SelectedIndex=0;
			DropPack1.Items.Clear();  
			DropPack2.Items.Clear();
			DropPack3.Items.Clear();
			DropPack4.Items.Clear();
			DropPack5.Items.Clear();
			DropPack6.Items.Clear();
			DropPack7.Items.Clear();
			DropPack8.Items.Clear();
			DropPack9.Items.Clear();
			DropPack10.Items.Clear();
			DropPack11.Items.Clear();
			DropPack12.Items.Clear();
			DropPack1.SelectedIndex=0;
			DropPack2.SelectedIndex=0;
			DropPack3.SelectedIndex=0;
			DropPack4.SelectedIndex=0;
			DropPack5.SelectedIndex=0;
			DropPack6.SelectedIndex=0;
			DropPack7.SelectedIndex=0;
			DropPack8.SelectedIndex=0;
			DropPack9.SelectedIndex=0;
			DropPack10.SelectedIndex=0;
			DropPack11.SelectedIndex=0;
			DropPack12.SelectedIndex=0;*/
            /*txtQty1.Text="";
			txtQty2.Text="";
			txtQty3.Text="";
			txtQty4.Text="";
			txtQty5.Text="";
			txtQty6.Text="";
			txtQty7.Text="";
			txtQty8.Text="";
			txtQty9.Text="";
			txtQty10.Text="";
			txtQty11.Text="";
			txtQty12.Text="";
			txtRate1.Text="";
			txtRate2.Text="";
			txtRate3.Text="";
			txtRate4.Text="";
			txtRate5.Text="";
			txtRate6.Text="";
			txtRate7.Text="";
			txtRate8.Text="";
			txtRate9.Text="";
			txtRate10.Text="";
			txtRate11.Text="";
			txtRate12.Text="";
			txtAmount1.Text=""; 
			txtAmount1.Text=""; 
			txtAmount2.Text=""; 
			txtAmount3.Text=""; 
			txtAmount4.Text=""; 
			txtAmount5.Text=""; 
			txtAmount6.Text=""; 
			txtAmount7.Text=""; 
			txtAmount8.Text="";
			txtAmount9.Text="";
			txtAmount10.Text="";
			txtAmount11.Text="";
			txtAmount12.Text="";
			txtsch1.Text="";
			txtsch2.Text="";
			txtsch3.Text="";
			txtsch4.Text="";
			txtsch5.Text="";
			txtsch6.Text="";
			txtsch7.Text="";
			txtsch8.Text="";
			txtsch9.Text="";
			txtsch10.Text="";
			txtsch11.Text="";
			txtsch12.Text="";
			//********
			txtfoe1.Text="";
			txtfoe2.Text="";
			txtfoe3.Text="";
			txtfoe4.Text="";
			txtfoe5.Text="";
			txtfoe6.Text="";
			txtfoe7.Text="";
			txtfoe8.Text="";
			txtfoe9.Text="";
			txtfoe10.Text="";
			txtfoe11.Text="";
			txtfoe12.Text="";*/

            //*********
            txtschemetotal.Text = "";
            #endregion

            #region Clear All Av. Stock TextBoxes
            /*txtAvStock1.Text="";
			txtAvStock2.Text="";
			txtAvStock3.Text="";
			txtAvStock4.Text="";
			txtAvStock5.Text="";
			txtAvStock6.Text="";
			txtAvStock7.Text="";
			txtAvStock8.Text="";
			txtAvStock9.Text="";
			txtAvStock10.Text="";
			txtAvStock11.Text="";
			txtAvStock12.Text="";*/
            #endregion

            #region Clear Hidden TextBoxex
            /*txtProdName1.Value=""; 
			txtProdName2.Value=""; 
			txtProdName3.Value=""; 
			txtProdName4.Value=""; 
			txtProdName5.Value=""; 
			txtProdName6.Value=""; 
			txtProdName7.Value=""; 
			txtProdName8.Value=""; 
			txtProdName9.Value=""; 
			txtProdName10.Value=""; 
			txtProdName11.Value=""; 
			txtProdName12.Value=""; 
			txtPack1.Value="";
			txtPack2.Value="";
			txtPack3.Value="";
			txtPack4.Value="";
			txtPack5.Value="";
			txtPack6.Value="";
			txtPack7.Value="";
			txtPack8.Value="";
			
			txtPack9.Value="";
			txtPack10.Value="";
			txtPack11.Value="";
			txtPack12.Value="";*/
            /*txtTempQty1.Text="";  
			txtTempQty2.Text="";
			txtTempQty3.Text="";
			txtTempQty4.Text="";
			txtTempQty5.Text="";
			txtTempQty6.Text="";
			txtTempQty7.Text="";
			txtTempQty8.Text="";
			txtTempQty9.Text="";
			txtTempQty10.Text="";
			txtTempQty11.Text="";
			txtTempQty12.Text="";
			tmpQty1.Value = "";
			tmpQty2.Value = "";
			tmpQty3.Value = "";
			tmpQty4.Value = "";
			tmpQty5.Value = "";
			tmpQty6.Value = "";
			tmpQty7.Value = "";
			tmpQty8.Value = "";
			tmpQty9.Value = "";
			tmpQty10.Value = "";
			tmpQty11.Value = "";
			tmpQty12.Value = "";*/
            totalltr.Value = "";
            tempdiscount.Value = "";
            tempcashdis.Value = "";
            //******Mahesh,10.04.007
            HtmlInputText[] DropType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
            TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
            TextBox[] TextRate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
            TextBox[] TextAmount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
            TextBox[] Scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
            TextBox[] Foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
            TextBox[] AVStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };
            TextBox[] TempQty = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12 };
            HtmlInputHidden[] tmpQty = { tmpQty1, tmpQty2, tmpQty3, tmpQty4, tmpQty5, tmpQty6, tmpQty7, tmpQty8, tmpQty9, tmpQty10, tmpQty11, tmpQty12 };
            //******************
            TextBox[] ProdNamesch = { txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12 };
            TextBox[] ProdTypesch = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
            TextBox[] Avlsch = { txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12 };
            TextBox[] PackTypesch = { txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12 };
            TextBox[] Qtysch = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
            HtmlInputHidden[] SchSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
            HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
            HtmlInputHidden[] tmpFoeType = { tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12 };
            TextBox[] SchQuantity = { txtTempSchQty1, txtTempSchQty2, txtTempSchQty3, txtTempSchQty4, txtTempSchQty5, txtTempSchQty6, txtTempSchQty7, txtTempSchQty8, txtTempSchQty9, txtTempSchQty10, txtTempSchQty11, txtTempSchQty12 };
            HtmlInputHidden[] SchSPType = { tmpSecSPType1, tmpSecSPType2, tmpSecSPType3, tmpSecSPType4, tmpSecSPType5, tmpSecSPType6, tmpSecSPType7, tmpSecSPType8, tmpSecSPType9, tmpSecSPType10, tmpSecSPType11, tmpSecSPType12 };

            HtmlInputHidden[] temcombo = { temcombo1, temcombo2, temcombo3, temcombo4, temcombo5, temcombo6, temcombo7, temcombo8, temcombo9, temcombo10, temcombo11, temcombo12 };        //add by vikas 9.11.2012

            for (int ii = 0; ii < ProdNamesch.Length; ii++)
            {
                ProdNamesch[ii].Text = "";
                ProdTypesch[ii].Text = "";
                Avlsch[ii].Text = "";
                PackTypesch[ii].Text = "";
                Qtysch[ii].Text = "";
                SchQuantity[ii].Text = "";
                //*****
                ProductType[ii] = "";
                ProductName[ii] = "";
                ProductPack[ii] = "";
                ProductQty[ii] = "";
                SchProductType[ii] = "";
                SchProductName[ii] = "";
                SchProductPack[ii] = "";
                SchProductQty[ii] = "";
                tmpSchType[ii].Value = "";
                tmpFoeType[ii].Value = "";
                SchSPType[ii].Value = "";
                //***********************
                DropType[ii].Value = "Type";
                Qty[ii].Text = "";
                TextRate[ii].Text = "";
                TextAmount[ii].Text = "";
                Scheme[ii].Text = "";
                Foe[ii].Text = "";
                AVStock[ii].Text = "";
                TempQty[ii].Text = "";
                tmpQty[ii].Value = "";
                SchSP[ii].Value = "";
                //***********************

                temcombo[ii].Value = "";            //add by vikas 9.11.2012
            }
            //******
            #endregion

        }

        /// <summary>
        /// This method clear the form.
        /// </summary>
        public void clear1()
        {
            /*DropPack1.SelectedIndex=0;
			DropPack2.SelectedIndex=0;
			DropPack3.SelectedIndex=0;
			DropPack4.SelectedIndex=0;
			DropPack5.SelectedIndex=0;
			DropPack6.SelectedIndex=0;
			DropPack7.SelectedIndex=0;
			DropPack8.SelectedIndex=0;
			DropPack9.SelectedIndex=0;
			DropPack10.SelectedIndex=0;
			DropPack11.SelectedIndex=0;
			DropPack12.SelectedIndex=0;*/
            //			DropDownList[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8};
            //			DropDownList[] ProdName={DropProd1, DropProd2, DropProd3, DropProd4, DropProd5, DropProd6, DropProd7, DropProd8};
            //			DropDownList[] PackType={DropPack1, DropPack2, DropPack3, DropPack4, DropPack5, DropPack6, DropPack7, DropPack8};
            //			TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8}; 
            //			TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8}; 
            //			TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8}; 			
            //			TextBox[]  AvStock = {txtAvStock1,txtAvStock2,txtAvStock3,txtAvStock4,txtAvStock5,txtAvStock6,txtAvStock7,txtAvStock8};

            //DropDownList[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12};
            //HtmlInputText[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12};
            //***DropDownList[] ProdName={DropProd1, DropProd2, DropProd3, DropProd4, DropProd5, DropProd6, DropProd7, DropProd8, DropProd9, DropProd10, DropProd11, DropProd12};
            //***DropDownList[] PackType={DropPack1, DropPack2, DropPack3, DropPack4, DropPack5, DropPack6, DropPack7, DropPack8, DropPack9, DropPack10, DropPack11, DropPack12};
            TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
            TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
            TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
            TextBox[] AvStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };

            for (int i = 0; i < Qty.Length; i++)
            {
                //ProdType[i].Enabled = true;
                //ProdName[i].Enabled = true;
                //PackType[i].Enabled = true;
                Qty[i].Enabled = true;
                Rate[i].Enabled = true;
                Amount[i].Enabled = true;
                AvStock[i].Enabled = true;
            }
            lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
            DropOrderInvoice.SelectedIndex = 0;
            DropOrderInvoice.Enabled = true;
        }

        /// <summary>
        /// This method returns the next Invoice No
        /// </summary>
        public void GetNextInvoiceNo()
        {
            //List<string> lstFromDateToDate = new List<string>();
            //lstFromDateToDate.Add(FromDate);
            //lstFromDateToDate.Add(ToDate);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    //var myContent = JsonConvert.SerializeObject(lstFromDateToDate);
                    //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    //var byteContent = new ByteArrayContent(buffer);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync("/api/sales/GetNextInvoiceNo").Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;

                            lblInvoiceNo.Text = res;
                        }
                    }
                }

                //            InventoryClass  obj=new InventoryClass ();
                //SqlDataReader SqlDtr,rdr=null;
                //string sql;

                //#region Fetch the Next Invoice Number
                //sql="select max(Invoice_No) from Sales_Master";
                //SqlDtr=obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	string InNo=SqlDtr.GetValue(0).ToString ();
                //	string fdt="",No="";
                //	int n=0;
                //	if(InNo!="" && InNo.Length>=4)
                //	{
                //		if(FromDate.StartsWith("0"))
                //		{
                //			fdt=FromDate.Substring(1)+ToDate;
                //			No=InNo.Substring(0,3);
                //		}
                //		else
                //		{
                //			fdt=FromDate+ToDate;
                //			if(fdt.Length==3)
                //				No=InNo.Substring(0,3);
                //			else
                //				No=InNo.Substring(0,4);
                //		}
                //	}
                //	else
                //		fdt="0";
                //	if(fdt==No)
                //	{				
                //		//lblInvoiceNo.Text =SqlDtr.GetValue(0).ToString ();				
                //		if(No.Length==3)
                //			InNo=InNo.Substring(3);
                //		else
                //			InNo=InNo.Substring(4);
                //		n=int.Parse(InNo);
                //		lblInvoiceNo.Text=System.Convert.ToString(++n);
                //	}
                //	else
                //		//if(lblInvoiceNo.Text=="")
                //	{	//lblInvoiceNo.Text ="1001";
                //		dbobj.SelectQuery("select * from organisation",ref rdr);
                //		if(rdr.Read())
                //		{
                //			lblInvoiceNo.Text =rdr["StartInvoice"].ToString();
                //		}
                //		else
                //			lblInvoiceNo.Text ="1";
                //	}

                //}
                //SqlDtr.Close ();		
                //#endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : Sales Invoice.aspx, Method : GetNextInvoiceNo().  EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
        }

        /// <summary>
        /// This method fatch the only year according to passing date.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetYear(string dt)
        {
            if (dt != "")
            {
                string[] year = dt.IndexOf("-") > 0 ? dt.Split(new char[] { '-' }, dt.Length) : dt.Split(new char[] { '/' }, dt.Length);
                string yr = year[2].Substring(2);
                return (yr);
            }
            else
                return "";
        }

        /// <summary>
        /// This method is not used.
        /// </summary>
        string mw = "";
        public void Getmwid()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                #region Fetch the Next Invoice Number
                sql = "select max(mwid)+1 from monthwise";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    mw = SqlDtr.GetValue(0).ToString();
                    if (mw == "")
                        mw = "1";
                }
                SqlDtr.Close();
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : Getmwid() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
        }
        protected bool CheckStock()
        {
            bool ok = false;
            return ok;
        }

        /// <summary>
        /// contacts the print server and sends the SalesInvoicePrePrintReport.txt file name to print.
        /// </summary>
        /// <param name="fileName"></param>
        public void print(string fileName)
        {
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // The name of the
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 62000);

                // Create a TCP/IP  socket.
                Socket sender1 = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:print" + " Sales Invoise is Print  userid   " + "   " + uid);
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender1.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}",
                        sender1.RemoteEndPoint.ToString());
                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(fileName + "<EOF>");
                    // Send the data through the socket.
                    int bytesSent = sender1.Send(msg);
                    // Receive the response from the remote device.
                    int bytesRec = sender1.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    // Release the socket.
                    sender1.Shutdown(SocketShutdown.Both);
                    sender1.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:print" + ane.Message + "  EXCEPTION " + " user " + uid);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:print" + se.Message + "  EXCEPTION " + " user " + uid);
                }
                catch (Exception es)
                {
                    Console.WriteLine("Unexpected exception : {0}", es.ToString());
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:print" + es.Message + "  EXCEPTION " + " user " + uid);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:print" + ex.Message + "  EXCEPTION " + " user " + uid);
            }
        }

        /// <summary>
        /// This method checks the price updation for all the products is available or not?
        /// </summary>
        public void GetProducts()
        {
            SalesModel sales = new SalesModel();
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("http://localhost:55251/api/sales"); baseUri

                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("/api/sales/GetProducts").Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;

                            //lblInvoiceNo.Text = res;
                            sales = JsonConvert.DeserializeObject<SalesModel>(res);
                        }
                    }
                }

                lblMessage.Text = sales.Message;
                temptext.Value = sales.TempText;
                tempminmax.Value = sales.Tempminmax;

                //            InventoryClass  obj=new InventoryClass ();
                //InventoryClass  obj1=new InventoryClass ();
                //SqlDataReader SqlDtr;
                //string sql;
                //SqlDataReader rdr=null; 
                //int count = 0;
                //int count1 = 0;
                //dbobj.ExecuteScalar("Select Count(Prod_id) from  products",ref count);
                //dbobj.ExecuteScalar("select count(distinct p.Prod_ID ) from products p, Price_Updation pu where p.Prod_id = pu.Prod_id",ref count1);
                ////				sql = "select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                ////				SqlDtr = obj.GetRecordSet(sql); 
                ////				while(SqlDtr.Read())
                ////				{			
                ////					count1 = count1+1;
                ////				}					
                ////				SqlDtr.Close();
                //if(count != count1)
                //{
                //	lblMessage.Text = "Price updation not available for some products";
                //}

                //#region Fetch the Product Types and fill in the ComboBoxes
                //string str="",MinMax="";
                ////sql="select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit,minlabel,maxlabel,reorderlable from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                //sql="select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit,minlabel,maxlabel,reorderlable from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                //SqlDtr=obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	#region Fetch Sales Rate
                //	/********
                //	sql= "select top 1 Sal_Rate from Price_Updation where Prod_ID="+SqlDtr["Prod_ID"]+" order by eff_date desc";
                //	//dbobj.SelectQuery(sql,ref rdr); 
                //	rdr = obj1.GetRecordSet(sql);
                //	if(rdr.Read())
                //	{
                //		if(double.Parse(rdr.GetValue(0).ToString())==0)
                //		{
                //			rdr.Close();
                //			continue;
                //		}
                //	}
                //	rdr.Close();
                //	***********/
                //	//str=str+ SqlDtr["Category"]+":"+SqlDtr["Prod_Name"]+":"+SqlDtr["Pack_Type"];
                //	sql= "select top 1 Pur_Rate from Price_Updation where Prod_ID="+SqlDtr["Prod_ID"]+ " and Pur_Rate<>0 order by eff_date desc";
                //                //dbobj.SelectQuery(sql,ref rdr); 
                //                rdr = obj1.GetRecordSet(sql);
                //	if(rdr.Read())
                //	{
                //		if(double.Parse(rdr["Pur_Rate"].ToString())!=0)
                //		{
                //			str=str+ SqlDtr["Category"]+":"+SqlDtr["Prod_Name"]+":"+SqlDtr["Pack_Type"];
                //			str=str+":"+rdr["Pur_Rate"];
                //		}
                //		else
                //		{
                //			rdr.Close();
                //			continue;
                //		}
                //	}
                //	else
                //		str=str+":0";
                //	rdr.Close();

                //	//********
                //	MinMax=MinMax+SqlDtr["Prod_Name"]+":"+SqlDtr["Pack_Type"]+":"+SqlDtr["minlabel"]+":"+SqlDtr["maxlabel"]+":"+SqlDtr["reorderlable"]+"~";
                //	//********
                //	#endregion

                //	#region Fetch Closing Stock
                //	sql="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr["Prod_ID"]+" order by stock_date desc";
                //	//dbobj.SelectQuery(sql,ref rdr); 
                //	rdr = obj1.GetRecordSet(sql);
                //	if(rdr.Read())
                //		//**str=str+":"+rdr["Closing_Stock"]+":"+SqlDtr["Unit"]+",";
                //		str=str+":"+rdr["Closing_Stock"]+":"+SqlDtr["Unit"];
                //	else
                //		//str=str+":0"+":"+SqlDtr["Unit"]+",";
                //		str=str+":0"+":"+SqlDtr["Unit"];
                //	rdr.Close();
                //	#endregion
                //	//*************
                //	#region Fetch Scheme 
                //	//sql="select discount from schemeupdation where Prod_ID="+SqlDtr["Prod_ID"]+"";
                //	sql="select discount from oilscheme where ProdID="+SqlDtr["Prod_ID"]+"";
                //	//dbobj.SelectQuery(sql,ref rdr);
                //	rdr = obj1.GetRecordSet(sql);
                //	if(rdr.Read())
                //		str=str+":"+rdr["discount"]+",";
                //	else
                //		str=str+":0"+",";
                //	rdr.Close();
                //	#endregion
                //	//*******************
                //}
                //SqlDtr.Close();
                //temptext.Value=str;
                //tempminmax.Value=MinMax;
                //#endregion		 
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:GetProducts().  EXCEPTION: " + ex.Message + "  user " + uid);
            }
        }

        /// <summary>
        /// This method get only fleet or oe type customer.
        /// </summary>
        public void GetFOECust()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //var response = client.GetAsync("/api/sales/GetDataSelectedOrderInvoice?id=" + DropOrderInvoice.SelectedItem.Value).Result;

                    var response = client.GetAsync("/api/sales/GetFOECust").Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                            temptext13.Value = res;
                        }
                    }
                }

                //InventoryClass  obj=new InventoryClass ();
                //SqlDataReader SqlDtr;
                //string str="";
                //string sql="select cust_Name from customer  where cust_type like'Fleet%' or cust_type like('Oe%')  order by cust_Name";
                //SqlDtr = obj.GetRecordSet (sql);
                //while(SqlDtr.Read ())
                //{
                //	str=str+","+SqlDtr.GetValue(0).ToString().Trim();
                //}
                //SqlDtr.Close();
                //temptext13.Value=str;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : GetFOECust() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method stored given scheme of the products and closing stock also.
        /// </summary>
        public void getscheme()
        {
            try
            {
                string strInvoiceDate = lblInvoiceDate.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    //var response = client.GetAsync("/api/sales/GetDataSelectedOrderInvoice?id=" + DropOrderInvoice.SelectedItem.Value).Result;

                    var response = client.GetAsync("/api/sales/Getscheme?invoiceDate=" + strInvoiceDate).Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                            temptext11.Value = res;
                            //sales = JsonConvert.DeserializeObject<SalesModel>(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : getscheme() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method stored only liter scheme information.
        /// </summarytemptextSecSP
        public void getscheme1()
        {
            try
            {
                string strInvoiceDate = lblInvoiceDate.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("/api/sales/Getscheme1?invoiceDate=" + strInvoiceDate).Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                            temptext12.Value = res;
                        }
                    }
                }

                //InventoryClass  obj=new InventoryClass ();
                //SqlDataReader SqlDtr;
                //string sql;
                //string str="";
                ////SqlDataReader rdr=null; 
                //int i=0;
                ////sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack free,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis  from products p,oilscheme o where p.prod_id=o.prodid and";
                ////sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname='Secondry(LTR Scheme)' or o.schname='Primary(LTR Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
                ////Mahesh11.04.007 

                ////coment by vikas 25.10.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";

                //sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype,o.group_name gname,o.unit unit from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103)";    //Add by vikas 25.10.2012

                ////sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()) +"'";
                //SqlDtr=obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	//Coment by vikas 25.10.2012 str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";
                //	i++;

                //	str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"];     //add by vikas 25.10.2012

                //	/*********Add by vikas 25.10.2012************
                //	if(SqlDtr["gname"]!= null && SqlDtr["unit"]!= null)
                //	{
                //		str=str+":"+SqlDtr["gname"]+":"+SqlDtr["unit"]+",";
                //	}
                //	else
                //	{
                //		str=str+":"+0+":"+0+",";
                //	}
                //	**********End***********/
                //	/*********add by vikas 25.10.2012************/
                //	if(SqlDtr["gname"].ToString().Trim()!= null && SqlDtr["gname"].ToString().Trim()!= "")
                //		str=str+":"+SqlDtr["gname"];       // 
                //	else
                //		str=str+":"+0;

                //	if(SqlDtr["unit"].ToString().Trim()!= null && SqlDtr["unit"].ToString().Trim()!= "")
                //		str=str+":"+SqlDtr["unit"]+",";       
                //	else
                //		str=str+":"+0+",";
                //	/*****************************/

                //}
                //int j=i;
                //SqlDtr.Close();
                //temptext12.Value=str;
                //	MessageBox.Show("Secondry " +str);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : getscheme1() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method stored only Secondry spacial liter scheme information.
        /// </summary>
        public void getschemeSecSP()
        {
            try
            {
                string strInvoiceDate = lblInvoiceDate.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync("/api/sales/GetschemeSecSP?invoiceDate=" + strInvoiceDate).Result;
                    string res = "";

                    if (response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            // ... Read the string.
                            Task<string> result = content.ReadAsStringAsync();
                            res = result.Result;
                            temptextSecSP.Value = res;
                        }
                    }
                }

                //            InventoryClass  obj=new InventoryClass();
                //SqlDataReader SqlDtr;
                //string sql;
                //string str="";
                ////SqlDataReader rdr=null; 

                ////sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack free,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis  from products p,oilscheme o where p.prod_id=o.prodid and";
                ////sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname='Secondry(LTR Scheme)' or o.schname='Primary(LTR Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
                ////Mahesh11.04.007 

                ////coment by vikas 26.10.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103)";
                //sql= "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype,o.group_name gname,o.unit from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Primary(LTR&% Addl Scheme)','Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103)";       //add by vikas 26.10.2012

                ////sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()) +"'";
                //SqlDtr=obj.GetRecordSet(sql);
                //while(SqlDtr.Read())
                //{
                //	//coment by vikas 26.10.2012 str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";

                //	/*************Add by vikas 26.10.2012******************/
                //	str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"];

                //	if(SqlDtr["gname"].ToString().Trim()!= null && SqlDtr["gname"].ToString().Trim()!= "")
                //		str=str+":"+SqlDtr["gname"]+",";
                //	else
                //		str=str+":"+0+",";
                //	/**********End*********************/

                //}
                //SqlDtr.Close();
                //temptextSecSP.Value=str;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : getschemeSecSP() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This Method to write into the report file to print.
        /// </summary>
        public void PrePrintReport()
        {
            try
            {

                string txtval = "";
                InventoryClass obj = new InventoryClass();

                getTemplateDetails();
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesInvoicePrePrintReport1.txt";
                StreamWriter sw = new StreamWriter(path);
                HtmlInputText[] ProdCat = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
                TextBox[] foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
                TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
                TextBox[] scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
                HtmlInputHidden[] SecSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
                TextBox[] schProdType = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                TextBox[] schQty = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
                HtmlInputHidden[] tmpCgst = { tempCgst1, tempCgst2, tempCgst3, tempCgst4, tempCgst5, tempCgst6, tempCgst7, tempCgst8, tempCgst9, tempCgst10, tempCgst11, tempCgst12, tempCgst13, tempCgst14, tempCgst15, tempCgst16, tempCgst17, tempCgst18, tempCgst19, tempCgst20 };
                HtmlInputHidden[] tmpSgst = { tempSgst1, tempSgst2, tempSgst3, tempSgst4, tempSgst5, tempSgst6, tempSgst7, tempSgst8, tempSgst9, tempSgst10, tempSgst11, tempSgst12, tempSgst13, tempSgst14, tempSgst15, tempSgst16, tempSgst17, tempSgst18, tempSgst19, tempSgst20 };
                HtmlInputHidden[] tmpIgst = { tempIgst1, tempIgst2, tempIgst3, tempIgst4, tempIgst5, tempIgst6, tempIgst7, tempIgst8, tempIgst9, tempIgst10, tempIgst11, tempIgst12, tempIgst13, tempIgst14, tempIgst15, tempIgst16, tempIgst17, tempIgst18, tempIgst19, tempIgst20 };
                HtmlInputHidden[] tmpHsn = { tempHsn1, tempHsn2, tempHsn3, tempHsn4, tempHsn5, tempHsn6, tempHsn7, tempHsn8, tempHsn9, tempHsn10, tempHsn11, tempHsn12, tempHsn13, tempHsn14, tempHsn15, tempHsn16, tempHsn17, tempHsn18, tempHsn19, tempHsn20 };


                string[] DespQty = new string[12];
                string[] freeDespQty = new string[12];
                string[] ProdCode = new string[12];
                string[] schProdCode = new string[12];
                string[] schPName = new string[12];
                string[] schPPack = new string[12];
                string[] schPQty = new string[12];
                string[] PackType = new string[12];
                string[] schProdName = new string[12];
                string[] IGST = new string[20];
                string[] CGST = new string[20];
                string[] SGST = new string[20];
                string[] Hsn = new string[20];
                int h1 = System.Convert.ToInt32(Math.Floor((Header1Height * 25) / 4.05));
                int h2 = System.Convert.ToInt32(Math.Floor((Header2Height * 25) / 4.05));
                //Coment by vikas 4.11.09 int bh = System.Convert.ToInt32(Math.Floor((BodyHeight * 24)/4.05));
                int bh = System.Convert.ToInt32(Math.Floor((BodyHeight * 24) / 4.18));          //Add by vikas 4.11.09 becouse message show in print
                int f1 = System.Convert.ToInt32(Math.Floor((Footer1Height * 25) / 4.05));
                int f2 = System.Convert.ToInt32(Math.Floor((Footer2Height * 25) / 4.05));
                int pn = System.Convert.ToInt32(Math.Floor((Position1 * 25) / 1.53));
                int sp = 50;
                int dn = System.Convert.ToInt32(Math.Floor((Position2 * 25) / 1.53));
                dn = dn - (pn + 50);
                int sp1 = 10;
                string info21 = " {0,-" + pn + ":S} {1,-" + sp + ":F} {2," + dn + ":F} {3,-" + sp1 + ":F}";
                int pc = 10;
                int bn = System.Convert.ToInt32(Math.Floor((BatchNo * 25) / 1.53));
                int gpn = System.Convert.ToInt32(Math.Floor((GradePackName * 25) / 1.53));
                int bq = System.Convert.ToInt32(Math.Floor((BillQty * 25) / 1.53));
                int fq = System.Convert.ToInt32(Math.Floor((FreeQty * 25) / 1.53));
                int dq = System.Convert.ToInt32(Math.Floor((DisQty * 25) / 1.53));
                int lkg = System.Convert.ToInt32(Math.Floor((LtrKg * 25) / 1.53));
                int rt = System.Convert.ToInt32(Math.Floor((RateRs * 25) / 1.53));
                int sd = System.Convert.ToInt32(Math.Floor((SchDis * 25) / 1.53));
                int am = System.Convert.ToInt32(Math.Floor((AmountRs * 25) / 1.53));
                int igst = System.Convert.ToInt32(Math.Floor((Igst * 25) / 1.53));
                int cgst = System.Convert.ToInt32(Math.Floor((Cgst * 25) / 1.53));
                int sgst = System.Convert.ToInt32(Math.Floor((Sgst * 25) / 1.53));
                string info31 = " {0,-" + pc + ":S} {1,-" + bn + ":F} {2,-" + gpn + ":F} {3," + dq + ":F} {4," + lkg + ":F} {5," + rt + ":F} {6," + sd + ":F} {7," + SSpDis + ":F} {8," + am + ":F}  {9," + cgst + ":F} {10," + sgst + ":F} {11," + igst + ":F}";
                int rinw = System.Convert.ToInt32(Math.Floor((RupeesinWords * 25) / 1.53));
                int pb = System.Convert.ToInt32(Math.Floor((ProvisionalBalance * 25) / 1.53));
                int rem = System.Convert.ToInt32(Math.Floor((Remarks * 25) / 1.53));
                string info51 = " {0,-" + rinw + ":S} {1,-" + 100 + ":S}";
                string info61 = " {0,-" + pb + ":S} {1,-10:S} {2,-40:S}";
                string info71 = " {0,-" + rem + ":S} {1,-80:S}";
                Double TotalQtyPack = 0, TotalQtyfoe = 0, TotalfoeLtr = 0;
                int k = 0;
                string info4 = "", str = "", InDate = "";
                info4 = " {0,-20:S} {1,20:S} {2,20:S} {3,55:S} {4,15:S} ";
                string curbal = lblCurrBalance.Value;
                string[] CurrBal = new string[2];
                string[] InvoiceDate = new string[2];

                if (lblInvoiceNo.Visible == true)
                    str = FromDate + ToDate + lblInvoiceNo.Text;
                else
                    str = FromDate + ToDate + dropInvoiceNo.SelectedItem.Text.Trim();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/sales/GetinvoiceDate?invoiceDate=" + str).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var date = Res.Content.ReadAsStringAsync().Result;
                        InDate = JsonConvert.DeserializeObject<string>(date);
                    }
                }

                //            if (lblInvoiceNo.Visible==true)
                //	str="select invoice_date from sales_master where invoice_no="+FromDate+ToDate+lblInvoiceNo.Text+"";
                //else
                //	str="select invoice_date from sales_master where invoice_no="+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"";

                //SqlDtr=obj.GetRecordSet(str);
                //if(SqlDtr.Read())
                //	InDate=SqlDtr.GetValue(0).ToString();
                //else
                //	InDate="";
                //SqlDtr.Close();

                if (InDate != "")
                    InvoiceDate = InDate.Split(new char[] { ' ' }, InDate.Length);
                else
                    InvoiceDate[1] = "";

                if (curbal != "")
                {
                    CurrBal = curbal.Split(new char[] { ' ' }, curbal.Length);
                }
                if (CurrBal[1].Equals("Dr."))
                {
                    if (txtNetAmount.Text != "")
                    {
                        if (dropInvoiceNo.Visible != true)
                            CurrBal[0] = System.Convert.ToString(System.Convert.ToDouble(CurrBal[0]) + System.Convert.ToDouble(txtNetAmount.Text));
                    }
                    else
                        CurrBal[0] = "";
                }
                else
                {
                    if (txtNetAmount.Text != "")
                    {
                        if (System.Convert.ToDouble(txtNetAmount.Text) > System.Convert.ToDouble(CurrBal[0]))
                        {
                            if (dropInvoiceNo.Visible != true)
                                CurrBal[0] = System.Convert.ToString(System.Convert.ToDouble(txtNetAmount.Text) - System.Convert.ToDouble(CurrBal[0]));
                        }
                        else
                            CurrBal[0] = System.Convert.ToString(System.Convert.ToDouble(CurrBal[0]) - System.Convert.ToDouble(txtNetAmount.Text));
                    }
                    else
                        CurrBal[0] = "";
                }
                string[] arrProdType = new string[3];
                for (int p = 0; p < 12; p++)
                {
                    if (ProdCat[p].Value.IndexOf(":") > 0)
                        arrProdType = ProdCat[p].Value.Split(new char[] { ':' }, ProdCat[p].Value.Length);
                    else
                    {
                        arrProdType[0] = "";
                        arrProdType[1] = "";
                        arrProdType[2] = "";
                    }
                    List<string> sales = new List<string>();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetProdCode_PackType?ProdType1=" + arrProdType[1].ToString() + "&ProdType2=" + arrProdType[2].ToString()).Result;
                        //var Res = client.GetAsync("api/sales/GetinvoiceDate?invoiceDate=" + str).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var product = Res.Content.ReadAsStringAsync().Result;
                            sales = JsonConvert.DeserializeObject<List<string>>(product);
                        }
                    }

                    ProdCode[p] = sales[0].ToString();
                    PackType[p] = sales[1].ToString();

                    //str="select Prod_Code,Total_Qty from Products where Prod_Name='"+arrProdType[0].ToString()+"' and Pack_Type='"+arrProdType[1].ToString()+"'"; //Comment by vikas sharma 30.04.09
                    //               str ="select Prod_Code,Total_Qty from Products where Prod_Name='"+arrProdType[1].ToString()+"' and Pack_Type='"+arrProdType[2].ToString()+"'";
                    //SqlDtr = obj.GetRecordSet(str);
                    //if(SqlDtr.Read())
                    //{
                    //	ProdCode[p]=SqlDtr.GetValue(0).ToString();
                    //	PackType[p]=SqlDtr.GetValue(1).ToString();
                    //}
                    //else
                    //{
                    //	ProdCode[p]="";
                    //	PackType[p]="";
                    //}
                    //SqlDtr.Close();
                }

                int p1 = 0;
                string[] arrProdSchType = new string[2];
                for (int p = 0; p < 12; p++)
                {
                    if (schProdType[p].Text.IndexOf(":") > 0)
                        arrProdSchType = schProdType[p].Text.Split(new char[] { ':' }, schProdType[p].Text.Length);
                    else
                    {
                        arrProdSchType[0] = "";
                        arrProdSchType[1] = "";
                        //arrProdSchType[2]="";
                    }

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetschProdCode?ProdSchType1=" + arrProdSchType[0].ToString() + "&ProdSchType2=" + arrProdSchType[1].ToString()).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var product = Res.Content.ReadAsStringAsync().Result;
                            schProdCode[p1] = JsonConvert.DeserializeObject<string>(product);
                            p1++;
                        }
                    }

                    //               str ="select Prod_Code from Products where Prod_Name='"+arrProdSchType[0].ToString()+"' and Pack_Type='"+arrProdSchType[1].ToString()+"'"; 
                    ////str="select Prod_Code from Products where Prod_Name='"+arrProdSchType[1].ToString()+"' and Pack_Type='"+arrProdSchType[2].ToString()+"'"; //Comment By vikas sharma 16.05.09
                    //SqlDtr = obj.GetRecordSet(str);
                    //if(SqlDtr.Read())
                    //{
                    //	schProdCode[p1]=SqlDtr.GetValue(0).ToString();
                    //	p1++;
                    //}
                    //SqlDtr.Close();
                }

                int jj1 = 0;
                string[] arrProdSchType1 = new string[2];
                for (int jj = 0; jj < 12; jj++)
                {
                    if (schProdType[jj].Text.IndexOf(":") > 0)
                        arrProdSchType1 = schProdType[jj].Text.Split(new char[] { ':' }, schProdType[jj].Text.Length);
                    else
                    {
                        arrProdSchType1[0] = "";
                        arrProdSchType1[1] = "";
                    }
                    if (!arrProdSchType1[0].ToString().Equals("") && !schQty[jj].Text.Equals(""))
                    {
                        schPName[jj1] = "(FREE) " + schProdType[jj].Text;
                        schPQty[jj1] = schQty[jj].Text;
                        schProdName[jj1] = schProdType[jj].Text;
                        schPPack[jj1] = arrProdSchType1[1].ToString();
                        jj1++;
                    }
                }
                for (int jj = jj1; jj < 12; jj++)
                {
                    schPQty[jj] = "";
                }
                for (int j = 0; j < 12; j++)
                {
                    if (!Qty[j].Text.Equals(""))
                    {
                        TotalQtyPack = TotalQtyPack + System.Convert.ToDouble(Qty[j].Text);
                        DespQty[j] = Qty[j].Text;
                    }
                    else
                        DespQty[j] = "";
                    if (!schQty[j].Text.Equals(""))
                    {
                        TotalQtyfoe = TotalQtyfoe + System.Convert.ToDouble(schQty[j].Text);
                        freeDespQty[j] = schQty[j].Text;
                    }
                    else
                        freeDespQty[j] = "";
                }
                string[] arrProdSchType2 = new string[2];
                for (int i = 0; i < 12; i++)
                {
                    if (schProdType[i].Text.IndexOf(":") > 0)
                        arrProdSchType2 = schProdType[i].Text.Split(new char[] { ':' }, schProdType[i].Text.Length);
                    else
                    {
                        arrProdSchType2[0] = "";
                        arrProdSchType2[1] = "";
                    }
                    if (arrProdSchType2[1].ToString() != "" && schQty[i].Text != "")
                    {
                        TotalfoeLtr = TotalfoeLtr + System.Convert.ToDouble(GenUtil.changeqtyltr(arrProdSchType2[1].ToString(), int.Parse(schQty[i].Text)));
                    }
                }

                //***********************************************************************
                ArrayList arrProdCode = new ArrayList();
                ArrayList arrProdName = new ArrayList();
                ArrayList arrBatchNo = new ArrayList();
                ArrayList arrBillQty = new ArrayList();
                ArrayList arrFreeQty = new ArrayList();
                ArrayList arrDespQty = new ArrayList();
                ArrayList arrLtrkg = new ArrayList();
                ArrayList arrProdRate = new ArrayList();
                ArrayList arrProdScheme = new ArrayList();
                ArrayList arrProdAmount = new ArrayList();
                ArrayList arrSecSP = new ArrayList();
                ArrayList arrCgst = new ArrayList();
                ArrayList arrSgst = new ArrayList();
                ArrayList arrIgst = new ArrayList();

                List<string> btQty = new List<string>();
                for (int p = 0; p <= Qty.Length - 1; p++)
                {
                    //InsertBatchNo(arrName[1].ToString(),arrName[2].ToString(),Qty[j].Text);  //hide this code for some time
                    if (Qty[p].Text != "")
                    {
                        string a = ProdCode[p].ToString();


                        string[] arrProdCat = ProdCat[p].Value.Split(new char[] { ':' }, ProdCat[p].Value.Length);
                        if (lblInvoiceNo.Visible == true)
                        {
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var Res = client.GetAsync("api/sales/GetBatchDetailsForProdCodeSales?ProdCode=" + ProdCode[p].ToString() + "&arrProdCat1=" + arrProdCat[1].ToString() + "&arrProdCat2=" + arrProdCat[2].ToString() + "&InvoiceNo=" + lblInvoiceNo.Text).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    btQty = JsonConvert.DeserializeObject<List<string>>(id);
                                }
                            }
                            //str = "select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='" + ProdCode[p].ToString() + "' and Prod_Name='" + arrProdCat[1].ToString() + "' and Pack_Type='" + arrProdCat[2].ToString() + "') and b.batch_id=bt.batch_id and bt.trans_id='" + lblInvoiceNo.Text + "' and trans_type='Sales Invoice'";
                        }
                        else
                        {
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var Res = client.GetAsync("api/sales/GetBatchDetailsForProdCodeSales?ProdCode=" + ProdCode[p].ToString() + "&arrProdCat1=" + arrProdCat[1].ToString() + "&arrProdCat2=" + arrProdCat[2].ToString() + "&InvoiceNo=" + dropInvoiceNo.SelectedItem.Text).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    btQty = JsonConvert.DeserializeObject<List<string>>(id);
                                }
                            }

                            // str = "select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='" + ProdCode[p].ToString() + "' and Prod_Name='" + arrProdCat[1].ToString() + "' and Pack_Type='" + arrProdCat[2].ToString() + "') and b.batch_id=bt.batch_id and bt.trans_id='" + dropInvoiceNo.SelectedItem.Text + "' and trans_type='Sales Invoice'";
                        }

                        int i = 0;
                        string d = arrProdCat[1] + "," + arrProdCat[2];
                        double qty = 0;
                        //SqlDtr = obj.GetRecordSet(str);
                        if (btQty.Count > 0)
                        {
                            while (i < btQty.Count)
                            {
                                arrProdCode.Add(ProdCode[p].ToString());

                                /********************************************/
                                //Comment by vikas sharma 1.05.09 arrProdName.Add(ProdCat[p].Value);
                                string Prod_NT = ProdCat[p].Value;
                                string[] Prod_NT1 = Prod_NT.Split(new char[] { ':' });
                                Prod_NT = Prod_NT1[1] + ":" + Prod_NT1[2];
                                arrProdName.Add(Prod_NT);
                                arrCgst.Add(tmpCgst[p].Value);
                                arrSgst.Add(tmpSgst[p].Value);
                                arrIgst.Add(tmpIgst[p].Value);
                                /********************************************/
                                //arrProdName.Add(ProdCat[p].Value);

                                arrBatchNo.Add(tmpHsn[p].Value);
                                arrBillQty.Add(btQty[i].ToString());
                                arrDespQty.Add(btQty[i].ToString());
                                arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString()) * double.Parse(btQty[i].ToString())));
                                GenUtil.strNumericFormat(arrProdRate.Add(Rate[p].Text).ToString());
                                //arrProdScheme.Add(scheme[p].Text);
                                if (tmpSchType[p].Value == "" || tmpSchType[p].Value == "Rs")
                                    arrProdScheme.Add(scheme[p].Text + "/-");
                                else
                                    arrProdScheme.Add(scheme[p].Text + tmpSchType[p].Value);
                                arrSecSP.Add(SecSP[p].Value);
                                GenUtil.strNumericFormat(arrProdAmount.Add(System.Convert.ToString(double.Parse(btQty[i].ToString()) * double.Parse(Rate[p].Text))).ToString());
                                arrFreeQty.Add("");

                                /*******Add by vikas 10.06.09******************/
                                if (btQty[i].ToString() != null)
                                    qty = Convert.ToDouble(btQty[i].ToString());
                                double sale_qty = Convert.ToDouble(Qty[p].Text.ToString());
                                double Ope_stk = sale_qty - qty;

                                if (sale_qty > qty)
                                {
                                    arrProdCode.Add(ProdCode[p].ToString());
                                    arrBatchNo.Add(tmpHsn[p].Value);
                                    arrProdName.Add(Prod_NT);
                                    arrDespQty.Add(Ope_stk.ToString());
                                    arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString()) * Ope_stk));
                                    GenUtil.strNumericFormat(arrProdRate.Add(Rate[p].Text).ToString());
                                    //arrProdScheme.Add(scheme[p].Text);
                                    if (tmpSchType[p].Value == "" || tmpSchType[p].Value == "Rs")
                                        arrProdScheme.Add(scheme[p].Text + "/-");
                                    else
                                        arrProdScheme.Add(scheme[p].Text + tmpSchType[p].Value);
                                    arrSecSP.Add(SecSP[p].Value);
                                    GenUtil.strNumericFormat(arrProdAmount.Add(System.Convert.ToString(Ope_stk * double.Parse(Rate[p].Text))).ToString());
                                    arrFreeQty.Add("");
                                }
                                /********End******************************/
                                i++;
                            }
                        }
                        else
                        {
                            arrProdCode.Add(ProdCode[p].ToString());

                            /********************************************/
                            //Comment by vikas sharma 1.05.09 arrProdName.Add(ProdCat[p].Value);
                            string Prod_NT = ProdCat[p].Value;
                            string[] Prod_NT1 = Prod_NT.Split(new char[] { ':' });
                            Prod_NT = Prod_NT1[1] + ":" + Prod_NT1[2];
                            arrProdName.Add(Prod_NT);
                            /********************************************/
                            arrCgst.Add(tmpCgst[p].Value);
                            arrSgst.Add(tmpSgst[p].Value);
                            arrIgst.Add(tmpIgst[p].Value);
                            arrBatchNo.Add(tmpHsn[p].Value);
                            arrBillQty.Add(Qty[p].Text);
                            arrDespQty.Add(DespQty[p].ToString());
                            arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString()) * double.Parse(Qty[p].Text)));
                            GenUtil.strNumericFormat(arrProdRate.Add(Rate[p].Text).ToString());
                            //arrProdScheme.Add(scheme[p].Text);
                            if (tmpSchType[p].Value == "" || tmpSchType[p].Value == "Rs")
                                arrProdScheme.Add(scheme[p].Text + "/-");
                            else
                                arrProdScheme.Add(scheme[p].Text + tmpSchType[p].Value);
                            arrSecSP.Add(SecSP[p].Value);
                            GenUtil.strNumericFormat(arrProdAmount.Add(Amount[p].Text).ToString());
                            arrFreeQty.Add("");

                        }
                        //SqlDtr.Close();
                    }
                }

                int iCount = 0;
                List<string> btSchQty = new List<string>();
                for (int p = 0; p <= schPQty.Length - 1; p++)
                {
                    string s = schPQty[p].ToString();
                    if (schPQty[p].ToString() != "")
                    {
                        string[] arrschProdCat = schProdName[p].Split(new char[] { ':' }, schProdName[p].Length);
                        if (lblInvoiceNo.Visible == true)
                        {
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var Res = client.GetAsync("api/sales/GetBatchDetailsForProdCodeSales?ProdCode=" + schProdCode[p].ToString() + "&arrProdCat1=" + arrschProdCat[0].ToString() + "&arrProdCat2=" + arrschProdCat[1].ToString() + "&InvoiceNo=" + lblInvoiceNo.Text).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    btQty = JsonConvert.DeserializeObject<List<string>>(id);
                                }
                            }
                            //str = "select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='" + schProdCode[p].ToString() + "' and Prod_Name='" + arrschProdCat[0].ToString() + "' and Pack_Type='" + arrschProdCat[1].ToString() + "') and b.batch_id=bt.batch_id and bt.trans_id='" + lblInvoiceNo.Text + "' and trans_type='Sales Invoice'";
                        }

                        else
                        {
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(baseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var Res = client.GetAsync("api/sales/GetBatchDetailsForProdCodeSales?ProdCode=" + schProdCode[p].ToString() + "&arrProdCat1=" + arrschProdCat[0].ToString() + "&arrProdCat2=" + arrschProdCat[1].ToString() + "&InvoiceNo=" + dropInvoiceNo.SelectedItem.Text).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    btQty = JsonConvert.DeserializeObject<List<string>>(id);
                                }
                            }
                            //str = "select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='" + schProdCode[p].ToString() + "' and Prod_Name='" + arrschProdCat[0].ToString() + "' and Pack_Type='" + arrschProdCat[1].ToString() + "') and b.batch_id=bt.batch_id and bt.trans_id='" + dropInvoiceNo.SelectedItem.Text + "' and trans_type='Sales Invoice'";
                        }
                        //SqlDtr = obj.GetRecordSet(str);
                        double totalqty = 0;
                        if (p == 0)
                        {
                            arrProdCode.Add("");
                            arrBatchNo.Add("");
                            arrProdName.Add("");
                            arrDespQty.Add("");
                            arrLtrkg.Add("");
                            arrProdRate.Add("");
                            arrProdScheme.Add("");
                            arrSecSP.Add("");
                            arrProdAmount.Add("");
                            arrBillQty.Add("");
                            arrCgst.Add("");
                            arrSgst.Add("");
                            arrIgst.Add("");
                        }
                        if (btSchQty.Count > 0)
                        {
                            while (iCount < btSchQty.Count)
                            {
                                arrProdCode.Add(schProdCode[p].ToString());
                                arrProdName.Add("(FREE) " + schProdType[p].Text);
                                arrBatchNo.Add(tmpHsn[p].Value);
                                arrBillQty.Add("");
                                if (btSchQty[iCount].ToString() == "" || btSchQty[iCount].ToString() == null)
                                    totalqty = 0;
                                else
                                    totalqty = double.Parse(btSchQty[iCount].ToString());
                                arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(), int.Parse(totalqty.ToString())));
                                arrDespQty.Add(btSchQty[iCount].ToString());
                                arrProdRate.Add("");
                                arrProdScheme.Add("");
                                arrSecSP.Add("");
                                arrProdAmount.Add("");
                                arrFreeQty.Add(btSchQty[iCount].ToString());
                            }
                        }
                        else
                        {
                            arrProdCode.Add(schProdCode[p].ToString());
                            //arrProdName.Add("(FREE) "+ProdCat[p].Value);
                            arrProdName.Add("(FREE) " + schProdName[p].ToString());
                            arrBatchNo.Add("");
                            arrBillQty.Add("");
                            if (schPQty[p].ToString() == "" || schPQty[p].ToString() == null)
                                totalqty = 0;
                            else
                                totalqty = double.Parse(schPQty[p].ToString());
                            arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(), int.Parse(totalqty.ToString())));
                            arrDespQty.Add(schPQty[p].ToString());
                            arrProdRate.Add("");
                            arrProdScheme.Add("");
                            arrSecSP.Add("");
                            arrProdAmount.Add("");
                            arrFreeQty.Add(schPQty[p].ToString());
                        }
                        iCount++;
                        //SqlDtr.Close();
                        arrCgst.Add(tmpCgst[p].Value);
                        arrSgst.Add(tmpSgst[p].Value);
                        arrIgst.Add(tmpIgst[p].Value);
                    }
                }
                //***********************************************************************
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
                int arrCount = 0;//,arrCon=0;
                double Space = 0, SpaceCount = arrBillQty.Count;
                bool FlagCount = false;

                do
                {
                    FlagCount = false;
                    sw.WriteLine("                                                DELIVERY CHALLAN COM INVOICE");
                    //for(int i=0;i<9;i++)//Old
                    for (int i = 0; i < h1 - 1; i++)//Old
                    {
                        sw.WriteLine("");
                    }
                    string addr = "", ssc = "", TinNo = "";
                    // Comment by vikas 1.05.09 dbobj.SelectQuery("select * from customer where cust_name='"+text1.Value+"'",ref SqlDtr);
                    txtval = text1.Value;
                    txtval = txtval.Substring(0, txtval.IndexOf(":"));

                    List<string> cust = new List<string>();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/sales/GetCustomer?txtval=" + txtval).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var product = Res.Content.ReadAsStringAsync().Result;
                            cust = JsonConvert.DeserializeObject<List<string>>(product);
                            addr = cust[0].ToString();
                            ssc = cust[1].ToString();
                            TinNo = cust[2].ToString();
                        }
                    }

                    //               dbobj.SelectQuery("select * from customer where cust_name='"+txtval+"'",ref SqlDtr);
                    //if(SqlDtr.Read())
                    //{
                    //	addr=SqlDtr["Address"].ToString();
                    //	ssc=SqlDtr["sadbhavnacd"].ToString();
                    //	TinNo=SqlDtr["Tin_No"].ToString();
                    //}

                    addr = addr.ToUpper();
                    if (addr.Length > 50)
                        addr = addr.Substring(0, 49);

                    if (PartyName)
                    {
                        if (DocumentNo)
                        {
                            if (lblInvoiceNo.Visible == true)
                            {
                                if (ssc != "")
                                    // Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","",lblInvoiceNo.Text);
                                    sw.WriteLine(info21, "", txtval.ToUpper() + "(" + ssc + ")", "", lblInvoiceNo.Text);
                                else
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper(),"",lblInvoiceNo.Text);
                                    sw.WriteLine(info21, "", txtval.ToUpper(), "", lblInvoiceNo.Text);
                            }
                            else
                            {
                                if (ssc != "")
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","",dropInvoiceNo.SelectedItem.Text);
                                    sw.WriteLine(info21, "", txtval.ToUpper() + "(" + ssc + ")", "", dropInvoiceNo.SelectedItem.Text);
                                else
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper(),"",dropInvoiceNo.SelectedItem.Text);
                                    sw.WriteLine(info21, "", txtval.ToUpper(), "", dropInvoiceNo.SelectedItem.Text);
                            }
                        }
                        else
                        {
                            if (lblInvoiceNo.Visible == true)
                            {
                                if (ssc != "")
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","","");
                                    sw.WriteLine(info21, "", txtval.ToUpper() + "(" + ssc + ")", "", "");
                                else
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper(),"","");
                                    sw.WriteLine(info21, "", txtval.ToUpper(), "", "");
                            }
                            else
                            {
                                if (ssc != "")
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper()+"("+ssc+")","","");
                                    sw.WriteLine(info21, "", txtval.ToUpper() + "(" + ssc + ")", "", "");
                                else
                                    //Comment by vikas 1.05.09 sw.WriteLine(info21,"",text1.Value.ToUpper(),"","");
                                    sw.WriteLine(info21, "", txtval.ToUpper(), "", "");
                            }
                        }
                    }
                    else if (DocumentNo)
                    {
                        if (lblInvoiceNo.Visible == true)
                            sw.WriteLine(info21, "", "", "", lblInvoiceNo.Text);
                        else
                            sw.WriteLine(info21, "", "", "", dropInvoiceNo.SelectedItem.Text);
                    }
                    if (Address)
                    {
                        if (DtTime)
                            sw.WriteLine(info21, "", addr, "", lblInvoiceDate.Text + " " + InvoiceDate[1]);
                        else if (Date)
                            sw.WriteLine(info21, "", addr, "", lblInvoiceDate.Text);
                        else
                            sw.WriteLine(info21, "", addr, "", "");
                    }
                    else if (DtTime)
                        sw.WriteLine(info21, "", "", "", lblInvoiceDate.Text + " " + InvoiceDate[1]);
                    if (Time)
                        sw.WriteLine(info21, "", "", "", InvoiceDate[1]);
                    if (City)
                    {
                        if (DueDate)
                            sw.WriteLine(info21, "City", lblPlace.Value.ToUpper(), "", lblDueDate.Value);
                        else
                            sw.WriteLine(info21, "City", lblPlace.Value.ToUpper(), "", "");
                    }
                    else if (DueDate)
                        sw.WriteLine(info21, "", "", "", lblDueDate.Value);
                    if (Tin_No)
                    {
                        if (VehicleNo)
                            sw.WriteLine(info21, "Tin No", TinNo, "", txtVehicleNo.Text);
                        else
                            sw.WriteLine(info21, "Tin No", TinNo, "", "");
                    }
                    else if (VehicleNo)
                        sw.WriteLine(info21, "", "", "", txtVehicleNo.Text);
                    if (Blank)
                        sw.WriteLine();
                    if (Blank1)
                        sw.WriteLine();
                    //					if(Blank == true && Blank1 == true)
                    //						sw.WriteLine();
                    //sw.WriteLine(info31,"P-Code","  Batch No"," Grade/Package Name","B-Qty","F-Qty"," D-Qty"," Ltr/Kg"," Rate Rs."," Sch Disc."," Amount (Rs.)");
                    sw.WriteLine(info31, "P-Code", "  HSN", " Grade/Package Name", " D-Qty", " Ltr/Kg", " Rate Rs.", " Sch Disc.", "SP Disc.", " Amount (Rs.)", "CGST (Rs.)", "SGST (Rs.)", "IGST (Rs.)");
                    sw.WriteLine("");

                    //add by vikas 10.06.09 for(k=arrCount;k<arrBillQty.Count;k++,arrCount++)

                    for (k = arrCount; k < arrProdName.Count; k++, arrCount++)
                    {
                        //sw.WriteLine(info31,arrProdCode[k].ToString(),arrBatchNo[k].ToString(),GenUtil.TrimLength(arrProdName[k].ToString(),34),arrBillQty[k].ToString(),arrFreeQty[k].ToString(),arrDespQty[k].ToString(),arrLtrkg[k].ToString(),arrProdRate[k].ToString(),arrProdScheme[k].ToString(),arrProdAmount[k].ToString());
                        sw.WriteLine(info31, arrProdCode[k].ToString(), arrBatchNo[k].ToString(), GenUtil.TrimLength(arrProdName[k].ToString(), 34), arrDespQty[k].ToString(), arrLtrkg[k].ToString(), GenUtil.strNumericFormat(arrProdRate[k].ToString()), arrProdScheme[k].ToString(), arrSecSP[k].ToString(), GenUtil.strNumericFormat(arrProdAmount[k].ToString()), GenUtil.strNumericFormat(arrCgst[k].ToString()), GenUtil.strNumericFormat(arrSgst[k].ToString()), GenUtil.strNumericFormat(arrIgst[k].ToString()));

                        //if(k==31 && arrBillQty.Count<39)
                        if (k == bh - 10 && arrBillQty.Count < bh - 2)
                        {
                            FlagCount = true;
                        }
                        if (k == bh - 2)
                        {
                            FlagCount = true;
                            arrCount++;
                            break;
                        }
                        if (k == (bh * 2) - 3)
                        {
                            FlagCount = true;
                            arrCount++;
                            break;
                        }
                    }

                    //Space=SpaceCount-39;
                    Space = SpaceCount - (bh - 2);
                    if (Space > 0)
                    {
                        //SpaceCount-=39;
                        SpaceCount -= (bh - 2);
                        //for(int r=0;r<=31;r++)
                        for (int r = 0; r <= (bh - 10); r++)
                        {
                            sw.WriteLine();
                        }
                    }
                    else
                    {
                        Space = Math.Abs(Space);
                        if (Space >= 8)
                        {
                            for (int r = 8; r <= Space; r++)
                            {
                                sw.WriteLine();
                            }
                        }
                        else
                        {
                            //for(int r=0;r<=Space+11;r++)//old
                            for (int r = 0; r <= Space + f2; r++)
                            {
                                sw.WriteLine();
                            }
                        }
                        SpaceCount = 0;
                    }
                }
                while (FlagCount == true);

                /*********Add by vikas 24.10.09********************************/
                string des = "---------------------------------------------------------------------------------------------------------------------------------------";
                sw.WriteLine(GenUtil.GetCenterAddr(txtMessage.Text.ToString(), des.Length));                                //Add by vikas 24.10.09 to show the message.
                                                                                                                            /**********End*******************************/

                sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(info4, "", "Packs", "Ltrs", "GROSS AMOUNT         : ", txtGrandTotal.Text);
                sw.WriteLine(info4, "", "----------", "----------", "FREE/SCH DISC        : ", "-" + txtschemetotal.Text);
                sw.WriteLine(info4, "", "", "", "Sec./Sp. Disc        : ", "-" + txtSecondrySpDisc.Text);//add
                if (txtfleetoediscountRs.Text == "" || txtfleetoediscountRs.Text == "0")
                    sw.WriteLine(info4, "Act Qty", TotalQtyPack.ToString(), txtliter.Text, "", "");
                //sw.WriteLine(info4,"","","","","");
                else
                    sw.WriteLine(info4, "Act Qty", TotalQtyPack.ToString(), txtliter.Text, "Oe/Fleet Discount    : ", "-" + txtfleetoediscountRs.Text);
                //sw.WriteLine(info4,"","","","Oe/Fleet Discount    : ","-"+txtfleetoediscountRs.Text);
                if (txtDisc.Text == "" || txtDisc.Text == "0")
                    sw.WriteLine(info4, "", "", "", "Discount             : ", "0");
                //sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Discount             : ","0");
                else
                {
                    if (DropDiscType.SelectedItem.Text.Equals("%"))
                        sw.WriteLine(info4, "", "", "", "Discount(" + txtDisc.Text + DropDiscType.SelectedItem.Text + ")      : ", "-" + tempdiscount.Value);
                    //sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Discount("+txtDisc.Text+DropDiscType.SelectedItem.Text+")      : ","-"+tempdiscount.Value);
                    else
                        sw.WriteLine(info4, "", "", "", "Discount(" + txtDisc.Text + DropDiscType.SelectedItem.Text + ")        : ", "-" + txtDiscount.Text);
                    //sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Discount("+DropDiscType.SelectedItem.Text+")        : ","-"+txtDisc.Text);
                }
                if (txtCashDisc.Text == "" || txtCashDisc.Text == "0")
                    sw.WriteLine(info4, "Free Qty", TotalQtyfoe.ToString(), TotalfoeLtr.ToString(), "Cash Discount        : ", "0");
                //sw.WriteLine(info4,"","----------","----------","Cash Discount        : ","0");
                else
                {
                    if (DropCashDiscType.SelectedItem.Text.Equals("%"))
                        sw.WriteLine(info4, "Free Qty", TotalQtyfoe.ToString(), TotalfoeLtr.ToString(), "Cash Discount(" + txtCashDisc.Text + DropCashDiscType.SelectedItem.Text + ") : ", "-" + tempcashdis.Value);
                    //sw.WriteLine(info4,"","----------","----------","Cash Discount("+txtCashDisc.Text+DropCashDiscType.SelectedItem.Text+") : ","-"+tempcashdis.Value);
                    else
                        sw.WriteLine(info4, "Free Qty", TotalQtyfoe.ToString(), TotalfoeLtr.ToString(), "Cash Discount(" + txtCashDisc.Text + DropCashDiscType.SelectedItem.Text + ")   : ", "-" + txtCashDiscount.Text);
                    //sw.WriteLine(info4,"","----------","----------","Cash Discount("+DropCashDiscType.SelectedItem.Text+")   : ","-"+txtCashDisc.Text);
                }
                sw.WriteLine(info4, "", "----------", "----------", "Total CGST          : ", tempTotalCgst.Value);
                sw.WriteLine(info4, "", "", "", "Total SGST          : ", tempTotalSgst.Value);
                sw.WriteLine(info4, "", "", "", "Total IGST          : ", tempTotalIgst.Value);
                sw.WriteLine(info4, "Total Qty", System.Convert.ToString(TotalQtyfoe + TotalQtyPack), System.Convert.ToString(System.Convert.ToDouble(txtliter.Text) + TotalfoeLtr), "Net Amount           : ", txtNetAmount.Text);
                sw.WriteLine(info51, "", GenUtil.ConvertNoToWord(txtNetAmount.Text));
                sw.WriteLine(info61, "", CurrBal[0], "(INCLUDING CURRENT INVOICE AMOUNT)");
                sw.WriteLine(info71, "", txtRemark.Text);
                sw.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:PrePrintReport().  EXCEPTION: " + ex.Message + "  user " + uid);
            }
        }

        /// <summary>
        /// This method are not used.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string trimProduct(string str)
        {
            if (str.Length > 15)
                return str.Substring(0, 15);
            else
                return str;
        }

        // This method read the pre print template and sets the  values in global variables.
        //		public void getTemplateDetails()
        //		{
        //			string home_drive = Environment.SystemDirectory;
        //			home_drive = home_drive.Substring(0,2); 
        //			string path = home_drive+@"\Inetpub\wwwroot\Servosms\PrePrintTemplate.INI";
        //			StreamReader  sr = new StreamReader(path);
        //			string[] data = new string[40];
        //			int n = 0;
        //			string info = "";
        //			while (sr.Peek() >= 0) 
        //			{
        //				info = sr.ReadLine();
        //				if (info.StartsWith("[") || info.StartsWith("#"))
        //				{
        //					continue;
        //				}
        //				else
        //				{
        //					data[n++] = info;
        //				}
        //			}
        //			sr.Close();
        //			string[] strarr = data[0].Split(new Char[] {'x'},data[0].Length);
        //			overallPrintWidth = float.Parse(strarr[0].Trim());
        //			overallPrintHeight = float.Parse(strarr[1].Trim()); 
        //			string[] strarr1 = data[1].Split(new Char[] {'x'},data[1].Length);
        //			effectivePrintWidth = float.Parse(strarr1[0].Trim());
        //			effectivePrintHeight = float.Parse(strarr1[1].Trim());
        //			header = float.Parse(data[2].Trim());
        //			body = float.Parse(data[3].Trim());
        //			footer = float.Parse(data[4].Trim());
        //			rate = float.Parse(data[5].Trim());
        //			quantity = float.Parse(data[6].Trim());
        //			amount = float.Parse(data[7].Trim());
        //			total = float.Parse(data[8].Trim());
        //			string[] strarr2 = data[9].Split(new Char[] {'x'},data[9].Length);
        //			cashPos = float.Parse(strarr2[0].Trim());
        //			cashPosHeight = float.Parse(strarr2[1].Trim());
        //
        //			if(data[10].Trim().Equals("True"))
        //			{
        //				cashMemo = true;
        //			}
        //			else
        //			{
        //				cashMemo = false;
        //			}
        //			if(data[11].Trim().Equals("True"))
        //			{
        //				date = true;
        //			}
        //			else
        //			{
        //				date = false;
        //			}
        //			if(data[12].Trim().Equals("True"))
        //			{
        //				vehicle = true;
        //			}
        //			else
        //			{
        //				vehicle = false;
        //			}
        //			if(data[13].Trim().Equals("True"))
        //			{
        //				address = true;
        //			}
        //			else
        //			{
        //				address = false;
        //			}
        //		}

        /// <summary>
        /// This method read the pre print template and sets the  values in global variables.
        /// </summary>
        public void getTemplateDetails()
        {
            try
            {
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                //string path = home_drive+@"\Inetpub\wwwroot\Servosms\PrePrintTemplate.INI";
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\InvoiceDesigner\SalesInvoicePrePrintTemplate.INI";
                StreamReader sr = new StreamReader(path);
                string[] data = new string[40];
                int n = 0;
                string info = "";
                while (sr.Peek() >= 0)
                {
                    info = sr.ReadLine();
                    if (info.StartsWith("[") || info.StartsWith("#"))
                    {
                        continue;
                    }
                    else
                    {
                        data[n++] = info;
                    }
                }
                sr.Close();
                //string[] strarr = data[0].Split(new Char[] {'x'},data[0].Length);
                Header1Height = float.Parse(data[0].ToString().Trim());
                Header2Height = float.Parse(data[1].ToString().Trim());
                //string[] strarr1 = data[1].Split(new Char[] {'x'},data[1].Length);
                BodyHeight = float.Parse(data[2].ToString().Trim());
                Footer1Height = float.Parse(data[3].ToString().Trim());
                Footer2Height = float.Parse(data[4].Trim());
                RateRs = float.Parse(data[5].Trim());
                BillQty = float.Parse(data[6].Trim());
                AmountRs = float.Parse(data[7].Trim());
                BatchNo = float.Parse(data[8].Trim());
                FreeQty = float.Parse(data[9].Trim());
                DisQty = float.Parse(data[10].Trim());
                GradePackName = float.Parse(data[11].Trim());
                LtrKg = float.Parse(data[12].Trim());
                SchDis = float.Parse(data[13].Trim());
                RupeesinWords = float.Parse(data[14].Trim());
                ProvisionalBalance = float.Parse(data[15].Trim());
                Remarks = float.Parse(data[16].Trim());
                Position1 = float.Parse(data[17].Trim());
                Position2 = float.Parse(data[18].Trim());

                if (data[19].Trim().Equals("True"))
                {
                    PartyName = true;
                }
                else
                {
                    PartyName = false;
                }
                if (data[20].Trim().Equals("True"))
                {
                    Date = true;
                }
                else
                {
                    Date = false;
                }
                if (data[21].Trim().Equals("True"))
                {
                    VehicleNo = true;
                }
                else
                {
                    VehicleNo = false;
                }
                if (data[22].Trim().Equals("True"))
                {
                    Address = true;
                }
                else
                {
                    Address = false;
                }
                if (data[23].Trim().Equals("True"))
                {
                    City = true;
                }
                else
                {
                    City = false;
                }
                if (data[24].Trim().Equals("True"))
                {
                    Tin_No = true;
                }
                else
                {
                    Tin_No = false;
                }
                if (data[25].Trim().Equals("True"))
                {
                    Blank = true;
                }
                else
                {
                    Blank = false;
                }
                if (data[26].Trim().Equals("True"))
                {
                    DocumentNo = true;
                }
                else
                {
                    DocumentNo = false;
                }
                if (data[27].Trim().Equals("True"))
                {
                    DtTime = true;
                }
                else
                {
                    DtTime = false;
                }
                if (data[28].Trim().Equals("True"))
                {
                    DueDate = true;
                }
                else
                {
                    DueDate = false;
                }
                if (data[29].Trim().Equals("True"))
                {
                    Blank1 = true;
                }
                else
                {
                    Blank1 = false;
                }
                if (data[30].Trim().Equals("True"))
                {
                    Time = true;
                }
                else
                {
                    Time = false;
                }
                Cgst = float.Parse(data[31].Trim());
                Sgst = float.Parse(data[32].Trim());
                Igst = float.Parse(data[33].Trim());
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : getTemplateDetails() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /*public void prePrint()
		{	
			try
			{
				int NOC = 14;  //18  1 inche = 18 characters
				int NOC1 = 15;
				double skip1 = 0.3;//0.5;
				double skip2 = 0.1;
				getTemplateDetails();
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesInvoicePrePrintReport.txt";
				string info = "";
				string strInvNo="";
				StreamWriter sw = new StreamWriter(path);
				string blank = "                                                                                                ";
				string str = "";
				// The code present below contains some printer escape sequences.
				// Condensed Mode
				// SI 15 0F
				//  27 38 108 [n] [n] 66
				// 17 11 OnLine
				// 0 48 30
				// 27 38 108 49 79 Landspace 
				// ESC N 78 4E Set skip over perforation 
				// ESC C 67 43 Set page length in lines 
				// ESC @ 64 40 Initialize printer 
				// FF 12 0C Form feed 
				// Online			
				//sw.Write((char)17);	
				// 27,67,22---- 22 lines
				// Initialize
				sw.Write((char)27);
				sw.Write((char)64);
				// ESC P 80 50 Select 10 cpi 
				//sw.Write((char)27);
				//sw.Write((char)80); 
				// 22 lines/page
				sw.Write((char)27);
				sw.Write((char)67); 
				sw.Write((char)23); 
				// Condensed
				sw.Write((char)27);
				sw.Write((char)15); // SI 15 0F Select condensed mode - Works
										
				if(lblInvoiceNo.Visible==true)
					strInvNo= lblInvoiceNo.Text;
				else
					strInvNo=dropInvoiceNo.SelectedItem.Value;   
				str = blank.Substring(1,System.Convert.ToInt32(Math.Round(NOC1 * cashPos)));
				if (cashMemo)
				{
					sw.WriteLine(str + strInvNo);
				}
				else
				{
					sw.WriteLine();
				}
				if (date)
				{
					//Mahesh11.04.007 
					sw.WriteLine(str + lblInvoiceDate.Text.ToString());
					//sw.WriteLine(str + Session["CurrentDate"].ToString());
				}
				else
				{
					sw.WriteLine();
				}
				if (vehicle)
				{
					sw.WriteLine(str + txtVehicleNo.Text);
				}
				else
				{
					sw.WriteLine();
				}
				if (address)
				{
					sw.WriteLine(str + lblPlace.Value);
				}
				else
				{
					sw.WriteLine();
				}
				for (int i = 0; i < System.Convert.ToInt32(skip1 * 10); ++i)
				{
					sw.WriteLine();
				}
				//  25/180 of an Inch  : 27 51 25
				sw.Write((char)27);
				sw.Write((char)51);
				sw.Write((char)25);
				//info = "{0,-15:S} {1,6:F} {2,6:F} {3,9:F}";
				info = "";
				int p = System.Convert.ToInt32(Math.Floor((rate * NOC) - 1));
				int r = System.Convert.ToInt32(Math.Floor((quantity * NOC) - 1));
				int q = System.Convert.ToInt32(Math.Floor((amount * NOC) - 1));
				int a  = System.Convert.ToInt32((Math.Ceiling(effectivePrintWidth - (rate + quantity + amount)) * NOC));
				int t = System.Convert.ToInt32(Math.Ceiling((total * NOC) - 1));
				//info = "{0,-" + p + ":S} {1,-" + r + ":F} {2,-" + q + ":F} {3," + a + ":F}";
				info = "{0,-" + p + ":S} {1," + r + ":F} {2," + q + ":F} {3," + a + ":F}";
//				sw.WriteLine(info,trimProduct(txtProdName1.Value.ToString()+" "+txtPack1.Value.Trim()) ,txtRate1.Text,txtQty1.Text,GenUtil.strNumericFormat(txtAmount1.Text.ToString().Trim()) );
//				sw.WriteLine(info,trimProduct(txtProdName2.Value.ToString()+" "+txtPack2.Value.Trim()) ,txtRate2.Text,txtQty2.Text,GenUtil.strNumericFormat(txtAmount2.Text.ToString().Trim()));
//				sw.WriteLine(info,trimProduct(txtProdName3.Value.ToString()+" "+txtPack3.Value.Trim()) ,txtRate3.Text,txtQty3.Text,GenUtil.strNumericFormat(txtAmount3.Text.ToString().Trim()));
//				sw.WriteLine(info,trimProduct(txtProdName4.Value.ToString()+" "+txtPack4.Value.Trim()) ,txtRate4.Text,txtQty4.Text,GenUtil.strNumericFormat(txtAmount4.Text.ToString().Trim()));
//				sw.WriteLine(info,trimProduct(txtProdName5.Value.ToString()+" "+txtPack5.Value.Trim()) ,txtRate5.Text,txtQty5.Text,GenUtil.strNumericFormat(txtAmount5.Text.ToString().Trim()));
//				sw.WriteLine(info,trimProduct(txtProdName6.Value.ToString()+" "+txtPack6.Value.Trim()) ,txtRate6.Text,txtQty6.Text,GenUtil.strNumericFormat(txtAmount6.Text.ToString().Trim()));
//				sw.WriteLine(info,trimProduct(txtProdName7.Value.ToString()+" "+txtPack7.Value.Trim()) ,txtRate7.Text,txtQty7.Text,GenUtil.strNumericFormat(txtAmount7.Text.ToString().Trim()));
//				sw.WriteLine(info,trimProduct(txtProdName8.Value.ToString()+" "+txtPack8.Value.Trim()) ,txtRate8.Text,txtQty8.Text,GenUtil.strNumericFormat(txtAmount8.Text.ToString().Trim()));
				//********************
//				sw.WriteLine(info,trimProduct(txtProdName9.Value.ToString()+" "+txtPack9.Value.Trim()) ,txtRate9.Text,txtQty9.Text,GenUtil.strNumericFormat(txtAmount9.Text.ToString().Trim())); 
//				sw.WriteLine(info,trimProduct(txtProdName10.Value.ToString()+" "+txtPack10.Value.Trim()) ,txtRate10.Text,txtQty10.Text,GenUtil.strNumericFormat(txtAmount10.Text.ToString().Trim())); 
//				sw.WriteLine(info,trimProduct(txtProdName11.Value.ToString()+" "+txtPack11.Value.Trim()) ,txtRate11.Text,txtQty11.Text,GenUtil.strNumericFormat(txtAmount11.Text.ToString().Trim())); 
//				sw.WriteLine(info,trimProduct(txtProdName12.Value.ToString()+" "+txtPack12.Value.Trim()) ,txtRate12.Text,txtQty12.Text,GenUtil.strNumericFormat(txtAmount12.Text.ToString().Trim())); 
				
				if(!txtProdsch1.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch1.Text.ToString()+" "+txtPacksch1.Text.Trim()) ,"",txtQtysch1.Text,"" );
				if(!txtProdsch2.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch2.Text+" "+txtPacksch2.Text.Trim()) ,"",txtQtysch2.Text,"" );
				if(!txtProdsch3.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch3.Text+" "+txtPacksch3.Text.Trim()) ,"",txtQtysch3.Text,"" );
				if(!txtProdsch4.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch4.Text+" "+txtPacksch4.Text.Trim()),"" ,txtQtysch4.Text,"" );
				if(!txtProdsch5.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch5.Text+" "+txtPacksch5.Text.Trim()),"",txtQtysch5.Text,"" );
				if(!txtProdsch6.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch6.Text+" "+txtPacksch6.Text.Trim()),"",txtQtysch6.Text,"" );
				if(!txtProdsch7.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch7.Text+" "+txtPacksch7.Text.Trim()) ,"",txtQtysch7.Text,"" );
				if(!txtProdsch8.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch8.Text+" "+txtPacksch8.Text.Trim()) ,"",txtQtysch8.Text,"" );
				if(!txtProdsch9.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch9.Text+" "+txtPacksch9.Text.Trim()) ,"",txtQtysch9.Text,"" );
				if(!txtProdsch10.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch10.Text+" "+txtPacksch10.Text.Trim()) ,"",txtQtysch10.Text,"" );
				if(!txtProdsch11.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch11.Text+" "+txtPacksch11.Text.Trim()) ,"",txtQtysch11.Text,"" );
				if(!txtProdsch12.Text.Equals(""))
					sw.WriteLine(info,trimProduct(txtProdsch12.Text+" "+txtPacksch12.Text.Trim()) ,"",txtQtysch12.Text,"" );
				//*********************
				for (int i = 0; i < System.Convert.ToInt32(skip2 * 10); ++i)
				{
					sw.WriteLine();
				}

				sw.WriteLine(info,"" ,"","",GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()) );
				//sw.WriteLine(blank.Substring(1,t) + GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()) );
				// back to normal
				sw.Write((char)27);
				sw.Write((char)51);
				sw.Write((char)10);
				//deselect condensed
				sw.Write((char)18);
				sw.Write((char)12);
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:prePrint().  EXCEPTION: "+ ex.Message+"  user "+uid);
			}
		}*/

        /// <summary>
        /// This method are not used.
        /// </summary>
        public void prePrint1()
        {
            try
            {
                //Response.Write(txtAvStock1.Text);  
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesInvoicePrePrintReport.txt";
                //string info = "";
                string strInvNo = "";
                string strDiscType = "";
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("           =============");
                sw.WriteLine("           SALES INVOICE");
                sw.WriteLine("           =============");
                if (lblInvoiceNo.Visible == true)
                    strInvNo = lblInvoiceNo.Text;
                else
                    strInvNo = dropInvoiceNo.SelectedItem.Value;

                sw.WriteLine(" Invoice No   : " + strInvNo);
                //Mahesh11.04.007 
                sw.WriteLine(" Date         : " + lblInvoiceDate.Text.ToString());
                //sw.WriteLine(" Date         : " + Session["CurrentDate"].ToString());
                //sw.WriteLine(" Customer     : " + DropCustName.SelectedItem.Text);
                //sw.WriteLine(" Customer     : " + text1.Value);

                string txtval = text1.Value.Substring(0, text1.Value.IndexOf(":"));// add by vikas 1.05.09

                sw.WriteLine(" Customer     : " + txtval);
                sw.WriteLine(" Place        : " + lblPlace.Value);
                sw.WriteLine(" Due Date     : " + lblDueDate.Value);
                sw.WriteLine(" Current Bal. : " + lblCurrBalance.Value);
                sw.WriteLine(" Vehicle No.  : " + txtVehicleNo.Text);
                sw.WriteLine("+---------------+------+------+--------+");
                sw.WriteLine("|Product        |Qty.  |Rate  | Amount |");
                sw.WriteLine("+---------------+------+------+--------+");
                //info = " {0,-30:S} {1,10:F}  {2,10:F} {3,10:F}";
                //info = " {0,-15:S} {1,6:F} {2,6:F} {3,9:F}";
                /*sw.WriteLine(info,trimProduct(txtProdName1.Value.ToString())  ,txtQty1.Text,txtRate1.Text,GenUtil.strNumericFormat(txtAmount1.Text.ToString().Trim()) );
				sw.WriteLine(info,trimProduct(txtProdName2.Value.ToString()) ,txtQty2.Text,txtRate2.Text,GenUtil.strNumericFormat(txtAmount2.Text.ToString().Trim()));
				sw.WriteLine(info,trimProduct(txtProdName3.Value.ToString()) ,txtQty3.Text,txtRate3.Text,GenUtil.strNumericFormat(txtAmount3.Text.ToString().Trim()));
				sw.WriteLine(info,trimProduct(txtProdName4.Value.ToString()) ,txtQty4.Text,txtRate4.Text,GenUtil.strNumericFormat(txtAmount4.Text.ToString().Trim()));
				sw.WriteLine(info,trimProduct(txtProdName5.Value.ToString()) ,txtQty5.Text,txtRate5.Text,GenUtil.strNumericFormat(txtAmount5.Text.ToString().Trim()));
				sw.WriteLine(info,trimProduct(txtProdName6.Value.ToString()) ,txtQty6.Text,txtRate6.Text,GenUtil.strNumericFormat(txtAmount6.Text.ToString().Trim()));
				sw.WriteLine(info,trimProduct(txtProdName7.Value.ToString()) ,txtQty7.Text,txtRate7.Text,GenUtil.strNumericFormat(txtAmount7.Text.ToString().Trim()));
				sw.WriteLine(info,trimProduct(txtProdName8.Value.ToString()) ,txtQty8.Text,txtRate8.Text,GenUtil.strNumericFormat(txtAmount8.Text.ToString().Trim()));*/
                sw.WriteLine("+---------------+------+------+--------+");
                sw.WriteLine("           Grand Total      : {0,10:F}", GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()));
                double disc_amt = 0;
                string msg = "";
                if (txtDisc.Text == "")
                {
                    strDiscType = "";
                    msg = "";
                    //disc_amt ="";
                }
                else
                {
                    disc_amt = System.Convert.ToDouble(txtDisc.Text.ToString());
                    strDiscType = DropDiscType.SelectedItem.Text;
                    if (strDiscType.Trim().Equals("%"))
                    {
                        double temp = 0;
                        if (txtGrandTotal.Text.Trim() != "")
                            temp = System.Convert.ToDouble(Request.Form["txtGrandTotal"].ToString());

                        disc_amt = (temp * disc_amt / 100);
                        msg = "(" + txtDisc.Text.ToString() + strDiscType + ")";
                    }
                    else
                    {
                        msg = "(" + strDiscType + ")";
                    }
                }
                sw.WriteLine("           Discount{0,-8:S} : {1,10:S}", msg, GenUtil.strNumericFormat(disc_amt.ToString()));
                sw.WriteLine("           Net Amount       : {0,10:F}", GenUtil.strNumericFormat(txtNetAmount.Text.ToString()));
                sw.WriteLine("+--------------------------------------+");
                string promo = txtPromoScheme.Text.Trim();
                if (promo.Length > 25)
                    promo = promo.Substring(0, 25);
                sw.WriteLine("Promo Scheme : " + promo);
                string remark = txtRemark.Text.Trim();
                if (remark.Length > 25)
                    remark = remark.Substring(0, 25);
                sw.WriteLine("Remarks      : " + remark);
                string message = txtMessage.Text.Trim();
                if (message.Length > 25)
                    message = message.Substring(0, 25);
                sw.WriteLine("Message      : " + message);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("                           Signature");
                sw.Close();
                //insert();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : prePrint1() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method are not used.
        /// </summary>
        public void reportmaking44()  // To be removed
        {
            try
            {
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesInvoiceReport.txt";
                StreamWriter sw = new StreamWriter(path);
                System.Data.SqlClient.SqlDataReader rdr = null;
                string sql = "";
                string str1 = "";
                string str4 = "";
                string str6 = "";
                string str8 = "";
                string str10 = "";
                string str12 = "";
                string str14 = "";
                string str16 = "";
                string str18 = "";
                string str20 = "";
                string str22 = "";
                string str24 = "";
                string str26 = "";
                string str28 = "";
                string str30 = "";
                string str32 = "";
                string str34 = "";
                string str36 = "";
                string str38 = "";
                string str40 = "";
                string str42 = "";
                string str44 = "";
                string str46 = "";
                string str48 = "";
                string str50 = "";
                string str52 = "";
                string str54 = "";
                string str56 = "";
                string str58 = "";
                string str60 = "";
                string str62 = "";
                string str64 = "";
                string str66 = "";
                string str68 = "";
                string str70 = "";
                string str72 = "";
                string str74 = "";
                string str76 = "";
                string str78 = "";
                string str80 = "";
                string str82 = "";
                sql = "select InvoiceNo,ToDate,CustomerName,Place,DueDate,CurrentBalance,VehicleNo,Prod1,Prod2,Prod3,Prod4,Prod5,Prod6,Prod7,Prod8,Qty1,Qty2,Qty3,Qty4,Qty5,Qty6,Qty7,Qty8,Rate1,Rate2,Rate3,Rate4,Rate5,Rate6,Rate7,Rate8,Amt1,Amt2,Amt3,Amt4,Amt5,Amt6,Amt7,Amt8,Total,Promo,Remarks from Salesinv  where InvoiceNo=(Select Max(InvoiceNo) from SalesInv)";
                dbobj.SelectQuery(sql, ref rdr);
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str1 = rdr["InvoiceNo"].ToString();
                }
                string str2 = System.DateTime.Now.Day + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Year;
                string str3 = "Invoice No : " + str1 + "             Date : " + str2;
                Write2File(sw, "               ==================== ");
                Write2File(sw, "                   SALES REPORT     ");
                Write2File(sw, "               ==================== ");
                Write2File(sw, str3);
                Write2File(sw, "--------------------------------------------");
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str4 = rdr["CustomerName"].ToString();
                }
                string str5 = "Customer Name      : " + str4;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str6 = rdr["Place"].ToString();
                }
                string str7 = "Place              :  " + str6;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str8 = rdr["DueDate"].ToString();
                }
                string str9 = "Due Date           :  " + str8;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str10 = rdr["CurrentBalance"].ToString();
                }
                string str11 = "Current Balance    :  " + str10;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str12 = rdr["VehicleNo"].ToString();
                }
                string str13 = "Vehicle No         :  " + str12;
                //  for Product
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str14 = rdr["Prod1"].ToString();
                }
                string str15 = str14;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str16 = rdr["Prod2"].ToString();
                }
                string str17 = str16;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str18 = rdr["Prod3"].ToString();
                }
                string str19 = str18;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str20 = rdr["Prod4"].ToString();
                }
                string str21 = str20;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str22 = rdr["Prod5"].ToString();
                }
                string str23 = str22;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str24 = rdr["Prod6"].ToString();
                }
                string str25 = str24;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str26 = rdr["Prod7"].ToString();
                }
                string str27 = str26;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str28 = rdr["Prod8"].ToString();
                }
                string str29 = str28;
                //for Qty
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str30 = rdr["Qty1"].ToString();
                }
                string str31 = str30;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str32 = rdr["Qty2"].ToString();
                }
                string str33 = str32;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str34 = rdr["Qty3"].ToString();
                }
                string str35 = str34;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str36 = rdr["Qty4"].ToString();
                }
                string str37 = str36;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str38 = rdr["Qty5"].ToString();
                }
                string str39 = str38;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str40 = rdr["Qty6"].ToString();
                }
                string str41 = str40;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str42 = rdr["Qty7"].ToString();
                }
                string str43 = str42;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str44 = rdr["Qty8"].ToString();
                }
                //for Rate
                string str45 = str44;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str46 = rdr["Rate1"].ToString();
                }
                string str47 = str46;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str48 = rdr["Rate2"].ToString();
                }
                string str49 = str48;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str50 = rdr["Rate3"].ToString();
                }
                string str51 = str50;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str52 = rdr["Rate4"].ToString();
                }
                string str53 = str52;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str54 = rdr["Rate5"].ToString();
                }
                string str55 = str54;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str56 = rdr["Rate6"].ToString();
                }
                string str57 = str56;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str58 = rdr["Rate7"].ToString();
                }
                string str59 = str58;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str60 = rdr["Rate8"].ToString();
                }
                string str61 = str60;
                //for Amount
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str62 = rdr["Amt1"].ToString();
                }
                string str63 = str62;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str64 = rdr["Amt2"].ToString();
                }
                string str65 = str64;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str66 = rdr["Amt3"].ToString();
                }
                string str67 = str66;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str68 = rdr["Amt4"].ToString();
                }
                string str69 = str68;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str70 = rdr["Amt5"].ToString();
                }
                string str71 = str70;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str72 = rdr["Amt6"].ToString();
                }
                string str73 = str72;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str74 = rdr["Amt7"].ToString();
                }
                string str75 = str74;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str76 = rdr["Amt8"].ToString();
                }
                string str77 = str76;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str78 = rdr["Total"].ToString();
                }
                string str79 = str78;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str80 = rdr["Promo"].ToString();
                }
                string str81 = str80;
                dbobj.SelectQuery(sql, ref rdr);
                if (rdr.Read())
                {
                    str82 = rdr["Remarks"].ToString();
                }
                string str83 = str80;

                Write2File(sw, str5);
                Write2File(sw, str7);
                Write2File(sw, str9);
                Write2File(sw, str11);
                Write2File(sw, str13);

                Write2File(sw, "--------------------------------------------------");
                Write2File(sw, "Prod             Qty   Rate     Amt");
                Write2File(sw, "--------------------------------------------------");

                // For product 
                if (str15.Length < 16)
                {
                    str15 = str15 + MakeString(16 - str15.Length);
                }
                if (str17.Length < 16)
                {
                    str17 = str17 + MakeString(16 - str17.Length);
                }
                if (str19.Length < 16)
                {
                    str19 = str19 + MakeString(16 - str19.Length);
                }
                if (str21.Length < 16)
                {
                    str21 = str21 + MakeString(16 - str21.Length);
                }
                if (str23.Length < 16)
                {
                    str23 = str23 + MakeString(16 - str23.Length);
                }
                if (str25.Length < 16)
                {
                    str25 = str25 + MakeString(16 - str25.Length);
                }
                if (str27.Length < 16)
                {
                    str27 = str27 + MakeString(16 - str27.Length);
                }
                if (str29.Length < 16)
                {
                    str29 = str29 + MakeString(16 - str29.Length);
                }
                //end product
                // for Qty
                if (str31.Length < 6)
                {
                    str31 = str31 + MakeString(6 - str31.Length);
                }
                if (str33.Length < 6)
                {
                    str33 = str33 + MakeString(6 - str33.Length);
                }
                if (str35.Length < 6)
                {
                    str35 = str35 + MakeString(6 - str35.Length);
                }
                if (str35.Length < 6)
                {
                    str35 = str35 + MakeString(6 - str35.Length);
                }
                if (str37.Length < 6)
                {
                    str37 = str37 + MakeString(6 - str37.Length);
                }
                if (str39.Length < 6)
                {
                    str39 = str39 + MakeString(6 - str39.Length);
                }
                if (str41.Length < 6)
                {
                    str41 = str41 + MakeString(6 - str41.Length);
                }
                if (str43.Length < 6)
                {
                    str43 = str43 + MakeString(6 - str43.Length);
                }
                if (str45.Length < 6)
                {
                    str45 = str45 + MakeString(6 - str45.Length);
                }
                // End Qty
                // for rate
                if (str47.Length < 5)
                {
                    str47 = str47 + MakeString(5 - str47.Length);
                }
                if (str49.Length < 5)
                {
                    str49 = str49 + MakeString(5 - str49.Length);
                }
                if (str51.Length < 5)
                {
                    str51 = str51 + MakeString(5 - str51.Length);
                }
                if (str53.Length < 5)
                {
                    str53 = str53 + MakeString(5 - str53.Length);
                }
                if (str55.Length < 5)
                {
                    str55 = str55 + MakeString(5 - str55.Length);
                }
                if (str57.Length < 5)
                {
                    str57 = str57 + MakeString(5 - str57.Length);
                }
                if (str59.Length < 5)
                {
                    str59 = str59 + MakeString(5 - str59.Length);
                }
                if (str61.Length < 5)
                {
                    str61 = str61 + MakeString(5 - str61.Length);
                }
                //end 
                // for amt
                if (str63.Length < 5)
                {
                    str63 = str63 + MakeString(5 - str63.Length);
                }
                if (str65.Length < 5)
                {
                    str65 = str65 + MakeString(5 - str65.Length);
                }
                if (str67.Length < 5)
                {
                    str67 = str67 + MakeString(5 - str67.Length);
                }
                if (str69.Length < 5)
                {
                    str69 = str69 + MakeString(5 - str69.Length);
                }
                if (str71.Length < 5)
                {
                    str71 = str71 + MakeString(5 - str71.Length);
                }
                if (str73.Length < 5)
                {
                    str73 = str73 + MakeString(5 - str73.Length);
                }
                if (str75.Length < 5)
                {
                    str75 = str75 + MakeString(5 - str75.Length);
                }
                if (str77.Length < 5)
                {
                    str77 = str77 + MakeString(5 - str77.Length);
                }
                //end amt
                string gap = "  ";
                Write2File(sw, str15 + gap + str31 + gap + str47 + gap + str63);
                Write2File(sw, str17 + gap + str33 + gap + str49 + gap + str65);
                Write2File(sw, str19 + gap + str35 + gap + str51 + gap + str67);
                Write2File(sw, str21 + gap + str37 + gap + str53 + gap + str69);
                Write2File(sw, str23 + gap + str39 + gap + str55 + gap + str71);
                Write2File(sw, str25 + gap + str41 + gap + str57 + gap + str73);
                Write2File(sw, str27 + gap + str43 + gap + str59 + gap + str75);
                Write2File(sw, str29 + gap + str45 + gap + str61 + gap + str77);
                Write2File(sw, "------------------------------------------------");
                Write2File(sw, "	           Total :      " + str79);
                Write2File(sw, "- -----------------------------------------------");
                Write2File(sw, "                                                                                           ");
                Write2File(sw, "Promo Scheme :" + str81);
                Write2File(sw, "                                                                                           ");
                Write2File(sw, "Remarks      :" + str83);
                Write2File(sw, "                                                                                           ");
                Write2File(sw, "                                                                                           ");
                Write2File(sw, "                                                                                           ");
                Write2File(sw, "                          Signature");
                sw.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : reportmaking44() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// Its calls the save_updateInvoice() fucntion to save or update invoice details and calls the prePrint() and Print() fucntion to create and print the SalesInvoicePrePrintReport.txt file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (FlagPrint == false)
                {
                    /***********Add by vikas 15.07.09 ***********************************/
                    if (btnEdit.Visible == true)
                    {
                        if (DropSalesType.SelectedItem.Text.Equals("Cash"))
                        {
                            double net = 0;
                            string str = lblCurrBalance.Value.ToString();
                            string[] str1 = str.Split(new char[] { ' ' }, str.Length);
                            if (str1[1].Equals("Cr."))
                            {
                                net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString());
                            }
                            else
                            {
                                MessageBox.Show("Current Balance is less than Net Amount");
                                return;
                            }

                            double cr = System.Convert.ToDouble(str1[0].ToString());
                            if (cr > net)
                            {
                                save_updateInvoive();            //Add by Vikas 15.07..09
                            }
                            else
                            {
                                MessageBox.Show("Current Balance is less than Net Amount");
                                return;
                            }
                        }
                        else if (DropSalesType.SelectedItem.Text.Equals("Credit"))
                        {
                            double net;
                            string s = TxtCrLimit.Value.ToString();
                            string s1 = TxtCrLimit1.Text.ToString();
                            double cr = System.Convert.ToDouble(TxtCrLimit.Value.ToString());
                            string str = lblCurrBalance.Value.ToString();
                            string[] str1 = str.Split(new char[] { ' ' }, str.Length);
                            if (str1[1].Equals("Cr."))
                                net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString()) - System.Convert.ToDouble(str1[0].ToString());
                            else
                                //net=System.Convert.ToDouble(txtNetAmount.Text.ToString())+System.Convert.ToDouble(str1[0].ToString());
                                net = System.Convert.ToDouble(Request.Form["txtNetAmount"].ToString());
                            if (cr >= net)
                            {
                                save_updateInvoive();
                            }
                            else
                            {
                                MessageBox.Show("Credit Limit is less than Net Amount");
                                return;
                            }

                        }
                        else if (DropSalesType.SelectedItem.Text.Equals("Van"))      //Add by vikas 15.07.09
                        {
                            if (txtChallanNo.Text != "")
                            {
                                save_updateInvoive();
                            }
                            else
                            {
                                MessageBox.Show("Challan No can not be blank");
                                return;
                            }
                        }
                    }
                    else
                    {
                        save_updateInvoive();
                    }
                }
                else
                {
                    Button1.CausesValidation = true;
                    FlagPrint = false;
                }
                tempEdit.Value = "True";           //Add by vikas 14.07.09
                if (flag == 0)
                {
                    string home_drive = Environment.SystemDirectory;
                    home_drive = home_drive.Substring(0, 2);
                    //print(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SalesInvoiceReport.txt");
                    print(home_drive + "\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SalesInvoicePrePrintReport1.txt");
                    Clear();
                    clear1();
                    CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:Button1_Click - InvoiceNo : " + FromDate + ToDate + lblInvoiceNo.Text);
                    GetNextInvoiceNo();
                    GetProducts();
                    getscheme();
                    FetchData();
                    lblInvoiceNo.Visible = true;
                    dropInvoiceNo.Visible = false;
                    btnEdit.Visible = true;
                    Button1.Enabled = true;
                }
                else
                {
                    flag = 0;
                    return;
                }
                GetOrderInvoice();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:Button1_Click  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        private void TxtCrLimit_TextChanged(object sender, System.EventArgs e)
        {
        }
        private void DropUnderSalesMan_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void TextBox3_TextChanged(object sender, System.EventArgs e)
        {
        }
        private void txtTempQty5_TextChanged(object sender, System.EventArgs e)
        {
        }
        private void tmpQty1_ServerChange(object sender, System.EventArgs e)
        {
        }
        private void TextBox4_TextChanged(object sender, System.EventArgs e)
        {
        }
        private void txtmwid2_TextChanged(object sender, System.EventArgs e)
        {
        }
        private void txtmwid5_TextChanged(object sender, System.EventArgs e)
        {
        }

        private void txtPacksch2_TextChanged(object sender, System.EventArgs e)
        {
        }

        protected void Yes_CheckedChanged(object sender, System.EventArgs e)
        {
        }

        protected void No_CheckedChanged(object sender, System.EventArgs e)
        {
        }

        protected void txtVAT_TextChanged(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// This method update the products qty before sales in edit time.
        /// </summary>
        public void UpdateProductQty()
        {
            SalesModel sales = new SalesModel();

            List<string> ProductType1 = new List<string>();
            List<string> ProductName1 = new List<string>();
            List<string> ProductQty1 = new List<string>();
            List<string> ProductPack1 = new List<string>();

            List<string> SchProductType1 = new List<string>();
            List<string> SchProductName1 = new List<string>();
            List<string> SchProductPack1 = new List<string>();
            List<string> SchProductQty1 = new List<string>();

            try
            {
                sales.Invoice_Date = Request.Form["tempInvoiceDate"].ToString();
                for (int i = 0; i < ProductType.Length; i++)
                {
                    if (ProductType[i].ToString() != "")
                    {
                        ProductType1.Add(ProductType[i].ToString());
                        ProductName1.Add(ProductName[i].ToString());
                        ProductQty1.Add(ProductQty[i].ToString());
                        ProductPack1.Add(ProductPack[i].ToString());
                    }
                }

                for (int i = 0; i < SchProductType.Length; i++)
                {
                    if (SchProductType[i].ToString() != "")
                    {
                        SchProductType1.Add(SchProductType[i].ToString());
                        SchProductName1.Add(SchProductName[i].ToString());
                        SchProductPack1.Add(SchProductPack[i].ToString());
                        SchProductQty1.Add(SchProductQty[i].ToString());
                    }
                }

                if (ProductType1.Count > 0)
                    sales.ProductType = ProductType1;

                if (ProductPack1.Count > 0)
                    sales.ProductPack = ProductPack1;

                if (ProductName1.Count > 0)
                    sales.ProductName = ProductName1;

                if (ProductQty1.Count > 0)
                    sales.ProductQty = ProductQty1;

                if (SchProductType1.Count > 0)
                    sales.SchProductType = SchProductType1;

                if (SchProductName1.Count > 0)
                    sales.SchProductName = SchProductName1;

                if (SchProductPack1.Count > 0)
                    sales.SchProductPack = SchProductPack1;

                if (SchProductQty1.Count > 0)
                    sales.SchProductQty = SchProductQty1;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/UpdateProductQty", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : UpdateProductQty() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method is used to update the product sale qty according to batch no in edit time.
        /// </summary>
        public void UpdateBatchNo()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(dropInvoiceNo.SelectedItem.Text);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/UpdateBatchNo?invoiceNo=" + dropInvoiceNo.SelectedItem.Text, byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                //            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                //InventoryClass obj = new InventoryClass();
                //SqlDataReader rdr;
                //SqlCommand cmd;
                ////coment by vikas 18.06.09 rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"'");
                //rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Sales Invoice'");
                //while(rdr.Read())
                //{
                //	//******************************
                //	string s="update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'";
                //	Con.Open();
                //	cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
                //	//cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"' and stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["tempInvoiceDate"].ToString()) + "',103)",Con);
                //	cmd.ExecuteNonQuery();
                //	cmd.Dispose();
                //	Con.Close();

                //	/*******Add by vikas 19.06.09**********************/
                //	Con.Open();
                //	cmd = new SqlCommand("update BatchNo set Qty=Qty+"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
                //	cmd.ExecuteNonQuery();
                //	cmd.Dispose();
                //	Con.Close();
                //	/************************************************/
                //}
                //rdr.Close();
                //Con.Open();
                //cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='"+dropInvoiceNo.SelectedItem.Text+"' and Trans_Type='Sales Invoice'",Con);
                //cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //Con.Close();

            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : UpdateBatchNo() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
		/// This method is used to update the product stock after sales in edit time.
		/// </summary>
		public void SeqStockMaster(SalesModel sales)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                var myContent = JsonConvert.SerializeObject(sales);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/sales/SeqStockMaster", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        /// <summary>
        /// This method is used to update the product stock after sales in edit time.
        /// </summary>
        public void SeqStockMaster()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                SqlCommand cmd;
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                SqlDataReader rdr1 = null, rdr = null;
                for (int i = 0; i < ProductType.Length; i++)
                {
                    if (ProductType[i] == "" || ProductName[i] == "" || ProductQty[i] == "")
                        continue;
                    else
                    {
                        //					InventoryClass obj = new InventoryClass();
                        //					InventoryClass obj1 = new InventoryClass();
                        //					SqlCommand cmd;
                        //					SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                        //					SqlDataReader rdr1=null,rdr=null;
                        string str = "select Prod_ID from Products where Category='" + ProductType[i].ToString() + "' and Prod_Name='" + ProductName[i].ToString() + "' and Pack_Type='" + ProductPack[i].ToString() + "'";
                        rdr = obj.GetRecordSet(str);
                        if (rdr.Read())
                        //for(int i=1001;i<=1070;i++)//add
                        {
                            string str1 = "select * from Stock_Master where Productid='" + rdr["Prod_ID"].ToString() + "' order by Stock_date";
                            //string str1="select * from Stock_Master where Productid='"+i.ToString()+"' order by Stock_date";//add
                            rdr1 = obj1.GetRecordSet(str1);
                            double OS = 0, CS = 0, k = 0;
                            while (rdr1.Read())
                            {
                                if (k == 0)
                                {
                                    OS = double.Parse(rdr1["opening_stock"].ToString());
                                    k++;
                                }
                                else
                                    OS = CS;
                                //CS=OS+double.Parse(rdr1["receipt"].ToString())-double.Parse(rdr1["sales"].ToString());
                                CS = OS + double.Parse(rdr1["receipt"].ToString()) - (double.Parse(rdr1["sales"].ToString()) + double.Parse(rdr1["salesfoc"].ToString()));
                                Con.Open();
                                cmd = new SqlCommand("update Stock_Master set opening_stock='" + OS.ToString() + "', Closing_Stock='" + CS.ToString() + "' where ProductID='" + rdr1["Productid"].ToString() + "' and Stock_Date='" + GenUtil.str2MMDDYYYY(rdr1["stock_date"].ToString()) + "'", Con);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                Con.Close();
                            }
                            rdr1.Close();
                        }
                        rdr.Close();
                    }
                    //*******************
                    if (SchProductType[i] == "" || SchProductName[i] == "" || SchProductQty[i] == "")
                        continue;
                    else
                    {

                        string str = "select Prod_ID from Products where Category='" + SchProductType[i].ToString() + "' and Prod_Name='" + SchProductName[i].ToString() + "' and Pack_Type='" + SchProductPack[i].ToString() + "'";
                        rdr = obj.GetRecordSet(str);
                        if (rdr.Read())
                        //for(int i=1001;i<=1070;i++)//add
                        {
                            string str1 = "select * from Stock_Master where Productid='" + rdr["Prod_ID"].ToString() + "' order by Stock_date";
                            //string str1="select * from Stock_Master where Productid='"+i.ToString()+"' order by Stock_date";//add
                            rdr1 = obj1.GetRecordSet(str1);
                            double OS = 0, CS = 0, k = 0;
                            while (rdr1.Read())
                            {
                                if (k == 0)
                                {
                                    OS = double.Parse(rdr1["opening_stock"].ToString());
                                    k++;
                                }
                                else
                                    OS = CS;
                                //CS=OS+double.Parse(rdr1["receipt"].ToString())-double.Parse(rdr1["sales"].ToString());
                                CS = OS + double.Parse(rdr1["receipt"].ToString()) - (double.Parse(rdr1["sales"].ToString()) + double.Parse(rdr1["salesfoc"].ToString()));
                                Con.Open();
                                cmd = new SqlCommand("update Stock_Master set opening_stock='" + OS.ToString() + "', Closing_Stock='" + CS.ToString() + "' where ProductID='" + rdr1["Productid"].ToString() + "' and Stock_Date='" + GenUtil.str2MMDDYYYY(rdr1["stock_date"].ToString()) + "'", Con);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                Con.Close();
                            }
                            rdr1.Close();
                        }
                        rdr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : SeqStockMaster() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        //ArrayList BatchInfo = new ArrayList();
        /*public void InsertBatchNo(string Prod,string PackType,string Qty)
		{
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass();
			DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr1 = null;
			int batch_id=0,SNo=0;
			//rdr1 = obj1.GetRecordSet("select max(Batch_ID) from Batch_Transaction");
			rdr1 = obj1.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
			if(rdr1.Read())
			{
				if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
					//batch_id=int.Parse(rdr1.GetValue(0).ToString());
					SNo=int.Parse(rdr1.GetValue(0).ToString());
				else
					SNo=1;
			}
			else
				//batch_id=1;
				SNo=1;
			rdr1.Close();
			SqlDataReader rdr = obj.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod+"' and packtype='"+PackType+"') order by Date");
			//int count=int.Parse(TextBox2.Text);
			int count=int.Parse(Qty);
			int k=0;
			double cl_sk=0;
			while(rdr.Read())
			{
				for(int i=1;i<=10;i++)
				{
					int flag=0,x=0;
					if(count>0)
					{
						//**string name=rdr["bat"+i].ToString();
						//**if(int.Parse(rdr["qty"+i].ToString())>0)
						if(int.Parse(rdr["qty"].ToString())>0)
						{
							System.Threading.Thread.Sleep(1000);
							dbobj1.SelectQuery("select Closing_Stock from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"' order by trans_date desc",ref rdr1);
							if(rdr1.Read())
							{
								if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
									cl_sk=double.Parse(rdr1.GetValue(0).ToString());
								else
									cl_sk=0;
							}
							else
								cl_sk=0;
							if(count<=int.Parse(rdr["qty"+i].ToString()))
							{
								//count=int.Parse(rdr["qty"+i].ToString())-count;
								cl_sk-=count;
								//**dbobj1.Insert_or_Update("update batchno set qty"+i+"=qty"+i+"-"+count+",total_qty=total_qty-"+count+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"'",ref x);
								dbobj1.Insert_or_Update("update batchno set qty=qty-"+count+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
								if(lblInvoiceNo.Visible==true)
								{
									//**dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+lblInvoiceNo.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+i].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
									dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice','"+lblInvoiceDate.Text+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
									//BatchInfo.Add(rdr["Prod_ID"].ToString()+","+rdr["Bat"+i].ToString()+","+count);
								}
								else
								{
									//**dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+i].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
									dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice','"+lblInvoiceDate.Text+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
									//BatchInfo.Add(rdr["Prod_ID"].ToString()+","+rdr["Bat"+i].ToString()+","+count);
								}
								count=0;
								break;
							}
							else
							{
//								dbobj1.SelectQuery("select max(Closing_Stock) from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"'",ref rdr1);
//								if(rdr1.Read())
//									cl_sk=double.Parse(rdr1.GetValue(0).ToString());
//								else
//									cl_sk=0;
								//**cl_sk-=double.Parse(rdr["qty"+i].ToString());
								cl_sk-=double.Parse(rdr["qty"].ToString());
								//**dbobj1.Insert_or_Update("update batchno set qty"+i+"=0,total_qty=total_qty-qty"+i+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"'",ref x);
								dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
								if(lblInvoiceNo.Visible==true)
								{
									//**dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+lblInvoiceNo.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+i].ToString()+"','"+rdr["qty"+i].ToString()+"',"+cl_sk.ToString()+")",ref x);
									dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice','"+lblInvoiceDate.Text+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
									//BatchInfo.Add(rdr["Prod_ID"].ToString()+","+rdr["Bat"+i].ToString()+","+rdr["qty"+i].ToString());
								}
								else
								{
									//dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+i].ToString()+"','"+rdr["qty"+i].ToString()+"',"+cl_sk.ToString()+")",ref x);
									dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice','"+lblInvoiceDate.Text+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
									//BatchInfo.Add(rdr["Prod_ID"].ToString()+","+rdr["Bat"+i].ToString()+","+rdr["qty"+i].ToString());
								}
								//count-=int.Parse(rdr["qty"+i].ToString());
								count-=int.Parse(rdr["qty"].ToString());
							}
						}
						k=i+1;
						if(count>0)
						{
							for(int j=k;j<=10;j++)
							{
								if(rdr["bat"+j].ToString()!="")
								{
									if(name==rdr["bat"+j].ToString())
									{
										if(int.Parse(rdr["Qty"+j].ToString())>0)
										{
											System.Threading.Thread.Sleep(1000);
											dbobj1.SelectQuery("select Closing_Stock from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"' order by trans_date desc",ref rdr1);
											if(rdr1.Read())
											{
												if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
													cl_sk=double.Parse(rdr1.GetValue(0).ToString());
												else
													cl_sk=0;
											}
											else
												cl_sk=0;
											//if(count<=int.Parse(rdr["qty"+j].ToString()))
											if(count<=int.Parse(rdr["qty"].ToString()))
											{
												cl_sk-=count;
												dbobj1.Insert_or_Update("update batchno set qty"+j+"=qty"+j+"-"+count+",total_qty=total_qty-"+count+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"'",ref x);
												if(lblInvoiceNo.Visible==true)
													dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+lblInvoiceNo.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+j].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
												else
													dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+j].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
												count=0;
												flag=1;
												break;
											}
											else
											{
//												dbobj1.SelectQuery("select max(Closing_Stock) from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"'",ref rdr1);
//												if(rdr1.Read())
//													cl_sk=double.Parse(rdr1.GetValue(0).ToString());
//												else
//													cl_sk=0;
												cl_sk-=double.Parse(rdr["qty"+i].ToString());
												dbobj1.Insert_or_Update("update batchno set qty"+j+"=0,total_qty=total_qty-qty"+j+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"'",ref x);
												if(lblInvoiceNo.Visible==true)
													dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+lblInvoiceNo.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+j].ToString()+"','"+rdr["qty"+j].ToString()+"',"+cl_sk.ToString()+")",ref x);
												else
													dbobj1.Insert_or_Update("insert into batch_transaction values("+(++batch_id)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Bat"+j].ToString()+"','"+rdr["qty"+j].ToString()+"',"+cl_sk.ToString()+")",ref x);
												count-=int.Parse(rdr["qty"+j].ToString());
											}
										}
									}
								}
								else
								{
									rdr1 = obj1.GetRecordSet("select total_qty from batchno where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"'");
									if(rdr1.Read())
									{
										string s=rdr1["total_qty"].ToString();
										if(int.Parse(rdr1["total_qty"].ToString())==0)
											flag=1;
									}
									rdr1.Close();
									break;
								}
							}
							if(flag==1)
								break;
						}
					}
				}
			}
			rdr.Close();
		}*/

        //		public void InsertBatchNo(string Prod,string PackType,string Qty)
        //		{
        //			InventoryClass obj = new InventoryClass();
        //			InventoryClass obj1 = new InventoryClass();
        //			DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
        //			SqlDataReader rdr1 = null;
        //			int SNo=0;
        //			rdr1 = obj1.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
        //			if(rdr1.Read())
        //			{
        //				if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
        //					SNo=int.Parse(rdr1.GetValue(0).ToString());
        //				else
        //					SNo=1;
        //			}
        //			else
        //				SNo=1;
        //			rdr1.Close();
        //			SqlDataReader rdr = obj.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by Batch_ID");
        //			int count=0;
        //			if(Qty!="")
        //				count=int.Parse(Qty);
        //			int x=0;
        //			double cl_sk=0;
        //			while(rdr.Read())
        //			{
        //				if(double.Parse(rdr["qty"].ToString())>0)
        //				{
        //					dbobj1.SelectQuery("select top 1 Closing_Stock from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"' order by sno desc",ref rdr1);
        //					if(rdr1.Read())
        //					{
        //						if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
        //							cl_sk=double.Parse(rdr1.GetValue(0).ToString());
        //						else
        //							cl_sk=0;
        //					}
        //					else
        //						cl_sk=0;
        //					rdr1.Close();
        //				}
        //				if(count>0)
        //				{
        //					if(int.Parse(rdr["qty"].ToString())>0)
        //					{
        //						if(count<=int.Parse(rdr["qty"].ToString()))
        //						{
        //							cl_sk-=count;
        //							dbobj1.Insert_or_Update("update batchno set qty=qty-"+count+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
        //							dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
        //							if(lblInvoiceNo.Visible==true)
        //								dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
        //							else
        //								dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
        //							count=0;
        //							break;
        //						}
        //						else
        //						{
        //							cl_sk-=double.Parse(rdr["qty"].ToString());
        //							dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
        //							dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+double.Parse(rdr["qty"].ToString())+",closing_stock=closing_stock-"+double.Parse(rdr["qty"].ToString())+" where productid='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
        //							if(lblInvoiceNo.Visible==true)
        //								dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
        //							else
        //								dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
        //							count-=int.Parse(rdr["qty"].ToString());
        //						}
        //					}
        //				}
        //			}
        //			rdr.Close();
        //		}

        /// <summary>
        /// This method is used to save the batch information in other table.
        /// </summary>
        /// <param name="Prod"></param>
        /// <param name="PackType"></param>
        /// <param name="Qty"></param>
        public void InsertBatchNo(string Prod, string PackType, string Qty)
        {
            try
            {
                List<string> sales = new List<string>();

                sales.Add(Prod);
                sales.Add(PackType);
                sales.Add(Qty);

                //string strInvoiceNo = sales[3].ToString();
                //string dropStrInvoiceNo = sales[4].ToString();
                //string strInvoiceDate = sales[5].ToString();

                if (lblInvoiceNo.Visible == true)
                {
                    string strInvoiceNo = lblInvoiceNo.Text;
                    sales.Add(strInvoiceNo);
                    sales.Add("");
                }
                else
                {
                    string strInvoiceNo = dropInvoiceNo.SelectedItem.Text;
                    sales.Add("");
                    sales.Add(strInvoiceNo);
                }

                sales.Add(lblInvoiceDate.Text.Trim());

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/InsertBatchNo", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                //            InventoryClass obj = new InventoryClass();
                //InventoryClass obj1 = new InventoryClass();
                //DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
                //SqlDataReader rdr1 = null;
                //int SNo=0;
                //rdr1 = obj1.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
                //if(rdr1.Read())
                //{
                //	if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
                //		SNo=int.Parse(rdr1.GetValue(0).ToString());
                //	else
                //		SNo=1;
                //}
                //else
                //	SNo=1;
                //rdr1.Close();
                //int x=0;


                ////SqlDataReader rdr = obj.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by Batch_ID");
                ////SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by Batch_ID");
                //SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by stock_date");
                //int count=0;
                //if(Qty!="")
                //	count=int.Parse(Qty);

                //double cl_sk=0;
                //double cl_sk_New=0;
                //while(rdr.Read())
                //{
                //	if(double.Parse(rdr["closing_stock"].ToString())>0)
                //	{
                //		cl_sk=double.Parse(rdr["closing_stock"].ToString());
                //		cl_sk_New=double.Parse(rdr["closing_stock"].ToString());
                //	}
                //	else
                //		continue;
                //	if(count>0)
                //	{
                //		if(int.Parse(rdr["closing_stock"].ToString())>0)
                //		{
                //			if(count<=int.Parse(rdr["closing_stock"].ToString()))
                //			{
                //				cl_sk-=count;
                //				cl_sk_New-=count;
                //				/**********Add by vikas 15.09.09*********************************
                //				if(lblInvoiceNo.Visible!=true)
                //				{
                //					cl_sk_New+=count;
                //					dbobj1.Insert_or_Update("Delete from batch_transaction where trans_id="+dropInvoiceNo.SelectedItem.Text+" and trans_type='Sales Invoice'",ref x);	                                        //Add by vikas 15.09.09
                //				}
                //				**********End***************************************************/

                //				//07.07.09 dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

                //				dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103), sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);  

                //				/***************Add by vikas 15.09.09******************************
                //				if(lblInvoiceNo.Visible!=true)
                //				{
                //					dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103), sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);  
                //				}
                //				else
                //				{
                //					dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103), sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);  
                //				}
                //				********************************End*******************************/

                //				if(lblInvoiceNo.Visible==true)
                //					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103),'"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
                //				else
                //					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103),'"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	

                //				/***********add by vikas 19.06.09 *****************/
                //				dbobj1.Insert_or_Update("update batchno set qty=qty-"+count+" where prod_id='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
                //				/****************************/
                //				count=0;
                //				break;
                //			}
                //			else
                //			{
                //				cl_sk-=double.Parse(rdr["closing_stock"].ToString());

                //				//dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
                //				//07.07.09 dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+double.Parse(rdr["closing_stock"].ToString())+",closing_stock=closing_stock-"+double.Parse(rdr["closing_stock"].ToString())+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

                //				/**********Add by vikas 15.09.09*********************************
                //				if(lblInvoiceNo.Visible!=true)
                //				{
                //					cl_sk_New+=double.Parse(rdr["closing_stock"].ToString());
                //					dbobj1.Insert_or_Update("Delete from batch_transaction where trans_id="+dropInvoiceNo.SelectedItem.Text+" and trans_type='Sales Invoice'",ref x);	                                        //Add by vikas 15.09.09
                //				}
                //				**********End***************************************************/

                //				dbobj1.Insert_or_Update("update stockmaster_batch set stock_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103), sales=sales+"+double.Parse(rdr["closing_stock"].ToString())+",closing_stock=closing_stock-"+double.Parse(rdr["closing_stock"].ToString())+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

                //				if(lblInvoiceNo.Visible==true)
                //					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103),'"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["closing_stock"].ToString()+"',"+cl_sk.ToString()+")",ref x);
                //				else
                //					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) + "',103),'"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["closing_stock"].ToString()+"',"+cl_sk.ToString()+")",ref x);

                //				//dbobj1.Insert_or_Update("update batch_transaction set trans_date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),qty='"+rdr["closing_stock"].ToString()+"',closing_stock="+cl_sk.ToString()+" where trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Sales Invoice' and prod_id='"+rdr["ProductID"].ToString()+"'",ref x);	

                //				//count-=int.Parse(rdr["qty"].ToString());

                //				/***********add by vikas 19.06.09 *****************/
                //				dbobj1.Insert_or_Update("update batchno set qty="+cl_sk+" where prod_id='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
                //				/****************************/

                //				count-=int.Parse(rdr["closing_stock"].ToString());

                //				/*****Add by vikas 10.06.09*********
                //				if(lblInvoiceNo.Visible==true)
                //					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["ProductID"].ToString()+"','0','"+count.ToString()+"',"+cl_sk.ToString()+")",ref x);
                //				else
                //					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Invoice',Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + "',103),'"+rdr["ProductID"].ToString()+"','0','"+count.ToString()+"',"+cl_sk.ToString()+")",ref x);
                //				/*****end*********/
                //			}
                //		}
                //	}
                //}
                //rdr.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : InsertBatchNo() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        /// <summary>
        /// This method is used to update the customer balance after sales.
        /// </summary>
        public void CustomerUpdate()
        {
            try
            {
                SqlDataReader rdr = null;
                SqlCommand cmd;
                InventoryClass obj = new InventoryClass();
                SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                double Bal = 0;
                string BalType = "", str = "";
                int i = 0;
                //************************
                string[] CheckDate = Invoice_Date.Split(new char[] { ' ' }, Invoice_Date.Length);
                if (DateTime.Compare(System.Convert.ToDateTime(CheckDate[0].ToString()), System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text))) > 0)
                    Invoice_Date = GenUtil.str2DDMMYYYY(lblInvoiceDate.Text);

                List<string> sales = new List<string>();

                sales.Add(CustID);
                sales.Add(Invoice_Date);
                sales.Add(CustID);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(sales);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/sales/CustomerUpdate", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                //            rdr = obj.GetRecordSet("select top 1 Entry_Date from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='"+CustID+"') and Entry_Date<='"+ GenUtil.str2MMDDYYYY(Invoice_Date) + "' order by entry_date desc");
                //if(rdr.Read())
                //	str="select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='"+CustID+ "') and Entry_Date>='" + GenUtil.str2MMDDYYYY(rdr.GetValue(0).ToString()) + "' order by entry_date";
                //else
                //	str="select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='"+CustID+"') order by entry_date";
                //rdr.Close();
                ////*************************
                ////string str="select * from AccountsLedgerTable where Ledger_ID=(select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='"+CustID+"') order by entry_date";
                //rdr=obj.GetRecordSet(str);
                //Bal=0;
                //BalType="";
                //i=0;
                //while(rdr.Read())
                //{
                //	if(i==0)
                //	{
                //		BalType=rdr["Bal_Type"].ToString();
                //		Bal = double.Parse(rdr["Balance"].ToString());
                //		i++;
                //	}
                //	else
                //	{
                //		if(double.Parse(rdr["Credit_Amount"].ToString())!=0)
                //		{
                //			if(BalType=="Cr")
                //			{
                //				Bal+=double.Parse(rdr["Credit_Amount"].ToString());
                //				BalType="Cr";
                //			}
                //			else
                //			{
                //				Bal-=double.Parse(rdr["Credit_Amount"].ToString());
                //				if(Bal<0)
                //				{
                //					Bal=double.Parse(Bal.ToString().Substring(1));
                //					BalType="Cr";
                //				}
                //				else
                //					BalType="Dr";
                //			}
                //		}
                //		else if(double.Parse(rdr["Debit_Amount"].ToString())!=0)
                //		{
                //			if(BalType=="Dr")
                //				Bal+=double.Parse(rdr["Debit_Amount"].ToString());
                //			else
                //			{
                //				Bal-=double.Parse(rdr["Debit_Amount"].ToString());
                //				if(Bal<0)
                //				{
                //					Bal=double.Parse(Bal.ToString().Substring(1));
                //					BalType="Dr";
                //				}
                //				else
                //					BalType="Cr";
                //			}
                //		}
                //		Con.Open();
                //		cmd = new SqlCommand("update AccountsLedgerTable set Balance='"+Bal.ToString()+"',Bal_Type='"+BalType+"' where Ledger_ID='"+rdr["Ledger_ID"].ToString()+"' and Particulars='"+rdr["Particulars"].ToString()+"'",Con);
                //		cmd.ExecuteNonQuery();
                //		cmd.Dispose();
                //		Con.Close();
                //	}			
                //}
                //rdr.Close();

                ///*string Customer_ID="0";
                //dbobj.SelectQuery("select Ledger_ID from Ledger_Master l,Customer c where Cust_Name=Ledger_Name and Cust_ID='"+CustID+"'",ref rdr);
                //if(rdr.Read())
                //{
                //	Customer_ID=rdr["Ledger_ID"].ToString();
                //}
                //rdr.Close();
                //object op=null;
                //dbobj.ExecProc(OprType.Update,"UpdateAccountsLedgerForCustomer",ref op,"@Ledger_ID",Customer_ID);
                //*/
                ////string str1="select * from CustomerLedgerTable where CustID='"+CustID+"' order by entrydate";
                ////*************************
                //rdr = obj.GetRecordSet("select top 1 EntryDate from CustomerLedgerTable where CustID='"+CustID.ToString()+ "' and EntryDate<='" + GenUtil.str2MMDDYYYY(Invoice_Date) + "' order by entrydate desc");
                //if(rdr.Read())
                //	str="select * from CustomerLedgerTable where CustID='"+CustID+"' and EntryDate>='"+ GenUtil.str2MMDDYYYY(rdr.GetValue(0).ToString())+"' order by entrydate";
                //else
                //	str="select * from CustomerLedgerTable where CustID='"+CustID+"' order by entrydate";
                //rdr.Close();
                ////*************************
                //rdr=obj.GetRecordSet(str);
                //Bal=0;
                //i=0;
                //BalType="";
                //while(rdr.Read())
                //{
                //	if(i==0)
                //	{
                //		BalType=rdr["BalanceType"].ToString();
                //		Bal = double.Parse(rdr["Balance"].ToString());
                //		i++;
                //	}
                //	else
                //	{
                //		if(double.Parse(rdr["CreditAmount"].ToString())!=0)
                //		{
                //			if(BalType=="Cr.")
                //			{
                //				Bal+=double.Parse(rdr["CreditAmount"].ToString());
                //				BalType="Cr.";
                //			}
                //			else
                //			{
                //				Bal-=double.Parse(rdr["CreditAmount"].ToString());
                //				if(Bal<0)
                //				{
                //					Bal=double.Parse(Bal.ToString().Substring(1));
                //					BalType="Cr.";
                //				}
                //				else
                //					BalType="Dr.";
                //			}
                //		}
                //		else if(double.Parse(rdr["DebitAmount"].ToString())!=0)
                //		{
                //			if(BalType=="Dr.")
                //				Bal+=double.Parse(rdr["DebitAmount"].ToString());
                //			else
                //			{
                //				Bal-=double.Parse(rdr["DebitAmount"].ToString());
                //				if(Bal<0)
                //				{
                //					Bal=double.Parse(Bal.ToString().Substring(1));
                //					BalType="Dr.";
                //				}
                //				else
                //					BalType="Cr.";
                //			}
                //		}
                //		Con.Open();
                //		cmd = new SqlCommand("update CustomerLedgerTable set Balance='"+Bal.ToString()+"',BalanceType='"+BalType+"' where CustID='"+rdr["CustID"].ToString()+"' and Particular='"+rdr["Particular"].ToString()+"'",Con);
                //		cmd.ExecuteNonQuery();
                //		cmd.Dispose();
                //		Con.Close();
                //	}
                //}
                //rdr.Close();
                //dbobj.ExecProc(OprType.Update,"UpdateCustomerLedgerForCustomer",ref op,"@Cust_ID",CustID);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : CustomerUpdate() EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }

        public void Select_OrderInvoice()
        {
            SalesModel sales = new SalesModel();
            TextSelect.Text = DropOrderInvoice.SelectedItem.Value.ToString();

            try
            {
                /*********Add by vikas 11.12.12********************************/
                string[] Order_No = TextSelect.Text.Split(new char[] { ':' });
                int Count = Order_No.Length;
                /***************************End********************************/

                if (TextSelect.Text == "Select")
                {
                    MessageBox.Show("Please Select Invoice No");
                }
                else
                {
                    Clear();
                    HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
                    TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                    TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
                    TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
                    TextBox[] AvStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };
                    TextBox[] tempQty = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12 };
                    TextBox[] tempSchQty = { txtTempSchQty1, txtTempSchQty2, txtTempSchQty3, txtTempSchQty4, txtTempSchQty5, txtTempSchQty6, txtTempSchQty7, txtTempSchQty8, txtTempSchQty9, txtTempSchQty10, txtTempSchQty11, txtTempSchQty12 };
                    HtmlInputHidden[] tmpQty = { tmpQty1, tmpQty2, tmpQty3, tmpQty4, tmpQty5, tmpQty6, tmpQty7, tmpQty8, tmpQty9, tmpQty10, tmpQty11, tmpQty12 };
                    HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
                    TextBox[] pid = { txtpname1, txtpname2, txtpname3, txtpname4, txtpname5, txtpname6, txtpname7, txtpname8, txtpname9, txtpname10, txtpname11, txtpname12 };
                    TextBox[] pid1 = { txtmwid1, txtmwid2, txtmwid3, txtmwid4, txtmwid5, txtmwid6, txtmwid7, txtmwid8, txtmwid9, txtmwid10, txtmwid11, txtmwid12 };
                    TextBox[] scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
                    TextBox[] foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
                    TextBox[] ProdType1 = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                    TextBox[] Qty1 = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                    TextBox[] stk1 = { txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12 };
                    HtmlInputHidden[] tmpFoeType = { tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12 };
                    HtmlInputHidden[] SchSPType = { tmpSecSPType1, tmpSecSPType2, tmpSecSPType3, tmpSecSPType4, tmpSecSPType5, tmpSecSPType6, tmpSecSPType7, tmpSecSPType8, tmpSecSPType9, tmpSecSPType10, tmpSecSPType11, tmpSecSPType12 };
                    HtmlInputHidden[] SchSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
                    InventoryClass obj = new InventoryClass();
                    InventoryClass obj1 = new InventoryClass();
                    SqlDataReader SqlDtr;
                    string sql, sql1;
                    SqlDataReader rdr = null, rdr1 = null, rdr2 = null, rdr3 = null;
                    int i = 0;
                    FlagPrint = false;
                    Button1.CausesValidation = true;

                    #region Get Data from Order_Col_Master Table regarding Order No.
                    //coment by vikas 11.12.12 sql="select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No='"+DropOrderInvoice.SelectedItem.Value +"'" ;

                    /***********Add by vikas 11.12.12***************************/


                    using (var client = new HttpClient())
                    {
                        //client.BaseAddress = new Uri("http://localhost:55251/api/sales"); baseUri

                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var response = client.GetAsync("/api/sales/GetDataSelectedOrderInvoice?id=" + DropOrderInvoice.SelectedItem.Value).Result;
                        string res = "";

                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                // ... Read the string.
                                Task<string> result = content.ReadAsStringAsync();
                                res = result.Result;

                                sales = JsonConvert.DeserializeObject<SalesModel>(res);
                            }
                        }
                    }

                    //if (Count == 1)
                    //    sql = "select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No='" + DropOrderInvoice.SelectedItem.Value + "'";
                    //else
                    //    sql = "select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No=(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ")";
                    /************End**************************/

                    //SqlDtr = obj.GetRecordSet(sql);
                    //while (SqlDtr.Read())
                    //{
                    Invoice_Date = sales.Invoice_Date;
                    //string strDate = SqlDtr.GetValue(1).ToString().Trim();
                    //int pos = strDate.IndexOf(" ");

                    //if (pos != -1)
                    //{
                    //    strDate = strDate.Substring(0, pos);
                    //}
                    //else
                    //{
                    //    strDate = "";
                    //}

                    lblInvoiceDate.Text = sales.Invoice_Date;
                    tempInvoiceDate.Value = sales.Invoice_Date;
                    DropSalesType.SelectedIndex = (DropSalesType.Items.IndexOf((DropSalesType.Items.FindByValue(sales.Sales_Type))));
                    //DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr.GetValue(4).ToString()))));
                    DropUnderSalesMan.SelectedIndex = (DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(sales.Under_SalesMan))));

                    if (getCustomerVehicles(sales.Cust_ID.ToString()) == true)
                    {
                        DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(sales.Vehicle_No.ToString()));
                    }
                    else
                    {
                        txtVehicleNo.Text = sales.Vehicle_No.ToString();
                    }
                    txtGrandTotal.Text = sales.Grand_Total;
                    txtGrandTotal.Text = GenUtil.strNumericFormat(txtGrandTotal.Text.ToString());
                    txtDisc.Text = GenUtil.strNumericFormat(sales.Discount.ToString());
                    //txtDisc.Text = GenUtil.strNumericFormat(txtDisc.Text.ToString());
                    DropDiscType.SelectedIndex = DropDiscType.Items.IndexOf((DropDiscType.Items.FindByValue(sales.Discount_Type.ToString())));
                    txtNetAmount.Text = GenUtil.strNumericFormat(sales.Net_Amount.ToString());
                    //txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
                    NetAmount = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
                    txtPromoScheme.Text = sales.Promo_Scheme;
                    txtRemark.Text = sales.Remark;
                    lblEntryBy.Text = sales.Entry_By;
                    lblEntryTime.Text = sales.Entry_Time.ToString();
                    txtSecondrySpDisc.Text = sales.SecSPDisc.ToString();

                    if (sales.Discount_Type.ToString() == "Per")
                    {
                        txtDiscount.Text = System.Convert.ToString((double.Parse(sales.Grand_Total.ToString()) - double.Parse(sales.Scheme_Discount.ToString())) * double.Parse(sales.Discount.ToString()) / 100);
                        txtDiscount.Text = System.Convert.ToString(Math.Round(double.Parse(txtDiscount.Text), 2));
                    }
                    else
                    {
                        txtDiscount.Text = "";
                    }


                    if (sales.Cash_Disc_Type.ToString() == "Per")
                    {
                        double tot = 0;
                        if (txtDiscount.Text != "")
                            tot = double.Parse(sales.Grand_Total.ToString()) - (double.Parse(sales.Scheme_Discount.ToString()) + double.Parse(sales.FOE_Discount.ToString()) + double.Parse(txtDiscount.Text));
                        else
                            tot = double.Parse(sales.Grand_Total.ToString()) - (double.Parse(sales.Scheme_Discount.ToString()) + double.Parse(sales.FOE_Discount.ToString()));
                        txtCashDiscount.Text = System.Convert.ToString(tot * double.Parse(sales.Cash_Discount.ToString()) / 100);
                        txtCashDiscount.Text = System.Convert.ToString(Math.Round(double.Parse(txtCashDiscount.Text), 2));
                        tempcashdis.Value = txtCashDiscount.Text;
                    }
                    else
                    {
                        txtCashDiscount.Text = "";
                    }


                    txtCashDisc.Text = sales.Cash_Discount.ToString();
                    txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString());
                    DropCashDiscType.SelectedIndex = DropCashDiscType.Items.IndexOf((DropCashDiscType.Items.FindByValue(sales.Cash_Disc_Type.ToString())));
                    txtVAT.Text = sales.IGST_Amount.ToString();
                    txtschemetotal.Text = sales.Scheme_Discount.ToString();
                    txtfleetoediscount.Text = sales.FOE_Discount.ToString();
                    dropfleetoediscount.SelectedIndex = dropfleetoediscount.Items.IndexOf((dropfleetoediscount.Items.FindByValue(sales.FOE_Discounttype.ToString())));
                    txtfleetoediscountRs.Text = sales.FOE_Discountrs.ToString();
                    txtliter.Text = sales.Total_Qty_Ltr.ToString();

                    if (sales.ChallanNo.ToString() == "0")
                        txtChallanNo.Text = "";
                    else
                        txtChallanNo.Text = sales.ChallanNo.ToString();

                    if (GenUtil.trimDate(sales.ChallanDate.ToString()) == "1/1/1900")
                        txtChallanDate.Text = "";
                    else
                        txtChallanDate.Text = GenUtil.str2DDMMYYYY(GenUtil.trimDate(sales.ChallanDate.ToString()));

                    //if (GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()) == "1/1/1900")
                    //        txtChallanDate.Text = "";
                    //    else
                    //        txtChallanDate.Text = GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()));

                    if (txtVAT.Text.Trim() == "0")
                    {
                        Yes.Checked = false;
                        No.Checked = true;
                    }
                    else
                    {
                        No.Checked = false;
                        Yes.Checked = true;
                    }
                    //}
                    //SqlDtr.Close();
                    #endregion

                    #region Get Customer name and place regarding Customer ID

                    //coment by vikas 10.11.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID from Customer as c, Order_Col_master as s where c.Cust_ID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";

                    //coment by vikas 11.12.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";
                    /***********Add by vikas 11.12.12***************************/
                    //if (Count == 1)
                    //    sql = "select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No='" + DropOrderInvoice.SelectedValue + "'";
                    //else
                    //    sql = "select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No =(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ")";
                    ///************End**************************/
                    //SqlDtr = obj.GetRecordSet(sql);
                    if (sales != null)
                    {
                        //coment by vikas 12.11.2012 texthidden1.Value=SqlDtr.GetValue(0).ToString();
                        //coment by vikas 12.11.2012 text1.Value=SqlDtr.GetValue(0).ToString();

                        if (sales.Cust_Name != null)
                        {
                            texthidden1.Value = sales.Cust_Name;
                            text1.Value = sales.Cust_Name;
                        }

                        CustID = sales.Cust_ID.ToString();
                        lblPlace.Value = sales.Place.ToString();
                        //DateTime duedate = DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
                        //string duedatestr = (duedate.ToShortDateString());
                        //lblDueDate.Value = GenUtil.str2DDMMYYYY(duedatestr);
                        lblDueDate.Value = GenUtil.str2DDMMYYYY(sales.DueDate.ToShortDateString());
                        //lblCurrBalance.Value = GenUtil.strNumericFormat(sales.Current_Balance.ToString());
                        TxtCrLimit.Value = sales.Credit_Limit.ToString();
                        lblCreditLimit.Value = sales.Credit_Limit.ToString();

                        if (sales.CustomerType != null)
                        {
                            txtcusttype.Text = sales.CustomerType.ToString();
                        }


                        /***Add by vikas 10.11.2012 *********************/
                        if (sales.Group_Name.ToString() != null && sales.Group_Name.ToString() != "")
                            tempCustGroup.Value = sales.Group_Name.ToString();
                        /*********************************************/

                    }
                    //SqlDtr.Close();

                    //coment by vikas 11.12.2012 sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue+"' order by entrydate desc";

                    /***********Add by vikas 11.12.12***************************/
                    //if (Count == 1)
                    //    sql = "select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No='" + DropOrderInvoice.SelectedValue + "' order by entrydate desc";
                    //else
                    //    sql = "select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No=(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ") order by entrydate desc";
                    ///*****************************End**************************/
                    //SqlDtr = obj.GetRecordSet(sql);
                    //while (SqlDtr.Read())
                    //{
                    if (sales != null && sales.Balance != null)
                    {
                        lblCurrBalance.Value = GenUtil.strNumericFormat(sales.Balance.ToString()) + " " + sales.BalanceType.ToString();
                    }
                    //}
                    //SqlDtr.Close();
                    #endregion

                    #region Get Data from Order Details Table regarding Order No.

                    /*Coment by vikas 10.11.2012 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_no,sm.Order_date,SchType,FoeType,SPDiscType,SPDisc,cust_id"+
						" from Products p, Order_Col_Details sd,Order_Col_master sm"+
						" where p.Prod_ID=sd.prod_id and sd.Order_no=sm.Order_no and sd.Rate >0 and sd.Amount > 0 and sd.Order_no='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;*/

                    double Avail_Stock = 0, Order_Qty = 0;  //Add by Vikas 12.11.2012

                    //coment by vikas 11.12.2012 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,cust_id,p.Prod_Code"+
                    //coment by vikas 11.12.2012	" from Products p, Order_Col_Details sd,Order_Col_Master sm"+
                    //coment by vikas 11.12.2012	" where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;

                    /***********Add by vikas 11.12.12***************************/
                    //if (Count == 1)
                    //{
                    //    sql = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,cust_id,p.Prod_Code" +
                    //        " from Products p, Order_Col_Details sd,Order_Col_Master sm" +
                    //        " where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='" + DropOrderInvoice.SelectedItem.Value + "' order by sd.sno";
                    //}
                    //else
                    //{
                    //    sql = "select p.Category,p.Prod_Name,p.Pack_Type,(cast(ovd.item_qty as int)-cast(ovd.sale_qty as int)) qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,ovd.cust_id,p.Prod_Code" +
                    //        " from Products p, Order_Col_Details sd,Order_Col_Master sm, ovd" +
                    //        " where ovd.item_id=sd.prod_id and ovd.order_id=sd.order_no and ovd.item_qty>ovd.sale_qty and p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No =(select distinct order_id from ovd o where bo_1=" + Order_No[1].ToString() + " or bo_2=" + Order_No[1].ToString() + " or bo_3=" + Order_No[1].ToString() + ") order by sd.sno";
                    //}
                    ///*****************************End**************************/

                    ////select p.Category,p.Prod_Name,p.Pack_Type,sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,ovd.cust_id,p.Prod_Code
                    ////from Products p, Order_Col_Details sd,Order_Col_Master sm, ovd
                    ////ovd where ovd.item_id=sd.prod_id and ovd.order_id=sd.order_no and ovd.item_qty=ovd.sale_qty and p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No =(select distinct order_id from ovd o where bo_1=9 or bo_2=9 or bo_3=9) order by sd.sno

                    //SqlDtr = obj.GetRecordSet(sql);
                    while (i < sales.ProductName.Count)
                    {
                        /*****************this Condition Add by Vikas 12.11.2012*becouse Condition shift Above*********************************/

                        //Order_Qty = double.Parse(SqlDtr.GetValue(3).ToString());

                        //if (Avail_Stock != 0)
                        //{

                        AvStock[i].Text = sales.Av_Stock[i].ToString();

                        /*******************End**********************************/
                        Qty[i].Enabled = true;
                        Rate[i].Enabled = true;
                        Amount[i].Enabled = true;
                        AvStock[i].Enabled = true;

                        Qty[i].Text = sales.ProductQty[i].ToString();

                        //Coment by Vikas 10.11.2012 ProdType[i].Value=SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();
                        ProdType[i].Value = sales.ProdType[i].ToString();

                        ProductType[i] = sales.ProductType[i].ToString();
                        ProductName[i] = sales.ProductName[i].ToString();
                        ProductPack[i] = sales.ProductPack[i].ToString();
                        ProductQty[i] = sales.ProductQty[i].ToString();

                        tempQty[i].Text = Qty[i].Text;
                        tmpQty[i].Value = sales.ProductQty[i].ToString();
                        Rate[i].Text = sales.Rate[i].ToString();
                        Amount[i].Text = sales.Amount[i].ToString();

                        pid[i].Text = sales.PID[i].ToString();
                        pid1[i].Text = sales.PID1[i].ToString();

                        scheme[i].Text = sales.scheme[i].ToString();

                        foe[i].Text = sales.Details_foe[i].ToString();


                        /*****************Coment by Vikas 12.11.2012*becouse Condition shift Above*********************************
                        sql1="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr.GetValue(6).ToString()+" order by stock_date desc";
                        dbobj.SelectQuery(sql1,ref rdr); 
                        if(rdr.Read())
                        {
                            AvStock [i].Text =rdr["Closing_Stock"]+" "+SqlDtr.GetValue(7).ToString();
                        }	
                        else
                        {
                            AvStock [i].Text ="0"+" "+SqlDtr.GetValue(7).ToString();
                        }
                        *****************End************************************/

                        if (sales.SPDiscType != null)
                        {
                            if (sales.SPDiscType.ToString() == "")
                            {
                                //rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                                //if (rdr3.HasRows)
                                //{
                                //    if (rdr3.Read())
                                //    {
                                SchSPType[i].Value = sales.SchSPType[i].ToString();
                                SchSP[i].Value = sales.SchSP[i].ToString();
                                //    }
                                //}
                                //rdr3.Close();
                            }
                            else
                            {
                                SchSPType[i].Value = sales.SchSPType[i].ToString();
                                SchSP[i].Value = sales.SchSP[i].ToString();
                            }
                        }

                        //strstrste="select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='1470' and sd.prod_id='1037' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'";
                        if (sales.FoeType != null)
                        {
                            if (sales.FoeType.ToString() == "")
                            {
                                //rdr3 = obj1.GetRecordSet("select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='" + SqlDtr["Cust_ID"].ToString() + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "' and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "'");
                                //if (rdr3.HasRows)
                                //{
                                //    if (rdr3.Read())
                                //    {
                                tmpFoeType[i].Value = sales.tmpSchType[i].ToString();
                                //    }
                                //}
                                //rdr3.Close();
                            }
                            else
                            {
                                tmpFoeType[i].Value = sales.FoeType.ToString();
                            }
                        }

                        //*************
                        if (sales.SchType != null)
                        {
                            if (sales.SchType.ToString() == "")
                            {
                                //string ssssss = "select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'";
                                ////rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                                //rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='" + SqlDtr["invoice_No"].ToString() + "' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and cast(floor(cast(o.dateto as float)) as datetime)>='" + GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()) + "' and sd.prod_id='" + SqlDtr["Prod_ID"].ToString() + "'");
                                //if (rdr3.HasRows)
                                //{
                                //    if (rdr3.Read())
                                //    {
                                tmpSchType[i].Value = sales.tmpSchType[i].ToString();
                                //    }
                                //}
                                //rdr3.Close();
                            }
                            else
                            {
                                tmpSchType[i].Value = sales.SchType.ToString();
                            }
                        }

                        Qty[i].ToolTip = "Actual Available Stock = " + Qty[i].Text.ToString() + " + " + AvStock[i].Text.ToString();


                        string sql11 = "select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit" +
                                " from Products p, Order_Col_Details sd" +
                                " where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno=" + i + " and sd.Order_no='" + DropOrderInvoice.SelectedItem.Value + "'";
                        dbobj.SelectQuery(sql11, ref rdr2);
                        if (sales != null)
                        {
                            if (sales.ProdType1.Count != 0)
                                ProdType1[i].Text = sales.ProdType1.ToString();

                            if (sales.SalesQty1.Count != 0)
                                Qty1[i].Text = sales.SalesQty1.ToString();

                            if (sales.SchProductType.Count != 0)
                                SchProductType[i] = sales.SchProductType[i].ToString();

                            if (sales.SchProductName.Count != 0)
                                SchProductName[i] = sales.SchProductName[i].ToString();

                            if (sales.SchProductPack.Count != 0)
                                SchProductPack[i] = sales.SchProductPack[i].ToString();

                            if (sales.SchProductQty.Count != 0)
                                SchProductQty[i] = sales.SchProductQty[i].ToString();

                            if (sales.tempSchQty.Count != 0)
                                tempSchQty[i].Text = sales.tempSchQty[i].ToString();

                            if (sales.stk1.Count != 0)
                                stk1[i].Text = sales.stk1[i].ToString();


                            i++;
                        }
                        //}
                    }
                    //SqlDtr.Close();
                    #endregion
                }
                CreateLogFiles.ErrorLog("Form:Sales Invoisee.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoice is viewed for invoice no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " userid " + "   " + "   " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoise is update for invoise no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
        }

        /// <summary>
        /// This method is used to fatch info according to select the invoice no from dropdown list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropOrderInvoice_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //TextSelect.Text=DropOrderInvoice.SelectedItem.Value.ToString();

            try
            {
                Select_OrderInvoice();
                ///*********Add by vikas 11.12.12********************************/
                //string[] Order_No=TextSelect.Text.Split(new char[] {':'});
                //int Count=Order_No.Length;
                ///***************************End********************************/

                //if(TextSelect.Text=="Select")
                //{
                //	MessageBox.Show("Please Select Invoice No");
                //}
                //else
                //{
                //	Clear();
                //	HtmlInputText[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12};
                //	TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
                //	TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
                //	TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
                //	TextBox[]  AvStock = {txtAvStock1,txtAvStock2,txtAvStock3,txtAvStock4,txtAvStock5,txtAvStock6,txtAvStock7,txtAvStock8,txtAvStock9,txtAvStock10,txtAvStock11,txtAvStock12};
                //	TextBox[]  tempQty = {txtTempQty1, txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12}; 
                //	TextBox[]  tempSchQty = {txtTempSchQty1,txtTempSchQty2,txtTempSchQty3,txtTempSchQty4,txtTempSchQty5,txtTempSchQty6,txtTempSchQty7,txtTempSchQty8 ,txtTempSchQty9,txtTempSchQty10,txtTempSchQty11,txtTempSchQty12};
                //	HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12};
                //	HtmlInputHidden[] tmpSchType = {tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12};
                //	TextBox[] pid={txtpname1,txtpname2,txtpname3,txtpname4,txtpname5,txtpname6,txtpname7,txtpname8,txtpname9,txtpname10,txtpname11,txtpname12};
                //	TextBox[] pid1={txtmwid1,txtmwid2,txtmwid3,txtmwid4,txtmwid5,txtmwid6,txtmwid7,txtmwid8,txtmwid9,txtmwid10,txtmwid11,txtmwid12};
                //	TextBox[]  scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};
                //	TextBox[]  foe = {txtfoe1,txtfoe2,txtfoe3,txtfoe4,txtfoe5,txtfoe6,txtfoe7,txtfoe8,txtfoe9,txtfoe10,txtfoe11,txtfoe12};
                //	TextBox[] ProdType1={txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12}; 
                //	TextBox[] Qty1={txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12}; 
                //	TextBox[] stk1={txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12}; 
                //	HtmlInputHidden[] tmpFoeType = {tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12};
                //	HtmlInputHidden[]  SchSPType = {tmpSecSPType1,tmpSecSPType2,tmpSecSPType3,tmpSecSPType4,tmpSecSPType5,tmpSecSPType6,tmpSecSPType7,tmpSecSPType8,tmpSecSPType9,tmpSecSPType10,tmpSecSPType11,tmpSecSPType12};
                //	HtmlInputHidden[]  SchSP = {txtTempSecSP1,txtTempSecSP2,txtTempSecSP3,txtTempSecSP4,txtTempSecSP5,txtTempSecSP6,txtTempSecSP7,txtTempSecSP8,txtTempSecSP9,txtTempSecSP10,txtTempSecSP11,txtTempSecSP12};
                //	InventoryClass  obj=new InventoryClass ();
                //	InventoryClass  obj1=new InventoryClass ();
                //	SqlDataReader SqlDtr;
                //	string sql,sql1;
                //	SqlDataReader rdr=null,rdr1=null,rdr2=null,rdr3=null;
                //	int i=0;
                //	FlagPrint=false;
                //	Button1.CausesValidation=true;

                //	#region Get Data from Order_Col_Master Table regarding Order No.
                //	//coment by vikas 11.12.12 sql="select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No='"+DropOrderInvoice.SelectedItem.Value +"'" ;

                //	/***********Add by vikas 11.12.12***************************/
                //	if(Count==1)
                //		sql="select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No='"+DropOrderInvoice.SelectedItem.Value +"'" ;
                //	else
                //		sql="select * from Order_Col_Master,Employee where Under_SalesMan=Emp_ID and Order_No=(select distinct order_id from ovd o where bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+")" ;
                //	/************End**************************/

                //	SqlDtr=obj.GetRecordSet(sql); 
                //	while(SqlDtr.Read())
                //	{
                //		Invoice_Date = SqlDtr.GetValue(1).ToString();
                //		string strDate = SqlDtr.GetValue(1).ToString().Trim();
                //		int pos = strDate.IndexOf(" ");

                //		if(pos != -1)
                //		{
                //			strDate = strDate.Substring(0,pos);
                //		}
                //		else
                //		{
                //			strDate = "";					
                //		}

                //		lblInvoiceDate.Text =GenUtil.str2DDMMYYYY(strDate);  
                //		tempInvoiceDate.Value=GenUtil.str2DDMMYYYY(strDate);
                //		DropSalesType.SelectedIndex=(DropSalesType.Items.IndexOf((DropSalesType.Items.FindByValue (SqlDtr.GetValue(2).ToString()))));
                //		//DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr.GetValue(4).ToString()))));
                //		DropUnderSalesMan.SelectedIndex=(DropUnderSalesMan.Items.IndexOf((DropUnderSalesMan.Items.FindByValue(SqlDtr["Emp_Name"].ToString()))));
                //		if(getCustomerVehicles(SqlDtr["Cust_ID"].ToString()) == true)
                //		{
                //			DropVehicleNo.SelectedIndex = DropVehicleNo.Items.IndexOf(DropVehicleNo.Items.FindByValue(SqlDtr.GetValue(5).ToString().Trim()));   
                //		}
                //		else
                //		{
                //			txtVehicleNo.Text=SqlDtr.GetValue(5).ToString();
                //		}
                //		txtGrandTotal.Text=SqlDtr.GetValue(6).ToString();
                //		txtGrandTotal.Text = GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()); 
                //		txtDisc.Text=SqlDtr.GetValue(7).ToString(); 
                //		txtDisc.Text = GenUtil.strNumericFormat(txtDisc.Text.ToString()); 
                //		DropDiscType.SelectedIndex= DropDiscType.Items.IndexOf((DropDiscType.Items.FindByValue(SqlDtr.GetValue(8).ToString())));
                //		txtNetAmount.Text =SqlDtr.GetValue(9).ToString(); 
                //		txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
                //		NetAmount=GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
                //		txtPromoScheme.Text= SqlDtr.GetValue(10).ToString(); 
                //		txtRemark.Text=SqlDtr.GetValue(11).ToString();  
                //		lblEntryBy.Text=SqlDtr.GetValue(12).ToString();  
                //		lblEntryTime.Text= SqlDtr.GetValue(13).ToString();  
                //		txtSecondrySpDisc.Text=SqlDtr["SecSPDisc"].ToString();
                //		if(SqlDtr["Discount_type"].ToString()=="Per")
                //		{
                //			txtDiscount.Text=System.Convert.ToString((double.Parse(SqlDtr["Grand_Total"].ToString())-double.Parse(SqlDtr["schdiscount"].ToString()))*double.Parse(SqlDtr["discount"].ToString())/100);
                //			txtDiscount.Text=System.Convert.ToString(Math.Round(double.Parse(txtDiscount.Text),2));
                //		}
                //		else
                //			txtDiscount.Text="";
                //		if(SqlDtr["cash_Disc_type"].ToString()=="Per")
                //		{
                //			double tot =0;
                //			if(txtDiscount.Text!="")
                //				tot = double.Parse(SqlDtr["Grand_Total"].ToString())-(double.Parse(SqlDtr["schdiscount"].ToString())+double.Parse(SqlDtr["foediscount"].ToString())+double.Parse(txtDiscount.Text));
                //			else
                //				tot = double.Parse(SqlDtr["Grand_Total"].ToString())-(double.Parse(SqlDtr["schdiscount"].ToString())+double.Parse(SqlDtr["foediscount"].ToString()));
                //			txtCashDiscount.Text=System.Convert.ToString(tot*double.Parse(SqlDtr["Cash_Discount"].ToString())/100);
                //			txtCashDiscount.Text=System.Convert.ToString(Math.Round(double.Parse(txtCashDiscount.Text),2));
                //			tempcashdis.Value=txtCashDiscount.Text;
                //		}
                //		else
                //			txtCashDiscount.Text="";

                //		txtCashDisc.Text=SqlDtr.GetValue(15).ToString(); 
                //		txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString()); 
                //		DropCashDiscType.SelectedIndex= DropCashDiscType.Items.IndexOf((DropCashDiscType.Items.FindByValue(SqlDtr.GetValue(16).ToString())));
                //		txtVAT.Text =  SqlDtr.GetValue(17).ToString();
                //		txtschemetotal.Text=SqlDtr.GetValue(18).ToString();
                //		txtfleetoediscount.Text=SqlDtr.GetValue(19).ToString();
                //		dropfleetoediscount.SelectedIndex= dropfleetoediscount.Items.IndexOf((dropfleetoediscount.Items.FindByValue(SqlDtr.GetValue(20).ToString())));
                //		txtfleetoediscountRs.Text=SqlDtr.GetValue(21).ToString();
                //		txtliter.Text=SqlDtr.GetValue(22).ToString();
                //		if(SqlDtr["ChallanNo"].ToString()=="0")
                //			txtChallanNo.Text="";
                //		else
                //			txtChallanNo.Text=SqlDtr["ChallanNo"].ToString();
                //		if(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString())=="1/1/1900")
                //			txtChallanDate.Text="";
                //		else
                //			txtChallanDate.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["ChallanDate"].ToString()));
                //		if(txtVAT.Text.Trim() == "0")
                //		{
                //			Yes.Checked = false;
                //			No.Checked = true;
                //		}
                //		else
                //		{
                //			No.Checked = false;
                //			Yes.Checked = true;
                //		}
                //	}
                //	SqlDtr.Close();
                //	#endregion

                //	#region Get Customer name and place regarding Customer ID

                //	//coment by vikas 10.11.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID from Customer as c, Order_Col_master as s where c.Cust_ID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";

                //	//coment by vikas 11.12.2012 sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";
                //	/***********Add by vikas 11.12.12***************************/
                //	if(Count==1)
                //		sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No='"+DropOrderInvoice.SelectedValue +"'";
                //	else
                //		sql="select Cust_Name, City,CR_Days,Op_Balance,Curr_Credit,Cust_Type,c.Cust_ID,ct.group_name from Customer as c, Order_Col_Master as s,customertype as ct where c.Cust_ID= s.Cust_ID and c.cust_type=ct.customertypename and s.Order_No =(select distinct order_id from ovd o where bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+")" ;
                //	/************End**************************/
                //	SqlDtr=obj.GetRecordSet(sql);
                //	while(SqlDtr.Read())
                //	{
                //		//coment by vikas 12.11.2012 texthidden1.Value=SqlDtr.GetValue(0).ToString();
                //		//coment by vikas 12.11.2012 text1.Value=SqlDtr.GetValue(0).ToString();

                //		texthidden1.Value=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString();
                //		text1.Value=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString();

                //		CustID=SqlDtr["Cust_ID"].ToString();
                //		lblPlace.Value=SqlDtr.GetValue(1).ToString();
                //		DateTime duedate=DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
                //		string duedatestr=(duedate.ToShortDateString());
                //		lblDueDate.Value =GenUtil.str2DDMMYYYY(duedatestr);
                //		lblCurrBalance.Value=GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());
                //		TxtCrLimit.Value = SqlDtr.GetValue(4).ToString();
                //		lblCreditLimit.Value  = SqlDtr.GetValue(4).ToString();
                //		txtcusttype.Text = SqlDtr.GetValue(5).ToString();

                //		/***Add by vikas 10.11.2012 *********************/
                //		if(SqlDtr["Group_Name"].ToString()!=null && SqlDtr["Group_Name"].ToString()!="")
                //			tempCustGroup.Value=SqlDtr["Group_Name"].ToString();
                //		/*********************************************/

                //	}
                //	SqlDtr.Close();

                //	//coment by vikas 11.12.2012 sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue+"' order by entrydate desc";

                //	/***********Add by vikas 11.12.12***************************/
                //	if(Count==1)
                //		sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No='"+DropOrderInvoice.SelectedValue+"' order by entrydate desc";
                //	else
                //		sql="select top 1 balance,balancetype  from CustomerLedgerTable as c, Order_Col_master as s where c.CustID= s.Cust_ID and s.Order_No=(select distinct order_id from ovd o where bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") order by entrydate desc" ;
                //	/*****************************End**************************/
                //	SqlDtr=obj.GetRecordSet(sql);
                //	while(SqlDtr.Read())
                //	{
                //		lblCurrBalance.Value=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString())+" "+SqlDtr.GetValue(1).ToString();
                //	}
                //	SqlDtr.Close();
                //	#endregion

                //	#region Get Data from Order Details Table regarding Order No.

                //	/*Coment by vikas 10.11.2012 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_no,sm.Order_date,SchType,FoeType,SPDiscType,SPDisc,cust_id"+
                //		" from Products p, Order_Col_Details sd,Order_Col_master sm"+
                //		" where p.Prod_ID=sd.prod_id and sd.Order_no=sm.Order_no and sd.Rate >0 and sd.Amount > 0 and sd.Order_no='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;*/

                //	double Avail_Stock=0,Order_Qty=0;  //Add by Vikas 12.11.2012

                //	//coment by vikas 11.12.2012 sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,cust_id,p.Prod_Code"+
                //	//coment by vikas 11.12.2012	" from Products p, Order_Col_Details sd,Order_Col_Master sm"+
                //	//coment by vikas 11.12.2012	" where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;

                //	/***********Add by vikas 11.12.12***************************/
                //	if(Count==1)
                //	{
                //		sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,cust_id,p.Prod_Code"+
                //			" from Products p, Order_Col_Details sd,Order_Col_Master sm"+
                //			" where p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No='"+DropOrderInvoice.SelectedItem.Value +"' order by sd.sno" ;
                //	}
                //	else
                //	{
                //		sql="select p.Category,p.Prod_Name,p.Pack_Type,(cast(ovd.item_qty as int)-cast(ovd.sale_qty as int)) qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,ovd.cust_id,p.Prod_Code"+
                //			" from Products p, Order_Col_Details sd,Order_Col_Master sm, ovd"+
                //			" where ovd.item_id=sd.prod_id and ovd.order_id=sd.order_no and ovd.item_qty>ovd.sale_qty and p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No =(select distinct order_id from ovd o where bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") order by sd.sno" ;
                //	}
                //	/*****************************End**************************/

                //	//select p.Category,p.Prod_Name,p.Pack_Type,sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,sd.Order_No,sm.Order_Date,SchType,FoeType,SPDiscType,SPDisc,ovd.cust_id,p.Prod_Code
                //	//from Products p, Order_Col_Details sd,Order_Col_Master sm, ovd
                //	//ovd where ovd.item_id=sd.prod_id and ovd.order_id=sd.order_no and ovd.item_qty=ovd.sale_qty and p.Prod_ID=sd.prod_id and sd.Order_No=sm.Order_No and sd.Rate >0 and sd.Amount > 0 and sd.Order_No =(select distinct order_id from ovd o where bo_1=9 or bo_2=9 or bo_3=9) order by sd.sno

                //	SqlDtr=obj.GetRecordSet(sql);
                //	while(SqlDtr.Read())
                //	{
                //		/*****************this Condition Add by Vikas 12.11.2012*becouse Condition shift Above*********************************/
                //		sql1="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr.GetValue(6).ToString()+" order by stock_date desc";
                //		dbobj.SelectQuery(sql1,ref rdr); 
                //		if(rdr.Read())
                //		{
                //			//Coment by Vikas 12.11.2012 AvStock [i].Text =rdr["Closing_Stock"]+" "+SqlDtr.GetValue(7).ToString();
                //			Avail_Stock=Double.Parse(rdr["Closing_Stock"].ToString());
                //		}	
                //		else
                //		{
                //			//Coment by Vikas 12.11.2012 AvStock [i].Text ="0"+" "+SqlDtr.GetValue(7).ToString();
                //			Avail_Stock=0;
                //		}

                //		Order_Qty=double.Parse(SqlDtr.GetValue(3).ToString());

                //		if(Avail_Stock != 0)
                //		{

                //			AvStock [i].Text =Avail_Stock+" "+SqlDtr.GetValue(7).ToString();

                //			/*******************End**********************************/
                //			Qty[i].Enabled = true;
                //			Rate[i].Enabled = true;
                //			Amount[i].Enabled = true;
                //			AvStock[i].Enabled = true;
                //			//Coment by Vikas 10.11.2012 ProdType[i].Value=SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();
                //			ProdType[i].Value=SqlDtr["Prod_Code"].ToString ()+":"+SqlDtr.GetValue(1).ToString ()+":"+SqlDtr.GetValue(2).ToString ();

                //			//Coment by Vikas 10.11.2012 Qty[i].Text=SqlDtr.GetValue(3).ToString();
                //			if(Avail_Stock >= Order_Qty)
                //			{
                //				Qty[i].Text=SqlDtr.GetValue(3).ToString();
                //			}
                //			else
                //			{
                //				Qty[i].Text=Avail_Stock.ToString();
                //			}

                //			ProductType[i]=SqlDtr.GetValue(0).ToString ();
                //			ProductName[i]=SqlDtr.GetValue(1).ToString ();
                //			ProductPack[i]=SqlDtr.GetValue(2).ToString ();
                //			ProductQty[i]=SqlDtr.GetValue(3).ToString();
                //			tempQty[i].Text   = Qty[i].Text ;
                //			tmpQty[i].Value  = SqlDtr.GetValue(3).ToString();  
                //			Rate[i].Text=SqlDtr.GetValue(4).ToString();
                //			Amount[i].Text=SqlDtr.GetValue(5).ToString();
                //			pid[i].Text=SqlDtr.GetValue(6).ToString();
                //			pid1[i].Text=SqlDtr.GetValue(6).ToString();
                //			scheme[i].Text=SqlDtr.GetValue(8).ToString();
                //			foe[i].Text=SqlDtr.GetValue(9).ToString();

                //			/*****************Coment by Vikas 12.11.2012*becouse Condition shift Above*********************************
                //			sql1="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr.GetValue(6).ToString()+" order by stock_date desc";
                //			dbobj.SelectQuery(sql1,ref rdr); 
                //			if(rdr.Read())
                //			{
                //				AvStock [i].Text =rdr["Closing_Stock"]+" "+SqlDtr.GetValue(7).ToString();
                //			}	
                //			else
                //			{
                //				AvStock [i].Text ="0"+" "+SqlDtr.GetValue(7).ToString();
                //			}
                //			*****************End************************************/

                //			if(SqlDtr["SPDiscType"].ToString()=="")
                //			{
                //				//rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                //				rdr3 = obj1.GetRecordSet("select o.DiscountType,o.Discount from order_col_details sd,oilscheme o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and sd.order_no='"+SqlDtr["Order_No"].ToString()+"' and o.schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["order_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                //				if(rdr3.HasRows)
                //				{
                //					if(rdr3.Read())
                //					{
                //						SchSPType[i].Value=rdr3.GetValue(0).ToString();
                //						SchSP[i].Value=rdr3.GetValue(1).ToString();
                //					}
                //				}
                //				rdr3.Close();
                //			}
                //			else
                //			{
                //				SchSPType[i].Value=SqlDtr["SPDiscType"].ToString();
                //				SchSP[i].Value=SqlDtr["SPDisc"].ToString();
                //			}

                //			if(SqlDtr["FoeType"].ToString()=="")
                //			{
                //				//rdr3 = obj1.GetRecordSet("select distinct o.distype from sales_details sd,foe o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and custid=cust_id and custid='"+SqlDtr["Cust_ID"].ToString()+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"'");
                //				string ss="select distinct o.distype from order_col_details sd,foe o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and custid=cust_id and custid='"+SqlDtr["Cust_ID"].ToString()+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Order_Date"].ToString())+"'";
                //				rdr3 = obj1.GetRecordSet("select distinct o.distype from order_col_details sd,foe o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and custid=cust_id and custid='"+SqlDtr["Cust_ID"].ToString()+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"' and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Order_Date"].ToString())+"'");
                //				if(rdr3.HasRows)
                //				{
                //					if(rdr3.Read())
                //					{
                //						tmpFoeType[i].Value=rdr3.GetValue(0).ToString();
                //					}
                //				}
                //				rdr3.Close();
                //			}
                //			else
                //				tmpFoeType[i].Value=SqlDtr["FoeType"].ToString();

                //			if(SqlDtr["SchType"].ToString()=="")
                //			{
                //				//rdr3 = obj1.GetRecordSet("select o.discounttype from sales_details sd,oilscheme o,sales_master sm where o.prodid=sd.prod_id and sm.invoice_no=sd.invoice_no and sd.invoice_no='"+SqlDtr["invoice_No"].ToString()+"' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                //				rdr3 = obj1.GetRecordSet("select o.discounttype from order_col_details sd,oilscheme o,order_col_master sm where o.prodid=sd.prod_id and sm.order_no=sd.order_no and sd.order_no='"+SqlDtr["Order_No"].ToString()+"' and (o.schname='Primary(LTR&% Scheme)' or o.schname='Secondry(LTR Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime)<='"+GenUtil.trimDate(SqlDtr["order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)>='"+GenUtil.trimDate(SqlDtr["Order_Date"].ToString())+"' and sd.prod_id='"+SqlDtr["Prod_ID"].ToString()+"'");
                //				if(rdr3.HasRows)
                //				{
                //					if(rdr3.Read())
                //					{
                //						tmpSchType[i].Value=rdr3.GetValue(0).ToString();
                //					}
                //				}
                //				rdr3.Close();
                //			}
                //			else
                //				tmpSchType[i].Value=SqlDtr["SchType"].ToString();

                //			Qty[i].ToolTip = "Actual Available Stock = "+Qty[i].Text.ToString()+" + "+ AvStock[i].Text.ToString();


                //			string	sql11="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,p.Prod_ID,p.unit"+
                //				" from Products p, Order_Col_Details sd"+
                //				" where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sno="+i+" and sd.Order_no='"+DropOrderInvoice.SelectedItem.Value +"'" ;
                //			dbobj.SelectQuery(sql11,ref rdr2);
                //			if(rdr2.HasRows)
                //			{
                //				while(rdr2.Read())
                //				{
                //					ProdType1[i].Text=rdr2.GetValue(1).ToString()+":"+rdr2.GetValue(2).ToString();
                //					Qty1[i].Text=rdr2.GetValue(3).ToString();
                //					SchProductType[i]=rdr2.GetValue(0).ToString();
                //					SchProductName[i]=rdr2.GetValue(1).ToString();
                //					SchProductPack[i]=rdr2.GetValue(2).ToString();
                //					SchProductQty[i]=rdr2.GetValue(3).ToString();
                //					tempSchQty[i].Text=rdr2.GetValue(3).ToString();
                //					string sql12="select top 1 Closing_Stock from Stock_Master where productid="+rdr2.GetValue(4).ToString()+" order by stock_date desc";
                //					dbobj.SelectQuery(sql12,ref rdr1); 
                //					if(rdr1.Read())
                //					{
                //						stk1[i].Text = rdr1["Closing_Stock"]+" "+rdr2.GetValue(5).ToString();
                //					}
                //					else
                //					{
                //						stk1[i].Text ="0"+" "+rdr2.GetValue(5).ToString();
                //					}

                //						/*********
                //						rdr3 = obj1.GetRecordSet("select o.discounttype from Order_Col_details sd,oilscheme o,Order_Col_master sm where o.prodid=sd.prod_id and sm.Order_no=sd.Order_no and sd.Order_no='"+SqlDtr["Order_No"].ToString()+"' and o.schname='Primary(LTR&% Scheme)' and cast(floor(cast(o.datefrom as float)) as datetime)>='"+GenUtil.str2DDMMYYYY(SqlDtr["Order_Date"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime)<='"+GenUtil.str2DDMMYYYY(SqlDtr["Order_Date"].ToString())+"' and sd.prod_id='"+rdr2["Prod_ID"].ToString()+"'");
                //						if(rdr3.HasRows)
                //						{
                //							if(rdr3.Read())
                //							{
                //								tmpSchType[i].Value=rdr3.GetValue(0).ToString();
                //							}
                //						}
                //						rdr3.Close();
                //						**********/
                //				}
                //				rdr1.Close();
                //			}
                //			rdr2.Close();
                //			rdr.Close();
                //		i++;
                //		}
                //	}
                //	SqlDtr.Close();
                //#endregion
                //}
                CreateLogFiles.ErrorLog("Form:Sales Invoisee.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoice is viewed for invoice no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " userid " + "   " + "   " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Sales Invoice.aspx,Method:dropInvoiceNo_SelectedIndexChanged " + " Sales invoise is update for invoise no: " + dropInvoiceNo.SelectedItem.Value.ToString() + " EXCEPTION  " + ex.Message + "  userid " + "   " + "   " + uid);
            }
        }

        protected void btnDelete_Click(object sender, System.EventArgs e)
        {
        }

        protected void txtfoe1_TextChanged(object sender, System.EventArgs e)
        {

        }

        protected void DropType11_ServerChange(object sender, System.EventArgs e)
        {

        }

        protected void txtPromoScheme_TextChanged(object sender, System.EventArgs e)
        {

        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {

        }

    }
}