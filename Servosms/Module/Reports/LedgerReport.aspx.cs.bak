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
using Servosms.Sysitem.Classes;
using RMG; 
using System.Data .SqlClient ;
using System.Net; 
using System.Net.Sockets ;
using System.IO ;
using System.Text;
using DBOperations;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for CustomerLedger.
	/// </summary>
	public partial class LedgerReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
		public string op_Balance="0";
		public double debit_total = 0;
		public double credit_total = 0;
		public string balance = "";
		public string baltype = "";
		static string FromDate="",ToDate="";
		System.Globalization.NumberFormatInfo  nfi = new System.Globalization.CultureInfo("en-US",false).NumberFormat;
		
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values with 
		/// the help of GetParties() function and organization table
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["txtDateTo"].ToString();
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["txtDateFrom"].ToString();
            // Put user code to initialize the page here
            try
			{
				uid=(Session["User_Name"].ToString());

				if(! IsPostBack)
				{
					//txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					//txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="16";
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
						Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
						return;
					}
					#endregion   
					CustomerGrid.Visible=false;
					getParties(); 
					#region Get FromDate and ToDate From Organisation
					InventoryClass obj1=new InventoryClass();
					SqlDataReader rdr1=null;
					rdr1=obj1.GetRecordSet("select * from organisation");
					if(rdr1.Read())
					{
						FromDate=GetYear(GenUtil.trimDate(rdr1["Acc_date_from"].ToString()));
						ToDate=GetYear(GenUtil.trimDate(rdr1["Acc_date_To"].ToString()));
					}
					else
					{
						MessageBox.Show("Please Fill The Organisation Details First");
						return;
					}
					#endregion
				}
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:pageload "+ " EXCEPTION  "+ex.Message+"  "+ uid );
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
		}
 
		/// <summary>
		/// This is used to fetch the parties(customer) and city from customer table and fills the Party name combo.
		/// </summary>
		public void getParties()
		{
			SqlDataReader SqlDtr = null;

			ArrayList cust_name=new ArrayList(); //add by vikas 01.06.09 
			//DropPartyName.Items.Clear();
			//DropPartyName.Items.Add("Select");
			//dbobj.SelectQuery("Select Ledger_Name+':' from Ledger_master order by Ledger_Name" ,ref SqlDtr);
			//dbobj.SelectQuery("(Select Ledger_Name+':'+City from Ledger_master lm,Customer c where c.cust_name=lm.Ledger_Name order by Ledger_Name)" ,ref SqlDtr);
			dbobj.SelectQuery("(Select Ledger_Name+':'+City from Ledger_master lm,Customer c where c.cust_name=lm.Ledger_Name union Select Ledger_Name+':' from Ledger_master where ledger_name not in(select cust_name from customer))",ref SqlDtr);
			if(SqlDtr.HasRows)
			{
				texthiddenprod.Value="Select,";
				while(SqlDtr.Read())
				{
					//DropPartyName.Items.Add(SqlDtr.GetValue(0).ToString()); 
					//coment by vikas 01.06.09 texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+",";
					cust_name.Add(SqlDtr.GetValue(0).ToString());
				}
			}
			SqlDtr.Close();

			/* *********Start add by vikas date on 01.06.09**********************/
			cust_name.Sort();
			for(int i=0;i<cust_name.Count;i++)
			{
				texthiddenprod.Value+=cust_name[i].ToString()+",";
			}

			/* *********End******************************************************/
			

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
		/// This checks the validity of filled and selected values before firing a query. 
		/// </summary>
		/// <returns></returns>
		public bool checkValidity()
		{
			string ErrorMessage = "";
			bool flag = true;
			
			if(txtDateFrom.Text.Trim().Equals(""))
			{
				ErrorMessage = ErrorMessage + " - Please Select From Date\n";
				flag = false;
			}
			if(txtDateTo.Text.Trim().Equals(""))
			{
				ErrorMessage = ErrorMessage + " - Please Select To Date\n";
				flag = false;
			}
			if(DropPartyName.Value  == "Select")
			{
				ErrorMessage = ErrorMessage + " - Please Select Party Name\n";
				flag = false;
			}
			
			if(flag == false)
			{
				MessageBox.Show(ErrorMessage);
				return false;
			}
			

			if(System.DateTime.Compare(ToMMddYYYY(txtDateFrom.Text.Trim()),ToMMddYYYY(txtDateTo.Text.Trim())) > 0)
			{
				MessageBox.Show("Date From Should be less than Date To");
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// This method is used to return the data in MM/dd/YYYY format 
		/// </summary>
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
		/// This method is used to calculates the total debit amount by passing value 
		/// </summary>
		protected void TotalDebit(double _debittotal)
		{
			debit_total  += _debittotal; 
		}
 
		/// <summary>
		/// This method is used to calculates total credit amount by passing value 
		/// </summary>
		/// <param name="_credittotal"></param>
		protected void TotalCredit(double _credittotal)
		{
			credit_total  += _credittotal; 
		}
 
		/// <summary>
		/// Its set the last Balance value and called from .aspx page from closing balance template coulumn 
		/// </summary>
		//double bal=0;//,Flag=0;
		//string Type="";
		
		//protected string setBal(string DDr,string CCr, string str, string balance,string bal_type)
		protected string setBal(string _balance)	
		{
			balance  = _balance;
			if(_balance!="0.00")
			{
				//balance  = _balance;
				return _balance;
			}
			else if(balance=="")
				return _balance;
			else
				return "0.00";
			/*************************************************
			//			if(amt!="11112222")
			//			{
			//				balance  = _balance;
			//				return _balance;
			//			}
			//			else
			//				return "";
						double Cr=0,Dr=0;
						if(DDr!="11,112,222.00" && DDr!="11112222.00")
						{
							if(str.StartsWith("Receipt"))
							{
								string trans_no = "";
								double DiscTot=0;
								trans_no = str.Substring(str.IndexOf("(")+1);
								trans_no = trans_no.Substring(0,trans_no.Length-1);
								SqlDataReader rdr = null;
								dbobj.SelectQuery("select * from payment_receipt where receipt_no='"+trans_no+"'",ref rdr);
								if(rdr.Read())
								{
									DiscTot+=double.Parse(rdr["discount1"].ToString())+double.Parse(rdr["discount2"].ToString());
									Dr = double.Parse(DDr);
									Cr = double.Parse(CCr)-DiscTot;
								}
								else
								{
									Dr = double.Parse(DDr);
									Cr = double.Parse(CCr);
								}
								rdr.Close();
							}
							else
							{
								Dr = double.Parse(DDr);
								Cr = double.Parse(CCr);
							}
						}
						else
						{
							Dr = double.Parse(DDr);
							Cr = double.Parse(CCr);
						}
						//if(bal==0)
						//if(str.StartsWith("Opening Balance"))
			if(Flag==0)
			{
				Type=bal_type;
				Flag=1;
				if(Dr==0 && Cr==0)
				{
					//Type="Dr";
					//bal+=Dr;
					bal+=double.Parse(balance);
					return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
				else if(Dr!=0)
				{
					//Type="Dr";
					//bal+=Dr;
					bal+=double.Parse(balance);
					return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
				else
				{
					//Type="Cr";
					//bal+=Cr;
					bal+=double.Parse(balance);;
					return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
				
			}
			else
			{
				if(DDr!="0.00" && DDr!="11,112,222.00" && DDr!="11112222.00" )
				{
					if(Type=="Dr")
						bal+=Dr;
					else
						bal-=Dr;
					if(bal==0)
						Type="Dr";
					else if(bal<0)
					{
						if(Type=="Cr")
							Type="Dr";
						else
							Type="Cr";
					}
					//					else
					//						Type="Cr";
					bal=Math.Abs(bal);
				}
				else
				{
					if(Type=="Cr")
						bal+=Cr;
					else
						bal-=Cr;
					if(bal==0)
						Type="Dr";
					else if(bal<0)
					{
						if(Type=="Cr")
							Type="Dr";
						else
							Type="Cr";
					}
					//					else
					//						Type="Dr";
					bal=Math.Abs(bal);
				}
				return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
			}
			***********************/
		}
		/**************** Comment By Mahesh on 09.07.008
		protected string setBal2(string DDr,string CCr, string str, string balance,string bal_type)
		{
			double Cr=0,Dr=0;
			if(DDr!="11,112,222.00" && DDr!="11112222.00")
			{
				if(str.StartsWith("Receipt"))
				{
					string trans_no = "";
					double DiscTot=0;
					trans_no = str.Substring(str.IndexOf("(")+1);
					trans_no = trans_no.Substring(0,trans_no.Length-1);
					SqlDataReader rdr = null;
					dbobj.SelectQuery("select * from payment_receipt where receipt_no='"+trans_no+"'",ref rdr);
					if(rdr.Read())
					{
						DiscTot+=double.Parse(rdr["discount1"].ToString())+double.Parse(rdr["discount2"].ToString());
						Dr = double.Parse(DDr);
						Cr = double.Parse(CCr)-DiscTot;
					}
					else
					{
						Dr = double.Parse(DDr);
						Cr = double.Parse(CCr);
					}
					rdr.Close();
				}
				else
				{
					Dr = double.Parse(DDr);
					Cr = double.Parse(CCr);
				}
			}
			else
			{
				Dr = double.Parse(DDr);
				Cr = double.Parse(CCr);
			}
			
			if(Flag==0)
			{
				Type=bal_type;
				Flag=1;
				if(Dr==0 && Cr==0)
				{
					bal+=double.Parse(balance);
					return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
				else if(Dr!=0)
				{
					bal+=double.Parse(balance);
					return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
				else
				{
					bal+=double.Parse(balance);;
					return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
				
			}
			else
			{
				if(DDr!="0.00" && DDr!="11,112,222.00" && DDr!="11112222.00" )
				{
					if(Type=="Dr")
						bal+=Dr;
					else
						bal-=Dr;
					if(bal==0)
						Type="Dr";
					else if(bal<0)
					{
						if(Type=="Cr")
							Type="Dr";
						else
							Type="Cr";
					}
					//					else
					//						Type="Cr";
					bal=Math.Abs(bal);
				}
				else
				{
					if(Type=="Cr")
						bal+=Cr;
					else
						bal-=Cr;
					if(bal==0)
						Type="Dr";
					else if(bal<0)
					{
						if(Type=="Cr")
							Type="Dr";
						else
							Type="Cr";
					}
					//					else
					//						Type="Dr";
					bal=Math.Abs(bal);
				}
				return GenUtil.strNumericFormat(bal.ToString())+" "+Type;
			}
		}
		******************************************************/
		/// <summary>
		/// Its set the last Balance value and called from .aspx page from closing balance template coulumn 
		/// </summary>
		double Balance1=0;
		protected string setBalance1(string _Debit,string _Credit)
		{
			if(DropPartyName.Value.Equals("Sales A/C"))
				Balance1  += double.Parse(_Debit);
			else if(DropPartyName.Value.Equals("Purchase A/C"))
				Balance1  += double.Parse(_Credit);
			return GenUtil.strNumericFormat(Balance1.ToString());
		}

		/// <summary>
		/// Its set the last Balance value and called from .aspx page from closing balance template coulumn 
		/// </summary>
		//protected string setCBal(string _Debit,string _Credit)
		public string ClosingBal="";
		protected string setCBal(string _Date,string _Year)
		{
			/*******************************
			string baltype=" Dr";
			double bal1=double.Parse(_Credit)-Double.Parse(_Debit);
			bal=bal1;
			if(bal1>=0)
				baltype=" Cr";
			else
				baltype=" Dr";
			bal1=Math.Abs(bal1);
			return bal1.ToString("N2",nfi)+baltype;
			*******************************/
			InventoryClass obj = new InventoryClass();
			string Ledger_ID = "";
			string[] Name = DropPartyName.Value.Split(new char[] {':'},DropPartyName.Value.Length);
			SqlDataReader rdr = obj.GetRecordSet("select Ledger_ID from Ledger_Master where Ledger_Name='"+Name[0]+"'");
			if(rdr.Read())
			{
				Ledger_ID = rdr.GetValue(0).ToString();
			}
			rdr.Close();
			string StartDate=getMonth(_Date)+"/1/"+_Year;
			int day=DateTime.DaysInMonth(int.Parse(_Year),getMonth(_Date));
			string EndDate=getMonth(_Date)+"/"+day+"/"+_Year;
			string str = "select top 1 * from accountsledgertable where ledger_id='"+Ledger_ID+"' and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+StartDate+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+EndDate+"' order by entry_date desc";
			rdr = obj.GetRecordSet(str);
			if(rdr.Read())
			{
				ClosingBal = GenUtil.strNumericFormat(rdr["balance"].ToString())+" "+rdr["Bal_Type"].ToString();
				return GenUtil.strNumericFormat(rdr["balance"].ToString())+" "+rdr["Bal_Type"].ToString();
			}
			else
			{
				return "";
			}
			//rdr.Close();
		}

		/// <summary>
		/// This method is used to return the only three charector of month name with the help of passing month.
		/// </summary>
		/// <param name="Month"></param>
		/// <returns></returns>
		public int getMonth(string Month)
		{
			if(Month.ToLower().StartsWith("jan"))
				return 1;
			else if(Month.ToLower().StartsWith("feb"))
				return 2;
			else if(Month.ToLower().StartsWith("mar"))
				return 3;
			else if(Month.ToLower().StartsWith("apr"))
				return 4;
			else if(Month.ToLower().StartsWith("may"))
				return 5;
			else if(Month.ToLower().StartsWith("jun"))
				return 6;
			else if(Month.ToLower().StartsWith("jul"))
				return 7;
			else if(Month.ToLower().StartsWith("aug"))
				return 8;
			else if(Month.ToLower().StartsWith("sep"))
				return 9;
			else if(Month.ToLower().StartsWith("oct"))
				return 10;
			else if(Month.ToLower().StartsWith("nov"))
				return 11;
			else if(Month.ToLower().StartsWith("dec"))
				return 12;
			else
				return 0;
		}

		double Debit=0;
		/// <summary>
		/// This method is used to calculate the total debit amount.
		/// </summary>
		protected string GetDebit(string dr)
		{
			Debit+=double.Parse(dr);
			Cache["dr"]=Debit.ToString();
			return dr;
		}

		/// <summary>
		/// This method is used to calculate the total credit amount.
		/// </summary>
		double Credit=0;
		protected string GetCredit(string cr)
		{
			Credit+=double.Parse(cr);
			Cache["cr"]=Credit.ToString();
			return cr;
		}

		/// <summary>
		/// This method is used to calculate the balance if balance is negetive then show credit amount(Cr) otherwise show debit amount(Dr).
		/// </summary>
		protected string setMonth()
		{
			double Bal=double.Parse(Cache["dr"].ToString())-double.Parse(Cache["cr"].ToString());
			string type= "Dr";
			if(Bal>=0)
				type=" Dr";
			else
				type=" Cr";
			Bal=Math.Abs(Bal);
			return Bal.ToString("N2",nfi)+type;
		}

		/// <summary>
		/// This method is used to calculate Opening Balance by passing values
		/// </summary>
		protected string setBal1(string _opb,string _opb1, string bal, string trans,string crt, string dbt, string balance)
		{
			string a = _opb.Substring(_opb.IndexOf("-")+1);
			if(trans.Equals("Opening Balance"))
			{
				if(bal=="Dr")
				{
					op_Balance=dbt+" "+bal;
					return op_Balance;
				}
				else if(bal=="Cr")
				{
					op_Balance=crt+" "+bal;
					return op_Balance;
				} 
			}
			string trans_no = trans.Substring(0,trans.IndexOf("(")-1);  
			if(trans_no.Equals("Purchase Invoice"))
			{
				if(bal.Equals("Dr"))
					op_Balance=_opb+" "+bal;
				if(bal.Equals("Cr"))
					//op_Balance=GenUtil.strNumericFormat(System.Convert.ToString(System.Convert.ToDouble(balance)-System.Convert.ToDouble(crt)))+" "+bal;
					op_Balance=_opb1+" "+bal;
				if(op_Balance.IndexOf("-")>=0)
				{
					op_Balance=op_Balance.Substring(op_Balance.IndexOf("-")+1);
					string Balance1=op_Balance.Substring(0,op_Balance.IndexOf(" "));
					op_Balance=GenUtil.strNumericFormat(Balance1)+" Dr";
					return op_Balance;
				}
				return op_Balance;
			}
			if(_opb.IndexOf("-")>=0)
			{
				if(bal=="Cr")
				{
					//op_Balance=GenUtil.strNumericFormat(System.Convert.ToString(System.Convert.ToDouble(balance)+System.Convert.ToDouble(dbt)-System.Convert.ToDouble(crt)))+" "+bal;
					op_Balance=_opb1+" "+bal;
					return op_Balance;
				}
				else
				{
					op_Balance=a+" "+"Cr";
					return op_Balance;
				}
			}
			else
			{
				if(bal=="Cr")
				{
					//op_Balance=GenUtil.strNumericFormat(System.Convert.ToString(System.Convert.ToDouble(balance)+System.Convert.ToDouble(dbt)-System.Convert.ToDouble(crt)))+" "+bal;
					op_Balance=_opb1+" "+bal;
					return op_Balance;
				}
				else
				{
					op_Balance=System.Convert.ToString(_opb)+" "+bal;
					return op_Balance;
				}
			}
		}
 
		/// <summary>
		/// Its set last Balance Type and called from .aspx page from closing balance template coulumn
		/// </summary>
		/// <param name="_baltype"></param>
		/// <returns></returns>
		protected string setType(string _baltype)
		{
			baltype  = _baltype; 
			return _baltype;
		}
       
		public int i=0;
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					
					string str = e.Item.Cells[1].Text;
					string trans_no = "";
					// if transaction type is Opening Balance then show the blank value in transaction no.
					if(str.StartsWith("Opening"))
					{
						e.Item.Cells[0].Text = "&nbsp;";
						i++;
					}
					else
					{
						trans_no = str.Substring(str.IndexOf("(")+1);
						if(str.StartsWith("Sales Invoice"))
						{
							if(trans_no.IndexOf(")")>0)
								trans_no = trans_no.Substring(0,trans_no.Length-1);
							if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
								trans_no=trans_no.Substring(4);
							else
								trans_no=trans_no.Substring(3);
						}
						else
							trans_no = trans_no.Substring(0,trans_no.Length-1);
						str = str.Substring(0,str.IndexOf("("));
						/***********Comment by Mahesh on09.07.008 
						str = str.Substring(0,str.IndexOf("("));
						if(str.StartsWith("Receipt"))
						{
							SqlDataReader rdr = null;
							if(e.Item.Cells[3].Text=="11,112,222.00")
							{
								
								int n=0;
								dbobj.SelectQuery("select Ledger_Name from Ledger_Master where Ledger_ID=(select distinct DiscLedgerID1 from Payment_Receipt where receipt_no='"+trans_no+"' and discLedgerID1!=0 and Discount1='"+e.Item.Cells[4].Text+"')",ref rdr);
								if(rdr.Read())
								{
									e.Item.Cells[1].Text = str.Trim()+" ("+rdr["Ledger_Name"].ToString()+")";
									n=1;
								}
								else
									e.Item.Cells[1].Text = str.Trim();
								if(n==0)
								{
									dbobj.SelectQuery("select Ledger_Name from Ledger_Master where Ledger_ID=(select distinct DiscLedgerID2 from Payment_Receipt where receipt_no='"+trans_no+"' and discLedgerID2!=0 and Discount2='"+e.Item.Cells[4].Text+"')",ref rdr);
									if(rdr.Read())
										e.Item.Cells[1].Text = str.Trim()+" ("+rdr["Ledger_Name"].ToString()+")";
									else
										e.Item.Cells[1].Text = str.Trim();
								}
								e.Item.Cells[3].Text="0.00";
							
							}
							else
							{
								double amt = double.Parse(e.Item.Cells[4].Text);
								dbobj.SelectQuery("select * from payment_receipt where Receipt_No='"+trans_no+"'",ref rdr);
								if(rdr.Read())
								{
									amt-=double.Parse(rdr["discount1"].ToString())+double.Parse(rdr["discount2"].ToString());
									e.Item.Cells[4].Text=amt.ToString();
								}
								e.Item.Cells[1].Text = str.Trim();
							}
						}
						else
							e.Item.Cells[1].Text = str.Trim();
						**********************************************************/
						e.Item.Cells[1].Text = str.Trim();
						e.Item.Cells[0].Text = trans_no ;
					}
					// Calls the Totaldebit() and TotalCredit() function to increment the total values for each row.
					TotalDebit(Double.Parse(e.Item.Cells[3].Text));
					TotalCredit(Double.Parse(e.Item.Cells[4].Text)); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					//e.Item.Cells[3].Text = debit_total.ToString("N",nfi);
					//e.Item.Cells[4].Text = credit_total.ToString("N",nfi);
					e.Item.Cells[5].Text = "(CB) "+balance+" "+baltype;
					//*****e.Item.Cells[5].Text = "(CB) "+GenUtil.strNumericFormat(bal.ToString())+" "+Type;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ItemTotal1(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					
					string str = e.Item.Cells[1].Text;
					// if transaction type is Opening Balance then  do not show the Datagrid1.
					if(str.StartsWith("Opening"))
					{
						Datagrid1.Visible=false;
					}
					else
					{
						/*SqlDataReader SqlDtr=null;
						dbobj.SelectQuery("Select top 1 Ledger_ID from Ledger_Master where Ledger_Name = '"+party_name+"'",ref SqlDtr); 
						if(SqlDtr.Read())
						{
							Ledger_ID = SqlDtr.GetValue(0).ToString();  
						}
						SqlDtr.Close();
						
						dbobj.SelectQuery("Select top 1 Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) < '"+ToMMddYYYY(txtDateFrom.Text)+"' order by Entry_Date desc",ref SqlDtr); 
						string bt="";
						string bl="";
						if(SqlDtr.Read())
						{
							bt=SqlDtr.GetValue(5).ToString();
							bl=SqlDtr.GetValue(4).ToString();
						}
						if(bt.Equals("Dr"))
						{
							e.Item.Cells[3].Text = GenUtil.strNumericFormat(bl);
							e.Item.Cells[4].Text = "0.00";
						}
						else
						{
							e.Item.Cells[4].Text = GenUtil.strNumericFormat(bl);
							e.Item.Cells[3].Text = "0.00";
						}*/
						
						e.Item.Cells[0].Text = "&nbsp;";
						e.Item.Cells[1].Text = "Opening Balance";
					}
				}
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This is used to make sorting of datagrid onclicking of datagridheader.
		/// </summary>
		string strorderby="";
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
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:sortcommand_Click()  EXCEPTION  "+ex.Message ); 
			}
		}

		/// <summary>
		/// This is used to bind the datagrid .
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlConnection sqlcon1=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//*****bhal*********
			SqlDataReader SqlDtr = null;
			if(DropReportType.SelectedIndex==0)
			{
				string drop_value = DropPartyName.Value.Trim();  
				string[] strArr = drop_value.Split(new char[] {':'},drop_value.Length);
				if(strArr.Length > 0)
				{
					party_name = strArr[0].Trim(); 
				}
				dbobj.SelectQuery("Select top 1 Ledger_ID from Ledger_Master where Ledger_Name = '"+party_name+"'",ref SqlDtr);
				if(SqlDtr.Read())
				{
					Ledger_ID = SqlDtr.GetValue(0).ToString();
				}
				SqlDtr.Close();
				
				string  sql="Select top 1 Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) < '"+ToMMddYYYY(txtDateFrom.Text)+"' order by Entry_Date desc"; 
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();
				da.Fill(ds,"AccountsLedgerTable");
				DataTable dtcustomer=ds.Tables["AccountsLedgerTable"]; 
				DataView dv=new DataView(dtcustomer);
				dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridSummerized.Visible=false;
				Datagrid1.DataSource=dv;
				if(dv.Count!=0)
				{
					Datagrid1.DataBind();
					Datagrid1.Visible=true;
					//CustomerGrid.ShowHeader=false;
					CustomerGrid.ShowHeader=false;
					CustomerGrid.Visible=false;	
				}
				else
				{
					Datagrid1.Visible=false;
					CustomerGrid.ShowHeader=true;
				}
				
				string sql1="Select Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+ToMMddYYYY(txtDateTo.Text)+"' order by Entry_Date"; 
				SqlDataAdapter da1=new SqlDataAdapter(sql1,sqlcon1);
				DataSet ds1=new DataSet();	
				da1.Fill(ds1," AccountsLedgerTable");
				DataTable dtcustomer1=ds1.Tables[" AccountsLedgerTable"];
				DataView dv1=new DataView(dtcustomer1);
				dv1.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				CustomerGrid.DataSource=dv1;
				GridSummerized.Visible=false;
				if(dv1.Count==0)
				{
					/*Comment By Vikas 5.3.2013 CustomerGrid.Visible=false;
					Datagrid1.Visible=false;
					MessageBox.Show("Data not available");
					return;*/
				}
				else
				{
					CustomerGrid.DataBind();
					CustomerGrid.Visible=true;
				}
				sqlcon.Dispose();
				sqlcon1.Dispose();
			}
			else
			{
				string sql="";
				string drop_value=DropPartyName.Value.Trim();
				string[] strArr = drop_value.Split(new char[] {':'},drop_value.Length);
				sql="Select distinct month(entry_date),substring(datename(month,entry_date),0,4)+' '+substring(datename(year,entry_date),3,4) entry_date,sum(Debit_Amount) Debit_Amount,sum(Credit_Amount) Credit_Amount,year(entry_date) dateyear from AccountsLedgerTable where ledger_id=(select ledger_id from ledger_master where ledger_name='"+strArr[0].ToString()+"') and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"' group by year(entry_date),datename(year,entry_date),datename(month,entry_date), month(entry_date) order by year(entry_date)";
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();
				da.Fill(ds,"AccountsLedgerTable");
				DataTable dtcustomer=ds.Tables["AccountsLedgerTable"]; 
				DataView dv=new DataView(dtcustomer);
				if(strorderby!="Entry_Date ASC")
					dv.Sort=strorderby;
				Cache["strorderby"]=strorderby;
				GridSummerized.DataSource=dv;
				if(dv.Count!=0)
				{
					GridSummerized.DataBind();
					GridSummerized.Visible=true;
					Datagrid1.Visible=false;
					CustomerGrid.Visible=false;
					CustomerGrid.ShowHeader=false;
				}
				else
				{
					Datagrid1.Visible=false;
					GridSummerized.Visible=false;
					CustomerGrid.Visible=false;
					CustomerGrid.ShowHeader=false;
					MessageBox.Show("Data Not Available");
				}
			}
		}
	
		string party_name = "";
		string Ledger_ID = "";

		/// <summary>
		/// This event occurres after pressing the view button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{
			try
			{
				//Checks the validity of inputs.
				if(!checkValidity())
				{
					return;
				}
				strorderby="Entry_Date ASC";
				Session["Column"]="Entry_Date";
				Session["order"]="ASC";
				Bindthedata();
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:cmdrpt_Click() User_ID:"+ uid ); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:cmdrpt_Click()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid ); 
			}
		}

		/// <summary>
		/// This event occurres after pressing the print button.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!checkValidity())
				{
					return;
				}

				// Get the home drive and opens the file CustomerLedger.txt in ServosmsPrintServices folder.
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LedgerReport.txt";
				StreamWriter sw = new StreamWriter(path);
				string info = "";		
				SqlDataReader SqlDtr = null;
				
				sw.Write((char)27);		//added by vishnu
				sw.Write((char)67);		//added by vishnu
				sw.Write((char)0);		//added by vishnu
				sw.Write((char)12);		//added by vishnu
			
				sw.Write((char)27);		//added by vishnu
				sw.Write((char)78);		//added by vishnu
				sw.Write((char)5);		//added by vishnu
							
				sw.Write((char)27);		//added by vishnu
				sw.Write((char)15);

				if(DropReportType.SelectedIndex==0)
				{
					string drop_value = DropPartyName.Value.Trim();  
					string party_name = "";
					string Ledger_ID = "";
					string trans_type = "";
					string trans_id = "";
					double debit = 0;
					double credit = 0;
					string bal1 = "";
					string[] strArr = drop_value.Split(new char[] {':'},drop_value.Length);
					if(strArr.Length > 0)
					{
						party_name = strArr[0].Trim(); 
					}
					dbobj.SelectQuery("Select top 1 Ledger_ID from Ledger_Master where Ledger_Name = '"+party_name+"'",ref SqlDtr); 
					if(SqlDtr.Read())
					{
						Ledger_ID = SqlDtr.GetValue(0).ToString();  
					}
					SqlDtr.Close();
					string des="-----------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					sw.WriteLine(GenUtil.GetCenterAddr("===============================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Customer Ledger From "+txtDateFrom.Text+" to "+txtDateTo.Text,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("===============================================",des.Length));
					sw.WriteLine("");
					sw.WriteLine("Party Name: "+DropPartyName.Value);
					sw.WriteLine("Remark    : (CB) Closing Balance ");
					sw.WriteLine("+------+-------------------------+----------+-----------+-----------+-------------------+");
					sw.WriteLine("|Trans.|    Transaction Type     |   Date   |   Debit   |   Credit  |  Closing Balance  |");
					sw.WriteLine("|  ID  |                         |          |           |           |                   |");  
					sw.WriteLine("+------+-------------------------+----------+-----------+-----------+-------------------+");
					//			   123456 1234567890123456789012345 1234567890 12345678901 12345678901 1234567890123456789
					//info = " {0,-6:S} {1,-16:S} {2,-10:S} {3,19:S} {4,11:F} {5,11:F} {6,19:S}";
					info = " {0,-6:S} {1,-25:S} {2,-10:S} {3,11:F} {4,11:F} {5,19:S}";
					string bt="";
					string bl="";
					string dbt="";
					string cdt="";
					if(Datagrid1.Items.Count!=0)
					{
					
						dbobj.SelectQuery("Select top 1 Ledger_ID from Ledger_Master where Ledger_Name = '"+party_name+"'",ref SqlDtr); 
						if(SqlDtr.Read())
						{
							Ledger_ID = SqlDtr.GetValue(0).ToString();  
						}
						SqlDtr.Close();
						
						dbobj.SelectQuery("Select top 1 Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) < '"+ToMMddYYYY(txtDateFrom.Text)+"' order by Entry_Date desc",ref SqlDtr); 
					
						if(SqlDtr.Read())
						{
							bt=SqlDtr.GetValue(5).ToString();
							bl=SqlDtr.GetValue(4).ToString();
						}
					
						if(bt.Equals("Dr"))
						{
							dbt = GenUtil.strNumericFormat(bl);
							cdt = "0.00";
						}
						else
						{
							cdt = GenUtil.strNumericFormat(bl);
							dbt = "0.00";
						}
						sw.WriteLine(info,"","Opening Balance",GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(0).ToString())),dbt,cdt,bl+" "+bt);
						//sw.WriteLine(info,"","Opening Balance",GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(0).ToString())),dbt,cdt,Tot1);
					}
				
					SqlDtr.Close();
			
					dbobj.SelectQuery("Select Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type,(Balance-Debit_Amount+Credit_Amount) opb from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+ToMMddYYYY(txtDateTo.Text)+"' order by Entry_Date",ref SqlDtr); 
					if(SqlDtr.HasRows)
					{
						while(SqlDtr.Read())
						{
							// if transaction type is opening balane then display the blank value in transaction ID.
							trans_type = SqlDtr["Particulars"].ToString();
							if(trans_type.StartsWith("Opening"))
							{
								trans_id = "";
							}
							else
							{
								trans_id = trans_type.Substring(trans_type.IndexOf("(")+1);
								if(trans_type.StartsWith("Sales Invoice"))
								{
									if(trans_id.IndexOf(")")>0)
										trans_id = trans_id.Substring(0,trans_id.Length-1);
									if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
										trans_id=trans_id.Substring(4);
									else
										trans_id=trans_id.Substring(3);
								}
								else
									trans_id = trans_id.Substring(0,trans_id.Length-1);
								trans_type = trans_type.Substring(0,trans_type.IndexOf("(")).Trim();  
								
							}
							// Calculate the total debit and credit and set the last value of balance and balance type into Bal.
							debit += Double.Parse(SqlDtr["Debit_Amount"].ToString());  
							credit += Double.Parse(SqlDtr["Credit_Amount"].ToString());
							bal1 = GenUtil.strNumericFormat(SqlDtr["Balance"].ToString())+" "+SqlDtr["Bal_Type"].ToString();
							
							sw.WriteLine(info,trans_id,
								trans_type,
								GenUtil.str2DDMMYYYY (trimDate(SqlDtr["Entry_Date"].ToString())),
								GenUtil.strNumericFormat(SqlDtr["Debit_Amount"].ToString()),
								GenUtil.strNumericFormat(SqlDtr["Credit_Amount"].ToString()),
								setBal(GenUtil.strNumericFormat(SqlDtr["balance"].ToString()))+" "+SqlDtr["Bal_Type"].ToString()
								); 
						}
					}
					else
					{
						//MessageBox.Show("Data not available" );
						//return;
					}
			
					SqlDtr.Close ();
					sw.WriteLine("+------+-------------------------+----------+-----------+-----------+-------------------+");
					sw.WriteLine(info,"Total:","","","","","(CB)"+bal1);
					sw.WriteLine("+------+-------------------------+----------+-----------+-----------+-------------------+");
				}
				else
				{
					string des="------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Customer Ledger From "+txtDateFrom.Text+" to "+txtDateTo.Text,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("=============================================",des.Length));
					sw.WriteLine("Report Type : "+DropReportType.SelectedItem.Text);
					sw.WriteLine("Party Name  : "+DropPartyName.Value);
					sw.WriteLine("Remark      : (CB) Closing Balance");
					sw.WriteLine("+----------+--------------------+--------------------+---------------------+");
					sw.WriteLine("|  Month   |    Debit Amount    |   Credit Amount    |   Closing Balance   |");
					sw.WriteLine("+----------+--------------------+--------------------+---------------------+");
					//             1234567890 12345678901234567890 12345678901234567890 123456789012345678901

					// To format the string to display into the printout.
					info = " {0,-10:S} {1,20:S} {2,20:S} {3,21:F}";
					string drop_value=DropPartyName.Value.Trim();
					string[] strArr = drop_value.Split(new char[] {':'},drop_value.Length);
					dbobj.SelectQuery("Select distinct month(entry_date),substring(datename(month,entry_date),0,4)+' '+substring(datename(year,entry_date),3,4) entry_date,sum(Debit_Amount) Debit_Amount,sum(Credit_Amount) Credit_Amount,year(entry_date) dateyear from AccountsLedgerTable where ledger_id=(select ledger_id from ledger_master where ledger_name='"+strArr[0].ToString()+"') and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"' group by year(entry_date),datename(year,entry_date),datename(month,entry_date), month(entry_date) order by year(entry_date)",ref SqlDtr);
					while(SqlDtr.Read())
					{
						sw.WriteLine(info,SqlDtr["entry_date"].ToString(),
							GenUtil.strNumericFormat(GetDebit(SqlDtr["Debit_Amount"].ToString())),
							GenUtil.strNumericFormat(GetCredit(SqlDtr["Credit_Amount"].ToString())),
							setCBal(SqlDtr["Entry_Date"].ToString(),SqlDtr["DateYear"].ToString())
							);
					}
					sw.WriteLine("+----------+--------------------+--------------------+---------------------+");
					sw.WriteLine(info,"Total",GenUtil.strNumericFormat(Cache["dr"].ToString()),GenUtil.strNumericFormat(Cache["cr"].ToString()),ClosingBal);
					sw.WriteLine("+----------+--------------------+--------------------+---------------------+");
				}
				sw.Close(); 
				Print(); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:BtnPrint_Click()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );  
			}
		
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\LedgerReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string drop_value = DropPartyName.Value.Trim();  
			string[] strArr = drop_value.Split(new char[] {':'},drop_value.Length);
			if(DropReportType.SelectedIndex==0)
			{
				string party_name = "";
				string Ledger_ID = "";
				string trans_type = "";
				string trans_id = "";
				double debit = 0;
				double credit = 0;
				string bal1 = "";
			
				if(strArr.Length > 0)
				{
					party_name = strArr[0].Trim(); 
				}
				dbobj.SelectQuery("Select top 1 Ledger_ID from Ledger_Master where Ledger_Name = '"+party_name+"'",ref SqlDtr); 
				if(SqlDtr.Read())
				{
					Ledger_ID = SqlDtr.GetValue(0).ToString();  
				}
				SqlDtr.Close();
				sw.WriteLine("Party Name\t"+DropPartyName.Value);
				sw.WriteLine("Transaction ID\tTransaction Type\tDate\tDebit\tCredit\tClosing Balance");
				string bt="";
				string bl="";
				string dbt="";
				string cdt="";
				if(Datagrid1.Items.Count!=0)
				{
					
					dbobj.SelectQuery("Select top 1 Ledger_ID from Ledger_Master where Ledger_Name = '"+party_name+"'",ref SqlDtr); 
					if(SqlDtr.Read())
					{
						Ledger_ID = SqlDtr.GetValue(0).ToString();
					}
					SqlDtr.Close();
						
					dbobj.SelectQuery("Select top 1 Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) < '"+ToMMddYYYY(txtDateFrom.Text)+"' order by Entry_Date desc",ref SqlDtr); 
					
					if(SqlDtr.Read())
					{
						bt=SqlDtr.GetValue(5).ToString();
						bl=SqlDtr.GetValue(4).ToString();
					}
					
					if(bt.Equals("Dr"))
					{
						dbt = GenUtil.strNumericFormat(bl);
						cdt = "0.00";
					}
					else
					{
						cdt = GenUtil.strNumericFormat(bl);
						dbt = "0.00";
					}
					sw.WriteLine("\tOpening Balance\t"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr.GetValue(0).ToString()))+"\t"+dbt+"\t"+cdt+"\t"+bl+" "+bt);
				}
				
				SqlDtr.Close();
			
				dbobj.SelectQuery("Select Entry_Date,Particulars,Debit_Amount,Credit_Amount,Balance, Bal_Type,(Balance-Debit_Amount+Credit_Amount) opb from AccountsLedgerTable where Ledger_ID = "+Ledger_ID+" and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+ToMMddYYYY(txtDateTo.Text)+"' order by Entry_Date",ref SqlDtr); 
				if(SqlDtr.HasRows)
				{
					//string DebitAmount="0.00";
					while(SqlDtr.Read())
					{
						// if transaction type is opening balane then display the blank value in transaction ID.
						trans_type = SqlDtr["Particulars"].ToString();
						if(trans_type.StartsWith("Opening"))
						{
							trans_id = "";
						}
						else
						{
							trans_id = trans_type.Substring(trans_type.IndexOf("(")+1);
							if(trans_type.StartsWith("Sales Invoice"))
							{
								if(trans_id.IndexOf(")")>0)
									trans_id = trans_id.Substring(0,trans_id.Length-1);
								if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
									trans_id=trans_id.Substring(4);
								else
									trans_id=trans_id.Substring(3);
							}
							else
								trans_id = trans_id.Substring(0,trans_id.Length-1);
							trans_type = trans_type.Substring(0,trans_type.IndexOf("(")).Trim();  
							
						}
						// Calculate the total debit and credit and set the last value of balance and balance type into Bal.
						debit += Double.Parse(SqlDtr["Debit_Amount"].ToString());  
						credit += Double.Parse(SqlDtr["Credit_Amount"].ToString());
						bal1 = GenUtil.strNumericFormat(SqlDtr["Balance"].ToString())+" "+SqlDtr["Bal_Type"].ToString();
  
						sw.WriteLine(trans_id+"\t"+
							trans_type+"\t"+
							GenUtil.str2DDMMYYYY (trimDate(SqlDtr["Entry_Date"].ToString()))+"\t"+
							GenUtil.strNumericFormat(SqlDtr["Debit_Amount"].ToString())+"\t"+
							GenUtil.strNumericFormat(SqlDtr["Credit_Amount"].ToString())+"\t"+
							setBal(GenUtil.strNumericFormat(SqlDtr["balance"].ToString()))+" "+SqlDtr["Bal_Type"].ToString()
							); 
					}	
				}
				else
				{
					//MessageBox.Show("Data not available");
					//return;
				}
			
				SqlDtr.Close ();
				sw.WriteLine("Total:\t\t\t\t\t"+"(CB)"+bal1.ToString());
			}
			else
			{
				sw.WriteLine("Report Type\t"+DropReportType.SelectedItem.Text);
				sw.WriteLine("Party Name\t"+DropPartyName.Value);
				sw.WriteLine("Month\tDebit Amount\tCredit Amount\tClosing Balance");
				dbobj.SelectQuery("Select distinct month(entry_date),substring(datename(month,entry_date),0,4)+' '+substring(datename(year,entry_date),3,4) entry_date,sum(Debit_Amount) Debit_Amount,sum(Credit_Amount) Credit_Amount,year(entry_date) dateyear from AccountsLedgerTable where ledger_id=(select ledger_id from ledger_master where ledger_name='"+strArr[0].ToString()+"') and cast(floor(cast(Entry_Date as float)) as datetime) >= '"+GenUtil.str2DDMMYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Entry_Date as float)) as datetime) <= '"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"' group by year(entry_date),datename(year,entry_date),datename(month,entry_date), month(entry_date) order by year(entry_date)",ref SqlDtr);
				while(SqlDtr.Read())
				{
					sw.WriteLine(SqlDtr["entry_date"].ToString()+"\t"+
						GetDebit(SqlDtr["Debit_Amount"].ToString())+"\t"+
						GetCredit(SqlDtr["Credit_Amount"].ToString())+"\t"+
						setCBal(SqlDtr["Entry_Date"].ToString(),SqlDtr["DateYear"].ToString())
						);
				}
				sw.WriteLine("Total"+"\t"+GenUtil.strNumericFormat(Cache["dr"].ToString())+"\t"+GenUtil.strNumericFormat(Cache["cr"].ToString())+"\t"+ClosingBal);
			}
			sw.Close();
		}

		/// <summary>
		/// This function displays returns the date with out time from passed datetime string.
		/// </summary>
		public string trimDate(string str)
		{
			if(str.IndexOf(" ")>0)
			{
				return str = str.Substring(0,str.IndexOf(" "));  
			}
			return str;
		}

		/// <summary>
		/// contacst the Print_WiindowServices via socket and sends the CustomerLedger.txt file name to print.
		/// </summary>
		public void Print()
		{
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				// Establish the remote endpoint for the socket.
				// The name of the
				// remote device is "host.contoso.com".
				IPHostEntry ipHostInfo = Dns.Resolve("127.0.0.1");
				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress,62000);

				// Create a TCP/IP  socket.
				Socket sender1 = new Socket(AddressFamily.InterNetwork, 
					SocketType.Stream, ProtocolType.Tcp );

				// Connect the socket to the remote endpoint. Catch any errors.
				try 
				{
					sender1.Connect(remoteEP);
					
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\LedgerReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerLedger.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		protected void txtDateFrom_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		protected void txtDateTo_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		protected void Datagrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		private void DropPartyName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// This method is used to prepares the excel report file LedgerReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(CustomerGrid.Visible==true || Datagrid1.Visible==true || GridSummerized.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:LedgerReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Ledger Report Convert Into Excel Format, userid  "+uid);
				}
				else
				{
					MessageBox.Show("Please Click the View Button First");
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:LedgerReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Ledger Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method return the only year part from given date
		/// </summary>
		public string GetYear(string dt)
		{
			if(dt!="")
			{
				string[] year=dt.Split(new char[] {'-'},dt.Length);
				string yr=year[2].Substring(2);	
				return(yr);
			}
			else
				return "";
		}

		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemTotal2(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					string str = e.Item.Cells[1].Text;
					string trans_no = "";
					// if transaction type is Opening Balance then show the blank value in transaction no.
					if(str.StartsWith("Opening"))
					{
						e.Item.Cells[0].Text = "&nbsp;";
					}
					else
					{
						// else show take the substring and display the no. in transaction no. and assign the remaining substring to transaction type.
						trans_no = str.Substring(str.IndexOf("(")+1);
						trans_no = trans_no.Substring(0,trans_no.Length-1);
						str = str.Substring(0,str.IndexOf("("));
						e.Item.Cells[0].Text = trans_no ;
						e.Item.Cells[1].Text = str.Trim();
					}
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					//if the row or item type is footer then display the calculated total debit, credit and last balance with type in the footer. nfi and "N" used to format the double no. in #,###.00 format.
					e.Item.Cells[5].Text = "(CB) "+GenUtil.strNumericFormat(Balance1.ToString())+" Dr";
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LedgerReport.aspx,Method:ItemTotal2()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}
	}
}