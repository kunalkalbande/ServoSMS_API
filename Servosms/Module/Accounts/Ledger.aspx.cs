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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Accounts
{
	/// <summary>
	/// Summary description for Ledger
	/// </summary>
	public partial class Ledger : System.Web.UI.Page
	{
		
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.TextBox TxtAmount;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
		protected System.Web.UI.WebControls.TextBox TextSub;
		protected System.Web.UI.WebControls.TextBox TextGroup;
		protected System.Web.UI.WebControls.CheckBox CheckBox2;
		protected System.Web.UI.WebControls.CheckBox CheckAsset;
		protected System.Web.UI.WebControls.CheckBox CheckIncome;
		protected System.Web.UI.WebControls.CheckBox CheckExpen;
		protected System.Web.UI.HtmlControls.HtmlImage IMG2;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid="";
		ArrayList al = new ArrayList();
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
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				string str = ex.ToString();
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(tempInfo.Value=="Yes")
			{
				DeleteTheRec();
			}
			if(!IsPostBack)
			{
				checkPrevileges();
				getSubGroup();
				getGroup();
				getParties();
				TxtSub.Text = "";
				TxtGroup.Text = ""; 
				TxtSub.Enabled =false;
				TxtGroup.Enabled =false;
				btnEdit.Enabled = false;
				btnDelete.Enabled =false;
			}
		}

		/// <summary>
		/// This method returns the parties (i.e. Customer and Vendor names) in the form array to compare the Ledger Name not equals to Parties Names.
		/// </summary>
		/// <returns></returns>
		public object[] getParties()
		{
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/GetParties").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        al = JsonConvert.DeserializeObject<ArrayList>(disc);
                    }
                }
                return al.ToArray(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:getParties() Exception: "+ex.Message+"  User: "+ uid);     
				return null;
			}
		}
		
		/// <summary>
		/// This method checks the user privileges from session.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="1";
			string SubModule="1";
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
			if(View_flag=="0")
			{
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}
			if(Add_Flag=="0")
				btnSave.Enabled=false;
			if(Edit_Flag=="0")
				btnEdit.Enabled=false;
			if(Del_Flag=="0")
				btnDelete.Enabled=false;
			#endregion
		}

		/// <summary>
		/// This method fills the DropSub combo box with values of sub groups from Ledger_Master_Sub_grp
		/// </summary>
		public void getSubGroup()
		{
			try
			{
				DropSub.Items.Clear();
				DropSub.Items.Add("Select");
				DropSub.SelectedIndex = 0;
                List<string> subGrops = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/GetSubGroup").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        subGrops = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if (subGrops != null && subGrops.Count > 0)
                {
                    foreach (var subGrop in subGrops)
                        DropSub.Items.Add(subGrop);
                }
				DropSub.Items.Add("Other");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:getSubGroup() Exception: "+ex.Message+"  User: "+ uid);     
			}
		}

		/// <summary>
		/// This Method Access all the group name from MGroup Table and fills the Group Name Combo
		/// </summary>
		public void getGroup()
		{
			try
			{	
				string s = "";
				string s1="";
                List<string> subItems = new List<string>();
                foreach(var item in DropSub.Items)
                {
                    subItems.Add(item.ToString());
                }
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(subItems);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/LedgerController/GetGroup", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        s = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    }
                }
                txtValue.Value = s;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/GetDistinctGroupName").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        s1 = JsonConvert.DeserializeObject<string>(disc);
                    }
                }
                txtGrp.Value = s1; 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:getGroup() Exception: "+ex.Message+"  User: "+ uid);     
			}
		}

		/// <summary>
		/// This is used to fetch The Group after the server trip to avoid the blank value in the Combo box.
		/// </summary>
		public void fetchGroup()
		{
			try
			{
				DropGroup.Items.Clear();
				DropGroup.Items.Add("Select");
                List<string> groups = new List<string>();
                if (DropSub.SelectedItem.Text.Trim() == "Other")
                    TxtSub.Enabled = true;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/FetchGroup?SubGroupName=" + DropSub.SelectedItem.Text.Trim()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        groups = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }

                if (groups != null && groups.Count > 0)
                {
                    foreach (var grp in groups)
                        DropGroup.Items.Add(grp);
                }
				DropGroup.Items.Add("Other");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:fetchGroup() Exception: "+ex.Message+"  User: "+ uid);     
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
		/// Its checks the before save that the account period is inserted in organisaton table or not.
		/// </summary>
		/// <returns></returns>
		public bool checkAcc_Period()
		{
            bool c = false;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/LedgerController/CheckAcc_Period").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    c = JsonConvert.DeserializeObject<bool>(disc);
                }
            }
            return c;
		}

		/// <summary>
		/// This method is used to save the record.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				string SubGrp = "";
				string Group = "";

				if(!checkAcc_Period())
				{
					MessageBox.Show("Please enter the Accounts Period from Organization Details ");
					fetchGroup();
					return;
				}
                bool exist = false;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/CheckLedgerExist?ledger="+ TxtLedger.Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        exist = JsonConvert.DeserializeObject<bool>(disc);
                    }
                }
				if(exist)
				{
					MessageBox.Show("Ledger Name '"+TxtLedger.Text+"' is Allready Exist");
					return;
				}
                if (DropSub.SelectedItem.Text.Equals("Cash in hand"))
                {
                    string ledgCount = "";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/LedgerController/GetLedgersCount").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            ledgCount = JsonConvert.DeserializeObject<string>(disc);
                        }
                    }
                    if (int.Parse(ledgCount) > 0)
                    {
                        MessageBox.Show("Cash Account Allready Exist");
                        return;
                    }
                }
				
				// Check & Fetch The sub Group
				if(DropSub.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Sub Group");
					fetchGroup();
					return;
				}
				else 
				{
					if(DropSub.SelectedItem.Text == "Other")
					{
						if(TxtSub.Text.Trim() == "")
						{
							MessageBox.Show("Please specify Other Sub Group");
							TxtSub.Enabled = true; 
							fetchGroup();
							return;
						}
						else
							SubGrp =TxtSub.Text.Trim(); 
					}
					else
					{
						SubGrp = DropSub.SelectedItem.Text.Trim(); 
					}
				}

				// Check & Fetch The  Group
				if(txtTempGrp.Value== "Select")
				{
					MessageBox.Show("Please select Group");
					fetchGroup();
					return;
				}
				else 
				{
					if(txtTempGrp.Value == "Other")
					{
						if(TxtGroup.Text.Trim() == "")
						{
							MessageBox.Show("Please specify Other Group");
							TxtGroup.Enabled = true;
							fetchGroup();
							DropGroup.SelectedIndex = DropGroup.Items.IndexOf(DropGroup.Items.FindByText(txtTempGrp.Value));  
							return;
						}
						else
							Group =TxtGroup.Text.Trim(); 
					}
					else
					{
						Group = txtTempGrp.Value;
					}
				}
				
				string ledgname = "";
				string nature = "";
				double Op_bal = 0;
				string bal_type = "";
				ledgname = TxtLedger.Text.Trim();
				string op_bal = "0";
				if(TxtOpeningBal.Text.Trim() != "")
					op_bal = TxtOpeningBal.Text.Trim();
 
				Op_bal = System.Convert.ToDouble(op_bal);
				bal_type = DropBalType.SelectedItem.Text.Trim();

				if(RadioAsset.Checked)
					nature = "Assets";
				else if(RadioLiab.Checked)
					nature = "Liabilities";
				else if(RadioExp.Checked)
					nature = "Expenses";
				else
					nature= "Income"; 
            
                // Get the sub_grp_id for selected Sub_Group Name and Nature Of Payment
                LedgerModel ledger = new LedgerModel();
                ledger.LedgerName= TxtLedger.Text.Trim();
                ledger.SubGroupName = SubGrp;
                ledger.GroupNature = nature;
                ledger.GroupName = Group;
                ledger.OpeningBalance= TxtOpeningBal.Text.Trim();
                ledger.BalanceType= DropBalType.SelectedItem.Text.Trim();
                int count = 0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(ledger);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/LedgerController/GetSubGroupID", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        count = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(responseString);
                    }
                }
				if(count > 0)
				{
					MessageBox.Show("Ledger Name is already exist for selected Sub Group ");
					fetchGroup();
					DropGroup .SelectedIndex = DropGroup.Items.IndexOf(DropGroup.Items.FindByText(Group));  
					return;
				}
				
				MessageBox.Show("Ledger Saved");	
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnSave_Click New Ledger of name "+ledgname+" Saved.  User: "+ uid);     
				clear();
				checkPrevileges(); 
				getSubGroup();
				getGroup();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnSave_Click Exception: "+ex.Message+"  User: "+ uid);     
			}
		}

		/// <summary>
		/// clear all the form 
		/// </summary>
		public void clear()
		{
			tempInfo.Value="";
			TxtLedger.Text = "";
			Ledger_Name="";
			DropSub.SelectedIndex = 0;
			DropGroup.SelectedIndex = 0;
			TxtSub.Text = "";
			TxtGroup.Text = "";
			RadioAsset.Checked = true;
			TxtOpeningBal.Text = "";
			DropBalType.SelectedIndex = 0;
			btnSave.Enabled = true;
			btnEdit.Enabled = false;
			btnDelete.Enabled = false;
			btnEdit1.Visible = true;
			TxtLedger.Visible = true; 
			dropLedgerName.Visible = false; 	
		}

		/// <summary>
		/// This method is used to fatch the all ledger name with ledger ID and fill into the dropdownlist.
		/// </summary>
		protected void btnEdit1_Click(object sender, System.EventArgs e)
		{
            try
            {
                clear();
                dropLedgerName.Items.Clear();
                dropLedgerName.Items.Add("Select");
                dropLedgerName.SelectedIndex = 0;

                btnSave.Enabled = false;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                checkPrevileges();
                btnEdit1.Visible = false;
                dropLedgerName.Visible = true;
                List<string> ledgers = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/FetchAllLedgers").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        ledgers = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if (ledgers != null && ledgers.Count > 0)
                {
                    foreach (var ledger in ledgers)
                        dropLedgerName.Items.Add(ledger);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnEdit1_Click Exception: " + ex.Message + "  User: " + uid);
            }
		}

		static string Ledger_Name="";
		/// <summary>
		/// This method is used to fatch the paricular ledger record from database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropLedgerName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(dropLedgerName.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Ledger Name");
					fetchGroup();
					return;
				}
				Ledger_Name="";
                LedgerModel ledger = new LedgerModel();
				string ledgname = dropLedgerName.SelectedItem.Text.ToString();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/FetchSelectedLedger?ledgerName="+ledgname).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        ledger = JsonConvert.DeserializeObject<LedgerModel>(disc);
                    }
                }
				if(ledger!=null)
				{
					TxtLedger.Text = ledger.LedgerName;
					Ledger_Name= ledger.LedgerName;
                    DropSub.SelectedIndex = DropSub.Items.IndexOf(DropSub.Items.FindByText(ledger.SubGroupName));
					fetchGroup();
					DropGroup.SelectedIndex = DropGroup.Items.IndexOf(DropGroup.Items.FindByText(ledger.GroupName));
					txtTempGrp.Value = DropGroup.SelectedItem.Text;    
					string nature = ledger.GroupNature;
					if(nature.Equals("Assets"))
					{
						RadioAsset.Checked = false;
						RadioLiab.Checked = false;
						RadioExp.Checked = false;
						RadioIncome.Checked = false;
						RadioAsset.Checked = true;
					}
					else if(nature.Equals("Liabilities"))
					{
						RadioAsset.Checked = false;
						RadioLiab.Checked = false;
						RadioExp.Checked = false;
						RadioIncome.Checked = false;
						RadioLiab.Checked = true;
					}
					else if(nature.Equals("Expenses"))
					{
						RadioAsset.Checked = false;
						RadioLiab.Checked = false;
						RadioExp.Checked = false;
						RadioIncome.Checked = false;
						RadioExp.Checked = true;
					}
					else
					{
						RadioAsset.Checked = false;
						RadioLiab.Checked = false;
						RadioExp.Checked = false;
						RadioIncome.Checked = false;
						RadioIncome.Checked = true;
					}
					TxtOpeningBal.Text = ledger.OpeningBalance;
					DropBalType.SelectedIndex = DropBalType.Items.IndexOf(DropBalType.Items.FindByText(ledger.BalanceType)); 
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:dropLedgerName_SelectedIndexChanged Exception: "+ex.Message+"  User: "+ uid);     
			}         
		}

		/// <summary>
		/// this function seprate the time from the date and returns only date.
		/// </summary>
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
		/// This method is used to update the record in edit time who select from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			if(dropLedgerName.SelectedIndex == 0)
			{
				MessageBox.Show("Please select Ledger Name");
				fetchGroup();
				return;
			}
			try
			{           
				string SubGrp = "";
				string Group = "";
				// Check & Fetch The sub Group
				if(DropSub.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Sub Group");
					fetchGroup();
					return;
				}
				else 
				{
					if(DropSub.SelectedItem.Text == "Other")
					{
						if(TxtSub.Text.Trim() == "")
						{
							MessageBox.Show("Please specify Other Sub Group");
							TxtSub.Enabled = true; 
							fetchGroup();
							return;
						}
						else
							SubGrp =TxtSub.Text.Trim(); 
					}
					else
					{
						SubGrp = DropSub.SelectedItem.Text.Trim(); 
					}
				}

				// Check & Fetch The  Group
				if(txtTempGrp.Value== "Select" )
				{
					MessageBox.Show("Please select Group");
					fetchGroup();
					return;
				}
				else 
				{
					if(txtTempGrp.Value == "Other")
					{
						if(TxtGroup.Text.Trim() == "")
						{
							MessageBox.Show("Please specify Other Group");
							TxtGroup.Enabled = true;
							fetchGroup();
							DropGroup.SelectedIndex = DropGroup.Items.IndexOf(DropGroup.Items.FindByText(txtTempGrp.Value));  
							return;
						}
						else
							Group =TxtGroup.Text.Trim(); 
					}
					else
					{
						//Group = DropGroup.SelectedItem.Text.Trim(); 
						Group = txtTempGrp.Value;
					}
				}
				SqlDataReader SqlDtr=null;
				if(Ledger_Name!=TxtLedger.Text.Trim())
				{
					dbobj.SelectQuery("select * from ledger_master where ledger_name='"+TxtLedger.Text.Trim()+"'",ref SqlDtr);
					if(SqlDtr.HasRows)
					{
						MessageBox.Show("Ledger Name '"+TxtLedger.Text+"' is Allready Exist");
						return;
					}
					SqlDtr.Close();
				}
				
				string ledgname = "";
				string nature = "";
				double Op_bal = 0;
				string bal_type = "";
				ledgname = TxtLedger.Text.Trim();
				Op_bal = System.Convert.ToDouble(TxtOpeningBal.Text.ToString());
				bal_type = DropBalType.SelectedItem.Text.Trim();

				if(RadioAsset.Checked)
					nature = "Assets";
				else if(RadioLiab.Checked)
					nature = "Liabilities";
				else if(RadioExp.Checked)
					nature = "Expenses";
				else
					nature= "Income"; 
				string ledgname1 = "";
				ledgname1 = dropLedgerName.SelectedItem.Text.ToString();   
				string[] strArr = ledgname1.Split(new char[] {':'},ledgname1.Length);
                LedgerModel ledger = new LedgerModel();
                ledger.SubGroupName = SubGrp;
                ledger.GroupNature = nature;
                ledger.GroupName = Group;
                ledger.LedgerName = ledgname;
                //ledger.SubGroupID = subgrpid;
                ledger.LedgerID = strArr[1].Trim();
                ledger.OpeningBalance= TxtOpeningBal.Text.ToString();
                ledger.BalanceType= DropBalType.SelectedItem.Text.Trim();
                string id = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(ledger);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/LedgerController/UpdateLedger", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        id = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    }
                }

                if (id !=null && !id.Equals(strArr[1]))
                {
                    MessageBox.Show("Ledger Name is already exist for selected Sub Group ");
                    fetchGroup();
                    DropGroup.SelectedIndex = DropGroup.Items.IndexOf(DropGroup.Items.FindByText(Group));
                    return;
                }
                
                MessageBox.Show("Ledger Updated");	
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnEdit_Click Ledger  ID "+strArr[1].Trim()+" of "+ledgname+" Updated.  User: "+ uid);     
				clear();
				checkPrevileges(); 
				getSubGroup();
				getGroup();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnEdit_Click Exception: "+ex.Message+"  User: "+ uid);     
			}
		}

		/// <summary>
		/// This method is used to delete the particular record according to select ledger from dropdown list.
		/// </summary>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			//This code execute by DeleteTheRec() function.
		}

		public void DeleteTheRec()
		{
			try
			{
				string ledgname1 = "";
				ledgname1 = dropLedgerName.SelectedItem.Text.ToString();   
				string[] strArr = ledgname1.Split(new char[] {':'},ledgname1.Length);
				//SqlDataReader SqlDtr = null;
				//string id1 ="";
				//string id2 = "";
                // Check  if the Voucher Transaction of the selected Ledger to delete is present or not.
                string message = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/LedgerController/DeleteLedger?LedgerID=" + strArr[1].Trim()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        message = JsonConvert.DeserializeObject<string>(disc);
                    }
                }
                if(message!=null && message.Contains("Unable to delete Ledger"))
                {
                    MessageBox.Show("Unable to delete Ledger ");
                    fetchGroup();
                    return;
                }
                if(message != null && message.Contains("Please Remove The All Transaction Concerning Ledger"))
                {
                    MessageBox.Show("Please Remove The All Transaction Concerning Ledger");
                    return;
                }
                
                MessageBox.Show("Ledger Deleted");	
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnDelete_Click Ledger  ID "+strArr[1].Trim()+" Deleted.  User: "+ uid);     
				clear();
				checkPrevileges();
				getSubGroup();
				getGroup();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:btnDelete_Click Exception: "+ex.Message+"  User: "+ uid);     
			}
		}
	}
}