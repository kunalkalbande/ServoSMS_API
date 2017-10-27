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
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using DBOperations;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Collections.Generic;


namespace Servosms.Module.Inventory
{
    /// <summary>
    /// Summary description for PurchaseInvoice.
    /// </summary>
    public partial class PurchaseInvoice : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DropDownList DropProd2;
        protected System.Web.UI.WebControls.DropDownList DropProd3;
        protected System.Web.UI.WebControls.DropDownList DropProd4;
        protected System.Web.UI.WebControls.DropDownList DropProd5;
        protected System.Web.UI.WebControls.DropDownList DropProd6;
        protected System.Web.UI.WebControls.DropDownList DropProd7;
        protected System.Web.UI.WebControls.DropDownList DropProd8;
        protected System.Web.UI.WebControls.DropDownList DropPack2;
        protected System.Web.UI.WebControls.DropDownList DropPack3;
        protected System.Web.UI.WebControls.DropDownList DropPack4;
        protected System.Web.UI.WebControls.DropDownList DropPack5;
        protected System.Web.UI.WebControls.DropDownList DropPack6;
        protected System.Web.UI.WebControls.DropDownList DropPack7;
        protected System.Web.UI.WebControls.DropDownList DropPack8;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack8;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack7;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack6;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack5;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack4;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName4;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName5;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName6;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName7;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName8;
        DBUtil dbobj = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        DBUtil dbobj1 = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        static string[] ProductType = new string[20];
        static string[] ProductName = new string[20];
        static string[] ProductPack = new string[20];
        static string[] ProductQty = new string[20];
        protected System.Web.UI.WebControls.DropDownList DropProd9;
        protected System.Web.UI.WebControls.DropDownList DropPack9;
        protected System.Web.UI.WebControls.DropDownList DropProd10;
        protected System.Web.UI.WebControls.DropDownList DropPack10;
        protected System.Web.UI.WebControls.DropDownList DropProd11;
        protected System.Web.UI.WebControls.DropDownList DropPack11;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName9;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack9;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName10;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack10;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName11;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack11;
        protected System.Web.UI.WebControls.CompareValidator Comparevalidator3;
        protected System.Web.UI.WebControls.DropDownList DropProd12;
        protected System.Web.UI.WebControls.DropDownList DropPack12;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName12;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack12;
        protected System.Web.UI.WebControls.DropDownList DropProd13;
        protected System.Web.UI.WebControls.DropDownList DropPack13;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName13;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack13;
        protected System.Web.UI.WebControls.DropDownList DropProd20;
        protected System.Web.UI.WebControls.DropDownList DropPack20;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName20;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack20;
        protected System.Web.UI.WebControls.DropDownList DropProd14;
        protected System.Web.UI.WebControls.DropDownList DropPack14;
        protected System.Web.UI.WebControls.DropDownList DropProd15;
        protected System.Web.UI.WebControls.DropDownList DropPack15;
        protected System.Web.UI.WebControls.DropDownList DropProd16;
        protected System.Web.UI.WebControls.DropDownList DropPack16;
        protected System.Web.UI.WebControls.DropDownList DropProd17;
        protected System.Web.UI.WebControls.DropDownList DropPack17;
        protected System.Web.UI.WebControls.DropDownList DropProd18;
        protected System.Web.UI.WebControls.DropDownList DropPack18;
        protected System.Web.UI.WebControls.DropDownList DropProd19;
        protected System.Web.UI.WebControls.DropDownList DropPack19;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName14;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack14;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName15;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack15;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName16;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack16;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName17;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack17;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName18;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack18;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtProdName19;
        protected System.Web.UI.HtmlControls.HtmlInputHidden txtPack19;
        string uid;
        string BaseUri = "http://localhost:64862";
        public static bool FlagPrint = false;
        static string Vendor_ID = "0", Invoice_Date = "";

        protected System.Web.UI.WebControls.TextBox txtebird2;
        protected System.Web.UI.WebControls.TextBox txtebird3;
        protected System.Web.UI.WebControls.TextBox txtebird4;
        protected System.Web.UI.WebControls.Calendar cal1;

        public void getheader()
        {
            string home_drive = Environment.SystemDirectory;
            home_drive = home_drive.Substring(0, 2);
            string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PurchaseInvoiceReport.txt";
            StreamWriter sw = new StreamWriter(path);
            //DropDownList[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20};
            HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
            //HtmlInputHidden[]  ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20};
            //HtmlInputHidden[]  ProdName={txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20};
            TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
            HtmlInputHidden[] batch = { bat0, bat1, bat2, bat3, bat4, bat5, bat6, bat7, bat8, bat9, bat10, bat11, bat12, bat13, bat14, bat15, bat16, bat17, bat18, bat19 };
            string[] arr = new string[20];
            int NewCount = 0, OldCount = 0;
            for (int i = 0, j = 0; i < 12; i++, j++)
            {
                string b = batch[i].Value;
                b = b.Replace("'", "");
                OldCount = 0;
                arr = b.Split(new char[] { ',' }, b.Length);
                //arr1=batch[++i].Value.Split(new char[] {','},batch[i].Value.Length);
                for (int k = 0; k < arr.Length; k++)
                {
                    if (arr[k].ToString() != "")
                        OldCount++;
                    else
                        break;
                }
                if (NewCount < OldCount)
                    NewCount = OldCount;
            }
            string header = "";
            string desdes = "+---------+-------+";
            header = "|ProductCD|Tot_Qty|";
            for (int j = 1; j <= NewCount / 2; j++)
            {
                desdes += "----------+---+";
                header += "  Batch" + j + "  |Qty|";
            }
            sw.WriteLine(desdes);
            sw.WriteLine(header);
            sw.WriteLine(desdes);
            for (int i = 0; i < 12; i++)
            {
                string bat = batch[i].Value;
                bat = bat.Replace("'", "");
                arr = bat.Split(new char[] { ',' }, bat.Length);
                int n = 0, s = arr.Length;
                string sss = arr[n].ToString();
                if (arr[n].ToString() == "")
                    break;
                //{
                //,arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString());
                //***sw.Write("|"+ProdName[i].Value);
                sw.Write("|" + ProdType[i].Value);
                //***for(int a=ProdName[i].Value.Length;a<9;a++)
                for (int a = ProdType[i].Value.Length; a < 9; a++)
                {
                    sw.Write(" ");
                }
                sw.Write("|" + Request.Form[Qty[i].ID].ToString());

                for (int a = Request.Form[Qty[i].ID].ToString().Length; a < 7; a++)

                {
                    sw.Write(" ");
                }
                sw.Write("|");
                int d = 0;
                for (int b = 0; b < arr.Length; b++)
                {
                    if (arr[b].ToString() != "")
                    {
                        d++;
                        sw.Write(arr[b].ToString());
                        for (int c = arr[b].Length; c < 10; c++)
                        {
                            sw.Write(" ");
                        }
                        sw.Write("|");
                        sw.Write(arr[++b].ToString());
                        for (int c = arr[b].Length; c < 3; c++)
                        {
                            sw.Write(" ");
                        }
                        sw.Write("|");
                    }
                    else
                    {
                        if (d >= NewCount / 2)
                        {
                            sw.WriteLine();
                            break;
                        }
                        else
                        {
                            sw.Write("          |   |");
                            d++;
                        }
                    }
                }

            }
            sw.WriteLine(desdes);
            sw.Close();
        }

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
                uid = (Session["User_Name"].ToString());
                txtMessage.Text = (Session["Message"].ToString());
                //txtVatRate.Value = (Session["VAT_Rate"].ToString());
                //txtentrytax.Value = (Session["Entrytax"].ToString());
                BtnEdit.Visible = true;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:pageload" + ex.Message + "  EXCEPTION " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }
            if (tempDelinfo.Value == "Yes")
            {
                DeleteTheRec();
                UpdateBatchNo();
            }

            Earlybird_dis();

            if (!IsPostBack)
            {
                try
                {
                    tempVndrInvoiceNo.Value = "";
                    DropInvoiceNo.Visible = false;
                    Vendor_ID = "0";
                    checkPrevileges();
                    Invoice_Date = "";
                    /********add-bhal****/
                    lblInvoiceDate.Text = (Session["CurrentDate"].ToString());
                    //********com-bhal****	lblInvoiceDate.Text=GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());  
                    lblEntryTime.Text = DateTime.Now.ToString();
                    lblEntryBy.Text = Session["User_Name"].ToString();
                    //txtebird.Text="0.40";
                    //txtfixed.Text="0.30";
                    txtCashDisc.Text = "1.5";
                    //txttradedis.Text="4.10";
                   
                    #region Fetch All Discount and fill in the textbox
                    DiscountModel discount = new DiscountModel();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/purchase/GetDiscount").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            discount = JsonConvert.DeserializeObject<DiscountModel>(disc);
                        }
                    }
                    if (discount!=null)
                    {
                        if (discount.FixedStatus == "1")
                            txtfixed.Text = discount.FixedDis;
                        else
                            txtfixed.Text = "";

                        /********Add by vikas 31.10.2012*************/
                        if (discount.FixedStatus == "1")
                            tempfixdisc.Value = discount.FixedDis;
                        else
                            tempfixdisc.Value = "";
                        /*********End************/


                        if (discount.ServoStatus == "1")
                        {
                            tempStkdisc.Value = discount.ServoStk;
                        }
                        else
                            tempStkdisc.Value = "0";


                        /*// Comment by vikas Sharma 24.04.09
						if(SqlDtr["EarlyStatus"].ToString()=="1")
							txtebird.Text=SqlDtr["EarlyBird"].ToString();
						else
							txtebird.Text="0.40";*/

                        /*********Add by vikas 6.11.2012***************
						if(SqlDtr["EarlyBird_Period"].ToString()!=null)
							tempEBPeriod.Value=SqlDtr["EarlyBird_Period"].ToString().Trim();
						*************************/


                        if (discount.CashDisPurchaseStatus == "1")
                        {
                            txtCashDisc.Text = discount.CashDisPurchase;
                            if (discount.CashDisLtrPurchase == "Rs.")
                                DropCashDiscType.SelectedIndex = 0;
                            else
                                DropCashDiscType.SelectedIndex = 1;
                        }
                        else
                            txtCashDisc.Text = "0";

                        txtDiscStatus.Value = discount.DiscountPurchaseStatus;

                        if ( discount.DiscountPurchaseStatus == "1")
                        {
                            txtDisc.Text = discount.DiscountPurchase;
                            if (discount.DisLtrPurchase == "Rs.")
                                DropDiscType.SelectedIndex = 0;
                            else
                                DropDiscType.SelectedIndex = 1;
                        }
                        else
                            txtDisc.Text = "0";
                    }
                    else
                    {
                        //txtebird.Text="0.40";
                        txtfixed.Text = "";
                        //txttradedis.Text="4.10";
                    }
                   
