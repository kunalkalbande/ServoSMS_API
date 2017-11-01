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
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for Payment_Receipt.
	/// </summary>
	public partial class Payment_Receipt : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		DBOperations.DBUtil dbobj1=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		double total=0;
		string uid;
		string[] billDetails;
		static string TempAcc_Type="";
		int f1 = 0;
		public static double RecAmt=0;
		static bool PrintFlag=false;
		static double Tot_Rec=0;
		int f2 = 0;

        string strReceiptFromDate = string.Empty;
        string strReceiptToDate = string.Empty;
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
                if (hidReceiptFromDate.Value != "" && hidReceiptToDate.Value != "")
                {
                    fillReceiptNoDropdown();
                }
                //string pass;
                uid =(Session["User_Name"].ToString());
				Last_ID();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:Page_Load" + ex.Message+" EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}

            txtReceivedDate.Text = Request.Form["txtReceivedDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtReceivedDate"].ToString().Trim();
            if (!IsPostBack)
			{
				try
				{
					txtReceivedDate.Text = DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
					PanReceiptNo.Visible=false;
					txtDate.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
					PanBankInfo.Visible=false;
					//panBankName.Visible=false;
					checkPrevileges();
					checkAccount();
					InventoryClass  obj=new InventoryClass ();
					SqlDataReader SqlDtr;
					string sql;
					//object op = null;
					GetBank();
					DropMode.Enabled=true;
					DropBankName.Enabled=true;
					lblAmountinWords.Text="0/-";
					Invoice_Date="";
					Acc_Date="";

                    #region Fetch All Customer Name & Account Name and fill in the ComboBox
                    ArrayList CustName = new ArrayList();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/FetchCustomerNames").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            CustName = JsonConvert.DeserializeObject<ArrayList>(disc);
                        }
                    }
					for(int i=0;i<CustName.Count;i++)
					{
						texthiddenprod.Value+=CustName[i].ToString()+",";
					}
					if(texthiddenprod.Value.IndexOf(",")>0)
						texthiddenprod.Value=texthiddenprod.Value.Substring(0,texthiddenprod.Value.Length-1);
                    #endregion

                    #region Fetch All Discount From Ledger Master and fill in the ComboBox
                    List<string> discount = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/FetchDiscount").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            discount = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                    }

					DropDiscount1.Items.Clear();
					DropDiscount1.Items.Add("Select");
					DropDiscount2.Items.Clear();
					DropDiscount2.Items.Add("Select");
                    if (discount != null && discount.Count > 0)
                    {
                        foreach (var dis in discount)
                        {
                            DropDiscount1.Items.Add(dis);
                            DropDiscount2.Items.Add(dis);
                        }
                    }
					if(DropDiscount1.Items.Count>1)
					{
						DropDiscount1.SelectedIndex=1;
						txtDisc1.Enabled=true;
					}
					else
						txtDisc1.Enabled=false;
					if(DropDiscount2.Items.Count>2)
					{
						DropDiscount2.SelectedIndex=2;
						txtDisc2.Enabled=true;
					}
					else
						txtDisc2.Enabled=false;
                    #endregion
                    //InventoryClass obj = new InventoryClass();
                    string DateFrom = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetOrgDate").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            DateFrom = JsonConvert.DeserializeObject<string>(disc);
                        }
                    }
					if(DateFrom!=null)
					{
						Acc_Date=GenUtil.trimDate(DateFrom);
					}
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:FuelPerchase.aspx,Method:Payment_Receipt. EXCEPTION  "+ex.Message +" userid "+uid);
				}
			}
		}
		
		/// <summary>
		/// This method checks the privilegs of the user.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="1";
			string SubModule="3";
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
			if(Add_Flag=="0" && View_flag=="0" && Edit_Flag=="0")
			{
				//string msg="UnAthourized Visit to Payment Receipt Page";
				//dbobj.LogActivity(msg,Session["User_Name"].ToString());  
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}
			if(Add_Flag == "0")
				btnSave.Enabled = false;  
			if(Edit_Flag == "0")
				btnEdit.Enabled = false;  
			#endregion
		}

        /// <summary>
        /// This method checks the cash and bank accounts are present or not , if not then display the message on screen.
        /// </summary>
        public void checkAccount()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/ReceiptController/CheckCashAccount").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        f1 = JsonConvert.DeserializeObject<int>(disc);
                    }
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/ReceiptController/CheckBankAccount").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        f2 = JsonConvert.DeserializeObject<int>(disc);
                    }
                }

                if (f1 == 0 && f2 == 0)
                    lblMessage.Text = "Cash and Bank Accounts are not created";
                else
                {
                    if (f1 == 0)
                        lblMessage.Text = "Cash Account not created";
                    if (f2 == 0)
                        lblMessage.Text = "Bank Account not created";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:FuelPerchase.aspx,Method:checkAccount(). EXCEPTION  " + ex.Message + " userid " + uid);
            }
        }

		/// <summary>
		/// returns MM/DD/YYYY date format.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public DateTime ToMMddYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];
			strarr=str.IndexOf("/")>0? str.Split(new char[] { '/' }, str.Length):str.Split(new char[]{'-'},str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);
			return(dt);
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
			this.GridDuePayment.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.GridDuePayment_ItemDataBound);

		}
		#endregion 

		/// <summary>
		/// To view the customer biling information according to select the Customer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DropCustName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This method is used to fatch the customer balance info 
		/// </summary>
		public void getRecInfo()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr,rdr;
			string sql;
			string Cust_ID="";
			
			txtFinalDues.Text="";
			//txtRecAmount.Text="";
			Textbox2.Text =""; 
			//Textbox1.Text = "";
			if(DropCustName.Value =="Select")
				return;
			CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:DropCustName_SelectedIndexChanged " +uid);

			#region Fetch Place of Customer Regarding Customer Name
			//string str=DropCustName.Value; //Coment by vikas sharma 30.04.09
			/************add by vikas sharma 30.04.09************************/
			string DropValue=DropCustName.Value;
			string str=DropValue.Substring(0,DropValue.IndexOf(";"));
            /*************end***********************/
            string city = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/ReceiptController/FetchCustomerPlace?Value="+ DropValue).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    city = JsonConvert.DeserializeObject<string>(disc);
                }
            }

            if (str.IndexOf(":") > 0)
            {
                DropDiscount1.Enabled = false;
                DropDiscount2.Enabled = false;
                txtDisc1.Enabled = false;
                txtDisc2.Enabled = false;
            }
            txtCity.Text = city;
            #endregion

   //         string _CustName;
			//string _City;
				
			////_CustName=DropCustName.Value;  //Comment by vikas sharma 30.04.09
			//_CustName=str.ToString();
			//_City=txtCity.Text;

            CustomerModel customer = new CustomerModel();
            customer.CustomerName= str.ToString();
            customer.City = txtCity.Text;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var myContent = JsonConvert.SerializeObject(customer);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/ReceiptController/GetCustomerID", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    Cust_ID = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                }
            }

            if (DropReceiptNo.Visible==false)
			{	
				// Disable the Bill Details and Total Due amount fields for the Ledger.
				//*****dbobj.SelectQuery("select Ledger_ID  from Cust_Ledger where Party_Name  = '"+_CustName+"' and Ledger_Id != ''",ref SqlDtr); 
				//*****if(SqlDtr.HasRows)
				//if(DropCustName.Value.IndexOf(":")>0) //Comment by vikas sharma 30.04.09
				if(str.IndexOf(":")>0)
				{
					Textbox3.Text="";
					txtCr.Text = "";
					GridDuePayment.Visible = false;
					txtTotalBalance.Enabled = false;
					txtTotalBalance.Text =""; 
					Textbox2.Enabled = false;
					Textbox3.Enabled = false; 
					txtCr.Enabled = false;
					txtFinalDues.Enabled = false;
					txtTotalBalance.Text ="0";
					DropDiscount1.Enabled=false;
					DropDiscount2.Enabled=false;
					DropDiscount1.Enabled=false;
					DropDiscount2.Enabled=false;
					txtDisc1.Enabled=false;
					txtDisc2.Enabled=false;
				}
				else
				{
					GridDuePayment.Visible = true;
					txtTotalBalance.Enabled = true;
					Textbox2.Enabled = true;
					Textbox3.Enabled = true; 
					txtCr.Enabled = true;
					txtFinalDues.Enabled = true;
					DropDiscount1.Enabled=false;
					DropDiscount2.Enabled=false;
					DropDiscount1.Enabled=true;
					DropDiscount2.Enabled=true;
					txtDisc1.Enabled=true;
					txtDisc2.Enabled=true;
                    //object op =null;
                    //dbobj.ExecProc(OprType.Insert,"Test",ref op,"@Cust_ID",Cust_ID);

                    #region Bind DataGrid
                    LedgerDetailsModel ledgerDetails = new LedgerDetailsModel();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetLedgerDetailsData?CustomerID=" + Cust_ID).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            ledgerDetails = JsonConvert.DeserializeObject<LedgerDetailsModel>(disc);
                        }
                    }

                    //SqlDtr=obj.GetRecordSet("select substring(cast(bill_no as varchar),4,9) as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '"+Cust_ID+"' and Amount > 0");
                    SqlDtr = obj.GetRecordSet("select bill_no as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '" + Cust_ID + "' and Amount > 0 order by Bill_Date");
                    GridDuePayment.DataSource = SqlDtr;
                    GridDuePayment.DataBind();
                    SqlDtr.Close();
                    #endregion

                    txtTotalBalance.Text =total.ToString();
				}
				txtRecAmount.Text="";
			}
			else
			{
				DropDiscount1.Enabled=true;
				DropDiscount2.Enabled=true;
				txtDisc1.Enabled=true;
				txtDisc2.Enabled=true;
				#region Bind DataGrid
				//SqlDtr=obj.GetRecordSet("select substring(cast(bill_no as varchar),4,9) as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '"+Cust_ID+"' and Amount > 0");
				SqlDtr=obj.GetRecordSet("select bill_no as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '"+Cust_ID+"' and Amount > 0 order by Bill_Date");
				GridDuePayment.DataSource=SqlDtr;
				GridDuePayment.DataBind();
				SqlDtr.Close();  
				#endregion 
				txtTotalBalance.Text =total.ToString();
			}
			//checkPrevileges(); 
		}

		/// <summary>
		/// This method is call at the binding of datagrid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GridDuePayment_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if(e.Item.Cells[2].Text!="Amount" && e.Item.Cells[2].Text!="&nbsp;")
					//total=total+System.Convert.ToDouble(e.Item.Cells[2].Text.ToString());
					total+=System.Convert.ToDouble(e.Item.Cells[2].Text.ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:GridDuePayment_ItemDataBound " + ex.Message+"  EXCEPTION "+uid);
			}
		}

		/// <summary>
		/// Sends the text file to print server to print.
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\PaymentReceiptReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:Print   Payment Receipt Print   userid  "+uid );
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:Print   Payment Receipt Print  Exception  "+ane.Message+"  userid  "+uid );
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:Print   Payment Receipt Print  Exception  "+se.Message+"  userid  "+uid );	
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:Print   Payment Receipt Print  Exception  "+es.Message+"  userid  "+uid );	
				}
			} 
			catch(Exception ex)
			{
				//CreateLogFiles Err = new CreateLogFiles();
				//Err.ErrorLog(Server.MapPath("Logs/ErrorLog"),"Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:btnSaved_Clicked " + ex.Message);
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:print  Payment Receipt Print " + ex.Message+" EXCEPTION  "+uid);
			}
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private string GetString(string str,string spc)
		{
			if(str.Length>spc.Length)
				return str;
			else
				return str+spc.Substring(0,spc.Length-str.Length)+"  ";			
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6,ref int len7,ref int len8)
		{
			while(rdr.Read())
			{
				if(rdr["Name"].ToString().Trim().Length>len1)
					len1=rdr["Name"].ToString().Trim().Length;					
				if(rdr["City"].ToString().Trim().Length>len2)
					len2=rdr["City"].ToString().Trim().Length;					
				if(rdr["Rupees"].ToString().Trim().Length>len3)
					len3=rdr["Rupees"].ToString().Trim().Length;					
				if(rdr["TotalAmt"].ToString().Trim().Length>len4)
					len4=rdr["TotalAmt"].ToString().Trim().Length;	
				if(rdr["RecpMode"].ToString().Trim().Length>len5)
					len5=rdr["RecpMode"].ToString().Trim().Length;	
				if(rdr["RecpAmount"].ToString().Trim().Length>len6)
					len6=rdr["RecpAmount"].ToString().Trim().Length;	
				if(rdr["FinalDueMode"].ToString().Trim().Length>len7)
					len7=rdr["FinalDueMode"].ToString().Trim().Length;	
				if(rdr["FinalDuePay"].ToString().Trim().Length>len8)
					len8=rdr["FinalDuePay"].ToString().Trim().Length;	
			}
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private string GetString(string str,int maxlen,string spc)
		{		
			return str+spc.Substring(0,maxlen>str.Length?maxlen-str.Length:str.Length-maxlen);
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private string MakeString(int len)
		{
			string spc="";
			for(int x=0;x<len;x++)
				spc+=" ";
			return spc;
		}
		// End Report
		// This function prepares the report Payment_ReceiptReport.txt file. takes argument int
		// 1. Customer Report
		// 2. Report for the Ledger.
		
		/// <summary>
		/// This method prepares the report and writes into a Text File to print. info string is used to display print the values in specified formats.
		/// </summary>
		/// <param name="f"></param>
		public void MakingReport(int f)
		{
			InventoryClass  obj = new InventoryClass();
			SqlDataReader SqlDtr;
			string info1="";
			string Cust_ID = "";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\Payment_ReceiptReport.txt";
			int flag1=0,flag2=0,flag3=0;
			StreamWriter sw = new StreamWriter(path);
			
			//string strCustName = DropCustName.Value;
			string strCustName = DropCustName.Value.Substring(0,DropCustName.Value.IndexOf(";"));
			string strCustCity  = txtCity.Text;
			if(f == 1)
			{
				string sql="select Cust_ID  from Customer where Cust_Name='"+ strCustName+"' and City = '"+strCustCity+"'";  
				SqlDtr = obj.GetRecordSet(sql);
				if(SqlDtr.Read())
					Cust_ID =SqlDtr.GetValue(0).ToString();
				else
					Cust_ID = "0";
				SqlDtr.Close ();

				int i=0;
				int count = 0;
				SqlDtr=obj.GetRecordSet("select Bill_No as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '"+Cust_ID+"' and Amount > 0");
				while(SqlDtr.Read())
				{
					count++;
					count++;
					count++;
				}
				SqlDtr.Close();
				if(count == 0)
				{
					billDetails = new string[3];
					billDetails[0] = "";
					billDetails[1] = "";
					billDetails[2] = "";
				}
				else
					billDetails = new string[count];
				SqlDtr=obj.GetRecordSet("select Bill_No as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '"+Cust_ID+"' and Amount > 0");
		
				while(SqlDtr.Read())
				{
					billDetails[i]= SqlDtr.GetValue(0).ToString();
					i++;  
					billDetails[i]= SqlDtr.GetValue(1).ToString();
					i++;
					billDetails[i]= SqlDtr.GetValue(2).ToString();
					i++;
				}
				SqlDtr.Close(); 
			}
			else
			{
				string[] name = strCustName.Split(new char[] {':'},strCustName.Length);
				strCustName = name[0].ToString();
				strCustCity = "";
				billDetails = new string[3];
				billDetails[0] = "";
				billDetails[1] = "";
				billDetails[2] = "";
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
			sw.WriteLine("");
			//**********
			string des="------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("=========",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Receipt",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("=========",des.Length));
			sw.WriteLine("");
			sw.WriteLine("                                                            Date: "+DateTime.Now.Day.ToString()+"/"+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString()   );
			sw.WriteLine("");
			sw.WriteLine("Recieved with thanks from "+strCustName+" "+strCustCity);
			sw.WriteLine("The sum of Rupees "+lblAmountinWords.Text+" in Full/Part");
			sw.WriteLine("payment against Bill details given on account of your supply.");
			sw.WriteLine("");
			sw.WriteLine("+--------------------------------+---------------------------+----------------+");
			sw.WriteLine("|           Due Payment          |     Recieved Payment      |Final Dues After|");
			sw.WriteLine("+-------+-----------+------------+-------------+-------------+    Payment     |");
			sw.WriteLine("|Bill No| Bill Date |   Amount   |    Mode     |   Amount    |                |");
			sw.WriteLine("+-------+-----------+------------+-------------+-------------+----------------+");
			//|O/B    |4/26/2006  |   111100.00|Cash                1500.00|Dr.      8650.00|

			info1 = "|{0,-6:F} |{1,-10:S} | {2,10:F} |{3,-12:S}    {4,10:F} |{5,-3:S}  {6,10:F} |";
			string info4 = "|{0,-6:F} |{1,-10:S} | {2,10:F} |{3,-12:S}    {4,-27:F} |";
			string strDate =billDetails[1];
			int pos = strDate.IndexOf(" ");
				
			if(pos != -1)
			{
				strDate = strDate.Substring(0,pos);
			}
			else
			{
				strDate = "";					
			}
			if(f==1)
				sw.WriteLine(info1,billDetails[0],GenUtil.str2DDMMYYYY(strDate),GenUtil.strNumericFormat(billDetails[2]),DropMode.SelectedItem.Value,GenUtil.strNumericFormat(txtRecAmount.Text.ToString()),txtCr.Text ,GenUtil.strNumericFormat(txtFinalDues.Text.ToString()) );
			else
				sw.WriteLine(info1,billDetails[0],strDate,billDetails[2],DropMode.SelectedItem.Value,GenUtil.strNumericFormat(txtRecAmount.Text.ToString()),"" ,"" );

			string	info2 = "|{0,-6:F} |{1,-10:S} | {2,10:F} |                           |                |";
			if(billDetails.Length >3 )
			{
				for (int j=3;j<billDetails.Length-2 ;j=j+3)  
				{
					strDate = billDetails[j+1];
					pos = strDate.IndexOf(" ");
				
					if(pos != -1)
					{
						strDate = strDate.Substring(0,pos);
					}
					else
					{
						strDate = "";					
					}
					string test = billDetails[j+2];
					
					if(!DropMode.SelectedItem.Text.Equals("Cash"))
					{
						if(flag1==0)
						{
							sw.WriteLine(info4,billDetails[j],GenUtil.str2DDMMYYYY(strDate),GenUtil.strNumericFormat(billDetails[j+2]),"Name of Bank",DropBankName.SelectedItem.Text);
							flag1=1;
						}
						else if(flag2==0)
						{
							sw.WriteLine(info1,billDetails[j],GenUtil.str2DDMMYYYY(strDate),GenUtil.strNumericFormat(billDetails[j+2]),"Cheque No",txtChequeno.Text,"","");
							flag2=1;
						}
						else if(flag3==0)
						{
							sw.WriteLine(info1,billDetails[j],GenUtil.str2DDMMYYYY(strDate),GenUtil.strNumericFormat(billDetails[j+2]),"Cheque Date",txtDate.Text,"","");
							flag3=1;
						}
						else
							sw.WriteLine(info2,billDetails[j],GenUtil.str2DDMMYYYY(strDate),GenUtil.strNumericFormat(billDetails[j+2]) );
					}
					else
						sw.WriteLine(info2,billDetails[j],GenUtil.str2DDMMYYYY(strDate),GenUtil.strNumericFormat(billDetails[j+2]) );

				}
			}
			if(!DropMode.SelectedItem.Text.Equals("Cash"))
			{
				if(flag1==0)
				{
					sw.WriteLine(info4,"","","","Name of Bank",DropBankName.SelectedItem.Text);
					flag1=1;
				}
				if(flag2==0)
				{
					sw.WriteLine(info1,"","","","Cheque No",txtChequeno.Text,"","");
					flag2=1;
				}
				if(flag3==0)
				{
					sw.WriteLine(info1,"","","","Cheque Date",txtDate.Text,"","");
					flag3=1;
				}
			}
			sw.WriteLine("+-------+-----------+------------+---------------------------+----------------+");
			string info3 = "|             Total : {0,10:F} |                {1,10:F} |{2,-3:S}  {3,10:F} |";
			if(f==1)
				sw.WriteLine(info3,GenUtil.strNumericFormat(txtTotalBalance.Text.ToString()),GenUtil.strNumericFormat(Textbox1.Text.ToString() ),Textbox3.Text,GenUtil.strNumericFormat(Textbox2.Text.ToString ()) );
			else
				sw.WriteLine(info3,"",GenUtil.strNumericFormat(Textbox1.Text.ToString() ),"","" );
			sw.WriteLine("+--------------------------------+---------------------------+----------------+");
			string info5 = "|Narration : {0,-65:S}|";
			sw.WriteLine(info5,GenUtil.TrimLength(txtNar.Text,65));
			sw.WriteLine("+----------+------------------------------------------------------------------+");
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file Payment_Receipt.txt for printing.
		/// </summary>
		public void MakingReport()
		{
			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\PaymentReceiptReport.txt";
				StreamWriter sw = new StreamWriter(path);
				string info=" {0,-14:S} {1,-50:S} {2,20:S} {3,-46:S}";//Party Name & Address
				string info1=" {0,-22:S} {1,-22:S} {2,-15:S} {3,-50:S} {4,20:S}";
				string info2=" {0,16:S} {1,-114:S}";
				//string info3=" {0,7:S} {1,-100:S}";
				string info3=" {0,-14:S} {1,-50:S} {2,20:S} {3,46:S}";//Party Name & Address
				
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
				sw.WriteLine("                                                         RECEIPT");
				for(int i=0;i<8;i++)
				{
					sw.WriteLine("");
				}
				string addr="",city="";
                //string name=DropCustName.Value; //comment by vikas sharma 02.05.09
				string name=DropCustName.Value.Substring(0,DropCustName.Value.IndexOf(";"));
				if(name.IndexOf(":")>0)
				{}
				else
				{
					SqlDataReader rdr=null;
					//string str = "select address,city from customer,ledger_master where ledger_name=cust_name and ledger_name='"+DropCustName.Value+"'"; //Comment by vikas sharma 2.05.09
					string str = "select address,city from customer,ledger_master where ledger_name=cust_name and ledger_name='"+name+"'";
					dbobj.SelectQuery(str,ref rdr);
					if(rdr.Read())
					{
						addr = rdr.GetValue(0).ToString();
						city = rdr.GetValue(1).ToString();
					}
					rdr.Close();
				}
					
				if(DropReceiptNo.Visible==true)
					//sw.WriteLine(info,"",DropCustName.Value.ToUpper(),"",DropReceiptNo.SelectedItem.Text); //Comment by vikas sharma 02.05.09
					sw.WriteLine(info,"",name.ToUpper(),"",DropReceiptNo.SelectedItem.Text);
				else
					//sw.WriteLine(info,"",DropCustName.Value.ToUpper(),"",ReceiptNo.ToString());  //Comment by vikas sharma 02.05.09
					sw.WriteLine(info,"",name.ToUpper(),"",ReceiptNo.ToString());
				sw.WriteLine(info,"",addr,"",txtReceivedDate.Text);
				//sw.WriteLine(info,"","","",DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString()+":"+DateTime.Now.Second.ToString());
				sw.WriteLine(info,"",city,"","---");
				sw.WriteLine(info,"","","","---");
				//sw.WriteLine();
				//sw.WriteLine();
				sw.WriteLine(info1,"Mode Of Payment","Cheque / DDNo","Date","Bank","Amount  ");
				sw.WriteLine();
				sw.WriteLine();
				//sw.WriteLine(info1,DropMode.SelectedItem.Text,txtChequeno.Text,txtReceivedDate.Text,txtCustBankName.Text,Textbox1.Text);
				if(DropMode.SelectedItem.Text=="Cash")
                    sw.WriteLine(info1,DropMode.SelectedItem.Text,txtChequeno.Text,txtReceivedDate.Text,txtCustBankName.Text,GenUtil.strNumericFormat(txtRecAmount.Text));
				else
					sw.WriteLine(info1,DropMode.SelectedItem.Text,txtChequeno.Text,txtDate.Text,txtCustBankName.Text,GenUtil.strNumericFormat(txtRecAmount.Text));
				for(int i=0;i<34;i++)
				{
					sw.WriteLine("");
				}
				sw.Write((char)27);//added by vishnu
				sw.Write((char)69);//added by vishnu
				//sw.Write((char)1);//added by vishnu
				sw.WriteLine(info2,"","Narration : "+txtNar.Text);
				sw.Write((char)27);//added by vishnu
				sw.Write((char)70);//added by vishnu
				sw.WriteLine(info3,"","","","Total Amount : "+GenUtil.strNumericFormat(txtRecAmount.Text));
				sw.WriteLine("");
				sw.WriteLine("");
				sw.WriteLine(info2,"",GenUtil.ConvertNoToWord(txtRecAmount.Text));
				sw.WriteLine();
				//sw.WriteLine(info3,"",txtNar.Text);
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:MakingReport"+"   EXCEPTION " +ex.Message +" "+ ex.StackTrace   +uid);
			}
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		public void Save2()
		{
			SqlConnection conMyData;
			string  strInsert;
			SqlCommand cmdInsert;
			conMyData = new SqlConnection( @"Server=localhost;user id=sa;password=;Database=Servosms" );
			strInsert = "Insert PaymentReport(Name,City,Rupees,TotalAmt,RecpMode,RecpAmount,FinalDueMode,FinalDuePay)Values(@Name,@City,@Rupees,@TotalAmt,@RecpMode,@RecpAmount,@FinalDueMode,@FinalDuePay)";
			cmdInsert = new SqlCommand( strInsert, conMyData );
			conMyData.Open();
			cmdInsert.Parameters.Add( "@Name", DropCustName.Value.ToString()); 
			cmdInsert.Parameters.Add( "@City", txtCity.Text.ToString() );
			cmdInsert.Parameters.Add( "@Rupees", lblAmountinWords.Text.ToString());
			cmdInsert.Parameters.Add( "@TotalAmt", txtTotalBalance.Text.ToString());
			cmdInsert.Parameters.Add( "@RecpMode", DropMode.SelectedItem.Value.ToString());
			cmdInsert.Parameters.Add( "@RecpAmount",txtRecAmount.Text.ToString());
			cmdInsert.Parameters.Add( "@FinalDueMode", txtCr.Text.ToString());
			cmdInsert.Parameters.Add( "@FinalDuePay", txtFinalDues.Text.ToString());
			cmdInsert.ExecuteNonQuery();
			conMyData.Close();
		}

		/// <summary>
		/// To insert all values in the database with the help of stored procedures.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
                var dt = System.Convert.ToDateTime(Acc_Date);
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtReceivedDate"].ToString()));
                if (DateTime.Compare(dt, dt2) > 0)
                {
                    MessageBox.Show("Please Select Date Must be Greater than Opening Date");
                }
                else
                {
                    string s = txtRecAmount.Text;
                    SaveUpdate();
                }
				Last_ID();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:btnSave_Click "+"   EXCEPTION " +ex.Message +" "+ ex.StackTrace   +uid);
			}
		}

        /// <summary>
        /// This method is used to save or update the record.
        /// </summary>
        public void SaveUpdate()
        {
            try
            {
                //string[] arrSubReceipt={"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
                InventoryClass obj = new InventoryClass();
                SqlDataReader SqlDtr = null;
                ArrayList UpdateLedgerID = new ArrayList();
                ArrayList UpdateCustomerID = new ArrayList();
                double rec_amount;
                //double Amount=0;
                //double balance;
                string Receipt = "Save";
                string Acc_Type = "";
                string _CustName;
                string _City;
                string Cust_ID = "";
                //int f = 0;
                //int z=0;
                //_CustName=DropCustName.Value; //Comment by vikas sharma 30.04.09
                _CustName = DropCustName.Value.Substring(0, DropCustName.Value.IndexOf(";"));
                _City = txtCity.Text;
                checkAccount();
                if (DropMode.SelectedItem.Text.Equals("Cash"))
                {
                    Acc_Type = "Cash in hand";
                    if (f1 == 0)
                    {
                        MessageBox.Show("Cash Account not created");
                        return;
                    }
                }
                else
                {
                    Acc_Type = "Bank";
                    if (f2 == 0)
                    {
                        MessageBox.Show("Bank Account not created");
                        return;
                    }
                }
                if (f1 == 0 && f2 == 0)
                {
                    MessageBox.Show("Cash and Bank Accounts are not created");
                    return;
                }
                string sql = "";
                string[] strName = new string[2];
                if (_CustName.IndexOf(":") > 0)
                {
                    strName = _CustName.Split(new char[] { ':' }, _CustName.Length);
                    Cust_ID = strName[1].ToString();
                    UpdateLedgerID.Add(strName[1].ToString());
                }
                else
                {
                    strName[0] = _CustName;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetLedgerID?LedgerName=" + strName[0].ToString()).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            Cust_ID = JsonConvert.DeserializeObject<string>(disc);
                            UpdateLedgerID.Add(Cust_ID);
                        }
                    }
                   
                }
                if (PanReceiptNo.Visible == true)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetCustomerID?LedgerName=" + strName[0].ToString()).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            var custID = JsonConvert.DeserializeObject<string>(disc);
                            UpdateCustomerID.Add(custID);
                        }
                    }

                }
                obj.Receipt = "Save";
                if (PanBankInfo.Visible == true)
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetLedgerIDByBank?Bank=" + DropBankName.SelectedItem.Text).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            var bankName = JsonConvert.DeserializeObject<string>(disc);
                            obj.BankName = bankName;
                            Acc_Type = bankName;
                            UpdateLedgerID.Add(bankName);
                        }
                    }
                }
                else
                {
                    obj.BankName = "";
                }
                dbobj.SelectQuery("select Ledger_ID from Ledger_Master lm, Ledger_Master_sub_grp lmsg where lm.sub_grp_id = lmsg.sub_grp_id and  lmsg.sub_grp_name = 'Cash in hand'", ref SqlDtr);
                if (SqlDtr.Read())
                {
                    UpdateLedgerID.Add(SqlDtr.GetValue(0).ToString());
                }
                SqlDtr.Close();

                //obj.BankName=txtBankName.Text.Trim().ToString();
                obj.ChequeNo = txtChequeno.Text.Trim().ToString();
                obj.Mode = DropMode.SelectedItem.Text;
                obj.ChequeDate = GenUtil.str2DDMMYYYY(txtDate.Text.Trim().ToString());
                obj.cust_id = Cust_ID;
                obj.Narration = txtNar.Text;
                obj.discount = txtDisc1.Text;
                obj.Discount = txtDisc2.Text;
                obj.CustBankName = txtCustBankName.Text;
                obj.Invoice_Date = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString());
                string DiscID1 = "0", DiscID2 = "0";


                if (DropDiscount1.SelectedIndex == 0)
                    obj.discountid1 = "";
                else
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetLedgerIDByDiscount?Discount=" + DropDiscount1.SelectedItem.Text).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            var discount = JsonConvert.DeserializeObject<string>(disc);
                            obj.discountid1 = discount;
                            DiscID1 = discount;
                            if (txtDisc1.Text != "")
                                UpdateLedgerID.Add(discount);
                        }
                    }
                }

                if (DropDiscount2.SelectedIndex == 0)
                    obj.discountid2 = "";
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/ReceiptController/GetLedgerIDByDiscount2?Discount=" + DropDiscount2.SelectedItem.Text).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            var discount = JsonConvert.DeserializeObject<string>(disc);
                            obj.discountid2 = discount;
                            DiscID2 = discount;
                            if (txtDisc2.Text != "")
                                UpdateLedgerID.Add(discount);
                        }
                    }
                }


                if (txtDisc1.Text == "")
                    obj.discountid1 = "";
                if (txtDisc2.Text == "")
                    obj.discountid2 = "";
                //********** Add This code by Mahesh On 05.07.008 **********************************

                /*Coment by vikas 12.4.2013 string DisType1 = "", DisType2 = "";
                if(DropDiscount1.SelectedIndex!=0)
                {
                    DisType1 = DropDiscount1.SelectedItem.Text.Substring(0,1);
                    DisType1 +="D";
                }
                if(DropDiscount2.SelectedIndex!=0)
                {
                    DisType2 = DropDiscount2.SelectedItem.Text.Substring(0,1);
                    DisType2 +="D";
                }
                */

                /******Add by vikas 12.4.2013*******************/
                string DisType1 = "", DisType2 = "";
                if (DropDiscount1.SelectedIndex != 0)
                {
                    DisType1 = DropDiscount1.SelectedItem.Text.Substring(0, 1);
                    DisType1 += "D";
                }
                else
                {
                    if (DiscLedgerName1 != "")
                    {
                        DisType1 = DiscLedgerName1.Substring(0, 1);
                        DisType1 += "D";
                    }

                }

                if (DropDiscount2.SelectedIndex != 0)
                {
                    DisType2 = DropDiscount2.SelectedItem.Text.Substring(0, 1);
                    DisType2 += "D";
                }
                else
                {
                    if (DiscLedgerName2 != "")
                    {
                        DisType2 = DiscLedgerName2.Substring(0, 1);
                        DisType2 += "D";
                    }

                }
                /*********end****************/

                double TotalAmt = 0;
                if (txtRecAmount.Text != "")
                    TotalAmt += double.Parse(txtRecAmount.Text);
                if (txtDisc1.Text != "")
                    TotalAmt += double.Parse(txtDisc1.Text);
                if (txtDisc2.Text != "")
                    TotalAmt += double.Parse(txtDisc2.Text);
                int OldCustID = 0;
                PaymentReceiptModel receipt = new PaymentReceiptModel();
                receipt.PanReceiptNo = PanReceiptNo.Visible;
                if (PanReceiptNo.Visible == true)
                    receipt.CustomerID = customerID;
                else
                    receipt.CustomerID2 = strName[0].ToString();



                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(receipt);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/ReceiptController/GetCustID", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        customerID = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    }
                }

                GetNextReceiptNo();
              
                if (_CustName.IndexOf(":") > 0)
                {
                    rec_amount = 0;
                    rec_amount = System.Convert.ToDouble(txtRecAmount.Text);
                   
                    MakingReport();
                    
                    //call procedure to insert the record into payment_Receipt and voucher_transaction tables.
                    PaymentReceiptModel payment = new PaymentReceiptModel();
                    payment.PanReceiptNo = PanReceiptNo.Visible;

                    if (PanReceiptNo.Visible == true)
                    {
                        payment.ReceiptNo = DropReceiptNo.SelectedItem.Text;
                        payment.Discount1 = DisType1;
                        payment.Discount2 = DisType2;
                        payment.CustomerID = OldCustID.ToString();

                        if (DropMode.SelectedItem.Text == "Cash")
                        {
                            payment.Cust_ID = Cust_ID;
                            payment.Amount = TotalAmt;
                            payment.AccountType = Acc_Type;
                            payment.Mode = DropMode.SelectedItem.Text;
                            payment.RecDate = GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString();
                            payment.ChequeDate = GenUtil.str2DDMMYYYY(txtDate.Text);
                            payment.ReceiptNo = DropReceiptNo.SelectedItem.Text;

                        }
                        else
                        {
                            payment.Cust_ID = Cust_ID;
                            payment.Amount = TotalAmt;
                            payment.AccountType = Acc_Type;
                            payment.ChequeNumber = txtChequeno.Text;
                            payment.ChequeDate = GenUtil.str2DDMMYYYY(txtDate.Text);
                            payment.Mode = DropMode.SelectedItem.Text;
                            payment.Narration = txtNar.Text;
                            payment.CustBankName = txtCustBankName.Text;
                            payment.RecDate = GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString();
                            payment.ReceiptNo = DropReceiptNo.SelectedItem.Text;
                        }
                    }
                    else
                    {
                        payment.ReceiptNo = ReceiptNo.ToString();
                    }
                   
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(payment);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/ReceiptController/InsertPayment", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                        }
                    }
                }
                else
                {
                    //f = 1 ;
                    //MakingReport(f);
                    MakingReport();

                    object op = null;
                    PaymentReceiptModel payment = new PaymentReceiptModel();
                    payment.PanReceiptNo = PanReceiptNo.Visible;
                    payment.ReceiptNo = DropReceiptNo.SelectedItem.Text;
                    payment.DiscountType1 = DisType1;
                    payment.DiscountType2 = DisType2;
                    payment.Cust_ID = OldCustID.ToString();
                    payment.ReceivedAmount = txtRecAmount.Text;
                    payment.TotalRec = Tot_Rec.ToString();
                    payment.CustomerID = customerID;


                    if (PanReceiptNo.Visible == true)
                    {
                        int x = 0;
                        //dbobj.ExecuteScalar("select cust_id from customer where cust_name=(select ledger_name from ledger_master where ledger_id = '"+customerID+"')",ref OldCustID);
                        if (customerID != Cust_ID)
                        {
                            UpdateLedgerID.Add(customerID);
                            UpdateCustomerID.Add(OldCustID);
                        }
                        //dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt ("+DropReceiptNo.SelectedItem.Text+")' and Ledger_ID = '"+customerID+"'",ref x);
                        dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt (" + DropReceiptNo.SelectedItem.Text + ")'", ref x);
                        dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + DisType1 + " (" + DropReceiptNo.SelectedItem.Text + ")'", ref x);
                        dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_" + DisType2 + " (" + DropReceiptNo.SelectedItem.Text + ")'", ref x);
                        //dbobj1.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Payment Received("+DropReceiptNo.SelectedItem.Text+")' and CustID='"+OldCustID+"'",ref x);
                        dbobj1.Insert_or_Update("delete from CustomerLedgerTable where Particular = 'Payment Received(" + DropReceiptNo.SelectedItem.Text + ")'", ref x);
                        dbobj1.Insert_or_Update("delete from Payment_Receipt where Receipt_No='" + DropReceiptNo.SelectedItem.Text + "'", ref x);

                        int Curr_Credit = 0;
                        int Credit_Limit = 0;
                        dbobj.ExecuteScalar("Select Cr_Limit from customer where Cust_ID = '" + OldCustID + "'", ref Credit_Limit);
                        dbobj.ExecuteScalar("Select Curr_Credit from customer where Cust_ID = '" + OldCustID + "'", ref Curr_Credit);
                        //12.09.09 coment by vikas if(Curr_Credit < Credit_Limit)
                        if (Curr_Credit <= Credit_Limit)
                        {
                            Curr_Credit = Curr_Credit + int.Parse(txtRecAmount.Text);

                            Curr_Credit = Curr_Credit - int.Parse(Tot_Rec.ToString());         //Add by vikas 12.09.09

                            if (@Curr_Credit >= @Credit_Limit)
                                dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Credit_Limit + "' where Cust_ID  = '" + customerID + "'", ref x);
                            else
                                dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Curr_Credit + "' where Cust_ID  = '" + customerID + "'", ref x);
                        }
                        /****************** Add by vikas 12.09.09 **************************************************/
                        else
                        {
                            Curr_Credit = Curr_Credit + int.Parse(txtRecAmount.Text);                                   //Add by vikas 12.09.09
                            Curr_Credit = Curr_Credit - int.Parse(Tot_Rec.ToString());                                  //Add by vikas 12.09.09
                            dbobj1.Insert_or_Update("update customer set Cr_Limit = '" + Curr_Credit + "' where Cust_ID  = '" + customerID + "'", ref x);
                        }
                        /*******************************************************************************************/
                        payment.ReceiptNo= DropReceiptNo.SelectedItem.Text; 
                        payment.SubReceiptNo= "A" + DropReceiptNo.SelectedItem.Text;
                        payment.InvoiceNo = "";
                        payment.ReceivedAmount = TotalAmt.ToString();
                        payment.ActualAmount = System.Convert.ToString(double.Parse(txtRecAmount.Text));

                        obj.InsertPaymentReceived();
                        payment.CustomerName = _CustName;
                        payment.City = txtCity.Text.ToString();
                        payment.Amount = TotalAmt;
                        payment.AccountType = Acc_Type;
                        payment.Receipt = Receipt;
                        payment.ReceiptNo = DropReceiptNo.SelectedItem.Text;
                        payment.RecDate = GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString();
                        payment.Cust_ID = Cust_ID;
                        payment.Discount1 = txtDisc1.Text;
                        payment.Discount2 = txtDisc2.Text;
                        payment.DiscountID1 = DiscID1;
                        payment.DiscountID2 = DiscID2;
                        payment.DiscountType1 = DisType1;
                        payment.DiscountType2 = DisType2;

                        //dbobj.ExecProc(OprType.Insert,"ProCustLedgerEntry",ref op,"@Cust_Name",DropCustName.Value,"@City",txtCity.Text.ToString(),"@Amount", TotalAmt,"@Rec_Acc_Type",Acc_Type,"@Receipt",Receipt,"@Receipt_No",DropReceiptNo.SelectedItem.Text,"@ActualAmount",txtRecAmount.Text,"@RecDate",System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtReceivedDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())); //Comment by vikas sharma 30.04.09
                        dbobj.ExecProc(OprType.Insert, "ProCustLedgerEntry", ref op, "@Cust_Name", _CustName, "@City", txtCity.Text.ToString(), "@Amount", TotalAmt, "@Rec_Acc_Type", Acc_Type, "@Receipt", Receipt, "@Receipt_No", DropReceiptNo.SelectedItem.Text, "@ActualAmount", txtRecAmount.Text, "@RecDate", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString()));
                        if (txtDisc1.Text != "" && txtDisc1.Text != "0")
                            dbobj.ExecProc(OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", Cust_ID, "@Receipt", "Save", "@Receipt_No", DropReceiptNo.SelectedItem.Text, "@Amount", txtDisc1.Text, "@Ledger_ID", DiscID1, "@RecDate", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString()), "@DisType", DisType1);
                        if (txtDisc2.Text != "" && txtDisc2.Text != "0")
                            dbobj.ExecProc(OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", Cust_ID, "@Receipt", "Save", "@Receipt_No", DropReceiptNo.SelectedItem.Text, "@Amount", txtDisc2.Text, "@Ledger_ID", DiscID2, "@RecDate", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString()), "@DisType", DisType2);
                    }
                    else
                    {
                        payment.ReceiptNo = ReceiptNo.ToString();
                        payment.SubReceiptNo = "A" + ReceiptNo.ToString();
                        payment.InvoiceNo = "";
                        payment.ReceivedAmount = TotalAmt.ToString();
                        payment.ActualAmount = System.Convert.ToString(double.Parse(txtRecAmount.Text));
                        obj.InsertPaymentReceived();
                        payment.OldCust_ID = OldCustID.ToString();

                        //dbobj.ExecProc(OprType.Insert,"ProCustLedgerEntry",ref op,"@Cust_Name",DropCustName.Value,"@City",txtCity.Text.ToString(),"@Amount", TotalAmt,"@Rec_Acc_Type",Acc_Type,"@Receipt",Receipt,"@Receipt_No",ReceiptNo.ToString(),"@ActualAmount",txtRecAmount.Text,"@RecDate",System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtReceivedDate.Text)+" "+DateTime.Now.TimeOfDay.ToString())); //Comment by Vikas sharma 30.04.09
                        dbobj.ExecProc(OprType.Insert, "ProCustLedgerEntry", ref op, "@Cust_Name", _CustName, "@City", txtCity.Text.ToString(), "@Amount", TotalAmt, "@Rec_Acc_Type", Acc_Type, "@Receipt", Receipt, "@Receipt_No", ReceiptNo.ToString(), "@ActualAmount", txtRecAmount.Text, "@RecDate", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString()));
                        if (txtDisc1.Text != "" && txtDisc1.Text != "0")
                            dbobj.ExecProc(OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", Cust_ID, "@Receipt", "Save", "@Receipt_No", ReceiptNo.ToString(), "@Amount", txtDisc1.Text, "@Ledger_ID", DiscID1, "@RecDate", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString()), "@DisType", DisType1);
                        if (txtDisc2.Text != "" && txtDisc2.Text != "0")
                            dbobj.ExecProc(OprType.Insert, "ProSpacialDiscountEntry", ref op, "@Cust_ID", Cust_ID, "@Receipt", "Save", "@Receipt_No", ReceiptNo.ToString(), "@Amount", txtDisc2.Text, "@Ledger_ID", DiscID2, "@RecDate", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text) + " " + DateTime.Now.TimeOfDay.ToString()), "@DisType", DisType2);

                        /******************Add by vikas Sharma 12.09.09**********************************************/
                        int x = 0;
                        int Curr_Credit = 0;
                        int Credit_Limit = 0;
                        dbobj.ExecuteScalar("Select Cr_Limit from customer where Cust_ID = '" + OldCustID + "'", ref Credit_Limit);
                        dbobj.ExecuteScalar("Select Curr_Credit from customer where Cust_ID = '" + OldCustID + "'", ref Curr_Credit);
                        //12.09.09 coment by vikas if(Curr_Credit < Credit_Limit)
                        if (Curr_Credit < Credit_Limit)
                        {
                            Curr_Credit = Curr_Credit + int.Parse(txtRecAmount.Text);
                            if (@Curr_Credit >= @Credit_Limit)
                                dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Credit_Limit + "' where Cust_ID  = '" + customerID + "'", ref x);
                            else
                                dbobj1.Insert_or_Update("update customer set Curr_Credit = '" + Curr_Credit + "' where Cust_ID  = '" + customerID + "'", ref x);
                        }
                        else
                        {
                            Curr_Credit = Curr_Credit + int.Parse(txtRecAmount.Text);          //Add by vikas 12.09.09
                            Curr_Credit = Curr_Credit - int.Parse(Tot_Rec.ToString());         //Add by vikas 12.09.09
                            dbobj1.Insert_or_Update("update customer set Cr_Limit = '" + Curr_Credit + "' where Cust_ID  = '" + customerID + "'", ref x);
                        }
                        /********************End******************************************************/



                    }


                    //**************************************** End *******************************************
                    //				
                    //***********************

                    if (PanReceiptNo.Visible == true)//Comment by Mahesh on 25.10.008 b'coz this condition is allow insert time also b'coz balance update in insert or update time both.
                    {

                        SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);

                        if (Invoice_Date.IndexOf(" ") > 0)
                        {
                            string[] CheckDate = Invoice_Date.Split(new char[] { ' ' }, Invoice_Date.Length);
                            if (DateTime.Compare(System.Convert.ToDateTime(CheckDate[0].ToString()), System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text))) > 0)
                                Invoice_Date = GenUtil.str2DDMMYYYY(txtReceivedDate.Text);
                            else
                                Invoice_Date = CheckDate[0].ToString();
                        }
                        else
                            Invoice_Date = GenUtil.str2DDMMYYYY(txtReceivedDate.Text);

                        for (int p = 0; p < UpdateLedgerID.Count; p++)
                        {
                            string ss = UpdateLedgerID[p].ToString();
                            dbobj.ExecProc(OprType.Update, "UpdateAccountsLedgerForCustomer", ref op, "@Ledger_ID", UpdateLedgerID[p].ToString(), "@Invoice_Date", Invoice_Date);
                            dbobj.SelectQuery("select cust_id from customer,ledger_master where Cust_Name=Ledger_Name and Ledger_ID='" + UpdateLedgerID[p].ToString() + "'", ref SqlDtr);
                            if (SqlDtr.Read())
                            {
                                dbobj.ExecProc(OprType.Update, "UpdateCustomerLedgerForCustomer", ref op, "@Cust_ID", SqlDtr["Cust_ID"].ToString(), "@Invoice_Date", GenUtil.str2MMDDYYYY(Invoice_Date));
                            }
                        }

                    }//Comment by Mahesh on 25.10.008 b'coz this condition is allow insert time also b'coz balance update in insert or update time both.
                    else
                    {
                        object opp = null;
                        for (int i = 0; i < UpdateLedgerID.Count; i++)
                        {
                            string ss = UpdateLedgerID[i].ToString();
                            dbobj.ExecProc(OprType.Update, "UpdateAccountsLedgerForCustomer", ref opp, "@Ledger_ID", UpdateLedgerID[i].ToString(), "@Invoice_Date", GenUtil.str2DDMMYYYY(txtReceivedDate.Text));
                            dbobj.SelectQuery("select cust_id from customer,ledger_master where Cust_Name=Ledger_Name and Ledger_ID='" + UpdateLedgerID[i].ToString() + "'", ref SqlDtr);
                            if (SqlDtr.Read())
                            {
                                dbobj.ExecProc(OprType.Update, "UpdateCustomerLedgerForCustomer", ref opp, "@Cust_ID", SqlDtr["Cust_ID"].ToString(), "@Invoice_Date", System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtReceivedDate.Text)));
                            }
                        }

                    }
                }
                //*********************add by Mahesh on 16.01.008
                if (DropReceiptNo.Visible == true)
                    MessageBox.Show("Payment Receipt Updated");
                else
                    MessageBox.Show("Payment Receipt Saved");
                Clear();
                CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:btnSaved_Clicked  Payment receipt saved. User_ID: " + uid);
                GridDuePayment.DataSource = null;
                GridDuePayment.DataBind();
                //***********************
                //checkPrevileges();
                PanBankInfo.Visible = false;
                PanReceiptNo.Visible = false;
                btnSave.Text = "Save";
                DropMode.Enabled = true;
                DropBankName.Enabled = true;
                btnPrint.CausesValidation = false;
                PrintFlag = true;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:SaveUpdate " + "   EXCEPTION " + ex.Message + " " + ex.StackTrace + uid);
            }
        }

        /// <summary>
        /// Clears whole form.
        /// </summary>
        public void Clear()
		{
			txtReceivedDate.Text = DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
			txtDate.Text = DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
			customerID="";
			txtCustBankName.Text="";
			GridDuePayment.Columns.Clear();   
			DropCustName.Value="Select";
			DropMode.SelectedIndex=0;
			DropMode.Enabled=true;
			txtCity.Text="";
			lblAmountinWords.Text="0/-";
			txtCr.Text="";  
			txtFinalDues.Text ="";
			txtRecAmount.Text="";
			txtTotalBalance.Text="";
			Textbox1.Text="";
			Textbox2.Text="";
			Textbox3.Text="";
			//txtBankName.Text="";
			PanBankInfo.Visible=false;
			txtChequeno.Text="";
			txtNar.Text="";
			//count=0;
			PanReceiptNo.Visible=false;
			//panBankName.Visible=false;
			DropBankName.SelectedIndex=0;
			txtDisc1.Text="";
			txtDisc2.Text="";
			DropDiscount1.Enabled=true;
			DropDiscount2.Enabled=true;
			txtDisc1.Enabled=true;
			txtDisc2.Enabled=true;
			if(DropDiscount1.Items.Count>1)
			{
				DropDiscount1.SelectedIndex=1;
				txtDisc1.Enabled=true;
			}
			else
				txtDisc1.Enabled=false;
			if(DropDiscount2.Items.Count>2)
			{
				DropDiscount2.SelectedIndex=2;
				txtDisc2.Enabled=true;
			}
			else
				txtDisc2.Enabled=false;
			GridDuePayment.Visible=false;
			DropCustName.Disabled=false;
		}
		
		/// <summary>
		/// Clears whole form.
		/// </summary>
		public void Clear1()
		{
			txtReceivedDate.Text = DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
			txtDate.Text = DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
			customerID="";
			txtCustBankName.Text="";
			//GridDuePayment.Columns.Clear();   
			DropCustName.Value="Select";
			DropMode.SelectedIndex=0;
			DropMode.Enabled=true;
			txtCity.Text="";
			lblAmountinWords.Text="0/-";
			txtCr.Text="";  
			txtFinalDues.Text ="";
			txtRecAmount.Text="";
			txtTotalBalance.Text="";
			Textbox1.Text="";
			Textbox2.Text="";
			Textbox3.Text="";
			txtChequeno.Text="";
			txtNar.Text="";
			DropBankName.SelectedIndex=0;
			txtDisc1.Text="";
			txtDisc2.Text="";
			DropDiscount1.Enabled=true;
			DropDiscount2.Enabled=true;
			txtDisc1.Enabled=true;
			txtDisc2.Enabled=true;
			if(DropDiscount1.Items.Count>1)
			{
				DropDiscount1.SelectedIndex=1;
				txtDisc1.Enabled=true;
			}
			else
				txtDisc1.Enabled=false;
			if(DropDiscount2.Items.Count>2)
			{
				DropDiscount2.SelectedIndex=2;
				txtDisc2.Enabled=true;
			}
			else
				txtDisc2.Enabled=false;
			//GridDuePayment.Visible=false;
		}

		protected void DropMode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DropMode.SelectedItem.Text.Equals("Cash"))
			{
				PanBankInfo.Visible=false;
				//panBankName.Visible=false;
			}
			else
			{
				PanBankInfo.Visible=true;
				//panBankName.Visible=true;
				if(DropBankName.Items.Count>1)
					DropBankName.SelectedIndex=1;
			}
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			if(DateTime.Compare(System.Convert.ToDateTime(Acc_Date),System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtReceivedDate.Text)))>0)
				MessageBox.Show("Please Select Date Must be Greater than Opening Date");
			else
			{
				if(PrintFlag==false)
				{
					SaveUpdate();
				}
				print();
				PrintFlag=false;
				btnPrint.CausesValidation=true;
				Last_ID();
			}
		}

        /// <summary>
        /// This method is used to fill the Receipt No in dropdownlist.
        /// </summary>
        public void fillReceiptNoDropdown()
        {
            PaymentReceiptModel payment = new PaymentReceiptModel();
            strReceiptFromDate = hidReceiptFromDate.Value;
            strReceiptToDate = hidReceiptToDate.Value;
            payment.ReceiptFromDate = GenUtil.str2MMDDYYYY(strReceiptFromDate);
            payment.ReceiptToDate = GenUtil.str2MMDDYYYY(strReceiptToDate);
            Clear();
			GridDuePayment.DataSource = null;
			GridDuePayment.DataBind(); 
			PanReceiptNo.Visible=true;
            List<string> receipts = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var myContent = JsonConvert.SerializeObject(payment);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/ReceiptController/GetReceiptNos", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    receipts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(responseString);
                }
            }
            DropReceiptNo.Items.Clear();
			DropReceiptNo.Items.Add("Select");
            while (receipts != null && receipts.Count > 0)
            {
                foreach (var receipt in receipts)
                    DropReceiptNo.Items.Add(receipt);
            }
			DropCustName.Disabled=true;
			btnPrint.CausesValidation=true;
			Last_ID();
            hidReceiptFromDate.Value = "";
            hidReceiptToDate.Value = "";
        }
		
		public void Last_ID()
		{
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/ReceiptController/GetLastID").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    lblId.Text = JsonConvert.DeserializeObject<string>(disc);
                }
            }
		}
		
		//static int count=0;
		static string customerID="";
		static string DiscLedgerName1="",DiscLedgerName2="",Invoice_Date="",Acc_Date="";
		static double TempDiscAmt1=0,TempDiscAmt2=0;
		
		/// <summary>
		/// This method is used to fatch the particular record according to select the Receipt No from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropReceiptNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DropReceiptNo.SelectedIndex==0)
				return;
			Clear1();
			txtFinalDues.Text="";
			txtRecAmount.Text="";
			Textbox2.Text =""; 
			Textbox1.Text = "";
			string Invoice_No="";
			Invoice_Date="";
			btnSave.Text="Update";
			//count=0;
			string CustName="",Cust_ID="",str="";
			//			if(DropCustName.SelectedIndex ==0)
			//				return;
			InventoryClass  obj=new InventoryClass ();
			InventoryClass  obj1=new InventoryClass ();
			SqlDataReader SqlDtr;
			
			string sql;
			sql="select * from payment_receipt where Receipt_No="+DropReceiptNo.SelectedItem.Text;
			SqlDtr = obj.GetRecordSet(sql);
			SqlDataReader rdr=null;
			double totdisc=0;
            //bool Flag=true;

            //*****while(SqlDtr.Read())

            PaymentReceiptModel receipt = new PaymentReceiptModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/ReceiptController/GetSelectedReceipt?ReceiptNo="+ DropReceiptNo.SelectedItem.Text).Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    receipt = JsonConvert.DeserializeObject<PaymentReceiptModel>(disc);
                }
            }

            if (receipt != null)
            {
                if (receipt.SubReceiptNo == "Deleted")
                {
                    btnSave.Enabled = false;
                    btnEdit.Enabled = false;
                    btnPrint.Enabled = false;
                    btnDel.Enabled = false;
                    DropCustName.Disabled = true;
                    txtNar.Text = "Deleted";
                    txtNar.CssClass = "TextColor";
                    Textbox1.Text = "0";
                    GridDuePayment.Visible = false;
                    return;
                }

                //*****if(Flag==true)
                //*****{
                txtNar.CssClass = "FontStyle";
                btnSave.Enabled = true;
                btnEdit.Enabled = true;
                btnPrint.Enabled = true;
                btnDel.Enabled = true;
                DropCustName.Disabled = false;
                Invoice_No = receipt.InvoiceNo;
                DropMode.SelectedIndex = DropMode.Items.IndexOf(DropMode.Items.FindByValue(receipt.Mode));
                //if (SqlDtr.GetValue(5).ToString() != "")
                {
                    //dbobj.SelectQuery("select Ledger_Name,Ledger_ID from Ledger_Master where Ledger_ID='" + SqlDtr.GetValue(5).ToString() + "'", ref rdr);
                    //if (rdr.Read())
                    {
                        DropBankName.SelectedIndex = DropBankName.Items.IndexOf(DropBankName.Items.FindByValue(receipt.BankName));
                        TempAcc_Type = receipt.LedgerID;
                    }
                    //rdr.Close();
                }
                //else
                //{
                //    DropBankName.SelectedIndex = 0;
                //    TempAcc_Type = "0";
                //}
                //txtBankName.Text=SqlDtr.GetValue(5).ToString();
                txtChequeno.Text = receipt.ChequeNumber;
                txtDate.Text = GenUtil.trimDate(GenUtil.str2DDMMYYYY(receipt.ChequeDate));
                txtReceivedDate.Text = GenUtil.str2DDMMYYYY(GenUtil.trimDate(receipt.RecDate));
                //Invoice_Date=txtReceivedDate.Text.ToString();
                Invoice_Date = receipt.Invoice_Date;
                //********
                totdisc = double.Parse(receipt.ReceivedAmount);
                if (receipt.Discount1 != "")
                    totdisc += double.Parse(receipt.Discount1);
                if (receipt.Discount2 != "")
                    totdisc += double.Parse(receipt.Discount2);
                //********
                str = receipt.ReceivedAmount;
                //str=System.Convert.ToString(double.Parse(SqlDtr.GetValue(4).ToString())-totdisc);
                Cache["RecAmt"] = receipt.ReceivedAmount;
                //Cache["RecAmt"]=System.Convert.ToString(double.Parse(SqlDtr.GetValue(4).ToString())-totdisc);
                Cust_ID = receipt.Cust_ID;
                customerID = receipt.CustomerID;
                txtNar.Text = receipt.Narration;
                txtRecAmount.Text = receipt.ReceivedAmount;
                txtCustBankName.Text = receipt.CustBankName;
                Textbox1.Text = totdisc.ToString();

                Tot_Rec = Convert.ToDouble(totdisc.ToString());                       //Add by vikas 12.09.09

                //dbobj.SelectQuery("select Ledger_Name from Ledger_Master where Ledger_ID='" + SqlDtr["DiscLedgerID1"].ToString() + "'", ref rdr);
                //if (rdr.Read())
                {
                    DropDiscount1.SelectedIndex = DropDiscount1.Items.IndexOf(DropDiscount1.Items.FindByValue(receipt.DiscLedgerID1));
                    DiscLedgerName1 = receipt.DiscLedgerID1;
                }
                //else
                //{
                //    DropDiscount1.SelectedIndex = 0;
                //    DiscLedgerName1 = "";
                //}
                //rdr.Close();
                //dbobj.SelectQuery("select Ledger_Name from Ledger_Master where Ledger_ID='" + SqlDtr["DiscLedgerID2"].ToString() + "'", ref rdr);
                //if (rdr.Read())
                {
                    DropDiscount2.SelectedIndex = DropDiscount2.Items.IndexOf(DropDiscount2.Items.FindByValue(receipt.DiscLedgerID2));
                    DiscLedgerName2 = receipt.DiscLedgerID2;
                }
                //else
                //{
                //    DropDiscount2.SelectedIndex = 0;
                //    DiscLedgerName2 = "";
                //}
                //rdr.Close();
                if (DropDiscount1.SelectedIndex == 0)
                    txtDisc1.Enabled = false;
                else
                    txtDisc1.Enabled = true;
                if (DropDiscount2.SelectedIndex == 0)
                    txtDisc2.Enabled = false;
                else
                    txtDisc2.Enabled = true;
                if (double.Parse(receipt.Discount1) > 0)
                    TempDiscAmt1 = double.Parse(receipt.Discount1);
                else
                    TempDiscAmt1 = 0;
                if (double.Parse(receipt.Discount1) > 0)
                    TempDiscAmt2 = double.Parse(receipt.Discount1);
                else
                    TempDiscAmt2 = 0;
                txtDisc1.Text = receipt.Discount1;
                txtDisc2.Text = receipt.Discount1;
                DropCustName.Disabled = false;

            }
			SqlDtr.Close();
			
			if(DropMode.SelectedItem.Text.Equals("Cash"))
				PanBankInfo.Visible=false;
			else
				PanBankInfo.Visible=true;
			
			#region Fetch Place of Customer Regarding Customer Name
			
			string OldCustomerID = "";
			int FlagFlag=0;
			string City_Name="";
			sql="select Cust_Name,City,cust_id from Ledger_Master l,Customer c where l.ledger_name=c.cust_name and Ledger_ID='"+Cust_ID+"'";
			SqlDtr = obj.GetRecordSet(sql);
			if(SqlDtr.Read())
			{
				CustName=SqlDtr.GetValue(0).ToString();
				if(SqlDtr.GetValue(1).ToString().Equals(""))
				{
					txtCity.Text="";
					City_Name="";
				}
				else
				{
					txtCity.Text=SqlDtr.GetValue(1).ToString();
					City_Name=SqlDtr.GetValue(1).ToString();
				}
				OldCustomerID=SqlDtr.GetValue(2).ToString();
				FlagFlag=1;
			}
			SqlDtr.Close ();
			if(FlagFlag==0)
			{
				sql="select Ledger_Name,city,Ledger_ID from Ledger_Master l,Employee e where l.ledger_name=e.Emp_name and Ledger_ID='"+Cust_ID+"'";
				SqlDtr = obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{
					CustName=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(2).ToString();
					if(SqlDtr.GetValue(1).ToString().Equals(""))
						txtCity.Text="";
					else
						txtCity.Text=SqlDtr.GetValue(1).ToString();
					FlagFlag=1;
					DropDiscount1.Enabled=false;
					DropDiscount2.Enabled=false;
					txtDisc1.Enabled=false;
					txtDisc2.Enabled=false;
				}
				SqlDtr.Close ();
			}
			if(FlagFlag==0)
			{
				sql="select Ledger_Name,Ledger_ID from Ledger_Master where Ledger_ID='"+Cust_ID+"'";
				SqlDtr = obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{
					CustName=SqlDtr.GetValue(0).ToString()+":"+SqlDtr.GetValue(1).ToString();
					txtCity.Text="";
					DropDiscount1.Enabled=false;
					DropDiscount2.Enabled=false;
					
				}
				SqlDtr.Close();
			}
			
			#endregion
			
			//DropCustName.Value=CustName; //Comment by vikas sharma 30.04.09
			DropCustName.Value=CustName+";"+City_Name;
			
			if(DropCustName.Value.IndexOf(":")>0)
			{
				Textbox3.Text="";
				txtCr.Text = "";
				GridDuePayment.Visible = false;
				txtTotalBalance.Enabled = false;
				txtTotalBalance.Text ="0";
				Textbox2.Enabled = false;
				Textbox3.Enabled = false;
				txtCr.Enabled = false;
				txtFinalDues.Enabled = false;
				//*********
				txtDisc1.Enabled=false;
				txtDisc2.Enabled=false;
				//*********
			}
			else
			{
				GridDuePayment.Visible = true;
				txtTotalBalance.Enabled = true;
				Textbox2.Enabled = true;
				Textbox3.Enabled = true; 
				txtCr.Enabled = true;
				txtFinalDues.Enabled = true;
				object op =null;
				//dbobj.ExecProc(OprType.Insert,"Test",ref op,"@Cust_ID",Cust_ID);
				dbobj.ExecProc(OprType.Insert,"Test",ref op,"@Cust_ID",OldCustomerID);

				#region Bind DataGrid
				if(DropReceiptNo.Visible==true)
				{
					//SqlDtr=obj.GetRecordSet("(select substring(cast(pr.invoice_no as varchar),4,9) as Invoice_No,si.invoice_date,pr.Received_Amount as balance from Payment_Receipt pr,Sales_Master si where cast(si.invoice_no as varchar)=cast(pr.invoice_no as varchar) and receipt_no='"+DropReceiptNo.SelectedItem.Text+"') union (select invoice_no,receipt_date,received_amount from payment_receipt where receipt_no='"+DropReceiptNo.SelectedItem.Text+"' and (invoice_no='' or invoice_no='O/B'))");
					SqlDtr=obj.GetRecordSet("(select substring(cast(pr.invoice_no as varchar),4,9) as Invoice_No,si.invoice_date,(pr.Received_Amount+pr.discount1+pr.discount2) as balance from Payment_Receipt pr,Sales_Master si where cast(si.invoice_no as varchar)=cast(pr.invoice_no as varchar) and receipt_no='"+DropReceiptNo.SelectedItem.Text+"') union (select invoice_no,receipt_date,(received_amount+discount1+discount2) from payment_receipt where receipt_no='"+DropReceiptNo.SelectedItem.Text+"' and (invoice_no='' or invoice_no='O/B'))");
					GridDuePayment.DataSource=SqlDtr;
					GridDuePayment.DataBind();
					GridDuePayment.Visible=true;
					SqlDtr.Close();  
				}
				else
				{
					//SqlDtr=obj.GetRecordSet("select substring(cast(Bill_No as varchar),4,9) as invoice_no,Bill_date as invoice_date,Amount as balance from LedgDetails where cust_id = '"+Cust_ID+"' and Amount > 0");
					SqlDtr=obj.GetRecordSet("select substring(cast(Bill_No as varchar),4,9) as invoice_no,Bill_date as invoice_date,Amount as balance from LedgerDetails where cust_id = '"+Cust_ID+"' and Amount > 0");
					GridDuePayment.DataSource=SqlDtr;
					GridDuePayment.DataBind();
					GridDuePayment.Visible=true;
					SqlDtr.Close();  
				}
				#endregion 

				txtTotalBalance.Text =total.ToString();
			}
			checkPrevileges(); 
			btnPrint.CausesValidation=true;
		}

		int ReceiptNo=0;
		/// <summary>
		/// This method is used to get the next id for saveing the data.
		/// </summary>
		public void GetNextReceiptNo()
		{
			SqlDataReader rdr=null;
			dbobj.SelectQuery("select max(Receipt_No)+1 from payment_receipt",ref rdr);
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
					ReceiptNo=System.Convert.ToInt32(rdr.GetValue(0).ToString());
				else
					ReceiptNo=1001;
			}
		}
		
		/// <summary>
		/// This method is used to get bank info.
		/// </summary>
		public void GetBank()
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr;
			string str="select Ledger_Name from Ledger_Master where sub_grp_id='117' or sub_grp_id='126' or sub_grp_id='127' order by Ledger_Name";
			rdr = obj.GetRecordSet(str);
			DropBankName.Items.Clear();
			DropBankName.Items.Add("Select");
			while(rdr.Read())
			{
				DropBankName.Items.Add(rdr.GetValue(0).ToString());
			}
			rdr.Close();
		}

		protected void GridDuePayment_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}


		/// <summary>
		/// This method is used to delete the particular record who select from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(PanReceiptNo.Visible==true)
				{
					string DisType1 = "", DisType2 = "";
					object obj=null;
                    if(DropDiscount1.SelectedIndex!=0)
					{
						DisType1 = DropDiscount1.SelectedItem.Text.Substring(0,1);
						DisType1 +="D";
					}
					if(DropDiscount2.SelectedIndex!=0)
					{
						DisType2 = DropDiscount2.SelectedItem.Text.Substring(0,1);
						DisType2 +="D";
					}

                    PaymentReceiptModel payment = new PaymentReceiptModel();
                    payment.DiscountType1 = DisType1;
                    payment.DiscountType2 = DisType2;
                    payment.ReceiptNo = DropReceiptNo.SelectedItem.Text;
                    payment.Invoice_Date = Invoice_Date;
                    payment.CustomerID = customerID;
                    payment.RecDate = GenUtil.str2DDMMYYYY(txtReceivedDate.Text);
                    //payment.Cust_ID = Cust_ID.ToString();


                    int x=0,Cust_ID=0;
					dbobj.Insert_or_Update("delete from AccountsLedgerTable where particulars = 'Receipt ("+DropReceiptNo.SelectedItem.Text+")'",ref x);
					dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_"+DisType1+" ("+DropReceiptNo.SelectedItem.Text+")'",ref x);
					dbobj1.Insert_or_Update("delete from AccountsLedgerTable where Particulars = 'Receipt_"+DisType2+" ("+DropReceiptNo.SelectedItem.Text+")'",ref x);
					dbobj.Insert_or_Update("delete from CustomerLedgerTable where particular = 'Payment Received("+DropReceiptNo.SelectedItem.Text+")'",ref x);
					dbobj.Insert_or_Update("delete from Payment_Receipt where Receipt_No='"+DropReceiptNo.SelectedItem.Text+"'",ref x);
					dbobj.Insert_or_Update("insert into payment_receipt values("+DropReceiptNo.SelectedItem.Text+",'Deleted','"+GenUtil.str2MMDDYYYY(Invoice_Date)+"','','','','','','','','','','','','','')",ref x);
					
					dbobj.ExecuteScalar("select cust_id from customer where cust_name=(select ledger_name from ledger_master where ledger_id = '"+customerID+"')",ref Cust_ID);
					dbobj.ExecProc(OprType.Insert,"UpdateAccountsLedgerForCustomer",ref obj,"@Ledger_ID",customerID,"@Invoice_Date",GenUtil.str2DDMMYYYY(txtReceivedDate.Text));
					dbobj.ExecProc(OprType.Insert,"UpdateCustomerLedgerForCustomer",ref obj,"@Cust_ID",Cust_ID,"@Invoice_Date",GenUtil.str2MMDDYYYY(txtReceivedDate.Text));
					MessageBox.Show("Receipt Cancellation Successfully");
					Clear();
				}
				else
					MessageBox.Show("Please Select Edit Button First");
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:btnDel_Clicked  Payment receipt Deleted. User_ID: " +uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:btnDel_Clicked  Payment receipt.     , Exception : "+ex.Message+"    User_ID: " +uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the customer info according to select the customer name from dropdown list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropCustName_ServerChange(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This method is used to temperary.
		/// </summary>
		public void GetInfo()
		{
			try
			{
				string Cust_ID="";
				//SqlDataReader rdr1=null;
				InventoryClass  obj=new InventoryClass ();
				InventoryClass  obj1=new InventoryClass ();
				
				//SqlDataReader rdr = obj.GetRecordSet("select Cust_ID from Customer where Cust_Name='"+ DropCustName.Value +"'"); // Comment by vikas sharma 30.04.09
				/******************add by vikas 02.05.09************************************/
				string cust_name=DropCustName.Value;
				if(cust_name.ToString()!="Select")
				{
					cust_name=cust_name.Substring(0,cust_name.IndexOf(";"));
				}
				SqlDataReader rdr = obj.GetRecordSet("select Cust_ID from Customer where Cust_Name='"+cust_name+"'");
				/******************end************************************/
				if(rdr.Read())
				{
					Cust_ID=rdr["Cust_ID"].ToString();
				}
				rdr.Close ();
				object ob=null;
				if(Cust_ID!="")
					dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetails",ref ob,"@Cust_ID",Cust_ID);
				
				getRecInfo();
				btnPrint.CausesValidation=true;
				PrintFlag=false;
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:DropCustName_SelectedIndexChanged  Payment Recipt Viewed   userid "+uid);			
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment_Receipt.aspx,Class:InventoryClass.cs,Method:DropCustName_SelectedIndexChanged " + ex.Message+" EXCEPTION "+uid);
			}
		}

		/// <summary>
		/// This method is used to update the customer balance after update the record in edit time.
		/// </summary>
		/// <param name="Ledger_ID"></param>
		public void CustomerInsertUpdate(string Ledger_ID)
		{
			SqlDataReader rdr=null;
			SqlCommand cmd;
			InventoryClass obj =new InventoryClass();
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			double Bal=0;
			string BalType="",str="";
			int i=0;
			//*************************
			if(Invoice_Date.IndexOf(" ")>0)
			{
				string[] CheckDate = Invoice_Date.Split(new char[] {' '},Invoice_Date.Length);
				if(DateTime.Compare(System.Convert.ToDateTime(CheckDate[0].ToString()),System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDate.Text)))>0)
					Invoice_Date=GenUtil.str2DDMMYYYY(txtDate.Text);
			}
			else
				Invoice_Date=GenUtil.str2DDMMYYYY(txtDate.Text);
			//			for(int k=0;k<LedgerID.Count;k++)
			//			{
			rdr = obj.GetRecordSet("select top 1 Entry_Date from AccountsLedgerTable where Ledger_ID='"+Ledger_ID.ToString()+"' and Entry_Date<='"+Invoice_Date+"' order by entry_date desc");
			if(rdr.Read())
				str="select * from AccountsLedgerTable where Ledger_ID='"+Ledger_ID+"' and Entry_Date>='"+rdr.GetValue(0).ToString()+"' order by entry_date";
			else
				str="select * from AccountsLedgerTable where Ledger_ID='"+Ledger_ID+"' order by entry_date";
			rdr.Close();
			//*************************
			//string str="select * from AccountsLedgerTable where Ledger_ID='"+LedgerID+"' order by entry_date";
			rdr=obj.GetRecordSet(str);
			Bal=0;
			BalType="";
			i=0;
			while(rdr.Read())
			{
				if(i==0)
				{
					BalType=rdr["Bal_Type"].ToString();
					Bal=double.Parse(rdr["Balance"].ToString());
					i++;
				}
				else
				{
					if(double.Parse(rdr["Credit_Amount"].ToString())!=0)
					{
						if(BalType=="Cr")
						{
							string ss=rdr["Credit_Amount"].ToString();
							Bal+=double.Parse(rdr["Credit_Amount"].ToString());
							BalType="Cr";
						}
						else
						{
							string ss=rdr["Credit_Amount"].ToString();
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
						{
							string ss=rdr["Debit_Amount"].ToString();
							Bal+=double.Parse(rdr["Debit_Amount"].ToString());
						}
						else
						{
							string ss=rdr["Debit_Amount"].ToString();
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
					string str11="update AccountsLedgerTable set Balance='"+Bal.ToString()+"',Bal_Type='"+BalType+"' where Ledger_ID='"+rdr["Ledger_ID"].ToString()+"' and Particulars='"+rdr["Particulars"].ToString()+"'";
					cmd = new SqlCommand("update AccountsLedgerTable set Balance='"+Bal.ToString()+"',Bal_Type='"+BalType+"' where Ledger_ID='"+rdr["Ledger_ID"].ToString()+"' and Particulars='"+rdr["Particulars"].ToString()+"'",Con);
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					Con.Close();
				}		
			}
			rdr.Close();
			//*************************
			rdr = obj.GetRecordSet("select top 1 EntryDate from CustomerLedgerTable where CustID=(select Cust_ID from Customer,Ledger_Master where Ledger_Name=Cust_Name and Ledger_ID='"+Ledger_ID.ToString()+"') and EntryDate<='"+Invoice_Date+"' order by entrydate desc");
			if(rdr.Read())
				str="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer,Ledger_Master where Ledger_Name=Cust_Name and Ledger_ID='"+Ledger_ID+"') and EntryDate>='"+rdr.GetValue(0).ToString()+"' order by entrydate";
			else
				str="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer c,Ledger_Master l where Ledger_Name=Cust_Name and Ledger_ID='"+Ledger_ID+"') order by entrydate";
			rdr.Close();
			//*************************
			//string str1="select * from CustomerLedgerTable where CustID=(select Cust_ID from Customer c,Ledger_Master l where Ledger_Name=Cust_Name and Ledger_ID='"+LedgerID+"') order by entrydate";
			rdr=obj.GetRecordSet(str);
			Bal=0;
			i=0;
			BalType="";
			while(rdr.Read())
			{
				if(i==0)
				{
					BalType=rdr["BalanceType"].ToString();
					Bal=double.Parse(rdr["Balance"].ToString());
					i++;
				}
				else
				{
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
					cmd = new SqlCommand("update CustomerLedgerTable set Balance='"+Bal.ToString()+"',BalanceType='"+BalType+"' where CustID='"+rdr["CustID"].ToString()+"' and Particular='"+rdr["Particular"].ToString()+"'",Con);
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					Con.Close();
				}
			}
			rdr.Close();
			//}
		}

		protected void DropDiscount2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}