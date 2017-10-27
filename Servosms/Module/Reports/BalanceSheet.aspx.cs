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
	/// Summary description for BalanceSheet.
	/// </summary>
	public partial class BalanceSheet : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateFrom;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDateTo;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
	
		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
				if(!IsPostBack )
				{
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="2";
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
					Table1.Visible = false; 
					txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
					txtDateTo.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				}
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:page_load.  EXCEPTION: "+ ex.Message+"  User: "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
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

		protected void Textbox1_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// This method is Returns the date in MM?DD/YYYY format.
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

		/// <summary>
		/// This method is used to seprate time from date and returns only date in mm/dd/yyyy
		/// </summary>
		/// <param name="strDate"></param>
		/// <returns></returns>
		public string trimDate(string strDate)
		{
			int pos = strDate.IndexOf(" ");
			if(pos != -1)
			{
				strDate = strDate.Substring(0,pos);
			}
			else
			{
				strDate = "";					
			}
			return strDate;
		}

		/// <summary>
		/// This method fetch the Account Period from date , from Organisation table for balance sheet
		/// </summary>
		/// <returns></returns>
		public string getFromDate()
		{
			SqlDataReader SqlDtr = null;
			string date = "";
			dbobj.SelectQuery("Select Acc_date_From from Organisation",ref SqlDtr);
			if(SqlDtr.Read())
			{
               date = trimDate(SqlDtr.GetValue(0).ToString());  
			}
			SqlDtr.Close();
			return date;
		}

		/// <summary>
		/// This method is used to show the balancesheet of given time period.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {                    
					MessageBox.Show("Date From Should be less than Date To");
					return;
				}
				Table1.Visible = true; 
				
				SqlConnection con = null;
				SqlCommand cmd= null;
				SqlDataReader SqlDtr = null;
				string Op_Stock= "";
				double Opening_Stock = 0;
				string Cl_Stock= "";
				double Closing_Stock = 0;
				string Net_Profit = "";
				double Net_Pro = 0;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();

				// This calls the getProfitLoss procedure to fetch the Net Profit , Opening Stock and Closing stock.
				cmd = new SqlCommand( "exec getProfitLoss '"+ GenUtil.str2DDMMYYYY(getFromDate())+"','"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"'", con );
				SqlDtr = cmd.ExecuteReader();
				if(SqlDtr.Read())
				{
                   Net_Profit = GenUtil.strNumericFormat(SqlDtr["Net_Profit"].ToString());
					if(!Net_Profit.Trim().Equals("") )
					{
						Net_Pro = System.Convert.ToDouble(Net_Profit);
						// If the Net Profit is -ve then display under Profit and Loss account else display under the Reserver & Surplus.
						if(Net_Pro < 0 )
						{
							Net_Pro = (Net_Pro * -1);
                            lblProfitLossValue.Text = GenUtil.strNumericFormat(Net_Pro.ToString());   
							lblRes_Sur_Value.Text = "0.00";
						}
						else
						{
							lblRes_Sur_Value.Text = GenUtil.strNumericFormat(Net_Pro.ToString());  
							lblProfitLossValue.Text = "0.00";
						}
					}
					Op_Stock = GenUtil.strNumericFormat(SqlDtr["Opening_Stock"].ToString());
					Cl_Stock = GenUtil.strNumericFormat(SqlDtr["Closing_Stock"].ToString());

				}
				SqlDtr.Close();
               
				string op_bal_dr = "";
				string op_bal_cr = "";
				double op_bal_d = 0;
				double op_bal_c = 0;

				// Its calls the getBalanceSheet stored procedure to calculates the closing balances of all the accounts from 1st april to selected end date.
				cmd = new SqlCommand( "exec getBalanceSheet '"+ GenUtil.str2DDMMYYYY(getFromDate())+"','"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"'", con );
				SqlDtr = cmd.ExecuteReader();
				if(SqlDtr.Read())
				{
                    op_bal_dr = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
					op_bal_cr = GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString());
                
					if(!Op_Stock.Trim().Equals(""))
					{
						Opening_Stock = System.Convert.ToDouble(Op_Stock);   
					}
					 
					// Adds the Opening Stock into diff. in opening balances.
					if(!op_bal_dr.Trim().Equals(""))
					{
						op_bal_d = System.Convert.ToDouble(op_bal_dr);   
						
						op_bal_d = op_bal_d + Opening_Stock;
					}
					if(!op_bal_cr.Trim().Equals(""))
					{
						op_bal_c = System.Convert.ToDouble(op_bal_cr);  
 
					}
					
					//calculate the opening balances . if the debit balances are greater then display the difference on credit side and if credit balances r more than debit then display the diff. on debit side.
					if(op_bal_d != op_bal_c)
					{
						op_bal_d = op_bal_d - op_bal_c;
						if(op_bal_d < 0)
						{
							op_bal_d = (op_bal_d * -1);
							lblDiffBalDebit.Visible = true;
							lblDiffCreditBal.Visible = false;
							lblDiffBal1.Visible = false;
							lblDiffBal2.Visible = true;
							lblDiffBal2.Text = GenUtil.strNumericFormat(op_bal_d.ToString() );  
						}
						else
						{
							lblDiffBalDebit.Visible = false;
							lblDiffCreditBal.Visible = true;
							lblDiffBal1.Visible = true;
							lblDiffBal2.Visible = false;
							lblDiffBal1.Text = GenUtil.strNumericFormat(op_bal_d.ToString() );  
					
						}
					}
					else
					{
						lblDiffBalDebit.Visible = false;
						lblDiffCreditBal.Visible = false;
						lblDiffBal1.Visible = false;
						lblDiffBal2.Visible = false;
					}

					lblCapitalValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString());    
					lblSecuredLoansValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString());    
					lblunsecuredValue.Text  = GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString());    
					lblCurrentValue.Text   = GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString());    
					lblProvisionsValue.Text   = GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString());    
					lblFixedAssetsValue.Text =  GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString());    
					lblInvestmentValue.Text =  GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString());    

					// Add the Closing stock into Current Assets
					if(!Cl_Stock.Trim().Equals(""))
					{
						Closing_Stock = System.Convert.ToDouble(Cl_Stock);   
					}
					 
					string current_assets = GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString());     
					double C_A = 0;
					if(!current_assets.Trim().Equals(""))
					{
						C_A = System.Convert.ToDouble(current_assets);   
						C_A = C_A + Closing_Stock;
					}	
              
					//Calculate the related totals.
					lblCurrentAssetsValue.Text = GenUtil.strNumericFormat(C_A.ToString());  
					lblLoansAdvancesValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString());     
					lblMiscExpValue.Text = GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString());     
					double  total11 = 0;
					double capital1 = 0;
					double Res_Sur = 0;
					double Sec_Loan = 0;
					double Un_sec_ln  =0;
					double Curr_liab = 0;
				    double Provision = 0;
					capital1 = System.Convert.ToDouble(lblCapitalValue.Text);  
					Res_Sur = System.Convert.ToDouble(lblRes_Sur_Value .Text);
					Sec_Loan = System.Convert.ToDouble(lblSecuredLoansValue.Text);
					Un_sec_ln = System.Convert.ToDouble(lblunsecuredValue.Text);
					Curr_liab  = System.Convert.ToDouble(lblCurrentValue .Text);
					Provision   = System.Convert.ToDouble(lblProvisionsValue.Text);
					total11  = capital1+Res_Sur+Sec_Loan+Un_sec_ln+Curr_liab +Provision;
					if(lblDiffBal1.Visible )
						total11 = total11+op_bal_d ;
					lblTotal1Value.Text = GenUtil.strNumericFormat(total11.ToString());  

					double  total22 = 0;
					double Fix_Assets = 0;
					double investment = 0;
					double Curr_assets = 0;
					double Pro_loss  =0;
					double Misc = 0;
					double Loan_adv = 0;
					Fix_Assets = System.Convert.ToDouble(lblFixedAssetsValue.Text);  
					investment = System.Convert.ToDouble(lblInvestmentValue .Text);
					Curr_assets = System.Convert.ToDouble(lblCurrentAssetsValue.Text);
					Loan_adv = System.Convert.ToDouble(lblLoansAdvancesValue.Text);
					Pro_loss  = System.Convert.ToDouble(lblProfitLossValue.Text);
					Misc   = System.Convert.ToDouble(lblMiscExpValue.Text);
					total22  = Fix_Assets+investment +Curr_assets+Pro_loss +Misc+Loan_adv;
					if(lblDiffBal2.Visible )
						total22 = total22+op_bal_d ;
					lblTotal2Value.Text = GenUtil.strNumericFormat(total22.ToString());  
				}
				SqlDtr.Close();  
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:btnShow_Click.  EXCEPTION: "+ ex.Message+"  User: "+uid);
			}
		}

		/// <summary>
		/// Prepares the report file BalanceSheet.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(txtDateTo.Text))>0)
				{
					MessageBox.Show("Date From Should be less than Date To");
					return;
				}
			//	Table1.Visible = true; 
