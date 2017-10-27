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
	/// Summary description for ServoStockistDiscountEntry.
	/// </summary>
	public partial class ServoStockistDiscountEntry : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;
	
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
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid);
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
				if(View_Flag=="0")
				{
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				if(Add_Flag=="0")
					btnSubmit.Enabled=false;
				if(Edit_Flag=="0")
					btschid.Enabled=false;
				#endregion

				try
				{
					dropschid.Visible=false;
					btnupdate.Visible=false;
					GetNextschemeID();
					InventoryClass obj= new InventoryClass();
					//SqlDataReader SqlDtr1;
					FillList();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:pageload"+ ex.Message+"  EXCEPTION  "+uid); 
				}
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
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
				dbobj.SelectQuery("Select max(sch_id)+1 from  stktSchDiscount",ref SqlDtr);
				if(SqlDtr.Read())
				{
					if(SqlDtr.GetValue(0).ToString()!=null && SqlDtr.GetValue(0).ToString()!="")
						lblschid.Text = SqlDtr.GetValue(0).ToString();
					else
						lblschid.Text="600001";
				}
				else
					lblschid.Text="600001";
				SqlDtr.Close();
				#endregion
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Class:PartiesClass.cs: Method:GetNextschemeID().  EXCEPTION "+ ex.Message  + "  User  "+uid);
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
				string schname="";
				InventoryClass obj =new InventoryClass();
				//string sql;
				SqlDataReader SqlDtr=null;
				if(ListEmpAssigned.Items.Count!=0)
				{
					for(int i=0;i<ListEmpAssigned.Items.Count;++i)
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value; 
						string[] arr1=pname.Split(new char[]{':'},pname.Length);
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr;
						//string sname=DropSchType.SelectedItem.Text;
						
						string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
						rdr = obj1.GetRecordSet (sql1);
						if(rdr.Read ())
						{
							/*02.06.09if(sname.IndexOf("Free")>0)
								schname="Free Scheme";
							else if(sname.IndexOf("LTR&%")>0)
								schname="LTR&% Scheme";
							else if(sname.IndexOf("LTRSP")>0)
								schname="LTRSP Scheme";
							else
								schname="LTR Scheme";*/
							
							// Coment By Vikas 1.1.2013 schname="Primary(LTR&% Scheme)";
							
							if(DropType.SelectedIndex==1)
								schname="Primary(LTR&% Scheme)";
							else if(DropType.SelectedIndex==2)
								schname=DropType.SelectedItem.Value.ToString();

							sql1="select * from StktSchDiscount where Prodid='"+rdr["Prod_ID"].ToString()+"' and schtype like '%"+schname+"%' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
							dbobj.SelectQuery(sql1,ref SqlDtr);
							if(SqlDtr.Read())
							{
								MessageBox.Show("'"+pname+"'"+" Allready Exist");
								return;
							}
						}
						rdr.Close();
					}
				}
				else
				{
					MessageBox.Show(" Please Select At Least One Product ");
					return;
				}
				
				//obj.schtype=DropSchType.SelectedItem.Text.ToString();
				//Coment by Vikas 1/1/2013 obj.schtype="Primary(LTR&% Scheme)";
				obj.schtype=schname;
				obj.schid=lblschid.Text;
				if(txtschname.Text.Equals(""))
					obj.schname="";
				else
					obj.schname=txtschname.Text.ToString();
//				if(DropShiftID.SelectedItem.Text.Equals("Primary(Free Scheme)") || DropShiftID.SelectedItem.Text.Equals("Secondry(Free Scheme)"))
//				{
//					string pname1=dropfoc.SelectedItem.Text.ToString();
//
//					string[] arr2=pname1.Split(new char[]{':'},pname1.Length);  
//					sql="select Prod_ID from Products where Prod_Name='"+arr2[0]+"' and Pack_Type='"+arr2[1]+"'";
//					SqlDtr = obj.GetRecordSet (sql);
//					while(SqlDtr.Read ())
//					{
//						obj.schprodid=SqlDtr.GetValue(0).ToString();
//					}
//					SqlDtr.Close();
//				}
//				else
//				{
//					obj.schprodid="";
//				}
				
