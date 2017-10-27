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
using System.Net; 
using System.Net.Sockets ;
using System.IO ;
using System.Text;
using DBOperations;    

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for CustomerDataMining.
	/// </summary>
	public partial class CustomerDataMining : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		

		string uid = "";
	
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values and 
		/// also check accessing priviledges for particular user.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			try
			{
				uid=(Session["User_Name"].ToString());
				if(! IsPostBack)
				{
					CustomerTestGrid.Visible=false;
					CustomerGrid.Visible=false;
					#region Check Privileges
					int i;
					string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
					string Module="5";
					string SubModule="59";
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
					CustomerGrid.Visible=false;					
					GetMultiValue();		
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:pageload  EXCEPTION  "+ex.Message+"  "+ uid );
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

		// Checks the Order by value and fires the query to display the customer information by given order and display the data grid.
//		private void DropOrderBy_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
			
//			if(DropOrderBy.SelectedIndex == 0)
//			{
//                MessageBox.Show("Please Select Display Order");
//				return;
//			}
//			string order_by = "Cust_Name";
//			if(DropOrderBy.SelectedItem.Text == "Customer Type")
//				order_by = "Cust_Type";
//
//			if(DropOrderBy.SelectedItem.Text == "Customer City")
//				order_by = "City";
//
//			try
//			{
//				SqlDataReader SqlDtr = null;
//		
//				dbobj.SelectQuery("Select * from Customer order by "+order_by+" asc",ref SqlDtr);
//				CustomerGrid.DataSource = SqlDtr;
//				CustomerGrid.DataBind();
//		
//				if(CustomerGrid.Items.Count==0)
//				{
//					MessageBox.Show("Data not available");
//					CustomerGrid.Visible=false;
//				}
//				else
//				{
//					CustomerGrid.Visible=true;
//				}
//				SqlDtr.Close ();
//			}
//			catch(Exception ex)
//			{
//				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:DropOrderBy_SelectedIndexChanged  EXCEPTION  "+ex.Message+"  User: "+ uid );
//
//			}
//		}
		
		/// <summary>
		/// This is a method to bind the datagrid.
		/// </summary>
		public void Bindthedata()
		{
			SqlConnection sqlcon=new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			string sql="";
			string Cust_Name="";
			if(DropSearchBy.SelectedIndex==0)
				sql="Select * from Customer";
			else
			{
				if(DropValue.Value=="All")
					sql="Select * from Customer";
				else if(DropSearchBy.SelectedIndex==1)
				{
					//coment by vikas 25.05.09 sql="Select * from Customer where cust_name='"+DropValue.Value+"'";
					/*******Add by vikas sharma 25.05.09************************/
					Cust_Name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
					sql="Select * from Customer where cust_name='"+Cust_Name.ToString()+"'";
					/*******Add by vikas sharma 25.05.09************************/
				}/******Add by vikas 17.11.2012*********************/
				else if(DropSearchBy.SelectedIndex==2)
					sql="Select * from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
				else if(DropSearchBy.SelectedIndex==3)
					sql="Select * from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')"; /****End******/
				else if(DropSearchBy.SelectedIndex==4)
					sql="Select * from Customer where state='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==5)
					sql="Select * from Customer where city='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==6)
					sql="Select * from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')";
			}
			sql+=" order by cust_name";
			SqlDataAdapter da=new SqlDataAdapter(sql,sqlcon);
			DataSet ds=new DataSet();	
			da.Fill(ds,"customer");
			DataTable dtcustomer=ds.Tables["customer"]; 
			//da.Fill(ds,"beat_master","machanic_entry","customermechanicentry ","customer");
			//DataTable dtcustomer=ds.Tables["beat_master","machanic_entry","customermechanicentry ","customer"]; 
			DataView dv=new DataView(dtcustomer);
			dv.Sort=strorderby;
			Cache["strorderby"]=strorderby;
			if(chkTesting.Checked)
				CustomerTestGrid.DataSource=dv;
			else
				CustomerGrid.DataSource=dv;
			if(dv.Count!=0)
			{
				if(chkTesting.Checked)
				{
					CustomerTestGrid.DataBind();
					CustomerTestGrid.Visible=true;
					CustomerGrid.Visible=false;
				}
				else
				{
					CustomerGrid.DataBind();
					CustomerTestGrid.Visible=false;
					CustomerGrid.Visible=true;
				}
			}
			else
			{
				CustomerGrid.Visible=false;
				CustomerTestGrid.Visible=false;
				MessageBox.Show("Data Not Available");
			}
			sqlcon.Dispose();
		}
	
		/// <summary>
		/// This is a method to Make sorting on the clicking of headertext of datagrid.
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
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:SortCommand_Click"+" Mechanic Report "+" EXCEPTION  "+ex.Message+" userid "+ uid);			
			}
		}

		/// <summary>
		/// This method is used to store the column name in asending order and set also a column name and asending 
		/// type on session variable and call the Bindthedata() function to view the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void txtview_Click(object sender, System.EventArgs e)
		{
			try
			{
				strorderby="cust_name ASC";
				Session["Column"]="cust_name";
				Session["order"]="ASC";
				Bindthedata();
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:btnView, userid  "+uid );
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:btnView,   EXCEPTION "+ex.Message+"  userid  "+uid );
			}
		}

		/// <summary>
		/// Its fires the query according to selected order and writes the result into file CustomerDataMining.txt.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
		{		
			try
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\CustomerDataMining.txt";
				StreamWriter sw = new StreamWriter(path);
				string info = "";
				SqlDataReader SqlDtr = null;
				sw.Write((char)15);
				sw.WriteLine(""); 
				//*********************
				string des="";
				if(chkTesting.Checked)
					des="------------------------------------------------------------------------------------";
				else
					//des="-----------------------------------------------------------------------------------------------------------------------------------------";
					  des="----------------------------------------------------------------------------------------------------------------------------";
					

				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//***********************
				sw.WriteLine(GenUtil.GetCenterAddr("======================",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("Customer Data Mining",des.Length)); 
				sw.WriteLine(GenUtil.GetCenterAddr("======================",des.Length)); 
				if(chkTesting.Checked)
				{
					sw.WriteLine("+-----------------------------------+-------------------------+--------------------+");
					sw.WriteLine("|           Customer Name           |          City           |   Customer Type    |");
					sw.WriteLine("+-----------------------------------+-------------------------+--------------------+");
					//             12345678901234567890123456789012345 1234567890123456789012345 12345678901234567890 
					info = "|{0,-35:S}|{1,-25:S}|{2,-20:S}|";
					
				}
				else
				{
					//coment by vikas 26.05.09 
					/*sw.WriteLine("+-----------------+---------+----------------+-----------+---------+-------+------------------------------+-----------------------------+"); 
					sw.WriteLine("|                 |         |                |           |         |       |        Contact Number        |                             |"); 
					sw.WriteLine("|  Customer Name  |  Type   |    Address     |   City    |  State  |Country|--------+---------+-----------|          EMail              |"); 
					sw.WriteLine("|                 |         |                |           |         |       | Office |Residence|  Mobile   |                             |"); 
					sw.WriteLine("+-----------------+---------+----------------+-----------+---------+-------+--------+---------+-----------+-----------------------------+"); 
					//             12345678901234567 123456789 1234567890123456 12345678901 123456789 1234567 12345678 123456789 12345678901 12345678901234567890123456789            
					info = "|{0,-17:S}|{1,-9:S}|{2,-16:S}|{3,-11:S}|{4,-9:S}|{5,-7:S}|{6,8:S}|{7,9:S}|{8,11:S}|{9,-29:S}|";*/

					/********Start add by vikas 26.05.09****************************/
					sw.WriteLine("+-----------------+---------+----------------+-----------+-----------------------------------+-----------------------------+"); 
					sw.WriteLine("|                 |         |                |           |          Contact Number           |                             |"); 
					sw.WriteLine("|  Customer Name  |  Type   |    Address     |   City    |-----------+-----------+-----------|       Contact Person        |"); 
					sw.WriteLine("|                 |         |                |           |  Office   | Residence |  Mobile   |                             |"); 
					sw.WriteLine("+-----------------+---------+----------------+-----------+-----------+-----------+-----------+-----------------------------+"); 
					//             12345678901234567 123456789 1234567890123456 12345678901 123456789 1234567 12345678 123456789 12345678901 12345678901234567890123456789            
					info = "|{0,-17:S}|{1,-9:S}|{2,-16:S}|{3,-11:S}|{4,11:S}|{5,11:S}|{6,11:S}|{7,-29:S}|";
					/********end****************************/

				}
            
				string sql="";
				string Cust_Name="";
				if(DropSearchBy.SelectedIndex==0)
					sql="Select * from Customer";
				else
				{
					if(DropValue.Value=="All")
						sql="Select * from Customer";
					else if(DropSearchBy.SelectedIndex==1)
					{
						//coment by vikas 25.05.09 sql="Select * from Customer where cust_name='"+DropValue.Value+"'";
						/*******Add by vikas sharma 25.05.09************************/
						Cust_Name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
						sql="Select * from Customer where cust_name='"+Cust_Name.ToString()+"'";
						/*******Add by vikas sharma 25.05.09************************/
					}
					/*Coment by vikas 17.11.2012
					 * else if(DropSearchBy.SelectedIndex==2)
						sql="Select * from Customer where cust_type  like '"+DropValue.Value+"%'";*/
						/******Add by vikas 17.11.2012*********************/
					else if(DropSearchBy.SelectedIndex==2)
						sql="Select * from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
					else if(DropSearchBy.SelectedIndex==3)
						sql="Select * from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')"; /****End******/
					else if(DropSearchBy.SelectedIndex==4)
						sql="Select * from Customer where state='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==5)
						sql="Select * from Customer where city='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==6)
						sql="Select * from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')";
				}
				sql+=" order by "+Cache["strorderby"];
				//dbobj.SelectQuery("Select * from Customer order by "+Cache["strorderby"],ref SqlDtr);
				dbobj.SelectQuery(sql,ref SqlDtr);
				if(SqlDtr.HasRows)
				{
					if(chkTesting.Checked)
					{
						while(SqlDtr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(SqlDtr["Cust_Name"].ToString().Trim(),35),
								GenUtil.TrimLength(SqlDtr["City"].ToString().Trim(),25),
								GenUtil.TrimLength(SqlDtr["Cust_Type"].ToString().Trim(),20)
							);  
						}
					}
					else
					{
						while(SqlDtr.Read())
						{
							sw.WriteLine(info,GenUtil.TrimLength(SqlDtr["Cust_Name"].ToString().Trim(),17),
								GenUtil.TrimLength(SqlDtr["Cust_Type"].ToString().Trim(),9),
								GenUtil.TrimLength(trimString(SqlDtr["Address"].ToString().Trim()),16),
								GenUtil.TrimLength(SqlDtr["City"].ToString().Trim(),11),
								//coment by vikas 26.05.09 GenUtil.TrimLength(SqlDtr["State"].ToString().Trim(),9),
								//coment by vikas 26.05.09 GenUtil.TrimLength(SqlDtr["Country"].ToString().Trim(),7),
								GenUtil.TrimLength(SqlDtr["Tel_Off"].ToString().Trim(),11),
								GenUtil.TrimLength(SqlDtr["Tel_Res"].ToString().Trim(),11),
								GenUtil.TrimLength(SqlDtr["Mobile"].ToString().Trim(),11),
								GenUtil.TrimLength(SqlDtr["ContactPerson"].ToString().Trim(),29));  
						}
					}
				}
				else
				{
					MessageBox.Show("Data not available");
					sw.Close();
					return;
				}
				if(chkTesting.Checked)
					sw.WriteLine("+-----------------------------------+-------------------------+--------------------+");
				else
					sw.WriteLine("+-----------------+---------+----------------+-----------+-----------+-----------+-----------+-----------------------------+"); 
				sw.Close();
				Print();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:btnPrint_Click  EXCEPTION  "+ex.Message+"  User: "+ uid );
			}
		}

		/// <summary>
		/// Method to write into the excel report file to print.
		/// </summary>
		public void ConvertToExcel()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2);
			string strExcelPath  = home_drive+"\\Servosms_ExcelFile\\Export\\";
			Directory.CreateDirectory(strExcelPath);
			string path = home_drive+@"\Servosms_ExcelFile\Export\CustomerDataMining.xls";
			StreamWriter sw = new StreamWriter(path);
			if(chkTesting.Checked)
				sw.WriteLine("Customer Name\tCity\tType");
			else
                //coment by vikas 26.05.09 sw.WriteLine("Tin No\tUnique Code\tCustomer Name\tType\tAddress\tCity\tState\tCountry\tOffice\tResidence\tMobile\tEMail"); 
				sw.WriteLine("Tin No\tUnique Code\tCustomer Name\tType\tAddress\tCity\tOffice\tResidence\tMobile\tContact Person"); 
			string sql="";
			string Cust_Name="";
			if(DropSearchBy.SelectedIndex==0)
				sql="Select * from Customer";
			else
			{
				if(DropValue.Value=="All")
					sql="Select * from Customer";
				else if(DropSearchBy.SelectedIndex==1)
				{
					//coment by vikas 25.05.09 sql="Select * from Customer where cust_name='"+DropValue.Value+"'";
					/*******Add by vikas sharma 25.05.09************************/
					Cust_Name=DropValue.Value.Substring(0,DropValue.Value.IndexOf(":"));
					sql="Select * from Customer where cust_name='"+Cust_Name.ToString()+"'";
					/*******Add by vikas sharma 25.05.09************************/
				}
					/*Coment by vikas 17.11.2012
						 * else if(DropSearchBy.SelectedIndex==2)
							sql="Select * from Customer where cust_type  like '"+DropValue.Value+"%'";*/
					/******Add by vikas 17.11.2012*********************/
				else if(DropSearchBy.SelectedIndex==2)
					sql="Select * from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and group_Name ='"+DropValue.Value+"')";
				else if(DropSearchBy.SelectedIndex==3)
					sql="Select * from Customer where cust_id in(select cust_id from customer c,customertype ct where c.cust_type=ct.customertypename and sub_group_Name ='"+DropValue.Value+"')"; /****End******/
				else if(DropSearchBy.SelectedIndex==3)
					sql="Select * from Customer where state='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==4)
					sql="Select * from Customer where city='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==5)
					sql="Select * from Customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"')";
			}
			sql+=" order by "+Cache["strorderby"];
			//dbobj.SelectQuery("Select * from Customer order by "+Cache["strorderby"],ref SqlDtr);
			dbobj.SelectQuery(sql,ref SqlDtr);
			if(SqlDtr.HasRows)
			{
				if(chkTesting.Checked)
				{
					while(SqlDtr.Read())
					{
						sw.WriteLine(SqlDtr["Cust_Name"].ToString().Trim()+"\t"+
							SqlDtr["City"].ToString().Trim()+"\t"+
							SqlDtr["Cust_Type"].ToString().Trim());
					}
				}
				else
				{
					while(SqlDtr.Read())
					{
						sw.WriteLine(SqlDtr["Tin_no"].ToString().Trim()+"\t"+
							SqlDtr["sadbhavnacd"].ToString().Trim()+"\t"+
							SqlDtr["Cust_Name"].ToString().Trim()+"\t"+
							SqlDtr["Cust_Type"].ToString().Trim()+"\t"+
							SqlDtr["Address"].ToString().Trim()+"\t"+
							//Coment by vikas 26.05.09 SqlDtr["City"].ToString().Trim()+"\t"+
							//Coment by vikas 26.05.09 SqlDtr["State"].ToString().Trim()+"\t"+
							SqlDtr["City"].ToString().Trim()+"\t"+
							SqlDtr["Tel_Off"].ToString().Trim()+"\t"+
							SqlDtr["Tel_Res"].ToString().Trim()+"\t"+
							SqlDtr["Mobile"].ToString().Trim()+"\t"+
							SqlDtr["ContactPerson"].ToString().Trim());  
					}
				}
			}
			SqlDtr.Close();
			sw.Close();
		}

		/// <summary>
		/// Its trim the customer address of customer if it is greater than 30 charcters to display in report.
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public string trimString(string address)
		{
			if(address.Length > 30)
			{
				address = address.Substring(0,30);
			}
			return address;
		}
	
		/// <summary>
		/// Its sends the CustomerDataMining.txt to Print Server.
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
					CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:Print"+uid);
					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\CustomerDataMining.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:print. Report Printed   userid  "+uid);
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:print"+ " EXCEPTION "  +ane.Message+"  userid  "+uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:print"+ " EXCEPTION "  +se.Message+"  userid  "+uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:print"+ " EXCEPTION "  +es.Message+"  userid  "+uid);
				}
			} 
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Method:print  EXCEPTION "  +ex.Message+"  userid  "+uid);
			}
		}

		/// <summary>
		/// Prepares the excel report file CustomerDataMining.xls for printing.
		/// call the ConvertToExcel() function to generate the excel report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(CustomerGrid.Visible==true || CustomerTestGrid.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   CustomerDataMining Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:CustomerDataMining.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    CustomerDataMining Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to fill the searchable combo box when according to select value
		/// from dropdownlist with the help of java script.
		/// </summary>
		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				string strName="",strType="",strDistrict="",strPlace="",strSSR="";
				string strGroup="",strSubGroup="";       //Add by vikas 16.11.2012
				
				//comment by vikas 25.05.09 strName = "select distinct cust_name from customer order by cust_name";
				
				strName = "select distinct cust_name,city from customer order by cust_name,city";
				
				//coment by vikas 17.11.2012 strType = "select distinct cust_type from customer union select distinct case when cust_type like 'oe%' then 'OE' when cust_type like 'ro%' then 'RO' when cust_type like 'ksk%' then 'KSK' when cust_type like 'N-ksk%' then 'N-KSK' when cust_type like 'Nksk%' then 'NKSK' else 'RO' end as cust_type from customer";

				strDistrict = "select distinct state from customer order by state";
				strPlace = "select distinct city from customer order by city";
				strSSR = "select emp_name from employee where emp_id in(select ssr from customer) and status=1 order by emp_name";

				strGroup="select distinct Group_Name from customertype";             //Add by vikas 16.11.2012 
				strSubGroup="select distinct Sub_Group_Name from customertype";		//Add by vikas 16.11.2012

				//coment by vikas 17.11.2012 string[] arrStr = {strName,strType,strDistrict,strPlace,strSSR};
				//coment by vikas 17.11.2012 HtmlInputHidden[] arrCust = {tempCustName,tempCustType,tempDistrict,tempPlace,tempSSR};	

				string[] arrStr = {strName,strDistrict,strPlace,strSSR,strGroup,strSubGroup};
				HtmlInputHidden[] arrCust = {tempCustName,tempDistrict,tempPlace,tempSSR,tempGroup,tempSubGroup};	

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
							//coment by vikas 25.05.09 arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
							
							/*******Add by vikas 25.05.09*********************/
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
							/*******End*********************/
						}
					}
					rdr.Close();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Customer_Bill_Ageing.aspx,Class:PetrolPumpClass.cs,Method:getMultiValue()    Customer Bill Ageing Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}		
		
	}
}