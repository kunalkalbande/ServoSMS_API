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
using DBOperations; 
using System.Data.OleDb;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for LubeIndent1.
	/// </summary>
	public partial class HSD_Customer_Sale : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		protected System.Web.UI.WebControls.DropDownList DropMonth;
		protected System.Web.UI.WebControls.DropDownList DropYear;
		//string strOrderBy="";
		string StartDate="",EndDate="";
		public string uid;
		public int j=0;
		SqlConnection con1;
		OleDbConnection con;
		public OleDbDataReader dtr=null;
		SqlDataReader rdr1=null;
		//protected System.Web.UI.WebControls.DataGrid DataGrid1;
		SqlCommand cmd;
		public int i=1;
		public static int flage=0;
		protected System.Web.UI.HtmlControls.HtmlInputFile file1;
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
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
				
			}
			/*if(DropMonth.SelectedIndex!=0)
			{
				int i=DropMonth.SelectedIndex;
				StartDate=i+"/1/"+DropYear.SelectedItem.Text;
				int day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),i);
				EndDate=i+"/"+day+"/"+DropYear.SelectedItem.Text;
			}*/

			

			if(!IsPostBack)
			{
				btnShow.Enabled=false;
				//DropYear.SelectedIndex=DropYear.Items.IndexOf(DropYear.Items.FindByValue(DateTime.Now.Year.ToString()));
				txtDateFrom.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				txtDateTo.Text=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="17";
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
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
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

		public string GetIndent_MS(string name,string Place)
		{
			string connstring1 = "Provider=Microsoft.Jet.OLEDB.4.0;"+ "Data Source=c:/Servosms_ExcelFile/Import/HSD_Customer_Sale.xls;"+"Extended Properties=Excel 8.0;";
			con =new OleDbConnection(connstring1);
			con.Open();
			OleDbCommand cmd=new OleDbCommand(" select MS from [HSD$] where name='"+name.Trim()+"' and Destination='"+Place.Trim()+"'",con);
			OleDbDataReader rdr=cmd.ExecuteReader();
			string str="";
			//if(DropYear.SelectedIndex!=0 && DropMonth.SelectedIndex!=0)
			//{
			//dbobj.SelectQuery(" select MS from HSD where name='"+name.Trim()+"' and Destination='"+Place.Trim()+"'",ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					str = rdr["MS"].ToString();
				}
			}
			dbobj.Dispose();
			//}
			return str;

			
		}
		public string GetIndent_HSD(string name,string Place)
		{
			string connstring1 = "Provider=Microsoft.Jet.OLEDB.4.0;"+ "Data Source=c:/Servosms_ExcelFile/Import/HSD_Customer_Sale.xls;"+"Extended Properties=Excel 8.0;";
			con =new OleDbConnection(connstring1);
			con.Open();
			OleDbCommand cmd=new OleDbCommand(" select HSD from [HSD$] where name='"+name.Trim()+"' and Destination='"+Place.Trim()+"'",con);
			OleDbDataReader rdr=cmd.ExecuteReader();
			string str="";
			//if(DropYear.SelectedIndex!=0 && DropMonth.SelectedIndex!=0)
			//{
			//OleDbDataAdapter mycommand = new OleDbDataAdapter();
			//dbobj.SelectQuery(" select HSD from [HSD$] where name='"+name.Trim()+"' and Destination='"+Place.Trim()+"'",ref rdr);
			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					str = rdr["HSD"].ToString();
				}
			}
			dbobj.Dispose();
			//}
			return str;
		}
		//public SqlDataReader dtr;
		public void getdata()
		{
			try
			{
				//coment by vikas 5.12.2012 string connstring1 = "Provider=Microsoft.Jet.OLEDB.4.0;"+ "Data Source=c:/Servosms_ExcelFile/Import/HSD_Customer_Sale.xls;"+"Extended Properties=Excel 8.0;";
				string s=tempPath.Value.ToString();
				string connstring1 = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source="+ tempPath.Value +".xls;"+"Extended Properties=Excel 8.0;";
				
				con =new OleDbConnection(connstring1);
				con.Open();
				
				//con.Open();
				//OleDbCommand mycommand = new OleDbCommand("select * from [HSD$]",con);
				//rdr=mycommand.ExecuteReader();

				//string sql="select * from [Sheet1$]";
				string sql="select distinct name,(select sum(qty) from [Sheet1$] where name=t.name and Material like 'MS%' or Material like 'XTRA PREMIUM%'),(select sum(qty) from [Sheet1$] where name=t.name and Material like 'HSD%' or Material like 'XtraMile Super%') from [Sheet1$] t group by name";
				//string sql="select sum(qty) from [temp$] where Material like 'MS%' or Material like 'XTRA PREMIUM%'";
				OleDbCommand cmd1=new OleDbCommand(sql,con);
				dtr=cmd1.ExecuteReader();

				//InventoryClass obj=new InventoryClass();
				
				//string sql="select name, (select sum(material) from temp_Cust_Sale_MS_HSD where (destination like 'MS%' or destination like 'XTRA PREMIUM%')) MS,(select sum(material) from temp_Cust_Sale_MS_HSD where (destination like 'HSD%' or destination like 'XtraMile Super%')) HSD from temp_Cust_Sale_MS_HSD group by name";
				//dtr=obj.GetRecordSet(sql);
				

				/*while(dtr.Read())
				{
						
					string cust_name=dtr.GetValue(0).ToString();
					cust_name=cust_name.Substring(0,15);
					InventoryClass obj=new InventoryClass();
					SqlDataReader dtr2=null;
					sql="select Cust_id,Cust_Name,cust_type,city from Customer where Cust_Name like '"+cust_name.ToString()+"%'";
					dtr2=obj.GetRecordSet(sql);
					while(dtr2.Read())
					{
								
					}
					dtr2.Close();
							
				}
				dtr.Close();*/
				//InventoryClass obj=new InventoryClass();
				//SqlDataReader dtr2=null;
				//string sql="select Cust_id,cust_type,city from Customer where Cust_Name like 'LADS AUTOMOBILE%'";
				//dtr2=obj.GetRecordSet(sql);
				//while(dtr.Read())
				//{
				//		sql=dtr.GetValue(0).ToString();
				//		sql=sql.Substring(0,15);
				//}
				//	dtr.Close();

				//con1 =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				//DataGrid1.DataSource=null;
				//**************************
				//if(DropMonth.SelectedIndex==0)
				//{
				//OleDbDataAdapter mycommand = new OleDbDataAdapter("select * from [HSD$]",connstring1);
				//DataSet mydataset = new DataSet();
				//mycommand.Fill(mydataset,"ExcelInfo");
				//DataGrid1.DataSource=mydataset.Tables["ExcelInfo"].DefaultView;
				//DataGrid1.DataBind();
				//DataGrid1.Visible=true;
				//}
				//else
				//{
					
				/*	con1.Open();
					string sql=" select * from SD_HSD_Cust_Sale where sd_hsd_id='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' ";
					//dbobj.SelectQuery(" select * from SD_HSD_Cust_Sale where sd_hsd_id='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' ",ref rdr1);
					cmd1= new SqlCommand(sql,con1);
					dtr=cmd1.ExecuteReader();
					if(dtr.HasRows)
					{
						DataGrid1.DataSource=dtr;
						DataGrid1.DataBind();
						DataGrid1.Visible=true;
					}
					else
					{
						OleDbDataAdapter mycommand = new OleDbDataAdapter("select * from [HSD$]",connstring1);
						DataSet mydataset = new DataSet();
						mycommand.Fill(mydataset,"ExcelInfo");
						DataGrid1.DataSource=mydataset.Tables["ExcelInfo"].DefaultView;
						DataGrid1.DataBind();
						DataGrid1.Visible=true;
					}
					con1.Close();
					dtr.Close();*/
				//}
				//*************************
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:page_load"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
				MessageBox.Show("Please Store Your Excel File In C:/ServoSMSExcelFile/Import");
				//DataGrid1.Visible=false;
				
				return;
			}
		}

		/// <summary>
		/// This method return the total receipt of given this period.
		/// </summary>
		public string getReceipt(string rec,string packqty)
		{
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(qty) from purchase_details pd,purchase_master pm where pd.invoice_no=pm.invoice_no and pd.prod_id =(select p.prod_id from indent_lube il,products p where p.prod_code=il.prodcode and il.packqty=p.total_qty and ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex.ToString()+"' and il.prodcode='"+rec+"' and il.packqty='"+packqty+"') and cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate+"' group by prod_id");
			if(rdr.Read())
			{
				return rdr.GetValue(0).ToString();
			}
			else
			{
				return "0";
			}
		}
		
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
				flage=1;
				getdata();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				dropName.Visible=true;
				txtName.Visible=false;
				btnSave.Text="Update";
				InventoryClass obj=new InventoryClass();
				SqlDataReader SqlDtr;
				string sql=" Select distinct Period_Name from Cust_Sale_MS_HSD";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					dropName.Items.Add(SqlDtr["Period_Name"].ToString());
				}
				SqlDtr.Close();
			}
			catch(Exception ex)
			{
			
			}
		}

		protected void dropName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				flage=1;
				InventoryClass obj=new InventoryClass();
				SqlDataReader dtr=null;
				string sql="select * from cust_sale_ms_hsd where period_name='"+dropName.SelectedItem.Text.ToString()+"'";
				dtr=obj.GetRecordSet(sql);
				if(dtr.Read())
				{
					txtDateFrom.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr["datefrom"].ToString()));
					txtDateTo.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr["dateto"].ToString()));
				}
				dtr.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}

		}

		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				
				if(flage!=0)
				{
					
					con1 =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
					con1.Open();
					string sql="";
					string customer="";
					string ms="";
					string hsd="";
					string CSMH_id="";
					string message=" Record Save Successfully ";
					InventoryClass obj=new InventoryClass();
					SqlDataReader dtr=null;
					string Next_Period_Id=GetNextPeriodID();
					for(int j=1;j<flage;j++)
					{
						string Next_Id=GetNextID();
						string Period_Name=txtName.Text.ToString();

						if(Request.Params.Get("dropcustomer"+j)!=null && Request.Params.Get("dropcustomer"+j)!="" )
							customer=Request.Params.Get("dropcustomer"+j);
						else
							customer=" ";

						string[] Cust=customer.Split(new char[] {':'});
						
						if(Request.Params.Get("MS"+j)!=null && Request.Params.Get("MS"+j)!="" )
							ms=Request.Params.Get("MS"+j);
						else
							ms="0";
						
						if(Request.Params.Get("HSD"+j)!=null && Request.Params.Get("HSD"+j)!="" )
							hsd=Request.Params.Get("HSD"+j);
						else
							hsd="0";
						
						if(btnSave.Text=="Update")
						{
							sql="select * from cust_sale_ms_hsd where period_name='"+dropName.SelectedItem.Text.ToString()+"' and cust_id="+Cust[1].ToString()+" and (cast(floor(cast(datefrom as float)) as datetime)>='"+ToMMddYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+ToMMddYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+ToMMddYYYY(txtDateFrom.Text)+"' and '"+ToMMddYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+ToMMddYYYY(txtDateFrom.Text)+"' and '"+ToMMddYYYY(txtDateTo.Text)+"')";
							dtr=obj.GetRecordSet(sql);
							if(dtr.HasRows)
							{
								while(dtr.Read())
								{
									CSMH_id=dtr["SD_HSD_id"].ToString();

									txtDateFrom.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr["datefrom"].ToString()));
									txtDateTo.Text=GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr["dateto"].ToString()));
								}
								sql="update Cust_Sale_MS_HSD set ms='"+ms.ToString()+"',hsd='"+hsd.ToString()+"' where sd_hsd_id="+CSMH_id.ToString();
								message=" Record Update Successfully ";
								cmd=new SqlCommand(sql,con1);
								cmd.ExecuteNonQuery();
							}
							else
							{
								//sql="insert into Cust_Sale_MS_HSD(sd_hsd_id,period_id,period_name,cust_id,ms,hsd,datefrom,dateto) values("+Next_Id.ToString()+","+Next_Period_Id.ToString()+",'"+Period_Name.ToString()+"','"+Cust[1].ToString()+"','"+ms.ToString()+"','"+hsd.ToString()+"','"+ToMMddYYYY(txtDateFrom.Text)+"','"+ToMMddYYYY(txtDateTo.Text)+"')";
								message=" Wrong Process ";
							}
							dtr.Close();
						}
						
						if(btnSave.Text=="Submit")
						{
							sql="insert into Cust_Sale_MS_HSD(sd_hsd_id,period_id,period_name,cust_id,ms,hsd,datefrom,dateto) values("+Next_Id.ToString()+","+Next_Period_Id.ToString()+",'"+Period_Name.ToString()+"','"+Cust[1].ToString()+"','"+ms.ToString()+"','"+hsd.ToString()+"','"+ToMMddYYYY(txtDateFrom.Text)+"','"+ToMMddYYYY(txtDateTo.Text)+"')";
							message=" Record Save Successfully ";
							cmd=new SqlCommand(sql,con1);
							cmd.ExecuteNonQuery();
						}
						
					}
					con1.Close();
					MessageBox.Show(message.ToString());
				}
				else
				{
					MessageBox.Show(" Read Excel file First ");
				}
				flage=0;
				dropName.Visible=false;
				txtName.Visible=true;
				btnSave.Text="Submit";
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
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

		string Next_Id;
		public string GetNextID()
		{
			try
			{
				Next_Id="1";
				InventoryClass obj=new InventoryClass();
				SqlDataReader SqlDtr;
				string sql=" Select max(SD_HSD_Id)+1 Next_Id from Cust_Sale_MS_HSD";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					if(SqlDtr["Next_Id"].ToString()!=null && SqlDtr["Next_Id"].ToString ()!="")
						Next_Id=SqlDtr["Next_Id"].ToString ();
					else
						Next_Id="1";
				}		
				SqlDtr.Close();
				return Next_Id;
			}
			catch(Exception ex)
			{
				return Next_Id;
			}
		}

		string Next_Period_Id;
		public string GetNextPeriodID()
		{
			try
			{
				Next_Period_Id="1";
				InventoryClass obj=new InventoryClass();
				SqlDataReader SqlDtr;
				string sql=" Select max(Period_Id)+1 Next_Id from Cust_Sale_MS_HSD";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					if(SqlDtr["Next_Id"].ToString()!=null && SqlDtr["Next_Id"].ToString ()!="")
						Next_Period_Id=SqlDtr["Next_Id"].ToString ();
					else
						Next_Period_Id="1";
				}		
				SqlDtr.Close();
				return Next_Period_Id;
			}
			catch(Exception ex)
			{
				return Next_Period_Id;
			}
		}
		/// <summary>
		/// This method is used to return the difference between given rec value and indent vaue.
		/// </summary>
		public string getDiff(string rec,string indent)
		{
			double diff=0;
			diff=double.Parse(rec)-double.Parse(indent);
			return diff.ToString();
		}

		/// <summary>
		/// Prepares the excel report file LubeIndent.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ConvertToExcel();
				MessageBox.Show("Successfully Convert File into Excel Format");
				CreateLogFiles.ErrorLog("Form:ClaimSheet.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Secon. Sales Claim Sheet Report Convert Into Excel Format ,  userid  "+uid);
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Lube Indent Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader rdr;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\LubeIndent.xls";
			StreamWriter sw = new StreamWriter(path);
			if(DropMonth.SelectedIndex!=0)
			{
				int k=DropMonth.SelectedIndex;
				StartDate=k+"/1/"+DropYear.SelectedItem.Text;
				int day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),k);
				EndDate=k+"/"+day+"/"+DropYear.SelectedItem.Text;
			}
			string sql="";
			sql="select * from Indent_Lube il,products p where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code order by prodcode";
			//sql=sql+" order by "+Cache["strOrderBy"];
			int i=0;
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("RSE\tSUP.EX.\tRETAIL MPSO\tSKY TYPE\tPACK TYPE\tPACK QTY\tPRODUCT CODE\tSKY NAME WITH PACK\tINDENT\tRECEIPT\tDIFFRENT\tREMARK");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["rse"].ToString()+"\t"+
					rdr["supex"].ToString()+"\t"+
					rdr["retailmpso"].ToString()+"\t"+
					rdr["skutype"].ToString()+"\t"+
					rdr["packcode"].ToString()+"\t"+
					rdr["packqty"].ToString()+"\t"+
					rdr["prodcode"].ToString()+"\t"+
					rdr["skunamewithpack"].ToString()+"\t"+
					rdr["indent"].ToString()+"\t"+
					getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString())+"\t"+
					getDiff(getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),rdr["Indent"].ToString())+"\t"+
					Request.Params.Get("txtRemark"+i++)
					);
			}
			sw.Close();
		}

		/// <summary>
		/// This method is used to write into the report file to print.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			try
			{
				InventoryClass obj=new InventoryClass();
				System.Data.SqlClient.SqlDataReader rdr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\LubeIndent.txt";
				StreamWriter sw = new StreamWriter(path);
				string sql="select * from Indent_Lube il,products p where ssaid='"+DropYear.SelectedItem.Text+DropMonth.SelectedIndex+"' and il.prodcode=p.prod_code order by prodcode";
				string info = "";
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
				//******S***
				sw.WriteLine(GenUtil.GetCenterAddr("=================================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Lube Indent Report For "+DropMonth.SelectedItem.Text,des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("=================================",des.Length));
				sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
				sw.WriteLine("|  RSE  |SUP.EX.|RETAIL MPSO| SKY TYPE |PACK CODE|PACK QTY|PRODUCT CODE| SKU NAME WITH PACK |INDENT|RECEIPT|DIFFRENT|    REMARK         |");
				sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
				//             1234567 1234567 12345678901 1234567890 123456789 12345678 123456789012 12345678901234567890 123456 1234567 12345678 1234567890123456789
				int i=0;
				if(DropMonth.SelectedIndex!=0)
				{
					int k=DropMonth.SelectedIndex;
					StartDate=k+"/1/"+DropYear.SelectedItem.Text;
					int day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),k);
					EndDate=k+"/"+day+"/"+DropYear.SelectedItem.Text;
				}
				info = " {0,-7:S} {1,-7:S} {2,-11:S} {3,-10:S} {4,-9:S} {5,-8:S} {6,-12:S} {7,-20:S} {8,6:S} {9,7:S} {10,8:S} {11,-19:S}";
				rdr=obj.GetRecordSet(sql);
				if(rdr.HasRows)
				{
					while(rdr.Read())
					{
						sw.WriteLine(info,GenUtil.TrimLength(rdr["rse"].ToString(),7),
							GenUtil.TrimLength(rdr["supex"].ToString(),7),
							GenUtil.TrimLength(rdr["retailmpso"].ToString(),11),
							GenUtil.TrimLength(rdr["skutype"].ToString(),10),
							rdr["packcode"].ToString(),
							rdr["packqty"].ToString(),
							rdr["prodcode"].ToString(),
							GenUtil.TrimLength(rdr["skunamewithpack"].ToString(),20),
							rdr["indent"].ToString(),
							getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),
							getDiff(getReceipt(rdr["prodcode"].ToString(),rdr["packqty"].ToString()),rdr["Indent"].ToString()),
							GenUtil.TrimLength(Request.Params.Get("txtRemark"+i++),19)
							);
					}
				}
				sw.WriteLine("+-------+-------+-----------+----------+---------+--------+------------+--------------------+------+-------+--------+-------------------+");
				//dbobj.Dispose();
				sw.Close();
				print();
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:Print() Indent Updated For "+DropMonth.SelectedItem.Text+" 2007");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:update().   EXCEPTION " +ex.Message );
			}
		}

		/// <summary>
		/// Contacts the server and sends the StockMovementReport.txt file name to print.
		/// </summary>
		public void print()
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\LubeIndent.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print, Lube Indent Report are Printed,  userid "+ uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+ane.Message+" userid "+ uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+se.Message+" userid "+ uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
				}
				//CreateLogFiles.ErrorLog("Form:StockMovement.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:LubeIndent.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
			}
		}
	}
}