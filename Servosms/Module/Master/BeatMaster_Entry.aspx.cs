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

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for BeatMaster_Entry.
	/// </summary>
	public partial class BeatMaster_Entry : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
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
				PartiesClass obj=new PartiesClass ();
				SqlDataReader SqlDtr;
				SqlDtr = obj.GetRecordSet ("select max(Beat_No)+1 from Beat_Master");
				while(SqlDtr.Read ())
				{
					lblBeatNo.Text =SqlDtr.GetValue(0).ToString ();
					if(lblBeatNo.Text =="")
					{
						lblBeatNo.Text ="1001";
					}
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:FillID().  EXCEPTION "+ ex.Message+"  "+uid);
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
				PartiesClass  obj=new PartiesClass  ();
				SqlDataReader SqlDtr;
				SqlDtr = obj.GetRecordSet("select Beat_No,city from Beat_Master order by city");
				DropBeatNo.Items.Clear();
				DropBeatNo.Items.Add("Select");
				while(SqlDtr.Read ())
				{
					DropBeatNo.Items.Add(SqlDtr.GetValue(0).ToString ()+':'+SqlDtr.GetValue(1).ToString ());
				}	
				SqlDtr.Close(); 
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:btnEdit_Click().  EXCEPTION "+ ex.Message+"  "+uid);
			}
		}
		/// <summary>
		/// This is to Save the beat information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			PartiesClass  obj = new PartiesClass ();
			try 
			{
				SqlDataReader SqlDtr; 
				string sql;
				int flag=0;
				sql="select City  from Beat_Master where City='"+ txtCity.Text  +"'";
				//sql="select City  from Beat_Master where City='"+ arr[0] +"'";
		
				SqlDtr=obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{
					flag=1;		
				}
				else if(DropBeatNo.Visible==false)
				{
					obj.City =StringUtil.FirstCharUpper(txtCity.Text.ToString());  
					obj.State= StringUtil.FirstCharUpper(txtState.Text.ToString());
					obj.Country=StringUtil.FirstCharUpper(txtCountry.Text.ToString()); 
					obj.Beat_No = lblBeatNo.Text;
					obj.InsertBeatMaster();	
					CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method: btnSave_Click"+"  Beatno  "+obj.Beat_No +" city  "+obj.City    +"   state  "+ obj.State   +" Country"+obj.Country+ " IS SAVED  "+" userid  "+ uid);
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
					SqlDtr.Close();
				}
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method: btnSave_Click"+"  Beatno  "+obj.Beat_No +" city  "+obj.City    +"   state  "+ obj.State   +" Country"+obj.Country+ ex.Message+" userid  "+ uid);
			}
		}
		
		/// <summary>
		/// This is to delete the particular beat from the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			PartiesClass  obj=new PartiesClass  (); 
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
					//obj.Beat_No = DropBeatNo.SelectedItem.Value ;  
					obj.Beat_No = Beat[0];  
					obj.DeleteBeatMaster();
					MessageBox.Show("Beat deleted");
					CreateLogFiles.ErrorLog("Form:BeatMasterEntry.aspx,Method: btnDelete_Click"+"  Beat no  "+obj.Beat_No+"  is DELETED  " +"  user id  "+uid);
			
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
				PartiesClass obj=new PartiesClass();
				SqlDataReader SqlDtr;
				string sql;
				string cty=DropBeatNo.SelectedItem.Value;
				string[] arr=cty.Split(new char[]{':'},cty.Length); 
				//sql="Select * from Beat_Master where Beat_No='"+ DropBeatNo.SelectedItem.Value  +"'";
				sql="Select * from Beat_Master where Beat_No='"+ arr[0]  +"'";
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					txtCity.Text=SqlDtr.GetValue(1).ToString();
					txtState.Text=SqlDtr.GetValue(2).ToString();
					txtCountry.Text=SqlDtr.GetValue(3).ToString();
				}
				SqlDtr.Close();
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:DropBeatNo_SelectedIndexChanged"+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx,Method:DropBeatNo_SelectedIndexChange"+"  EXCEPTION "+ ex.Message+uid);
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
			PartiesClass  obj1 = new PartiesClass ();
			try
			{	
				PartiesClass  obj = new PartiesClass ();
				obj1.City = txtCity.Text;  
				obj1.State = txtState.Text;
				obj1.Country=txtCountry.Text; 
				string cty=DropBeatNo.SelectedItem.Value;
				string[] arr=cty.Split(new char[]{':'},cty.Length); 
				//obj1.Beat_No =DropBeatNo.SelectedItem.Value ; 
				obj1.Beat_No =arr[0] ; 
				obj1.UpdateBeatMaster();
				MessageBox.Show("Beat Updated");
				Clear();
				DropBeatNo.Visible=false;
				lblBeatNo.Visible=true;
				Edit1.Visible=false;
				btnEdit.Visible=true; 
				checkPrevileges();
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx ,method Edit1_Click,"+"  Beat no   "+obj1.Beat_No +"City Updated to   "+obj1.City+"  user:"+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BeatMasterEntery.aspx ,method Edit1_Click,"+"  Beat no   "+obj1.Beat_No +"City Updated to   "+obj1.City+ "  EXCEPTION  "+ex.Message+"  user:"+uid);
			}
		}
	}
}