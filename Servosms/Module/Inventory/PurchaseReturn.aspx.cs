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
	/// Summary description for PurchaseReturn.
	/// </summary>
	public partial class PurchaseReturn : System.Web.UI.Page
	{
		DBUtil dbobj = new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid= "";

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
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
				txtMessage.Text =(Session["Message"].ToString());
				txtVatRate.Value  = (Session["VAT_Rate"].ToString());  
				lblEntryBy.Text = uid;
				lblEntryTime.Text = DateTime.Now.ToString (); 
				if(!Page.IsPostBack)
				{
					checkPrevileges(); 
					getInvoiceNo();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:page_load  EXCEPTION: "+ ex.Message+"  User: "+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}

		/// <summary>
		/// Its Checks the user privileges
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="4";
			string SubModule="7";
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
		/// Its fetch the invoice no.s from Purchase_details table except those r present in Purchase_Return_Master table.
		/// </summary>
		public void getInvoiceNo()
		{
			try
			{
				SqlDataReader SqlDtr = null;
				dropInvoiceNo.Items.Clear();
				dropInvoiceNo.Items.Add("Select");  
				dbobj.SelectQuery("Select distinct pd.Invoice_No from Purchase_Details pd where pd.Invoice_No not in (Select Invoice_No from Purchase_Return_Master) ",ref SqlDtr);
				while(SqlDtr.Read())
				{
					dropInvoiceNo.Items.Add(SqlDtr["Invoice_No"].ToString());   
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:getInvoiceNo()  EXCEPTION: "+ ex.Message+"  User: "+uid);	
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
		/// Its gets the selected invoice no. and display the details in form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropInvoiceNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InventoryClass  obj=new InventoryClass ();
			try
			{
				if(dropInvoiceNo.SelectedIndex == 0  )
				{
					MessageBox.Show("Please Select Invoice No");
				}
				else
				{
					Clear();
					HtmlInputText[] Name={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20}; 
					//HtmlInputText[] Type={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
					TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20}; 
					TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20}; 
					TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20}; 			
					TextBox[]  Quantity = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12,txtTempQty13,txtTempQty14,txtTempQty15,txtTempQty16,txtTempQty17,txtTempQty18,txtTempQty19,txtTempQty20};
					HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20};
					HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12,tmpQty13,tmpQty14,tmpQty15,tmpQty16,tmpQty17,tmpQty18,tmpQty19,tmpQty20};
					HtmlInputCheckBox[] chkFOC = {chkFOC1,chkFOC2,chkFOC3,chkFOC4,chkFOC5,chkFOC6,chkFOC7,chkFOC8,chkFOC9,chkFOC10,chkFOC11,chkFOC12,chkFOC13,chkFOC14,chkFOC15,chkFOC16,chkFOC17,chkFOC18,chkFOC19,chkFOC20};
					SqlDataReader SqlDtr;
					string sql;
					string strDate,strDate1;
					int i=0;
					sql="select * from Purchase_Master where Invoice_No='"+ dropInvoiceNo.SelectedItem.Value +"'" ;
					SqlDtr=obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
						strDate = SqlDtr.GetValue(1).ToString().Trim();
						int pos = strDate.IndexOf(" ");
				
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}

						strDate1 = SqlDtr.GetValue(6).ToString().Trim();
						pos = strDate1.IndexOf(" ");
				
						if(pos != -1)
						{
							strDate1 = strDate1.Substring(0,pos);
						}
						else
						{
							strDate1 = "";					
						}
						lblInvoiceDate.Text =GenUtil.str2DDMMYYYY(strDate);  
						lblVehicleNo.Value=SqlDtr.GetValue(4).ToString();
						lblVendInvoiceNo.Value =SqlDtr.GetValue(5).ToString();
						lblVendInvoiceDate.Value =GenUtil.str2DDMMYYYY(strDate1);
						txtGrandTotal.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString());
						tmpGrandTotal.Value = GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString());
						txtDisc.Text=SqlDtr.GetValue(8).ToString(); 
						tmpDisc.Value = SqlDtr.GetValue(8).ToString(); 
						txtDiscType.Text = SqlDtr.GetValue(9).ToString();
						if(txtDiscType.Text =="Per")
							txtDiscType.Text = "%"; 
						txtNetAmount.Text =GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()); 
						tmpNetAmount.Value = GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()); 
						txtPromoScheme.Text= SqlDtr.GetValue(11).ToString(); 
						txtRemark.Text=SqlDtr.GetValue(12).ToString();  
						lblEntryBy.Text=SqlDtr.GetValue(13).ToString();  
						lblEntryTime.Text= SqlDtr.GetValue(14).ToString();  
						txtCashDisc.Text=SqlDtr.GetValue(15).ToString(); 
						txtCashDisc.Text = GenUtil.strNumericFormat(txtCashDisc.Text.ToString()); 
						tmpCashDisc.Value = txtCashDisc.Text;
						txtCashDiscType.Text = SqlDtr.GetValue(16).ToString();
						if(txtCashDiscType.Text =="Per")
							txtCashDiscType.Text = "%"; 
						txtVAT.Text =  SqlDtr.GetValue(17).ToString();
						tmpVatAmount.Value = SqlDtr.GetValue(17).ToString();
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
						/********bhal add********/
						txttrade.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(18).ToString()); 
						txttradeamt.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(19).ToString()); 
						txtebird.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(20).ToString()); 
						txtebirdamt.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(21).ToString()); 
						txtfocamt.Text=GenUtil.strNumericFormat(SqlDtr.GetValue(24).ToString());
						//dropfoc.SelectedIndex= dropfoc.Items.IndexOf((dropfoc.Items.FindByValue(SqlDtr.GetValue(25).ToString())));
						txtfoctype.Text = SqlDtr.GetValue(25).ToString();
						if(txtfoctype.Text =="Per")
							txtfoctype.Text = "%"; 
						txtentry.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(22).ToString()); 
						//dropentry.SelectedIndex= dropentry.Items.IndexOf((dropentry.Items.FindByValue(SqlDtr.GetValue(23).ToString())));
						txtentrytype.Text = SqlDtr.GetValue(23).ToString();
						if(txtentrytype.Text =="Per")
							txtentrytype.Text = "%"; 
						//txtVAT.Text =  SqlDtr.GetValue(17).ToString();
						//************bhal end***************/						
					}
					SqlDtr.Close();
					sql="select s.Supp_Name,s.City from Supplier as s, Purchase_Master as p where p.Invoice_No='"+dropInvoiceNo.SelectedValue +"' and S.Supp_ID = P.Vendor_ID ";
					SqlDtr=obj.GetRecordSet(sql); 
					while(SqlDtr.Read())
					{
						lblVendName.Value = SqlDtr.GetValue(0).ToString();  
						lblPlace.Value=SqlDtr.GetValue(1).ToString();  
					}   
					SqlDtr.Close();

					#region Get Data from Purchase Details Table regarding Invoice No.
					sql="select p.Category,p.Prod_Name,p.Pack_Type,	pd.qty,pd.rate,pd.amount,pd.foc"+
						" from Products p, Purchase_Details pd"+
						" where p.Prod_ID=pd.prod_id and pd.invoice_no='"+ dropInvoiceNo.SelectedItem.Value +"'" ;

					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						//Qty[i].Enabled = true;
						Check[i].Checked = false;
						Name[i].Value=SqlDtr.GetValue(1).ToString()+":"+SqlDtr.GetValue(2).ToString();   
						//Type[i].Value=SqlDtr.GetValue(2).ToString();   
						Qty[i].Text=SqlDtr.GetValue(3).ToString();
						Quantity[i].Text = Qty[i].Text;
						tmpQty[i].Value = Qty[i].Text;
						Rate[i].Text=SqlDtr.GetValue(4).ToString();
						Amount[i].Text=SqlDtr.GetValue(5).ToString();
						if(SqlDtr.GetValue(6).ToString().Equals("0"))
							chkFOC[i].Checked=false;
						else
							chkFOC[i].Checked=true;
						i++;
					}
					while(i<12)
					{
						Qty[i].Text="";
						Qty[i].Enabled = false;
						tmpQty[i].Value = "";
						Check[i].Checked = false; 
						chkFOC[i].Checked=false;
						i++;
					}
					SqlDtr.Close();
					CheckAll.Checked = false;
					#endregion
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:btnSaved_Click,Class:PartiesClass.cs,Method :dropInvoiceNo_SelectedIndexChanged  EXCEPTION  "+ex.Message+"  userid "+uid);
			}
		}


		/// <summary>
		/// Its clears the form.
		/// </summary>
		public void Clear()
		{
			lblInvoiceDate.Text = "";
			lblVendInvoiceDate.Value = "";
			lblVendInvoiceNo.Value = "";
			lblVendName.Value = "";
			lblPlace.Value = "";
			lblVehicleNo.Value = "";
			txtPromoScheme.Text = "";
			txtRemark.Text = "";
			txtGrandTotal.Text = "";
			txtDisc.Text = "";
			txtDiscType.Text = "";
			txtVAT.Text = "";
			No.Checked = true;
			Yes.Checked = false;
			txtCashDisc.Text = "";
			txtCashDiscType.Text = "";
			txtNetAmount.Text = "";
			tmpNetAmount.Value = "";
			tmpGrandTotal.Value ="";
			tmpDisc.Value ="";
			tmpCashDisc.Value ="";
			tmpVatAmount.Value = "";
			txttradeless.Text="";
			txtbirdless.Text="";

			HtmlInputText[] Name={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20}; 
			//HtmlInputText[] Type={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
			TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20}; 
			TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20}; 
			TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20};
			TextBox[]  Quantity = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12,txtTempQty13,txtTempQty14,txtTempQty15,txtTempQty16,txtTempQty17,txtTempQty18,txtTempQty19,txtTempQty20};
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20};
			HtmlInputCheckBox[] chkFOC = {chkFOC1,chkFOC2,chkFOC3,chkFOC4,chkFOC5,chkFOC6,chkFOC7,chkFOC8,chkFOC9,chkFOC10,chkFOC11,chkFOC12,chkFOC13,chkFOC14,chkFOC15,chkFOC16,chkFOC17,chkFOC18,chkFOC19,chkFOC20};
			HtmlInputHidden[] tmpQty = {tmpQty1,tmpQty2,tmpQty3,tmpQty4,tmpQty5,tmpQty6,tmpQty7,tmpQty8,tmpQty9,tmpQty10,tmpQty11,tmpQty12,tmpQty13,tmpQty14,tmpQty15,tmpQty16,tmpQty17,tmpQty18,tmpQty19,tmpQty20};

			for(int i=0; i<Name.Length; i++)
			{
				Name[i].Value = "";
				//Type[i].Value = "";
				Qty[i].Text = "";
				Rate[i].Text = "";
				Amount[i].Text = "";
				Quantity[i].Text = "";
				Check[i].Checked = false;
				tmpQty[i].Value = "";
				chkFOC[i].Checked=false;
			}
			CheckAll.Checked = false;
		}

		/// <summary>
		/// Its saves the purchase return details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			InventoryClass  obj = new InventoryClass(); 
			if(dropInvoiceNo.SelectedIndex == 0)
			{
				MessageBox.Show("Please Select Invoice No.") ;
				return;
			}

			int c = 0;
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20};
			for(int i=0;i < Check.Length ; i++)
			{
				if(Check[i].Checked == false)
				{
					c++;
				}
			}
			if(c == 20)
			{
				MessageBox.Show("Please select a Product to return");
				return;
			}
			try
			{
				int count = 0;
				//This used to solve the double click problem. Its checks the Purchase Return and Invoice no and display the popup.
				dbobj.ExecuteScalar("Select count(Invoice_No) from Purchase_Return_Master where Invoice_No = "+dropInvoiceNo.SelectedItem.Text.Trim(),ref count);
				if(count > 0)
				{
					MessageBox.Show("Purchase Return Saved");						
					Clear(); 
					getInvoiceNo();
					dropInvoiceNo.SelectedIndex = 0;  
					return ;

				}
				obj.Invoice_Date=System.Convert.ToDateTime(GenUtil.str2DDMMYYYY (lblInvoiceDate.Text.ToString() )) ;
				obj.Vendor_Name=lblVendName.Value.ToString();
				obj.City=lblPlace.Value .ToString();
				obj.Vehicle_No=lblVehicleNo.Value  ;
				obj.Vendor_Invoice_No =lblVendInvoiceNo.Value ;
				obj.Vendor_Invoice_Date=GenUtil.str2DDMMYYYY(lblVendInvoiceDate.Value) ;
				obj.Grand_Total = Request.Form["txtGrandTotal"];
				if(txtDisc.Text=="")
					obj.Discount ="0.0";
				else
					obj.Discount =txtDisc.Text;
				obj.Discount_Type=txtDiscType.Text ;
				obj.Net_Amount = Request.Form["txtNetAmount"];
				obj.Promo_Scheme=txtPromoScheme.Text;
				obj.Remerk =txtRemark.Text;
				obj.Entry_By =lblEntryBy.Text ;
				//obj.Entry_Time =DateTime.Parse(lblEntryTime .Text);			
				obj.Entry_Time =System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString());
				if(txtCashDisc.Text.Trim() =="")
					obj.Cash_Discount  ="0.0";
				else
					obj.Cash_Discount  = txtCashDisc.Text.Trim() ;
				obj.Cash_Disc_Type =txtDiscType.Text ;
				obj.VAT_Amount = Request.Form["txtVAT"]; 
				obj.Invoice_No = dropInvoiceNo.SelectedItem.Text; 
				obj.Pre_Amount = tmpNetAmount.Value; 
				/////////////******Save*****************
