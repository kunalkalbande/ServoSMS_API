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
	/// Summary description for StockLedgerReport.
	/// </summary>
	public partial class StockLedgerReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid = "";
		static string FromDate="",ToDate="";
		public static int InQtyNos=0,InQtyLtr=0,OutQtyNos=0,OutQtyLtr=0,closing_bal_nos=0,closing_bal_ltr=0;
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
				if(!Page.IsPostBack)
				{
					InQtyNos=0;InQtyLtr=0;OutQtyNos=0;OutQtyLtr=0;closing_bal_nos=0;closing_bal_ltr=0;
					Stock_Ledger.Visible=false;
					getProducts();
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					txtDateTo.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="45";
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
				CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:Page_Load"+ " EXCEPTION "  +ex.Message+"  userid  "+uid);  
			}
		}

		/// <summary>
		/// Fetches the products and pack type from products table and fills the Product Name combo.
		/// </summary>
		public void getProducts()
		{
			SqlDataReader SqlDtr = null;
			//drpProductName.Items.Clear();
			//drpProductName.Items.Add("Select");
               
			dbobj.SelectQuery("Select case when pack_type != '' then Prod_Name+':'+Pack_Type else Prod_Name  end from products order by Prod_Name",ref SqlDtr);
			if(SqlDtr.HasRows)
			{
				texthiddenprod.Value="Select,";
				while(SqlDtr.Read())
				{
					//drpProductName.Items.Add(SqlDtr.GetValue(0).ToString());    
					texthiddenprod.Value+=SqlDtr.GetValue(0).ToString()+",";
				}
			}
			SqlDtr.Close();
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
		/// Check the validity of filled form.
		/// </summary>
		public bool checkValidity()
		{
			string ErrorMessage = "";
			bool flag = true;
			//if(drpProductName.SelectedIndex  == 0)
			if(drpProductName.Value  == "Select")
			{
				ErrorMessage = ErrorMessage + " - Please Select Product\n";
				flag = false;
			}
			//			if(drpTransType.SelectedIndex  == 0)
			//			{
			//				ErrorMessage = ErrorMessage + " - Please Select Transaction Type\n";
			//				flag = false;
			//			}
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
			
			if(flag == false)
			{
				MessageBox.Show(ErrorMessage);
				return false;
			}


            var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
            var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()));
            if (DateTime.Compare(dt1, dt2) > 0)
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
		/// Return date in MM/DD/YYYY format
		/// </summary>
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
		/// This is used to make sorting the datagrid on clicking of datagridheader.
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
				CreateLogFiles.ErrorLog("Form:StockledgerReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to  binding the datagrid .
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//************
			string prod_name ="";
			string pack1 = "";
			string pack2 = "";
			string trans_type ="";
			string Cat = "";
			prod_name = drpProductName.Value.ToString().Trim();

			// check if product contains the package then split it into pack1 and pack2 . Incase of fuel category and Loose Oil pack the pack1 and pack2 will be 0.
			if(prod_name.LastIndexOf(":") > -1)
			{
				string[] strArr = prod_name.Split(new char[] {':'},prod_name.Length);
				prod_name = strArr[0].Trim();
				if(strArr[1].Trim().IndexOf("Loose") > -1)
				{
					pack1 = "0";
					pack2 = "0";
					Cat = "Loose";
				}
				else
				{
					string[] strPack = strArr[1].Trim().Split(new char[] {'X'} ,strArr[1].Length);
					pack1 = strPack[0].Trim();
					pack2 = strPack[1].Trim();
					Cat = "Others";
				}
			}
			else
			{
				pack1 = "0";
				pack2 = "0";
				Cat = "Fuel";
			}
			trans_type = drpTransType.SelectedItem.Value.ToString().Trim();
     
			// Response.Write(prod_name+"#"+pack1+"#"+pack2+"#"+trans_type); 
			//exec sp_StockLedger 'Servo 4t',2,4,'Sales','06/12/2006','06/13/2006'
			object obj = null;
			// Calls the procedure sp_StockLedger and creates the temporary table Stock_Ledger.
			dbobj.ExecProc(OprType.Insert,"sp_stockLedger",ref obj,"@Prod_Name",prod_name,"@Pack11",pack1,"@Pack22" ,pack2,"@Trans_Type",trans_type,"@fromdate",GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim()),"@Todate",GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()),"@Cat",Cat);
			//SqlDataReader SqlDtr = null;

			//dbobj.SelectQuery("Select * from Stock_Ledger order by trans_date ",ref SqlDtr);
			//if(SqlDtr.HasRows)
			//{
			//	Stock_Ledger.Visible = true;
			//	Stock_Ledger.DataSource = SqlDtr;
			//	Stock_Ledger.DataBind();
			//}
			//else
			//{
			//		Stock_Ledger.Visible = false;
			//		MessageBox.Show("Data not available" );
			//		}
			//		SqlDtr.Close();
			//*********
			string  sql="Select * from Stock_Ledger order by trans_date ";
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"Stock_Ledger");
			DataTable dtcustomer=ds.Tables["Stock_Ledger"]; 
			//da.Fill(ds,"beat_master","machanic_entry","customermechanicentry ","customer");
			//DataTable dtcustomer=ds.Tables["beat_master","machanic_entry","customermechanicentry ","customer"]; 
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			Stock_Ledger.DataSource=dv;
			if(dv.Count!=0)
			{
				Stock_Ledger.DataBind();
				Stock_Ledger.Visible=true;
			}
			else
			{
				Stock_Ledger.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}

		/// <summary>
		/// This is used to show the report.
		/// </summary>
		protected void cmdrpt_Click(object sender, System.EventArgs e)
		{	
			try
			{
				if(!checkValidity())
				{
					return;
				}
				/************************
				string prod_name ="";
				string pack1 = "";
				string pack2 = "";
				string trans_type ="";
				string Cat = "";
				prod_name = drpProductName.SelectedItem.Value.ToString().Trim();

				// check if product contains the package then split it into pack1 and pack2 . Incase of fuel category and Loose Oil pack the pack1 and pack2 will be 0.
				if(prod_name.LastIndexOf(":") > -1)
				{
					string[] strArr = prod_name.Split(new char[] {':'},prod_name.Length);
					prod_name = strArr[0].Trim();
					if(strArr[1].Trim().IndexOf("Loose") > -1)
					{
						pack1 = "0";
						pack2 = "0";
						Cat = "Loose";
					}
					else
					{
						string[] strPack = strArr[1].Trim().Split(new char[] {'X'} ,strArr[1].Length);
						pack1 = strPack[0].Trim();
						pack2 = strPack[1].Trim();
						Cat = "Others";
					}
                    
				}
				else
				{
					pack1 = "0";
					pack2 = "0";
					Cat = "Fuel";

				}
				trans_type = drpTransType.SelectedItem.Value.ToString().Trim();
     
			  // Response.Write(prod_name+"#"+pack1+"#"+pack2+"#"+trans_type); 
				//exec sp_StockLedger 'Servo 4t',2,4,'Sales','06/12/2006','06/13/2006'
			   object obj = null;
				// Calls the procedure sp_StockLedger and creates the temporary table Stock_Ledger.
			   dbobj.ExecProc(OprType.Insert,"sp_stockLedger",ref obj,"@Prod_Name",prod_name,"@Pack11",pack1,"@Pack22" ,pack2,"@Trans_Type",trans_type,"@fromdate",GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim()),"@Todate",GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()),"@Cat",Cat);
			   SqlDataReader SqlDtr = null;

			   dbobj.SelectQuery("Select * from Stock_Ledger order by trans_date ",ref SqlDtr);
				if(SqlDtr.HasRows)
				{
					Stock_Ledger.Visible = true;
					Stock_Ledger.DataSource = SqlDtr;
					Stock_Ledger.DataBind();
				}
				else
				{
					Stock_Ledger.Visible = false;
					MessageBox.Show("Data not available" );
				}
				SqlDtr.Close(); 
				*************bhal com end***************************/
				strorderby="Trans_Date ASC";
				Session["Column"]="Trans_Date";
				Session["order"]="ASC";
				Bindthedata();

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:cmdrpt_Click"+ " EXCEPTION "  +ex.Message+"  userid  "+uid); 
			}
		}

		/// <summary>
		/// This is calls from the .aspx page to check , if the value is zero then return &nbsp to display the space in grid.;
		/// </summary>
		public string checkValue(string str)
		{
			if(str.Equals("0")|| str == null)
			{
				return "&nbsp;";
			}
			return str;
		}

		/// <summary>
		/// This is calls from the btnPrint_Click page to check , if the value is zero then return "" to display the blank value in report file.;
		/// </summary>
		public string checkValue1(string str)
		{
			if(str.Equals("0")|| str == null)
			{
				return "";
			}
			return str;
		}

		/// <summary>
		/// Its fires the sp_StockLedger procedure and fetch the values from Stock_Ledger Report and writes the return values into StockLedgerReport.txt file.
		/// </summary>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{

			if(!checkValidity())
			{
				return;
			}
			/*
								   =================================================
								   Stock Ledger Report From mm/dd/yyyy To mm/dd/yyyy
								   =================================================

			Product Name     : Petrol(MS)
			Transaction Type : Sales
			+----------------------+-----+----------+------------+------------+------------+ 
			|                      |     |          |     IN     |    OUT     |CLOSING BAL.|
			|    Transaction       |Trans|   Date   |----+-------|----+-------|----+-------|
			|       Type           |  ID |          |Qty.|Qty. in|Qty.|Qty. in|Bal.|Bal. in|
			|                      |     |          |Nos |  Ltr. |Nos |  Ltr. |Nos |  Ltr. |
			+----------------------+-----+----------+----+-------+----+-------+----+-------+
			 Opening Balance        1001  mm/dd/yyyy 1234 1234567 1234 1234567 1234 1234567
			 Closing Balance       
			 Purchase Invoice      
			 Stock Adjustment(OUT) 
			 123456789012345678|
			+----------------------+-----+----------+----+-------+----+-------+----+-------+
			*/

			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\StockLedgerReport.txt";
				StreamWriter sw = new StreamWriter(path);
				string info = "";
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
				sw.WriteLine(GenUtil.GetCenterAddr("=================================================",des.Length));  
				sw.WriteLine(GenUtil.GetCenterAddr("Stock Ledger Report From "+txtDateFrom.Text.Trim()+" To "+txtDateTo.Text.Trim(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("=================================================",des.Length));
				//sw.WriteLine("");
				sw.WriteLine("Product Name     : "+drpProductName.Value);
				//sw.WriteLine("Transaction Type : "+drpTransType.SelectedItem.Value);
				sw.WriteLine("+------------------------------+----------------------+-----+----------+------------+------------+------------+  ");
				sw.WriteLine("|                              |                      |     |          |     IN     |    OUT     |CLOSING BAL.|");
				sw.WriteLine("|    Shipping Party            |    Transaction       |Trans|   Date   |----+-------|----+-------|----+-------|");
				sw.WriteLine("|                              |                      |  ID |          |Qty.|Qty. in|Qty.|Qty. in|Bal.|Bal. in|");
				sw.WriteLine("|                              |                      |     |          |Nos |  Ltr. |Nos |  Ltr. |Nos |  Ltr. |"); 
				sw.WriteLine("+------------------------------+----------------------+-----+----------+----+-------+----+-------+----+-------+");
				//             123456789012345678901234567890 1234567890123456789012 1001  mm/dd/yyyy 1234 1234567 1234 1234567 1234 1234567
				info = " {0,-30:S} {1,-22:S} {2,-5:S} {3,-10:F} {4,4:F} {5,7:F} {6,4:F} {7,7:F} {8,4:F} {9,7:F}";

				string prod_name ="";
				string pack1 = "";
				string pack2 = "";
				string trans_type ="";
				string Cat = "";
				prod_name = drpProductName.Value.ToString().Trim();
				if(prod_name.LastIndexOf(":") > -1)
				{
					string[] strArr = prod_name.Split(new char[] {':'},prod_name.Length);
					prod_name = strArr[0].Trim();
					if(strArr[1].Trim().IndexOf("Loose") > -1)
					{
						pack1 = "0";
						pack2 = "0";
						Cat = "Loose";
					}
					else
					{
						string[] strPack = strArr[1].Trim().Split(new char[] {'X'} ,strArr[1].Length);
						pack1 = strPack[0].Trim();
						pack2 = strPack[1].Trim();
						Cat = "Others";
					}
                    
				}
				else
				{
					pack1 = "0";
					pack2 = "0";
					Cat = "Fuel";

				}
				int f = 0;
				trans_type = drpTransType.SelectedItem.Value.ToString().Trim();
				object obj = null;
				dbobj.ExecProc(OprType.Insert,"sp_stockLedger",ref obj,"@Prod_Name",prod_name,"@Pack11",pack1,"@Pack22" ,pack2,"@Trans_Type",trans_type,"@fromdate",GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim()),"@Todate",GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()),"@Cat",Cat );
				SqlDataReader SqlDtr = null;
				//dbobj.SelectQuery("Select * from Stock_Ledger order by trans_date ",ref SqlDtr);
				dbobj.SelectQuery("Select * from Stock_Ledger order by "+Cache["strorderby"]+"",ref SqlDtr);
				//sql=sql+" order by "+""+Cache["strorderby"]+"";
				SqlDataReader rdr =null;
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						string trans_no = "";
						string Party = "";
						if(SqlDtr.GetValue(0).ToString().StartsWith("Sales Invoice"))
						{
							trans_no = SqlDtr.GetValue(1).ToString();
							//*********************
							//Coment by vikas 23.10.09 dbobj.SelectQuery("select Cust_Name from Customer c,Sales_Master sm where c.Cust_ID=sm.Cust_ID and Invoice_No='"+trans_no+"'",ref rdr);
							dbobj.SelectQuery("select Cust_Name+':'+cast(c.City as varchar) Cust_Name from Customer c,Sales_Master sm where c.Cust_ID=sm.Cust_ID and Invoice_No='"+trans_no+"'",ref rdr);
							if(rdr.Read())
							{
								Party=rdr["Cust_Name"].ToString();
							}
							rdr.Close();
							//*********************
							if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
								trans_no=trans_no.Substring(4);
							else
								trans_no=trans_no.Substring(3);
						}
							//*********************
						else if(SqlDtr.GetValue(0).ToString().StartsWith("Purchase Invoice"))
						{
							trans_no = SqlDtr.GetValue(1).ToString();
							dbobj.SelectQuery("select supp_name from purchase_master pm,supplier s where supp_id=vendor_id and Invoice_No='"+trans_no+"'",ref rdr);
							if(rdr.Read())
							{
								Party=rdr["Supp_Name"].ToString();
							}
							rdr.Close();
						}
							//*********************
						else
							trans_no=SqlDtr.GetValue(1).ToString();
						
						sw.WriteLine(info,GenUtil.TrimLength(Party,30),SqlDtr.GetValue(0).ToString(),
							//SqlDtr.GetValue(1).ToString(),
							trans_no,
							GenUtil.str2MMDDYYYY(trimDate(SqlDtr.GetValue(2).ToString())),
							checkValue1(SqlDtr.GetValue(3).ToString()),
							checkValue1(SqlDtr.GetValue(4).ToString()),
							checkValue1(SqlDtr.GetValue(5).ToString()),
							checkValue1(SqlDtr.GetValue(6).ToString()),
							checkValue1(SqlDtr.GetValue(7).ToString()),
							checkValue1(SqlDtr.GetValue(8).ToString()));
					} 
				}
				else
				{
					Stock_Ledger.Visible = false;
					f = 1;
					sw.Close(); 
					MessageBox.Show("Data not available" );
					return;
				}
				SqlDtr.Close(); 
				sw.WriteLine("+------------------------------+----------------------+-----+----------+----+-------+----+-------+----+-------+");
				sw.WriteLine(info,"  Total","","","",InQtyNos.ToString(),InQtyLtr.ToString(),OutQtyNos.ToString(),OutQtyLtr.ToString(),closing_bal_nos.ToString(),closing_bal_ltr.ToString());
				sw.WriteLine("+------------------------------+----------------------+-----+----------+----+-------+----+-------+----+-------+");
				SqlDtr.Close();
				sw.Close(); 
				if(f == 0)
					Print(); 
				else
					return; 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:btnPrint_Click"+ " EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			//InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\StockLedgerReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string prod_name ="";
			string pack1 = "";
			string pack2 = "";
			string trans_type ="";
			string Cat = "";
			//prod_name = drpProductName.SelectedItem.Value.ToString().Trim();
			prod_name = drpProductName.Value.ToString().Trim();
			if(prod_name.LastIndexOf(":") > -1)
			{
				string[] strArr = prod_name.Split(new char[] {':'},prod_name.Length);
				prod_name = strArr[0].Trim();
				if(strArr[1].Trim().IndexOf("Loose") > -1)
				{
					pack1 = "0";
					pack2 = "0";
					Cat = "Loose";
				}
				else
				{
					string[] strPack = strArr[1].Trim().Split(new char[] {'X'} ,strArr[1].Length);
					pack1 = strPack[0].Trim();
					pack2 = strPack[1].Trim();
					Cat = "Others";
				}
                    
			}
			else
			{
				pack1 = "0";
				pack2 = "0";
				Cat = "Fuel";

			}
			//int f = 0;
			//trans_type = drpTransType.SelectedItem.Value.ToString().Trim();
			object obj = null;
			dbobj.ExecProc(OprType.Insert,"sp_stockLedger",ref obj,"@Prod_Name",prod_name,"@Pack11",pack1,"@Pack22" ,pack2,"@Trans_Type",trans_type,"@fromdate",GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim()),"@Todate",GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()),"@Cat",Cat );
			dbobj.SelectQuery("Select * from Stock_Ledger order by "+Cache["strorderby"]+"",ref SqlDtr);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+txtDateTo.Text);
			//sw.WriteLine("Product Name\t"+drpProductName.SelectedItem.Text);
			sw.WriteLine("Product Name\t"+drpProductName.Value);
			sw.WriteLine("Transaction Type\t"+drpTransType.SelectedItem.Text);
			sw.WriteLine();
			sw.WriteLine("Shipping Party\tTransaction Type\tTransaction ID\tDate\tQty Nos(IN)\tQty Ltr(IN)\tQty Nos(OUT)\tQty Ltr(OUT)\tClosing Balance(Nos)\tClosing Balance(Ltr)");
			SqlDataReader rdr = null;
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					string trans_no = "";
					string Party = "";
					if(SqlDtr.GetValue(0).ToString().StartsWith("Sales Invoice"))
					{
						trans_no = SqlDtr.GetValue(1).ToString();
						//*********************
						dbobj.SelectQuery("select Cust_Name from Customer c,Sales_Master sm where c.Cust_ID=sm.Cust_ID and Invoice_No='"+trans_no+"'",ref rdr);
						if(rdr.Read())
						{
							Party = rdr["Cust_Name"].ToString();
						}
						rdr.Close();
						//*********************
						if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
							trans_no=trans_no.Substring(4);
						else
							trans_no=trans_no.Substring(3);
					}
						//*********************
					else if(SqlDtr.GetValue(0).ToString().StartsWith("Purchase Invoice"))
					{
						trans_no = SqlDtr.GetValue(1).ToString();
						dbobj.SelectQuery("select supp_name from purchase_master pm,supplier s where supp_id=vendor_id and Invoice_No='"+trans_no+"'",ref rdr);
						if(rdr.Read())
						{
							Party = rdr["Supp_Name"].ToString();
						}
						rdr.Close();
					}
						//*********************
					else
						trans_no=SqlDtr.GetValue(1).ToString();
					sw.WriteLine(Party+"\t"+SqlDtr.GetValue(0).ToString()+"\t"+
						//SqlDtr.GetValue(1).ToString()+"\t"+
						trans_no+"\t"+
						GenUtil.str2MMDDYYYY(trimDate(SqlDtr.GetValue(2).ToString()))+"\t"+
						checkValue1(SqlDtr.GetValue(3).ToString())+"\t"+
						checkValue1(SqlDtr.GetValue(4).ToString())+"\t"+
						checkValue1(SqlDtr.GetValue(5).ToString())+"\t"+
						checkValue1(SqlDtr.GetValue(6).ToString())+"\t"+
						checkValue1(SqlDtr.GetValue(7).ToString())+"\t"+
						checkValue1(SqlDtr.GetValue(8).ToString()));
				}
				sw.WriteLine("Total\t\t\t\t"+InQtyNos.ToString()+"\t"+InQtyLtr.ToString()+"\t"+OutQtyNos.ToString()+"\t"+OutQtyLtr.ToString()+"\t"+closing_bal_nos.ToString()+"\t"+closing_bal_ltr.ToString());
			}
			else
			{
				Stock_Ledger.Visible = false;
				sw.Close(); 
				MessageBox.Show("Data not available" );
				return;
			}
			SqlDtr.Close(); 
			sw.Close();
		}

		/// <summary>
		/// This is used to discard the date after the space.
		/// </summary>
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
		/// Contacts the server and sends the StockLedgerReport.txt file name to print.
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
					//CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\StockLedgerReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());

					 
					CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file StockLedgerReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Stock_Ledger.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    StockLedgerReport Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    StockLedgerReport Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
		
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemDataBound(object sender,DataGridItemEventArgs e)
		{
			try
			{
				// If datagrid item is a bound column other than header and footer
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					//string strtype = e.Item.Cells[0].Text;
					string strtype = e.Item.Cells[1].Text;
					if(strtype.StartsWith("Sales Invoice"))
					{
						//string trans_no = e.Item.Cells[1].Text;
						string trans_no = e.Item.Cells[2].Text;
						//*********************
						SqlDataReader rdr = null;
						//Coment by vikas 23.10.09 dbobj.SelectQuery("select Cust_Name from Customer c,Sales_Master sm where c.Cust_ID=sm.Cust_ID and Invoice_No='"+trans_no+"'",ref rdr);
						dbobj.SelectQuery("select Cust_Name+':'+cast(c.city as varchar) Cust_Name from Customer c,Sales_Master sm where c.Cust_ID=sm.Cust_ID and Invoice_No='"+trans_no+"'",ref rdr);
						if(rdr.Read())
						{
							e.Item.Cells[0].Text=rdr["Cust_Name"].ToString();
						}
						else
						{
							e.Item.Cells[0].Text="";
						}
						rdr.Close();
						//*********************
						if(System.Convert.ToString(int.Parse(FromDate).ToString()+ToDate).Length>3)
							trans_no=trans_no.Substring(4);
						else
							trans_no=trans_no.Substring(3);
						//e.Item.Cells[1].Text=trans_no;
						e.Item.Cells[2].Text=trans_no;
					}
					//*********************
					if(strtype.StartsWith("Purchase Invoice"))
					{
						string trans_no = e.Item.Cells[2].Text;
						SqlDataReader rdr = null;
						dbobj.SelectQuery("select supp_name from purchase_master pm,supplier s where supp_id=vendor_id and Invoice_No='"+trans_no+"'",ref rdr);
						if(rdr.Read())
						{
							e.Item.Cells[0].Text=rdr["Supp_Name"].ToString();
						}
						else
						{
							e.Item.Cells[0].Text="";
						}
						rdr.Close();
					}
					//*********************
				}
				if((e.Item.ItemType == ListItemType.Footer))
				{
					dbobj.ExecuteScalar("select sum(in_qty_nos) from stock_ledger",ref InQtyNos);
					dbobj.ExecuteScalar("select sum(in_qty_ltr) from stock_ledger",ref InQtyLtr);
					dbobj.ExecuteScalar("select sum(out_qty_nos) from stock_ledger",ref OutQtyNos);
					dbobj.ExecuteScalar("select sum(out_qty_ltr) from stock_ledger",ref OutQtyLtr);
					dbobj.ExecuteScalar("select closing_bal_nos from stock_ledger where trans_type='closing balance'",ref closing_bal_nos);
					dbobj.ExecuteScalar("select closing_bal_ltr from stock_ledger where trans_type='closing balance'",ref closing_bal_ltr);
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:StockLedgerReport.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		/// <summary>
		/// This method return the year in given date.
		/// </summary>
		public string GetYear(string dt)
		{
			if(dt!="")
			{
				string[] year=dt.IndexOf("/")>0? dt.Split(new char[] {'/'},dt.Length): dt.Split(new char[] { '-' }, dt.Length);
				string yr=year[2].Substring(2);	
				return(yr);
			}
			else
				return "";
		}
	}
}