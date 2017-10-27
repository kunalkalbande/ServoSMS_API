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

namespace Servosms.Module.inventory
{
	/// <summary>
	/// Summary description for Shift_Asignment.
	/// </summary>
	public partial class schemediscountentry : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid="";
		//string shift1="";
		//string emp="";
		//static	int countNo1=0;
		protected System.Web.UI.WebControls.DropDownList dropGroup;
		protected System.Web.UI.WebControls.CompareValidator validgroup;
		protected System.Web.UI.WebControls.Label lblGroup;
		public static string group="";
		//static int countNo2=0;
		
	
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
				CreateLogFiles.ErrorLog("Form:schemediscountEntry.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if (!Page.IsPostBack )
			{
				txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
				txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
				#region Check Privileges
				int i;
				string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="3";
				string SubModule="11";
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
				//Cache["Add"]=Add_Flag;
				if(View_Flag=="0")
				{
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				if(Add_Flag=="0")
					btnSubmit.Enabled=false;
				if(Edit_Flag=="0")
					btschid.Enabled=false;
				//string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				//string Module="2";
				//string SubModule="5";
				//string[,] Priv=(string[,]) Session["Privileges"];
				//for(i=0;i<Priv.GetLength(0);i++)
				//{
				//	if(Priv[i,0]== Module &&  Priv[i,1]==SubModule)
				//	{						
				//		View_flag=Priv[i,2];
				//		Add_Flag=Priv[i,3];
				//		Edit_Flag=Priv[i,4];
				//		Del_Flag=Priv[i,5];
				//	break;
				//	}
				//	}	
				//	if(Add_Flag=="0" && Edit_Flag == "0" && View_flag == "0")
				//	{
				//		//string msg="UnAthourized Visit to Shift Asignment Page";
				//		//dbobj.LogActivity(msg,Session["User_Name"].ToString());  
				//		Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				//	}
				//	if(Add_Flag == "0")
				//		btnSubmit.Enabled = false; 
				#endregion

				try
				{
					// Fecth all the shifts and fill sthe combo
					//EmployeeClass obj=new EmployeeClass ();

					//getGroup();       //Add by Vikas Sharma 23.10.2012
					dropschid.Visible=false;
					btnupdate.Visible=false;
					GetNextschemeID();
					InventoryClass obj= new InventoryClass();
					SqlDataReader SqlDtr1;
					//int x=0;
					//SqlDataReader SqlDtr2;
					//******************
					/*MAEHSH 14.05.007
					SqlDataReader rdr=null;
					SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					SqlCon.Open();
					string sqlstr="select Prod_Name,pack_type,unit  from products";
					SqlCommand cmd;
					string PName="";
					dbobj.SelectQuery(sqlstr,ref rdr);
					while(rdr.Read())
					{
						PName=rdr["Prod_Name"].ToString();
						if(PName.StartsWith("SERVO"))
							PName=PName.Substring(5);
						cmd=new SqlCommand("insert into ProdTable values('','','"+rdr.GetValue(2).ToString()+"','"+PName.Trim().ToString()+"','"+rdr.GetValue(1).ToString()+"')",SqlCon);
						cmd.ExecuteNonQuery();
					}
					rdr.Close();
					SqlCon.Close();
					*/
					//******************
					
					
					//sql="select prod_Name + ':' + pack_type  from products where unit = 'Nos.' order by prod_Name";
					string sql="select prod_Name + ':' + pack_type  from products where unit = 'Can' or unit = 'Nos.' or unit = 'Tin' order by prod_Name";
					//sql="select prod_Name + ':' + pack_type  from ProdTable where Category = 'Nos.' or Category = 'Tin' order by Prod_Name";
					SqlDtr1 = obj.GetRecordSet (sql);
					while(SqlDtr1.Read ())
					{
						string st=SqlDtr1.GetValue(0).ToString ();
						dropfoc.Items.Add(SqlDtr1.GetValue(0).ToString ());
					}
					SqlDtr1.Close();
					FillList();
					// truncate table after use.
					//dbobj.Insert_or_Update("truncate table ProdTable", ref x);
					//	sql="select prod_Name + ':' + pack_type from products";
					//	SqlDtr2 = obj.GetRecordSet (sql);
					//	while(SqlDtr.Read ())
					//	{
					//	DropShiftID.Items.Add(SqlDtr.GetValue(0).ToString ());   
					//	ListEmpAvailable.Items.Add(SqlDtr2.GetValue(0).ToString ());
					//	}
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:SchemeDiscountEntry.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid); 
				}
			}

            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		/// <summary>
		/// This method is used to fatch the all product name with pack type and fill in list.
		/// </summary>
		public void FillList()
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader SqlDtr;
			string sql="select prod_Name, prod_Name + ':' + pack_type  from products  where unit!='Nos.' order by prod_Name";
			SqlDtr = obj.GetRecordSet (sql);
			ListEmpAvailable.Items.Clear();
			while(SqlDtr.Read ())
			{
				ListEmpAvailable.Items.Add(SqlDtr.GetValue(1).ToString ());
			}
			SqlDtr.Close();
		}

