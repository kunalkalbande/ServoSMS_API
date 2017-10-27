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
using RMG;
using Servosms.Sysitem.Classes;
using System.Data.SqlClient;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for CustomerwiseSalesReport.
	/// </summary>
	public partial class CustomerwiseSalesReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid="";
		static string FromDate="",ToDate="";


		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
				// Put user code to initialize the page here
				if(!IsPostBack)
				{
					GridReport.Visible=false;
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					Textbox1.Text=GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString());
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="23";
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
					# region Fill dropType
					//					SqlCommand cmd;
					//					SqlConnection con;
					//					con=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					//					con.Open ();
					//					SqlDataReader SqlDtr1; 
					//					cmd=new SqlCommand("select * from CustomerType order by CustomerTypeName",con);
					//					SqlDtr1=cmd.ExecuteReader();
					//					DropType.Items.Clear();
					//					DropType.Items.Add("All");
					//					while(SqlDtr1.Read())
					//					{
					//						DropType.Items.Add(SqlDtr1.GetValue(1).ToString());
					//					}
					//					con.Close();
					//					SqlDtr1.Close();
					//					con.Open();
					//					cmd=new SqlCommand("select distinct Category from Products order by Category",con);
					//					SqlDtr1=cmd.ExecuteReader();
					//					DropCategory.Items.Clear();
					//					DropCategory.Items.Add("All");
					//					while(SqlDtr1.Read())
					//					{
					//						DropCategory.Items.Add(SqlDtr1.GetValue(0).ToString());
					//					}
					//					con.Close();
					//					SqlDtr1.Close();
					//					con.Open();
					//					cmd=new SqlCommand("select distinct Emp_Name from Employee where Designation='Servo Sales Representative' order by Emp_Name",con);
					//					SqlDtr1=cmd.ExecuteReader();
					//					DropSSR.Items.Clear();
					//					DropSSR.Items.Add("All");
					//					while(SqlDtr1.Read())
					//					{
					//						DropSSR.Items.Add(SqlDtr1.GetValue(0).ToString());
					//					}
					//					con.Close();
					//					SqlDtr1.Close();
					#endregion
					#region Fetch the From and To Date From OrganisationDatail
					SqlDataReader rdr=null;
					dbobj.SelectQuery("select * from organisation",ref rdr);
					if(rdr.Read())
					{
						FromDate=GetYear(GenUtil.trimDate(rdr["Acc_date_from"].ToString()));
						if(FromDate!="")
							FromDate=System.Convert.ToString(int.Parse(FromDate));
						ToDate=GetYear(GenUtil.trimDate(rdr["Acc_date_To"].ToString()));
					}
					else
					{
						MessageBox.Show("Please Fill The Organization Form First");
						return;
					}
					#endregion
					GetMultiValue();
				}

                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                Textbox1.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerwiseSalesReport.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
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

		/// <summary>
		/// Returns the date in MM/DD/YYYY format.
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
		/// This is used to sorting the datagrid on click of datagridheader
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
				CreateLogFiles.ErrorLog("Form:CustomerwiseSalesReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This is used to bind the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			string cust_type="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
            //string sqlstr="select * from vw_PriceList order by Prod_id";
            //string sql="select * from vw_CustWiseSales where rate<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(Request.Form["txtDateFrom"].ToString()).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
          //string sql = "select * from vw_CustWiseSales where cast(floor(cast(invoice_date as float)) as datetime) >= '" + ToMMddYYYY(txtDateFrom.Text).ToShortDateString() + "' and cast(floor(cast(invoice_date as float)) as datetime) <='" + ToMMddYYYY(Textbox1.Text).ToShortDateString() + "'";
            string sql="select * from vw_CustWiseSales where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) +"'";
				
			//			if(DropCategory.SelectedIndex!=0)
			//				sql=sql+ " and Prod_Type='"+ DropCategory.SelectedItem.Value +"'"; 
			//			if(DropType.SelectedIndex!=0)
			//				sql=sql+ " and Cust_Type='"+ DropType.SelectedItem.Value +"'";
			//			if(DropSSR.SelectedIndex!=0)
			//				sql=sql+ " and Under_SalesMan='"+ DropSSR.SelectedItem.Value +"'";
			if(DropSearchBy.SelectedIndex!=0)
			{
				/********Add by vikas 16.11.2012********************/
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value!="All")
						sql="select * from vw_CustWiseSales cws,customertype ct where cws.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value!="All")
						sql="select * from vw_CustWiseSales cws,customertype ct where cws.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				}/********end********************/
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value!="All")
					{
						//coment by vikas 25.05.09 sql=sql+" and cust_Name='"+DropValue.Value+"'";
						cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql=sql+" and cust_Name='"+cust_type.ToString()+"'";
					}
						
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Invoice_No='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Name='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==7)
				{
					if(DropValue.Value!="All")
						//sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
						sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
				}
			}

			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"vw_CustWiseSales");
			DataTable dtcustomer=ds.Tables["vw_CustWiseSales"];
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
		/// This method is used to show the report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{  
			try
			{
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["Textbox1"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {                  
					MessageBox.Show("Date From Should be less than Date To");
					GridReport.Visible=false;
				}
				else
				{
					//PetrolPumpClass  obj=new PetrolPumpClass();
					//SqlDataReader SqlDtr;
					//string sql;

					#region Bind DataGrid
					//**		sql="select * from vw_CustWiseSales where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(Request.Form["txtDateFrom"].ToString()).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				
					//**		if(DropCategory.SelectedIndex!=0)
					//**			sql=sql+ " and Prod_Type='"+ DropCategory.SelectedItem.Value +"'"; 
					//**		if(DropType.SelectedIndex!=0)
					//**			sql=sql+ " and Cust_Type='"+ DropType.SelectedItem.Value +"'";

					//Response.Write(sql); 
					//**	SqlDtr =obj.GetRecordSet(sql);
					//**	GridReport.DataSource=SqlDtr;
					//**GridReport.DataBind();
					//**	if(GridReport.Items.Count==0)
					//**	{
					//**		MessageBox.Show("Data not available");
					//**		GridReport.Visible=false;
					//**	}
					//**	else
					//**	{
					//**		GridReport.Visible=true;
					//**	}
					//**	SqlDtr.Close();
					#endregion 
					strorderby="Invoice_No ASC";
					Session["Column"]="Invoice_No";
					Session["order"]="ASC";
					Bindthedata();
				}
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnShow_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Viewed "+ " userid  "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnShow_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Viewed "+"   EXCEPTION   "+ex.Message+ " userid  "+uid);
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string cust_type="";
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\MonthWiseCustomerSecondarySales.xls";
			StreamWriter sw = new StreamWriter(path);
			//string sql="select * from vw_CustWiseSales where rate<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(Request.Form["txtDateFrom"].ToString()).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
			string sql="select * from vw_CustWiseSales where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(Request.Form["txtDateFrom"].ToString()).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				
			//			if(DropCategory.SelectedIndex!=0)
			//				sql=sql+ " and Prod_Type='"+ DropCategory.SelectedItem.Value +"'"; 
			//			if(DropType.SelectedIndex!=0)
			//				sql=sql+ " and Cust_Type='"+ DropType.SelectedItem.Value +"'";
			//			//*****
			//			if(DropSSR.SelectedIndex!=0)
			//				sql=sql+ " and Under_SalesMan='"+ DropSSR.SelectedItem.Value +"'";
			//			sql=sql+" order by "+Cache["strorderby"];
			//*****
			if(DropSearchBy.SelectedIndex!=0)
			{
				/********Add by vikas 16.11.2012********************/
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value!="All")
						sql="select * from vw_CustWiseSales cws,customertype ct where cws.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value!="All")
						sql="select * from vw_CustWiseSales cws,customertype ct where cws.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				}/********end********************/
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value!="All")
					{
						//coment by vikas 25.05.09 sql=sql+" and cust_Name='"+DropValue.Value+"'";
						cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql=sql+" and cust_Name='"+cust_type.ToString()+"'";
					}
						
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Invoice_No='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Name='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==7)
				{
					if(DropValue.Value!="All")
						//sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
						sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
				}
			}
			sql=sql+" order by "+Cache["strorderby"];
			SqlDtr=obj.GetRecordSet(sql);
			//sw.WriteLine("Search By\t"+DropSearchBy.SelectedItem.Text+"\tValue\t"+DropValue.Value);
			//sw.WriteLine("|Cust.ID|  Cutomer Name           |       Place        |Inv.No|Inv.Date  | Under Salesman     | Prod.Name     | Qty    |  Price  |");
			sw.WriteLine("Customer Name\tPlace\tInvoice No\tInvoice Date\tProduct Name\tQty(No's)\tQty(Ltr)\tPrice");
			TotalQty=0;TotalQtyLtr=0;
			while(SqlDtr.Read())
			{
				//sw.WriteLine(SqlDtr["Cust_ID"].ToString()+"\t"+SqlDtr["Cust_Name"].ToString()+"\t"+SqlDtr["Place"].ToString()+"\t"+SqlDtr["Invoice_No"].ToString()+"\t"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()))+"\t"+SqlDtr["Under_SalesMan"].ToString()+"\t"+SqlDtr["Prod_Name"].ToString()+"\t"+SqlDtr["Qty"].ToString()+"\t"+SqlDtr["Rate"].ToString());
				//sw.WriteLine(SqlDtr["Cust_Name"].ToString()+"\t"+SqlDtr["Place"].ToString()+"\t"+SqlDtr["Invoice_No"].ToString()+"\t"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()))+"\t"+SqlDtr["Prod_Name"].ToString()+"\t"+SqlDtr["Qty"].ToString()+"\t"+SqlDtr["qtyltr"].ToString()+"\t"+getPrice(SqlDtr["Rate"].ToString(),SqlDtr["qtyltr"].ToString()));
				sw.WriteLine(SqlDtr["Cust_Name"].ToString()+"\t"+SqlDtr["Place"].ToString()+"\t"+SqlDtr["Invoice_No"].ToString()+"\t"+GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Invoice_Date"].ToString()))+"\t"+SqlDtr["Prod_Name"].ToString()+"\t"+SqlDtr["Qty"].ToString()+"\t"+SqlDtr["qtyltr"].ToString()+"\t"+getPrice(SqlDtr["Rate"].ToString(),SqlDtr["qty"].ToString(),SqlDtr["invoice_no"].ToString(),SqlDtr["Prod_ID"].ToString(),SqlDtr["invoice_date"].ToString(),SqlDtr["cust_id"].ToString(),SqlDtr["qtyltr"].ToString()));
				TotalQty+=double.Parse(SqlDtr["Qty"].ToString());
				TotalQtyLtr+=double.Parse(SqlDtr["QtyLtr"].ToString());
			}
			sw.WriteLine("Total\t\t\t\t\t"+TotalQty.ToString()+"\t"+TotalQtyLtr.ToString()+"\t"+Cache["TotalPrice"].ToString());
			sw.Close();
		}

		/// <summary>
		/// Prepares the report file customerwisesalesReport.txt for printting.
		/// </summary>
		public void MakingReport()
		{
			/*
												  ==========================                          
													CUSTOMER SALES REPORT                                    
												  ==========================                    
			+-------+-------------------------+----------+------+----------+--------------------+---------------+-----+---------+
			|Cust.ID|  Cutomer Name           |  Place   |Inv.No|Inv.Date  | Under Salesman     | Prod.Name     | Qty |  Price  |
			+-------+-------------------------+----------+------+----------+--------------------+---------------+-----+---------+
			 1234    1234567890123456789012345 1234567890 1234   DD/MM/YYYY 12345678901234567890 123456789012345 12345 123456.00
			+-------+-------------------------+----------+------+----------+--------------------+---------------+-----+---------+
			 */   
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\MonthWiseCustomerSecondarySales.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string strDate = "";
			string info ="";
			string cust_type="";
			//sql="select * from vw_CustWiseSales where rate<>0 and cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(Request.Form["txtDateFrom"].ToString()).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
			sql="select * from vw_CustWiseSales where cast(floor(cast(invoice_date as float)) as datetime) >= '"+ ToMMddYYYY(Request.Form["txtDateFrom"].ToString()).ToShortDateString() +"' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				
			//			if(DropCategory.SelectedIndex!=0)
			//				sql=sql+ " and Prod_Type='"+ DropCategory.SelectedItem.Value +"'"; 
			//			if(DropType.SelectedIndex!=0)
			//				sql=sql+ " and Cust_Type='"+ DropType.SelectedItem.Value +"'";
			//			//*****
			//			if(DropSSR.SelectedIndex!=0)
			//				sql=sql+ " and Under_SalesMan='"+ DropSSR.SelectedItem.Value +"'";
			//			sql=sql+" order by "+Cache["strorderby"];
			//*****
			if(DropSearchBy.SelectedIndex!=0)
			{
				/********Add by vikas 16.11.2012********************/
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value!="All")
						sql="select * from vw_CustWiseSales cws,customertype ct where cws.cust_type=ct.customertypename and ct.group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value!="All")
						sql="select * from vw_CustWiseSales cws,customertype ct where cws.cust_type=ct.customertypename and ct.sub_group_name='"+DropValue.Value.ToString().Trim()+"' and  cast(floor(cast(invoice_date as float)) as datetime) >= '"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString()) + "' and cast(floor(cast(invoice_date as float)) as datetime) <='"+ GenUtil.str2MMDDYYYY(Request.Form["Textbox1"].ToString()) + "'";
				}/********end********************/
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value!="All")
					{
						//coment by vikas 25.05.09 sql=sql+" and cust_Name='"+DropValue.Value+"'";
						cust_type=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql=sql+" and cust_Name='"+cust_type.ToString()+"'";
					}
						
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Invoice_No='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Type='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Prod_Name='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==7)
				{
					if(DropValue.Value!="All")
						//sql=sql+" and Under_SalesMan='"+DropValue.Value+"'";
						sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
				}
			}
			sql=sql+" order by "+Cache["strorderby"];
			dbobj.SelectQuery(sql,ref rdr);

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
			string des="--------------------------------------------------------------------------------------------------------------------------";
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			//**********
			sw.WriteLine(GenUtil.GetCenterAddr("========================================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("CUSTOMER WISE SALES REPORT FROM "+txtDateFrom.Text+" TO "+Textbox1.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("========================================================",des.Length));
			//sw.WriteLine(" Search By       : "+DropSearchBy.SelectedItem.Value+",   Value    : "+DropValue.Value);
			//sw.WriteLine(" Customer Category: "+DropType.SelectedItem.Value);
			//sw.WriteLine("+-------+-------------------------+--------------------+------+----------+--------------------+---------------+--------+---------+");
			sw.WriteLine("+------------------------------+--------------------+------+----------+------------------+---------+--------+------------+");
			sw.WriteLine("|       Cutomer Name           |       Place        |Inv.No|Inv.Date  |  Product Name    |Qty(No's)|Qty(Ltr)|   Price    |");
			sw.WriteLine("+------------------------------+--------------------+------+----------+------------------+---------+--------+------------+");
			//                           123456789012345678901234567890 12345678901234567890 123456 DD/MM/YYYY 123456789012345678 123456789 12345678 123456789012

			if(rdr.HasRows)
			{
				// info : to displays the each field in different format.
				//info = " {0,-7:S} {1,-30:S} {2,-20:S} {3,-6:D} {4,-10:S} {5,-18:S} {6,9:S} {7,8:F} {8,12:F}";
				info = " {0,-30:S} {1,-20:S} {2,-6:D} {3,-10:S} {4,-18:S} {5,9:S} {6,8:F} {7,12:F}";
				TotalQty=0;TotalQtyLtr=0;
				while(rdr.Read())
				{
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
					sw.WriteLine(info,//rdr["Cust_ID"].ToString().Trim(),
						GenUtil.TrimLength(rdr["Cust_Name"].ToString().Trim(),30),
						GenUtil.TrimLength(rdr["Place"].ToString(),20),
						rdr["Invoice_No"].ToString().Trim(),
						strDate,
						//rdr["Under_SalesMan"].ToString().Trim(),
						GenUtil.TrimLength(strTrim(rdr["Prod_Name"].ToString().Trim()),18),
						rdr["Qty"].ToString().Trim(),
						rdr["Qtyltr"].ToString().Trim(),
						//getPrice(rdr["Rate"].ToString().Trim(),rdr["Qtyltr"].ToString().Trim())
						getPrice(rdr["Rate"].ToString(),rdr["qty"].ToString(),rdr["invoice_no"].ToString(),rdr["Prod_ID"].ToString(),rdr["invoice_date"].ToString(),rdr["cust_id"].ToString(),rdr["qtyltr"].ToString())
						);
					TotalQty+=double.Parse(rdr["Qty"].ToString());
					TotalQtyLtr+=double.Parse(rdr["Qtyltr"].ToString());
				}
			}
			
			sw.WriteLine("+------------------------------+--------------------+------+----------+------------------+---------+--------+------------+");
			sw.WriteLine(info,"Total","","","","",TotalQty.ToString(),TotalQtyLtr.ToString(),Cache["TotalPrice"].ToString());
			sw.WriteLine("+------------------------------+--------------------+------+----------+------------------+---------+--------+------------+");
			// deselect Condensed
			//sw.Write((char)18);
			//sw.Write((char)12);
			sw.Close(); 	

		}

		/// <summary>
		/// This method is used to trim the size of given string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string strTrim(string str)
		{
			if(str.Length > 15)
			{
				str = str.Substring(0,15); 
			}
			return str;
		}
		
		/// <summary>
		/// This method is not used.
		/// </summary>
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6,ref int len7,ref int len8,ref int len9,ref int len10)
		{
			while(rdr.Read())
			{
				if(rdr["Cust_ID"].ToString().Trim().Length>len1)
					len1=rdr["Cust_ID"].ToString().Trim().Length;					
				if(rdr["Cust_Name"].ToString().Trim().Length>len2)
					len2=rdr["Cust_Name"].ToString().Trim().Length;					
				if(rdr["Place"].ToString().Trim().Length>len3)
					len3=rdr["Place"].ToString().Trim().Length;
						
				if(rdr["Invoice_No"].ToString().Trim().Length>len4)
					len4=rdr["Invoice_No"].ToString().Trim().Length;					
				if(rdr["Invoice_Date"].ToString().Trim().Length>len5)
					len5=rdr["Invoice_Date"].ToString().Trim().Length;					
				if(rdr["Under_SalesMan"].ToString().Trim().Length>len6)
					len6=rdr["Under_SalesMan"].ToString().Trim().Length;	
				
				if(rdr["Rate"].ToString().Trim().Length>len10)
					len10=rdr["Rate"].ToString().Trim().Length;	
			}
		}

		protected void GridReport_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// Prepares the report file CustomerWiseSalesReport.txt for printing.
		/// </summary>
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
					CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnprint_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Printed "+"  userid  "+uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MonthWiseCustomerSecondarySales.txt<EOF>");

					// Send the data through the socket.http://localhost/Servosms/Forms/Reports/NozzleReport.aspx
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
					CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnprint_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Printed "+"  EXCEPTION   "+ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnprint_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Printed "+"  EXCEPTION   "+se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnprint_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Printed "+"  EXCEPTION   "+es.Message+"  userid  "+uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnprint_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Printed "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file CustomerWiseSalesReport.xls for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridReport.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnExcel_Click,Class:PetrolPumpClass "+" Customerwise Sales Report Convert Into Excel Format ,  userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: btnExcel_Click,Class:PetrolPumpClass "+" Customerwise Sales Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}

		public double TotalPrice=0;
		/// <summary>
		/// This method is used to calculate the price of given string value.
		/// </summary>
		public string getPrice(string rate, string QtyLtr, string Invoice_No, string Prod_ID, string In_Date, string cust_id, string totqty)
		{
			SqlDataReader rdr = null;
			string schemetype="",cashtype="",disctype="";
			double scheme=0,foe=0,cashdisc=0,vat=0,disc=0;
			
			dbobj.SelectQuery("select discount from foe where prodid='"+Prod_ID+"' and custid='"+cust_id+"' and datefrom<='"+GenUtil.str2MMDDYYYY(In_Date)+"' and dateto>='"+GenUtil.str2MMDDYYYY(In_Date)+"'",ref rdr);
			while(rdr.Read())
			{
				foe=double.Parse(rdr.GetValue(0).ToString());
			}
			rdr.Close();
			foe=foe*double.Parse(totqty);
			if(foe==0)
			{
				dbobj.SelectQuery("select discount,discounttype from oilscheme where prodid='"+Prod_ID+"' and datefrom<='"+GenUtil.str2MMDDYYYY(In_Date)+"' and dateto>='"+GenUtil.str2MMDDYYYY(In_Date)+"'",ref rdr);
				if(rdr.Read())
				{
					scheme=double.Parse(rdr.GetValue(0).ToString());
					schemetype=rdr.GetValue(1).ToString();
				}
				rdr.Close();
				if(schemetype=="%")
				{
					double total=double.Parse(QtyLtr)*double.Parse(rate);
					scheme=(total*scheme)/100;
				}
				else
				{
					scheme=scheme*double.Parse(totqty);
				}
			}
			//dbobj.SelectQuery("select discount,discount_type from sales_master where invoice_no='"+FromDate+ToDate+Invoice_No+"'",ref rdr);
			dbobj.SelectQuery("select discount,discount_type,cash_discount,cash_disc_type from sales_master where invoice_no='"+FromDate+ToDate+Invoice_No+"'",ref rdr);
			if(rdr.Read())
			{
				disc=double.Parse(rdr.GetValue(0).ToString());
				disctype=rdr.GetValue(1).ToString();
				cashdisc=double.Parse(rdr["Cash_Discount"].ToString());
				cashtype=rdr["Cash_Disc_Type"].ToString();
			}
			rdr.Close();
			if(disctype!="")
			{
				if(disctype=="Per")
				{
					double total=double.Parse(QtyLtr)*double.Parse(rate);
					disc=(total*disc)/100;
				}
				else
				{
					dbobj.SelectQuery("select count(*) from sales_details where invoice_no='"+FromDate+ToDate+Invoice_No+"'",ref rdr);
					if(rdr.Read())
					{
						double total=double.Parse(rdr.GetValue(0).ToString());
						disc=disc/total;
					}
					rdr.Close();
				}
			}
			//****************************
			if(cashtype!="")
			{
				if(cashtype=="Per")
				{
					double total=double.Parse(QtyLtr)*double.Parse(rate);
					total=total-(disc+scheme+foe);
					cashdisc=(total*cashdisc)/100;
				}
				else
				{
					dbobj.SelectQuery("select count(*) from sales_details where invoice_no='"+FromDate+ToDate+Invoice_No+"'",ref rdr);
					if(rdr.Read())
					{
						double total=double.Parse(rdr.GetValue(0).ToString());
						cashdisc=cashdisc/total;
					}
					rdr.Close();
				}
			}
			//****************************
			/*dbobj.SelectQuery("select cash_discount,cash_disc_type from sales_master where invoice_no='"+FromDate+ToDate+Invoice_No+"'",ref rdr);
			if(rdr.Read())
			{
				cashdisc=double.Parse(rdr.GetValue(0).ToString());
				cashtype=rdr.GetValue(1).ToString();
			}
			rdr.Close();
			if(cashtype!="")
			{
				if(cashtype=="Per")
				{
					double total=double.Parse(QtyLtr)*double.Parse(rate);
					total=total-(disc+scheme+foe);
					cashdisc=(total*cashdisc)/100;
				}
				else
				{
					dbobj.SelectQuery("select count(*) from sales_details where invoice_no='"+FromDate+ToDate+Invoice_No+"'",ref rdr);
					if(rdr.Read())
					{
						double total=double.Parse(rdr.GetValue(0).ToString());
						cashdisc=cashdisc/total;
					}
					rdr.Close();
				}
			}*/
			//			dbobj.SelectQuery("select vat_rate from organisation",ref rdr);
			//			if(rdr.Read())
			//			{
			//				vat=double.Parse(rdr.GetValue(0).ToString());
			//				double total=double.Parse(QtyLtr)*double.Parse(rate);
			//				total=total-(cashdisc+disc+scheme+foe);
			//				vat=(total*vat)/100;
			//			}
			//			rdr.Close();
			//****************
			vat=double.Parse(Session["VAT_Rate"].ToString());
			double total1=double.Parse(QtyLtr)*double.Parse(rate);
			total1=total1-(cashdisc+disc+scheme+foe);
			vat=(total1*vat)/100;
			//****************
			double tot=0;
			tot=double.Parse(rate)*double.Parse(QtyLtr);
			tot=tot+vat-(cashdisc+scheme+foe+disc);
			tot=Math.Round(tot);
			TotalPrice+=tot;
			Cache["TotalPrice"]=TotalPrice;
			return tot.ToString();
		}

		public double TotalQty=0;
		public double TotalQtyLtr=0;
		/// <summary>
		/// This method is used to calculate the total qty.
		/// </summary>
		public void getQty(double qty)
		{
			TotalQty+=qty;
			//Cache["Qty"]=TotalQty;
			//return qty;
		}

		/// <summary>
		/// This method is used to calculate the total qty in liter.
		/// </summary>
		public void getQtyLtr(double qtyltr)
		{
			TotalQtyLtr+=qtyltr;
			//Cache["QtyLtr"]=TotalQtyLtr;
			//return qtyltr;
		}

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
					getQty(Double.Parse(e.Item.Cells[6].Text));
					getQtyLtr(Double.Parse(e.Item.Cells[7].Text)); 
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					e.Item.Cells[6].Text = TotalQty.ToString();
					e.Item.Cells[7].Text = TotalQtyLtr.ToString();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerwiseSalesReport.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
		}

		protected void DropSearchBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//			try
			//			{
			//				InventoryClass obj = new InventoryClass();
			//				SqlDataReader rdr = null;
			//				string sql="";
			//				DropValue.Items.Clear();
			//				DropValue.Items.Add("All");
			//				if(DropSearchBy.SelectedIndex!=0)
			//				{
			//					if(DropSearchBy.SelectedIndex==1)
			//						sql="select distinct case when Cust_Type like'Oe%' then 'Oe' when Cust_Type like'Ibp%' then 'IBP' when  Cust_Type like'ro%' then 'Ro' else Cust_Type  end  from vw_custwisesales";
			//					else if(DropSearchBy.SelectedIndex==2)
			//						sql="select distinct Cust_Name from vw_custwisesales order by cust_Name";
			//					else if(DropSearchBy.SelectedIndex==3)
			//						sql="select distinct Prod_Type from vw_custwisesales order by prod_type";
			//					else if(DropSearchBy.SelectedIndex==4)
			//						sql="select distinct Prod_Name from vw_custwisesales order by prod_name";
			//					else if(DropSearchBy.SelectedIndex==5)
			//						sql="select distinct Under_Salesman from vw_custwisesales order by under_salesman";
			//					rdr = obj.GetRecordSet(sql);
			//					if(rdr.HasRows)
			//					{
			//						while(rdr.Read())
			//						{
			//							DropValue.Items.Add(rdr.GetValue(0).ToString());
			//						}
			//					}
			//					else
			//					{
			//						MessageBox.Show("Data Not Available");
			//						return;
			//					}
			//				}
			//			}
			//			catch(Exception ex)
			//			{
			//				CreateLogFiles.ErrorLog("Form:CustomerWiseSalseReport,Method: DropSearchBy_Click,Class:PetrolPumpClass "+" Customerwise Sales Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			//			}
		}

		/// <summary>
		/// This method return only year part of passing the date
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetYear(string dt)
		{
			if(dt!="")
			{
				string[] year=dt.IndexOf("/")>0?dt.Split(new char[] {'/'},dt.Length): dt.Split(new char[] { '-' }, dt.Length);
				string yr=year[2].Substring(2);	
				return(yr);
			}
			else
				return "";
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
				string strName="",strInvoiceNo="",strType="",strProductGroup="",strProdWithPack="",strSSR="";
				string strGroup="",strSubGroup="";

				//strType = "select distinct case when cust_type like 'oe%' then 'Oe' else cust_type end as cust_type from customer order by cust_type";
				// coment by vikas 16.11.2012 strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				
				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012

				//Coment by vikas 25.05.09 strName="select distinct Cust_Name from vw_custwisesales order by cust_Name";
				strName="select distinct Cust_Name,place from vw_custwisesales order by cust_Name,place";
				
				strInvoiceNo="select distinct Invoice_No from vw_custwisesales order by invoice_no";
				strProductGroup="select distinct Prod_Type from vw_custwisesales order by prod_type";
				strProdWithPack="select distinct Prod_Name from vw_custwisesales order by prod_name";
				strSSR="select Emp_Name from Employee where designation='Servo Sales representative' order by Emp_name";

				//coment by vikas 16.11.2012 string[] arrStr = {strName,strType,strInvoiceNo,strProductGroup,strProdWithPack,strSSR};
				//coment by vikas 16.11.2012 HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempInvoiceNo,tempProductGroup,tempProdWithPack,tempSSR};
				
				string[] arrStr = {strName,strInvoiceNo,strProductGroup,strProdWithPack,strSSR,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempCustName,tempInvoiceNo,tempProductGroup,tempProdWithPack,tempSSR,tempGroup,tempSubGroup};

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							/********Add by vikas 25.05.09*************/
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
							/******************************************/
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerWiseSalesReport.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Wise Sales Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}