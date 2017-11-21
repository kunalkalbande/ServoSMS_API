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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        string BaseUri = "http://localhost:64862";
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
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
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
				
                string str1,str2,str3="";               
                str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/BindTheData?str1="+str1+ "&str2="+str2).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        str3 = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

    //            sql ="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex) +"/"+dropyear.SelectedIndex+"' ";	
				//SqlDtr =obj.GetRecordSet(sql);
                
				if(str3 != null)
				{					
						if(str3.Equals("NULLS") || !str3.Trim().Equals("") )
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
				//SqlDtr.Close();

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
                DataSet DA = new DataSet();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/GetData").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        DA = JsonConvert.DeserializeObject<DataSet>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }

                //sql ="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
				SqlDataAdapter da=new SqlDataAdapter();
                
				//DataSet ds=new DataSet();	
				//da.Fill(ds,"stkmv");
				DataTable dtcustomer=DA.Tables["stkmv"];
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
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
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
                    /*4*/
                    int leaveno = 0;
                    string str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;                        
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/FindLeaves?str1=" + str1+ "&from_date="+ from_date + "&emp_id="+ emp_id).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            leaveno = JsonConvert.DeserializeObject<int>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
     //               sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ emp_id +"' and isSanction = 1";
					//SqlDtr =obj.GetRecordSet(sql);
					//if(SqlDtr.HasRows )
					//{
					//	if(SqlDtr.Read())
					//	{
					//		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
					//			leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
					//	}
					//}
					//SqlDtr.Close();
                    leave = leaveno;
                    //sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"' and cast(floor(cast(date_to as float)) as datetime) > '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(DateTime.Now.Year,DropMonth.SelectedIndex)+"/"+DateTime.Now.Year +"'and emp_id = '"+ emp_id +"'";
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/FindDays?from_date=" + from_date + "&emp_id=" + emp_id).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            leaveno = JsonConvert.DeserializeObject<int>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    leave += leaveno;
                    //               sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ emp_id +"' and isSanction = 1 and datepart(month,date_from) = datepart(month,'"+from_date +"')";
                    ////if(emp_id.Equals("1002")) 
                    ////Response.Write(sql+"<br><br>");
                    //SqlDtr =obj.GetRecordSet(sql);
                    //if(SqlDtr.HasRows  )
                    //{
                    //	if(SqlDtr.Read())
                    //	{
                    //		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                    //			leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
                    //	}
                    //}
                    //SqlDtr.Close();
                    //date_to
                    /*5*/
                    str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/SelectCase?from_date=" + from_date + "&emp_id=" + emp_id + "&str1="+ str1).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            leaveno = JsonConvert.DeserializeObject<int>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    leave += leaveno;
     //               sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '"+from_date +"' then sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,'"+from_date +"'))) else sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ emp_id +"' and isSanction = 1 group by date_to";
					////if(emp_id.Equals("1002")) 
					////	Response.Write(sql+"<br><br>");
					//SqlDtr =obj.GetRecordSet(sql);
					//if(SqlDtr.HasRows )
					//{
					//	if(SqlDtr.Read())
					//	{
					//		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
					//			leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
					//	}
					//}
					//SqlDtr.Close();

					/*Coment by vikas 18.11.2012 
					 * if(!e.Item.Cells[5].Text.ToString().Equals("Leave"))    
						e.Item.Cells[5].Text = leave.ToString();  */
					if(!e.Item.Cells[3].Text.ToString().Equals("Leave"))    
						e.Item.Cells[3].Text = leave.ToString(); 

					string Present="0";
                    #region Bind Total Present Regarding Each Item				
                    /*6*/
                    string str2, str3,sum="";
                    str2 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                    str3 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/SelectSum?str2=" + str2 + "&str3=" + str3 + "&emp_id=" + emp_id).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            sum = JsonConvert.DeserializeObject<string>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }

                    //sql ="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ emp_id +"'";
					//SqlDtr =obj.GetRecordSet(sql);
					if(sum!= null )
					{
						
							if(!sum.Equals("NULL") ||!sum.Equals("")|| sum != null)
							{
								//coment by vikas 18.11.2012 e.Item.Cells[6].Text=SqlDtr.GetValue(0).ToString() ;
								if(!e.Item.Cells[2].Text.ToString().Equals("Working Days")) 
									e.Item.Cells[2].Text= sum;
								Present= sum;
							}	
						
					}
					totpresenttot += System.Convert.ToDouble(e.Item.Cells[6].Text.ToString()) ;
					totpresenttot1=totpresenttot;
					//SqlDtr.Close();
					#endregion

					/*******Add by vikas 18.11.2012*******************/
					e.Item.Cells[5].Text=Convert.ToString((double.Parse(Present.ToString())-leave));

                    /*******End*******************/

                    #region Bind Total OverTime Hours Regarding Each Item
                    /*7*/
                    SalaryStatementModel salary = new SalaryStatementModel();
                    str2 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                    str3 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/SelectSumAndTime?str2=" + str2 + "&str3=" + str3 + "&emp_id=" + emp_id).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            salary = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    //string	Sql1 ="select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ emp_id+"'";
					//Response.Write(Sql1+"<br>"); 
					float hr=0;
					float mn1=0;
					//SqlDtr = obj.GetRecordSet(Sql1);
					if(salary.hour != null)
					{
						
								string	strh11= salary.hour;
								if(strh11==null||strh11.Equals(""))
								{
									strh11="0";
								}
								string	strm11= salary.min;
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
					//SqlDtr.Close();
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
				string Ledger_ID="",str11="",str12="",str="",Ledger="";
				SqlDtr = null;
                string evalue = e.Item.Cells[1].Text.ToString();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectLedgerId?evalue=" + evalue).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        Ledger = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                Ledger_ID = Ledger;
                //Ledger_ID ="Select Ledger_ID from Ledger_Master where Ledger_Name ='"+e.Item.Cells[1].Text.ToString()+"'";
    //            dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='"+e.Item.Cells[1].Text.ToString()+"'",ref SqlDtr);
				//if(SqlDtr.Read())
				//{
				//	Ledger_ID = SqlDtr["Ledger_ID"].ToString(); 
				//}
				//SqlDtr.Close();
				if(Ledger_ID!="")
				{
                    string str1, str2,SumAdvance="";
                    str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                    str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/SelectSumAdvance?str1=" + str1+"&str2="+str2+ "&Ledger_ID="+ Ledger_ID).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            SumAdvance = JsonConvert.DeserializeObject<string>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    //sql ="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Payment%') and Ledger_ID="+Ledger_ID;
					//SqlDtr =obj.GetRecordSet(sql);
					if(SumAdvance != "" || SumAdvance != null)
					{						
							if(!SumAdvance.Equals("NULL") ||!SumAdvance.Equals("")|| SumAdvance != null)
							{
								str11= SumAdvance;
							}						
					}
					else
					{
						str11="0";
					}
                    //SqlDtr.Close();
                    string str3, str4, Balance = "";
                    str3 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                    str4 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/SelectBalance?str3=" + str3 + "&str4=" + str4 + "&Ledger_ID=" + Ledger_ID).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            Balance = JsonConvert.DeserializeObject<string>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    //sql ="select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID="+Ledger_ID+" and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'";
					//SqlDtr =obj.GetRecordSet(sql);
					if(Balance!="")
					{
						
							if(!Balance.Equals("NULL") ||!Balance.Equals("")|| Balance != null)
							{
								str12= Balance;
							}
						
					}
					else
					{
						str12="0";
					}
					//SqlDtr.Close();
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
				double TotalReceipt=0, receipt = 0;
                string str5, str6 ;
				InventoryClass obj1 = new InventoryClass();
                str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectCreditAmtOfCustomer?str5=" + str5 + "&str6=" + str6 + "&emp_id=" + emp_id).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        receipt = JsonConvert.DeserializeObject<double>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (receipt != 0)
                    TotalReceipt = receipt;
                else
                    TotalReceipt = 0;
                
    //            SqlDataReader rdr = obj1.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+emp_id+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				//if(rdr.Read())
				//{
				//	if(rdr.GetValue(0).ToString()!="")
				//		TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
				//	else
				//		TotalReceipt=0;
				//}
				//rdr.Close();

				/***************************************************************/
				double bounce=0;
                string  Bounce="";
                str5=DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;                     
                str6 =DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectBounce?str5=" + str5 + "&str6=" + str6 + "&emp_id=" + emp_id).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        Bounce = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (Bounce != "")
                {
                    bounce = double.Parse(Bounce);
                }
                else
                {
                    bounce = 0;
                }

                //            rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+emp_id+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
                //if(rdr.Read())
                //{
                //	if(rdr.GetValue(0).ToString()!="")
                //		bounce = double.Parse(rdr.GetValue(0).ToString());
                //	else
                //		bounce=0;
                //}
                //rdr.Close();
                TotalReceipt -=bounce;
				//TotalAmount[4]+=bounce;
				//return GenUtil.strNumericFormat(bounce.ToString());

				double cd =0;
                string creditAmt = "";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectCreditAmt?str5=" + str5 + "&str6=" + str6 + "&emp_id=" + emp_id).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        creditAmt = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (creditAmt != "")
                {
                    cd = double.Parse(creditAmt);
                }                
    //            rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+emp_id+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				//if(rdr.Read())
				//{
				//	if(rdr.GetValue(0).ToString()!="")
				//		cd=double.Parse(rdr.GetValue(0).ToString());
				//}
				//rdr.Close();
				//TotalReceipt-=cd;
				//TotalAmount[1]+=cd;
				//return GenUtil.strNumericFormat(cd.ToString());

				double cn=0;
                string SumOfcreditAmt = "";
                str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectSumOfCreditAmt?str5=" + str5 + "&str6=" + str6 + "&emp_id=" + emp_id).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        SumOfcreditAmt = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (SumOfcreditAmt != "")
                {
                    cn = double.Parse(SumOfcreditAmt);
                }
    //            rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+emp_id+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				//if(rdr.Read())
				//{
				//	if(rdr.GetValue(0).ToString()!="")
				//		cn=double.Parse(rdr.GetValue(0).ToString());
				//}
				//rdr.Close();
				TotalReceipt-=cn;
				//TotalAmount[3]+=cn;

				double sd =0;
                string SumOfCreAmt="";
                //SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and particulars like 'Receipt_sd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectSumOfCreAmt?str5=" + str5 + "&str6=" + str6 + "&emp_id=" + emp_id).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        SumOfCreAmt = JsonConvert.DeserializeObject<string>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                if (SumOfCreAmt != "")
                {
                    sd = double.Parse(SumOfCreAmt);
                }
    //            rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+emp_id+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
				//if(rdr.Read())
				//{
				//	if(rdr.GetValue(0).ToString()!="")
				//		sd=double.Parse(rdr.GetValue(0).ToString());
				//}
				//rdr.Close();
				TotalReceipt-=sd;
				//TotalAmount[2]+=sd;
				//GenUtil.strNumericFormat(sd.ToString());

				/*********************/

				SqlDataReader dtr=null;
				double ssrinc=0;
                SalaryStatementModel salary1 = new SalaryStatementModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/SelectSSRincentive").Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        salary1 = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                //sql ="select * from setDis";
				//InventoryClass obj2 = new InventoryClass();
				//dtr=obj1.GetRecordSet(sql);
				//while(dtr.Read())
				//{
					if(salary1.SSRincentiveStatus!="0")
						ssrinc=double.Parse(salary1.SSRincentive);
					else
						ssrinc=0;
				//}
				//dtr.Close();

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
                    string str1, str2,sum="";
                    str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                    str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BaseUri);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var Res = client.GetAsync("api/SalaryStatement/SelectSumAdv?str1=" + str1 + "&str2=" + str2 + "&Ledger_ID=" + Ledger_ID).Result;
                        if (Res.IsSuccessStatusCode)
                        {
                            var id = Res.Content.ReadAsStringAsync().Result;
                            sum = JsonConvert.DeserializeObject<string>(id);
                        }
                        else
                            Res.EnsureSuccessStatusCode();
                    }
                    //sql ="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Journal%') and Ledger_ID="+Ledger_ID;
					//SqlDtr =obj.GetRecordSet(sql);
					if(sum!="")
					{						
							//if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							if(!sum.Equals("NULL") && !sum.Equals("") && sum != null)
							{
								str11= sum;
							}
							else
							{
								str11="0";
							}
						
						
					}
					else
					{
						str11="0";
					}
					//SqlDtr.Close();
				
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

                SalaryStatementModel salary = new SalaryStatementModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/GetDataFomEmployee?strorderby=" + strorderby).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        salary = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                //sql ="select emp_id,emp_name, salary, ot_compensation from employee where status='1'";
                //sql+=" order by "+strorderby;

                //SqlDtr1 = obj2.GetRecordSet(sql);
                List<string> controlempid = new List<string>();
                List<string> controlempname = new List<string>();
                List<string> controlbasicsalary = new List<string>();
                List<string> controlextradays = new List<string>();
                foreach (var id in salary.empid)
                {
                    controlempid = salary.empid;
                }
                foreach (var id in salary.empname)
                {
                    controlempname = salary.empname;
                }
                foreach (var id in salary.basicsalary)
                {
                    controlbasicsalary = salary.basicsalary;
                }
                foreach (var id in salary.extradays)
                {
                    controlextradays = salary.extradays;
                }                
                double hr1=0;
                int count = salary.empid.Count;

                if (salary.empid.Count != 0)
				{
					//sw.WriteLine("Emp ID\tEmployee Name\tWorking Days\tLeave\tXtra Days\tTotal Days - Leave+ Xtra Days Working\t Monthly Salary\tNet Salary\tAdvance	Incentive\tTotal Salery-Advance + Incentive\tTA/DA\tNet Expenditure to Employees");
					Write2File1(sw,"+----+-------------------------+--------+-------+-----+----------+-------+------+-------+---------+----------+---------+-----------+");				
					Write2File1(sw,"|(1) |         (2)             |   (3)  |  (4)  | (5) |    (6)   |  (7)  |  (8) |  (9)  |   (10)  |   (11)   |  (12)   |   (13)    |");				
					Write2File1(sw,"|Emp.|     Employee Name       | Working| Leave |Extra|Total Days| Basic |  Net |Advance|Incentive|Tot.Salary|  TA-DA  |  Tot Exp. |");
					Write2File1(sw,"|ID  |                         |  days  |       |Days | (4)+(5)  |Salary |Salary|       |         |  (9)+(10)|         | Employees |");
					Write2File1(sw,"+----+-------------------------+--------+-------+-----+----------+------+-------+-------+---------+----------+---------+-----------+"); 
								  // 0123 0123456789012345678901234 01234567 0123456 01234 0123456789 012345 0123456 0123456 012345678 0123456789 012345678 01234567890
					string info = " {0,-4:S} {1,-25:S} {2,-8:S} {3,-7:S} {4,-5:S} {5,-10:S} {6,6:S} {7,7:S} {8,7:S} {9,9:S} {10,10:S} {11,9:S} {12,10:S} ";
                    for (int p = 0; p <= count - 1; p++)
                    {
                        Emp_id = controlempid[p].ToString();
                        Emp_Name = controlempname[p].ToString();
                        Basic_Salary = controlbasicsalary[p].ToString();
                        Extra_Days = controlextradays[p].ToString();

                        int leave = 0;

                        int leaveno = 0;
                        string str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintFindLeaves?str1=" + str1 + "&from_date=" + from_date + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                leaveno = JsonConvert.DeserializeObject<int>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        leave = leaveno;
                        //                  sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ Emp_id +"' and isSanction = 1";
                        //SqlDtr =obj.GetRecordSet(sql);
                        //if(SqlDtr.HasRows )
                        //{
                        //	if(SqlDtr.Read())
                        //	{
                        //		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                        //			leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
                        //	}
                        //}
                        //SqlDtr.Close();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintFindDays?from_date=" + from_date + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                leaveno = JsonConvert.DeserializeObject<int>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        leave += leaveno;
                        //                  sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ Emp_id +"' and isSanction = 1 and datepart(month,date_from) = datepart(month,'"+from_date +"')";
                        //SqlDtr =obj.GetRecordSet(sql);
                        //if(SqlDtr.HasRows  )
                        //{
                        //	if(SqlDtr.Read())
                        //	{
                        //		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                        //			leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
                        //	}
                        //}
                        //SqlDtr.Close();
                        str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectCase?from_date=" + from_date + "&Emp_id=" + Emp_id + "&str1=" + str1).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                leaveno = JsonConvert.DeserializeObject<int>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        leave += leaveno;
      //                  sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '"+from_date +"' then sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,'"+from_date +"'))) else sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ Emp_id +"' and isSanction = 1 group by date_to";
						//SqlDtr =obj.GetRecordSet(sql);
						//if(SqlDtr.HasRows )
						//{
						//	if(SqlDtr.Read())
						//	{
						//		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						//			leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
						//	}
						//}
						//SqlDtr.Close();

						string Present="0";
                        #region Bind Total Present Regarding Each Item
                        string str2,sum="";
                        str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSum?str1=" + str1 + "&str2=" + str2 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                sum = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        //sql ="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id +"'";
						//SqlDtr =obj.GetRecordSet(sql);
						if(sum != "" )
						{
                            if (!sum.Equals("NULL") ||!sum.Equals("")|| sum != null)
							{
								//e.Item.Cells[2].Text=SqlDtr.GetValue(0).ToString() ;
								Present= sum;
							}	
							
						}
						totpresenttot += System.Convert.ToDouble(Present.ToString()) ;
						totpresenttot1=totpresenttot;
						//SqlDtr.Close();
						#endregion

						Working_Day=Convert.ToString((double.Parse(Present.ToString())-leave));

                        #region Bind Total OverTime Hours Regarding Each Item
                        //SalaryStatementModel salary = new SalaryStatementModel();
                        string str3;
                        str2 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        str3 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumAndTime?str2=" + str2 + "&str3=" + str3 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                salary = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        //string	Sql1 ="select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id+"'";
						float hr=0;
						float mn1=0;
						//SqlDtr = obj.GetRecordSet(Sql1);
						if(salary.hour != null)
						{
							
								if (salary.hour != "")
								{
									string	strh11=salary.hour;
									if(strh11==null||strh11.Equals(""))
									{
										strh11="0";
									}
									string	strm11=salary.min;
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
						//SqlDtr.Close();
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
                        string Ledger_ID = "", str11 = "", str12 = "", str = "",ledgerid="" ;
                        Emp_Name = Emp_Name.ToString();

                        SqlDtr = null;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/FindLedgerId?Emp_Name=" + Emp_Name).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                ledgerid = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        Ledger_ID = ledgerid;
      //                  dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='"+Emp_Name.ToString()+"'",ref SqlDtr);
						//if(SqlDtr.Read())
						//{
						//	Ledger_ID = SqlDtr["Ledger_ID"].ToString(); 
						//}
						//SqlDtr.Close();
						if(Ledger_ID!="")
						{
                            string SumAdvance = "";
                            str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                            str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(BaseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumAdvance?str1=" + str1 + "&str2=" + str2 + "&Ledger_ID=" + Ledger_ID).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    SumAdvance = JsonConvert.DeserializeObject<string>(id);
                                }
                                else
                                    Res.EnsureSuccessStatusCode();
                            }
                            if (SumAdvance != "" || SumAdvance != null)
                            {
                                if (!SumAdvance.Equals("NULL") || !SumAdvance.Equals("") || SumAdvance != null)
                                {
                                    str11 = SumAdvance;
                                }
                            }
                            else
                            {
                                str11 = "0";
                            }
                            //                     sql ="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Payment%') and Ledger_ID="+Ledger_ID;
                            //SqlDtr =obj.GetRecordSet(sql);
                            //if(SqlDtr.HasRows )
                            //{
                            //	while(SqlDtr.Read())
                            //	{
                            //		if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
                            //		{
                            //			str11=SqlDtr.GetValue(0).ToString();
                            //		}
                            //	}
                            //}
                            //SqlDtr.Close();
                            string str4, Balance = "";
                            str3 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                            str4 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(BaseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var Res = client.GetAsync("api/SalaryStatement/PrintSelectBalance?str3=" + str3 + "&str4=" + str4 + "&Ledger_ID=" + Ledger_ID).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    Balance = JsonConvert.DeserializeObject<string>(id);
                                }
                                else
                                    Res.EnsureSuccessStatusCode();
                            }
                            if (Balance != "")
                            {

                                if (!Balance.Equals("NULL") || !Balance.Equals("") || Balance != null)
                                {
                                    str12 = Balance;
                                }

                            }
                            else
                            {
                                str12 = "0";
                            }
       //                     sql ="select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID="+Ledger_ID+" and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'";
							//SqlDtr =obj.GetRecordSet(sql);
							//if(SqlDtr.HasRows )
							//{
							//	while(SqlDtr.Read())
							//	{
							//		if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							//		{
							//			str12=SqlDtr.GetValue(0).ToString();
							//		}
							//	}
							//}
							//SqlDtr.Close();
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
                        string str5, str6;
                        double receipt = 0;
                        str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                        str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectCreditAmtOfCustomer?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                receipt = JsonConvert.DeserializeObject<double>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (receipt != 0)
                            TotalReceipt = receipt;
                        else
                            TotalReceipt = 0;
      //                  SqlDataReader rdr = obj1.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
						//	else
						//		TotalReceipt=0;
						//}
						//rdr.Close();

                        SqlDataReader rdr;
                        /**********************/
                        double bounce=0;                        
                        string Bounce = "";
                        str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                        str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectBounce?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                Bounce = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (Bounce != "")
                        {
                            bounce = double.Parse(Bounce);
                        }
                        else
                        {
                            bounce = 0;
                        }
      //                  rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		bounce = double.Parse(rdr.GetValue(0).ToString());
						//	else
						//		bounce=0;
						//}
						//rdr.Close();
						TotalReceipt-=bounce;
						//TotalAmount[4]+=bounce;
						//return GenUtil.strNumericFormat(bounce.ToString());



						double cd =0;                        
                        string creditAmt = "";
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectCreditAmt?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                creditAmt = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (creditAmt != "")
                        {
                            cd = double.Parse(creditAmt);
                        }
      //                  rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		cd=double.Parse(rdr.GetValue(0).ToString());
						//}
						//rdr.Close();
						TotalReceipt-=cd;
						//TotalAmount[1]+=cd;
						//return GenUtil.strNumericFormat(cd.ToString());

						double cn=0;
                        string SumOfcreditAmt = "";
                        str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                        str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumOfCreditAmt?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                SumOfcreditAmt = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (SumOfcreditAmt != "")
                        {
                            cn = double.Parse(SumOfcreditAmt);
                        }
                        //rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		cn=double.Parse(rdr.GetValue(0).ToString());
						//}
						//rdr.Close();
						TotalReceipt-=cn;
						//TotalAmount[3]+=cn;

						double sd =0;
                        //SqlDataReader rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_ID+"') and particulars like 'Receipt_sd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'");
                        string SumOfCreAmt = "";                        
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumOfCreAmt?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                SumOfCreAmt = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (SumOfCreAmt != "")
                        {
                            sd = double.Parse(SumOfCreAmt);
                        }
      //                  rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		sd=double.Parse(rdr.GetValue(0).ToString());
						//}
						//rdr.Close();
						TotalReceipt-=sd;
					

						/*********************/



						SqlDataReader dtr=null;
						double ssrinc=0;
                        SalaryStatementModel salary1 = new SalaryStatementModel();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/SelectSSRincentive").Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                salary1 = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (salary1.SSRincentiveStatus != "0")
                            ssrinc = double.Parse(salary1.SSRincentive);
                        else
                            ssrinc = 0;
      //                  sql ="select * from setDis";
				
						//dtr=obj1.GetRecordSet(sql);
						//while(dtr.Read())
						//{
						//	if(dtr["SSRincentiveStatus"].ToString()!="0")
						//		ssrinc=double.Parse(dtr["SSRincentive"].ToString());
						//	else
						//		ssrinc=0;
						//}
						//dtr.Close();

						double totInc=TotalReceipt*ssrinc/100;

						Incentive=Convert.ToString(Math.Round(double.Parse(totInc.ToString())));
				
						if(str=="")
							str="0";

						double Final_Tot=Math.Round((Net_Salary+totInc)-double.Parse(str.ToString()),2);
				
						Tot_Adv_Incent=Final_Tot.ToString();

						if(Ledger_ID!="")
						{
                            sum = "";
                            str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                            str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(BaseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumAdv?str1=" + str1 + "&str2=" + str2 + "&Ledger_ID=" + Ledger_ID).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    sum = JsonConvert.DeserializeObject<string>(id);
                                }
                                else
                                    Res.EnsureSuccessStatusCode();
                            }
                            if (sum != "")
                            {
                                //if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
                                if (!sum.Equals("NULL") && !sum.Equals("") && sum != null)
                                {
                                    str11 = sum;
                                }
                                else
                                {
                                    str11 = "0";
                                }


                            }
                            else
                            {
                                str11 = "0";
                            }
       //                     sql ="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Journal%') and Ledger_ID="+Ledger_ID;
							//SqlDtr =obj.GetRecordSet(sql);
							//if(SqlDtr.HasRows )
							//{
							//	while(SqlDtr.Read())
							//	{
							//		if(!SqlDtr.GetValue(0).ToString().Equals("NULL") && !SqlDtr.GetValue(0).ToString().Equals("") && SqlDtr.GetValue(0).ToString() != null)
							//		{
							//			str11=SqlDtr.GetValue(0).ToString();
							//		}
							//		else
							//		{
							//			str11="0";
							//		}
						
							//	}
							//}
							//else
							//{
							//	str11="0";
							//}
							//SqlDtr.Close();
				
							Ta_Da=Convert.ToString(Math.Round(double.Parse(str11.ToString())));
                            
							//coment by vikas 20.12.2012 double Net_Exp=Math.Round(Final_Tot+double.Parse(str11),2);
							double Net_Exp=totInc+double.Parse(str11)+Net_Salary;       //20.12.2012

							Net_Expences=Net_Exp.ToString();
                            Emp_id = "";
                            Emp_Name = "";
                            Basic_Salary = "";
                            Extra_Days = "";
                        }
					
						//sw.WriteLine(Emp_id+"\t"+Emp_Name+"\t"+Working_Day+"\t"+leave+"\t"+Extra_Days+"\t\t"+Basic_Salary+"\t"+Net_Salary+"\t"+Advance+"\t"+Incentive+"\t"+Tot_Adv_Incent+"\t"+Ta_Da+"\t"+Net_Expences+"\t");
						sw.WriteLine(info,Emp_id.ToString(),GenUtil.TrimLength(Emp_Name.ToString(),24),Working_Day.ToString(),leave.ToString(),Extra_Days.ToString(),"",Basic_Salary.ToString(), Convert.ToString(Math.Round(Net_Salary)),Advance.ToString(),Incentive.ToString(),Convert.ToString(Math.Round(double.Parse(Tot_Adv_Incent.ToString()))),Ta_Da.ToString(),Convert.ToString(Math.Round(double.Parse(Net_Expences.ToString()))));

					}
					//SqlDtr1.Close();
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
                Response.Redirect("../../Sysitem/ErrorPage.aspx", false);
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

                SalaryStatementModel salary = new SalaryStatementModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Res = client.GetAsync("api/SalaryStatement/ExcelGetDataFomEmployee?strorderby=" + strorderby).Result;
                    if (Res.IsSuccessStatusCode)
                    {
                        var id = Res.Content.ReadAsStringAsync().Result;
                        salary = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                    }
                    else
                        Res.EnsureSuccessStatusCode();
                }
                //            sql ="select emp_id,emp_name, salary, ot_compensation from employee where status='1' ";

                //sql+=" order by "+strorderby;

                //SqlDtr1 = obj2.GetRecordSet(sql);
                List<string> controlempid = new List<string>();
                List<string> controlempname = new List<string>();
                List<string> controlbasicsalary = new List<string>();
                List<string> controlextradays = new List<string>();
                foreach (var id in salary.empid)
                {
                    controlempid = salary.empid;
                }
                foreach (var id in salary.empname)
                {
                    controlempname = salary.empname;
                }
                foreach (var id in salary.basicsalary)
                {
                    controlbasicsalary = salary.basicsalary;
                }
                foreach (var id in salary.extradays)
                {
                    controlextradays = salary.extradays;
                }
                double hr1=0;
                int count = salary.empid.Count;
                if (salary.empid.Count != 0)
				{
					sw.WriteLine("Emp ID\tEmployee Name\tWorking Days\tLeave\tXtra Days\tTotal Days - Leave+ Xtra Days Working\t Monthly Salary\tNet Salary\tAdvance	Incentive\tTotal Salery-Advance + Incentive\tTA/DA\tNet Expenditure to Employees");

                    for (int p = 0; p <= count - 1; p++)
                    {
                        Emp_id = controlempid[p].ToString();
                        Emp_Name = controlempname[p].ToString();
                        Basic_Salary = controlbasicsalary[p].ToString();
                        Extra_Days = controlextradays[p].ToString();

                        int leave = 0;
                        int leaveno = 0;
                        string str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintFindLeaves?str1=" + str1 + "&from_date=" + from_date + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                leaveno = JsonConvert.DeserializeObject<int>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        leave = leaveno;
                        //sql = "select sum(datediff(day,date_from,dateadd(day,1,date_to))) from leave_register where cast(floor(cast(Date_From  as float))as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) <= '"+from_date+"' and emp_id = '"+ Emp_id +"' and isSanction = 1";
                        //SqlDtr =obj.GetRecordSet(sql);
                        //if(SqlDtr.HasRows )
                        //{
                        //	if(SqlDtr.Read())
                        //	{
                        //		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                        //			leave = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
                        //	}
                        //}
                        //SqlDtr.Close();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintFindDays?from_date=" + from_date + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                leaveno = JsonConvert.DeserializeObject<int>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        leave += leaveno;
                        //                  sql = "select sum(datediff(day,date_from,dateadd(day,1,'"+from_date +"'))) from leave_register where cast(floor(cast(date_from as float)) as datetime) <= '"+from_date +"' and cast(floor(cast(date_to as float)) as datetime) > '"+from_date+"'and emp_id = '"+ Emp_id +"' and isSanction = 1 and datepart(month,date_from) = datepart(month,'"+from_date +"')";
                        //SqlDtr =obj.GetRecordSet(sql);
                        //if(SqlDtr.HasRows  )
                        //{
                        //	if(SqlDtr.Read())
                        //	{
                        //		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
                        //			leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
                        //	}
                        //}
                        //SqlDtr.Close();
                        str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;

                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectCase?from_date=" + from_date + "&Emp_id=" + Emp_id + "&str1=" + str1).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                leaveno = JsonConvert.DeserializeObject<int>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        leave += leaveno;
      //                  sql = "select case when cast(floor(cast(date_to as float)) as datetime) >= '"+from_date +"' then sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,'"+from_date +"'))) else sum(datediff(day,'"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"',dateadd(day,1,date_to))) end from leave_register where cast(floor(cast(date_from as float)) as datetime) < '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(date_to as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"'and emp_id = '"+ Emp_id +"' and isSanction = 1 group by date_to";
						//SqlDtr =obj.GetRecordSet(sql);
						//if(SqlDtr.HasRows )
						//{
						//	if(SqlDtr.Read())
						//	{
						//		if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						//			leave += System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()) ;
						//	}
						//}
						//SqlDtr.Close();

						string Present="0";
                        #region Bind Total Present Regarding Each Item	
                        string str2, sum = "";
                        str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSum?str1=" + str1 + "&str2=" + str2 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                sum = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        //sql ="select sum(cast(status as integer)) Total_Present from attandance_register where cast(floor(cast(cast(att_date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(cast(att_date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id +"'";
                        //SqlDtr =obj.GetRecordSet(sql);
                        if (sum != "")
                        {
                            if (!sum.Equals("NULL") || !sum.Equals("") || sum != null)
                            {
                                //e.Item.Cells[2].Text=SqlDtr.GetValue(0).ToString() ;
                                Present = sum;
                            }

                        }
                        totpresenttot += System.Convert.ToDouble(Present.ToString()) ;
						totpresenttot1=totpresenttot;
						//SqlDtr.Close();
						#endregion

						Working_Day=Convert.ToString((double.Parse(Present.ToString())-leave));

                        #region Bind Total OverTime Hours Regarding Each Item
                        string str3;
                        str2 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedIndex;
                        str3 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(dropyear.SelectedIndex, DropMonth.SelectedIndex) + "/" + dropyear.SelectedIndex;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumAndTime?str2=" + str2 + "&str3=" + str3 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                salary = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        //string	Sql1 ="select sum(datepart(hour,Ot_To)-datepart(hour,Ot_From)) OT_Hour,sum(datepart(minute,Ot_To)-datepart(minute,Ot_From)) OT_Minute from OverTime_Register where cast(floor(cast(OT_Date as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedIndex +"' and cast(floor(cast(OT_Date as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex,DropMonth.SelectedIndex)+"/"+dropyear.SelectedIndex +"' and emp_id='"+ Emp_id+"'";
						float hr=0;
						float mn1=0;
						//SqlDtr = obj.GetRecordSet(Sql1);
						if(salary.hour != null)
						{
							
								if (salary.hour != "")
								{
									string	strh11=salary.hour;
									if(strh11==null||strh11.Equals(""))
									{
										strh11="0";
									}
									string	strm11=salary.min;
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
						//SqlDtr.Close();
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
						string Ledger_ID="",str11="",str12="",str="", ledgerid = "";
						SqlDtr = null;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/FindLedgerId?Emp_Name=" + Emp_Name).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                ledgerid = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        Ledger_ID = ledgerid;
      //                  dbobj.SelectQuery("Select Ledger_ID from Ledger_Master where Ledger_Name ='"+Emp_Name.ToString()+"'",ref SqlDtr);
						//if(SqlDtr.Read())
						//{
						//	Ledger_ID = SqlDtr["Ledger_ID"].ToString(); 
						//}
						//SqlDtr.Close();
						if(Ledger_ID!="")
						{
                            string SumAdvance = "";
                            str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                            str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(BaseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumAdvance?str1=" + str1 + "&str2=" + str2 + "&Ledger_ID=" + Ledger_ID).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    SumAdvance = JsonConvert.DeserializeObject<string>(id);
                                }
                                else
                                    Res.EnsureSuccessStatusCode();
                            }
                            if (SumAdvance != "" || SumAdvance != null)
                            {
                                if (!SumAdvance.Equals("NULL") || !SumAdvance.Equals("") || SumAdvance != null)
                                {
                                    str11 = SumAdvance;
                                }
                            }
                            else
                            {
                                str11 = "0";
                            }
                            //                     sql ="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Payment%') and Ledger_ID="+Ledger_ID;
                            //SqlDtr =obj.GetRecordSet(sql);
                            //if(SqlDtr.HasRows )
                            //{
                            //	while(SqlDtr.Read())
                            //	{
                            //		if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
                            //		{
                            //			str11=SqlDtr.GetValue(0).ToString();
                            //		}
                            //	}
                            //}
                            //SqlDtr.Close();
                            string str4, Balance = "";
                            str3 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                            str4 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(BaseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var Res = client.GetAsync("api/SalaryStatement/PrintSelectBalance?str3=" + str3 + "&str4=" + str4 + "&Ledger_ID=" + Ledger_ID).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    Balance = JsonConvert.DeserializeObject<string>(id);
                                }
                                else
                                    Res.EnsureSuccessStatusCode();
                            }
                            if (Balance != "")
                            {

                                if (!Balance.Equals("NULL") || !Balance.Equals("") || Balance != null)
                                {
                                    str12 = Balance;
                                }

                            }
                            else
                            {
                                str12 = "0";
                            }
       //                     sql ="select Balance from AccountsLedgerTable where particulars like ('Opening%') and Ledger_ID="+Ledger_ID+" and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'";
							//SqlDtr =obj.GetRecordSet(sql);
							//if(SqlDtr.HasRows )
							//{
							//	while(SqlDtr.Read())
							//	{
							//		if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
							//		{
							//			str12=SqlDtr.GetValue(0).ToString();
							//		}
							//	}
							//}
							//SqlDtr.Close();
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
                        string str5, str6;
                        double receipt = 0;
                        str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                        str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectCreditAmtOfCustomer?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                receipt = JsonConvert.DeserializeObject<double>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (receipt != 0)
                            TotalReceipt = receipt;
                        else
                            TotalReceipt = 0;
                        //                  SqlDataReader rdr = obj1.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'Payment Received%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
                        //if(rdr.Read())
                        //{
                        //	if(rdr.GetValue(0).ToString()!="")
                        //		TotalReceipt = double.Parse(rdr.GetValue(0).ToString());
                        //	else
                        //		TotalReceipt=0;
                        //}
                        //rdr.Close();

                        SqlDataReader rdr;
                        /**********************/
                        double bounce=0;
                        string Bounce = "";
                        str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                        str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectBounce?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                Bounce = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (Bounce != "")
                        {
                            bounce = double.Parse(Bounce);
                        }
                        else
                        {
                            bounce = 0;
                        }
      //                  rdr = obj.GetRecordSet("select sum(DebitAmount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like'voucher(5%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		bounce = double.Parse(rdr.GetValue(0).ToString());
						//	else
						//		bounce=0;
						//}
						//rdr.Close();
						TotalReceipt-=bounce;

						double cd =0;                        
                        string creditAmt = "";
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectCreditAmt?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                creditAmt = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (creditAmt != "")
                        {
                            cd = double.Parse(creditAmt);
                        }
      //                  rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and particulars like 'Receipt_cd%' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		cd=double.Parse(rdr.GetValue(0).ToString());
						//}
						//rdr.Close();
						TotalReceipt-=cd;

						double cn=0;
                        string SumOfcreditAmt = "";
                        str5 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                        str6 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumOfCreditAmt?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                SumOfcreditAmt = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (SumOfcreditAmt != "")
                        {
                            cn = double.Parse(SumOfcreditAmt);
                        }
      //                  rdr = obj.GetRecordSet("select sum(creditamount) from customerledgertable where custid in(select cust_id from customer where ssr='"+Emp_id+"') and particular like 'voucher(3%' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entrydate as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		cn=double.Parse(rdr.GetValue(0).ToString());
						//}
						//rdr.Close();
						TotalReceipt-=cn;

						double sd =0;
                        string SumOfCreAmt = "";
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumOfCreAmt?str5=" + str5 + "&str6=" + str6 + "&Emp_id=" + Emp_id).Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                SumOfCreAmt = JsonConvert.DeserializeObject<string>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (SumOfCreAmt != "")
                        {
                            sd = double.Parse(SumOfCreAmt);
                        }
      //                  rdr = obj.GetRecordSet("select sum(credit_amount) from Accountsledgertable where Ledger_id in(select Ledger_id from ledger_master,customer where ledger_name=cust_name and ssr='"+Emp_id+"') and (particulars like 'Receipt_sd%' or particulars like 'Receipt_fd%' or particulars like 'Receipt_dd%') and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)>='"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(entry_date as datetime) as float)) as datetime)<='"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"'");
						//if(rdr.Read())
						//{
						//	if(rdr.GetValue(0).ToString()!="")
						//		sd=double.Parse(rdr.GetValue(0).ToString());
						//}
						//rdr.Close();
						TotalReceipt-=sd;
						/*********************/


						SqlDataReader dtr=null;
						double ssrinc=0;
                        SalaryStatementModel salary1 = new SalaryStatementModel();
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(BaseUri);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var Res = client.GetAsync("api/SalaryStatement/SelectSSRincentive").Result;
                            if (Res.IsSuccessStatusCode)
                            {
                                var id = Res.Content.ReadAsStringAsync().Result;
                                salary1 = JsonConvert.DeserializeObject<SalaryStatementModel>(id);
                            }
                            else
                                Res.EnsureSuccessStatusCode();
                        }
                        if (salary1.SSRincentiveStatus != "0")
                            ssrinc = double.Parse(salary1.SSRincentive);
                        else
                            ssrinc = 0;
                        //sql ="select * from setDis";
				
						//dtr=obj1.GetRecordSet(sql);
						//while(dtr.Read())
						//{
						//	if(dtr["SSRincentiveStatus"].ToString()!="0")
						//		ssrinc=double.Parse(dtr["SSRincentive"].ToString());
						//	else
						//		ssrinc=0;
						//}
						//dtr.Close();

						double totInc=TotalReceipt*ssrinc/100;

						Incentive=totInc.ToString();
				
						if(str=="")
							str="0";

						double Final_Tot=Math.Round((Net_Salary+totInc)-double.Parse(str.ToString()),2);
				
						Tot_Adv_Incent=Final_Tot.ToString();

						if(Ledger_ID!="")
						{
                            sum = "";
                            str1 = DropMonth.SelectedIndex + "/1/" + dropyear.SelectedItem.Text;
                            str2 = DropMonth.SelectedIndex + "/" + DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text), DropMonth.SelectedIndex) + "/" + dropyear.SelectedItem.Text;
                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(BaseUri);
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                var Res = client.GetAsync("api/SalaryStatement/PrintSelectSumAdv?str1=" + str1 + "&str2=" + str2 + "&Ledger_ID=" + Ledger_ID).Result;
                                if (Res.IsSuccessStatusCode)
                                {
                                    var id = Res.Content.ReadAsStringAsync().Result;
                                    sum = JsonConvert.DeserializeObject<string>(id);
                                }
                                else
                                    Res.EnsureSuccessStatusCode();
                            }
                            if (sum != "")
                            {
                                //if(!SqlDtr.GetValue(0).ToString().Equals("NULL") ||!SqlDtr.GetValue(0).ToString().Equals("")|| SqlDtr.GetValue(0).ToString() != null)
                                if (!sum.Equals("NULL") && !sum.Equals("") && sum != null)
                                {
                                    str11 = sum;
                                }
                                else
                                {
                                    str11 = "0";
                                }


                            }
                            else
                            {
                                str11 = "0";
                            }
       //                     sql ="select sum(cast(Debit_Amount as float)) advance from AccountsLedgerTable where cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) >= '"+DropMonth.SelectedIndex+"/1/"+dropyear.SelectedItem.Text+"' and cast(floor(cast(cast(Entry_Date as datetime) as float)) as datetime) <= '"+DropMonth.SelectedIndex+"/"+DateTime.DaysInMonth(int.Parse(dropyear.SelectedItem.Text),DropMonth.SelectedIndex)+"/"+dropyear.SelectedItem.Text+"' and particulars like ('Journal%') and Ledger_ID="+Ledger_ID;
							//SqlDtr =obj.GetRecordSet(sql);
							//if(SqlDtr.HasRows )
							//{
							//	while(SqlDtr.Read())
							//	{
							//		if(!SqlDtr.GetValue(0).ToString().Equals("NULL") && !SqlDtr.GetValue(0).ToString().Equals("") && SqlDtr.GetValue(0).ToString() != null)
							//		{
							//			str11=SqlDtr.GetValue(0).ToString();
							//		}
							//		else
							//		{
							//			str11="0";
							//		}
						
							//	}
							//}
							//else
							//{
							//	str11="0";
							//}
							//SqlDtr.Close();
				
							Ta_Da=str11.ToString();

							//coment by vikas 20.12.2012 double Net_Exp=Final_Tot+double.Parse(str11);
							double Net_Exp=totInc+double.Parse(str11)+Net_Salary;       //20.12.2012

							Net_Expences=Net_Exp.ToString();
						}
					
						//coment by vikas 19.12.2012 sw.WriteLine(Emp_id+"\t"+Emp_Name+"\t"+Working_Day+"\t"+leave+"\t"+Extra_Days+"\t\t"+Basic_Salary+"\t"+Net_Salary+"\t"+Advance+"\t"+Incentive+"\t"+Tot_Adv_Incent+"\t"+Ta_Da+"\t"+Net_Expences+"\t");
						
						sw.WriteLine(Emp_id+"\t"+Emp_Name+"\t"+Working_Day+"\t"+leave+"\t"+Extra_Days+"\t\t"+Basic_Salary+"\t"+Convert.ToString(Math.Round(Net_Salary))+"\t"+Advance+"\t"+Convert.ToString(Math.Round(double.Parse(Incentive.ToString())))+"\t"+Convert.ToString(Math.Round(double.Parse(Tot_Adv_Incent.ToString())))+"\t"+Ta_Da+"\t"+Convert.ToString(Math.Round(double.Parse(Net_Expences.ToString())))+"\t");
						//sw.WriteLine(info,Emp_id.ToString(),GenUtil.TrimLength(Emp_Name.ToString(),24),Working_Day.ToString(),leave.ToString(),Extra_Days.ToString(),"",Basic_Salary.ToString(), Convert.ToString(Math.Round(Net_Salary)),Advance.ToString(),Incentive.ToString(),Convert.ToString(Math.Round(double.Parse(Tot_Adv_Incent.ToString()))),Ta_Da.ToString(),Convert.ToString(Math.Round(double.Parse(Net_Expences.ToString()))));
					}
					//SqlDtr1.Close();
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