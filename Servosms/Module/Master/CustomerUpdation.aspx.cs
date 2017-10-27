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
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Master
{
	/// <summary>
	/// Summary description for CustomerUpdation.
	/// </summary>
	public partial class CustomerUpdation : System.Web.UI.Page
	{
		public bool chkPriv = false;
		public string sqlstr="";
		protected System.Web.UI.WebControls.CheckBox ChkTrue;
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required textboxes with database values and also fill some 
		/// additional information and also check accessing priviledges for particular user
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
				CreateLogFiles.ErrorLog("Form:CustomerUpdation.aspx,Method:pageload"+"EXCEPTION  "+ ex.Message+ uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);	
				return;
			}
			if(!Page.IsPostBack)
			{
				InventoryClass obj = new InventoryClass();
				string str="";
				SqlDataReader rdr = obj.GetRecordSet("select distinct sadbhavnacd from customer where sadbhavnacd<>'0' and sadbhavnacd<>''");
				while(rdr.Read())
				{
					str+=rdr.GetValue(0).ToString()+",";
				}
				rdr.Close();
				tempUniqueCode.Value=str;
				#region Check Privileges
				int i;
				string View_Flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="3";
				string SubModule="7";
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
					chkPriv=true;
					return;
				}
				if(Add_Flag=="0")
					btnSubmit.Enabled=false;
//				if(Edit_Flag=="0")
//					btnEdit.Enabled=false;
//				if(Del_Flag=="0")
//					btnDelete.Enabled=false;
				#endregion
				GetMultiValue();
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
		/// This method is used to fill the searchable combo box when according to select value
		/// from dropdownlist with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strName="",strType="",strDistrict="",strPlace="",strSSR="";
				
				//comment by vikas 25.05.09 strName = "select distinct cust_name from customer order by cust_name";
				
				strName = "select distinct cust_name,city from customer order by cust_name,city";
				
				strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				strDistrict = "select distinct state from customer order by state";
				strPlace = "select distinct city from customer order by city";
				strSSR = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";
				string[] arrStr = {strName,strType,strDistrict,strPlace,strSSR};
				HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempDistrict,tempPlace,tempSSR};	
				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							
							/*******Add by vikas 25.05.09*********************/
							if(i==0)
							{
								arrCust[i].Value+=rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString()+",";
							}
							else
							{
								arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							}
							/*******End*********************/
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to update the data with the help of ProCustomerUpdateAll stored procedure.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
				 DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
				//InventoryClass obj1=new InventoryClass ();
				//SqlDataReader SqlDtr1=null;
				SqlDataReader rdr1=null;
				int Total_Cust=0;
				//string prod_cat="",sql;
				//int flag = 0;
				//string j="";
				//int x=0;
				PartiesClass obj=new PartiesClass ();
				Total_Cust=System.Convert.ToInt32(Request.Params.Get("Total_Cust"));
				int i=0;
				for(int j=0;j<Total_Cust;j++)
				{ 
					i=0;
					obj.Cust_ID=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.sadbhavnacd=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.Cust_Name=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.Address=Request.Params.Get("txtInfo"+j+"To"+i++);
					//obj.EMail=Request.Params.Get("txtInfo"+j+"To"+i++);
					string type=Request.Params.Get("txtInfo"+j+"To"+i++);
					if(type!="Select")
						obj.Cust_Type=type;
					else
						obj.Cust_Type="";
					string ssr=Request.Params.Get("txtInfo"+j+"To"+i++);
                    if(ssr=="Select")
						obj.SSR="";
					else
					{
						dbobj1.SelectQuery("select Emp_ID from Employee where Emp_Name='"+ssr+"'",ref rdr1);
						if(rdr1.Read())
							obj.SSR=rdr1["Emp_ID"].ToString();
						else
							obj.SSR="";
						rdr1.Close();
					}
					//obj.SSR=Request.Params.Get("txtInfo"+j+"To"+i++);
					string city=Request.Params.Get("txtInfo"+j+"To"+i++);
					if(city!="Select")
						obj.City=city;
					else
						obj.City="";
					dbobj1.SelectQuery("select state,country from beat_master where city='"+city+"'",ref rdr1);
					if(rdr1.Read())
					{
						obj.State=rdr1["state"].ToString();
						obj.Country=rdr1["country"].ToString();
					}
					else
					{
						obj.State="";
						obj.Country="";
					}
					rdr1.Close();
					//obj.Op_Balance=Request.Params.Get("txtInfo"+j+"To"+i++);        //Coment by vikas 17.07.09
					//obj.Balance_Type=Request.Params.Get("txtInfo"+j+"To"+i++);      //Coment by vikas 17.07.09

					obj.ContactPerson=Request.Params.Get("txtInfo"+j+"To"+i++);      //Add by vikas 17.07.09

					obj.Tel_Off=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.Mobile=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.Tin_No=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.CR_Limit=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.CR_Days=Request.Params.Get("txtInfo"+j+"To"+i++);
					obj.Op_Balance=Request.Params.Get("txtInfo"+j+"To"+i++);        //Add by vikas 17.07.09
					obj.Balance_Type=Request.Params.Get("txtInfo"+j+"To"+i++);      //Add by vikas 17.07.09
					//call the procedure ProCustomerUpdateAll for update the customer data.
					obj.UpdateCustomerAll();
				}
				if(i>0)
				{
					DropSearchBy.SelectedIndex=0;          //Add by vikas 17.07.09
					DropValue.Value="All";                 //Add by vikas 17.07.09
					//Customer balance update after update the customer record.
					CustomerBalanceUpdation();
					MessageBox.Show("Customer Updated");
				}
				CreateLogFiles.ErrorLog("Form:CustomerUpdation.aspx,Method:btnSubmit_click(),  Customer Updated Successfully");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerUpdation.aspx,Method:update().   EXCEPTION " +ex.Message );
				// CreateLogFiles.ErrorLog("Form:stockledgerUpdation.aspx,Method:update().   EXCEPTION " +ex.Message +"  User_ID : "+ Session["User_Name"].ToString());
			}
		}

		/// <summary>
		/// This method is used to update the customer balance after update the customer record.
		/// </summary>
		public void CustomerBalanceUpdation()
		{
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass();
			SqlCommand cmd;
			DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlDataReader rdr1=null,rdr=null;
			dbobj.SelectQuery("select Ledger_ID from Ledger_Master l,customer c where cust_name=ledger_name",ref rdr1);
			while(rdr1.Read())
			{
				dbobj.SelectQuery("select * from AccountsLedgerTable where Ledger_ID='"+rdr1["Ledger_ID"].ToString()+"' order by entry_date",ref rdr);
				double Bal=0;
				string BalType="";
				int i=0;
				while(rdr.Read())
				{
					if(i==0)
					{
						BalType=rdr["Bal_Type"].ToString();
						i++;
					}
					if(double.Parse(rdr["Credit_Amount"].ToString())!=0)
					{
						if(BalType=="Cr")
						{
							Bal+=double.Parse(rdr["Credit_Amount"].ToString());
							BalType="Cr";
						}
						else
						{
							Bal-=double.Parse(rdr["Credit_Amount"].ToString());
							if(Bal<0)
							{
								Bal=double.Parse(Bal.ToString().Substring(1));
								BalType="Cr";
							}
							else
								BalType="Dr";
						}
					}
					else if(double.Parse(rdr["Debit_Amount"].ToString())!=0)
					{
						if(BalType=="Dr")
							Bal+=double.Parse(rdr["Debit_Amount"].ToString());
						else
						{
							Bal-=double.Parse(rdr["Debit_Amount"].ToString());
							if(Bal<0)
							{
								Bal=double.Parse(Bal.ToString().Substring(1));
								BalType="Dr";
							}
							else
								BalType="Cr";
						}
					}

					Con.Open();
					cmd = new SqlCommand("update AccountsLedgerTable set Balance='"+Bal.ToString()+"',Bal_Type='"+BalType+"' where Ledger_ID='"+rdr["Ledger_ID"].ToString()+"' and Particulars='"+rdr["Particulars"].ToString()+"' ",Con);
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					Con.Close();
				}
				rdr.Close();
			}
			rdr1.Close();
			//****************
			dbobj.SelectQuery("select Cust_ID from Customer",ref rdr1);
			while(rdr1.Read())
			{
				dbobj.SelectQuery("select * from CustomerLedgerTable where CustID='"+rdr1["Cust_ID"].ToString()+"' order by entrydate",ref rdr);
				double Bal=0;
				string BalType="";
				int i=0;
				while(rdr.Read())
				{
					if(i==0)
					{
						BalType=rdr["BalanceType"].ToString();
						i++;
					}
					if(double.Parse(rdr["CreditAmount"].ToString())!=0)
					{
						if(BalType=="Cr.")
						{
							Bal+=double.Parse(rdr["CreditAmount"].ToString());
							BalType="Cr.";
						}
						else
						{
							Bal-=double.Parse(rdr["CreditAmount"].ToString());
							if(Bal<0)
							{
								Bal=double.Parse(Bal.ToString().Substring(1));
								BalType="Cr.";
							}
							else
								BalType="Dr.";
						}
					}
					else if(double.Parse(rdr["DebitAmount"].ToString())!=0)
					{
						if(BalType=="Dr.")
							Bal+=double.Parse(rdr["DebitAmount"].ToString());
						else
						{
							Bal-=double.Parse(rdr["DebitAmount"].ToString());
							if(Bal<0)
							{
								Bal=double.Parse(Bal.ToString().Substring(1));
								BalType="Dr.";
							}
							else
								BalType="Cr.";
						}
					}
					Con.Open();
					cmd = new SqlCommand("update CustomerLedgerTable set Balance='"+Bal.ToString()+"',BalanceType='"+BalType+"' where CustID='"+rdr["CustID"].ToString()+"' and Particular='"+rdr["Particular"].ToString()+"' ",Con);
					cmd.ExecuteNonQuery();
					cmd.Dispose();
					Con.Close();
				}
				rdr.Close();
			}
			rdr1.Close();
		}

		protected void txtview_Click(object sender, System.EventArgs e)
		{
			string Cust_Name="";
			/*InventoryClass obj = new InventoryClass();
			string str="";
			SqlDataReader rdr = obj.GetRecordSet("select distinct sadbhavnacd from customer where sadbhavnacd<>'0' and sadbhavnacd<>''");
			while(rdr.Read())
			{
				str+=rdr.GetValue(0).ToString()+",";
			}
			rdr.Close();
			tempUniqueCode.Value=str;*/
			chkPriv=true;
			
			if(DropSearchBy.SelectedIndex==0)
				//sql="Select * from Customer";
				//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer order by Cust_Name";
				sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer order by Cust_Name";
			else
			{
				if(DropValue.Value=="All")
					//sql="Select * from Customer";
					//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer";
					sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer";
				else if(DropSearchBy.SelectedIndex==1)
				{
					Cust_Name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
					//sql="Select * from Customer where cust_name='"+Cust_Name.ToString()+"'";
					//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer where cust_name='"+Cust_Name.ToString()+"'";
					sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer where cust_name='"+Cust_Name.ToString()+"'";
				}
				else if(DropSearchBy.SelectedIndex==2)
					//sql="Select * from Customer where cust_type like '"+DropValue.Value+"%'";
					//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer where cust_type like '"+DropValue.Value+"%'";
					sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer where cust_type like '"+DropValue.Value+"%'";
				else if(DropSearchBy.SelectedIndex==3)
					//sql="Select * from Customer where state='"+DropValue.Value+"'";
					//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer where state='"+DropValue.Value+"'";
					sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer where state='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==4)
					//sql="Select * from Customer where city='"+DropValue.Value+"'";
					//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer where city='"+DropValue.Value+"'";
					sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer where city='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==5)
					//sql="Select * from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')";
					//17.07.09 vikas sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,Op_Balance,Balance_type,tel_off,mobile,tin_no,cr_limit,cr_days from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')";
					sqlstr="select cust_id,sadbhavnacd,cust_name,address,cust_type,ssr,city,ContactPerson,tel_off,mobile,tin_no,cr_limit,cr_days,Op_Balance,Balance_type from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')";

				sqlstr=sqlstr+" order by Cust_Name";
			}


			
		}

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