//				if(txtfocamt.Text.Trim() =="")
				if(tempfoc.Value.Trim() =="")
					obj.Foc_Discount  ="0.0";
				else
					//obj.Foc_Discount  = txtfocamt.Text.Trim() ;
				obj.Foc_Discount  = tempfoc.Value.Trim() ;
				obj.Foc_Discount_Type =txtfoctype.Text;
				//if(txtentry.Text.Trim() =="")
					if(tempentrytax.Value.Trim() =="")
					obj.Entry_Tax1  ="0.0";
				else
					//obj.Entry_Tax1  = txtentry.Text.Trim() ;
				obj.Entry_Tax1  = tempentrytax.Value.Trim() ;
				obj.Entry_Tax_Type =txtentrytype.Text ;
				if(txtebird.Text.Trim() =="")

					obj.Ebird  ="0.0";
				else
					obj.Ebird  = txtebird.Text.Trim() ;
				//obj.Ebird=txtebird.Text.Trim();
				//if(txtebirdamt.Text.Trim() =="")
				if(tempebird.Value.Trim() =="")
					obj.Ebird_Discount  ="0.0";
				else
					//obj.Ebird_Discount  = txtebirdamt.Text.Trim() ;
					obj.Ebird_Discount  = tempebird.Value.Trim() ;
				if(txttrade.Text.Trim() =="")
					obj.Tradeval  ="0.0";
				else
					obj.Tradeval  = txttrade.Text.Trim() ;
				//obj.Tradeval=txttradedis.Text.Trim();
