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
using System.Data.SqlClient ;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes; 
using RMG;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for SalespesonAssignment.
	/// </summary>
	public partial class SalespesonAssignment : System.Web.UI.Page
	{
		string uid="";

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
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : Page_Load"+ ex.Message+"  EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			try
			{
				if(!Page.IsPostBack)
				{
					FillList();
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="10";
					string[,] Priv=(string[,]) Session["Privileges"];
					for(i=0;i<Priv.GetLength(0);i++)
					{
						if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
						{						
							View_Flag=Priv[i,2];
							Add_Flag=Priv[i,3];
							Edit_Flag=Priv[i,4];
							Del_Flag=Priv[i,5];
							break;
						}
					}	
					Cache["Add"]=Add_Flag;
					Cache["View"]=View_Flag;
					Cache["Edit"]=Edit_Flag;
					Cache["Del"]=Del_Flag;
					if(View_Flag=="0")
					{
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					}
					if(Add_Flag=="0")
						btnSubmit.Enabled=false;
					//					if(Edit_Flag=="0")
					//						btnEdit.Enabled=false;
					//					if(Del_Flag=="0")
					//						btnDelete.Enabled=false;
					#endregion
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : Page_Load"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the ssr information from employee table and also fatch the 
		/// city from beat Master table and fill into the dropdownlist.
		/// </summary>
		public void FillList()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader SqlDtr;
				string sql="select emp_name from employee where Designation='Servo Sales Representative' order by emp_name";
				SqlDtr = obj.GetRecordSet (sql);
				DropSSRname.Items.Clear();
				DropSSRname.Items.Add("Select");;
				while(SqlDtr.Read ())
				{
					DropSSRname.Items.Add(SqlDtr.GetValue(0).ToString ());
				}
				SqlDtr.Close();

				sql="select city from beat_master order by city";
				SqlDtr = obj.GetRecordSet (sql);
				Listbeat.Items.Clear();
				while(SqlDtr.Read ())
				{
					Listbeat.Items.Add(SqlDtr.GetValue(0).ToString ());
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : FillList()"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}
		
		/// <summary>
		/// This method is used to fatch the city from Beat Master table and fill the information 
		/// in the dropdownlist.
		/// </summary>
		public void fillbeat()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader SqlDtr;
			
				string sql="select city from beat_master order by city";
				SqlDtr = obj.GetRecordSet (sql);
				Listbeat.Items.Clear();
				while(SqlDtr.Read ())
				{
					Listbeat.Items.Add(SqlDtr.GetValue(0).ToString ());
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : FillBeat()"+ ex.Message+"  EXCEPTION  "+uid);
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
		/// This method is used to move the city from one list to anather list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnIn_Click(object sender, System.EventArgs e)
		{
			try
			{
				while(Listbeat.SelectedItem.Selected)
				{
					Listassibeat.Items.Add(Listbeat.SelectedItem.Value);  
					Listbeat.Items.Remove(Listbeat.SelectedItem.Value);
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : btnIn_Click"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}

		/// <summary>
		/// This method is used to move the city from one list to anather list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnout_Click(object sender, System.EventArgs e)
		{
			try
			{
				while(Listassibeat.SelectedItem.Selected)
				{
					Listbeat.Items.Add(Listassibeat.SelectedItem.Value);
					Listassibeat.Items.Remove(Listassibeat.SelectedItem.Value);  
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : btnout_Click"+ ex.Message+"  EXCEPTION  "+uid);
			}	
		}

		/// <summary>
		/// This method is used to move all the city from one list to anather list or vice versa.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btn1_Click(object sender, System.EventArgs e)
		{
			if(btn1.Text.Trim().Equals(">>"))
			{
				try
				{
					btn1.Text="<<";
					foreach(System.Web.UI.WebControls.ListItem lst in Listbeat.Items)
						Listassibeat.Items.Add(lst);
					Listbeat.Items.Clear();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method: btn1_Click"+ ex.Message);
				}
			}
			else
			{
				try
				{
					btn1.Text=">>";
					foreach(System.Web.UI.WebControls.ListItem lst in Listassibeat.Items)
						Listbeat.Items.Add(lst);
					Listassibeat.Items.Clear();
					
				}
				catch(Exception ex)
				{	
					CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method: btn1_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
				}
			}
		}

		/// <summary>
		/// This method is used to save the beat information in SSR_Beat table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
				SqlConnection cn=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlCommand cmd;
				cn.Open();
				InventoryClass obj=new InventoryClass();
				InventoryClass obj1=new InventoryClass();
				SqlDataReader sdtr=null;
				int emp_id=0;
				string sql="Select emp_id from employee where emp_name='"+DropSSRname.SelectedItem.Value.ToString()+"'";
				sdtr=obj.GetRecordSet(sql);
				if(sdtr.Read())
				{
					emp_id=Convert.ToInt32(sdtr.GetValue(0).ToString());
				
				}
				sdtr.Close();
				sql="delete from ssr_beat where ssr_id="+emp_id;
				cmd=new SqlCommand(sql,cn);
				cmd.ExecuteNonQuery();
				for(int i=0;i<Listassibeat.Items.Count;i++)
				{
					int beat_id=0;
					string beat_name=Listassibeat.Items[i].Value;
					sql="Select Beat_no from beat_master where city='"+beat_name.ToString().Trim()+"'";
					sdtr=obj.GetRecordSet(sql);
					if(sdtr.Read())
					{
						beat_id=Convert.ToInt32(sdtr.GetValue(0).ToString());
				
					}
					sdtr.Close();
					sql="Insert into SSR_Beat(SSR_ID,Beat_Id) values(@SSR,@Beat)";
					cmd=new SqlCommand(sql,cn);
					cmd.Parameters.Add("@SSR",emp_id);
					cmd.Parameters.Add("@Beat",beat_id);
					cmd.ExecuteNonQuery();
				}
				MessageBox.Show("Record Save Succesfully");
				DropSSRname.SelectedIndex=0;
				Listassibeat.Items.Clear();
				FillList();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx, Method : btnSubmit_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}

		/// <summary>
		/// This method is used to fatch the all beat according to select SSR from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropSSRname_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				fillbeat();
				InventoryClass obj=new InventoryClass();
				InventoryClass obj1=new InventoryClass();
				SqlDataReader sdr=null,sdr1=null;
				int ssr_id=0;
				string sql="Select emp_id from employee where emp_name='"+DropSSRname.SelectedItem.Value+"'";
				sdr=obj.GetRecordSet(sql);
				if(sdr.Read())
				{
					ssr_id=Convert.ToInt32(sdr.GetValue(0));
				}
				sdr.Close();

				sql="Select beat_id from ssr_beat where ssr_id="+ssr_id;
				sdr=obj.GetRecordSet(sql);
				Listassibeat.Items.Clear();
				while(sdr.Read())
				{
					sql="Select city from beat_master where beat_no="+sdr.GetValue(0);
					sdr1=obj1.GetRecordSet(sql);
					if(sdr1.Read())
					{
						Listassibeat.Items.Add(sdr1.GetValue(0).ToString().Trim());
						Listbeat.Items.Remove(sdr1.GetValue(0).ToString().Trim());
					}
					sdr1.Close();
				}
				sdr.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SelesPersionAssignment.aspx,Method : DropSSRname_SelectedIndexChanged"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}
	}
}