                    #endregion
                    HtmlInputText[] ProductType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };

                    GetNextInvoiceNo();

                    #region Fetch the Product Types and fill in the ComboBoxes
                    string ProductTypes = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/purchase/GetProductsType").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            ProductTypes = JsonConvert.DeserializeObject<string>(disc);
                        }
                    }
                    if (ProductTypes != null)
                    {
                        texthiddenprod.Value += ProductTypes;
                    }
                    #endregion

                    #region Fetch All Supplier ID and fill in the ComboBox
                    List<string> SupplierName = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/purchase/GetSupplierName").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            SupplierName = JsonConvert.DeserializeObject<List<string>>(id);
                        }
                    }
                    
                    if (SupplierName!=null)
                    {
                        foreach(var supplier in SupplierName)
                        DropVendorID.Items.Add(supplier);
                    }
                    #endregion

                    //getvalue();
                    PriceUpdation();
                    GetProducts();
                    GetProductsUnit();
                    FetchCity();
                    FatchInvoiceNo();
                    getscheme();
                    get_Addscheme();     //Add by vikas 30.06.09
                    GetStockistScheme();
                    GetFixedDiscount();

                }
                catch (Exception ex)
                {
                    CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:pageload.  EXCEPTION: " + ex.Message + " User_ID: " + uid);
                }
            }
            SaveDataInControlsOnPageLoad();
        }
        //public void getvalue()
        //{
        //    InventoryClass obj = new InventoryClass();
        //    SqlDataReader SqlDtr = obj.GetRecordSet("select * from SetDis");
        //    if (SqlDtr.Read())
        //    {
        //        if (SqlDtr["IGSTPurchaseStatus"].ToString()=="1")
        //           txtVatRate.Value = SqlDtr["IGSTPurchase"].ToString();
        //        else
        //           txtVatRate.Value = "0";
        //        if (SqlDtr["CGSTPurchaseStatus"].ToString() == "1")
        //            Tempcgstrate.Value = SqlDtr["CGSTPurchase"].ToString();
        //        else
        //            Tempcgstrate.Value = "0";
        //        if (SqlDtr["SGSTPurchaseStatus"].ToString() == "1")
        //            Tempsgstrate.Value = SqlDtr["SGSTPurchase"].ToString();
        //        else
        //            Tempsgstrate.Value = "0";


        //    }
        //}
        public void PriceUpdation()
        {
            string mainGST = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/PriceUpdation").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    mainGST = JsonConvert.DeserializeObject<string>(id);
                }
            }
            txtMainGST.Value = mainGST;
        }

        //public void GetGstCalculation()
        //{
        //    HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
        //    TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
        //    TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
        //    TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
        //    HtmlInputHidden[] batch = { bat0, bat1, bat2, bat3, bat4, bat5, bat6, bat7, bat8, bat9, bat10, bat11, bat12, bat13, bat14, bat15, bat16, bat17, bat18, bat19 };
        //    HtmlInputHidden[] tempSchDis = { tempSchDis1, tempSchDis2, tempSchDis3, tempSchDis4, tempSchDis5, tempSchDis6, tempSchDis7, tempSchDis8, tempSchDis9, tempSchDis10, tempSchDis11, tempSchDis12, tempSchDis13, tempSchDis14, tempSchDis15, tempSchDis16, tempSchDis17, tempSchDis18, tempSchDis19, tempSchDis20 };
        //    HtmlInputHidden[] tempStktSchDis = { tempStktSchDis1, tempStktSchDis2, tempStktSchDis3, tempStktSchDis4, tempStktSchDis5, tempStktSchDis6, tempStktSchDis7, tempStktSchDis8, tempStktSchDis9, tempStktSchDis10, tempStktSchDis11, tempStktSchDis12, tempStktSchDis13, tempStktSchDis14, tempStktSchDis15, tempStktSchDis16, tempStktSchDis17, tempStktSchDis18, tempStktSchDis19, tempStktSchDis20 };
        //    HtmlInputHidden[] tempDiscount = { tempSchDiscount1, tempSchDiscount2, tempSchDiscount3, tempSchDiscount4, tempSchDiscount5, tempSchDiscount6, tempSchDiscount7, tempSchDiscount8, tempSchDiscount9, tempSchDiscount10, tempSchDiscount11, tempSchDiscount12, tempSchDiscount13, tempSchDiscount14, tempSchDiscount15, tempSchDiscount16, tempSchDiscount17, tempSchDiscount18, tempSchDiscount19, tempSchDiscount20 };
        //    HtmlInputHidden[] tempSchAddDis = { tempSchAddDis1, tempSchAddDis2, tempSchAddDis3, tempSchAddDis4, tempSchAddDis5, tempSchAddDis6, tempSchAddDis7, tempSchAddDis8, tempSchAddDis9, tempSchAddDis10, tempSchAddDis11, tempSchAddDis12, tempSchAddDis13, tempSchAddDis14, tempSchAddDis15, tempSchAddDis16, tempSchAddDis17, tempSchAddDis18, tempSchAddDis19, tempSchAddDis20 };             //Add by vikas 03.07.09
        //    HtmlInputHidden[] tempFixdDisc = { tempFixedDisc1, tempFixedDisc2, tempFixedDisc3, tempFixedDisc4, tempFixedDisc5, tempFixedDisc6, tempFixedDisc7, tempFixedDisc8, tempFixedDisc9, tempFixedDisc10, tempFixedDisc11, tempFixedDisc12, tempFixedDisc13, tempFixedDisc14, tempFixedDisc15, tempFixedDisc16, tempFixedDisc17, tempFixedDisc18, tempFixedDisc19, tempFixedDisc20 };             //Add by vikas 16.05.13
        //    CheckBox[] chkfoc = { chkfoc1, chkfoc2, chkfoc3, chkfoc4, chkfoc5, chkfoc6, chkfoc7, chkfoc8, chkfoc9, chkfoc10, chkfoc11, chkfoc12, chkfoc13, chkfoc14, chkfoc15, chkfoc16, chkfoc17, chkfoc18, chkfoc19, chkfoc20 };
        //    HtmlInputCheckBox[] chkDisc = { chkSchDisc1, chkSchDisc2, chkSchDisc3, chkSchDisc4, chkSchDisc5, chkSchDisc6, chkSchDisc7, chkSchDisc8, chkSchDisc9, chkSchDisc10, chkSchDisc11, chkSchDisc12, chkSchDisc13, chkSchDisc14, chkSchDisc15, chkSchDisc16, chkSchDisc17, chkSchDisc18, chkSchDisc19, chkSchDisc20 };

        //    List<ProductModel> products = new List<ProductModel>();
        //    for (int i = 0; i <= 19; i++)
        //    {
        //        if (ProdType[i].Value != "Type")
        //        {
        //            ProductModel product = new ProductModel();
        //            product.ProductName = ProdType[i].Value;
        //            product.Quantity = Convert.ToInt32(Qty[i].Text);
        //            product.Amount = double.Parse(Amount[i].Text);
        //            if (chkfoc[i].Checked)
        //                product.CheckFOC = true;
        //            else
        //                product.CheckFOC = false;

        //            if (chkfoc[i].Checked)
        //                product.FOC = double.Parse(Amount[i].Text);

        //            if (chkDisc[i].Checked)
        //                product.CheckSchDisc = true;
        //            else
        //                product.CheckSchDisc = false;
        //            product.InvoiceDate = GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim());
        //            product.TradeLess = Convert.ToInt32(txttradeless.Text);
        //            product.EarlyDisType = tempEarlyDisType.Value.ToString();
        //            product.BirdLess = Convert.ToInt32(txtbirdless.Text);
        //            product.Discount = txtDisc.Text;
        //            product.DropDiscType = DropDiscType.SelectedValue;
        //            product.CashDiscount = txtCashDisc.Text;
        //            product.SchDiscount = tempDiscount[i].Value;
        //            product.DropCashDiscType = DropCashDiscType.Text;
        //            products.Add(product);
        //        }
        //    }

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:64861/api/purchase/");
        //        var myContent = JsonConvert.SerializeObject(products);
        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        var byteContent = new ByteArrayContent(buffer);
        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        var response = client.PostAsync(client.BaseAddress, byteContent).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseString = response.Content.ReadAsStringAsync().Result;
        //            var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
        //        }
        //    }
        //}


        public void SaveDataInControlsOnPageLoad()
        {
            lblInvoiceDate.Text = Request.Form["lblInvoiceDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["lblInvoiceDate"].ToString().Trim();
            txtVInvoiceDate.Text = Request.Form["txtVInvoiceDate"] == null ? null : Request.Form["txtVInvoiceDate"].ToString().Trim();

            txttotalqtyltr1.Text = Request.Form["txttotalqtyltr1"] == null ? null : Request.Form["txttotalqtyltr1"].ToString().Trim();
            txttotalqtyltr.Text = Request.Form["txttotalqtyltr"] == null ? null : Request.Form["txttotalqtyltr"].ToString().Trim();
            txttotalqty.Text = Request.Form["txttotalqty"] == null ? null : Request.Form["txttotalqty"].ToString().Trim();

            txtPromoScheme.Text = Request.Form["txtPromoScheme"] == null ? null : Request.Form["txtPromoScheme"].ToString().Trim();
            txtfoc.Text = Request.Form["txtfoc"] == null ? null : Request.Form["txtfoc"].ToString().Trim();
            //txtentry.Text = Request.Form["txtentry"] == null ? null : Request.Form["txtentry"].ToString().Trim();
            txtfixedamt.Text = Request.Form["txtfixedamt"] == null ? null : Request.Form["txtfixedamt"].ToString().Trim();
            txtAddDis.Text = Request.Form["txtAddDis"] == null ? null : Request.Form["txtAddDis"].ToString().Trim();
            txtfixdisc.Text = Request.Form["txtfixdisc"] == null ? null : Request.Form["txtfixdisc"].ToString().Trim();
            txtfixdiscamount.Text = Request.Form["txtfixdiscamount"] == null ? null : Request.Form["txtfixdiscamount"].ToString().Trim();

            txtGrandTotal.Text = Request.Form["txtGrandTotal"] == null ? null : Request.Form["txtGrandTotal"].ToString().Trim();
            txtebird.Text = txtebird.Text;
            txtebirdamt.Text = txtebirdamt.Text;
            txtbirdless.Text = txtbirdless.Text;
            txttradedisamt.Text = txttradedisamt.Text;
            txttradeless.Text = txttradeless.Text;
            txtCashDisc.Text = txtCashDisc.Text;
            txtTotalCashDisc.Text = txtTotalCashDisc.Text;

            txtDisc.Text = txtDisc.Text;
            txtTotalDisc.Text = txtTotalDisc.Text;
            txtVAT.Text = txtVAT.Text;
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
            txtRate13.Text = Request.Form["txtRate13"] == null ? null : Request.Form["txtRate13"].ToString().Trim();
            txtRate14.Text = Request.Form["txtRate14"] == null ? null : Request.Form["txtRate14"].ToString().Trim();
            txtRate15.Text = Request.Form["txtRate15"] == null ? null : Request.Form["txtRate15"].ToString().Trim();
            txtRate16.Text = Request.Form["txtRate16"] == null ? null : Request.Form["txtRate16"].ToString().Trim();
            txtRate17.Text = Request.Form["txtRate17"] == null ? null : Request.Form["txtRate17"].ToString().Trim();
            txtRate18.Text = Request.Form["txtRate18"] == null ? null : Request.Form["txtRate18"].ToString().Trim();
            txtRate19.Text = Request.Form["txtRate19"] == null ? null : Request.Form["txtRate19"].ToString().Trim();
            txtRate20.Text = Request.Form["txtRate20"] == null ? null : Request.Form["txtRate20"].ToString().Trim();

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
            txtAmount13.Text = Request.Form["txtAmount13"] == null ? null : Request.Form["txtAmount13"].ToString().Trim();
            txtAmount14.Text = Request.Form["txtAmount14"] == null ? null : Request.Form["txtAmount14"].ToString().Trim();
            txtAmount15.Text = Request.Form["txtAmount15"] == null ? null : Request.Form["txtAmount15"].ToString().Trim();
            txtAmount16.Text = Request.Form["txtAmount16"] == null ? null : Request.Form["txtAmount16"].ToString().Trim();
            txtAmount17.Text = Request.Form["txtAmount17"] == null ? null : Request.Form["txtAmount17"].ToString().Trim();
            txtAmount18.Text = Request.Form["txtAmount18"] == null ? null : Request.Form["txtAmount18"].ToString().Trim();
            txtAmount19.Text = Request.Form["txtAmount19"] == null ? null : Request.Form["txtAmount19"].ToString().Trim();
            txtAmount20.Text = Request.Form["txtAmount20"] == null ? null : Request.Form["txtAmount20"].ToString().Trim();

        }

        public void Earlybird_dis()
        {
            try
            {
                DiscountModel discount = new DiscountModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/purchase/GetDiscount").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        discount = JsonConvert.DeserializeObject<DiscountModel>(disc);
                    }
                }
               
                if (discount!=null)
                {
                    /**********Start**Add by vikas sharma 24.04.09*****************/
                    string Get_EDB = "";
                    temp_EDB.Value = "";
                    if (discount.EarlyStatus == "1")
                    {
                        Get_EDB = discount.EarlyBird; 
                        tempEarlyDisType.Value = discount.EarlyDisLtrPurchase;
                        temp_EDB.Value = discount.EarlyBird; 
                        tempEBPeriod.Value = discount.EarlyBird_Period;
                    }
                    else
                    {
                        Get_EDB = "0.0/0.0/0.0/0.0";
                        temp_EDB.Value = "0.0/0.0/0.0/0.0";
                        tempEBPeriod.Value = "0,0,0,0,0,0,0,0";
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message.ToString();
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
            string SubModule = "3";
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
                //string msg="UnAthourized Visit to Purchase Invoice Page";
                //	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
                Response.Redirect("../../Sysitem/AccessDeny.aspx", false);
                return;
            }

            if (Add_Flag == "0")
                btnSave.Enabled = false;
            if (Edit_Flag == "0")
                BtnEdit.Enabled = false;
            if (Del_Flag == "0")
                btnDelete.Enabled = false;

            #endregion
        }

        /// <summary>
        /// This method is used to fatch the all vendor invoice no from purchase master table and stored in hidden textbox.
        /// </summary>
        public void FatchInvoiceNo()
        {
            string invoiceNo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/FetchInvoiceNo").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    invoiceNo = JsonConvert.DeserializeObject<string>(id);
                }
            }
            tempInvoiceInfo.Value = invoiceNo;
        }

        /// <summary>
        /// this is used to fill all the invoiceno. in dropdown.
        /// </summary>
        public void fillID()
        {
            try
            {
                List<string> invoiceNo = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/purchase/FillInvoiceNo").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        invoiceNo = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if(invoiceNo!=null)
                {
                    foreach(var id in invoiceNo)
                    {
                        DropInvoiceNo.Items.Add(id);
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:fillID().  EXCEPTION: " + ex.Message + " User_ID: " + uid);
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

        protected void DropVendorID_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        //		public static string changeqtyltr(string pro,int qty)
        //		{
        //			int s2=qty;
        //			if(!pro.Trim().Equals("") && pro.IndexOf("X")>0 && s2!=0)
        //			{
        //				string [] s1=pro.Split(new char[]{'X'},pro.Length);
        //				return System.Convert.ToString(System.Convert.ToDouble(s1[0])*System.Convert.ToDouble(s1[1])*s2);
        //
        //
        //			}
        //			else
        //				return System.Convert.ToString(s2);
        //		}
        //		public static string GetGrandTotal11()
        //		{
        //			int GTotal2=0;
        //				if(txtQty1.value!="")
        //				{  
        //					GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack1.value,txtQty1.value);
        //					}
        //			if(txtQty2.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty2.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack2.value,txtQty2.value);
        //				}
        //			if(txtQty3.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty3.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack3.value,txtQty3.value);
        //				}
        //			if(txtQty4.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty4.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack4.value,txtQty4.value);
        //				}
        //			if(txtQty5.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty5.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack5.value,txtQty5.value);
        //				}
        //			if(txtQty6.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty6.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack6.value,txtQty6.value);
        //				}
        //			if(txtQty7.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty7.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack7.value,txtQty7.value);
        //				}
        //			if(txtQty8.value!="")
        //			{
        //				//	GTotal1=GTotal1+eval(document.Form1.txtQty8.value)
        //				GTotal2=GTotal2+GenUtil.changeqtyltr(txtPack8.value,txtQty8.value);
        //				}
        //			//document.Form1.txttotalqty.value=GTotal1 ;
        //			txttotalqtyltr.text="GTotal2" ;
        //			//makeRound(document.Form1.txttotalqty);
        //			//makeRound(document.Form1.txttotalqtyltr);
        //		}

        /// <summary>
        /// Fetch the supplier city and put it into a hidden fields
        /// </summary>
        public void FetchCity()
        {
            string city = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/FetchCity").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    city = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            TxtVen.Value = city;
        }

        public void SaveForReport()
        {
            Sysitem.Classes.InventoryClass obj = new InventoryClass();
            obj.InvoiceNo = Request.Form["lblInvoiceNo"].ToString();
            //Mahesh11.04.007 
            obj.InvoiceDate = Request.Form["lblInvoiceDate"];
            //obj.InvoiceDate= Session["CurrentDate"].ToString();
            obj.VendorName = DropVendorID.SelectedItem.Value.ToString();
            obj.Place = lblPlace.Value.ToString();
            obj.vendorInvoiceNo = Request.Form["txtVInnvoiceNo"].ToString();

            obj.vendorInvoiceDate = Request.Form["txtVInvoiceDate"].ToString();
            //obj.Prod1=txtProdName1.Value.ToString();
            obj.Prod2 = txtProdName2.Value.ToString();
            obj.Prod3 = txtProdName3.Value.ToString();
            obj.Prod4 = txtProdName4.Value.ToString();
            obj.Prod5 = txtProdName5.Value.ToString();
            obj.Prod6 = txtProdName6.Value.ToString();
            obj.Prod7 = txtProdName7.Value.ToString();
            obj.Prod8 = txtProdName8.Value.ToString();
            obj.Qty1 = Request.Form["txtQty1"].ToString();

            obj.Qty2 = Request.Form["txtQty2"].ToString();

            obj.Qty3 = Request.Form["txtQty3"].ToString();

            obj.Qty4 = Request.Form["txtQty4"].ToString();

            obj.Qty5 = Request.Form["txtQty5"].ToString();

            obj.Qty6 = Request.Form["txtQty6"].ToString();

            obj.Qty7 = Request.Form["txtQty7"].ToString();

            obj.Qty8 = Request.Form["txtQty8"].ToString();

            obj.Rate1 = Request.Form["txtRate1"].ToString();

            obj.Rate2 = Request.Form["txtRate2"].ToString();

            obj.Rate3 = Request.Form["txtRate3"].ToString();

            obj.Rate4 = Request.Form["txtRate4"].ToString();

            obj.Rate5 = Request.Form["txtRate5"].ToString();

            obj.Rate6 = Request.Form["txtRate6"].ToString();

            obj.Rate7 = Request.Form["txtRate7"].ToString();

            obj.Rate8 = Request.Form["txtRate8"].ToString();

            obj.Amt1 = Request.Form["txtAmount1"].ToString();

            obj.Amt2 = Request.Form["txtAmount2"].ToString();

            obj.Amt3 = Request.Form["txtAmount3"].ToString();

            obj.Amt4 = Request.Form["txtAmount4"].ToString();

            obj.Amt5 = Request.Form["txtAmount5"].ToString();

            obj.Amt6 = Request.Form["txtAmount6"].ToString();

            obj.Amt7 = Request.Form["txtAmount7"].ToString();

            obj.Amt8 = Request.Form["txtAmount8"].ToString();

            obj.Total = Request.Form["txtNetAmount"].ToString();

            obj.Promo = Request.Form["txtPromoScheme"].ToString();

            obj.Remarks = Request.Form["txtRemark"].ToString();

            obj.InsertPurchaseInvoiceDuplicate();
        }

        /// <summary>
        /// this is used to fetch productname. 
        /// </summary>
        /// <param name="ddType"></param>
        /// <param name="ddProd"></param>
        /// <param name="ddPack"></param>
        public void Type_Changed(DropDownList ddType, DropDownList ddProd, DropDownList ddPack)
        {
            try
            {
                ddProd.Items.Clear();
                ddProd.Items.Add("Select");
                ddPack.Items.Clear();
                if (ddType.SelectedItem.Value == "Fuel")
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
                sql = "select distinct Prod_Name from Products where Category='" + ddType.SelectedItem.Value + "'";
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
                CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:Type_Changed.  EXCEPTION: " + ex.Message + " User_ID: " + uid);
            }
        }

        /// <summary>
        /// this is used to fetch packtype & Rate.
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
                sql = "Select distinct Pack_Type from Products where Prod_Name='" + ddProd.SelectedItem.Value + "' and Category='" + ddType.SelectedItem.Value + "'";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    ddPack.Items.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();
                #endregion

                #region Fetch Purchase Rate Regarding Product Name		
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
                CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:Prod_Changed.  EXCEPTION: " + ex.Message + " User_ID: " + uid);
            }
        }
        /// <summary>
        /// This fucntion saves the purchase details into a purchase_details table.
        /// </summary>
        public void Save(string ProdName, string PackType, string Qty, string Rate, string Amount, string foc, string PerDisc, string PerDiscType, string StktDisc, string StktDiscType, string Discount, int sno)
        {
            
            ProductModel product = new ProductModel();
            product.Invoice_No= Request.Form["lblInvoiceNo"].ToString();
            product.ProductName = ProdName;
            product.Package_Type = PackType;
            product.Quantity= Qty;
            product.Rate = Rate;
            product.Amount = Amount;
            //********
            product.InvoiceDate= DateTime.Parse(GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + " " + DateTime.Now.TimeOfDay.ToString());
            product.FOC = foc;
            //***********
            product.Sno= sno + 1;
            product.SchPerDisc = PerDisc;
            product.SchPerDiscType = PerDiscType;
            product.StockDiscount = StktDisc;
            product.SchStktDiscType = StktDiscType;
            product.Discount = Discount;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var myContent = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/purchase/", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                }
            }

        }

        /// <summary>
        /// This method update the purchase details
        /// </summary>
        public void Save1(string ProdName, string PackType, string Qty, string Rate, string Amount, string Qty1, string Inv_date, string foc, string PerDisc, string PerDiscType, string StktDisc, string StktDiscType, string Discount, int sno)
        {
            string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);
            ProductModel product = new ProductModel();
            product.Invoice_No = arrInvoiceNo[0];
            product.ProductName = ProdName;
            product.Package_Type = PackType;
            product.Quantity = Qty;
            product.Rate = Rate;
            product.Amount = Amount;
            product.QtyTemp = Qty1;
            product.InvoiceDate = DateTime.Parse(Inv_date);
            product.FOC = foc;
            product.Sno = sno + 1;
            product.SchPerDisc = PerDisc;
            product.SchPerDiscType = PerDiscType;
            product.StockDiscount = StktDisc;
            product.SchStktDiscType = StktDiscType;
            product.Discount = Discount;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var myContent = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/purchase/UpdateProductDetails", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                }
            }

        }

        /// <summary>
        /// this is used to split the date.
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private DateTime getdate(string dat, bool to)
        {
            string[] dt = dat.IndexOf("/") > 0 ? dat.Split(new char[] { '/' }, dat.Length) : dat.Split(new char[] { '-' }, dat.Length);
            if (to)
                return new DateTime(Int32.Parse(dt[2]), Int32.Parse(dt[1]), Int32.Parse(dt[0]));
            else
                return new DateTime(Int32.Parse(dt[2]), Int32.Parse(dt[1]), Int32.Parse(dt[0]));
        }

        /// <summary>
        /// this is used to print the report & contact with printserver.
        /// </summary>
        public void print()
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
                CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:print" + uid);
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender1.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}",
                        sender1.RemoteEndPoint.ToString());
                    // Encode the data string into a byte array.
                    string home_drive = Environment.SystemDirectory;
                    home_drive = home_drive.Substring(0, 2);
                    byte[] msg = Encoding.ASCII.GetBytes(home_drive + "\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PurchaseInvoicePrePrintReport.txt<EOF>");
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
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception es)
                {
                    Console.WriteLine("Unexpected exception : {0}", es.ToString());
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaceInvice.aspx,Method:print" + ex.Message + "  EXCEPTION " + uid);
            }
        }

        /// <summary>
        /// This method is used to call the Save() function for save the purchase invoice no.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// This method is used to save the purchase invoice no.
        /// </summary>
        public void Save()
        {
            InventoryClass obj = new InventoryClass();
            try
            {
                // if lable is visible then save otherwise update the invoice.
                if (lblInvoiceNo.Visible == true)
                {
                    PurchaseModel purchase = new PurchaseModel();
                    purchase.InvoiceNo = Request.Form["lblInvoiceNo"].ToString();

                    int count = 0;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/purchase/GetInvoiceCount?invoiceNo=" + purchase.InvoiceNo).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            count = JsonConvert.DeserializeObject<int>(disc);
                        }
                    }

                    if (count > 0)
                    {
                        MessageBox.Show("Purchase Invoice AllReady Saved");
                        GetProducts();
                        FatchInvoiceNo();
                        Clear();
                        clear1();
                        GetNextInvoiceNo();
                        lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
                        return;
                    }
                    else
                    {
                        purchase.InvoiceDate = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form[lblInvoiceDate.ID.ToString()].ToString()) + " " + DateTime.Now.TimeOfDay.ToString());
                        purchase.ModeofPayment = DropModeType.SelectedItem.Value;
                        purchase.VendorName = DropVendorID.SelectedItem.Value;
                        purchase.City = lblPlace.Value.ToString();
                        purchase.VehicleNo = Request.Form["txtVehicleNo"].ToString();

                        purchase.VendorInvoiceNo = Request.Form["txtVInnvoiceNo"].ToString();

                        purchase.VendorInvoiceDate = GenUtil.str2MMDDYYYY(Request.Form["txtVInvoiceDate"].ToString());

                        purchase.GrandTotal = Request.Form["txtGrandTotal"].ToString();

                        if (DropDiscType.SelectedValue.ToString() == "Rs")
                        {
                            if (Request.Form["txtDisc"].ToString() != "")
                                purchase.Discount = Request.Form["txtDisc"].ToString();
                            else
                                purchase.Discount = "0.0";
                        }
                        else
                        {
                            if (Request.Form["txtDisc"].ToString() != "")
                                purchase.Discount = Request.Form["txtDisc"].ToString();
                            else

                                purchase.Discount = "0.0";
                        }
                       

                        purchase.DiscountType = DropDiscType.SelectedItem.Value;
                        purchase.NetAmount = Request.Form["txtNetAmount"].ToString();
                        purchase.PromoScheme = Request.Form["txtPromoScheme"].ToString();
                        purchase.Remark = Request.Form["txtRemark"].ToString();

                        purchase.EntryBy = lblEntryBy.Text.ToString();

                        purchase.EntryTime = DateTime.Parse(lblEntryTime.Text.ToString());

                        if (Request.Form["txtCashDisc"].ToString().Trim() == "")
                            purchase.CashDiscount = "0.0";
                        else
                            purchase.CashDiscount = Request.Form["txtCashDisc"].ToString().Trim();

                        purchase.CashDiscType = DropCashDiscType.SelectedItem.Value;
                        purchase.VATAmount = Request.Form["txtVAT"].ToString().Trim();
                        purchase.SGSTAmount = Request.Form["Textsgst"].ToString().Trim();
                        purchase.CGSTAmount = Request.Form["Textcgst"].ToString().Trim();

                        if (Request.Form["txtAddDis"].ToString().Trim() == "")
                            purchase.FixedDiscount = "0.0";
                        else
                            purchase.FixedDiscount = Request.Form["txtAddDis"].ToString().Trim();

                        purchase.FixedDiscountType = Request.Form["txtfixedamt"].ToString();

                        if (Request.Form["txtfoc"].ToString().Trim() == "")
                            purchase.FocDiscount = "0.0";
                        else
                            purchase.FocDiscount = Request.Form["txtfoc"].ToString().Trim();

                        purchase.FocDiscountType = dropfoc.SelectedItem.Value;

                        if (Request.Form["txtebird"].ToString().Trim() == "")
                            purchase.Ebird = "0.0";
                        else
                            purchase.Ebird = Request.Form["txtebird"].ToString().Trim();

                        purchase.Tradeless = txttradeless.Text.ToString();
                        purchase.Birdless = txtbirdless.Text.ToString();

                        if (Request.Form["txtebirdamt"].ToString().Trim() == "")
                            purchase.EbirdDiscount = "0.0";
                        else
                            purchase.EbirdDiscount = Request.Form["txtebirdamt"].ToString().Trim();


                        purchase.Tradeval = "0";
                        if (Request.Form["txttradedisamt"].ToString().Trim() == "")
                            purchase.TradeDiscount = "0.0";
                        else
                            purchase.TradeDiscount = Request.Form["txttradedisamt"].ToString().Trim();

                        if (Request.Form["txttotalqtyltr1"].ToString().Trim() == "")
                            purchase.Totalqtyltr = "0.0";
                        else
                            purchase.Totalqtyltr = Request.Form["txttotalqtyltr1"].ToString().Trim();

                        /********Add by vikas 5.11.2012 ************************/
                        if (Request.Form["txtfixdisc"].ToString().Trim() == "")
                            purchase.NewFixeddisc = "0.0";
                        else
                            purchase.NewFixeddisc = Request.Form["txtfixdisc"].ToString().Trim();

                        purchase.NewFixeddiscAmount = Request.Form["txtfixdiscamount"].ToString().Trim();

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            var myContent = JsonConvert.SerializeObject(purchase);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/purchase/PostPurchaseMaster", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                                var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                            }
                        }

                        HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                        TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                        TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
                        TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
                        HtmlInputHidden[] batch = { bat0, bat1, bat2, bat3, bat4, bat5, bat6, bat7, bat8, bat9, bat10, bat11, bat12, bat13, bat14, bat15, bat16, bat17, bat18, bat19 };
                        HtmlInputHidden[] tempSchDis = { tempSchDis1, tempSchDis2, tempSchDis3, tempSchDis4, tempSchDis5, tempSchDis6, tempSchDis7, tempSchDis8, tempSchDis9, tempSchDis10, tempSchDis11, tempSchDis12, tempSchDis13, tempSchDis14, tempSchDis15, tempSchDis16, tempSchDis17, tempSchDis18, tempSchDis19, tempSchDis20 };
                        HtmlInputHidden[] tempStktSchDis = { tempStktSchDis1, tempStktSchDis2, tempStktSchDis3, tempStktSchDis4, tempStktSchDis5, tempStktSchDis6, tempStktSchDis7, tempStktSchDis8, tempStktSchDis9, tempStktSchDis10, tempStktSchDis11, tempStktSchDis12, tempStktSchDis13, tempStktSchDis14, tempStktSchDis15, tempStktSchDis16, tempStktSchDis17, tempStktSchDis18, tempStktSchDis19, tempStktSchDis20 };
                        HtmlInputHidden[] tempDiscount = { tempSchDiscount1, tempSchDiscount2, tempSchDiscount3, tempSchDiscount4, tempSchDiscount5, tempSchDiscount6, tempSchDiscount7, tempSchDiscount8, tempSchDiscount9, tempSchDiscount10, tempSchDiscount11, tempSchDiscount12, tempSchDiscount13, tempSchDiscount14, tempSchDiscount15, tempSchDiscount16, tempSchDiscount17, tempSchDiscount18, tempSchDiscount19, tempSchDiscount20 };

                        HtmlInputHidden[] tempSchAddDis = { tempSchAddDis1, tempSchAddDis2, tempSchAddDis3, tempSchAddDis4, tempSchAddDis5, tempSchAddDis6, tempSchAddDis7, tempSchAddDis8, tempSchAddDis9, tempSchAddDis10, tempSchAddDis11, tempSchAddDis12, tempSchAddDis13, tempSchAddDis14, tempSchAddDis15, tempSchAddDis16, tempSchAddDis17, tempSchAddDis18, tempSchAddDis19, tempSchAddDis20 };             //Add by vikas 03.07.09
                                                                                                                                                                                                                                                                                                                                                                                                                     //************************

                        CheckBox[] chkfoc = { chkfoc1, chkfoc2, chkfoc3, chkfoc4, chkfoc5, chkfoc6, chkfoc7, chkfoc8, chkfoc9, chkfoc10, chkfoc11, chkfoc12, chkfoc13, chkfoc14, chkfoc15, chkfoc16, chkfoc17, chkfoc18, chkfoc19, chkfoc20 };
                        string[] foc = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        for (int j = 0; j < ProdType.Length; j++)
                        {
                            if (chkfoc[j].Checked)
                                foc[j] = "1";
                            else
                                foc[j] = "0";
                        }
                        for (int j = 0; j < ProdType.Length; j++)
                        {
                            if (Request.Form[Rate[j].ID.ToString()].ToString() == "" || Request.Form[Rate[j].ID.ToString()].ToString() == "0")
                                continue;
                            string[] arrName = ProdType[j].Value.Split(new char[] { ':' }, ProdType[j].Value.Length);
                            string[] arrPerDisc = new string[2];
                            string[] arrStktDisc = new string[2];
                            string perdisc = tempSchDis[j].Value.ToString();
                            string stktdisc = tempStktSchDis[j].Value.ToString();
                            if (tempSchDis[j].Value != "" && tempSchDis[j].Value.IndexOf(":") > 0)
                                arrPerDisc = perdisc.Split(new char[] { ':' }, perdisc.Length);
                            else
                            {
                                arrPerDisc[0] = "";
                                arrPerDisc[1] = "";
                            }
                            if (tempStktSchDis[j].Value != "" && tempStktSchDis[j].Value.IndexOf(":") > 0)
                                arrStktDisc = stktdisc.Split(new char[] { ':' }, stktdisc.Length);
                            else
                            {
                                arrStktDisc[0] = "";
                                arrStktDisc[1] = "";
                            }

                            Save(arrName[1].ToString(), arrName[2].ToString(), Request.Form[Qty[j].ID.ToString()].ToString(), Request.Form[Rate[j].ID.ToString()].ToString(), Request.Form[Amount[j].ID.ToString()], foc[j].ToString(), arrPerDisc[0], arrPerDisc[1], arrStktDisc[0], arrStktDisc[1], tempDiscount[j].Value, j);
                        }

                        /*** **** Insert Batch No ********************/

                        List<ProductModel> products = new List<ProductModel>();
                        for (int i = 0, j = 1; i < 11; i++, j++)
                        {
                            if (Request.Params.Get("chkbatch" + j) == "on" && batch[i].Value != "")
                            {
                                ProductModel product = new ProductModel();
                                product.Batch = batch[i].Value;
                                product.ProductName = ProdType[i].Value;
                                product.Invoice_No = Request.Form["lblInvoiceNo"].ToString();
                                product.InvoiceDate = DateTime.Parse(GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + " " + DateTime.Now.TimeOfDay.ToString());
                                products.Add(product);
                            }
                        }

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            var myContent = JsonConvert.SerializeObject(products);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/purchase/PostBatchTransaction", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                                var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                            }
                        }
                        /**********end comment by vikas 17.06.09*******************************************/

                        PrePrintReport();
                        Clear();
                        clear1();
                        GetProducts();
                        FatchInvoiceNo();
                        GetNextInvoiceNo();
                        lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
                        MessageBox.Show("Purchase Invoice Saved");
                        CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnSaved_Click,Class:PartiesClass.cs" + " Fuel Purchase Invoise for  Invoice No." + obj.Invoice_No + " ," + "for Vender Name  " + obj.Vendor_Name + "on Date " + obj.Vendor_Name + " and NetAmount  " + obj.Net_Amount + "  is Saved " + " userid " + uid);
                    }
                }
                else
                {
                    string strChck = "";
                    strChck = DropInvoiceNo.SelectedItem.Value.ToString();
                    if (strChck.Equals("Select"))
                    {
                        MessageBox.Show("Please Select Invoice No");
                    }
                    else
                    {
                        PurchaseModel updatePurchase = new PurchaseModel();

                        string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);
                        string temp = "";
                        updatePurchase.InvoiceNo = arrInvoiceNo[0];
                        updatePurchase.InvoiceDate = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()) + " " + DateTime.Now.TimeOfDay.ToString());

                        updatePurchase.ModeofPayment = DropModeType.SelectedItem.Value;
                        updatePurchase.VendorName = DropVendorID.SelectedItem.Value;
                        updatePurchase.City = lblPlace.Value.ToString();
                        updatePurchase.VehicleNo = Request.Form["txtVehicleNo"].ToString();

                        updatePurchase.VendorInvoiceNo = Request.Form["txtVInnvoiceNo"].ToString();

                        updatePurchase.VendorInvoiceDate = GenUtil.str2MMDDYYYY(Request.Form["txtVInvoiceDate"].ToString());

                        updatePurchase.GrandTotal = Request.Form["txtGrandTotal"].ToString();
                        
                        if (DropDiscType.SelectedValue.ToString() == "Rs")
                        {
                            if (Request.Form["txtDisc"].ToString() != "")
                                updatePurchase.Discount = Request.Form["txtDisc"].ToString();
                            else

                                updatePurchase.Discount = "0.0";
                        }
                        else
                        {
                            if (Request.Form["txtDisc"].ToString() != "")
                                updatePurchase.Discount = Request.Form["txtDisc"].ToString();
                            else

                                updatePurchase.Discount = "0.0";
                        }
                       
                        updatePurchase.DiscountType = DropDiscType.SelectedItem.Value;
                        updatePurchase.NetAmount = Request.Form["txtNetAmount"].ToString();

                        updatePurchase.PromoScheme = Request.Form["txtPromoScheme"].ToString();

                        updatePurchase.Remark = Request.Form["txtRemark"].ToString();

                        updatePurchase.EntryBy = lblEntryBy.Text.ToString();

                        updatePurchase.EntryTime = DateTime.Parse(lblEntryTime.Text.ToString());

                        if (Request.Form["txtCashDisc"].ToString().Trim() == "")
                            updatePurchase.CashDiscount = "0.0";
                        else
                            updatePurchase.CashDiscount = Request.Form["txtCashDisc"].ToString().Trim();

                        updatePurchase.CashDiscType = DropCashDiscType.SelectedItem.Value;
                        updatePurchase.VATAmount = Request.Form["txtVAT"].ToString().Trim();
                        updatePurchase.SGSTAmount = Request.Form["Textsgst"].ToString().Trim();
                        updatePurchase.CGSTAmount = Request.Form["Textcgst"].ToString().Trim();
                        
                        if (Request.Form["txtAddDis"].ToString().Trim() == "")
                            updatePurchase.FixedDiscount = "0.0";
                        else
                            updatePurchase.FixedDiscount = Request.Form["txtAddDis"].ToString().Trim();
                        

                        updatePurchase.FixedDiscountType = Request.Form["txtfixedamt"].ToString();

                        if (Request.Form["txtfoc"].ToString().Trim() == "")
                            updatePurchase.FocDiscount = "0.0";
                        else
                            updatePurchase.FocDiscount = Request.Form["txtfoc"].ToString().Trim();

                        updatePurchase.FocDiscountType = dropfoc.SelectedItem.Value;

                        if (Request.Form["txtebird"].ToString().Trim() == "")
                            updatePurchase.Ebird = "0.0";
                        else
                            updatePurchase.Ebird = Request.Form["txtebird"].ToString().Trim();

                        if (Request.Form["txtebirdamt"].ToString().Trim() == "")
                            updatePurchase.EbirdDiscount = "0.0";
                        else
                            updatePurchase.EbirdDiscount = Request.Form["txtebirdamt"].ToString().Trim();

                        updatePurchase.Tradeless = txttradeless.Text.ToString();
                        updatePurchase.Birdless = txtbirdless.Text.ToString();
                        updatePurchase.Tradeval = "0";
                        if (Request.Form["txttradedisamt"].ToString().Trim() == "")
                            updatePurchase.TradeDiscount = "0.0";
                        else
                            updatePurchase.TradeDiscount = Request.Form["txttradedisamt"].ToString().Trim();


                        if (Request.Form["txttotalqtyltr1"].ToString().Trim() == "")
                            updatePurchase.Totalqtyltr = "0.0";
                        else
                            updatePurchase.Totalqtyltr = Request.Form["txttotalqtyltr1"].ToString().Trim();

                        /********Add by vikas 5.11.2012 ************************/
                        if (Request.Form["txtfixdisc"].ToString().Trim() == "")
                            updatePurchase.NewFixeddisc = "0.0";
                        else
                            updatePurchase.NewFixeddisc = Request.Form["txtfixdisc"].ToString().Trim();

                        updatePurchase.NewFixeddiscAmount = Request.Form["txtfixdiscamount"].ToString().Trim();
                        /************end********************/

                        UpdateProductQty();
                        updatePurchase.VendorID = DropVendorID.SelectedItem.Text;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            var myContent = JsonConvert.SerializeObject(updatePurchase);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/purchase/UpdateMasterPurchase", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                                var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                            }
                        }
                       

                        HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                        TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                        TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
                        TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
                        TextBox[] Quantity = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12, txtTempQty13, txtTempQty14, txtTempQty15, txtTempQty16, txtTempQty17, txtTempQty18, txtTempQty19, txtTempQty20 };
                        HtmlInputHidden[] batch = { bat0, bat1, bat2, bat3, bat4, bat5, bat6, bat7, bat8, bat9, bat10, bat11, bat12, bat13, bat14, bat15, bat16, bat17, bat18, bat19 };
                        HtmlInputHidden[] tempSchDis = { tempSchDis1, tempSchDis2, tempSchDis3, tempSchDis4, tempSchDis5, tempSchDis6, tempSchDis7, tempSchDis8, tempSchDis9, tempSchDis10, tempSchDis11, tempSchDis12, tempSchDis13, tempSchDis14, tempSchDis15, tempSchDis16, tempSchDis17, tempSchDis18, tempSchDis19, tempSchDis20 };
                        HtmlInputHidden[] tempStktSchDis = { tempStktSchDis1, tempStktSchDis2, tempStktSchDis3, tempStktSchDis4, tempStktSchDis5, tempStktSchDis6, tempStktSchDis7, tempStktSchDis8, tempStktSchDis9, tempStktSchDis10, tempStktSchDis11, tempStktSchDis12, tempStktSchDis13, tempStktSchDis14, tempStktSchDis15, tempStktSchDis16, tempStktSchDis17, tempStktSchDis18, tempStktSchDis19, tempStktSchDis20 };
                        HtmlInputHidden[] tempDiscount = { tempSchDiscount1, tempSchDiscount2, tempSchDiscount3, tempSchDiscount4, tempSchDiscount5, tempSchDiscount6, tempSchDiscount7, tempSchDiscount8, tempSchDiscount9, tempSchDiscount10, tempSchDiscount11, tempSchDiscount12, tempSchDiscount13, tempSchDiscount14, tempSchDiscount15, tempSchDiscount16, tempSchDiscount17, tempSchDiscount18, tempSchDiscount19, tempSchDiscount20 };
                        CheckBox[] chkfoc = { chkfoc1, chkfoc2, chkfoc3, chkfoc4, chkfoc5, chkfoc6, chkfoc7, chkfoc8, chkfoc9, chkfoc10, chkfoc11, chkfoc12, chkfoc13, chkfoc14, chkfoc15, chkfoc16, chkfoc17, chkfoc18, chkfoc19, chkfoc20 };

                        string[] foc = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                        for (int j = 0; j < ProdType.Length; j++)
                        {
                            if (chkfoc[j].Checked)
                                foc[j] = "1";
                            else
                                foc[j] = "0";
                        }

                        for (int j = 0; j < ProdType.Length; j++)
                        {
                            if (Request.Form[Rate[j].ID.ToString()].ToString() == "" || Request.Form[Rate[j].ID.ToString()].ToString() == "0")
                                continue;
                            string[] arrName = ProdType[j].Value.Split(new char[] { ':' }, ProdType[j].Value.Length);
                            string[] arrPerDisc = new string[2];
                            string[] arrStktDisc = new string[2];
                            string perdisc = tempSchDis[j].Value.ToString();
                            string stktdisc = tempStktSchDis[j].Value.ToString();
                            if (tempSchDis[j].Value != "" && tempSchDis[j].Value.IndexOf(":") > 0)
                                arrPerDisc = perdisc.Split(new char[] { ':' }, perdisc.Length);
                            else
                            {
                                arrPerDisc[0] = "";
                                arrPerDisc[1] = "";
                            }
                            if (tempStktSchDis[j].Value != "" && tempStktSchDis[j].Value.IndexOf(":") > 0)
                                arrStktDisc = stktdisc.Split(new char[] { ':' }, stktdisc.Length);
                            else
                            {
                                arrStktDisc[0] = "";
                                arrStktDisc[1] = "";
                            }

                            temp = Request.Form[Qty[j].ID.ToString()].ToString();


                            Save1(arrName[1].ToString(), arrName[2].ToString(), Request.Form[Qty[j].ID.ToString()].ToString(), Request.Form[Rate[j].ID.ToString()].ToString(), Request.Form[Amount[j].ID.ToString()].ToString(), temp, GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString() + " " + DateTime.Now.TimeOfDay.ToString()), foc[j].ToString(), arrPerDisc[0], arrPerDisc[1], arrStktDisc[0], arrStktDisc[1], tempDiscount[j].Value, j);

                            InsertBatchNo(arrName[1].ToString(), arrName[2].ToString(), Request.Form[Qty[j].ID.ToString()].ToString(), batch[j].Value, j);

                        }
                        PrePrintReport();
                        SeqStockMaster();
                        CustomerUpdate();
                        MessageBox.Show("Purchase Invoice Updated");
                        CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnSaved_Click,Class:PartiesClass.cs" + " Fuel Purchase Invoise for  Invoice No." + obj.Invoice_No + " ," + "for Vender Name  " + obj.Vendor_Name + "on Date " + obj.Vendor_Name + " and NetAmount  " + obj.Net_Amount + "  is Updated. " + " userid " + uid);
                        DropInvoiceNo.SelectedIndex = 0;
                        DropInvoiceNo.Visible = false;
                        lblInvoiceNo.Visible = true;
                        Clear();
                        clear1();
                        lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
                    }
                }
                FlagPrint = false;
                checkPrevileges();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnSaved_Click,Class:PartiesClass.cs" + " Fuel Purchase Invoise for  Invoice No." + obj.Invoice_No + " ," + "for Vender Name  " + obj.Vendor_Name + "on Date " + obj.Vendor_Name + " and NetAmount  " + obj.Net_Amount + "  EXCEPTION   " + ex.Message + "  userid " + uid);
            }
        }

        //static string Prod_ID="";

        /// <summary>
        /// This method is used to save the product with batch wise information in other table.
        /// </summary>
        /*public void GetBatch()
		{
			try
			{
				InventoryClass objprod = new InventoryClass();
				int x=0,batch_id=0,SNo=0,BID=0;
				SqlDataReader rdr = null;
				rdr = objprod.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null)
						SNo=int.Parse(rdr.GetValue(0).ToString());
					else
						SNo=1;
				}
				else
					SNo=1;
				rdr.Close();
				if(Request.Params.Get("BatchNo")=="Yes" && batch[1].Value!="")
				{
					DateTime dt;
					dbobj.SelectQuery("select acc_date_from from organisation",ref rdr);
					if(rdr.Read())
					{
						dt=System.Convert.ToDateTime(rdr.GetValue(0).ToString());
					}
					else
					{
						MessageBox.Show("First Fill The Organisation Form");
						return;
					}
					string op="0";
					rdr = objprod.GetRecordSet("select max(Batch_ID) from BatchNo");
					if(rdr.Read())
					{
						if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null)
							batch_id=int.Parse(rdr.GetValue(0).ToString());
						else
							batch_id=0;
					}
					else
						batch_id=0;
					rdr.Close();
				
					if(txtOp_Stock.Text!="")
						op=txtOp_Stock.Text;
					int tot_bat_qty=0;								//add by vikas 11.06.09
					if(lblProdID.Visible==true)
					{


						string[] arr=batch.Value.Split(new char[] {','},batch.Value.Length);
						for(int n=0;n<arr.Length;n+=3)
						{
							if(!arr[n].ToString().Equals("''"))
							{
								string BNo=arr[n].ToString();
								string BQty=arr[n+2].ToString();
								dbobj.Insert_or_Update("insert into BatchNo values('"+(++batch_id)+"',"+BNo+",'"+lblProdID.Text+"','"+dt+"',"+BQty+","+lblProdID.Text+")",ref x);
								dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+lblProdID.Text+"','Opening Stock','"+dt+"','"+lblProdID.Text+"',"+batch_id+","+BQty+","+BQty+")",ref x);
								//coment by vikas 11.06.09 dbobj.Insert_or_Update("insert into StockMaster_Batch values("+lblProdID.Text+",'"+batch_id+"','"+dt+"',0,"+BQty+",0,"+BQty+",0,0)",ref x);
								dbobj.Insert_or_Update("insert into StockMaster_Batch values("+lblProdID.Text+",'"+batch_id+"','"+dt+"',"+BQty+",0,0,"+BQty+",0,0)",ref x);
								
								BQty=BQty.Substring(1,(BQty.Length)-2);
								tot_bat_qty=+Convert.ToInt32(BQty);    //Add by vikas 11.06.09
							}
							else
							{
								//***********Add by vikas 11.06.09***************
								if(!arr[n-3].ToString().Equals("''"))
								{
									if(Convert.ToInt32(txtOp_Stock.Text)>tot_bat_qty)
									{
										int NQty=Convert.ToInt32(txtOp_Stock.Text)-tot_bat_qty;
										dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+lblProdID.Text+"','Opening Stock','"+dt+"','"+lblProdID.Text+"','','"+NQty+"','"+NQty+"')",ref x);
										dbobj.Insert_or_Update("insert into StockMaster_Batch values("+lblProdID.Text+",'','"+dt+"','"+NQty+"',0,0,'"+NQty+"',0,0)",ref x);
									}
								}
								//**************************
								break;
							}
						}
					
					}
					else
					{
						dbobj.SelectQuery("select batch_id from Batch_Transaction where prod_id='"+Prod_ID+"' and trans_id='"+Prod_ID+"'",ref rdr);
						string[] bat=new string[10];
						int q=0;
						while(rdr.Read())
						{
							bat[q]=rdr.GetValue(0).ToString();
							q++;
						}
						rdr.Close();
						dbobj.Insert_or_Update("delete from Batch_Transaction where trans_ID='"+Prod_ID+"' and prod_id='"+Prod_ID+"' and trans_type='Opening Stock'",ref x);
						//************Add By Vikas 12.06.09**********************************

						rdr = objprod.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null)
								SNo=int.Parse(rdr.GetValue(0).ToString());
							else
								SNo=1;
						}
						else
							SNo=1;
						rdr.Close();

						//**********************************************

						rdr=objprod.GetRecordSet("select * from BatchNo where Trans_No='"+Prod_ID+"' order by Batch_ID");
						if(rdr.Read())
						{
							BID=int.Parse(rdr["Batch_ID"].ToString());
						}
						rdr.Close();
						string[] arr=batch.Value.Split(new char[] {','},batch.Value.Length);
						for(int n=0,p=0;n<arr.Length;n+=3,p++)
						{
							if(!arr[n].ToString().Equals("''"))
							{
								string BNo=arr[n].ToString();
								string BQty=arr[n+2].ToString();
								rdr = objprod.GetRecordSet("select * from BatchNo where Trans_No='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"");
								if(rdr.HasRows)
								{
									dbobj.Insert_or_Update("update BatchNo set Batch_No="+BNo.ToString()+",Prod_ID='"+Prod_ID+"',Date='"+dt+"',Qty="+BQty.ToString()+" where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);
									//coment by vikas 11.06.09 dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+arr[n+1].ToString()+","+BQty+","+BQty+")",ref x);
									dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+arr[n+1].ToString()+","+BQty+","+BQty+")",ref x);
									dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='"+dt+"',opening_stock="+BQty+",closing_stock="+BQty+" where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);

									//***********Add by vikas 11.06.09***************
									BQty=BQty.Substring(1,(BQty.Length)-2);
									tot_bat_qty+=Convert.ToInt32(BQty); 

									//**************************
									

								}
								else
								{
									dbobj.Insert_or_Update("insert into BatchNo values("+(++batch_id)+","+BNo.ToString()+",'"+Prod_ID+"','"+dt+"',"+BQty.ToString()+",'"+Prod_ID+"')",ref x);
									//dbobj.Insert_or_Update("update BatchNo set Batch_No="+BNo.ToString()+",Prod_ID='"+Prod_ID+"',Date='"+dt+"',Qty="+BQty.ToString()+" where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);
									dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+batch_id+","+BQty+","+BQty+")",ref x);
									//dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+batch_id+","+BQty+","+BQty+")",ref x);
									dbobj.Insert_or_Update("insert into StockMaster_Batch values("+Prod_ID+",'"+batch_id+"','"+dt+"',"+BQty+",0,0,"+BQty+",0,0)",ref x);
									//dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='"+dt+"',opening_stock="+BQty+",closing_stock="+BQty+" where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);

									/***********Add by vikas 11.06.09**************
									dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+arr[n+1].ToString()+","+BQty+","+BQty+")",ref x);
									dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='"+dt+"',opening_stock="+BQty+",closing_stock="+BQty+" where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
									//**************************
									//***********Add by vikas 11.06.09***************
									BQty=BQty.Substring(1,(BQty.Length)-2);
									tot_bat_qty+=Convert.ToInt32(BQty); 

									//**************************

								}
								rdr.Close();
							}
							else if(arr[n+1].ToString()!="''")
							{
								//coment by vikas 12.06.09 dbobj.Insert_or_Update("update BatchNo set Qty=0 where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);
								//coment by vikas 12.06.09 dbobj.Insert_or_Update("update StockMaster_Batch set opening_stock=0,closing_stock=0 where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
								dbobj.Insert_or_Update("delete from BatchNo where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);                    // Coment by vikas 12.06.09
								dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);         // Coment by vikas 12.06.09
							}
							else
							{
								//***********Add by vikas 11.06.09***************
								if(!arr[n-3].ToString().Equals("''"))
								{
									if(Convert.ToInt32(txtOp_Stock.Text)>tot_bat_qty)
									{
										int NQty=Convert.ToInt32(txtOp_Stock.Text)-tot_bat_qty;
										dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+arr[n+1].ToString()+","+NQty+","+NQty+")",ref x);

										rdr = objprod.GetRecordSet("select * from BatchNo where Trans_No='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"");
										if(rdr.HasRows)
										{
											dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='"+dt+"',opening_stock="+NQty+",closing_stock="+NQty+" where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
										}
										else
										{
											dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
											dbobj.Insert_or_Update("insert into StockMaster_Batch values("+Prod_ID+",'','"+dt+"',"+NQty+",0,0,"+NQty+",0,0)",ref x);
										}
									}
									else
									{
										dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
									}
								}
								//**************************
								break;
							}
						}
					}
				}
				else  // 12.06.09
				{

					dbobj.SelectQuery("select batch_id from Batch_Transaction where prod_id='"+Prod_ID+"' and trans_id='"+Prod_ID+"'",ref rdr);
					string[] bat=new string[10];
					int q=0;
					while(rdr.Read())
					{
						bat[q]=rdr.GetValue(0).ToString();
						q++;
					}
					rdr.Close();
					dbobj.Insert_or_Update("delete from Batch_Transaction where prod_id='"+Prod_ID+"' and Trans_id="+Prod_ID+"",ref x);					// Coment by vikas 12.06.09
					dbobj.Insert_or_Update("delete from Batchno where prod_id='"+Prod_ID+"' and Trans_No="+Prod_ID+"",ref x);								// Coment by vikas 12.06.09

					for(int i=0;i<q;i++)
					{
						dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+bat[i],ref x);						// Coment by vikas 12.06.09
					}
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method GetBatch() "+"  EXCEPTION  "+  ex.Message+" userid is   "+uid);
			}
		}*/



        /// <summary>
        /// This method clear the form.
        /// </summary>
        public void clear1()
        {

            TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
            TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
            TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };

            for (int i = 0; i < Qty.Length; i++)
            {

                Qty[i].Enabled = true;
                Rate[i].Enabled = true;
                Amount[i].Enabled = true;

            }
            lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
        }

        /// <summary>
        /// this is used to fetch salesRate Regarding Productname.
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
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:Pack_Changed  EXCEPTION   " + ex.Message + ". Userid " + uid);
            }
        }

        private void DropPack1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack4_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack5_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack6_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack7_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropPack8_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private string GetString(string str, int maxlen, string spc)
        {
            return str + spc.Substring(0, maxlen > str.Length ? maxlen - str.Length : str.Length - maxlen);
        }
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
        /// this is used to make report in printing format.
        /// </summary>
        public void reportmaking()
        {
            try
            {
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PurchaseInvoiceReport.txt";
                StreamWriter sw = new StreamWriter(path);
                string info = "", info1 = "";
                string strInvNo = "";
                string strDiscType = "";
                double disc_amt = 0;
                string msg = "";
                double ET = 0, TD = 0, FOC = 0, FD = 0;
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
                string des = "----------------------------------------------------------------------------";
                string Address = GenUtil.GetAddress();
                string[] addr = Address.Split(new char[] { ':' }, Address.Length);
                sw.WriteLine(GenUtil.GetCenterAddr(addr[0], des.Length).ToUpper());
                sw.WriteLine(GenUtil.GetCenterAddr(addr[1] + addr[2], des.Length));
                sw.WriteLine(GenUtil.GetCenterAddr("Tin No : " + addr[3], des.Length));
                sw.WriteLine(des);
                //**********
                info = " {0,-40:S} {1,10:F}  {2,10:F} {3,10:F}";
                info1 = " {0,40:S} {1,-21:F}  {2,10:F}";
                sw.WriteLine(GenUtil.GetCenterAddr("==================", des.Length));
                sw.WriteLine(GenUtil.GetCenterAddr("PURCHASE INVOICE", des.Length));
                sw.WriteLine(GenUtil.GetCenterAddr("==================", des.Length));
                if (lblInvoiceNo.Visible == true)
                    //strInvNo = lblInvoiceNo.Text;
                    strInvNo = Request.Form["txtVInnvoiceNo"].ToString();
                else

                    strInvNo = DropInvoiceNo.SelectedItem.Text;
                //sw.WriteLine(" Invoice No : " +strInvNo+ "                               Date : " +lblInvoiceDate.Text.ToString());
                //Mahesh11.04.07 
                sw.WriteLine(info, "Invoice No : " + strInvNo, "", "Date :", Request.Form["lblInvoiceDate"].ToString());
                //sw.WriteLine(info,"Invoice No : " +strInvNo,"","Date :",Session["CurrentDate"].ToString());
                sw.WriteLine("+--------------------------------------------------------------------------+");
                sw.WriteLine(" Vendor Name         :  " + DropVendorID.SelectedItem.Value);
                sw.WriteLine(" Place               :  " + lblPlace.Value);
                sw.WriteLine(" Vendor Invoice No   :  " + Request.Form["txtVInnvoiceNo"].ToString());

                sw.WriteLine(" Vendor Invoice Date :  " + Request.Form["txtVInvoiceDate"].ToString());

                sw.WriteLine("+----------------------------------------+-----------+----------+----------+");
                sw.WriteLine("|            Product Name                | Quantity  |   Rate   |  Amount  |");
                sw.WriteLine("+----------------------------------------+-----------+----------+----------+");

                /*sw.WriteLine(info,GenUtil.TrimLength(txtProdName1.Value+" "+txtPack1.Value,40),txtQty1.Text,GenUtil.strNumericFormat(txtRate1.Text),GenUtil.strNumericFormat(txtAmount1.Text.ToString().Trim()));
				if(!txtProdName2.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName2.Value+" "+txtPack2.Value,40),txtQty2.Text,GenUtil.strNumericFormat(txtRate2.Text),GenUtil.strNumericFormat(txtAmount2.Text.ToString().Trim()));
				if(!txtProdName3.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName3.Value+" "+txtPack3.Value,40),txtQty3.Text,GenUtil.strNumericFormat(txtRate3.Text),GenUtil.strNumericFormat(txtAmount3.Text.ToString().Trim()));
				if(!txtProdName4.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName4.Value+" "+txtPack4.Value,40),txtQty4.Text,GenUtil.strNumericFormat(txtRate4.Text),GenUtil.strNumericFormat(txtAmount4.Text.ToString().Trim()));
				if(!txtProdName5.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName5.Value+" "+txtPack5.Value,40),txtQty5.Text,GenUtil.strNumericFormat(txtRate5.Text),GenUtil.strNumericFormat(txtAmount5.Text.ToString().Trim()));
				if(!txtProdName6.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName6.Value+" "+txtPack6.Value,40),txtQty6.Text,GenUtil.strNumericFormat(txtRate6.Text),GenUtil.strNumericFormat(txtAmount6.Text.ToString().Trim()));
				if(!txtProdName7.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName7.Value+" "+txtPack7.Value,40),txtQty7.Text,GenUtil.strNumericFormat(txtRate7.Text),GenUtil.strNumericFormat(txtAmount7.Text.ToString().Trim()));
				if(!txtProdName8.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName8.Value+" "+txtPack8.Value,40),txtQty8.Text,GenUtil.strNumericFormat(txtRate8.Text),GenUtil.strNumericFormat(txtAmount8.Text.ToString().Trim()));
				if(!txtProdName9.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName9.Value+" "+txtPack9.Value,40),txtQty9.Text,GenUtil.strNumericFormat(txtRate9.Text),GenUtil.strNumericFormat(txtAmount9.Text.ToString().Trim()));
				if(!txtProdName10.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName10.Value+" "+txtPack10.Value,40),txtQty10.Text,GenUtil.strNumericFormat(txtRate10.Text),GenUtil.strNumericFormat(txtAmount10.Text.ToString().Trim()));
				if(!txtProdName11.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName11.Value+" "+txtPack11.Value,40),txtQty11.Text,GenUtil.strNumericFormat(txtRate11.Text),GenUtil.strNumericFormat(txtAmount11.Text.ToString().Trim()));
				if(!txtProdName12.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName12.Value+" "+txtPack12.Value,40),txtQty12.Text,GenUtil.strNumericFormat(txtRate12.Text),GenUtil.strNumericFormat(txtAmount12.Text.ToString().Trim()));
				if(!txtProdName13.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName13.Value+" "+txtPack13.Value,40),txtQty13.Text,GenUtil.strNumericFormat(txtRate13.Text),GenUtil.strNumericFormat(txtAmount13.Text.ToString().Trim()));
				if(!txtProdName14.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName14.Value+" "+txtPack14.Value,40),txtQty14.Text,GenUtil.strNumericFormat(txtRate14.Text),GenUtil.strNumericFormat(txtAmount14.Text.ToString().Trim()));
				if(!txtProdName15.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName15.Value+" "+txtPack15.Value,40),txtQty15.Text,GenUtil.strNumericFormat(txtRate15.Text),GenUtil.strNumericFormat(txtAmount15.Text.ToString().Trim()));
				if(!txtProdName16.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName16.Value+" "+txtPack16.Value,40),txtQty16.Text,GenUtil.strNumericFormat(txtRate16.Text),GenUtil.strNumericFormat(txtAmount16.Text.ToString().Trim()));
				if(!txtProdName17.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName17.Value+" "+txtPack17.Value,40),txtQty17.Text,GenUtil.strNumericFormat(txtRate17.Text),GenUtil.strNumericFormat(txtAmount17.Text.ToString().Trim()));
				if(!txtProdName18.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName18.Value+" "+txtPack18.Value,40),txtQty18.Text,GenUtil.strNumericFormat(txtRate18.Text),GenUtil.strNumericFormat(txtAmount18.Text.ToString().Trim()));
				if(!txtProdName19.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName19.Value+" "+txtPack19.Value,40),txtQty19.Text,GenUtil.strNumericFormat(txtRate19.Text),GenUtil.strNumericFormat(txtAmount19.Text.ToString().Trim()));
				if(!txtProdName20.Value.Equals(""))
					sw.WriteLine(info,GenUtil.TrimLength(txtProdName20.Value+" "+txtPack20.Value,40),txtQty20.Text,GenUtil.strNumericFormat(txtRate20.Text),GenUtil.strNumericFormat(txtAmount20.Text.ToString().Trim()));
				*/
                //DropDownList[] ProdType={DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20};
                HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
                TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
                //sw.WriteLine(info,GenUtil.TrimLength(txtProdName1.Value+" "+txtPack1.Value,40),txtQty1.Text,GenUtil.strNumericFormat(txtRate1.Text),GenUtil.strNumericFormat(txtAmount1.Text.ToString().Trim()));
                string[] arrName = new string[3];
                for (int i = 0; i < 20; i++)
                {
                    if (!ProdType[i].Value.Equals("Type") && (Request.Form[Qty[i].ID.ToString()].ToString() != "" || Request.Form[Qty[i].ID.ToString()].ToString() != "0"))
                    {
                        arrName = ProdType[i].Value.Split(new char[] { ':' }, ProdType[i].Value.Length);
                        sw.WriteLine(info, arrName[1].ToString() + ":" + arrName[2].ToString(), Request.Form[Qty[i].ID.ToString()].ToString(), GenUtil.strNumericFormat(Request.Form[Rate[i].ID.ToString()].ToString()), GenUtil.strNumericFormat(Request.Form[Amount[i].ID.ToString()].ToString().Trim()));

                    }
                }

                sw.WriteLine("+----------------------------------------+-----------+----------+----------+");

                //sw.WriteLine("                               Grand Total           : {0,10:F}" , GenUtil.strNumericFormat(txtGrandTotal.Text.ToString() ));
                sw.WriteLine(info1, "", "Grand Total", GenUtil.strNumericFormat(Request.Form["txtGrandTotal"].ToString()));
                //**********Calculation of Entry Tax **************
                //********
                double sdisc = 0;
                double disc_amt1 = 0;
                string msg1 = "";
                double temp1 = 0;
                string strDiscType1 = "";
                //*********
                disc_amt1 = 0;
                msg1 = "";
                temp1 = 0;
                //if(Request.Form["txtentry"].ToString()=="")
                //{
                //	strDiscType1="";
                //	msg1 = "";
                //}
                //else
                {
                    //disc_amt1 = System.Convert.ToDouble(Request.Form["txtentry"].ToString()); 

                    //strDiscType1 = dropentry.SelectedItem.Text;
                    if (strDiscType1.Trim().Equals("%"))
                    {
                        if (Request.Form["txtGrandTotal"].ToString().Trim() != "")
                            temp1 = System.Convert.ToDouble(Request.Form["txtGrandTotal"].ToString().Trim().ToString());

                        disc_amt1 = (temp1 * disc_amt1 / 100);
                        //msg1 = "("+ Request.Form["txtentry"].ToString()+strDiscType1+")";
                    }
                    else
                    {
                        msg1 = "(" + strDiscType1 + ")";
                    }
                    ET = disc_amt1;
                }
                //sw.WriteLine("                               Entry Tax {0,-8:S} : {1,13:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));
                sw.WriteLine(info1, "", "Entry Tax" + msg1, GenUtil.strNumericFormat(disc_amt1.ToString()));
                //******************** End of entry Tax ***********************
                //**********Calculation of FOC Discount **************
                //*********bhal Add*************
                disc_amt1 = 0;
                msg1 = "";
                strDiscType1 = "";
                temp1 = 0;
                disc_amt1 = 0;
                msg1 = "";
                if (Request.Form["txtfoc"].ToString() == "")
                {
                    strDiscType1 = "";
                    msg1 = "";
                }
                else
                {
                    disc_amt1 = System.Convert.ToDouble(Request.Form["txtfoc"].ToString());

                    strDiscType1 = dropfoc.SelectedItem.Text;
                    if (strDiscType1.Trim().Equals("%"))
                    {
                        if (Request.Form["txtGrandTotal"].ToString().Trim() != "")
                            temp1 = System.Convert.ToDouble(Request.Form["txtGrandTotal"].Trim().ToString());

                        disc_amt1 = (temp1 * disc_amt1 / 100);
                        msg1 = "(" + Request.Form["txtfoc"].ToString() + strDiscType1 + ")";
                    }
                    else
                    {
                        msg1 = "(" + strDiscType1 + ")";
                    }
                    FOC = disc_amt1;
                }
                //sw.WriteLine("                               FOC Discount{0,-8:S} : {1,11:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));
                sw.WriteLine(info1, "", "FOC Discount" + msg1, "-" + GenUtil.strNumericFormat(disc_amt1.ToString()));
                //********************* End of FOC Discount ************************
                //****************** Calculation of Fixed Discount ****************
                disc_amt1 = 0;
                msg1 = "";
                if (Request.Form["txtfixed"].ToString() == "")
                {
                    strDiscType1 = "";
                    msg1 = "";
                }
                else
                {
                    disc_amt1 = System.Convert.ToDouble(Request.Form["txtfixed"].ToString());
                    //strDiscType1= dropfixed.SelectedItem.Text;
                    strDiscType1 = Request.Form["txtfixedamt"].ToString();

                    if (strDiscType1.Trim().Equals("%"))
                    {
                        if (Request.Form["txtGrandTotal"].ToString().Trim() != "")
                            temp1 = System.Convert.ToDouble(Request.Form["txtGrandTotal"].ToString().Trim().ToString());

                        disc_amt1 = (temp1 * disc_amt1 / 100);
                        msg1 = "(" + Request.Form["txtfixed"].ToString() + strDiscType1 + ")";
                    }
                    else
                    {
                        msg1 = strDiscType1;
                    }
                    FD = disc_amt1;
                }
                //sw.WriteLine("                               Fixed Discount{0,-8:S} : {1,11:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));
                sw.WriteLine(info1, "", "Fixed Discount" + "(" + GenUtil.strNumericFormat(disc_amt1.ToString()) + ")", "-" + msg1);
                //********************* End of Fixed Discount ********************
                //********************* Calculation of Trade Discount ************
                disc_amt1 = 0;
                msg1 = "";
                sdisc = 0;
                if (Request.Form["txttradeless"].ToString().Equals(""))
                    txttradeless.Text = "0";
                if (Request.Form["txttradedisamt"].ToString() == "")
                {
                    sdisc = 0;
                    disc_amt1 = 0;
                    msg1 = "";
                }
                else
                {
                    //disc_amt1 = System.Convert.ToDouble(txttradedisamt.Text.ToString())-System.Convert.ToDouble(txttradeless.Text.ToString()); 
                    disc_amt1 = System.Convert.ToDouble(Request.Form["txttradedisamt"].ToString());
                    //sdisc= System.Convert.ToDouble(txttradedis.Text.ToString()); 
                }
                TD = disc_amt1;
                //sw.WriteLine("                              Trade Discount{0,-8:S} : {1,10:F}" ,"("+GenUtil.strNumericFormat(sdisc.ToString())+")",GenUtil.strNumericFormat(disc_amt1.ToString()));
                sw.WriteLine(info1, "", "Trade Discount" + "(" + GenUtil.strNumericFormat(sdisc.ToString()) + ")", "-" + GenUtil.strNumericFormat(disc_amt1.ToString()));
                //********************* End of Trade Discount ***********************
                //********************* Calculation of Discount *********************
                disc_amt = 0;
                msg = "";
                if (Request.Form["txtDisc"].ToString() == "")
                {
                    strDiscType = "";
                    msg = "";
                }
                else
                {
                    disc_amt = System.Convert.ToDouble(Request.Form["txtDisc"].ToString());

                    strDiscType = DropDiscType.SelectedItem.Text;
                    if (strDiscType.Trim().Equals("%"))
                    {
                        double temp = 0;
                        if (Request.Form["txtGrandTotal"].ToString().Trim() != "")
                            temp = System.Convert.ToDouble(Request.Form["txtGrandTotal"].Trim().ToString());

                        disc_amt = (temp * disc_amt / 100);
                        msg = "(" + Request.Form["txtDisc"].ToString() + strDiscType + ")";
                    }
                    else
                    {
                        msg = "(" + strDiscType + ")";
                    }
                }
                //sw.WriteLine("                               Discount     {0,-8:S} : {1,10:F}" ,msg,GenUtil.strNumericFormat(disc_amt.ToString()));
                sw.WriteLine(info1, "", "Discount" + msg, "-" + GenUtil.strNumericFormat(disc_amt.ToString()));
                //******************* End of Discount *********************
                //******************* Calculation of Cash Discount ********
                //*********bhal End************
                disc_amt = 0;
                msg = "";
                if (Request.Form["txtCashDisc"].ToString() == "")
                {
                    strDiscType = "";
                    msg = "";
                }
                else
                {
                    disc_amt = System.Convert.ToDouble(Request.Form["txtCashDisc"].ToString());

                    strDiscType = DropCashDiscType.SelectedItem.Text;
                    if (strDiscType.Trim().Equals("%"))
                    {
                        double temp = 0;
                        if (Request.Form["txtGrandTotal"].ToString().Trim() != "")
                            //temp = System.Convert.ToDouble(txtGrandTotal.Text.Trim().ToString());
                            temp = System.Convert.ToDouble(Request.Form["txtGrandTotal"].ToString().Trim().ToString()) + ET - (TD + FOC + FD);

                        disc_amt = (temp * disc_amt / 100);
                        msg = "(" + Request.Form["txtCashDisc"].ToString() + strDiscType + ")";
                    }
                    else
                    {
                        msg = "(" + strDiscType + ")";
                    }
                }
                sw.WriteLine(info1, "", "Cash Discount" + msg, "-" + GenUtil.strNumericFormat(disc_amt.ToString()));
                //******************* End of Cash Discount ***********************
                //******************* Calculation of Ebird Discount **************
                disc_amt1 = 0;
                msg1 = "";
                sdisc = 0;
                if (Request.Form["txtbirdless"].ToString().Equals(""))
                    txtbirdless.Text = "0";
                if (Request.Form["txtebirdamt"].ToString() == "")
                {
                    sdisc = 0;
                    disc_amt1 = 0;
                    //msg1 = "";
                }
                else
                {
                    //disc_amt1 = System.Convert.ToDouble(txtebirdamt.Text.ToString())-System.Convert.ToDouble(txtbirdless.Text.ToString()); 
                    disc_amt1 = System.Convert.ToDouble(Request.Form["txtebirdamt"].ToString());

                    sdisc = System.Convert.ToDouble(Request.Form["txtebird"].ToString());

                }
                //sw.WriteLine("                              Ebird Discount{0,-8:S} : {1,10:F}" ,"("+GenUtil.strNumericFormat(sdisc.ToString())+")",GenUtil.strNumericFormat(disc_amt1.ToString()));
                sw.WriteLine(info1, "", "Ebird Discount" + "(" + GenUtil.strNumericFormat(sdisc.ToString()) + ")", "-" + GenUtil.strNumericFormat(disc_amt1.ToString()));
                //******************** End of Ebird Discount *******************

                string Vat_Rate = "";
                string amount = "0";
                if (Yes.Checked)
                {
                    Vat_Rate = "(" + Session["VAT_Rate"].ToString() + "%)";
                    amount = Request.Form["txtVAT"].ToString().Trim();

                }
                //sw.WriteLine("                               VAT          {0,-8:S} : {1,10:F}" ,Vat_Rate,GenUtil.strNumericFormat(amount));
                sw.WriteLine(info1, "", "VAT" + Vat_Rate, GenUtil.strNumericFormat(amount));

                //sw.WriteLine("                               Net Amount            : {0,10:F}" , GenUtil.strNumericFormat(txtNetAmount.Text.ToString()));
                sw.WriteLine(info1, "", "Net Amount", GenUtil.strNumericFormat(Request.Form["txtNetAmount"].ToString()));

                sw.WriteLine("+----------------------------------------+-----------+----------+----------+");
                sw.WriteLine("Promo Scheme : " + Request.Form["txtPromoScheme"].ToString());

                sw.WriteLine("Remarks      : " + Request.Form["txtRemark"].ToString());

                sw.WriteLine("Message      : " + Request.Form["txtMessage"].ToString());
                //				sw.WriteLine("");
                //				sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("                                                Signature");
                /*
				HtmlInputHidden[] batch={bat0,bat1,bat2,bat3,bat4,bat5,bat6,bat7,bat8,bat9,bat10,bat11};
				HtmlInputHidden[]  ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12};
				TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12};
				sw.WriteLine("List Of Batch No For Invoice No:"+lblInvoiceNo.Text+", Date:"+lblInvoiceDate.Text+", Location:"+DropVendorID.SelectedItem.Text+", And Receipt Date:"+txtVInvoiceDate.Text);
				//sw.WriteLine("+------------------+-------+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+");
			   // sw.WriteLine("|   Product Name   |Tot_qty|  Batch1  |qty|  Batch2  |qty|  Batch3  |qty|  Batch4  |qty|  Batch5  |qty|  Batch6  |qty|  Batch7  |qty|  Batch8  |qty|  Batch9  |qty|  Batch10 |qty|");
				//sw.WriteLine("+------------------+-------+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+----------+---+");
				//string info2="|{0,-18:S}|{1,7:S}|{2,-10:S}|{3,3:S}|{10,-10:S}|{5,3:S}|{6,-10:S}|{7,3:S}|{8,-8:S}|{9,4:S}|{10,-8:S}|{11,4:S}|{12,-8:S}|{13,4:S}|{14,-8:S}|{15,4:S}|{16,-8:S}|{17,4:S}|{18,-8:S}|{19,4:S}|{20,-9:S}|{21,5:S}|";
				string[] arr=new string[20];
				int NewCount=0,OldCount=0;
				for(int i=0,j=0;i<12;i++,j++)
				{
					string b=batch[i].Value;
					b=b.Replace("'","");
					OldCount=0;
					arr=b.Split(new char[] {','},b.Length);
					//arr1=batch[++i].Value.Split(new char[] {','},batch[i].Value.Length);
					for(int k=0;k<arr.Length;k++)
					{
						if(arr[k].ToString()!="")
							OldCount++;
						else
							break;
					}
					if(NewCount<OldCount)
						NewCount=OldCount;
				}
				string header="";
				string desdes="+---------+-------+";
				header="|ProductCD|Tot_Qty|";
				//sw.WriteLine();
				for(int j=1;j<=NewCount/2;j++)
				{
					desdes+="----------+---+";
					header+="  Batch"+j+"  |Qty|";
				}
				sw.WriteLine(desdes);
				sw.WriteLine(header);
				sw.WriteLine(desdes);
				for(int i=0;i<12;i++)
				{
					string bat=batch[i].Value;
					bat=bat.Replace("'","");
					arr=bat.Split(new char[] {','},bat.Length);
					int n=0,p=0,s=arr.Length;
					string sss=arr[n].ToString();
					if(arr[n].ToString()=="")
						break;
					//{
					//,arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString(),arr[n++].ToString());
					SqlDataReader rdr=null;
					dbobj.SelectQuery("select Prod_Code from products where Prod_Name='"+ProdName[i].Value+"'",ref rdr);
					string pd="0";
					if(rdr.Read())
					{
						pd=rdr.GetValue(0).ToString();
					}
					sw.Write("|"+pd);
					for(int a=pd.Length;a<9;a++)
					{
						sw.Write(" ");
					}
					sw.Write("|"+Qty[i].Text);
					for(int a=Qty[i].Text.Length;a<7;a++)
					{
						sw.Write(" ");
					}
					sw.Write("|");
					int d=0;
					for(int b=0;b<arr.Length;b++)
					{
						if(arr[b].ToString()!="")
						{
							d++;
							sw.Write(arr[b].ToString());
							for(int c=arr[b].Length;c<10;c++)
							{
								sw.Write(" ");
							}
							sw.Write("|");
							sw.Write(arr[++b].ToString());
							for(int c=arr[b].Length;c<3;c++)
							{
								sw.Write(" ");
							}
							sw.Write("|");
						}
						else
						{
							if(d>=NewCount/2)
							{
								sw.WriteLine();
								break;
							}
							else
							{
								sw.Write("          |   |");
								d++;
							}
						}
						//sw.WriteLine();
					}
				
				}
				sw.WriteLine(desdes);
				sw.WriteLine();
				sw.WriteLine("                                                                  Signature");
				*/
                sw.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:reportmaking().  EXCEPTION   " + ex.Message + ". User_id " + uid);
            }
        }

        /// <summary>
        /// this is used to clears the forms.
        /// </summary>
        public void Clear()
        {
            txtAddDis.Text = "";
            tempVndrInvoiceNo.Value = "";
            txtTotalDisc.Text = "";
            Vendor_ID = "0";
            //CheckMode="";
            tempGrandTotal.Value = "";
            txtTotalCashDisc.Text = "";
            DropModeType.SelectedIndex = 0;
            DropVendorID.SelectedIndex = 0;
            lblPlace.Value = "";
            txtVehicleNo.Text = "";
            txtVInnvoiceNo.Text = "";
            //txtVInnvoiceNo.Enabled=true;
            txtVInvoiceDate.Text = "";
            txtPromoScheme.Text = "";
            txtRemark.Text = "";
            txtGrandTotal.Text = "";
            txtDisc.Text = "";
            txtNetAmount.Text = "";
            DropDiscType.SelectedIndex = 0;
            DropCashDiscType.SelectedIndex = 0;
            txtCashDisc.Text = "";
            txtVAT.Text = "";
            Yes.Checked = true;
            No.Checked = false;
            txtfoc.Text = "";
            tempDelinfo.Value = "";
            //txtfixed.Text="0.30";
            txtfixed.Text = "";
            txtCashDisc.Text = "1.5";
            //dropfixed.SelectedIndex = 0;
            txtfixedamt.Text = "";
            //txtentry.Text="";
            txtebirdamt.Text = "";
            //dropentry.SelectedIndex=0;
            dropfoc.SelectedIndex = 0;
            txttradedisamt.Text = "";
            txtebird.Text = "";
            //txttradedis.Text = "";
            txtebird.Text = "0.40";
            //txttradedis.Text="4.10";
            txttotalqty.Text = "";
            txttotalqtyltr.Text = "";
            txttotalqtyltr1.Text = "";
            txtbirdless.Text = "";
            txttradeless.Text = "";
            Textcgst.Text = "";
            Textsgst.Text = "";
            #region Clear All Products Details
            /*DropType1.SelectedIndex=0;
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
			DropType13.SelectedIndex=0;
			DropType14.SelectedIndex=0;
			DropType15.SelectedIndex=0;
			DropType16.SelectedIndex=0;
			DropType17.SelectedIndex=0;
			DropType18.SelectedIndex=0;
			DropType19.SelectedIndex=0;
			DropType20.SelectedIndex=0;*/
            DropType1.Value = "Type";
            DropType2.Value = "Type";
            DropType3.Value = "Type";
            DropType4.Value = "Type";
            DropType5.Value = "Type";
            DropType6.Value = "Type";
            DropType7.Value = "Type";
            DropType8.Value = "Type";
            DropType9.Value = "Type";
            DropType10.Value = "Type";
            DropType11.Value = "Type";
            DropType12.Value = "Type";
            DropType13.Value = "Type";
            DropType14.Value = "Type";
            DropType15.Value = "Type";
            DropType16.Value = "Type";
            DropType17.Value = "Type";
            DropType18.Value = "Type";
            DropType19.Value = "Type";
            DropType20.Value = "Type";
            //DropProd1.SelectedIndex=0;

            /*
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
			DropProd13.SelectedIndex=0;
			DropProd14.SelectedIndex=0;
			DropProd15.SelectedIndex=0;
			DropProd16.SelectedIndex=0;
			DropProd17.SelectedIndex=0;
			DropProd18.SelectedIndex=0;
			DropProd19.SelectedIndex=0;
			DropProd20.SelectedIndex=0;
			//DropPack1.Items.Clear();
			//DropPack1.Items.Add("Select");
			//DropPack1.SelectedIndex=(DropPack1.Items.IndexOf((DropPack1.Items.FindByValue ("Select"))));

			DropPack2.Items.Clear();
			DropPack2.Items.Add("Select");
			DropPack2.SelectedIndex=(DropPack2.Items.IndexOf((DropPack2.Items.FindByValue ("Select"))));
			
			DropPack3.Items.Clear();
			DropPack3.Items.Add("Select");
			DropPack3.SelectedIndex=(DropPack3.Items.IndexOf((DropPack3.Items.FindByValue ("Select"))));
			
			DropPack4.Items.Clear();
			DropPack4.Items.Add("Select");
			DropPack4.SelectedIndex=(DropPack4.Items.IndexOf((DropPack4.Items.FindByValue ("Select"))));
			
			DropPack5.Items.Clear();
			DropPack5.Items.Add("Select");
			DropPack5.SelectedIndex=(DropPack5.Items.IndexOf((DropPack5.Items.FindByValue ("Select"))));
			
			DropPack6.Items.Clear();
			DropPack6.Items.Add("Select");
			DropPack6.SelectedIndex=(DropPack6.Items.IndexOf((DropPack6.Items.FindByValue ("Select"))));
			
			DropPack7.Items.Clear();
			DropPack7.Items.Add("Select");
			DropPack7.SelectedIndex=(DropPack7.Items.IndexOf((DropPack7.Items.FindByValue ("Select"))));
			
			DropPack8.Items.Clear();
			DropPack8.Items.Add("Select");
			DropPack8.SelectedIndex=(DropPack8.Items.IndexOf((DropPack8.Items.FindByValue ("Select"))));
			
			DropPack9.Items.Clear();
			DropPack9.Items.Add("Select");
			DropPack9.SelectedIndex=(DropPack9.Items.IndexOf((DropPack9.Items.FindByValue ("Select"))));
			
			DropPack10.Items.Clear();
			DropPack10.Items.Add("Select");
			DropPack10.SelectedIndex=(DropPack10.Items.IndexOf((DropPack10.Items.FindByValue ("Select"))));
			
			DropPack11.Items.Clear();
			DropPack11.Items.Add("Select");
			DropPack11.SelectedIndex=(DropPack11.Items.IndexOf((DropPack11.Items.FindByValue ("Select"))));
			
			DropPack12.Items.Clear();
			DropPack12.Items.Add("Select");
			DropPack12.SelectedIndex=(DropPack12.Items.IndexOf((DropPack12.Items.FindByValue ("Select"))));
			
			DropPack13.Items.Clear();
			DropPack13.Items.Add("Select");
			DropPack13.SelectedIndex=(DropPack13.Items.IndexOf((DropPack13.Items.FindByValue ("Select"))));

			DropPack14.Items.Clear();
			DropPack14.Items.Add("Select");
			DropPack14.SelectedIndex=(DropPack14.Items.IndexOf((DropPack14.Items.FindByValue ("Select"))));

			DropPack15.Items.Clear();
			DropPack15.Items.Add("Select");
			DropPack15.SelectedIndex=(DropPack15.Items.IndexOf((DropPack15.Items.FindByValue ("Select"))));

			DropPack16.Items.Clear();
			DropPack16.Items.Add("Select");
			DropPack16.SelectedIndex=(DropPack16.Items.IndexOf((DropPack16.Items.FindByValue ("Select"))));

			DropPack17.Items.Clear();
			DropPack17.Items.Add("Select");
			DropPack17.SelectedIndex=(DropPack17.Items.IndexOf((DropPack17.Items.FindByValue ("Select"))));

			DropPack18.Items.Clear();
			DropPack18.Items.Add("Select");
			DropPack18.SelectedIndex=(DropPack18.Items.IndexOf((DropPack18.Items.FindByValue ("Select"))));

			DropPack19.Items.Clear();
			DropPack19.Items.Add("Select");
			DropPack19.SelectedIndex=(DropPack19.Items.IndexOf((DropPack19.Items.FindByValue ("Select"))));

			DropPack20.Items.Clear();
			DropPack20.Items.Add("Select");
			DropPack20.SelectedIndex=(DropPack20.Items.IndexOf((DropPack20.Items.FindByValue ("Select"))));
					
			//DropPack1.SelectedIndex=0;
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
			DropPack12.SelectedIndex=0;
			DropPack13.SelectedIndex=0;
			DropPack14.SelectedIndex=0;
			DropPack15.SelectedIndex=0;
			DropPack16.SelectedIndex=0;
			DropPack17.SelectedIndex=0;
			DropPack18.SelectedIndex=0;
			DropPack19.SelectedIndex=0;
			DropPack20.SelectedIndex=0;
			*/
            txtQty1.Text = "";
            txtQty2.Text = "";
            txtQty3.Text = "";
            txtQty4.Text = "";
            txtQty5.Text = "";
            txtQty6.Text = "";
            txtQty7.Text = "";
            txtQty8.Text = "";
            txtQty9.Text = "";
            txtQty10.Text = "";
            txtQty11.Text = "";
            txtQty12.Text = "";
            txtQty13.Text = "";
            txtQty14.Text = "";
            txtQty15.Text = "";
            txtQty16.Text = "";
            txtQty17.Text = "";
            txtQty18.Text = "";
            txtQty19.Text = "";
            txtQty20.Text = "";
            txtTempQty1.Text = "";
            txtTempQty2.Text = "";
            txtTempQty3.Text = "";
            txtTempQty4.Text = "";
            txtTempQty5.Text = "";
            txtTempQty6.Text = "";
            txtTempQty7.Text = "";
            txtTempQty8.Text = "";
            txtTempQty9.Text = "";
            txtTempQty10.Text = "";
            txtTempQty11.Text = "";
            txtTempQty12.Text = "";
            txtTempQty13.Text = "";
            txtTempQty14.Text = "";
            txtTempQty15.Text = "";
            txtTempQty16.Text = "";
            txtTempQty17.Text = "";
            txtTempQty18.Text = "";
            txtTempQty19.Text = "";
            txtTempQty20.Text = "";

            txtRate1.Text = "";
            txtRate2.Text = "";
            txtRate3.Text = "";
            txtRate4.Text = "";
            txtRate5.Text = "";
            txtRate6.Text = "";
            txtRate7.Text = "";
            txtRate8.Text = "";
            txtRate9.Text = "";
            txtRate10.Text = "";
            txtRate11.Text = "";
            txtRate12.Text = "";
            txtRate13.Text = "";
            txtRate14.Text = "";
            txtRate15.Text = "";
            txtRate16.Text = "";
            txtRate17.Text = "";
            txtRate18.Text = "";
            txtRate19.Text = "";
            txtRate20.Text = "";
            txtAmount1.Text = "";
            txtAmount1.Text = "";
            txtAmount2.Text = "";
            txtAmount3.Text = "";
            txtAmount4.Text = "";
            txtAmount5.Text = "";
            txtAmount6.Text = "";
            txtAmount7.Text = "";
            txtAmount8.Text = "";
            txtAmount9.Text = "";
            txtAmount10.Text = "";
            txtAmount11.Text = "";
            txtAmount12.Text = "";
            txtAmount13.Text = "";
            txtAmount14.Text = "";
            txtAmount15.Text = "";
            txtAmount16.Text = "";
            txtAmount17.Text = "";
            txtAmount18.Text = "";
            txtAmount19.Text = "";
            txtAmount20.Text = "";
            //**
            chkfoc1.Checked = false;
            chkfoc2.Checked = false;
            chkfoc3.Checked = false;
            chkfoc4.Checked = false;
            chkfoc5.Checked = false;
            chkfoc6.Checked = false;
            chkfoc7.Checked = false;
            chkfoc8.Checked = false;
            chkfoc9.Checked = false;
            chkfoc10.Checked = false;
            chkfoc11.Checked = false;
            chkfoc12.Checked = false;
            chkfoc13.Checked = false;
            chkfoc14.Checked = false;
            chkfoc15.Checked = false;
            chkfoc16.Checked = false;
            chkfoc17.Checked = false;
            chkfoc18.Checked = false;
            chkfoc19.Checked = false;
            chkfoc20.Checked = false;

            chkSchDisc1.Checked = false;
            chkSchDisc2.Checked = false;
            chkSchDisc3.Checked = false;
            chkSchDisc4.Checked = false;
            chkSchDisc5.Checked = false;
            chkSchDisc6.Checked = false;
            chkSchDisc7.Checked = false;
            chkSchDisc8.Checked = false;
            chkSchDisc9.Checked = false;
            chkSchDisc10.Checked = false;
            chkSchDisc11.Checked = false;
            chkSchDisc12.Checked = false;
            chkSchDisc13.Checked = false;
            chkSchDisc14.Checked = false;
            chkSchDisc15.Checked = false;
            chkSchDisc16.Checked = false;
            chkSchDisc17.Checked = false;
            chkSchDisc18.Checked = false;
            chkSchDisc19.Checked = false;
            chkSchDisc20.Checked = false;

            //***
            #endregion

            #region Clear Hidden TextBoxex
            //txtProdName1.Value=""; 
            /*
			txtProdName2.Value=""; 
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
			txtProdName13.Value=""; 
			txtProdName14.Value=""; 
			txtProdName15.Value=""; 
			txtProdName16.Value=""; 
			txtProdName17.Value=""; 
			txtProdName18.Value=""; 
			txtProdName19.Value="";
			txtProdName20.Value=""; 

			//txtPack1.Value="";
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
			txtPack12.Value="";
			txtPack13.Value="";
			txtPack14.Value="";
			txtPack15.Value="";
			txtPack16.Value="";
			txtPack17.Value="";
			txtPack18.Value="";
			txtPack19.Value="";
			txtPack20.Value="";
			*/
            //***********
            bat0.Value = "";
            bat1.Value = "";
            bat2.Value = "";
            bat3.Value = "";
            bat4.Value = "";
            bat5.Value = "";
            bat6.Value = "";
            bat7.Value = "";
            bat8.Value = "";
            bat9.Value = "";
            bat10.Value = "";
            bat11.Value = "";
            bat12.Value = "";
            bat13.Value = "";
            bat14.Value = "";
            bat15.Value = "";
            bat16.Value = "";
            bat17.Value = "";
            bat18.Value = "";
            bat19.Value = "";

            tempSchDis1.Value = "";
            tempSchDis2.Value = "";
            tempSchDis3.Value = "";
            tempSchDis4.Value = "";
            tempSchDis5.Value = "";
            tempSchDis6.Value = "";
            tempSchDis7.Value = "";
            tempSchDis8.Value = "";
            tempSchDis9.Value = "";
            tempSchDis10.Value = "";
            tempSchDis11.Value = "";
            tempSchDis12.Value = "";
            tempSchDis13.Value = "";
            tempSchDis14.Value = "";
            tempSchDis15.Value = "";
            tempSchDis16.Value = "";
            tempSchDis17.Value = "";
            tempSchDis18.Value = "";
            tempSchDis19.Value = "";
            tempSchDis20.Value = "";
            tempStktSchDis1.Value = "";
            tempStktSchDis2.Value = "";
            tempStktSchDis3.Value = "";
            tempStktSchDis4.Value = "";
            tempStktSchDis5.Value = "";
            tempStktSchDis6.Value = "";
            tempStktSchDis7.Value = "";
            tempStktSchDis8.Value = "";
            tempStktSchDis9.Value = "";
            tempStktSchDis10.Value = "";
            tempStktSchDis11.Value = "";
            tempStktSchDis12.Value = "";
            tempStktSchDis13.Value = "";
            tempStktSchDis14.Value = "";
            tempStktSchDis15.Value = "";
            tempStktSchDis16.Value = "";
            tempStktSchDis17.Value = "";
            tempStktSchDis18.Value = "";
            tempStktSchDis19.Value = "";
            tempStktSchDis20.Value = "";
            tempStktSchDisType1.Value = "";
            tempStktSchDisType2.Value = "";
            tempStktSchDisType3.Value = "";
            tempStktSchDisType4.Value = "";
            tempStktSchDisType5.Value = "";
            tempStktSchDisType6.Value = "";
            tempStktSchDisType7.Value = "";
            tempStktSchDisType8.Value = "";
            tempStktSchDisType9.Value = "";
            tempStktSchDisType10.Value = "";
            tempStktSchDisType11.Value = "";
            tempStktSchDisType12.Value = "";
            tempStktSchDisType13.Value = "";
            tempStktSchDisType14.Value = "";
            tempStktSchDisType15.Value = "";
            tempStktSchDisType16.Value = "";
            tempStktSchDisType17.Value = "";
            tempStktSchDisType18.Value = "";
            tempStktSchDisType19.Value = "";
            tempStktSchDisType20.Value = "";
            tempSchDiscount1.Value = "";
            tempSchDiscount2.Value = "";
            tempSchDiscount3.Value = "";
            tempSchDiscount4.Value = "";
            tempSchDiscount5.Value = "";
            tempSchDiscount6.Value = "";
            tempSchDiscount7.Value = "";
            tempSchDiscount8.Value = "";
            tempSchDiscount9.Value = "";
            tempSchDiscount10.Value = "";
            tempSchDiscount11.Value = "";
            tempSchDiscount12.Value = "";
            tempSchDiscount13.Value = "";
            tempSchDiscount14.Value = "";
            tempSchDiscount15.Value = "";
            tempSchDiscount16.Value = "";
            tempSchDiscount17.Value = "";
            tempSchDiscount18.Value = "";
            tempSchDiscount19.Value = "";
            tempSchDiscount20.Value = "";
            //***********
            #endregion
            for (int i = 0; i < ProductType.Length; i++)
            {
                ProductType[i] = "";
                ProductName[i] = "";
                ProductPack[i] = "";
                ProductQty[i] = "";
            }
        }

        /// <summary>
        /// this is used to get nextID auto.
        /// </summary>
        public void GetNextInvoiceNo()
        {
            string invoiceNo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/GetNextInvoiceNo").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    invoiceNo = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            lblInvoiceNo.Text = invoiceNo;
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// this method is used to show a message ,incase of price is not fill.
        /// </summary>
        public void GetProducts()
        {
            try
            {
                List<string> products = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/purchase/GetProducts").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        products = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if(products!=null)
                {
                    lblMessage.Text = products[0];
                    temptext.Value = products[1];
                }
                
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:GetProducts().  EXCEPTION   " + ex.Message + ". User_id " + uid);
            }
        }


        private void DropType1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// This method is used to fill the all purchase invoice no in dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnEdit_Click(object sender, System.EventArgs e)
        {
            try
            {
                BtnEdit.Visible = false;
                DropInvoiceNo.Visible = true;
                lblInvoiceNo.Visible = false;
                btnSave.Enabled = true;

                List<string> invoice = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/purchase/FetchAllInvoice").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        invoice = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if(invoice!=null)
                {
                    foreach (var id in invoice)
                        DropInvoiceNo.Items.Add(id);
                }
                
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:BtnEdit_Click().  EXCEPTION   " + ex.Message + ". User_id " + uid);
            }
        }

        /// <summary>
        /// This method is used to fatch the record according to select purchase invoice no from dropdownlist.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropInvoiceNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            try
            {
                Txtselect.Text = DropInvoiceNo.SelectedItem.Value.ToString();
                if (Txtselect.Text == "Select")
                {
                    MessageBox.Show("Please Select Invoice No");
                }
                else
                {
                    Clear();
                    HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                    TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                    TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
                    TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
                    TextBox[] Quantity = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12, txtTempQty13, txtTempQty14, txtTempQty15, txtTempQty16, txtTempQty17, txtTempQty18, txtTempQty19, txtTempQty20 };
                    HtmlInputHidden[] tempStktSchDis = { tempStktSchDis1, tempStktSchDis2, tempStktSchDis3, tempStktSchDis4, tempStktSchDis5, tempStktSchDis6, tempStktSchDis7, tempStktSchDis8, tempStktSchDis9, tempStktSchDis10, tempStktSchDis11, tempStktSchDis12, tempStktSchDis13, tempStktSchDis14, tempStktSchDis15, tempStktSchDis16, tempStktSchDis17, tempStktSchDis18, tempStktSchDis19, tempStktSchDis20 };
                    HtmlInputHidden[] tempSchDis = { tempSchDis1, tempSchDis2, tempSchDis3, tempSchDis4, tempSchDis5, tempSchDis6, tempSchDis7, tempSchDis8, tempSchDis9, tempSchDis10, tempSchDis11, tempSchDis12, tempSchDis13, tempSchDis14, tempSchDis15, tempSchDis16, tempSchDis17, tempSchDis18, tempSchDis19, tempSchDis20 };
                    HtmlInputHidden[] tempDiscount = { tempSchDiscount1, tempSchDiscount2, tempSchDiscount3, tempSchDiscount4, tempSchDiscount5, tempSchDiscount6, tempSchDiscount7, tempSchDiscount8, tempSchDiscount9, tempSchDiscount10, tempSchDiscount11, tempSchDiscount12, tempSchDiscount13, tempSchDiscount14, tempSchDiscount15, tempSchDiscount16, tempSchDiscount17, tempSchDiscount18, tempSchDiscount19, tempSchDiscount20 };
                    HtmlInputCheckBox[] chkDisc = { chkSchDisc1, chkSchDisc2, chkSchDisc3, chkSchDisc4, chkSchDisc5, chkSchDisc6, chkSchDisc7, chkSchDisc8, chkSchDisc9, chkSchDisc10, chkSchDisc11, chkSchDisc12, chkSchDisc13, chkSchDisc14, chkSchDisc15, chkSchDisc16, chkSchDisc17, chkSchDisc18, chkSchDisc19, chkSchDisc20 };
                    //***
                    HtmlInputHidden[] tempSchAddDis = { tempSchAddDis1, tempSchAddDis2, tempSchAddDis3, tempSchAddDis4, tempSchAddDis5, tempSchAddDis6, tempSchAddDis7, tempSchAddDis8, tempSchAddDis9, tempSchAddDis10, tempSchAddDis11, tempSchAddDis12, tempSchAddDis13, tempSchAddDis14, tempSchAddDis15, tempSchAddDis16, tempSchAddDis17, tempSchAddDis18, tempSchAddDis19, tempSchAddDis20 };             //Add by vikas 03.07.09

                    CheckBox[] chkfoc = { chkfoc1, chkfoc2, chkfoc3, chkfoc4, chkfoc5, chkfoc6, chkfoc7, chkfoc8, chkfoc9, chkfoc10, chkfoc11, chkfoc12, chkfoc13, chkfoc14, chkfoc15, chkfoc16, chkfoc17, chkfoc18, chkfoc19, chkfoc20 };

                    HtmlInputHidden[] tempFixdDisc = { tempFixedDisc1, tempFixedDisc2, tempFixedDisc3, tempFixedDisc4, tempFixedDisc5, tempFixedDisc6, tempFixedDisc7, tempFixedDisc8, tempFixedDisc9, tempFixedDisc10, tempFixedDisc11, tempFixedDisc12, tempFixedDisc13, tempFixedDisc14, tempFixedDisc15, tempFixedDisc16, tempFixedDisc17, tempFixedDisc18, tempFixedDisc19, tempFixedDisc20 };             //Add by vikas 16.05.13

                    HtmlInputHidden[] tmpCgst = { tempCgst1, tempCgst2, tempCgst3, tempCgst4, tempCgst5, tempCgst6, tempCgst7, tempCgst8, tempCgst9, tempCgst10, tempCgst11, tempCgst12, tempCgst13, tempCgst14, tempCgst15, tempCgst16, tempCgst17, tempCgst18, tempCgst19, tempCgst20 };
                    HtmlInputHidden[] tmpSgst = { tempSgst1, tempSgst2, tempSgst3, tempSgst4, tempSgst5, tempSgst6, tempSgst7, tempSgst8, tempSgst9, tempSgst10, tempSgst11, tempSgst12, tempSgst13, tempSgst14, tempSgst15, tempSgst16, tempSgst17, tempSgst18, tempSgst19, tempSgst20 };
                    HtmlInputHidden[] tmpIgst = { tempIgst1, tempIgst2, tempIgst3, tempIgst4, tempIgst5, tempIgst6, tempIgst7, tempIgst8, tempIgst9, tempIgst10, tempIgst11, tempIgst12, tempIgst13, tempIgst14, tempIgst15, tempIgst16, tempIgst17, tempIgst18, tempIgst19, tempIgst20 };
                    HtmlInputHidden[] tmpHsn = { tempHsn1, tempHsn2, tempHsn3, tempHsn4, tempHsn5, tempHsn6, tempHsn7, tempHsn8, tempHsn9, tempHsn10, tempHsn11, tempHsn12, tempHsn13, tempHsn14, tempHsn15, tempHsn16, tempHsn17, tempHsn18, tempHsn19, tempHsn20 };
                    PurchaseModel purchase = new PurchaseModel();
                    string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/purchase/GetPurchaseInvoice?invoiceNo="+ arrInvoiceNo[0]).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            purchase = JsonConvert.DeserializeObject<PurchaseModel>(disc);
                        }
                    }
                   
                    string strDate;//, strDate1;
                    int i = 0;
                   
                    if (purchase!=null)
                    {
                        Invoice_Date = purchase.InvoiceDate.ToString();
                        strDate = purchase.InvoiceDate.ToString().Trim();
                    
                        int pos = strDate.IndexOf(" ");
                        if (pos != -1)
                        {
                            strDate = strDate.Substring(0, pos);
                        }
                        else
                        {
                            strDate = "";
                        }
                       
                        lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(strDate);

                        DropModeType.SelectedIndex = (DropModeType.Items.IndexOf((DropModeType.Items.FindByValue(purchase.ModeofPayment))));
                        txtVehicleNo.Text = purchase.VehicleNo;
                        txtVInnvoiceNo.Text = purchase.VendorInvoiceNo;
                        tempVndrInvoiceNo.Value = purchase.VendorInvoiceNo;
                        //Coment By vikas 7.3.2013 Fore Early Birld Tax txtVInvoiceDate.Text=GenUtil.str2DDMMYYYY(strDate1);
                        txtVInvoiceDate.Text = GenUtil.str2DDMMYYYY(purchase.VendorInvoiceDate);
                        txtGrandTotal.Text = purchase.GrandTotal;


                        //double ETFOC = double.Parse(SqlDtr["FOC_Discount"].ToString()) * 2 / 100;
                        //coment by vikas 23.11.2012 txtDisc.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()); 
                        //txtDisc.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString());
                        DropDiscType.SelectedIndex = DropDiscType.Items.IndexOf((DropDiscType.Items.FindByValue(purchase.DiscountType)));
                        
                        txtNetAmount.Text = purchase.NetAmount;
                        txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());

                        txtPromoScheme.Text = purchase.PromoScheme;
                        txtRemark.Text = purchase.Remark;
                        lblEntryBy.Text = purchase.EntryBy;
                        lblEntryTime.Text = purchase.EntryTime.ToString();
                        txtCashDisc.Text = purchase.CashDiscount;
                        //txtCashDisc.Text = GenUtil.strNumericFormat(Request.Form["txtCashDisc"].ToString()); 

                        DropCashDiscType.SelectedIndex = DropCashDiscType.Items.IndexOf((DropCashDiscType.Items.FindByValue(purchase.CashDiscType)));
                        txtVAT.Text = purchase.VATAmount;
                        Textcgst.Text = purchase.CGSTAmount;
                        Textsgst.Text = purchase.SGSTAmount;
                        txtbirdless.Text = purchase.Birdless;
                        txttradeless.Text = purchase.Tradeless;

                        //03.07.09 double TotalCashDiscount=double.Parse(SqlDtr["Grand_Total"].ToString())+double.Parse(SqlDtr["Entry_Tax1"].ToString())-(double.Parse(SqlDtr["Trade_Discount"].ToString())+double.Parse(SqlDtr["FOC_Discount"].ToString())+double.Parse(SqlDtr["Discount"].ToString())+double.Parse(SqlDtr["Fixed_Discount_Type"].ToString())+double.Parse(SqlDtr["Ebird_Discount"].ToString())+ETFOC);

                        /*******Temp***********
						double G_Tot=double.Parse(SqlDtr["Grand_Total"].ToString());
						double E_Tax=double.Parse(SqlDtr["Entry_Tax1"].ToString());
						double Trade_Disc=double.Parse(SqlDtr["Trade_Discount"].ToString());
						double Foc_Disc=double.Parse(SqlDtr["FOC_Discount"].ToString());
						double Disc=double.Parse(SqlDtr["Discount"].ToString());
						double Fixd_Disc_Type=double.Parse(SqlDtr["Fixed_Discount_Type"].ToString());
						double Fixd_Disc=double.Parse(SqlDtr["Fixed_Discount"].ToString());
						double EB_Disc=double.Parse(SqlDtr["Ebird_Discount"].ToString());
						******************/

                        /*******Add by vikas 6.12.2012****************************/
                        double Fixed_Disc_Amount = 0;
                        if (purchase.FixedDiscount != null && purchase.FixedDiscount != "")
                            Fixed_Disc_Amount = double.Parse(purchase.FixedDiscount);
                        else
                            Fixed_Disc_Amount = 0;
                        /*******End by vikas 6.12.2012****************************/
                        double TotalDiscount = 0;//double.Parse(SqlDtr["Grand_Total"].ToString()) + double.Parse(SqlDtr["Entry_Tax1"].ToString()) - (double.Parse(SqlDtr["Trade_Discount"].ToString()) + double.Parse(SqlDtr["FOC_Discount"].ToString()) + double.Parse(SqlDtr["Fixed_Discount"].ToString()) + double.Parse(SqlDtr["Ebird_Discount"].ToString()) + ETFOC - TotalCashDiscount + Fixed_Disc_Amount);
                        if (DropDiscType.SelectedIndex == 0)
                        {
                            txtDisc.Text = GenUtil.strNumericFormat(purchase.Discount);
                            TotalDiscount = double.Parse(GenUtil.strNumericFormat(purchase.Discount)) * double.Parse(GenUtil.strNumericFormat(purchase.Totalqtyltr));
                            txtTotalDisc.Text = Convert.ToString(Math.Round(TotalDiscount));
                        }
                        else
                        {
                            txtDisc.Text = GenUtil.strNumericFormat(purchase.Discount);
                            if (purchase.DiscountType == "Per")
                                TotalDiscount = double.Parse(purchase.GrandTotal) * double.Parse(purchase.Discount) / 100;
                            txtTotalDisc.Text = GenUtil.strNumericFormat(TotalDiscount.ToString());
                        }
                        //Coment by vikas 2.1.2013 double TotalCashDiscount=double.Parse(SqlDtr["Grand_Total"].ToString())+double.Parse(SqlDtr["Entry_Tax1"].ToString())-(double.Parse(SqlDtr["Trade_Discount"].ToString())+double.Parse(SqlDtr["FOC_Discount"].ToString())+double.Parse(SqlDtr["Discount"].ToString())+double.Parse(SqlDtr["Fixed_Discount_Type"].ToString())+double.Parse(SqlDtr["Fixed_Discount"].ToString())+double.Parse(SqlDtr["Ebird_Discount"].ToString())+ETFOC);
                        var tradeDisc = double.Parse(purchase.TradeDiscount);
                        double TotalCashDiscount = double.Parse(purchase.GrandTotal) - tradeDisc - double.Parse(purchase.FocDiscount) - TotalDiscount + double.Parse(purchase.FixedDiscount) + double.Parse(purchase.EbirdDiscount) + Fixed_Disc_Amount;

                        if (purchase.CashDiscType == "Per")
                            TotalCashDiscount = TotalCashDiscount * double.Parse(purchase.CashDiscount) / 100;
                        else
                            TotalCashDiscount = double.Parse(GenUtil.strNumericFormat(purchase.CashDiscount)) * double.Parse(GenUtil.strNumericFormat(purchase.Totalqtyltr));

                        txtTotalCashDisc.Text = GenUtil.strNumericFormat(TotalCashDiscount.ToString());
                        
                        txttradedisamt.Text = GenUtil.strNumericFormat(purchase.TradedisAmount);
                        txtebird.Text = GenUtil.strNumericFormat(purchase.Ebird);
                        txtebirdamt.Text = GenUtil.strNumericFormat(purchase.EbirdAmount);
                        txtfoc.Text = GenUtil.strNumericFormat(purchase.FocDiscount);
                        dropfoc.SelectedIndex = dropfoc.Items.IndexOf((dropfoc.Items.FindByValue(purchase.FocDiscountType)));
                        //txtentry.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(22).ToString()); 
                        //dropentry.SelectedIndex= dropentry.Items.IndexOf((dropentry.Items.FindByValue(SqlDtr.GetValue(23).ToString())));
                        txtfixed.Text = GenUtil.strNumericFormat(purchase.FixedAmount);
                        //dropfixed.SelectedIndex= dropfixed.Items.IndexOf((dropfixed.Items.FindByValue(SqlDtr.GetValue(27).ToString())));
                        txtfixedamt.Text = purchase.FixedAmount;
                        txttotalqtyltr.Text = GenUtil.strNumericFormat(purchase.Totalqtyltr);
                        txttotalqtyltr1.Text = GenUtil.strNumericFormat(purchase.Totalqtyltr1);
                        //***************************

                        /*********Add by vikas 5.11.2012 **********************/
                        if (purchase.NewFixeddisc != null && purchase.NewFixeddisc != "")
                            txtfixdisc.Text = purchase.NewFixeddisc.Trim();
                        else
                            txtfixdisc.Text = "0";

                        if (purchase.NewFixeddiscAmount != null && purchase.NewFixeddiscAmount != "")
                            txtfixdiscamount.Text = purchase.NewFixeddiscAmount.Trim();
                        else
                            txtfixdiscamount.Text = "0";
                        /*********End**********************/

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
                        //Vendor_ID = SqlDtr["Vendor_ID"].ToString();
                        //CheckMode=SqlDtr["Mode_of_Payment"].ToString();

                        //txtAddDis.Text = SqlDtr.GetValue(26).ToString();   //Add by vikas 30.06.09

                        /*************************
						double Net_Amount=double.Parse(txtGrandTotal.Text.ToString())+ETFOC+Fixed_Disc_Amount+double.Parse(txtebirdamt.Text)+double.Parse(txttradedisamt.Text)+double.Parse(txtfixdiscamount.Text)+double.Parse(txtVAT.Text);
						txtNetAmount.Text=	Net_Amount.ToString();							//Add by Vikas7.3.2013
						/******************************/

                    }
                   
                        DropVendorID.SelectedIndex = (DropVendorID.Items.IndexOf((DropVendorID.Items.FindByValue(purchase.VendorID))));
                        lblPlace.Value = purchase.Place;
                    

                    #region Get Data from Purchase Details Table regarding Invoice No.
                    int TotalQty = 0;
                   
                    ProductModel pro = new ProductModel();
                    pro.InvoiceDate = DateTime.Parse(lblInvoiceDate.Text);
                    pro.Invoice_No = arrInvoiceNo[0];
                    List<ProductModel> products = new List<ProductModel>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(pro);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/purchase/GetProductDetails", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                        }
                    }
                    foreach (var product in products)
                    {
                        Rate[i].Enabled = true;
                        Qty[i].Enabled = true;
                        Amount[i].Enabled = true;
                        chkfoc[i].Enabled = true;
                        ProdType[i].Value = product.ProductType;
                        Qty[i].Text = product.Quantity;
                        if (Qty[i].Text != "")
                            TotalQty += System.Convert.ToInt32(Qty[i].Text);

                        Quantity[i].Text = Request.Form[Qty[i].ID.ToString()].ToString();

                        Rate[i].Text = product.Rate;
                        Amount[i].Text = product.Amount;
                        ProductType[i] = product.Category;
                        ProductName[i] = product.ProductName;
                        ProductPack[i] = product.ProductPack;
                        ProductQty[i] = product.Quantity;
                        if (product.FOC.Equals("0"))
                            chkfoc[i].Checked = false;
                        else
                            chkfoc[i].Checked = true;

                        tempStktSchDis[i].Value = product.TempStktSchDis;

                        tempSchDis[i].Value = product.TempSchDis;

                        tempSchAddDis[i].Value = product.TempSchAddDis;


                        if (double.Parse(product.Discount) > 0)
                            chkDisc[i].Checked = true;
                        else
                            chkDisc[i].Checked = false;
                        tempDiscount[i].Value = product.TempDiscount;

                        i++;

                    }
                    txttotalqty.Text = System.Convert.ToString(TotalQty);

                    /*****Add by vikas 10.12.2012********************/
                    Earlybird_dis();
                    /******end*******************/
                    /*Hide By Mahesh 16.08.007
					while(i<12)
					{
						ProdType[i].SelectedIndex=0;
						ProdType[i].Enabled = false; 
						ProdName[i].SelectedIndex=0;
						ProdName[i].Enabled = false; 
						PackType[i].Items.Clear();
						PackType[i].Items.Add("Select");
						PackType[i].SelectedIndex=(PackType[i].Items.IndexOf((PackType[i].Items.FindByValue ("Select"))));
						PackType[i].Enabled = false; 
						Qty[i].Text="";
						Qty[i].Enabled = false;
						Quantity[i].Text = "";
						Quantity[i].Enabled = false; 
						Rate[i].Text="";
						Rate[i].Enabled = false;
						Amount[i].Text="";
						Amount[i].Enabled = false;
						chkfoc[i].Enabled=false;//*bhal*
						//***********
						ProductType[i]="";
						ProductName[i]="";
						ProductPack[i]="";
						ProductQty[i]="";
						//***********
						i++;
					}
					*/
                    //SqlDtr.Close();
                    #endregion
                    //GetGstCalculation();

                }
                BtnEdit.Visible = false;
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnSaved_Click,Class:PartiesClass.cs,Method : DropInvoiceNo_SelectedIndexChanged " + "  Purchase Invoise  Updated to    :-" + "Invoice No=" + DropInvoiceNo.SelectedItem.Value.ToString() + "  userid " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnSaved_Click,Class:PartiesClass.cs,Method : DropInvoiceNo_SelectedIndexChanged " + "  Purchase Invoise  Updated to    :-" + "Invoice No=" + obj.Invoice_No + "  EXCEPTION  " + ex.Message + "  userid " + uid);
            }
        }
        private void Button1_Click(object sender, System.EventArgs e)
        {
        }
        protected void txtVehicleNo_TextChanged(object sender, System.EventArgs e)
        {
        }
        protected void DropModeType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        private void DropProd1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        protected void Dropdownlist1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        /// <summary>
        /// This method is used to save the record after that write into the report file to print.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
            //******
            if (FlagPrint == false)
                Save();
            else
            {
                btnPrint.CausesValidation = true;
                FlagPrint = false;
            }
            //*****
            print();
        }

        protected void txtfixed_TextChanged(object sender, System.EventArgs e)
        {

        }
        protected void txtVAT_TextChanged(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// This method is used to delete the particular record select from dropdownlist.
        /// </summary>
        public void DeleteTheRec()
        {
            try
            {
                //DropDownList[] DropType={DropType1,DropType2,DropType3,DropType4,DropType5,DropType6,DropType7,DropType8,DropType9,DropType10,DropType11,DropType12,DropType13,DropType14,DropType15,DropType16,DropType17,DropType18,DropType19,DropType20};
                HtmlInputText[] DropType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                //HtmlInputHidden[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20}; 
                //HtmlInputHidden[] PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12, txtPack13, txtPack14, txtPack15, txtPack16, txtPack17, txtPack18, txtPack19, txtPack20}; 
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);
                PurchaseModel purchase = new PurchaseModel();
                purchase.InvoiceNo = arrInvoiceNo[0];
                purchase.VendorID = DropVendorID.SelectedItem.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(purchase);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/purchase/DeletePurchaseInvoice", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var prodd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                    }
                }
                
                List<ProductModel> products = new List<ProductModel>();
                //for(int i=0;i<20;i++)
                for (int i = 0; i < ProductType.Length; i++)
                {
                    //if(DropType[i].SelectedItem.Text.Equals("Type") || ProdName[i].Value=="" || PackType[i].Value=="")
                    if (ProductType[i].ToString().Equals(""))
                        continue;
                    else
                    {
                        ProductModel product = new ProductModel();
                        product.Quantity = ProductQty[i].ToString();
                        product.ProductType = ProductType[i].ToString();
                        product.ProductName = ProductName[i].ToString();
                        product.ProductPack = ProductPack[i].ToString();
                        product.InvoiceDate = DateTime.Parse(GenUtil.str2MMDDYYYY(Request.Form["lblInvoiceDate"].ToString()));
                        products.Add(product);
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(products);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/purchase/DeleteProductDetails", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var prodd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                    }
                }
                SeqStockMaster();
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + DropInvoiceNo.SelectedItem.Text + " Deleted, user : " + uid);
                Clear();
                clear1();
                GetNextInvoiceNo();
                GetProducts();
                FatchInvoiceNo();
                lblInvoiceNo.Visible = true;
                DropInvoiceNo.Visible = false;
                BtnEdit.Visible = true;
                MessageBox.Show("Purchase Transaction Deleted");
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnDelete_Click - InvoiceNo : " + DropInvoiceNo.SelectedItem.Text + " ,Exception : " + ex.Message + " user : " + uid);
            }
        }

        /// <summary>
        /// This method is used to update the product qty in edit time.
        /// </summary>
        public void UpdateProductQty()
        {
            List<ProductModel> products = new List<ProductModel>();
            for (int i = 0; i < ProductType.Length; i++)
            {
                if (ProductType[i] == "" || ProductName[i] == "" || ProductQty[i] == "")
                    continue;
                else
                {
                    ProductModel product = new ProductModel();
                    product.ProductName = ProductName[i].ToString();
                    product.ProductType = ProductType[i].ToString();
                    product.Quantity = ProductQty[i].ToString();
                    product.InvoiceDate = DateTime.Parse(GenUtil.trimDate(Invoice_Date));
                    product.ProductPack = ProductPack[i].ToString();
                    products.Add(product);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(products);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/purchase/UpdateProductQty", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                        }
                    }
                }
            }
        }
        

        /// <summary>
        /// This method is used to update the product stock after update the purchase invoice in edit time.
        /// </summary>
        public void SeqStockMaster()
        {
            List<ProductModel> products = new List<ProductModel>();

            for (int i = 0; i < ProductType.Length; i++)
            {
                ProductModel product = new ProductModel();
                product.ProductType = ProductType[i].ToString();
                product.ProductName = ProductName[i].ToString();
                product.Quantity= ProductQty[i].ToString();
                product.ProductPack = ProductPack[i].ToString();
                products.Add(product);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(product);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/purchase/InsertSeqStockMaster", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                    }
                }
            }
        }

        /*public void UpdateBatchNo()
		{
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//InventoryClass obj = new InventoryClass();
			//SqlDataReader rdr;
			SqlCommand cmd;
			//rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+DropInvoiceNo.SelectedItem.Text+"'");
			//while(rdr.Read())
			//{
			//	Con.Open();
			//	cmd = new SqlCommand("update BatchNo set Qty=Qty-"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
			//	cmd.ExecuteNonQuery();
			//	cmd.Dispose();
			//	Con.Close();
			//}
			Con.Open();
			cmd = new SqlCommand("delete BatchNo where Trans_No='"+DropInvoiceNo.SelectedItem.Text+"'",Con);
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			Con.Close();
			Con.Open();
			cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='"+DropInvoiceNo.SelectedItem.Text+"' and Trans_Type='Purchase Invoice'",Con);
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			Con.Close();
		}*/

        /// <summary>
        /// This method is used to update the product sale qty according to batch no in edit time.
        /// </summary>
        public void UpdateBatchNo()
        {
            string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/UpdateBatchNo?invoiceNo="+arrInvoiceNo[0]).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    var batch = JsonConvert.DeserializeObject<List<string>>(disc);
                }
            }
        }
        protected void btnDelete_Click(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// This method is used to save the batch information in other table.
        /// </summary>
        /// <param name="Prod"></param>
        /// <param name="Pack"></param>
        /// <param name="Qty"></param>
        /// <param name="Batch"></param>
        /// <param name="Count"></param>
        public void InsertBatchNo(string Prod, string Pack, string Qty, string Batch, int Count)
        {
            try
            {
                ProductModel product = new ProductModel();
                product.ProductName = Prod;
                product.ProductPack = Pack;
                product.Quantity = Qty;
                product.Batch = Batch;
                product.ProductCount = Count;
                string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);

                product.Invoice_No = arrInvoiceNo[0];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(product);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/purchase/InsertBatchNo", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog(" Form : PurchaseInvoice.aspx , Class : PetrolPumpClass.cs ,Method:InsertBatchNo()    Purchase Invoice Preview Report Viewed  " + ex.Message + "  EXCEPTION " + " userid  " + uid);
            }
        }


    //    public void UpdateBatchNo1()
    //    {
    //        try
    //        {
    //            SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
    //            InventoryClass obj = new InventoryClass();
    //            SqlDataReader rdr;
    //            SqlCommand cmd;
    //            string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);
    //            rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='" + arrInvoiceNo[0] + "' and trans_type='Purchase invoice' and batch_id=0");
    //            while (rdr.Read())
    //            {
    //                Con.Open();
    //                cmd = new SqlCommand("update StockMaster_Batch set Receipt=Receipt-" + rdr["Qty"].ToString() + ",Closing_Stock=Closing_Stock-" + rdr["Qty"].ToString() + " where ProductID='" + rdr["Prod_ID"].ToString() + "' and Batch_ID='0'", Con);
    //                cmd.ExecuteNonQuery();
    //                cmd.Dispose();
    //                Con.Close();
    //            }
    //            rdr.Close();

    //            /*Con.Open();
				//cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='"+dropInvoiceNo.SelectedItem.Text+"' and Trans_Type='Sales Invoice'",Con);
				//cmd.ExecuteNonQuery();
				//cmd.Dispose();
				//Con.Close();*/
    //        }
    //        catch (Exception ex)
    //        {
    //            CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : UpdateBatchNo() EXCEPTION :  " + ex.Message + "   " + uid);
    //        }
    //    }


        /// <summary>
        /// This Method to write into the report file to print.
        /// </summary>
        public void PrePrintReport()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = @"E:\ServoSMS\Servosms\Sysitem\ServosmsPrintServices\ReportView\PurchaseInvoicePrePrintReport.txt";
                StreamWriter sw = new StreamWriter(path);
                //DropDownList[] ProdCat={DropType1,DropType2,DropType3,DropType4,DropType5,DropType6,DropType7,DropType8,DropType9,DropType10,DropType11,DropType12,DropType13,DropType14,DropType15,DropType16,DropType17,DropType18,DropType19,DropType20}; 
                HtmlInputText[] ProdCat = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                CheckBox[] foe = { chkfoc1, chkfoc2, chkfoc3, chkfoc4, chkfoc5, chkfoc6, chkfoc7, chkfoc8, chkfoc9, chkfoc10, chkfoc11, chkfoc12, chkfoc13, chkfoc14, chkfoc15, chkfoc16, chkfoc17, chkfoc18, chkfoc19, chkfoc20 };
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
                TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
                HtmlInputHidden[] tmpCgst = { tempCgst1, tempCgst2, tempCgst3, tempCgst4, tempCgst5, tempCgst6, tempCgst7, tempCgst8, tempCgst9, tempCgst10, tempCgst11, tempCgst12, tempCgst13, tempCgst14, tempCgst15, tempCgst16, tempCgst17, tempCgst18, tempCgst19, tempCgst20 };
                HtmlInputHidden[] tmpSgst = { tempSgst1, tempSgst2, tempSgst3, tempSgst4, tempSgst5, tempSgst6, tempSgst7, tempSgst8, tempSgst9, tempSgst10, tempSgst11, tempSgst12, tempSgst13, tempSgst14, tempSgst15, tempSgst16, tempSgst17, tempSgst18, tempSgst19, tempSgst20 };
                HtmlInputHidden[] tmpIgst = { tempIgst1, tempIgst2, tempIgst3, tempIgst4, tempIgst5, tempIgst6, tempIgst7, tempIgst8, tempIgst9, tempIgst10, tempIgst11, tempIgst12, tempIgst13, tempIgst14, tempIgst15, tempIgst16, tempIgst17, tempIgst18, tempIgst19, tempIgst20 };
                HtmlInputHidden[] tmpHsn = { tempHsn1, tempHsn2, tempHsn3, tempHsn4, tempHsn5, tempHsn6, tempHsn7, tempHsn8, tempHsn9, tempHsn10, tempHsn11, tempHsn12, tempHsn13, tempHsn14, tempHsn15, tempHsn16, tempHsn17, tempHsn18, tempHsn19, tempHsn20 };


                string[] DespQty = new string[20];
                string[] freeDespQty = new string[20];
                string[] ProdCode = new string[20];
                string[] PackType = new string[20];
                string[] ProdName = new string[20];
                string[] Igst = new string[20];
                string[] Cgst = new string[20];
                string[] Sgst = new string[20];
                string[] BatchNo = new string[20];

                Double TotalQtyPack = 0, TotalQtyfoe = 0, TotalfoeLtr = 0, TotalQtyPackLtr = 0, TotalQtyFoeLtr = 0;
                int k = 0;
                string info2 = "", info3 = "", info4 = "", info5 = "", info7 = "", str = "", InDate = "";//,info6="";
                info2 = " {0,-14:S} {1,-50:S} {2,20:S} {3,-46:S}";//Party Name & Address
                info4 = " {0,-20:S} {1,20:S} {2,20:S} {3,55:S} {4,15:S}";//Party Name & Address
                                                                         //info3=" {0,-12:S} {1,-35:S} {2,10:S} {3,10:S} {4,10:S} {5,12:S} {6,12:S} {7,10:S} {8,15:S}";//Item Code
                info3 = " {0,-12:S} {1,-19:S} {2,-35:S} {3,7:S} {4,7:S} {5,7:S} {6,10:S} {7,12:S} {8,15:S} {9,12:S} {10,12:S} {11,12:S}";//Item Code
                info5 = " {0,16:S} {1,-114:S}";
                info7 = " {0,46:S} {1,-88:S}";
                //info6=" {0,44:S} {1,-10:S} {2,-76:S}";
                //string curbal=lblCurrBalance.Value;
                string[] CurrBal = new string[2];
                string[] InvoiceDate = new string[2];
                string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);
                ProductModel product = new ProductModel();
                if (lblInvoiceNo.Visible == true)
                    product.Invoice_No = Request.Form["lblInvoiceNo"].ToString();
                else
                    product.Invoice_No = arrInvoiceNo[0];

                string[] arrProdType = new string[3];
                List<ProductModel> products = new List<ProductModel>();

                for (int i = 0; i < 20; i++)
                {
                    product = new ProductModel();
                    if (lblInvoiceNo.Visible == true)
                        product.Invoice_No = Request.Form["lblInvoiceNo"].ToString();
                    else
                        product.Invoice_No = arrInvoiceNo[0];
                    if (ProdCat[i].Value.IndexOf(":") > 0)
                        product.Category = ProdCat[i].Value;

                    products.Add(product);
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(products);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/purchase/PrePrintReport", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductModel>>(responseString);
                    }
                }
                int h = 0;
                foreach (var prod in products)
                {
                    if (prod != null)
                        InDate = prod.InvoiceDate.ToShortDateString();
                    else
                        InDate = "";
                    if (InDate != "")
                        InvoiceDate = InDate.Split(new char[] { ' ' }, InDate.Length);
                    else
                        InvoiceDate[1] = "";
                    if (prod != null)
                    {
                        ProdCode[h] = prod.ProductCode;
                        PackType[h] = prod.Package_Type;
                        ProdName[h] = prod.ProductName;
                        Igst[h] = tmpIgst[h].Value.ToString();
                        Cgst[h] = tmpCgst[h].Value.ToString();
                        Sgst[h] = tmpSgst[h].Value.ToString();
                        BatchNo[h] = tmpHsn[h].Value.ToString();
                    }
                    else
                    {
                        ProdCode[h] = "";
                        PackType[h] = "";
                        ProdName[h] = "";
                    }
                    h++;
                }

                //int p1=0;
                for (int j = 0; j < 20; j++)
                {
                    if (!foe[j].Checked)
                    {
                        if (!Request.Form[Qty[j].ID.ToString()].Equals(""))
                        {
                            TotalQtyPack = TotalQtyPack + System.Convert.ToDouble(Request.Form[Qty[j].ID.ToString()].ToString());

                            TotalQtyPackLtr += System.Convert.ToDouble(Request.Form[Qty[j].ID.ToString()].ToString()) * System.Convert.ToDouble(PackType[j].ToString());

                            DespQty[j] = Request.Form[Qty[j].ID.ToString()].ToString();

                        }
                        else
                            DespQty[j] = "";
                        freeDespQty[j] = "";
                    }
                    else
                    {
                        if (!Request.Form[Qty[j].ID.ToString()].ToString().Equals(""))
                        {
                            TotalQtyfoe = TotalQtyfoe + System.Convert.ToDouble(Request.Form[Qty[j].ID.ToString()]);

                            TotalQtyFoeLtr += System.Convert.ToDouble(Request.Form[Qty[j].ID.ToString()]) * System.Convert.ToDouble(PackType[j].ToString());

                            freeDespQty[j] = Request.Form[Qty[j].ID.ToString()].ToString();

                        }
                        else
                            freeDespQty[j] = "";
                        DespQty[j] = "";
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
                //ArrayList arrProdScheme = new ArrayList();
                ArrayList arrProdAmount = new ArrayList();
                ArrayList arrCgst = new ArrayList();
                ArrayList arrSgst = new ArrayList();
                ArrayList arrIgst = new ArrayList();

                //int q=0;
                for (int p = 0; p <= Qty.Length - 1; p++)
                {
                    if (Request.Form[Qty[p].ID.ToString()].ToString() != "")
                    {
                        string[] arrProdCat = ProdCat[p].Value.Split(new char[] { ':' }, ProdCat[p].Value.Length);
                        ProductModel prodct = new ProductModel();
                        prodct.ProductCode = ProdCode[p].ToString();
                        prodct.ProductName = arrProdCat[1].ToString();
                        prodct.Package_Type = arrProdCat[2].ToString();

                        if (lblInvoiceNo.Visible == true)
                            prodct.Invoice_No = Request.Form["lblInvoiceNo"].ToString();
                        else
                            prodct.Invoice_No = arrInvoiceNo[0];

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            var myContent = JsonConvert.SerializeObject(prodct);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("api/purchase/PrePrintProductDetails", byteContent).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                                prodct = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                            }
                        }

                        if (prodct != null && prodct.Quantity != null)
                        {
                            arrProdCode.Add(ProdCode[p].ToString());
                            if (!foe[p].Checked)
                            {
                                arrProdName.Add(ProdName[p].ToString());
                                arrBillQty.Add(prodct.Quantity);
                                arrProdRate.Add(Request.Form[Rate[p].ID.ToString()].ToString());

                                arrProdAmount.Add(System.Convert.ToString(double.Parse(prodct.Quantity) * double.Parse(Request.Form[Rate[p].ID.ToString()].ToString())));

                                arrFreeQty.Add("");
                            }
                            else
                            {
                                arrProdName.Add("Free " + ProdName[p].ToString());
                                arrBillQty.Add("");
                                arrProdRate.Add("");
                                arrProdAmount.Add("");
                                arrFreeQty.Add(prodct.Quantity);
                            }
                            arrBatchNo.Add(tmpHsn[p].Value);
                            arrCgst.Add(Cgst[p].ToString());
                            arrSgst.Add(Sgst[p].ToString());
                            arrIgst.Add(Igst[p].ToString());
                            arrDespQty.Add(prodct.Quantity);
                            arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString()) * double.Parse(prodct.Quantity)));
                            //arrProdScheme.Add(scheme[p].Text);
                            //}
                        }
                        else
                        {
                            arrProdCode.Add(ProdCode[p].ToString());
                            arrBatchNo.Add(BatchNo[p].ToString());
                            arrDespQty.Add(DespQty[p].ToString());
                            arrCgst.Add(Cgst[p].ToString());
                            arrSgst.Add(Sgst[p].ToString());
                            arrIgst.Add(Igst[p].ToString());
                            arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString()) * double.Parse(Request.Form[Qty[p].ID.ToString()].ToString())));

                            if (!foe[p].Checked)
                            {
                                arrProdName.Add(ProdName[p].ToString());
                                arrBillQty.Add(Request.Form[Qty[p].ID].ToString());

                                arrProdRate.Add(Request.Form[Rate[p].ID].ToString());

                                arrProdAmount.Add(Request.Form[Amount[p].ID].ToString());

                                arrFreeQty.Add("");
                            }
                            else
                            {
                                arrProdName.Add("Free " + ProdName[p].ToString());
                                arrBillQty.Add("");
                                arrProdRate.Add("");
                                arrProdAmount.Add("");
                                arrFreeQty.Add(Request.Form[Qty[p].ID].ToString());

                            }
                        }
                        //SqlDtr.Close();

                    }
                }

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
                    for (int i = 0; i < 14; i++)
                    {
                        sw.WriteLine("");
                    }

                    string addr = "", TinNo = "";//,ssc="";
                    PurchaseModel purchse = new PurchaseModel();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/purchase/GetSupplierDetails?vendorID=" + DropVendorID.SelectedItem.Text).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            purchse = JsonConvert.DeserializeObject<PurchaseModel>(disc);
                        }
                    }

                    if (purchse!=null)
                    {
                        addr = purchse.City;
                        //ssc=SqlDtr["sadbhavnacd"].ToString();
                        TinNo = purchse.Tin_No;
                    }
                    addr = addr.ToUpper();
                    if (lblInvoiceNo.Visible == true)
                        sw.WriteLine(info2, "", DropVendorID.SelectedItem.Text.ToUpper(), "", Request.Form["lblInvoiceNo"].ToString());
                    else

                    {
                        string invoiceNumber = null;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/purchase/GetInvoiceNumber?invoiceNo=" + arrInvoiceNo[0]).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var disc = Res.Content.ReadAsStringAsync().Result;
                                invoiceNumber = JsonConvert.DeserializeObject<string>(disc);
                            }
                        }
                        sw.WriteLine(info2, "", DropVendorID.SelectedItem.Text.ToUpper(), "", invoiceNumber);
                    }
                    if (addr != "")
                        sw.WriteLine(info2, "", GenUtil.TrimLength(addr, 50), "", Request.Form["lblInvoiceDate"].ToString());
                    else

                        sw.WriteLine(info2, "", "", "", Request.Form["lblInvoiceDate"].ToString());
                    sw.WriteLine(info2, "V_Inv_No/DT", Request.Form["txtVInnvoiceNo"].ToString() + " / " + Request.Form["txtVInvoiceDate"].ToString(), "", InvoiceDate[0]);
                    sw.WriteLine(info2, "Tin No", TinNo, "", "");
                    sw.WriteLine(info2, "", "", "", Request.Form["txtVehicleNo"].ToString());

                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine(info3, "P-Code", "  HSN", " Grade/Package Name ", " B-Qty", " F-Qty", " R-Qty", "  Ltr/Kg", "  Rate Rs.", "    Amount (Rs.)", " CGST (Rs.)", " SGST (Rs.)", " IGST(Rs.)");
                    sw.WriteLine("");
                    //arrCount++;
                    for (k = arrCount; k < arrBillQty.Count; k++, arrCount++)
                    {
                        sw.WriteLine(info3, arrProdCode[k].ToString(), arrBatchNo[k].ToString(), GenUtil.TrimLength(arrProdName[k].ToString(), 34), arrBillQty[k].ToString(), arrFreeQty[k].ToString(), arrDespQty[k].ToString(), arrLtrkg[k].ToString(), arrProdRate[k].ToString(), arrProdAmount[k].ToString(), arrCgst[k].ToString(), arrSgst[k].ToString(), arrIgst[k].ToString());
                        if (k == 15 && arrBillQty.Count < 25)
                            FlagCount = true;
                        if (k == 25)
                        {
                            FlagCount = true;
                            arrCount++;
                            break;
                        }
                        if (k == 51)
                        {
                            FlagCount = true;
                            arrCount++;
                            break;
                        }
                        if (k == 77)
                        {
                            FlagCount = true;
                            arrCount++;
                            break;
                        }
                        if (k == 103)
                        {
                            FlagCount = true;
                            arrCount++;
                            break;
                        }
                    }
                    Space = SpaceCount - 25;
                    if (Space > 0)
                    {
                        SpaceCount -= 25;
                        for (int r = 0; r <= 15; r++)
                        {
                            sw.WriteLine();
                        }
                    }
                    else
                    {
                        Space = Math.Abs(Space);
                        if (Space >= 10)
                        {
                            for (int r = 10; r <= Space; r++)
                            {
                                sw.WriteLine();
                            }
                        }
                        else
                        {
                            for (int r = 0; r <= Space + 17; r++)
                            {
                                sw.WriteLine();
                            }
                        }
                        SpaceCount = 0;
                    }
                }
                while (FlagCount == true);
                sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(info4, "", "Packs", "Ltrs", "GROSS AMOUNT         : ", Request.Form["txtGrandTotal"].ToString());

                //sw.WriteLine(info4, "", "----------", "----------", "Entry Tax(.Rs)       : ", Request.Form["txtentry"].ToString());

                if (Request.Form["txtfoc"].ToString() != "")
                    //sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txttotalqtyltr1.Text,"FOC Discount         : ","-"+txtfoc.Text);
                    sw.WriteLine(info4, "Act Qty", TotalQtyPack.ToString(), TotalQtyPackLtr.ToString(), "FOC Discount         : ", "-" + Request.Form["txtfoc"].ToString());
                else
                    //sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txttotalqtyltr1.Text,"FOC Discount         : ","0");
                    sw.WriteLine(info4, "Act Qty", TotalQtyPack.ToString(), TotalQtyPackLtr.ToString(), "FOC Discount         : ", "0");
                /*if(txttradedisamt.Text!="")
                    sw.WriteLine(info4,"","","","Trade Dis("+txttradedis.Text+")      : ","-"+txttradedisamt.Text);
                else
                    sw.WriteLine(info4,"","","","Trade Dis("+txttradedis.Text+")      : ","0");*/
                if (Request.Form["txttradedisamt"].ToString() != "")
                    sw.WriteLine(info4, "", "", "", "Trade Discount       : ", "-" + Request.Form["txttradedisamt"].ToString());
                else

                    sw.WriteLine(info4, "", "", "", "Trade Discount       : ", "0");
                if (Request.Form["txtTotalDisc"].ToString() != "")
                {
                    if (DropDiscType.SelectedIndex == 0)
                        sw.WriteLine(info4, "FOC Qty", TotalQtyfoe.ToString(), TotalQtyFoeLtr.ToString(), "Discount(" + DropDiscType.SelectedItem.Text + ")        : ", "-" + Request.Form["txtTotalDisc"].ToString());
                    else

                        sw.WriteLine(info4, "FOC Qty", TotalQtyfoe.ToString(), TotalQtyFoeLtr.ToString(), "Discount(" + DropDiscType.SelectedItem.Text + ")       : ", "-" + Request.Form["txtTotalDisc"].ToString());

                }
                else
                    sw.WriteLine(info4, "FOC Qty", TotalQtyfoe.ToString(), TotalQtyFoeLtr.ToString(), "Discount(" + DropDiscType.SelectedItem.Text + ")       : ", "0");
                if (Request.Form["txtTotalCashDisc"].ToString() != "")
                {
                    if (DropCashDiscType.SelectedIndex == 0)
                        sw.WriteLine(info4, "", "----------", "----------", "Cash Dis(" + Request.Form["txtCashDisc"].ToString() + DropCashDiscType.SelectedItem.Text + ")    : ", "-" + Request.Form["txtTotalCashDisc"].ToString());
                    else

                        sw.WriteLine(info4, "", "----------", "----------", "Cash Dis(" + Request.Form["txtCashDisc"].ToString() + DropCashDiscType.SelectedItem.Text + ")      : ", "-" + Request.Form["txtTotalCashDisc"].ToString());

                }
                else
                    sw.WriteLine(info4, "", "----------", "----------", "Cash Discount        : ", "0");
                sw.WriteLine(info4, "Total Qty", System.Convert.ToString(TotalQtyfoe + TotalQtyPack), System.Convert.ToString(System.Convert.ToDouble(Request.Form["txttotalqtyltr1"].ToString()) + TotalfoeLtr), "EBird Dis(" + Request.Form["txtebird"].ToString() + ")      : ", "-" + Request.Form["txtebirdamt"].ToString());

                sw.WriteLine(info4, "", "", "", "Total CGST         : ", tempTotalCgst.Value);
                sw.WriteLine(info4, "", "", "", "Total SGST         : ", tempTotalSgst.Value);
                sw.WriteLine(info4, "", "", "", "Total IGST         : ", tempTotalIgst.Value);

                sw.WriteLine(info4, "", "", "", "Net Amount           : ", string.IsNullOrEmpty(Request.Form["txtNetAmount"].ToString()) ? txtNetAmount.Text.ToString() : Request.Form["txtNetAmount"].ToString());

                sw.WriteLine(info5, "", GenUtil.ConvertNoToWord(string.IsNullOrEmpty(Request.Form["txtNetAmount"].ToString()) ? txtNetAmount.Text.ToString() : Request.Form["txtNetAmount"].ToString()));

                sw.WriteLine();
                sw.WriteLine(info7, "", Request.Form["txtRemark"].ToString());

                sw.Close();
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:PrePrintReport().  EXCEPTION: " + ex.Message + "  user " + uid);

            }
        }

        //		public void getscheme()
        //		{
        //			InventoryClass  obj=new InventoryClass ();
        //			SqlDataReader SqlDtr;
        //			string sql;
        //			string str="";
        //			SqlDataReader rdr=null; 
        //			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,per_discount o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";
        //			SqlDtr=obj.GetRecordSet(sql);
        //			while(SqlDtr.Read())
        //			{
        //				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"];
        //				string sql1="select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack freep,o.datefrom df,o.dateto dt,p.unit unit from products p,per_discount o where p.prod_id='"+SqlDtr["sch"]+"'";
        //				dbobj.SelectQuery(sql1,ref rdr); 
        //				string unit="";
        //				if(rdr.Read())
        //				{
        //					str=str+":"+rdr["cat1"]+":"+rdr["pname1"]+":"+rdr["ptype1"]+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
        //					unit =rdr["unit"].ToString();
        //				}
        //				else
        //				{
        //					str=str+":"+0+":"+0+":"+0+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
        //					unit ="";
        //				}
        //				rdr.Close();
        //
        //				#region Fetch Closing Stock
        //				string sql2="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr["sch"]+" order by stock_date desc";
        //				dbobj.SelectQuery(sql2,ref rdr); 
        //				if(rdr.Read())
        //					str=str+":"+rdr["Closing_Stock"]+":"+unit+":"+SqlDtr["dis"];
        //				else
        //					str=str+":0"+":"+unit+":"+SqlDtr["dis"];
        //				rdr.Close();
        //				str=str+":"+SqlDtr["scheme"]+",";
        //				#endregion
        //			}
        //			SqlDtr.Close();
        //			temptext11.Value=str;
        //		}

        /// <summary>
        /// This method is used to fatch the product scheme and stored in hidden textbox and closing stock also.
        /// </summary>
        public void getscheme()
        {
            string scheme = null;
            string invoicedt = GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/getscheme?invoiceDate="+invoicedt).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    scheme = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            temptext12.Value = scheme;
        }


        public void get_Addscheme()
        {
            string addScheme = null;
            string invoicedt = GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/get_Addscheme?invoiceDate="+invoicedt).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    addScheme = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            temptext_add1.Value = addScheme;
            
        }

        /// <summary>
        /// This method is used to fatch the product stockist scheme and stored in hidden textbox.
        /// </summary>
        /// <summary>
        /// This method is used to fatch the product stockist scheme and stored in hidden textbox.
        /// </summary>
        public void GetStockistScheme()
        {
            string stockistScheme = null;
            string invoicedt = GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/GetStockistScheme?invoiceDate="+invoicedt).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    stockistScheme = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            tempStktSchDis.Value = stockistScheme;
            
        }

        /// <summary>
        /// This method retrives discount Servostk from SetDis table.
        /// </summary>
        /// <returns></returns>
        //public string GetStockistDiscount()
        //{
        //    string strDiscount = "";
        //    InventoryClass obj = new InventoryClass();
        //    SqlDataReader SqlDtr;
        //    string sql;
        //    string str = "";
        //    sql = "select Servostk from SetDis";
        //    SqlDtr = obj.GetRecordSet(sql);
        //    while (SqlDtr.Read())
        //    {
        //        strDiscount = SqlDtr["Servostk"].ToString();
        //    }
        //    SqlDtr.Close();
        //    return strDiscount;
        //}


        public void GetProductsUnit()
        {
            string prodUnit = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/GetProductsUnit").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    prodUnit = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            tempUnit.Value = prodUnit;
            
        }

        public void GetFixedDiscount()
        {
            string fixedDisc = null;
            string invoicedt = GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim());
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/purchase/GetFixedDiscount?invoiceDate="+invoicedt).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    fixedDisc = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            tempFixedDisc.Value = fixedDisc;
            
        }


        /// <summary>
        /// This method is used to update the customer balance after update the invoice no in edit time.
        /// </summary>
        public void CustomerUpdate()
        {
            string[] CheckDate = Invoice_Date.Split(new char[] { ' ' }, Invoice_Date.Length);
            if (DateTime.Compare(System.Convert.ToDateTime(CheckDate[0].ToString()), System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["lblInvoiceDate"].ToString()))) > 0)

                Invoice_Date = GenUtil.str2MMDDYYYY(Request.Form["lblInvoiceDate"].ToString());
            ProductModel product = new ProductModel();
            string[] arrInvoiceNo = DropInvoiceNo.SelectedItem.Text.Split(new char[] { ':' }, DropInvoiceNo.SelectedItem.Text.Length);

            product.Invoice_No = arrInvoiceNo[0];

            product.InvoiceDate = DateTime.Parse(Invoice_Date);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var myContent = JsonConvert.SerializeObject(product);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/purchase/CustomerUpdate", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductModel>(responseString);
                }
            }            
        }

        /// <summary>
        /// This method is used to Prepares the excel report file PurchaseInvoicePreviewReport.xls for preview the report in details.
        /// </summary>
        protected void btnPreview_Click(object sender, System.EventArgs e)
        {
            try
            {
                HtmlInputText[] ProdType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12, DropType13, DropType14, DropType15, DropType16, DropType17, DropType18, DropType19, DropType20 };
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20 };
                TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20 };
                TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20 };
                HtmlInputHidden[] tempStktSchDis = { tempStktSchDis1, tempStktSchDis2, tempStktSchDis3, tempStktSchDis4, tempStktSchDis5, tempStktSchDis6, tempStktSchDis7, tempStktSchDis8, tempStktSchDis9, tempStktSchDis10, tempStktSchDis11, tempStktSchDis12, tempStktSchDis13, tempStktSchDis14, tempStktSchDis15, tempStktSchDis16, tempStktSchDis17, tempStktSchDis18, tempStktSchDis19, tempStktSchDis20 };
                CheckBox[] chkfoc = { chkfoc1, chkfoc2, chkfoc3, chkfoc4, chkfoc5, chkfoc6, chkfoc7, chkfoc8, chkfoc9, chkfoc10, chkfoc11, chkfoc12, chkfoc13, chkfoc14, chkfoc15, chkfoc16, chkfoc17, chkfoc18, chkfoc19, chkfoc20 };
                HtmlInputHidden[] tempSchDis = { tempSchDis1, tempSchDis2, tempSchDis3, tempSchDis4, tempSchDis5, tempSchDis6, tempSchDis7, tempSchDis8, tempSchDis9, tempSchDis10, tempSchDis11, tempSchDis12, tempSchDis13, tempSchDis14, tempSchDis15, tempSchDis16, tempSchDis17, tempSchDis18, tempSchDis19, tempSchDis20 };
                HtmlInputHidden[] tempDiscount = { tempSchDiscount1, tempSchDiscount2, tempSchDiscount3, tempSchDiscount4, tempSchDiscount5, tempSchDiscount6, tempSchDiscount7, tempSchDiscount8, tempSchDiscount9, tempSchDiscount10, tempSchDiscount11, tempSchDiscount12, tempSchDiscount13, tempSchDiscount14, tempSchDiscount15, tempSchDiscount16, tempSchDiscount17, tempSchDiscount18, tempSchDiscount19, tempSchDiscount20 };
                HtmlInputHidden[] tempSchAddDis = { tempSchAddDis1, tempSchAddDis2, tempSchAddDis3, tempSchAddDis4, tempSchAddDis5, tempSchAddDis6, tempSchAddDis7, tempSchAddDis8, tempSchAddDis9, tempSchAddDis10, tempSchAddDis11, tempSchAddDis12, tempSchAddDis13, tempSchAddDis14, tempSchAddDis15, tempSchAddDis16, tempSchAddDis17, tempSchAddDis18, tempSchAddDis19, tempSchAddDis20 };             //Add by vikas 30.06.09

                HtmlInputHidden[] tempFixdDisc = { tempFixedDisc1, tempFixedDisc2, tempFixedDisc3, tempFixedDisc4, tempFixedDisc5, tempFixedDisc6, tempFixedDisc7, tempFixedDisc8, tempFixedDisc9, tempFixedDisc10, tempFixedDisc11, tempFixedDisc12, tempFixedDisc13, tempFixedDisc14, tempFixedDisc15, tempFixedDisc16, tempFixedDisc17, tempFixedDisc18, tempFixedDisc19, tempFixedDisc20 };             //Add by vikas 30.06.09

                HtmlInputHidden[] tmpCgst = { tempCgst1, tempCgst2, tempCgst3, tempCgst4, tempCgst5, tempCgst6, tempCgst7, tempCgst8, tempCgst9, tempCgst10, tempCgst11, tempCgst12, tempCgst13, tempCgst14, tempCgst15, tempCgst16, tempCgst17, tempCgst18, tempCgst19, tempCgst20 };
                HtmlInputHidden[] tmpSgst = { tempSgst1, tempSgst2, tempSgst3, tempSgst4, tempSgst5, tempSgst6, tempSgst7, tempSgst8, tempSgst9, tempSgst10, tempSgst11, tempSgst12, tempSgst13, tempSgst14, tempSgst15, tempSgst16, tempSgst17, tempSgst18, tempSgst19, tempSgst20 };
                HtmlInputHidden[] tmpIgst = { tempIgst1, tempIgst2, tempIgst3, tempIgst4, tempIgst5, tempIgst6, tempIgst7, tempIgst8, tempIgst9, tempIgst10, tempIgst11, tempIgst12, tempIgst13, tempIgst14, tempIgst15, tempIgst16, tempIgst17, tempIgst18, tempIgst19, tempIgst20 };
                HtmlInputHidden[] tmpHsn = { tempHsn1, tempHsn2, tempHsn3, tempHsn4, tempHsn5, tempHsn6, tempHsn7, tempHsn8, tempHsn9, tempHsn10, tempHsn11, tempHsn12, tempHsn13, tempHsn14, tempHsn15, tempHsn16, tempHsn17, tempHsn18, tempHsn19, tempHsn20 };


                StreamWriter sw = null;
                double TotalInvoiceAmount = 0;
                for (int i = 0; i < ProdType.Length; i++)
                {
                    if (ProdType[i].Value != "" && Request.Form[Qty[i].ID].ToString() != "")
                    {
                        if (i == 0)
                        {
                            string home_drive = Environment.SystemDirectory;
                            home_drive = home_drive.Substring(0, 2);
                            string strExcelPath = home_drive + "\\Servosms_ExcelFile\\Export\\";
                            Directory.CreateDirectory(strExcelPath);
                            string path = home_drive + @"\Servosms_ExcelFile\Export\PurchaseInvoicePreviewReport1.xls";
                            sw = new StreamWriter(path);
                            sw.WriteLine("Product Name\tPack Type\tQuantity\tBasic\tTotal");
                        }
                        string str = ProdType[i].Value;
                        string[] Name = str.Split(new char[] { ':' }, str.Length);
                        sw.WriteLine(Name[1] + "\t" + Name[2] + "\t" + Request.Form[Qty[i].ID].ToString() + "\t" + Request.Form[Rate[i].ID].ToString() + "\t" + Request.Form[Amount[i].ID].ToString());

                        double entrytax = 0, stktDisc = 0, bird = 0, foc = 0, etfoc = 0, disc = 0, cashdisc = 0, perdisc = 0, perdiscAdd = 0;
                        string[] stkt;

                        if (tempSchDis[i].Value != "")
                        {
                            string discname = tempSchDis[i].Value;
                            string[] arrstr = discname.Split(new char[] { ':' }, discname.Length);
                            if (arrstr[1] == "%")
                                perdisc = double.Parse(Request.Form[Amount[i].ID].ToString()) * double.Parse(arrstr[0]) / 100;
                            else

                                perdisc = double.Parse(GenUtil.changeqtyltr(Name[2], int.Parse(Request.Form[Qty[i].ID].ToString()))) * double.Parse(arrstr[0]);

                        }

                        /****Add by vikas 30.06.09******************/
                        if (tempSchAddDis[i].Value != "")
                        {
                            string discname_Add = tempSchAddDis[i].Value;
                            string[] arrstr = discname_Add.Split(new char[] { ':' }, discname_Add.Length);
                            if (arrstr[1] == "%")
                                perdiscAdd = double.Parse(Request.Form[Amount[i].ID].ToString()) * double.Parse(arrstr[0]) / 100;
                            else

                                perdiscAdd = double.Parse(GenUtil.changeqtyltr(Name[2], int.Parse(Request.Form[Qty[i].ID].ToString()))) * double.Parse(arrstr[0]);

                        }
                        /*****end*****************/

                        //Coment by vikas 23.5.2013 entrytax=(double.Parse(Amount[i].Text)-perdisc)*2/100;
                        entrytax = (double.Parse(Request.Form[Amount[i].ID].ToString())) * 2 / 100;

                        if (chkfoc[i].Checked)
                        {
                            foc = double.Parse(Request.Form[Amount[i].ID].ToString());

                            etfoc = foc * 2 / 100;
                        }
                        else
                        {
                            if (tempStktSchDis[i].Value != "")    // coment by vikas 09.06.09 
                            {
                                stkt = tempStktSchDis[i].Value.Split(new char[] { ':' }, tempStktSchDis[i].Value.Length);
                                if (stkt[1] == "%")
                                {
                                    /**********Add by Vikas 29.12.2012********************/
                                    string[] qty_Ltr = Name[2].Split(new char[] { 'X' }, Name[2].Length);
                                    double Tot_Ltr = double.Parse(qty_Ltr[0].ToString()) * double.Parse(qty_Ltr[1]);

                                    if (Tot_Ltr >= 50)
                                    {
                                        /*******Add by vikas 1.11.2012*****************/
                                        stktDisc = (double.Parse(Request.Form[Amount[i].ID].ToString())) * double.Parse(stkt[0]) / 100;
                                        /******end******************/
                                    }
                                    else
                                    {
                                        /*************End by Vikas 29.12.2012*****************/

                                        //coment by vikas 1.11.2012 stktDisc=double.Parse(Amount[i].Text)*double.Parse(stkt[0])/100;
                                        /*******Add by vikas 1.11.2012*****************/
                                        double Dealer_VAT = double.Parse(Request.Form[Amount[i].ID].ToString()) + entrytax;

                                        Dealer_VAT = Dealer_VAT * double.Parse(txtVatRate.Value.ToString()) / 100;
                                        stktDisc = (double.Parse(Request.Form[Amount[i].ID].ToString()) + entrytax + Dealer_VAT) * double.Parse(stkt[0]) / 100;
                                        /******end******************/
                                    }
                                }
                                else
                                {
                                    /*******Add by vikas 1.11.2012*****************
									double Dealer_VAT=double.Parse(Amount[i].Text)+entrytax;
									Dealer_VAT=Dealer_VAT*double.Parse(txtVatRate.Value.ToString())/100;
									stktDisc=(double.Parse(Amount[i].Text)+entrytax+Dealer_VAT)*double.Parse(stkt[0])/100;
									******end******************/
                                    string[] qty_Ltr = Name[2].Split(new char[] { 'X' }, Name[2].Length);
                                    double Tot_Ltr = double.Parse(qty_Ltr[0].ToString()) * double.Parse(qty_Ltr[1]) * Double.Parse(Request.Form[Qty[i].ID].ToString());

                                    stktDisc = Tot_Ltr * double.Parse(stkt[0].ToString());
                                }
                                /*coment by vikas 29.12.2012 if(txtebird.Text!="")
								{
									bird=double.Parse(GenUtil.changeqtyltr(Name[2],int.Parse(Qty[i].Text)))*double.Parse(txtebird.Text);
								}*/
                            }
                        }
                        if (Request.Form["txtebird"].ToString() != "")
                        {
                            bird = double.Parse(GenUtil.changeqtyltr(Name[2], int.Parse(Request.Form[Qty[i].ID].ToString()))) * double.Parse(Request.Form["txtebird"].ToString());

                        }
                        if (tempDiscount[i].Value != "")
                        {
                            disc = double.Parse(tempDiscount[i].Value);
                            //disc=double.Parse(txtDisc.Text);
                            //if(DropDiscType.SelectedItem.Text=="%")
                            //	disc=double.Parse(Amount[i].Text)*double.Parse(txtDisc.Text)/100;
                        }

                        /*********Add by Vikas**********************
						
						if(Name[2]!="")
						{
							string[] qty_Ltr=Name[2].Split(new char[] {'X'},Name[2].Length);
							double Tot_Ltr=double.Parse(qty_Ltr[0].ToString())*double.Parse(qty_Ltr[1])*Double.Parse(Qty[i].Text.ToString());
							fixdDisc=Tot_Ltr*double.Parse(tempfixdisc.Value.ToString());
						}
						*********End**********************/
                        double fixdDisc = 0;
                        if (tempFixdDisc[i].Value != "")                 // coment by vikas 2/1/2013 
                        {
                            stkt = tempFixdDisc[i].Value.Split(new char[] { ':' }, tempFixdDisc[i].Value.Length);
                            if (stkt[1] == "%")
                            {

                            }
                            else
                            {
                                string[] qty_Ltr = Name[2].Split(new char[] { 'X' }, Name[2].Length);
                                double Tot_Ltr = double.Parse(qty_Ltr[0].ToString()) * double.Parse(qty_Ltr[1]) * Double.Parse(Request.Form[Qty[i].ID].ToString());

                                fixdDisc = Tot_Ltr * double.Parse(stkt[0].ToString());
                            }
                        }
                        if (Request.Form["txtCashDisc"].ToString() != "")
                        {
                            //03.07.09 cashdisc=double.Parse(Amount[i].Text)+entrytax-(stktDisc+foc+bird+etfoc+disc+perdisc);
                            //coment by vikas 1.11.2012 cashdisc=double.Parse(Amount[i].Text)+entrytax-(stktDisc+foc+bird+etfoc+disc+perdisc+perdiscAdd);
                            cashdisc = double.Parse(Request.Form[Amount[i].ID].ToString()) + entrytax - (stktDisc + foc + bird + etfoc + disc + perdisc + perdiscAdd + fixdDisc);

                            cashdisc = cashdisc * double.Parse(Request.Form["txtCashDisc"].ToString()) / 100;

                        }

                        //03.07.09 double vat=double.Parse(Amount[i].Text)+entrytax-(stktDisc+foc+bird+disc+cashdisc+perdisc);
                        //coment by vikas 31.10.2012 double vat=double.Parse(Amount[i].Text)+entrytax-(stktDisc+foc+bird+disc+cashdisc+perdisc+perdiscAdd);
                        double vat = double.Parse(Request.Form[Amount[i].ID].ToString()) + entrytax - (stktDisc + foc + bird + disc + cashdisc + perdisc + perdiscAdd + fixdDisc);

                        vat = vat * double.Parse(txtVatRate.Value) / 100;
                        //03.07.09 double total=double.Parse(Amount[i].Text)+entrytax+vat-(stktDisc+cashdisc+bird+foc+disc+perdisc);
                        //coment by vikas 31.10.2012 double total=double.Parse(Amount[i].Text)+entrytax+vat-(stktDisc+cashdisc+bird+foc+disc+perdisc+perdiscAdd);
                        double total = double.Parse(Request.Form[Amount[i].ID].ToString()) + entrytax + vat - (stktDisc + cashdisc + bird + foc + disc + perdisc + perdiscAdd + fixdDisc);


                        sw.WriteLine("Entry Tax\t\t\t\t" + GenUtil.strNumericFormat(entrytax.ToString()));
                        sw.WriteLine("Servo Stockist Discount\t\t\t\t" + "-" + GenUtil.strNumericFormat(stktDisc.ToString()));
                        sw.WriteLine("FOC Discount\t\t\t\t" + "-" + GenUtil.strNumericFormat(foc.ToString()));
                        sw.WriteLine("Add Discount (%)\t\t\t\t" + "-" + GenUtil.strNumericFormat(perdiscAdd.ToString()));
                        sw.WriteLine("Discount(%)\t\t\t\t" + "-" + GenUtil.strNumericFormat(perdisc.ToString()));
                        sw.WriteLine("Early Bird Discount\t\t\t\t" + "-" + GenUtil.strNumericFormat(bird.ToString()));
                        //coment by vikas 22.12.2012 sw.WriteLine("Discount\t\t\t\t"+"-"+GenUtil.strNumericFormat(disc.ToString()));
                        sw.WriteLine("Scheme Discount\t\t\t\t" + "-" + GenUtil.strNumericFormat(disc.ToString()));
                        sw.WriteLine("Fixd Discount\t\t\t\t" + "-" + GenUtil.strNumericFormat(fixdDisc.ToString()));               //Add by vikas 1.11.2012
                        sw.WriteLine("Credit Discount\t\t\t\t" + "-" + GenUtil.strNumericFormat(cashdisc.ToString()));
                        sw.WriteLine("Vat Payable\t\t\t\t" + GenUtil.strNumericFormat(vat.ToString()));
                        sw.WriteLine("Total\t\t\t\t" + GenUtil.strNumericFormat(total.ToString()));
                        TotalInvoiceAmount += total;
                        sw.WriteLine();
                    }
                }
                string[] tot = null;
                TotalInvoiceAmount = double.Parse(GenUtil.strNumericFormat(TotalInvoiceAmount.ToString()));
                if (TotalInvoiceAmount.ToString().IndexOf(".") > 0)
                {
                    tot = TotalInvoiceAmount.ToString().Split(new char[] { '.' }, TotalInvoiceAmount.ToString().Length);
                    if (double.Parse(tot[1]) >= 50)
                        TotalInvoiceAmount = double.Parse(tot[0]) + 1;
                    else
                        TotalInvoiceAmount = double.Parse(tot[0]);
                }
                sw.WriteLine("Round Off\t\t\t\t" + "." + tot[1].ToString());
                sw.WriteLine("Total Invoice Amount\t\t\t\t" + TotalInvoiceAmount.ToString());
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("First Close The Open Excel File");
                CreateLogFiles.ErrorLog("Form:PurchaseInvoice.aspx,Method:btnPreview_Click    Purchase Invoice Preview Report Viewed  " + ex.Message + "  EXCEPTION " + " userid  " + uid);
            }
        }

        protected void txtTotalCashDisc_TextChanged(object sender, System.EventArgs e)
        {

        }
    }
}