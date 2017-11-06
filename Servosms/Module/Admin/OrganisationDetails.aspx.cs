/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
using System;
using Servosms.Sysitem.Classes;
using DBOperations;
using RMG;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Admin
{
    /// <summary>
    /// Summary description for OrganisationDetails.
    /// </summary>
    public partial class OrganisationDetails : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.TextBox txtFileTitle;
		string uid;
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
				TxtDealership.Visible=false;
				try
				{
                    bool enabled = false; 
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/OrganizationDetailsController/CheckInvoiceEnabled").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            enabled = JsonConvert.DeserializeObject<bool>(disc);
                        }
                    }

                    if (enabled)
                        txtStartInvoiceNo.Enabled = false;
                    else
                        txtStartInvoiceNo.Enabled = true;
                    uid =(Session["User_Name"].ToString());
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:page_load"+ ex.Message+"  EXCEPTION" +"  "  +uid);
					Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
					return;
				}	
				if(!IsPostBack)
				{
					try
					{
						#region Check Privileges
						// Checks the user id adminnistrator or not ?
						if(Session["User_ID"].ToString ()!="1001")
						{
							Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
							return;
						}
						#endregion
						txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
						txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
						LblCompanyID.Text = "1001";
						showdealer();
						getbeat();
						city();
                        nextid();
                        //GetNextCustomerID();

                    }
					catch(Exception ex)
					{
						CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:page_load"+ ex.Message+"  EXCEPTION" +"  "  +uid);
					}
				}

                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:page_load"+ ex.Message+"  EXCEPTION " +"  "  +uid);
			}
		}

		/// <summary>
		/// This is used to concatenate city,state,country for javascripting.
		/// </summary>
		public void getbeat()
		{
            try
            {
                string str = "";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/OrganizationDetailsController/GetBeat").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        str = JsonConvert.DeserializeObject<string>(disc);
                    }
                }
                txtbeatname.Text = str;
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,class:Inventoryclass.cs,method:getbeat()" + "Exception" + ex.Message + uid);
            }
		}

		/// <summary>
		/// If the Dealer name is not present in combo box then add the dealer in combo box.	
		/// </summary>
		public void showdealer()
		{
			
		}

		/// <summary>
		/// This is used to generate next CompanyID auto.
		/// </summary>
		public void GetNextCustomerID()
		{
            #region Fetch Next Customer ID
            string CompanyID = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/OrganizationDetailsController/GetNextID").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    CompanyID = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            if (CompanyID != null)
            {
                LblCompanyID.Text = CompanyID;
                if (LblCompanyID.Text == "Null")
                    LblCompanyID.Text = "1001";
            }
			#endregion
		}

		/// <summary>
		/// This method is used to Fetch the Organisation ID from Organisation table.
		/// </summary>
		public void nextid()
		{
            #region Fetch the Next Company Number

            string CompanyID = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var Res = client.GetAsync("api/OrganizationDetailsController/GetNextID").Result;
                if (Res.IsSuccessStatusCode)
                {
                    var disc = Res.Content.ReadAsStringAsync().Result;
                    CompanyID = JsonConvert.DeserializeObject<string>(disc);
                }
            }
            if (CompanyID != null)
            {
                LblCompanyID.Text = CompanyID;
                if (LblCompanyID.Text == "Null")
                    LblCompanyID.Text = "1001";
            }
            #endregion
        }
		
		/// <summary>
		/// This is method is used to fetch city,state,country.
		/// </summary>
		public void city()
		{
			try
			{
                #region Fetch Extra Cities from Database and add to the ComboBox
                List<string> cities = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/OrganizationDetailsController/GetExtraCities").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        cities = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
				if(cities!=null && cities.Count>0)
				{
                    foreach (var city in cities)
                        DropCity.Items.Add(city);
				
				}
                #endregion

                #region Fetch Extra Cities from Database and add to the ComboBox
                List<string> states = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/OrganizationDetailsController/GetExtraStates").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        states = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if (states != null && states.Count > 0)
                {
                    foreach (var state in states)
                        DropState.Items.Add(state);
                }
                #endregion

                #region Fetch Extra Cities from Database and add to the ComboBox
                List<string> countries = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/OrganizationDetailsController/GetExtraCountry").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        countries = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if (countries != null && countries.Count > 0)
                {
                    foreach (var country in countries)
                        DropCountry.Items.Add(country);
                }
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:btnupdate_click"+ ex.Message+"  EXCEPTION" +"  "  +uid);
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
		/// This method used to check the date, date should not be blank and also check the 
		/// to date must be greter then from date. if not than show popup msg for user.
		/// </summary>
		/// <returns></returns>
		public bool checkValidity()
		{
            string ErrorMessage = "";
			bool flag = true;
			
			if(string.IsNullOrEmpty(Request.Form["txtDateFrom"].ToString()))
			{
				ErrorMessage = ErrorMessage + " - Please Select Accounts Period From Date\n";
				flag = false;
			}
			if(string.IsNullOrEmpty(Request.Form["txtDateTo"].ToString()))
			{
				ErrorMessage = ErrorMessage + " - Please Select Accounts Period To Date\n";
				flag = false;
			}
			if(flag == false)
			{
				MessageBox.Show(ErrorMessage);
				return false;
			}
			if(System.DateTime.Compare(ToMMddYYYY(Request.Form["txtDateFrom"].ToString().Trim()),ToMMddYYYY(Request.Form["txtDateTo"].ToString().Trim())) > 0)
			{
				MessageBox.Show("Date From Should be less than Date To");
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// This method is used to split the date.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.Split(new char[]{'/'},str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// This method is used to update the informatoin in organiaztion table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
			try
			{
                StringBuilder errorMessage = new StringBuilder();
                if (TxtTinno.Text != string.Empty)
                {
                    string sPattern = "^[a-zA-Z0-9]+$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(TxtTinno.Text, sPattern))
                    {
                        errorMessage.Append("- Please Enter GSTIN No. in Alpha Numeric");
                        errorMessage.Append("\n");
                    }
                }
                if (errorMessage.Length > 0)
                {
                    MessageBox.Show(errorMessage.ToString());
                    return;
                }
                CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:btnupdate_click"+ "  "  +uid);
				if(DropDealerShip.SelectedIndex == 0)
				{
					MessageBox.Show("Please select the Dealership");
					return;
				}
				if(!checkValidity())
				{
					return;
				}
				if(LblCompanyID.Visible==true)
				{
					if(LblCompanyID.Text=="")
					{
						MessageBox.Show("Organisation Details Already Stored ");
						return;
					}
					saveimage();
				}
				else if(Drop.Visible==true)
				{
					Label1.Visible=true;
					saveimage1();
					Label1.Visible=true;
					Drop.Visible = false; 
					Button1.Visible = true;
					LblCompanyID.Visible=true;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:btnupdate_click"+ ex.Message+"  EXCEPTION" +"  "  +uid);
				TextBox1.Text="j";
			}
		}

		/// <summary>
		/// This method is used to clear the form.
		/// </summary>
		public void clear()
		{
			txtDealerName.Text="";
			TxtDealership.Text="";
			TxtAddress .Text="";
			DropCity.SelectedIndex=0;
			DropState.SelectedIndex=0;
			DropCountry .SelectedIndex=0;
			txtPhoneOff.Text="";
			TxtFaxNo.Text="";
			txtEMail.Text="";
			TxtWebsite.Text="";
			TxtTinno.Text="";
			txtfood.Text="";
			TxtWMlic .Text="";
			DropDealerShip.SelectedIndex = 0; 
			Drop.Items.Clear();
			Drop.Items.Add("Select");  
			Drop.SelectedIndex=0;
			txtMsg.Text = "";
			txtVatRate.Text = "";
			txtDateFrom.Text = "";
			txtDateTo.Text = "";
			dropstateoffice.SelectedIndex=0;
			txtentry.Text="";
			txtStartInvoiceNo.Text="";
		}

		/// <summary>
		/// This method is used to Insert the Organisation details into organisation table.
		/// </summary>
		public void saveimage()
		{
			try
			{
				//This Code Is hide  20 feb 2006  , This code is used for image upload in data base.
				
				if ( txtFileContents.PostedFile != null )
				{
					int fileLen = txtFileContents.PostedFile.FileName.Length;
					int Lastindex = txtFileContents.PostedFile.FileName.LastIndexOf("\\");
					string filename =  txtFileContents.PostedFile.FileName.Substring(Lastindex + 1);
					
                    OrganizationModel organization = new OrganizationModel();
				
					organization.InvoiceNo= txtStartInvoiceNo.Text;
					organization.CompanyID= LblCompanyID.Text;
					organization.DealerName= txtDealerName.Text;
					if(!DropDealerShip.SelectedItem.Text.ToString().Equals("Other"))
					{
						organization.DealerShip= DropDealerShip.SelectedItem.Value.ToString();
					}
					else
					{
						organization.DealerShip= TxtDealership.Text;
						if(DropDealerShip.Items.IndexOf(DropDealerShip.Items.FindByValue(TxtDealership.Text)) == -1)      
							DropDealerShip.Items.Add( TxtDealership.Text);
					}

                    organization.Address = TxtAddress.Text.ToString();
					//		cmdInsert.Parameters.Add( "@Address", TxtAddress .Text.ToString()+" "+TxtAddress1.Text.ToString()+" "+TxtAddress2.Text.ToString() );
					organization.City= DropCity.SelectedItem.Value.ToString();
					organization.State= DropState.SelectedItem.Value.ToString();
					organization.Country= DropCountry .SelectedItem.Value.ToString();
					organization.PhoneOff= txtPhoneOff.Text.ToString();
                    organization.FaxNo = TxtFaxNo.Text.ToString();
					organization.EMail= txtEMail.Text.ToString();
					organization.Website= TxtWebsite .Text.ToString() ;
					organization.Tinno= TxtTinno.Text.ToString() ;
					//	cmdInsert.Parameters.Add( "@ExplosiveNo", txtExplosive.Text.ToString() );
					organization.Entrytax= txtentry.Text.Trim().ToString();
					organization.FoodLicNO= txtfood.Text.ToString() ;
					organization.WMlic= TxtWMlic .Text.ToString() ;
					organization.Logo=txtFileContents.PostedFile.FileName.ToString();
					//	cmdInsert.Parameters.Add( "@Div_Office",txtDivOffice.Text.ToString() );
					organization.DivOffice=dropstateoffice.SelectedItem.Value.ToString();
					organization.Message= txtMsg.Text.ToString();
					organization.VATRate=txtVatRate.Text.Trim().ToString();    
					organization.DateFrom = GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim());    
					organization.DateTo = GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim());
					Session["Message"] = txtMsg.Text.ToString();
					Session["VAT_Rate"] = txtVatRate.Text.Trim(); 
					Session["Entrytax"] = txtentry.Text.Trim();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(organization);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/OrganizationDetailsController/InsertOrganizationDetails", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(responseString);
                        }
                    }
                    //********************************
                   	clear();
					MessageBox.Show("Organisation Details Saved");	
					CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:EmployeeClass.cs ,Method:saveImage(). Organisation Details Saved. User_ID: "+uid);
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:EmployeeClass.cs ,Method:image"+"  EXCEPTION "+ ex.Message+uid);
			}
		}
		
		/// <summary>
		/// This method is used to fatch the organization id from organization table and fill the 
		/// dropdownlist on edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Button1_Click(object sender, System.EventArgs e)
		{  
			CreateLogFiles Err = new CreateLogFiles();
			try
			{
				Label1.Visible=true;
				//txtFileTitle.Enabled=false;
				txtFileContents.EnableViewState =false;
				Button1.Visible=false;
				Drop.Visible=true;
				LblCompanyID.Visible=false;
				txtFileContents .Visible =false;

                List<string> orgs = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/OrganizationDetailsController/GetOrganizations").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        orgs = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }
                if (orgs != null && orgs.Count > 0)
                {
                    foreach (var org in orgs)
                        Drop.Items.Add(org);

                }
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:saveButton   EXCEPTION: "+ ex.Message+"  User_ID: "+uid);
			}
		}

		/// <summary>
		/// This method is used to update the Organisation details.
		/// </summary>
		public void saveimage1()
		{
			try
			{
                OrganizationModel organization = new OrganizationModel();

                organization.InvoiceNo = txtStartInvoiceNo.Text;
                organization.CompanyID = Drop.SelectedItem.Value.ToString();
                organization.DealerName = txtDealerName.Text;
                if (!DropDealerShip.SelectedItem.Text.ToString().Equals("Other"))
                {
                    organization.DealerShip= DropDealerShip.SelectedItem.Value.ToString();
                }
                else
                {
                    organization.DealerShip = TxtDealership.Text.Trim();
                    if (DropDealerShip.Items.IndexOf(DropDealerShip.Items.FindByValue(TxtDealership.Text)) == -1)
                        DropDealerShip.Items.Add(TxtDealership.Text);
                }
                organization.Address= TxtAddress.Text.ToString();
                organization.City= DropCity.SelectedItem.Value.ToString();
                organization.State = DropState.SelectedItem.Value.ToString();
                organization.Country = DropCountry.SelectedItem.Value.ToString();
                organization.PhoneOff = txtPhoneOff.Text.ToString();
                organization.FaxNo = TxtFaxNo.Text.ToString();
                organization.EMail = txtEMail.Text.ToString();
                organization.Website = TxtWebsite.Text.ToString();
                organization.Tinno = TxtTinno.Text.ToString();
                organization.Entrytax = txtentry.Text.Trim().ToString();
                organization.FoodLicNO = txtfood.Text.ToString();
                organization.WMlic = TxtWMlic.Text.ToString();
                organization.Logo = txtFileContents.PostedFile.FileName.ToString();
                organization.DivOffice = dropstateoffice.SelectedItem.Value.ToString();
                organization.Message = txtMsg.Text.ToString();
                organization.VATRate = txtVatRate.Text.Trim().ToString();
                organization.DateFrom = GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim());
                organization.DateTo = GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim());
                Session["Message"] = txtMsg.Text.ToString();
                Session["VAT_Rate"] = txtVatRate.Text.Trim();
                Session["Entrytax"] = txtentry.Text.Trim();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(organization);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/OrganizationDetailsController/UpdateOrganizationDetails", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:saveImage() Organisation Details Updated.  userid "+uid);

				clear();
				MessageBox.Show("Organisation Details Updated ");	
				showdealer();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:saveImage(). EXCEPTION: "+ex.Message+".   userid "+uid);
			}
		}

		/// <summary>
		/// This method is used to set the image path in session variable.
		/// </summary>
		public void SaveimageinFolder()
		{
			string strpath =System.Configuration .ConfigurationSettings .AppSettings["FileUploadPath"];  
			string strExt=System.IO.Path .GetFileName (txtFileContents.PostedFile.FileName);
			string  filePath=strpath+"/"+strExt;
			txtFileContents.PostedFile.SaveAs(filePath);
			Session["rajImage"]=filePath; 
		}

		/// <summary>
		/// This method is used to Displays the Organisation details information on edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Drop_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				txtdumy.Text=Drop.SelectedItem.Value.ToString();
				if(txtdumy.Text=="Select")
				{
					MessageBox.Show("Please Select Company ID ");
				}
				else
				{
                    OrganizationModel organization = new OrganizationModel();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/OrganizationDetailsController/GetSelectedOrganization?OrgID="+ Drop.SelectedItem.Value).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            organization = JsonConvert.DeserializeObject<OrganizationModel>(disc);
                        }
                    }
                    
					if(organization!=null)
					{
						txtDealerName.Text=organization.DealerName;  
						DropDealerShip.SelectedIndex=(DropDealerShip.Items.IndexOf((DropDealerShip.Items.FindByValue (organization.DealerShip))));
						TxtAddress.Text=organization.Address;
						DropCity .SelectedIndex=(DropCity.Items.IndexOf((DropCity.Items.FindByValue(organization.City))));
						DropState .SelectedIndex=(DropState.Items.IndexOf((DropState.Items.FindByValue(organization.State))));
						DropCountry .SelectedIndex=(DropCountry .Items.IndexOf((DropCountry .Items.FindByValue(organization.Country))));
						txtPhoneOff.Text=organization.PhoneOff;
						TxtFaxNo.Text=organization.FaxNo; 
						txtEMail .Text =organization.EMail; 
						TxtWebsite.Text= organization.Website; 
						TxtTinno .Text=organization.Tinno;  
						txtentry.Text=organization.Entrytax; 
						txtfood.Text= organization.FoodLicNO;  
						TxtWMlic.Text= organization.WMlic;  
						dropstateoffice .SelectedIndex =  (dropstateoffice .Items.IndexOf((dropstateoffice  .Items.FindByValue(organization.StateOffice))));
						txtFileContents.Name = organization.Logo; 
						txtMsg.Text = organization.Message;
						txtVatRate.Text = GenUtil.strNumericFormat(organization.VATRate);  
						txtDateFrom.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(organization.DateFrom.Trim ()))); 
						txtDateTo.Text = checkDate(GenUtil.str2DDMMYYYY(trimDate(organization.DateTo.Trim ()))); 
						txtStartInvoiceNo.Text=organization.InvoiceNo;
					}
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:OrganisationDetails.aspx,Class:InventoryClass.cs ,Method:Drop_SelectedIndexChanged(). EXCEPTION: "+ex.Message+".   userid "+uid);
			}
			
		}
		
		/// <summary>
		/// This is used to make blank if date is "1/1/1900".
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string checkDate(string str)
		{
			if(!str.Trim().Equals(""))
			{
				if(str.Trim().Equals("1/1/1900"))
					str = "";
			}
			return str;
		}

		/// <summary>
		/// This method is used to seprate time from date and returns only date in mm/dd/yyyy
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
		/// If the dealer name is other then visible the Text Box to enter the dealer name. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropDealerShip_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtdummy.Text=DropDealerShip.SelectedItem.Value.ToString();
			if(txtdummy.Text=="Other")
			{
				TxtDealership.Visible=true;
			}
			else
			{
				TxtDealership.Visible=false;
			}
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropCity_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		protected void txtDealerName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtdummy_TextChanged(object sender, System.EventArgs e)
		{
		
		}

        protected void txtDateFrom_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDateTo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}