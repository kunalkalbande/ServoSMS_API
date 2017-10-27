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
    /// Summary description for PerformaInvoice.
    /// </summary>
    public partial class PerformaInvoice : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DropDownList DropSalesType;
        protected System.Web.UI.WebControls.CompareValidator CompareValidator2;
        protected System.Web.UI.WebControls.DropDownList DropUnderSalesMan;
        protected System.Web.UI.WebControls.TextBox txtChallanNo;
        protected System.Web.UI.WebControls.TextBox txtChallanDate;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
        protected System.Web.UI.WebControls.TextBox txtVehicleNo;
        protected System.Web.UI.WebControls.DropDownList DropVehicleNo;
        protected System.Web.UI.HtmlControls.HtmlInputText lblDueDate;
        protected System.Web.UI.HtmlControls.HtmlInputText lblCurrBalance;
        protected System.Web.UI.HtmlControls.HtmlInputText lblCreditLimit;
        DBUtil dbobj = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"], true);
        string uid;
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
        static string[] ProductType = new string[12];
        static string[] ProductName = new string[12];
        static string[] ProductPack = new string[12];
        static string[] ProductQty = new string[12];
        static string[] SchProductType = new string[12];
        static string[] SchProductName = new string[12];
        static string[] SchProductPack = new string[12];
        static string[] SchProductQty = new string[12];
        public bool address = false;
        public static string val = "";
        protected System.Web.UI.WebControls.RequiredFieldValidator rfv1;

        /// This method is used for setting the Session variable for userId and after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user and generate the next ID also.

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                uid = (Session["User_Name"].ToString());
                txtMessage.Text = (Session["Message"].ToString());
                txtVatRate.Value = (Session["VAT_Rate"].ToString());
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:pageload" + ex.Message + "  EXCEPTION " + "   " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                try
                {
                    //Invoice_Date="";
                    checkPrevileges();
                    lblInvoiceDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                    //06.08.09 vikas lblEntryTime.Text=DateTime.Now.ToString ();

                    InventoryClass obj = new InventoryClass();
                    SqlDataReader SqlDtr;
                    string sql;

                    #region Fetch the Product Types and fill in the ComboBoxes
                    sql = "select distinct Prod_Code,Prod_name,Pack_Type from Products p,price_updation pu where p.prod_id=pu.prod_id";
                    SqlDtr = obj.GetRecordSet(sql);
                    if (SqlDtr.HasRows)
                    {
                        texthiddenprod.Value = "Type,";
                        while (SqlDtr.Read())
                        {
                            texthiddenprod.Value += SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ":" + SqlDtr.GetValue(2).ToString() + ",";
                            //coment by vikas 09.06.09 texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString()+",";
                        }
                    }
                    SqlDtr.Close();
                    #endregion

                    /*Coment  by Vikas 06.08.09 #region Fetch All SalesMan and Fill in the ComboBox
					sql = "Select Emp_Name from Employee where Designation ='Servo Sales Representative' order by Emp_Name";
					SqlDtr = obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
						DropUnderSalesMan.Items.Add (SqlDtr.GetValue(0).ToString ());
					}
					SqlDtr.Close ();
					#endregion*/

                    #region fill hiddentext 
                    //10.06.09 sql="select cust_name from customer order by cust_name ";
                    sql = "select cust_name,City from customer order by cust_name ";
                    SqlDtr = obj.GetRecordSet(sql);
                    int i = 0;
                    //if(SqlDtr.Read())
                    //{
                    val = "";
                    while (SqlDtr.Read())
                    {
                        //coment by vikas 10.06.09 texthidden.Value+=SqlDtr.GetValue(0).ToString()+",";
                        texthidden.Value += SqlDtr.GetValue(0).ToString() + ":" + SqlDtr.GetValue(1).ToString() + ",";
                        val += SqlDtr.GetValue(0).ToString() + ",";
                        //val+=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString()+",";
                        i++;
                    }
                    //}
                    SqlDtr.Close();
                    #endregion

                    #region Fetch All Discount and fill in the textbox
                    sql = "select * from SetDis";
                    SqlDtr = obj.GetRecordSet(sql);
                    if (SqlDtr.Read())
                    {
                        if (SqlDtr["CashDisSalesStatus"].ToString() == "1")
                        {
                            txtCashDisc.Text = SqlDtr["CashDisSales"].ToString();
                            if (SqlDtr["CashDisLtrSales"].ToString() == "Rs.")
                                DropCashDiscType.SelectedIndex = 0;
                            else
                                DropCashDiscType.SelectedIndex = 1;
                        }
                        else
                            txtCashDisc.Text = "";
                        if (SqlDtr["DiscountSalesStatus"].ToString() == "1")
                        {
                            txtDisc.Text = SqlDtr["DiscountSales"].ToString();
                            if (SqlDtr["DisLtrSales"].ToString() == "Rs.")
                                DropDiscType.SelectedIndex = 0;
                            else
                                DropDiscType.SelectedIndex = 1;
                        }
                        else
                            txtDisc.Text = "";
                    }
                    else
                    {
                        txtDisc.Text = "";
                        txtCashDisc.Text = "";
                        DropCashDiscType.SelectedIndex = 0;
                        DropDiscType.SelectedIndex = 0;
                    }
                    SqlDtr.Close();
                    #endregion
                    PriceUpdation();
                    GetProducts();
                    FetchData();
                    GetFOECust();
                    getscheme();
                    getscheme1();
                    getschemefoe();
                    getschemeSecSP();
                }
                catch (Exception ex)
                {
                    CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:pageload. EXCEPTION: " + ex.Message + "  User_ID: " + uid);
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

        public void PriceUpdation()
        {
            InventoryClass obj = new InventoryClass();
            var dsPriceUpdation = obj.ProPriceUpdation();
            var dtTable = dsPriceUpdation.Tables[0];
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                txtMainIGST.Value = txtMainIGST.Value + dtTable.Rows[i][0].ToString();//ProductCode
                txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][1];//ProductName 
                txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][2];//ProductId
                txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][3];//IGST
                txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][4];//cGST
                txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][5];//sGST
                txtMainIGST.Value = txtMainIGST.Value + "|" + dtTable.Rows[i][6];//HSN
                txtMainIGST.Value = txtMainIGST.Value + "~";


            }
            txtMainIGST.Value = txtMainIGST.Value.Substring(0, txtMainIGST.Value.LastIndexOf("~"));
        }


        /// <summary>
        /// This method store all foe discount in a hidden textbox.
        /// </summary>
        public void getschemefoe()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string sql1;
            string str = "";
            SqlDataReader rdr = null;

            sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust,o.distype  from products p,foe o where p.prod_id=o.prodid  and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "'";
            SqlDtr = obj.GetRecordSet(sql);
            if (SqlDtr.HasRows)
            {
                while (SqlDtr.Read())
                {
                    str = str + ":" + SqlDtr["cat"].ToString().Trim() + ":" + SqlDtr["pname"].ToString().Trim() + ":" + SqlDtr["ptype"].ToString().Trim() + ":" + SqlDtr["dis"].ToString() + ":" + SqlDtr["distype"].ToString();
                    sql1 = "select cust_name from customer where cust_id='" + SqlDtr["cust"] + "'";
                    dbobj.SelectQuery(sql1, ref rdr);
                    if (rdr.Read())
                    {
                        str = str + ":" + rdr["cust_name"].ToString().Trim() + ",";
                    }
                    rdr.Close();
                }
                SqlDtr.Close();
            }

            string sql2 = "select p.cust_name cust,o.datefrom df,o.dateto dt,o.discount dis,o.custid cust  from customer p,foe o where p.cust_id=o.custid and o.prodid='0'  and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "'";
            dbobj.SelectQuery(sql2, ref rdr);
            while (rdr.Read())
            {
                str = str + ":" + "0" + ":" + "0" + ":" + "0" + ":" + rdr["dis"] + ":" + rdr["cust"].ToString().Trim() + ",";
            }
            rdr.Close();
            temptextfoe.Value = str;
        }

        /// <summary>
        /// This method stored only Secondry spacial liter scheme information.
        /// </summary>
        public void getschemeSecSP()
        {
            /*Coment by vikas 2.1.2013 InventoryClass  obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string sql;
			string str="";
			
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"'";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";
			}
			SqlDtr.Close();
			temptextSecSP.Value=str;*/

            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string str = "";
            sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype,o.group_name gname,o.unit from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry SP(LTRSP Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "'";       //add by vikas 26.10.2012
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["scheme"] + ":" + SqlDtr["distype"];

                if (SqlDtr["gname"].ToString().Trim() != null && SqlDtr["gname"].ToString().Trim() != "")
                    str = str + ":" + SqlDtr["gname"] + ",";
                else
                    str = str + ":" + 0 + ",";
            }
            SqlDtr.Close();
            temptextSecSP.Value = str;
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
            string SubModule = "5";
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
            //if(Edit_Flag=="0" )
            //	btnEdit.Enabled=false; 
            //			if(Del_Flag=="0" )
            //				btnDelete.Enabled=false;
            //			if(Add_Flag=="0")
            //			{
            //				btnSave.Enabled = false; 
            //				Button1.Enabled = false; 
            //			}
            #endregion
        }

        /// <summary>
        /// This method store all customer information in a hidden textbox.
        /// </summary>
        public void FetchData()
        {
            InventoryClass obj = new InventoryClass();
            InventoryClass obj1 = new InventoryClass();
            SqlDataReader rdr1 = null;
            SqlDataReader rdr3 = null;
            string str1 = "";
            DateTime duedate;
            string duedatestr = "";

            rdr3 = obj.GetRecordSet("select c.City,CR_Days,Curr_Credit,Cust_ID,SSR,Cust_Name,Emp_Name  from Customer c,Employee e where e.Emp_ID=c.SSR order by Cust_Name");
            while (rdr3.Read())
            {
                duedate = DateTime.Now.AddDays(System.Convert.ToDouble(rdr3["CR_Days"]));
                duedatestr = (duedate.ToShortDateString());
                //str1 = str1+rdr3["Cust_Name"].ToString()+"~"+rdr3["City"].ToString().Trim()+"~"+GenUtil.str2DDMMYYYY(duedatestr.Trim())+"~"+GenUtil.strNumericFormat(rdr3["Curr_Credit"].ToString().Trim())+"~";
                str1 = str1 + rdr3["Cust_Name"].ToString() + "~" + rdr3["City"].ToString().Trim() + "#";
                //rdr1 = obj1.GetRecordSet("select top 1 Balance,BalanceType from customerledgertable where CustID="+rdr3["Cust_ID"]+" order by EntryDate Desc");
                //if(rdr1.Read())
                //{
                //	str1 = str1+GenUtil.strNumericFormat(rdr1["Balance"].ToString().Trim())+"~"+rdr1["BalanceType"].ToString().Trim()+"~";	
                //}
                //else
                //{
                //	str1 = str1+"0"+"~"+" "+"~";	
                //}
                //rdr1.Close();
                //str1+=rdr3["Emp_Name"].ToString()+"#";
            }
            rdr3.Close();
            TxtVen.Value = str1;
        }

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
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:Type_Changed().   EXCEPTION: " + ex.Message + "  User_ID: " + uid);
            }
        }

        public void Prod_Changed(DropDownList ddType, DropDownList ddProd, DropDownList ddPack, TextBox txtPurRate)
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
            sql = "select top 1 Sal_Rate from Price_Updation where Prod_ID=(select  Prod_ID from Products where Prod_Name='" + ddProd.SelectedItem.Value + "' and Pack_Type='" + ddPack.SelectedItem.Value + "') order by eff_date desc";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                txtPurRate.Text = SqlDtr.GetValue(0).ToString();
            }
            SqlDtr.Close();
            #endregion
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
        /// This method clears the form.
        /// </summary>
        public void Clear()
        {
            //NetAmount="0";
            //Invoice_Date="";
            //CustID="";
            txtCashDiscount.Text = "";
            txtDiscount.Text = "";
            tempInvoiceDate.Value = "";
            tempDelinfo.Value = "";
            //vikas 06.08.09 txtChallanNo.Text="";
            //vikas 06.08.09 DropSalesType.SelectedIndex=0;
            //vikas 06.08.09 TxtCrLimit.Value="";
            //vikas 06.08.09 DropUnderSalesMan.SelectedIndex=0;
            text1.Value = "Select";
            lblPlace.Value = "";
            //vikas 06.08.09 lblDueDate.Value="";
            //vikas 06.08.09 lblCurrBalance .Value="";
            //vikas 06.08.09 txtVehicleNo.Text="";
            //vikas 06.08.09 DropVehicleNo.Visible = false;
            //vikas 06.08.09 txtVehicleNo.Visible = true;
            txtPromoScheme.Text = "";
            txtRemark.Text = "";
            txtGrandTotal.Text = "";
            //vikas 06.08.09 lblCreditLimit.Value = "";
            txtDisc.Text = "";
            txtNetAmount.Text = "";
            DropDiscType.SelectedIndex = 0;
            txtVAT.Text = "";
            txtCashDisc.Text = "";
            DropCashDiscType.SelectedIndex = 0;
            Yes.Checked = true;
            No.Checked = false;
            txtfleetoediscount.Text = "";
            dropfleetoediscount.SelectedIndex = 0;
            txtfleetoediscountRs.Text = "";
            txtliter.Text = "";
            txtschemetotal.Text = "";

            #region Clear Hidden TextBoxex
            totalltr.Value = "";
            tempdiscount.Value = "";
            tempcashdis.Value = "";
            HtmlInputText[] DropType = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
            TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
            TextBox[] TextRate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
            TextBox[] TextAmount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
            TextBox[] Scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
            TextBox[] Foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
            TextBox[] AVStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };
            TextBox[] TempQty = { txtTempQty1, txtTempQty2, txtTempQty3, txtTempQty4, txtTempQty5, txtTempQty6, txtTempQty7, txtTempQty8, txtTempQty9, txtTempQty10, txtTempQty11, txtTempQty12 };
            HtmlInputHidden[] tmpQty = { tmpQty1, tmpQty2, tmpQty3, tmpQty4, tmpQty5, tmpQty6, tmpQty7, tmpQty8, tmpQty9, tmpQty10, tmpQty11, tmpQty12 };
            //TextBox[] ProdNamesch={txtProdsch1, txtProdsch2, txtProdsch3, txtProdsch4, txtProdsch5, txtProdsch6, txtProdsch7, txtProdsch8, txtProdsch9, txtProdsch10, txtProdsch11, txtProdsch12}; 
            TextBox[] ProdTypesch = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
            TextBox[] Avlsch = { txtstk1, txtstk2, txtstk3, txtstk4, txtstk5, txtstk6, txtstk7, txtstk8, txtstk9, txtstk10, txtstk11, txtstk12 };
            //TextBox[] PackTypesch={txtPacksch1, txtPacksch2, txtPacksch3, txtPacksch4, txtPacksch5, txtPacksch6, txtPacksch7, txtPacksch8, txtPacksch9, txtPacksch10, txtPacksch11, txtPacksch12}; 
            TextBox[] Qtysch = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
            HtmlInputHidden[] tmpSchType = { tmpSchType1, tmpSchType2, tmpSchType3, tmpSchType4, tmpSchType5, tmpSchType6, tmpSchType7, tmpSchType8, tmpSchType9, tmpSchType10, tmpSchType11, tmpSchType12 };
            TextBox[] SchQuantity = { txtTempSchQty1, txtTempSchQty2, txtTempSchQty3, txtTempSchQty4, txtTempSchQty5, txtTempSchQty6, txtTempSchQty7, txtTempSchQty8, txtTempSchQty9, txtTempSchQty10, txtTempSchQty11, txtTempSchQty12 };
            HtmlInputHidden[] SchSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
            HtmlInputHidden[] tmpFoeType = { tmpFoeType1, tmpFoeType2, tmpFoeType3, tmpFoeType4, tmpFoeType5, tmpFoeType6, tmpFoeType7, tmpFoeType8, tmpFoeType9, tmpFoeType10, tmpFoeType11, tmpFoeType12 };

            for (int ii = 0; ii < ProdTypesch.Length; ii++)
            {
                //ProdNamesch[ii].Text="";
                ProdTypesch[ii].Text = "";
                Avlsch[ii].Text = "";
                //PackTypesch[ii].Text="";
                Qtysch[ii].Text = "";
                SchQuantity[ii].Text = "";
                ProductType[ii] = "";
                ProductName[ii] = "";
                ProductPack[ii] = "";
                ProductQty[ii] = "";
                SchProductType[ii] = "";
                SchProductName[ii] = "";
                SchProductPack[ii] = "";
                SchProductQty[ii] = "";
                tmpSchType[ii].Value = "";
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
                tmpFoeType[ii].Value = "";
            }
            #endregion
        }

        /// <summary>
        /// This method is used to clear the form.
        /// </summary>
        public void clear1()
        {
            TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
            TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
            TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
            TextBox[] AvStock = { txtAvStock1, txtAvStock2, txtAvStock3, txtAvStock4, txtAvStock5, txtAvStock6, txtAvStock7, txtAvStock8, txtAvStock9, txtAvStock10, txtAvStock11, txtAvStock12 };
            for (int i = 0; i < Qty.Length; i++)
            {
                Qty[i].Enabled = true;
                Rate[i].Enabled = true;
                Amount[i].Enabled = true;
                AvStock[i].Enabled = true;
            }
            lblInvoiceDate.Text = GenUtil.str2DDMMYYYY(DateTime.Today.ToShortDateString());
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
                string[] year = dt.IndexOf("/") > 0 ? dt.Split(new char[] { '/' }, dt.Length) : dt.Split(new char[] { '-' }, dt.Length);
                string yr = year[2].Substring(2);
                return (yr);
            }
            else
                return "";
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
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:print" + " Sales Invoise is Print  userid   " + "   " + uid);
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
                    CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:print" + ane.Message + "  EXCEPTION " + " user " + uid);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:print" + se.Message + "  EXCEPTION " + " user " + uid);
                }
                catch (Exception es)
                {
                    Console.WriteLine("Unexpected exception : {0}", es.ToString());
                    CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:print" + es.Message + "  EXCEPTION " + " user " + uid);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:print" + ex.Message + "  EXCEPTION " + " user " + uid);
            }
        }

        /// <summary>
        /// This method checks the price updation for all the products is available or not?
        /// </summary>
        public void GetProducts()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                InventoryClass obj1 = new InventoryClass();
                SqlDataReader SqlDtr;
                string sql;
                SqlDataReader rdr = null;
                int count = 0;
                int count1 = 0;
                dbobj.ExecuteScalar("Select Count(Prod_id) from  products", ref count);
                dbobj.ExecuteScalar("select count(distinct p.Prod_ID ) from products p, Price_Updation pu where p.Prod_id = pu.Prod_id", ref count1);

                if (count != count1)
                {
                    lblMessage.Text = "Price updation not available for some products";
                }

                #region Fetch the Product Types and fill in the ComboBoxes
                string str = "", MinMax = "";
                sql = "select distinct p.Prod_ID,Category,Prod_Name,Pack_Type,Unit,minlabel,maxlabel,reorderlable from products p, Price_Updation pu where p.Prod_id = pu.Prod_id order by Category,Prod_Name";
                SqlDtr = obj.GetRecordSet(sql);
                while (SqlDtr.Read())
                {
                    #region Fetch Sales Rate
                    str = str + SqlDtr["Category"] + ":" + SqlDtr["Prod_Name"] + ":" + SqlDtr["Pack_Type"];
                    sql = "select top 1 Pur_Rate from Price_Updation where Prod_ID=" + SqlDtr["Prod_ID"] + " order by eff_date desc";
                    rdr = obj1.GetRecordSet(sql);
                    if (rdr.Read())
                    {
                        if (double.Parse(rdr["Pur_Rate"].ToString()) != 0)
                            str = str + ":" + rdr["Pur_Rate"];
                        else
                        {
                            rdr.Close();
                            continue;
                        }
                    }
                    else
                        str = str + ":0";
                    rdr.Close();

                    MinMax = MinMax + SqlDtr["Prod_Name"] + ":" + SqlDtr["Pack_Type"] + ":" + SqlDtr["minlabel"] + ":" + SqlDtr["maxlabel"] + ":" + SqlDtr["reorderlable"] + "~";

                    #endregion

                    #region Fetch Closing Stock
                    sql = "select top 1 Closing_Stock from Stock_Master where productid=" + SqlDtr["Prod_ID"] + " order by stock_date desc";
                    rdr = obj1.GetRecordSet(sql);
                    if (rdr.Read())
                        str = str + ":" + rdr["Closing_Stock"] + ":" + SqlDtr["Unit"];
                    else
                        str = str + ":0" + ":" + SqlDtr["Unit"];
                    rdr.Close();
                    #endregion
                    #region Fetch Scheme 
                    sql = "select discount from oilscheme where ProdID=" + SqlDtr["Prod_ID"] + "";
                    rdr = obj1.GetRecordSet(sql);
                    if (rdr.Read())
                        str = str + ":" + rdr["discount"] + ",";
                    else
                        str = str + ":0" + ",";
                    rdr.Close();
                    #endregion
                }
                SqlDtr.Close();
                temptext.Value = str;
                tempminmax.Value = MinMax;
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:GetProducts().  EXCEPTION: " + ex.Message + "  user " + uid);
            }
        }

        /// <summary>
        /// This method get only fleet or oe type customer.
        /// </summary>
        public void GetFOECust()
        {
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string str = "";
            string sql = "select cust_Name from customer  where cust_type='Fleet' or cust_type like('Oe%')  order by cust_Name";
            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = str + "," + SqlDtr.GetValue(0).ToString().Trim();
            }
            SqlDtr.Close();
            temptext13.Value = str;
        }

        /// <summary>
        /// This method stored given scheme of the products and closing stock also.
        /// </summary>
        public void getscheme()
        {
            /*coment by vikas 20.12.2012 InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string str="";
			SqlDataReader rdr=null; 
			 
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"];
				string sql1="select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack freep,o.datefrom df,o.dateto dt,p.unit unit from products p,oilscheme o where p.prod_id='"+SqlDtr["sch"]+"'";
				dbobj.SelectQuery(sql1,ref rdr); 
				string unit="";
				if(rdr.Read())
				{
					str=str+":"+rdr["cat1"]+":"+rdr["pname1"]+":"+rdr["ptype1"]+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
					unit =rdr["unit"].ToString();
				}
				else
				{
					str=str+":"+0+":"+0+":"+0+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
					unit ="";
				}
				rdr.Close();

				#region Fetch Closing Stock
				string sql2="select top 1 Closing_Stock from Stock_Master where productid="+SqlDtr["sch"]+" order by stock_date desc";
				dbobj.SelectQuery(sql2,ref rdr); 
				if(rdr.Read())
					str=str+":"+rdr["Closing_Stock"]+":"+unit+":"+SqlDtr["dis"];
				else
					str=str+":0"+":"+unit+":"+SqlDtr["dis"];
				rdr.Close();
				str=str+":"+SqlDtr["scheme"]+",";
				#endregion
			}
			SqlDtr.Close();
			temptext11.Value=str;*/

            int i = 0;
            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string str = "";
            SqlDataReader rdr = null;

            //coment by vikas 2.11.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";

            //coment by vikas 10.11.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,Group_Name GName,o.Unit Unit   from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";

            sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,Group_Name GName,o.Unit Unit,o.Pack_Type Packtype from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";        //Add by vikas 7.11.2012

            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"];
                string sql1 = "select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack freep,o.datefrom df,o.dateto dt,p.unit unit from products p,oilscheme o where p.prod_id='" + SqlDtr["sch"] + "'";
                dbobj.SelectQuery(sql1, ref rdr);
                string unit = "";
                if (rdr.Read())
                {
                    str = str + ":" + rdr["cat1"] + ":" + rdr["pname1"] + ":" + rdr["ptype1"] + ":" + SqlDtr["one"] + ":" + SqlDtr["freep"] + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString())) + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
                    unit = rdr["unit"].ToString();
                }
                else
                {
                    str = str + ":" + 0 + ":" + 0 + ":" + 0 + ":" + SqlDtr["one"] + ":" + SqlDtr["freep"] + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString())) + ":" + GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()));
                    unit = "";
                }
                rdr.Close();

                #region Fetch Closing Stock
                string sql2 = "select top 1 Closing_Stock from Stock_Master where productid=" + SqlDtr["sch"] + " order by stock_date desc";
                dbobj.SelectQuery(sql2, ref rdr);
                if (rdr.Read())
                    str = str + ":" + rdr["Closing_Stock"] + ":" + unit + ":" + SqlDtr["dis"];
                else
                    str = str + ":0" + ":" + unit + ":" + SqlDtr["dis"];
                rdr.Close();
                //coment by vikas 2.11.2012 str=str+":"+SqlDtr["scheme"]+",";
                str = str + ":" + SqlDtr["scheme"];
                #endregion

                /*********add by vikas 2.11.2012************/
                if (SqlDtr["GName"].ToString().Trim() != null && SqlDtr["GName"].ToString().Trim() != "")
                    str = str + ":" + SqlDtr["GName"];       // 
                else
                    str = str + ":" + 0;

                if (SqlDtr["unit"].ToString().Trim() != null && SqlDtr["unit"].ToString().Trim() != "")
                    str = str + ":" + SqlDtr["unit"];
                else
                    str = str + ":" + 0;
                /*****************************/

                if (SqlDtr["Packtype"].ToString().Trim() != null && SqlDtr["Packtype"].ToString().Trim() != "")
                    str = str + ":" + SqlDtr["Packtype"] + ",";
                else
                    str = str + ":" + 0 + ",";

                i++;
            }
            SqlDtr.Close();
            temptext11.Value = str;
        }

        /// <summary>
        /// This method stored only liter scheme information.
        /// </summary>
        public void getscheme1()
        {
            /*coment by vikas 20.12.2012 InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string str="";
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"'";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";
			}
			SqlDtr.Close();
			temptext12.Value=str;*/

            InventoryClass obj = new InventoryClass();
            SqlDataReader SqlDtr;
            string sql;
            string str = "";

            //Coment by vikas 2.11.2012 sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype  from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) +"'";

            sql = "select p.category cat,p.prod_name pname,p.pack_type ptype,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme,o.discounttype distype,o.group_name gname,o.unit unit,sch_id from products p,oilscheme o where p.prod_id=o.prodid and o.schname in ('Secondry(LTR Scheme)','Primary(LTR&% Scheme)') and cast(floor(cast(o.datefrom as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "' and cast(floor(cast(o.dateto as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.Trim()) + "'";    //Add by vikas 2.11.2012

            SqlDtr = obj.GetRecordSet(sql);
            while (SqlDtr.Read())
            {
                //coment by vikas 2.11.2012 str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"]+":"+SqlDtr["dis"]+":"+SqlDtr["scheme"]+":"+SqlDtr["distype"]+",";
                str = str + ":" + SqlDtr["cat"] + ":" + SqlDtr["pname"] + ":" + SqlDtr["ptype"] + ":" + SqlDtr["dis"] + ":" + SqlDtr["scheme"] + ":" + SqlDtr["distype"];     //add by vikas 2.11.2012

                /*********add by vikas 2.11.2012************/
                if (SqlDtr["gname"].ToString().Trim() != null && SqlDtr["gname"].ToString().Trim() != "")
                    str = str + ":" + SqlDtr["gname"];       // 
                else
                    str = str + ":" + 0;

                if (SqlDtr["unit"].ToString().Trim() != null && SqlDtr["unit"].ToString().Trim() != "")
                    str = str + ":" + SqlDtr["unit"] + ",";
                else
                    str = str + ":" + 0 + ",";
                /*****************************/
            }
            SqlDtr.Close();
            temptext12.Value = str;
        }

        /// <summary>
        /// This Method to write into the report file to print.
        /// </summary>
        public void PrePrintReport()
        {
            try
            {
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;
                //getTemplateDetails();
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PerformaInvoiceReport.txt";
                //string path = @"E:\ServoSMS\Servosms\Sysitem\ServosmsPrintServices\ReportView\PerformaInvoiceReport.txt";
                StreamWriter sw = new StreamWriter(path);
                HtmlInputText[] ProdCat = { DropType1, DropType2, DropType3, DropType4, DropType5, DropType6, DropType7, DropType8, DropType9, DropType10, DropType11, DropType12 };
                TextBox[] foe = { txtfoe1, txtfoe2, txtfoe3, txtfoe4, txtfoe5, txtfoe6, txtfoe7, txtfoe8, txtfoe9, txtfoe10, txtfoe11, txtfoe12 };
                TextBox[] Qty = { txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12 };
                TextBox[] Rate = { txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12 };
                TextBox[] Amount = { txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12 };
                TextBox[] scheme = { txtsch1, txtsch2, txtsch3, txtsch4, txtsch5, txtsch6, txtsch7, txtsch8, txtsch9, txtsch10, txtsch11, txtsch12 };
                TextBox[] schProdType = { txtTypesch1, txtTypesch2, txtTypesch3, txtTypesch4, txtTypesch5, txtTypesch6, txtTypesch7, txtTypesch8, txtTypesch9, txtTypesch10, txtTypesch11, txtTypesch12 };
                TextBox[] schQty = { txtQtysch1, txtQtysch2, txtQtysch3, txtQtysch4, txtQtysch5, txtQtysch6, txtQtysch7, txtQtysch8, txtQtysch9, txtQtysch10, txtQtysch11, txtQtysch12 };
                HtmlInputHidden[] SecSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
                HtmlInputHidden[] tmpCgst = { tempCgst1, tempCgst2, tempCgst3, tempCgst4, tempCgst5, tempCgst6, tempCgst7, tempCgst8, tempCgst9, tempCgst10, tempCgst11, tempCgst12, tempCgst13, tempCgst14, tempCgst15, tempCgst16, tempCgst17, tempCgst18, tempCgst19, tempCgst20 };
                HtmlInputHidden[] tmpSgst = { tempSgst1, tempSgst2, tempSgst3, tempSgst4, tempSgst5, tempSgst6, tempSgst7, tempSgst8, tempSgst9, tempSgst10, tempSgst11, tempSgst12, tempSgst13, tempSgst14, tempSgst15, tempSgst16, tempSgst17, tempSgst18, tempSgst19, tempSgst20 };
                HtmlInputHidden[] tmpIgst = { tempIgst1, tempIgst2, tempIgst3, tempIgst4, tempIgst5, tempIgst6, tempIgst7, tempIgst8, tempIgst9, tempIgst10, tempIgst11, tempIgst12, tempIgst13, tempIgst14, tempIgst15, tempIgst16, tempIgst17, tempIgst18, tempIgst19, tempIgst20 };
                HtmlInputHidden[] tmpHsn = { tempHsn1, tempHsn2, tempHsn3, tempHsn4, tempHsn5, tempHsn6, tempHsn7, tempHsn8, tempHsn9, tempHsn10, tempHsn11, tempHsn12, tempHsn13, tempHsn14, tempHsn15, tempHsn16, tempHsn17, tempHsn18, tempHsn19, tempHsn20 };
                HtmlInputHidden[] tmpAmount = { tmpAmount1, tmpAmount2, tmpAmount3, tmpAmount4, tmpAmount5, tmpAmount6, tmpAmount7, tmpAmount8, tmpAmount9, tmpAmount10, tmpAmount11, tmpAmount12 };
                HtmlInputHidden[] tmpscheme = { tmpSch1, tmpSch2, tmpSch3, tmpSch4, tmpSch5, tmpSch6, tmpSch7, tmpSch8, tmpSch9, tmpSch10, tmpSch11, tmpSch12 };


                int h1 = System.Convert.ToInt32(Math.Floor((Header1Height * 25) / 4.05));
                int h2 = System.Convert.ToInt32(Math.Floor((Header2Height * 25) / 4.05));
                int bh = System.Convert.ToInt32(Math.Floor((BodyHeight * 25) / 4.05));
                int f1 = System.Convert.ToInt32(Math.Floor((Footer1Height * 25) / 4.05));
                int f2 = System.Convert.ToInt32(Math.Floor((Footer2Height * 25) / 4.05));
                int pn = System.Convert.ToInt32(Math.Floor((Position1 * 25) / 1.53));
                int sp = 50;
                int dn = System.Convert.ToInt32(Math.Floor((Position2 * 25) / 1.53));
                dn = dn - (pn + 50);
                int sp1 = 10;
                string info21 = " {0,-16:S} {1,-12:F} {2,-20:F} {3,5:F}";
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
                //string info31 = " {0,-" + pc + ":S} {1,-" + bn + ":F} {2,-" + gpn + ":F} {3," + bq + ":F} {4," + fq + ":F} {5," + dq + ":F} {6," + lkg + ":F} {7," + rt + ":F} {8," + sd + ":F} {9," + am + ":F}";

                //Coment by vikas 14.08.09 string info31 = " {0,-" + pc + ":S} {1,-" + bn + ":F} {2,-" + gpn + ":F} {3," + dq + ":F} {4," + lkg + ":F} {5," + rt + ":F} {6," + sd + ":F} {7," + SSpDis + ":F} {8," + am + ":F}";

                string info31 = " {0,-16:S} {1,-12:F} {2,-20:F} {3,5:F} {4,6:F} {5,8:F} {6,8:F} {7,8:F} {8,12:F} {9,12:F} {10,12:F} {11,12:F} ";

                int rinw = System.Convert.ToInt32(Math.Floor((RupeesinWords * 25) / 1.53));
                int pb = System.Convert.ToInt32(Math.Floor((ProvisionalBalance * 25) / 1.53));
                int rem = System.Convert.ToInt32(Math.Floor((Remarks * 25) / 1.53));
                string info51 = " {0,-" + rinw + ":S} {1,-" + 100 + ":S}";
                string info61 = " {0,-" + pb + ":S} {1,-10:S} {2,-40:S}";
                string info71 = " {0,-" + rem + ":S} {1,-80:S}";
                Double TotalQtyPack = 0, TotalQtyfoe = 0, TotalfoeLtr = 0;
                int k = 0;
                string info4 = "";   //,InDate="",info2="",info3="",,info5="",info6="",info7="",str="";
                                     //info2=" {0,-16:S} {1,-50:S} {2,20:S} {3,-44:S}";//Party Name & Address
                info4 = " {0,-20:S} {1,20:S} {2,20:S} {3,55:S} {4,15:S}";//Party Name & Address
                                                                         //info3=" {0,-10:S} {1,-19:S} {2,-35:S} {3,5:S} {4,5:S} {5,5:S} {6,10:S} {7,12:S} {8,10:S} {9,15:S}";//Item Code
                                                                         //info5=" {0,18:S} {1,-112:S}";
                                                                         //info7=" {0,10:S} {1,-120:S}";
                                                                         //info6=" {0,46:S} {1,-10:S} {2,-74:S}";

                for (int j = 0; j < 12; j++)
                {
                    if (!Qty[j].Text.Equals(""))
                    {
                        TotalQtyPack = TotalQtyPack + System.Convert.ToDouble(Qty[j].Text);
                    }
                    if (!schQty[j].Text.Equals(""))
                    {
                        TotalQtyfoe = TotalQtyfoe + System.Convert.ToDouble(schQty[j].Text);
                    }
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

                // Condensed
                sw.Write((char)27);             //added by vishnu
                sw.Write((char)67);             //added by vishnu
                sw.Write((char)0);              //added by vishnu
                sw.Write((char)12);             //added by vishnu

                sw.Write((char)27);             //added by vishnu
                sw.Write((char)78);             //added by vishnu
                sw.Write((char)5);              //added by vishnu

                sw.Write((char)27);             //added by vishnu
                sw.Write((char)15);

                int arrCount = 0;
                double SpaceCount = Qty.Length;           //Space=0,
                bool FlagCount = false;

                do
                {
                    FlagCount = false;
                    sw.WriteLine("                                                PERFORMA INVOICE / QUOTATION");
                    for (int i = 0; i < h1 - 1; i++)
                    {
                        sw.WriteLine("");
                    }
                    string addr = "";
                    //Coment by vikas 14.08.09 dbobj.SelectQuery("select * from customer where cust_name='"+text1.Value+"'",ref SqlDtr);

                    /**********Add by vikas 14.08.09******************************/
                    string cust_name = "";
                    cust_name = text1.Value.ToString().Substring(0, text1.Value.IndexOf(':'));
                    dbobj.SelectQuery("select * from customer where cust_name='" + cust_name.ToString() + "'", ref SqlDtr);
                    /*****************End***********************/

                    if (SqlDtr.Read())
                    {
                        addr = SqlDtr["Address"].ToString();
                    }
                    SqlDtr.Close();
                    addr = addr.ToUpper();
                    if (addr.Length > 50)
                        addr = addr.Substring(0, 49);
                    PartyName = true;
                    DocumentNo = true;
                    if (PartyName)
                    {
                        if (DocumentNo)
                        {

                            //Coment bY vikas 14.08.09 sw.WriteLine(info21,"",text1.Value.ToUpper(),"","");
                            sw.WriteLine(info21, "", cust_name.ToString().ToUpper(), "", "");
                        }
                        else
                        {
                            //Coment bY vikas 14.08.09 sw.WriteLine(info21,"",text1.Value.ToUpper(),"","");
                            sw.WriteLine(info21, "", cust_name.ToString().ToUpper(), "", "");
                        }
                    }
                    else if (DocumentNo)
                    {
                        sw.WriteLine(info21, "", "", "", "");
                    }
                    //if(Address)
                    {
                        sw.WriteLine(info21, "", addr, "", "");
                    }
                    //if(City)
                    {
                        sw.WriteLine(info21, "City", lblPlace.Value.ToUpper(), "", "");
                    }
                    sw.WriteLine();
                    if (Blank)
                        sw.WriteLine();
                    if (Blank1)
                        sw.WriteLine();
                    //sw.WriteLine(info31,"P-Code","  Batch No"," Grade/Package Name","B-Qty","F-Qty"," D-Qty"," Ltr/Kg"," Rate Rs."," Sch Disc."," Amount (Rs.)");
                    sw.WriteLine(info31, "P-Code", "  HSN ", " Grade/Package Name", " D-Qty", " Ltr/Kg", " Rate Rs.", " Sch Disc.", "SP Disc.", " Amount (Rs.)", "CGST (Rs.)", "SGST (Rs.)", "IGST (Rs.)");
                    sw.WriteLine("");
                    int rowCounter = 0;
                    for (k = arrCount; k < Qty.Length; k++, arrCount++)
                    {
                        if (Qty[k].Text != "")
                        {
                            string[] pname = ProdCat[k].Value.Split(new char[] { ':' }, ProdCat[k].Value.Length);
                            string PCode = "", totalqty = "";
                            // coment by vikas 09.06.09 dbobj.SelectQuery("select prod_code,total_qty from products where prod_name='"+pname[0]+"' and pack_type='"+pname[1]+"'",ref SqlDtr);
                            dbobj.SelectQuery("select prod_code,total_qty from products where prod_name='" + pname[1] + "' and pack_type='" + pname[2] + "'", ref SqlDtr);
                            if (SqlDtr.Read())
                            {
                                PCode = SqlDtr.GetValue(0).ToString();
                                totalqty = SqlDtr.GetValue(1).ToString();
                            }
                            SqlDtr.Close();
                            //sw.WriteLine(info31,PCode,"",GenUtil.TrimLength(ProdCat[k].Value,35),Qty[k].Text,"",Qty[k].Text,System.Convert.ToString(double.Parse(Qty[k].Text)*double.Parse(totalqty)),Rate[k].Text,scheme[k].Text,Amount[k].Text);

                            //Coment by vikas 14.08.09 sw.WriteLine(info31,PCode,"",GenUtil.TrimLength(ProdCat[k].Value,35),Qty[k].Text,System.Convert.ToString(double.Parse(Qty[k].Text)*double.Parse(totalqty)),Rate[k].Text,scheme[k].Text,SecSP[k].Value,Amount[k].Text);

                            sw.WriteLine(info31, PCode, tmpHsn[k].Value.ToString(), GenUtil.TrimLength(pname[1] + ":" + pname[2], 35), Qty[k].Text, System.Convert.ToString(double.Parse(Qty[k].Text) * double.Parse(totalqty)), Rate[k].Text, tmpscheme[k].Value.ToString(), SecSP[k].Value, tmpAmount[k].Value.ToString(), tmpCgst[k].Value.ToString(), tmpSgst[k].Value.ToString(), tmpIgst[k].Value.ToString());

                            rowCounter++;
                            //if(k==bh-10 && Qty.Length<bh-2)
                            //							{
                            //								FlagCount=true;
                            //							}
                            //							if(k==bh-2)
                            //							{
                            //								FlagCount=true;
                            //								arrCount++;
                            //								break;
                            //							}
                            //							if(k==(bh*2)-3)
                            //							{
                            //								FlagCount=true;
                            //								arrCount++;
                            //								break;
                            //							}
                        }
                        if (schQty[k].Text != "")
                        {
                            string[] pname = ProdCat[k].Value.Split(new char[] { ':' }, ProdCat[k].Value.Length);
                            string PCode = "", totalqty = "";
                            //Coment by vikas 09.06.09 dbobj.SelectQuery("select prod_code,total_qty from products where prod_name='"+pname[0]+"' and pack_type='"+pname[1]+"'",ref SqlDtr);
                            dbobj.SelectQuery("select prod_code,total_qty from products where prod_name='" + pname[1] + "' and pack_type='" + pname[2] + "'", ref SqlDtr);
                            if (SqlDtr.Read())
                            {
                                PCode = SqlDtr.GetValue(0).ToString();
                                totalqty = SqlDtr.GetValue(1).ToString();
                            }
                            SqlDtr.Close();
                            //Coment by vikas 14.08.09 sw.WriteLine(info31,PCode,"",GenUtil.TrimLength("(Free) "+ProdCat[k].Value,35),"",schQty[k].Text,schQty[k].Text,System.Convert.ToString(double.Parse(schQty[k].Text)*double.Parse(totalqty)),"","","");

                            sw.WriteLine(info31, PCode, tmpHsn[k].Value.ToString(), GenUtil.TrimLength(pname[1] + ":" + pname[2], 35), Qty[k].Text, System.Convert.ToString(double.Parse(Qty[k].Text) * double.Parse(totalqty)), Rate[k].Text, tmpscheme[k].Value.ToString(), SecSP[k].Value, tmpAmount[k].Value.ToString(), tmpCgst[k].Value.ToString(), tmpSgst[k].Value.ToString(), tmpIgst[k].Value.ToString());
                            rowCounter++;
                        }
                    }
                    for (int l = rowCounter; l < 32; l++)
                    {
                        sw.WriteLine();
                    }
                    //				
                    //					Space=SpaceCount-(bh-2);
                    //					if(Space>0)
                    //					{
                    //						SpaceCount-=(bh-2);
                    //						for(int r=0;r<=(bh-10);r++)
                    //						{
                    //							sw.WriteLine();
                    //						}
                    //					}
                    //					else
                    //					{
                    //						Space=Math.Abs(Space);
                    //						if(Space>=8)
                    //						{
                    //							for(int r=8;r<=Space;r++)
                    //							{
                    //								sw.WriteLine();
                    //							}
                    //						}
                    //						else
                    //						{
                    //							for(int r=0;r<=Space+f2;r++)
                    //							{
                    //								sw.WriteLine();
                    //							}
                    //						}
                    //						SpaceCount=0;
                    //					}
                } while (FlagCount == true);
                sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
                sw.WriteLine(info4, "", "Packs", "Ltrs", "GROSS AMOUNT         : ", tmpGrandTotal.Value);
                sw.WriteLine(info4, "", "----------", "----------", "FREE/SCH DISC        : ", "-" + Request.Form["txtschemetotal"].ToString());
                sw.WriteLine(info4, "", "", "", "Sec./Sp. Disc        : ", "-" + txtSecondrySpDisc.Text);//add
                var fleetoe = Request.Form["txtfleetoediscountRs"].ToString();
                if (string.IsNullOrEmpty(fleetoe))
                    sw.WriteLine(info4, "Act Qty", TotalQtyPack.ToString(), txtliter.Text, "", "");
                else
                    sw.WriteLine(info4, "Act Qty", TotalQtyPack.ToString(), txtliter.Text, "Oe/Fleet Discount    : ", "-" + Request.Form["txtfleetoediscountRs"].ToString());
                if (txtDisc.Text == "" || txtDisc.Text == "0")
                    sw.WriteLine(info4, "", "", "", "Discount             : ", "0");
                else
                {
                    if (DropDiscType.SelectedItem.Text.Equals("%"))
                        sw.WriteLine(info4, "", "", "", "Discount(" + txtDisc.Text + DropDiscType.SelectedItem.Text + ")      : ", "-" + tempdiscount.Value);
                    else
                        sw.WriteLine(info4, "", "", "", "Discount(" + DropDiscType.SelectedItem.Text + ")        : ", "-" + tempdiscount.Value);
                }
                if (txtCashDisc.Text == "" || txtCashDisc.Text == "0")
                    sw.WriteLine(info4, "Free Qty", TotalQtyfoe.ToString(), TotalfoeLtr.ToString(), "Cash Discount        : ", "0");
                else
                {
                    if (DropCashDiscType.SelectedItem.Text.Equals("%"))
                        sw.WriteLine(info4, "Free Qty", TotalQtyfoe.ToString(), TotalfoeLtr.ToString(), "Cash Discount(" + txtCashDisc.Text + DropCashDiscType.SelectedItem.Text + ") : ", "-" + tempcashdis.Value);
                    else
                        sw.WriteLine(info4, "Free Qty", TotalQtyfoe.ToString(), TotalfoeLtr.ToString(), "Cash Discount(" + DropCashDiscType.SelectedItem.Text + ")   : ", "-" + tempcashdis.Value);

                }
                sw.WriteLine(info4, "", "----------", "----------", "Total CGST          : ", tempTotalCgst.Value);
                sw.WriteLine(info4, "", "", "", "Total SGST          : ", tempTotalSgst.Value);
                sw.WriteLine(info4, "", "", "", "Total IGST          : ", tempTotalIgst.Value);

                Double litre;
                if (string.IsNullOrEmpty(txtliter.Text))
                    litre = 0;
                else
                    litre = System.Convert.ToDouble(txtliter.Text);

                var lit = litre + TotalfoeLtr;
                string litres = Convert.ToString(lit);
                sw.WriteLine(info4, "Total Qty", Convert.ToString(TotalQtyfoe + TotalQtyPack), litres, "Net Amount           : ", tmpNetAmount.Value);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine(info71, "", txtRemark.Text);
                sw.Close();

                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:reportmaking4() user " + uid);
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:reportmaking4().  EXCEPTION: " + ex.Message + "  user " + uid);
            }
        }


        /// This method is used to trim the length.
        public string trimProduct(string str)
        {
            if (str.Length > 15)
                return str.Substring(0, 15);
            else
                return str;
        }

        /// This method read the pre print template and sets the  values in global variables.
        public void getTemplateDetails()
        {
            string home_drive = Environment.SystemDirectory;
            home_drive = home_drive.Substring(0, 2);
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
            Header1Height = float.Parse(data[0].ToString().Trim());
            Header2Height = float.Parse(data[1].ToString().Trim());
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

        /// Its calls the PrePrintReport() fucntion to print Performa invoice.
        protected void Button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                PrePrintReport();
                string home_drive = Environment.SystemDirectory;
                home_drive = home_drive.Substring(0, 2);
                print(home_drive + "\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PerformaInvoiceReport.txt");
                Clear();
                clear1();
                Button1.Enabled = true;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:PerformaInvoice.aspx,Method:Button1_Click  EXCEPTION :  " + ex.Message + "   " + uid);
            }
        }
    }
}
