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
using Servosms.Sysitem.Classes;
using System.Data.SqlClient;
using RMG;
using DBOperations;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Accounts
{
	/// <summary>
	/// Summary description for voucher.
	/// </summary>
	public partial class voucher : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string id= "";
		string uid = "";
		static bool PrintFlag = false;
		string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
		static ArrayList LedgerID= new ArrayList();
		static string Invoice_Date = "", Acc_Date = "";
        bool isEditButtonClick = false;
        string BaseUri = "http://localhost:64862";

        /// <summary>
        /// Put user code to initialize the page here
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// and generate the next ID also.
        /// </summary>
        protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				string str = ex.ToString();
				CreateLogFiles.ErrorLog("Form:voucher,Method:PageLoad   "+" EXCEPTION "+ ex.Message+" userid "+ uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				Acc_Date="";
				checkPrevileges();
				LedgerID=new ArrayList();
				Invoice_Date = "";
				getID();
				txtNarration.Text = "";
				setValue();
				DropDownID.Visible = false;  
				btnEdit.Enabled = false;
				btnDelete.Enabled = false; 
				txtDate.Text = DateTime.Now.Day.ToString()+"/"+ DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/ReceiptController/GetOrgDate").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        var DateFrom = JsonConvert.DeserializeObject<string>(disc);
                        Acc_Date = GenUtil.trimDate(DateFrom);
                    }
                }
			}
            txtDate.Text = Request.Form["txtDate"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDate"].ToString().Trim();
        }

		/// <summary>
		/// This method checks the User Privilegs from session.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
				
			string Module="1";
			string SubModule="4";
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
			//if(Add_Flag=="0" && Edit_Flag=="0" && Del_Flag=="0")
			if(View_flag=="0")
			{
				//string msg="UnAthourized Visit to Voucher Type Page";
				//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}
			if(Add_Flag=="0")
				btnAdd.Enabled=false;
			if(Edit_Flag=="0")
				btnEdit.Enabled=false;
			if(Del_Flag=="0")
				btnDelete.Enabled=false;
			#endregion
		}

		/// <summary>
		/// This method used reset the combo to their initial value "Select".
		/// </summary>
		public void setValue()
		{
			txtAccName1.Value = "Select";  
			txtAccName2.Value = "Select";
			txtAccName3.Value = "Select";
			txtAccName4.Value = "Select";
			txtAccName5.Value = "Select";
			txtAccName6.Value = "Select";
			txtAccName7.Value = "Select";
			txtAccName8.Value = "Select";
		}

		/// <summary>
		/// Method Returns the ID's of voucher type Contra, debit, Credit, Journal. sets all the Id's into HTML hidden fields.
		/// Contra ID starts with : 10001;
		/// Debit Note : 20001;
		/// Credit Note :30001;
		/// Journal    : 40001;
		/// </summary>
		public void getID()
		{
			try
			{
				ArrayList CustName = new ArrayList();                  //Add by vikas 07.09.09
				string strContra = "";
				string strCreditNote = "";
				string strDebitNote = "";
				string strJournal = "";
                VoucherModel voucher = new VoucherModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/VoucherController/GetVoucherTypeInfo").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        voucher = JsonConvert.DeserializeObject<VoucherModel>(disc);
                    }
                }
				if(voucher!=null)
				{
					int id1 = 0;
					txtTempContra.Value = voucher.Contra;
					if(txtTempContra.Value.Equals("") || txtTempContra.Value.Equals("0"))
						txtTempContra.Value="10001~";
					else
					{
						id1 = (System.Convert.ToInt32(txtTempContra.Value)+1);  
						txtTempContra.Value = id1.ToString()+"~"; 
					}
				}
				else
				{
					txtTempContra.Value = "10001"+"~";   
				}
				if(voucher != null)
				{
					int id3 = 0;
					txTempCredit.Value = voucher.Credit;
					if(txTempCredit.Value.Equals("") || txTempCredit.Value.Equals("0"))
						txTempCredit.Value = "30001"+"~";
					else
					{
						id3 = (System.Convert.ToInt32(txTempCredit.Value)+1);  
						txTempCredit.Value = id3.ToString()+"~"; 
					}
				}
				else
				{
					txTempCredit.Value = "30001"+"~";   
				}
				if(voucher != null)
				{
					int id2 = 0;
					txtTempDebit.Value = voucher.Debit;
					if(txtTempDebit.Value.Equals("") || txtTempDebit.Value.Equals("0"))
						txtTempDebit.Value = "20001"+"~";
					else
					{
						id2 = (System.Convert.ToInt32(txtTempDebit.Value)+1);
						txtTempDebit.Value = id2.ToString()+"~";
					}
				}
				else
				{
					txtTempDebit.Value = "20001"+"~";
				}
                if (voucher != null)
                {
					int id4 = 0;
					txtTempJournal.Value = voucher.Journal;
					if(txtTempJournal.Value.Equals("") || txtTempJournal.Value.Equals("0"))
						txtTempJournal.Value = "40001"+"~";
					else
					{
						id4 = (System.Convert.ToInt32(txtTempJournal.Value)+1);  
						txtTempJournal.Value = id4.ToString()+"~"; 
					}
				}
				else
				{
					txtTempJournal.Value = "40001"+"~";   
				}

                /* Coment by vikas Date on 13.08.09
				  dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar),lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg where lm.Sub_grp_ID = lmsg.Sub_grp_ID order by lm.Ledger_Name",ref SqlDtr);
				//dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar)+':'+c.City,lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg,customer c where lm.Sub_grp_ID = lmsg.Sub_grp_ID and c.cust_name=lm.Ledger_name order by lm.Ledger_Name",ref SqlDtr);
				while(SqlDtr.Read())
				{
					string subgrpname = SqlDtr.GetValue(1).ToString();
  
					if(subgrpname.Trim().StartsWith("Cash") || subgrpname.Trim().StartsWith("Bank"))
					{
						strContra = strContra +SqlDtr.GetValue(0).ToString()+"~"; 
						strDebitNote = strDebitNote +SqlDtr.GetValue(0).ToString()+"~";
						strCreditNote = strCreditNote +SqlDtr.GetValue(0).ToString()+"~";
					}
					else
					{
						strJournal = strJournal + SqlDtr.GetValue(0).ToString()+"~"; 
						strDebitNote = strDebitNote +SqlDtr.GetValue(0).ToString()+"~";
						strCreditNote = strCreditNote +SqlDtr.GetValue(0).ToString()+"~";
					}
				}
				SqlDtr.Close();*/

                /***********Add by vikas 13.08.09*************************/

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/VoucherController/GetLedgerName").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        voucher = JsonConvert.DeserializeObject<VoucherModel>(disc);
                    }
                }

                if (voucher != null)
                {
                    strContra = voucher.Contra;
                    strDebitNote = voucher.Debit;
                    strCreditNote = voucher.Credit;
                    strJournal = voucher.Journal;
                    CustName.Add(voucher.CustomerName);
                }

				for(int i=0;i<CustName.Count;i++)
				{
					strDebitNote = strDebitNote +CustName[i].ToString()+"~";
					strCreditNote = strCreditNote +CustName[i].ToString()+"~";
					strJournal = strJournal +CustName[i].ToString()+"~";     //add by vikas 21.11.2012
				}

				txtTempContra.Value += strContra;
				txtTempJournal.Value +=  strJournal;
				txTempCredit.Value += strCreditNote;
				txtTempDebit.Value += strDebitNote;
  
				strContra = strContra.Replace("~",",");
				strJournal = strJournal.Replace("~",",");
				strCreditNote = strCreditNote.Replace("~",",");
				strDebitNote = strDebitNote.Replace("~",",");
				
				txtTempContra1.Value = "Select,";
				txtTempJournal1.Value = "Select,";
				txTempCredit1.Value = "Select,";
				txtTempDebit1.Value = "Select,";

				txtTempContra1.Value += strContra;
				txtTempJournal1.Value +=  strJournal;
				txTempCredit1.Value += strCreditNote;
				txtTempDebit1.Value += strDebitNote;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx,Method:getID() EXCEPTION: "+ ex.Message+" userid :"+ uid);
			}
		}

		/// <summary>
		/// This method fills all the combo box with Ledger Names after server trip to avoid the blank values, to set the last selected values.
		/// </summary>
		public void fillCombo()
		{
			try
			{
				SqlDataReader SqlDtr = null;
				string Vouch_Type = DropVoucherName.SelectedItem.Text;
				//DropDownList[] dropAccName ={dropAccName1, dropAccName2, dropAccName3, dropAccName4, dropAccName5, dropAccName6, dropAccName7, dropAccName8};
				HtmlInputText[] dropAccName ={dropAccName1, dropAccName2, dropAccName3, dropAccName4, dropAccName5, dropAccName6, dropAccName7, dropAccName8};
				HtmlInputHidden[] txtAccName ={txtAccName1, txtAccName2, txtAccName3, txtAccName4, txtAccName5,txtAccName6, txtAccName7, txtAccName8}; 
				for(int k=0;k<dropAccName.Length;k++)
				{
					dropAccName[k].Value="Select";  
				}
                /*Coment by vikas 13.08.09
				dbobj.SelectQuery("select lm.Ledger_name+':'+cast(lm.Ledger_ID as varchar),lmsg.sub_grp_name from Ledger_Master lm,Ledger_Master_Sub_grp lmsg where lm.Sub_grp_ID = lmsg.Sub_grp_ID ",ref SqlDtr);
				texthiddenprod.Value="Select,";
				while(SqlDtr.Read())
				{
					string subgrpname = SqlDtr.GetValue(1).ToString();
					if(Vouch_Type.Equals("Contra"))
					{
						if(subgrpname.Trim().StartsWith("Cash") || subgrpname.Trim().StartsWith("Bank"))
						{
							texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+",";
						}
					}
					if(Vouch_Type.Equals("Journal"))
					{
						if(!subgrpname.Trim().StartsWith("Cash") && !subgrpname.Trim().StartsWith("Bank"))
						{
							texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+",";
						}
					}
					if(Vouch_Type.Equals("Credit Note") || Vouch_Type.Equals("Debit Note"))
					{
						texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+",";
					}
				}
				SqlDtr.Close();
				*/

                /*************Add by vikas 13.08.09 ****************************/

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/VoucherController/GetLedgerNames?VoucherType="+Vouch_Type).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        var voucher = JsonConvert.DeserializeObject<string>(disc);
                        texthiddenprod.Value = voucher;
                    }
                }
                
                for (int j=0;j<dropAccName.Length;j++)
				{
					dropAccName[j].Value =txtAccName[j].Value;      
				}
				txtVouchID.Value = id; 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx,Method:fillCombo() EXCEPTION: "+ ex.Message+" userid :"+ uid);
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

		private void Dropdownlist13_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void Dropdownlist10_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// This method is used to call the Save() function for save the voucher information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DateTime.Compare(System.Convert.ToDateTime(Acc_Date),System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDate.Text)))>0)
					MessageBox.Show("Please Select Date Must be Greater than Opening Date");
				else
				{
					Save();
					PrintFlag=true;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:btnAdd_Click EXCEPTION: "+ ex.Message+" userid :"+ uid);
			}
		}

		/// <summary>
		/// This method is used to save the record.
		/// </summary>
		public void Save()
		{
			if(txtVouchID.Visible==true)
				id = txtID.Value;
			else
				id=DropDownID.SelectedItem.Text;
			if(txtAccName1.Value == "Select" && txtAccName2.Value == "Select"  && txtAccName3.Value == "Select"  && txtAccName4.Value == "Select"  && txtAccName5.Value == "Select"  && txtAccName6.Value == "Select"  && txtAccName7.Value == "Select"  && txtAccName8.Value == "Select" )
			{
				txtVouchID.Value = id;
				MessageBox.Show("Please select Account Name");
				fillCombo();
				return;
			}
			HtmlInputText[] dropAccName ={dropAccName1, dropAccName2, dropAccName3, dropAccName4, dropAccName5, dropAccName6, dropAccName7, dropAccName8};
			HtmlInputHidden[] txtAccName ={txtAccName1, txtAccName2, txtAccName3, txtAccName4, txtAccName5,txtAccName6, txtAccName7, txtAccName8}; 
			TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
			DropDownList[] dropType = {dropType_1 ,dropType_2,dropType_3,dropType_4,dropType_5,dropType_6,dropType_7,dropType_8};
			string narration = txtNarration.Text.Trim();
			string Vouch_Type = DropVoucherName.SelectedItem.Text;
			for(int i=0; i<(txtAccName.Length/2);i++)
			{
				if(txtAccName[i].Value!="Select")
				{
					if(txtAccName[i].Value==txtAccName[i+4].Value)
					{
						MessageBox.Show("Can Not be Select Same Ledger Name");
						fillCombo();
						return;
					}
					if(Amount[i].Text.Trim()== "" && Amount[i+4].Text.Trim()== "")
					{
						MessageBox.Show("Please Enter the Amount");
						fillCombo();
						return;
					}
				}
			}
			DateTime date = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDate.Text.ToString())+" "+DateTime.Now.TimeOfDay.ToString());
			int intID = System.Convert.ToInt32(id.ToString()); 
			int flag = 0;
			
			for(int i=0; i<(dropAccName.Length/2);i++)
			{
				int c = 0;
				if(	txtAccName[i].Value != "Select" && txtAccName[i+4].Value == "Select")
				{
					MessageBox.Show("Please Select The Second Account Name");
					flag = 1;
					break;
				}
				else if(txtAccName[i].Value == "Select" && txtAccName[i+4].Value != "Select")
				{
					MessageBox.Show("Please Select The First Account Name");
					flag = 1;
					break;
				}
				
				if(txtAccName[i].Value != "Select" && txtAccName[i+4].Value != "Select" && Amount[i].Text.Trim()!= "" && Amount[i+4].Text.Trim()!= "")
				{
					string crID = "";
					string drID = "";
					string Amount_cr = "";
					string Amount_Dr = "";
					string L_Type =""; 

					string Ledg_ID =txtAccName[i].Value.ToString() ;
					string[] arr = Ledg_ID.Split(new char[] {':'},Ledg_ID.Length);
					string Ledg_ID1 =txtAccName[i+4].Value.ToString() ;
					string[] arr1 = Ledg_ID1.Split(new char[] {':'},Ledg_ID1.Length);
					if(dropType[i].SelectedItem.Text.Trim() == "Dr")
					{
						drID = arr[1];
						crID = arr1[1];
						Amount_Dr = Amount[i].Text.Trim();
						Amount_cr = Amount[i+4].Text.Trim();
						L_Type = "Dr";
					}
					else
					{
						drID = arr1[1];
						crID = arr[1];
						Amount_Dr = Amount[i+4].Text.Trim();
						Amount_cr = Amount[i].Text.Trim();
						L_Type = "Cr";
					}
                    VoucherModel voucher = new VoucherModel();
                    voucher.VoucherID = intID;
                    voucher.VoucherType = Vouch_Type.Trim();
                    voucher.VoucherDate = date.ToShortDateString();
                    voucher.LedgerIDCr = crID.Trim();
                    voucher.Amount1 = Amount_cr.Trim();
                    voucher.LedgerIDDr = drID.Trim();
                    voucher.Amount2 = Amount_Dr.Trim();
                    voucher.Narration = narration;
                    voucher.LType = L_Type;
                    voucher.InvoiceDate = date.ToShortDateString();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(voucher);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/VoucherController/InsertVoucher", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var rr = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                        }
                    }

					if(txtVouchID.Visible==true)
					{
						CustomerInsertUpdate(drID);
						CustomerInsertUpdate(crID);
					}
					intID++;
				}
			}
			if(DropDownID.Visible==true)
			{
				CustomerUpdate();
			}
			
			if(flag == 1)
			{
				txtVouchID.Value = id;
				fillCombo();
				return;
			}
			else
			{
				if(txtVouchID.Visible==true)
					MessageBox.Show("Voucher Saved");
				else
					MessageBox.Show("Voucher Updated");
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:btnAdd_Click, New Voucher of ID = "+(--intID)+" Saved  userid :"+ uid);
				makingReport();
				clear1();
				clear();
				getID();
			}
			checkPrevileges();
			PrintFlag=true;
			btnPrint.CausesValidation=false;
		}
		
		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\Voucher.txt";
				StreamWriter sw = new StreamWriter(path);
				string info =" {0,-14:S} {1,-50:S} {2,20:S} {3,-46:S}";//Party Name & Address
				string info2=" {0,16:S} {1,-114:S}";
				string info3=" {0,-14:S} {1,-50:S} {2,20:S} {3,46:S}";//Party Name & Address
				HtmlInputHidden[] arrName = {txtAccName5,txtAccName1,txtAccName6,txtAccName2,txtAccName7,txtAccName3,txtAccName8,txtAccName4};
				TextBox[] arrAmt = {txtAmount5,txtAmount1,txtAmount6,txtAmount2,txtAmount7,txtAmount3,txtAmount8,txtAmount4};
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
				string VoucherName="";
				if(DropVoucherName.SelectedIndex==1)
					VoucherName="  CONTRA";
				else if(DropVoucherName.SelectedIndex==2)
					VoucherName="CREDIT NOTE";
				else if(DropVoucherName.SelectedIndex==3)
					VoucherName="DEBIT NOTE";
				else if(DropVoucherName.SelectedIndex==4)
					VoucherName="  JOURNAL";
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr = null;
				if(arrName[1].Value!="Select")
				{
					sw.WriteLine("                                                             "+VoucherName);
					for(int j=0;j<8;j++)
					{
						sw.WriteLine("");
					}
					string addr="",city="";
					string name=arrName[0].Value;
					string[] arrAname = name.Split(new char[] {':'},name.Length);
					string str = "select address,city from customer,ledger_master where ledger_name=cust_name and ledger_id='"+arrAname[1]+"'";
					rdr = obj.GetRecordSet(str);
					if(rdr.Read())
					{
						addr = rdr.GetValue(0).ToString();
						city = rdr.GetValue(1).ToString();
					}
					rdr.Close();
					if(txtVouchID.Visible==true)
						sw.WriteLine(info,"",arrName[0].Value,"",txtID.Value);
					else
						sw.WriteLine(info,"",arrName[0].Value,"",DropDownID.SelectedItem.Text);
					sw.WriteLine(info,"",addr,"",txtDate.Text);
					sw.WriteLine(info,"",city,"","---");
					sw.WriteLine(info,"","","","---");
					sw.WriteLine(info3,"","   Particular","","Amount");
					sw.WriteLine("");
					sw.WriteLine("");
					sw.WriteLine(info3,"",arrName[1].Value,"",GenUtil.strNumericFormat(arrAmt[0].Text));
					for(int k=0;k<34;k++)
					{
						sw.WriteLine("");
					}
					sw.Write((char)27);//added by vishnu
					sw.Write((char)69);//added by vishnu
					sw.WriteLine(info2,"","Narration : "+txtNarration.Text);
					sw.Write((char)27);//added by vishnu
					sw.Write((char)70);//added by vishnu
					sw.WriteLine(info3,"","","","Total Amount : "+GenUtil.strNumericFormat(arrAmt[0].Text));
					sw.WriteLine("");
					sw.WriteLine("");
					sw.WriteLine(info2,"","    "+GenUtil.ConvertNoToWord(arrAmt[0].Text));
				}
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Payment,Method:makingReport() Exception: "+ex.Message+"  User: "+ uid);
			}
		}

		/// <summary>
		/// contacst the Print_WiindowServices via socket and sends the Voucher.txt file name to print.
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
					
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\Voucher.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:Voucher.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:Voucher.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Voucher.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Voucher.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Voucher.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}
	
		/// <summary>
		/// This method is used to check the string length with passing argument.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string checkStr(string str)
		{
			if(str.IndexOf("\r\n") >0)
			{
				str = str.Replace("\r\n"," "); 
			}
			return str;
		}

		/// <summary>
		/// This method is used to trim the string length
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string trimStr(string str)
		{
			if(str.Length > 30)
			{
				str = str.Substring(0,30); 
			}
			return str;
		}

		/// <summary>
		/// This method is used to enable or disable the component.
		/// </summary>
		public void clear1()
		{
			txtDate.Text = DateTime.Now.Day.ToString()+"/"+ DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString();
			LedgerID=new ArrayList();
			DropDownID.Visible = false;
			txtVouchID .Visible = true;
			btnEdit1.Visible = true;
			btnEdit.Enabled = false;  
			btnDelete.Enabled = false;
			btnAdd.Enabled = true;
			DropVoucherName.Enabled = true; 
			HtmlInputText[] dropAccName ={dropAccName1, dropAccName2, dropAccName3, dropAccName4, dropAccName5, dropAccName6, dropAccName7, dropAccName8};
			TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
			DropDownList[] dropType = {dropType_1 ,dropType_2,dropType_3,dropType_4,dropType_5,dropType_6,dropType_7,dropType_8};
			for(int i = 1;i<dropAccName.Length/2;i++)
			{
				dropAccName[i].Disabled = false;
				dropAccName[i+4].Disabled = false;
				Amount[i].Enabled = true;
				Amount[i+4].Enabled = true;
				dropType[i].Enabled = true;  
				dropType[i+4].Enabled = true;
			}
		}

		/// <summary>
		/// This method is used to clear the all component.
		/// </summary>
		public void clear()
		{
            if(!isEditButtonClick)
            {
                DropVoucherName.SelectedIndex = 0;
            }
			
			HtmlInputText[] dropAccName ={dropAccName1, dropAccName2, dropAccName3, dropAccName4, dropAccName5, dropAccName6, dropAccName7, dropAccName8};
			HtmlInputHidden[] txtAccName ={txtAccName1, txtAccName2, txtAccName3, txtAccName4, txtAccName5,txtAccName6, txtAccName7, txtAccName8}; 
			TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
			DropDownList[] dropType = {dropType_1 ,dropType_2,dropType_3,dropType_4,dropType_5,dropType_6,dropType_7,dropType_8};
			for(int i = 0;i<dropAccName.Length;i++)
			{
				dropAccName[i].Value = "Select";
				txtAccName[i].Value = "Select";
				Amount[i].Text = "";
				dropType[i].SelectedIndex = 0; 
			}
			txtLCr.Text  = ""; 
			txtLDr.Text = "";
			txtRCr.Text = "";
			txtRDr.Text = "";
			txtNarration.Text = ""; 
			txtVouchID.Value =""; 
		}

		/// <summary>
		/// This method is used to fill the dropdownlist from Voucher id.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit1_Click(object sender, System.EventArgs e)
		{
			try
			{
                if (DropVoucherName.SelectedIndex == 0)
                {
                    MessageBox.Show("Please select Voucher Type.");
                    return;
                }
                isEditButtonClick = true;

                clear();
				DropDownID.Items.Clear();
				DropDownID.Items.Add("Select");
				//DropVoucherName.Enabled = false; 
				txtVouchID.Visible  = false;
				DropDownID.Visible = true;
				btnEdit1.Visible = false;
				btnAdd.Enabled  = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled =  true;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/VoucherController/GetVoucherId?VoucherName=" + DropVoucherName.SelectedValue.ToString()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        var Voucherids = JsonConvert.DeserializeObject<List<string>>(disc);
                        if (Voucherids != null || Voucherids.Count > 0)
                            foreach (var voucher in Voucherids)
                                DropDownID.Items.Add(voucher);
                    }

                }
                
				checkPrevileges();
				btnPrint.CausesValidation=true;
				PrintFlag=false;
                isEditButtonClick = false;
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:btnEdit1_Click EXCEPTION: "+ ex.Message+" userid :"+ uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the record according to select the voucher id from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropDownID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(DropDownID.SelectedIndex == 0)  
				{
					MessageBox.Show("Please select Voucher ID");
					fillCombo();
					setValue(); 
					return;
				}
				clear();
				string voucher_id = DropDownID.SelectedItem.Text;  
			
                VoucherModel vocher = new VoucherModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/VoucherController/FetchVoucherByVoucherID?VoucherID="+ voucher_id).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        vocher = JsonConvert.DeserializeObject<VoucherModel>(disc);
                    }
                }

                if (vocher != null)
                {
                    DropVoucherName.SelectedIndex = DropVoucherName.Items.IndexOf(DropVoucherName.Items.FindByText(vocher.VoucherType));
                    txtDate.Text = GenUtil.str2DDMMYYYY(trimDate(vocher.VoucherDate));
                    Invoice_Date = vocher.VoucherDate;
                    if (vocher.LType.Equals("Cr"))
                    {
                        txtAmount1.Text = vocher.Amount1;
                        txtAmount5.Text = vocher.Amount2;
                        txtLCr.Text = vocher.Amount1;
                        txtRDr.Text = vocher.Amount2;
                        txtNarration.Text = vocher.Narration;
                        dropType_1.SelectedIndex = dropType_1.Items.IndexOf(dropType_1.Items.FindByText("Cr"));
                        dropType_5.SelectedIndex = dropType_5.Items.IndexOf(dropType_5.Items.FindByText("Dr"));
                        txtAccName1.Value = vocher.AccName1;
                        txtAccName5.Value = vocher.AccName5;
                        LedgerID = vocher.LedgerIDS;
                    }
                    else
                    {
                        txtAmount1.Text = vocher.Amount1;
                        txtAmount5.Text = vocher.Amount2;
                        txtLDr.Text = vocher.Amount1;
                        txtRCr.Text = vocher.Amount2;
                        txtNarration.Text = vocher.Narration;
                        dropType_1.SelectedIndex = dropType_1.Items.IndexOf(dropType_1.Items.FindByText("Dr"));
                        dropType_5.SelectedIndex = dropType_5.Items.IndexOf(dropType_5.Items.FindByText("Cr"));
                        txtAccName1.Value = vocher.AccName1;
                        txtAccName5.Value = vocher.AccName5;
                        LedgerID = vocher.LedgerIDS;
                    }
                    fillCombo();
                    dropAccName1.Value = txtAccName1.Value.ToString();
                    dropAccName5.Value = txtAccName5.Value.ToString();
                    DropVoucherName.Enabled = false;
                }
				HtmlInputText[] dropAccName ={dropAccName1, dropAccName2, dropAccName3, dropAccName4, dropAccName5, dropAccName6, dropAccName7, dropAccName8};
				TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
				DropDownList[] dropType = {dropType_1 ,dropType_2,dropType_3,dropType_4,dropType_5,dropType_6,dropType_7,dropType_8};
				for(int i = 1;i<dropAccName.Length/2;i++)
				{
					dropAccName[i].Disabled = true;
					dropAccName[i+4].Disabled = true;
					Amount[i].Enabled = false;
					Amount[i+4].Enabled = false;
					dropType[i].Enabled = false;  
					dropType[i+4].Enabled = false;
				}
				checkPrevileges();
				btnPrint.CausesValidation=true;
				PrintFlag=false;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:DropDownID_SelectedIndexChanged EXCEPTION: "+ ex.Message+" userid :"+ uid);
			}
		}

		/// <summary>
		/// This method return only date from date and time.
		/// </summary>
		/// <param name="strDate"></param>
		/// <returns></returns>
		public string trimDate(string strDate)
		{
			int pos = strDate.IndexOf(" ");
			if(pos != -1)
			{
				strDate = strDate.Substring(0,pos);
			}
			else
			{
				strDate = "";					
			}
			return strDate;
		}

		/// <summary>
		/// This method update the voucher entry in edit time.
		/// </summary>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DateTime.Compare(System.Convert.ToDateTime(Acc_Date),System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDate.Text)))>0)
					MessageBox.Show("Please Select Date Must be Greater than Opening Date");
				else
				{
					int c = 0;
					dbobj.Insert_or_Update("delete from voucher_Transaction where voucher_id ="+DropDownID.SelectedItem.Text.Trim(),ref c);
					if(DropVoucherName.SelectedItem.Text.Equals("Contra"))
						dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Contra ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
					else if(DropVoucherName.SelectedItem.Text.Equals("Journal"))
						dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Journal ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
					else if(DropVoucherName.SelectedItem.Text.Equals("Credit Note"))
						dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Credit Note ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
					else if(DropVoucherName.SelectedItem.Text.Equals("Debit Note"))
						dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Debit Note ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
					dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular ='Voucher("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
                    VoucherModel vcher = new VoucherModel();
                    vcher.VoucherType = DropVoucherName.SelectedItem.Text;
                    vcher.VoucherID = Convert.ToInt32(DropDownID.SelectedItem.Text.Trim());


                    Save();
					PrintFlag=true;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:btnEdit_Click EXCEPTION: "+ ex.Message+" userid :"+ uid);
			}
		}

		/// <summary>
		/// This method is used to update the record from select the dropdown list.
		/// </summary>
		public void Update()
		{
			if(DropDownID.SelectedIndex == 0)
			{
				MessageBox.Show("Please select Account Name");
				return;
			}
			if(txtAccName1.Value == "Select" || txtAccName5.Value == "Select" )
			{
				MessageBox.Show("Please select Account Name");
				fillCombo();
				
				return;
			}
			string voucher_type = DropVoucherName.SelectedItem.Text;
			string voucher_ID = DropDownID.SelectedItem.Text;
			DateTime date = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDate.Text)+" "+DateTime.Now.TimeOfDay.ToString());
			string narration = txtNarration.Text.Trim();  
			string crID = "";
			string drID = "";
			string Amount_cr = "";
			string Amount_Dr = "";
			string L_Type =""; 
			string Ledg_ID =txtAccName1.Value.ToString() ;
			string[] arr = Ledg_ID.Split(new char[] {':'},Ledg_ID.Length);
			string Ledg_ID1 =txtAccName5.Value.ToString() ;
			string[] arr1 = Ledg_ID1.Split(new char[] {':'},Ledg_ID1.Length);
			if(dropType_1.SelectedItem.Text.Trim() == "Dr")
			{
				drID = arr[1];
				crID = arr1[1];
				Amount_Dr = txtAmount1.Text.Trim();
				Amount_cr = txtAmount5.Text.Trim();
				L_Type = "Dr";
			}
			else
			{
				drID = arr1[1];
				crID = arr[1];
				Amount_Dr = txtAmount5.Text.Trim();
				Amount_cr = txtAmount1.Text.Trim();
				L_Type = "Cr";
			}
			int c = 0;
				
			dbobj.Insert_or_Update("Update voucher_transaction set voucher_date =Convert(datetime,'" + date+"',103),Ledg_ID_Cr ="+crID.Trim()+",Amount1="+Amount_cr+",Ledg_ID_Dr="+drID.Trim()+",Amount2="+Amount_Dr+",Narration='"+narration+"',L_Type='"+L_Type+"' where Voucher_ID ="+voucher_ID,ref c);   
			object obj = null;
			dbobj.ExecProc(OprType.Update,"ProUpdateAccountsLedger",ref obj,"@Voucher_ID",voucher_ID,"@Ledger_ID",drID.Trim(),"@Amount",Amount_Dr,"@Type","Dr","@Invoice_Date",date);
			dbobj.ExecProc(OprType.Update,"ProUpdateAccountsLedger",ref obj,"@Voucher_ID",voucher_ID,"@Ledger_ID",crID.Trim(),"@Amount",Amount_cr,"@Type","Cr","@Invoice_Date",date);
			if(c > 0)
			{
				MessageBox.Show("Voucher Updated"); 
				CreateLogFiles.ErrorLog("Form:voucher,Method:btnEdit_Click, Voucher of ID = "+voucher_ID+" updated  userid :"+ uid);
				makingReport();
				clear1();
				clear();
				getID();
			}
			checkPrevileges();
			PrintFlag=true;
			btnPrint.CausesValidation=false;
		}

		/// <summary>
		/// This method is used to delete the particular record when select the dropdownlist.
		/// </summary>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropDownID.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Account Name");
					return;
				}
				int c = 0;
				dbobj.Insert_or_Update("delete from voucher_Transaction where voucher_id ="+DropDownID.SelectedItem.Text.Trim(),ref c);
				if(DropVoucherName.SelectedItem.Text.Equals("Contra"))
					dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Contra ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
				else if(DropVoucherName.SelectedItem.Text.Equals("Journal"))
					dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Journal ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
				else if(DropVoucherName.SelectedItem.Text.Equals("Credit Note"))
					dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Credit Note ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
				else if(DropVoucherName.SelectedItem.Text.Equals("Debit Note"))
					dbobj.Insert_or_Update("delete from AccountsLedgerTable where Particulars ='Debit Note ("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
				dbobj.Insert_or_Update("delete from CustomerLedgerTable where Particular ='Voucher("+DropDownID.SelectedItem.Text.Trim()+")'",ref c);
				CustomerUpdate();
				MessageBox.Show("Voucher Deleted");
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:btnDelete_Click Voucher of ID = "+DropDownID.SelectedItem.Text.Trim()+" deleted  userid :"+ uid);
				clear1();
				clear();
				getID();
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:btnDelete_Click EXCEPTION: "+ ex.Message+" userid :"+ uid);
			}
		}

		/// <summary>
		/// Prepares the report file Voucher.txt for printing.
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
            var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Acc_Date));
            var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString()));
            if (DateTime.Compare(dt1, dt2) > 0)
                MessageBox.Show("Please Select Date Must be Greater than Opening Date");

            else
            {
                if (PrintFlag == false)
                {
                    if (txtVouchID.Visible == true)
                        Save();
                    else
                    {
                        Update();
                    }
                }
                Print();
                PrintFlag = false;
                btnPrint.CausesValidation = true;
            }
        }

		protected void DropVoucherName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            try
            {
                isEditButtonClick = true;
                if (DropDownID.Visible)
                {
                    clear();
                    DropDownID.Items.Clear();
                    DropDownID.Items.Add("Select");
                    //DropVoucherName.Enabled = false; 
                    txtVouchID.Visible = false;
                    DropDownID.Visible = true;
                    btnEdit1.Visible = false;
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/VoucherController/GetVoucherId?VoucherName=" + DropVoucherName.SelectedValue.ToString()).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            var Voucherids = JsonConvert.DeserializeObject<List<string>>(disc);
                            if (Voucherids != null || Voucherids.Count > 0)
                                foreach (var voucher in Voucherids)
                                    DropDownID.Items.Add(voucher);
                        }

                    }
                    getAcountName();
                    checkPrevileges();
                    btnPrint.CausesValidation = true;
                    PrintFlag = false;
                }
                else
                {
                    clear();
                    getAcountName();
                }
                isEditButtonClick = false;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:DropVoucherName_SelectedIndexChanged EXCEPTION: " + ex.Message + " userid :" + uid);
            }
        }

        public void getAcountName()
        {  
            try
            {                
                string strVoucherType = DropVoucherName.SelectedValue.ToString();
                
                string temp = "";
                string temp1 = "";
                if (strVoucherType == "Contra")
                {
                    temp = txtTempContra.Value;
                    temp1 = txtTempContra1.Value;                             
                }
                else if (strVoucherType == "Credit Note")
                {
                    temp = txTempCredit.Value;
                    temp1 = txTempCredit1.Value;                           
                }
                else if (strVoucherType == "Debit Note")
                {
                    temp = txtTempDebit.Value;
                    temp1 = txtTempDebit1.Value;                           
                }
                else
                {
                    temp = txtTempJournal.Value;
                    temp1 = txtTempJournal1.Value;                    
                }
                
                string[] strVouchID = temp.Split('~');
                txtVouchID.Value = strVouchID[0];
                txtID.Value = strVouchID[0];
                texthiddenprod.Value = temp1;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:voucher.aspx.cs,Method:DropVoucherName_SelectedIndexChanged EXCEPTION: " + ex.Message + " userid :" + uid);
            }            
        }

        /// <summary>
        /// This method is used for update the customer balance after update the record.
        /// </summary>
        public void CustomerUpdate()
		{
			if(Invoice_Date.IndexOf(" ")>0)
			{               
                string[] CheckDate = Invoice_Date.Split(new char[] {' '},Invoice_Date.Length);
				if(DateTime.Compare(System.Convert.ToDateTime(CheckDate[0].ToString()), System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString())))>0)
					Invoice_Date= GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString());
				else
					Invoice_Date=CheckDate[0].ToString();
			}
			else
				Invoice_Date= GenUtil.str2DDMMYYYY(Request.Form["txtDate"].ToString());
						
			for(int k=0;k<LedgerID.Count;k++)
			{
                VoucherModel vouchr = new VoucherModel();
                vouchr.LedgerID = LedgerID[k].ToString();
                vouchr.InvoiceDate = Invoice_Date;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(vouchr);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/VoucherController/CustomerUpdate", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var meessage = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    }
                }
            }
		}

		/// <summary>
		/// This method is used for update the customer balance after insert record.
		/// </summary>
		/// <param name="Ledger_ID"></param>
		public void CustomerInsertUpdate(string Ledger_ID)
		{
            VoucherModel voucher = new VoucherModel();
            voucher.LedgerID = Ledger_ID;
            voucher.InvoiceDate = txtDate.Text;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                var myContent = JsonConvert.SerializeObject(voucher);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("api/VoucherController/CustomerInsertUpdate", byteContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    var meessage = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                }
            }


        }
	}
}