//				if(txttradeamt.Text.Trim() =="")
				if(temptrade.Value.Trim() =="")
					obj.Trade_Discount  ="0.0";
				else
					//obj.Trade_Discount  = txttradeamt.Text.Trim() ;
				obj.Trade_Discount  = temptrade.Value.Trim() ;
					
						
				///************************
				//Calls the InsertPurchaseReturnMaster which calls the ProInsertPurchaseReturnMaster procedure to insert the Purchase Return master details.
				
				obj.InsertPurchaseReturnMaster();		
	
				HtmlInputCheckBox[] chkFOC = {chkFOC1,chkFOC2,chkFOC3,chkFOC4,chkFOC5,chkFOC6,chkFOC7,chkFOC8,chkFOC9,chkFOC10,chkFOC11,chkFOC12,chkFOC13,chkFOC14,chkFOC15,chkFOC16,chkFOC17,chkFOC18,chkFOC19,chkFOC20};
				HtmlInputText[]  ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20}; 
				//HtmlInputText[]  PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12}; 
				TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20}; 
				TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20}; 
				TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20};
				TextBox[]  Quantity = {txtTempQty1,txtTempQty2,txtTempQty3,txtTempQty4,txtTempQty5,txtTempQty6,txtTempQty7,txtTempQty8,txtTempQty9,txtTempQty10,txtTempQty11,txtTempQty12,txtTempQty13,txtTempQty14,txtTempQty15,txtTempQty16,txtTempQty17,txtTempQty18,txtTempQty19,txtTempQty20};
				
               //calls the save fucntion to save the purchase return details of only the return products.
				for(int j=0;j<ProdName.Length ;j++)
				{
					if(Check[j].Checked == false )
						continue;
					string[] arrName=ProdName[j].Value.Split(new char[] {':'},ProdName[j].Value.Length);
					//Save(ProdName[j].Value,PackType[j].Value,Qty[j].Text.ToString(),Rate[j].Text.ToString (),Amount[j].Text.ToString ());
					Save(arrName[0].ToString(),arrName[1].ToString(),Qty[j].Text.ToString(),Rate[j].Text.ToString (), Request.Form[Amount[j].ID].ToString());
					UpdateBatchNo(arrName[0].ToString(),arrName[1].ToString(),Qty[j].Text);
				}
		
				MessageBox.Show("Purchase Return Saved");
				CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:btnSaved_Click,Class:PartiesClass.cs"+"  Purchase Return for  Invoice No."+obj.Invoice_No+" is Saved  Userid: "+uid);
				reportmaking(); 
				//print();
				Clear();
				getInvoiceNo();
				dropInvoiceNo.SelectedIndex = 0;
			}
			catch(Exception ex)
			{
               CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:btnSaved_Click,Class:PartiesClass.cs  EXCEPTION: "+ex.Message+"    Userid: "+uid);
			}
		}

		/// <summary>
		/// This calls the fucntion InsertPurchaseReturnDetails() which calls the Procedure ProInsertPurchaseReturnMaster to enter the details of return products.
		/// </summary>
		/// <param name="ProdName"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		/// <param name="Rate"></param>
		/// <param name="Amount"></param>
		public void Save(string ProdName,string PackType, string Qty, string Rate,string Amount)
		{
			InventoryClass obj=new InventoryClass();
			obj.Product_Name=ProdName;
			obj.Package_Type=PackType;
			obj.Qty=Qty;
			obj.Rate=Rate;
			obj.Invoice_No=dropInvoiceNo.SelectedItem.Value;
			obj.InsertPurchaseReturnDetail(); 
		}
		
		/// <summary>
		/// Creates the report file PurchaseReturn.txt and fetch the screen details of a return products and writes it into a file for printing.
		/// </summary>
		public void reportmaking()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PurchaseReturnReport.txt";
					
			StreamWriter sw = new StreamWriter(path);
			string info = "";
			string strInvNo="";
			string strDiscType = "";
			double disc_amt=0;
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20};
			HtmlInputCheckBox[] chkFOC = {chkFOC1,chkFOC2,chkFOC3,chkFOC4,chkFOC5,chkFOC6,chkFOC7,chkFOC8,chkFOC9,chkFOC10,chkFOC11,chkFOC12,chkFOC13,chkFOC14,chkFOC15,chkFOC16,chkFOC17,chkFOC18,chkFOC19,chkFOC20};
			HtmlInputText[]  ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20}; 
			//HtmlInputText[]  PackType={txtPack1, txtPack2, txtPack3, txtPack4, txtPack5, txtPack6, txtPack7, txtPack8, txtPack9, txtPack10, txtPack11, txtPack12, txtPack13, txtPack14, txtPack15, txtPack16, txtPack17, txtPack18, txtPack19, txtPack20}; 
			TextBox[]  Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20}; 
			TextBox[]  Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20};
			TextBox[]  Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20};
			string[] products = {"","","","","","","","","","","","","","","","","","","",""};
			string[] qty = {"","","","","","","","","","","","","","","","","","","",""};
			string[] rate = {"","","","","","","","","","","","","","","","","","","",""};
			string[] amt = {"","","","","","","","","","","","","","","","","","","",""};
			int j = 0;
			for(int i = 0; i < Check.Length ; i++)
			{
				if(Check[i].Checked)
				{
					//products[j] = ProdName[i].Value +" "+PackType[i].Value;
					products[j] = ProdName[i].Value;
					qty[j] = Qty[i].Text ;
					rate[j] = Rate[i].Text;
					amt[j] = Amount[i].Text;
					j++;
				}
			}

			string msg ="";
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
			sw.WriteLine(GenUtil.GetCenterAddr("=================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("PURCHASE RETURN",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=================",des.Length));
			strInvNo = dropInvoiceNo.SelectedItem.Text;   

			sw.WriteLine("Invoice No : " +strInvNo+ "                               Date : " +lblInvoiceDate.Text.ToString());
			sw.WriteLine("+----------------------------------------------------------------+");
			sw.WriteLine("Vendor Name             : " + lblVendName.Value);
			sw.WriteLine("Place                   :  " + lblPlace.Value);
			sw.WriteLine("Vendor Invoice No       :  " + lblVendInvoiceNo.Value);
			sw.WriteLine("Vendor Invoice Date     :  " + lblVendInvoiceDate.Value);
			sw.WriteLine("+------------------------------+-----------+----------+----------+");
			sw.WriteLine("|      Product Name            | Quantity  |   Rate   |  Amount  |");
			sw.WriteLine("+------------------------------+-----------+----------+----------+");
			info = " {0,-30:S} {1,10:F}  {2,10:F} {3,10:F}";
			// Writes the return details  upto to 8 products and  otherwise if less then writes the blank value.
			sw.WriteLine(info,products[0]  ,qty[0],rate[0],GenUtil.strNumericFormat(amt[0]));
			sw.WriteLine(info,products[1]  ,qty[1],rate[1],GenUtil.strNumericFormat(amt[1]));
			sw.WriteLine(info,products[2]  ,qty[2],rate[2],GenUtil.strNumericFormat(amt[2]));
			sw.WriteLine(info,products[3]  ,qty[3],rate[3],GenUtil.strNumericFormat(amt[3]));
			sw.WriteLine(info,products[4]  ,qty[4],rate[4],GenUtil.strNumericFormat(amt[4]));
			sw.WriteLine(info,products[5]  ,qty[5],rate[5],GenUtil.strNumericFormat(amt[5]));
			sw.WriteLine(info,products[6]  ,qty[6],rate[6],GenUtil.strNumericFormat(amt[6]));
			sw.WriteLine(info,products[7]  ,qty[7],rate[7],GenUtil.strNumericFormat(amt[7]));
		
		sw.WriteLine("+------------------------------+-----------+----------+----------+");
		
			sw.WriteLine("                               Grand Total           : {0,10:F}" , GenUtil.strNumericFormat(txtGrandTotal.Text.ToString() ));
			//*********bhal Add*************
			double disDiscount=0;
			double disfoc1=0;
			double disc_amt1=0;
			string msg1 ="";
			string strDiscType1 = "";
			double temp1 =0;
			disc_amt1=0;
			msg1 ="";
			if(tempfoc.Value=="")
			//			if(txtfocamt.Text=="")
			{
				strDiscType1="";
				msg1 = "";
			}
			else
			{
				//disc_amt1 = System.Convert.ToDouble(txtfocamt.Text.ToString()); 
				disc_amt1 = System.Convert.ToDouble(tempfoc.Value.ToString()); 
				strDiscType1= txtfoctype.Text;
				
				if(strDiscType1.Trim().Equals("%"))
				{
						
					if(txtGrandTotal.Text.Trim() != "")
						temp1 = System.Convert.ToDouble(txtGrandTotal.Text.Trim().ToString());
 
					disc_amt1  = (temp1*disc_amt1/100);
					
					msg1 = "("+txtfocamt.Text.ToString()+strDiscType1+")";
						
				}
				else
				{
					msg1 ="("+strDiscType1+")";
				}
			
				msg1 ="("+strDiscType1+")";
				disfoc1=disc_amt1;
			}
			sw.WriteLine("                               FOC Discount{0,-8:S}  : {1,10:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));
			disc_amt1=0;
			msg1 ="";
			temp1 =0;
			//if(txtentry.Text=="")
			if(tempentrytax.Value=="")
			{
				strDiscType1="";
				msg1 = "";
			}
			else
			{
				//disc_amt1 = System.Convert.ToDouble(txtentry.Text.ToString()); 
				disc_amt1 = System.Convert.ToDouble(tempentrytax.Value.ToString()); 
				strDiscType1=txtentrytype.Text;
				if(strDiscType1.Trim().Equals("%"))
				{
						
					if(txtGrandTotal.Text.Trim() != "")
						temp1 = System.Convert.ToDouble(txtGrandTotal.Text.Trim().ToString());
 
					disc_amt1  = (temp1*disc_amt1/100);
					msg1 = "("+tempentrytax.Value.ToString()+strDiscType1+")";
						
				}
				else
				{
					msg1 ="("+strDiscType1+")";
				}
				double disentry=disc_amt1;
			}
			sw.WriteLine("                               Entry Tax {0,-8:S}    : {1,10:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));
				
			disc_amt1=0;
			msg1 ="";
			double sdisc=0;
			//if(txtebirdamt.Text=="")
				if(tempebird.Value=="")
			{
				sdisc=0;
				disc_amt1=0;
				//msg1 = "";
			}
			else
			{
				//disc_amt1 = System.Convert.ToDouble(txtebirdamt.Text.ToString()); 
					disc_amt1 = System.Convert.ToDouble(tempebird.Value.ToString()); 
				sdisc= System.Convert.ToDouble(txtebird.Text.ToString()); 
				sdisc=System.Convert.ToDouble(GenUtil.strNumericFormat(sdisc.ToString()));
					msg1 ="("+sdisc+")";	
			}			
				double disebird=disc_amt1;
			sw.WriteLine("                              Ebird Discount{0,-8:S} : {1,10:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));

			disc_amt1=0;
			msg1 ="";
			sdisc=0;
			//if(txttradeamt.Text=="")
			if(temptrade.Value=="")
			{
				sdisc=0;
				disc_amt1=0;
				msg1 = "";
			}
			else
			{
				//disc_amt1 = System.Convert.ToDouble(txttradeamt.Text.ToString()); 
				disc_amt1 = System.Convert.ToDouble(temptrade.Value.ToString()); 
				sdisc= System.Convert.ToDouble(txttrade.Text.ToString()); 
					sdisc=System.Convert.ToDouble(GenUtil.strNumericFormat(sdisc.ToString()));
				msg1 ="("+sdisc+")";
			}			
				double distrade=disc_amt1;
			sw.WriteLine("                              Trade Discount{0,-8:S} : {1,10:F}" ,msg1,GenUtil.strNumericFormat(disc_amt1.ToString()));
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
			 disDiscount=disc_amt;
			}
			sw.WriteLine("                               Discount     {0,-8:S} : {1,10:F}" ,msg,GenUtil.strNumericFormat(disc_amt.ToString()));

			//*********bhal End************
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
					temp=temp-(disfoc1+distrade+disebird+disDiscount);
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
		
			
			sw.WriteLine("                               Net Amount            : {0,10:F}" , GenUtil.strNumericFormat(txtNetAmount.Text.ToString()));
			sw.WriteLine("+----------------------------------------------------------------+");
			sw.WriteLine("Promo Scheme : " + txtPromoScheme.Text);
			sw.WriteLine("Remarks      : " + txtRemark.Text);
			sw.WriteLine("Message      : " + txtMessage.Text);
			sw.WriteLine("");
			sw.WriteLine("");
			sw.WriteLine("");
			sw.WriteLine("                                                Signature");

			sw.Close();
		}

		/// <summary>
		/// Sends the PurchaseReturn.txt file to Print Server for printing purpose.
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PurchaseReturnReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:print  ArgumentNullException : "+ane.Message+" Userid: "+uid);
			
				} 
				catch (SocketException se) 
				{
					CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:print  SocketException : "+se.Message+" Userid: "+uid);
					
				} 
				catch (Exception es) 
				{
					CreateLogFiles.ErrorLog("Form:PurchaseReturn.aspx,Method:print  Unexpected exception : "+ es.Message+" Userid: "+uid);
			
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:purchaseReturn.aspx,Method:print.   EXCEPTION: "+ex.Message+" User: "+uid);
			}
		}

		protected void CheckAll_ServerChange(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Sends the PurchaseReturn.txt file to Print Server for printing purpose.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			print();
		}

		/// <summary>
		/// This method is used to update the product batch no informatin.
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
			rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and trans_type='Purchase Invoice' and prod_id='"+ProdID+"'");
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
						cmd = new SqlCommand("update StockMaster_Batch set Receipt=Receipt-"+CountQty+",Closing_Stock=Closing_Stock-"+CountQty+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						Con.Open();
						//cmd = new SqlCommand("update BatchNo set Qty=Qty+"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd = new SqlCommand("update BatchNo set Qty=Qty-"+CountQty+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						cl_sk-=CountQty;
						Con.Open();
						cmd = new SqlCommand("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Purchase Return','"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+CountQty.ToString()+"',"+cl_sk.ToString()+")",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						CountQty=0;
					}
					else
					{
						Con.Open();
						//cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd = new SqlCommand("update StockMaster_Batch set Receipt=Receipt-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock-"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						Con.Open();
						cmd = new SqlCommand("update BatchNo set Qty=Qty-"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
						//*****************************
						cl_sk-=double.Parse(rdr["Qty"].ToString());
						Con.Open();
						cmd = new SqlCommand("insert into batch_transaction values("+(SNo++)+",'"+dropInvoiceNo.SelectedItem.Text+"','Purchase Return','"+System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(lblInvoiceDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["Qty"].ToString()+"',"+cl_sk.ToString()+")",Con);
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
		/// Prepares the report file PurchaseReturn.txt for printing.
		/// </summary>
		public void PrePrintReport()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalesReturnPrePrintReport.txt";
			StreamWriter sw = new StreamWriter(path);
			HtmlInputCheckBox[] Check = {Check1,Check2,Check3,Check4,Check5,Check6,Check7,Check8,Check9,Check10,Check11,Check12,Check13,Check14,Check15,Check16,Check17,Check18,Check19,Check20};
			HtmlInputText[] ProdName={txtProdName1, txtProdName2, txtProdName3, txtProdName4, txtProdName5, txtProdName6, txtProdName7, txtProdName8, txtProdName9, txtProdName10, txtProdName11, txtProdName12, txtProdName13, txtProdName14, txtProdName15, txtProdName16, txtProdName17, txtProdName18, txtProdName19, txtProdName20}; 
			HtmlInputCheckBox[] foe = {chkFOC1,chkFOC2,chkFOC3,chkFOC4,chkFOC5,chkFOC6,chkFOC7,chkFOC8,chkFOC9,chkFOC10,chkFOC11,chkFOC12,chkFOC13,chkFOC14,chkFOC15,chkFOC16,chkFOC17,chkFOC18,chkFOC19,chkFOC20};
			TextBox[] Qty={txtQty1, txtQty2, txtQty3, txtQty4, txtQty5, txtQty6, txtQty7, txtQty8, txtQty9, txtQty10, txtQty11, txtQty12, txtQty13, txtQty14, txtQty15, txtQty16, txtQty17, txtQty18, txtQty19, txtQty20}; 
			TextBox[] Rate={txtRate1, txtRate2, txtRate3, txtRate4, txtRate5, txtRate6, txtRate7, txtRate8, txtRate9, txtRate10, txtRate11, txtRate12, txtRate13, txtRate14, txtRate15, txtRate16, txtRate17, txtRate18, txtRate19, txtRate20}; 
			TextBox[] Amount={txtAmount1, txtAmount2, txtAmount3, txtAmount4, txtAmount5, txtAmount6, txtAmount7, txtAmount8, txtAmount9, txtAmount10, txtAmount11, txtAmount12, txtAmount13, txtAmount14, txtAmount15, txtAmount16, txtAmount17, txtAmount18, txtAmount19, txtAmount20};
			//TextBox[] scheme = {txtsch1,txtsch2,txtsch3,txtsch4,txtsch5,txtsch6,txtsch7,txtsch8,txtsch9,txtsch10,txtsch11,txtsch12};	
			//HtmlInputText[] ProdSchName = {txtProdsch1,txtProdsch2,txtProdsch3,txtProdsch4,txtProdsch5,txtProdsch6,txtProdsch7,txtProdsch8,txtProdsch9,txtProdsch10,txtProdsch11,txtProdsch12};	
			//TextBox[] schQty={txtQtysch1,txtQtysch2,txtQtysch3,txtQtysch4,txtQtysch5,txtQtysch6,txtQtysch7,txtQtysch8,txtQtysch9,txtQtysch10,txtQtysch11,txtQtysch12};
			string[] DespQty=new string[20];
			string[] freeDespQty=new string[20];
			string[] ProdCode=new string[20];
			string[] schProdCode=new string[20];
			string[] schPName=new string[20];
			string[] schPPack=new string[20];
			string[] schPQty=new string[20];
			string[] PackType=new string[20];
			string[] schProdName=new string[20];

			Double TotalQtyfoe=0,TotalfoeLtr=0;//,TotalQtyPack=0;
			int k=0;
			string info2="",info3="",info4="",info5="",info6="",info7="",str="",InDate="";
			info2=" {0,-14:S} {1,-50:S} {2,20:S} {3,-46:S}";//Party Name & Address
			info4=" {0,-20:S} {1,20:S} {2,20:S} {3,55:S} {4,15:S}";//Party Name & Address
			//info3=" {0,-10:S} {1,-19:S} {2,-35:S} {3,5:S} {4,5:S} {5,5:S} {6,10:S} {7,12:S} {8,10:S} {9,15:S}";//Item Code
			
			info5=" {0,16:S} {1,-114:S}";
			info7=" {0,46:S} {1,-88:S}";
			info6=" {0,44:S} {1,-10:S} {2,-76:S}";
			//string curbal=lblCurrBalance.Value;
			//string curbal="";
			string[] CurrBal=new string[2];
			string[] InvoiceDate=new string[2];
			
			str="select invoice_date from Purchase_Master where invoice_no="+dropInvoiceNo.SelectedItem.Text.Trim()+"";
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
			//int p1=0;
//			string[] arrProdSchType = new string[2];
//			for(int p=0;p<12;p++)
//			{
//				if(Check[p].Checked==true)
//				{
//					if(ProdSchName[p].Value.IndexOf(":")>0)
//						arrProdSchType=ProdSchName[p].Value.Split(new char[] {':'},ProdSchName[p].Value.Length);
//					else
//					{
//						arrProdSchType[0]="";
//						arrProdSchType[1]="";
//					}
//					//str="select Prod_Code from Products where Prod_Name='"+schProdName[p].Text+"' and Category='"+schProdType[p].Text+"' and Pack_Type='"+schProdPack[p].Text+"'";
//					str="select Prod_Code from Products where Prod_Name='"+arrProdSchType[0].ToString()+"' and Pack_Type='"+arrProdSchType[1].ToString()+"'";
//					SqlDtr = obj.GetRecordSet(str);
//					if(SqlDtr.Read())
//					{
//						schProdCode[p1]=SqlDtr.GetValue(0).ToString();
//						p1++;
//					}
//					SqlDtr.Close();
//				}
//				
//			}
//			int jj1=0;
//			string[] arrProdSchType1 = new string[2];
//			for(int jj=0;jj<12;jj++)
//			{
//				if(Check[jj].Checked==true)
//				{
//					if(ProdSchName[jj].Value.IndexOf(":")>0)
//						arrProdSchType1=ProdSchName[jj].Value.Split(new char[] {':'},ProdSchName[jj].Value.Length);
//					else
//					{
//						arrProdSchType1[0]="";
//						arrProdSchType1[1]="";
//					}
//					if(!arrProdSchType1[0].ToString().Equals("") && !schQty[jj].Text.Equals(""))
//					{
//						schPName[jj1]="(FREE) "+ProdSchName[jj].Value;
//						schPQty[jj1]=schQty[jj].Text;
//						schProdName[jj1]=ProdSchName[jj].Value;
//						schPPack[jj1]=arrProdSchType1[1].ToString();
//						jj1++;
//					}
//				}
//				
//			}
//			for(int jj=jj1;jj<12;jj++)
//			{
//				schPQty[jj]="";
//			}
//			for(int j=0;j<12;j++)
//			{
//				if(Check[j].Checked==true)
//				{
//					if(!Qty[j].Text.Equals(""))
//					{
//						TotalQtyPack=TotalQtyPack+System.Convert.ToDouble(Qty[j].Text);
//						DespQty[j]=Qty[j].Text;
//					}
//					else
//						DespQty[j]="";
//					if(!schQty[j].Text.Equals(""))
//					{
//						TotalQtyfoe=TotalQtyfoe+System.Convert.ToDouble(schQty[j].Text);
//						freeDespQty[j]=schQty[j].Text;
//					}
//					else
//						freeDespQty[j]="";
//				}
//				else
//				{
//					DespQty[j]="";
//					freeDespQty[j]="";
//				}
//			}
//			string[] arrProdSchType2 = new string[2];
//			for(int i=0;i<12;i++)
//			{
//				if(Check[i].Checked==true)
//				{
//					if(ProdSchName[i].Value.IndexOf(":")>0)
//						arrProdSchType2=ProdSchName[i].Value.Split(new char[] {':'},ProdSchName[i].Value.Length);
//					else
//					{
//						arrProdSchType2[0]="";
//						arrProdSchType2[1]="";
//					}
//					//if(schProdPack[i].text != "" && schQty[i].Text != "")
//					if(arrProdSchType2[1].ToString() != "" && schQty[i].Text != "")
//					{
//						TotalfoeLtr=TotalfoeLtr+System.Convert.ToDouble(GenUtil.changeqtyltr(arrProdSchType2[1].ToString(),int.Parse(schQty[i].Text)));
//					}
//				}
//			}	

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
								//arrDespQty.Add(SqlDtr.GetValue(1).ToString());
								arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString())*double.Parse(SqlDtr.GetValue(1).ToString())));
								arrProdRate.Add(Rate[p].Text);
								//arrProdScheme.Add(scheme[p].Text);
								arrProdAmount.Add(System.Convert.ToString(double.Parse(SqlDtr.GetValue(1).ToString())*double.Parse(Rate[p].Text)));
								//arrFreeQty.Add("");
							}
						}
						else
						{
							arrProdCode.Add(ProdCode[p].ToString());
							arrProdName.Add(ProdName[p].Value);
							arrBatchNo.Add("");
							arrBillQty.Add(Qty[p].Text);
							//arrDespQty.Add(DespQty[p].ToString());
							arrLtrkg.Add(System.Convert.ToString(double.Parse(PackType[p].ToString())*double.Parse(Qty[p].Text)));
							arrProdRate.Add(Rate[p].Text);
							//arrProdScheme.Add(scheme[p].Text);
							arrProdAmount.Add(Amount[p].Text);
							//arrFreeQty.Add("");
						}
						SqlDtr.Close();
					}
				}
			}
