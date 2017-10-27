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

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for Fleet_OE_Discount_Entry.
	/// </summary>
	public partial class Fleet_OEDiscountEntry : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
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
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid);
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
				string SubModule="8";
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
					btnsave.Enabled=false;
				if(Edit_Flag=="0")
					btschid.Enabled=false;
				
				#endregion
				try
				{
					dropschid.Visible=false;
					btnupdate.Visible=false;
					GetNextschemeID();
					getCustandProd();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid); 
				}
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		/// <summary>
		/// This method is used to fatch the Customer name with type and fill the list.
		/// and also fatch the product name with pack type and fill the list.
		/// </summary>
		public void getCustandProd()
		{
			InventoryClass obj= new InventoryClass();
			SqlDataReader SqlDtr;
			string sql;
			//sql="select cust_Name, cust_Name + ':' + cust_type  from customer  where cust_type='Fleet' or cust_type like('Oe%')  order by cust_Name";
			sql="select cust_Name, cust_Name + ':' + cust_type  from customer where cust_type like'Oe%' order by cust_Name";
			SqlDtr = obj.GetRecordSet (sql);
			ListEmpAvailable.Items.Clear();
			while(SqlDtr.Read ())
			{
				ListEmpAvailable.Items.Add(SqlDtr.GetValue(1).ToString());
			}
			SqlDtr.Close();
			
			//*********************************
			sql="select prod_Name, prod_Name + ':' + pack_type  from products  order by prod_Name";
			SqlDtr = obj.GetRecordSet(sql);
			Listprodavail.Items.Clear();
			while(SqlDtr.Read ())
			{
				Listprodavail.Items.Add(SqlDtr.GetValue(1).ToString ());
			}
			SqlDtr.Close();
			//***********************************
		}
		
		//**************
		/// <summary>
		/// This method is used to get the nextID auto.
		/// </summary>
		public void GetNextschemeID()
		{
			try
			{
				PartiesClass obj=new PartiesClass();
				SqlDataReader SqlDtr;

				#region Fetch Next scheme ID
				SqlDtr =obj.GetNextFOID();
				
				while(SqlDtr.Read())
				{
					lblschid.Text =SqlDtr.GetSqlValue(0).ToString ();
					if (lblschid.Text=="Null")
						lblschid.Text ="100001";
				}	
				SqlDtr.Close();
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Class:PartiesClass.cs: Method:GetNextschemeID().  EXCEPTION "+ ex.Message  + "  User  "+uid);
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
		/// this method is used to fill the customer with customer type in a list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropShiftID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader SqlDtr=null;
			string sql="";
			if(DropShiftID.SelectedIndex==0)
			{
				sql="select cust_Name, cust_Name + ':' + cust_type  from customer  where cust_type like'OE%' order by cust_Name";
				SqlDtr = obj.GetRecordSet (sql);
				ListEmpAvailable.Items.Clear();
				while(SqlDtr.Read ())
				{
					ListEmpAvailable.Items.Add(SqlDtr.GetValue(1).ToString());
				}
				SqlDtr.Close();
			}
			else
			{
				sql="select cust_Name, cust_Name + ':' + cust_type  from customer  where cust_type like'Fleet%' order by cust_Name";
				SqlDtr = obj.GetRecordSet (sql);
				ListEmpAvailable.Items.Clear();
				while(SqlDtr.Read ())
				{
					ListEmpAvailable.Items.Add(SqlDtr.GetValue(1).ToString());
				}
				SqlDtr.Close();
			}
		}
		
		/// <summary>
		/// This is used to move the selected customer from one list to anather.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnIn_Click(object sender, System.EventArgs e)
		{			
			transfer();
		}
		
		/// <summary>
		/// This function is used to Transfer from available customer to assigned.
		/// </summary>
		int Flag=0;
		public void transfer()
		{
			//countNo1=1;
			//countNo2=1;
			try
			{
				//ListEmpAssigned.Items.Add(ListEmpAvailable.SelectedItem.Value);  
				//ListEmpAvailable.Items.Remove(ListEmpAvailable.SelectedItem.Value);
				
				//				if(ListEmpAvailable.SelectedItem.Selected==false)
				//				{
				//					MessageBox.Show("Please Select The Customer To Move");
				//					return;
				//				}
				while(ListEmpAvailable.SelectedItem.Selected)
				{
					//ListEmpAvailable.Items.Add(ListEmpAssigned.SelectedItem.Value);
					//ListEmpAssigned.Items.Remove(ListEmpAssigned.SelectedItem.Value);  
					ListEmpAssigned.Items.Add(ListEmpAvailable.SelectedItem.Value);  
					ListEmpAvailable.Items.Remove(ListEmpAvailable.SelectedItem.Value);
					Flag=1;
				}
				//CreateLogFiles.ErrorLog("Form:Schemediscount.aspx,Method:btnIn_Click"+"  userid "+uid);
			}
			catch(Exception)
			{
				if(Flag==0)
					MessageBox.Show("Please Select The Customer To Move");
				//CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnIn_Click"+",Exception : "+ex.Message+"  userid  "+uid);
			}
		}
		
		//This method is used to save the scheme.
		/*	private void btnSubmit_Click(object sender, System.EventArgs e)
			{
				try
				{
					InventoryClass obj =new InventoryClass();
					//string sql;
					SqlDataReader SqlDtr=null;
					obj.foid=lblschid.Text;
					if(txtschname.Text.Equals(""))
						obj.discription="";
					else
						obj.discription=txtschname.Text.ToString();
					obj.custtype=DropShiftID.SelectedItem.Text.ToString();
					if(txtrs.Text.Equals(""))
						obj.discount="";
					else
						obj.discount=txtrs.Text.ToString();
					obj.discounttype=dropdiscount.SelectedItem.Text.ToString();
					obj.dateto=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString()));
					obj.datefrom=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString()));			
					for(int i=0;i<ListEmpAssigned.Items.Count;++i)
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value; 
						string[] arr1=pname.Split(new char[]{':'},pname.Length);  
						string sql1="select cust_ID from customer where cust_Name='"+arr1[0]+"' and cust_Type='"+arr1[1]+"'";
						dbobj.SelectQuery(sql1,ref SqlDtr);
						while(SqlDtr.Read ())
						{
							obj.cust_id=SqlDtr.GetValue(0).ToString();
							obj.InsertFOEdiscount();
						}
					}
					SqlDtr.Close();
					MessageBox.Show("F-OE discount Saved"); 
					Clear();
					GetNextschemeID();
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnSubmit_Click");
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnSubmit_Click EXCEPTION "+ex.Message   + "  User  "+uid );	
				}
			}*/
		
		/// <summary>
		/// Clears the form
		/// </summary>
		public void Clear()
		{
			txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
			txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
			txtrs.Text="";
			txtschname.Text="";
			GetNextschemeID();
			ListEmpAssigned.Items.Clear();
			Listprodassign.Items.Clear();
			Panel1.Visible=false;
			Checkfoe.Checked=false;
		} 
		
		/// <summary>
		/// This is used to come back from assigned to available listbox.
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
				//MessageBox.Show(ListEmpAssigned.Items.Count.ToString());
				//EmployeeClass obj=new EmployeeClass();	
				//ListEmpAvailable.Items.Add(ListEmpAssigned.SelectedItem.Value);   
				//empRemove=ListEmpAssigned.SelectedItem.Value;
				//empSelect=ListEmpAssigned.SelectedItem.Value;
				//ListEmpAssigned.Items.Remove(ListEmpAssigned.SelectedItem.Value);
				while(ListEmpAssigned.SelectedItem.Selected)
				{
					ListEmpAvailable.Items.Add(ListEmpAssigned.SelectedItem.Value);
					ListEmpAssigned.Items.Remove(ListEmpAssigned.SelectedItem.Value);  
					Flag=1;
				}
				//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:buttonout_Click"+"  Selected Employee is "+empSelect+"  empRemove  "+empRemove+"   userid "+uid);
			}
			catch(Exception)
			{
				if(Flag==0)
					MessageBox.Show("Please Select The Customer To Move");
				//CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:buttonout_Click ,Exception : "+ex.Message+"  User  "+uid);
			}		
		}

		/// <summary>
		/// This is used to transfer/retrieve between listboxes with all customer.
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
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnOut_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
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
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnOut_Click EXCEPTION "+ ex.Message  + "  User  "+uid);
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
			strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}

		/// <summary>
		/// This is used to retrieve Fo/oeID in dropdown.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btschid_Click(object sender, System.EventArgs e)
		{
			btnSubmit.Visible=false;
			btnsave.Visible=false;
			lblschid.Visible=false;
			//txtDateFrom.Text="";
			//txtDateTo.Text="";
			ListEmpAssigned.Items.Clear();
			Listprodassign.Items.Clear();
			ListEmpAvailable.Enabled=true;
			DropShiftID.SelectedIndex=0;
			dropschid.Visible=true;
			txtschname.Text="";
			btnSubmit.Enabled=false;
			txtrs.Text="";
			btnupdate.Visible=true;
			btschid.Visible=false;
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr1;
			string sql;

			#region Fetch the All Invoice Number and fill in Combo
			dropschid.Items.Clear();  
			dropschid.Items.Add("Select"); 
			//**sql="select distinct foid ,discription from fleetoe_discount order by foid";
			sql="select distinct foid ,discription from foe order by foid";
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
		/// This is used to fetch the customers/scheme corrosponding to the FO/OeID. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropschid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SqlConnection con;
			SqlCommand cmd;
			InventoryClass obj=new InventoryClass ();
			//SqlDataReader rdr2=null;
			SqlDataReader rdr1=null;
			SqlDataReader rdr3=null;
			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				//***
				string scheme=dropschid.SelectedItem.Text.Trim().ToString();
				string[] schid=scheme.Split(new char[]{':'},scheme.Length);  
				//****
				cmd=new SqlCommand("select * from foe WHERE foid='"+schid[0]+"'",con);
				SqlDtr=cmd.ExecuteReader();
				ListEmpAssigned.Items.Clear();
				Listprodassign.Items.Clear();
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{

						DropShiftID.SelectedIndex=(DropShiftID.Items.IndexOf((DropShiftID.Items.FindByValue(SqlDtr.GetValue(7).ToString()))));
						dropdiscount.SelectedIndex=(dropdiscount.Items.IndexOf((dropdiscount.Items.FindByValue(SqlDtr.GetValue(4).ToString()))));
						
						if(SqlDtr.GetValue(8).Equals("")||SqlDtr.GetValue(8).Equals("NULL"))
							txtschname.Text="";
						else
							txtschname.Text=SqlDtr.GetValue(8).ToString();
						
						txtrs.Text=SqlDtr.GetValue(3).ToString();
						txtDateFrom.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(5).ToString()));
						txtDateTo.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(6).ToString()));
						dbobj.SelectQuery("select cust_name+':'+cust_type from customer where cust_ID="+SqlDtr.GetValue(1).ToString()+" ", ref rdr1);
						if(rdr1.Read())
						{
							int x=ListEmpAssigned.Items.IndexOf((ListEmpAssigned.Items.FindByValue(rdr1.GetValue(0).ToString())));
							//if(ListEmpAssigned.Items.Contains(rdr1.GetValue(0)))
							if(x>=0)
							{}
							else
								ListEmpAssigned.Items.Add(rdr1.GetValue(0).ToString());
						}
						//Checkfoe.Checked=true;
						//Panel1.Visible=true;
						int i=System.Convert.ToInt32(SqlDtr.GetValue(2).ToString());
						if(i!=0)
						{
							Checkfoe.Checked=true;
							Panel1.Visible=true;
							dbobj.SelectQuery("select prod_name+':'+pack_type from products where prod_ID="+SqlDtr.GetValue(2).ToString()+" ", ref rdr3);
							if(rdr3.Read())
							{
								int y=Listprodassign.Items.IndexOf((Listprodassign.Items.FindByValue(rdr3.GetValue(0).ToString())));
								if(y>=0)
								{}
									//								if(Listprodassign.Items.Contains(rdr3.GetValue(0)))
									//								{}
								else
									Listprodassign.Items.Add(rdr3.GetValue(0).ToString());
							}
							//**dbobj.SelectQuery("select prod_Name, prod_Name + ':' + pack_type  from products  order by prod_Name", ref rdr3);
							//**while(rdr3.Read())
							//**{
							//**	Listprodavail.Items.Add(rdr3.GetValue(1).ToString());
							//**}
							btnall.Text=">>";
						}
						else
						{
							Checkfoe.Checked=false;
							Panel1.Visible=false;
							//**dbobj.SelectQuery("select prod_Name, prod_Name + ':' + pack_type  from products  order by prod_Name", ref rdr3);
							//**while(rdr3.Read())
							//**{
							//Listprodassign.Items.Add(rdr3.GetValue(1).ToString());
							//**	Listprodavail.Items.Add(rdr3.GetValue(1).ToString());
							//***}
							//Listprodavail.Items.Clear();
							Listprodassign.Items.Clear();
							btnall.Text="<<";
						}
					}
				}
				
				rdr1.Close();
				//rdr2.Close();
				rdr3.Close();
				dropschid.Visible=true;
				btschid.Visible=false;
				
				SqlDtr.Close (); 
				con.Close();
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:dropschid_SelectedIndexChange, Userid= "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:dropschid_SelectedIndexChange"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				//MessageBox.Show(ex.Message);
			}
			
		}
		
		/// <summary>
		/// This is used to transfer the selected customer on click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ListEmpAvailable_DoubleClick(object sender, System.EventArgs e)
		{
			transfer();
		} 
		
		/// <summary>
		/// This is used to Update the scheme 
		/// on behalf of the FO/OEID selected in dropdown.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnupdate_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ListEmpAssigned.Items.Count==0)
				{
					MessageBox.Show("Customer Assigned List Not Empty");
					return;
				}
				InventoryClass obj =new InventoryClass();
				//string sql;
				SqlDataReader SqlDtr=null;
				SqlDataReader SqlDtr1=null;
				
				obj.custtype=DropShiftID.SelectedItem.Text.ToString();
				string scheme=dropschid.SelectedItem.Text.Trim().ToString();
				string[] schid=scheme.Split(new char[]{':'},scheme.Length);  
				//	obj.schid=dropschid.SelectedItem.Text.ToString();
				obj.foid=schid[0];
				if(txtschname.Text.Equals(""))
					obj.discription="";
				else
					obj.discription=txtschname.Text.ToString();
				//				if(DropShiftID.SelectedItem.Text.Equals("Primary"))
				//				{
				//					string pname1=dropfoc.SelectedItem.Text.ToString();
				//
				//					string[] arr2=pname1.Split(new char[]{':'},pname1.Length);  
				//					sql="select Prod_ID from Products where Prod_Name='"+arr2[0]+"' and Pack_Type='"+arr2[1]+"'";
				//					SqlDtr = obj.GetRecordSet (sql);
				//					while(SqlDtr.Read ())
				//					{
				//						obj.schprodid=SqlDtr.GetValue(0).ToString();
				//					
				//					}
				//					SqlDtr.Close();
				//					
				//				}
				//				else
				//				{
				//					obj.schprodid="";
				//					//obj.InsertSchemediscount();		
				//				}
				
				//				if(txtevery.Text.Equals(""))
				//					obj.onevery="";
				//				else
				//					obj.onevery=txtevery.Text.ToString();
				//				if(txtfree.Text.Equals(""))
				//					obj.freepack="";
				//				else
				//					obj.freepack=txtfree.Text.ToString();
				if(txtrs.Text.Equals(""))
					obj.discount="";
				else
					obj.discount=txtrs.Text.ToString();
                //DateTime df;
                //DateTime dt;
                obj.dateto = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()));
					
				obj.datefrom= System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
				//   string prod="";
				obj.discounttype=dropdiscount.SelectedItem.Text.ToString();
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					if(Checkfoe.Checked)
					{
						int k=Listprodassign.Items.Count;
						if(k==0)
						{
							MessageBox.Show("Please Select The Product");
							return ;
						}
					}
					//ListEmpAssigned.SelectedIndex =i;
					//string pname = ListEmpAssigned.SelectedItem.Value;
					//string sql;
					//string[] arr1=pname.Split(new char[]{':'},pname.Length);  
					//string sql1="select cust_ID from customer where cust_Name='"+arr1[0]+"' and cust_Type='"+arr1[1]+"'";
					//						SqlDtr = obj.GetRecordSet (sql1);
					//dbobj.SelectQuery(sql1,ref SqlDtr);
					//while(SqlDtr.Read ())
					//{
					//	obj.cust_id=SqlDtr.GetValue(0).ToString();
					//}
					//obj.Deletefoecust();
					//SqlDtr.Close();
				}
				
				//**************
				SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlCon.Open();
				SqlCommand cmd;
				cmd=new SqlCommand("delete from foe where foid='"+schid[0]+"'",SqlCon);
				cmd.ExecuteNonQuery();
				SqlCon.Close();
				cmd.Dispose();
				//**************
				//*******************
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					//					if(Checkfoe.Checked)
					//					{
					//						int k=Listprodassign.Items.Count;
					//						if(k==0)
					//						{
					//							MessageBox.Show("Please select product");
					//							return ;
					//						}
					//					}
					if(Checkfoe.Checked)
					{
						for(int j=0;j<Listprodassign.Items.Count;++j)
						{
							ListEmpAssigned.SelectedIndex =i;
							string pname = ListEmpAssigned.SelectedItem.Value; 
							string[] arr1=pname.Split(new char[]{':'},pname.Length);  
							string sql1="select cust_ID from customer where cust_Name='"+arr1[0]+"' and cust_Type='"+arr1[1]+"'";
							dbobj.SelectQuery(sql1,ref SqlDtr);
							while(SqlDtr.Read ())
							{
								obj.cust_id=SqlDtr.GetValue(0).ToString();
							}
							SqlDtr.Close();
							//*****************
							Listprodassign.SelectedIndex =j;
							string prodname = Listprodassign.SelectedItem.Value; 
							string[] arr2=prodname.Split(new char[]{':'},prodname.Length);  
							string sql2="select prod_ID from products where prod_Name='"+arr2[0]+"' and pack_Type='"+arr2[1]+"'";
							dbobj.SelectQuery(sql2,ref SqlDtr1);
							while(SqlDtr1.Read ())
							{
								obj.prod_id=SqlDtr1.GetValue(0).ToString();
							}
							//obj.InsertFOEdiscountEntry();
							SqlDtr1.Close();
							obj.updateFOEdiscountEntry();
							//*******************
						}
					}
					else
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value;
						string[] arr1=pname.Split(new char[]{':'},pname.Length);
						string sql1="select cust_ID from customer where cust_Name='"+arr1[0]+"' and cust_Type='"+arr1[1]+"'";
						dbobj.SelectQuery(sql1,ref SqlDtr);
						while(SqlDtr.Read ())
						{
							obj.cust_id=SqlDtr.GetValue(0).ToString();
							//obj.InsertFOEdiscount();
						}
						SqlDtr.Close();
						obj.prod_id="0";
						//obj.InsertFOEdiscountEntry();
						obj.updateFOEdiscountEntry();
					}
				}
				//***********************
				MessageBox.Show("F-OE Discount Updated");
				Clear();
				GetNextschemeID();
				getCustandProd();
				dropschid.Visible=false;
				DropShiftID.SelectedIndex=0;
				btnupdate.Visible=false;
				lblschid.Visible=true;
				//				btnSubmit.Visible=true;
				//				btnSubmit.Enabled=true;
				btnsave.Visible=true;
				btnsave.Enabled=true;
				btschid.Visible=true;
				Checkfoe.Checked=false;
				Panel1.Visible=false;
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnupdate_Click");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnupdate_Click  EXCEPTION "+ ex.Message  + "  User  "+uid );	
			}
		}

		/// <summary>
		/// This method is used to hide or open the panel according to checkbox is checked or not.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Checkfoe_CheckedChanged(object sender, System.EventArgs e)
		{
			if(Checkfoe.Checked)
				Panel1.Visible=true;
			else
				Panel1.Visible=false;
		}

		//*******************
		/// <summary>
		/// This function is used to Transfer from available customer to assigned.
		/// </summary>
		public void transfer1()
		{
			//countNo1=1;
			//countNo2=1;
				
			try
			{
				while(Listprodavail.SelectedItem.Selected)
				{
					Listprodassign.Items.Add(Listprodavail.SelectedItem.Value);  
					Listprodavail.Items.Remove(Listprodavail.SelectedItem.Value);
					Flag=1;
				}
				//CreateLogFiles.ErrorLog("Form:Schemediscount.aspx,Method:btnIn_Click"+"  userid "+uid);
			}
			catch(Exception)
			{
				if(Flag==0)
					MessageBox.Show("Please Select The Product To Move");
				//MessageBox.Show("Please Select Product");
				//CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnIn_Click"+"  EXCCEPTION "+ex.Message+"  userid  "+uid);
			}
		}
		//********************
		
		/// <summary>
		/// This is used to transfer the selected product.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btngo_Click(object sender, System.EventArgs e)
		{
			transfer1();
		}
		//**********************
		
		/// <summary>
		/// This is used to come back from assigned to available listbox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnback_Click(object sender, System.EventArgs e)
		{
			//countNo2=1;
			//countNo1=1;
			//string empRemove="";
			//string empSelect="";
			try
			{
				//EmployeeClass obj=new EmployeeClass();	
				//Listprodavail.Items.Add(Listprodassign.SelectedItem.Value);   
				//empRemove=Listprodassign.SelectedItem.Value;
				//empSelect=Listprodassign.SelectedItem.Value;
				//Listprodassign.Items.Remove(Listprodassign.SelectedItem.Value);
				//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:buttonout_Click"+"  Selected Employee is "+empSelect+"  empRemove  "+empRemove+"   userid "+uid);
				while(Listprodassign.SelectedItem.Selected)
				{
					Listprodavail.Items.Add(Listprodassign.SelectedItem.Value);
					Listprodassign.Items.Remove(Listprodassign.SelectedItem.Value);
					Flag=1;
				}
			}
			catch(Exception)
			{
				if(Flag==0)
					MessageBox.Show("Please Select The Product To Move");
				//CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:buttonout_Click  EXCEPTION: "+ ex.Message  + "  User  "+uid);
			}	
		}

		/// <summary>
		/// This is used to transfer/retrieve between listboxes with all customer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnall_Click(object sender, System.EventArgs e)
		{
			//countNo2=1;
			//countNo1=1;
			if(btnall.Text.Trim().Equals(">>"))
			{
				try
				{
					btnall.Text="<<";
					foreach(System.Web.UI.WebControls.ListItem lst in Listprodavail.Items)
						Listprodassign.Items.Add(lst);
					Listprodavail.Items.Clear();
					//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:btnOut_Click"+uid);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnall_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
					//	CreateLogFiles.ErrorLog("Form:Shift.aspx,Method:cmdrpt_Click"+ ex.Message+"EXCEPTION  "+uid);
				}
			}
			else
			{
				try
				{
					btnall.Text=">>";
					foreach(System.Web.UI.WebControls.ListItem lst in Listprodassign.Items)
						Listprodavail.Items.Add(lst);
					Listprodassign.Items.Clear();
					//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:btnOut_Click"+uid);
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnall_Click EXCEPTION "+ ex.Message  + "  User  "+uid);
					//	CreateLogFiles.ErrorLog("Form:Shift_Asignment.aspx,Method:btnOut_Click"+ex.Message+"EXCEPTION  "+uid);
				}
			}
		}

		/// <summary>
		/// This method is used to save the record.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnsave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ListEmpAssigned.Items.Count==0)
				{
					MessageBox.Show("Customer Assigned List Not Empty");
					return;
				}
				InventoryClass obj =new InventoryClass();
				SqlDataReader SqlDtr=null;
				SqlDataReader SqlDtr1=null;
				obj.foid=lblschid.Text;
				if(txtschname.Text.Equals(""))
					obj.discription="";
				else
					obj.discription=txtschname.Text.ToString();
				obj.custtype=DropShiftID.SelectedItem.Text.ToString();
				if(txtrs.Text.Equals(""))
					obj.discount="";
				else
					obj.discount=txtrs.Text.ToString();
				obj.discounttype=dropdiscount.SelectedItem.Text.ToString();
                obj.dateto = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString())); 
				obj.datefrom= System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					if(Checkfoe.Checked)
					{
						int k=Listprodassign.Items.Count;
						if(k==0)
						{
							MessageBox.Show("Please Select The Product");
							return ;
						}
					}
				}
				
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					if(Checkfoe.Checked)
					{	
						for(int j=0;j<Listprodassign.Items.Count;++j)
						{
							ListEmpAssigned.SelectedIndex =i;
							string pname = ListEmpAssigned.SelectedItem.Value; 
							string[] arr1=pname.Split(new char[]{':'},pname.Length);  
							string sql1="select cust_ID from customer where cust_Name='"+arr1[0]+"' and cust_Type='"+arr1[1]+"'";
							dbobj.SelectQuery(sql1,ref SqlDtr);
							while(SqlDtr.Read ())
							{
								obj.cust_id=SqlDtr.GetValue(0).ToString();
							}
							//							obj.Deletefoecust();
							//*****************
							Listprodassign.SelectedIndex =j;
							string prodname = Listprodassign.SelectedItem.Value; 
							string[] arr2=prodname.Split(new char[]{':'},prodname.Length);  
							string sql2="select prod_ID from products where prod_Name='"+arr2[0]+"' and pack_Type='"+arr2[1]+"'";
							dbobj.SelectQuery(sql2,ref SqlDtr1);
							while(SqlDtr1.Read ())
							{
								obj.prod_id=SqlDtr1.GetValue(0).ToString();
							}
							obj.InsertFOEdiscountEntry();
							SqlDtr1.Close();
							//*******************
						}
					}
					else
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value; 
						string[] arr1=pname.Split(new char[]{':'},pname.Length);  
						string sql1="select cust_ID from customer where cust_Name='"+arr1[0]+"' and cust_Type='"+arr1[1]+"'";
						dbobj.SelectQuery(sql1,ref SqlDtr);
						while(SqlDtr.Read ())
						{
							obj.cust_id=SqlDtr.GetValue(0).ToString();
							//obj.InsertFOEdiscount();
						}
						obj.prod_id="0";
						obj.InsertFOEdiscountEntry();
						SqlDtr.Close();
					}
				}
				MessageBox.Show("F-OE discount Saved"); 
				Clear();
				GetNextschemeID();
				getCustandProd();
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnSave_Click");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:fleet/oe discountEntry.aspx,Method:btnSave_Click EXCEPTION "+ex.Message   + "  User  "+uid );	
			}
		}
	}
}