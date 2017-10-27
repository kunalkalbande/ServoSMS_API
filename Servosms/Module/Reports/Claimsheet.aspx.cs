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
using DBOperations;
using RMG;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for Claimsheet.
	/// </summary>
	public partial class Claimsheet : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid="";

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
                txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
                txtDateTo.Text = Request.Form["txtDateTo"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTo"].ToString().Trim();
            }
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:page_load"+ ex.Message+"EXCEPTION"+uid);	
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				try
				{
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="38";
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
					grdLeg.Visible=false;
                  
                    //System.Data.SqlClient.SqlDataReader rdr1=null;
                    // Fetch store_in locations from products tables, if store_in not available then get the tank.
                    //					dbobj.SelectQuery("select distinct store_in from products",ref rdr);
                    //					while(rdr.Read())
                    //					{
                    //						if(Char.IsDigit(rdr["store_in"].ToString(),0))
                    //						{
                    //							dbobj.SelectQuery("select tank_id,tank_name,prod_name from tank where tank_id="+rdr["store_in"].ToString(),ref rdr1);
                    //							if(rdr1.Read())
                    //							{
                    //							
                    //								drpstore.Items.Add(rdr1["tank_name"].ToString()+":"+rdr1["prod_name"].ToString());					
                    //							}
                    //						}
                    //						else
                    //							drpstore.Items.Add(rdr["store_in"].ToString());					
                    //					}


                    //					drpstore.Items.Insert(0,"All");
                    //					dbobj.Dispose();
                }
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:page_load().  EXCEPTION: "+ ex.Message+" User_ID: "+uid);	
				}
			}
		}

		/// <summary>
		/// if the tank the returns the tank abbrevation name, for that tank.
		/// This method is not used.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected string IsTank(string str)
		{
			string op="";
			if(Char.IsDigit(str,0))
				dbobj.SelectQuery("select top 1 prod_abbname from tank where tank_id='"+str+"'","prod_abbname",ref op);
			if(op.Length>0)
				return op;
			else
				return str;
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		double count=1,i=1,j=2,k=3,l=4,m=5,n=6;
		public double os=0,cs=0,sales=0,rect=0,fsale=0,fpur=0;
		protected double Multiply(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=6;
				}
				if(count==j)
				{
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=6;
				}
				if(count==k)
				{
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=6;
				}
				if(count==l)
				{
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=6;
				}
				if(count==m)
				{
					fsale+=ans;
					Cache["fsale"]=System.Convert.ToString(fsale);
					m+=6;
				}
				if(count==n)
				{
					fpur+=ans;
					Cache["fpur"]=System.Convert.ToString(fpur);
					n+=6;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=6;
					}
					if(count==j)
					{
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=6;
					}
					if(count==k)
					{
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=6;
					}
					if(count==l)
					{
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=6;
					}
					if(count==m)
					{
						fsale+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fsale"]=System.Convert.ToString(fsale);
						m+=6;
					}
					if(count==n)
					{
						fpur+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fpur"]=System.Convert.ToString(fpur);
						n+=6;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
		}

		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="strfoc"></param>
		/// <param name="foc"></param>
		/// <returns></returns>
		protected double Multiply1(string str,string strfoc,string foc)
		{
			//string[] mystr=str.Split(new char[]{'X'},str.Length);
			//*************
			string[] mystr=null;
			if(double.Parse(foc)==0)
				mystr=str.Split(new char[]{'X'},str.Length);
			else
				mystr=strfoc.Split(new char[]{'X'},str.Length);
			//*************
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=6;
				}
				if(count==j)
				{
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=6;
				}
				if(count==k)
				{
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=6;
				}
				if(count==l)
				{
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=6;
				}
				if(count==m)
				{
					fsale+=ans;
					Cache["fsale"]=System.Convert.ToString(fsale);
					m+=6;
				}
				if(count==n)
				{
					fpur+=ans;
					Cache["fpur"]=System.Convert.ToString(fpur);
					n+=6;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=6;
					}
					if(count==j)
					{
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=6;
					}
					if(count==k)
					{
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=6;
					}
					if(count==l)
					{
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=6;
					}
					if(count==m)
					{
						fsale+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fsale"]=System.Convert.ToString(fsale);
						m+=6;
					}
					if(count==n)
					{
						fpur+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fpur"]=System.Convert.ToString(fpur);
						n+=6;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
		}
		//************
		
		/// <summary>
		/// This Method multiplies the package quantity with Quantity.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		protected double Multiplyfoc(string str)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
			
		}
		//**************
		
		/// <summary>
		/// This method is used to split the date and return the date.
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
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Method:sortcommand_click"+ "  EXCEPTION "+ex.Message+"  userid  "+uid);
			}
		}
		
		/// <summary>
		/// This method is used to bind the datagrid with the help of query.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//***********
			grdLeg.Visible=true;
			int x=0;
			object op=null;	
			int count=0;
			string sql="";
			System.Data.SqlClient.SqlDataReader rdr=null;
			//*********comment by Mahesh on 11.08.008
			sql="select distinct productid from stock_master";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			try
			{
				while(rdr.Read())
				{
					dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@strfromdate",getdate(Request.Form["txtDateFrom"].ToString(), true).Date.ToShortDateString(),"@strtodate",getdate(Request.Form["txtDateTo"].ToString(), true).Date.ToShortDateString());
					count++;
				}
				rdr.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("stock : "+ex+"  "+count);
			}
			///*******************/
			//			if(drpstore.SelectedIndex>0)
			//			{
			//				
			//				sql="select * from stkmv where Location='"+drpstore.SelectedItem.Value +"'";
			//				
			//				
			//			}
			//			else
			//			{
			//sql="select * from stkmv s,products p where p.prod_id=s.prod_id";
			sql="select * from stkmv s,products p where p.prod_id=s.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
				
			//			}         
			//***********
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"stkmv");
			DataTable dtcustomer=ds.Tables["stkmv"];
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			//coment by vikas 1.11.2012 grdLeg.DataSource=dv;
			
			/*******Add by vikas 1.11.2012****************/
			if(dropType.SelectedIndex==0)
				grdLeg.DataSource=dv;
			else
				grdnewdata.DataSource=dv;
			/*******End****************/

			if(dv.Count!=0)
			{
				//coment by vikas 1.11.2012 grdLeg.DataBind();
				//coment by vikas 1.11.2012 grdLeg.Visible=true;
				/*******Add by vikas 1.11.2012****************/
				grdLeg.Visible=false;
				grdnewdata.Visible=false;
				if(dropType.SelectedIndex==0)
				{
					grdLeg.DataBind();
					grdLeg.Visible=true;
				}
				else
				{
					grdnewdata.DataBind();
					grdnewdata.Visible=true;
				}
				/*******End****************/
			}
			else
			{
				grdLeg.Visible=false;
				grdnewdata.Visible=false;http://localhost/Servosms/HeaderFooter/images/headersms1.jpg
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
			// truncate table after use.
			//dbobj.Insert_or_Update("truncate table stkmv", ref x);
		}

		/// <summary>
		/// This method is used to Prepares the report file ClaimSheet.txt for printing.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void prnButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				
				//string info1 =" {0,-30:S} {1,-15:S} {2,6:F} {3,8:F} {4,6:F} {5,8:F} {6,6:F} {7,8:F} {8,6:F} {9,8:F}";
				string sql         = "";

				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\claimsheetReport.txt";
				StreamWriter sw = new StreamWriter(path);
				System.Data.SqlClient.SqlDataReader rdr=null;
				sql="select distinct productid from stock_master";
				dbobj.SelectQuery(sql,ref rdr);
				object op=null;
				while(rdr.Read())
					dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@strfromdate",getdate(Request.Form["txtDateFrom"].ToString().Trim(),true).Date.ToShortDateString(),"@strtodate",getdate(Request.Form["txtDateTo"].ToString().Trim(),true).Date.ToShortDateString());

                rdr.Close();
				
				//sql="select * from stkmv s,products p where s.Prod_id=p.Prod_id";
				sql="select * from stkmv s,products p where p.prod_id=s.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";

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
				string des="";
				if(dropType.SelectedIndex==0)
				{
					des="-------------------------------------------------------------------------------------------------------------------------------";
				}
				else
				{
					des="-------------------------------------------------------------------------------------------------------------------------------";
				}

				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				if(dropType.SelectedIndex==0)
				{
					sw.WriteLine(GenUtil.GetCenterAddr("================================================",des.Length));			
					sw.WriteLine(GenUtil.GetCenterAddr("CLAIM SHEET REPORT FROM "+txtDateFrom.Text+" TO "+txtDateTo.Text,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("================================================",des.Length));
					//sw.WriteLine("From : "+txtDateFrom.Text);
					//sw.WriteLine("To   : "+txtDateTo.Text);
				
					sw.WriteLine("");
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
					sw.WriteLine("| Product |                                   | Opening  | Receipt  |Receipt|  Sales   | Sales | Closing  |Discount|   Total  |");
					sw.WriteLine("|  Code   |           Product Name            |  Stock   |          | FOC   |          |  FOC  | Stock    |        |          |");
					sw.WriteLine("|         |                                   |  Lt./Kg  | Lt./Kg   | Lt./Kg| Lt./Kg   | Lt./Kg| Lt./Kg   |        |          |"); 
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
					//             123456789 12345678901234567890123456789012345 1234567890 1234567890 1234567 1234567890 1234567 1234567890 12345678 1234567890
					string info = " {0,-9:S} {1,-35:S} {2,10:F} {3,10:F} {4,7:F} {5,10:F} {6,7:F} {7,10:F} {8,8:F} {9,10:F}";
					if(rdr.HasRows)
					{
						while(rdr.Read())
						{
						
							sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),
								StringUtil.trimlength(rdr["prod_name"].ToString().Trim(),35),
								Math.Round(Multiply(rdr["op"].ToString().Trim()+"X" +rdr["pack_type"].ToString().Trim())),
								Multiply(rdr["rcpt"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								Multiply(rdr["rcptfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								//Multiply(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								Multiply1(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()),
								Multiply(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								Multiply(rdr["cs"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								discount(rdr["prod_name"].ToString().Trim()),
								//Coment By vikas 5.3.2013 totalfoc(Multiplyfoc(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),Multiplyfoc(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),discount(rdr["prod_name"].ToString().Trim())));
								totalfoc_new(Multiplyfoc(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),Multiplyfoc(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),discount(rdr["prod_name"].ToString().Trim()),rdr["prod_id"].ToString()));
						}
					}
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
					sw.WriteLine(info,"Total:","",Cache["os"],Cache["rect"],Cache["sales"],Cache["cs"],Cache["fsale"],Cache["fpur"],"",Cache["foctotal"]);
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
				}
				else
				{
					sw.WriteLine(GenUtil.GetCenterAddr("===================================================",des.Length));			
					sw.WriteLine(GenUtil.GetCenterAddr("NEW CLAIM SHEET REPORT FROM "+txtDateFrom.Text+" TO "+txtDateTo.Text,des.Length));
					sw.WriteLine(GenUtil.GetCenterAddr("===================================================",des.Length));
					sw.WriteLine("");
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
					sw.WriteLine("| Product |                                   | Opening  | Receipt  |Receipt|  Sales   | Sales | Closing  |Discount|   Total  |");
					sw.WriteLine("|  Code   |           Product Name            |  Stock   |          | FOC   |          |  FOC  | Stock    |        |          |");
					sw.WriteLine("|         |                                   |  Lt./Kg  | Lt./Kg   | Lt./Kg| Lt./Kg   | Lt./Kg| Lt./Kg   |        |          |"); 
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
					//             123456789 12345678901234567890123456789012345 1234567890 1234567890 1234567 1234567890 1234567 1234567890 12345678 1234567890
					string info = " {0,-9:S} {1,-35:S} {2,10:F} {3,10:F} {4,7:F} {5,10:F} {6,7:F} {7,10:F} {8,8:F} {9,10:F}";
					if(rdr.HasRows)
					{
						while(rdr.Read())
						{
							sw.WriteLine(info,rdr["Prod_Code"].ToString().Trim(),
								StringUtil.trimlength(rdr["prod_name"].ToString().Trim(),35),
								Math.Round(Multiply(rdr["op"].ToString().Trim()+"X" +rdr["pack_type"].ToString().Trim())),
								Multiply(rdr["rcpt"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								Multiply(rdr["rcptfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								Multiply1(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()),
								Multiply(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								Multiply(rdr["cs"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),
								discount(rdr["prod_name"].ToString().Trim()),
								totalfoc(Multiplyfoc(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),Multiplyfoc(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),discount(rdr["prod_name"].ToString().Trim())));
						}
					}
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
					sw.WriteLine(info,"Total:","",Cache["os"],Cache["rect"],Cache["sales"],Cache["cs"],Cache["fsale"],Cache["fpur"],"",Cache["foctotal"]);
					sw.WriteLine("+---------+-----------------------------------+----------+----------+-------+----------+-------+----------+--------+----------+");
				}
				dbobj.Dispose();
				rdr.Close();
				sw.Close();
				print();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:claimsheetReport.aspx,Method:prnButton_Click(). EXCEPTION  "+ex.Message+" userid "+ uid);
			}
		}

		/// <summary>
		/// This method is used to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			//SqlDataReader SqlDtr;
			string sql="";
			int x=0;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SecondarySalesClaimReport.xls";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;
			/********* comment by Mahesh on 11.08.008*/
			sql="select distinct productid from stock_master";
			dbobj.SelectQuery(sql,ref rdr);
			object op=null;
			while(rdr.Read())
				dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@strfromdate",getdate(Request.Form["txtDateFrom"].ToString(),true).Date.ToShortDateString(),"@strtodate",getdate(Request.Form["txtDateTo"].ToString(),true).Date.ToShortDateString());
			rdr.Close();
			/*******************/
			//sql="select * from stkmv s, products p where s.Prod_id=p.Prod_id";
			sql="select * from stkmv s,products p where p.prod_id=s.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
			sql=sql+" order by "+Cache["strorderby"];
			rdr=obj.GetRecordSet(sql);
			
			sw.WriteLine("Product Code\tProduct Name\tOpening Stock\tReceipt\tReceipt FOC\tSales\tSales FOC\tClosing Stock\tDiscount\tTotal");
			while(rdr.Read())
			{
				//sw.WriteLine(SqlDtr["prod_name"].ToString()+"\t"+Multiply(rdr["op"].ToString().Trim()+"X" +rdr["pack_type"].ToString().Trim())+"\t"+Multiply(rdr["rcpt"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+SqlDtr["rcptfoc"].ToString()+"\t"+SqlDtr["Sales"].ToString()+"\t"+SqlDtr["Salesfoc"].ToString()+"\t"+SqlDtr["cs"].ToString()+"\t"+discount(SqlDtr["prod_name"].ToString())+"\t"+totalfoc(Multiplyfoc(SqlDtr["sales"].ToString().Trim()+"X"+SqlDtr["pack_type"].ToString().Trim()),Multiplyfoc(SqlDtr["salesfoc"].ToString().Trim()+"X"+SqlDtr["pack_type"].ToString().Trim()),discount(SqlDtr["prod_name"].ToString().Trim())));
				sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+
					rdr["prod_name"].ToString().Trim()+"\t"+
					Multiply(rdr["op"].ToString().Trim()+"X" +rdr["pack_type"].ToString().Trim())+"\t"+
					//coment by vikas 3.11.2012 Multiply(rdr["rcpt"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					MultiplySJ(rdr["rcpt"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["prod_id"].ToString())+"\t"+
					Multiply(rdr["rcptfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					//Multiply(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					//coment by vikas 3.11.2012 Multiply1(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim())+"\t"+
					MultiplySJ1(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim(),rdr["prod_id"].ToString())+"\t"+
					Multiply(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					Multiply(rdr["cs"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					discount(rdr["prod_name"].ToString().Trim())+"\t"+
					//Coment by vikas 5.3.2013 totalfoc(Multiplyfoc(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),Multiplyfoc(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),discount(rdr["prod_name"].ToString().Trim())));
					totalfoc_new(Multiplyfoc(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),Multiplyfoc(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),discount(rdr["prod_name"].ToString().Trim()),rdr["prod_id"].ToString()));
			}
			sw.WriteLine("Total\t\t"+Cache["os"]+"\t"+Cache["rect"]+"\t"+Cache["sales"]+"\t"+Cache["cs"]+"\t"+Cache["fsale"]+"\t"+Cache["fpur"]+"\t\t"+Cache["foctotal"]);
			rdr.Close();
			sw.Close();
			// truncate table after use.
			dbobj.Insert_or_Update("truncate table stkmv", ref x);
		}

		public void ConvertToExcel_New()
		{
			InventoryClass obj=new InventoryClass();
			//SqlDataReader SqlDtr;
			string sql="";
			int x=0;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\SecondarySalesClaimReport.xls";
			StreamWriter sw = new StreamWriter(path);
			System.Data.SqlClient.SqlDataReader rdr=null;
			
			sql="select distinct productid from stock_master";
			dbobj.SelectQuery(sql,ref rdr);
			object op=null;
			while(rdr.Read())
				dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@strfromdate",getdate(Request.Form["txtDateFrom"].ToString(),true).Date.ToShortDateString(),"@strtodate",getdate(Request.Form["txtDateTo"].ToString(),true).Date.ToShortDateString());
			rdr.Close();

			sql="select * from stkmv s,products p where p.prod_id=s.prod_id and (op<>0 or sales<>0 or rcpt<>0 or cs<>0 or salesfoc<>0 or rcptfoc<>0)";
			sql=sql+" order by "+Cache["strorderby"];
			rdr=obj.GetRecordSet(sql);
			
			sw.WriteLine("Product Code\tProduct Name\tOpening Stock\tReceipt\tReceipt FOC\tSales\tSales FOC\tClosing Stock\tSale RO\tDisc. RO\tTotal RO Discount\tSale Bazzar\tDisc. Bazzar\tTotal Bazzar Discount\tTotal RO Bazzar Discount");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["Prod_Code"].ToString().Trim()+"\t"+
					rdr["prod_name"].ToString().Trim()+"\t"+
					Multiply(rdr["op"].ToString().Trim()+"X" +rdr["pack_type"].ToString().Trim())+"\t"+
					MultiplySJ(rdr["rcpt"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["prod_id"].ToString())+"\t"+
					Multiply(rdr["rcptfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					MultiplySJ1(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim(),rdr["salesfoc"].ToString().Trim(),rdr["prod_id"].ToString())+"\t"+
					Multiply(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					Multiply(rdr["cs"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim())+"\t"+
					Sale_RO(rdr["prod_name"].ToString())+"\t"+
					discount_RO(rdr["prod_name"].ToString())+"\t"+
					totalRo()+"\t"+
					Sale_Bazzar(rdr["prod_name"].ToString())+"\t"+
					discount_Bazzar(rdr["prod_name"].ToString())+"\t"+
					totalBazz()+"\t"+
					totalRoBazz());

				//	totalfoc(Multiplyfoc(rdr["sales"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),Multiplyfoc(rdr["salesfoc"].ToString().Trim()+"X"+rdr["pack_type"].ToString().Trim()),discount(rdr["prod_name"].ToString().Trim())));
			}

			//Cache["TotBazzarSale"],Cache["TotRoSale"],Cache["TotBazzarDisc"],Cache["Tot_DiscRO"]
			sw.WriteLine("Total\t\t"+Cache["os"]+"\t"+Cache["rect"]+"\t\t"+Cache["sales"]+"\t\t"+Cache["cs"]+"\t"+Cache["TotRoSale"]+"\t\t"+Cache["Tot_DiscRO"]+"\t"+Cache["TotBazzarSale"]+"\t\t"+Cache["TotBazzarDisc"]+"\t"+GTotROBazz.ToString());
			rdr.Close();
			sw.Close();

			dbobj.Insert_or_Update("truncate table stkmv", ref x);
			
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
		/// This method is used to view the report and set the session variable.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void cmdrpt_Click(object sender,System.EventArgs e)
		{
			try
			{
					strorderby="Prod_Code ASC";
					Session["Column"]="Prod_Code";
					Session["order"]="ASC";
					Bindthedata();
				
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:cmdrpt_Click  claimsheet Report  Viewed  useried "+uid);	
			}
			catch(Exception ex)
			{
				
				CreateLogFiles.ErrorLog("Form:claimsheet.aspx,Class:DBOperation_LETEST.cs,Method:cmdrpt_Click,  claimsheet Report  Viewed  EXCEPTION "+ ex.Message+"  userid  "+uid);		
			}	
		}


		/// <summary>
		/// This method is used to calculate the discount of given product name.
		/// </summary>
		public double discount(string prodname)
		{
			string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			double dis;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+str[0]+"' and pack_type='"+str[1]+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			//string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//coment by vikas 2.11.2012 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and  cast(floor(cast(o.datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//coment by vikas 5.3.2013 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and  (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";     //Add by vikas 2.11.2012
			
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and cast(floor(cast(datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and  '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' <= cast(floor(cast(dateto as float)) as datetime)"; 
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis=0;
			rdr.Close();
			return dis;
			
		}
		public double discount2(string prodname)
		{
			string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			double dis;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+str[0]+"' and pack_type='"+str[1]+"'";
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry SP(LTRSP Scheme)' and cast(floor(cast(datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and  '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' <= cast(floor(cast(dateto as float)) as datetime)"; 
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis=0;
			rdr.Close();
			return dis;
		}

		public double Tot_SaleRO;
		public string SaleRO="";
		public double Sale_RO(string prodname)
		{
			string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="",Pack_Type="";
			double Sale_Ro;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+str[0]+"' and pack_type='"+str[1]+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			//string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//coment by vikas 2.11.2012 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and  cast(floor(cast(o.datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and  (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";     //Add by vikas 2.11.2012

			string sql1="select sum(quant) Total_qty,Pack_Type from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and ct.group_name like '%Ro%' and prod_id="+prodid+" and  (cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' or cast(floor(cast(invoice_date as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim()) +"' and '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"') group by pack_type";

			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				if(rdr["Total_qty"].ToString().Trim()!=null && rdr["Total_qty"].ToString().Trim()!="")
					Sale_Ro=System.Convert.ToDouble(rdr["Total_qty"].ToString());
				else
					Sale_Ro=0;

				if(rdr["Pack_Type"].ToString().Trim()!=null && rdr["Pack_Type"].ToString().Trim()!="")
					Pack_Type=rdr["Pack_Type"].ToString();
				else
					Pack_Type="";
			}
			else
				Sale_Ro=0;
			rdr.Close();
			if(Pack_Type!="")
			{
				string[] Pack=Pack_Type.Split(new char[] {'X'},Pack_Type.Length);
				Sale_Ro=Sale_Ro*double.Parse(Pack[0].ToString())*double.Parse(Pack[1].ToString());
			}
			SaleRO=Sale_Ro.ToString();
			Tot_SaleRO+=Sale_Ro;
			Cache["TotRoSale"]=Tot_SaleRO;
			return Sale_Ro;
		}

		public double Tot_SaleBazz;
		public string SaleBazz="";
		public double Sale_Bazzar(string prodname)
		{
			string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="",Pack_Type="";
			double Sale_bazzar;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+str[0]+"' and pack_type='"+str[1]+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			string sql1="select sum(quant) Total_qty,pack_type from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and (ct.group_name like '%Bazzar%' or ct.group_name like '%OE%' or ct.group_name like '%Fleet%') and prod_id="+prodid+" and  (cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' or cast(floor(cast(invoice_date as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim()) +"' and '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"') group by pack_type ";
			
			//(ct.group_name like '%Bazzar%' or ct.group_name like '%OE%' or ct.group_name like '%Fleet%')
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				if(rdr["Total_qty"].ToString().Trim()!=null && rdr["Total_qty"].ToString().Trim()!="")
					Sale_bazzar=System.Convert.ToDouble(rdr.GetValue(0).ToString());
				else
					Sale_bazzar=0;

				
			}
			else
				Sale_bazzar=0;
			rdr.Close();
			
			if(str[1]!="")
			{
				string[] Pack=str[1].Split(new char[] {'X'},str[1].Length);
				Sale_bazzar=Sale_bazzar*double.Parse(Pack[0].ToString())*double.Parse(Pack[1].ToString());
			}

			SaleBazz=Sale_bazzar.ToString();
			Tot_SaleBazz+=Sale_bazzar;
			Cache["TotBazzarSale"]=Tot_SaleBazz;
			return Sale_bazzar;
		}

		double foctotal=0;
		/// <summary>
		/// This method is used to get the FOC qty and also get the total FOC qty.
		/// </summary>
		public double totalfoc_new(double sale,double salefoc,double dis,string prodid)
		{
			/****************Add by vikas 5.3.2013*******************************/
			sale=0;
			SqlDataReader rdr=null;
			string sql1="select sum(quant) Qty,Total_qty,pack_type from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and prod_id="+prodid+" and  (cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' or cast(floor(cast(invoice_date as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim()) +"' and '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"') group by pack_type,Total_qty ";
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				if(rdr["Total_qty"].ToString().Trim()!=null && rdr["Total_qty"].ToString().Trim()!="")
					sale=System.Convert.ToDouble(rdr.GetValue(0).ToString())*System.Convert.ToDouble(rdr.GetValue(1).ToString());
				else
					sale=0;
			}
			else
				sale=0;
			rdr.Close();
			/***************End********************************/

			double tot=(sale-salefoc)*dis;
			foctotal+=tot;
			Cache["foctotal"]=foctotal;
			return tot;
		}

		double foctotal2=0;
		public double totalfoc_new2(double sale,double salefoc,double dis,string prodid)
		{
			sale=0;
			SqlDataReader rdr=null;
			string sql1="select sum(quant) Qty,Total_qty,pack_type from vw_SaleBook sb,customertype ct where sb.cust_type=ct.customertypename and prod_id="+prodid+" and  (cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' or cast(floor(cast(invoice_date as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim()) +"' and '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"') group by pack_type,Total_qty ";
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				if(rdr["Total_qty"].ToString().Trim()!=null && rdr["Total_qty"].ToString().Trim()!="")
					sale=System.Convert.ToDouble(rdr.GetValue(0).ToString())*System.Convert.ToDouble(rdr.GetValue(1).ToString());
				else
					sale=0;
			}
			else
				sale=0;
			rdr.Close();
			double tot=(sale-salefoc)*dis;
			foctotal2+=tot;
			Cache["foctotal2"]=foctotal2;
			return tot;
		}

		public double totalfoc(double sale,double salefoc,double dis)
		{
			double tot=(sale-salefoc)*dis;
			foctotal+=tot;
			Cache["foctotal"]=foctotal;
			return tot;
		}


		double totalDisc=0;
		/// <summary>
		/// This method is used to get the FOC qty and also get the total FOC qty.
		/// </summary>
		public double GrandTotRO;
		public double Ro=0;
		public double totalRo()
		{
			double sale1=double.Parse(SaleRO.ToString());
			double dis1=double.Parse(DiscRO.ToString());
			double tot=sale1*dis1;
			totalDisc+=tot;
			//Cache["totalDisc"]=totalDisc;
			Ro=tot;
			GrandTotRO+=tot;
			return tot;
		}
		public double GrandTotBazzar;
		public double Bazzar=0;
		public double totalBazz()
		{
			double sale1=double.Parse(SaleBazz.ToString());
			double dis1=double.Parse(DiscBazz.ToString());
			double tot=sale1*dis1;
			totalDisc+=tot;
			Cache["totalDisc"]=totalDisc;
			Bazzar=tot;
			GrandTotBazzar+=tot;
			return tot;
		}
		
		public double GTotROBazz;
		public double totalRoBazz()
		{
			double tot=Bazzar+	Ro;
			totalDisc+=tot;
			Cache["totalDisc"]=totalDisc;
			GTotROBazz+=tot;
			return tot;
		}
		/// <summary>
		/// check if the products type is fuel or package is Loose Oil then return space.
		/// </summary>
		double count1=1,i1=1,j1=2,k1=3,l1=4,m1=5,n1=6;
		public double osp=0,csp=0,salesp=0,rectp=0,fsalep=0,fpurp=0;
		protected string Check(string cs, string type,string pack)
		{
			if(type.ToUpper().Equals("FUEL") || pack.IndexOf("Loose")!= -1)
				return "&nbsp;";	
			else
				//**********************
				if(count1==i1)
			{
				osp+=System.Convert.ToDouble(cs);
				Cache["osp"]=System.Convert.ToString(osp);
				i1+=6;
			}
			if(count1==j1)
			{
				rectp+=System.Convert.ToDouble(cs);
				Cache["rectp"]=System.Convert.ToString(rectp);
				j1+=6;
			}
			if(count1==k1)
			{
				salesp+=System.Convert.ToDouble(cs);
				Cache["salesp"]=System.Convert.ToString(salesp);
				k1+=6;
			}
			if(count1==l1)
			{
				csp+=System.Convert.ToDouble(cs);
				Cache["csp"]=System.Convert.ToString(csp);
				l1+=6;
			}
			if(count1==m1)
			{
				fsalep+=System.Convert.ToDouble(cs);
				Cache["fsalep"]=System.Convert.ToString(fsalep);
				m1+=6;
			}
			if(count1==n1)
			{
				fpurp+=System.Convert.ToDouble(cs);
				Cache["fpurp"]=System.Convert.ToString(fpurp);
				n1+=6;
			}
			count1++;
			//**********************
			return cs;
		}
		public double Tot_DiscBazz;
		public string DiscBazz="";
		public double discount_Bazzar(string prodname)
		{
			string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			double dis_Bazzr;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+str[0]+"' and pack_type='"+str[1]+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			//string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//coment by vikas 2.11.2012 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and  cast(floor(cast(o.datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			
			//coment by vikas 5.3.2013 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and Group_Name like '%Bazzar%' and schname='Secondry(LTR Scheme)' and  (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";     //Add by vikas 2.11.2012
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and Group_Name like '%Bazzar%' and schname='Secondry(LTR Scheme)' and cast(floor(cast(datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and  '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' <= cast(floor(cast(dateto as float)) as datetime)"; 
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis_Bazzr=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis_Bazzr=0;
			rdr.Close();
			DiscBazz=dis_Bazzr.ToString();
			Tot_DiscBazz+=dis_Bazzr;
			Cache["TotBazzarDisc"]=Tot_DiscBazz;
			return dis_Bazzr;
			
		}
		public double Tot_DiscRO;
		public string DiscRO="";
		public double discount_RO(string prodname)
		{
			string[] str=prodname.Split(new char[] {':'},prodname.Length);
			string prodid="";
			double dis_Ro;
			System.Data.SqlClient.SqlDataReader rdr=null;
			string sql="select prod_id from products where prod_name='"+str[0]+"' and pack_type='"+str[1]+"'";
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql,ref rdr);
			if(rdr.Read())
			{
				prodid=rdr.GetValue(0).ToString();
			}
			rdr.Close();
			
			//string sql1="select discount from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//coment by vikas 2.11.2012 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and schname='Secondry(LTR Scheme)' and  cast(floor(cast(o.datefrom as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"'";
			//Coment By vikas 5.3.2013 string sql1="select discount from oilscheme o where prodid='"+prodid+"' and Group_Name like '%Ro%' and schname='Secondry(LTR Scheme)' and  (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text.Trim()) +"')";     //Add by vikas 2.11.2012
			string sql1="select discount from oilscheme o where prodid='"+prodid+"' and Group_Name like '%Ro%' and schname='Secondry(LTR Scheme)' and cast(floor(cast(datefrom as float)) as datetime)<= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString().Trim())+"' and  '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString().Trim()) +"' <= cast(floor(cast(dateto as float)) as datetime)"; 
			// Calls the sp_stockmovement for each product and create one stkmv temp. table.
			dbobj.SelectQuery(sql1,ref rdr);
			if(rdr.Read())
			{
				dis_Ro=System.Convert.ToDouble(rdr.GetValue(0).ToString());
			}
			else
				dis_Ro=0;
			rdr.Close();
			DiscRO=dis_Ro.ToString();
			Tot_DiscRO+=dis_Ro;
			Cache["Tot_DiscRO"]=Tot_DiscRO;
			return dis_Ro;
			
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
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\claimsheetReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:claimsheetReport.aspx,Method:print EXCEPTION  "+ane.Message+" userid "+ uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:claimsheetReport.aspx,Method:print EXCEPTION  "+se.Message+" userid "+ uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:claimsheetReport.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
				}
				//CreateLogFiles.ErrorLog("Form:StockMovement.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);
			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:claimsheetReport.aspx,Method:print EXCEPTION  "+es.Message+" userid "+ uid);

			}

		}

		protected double MultiplySJ(string str, string Prod_ID)
		{
			string[] mystr=str.Split(new char[]{'X'},str.Length);
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=6;
				}
				if(count==j)
				{
					double tot=0;
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' group by prod_id,total_qty");
					if(rdr.Read())
					{
						if(rdr.GetValue(0).ToString()!="")
							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					}
					rdr.Close();
					ans-=tot;
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=6;
				}
				if(count==k)
				{
					//					double tot=0;
					//					InventoryClass obj = new InventoryClass();
					//					SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where inprod_id=prod_id and inProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					//					if(rdr.Read())
					//					{
					//						if(rdr.GetValue(0).ToString()!="")
					//							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					//					}
					//					rdr.Close();
					//					ans-=tot;
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=6;
				}
				if(count==l)
				{
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=6;
				}
				if(count==m)
				{
					fsale+=ans;
					Cache["fsale"]=System.Convert.ToString(fsale);
					m+=6;
				}
				if(count==n)
				{
					fpur+=ans;
					Cache["fpur"]=System.Convert.ToString(fpur);
					n+=6;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=6;
					}
					if(count==j)
					{
						double tot=0;
						InventoryClass obj = new InventoryClass();
						SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' group by prod_id,total_qty");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						}
						rdr.Close();
						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=6;
					}
					if(count==k)
					{
						//						double tot=0;
						//						InventoryClass obj = new InventoryClass();
						//						SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where inprod_id=prod_id and inProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						//						if(rdr.Read())
						//						{
						//							if(rdr.GetValue(0).ToString()!="")
						//								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						//						}
						//						rdr.Close();
						//						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=6;
					}
					if(count==l)
					{
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=6;
					}
					if(count==m)
					{
						fsale+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fsale"]=System.Convert.ToString(fsale);
						m+=6;
					}
					if(count==n)
					{
						fpur+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fpur"]=System.Convert.ToString(fpur);
						n+=6;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
		}

		protected double MultiplySJ1(string str,string strfoc,string foc,string Prod_ID)
		{
			//string[] mystr=str.Split(new char[]{'X'},str.Length);
			//*************
			string[] mystr=null;
			if(double.Parse(foc)==0)
				mystr=str.Split(new char[]{'X'},str.Length);
			else
				mystr=strfoc.Split(new char[]{'X'},str.Length);
			//*************
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				if(count==i)
				{
					os+=ans;
					Cache["os"]=System.Convert.ToString(os);
					i+=6;
				}
				if(count==j)
				{
					//					double tot=0;
					//					InventoryClass obj = new InventoryClass();
					//					SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					//					if(rdr.Read())
					//					{
					//						if(rdr.GetValue(0).ToString()!="")
					//							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					//					}
					//					rdr.Close();
					//					ans-=tot;
					rect+=ans;
					Cache["rect"]=System.Convert.ToString(rect);
					j+=6;
				}
				if(count==k)
				{
					//					double tot=0;
					//					InventoryClass obj = new InventoryClass();
					//					SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
					//					if(rdr.Read())
					//					{
					//						if(rdr.GetValue(0).ToString()!="")
					//							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					//					}
					//					rdr.Close();
					//					ans-=tot;
					sales+=ans;
					Cache["sales"]=System.Convert.ToString(sales);
					k+=6;
				}
				if(count==l)
				{
					double tot=0;
					InventoryClass obj = new InventoryClass();
					SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where Outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' group by prod_id,total_qty");
					if(rdr.Read())
					{
						if(rdr.GetValue(0).ToString()!="")
							tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
					}
					rdr.Close();
					ans-=tot;
					cs+=ans;
					Cache["cs"]=System.Convert.ToString(cs);
					l+=6;
				}
				if(count==m)
				{
					fsale+=ans;
					Cache["fsale"]=System.Convert.ToString(fsale);
					m+=6;
				}
				if(count==n)
				{
					fpur+=ans;
					Cache["fpur"]=System.Convert.ToString(fpur);
					n+=6;
				}
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==i)
					{
						os+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["os"]=System.Convert.ToString(os);
						i+=6;
					}
					if(count==j)
					{
						//						double tot=0;
						//						InventoryClass obj = new InventoryClass();
						//						SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						//						if(rdr.Read())
						//						{
						//							if(rdr.GetValue(0).ToString()!="")
						//								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						//						}
						//						rdr.Close();
						//						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						rect+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["rect"]=System.Convert.ToString(rect);
						j+=6;
					}
					if(count==k)
					{
						//						double tot=0;
						//						InventoryClass obj = new InventoryClass();
						//						SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where Inprod_id=prod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by prod_id,total_qty");
						//						if(rdr.Read())
						//						{
						//							if(rdr.GetValue(0).ToString()!="")
						//								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						//						}
						//						rdr.Close();
						//						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						sales+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["sales"]=System.Convert.ToString(sales);
						k+=6;
					}
					if(count==l)
					{
						double tot=0;
						InventoryClass obj = new InventoryClass();
						SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where Outprod_id=prod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' group by prod_id,total_qty");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="")
								tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
						}
						rdr.Close();
						mystr[0]=System.Convert.ToString(System.Convert.ToDouble(mystr[0].ToString())-tot);
						cs+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["cs"]=System.Convert.ToString(cs);
						l+=6;
					}
					if(count==m)
					{
						fsale+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fsale"]=System.Convert.ToString(fsale);
						m+=6;
					}
					if(count==n)
					{
						fpur+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["fpur"]=System.Convert.ToString(fpur);
						n+=6;
					}
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
			
		}
		public double salesro=0;
		protected double MultiplySJ_RO(string str,string strfoc,string foc,string Prod_ID)
		{
			//string[] mystr=str.Split(new char[]{'X'},str.Length);
			//*************
			string[] mystr=null;
			if(double.Parse(foc)==0)
				mystr=str.Split(new char[]{'X'},str.Length);
			else
				mystr=strfoc.Split(new char[]{'X'},str.Length);
			//*************
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				
				if(count==k)
				{
					salesro+=ans;
					Cache["salesro"]=System.Convert.ToString(salesro);
					k+=6;
				}
				
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==k)
					{
					
						salesro+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["salesro"]=System.Convert.ToString(salesro);
						k+=6;
					}
					
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
			
		}


		public double salesbazz=0;
		protected double MultiplySJ_Bazzar(string str,string strfoc,string foc,string Prod_ID)
		{
			//string[] mystr=str.Split(new char[]{'X'},str.Length);
			//*************
			string[] mystr=null;
			if(double.Parse(foc)==0)
				mystr=str.Split(new char[]{'X'},str.Length);
			else
				mystr=strfoc.Split(new char[]{'X'},str.Length);
			//*************
			// check the package type is loose or not.
			if(str.Trim().IndexOf("Loose") == -1)
			{
				double ans=1;
				foreach(string val in mystr)
				{
					if(val.Length>0 && !val.Trim().Equals(""))
						ans*=double.Parse(val,System.Globalization.NumberStyles.Float);
				}
				//**********************
				
				if(count==k)
				{
					salesbazz+=ans;
					Cache["salesbazz"]=System.Convert.ToString(salesbazz);
					k+=6;
				}
				
				count++;
				//**********************
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//**********************
					if(count==k)
					{
					
						salesbazz+=System.Convert.ToDouble(mystr[0].ToString());
						Cache["salesbazz"]=System.Convert.ToString(salesbazz);
						k+=6;
					}
					
					count++;
					//**********************
					return System.Convert.ToDouble( mystr[0].ToString()); 

				}
				else
					return 0;
			}
			
		}

		public double SJin=0,SJOut=0;
		/// <summary>
		/// This method return the total stock adjustment out qty in given period.
		/// </summary>
		public string getSJOut(string Prod_ID)
		{
			double tot=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(InQty),total_qty from stock_adjustment,products where prod_id=inprod_id and InProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' group by prod_id,total_qty");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
			}
			rdr.Close();
			SJOut+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// This method return the total stock adjustment in qty in given period.
		/// </summary>
		public string getSJIn(string Prod_ID)
		{
			double tot=0;
			InventoryClass obj = new InventoryClass();
			SqlDataReader rdr = obj.GetRecordSet("select sum(OutQty),total_qty from stock_adjustment,products where prod_id=outprod_id and OutProd_ID='"+Prod_ID+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(cast(entry_time as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' group by prod_id,total_qty");
			if(rdr.Read())
			{
				if(rdr.GetValue(0).ToString()!="")
					tot=double.Parse(rdr.GetValue(0).ToString())*double.Parse(rdr.GetValue(1).ToString());
			}
			rdr.Close();
			SJin+=tot;
			return tot.ToString();
		}

		/// <summary>
		/// Prepares the excel report file ClaimSheet.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{

				/*coment by vikas 2.11.2012 if(grdLeg.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File into Excel Format");
					CreateLogFiles.ErrorLog("Form:ClaimSheet.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Secon. Sales Claim Sheet Report Convert Into Excel Format ,  userid  "+uid);
				}
				else
				{
					MessageBox.Show("Please Click the View Button First");
					return;
				}*/

				if(dropType.SelectedIndex==0)
				{
					ConvertToExcel();
				}
				else
				{
					ConvertToExcel_New();
				}
				MessageBox.Show("Successfully Convert File into Excel Format");
				CreateLogFiles.ErrorLog("Form:ClaimSheet.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Secon. Sales Claim Sheet Report Convert Into Excel Format ,  userid  "+uid);
			}
			catch(Exception ex)
			{
				MessageBox.Show("First Close The Open Excel File");
				CreateLogFiles.ErrorLog("Form:ClaimSheet.aspx,Method: btnExcel_Click,Class:PetrolPumpClass "+" Secon. Sales Claim Sheet Report "+"  EXCEPTION   "+ex.Message+"  userid  "+uid);
			}
		}
	}
}