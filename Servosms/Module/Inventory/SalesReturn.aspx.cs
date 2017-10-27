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
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using DBOperations; 
  
  
namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for SalesReturn.
	/// </summary>
	public partial class SalesReturn : System.Web.UI.Page
	{
	    DBUtil dbobj = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid= "";
		static string FromDate="",ToDate="";

        string strInvoiceFromDate = string.Empty;
        string strInvoiceToDate = string.Empty;

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
                if (hidInvoiceFromDate.Value != "" && hidInvoiceToDate.Value != "")
                {
                    getInvoiceNo();
                }

                uid =(Session["User_Name"].ToString());
				txtMessage.Text =(Session["Message"].ToString());
				txtVatRate.Value  = (Session["VAT_Rate"].ToString()); 
				
				//lblEntryBy.Text = uid;
				//lblEntryTime.Text = DateTime.Now.ToString (); 
				if(!Page.IsPostBack)
				{
					#region Fetch the From and To Date From OrganisationDatail
					SqlDataReader rdr=null;
					dbobj.SelectQuery("select * from organisation",ref rdr);
					if(rdr.Read())
					{
						FromDate=GetYear(GenUtil.trimDate(rdr["Acc_date_from"].ToString()));
						ToDate=GetYear(GenUtil.trimDate(rdr["Acc_date_To"].ToString()));
					}
					#endregion
					checkPrevileges();
                    PriceUpdation();
                    //getInvoiceNo();
                    //getscheme();
                    GetFOECust();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:page_load  EXCEPTION: "+ ex.Message+"  User: "+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}

		// Its Cheks the users privilegs.
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="4";
			string SubModule="6";
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
			if(View_flag =="0")
			{
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}

			if(Add_Flag=="0")
			{
				btnSave.Enabled = false; 
			}
			#endregion				
		}

		/// <summary>
		/// Its fetches all the invoie nos from sales_master except those r  present in Sales_return_master and fills the Sales Invoice No. drop down..
		/// </summary>
		public void getInvoiceNo()
		{
			try
			{
                strInvoiceFromDate = hidInvoiceFromDate.Value;
                strInvoiceToDate = hidInvoiceToDate.Value;
                SqlDataReader SqlDtr = null;
				dropInvoiceNo.Items.Clear();
				dropInvoiceNo.Items.Add("Select");  
				dbobj.SelectQuery("Select SUBSTRING(CAST(sm.Invoice_No AS varchar), 4, 9) as Invoice_No from Sales_Master sm where cast(floor(cast(Invoice_Date as float)) as datetime) >= '" + GenUtil.str2MMDDYYYY(strInvoiceFromDate) + "' and cast(floor(cast(Invoice_Date as float)) as datetime) <= '" + GenUtil.str2MMDDYYYY(strInvoiceToDate) + "' and sm.Invoice_No not in (Select Invoice_No from Sales_Return_Master)", ref SqlDtr);
				while(SqlDtr.Read())
				{
					dropInvoiceNo.Items.Add(SqlDtr["Invoice_No"].ToString());   
				}
				SqlDtr.Close();
                hidInvoiceFromDate.Value = "";
                hidInvoiceToDate.Value = "";
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:getInvoiceNo()  EXCEPTION: "+ ex.Message+"  User: "+uid);	
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
        /// This event occurres after selecting the invoice no. its fetches the invoice details and display on a screen .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropInvoiceNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			TextSelect.Text=dropInvoiceNo.SelectedItem.Value.ToString();
			try
			{
				Clear1();
				if(TextSelect.Text=="Select")
				{
					clear();
					MessageBox.Show("Please Select Invoice No");
				}
				else
				{
					HtmlInputText[] Name={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12}; 
					//HtmlInputText[] Type={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
					TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
					TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
					TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12}; 			
					TextBox[]  tempQty = {txtTempQty1, txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12}; 
					HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12};
                    HtmlInputHidden[] tmpProdCode = { tempProdCode1, tempProdCode2, tempProdCode3, tempProdCode4, tempProdCode5, tempProdCode6, tempProdCode7, tempProdCode8, tempProdCode9, tempProdCode10, tempProdCode11, tempProdCode12 };
                    HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12};
                    TextBox[]  Schreturn = {txtsch1, txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12}; 
					TextBox[]  SchFOC = {txtfoc1, txtfoc2,txtfoc3,txtfoc4,txtfoc5,txtfoc6,txtfoc7,txtfoc8,txtfoc9,txtfoc10,txtfoc11,txtfoc12}; 
					HtmlInputText[]  SchName = {txtProdsch1, txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12}; 
					//TextBox[]  SchPack = {txtPacksch1, txtPacksch2,txtPacksch3,txtPacksch4,txtPacksch5,txtPacksch6,txtPacksch7,txtPacksch8,txtPacksch9,txtPacksch10,txtPacksch11,txtPacksch12}; 
					TextBox[]  SchQty = {txtQtysch1, txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12}; 

					InventoryClass  obj=new InventoryClass();
					SqlDataReader SqlDtr,rdr=null;
					string sql = "";
					int i=0;//,j=0;
					#region Get Data from Sales Master Table regarding Invoice No.
					//sql="select * from Sales_Master where Invoice_No='"+ dropInvoiceNo.SelectedItem.Value +"'" ;
					if(FromDate!="")
						sql="select * from Sales_Master where Invoice_No='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value +"'" ;
					else
					{
						MessageBox.Show("Please Fill The Organisation Form First");
						return;
					}
					SqlDtr=obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
						string strDate = SqlDtr.GetValue(1).ToString().Trim();
						int pos = strDate.IndexOf(" ");
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}
						lblInvoiceDate.Text =GenUtil.str2DDMMYYYY(strDate);  
						lblSalesType.Value = SqlDtr.GetValue(2).ToString() ;
						lblVehicleNo.Value = SqlDtr.GetValue(5).ToString();
						txtGrandTotal.Text=SqlDtr.GetValue(6).ToString();
						txtGrandTotal.Text = GenUtil.strNumericFormat(txtGrandTotal.Text.ToString()); 
						tmpGrandTotal.Value = txtGrandTotal.Text; 
						txtDisc.Text=SqlDtr.GetValue(7).ToString(); 
						txtDisc.Text = GenUtil.strNumericFormat(txtDisc.Text.ToString()); 
						tmpDisc.Value = txtDisc.Text;
						txtDiscType.Text = SqlDtr.GetValue(8).ToString(); 
						if(txtDiscType.Text =="Per")
							txtDiscType.Text = "%"; 
						txtNetAmount.Text =SqlDtr.GetValue(9).ToString(); 
						txtNetAmount.Text = GenUtil.strNumericFormat(txtNetAmount.Text.ToString());
						tmpNetAmount.Value = txtNetAmount.Text;
						txtPromoScheme.Text= SqlDtr.GetValue(10).ToString(); 
						txtRemark.Text=SqlDtr.GetValue(11).ToString();  
						txtSlipNo.Text= SqlDtr.GetValue(14).ToString(); 
						if(txtSlipNo.Text == "0")
							txtSlipNo.Text = ""; 
						txtCashDisc.Text=SqlDtr.GetValue(15).ToString(); 
						txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString()); 
						tmpCashDisc.Value = txtCashDisc.Text;
						txtCashDiscType.Text = SqlDtr.GetValue(16).ToString(); 
						if(txtCashDiscType.Text == "Per")
							txtCashDiscType.Text = "%";
						txtVAT.Text =  SqlDtr.GetValue(17).ToString();
                        Textcgst.Text = SqlDtr["CGST_Amount"].ToString();
                        Textsgst.Text = SqlDtr["SGST_Amount"].ToString();
                        txtschemetotal.Text=SqlDtr.GetValue(18).ToString();
						txtfleetoedis.Text=SqlDtr.GetValue(19).ToString();
						txtlitertotal.Text=SqlDtr["totalqtyltr"].ToString();
						tmpVatAmount.Value = txtVAT.Text;
						if(txtVAT.Text.Trim() == "0")
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
					SqlDtr.Close();
					#endregion
		
					#region Get Customer name and place regarding Customer ID
					sql="select Cust_Name, City,CR_Days,Op_Balance,CR_Limit from Customer as c, sales_master as s where c.Cust_ID= s.Cust_ID and s.Invoice_No='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedValue +"'";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						lblCustName.Value = SqlDtr.GetValue(0).ToString();
						lblPlace.Value=SqlDtr.GetValue(1).ToString();
						DateTime duedate=DateTime.Now.AddDays(System.Convert.ToDouble(SqlDtr.GetValue(2).ToString()));
						string duedatestr=(duedate.ToShortDateString());
						lblDueDate.Value =GenUtil.str2DDMMYYYY(duedatestr);
						TxtCrLimit.Value = SqlDtr.GetValue(4).ToString();
					}
					SqlDtr.Close();
				
					#endregion
					#region Get Data from Sales Details Table regarding Invoice No.
					//sql="select	p.Category,p.Prod_Name,p.Pack_Type,	sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1"+
					//	" from Products p, sales_Details sd"+
					//	" where p.Prod_ID=sd.prod_id and sd.invoice_no='"+ dropInvoiceNo.SelectedItem.Value +"'" ;
					sql= "select p.Category,p.Prod_Name,p.Pack_Type,sd.qty,sd.rate,sd.amount,p.Prod_ID,p.unit,sd.scheme1,sd.foe,p.Prod_Code" +
						" from Products p, sales_Details sd"+
						" where p.Prod_ID=sd.prod_id and sd.Rate >0 and sd.Amount > 0 and sd.invoice_no='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sno" ;
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{

                        //Qty[i].Enabled = true;
                        tmpProdCode[i].Value = SqlDtr["Prod_Code"].ToString();
                        Name[i].Value=SqlDtr.GetValue(1).ToString()+":"+SqlDtr.GetValue(2).ToString();
						//Type[i].Value=SqlDtr.GetValue(2).ToString();   
						Qty[i].Text=SqlDtr.GetValue(3).ToString();
						tempQty[i].Text   = Qty[i].Text ;
						tmpQty[i].Value =  SqlDtr.GetValue(3).ToString();  
						Rate[i].Text=SqlDtr.GetValue(4).ToString();
						Amount[i].Text=SqlDtr.GetValue(5).ToString();
						Schreturn[i].Text = SqlDtr.GetValue(8).ToString();
						SchFOC[i].Text = SqlDtr.GetValue(9).ToString();
						Check[i].Checked = false;
						//string sql11="select schprodid from oilscheme where Prodid='"+SqlDtr.GetValue(6).ToString()+"' and discount=0 and cast(floor(cast(datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text)+"' and cast(floor(cast(dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(lblInvoiceDate.Text) +"'";
						//dbobj.SelectQuery(sql11,ref rdr1);
						//if(rdr1.Read())
						//{
						string sql11="select p.Prod_Name,p.Pack_Type,sd.qty,p.Prod_ID"+
								" from Products p, sales_Details sd"+
								//" where p.Prod_ID=sd.prod_id and sd.Rate =0 and sd.Amount = 0 and sd.invoice_no='"+ dropInvoiceNo.SelectedItem.Value +"'" ;
								" where p.Prod_ID=sd.Prod_ID and sd.Rate =0 and sd.Amount = 0 and sno='"+i+"' and sd.invoice_no='"+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value +"' order by sno" ;
							dbobj.SelectQuery(sql11,ref rdr);
							if(rdr.Read())
							{
								SchName[i].Value = rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString();
								//SchPack[i].Text = rdr.GetValue(1).ToString();
								SchQty[i].Text = rdr.GetValue(2).ToString();
							}
							i++;
						//}
						//else
                        //    i++;
					}
					SqlDtr.Close();

					while(i<12)
					{
					
                        Name[i].Value = "";  
                        //Type[i].Value = "";  
						Qty[i].Text="";
						Qty[i].Enabled = false; 
						tempQty[i].Text ="";
						tmpQty[i].Value = "";
						Rate[i].Text="";
						Amount[i].Text="";
						Schreturn[i].Text="";
						Check[i].Checked = false;
						Check[i].Disabled=true;
						i++;
					}
					//SqlDtr.Close();
					CheckAll.Checked = false;
					#endregion
					getscheme();
				}
				CreateLogFiles.ErrorLog("Form:Salesreturn.aspx,Method:dropInvoiceNo_SelectedIndexChanged " +" Sales invoice is viewed for invoice no: "+dropInvoiceNo.SelectedItem.Value.ToString()+"  userid:"+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesInvoice.aspx,Method:dropInvoiceNo_SelectedIndexChanged   EXCEPTION  "+ex.Message+"  userid: "+uid);
			}
		}
		
		/// <summary>
		/// this is used to save.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(dropInvoiceNo.SelectedIndex == 0)
				{
					MessageBox.Show("Please Select Invoice No.");
					return;  
				}
				int c = 0;
				// Its checks if any check box is selected or not , if not display the popup message.
				HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12};
				for(int i=0;i < Check.Length ; i++)
				{
					if(Check[i].Checked == false)
					{
						c++;
					}
				}
				if(c == 12)
				{
					MessageBox.Show("Please select a Product to return");
					return;
				}
				InventoryClass  obj=new InventoryClass ();
				//obj.Invoice_No = dropInvoiceNo.SelectedItem.Text.Trim();
				obj.Invoice_No = int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Text.Trim();
				int count = 0;
				// This part of code is use to solve the double click problem, Its checks the sales return no. and display the popup, that it is saved.
				dbobj.ExecuteScalar("Select count(Invoice_No) from Sales_Return_Master where Invoice_No = "+int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Text.Trim(),ref count);
				if(count > 0)
				{
					MessageBox.Show("Sales Return Saved");						
					clear(); 
					getInvoiceNo();
					return ;
				}
				obj.Customer_Name =lblCustName.Value.ToString() ;
				obj.Place =lblPlace.Value.ToString();
				obj.Grand_Total = Request.Form["txtGrandTotal"];
				if(txtDisc.Text.Trim() =="")
					obj.Discount ="0.0";
				else
					obj.Discount =txtDisc.Text;
				obj.Discount_Type=txtDiscType.Text;
				obj.Net_Amount = Request.Form["txtNetAmount"];
				obj.Entry_By =uid ;
				//obj.Entry_Time =DateTime.Parse(lblEntryTime .Text);
				//obj.Entry_Time =DateTime.Parse("1/1/1900");
				obj.Entry_Time =System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate .Text)+" "+DateTime.Now.TimeOfDay.ToString());
				if(txtCashDisc.Text.Trim() =="")
					obj.Cash_Discount  ="0.0";
				else
					obj.Cash_Discount  = txtCashDisc.Text.Trim();
				obj.Cash_Disc_Type = txtCashDiscType.Text;
				obj.VAT_Amount = Request.Form["txtVAT"];
                obj.CGST_Amount = Textcgst.Text;
                obj.SGST_Amount = Textsgst.Text;
                obj.Cr_Plus="0";
				double amount = System.Convert.ToDouble(Request.Form["txtNetAmount"]) *-1;
				obj.Dr_Plus= amount.ToString();
		        //obj.Pre_Amount = tmpNetAmount.Value;
				obj.Pre_Amount = Request.Form["txtNetAmount"];
				obj.schdiscount= Request.Form["txtschemetotal"];
				
				// Calls the InsertSalesReturnMaster fucntion which calls the ProInsertSalesReturnMaster pocedure to enter the Sales Return master details.
			obj.InsertSalesReturnMaster();
			obj.UpdateCustomerBalance();
			// Calls the Save function only for return products.
				HtmlInputText[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12};
				//HtmlInputText[] PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12};
				TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12};
				TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12};
				TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};
				TextBox[]  Quantity = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12};
				TextBox[]  scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};
				TextBox[]  foc = {txtfoc1,txtfoc2,txtfoc3,txtfoc4,txtfoc5,txtfoc6,txtfoc7,txtfoc8,txtfoc9,txtfoc10,txtfoc11,txtfoc12};
				HtmlInputText[]  SchName = {txtProdsch1, txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12}; 
				//TextBox[]  SchPack = {txtPacksch1, txtPacksch2,txtPacksch3,txtPacksch4,txtPacksch5,txtPacksch6,txtPacksch7,txtPacksch8,txtPacksch9,txtPacksch10,txtPacksch11,txtPacksch12}; 
				TextBox[]  SchQty = {txtQtysch1, txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12}; 
				string[] strName = new string[2];
				string[] strSchName = new string[2];
				for(int j=0;j<ProdName.Length ;j++)
				{
					if(Check[j].Checked == false)
						continue;
					strName = ProdName[j].Value.Split(new char[] {':'},ProdName[j].Value.Length);
					//Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString (),scheme[j].Text.Trim());
					Save(strName[0].ToString(),strName[1].ToString(),Qty[j].Text.ToString(),Rate[j].Text.ToString (), Request.Form[Amount[j].ID].ToString(), scheme[j].Text.Trim());
					UpdateBatchNo(strName[0].ToString(),strName[1].ToString(),Qty[j].Text);
					
					if((SchQty[j].Text == "" || SchQty[j].Text=="0") &&(SchName[j].Value==""))
						continue;
					strSchName = SchName[j].Value.Split(new char[] {':'},SchName[j].Value.Length);
					//Mahesh11.04.007 Save1(ProdName1[j].Text.ToString(),PackType1[j].Text.ToString(),Qty1[j].Text.ToString(),GenUtil.str2MMDDYYYY(lblInvoiceDate.Text.ToString()),scheme[j].Text.ToString ());
					//**Save1(SchName[j].Value.ToString(),SchPack[j].Text.ToString(),SchQty[j].Text.ToString(),GenUtil.str2MMDDYYYY(Session["CurrentDate"].ToString()),scheme[j].Text.Trim());
					Save1(strSchName[0].ToString(),strSchName[1].ToString(),SchQty[j].Text.ToString(),GenUtil.str2DDMMYYYY(Session["CurrentDate"].ToString()),scheme[j].Text.Trim());
					UpdateBatchNo(strSchName[0].ToString(),strSchName[1].ToString(),SchQty[j].Text);
				}
				CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:btnSave_Click()"+" Sales Return for  Invoice No."+obj.Invoice_No+" ,"+"of Customer Name  "+obj.Customer_Name+",  "+" and NetAmount  "+obj.Net_Amount+"  is Saved "+" userid "+"   "+uid);
				MessageBox.Show("Sales Return Saved");
                //reportmaking();
				PrePrintReport();
				//print();
				clear();
				getInvoiceNo();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:btnSave_Click  EXCEPTION: "+ ex.Message+"  User: "+uid);
			}
		}

		/// <summary>
		/// its Calls the InsertSalesReturnDetails fucntion which calls the ProInsertSalesReturn procedure to enter the return details of a product.
		/// </summary>
		/// <param name="ProdName"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		/// <param name="Rate"></param>
		/// <param name="Amount"></param>
		/// <param name="scheme1"></param>
		public void Save(string ProdName,string PackType, string Qty, string Rate,string Amount,string scheme1)
		{
			InventoryClass obj=new InventoryClass();
			obj.Product_Name=ProdName;
			obj.Package_Type=PackType;
			obj.Qty=Qty;
			obj.scheme1=scheme1;
			obj.Rate=Rate;
			obj.Amount=Amount;
			//obj.Invoice_No=dropInvoiceNo.SelectedItem.Value;
			obj.Invoice_No=System.Convert.ToString(int.Parse(FromDate)+ToDate+dropInvoiceNo.SelectedItem.Value);
	    	obj.InsertSalesReturnDetail();
		}

		/// <summary>
		/// its Calls the InsertSalesReturnDetails fucntion which calls the ProInsertSalesReturn procedure to enter the return details of a product.
		/// </summary>
		/// <param name="ProdName"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		/// <param name="Invoice_date"></param>
		/// <param name="scheme"></param>
		public void Save1(string ProdName,string PackType, string Qty,string Invoice_date,string scheme)
		{
			InventoryClass obj=new InventoryClass();
			obj.Product_Name=ProdName ;
			obj.Package_Type=PackType ;
			obj.scheme1=scheme;
			if(Qty.Equals(""))
				obj.Qty="0";
			else
				obj.Qty=Qty;

			//obj.sch=scheme;
			obj.Inv_date = Invoice_date;

			//if(dropInvoiceNo.Visible==true)
			//{
			//	obj.Invoice_No=lblInvoiceNo.Text;
			//	obj.InsertSaleReturnSchemeDetail();
			//}
			//else
			//{
				obj.Invoice_No=dropInvoiceNo.SelectedItem.Value;
				obj.InsertSaleReturnSchemeDetail(); 
			//}
		}

		/// <summary>
		/// Its clears the web form.
		/// </summary>
		public void clear()
		{
			dropInvoiceNo.SelectedIndex = 0;
			lblInvoiceDate.Text  = "";
			lblSalesType.Value = "";
			txtSlipNo.Text = "";
			lblCustName.Value  = "";
			lblPlace.Value  = "";
			lblDueDate.Value = "";
			lblVehicleNo.Value = "";
			txtPromoScheme.Text = "";
			txtRemark.Text = "";
			txtGrandTotal.Text = "";
			txtCashDisc.Text = "";
			txtCashDiscType.Text = "";
			txtVAT.Text = "";
            Textcgst.Text = "";
            Textsgst.Text = "";
			Yes.Checked = false;
			No.Checked = true;
			txtDisc.Text = "";
			txtDiscType.Text = "";
			txtNetAmount.Text = "";
			tmpNetAmount.Value ="";
			tmpGrandTotal.Value = "";
			tmpCashDisc.Value = "";
			tmpDisc.Value = "";
			tmpVatAmount.Value = "";
            txtschemetotal.Text="";
			txtlitertotal.Text="";
			txtfleetoedis.Text="";
			HtmlInputText[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12}; 
			//HtmlInputText[] PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
			TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
			TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
			TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};
			TextBox[]  Quantity = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12};
			HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12};
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12};
			TextBox[]  Schreturn = {txtsch1, txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12}; 
			TextBox[]  SchFOC = {txtfoc1, txtfoc2,txtfoc3,txtfoc4,txtfoc5,txtfoc6,txtfoc7,txtfoc8,txtfoc9,txtfoc10,txtfoc11,txtfoc12}; 
			HtmlInputText[]  SchName = {txtProdsch1, txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12}; 
			//TextBox[]  SchPack = {txtPacksch1, txtPacksch2,txtPacksch3,txtPacksch4,txtPacksch5,txtPacksch6,txtPacksch7,txtPacksch8,txtPacksch9,txtPacksch10,txtPacksch11,txtPacksch12}; 
			TextBox[]  SchQty = {txtQtysch1, txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12}; 
			for(int j=0;j<ProdName.Length ;j++)
			{
				ProdName[j].Value = "";
				//PackType[j].Value = "";
				Qty[j].Text = "";
				Rate[j].Text = "";
				Amount[j].Text = "";
				Quantity[j].Text = "";
				tmpQty[j].Value = ""; 
				Check[j].Checked = false;
				Check[j].Disabled = false;
				Schreturn[j].Text="";
				SchFOC[j].Text="";
				SchName[j].Value="";
				//SchPack[j].Text="";
				SchQty[j].Text="";
			}
				CheckAll.Checked = false;
		}

		/// <summary>
		/// This method is used to clear the form.
		/// </summary>
		public void Clear1()
		{
			TextBox[]  Schreturn = {txtsch1, txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12}; 
			TextBox[]  SchFOC = {txtfoc1, txtfoc2,txtfoc3,txtfoc4,txtfoc5,txtfoc6,txtfoc7,txtfoc8,txtfoc9,txtfoc10,txtfoc11,txtfoc12}; 
			HtmlInputText[]  SchName = {txtProdsch1, txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12}; 
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12};
			//TextBox[]  SchPack = {txtPacksch1, txtPacksch2,txtPacksch3,txtPacksch4,txtPacksch5,txtPacksch6,txtPacksch7,txtPacksch8,txtPacksch9,txtPacksch10,txtPacksch11,txtPacksch12}; 
			TextBox[]  SchQty = {txtQtysch1, txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12}; 
			for(int j=0;j<SchName.Length ;j++)
			{
				SchName[j].Value="";
				//SchPack[j].Text="";
				SchQty[j].Text="";
				SchFOC[j].Text="";
				Schreturn[j].Text="";
				Check[j].Disabled=false;
			}
		}
		// Its fetch the screen values and writes the details in SalesReturn.txt file to print but the details of only the Return products not others.
		/*public void reportmaking()
		{
			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesReturnReport.txt";
				string info = "";
				string strInvNo="";
				string strDiscType="";
				double disc_amt=0;
				string msg ="";
				HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12};
				HtmlInputText[]  ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12}; 
				//HtmlInputText[]  PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
				TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
				TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12};
				TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};
				TextBox[]  SchFOC = {txtfoc1, txtfoc2,txtfoc3,txtfoc4,txtfoc5,txtfoc6,txtfoc7,txtfoc8,txtfoc9,txtfoc10,txtfoc11,txtfoc12};
				HtmlInputText[]  SchName = {txtProdsch1, txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12}; 
				//TextBox[]  SchPack = {txtPacksch1, txtPacksch2,txtPacksch3,txtPacksch4,txtPacksch5,txtPacksch6,txtPacksch7,txtPacksch8,txtPacksch9,txtPacksch10,txtPacksch11,txtPacksch12}; 
				TextBox[]  SchQty = {txtQtysch1, txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12}; 
				string[] products = {"","","","","","","","","","","",""};
				string[] qty = {"","","","","","","","","","","",""};
				string[] rate = {"","","","","","","","","","","",""};
				string[] amt = {"","","","","","","","","","","",""};
				int j = 0;
				for(int i = 0; i < Check.Length ; i++)
				{
					if(Check[i].Checked)
					{
						//products[j] = ProdName[i].Value +" "+PackType[i].Value ;
						products[j] = ProdName[i].Value;
						qty[j] = Qty[i].Text;
						rate[j] = Rate[i].Text;
						amt[j] = Amount[i].Text;
						j++;
					}
				}

				StreamWriter sw = new StreamWriter(path);
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
				string des="------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==============",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("SALES RETURN",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==============",des.Length));
				strInvNo= dropInvoiceNo.SelectedItem.Value;   
 
				sw.WriteLine(" Invoice No : " + strInvNo+ "                              Date : " + lblInvoiceDate.Text.ToString());
				sw.WriteLine(" Customer Name     : " + lblCustName.Value);
				sw.WriteLine(" Place             :  "+lblPlace.Value);
				sw.WriteLine(" Due Date          :  "+lblDueDate.Value);
				sw.WriteLine(" Vehicle Number    :  "+lblVehicleNo.Value);
				sw.WriteLine("+------------------------------+-----------+----------+----------+");
				sw.WriteLine("|Product                       | Quantity  |   Rate   |  Amount  |");
				sw.WriteLine("+------------------------------+-----------+----------+----------+");
				info = " {0,-30:S} {1,10:F}  {2,10:F} {3,10:F}";
				// Writes the details of return products i.e. up to 8 products if less then display the sapces.
				sw.WriteLine(info,products[0]  ,qty[0],rate[0],GenUtil.strNumericFormat(amt[0]));
				sw.WriteLine(info,products[1]  ,qty[1],rate[1],GenUtil.strNumericFormat(amt[1]));
				sw.WriteLine(info,products[2]  ,qty[2],rate[2],GenUtil.strNumericFormat(amt[2]));
				sw.WriteLine(info,products[3]  ,qty[3],rate[3],GenUtil.strNumericFormat(amt[3]));
				sw.WriteLine(info,products[4]  ,qty[4],rate[4],GenUtil.strNumericFormat(amt[4]));
				sw.WriteLine(info,products[5]  ,qty[5],rate[5],GenUtil.strNumericFormat(amt[5]));
				sw.WriteLine(info,products[6]  ,qty[6],rate[6],GenUtil.strNumericFormat(amt[6]));
				sw.WriteLine(info,products[7]  ,qty[7],rate[7],GenUtil.strNumericFormat(amt[7]));
				sw.WriteLine(info,products[8]  ,qty[8],rate[8],GenUtil.strNumericFormat(amt[8]));
				sw.WriteLine(info,products[9]  ,qty[9],rate[9],GenUtil.strNumericFormat(amt[9]));
				sw.WriteLine(info,products[10]  ,qty[10],rate[10],GenUtil.strNumericFormat(amt[10]));
				sw.WriteLine(info,products[11]  ,qty[11],rate[11],GenUtil.strNumericFormat(amt[11]));
				sw.WriteLine("+------------------------------+-----------+----------+----------+");
				sw.WriteLine("                               Grand Total           : {0,10:F}" , GenUtil.strNumericFormat(txtGrandTotal.Text.ToString() ));
				disc_amt=0;
				msg ="";
				if(txtCashDisc .Text=="")
				{
					strDiscType="";
					msg = "";
				}
				else
				{
					disc_amt = System.Convert.ToDouble(txtCashDisc.Text.ToString()); 
					strDiscType= txtCashDiscType.Text ;
					if(strDiscType.Trim().Equals("%"))
					{
						double temp =0;
						if(txtGrandTotal.Text.Trim() != "")
							temp = System.Convert.ToDouble(txtGrandTotal.Text.Trim().ToString());
 
						disc_amt  = (temp*disc_amt/100);
						msg = "("+txtCashDisc.Text.ToString()+strDiscType+")";
					}
					else
					{
						msg ="("+strDiscType+")";
					}			
				}
				sw.WriteLine("                               Cash Discount{0,-8:S} : {1,10:F}" ,msg,GenUtil.strNumericFormat(disc_amt.ToString()));
				string Vat_Rate = "";
				string amount = "0";
				if(Yes.Checked)
				{
					Vat_Rate = "("+Session["VAT_Rate"].ToString()+"%)";
					amount = txtVAT.Text.Trim();  
				}
				sw.WriteLine("                               VAT          {0,-8:S} : {1,10:F}" ,Vat_Rate,GenUtil.strNumericFormat(amount));
				disc_amt=0;
				msg ="";
				if(txtDisc.Text=="")
				{
					strDiscType="";
					msg = "";
				}
				else
				{
					disc_amt = System.Convert.ToDouble(txtDisc.Text.ToString()); 
					strDiscType= txtDiscType.Text ;
					if(strDiscType.Trim().Equals("%"))
					{
						double temp =0;
						if(txtGrandTotal.Text.Trim() != "")
							temp = System.Convert.ToDouble(txtGrandTotal.Text.Trim().ToString());
 
						disc_amt  = (temp*disc_amt/100);
						msg = "("+txtDisc.Text.ToString()+strDiscType+")";
					}
					else
					{
						msg ="("+strDiscType+")";
					}			
				}
				//********
				disc_amt=0;
				msg ="";
				if(txtschemetotal.Text=="")
				{
					strDiscType="";
				}
				else
				{
					disc_amt = System.Convert.ToDouble(txtschemetotal.Text.ToString()); 
				}
				sw.WriteLine("                               Scheme Discount       : {0,10:F}" , GenUtil.strNumericFormat(txtschemetotal.Text.ToString()));
				//*********
				sw.WriteLine("                               Discount     {0,-8:S} : {1,10:F}" ,msg,GenUtil.strNumericFormat(disc_amt.ToString()));
				sw.WriteLine("                               Net Amount            : {0,10:F}" , GenUtil.strNumericFormat(txtNetAmount.Text.ToString()));
				sw.WriteLine("+----------------------------------------------------------------+");
				sw.WriteLine("Promo Scheme : " + txtPromoScheme.Text);
				sw.WriteLine("Remarks      : " + txtRemark.Text);
				sw.WriteLine("Message      : " + txtMessage.Text);
				sw.WriteLine("");
				sw.WriteLine("");
				sw.WriteLine("");
				sw.WriteLine("                                               Signature");
				sw.Close();	
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:reportmaking().  EXCEPTION: "+ ex.Message+"  User: "+uid);	
			}
		}*/

		/// <summary>
		/// This Method to write into the report file to print.
		/// </summary>
		public void PrePrintReport()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
            string path = home_drive + @"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesReturnPrePrintReport.txt";
            //string path = @"E:\ServoSMS\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesReturnPrePrintReport.txt";
            StreamWriter sw = new StreamWriter(path);
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12};
			HtmlInputText[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12}; 
			TextBox[] foe = {txtfoc1,txtfoc2,txtfoc3,txtfoc4,txtfoc5,txtfoc6,txtfoc7,txtfoc8,txtfoc9,txtfoc10,txtfoc11,txtfoc12};
			TextBox[] Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12}; 
			TextBox[] Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12}; 
			TextBox[] Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12};			
			TextBox[] scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};	
			HtmlInputText[] ProdSchName = {txtProdsch1,txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12};	
			TextBox[] schQty={txtQtysch1,txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12};
            HtmlInputHidden[] SecSP = { txtTempSecSP1, txtTempSecSP2, txtTempSecSP3, txtTempSecSP4, txtTempSecSP5, txtTempSecSP6, txtTempSecSP7, txtTempSecSP8, txtTempSecSP9, txtTempSecSP10, txtTempSecSP11, txtTempSecSP12 };
            HtmlInputHidden[] tmpCgst = { tempCgst1, tempCgst2, tempCgst3, tempCgst4, tempCgst5, tempCgst6, tempCgst7, tempCgst8, tempCgst9, tempCgst10, tempCgst11, tempCgst12, tempCgst13, tempCgst14, tempCgst15, tempCgst16, tempCgst17, tempCgst18, tempCgst19, tempCgst20 };
            HtmlInputHidden[] tmpSgst = { tempSgst1, tempSgst2, tempSgst3, tempSgst4, tempSgst5, tempSgst6, tempSgst7, tempSgst8, tempSgst9, tempSgst10, tempSgst11, tempSgst12, tempSgst13, tempSgst14, tempSgst15, tempSgst16, tempSgst17, tempSgst18, tempSgst19, tempSgst20 };
            HtmlInputHidden[] tmpIgst = { tempIgst1, tempIgst2, tempIgst3, tempIgst4, tempIgst5, tempIgst6, tempIgst7, tempIgst8, tempIgst9, tempIgst10, tempIgst11, tempIgst12, tempIgst13, tempIgst14, tempIgst15, tempIgst16, tempIgst17, tempIgst18, tempIgst19, tempIgst20 };
            HtmlInputHidden[] tmpHsn = { tempHsn1, tempHsn2, tempHsn3, tempHsn4, tempHsn5, tempHsn6, tempHsn7, tempHsn8, tempHsn9, tempHsn10, tempHsn11, tempHsn12, tempHsn13, tempHsn14, tempHsn15, tempHsn16, tempHsn17, tempHsn18, tempHsn19, tempHsn20 };


            string[] DespQty=new string[12];
			string[] freeDespQty=new string[12];
			string[] ProdCode=new string[12];
			string[] schProdCode=new string[12];
			string[] schPName=new string[12];
			string[] schPPack=new string[12];
			string[] schPQty=new string[12];
			string[] PackType=new string[12];
			string[] schProdName=new string[12];

			Double TotalQtyPack=0,TotalQtyfoe=0,TotalfoeLtr=0;
			int k=0;
			string info2="",info3="",info4="",info5="",info6="",info7="",str="",InDate="";
			info2=" {0,-14:S} {1,-50:S} {2,20:S} {3,-46:S}";//Party Name & Address
			info4=" {0,-20:S} {1,20:S} {2,20:S} {3,55:S} {4,15:S}";//Party Name & Address
			info3= " {0,-10:S} {1,-19:S} {2,-35:S} {3,5:S} {4,5:S} {5,5:S} {6,10:S} {7,12:S} {8,10:S} {9,15:S} {10,12:S} {11,10:S} {12,15:S}";//Item Code
			info5=" {0,16:S} {1,-114:S}";
			info7=" {0,46:S} {1,-88:S}";
			info6=" {0,44:S} {1,-10:S} {2,-76:S}";
			//string curbal=lblCurrBalance.Value;
			//string curbal="";
			string[] CurrBal=new string[2];
			string[] InvoiceDate=new string[2];
			
			str="select invoice_date from Sales_Master where invoice_no="+FromDate+ToDate+dropInvoiceNo.SelectedItem.Text.Trim()+"";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.Read())
				InDate=SqlDtr.GetValue(0).ToString();
			else
				InDate="";
			SqlDtr.Close();
			if(InDate!="")
				InvoiceDate=InDate.Split(new char[] {' '},InDate.Length);
			else
				InvoiceDate[1]="";

			string[] arrProdType = new string[2];
			for(int p=0;p<12;p++)
			{
				if(Check[p].Checked==true)
				{
					if(ProdName[p].Value.IndexOf(":")>0)
						arrProdType=ProdName[p].Value.Split(new char[] {':'},ProdName[p].Value.Length);
					else
					{
						arrProdType[0]="";
						arrProdType[1]="";
					}
					//str="select Prod_Code from Products where Prod_Name='"+ProdName[p].Value+"' and Category='"+ProdCat[p].SelectedItem.Text+"' and Pack_Type='"+PackType[p].Value+"'";
					str="select Prod_Code,Total_Qty from Products where Prod_Name='"+arrProdType[0].ToString()+"' and Pack_Type='"+arrProdType[1].ToString()+"'";
					SqlDtr = obj.GetRecordSet(str);
					if(SqlDtr.Read())
					{
						ProdCode[p]=SqlDtr.GetValue(0).ToString();
						PackType[p]=SqlDtr.GetValue(1).ToString();
					}
					else
					{
						ProdCode[p]="";
						PackType[p]="";
					}
					SqlDtr.Close();
				}
				else
				{
					ProdCode[p]="";
					PackType[p]="";
				}
			}
			int p1=0;
			string[] arrProdSchType = new string[2];
			for(int p=0;p<12;p++)
			{
				if(Check[p].Checked==true)
				{
					if(ProdSchName[p].Value.IndexOf(":")>0)
						arrProdSchType=ProdSchName[p].Value.Split(new char[] {':'},ProdSchName[p].Value.Length);
					else
					{
						arrProdSchType[0]="";
						arrProdSchType[1]="";
					}
					//str="select Prod_Code from Products where Prod_Name='"+schProdName[p].Text+"' and Category='"+schProdType[p].Text+"' and Pack_Type='"+schProdPack[p].Text+"'";
					str="select Prod_Code from Products where Prod_Name='"+arrProdSchType[0].ToString()+"' and Pack_Type='"+arrProdSchType[1].ToString()+"'";
					SqlDtr = obj.GetRecordSet(str);
					if(SqlDtr.Read())
					{
						schProdCode[p1]=SqlDtr.GetValue(0).ToString();
						p1++;
					}
					SqlDtr.Close();
				}
				
			}
			int jj1=0;
			string[] arrProdSchType1 = new string[2];
			for(int jj=0;jj<12;jj++)
			{
				if(Check[jj].Checked==true)
				{
					if(ProdSchName[jj].Value.IndexOf(":")>0)
						arrProdSchType1=ProdSchName[jj].Value.Split(new char[] {':'},ProdSchName[jj].Value.Length);
					else
					{
						arrProdSchType1[0]="";
						arrProdSchType1[1]="";
					}
					if(!arrProdSchType1[0].ToString().Equals("") && !schQty[jj].Text.Equals(""))
					{
						schPName[jj1]="(FREE) "+ProdSchName[jj].Value;
						schPQty[jj1]=schQty[jj].Text;
						schProdName[jj1]=ProdSchName[jj].Value;
						schPPack[jj1]=arrProdSchType1[1].ToString();
						jj1++;
					}
				}
				
			}
			for(int jj=jj1;jj<12;jj++)
			{
				schPQty[jj]="";
			}
			for(int j=0;j<12;j++)
			{
				if(Check[j].Checked==true)
				{
					if(!Qty[j].Text.Equals(""))
					{
						TotalQtyPack=TotalQtyPack+System.Convert.ToDouble(Qty[j].Text);
						DespQty[j]=Qty[j].Text;
					}
					else
						DespQty[j]="";
					if(!schQty[j].Text.Equals(""))
					{
						TotalQtyfoe=TotalQtyfoe+System.Convert.ToDouble(schQty[j].Text);
						freeDespQty[j]=schQty[j].Text;
					}
					else
						freeDespQty[j]="";
				}
				else
				{
					DespQty[j]="";
					freeDespQty[j]="";
				}
			}
			string[] arrProdSchType2 = new string[2];
			for(int i=0;i<12;i++)
			{
				if(Check[i].Checked==true)
				{
					if(ProdSchName[i].Value.IndexOf(":")>0)
						arrProdSchType2=ProdSchName[i].Value.Split(new char[] {':'},ProdSchName[i].Value.Length);
					else
					{
						arrProdSchType2[0]="";
						arrProdSchType2[1]="";
					}
					//if(schProdPack[i].text != "" && schQty[i].Text != "")
					if(arrProdSchType2[1].ToString() != "" && schQty[i].Text != "")
					{
						TotalfoeLtr=TotalfoeLtr+System.Convert.ToDouble(GenUtil.changeqtyltr(arrProdSchType2[1].ToString(),int.Parse(schQty[i].Text)));
					}
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
			
			//int q=0;
			for(int p=0;p<=Qty.Length-1;p++)
			{
				if(Check[p].Checked==true)
				{
					if(Qty[p].Text!="")
					{
						string[] arrProdCat = ProdName[p].Value.Split(new char[] {':'},ProdName[p].Value.Length);
						str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+ProdCode[p].ToString()+"' and Prod_Name='"+arrProdCat[0].ToString()+"' and Pack_Type='"+arrProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and bt.trans_type='Sales Return'";
						SqlDtr = obj.GetRecordSet(str);
						if(SqlDtr.HasRows)
						{
							while(SqlDtr.Read())
							{
								arrProdCode.Add(ProdCode[p].ToString());
								arrProdName.Add(ProdName[p].Value);
								arrBatchNo.Add(SqlDtr.GetValue(0).ToString());
								arrBillQty.Add(SqlDtr.GetValue(1).ToString());
								arrDespQty.Add(SqlDtr.GetValue(1).ToString());
								arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString())*double.Parse(SqlDtr.GetValue(1).ToString())));
								arrProdRate.Add(Rate[p].Text);
								arrProdScheme.Add(scheme[p].Text);
								arrProdAmount.Add(System.Convert.ToString(double.Parse(SqlDtr.GetValue(1).ToString())*double.Parse(Rate[p].Text)));
								arrFreeQty.Add("");
							}
						}
						else
						{
							arrProdCode.Add(ProdCode[p].ToString());
							arrProdName.Add(ProdName[p].Value);
							arrBatchNo.Add("");
							arrBillQty.Add(Qty[p].Text);
							arrDespQty.Add(DespQty[p].ToString());
							arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString())*double.Parse(Qty[p].Text)));
							arrProdRate.Add(Rate[p].Text);
							arrProdScheme.Add(scheme[p].Text);
							arrProdAmount.Add(Amount[p].Text);
							arrFreeQty.Add("");
						}
						SqlDtr.Close();
					}
				}
			}
			for(int p=0;p<=schPQty.Length-1;p++)
			{
				string s=schPQty[p].ToString();
				if(Check[p].Checked==true)
				{
					if(schPQty[p].ToString()!="")
					{
						//string[] arrschProdCat = schProdType[p].Text.Split(new char[] {':'},schProdType[p].Text.Length);
						string[] arrschProdCat = ProdSchName[p].Value.Split(new char[] {':'},ProdSchName[p].Value.Length);
						str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+schProdCode[p].ToString()+"' and Prod_Name='"+arrschProdCat[0].ToString()+"' and Pack_Type='"+arrschProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and bt.trans_type='Sales Return'";
						SqlDtr = obj.GetRecordSet(str);
						double totalqty=0;
						if(SqlDtr.HasRows)
						{
							while(SqlDtr.Read())
							{
								arrProdCode.Add(schProdCode[p].ToString());
								arrProdName.Add("(FREE) "+ProdSchName[p].Value);
								arrBatchNo.Add(SqlDtr.GetValue(0).ToString());
								arrBillQty.Add("");
								//arrLtrkg.Add(System.Convert.ToString(double.Parse(arrschProdCat[1].ToString())*double.Parse(arrschBillQty[p].ToString())));
								if(SqlDtr.GetValue(1).ToString()=="" || SqlDtr.GetValue(1).ToString()==null)
									totalqty=0;
								else
									totalqty=double.Parse(SqlDtr.GetValue(1).ToString());
								arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(),int.Parse(totalqty.ToString())));
								arrDespQty.Add(SqlDtr.GetValue(1).ToString());
								arrProdRate.Add("");
								arrProdScheme.Add("");
								arrProdAmount.Add("");
								arrFreeQty.Add(SqlDtr.GetValue(1).ToString());
							}
						}
						else
						{
							arrProdCode.Add(schProdCode[p].ToString());
							arrProdName.Add("(FREE) "+ProdSchName[p].Value);
							arrBatchNo.Add("");
							//arrBillQty.Add(schPQty[p].ToString());
							arrBillQty.Add("");
							if(schPQty[p].ToString()=="" || schPQty[p].ToString()==null)
								totalqty=0;
							else
								totalqty=double.Parse(schPQty[p].ToString());
							//arrLtrkg.Add(System.Convert.ToString(double.Parse(arrschProdCat[1].ToString())*double.Parse(arrschBillQty[p].ToString())));
							arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(),int.Parse(totalqty.ToString())));
							arrDespQty.Add(schPQty[p].ToString());
							arrProdRate.Add("");
							arrProdScheme.Add("");
							arrProdAmount.Add("");
							arrFreeQty.Add(schPQty[p].ToString());
						}
						SqlDtr.Close();
					}
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
			int arrCount=0;//,arrCon=0;
			double Space=0,SpaceCount=arrBillQty.Count;
			bool FlagCount=false;
			
			do 
			{
				FlagCount=false;
				sw.WriteLine("                                                          SALES RETURN");
				for(int i=0;i<14;i++)
				{
					sw.WriteLine("");
				}
				string addr="",ssc="",TinNo="";
				dbobj.SelectQuery("select * from customer where cust_name='"+lblCustName.Value+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
					addr=SqlDtr["Address"].ToString();
					ssc=SqlDtr["sadbhavnacd"].ToString();
					TinNo=SqlDtr["Tin_No"].ToString();
				}
				addr=addr.ToUpper();
				
				if(ssc!="")	
					sw.WriteLine(info2,"",lblCustName.Value.ToUpper()+"("+ssc+")","",dropInvoiceNo.SelectedItem.Text);
				else
					sw.WriteLine(info2,"",lblCustName.Value.ToUpper(),"",dropInvoiceNo.SelectedItem.Text);
				if(addr!="")
					sw.WriteLine(info2,"",GenUtil.TrimLength(addr,50),"",lblInvoiceDate.Text);
				else
					sw.WriteLine(info2,"","","",lblInvoiceDate.Text);
				if(addr.Length>50)
				{
					string adr=addr.Substring(50);
					sw.WriteLine(info2,"",adr,"",InvoiceDate[1]);
				}
				else
					sw.WriteLine(info2,"",lblPlace.Value.ToUpper(),"",InvoiceDate[1]);
				if(addr.Length>50)
					sw.WriteLine(info2,"",lblPlace.Value.ToUpper(),"",lblDueDate.Value);
				else
					sw.WriteLine(info2,"Tin No",TinNo,"",lblDueDate.Value);
				if(addr.Length>50)
					sw.WriteLine(info2,"Tin No",TinNo,"",lblVehicleNo.Value);
				else
					sw.WriteLine(info2,"","","",lblVehicleNo.Value);
				sw.WriteLine("");
				sw.WriteLine("");
				
				sw.WriteLine(info3,"P-Code","  HSN"," Grade/Package Name","B-Qty","F-Qty"," D-Qty"," Ltr/Kg"," Rate Rs."," Sch Disc."," Amount (Rs.)", "CGST (Rs.)", "SGST (Rs.)", "IGST (Rs.)");
                sw.WriteLine("");
				
				for(k=arrCount;k<arrBillQty.Count;k++,arrCount++)
				{
					sw.WriteLine(info3,arrProdCode[k].ToString(),tmpHsn[k].Value.ToString(),GenUtil.TrimLength(arrProdName[k].ToString(),34),arrBillQty[k].ToString(),arrFreeQty[k].ToString(),arrDespQty[k].ToString(),arrLtrkg[k].ToString(),arrProdRate[k].ToString(),arrProdScheme[k].ToString(),arrProdAmount[k].ToString(),tmpCgst[k].Value.ToString(), tmpSgst[k].Value.ToString(), tmpIgst[k].Value.ToString());
					if(k==17 && arrBillQty.Count<25)
					{
						FlagCount=true;
					}
					if(k==25)
					{
						FlagCount=true;
						break;
					}
					if(k==51)
					{
						FlagCount=true;
						break;
					}
				}
				Space=SpaceCount-25;
				if(Space>0)
				{
					SpaceCount-=25;
					for(int r=0;r<=18;r++)
					{
						sw.WriteLine();
					}
				}
				else
				{
					Space=Math.Abs(Space);
					if(Space>=8)
					{
						for(int r=8;r<=Space;r++)
						{
							sw.WriteLine();
						}
					}
					else
					{
						//for(int r=Space;r<26;r++)
						for(int r=0;r<=Space+17;r++)
						{
							sw.WriteLine();
						}
					}
					SpaceCount=0;
				}
				
				//**********************************************
				
			}
			while(FlagCount==true);
			sw.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------");
			//sw.WriteLine("");
			sw.WriteLine(info4,"","Packs","Ltrs","GROSS AMOUNT         : ",txtGrandTotal.Text);
			sw.WriteLine(info4,"","----------","----------","FREE/SCH DISC        : ","-"+txtschemetotal.Text);
			if(txtfleetoedis.Text=="" || txtfleetoedis.Text=="0")
				sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txtlitertotal.Text,"","");
			else
				sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txtlitertotal.Text,"Oe/Fleet Discount    : ","-"+txtfleetoedis.Text);
			if(txtDisc.Text=="" || txtDisc.Text=="0")
				sw.WriteLine(info4,"","","","Discount             : ","0");
			else
			{
//				if(DropDiscType.SelectedItem.Text.Equals("%"))
//					sw.WriteLine(info4,"","","","Discount("+txtDisc.Text+DropDiscType.SelectedItem.Text+")      : ","-"+tempdiscount.Value);
//				else
				sw.WriteLine(info4,"","","","Discount("+txtDiscType.Text+")        : ","-"+ tempdiscount.Value);
			}
			if(txtCashDisc.Text=="" || txtCashDisc.Text=="0")
				sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount        : ","0");
			else
			{
//				if(DropCashDiscType.SelectedItem.Text.Equals("%"))
//					sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount("+txtCashDisc.Text+DropCashDiscType.SelectedItem.Text+") : ","-"+tempcashdis.Value);
//				else
				sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount("+txtCashDiscType.Text+")   : ","-"+tempcashdis.Value);
			}
            sw.WriteLine(info4, "", "----------", "----------", "Total CGST          : ", tempTotalCgst.Value);
            sw.WriteLine(info4, "", "", "", "Total SGST          : ", tempTotalSgst.Value);
            sw.WriteLine(info4, "", "", "", "Total IGST          : ", tempTotalIgst.Value);
            sw.WriteLine(info4,"Total Qty",System.Convert.ToString(TotalQtyfoe+TotalQtyPack),System.Convert.ToString(System.Convert.ToDouble(txtlitertotal.Text)+TotalfoeLtr),"Net Amount           : ", tempNetAmnt.Value);
			sw.WriteLine(info5,"",GenUtil.ConvertNoToWord(txtNetAmount.Text));
			sw.WriteLine(info6,"",CurrBal[0],"(INCLUDING CURRENT INVOICE AMOUNT)");
			sw.WriteLine(info7,"",txtRemark.Text);
			sw.Close();
            clear();
		}

		/// <summary>
		/// Sends the SalesReturn.txt file name to print server.
		/// </summary>
		public void print()
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
                PrePrintReport();
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SalesReturnReport.txt<EOF>");
					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);
					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
				} 
				catch (ArgumentNullException ane) 
				{
					CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:print  ArgumentNullException : "+ane.Message+" Userid: "+uid);
				} 
				catch (SocketException se) 
				{
					CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:print  SocketException : "+se.Message+" Userid: "+uid);
				} 
				catch (Exception es) 
				{
					CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:print  Unexpected exception : "+ es.Message+" Userid: "+uid);
 				}
			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:SalesReturn.aspx,Method:print.   EXCEPTION: "+ex.Message+" User: "+uid);
			}
		}

		protected void Check2_ServerChange(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Sends the SalesReturn.txt file name to print server.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			print();
		}

		protected void txtProdName1_ServerChange(object sender, System.EventArgs e)
		{
		
		}
		
		/*public void InsertBatchNo(string Prod,string PackType,string Qty)
		{
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass();
			DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr1 = null;
			int SNo=0;
			rdr1 = obj1.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
			if(rdr1.Read())
			{
				if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
					SNo=int.Parse(rdr1.GetValue(0).ToString());
				else
					SNo=1;
			}
			else
				SNo=1;
			rdr1.Close();
			SqlDataReader rdr = obj.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by Batch_ID");
			string PID="";
			int count=int.Parse(Qty);
			int k=0,x=0,flag=0,flagflag=0;
			double cl_sk=0;
			while(rdr.Read())
			{
				if(double.Parse(rdr["qty"].ToString())>0)
				{
					dbobj1.SelectQuery("select top 1 Closing_Stock from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"' order by sno desc",ref rdr1);
					if(rdr1.Read())
					{
						if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
							cl_sk=double.Parse(rdr1.GetValue(0).ToString());
						else
							cl_sk=0;
					}
					else
						cl_sk=0;
					rdr1.Close();
				}
				if(count>0)
				{
					if(int.Parse(rdr["qty"].ToString())>0)
					{
						if(count<=int.Parse(rdr["qty"].ToString()))
						{
							cl_sk+=count;
							dbobj1.Insert_or_Update("update batchno set qty=qty+"+count+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
							dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales-"+count+",closing_stock=closing_stock+"+count+" where productid='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
//							if(lblInvoiceNo.Visible==true)
//								dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Return','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
//							else
							dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Return','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
							count=0;
							break;
						}
						else
						{
							cl_sk+=double.Parse(rdr["qty"].ToString());
							dbobj1.Insert_or_Update("update batchno set qty='"+rdr["qty"].ToString()+"' where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
							dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales-"+double.Parse(rdr["qty"].ToString())+",closing_stock=closing_stock+"+double.Parse(rdr["qty"].ToString())+" where productid='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
//							if(lblInvoiceNo.Visible==true)
//								dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblInvoiceNo.Text+"','Sales Return','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
//							else
							dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Return','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
							count+=int.Parse(rdr["qty"].ToString());
						}
					}
				}
			}
			rdr.Close();
		}*/

		/// <summary>
		/// This method is used to save the batch information in other table.
		/// </summary>
		/// <param name="Prod"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		public void UpdateBatchNo(string Prod, string PackType, string Qty)
		{
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr;
			SqlCommand cmd;
			string ProdID="";
			double CountQty=double.Parse(Qty),cl_sk=0;
			DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlDataReader rdr1 = null;
			int SNo=0;
			dbobj.SelectQuery("select max(SNo)+1 from Batch_Transaction",ref rdr1);
			if(rdr1.Read())
			{
				if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
					SNo=int.Parse(rdr1.GetValue(0).ToString());
				else
					SNo=1;
			}
			else
				SNo=1;
			rdr1.Close();
			dbobj.SelectQuery("select prod_id from products where prod_name='"+Prod+"' and pack_type='"+PackType+"'",ref rdr1);
			if(rdr1.Read())
			{
				ProdID=rdr1["Prod_ID"].ToString();
			}
			rdr1.Close();
			rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Sales Invoice' and prod_id='"+ProdID+"'");
			while(rdr.Read())
			{
				//******************************
				if(CountQty != 0)
				{
					dbobj.SelectQuery("select top 1 Closing_Stock from Batch_Transaction where prod_id='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"' order by sno desc",ref rdr1);
					if(rdr1.Read())
					{
						if(rdr1.GetValue(0).ToString()!="" && rdr1.GetValue(0).ToString()!=null)
							cl_sk=double.Parse(rdr1.GetValue(0).ToString());
						else
							cl_sk=0;
					}
					else
						cl_sk=0;
					rdr1.Close();
					if(CountQty <= double.Parse(rdr["Qty"].ToString()))
					{
						Con.Open();
						//cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+CountQty+",Closing_Stock=Closing_Stock+"+CountQty+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						Con.Open();
						//cmd = new SqlCommand("update BatchNo set Qty=Qty+"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd = new SqlCommand("update BatchNo set Qty=Qty+"+CountQty+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						cl_sk+=CountQty;
						Con.Open();
						cmd = new SqlCommand("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Return','"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+CountQty.ToString()+"',"+cl_sk.ToString()+")",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						CountQty=0;
					}
					else
					{
						Con.Open();
						//cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						Con.Open();
						cmd = new SqlCommand("update BatchNo set Qty=Qty+"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						cl_sk+=double.Parse(rdr["Qty"].ToString());
						Con.Open();
						cmd = new SqlCommand("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Sales Return','"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["Qty"].ToString()+"',"+cl_sk.ToString()+")",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						CountQty-=double.Parse(rdr["Qty"].ToString());
					}
				}
			}
			rdr.Close();
		}

		/// <summary>
		/// This method fatch the only year according to passing date.
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetYear(string dt)
		{
			if(dt!="")
			{
				string[] year=dt.IndexOf("/")>0?dt.Split(new char[] {'/'},dt.Length): dt.Split(new char[] { '-' }, dt.Length);
				string yr=year[2].Substring(2);	
				return(yr);
			}
			else
				return "";
		}

		/// <summary>
		/// This method stored given scheme of the products and closing stock also.
		/// </summary>
		public void getscheme()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string str="";
			SqlDataReader rdr=null; 
			 
			sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(lblInvoiceDate.Text.Trim()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";
			//sql="select p.category cat,p.prod_name pname,p.pack_type ptype,o.onevery one,o.freepack freep,o.schprodid sch,o.datefrom df,o.dateto dt,o.discount dis,o.schname scheme  from products p,oilscheme o where p.prod_id=o.prodid and cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(Session["CurrentDate"].ToString())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Session["CurrentDate"].ToString()) +"' and schname in ('Primary(Free Scheme)','Secondry(Free Scheme)')";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				str=str+":"+SqlDtr["cat"]+":"+SqlDtr["pname"]+":"+SqlDtr["ptype"];
				
				string sql1="select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack freep,o.datefrom df,o.dateto dt,p.unit unit from products p,oilscheme o where p.prod_id='"+SqlDtr["sch"]+"'";
				//string sql1="select p.category cat1,p.prod_name pname1,p.pack_type ptype1,o.onevery one,o.freepack free from products p,oilscheme o where p.prod_id='"+SqlDtr.GetValue(5).ToString()+"'";
				dbobj.SelectQuery(sql1,ref rdr); 
				//while(rdr.Read())
				string unit="";
				if(rdr.Read())
				{
					str=str+":"+rdr["cat1"]+":"+rdr["pname1"]+":"+rdr["ptype1"]+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()))+",";
					unit =rdr["unit"].ToString();
					//str=str+":"+rdr["cat1"]+":"+rdr["pname1"]+":"+rdr["ptype1"]+":"+rdr["one"]+":"+rdr["free"]+",";
				}
				else
				{
					str=str+":"+0+":"+0+":"+0+":"+SqlDtr["one"]+":"+SqlDtr["freep"]+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["df"].ToString()))+":"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["dt"].ToString()))+",";
					unit ="";
				}
				rdr.Close();
			}
			SqlDtr.Close();
			temptext11.Value=str;
		}

		/// <summary>
		/// This method stored given scheme of the products and closing stock also.
		/// </summary>
		public void GetFOECust()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string str="";
			string sql="select cust_Name from customer  where cust_type like'Fleet%' or cust_type like('Oe%')  order by cust_Name";
			SqlDtr = obj.GetRecordSet (sql);
			while(SqlDtr.Read ())
			{
				str=str+","+SqlDtr.GetValue(0).ToString().Trim();
			}
			SqlDtr.Close();
			temptext13.Value=str;
		}
	}
}
