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
using RMG;
using Servosms.Sysitem.Classes;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for CustomerMechanicEntry.
	/// </summary>
	public partial class CustomerMechanicEntry : System.Web.UI.Page
	{
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next Mechanic ID also.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString ());
				if(!Page.IsPostBack)
				{
					//btnselect.Visible=false;
					btnedit.Enabled=false;
					DropID.Visible=false;
					
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="4";
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
						return;
					}
					if(Add_Flag=="0")
						btnadd.Enabled=false;
					if(Edit_Flag=="0")
						btnedit.Enabled=false;
					if(Del_Flag=="0")
						btnDelete.Enabled=false;
					#endregion
					
					fillID();
					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					SqlDataReader SqlDtr; 
					cmd=new SqlCommand("select city from Beat_master order by city",con);
					SqlDtr=cmd.ExecuteReader();
					Dropcity1.Items.Clear();
					Dropcity2.Items.Clear();
					Dropcity3.Items.Clear();
					Dropcity4.Items.Clear();
					Dropcity5.Items.Clear();
					Dropcity6.Items.Clear();
					Dropcity7.Items.Clear();
					Dropcity8.Items.Clear();
					Dropcity9.Items.Clear();
					Dropcity10.Items.Clear();
				
					Dropcity1.Items.Add("select");
					Dropcity2.Items.Add("select");
					Dropcity3.Items.Add("select");
					Dropcity4.Items.Add("select");
					Dropcity5.Items.Add("select");
					Dropcity6.Items.Add("select");
					Dropcity7.Items.Add("select");
					Dropcity8.Items.Add("select");
					Dropcity9.Items.Add("select");
					Dropcity10.Items.Add("select");
					if(SqlDtr.HasRows )
					{
						while(SqlDtr.Read ())
						{
							Dropcity1.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity2.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity3.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity4.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity5.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity6.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity7.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity8.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity9.Items.Add(SqlDtr.GetValue(0).ToString ());
							Dropcity10.Items.Add(SqlDtr.GetValue(0).ToString ());
						}
					}
					SqlDtr.Close (); 
					con.Close();
					cmd.Dispose();
					#region Fatching the Customer Name from Customer table
					SqlCommand cmd3;
					SqlConnection con3;
					con3=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con3.Open ();
					SqlDataReader SqlDtr3; 
					//cmd3=new SqlCommand("select cust_name from customer order by Cust_Name",con3);
					cmd3=new SqlCommand("select cust_name from customer where (cust_type like'baz%' or cust_type like'ro%' or cust_type like'ksk%' or cust_type like'n-ksk%' or cust_type like'fleat%') order by cust_name",con3);
					SqlDtr3=cmd3.ExecuteReader();
					if(SqlDtr3.HasRows )
					{
						while(SqlDtr3.Read ())
						{
							DropCustomerName.Items.Add(SqlDtr3.GetValue(0).ToString ());
						}
					}
					SqlDtr3.Close (); 
					con3.Close();
					cmd3.Dispose();
					#endregion
					CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:Page_Load, userid  "+uid );
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:Page_Load,   EXCEPTION "+ex.Message+"  userid  "+uid );
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
		/// This is used to generate next Mechanic ID  from CustomerMechanicEntry table.
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
				cmd=new SqlCommand("select max(customermechid)+1 from Customermechanicentry",con);
				SqlDtr=cmd.ExecuteReader();
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						txtcustid.Text=SqlDtr.GetValue(0).ToString ();
						if(txtcustid.Text.Trim().Equals(""))
							txtcustid.Text="1";
					}
				}
				SqlDtr.Close (); 
				con.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:FillID"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// This method is used to generatet the next MCID from Machanic_Entry table.
		/// </summary>
		string count="";
		private void fillID1()
		{
			SqlConnection con;
			SqlCommand cmd;
			try
			{
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				cmd=new SqlCommand("select max(mccd)+1 from Machanic_Entry",con);
				SqlDtr=cmd.ExecuteReader();
				if(SqlDtr.HasRows )
				{
					while(SqlDtr.Read ())
					{
						count=SqlDtr.GetValue(0).ToString();
						if(count.Equals(""))
							count="1001";
					}
				}
				SqlDtr.Close (); 
				con.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:FillID1"+"  EXCEPTION "+ ex.Message+"Userid= "+uid);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// This method is used to Save the mechanic information in CustomerMechanicEntry and Machanic_Entry table 
		/// before you checked all dropdownlist and textboxes have proper value or not
		/// if textboxed have blank value then show the popup msg.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnadd_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropCustomerName.SelectedItem.Text.Trim().Equals("Select"))
				{
					MessageBox.Show("Please select Customer Name");
					return;
				}
				SqlCommand cmd;
				SqlConnection con;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				if(txtname1.Text=="" && txtname2.Text=="" && txtname3.Text=="" && txtname4.Text=="" && txtname5.Text=="" && txtname6.Text=="" && txtname7.Text=="" && txtname8.Text=="" && txtname9.Text=="" && txtname10.Text=="")
				{
					MessageBox.Show("Please Fill the Name,type and City");
					return;
				}
				else
				{
					if(txtname1.Text!="")
					{
						if(Droptype1.SelectedIndex==0 || Dropcity1.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname2.Text!="")
					{
						if(Droptype2.SelectedIndex==0 || Dropcity2.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname3.Text!="")
					{
						if(Droptype3.SelectedIndex==0 || Dropcity3.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname4.Text!="")
					{
						if(Droptype4.SelectedIndex==0 || Dropcity4.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname5.Text!="")
					{
						if(Droptype5.SelectedIndex==0 || Dropcity5.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname6.Text!="")
					{
						if(Droptype6.SelectedIndex==0 || Dropcity6.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname7.Text!="")
					{
						if(Droptype7.SelectedIndex==0 || Dropcity7.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname8.Text!="")
					{
						if(Droptype8.SelectedIndex==0 || Dropcity8.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname9.Text!="")
					{
						if(Droptype9.SelectedIndex==0 || Dropcity9.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
					if(txtname10.Text!="")
					{
						if(Droptype10.SelectedIndex==0 || Dropcity10.SelectedIndex==0)
						{
							MessageBox.Show("Please Select the Type and City");
							return;
						}
					}
				}
				
				//SqlDataReader SqlDtr; 
				cmd=new SqlCommand("insert into Customermechanicentry values("+txtcustid.Text.ToString()+",'"+DropCustomerName.SelectedItem.Text.ToString()+"','"+txtname1.Text.Trim().ToString()+"','"+txtname2.Text.Trim().ToString()+"','"+txtname3.Text.Trim().ToString()+"','"+txtname4.Text.Trim().ToString()+
					"','"+txtname5.Text.Trim().ToString()+"','"+txtname6.Text.Trim().ToString()+"','"+txtname7.Text.Trim().ToString()+"','"+txtname8.Text.Trim().ToString()+"','"+txtname9.Text.Trim().ToString()+"','"+txtname10.Text.Trim().ToString()+
					"','"+Droptype1.SelectedItem.Text.ToString()+"','"+Droptype2.SelectedItem.Text.ToString()+"','"+Droptype3.SelectedItem.Text.ToString()+
					"','"+Droptype4.SelectedItem.Text.ToString()+"','"+Droptype5.SelectedItem.Text.ToString()+"','"+Droptype6.SelectedItem.Text.ToString()+
					"','"+Droptype7.SelectedItem.Text.ToString()+"','"+Droptype8.SelectedItem.Text.ToString()+"','"+Droptype9.SelectedItem.Text.ToString()+
					"','"+Droptype10.SelectedItem.Text.ToString()+"','"+Dropcity1.SelectedItem.Text.ToString()+"','"+Dropcity2.SelectedItem.Text.ToString()+
					"','"+Dropcity3.SelectedItem.Text.ToString()+"','"+Dropcity4.SelectedItem.Text.ToString()+"','"+Dropcity5.SelectedItem.Text.ToString()+
					"','"+Dropcity6.SelectedItem.Text.ToString()+"','"+Dropcity7.SelectedItem.Text.ToString()+
					"','"+Dropcity8.SelectedItem.Text.ToString()+"','"+Dropcity9.SelectedItem.Text.ToString()+"','"+Dropcity10.SelectedItem.Text.ToString()+"')",con);
				cmd.ExecuteNonQuery();
				MessageBox.Show("Customer Mechanic Entry Saved");
				cmd.Dispose();
				con.Close();
			
				//****************************************************
				fillID1();
				con.Open();
				int c=System.Convert.ToInt32(count.ToString());
				if(txtname1.Text=="" && Droptype1.SelectedIndex==0 && Dropcity1.SelectedIndex==0)
				{}
				else
				{
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname1.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype1.SelectedItem.Text.ToString()+"','"+Dropcity1.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname2.Text=="" && Droptype2.SelectedIndex==0 && Dropcity2.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname2.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype2.SelectedItem.Text.ToString()+"','"+Dropcity2.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname3.Text=="" && Droptype3.SelectedIndex==0 && Dropcity3.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname3.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype3.SelectedItem.Text.ToString()+"','"+Dropcity3.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname4.Text=="" && Droptype4.SelectedIndex==0 && Dropcity4.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname4.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype4.SelectedItem.Text.ToString()+"','"+Dropcity4.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname5.Text=="" && Droptype5.SelectedIndex==0 && Dropcity5.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname5.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype5.SelectedItem.Text.ToString()+"','"+Dropcity5.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname6.Text=="" && Droptype6.SelectedIndex==0 && Dropcity6.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname6.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype6.SelectedItem.Text.ToString()+"','"+Dropcity6.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname7.Text=="" && Droptype7.SelectedIndex==0 && Dropcity7.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname7.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype7.SelectedItem.Text.ToString()+"','"+Dropcity7.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname8.Text=="" && Droptype8.SelectedIndex==0 && Dropcity8.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname8.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype8.SelectedItem.Text.ToString()+"','"+Dropcity8.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname9.Text=="" && Droptype9.SelectedIndex==0 && Dropcity9.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname9.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype9.SelectedItem.Text.ToString()+"','"+Dropcity9.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
					c++;
				}
				if(txtname10.Text=="" && Droptype10.SelectedIndex==0 && Dropcity10.SelectedIndex==0)
				{}
				else
				{
					con.Open();
					cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname10.Text.Trim()+"',"+txtcustid.Text.ToString().Trim()+",'"+Droptype10.SelectedItem.Text.ToString()+"','"+Dropcity10.SelectedItem.Text.ToString()+"')",con);
					cmd.ExecuteNonQuery();
					con.Close();
					cmd.Dispose();
				}
				//****************************************************
				fillID();
				clear();
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
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnAdd, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnAdd,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// This is used to clear the form.
		/// </summary>
		private void clear()
		{
			mccd1.Text="";
			mccd2.Text="";
			mccd3.Text="";
			mccd4.Text="";
			mccd5.Text="";
			mccd6.Text="";
			mccd7.Text="";
			mccd8.Text="";
			mccd9.Text="";
			mccd10.Text="";
			DropCustomerName.SelectedIndex=0;
			txtname1.Text="";
			txtname2.Text="";
			txtname3.Text="";
			txtname4.Text="";
			txtname5.Text="";
			txtname6.Text="";
			txtname7.Text="";
			txtname8.Text="";
			txtname9.Text="";
			txtname10.Text="";
			Droptype1.SelectedIndex=0;
			Droptype2.SelectedIndex=0;
			Droptype3.SelectedIndex=0;
			Droptype4.SelectedIndex=0;
			Droptype5.SelectedIndex=0;
			Droptype6.SelectedIndex=0;
			Droptype7.SelectedIndex=0;
			Droptype8.SelectedIndex=0;
			Droptype9.SelectedIndex=0;
			Droptype10.SelectedIndex=0;
			Dropcity1.SelectedIndex=0;
			Dropcity2.SelectedIndex=0;
			Dropcity3.SelectedIndex=0;
			Dropcity4.SelectedIndex=0;
			Dropcity5.SelectedIndex=0;
			Dropcity6.SelectedIndex=0;
			Dropcity7.SelectedIndex=0;
			Dropcity8.SelectedIndex=0;
			Dropcity9.SelectedIndex=0;
			Dropcity10.SelectedIndex=0;

		}

		/// <summary>
		/// This method is used to Save or Update the Mechanic information in CustomerMechanicEntry and 
		/// Machanic_Entry table before you checked all dropdownlist and textboxes have proper value or not
		/// if textboxed have blank value then show the popup msg.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnedit_Click(object sender, System.EventArgs e)
		{
			try
			{
				//btnselect.Visible=true;
				//txtcustid.Visible=false;
				//btnadd.Enabled=false;
				if(btnedit.Text.Trim().Equals("Update"))
				{
					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					//***************************************************
					mccd.Visible=false;
					mccd1.Visible=false;
					mccd2.Visible=false;
					mccd3.Visible=false;
					mccd4.Visible=false;
					mccd5.Visible=false;
					mccd6.Visible=false;
					mccd7.Visible=false;
					mccd8.Visible=false;
					mccd9.Visible=false;
					mccd10.Visible=false;
					
					if(txtname1.Text=="" && Droptype1.SelectedIndex==0 && Dropcity1.SelectedIndex==0 )
					{}
					else
					{
						if(mccd1.Text=="")
						{
							if(Droptype1.SelectedIndex==0 || Dropcity1.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname1.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype1.SelectedItem.Text.ToString()+"','"+Dropcity1.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname1.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype1.SelectedItem.Text.ToString()+"', place='"+Dropcity1.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd1.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname2.Text=="" && Droptype2.SelectedIndex==0 && Dropcity2.SelectedIndex==0 )
					{}
					else
					{
						if(mccd2.Text=="")
						{
							if(Droptype2.SelectedIndex==0 || Dropcity2.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname2.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype2.SelectedItem.Text.ToString()+"','"+Dropcity2.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname2.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype2.SelectedItem.Text.ToString()+"', place='"+Dropcity2.SelectedItem.Text.ToString()+"' where  custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd2.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname3.Text=="" && Droptype3.SelectedIndex==0 && Dropcity3.SelectedIndex==0 )
					{}
					else
					{
						if(mccd3.Text=="")
						{
							if(Droptype3.SelectedIndex==0 || Dropcity3.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname3.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype3.SelectedItem.Text.ToString()+"','"+Dropcity3.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname3.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype3.SelectedItem.Text.ToString()+"', place='"+Dropcity3.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd3.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname4.Text=="" && Droptype4.SelectedIndex==0 && Dropcity4.SelectedIndex==0 )
					{}
					else
					{
						if(mccd4.Text=="")
						{
							if(Droptype4.SelectedIndex==0 || Dropcity4.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname4.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype4.SelectedItem.Text.ToString()+"','"+Dropcity4.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname4.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype4.SelectedItem.Text.ToString()+"', place='"+Dropcity4.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd4.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname5.Text=="" && Droptype5.SelectedIndex==0 && Dropcity5.SelectedIndex==0 )
					{}
					else
					{
						if(mccd5.Text=="")
						{
							if(Droptype5.SelectedIndex==0 || Dropcity5.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname5.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype5.SelectedItem.Text.ToString()+"','"+Dropcity5.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname5.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype5.SelectedItem.Text.ToString()+"', place='"+Dropcity5.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd5.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname6.Text=="" && Droptype6.SelectedIndex==0 && Dropcity6.SelectedIndex==0 )
					{}
					else
					{
						if(mccd6.Text=="")
						{
							if(Droptype6.SelectedIndex==0 || Dropcity6.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname6.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype6.SelectedItem.Text.ToString()+"','"+Dropcity6.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname6.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype6.SelectedItem.Text.ToString()+"', place='"+Dropcity6.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd6.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname7.Text=="" && Droptype7.SelectedIndex==0 && Dropcity7.SelectedIndex==0 )
					{}
					else
					{
						if(mccd7.Text=="")
						{
							if(Droptype7.SelectedIndex==0 || Dropcity7.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname7.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype7.SelectedItem.Text.ToString()+"','"+Dropcity7.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname7.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype7.SelectedItem.Text.ToString()+"', place='"+Dropcity1.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd7.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname8.Text=="" && Droptype8.SelectedIndex==0 && Dropcity8.SelectedIndex==0 )
					{}
					else
					{
						if(mccd8.Text=="")
						{
							if(Droptype8.SelectedIndex==0 || Dropcity8.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname8.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype8.SelectedItem.Text.ToString()+"','"+Dropcity8.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname8.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype8.SelectedItem.Text.ToString()+"', place='"+Dropcity8.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd8.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname9.Text=="" && Droptype9.SelectedIndex==0 && Dropcity9.SelectedIndex==0 )
					{}
					else
					{
						if(mccd9.Text=="")
						{
							if(Droptype9.SelectedIndex==0 || Dropcity9.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname9.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype9.SelectedItem.Text.ToString()+"','"+Dropcity9.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname9.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype9.SelectedItem.Text.ToString()+"', place='"+Dropcity9.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd9.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}
					
					if(txtname10.Text=="" && Droptype10.SelectedIndex==0 && Dropcity10.SelectedIndex==0 )
					{}
					else
					{
						if(mccd10.Text=="")
						{
							if(Droptype10.SelectedIndex==0 || Dropcity10.SelectedIndex==0)
							{
								MessageBox.Show("Please Select the Type and City");
								return;
							}
							else
							{
								fillID1();
								int c=System.Convert.ToInt32(count.ToString());
								con.Open();
								cmd=new SqlCommand("insert into Machanic_Entry values("+c+",'"+txtname10.Text.Trim()+"','"+DropID.SelectedItem.Text.ToString()+"','"+Droptype10.SelectedItem.Text.ToString()+"','"+Dropcity10.SelectedItem.Text.ToString()+"')",con);
								cmd.ExecuteNonQuery();
								con.Close();
								cmd.Dispose();
							}
						}
						else
						{
							con.Open();
							cmd=new SqlCommand("update Machanic_Entry set mcname='"+txtname10.Text.Trim()+"', custid='"+DropID.SelectedItem.Text.Trim()+"', mctype='"+Droptype10.SelectedItem.Text.ToString()+"', place='"+Dropcity10.SelectedItem.Text.ToString()+"' where custid='"+DropID.SelectedItem.Text.Trim()+"' and mccd='"+mccd10.Text+"'",con);
							cmd.ExecuteNonQuery();
							con.Close();
							cmd.Dispose();
						}
					}

					//***************************************************
					//SqlDataReader SqlDtr; 
			
					//"+txtcustid.Text.ToString()+",
					con.Open ();

					cmd=new SqlCommand("update Customermechanicentry  set 	customername='"+DropCustomerName.SelectedItem.Text.ToString()
						+"',mechname1='"+txtname1.Text.Trim().ToString()+
						"',mechname2='"+txtname2.Text.Trim().ToString()+
						"',mechname3='"+txtname3.Text.Trim().ToString()+
						"',mechname4='"+txtname4.Text.Trim().ToString()+
						"',mechname5='"+txtname5.Text.Trim().ToString()+
						"',mechname6='"+txtname6.Text.Trim().ToString()+
						"',mechname7='"+txtname7.Text.Trim().ToString()+
						"',mechname8='"+txtname8.Text.Trim().ToString()+
						"',mechname9='"+txtname9.Text.Trim().ToString()+
						"',mechname10='"+txtname10.Text.Trim().ToString()+
						"',mechtype1='"+Droptype1.SelectedItem.Text.ToString()+
						"',mechtype2='"+Droptype2.SelectedItem.Text.ToString()+
						"',mechtype3='"+Droptype3.SelectedItem.Text.ToString()+
						"',mechtype4='"+Droptype4.SelectedItem.Text.ToString()+
						"',mechtype5='"+Droptype5.SelectedItem.Text.ToString()+
						"',mechtype6='"+Droptype6.SelectedItem.Text.ToString()+
						"',mechtype7='"+Droptype7.SelectedItem.Text.ToString()+
						"',mechtype8='"+Droptype8.SelectedItem.Text.ToString()+
						"',mechtype9='"+Droptype9.SelectedItem.Text.ToString()+
						"',mechtype10='"+Droptype10.SelectedItem.Text.ToString()+
						"',mechcity1='"+Dropcity1.SelectedItem.Text.ToString()+
						"',mechcity2='"+Dropcity2.SelectedItem.Text.ToString()+
						"',mechcity3='"+Dropcity3.SelectedItem.Text.ToString()+
						"',mechcity4='"+Dropcity4.SelectedItem.Text.ToString()+
						"',mechcity5='"+Dropcity5.SelectedItem.Text.ToString()+
						"',mechcity6='"+Dropcity6.SelectedItem.Text.ToString()+
						"',mechcity7='"+Dropcity7.SelectedItem.Text.ToString()+
						"',mechcity8='"+Dropcity8.SelectedItem.Text.ToString()+
						"',mechcity9='"+Dropcity9.SelectedItem.Text.ToString()+
						"',mechcity10='"+Dropcity10.SelectedItem.Text.ToString()+"' where customermechid='"+DropID.SelectedItem.Text.Trim()+"'",con);
					cmd.ExecuteNonQuery();
					MessageBox.Show("Customer Mechanic Entry updated");
					btnselect.Visible=true;
					txtcustid.Visible=true;
					DropID.Visible=false;
					btnadd.Enabled=true;
					btnedit.Text="Edit";
					cmd.Dispose();
					con.Close();
					fillID();
					clear();
				}
				else
				{
					MessageBox.Show("Please Click The Edit Button");
					return;
				}
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
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnEdit, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnEdit,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}
	
		/// <summary>
		/// This method is used to fatch the mechanic information from customerMechanicEntry table 
		/// according to select the Mechanic ID from dropdownlist and fill the data in textboxes 
		/// and dropdownlist and also fatch the MCID from Machanic_Entry table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void DropID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(DropID.SelectedItem.Text.Equals("select"))
				{
					MessageBox.Show("Please select ID");
					return;
				}
				clear();
				btnadd.Enabled=false;
				btnedit.Text="Update";
				btnDelete.Enabled=true;
				btnedit.Enabled=true;
				SqlCommand cmd4;
				SqlConnection con4;
				con4=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con4.Open ();
				SqlDataReader SqlDtr4; 
				cmd4=new SqlCommand("select * from customermechanicentry where customermechid='"+DropID.SelectedItem.Text.Trim()+"'",con4);
				SqlDtr4=cmd4.ExecuteReader();
			
				if(SqlDtr4.HasRows )
				{
					while(SqlDtr4.Read ())
					{
						txtname1.Text=SqlDtr4.GetValue(2).ToString();
						txtname2.Text=SqlDtr4.GetValue(3).ToString();
						txtname3.Text=SqlDtr4.GetValue(4).ToString();
						txtname4.Text=SqlDtr4.GetValue(5).ToString();
						txtname5.Text=SqlDtr4.GetValue(6).ToString();
						txtname6.Text=SqlDtr4.GetValue(7).ToString();
						txtname7.Text=SqlDtr4.GetValue(8).ToString();
						txtname8.Text=SqlDtr4.GetValue(9).ToString();
						txtname9.Text=SqlDtr4.GetValue(10).ToString();
						txtname10.Text=SqlDtr4.GetValue(11).ToString();
						DropCustomerName.SelectedIndex=(DropCustomerName.Items.IndexOf((DropCustomerName.Items.FindByValue(SqlDtr4.GetValue(1).ToString()))));
						Droptype1.SelectedIndex=(Droptype1.Items.IndexOf((Droptype1.Items.FindByValue(SqlDtr4.GetValue(12).ToString()))));
						//					Droptype1.SelectedIndex=0;
						Droptype2.SelectedIndex=(Droptype2.Items.IndexOf((Droptype2.Items.FindByValue(SqlDtr4.GetValue(13).ToString()))));
						Droptype3.SelectedIndex=(Droptype3.Items.IndexOf((Droptype3.Items.FindByValue(SqlDtr4.GetValue(14).ToString()))));
						Droptype4.SelectedIndex=(Droptype4.Items.IndexOf((Droptype4.Items.FindByValue(SqlDtr4.GetValue(15).ToString()))));
						Droptype5.SelectedIndex=(Droptype5.Items.IndexOf((Droptype5.Items.FindByValue(SqlDtr4.GetValue(16).ToString()))));
						Droptype6.SelectedIndex=(Droptype6.Items.IndexOf((Droptype6.Items.FindByValue(SqlDtr4.GetValue(17).ToString()))));
						Droptype7.SelectedIndex=(Droptype7.Items.IndexOf((Droptype7.Items.FindByValue(SqlDtr4.GetValue(18).ToString()))));
						Droptype8.SelectedIndex=(Droptype8.Items.IndexOf((Droptype8.Items.FindByValue(SqlDtr4.GetValue(19).ToString()))));
						Droptype9.SelectedIndex=(Droptype9.Items.IndexOf((Droptype9.Items.FindByValue(SqlDtr4.GetValue(20).ToString()))));
						Droptype10.SelectedIndex=(Droptype10.Items.IndexOf((Droptype10.Items.FindByValue(SqlDtr4.GetValue(21).ToString()))));
						Dropcity1.SelectedIndex=(Dropcity1.Items.IndexOf((Dropcity1.Items.FindByValue(SqlDtr4.GetValue(22).ToString()))));
						Dropcity2.SelectedIndex=(Dropcity2.Items.IndexOf((Dropcity2.Items.FindByValue(SqlDtr4.GetValue(23).ToString()))));
						Dropcity3.SelectedIndex=(Dropcity3.Items.IndexOf((Dropcity3.Items.FindByValue(SqlDtr4.GetValue(24).ToString()))));
						Dropcity4.SelectedIndex=(Dropcity4.Items.IndexOf((Dropcity4.Items.FindByValue(SqlDtr4.GetValue(25).ToString()))));
						Dropcity5.SelectedIndex=(Dropcity5.Items.IndexOf((Dropcity5.Items.FindByValue(SqlDtr4.GetValue(26).ToString()))));
						Dropcity6.SelectedIndex=(Dropcity6.Items.IndexOf((Dropcity6.Items.FindByValue(SqlDtr4.GetValue(27).ToString()))));
						Dropcity7.SelectedIndex=(Dropcity7.Items.IndexOf((Dropcity7.Items.FindByValue(SqlDtr4.GetValue(28).ToString()))));
						Dropcity8.SelectedIndex=(Dropcity8.Items.IndexOf((Dropcity8.Items.FindByValue(SqlDtr4.GetValue(29).ToString()))));
						Dropcity9.SelectedIndex=(Dropcity9.Items.IndexOf((Dropcity9.Items.FindByValue(SqlDtr4.GetValue(30).ToString()))));
						Dropcity10.SelectedIndex=(Dropcity10.Items.IndexOf((Dropcity10.Items.FindByValue(SqlDtr4.GetValue(31).ToString()))));
					}
				}
				mccd.Visible=true;
				mccd1.Visible=true;
				mccd2.Visible=true;
				mccd3.Visible=true;
				mccd4.Visible=true;
				mccd5.Visible=true;
				mccd6.Visible=true;
				mccd7.Visible=true;
				mccd8.Visible=true;
				mccd9.Visible=true;
				mccd10.Visible=true;
				SqlDtr4.Close (); 
				con4.Close();
				cmd4.Dispose();
				
				if(txtname1.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname1.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd1.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname2.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname2.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd2.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname3.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname3.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd3.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname4.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname4.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd4.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname5.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname5.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd5.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname6.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname6.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd6.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname7.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname7.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd7.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname8.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname8.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd8.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname9.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname9.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd9.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
				if(txtname10.Text != "")
				{
					con4.Open();
					SqlCommand cmd=new SqlCommand("select mccd from Machanic_Entry where custid='"+DropID.SelectedItem.Text+"' and mcname='"+txtname10.Text+"'",con4);
					SqlDataReader sdr=cmd.ExecuteReader();
					if(sdr.Read())
						mccd10.Text=sdr.GetValue(0).ToString();
					con4.Close();
					cmd.Dispose();
				}
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
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:DropID_SelectedIndexChange, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:DropID_SelectedIndexChange, EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// This method is used to fatch the all Mechanic ID from CustomerMechanicEntry table and 
		/// fill the dropdownlist on edit time.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnselect_Click(object sender, System.EventArgs e)
		{
			try
			{
				txtcustid.Visible=false;
				btnadd.Enabled=false;
				btnselect.Visible=false;
				DropID.Visible=true;
				SqlCommand cmd5;
				SqlConnection con5;
				SqlDataReader SqlDtr5;
				con5=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con5.Open ();
				cmd5=new SqlCommand("select customermechid from customermechanicentry",con5);
				SqlDtr5=cmd5.ExecuteReader();
				DropID.Items.Clear();
				DropID.Items.Add("select");
				
				if(SqlDtr5.HasRows )
				{
					while(SqlDtr5.Read ())
					{
						DropID.Items.Add(SqlDtr5.GetValue(0).ToString ());
					}
				}
				SqlDtr5.Close (); 
				con5.Close();
				cmd5.Dispose();
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
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnSelect, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnSelect, EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// This method is used to delete the particular mechanic id from CustomerMechanicEntry and 
		/// Machanic_Entry table according to select Mechanic ID from dropdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DropID.SelectedItem.Text.Equals("Select"))
				{
					MessageBox.Show("Please Select the Customer MechanicID");
					return;
				}
			
				if (DropID.Visible==true && DropID.SelectedIndex==0 )
				{
					MessageBox.Show("Please select the ID to Delete");
				}
				else
				{
					SqlCommand cmd6;
					SqlConnection con6;
					con6=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con6.Open ();
					cmd6=new SqlCommand("delete from CustomerMechanicentry where CustomermechID="+DropID.SelectedItem.Text.ToString()+" ",con6);
					cmd6.ExecuteNonQuery();	

					MessageBox.Show("CustomerMechanicID deleted");
					con6.Close();
					cmd6.Dispose();
					con6.Open();
					cmd6=new SqlCommand("delete from Machanic_Entry where custid="+DropID.SelectedItem.Text.ToString()+" ",con6);
					cmd6.ExecuteNonQuery();	
					con6.Close();
					cmd6.Dispose();
					txtcustid.Visible=true;
					DropID.Visible=false;
					btnedit.Text="Edit";
					btnadd.Enabled=true;
					btnDelete.Enabled=false;
					btnselect.Visible=true;
					fillID();
					clear();
				}
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
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnDelete, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerMechanicEntry.aspx,Method:btnDelete, EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}
	}
}