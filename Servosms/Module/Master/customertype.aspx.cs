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
using RMG;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using DBOperations;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for customertype.
	/// </summary>
	public partial class customertype : System.Web.UI.Page
	{
		string uid;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);

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
					//MessageBox.Show("type");
					fillID();
					getGroup();
					getSubGroup();
					#region Check Privileges
					int i;
					string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="3";
					string SubModule="6";
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
						btnAdd.Enabled=false;
					if(Edit_Flag=="0")
						btnEdit.Enabled=false;
					if(Del_Flag=="0")
						btnDelete.Enabled=false;
					#endregion
					//txtid.Text="CT"+txtid.Text;

					txtGroup.Text="";
					txtSGroup.Text="";
					txtGroup.Enabled=false;
					txtSGroup.Enabled=false;
					CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:Page_Load, userid  "+uid );
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:Page_Load,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}
		
		/// <summary>
		/// This method is used to generate next CustomerTypeID from CustomerType table
		/// and fill in the textbox when page is loaded.
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
				cmd=new SqlCommand("select max(CustomerTypeID)+1 from CustomerType",con);
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
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:FillID, EXCEPTION "+ex.Message+"  userid  "+uid );
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// This method is used to Group Name in DropDownlist from CustomerType table
		/// Add By Viks Sharma 23.10.2012
		/// </summary>
		public void getGroup()
		{
			try
			{
				dropGroup.Items.Clear();
				dropGroup.Items.Add("Select");
				dropGroup.SelectedIndex = 0; 
				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("select distinct Group_Name from CustomerType order by Group_Name ",ref SqlDtr );
				while(SqlDtr.Read())
				{
					if(SqlDtr["Group_Name"].ToString()!=null && SqlDtr["Group_Name"].ToString()!="")
					{
						dropGroup.Items.Add(SqlDtr["Group_Name"].ToString());  
					}
				}
				SqlDtr.Close();
				
				dropGroup.Items.Add("Other");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer Type,Method: getGroup() Exception: "+ex.Message+"  User: "+ uid);     
			}
		}

		/// <summary>
		/// This method is used to Sub Group Name in DropDownlist from CustomerType table
		/// Add By Viks Sharma 23.10.2012
		/// </summary>
		public void getSubGroup()
		{
			try
			{
				dropSGroup.Items.Clear();
				dropSGroup.Items.Add("Select");
				dropSGroup.SelectedIndex = 0; 
				SqlDataReader SqlDtr = null;
				dbobj.SelectQuery("select distinct Sub_Group_Name from CustomerType order by Sub_Group_Name",ref SqlDtr );
				while(SqlDtr.Read())
				{
					if(SqlDtr["Sub_Group_Name"].ToString()!=null && SqlDtr["Sub_Group_Name"].ToString()!="")
					{
						dropSGroup.Items.Add(SqlDtr["Sub_Group_Name"].ToString());  
					}
				}
				SqlDtr.Close();
				dropSGroup.Items.Add("Other");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer Type,Method: getGroup() Exception: "+ex.Message+"  User: "+ uid);     
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
		/// This is used to fetch The Group after the server trip to avoid the blank value in the Combo box.
		/// Add by vikas Sharma 23/10/2012
		/// </summary>
		public void fetchGroup()
		{
			try
			{
				dropGroup.Items.Clear();
				dropGroup.Items.Add("Select");
		
				SqlDataReader SqlDtr = null;
				if(dropSGroup.SelectedItem.Text.Trim() != "Other")
				{
					dbobj.SelectQuery("select Group_Name from CustomerType where Sub_Group_Name = '"+dropSGroup.SelectedItem.Text.Trim()+"'",ref SqlDtr);
				}
				else
				{
					txtSGroup.Enabled = true; 
					dbobj.SelectQuery("select distinct Group_Name from CustomerType where  lmsg.Grp_ID = gr.Grp_ID",ref SqlDtr);
				}
				while(SqlDtr.Read())
				{
					dropGroup.Items.Add(SqlDtr["Grp_Name"].ToString());
				}
				dropGroup.Items.Add("Other");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Ledger Creation,Method:fetchGroup() Exception: "+ex.Message+"  User: "+ uid);     
			}
		}

		/// <summary>
		/// This method is used to Save the CustomerTypeName from CustomerType table 
		/// before check the customer type is not duplicate in dtabase.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				SqlCommand cmd5;
				SqlConnection con5;
				SqlDataReader sql5;
				con5=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con5.Open ();
				//SqlDataReader SqlDtr; 
				cmd5=new SqlCommand("SELECT * FROM CustomerType where CustomerTypeName='"+txtname.Text.ToString()+"'",con5);
				sql5=cmd5.ExecuteReader();
				if(sql5.HasRows)
				{
					MessageBox.Show("CustomerType Exist Already");
					txtname.Text="";
					return;
				}
				//**************

				/********** Add by Vikas Sharma Date 23/10/2012 *****************/
				
				string Group="",Sub_Group="";

				if(dropGroup.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Group");
					//fetchGroup();
					return;
				}
				else 
				{
					if(dropGroup.SelectedItem.Text == "Other")
					{
						if(txtGroup.Text.Trim() == "")
						{
							MessageBox.Show("Please specify Other Group");
							txtGroup.Enabled = true; 
							//fetchGroup();
							return;
						}
						else
							Group =txtGroup.Text.Trim(); 
					}
					else
					{
						Group = dropGroup.SelectedItem.Text.Trim(); 
					}
				}

				if(dropSGroup.SelectedIndex == 0)
				{
					MessageBox.Show("Please select Sub Group");
					//fetchGroup();
					return;
				}
				else 
				{
					if(dropSGroup.SelectedItem.Text == "Other")
					{
						if(txtSGroup.Text.Trim() == "")
						{
							MessageBox.Show("Please specify Other Sub Group");
							txtSGroup.Enabled = true; 
							//fetchGroup();
							return;
						}
						else
							Sub_Group =txtSGroup.Text.Trim(); 
					}
					else
					{
						Sub_Group = dropSGroup.SelectedItem.Text.Trim(); 
					}
				}
				
				/***************************/
				SqlCommand cmd;
				SqlConnection con;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				//SqlDataReader SqlDtr; 
				//Comment By Vikas Date 23/10/2012 cmd=new SqlCommand("insert into CustomerType values('"+txtid.Text.ToString()+"','"+StringUtil.FirstCharUpper(txtname.Text.ToString())+"')",con);
				
				cmd=new SqlCommand("insert into CustomerType values('"+txtid.Text.ToString()+"','"+StringUtil.FirstCharUpper(txtname.Text.ToString())+"','"+Group+"','"+Sub_Group+"')",con); //Add By Vikas 23/10/2012
				
				cmd.ExecuteNonQuery();
				MessageBox.Show("CustomerType Saved");
				cmd.Dispose();
				con.Close();
				fillID();
				getGroup();
				getSubGroup();
				txtname.Text="";
				txtGroup.Text="";     //Add by Vikas 23.10.2012
				txtSGroup.Text="";    //Add by Vikas 23.10.2012
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnAdd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnEdit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:btnAdd, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:btnAdd,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// This method is used to handle two operation
		/// if button text name is Edit then fatch the CustomerTypeName from CustomerType table and fill into the 
		/// dropdownlist otherwise update the CustomerType from database.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(btnEdit.Text.Trim().Equals("Edit"))
				{
					dropid.Visible=true;
					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					SqlDataReader SqlDtr; 
					cmd=new SqlCommand("select * from CustomerType order by CustomerTypename",con);
					SqlDtr=cmd.ExecuteReader();
					dropid.Items.Clear();
					dropid.Items.Add("Select");
					while(SqlDtr.Read())
					{
						dropid.Items.Add(SqlDtr.GetValue(1).ToString());
						//			*		dropid.Items.Add(SqlDtr.GetValue(0).ToString());
					}
					btnEdit.Enabled=false;
					txtid.Visible=false;
					btnAdd.Enabled=false;
					btnDelete.Enabled=false;

				}
					//			txtid.Text=SqlDtr.GetValue(0).ToString();
					//			txtname.Text=SqlDtr.GetValue(1).ToString();
					//MessageBox.Show("CustomerType Saved");

					//txtid.Text="";
					//txtname.Text="";
				else if(dropid.SelectedItem.Text.Equals("Select"))
				{
					MessageBox.Show("Please Select the Customer Type");
					return;
				}
				if(btnEdit.Text=="Update")
				{
					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();

					/********** Add by Vikas Sharma Date 23/10/2012 *****************/
				
					string Group="",Sub_Group="";

					if(dropGroup.SelectedIndex == 0)
					{
						MessageBox.Show("Please select Group");
						//fetchGroup();
						return;
					}
					else 
					{
						if(dropGroup.SelectedItem.Text == "Other")
						{
							if(txtGroup.Text.Trim() == "")
							{
								MessageBox.Show("Please specify Other Group");
								txtGroup.Enabled = true; 
								//fetchGroup();
								return;
							}
							else
								Group =txtGroup.Text.Trim(); 
						}
						else
						{
							Group = dropGroup.SelectedItem.Text.Trim(); 
						}
					}

					if(dropSGroup.SelectedIndex == 0)
					{
						MessageBox.Show("Please select Sub Group");
						//fetchGroup();
						return;
					}
					else 
					{
						if(dropSGroup.SelectedItem.Text == "Other")
						{
							if(txtSGroup.Text.Trim() == "")
							{
								MessageBox.Show("Please specify Other Sub Group");
								txtSGroup.Enabled = true; 
								//fetchGroup();
								return;
							}
							else
								Sub_Group =txtSGroup.Text.Trim(); 
						}
						else
						{
							Sub_Group = dropSGroup.SelectedItem.Text.Trim(); 
						}
					}
					//SqlDataReader SqlDtr; 
					//	*			cmd=new SqlCommand("update CustomerType set CustomerTypeName='"+txtname.Text.ToString().Trim()+"' where CustomerTypeID='"+dropid.SelectedItem.Text.ToString().Trim()+"'",con);
					
					//Comment by Vikas Sharma 23.10.2012 cmd=new SqlCommand("update CustomerType set CustomerTypeName='"+StringUtil.FirstCharUpper(txtname.Text.ToString().Trim())+"' where CustomerTypeName='"+dropid.SelectedItem.Text.ToString().Trim()+"'",con);
					
					cmd=new SqlCommand("update CustomerType set CustomerTypeName='"+StringUtil.FirstCharUpper(txtname.Text.ToString().Trim())+"', Group_Name='"+StringUtil.FirstCharUpper(Group.ToString().Trim())+"', Sub_Group_Name='"+StringUtil.FirstCharUpper(Sub_Group.ToString().Trim())+"' where CustomerTypeName='"+dropid.SelectedItem.Text.ToString().Trim()+"'",con);     //Add by Vikas Sharma 23.10.2012

					cmd.ExecuteNonQuery();
					MessageBox.Show("CustomerType Updated");
					btnEdit.Text="Edit";
					dropid.Visible=false;
					txtid.Visible=true;
					btnDelete.Enabled=false;
					fillID();
					getGroup();
					getSubGroup();
					btnAdd.Enabled=true;
					txtname.Text="";
					txtGroup.Text="";
					txtSGroup.Text="";
				}
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnAdd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnEdit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btnDelete.Enabled=false;
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:btnEdit, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:btnEdit,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// this method is used to fatch the CustomerTypeName and CustomerTypeID from CustomerType table
		/// and fill the textboxes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropid_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				//add by vikas sharma 23.10.2012

				if(dropid.SelectedIndex==0)
				{
					txtname.Text="";
					dropGroup.SelectedIndex=0;
					dropSGroup.SelectedIndex=0;
					return;
				}

				btnEdit.Text="Update";
				btnEdit.Enabled=true;
				btnDelete.Enabled=true;
				//dropid.Visible=true;
				SqlCommand cmd;
				SqlConnection con;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				SqlDataReader SqlDtr; 
				//	*		cmd=new SqlCommand("select * from CustomerType where CustomerTypeID='"+dropid.SelectedItem.Text.ToString()+"'",con);
				cmd=new SqlCommand("select * from CustomerType where CustomerTypeName='"+dropid.SelectedItem.Text.Trim().ToString()+"'",con);
				SqlDtr=cmd.ExecuteReader();
				while(SqlDtr.Read())
				{
					txtid.Text=SqlDtr.GetValue(0).ToString();
					txtname.Text=SqlDtr.GetValue(1).ToString();
					dropGroup.SelectedIndex=dropGroup.Items.IndexOf(dropGroup.Items.FindByText(SqlDtr.GetValue(2).ToString()));        //Add by Vikas Sharma 23.10.2012
					dropSGroup.SelectedIndex=dropSGroup.Items.IndexOf(dropSGroup.Items.FindByText(SqlDtr.GetValue(3).ToString()));	   //Add by Vikas Sharma 23.10.2012
				}
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnAdd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnEdit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btnDelete.Enabled=false;
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:Dropid_SelectedIndexChange, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:Dropid_SelectedIndexChange,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// This method is used to delete the particular CustomerTypeName from database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(dropid.SelectedItem.Text.Equals("Select"))
				{
					MessageBox.Show("Please Select the Customer Type");
					return;
				}
				if (dropid.Visible==true && dropid.SelectedIndex==0 )
				{
					MessageBox.Show("Please select the ID to Delete");
				}
				else
				{
					SqlCommand cmd;
					SqlConnection con;
					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con.Open ();
					//		*			cmd=new SqlCommand("delete from CustomerType where CustomerTypeID="+dropid.SelectedItem.Text.ToString()+" ",con);
					cmd=new SqlCommand("delete from CustomerType where CustomerTypeName='"+dropid.SelectedItem.Text.Trim().ToString()+"' ",con);
					cmd.ExecuteNonQuery();	
					MessageBox.Show("Customer Type deleted");
					txtid.Visible=true;
					dropid.Visible=false;
					txtname.Text="";
					btnEdit.Text="Edit";
					btnAdd.Enabled=true;
					btnDelete.Enabled=false;
					fillID();
					getGroup();
					getSubGroup();
					txtGroup.Text="";
					txtSGroup.Text="";
					dropGroup.SelectedIndex=0;
					dropSGroup.SelectedIndex=0;
				}
				Object Add_Flag=Cache["Add"];
				Object Edit_Flag=Cache["Edit"];
				Object Del_Flag=Cache["Del"];
				if(System.Convert.ToString(Add_Flag)=="0")
				{
					btnAdd.Enabled=false;
				}
				if(System.Convert.ToString(Edit_Flag)=="0")
					btnEdit.Enabled=false;
				if(System.Convert.ToString(Del_Flag)=="0")
					btnDelete.Enabled=false;		
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:btnDelete, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerType.aspx,Method:btnDelete,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}
	}
}