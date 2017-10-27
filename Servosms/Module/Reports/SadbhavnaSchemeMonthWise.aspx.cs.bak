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
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using DBOperations;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Servosms.Sysitem.Classes;
using RMG;
using System.Web.SessionState;
using System.Runtime.InteropServices;
using System.Management;
using System.Net;
using System.Net.Sockets;
using MySecurity;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SadbhavnaSchemeMonthWise.
	/// </summary>
	public partial class SadbhavnaSchemeMonthWise : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		System.Globalization.NumberFormatInfo  nfi = new System.Globalization.CultureInfo("en-US",false).NumberFormat;
		public string sql;
		public string str1;
		public string str2;
		public string str3;
		string uid="";

		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();

			//			string sql="Select Dealername,dealership,address,foodlicno,wm from organisation where CompanyID = 1001";
			//			SqlDtr =obj.GetRecordSet(sql);
			//			if(SqlDtr.Read())
			//			{
			//				str1 = SqlDtr.GetValue(0).ToString()+","+SqlDtr.GetValue(1).ToString()+SqlDtr.GetValue(2).ToString();  
			//				str2 = SqlDtr.GetValue(3).ToString();
			//				str3 = SqlDtr.GetValue(4).ToString();
			//			}
			//			else
			//			{
			//				str1 = "";
			//				str2 = "";
			//				str3 ="";
			//			}
			//			SqlDtr.Close();
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["txtDateFrom"].ToString();
            Textbox1.Text = Request.Form["Textbox1"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["Textbox1"].ToString();
            if (!Page.IsPostBack )
			{
				GridSalesReport.Visible=false;
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="40";
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
				txtDateFrom.Text=DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
				Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            Textbox1.Text = Request.Form["Textbox1"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["Textbox1"].ToString().Trim();
        }

		# region DateTime Function...
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.IndexOf("/")>0? str.Split(new char[]{'/'},str.Length) : str.Split(new char[] { '-' }, str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}
		# endregion
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

		//		double bonus;
		//		double total;
		
		public double total2;
		public double total3;
		public double total4;
		public double invtotal;
		public double qtytotal;
		
		/// <summary>
		/// This method is called from the data grid and declare in the data grid tag parameter OnItemDataBound
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{		
			double bonus;
			double total;
			double total1;
			double check;
			double  qtyltr;
			qtyltr=0;
			total=0;
			total1=0;
			bonus=0;
			check=0;

			double c9;
			double c11;
			
			// If the cell item is not a header and footer then pass calls the total functions by passing the corressponding values.
			if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem )  )
			{
				qtyltr=Double.Parse(e.Item.Cells[6].Text);
				check=Double.Parse(e.Item.Cells[7].Text);
				   
				bonus=Math.Round(Double.Parse(e.Item.Cells[7].Text)/100,2);
				e.Item.Cells[9].Text=bonus.ToString();
				c9=	Double.Parse(e.Item.Cells[9].Text);
				if(check >=100000)
				{
					total1=Math.Round(Double.Parse(e.Item.Cells[9].Text)*.2,2);
					e.Item.Cells[10].Text=total1.ToString();
				}
				else if(check >=50000)
				{
					total1=Math.Round(Double.Parse(e.Item.Cells[9].Text)*.1,2);
					e.Item.Cells[10].Text=total1.ToString();
				}
				else if(check >=10000)
				{
					total1=Math.Round(Double.Parse(e.Item.Cells[9].Text)*.05,2);
					e.Item.Cells[10].Text=total1.ToString();
				}
				else 
				{
					e.Item.Cells[10].Text="0";
					total1=0;
				}
				if(e.Item.Cells[11].Text=="")
				{
					c11=0;
				}
				else
				{
					c11=Math.Round(Double.Parse(e.Item.Cells[11].Text),2);
				}
				total=Math.Round(c9+c11+total1,2);
				e.Item.Cells[12].Text=total.ToString();
				//***********
				qtytotal+=qtyltr;
				total2+=bonus;
				total3+=total1;
				total4+=total;
				invtotal+=check;
				//***********
			}
			else if(e.Item.ItemType == ListItemType.Footer)
			{
				// else if the item cell is footer then display the final total values in corressponding cells and columns. the nfi is used to display the amount in #,###.00 format
               
				e.Item.Cells[6].Text =qtytotal.ToString("N",nfi);
				e.Item.Cells[7].Text =invtotal.ToString("N",nfi); 
				e.Item.Cells[9].Text = total2.ToString("N",nfi);
				e.Item.Cells[10].Text = total3.ToString("N",nfi);
				e.Item.Cells[12].Text =total4.ToString("N",nfi);   
				//				Cache["qty"]=qtytotal.ToString("N",nfi);
				//				Cache["inv"]=invtotal.ToString("N",nfi); 
				//				Cache["bas"]=total2.ToString("N",nfi);
				//				Cache["bon"]=total3.ToString("N",nfi);
				//				Cache["tot"]=total4.ToString("N",nfi);
				Cache["qty"]=qtytotal.ToString();
				Cache["inv"]=invtotal.ToString(); 
				Cache["bas"]=total2.ToString();
				Cache["bon"]=total3.ToString();
				Cache["tot"]=total4.ToString();
			}
		
		}

		/// <summary>
		/// this is used to bind the datagrid .
		/// </summary>
		public void Bindthedata()
		{
			string sql="";
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			
			//string sql="select * from vw_SaleBook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			//string sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Sales_Master.Under_SalesMan, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master INNER JOIN  dbo.Customer ON dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID where (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			// string sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'"; // 30.03.09 Vikas Sharma
			
			//coment by vikas sharma 23.05.09 string sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			
			/*************Start add by vikas 23.05.09 ***************************************/
			if(DropCustType.SelectedIndex==0)
				//15.07.09 vikas sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%') or Cust_Type like('Essar ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";                   //15.07.09 Add by vikas
			else
				sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and  Cust_Type like('"+DropCustType.SelectedValue.ToString().Trim()+"%') and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";

			/*************end ***************************************/
								
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();
			da.Fill(ds,"Sales_Master");
			DataTable dtcustomer=ds.Tables["Sales_Master"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			GridSalesReport.DataSource=dv;
			if(dv.Count!=0)
			{
				GridSalesReport.DataBind();
				GridSalesReport.Visible=true;
			}
			else
			{
				GridSalesReport.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}

		/// <summary>
		/// this is used to make sorting the datagrid on clicking of the datagridheader.
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
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  ");
			}
		}

		/// <summary>
		/// This method is used to prepare the report  file.
		/// </summary>
		public void makingReport()
		{
			/*
																	=========================================
																	Sadbhavna Scheme MonthWise From 01/07/2006 To 9/7/2006
																	=========================================
+-------+--------------------+---------------+-------------+--------+---------+------+----------+----------+-------+-------+-------+-------+
|Unique |    Customer Name   |     Place     |Customer Type|Invoice |Invoice  | QTY  | Invoice  |Ineligible| Basic | Bonus |Special|Total  |
|Code   |                    |               |             | No.    | Date    |Liter | Amount   |GradeValue|Points |Points |Points |Points |
+-------+--------------------+---------------+-------------+--------+---------+------+----------+----------+-------+-------+-------+-------+
 1234567 12345678901234567890 123456789012345 1234567890123 12345678 123456789 123456 1234567890 1234567890 1234567 1234567 1234567 1234567 
 */
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				string strDate="";
							
				
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\SadbhavnaSchemeMonthWise.txt";
				StreamWriter sw = new StreamWriter(path);
				//sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Sales_Master.Under_SalesMan, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master INNER JOIN  dbo.Customer ON dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID where (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";		
				// sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name,dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'"; // 30.03.09 Vikas Sharma
				
				//Coment by vikas sharma 23.05.09 sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name,dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				
				/*************Start add by vikas 23.05.09 ***************************************/
				if(DropCustType.SelectedIndex==0)
					//15.07.09 coment by vikas sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
					sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%') or Cust_Type like('Essar ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";                   //15.07.09 Add by vikas
				else
					sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and  Cust_Type like('"+DropCustType.SelectedValue.ToString().Trim()+"%') and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";

				/*************end ***************************************/
		
				sql=sql+" order by "+Cache["strorderby"];
						
				//sql="select vndr_invoice_no invoice_no, vndr_invoice_date invoice_date,trade_discount,ebird_discount,Grand_Total, (case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end) as Cash_Disc, VAT_Amount, (case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end) as Disc, Net_Amount, s.Supp_Name, s.City, s.Tin_No from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
				//sql=sql+" order by "+Cache["strorderby2"];
				
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
				string des="-----------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("========================================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Sadbhavna Scheme MonthWise From "+txtDateFrom.Text.ToString()+" To "+Textbox1.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("========================================================",des.Length));
				sw.WriteLine("+--------+--------------------+------------+--------+------+----------+--------+----------+----------+--------+--------+------+---------+");
				sw.WriteLine("| Unique |   Customer Name    |   Place    |Customer|Invoic| Invoice  |  QTY   | Invoice  |Ineligible| Basic  | Bonus  | Spe. |  Total  |");
				sw.WriteLine("|  Code  |                    |            |  Type  | No.  |  Date    | Liter  | Amount   |GradeValue| Points | Points |Points|  Points |");
				sw.WriteLine("+--------+--------------------+------------+--------+------+----------+--------+----------+----------+--------+--------+------+---------+"); 
				//             12345678 12345678901234567890 123456789012 12345678 123456 1234567890 12345678 1234567890 1234567890 12345678 12345678 123456 123456789 
			
				if(rdr.HasRows)
				{
					// info : to set the string format.
					info = " {0,-8:S} {1,-20:S} {2,-12:S} {3,-8:S} {4,-6:S} {5,-10:S} {6,8:S} {7,10:S} {8,10:S} {9,8:S} {10,8:F} {11,6:S} {12,9:S}";
					while(rdr.Read())
					{
						strDate = rdr["s6"].ToString().Trim();
						int pos = strDate.IndexOf(" ");
						if(pos != -1)
						{
							strDate = strDate.Substring(0,pos);
						}
						else
						{
							strDate = "";					
						}
						sw.WriteLine(info,rdr["s1"].ToString().Trim(),
							//GenUtil.str2DDMMYYYY(strDate),
							StringUtil.trimlength(rdr["s2"].ToString().Trim(),20),
							StringUtil.trimlength(rdr["s3"].ToString().Trim(),12),
							StringUtil.trimlength(rdr["s4"].ToString().Trim(),8),
							rdr["s5"].ToString().Trim(),GenUtil.str2DDMMYYYY(strDate),
							//discountfocDiscC(rdr["s6"].ToString().Trim()).ToString(),
							GenUtil.strNumericFormat(rdr["s7"].ToString().Trim()),
							GenUtil.strNumericFormat(rdr["s8"].ToString().Trim()),"",basic(rdr["s8"].ToString().Trim()),Bonus(rdr["s5"].ToString().Trim()),"",sum(basic(rdr["s8"].ToString().Trim()),Bonus(rdr["s5"].ToString().Trim()))
							//							discountfocDiscB(rdr["invoice_no"].ToString().Trim()).ToString(),
							//							discountfocDiscA(rdr["invoice_no"].ToString().Trim()).ToString(),
							//							rdr["VAT_Amount"].ToString().Trim(),rdr["Net_Amount"].ToString().Trim()
							);
					}
				}
				sw.WriteLine("+--------+--------------------+------------+--------+------+----------+--------+----------+----------+--------+--------+------+---------+");
				sw.WriteLine(info,"Total:","","","","","",GenUtil.strNumericFormat(Cache["qty"].ToString()),GenUtil.strNumericFormat(Cache["inv"].ToString()),"",Cache["bas"],Cache["bon"],"",Cache["tot"]);
				sw.WriteLine("+--------+--------------------+------------+--------+------+----------+--------+----------+----------+--------+--------+------+---------+");
				dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid ");
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\ServoSadbhavnaSchemeMonthWise.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",strDate="";
			//sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Sales_Master.Under_SalesMan, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master INNER JOIN  dbo.Customer ON dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID where (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";		
			// sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'"; // 30.03.09 Vikas Sharma
			
			//Coment by vikas 23.05.09 sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
			
			/*************Start add by vikas 23.05.09 ***************************************/
			if(DropCustType.SelectedIndex==0)
				//15.07.09 coment by vikas sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and (Cust_Type='Bazzar' or Cust_Type like('Ksk%') or Cust_Type like('N-Ksk%') or Cust_Type like('Essar ro%')) and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";                   //15.07.09 Add by vikas
			else
				sql="SELECT dbo.Customer.Cust_ID, dbo.Customer.sadbhavnacd s1, dbo.Customer.Cust_Name s2, dbo.Customer.City s3, dbo.Customer.Cust_Type s4, substring(cast(dbo.Sales_Master.Invoice_No as varchar),4,9) s5,dbo.Sales_Master.Invoice_Date s6, dbo.Employee.Emp_Name, dbo.Sales_Master.totalqtyltr as s7, dbo.Sales_Master.Net_Amount s8 FROM dbo.Sales_Master,dbo.Employee,dbo.Customer where dbo.Sales_Master.Cust_ID = dbo.Customer.Cust_ID and dbo.Employee.Emp_ID=dbo.Customer.SSR and  Cust_Type like('"+DropCustType.SelectedValue.ToString().Trim()+"%') and  cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";

			/*************end ***************************************/
				
			sql=sql+" order by "+""+Cache["strorderby"]+"";
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("From Date\t"+txtDateFrom.Text);
			sw.WriteLine("To Date\t"+Textbox1.Text);
			sw.WriteLine();
			sw.WriteLine("Unique Code\tCustomer Name\tPlace\tCustomer Type\tInvoice No\tInvoice Date\tQTY Ltr.\tInvoice Amount\tIneligible GradeValue\tBasic Point\tBonus Point\tSpecial Point\tTotal Point");
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{					
					strDate = rdr["s6"].ToString().Trim();
					int pos = strDate.IndexOf(" ");
				
					if(pos != -1)
					{
						strDate = strDate.Substring(0,pos);
					}
					else
					{
						strDate = "";					
					}
					sw.WriteLine(rdr["s1"].ToString().Trim()+"\t"+
						rdr["s2"].ToString().Trim()+"\t"+
						rdr["s3"].ToString().Trim()+"\t"+
						rdr["s4"].ToString().Trim()+"\t"+
						rdr["s5"].ToString().Trim()+"\t"+
						GenUtil.str2DDMMYYYY(strDate)+"\t"+
						GenUtil.strNumericFormat(rdr["s7"].ToString().Trim())+"\t"+
						GenUtil.strNumericFormat(rdr["s8"].ToString().Trim())+"\t"+
						""+"\t"+
						basic(rdr["s8"].ToString().Trim())+"\t"+
						Bonus(rdr["s5"].ToString().Trim())+"\t"+
						""+"\t"+
						sum(basic(rdr["s8"].ToString().Trim()),Bonus(rdr["s5"].ToString().Trim()))
						);
				}
			}
			rdr.Close();
			sw.WriteLine("Total\t\t\t\t\t\t"+Cache["qty"]+"\t"+Cache["inv"]+"\t"+""+"\t"+Cache["bas"]+"\t"+Cache["bon"]+"\t"+""+"\t"+Cache["tot"]);
			sw.Close();
		}

		//public double total3;
		/// <summary>
		/// This method is used to calculate the bonus point from sales master table with apply some condition.
		/// </summary>
		protected double Bonus(string invoiceno )
		{
			//int month=System.Convert.ToInt32(mon);
			//int year=System.Convert.ToInt32(dropyear.SelectedItem.Text.ToString());
			
			double total=0;
			double bonus=0;
			double totpt=0;
			double total1=0;
			//total3=0;
			SqlDataReader SqlDtr;
			PetrolPumpClass obj=new PetrolPumpClass();
			
			//string sql="select sum(sm.totalqtyltr )  from Sales_Master sm where  cast(floor(cast(invoice_date as float)) as datetime) >= '"+mon+"/1/"+dropyear.SelectedIndex+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+mon+"/"+DateTime.DaysInMonth(dropyear.SelectedIndex+1,month) +"/"+dropyear.SelectedIndex+"'and sm.Cust_ID='"+customerid+"'";	
			string sql="select sm.Net_Amount from Sales_Master sm where  sm.invoice_no='"+invoiceno+"'";	
			SqlDtr =obj.GetRecordSet(sql);
			if(SqlDtr.HasRows )
			{
				while(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals(""))
						totpt =	System.Convert.ToDouble(SqlDtr.GetValue(0).ToString());
					//*************
					total=0;
					total1=0;
					bonus=Math.Round(totpt/100,2);
					//					e.Item.Cells[9].Text=bonus.ToString();
					//					c9=	Double.Parse(e.Item.Cells[9].Text);
					if(totpt >=100000)
					{
						total1=Math.Round(bonus*.2,2);
						//e.Item.Cells[10].Text=total1.ToString();
					}
					else if(totpt >=50000)
					{
						total1=Math.Round(bonus*.1,2);
						//e.Item.Cells[10].Text=total1.ToString();
					}
					else if(totpt >=10000)
					{
						total1=Math.Round(bonus*.05,2);
						//e.Item.Cells[10].Text=total1.ToString();
					}
					else 
					{
						//e.Item.Cells[10].Text="0";
						total1=0;
					}
					//					if(e.Item.Cells[11].Text=="")
					//					{
					//						c11=0;
					//					}
					//					else
					//					{
					//						c11=Math.Round(Double.Parse(e.Item.Cells[11].Text),2);
					//					}
					total=Math.Round(total1,2);
					//					e.Item.Cells[12].Text=total.ToString();
					
					//*************
				}
			}
			else
				total= 0 ;
			//	total3+=total;
			SqlDtr.Close();
			return total;
		}
		
		/// <summary>
		/// This method is used to calculate the invoice amount
		/// </summary>
		public string basic(string invamt)
		{
			double amt=System.Convert.ToDouble(invamt);
			amt=amt/100;
			return GenUtil.strNumericFormat(amt.ToString());
		}

		public string sum(string bas,double bonus)
		{
			double amt=System.Convert.ToDouble(bas);
			amt=amt+bonus;
			return GenUtil.strNumericFormat(amt.ToString());
		}

		/// <summary>
		/// This method is used to view the report with the help of Bindthedata() function.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
                if (System.DateTime.Compare(ToMMddYYYY(Request.Form["txtDateFrom"].ToString().Trim()), ToMMddYYYY(Request.Form["Textbox1"].ToString().Trim())) > 0)
                //if(DateTime.Compare(ToMMddYYYY(txtDateFrom.Text),ToMMddYYYY(Textbox1.Text))>0)
                {
					MessageBox.Show("Date From Should be less than Date To");
					GridSalesReport.Visible=false;
				}
				else
				{
					strorderby="s4 ASC";
					Session["Column"]="s4";
					Session["order"]="ASC";
					Bindthedata();
				}
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:btnShow_Click  SadbhavnaSchemeMonthWise   Viewed "+"  userid  ");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:btnShow_Click  SadbhavnaSchemeMonthWise   Viewed "+"  EXCEPTION  "+ ex.Message+"  userid  ");
			}
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the SadBhavnaSchemeMonthWise.txt file name to print.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			makingReport();

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\SadbhavnaSchemeMonthWise.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:BtnPrint_Click  SadbhavnaSchemeMonthWise   userid  ");
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed    EXCEPTION  "+ ane.Message+" userid  ");
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed  EXCEPTION  "+ se.Message+"  userid  ");
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed   EXCEPTION "+es.Message+"  userid  ");
				}
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Method:BtnPrint_Click, SadbhavnaSchemeMonthWise Printed  EXCEPTION   "+ es.Message+"  userid  ");
			}
		}

		/// <summary>
		/// Prepares the excel report file SadBhavnaschemeMonthWise.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridSalesReport.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SadbhavnaSchemeMonthWise Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:SadbhavnaSchemeMonthWise.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    SadbhavnaSchemeMonthWise Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}