//				if(txtevery.Text.Equals(""))
//					obj.onevery="";
//				else
//					obj.onevery=txtevery.Text.ToString();
//				if(txtfree.Text.Equals(""))
//					obj.freepack="";
//				else
//					obj.freepack=txtfree.Text.ToString();
				if(txtSchDiscount.Text.Equals(""))
					obj.discount="";
				else
					obj.discount=txtSchDiscount.Text.ToString();
				
				obj.discounttype=DropSchDiscount.SelectedItem.Text;
				obj.dateto=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString()));
				obj.datefrom=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString()));			
				//obj.Type=DropType.SelectedItem.Text;
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					ListEmpAssigned.SelectedIndex =i;
					string pname = ListEmpAssigned.SelectedItem.Value; 
					string[] arr1=pname.Split(new char[]{':'},pname.Length);  
					string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
					dbobj.SelectQuery(sql1,ref SqlDtr);
					while(SqlDtr.Read ())
					{
						obj.prodid=SqlDtr.GetValue(0).ToString();
						obj.InsertStockiestSchemediscount();
					}
				}
				SqlDtr.Close();
				MessageBox.Show("Stockiest Scheme Saved");
				Clear();
				FillList();
				GetNextschemeID();
				CreateLogFiles.ErrorLog("Form:ServoStockiestStockiestDiscountEntry.aspx,Method:btnSubmit_Click  Stockiest Discount Entry Saved, User : "+uid);
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
			txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
			txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
			ListEmpAssigned.Items.Clear();
			//txtevery.Text="";
			txtSchDiscount.Text="";
			//txtfree.Text="";
			txtschname.Text="";
			GetNextschemeID();
			DropType.SelectedIndex=0;
			//			Panel1.Visible=false;
			//			Panel2.Visible=false;
			DropSchDiscount.SelectedIndex=0;
			//DropSchType.SelectedIndex=0;
		}

		/// <summary>
		/// this is used to transfer the selected product from  assigned to available.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnout_Click(object sender, System.EventArgs e)
		{
			transfer1();
		} 

		/// <summary>
		/// this is used to transfer the selected product from available list to assigned list.
		/// </summary>
		public void transfer()
		{
			try
			{
				while(ListEmpAvailable.SelectedItem.Selected)
				{
					ListEmpAssigned.Items.Add(ListEmpAvailable.SelectedItem.Value);  
					ListEmpAvailable.Items.Remove(ListEmpAvailable.SelectedItem.Value);
				}
			}
			catch(Exception)
			{
			}
		}

		/// <summary>
		/// this is used to transfer the selected product from assigned list to available list.
		/// </summary>
		public void transfer1()
		{
			try
			{
				while(ListEmpAssigned.SelectedItem.Selected)
				{
					ListEmpAvailable.Items.Add(ListEmpAssigned.SelectedItem.Value);  
					ListEmpAssigned.Items.Remove(ListEmpAssigned.SelectedItem.Value);
				}
			}
			catch(Exception)
			{
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

		protected void btn1_Click(object sender, System.EventArgs e)
		{
			if(btn1.Text.Trim().Equals(">>"))
			{
				try
				{
					btn1.Text="<<";
					foreach(System.Web.UI.WebControls.ListItem lst in ListEmpAvailable.Items)
						ListEmpAssigned.Items.Add(lst);
					ListEmpAvailable.Items.Clear();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:cmdrpt_Click"+ ex.Message);
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
				}
				catch(Exception ex)
				{	
					CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:btnOut_Click  EXCEPTION "+ ex.Message  + "  User  "+uid);
				}
			}
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
				SqlDataReader SqlDtr=null;
				//by vikas 02.06.09 obj.schtype=DropSchType.SelectedItem.Text.ToString();
				//Coment By Vikas 1.1.2012 obj.schtype="Primary(LTR&% Scheme)";
				
				obj.schtype=DropType.SelectedValue.ToString();

				string scheme=dropschid.SelectedItem.Text.Trim().ToString();
				string[] schid=scheme.Split(new char[]{':'},scheme.Length);  
				obj.schid=schid[0];
				if(txtschname.Text.Equals(""))
					obj.schname="";
				else
					obj.schname=txtschname.Text.ToString();
				if(txtSchDiscount.Text.Equals(""))
					obj.discount="";
				else
					obj.discount=txtSchDiscount.Text.ToString();
				obj.discounttype=DropSchDiscount.SelectedItem.Text;
				obj.dateto=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text.ToString()));
				obj.datefrom=System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text.ToString()));			
				if(ListEmpAssigned.Items.Count!=0)
				{
					for(int i=0;i<ListEmpAssigned.Items.Count;++i)
					{
						ListEmpAssigned.SelectedIndex =i;
						string pname = ListEmpAssigned.SelectedItem.Value; 
						string[] arr1=pname.Split(new char[]{':'},pname.Length);
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr,rdr1=null;
						//02.06.09string sname=DropSchType.SelectedItem.Text;
						string schname="";
						string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
						rdr = obj1.GetRecordSet (sql1);
						if(rdr.Read ())
						{
							sql1="select * from StktSchDiscount where Prodid='"+rdr["Prod_ID"].ToString()+"' and sch_id='"+schid[0]+"'";
							dbobj.SelectQuery(sql1,ref rdr1);
							if(rdr1.Read())
							{
							}
							else
							{
								/*if(sname.IndexOf("Free")>0)
									schname="Free Scheme";
								else if(sname.IndexOf("LTR&%")>0)
									schname="LTR&% Scheme";
								else if(sname.IndexOf("LTRSP")>0)
									schname="LTRSP Scheme";
								else
									schname="LTR Scheme";*/
								
								//Coment By Vikas 1.1.2013 schname="Primary(LTR&% Scheme)";
								
								//if(DropType.SelectedIndex==1)
								//	schname="Primary(LTR&% Scheme)";
								//else if(DropType.SelectedIndex==2)
									schname=DropType.SelectedItem.Value.ToString();

								sql1="select * from StktSchDiscount where Prodid='"+rdr["Prod_ID"].ToString()+"'and SchType like '%"+schname+"%' and (cast(floor(cast(datefrom as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text))  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateFrom.Text)) +"' and '"+ GenUtil.str2MMDDYYYY(GenUtil.trimDate(txtDateTo.Text)) +"')";
								dbobj.SelectQuery(sql1,ref SqlDtr);
								if(SqlDtr.Read())
								{
									MessageBox.Show("'"+pname+"'"+" Allready Exist");
									return;
								}
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
				SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlCon.Open();
				SqlCommand cmd;
				cmd=new SqlCommand("delete from StktschDiscount where sch_id='"+schid[0]+"'",SqlCon);
				cmd.ExecuteNonQuery();
				SqlCon.Close();
				cmd.Dispose();
				for(int i=0;i<ListEmpAssigned.Items.Count;++i)
				{
					ListEmpAssigned.SelectedIndex =i;
					string pname = ListEmpAssigned.SelectedItem.Value; 
					string[] arr1=pname.Split(new char[]{':'},pname.Length);  
					string sql1="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
					dbobj.SelectQuery(sql1,ref SqlDtr);
					while(SqlDtr.Read ())
					{
						obj.prodid=SqlDtr.GetValue(0).ToString();
						obj.InsertStockiestSchemediscount();
					}
						
				}
				MessageBox.Show("Stockiest Discount Updated"); 
				Clear();
				FillList();
				GetNextschemeID();
				dropschid.Visible=false;
				btnupdate.Visible=false;
				lblschid.Visible=true;
				btnSubmit.Visible=true;
				btnSubmit.Enabled=true;
				btschid.Visible=true;
				CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:btnupdate_Click  Stockiest Discount Entry Updated, User "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Schemediscount.aspx,Method:btnupdate_Click   EXCEPTION "+ ex.Message  + "  User  "+uid);	
			}
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
			ListEmpAssigned.Items.Clear();
			ListEmpAvailable.Enabled=true;
			//DropSchType.SelectedIndex=0;
			dropschid.Visible=true;
			btschid.Visible=false;
			btnSubmit.Enabled=false;
			btnupdate.Visible=true;
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr1;
			string sql;

			#region Fetch the All Invoice Number and fill in Combo
			dropschid.Items.Clear();  
			dropschid.Items.Add("Select"); 
			sql="select distinct sch_id ,schname from StktSchDiscount order by sch_id";
			SqlDtr1=obj.GetRecordSet(sql);
			while(SqlDtr1.Read())
			{
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
			SqlDataReader rdr1=null;
			try
			{
				if(dropschid.SelectedIndex!=0)
				{
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					SqlDataReader SqlDtr; 
					string scheme=dropschid.SelectedItem.Text.Trim().ToString();
					string[] schid=scheme.Split(new char[]{':'},scheme.Length);  
					cmd=new SqlCommand("select * from StktSchDiscount WHERE sch_id='"+schid[0]+"'",con);
					SqlDtr=cmd.ExecuteReader();
					ListEmpAssigned.Items.Clear();
					if(SqlDtr.HasRows )
					{
						while(SqlDtr.Read ())
						{
							//02.06.09 DropSchType.SelectedIndex=(DropSchType.Items.IndexOf((DropSchType.Items.FindByValue(SqlDtr.GetValue(1).ToString()))));
							/*********Coment By Vikas 1.1.2013****************************/
							
							if(!SqlDtr.GetValue(1).Equals("") || !SqlDtr.GetValue(1).Equals("NULL"))
								DropType.SelectedIndex= DropType.Items.IndexOf(DropType.Items.FindByValue(SqlDtr.GetValue(1).ToString()));
							
							/*************************************/
							if(SqlDtr.GetValue(3).Equals("")||SqlDtr.GetValue(1).Equals("NULL"))
								txtschname.Text="";
							else
								txtschname.Text=SqlDtr.GetValue(3).ToString();
							txtSchDiscount.Text=SqlDtr.GetValue(4).ToString();
							DropSchDiscount.SelectedIndex=(DropSchDiscount.Items.IndexOf((DropSchDiscount.Items.FindByValue(SqlDtr.GetValue(5).ToString()))));
							txtDateFrom.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(6).ToString()));
							txtDateTo.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(7).ToString()));
							dbobj.SelectQuery("select prod_name+':'+pack_type from products where prod_ID="+SqlDtr.GetValue(2).ToString()+" ", ref rdr1);
							if(rdr1.Read())
							{
								ListEmpAssigned.Items.Add(rdr1.GetValue(0).ToString());
							}
						}
					}
					dropschid.Visible=true;
					btschid.Visible=false;
					SqlDtr.Close (); 
					con.Close();
				}
				else
				{
					//02.06.09 DropSchType.SelectedIndex=0;
					txtschname.Text="";
					txtSchDiscount.Text="";
					DropSchDiscount.SelectedIndex=0;
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					ListEmpAssigned.Items.Clear();
				}
				CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:dropschid_SelectedIndexChange, Userid= "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockiestDiscountEntry.aspx,Method:dropschid_SelectedIndexChange"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
			}
		}
	}
}