		//**************
		/// <summary>
		/// this is used to generate the next ID auto .
		/// </summary>
		public void GetNextschemeID()
		{
			try
			{
				PartiesClass obj=new PartiesClass();
				SqlDataReader SqlDtr=null;

				#region Fetch Next scheme ID
				//SqlDtr =obj.GetNextschemeID();
				int count = 0,count1 = 0;
				//dbobj.ExecuteScalar("Select max(sch_id)+1 from  oilscheme",ref count);
				//dbobj.ExecuteScalar("Select max(sch_id)+1 from  per_discount",ref count1);
				dbobj.SelectQuery("Select max(sch_id)+1 from  oilscheme",ref SqlDtr);
				if(SqlDtr.Read())
				{
					if(SqlDtr.GetValue(0).ToString()!=null && SqlDtr.GetValue(0).ToString()!="")
						count = int.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				dbobj.SelectQuery("Select max(sch_id)+1 from  per_discount",ref SqlDtr);
				if(SqlDtr.Read())
				{
					if(SqlDtr.GetValue(0).ToString()!=null && SqlDtr.GetValue(0).ToString()!="")
						count1 = int.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				if(count>=count1)
				{
					lblschid.Text =count.ToString();
					if(lblschid.Text=="0" || lblschid.Text==null)
						lblschid.Text ="900001";
				}
				else
				{
					lblschid.Text =count1.ToString();
					if (lblschid.Text=="0" || lblschid.Text==null)
						lblschid.Text ="900001";
				}
//				while(SqlDtr.Read())
//				{
//					lblschid.Text =SqlDtr.GetSqlValue(0).ToString ();
//					if (lblschid.Text=="Null")
//						lblschid.Text ="900001";
//				}	
//				SqlDtr.Close();
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:schemediscountEntry.aspx,Class:PartiesClass.cs: Method:GetNextschemeID().  EXCEPTION "+ ex.Message  + "  User  "+uid);
			}
		}

		//************
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
		/// this method is used to select the particular scheme. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropShiftID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//	countNo1=0;
			try
			{
				//Clear();
				if(DropShiftID.SelectedItem.Text.Equals("Secondry(LTR Scheme)") || DropShiftID.SelectedItem.Text.Equals("Primary(LTR&% Scheme)") || DropShiftID.SelectedItem.Text.Equals("Secondry SP(LTRSP Scheme)") || DropShiftID.SelectedItem.Text.Equals("Primary(LTR&% Addl Scheme)"))
				{
					Panel1.Visible=true;
					//txtevery.Enabled=false;
					//txtfree.Enabled=false;
					//txtrs.Enabled=true;
					Panel2.Visible=false;
				}
				else if(DropShiftID.SelectedItem.Text.Equals("Primary(Free Scheme)") || DropShiftID.SelectedItem.Text.Equals("Secondry(Free Scheme)"))
				{
					Panel2.Visible=true;
					//dropfoc.Enabled=true;
					//txtevery.Enabled=true;
					//txtfree.Enabled=true;
					//txtrs.Enabled=false;
					Panel1.Visible=false;
				}
				else
				{
					Panel1.Visible=false;
					Panel2.Visible=false;
				}
				//				if(DropShiftID.SelectedItem.Text.Equals("Both"))
				//				{
				//					dropfoc.Enabled=true;
				//					txtevery.Enabled=true;
				//					txtfree.Enabled=true;
				//					txtrs.Enabled=true;
				//				}
				//	shift1= DropShiftID.SelectedItem.Value ;
				//	EmployeeClass obj=new EmployeeClass();
				//	SqlDataReader SqlDtr;
				//	string sql;

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:schemediscountentry.aspx,Method:cmdrpt_Click"+ ex.Message+"  EXCEPTION  "+uid);
			}
		}

		/// <summary>
		/// this is used to transfer the selected product from available to assigned.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnIn_Click(object sender, System.EventArgs e)
		{			
			transfer();
			//foreach(System.Web.UI.WebControls.ListItem lst in ListEmpAvailable.Items)
			//{
			//if(ListEmpAvailable.SelectedItem)
			//ListEmpAssigned.Items.Add(lst);
			//}
		}
		
		/// <summary>
		/// This method is used to fatch the all Group name from Customertype Table.
		/// Add By Vikas Sharma 23.10.2012
		/// </summary>
		public void getGroup()
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader SqlDtr;
			string sql="select Distinct Group_Name  from CustomerType";
			SqlDtr = obj.GetRecordSet (sql);
			dropGroup.Items.Clear();
			dropGroup.Items.Add("Select");
			while(SqlDtr.Read ())
			{
				if(SqlDtr["Group_Name"].ToString()!=null && SqlDtr["Group_Name"].ToString()!="")
				{
					dropGroup.Items.Add(SqlDtr["Group_Name"].ToString ());
				}
			}
			SqlDtr.Close();
			dropGroup.Items.Add("Multiple");
		}

