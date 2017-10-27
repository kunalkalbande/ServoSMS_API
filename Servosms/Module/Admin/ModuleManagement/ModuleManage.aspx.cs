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

namespace Servosms.Module.Admin.ModuleManagement
{
	/// <summary>
	/// Summary description for ModuleManage.
	/// </summary>
	public partial class ModuleManage : System.Web.UI.Page
	{
		string uid;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);

		/// <summary>
		/// Put user code to initialize the page here
		/// This method is used for setting the Session variable for userId
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
				CreateLogFiles.ErrorLog("Form:ModuleManage.aspx,Method:pageload"+ ex.Message+"  EXCEPTION "+"   "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				#region Check Privileges if user id admin then grant the access
				if(Session["User_ID"].ToString ()!="1001")
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				#endregion				
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
		/// This method is used to update the all Ledger balance of AccountsLedgerTable and CustomerLedgerTable in sequencialy.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			string Cust_ID="0";
			string Ledger_ID="0";
			try
			{
				if(chkCustBal.Checked!=false || chkStock.Checked!=false)
				{
					InventoryClass obj = new InventoryClass();
					InventoryClass obj1 = new InventoryClass();
					SqlCommand cmd;
					int Flag=0;
					string str="",str1="";
					SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					SqlDataReader rdr1=null,rdr=null;
					if(chkStock.Checked)
					{
						str="select Prod_ID from Products";
						rdr=obj.GetRecordSet(str);
						while(rdr.Read())
						{
							str1="select * from Stock_Master where Productid='"+rdr["Prod_ID"].ToString()+"' order by Stock_date";
							rdr1=obj1.GetRecordSet(str1);
							double OS=0,CS=0,k=0;
							while(rdr1.Read())
							{
								Flag=1;
								if(k==0)
								{
									OS=double.Parse(rdr1["opening_stock"].ToString());
									k++;
								}
								else
									OS=CS;
								CS=OS+double.Parse(rdr1["receipt"].ToString())-(double.Parse(rdr1["sales"].ToString())+double.Parse(rdr1["salesfoc"].ToString()));
								Con.Open();
								cmd = new SqlCommand("update Stock_Master set opening_stock='"+OS.ToString()+"', Closing_Stock='"+CS.ToString()+"' where ProductID='"+rdr1["Productid"].ToString()+ "' and Stock_Date=Convert(datetime,'" + GenUtil.str2DDMMYYYY(rdr1["stock_date"].ToString()) + "',103)",Con);
								cmd.ExecuteNonQuery();
								cmd.Dispose();
								Con.Close();
							}
							rdr1.Close();
						}
						rdr.Close();
					}

					if(chkCustBal.Checked)
					{
						/*16.4.2013 str1 = "select Ledger_ID from Ledger_Master";
						rdr1 = obj1.GetRecordSet(str1);
						while(rdr1.Read())
						{
							str = "select * from AccountsLedgerTable where Ledger_ID='"+rdr1["Ledger_ID"].ToString()+"' order by entry_date";
							rdr = obj.GetRecordSet(str);
							double Bal=0;
							string BalType="";
							int i=0;
							Flag=1;
							while(rdr.Read())
							{
								if(i==0)
								{
									BalType=rdr["Bal_Type"].ToString();
									i++;
								}
								if(double.Parse(rdr["Credit_Amount"].ToString())!=0)
								{
									if(BalType=="Cr")
									{
										Bal+=double.Parse(rdr["Credit_Amount"].ToString());
										BalType="Cr";
									}
									else
									{
										Bal-=double.Parse(rdr["Credit_Amount"].ToString());
										if(Bal<0)
										{
											Bal=double.Parse(Bal.ToString().Substring(1));
											BalType="Cr";
										}
										else
											BalType="Dr";
									}
								}
								else if(double.Parse(rdr["Debit_Amount"].ToString())!=0)
								{
									if(BalType=="Dr")
										Bal+=double.Parse(rdr["Debit_Amount"].ToString());
									else
									{
										Bal-=double.Parse(rdr["Debit_Amount"].ToString());
										if(Bal<0)
										{
											Bal=double.Parse(Bal.ToString().Substring(1));
											BalType="Dr";
										}
										else
											BalType="Cr";
									}
								}
								Con.Open();
								cmd = new SqlCommand("update AccountsLedgerTable set Balance='"+Bal.ToString()+"',Bal_Type='"+BalType+"' where Ledger_ID='"+rdr["Ledger_ID"].ToString()+"' and Particulars='"+rdr["Particulars"].ToString()+"' ",Con);
								cmd.ExecuteNonQuery();
								cmd.Dispose();
								Con.Close();
							}
							rdr.Close();
						}
						rdr1.Close();

						str1 = "select Cust_ID from Customer";
						rdr1 = obj1.GetRecordSet(str1);
						while(rdr1.Read())
						{
							//dbobj.SelectQuery("select * from CustomerLedgerTable where CustID='"+rdr1["Cust_ID"].ToString()+"' order by entrydate",ref rdr);
							str = "select * from CustomerLedgerTable where CustID='"+rdr1["Cust_ID"].ToString()+"' order by entrydate";
							rdr = obj.GetRecordSet(str);
							double Bal=0;
							string BalType="";
							int i=0;
							while(rdr.Read())
							{
								if(i==0)
								{
									BalType=rdr["BalanceType"].ToString();
									i++;
								}
								if(double.Parse(rdr["CreditAmount"].ToString())!=0)
								{
									if(BalType=="Cr.")
									{
										Bal+=double.Parse(rdr["CreditAmount"].ToString());
										BalType="Cr.";
									}
									else
									{
										Bal-=double.Parse(rdr["CreditAmount"].ToString());
										if(Bal<0)
										{
											Bal=double.Parse(Bal.ToString().Substring(1));
											BalType="Cr.";
										}
										else
											BalType="Dr.";
									}
								}
								else if(double.Parse(rdr["DebitAmount"].ToString())!=0)
								{
									if(BalType=="Dr.")
										Bal+=double.Parse(rdr["DebitAmount"].ToString());
									else
									{
										Bal-=double.Parse(rdr["DebitAmount"].ToString());
										if(Bal<0)
										{
											Bal=double.Parse(Bal.ToString().Substring(1));
											BalType="Dr.";
										}
										else
											BalType="Cr.";
									}
								}
								Con.Open();
								cmd = new SqlCommand("update CustomerLedgerTable set Balance='"+Bal.ToString()+"',BalanceType='"+BalType+"' where CustID='"+rdr["CustID"].ToString()+"' and Particular='"+rdr["Particular"].ToString()+"' ",Con);
								cmd.ExecuteNonQuery();
								cmd.Dispose();
								Con.Close();
							}
							rdr.Close();
						}
						rdr1.Close(); End*/

					
						
						/******************Add by vikas 6.3.2013**********************/
						object op=null;
						int x=0;
						System.Data.SqlClient.SqlDataReader rd=null;
						string sql1="";
						
						str1 = "select * from Ledger_Master order by ledger_id";
						rdr1 = obj1.GetRecordSet(str1);
						while(rdr1.Read())
						{
							str = "select * from AccountsLedgerTable where Ledger_ID='"+rdr1["Ledger_ID"].ToString()+"' and particulars='Opening Balance'";
							rdr = obj.GetRecordSet(str);
							double Bal=0;
							string BalType="";
							int i=0;
							Flag=1;
							while(rdr.Read())
							{
								try
								{
									Ledger_ID=rdr1["Ledger_ID"].ToString()+" : "+rdr1["Ledger_Name"].ToString();

									BalType=rdr["Bal_Type"].ToString();
									if(BalType=="Cr")
									{
										Bal=double.Parse(rdr["Credit_Amount"].ToString());
									}
									else if(BalType=="Dr")
									{
										Bal=double.Parse(rdr["Debit_Amount"].ToString());
									}

									dbobj.ExecProc(OprType.Update,"UpdateAccountsLedger",ref op,"@Ledger_ID",rdr["Ledger_ID"].ToString(),"@Amount",Bal.ToString(),"@Type",BalType.ToString());
								}
								catch(Exception ex)
								{
									//MessageBox.Show("Ledger Name : "+Ledger_ID.ToString());
								}
							}
							rdr.Close();
						}
						rdr1.Close();

						/*sql1="select Ledger_ID from Ledger_Master";
						dbobj.SelectQuery(sql1,ref rd);
						while(rd.Read())
							dbobj.ExecProc(OprType.Insert,"UpdateAccountsLedger ",ref op,"@Ledger_ID",rd["Ledger_ID"].ToString(),"@Amount","0","@Type","Dr");
							//exec UpdateAccountsLedger @Ledger_ID,@Op_Balance,@Balance_Type
						rdr.Close();*/


						str1 = "select * from Customer";
						rdr1 = obj1.GetRecordSet(str1);
						while(rdr1.Read())
						{
							Cust_ID=rdr1["Cust_ID"].ToString()+":"+rdr1["Cust_Name"].ToString();

							str = "select * from CustomerLedgerTable where CustID='"+rdr1["Cust_ID"].ToString()+"' and particular='Opening Balance'";
							rdr = obj.GetRecordSet(str);
							double Bal=0;
							string BalType="";
							int i=0;
							while(rdr.Read())
							{
								try
								{
									BalType=rdr["BalanceType"].ToString();
									if(BalType=="Cr.")
									{
										Bal=double.Parse(rdr["CreditAmount"].ToString());
									}
									else if(BalType=="Dr.")
									{
										Bal=double.Parse(rdr["DebitAmount"].ToString());
									}

									dbobj.ExecProc(OprType.Update,"UpdateBalance",ref op,"@Cust_ID",rdr["custid"].ToString(),"@Balance",Bal.ToString(),"@Bal_Type",BalType.ToString());

								}
								catch(Exception ex)
								{
									//MessageBox.Show("Cust Name : "+Cust_ID.ToString());
								}
								//exec UpdateBalance   @Cust_ID,@Op_Balance,@Balance_Type

							}
							rdr.Close();
						}
						rdr1.Close();
						
						//sql1="select * from Customer order by cust_name,city,cust_type";
						//dbobj.SelectQuery(sql1,ref rd);
						//while(rd.Read())
						//	dbobj.ExecProc(OprType.Insert,"UpdateBalance",ref op,"@Cust_Name",rd["cust_Name"].ToString(),"@Place",rd["City"].ToString(),"@Cr_Plus","0","@Dr_Plus","0");
							//exec UpdateBalance   @Cust_ID,@Op_Balance,@Balance_Type
						//rdr.Close();

						Flag=1;
						/******************Add by vikas **********************/

					}
					if(Flag==1)
					{
						MessageBox.Show("Stock Variation Updated Successfully");
						chkCustBal.Checked=false;
						chkStock.Checked=false;
					}
				}
				else
				{
					MessageBox.Show("Please Select Atleast One CheckBox");

				}
				CreateLogFiles.ErrorLog("Form:ModuleManage.aspx,Class:InventoryClass.cs,Method:btnUpdate_Click, User_ID: " +uid);
			}
			catch(Exception ex)
			{
				//MessageBox.Show("Ledger ID : "+Ledger_ID.ToString()+"Cust ID : "+Cust_ID.ToString());
				CreateLogFiles.ErrorLog("Form:ModuleManage.aspx,Class:InventoryClass.cs,Method:btnUpdate_Click,  Exception : "+ex.Message+"    User_ID: " +uid);
			}
		}
	}
}