//			for(int p=0;p<=schPQty.Length-1;p++)
//			{
//				string s=schPQty[p].ToString();
//				if(Check[p].Checked==true)
//				{
//					if(schPQty[p].ToString()!="")
//					{
//						//string[] arrschProdCat = schProdType[p].Text.Split(new char[] {':'},schProdType[p].Text.Length);
//						string[] arrschProdCat = ProdSchName[p].Value.Split(new char[] {':'},ProdSchName[p].Value.Length);
//						str="select b.batch_no,bt.qty from batch_transaction bt,batchno b where b.prod_id=bt.prod_id and b.prod_id=(select prod_id from products where Prod_Code='"+schProdCode[p].ToString()+"' and Prod_Name='"+arrschProdCat[0].ToString()+"' and Pack_Type='"+arrschProdCat[1].ToString()+"') and b.batch_id=bt.batch_id and bt.trans_id='"+dropInvoiceNo.SelectedItem.Text+"' and bt.trans_type='Sales Return'";
//						SqlDtr = obj.GetRecordSet(str);
//						double totalqty=0;
//						if(SqlDtr.HasRows)
//						{
//							while(SqlDtr.Read())
//							{
//								arrProdCode.Add(schProdCode[p].ToString());
//								arrProdName.Add("(FREE) "+ProdSchName[p].Value);
//								arrBatchNo.Add(SqlDtr.GetValue(0).ToString());
//								arrBillQty.Add("");
//								//arrLtrkg.Add(System.Convert.ToString(double.Parse(arrschProdCat[1].ToString())*double.Parse(arrschBillQty[p].ToString())));
//								if(SqlDtr.GetValue(1).ToString()=="" || SqlDtr.GetValue(1).ToString()==null)
//									totalqty=0;
//								else
//									totalqty=double.Parse(SqlDtr.GetValue(1).ToString());
//								arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(),int.Parse(totalqty.ToString())));
//								//arrDespQty.Add(SqlDtr.GetValue(1).ToString());
//								arrProdRate.Add("");
//								//arrProdScheme.Add("");
//								arrProdAmount.Add("");
//								//arrFreeQty.Add(SqlDtr.GetValue(1).ToString());
//							}
//						}
//						else
//						{
//							arrProdCode.Add(schProdCode[p].ToString());
//							arrProdName.Add("(FREE) "+ProdSchName[p].Value);
//							arrBatchNo.Add("");
//							//arrBillQty.Add(schPQty[p].ToString());
//							arrBillQty.Add("");
//							if(schPQty[p].ToString()=="" || schPQty[p].ToString()==null)
//								totalqty=0;
//							else
//								totalqty=double.Parse(schPQty[p].ToString());
//							//arrLtrkg.Add(System.Convert.ToString(double.Parse(arrschProdCat[1].ToString())*double.Parse(arrschBillQty[p].ToString())));
//							arrLtrkg.Add(GenUtil.changeqtyltr(arrschProdCat[1].ToString(),int.Parse(totalqty.ToString())));
//							//arrDespQty.Add(schPQty[p].ToString());
//							arrProdRate.Add("");
//							//arrProdScheme.Add("");
//							arrProdAmount.Add("");
//							//arrFreeQty.Add(schPQty[p].ToString());
//						}
//						SqlDtr.Close();
//					}
//				}
//			}

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
				sw.WriteLine("                                                          PURCHASE RETURN");
				for(int i=0;i<14;i++)
				{
					sw.WriteLine("");
				}
				string addr="",TinNo="";//,ssc="";
				dbobj.SelectQuery("select * from supplier where supp_name='"+lblVendName.Value+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
					addr=SqlDtr["Address"].ToString();
					//ssc=SqlDtr["sadbhavnacd"].ToString();
					TinNo=SqlDtr["Tin_No"].ToString();
				}
				addr=addr.ToUpper();
				
