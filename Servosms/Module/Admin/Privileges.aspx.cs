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
using RMG;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Servosms.Module.Admin
{
	/// <summary>
	/// Summary description for Privileges.
	/// </summary>
	public partial class Privileges : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		//protected System.Web.UI.WebControls.CheckBox chkParties;
		string uid;
        string uname;
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
				btnAllocate.Visible=false;
				uid=(Session["User_Name"].ToString ());
				CreateLogFiles.ErrorLog("Form:Privileges.aspx,Method:Page_load    userid  "+uid  );	
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Privileges.aspx,Method:Page_load   EXCEPTION:  "+ex.Message+" userid  "+uid  );
				Response.Redirect("ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				#region Check Privileges if user id admin then grant the access
				if(Session["User_ID"].ToString ()!="1001")
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				#endregion

				
				try
				{
                    #region Fetch All Users Information
                    List<string> Users = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/PriviligesController/GetAllUsers").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            Users = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                    }
                    if (Users != null && Users.Count > 0)
                    {
                        foreach (var user in Users)
                            DropUserID.Items.Add(user);
                    }
                    
					#endregion
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Privileges.aspx,Method:Page_load   EXCEPTION:  "+ex.Message+" userid  "+uid  );
				}
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
		/// This method is used to fatch all information from database according to selected user.
		/// </summary>
		protected void DropUserID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int a=0;
			int Modules=6;
			/*coment by vikas 29.12.2012 int[] SubModules={4,5,12,13,52,3};
			CheckBox[] ChkView={chkView1, chkView2, chkView3, chkView4, chkView5, chkView6, chkView7, chkView8, chkView9, chkView10, chkView11, chkView12, chkView13, chkView14, chkView15, chkView16, chkView17, chkView18, chkView66, chkView67, chkView68, chkView19, chkView20, chkView21, chkView22, chkView23, chkView24, chkView25, chkView26, chkView27,chkView44,chkView64,chkView69,chkView70, chkView28, chkView29, chkView30, chkView31, chkView32, chkView33, chkView34, chkView35, chkView36, chkView37, chkView38, chkView39,chkView40,chkView41,chkView42,chkView43,chkView45,chkView46,chkView47,chkView48,chkView49,chkView50,chkView54,chkView55,chkView56,chkView57,chkView58,chkView59,chkView60,chkView61,chkView62,chkView63,chkView65,chkView71,chkView72,chkView73,chkView74,chkView76,chkView77,chkView78,chkView79,chkView80,chkView81,chkView82,chkView83,chkView84,chkView85,chkView86,chkView87,chkView88,chkView89,chkView90,chkView51,chkView52,chkView53};
			CheckBox[] ChkAdd={chkAdd1, chkAdd2, chkAdd3, chkAdd4, chkAdd5, chkAdd6, chkAdd7, chkAdd8, chkAdd9, chkAdd10, chkAdd11, chkAdd12, chkAdd13, chkAdd14, chkAdd15, chkAdd16, chkAdd17, chkAdd18, chkAdd66, chkAdd67, chkAdd68, chkAdd19, chkAdd20, chkAdd21, chkAdd22, chkAdd23, chkAdd24, chkAdd25, chkAdd26, chkAdd27,chkAdd44,chkAdd64,chkAdd69,chkAdd70, chkAdd28, chkAdd29, chkAdd30, chkAdd31, chkAdd32, chkAdd33, chkAdd34, chkAdd35, chkAdd36, chkAdd37, chkAdd38, chkAdd39,chkAdd40, chkAdd41,chkAdd42,chkAdd43,chkAdd45,chkAdd46,chkAdd47,chkAdd48,chkAdd49,chkAdd50,chkAdd54,chkAdd55,chkAdd56,chkAdd57,chkAdd58,chkAdd59,chkAdd60,chkAdd61,chkAdd62,chkAdd63,chkAdd65,chkAdd71,chkAdd72,chkAdd73,chkAdd74,chkAdd76,chkAdd77,chkAdd78,chkAdd79,chkAdd80,chkAdd81,chkAdd82,chkAdd83,chkAdd84,chkAdd85,chkAdd86,chkAdd87,chkAdd88,chkAdd89,chkAdd90,chkAdd51,chkAdd52,chkAdd53};
			CheckBox[] ChkEdit={chkEdit1, chkEdit2, chkEdit3, chkEdit4, chkEdit5, chkEdit6, chkEdit7, chkEdit8, chkEdit9, chkEdit10, chkEdit11, chkEdit12, chkEdit13, chkEdit14, chkEdit15, chkEdit16, chkEdit17, chkEdit18, chkEdit66, chkEdit67, chkEdit68, chkEdit19, chkEdit20, chkEdit21, chkEdit22, chkEdit23, chkEdit24, chkEdit25, chkEdit26, chkEdit27,chkEdit44,chkEdit64,chkEdit69,chkEdit70, chkEdit28, chkEdit29, chkEdit30, chkEdit31, chkEdit32, chkEdit33, chkEdit34, chkEdit35, chkEdit36, chkEdit37, chkEdit38, chkEdit39,chkEdit40, chkEdit41,chkEdit42,chkEdit43,chkEdit45,chkEdit46,chkEdit47,chkEdit48,chkEdit49,chkEdit50,chkEdit54,chkEdit55,chkEdit56,chkEdit57,chkEdit58,chkEdit59,chkEdit60,chkEdit61,chkEdit62,chkEdit63,chkEdit65,chkEdit71,chkEdit72,chkEdit73,chkEdit74,chkEdit76,chkEdit77,chkEdit78,chkEdit79,chkEdit80,chkEdit81,chkEdit82,chkEdit83,chkEdit84,chkEdit85,chkEdit86,chkEdit87,chkEdit88,chkEdit89,chkEdit90,chkEdit51,chkEdit52,chkEdit53};
			CheckBox[] ChkDel={chkDel1, chkDel2, chkDel3, chkDel4, chkDel5, chkDel6, chkDel7, chkDel8, chkDel9, chkDel10, chkDel11, chkDel12, chkDel13, chkDel14, chkDel15, chkDel16, chkDel17, chkDel18, chkDel66, chkDel67, chkDel68, chkDel19, chkDel20, chkDel21, chkDel22, chkDel23, chkDel24, chkDel25, chkDel26, chkDel27,chkDel44,chkDel64,chkDel69,chkDel70, chkDel28, chkDel29, chkDel30, chkDel31, chkDel32, chkDel33, chkDel34, chkDel35, chkDel36, chkDel37, chkDel38, chkDel39,chkDel40, chkDel41,chkDel42,chkDel43,chkDel45,chkDel46,chkDel47,chkDel48,chkDel49,chkDel50,chkDel54,chkDel55,chkDel56,chkDel57,chkDel58,chkDel59,chkDel60,chkDel61,chkDel62,chkDel63,chkDel65,chkDel71,chkDel72,chkDel73,chkDel74,chkDel76,chkDel77,chkDel78,chkDel79,chkDel80,chkDel81,chkDel82,chkDel83,chkDel84,chkDel85,chkDel86,chkDel87,chkDel88,chkDel89,chkDel90,chkDel51,chkDel52,chkDel53};*/

			int[] SubModules={4,5,12,13,61,3};
			CheckBox[] ChkView={chkView1, chkView2, chkView3, chkView4, chkView5, chkView6, chkView7, chkView8, chkView9, chkView10, chkView11, chkView12, chkView13, chkView14, chkView15, chkView16, chkView17, chkView18, chkView66, chkView67, chkView68, chkView19, chkView20, chkView21, chkView22, chkView23, chkView24, chkView25, chkView26, chkView27,chkView44,chkView64,chkView69,chkView70, chkView28, chkView29, chkView30, chkView31, chkView32, chkView33, chkView34, chkView35, chkView36, chkView37, chkView38, chkView39,chkView40,chkView41,chkView42,chkView43,chkView45,chkView46,chkView47,chkView48,chkView49,chkView50,chkView54,chkView55,chkView56,chkView57,chkView58,chkView59,chkView60,chkView61,chkView62,chkView63,chkView65,chkView71,chkView72,chkView73,chkView74,chkView76,chkView77,chkView78,chkView79,chkView80,chkView81,chkView82,chkView83,chkView84,chkView85,chkView86,chkView87,chkView88,chkView89,chkView90,chkView91,chkView92,chkView93,chkView94,chkView95,chkView96,chkView97,chkView98,chkView99,chkView51,chkView52,chkView53};
			CheckBox[] ChkAdd={chkAdd1, chkAdd2, chkAdd3, chkAdd4, chkAdd5, chkAdd6, chkAdd7, chkAdd8, chkAdd9, chkAdd10, chkAdd11, chkAdd12, chkAdd13, chkAdd14, chkAdd15, chkAdd16, chkAdd17, chkAdd18, chkAdd66, chkAdd67, chkAdd68, chkAdd19, chkAdd20, chkAdd21, chkAdd22, chkAdd23, chkAdd24, chkAdd25, chkAdd26, chkAdd27,chkAdd44,chkAdd64,chkAdd69,chkAdd70, chkAdd28, chkAdd29, chkAdd30, chkAdd31, chkAdd32, chkAdd33, chkAdd34, chkAdd35, chkAdd36, chkAdd37, chkAdd38, chkAdd39,chkAdd40, chkAdd41,chkAdd42,chkAdd43,chkAdd45,chkAdd46,chkAdd47,chkAdd48,chkAdd49,chkAdd50,chkAdd54,chkAdd55,chkAdd56,chkAdd57,chkAdd58,chkAdd59,chkAdd60,chkAdd61,chkAdd62,chkAdd63,chkAdd65,chkAdd71,chkAdd72,chkAdd73,chkAdd74,chkAdd76,chkAdd77,chkAdd78,chkAdd79,chkAdd80,chkAdd81,chkAdd82,chkAdd83,chkAdd84,chkAdd85,chkAdd86,chkAdd87,chkAdd88,chkAdd89,chkAdd90,chkAdd91,chkAdd92,chkAdd93,chkAdd94,chkAdd95,chkAdd96,chkAdd97,chkAdd98,chkAdd99,chkAdd51,chkAdd52,chkAdd53};
			CheckBox[] ChkEdit={chkEdit1, chkEdit2, chkEdit3, chkEdit4, chkEdit5, chkEdit6, chkEdit7, chkEdit8, chkEdit9, chkEdit10, chkEdit11, chkEdit12, chkEdit13, chkEdit14, chkEdit15, chkEdit16, chkEdit17, chkEdit18, chkEdit66, chkEdit67, chkEdit68, chkEdit19, chkEdit20, chkEdit21, chkEdit22, chkEdit23, chkEdit24, chkEdit25, chkEdit26, chkEdit27,chkEdit44,chkEdit64,chkEdit69,chkEdit70, chkEdit28, chkEdit29, chkEdit30, chkEdit31, chkEdit32, chkEdit33, chkEdit34, chkEdit35, chkEdit36, chkEdit37, chkEdit38, chkEdit39,chkEdit40, chkEdit41,chkEdit42,chkEdit43,chkEdit45,chkEdit46,chkEdit47,chkEdit48,chkEdit49,chkEdit50,chkEdit54,chkEdit55,chkEdit56,chkEdit57,chkEdit58,chkEdit59,chkEdit60,chkEdit61,chkEdit62,chkEdit63,chkEdit65,chkEdit71,chkEdit72,chkEdit73,chkEdit74,chkEdit76,chkEdit77,chkEdit78,chkEdit79,chkEdit80,chkEdit81,chkEdit82,chkEdit83,chkEdit84,chkEdit85,chkEdit86,chkEdit87,chkEdit88,chkEdit89,chkEdit90,chkEdit91,chkEdit92,chkEdit93,chkEdit94,chkEdit95,chkEdit96,chkEdit97,chkEdit98,chkEdit99,chkEdit51,chkEdit52,chkEdit53};
			CheckBox[] ChkDel={chkDel1, chkDel2, chkDel3, chkDel4, chkDel5, chkDel6, chkDel7, chkDel8, chkDel9, chkDel10, chkDel11, chkDel12, chkDel13, chkDel14, chkDel15, chkDel16, chkDel17, chkDel18, chkDel66, chkDel67, chkDel68, chkDel19, chkDel20, chkDel21, chkDel22, chkDel23, chkDel24, chkDel25, chkDel26, chkDel27,chkDel44,chkDel64,chkDel69,chkDel70, chkDel28, chkDel29, chkDel30, chkDel31, chkDel32, chkDel33, chkDel34, chkDel35, chkDel36, chkDel37, chkDel38, chkDel39,chkDel40, chkDel41,chkDel42,chkDel43,chkDel45,chkDel46,chkDel47,chkDel48,chkDel49,chkDel50,chkDel54,chkDel55,chkDel56,chkDel57,chkDel58,chkDel59,chkDel60,chkDel61,chkDel62,chkDel63,chkDel65,chkDel71,chkDel72,chkDel73,chkDel74,chkDel76,chkDel77,chkDel78,chkDel79,chkDel80,chkDel81,chkDel82,chkDel83,chkDel84,chkDel85,chkDel86,chkDel87,chkDel88,chkDel89,chkDel90,chkDel91,chkDel92,chkDel93,chkDel94,chkDel95,chkDel96,chkDel97,chkDel98,chkDel99,chkDel51,chkDel52,chkDel53};

			
			try
			{

                #region Fetch User Name of Selected User
                UserModel user = new UserModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/PriviligesController/GetSelectedUser?UserID="+ DropUserID.SelectedItem.Value).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        user = JsonConvert.DeserializeObject<UserModel>(disc);
                    }
                }

                string Userid="";
                if (user != null)
                {
                    Userid = user.UserID;
                    txtUserName.Text = user.UserName;
                }
				#endregion

				#region Fetch Privileges of the Selected User
				ClearCheckBox();
                List<PriviligesModel> priviliges = new List<PriviligesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/PriviligesController/GetPriviliges?id=" + Userid).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        priviliges = JsonConvert.DeserializeObject<List<PriviligesModel>>(disc);
                    }
                }
                if(priviliges!=null && priviliges.Count>0)
				{	
					a=0;
					PanelAcc.Visible=true;
					PanelEmp.Visible=true; 
					PanelMaster.Visible=true;
					PanelInventory.Visible=true;
					PanelAdmin.Visible=true; 
					panelLogistics.Visible = true;  
					for(int i=1;i<=Modules;i++)
					{
						for(int j=1;j<=SubModules[i-1];j++)
						{
                            foreach (var privilige in priviliges)
                            {
                                if ((i == System.Convert.ToInt32(privilige.Module_ID)) && (j == System.Convert.ToInt32(privilige.SubModule_ID)))
                                {
                                    if (privilige.ViewFlag == "1")
                                        ChkView[a].Checked = true;
                                    else
                                        ChkView[a].Checked = false;
                                    if (privilige.Add_Flag == "1")
                                        ChkAdd[a].Checked = true;
                                    else
                                        ChkAdd[a].Checked = false;
                                    if (privilige.Edit_Flag == "1")
                                        ChkEdit[a].Checked = true;
                                    else
                                        ChkEdit[a].Checked = false;
                                    if (privilige.Del_Flag == "1")
                                        ChkDel[a].Checked = true;
                                    else
                                        ChkDel[a].Checked = false;
                                }
                            }
							a++;
						}
					}
				}
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Privileges.aspx,Method:DropUserID_SelectedIndexChanged   EXCEPTION:  "+ex.Message+" userid  "+uid  );
			}
		}
		
		/// <summary>
		/// This method is used to clear the checkboxes of selected module
		/// </summary>
		public void ClearCheckBox()
		{
			foreach(Control ctl in Page.Controls)
			{
				foreach (Control childctl in ctl.Controls)
				{
					if(childctl is CheckBox)
					{
						((CheckBox)childctl).Checked=false;
					}
				}
			}
			foreach(Control ctl in PanelAcc.Controls)
			{
				if(ctl is CheckBox)
				{
					((CheckBox)ctl).Checked=false;
				}
			}
			foreach(Control ctl in PanelEmp.Controls)
			{
				if(ctl is CheckBox)
				{
					((CheckBox)ctl).Checked=false;
				}
			}
			foreach(Control ctl in PanelMaster.Controls)
			{
				if(ctl is CheckBox)
				{
					((CheckBox)ctl).Checked=false;
				}
			}
			foreach(Control ctl in PanelInventory.Controls)
			{
				if(ctl is CheckBox)
				{
					((CheckBox)ctl).Checked=false;
				}
			}
			foreach(Control ctl in panelLogistics.Controls)
			{
				if(ctl is CheckBox)
				{
					((CheckBox)ctl).Checked=false;
				}
			}
		}
		
		/// <summary>
		/// This method is used to clear the form.
		/// </summary>
		public void Clear()
		{
			DropUserID.SelectedIndex=0;
			txtUserName.Text="";
			ClearCheckBox();
			PanelAcc.Visible=false;
			PanelEmp.Visible=false;
			PanelMaster.Visible=false;
			PanelInventory.Visible=false;
			//PanelPetrolpump.Visible=false;
			PanelAdmin.Visible=false; 
			panelLogistics.Visible = false; 
		}

		/// <summary>
		/// This method is used to allocate the module of user if checkbox is checked.
		/// </summary>
		protected void btnAllocate_Click(object sender, System.EventArgs e)
		{
			if(chkAccount.Checked)
				PanelAcc.Visible=true;
			else
				PanelAcc.Visible=false;
			if(chkEmployee.Checked)
				PanelEmp.Visible=true; 
			else
				PanelEmp.Visible=false; 
			if(chkMaster.Checked)
				PanelMaster.Visible=true; 
			else
				PanelMaster.Visible=false; 
			if(chkInventory.Checked)
				PanelInventory.Visible=true; 
			else
				PanelInventory.Visible=false; 
			

			if(checkLogistics.Checked)
				panelLogistics.Visible = true;
			else
				panelLogistics.Visible = false;
		}

		/// <summary>
		/// This method is used to save the data in database with the help of ProPrivilegesEntry procedure.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			if(DropUserID.SelectedIndex==0)
			{
				MessageBox.Show("Please select the User Name");
				return;
			}
			
            List<PriviligesModel> priviliges = new List<PriviligesModel>();
			int Modules=6;
			int a=0;
			/* Coment by vikas 29.12.2012 
			int[] SubModules={4,5,12,13,52,3};
			CheckBox[] ChkView={chkView1, chkView2, chkView3, chkView4, chkView5, chkView6, chkView7, chkView8, chkView9, chkView10, chkView11, chkView12, chkView13, chkView14, chkView15, chkView16, chkView17, chkView18, chkView66, chkView67, chkView68, chkView19, chkView20, chkView21, chkView22, chkView23, chkView24, chkView25, chkView26, chkView27,chkView44,chkView64,chkView69,chkView70, chkView28, chkView29, chkView30, chkView31, chkView32, chkView33, chkView34, chkView35, chkView36, chkView37, chkView38, chkView39,chkView40,chkView41,chkView42,chkView43,chkView45,chkView46,chkView47,chkView48,chkView49,chkView50,chkView54,chkView55,chkView56,chkView57,chkView58,chkView59,chkView60,chkView61,chkView62,chkView63,chkView65,chkView71,chkView72,chkView73,chkView74,chkView76,chkView77,chkView78,chkView79,chkView80,chkView81,chkView82,chkView83,chkView84,chkView85,chkView86,chkView87,chkView88,chkView89,chkView90,chkView51,chkView52,chkView53};
			CheckBox[] ChkAdd={chkAdd1, chkAdd2, chkAdd3, chkAdd4, chkAdd5, chkAdd6, chkAdd7, chkAdd8, chkAdd9, chkAdd10, chkAdd11, chkAdd12, chkAdd13, chkAdd14, chkAdd15, chkAdd16, chkAdd17, chkAdd18, chkAdd66, chkAdd67, chkAdd68, chkAdd19, chkAdd20, chkAdd21, chkAdd22, chkAdd23, chkAdd24, chkAdd25, chkAdd26, chkAdd27,chkAdd44,chkAdd64,chkAdd69,chkAdd70, chkAdd28, chkAdd29, chkAdd30, chkAdd31, chkAdd32, chkAdd33, chkAdd34, chkAdd35, chkAdd36, chkAdd37, chkAdd38, chkAdd39,chkAdd40, chkAdd41,chkAdd42,chkAdd43,chkAdd45,chkAdd46,chkAdd47,chkAdd48,chkAdd49,chkAdd50,chkAdd54,chkAdd55,chkAdd56,chkAdd57,chkAdd58,chkAdd59,chkAdd60,chkAdd61,chkAdd62,chkAdd63,chkAdd65,chkAdd71,chkAdd72,chkAdd73,chkAdd74,chkAdd76,chkAdd77,chkAdd78,chkAdd79,chkAdd80,chkAdd81,chkAdd82,chkAdd83,chkAdd84,chkAdd85,chkAdd86,chkAdd87,chkAdd88,chkAdd89,chkAdd90,chkAdd51,chkAdd52,chkAdd53};
			CheckBox[] ChkEdit={chkEdit1, chkEdit2, chkEdit3, chkEdit4, chkEdit5, chkEdit6, chkEdit7, chkEdit8, chkEdit9, chkEdit10, chkEdit11, chkEdit12, chkEdit13, chkEdit14, chkEdit15, chkEdit16, chkEdit17, chkEdit18, chkEdit66, chkEdit67, chkEdit68, chkEdit19, chkEdit20, chkEdit21, chkEdit22, chkEdit23, chkEdit24, chkEdit25, chkEdit26, chkEdit27,chkEdit44,chkEdit64,chkEdit69,chkEdit70, chkEdit28, chkEdit29, chkEdit30, chkEdit31, chkEdit32, chkEdit33, chkEdit34, chkEdit35, chkEdit36, chkEdit37, chkEdit38, chkEdit39,chkEdit40, chkEdit41,chkEdit42,chkEdit43,chkEdit45,chkEdit46,chkEdit47,chkEdit48,chkEdit49,chkEdit50,chkEdit54,chkEdit55,chkEdit56,chkEdit57,chkEdit58,chkEdit59,chkEdit60,chkEdit61,chkEdit62,chkEdit63,chkEdit65,chkEdit71,chkEdit72,chkEdit73,chkEdit74,chkEdit76,chkEdit77,chkEdit78,chkEdit79,chkEdit80,chkEdit81,chkEdit82,chkEdit83,chkEdit84,chkEdit85,chkEdit86,chkEdit87,chkEdit88,chkEdit89,chkEdit90,chkEdit51,chkEdit52,chkEdit53};
			CheckBox[] ChkDel={chkDel1, chkDel2, chkDel3, chkDel4, chkDel5, chkDel6, chkDel7, chkDel8, chkDel9, chkDel10, chkDel11, chkDel12, chkDel13, chkDel14, chkDel15, chkDel16, chkDel17, chkDel18, chkDel66, chkDel67, chkDel68, chkDel19, chkDel20, chkDel21, chkDel22, chkDel23, chkDel24, chkDel25, chkDel26, chkDel27,chkDel44,chkDel64,chkDel69,chkDel70, chkDel28, chkDel29, chkDel30, chkDel31, chkDel32, chkDel33, chkDel34, chkDel35, chkDel36, chkDel37, chkDel38, chkDel39,chkDel40, chkDel41,chkDel42,chkDel43,chkDel45,chkDel46,chkDel47,chkDel48,chkDel49,chkDel50,chkDel54,chkDel55,chkDel56,chkDel57,chkDel58,chkDel59,chkDel60,chkDel61,chkDel62,chkDel63,chkDel65,chkDel71,chkDel72,chkDel73,chkDel74,chkDel76,chkDel77,chkDel78,chkDel79,chkDel80,chkDel81,chkDel82,chkDel83,chkDel84,chkDel85,chkDel86,chkDel87,chkDel88,chkDel89,chkDel90,chkDel51,chkDel52,chkDel53};*/

			int[] SubModules={4,5,12,13,61,3};
			CheckBox[] ChkView={chkView1, chkView2, chkView3, chkView4, chkView5, chkView6, chkView7, chkView8, chkView9, chkView10, chkView11, chkView12, chkView13, chkView14, chkView15, chkView16, chkView17, chkView18, chkView66, chkView67, chkView68, chkView19, chkView20, chkView21, chkView22, chkView23, chkView24, chkView25, chkView26, chkView27,chkView44,chkView64,chkView69,chkView70, chkView28, chkView29, chkView30, chkView31, chkView32, chkView33, chkView34, chkView35, chkView36, chkView37, chkView38, chkView39,chkView40,chkView41,chkView42,chkView43,chkView45,chkView46,chkView47,chkView48,chkView49,chkView50,chkView54,chkView55,chkView56,chkView57,chkView58,chkView59,chkView60,chkView61,chkView62,chkView63,chkView65,chkView71,chkView72,chkView73,chkView74,chkView76,chkView77,chkView78,chkView79,chkView80,chkView81,chkView82,chkView83,chkView84,chkView85,chkView86,chkView87,chkView88,chkView89,chkView90,chkView91,chkView92,chkView93,chkView94,chkView95,chkView96,chkView97,chkView98,chkView99,chkView51,chkView52,chkView53};
			CheckBox[] ChkAdd={chkAdd1, chkAdd2, chkAdd3, chkAdd4, chkAdd5, chkAdd6, chkAdd7, chkAdd8, chkAdd9, chkAdd10, chkAdd11, chkAdd12, chkAdd13, chkAdd14, chkAdd15, chkAdd16, chkAdd17, chkAdd18, chkAdd66, chkAdd67, chkAdd68, chkAdd19, chkAdd20, chkAdd21, chkAdd22, chkAdd23, chkAdd24, chkAdd25, chkAdd26, chkAdd27,chkAdd44,chkAdd64,chkAdd69,chkAdd70, chkAdd28, chkAdd29, chkAdd30, chkAdd31, chkAdd32, chkAdd33, chkAdd34, chkAdd35, chkAdd36, chkAdd37, chkAdd38, chkAdd39,chkAdd40, chkAdd41,chkAdd42,chkAdd43,chkAdd45,chkAdd46,chkAdd47,chkAdd48,chkAdd49,chkAdd50,chkAdd54,chkAdd55,chkAdd56,chkAdd57,chkAdd58,chkAdd59,chkAdd60,chkAdd61,chkAdd62,chkAdd63,chkAdd65,chkAdd71,chkAdd72,chkAdd73,chkAdd74,chkAdd76,chkAdd77,chkAdd78,chkAdd79,chkAdd80,chkAdd81,chkAdd82,chkAdd83,chkAdd84,chkAdd85,chkAdd86,chkAdd87,chkAdd88,chkAdd89,chkAdd90,chkAdd91,chkAdd92,chkAdd93,chkAdd94,chkAdd95,chkAdd96,chkAdd97,chkAdd98,chkAdd99,chkAdd51,chkAdd52,chkAdd53};
			CheckBox[] ChkEdit={chkEdit1, chkEdit2, chkEdit3, chkEdit4, chkEdit5, chkEdit6, chkEdit7, chkEdit8, chkEdit9, chkEdit10, chkEdit11, chkEdit12, chkEdit13, chkEdit14, chkEdit15, chkEdit16, chkEdit17, chkEdit18, chkEdit66, chkEdit67, chkEdit68, chkEdit19, chkEdit20, chkEdit21, chkEdit22, chkEdit23, chkEdit24, chkEdit25, chkEdit26, chkEdit27,chkEdit44,chkEdit64,chkEdit69,chkEdit70, chkEdit28, chkEdit29, chkEdit30, chkEdit31, chkEdit32, chkEdit33, chkEdit34, chkEdit35, chkEdit36, chkEdit37, chkEdit38, chkEdit39,chkEdit40, chkEdit41,chkEdit42,chkEdit43,chkEdit45,chkEdit46,chkEdit47,chkEdit48,chkEdit49,chkEdit50,chkEdit54,chkEdit55,chkEdit56,chkEdit57,chkEdit58,chkEdit59,chkEdit60,chkEdit61,chkEdit62,chkEdit63,chkEdit65,chkEdit71,chkEdit72,chkEdit73,chkEdit74,chkEdit76,chkEdit77,chkEdit78,chkEdit79,chkEdit80,chkEdit81,chkEdit82,chkEdit83,chkEdit84,chkEdit85,chkEdit86,chkEdit87,chkEdit88,chkEdit89,chkEdit90,chkEdit91,chkEdit92,chkEdit93,chkEdit94,chkEdit95,chkEdit96,chkEdit97,chkEdit98,chkEdit99,chkEdit51,chkEdit52,chkEdit53};
			CheckBox[] ChkDel={chkDel1, chkDel2, chkDel3, chkDel4, chkDel5, chkDel6, chkDel7, chkDel8, chkDel9, chkDel10, chkDel11, chkDel12, chkDel13, chkDel14, chkDel15, chkDel16, chkDel17, chkDel18, chkDel66, chkDel67, chkDel68, chkDel19, chkDel20, chkDel21, chkDel22, chkDel23, chkDel24, chkDel25, chkDel26, chkDel27,chkDel44,chkDel64,chkDel69,chkDel70, chkDel28, chkDel29, chkDel30, chkDel31, chkDel32, chkDel33, chkDel34, chkDel35, chkDel36, chkDel37, chkDel38, chkDel39,chkDel40, chkDel41,chkDel42,chkDel43,chkDel45,chkDel46,chkDel47,chkDel48,chkDel49,chkDel50,chkDel54,chkDel55,chkDel56,chkDel57,chkDel58,chkDel59,chkDel60,chkDel61,chkDel62,chkDel63,chkDel65,chkDel71,chkDel72,chkDel73,chkDel74,chkDel76,chkDel77,chkDel78,chkDel79,chkDel80,chkDel81,chkDel82,chkDel83,chkDel84,chkDel85,chkDel86,chkDel87,chkDel88,chkDel89,chkDel90,chkDel91,chkDel92,chkDel93,chkDel94,chkDel95,chkDel96,chkDel97,chkDel98,chkDel99,chkDel51,chkDel52,chkDel53};

			try
			{
				for(int i=0;i<Modules;i++)
				{
					for(int j=0;j<SubModules[i];j++)
					{
                        PriviligesModel privilige = new PriviligesModel();
                       
                        privilige.Login_Name=DropUserID.SelectedItem.Value;
                        privilige.Module_ID=System.Convert.ToString(i+1);
                        privilige.SubModule_ID=System.Convert.ToString(j+1);
						
						if(ChkView[a].Checked)
                            privilige.ViewFlag="1";
						else
                            privilige.ViewFlag="0";
						if(ChkAdd[a].Checked)
                            privilige.Add_Flag="1";
						else
                            privilige.Add_Flag="0";
						if(ChkEdit[a].Checked)
                            privilige.Edit_Flag="1";
						else
                            privilige.Edit_Flag="0";
						if(ChkDel[a].Checked)
                            privilige.Del_Flag="1";
						else
                            privilige.Del_Flag="0";
                        //call the procedure ProPrivilegesEntry to save the data in Privileges table before take the 
                        //user id from user master table according to passing the user name in procedure after that
                        //check this privileges ID exist or not if exist then delete the data of this priovileges id 
                        //after that insert the data in privileges table otherwise direct insert the data in privileges
                        //table
                        priviliges.Add(privilige);
						a++;
					}
				}
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    var myContent = JsonConvert.SerializeObject(priviliges);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("api/PriviligesController/InsertPriviliges", byteContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                    }
                }

                CreateLogFiles.ErrorLog("Form:Privileges.aspx,Method:btnSave_Click  Privilegs of User  "+ uid + " Updated.  "+uid  );
				MessageBox.Show("Privileges Allocated");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Privileges.aspx,Method:btnSave_Click   EXCEPTION:  "+ex.Message+" userid  "+uid  );
			}
		}

		/// <summary>
		/// if the Account check box is checked then display the coressponding panel and checks all the check box.
		/// </summary>
		protected void chkAccount_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkAccount.Checked)
			{
				PanelAcc.Visible=true; 
				foreach(Control ctl in PanelAcc.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=true;
					}
				}
			}
			else
			{
				PanelAcc.Visible=false;
				foreach(Control ctl in PanelAcc.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=false;
					}
				}
			}
		}

		/// <summary>
		/// if the employee check box is checked then display the coressponding panel and checks all the check box.
		/// </summary>
		protected void chkEmployee_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkEmployee.Checked)
			{
				PanelEmp.Visible=true;
				foreach(Control ctl in PanelEmp.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=true;
					}
				}
			}
			else
			{
				PanelEmp.Visible=false;
				foreach(Control ctl in PanelEmp.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=false;
					}
				}
			}
		}

		/// <summary>
		/// if the Master check box is checked then display the coressponding panel and checks all the check box.
		/// </summary>
		protected void chkMaster_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkMaster.Checked)
			{
				PanelMaster.Visible=true;
				foreach(Control ctl in PanelMaster.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=true;
					}
				}
				
			}
			else
			{
				PanelMaster.Visible=false;
				foreach(Control ctl in PanelMaster.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=false;
					}
				}
			}
		}

		/// <summary>
		/// if the Inventory check box is checked then display the coressponding panel and checks all the check box.
		/// </summary>
		protected void chkInventory_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkInventory.Checked)
			{
				PanelInventory.Visible=true;
				foreach(Control ctl in PanelInventory.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=true;
					}
				}
			}
			else
			{
				PanelInventory.Visible=false;
				foreach(Control ctl in PanelInventory.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=false;
					}
				}
			}
		}

			
		/// <summary>
		/// if the admin check box is checked then display the coressponding panel and checks all the check box.
		/// </summary>
		protected void chkAdmin_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkAdmin.Checked)
			{
				PanelAdmin.Visible=true;
				foreach(Control ctl in PanelAdmin.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=true;
					}
				}
			}
			else
			{
				PanelAdmin.Visible=false;
				foreach(Control ctl in PanelAdmin.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=false;
					}
				}
			}
		}

		protected void chkView11_CheckedChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// if the logistics check box is checked then display the coressponding panel and checks all the check box.
		/// </summary>
		protected void checkLogistics_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkLogistics.Checked)
			{
				panelLogistics.Visible=true;
				foreach(Control ctl in panelLogistics.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=true;
					}
				}
			}
			else
			{
				panelLogistics.Visible=false;
				foreach(Control ctl in panelLogistics.Controls)
				{
					if(ctl is CheckBox)
					{
						((CheckBox)ctl).Checked=false;
					}
				}
			}
		}
	}
}