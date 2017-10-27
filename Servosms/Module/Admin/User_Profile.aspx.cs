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
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using DBOperations;
using RMG;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Servosms.Module.Admin
{
	/// <summary>
	/// Summary description for User_Profile.
	/// </summary>
	public partial class User_Profile : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
        string BaseUri = "http://localhost:64862";
        /// <summary>
        /// This method is used for setting the Session variable for userId and 
        /// after that filling the required dropdowns with database values 
        /// and also check accessing priviledges for particular user
        /// and generate the next ID also.
        /// </summary>
        protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{ 
				uid=(Session["User_Name"].ToString ());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:Page_load  EXCEPTION: "+ ex.Message+"    "+uid);
				Response.Redirect("ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				try
				{
					#region Check Privileges, if the user is admin then grant the access
					if(Session["User_ID"].ToString ()!="1001")
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					#endregion
                    #region Fetch Roles from Database and Add in the ComboBox
                    List<string> roles = new List<string>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/userprofile/GetRoles").Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var disc = Res.Content.ReadAsStringAsync().Result;
                            roles = JsonConvert.DeserializeObject<List<string>>(disc);
                        }
                    }
					if(roles!=null && roles.Count>0)
					{
                        foreach (var role in roles)
                            DropRole.Items.Add(role);
					}
					
					#endregion

					GetNextUserID();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:Page_load EXCEPTION: "+ ex.Message+"    "+uid);
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
		/// This method is used to Clear all information in the form.
		/// </summary>
		public void Clear()
		{
			lblUserID.Text="";
			txtLoginName.Text="";
			txtPassword.Text="";
			txtFName.Text="";
			txtMName.Text="";
			txtLName.Text="";
			DropRole.SelectedIndex=0;
		}

		/// <summary>
		/// This method is used to generate the next user id
		/// </summary>
		public void GetNextUserID()
		{
            try
            {
                #region Fetch Next User ID
                string userID = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/userprofile/GetNextUserID").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        userID = JsonConvert.DeserializeObject<string>(disc);
                    }
                    lblUserID.Text = userID;
                }
                #endregion
            }
            catch (Exception ex)
            {
                CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:GetNextUserID  EXCEPTION: " + ex.Message + "    " + uid);
            }
		}

		/// <summary>
		/// This method is used to save or update the user details in user master table with the help of 
		/// ProUserMasterUpdate and ProUserMasterEntry stored procedure.
		/// </summary>
		protected void btnUpdate_Click(object sender, System.EventArgs e)
		{
            UserModel user = new UserModel();
			int x=0;
            string UserId = null;

            try
			{
                #region check the username is exist or not?
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/userprofile/CheckUser?loginName="+ txtLoginName.Text.Trim()).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        x = JsonConvert.DeserializeObject<int>(disc);
                    }
                }
				if(x>0)
				{
					if(dropUserID.Visible)
					{
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/userprofile/GetUserID?loginName=" + txtLoginName.Text.Trim()).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var disc = Res.Content.ReadAsStringAsync().Result;
                                UserId = JsonConvert.DeserializeObject<string>(disc);
                            }
                        }
                        if (!dropUserID.SelectedItem.Text.Trim().Equals(UserId))
						{
							RMG.MessageBox.Show("User " +txtLoginName.Text +" already exists");
							return;
						}
					}
					else
					{
						RMG.MessageBox.Show("User " +txtLoginName.Text +" already exists");
						return;
					}
				}
				#endregion
				string Name="";
				Name = txtFName.Text;
				Name += " "+txtMName.Text;
				Name += " "+txtLName.Text;
				Name = Name.Trim();
                user.LoginName=txtLoginName.Text.ToString();

                user.Password = Encrypt(txtPassword.Text.ToString(),"!@#$%^");

                //obj.User_Name=txtFName.Text.ToString()+" "+txtMName.Text.ToString()+" "+txtLName.Text.ToString();
                user.UserName = Name;
                user.RoleName=DropRole.SelectedItem.Value; 
				if(dropUserID.Visible)
				{
                    user.UserID = dropUserID.SelectedItem.Value;
                    //call the procedure ProUserMasterUpdate to update the required user details in user master table.
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(user);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/userprofile/UpdateUser", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(responseString);
                        }
                    }

					CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:brnUpdate_Click.  User  ID"+ user.UserID +" Updated.  User: "+uid);
					MessageBox.Show("User Profile Updated");
				}
				else
				{
                    user.UserID = lblUserID.Text.ToString();
                    //call the procedure ProUserMasterEntry to insert the user details in user master table.

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        var myContent = JsonConvert.SerializeObject(user);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var response = client.PostAsync("api/userprofile/InsertUser", byteContent).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            string responseString = response.Content.ReadAsStringAsync().Result;
                            var prod = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(responseString);
                        }
                    }
					CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:brnUpdate_Click. New User With ID "+ user.UserID +" Created.  User: "+uid);
					MessageBox.Show("User Profile Created");
				}
				
				Clear();
				GetNextUserID();
				lblUserID.Visible=true;
				btnEdit.Visible=true;
				dropUserID.Visible=false;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:Page_load  EXCEPTION: "+ ex.Message+"    "+uid);
			}
		}

		/// <summary>
		/// Function to encrypt the user password before saving to the database.
		/// </summary>
		public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV) 
		{ 
			// Create a MemoryStream to accept the encrypted bytes 
			MemoryStream ms = new MemoryStream(); 

			// Create a symmetric algorithm. 
			// We are going to use Rijndael because it is strong and
			// available on all platforms. 
			// You can use other algorithms, to do so substitute the
			// next line with something like 
			//      TripleDES alg = TripleDES.Create(); 

			Rijndael alg = Rijndael.Create(); 

			// Now set the key and the IV. 
			// We need the IV (Initialization Vector) because
			// the algorithm is operating in its default 
			// mode called CBC (Cipher Block Chaining).
			// The IV is XORed with the first block (8 byte) 
			// of the data before it is encrypted, and then each
			// encrypted block is XORed with the 
			// following block of plaintext.
			// This is done to make encryption more secure. 

			// There is also a mode called ECB which does not need an IV,
			// but it is much less secure. 
			alg.Key = Key; 
			alg.IV = IV; 

			// Create a CryptoStream through which we are going to be
			// pumping our data. 
			// CryptoStreamMode.Write means that we are going to be
			// writing data to the stream and the output will be written
			// in the MemoryStream we have provided. 
			CryptoStream cs = new CryptoStream(ms, 
				alg.CreateEncryptor(), CryptoStreamMode.Write); 

			// Write the data and make it do the encryption 
			cs.Write(clearData, 0, clearData.Length); 

			// Close the crypto stream (or do FlushFinalBlock). 
			// This will tell it that we have done our encryption and
			// there is no more data coming in, 
			// and it is now a good time to apply the padding and
			// finalize the encryption process. 
			cs.Close(); 

			// Now get the encrypted data from the MemoryStream.
			// Some people make a mistake of using GetBuffer() here,
			// which is not the right way. 
			byte[] encryptedData = ms.ToArray();

			return encryptedData; 
		} 

		/// <summary>
		/// Encrypt a string into a string using a password 
		/// Uses Encrypt(byte[], byte[], byte[]) 
		/// </summary>
		/// <param name="clearText"></param>
		/// <param name="Password"></param>
		/// <returns></returns>
		public static string Encrypt(string clearText, string Password) 
		{ 
			// First we need to turn the input string into a byte array. 
			byte[] clearBytes = 
				System.Text.Encoding.Unicode.GetBytes(clearText); 

			// Then, we need to turn the password into Key and IV 
			// We are using salt to make it harder to guess our key
			// using a dictionary attack - 
			// trying to guess a password by enumerating all possible words. 
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, 
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76}); 

			// Now get the key/IV and do the encryption using the
			// function that accepts byte arrays. 
			// Using PasswordDeriveBytes object we are first getting
			// 32 bytes for the Key 
			// (the default Rijndael key length is 256bit = 32bytes)
			// and then 16 bytes for the IV. 
			// IV should always be the block size, which is by default
			// 16 bytes (128 bit) for Rijndael. 
			// If you are using DES/TripleDES/RC2 the block size is
			// 8 bytes and so should be the IV size. 
			// You can also read KeySize/BlockSize properties off
			// the algorithm to find out the sizes. 
			byte[] encryptedData = Encrypt(clearBytes, 
				pdb.GetBytes(32), pdb.GetBytes(16)); 

			// Now we need to turn the resulting byte array into a string. 
			// A common mistake would be to use an Encoding class for that.
			//It does not work because not all byte values can be
			// represented by characters. 
			// We are going to be using Base64 encoding that is designed
			//exactly for what we are trying to do. 
			return Convert.ToBase64String(encryptedData); 

		}
		//		private byte[] Encrypt(string pswd)
		//		{
		//			RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
		//			byte[]key=System.Text.ASCIIEncoding.ASCII.GetBytes("shashank");
		//			byte[]IV=System.Text.ASCIIEncoding.ASCII.GetBytes("shashank");
		//			byte[]data=System.Text.ASCIIEncoding.ASCII.GetBytes(pswd);
		//			MemoryStream msEncrypt = new MemoryStream();
		//			//CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
		//			CryptoStream csEncrypt = new CryptoStream(msEncrypt, rc2CSP.CreateEncryptor(key,IV), CryptoStreamMode.Write);			
		//			csEncrypt.Write(data,0,data.Length);
		//			csEncrypt.FlushFinalBlock();
		//			return msEncrypt.ToArray();
		//			//txtres.Text=System.Text.ASCIIEncoding.ASCII.GetString(msEncrypt.ToArray());
		//		}

		/// <summary>
		/// This method is used to fatch the all user ID from user master table and fill the dropdownlist on edit time.
		/// </summary>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			lblUserID.Visible=false;
			btnEdit.Visible=false;
			dropUserID.Visible=true;

            #region	Fetch All User ID
            try
			{
				dropUserID.Items.Clear();
				dropUserID.Items.Add("Select");
                List<string> users = new List<string>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/userprofile/GetUsers").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        users = JsonConvert.DeserializeObject<List<string>>(disc);
                    }
                }

                if (users != null && users.Count > 0)
                {
                    foreach (var user in users)
                        dropUserID.Items.Add(user);
                }
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:btnEdit_Click EXCEPTION: "+ ex.Message+"  " +uid);
			}
			#endregion
		}

		/// <summary>
		/// This method is used to fatch the record according to select user from dropdownlist on edit time.
		/// </summary>
		protected void dropUserID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				Clear();
                UserModel user = new UserModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/userprofile/GetSelectedUser?userID="+ dropUserID.SelectedItem.Value).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        user = JsonConvert.DeserializeObject<UserModel>(disc);
                    }
                }

                string[] UName=null;
                if (user != null)
                {
                    txtLoginName.Text = user.LoginName;
                    txtPassword.Text = user.Password;
                    DropRole.SelectedIndex = DropRole.Items.IndexOf(DropRole.Items.FindByValue(user.RoleName));

                    if (user.UserName.IndexOf(" ") > 0)
                    {
                        UName = user.UserName.Split(new char[] { ' ' }, user.UserName.Length);
                        if (UName.Length > 2)
                        {
                            txtFName.Text = UName[0].ToString();
                            txtMName.Text = UName[1].ToString();
                            txtLName.Text = UName[2].ToString();
                        }
                        else
                        {
                            txtFName.Text = UName[0].ToString();
                            txtLName.Text = UName[1].ToString();
                        }
                    }
                    else
                        txtFName.Text = user.UserName.Trim();
                }
			}
			catch(Exception ex)
			{
				MessageBox.Show("Please Select User ID");
				CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:dropUserID_SelectedIndexChanged  EXCEPTION: "+ ex.Message+"    "+uid);
			}
		}

		/// <summary>
		/// This method is used to delete the particular record from user master table.
		/// </summary>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			if(dropUserID.SelectedIndex==0)
			{
				RMG.MessageBox.Show("Please select the User ID");
				return;
			}
			try
			{
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/userprofile/DeleteUser?UserID=" + dropUserID.SelectedItem.Value).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var disc = Res.Content.ReadAsStringAsync().Result;
                        var user = JsonConvert.DeserializeObject<UserModel>(disc);
                    }
                }

                dropUserID.Items.Remove(dropUserID.SelectedItem.Value);
				MessageBox.Show("User Deleted");
				Clear();
				GetNextUserID();
				lblUserID.Visible=true;
				btnEdit.Visible=true;
				dropUserID.Visible=false;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:User_Profile.aspx,Method:btnDelete_Click  EXCEPTION: "+ ex.Message+"    "+uid);
			}
		}
	}
}