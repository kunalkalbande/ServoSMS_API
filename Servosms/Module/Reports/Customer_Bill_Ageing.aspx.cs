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
using System.Data.SqlClient;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;


namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for Customer_Bill_Ageing.
	/// </summary>
	public partial class Customer_Bill_Ageing : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.Button Update;
		string uid;
		public SqlDataReader dtr=null;

		/// <summary>
		/// This method is used for setting the Session variable for userId and after that filling the required 
		/// dropdowns with database values and fill the data in LedgerDetails table with the help of 
		/// ProInsertLedgerDetails Procedure and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
           
            try
			{
				uid=(Session["User_Name"].ToString());
				// if the user is admin. then visible the update interest button and make the Ineterest Text field editable to modify or vice versa.
				if(Session["User_ID"].ToString().Equals("1001") )
				{
					InterestText.ReadOnly = false;
					Update1.Visible = true;              
				}
				else
				{
					InterestText.ReadOnly = true;
					Update1.Visible = false;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:pageload"+ ex.Message+" EXCEPTION  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"]==null? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString(): Request.Form["txtDateFrom"].ToString();
            Textbox1.Text = Request.Form["Textbox1"]==null? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString(): Request.Form["Textbox1"].ToString();
            if (!IsPostBack)
			{
				try
				{
					//****************************************************************************************
					//object ob=null;
					//dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetails",ref ob,"@Cust_ID","");
					/*SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					SqlCommand cmd;
					InventoryClass obj = new InventoryClass();
					InventoryClass obj1 = new InventoryClass();
					SqlDataReader rdr,rdr1;*/
					object ob=null;
					//call this procedure for fill the LedgerDetails Table when page is loaded, that procedure fill only that ledger whose balance is greter then zero.
					dbobj.ExecProc(DBOperations.OprType.Insert,"ProInsertLedgerDetails",ref ob,"@Cust_ID","");
					/*rdr = obj.GetRecordSet("select CustID,sum(creditamount) CreditAmount from customerledgertable group by custid order by custid");
					int c=0;
					if(rdr.HasRows)
					{
						while(rdr.Read())
						{
							double Amount=double.Parse(rdr["CreditAmount"].ToString());
							rdr1 = obj1.GetRecordSet("select * from LedgerDetails where Cust_ID='"+rdr["Custid"].ToString()+"' and Amount>0 order by Bill_Date");
							while(rdr1.Read())
							{
								Amount=Amount-double.Parse(rdr1["Amount"].ToString());
								if(Amount>=0)
									dbobj.Insert_or_Update("update LedgerDetails set Amount=0 where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",ref c);
								else
								{
									dbobj.Insert_or_Update("update LedgerDetails set Amount=abs("+Amount+") where Cust_ID='"+rdr["CustID"].ToString()+"' and Bill_No='"+rdr1["Bill_No"].ToString()+"'",ref c);
									break;
								}
							}
							rdr1.Close();
						}
					}
					rdr.Close();
					*/
					//****************************************************************************************
					GridReport.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="8";
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
					txtDateFrom.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					//Fetch The Interest Rate from mmisc_cd table and insert it into Interest Rate Text Field.
					System.Data.SqlClient.SqlDataReader rdr=null;
					dbobj.SelectQuery("Select Key_Descr from mmisc_cd where key_type = 'Interest'",ref rdr);
					if(rdr.Read())
						InterestText.Text = rdr["Key_Descr"].ToString();
					else
						InterestText.Text = "0";
					rdr.Close();
					GetMultiValue();
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:pageload()  EXCEPTION :"+ ex.Message+" User: "+uid);
				}
			}
		}

		public string BillAmount(string Invoice_No)
		{
			string Amount=" ";
			if(Invoice_No!="O/B")
			{
				
				InventoryClass obj=new InventoryClass();
				string sql="select distinct Net_Amount from sales_master where invoice_no='709"+ Invoice_No.ToString() +"'";  
				SqlDataReader SqlDtr = obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					Amount=SqlDtr.GetValue(0).ToString().Trim();
				}
				SqlDtr.Close();
				
			}
			return Amount;
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
				string strName="",strType="",strDistrict="",strPlace="",strSSR="",strInvoiceNo="";
				string strGroup="",strSubGroup="";
				//DropValue.Items.Clear();
				//DropValue.Items.Add("All");
				//if(DropSearchBy.SelectedIndex!=0)
				//{
				
				//coment by vikas sharma 25.05.09 strName = "select distinct c.cust_name from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name";
				
				strName = "select distinct c.cust_name,c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.cust_name";
				
				//strType = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				
				//coment by vikas 17.10.2012 strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";

				strDistrict = "select distinct state from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by state";
				strInvoiceNo = "select distinct invoice_no from vw_cust_ageing a,customer c where c.cust_id=a.cust_id and invoice_no<>'' order by invoice_no";
				strPlace = "select distinct c.city from vw_cust_ageing a,customer c where c.cust_id=a.cust_id order by c.city";
				strSSR = "select emp_name from employee where emp_id in(select ssr from vw_cust_ageing a,customer c where c.cust_id=a.cust_id) and status=1 order by emp_name";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 17.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 17.11.2012

				//coment by vikas 17.10.2012 string[] arrStr = {strName,strType,strDistrict,strPlace,strSSR,strInvoiceNo};
				//coment by vikas 17.10.2012 HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempDistrict,tempPlace,tempSSR,tempInvoiceNo};	

				string[] arrStr = {strName,strDistrict,strPlace,strSSR,strInvoiceNo,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempCustName,tempDistrict,tempPlace,tempSSR,tempInvoiceNo,tempGroup,tempSubGroup};	

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//DropValue.Items.Add(rdr.GetValue(0).ToString());
							//tempCustName.Value+=rdr.GetValue(0).ToString()+",";

							//coment by vikas sharma 25.05.06 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";

							/************Start Add by vikas sharma 25.05.09 *******************************/
							if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
							{
								if(i==0)
								{
									arrCust[i].Value+=rdr.GetValue(0).ToString()+":"+rdr.GetValue(1).ToString()+",";
								}
								else
								{
									arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
								}
							}
							/************End*******************************/
						}
					}
					rdr.Close();
				}
				/*rdr = obj.GetRecordSet(strType);
				if(rdr.HasRows)
				{
					tempCustType.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						tempCustType.Value+=rdr.GetValue(0).ToString()+",";
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
				rdr = obj.GetRecordSet(strInvoiceNo);
				if(rdr.HasRows)
				{
					tempInvoiceNo.Value="All,";
					while(rdr.Read())
					{
						//DropValue.Items.Add(rdr.GetValue(0).ToString());
						tempInvoiceNo.Value+=rdr.GetValue(0).ToString()+",";
					}
				}
				rdr.Close();*/
				//}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
		
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
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6)
		{
			while(rdr.Read())
			{
				if(rdr["Cust_ID"].ToString().Trim().Length>len1)
					len1=rdr["Cust_ID"].ToString().Trim().Length;					
				if(rdr["Cust_Name"].ToString().Trim().Length>len2)
					len2=rdr["Cust_Name"].ToString().Trim().Length;					
				if(rdr["City"].ToString().Trim().Length>len3)
					len3=rdr["City"].ToString().Trim().Length;
				if(rdr["Invoice_No"].ToString().Trim().Length>len4)
					len4=rdr["Invoice_No"].ToString().Trim().Length;					
				if(rdr["Invoice_Date"].ToString().Trim().Length>len5)
					len5=rdr["Invoice_Date"].ToString().Trim().Length;					
				if(rdr["Cr_Days"].ToString().Trim().Length>len6)
					len6=rdr["Cr_Days"].ToString().Trim().Length;	
			}
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
		/// Method to write into the report file to print.
		/// </summary>
		public void MakingReport()
		{
			
			string sql        = "";
			string info       = "";
			string strDate    = "";
			string strDueDate = "";
			string cust_name="";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\BillAgeingReport.txt";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;
			          			
			//sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"'";
			if(DropType.SelectedIndex==0)
			{
				if(DropSearchBy.SelectedIndex==0)
					sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'" ;
				else
				{
				
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
						{
							//coment by vikas 25.05.09 sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"' and cust_name='"+DropValue.Value+"'";
							
							/*******Add by vikas sharma 25.05.09 *****************************/
								
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and cust_name='"+cust_name+"'";
								
							/*******End*****************************/
						}
					}
						/******Add by vikas 17.11.2012***********************/
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
						{
							//if(DropValue.Value=="Oe")
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
							//else
							//	sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where cust_type='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
						{
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						}
					}/******End*17.11.2012**********************/
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where state='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and invoice_no='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and city='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')) and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					}
				}
				sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				string subTitle = "FROM : "+txtDateFrom.Text.ToString()+" TO : "+Textbox1.Text.ToString();
				if(c.Checked && !InterestText.Text.ToString().Trim().Equals("0")) 
					subTitle = subTitle+" with "+InterestText.Text.ToString().Trim()+"% Interest";
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
				string des="-------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("CUSTOMER BILL AGEING REPORT "+subTitle,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
				//sw.WriteLine("+-------+------------------------------+------------+-------+----------+--------------+-------+----------+------+-------+--------------+");
				//sw.WriteLine("|Cust.ID|Cust.Name                     |City        |Inv.No.|Inv.Date  | Bill Amount  |Cr.Days|Due Date  |>7Days|>15Days|30Days & Above|");
				//sw.WriteLine("+-------+------------------------------+------------+-------+----------+--------------+-------+----------+------+-------+--------------+");
				//sw.WriteLine("+-------+------------------------------+------------+-------+----------+--------------+-------+----------+--------------+------------------+");
				//sw.WriteLine("|Cust.ID|Cust.Name                     |City        |Inv.No.|Inv.Date  | Bill Amount  |Cr.Days|Due Date  |Total Due Days|Total Overdue Days|");
				//sw.WriteLine("+-------+------------------------------+------------+-------+----------+--------------+-------+----------+--------------+------------------+");
				//                         XXXX    123456789012345678901234567890 123456789012  XXXX   DD/MM/YYYY  12345678.90    XX     DD/MM/YYYY       999            999
				//sw.WriteLine(subTitle);
				//sw.WriteLine("Search By : "+DropSearchBy.SelectedItem.Text+",  Value : "+DropValue.Value);
				sw.WriteLine("+-----------------------------------+--------------------+------+----------+------------+----+----------+-------+-------+-----------+");
				sw.WriteLine("|         Customer Name             |       City         | Inv. |Inv. Date |Bill Amount | Cr.| Due Date | Total |Overdue| Amt. With |");
				sw.WriteLine("|                                   |                    |  No. |          |            |Days|          |DueDays| Days  | Interest  |");
				sw.WriteLine("+-----------------------------------+--------------------+------+----------+------------+----+----------+-------+-------+-----------+");
				// 1001 1234567890123456789012345 123456789012 123456 1234567890 123456789012 1234 1234567890 1234567 1234567 12345678901            	
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						// info used to set the display format of the string.
						//info = " {0,-4:S} {1,-35:S} {2,-20:S} {3,-6:S} {4,-10:S} {5,12:F} {6,4:D} {7,-10:S} {8,7:D} {9,7:D} {10,11:F}";
						info = " {0,-35:S} {1,-20:S} {2,-6:S} {3,-10:S} {4,12:F} {5,4:D} {6,-10:S} {7,7:D} {8,7:D} {9,11:F}";
					
						// Trim the Time part from Date
						strDate = rdr["Invoice_Date"].ToString().Trim();
						int pos = strDate.IndexOf(" ");
				
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}

						// Calculate Due Date

						strDueDate=rdr["due_date"].ToString(); 
						pos = strDueDate.IndexOf(" ");
						if(pos != -1)
						{
							strDueDate = strDueDate.Substring(0,pos);
						}
						else
						{
							strDueDate = "";					
						}

						sw.WriteLine(info,//rdr["Cust_ID"].ToString().Trim(),
							rdr["Cust_Name"].ToString().Trim(),
							rdr["City"].ToString(),
							rdr["Invoice_No"].ToString().Trim(),
							GenUtil.str2DDMMYYYY(strDate),
							GenUtil.strNumericFormat(rdr["Net_Amount"].ToString().Trim()),
							rdr["Cr_Days"].ToString().Trim(),
							GenUtil.str2DDMMYYYY(strDueDate),
							rdr["tcr"].ToString().Trim(),
							rdr["tdd"].ToString().Trim(),	             
							CalcInterest(rdr["Net_Amount"].ToString().Trim(),rdr["tdd"].ToString().Trim())
							);

					}
				}
			
				sw.WriteLine("+-----------------------------------+--------------------+------+----------+------------+----+----------+-------+-------+-----------+");
				sw.WriteLine(info,"          Total","","","",GenUtil.strNumericFormat(Cache["Amount"].ToString()),"","","","",Cache["Amt1"]);
				sw.WriteLine("+-----------------------------------+--------------------+------+----------+------------+----+----------+-------+-------+-----------+");
				// deselect Condensed
				//sw.Write((char)18);
				//sw.Write((char)12);
				sw.Close();
			}
			else
			{
				InventoryClass obj=new InventoryClass();
				sql="";
				string subTitle = "FROM : "+txtDateFrom.Text.ToString()+" TO : "+Textbox1.Text.ToString();	
				string des="-------------------------------------------------------------------------------------------------------------------------------------";
				if(dropCat.SelectedIndex==0)
				{
					if(DateTime.Compare(Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDateFrom.Text)),Convert.ToDateTime( GenUtil.str2DDMMYYYY(Textbox1.Text)))>0)
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' order by cust_id,invoice_date ,invoice_no" ;
					else
						sql="select * from vw_Cust_Ageing order by cust_id,invoice_date ,invoice_no" ;
				}
				else if(dropCat.SelectedIndex==1)
				{
					sql="select * from vw_Cust_Ageing where tcr between 1 and 15 order by cust_id,invoice_date ,invoice_no" ;
				}
				else if(dropCat.SelectedIndex==2)
				{
					sql="select * from vw_Cust_Ageing where tcr between 16 and 30 order by cust_id,invoice_date ,invoice_no" ;
				}
				else if(dropCat.SelectedIndex==3)
				{
					sql="select * from vw_Cust_Ageing where tcr >30 order by cust_id,invoice_date ,invoice_no" ;
				}
				dtr=obj.GetRecordSet(sql);
				int i=1;
				if(dtr.HasRows)
				{
					sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("NEW CUSTOMER BILL AGEING REPORT "+subTitle,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
					sw.WriteLine("+---+-----------------------------------+------------+----------+------------+-----------+-------+--------+");
					sw.WriteLine("|SNo|         Customer Name             |  Balance   | Bill No  |Bill Amount | Bill Date | Day's |Catagory|");
					sw.WriteLine("+---+-----------------------------------+------------+----------+------------+-----------+-------+--------+");
					//             012 01234567890123456789012345678901234 012345678901 0123456789 012345678901 01234567890 0123456 01234567
					info = "|{0,-3:S}|{1,-35:S}|{2,12:S}|{3,-10:S}|{4,12:S}|{5,11:S}|{6,-7:S}|{7,8:S}|";
					while(dtr.Read())
					{
						int Days=int.Parse(dtr.GetValue(8).ToString());
						String Catgory="";
						if(Days>=1 && Days<=15)
						{
							Catgory="Regular";
						}
						else if(Days>=16 && Days<=30)
						{
							Catgory="Beyond";
						}
						else
						{
							Catgory="Serious";
						}
						sw.WriteLine(info,
							i.ToString(),
							dtr.GetValue(1).ToString(),
							GenUtil.strNumericFormat(dtr.GetValue(5).ToString()),
							dtr.GetValue(3).ToString(),
							GenUtil.strNumericFormat(BillAmount(dtr.GetValue(3).ToString())),
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr.GetValue(4).ToString())).ToString(),
							dtr.GetValue(8).ToString(),
							Catgory.ToString());
						i++;
						//MessageBox.Show(dtr.GetValue(0).ToString()+" : "+dtr.GetValue(1).ToString());
					}
					dtr.Close();

					sw.WriteLine("+---+-----------------------------------+------------+----------+------------+-----------+-------+--------+");
					sw.Close();
				}
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
        /// This method returns the date in MM/DD/YYYY format.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public DateTime ToMMddYYYY(string str)
        //{
        //	int dd,mm,yy;
        //	string [] strarr = new string[3];			
        //	strarr=str.IndexOf("/")>0?str.Split(new char[]{'/'},str.Length):str.Split(new char[]{'-'},str.Length);
        //	dd=Int32.Parse(strarr[0]);
        //	mm=Int32.Parse(strarr[1]);
        //	yy=Int32.Parse(strarr[2]);
        //	DateTime dt=new DateTime(yy,mm,dd);			
        //	return(dt);
        //}

        /// <summary>
        /// This method is not used.
        /// </summary>
        protected string Stop(string str)
		{
			if(str.IndexOf(".")>0)
			{
				string ret=str.Substring(0,str.IndexOf(".")+3);
				return ret;
			}
			else
				return str;
		}


		/*	// This method called from ".aspx" page to calculate the interest if the check box is checked.
			protected string CalcInterest(string str,string tdd)
			{
				double amt=0;
					double interest=0;
					// if the check box is checkd then calculate intrest.
					if(c.Checked)
					{
						interest = System.Convert.ToDouble(InterestText.Text.ToString());
						if(interest == 0)
							return "";
						else
						{
							if(!str.Trim().Equals("")  )
							{
								amt = System.Convert.ToDouble(str);
								//amt = amt+(amt*(interest/100));
								amt=((amt*(interest/100))/365)*System.Convert.ToDouble(tdd);
						
							}
							else
								amt = 0;


							return GenUtil.strNumericFormat(amt.ToString()); 
					
						}				
					}
					return "";
			
				
			}*/
		//************************
		/// <summary>
		/// This method called from ".aspx" page to calculate the interest if the check box is checked.
		/// </summary>
		double amt1=0;
		protected string CalcInterest(string str,string ttd)
		{
			double amt=0;
			
			double interest=0;
			// if the check box is checkd then calculate intrest.
			if(c.Checked)
			{
				interest = System.Convert.ToDouble(InterestText.Text.ToString());
				if(interest == 0)
					return "";
				else
				{
					if(!str.Trim().Equals("")  )
					{
						amt = System.Convert.ToDouble(str);
						//amt = amt+(amt*(interest/100));
						amt = ((amt*(interest/100))/365)*System.Convert.ToDouble(ttd);
					}
					else
						amt = 0;
					amt1+=System.Convert.ToDouble(GenUtil.strNumericFormat(amt.ToString()));
					Cache["Amt1"]=GenUtil.strNumericFormat(amt1.ToString()); 
					return GenUtil.strNumericFormat(amt.ToString()); 
					
				}				
			}
			else
				Cache["Amt1"]="";
			return "";
		}

		double amount=0;
		/// <summary>
		/// This method is used to calculate the total amount.
		/// </summary>
		/// <param name="amt"></param>
		/// <returns></returns>
		protected string SumAmount(string amt)
		{
			amount+=System.Convert.ToDouble(amt);
			Cache["Amount"]=amount;
			return GenUtil.strNumericFormat(amt);
		}

		//*************************
		/// <summary>
		/// This is used to sorting the datagrid on clicking of datagridheader.
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
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:SortCommand_Click"+"  Cusetomer Ageing Report  "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		/// <summary>
		/// This is used to bind the data in datagrid with the help of query.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//string sqlstr="select * from vw_PriceList order by Prod_id";
			string sql="";
			string cust_name="";
			if(DropSearchBy.SelectedIndex==0)
				sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'" ;
			else
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
					{
						//coment by vikas 25.05.09 sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"' and cust_name='"+DropValue.Value+"'";
						
						/*******Add by vikas sharma 25.05.09 *****************************/
						
						cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and cust_name='"+cust_name+"'";
						
						/*******End*****************************/
					}
				}/******Add by vikas 17.11.2012***********************/
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
					{
						//if(DropValue.Value=="Oe")
						sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						//else
						//	sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where cust_type='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"'";
					}
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
					{
						sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					}
				}/******End*17.11.2012**********************/
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
						sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where state='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and invoice_no='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and city='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==7)
				{
					if(DropValue.Value=="All")
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2DDMMYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					else
						sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')) and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
				}
			}
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_Cust_Ageing");
			DataTable dtcustomer=ds.Tables["vw_Cust_Ageing"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridReport.DataSource=dv;
			if(dv.Count!=0)
			{
				GridReport.DataBind();
				GridReport.Visible=true;
			}
			else
			{
				GridReport.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to show the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{     
			try
			{
				if(DropType.SelectedIndex==0)
				{
                    if (System.DateTime.Compare(Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString().Trim())), Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString().Trim()))) > 0)
                      //  if (DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(Textbox1.Text))>0)
                    {
						MessageBox.Show("Date From Should be less than Date To");
						GridReport.Visible=false;
					}
					else
					{
						strorderby="Cust_ID ASC";
						Session["Column"]="Cust_ID";
						Session["order"]="ASC";
						Bindthedata();
					}
				}
				else
				{
					GridReport.Visible=false;
				}
				CreateLogFiles.ErrorLog("Form:Customer_Billing.aspx,Class:PetrolPumpClass.cs ,Method:btnShow_Click"+"Customer Ageing Report viewed "+" userid "+uid);

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Billing.aspx,Class:PetrolPumpClass.cs ,Method:btnShow_Click"+"Customer Ageing Report viewed "+" EXCEPTION  "+ex.Message+" userid "+uid);
			}
		}

		/// <summary>
		/// This method is used to print the report before prepare the .txt file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				MakingReport();
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\BillAgeingReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:BtnPrint_Click"+"  Cusetomer Ageing Report Printed "+" EXCEPTION  "+ane.Message+" userid " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:BtnPrint_Click"+"  Cusetomer Ageing Report Printed "+" EXCEPTION  "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:BtnPrint_Click"+"  Cusetomer Ageing Report Printed "+"  EXCEPTION "+es.Message+"  userid " +uid);
				}
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:BtnPrint_Click"+"  Cusetomer Ageing Report Printed "+"  " +uid);


			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:BtnPrint_Click"+"  Cusetomer Ageing Report Printed "+" EXCEPTION  "+es.Message+" userid "+ uid);

			}
		}

		private void GridReport_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void InterestText_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// This is used to updations on behalf of the Interest.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Update1_Click(object sender, System.EventArgs e)
		{
			// Method to update the ineterst rate . but only by administrator.
			try
			{
				string strInterest = InterestText.Text.ToString().Trim();  
				if(strInterest.Equals(""))
				{
					MessageBox.Show("Please enter Interest Rate");
					return ;
				}
				int i=0;
				dbobj.Insert_or_Update("Update mmisc_cd set Key_Descr = '"+strInterest+"' where key_type = 'Interest'",ref i);
				MessageBox.Show("Interest Updated"); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:Update1_Click"+"  Cusetomer Ageing Report Printed "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		/// <summary>
		/// This is used to make sorting the datagrid on clicking of datagridheader.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void SortCommand_Click(Object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:SortCommand_Click"+"  Cusetomer Ageing Report Printed "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		/// <summary>
		/// Prepares the excel report file CustomerBillAgeing.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//20.3.2013 if(GridReport.Visible==true)
				//20.3.2013 {
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Bill Ageing Report Convert Into Excel Format, userid  "+uid);
				//20.3.2013 }
				//20.3.2013 else
				//20.3.2013 {
					//20.3.2013 MessageBox.Show("Please Click the View Button First");
					//20.3.2013 return;
				//20.3.2013 }
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\Customer_Bill_Ageing.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string cust_name="";
			//sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"'"; 			
			if(DropType.SelectedIndex==0)
			{
				if(DropSearchBy.SelectedIndex==0)
					sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Textbox1.Text)+"'" ;
				else
				{
				
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
						{
							//coment by vikas 25.05.09 sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"' and cust_name='"+DropValue.Value+"'";
								
							/*******Add by vikas sharma 25.05.09 *****************************/
									
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and cust_name='"+cust_name+"'";
									
							/*******End*****************************/
						}
					}/******Add by vikas 17.11.2012***********************/
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
						{
							//if(DropValue.Value=="Oe")
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
							//else
							//	sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where cust_type='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(ToMMddYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(ToMMddYYYY(Textbox1.Text)).ToShortDateString()+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
						{
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						}
					}/******End*17.11.2012**********************/
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where state='"+DropValue.Value+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and invoice_no='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' and city='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value=="All")
							sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
						else
							sql="select * from vw_cust_ageing where cust_id in(select cust_id from customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')) and cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"'";
					}
				}
				sql=sql+" order by "+Cache["strorderby"];
			
				rdr=obj.GetRecordSet(sql);
				sw.WriteLine("From Date\t"+txtDateFrom.Text+"\tTo Date\t"+Textbox1.Text);
				//sw.WriteLine("Search By\t"+DropSearchBy.SelectedItem.Text+"\tValue\t"+DropValue.Value);
				sw.WriteLine("Customer Name\tPlace\tInvoice No\tInvoice Date\tBill Amount\tCredit Days\tDue Date\tTotal Due Days\tTotal OverDue Days\tAmount With Interest");
				while(rdr.Read())
				{
					sw.WriteLine(//rdr["Cust_ID"].ToString().Trim()+"\t"+
						rdr["Cust_Name"].ToString().Trim()+"\t"+
						rdr["City"].ToString()+"\t"+
						rdr["Invoice_No"].ToString().Trim()+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Invoice_Date"].ToString()))+"\t"+
						GenUtil.strNumericFormat(rdr["Net_Amount"].ToString().Trim())+"\t"+
						rdr["Cr_Days"].ToString().Trim()+"\t"+
						GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Due_Date"].ToString()))+"\t"+
						rdr["tcr"].ToString().Trim()+"\t"+
						rdr["tdd"].ToString().Trim()+"\t"+	             
						CalcInterest(rdr["Net_Amount"].ToString().Trim(),rdr["tdd"].ToString().Trim())
						);
				}
				sw.WriteLine("Total\t\t\t\t"+GenUtil.strNumericFormat(Cache["Amount"].ToString())+"\t\t\t\t\t"+Cache["Amt1"]);
				sw.Close();
			}
			else
			{
				sql="";
				string subTitle = "FROM : "+txtDateFrom.Text.ToString()+" TO : "+Textbox1.Text.ToString();	
				string des="-------------------------------------------------------------------------------------------------------------------------------------";
				if(dropCat.SelectedIndex==0)
				{
					if(DateTime.Compare(Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDateFrom.Text)), Convert.ToDateTime(GenUtil.str2DDMMYYYY(Textbox1.Text)))>0)
						sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' order by cust_id,invoice_date ,invoice_no" ;
					else
						sql="select * from vw_Cust_Ageing order by cust_id,invoice_date ,invoice_no" ;
				}
				else if(dropCat.SelectedIndex==1)
				{
					sql="select * from vw_Cust_Ageing where tcr between 1 and 15 order by cust_id,invoice_date ,invoice_no" ;
				}
				else if(dropCat.SelectedIndex==2)
				{
					sql="select * from vw_Cust_Ageing where tcr between 15 and 30 order by cust_id,invoice_date ,invoice_no" ;
				}
				else if(dropCat.SelectedIndex==3)
				{
					sql="select * from vw_Cust_Ageing where tcr >30 order by cust_id,invoice_date ,invoice_no" ;
				}
				dtr=obj.GetRecordSet(sql);
				int i=1;
				if(dtr.HasRows)
				{
					sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("NEW CUSTOMER BILL AGEING REPORT "+subTitle,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("============================================================",des.Length));
					sw.WriteLine("SNo\tCustomer Name\tBalance\tBill No\tBill Amount\tBill Date\tDay's\tCatagory");
					while(dtr.Read())
					{
						int Days=int.Parse(dtr.GetValue(8).ToString());
						String Catgory="";
						if(Days>=1 && Days<=15)
						{
							Catgory="Regular";
						}
						else if(Days>15 && Days<=30)
						{
							Catgory="Beyond";
						}
						else
						{
							Catgory="Serious";
						}
						sw.WriteLine(
							i.ToString()+"\t"+
							dtr.GetValue(1).ToString()+"\t"+
							GenUtil.strNumericFormat(dtr.GetValue(5).ToString())+"\t"+
							dtr.GetValue(3).ToString()+"\t"+
							GenUtil.strNumericFormat(BillAmount(dtr.GetValue(3).ToString()))+"\t"+
							GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr.GetValue(4).ToString())).ToString()+"\t"+
							dtr.GetValue(8).ToString()+"\t"+
							Catgory.ToString());
						i++;
						//MessageBox.Show(dtr.GetValue(0).ToString()+" : "+dtr.GetValue(1).ToString());
					}
					dtr.Close();

					//sw.WriteLine("+---+-----------------------------------+------------+----------+------------+-----------+-------+--------+");
					sw.Close();
				}
			}
		}

		protected void DropSearch_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
		}
	}
}