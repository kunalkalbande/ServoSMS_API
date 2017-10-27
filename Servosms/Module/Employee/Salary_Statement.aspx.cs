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
using Servosms.Sysitem.Classes;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Employee
{
	/// <summary>
	/// Summary description for Salary_Statement.
	/// </summary>
	public partial class Salary_Statement : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		TimeSpan sp = DateTime.Now.Subtract(new DateTime(DateTime.Now.Year ,DateTime.Now.Month,1));
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				
				uid=(Session["User_Name"].ToString());
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:pageload"+ ex.Message+" EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="2";
				string SubModule="5";
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
					//	string msg="UnAthourized Visit to Salary Statement Page";
					//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
					return;
				}
				#endregion
				dropyear.SelectedIndex =dropyear.Items.IndexOf(dropyear.Items.FindByValue(System.DateTime.Today.Year.ToString()));
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
			this.GridMachineReport.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.GridMachineReport_ItemDataBound);

		}
		#endregion

		/// <summary>
		/// This method is used to view the salary of all employee from attendance register table.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
				strorderby="emp_id ASC";
				Session["Column"]="emp_id";
				Session["order"]="ASC";
				Bindthedata();
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:btnShow_Click"+"Userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : Salary_statement2.aspx ,Method:btnShow_Click"+ ex.Message+" EXCEPTION "+uid);
			}
		}

		public void Bindthedata()
		{
			try
			{
				/************Add by vikas 22.11.2012*************************/
				Tot_Basic_Salary=0;
				Tot_Net_Salary=0;
				Tot_Advance=0;
				Tot_Incentive=0;
				G_Tot_Salary=0;
				Tot_TADA=0;
				Tot_Expendeture=0;
				/************End*************************/ 

				EmployeeClass  obj=new EmployeeClass();
				SqlDataReader SqlDtr;
				string sql;
				sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex) +"/"+dropyear.SelectedIndex+"' ";	
				SqlDtr =obj.GetRecordSet(sql);
				if(SqlDtr != null)
				{
					while(SqlDtr.Read())
					{
						if(SqlDtr.GetValue(0).ToString().Equals("NULLS") || !SqlDtr.GetValue(0).ToString().Trim().Equals("") )
						{
							GridMachineReport.Visible = true;
						}
						else
						{
							GridMachineReport.Visible = false;
							MessageBox.Show("Details not available");
							return;
						}
					}
				}
				SqlDtr.Close();

				//Fetch the employee id and its salary details ad bind the data grid.

				#region Bind DataGrid
				
				//coment by vikas 29.10.2012 sql="select emp_id,emp_name, salary, ot_compensation from employee";
				/*coment by vikas 19.12.2012 sql="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
				SqlDtr =obj.GetRecordSet(sql);
				if(SqlDtr.HasRows)
				{		
					
					GridMachineReport.DataSource=SqlDtr;
					GridMachineReport.DataBind();
				}
				SqlDtr.Close();*/

				SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				sql="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();	
				da.Fill(ds,"stkmv");
				DataTable dtcustomer=ds.Tables["stkmv"];
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridMachineReport.DataSource=dv;
				if(dv.Count!=0)
				{
						GridMachineReport.DataBind();
						GridMachineReport.Visible=true;
				}
				else
				{
					GridMachineReport.Visible=false;
					MessageBox.Show("Data Not Available");
				}
				sqlcon.Dispose();

				#endregion
					
				//CreateLogFiles.ErrorLog("Form : Salary_statement.aspx , Method:btnShow_Click"+"Userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog(" Form : Salary_statement1.aspx,Method:btnShow_Click"+ ex.Message+" EXCEPTION "+uid);
			}
		}

		public static string strorderby="";
		public void sortcommand_click(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["order"].Equals("ASC"))
					{
						strorderby=e.SortExpression.ToString()+" DESC";
						Session["order"]="DESC";
					}
					else
					{
						strorderby=e.SortExpression.ToString()+" ASC";
						Session["order"]="ASC";
					}
				}
				else
				{
					strorderby=e.SortExpression.ToString()+" ASC";
					Session["order"]="ASC";
				}
				Session["column"]=e.SortExpression.ToString();
				Bindthedata();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		//*************************bhal new( change)********************************
		public static double Net_Salarytot1;
		public double Net_Salarytot=0;
		public static double totpresenttot1;
		public double totpresenttot=0;
		public double othtot=0;
		public static double othtot1;

		/************Add by vikas 22.11.2012*************************/
		public static double Tot_Basic_Salary=0;
		public static double Tot_Net_Salary=0;
		public static double Tot_Advance=0;
		public static double Tot_Incentive=0;
		public static double G_Tot_Salary=0;
		public static double Tot_TADA=0;
		public static double Tot_Expendeture=0;
		/************End*************************/
		
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public string emp_id3;
		private void GridMachineReport_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			int Total_Present=0;
			float Total_OverTime=0;
			double Monthly_Salary=0;
			double OT_Compensation=0;
			double Net_Salary=0;
			try
			{
				EmployeeClass  obj=new EmployeeClass();
				SqlDataReader SqlDtr;
				string sql;
				int Days_in_Months=30;
				int diff = System.Convert.ToInt32(sp.Days)+1;
				// here from_date = actual "To date".
				/*1*/		string from_date = DropMonth.SelectedIndex+"/"+diff+"/"+dropyear.SelectedIndex;
				// if the current month is not equals to selected month then set the to date i.e(from_date) as th final date of the month.
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					/*2*/from_date = DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex;
				}
				
				// if the month is not current month then diff  =  30;
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					diff = 30;
				}
				else
				{
					/*3*/if(DateTime.DaysInMonth(dropyear.SelectedIndex,DateTime.Now.Month) == diff)
							diff = 30;
				}
				/*Coment by vikas 18.11.2012 if(!e.Item.Cells[4].Text.ToString().Equals("Total Days"))    
					e.Item.Cells[4].Text = diff.ToString();  */

				
				string emp_id = e.Item.Cells[0].Text.ToString();
				if(emp_id.Trim().Equals("Emp ID") || emp_id.Trim().Equals("&nbsp;")|| emp_id.Trim().Equals(" "))
					emp_id = "0";
				else
				{
					int leave = 0;
					/*4*/				sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ emp_id +"' and isSanction = 1";
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows )
					{
						if(SqlDtr.Read())
						{
							if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
								leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
						}
					}
					SqlDtr.Close();
					
					//sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"' and cast(floor(cast(date_to as float)) as datetime) > '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"'and emp_id = '"+ emp_id +"'";
					sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ emp_id +"' and isSanction = 1 and datepart(month,date_from) = datepart(month,'"+from_date +"')";
					//if(emp_id.Equals("1002")) 
					//Response.Write(sql+"<br><br>");
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows  )
					{
						if(SqlDtr.Read())
						{
							if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
								leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
						}
					}
					SqlDtr.Close();
					//date_to
					/*5*/		sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '"+from_date +"' then sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,'"+from_date +"'))) else sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ emp_id +"' and isSanction = 1 group by date_to";
					//if(emp_id.Equals("1002")) 
					//	Response.Write(sql+"<br><br>");
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows )
					{
						if(SqlDtr.Read())
						{
							if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
								leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
						}
					}
					SqlDtr.Close();

					/*Coment by vikas 18.11.2012 
					 * if(!e.Item.Cells[5].Text.ToString().Equals("Leave"))    
						e.Item.Cells[5].Text = leave.ToString();  */
					if(!e.Item.Cells[3].Text.ToString().Equals("Leave"))    
						e.Item.Cells[3].Text = leave.ToString(); 

					string Present="0";
					#region Bind Total Present Regarding Each Item				
					/*6*/			sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ emp_id +"'";
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows )
					{
						while(SqlDtr.Read())
						{
							if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							{
								//coment by vikas 18.11.2012 e.Item.Cells[6].Text=SqlDtr.GetValue(0).ToString() ;
								if(!e.Item.Cells[2].Text.ToString().Equals("Working Days")) 
									e.Item.Cells[2].Text=SqlDtr.GetValue(0).ToString() ;
								Present=SqlDtr.GetValue(0).ToString();
							}	
						}
					}
					totpresenttot += System.Convert.ToDouble(e.Item.Cells[6].Text.ToString()) ;
					totpresenttot1=totpresenttot;
					SqlDtr.Close();
					#endregion

					/*******Add by vikas 18.11.2012*******************/
					e.Item.Cells[5].Text=Convert.ToString((double.Parse(Present.ToString())-leave));

					/*******End*******************/

					#region Bind Total OverTime Hours Regarding Each Item
					/*7*/			string	Sql1 ="select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ emp_id+"'";
					//Response.Write(Sql1+"<br>"); 
					float hr=0;
					float mn1=0;
					SqlDtr = obj.GetRecordSet(Sql1);
					if(SqlDtr != null)
					{
						if(SqlDtr.HasRows)
						{
							if (SqlDtr.Read())
							{
								string	strh11=SqlDtr.GetValue(0).ToString();
								if(strh11==null||strh11.Equals(""))
								{
									strh11="0";
								}
								string	strm11=SqlDtr.GetValue(1).ToString();
								if(strm11==null || strm11.Equals(""))
								{
									strm11="0";
								}
						
								// Response.Write(strh11+"<br>" );
								//Response.Write(strm11+"<br>" );
								hr= float.Parse(strm11) / 60  +  float.Parse(strh11);
								//Response.Write(hr+"<br>" );
								mn1=float.Parse(strm11)% 60;
								//Response.Write(mn1+"<br>" );
								double hr1=System.Math.Floor(System.Convert.ToDouble(hr));
								//Response.Write(hr1+"<br>" );
								string st="24";
								Total_OverTime=float.Parse(hr1.ToString() );
								if(Total_OverTime<0)
								{
									Total_OverTime=Total_OverTime+float.Parse(st);
								}
								string	hr2=Total_OverTime.ToString()+"."+mn1.ToString() ;
								//Total_OverTime = float.Parse(hr2);
								//Response.Write(Total_OverTime+"<br>" );
							
								//coment by vikas 19.12.2012 e.Item.Cells[7].Text=GenUtil.strNumericFormat(hr2);
								e.Item.Cells[7].Text=Convert.ToString(Math.Round(double.Parse(hr2.ToString())));
								othtot+=double.Parse(GenUtil.strNumericFormat(hr2));
								othtot1=othtot;
							}
						}
					}
					SqlDtr.Close();
					#endregion
					#region Calculate Net Salary
					Monthly_Salary=System.Convert.ToDouble(e.Item.Cells[2].Text.ToString());
					
					Tot_Basic_Salary+=Monthly_Salary;

					OT_Compensation=System.Convert.ToDouble(e.Item.Cells[3].Text.ToString());
					if(e.Item.Cells[6].Text.Equals(""))
					{
						e.Item.Cells[6].Text="0";
					}
					if(e.Item.Cells[7].Text.Equals(""))
					{
						e.Item.Cells[7].Text="0";
					}
					Total_Present=System.Convert.ToInt32(e.Item.Cells[6].Text.ToString());
					//coment by vikas 18.11.2012 Net_Salary=(Total_Present * Monthly_Salary / Days_in_Months);
					Net_Salary=(Total_Present * Monthly_Salary / 30);
					//Response.Write(Net_Salary+"&nbsp;&nbsp;"); 
					Net_Salary=Net_Salary + OT_Compensation * Total_OverTime;
					float min = 0.0f;
					if(mn1 != 0)
					{
						if(mn1 == 15)
							min = 0.25f;
						if(mn1 == 30)
							min = 0.50f;
						if(mn1 == 45)
							min = 0.75f;
					}
					Net_Salary=Net_Salary + (OT_Compensation * min);
					//Net_Salarytot1=0;
					Net_Salarytot+=double.Parse(GenUtil.strNumericFormat(Net_Salary.ToString()));
					//Coment by vikas 19.12.2012 Tot_Net_Salary+=double.Parse(GenUtil.strNumericFormat(Net_Salary.ToString()));
					Tot_Net_Salary+=Math.Round(double.Parse(Net_Salary.ToString()));
					Net_Salarytot1=Net_Salarytot;
					//Response.Write(Net_Salary+"<br>"); 
					//coment by vikas 18.11.2012 e.Item.Cells[8].Text = GenUtil.strNumericFormat(Net_Salary.ToString());
					e.Item.Cells[7].Text = Convert.ToString(Math.Round(double.Parse(Net_Salary.ToString())));
				
				}
				#endregion
				//****************
				#region Bind Total Advance Regarding Each Employee
				//**sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+DateTime.Now.Year +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"' and emp_id='"+ emp_id +"'";
				string Ledger_ID="",str11="",str12="",str="";
				SqlDtr = null;
				Ledger_ID="Select Ledger_ID from Ledger_Master where Ledger_Name ='"+e.Item.Cells[1].Text.ToString()+"'";
				dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='"+e.Item.Cells[1].Text.ToString()+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Ledger_ID = SqlDtr["Ledger_ID"].ToString(); 
				}
				SqlDtr.Close();
				if(Ledger_ID!="")
				{
					sql="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Payment%') and Ledger_ID="+Ledger_ID;
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows )
					{
						while(SqlDtr.Read())
						{
							if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							{
								str11=SqlDtr.GetValue(0).ToString();
							}
						}
					}
					else
					{
						str11="0";
					}
					SqlDtr.Close();

					sql="select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID="+Ledger_ID+" and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'";
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows )
					{
						while(SqlDtr.Read())
						{
							if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							{
								str12=SqlDtr.GetValue(0).ToString();
							}
						}
						SqlDtr.Close();
					}
					else
					{
						str12="0";
					}
					SqlDtr.Close();
				}
				//**SqlDtr.Close();
				if(str12 != "" && str11 != "")
					str=System.Convert.ToString(System.Convert.ToDouble(str11)+System.Convert.ToDouble(str12));
				else
				{
					if(str11 !="")
						str=str11;
					else
						str=str12;
				}
				Tot_Advance+=double.Parse(str.ToString());
				//string ddd = e.Item.Cells[9].Text.ToString();
				/* Coment by vikas 18.11.2012 if(!e.Item.Cells[9].Text.ToString().Equals("Advance"))
					e.Item.Cells[9].Text=str;*/

				if(!e.Item.Cells[8].Text.ToString().Equals("Advance"))
					e.Item.Cells[8].Text=str;
				#endregion
				//*************

				/***********Add by vikas 18.11.2012******************************/
				double TotalReceipt=0;
				InventoryClass obj1 = new InventoryClass();
				SqlDataReader rdr = obj1.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+emp_id+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
					else
						TotalReceipt=0;
				}
				rdr.Close();

				/***************************************************************/
				double bounce=0;
				rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+emp_id+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						bounce = double.Parse(rdr.GetValue(0).ToString());
					else
						bounce=0;
				}
				rdr.Close();
				TotalReceipt-=bounce;
				//TotalAmount[4]+=bounce;
				//return GenUtil.strNumericFormat(bounce.ToString());

				double cd =0;
				rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+emp_id+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						cd=double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				TotalReceipt-=cd;
				//TotalAmount[1]+=cd;
				//return GenUtil.strNumericFormat(cd.ToString());

				double cn=0;
				rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+emp_id+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						cn=double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				TotalReceipt-=cn;
				//TotalAmount[3]+=cn;

				double sd =0;
				//SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and particulars like 'Receipt_sd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
				rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+emp_id+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="")
						sd=double.Parse(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				TotalReceipt-=sd;
				//TotalAmount[2]+=sd;
				//GenUtil.strNumericFormat(sd.ToString());

				/*********************/

				SqlDataReader dtr=null;
				double ssrinc=0;
				sql="select * from setDis";
				//InventoryClass obj2 = new InventoryClass();
				dtr=obj1.GetRecordSet(sql);
				while(dtr.Read())
				{
					if(dtr["SSRincentiveStatus"].ToString()!="0")
						ssrinc=double.Parse(dtr["SSRincentive"].ToString());
					else
						ssrinc=0;
				}
				dtr.Close();

				double totInc=Math.Round(TotalReceipt*ssrinc/100);
				//double totInc=TotalReceipt*ssrinc/100;
				Tot_Incentive+=totInc;
				if(!e.Item.Cells[9].Text.ToString().Equals("Incentive"))
					e.Item.Cells[9].Text=totInc.ToString();
				
				if(str=="")
					str="0";

				double Final_Tot=Math.Round((Net_Salary+totInc)-double.Parse(str.ToString()));
				G_Tot_Salary+=Final_Tot;
				if(!e.Item.Cells[10].Text.ToString().Equals("Total Salery-Advance + Incentive"))
					e.Item.Cells[10].Text=Final_Tot.ToString();

				/************Ad by vikas 21.11.2012*****************************/
				if(Ledger_ID!="")
				{
					sql="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Journal%') and Ledger_ID="+Ledger_ID;
					SqlDtr =obj.GetRecordSet(sql);
					if(SqlDtr.HasRows )
					{
						while(SqlDtr.Read())
						{
							//if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							if(!SqlDtr.GetValue(0).ToString().Equals("NULL") && !SqlDtr.GetValue(0).ToString().Equals("") && SqlDtr.GetValue(0).ToString() != null)
							{
								str11=SqlDtr.GetValue(0).ToString();
							}
							else
							{
								str11="0";
							}
						
						}
					}
					else
					{
						str11="0";
					}
					SqlDtr.Close();
				
					if(!e.Item.Cells[11].Text.ToString().Equals("TA/DA"))
						e.Item.Cells[11].Text=str11.ToString();
					Tot_TADA+=double.Parse(str11);

					//coment by vikas 20.12.2012 double Net_Exp=Final_Tot+double.Parse(str11);
					double Net_Exp=totInc+double.Parse(str11)+Net_Salary;       //20.12.2012
					
					Net_Exp=Math.Round(Net_Exp);
					
					Tot_Expendeture+=Net_Exp;

					
					if(!e.Item.Cells[12].Text.ToString().Equals("Net Expenditure to Employees"))
						e.Item.Cells[12].Text=Net_Exp.ToString();
				}
				/************End*****************************/
				//
			}
			catch(Exception ex)
			{
				//CreateLogFiles.ErrorLog("Form : Salary_statement3.aspx,Method:GridMachineReport_ItemDataBound  Exception "+ex.Message+" userid is   "+uid);
			}
		}

		//*****************bhal end****************************************
		protected void DropMonth_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//***	txtYear.Text=System.DateTime.Today.Year.ToString();
		}

		protected void GridMachineReport_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Method to write into the report file to print.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnprint_Click(object sender, System.EventArgs e)
		{
			try
			{
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:btnprint_Click   "+ uid);
				GetData();
				Print();	
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:btnprint_Click   "+ ex.Message+" EXCEPTION "+uid);
			}
		}

		/// <summary>
		/// This method writes a line to a report file.
		/// </summary>
		/// <param name="sw"></param>
		/// <param name="info"></param>
		public void Write2File1(StreamWriter sw, string info)
		{
			sw.WriteLine(info);			
		}

		/// <summary>
		/// Prepares the report file SalaryStatement.txt for printing.
		/// </summary>
		public void GetData()
		{   

			/*******************************
			string sql="";
			
			int Total_Present;
			float Total_OverTime;
			double Monthly_Salary;
			double OT_Compensation;			
			double Net_Salary;
			double grandTotal=0;
			
			string str1, str2, str3, str4, str5, str6, str7, info;
			str6 = "";
			SqlDataReader SqlDtr, SqlDtrOP, SqlDtrOT,SqlDtr1;
			int Days_in_Months=30;

			EmployeeClass  obj=new EmployeeClass();
			EmployeeClass  obj2=new EmployeeClass();
			EmployeeClass  obj3=new EmployeeClass();
			EmployeeClass  obj4=new EmployeeClass();
			EmployeeClass  obj5=new EmployeeClass();
			EmployeeClass  obj6=new EmployeeClass();
			
			try
			{
				//*1*			sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex) +"/"+dropyear.SelectedIndex +"' ";
				 

				SqlDtr =obj.GetRecordSet(sql);
				if(SqlDtr != null)
				{
					while(SqlDtr.Read())
					{
						if(SqlDtr.GetValue(0).ToString().Equals("NULLS") || !SqlDtr.GetValue(0).ToString().Trim().Equals("") )
						{
							//GridMachineReport.Visible = true;
						}
						else
						{
							//GridMachineReport.Visible = false;
							MessageBox.Show("Details not available");
							return;
						}
					}
				}
				SqlDtr.Close();

				int diff = System.Convert.ToInt32(sp.Days)+1;
				//*11*			string from_date = DropMonth.SelectedIndex+"/"+diff+"/"+dropyear.SelectedIndex;
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					//*2*				from_date = DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex;
				}
				
               
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					diff = 30;
				}
				else
				{
					//*3*				if(DateTime.DaysInMonth(dropyear.SelectedIndex,DateTime.Now.Month) == diff)
											diff = 30;
				}

				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalaryReport.txt";
				StreamWriter sw = new StreamWriter(path);
				// Condensed
				sw.Write((char)27);//added by vishnu
				sw.Write((char)67);//added by vishnu
				sw.Write((char)0);//added by vishnu
				sw.Write((char)12);//added by vishnu
			
				sw.Write((char)27);//added by vishnu
				sw.Write((char)78);//added by vishnu
				sw.Write((char)5);//added by vishnu
							
				sw.Write((char)27);//added by vishnu
				sw.Write((char)15);
				//**********
				string des="--------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				//*4*			string title = "			Salary Report As On " + DropMonth.SelectedItem.Text + " " + dropyear.SelectedItem.Text;
				Write2File1(sw,title);
				Write2File1(sw,"");
				
				Write2File1(sw,"+----+-------------------------+--------+-----+-----+-----+-----+-----+--------+");				
				Write2File1(sw,"|Emp.|          Name           |Monthly | OT  |Total|Leave|Total| OT  |  Net   |");
				Write2File1(sw,"|ID  |                         |Salary  |Comp.|Days |     |Pres.| Hrs | Salary |");
				Write2File1(sw,"+----+-------------------------+--------+-----+-----+-----+-----+-----+--------+" );                                                                                        
				//1234 1234567890123456789012345 12345678 12345  23    23    23   12345 12345678			 
				info = " {0,4:D} {1,-25:S} {2,8:F} {3,5:F}  {4,2:F}    {5,2:F}    {6,2:F}   {7,5:F} {8,8:F}";
				//coment by vikas 29.10.2012 sql="select emp_id,emp_name, salary, ot_compensation from employee";
				sql="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
				SqlDtr = obj.GetRecordSet(sql);
				string strQueryOT  ="";
				string strQueryOP = "";
				double hr1=0;
				string strh="";
				while(SqlDtr.Read())                
				{
					//info = "";
					str1 = SqlDtr.GetValue(0).ToString ();
					str2 = SqlDtr.GetValue(1).ToString ();
					str3 = SqlDtr.GetValue(2).ToString ();
					str4 = SqlDtr.GetValue(3).ToString ();
					int leave = 0;
					
					//*5*		sql = "select sum(datepart(day,dateadd(day,1,date_to)) - datepart(day,date_from)) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ str1  +"' and isSanction = 1";
							
					SqlDtr1 =obj4.GetRecordSet(sql);
					if(SqlDtr1.HasRows )
					{
						if(SqlDtr1.Read())
						{
							if(!SqlDtr1.GetValue(0).ToString().Trim().Equals(""))
								leave = System.Convert.ToInt32(SqlDtr1.GetValue(0).ToString()) ;
						}
					}
					SqlDtr1.Close();

					
					//*6*				sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ str1 +"' and isSanction = 1";
					SqlDtr1 =obj5.GetRecordSet(sql);
					if(SqlDtr1.HasRows  )
					{
						if(SqlDtr1.Read())
						{
							if(!SqlDtr1.GetValue(0).ToString().Trim().Equals(""))
								leave += System.Convert.ToInt32(SqlDtr1.GetValue(0).ToString()) ;
						}
					}
					SqlDtr1.Close();

					
					//*7*				sql = "select sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ str1 +"' and isSanction = 1";
					
					SqlDtr1 =obj6.GetRecordSet(sql);
					if(SqlDtr1.HasRows )
					{
						if(SqlDtr1.Read())
						{
							if(!SqlDtr1.GetValue(0).ToString().Trim().Equals(""))
								leave += System.Convert.ToInt32(SqlDtr1.GetValue(0).ToString()) ;
						}
					}
					SqlDtr1.Close();
                
					//*8*				strQueryOP="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id="+ str1;
					           
					SqlDtrOP = obj2.GetRecordSet(strQueryOP);	

					if (SqlDtrOP.Read())
					{
						str5 = SqlDtrOP.GetValue(0).ToString ();
					}
					else
					{
						str5 = "0";
					}
                
					SqlDtrOP.Close();

					/*9*			strQueryOT = "select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id=" + str1;
					             
					SqlDtrOT = obj3.GetRecordSet(strQueryOT);	
					string	hr2="";
					string strm="";
					float hr = 0.0f;
					float mn1 = 0.0f;
					if (SqlDtrOT.Read())
					{
						strh=SqlDtrOT.GetValue(0).ToString();
						if(strh==null||strh.Equals(""))
						{
							strh="0";
						}
						strm=SqlDtrOT.GetValue(1).ToString();
						if(strm==null || strm.Equals(""))
						{
							strm="0";
						}
									
						
                            
							
						// Calculate Total Present Hours
						hr= float.Parse(strm) / 60  +  float.Parse(strh);
						
						//Calculate total minutes
						
						mn1=float.Parse(strm)% 60;
						hr1=System.Math.Floor(System.Convert.ToDouble(hr));
							
						
							
						hr2=hr1.ToString()+"."+mn1.ToString() ;
						//str6 =hr2;
						
					}
					else
					{
						str6 = "0";
					}
					if (str5.Equals(""))
					{
						str5 = "0";
					}
					if (str6.Equals(""))
					{
						str6 = "0";
					}

					Monthly_Salary=System.Convert.ToDouble(str3);
					string st = "24" ;
					Total_Present=System.Convert.ToInt32(str5);
					Total_OverTime=float.Parse(hr1.ToString()) ;
					if(Total_OverTime<0)
					{
						Total_OverTime=Total_OverTime+float.Parse(st);
					}
					str6 = Total_OverTime.ToString()+"."+mn1.ToString();
					OT_Compensation=System.Convert.ToDouble(str4);
					// Calculate Net Salary.
					Net_Salary=(Total_Present * Monthly_Salary / Days_in_Months);
					Net_Salary=Net_Salary + OT_Compensation * Total_OverTime;
					float min = 0.0f;
					if(mn1 != 0)
					{
						if(mn1 == 15)
							min = 0.25f;
						if(mn1 == 30)
							min = 0.50f;
						if(mn1 == 45)
							min = 0.75f;
					}
					Net_Salary=Net_Salary + (OT_Compensation * min);
					Net_Salary=System.Math.Round(Net_Salary,2);
					grandTotal = grandTotal + Net_Salary;
					str7=System.Convert.ToString(Net_Salary);
					SqlDtrOT.Close();
					sw.WriteLine(info,str1,str2,GenUtil.strNumericFormat(str3),str4,diff.ToString(),leave.ToString() ,str5,GenUtil.strNumericFormat(str6),GenUtil.strNumericFormat(str7));
				}
				Write2File1(sw,"+----+-------------------------+--------+-----+-----+-----+-----+-----+--------+" );                                                                                        
				sw.WriteLine("                                                                 Total:{0,8:F}",GenUtil.strNumericFormat(grandTotal.ToString()));
				Write2File1(sw,"+----+-------------------------+--------+-----+-----+-----+-----+-----+--------+" );                                                                                        
				sw.Close();
				SqlDtr.Close();	
				********************************/
				 
			int Total_Present=0;
			float Total_OverTime=0;
			double Monthly_Salary=0;
			double OT_Compensation=0;
			double Net_Salary=0;
			try
			{

				string Emp_id="", Emp_Name="", Working_Day="", Extra_Days="", Total_day="", Basic_Salary="",Advance="",Incentive="",Tot_Adv_Incent="",Ta_Da="",Net_Expences="";
				SqlDataReader SqlDtr, SqlDtrOP, SqlDtrOT,SqlDtr1;
				EmployeeClass  obj=new EmployeeClass();
				EmployeeClass  obj2=new EmployeeClass();
				EmployeeClass  obj3=new EmployeeClass();
				EmployeeClass  obj4=new EmployeeClass();
				EmployeeClass  obj5=new EmployeeClass();
				EmployeeClass  obj6=new EmployeeClass();

				string sql;
				int diff = System.Convert.ToInt32(sp.Days)+1;
				
				string from_date = DropMonth.SelectedIndex+"/"+diff+"/"+dropyear.SelectedIndex;
				// if the current month is not equals to selected month then set the to date i.e(from_date) as th final date of the month.
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					from_date = DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex;
				}
				
				// if the month is not current month then diff  =  30;
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					diff = 30;
				}
				else
				{
					if(DateTime.DaysInMonth(dropyear.SelectedIndex,DateTime.Now.Month) == diff)
						diff = 30;
				}
				string des=    "------------------------------------------------------------------------------------------------------------------------";
				string title = "			                   Salary Report As On " + DropMonth.SelectedItem.Text + " " + dropyear.SelectedItem.Text;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SalaryReport1.txt";
				StreamWriter sw = new StreamWriter(path);
				// Condensed
				sw.Write((char)27);//added by vishnu
				sw.Write((char)67);//added by vishnu
				sw.Write((char)0);//added by vishnu
				sw.Write((char)12);//added by vishnu
			
				sw.Write((char)27);//added by vishnu
				sw.Write((char)78);//added by vishnu
				sw.Write((char)5);//added by vishnu
							
				sw.Write((char)27);//added by vishnu
				sw.Write((char)15);
				//**********
				
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				/*4*/			
				Write2File1(sw,title);
				Write2File1(sw,"");
				
				
				sql="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
				sql+=" order by "+strorderby;

				SqlDtr1 = obj2.GetRecordSet(sql);
				double hr1=0;
				
				if(SqlDtr1.HasRows)
				{
					//sw.WriteLine("Emp ID\tEmployee Name\tWorking Days\tLeave\tXtra Days\tTotal Days - Leave+ Xtra Days Working\t Monthly Salary\tNet Salary\tAdvance	Incentive\tTotal Salery-Advance + Incentive\tTA/DA\tNet Expenditure to Employees");
					Write2File1(sw,"+----+-------------------------+--------+-------+-----+----------+-------+------+-------+---------+----------+---------+-----------+");				
					Write2File1(sw,"|(1) |         (2)             |   (3)  |  (4)  | (5) |    (6)   |  (7)  |  (8) |  (9)  |   (10)  |   (11)   |  (12)   |   (13)    |");				
					Write2File1(sw,"|Emp.|     Employee Name       | Working| Leave |Extra|Total Days| Basic |  Net |Advance|Incentive|Tot.Salary|  TA-DA  |  Tot Exp. |");
					Write2File1(sw,"|ID  |                         |  days  |       |Days | (4)+(5)  |Salary |Salary|       |         |  (9)+(10)|         | Employees |");
					Write2File1(sw,"+----+-------------------------+--------+-------+-----+----------+------+-------+-------+---------+----------+---------+-----------+"); 
								  // 0123 0123456789012345678901234 01234567 0123456 01234 0123456789 012345 0123456 0123456 012345678 0123456789 012345678 01234567890
					string info = " {0,-4:S} {1,-25:S} {2,-8:S} {3,-7:S} {4,-5:S} {5,-10:S} {6,6:S} {7,7:S} {8,7:S} {9,9:S} {10,10:S} {11,9:S} {12,10:S} ";
					while(SqlDtr1.Read())                
					{
						Emp_id = SqlDtr1.GetValue(0).ToString ();
						Emp_Name = SqlDtr1.GetValue(1).ToString ();
						Basic_Salary = SqlDtr1.GetValue(2).ToString ();
						Extra_Days = SqlDtr1.GetValue(3).ToString ();

						int leave = 0;
						sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ Emp_id +"' and isSanction = 1";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows )
						{
							if(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
									leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
							}
						}
						SqlDtr.Close();
					
						sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ Emp_id +"' and isSanction = 1 and datepart(month,date_from) = datepart(month,'"+from_date +"')";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows  )
						{
							if(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
									leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
							}
						}
						SqlDtr.Close();
						sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '"+from_date +"' then sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,'"+from_date +"'))) else sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ Emp_id +"' and isSanction = 1 group by date_to";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows )
						{
							if(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
									leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
							}
						}
						SqlDtr.Close();

						string Present="0";
						#region Bind Total Present Regarding Each Item				
						sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id +"'";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows )
						{
							while(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
								{
									//e.Item.Cells[2].Text=SqlDtr.GetValue(0).ToString() ;
									Present=SqlDtr.GetValue(0).ToString();
								}	
							}
						}
						totpresenttot += System.Convert.ToDouble(Present.ToString()) ;
						totpresenttot1=totpresenttot;
						SqlDtr.Close();
						#endregion

						Working_Day=Convert.ToString((double.Parse(Present.ToString())-leave));

						#region Bind Total OverTime Hours Regarding Each Item
						string	Sql1 ="select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id+"'";
						float hr=0;
						float mn1=0;
						SqlDtr = obj.GetRecordSet(Sql1);
						if(SqlDtr != null)
						{
							if(SqlDtr.HasRows)
							{
								if (SqlDtr.Read())
								{
									string	strh11=SqlDtr.GetValue(0).ToString();
									if(strh11==null||strh11.Equals(""))
									{
										strh11="0";
									}
									string	strm11=SqlDtr.GetValue(1).ToString();
									if(strm11==null || strm11.Equals(""))
									{
										strm11="0";
									}
									hr= float.Parse(strm11) / 60  +  float.Parse(strh11);
									mn1=float.Parse(strm11)% 60;
									hr1=System.Math.Floor(System.Convert.ToDouble(hr));
									string st="24";
									Total_OverTime=float.Parse(hr1.ToString() );
									if(Total_OverTime<0)
									{
										Total_OverTime=Total_OverTime+float.Parse(st);
									}
									string	hr2=Total_OverTime.ToString()+"."+mn1.ToString() ;
									Net_Salary=double.Parse(GenUtil.strNumericFormat(hr2).ToString());
									othtot+=double.Parse(GenUtil.strNumericFormat(hr2));
									othtot1=othtot;
								}
							}
						}
						SqlDtr.Close();
						#endregion
						#region Calculate Net Salary
						Monthly_Salary=System.Convert.ToDouble(Basic_Salary.ToString());
						OT_Compensation=System.Convert.ToDouble(Extra_Days.ToString());
					
						Total_Present=System.Convert.ToInt32(Present.ToString());
						Net_Salary=(Total_Present * Monthly_Salary / 30);
						Net_Salary=Net_Salary + OT_Compensation * Total_OverTime;
						float min = 0.0f;
						if(mn1 != 0)
						{
							if(mn1 == 15)
								min = 0.25f;
							if(mn1 == 30)
								min = 0.50f;
							if(mn1 == 45)
								min = 0.75f;
						}
						Net_Salary=Net_Salary + (OT_Compensation * min);
						Net_Salarytot+=double.Parse(GenUtil.strNumericFormat(Net_Salary.ToString()));
						Net_Salarytot1=Net_Salarytot;
						Net_Salary =double.Parse(GenUtil.strNumericFormat(Net_Salary.ToString()));
				
						#endregion
						#region Bind Total Advance Regarding Each Employee
						string Ledger_ID="",str11="",str12="",str="";
						SqlDtr = null;
						dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='"+Emp_Name.ToString()+"'",ref SqlDtr);
						if(SqlDtr.Read())
						{
							Ledger_ID = SqlDtr["Ledger_ID"].ToString(); 
						}
						SqlDtr.Close();
						if(Ledger_ID!="")
						{
							sql="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Payment%') and Ledger_ID="+Ledger_ID;
							SqlDtr =obj.GetRecordSet(sql);
							if(SqlDtr.HasRows )
							{
								while(SqlDtr.Read())
								{
									if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
									{
										str11=SqlDtr.GetValue(0).ToString();
									}
								}
							}
							SqlDtr.Close();
							sql="select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID="+Ledger_ID+" and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'";
							SqlDtr =obj.GetRecordSet(sql);
							if(SqlDtr.HasRows )
							{
								while(SqlDtr.Read())
								{
									if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
									{
										str12=SqlDtr.GetValue(0).ToString();
									}
								}
							}
							SqlDtr.Close();
						}
						if(str12 != "" && str11 != "")
							str=System.Convert.ToString(System.Convert.ToDouble(str11)+System.Convert.ToDouble(str12));
						else
						{
							if(str11 !="")
								str=str11;
							else
								str=str12;
						}
						Advance=str;
						#endregion
						double TotalReceipt=0;
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr = obj1.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
							else
								TotalReceipt=0;
						}
						rdr.Close();


						/**********************/
						double bounce=0;
						rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								bounce = double.Parse(rdr.GetValue(0).ToString());
							else
								bounce=0;
						}
						rdr.Close();
						TotalReceipt-=bounce;
						//TotalAmount[4]+=bounce;
						//return GenUtil.strNumericFormat(bounce.ToString());



						double cd =0;
						rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								cd=double.Parse(rdr.GetValue(0).ToString());
						}
						rdr.Close();
						TotalReceipt-=cd;
						//TotalAmount[1]+=cd;
						//return GenUtil.strNumericFormat(cd.ToString());

						double cn=0;
						rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								cn=double.Parse(rdr.GetValue(0).ToString());
						}
						rdr.Close();
						TotalReceipt-=cn;
						//TotalAmount[3]+=cn;

						double sd =0;
						//SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and particulars like 'Receipt_sd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
						rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								sd=double.Parse(rdr.GetValue(0).ToString());
						}
						rdr.Close();
						TotalReceipt-=sd;
					

						/*********************/



						SqlDataReader dtr=null;
						double ssrinc=0;
						sql="select * from setDis";
				
						dtr=obj1.GetRecordSet(sql);
						while(dtr.Read())
						{
							if(dtr["SSRincentiveStatus"].ToString()!="0")
								ssrinc=double.Parse(dtr["SSRincentive"].ToString());
							else
								ssrinc=0;
						}
						dtr.Close();

						double totInc=TotalReceipt*ssrinc/100;

						Incentive=Convert.ToString(Math.Round(double.Parse(totInc.ToString())));
				
						if(str=="")
							str="0";

						double Final_Tot=Math.Round((Net_Salary+totInc)-double.Parse(str.ToString()),2);
				
						Tot_Adv_Incent=Final_Tot.ToString();

						if(Ledger_ID!="")
						{
							sql="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Journal%') and Ledger_ID="+Ledger_ID;
							SqlDtr =obj.GetRecordSet(sql);
							if(SqlDtr.HasRows )
							{
								while(SqlDtr.Read())
								{
									if(!SqlDtr.GetValue(0).ToString().Equals("NULL") && !SqlDtr.GetValue(0).ToString().Equals("") && SqlDtr.GetValue(0).ToString() != null)
									{
										str11=SqlDtr.GetValue(0).ToString();
									}
									else
									{
										str11="0";
									}
						
								}
							}
							else
							{
								str11="0";
							}
							SqlDtr.Close();
				
							Ta_Da=Convert.ToString(Math.Round(double.Parse(str11.ToString())));
                            
							//coment by vikas 20.12.2012 double Net_Exp=Math.Round(Final_Tot+double.Parse(str11),2);
							double Net_Exp=totInc+double.Parse(str11)+Net_Salary;       //20.12.2012

							Net_Expences=Net_Exp.ToString();
						}
					
						//sw.WriteLine(Emp_id+"\t"+Emp_Name+"\t"+Working_Day+"\t"+leave+"\t"+Extra_Days+"\t\t"+Basic_Salary+"\t"+Net_Salary+"\t"+Advance+"\t"+Incentive+"\t"+Tot_Adv_Incent+"\t"+Ta_Da+"\t"+Net_Expences+"\t");
						sw.WriteLine(info,Emp_id.ToString(),GenUtil.TrimLength(Emp_Name.ToString(),24),Working_Day.ToString(),leave.ToString(),Extra_Days.ToString(),"",Basic_Salary.ToString(), Convert.ToString(Math.Round(Net_Salary)),Advance.ToString(),Incentive.ToString(),Convert.ToString(Math.Round(double.Parse(Tot_Adv_Incent.ToString()))),Ta_Da.ToString(),Convert.ToString(Math.Round(double.Parse(Net_Expences.ToString()))));

					}
					SqlDtr1.Close();
					Write2File1(sw,"+----+-------------------------+--------+-------+-----+----------+-------+------+-------+---------+----------+---------+-----------+");
					sw.WriteLine(info,"","Total","","","","","",Tot_Net_Salary.ToString(),Tot_Advance.ToString(),Tot_Incentive.ToString(),G_Tot_Salary.ToString(),Tot_TADA.ToString(),Tot_Expendeture.ToString());				
					Write2File1(sw,"+----+-------------------------+--------+-------+-----+----------+-------+------+-------+---------+----------+---------+-----------+");				
					sw.Close();
				}
				 
				/* ********************************/
			}
			catch(Exception ex)
			{		
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:GetData "+ ex.Message+" EXCEPTION "+uid);
			}
		}
	 

		public void Convertinto_Excel()
		{
			int Total_Present=0;
			float Total_OverTime=0;
			double Monthly_Salary=0;
			double OT_Compensation=0;
			double Net_Salary=0;
			try
			{

				string Emp_id="", Emp_Name="", Working_Day="", Extra_Days="", Total_day="", Basic_Salary="",Advance="",Incentive="",Tot_Adv_Incent="",Ta_Da="",Net_Expences="";
				SqlDataReader SqlDtr, SqlDtrOP, SqlDtrOT,SqlDtr1;
				EmployeeClass  obj=new EmployeeClass();
				EmployeeClass  obj2=new EmployeeClass();
				EmployeeClass  obj3=new EmployeeClass();
				EmployeeClass  obj4=new EmployeeClass();
				EmployeeClass  obj5=new EmployeeClass();
				EmployeeClass  obj6=new EmployeeClass();

				string sql;
				int diff = System.Convert.ToInt32(sp.Days)+1;
				
				string from_date = DropMonth.SelectedIndex+"/"+diff+"/"+dropyear.SelectedIndex;
				// if the current month is not equals to selected month then set the to date i.e(from_date) as th final date of the month.
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					from_date = DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex;
				}
				
				// if the month is not current month then diff  =  30;
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					diff = 30;
				}
				else
				{
					if(DateTime.DaysInMonth(dropyear.SelectedIndex,DateTime.Now.Month) == diff)
						diff = 30;
				}

				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2);
				string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
				Directory.CreateDirectory(strExcelPath);
				string path = home_drive+@"\Servosms_ExcelFile\Export\Salary_Statement1.xls";
				StreamWriter sw = new StreamWriter(path);
				
				sql="select emp_id,emp_name, salary, ot_compensation from employee where status='1' ";

				sql+=" order by "+strorderby;

				SqlDtr1 = obj2.GetRecordSet(sql);
				double hr1=0;

				if(SqlDtr1.HasRows)
				{
					sw.WriteLine("Emp ID\tEmployee Name\tWorking Days\tLeave\tXtra Days\tTotal Days - Leave+ Xtra Days Working\t Monthly Salary\tNet Salary\tAdvance	Incentive\tTotal Salery-Advance + Incentive\tTA/DA\tNet Expenditure to Employees");

					while(SqlDtr1.Read())                
					{
						

						Emp_id = SqlDtr1.GetValue(0).ToString ();
						Emp_Name = SqlDtr1.GetValue(1).ToString ();
						Basic_Salary = SqlDtr1.GetValue(2).ToString ();
						Extra_Days = SqlDtr1.GetValue(3).ToString ();

						int leave = 0;
						sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ Emp_id +"' and isSanction = 1";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows )
						{
							if(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
									leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
							}
						}
						SqlDtr.Close();
					
						sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ Emp_id +"' and isSanction = 1 and datepart(month,date_from) = datepart(month,'"+from_date +"')";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows  )
						{
							if(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
									leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
							}
						}
						SqlDtr.Close();
						sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '"+from_date +"' then sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,'"+from_date +"'))) else sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ Emp_id +"' and isSanction = 1 group by date_to";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows )
						{
							if(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
									leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
							}
						}
						SqlDtr.Close();

						string Present="0";
						#region Bind Total Present Regarding Each Item				
						sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id +"'";
						SqlDtr =obj.GetRecordSet(sql);
						if(SqlDtr.HasRows )
						{
							while(SqlDtr.Read())
							{
								if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
								{
									//e.Item.Cells[2].Text=SqlDtr.GetValue(0).ToString() ;
									Present=SqlDtr.GetValue(0).ToString();
								}	
							}
						}
						totpresenttot += System.Convert.ToDouble(Present.ToString()) ;
						totpresenttot1=totpresenttot;
						SqlDtr.Close();
						#endregion

						Working_Day=Convert.ToString((double.Parse(Present.ToString())-leave));

						#region Bind Total OverTime Hours Regarding Each Item
						string	Sql1 ="select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id+"'";
						float hr=0;
						float mn1=0;
						SqlDtr = obj.GetRecordSet(Sql1);
						if(SqlDtr != null)
						{
							if(SqlDtr.HasRows)
							{
								if (SqlDtr.Read())
								{
									string	strh11=SqlDtr.GetValue(0).ToString();
									if(strh11==null||strh11.Equals(""))
									{
										strh11="0";
									}
									string	strm11=SqlDtr.GetValue(1).ToString();
									if(strm11==null || strm11.Equals(""))
									{
										strm11="0";
									}
									hr= float.Parse(strm11) / 60  +  float.Parse(strh11);
									mn1=float.Parse(strm11)% 60;
									hr1=System.Math.Floor(System.Convert.ToDouble(hr));
									string st="24";
									Total_OverTime=float.Parse(hr1.ToString() );
									if(Total_OverTime<0)
									{
										Total_OverTime=Total_OverTime+float.Parse(st);
									}
									string	hr2=Total_OverTime.ToString()+"."+mn1.ToString() ;
									Net_Salary=double.Parse(GenUtil.strNumericFormat(hr2).ToString());
									othtot+=double.Parse(GenUtil.strNumericFormat(hr2));
									othtot1=othtot;
								}
							}
						}
						SqlDtr.Close();
						#endregion
						#region Calculate Net Salary
						Monthly_Salary=System.Convert.ToDouble(Basic_Salary.ToString());
						OT_Compensation=System.Convert.ToDouble(Extra_Days.ToString());
					
						Total_Present=System.Convert.ToInt32(Present.ToString());
						Net_Salary=(Total_Present * Monthly_Salary / 30);
						Net_Salary=Net_Salary + OT_Compensation * Total_OverTime;
						float min = 0.0f;
						if(mn1 != 0)
						{
							if(mn1 == 15)
								min = 0.25f;
							if(mn1 == 30)
								min = 0.50f;
							if(mn1 == 45)
								min = 0.75f;
						}
						Net_Salary=Net_Salary + (OT_Compensation * min);
						Net_Salarytot+=double.Parse(GenUtil.strNumericFormat(Net_Salary.ToString()));
						Net_Salarytot1=Net_Salarytot;
						Net_Salary =double.Parse(GenUtil.strNumericFormat(Net_Salary.ToString()));
				
						#endregion
						#region Bind Total Advance Regarding Each Employee
						string Ledger_ID="",str11="",str12="",str="";
						SqlDtr = null;
						dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='"+Emp_Name.ToString()+"'",ref SqlDtr);
						if(SqlDtr.Read())
						{
							Ledger_ID = SqlDtr["Ledger_ID"].ToString(); 
						}
						SqlDtr.Close();
						if(Ledger_ID!="")
						{
							sql="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Payment%') and Ledger_ID="+Ledger_ID;
							SqlDtr =obj.GetRecordSet(sql);
							if(SqlDtr.HasRows )
							{
								while(SqlDtr.Read())
								{
									if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
									{
										str11=SqlDtr.GetValue(0).ToString();
									}
								}
							}
							SqlDtr.Close();
							sql="select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID="+Ledger_ID+" and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'";
							SqlDtr =obj.GetRecordSet(sql);
							if(SqlDtr.HasRows )
							{
								while(SqlDtr.Read())
								{
									if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
									{
										str12=SqlDtr.GetValue(0).ToString();
									}
								}
							}
							SqlDtr.Close();
						}
						if(str12 != "" && str11 != "")
							str=System.Convert.ToString(System.Convert.ToDouble(str11)+System.Convert.ToDouble(str12));
						else
						{
							if(str11 !="")
								str=str11;
							else
								str=str12;
						}
						Advance=str;
						#endregion
						double TotalReceipt=0;
						InventoryClass obj1 = new InventoryClass();
						SqlDataReader rdr = obj1.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
							else
								TotalReceipt=0;
						}
						rdr.Close();


						/**********************/
						double bounce=0;
						rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								bounce = double.Parse(rdr.GetValue(0).ToString());
							else
								bounce=0;
						}
						rdr.Close();
						TotalReceipt-=bounce;

						double cd =0;
						rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								cd=double.Parse(rdr.GetValue(0).ToString());
						}
						rdr.Close();
						TotalReceipt-=cd;

						double cn=0;
						rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								cn=double.Parse(rdr.GetValue(0).ToString());
						}
						rdr.Close();
						TotalReceipt-=cn;

						double sd =0;
						rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								sd=double.Parse(rdr.GetValue(0).ToString());
						}
						rdr.Close();
						TotalReceipt-=sd;
						/*********************/


						SqlDataReader dtr=null;
						double ssrinc=0;
						sql="select * from setDis";
				
						dtr=obj1.GetRecordSet(sql);
						while(dtr.Read())
						{
							if(dtr["SSRincentiveStatus"].ToString()!="0")
								ssrinc=double.Parse(dtr["SSRincentive"].ToString());
							else
								ssrinc=0;
						}
						dtr.Close();

						double totInc=TotalReceipt*ssrinc/100;

						Incentive=totInc.ToString();
				
						if(str=="")
							str="0";

						double Final_Tot=Math.Round((Net_Salary+totInc)-double.Parse(str.ToString()),2);
				
						Tot_Adv_Incent=Final_Tot.ToString();

						if(Ledger_ID!="")
						{
							sql="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Journal%') and Ledger_ID="+Ledger_ID;
							SqlDtr =obj.GetRecordSet(sql);
							if(SqlDtr.HasRows )
							{
								while(SqlDtr.Read())
								{
									if(!SqlDtr.GetValue(0).ToString().Equals("NULL") && !SqlDtr.GetValue(0).ToString().Equals("") && SqlDtr.GetValue(0).ToString() != null)
									{
										str11=SqlDtr.GetValue(0).ToString();
									}
									else
									{
										str11="0";
									}
						
								}
							}
							else
							{
								str11="0";
							}
							SqlDtr.Close();
				
							Ta_Da=str11.ToString();

							//coment by vikas 20.12.2012 double Net_Exp=Final_Tot+double.Parse(str11);
							double Net_Exp=totInc+double.Parse(str11)+Net_Salary;       //20.12.2012

							Net_Expences=Net_Exp.ToString();
						}
					
						//coment by vikas 19.12.2012 sw.WriteLine(Emp_id+"\t"+Emp_Name+"\t"+Working_Day+"\t"+leave+"\t"+Extra_Days+"\t\t"+Basic_Salary+"\t"+Net_Salary+"\t"+Advance+"\t"+Incentive+"\t"+Tot_Adv_Incent+"\t"+Ta_Da+"\t"+Net_Expences+"\t");
						
						sw.WriteLine(Emp_id+"\t"+Emp_Name+"\t"+Working_Day+"\t"+leave+"\t"+Extra_Days+"\t\t"+Basic_Salary+"\t"+Convert.ToString(Math.Round(Net_Salary))+"\t"+Advance+"\t"+Convert.ToString(Math.Round(double.Parse(Incentive.ToString())))+"\t"+Convert.ToString(Math.Round(double.Parse(Tot_Adv_Incent.ToString())))+"\t"+Ta_Da+"\t"+Convert.ToString(Math.Round(double.Parse(Net_Expences.ToString())))+"\t");
						//sw.WriteLine(info,Emp_id.ToString(),GenUtil.TrimLength(Emp_Name.ToString(),24),Working_Day.ToString(),leave.ToString(),Extra_Days.ToString(),"",Basic_Salary.ToString(), Convert.ToString(Math.Round(Net_Salary)),Advance.ToString(),Incentive.ToString(),Convert.ToString(Math.Round(double.Parse(Tot_Adv_Incent.ToString()))),Ta_Da.ToString(),Convert.ToString(Math.Round(double.Parse(Net_Expences.ToString()))));
					}
					SqlDtr1.Close();
					sw.WriteLine();
					sw.WriteLine("\tTotal\t\t\t\t\t\t"+Tot_Net_Salary.ToString()+"\t"+Tot_Advance.ToString()+"\t"+Tot_Incentive.ToString()+"\t"+G_Tot_Salary.ToString()+"\t"+Tot_TADA.ToString()+"\t"+Tot_Expendeture.ToString());				
					sw.Close();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}


			/*string sql="";
			int Total_Present;
			float Total_OverTime;
			double Monthly_Salary;
			double OT_Compensation;			
			double Net_Salary;
			double grandTotal=0;
			
			string Emp_id="", Emp_Name="", Working_Day="", Extra_Days="", Total_day="", Basic_Salary="",Advance="",Incentive="",Tot_Adv_Incent="",Ta_Da="",Net_Expences="";
			SqlDataReader SqlDtr, SqlDtrOP, SqlDtrOT,SqlDtr1;
			int Days_in_Months=30;
			EmployeeClass  obj=new EmployeeClass();
			EmployeeClass  obj2=new EmployeeClass();
			EmployeeClass  obj3=new EmployeeClass();
			EmployeeClass  obj4=new EmployeeClass();
			EmployeeClass  obj5=new EmployeeClass();
			EmployeeClass  obj6=new EmployeeClass();
			
			try
			{
			
				 sql="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex) +"/"+dropyear.SelectedIndex +"' ";
				SqlDtr =obj.GetRecordSet(sql);
				if(SqlDtr != null)
				{
					while(SqlDtr.Read())
					{
						if(SqlDtr.GetValue(0).ToString().Equals("NULLS") || !SqlDtr.GetValue(0).ToString().Trim().Equals("") )
						{

						}
						else
						{
							MessageBox.Show("Details not available");
							return;
						}
					}
				}
				SqlDtr.Close();

				int diff = System.Convert.ToInt32(sp.Days)+1;
				string from_date = DropMonth.SelectedIndex+"/"+diff+"/"+dropyear.SelectedIndex;
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					from_date = DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex;
				}
				
               
				if(DropMonth.SelectedIndex != DateTime.Now.Month)
				{
					diff = 30;
				}
				else
				{
					if(DateTime.DaysInMonth(dropyear.SelectedIndex,DateTime.Now.Month) == diff)
						diff = 30;
				}

				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2);
				string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
				Directory.CreateDirectory(strExcelPath);
				string path = home_drive+@"\Servosms_ExcelFile\Export\Salary_Statement.xls";
				StreamWriter sw = new StreamWriter(path);
				
				sql="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";

				SqlDtr = obj.GetRecordSet(sql);
				string strQueryOT  ="";
				string strQueryOP = "";
				double hr1=0;
				string strh="";

				sw.WriteLine("Emp ID\tEmployee Name\tWorking Days\tLeave\tXtra Days\tTotal Days - Leave+ Xtra Days Working\t Monthly Salary\tNet Salary\tAdvance	Incentive\tTotal Salery-Advance + Incentive\tTA/DA\tNet Expenditure to Employees");

				while(SqlDtr.Read())                
				{
					Emp_id = SqlDtr.GetValue(0).ToString ();
					Emp_Name = SqlDtr.GetValue(1).ToString ();
					Basic_Salary = SqlDtr.GetValue(2).ToString ();
					Extra_Days = SqlDtr.GetValue(3).ToString ();
					int leave = 0;
					
					sql = "select sum(datepart(day,dateadd(day,1,date_to)) - datepart(day,date_from)) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ Emp_id  +"' and isSanction = 1";
							
					SqlDtr1 =obj4.GetRecordSet(sql);
					if(SqlDtr1.HasRows )
					{
						if(SqlDtr1.Read())
						{
							if(!SqlDtr1.GetValue(0).ToString().Trim().Equals(""))
								leave = System.Convert.ToInt32(SqlDtr1.GetValue(0).ToString()) ;
						}
					}
					SqlDtr1.Close();

					
					sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ Emp_id +"' and isSanction = 1";
					SqlDtr1 =obj5.GetRecordSet(sql);
					if(SqlDtr1.HasRows  )
					{
						if(SqlDtr1.Read())
						{
							if(!SqlDtr1.GetValue(0).ToString().Trim().Equals(""))
								leave += System.Convert.ToInt32(SqlDtr1.GetValue(0).ToString()) ;
						}
					}
					SqlDtr1.Close();

					
					sql = "select sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ Emp_id +"' and isSanction = 1";
					
					SqlDtr1 =obj6.GetRecordSet(sql);
					if(SqlDtr1.HasRows )
					{
						if(SqlDtr1.Read())
						{
							if(!SqlDtr1.GetValue(0).ToString().Trim().Equals(""))
								leave += System.Convert.ToInt32(SqlDtr1.GetValue(0).ToString()) ;
						}
					}
					SqlDtr1.Close();
                
					strQueryOP="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id="+ Emp_id;
					           
					SqlDtrOP = obj2.GetRecordSet(strQueryOP);	

					if (SqlDtrOP.Read())
					{
						Working_Day = SqlDtrOP.GetValue(0).ToString ();
					}
					else
					{
						Working_Day = "0";
					}
                
					SqlDtrOP.Close();

					strQueryOT = "select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id=" + Emp_id;
					             
					SqlDtrOT = obj3.GetRecordSet(strQueryOT);	
					string	hr2="";
					string strm="";
					float hr = 0.0f;
					float mn1 = 0.0f;
					if (SqlDtrOT.Read())
					{
						strh=SqlDtrOT.GetValue(0).ToString();
						if(strh==null||strh.Equals(""))
						{
							strh="0";
						}
						strm=SqlDtrOT.GetValue(1).ToString();
						if(strm==null || strm.Equals(""))
						{
							strm="0";
						}
									
					
						hr= float.Parse(strm) / 60  +  float.Parse(strh);
						mn1=float.Parse(strm)% 60;
						hr1=System.Math.Floor(System.Convert.ToDouble(hr));
						hr2=hr1.ToString()+"."+mn1.ToString() ;
						
					}
					else
					{
						str6 = "0";
					}
					if (str5.Equals(""))
					{
						str5 = "0";
					}
					if (str6.Equals(""))
					{
						str6 = "0";
					}

					Monthly_Salary=System.Convert.ToDouble(Basic_Salary);
					string st = "24" ;
					Total_Present=System.Convert.ToInt32(str5);
					Total_OverTime=float.Parse(hr1.ToString()) ;
					if(Total_OverTime<0)
					{
						Total_OverTime=Total_OverTime+float.Parse(st);
					}
					Extra_Days = Total_OverTime.ToString()+"."+mn1.ToString();
					OT_Compensation=System.Convert.ToDouble(str4);
					// Calculate Net Salary.
					Net_Salary=(Total_Present * Monthly_Salary / Days_in_Months);
					Net_Salary=Net_Salary + OT_Compensation * Total_OverTime;
					float min = 0.0f;
					if(mn1 != 0)
					{
						if(mn1 == 15)
							min = 0.25f;
						if(mn1 == 30)
							min = 0.50f;
						if(mn1 == 45)
							min = 0.75f;
					}
					Net_Salary=Net_Salary + (OT_Compensation * min);
					Net_Salary=System.Math.Round(Net_Salary,2);
					grandTotal = grandTotal + Net_Salary;
					Net_Salary=System.Convert.ToString(Net_Salary);
					SqlDtrOT.Close();
					//sw.WriteLine(info,str1,str2,GenUtil.strNumericFormat(str3),str4,diff.ToString(),leave.ToString() ,str5,GenUtil.strNumericFormat(str6),GenUtil.strNumericFormat(str7));
				}
				sw.Close();
				SqlDtr.Close();		
			}
			catch(Exception ex)
			{		
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:GetData "+ ex.Message+" EXCEPTION "+uid);
			}*/
			
		}
		/// <summary>
		/// This is used to print ,contact the printserver.
		/// </summary>
		public void Print()
		{
			byte[] bytes = new byte[1024];

			try 
			{
				IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress,62000);

				// Create a TCP/IP  socket.
				Socket sender1 = new Socket(AddressFamily.InterNetwork, 
					SocketType.Stream, ProtocolType.Tcp );
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:print  "+uid);
				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);

					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SalaryReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:Print  "+ ane.Message+" EXCEPTION "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:Print  "+ se.Message+" EXCEPTION "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:Print  "+ es.Message+" EXCEPTION "+uid);
				}
			}
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:Salary_statement.aspx,Method:print "+  ex.Message+" EXCEPTION  "+uid);
			}
		}

		protected void dropyear_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
                if (DropMonth.SelectedIndex != 0)
                {

                    Convertinto_Excel();
                    MessageBox.Show(" Successfully Convert File Into Excel Format ");
                    CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Convert Into Excel Format, userid  " + uid);
                }
			}
			catch(Exception ex)
			{
				MessageBox.Show(" First Close The Open Excel File ");
				CreateLogFiles.ErrorLog("Form : Salary_Statement.aspx, Class : PetrolPumpClass.cs, Method : btnExcel_Click   SaleBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}