/*
                                 ===============
			         Balance Sheet	
                                ===============

From Date : mm/dd/yyyy
To   Date : mm/dd/yyyy
+--------------------------------------+--------------------------------------+
|      LIABILITIES                     |         ASSETS                       |
+--------------------------------------+--------------------------------------+
|Capital               123456789012.00 |Fixed Assets          123456789012.00 |
|Reserve & Surplus     12              |Investments           12              |
|Secured Loans         12              |Current Assets        12              | 
|Unsecured Loans       12              |Loan & Advances       12              | 
|Current Liabilities   12              |Profit & Loss A/C     12              |
|Provisions            12              |Misc. Expenditure     12              | 
|Difference in Balance 123456789012.00 |Difference in Balance 12              |
|                                      |                                      |
|---------------------                 |---------------------                 | 
|Total                 123456789012.00 |Total                 123456789012.00 | 
|---------------------                 |---------------------                 |
|                                      |                                      |
+--------------------------------------+--------------------------------------+
 
 */
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\BalanceSheet.txt";
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
				  string des="-----------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("===============",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("Balance Sheet",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("===============",des.Length));
				sw.WriteLine("");
				sw.WriteLine("From Date : "+txtDateFrom.Text );
				sw.WriteLine("To   Date : "+txtDateTo.Text );
				sw.WriteLine("+-------------------------------------+-------------------------------------+");
				sw.WriteLine("|           LIABILITIES               |               ASSETS                |");
				sw.WriteLine("+-------------------------------------+-------------------------------------+");
			
				SqlConnection con = null;
				SqlCommand cmd= null;
				SqlDataReader SqlDtr = null;
				string Op_Stock= "";
				double Opening_Stock = 0;
				string Cl_Stock= "";
				double Closing_Stock = 0;
				string Net_Profit = "";
				double Net_Pro = 0;
				double Net_Loss= 0;
				con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				con.Open ();
				cmd = new SqlCommand( "exec getProfitLoss '"+getFromDate()+"','"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'", con );
				SqlDtr = cmd.ExecuteReader();
				if(SqlDtr.Read())
				{
					Net_Profit = GenUtil.strNumericFormat(SqlDtr["Net_Profit"].ToString());
					if(!Net_Profit.Trim().Equals("") )
					{
						Net_Pro = System.Convert.ToDouble(Net_Profit);
						if(Net_Pro < 0 )
						{
							Net_Pro = (Net_Pro * -1);
							Net_Loss = Net_Pro;
							Net_Pro = 0;
							
						}
						else
						{
							Net_Loss = 0;
						}
					}
					Op_Stock = GenUtil.strNumericFormat(SqlDtr["Opening_Stock"].ToString());
					Cl_Stock = GenUtil.strNumericFormat(SqlDtr["Closing_Stock"].ToString());

				}
				SqlDtr.Close();
               
				string op_bal_dr = "";
				string op_bal_cr = "";
				double op_bal_d = 0;
				double op_bal_c = 0;
				cmd = new SqlCommand( "exec getBalanceSheet '"+getFromDate()+"','"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'", con );
				SqlDtr = cmd.ExecuteReader();
				if(SqlDtr.Read())
				{
					sw.WriteLine("|Capital              {0,15:F} |Fixed Assets         {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString()));    
					sw.WriteLine("|Reserve & Surplus    {0,15:F} |Investments          {1,15:F} |",GenUtil.strNumericFormat(Net_Pro.ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()));
					if(!Cl_Stock.Trim().Equals(""))
					{
						Closing_Stock = System.Convert.ToDouble(Cl_Stock);   
					}
					string current_assets = GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString());     
					double C_A = 0;
					if(!current_assets.Trim().Equals(""))
					{
						C_A = System.Convert.ToDouble(current_assets);   
						C_A = C_A + Closing_Stock;
					}	
								
					sw.WriteLine("|Secured Loans        {0,15:F} |Current Assets       {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString()),GenUtil.strNumericFormat(C_A.ToString()));
					sw.WriteLine("|Unsecured Loans      {0,15:F} |Loan & Advances      {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));
					sw.WriteLine("|Current Liabilities  {0,15:F} |Profit & Loss A/C    {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString()),GenUtil.strNumericFormat(Net_Loss.ToString()));
					sw.WriteLine("|Provisions           {0,15:F} |Misc. Expenditure    {1,15:F} |",GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString()),GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString())); 
	               op_bal_dr = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
					op_bal_cr = GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString());
					if(!Op_Stock.Trim().Equals(""))
					{
						Opening_Stock = System.Convert.ToDouble(Op_Stock);   
					}
					  
					if(!op_bal_dr.Trim().Equals(""))
					{
						op_bal_d = System.Convert.ToDouble(op_bal_dr);   
						
						op_bal_d = op_bal_d + Opening_Stock;
					}
					
					
					if(!op_bal_cr.Trim().Equals(""))
					{
						op_bal_c = System.Convert.ToDouble(op_bal_cr);  
 
					}
					
                    bool d1 = false;
					bool d2 = false;

					if(op_bal_d != op_bal_c)
					{
						op_bal_d = op_bal_d - op_bal_c;
						if(op_bal_d < 0)
						{
							op_bal_d = (op_bal_d * -1);
							sw.WriteLine("|                                     |Diff. in Op. Balance {0,15:F} |",GenUtil.strNumericFormat(op_bal_d.ToString())); 
							d2 = true;
						}
						else
						{
							sw.WriteLine("|Diff. in Op. Balance {0,15:F} |                                     |",GenUtil.strNumericFormat(op_bal_d.ToString() )); 
							d1 = true;
									
						}

					}
					else
					{
                            sw.WriteLine("|                                     |                                     |");
					}
                            sw.WriteLine("|                                     |                                     |");
					
					double  total11 = 0;
					double capital1 = 0;
					double Res_Sur = 0;
					double Sec_Loan = 0;
					double Un_sec_ln  =0;
					double Curr_liab = 0;
					double Provision = 0;
					capital1 = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString()));  
					Res_Sur = System.Convert.ToDouble(GenUtil.strNumericFormat(Net_Pro.ToString()));
					Sec_Loan = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString()));
					Un_sec_ln = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString()));
					Curr_liab  = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString()));
					Provision   = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString()));
					total11  = capital1+Res_Sur+Sec_Loan+Un_sec_ln+Curr_liab +Provision;
					if(d1 == true )
						total11 = total11+op_bal_d ;
					

					double  total22 = 0;
					double Fix_Assets = 0;
					double investment = 0;
					double Curr_assets = 0;
					double Pro_loss  =0;
					double Misc = 0;
					double Loan_adv = 0;
					Fix_Assets = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString()));  
					investment = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()));
					Curr_assets = System.Convert.ToDouble(GenUtil.strNumericFormat(C_A.ToString()));
					Loan_adv = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));
					Pro_loss  = System.Convert.ToDouble(GenUtil.strNumericFormat(Net_Loss.ToString()));
					Misc   = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString()));
					total22  = Fix_Assets+investment +Curr_assets+Pro_loss +Misc+Loan_adv;
					if(d2 == true )
						total22 = total22+op_bal_d ;
				
					sw.WriteLine("|--------------------                 |--------------------                 |"); 
					sw.WriteLine("|Total                {0,15:F} |Total                {1,15:F} |",GenUtil.strNumericFormat(total11.ToString()) ,GenUtil.strNumericFormat(total22.ToString()));
					sw.WriteLine("|--------------------                 |--------------------                 |"); 
					sw.WriteLine("|                                     |                                     |");
					sw.WriteLine("+-------------------------------------+-------------------------------------+");


				}
				sw.Close(); 

				SqlDtr.Close(); 
				Print(); 
			}
			catch(Exception ex)
			{
                  CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:BtnPrint_Click.  EXCEPTION: "+ ex.Message+"  User: "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\BalanceSheet.xls";
			StreamWriter sw = new StreamWriter(path);
			SqlConnection con = null;
			SqlCommand cmd= null;
			SqlDataReader SqlDtr = null;
			string Op_Stock= "";
			double Opening_Stock = 0;
			string Cl_Stock= "";
			double Closing_Stock = 0;
			string Net_Profit = "";
			double Net_Pro = 0;
			double Net_Loss= 0;
			con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			con.Open ();
			cmd = new SqlCommand( "exec getProfitLoss '"+GenUtil.str2DDMMYYYY(getFromDate())+"','"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"'", con );
			SqlDtr = cmd.ExecuteReader();
			
			
			if(SqlDtr.Read())
			{
				sw.WriteLine("From Date\t"+txtDateFrom.Text);
				sw.WriteLine("To Date\t"+txtDateTo.Text);
				sw.WriteLine();
				sw.WriteLine("LIABILITIES\t\tASSETS");
				Net_Profit = GenUtil.strNumericFormat(SqlDtr["Net_Profit"].ToString());
				if(!Net_Profit.Trim().Equals("") )
				{
					Net_Pro = System.Convert.ToDouble(Net_Profit);
					if(Net_Pro < 0 )
					{
						Net_Pro = (Net_Pro * -1);
						Net_Loss = Net_Pro;
						Net_Pro = 0;
							
					}
					else
					{
						Net_Loss = 0;
					}
				}
				Op_Stock = GenUtil.strNumericFormat(SqlDtr["Opening_Stock"].ToString());
				Cl_Stock = GenUtil.strNumericFormat(SqlDtr["Closing_Stock"].ToString());

			}
			SqlDtr.Close();
               
			string op_bal_dr = "";
			string op_bal_cr = "";
			double op_bal_d = 0;
			double op_bal_c = 0;
			cmd = new SqlCommand( "exec getBalanceSheet '"+ GenUtil.str2DDMMYYYY(getFromDate())+"','"+GenUtil.str2DDMMYYYY(txtDateTo.Text)+"'", con );
			SqlDtr = cmd.ExecuteReader();
			if(SqlDtr.Read())
			{
				sw.WriteLine("Capital\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString())+"\t"+"Fixed Assets\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString()));    
				sw.WriteLine("Reserve & Surplus\t"+GenUtil.strNumericFormat(Net_Pro.ToString())+"\t"+"Investments\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()));
				if(!Cl_Stock.Trim().Equals(""))
				{
					Closing_Stock = System.Convert.ToDouble(Cl_Stock);   
				}
				string current_assets = GenUtil.strNumericFormat(SqlDtr.GetValue(9).ToString());     
				double C_A = 0;
				if(!current_assets.Trim().Equals(""))
				{
					C_A = System.Convert.ToDouble(current_assets);   
					C_A = C_A + Closing_Stock;
				}	
								
				sw.WriteLine("Secured Loans\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString())+"\t"+"Current Assets\t"+GenUtil.strNumericFormat(C_A.ToString()));
				sw.WriteLine("Unsecured Loans\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString())+"\t"+"Loan & Advances\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));
				sw.WriteLine("Current Liabilities\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString())+"\t"+"Profit & Loss A/C\t"+GenUtil.strNumericFormat(Net_Loss.ToString()));
				sw.WriteLine("Provisions\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString())+"\tMisc. Expenditure\t"+GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString())); 
				op_bal_dr = GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
				op_bal_cr = GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString());
				if(!Op_Stock.Trim().Equals(""))
				{
					Opening_Stock = System.Convert.ToDouble(Op_Stock);   
				}
					  
				if(!op_bal_dr.Trim().Equals(""))
				{
					op_bal_d = System.Convert.ToDouble(op_bal_dr);   
						
					op_bal_d = op_bal_d + Opening_Stock;
				}
					
					
				if(!op_bal_cr.Trim().Equals(""))
				{
					op_bal_c = System.Convert.ToDouble(op_bal_cr);  
 
				}
					
				bool d1 = false;
				bool d2 = false;

				if(op_bal_d != op_bal_c)
				{
					op_bal_d = op_bal_d - op_bal_c;
					if(op_bal_d < 0)
					{
						op_bal_d = (op_bal_d * -1);
						sw.WriteLine("\t\tDiff. in Op. Balance\t"+GenUtil.strNumericFormat(op_bal_d.ToString())); 
						d2 = true;
					}
					else
					{
						sw.WriteLine("Diff. in Op. Balance\t"+GenUtil.strNumericFormat(op_bal_d.ToString() )); 
						d1 = true;
									
					}

				}
				else
				{
					sw.WriteLine();
				}
				sw.WriteLine();
					
				double  total11 = 0;
				double capital1 = 0;
				double Res_Sur = 0;
				double Sec_Loan = 0;
				double Un_sec_ln  =0;
				double Curr_liab = 0;
				double Provision = 0;
				capital1 = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString()));  
				Res_Sur = System.Convert.ToDouble(GenUtil.strNumericFormat(Net_Pro.ToString()));
				Sec_Loan = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString()));
				Un_sec_ln = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString()));
				Curr_liab  = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(5).ToString()));
				Provision   = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(6).ToString()));
				total11  = capital1+Res_Sur+Sec_Loan+Un_sec_ln+Curr_liab +Provision;
				if(d1 == true )
					total11 = total11+op_bal_d ;
					

				double  total22 = 0;
				double Fix_Assets = 0;
				double investment = 0;
				double Curr_assets = 0;
				double Pro_loss  =0;
				double Misc = 0;
				double Loan_adv = 0;
				Fix_Assets = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(7).ToString()));  
				investment = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(8).ToString()));
				Curr_assets = System.Convert.ToDouble(GenUtil.strNumericFormat(C_A.ToString()));
				Loan_adv = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(10).ToString()));
				Pro_loss  = System.Convert.ToDouble(GenUtil.strNumericFormat(Net_Loss.ToString()));
				Misc   = System.Convert.ToDouble(GenUtil.strNumericFormat(SqlDtr.GetValue(11).ToString()));
				total22  = Fix_Assets+investment +Curr_assets+Pro_loss +Misc+Loan_adv;
				if(d2 == true )
					total22 = total22+op_bal_d ;
				sw.WriteLine("Total\t"+GenUtil.strNumericFormat(total11.ToString())+"\tTotal\t"+GenUtil.strNumericFormat(total22.ToString()));
			}
			SqlDtr.Close(); 
			sw.Close(); 
		}
		
		/// <summary>
		/// Sends the file BalanceSheet.txt to print server to print.
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
					CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
					sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\BalanceSheet.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					//CreateLogFiles.ErrorLog("Form:Vehiclereport.aspx,Method:print"+ "  Daily sales record  Printed   userid  "+uid);
					CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BalanceSheet.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file BalanceSheet.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				//if(GridReport.Visible==true)
				//{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Leave Report Convert Into Excel Format, userid  "+uid);
				//}
				//else
				//{
				//	MessageBox.Show("Please Click the View Button First");
				//	return;
				//}
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:LeaveReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Leave Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}
