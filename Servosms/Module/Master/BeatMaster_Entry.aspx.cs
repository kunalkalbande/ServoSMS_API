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
using Servosms.Sysitem.Classes;
using RMG;
using DBOperations;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for BeatMaster_Entry.
	/// </summary>
	public partial class BeatMaster_Entry : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
        string BaseUri = "http://localhost:64862";
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
				uid=(Session["User_Name"].ToString());
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx ,Method:pageload"+"  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:pageload"+"  EXCEPTION "+ ex.Message+"  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				checkPrevileges();
				Edit1.Visible=false;
			}
			FillID();
		}
		
		/// <summary>
		/// This is used to check permissoin.
		/// and this method checks the user privileges from session.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="3";
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
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			btnSave.CausesValidation=true;
			lblBeatNo.Visible=true;
			DropBeatNo.Visible=false; 
			btnEdit.Enabled=false;
			btnSave.Enabled=true;
			btnDelete.Enabled =false;
			Clear();
		}


		/// <summary>
		/// Method Fetch the next beat ID from Beat_Master table.
		/// </summary>
		public void FillID()
		{
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/BeatMasterEntryController/GetID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        lblBeatNo.Text = JsonConvert.DeserializeObject<string>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:FillID().  EXCEPTION "+ ex.Message+"  "+uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		
		}

		/// <summary>
		/// This is used fill all ID's in dropdown.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			DropBeatNo.Visible=true;
			btnEdit.Visible=false;
			Edit1.Visible=true;
			Edit1.Enabled = true;
			btnDelete.Enabled = true;  
			btnSave.CausesValidation=false;
			lblBeatNo.Visible=false;
			
			//		
			Clear();
			try
			{
                List<string> BeatIds = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/BeatMasterEntryController/GetAllBeatIDs").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        BeatIds = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

				DropBeatNo.Items.Clear();
				DropBeatNo.Items.Add("Select");
                if (BeatIds != null && BeatIds.Count > 0)
                {
                    foreach (var beat in BeatIds)
                        DropBeatNo.Items.Add(beat);
                }
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:btnEdit_Click().  EXCEPTION "+ ex.Message+"  "+uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}
		/// <summary>
		/// This is to Save the beat information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try 
			{
				int flag=0;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/BeatMasterEntryController/FetchCity?City="+ txtCity.Text).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        flag = JsonConvert.DeserializeObject<int>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

				if (DropBeatNo.Visible==false)
				{
                    BeatMasterModel beatmaster = new BeatMasterModel();

                    beatmaster.City =StringUtil.FirstCharUpper(txtCity.Text.ToString());
                    beatmaster.State= StringUtil.FirstCharUpper(txtState.Text.ToString());
                    beatmaster.Country=StringUtil.FirstCharUpper(txtCountry.Text.ToString());
                    beatmaster.BeatNo = lblBeatNo.Text;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(beatmaster);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/BeatMasterEntryController/InsertBeatMaster", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var rr = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                        }
                        else
                            response.EnsureSuccessStatusCode();
                    }
                    CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method: btnSave_Click"+"  Beatno  "+ beatmaster.BeatNo + " city  "+beatmaster.City    +"   state  "+ beatmaster.State   +" Country"+beatmaster.Country+ " IS SAVED  "+" userid  "+ uid);
					FillID();
					lblBeatNo.Visible=true;
					DropBeatNo.Visible=false;
					MessageBox.Show("Beat details Saved");
					Clear();
				}
				else if (DropBeatNo.Visible==true && DropBeatNo.SelectedIndex==0 )
				{
					MessageBox.Show("Please select the Beat Number to Edit");
				}
				if(flag==1)
				{
					RMG.MessageBox.Show("City already Exits");				
				}
				checkPrevileges();
			}
			catch(Exception ex)
			{
                CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method: btnSave_Click" + ex.Message + " userid  " + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}
		
		/// <summary>
		/// This is to delete the particular beat from the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (DropBeatNo.Visible==true && DropBeatNo.SelectedIndex==0 )
				{
					MessageBox.Show("Please select the Beat Number to Delete");
				}
				else
				{		
					string bno = DropBeatNo.SelectedItem.Value ;  
					string[] Beat = bno.Split(new char[] {':'},bno.Length);
                    string message = "";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/BeatMasterEntryController/DeleteBeat?BeatNo="+ Beat[0]).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            message = JsonConvert.DeserializeObject<string>(disc);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                   
					MessageBox.Show("Beat deleted");
					CreateLogFiles.ErrorLog("Form:BeatMasterEntry.aspx,Method: btnDelete_Click"+"  Beat no  "+ Beat[0] + "  is DELETED  " +"  user id  "+uid);
			
					Clear(); 
					btnEdit.Visible=true;
					Edit1.Visible=false;
					DropBeatNo.Visible=false;
					lblBeatNo.Visible=true;
					FillID();
					checkPrevileges();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntry.aspx,Method: btnDelete_Click. EXCEPTION  "+ex.Message+"  user id  "+uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}

		/// <summary>
		/// This method fetch the beat related information and put into the textboxes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropBeatNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(DropBeatNo.SelectedIndex==0)
					return;
                string cty = DropBeatNo.SelectedItem.Value;
                string[] arr = cty.Split(new char[] { ':' }, cty.Length);
                BeatMasterModel beat = new BeatMasterModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/BeatMasterEntryController/GetSelectedBeat?BeatNo="+ arr[0]).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        beat = JsonConvert.DeserializeObject<BeatMasterModel>(disc);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
				if(beat !=null)
				{
                    txtCity.Text = beat.City;
                    txtState.Text = beat.State;
                    txtCountry.Text = beat.Country;
				}
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:DropBeatNo_SelectedIndexChanged"+uid);
			}
			catch(Exception ex)
			{
                CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:DropBeatNo_SelectedIndexChange" + "  EXCEPTION " + ex.Message + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}

		/// <summary>
		/// This is to clear the form
		/// </summary>
		public void Clear()
		{			
			txtCity.Text="";
			txtState.Text="";
			txtCountry.Text=""; 
		}

		/// <summary>
		/// This method is used to update the particular beat information, who is select from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Edit1_Click(object sender, System.EventArgs e)
		{
            BeatMasterModel beat = new BeatMasterModel();
            try
			{
                string cty = DropBeatNo.SelectedItem.Value;
                string[] arr = cty.Split(new char[] { ':' }, cty.Length);

                beat.City = txtCity.Text;
                beat.State = txtState.Text;
                beat.Country=txtCountry.Text;
                beat.BeatNo =arr[0] ;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(beat);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/BeatMasterEntryController/UpdateBeat", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        var rr = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
                    }
                    else
                        response.EnsureSuccessStatusCode();
                }

                MessageBox.Show("Beat Updated");
				Clear();
				DropBeatNo.Visible=false;
				lblBeatNo.Visible=true;
				Edit1.Visible=false;
				btnEdit.Visible=true; 
				checkPrevileges();
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx ,method Edit1_Click,"+"  Beat no   "+ arr[0] + "City Updated to   "+ beat.City + "  user:"+uid);
			}
			catch(Exception ex)
			{
                CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx ,method Edit1_Click," + ex.Message + "  user:" + uid);
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
            }
		}
	}
}