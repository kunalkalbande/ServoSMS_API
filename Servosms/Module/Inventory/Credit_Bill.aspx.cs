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
using System.Drawing.Imaging;
using System.IO.IsolatedStorage;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for Credit_Bill.
	/// </summary>
	public partial class Credit_Bill : System.Web.UI.Page
	{

		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		// Put user code to initialize the page here
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				FillOrgInfo();
				show();
				try
				{
					uid=(Session["User_Name"].ToString());
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx,Method=pageload"+ ex.Message+" EXCEPTION     "+uid);	
					Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
					return;
				}
				if(!IsPostBack)
				{
					checkPrevileges();
					lblDate.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
					txtDateFrom.Text=lblDate.Text.ToString();
					txtDateTO.Text=lblDate.Text.ToString();
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					string sql;

					#region Fetch All Customer Name and fill in the ComboBox
					sql="select Cust_Name,City from Customer order by Cust_Name";
					SqlDtr=obj.GetRecordSet(sql);
			
					while(SqlDtr.Read())
					{
				
						DropCustID.Items.Add (SqlDtr.GetValue(0).ToString ()+ ":" +SqlDtr.GetValue(1).ToString ());				
					}
					SqlDtr.Close ();		
					#endregion

					GetNextBillNo();
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx method  page_load"+ ex.Message+" EXCEPTION "+uid);
			}
		}

		// This method checks the user privileges from session.
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="4";
			string SubModule="5";
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
			if(View_flag=="0" )
			{
				//	string msg="UnAthourized Visit to Credit Bill Page";
				//	dbobj.LogActivity(msg,Session["User_Name"].ToString());  
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
			}
			if(Add_Flag=="0")
				printBtn.Enabled = false;  
				
			#endregion
		}

		// This method displays the Dealers Logo.
		public void show()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			string filePath = "";
			sql="select Logo from Organisation where CompanyID='1001'" ;
			SqlDtr=obj.GetRecordSet(sql); 
			if(SqlDtr.Read())
			{
				filePath = SqlDtr["Logo"].ToString() ;
	
			}
			else
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				filePath =home_drive+@"\Inetpub\wwwroot\Servosms\CompanyLogo\Logo.jpg";
			}
			if(filePath.Trim().Equals(""))
			{
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				filePath =home_drive+@"\Inetpub\wwwroot\Servosms\CompanyLogo\Logo.jpg";
			}
			imgSample.ImageUrl = filePath;
			SqlDtr.Close(); 
		}

		// This Method displays the organisation name and address.
		public void FillOrgInfo()
		{
			InventoryClass  obj=new InventoryClass ();
			SqlDataReader SqlDtr;
			string sql;
			sql="select * from Organisation where CompanyID='1001'" ;
			SqlDtr=obj.GetRecordSet(sql); 
			while(SqlDtr.Read())
			{
				txtname1.Text =SqlDtr.GetValue(1).ToString();  
				txtdet1.Text=SqlDtr.GetValue(2).ToString(); 
				txtadd1.Text=SqlDtr.GetValue(3).ToString(); 
				txtci11.Text=SqlDtr.GetValue(4).ToString();
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

		// This is used to change the format.
		public DateTime ToMMddYYYY(string str)
		{
			int dd,mm,yy;
			string [] strarr = new string[3];			
			strarr=str.Split(new char[]{'/'},str.Length);
			dd=Int32.Parse(strarr[0]);
			mm=Int32.Parse(strarr[1]);
			yy=Int32.Parse(strarr[2]);
			DateTime dt=new DateTime(yy,mm,dd);			
			return(dt);
		}
		
		protected void DropCustID_SelectedIndexChanged(object sender, System.EventArgs e)
		{   
			try
			{
				PetrolPumpClass obj1=new PetrolPumpClass();
				TextBox1.Text=DropCustID.SelectedValue.ToString(); 
				if(DropCustID.SelectedIndex ==0)
				{
					MessageBox.Show("Please Select Customer Name");
					return;
				}
				InventoryClass  obj=new InventoryClass ();
				SqlDataReader SqlDtr= null;
				string temp = DropCustID.SelectedItem.Text.Trim() ;   
				string[] arr= temp.Split(new char[] {':'} ,temp.Length);
				SqlDtr = obj.GetRecordSet("Select cv.* from Customer_Vehicles cv, Customer c where cv.Cust_id = c.Cust_id and c.Cust_Name='"+arr[0].Trim()+"' and c.City = '"+arr[1].Trim()+"'");
				DropVehicleNo.Items.Clear();
				DropVehicleNo.Items.Add("All");  
				if(SqlDtr.HasRows)
				{
					while(SqlDtr.Read())
					{
						if(!SqlDtr.GetValue(2).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(2).ToString().Trim()); 
						if(!SqlDtr.GetValue(3).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(3).ToString().Trim()); 
						if(!SqlDtr.GetValue(4).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(4).ToString().Trim()); 
						if(!SqlDtr.GetValue(5).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(5).ToString().Trim()); 
						if(!SqlDtr.GetValue(6).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(6).ToString().Trim()); 
						if(!SqlDtr.GetValue(7).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(7).ToString().Trim()); 
						if(!SqlDtr.GetValue(8).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(8).ToString().Trim()); 
						if(!SqlDtr.GetValue(9).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(9).ToString().Trim()); 
						if(!SqlDtr.GetValue(10).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(10).ToString().Trim()); 
						if(!SqlDtr.GetValue(11).ToString().Trim().Equals(""))
							DropVehicleNo.Items.Add( SqlDtr.GetValue(11).ToString().Trim()); 
					}
				}
				SqlDtr.Close(); 
				displayReport();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx ,method :DropCustID_SelectedIndexChanged "+"  Coustmer :"+   DropCustID.SelectedValue.ToString()+"     is Selected "+ ex.Message+" EXCEPTION "+"  Userid  "+uid);
			}
		}

		//This is used to bind the grid.
		public void displayReport()
		{
			try
			{
				PetrolPumpClass obj1=new PetrolPumpClass();
				TextBox1.Text=DropCustID.SelectedValue.ToString(); 
				InventoryClass  obj=new InventoryClass ();
				SqlDataReader SqlDtr;
				string sql="";

				#region Bind DataGrid
				if(DropVehicleNo.SelectedIndex == 0) 
					sql="select sm.invoice_no, slip_no, invoice_date, vehicle_no, prod_Name+ ' ' +Pack_Type Prod_Name, qty, rate, amount from sales_master sm, sales_details sd, products p where sm.invoice_date between '"+ ToMMddYYYY(txtDateFrom.Text) +"' and dateadd(day,1,'"+ ToMMddYYYY(txtDateTO.Text) + "') and sm.cust_id in ( select cust_id from customer where cust_name=substring('"+ DropCustID.SelectedItem.Value +"',1,charindex(':','"+ DropCustID.SelectedItem.Value +"')-1)  and city=substring('"+ DropCustID.SelectedItem.Value +"',charindex(':','"+ DropCustID.SelectedItem.Value +"')+1,len('"+ DropCustID.SelectedItem.Value +"'))) and sm.sales_type = 'credit' and sm.invoice_no = sd.invoice_no and sd.prod_id = p.prod_id";
				else
					sql="select sm.invoice_no, slip_no, invoice_date, vehicle_no, prod_Name+ ' ' +Pack_Type Prod_Name, qty, rate, amount from sales_master sm, sales_details sd, products p where sm.invoice_date between '"+ ToMMddYYYY(txtDateFrom.Text) +"' and dateadd(day,1,'"+ ToMMddYYYY(txtDateTO.Text) + "') and sm.cust_id in ( select cust_id from customer where cust_name=substring('"+ DropCustID.SelectedItem.Value +"',1,charindex(':','"+ DropCustID.SelectedItem.Value +"')-1)  and city=substring('"+ DropCustID.SelectedItem.Value +"',charindex(':','"+ DropCustID.SelectedItem.Value +"')+1,len('"+ DropCustID.SelectedItem.Value +"'))) and sm.sales_type = 'credit' and sm.invoice_no = sd.invoice_no and sd.prod_id = p.prod_id and Vehicle_No = '"+DropVehicleNo.SelectedItem.Text+"'" ; 

				SqlDtr =obj.GetRecordSet(sql);
				GridCreditBill.DataSource=SqlDtr;
				GridCreditBill.DataBind();
				if(GridCreditBill.Items.Count==0)
				{
					MessageBox.Show("Data not available");
					GridCreditBill.Visible=false;
				}
				else
				{
					GridCreditBill.Visible=true;
				}

				SqlDtr.Close();
				checkPrevileges();
	
				#endregion
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx,Class:PetrolPumpClass.cs,Method:displayReport()   Credit Bill Viewed for Bill NO  "+lblBillNo.Text.ToString()+"   Userid "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx ,method :displayReport() "+"  Coustmer :"+   DropCustID.SelectedValue.ToString()+"     is Selected "+ ex.Message+" EXCEPTION "+"  Userid  "+uid);
			}
		}

		//This is used to make report for printing with making a file.txt.
		public void reportmaking()  
		{
			/*
											======================                                   
												CREDIT BILL REPORT                                      
											======================                                   
			M/s        :
			Bill No    :
			Bill Date  :
		+----------+-----+-------------------------+----+-------+---------+-------------+
		|Inv.Date  |Slip |       Product Name      |Qty | Rate  | Amount  | Vehicle.No  |
		+----------+-----+-------------------------+----+-------+---------+-------------+
		 18/7/2006  12345 1234567890123456789012345 1235 1234567 123456.00 1234567890123 	DD/MM/YYYY  XXX    1234567890123456789012345 123456.78 123456.00  1234567.00  xxxxxxxxxx          	
			*/
			System.Data.SqlClient.SqlDataReader rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\CreditBillReport.txt";
			StreamWriter sw = new StreamWriter(path);

			string sql="";
			string info = "";
			string strDate="";

			string abc = DropCustID.SelectedItem.Text;   
			string billno=lblBillNo.Text;
			string billdate =lblDate.Text; 
			
			//sql="select sm.invoice_no, slip_no, invoice_date, vehicle_no, prod_Name+ ' ' +Pack_Type Prod_Name, qty, rate, amount from sales_master sm, sales_details sd, products p where sm.invoice_date between '"+ ToMMddYYYY(txtDateFrom.Text) +"' and dateadd(day,1,'"+ ToMMddYYYY(txtDateTO.Text) + "') and sm.cust_id in ( select cust_id from customer where cust_name=substring('"+ DropCustID.SelectedItem.Value +"',1,charindex(':','"+ DropCustID.SelectedItem.Value +"')-1)  and city=substring('"+ DropCustID.SelectedItem.Value +"',charindex(':','"+ DropCustID.SelectedItem.Value +"')+1,len('"+ DropCustID.SelectedItem.Value +"'))) and sm.invoice_no = sd.invoice_no and sd.prod_id = p.prod_id";
			if(DropVehicleNo.SelectedIndex == 0) 
				sql="select sm.invoice_no, slip_no, invoice_date, vehicle_no, prod_Name+ ' ' +Pack_Type Prod_Name, qty, rate, amount from sales_master sm, sales_details sd, products p where sm.invoice_date between '"+ ToMMddYYYY(txtDateFrom.Text) +"' and dateadd(day,1,'"+ ToMMddYYYY(txtDateTO.Text) + "') and sm.cust_id in ( select cust_id from customer where cust_name=substring('"+ DropCustID.SelectedItem.Value +"',1,charindex(':','"+ DropCustID.SelectedItem.Value +"')-1)  and city=substring('"+ DropCustID.SelectedItem.Value +"',charindex(':','"+ DropCustID.SelectedItem.Value +"')+1,len('"+ DropCustID.SelectedItem.Value +"'))) and sm.sales_type = 'credit' and sm.invoice_no = sd.invoice_no and sd.prod_id = p.prod_id";
			else
				sql="select sm.invoice_no, slip_no, invoice_date, vehicle_no, prod_Name+ ' ' +Pack_Type Prod_Name, qty, rate, amount from sales_master sm, sales_details sd, products p where sm.invoice_date between '"+ ToMMddYYYY(txtDateFrom.Text) +"' and dateadd(day,1,'"+ ToMMddYYYY(txtDateTO.Text) + "') and sm.cust_id in ( select cust_id from customer where cust_name=substring('"+ DropCustID.SelectedItem.Value +"',1,charindex(':','"+ DropCustID.SelectedItem.Value +"')-1)  and city=substring('"+ DropCustID.SelectedItem.Value +"',charindex(':','"+ DropCustID.SelectedItem.Value +"')+1,len('"+ DropCustID.SelectedItem.Value +"'))) and sm.sales_type = 'credit' and sm.invoice_no = sd.invoice_no and sd.prod_id = p.prod_id and Vehicle_No = '"+DropVehicleNo.SelectedItem.Text+"'" ; 

				
			dbobj.SelectQuery(sql,ref rdr);
			// Condensed
			sw.Write((char)27);
			sw.Write((char)15);
	
			sw.WriteLine("                                  ======================");
			sw.WriteLine("                                    CREDIT BILL REPORT ");
			sw.WriteLine("                                  ======================  ");
			sw.WriteLine("M/s        : " + abc);
			sw.WriteLine("Bill No    : " + billno);
			sw.WriteLine("Bill Date  : " + billdate);
			sw.WriteLine("+----------+--------+------------------------------+-----+--------+----------+--------------+");
			sw.WriteLine("|Inv.Date  |Slip No |       Product Name           | Qty |  Rate  | Amount   | Vehicle.No   |");
			sw.WriteLine("+----------+--------+------------------------------+-----+--------+----------+--------------+");
			//             18/7/2006  12345678 123456789012345678901234567890 12345 12345678 1234567.00 1234567890123 
			if(rdr.HasRows)
			{
				info = " {0,-10:S} {1,8:S} {2,-30:S} {3,5:F} {4,8:F} {5,10:F} {6,-14:S}";
				while(rdr.Read())
				{
			
					// Trim Date
					strDate = rdr["invoice_date"].ToString().Trim();
					int pos = strDate.IndexOf(" ");
		
					if(pos != -1)
					{
						strDate = strDate.Substring(0,pos);
					}
					else
					{
						strDate = "";					
					}

					sw.WriteLine(info,GenUtil.str2DDMMYYYY(strDate),
						rdr["Slip_no"].ToString().Trim(),
						strTrim(rdr["prod_Name"].ToString()),
						rdr["qty"].ToString().Trim(),
						rdr["rate"].ToString().Trim(),
						GenUtil.strNumericFormat(rdr["amount"].ToString().Trim()),
						rdr["Vehicle_no"].ToString().Trim());

				}
			}

			sw.WriteLine("+----------+--------+------------------------------+-----+--------+----------+--------------+");
			// deselect Condensed
			sw.Write((char)18);
			sw.Write((char)12);
			dbobj.Dispose();
			sw.Close();

			rdr.Close();
		}

		//This is used to trim the length morethan 50.
		public string strTrim(string str)
		{
			if(str.Length > 30)
			{
				str =str.Substring(0,30); 
			}
			return str;
		}

		//This is used to print the report & contact with printserver
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

					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\CreditBillReport.txt<EOF>");

					// Send the data through the socket.
					int bytesSent = sender1.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender1.Receive(bytes);
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(bytes,0,bytesRec));

					// Release the socket.
					sender1.Shutdown(SocketShutdown.Both);
					sender1.Close();
					CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx,Class:PetrolPumpClass.cs,Method:Print"+ uid);
		
				} 
				catch (ArgumentNullException ane) 
				{
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
				}

			} 
			catch (Exception es) 
			{
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx,Class:PetrolPumpClass.cs,Method:Print"+ es.Message+"  EXCEPTION  "+uid);
			}
		}

		public void Clear()
		{
		}

		// Method to fetch the next Bill no from print_credit_bill table.
		public void GetNextBillNo()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string sql;

			#region Fetch the Next Bill No

			sql="select Max(Bill_No)+1 from Print_Credit_Bill";
			SqlDtr=obj.GetRecordSet(sql);
			while(SqlDtr.Read())
			{
				lblBillNo.Text=SqlDtr.GetValue(0).ToString();
				if(lblBillNo.Text=="")
				{
					lblBillNo.Text="1001";
				}
			}
			SqlDtr.Close ();		
			#endregion
		}

		protected void txtDateFrom_TextChanged(object sender, System.EventArgs e)
		{

		}

		protected void printBtn_Click(object sender, System.EventArgs e)
		{
			// The follwing code saves the credit bill as well as print it.
			if(DropCustID.SelectedIndex == 0)
			{
				MessageBox.Show("Please Select the Customer Name"); 
				return;
			}
			string	sql="select sm.invoice_no, slip_no, invoice_date, vehicle_no, prod_Name+ ' ' +Pack_Type Prod_Name,qty, rate, amount from sales_master sm, sales_details sd, products p where sm.invoice_date between '"+ ToMMddYYYY(txtDateFrom.Text) +"' and dateadd(day,1,'"+ ToMMddYYYY(txtDateTO.Text) + "') and sm.cust_id in ( select cust_id from customer where cust_name=substring('"+ DropCustID.SelectedItem.Value +"',1,charindex(':','"+ DropCustID.SelectedItem.Value +"')-1)  and city=substring('"+ DropCustID.SelectedItem.Value +"',charindex(':','"+ DropCustID.SelectedItem.Value +"')+1,len('"+ DropCustID.SelectedItem.Value +"'))) and sm.invoice_no = sd.invoice_no and sd.prod_id = p.prod_id";
	
			PetrolPumpClass obj=new PetrolPumpClass();
			PetrolPumpClass obj1=new PetrolPumpClass();
			SqlDataReader	SqlDtr2 =obj.GetRecordSet(sql);
			string sql1="";
			try
			{
				while(SqlDtr2.Read())
				{
					DateTime dt=System.Convert.ToDateTime(SqlDtr2.GetValue(2).ToString());
					string str1=dt.ToShortDateString();
					string str2=SqlDtr2.GetValue(1).ToString();
					string str3=SqlDtr2.GetValue(4).ToString();
					string str4=SqlDtr2.GetValue(5).ToString();
					string str5=SqlDtr2.GetValue(6).ToString();
					string str6=SqlDtr2.GetValue(7).ToString();
					string str7=SqlDtr2.GetValue(3).ToString();
					sql1="insert into Print_Credit_Bill(Bill_No,Bill_date,Slip_no,Particulars,Qty,Rate,Amount,Vehicle_No)values('"+lblBillNo.Text.ToString()+"',"+str1+","+str2+",'"+str3+"',"+str4+","+str5+","+str6+",'"+str7+"')";		
					obj1.InsertRecord(sql1);
				}		 
				SqlDtr2.Close();
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx,Class:PetrolPumpClass.cs,Method:Print Bill No."+lblBillNo.Text.ToString()+" Saved. User Id = "+uid );
				MessageBox.Show("Credit Bill Saved");
				reportmaking();
				Print();
				GetNextBillNo();
				checkPrevileges();
	
				txtDateFrom.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
				txtDateTO.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
				DropCustID.SelectedIndex=0;
				GridCreditBill.DataSource=null;
				GridCreditBill.DataBind();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Credit_Bill.aspx,Class:PetrolPumpClass.cs,Method:Print  "+sql1+"    ====="+ ex.Message+"  EXCEPTION  "+uid);
			}
		}

		protected void GridCreditBill_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		protected void DropVehicleNo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DropCustID.SelectedIndex == 0)
			{
				MessageBox.Show("Please Select the Customer Name");
				return;
			}
			displayReport(); 
		}
	}
}