//				if(ssc!="")	
//					sw.WriteLine(info2,"",lblVendName.Value.ToUpper()+"("+ssc+")","",dropInvoiceNo.SelectedItem.Text);
//				else
				sw.WriteLine(info2,"",lblVendName.Value.ToUpper(),"",dropInvoiceNo.SelectedItem.Text);
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
					//sw.WriteLine(info2,"",lblPlace.Value.ToUpper(),"",lblDueDate.Value);
					sw.WriteLine(info2,"",lblPlace.Value.ToUpper(),"","");
				else
					//sw.WriteLine(info2,"Tin No",TinNo,"",lblDueDate.Value);
					sw.WriteLine(info2,"Tin No",TinNo,"","");
				if(addr.Length>50)
					sw.WriteLine(info2,"Tin No",TinNo,"",lblVehicleNo.Value);
				else
					sw.WriteLine(info2,"","","",lblVehicleNo.Value);
				sw.WriteLine("");
				sw.WriteLine("");
				info3=" {0,-15:S} {1,-15:S} {2,-40:S} {3,10:S} {4,10:S} {5,10:S} {6,15:S}";//Item Code
				//sw.WriteLine(info3,"P-Code","  Batch No"," Grade/Package Name","B-Qty","F-Qty"," D-Qty"," Ltr/Kg"," Rate Rs."," Sch Disc."," Amount (Rs.)");
				sw.WriteLine(info3,"Prod-Code","  Batch No","  Grade/Package Name","Bill-Qty","   Ltr/Kg"," Rate Rs."," Amount (Rs.)");
				sw.WriteLine("");
				
				for(k=arrCount;k<arrBillQty.Count;k++,arrCount++)
				{
					sw.WriteLine(info3,arrProdCode[k].ToString(),arrBatchNo[k].ToString(),GenUtil.TrimLength(arrProdName[k].ToString(),34),arrBillQty[k].ToString(),arrFreeQty[k].ToString(),arrDespQty[k].ToString(),arrLtrkg[k].ToString(),arrProdRate[k].ToString(),arrProdScheme[k].ToString(),arrProdAmount[k].ToString());
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
			//sw.WriteLine(info4,"","----------","----------","FREE/SCH DISC        : ","-"+txtschemetotal.Text);
			//if(txtfleetoedis.Text=="" || txtfleetoedis.Text=="0")
			//	sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txtlitertotal.Text,"","");
			//else
			//	sw.WriteLine(info4,"Act Qty",TotalQtyPack.ToString(),txtlitertotal.Text,"Oe/Fleet Discount    : ","-"+txtfleetoedis.Text);
			if(txtDisc.Text=="" || txtDisc.Text=="0")
				sw.WriteLine(info4,"","","","Discount             : ","0");
			else
			{
				//				if(DropDiscType.SelectedItem.Text.Equals("%"))
				//					sw.WriteLine(info4,"","","","Discount("+txtDisc.Text+DropDiscType.SelectedItem.Text+")      : ","-"+tempdiscount.Value);
				//				else
				sw.WriteLine(info4,"","","","Discount("+txtDiscType.Text+")        : ","-"+txtDisc.Text);
			}
			if(txtCashDisc.Text=="" || txtCashDisc.Text=="0")
				sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount        : ","0");
			else
			{
				//				if(DropCashDiscType.SelectedItem.Text.Equals("%"))
				//					sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount("+txtCashDisc.Text+DropCashDiscType.SelectedItem.Text+") : ","-"+tempcashdis.Value);
				//				else
				sw.WriteLine(info4,"Free Qty",TotalQtyfoe.ToString(),TotalfoeLtr.ToString(),"Cash Discount("+txtCashDiscType.Text+")   : ","-"+txtCashDisc.Text);
			}
			sw.WriteLine(info4,"","----------","----------","Vat Amount(@"+txtVatRate.Value+")    : ",txtVAT.Text);
			//sw.WriteLine(info4,"Total Qty",System.Convert.ToString(TotalQtyfoe+TotalQtyPack),System.Convert.ToString(System.Convert.ToDouble(txtlitertotal.Text)+TotalfoeLtr),"Net Amount           : ",txtNetAmount.Text);
			sw.WriteLine(info5,"",GenUtil.ConvertNoToWord(txtNetAmount.Text));
			sw.WriteLine(info6,"",CurrBal[0],"(INCLUDING CURRENT INVOICE AMOUNT)");
			sw.WriteLine(info7,"",txtRemark.Text);
			sw.Close();
		}
	}
}
