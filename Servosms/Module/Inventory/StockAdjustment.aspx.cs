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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Servo_API.Models;
using System.Collections.Generic;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for StockAdjustment.
	/// </summary>
	public partial class StockAdjustment : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
		static ArrayList ProductID = new ArrayList();
		static ArrayList ProductQty = new ArrayList();
		static string tempDate ="";
        string baseUri = "http://localhost:64862";

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
				if(!Page.IsPostBack)
				{
					checkPrevileges(); 
					fillCombo();
					getStoreIn();
					getID();
					DropSavID.Visible=false;
					lblSAV_ID.Visible=true;
					txtDate.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
					ProductID = new ArrayList();
					ProductQty = new ArrayList();
					tempDate ="";
				}
                txtDate.Text = Request.Form["txtDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDate"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:page_load  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}

		/// <summary>
		/// Its checks the User Privilegs.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="4";
			string SubModule="8";
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
				btnPrint.Enabled = false; 
			}
			#endregion				
		}


		/// <summary>
		/// Its fills all the Product Name combos with Product Names and their packege.
		/// </summary>
		public void fillCombo()
		{			
			HtmlInputText[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			try
			{
                string strVehicleLBID = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/StockAdjustment/GetFillCombo").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        texthiddenprod.Value = JsonConvert.DeserializeObject<string>(id);                        
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                //dbobj.SelectQuery("Select case when pack_type != '' then Prod_Name+':'+Pack_Type else Prod_Name  end from products order by Prod_Name",ref SqlDtr);
                //if(SqlDtr.HasRows)
                //{
                //	texthiddenprod.Value="Select,";
                //	while(SqlDtr.Read())
                //	{
                //		//					for(int i= 0; i< Products.Length; i++)
                //		//					{
                //		//						Products[i].Items.Add(SqlDtr.GetValue(0).ToString() ); 
                //		//					}
                //		texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+","; 
                //	}
                //}
                //SqlDtr.Close();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:fillCombo()  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	

			}
		}

		/// <summary>
		/// This function  fetches the products store in location, Category, Package and quantity. And add this into hidden fields to use in java script.
		/// </summary>
		public void getStoreIn()
		{
			//SqlDataReader SqlDtr = null;
			//SqlDataReader SqlDtr1= null;
			//SqlDataReader SqlDtr2 = null;
			//string product_info="";
			//string product_info1 = "";
			//string product_info2 = "";
			try
			{
                StockAdjustmentModel stockAdjust = new StockAdjustmentModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/StockAdjustment/GetStoreIn").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        stockAdjust = JsonConvert.DeserializeObject<StockAdjustmentModel>(id);
                        txtTemp.Value = stockAdjust.product_info;
                        txtTemp1.Value = stockAdjust.product_info1;
                        txtQty.Value = stockAdjust.product_info2;
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

    //            dbobj.SelectQuery("Select case when pack_type != '' then Prod_Name+':'+Pack_Type else Prod_Name  end,Category,Store_In,Pack_Type,Prod_ID from products",ref SqlDtr);
				//while(SqlDtr.Read())
				//{
				//	if(SqlDtr.GetValue(1).ToString().Equals("Fuel"))
				//	{
				//		dbobj.SelectQuery("Select Prod_AbbName from tank where Tank_ID = '"+SqlDtr.GetValue(2).ToString()+"'", ref SqlDtr1);
				//		if(SqlDtr1.Read())
				//		{
				//			product_info = product_info+SqlDtr.GetValue(0).ToString().Trim() +"~"+SqlDtr.GetValue(1).ToString().Trim() +"~"+SqlDtr1.GetValue(0).ToString().Trim()+"~"+" "+"#";    
				//			product_info1 = product_info1+SqlDtr.GetValue(0).ToString().Trim()+"~"+"1X1#";
				//		}
				//		SqlDtr1.Close();
				//	}
				//	else
				//	{
				//		product_info = product_info+SqlDtr.GetValue(0).ToString().Trim() +"~"+SqlDtr.GetValue(1).ToString().Trim() +"~"+SqlDtr.GetValue(2).ToString().Trim() +"~"+SqlDtr.GetValue(3).ToString().Trim()+"#";    
				//		product_info1 = product_info1+SqlDtr.GetValue(0).ToString().Trim()+"~"+SqlDtr.GetValue(3).ToString()+"#";
				//	}
				//	dbobj.SelectQuery("Select top 1 Closing_Stock from Stock_Master where ProductID = "+SqlDtr.GetValue(4).ToString()+" order by stock_date desc",ref SqlDtr2);
				//	if(SqlDtr2.Read())
				//	{
				//		product_info2 = product_info2 + SqlDtr.GetValue(0).ToString().Trim() +"~"  +SqlDtr2.GetValue(0).ToString()+"#";  
				//	}
				//	SqlDtr2.Close() ;
				//}
				//SqlDtr.Close();
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:getStoreIn()  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	
			}
		}

		/// <summary>
		/// Its fetches the Max. Stock Adjustment ID from Stock_Adjustment to display on screen.
		/// </summary>
		public void getID()
		{
			//SqlDataReader SqlDtr = null;
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Res = client.GetAsync("api/StockAdjustment/GetNextStockAdjustID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        lblSAV_ID.Text = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

    //            dbobj.SelectQuery("Select max(SAV_ID)+1 from Stock_Adjustment",ref SqlDtr);
				//if(SqlDtr.Read())
				//{
				//	if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))   
				//		lblSAV_ID.Text  = SqlDtr.GetValue(0).ToString();
				//	else
				//		lblSAV_ID.Text = "1001";
				//}
				//SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:getID()  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	

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

		private void DropInProd2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		protected void TextBox4_TextChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Its fetch the values from screen and calls the save fucntion for each row.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		 {
			//DropDownList[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			HtmlInputText[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			TextBox[] QtyPack = {txtOutQtyPack1 ,txtOutQtyPack2,txtOutQtyPack3 ,txtOutQtyPack4 ,txtInQtyPack1 ,txtInQtyPack2 ,txtInQtyPack3 ,txtInQtyPack4 };
			TextBox[] QtyLtr = {txtOutQtyLtr1 ,txtOutQtyLtr2,txtOutQtyLtr3 ,txtOutQtyLtr4 ,txtInQtyLtr1 ,txtInQtyLtr2 ,txtInQtyLtr3 ,txtInQtyLtr4 };
			TextBox[] Store_In = {txtOutStoreIn1,txtOutStoreIn2,txtOutStoreIn3,txtOutStoreIn4,txtInStoreIn1,txtInStoreIn2,txtInStoreIn3,txtInStoreIn4 };
			HtmlInputHidden[] batch={bat0,bat1,bat2,bat3};

			try
			{
				int count = 0;
				for(int i = 0; i< Products.Length-4; i++)
				{
					if(Products[i].Value == "Select" && Products[i+4].Value == "Select" )
						count++;

					if(Products[i].Value == "Select" && Products[i+4].Value != "Select" )
					{
						MessageBox.Show("Please Select the OUT Product at Row "+(i+1));
						return;
					}

					if(Products[i].Value != "Select" && Products[i+4].Value == "Select" )
					{
						MessageBox.Show("Please Select the IN Product at Row "+(i+1));
						return;
					}
				  
				}

				if(count == 4 )
				{
					MessageBox.Show("Please Select Product");
					return;
				}

				for(int i = 0; i< Products.Length-4; i++)
				{
					if(Products[i].Value != "Select" && Products[i+4].Value != "Select")
					{
						if(QtyPack[i].Text == "" || QtyPack[i+4].Text == "")
						{
							MessageBox.Show("Please Fill The Qty");
							return;
						}
					}
				}
				string prod_name1= "";
				string pack1 = "";
				string prod_name2= "";
				string pack2 = "";
				string qty1 = "";
				string qty2 = "";
				string store1 = "";
				string store2 = "";
				string type1 = "";
				string type2 = "";

				string sav_id = "";
				if(lblSAV_ID.Visible==true)
					sav_id=lblSAV_ID.Text.ToString();
				else
				{
					UpdateProductQty();
					sav_id=DropSavID.SelectedItem.Text;
					int x=0;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/StockAdjustment/DeleteStockAdj?sav_id=" + sav_id).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            //receipt = JsonConvert.DeserializeObject<double>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    //dbobj.Insert_or_Update("delete from stock_adjustment where sav_id='"+DropSavID.SelectedItem.Text+"'",ref x);
				}
				
				if(lblSAV_ID.Visible!=true)
				{
					UpdateBatchNo();
					UpdateBatchNo_In();
				}

				for(int i = 0; i< Products.Length-4;i++)
				{
					//if(Products[i].SelectedIndex != 0 && Products[i+4].SelectedIndex != 0)
                    if(Products[i].Value != "Select" && Products[i+4].Value != "Select")
					{
						// Fetch the OUT products package and quantity if the product is of category Fuel or if the package is Loose then  take the Ltr quantity.
						//if(Products[i].SelectedItem.ToString().IndexOf(":") > -1  )
						if(Products[i].Value.ToString().IndexOf(":") > -1  )
						{
							//string[] arr = Products[i].SelectedItem.ToString().Split(new char[] {':'}  ,Products[i].SelectedItem.ToString().Length );
							string[] arr = Products[i].Value.ToString().Split(new char[] {':'}  ,Products[i].Value.ToString().Length );
							prod_name1 = arr[0].Trim();
							pack1 = arr[1].Trim();
							if(pack1.IndexOf("Loose") > -1)
								qty1 = QtyLtr[i].Text.Trim();
							else
								qty1 = QtyPack[i].Text.Trim();
							store1 = Request.Form[Store_In[i].ID.ToString()].ToString().Trim ();
							type1 = "Other";
						}
						else
						{
							prod_name1  = Products[i].Value.ToString().Trim();
							pack1 = "";
							qty1 = QtyLtr[i].Text.Trim();
							store1 = Store_In[i].Text.Trim ();
							type1 = "Fuel";
						}

						// Fetch the IN Products packages and quantity.if the product is of category Fuel or if the package is Loose then  take the Ltr quantity.
						if(Products[i+4].Value.ToString().IndexOf(":") > -1 )
						{
							string[] arr1 = Products[i+4].Value.ToString().Split(new char[] {':'} ,Products[i+4].Value.ToString().Length);
							prod_name2 = arr1[0].Trim();
							pack2 = arr1[1].Trim();
							if(pack2.IndexOf("Loose") > -1)
								qty2 = QtyLtr[i+4].Text.Trim();
							else
								qty2 = QtyPack[i+4].Text.Trim();
							store2 = Request.Form[Store_In[i + 4].ID.ToString()].ToString().Trim ();
							type2 = "Other";
						}
						else
						{
							prod_name2  = Products[i+4].Value.ToString().Trim();
							pack2 = "";
							qty2 = QtyLtr[i+4].Text.Trim();
							store2 = Request.Form[Store_In[i+4].ID.ToString()].ToString().Trim ();
							type2 = "Fuel";
						}
						save(sav_id,prod_name1,pack1,store1,type1,qty1,prod_name2,pack2,store2,type2,qty2);

						//Comment by Mahesh on 08.08.08 b'coz apply edit function then also required updated the batchno and batchtransaction table. only insert function working now. this function update leter.
						//UpdateBatchNo(prod_name1,pack1,qty1);
						//InsertBatchNo(prod_name2,pack2,qty2,batch[i].Value,i+1);
						

						InsertBatchNo(prod_name1,pack1,qty1);
						InsertBatchNo_In(prod_name2,prod_name1,pack2,pack1,qty2,qty1);
					}
					else
						continue;
				}
				if(lblSAV_ID.Visible==true)
					MessageBox.Show("Stock Adjustment Saved"); 
				else
				{
					SeqStockMaster();
					MessageBox.Show("Stock Adjustment Updated");
				}
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:btnPrint_Click  Stock Adjustment with ID "+lblSAV_ID.Text+" saved. UserID: "+uid);	
				makingReport();
				clear();
				getID(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:btnPrint_Click  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	
			}
		}

		public void UpdateBatchNo()
		{
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(DropSavID.SelectedItem.Text.Trim());
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/StockAdjustment/UpdateBatchNo?DropSavID=" + DropSavID.SelectedItem.Text.Trim(), byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;                                               
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }

                //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                //InventoryClass obj = new InventoryClass();
                //SqlDataReader rdr;
                //SqlCommand cmd;
                ////coment by vikas 18.06.09 rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"'");
                //rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+DropSavID.SelectedItem.Text+"' and trans_type='Stock Adjustment (OUT)'");
                //while(rdr.Read())
                //{
                //	//******************************
                //	string s="update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'";
                //	Con.Open();
                //	cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
                //	//cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"' and stock_date='"+GenUtil.str2MMDDYYYY(tempInvoiceDate.Value)+"'",Con);
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
                //cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='"+DropSavID.SelectedItem.Text+"' and Trans_Type='Stock Adjustment (OUT)'",Con);
                //cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //Con.Close();

            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : UpdateBatchNo() EXCEPTION :  "+ ex.Message+"   "+uid);
			}
		}



		public void UpdateBatchNo_In()
		{
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(DropSavID.SelectedItem.Text.Trim());
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/StockAdjustment/UpdateBatchNo_In?DropSavID=" + DropSavID.SelectedItem.Text.Trim(), byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        MessageBox.Show("Route Deleted");
                        
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }
                //SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
                //InventoryClass obj = new InventoryClass();
                //SqlDataReader rdr;
                //SqlCommand cmd;
                ////coment by vikas 18.06.09 rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+dropInvoiceNo.SelectedItem.Text+"'");
                //rdr = obj.GetRecordSet("select * from Batch_transaction where trans_id='"+DropSavID.SelectedItem.Text+"' and trans_type='Stock Adjustment (IN)'");
                //while(rdr.Read())
                //{
                //	//******************************
                //	string s="update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'";
                //	Con.Open();
                //	cmd = new SqlCommand("update StockMaster_Batch set Receipt=Receipt-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock-"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
                //	//cmd = new SqlCommand("update StockMaster_Batch set Sales=Sales-"+rdr["Qty"].ToString()+",Closing_Stock=Closing_Stock+"+rdr["Qty"].ToString()+" where ProductID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"' and stock_date='"+GenUtil.str2MMDDYYYY(tempInvoiceDate.Value)+"'",Con);
                //	cmd.ExecuteNonQuery();
                //	cmd.Dispose();
                //	Con.Close();

                //	/*******Add by vikas 19.06.09**********************/
                //	Con.Open();
                //	cmd = new SqlCommand("update BatchNo set Qty=Qty-"+rdr["Qty"].ToString()+" where Prod_ID='"+rdr["Prod_ID"].ToString()+"' and Batch_ID='"+rdr["Batch_ID"].ToString()+"'",Con);
                //	cmd.ExecuteNonQuery();
                //	cmd.Dispose();
                //	Con.Close();
                //	/************************************************/
                //}
                //rdr.Close();
                //Con.Open();
                //cmd = new SqlCommand("delete Batch_Transaction where Trans_ID='"+DropSavID.SelectedItem.Text+"' and Trans_Type='Stock Adjustment (IN)'",Con);
                //cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //Con.Close();

            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : UpdateBatchNo() EXCEPTION :  "+ ex.Message+"   "+uid);
			}
		}




		/// <summary>
		/// its calls the procedure ProInsertStockAdjustment to insert the stock adjustment details of each product.
		/// </summary>
		/// <param name="sav_id"></param>
		/// <param name="prod_name1"></param>
		/// <param name="pack1"></param>
		/// <param name="store1"></param>
		/// <param name="type1"></param>
		/// <param name="qty1"></param>
		/// <param name="prod_name2"></param>
		/// <param name="pack2"></param>
		/// <param name="store2"></param>
		/// <param name="type2"></param>
		/// <param name="qty2"></param>
		public void save(string sav_id, string prod_name1,string pack1,string store1,string type1,string qty1,string prod_name2,string pack2,string store2,string type2, string qty2)
		{
			try
			{
				//object obj = null;
                string str1 = txtNarration.Text;
                string str2= GenUtil.str2MMDDYYYY(txtDate.Text);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/StockAdjustment/SaveStockAdj?sav_id=" + sav_id + "&prod_name1=" + prod_name1 + "&pack1=" + pack1 + "&store1=" + store1 + "&type1=" + type1 + "&qty1=" + qty1 + "&prod_name2=" + prod_name2 + "&pack2=" + pack2 + "&store2=" + store2 + "&type2=" + type2 + "&qty2=" + qty2 + "&uid=" + uid + "&str1=" + str1 + "&str2=" + str2).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        //receipt = JsonConvert.DeserializeObject<double>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                    //dbobj.ExecProc(OprType.Insert, "ProInsertStockAdjustment", ref obj, "@SAV_ID", sav_id, "@Out_Product", prod_name1, "@pack1", pack1, "@Store1", store1, "@Type1", type1, "@Out_Qty", qty1, "@In_Product", prod_name2, "@Pack2", pack2, "@Store2", store2, "@Type2", type2, "@In_Qty", qty2, "@Entry_By", uid, "@Nar", txtNarration.Text, "@Stock_Date", GenUtil.str2MMDDYYYY(txtDate.Text));
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:Save()  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	
			}
		}

		/// <summary>
		/// It clears the form.
		/// </summary>
		public void clear()
		{
			//DropDownList[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			HtmlInputText[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			TextBox[] QtyPack = {txtOutQtyPack1 ,txtOutQtyPack2,txtOutQtyPack3 ,txtOutQtyPack4 ,txtInQtyPack1 ,txtInQtyPack2 ,txtInQtyPack3 ,txtInQtyPack4 };
			TextBox[] QtyLtr = {txtOutQtyLtr1 ,txtOutQtyLtr2,txtOutQtyLtr3 ,txtOutQtyLtr4 ,txtInQtyLtr1 ,txtInQtyLtr2 ,txtInQtyLtr3 ,txtInQtyLtr4 };
			HtmlInputHidden[] QtyLtr1 = {tmpOutQtyLtr1 ,tmpOutQtyLtr2,tmpOutQtyLtr3 ,tmpOutQtyLtr4 ,tmpInQtyLtr1 ,tmpInQtyLtr2 ,tmpInQtyLtr3 ,tmpInQtyLtr4 };
			TextBox[] Store_In = {txtOutStoreIn1,txtOutStoreIn2,txtOutStoreIn3,txtOutStoreIn4,txtInStoreIn1,txtInStoreIn2,txtInStoreIn3,txtInStoreIn4 };
			for(int i =0; i < QtyPack.Length ; i++)
			{
				//Products[i].SelectedIndex = 0;
				Products[i].Value = "Select";
				QtyPack[i].Enabled = true;
				QtyPack[i].Text = "";
				QtyLtr[i].Enabled = true;
				QtyLtr[i].Text = "";
				QtyLtr1[i].Value = "";
				Store_In[i].Text = "";
              
			}
			txtTotalInQtyLtr.Text = "";
			txtTotalInQtyPack.Text = "";
			txtTotalOutQtyLtr.Text = "";
			txtTotalOutQtyPack.Text = "";
			txtNarration.Text="";
			lblSAV_ID.Visible=true;
			DropSavID.Visible=false;
			btnEdit.Visible=true;
			btnPrint.Text="Save";
			txtDate.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
			ProductID = new ArrayList();
			ProductQty = new ArrayList();
		}

		/// <summary>
		/// It clears the form.
		/// </summary>
		public void clear1()
		{
			//DropDownList[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			HtmlInputText[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			TextBox[] QtyPack = {txtOutQtyPack1 ,txtOutQtyPack2,txtOutQtyPack3 ,txtOutQtyPack4 ,txtInQtyPack1 ,txtInQtyPack2 ,txtInQtyPack3 ,txtInQtyPack4 };
			TextBox[] QtyLtr = {txtOutQtyLtr1 ,txtOutQtyLtr2,txtOutQtyLtr3 ,txtOutQtyLtr4 ,txtInQtyLtr1 ,txtInQtyLtr2 ,txtInQtyLtr3 ,txtInQtyLtr4 };
			HtmlInputHidden[] QtyLtr1 = {tmpOutQtyLtr1 ,tmpOutQtyLtr2,tmpOutQtyLtr3 ,tmpOutQtyLtr4 ,tmpInQtyLtr1 ,tmpInQtyLtr2 ,tmpInQtyLtr3 ,tmpInQtyLtr4 };
			TextBox[] Store_In = {txtOutStoreIn1,txtOutStoreIn2,txtOutStoreIn3,txtOutStoreIn4,txtInStoreIn1,txtInStoreIn2,txtInStoreIn3,txtInStoreIn4 };
			for(int i =0; i < QtyPack.Length ; i++)
			{
				//Products[i].SelectedIndex = 0;
				Products[i].Value = "Select";
				QtyPack[i].Enabled = true;
				QtyPack[i].Text = "";
				QtyLtr[i].Enabled = true;
				QtyLtr[i].Text = "";
				QtyLtr1[i].Value = "";
				Store_In[i].Text = "";
              
			}
			txtTotalInQtyLtr.Text = "";
			txtTotalInQtyPack.Text = "";
			txtTotalOutQtyLtr.Text = "";
			txtTotalOutQtyPack.Text = "";
			txtNarration.Text="";
//			lblSAV_ID.Visible=true;
//			DropSavID.Visible=false;
//			btnEdit.Visible=true;
//			btnPrint.Text="Save";
//			txtDate.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
			ProductID = new ArrayList();
			ProductQty = new ArrayList();
		}

		/// <summary>
		/// Fetch the details from screen and writes into a StockAdjustment.txt file.
		/// </summary>
		public void makingReport()
		{
			
			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\StockAdjustment.txt";
				StreamWriter sw = new StreamWriter(path);
				//DropDownList[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
				HtmlInputText[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
				TextBox[] QtyPack = {txtOutQtyPack1 ,txtOutQtyPack2,txtOutQtyPack3 ,txtOutQtyPack4 ,txtInQtyPack1 ,txtInQtyPack2 ,txtInQtyPack3 ,txtInQtyPack4 };
				HtmlInputHidden[] QtyLtr = {tmpOutQtyLtr1 ,tmpOutQtyLtr2,tmpOutQtyLtr3 ,tmpOutQtyLtr4 ,tmpInQtyLtr1 ,tmpInQtyLtr2 ,tmpInQtyLtr3 ,tmpInQtyLtr4 };
				TextBox[] QtyLtr1 = {txtOutQtyLtr1 ,txtOutQtyLtr2,txtOutQtyLtr3 ,txtOutQtyLtr4 ,txtInQtyLtr1 ,txtInQtyLtr2 ,txtInQtyLtr3 ,txtInQtyLtr4 };
				TextBox[] Store_In = {txtOutStoreIn1,txtOutStoreIn2,txtOutStoreIn3,txtOutStoreIn4,txtInStoreIn1,txtInStoreIn2,txtInStoreIn3,txtInStoreIn4 };
				for(int i = 0; i < QtyLtr1.Length ; i++)
				{
					if(QtyLtr1[i].Text.Trim()  != "")
					{
						QtyLtr[i].Value =  QtyLtr1[i].Text.Trim();
					}
				}

				string info = "";

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
				string des="-----------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("==========================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Stock Adjustment Voucher",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==========================",des.Length));
				sw.WriteLine("") ;
				sw.WriteLine("+----------------------------------------------------------------+----------------------------------------------------------------+") ;
				sw.WriteLine("|                              OUT                               |                               IN                               |") ;
				sw.WriteLine("+------------------------------+---------------+-------+---------+------------------------------+---------------+-------+---------+") ;
				sw.WriteLine("|                              |               | Qty.  |         |                              |               | Qty.  |         |") ;
				sw.WriteLine("|     Product Name &           |   Store In    |  in   | Qty. in |     Product Name &           |   Store In    |  in   | Qty. in |") ;
				sw.WriteLine("|         Package              |               | Pack. |   Ltr.  |        Package               |               | Pack. |   Ltr.  |") ;
				sw.WriteLine("+------------------------------+---------------+-------+---------+------------------------------+---------------+-------+---------+") ;
				// " 1234567890123456789012345 123456789012345 123.555 123456789 12345678901234567890 123456789012345 123.555 123456789 ";
				info = " {0,-30:S} {1,-15:S} {2,7:F} {3,9:F} {4,-30:S} {5,-15:S} {6,7:F} {7,9:F}";
				//if(Products[0].SelectedIndex != 0)
				if(Products[0].Value != "Select")
				{
					//sw.WriteLine(info,Products[0].SelectedItem.Text.Trim(),Store_In[0].Text.Trim(),QtyPack[0].Text.Trim(),QtyLtr[0].Value.Trim(),Products[4].SelectedItem.Text.Trim(),Store_In[4].Text.Trim(),QtyPack[4].Text.Trim(),QtyLtr[4].Value.Trim()); 
					sw.WriteLine(info,Products[0].Value.Trim(),Store_In[0].Text.Trim(),QtyPack[0].Text.Trim(),QtyLtr[0].Value.Trim(),Products[4].Value.Trim(),Store_In[4].Text.Trim(),QtyPack[4].Text.Trim(),QtyLtr[4].Value.Trim()); 
				}
				else
					sw.WriteLine(info,"","","","","","","",""); 

				//if(Products[1].SelectedIndex != 0)
				if(Products[1].Value != "Select")
					//sw.WriteLine(info,Products[1].SelectedItem.Text.Trim(),Store_In[1].Text.Trim(),QtyPack[1].Text.Trim(),QtyLtr[1].Value.Trim(),Products[5].SelectedItem.Text.Trim(),Store_In[5].Text.Trim(),QtyPack[5].Text.Trim(),QtyLtr[5].Value.Trim()); 
					sw.WriteLine(info,Products[1].Value.Trim(),Store_In[1].Text.Trim(),QtyPack[1].Text.Trim(),QtyLtr[1].Value.Trim(),Products[5].Value.Trim(),Store_In[5].Text.Trim(),QtyPack[5].Text.Trim(),QtyLtr[5].Value.Trim()); 
				else
					sw.WriteLine(info,"","","","","","","","");

				//if(Products[2].SelectedIndex != 0)
				if(Products[2].Value != "Select")
					//sw.WriteLine(info,Products[2].SelectedItem.Text.Trim(),Store_In[2].Text.Trim(),QtyPack[2].Text.Trim(),QtyLtr[2].Value.Trim(),Products[6].SelectedItem.Text.Trim(),Store_In[6].Text.Trim(),QtyPack[6].Text.Trim(),QtyLtr[6].Value.Trim()); 
					sw.WriteLine(info,Products[2].Value.Trim(),Store_In[2].Text.Trim(),QtyPack[2].Text.Trim(),QtyLtr[2].Value.Trim(),Products[6].Value.Trim(),Store_In[6].Text.Trim(),QtyPack[6].Text.Trim(),QtyLtr[6].Value.Trim()); 
				else
					sw.WriteLine(info,"","","","","","","","");

				//if(Products[3].SelectedIndex != 0)
				if(Products[3].Value != "Select")
					//sw.WriteLine(info,Products[3].SelectedItem.Text.Trim(),Store_In[3].Text.Trim(),QtyPack[3].Text.Trim(),QtyLtr[3].Value.Trim(),Products[7].SelectedItem.Text.Trim(),Store_In[7].Text.Trim(),QtyPack[7].Text.Trim(),QtyLtr[7].Value.Trim()); 
					sw.WriteLine(info,Products[3].Value.Trim(),Store_In[3].Text.Trim(),QtyPack[3].Text.Trim(),QtyLtr[3].Value.Trim(),Products[7].Value.Trim(),Store_In[7].Text.Trim(),QtyPack[7].Text.Trim(),QtyLtr[7].Value.Trim()); 
				else
					sw.WriteLine(info,"","","","","","","",""); 
				sw.WriteLine("+------------------------------+---------------+-------+---------+------------------------------+---------------+-------+---------+") ;
				sw.WriteLine("                                Total Out:      {0,7:F} {1,9:F}                                Total IN:       {2,7:F} {3,9:F} ",txtTotalOutQtyPack.Text,txtTotalOutQtyLtr.Text,txtTotalInQtyPack.Text,txtTotalInQtyLtr.Text); 
				sw.WriteLine("+------------------------------+---------------+-------+---------+------------------------------+---------------+-------+---------+") ;
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();		
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:makingReport()  EXCEPTION: "+ ex.Message+".  UserID: "+uid);	
			}
		}


		/// <summary>
		/// Sends the StockAdjustment.txt file name to print server.
		/// </summary>
		public void Print()
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
					CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\StockAdjustment.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					//CreateLogFiles.ErrorLog("Form:Vehiclereport.aspx,Method:print"+ "  Daily sales record  Printed   userid  "+uid);
					CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:print"+ " Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockAdjustment.aspx,Method:print"+ " EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}
	


		public void InsertBatchNo(string Prod,string PackType,string Qty)
		{
			try
			{
                StockAdjustmentModel stockAdjust = new StockAdjustmentModel();

                stockAdjust.Prod = Prod;
                stockAdjust.PackType = PackType;
                stockAdjust.Qty = Qty;                

                if (lblSAV_ID.Visible == true)
                {
                    stockAdjust.SAV_ID = lblSAV_ID.Text;
                    stockAdjust.SAV_ID_Visible = true;
                }                    
                else
                {
                    stockAdjust.SAV_ID = DropSavID.SelectedItem.Text;
                    stockAdjust.SAV_ID_Visible = false;
                }

                stockAdjust.Date = txtDate.Text;                

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(stockAdjust);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/StockAdjustment/InsertBatchNo", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                        response.EnsureSuccessStatusCode();
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
				//SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by stock_date");
				//int count=0;
				//if(Qty!="")
				//	count=int.Parse(Qty);
				//int x=0;
				//double cl_sk=0;
				//while(rdr.Read())
				//{
				//	if(double.Parse(rdr["closing_stock"].ToString())>0)
				//		cl_sk=double.Parse(rdr["closing_stock"].ToString());
				//	else
				//		continue;
				//	if(count>0)
				//	{
				//		if(int.Parse(rdr["closing_stock"].ToString())>0)
				//		{
				//			if(count<=int.Parse(rdr["closing_stock"].ToString()))
				//			{
				//				cl_sk-=count;
								
				//				dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//				if(lblSAV_ID.Visible==true)
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
				//				else
				//					//22.06.09 dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
								
								
				//				//***********add by vikas 19.06.09 *****************
								
				//				dbobj1.Insert_or_Update("update batchno set qty=qty-"+count+" where prod_id='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//				//****************************
				//				count=0;
				//				break;
				//			}
				//			else
				//			{
				//				cl_sk-=double.Parse(rdr["closing_stock"].ToString());
				//				//dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
				//				dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+double.Parse(rdr["closing_stock"].ToString())+",closing_stock=closing_stock-"+double.Parse(rdr["closing_stock"].ToString())+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//				if(lblSAV_ID.Visible==true)
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["closing_stock"].ToString()+"',"+cl_sk.ToString()+")",ref x);
				//				else
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["closing_stock"].ToString()+"',"+cl_sk.ToString()+")",ref x);
				//				//count-=int.Parse(rdr["qty"].ToString());

				//				//***********add by vikas 19.06.09 *****************
				//				dbobj1.Insert_or_Update("update batchno set qty="+cl_sk+" where prod_id='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//				//****************************

				//				count-=int.Parse(rdr["closing_stock"].ToString());

				//				//*****Add by vikas 10.06.09*********
				//				if(lblSAV_ID.Visible==true)
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','0','"+count.ToString()+"',"+cl_sk.ToString()+")",ref x);
				//				else
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (OUT)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','0','"+count.ToString()+"',"+cl_sk.ToString()+")",ref x);
				//				//*****end*********
				//			}
				//		}
				//	}
				//}
				//rdr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : InsertBatchNo() EXCEPTION :  "+ ex.Message+"   "+uid);
			}
		}


		public void InsertBatchNo_In(string Prod,string Prod1,string PackType,string PackType1,string Qty,string Qty1)
		{
			try
			{
                StockAdjustmentModel stockAdjust = new StockAdjustmentModel();

                stockAdjust.Prod = Prod;
                stockAdjust.PackType = PackType;
                stockAdjust.Qty = Qty;                

                stockAdjust.Prod1 = Prod1;
                stockAdjust.PackType1 = PackType1;
                stockAdjust.Qty1 = Qty1;

                if (lblSAV_ID.Visible == true)
                {
                    stockAdjust.SAV_ID = lblSAV_ID.Text;
                    stockAdjust.SAV_ID_Visible = true;
                }
                else
                {
                    stockAdjust.SAV_ID = DropSavID.SelectedItem.Text;
                    stockAdjust.SAV_ID_Visible = false;
                }

                stockAdjust.Date = txtDate.Text;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    var myContent = JsonConvert.SerializeObject(stockAdjust);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/StockAdjustment/InsertBatchNoIn", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }

    //            InventoryClass obj = new InventoryClass();
				//InventoryClass obj1 = new InventoryClass();
				//DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				//SqlDataReader rdr1 = null;
				//int SNo=0,BatID=0;;
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
				//rdr1 = obj.GetRecordSet("select max(Batch_ID) from BatchNo");
				//if(rdr1.Read())
				//{
				//	if(rdr1.GetValue(0).ToString() != null && rdr1.GetValue(0).ToString()!="")
				//		BatID=int.Parse(rdr1.GetValue(0).ToString());
				//	else
				//		BatID=0;
				//}
				//else
				//	BatID=0;
				//rdr1.Close();
	
				//SqlDataReader rdr = obj.GetRecordSet("select * from stockmaster_batch where productid=(select prod_id from products where prod_name='"+Prod+"' and Pack_Type='"+PackType+"') order by stock_date");
				//int count=0;
				//if(Qty!="")
				//	count=int.Parse(Qty);
				//int x=0;
				//double cl_sk=0;
				//string batch_name="";
				//while(rdr.Read())
				//{
				//	if(double.Parse(rdr["closing_stock"].ToString())>0)
				//	{
				//		cl_sk=double.Parse(rdr["closing_stock"].ToString());
				//	}
				//	else
				//	{
				//		/*******Add by vikas 24.06.09 ****************************/

				//		rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod1+"' and pack_type='"+PackType1+"')");
				//		if(rdr1.Read())
				//		{
				//			batch_name=rdr1.GetValue(1).ToString();
				//		}
				//		rdr1.Close();
					
				//		cl_sk+=count;
					
				//		string prod_id="";
				//		rdr1 = obj1.GetRecordSet("select prod_id from products where prod_name='"+Prod+"' and pack_type='"+PackType+"'");
				//		if(rdr1.Read())
				//		{
				//			prod_id=rdr1.GetValue(0).ToString();
				//		}
				//		rdr1.Close();

				//		string batch_id="";
				//		rdr1 = obj1.GetRecordSet("select batch_id from batchno where batch_no='"+batch_name+"' and prod_id='"+prod_id+"'");
				//		if(rdr1.Read())
				//		{
				//			batch_id=rdr1.GetValue(0).ToString();
				//		}
				//		rdr1.Close();

				//		//24.06.09 dbobj.Insert_or_Update("insert into BatchNo values("+(++BatID)+",'"+batch_name.ToString()+"','"+prod_id.ToString()+"','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',"+count.ToString()+",'"+lblSAV_ID.Text+"')",ref x);
				//		//24.06.09 dbobj1.Insert_or_Update("insert into stockmaster_batch values("+prod_id.ToString()+","+BatID.ToString()+",'"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',0,"+count.ToString()+",0,"+count.ToString()+",0,0)",ref x);

				//		//dbobj.Insert_or_Update("update BatchNo set qty="+count.ToString()+", '"+lblSAV_ID.Text+"')",ref x);
				//		//dbobj1.Insert_or_Update("update stockmaster_batch values("+prod_id.ToString()+","+BatID.ToString()+",'"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',0,"+count.ToString()+",0,"+count.ToString()+",0,0)",ref x);

				//		dbobj1.Insert_or_Update("update batchno set qty="+count.ToString()+" where prod_id='"+prod_id.ToString()+"' and batch_id='"+batch_id.ToString()+"'",ref x);
				//		dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+"+count+",closing_stock=closing_stock+"+count+" where productid='"+prod_id.ToString()+"' and batch_id='"+batch_id.ToString()+"'",ref x);

								
				//		if(lblSAV_ID.Visible==true)
				//			dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+prod_id.ToString()+"','"+BatID.ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
				//		else
				//			dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+prod_id.ToString()+"','"+batch_id.ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
														
				//		count=0;

				//		/*******End ****************************/
				//		continue;
				//	}

				//	/*******Add by vikas 23.06.09***********************/
				//	rdr1 = obj1.GetRecordSet("select * from batchno where prod_id="+rdr["productid"].ToString()+" and batch_id="+rdr["batch_id"].ToString());
				//	if(rdr1.Read())
				//	{
				//		batch_name=rdr1.GetValue(1).ToString();
				//	}
				//	rdr1.Close();
				//	/*******End***********************/

				//	if(count>0)
				//	{
				//		if(int.Parse(rdr["closing_stock"].ToString())>0)
				//		{
				//			if(count<=int.Parse(rdr["closing_stock"].ToString()))
				//			{
				//				cl_sk+=count;
								
				//				//23.06.09 dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+"+count+",closing_stock=closing_stock+"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

				//				/*******Add by vikas 23.06.09***********************/
								
				//				rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod1+"' and Pack_Type='"+PackType1+"') and batch_no='"+batch_name+"'");
				//				//23.06.09 rdr1 = obj1.GetRecordSet("select * from batchno where prod_id="+rdr["productid"].ToString()+" and batch_no='"+batch_name+"'");
				//				if(rdr1.HasRows)
				//				{
				//					dbobj1.Insert_or_Update("update batchno set qty="+cl_sk+" where prod_id='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//					dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+"+count+",closing_stock=closing_stock+"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//				}
				//				else
				//				{
				//					dbobj.Insert_or_Update("insert into BatchNo values("+(++BatID)+",'"+batch_name.ToString()+"','"+rdr["productid"].ToString()+"','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',"+count.ToString()+",'"+lblSAV_ID.Text+"')",ref x);
				//					dbobj1.Insert_or_Update("insert into stockmaster_batch values("+rdr["productid"].ToString()+","+rdr["batch_id"].ToString()+",'"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',0,"+count.ToString()+",0,"+count.ToString()+",0,0",ref x);
				//				}
				//				rdr1.Close();
				//				/*******End***********************/


				//				if(lblSAV_ID.Visible==true)
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
				//				else
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
															
				//				count=0;
				//				break;
				//			}
				//			else
				//			{

				//				cl_sk+=count;
								
				//				//23.06.09 dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+"+count+",closing_stock=closing_stock+"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);

				//				/*******Add by vikas 23.06.09***********************/
								
				//				rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod1+"' and Pack_Type='"+PackType1+"') and batch_no='"+batch_name+"'");
				//				//23.06.09 rdr1 = obj1.GetRecordSet("select * from batchno where prod_id="+rdr["productid"].ToString()+" and batch_no='"+batch_name+"'");
				//				if(rdr1.HasRows)
				//				{
				//					dbobj1.Insert_or_Update("update batchno set qty="+cl_sk+" where prod_id='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//					dbobj1.Insert_or_Update("update stockmaster_batch set receipt=receipt+"+count+",closing_stock=closing_stock+"+count+" where productid='"+rdr["productid"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
				//				}
				//				else
				//				{
				//					dbobj.Insert_or_Update("insert into BatchNo values("+(++BatID)+",'"+batch_name.ToString()+"','"+rdr["productid"].ToString()+"','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',"+count.ToString()+",'"+lblSAV_ID.Text+"')",ref x);
				//					dbobj1.Insert_or_Update("insert into stockmaster_batch values("+rdr["productid"].ToString()+","+rdr["batch_id"].ToString()+",'"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',0,"+count.ToString()+",0,"+count.ToString()+",0,0",ref x);
				//				}
				//				rdr1.Close();
				//				/*******End***********************/

				//				if(lblSAV_ID.Visible==true)
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
				//				else
				//					dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+rdr["ProductID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);									
				//			}
				//		}
				//	}
				//}
				//if(!rdr.HasRows)
				//{
				//	rdr1 = obj1.GetRecordSet("select * from batchno where prod_id=(select prod_id from products where prod_name='"+Prod1+"' and pack_type='"+PackType1+"')");
				//	if(rdr1.Read())
				//	{
				//		batch_name=rdr1.GetValue(1).ToString();
				//	}
				//	rdr1.Close();
					
				//	if(batch_name!="")
				//	{
				//		cl_sk+=count;
					
				//		string prod_id="";
				//		rdr1 = obj1.GetRecordSet("select prod_id from products where prod_name='"+Prod+"' and pack_type='"+PackType+"'");
				//		if(rdr1.Read())
				//		{
				//			prod_id=rdr1.GetValue(0).ToString();
				//		}
				//		rdr1.Close();

				//		dbobj.Insert_or_Update("insert into BatchNo values("+(++BatID)+",'"+batch_name.ToString()+"','"+prod_id.ToString()+"','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',"+count.ToString()+",'"+lblSAV_ID.Text+"')",ref x);
				//		dbobj1.Insert_or_Update("insert into stockmaster_batch values("+prod_id.ToString()+","+BatID.ToString()+",'"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"',0,"+count.ToString()+",0,"+count.ToString()+",0,0)",ref x);
								
				//		if(lblSAV_ID.Visible==true)
				//			dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+prod_id.ToString()+"','"+BatID.ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);
				//		else
				//			dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+DropSavID.SelectedItem.Text+"','Stock Adjustment (IN)','"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())+"','"+prod_id.ToString()+"','"+BatID.ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
														
				//		count=0;
				//	}				
				//}
				//rdr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SalesInvoice.aspx, Method : InsertBatchNo() EXCEPTION :  "+ ex.Message+"   "+uid);
			}
		}




		/// <summary>
		/// This method is used to update the product batch no infrmation.
		/// </summary>
		/// <param name="Prod"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		public void UpdateBatchNo(string Prod,string PackType,string Qty)
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
			int count=int.Parse(Qty);
			int x=0;
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
							cl_sk-=count;
							dbobj1.Insert_or_Update("update batchno set qty=qty-"+count+" where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
							dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+count+",closing_stock=closing_stock-"+count+" where productid='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
							dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (OUT)','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+count+"',"+cl_sk.ToString()+")",ref x);	
							count=0;
							break;
						}
						else
						{
							cl_sk-=double.Parse(rdr["qty"].ToString());
							dbobj1.Insert_or_Update("update batchno set qty=0 where prod_id='"+rdr["prod_id"].ToString()+"' and trans_no='"+rdr["trans_no"].ToString()+"' and Batch_No='"+rdr["Batch_No"].ToString()+"' and Date='"+rdr["Date"].ToString()+"'",ref x);
							dbobj1.Insert_or_Update("update stockmaster_batch set sales=sales+"+double.Parse(rdr["qty"].ToString())+",closing_stock=closing_stock-"+double.Parse(rdr["qty"].ToString())+" where productid='"+rdr["prod_id"].ToString()+"' and batch_id='"+rdr["batch_id"].ToString()+"'",ref x);
							dbobj1.Insert_or_Update("insert into batch_transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (OUT)','"+DateTime.Now.ToString()+"','"+rdr["Prod_ID"].ToString()+"','"+rdr["Batch_ID"].ToString()+"','"+rdr["qty"].ToString()+"',"+cl_sk.ToString()+")",ref x);
							count-=int.Parse(rdr["qty"].ToString());
						}
					}
				}
			}
			rdr.Close();
		}

		/// <summary>
		/// This method is used to save the product batch no.
		/// </summary>
		/// <param name="Prod"></param>
		/// <param name="PackType"></param>
		/// <param name="Qty"></param>
		/// <param name="batch"></param>
		/// <param name="j"></param>
		public void InsertBatchNo(string Prod,string PackType,string Qty,string batch,int j)
		{
			InventoryClass objprod = new InventoryClass();
			int batch_id=0,SNo=0,x=0;
			SqlDataReader rdr = null;
			rdr = objprod.GetRecordSet("select max(sno)+1 from Batch_Transaction");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString()!="")
					SNo=int.Parse(rdr.GetValue(0).ToString());
				else
					SNo=1;
			}
			else
				SNo=1;
			rdr.Close();
			rdr = objprod.GetRecordSet("select max(Batch_ID)+1 from BatchNo");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString() != null && rdr.GetValue(0).ToString()!="")
					batch_id=int.Parse(rdr.GetValue(0).ToString());
				else
					batch_id=1;
			}
			else
				batch_id=1;
			rdr.Close();
			rdr=null;
			 if(Request.Params.Get("chkbatch"+j)=="on" && batch.ToString() != "")
			{
				string prodid="";
				rdr=objprod.GetRecordSet("select prod_id from products where prod_name='"+Prod.ToString()+"' and pack_type='"+PackType.ToString()+"'");
				if(rdr.Read())
				{
					prodid=rdr["prod_id"].ToString();
				}
				rdr.Close();
				
				string dt = DateTime.Now.ToString();
				string[] arr = batch.Split(new char[] {','},batch.Length);
				 for(int n=0;n<arr.Length/2;n++)
				{
					if(!arr[n].ToString().Equals("''"))
					{
						System.Threading.Thread.Sleep(2000);
						dbobj.Insert_or_Update("insert into BatchNo values("+batch_id+","+arr[n].ToString()+",'"+prodid+"','"+dt+"',"+arr[++n].ToString()+",'"+lblSAV_ID.Text+"')",ref x);
						dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+lblSAV_ID.Text+"','Stock Adjustment (IN)','"+dt+"','"+prodid+"',"+batch_id+","+arr[n].ToString()+","+arr[n].ToString()+")",ref x);//Maintain the closing stock by Prod_ID on every Batch No
						dbobj.Insert_or_Update("insert into StockMaster_Batch values("+prodid+",'"+batch_id+"','"+dt+"',0,"+arr[n].ToString()+",0,"+arr[n].ToString()+",0,0)",ref x);
						batch_id++;
					}
					else
						break;
				}
			 }
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Print();
		}

		/// <summary>
		/// This method is used to fatch the all stock adjustment ID from database and fill the dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			lblSAV_ID.Visible=false;
			DropSavID.Visible=true;
			btnEdit.Visible=false;
			btnPrint.Text="Update";

            List<string> lstStockAdjustmentIDs = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Res = client.GetAsync("api/StockAdjustment/GetStockAdjustmentIds").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var id = Res.Content.ReadAsStringAsync().Result;
                    lstStockAdjustmentIDs = JsonConvert.DeserializeObject<List<string>>(id);
                }
                else
                    Res.EnsureSuccessStatusCode();
            }

            if (lstStockAdjustmentIDs != null)
            {
                DropSavID.Items.Clear();
                DropSavID.Items.Add("Select");
                foreach (var ID in lstStockAdjustmentIDs)
                    DropSavID.Items.Add(ID);
            }

            //InventoryClass obj = new InventoryClass();
            //SqlDataReader rdr = obj.GetRecordSet("select distinct sav_id from stock_adjustment order by sav_id");
   //         DropSavID.Items.Clear();
			//DropSavID.Items.Add("Select");
			//if(rdr.HasRows)
			//{
			//	while(rdr.Read())
			//	{
			//		DropSavID.Items.Add(rdr.GetValue(0).ToString());
			//	}
			//}
			//rdr.Close();
		}

		/// <summary>
		/// This method is used to fatch the record according to select the stock adjustment ID from drodownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropSavID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader SqlDtr = null;
			HtmlInputText[] Products = {DropOutProd1,DropOutProd2,DropOutProd3,DropOutProd4,DropInProd1,DropInProd2,DropInProd3,DropInProd4};
			TextBox[] QtyPack = {txtOutQtyPack1 ,txtOutQtyPack2,txtOutQtyPack3 ,txtOutQtyPack4 ,txtInQtyPack1 ,txtInQtyPack2 ,txtInQtyPack3 ,txtInQtyPack4 };
			TextBox[] QtyLtr = {txtOutQtyLtr1 ,txtOutQtyLtr2,txtOutQtyLtr3 ,txtOutQtyLtr4 ,txtInQtyLtr1 ,txtInQtyLtr2 ,txtInQtyLtr3 ,txtInQtyLtr4 };
			HtmlInputHidden[] QtyLtr1 = {tmpOutQtyLtr1 ,tmpOutQtyLtr2,tmpOutQtyLtr3 ,tmpOutQtyLtr4 ,tmpInQtyLtr1 ,tmpInQtyLtr2 ,tmpInQtyLtr3 ,tmpInQtyLtr4 };
			TextBox[] Store_In = {txtOutStoreIn1,txtOutStoreIn2,txtOutStoreIn3,txtOutStoreIn4,txtInStoreIn1,txtInStoreIn2,txtInStoreIn3,txtInStoreIn4 };
			SqlDataReader rdr = obj.GetRecordSet("select * from stock_adjustment where sav_id='"+DropSavID.SelectedItem.Text+"'");
			int i=0;
			clear1();
			while(rdr.Read())
			{
				double totqty =0;
				ProductID.Add(rdr["outprod_id"].ToString());
				ProductID.Add(rdr["inprod_id"].ToString());
				ProductQty.Add(rdr["outqty"].ToString());
				ProductQty.Add(rdr["inqty"].ToString());
				dbobj.SelectQuery("select prod_name,pack_type,total_qty,store_in from products where prod_id='"+rdr["outprod_id"].ToString()+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
                    Products[i].Value=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString();
					Store_In[i].Text=SqlDtr["Store_In"].ToString();
					totqty=double.Parse(SqlDtr.GetValue(2).ToString());
				}
				SqlDtr.Close();
				QtyPack[i].Text=rdr["OutQty"].ToString();
				QtyLtr[i].Text=System.Convert.ToString(totqty*double.Parse(rdr["outqty"].ToString()));
				QtyLtr1[i].Value=System.Convert.ToString(totqty*double.Parse(rdr["outqty"].ToString()));
				totqty =0;
				dbobj.SelectQuery("select prod_name,pack_type,total_qty,store_in from products where prod_id='"+rdr["inprod_id"].ToString()+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Products[i+4].Value=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString();
					Store_In[i+4].Text=SqlDtr["Store_In"].ToString();
					totqty=double.Parse(SqlDtr.GetValue(2).ToString());
				}
				SqlDtr.Close();
				QtyPack[i+4].Text=rdr["InQty"].ToString();
				QtyLtr[i+4].Text=System.Convert.ToString(totqty*double.Parse(rdr["inqty"].ToString()));
				QtyLtr1[i+4].Value=System.Convert.ToString(totqty*double.Parse(rdr["inqty"].ToString()));
				txtDate.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Entry_Time"].ToString()));
				tempDate=GenUtil.trimDate(rdr["Entry_Time"].ToString());
				i++;
			}
			rdr.Close();
		}

		/// <summary>
		/// This method is used to update the qty of given product before update the record.
		/// </summary>
		public void UpdateProductQty()
		{
			int x=0;
			for(int i=0;i<ProductID.Count;i++)
			{
                string str1 = txtNarration.Text;
                string str2 = GenUtil.str2MMDDYYYY(txtDate.Text);
                using (var client = new HttpClient())
                {
                     str1 = ProductQty[i].ToString();
                     str2 = ProductID[i].ToString();
                    string str3 = tempDate;
                    string str4 = ProductQty[++i].ToString();
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/StockAdjustment/UpdateStockAdj?str1=" + str1 + "&str2=" + str2 + "&str3=" + str3 + "&str4=" + str4).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        //receipt = JsonConvert.DeserializeObject<double>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
            //dbobj.Insert_or_Update("update stock_master set sales=sales-"+ProductQty[i]+",closing_stock=closing_stock+"+ProductQty[i]+" where productid='"+ProductID[i]+"' and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'"+ tempDate + "',103)",ref x);
			//dbobj.Insert_or_Update("update stock_master set receipt=receipt-"+ProductQty[++i]+",closing_stock=closing_stock-"+ProductQty[i]+" where productid='"+ProductID[i]+ "' and cast(floor(cast(stock_date as float)) as datetime)=Convert(datetime,'" + tempDate + "',103)",ref x);
			}
		}

		/// <summary>
		/// This method is used to update the product stock in sequence after update the record.
		/// </summary>
		public void SeqStockMaster()
		{
			InventoryClass obj = new InventoryClass();
			SqlCommand cmd;
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlDataReader rdr1=null;

			for(int i=0;i<ProductID.Count;i++)
			{
                StockAdjustmentModel stkadj = new StockAdjustmentModel();
                string str1 = ProductID[i].ToString();                
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/StockAdjustment/SeqStockMaster?str1=" + str1).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        stkadj = JsonConvert.DeserializeObject<StockAdjustmentModel>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                List<string> controlopening_stock = new List<string>();
                List<string> controlreceipt = new List<string>();
                List<string> controlsales = new List<string>();
                List<string> controlsalesfoc = new List<string>();
                List<string> controlProductid = new List<string>();
                List<string> controlstock_date = new List<string>();
                foreach (var id in stkadj.opening_stock)
                {
                    controlopening_stock = stkadj.opening_stock;
                }
                foreach (var id in stkadj.receipt)
                {
                    controlreceipt = stkadj.receipt;
                }
                foreach (var id in stkadj.sales)
                {
                    controlsales = stkadj.sales;
                }
                foreach (var id in stkadj.salesfoc)
                {
                    controlsalesfoc = stkadj.salesfoc;
                }
                foreach (var id in stkadj.Productid)
                {
                    controlProductid = stkadj.Productid;
                }
                foreach (var id in stkadj.stock_date)
                {
                    controlstock_date = stkadj.stock_date;
                }
                
                //string str="select * from Stock_Master where Productid='"+ProductID[i].ToString()+"' order by Stock_date";
				//rdr1=obj.GetRecordSet(str);
				double OS=0,CS=0,k=0;
                int count = stkadj.opening_stock.Count;
                for (int p = 0; p <= count - 1; p++)
                {
					if(k==0)
					{
						OS=double.Parse(controlopening_stock[p].ToString());
						k++;
					}
					else
						OS=CS;
					CS=OS+double.Parse(controlreceipt[p].ToString())-(double.Parse(controlsales[p].ToString()) +double.Parse(controlsalesfoc[p].ToString()));
					//Con.Open();
                    string str7 = controlProductid[p].ToString();
                    string str8 = controlstock_date[p].ToString();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/StockAdjustment/UpdateSeqStockMaster?OS=" + OS + "&CS=" + CS + "&str7=" + str7 + "&str8=" + str8).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            //stkadj = JsonConvert.DeserializeObject<StockAdjustmentModel>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    //cmd = new SqlCommand("update Stock_Master set opening_stock='"+OS.ToString()+"', Closing_Stock='"+CS.ToString()+"' where ProductID='"+ controlProductid[p].ToString()+"' and Stock_Date=Convert(datetime,'"+ controlstock_date[p].ToString() + "',103)",Con);
					//cmd.ExecuteNonQuery();
					//cmd.Dispose();
					//Con.Close();
				}
				//rdr1.Close();
			}
		}
	}
}