		/// <summary>
		/// this is used to save the scheme .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{

				InventoryClass obj =new InventoryClass();
				string sql;
				SqlDataReader SqlDtr=null;
				if(ListEmpAssigned.Items.Count!=0)
				{
					/*******Add by vikas 31.10.2012*************/
					
					if(Request.Params.Get("chkGroup")!=null)
					{
						obj.Group_Name=Session["Group"].ToString();
						group=Session["Group"].ToString();
					}
					else
					{
						obj.Group_Name="";
						group="";
					}
					/*********End***********/

					/*******Add by vikas 31.10.2012*************
					string group="";
					if(chkGroup.Checked!=true)
					{
						obj.Group_Name=Session["Group"].ToString();
						group=Session["Group"].ToString();
					}
					else
					{
						//obj.Group_Name="";
						//group="";
						MessageBox.Show("Please Select Customer Group");
					}
					*********End***********/

					for(int i=0;i<ListEmpAssigned.Items.Count;++i)
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value; 
						string[] arr1=pname.Split(new char[]{':'},pname.Length);
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr;
						string sname=DropShiftID.SelectedItem.Text;
						string schname="";
						string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
						rdr = obj1.GetRecordSet (sql1);
						//dbobj.SelectQuery(sql1,ref SqlDtr);
						if(rdr.Read ())
						{
							/*
							if(sname.IndexOf("Free")>0)
								schname="Free Scheme";
							else
								schname="LTR Scheme";
							*/

							/****Open by vikas 02.06.09***********
							 if(sname.IndexOf("Free")>0)
								schname="Free Scheme";
							else if(sname.IndexOf("LTR&%")>0)
								schname="LTR&% Scheme";
							else if(sname.IndexOf("LTRSP")>0)
								schname="LTRSP Scheme";
							else
								schname="LTR Scheme";
							**************************************/

							/****Open by vikas 02.07.09***********/
							 if(sname.IndexOf("Free")>0)
								schname="Free Scheme";
							else if(sname.IndexOf("LTR&% Addl")>0)
								 schname="LTR&% Addl Scheme";
							else if(sname.IndexOf("LTR&%")>0)
								 schname="LTR&% Scheme";
							else if(sname.IndexOf("LTRSP")>0)
								schname="LTRSP Scheme";
							else
								schname="LTR Scheme";
							/**************************************/

							// Coment by vikas 01.07.09 schname="Primary(LTR&% Scheme)";

							//coment by vikas 27.10.2012 sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"' and schname like '%"+schname+"%' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
							
							//coment by vikas 31.10.2012 sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"' and schname like '%"+schname+"%' and group_name='"+dropGroup.SelectedValue.ToString().Trim()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
							
							sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"' and schname like '%"+schname+"%' and group_name='"+group.ToString().Trim()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
							dbobj.SelectQuery(sql1,ref SqlDtr);
							if(SqlDtr.Read())
							{
								//if(int.Parse(SqlDtr.GetValue(0).ToString())>0)
								//{
								MessageBox.Show("'"+pname+"'"+" Allready Exist");
								return;
								//}
							}
						}
						rdr.Close();
					}
				}
				else
				{
					MessageBox.Show("Please Select At Least One Product");
					return;
				}
				
				obj.schname=DropShiftID.SelectedItem.Text.ToString();
				obj.schid=lblschid.Text;
				if(txtschname.Text.Equals(""))
					obj.schtype="";
				else
					obj.schtype=txtschname.Text.ToString();
				if(DropShiftID.SelectedItem.Text.Equals("Primary(Free Scheme)") || DropShiftID.SelectedItem.Text.Equals("Secondry(Free Scheme)"))
				{
					string pname1=dropfoc.SelectedItem.Text.ToString();

					string[] arr2=pname1.Split(new char[]{':'},pname1.Length);  
					sql="select Prod_ID from Products where Prod_Name='"+arr2[0]+"' and Pack_Type='"+arr2[1]+"'";
					SqlDtr = obj.GetRecordSet (sql);
					while(SqlDtr.Read ())
					{
						obj.schprodid=SqlDtr.GetValue(0).ToString();
					
					}
					SqlDtr.Close();
					
				}
				else
				{
					obj.schprodid="";
				}
				
				if(txtevery.Text.Equals(""))
					obj.onevery="";
				else
					obj.onevery=txtevery.Text.ToString();
				if(txtfree.Text.Equals(""))
					obj.freepack="";
				else
					obj.freepack=txtfree.Text.ToString();
				if(txtrs.Text.Equals(""))
					obj.discount="";
				else
					obj.discount=txtrs.Text.ToString();
				
				obj.schemetype=DropDisType.SelectedItem.Text;
				obj.dateto=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString()));
					
				obj.datefrom=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString()));			
				obj.Type=DropType.SelectedItem.Text;

				//obj.Group_Name=dropGroup.SelectedValue.ToString().Trim();      //Add by Vikas Sharma 23.10.2012
				
				obj.Unit=dropUnit.SelectedValue.ToString().Trim();               //Add by Vikas Sharma 25.10.2012
				obj.SPack_Type=DropPackType.SelectedValue.ToString().Trim();     //Add by Vikas Sharma 7.11.2012

				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					ListEmpAssigned.SelectedIndex =i;
					string pname = ListEmpAssigned.SelectedItem.Value; 
					//string sql;
					string[] arr1=pname.Split(new char[]{':'},pname.Length);  
					string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
					//						SqlDtr = obj.GetRecordSet (sql1);
					dbobj.SelectQuery(sql1,ref SqlDtr);
					while(SqlDtr.Read ())
					{
						obj.prodid=SqlDtr.GetValue(0).ToString();
						obj.InsertSchemediscount();
					}
				}
				SqlDtr.Close();
				//  InsertSchemediscount()
				//	obj.InsertShiftAssignment();

					 
				//	countNo1=0;
				//	countNo2=0;   
				MessageBox.Show("Scheme Saved"); 
				Clear();
				FillList();
				GetNextschemeID();
				CreateLogFiles.ErrorLog("Form:schemeDiscountentry.aspx,Method:btnSubmit_Click  Scheme Discount Entry Saved, User : "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Schemediscount.aspx,Method:btnSubmit_Click EXCEPTION "+ex.Message );	
			}
		}
		
		/// <summary>
		/// Clears the form
		/// </summary>
		public void Clear()
		{
			//dropfoc.Items.Clear();
			//dropfoc.SelectedIndex=0;
			//DropShiftID.SelectedIndex=0;
			txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
			txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
			txtevery.Text="";
			txtrs.Text="";
			txtfree.Text="";
			txtschname.Text="";
			GetNextschemeID();
			ListEmpAssigned.Items.Clear();
			Panel1.Visible=false;
			Panel2.Visible=false;
			DropShiftID.SelectedIndex=0;
			DropDisType.SelectedIndex=0;
			DropType.Enabled=true;
			DropType.SelectedIndex=0;
			chkGroup.Checked=false;
			dropUnit.SelectedIndex=0;
			//ListEmpAvailable.Items.Clear();
			DropPackType.SelectedIndex=0;
			
		} 
		
		/// <summary>
		/// this is used to transfer the selected product from  assigned to available.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonout_Click(object sender, System.EventArgs e)
		{
			//countNo2=1;
			//countNo1=1;
			//string empRemove="";
			//string empSelect="";
			try
			{
				/*
				//EmployeeClass obj=new EmployeeClass();	
				ListEmpAvailable.Items.Add(ListEmpAssigned.SelectedItem.Value);   
				empRemove=ListEmpAssigned.SelectedItem.Value;
				empSelect=ListEmpAssigned.SelectedItem.Value;
				ListEmpAssigned.Items.Remove(ListEmpAssigned.SelectedItem.Value);
				*/
				
				while(ListEmpAssigned.SelectedItem.Selected)
				{
					ListEmpAvailable.Items.Add(ListEmpAssigned.SelectedItem.Value);
					ListEmpAssigned.Items.Remove(ListEmpAssigned.SelectedItem.Value);  
				}
				//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:buttonout_Click"+"  Selected Employee is "+empSelect+"  empRemove  "+empRemove+"   userid "+uid);
			}
			catch(Exception)
			{
				//CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:buttonout_Click"+ex.Message+" EXCEPTION  "+uid);
			}		
		}

		/// <summary>
		/// this is used to transfer the selected product from available to assigned.
		/// </summary>
		public void transfer()
		{
			//countNo1=1;
			//countNo2=1;
				
			try
			{
				
				//MessageBox.Show(ListEmpAvailable.SelectedValue);
				//int cc=ListEmpAvailable.SelectedIndex;
				//MessageBox.Show(cc.ToString());
				while(ListEmpAvailable.SelectedItem.Selected)
				{
					ListEmpAssigned.Items.Add(ListEmpAvailable.SelectedItem.Value);  
					ListEmpAvailable.Items.Remove(ListEmpAvailable.SelectedItem.Value);
				}
				
				//CreateLogFiles.ErrorLog("Form:Schemediscount.aspx,Method:btnIn_Click"+"  userid "+uid);
				
				
			}
			catch(Exception)
			{
				//MessageBox.Show("Please Select Product");
				//CreateLogFiles.ErrorLog("Form:Schemediscount_assignment.aspx,Method:btnIn_Click"+"  EXCCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// this is used to transfer  all the  product from available to assigned and vice-versa.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnOut_Click(object sender, System.EventArgs e)
		{
			//countNo2=1;
			//countNo1=1;
			if(btn1.Text.Trim().Equals(">>"))
			{
				try
				{
					btn1.Text="<<";
					foreach(System.Web.UI.WebControls.ListItem lst in ListEmpAvailable.Items)
						ListEmpAssigned.Items.Add(lst);
					ListEmpAvailable.Items.Clear();
					//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:btnOut_Click"+uid);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:schemediscountentry.aspx,Method:cmdrpt_Click"+ ex.Message);
					//	CreateLogFiles.ErrorLog("Form:Shift.aspx,Method:cmdrpt_Click"+ ex.Message+"EXCEPTION  "+uid);
				}
			}
			else
			{
				try
				{
					btn1.Text=">>";
					foreach(System.Web.UI.WebControls.ListItem lst in ListEmpAssigned.Items)
						ListEmpAvailable.Items.Add(lst);
					ListEmpAssigned.Items.Clear();
					//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:btnOut_Click"+uid);
					
				}
				catch(Exception ex)
				{	
					CreateLogFiles.ErrorLog("Form:schemediscountentry.aspx,Method:btnOut_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
					//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:btnOut_Click"+ex.Message+"EXCEPTION  "+uid);
				}
			}
		}

		/// <summary>
		/// return mm/dd/yyyy date
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
		/// this is used to fill the id in the dropdown for update the scheme.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btschid_Click(object sender, System.EventArgs e)
		{
			btnSubmit.Visible=false;
			lblschid.Visible=false;
			//txtDateFrom.Text="";
			//txtDateTo.Text="";
			ListEmpAssigned.Items.Clear();
			ListEmpAvailable.Enabled=true;
			DropShiftID.SelectedIndex=0;
			dropschid.Visible=true;
			btschid.Visible=false;
			//btnIn.Enabled=false;
			//btnout.Enabled=false;
			//btn1.Enabled=false;
			//Panel1.Visible=false;
			//Panel2.Visible=false;
			btnSubmit.Enabled=false;
			//	txtevery.Enabled=false;
			//	txtfree.Enabled=false;
			//	txtrs.Enabled=false;
			btnupdate.Visible=true;
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr1;
			string sql;

			#region Fetch the All Invoice Number and fill in Combo
			dropschid.Items.Clear();  
			dropschid.Items.Add("Select"); 
			//**sql="select distinct sch_id  from oilscheme order by sch_id";
			sql="select distinct sch_id ,schtype from oilscheme union select distinct sch_id ,schtype from per_discount order by sch_id";
			SqlDtr1=obj.GetRecordSet(sql);
			while(SqlDtr1.Read())
			{
				//**dropschid.Items.Add(SqlDtr1.GetValue(0).ToString());
				dropschid.Items.Add(SqlDtr1.GetValue(0).ToString() + ':' + SqlDtr1.GetValue(1).ToString());
			}
			SqlDtr1.Close ();
			#endregion
		}
		
		/// <summary>
		/// this is used to fill all the data on the behalf of selected Id in the dropdown.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropschid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SqlConnection con;
			SqlCommand cmd;
			InventoryClass obj=new InventoryClass ();
			SqlDataReader rdr2=null;
			SqlDataReader rdr1=null;

			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				//***
				string scheme=dropschid.SelectedItem.Text.Trim().ToString();
				string[] schid=scheme.Split(new char[]{':'},scheme.Length);  
				//****
				//**cmd=new SqlCommand("select * from oilscheme WHERE sch_id='"+dropschid.SelectedItem.Text.Trim().ToString()+"'",con);
				int count = 0;
				dbobj.ExecuteScalar("Select Count(*) from  oilscheme WHERE sch_id='"+schid[0]+"'",ref count);
				if(count>0)
					cmd=new SqlCommand("select * from oilscheme WHERE sch_id='"+schid[0]+"'",con);
				else
					cmd=new SqlCommand("select * from Per_Discount WHERE sch_id='"+schid[0]+"'",con);
				SqlDtr=cmd.ExecuteReader();
				ListEmpAssigned.Items.Clear();
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						if(SqlDtr.GetValue(3).ToString().Equals("Secondry(LTR Scheme)") || SqlDtr.GetValue(3).ToString().Equals("Primary(LTR&% Scheme)") || SqlDtr.GetValue(3).ToString().Equals("Secondry SP(LTRSP Scheme)")  || SqlDtr.GetValue(3).ToString().Equals("Primary(LTR&% Addl Scheme)"))
						{
							Panel2.Visible=false;
							Panel1.Visible=true;
						}
						else if(SqlDtr.GetValue(3).ToString().Equals("Secondry(Free Scheme)") || SqlDtr.GetValue(3).ToString().Equals("Primary(Free Scheme)"))
						{
							Panel2.Visible=true;
							Panel1.Visible=false;
						}
						
						DropShiftID.SelectedIndex=(DropShiftID.Items.IndexOf((DropShiftID.Items.FindByValue(SqlDtr.GetValue(3).ToString()))));
						//DropShiftID.Enabled=false;
						if(SqlDtr.GetValue(1).Equals("")||SqlDtr.GetValue(1).Equals("NULL"))
							txtschname.Text="";
						else
							txtschname.Text=SqlDtr.GetValue(1).ToString();
						txtevery.Text=SqlDtr.GetValue(5).ToString();
						txtfree.Text=SqlDtr.GetValue(6).ToString();
						txtrs.Text=SqlDtr.GetValue(7).ToString();
						DropDisType.SelectedIndex=(DropDisType.Items.IndexOf((DropDisType.Items.FindByValue(SqlDtr.GetValue(8).ToString()))));
						txtDateFrom.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(9).ToString()));
						txtDateTo.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(10).ToString()));
						dbobj.SelectQuery("select prod_name+':'+pack_type from products where prod_ID="+SqlDtr.GetValue(2).ToString()+" ", ref rdr1);
						
						if(rdr1.Read())
						{
							ListEmpAssigned.Items.Add(rdr1.GetValue(0).ToString());
						}
						dbobj.SelectQuery("select prod_Name + ':' + pack_type from products where prod_ID="+SqlDtr.GetValue(4).ToString()+" ", ref rdr2);
						if(rdr2.Read())
						{
							dropfoc.SelectedIndex=(dropfoc.Items.IndexOf((dropfoc.Items.FindByValue(rdr2.GetValue(0).ToString()))));
						}
						if(count>0)
							DropType.SelectedIndex=0;
						else
							DropType.SelectedIndex=1;
						//Coment by vikas 17.08.09 DropType.Enabled=false;

						/*******Vikas 29.10.2012***********
						if(count>0)
						{
							lblGroup.Visible=true;
							dropGroup.Visible=true;
						}
						else
						{
							lblGroup.Visible=false;
							dropGroup.Visible=false;
						}
						********end**********/
						//dropGroup.SelectedIndex=dropGroup.Items.IndexOf(dropGroup.Items.FindByText(SqlDtr.GetValue(11).ToString().Trim()));     // Add by Vikas Sharma 23.10.2012
						if(SqlDtr["Group_Name"].ToString().Trim()!=null && SqlDtr["Group_Name"].ToString().Trim()!="")
						{
							group=SqlDtr["Group_Name"].ToString().Trim();
							chkGroup.Checked=true;
						}
						else
						{
							group="";
							chkGroup.Checked=false;
						}
						
						dropUnit.SelectedIndex=dropUnit.Items.IndexOf(dropUnit.Items.FindByText(SqlDtr.GetValue(12).ToString().Trim()));				// Add by Vikas Sharma 25.10.2012
						DropPackType.SelectedIndex=DropPackType.Items.IndexOf(DropPackType.Items.FindByText(SqlDtr.GetValue(13).ToString().Trim()));   // Add by Vikas Sharma 7.11.2012
					}
				}
				
				//rdr1.Close();
				//rdr2.Close();
				dropschid.Visible=true;
				btschid.Visible=false;
				
				SqlDtr.Close (); 
				con.Close();
				
					
				CreateLogFiles.ErrorLog("Form:schemediscountEntry.aspx,Method:dropschid_SelectedIndexChange, Userid= "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:schemediscountEntry.aspx,Method:dropschid_SelectedIndexChange"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				//MessageBox.Show(ex.Message);
			}
		}
		/// <summary>
		/// this is used to transfer the selected product from available to assigned.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ListEmpAvailable_DoubleClick(object sender, System.EventArgs e)
		{
			//transfer();
		
		}

		/// <summary>
		/// this is used to Update the scheme on behalf of the selected scheme in dropdown.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnupdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				InventoryClass obj =new InventoryClass();
				string sql;
				SqlDataReader SqlDtr=null;
				
				obj.schname=DropShiftID.SelectedItem.Text.ToString();
				string scheme=dropschid.SelectedItem.Text.Trim().ToString();
				string[] schid=scheme.Split(new char[]{':'},scheme.Length);  
				//	obj.schid=dropschid.SelectedItem.Text.ToString();
				obj.schid=schid[0];
				if(txtschname.Text.Equals(""))
					obj.schtype="";
				else
					obj.schtype=txtschname.Text.ToString();

				/*******Add by vikas 31.10.2012*************
				
				if(Request.Params.Get("chkGroup")!=null)
				{
					obj.Group_Name=Session["Group"].ToString();
					group=Session["Group"].ToString();
				}
				else
				{
					obj.Group_Name="";
					group="";
				}

				*********End***********/
				/*******Add by vikas 31.10.2012*************/
				//string s=Session["Group"].ToString();
				if(chkGroup.Checked==true)
				{

					if(Session["Group"]!=null && Session["Group"].ToString()!="")
					{
						
						obj.Group_Name=Session["Group"].ToString();
						group=Session["Group"].ToString();
						
					}
					else
					{
						//if(group==null && group.ToString()=="")
						//{
							obj.Group_Name=group;
						//}
						//else
						//{
							//obj.Group_Name="";
						//}
						
					}
				}
				else
				{
					obj.Group_Name="";
					group="";
				}
				
					
					//group="";
					//MessageBox.Show("Please Select Customer Group");
				
				/*********End***********/

				if(DropShiftID.SelectedItem.Text.Equals("Primary(Free Scheme)") || DropShiftID.SelectedItem.Text.Equals("Secondry(Free Scheme)"))
				{
					string pname1=dropfoc.SelectedItem.Text.ToString();

					string[] arr2=pname1.Split(new char[]{':'},pname1.Length);  
					sql="select Prod_ID from Products where Prod_Name='"+arr2[0]+"' and Pack_Type='"+arr2[1]+"'";
					SqlDtr = obj.GetRecordSet (sql);
					while(SqlDtr.Read ())
					{
						obj.schprodid=SqlDtr.GetValue(0).ToString();
					
					}
					SqlDtr.Close();
					
				}
				else
				{
					obj.schprodid="";
					//obj.InsertSchemediscount();		
				}
				
				if(txtevery.Text.Equals(""))
					obj.onevery="";
				else
					obj.onevery=txtevery.Text.ToString();
				if(txtfree.Text.Equals(""))
					obj.freepack="";
				else
					obj.freepack=txtfree.Text.ToString();
				if(txtrs.Text.Equals(""))
					obj.discount="";
				else
					obj.discount=txtrs.Text.ToString();
				obj.schemetype=DropDisType.SelectedItem.Text;
				//DateTime df;
				//DateTime dt;
				obj.dateto=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString()));
					
				obj.datefrom=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString()));			
				obj.Type=DropType.SelectedItem.Text;

				//obj.Group_Name=dropGroup.SelectedValue.ToString().Trim();			//Add by Vikas Sharma 23.10.2012
				obj.Unit=dropUnit.SelectedValue.ToString().Trim();					//Add by Vikas Sharma 25.10.2012
				obj.SPack_Type=DropPackType.SelectedValue.ToString().Trim();		//Add by Vikas Sharma 7.11.2012
				//**************
				if(ListEmpAssigned.Items.Count!=0)
				{
					for(int i=0;i<ListEmpAssigned.Items.Count;++i)
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value;
						string[] arr1=pname.Split(new char[]{':'},pname.Length);
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr,rdr1=null;
						string sname=DropShiftID.SelectedItem.Text;
						string schname="";
						string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
						rdr = obj1.GetRecordSet (sql1);
						//dbobj.SelectQuery(sql1,ref SqlDtr);
						if(rdr.Read ())
						{
							if(DropType.SelectedIndex==0)
							{
								sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"' and sch_id='"+schid[0]+"'";
								dbobj.SelectQuery(sql1,ref rdr1);
								if(rdr1.Read())
								{
								
								}
								else
								{
									/*
									if(sname.IndexOf("Free")>0)
										schname="Free Scheme";
									else
										//**schname="LTR Scheme";
										schname="LTR";
									*/

									/***coment by vikas 02.06.09********
									if(sname.IndexOf("Free")>0)
										schname="Free Scheme";
									else if(sname.IndexOf("LTR&%")>0)
										schname="LTR&% Scheme";
									else if(sname.IndexOf("LTRSP")>0)
										schname="LTRSP Scheme";
									else
										schname="LTR Scheme";
									********End********************/

									/****Add by vikas 02.07.09***********/
									if(sname.IndexOf("Free")>0)
										schname="Free Scheme";
									else if(sname.IndexOf("LTR&% Addl")>0)
										schname="LTR&% Addl Scheme";
									else if(sname.IndexOf("LTR&%")>0)
										schname="LTR&% Scheme";
									else if(sname.IndexOf("LTRSP")>0)
										schname="LTRSP Scheme";
									else
										schname="LTR Scheme";
									/**************************************/

									//Coment by vikas 01.07.09 schname="Primary(LTR&% Scheme)";

									//coment by vikas 27/10/2012 sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"'and schname like '%"+schname+"%' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
									
									//coment by vikas 31/10/2012sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"'and schname like '%"+schname+"%' and group_name='"+dropGroup.SelectedValue.ToString().Trim()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
									sql1="select * from oilscheme where Prodid='"+rdr["Prod_ID"].ToString()+"'and schname like '%"+schname+"%' and group_name='"+group.ToString().Trim()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
									
									dbobj.SelectQuery(sql1,ref SqlDtr);
									if(SqlDtr.Read())
									{
										//if(int.Parse(SqlDtr.GetValue(0).ToString())>0)
										//{
										MessageBox.Show("'"+pname+"'"+" Allready Exist");
										return;
										//}
									}
								}
							}
							else
							{
								sql1="select * from per_discount where Prodid='"+rdr["Prod_ID"].ToString()+"' and sch_id='"+schid[0]+"'";
								dbobj.SelectQuery(sql1,ref rdr1);
								if(rdr1.Read())
								{
								
								}
								else
								{
									/*Coment by vikas 02.07.09if(sname.IndexOf("Free")>0)
										schname="Free Scheme";
									else
										//**schname="LTR Scheme";
										schname="LTR";*/

									/*coment by vikas 02.07.09 if(sname.IndexOf("Free")>0)
										schname="Free Scheme";
									else if(sname.IndexOf("LTR&% Addl")>0)
										schname="LTR&% Addl";
									else
										schname="LTR";*/

									/****Add by vikas 02.07.09***********/
									if(sname.IndexOf("Free")>0)
										schname="Free Scheme";
									else if(sname.IndexOf("LTR&% Addl")>0)
										schname="LTR&% Addl Scheme";
									else if(sname.IndexOf("LTR&%")>0)
										schname="LTR&% Scheme";
									else if(sname.IndexOf("LTRSP")>0)
										schname="LTRSP Scheme";
									else
										schname="LTR Scheme";
									/**************************************/


									//coment by vikas 27/10/2012 sql1="select * from per_discount where Prodid='"+rdr["Prod_ID"].ToString()+"'and schname like '%"+schname+"%' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
									//coment by vikas 31/10/2012sql1="select * from per_discount where Prodid='"+rdr["Prod_ID"].ToString()+"'and schname like '%"+schname+"%' and group_name='"+dropGroup.SelectedValue.ToString().Trim()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
									sql1="select * from per_discount where Prodid='"+rdr["Prod_ID"].ToString()+"'and schname like '%"+schname+"%' and group_name='"+group.ToString().Trim()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
									dbobj.SelectQuery(sql1,ref SqlDtr);
									if(SqlDtr.Read())
									{
										//if(int.Parse(SqlDtr.GetValue(0).ToString())>0)
										//{
										MessageBox.Show("'"+pname+"'"+" Allready Exist");
										return;
										//}
									}
								}
								rdr.Close();
							}
						}
						rdr.Close();
					}
				}
				else
				{
					MessageBox.Show("Please Select At Least One Product");
					return;
				}
				//**************
				SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlCon.Open();
				SqlCommand cmd;
				SqlCommand cmd1;
				if(DropType.SelectedIndex==0)
				{
					cmd=new SqlCommand("delete from oilscheme where sch_id='"+schid[0]+"'",SqlCon);
					cmd1=new SqlCommand("delete from per_discount where sch_id='"+schid[0]+"'",SqlCon);    //Add by vikas  17.08.09
				}
				else
				{
					cmd=new SqlCommand("delete from per_discount where sch_id='"+schid[0]+"'",SqlCon);
					cmd1=new SqlCommand("delete from oilscheme where sch_id='"+schid[0]+"'",SqlCon);       //Add by vikas  17.08.09
				}
				cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
				SqlCon.Close();
                cmd.Dispose();
				cmd1.Dispose();
				//**************
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					ListEmpAssigned.SelectedIndex =i;
					string pname = ListEmpAssigned.SelectedItem.Value; 
					//string sql;
					string[] arr1=pname.Split(new char[]{':'},pname.Length);  
					string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
					//						SqlDtr = obj.GetRecordSet (sql1);
					dbobj.SelectQuery(sql1,ref SqlDtr);
					while(SqlDtr.Read ())
					{
						obj.prodid=SqlDtr.GetValue(0).ToString();
						//obj.InsertSchemediscount();
						obj.updateSchemediscount();
					}
						
				}
				//SqlDtr.Close();
				
				MessageBox.Show("Scheme Updated"); 
				Session["Group"]="";										 //Add by vikas 1.11.2012
				Clear();
				FillList();
				GetNextschemeID();
				dropschid.Visible=false;
				//DropShiftID.SelectedIndex=0;
				btnupdate.Visible=false;
				lblschid.Visible=true;
				btnSubmit.Visible=true;
				btnSubmit.Enabled=true;
				btschid.Visible=true;
				//Panel1.Visible=false;
				//Panel2.Visible=false;
				CreateLogFiles.ErrorLog("Form:schemeDiscountentry.aspx,Method:btnupdate_Click  Scheme Discount Entry Updated, User "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Schemediscount.aspx,Method:btnupdate_Click   EXCEPTION "+ ex.Message  + "  User  "+uid);	
			}
		}

		protected void ListEmpAssigned_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void DropType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			/*if(DropType.SelectedValue=="Purchase")
			{
				lblGroup.Visible=false;
				dropGroup.Visible=false;
				
			}
			else
			{
				lblGroup.Visible=true;
				dropGroup.Visible=true;
			}*/

		}
	}
}