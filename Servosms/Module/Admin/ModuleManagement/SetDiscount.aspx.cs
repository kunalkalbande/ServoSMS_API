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
using Servosms.Sysitem.Classes ;
using RMG;
using DBOperations;

namespace Servosms.Module.Admin.ModuleManagement
{
	/// <summary>
	/// Summary description for SetDiscount.
	/// </summary>
	public partial class SetDiscount : System.Web.UI.Page
	{
		//protected System.Web.UI.HtmlControls.HtmlInputHidden tempEBPeriod;
		string uid;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=Session["User_Name"].ToString();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SetDiscount.aspx,Method:pageload"+ex.Message+"  EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);	
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges if user id admin then grant the access
				if(Session["User_ID"].ToString ()!="1001")
					Response.Redirect("AccessDeny.aspx",false);
				#endregion
				GetDiscount();
				//GetVat();
				PanPurchase.Visible=true;
				PanSales.Visible=false;
				PanModCen.Visible=false;
				//getfilldrop();
			}
		}

		public void getfilldrop()
		{
			drop1.Items.Clear();
			drop2.Items.Clear();
			drop3.Items.Clear();
			drop4.Items.Clear();
			drop5.Items.Clear();
			drop6.Items.Clear();
			drop7.Items.Clear();
			int j=5;
			int k=10;
			int l=15;
			for(int i=1;i<=10;i++)
			{
				drop1.Items.Add(i.ToString());
				drop2.Items.Add(j.ToString());
				drop3.Items.Add(j.ToString());
				drop4.Items.Add(k.ToString());
				drop5.Items.Add(k.ToString());
				drop6.Items.Add(l.ToString());
				drop7.Items.Add(l.ToString());
				j++;
				k++;
				l++;
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
		/// This method is not used.
		/// </summary>
		public void GetVat()
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select * from organisation");
			if(rdr.Read())
			{
				txtModVat.Text=rdr["Vat_Rate"].ToString();
				//txtSalesVat.Text=rdr["Vat_Rate"].ToString();
				//txtPurVat.Text=rdr["Vat_Rate"].ToString();
				chkModVat.Checked=true;
				//chkSalesVat.Checked=true;
				//chkPurVat.Checked=true;
                //CheckBoxCGST2.Checked = true;
                //CheckBoxSGST2.Checked = true;
                //CheckBoxcgst.Checked = true;
                //CheckBoxsgst.Checked = true;  
			}
			rdr.Close();
		}

		/// <summary>
		/// this method is used to insert or update the discount value in SetDis table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlCommand cmd = null;
				InventoryClass obj=new InventoryClass();
                int Early = 0, Servo = 0, FixedDis = 0, SalesSchDis = 0, SalesFleetOe = 0, ModSchDis = 0, ModFleetOe = 0, ModExcise = 0, ModEntry = 0, DisSales = 0, DisPurchase = 0, DisModVat = 0, VatSales = 0, VatPurchase = 0, VatModVat = 0, CashDisSales = 0, CashDisPurchase = 0, CashDisModVat = 0, cgstsales = 0, sgstsales = 0, cgstpur = 0,sgstpur=0 ;
				int SSRInc=0; //by vikas 02.06.09 
				double vatRate=0;
				string Tot_EBD="";
				string Period="";
				if(RadPurchase.Checked)
				{
					if(chkPurEarlyBird.Checked)
						Early=1;
					if(chkPurServostk.Checked)
						Servo=1;
					if(chkPurFixed.Checked)
						FixedDis=1;
					if(chkPurDis.Checked)
						DisPurchase=1;
                    //if(chkPurVat.Checked)
                    //	VatPurchase=1;
                    //               if (CheckBoxcgst.Checked)
                    //                   cgstpur = 1;
                    //               if (CheckBoxsgst.Checked)
                    //                   sgstpur = 1;
                    if (chkPurCashDis.Checked)
                        CashDisPurchase = 1;
                    //if(txtPurVat.Text.Trim()!="")
                    //	vatRate=double.Parse(txtPurVat.Text);

                    /* **************Add by vikas sharma ** date on 24.04.09*********/

                    if (txtPurEarlyBird.Text!="")
						Tot_EBD=txtPurEarlyBird.Text;
					else
						Tot_EBD="0.0";

					if(txtPurEarlyBird1.Text!="")
						Tot_EBD+="/"+txtPurEarlyBird1.Text;
					else
						Tot_EBD+="/"+"0.0";

					if(txtPurEarlyBird2.Text!="")
						Tot_EBD+="/"+txtPurEarlyBird2.Text;
					else
						Tot_EBD+="/"+"0.0";

					if(txtPurEarlyBird3.Text!="")
						Tot_EBD+="/"+txtPurEarlyBird3.Text;
					else
						Tot_EBD+="/"+"0.0";

					/* *********************end********************/
					/***********Add by vikas 6.11.2012***************/
					
					
					Period=drop1.SelectedValue.ToString().Trim();
					Period+=","+drop2.SelectedValue.ToString().Trim();
					Period+=","+drop3.SelectedValue.ToString().Trim();
					Period+=","+drop4.SelectedValue.ToString().Trim();
					Period+=","+drop5.SelectedValue.ToString().Trim();
					Period+=","+drop6.SelectedValue.ToString().Trim();
					Period+=","+drop7.SelectedValue.ToString().Trim();
					Period+=","+drop8.SelectedValue.ToString().Trim();
					/***********End***************/
				}
				else if(RadSales.Checked)
				{
					if(chkSalesSchDis.Checked)
						SalesSchDis=1;
					if(chkSalesFleetOe.Checked)
						SalesFleetOe=1;
					if(chkSalesDis.Checked)
						DisSales=1;
                    //if(chkSalesVat.Checked)
                    //	VatSales=1;
                    //               if (CheckBoxCGST2.Checked)
                    //                   cgstsales = 1;
                    //               if (CheckBoxSGST2.Checked)
                    //                   sgstsales = 1;
                    if (chkSalesCashDis.Checked)
                        CashDisSales = 1;
                    //if(txtSalesVat.Text.Trim()!="")
                    //	vatRate=double.Parse(txtSalesVat.Text);


                }
					//coment by vikas 02.06.09 else
				else if(RadModCen.Checked)  //add by vikas 02.06.09 
				{
					if(chkModSchDis.Checked)
						ModSchDis=1;
					if(chkModFleetOe.Checked)
						ModFleetOe=1;
					if(chkModExcise.Checked)
						ModExcise=1;
					if(chkModEntryTax.Checked)
						ModEntry=1;
					if(chkModDis.Checked)
						DisModVat=1;
					if(chkModVat.Checked)
						VatModVat=1;
					if(chkModCashDis.Checked)
						CashDisModVat=1;
					if(txtModVat.Text.Trim()!="")
						vatRate=double.Parse(txtModVat.Text);
				}
				else
				{
					if(Checkappl.Checked)
						SSRInc=1;
				}
				SqlDataReader SqlDtr=obj.GetRecordSet("select * from SetDis");
				if(SqlDtr.Read())
				{
					con.Open();
					if(RadPurchase.Checked)
						// Comment By vikas sharma 24.04.09 cmd = new SqlCommand("update SetDis set EarlyBird='"+txtPurEarlyBird.Text+"',EarlyStatus='"+Early+"',Servostk='"+txtPurServostk.Text+"',ServoStatus='"+Servo+"',Fixeddis='"+txtPurFixed.Text+"',FixedStatus='"+FixedDis+"',DiscountPurchase='"+txtPurDis.Text+"',DiscountPurchaseStatus='"+DisPurchase+"',VatPurchase='"+txtPurVat.Text+"',VatPurchaseStatus='"+VatPurchase+"',CashDisPurchase='"+txtPurCashDis.Text+"',CashDisPurchaseStatus='"+CashDisPurchase+"',CashDisLtrPurchase='"+DropPurCashDisRs.SelectedItem.Text+"',DisLtrPurchase='"+DropPurDisRs.SelectedItem.Text+"'",con);
						//coment by vikas 6.11.2012  cmd = new SqlCommand("update SetDis set EarlyBird='"+Tot_EBD+"',EarlyStatus='"+Early+"',Servostk='"+txtPurServostk.Text+"',ServoStatus='"+Servo+"',Fixeddis='"+txtPurFixed.Text+"',FixedStatus='"+FixedDis+"',DiscountPurchase='"+txtPurDis.Text+"',DiscountPurchaseStatus='"+DisPurchase+"',VatPurchase='"+txtPurVat.Text+"',VatPurchaseStatus='"+VatPurchase+"',CashDisPurchase='"+txtPurCashDis.Text+"',CashDisPurchaseStatus='"+CashDisPurchase+"',CashDisLtrPurchase='"+DropPurCashDisRs.SelectedItem.Text+"',DisLtrPurchase='"+DropPurDisRs.SelectedItem.Text+"',EarlyDisLtrPurchase='"+DropPurEarlyRs.SelectedItem.Text+"'",con);
						//cmd = new SqlCommand("update SetDis set EarlyBird='"+Tot_EBD+"',EarlyStatus='"+Early+"',Servostk='"+txtPurServostk.Text+"',ServoStatus='"+Servo+"',Fixeddis='"+txtPurFixed.Text+"',FixedStatus='"+FixedDis+"',DiscountPurchase='"+txtPurDis.Text+"',DiscountPurchaseStatus='"+DisPurchase+"',IGSTPurchase='"+txtPurVat.Text+ "',IGSTPurchaseStatus='" + VatPurchase+"',CashDisPurchase='"+txtPurCashDis.Text+"',CashDisPurchaseStatus='"+CashDisPurchase+"',CashDisLtrPurchase='"+DropPurCashDisRs.SelectedItem.Text+"',DisLtrPurchase='"+DropPurDisRs.SelectedItem.Text+"',EarlyDisLtrPurchase='"+DropPurEarlyRs.SelectedItem.Text+"',EarlyBird_Period= '"+Period+"',CGSTPurchase='"+TextBoxcgst.Text+"',SGSTPurchase='"+TextBoxsgst.Text+ "',CGSTPurchaseStatus='"+ +cgstpur+"',SGSTPurchaseStatus='"+sgstpur+"'", con);
                        cmd = new SqlCommand("update SetDis set EarlyBird='" + Tot_EBD + "',EarlyStatus='" + Early + "',Servostk='" + txtPurServostk.Text + "',ServoStatus='" + Servo + "',Fixeddis='" + txtPurFixed.Text + "',FixedStatus='" + FixedDis + "',DiscountPurchase='" + txtPurDis.Text + "',DiscountPurchaseStatus='" + DisPurchase + "',IGSTPurchaseStatus='" + VatPurchase + "',CashDisPurchase='" + txtPurCashDis.Text + "',CashDisPurchaseStatus='" + CashDisPurchase + "',CashDisLtrPurchase='" + DropPurCashDisRs.SelectedItem.Text + "',DisLtrPurchase='" + DropPurDisRs.SelectedItem.Text + "',EarlyDisLtrPurchase='" + DropPurEarlyRs.SelectedItem.Text + "',EarlyBird_Period= '" + Period + "',CGSTPurchaseStatus='" + +cgstpur + "',SGSTPurchaseStatus='" + sgstpur + "'", con);
					else if (RadSales.Checked)
                        //cmd = new SqlCommand("update SetDis set SchDis='"+txtSalesSchDis.Text+"',SchDisStatus='"+SalesSchDis+"',FleetOe='"+txtSalesFleetOe.Text+"',FleetOeStatus='"+SalesFleetOe+"',DiscountSales='"+txtSalesDis.Text+"',DiscountSalesStatus='"+DisSales+ "',IGSTSales='" + txtSalesVat.Text + "',IGSTSalesStatus='" + VatSales+"',CashDisSales='"+txtSalesCashDis.Text+"',CashDisSalesStatus='"+CashDisSales+"',CashDisLtrSales='"+DropSalesCashDisRs.SelectedItem.Text+"',DisLtrSales='"+DropSalesDisRs.SelectedItem.Text+"',CGSTSales='"+TextBoxCGST2.Text+"',SGSTSales='"+TextBoxSGST2.Text+ "',CGSTSalesStatus='"+ cgstsales+"',SGSTSalesStatus='"+sgstsales+"'", con);
                        cmd = new SqlCommand("update SetDis set SchDis='" + txtSalesSchDis.Text + "',SchDisStatus='" + SalesSchDis + "',FleetOe='" + txtSalesFleetOe.Text + "',FleetOeStatus='" + SalesFleetOe + "',DiscountSales='" + txtSalesDis.Text + "',DiscountSalesStatus='" + DisSales + "',IGSTSalesStatus='" + VatSales + "',CashDisSales='" + txtSalesCashDis.Text + "',CashDisSalesStatus='" + CashDisSales + "',CashDisLtrSales='" + DropSalesCashDisRs.SelectedItem.Text + "',DisLtrSales='" + DropSalesDisRs.SelectedItem.Text + "',CGSTSalesStatus='" + cgstsales + "',SGSTSalesStatus='" + sgstsales + "'", con);
                    //else //coment by vikas 2.06.09 
                    else if(RadModCen.Checked)
						cmd = new SqlCommand("update SetDis set SchDis='"+txtModSchDis.Text+"',SchDisStatus='"+ModSchDis+"',FleetOe='"+txtModFleetOe.Text+"',FleetOeStatus='"+ModFleetOe+"',Excise='"+txtModExcise.Text+"',ExciseStatus='"+ModExcise+"',EntryTax='"+txtModEntryTax.Text+"',EntrytaxStatus='"+ModEntry+"',DiscountModVat='"+txtModDis.Text+"',DiscountModVatStatus='"+DisModVat+"',VatModVat='"+txtModVat.Text+"',VatModVatStatus='"+VatModVat+"',CashDismodvat='"+txtModCashDis.Text+"',CashDismodvatStatus='"+CashDisModVat+"',CashDisLtrmodvat='"+DropModCashDisRs.SelectedItem.Text+"',DisLtrModVat='"+DropModDisRs.SelectedItem.Text+"',ExciseLtr='"+DropModExciseRs.SelectedItem.Text+"',EntryTaxLtr='"+DropModEntryTaxRs.SelectedItem.Text+"'",con);
					else  //add by vikas 02.06.09 
						cmd = new SqlCommand("update SetDis set SSRIncentive='"+txtdisname.Text+"',SSRIncentiveStatus='"+SSRInc+"'",con); //add by vikas 02.06.09 
					cmd.ExecuteNonQuery();
					MessageBox.Show("Discount Updated Successfully");
					cmd.Dispose();
					con.Close();
				}
				else
				{
					con.Open();
					if(RadPurchase.Checked)
                        //Comment by vikas sharma 24.04.09 cmd = new SqlCommand("insert into SetDis(EarlyBird,EarlyStatus,servostk,servostatus,Fixeddis,fixedstatus,discountPurchase,discountPurchasestatus,vatPurchase,vatpurchasestatus,cashdispurchase,cashdisPurchasestatus,CashDisLtrpurchase,DisLtrPurchase) values('"+txtPurEarlyBird.Text+"','"+Early+"','"+txtPurServostk.Text+"','"+Servo+"','"+txtPurFixed.Text+"','"+FixedDis+"','"+txtPurDis.Text+"','"+DisPurchase+"','"+txtPurVat.Text+"','"+VatPurchase+"','"+txtPurCashDis.Text+"','"+CashDisPurchase+"','"+DropPurCashDisRs.SelectedItem.Text+"','"+DropPurDisRs.SelectedItem.Text+"')",con);
                        //coment by vikas 6.11.2012 cmd = new SqlCommand("insert into SetDis(EarlyBird,EarlyStatus,servostk,servostatus,Fixeddis,fixedstatus,discountPurchase,discountPurchasestatus,vatPurchase,vatpurchasestatus,cashdispurchase,cashdisPurchasestatus,CashDisLtrpurchase,DisLtrPurchase,EarlyDisLtrPurchase) values('"+Tot_EBD+"','"+Early+"','"+txtPurServostk.Text+"','"+Servo+"','"+txtPurFixed.Text+"','"+FixedDis+"','"+txtPurDis.Text+"','"+DisPurchase+"','"+txtPurVat.Text+"','"+VatPurchase+"','"+txtPurCashDis.Text+"','"+CashDisPurchase+"','"+DropPurCashDisRs.SelectedItem.Text+"','"+DropPurDisRs.SelectedItem.Text+"','"+DropPurEarlyRs.SelectedItem.Text+"')",con);
                        //cmd = new SqlCommand("insert into SetDis(EarlyBird,EarlyStatus,servostk,servostatus,Fixeddis,fixedstatus,discountPurchase,discountPurchasestatus,IGSTPurchase,IGSTPurchaseStatus,cashdispurchase,cashdisPurchasestatus,CashDisLtrpurchase,DisLtrPurchase,EarlyDisLtrPurchase,EarlyBird_Period,CGSTPurchase,SGSTPurchase,CGSTPurchaseStatus,SGSTPurchaseStatus) values('" + Tot_EBD+"','"+Early+"','"+txtPurServostk.Text+"','"+Servo+"','"+txtPurFixed.Text+"','"+FixedDis+"','"+txtPurDis.Text+"','"+DisPurchase+"','"+txtPurVat.Text+"','"+VatPurchase+"','"+txtPurCashDis.Text+"','"+CashDisPurchase+"','"+DropPurCashDisRs.SelectedItem.Text+"','"+DropPurDisRs.SelectedItem.Text+"','"+DropPurEarlyRs.SelectedItem.Text+"','"+Period+"','"+TextBoxcgst.Text+"','"+TextBoxsgst.Text+"','"+cgstpur+"','"+sgstpur+"')",con);
                        cmd = new SqlCommand("insert into SetDis(EarlyBird,EarlyStatus,servostk,servostatus,Fixeddis,fixedstatus,discountPurchase,discountPurchasestatus,IGSTPurchaseStatus,cashdispurchase,cashdisPurchasestatus,CashDisLtrpurchase,DisLtrPurchase,EarlyDisLtrPurchase,EarlyBird_Period,CGSTPurchaseStatus,SGSTPurchaseStatus) values('" + Tot_EBD + "','" + Early + "','" + txtPurServostk.Text + "','" + Servo + "','" + txtPurFixed.Text + "','" + FixedDis + "','" + txtPurDis.Text + "','" + DisPurchase + "','" + VatPurchase + "','" + txtPurCashDis.Text + "','" + CashDisPurchase + "','" + DropPurCashDisRs.SelectedItem.Text + "','" + DropPurDisRs.SelectedItem.Text + "','" + DropPurEarlyRs.SelectedItem.Text + "','" + Period + "','" + cgstpur + "','" + sgstpur + "')", con);
                    else if(RadSales.Checked)
                        //cmd = new SqlCommand("insert into SetDis(schdis,schdisstatus,fleetoe,fleetoestatus,discountsales,discountsalesstatus,IGSTSales,IGSTSalesStatus,cashdissales,cashdissalesstatus,CashDisLtrsales,DisLtrSales,CGSTSales,SGSTSales,CGSTSalesStatus,SGSTSalesStatus) values('" + txtSalesSchDis.Text+"','"+SalesSchDis+"','"+txtSalesFleetOe.Text+"','"+SalesFleetOe+"','"+txtSalesDis.Text+"','"+DisSales+"','"+txtSalesVat.Text+"','"+VatSales+"','"+txtSalesCashDis.Text+"','"+CashDisSales+"','"+DropSalesCashDisRs.SelectedItem.Text+"','"+DropSalesDisRs.SelectedItem.Text+"','"+TextBoxCGST2.Text+"','"+TextBoxSGST2.Text+"','"+cgstsales+"','"+sgstsales+"')",con);
                        cmd = new SqlCommand("insert into SetDis(schdis,schdisstatus,fleetoe,fleetoestatus,discountsales,discountsalesstatus,IGSTSalesStatus,cashdissales,cashdissalesstatus,CashDisLtrsales,DisLtrSales,CGSTSalesStatus,SGSTSalesStatus) values('" + txtSalesSchDis.Text + "','" + SalesSchDis + "','" + txtSalesFleetOe.Text + "','" + SalesFleetOe + "','" + txtSalesDis.Text + "','" + DisSales + "','" + VatSales + "','" + txtSalesCashDis.Text + "','" + CashDisSales + "','" + DropSalesCashDisRs.SelectedItem.Text + "','" + DropSalesDisRs.SelectedItem.Text + "','" + cgstsales + "','" + sgstsales + "')", con);
                    // else //coment by vikas 02.06.09 
                    else if(RadModCen.Checked)
						cmd = new SqlCommand("insert into SetDis(schdis,schdisstatus,fleetoe,fleetoestatus,Excise,ExciseStatus,EntryTax,EntryTaxStatus,discountmodvat,discountmodvatstatus,vatmodvat,vatmodvatstatus,cashdisModvat,cashdismodvatstatus,CashDisLtrmodvat,DisLtrModVat,ExciseLtr,EntryTaxLtr) values('"+txtModSchDis.Text+"','"+ModSchDis+"','"+txtModFleetOe.Text+"','"+ModFleetOe+"','"+txtModExcise.Text+"','"+ModExcise+"','"+txtModEntryTax.Text+"','"+ModEntry+"','"+txtModDis.Text+"','"+DisModVat+"','"+txtModVat.Text+"','"+VatModVat+"','"+txtModCashDis.Text+"','"+CashDisModVat+"','"+DropModCashDisRs.SelectedItem.Text+"','"+DropModDisRs.SelectedItem.Text+"','"+DropModExciseRs.SelectedItem.Text+"','"+DropModEntryTaxRs.SelectedItem.Text+"')",con);
					else
						cmd = new SqlCommand("insert into SetDis(SSRIncentive,SSRIncentiveStatus) values('"+txtdisname.Text+"',"+SSRInc+"')",con);
					//cmd = new SqlCommand("insert into SetDis values('"+txtPurEarlyBird.Text+"','"+Early+"','"+txtPurServostk.Text+"','"+Servo+"','"+txtPurFixed.Text+"','"+FixedDis+"')",con);
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					con.Close();
					MessageBox.Show("Discount Save Successfully");
				}
				if(!RadSSrInc.Checked)  //add by vikas 02.06.09
				{
					con.Open();
					cmd = new SqlCommand("update Organisation set Vat_Rate='"+vatRate+"'",con);
					cmd.ExecuteNonQuery();
					con.Close();
				}
				GetDiscount();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SetDiscount.aspx,Method:btnUpdate ,  EXCEPTION"+ex.Message+"   "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the all discount when page is load and check the radio button also.
		/// </summary>
		public void GetDiscount()
		{
			try
			{

				//getfilldrop();
				InventoryClass obj=new InventoryClass();
				SqlDataReader SqlDtr=obj.GetRecordSet("select * from SetDis");
				string Get_EDB="";
				if(SqlDtr.Read())
				{
					if(RadPurchase.Checked)
					{

						// Comment By vikas sharma 24.04.09 txtPurEarlyBird.Text = SqlDtr["EarlyBird"].ToString();
						
						/* *********start**Add by vikas sharma 24.04.09***/
						Get_EDB = SqlDtr["EarlyBird"].ToString();
						string[] All_EDB=Get_EDB.Split('/');
						txtPurEarlyBird.Text=All_EDB[0];
						txtPurEarlyBird1.Text=All_EDB[1];
						txtPurEarlyBird2.Text=All_EDB[2];
						txtPurEarlyBird3.Text=All_EDB[3];
						DropPurEarlyRs.SelectedIndex=DropPurEarlyRs.Items.IndexOf(DropPurEarlyRs.Items.FindByValue(SqlDtr["EarlyDisLtrPurchase"].ToString()));

						/* *********end***********************************/

						txtPurServostk.Text = SqlDtr["Servostk"].ToString();
						txtPurFixed.Text = SqlDtr["FixedDis"].ToString();
						txtPurDis.Text = SqlDtr["DiscountPurchase"].ToString();
                        //txtPurVat.Text = SqlDtr["IGSTPurchase"].ToString();
                        //TextBoxcgst.Text = SqlDtr["CGSTPurchase"].ToString();
                        //TextBoxsgst.Text = SqlDtr["SGSTPurchase"].ToString();
                        txtPurCashDis.Text = SqlDtr["CashDisPurchase"].ToString();
						DropPurCashDisRs.SelectedIndex=DropPurCashDisRs.Items.IndexOf(DropPurCashDisRs.Items.FindByValue(SqlDtr["CashDisLtrPurchase"].ToString()));
						DropPurDisRs.SelectedIndex=DropPurDisRs.Items.IndexOf(DropPurDisRs.Items.FindByValue(SqlDtr["DisLtrPurchase"].ToString()));
						if(SqlDtr["EarlyStatus"].ToString()=="1")
							chkPurEarlyBird.Checked=true;
						else
							chkPurEarlyBird.Checked=false;
						if(SqlDtr["ServoStatus"].ToString()=="1")
							chkPurServostk.Checked=true;
						else
							chkPurServostk.Checked=false;
						if(SqlDtr["FixedStatus"].ToString()=="1")
							chkPurFixed.Checked=true;
						else
							chkPurFixed.Checked=false;
						if(SqlDtr["DiscountPurchaseStatus"].ToString()=="1")
							chkPurDis.Checked=true;
						else
							chkPurDis.Checked=false;
                        if (SqlDtr["CashDisPurchaseStatus"].ToString() == "1")
                            chkPurCashDis.Checked = true;
                        else
                            chkPurCashDis.Checked = false;
                        //if(SqlDtr["IGSTPurchaseStatus"].ToString()=="1")
                        //	chkPurVat.Checked=true;
                        //else
                        //	chkPurVat.Checked=false;

                        //                  if (SqlDtr["CGSTSalesStatus"].ToString() == "1")
                        //                      CheckBoxcgst.Checked = true;
                        //                  else
                        //                      CheckBoxcgst.Checked = false;
                        //                  if (SqlDtr["SGSTSalesStatus"].ToString() == "1")
                        //                      CheckBoxsgst.Checked = true;
                        //                  else
                        //                      CheckBoxsgst.Checked = false;

                        /***********Add by vikas 5.11.2012*******************/
                        string EB_period="";
						

						if(SqlDtr["EarlyBird_Period"].ToString()!=null && SqlDtr["EarlyBird_Period"].ToString()!="")
						{
							EB_period=SqlDtr["EarlyBird_Period"].ToString().Trim();
							//tempEBPeriod.Value=SqlDtr["EarlyBird_Period"].ToString().Trim();
						}

						 string[] arrperiod=EB_period.Split(new char[] {','},EB_period.Length);
						drop1.SelectedIndex=(drop1.Items.IndexOf((drop1.Items.FindByText(arrperiod[0].ToString().Trim()))));
						drop2.SelectedIndex=(drop2.Items.IndexOf((drop2.Items.FindByText(arrperiod[1].ToString().Trim()))));
						drop3.SelectedIndex=(drop3.Items.IndexOf((drop3.Items.FindByValue(arrperiod[2].ToString().Trim()))));
						drop4.SelectedIndex=(drop4.Items.IndexOf((drop4.Items.FindByValue(arrperiod[3].ToString().Trim()))));
						drop5.SelectedIndex=(drop5.Items.IndexOf((drop5.Items.FindByValue(arrperiod[4].ToString().Trim()))));
						drop6.SelectedIndex=(drop6.Items.IndexOf((drop6.Items.FindByValue(arrperiod[5].ToString().Trim()))));
						drop7.SelectedIndex=(drop7.Items.IndexOf((drop7.Items.FindByValue(arrperiod[6].ToString().Trim()))));
						drop8.SelectedIndex=(drop8.Items.IndexOf((drop8.Items.FindByValue(arrperiod[7].ToString().Trim()))));
						
						/***********End*******************/
					}
					else if(RadSales.Checked)
					{
						txtSalesSchDis.Text = SqlDtr["SchDis"].ToString();
						txtSalesFleetOe.Text = SqlDtr["FleetOe"].ToString();
						txtSalesDis.Text = SqlDtr["DiscountSales"].ToString();
                        //txtSalesVat.Text = SqlDtr["IGSTSales"].ToString();
                        //TextBoxCGST2.Text = SqlDtr["CGSTSales"].ToString();
                        //TextBoxSGST2.Text = SqlDtr["SGSTSales"].ToString();
                        txtSalesCashDis.Text = SqlDtr["CashDisSales"].ToString();
						DropSalesCashDisRs.SelectedIndex=DropSalesCashDisRs.Items.IndexOf(DropSalesCashDisRs.Items.FindByValue(SqlDtr["CashDisLtrSales"].ToString()));
						DropSalesDisRs.SelectedIndex=DropSalesDisRs.Items.IndexOf(DropSalesDisRs.Items.FindByValue(SqlDtr["DisLtrSales"].ToString()));
						if(SqlDtr["SchDisStatus"].ToString()=="1")
							chkSalesSchDis.Checked=true;
						else
							chkSalesSchDis.Checked=false;
						if(SqlDtr["FleetOeStatus"].ToString()=="1")
							chkSalesFleetOe.Checked=true;
						else
							chkSalesFleetOe.Checked=false;
						if(SqlDtr["DiscountSalesStatus"].ToString()=="1")
							chkSalesDis.Checked=true;
						else
							chkSalesDis.Checked=false;
                        //if(SqlDtr["IGSTSalesStatus"].ToString()=="1")
                        //	chkSalesVat.Checked=true;
                        //else
                        //	chkSalesVat.Checked=false;
                        if (SqlDtr["CashDisSalesStatus"].ToString() == "1")
                            chkSalesCashDis.Checked = true;
                        else
                            chkSalesCashDis.Checked = false;
                        //                  if (SqlDtr["CGSTSalesStatus"].ToString() == "1")
                        //                      CheckBoxCGST2.Checked = true;
                        //                  else
                        //                      CheckBoxCGST2.Checked = false;
                        //                  if (SqlDtr["SGSTSalesStatus"].ToString() == "1")
                        //                      CheckBoxSGST2.Checked = true;
                        //                  else
                        //                      CheckBoxSGST2.Checked = false;
                    }
                    // else //coment by vikas 02.06.09 
                    else if(RadModCen.Checked)
					{
						txtModExcise.Text = SqlDtr["Excise"].ToString();
						txtModEntryTax.Text = SqlDtr["EntryTax"].ToString();
						txtModSchDis.Text = SqlDtr["SchDis"].ToString();
						txtModFleetOe.Text = SqlDtr["FleetOe"].ToString();
						txtModDis.Text = SqlDtr["DiscountModVat"].ToString();
					//	txtModVat.Text = SqlDtr["VatModVat"].ToString();//TOdo replace with new Value
						txtModCashDis.Text = SqlDtr["CashDisModvat"].ToString();
						DropModCashDisRs.SelectedIndex=DropModCashDisRs.Items.IndexOf(DropModCashDisRs.Items.FindByValue(SqlDtr["CashDisLtrModVat"].ToString()));
						DropModDisRs.SelectedIndex=DropModDisRs.Items.IndexOf(DropModDisRs.Items.FindByValue(SqlDtr["DisLtrModVat"].ToString()));
						DropModExciseRs.SelectedIndex=DropModExciseRs.Items.IndexOf(DropModExciseRs.Items.FindByValue(SqlDtr["CashDisLtr"].ToString()));
						DropModEntryTaxRs.SelectedIndex=DropModEntryTaxRs.Items.IndexOf(DropModEntryTaxRs.Items.FindByValue(SqlDtr["DisLtr"].ToString()));
						if(SqlDtr["ExciseStatus"].ToString()=="1")
							chkModExcise.Checked=true;
						else
							chkModExcise.Checked=false;
						if(SqlDtr["EntryTaxStatus"].ToString()=="1")
							chkModEntryTax.Checked=true;
						else
							chkModEntryTax.Checked=false;
						if(SqlDtr["SchDisStatus"].ToString()=="1")
							chkModSchDis.Checked=true;
						else
							chkModSchDis.Checked=false;
						if(SqlDtr["FleetOeStatus"].ToString()=="1")
							chkModFleetOe.Checked=true;
						else
							chkModFleetOe.Checked=false;
						if(SqlDtr["DiscountModVatStatus"].ToString()=="1")
							chkModDis.Checked=true;
						else
							chkModDis.Checked=false;
						if(SqlDtr["VatModVatStatus"].ToString()=="1")
							chkModVat.Checked=true;
						else
							chkModVat.Checked=false;
						if(SqlDtr["CashDisModVatStatus"].ToString()=="1")
							chkModCashDis.Checked=true;
						else
							chkModCashDis.Checked=false;
					}
					else													  //add by vikas 02.06.09 
					{
						txtdisname.Text=SqlDtr["SSRIncentive"].ToString();

						if(SqlDtr["SSRIncentiveStatus"].ToString()=="1")      //add by vikas 02.06.09 
							Checkappl.Checked=true;                           //add by vikas 02.06.09 
						else                                                  //add by vikas 02.06.09 
							Checkappl.Checked=false;                          //add by vikas 02.06.09 
					}

				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SetDiscount.aspx,Method:GetDiscount() ,  EXCEPTION"+ex.Message+"   "+uid);
			}
		}

		/// <summary>
		/// This method is used to hide the information from user only show the value who is selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RadSales_CheckedChanged(object sender, System.EventArgs e)
		{
			/*if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
			}
			else
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
			}
			*/

			if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
				panSSRInc.Visible=false;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else if(RadModCen.Checked==true)
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=true;
			}

			GetDiscount();
		}

		/// <summary>
		/// This method is used to hide the information from user only show the value who is selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RadPurchase_CheckedChanged(object sender, System.EventArgs e)
		{
			/* by vikas 02.06.09 if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
			}
			else
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
			}*/
			if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
				panSSRInc.Visible=false;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else if(RadModCen.Checked==true)
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=true;
			}

			GetDiscount();
		}

		/// <summary>
		/// This method is used to hide the information from user only show the value who is selected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void RadModCen_CheckedChanged(object sender, System.EventArgs e)
		{
			/*coment by vikas 02.06.09 if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
			}
			else
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
			}*/

			if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
				panSSRInc.Visible=false;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else if(RadModCen.Checked==true)
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=true;
			}

			GetDiscount();
		}

		private void txtSalesCashDis_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void RadSSrInc_CheckedChanged(object sender, System.EventArgs e)
		{
			if(RadPurchase.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=true;
				panSSRInc.Visible=false;
			}
			else if(RadSales.Checked==true)
			{
				PanModCen.Visible=false;
				PanSales.Visible=true;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else if(RadModCen.Checked==true)
			{
				PanModCen.Visible=true;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=false;
			}
			else
			{
				PanModCen.Visible=false;
				PanSales.Visible=false;
				PanPurchase.Visible=false;
				panSSRInc.Visible=true;
			}
			GetDiscount();
		}
	}
}
