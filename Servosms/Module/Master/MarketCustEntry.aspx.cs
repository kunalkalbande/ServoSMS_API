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
using System.Data.SqlClient;
using RMG;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for MarketCustEntry.
	/// </summary>
	public partial class MarketCustEntry : System.Web.UI.Page
	{
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
			try
			{
				uid=(Session["User_Name"].ToString ());
				if(!Page.IsPostBack)
				{
					fillID();
					btnselect.Visible=false;
					btndelete.Enabled=false;
					btnadd.Enabled=true;
					btnedit.Enabled=true;
					droptype.SelectedIndex=0;
					dropregcust.SelectedIndex=0;

					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					SqlDataReader SqlDtr1; 
					cmd=new SqlCommand("select * from CustomerType order by CustomerTypeName",con);
					SqlDtr1=cmd.ExecuteReader();
				
					droptype.Items.Clear();
					droptype.Items.Add("Select");
					if(SqlDtr1.HasRows)
					{
						while(SqlDtr1.Read())
						{
							droptype.Items.Add(SqlDtr1.GetValue(1).ToString());
						}
					}
					con.Close();
					SqlDtr1.Close();
					cmd.Dispose();
					con.Open ();
					//SqlDataReader SqlDtr1; 
					cmd=new SqlCommand("select city from Beat_Master order by city",con);
					SqlDtr1=cmd.ExecuteReader();
				
					dropplace.Items.Clear();
					dropplace.Items.Add("Select");
					if(SqlDtr1.HasRows)
					{
						while(SqlDtr1.Read())
						{
							dropplace.Items.Add(SqlDtr1.GetValue(0).ToString());
						}
					}
					con.Close();
					SqlDtr1.Close();
					cmd.Dispose();
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="9";
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
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					if(Add_Flag=="0")
						btnadd.Enabled=false;
					if(Edit_Flag=="0")
						btnedit.Enabled=false;
					if(Del_Flag=="0")
						btndelete.Enabled=false;
					#endregion
					CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:pageload, Userid="+ uid);
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:pageload"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
			}
		}

		/// <summary>
		/// This is used to generate nextID auto.
		/// </summary>
		private void fillID()
		{
			SqlConnection con;
			SqlCommand cmd;

			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				
				cmd=new SqlCommand("select max(mcid)+1 from marketCustomerentry1",con);
				SqlDtr=cmd.ExecuteReader();
				
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						txtid.Text=SqlDtr.GetValue(0).ToString ();
						if(txtid.Text.Trim().Equals(""))
							txtid.Text="1";
					}
				}
				SqlDtr.Close (); 
				con.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:FillID"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.Message);
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
		/// This method is used to Save the Market Customer information in the database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnadd_Click(object sender, System.EventArgs e)
		{
			
			//				Response.Write("insert into marketCustomerentry values("+txtid.Text.Trim()+",'"+
			//					txtfname.Text.ToString().Trim()+"','"+
			//					droptype.SelectedItem.Text.ToString().Trim()+"','"+
			//					txtcontact.Text.ToString().Trim()+"',"+
			//					txttelno.Text.Trim()+",'"+
			//					txtdetail.Text.ToString().Trim()+"','"+
			//					dropregcust.SelectedItem.Text.ToString().Trim()+"',"+
			//					txtpot.Text.Trim()+","+
			//					txtservo.Text.Trim()+","+
			//					txtcastrol.Text.Trim()+","+
			//					txtshell.Text.Trim()+","+
			//					txtbpcl.Text.Trim()+","+
			//					txtveedol.Text.Trim()+","+
			//					txtelf.Text.Trim()+","+
			//					txthpcl.Text.Trim()+","+
			//					txtpennzoil.Text.Trim()+","+
			//					txtspurious.Text.Trim()+","+
			//					txtother.Text.Trim()+")");
			
			//Response.Write("insert into marketcustomerentry values("+txtid.Text.ToString()+",'"+txtfname.Text.ToString()+"','"+droptype.SelectedItem.Text.ToString()+"','"+txtcontact.Text.ToString()+"',"+txttelno.Text.ToString()+",'"+txtdetail.Text.ToString()+"','"+dropregcust.SelectedItem.Text.ToString()+"',"+txtpot.Text.ToString()+","+txtservo.Text.ToString()+","+txtcastrol.Text.ToString()+","+txtshell.Text.ToString()+","+txtbpcl.Text.ToString()+","+txtveedol.Text.ToString()+","+txtelf.Text.ToString()+","+txthpcl.Text.ToString()+","+txtpennzoil.Text.ToString()+","+txtspurious.Text.ToString()+","+txtother.Text.ToString()+")");
			//MessageBox.Show("hello");
			//MessageBox.Show("insert into marketcustomerentry values("+txtid.Text.ToString()+",'"+txtfname.Text.ToString()+"','"+droptype.SelectedItem.Text.ToString()+"','"+txtcontact.Text.ToString()+"',"+txttelno.Text.ToString()+",'"+txtdetail.Text.ToString()+"','"+dropregcust.SelectedItem.Text.ToString()+"',"+txtpot.Text.ToString()+","+txtservo.Text.ToString()+","+txtcastrol.Text.ToString()+","+txtshell.Text.ToString()+","+txtbpcl.Text.ToString()+","+txtveedol.Text.ToString()+","+txtelf.Text.ToString()+","+txthpcl.Text.ToString()+","+txtpennzoil.Text.ToString()+","+txtspurious.Text.ToString()+","+txtother.Text.ToString()+")");
			
			SqlCommand cmdinsert;
			SqlConnection constr;
			constr=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			constr.Open ();
			
			try
			{
				if(txtfname.Text.Trim().ToString().Equals(""))
				{
					MessageBox.Show("Please select Firm name");
					return;
				}
				cmdinsert=new SqlCommand("insert into marketCustomerentry1 values("+txtid.Text.Trim()+",'"+
					txtfname.Text.ToString().Trim()+"','"+
					droptype.SelectedItem.Text.ToString().Trim()+"','"+
					txtcontact.Text.ToString().Trim()+"','"+
					txttelno.Text.Trim().ToString()+"','"+
					txtdetail.Text.ToString().Trim()+"','"+
					dropregcust.SelectedItem.Text.ToString().Trim()+"','"+
					txtpot.Text.Trim().ToString()+"','"+
					txtservo.Text.Trim().ToString()+"','"+
					txtcastrol.Text.Trim().ToString()+"','"+
					txtshell.Text.Trim().ToString()+"','"+
					txtbpcl.Text.Trim().ToString()+"','"+
					txtveedol.Text.Trim().ToString()+"','"+
					txtelf.Text.Trim().ToString()+"','"+
					txthpcl.Text.Trim().ToString()+"','"+
					txtpennzoil.Text.Trim().ToString()+"','"+
					txtspurious.Text.Trim().ToString()+"','"+
					txtother.Text.Trim().ToString()+"','"+
					dropplace.SelectedItem.Text.Trim()+"')", constr);
				cmdinsert.ExecuteNonQuery();
				MessageBox.Show("Market Customer Entry Saved");
				//cmdinsert.Dispose();
				constr.Close();
				clear();
				droptype.SelectedIndex=0;
				dropregcust.SelectedIndex=0;
				fillID();
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnadd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnedit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btndelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnAdd, Userid= "+uid);
			}
			catch(Exception  ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnAdd"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.StackTrace);
			}
		}

		/// <summary>
		/// This is used to clear form.
		/// </summary>
		private void clear()
		{
			txtfname.Text="";
			droptype.SelectedIndex=0;
			txtcontact.Text="";
			txttelno.Text="";
			txtdetail.Text="";
			dropregcust.SelectedIndex=0;
			txtpot.Text="";
			txtservo.Text="";
			txtcastrol.Text="";
			txtshell.Text="";
			txtbpcl.Text="";
			txtveedol.Text="";
			txtelf.Text="";
			txthpcl.Text="";
			txtpennzoil.Text="";
			txtspurious.Text="";
			txtother.Text="";
			dropplace.SelectedIndex=0;
		}
		
		/// <summary>
		/// This method is used to update the Market Customer information in edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnedit_Click(object sender, System.EventArgs e)
		{
			btnselect.Visible=true;
			txtid.Visible=false;
			//			droptype.SelectedIndex=0;
			//			dropregcust.SelectedIndex=0;
			//clear();
			if(btnedit.Text.Trim().Equals("UPDATE"))
			{
				SqlCommand cmdinsert;
				SqlConnection constr;
				constr=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				constr.Open ();
				try
				{
					cmdinsert=new SqlCommand("update marketCustomerentry1 set 	firmname='"+txtfname.Text.ToString().Trim()+
						"',type='"+droptype.SelectedItem.Text.ToString().Trim()+
						"',contactper='"+txtcontact.Text.ToString().Trim()+
						"',teleno='"+txttelno.Text.Trim().ToString()+
						"',specificdet='"+txtdetail.Text.ToString().Trim()+
						"',regcustomer='"+dropregcust.SelectedItem.Text.ToString().Trim()+
						"',potential='"+txtpot.Text.Trim().ToString()+
						"',servo='"+txtservo.Text.Trim().ToString()+
						"',castrol='"+txtcastrol.Text.Trim().ToString()+
						"',shell='"+txtshell.Text.Trim().ToString()+
						"',bpcl='"+txtbpcl.Text.Trim().ToString()+
						"',veedol='"+txtveedol.Text.Trim().ToString()+
						"',elf='"+txtelf.Text.Trim().ToString()+
						"',hpcl='"+txthpcl.Text.Trim().ToString()+
						"',pennzoil='"+txtpennzoil.Text.Trim().ToString()+
						"',spurious='"+txtspurious.Text.Trim().ToString()+
						"',others='"+txtother.Text.Trim().ToString()+
						"',place='"+dropplace.SelectedItem.Text.Trim()+"' where mcid='"+dropid.SelectedItem.Text.ToString()+"'", constr);
					cmdinsert.ExecuteNonQuery();
					MessageBox.Show("Market Customer Entry Updated");
					btnedit.Text="EDIT";
					btndelete.Enabled=false;
					btnadd.Enabled=true;
					btnselect.Visible=false;
					//cmdinsert.Dispose();
					constr.Close();
					droptype.SelectedIndex=0;
					dropregcust.SelectedIndex=0;
					txtid.Visible=true;
					dropid.Visible=false;
					clear();
					fillID();
					Object Add_Flag=Cache["Add"];
					Object Edit_Flag=Cache["Edit"];
					Object Del_Flag=Cache["Del"];
					if(System.Convert.ToString(Add_Flag)=="0")
					{
						btnadd.Enabled=false;
					}
					if(System.Convert.ToString(Edit_Flag)=="0")
						btnedit.Enabled=false;
					if(System.Convert.ToString(Del_Flag)=="0")
						btndelete.Enabled=false;		
					CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnEdit, Userid= "+uid);
				}
				catch(Exception  ex)
				{
					CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnEdit"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
					MessageBox.Show(ex.StackTrace);
				}
			}
		}

		/// <summary>
		/// This method is used to fatch the information according to select the Market Custoemr ID 
		/// from dropdownlist in edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			btnedit.Text="UPDATE";
			btndelete.Enabled=true;
			clear();
			SqlConnection con;
			SqlCommand cmd;
			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				cmd=new SqlCommand("select * from marketCustomerentry1 WHERE mcid='"+dropid.SelectedItem.Text.Trim().ToString()+"'",con);
				SqlDtr=cmd.ExecuteReader();
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						txtfname.Text=SqlDtr.GetValue(1).ToString();
						droptype.SelectedIndex=(droptype.Items.IndexOf((droptype.Items.FindByValue(SqlDtr.GetValue(2).ToString()))));
						dropplace.SelectedIndex=(dropplace.Items.IndexOf((dropplace.Items.FindByValue(SqlDtr.GetValue(18).ToString()))));
						txtcontact.Text=SqlDtr.GetValue(3).ToString();
						txttelno.Text=SqlDtr.GetValue(4).ToString();
						txtdetail.Text=SqlDtr.GetValue(5).ToString();
						//Droptype2.SelectedIndex=(Droptype2.Items.IndexOf((Droptype2.Items.FindByValue(SqlDtr4.GetValue(13).ToString()))));
						dropregcust.SelectedIndex=(dropregcust.Items.IndexOf((dropregcust.Items.FindByValue(SqlDtr.GetValue(6).ToString()))));
						txtpot.Text=SqlDtr.GetValue(7).ToString();
						txtservo.Text=SqlDtr.GetValue(8).ToString();
						txtcastrol.Text=SqlDtr.GetValue(9).ToString();
						txtshell.Text=SqlDtr.GetValue(10).ToString();
						txtbpcl.Text=SqlDtr.GetValue(11).ToString();
						txtveedol.Text=SqlDtr.GetValue(12).ToString();
						txtelf.Text=SqlDtr.GetValue(13).ToString();
						txthpcl.Text=SqlDtr.GetValue(14).ToString();
						txtpennzoil.Text=SqlDtr.GetValue(15).ToString();
						txtspurious.Text=SqlDtr.GetValue(16).ToString();
						txtother.Text=SqlDtr.GetValue(17).ToString();
					}
				}
				dropid.Visible=true;
				btnselect.Visible=false;
				SqlDtr.Close (); 
				con.Close();
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnadd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnedit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btndelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:dropid_SelectedIndexChange, Userid= "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:dropid_SelectedIndexChange"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// This method is used to fatch the all market customer id from database table 
		/// and fill this value into the dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnselect_Click(object sender, System.EventArgs e)
		{
			SqlConnection con;
			SqlCommand cmd;
			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				
				cmd=new SqlCommand("select mcid from marketCustomerentry1",con);
				SqlDtr=cmd.ExecuteReader();
				dropid.Items.Clear();
				dropid.Items.Add("select");
				
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						dropid.Items.Add(SqlDtr.GetValue(0).ToString ());
					}
				}
				
				dropid.Visible=true;
				btnselect.Visible=false;
				btnadd.Enabled=false;
				SqlDtr.Close (); 
				con.Close();
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnadd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnedit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btndelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnSelect, Userid= "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnSelect"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// This method is used to delete the particular record according to select the value
		/// from dropdownlist on edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btndelete_Click(object sender, System.EventArgs e)
		{
			SqlConnection con;
			SqlCommand cmd;
			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				cmd=new SqlCommand("delete from marketCustomerentry1 where mcid='"+dropid.SelectedItem.Text.ToString()+"'",con);
				cmd.ExecuteNonQuery();
				MessageBox.Show("Deleted Successfully");
				fillID();
				clear();
				droptype.SelectedIndex=0;
				dropregcust.SelectedIndex=0;
				txtid.Visible=true;
				dropid.Visible=false;
				btnselect.Visible=false;
				btnadd.Enabled=true;
				btndelete.Enabled=false;
				btnedit.Text="EDIT";
				con.Close();
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnadd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnedit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btndelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnDelete, Userid= "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MarketCustEntry.aspx,Method:btnDelete"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.Message);
			}
		}

		protected void droptype_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void dropregcust_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtdetail_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}