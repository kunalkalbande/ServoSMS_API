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
	/// Summary description for Product_Report.
	/// </summary>
	public partial class ProductReport : System.Web.UI.Page
	{
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string strOrderBy="";
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception es)
			{
				CreateLogFiles.ErrorLog("Form:Fleet/OE discount.aspx,Method:page_load  EXCEPTION "+ es.Message+" userid "+  uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack)
			{
				GetMultiValue();
				GridReport.Visible=false;
				
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="30";
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
				
				//				txtDateFrom.Text=DateTime.Now.Day +"/"+ DateTime.Now.Month+"/"+ DateTime.Now.Year; 
				//				Textbox1.Text = DateTime.Now.Day+ "/"+ DateTime.Now.Month +"/"+ DateTime.Now.Year;

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
		/// This method is used to view the report with the help of BindTheData() function and set the column name 
		/// with ascending order in session variable.
		/// </summary>
		protected void btnShow_Click(object sender, System.EventArgs e)
		{
			try
			{
				strOrderBy = "Prod_Name ASC";
				Session["Column"] = "Prod_Name";
				Session["Order"] = "ASC";
				BindTheData();
				CreateLogFiles.ErrorLog("Form:ProductReportReport.aspx,Method:btnShow_Click"+ " Product Report Viewed "+" userid "+ uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Method:btnShow_Click"+  " Product Report Viewed "+"  EXCEPTION "+ex.Message+" userid  "+uid);
			}
		}

		/// <summary>
		/// This method is used to binding the datagrid with the help of sql query.
		/// </summary>
		public void BindTheData()
		{
			//InventoryClass obj=new InventoryClass();
			//SqlDataReader rdr=null;
			//SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			//SqlCon.Open();
			//string sqlstr="select Prod_Code,Category,Prod_Name,Pack_Type from Products";
			//SqlDtr=obj.GetRecordSet(sqlstr);
			//SqlCommand cmd;
			//string PName="";
			//	dbobj.SelectQuery(sqlstr,ref rdr);
			//	while(rdr.Read())
			//	{
			//		PName=rdr["Prod_Name"].ToString();
			//		if(PName.StartsWith("SERVO"))
			//			PName=PName.Substring(5);
			//dbobj.ExecProc(OprType.Insert,"sp_stockmovement",ref op,"@id",Int32.Parse(rdr["productid"].ToString()),"@fromdate",getdate(txtDateFrom.Text,true).Date.ToShortDateString(),"@todate",getdate(txtDateTo.Text,true).Date.ToShortDateString());
			//count++;
			//		cmd=new SqlCommand("insert into ProdTable values("+rdr.GetValue(0).ToString()+",'"+rdr.GetValue(1).ToString()+"','"+PName+"','"+rdr.GetValue(3).ToString()+"')",SqlCon);
			//		cmd.ExecuteNonQuery();
			//	}
			//	rdr.Close();
			//	SqlCon.Close();
			//	SqlCon.Open();
			
			//string sqlstr="select Category,Prod_Name,Pack_Type from Products";
			/********** Impliment to hase table *************
			SqlDtr=obj.GetRecordSet(sqlstr);
			Hashtable ht1 = new Hashtable();
			int i=0;
			ArrayList Data=new ArrayList();
			
			Hashtable ht=new Hashtable();
			Data.Add("Category");
			Data.Add("Prod_Name");
			Data.Add("Pack_Type");
			Data.Add("Prod_Code");
			ht.Add(i,Data);
			i++;
			while(SqlDtr.Read())
			{
				
				Data.Add(SqlDtr["Category"].ToString());
				Data.Add(SqlDtr["Prod_Name"].ToString());
				Data.Add(SqlDtr["Pack_Type"].ToString());
				Data.Add(SqlDtr["Prod_Code"].ToString());
				ProdName[i]=SqlDtr["Prod_Name"].ToString();
				PackType[i]=SqlDtr["Pack_Type"].ToString();
				ProdCode[i]=SqlDtr["Prod_Code"].ToString();
				ht.Add(i,Data);
				i++;
			}
			*/
			//SqlDataReader rdr=null;
			SqlConnection SqlCon =new SqlConnection(System .Configuration.ConfigurationSettings.AppSettings["Servosms"]);
			SqlCon.Open();
			string sqlstr="select Prod_ID,Prod_Code,Category,Prod_Name,Pack_Type,Unit,Total_Qty,Store_In from Products";


			if(DropValue.Value!="All")
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
					sqlstr+=" where prod_name='"+pname[0].ToString()+"' and pack_type='"+pname[1].ToString()+"'";
				}
				else if(DropSearchBy.SelectedIndex==2)
					sqlstr+=" where Prod_Name='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==3)
					sqlstr+=" where Pack_Type='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==4)
					sqlstr+=" where Category='"+DropValue.Value+"'";
			}

			/*
			SqlCommand cmd;
			string PName="";
			dbobj.SelectQuery(sqlstr,ref rdr);
			while(rdr.Read())
			{
				PName=rdr["Prod_Name"].ToString();
				if(PName.StartsWith("SERVO"))
					PName=PName.Substring(5);
				cmd=new SqlCommand("insert into ProdTable values("+rdr.GetValue(0).ToString()+",'"+rdr.GetValue(1).ToString()+"','"+rdr.GetValue(2).ToString()+"','"+PName.Trim().ToString()+"','"+rdr.GetValue(4).ToString()+"')",SqlCon);
				cmd.ExecuteNonQuery();
			}
			rdr.Close();
			SqlCon.Close();
			SqlCon.Open();
			int x=0;
			sqlstr="select Prod_ID,Prod_Code,Category,Prod_Name,Pack_Type from ProdTable";
			*/
			DataSet ds= new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(sqlstr, SqlCon);
			da.Fill(ds, "Products");
			DataTable dtCustomers = ds.Tables["Products"];
			DataView dv=new DataView(dtCustomers);
			dv.Sort = strOrderBy;
			Cache["strOrderBy"]=strOrderBy;
			if(dv.Count==0)
			{
				MessageBox.Show("Data not available");
				GridReport.Visible=false;
			}
			else
			{
				GridReport.DataSource=dv;
				GridReport.DataBind();
				GridReport.Visible=true;
			}
			SqlCon.Close();
			// truncate table after use.
			//dbobj.Insert_or_Update("truncate table ProdTable", ref x);
			//TextBox1.Text=ht.Values.ToString();
			/*
			GridReport.DataSource=ht.Values;
			GridReport.DataBind();
			GridReport.Visible=true;*/
		}


		public void GetMultiValue()
		{
			try
			{
				InventoryClass obj = new InventoryClass();
				SqlDataReader rdr=null;
				
				string strProductGroup="",strProdWithPack="",strProduct="",strPack="";
				
				strProductGroup="select distinct category from Products";
				strProdWithPack="select distinct Prod_Name+':'+pack_type from Products";
				strProduct="select distinct Prod_Name from Products";
				strPack="select distinct pack_type from Products";
						
				string[] arrStr = {strProdWithPack,strProduct,strPack,strProductGroup};
				
				HtmlInputHidden[] arrCust = {tempProdWithPack,tempProduct,tempPack,tempProductGroup};

				for(int i=0; i<arrStr.Length; i++)
				{
					rdr = obj.GetRecordSet(arrStr[i].ToString());
					if(rdr.HasRows)
					{
						arrCust[i].Value="All,";
						while(rdr.Read())
						{
								arrCust[i].Value+=rdr.GetValue(0).ToString()+",";
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

		/// <summary>
		/// This method is used to make sorting the datagrid onclicking of the datagridheader.
		/// </summary>
		public void SortCommand_Click(object sender,DataGridSortCommandEventArgs e)
		{
			try
			{
				//Check to see if same column clicked again
				if(e.SortExpression.ToString().Equals(Session["Column"]))
				{
					if(Session["Order"].Equals("ASC"))
					{
						strOrderBy=e.SortExpression.ToString() +" DESC";
						Session["Order"]="DESC";
					}
					else
					{
						strOrderBy=e.SortExpression.ToString() +" ASC";
						Session["Order"]="ASC";
					}
				}
					//Different column selected, so default to ascending order
				else
				{
					strOrderBy = e.SortExpression.ToString() +" ASC";
					Session["Order"] = "ASC";
				}
				Session["Column"] = e.SortExpression.ToString();
				BindTheData();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Method : ShortCommand_Click,  EXCEPTION  "+ex.Message+" userid ");
			}
		}

		/// <summary>
		/// This method is used to Returns date in MM/DD/YYYY format.
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
		/// This method is used to contacts the print server and sends the ProductReport.txt file name to print.
		/// </summary>
		protected void BtnPrint_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];
			// Connect to a remote device.
			try 
			{
				makingReport();
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
					CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Product Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ProductReport.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Product Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Product Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Product Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    Product Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to Method to write into the report file to print.
		/// </summary>
		public void makingReport()
		{
			try
			{
				System.Data.SqlClient.SqlDataReader rdr=null;
				string home_drive = Environment.SystemDirectory;
				home_drive = home_drive.Substring(0,2); 
				string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ProductReport.txt";
				StreamWriter sw = new StreamWriter(path);
				string sql="";
				string info = "";
				//string strDate = "";
				//sql="select lr.Emp_ID r1,e.Emp_Name r2,lr.Date_From r3,lr.Date_To r4,lr.Reason r5,lr.isSanction r6 from Employee e,Leave_Register lr where e.Emp_ID=lr.Emp_ID and cast(floor(cast(lr.Date_From as float)) as datetime)>='"+ ToMMddYYYY(txtDateFrom.Text)  +"' and cast(floor(cast(lr.Date_To as float)) as datetime)<='"+ ToMMddYYYY(Textbox1.Text) +"'";
				//Coment by vikas 23.10.09 sql="select Category,Prod_Name,Pack_Type,Prod_Code,Unit,Total_Qty,Store_In from Products";
				sql="select Category,Prod_Name,Pack_Type,Prod_Code,Unit,Total_Qty,Store_In,Prod_id from Products";

				/*******Add by vikas 18.07.09***********************/
				if(DropValue.Value!="All")
				{
					if(DropSearchBy.SelectedIndex==1)
					{
						string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
						sql+=" where prod_name='"+pname[0].ToString()+"' and pack_type='"+pname[1].ToString()+"'";
					}
					else if(DropSearchBy.SelectedIndex==2)
						sql+=" where Prod_Name='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==3)
						sql+=" where Pack_Type='"+DropValue.Value+"'";
					else if(DropSearchBy.SelectedIndex==4)
						sql+=" where Category='"+DropValue.Value+"'";
				}
				/********End**********************/

				sql=sql+" order by "+Cache["strOrderBy"];
				dbobj.SelectQuery(sql,ref rdr);
				// Condensed
				sw.Write((char)27);      //added by vishnu
				sw.Write((char)67);      //added by vishnu
				sw.Write((char)0);       //added by vishnu
				sw.Write((char)12);      //added by vishnu
			
				sw.Write((char)27);      //added by vishnu
				sw.Write((char)78);      //added by vishnu
				sw.Write((char)5);       //added by vishnu
							
				sw.Write((char)27);      //added by vishnu
				sw.Write((char)15);
				//**********
				string des="----------------------------------------------------------------------------------------------------------------------------------------";
				string Address=GenUtil.GetAddress();
				string[] addr=Address.Split(new char[] {':'},Address.Length);
				sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
				sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
				sw.WriteLine(des);
				//**********
				sw.WriteLine(GenUtil.GetCenterAddr("================",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("PRODUCT REPORT",des.Length));
				sw.WriteLine(GenUtil.GetCenterAddr("================",des.Length));
				
				/*Coment by vikas 23.10.09 sw.WriteLine("+------------+-------------------------+--------------------------------------------------+---------------+--------+--------+----------+");
				sw.WriteLine("|Product Code|        Category         |                  Product Name                    |   Pack Type   |  unit  |Unit Qty| Store In |");
				sw.WriteLine("+------------+-------------------------+--------------------------------------------------+---------------+--------+--------+----------+");
				//             123456789012 1234567890123456789012345 12345678901234567890123456789012345678901234567890 123456789012345 */

				sw.WriteLine("+----------+------------+--------------------+--------------------------------------------+---------------+--------+--------+----------+");
				sw.WriteLine("|Product ID|Product Code|        Category    |             Product Name                   |   Pack Type   |  unit  |Unit Qty| Store In |");
				sw.WriteLine("+----------+------------+--------------------+--------------------------------------------+---------------+--------+--------+----------+");
				//             1234567890 123456789012 12345678901234567890 12345678901234567890123456789012345678901234 123456789012345 12345678 12345678 1234567890

				//string str="";
				
				if(rdr.HasRows)
				{
					//Coment by vikas 23.10.09 info = " {0,-12:S} {1,-25:F} {2,-50:S} {3,-15:S} {4,-8:S} {5,8:S} {6,-10:S}"; 
					info = " {0,-10:S} {1,-12:F} {2,-20:S} {3,-44:S} {4,-15:S} {5,8:S} {6,-8:S} {7,-10:S}"; 
					while(rdr.Read())
					{
						sw.WriteLine(info,rdr["Prod_ID"].ToString(),
							rdr["Prod_Code"].ToString(),
							rdr["Category"].ToString(),
							rdr["Prod_Name"].ToString().Trim(),
							rdr["Pack_Type"].ToString(),
							rdr["Unit"].ToString(),
							rdr["Total_Qty"].ToString(),
							rdr["Store_In"].ToString()
							);
					}
				}
				sw.WriteLine("+----------+------------+--------------------+--------------------------------------------+---------------+--------+--------+----------+");
				dbobj.Dispose();
				sw.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			string path = home_drive+@"\Servosms_ExcelFile\Export\ProductReport.xls";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			//Coment by vikas 23.10.09 sql="select Category,Prod_Name,Pack_Type,Prod_Code,Unit,Total_Qty,Store_In from Products";
			sql="select Category,Prod_Name,Pack_Type,Prod_Code,Unit,Total_Qty,Store_In,prod_id from Products";
			
			/*******Add by vikas 18.07.09***********************/
			if(DropValue.Value!="All")
			{
				if(DropSearchBy.SelectedIndex==1)
				{
					string[] pname=DropValue.Value.Split(new char[] {':'},DropValue.Value.Length);
					sql+=" where prod_name='"+pname[0].ToString()+"' and pack_type='"+pname[1].ToString()+"'";
				}
				else if(DropSearchBy.SelectedIndex==2)
					sql+=" where Prod_Name='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==3)
					sql+=" where Pack_Type='"+DropValue.Value+"'";
				else if(DropSearchBy.SelectedIndex==4)
					sql+=" where Category='"+DropValue.Value+"'";
			}
			/********End**********************/

			sql=sql+" order by "+Cache["strOrderBy"];
			
			rdr=obj.GetRecordSet(sql);
			sw.WriteLine("Product ID\tProduct Code\tCategory\tProduct Name\tPack Type\tunit\tUnit Qty\tStore In");
			while(rdr.Read())
			{
				sw.WriteLine(rdr["Prod_id"].ToString()+"\t"+
					rdr["Prod_Code"].ToString()+"\t"+
					rdr["Category"].ToString()+"\t"+
					rdr["Prod_Name"].ToString().Trim()+"\t"+
					rdr["Pack_Type"].ToString()+"\t"+
					rdr["Unit"].ToString()+"\t"+
					rdr["Total_Qty"].ToString()+"\t"+
					rdr["Store_In"].ToString()
					);
			}
			rdr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method is not used.
		/// </summary>
		public string ProdName(string str)
		{
			if(str.StartsWith("SERVO"))
				str=str.Substring(5);
			return str.Trim();
		}

		/// <summary>
		/// This method is used to prepares the excel report file ProductReport.xls for printing.
		/// </summary>
		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(GridReport.Visible==true)
				{
					ConvertToExcel();
					MessageBox.Show("Successfully Convert File Into Excel Format");
					CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click   Product Report Convert Into Excel Format, userid  "+uid);
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
				CreateLogFiles.ErrorLog("Form:ProductReport.aspx,Class:PetrolPumpClass.cs,Method:btnExcel_Click    Product Report Viewed  "+ ex.Message+ "  EXCEPTION " +" userid  "+uid);
			}
		}
	}
}