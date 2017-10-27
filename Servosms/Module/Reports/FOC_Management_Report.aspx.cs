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
using System.Data.SqlClient;
using RMG;
using DBOperations;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for FOC_Management_Report.
	/// </summary>
	public partial class FOC_Management_Report : System.Web.UI.Page
	{
		string uid;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public static double TotalQty_Ltr = 0;
		public static double TotalQty_No = 0;
		public static double TotalNet_Amount = 0;
		public int i=1;
		public static ArrayList ALFOC=new ArrayList();
		public static ArrayList ALFOC_Details=new ArrayList();

		public static double TotalQty_Ltr_main = 0;
		public static double TotalQty_No_main = 0;
		
		public int flage=0;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="36";
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
				GetMultiValue();
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            Textbox1.Text = Request.Form["Textbox1"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["Textbox1"].ToString().Trim();
        }


		/// <summary>
		/// This method is used to calculates the total Qty in No. by passing value 
		/// </summary>
		protected void TotalQtyinNo(string _QtyInNo)
		{
			TotalQty_No  += double.Parse(_QtyInNo); 
		}

		protected void TotalQtyinLtr(string _QtyInLtr)
		{
			TotalQty_Ltr  += double.Parse(_QtyInLtr); 
		}

		/// <summary>
		/// This method is used to calculates total Net Amount by passing value 
		/// </summary>
		protected void TotalNetAmount(string _NetAmount)
		{
			TotalNet_Amount  += double.Parse(_NetAmount); 
		}
		/// <summary>
		/// Its calls from data grid  and define in the data grid tag parameter "OnItemDataBound"
		/// </summary>
		public void ItemTotal(object sender,DataGridItemEventArgs e)
		{
			try
			{
				if((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem ) || (e.Item.ItemType == ListItemType.SelectedItem)  )
				{
					TotalQtyinNo(e.Item.Cells[7].Text);
					TotalQtyinLtr(e.Item.Cells[8].Text); 
					
				}
				else if(e.Item.ItemType == ListItemType.Footer)
				{
					e.Item.Cells[7].Text = GenUtil.strNumericFormat(TotalQty_No.ToString());
					e.Item.Cells[8].Text = GenUtil.strNumericFormat(TotalQty_Ltr.ToString());
					
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:ItemTotal()  EXCEPTION  "+ex.Message+".  User_ID:"+ uid );
			}
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
				CreateLogFiles.ErrorLog("Form:SalesBookReport.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value from dropdownlist.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strName="",strInvoiceNo="",strType="",strProductGroup="",strProdWithPack="",strSSR="",strPackType="";

				strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";
				
				strName="select distinct Cust_Name,city from vw_salebook order by cust_Name,city";
				
				strInvoiceNo="select distinct cast(Invoice_No as int) Invoice_No from vw_salebook order by invoice_no"; //Add By Vikas Sharma 16.04.09

				strProductGroup="select distinct category from vw_salebook order by category";
				strPackType="select distinct Pack_Type from vw_salebook order by Pack_Type";
				strProdWithPack="select distinct Prod_Name+':'+pack_type from vw_salebook order by prod_name+':'+pack_type";
				strSSR="select Emp_Name from Employee where designation='Servo Sales representative' and status=1 order by Emp_name";

				string[] arrStr = {strName,strType,strInvoiceNo,strPackType,strProductGroup,strProdWithPack,strSSR};
				HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempInvoiceNo,tempPackType,tempProductGroup,tempProdWithPack,tempSSR};
				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
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
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Sale Book Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
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

		public void Bindthedata()
		{
			ALFOC=new ArrayList();
			ALFOC_Details=new ArrayList();
			try
			{
				SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				string sql="";
				
				if(dropsummry.SelectedIndex==0)
				{
					sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";
					//string sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"])+"'";

					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							if(DropValue.Value!="All")
								sql=sql+" and cust_type like '"+DropValue.Value+"%'";
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							if(DropValue.Value!="All")
							{
								string cust_name="";
								cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
								sql=sql+" and cust_Name='"+cust_name.ToString()+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							if(DropValue.Value!="All")
								sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where prod_name like '%(FOC)' and Invoice_No='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==4)
						{
							if(DropValue.Value!="All")
								//02.10.09 sql=sql+" and Category='"+DropValue.Value+"'";
								sql=sql+" and Pack_Type='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==5)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Category='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==6)
						{
							if(DropValue.Value!="All")
							{
								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==7)
						{
							if(DropValue.Value!="All")
								sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
						}
					}
					sql += " group by invoice_no,cust_name,city,invoice_date,Prod_Name,Pack_Type order by invoice_no";
				
					/*********Start Add by vikas 24.10.09********************************/
					sqlcon.Open();
					SqlCommand cmd= new SqlCommand(sql,sqlcon);
					SqlDataReader sdr=cmd.ExecuteReader();
					while(sdr.Read())
					{
						ALFOC.Add(sdr.GetValue(2)+":"+sdr.GetValue(1)+":"+sdr.GetValue(0)+":"+ GenUtil.trimDate(GenUtil.str2DDMMYYYY(sdr.GetValue(3).ToString()))+":"+sdr.GetValue(4)+":"+sdr.GetValue(5)+":"+sdr.GetValue(6)+":"+sdr.GetValue(7));
					}
					sdr.Close();
					cmd.Dispose();
					/*********End********************************/
				}
				else
				{
					//1.12.2012 sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,quant quant,quant*total_qty totqty,sno,prod_id from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";
					sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,quant quant,quant*total_qty totqty,sno,prod_id from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name not like '%FOC%'";

					//sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,quant quant,quant*total_qty totqty,sno,prod_id from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])+"' and cast(floor(cast(invoice_date as float)) as datetime)<='10/31/2012' and prod_name not like '%FOC%'";

					if(DropSearchBy.SelectedIndex!=0)
					{
						if(DropSearchBy.SelectedIndex==1)
						{
							if(DropValue.Value!="All")
								sql=sql+" and cust_type like '"+DropValue.Value+"%'";
						}
						else if(DropSearchBy.SelectedIndex==2)
						{
							if(DropValue.Value!="All")
							{
								string cust_name="";
								cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
								sql=sql+" and cust_Name='"+cust_name.ToString()+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==3)
						{
							if(DropValue.Value!="All")
								sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where prod_name like '%(FOC)' and Invoice_No='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==4)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Pack_Type='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==5)
						{
							if(DropValue.Value!="All")
								sql=sql+" and Category='"+DropValue.Value+"'";
						}
						else if(DropSearchBy.SelectedIndex==6)
						{
							if(DropValue.Value!="All")
							{
								string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
								sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
							}
						}
						else if(DropSearchBy.SelectedIndex==7)
						{
							if(DropValue.Value!="All")
								sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
						}

						//1.12.2012 sql+=" and prod_id in (select Prodid from oilscheme where (cast(floor(cast(datefrom as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(dateto as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ ToMMddYYYY(Request.Form["Textbox1"])+"')) Order by invoice_no,sno,cust_name";
					}
					sql+=" and prod_id in (select Prodid from oilscheme where (schname ='Secondry(Free Scheme)' or schname ='Primary(Free Scheme)')  and (cast(floor(cast(datefrom as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(dateto as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(dateto as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ ToMMddYYYY(Request.Form["Textbox1"])+"')) Order by invoice_no,sno,cust_name";

					int i;
					string invoice_no="";
					string ProductName="";
					string Name="";
					string Type="";
					double qty_Nos=0;
					double qty_Ltr=0;
					string prod_id="";
					
					InventoryClass obj=new InventoryClass();
					InventoryClass obj1=new InventoryClass();
					SqlDataReader sdr_detail=null;
					SqlDataReader sdr_detail1=null;
					sqlcon.Open();
					SqlCommand cmd= new SqlCommand(sql,sqlcon);
					SqlDataReader sdr=cmd.ExecuteReader();
					while(sdr.Read())
					{
						i=0;
						i=int.Parse(sdr["sno"].ToString());
						invoice_no=sdr["invoice_no"].ToString();
						i--;
						ProductName="";
						Name="";
						Type="";
						qty_Nos=0;
						qty_Ltr=0;

						//sql="select p.Prod_Name,p.Pack_Type,qty,(qty*total_qty) Ltr from oilscheme o,Sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and sd.prod_id=p.prod_id and sd.prod_id='"+prod_id+"' and sm.invoice_no='709"+invoice_no+"' and o.pack_type='combo' and o.prodid=sd.prod_id and p.mrp>0 and (cast(floor(cast(datefrom as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(dateto as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ ToMMddYYYY(Request.Form["Textbox1"])+"')";
						//sdr_detail1=obj1.GetRecordSet(sql);
						//while(sdr_detail1.Read())
						//{
							sql="select Prod_Name,Pack_type,quant,(quant*total_qty) Ltr,prod_id from vw_salebook where invoice_no='"+invoice_no+"' and Rate =0 and sno='"+i+"'";
							sdr_detail=obj.GetRecordSet(sql);
							while(sdr_detail.Read())
							{
								prod_id=sdr_detail["Prod_id"].ToString();
								//coment by vikas 30.11.2012 ProductName=sdr_detail["Prod_Name"].ToString()+":"+sdr_detail["Pack_type"].ToString()+":"+sdr_detail["quant"].ToString()+":"+sdr_detail["Ltr"].ToString();;
								Name=sdr_detail["Prod_Name"].ToString();
								Type=sdr_detail["Pack_type"].ToString();
								qty_Nos=double.Parse(sdr_detail["quant"].ToString());
								qty_Ltr=double.Parse(sdr_detail["Ltr"].ToString());
							}
							sdr_detail.Close();
						//}
						//sdr_detail1.Close();

						/*sql="select p.Prod_Name,p.Pack_Type,qty,(qty*total_qty) Ltr from oilscheme o,Sales_details sd,sales_master sm,products p where sd.invoice_no=sm.invoice_no and sd.prod_id=p.prod_id and sd.prod_id='"+prod_id+"' and sm.invoice_no='709"+invoice_no+"' and o.pack_type='combo' and o.prodid=sd.prod_id and p.mrp>0 and (cast(floor(cast(datefrom as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ToMMddYYYY(Request.Form["Textbox1"])+"' or cast(floor(cast(dateto as float)) as datetime) between '"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and '"+ ToMMddYYYY(Request.Form["Textbox1"])+"')";
						sdr_detail1=obj1.GetRecordSet(sql);
						while(sdr_detail1.Read())
						{
							if(Name.ToString() != sdr_detail1["Prod_Name"].ToString() && Type.ToString()!=sdr_detail1["Pack_type"].ToString())
							{
								Name+=","+sdr_detail1["Prod_Name"].ToString();
								Type+=","+sdr_detail1["Pack_type"].ToString();
							}
							qty_Nos+=double.Parse(sdr_detail1["qty"].ToString());
							qty_Ltr+=double.Parse(sdr_detail1["Ltr"].ToString());
						}
						sdr_detail1.Close();*/
						
						ProductName=Name.ToString()+":"+Type.ToString()+":"+qty_Nos.ToString()+":"+qty_Ltr.ToString();

						ALFOC_Details.Add(sdr.GetValue(2)+":"+sdr.GetValue(1)+":"+sdr.GetValue(0)+":"+ GenUtil.trimDate(GenUtil.str2DDMMYYYY(sdr.GetValue(3).ToString()))+":"+sdr.GetValue(4)+":"+sdr.GetValue(5)+":"+sdr.GetValue(6)+":"+sdr.GetValue(7)+":"+ProductName);
					}
					sdr.Close();
					cmd.Dispose();
				}
				/*
				SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
				DataSet ds=new DataSet();

				da.Fill(ds,"vw_SaleBook");
				DataTable dtcustomer=ds.Tables["vw_SaleBook"];
				DataView dv=new DataView(dtcustomer);*/
				/***************Start Add by vikas sharma**************************************/

				//string sql1 = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";
				
				/*Coment by vikas 25.10.09 string sql1 = "select cust_name,city,invoice_no,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";

				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
							sql1=sql1+" and cust_type like '"+DropValue.Value+"%'";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							string cust_name="";
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql1=sql1+" and cust_Name='"+cust_name.ToString()+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
							sql1 = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where prod_name like '%(FOC)' and Invoice_No='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
							//02.10.09 sql=sql+" and Category='"+DropValue.Value+"'";
							sql1=sql1+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							sql1=sql1+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
						{
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sql1=sql1+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
							sql1=sql1+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}
				}
				sql1 += ") group by invoice_no,cust_name,city,invoice_date";*/

				/******************Start add by vikas 25.10.09************************************/
				if(dropsummry.SelectedIndex==0)
				{
					if(ChkFoc.Checked!=true)
					{
						string sql1 = " select cust_name,city,invoice_no,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' ";

						if(DropSearchBy.SelectedIndex!=0)
						{
							if(DropSearchBy.SelectedIndex==1)
							{
								if(DropValue.Value!="All")
									sql1=sql1+"  and cust_type like '"+DropValue.Value+"%' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%' and cust_type like '"+DropValue.Value+"%'";
							}
							else if(DropSearchBy.SelectedIndex==2)
							{
								if(DropValue.Value!="All")
								{
									string cust_name="";
									cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
									sql1=sql1+"  and cust_Name='"+cust_name.ToString()+"' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%' and cust_Name='"+cust_name.ToString()+"'";
								}
							}
							else if(DropSearchBy.SelectedIndex==3)
							{
								if(DropValue.Value!="All")
									sql1 = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where prod_name like '%(FOC)' and Invoice_No='"+DropValue.Value+"'";
							}
							else if(DropSearchBy.SelectedIndex==4)
							{
								if(DropValue.Value!="All")
									//02.10.09 sql=sql+" and Category='"+DropValue.Value+"'";
									sql1=sql1+" and Pack_Type='"+DropValue.Value+"' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%' and Pack_Type='"+DropValue.Value+"'";
							}
							else if(DropSearchBy.SelectedIndex==5)
							{
								if(DropValue.Value!="All")
									sql1=sql1+" and Category='"+DropValue.Value+"' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%' and Category='"+DropValue.Value+"'";
							}
							else if(DropSearchBy.SelectedIndex==6)
							{
								if(DropValue.Value!="All")
								{
									string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
									sql1=sql1+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%' and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
								}
							}
							else if(DropSearchBy.SelectedIndex==7)
							{
								if(DropValue.Value!="All")
									sql1=sql1+"  and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
							}

							//}
							//else
							//{
							//sql1 = "select cust_name,city,invoice_no,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and invoice_no not in (select distinct invoice_no from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"'and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";

							/*21.11.2012 if(DropSearchBy.SelectedIndex!=0)
							{
								if(DropSearchBy.SelectedIndex==1)
								{
									if(DropValue.Value!="All")
										sql1=sql1+" and cust_type like '"+DropValue.Value+"%'";
								}
								else if(DropSearchBy.SelectedIndex==2)
								{
									if(DropValue.Value!="All")
									{
										string cust_name="";
										cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
										sql1=sql1+" and cust_Name='"+cust_name.ToString()+"'";
									}
								}
								else if(DropSearchBy.SelectedIndex==3)
								{
									if(DropValue.Value!="All")
										sql1 = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where prod_name like '%(FOC)' and Invoice_No='"+DropValue.Value+"'";
								}
								else if(DropSearchBy.SelectedIndex==4)
								{
									if(DropValue.Value!="All")
										//02.10.09 sql=sql+" and Category='"+DropValue.Value+"'";
										sql1=sql1+" and Pack_Type='"+DropValue.Value+"'";
								}
								else if(DropSearchBy.SelectedIndex==5)
								{
									if(DropValue.Value!="All")
										sql1=sql1+" and Category='"+DropValue.Value+"'";
								}
								else if(DropSearchBy.SelectedIndex==6)
								{
									if(DropValue.Value!="All")
									{
										string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
										sql1=sql1+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
									}
								}
								else if(DropSearchBy.SelectedIndex==7)
								{
									if(DropValue.Value!="All")
										sql1=sql1+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
								}
							} end */
						}

						sql1 += " group by invoice_no,cust_name,city,invoice_date order by invoice_no,cust_name,city,invoice_date";
						/*************End 25.10.09*****************************************/

						SqlCommand cmd= new SqlCommand(sql1,sqlcon);
						SqlDataReader sdr=cmd.ExecuteReader();
						while(sdr.Read())
						{
							ALFOC.Add(sdr.GetValue(2)+":"+sdr.GetValue(1)+":"+sdr.GetValue(0)+":"+ GenUtil.trimDate(GenUtil.str2DDMMYYYY(sdr.GetValue(3).ToString()))+":No FOC: : : ");
						}
						sdr.Close();
						cmd.Dispose();
						ALFOC.Sort();
					}
				}
				/***************End**************************************/
				/*Coment by vikas 24.10.09				
				//dv.Sort=strorderby;
				//Cache["strorderby"]=strorderby;
				GridSalesSummerized.DataSource=ALFOC;
				if(ALFOC.Count!=0)
				{
					GridSalesSummerized.DataBind();
					GridSalesSummerized.Visible=true;
				}
				else
				{
					GridSalesSummerized.Visible=false;
					MessageBox.Show("Data Not Available");
				}
				*/
				ALFOC_Details.Sort();
				sqlcon.Dispose();
				CreateLogFiles.ErrorLog("Form : FOC_Management.aspx,Class:PetrolPumpClass.cs,Method:Bindthedata()    FOC_Management Viewed "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : FOC_Management.aspx,Class:PetrolPumpClass.cs,Method:Bindthedata()    FOC_Management Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		private void GridSalesSummerized_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

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
		
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
         
            if (DateTime.Compare(System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"])), System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["Textbox1"])))>0)
			{
				MessageBox.Show("Date From Should be less than Date To");
			}
			else
			{
				strorderby="invoice_no ASC";
				Session["Column"]="invoice_no";
				Session["order"]="ASC";
				Bindthedata();
			}
			flage=1;
		}

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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\FOC_Management_Report.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click   Sale Book Report   userid  "+uid);
					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
                
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed    EXCEPTION  "+ ane.Message+" userid  "+  uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed  EXCEPTION  "+ se.Message+"  userid  "+  uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed   EXCEPTION "+es.Message+"  userid  "+  uid);
				}
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:BtnPrint_Click, Sales Book Report Printed  EXCEPTION   "+ es.Message+"  userid  "+  uid);
			}
			i=1;
			flage=1;
		}


		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string sql="";
				string info = "";
				string strDate="";
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\FOC_Management_Report.txt";
				StreamWriter sw = new StreamWriter(path);
			
				sw.Write((char)27);
				sw.Write((char)67);
				sw.Write((char)0);
				sw.Write((char)12);
				sw.Write((char)27);
				sw.Write((char)78);
				sw.Write((char)5);
				sw.Write((char)27);
				sw.Write((char)15);

				/*****************Start Coment by vikas 24.10.09 
				//02.10.09 sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"'";
				sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";

				if(DropSearchBy.SelectedIndex!=0)
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						if(DropValue.Value!="All")
							sql=sql+" and cust_type like '"+DropValue.Value+"%'";
					}
					else if(DropSearchBy.SelectedIndex==2)
					{
						if(DropValue.Value!="All")
						{
							string cust_name="";
							cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
							sql=sql+" and cust_Name='"+cust_name.ToString()+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==3)
					{
						if(DropValue.Value!="All")
							sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where prod_name like '%(FOC)' and Invoice_No='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==4)
					{
						if(DropValue.Value!="All")
							//02.10.09 sql=sql+" and Category='"+DropValue.Value+"'";
							sql=sql+" and Pack_Type='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==5)
					{
						if(DropValue.Value!="All")
							sql=sql+" and Category='"+DropValue.Value+"'";
					}
					else if(DropSearchBy.SelectedIndex==6)
					{
						if(DropValue.Value!="All")
						{
							string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
							sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
						}
					}
					else if(DropSearchBy.SelectedIndex==7)
					{
						if(DropValue.Value!="All")
							sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
					}
				}
				sql+=" group by invoice_no,cust_name,city,invoice_date,Prod_Name,Pack_Type";
				sql=sql+" order by "+Cache["strorderby"];
				dbobj.SelectQuery(sql,ref rdr);
				*********end Coment *******************/
				string des="-----------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("FOC MAnagement Report From "+ txtDateFrom.Text.ToString()+" To "+ Textbox1.Text.ToString(),des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("==========================================",des.Length));
				
				/*Coment by vikas 23.10.09
				 * sw.WriteLine("+---------------------------+-----------------+-------+----------+--------------------+-------+----------+------------+");
				sw.WriteLine("|       Customer Name       |      Place      | Invo. | Invoice  |     Prod Name      |  Pack |  Qty in  |   Qty in   |");
				sw.WriteLine("|                           |                 |  No   |  Date    |                    |  Type |    No.   |   Ltr.     |");
				sw.WriteLine("+---------------------------+-----------------+-------+----------+--------------------+-------+----------+------------+"); 
				//             123456789012345678901234567 12345678901234567 1234567 1234567890 1234567890 123456789012 1234567890 12345678901234567*/
				if(dropsummry.SelectedIndex==0)
				{
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+-------+----------+------------+");
					sw.WriteLine("|SN.|       Customer Name       |      Place      | Invo. | Invoice  |     Prod Name      |  Pack |  Qty in  |   Qty in   |");
					sw.WriteLine("|   |                           |                 |  No   |  Date    |                    |  Type |    No.   |   Ltr.     |");
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+-------+----------+------------+"); 
					//             123 123456789012345678901234567 12345678901234567 1234567 1234567890 12345678901234567890 1234567 1234567890 123456789012

					info = " {0,-3:S} {1,-27:S} {2,-17:S} {3,-7:S} {4,-10:S} {5,-20:S} {6,-7:S} {7,10:S} {8,12:S} ";
				}
				else
				{
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+---------+----------+------------+--------------------+-----+----------+------------+");
					sw.WriteLine("|SN.|       Customer Name       |      Place      | Invo. | Invoice  |     Product        |   Pack  |  Qty in  |   Qty in   |         FOC        |Pack |  Qty in  |   Qty in   |");
					sw.WriteLine("|   |                           |                 |  No   |  Date    |     Name           |   Type  |    No.   |   Ltr.     |       Product      |Type |    No.   |   Ltr.     |");
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+---------+----------+------------+--------------------+-----+----------+------------+"); 
					//             123 123456789012345678901234567 12345678901234567 1234567 1234567890 12345678901234567890 123456789 1234567890 123456789012 12345678901234567890 12345 1234567890 123456789012

					info = " {0,-3:S} {1,-27:S} {2,-17:S} {3,-7:S} {4,-10:S} {5,-20:S} {6,-9:S} {7,10:S} {8,12:S} {9,-20:S} {10,-5:S} {11,10:S} {12,12:S} ";
				}

				//Coment by vikas 23.10.09 info = " {0,-27:S} {1,-17:S} {2,-7:S} {3,-10:S} {4,-20:S} {5,-7:S} {6,10:S} {7,12:S}";
				

				/*Coment by vikas 24.10.09 if(rdr.HasRows)
				{
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
						sw.WriteLine(info,i.ToString(),StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),27),
							GenUtil.TrimLength(rdr["City"].ToString().Trim(),17),
							GenUtil.TrimLength(rdr["Invoice_No"].ToString().Trim(),7),
							GenUtil.str2DDMMYYYY(strDate),
							GenUtil.TrimLength(rdr["Prod_Name"].ToString().Trim(),20),
							rdr["Pack_Type"].ToString().Trim(),
							rdr["quant"].ToString().Trim(),
							rdr["totQty"].ToString().Trim()
							);
						TotalQty_No+=double.Parse(rdr["quant"].ToString());
						TotalQty_Ltr+=double.Parse(rdr["TotQty"].ToString());
					i++;	
					}
				}*/
				if(dropsummry.SelectedIndex==0)
				{
					for(int j=0;j<ALFOC.Count;j++)
					{
						string[] arr_value=ALFOC[j].ToString().Split(new char[] {':'});
						sw.WriteLine(info,i.ToString(),
							StringUtil.trimlength(arr_value[2].ToString(),27),
							GenUtil.TrimLength(arr_value[1].ToString(),17),
							GenUtil.TrimLength(arr_value[0].ToString(),7),
							arr_value[3].ToString(),
							GenUtil.TrimLength(arr_value[4].ToString(),20),
							arr_value[5].ToString(),
							arr_value[6].ToString(),
							arr_value[7].ToString());
						i++;
					}
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+-------+----------+------------+"); 
					sw.WriteLine(info,"","Total:","","","","","",TotalQty_No.ToString(),TotalQty_Ltr.ToString());
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+-------+----------+------------+"); 
				}
				else
				{
					for(int j=0;j<ALFOC_Details.Count;j++)
					{
						string[] arr_value=ALFOC_Details[j].ToString().Split(new char[] {':'});
						sw.WriteLine(info,i.ToString(),
							StringUtil.trimlength(arr_value[2].ToString(),27),
							GenUtil.TrimLength(arr_value[1].ToString(),17),
							GenUtil.TrimLength(arr_value[0].ToString(),7),
							arr_value[3].ToString(),
							GenUtil.TrimLength(arr_value[4].ToString(),20),
							arr_value[5].ToString(),
							arr_value[6].ToString(),
							arr_value[7].ToString(),
							GenUtil.TrimLength(arr_value[8].ToString(),20),
							arr_value[9].ToString(),
							arr_value[10].ToString(),
							arr_value[11].ToString());
						i++;
					}
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+---------+----------+------------+--------------------+-----+----------+------------+"); 
					sw.WriteLine(info,"","Total:","","","","","",TotalQty_No.ToString(),TotalQty_Ltr.ToString(),"","",TotalQty_No_main.ToString(),TotalQty_Ltr_main.ToString());
					sw.WriteLine("+---+---------------------------+-----------------+-------+----------+--------------------+---------+----------+------------+--------------------+-----+----------+------------+"); 
				}
				
				dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Method:makingReport().  EXCEPTION "+ ex.Message+" userid "+  uid);
			}
		}

		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\FOC_Management_Report1.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="",strDate="";  
			/*Coment by vikas 24.10.09
			//sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"'";
			sql = "select cust_name,city,invoice_no,invoice_date,Prod_Name,Pack_Type,sum(quant) quant,sum(quant*total_qty) totqty from vw_salebook where cast(floor(cast(invoice_date as float)) as datetime)>='"+ ToMMddYYYY(Request.Form["txtDateFrom"])  +"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+ ToMMddYYYY(Request.Form["Textbox1"]) +"' and prod_name like '%FOC%'";
			if(DropSearchBy.SelectedIndex!=0)
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					if(DropValue.Value!="All")
						sql=sql+" and cust_type like '"+DropValue.Value+"%'";
				}
				else if(DropSearchBy.SelectedIndex==2)
				{
					if(DropValue.Value!="All")
					{
						string cust_name="";
						cust_name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql=sql+" and cust_Name='"+cust_name.ToString()+"'";
					}
				}
				else if(DropSearchBy.SelectedIndex==3)
				{
					if(DropValue.Value!="All")
						sql="select invoice_no,cust_name,city,invoice_date,sum(quant) quant,sum(quant*total_qty) totqty,discount,net_amount from vw_salebook where Invoice_No='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==4)
				{
					if(DropValue.Value!="All")
						sql=sql+" and Category='"+DropValue.Value+"'";
				}
				else if(DropSearchBy.SelectedIndex==5)
				{
					if(DropValue.Value!="All")
					{
						string[] str = DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sql=sql+" and Prod_Name='"+str[0]+"' and Pack_Type='"+str[1]+"'";
					}
				}
				else if(DropSearchBy.SelectedIndex==6)
				{
					if(DropValue.Value!="All")
						sql=sql+" and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"')";
				}
			}
			sql+=" group by invoice_no,cust_name,city,invoice_date,Prod_Name,Pack_Type";
			sql=sql+" order by "+Cache["strorderby"];
			rdr=obj.GetRecordSet(sql);*/

			sw.WriteLine("From Date\t"+Request.Form["txtDateFrom"]);
			sw.WriteLine("To Date\t"+Request.Form["Textbox1"]);
			sw.WriteLine();
			
			
			/*Coment by vikas 24.10.09
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
					//
					// * StringUtil.trimlength(rdr["Cust_Name"].ToString().Trim(),27),
					//		GenUtil.TrimLength(rdr["City"].ToString().Trim(),17),
					//		GenUtil.TrimLength(rdr["Invoice_No"].ToString().Trim(),7),
					//		GenUtil.str2DDMMYYYY(strDate),
					//		rdr["Prod_Name"].ToString().Trim(),
					//		rdr["Pack_Type"].ToString().Trim(),
					//		rdr["quant"].ToString().Trim(),
					//		rdr["totQty"].ToString().Trim()
					// * 
				sw.WriteLine(i.ToString().Trim()+"\t"+
					rdr["Cust_Name"].ToString().Trim()+"\t"+
					rdr["City"].ToString().Trim()+"\t"+
					rdr["Invoice_No"].ToString().Trim()+"\t"+
					GenUtil.str2DDMMYYYY(strDate)+"\t"+
					(rdr["Prod_Name"].ToString())+"\t"+
					rdr["Pack_Type"].ToString()+"\t"+
					rdr["quant"].ToString().Trim()+"\t"+
					rdr["totQty"].ToString()
					);
				TotalQty_No+=double.Parse(rdr["quant"].ToString());
				TotalQty_Ltr+=double.Parse(rdr["totQty"].ToString());
				//TotalNet_Amount+=double.Parse(rdr["Net_Amount"].ToString());
				i++;
			}*/
			if(dropsummry.SelectedIndex==0)
			{
				sw.WriteLine("Sr.No.\tCustomer Name\tPlace\tInvoice No\tInvoice Date\tProduct Name\tPack Type\tQty in No.\tQty in ltr.\t");
				for(int j=0;j<ALFOC.Count;j++)
				{
					string[] arr_value=ALFOC[j].ToString().Split(new char[] {':'});
					sw.WriteLine(i.ToString()+"\t"+
						StringUtil.trimlength(arr_value[2].ToString(),27)+"\t"+
						arr_value[1].ToString()+"\t"+
						arr_value[0].ToString()+"\t"+
						arr_value[3].ToString()+"\t"+
						arr_value[4].ToString()+"\t"+
						arr_value[5].ToString()+"\t"+
						arr_value[6].ToString()+"\t"+
						arr_value[7].ToString());
					i++;
				}
				sw.WriteLine("Total\t\t\t\t\t\t\t"+TotalQty_No.ToString()+"\t"+TotalQty_Ltr.ToString());
			}
			else
			{
				sw.WriteLine("Sr.No.\tCustomer Name\tPlace\tInvoice No\tInvoice Date\tProduct Name\tPack Type\tQty in Nos\tQty in Ltr\tFOC Product\tFOC Pack Type\tQty in No.\tQty in ltr.\t");
				for(int j=0;j<ALFOC_Details.Count;j++)
				{
					string[] arr_value=ALFOC_Details[j].ToString().Split(new char[] {':'});
					sw.WriteLine(i.ToString()+"\t"+
						StringUtil.trimlength(arr_value[2].ToString(),27)+"\t"+
						arr_value[1].ToString()+"\t"+
						arr_value[0].ToString()+"\t"+
						arr_value[3].ToString()+"\t"+
						arr_value[4].ToString()+"\t"+
						arr_value[5].ToString()+"\t"+
						arr_value[6].ToString()+"\t"+
						arr_value[7].ToString()+"\t"+
						arr_value[8].ToString()+"\t"+
						arr_value[9].ToString()+"\t"+
						arr_value[10].ToString()+"\t"+
						arr_value[11].ToString());
					i++;
				}
				sw.WriteLine("Total\t\t\t\t\t\t\t"+TotalQty_No+"\t"+TotalQty_Ltr+"\t\t\t"+TotalQty_No_main.ToString()+"\t"+TotalQty_Ltr_main.ToString());
			}
			sw.Close();
		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ConvertToExcel();
				MessageBox.Show("Successfully Convert File Into Excel Format");
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Convert Into Excel Format, userid  "+uid);
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:SaleBook.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   SaleBook Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
			i=1;
			flage=1;
		}
	}
}
