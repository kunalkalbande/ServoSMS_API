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
using DBOperations; 

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SRSM_Format_Report.
	/// </summary>
	public partial class SRSM_Format_Report : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public string uid;
		public string StartDate="",EndDate="",CM_StartDate="",LM_StartDate;
		public int ds11=0;
		public int ds12=0;
		public int ds21=0;
		public int ds22=0;
		public int ds10=0;
		public int ds20=0;
		public static int View = 0;
		public string[] DateFrom = null;
		public string[] DateFrom1 = null;
		public string[] DateTo = null;
		public string[] DateTo1 = null;
		public static int count=0;
		public static int count1=0;
		public double[] TotalSum = null;
		public double[] TotalSum1 = null;
		public SqlDataReader dtr=null;
		public SqlDataReader dtr1=null;
		public int i;
		public int ii;
		public string sql="";
		public double Total_2t=0,Total_Other=0,Total_Tot=0; 
		
		public string s1="";
		public string s2="";
		
		public string Cust_Name="";

		public double LY_P_Sale=0;
		public double CY_P_Sale=0;
		public double Target=0;
		public double Growth=0;
		public double GR_Per=0;
		public double Achiv=0;
		public double Poten_Per=0;
		public double Cumu_LY_P_Sale=0;
		public double Cumu_CY_P_Sale=0;
		public double Cumu_Target=0;
		public double Cumu_Growth=0;
		public double Cumu_GR_Per=0;
		public double Cumu_Achiv=0;
		public double Cumu_Poten_Per=0;

		public double NKR_LY_Sale=0;
		public double NKR_CY_Sale=0;
		public double KR_LY_Sale=0;
		public double KR_CY_Sale=0;
		public double ERO_LY_Sale=0;
		public double ERO_CY_Sale=0;
		public double Tot_LY_Sale=0;
		public double Tot_CY_Sale=0;
		public double Cumu_NKR_LY_Sale=0;
		public double Cumu_NKR_CY_Sale=0;
		public double Cumu_KR_LY_Sale=0;
		public double Cumu_KR_CY_Sale=0;
		public double Cumu_ERO_LY_Sale=0;
		public double Cumu_ERO_CY_Sale=0;
		public double Cumu_Tot_LY_Sale=0;
		public double Cumu_Tot_CY_Sale=0;

		public double RO_LY_Sale=0;
		public double RO_CY_Sale=0;
		public double BAZ_LY_Sale=0;
		public double BAZ_CY_Sale=0;
		public double OE_LY_Sale=0;
		public double OE_CY_Sale=0;
		public double FLEET_LY_Sale=0;
		public double FLEET_CY_Sale=0;
		public double Tot_LY_Sale3B=0;
		public double Tot_CY_Sale3B=0;
		public double Cumu_RO_LY_Sale=0;
		public double Cumu_RO_CY_Sale=0;
		public double Cumu_BAZ_LY_Sale=0;
		public double Cumu_BAZ_CY_Sale=0;
		public double Cumu_OE_LY_Sale=0;
		public double Cumu_OE_CY_Sale=0;
		public double Cumu_FLEET_LY_Sale=0;
		public double Cumu_FLEET_CY_Sale=0;
		public double Cumu_Tot_LY_Sale3B=0;
		public double Cumu_Tot_CY_Sale3B=0;

		public double NKR_LY_Sale_4=0;
		public double NKR_CY_Sale_4=0;
		public double NKR_Tot_Sale_4=0;
		public double KR_LY_Sale_4=0;
		public double KR_CY_Sale_4=0;
		public double KR_Tot_Sale_4=0;
		public double BAZ_LY_Sale_4=0;
		public double BAZ_CY_Sale_4=0;
		public double BAZ_Tot_Sale_4=0;
		public double OE_LY_Sale_4=0;
		public double OE_CY_Sale_4=0;
		public double OE_Tot_Sale_4=0;
		public double FLEET_LY_Sale_4=0;
		public double FLEET_CY_Sale_4=0;
		public double FLEET_Tot_Sale_4=0;
		public double IBP_LY_Sale_4=0;
		public double IBP_CY_Sale_4=0;
		public double IBP_Tot_Sale_4=0;
		public double Tot_LY_Sale_4=0;
		public double Tot_CY_Sale_4=0;
		public double Tot_Sale_4=0;

		public double MS_Sale=0;
		public double HSD_Sale=0;
		public double RO_Lube=0;
		public double RO_LFR=0;
		public double RO_2T=0;
		public double RO_4T=0;
		public double MS_2T_4T=0;
		public double Cumu_MS_Sale=0;
		public double Cumu_HSD_Sale=0;
		public double Cumu_RO_Lube=0;
		public double Cumu_RO_LFR=0;
		public double Cumu_RO_2T=0;
		public double Cumu_RO_4T=0;
		public double Cumu_MS_2T_4T=0;
		
		public double Total=0;

		public double CY_Small_M=0;
		public double CY_Barel_M=0;
		public double CY_Tot_M=0;
		public double LY_Small_M=0;
		public double LY_Barel_M=0;
		public double LY_Tot_M=0;
		public double CY_Small_C=0;
		public double CY_Barel_C=0;
		public double CY_Tot_C=0;
		public double LY_Small_C=0;
		public double LY_Barel_C=0;
		public double LY_Tot_C=0;

		public double RO_OS=0;
		public double BAZZAR_OS=0;
		public double OE_OS=0;
		public double FLEET_OS=0;
		public double TOTAL_OS=0;
		public double Closing_Stock=0;
		public double NODC=0;

		public double[] PS_Barel;
		public double[] PS_Small;
		public double[] PS_Tot;
		public double[] PS_Claim;
		public double[] Trade_Disc;
		public double[] Cash_Disc;
		public double[] EB_Disc;
		public double[] Fixed_Disc;
		public double[] baral_Disc;
		public double[] Tot_SSA_Inc1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
				
			}
			if(!IsPostBack)
			{
				DropYear.SelectedIndex= DropYear.Items.IndexOf(DropYear.Items.FindByValue(DateTime.Now.Year.ToString()));

				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="54";
				string[,] Priv=(string[,]) Session["Privileges"];
								
				for(i=0;i<Priv.GetLength(0);i++)
				{
					if(Priv[i,0] == Module && Priv[i,1] == SubModule)
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

		public double Month_CY_Sale_SRSM_6A;
		public double Month_CY_Sale(string prod_id)
		{
			try
			{
				Month_CY_Sale_SRSM_6A=0;
				InventoryClass obj=new InventoryClass();
				sql="select sum(quant*total_qty) sale from vw_salebook where prod_id='"+prod_id.ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						Month_CY_Sale_SRSM_6A=double.Parse(dtr["sale"].ToString());
					else
						Month_CY_Sale_SRSM_6A=0;
				}
				dtr.Close();
				Month_CY_Sale_SRSM_6A=Math.Round((Month_CY_Sale_SRSM_6A/1000),2);
				return Month_CY_Sale_SRSM_6A;
			}
			catch(Exception ex)
			{
				return Month_CY_Sale_SRSM_6A;
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
			
		}
	
		public double Month_LY_Sale_SRSM_6A;
		public double Month_LY_Sale(string prod_id)
		{
			try
			{
				Month_LY_Sale_SRSM_6A=0;
				InventoryClass obj=new InventoryClass();
				sql="select sum(quant*total_qty) sale from vw_salebook where prod_id='"+prod_id.ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						Month_LY_Sale_SRSM_6A=double.Parse(dtr["sale"].ToString());
					else
						Month_LY_Sale_SRSM_6A=0;
				}
				dtr.Close();
				Month_LY_Sale_SRSM_6A=Math.Round((Month_LY_Sale_SRSM_6A/1000),2);
				return Month_LY_Sale_SRSM_6A;
			}
			catch(Exception ex)
			{
				return Month_LY_Sale_SRSM_6A;
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public double Cumu_CY_Sale_SRSM_6A;
		public double Cumu_CY_Sale(string prod_id)
		{
			try
			{
				Cumu_CY_Sale_SRSM_6A=0;
				InventoryClass obj=new InventoryClass();
				sql="select sum(quant*total_qty) sale from vw_salebook where prod_id='"+prod_id.ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						Cumu_CY_Sale_SRSM_6A=double.Parse(dtr["sale"].ToString());
					else
						Cumu_CY_Sale_SRSM_6A=0;
						
				}
				dtr.Close();
				Cumu_CY_Sale_SRSM_6A=Math.Round((Cumu_CY_Sale_SRSM_6A/1000),2);
				return Cumu_CY_Sale_SRSM_6A;
			}
			catch(Exception ex)
			{
				return Cumu_CY_Sale_SRSM_6A;
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
			
		}
	
		public double Cumu_LY_Sale_SRSM_6A;
		public double Cumu_LY_Sale(string prod_id)
		{
			try
			{
				Cumu_LY_Sale_SRSM_6A=0;
				InventoryClass obj=new InventoryClass();
				sql="select sum(quant*total_qty) sale from vw_salebook where prod_id='"+prod_id.ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						Cumu_LY_Sale_SRSM_6A=double.Parse(dtr["sale"].ToString());
					else
						Cumu_LY_Sale_SRSM_6A=0;
				}
				dtr.Close();
				Cumu_LY_Sale_SRSM_6A=Math.Round((Cumu_LY_Sale_SRSM_6A/1000),2);
				return Cumu_LY_Sale_SRSM_6A;
				
			}
			catch(Exception ex)
			{
				return Cumu_LY_Sale_SRSM_6A;
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public void SRSM_9()
		{
			try
			{
				
				PS_Barel=new double[DateFrom.Length];
				PS_Small=new double[DateFrom.Length];
				PS_Tot=new double[DateFrom.Length];
				PS_Claim=new double[DateFrom.Length];
				Trade_Disc=new double[DateFrom.Length];
				Cash_Disc=new double[DateFrom.Length];
				EB_Disc=new double[DateFrom.Length];
				Fixed_Disc=new double[DateFrom.Length];
				baral_Disc=new double[DateFrom.Length];
				Tot_SSA_Inc1=new double[DateFrom.Length];

				double temp=0; 
				InventoryClass obj=new InventoryClass();
				for(int m=0;m<DateFrom.Length;m++)
				{
					sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and p.total_qty>=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[m].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[m].ToString()+"'";
					dtr=obj.GetRecordSet(sql);
					if(dtr.Read())
					{
						if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
							temp=double.Parse(dtr["sale"].ToString());
						else
							temp=0;
					
						temp=Math.Round((temp/1000),2);
					}
					dtr.Close();
					PS_Barel[m]=temp;
				}

				for(int m=0;m<DateFrom.Length;m++)
				{
					sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and p.total_qty<50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[m].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[m].ToString()+"'";
					dtr=obj.GetRecordSet(sql);
					if(dtr.Read())
					{
						if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
							temp=double.Parse(dtr["sale"].ToString());
						else
							temp=0;
					
						temp=Math.Round((temp/1000),2);
					}
					dtr.Close();
					PS_Small[m]=temp;
				}

				for(int m=0;m<DateFrom.Length;m++)
				{
					sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+DateFrom[m].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[m].ToString()+"'";
					dtr=obj.GetRecordSet(sql);
					if(dtr.Read())
					{
						if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
							temp=double.Parse(dtr["sale"].ToString());
						else
							temp=0;
					
						temp=Math.Round((temp/1000),2);
					}
					dtr.Close();
					PS_Tot[m]=temp;
				}
				
				for(int m=0;m<DateFrom.Length;m++)
				{
					PS_Claim[m]=Math.Round(SC_incentive(DateFrom[m].ToString(),DateTo[m].ToString()));
				}
				
				for(int m=0;m<DateFrom.Length;m++)
				{
					sql="select sum(trade_discount) Trade_Disc,sum(ebird_discount) EB_Disc, sum((case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end)) as Cash_Disc, sum((case when discount_type = 'Per' then ((Grand_Total-(case when cash_disc_type = 'Per' then (Grand_Total*Cash_Discount/100) else Cash_Discount end))+VAT_Amount)*Discount/100 else Discount end)) as Disc, sum(cast(fixed_disc_amount as float)) Fixed_Disc from Purchase_Master p, Supplier s where s.Supp_ID = p.Vendor_ID and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+DateFrom[m]+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[m]+"'";
					dtr=obj.GetRecordSet(sql);
					if(dtr.Read())
					{
						temp=0;
						if(dtr["Trade_Disc"].ToString()!=null && dtr["Trade_Disc"].ToString()!="")
							temp=double.Parse(dtr["Trade_Disc"].ToString());
						else
							temp=0;
						temp=Math.Round(temp);
						Trade_Disc[m]=temp;
						Tot_SSA_Inc1[m]+=temp;

						temp=0;
						if(dtr["EB_Disc"].ToString()!=null && dtr["EB_Disc"].ToString()!="")
							temp=double.Parse(dtr["EB_Disc"].ToString());
						else
							temp=0;
						temp=Math.Round(temp);
						EB_Disc[m]=temp;
						Tot_SSA_Inc1[m]+=temp;

						temp=0;
						if(dtr["Cash_Disc"].ToString()!=null && dtr["Cash_Disc"].ToString()!="")
							temp=double.Parse(dtr["Cash_Disc"].ToString());
						else
							temp=0;
						temp=Math.Round(temp);
						Cash_Disc[m]=temp;
						Tot_SSA_Inc1[m]+=temp;

						temp=0;
						if(dtr["Fixed_Disc"].ToString()!=null && dtr["Fixed_Disc"].ToString()!="")
							temp=double.Parse(dtr["Fixed_Disc"].ToString());
						else
							temp=0;
						temp=Math.Round(temp);
						Fixed_Disc[m]=temp;
						Tot_SSA_Inc1[m]+=temp;
						
					}
					dtr.Close();
				}

				for(int m=0;m<DateFrom.Length;m++)
				{
					sql="select sum((case when perdisctype = '%' then (amount*perdisc/100) else (total_qty*perdisc*qty) end)) as Discount from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and p.total_qty>=50 and cast(floor(cast(Invoice_date as float)) as datetime) >= '"+DateFrom[m].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[m].ToString()+"'";
					dtr=obj.GetRecordSet(sql);
					if(dtr.Read())
					{
						temp=0;
						if(dtr["Discount"].ToString()!=null && dtr["Discount"].ToString()!="")
							temp=double.Parse(dtr["Discount"].ToString());
						else
							temp=0;
						temp=Math.Round(temp);
						baral_Disc[m]=temp;
						Tot_SSA_Inc1[m]+=temp;
					}
					dtr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
		
		private DateTime getdate(string dat,bool to)
		{
			string[] dt=dat.IndexOf("/")>0? dat.Split(new char[]{'/'},dat.Length): dat.Split(new char[] { '-' }, dat.Length);
			if(to)
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
			else
				return new DateTime(Int32.Parse(dt[2]),Int32.Parse(dt[1]),Int32.Parse(dt[0]));
		}

		public double SC_incentive(string datefrom,string dateto)
		{
			int x=0;
			object op=null;	
			SqlDataReader rdr=null;
			sql="select distinct productid from stock_master";
			dbobj.SelectQuery(sql,ref rdr);
			try
			{
				while(rdr.Read())
				{
					dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@strfromdate",getdate(GenUtil.str2MMDDYYYY(datefrom.ToString()),true).Date.ToShortDateString(),"@strtodate",getdate(GenUtil.str2MMDDYYYY(dateto.ToString()),true).Date.ToShortDateString());
					count++;
				}
				rdr.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("stock : "+ex+"  "+count);
			}
				
			double sale=0;
			double foc_sale=0;
			double discount=0;
			string prod_id="";
			double Tot_Claim=0;
			double amount=0;
			string pack_type="";
			InventoryClass obj3=new InventoryClass();
			SqlDataReader dtr3=null;
			sql="select * from stkmv s,products p where p.prod_id=s.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
			dtr3=obj3.GetRecordSet(sql);
			while(dtr3.Read())
			{	
				discount=0;
				amount=0;
				pack_type=dtr3["pack_type"].ToString();
				string[] pack=pack_type.Split(new char[] {'X'});
				sale=double.Parse(dtr3["sales"].ToString());
				sale=Math.Round((double.Parse(pack[0].ToString())*double.Parse(pack[1].ToString())*sale),2);
				foc_sale=double.Parse(dtr3["salesfoc"].ToString());
				prod_id=dtr3["prod_id"].ToString();
				sql="select discount from oilscheme o where prodid='"+prod_id+"' and schname='Secondry(LTR Scheme)' and  (cast(floor(cast(datefrom as float)) as datetime)>='"+datefrom.ToString().Trim()+"' and cast(floor(cast(dateto as float)) as datetime)<='"+dateto.ToString().Trim()+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+datefrom.ToString().Trim()+"' and '"+dateto.ToString().Trim()+"' or cast(floor(cast(dateto as float)) as datetime) between '"+datefrom.ToString().Trim()+"' and '"+dateto.ToString().Trim()+"')";
				dbobj.SelectQuery(sql,ref rdr);
				if(rdr.Read())
				{
					discount=System.Convert.ToDouble(rdr.GetValue(0).ToString());
				}
				rdr.Close();
				amount=Math.Round(((sale-foc_sale)*discount),2);
				Tot_Claim+=amount;
			}
			dtr3.Close();
			
			dbobj.Insert_or_Update("truncate table stkmv", ref x);

			return Tot_Claim;
		}

		public void SRSM_10_12()
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty>=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						LY_Barel_C=double.Parse(dtr["sale"].ToString());
					else
						LY_Barel_C=0;
					
					LY_Barel_C=Math.Round((LY_Barel_C/1000),2);
					LY_Tot_C+=LY_Barel_C;
				}
				dtr.Close();
				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty<=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						LY_Small_C=double.Parse(dtr["sale"].ToString());
					else
						LY_Small_C=0;
					
					LY_Small_C=Math.Round((LY_Small_C/1000),2);
					LY_Tot_C+=LY_Small_C;
				}
				dtr.Close();

				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty>=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						CY_Barel_C=double.Parse(dtr["sale"].ToString());
					else
						CY_Barel_C=0;
					
					CY_Barel_C=Math.Round((CY_Barel_C/1000),2);
					CY_Tot_C+=CY_Barel_C;
				}
				dtr.Close();
				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty<=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						CY_Small_C=double.Parse(dtr["sale"].ToString());
					else
						CY_Small_C=0;
					
					CY_Small_C=Math.Round((CY_Small_C/1000),2);
					CY_Tot_C+=CY_Small_C;
				}
				dtr.Close();



				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty>=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						LY_Barel_M=double.Parse(dtr["sale"].ToString());
					else
						LY_Barel_M=0;
					
					LY_Barel_M=Math.Round((LY_Barel_M/1000),2);
					LY_Tot_M+=LY_Barel_M;
				}
				dtr.Close();
				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty<=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						LY_Small_M=double.Parse(dtr["sale"].ToString());
					else
						LY_Small_M=0;
					
					LY_Small_M=Math.Round((LY_Small_M/1000),2);
					LY_Tot_M+=LY_Small_M;
				}
				dtr.Close();

				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty>=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						CY_Barel_M=double.Parse(dtr["sale"].ToString());
					else
						CY_Barel_M=0;
					
					CY_Barel_M=Math.Round((CY_Barel_M/1000),2);
					CY_Tot_M+=CY_Barel_M;
				}
				dtr.Close();
				sql="select sum(total_qty*qty) sale from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and pd.prod_id=p.prod_id and total_qty<=50 and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						CY_Small_M=double.Parse(dtr["sale"].ToString());
					else
						CY_Small_M=0;
					
					CY_Small_M=Math.Round((CY_Small_M/1000),2);
					CY_Tot_M+=CY_Small_M;
				}
				dtr.Close();

				/*""
				""
				""
				""
				"select sum(Balance) Balance from custout where balancetype='Dr.'"*/

				sql="select sum(Balance) sale from custout where cust_type in (select customertypename from customertype where group_name like 'Bazzar%') and balancetype='Dr.'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						BAZZAR_OS=double.Parse(dtr["sale"].ToString());
					else
						BAZZAR_OS=0;
					
					BAZZAR_OS=Math.Round((BAZZAR_OS));
					TOTAL_OS+=BAZZAR_OS;
				}
				dtr.Close();
				sql="select sum(Balance) sale from custout where cust_type in (select customertypename from customertype where group_name Like '%RO%') and balancetype='Dr.'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						RO_OS=double.Parse(dtr["sale"].ToString());
					else
						RO_OS=0;
					
					RO_OS=Math.Round((RO_OS));
					TOTAL_OS+=RO_OS;
				}
				dtr.Close();
				sql="select sum(Balance) sale from custout where cust_type in (select customertypename from customertype where group_name like '%OE%') and balancetype='Dr.'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						OE_OS=double.Parse(dtr["sale"].ToString());
					else
						OE_OS=0;
					
					OE_OS=Math.Round((OE_OS));
					TOTAL_OS+=OE_OS;
				}
				dtr.Close();
				sql="select sum(Balance) sale from custout where cust_type in (select customertypename from customertype where group_name like '%Fleet%') and balancetype='Dr.'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					if(dtr["sale"].ToString()!=null && dtr["sale"].ToString()!="")
						FLEET_OS=double.Parse(dtr["sale"].ToString());
					else
						FLEET_OS=0;
					
					FLEET_OS=Math.Round((FLEET_OS));
					TOTAL_OS+=FLEET_OS;
				}
				dtr.Close();

				string qty="";
				sql="select a.pack_type,a.closing_stock from vw_stockreport a , stk b where a.product=b.product and a.stock_date=b.sdate";
				dtr=obj.GetRecordSet(sql);
				while(dtr.Read())
				{
					if(dtr["pack_type"].ToString()!=null && dtr["pack_type"].ToString()!="")
						qty=dtr["pack_type"].ToString()+"X"+dtr["closing_stock"].ToString();
					else
						qty="0X0X0";
					
					Closing_Stock+=double.Parse(Multiply(qty).ToString());
				}
				dtr.Close();
				Closing_Stock=Math.Round((Closing_Stock/1000),2);

				double Rate=0;
				if(txtvalue.Text.ToString()!="")
					Rate=double.Parse(txtvalue.Text.ToString());
				else
					Rate=0;

				if(Rate!=0)
					NODC=Math.Round((Closing_Stock/(Rate/30)));
				else
					NODC=0;
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public double cs=0;
		protected string Multiply(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				//if(count==i)
				cs+=ans;
				Cache["cs"]=System.Convert.ToString(cs);
				//i++;
				//count++;
				//**********************
				return ans.ToString() ;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//******
					cs+=System.Convert.ToDouble(mystr[0].ToString());
					Cache["cs"]=System.Convert.ToString(cs);
					//********
					return System.Convert.ToDouble( mystr[0].ToString()).ToString() ; 
				}
				else
					return "0";
			}
		}

		public void SRSM14()
		{
			try
			{
				s1="";
				s2="";
			
				s2=GenUtil.str2MMDDYYYY(StartDate.ToString());
				s1=GenUtil.str2MMDDYYYY(EndDate.ToString());

				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 = s1.IndexOf("/")>0?s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				int Toyear=Convert.ToInt32(ds2[2]);
				int Fromyear=Convert.ToInt32(ds1[2]);
				Toyear--;
				Fromyear--;
				s1=ds1[1]+"/"+ds1[0]+"/"+Fromyear.ToString();
				int day=DateTime.DaysInMonth(Toyear,Convert.ToInt32(ds2[1]));
				s2=ds2[1]+"/"+day+"/"+Toyear.ToString();

				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					View=0;
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					View=0;
					return;
				}
				if((ds22-ds12) > 1)
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}

				getDate(ds10,ds11,ds12,ds20,ds21,ds22);          //add by mahesh
				getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22);

			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_14()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
			//InventoryClass obj=new InventoryClass();
			//sql="select cust_type,cust_name,city from customer where cust_type='ESSAR RO' order by cust_name";
			//dtr=obj.GetRecordSet(sql);
		}

		public void SRSM_1_4()
		{
			try
			{
				string s1="";
				string s2="";
				s2=GenUtil.str2MMDDYYYY(StartDate.ToString());
				s1=GenUtil.str2MMDDYYYY(EndDate.ToString());
				string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length) : s2.Split(new char[] { '-' }, s2.Length);
				string[] ds2 =s1.IndexOf("-")>0? s1.Split(new char[] {'/'},s1.Length) : s1.Split(new char[] { '-' }, s1.Length);
				ds10=System.Convert.ToInt32(ds1[0]);
				ds20=System.Convert.ToInt32(ds2[0]);
				ds11=System.Convert.ToInt32(ds1[1]);
				ds12=System.Convert.ToInt32(ds1[2]);
				ds21=System.Convert.ToInt32(ds2[1]);
				ds22=System.Convert.ToInt32(ds2[2]);
				int Toyear=Convert.ToInt32(ds2[2]);
				int Fromyear=Convert.ToInt32(ds1[2]);
			
				CM_StartDate=ds2[1]+"/1/"+Toyear;
			
				Toyear--;
				Fromyear--;
				s1=ds1[1]+"/"+ds1[0]+"/"+Fromyear.ToString();
				int day=DateTime.DaysInMonth(Toyear,Convert.ToInt32(ds2[1]));
				s2=ds2[1]+"/"+day+"/"+Toyear.ToString();
			
				LM_StartDate=ds2[1]+"/1/"+Toyear;
				//CM_StartDate=ds2[1]+"/1/"+Toyear;

				if(ds12==ds22 && ds11 > ds21)
				{
					MessageBox.Show("Please Select Greater Month in DateTo");
					View=0;
					return;
				}
				if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
				{
					MessageBox.Show("Please Select Greater Date");
					View=0;
					return;
				}
				if((ds22-ds12) > 1)
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}
				if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
				{
					MessageBox.Show("Please Select date between one year");
					View=0;
					return;
				}

				getDate(ds10,ds11,ds12,ds20,ds21,ds22);
				getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22);

				#region Get Data for SRSM 1
				/********SRSM 1***************/
				InventoryClass obj1=new InventoryClass();
				sql="select sum(totalqtyltr) sale from Purchase_Master where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_CY_P_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_CY_P_Sale=Math.Round((Cumu_CY_P_Sale/1000),2);
				}
				dtr1.Close();
				sql="select sum(totalqtyltr) sale from Purchase_Master where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_LY_P_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_LY_P_Sale=Math.Round((Cumu_LY_P_Sale/1000),2);
				}
				dtr1.Close();
				Cumu_Growth=Math.Round(Cumu_CY_P_Sale-Cumu_LY_P_Sale,1);
				Cumu_Target= Math.Round((Cumu_LY_P_Sale*1.1),1);
				Cumu_GR_Per=Math.Round(((Cumu_Growth/Cumu_LY_P_Sale)*100),1);
				Cumu_Achiv=Math.Round(((Cumu_CY_P_Sale/Cumu_Target)*100),1);
				Cumu_Poten_Per=Math.Round((Cumu_CY_P_Sale/100),1);
				sql="select sum(totalqtyltr) sale from Purchase_Master where cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						CY_P_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					CY_P_Sale=Math.Round((CY_P_Sale/1000),2);
				}
				dtr1.Close();
				sql="select sum(totalqtyltr) sale from Purchase_Master where cast(floor(cast(invoice_date as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						LY_P_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					LY_P_Sale=Math.Round((LY_P_Sale/1000),2);
				}
				dtr1.Close();
				Growth=Math.Round(CY_P_Sale-LY_P_Sale,1);
				Target= Math.Round((LY_P_Sale*1.1),1);
				GR_Per=Math.Round(((Growth/LY_P_Sale)*100),1);
				Achiv=Math.Round(((CY_P_Sale/Target)*100),1);
				Poten_Per=Math.Round((CY_P_Sale));
				/********SRSM 1***************/
				#endregion

				#region Get Data for SRSM 2
				/********SRSM 2***************/
				//select sum(cast(MS as float)) SM, sum(cast(HSD as float)) HSD from cust_sale_ms_hsd where cast(floor(cast(Datefrom as float)) as datetime)<='"+StartDate.ToString()+"' and '"+EndDate.ToString()+"' <=cast(floor(cast(dateto as float)) as datetime)
				sql=" select sum(cast(MS as float)) MS, sum(cast(HSD as float)) HSD from cust_sale_ms_hsd where cast(floor(cast(Datefrom as float)) as datetime)<='"+StartDate.ToString()+"' and '"+EndDate.ToString()+"' <=cast(floor(cast(dateto as float)) as datetime)";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{

					if(dtr1["MS"].ToString()!=null && dtr1["MS"].ToString()!="")
						MS_Sale=Math.Round(double.Parse(dtr1["MS"].ToString()),1);

					if(dtr1["HSD"].ToString()!=null && dtr1["HSD"].ToString()!="")
						HSD_Sale=Math.Round(double.Parse(dtr1["HSD"].ToString()),1);
				}
				dtr1.Close();
				sql=" select sum(cast(MS as float)) MS, sum(cast(HSD as float)) HSD from cust_sale_ms_hsd where cast(floor(cast(Datefrom as float)) as datetime)<='"+StartDate.ToString()+"' and '"+EndDate.ToString()+"' <=cast(floor(cast(dateto as float)) as datetime)";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["MS"].ToString()!=null && dtr1["MS"].ToString()!="")
						Cumu_MS_Sale=Math.Round(double.Parse(dtr1["MS"].ToString()),1);

					if(dtr1["HSD"].ToString()!=null && dtr1["HSD"].ToString()!="")
						Cumu_HSD_Sale=Math.Round(double.Parse(dtr1["HSD"].ToString()),1);
				}
				dtr1.Close();

				sql="select sum(v.totalqty) lube,sum(v.oil2t) t2,sum(v.oil4t) t4 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name like 'RO') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["t2"].ToString()!=null && dtr1["t2"].ToString()!="")
						RO_2T=double.Parse(dtr1["t2"].ToString());
					
					RO_2T=Math.Round((RO_2T/1000),2);

					if(dtr1["t4"].ToString()!=null && dtr1["t4"].ToString()!="")
						RO_4T=double.Parse(dtr1["t4"].ToString());

					RO_4T=Math.Round((RO_4T/1000),2);

					if(dtr1["lube"].ToString()!=null && dtr1["lube"].ToString()!="")
						RO_Lube=double.Parse(dtr1["lube"].ToString());

					RO_Lube=Math.Round((RO_Lube/1000),2);
				}
				dtr1.Close();
				
				if(HSD_Sale!=0 || MS_Sale!=0)
				{
					RO_LFR=Math.Round((RO_Lube/(HSD_Sale+MS_Sale))*100,2);
				}
				else
				{
					RO_LFR=0;
				}

				if(MS_Sale!=0)
				{
					MS_2T_4T=Math.Round(((RO_2T+RO_4T)/MS_Sale)*100,2);
				}
				else
				{
					MS_2T_4T=0;
				}

				sql="select sum(v.totalqty) lube,sum(v.totalqty) lube,sum(v.oil2t) t2,sum(v.oil4t) t4 from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name like 'RO') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				if(dtr1.Read())
				{
					if(dtr1["t2"].ToString()!=null && dtr1["t2"].ToString()!="")
						Cumu_RO_2T=double.Parse(dtr1["t2"].ToString());

					Cumu_RO_2T=Math.Round((Cumu_RO_2T/1000),2);
					if(dtr1["t2"].ToString()!=null && dtr1["t2"].ToString()!="")
						Cumu_RO_4T=double.Parse(dtr1["t4"].ToString());

					Cumu_RO_4T=Math.Round((Cumu_RO_4T/1000),2);
					if(dtr1["lube"].ToString()!=null && dtr1["lube"].ToString()!="")
						Cumu_RO_Lube=double.Parse(dtr1["lube"].ToString());
					
					Cumu_RO_Lube=Math.Round((Cumu_RO_Lube/1000),2);
				}
				dtr1.Close();

				if(Cumu_HSD_Sale!=0 || Cumu_MS_Sale!=0)
				{
					Cumu_RO_LFR=Math.Round((Cumu_RO_Lube/(Cumu_HSD_Sale+Cumu_MS_Sale))*100,2);
				}
				else
				{
					Cumu_RO_LFR=0;
				}

				if(Cumu_MS_Sale!=0)
				{
					Cumu_MS_2T_4T=Math.Round(((Cumu_RO_2T+Cumu_RO_4T)/Cumu_MS_Sale)*100,2);
				}
				else
				{
					Cumu_RO_LFR=0;
				}

				/********SRSM 2***************/
				#endregion
			
				#region Get Data for SRSM 3A
				/********SRSM 3A***************/

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='ESSAR') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						Cumu_ERO_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_ERO_CY_Sale=Math.Round((Cumu_ERO_CY_Sale/1000),1);
					Cumu_Tot_CY_Sale+=Cumu_ERO_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='KSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						Cumu_KR_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_KR_CY_Sale=Math.Round((Cumu_KR_CY_Sale/1000),1);
					Cumu_Tot_CY_Sale+=Cumu_KR_CY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='NKSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						Cumu_NKR_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_NKR_CY_Sale=Math.Round((Cumu_NKR_CY_Sale/1000),1);
					Cumu_Tot_CY_Sale+=Cumu_NKR_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='ESSAR') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						Cumu_ERO_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_ERO_LY_Sale=Math.Round((Cumu_ERO_LY_Sale/1000),1);
					Cumu_Tot_LY_Sale+=Cumu_ERO_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='KSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						Cumu_KR_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_KR_LY_Sale=Math.Round((Cumu_KR_LY_Sale/1000),1);
					Cumu_Tot_LY_Sale+=Cumu_KR_LY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='NKSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						Cumu_NKR_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_NKR_LY_Sale=Math.Round((Cumu_NKR_LY_Sale/1000),1);
					Cumu_Tot_LY_Sale+=Cumu_NKR_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='ESSAR') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						ERO_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					ERO_CY_Sale=Math.Round((ERO_CY_Sale/1000),1);
					Tot_CY_Sale+=ERO_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='KSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						KR_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					KR_CY_Sale=Math.Round((KR_CY_Sale/1000),1);
					Tot_CY_Sale+=KR_CY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='NKSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						NKR_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					NKR_CY_Sale=Math.Round((NKR_CY_Sale/1000),1);
					Tot_CY_Sale+=NKR_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='ESSAR') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						ERO_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					ERO_LY_Sale=Math.Round((ERO_LY_Sale/1000),1);
					Tot_LY_Sale+=ERO_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='KSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						KR_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					KR_LY_Sale=Math.Round((KR_LY_Sale/1000),1);
					Tot_LY_Sale+=KR_LY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where sub_group_name='NKSK') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!=null && dtr1["sale"].ToString()!="")
						NKR_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					NKR_LY_Sale=Math.Round((NKR_LY_Sale/1000),1);
					Tot_LY_Sale+=NKR_LY_Sale;
				}
				dtr1.Close();
				/********SRSM 3A***************/
				#endregion

				#region Get Data for SRSM 3B
				/********SRSM 1***************/

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='RO') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_RO_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_RO_CY_Sale=Math.Round((Cumu_RO_CY_Sale/1000),1);
					Cumu_Tot_CY_Sale3B+=Cumu_RO_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='FLEET') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_FLEET_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					Cumu_FLEET_CY_Sale=Math.Round((Cumu_FLEET_CY_Sale/1000),2);
					Cumu_Tot_CY_Sale3B+=Cumu_FLEET_CY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='BAZZAR1') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_BAZ_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					Cumu_BAZ_CY_Sale=Math.Round((Cumu_BAZ_CY_Sale/1000),2);
					Cumu_Tot_CY_Sale3B+=Cumu_BAZ_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='OE') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_OE_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					Cumu_OE_CY_Sale=Math.Round((Cumu_OE_CY_Sale/1000),2);
					Cumu_Tot_CY_Sale3B+=Cumu_OE_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='RO') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_RO_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					Cumu_RO_LY_Sale=Math.Round((Cumu_RO_LY_Sale/1000),1);
					Cumu_Tot_LY_Sale3B+=Cumu_RO_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='FLEET') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_FLEET_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					Cumu_FLEET_LY_Sale=Math.Round((Cumu_FLEET_LY_Sale/1000),2);
					Cumu_Tot_LY_Sale3B+=Cumu_FLEET_LY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='BAZZAR1') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_BAZ_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					Cumu_BAZ_LY_Sale=Math.Round((Cumu_BAZ_LY_Sale/1000),2);
					Cumu_Tot_LY_Sale3B+=Cumu_BAZ_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='OE') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						Cumu_OE_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					Cumu_OE_LY_Sale=Math.Round((Cumu_OE_LY_Sale/1000),2);
					Cumu_Tot_LY_Sale3B+=Cumu_OE_LY_Sale;
				}
				dtr1.Close();

				//**********************************************//
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='RO') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						RO_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					RO_CY_Sale=Math.Round((RO_CY_Sale/1000),1);
					Tot_CY_Sale3B+=RO_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='FLEET') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						FLEET_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					FLEET_CY_Sale=Math.Round((FLEET_CY_Sale/1000),2);
					Tot_CY_Sale3B+=FLEET_CY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='BAZZAR1') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						BAZ_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					BAZ_CY_Sale=Math.Round((BAZ_CY_Sale/1000),2);
					Tot_CY_Sale3B+=BAZ_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='OE') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+EndDate.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						OE_CY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					OE_CY_Sale=Math.Round((OE_CY_Sale/1000),2);
					Tot_CY_Sale3B+=OE_CY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='RO') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						RO_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),1);
					RO_LY_Sale=Math.Round((RO_LY_Sale/1000),1);
					Tot_LY_Sale3B+=RO_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='FLEET') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						FLEET_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					FLEET_LY_Sale=Math.Round((FLEET_LY_Sale/1000),2);
					Tot_LY_Sale3B+=FLEET_LY_Sale;
				}
				dtr1.Close();
				//sql="select sum(totalqtyltr) sale from sales_master sm,customer c,customertype ct where c.cust_id=sm.cust_id and cust_type=customertypename and sub_group_name ='NKSK' and cast(floor(cast(invoice_date as float)) as datetime)>='"+CM_StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"'";
				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='BAZZAR1') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						BAZ_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					BAZ_LY_Sale=Math.Round((BAZ_LY_Sale/1000),2);
					Tot_LY_Sale3B+=BAZ_LY_Sale;
				}
				dtr1.Close();

				sql="select distinct sum(v.totalqty) sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and v.invoice_no=sm.invoice_no and cust_type in (select customertypename from customertype where group_name='OE') and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+LM_StartDate.ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+s2.ToString()+"'";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1["sale"].ToString()!="" && dtr1["sale"].ToString()!=null)
						OE_LY_Sale=Math.Round(double.Parse(dtr1["sale"].ToString()),2);
					OE_LY_Sale=Math.Round((OE_LY_Sale/1000),2);
					Tot_LY_Sale3B+=OE_LY_Sale;
				}
				dtr1.Close();

				/********SRSM 1***************/
				#endregion

				#region Get Data for SRSM 4
				/********SRSM 1***************/

				//select count(*) from customer where cust_type like 'Bazzar' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2012' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'ksk'  and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2012' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'OE' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2012' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'Fleet' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2012' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'ibp' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2012' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where (cust_type not like 'Ksk%' or cust_type not like '%N-Ksk%') and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2012' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')

				//select count(*) from customer where cust_type like 'Bazzar' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2011')
				//select count(*) from customer where cust_type like 'ksk'  and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2011')
				//select count(*) from customer where cust_type like 'OE' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2011')
				//select count(*) from customer where cust_type like 'Fleet' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2011')
				//select count(*) from customer where cust_type like 'ibp' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2011')
				//select count(*) from customer where (cust_type not like 'Ksk%' or cust_type not like '%N-Ksk%') and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2011')

				//select count(*) from customer where cust_type like 'Bazzar' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'ksk'  and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'OE' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'Fleet' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where cust_type like 'ibp' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')
				//select count(*) from customer where (cust_type not like 'Ksk%' or cust_type not like '%N-Ksk%') and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='4/1/2011' and cast(floor(cast(invoice_date as float)) as datetime)<='11/30/2012')

				/**Start**CY****/
				sql="select count(*) from customer where cust_type like 'Bazzar' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						BAZ_CY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_CY_Sale_4+=BAZ_CY_Sale_4;
					
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'ksk%'  and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						KR_CY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_CY_Sale_4+=KR_CY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like '%OE%' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						OE_CY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_CY_Sale_4+=OE_CY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'Fleet' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						FLEET_CY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_CY_Sale_4+=FLEET_CY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'ibp' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						IBP_CY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_CY_Sale_4+=IBP_CY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'N-Ksk%' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						NKR_CY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_CY_Sale_4+=NKR_CY_Sale_4;
				}
				dtr1.Close();
				/**End**CY****/

				/**Start**LY****/
				sql="select count(*) from customer where cust_type like 'Bazzar' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						BAZ_LY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_LY_Sale_4+=BAZ_LY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'ksk%'  and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						KR_LY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_LY_Sale_4+=KR_LY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like '%OE%' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						OE_LY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_LY_Sale_4+=OE_LY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'Fleet' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						FLEET_LY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_LY_Sale_4+=FLEET_LY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'ibp' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						IBP_LY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_LY_Sale_4+=IBP_LY_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'N-Ksk%' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+s2.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						NKR_LY_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_LY_Sale_4+=NKR_LY_Sale_4;
				}
				dtr1.Close();
				/**End**LY****/
			
				/**Start**All****/
				sql="select count(*) from customer where cust_type like 'Bazzar' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						BAZ_Tot_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_Sale_4+=BAZ_Tot_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'ksk%'  and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						KR_Tot_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_Sale_4+=KR_Tot_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like '%OE%' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						OE_Tot_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_Sale_4+=OE_Tot_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'Fleet' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						FLEET_Tot_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_Sale_4+=FLEET_Tot_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'ibp' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						IBP_Tot_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_Sale_4+=IBP_Tot_Sale_4;
				}
				dtr1.Close();
				sql="select count(*) from customer where cust_type like 'N-Ksk%' and cust_id in(select distinct cust_id from sales_master sm where cast(floor(cast(invoice_date as float)) as datetime)>='"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"')";
				dtr1=obj1.GetRecordSet(sql);
				while(dtr1.Read())
				{
					if(dtr1.GetValue(0).ToString()!="" && dtr1.GetValue(0).ToString()!=null)
						NKR_Tot_Sale_4=double.Parse(dtr1.GetValue(0).ToString());
					Tot_Sale_4+=NKR_Tot_Sale_4;
				}
				dtr1.Close();
				/**End**All****/
				/********SRSM 1***************/
				#endregion
			
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : SRSM_1_4()   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}


		public string GetMonthName(string mon)
		{
			if(mon.IndexOf("/")>0 || mon.IndexOf("-")>0)
			{
				string[] month=mon.IndexOf("/")>0?mon.Split(new char[] {'/'},mon.Length): mon.Split(new char[] { '-' }, mon.Length);
				if(month[0].ToString()=="1")
					return "Jan. "+month[2].ToString();
				else if(month[0].ToString()=="2")
					return "Feb. "+month[2].ToString();
				else if(month[0].ToString()=="3")
					return "Mar. "+month[2].ToString();
				else if(month[0].ToString()=="4")
					return "Apr. "+month[2].ToString();
				else if(month[0].ToString()=="5")
					return "May "+month[2].ToString();
				else if(month[0].ToString()=="6")
					return "June "+month[2].ToString();
				else if(month[0].ToString()=="7")
					return "July "+month[2].ToString();
				else if(month[0].ToString()=="8")
					return "Aug. "+month[2].ToString();
				else if(month[0].ToString()=="9")
					return "Sep. "+month[2].ToString();
				else if(month[0].ToString()=="10")
					return "Oct. "+month[2].ToString();
				else if(month[0].ToString()=="11")
					return "Nov. "+month[2].ToString();
				else if(month[0].ToString()=="12")
					return "Dec. "+month[2].ToString();
			}
			return "";
		}
		

		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
				Show();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : SRSM_Report.aspx, Method : btnShow_Click   SRSM_Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		public void Show()
		{
			int day=0;
			int Month=DropMonth.SelectedIndex;
			int Year=int.Parse(DropYear.SelectedItem.Text);
			
			if(DropMonth.SelectedIndex!=0)
			{
				if(DropMonth.SelectedIndex==1 || DropMonth.SelectedIndex==2 || DropMonth.SelectedIndex==3)
				{
					Year--;
					int last_year=Year;
					Year++;
					StartDate="4/1/"+last_year;
					day=DateTime.DaysInMonth(Year,Month);
					EndDate=Month+"/"+day+"/"+Year;
				}
				else
				{
					StartDate="4/1/"+Year;
					day=DateTime.DaysInMonth(Year,Month);
					EndDate=Month+"/"+day+"/"+Year;
				}
			}

			s1="";
			s2="";
			s2=GenUtil.str2MMDDYYYY(StartDate.ToString());
			s1=GenUtil.str2MMDDYYYY(EndDate.ToString());

			string[] ds1 =s2.IndexOf("/")>0? s2.Split(new char[] {'/'},s2.Length): s2.Split(new char[] { '-' }, s2.Length);
			string[] ds2 =s1.IndexOf("/")>0? s1.Split(new char[] {'/'},s1.Length): s1.Split(new char[] { '-' }, s1.Length);
			ds10=System.Convert.ToInt32(ds1[0]);
			ds20=System.Convert.ToInt32(ds2[0]);
			ds11=System.Convert.ToInt32(ds1[1]);
			ds12=System.Convert.ToInt32(ds1[2]);
			ds21=System.Convert.ToInt32(ds2[1]);
			ds22=System.Convert.ToInt32(ds2[2]);
			int Toyear=Convert.ToInt32(ds2[2]);
			int Fromyear=Convert.ToInt32(ds1[2]);
			
			CM_StartDate=ds2[1]+"/1/"+Toyear;

			Toyear--;
			Fromyear--;
			s1=ds1[1]+"/"+ds1[0]+"/"+Fromyear.ToString();
			day=DateTime.DaysInMonth(Toyear,Convert.ToInt32(ds2[1]));
			s2=ds2[1]+"/"+day+"/"+Toyear.ToString();

			LM_StartDate=ds2[1]+"/1/"+Toyear;
			
			if(ds12==ds22 && ds11 > ds21)
			{
				MessageBox.Show("Please Select Greater Month in DateTo");
				View=0;
				return;
			}
			if(ds10 >ds20 && ds12==ds22 && ds11 == ds21 )
			{
				MessageBox.Show("Please Select Greater Date");
				View=0;
				return;
			}
			if((ds22-ds12) > 1)
			{
				MessageBox.Show("Please Select date between one year");
				View=0;
				return;
			}
			if((ds22-ds12) == -1 || ((ds22-ds12) >= 1 && ds21 >=ds11))
			{
				MessageBox.Show("Please Select date between one year");
				View=0;
				return;
			}

			getDate(ds10,ds11,ds12,ds20,ds21,ds22); 
			getDateLastYear(ds10,ds11,ds12,ds20,ds21,ds22);


			if(dropType.SelectedIndex==1)
			{
				panSRSM_1_4.Visible=true;
				panSRSM_5.Visible=false;
				panSRSM_6.Visible=false;
				panSRSM_9.Visible=false;
				panSRSM_14.Visible=false;
				panSRSM_10_12.Visible=false;
				SRSM_1_4();
			}
			else if(dropType.SelectedIndex==2)
			{
				panSRSM_1_4.Visible=false;
				panSRSM_5.Visible=true;
				panSRSM_6.Visible=false;
				panSRSM_9.Visible=false;
				panSRSM_14.Visible=false;
				panSRSM_10_12.Visible=false;
			}
			else if(dropType.SelectedIndex==3)
			{
				panSRSM_1_4.Visible=false;
				panSRSM_5.Visible=false;
				panSRSM_6.Visible=true;
				panSRSM_9.Visible=false;
				panSRSM_14.Visible=false;
				panSRSM_10_12.Visible=false;
			}
			else if(dropType.SelectedIndex==4)
			{
				panSRSM_1_4.Visible=false;
				panSRSM_5.Visible=false;
				panSRSM_6.Visible=false;
				panSRSM_9.Visible=true;
				panSRSM_14.Visible=false;
				panSRSM_10_12.Visible=false;
				SRSM_9();
			}
			else if(dropType.SelectedIndex==5)
			{
				panSRSM_1_4.Visible=false;
				panSRSM_5.Visible=false;
				panSRSM_6.Visible=false;
				panSRSM_9.Visible=false;
				panSRSM_10_12.Visible=true;
				panSRSM_14.Visible=false;
				SRSM_10_12();
			}
			else if(dropType.SelectedIndex==6)
			{
				panSRSM_1_4.Visible=false;
				panSRSM_5.Visible=false;
				panSRSM_6.Visible=false;
				panSRSM_9.Visible=false;
				panSRSM_10_12.Visible=false;
				panSRSM_14.Visible=true;
				SRSM14();
			}
		}

		public void getDateLastYear(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count1=To2-From2;
				DateFrom1 = new string[count+1];
				DateTo1 = new string[count+1];
				TotalSum1 = new double[(count+1)];
			}
			else
			{
				count1=13-From2;
				count1+=To2;
				DateFrom1 = new string[count];
				DateTo1 = new string[count];
				TotalSum1 = new double[count];
			}
			From3--;
			To3--;
			int c=0;
			if(From2<=To2)
			{
				for(int i=From2,j=0;i<=To2;i++,j++)
				{
					if(c==0)
					{
						
						DateFrom1[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
						c=1;
					}
					else
						DateFrom1[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					
					int day=DateTime.DaysInMonth(From3,i);
					DateTo1[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
				}
			}
			else
			{
				for(int i=From2,j=0;i<=12;i++,j++)
				{
					if(c==0)
						DateFrom1[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
					else
						DateFrom1[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					int day=DateTime.DaysInMonth(From3,i);
					DateTo1[j]=i.ToString()+"/"+day.ToString()+"/"+From3.ToString();
					c++;
				}
				for(int i=1,j=c;i<=To2;i++,j++)
				{
					DateFrom1[j]=i.ToString()+"/"+"1"+"/"+To3.ToString();
					if(i==To2)
						DateTo1[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
					else
					{
						int day=DateTime.DaysInMonth(To3,i);
						DateTo1[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
		}

		/// <summary>
		/// This method is used to get the to and from date.
		/// </summary>
		public void getDate(int From1,int From2,int From3,int To1,int To2,int To3)
		{
			if(From2<=To2)
			{
				count=To2-From2;
				DateFrom = new string[count+1];
				DateTo = new string[count+1];
				TotalSum = new double[(count+1)];
			}
			else
			{
				count=13-From2;
				count+=To2;
				DateFrom = new string[count];
				DateTo = new string[count];
				TotalSum = new double[count];
			}
			int c=0;
			if(From2<=To2)
			{
				for(int i=From2,j=0;i<=To2;i++,j++)
				{
					if(c==0)
					{
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
						c=1;
					}
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					if(i==To2)
					{
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
						c=2;
					}
					else
					{
						int day=DateTime.DaysInMonth(From3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
			}
			else
			{
				for(int i=From2,j=0;i<=12;i++,j++)
				{
					if(c==0)
						DateFrom[j]=i.ToString()+"/"+From1.ToString()+"/"+From3.ToString();
					else
						DateFrom[j]=i.ToString()+"/"+"1"+"/"+From3.ToString();
					int day=DateTime.DaysInMonth(From3,i);
					DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+From3.ToString();
					c++;
				}
				for(int i=1,j=c;i<=To2;i++,j++)
				{
					DateFrom[j]=i.ToString()+"/"+"1"+"/"+To3.ToString();
					if(i==To2)
						DateTo[j]=i.ToString()+"/"+To1.ToString()+"/"+To3.ToString();
					else
					{
						int day=DateTime.DaysInMonth(To3,i);
						DateTo[j]=i.ToString()+"/"+day.ToString()+"/"+To3.ToString();
					}
				}
				
			}
		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				HttpCookie cookie = Request.Cookies["A"];
				if (cookie == null)
				{
					cookie = new HttpCookie("A");
				}
				cookie["A"] = txtvalue.Text.ToString();
				cookie.Expires = DateTime.Now.AddYears(1);
				Response.Cookies.Add(cookie);
				
				/*string Val = txtvalue.Text.ToString().Trim();  
				if(Val.Equals(""))
				{
					MessageBox.Show(" Please enter Value in TextBox ");
					return ;
				}*/
				//int i=0;
				//dbobj.Insert_or_Update("Update mmisc_cd set Key_Descr = '"+Val+"' where key_type = 'Interest'",ref i);
				MessageBox.Show(" Value Saved "); 
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_bill_Ageing.aspx,Method:Update1_Click"+"  Customer Ageing Report Printed "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		protected void dropType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(dropType.SelectedIndex==5)
				{
						txtvalue.Visible=true;
					btnSave.Visible=true;
					HttpCookie cookie = Request.Cookies["A"];
					if (cookie == null)
					{
						
						cookie["A"]="";
					}
					if (cookie["A"] != null)
					{
						txtvalue.Text=cookie["A"];
					}
					else
					{
						txtvalue.Text="";
					}
				}
				else
				{
					txtvalue.Visible=false;
					btnSave.Visible=false;
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog(" Form : SRSM_Report.aspx,Method : dropType_SelectedIndexChanged "+"  dropType_SelectedIndexChanged "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		protected void btneExcel_Click(object sender, System.EventArgs e)
		{
			ConverttoExcel();
			MessageBox.Show("Successfully Convert File into Excel Format");
			CreateLogFiles.ErrorLog("Form:PartyWiseSalesFigure.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" PartyWiseSalesFigure Report Convert Into Excel Format ,  userid  "+uid);
		}

		public void ConverttoExcel()
		{
			try
			{
				Show();
				string home_drive=Environment.SystemDirectory;
				home_drive=home_drive.Substring(0,2);
				string strExcelPath=home_drive+"\\Servosms_ExcelFile\\Export\\";
				Directory.CreateDirectory(strExcelPath);
				string Path=home_drive+@"\Servosms_ExcelFile\Export\SRSM_Report.xls";
				StreamWriter sw=new StreamWriter(Path);

				if(dropType.SelectedIndex==1)
				{
					sw.WriteLine("\t\t\t\t\t\t\t\t\tSRSM 1-4\t\t\t\t\t\t\t\t\t");
					sw.WriteLine("SRSM-1\t\t\t\t\t\t"+
						"MONTH : "+GetMonthName(EndDate.ToString())+"\t\t\t\t\t"+
						"CUMULATIVE : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())+"\t");
					sw.WriteLine("SSA/GSS/RSE\tSSA/GSS/SAP Code\tNo of Districts Covered\tPotential\tTarget\tCY\tLY\tGrowth\t% GR\t% Achv.\t% of Potential\tPotential\tTarget\tCY\tLY\tGrowth\t% GR\t% Achv.\t% of Potential");
					sw.WriteLine("SSA GWALIOR\t178676\t6\t100\t"+Target+"\t"+
						CY_P_Sale+"\t"+
						LY_P_Sale+"\t"+
						Growth+"\t"+
						GR_Per+"\t"+
						Achiv+"\t"+
						Poten_Per+"\t\t"+
						Cumu_Target+"\t"+
						Cumu_CY_P_Sale+"\t"+
						Cumu_LY_P_Sale+"\t"+
						Cumu_Growth+"\t"+
						Cumu_GR_Per+"\t"+
						Cumu_Achiv+"\t"+
						Cumu_Poten_Per);
													
					sw.WriteLine();
					sw.WriteLine();
					sw.WriteLine("SRSM-2\t\t\t\t\t\t\tLFR, 2T+4T/MS RATIO\t\t\t\t\t\t\t");
					sw.WriteLine("SO\tMONTH : "+GetMonthName(EndDate.ToString())+"\t\t\t\t\t\t\tCUMULATIVE : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
					sw.WriteLine("SSA/GSS\tMS\tHSD\tRO Lube\tRO LFR\tRO 2T\tRO 4T\t2T+4T/MS\tMS\tHSD\tRO Lube\tRO LFR\tRO 2T\tRO 4T\t2T+4T/MS");
					sw.WriteLine("SSA GWALIOR\t"+MS_Sale.ToString()+"\t"+HSD_Sale.ToString()+"\t"+RO_Lube.ToString()+"\t\t"+
						RO_2T.ToString()+"\t"+RO_4T.ToString()+"\t\t"+
						Cumu_MS_Sale.ToString()+"\t"+
						Cumu_HSD_Sale.ToString()+"\t"+Cumu_RO_Lube.ToString()+"\t\t"+
						Cumu_RO_2T.ToString()+"\t"+
						Cumu_RO_4T.ToString()+"\t\t");
					sw.WriteLine();
					sw.WriteLine();

					sw.WriteLine("SRSM-3A\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t");
					sw.WriteLine("SO : \t\t\t\t\t\t\t\tMONTH : "+GetMonthName(EndDate.ToString())+"\tCUMULATIVE : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
					sw.WriteLine("SSA/GSS\tNON KSK RO\t\tKSK RO\t\tESSAR RO\t\tRO TOTAL\t\tNON KSK RO\t\tKSK RO\t\tESSAR RO\t\tRO TOTAL");
					sw.WriteLine("\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY");
					sw.WriteLine("SSA GWALIOR\t"+NKR_CY_Sale.ToString()+"\t"+
						NKR_LY_Sale.ToString()+"\t"+
						KR_CY_Sale.ToString()+"\t"+
						KR_LY_Sale.ToString()+"\t"+
						ERO_CY_Sale.ToString()+"\t"+
						ERO_LY_Sale.ToString()+"\t"+
						Tot_CY_Sale.ToString()+"\t"+
						Tot_LY_Sale.ToString()+"\t"+
						Cumu_NKR_CY_Sale.ToString()+"\t"+
						Cumu_NKR_LY_Sale.ToString()+"\t"+
						Cumu_KR_CY_Sale.ToString()+"\t"+
						Cumu_KR_LY_Sale.ToString()+"\t"+
						Cumu_ERO_CY_Sale.ToString()+"\t"+
						Cumu_ERO_LY_Sale.ToString()+"\t"+
						Cumu_Tot_CY_Sale.ToString()+"\t"+
						Cumu_Tot_LY_Sale.ToString());
					sw.WriteLine();
					sw.WriteLine();
					sw.WriteLine("SRSM-3B\tCHANNEL-WISE SALES PERFORMANCE");
					sw.WriteLine("SO :\t\t\t\t\tMONTH :"+GetMonthName(EndDate.ToString())+"\tCUMULATIVE : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
					sw.WriteLine("SSA/GSS\t\tRO TOTAL\t\tBAZZAR\t\tOE\t\tFLEET\t\tTOTAL\t\tRO TOTAL\t\tBAZZAR\t\tOE\t\tFLEET\t\tTOTAL");
					sw.WriteLine("\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY\tCY\tLY");
					sw.WriteLine("SSA GWALIOR\t"+
						RO_CY_Sale.ToString()+"\t"+
						RO_LY_Sale.ToString()+"\t"+
						BAZ_CY_Sale.ToString()+"\t"+
						BAZ_LY_Sale.ToString()+"\t"+
						OE_CY_Sale.ToString()+"\t"+
						OE_LY_Sale.ToString()+"\t"+
						FLEET_CY_Sale.ToString()+"\t"+
						FLEET_LY_Sale.ToString()+"\t"+
						Tot_CY_Sale3B.ToString()+"\t"+
						Tot_LY_Sale3B.ToString()+"\t"+
						Cumu_RO_CY_Sale.ToString()+"\t"+
						Cumu_RO_LY_Sale.ToString()+"\t"+
						Cumu_BAZ_CY_Sale.ToString()+"\t"+
						Cumu_BAZ_LY_Sale.ToString()+"\t"+
						Cumu_OE_CY_Sale.ToString()+"\t"+
						Cumu_OE_LY_Sale.ToString()+"\t"+
						Cumu_FLEET_CY_Sale.ToString()+"\t"+
						Cumu_FLEET_LY_Sale.ToString()+"\t"+
						Cumu_Tot_CY_Sale3B.ToString()+"\t"+
						Cumu_Tot_LY_Sale3B.ToString());
					sw.WriteLine();
					sw.WriteLine();
					sw.WriteLine("SRSM-4\tCHANNEL-MARKET PENETRATION");
					sw.WriteLine("SO : SO\tCUMULATIVE : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
					sw.WriteLine("\tNON KSK RO\t\t\tKSK RO\t\t\tBAZZAR\t\t\tOE DEALERS\t\t\tFLEET OWNER\t\t\tIBP\t\t\tTOTAL");
					sw.WriteLine("SSA/GSS\tTOTAL\tCY\tLY\tTOTAL\tCY\tLY\tTOTAL\tCY\tLY\tTOTAL\tCY\tLY\tTOTAL\tCY\tLY\tTOTAL\tCY\tLY\tTOTAL\tCY\tLY");
					sw.WriteLine("SSA GWALIOR\t"+
						NKR_Tot_Sale_4.ToString()+"\t"+
						NKR_CY_Sale_4.ToString()+"\t"+
						NKR_LY_Sale_4.ToString()+"\t"+
						KR_Tot_Sale_4.ToString()+"\t"+
						KR_CY_Sale_4.ToString()+"\t"+
						KR_LY_Sale_4.ToString()+"\t"+
						BAZ_Tot_Sale_4.ToString()+"\t"+
						BAZ_CY_Sale_4.ToString()+"\t"+
						BAZ_LY_Sale_4.ToString()+"\t"+
						OE_Tot_Sale_4.ToString()+"\t"+
						OE_CY_Sale_4.ToString()+"\t"+
						OE_LY_Sale_4.ToString()+"\t"+
						FLEET_Tot_Sale_4.ToString()+"\t"+
						FLEET_CY_Sale_4.ToString()+"\t"+
						FLEET_LY_Sale_4.ToString()+"\t"+
						IBP_Tot_Sale_4.ToString()+"\t"+
						IBP_CY_Sale_4.ToString()+"\t"+
						IBP_LY_Sale_4.ToString()+"\t"+
						Tot_Sale_4.ToString()+"\t"+
						Tot_CY_Sale_4.ToString()+"\t"+
						Tot_LY_Sale_4.ToString());
										
				}
				else if(dropType.SelectedIndex==2)
				{
					i=1;
					ii=0;
					InventoryClass obj=new InventoryClass();
					sql="select cust_id,cust_name,prod_name+' : '+pack_type prod,(quant*total_qty) Qty_Ltr from vw_salebook where cust_type like 'OE%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"' order by cust_name";
					dtr1=obj.GetRecordSet(sql);
					if(dtr1.HasRows)
					{
						sw.WriteLine("\tSRSM 5\t");
						sw.WriteLine("SO\t"+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())+"\tFIGS in KL");
						sw.WriteLine("SL. No.\tName of OE Dealer\tGenuine Oil 1\tGenuine Oil 2\tGenuine Oil 3\tGenuine Oil 4\tTotal");
						while(dtr1.Read())
						{
							if(dtr1["cust_name"].ToString()!=Cust_Name)
							{
								i=1;
							}
							if(ii!=0)
							{
								if(i==1)
								{
									sw.WriteLine("\tTotal\t\t"+Total.ToString()+"\t\t");
									Total=0;
								}
							}
							sw.Write(i.ToString()+"\t");
							if(dtr1["cust_name"].ToString()!=Cust_Name)
							{
								Cust_Name=dtr1["cust_name"].ToString();
								sw.Write(dtr1["cust_name"].ToString()+"\t");
							}
							else
							{
								sw.Write("\t");
							}
							sw.Write(dtr1["prod"].ToString());
							Total+=double.Parse(dtr1["Qty_Ltr"].ToString());
							sw.Write(dtr1["Qty_Ltr"].ToString()+"\t\t\t");
							sw.WriteLine();
							i++;
						}
						dtr1.Close();
						sw.WriteLine("\tTotal\t\t"+Total.ToString()+"\t\t");
					}
				}
				else if(dropType.SelectedIndex==3)
				{
					sw.WriteLine();
					sw.WriteLine("SRSM 6");
					double Month_Tot_Target=0;
					double Month_Tot_CY=0;
					double Month_Tot_LY=0;
					double Month_Tot_Growth=0;
					double Month_Tot_Achivent=0;
					double Cumu_Tot_Target=0;
					double Cumu_Tot_CY=0;
					double Cumu_Tot_LY=0;
					double Cumu_Tot_Growth=0;
					double Cumu_Tot_Achivent=0;
					double sale1=0;
					double sale2=0;
					double growth=0;
					double GR=0;
					i=1;
					InventoryClass obj=new InventoryClass();
					sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where category like '%GEN OIL%' or category ='Mstar' or  category = 'Eicher' or  category ='hyundai oils' or  category = 'L&t' order by Prod_Name";
					dtr1=obj.GetRecordSet(sql);
					if(dtr1.HasRows)
					{
						sw.WriteLine("SRSM-6 A\tSO :\tMONTH : "+GetMonthName(EndDate.ToString()));
						sw.WriteLine("SRSM-6 A\t\t\tMONTH : "+GetMonthName(EndDate.ToString())+"CUM :"+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
						sw.WriteLine("SR. No.\tProduct Code\tGENUINE OIL\tCY\tLY\tGRTH\t%GR\tCY\tLY\tGRTH\t%GR");
						while(dtr1.Read())
						{
							sw.Write(i.ToString()+"\t");
							sw.Write(dtr1["Prod_code"].ToString()+"\t");
							sw.Write(dtr1["Product"].ToString()+"\t");
							sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
							Month_Tot_CY+=sale1;
							Month_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
							}
							else
							{
								growth=0;
								GR=0;
							}
							sw.Write(sale1.ToString()+"\t");
							sw.Write(sale2.ToString()+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sale1=0;
							sale2=0;
							sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
							Cumu_Tot_CY+=sale1;
							Cumu_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
							}
							else
							{
								growth=0;
								GR=0;
							}
							sw.Write(sale1+"\t");
							sw.Write(sale2+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.WriteLine();
							i++;
						}
						dtr1.Close();
						sw.Write("\t\t\t"+Month_Tot_CY.ToString()+"\t");
						sw.Write(Month_Tot_LY.ToString()+"\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.Write(Cumu_Tot_CY.ToString()+"\t");
						sw.Write(Cumu_Tot_LY.ToString()+"\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.WriteLine();
						sw.WriteLine();
						sw.WriteLine();
					}
					Month_Tot_Target=0;
					Month_Tot_CY=0;
					Month_Tot_LY=0;
					Month_Tot_Growth=0;
					Month_Tot_Achivent=0;
					Cumu_Tot_Target=0;
					Cumu_Tot_CY=0;
					Cumu_Tot_LY=0;
					Cumu_Tot_Growth=0;
					Cumu_Tot_Achivent=0;
					sale1=0;
					sale2=0;
					growth=0;
					double Target=0;
					double Achivment=0;
					GR=0;
					i=1;
					sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where (prod_name like 'PREMIUM CF4 15w40%' or prod_name = '4T' or prod_name = '4T Goben' or prod_name like 'Pride TC 15w40%' or prod_name like 'GEAR HP ALPHA 80w90%' or prod_name like 'Tractor Oil 20w40%' or prod_name like 'Transfluid A%' or prod_name like 'Autorickshaw oil%' or prod_name like 'Gear HP 90 - 20%' or prod_name like 'Pride XL 15w40%' or prod_name like 'Super MG CF4 15W-40%' or prod_name like 'GAS ENGINE OIL%' or prod_name like 'Agrospray%' or prod_name like 'Pride  GEO%' or prod_name like 'HBF Super HD%' or prod_name like 'KOOL PLUS%' or prod_name like 'KOOL READY%' or prod_name like 'Glysantin G 05%'  or prod_name like 'Glysantin G 48%') order by Prod_Name";
					dtr1=obj.GetRecordSet(sql);
					if(dtr1.HasRows)
					{
						sw.WriteLine(" SRSM-6 B\tSO : \tSALES OF FOCUS GRADES ");
						sw.WriteLine("\t\t\t"+GetMonthName(EndDate.ToString())+"\t\t\t\t\t\tCUM :"+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
						sw.WriteLine("SR. No.\tProduct Code\tGRADE\tTARGET\tCY\tLY\tGR\t%GR\t%ACHV.\tTARGET\tCY\tLY\tGR\t%GR\t%ACHV.");
						while(dtr1.Read())
						{
							sw.Write(i.ToString()+"\t"+dtr1["Prod_code"].ToString()+"\t"+dtr1["Product"].ToString()+"\t");
							sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
							Month_Tot_CY+=sale1;
							Month_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
								Target=Math.Round((sale1*1.1),2);
								Achivment=Math.Round(((sale1/Target)*100),2);
								if(Target!=0)
									Achivment=Math.Round(((sale1/Target)*100),2);
								else
									Achivment=0;
							}
							else
							{
								growth=0;
								GR=0;
								Target=0;
								Achivment=0;
							}
							sw.Write(Target.ToString()+"\t");
							sw.Write(sale1.ToString()+"\t");
							sw.Write(sale2.ToString()+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.Write(Achivment.ToString()+"\t");

							sale1=0;
							sale2=0;
							sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
							Cumu_Tot_CY+=sale1;
							Cumu_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
								Target=Math.Round((sale1*1.1),2);
								if(Target!=0)
									Achivment=Math.Round(((sale1/Target)*100),2);
								else
									Achivment=0;
							}
							else
							{
								growth=0;
								GR=0;
								Target=0;
								Achivment=0;
							}
							sw.Write(Target.ToString()+"\t");
							sw.Write(sale1+"\t");
							sw.Write(sale2+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.Write(Achivment.ToString()+"\t");
							sw.WriteLine();
							i++;
						}
						dtr1.Close();
						sw.Write("\t\tTotal\t");
						sw.Write(Month_Tot_CY.ToString()+"\t");
						sw.Write(Month_Tot_LY.ToString()+"\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.Write(Cumu_Tot_CY.ToString()+"\t");
						sw.Write(Cumu_Tot_LY.ToString()+"\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.Write("\t");
						sw.WriteLine();
						sw.WriteLine();
						sw.WriteLine();
					}
					Month_Tot_Target=0;
					Month_Tot_CY=0;
					Month_Tot_LY=0;
					Month_Tot_Growth=0;
					Month_Tot_Achivent=0;
					Cumu_Tot_Target=0;
					Cumu_Tot_CY=0;
					Cumu_Tot_LY=0;
					Cumu_Tot_Growth=0;
					Cumu_Tot_Achivent=0;
					sale1=0;
					sale2=0;
					growth=0;
					Target=0;
					Achivment=0;
					GR=0;
					i=1;
					sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where (prod_name like '2T Supreme%' or prod_name like 'TowerGen%' or prod_name like 'PSO%' or prod_name like 'Servo system 68%' or prod_name like 'Servosystem HLP 68%' or prod_name like 'Servo Hydrex TH 46%') order by Prod_Name";
					dtr1=obj.GetRecordSet(sql);
					if(dtr1.HasRows)
					{
						sw.Write("SRSM-6 C\tSALES OF IMPORTANT GRADES");
						sw.WriteLine("\t\t\t"+GetMonthName(EndDate.ToString())+"\t\t\t\t\t\tCUM : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
						sw.WriteLine("SR. No.\tProduct Code\tGRADE\tTARGET\tCY\tLY\tGR\t%GR\t%ACHV.\tTARGET\tCY\tLY\tGR\t%GR\t%ACHV.");
						while(dtr1.Read())
						{
							sw.Write(i.ToString()+"\t"+dtr1["Prod_code"].ToString()+"\t"+dtr1["Product"].ToString()+"\t");
							sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
							Month_Tot_CY+=sale1;
							Month_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
								Target=Math.Round((sale1*1.1),2);
								Achivment=Math.Round(((sale1/Target)*100),2);
								if(Target!=0)
									Achivment=Math.Round(((sale1/Target)*100),2);
								else
									Achivment=0;
							}
							else
							{
								growth=0;
								GR=0;
								Target=0;
								Achivment=0;
							}
							sw.Write(Target.ToString()+"\t");
							sw.Write(sale1.ToString()+"\t");
							sw.Write(sale2.ToString()+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.Write(Achivment.ToString()+"\t");
							sale1=0;
							sale2=0;
							sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
							Cumu_Tot_CY+=sale1;
							Cumu_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
								Target=Math.Round((sale1*1.1),2);
								if(Target!=0)
									Achivment=Math.Round(((sale1/Target)*100),2);
								else
									Achivment=0;
							}
							else
							{
								growth=0;
								GR=0;
								Target=0;
								Achivment=0;
							}
							sw.Write(Target.ToString()+"\t");
							sw.Write(sale1+"\t");
							sw.Write(sale2+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.Write(Achivment.ToString());
							sw.WriteLine();
							i++;
						}
						dtr1.Close();
						sw.Write("\t\tTotal\t"+Month_Tot_CY.ToString());
						sw.Write(Month_Tot_LY.ToString()+"\t\t\t\t\t");
						sw.Write(Cumu_Tot_CY.ToString()+"\t");
						sw.Write(Cumu_Tot_LY.ToString()+"\t\t\t\t");
					}
					Month_Tot_Target=0;
					Month_Tot_CY=0;
					Month_Tot_LY=0;
					Month_Tot_Growth=0;
					Month_Tot_Achivent=0;
					Cumu_Tot_Target=0;
					Cumu_Tot_CY=0;
					Cumu_Tot_LY=0;
					Cumu_Tot_Growth=0;
					Cumu_Tot_Achivent=0;
					sale1=0;
					sale2=0;
					growth=0;
					Target=0;
					Achivment=0;
					GR=0;
					i=1;
					sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where (prod_name like 'GREASE WB%' or prod_name like 'GREASE MP%' or prod_name like 'Servo Grease MP3%' or prod_name like 'Servo Gem RR 3%') order by Prod_Name";
					dtr1=obj.GetRecordSet(sql);
					if(dtr1.HasRows)
					{
						sw.WriteLine("SRSM-6 D\tSRSM-6 D\tSALES OF GREASES");
						sw.WriteLine("\t\t\t"+GetMonthName(EndDate.ToString())+"\tCUM : "+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));

						sw.WriteLine("SR. No.\tProduct Code\tGRADE\tTARGET\tCY\tLY\tGR\t%GR\t%ACHV.\tTARGET\tCY\tLY\tGR\t%GR\t%ACHV.");
						while(dtr1.Read())
						{
							sw.Write(i.ToString()+"\t");
							sw.Write(dtr1["Prod_code"].ToString()+"\t");
							sw.Write(dtr1["Product"].ToString()+"\t");

							sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
							Month_Tot_CY+=sale1;
							Month_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
								Target=Math.Round((sale1*1.1),2);
								Achivment=Math.Round(((sale1/Target)*100),2);
								if(Target!=0)
									Achivment=Math.Round(((sale1/Target)*100),2);
								else
									Achivment=0;
							}
							else
							{
								growth=0;
								GR=0;
								Target=0;
								Achivment=0;
							}
							sw.Write(Target.ToString()+"\t");
							sw.Write(sale1.ToString()+"\t");
							sw.Write(sale2.ToString()+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.Write(Achivment.ToString()+"\t");

							sale1=0;
							sale2=0;
							sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
							sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
							Cumu_Tot_CY+=sale1;
							Cumu_Tot_LY+=sale2;
							if(sale2!=0 && sale2!=0)
							{
								growth=Math.Round((sale1-sale2),2);
								GR=Math.Round(((growth/sale2)*100),2);
								Target=Math.Round((sale1*1.1),2);
								if(Target!=0)
									Achivment=Math.Round(((sale1/Target)*100),2);
								else
									Achivment=0;
							}
							else
							{
								growth=0;
								GR=0;
								Target=0;
								Achivment=0;
							}
							sw.Write(Target.ToString()+"\t");
							sw.Write(sale1+"\t");
							sw.Write(sale2+"\t");
							sw.Write(growth.ToString()+"\t");
							sw.Write(GR.ToString()+"\t");
							sw.Write(Achivment.ToString()+"\t");
							sw.WriteLine();
							i++;
						}
						dtr1.Close();
						sw.Write("\t\tTotal\t\t"+Month_Tot_CY.ToString()+"\t"+Month_Tot_LY.ToString()+"\t\t\t\t");
						sw.Write(Cumu_Tot_CY.ToString()+"\t"+Cumu_Tot_LY.ToString()+"\t\t\t\t");
						sw.WriteLine();
						sw.WriteLine();
					}

				}
				else if(dropType.SelectedIndex==4)
				{
					sw.WriteLine("SRSM 9");
					sw.WriteLine("\tOUTGO THROUGH INCENTIVE SCHEMES IN RETAIL LUBE SALES -"+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString()));
					sw.Write("\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(GetMonthName(DateFrom[m].ToString())+"\t");
					}
					sw.Write("Total");
					sw.WriteLine();
					sw.Write("Primery Sales Vol, KL (50 Ltrs & Brls)\t");
					double total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(PS_Barel[m].ToString()+"\t");
						total+=PS_Barel[m];
					}
					sw.Write(total.ToString());
					sw.WriteLine();
					sw.Write(" Primery Sales Vol, KL (Smalls) \t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(PS_Small[m].ToString()+"\t");
						total+=PS_Small[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write("Total Primery Sales Vol, KL\t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(PS_Tot[m].ToString()+"\t");
						total+=PS_Tot[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write("Secondary Channel Incentive, Rs.\t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(PS_Claim[m].ToString()+"\t");
						total+=PS_Claim[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write("Resellers, Rs.\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("Product Discount\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();

					sw.Write(" LFR & 2T+4T to MS Ratio Scheme \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" 4T PROMOTIONAL QUATERLY BASIS \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("ANNUAL GROWTH INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("DIFF PROMOTIONAL DISCOUNT CLAIM PBS \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("DLP DIFF DISCOUNT \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("CAP KE NICHE CASH \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();

					sw.Write(" QUATERLY GROWTH INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("SERVO BONANZA OFFER \t");

					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("OE SEGMENT, Rs.");

					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" VOLUME DISCOUNT \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("EQUIPMENT FINANCE INCL EICHER CAPEX \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("FLEET OWNERS, Rs.\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("VOLUME DISCOUNT \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("XTRAPOWER REWARD\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" TOTAL CHANNEL INCENTIVE, Rs.\t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("SSA INCENTIVE, Rs.\t");
					
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("SSA TRADE DISCOUNT \t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(Trade_Disc[m].ToString()+"\t");
						total+=Trade_Disc[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write("SSA CASH DISCOUNT \t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(Cash_Disc[m].ToString()+"\t");
						total+=Cash_Disc[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write("SSA EARLY BIRD DISCOUNT\t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(EB_Disc[m].ToString()+"\t");
						total+=EB_Disc[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write(" BAZZAR PROMOTION SCHEME \t ");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");

					sw.WriteLine();
					
					sw.Write(" KSK PROMOSCHEME \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" DISCOUNT ON 50 LTRS &amp; BRLS \t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(baral_Disc[m].ToString()+"\t");
						total+=baral_Disc[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write(" ADVANCE DISCOUNT ADJUST IN SSC \t");

					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(Fixed_Disc[m].ToString()+"\t");
						total+=Fixed_Disc[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();

					sw.Write(" PGP INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" SGP INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(" \t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" ANNUAL GROWTH INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(" \t");
					}
					sw.Write(" \t");
					sw.WriteLine();
					sw.Write(" CONSISTENCY GROWTH INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write(" \t");
					sw.WriteLine();
					sw.Write(" PRODUCT PROMO DISCOUNT \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(" \t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" DEBIT NOTE FOR 50 LTRS & BRLS INCENTIVE NOT PASSED \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" TOTAL SSA INCENTIVE, RS.\t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(Tot_SSA_Inc1[m].ToString()+"\t");
						total+=Tot_SSA_Inc1[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();

					sw.Write(" SALES PROMOTION SCHEME, RS. \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" SERVO SADBHAVNA SCHEME \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" CUSTOMER PULL SCHEME \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" SERVO MECHANIC CLUB SCHEME \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" SERVO VISIBILTY SCHEME \t");

					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();

					sw.Write(" SERVO GIVEAWAY SCHEME \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();

					sw.Write(" SERVO VAN PUBLICITY SCHEME \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" REWARD SCHEME \t");

					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();

					sw.Write(" TOTAL OUTGO ON RETAIL SCHEME, RS. \t");
					total=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write(Tot_SSA_Inc1[m].ToString()+"\t");
						total+=Tot_SSA_Inc1[m];
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write(" TOTAL OUTGO \t");
					total=0;
					double tem=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						tem=Math.Round((Tot_SSA_Inc1[m]/PS_Barel[m]));
						total+=tem;
						sw.Write(tem.ToString()+"\t");
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write(" SECONDARY CHANNEL INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" RESELLER INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" OE SEGMENT INCENTIVE \t");

					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write(" FLEET SEGMENT INCENTIVE \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
					sw.Write("SSA INCENTIVE \t");
					total=0;
					tem=0;
					for(int m=0;m<DateFrom.Length;m++)
					{
						tem=Math.Round((Tot_SSA_Inc1[m]/PS_Tot[m]));
						total+=tem;
						sw.Write(tem.ToString()+"\t");
					}
					sw.Write(total.ToString()+"\t");
					sw.WriteLine();
					sw.Write("SALES PROMOTION SCHEMES \t");
					for(int m=0;m<DateFrom.Length;m++)
					{
						sw.Write("\t");
					}
					sw.Write("\t");
					sw.WriteLine();
				}
				else if(dropType.SelectedIndex==5)
				{
					sw.Write(" SRSM 10 & 12 \t");
					sw.Write(" SALE IN SMALLS/BARRELS MONTH: NOV-12 \t");
					sw.WriteLine();
					sw.Write("SRSM 10 \t");
					sw.Write("CURRENT YEAR ("+GetMonthName(EndDate.ToString())+"\t\t\t");
					sw.Write("LAST YEAR ("+GetMonthName(s2.ToString())+"\t\t\t");
					sw.Write("CURRENT YEAR CUM ("+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())+"\t\t\t");
					sw.Write("LAST YEAR CUM : "+GetMonthName(s1.ToString())+" - "+GetMonthName(s2.ToString())+"\t\t\t");
					sw.WriteLine();
					sw.WriteLine("SSA/GSS\tSMALLS\tBARELS\tTOTAL\tSMALLS\tBARELS\tTOTAL\tSMALLS\tBARELS\tTOTAL\tSMALLS\tBARELS\tTOTAL");
					sw.WriteLine("SSA GWALIOR \t"+
						CY_Small_M.ToString()+"\t"+
						CY_Barel_M.ToString()+"\t"+
						CY_Tot_M.ToString()+"\t"+
						LY_Small_M.ToString()+"\t"+
						LY_Barel_M.ToString()+"\t"+
						LY_Tot_M.ToString()+"\t"+
						CY_Small_C.ToString()+"\t"+
						CY_Barel_C.ToString()+"\t"+
						CY_Tot_C.ToString()+"\t"+
						LY_Small_C.ToString()+"\t"+
						LY_Barel_C.ToString()+"\t"+
						LY_Tot_C.ToString());
					sw.WriteLine();
					sw.WriteLine();

					sw.WriteLine("SRSM 12\t\tMONTHEND OUTSTANDING,Rs. Lacs\t\t\t\t");
					sw.WriteLine("SSA/GSS\tRO\tBAZZAR\tOE\tFLEET\tTOTAL\tMONTH END INVENTORY (KL)\tNO OF DAY'S COVERAGE (MIN 15 DAYS)");
					sw.WriteLine("SSA GWALIOR\t"+
						RO_OS.ToString()+"\t"+
						BAZZAR_OS.ToString()+"\t"+
						OE_OS.ToString()+"\t"+
						FLEET_OS.ToString()+"\t"+
						TOTAL_OS.ToString()+"\t"+
						Closing_Stock.ToString()+"\t"+
						NODC.ToString());

				}
				else if(dropType.SelectedIndex==6)
				{
					try
					{
						InventoryClass obj=new InventoryClass();
	
						i=1;
						sql="select cust_type,cust_name,city,cust_id from customer where cust_type='ESSAR RO' order by cust_name";
						dtr=obj.GetRecordSet(sql);
						if(dtr.HasRows)
						{
							sw.WriteLine("SRSM 14\t");
							sw.WriteLine("ESSAR RO SALES ("+GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())+")");
							sw.Write("\t\t");
							for(int m=0;m<DateFrom.Length;m++)
							{
								sw.Write("\t"+GetMonthName(DateFrom[m].ToString())+"\t\t");
							}
							sw.Write("Total");
							sw.WriteLine();
	 
							sw.Write("SL NO.\tNAME OF ESSAR RO\t");

							for(int m=0;m<DateFrom.Length;m++)
							{
								sw.Write("2T SALES \tOTHER SALE \tTOTAL SALE\t");
							}
							sw.Write("2T SALES \tOTHER SALES\tTOTAL SALES\t");
							sw.WriteLine();
							while(dtr.Read())
							{
								Total_2t=0;
								Total_Other=0;
								Total_Tot=0;
								sw.Write(i.ToString()+"\t"+dtr["cust_name"].ToString()+"\t");
								for(int m=0;m<DateFrom.Length;m++)
								{
									InventoryClass obj1=new InventoryClass();
									sql="select distinct sum(v.oil2t) TT_sale,sum(v.totalqty - v.oil2t) OTH_sale,sum(v.totalqty) Tot_sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and c.cust_id='"+dtr["cust_id"].ToString()+"' and cust_type='ESSAR RO' and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+DateFrom[m].ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+DateTo[m].ToString()+"' group by cust_type,cust_name,city,city";
									dtr1=obj1.GetRecordSet(sql);
									if(dtr1.Read())
									{
										sw.Write(Math.Round(double.Parse(dtr1["TT_sale"].ToString()),2)+"\t");
										sw.Write(Math.Round(double.Parse(dtr1["OTH_sale"].ToString()),2)+"\t");
										sw.Write(Math.Round(double.Parse(dtr1["Tot_sale"].ToString()),2)+"\t");
										Total_2t+=Math.Round(double.Parse(dtr1["TT_sale"].ToString()),2);
										Total_Other+=Math.Round(double.Parse(dtr1["OTH_sale"].ToString()),2);
										Total_Tot+=Math.Round(double.Parse(dtr1["Tot_sale"].ToString()),2);
									}
									else
									{
										sw.Write("0\t0\t0\t");
	 
									}
									dtr1.Close();
								}
								sw.Write(Total_2t.ToString()+"\t");
								sw.Write(Total_Other.ToString()+"\t");
								sw.Write(Total_Tot.ToString()+"\t");
								sw.WriteLine();
								i++;
							}
							dtr.Close();

						}
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message.ToString());
					}
				}

				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}
	}
}
