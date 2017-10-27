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
using DBOperations;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Sockets;
using Servosms.Sysitem.Classes ;
using System.IO;
using System.Text;
using RMG;


namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for ViewAccounts.
	/// </summary>
	public partial class CustomerWiseOutstanding : System.Web.UI.Page
	{
		
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public double debit_total = 0;
		public double credit_total = 0;	
		string uid;


		string dt1="";

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:pageload" + ex.Message+"  EXCEPTION     userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!IsPostBack)
			{
				grdLeg.Visible=false;
				Datagrid1.Visible=false;
				Datagrid2.Visible=false;
				Datagrid3.Visible=false;
				Session["Btype"]=drpOptions.SelectedItem.Value;
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="9";
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
				}
				#endregion
				txtDateTo.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
				txtDateFrom.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
				# region Fill dropType
				//				SqlCommand cmd;
				//				SqlConnection con;
				//				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//				con.Open ();
				//				SqlDataReader SqlDtr1; 
				//				cmd=new SqlCommand("select * from CustomerType order by CustomerTypeName",con);
				//				SqlDtr1=cmd.ExecuteReader();
				//				drpcustype.Items.Clear();
				//				drpcustype.Items.Add("All");
				//				while(SqlDtr1.Read())
				//				{
				//					drpcustype.Items.Add(SqlDtr1.GetValue(1).ToString());
				//				}
				//				con.Close();
				//				SqlDtr1.Close();
				#endregion
				GetMultiValue();
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
        }

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value from dropdownlist with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strName="",strType="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012

				//DropValue.Items.Clear();
				//DropValue.Items.Add("All");
				//if(DropSearchBy.SelectedIndex!=0)
				//{
				//				strName = "select distinct c.cust_name from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name";
				//				strType = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				//				strDistrict = "select distinct state from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by state";
				//				strPlace = "select distinct c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.city";
				//				strSSR = "select emp_name from employee where emp_id in(select ssr from vw_cust_ageing a,customer c where c.cust_id=a.cust_id)";
				
				//Coment by vikas 25.05.09 strName = "select cust_name from customer order by cust_name";
				strName = "select cust_name,city from customer order by cust_name,city";
				
				//coment by vikas 17.11.2012 strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";

				strDistrict = "select distinct state from customer order by state";
				strPlace = "select distinct city from customer order by city";
				strSSR = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012
					
				rdr = obj.GetRecordSet(strName);
				if(rdr.HasRows)
				{
					tempCustName.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						
						//Coment by vikas 25.05.09 tempCustName.Value+=rdr.GetValue(0).ToString()+",";
						tempCustName.Value+=rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString()+",";
					}
				}
				rdr.Close();
				/*Coment by vika 17.11.2012 rdr = obj.GetRecordSet(strType);
				if(rdr.HasRows)
				{
					tempCustType.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						tempCustType.Value+=rdr.GetValue(0).ToString()+",";
					}
				}
				rdr.Close();*/
				rdr = obj.GetRecordSet(strGroup);
				if(rdr.HasRows)
				{
					tempGroup.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
						{
							tempGroup.Value+=rdr.GetValue(0).ToString()+",";
						}
					}
				}
				rdr.Close();rdr = obj.GetRecordSet(strSubGroup);
				if(rdr.HasRows)
				{
					tempSubGroup.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
						{
							tempSubGroup.Value+=rdr.GetValue(0).ToString()+",";
						}
					}
				}
				rdr.Close();
				rdr = obj.GetRecordSet(strPlace);
				if(rdr.HasRows)
				{
					tempPlace.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						tempPlace.Value+=rdr.GetValue(0).ToString()+",";
					}
				}
				rdr.Close();
				rdr = obj.GetRecordSet(strDistrict);
				if(rdr.HasRows)
				{
					tempDistrict.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null )
							tempDistrict.Value+=rdr.GetValue(0).ToString()+",";
					}
				}
				rdr.Close();
				rdr = obj.GetRecordSet(strSSR);
				if(rdr.HasRows)
				{
					tempSSR.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						tempSSR.Value+=rdr.GetValue(0).ToString()+",";
					}
				}
				rdr.Close();
				//}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseOutStanding.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
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
		/// This is used split the date.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string ToSplit(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];
			strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length): str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			dt1 = yy+"-"+mm+"-"+dd;
			return(dt1);
		}
 
		/// <summary>
		/// this method is used to return date in MM/DD/YYYY format.
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

		//**************bhal*******************	
		/// <summary>
		/// calculates the total debit amount by passing value 
		/// </summary>
		protected double TotalDebit(double _debittotal)
		{
			debit_total  += _debittotal;
			return debit_total;
		}
 
		/// <summary>
		/// calculates total credit amount by passing value 
		/// </summary>
		protected double TotalCredit(double _credittotal)
		{
			credit_total  += _credittotal;
			return credit_total;
		}

		/// <summary>
		/// This is used with the method of onitemdatabound() at time of grid binding. 
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				if(e.Item.ItemType == ListItemType.Footer)	
				{
					e.Item.Cells[3].Text =  System.Math.Round(System.Convert.ToDouble(totaldr),2)+"Dr.     "+ System.Math.Round(System.Convert.ToDouble(totalcr),2)+"Cr."; 	
					Cache["opening"]=   System.Math.Round(System.Convert.ToDouble(totaldr),2)+"Dr.     "+ System.Math.Round(System.Convert.ToDouble(totalcr),2)+"Cr."; 		 
					//e.Item.Cells[3].Text =  totaldr.ToString()+"Dr.     "+ totalcr.ToString()+"Cr."; 
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customeroutstanding.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This is used with the method of onitemdatabound() at time of grid binding.
		/// </summary>
		public void ItemTotal1(object sender,DataGridItemEventArgs e)
		{
			try
			{
				double s1=0;
				double s2=0;
				double s3=0;
				double s4=0;
				if(e.Item.ItemType == ListItemType.Footer)	
				{
					Cache["Opndr"]=System.Math.Round(System.Convert.ToDouble(totaldr),2)+"Dr";
					Cache["Opncr"]=System.Math.Round(System.Convert.ToDouble(totalcr),2)+"Cr";
					Cache["Transdr"]=System.Math.Round(System.Convert.ToDouble(totaldrlim),2)+"Dr";
					Cache["Transcr"]=System.Math.Round(System.Convert.ToDouble(totalcrlim),2)+"Cr";
					
					s1=totaldr+totaldrlim;
					s2=totalcr+totalcrlim;
					if(s1>s2)
					{
						s3=s1-s2;
						e.Item.Cells[5].Text =  System.Math.Round(System.Convert.ToDouble(s3),2)+"Dr"; 
						Cache["closeAll"]=   System.Math.Round(System.Convert.ToDouble(s3),2)+"Dr"; 
					}
					else
					{
						s4=s2-s1;
						e.Item.Cells[5].Text =  System.Math.Round(System.Convert.ToDouble(s4),2)+"cr."; 
						Cache["closeAll"]=  System.Math.Round(System.Convert.ToDouble(s4),2)+"cr."; 
					}
						
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customeroutstanding.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This is used with the method of onitemdatabound() at time of grid binding.
		/// </summary>
		public void ItemTotaltr(object sender,DataGridItemEventArgs e)
		{
			try
			{
				
				if(e.Item.ItemType == ListItemType.Footer)	
				{
					//e.Item.Cells[3].Text =  totaldrlim.ToString()+"Dr.     "+ totalcrlim.ToString()+"Cr."; 
					//**e.Item.Cells[3].Text =  System.Math.Round(System.Convert.ToDouble(totaldrlim),2)+"Dr.     "+ System.Math.Round(System.Convert.ToDouble(totalcrlim),2)+"Cr."; 
					//**Cache["trans"]=  System.Math.Round(System.Convert.ToDouble(totaldrlim),2)+"Dr.     "+ System.Math.Round(System.Convert.ToDouble(totalcrlim),2)+"Cr."; 
					Cache["Transdr"]=System.Math.Round(System.Convert.ToDouble(totaldrlim),2)+"Dr";
					Cache["Transcr"]=System.Math.Round(System.Convert.ToDouble(totalcrlim),2)+"Cr";
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customeroutstanding.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This is used with the method of onitemdatabound() at time of grid binding.
		/// </summary>
		public void ItemTotalclosing(object sender,DataGridItemEventArgs e)
		{
			try
			{
				//public double baldr;
				//public double balcr;
				double s1=0;
				double s2=0;
				double s3=0;
				double s4=0;
				if(e.Item.ItemType == ListItemType.Footer)	
				{
					s1=baldr;
					s2=balcr;
					if(s1>s2)
					{
						s3=s1-s2;
						e.Item.Cells[3].Text =  System.Math.Round(System.Convert.ToDouble(s3),2)+"Dr."; 
						Cache["closing"]=   System.Math.Round(System.Convert.ToDouble(s3),2)+"Dr. "; 
					}
					else
					{
						s4=s2-s1;
						e.Item.Cells[3].Text =  System.Math.Round(System.Convert.ToDouble(s4),2)+"cr."; 
						Cache["closing"]=  System.Math.Round(System.Convert.ToDouble(s4),2)+"cr."; 
					}
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customeroutstanding.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}
		//***************************************	
		
		/// <summary>
		/// This is used to make the report for printing.
		/// </summary>
		public void MakingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\OutstandingReport.txt";
				StreamWriter sw = new StreamWriter(path);

				string info = "",sql1="";
				string strOpBalDr= "";
				string strOpBalCr= "";
				string cust_type="";				
				
				/*if(DropSearchBy.SelectedIndex==0)
				{
					if(chkZeroBal.Checked==true)
						//sql1 = "select * from custout where op_balance=0 and debitamount=0 and creditamount=0 and balance=0"; 
						sql1 = "select * from custout";
					else
						sql1 = "select * from custout where balance<>0";
				}
				else
				{
				
					//sql1 = "select * from custout where Cust_Type = '"+ drpcustype.SelectedItem.Value+"' and op_balance=0 and debitamount=0 and creditamount=0 and balance=0";
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value=="All")
							sql1 = "select * from custout";
						else
						{
							//coment by vikas 25.05.09 sql1 = "select * from custout where Cust_name = '"+ DropValue.Value+"'";
							cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql1 = "select * from custout where Cust_name = '"+cust_type.ToString()+"'";
						}
							
					}
						
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value=="All")
							sql1 = "select * from custout";
						else
						{
							// Comment By Vikas Sharma 17.04.09
							/*if(DropValue.Value=="Oe" || DropValue.Value=="OE")
								sql1 = "select * from custout where Cust_Type like '"+ DropValue.Value+"%'";
							else
								sql1 = "select * from custout where Cust_Type = '"+ DropValue.Value+"'";/
							sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value=="All")
							sql1 = "select * from custout";
						else
						{
							sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')";
						}
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value=="All")
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where cust_id in(select cust_id from customer where state='"+DropValue.Value+"')";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value=="All")
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where city = '"+ DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value=="All")
							sql1 = "select * from custout";
						else
							//sql1 = "select * from custout a,customer c where c.cust_id=a.cust_id and ssr = (select emp_id from employee where emp_name='"+ DropValue.Value+"')";
							sql1 = "select * from custout where cust_id in (select cust_id from customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"'))";
					}
					if(chkZeroBal.Checked!=true)
					{
						if(DropValue.Value=="All")
							sql1 += " where balance<>0";
						else
							sql1 += " and balance<>0";
					}
				}*/

				if(DropSearchBy.SelectedIndex==0)
				{
					if(chkZeroBal.Checked==true)
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout where balance<>0";
						else
							sql1 = "select * from custout where balance<>0 and BalanceType='Dr.'";
					}
				}
				else
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 = "select * from custout";
							else
								sql1 = "select * from custout where BalanceType='Dr.'";
						}
						else
						{
							cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql1 = "select * from custout where Cust_name = '"+cust_type+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 = "select * from custout";
							else
								sql1 = "select * from custout where BalanceType='Dr.'";
						}
						else
						{
							sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 = "select * from custout";
							else
								sql1 = "select * from custout where BalanceType='Dr.'";
						}
						else
						{
							sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')";
						}
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 = "select * from custout";
							else
								sql1 = "select * from custout where BalanceType='Dr.'";
						}
						else
							sql1 = "select * from custout where cust_id in(select cust_id from customer where state='"+DropValue.Value+"')";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 = "select * from custout";
							else
								sql1 = "select * from custout where BalanceType='Dr.'";
						}
						else
							sql1 = "select * from custout where city = '"+ DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 = "select * from custout";
							else
								sql1 = "select * from custout where BalanceType='Dr.'";
						}
						else
							sql1 = "select * from custout a,customer c where c.cust_id=a.cust_id and ssr = (select emp_id from employee where emp_name='"+ DropValue.Value+"')";
					}
					if(chkZeroBal.Checked!=true)
					{
						if(DropValue.Value=="All")
						{
							if(chkShowCR.Checked==true)
								sql1 += " where balance<>0";
							else
								sql1 += " where BalanceType='Dr.' and balance<>0";

							//sql1 += " where balance<>0";
						}
						else
						{
							if(chkShowCR.Checked==true)
								sql1 += " and balance<>0";
							else
								sql1 += " and BalanceType='Dr.' and balance<>0";
							//sql1 += " and balance<>0";
						}
					}
				}

				//sql1=sql1+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql1,ref rdr);
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
				if (drpOptions.SelectedItem.Value=="All")
				{  
					string des="----------------------------------------------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("CUSTOMER OUTSTANDING REPORT",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
				}
				//*********bhal add*******************
				if (drpOptions.SelectedItem.Value=="Opening Balance")
				{	
					//line break--bhal
					//sw.Write((char)27);
					//	sw.Write((char)67); 
					//	sw.Write((char)23); 
					string des="------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("CUSTOMER OUTSTANDING REPORT",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
				}
				if (drpOptions.SelectedItem.Value=="Transaction")
				{	
					//line break--bhal
					//	sw.Write((char)27);
					//  sw.Write((char)67); 
					//	sw.Write((char)23); 
					string des="------------------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("CUSTOMER OUTSTANDING REPORT",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
				}
				if (drpOptions.SelectedItem.Value=="Total Balance")
				{	
					//line break--bhal
					//	sw.Write((char)27);
					//	sw.Write((char)67); 
					//	sw.Write((char)23); 
					string des="------------------------------------------------------------------------";
					string Address=GenUtil.GetAddress();
					string[] addr=Address.Split(new char[] {':'},Address.Length);
					sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
					sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
					sw.WriteLine(des);
					//**********
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("CUSTOMER OUTSTANDING REPORT",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("=============================",des.Length));
				}
				//*************** End *************************************
				
				sw.WriteLine("Search By    : "+DropSearchBy.SelectedItem.Text+", Value : "+DropValue.Value);
				sw.WriteLine("Balance Type : "+drpOptions.SelectedItem.Text.ToString());
				sw.WriteLine("From         : "+txtDateTo.Text.ToString()+",  To  : "+txtDateFrom.Text.ToString());
				//sw.WriteLine("To           : "+txtDateFrom.Text.ToString());
				if (drpOptions.SelectedItem.Value=="All")
				{
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+-----------+-----------+---------------+");
					sw.WriteLine("|     Customer Name      |     City      |Customer Type|      Opening Balance      |     Transaction       |Closing Balance|");
					sw.WriteLine("|                        |               |             |  Dr.Amount  |  Cr.Amount  | Dr.Amount | Cr.Amount |               |");
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+-----------+-----------+---------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,13:F} {4,13:F} {5,11:F} {6,11:F} {7,15:S}";
				}

				if (drpOptions.SelectedItem.Value=="Opening Balance")
				{
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+");
					sw.WriteLine("|     Customer Name      |     City      |Customer Type|     Opening Balance       |");
					sw.WriteLine("|                        |               |             |  Dr.Amount  |  Cr.Amount  |");
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,13:F} {4,13:F}";
				}

				if (drpOptions.SelectedItem.Value=="Transaction")
				{
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+");
					sw.WriteLine("|     Customer Name      |     City      |Customer Type|       Transaction         |");
					sw.WriteLine("|                        |               |             |  Dr.Amount  |  Cr.Amount  |");
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,13:F} {4,13:F}";
				}

				if (drpOptions.SelectedItem.Value=="Total Balance")
				{
					sw.WriteLine("+------------------------+---------------+-------------+---------------+");
					sw.WriteLine("|     Customer Name      |     City      |Customer Type|Closing Balance|");
					sw.WriteLine("|                        |               |             |               |");
					sw.WriteLine("+------------------------+---------------+-------------+---------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,15:S}";
				}

				//              		       1234567890123456789012345 123456789012345 123456789012345 12345678.00 12345678.00 
                        
				if(rdr.HasRows)
				{
				
					while(rdr.Read())
					{
					
						if (drpOptions.SelectedItem.Value=="All")
						{
						
							if(rdr["Balance_Type"].ToString().Equals("Dr."))
							{
								strOpBalDr = rdr["Op_Balance"].ToString().Trim();
								strOpBalCr = "0";
							}
							else
							{
								strOpBalCr = rdr["Op_Balance"].ToString().Trim();
								strOpBalDr = "0";
							}
		
							//sw.WriteLine(info,rdr["Cust_Name"].ToString().Trim(),
							sw.WriteLine(info,StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),24),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),15),
								GenUtil.TrimLength(rdr["Cust_Type"].ToString(),13),
								GenUtil.strNumericFormat(strOpBalDr),
								GenUtil.strNumericFormat(strOpBalCr),
								GenUtil.strNumericFormat(rdr["DebitAmount"].ToString()),
								GenUtil.strNumericFormat(rdr["CreditAmount"].ToString()),
								GenUtil.strNumericFormat(rdr["Balance"].ToString()) + " " + rdr["BalanceType"].ToString()
								);
						}

						if (drpOptions.SelectedItem.Value=="Opening Balance")
						{
							if(rdr["Balance_Type"].ToString().Equals("Dr."))
							{
								strOpBalDr = rdr["Op_Balance"].ToString().Trim();
								strOpBalCr = "0";
							}
							else
							{
								strOpBalCr = rdr["Op_Balance"].ToString().Trim();
								strOpBalDr = "0";
							}
							sw.WriteLine(info,GenUtil.TrimLength(rdr["Cust_Name"].ToString().Trim(),24),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),15),
								GenUtil.TrimLength(rdr["Cust_Type"].ToString(),13),
								GenUtil.strNumericFormat(strOpBalDr),
								GenUtil.strNumericFormat(strOpBalCr)
								);
						}

				
						if (drpOptions.SelectedItem.Value=="Transaction")
						{
							if(rdr["Balance_Type"].ToString().Equals("Dr."))
							{
								strOpBalDr = rdr["Op_Balance"].ToString().Trim();
								strOpBalCr = "0";
							}
							else
							{
								strOpBalCr = rdr["Op_Balance"].ToString().Trim();
								strOpBalDr = "0";
							}
							sw.WriteLine(info,GenUtil.TrimLength(rdr["Cust_Name"].ToString().Trim(),24),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),15),
								GenUtil.TrimLength(rdr["Cust_Type"].ToString(),13),
								GenUtil.strNumericFormat(rdr["DebitAmount"].ToString()),
								GenUtil.strNumericFormat(rdr["CreditAmount"].ToString())
								);
						}
				
						if (drpOptions.SelectedItem.Value=="Total Balance")
						{
						
							sw.WriteLine(info,GenUtil.TrimLength(rdr["Cust_Name"].ToString().Trim(),24),
								GenUtil.TrimLength(rdr["City"].ToString().Trim(),15),
								GenUtil.TrimLength(rdr["Cust_Type"].ToString(),13),
								GenUtil.strNumericFormat(rdr["Balance"].ToString()) + " " + rdr["BalanceType"].ToString()
							
								);
						}
				
				
					}
				}
				if (drpOptions.SelectedItem.Value=="All")
				{
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+-----------+-----------+---------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,13:F} {4,13:F} {5,11:F} {6,11:F} {7,15:S}";
					//**info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,22:F} {4,22:F} {5,22:F}";
					//**sw.WriteLine(info," Total:","","",Cache["openAll"],Cache["transAll"],Cache["closeAll"] );                    
					sw.WriteLine(info,"      Total:","","",Cache["Opndr"],Cache["Opncr"],Cache["Transdr"],Cache["Transcr"],Cache["closeAll"] );
					// deselect Condensed
					//sw.Write((char)18);
					//sw.Write((char)12);
				}
				else if(drpOptions.SelectedItem.Value=="Total Balance")
				{
					sw.WriteLine("+------------------------+---------------+-------------+---------------+");
					sw.WriteLine(info," Total:","","",Cache["closing"] );  
				}
				else if(drpOptions.SelectedItem.Value=="Transaction")
				{
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,13:F} {4,13:F}";
					sw.WriteLine(info ,"Total:","","",Cache["Transdr"],Cache["Transcr"]);
				}
				else if(drpOptions.SelectedItem.Value=="Opening Balance")
				{
					sw.WriteLine("+------------------------+---------------+-------------+-------------+-------------+");
					info = " {0,-24:S} {1,-15:S} {2,-13:S} {3,22:F} ";
					sw.WriteLine(info ,"Total:","","",Cache["opening"]);
				} 
				else
					sw.WriteLine("+------------------------+---------------+-------------+-----------+-----------+");
				//dbobj.Insert_or_Update("truncate table custout", ref x);
				//dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:makingReport() "+"  Customerwise Outstanding Report Viewed  for customer "+DropValue.Value+ "  for balance type "+ Session["Btype"].ToString()+"  EXCEPTION  "+ex.Message+" userid "+uid );
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\CustomerWiseOutStanding.xls";
			StreamWriter sw = new StreamWriter(path);
			string strOpBalDr= "";
			string strOpBalCr= "",sql1="";
			string cust_name="";
			
			if(DropSearchBy.SelectedIndex==0)
			{
				if(chkZeroBal.Checked==true)
				{
					if(chkShowCR.Checked==true)
						sql1 = "select * from custout";
					else
						sql1 = "select * from custout where BalanceType='Dr.'";
				}
				else
				{
					if(chkShowCR.Checked==true)
						sql1 = "select * from custout where balance<>0";
					else
						sql1 = "select * from custout where balance<>0 and BalanceType='Dr.'";
				}
			}
			else
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql1 = "select * from custout where Cust_name = '"+cust_name+"'";
					}
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
					}
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')";
					}
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
						sql1 = "select * from custout where cust_id in(select cust_id from customer where state='"+DropValue.Value+"')";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
						sql1 = "select * from custout where city = '"+ DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
						sql1 = "select * from custout a,customer c where c.cust_id=a.cust_id and ssr = (select emp_id from employee where emp_name='"+ DropValue.Value+"')";
				}
				if(chkZeroBal.Checked!=true)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 += " where balance<>0";
						else
							sql1 += " where BalanceType='Dr.' and balance<>0";
					}
					else
					{
						if(chkShowCR.Checked==true)
							sql1 += " and balance<>0";
						else
							sql1 += " and BalanceType='Dr.' and balance<>0";
					}
				}
			}

			//sql1=sql1+" order by "+Cache["strorderby"];

			rdr=obj.GetRecordSet(sql1);
			sw.WriteLine("Search By\t"+DropSearchBy.SelectedItem.Text.ToString()+"\tValue\t"+DropValue.Value);
			sw.WriteLine("Balance Type\t"+drpOptions.SelectedItem.Text.ToString());
			sw.WriteLine("From\t"+txtDateTo.Text.ToString()+"\tTo\t"+DropValue.Value);
			//sw.WriteLine("To\t"+txtDateFrom.Text.ToString()); 
			if (drpOptions.SelectedItem.Value=="All")
			{
				sw.WriteLine("Customer Name\tCity\tCustomer Type\tOpening Balance\tOpening Balance\tTransaction\tTransaction\tClosing Balance");
				sw.WriteLine("\t\t\tDr.Amount\tCr.Amount\tDr.Amount\tCr.Amount\t");
			}

			if (drpOptions.SelectedItem.Value=="Opening Balance")
			{
				sw.WriteLine("Customer Name\tCity\tCustomer Type\tOpening Balance\tOpening Balance");
				sw.WriteLine("\t\t\tDr.Amount\tCr.Amount");
			}

			if (drpOptions.SelectedItem.Value=="Transaction")
			{
				sw.WriteLine("Customer Name\tCity\tCustomer Type\tTransaction\tTransaction");
				sw.WriteLine("\t\t\tDr.Amount\tCr.Amount");
			}

			if (drpOptions.SelectedItem.Value=="Total Balance")
			{
				sw.WriteLine("Customer Name\tCity\tCustomer Type\tClosing Balance");
			}
			if(rdr.HasRows)
			{
				
				while(rdr.Read())
				{
					if (drpOptions.SelectedItem.Value=="All")
					{
						if(rdr["Balance_Type"].ToString().Equals("Dr."))
						{
							strOpBalDr = rdr["Op_Balance"].ToString().Trim();
							strOpBalCr = "0";
						}
						else
						{
							strOpBalCr = rdr["Op_Balance"].ToString().Trim();
							strOpBalDr = "0";
						}
		
						//sw.WriteLine(info,rdr["Cust_Name"].ToString().Trim(),
						sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
							rdr["City"].ToString().Trim()+"\t"+
							rdr["Cust_Type"].ToString()+"\t"+
							GenUtil.strNumericFormat(strOpBalDr)+"\t"+
							GenUtil.strNumericFormat(strOpBalCr)+"\t"+
							GenUtil.strNumericFormat(rdr["DebitAmount"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["CreditAmount"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["Balance"].ToString()) + " " + rdr["BalanceType"].ToString()
							);
					}

					if (drpOptions.SelectedItem.Value=="Opening Balance")
					{
						if(rdr["Balance_Type"].ToString().Equals("Dr."))
						{
							strOpBalDr = rdr["Op_Balance"].ToString().Trim();
							strOpBalCr = "0";
						}
						else
						{
							strOpBalCr = rdr["Op_Balance"].ToString().Trim();
							strOpBalDr = "0";
						}
						sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
							rdr["City"].ToString().Trim()+"\t"+
							rdr["Cust_Type"].ToString()+"\t"+
							GenUtil.strNumericFormat(strOpBalDr)+"\t"+
							GenUtil.strNumericFormat(strOpBalCr)
							);
					}
				
					if (drpOptions.SelectedItem.Value=="Transaction")
					{
						if(rdr["Balance_Type"].ToString().Equals("Dr."))
						{
							strOpBalDr = rdr["Op_Balance"].ToString().Trim();
							strOpBalCr = "0";
						}
						else
						{
							strOpBalCr = rdr["Op_Balance"].ToString().Trim();
							strOpBalDr = "0";
						}
						sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
							rdr["City"].ToString().Trim()+"\t"+
							rdr["Cust_Type"].ToString()+"\t"+
							GenUtil.strNumericFormat(rdr["DebitAmount"].ToString())+"\t"+
							GenUtil.strNumericFormat(rdr["CreditAmount"].ToString())
							);
					}

					if (drpOptions.SelectedItem.Value=="Total Balance")
					{
						
						sw.WriteLine(rdr["Cust_Name"].ToString().Trim()+"\t"+
							rdr["City"].ToString().Trim()+"\t"+
							rdr["Cust_Type"].ToString()+"\t"+
							GenUtil.strNumericFormat(rdr["Balance"].ToString()) + " " + rdr["BalanceType"].ToString()
							);
					}
				}
			}
			if (drpOptions.SelectedItem.Value=="All")
			{
				sw.WriteLine("Total:\t\t\t"+Cache["Opndr"]+"\t"+Cache["Opncr"]+"\t"+Cache["Transdr"]+"\t"+Cache["Transcr"]+"\t"+Cache["closeAll"]);
			}
			else if(drpOptions.SelectedItem.Value=="Total Balance")
			{
				sw.WriteLine("Total:\t\t\t"+Cache["closing"] );  
			}
			else if(drpOptions.SelectedItem.Value=="Transaction")
			{
				sw.WriteLine("Total:\t\t\t"+Cache["Transdr"]+"\t"+Cache["Transcr"]);
			}
			else if(drpOptions.SelectedItem.Value=="Opening Balance")
			{
				string ope=Cache["opening"].ToString();
				string[] opening=ope.Split(new char[] {' '},ope.Length);
				sw.WriteLine("Total:\t\t\t"+opening[0].ToString()+"\t"+opening[5].ToString());
			} 
			//dbobj.Insert_or_Update("truncate table custout", ref x);
			//dbobj.Dispose();
			sw.Close();
		}

		/// <summary>
		/// This is used to make sorting of datagrid on click of datagridheader.
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
				CreateLogFiles.ErrorLog("Form:CustomerwiseOutstanding.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to binding of datagrid .
		/// </summary>
		public void Bindthedata()
		{
			object op=null;
			int x=0;
			System.Data.SqlClient.SqlDataReader rdr=null;
			dbobj.Insert_or_Update("truncate table custout", ref x);
			string sql1="";
			string cust_type="";
			//**bhal***string sql="select distinct cust_id from Customer order by cust_name";
			//string sql="select cust_id from Customer order by cust_name";
			if(DropSearchBy.SelectedIndex==0)
				sql1="select cust_id from Customer order by cust_name,city,cust_type";
			else
			{
				if(DropValue.Value=="All")
					sql1="select cust_id from Customer order by cust_name,city,cust_type";
				else
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						//Coment by vikas sharma 25.05.09 sql1="select cust_id from Customer where cust_name='"+DropValue.Value+"' order by cust_name,city,cust_type";
						
						/*******Coment by vikas 25.05.09 **************/
						cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql1="select cust_id from Customer where cust_name='"+cust_type+"' order by cust_name,city,cust_type";
						/*******End**************/
					}/******Add by vikas 17.11.2012***************/
					else if(DropSearchBy.SelectedIndex==2)
					{
						// Comment By Vikas Sharma
						/*if(DropValue.Value=="Oe" || DropValue.Value=="OE")
						sql1="select cust_id from Customer where cust_type like'"+DropValue.Value+"%' order by cust_name,city,cust_type";
						else
						sql1="select cust_id from Customer where cust_type like'"+DropValue.Value+"%' order by cust_name,city,cust_type";*/
						sql1="select cust_id from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"') order by cust_name,city,cust_type";
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						sql1="select cust_id from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"') order by cust_name,city,cust_type";
					}/******End***************/
					else if(DropSearchBy.SelectedIndex==4)
						sql1="select cust_id from Customer where state='"+DropValue.Value+"' order by cust_name,city,cust_type";
					else if(DropSearchBy.SelectedIndex==5)
						sql1="select cust_id from Customer where city='"+DropValue.Value+"' order by cust_name,city,cust_type";
					else if(DropSearchBy.SelectedIndex==6)
						sql1="select cust_id from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by cust_name,city,cust_type";
				}
			}
			dbobj.SelectQuery(sql1,ref rdr);
			while(rdr.Read())
				dbobj.ExecProc(OprType.Insert,"Sp_CustOutstanding ",ref op,"@id",Int32.Parse(rdr["cust_id"].ToString()),"@fromdate",getdate(txtDateTo.Text,true),"@todate",getdate(txtDateFrom.Text,true));
			rdr.Close();
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//string sqlstr="select * from vw_PriceList order by Prod_id";
			
			
			//if(drpcustype.SelectedItem.Value=="All")
			if(DropSearchBy.SelectedIndex==0)
			{
				if(chkZeroBal.Checked==true)
				{
					if(chkShowCR.Checked==true)
						sql1 = "select * from custout";
					else
						sql1 = "select * from custout where BalanceType='Dr.'";
				}
				else
				{
					if(chkShowCR.Checked==true)
						sql1 = "select * from custout where balance<>0";
					else
						sql1 = "select * from custout where balance<>0 and BalanceType='Dr.'";
				}
			}
			else
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql1 = "select * from custout where Cust_name = '"+cust_type+"'";
					}
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
					}
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
					{
						sql1 = "select * from custout where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')";
					}
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
						sql1 = "select * from custout where cust_id in(select cust_id from customer where state='"+DropValue.Value+"')";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
						sql1 = "select * from custout where city = '"+ DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 = "select * from custout";
						else
							sql1 = "select * from custout where BalanceType='Dr.'";
					}
					else
						sql1 = "select * from custout a,customer c where c.cust_id=a.cust_id and ssr = (select emp_id from employee where emp_name='"+ DropValue.Value+"')";
				}
				if(chkZeroBal.Checked!=true)
				{
					if(DropValue.Value=="All")
					{
						if(chkShowCR.Checked==true)
							sql1 += " where balance<>0";
						else
							sql1 += " where BalanceType='Dr.' and balance<>0";

						//sql1 += " where balance<>0";
					}
					else
					{
						if(chkShowCR.Checked==true)
							sql1 += " and balance<>0";
						else
							sql1 += " and BalanceType='Dr.' and balance<>0";
						//sql1 += " and balance<>0";
					}
				}
			}
			
			SqlDataAdapter da=new SqlDataAdapter(sql1,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"custout");
			DataTable dtcustomer=ds.Tables["custout"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
		
			if(drpOptions.SelectedItem.Value.Equals("Opening Balance"))
			{
				grdLeg.DataSource=dv;
				if(dv.Count!=0)
				{
					grdLeg.DataBind();
					grdLeg.Visible=true;
				}
				else
				{
					MessageBox.Show("Data not available");
				}
			}
			else if(drpOptions.SelectedItem.Value.Equals("Total Balance"))
			{    
				Datagrid1.DataSource=dv;				
				if(dv.Count!=0)
				{
					Datagrid1.Visible=true;				
					Datagrid1.DataBind();
				}
				else
				{
					MessageBox.Show("Data not available");
				}
			}
			
			else if(drpOptions.SelectedItem.Value.Equals("Transaction"))
			{
				Datagrid2.DataSource=dv;				
				if(dv.Count!=0)
				{
					Datagrid2.Visible=true;				
					Datagrid2.DataBind();
				}
				else
				{
					MessageBox.Show("Data not available");
				}
			}
			else if(drpOptions.SelectedItem.Value.Equals("All"))
			{
				Datagrid3.DataSource=dv;
				if(dv.Count!=0)
				{
					Datagrid3.Visible=true;
					Datagrid3.DataBind();
				}
				else
				{
					MessageBox.Show("Data not available");
				}
				
			}
			else
				MessageBox.Show("Data not available");
			//*********
			// Truncate the table after used
			//dbobj.Insert_or_Update("truncate table custout", ref x);
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to Printing the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{   
			Session["Btype"]=drpOptions.SelectedItem.Value;
			try
			{
				
				Datagrid1.DataSource = null;
				Datagrid1.DataBind(); 
				Datagrid2.DataSource = null;
				Datagrid2.DataBind(); 
				Datagrid3.DataSource = null;
				Datagrid3.DataBind(); 
				grdLeg.DataSource=null;				
				grdLeg.DataBind();
				strorderby="cust_Name ASC";
				Session["Column"]="cust_Name";
				Session["order"]="ASC";
				Bindthedata();
				

				CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:cmdrpt_Click "+  Session["Btype"].ToString() +"Customerwise Outstanding Report Viewed   on date  "+ "  and date to --"+ToSplit(txtDateTo.Text)+"   For Customer  "+DropValue.Value+ "  for balance type  "+Session["Btype"].ToString() +"    userid "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:cmdrpt_Click  "+"  Customerwise Outstanding Report Viewed  for customer "+DropValue.Value+ "  for balance type "+ Session["Btype"].ToString()+"  EXCEPTION  "+ex.Message+" userid "+uid );
			}
		}

		/// <summary>
		/// This is used to change the format.
		/// </summary>
		/// <param name="dat"></param>
		/// <param name="to"></param>
		/// <returns></returns>
		private DateTime getdate(string dat,bool to)
		{
			string[] dt=dat.IndexOf("/")>0?dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}

		/// <summary>
		/// Returns the value by adding the two precision after the "."
		/// </summary>
		protected string Limit(string str)
		{
			if(!str.Equals("")) 
			{
				double temp = System.Math.Round(System.Convert.ToDouble(str),2);
				str = temp.ToString(); 
			}
			else
				str = "0.00";
			return str;
		}
		//***********

		/// <summary>
		/// This is used sum of transactions for debitAmount.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected string Limit1(string str)
		{
			if(!str.Equals("")) 
			{
				double temp = System.Math.Round(System.Convert.ToDouble(str),2);
				totaldrlim+=temp;
				totaldrlim=	System.Math.Round(System.Convert.ToDouble(totaldrlim),2);
				str = temp.ToString(); 
			}
			else
				str = "0.00";
			return str;
		}

		/// <summary>
		/// This is used sum of transactions for creditAmount.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected string Limit2(string str)
		{
			if(!str.Equals("")) 
			{
				double temp = System.Math.Round(System.Convert.ToDouble(str),2);
				totalcrlim+=temp;
				totalcrlim=	System.Math.Round(System.Convert.ToDouble(totalcrlim),2);
				str = temp.ToString(); 
			}
			else
				str = "0.00";
			return str;
		}

		//**********
		/// <summary>
		/// This method s called from .aspx to check the balance type.
		/// </summary>
		/*bhal*/	double totalcr=0;
		/*bhal*/	double totaldr=0;
		/*bhal*/	double totalcrlim=0;
		/*bhal*/	double totaldrlim=0;
		protected string CheckCredit(string id)
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			dbobj.SelectQuery("select top 1 op_balance,balance_type from custout where cust_id='"+id+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["balance_type"].ToString().ToUpper().Equals("CR."))
				{
					totalcr+= System.Convert.ToDouble(rdr["op_balance"]);
					return System.Convert.ToString(Math.Round(System.Convert.ToDouble(rdr["op_balance"].ToString()),2)); 
				}
				else
					return "0.00";
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// This method is used to check the balance type is debit or not? called from .aspx
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		protected string CheckDebit(string id)
		{
			System.Data.SqlClient.SqlDataReader rdr=null;
			dbobj.SelectQuery("select top 1 op_balance,balance_type from custout where cust_id='"+id+"'",ref rdr);
			if(rdr.Read())
			{
				if(rdr["balance_type"].ToString().ToUpper().Equals("DR."))
				{
					totaldr+= System.Convert.ToDouble(rdr["op_balance"]);
					return System.Convert.ToString(Math.Round(System.Convert.ToDouble(rdr["op_balance"].ToString()),2)); 
				}
				else
					return "0.00";
			}
			return "0";
		}

		//*************************
		public double baldr;
		public double balcr;
		/// <summary>
		/// This is used sum of Closing Amount.
		/// </summary>
		/// <param name="bal"></param>
		/// <param name="baltype"></param>
		/// <returns></returns>
		protected string CheckClosing(string bal,string baltype)
		{
			//			System.Data.SqlClient.SqlDataReader rdr=null;
			//			dbobj.SelectQuery("select top 1 op_balance,balance_type from custout where cust_id='"+id+"'",ref rdr);
			if(!bal.Equals(""))
			{
				if(baltype.Equals("Dr."))
				{
					baldr+= System.Convert.ToDouble(bal);
				}
				else
					balcr+= System.Convert.ToDouble(bal);
				return System.Convert.ToString(Math.Round(System.Convert.ToDouble(bal),2))+baltype; 
			}
			else
				return "0";
		}
		//***************************
		/// <summary>
		/// This method is not used.
		/// </summary>
		private string GetString(string str,string spc)
		{
			if(str.Length>spc.Length)
				return str;
			else
				return str+spc.Substring(0,spc.Length-str.Length)+"  ";			
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private string GetString(string str,int maxlen,string spc)
		{		
			return str+spc.Substring(0,maxlen>str.Length?maxlen-str.Length:str.Length-maxlen);
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		private string MakeString(int len)
		{
			string spc="";
			for(int x=0;x<len;x++)
				spc+=" ";
			return spc;
		}
		/// <summary>
		/// This method is not used.
		/// </summary>
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6)
		{
			while(rdr.Read())
			{
				if(rdr["Cust_Name"].ToString().Trim().Length>len1)
					len1=rdr["Cust_Name"].ToString().Trim().Length;					
				if(rdr["City"].ToString().Trim().Length>len2)
					len2=rdr["City"].ToString().Trim().Length;					
				if(rdr["Cust_Type"].ToString().Trim().Length>len3)
					len3=rdr["Cust_Type"].ToString().Trim().Length;
				if(rdr["DebitAmount"].ToString().Trim().Length>len4)
					len4=rdr["DebitAmount"].ToString().Trim().Length;					
				if(rdr["CreditAmount"].ToString().Trim().Length>len5)
					len5=rdr["CreditAmount"].ToString().Trim().Length;					
			}
		}
		protected void grdLeg_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		private void txtDateTo_TextChanged(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// This is used to Print the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			Session["Btype"]=drpOptions.SelectedItem.Value;
			//CreateLogFiles Err = new CreateLogFiles();
			MakingReport();
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\OutstandingReport.txt<EOF>");
					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);
					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					//Err.ErrorLog(Server.MapPath("Logs/ErrorLog"),"Form:CustomerWiseOutstanding.aspx,Class:ObOpration_LATEST.cs,Method: BtnPrint_Click" );
					CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:BtnPrint_Click "+"  Customer Outstanding Report Printed  "+" userid "+uid );
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:BtnPrint_Click "+"  Customer Outstanding Report Printed  "+"  EXCEPTION  "+ ane.Message+" userid "+uid );
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:BtnPrint_Click "+"  Customer Outstanding Report Printed  "+"  EXCEPTION  "+se.Message+" userid "+uid );
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:BtnPrint_Click "+"  Customer Outstanding Report Printed  "+"  EXCEPTION  "+ es.Message+" userid "+uid );
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseOutstanding.aspx,Class:DbOperation_LETEST.cs,Method:BtnPrint_Click "+"  Customer Outstanding Report Printed  "+"  EXCEPTION  "+ ex.Message+" userid "+uid );
			}
		}
		private void Clear()
		{
		}
		private void drpOptions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}
		protected void Datagrid2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		private void drpcustype_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Prepares the excel report file CustomerWiseOutstanding.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(grdLeg.Visible==true || Datagrid1.Visible==true || Datagrid2.Visible==true || Datagrid3.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Bill Ageing Report Convert Into Excel Format, userid  "+uid);
				}
				else
				{
					MessageBox.Show("Please Click the View Button First");
					//return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				//				InventoryClass obj = new InventoryClass();
				//				SqlDataReader rdr=null;
				//				string str="";
				//				DropValue.Items.Clear();
				//				DropValue.Items.Add("All");
				//				if(DropSearchBy.SelectedIndex!=0)
				//				{
				//					if(DropSearchBy.SelectedIndex==1)
				//						str = "select distinct cust_name from customer order by cust_name";
				//					else if(DropSearchBy.SelectedIndex==2)
				//						str = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				//					else if(DropSearchBy.SelectedIndex==3)
				//						str = "select distinct state from customer order by state";
				//					else if(DropSearchBy.SelectedIndex==4)
				//						str = "select distinct city from customer order by city";
				//					else if(DropSearchBy.SelectedIndex==5)
				//						str = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";
				//					rdr = obj.GetRecordSet(str);
				//					if(rdr.HasRows)
				//					{
				//						while(rdr.Read())
				//						{
				//							//DropValue.Items.Add(rdr.GetValue(0).ToString());
				//						}
				//					}
				//					rdr.Close();
				//				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:DropSearch_SelectedIndexChanged    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}