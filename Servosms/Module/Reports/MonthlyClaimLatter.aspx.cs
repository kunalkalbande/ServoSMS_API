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
using Servosms.Sysitem.Classes ;
using System.Data.SqlClient;
using RMG;
using System.IO;
using DBOperations;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class MonthlyClaimLatter : System.Web.UI.Page
	{
		string uid;
		DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		public static string Invoice_Amount="0",Invoice_Qty="0",Cash_Dis="0",Trade_Dis="0",EarlyBird_Dis="0",Claim_Amt="0",OEM_Amt="0",Fleet_Amt="0";
		
		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:PageLoad  Exception "+ex.Message+"  userid  "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			btnPrint.Visible=false;
			InventoryClass obj = new InventoryClass();
			SqlDataReader SqlDtr;
			string str = "select * from organisation";
			SqlDtr = obj.GetRecordSet(str);
			if(SqlDtr.Read())
			{
				imgLogo.ImageUrl=SqlDtr["Logo"].ToString();
			}
			SqlDtr.Close();
			if(!Page.IsPostBack)
			{
				// To checks the user privileges from session.
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="5";
				string SubModule="22";
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
				//btnPrint.Visible=false;
				panMonth.Visible=true;
			}
			else
			{
				if(DropMonth.SelectedIndex!=0 && DropYear.SelectedIndex!=0)
					panMonth.Visible=false;
				else
					panMonth.Visible=true;
			}
			
		}

		/// <summary>
		/// This method is used to return the only year from given string.
		/// </summary>
		public string DateToYear(string str)
		{
			string[] dt1=str.Split(new char[] {' '},str.Length);
			string[] dt= dt1[0].IndexOf("/")>0? dt1[0].Split(new char[] { '/' }, dt1[0].Length) : dt1[0].Split(new char[] {'-'},dt1[0].Length);
			return dt[2];
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
		/// contacts the print server and sends the MonthlyClaimLatter.txt file name to print before 
		/// preparing the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnPrint_Click(object sender, System.EventArgs e)
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
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonthlyClaimLatter Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MonthlyClaimLatter.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnprint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to prepares the report file MonthlyClaimLatter.txt for printing.
		/// </summary>
		public void MakingReport()
		{
			InventoryClass obj = new InventoryClass();
			System.Data.SqlClient.SqlDataReader SqlDtr,rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\MonthlyClaimLatter.txt";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			sql="select * from Organisation";
			SqlDtr=obj.GetRecordSet(sql);
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
			string des1="+---------------------------------------------------------------------------------------------------------------------------------------+";
			/*
			string Address=GenUtil.GetAddress();
			string[] addr=Address.Split(new char[] {':'},Address.Length);
			sw.WriteLine(GenUtil.GetCenterAddr(addr[0],des.Length).ToUpper());
			sw.WriteLine(GenUtil.GetCenterAddr(addr[1]+addr[2],des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Tin No : "+addr[3],des.Length));
			sw.WriteLine(des);
			*/
			//**********
			/*
			sw.WriteLine(GenUtil.GetCenterAddr("======================================",des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("Monthly Claim Latter For "+DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,des.Length));
			sw.WriteLine(GenUtil.GetCenterAddr("======================================",des.Length));
			*/
			info = " {0,-20:S} {1,-50:S} {2,-30:S} {3,-30:S}";
			string info1 = "|{0,-5:S}|{1,-16:S}|{2,-16:S}|{3,-15:S}|{4,-15:S}|{5,-15:S}|{6,-15:S}|{7,-15:S}|{8,-15:S}|";
			if(SqlDtr.Read())
			{
				sw.WriteLine(SqlDtr["DealerName"].ToString().ToUpper(),"");
				sw.WriteLine("SOLE EXCLUSIVE SERVO STOCKIST(AUTOMOTIVE)","");
				sw.WriteLine(SqlDtr["Address"].ToString()+" "+SqlDtr["City"].ToString());
				sw.WriteLine("Tele. No      "+SqlDtr["PhoneNo"].ToString());
				sw.WriteLine("Fax No        "+SqlDtr["FaxNo"].ToString());
				sw.WriteLine("Mobile        ");
				sw.WriteLine("E-Mail        "+SqlDtr["EMail"].ToString());
				sw.WriteLine("Web Site      "+SqlDtr["WebSite"].ToString());
				sw.WriteLine(des);
				sw.WriteLine("To,");
				sw.WriteLine("RETAIL SALES EXECUTIVE                 Ref:    Claim/SSA-"+SqlDtr["City"].ToString()+"/"+DateToYear(SqlDtr["Acc_Date_From"].ToString())+"-"+DateToYear(SqlDtr["Acc_Date_To"].ToString()));
				sw.WriteLine("INDIAN OIL CORPORATION LIMITED         Dated:  "+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year);
				sw.WriteLine(SqlDtr["Div_Office"].ToString());
				sw.WriteLine();
				sw.WriteLine("Sub: Secondary Sales Claim For The Month of "+DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text);
				sw.WriteLine();
				sw.WriteLine("Dear Sir");
				sw.WriteLine();
				sw.WriteLine("We are submitting our monthly secondary sales claim for the month of "+
					DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text+
					" and we are also attaching all essential document for");
				sw.WriteLine("approving our claim easier "+
					"with company rules. All Scheme Given to party as per policy. no PRCN or party "+
					"bill cancelled in "+
					DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text);
				sw.WriteLine();
				dbobj.SelectQuery("select * from claimanalysis where claimID='"+DropMonth.SelectedItem.Text+DropYear.SelectedItem.Text+"'",ref rdr);
				int c=0;
				if(rdr.HasRows)
				{
					TextBox[] TypeofClaim = {txtTypeofclaim1,txtTypeofclaim2,txtTypeofclaim3,txtTypeofclaim4,txtTypeofclaim5,txtTypeofclaim6,txtTypeofclaim7,txtTypeofclaim8};
					TextBox[] Period = {txtPeriod1,txtPeriod2,txtPeriod3,txtPeriod4,txtPeriod5,txtPeriod6,txtPeriod7,txtPeriod8};
					TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
					sw.WriteLine(info,"Sr.No.","Type of Claim","Period","Amount");
					double tot=0;
					for(int i=0;i<TypeofClaim.Length;i++)
					{
						if(TypeofClaim[i].Text!="" || Period[i].Text!="" || Amount[i].Text!="")
						{
							sw.WriteLine(info,System.Convert.ToString(i+1),TypeofClaim[i].Text,Period[i].Text,Amount[i].Text);
							tot+=double.Parse(Amount[i].Text);
							c++;
						}
					}
					sw.WriteLine(info,"","Total Claim Amount","",GenUtil.strNumericFormat(tot.ToString()));
					while(c<8)
					{
						TypeofClaim[c].Text = "";
						Period[c].Text = "";
						Amount[c].Text = "";
						c++;
					}
				}
				else
				{
					sw.WriteLine(info,"1","Secondary Sales Claim",DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,Claim_Amt);
					sw.WriteLine(info,"2","Fleet Operator Claim",DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,Fleet_Amt);
					sw.WriteLine(info,"3","OE Dealer Claim",DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,OEM_Amt);
					sw.WriteLine(info,"","Total Claim Amount","",GenUtil.strNumericFormat((double.Parse(Claim_Amt)+double.Parse(Fleet_Amt)+double.Parse(OEM_Amt)).ToString()));
				}
				rdr.Close();
				//sw.WriteLine();
				sw.WriteLine(des1);
				sw.WriteLine(info1,"S.No.","     Total","    Invoice","     Cash","     Trade","  Early Bird","  Free Carton","   Old Rate","     Total");
				sw.WriteLine(info1,"","  Invoice Qty","     Amount","   Discount","   Discount","","   Discount","Diff. Discount","");
				sw.WriteLine(des1);
				sw.WriteLine(info1,"  1",Invoice_Qty+" Ltr.",Invoice_Amount,Cash_Dis,Trade_Dis,EarlyBird_Dis,"","",GenUtil.strNumericFormat((double.Parse(Cash_Dis)+double.Parse(Trade_Dis)+double.Parse(EarlyBird_Dis)).ToString()));
				sw.WriteLine(des1);
				sw.WriteLine(info1,"","   Sec. Claim","      OEM","     Fleet","      IBP","","","","     Total");
				sw.WriteLine(info1,"","     Amount","     Amount","    Amount","    Amount","","","","    Amount");
				sw.WriteLine(des1);
				sw.WriteLine(info1,"  2",Claim_Amt,OEM_Amt,Fleet_Amt,"0.00","","","",GenUtil.strNumericFormat((double.Parse(Claim_Amt)+double.Parse(OEM_Amt)+double.Parse(Fleet_Amt)).ToString()));
				sw.WriteLine(des1);
				sw.WriteLine();
				sw.WriteLine("Enclosed:");
				sw.WriteLine("1) CFA Purchase &amp; SSA Sales Invoice Copies.");
				sw.WriteLine("2) Secondary Sales / Purchase statement.");
				sw.WriteLine("3) Fleet / OE Claim Statement (With Bills Attached)");
				sw.WriteLine("4) Attached Sales figure and Stock Movement, Stock "+
					"list (Servosms Generated).");
				sw.WriteLine("");
				sw.WriteLine("Thanks &amp; Regards,");
				sw.WriteLine(SqlDtr["DealerName"].ToString());
				sw.WriteLine("");
				sw.WriteLine("");
				sw.WriteLine("("+SqlDtr["FoodLicNo"].ToString()+")");
			}
			else
			{
				MessageBox.Show("First Fill The Organisation Detail Form");
				return;
			}
			SqlDtr.Close();
			//			txtTypeofclaim1.Text="Secodary Sales Claim";
			//			txtPeriod1.Text=DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text;
			//			txtAmount1.Text=Claim_Amt;
			//			txtTypeofclaim2.Text="Fleet Operator Claim";
			//			txtPeriod2.Text=DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text;
			//			txtAmount2.Text=Fleet_Amt;
			//			txtTypeofclaim3.Text="OE Dealer Claim";
			//			txtPeriod3.Text=DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text;
			//			txtAmount3.Text=OEM_Amt;
			//			double total=0;
			//			if(txtAmount1.Text!="")
			//				total+=double.Parse(txtAmount1.Text);
			//			if(txtAmount2.Text!="")
			//				total+=double.Parse(txtAmount2.Text);
			//			if(txtAmount3.Text!="")
			//				total+=double.Parse(txtAmount3.Text);
			//			txtTotal.Text=GenUtil.strNumericFormat(total.ToString());
			sw.Close();
		}

		/// <summary>
		/// This method is used to prepares the report file MonthlyClaimLatter.doc for printing in MS Word format.
		/// </summary>
		public void MakingReportInMSWord()
		{
			InventoryClass obj = new InventoryClass();
			System.Data.SqlClient.SqlDataReader SqlDtr,rdr=null;
			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Servosms_ExcelFile\Export\MonthlyClaimLatter.doc";
			StreamWriter sw = new StreamWriter(path);
			string sql="";
			string info = "";
			// Condensed
			//			sw.Write((char)27);//added by vishnu
			//			sw.Write((char)67);//added by vishnu
			//			sw.Write((char)0);//added by vishnu
			//			sw.Write((char)12);//added by vishnu
			//			sw.Write((char)27);//added by vishnu
			//			sw.Write((char)78);//added by vishnu
			//			sw.Write((char)5);//added by vishnu
			//			sw.Write((char)27);//added by vishnu
			//			sw.Write((char)15);
			//**********
			sql="select * from Organisation";
			SqlDtr=obj.GetRecordSet(sql);
			string des="--------------------------------------------------------------------------------";
			string des1="+------------------------------------------------------------------------------+";
			info = " {0,-12:S} {1,-35:S} {2,-20:S} {3,-13:S}";
			string info1 = "|{0,-2:S}|{1,-11:S}|{2,-10:S}|{3,-8:S}|{4,-8:S}|{5,-7:S}|{6,-7:S}|{7,-7:S}|{8,-10:S}|";
			if(SqlDtr.Read())
			{
				sw.WriteLine(SqlDtr["DealerName"].ToString().ToUpper(),"");
				sw.WriteLine("SOLE EXCLUSIVE SERVO STOCKIST(AUTOMOTIVE)","");
				sw.WriteLine(SqlDtr["Address"].ToString()+" "+SqlDtr["City"].ToString());
				sw.WriteLine("Tele. No    "+SqlDtr["PhoneNo"].ToString());
				sw.WriteLine("Fax No      "+SqlDtr["FaxNo"].ToString());
				sw.WriteLine("Mobile      ");
				sw.WriteLine("E-Mail      "+SqlDtr["EMail"].ToString());
				sw.WriteLine("Web Site    "+SqlDtr["WebSite"].ToString());
				sw.WriteLine(des);
				sw.WriteLine("To,");
				sw.WriteLine("RETAIL SALES EXECUTIVE              Ref:    Claim/SSA-"+SqlDtr["City"].ToString()+"/"+DateToYear(SqlDtr["Acc_Date_From"].ToString())+"-"+DateToYear(SqlDtr["Acc_Date_To"].ToString()));
				sw.WriteLine("INDIAN OIL CORPORATION LIMITED      Dated:  "+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year);
				sw.WriteLine(SqlDtr["Div_Office"].ToString());
				sw.WriteLine();
				sw.WriteLine("Sub: Secondary Sales Claim For The Month of "+DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text);
				sw.WriteLine();
				sw.WriteLine("Dear Sir");
				sw.WriteLine();
				sw.WriteLine("We are submitting our monthly secondary sales claim "+ 
					"for the month of "+
					DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text+
					" and we are also attaching all essential document for approving our claim easier "+
					"with company rules. All Scheme Given to party as per policy. no PRCN or party "+
					"bill cancelled in "+
					DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text);
				sw.WriteLine();
				dbobj.SelectQuery("select * from claimanalysis where claimID='"+DropMonth.SelectedItem.Text+DropYear.SelectedItem.Text+"'",ref rdr);
				int c=0;
				if(rdr.HasRows)
				{
					TextBox[] TypeofClaim = {txtTypeofclaim1,txtTypeofclaim2,txtTypeofclaim3,txtTypeofclaim4,txtTypeofclaim5,txtTypeofclaim6,txtTypeofclaim7,txtTypeofclaim8};
					TextBox[] Period = {txtPeriod1,txtPeriod2,txtPeriod3,txtPeriod4,txtPeriod5,txtPeriod6,txtPeriod7,txtPeriod8};
					TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
					sw.WriteLine(info,"Sr.No.","Type of Claim","Period","Amount");
					double tot=0;
					for(int i=0;i<TypeofClaim.Length;i++)
					{
						if(TypeofClaim[i].Text!="" || Period[i].Text!="" || Amount[i].Text!="")
						{
							sw.WriteLine(info,System.Convert.ToString(i+1),TypeofClaim[i].Text,Period[i].Text,Amount[i].Text);
							tot+=double.Parse(Amount[i].Text);
							c++;
						}
					}
					sw.WriteLine(info,"","Total Claim Amount","",GenUtil.strNumericFormat(tot.ToString()));
					while(c<8)
					{
						TypeofClaim[c].Text = "";
						Period[c].Text = "";
						Amount[c].Text = "";
						c++;
					}
				}
				else
				{
					sw.WriteLine(info,"Sr.No.","Type of Claim","Period","Amount");
					sw.WriteLine(info,"1","Secondary Sales Claim",DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,Claim_Amt);
					sw.WriteLine(info,"2","Fleet Operator Claim",DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,Fleet_Amt);
					sw.WriteLine(info,"3","OE Dealer Claim",DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text,OEM_Amt);
					sw.WriteLine(info,"","Total Claim Amount","",GenUtil.strNumericFormat((double.Parse(Claim_Amt)+double.Parse(Fleet_Amt)+double.Parse(OEM_Amt)).ToString()));
				}
				rdr.Close();
				//sw.WriteLine();
				sw.WriteLine(des1);
				sw.WriteLine(info1,"S.","Total","Invoice","Cash","Trade","Early","Free","OldRate","");
				sw.WriteLine(info1,"No","Invoice","Amount","Discount","Discount","Bird","Carton","Diff.","Total");
				sw.WriteLine(info1,"","Qty","","","","","Disc.","Disc.","");
				//sw.WriteLine(info1,"S.No.","       Total","      Invoice","     Cash","     Trade","  Early Bird","  Free Carton","   Old Rate","     Total");
				//sw.WriteLine(info1,"","   Qty Invoice","       Amount","   Discount","   Discount","","   Discount","Diff. Discount","");
				sw.WriteLine(des1);
				sw.WriteLine(info1,"1",Invoice_Qty+" LT",Invoice_Amount,Cash_Dis,Trade_Dis,EarlyBird_Dis,"","",GenUtil.strNumericFormat((double.Parse(Cash_Dis)+double.Parse(Trade_Dis)+double.Parse(EarlyBird_Dis)).ToString()));
				sw.WriteLine(des1);
				sw.WriteLine(info1,"","Sec. Claim","OEM","Fleet","IBP","","","","Total");
				sw.WriteLine(info1,"","Amount","Amount","Amount","Amount","","","","Amount");
				sw.WriteLine(des1);
				sw.WriteLine(info1,"2",Claim_Amt,OEM_Amt,Fleet_Amt,"0.00","","","",GenUtil.strNumericFormat((double.Parse(Claim_Amt)+double.Parse(OEM_Amt)+double.Parse(Fleet_Amt)).ToString()));
				sw.WriteLine(des1);
				sw.WriteLine();
				sw.WriteLine("Enclosed:");
				sw.WriteLine("1) CFA Purchase &amp; SSA Sales Invoice Copies.");
				sw.WriteLine("2) Secondary Sales / Purchase statement.");
				sw.WriteLine("3) Fleet / OE Claim Statement (With Bills Attached)");
				sw.WriteLine("4) Attached Sales figure and Stock Movement, Stock "+
					"list (Servosms Generated).");
				sw.WriteLine("");
				sw.WriteLine("Thanks & Regards,");
				sw.WriteLine(SqlDtr["DealerName"].ToString());
				sw.WriteLine("");
				sw.WriteLine("");
				sw.WriteLine("("+SqlDtr["FoodLicNo"].ToString()+")");
				MessageBox.Show("Successfully Convert File Into MS-Word Fromat");
			}
			else
			{
				MessageBox.Show("First Fill The Organisation Detail Form");
				return;
			}
			SqlDtr.Close();
			sw.Close();
		}

		/// <summary>
		/// This method is used to view the monthly claim latter with calculate the all primary and secondry discount.
		/// </summary>
		protected void btnView_Click(object sender, System.EventArgs e)
		{
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass();
			SqlDataReader SqlDtr,rdr;
			string StartDate="",EndDate="";
			
			int day=0;
			double TotalClaim=0;
			int i=DropMonth.SelectedIndex;
			if(DropMonth.SelectedIndex!=0)
			{
				StartDate=i+"/1/"+DropYear.SelectedItem.Text;
				day=DateTime.DaysInMonth(int.Parse(DropYear.SelectedItem.Text),i);
				EndDate=i+"/"+day+"/"+DropYear.SelectedItem.Text;
			}
			string str="select sum(net_amount) from purchase_master where cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			SqlDtr = obj.GetRecordSet(str);
			if(SqlDtr.Read())
			{
				if(!SqlDtr.GetValue(0).ToString().Equals(""))
					Invoice_Amount=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
				//MessageBox.Show("Invoice_Amount : "+Invoice_Amount);
			}
			SqlDtr.Close();
			str="select sum(totalqtyltr) from purchase_master where cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			SqlDtr = obj.GetRecordSet(str);
			if(SqlDtr.Read())
			{
				if(!SqlDtr.GetValue(0).ToString().Equals(""))
					Invoice_Qty=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
				//MessageBox.Show("Invoice_Qty : "+Invoice_Qty);
			}
			SqlDtr.Close();
			//****************
			str = "select * from purchase_master where cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			double TotalCash=0;
			SqlDtr = obj.GetRecordSet(str);
			while(SqlDtr.Read())
			{
				double focDisc=0,fixedDisc=0,Disc=0,CashDisc=0;//ebird=0;
				double grandtot=System.Convert.ToDouble(SqlDtr.GetValue(7).ToString());
				fixedDisc=System.Convert.ToDouble(SqlDtr.GetValue(27).ToString());
				focDisc=System.Convert.ToDouble(SqlDtr.GetValue(24).ToString());
				double ETFOC = (focDisc*2)/100;
				if(SqlDtr.GetValue(25).ToString().Equals("Per"))
					focDisc=grandtot*focDisc/100;
				Disc=System.Convert.ToDouble(SqlDtr.GetValue(8).ToString());
				if(SqlDtr.GetValue(9).ToString().Equals("Per"))
					Disc=grandtot*Disc/100 ;
				double etax=System.Convert.ToDouble(SqlDtr.GetValue(22).ToString());
				if(SqlDtr.GetValue(23).ToString().Equals("Per"))
					etax=grandtot*etax/100 ;
				CashDisc=System.Convert.ToDouble(SqlDtr.GetValue(15).ToString());
				double GT=0;
				if(SqlDtr.GetValue(16).ToString().Equals("Per"))
				{
					GT=grandtot+ etax-(System.Convert.ToDouble(SqlDtr.GetValue(19).ToString())+System.Convert.ToDouble(SqlDtr.GetValue(21).ToString())+focDisc+Disc+fixedDisc+ETFOC);
					TotalCash+=GT*CashDisc/100;
				}
				else
					TotalCash+=CashDisc;
			}
			SqlDtr.Close();
			Cash_Dis=GenUtil.strNumericFormat(TotalCash.ToString());
			//****************
			//			str="select sum(cash_discount) from purchase_master where cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			//			SqlDtr = obj.GetRecordSet(str);
			//			if(SqlDtr.Read())
			//			{
			//				if(!SqlDtr.GetValue(0).ToString().Equals(""))
			//					Cash_Dis=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
			//				//MessageBox.Show("Cash Discount : "+Cash_Dis);
			//			}
			//			SqlDtr.Close();
			str="select sum(Trade_Discount),sum(ebird_discount) from purchase_master where cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			SqlDtr = obj.GetRecordSet(str);
			if(SqlDtr.Read())
			{
				if(!SqlDtr.GetValue(0).ToString().Equals(""))
					Trade_Dis=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
				if(!SqlDtr.GetValue(1).ToString().Equals(""))
					EarlyBird_Dis=GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString());
				//MessageBox.Show("Trade Discount : "+Trade_Dis);
			}
			SqlDtr.Close();
//			str="select sum(ebird_discount) from purchase_master where cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
//			SqlDtr = obj.GetRecordSet(str);
//			if(SqlDtr.Read())
//			{
//				if(!SqlDtr.GetValue(0).ToString().Equals(""))
//					EarlyBird_Dis=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString());
//				//MessageBox.Show("EarlyBird Discount : "+EarlyBird_Dis);
//			}
//			SqlDtr.Close();
			
			//******* To Calculate The Secondaty Claim Amount *********
			//str="select sales,salesfoc,productid from stock_master where cast(floor(cast(Stock_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Stock_Date as float)) as datetime)<='"+ EndDate +"'";
			str="select sum(sales)*total_qty sales,sum(salesfoc) salesfoc,productid,total_qty from stock_master sm,products p where p.prod_id=sm.productid and cast(floor(cast(Stock_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Stock_Date as float)) as datetime)<='"+ EndDate +"' group by productid,total_qty,prod_code order by prod_code";
			SqlDtr = obj.GetRecordSet(str);
			
			while(SqlDtr.Read())
			{	
				//string Prod_ID="",Pack_Type="";
				double Sales=0,Count=0;//,SalesFOC=0;
				
				//				Prod_ID=SqlDtr.GetValue(2).ToString();
				//				str="select Pack_Type from Products where Prod_id='"+Prod_ID+"'";
				//				rdr = obj1.GetRecordSet(str);
				//				if(rdr.Read())
				//				{
				//					Pack_Type=rdr.GetValue(0).ToString();
				//				}
				//				rdr.Close();
				//Sales=Multiplyfoc(SqlDtr.GetValue(0).ToString()+"X"+Pack_Type);
				Sales=double.Parse(SqlDtr.GetValue(0).ToString());
				//SalesFOC=Multiplyfoc(SqlDtr.GetValue(1).ToString()+"X"+Pack_Type);
				str="select discount from oilscheme where Prodid='"+SqlDtr["Productid"].ToString()+"' and cast(floor(cast(datefrom as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ EndDate +"' and (schname='Secondry(LTR Scheme)' or schname='Primary(LTR&% Scheme)')";
				rdr = obj1.GetRecordSet(str);
				if(rdr.Read())
				{
					if(!rdr.GetValue(0).ToString().Equals("") || !rdr.GetValue(0).ToString().Equals("0"))
					{
						//Count=(Sales-SalesFOC)*double.Parse(rdr.GetValue(0).ToString());
						string ss = rdr.GetValue(0).ToString();
						Count=Sales*double.Parse(rdr.GetValue(0).ToString());
					}
					TotalClaim=TotalClaim+Count;
				}
				rdr.Close();
				Claim_Amt=GenUtil.strNumericFormat(TotalClaim.ToString());
			}
			SqlDtr.Close();
			//MessageBox.Show("Sec. Claim Amount : "+Claim_Amt);
			//****************
			//******* To Calculate The Fleet Amount *********
			SqlDataReader rdr1=null;
			str="select pack_Type,Qty,schdiscount,Prod_Name,Pack_Type,foe from vw_fleetoediscount where Cust_Type='Fleet' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			SqlDtr = obj.GetRecordSet(str);
			double Total=0;
			while(SqlDtr.Read())
			{	
				string Prod_ID="";
				double DiscGiven=0,Scheme=0;//,FOC=0;
				//DiscGiven=System.Convert.ToDouble(SqlDtr["schdiscount"].ToString());
				//********
				DiscGiven=System.Convert.ToDouble(SqlDtr["foe"].ToString());
				DiscGiven=DiscGiven*Multiply(SqlDtr["qty"].ToString()+"X"+SqlDtr["Pack_Type"].ToString());
				//*********
				str="select Prod_ID from Products where Prod_Name='"+SqlDtr["Prod_Name"].ToString()+"' and Pack_Type='"+SqlDtr["Pack_Type"].ToString()+"'";
				rdr = obj1.GetRecordSet(str);
				if(rdr.Read())
				{
					Prod_ID=rdr.GetValue(0).ToString();
				}
				rdr.Close();
				str="select discount from oilscheme where Prodid='"+Prod_ID+"' and cast(floor(cast(datefrom as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ EndDate +"'";
				rdr = obj1.GetRecordSet(str);
				if(rdr.Read())
				{
					if(!rdr.GetValue(0).ToString().Equals("") || !rdr.GetValue(0).ToString().Equals("0"))
					{
						//Scheme=Multiply(rdr.GetValue(0).ToString()+"X"+SqlDtr["Pack_Type"].ToString());
						Scheme=Multiply(SqlDtr["qty"].ToString()+"X"+SqlDtr["Pack_Type"].ToString());
						//********
						Scheme=Scheme*double.Parse(rdr["discount"].ToString());
						//********
					}
				}
				rdr.Close();
				//				str="select discount from foe where Prodid='"+Prod_ID+"' and ctype='Fleet' and cast(floor(cast(datefrom as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ EndDate +"'";
				//				rdr = obj1.GetRecordSet(str);
				//				if(rdr.Read())
				//				{
				//					if(!rdr.GetValue(0).ToString().Equals("") || !rdr.GetValue(0).ToString().Equals("0"))
				//					{
				//						FOC=Multiply(rdr.GetValue(0).ToString()+"X"+SqlDtr["Pack_Type"].ToString());
				//					}
				//				}
				//				rdr.Close();
				//*********************
				string schprodid="";//,prodid="";
				double free=0,onevery=0,Tot=0;
				string sql1="select freepack,onevery,schprodid from oilscheme where prodid='"+Prod_ID+"' and  cast(floor(cast(datefrom as float)) as datetime) <= '"+StartDate+"' and cast(floor(cast(dateto as float)) as datetime) >= '"+EndDate+"' and schprodid<>0";
				// Calls the sp_stockmovement for each product and create one stkmv temp. table.
				dbobj.SelectQuery(sql1,ref rdr);
				if(rdr.Read())
				{
					free=System.Convert.ToDouble(rdr.GetValue(0).ToString());
					onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
					schprodid=rdr.GetValue(2).ToString();
				}
				rdr.Close();
				if(free>0)
				{
					//string sql1="select freepack,onevery from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
					sql1="select Top 1(sal_rate) from price_updation where prod_id='"+schprodid+"' order by eff_date desc";
					// Calls the sp_stockmovement for each product and create one stkmv temp. table.
					dbobj.SelectQuery(sql1,ref rdr1);
					if(rdr1.Read())
					{
						Tot=System.Convert.ToDouble(rdr1.GetValue(0).ToString())*double.Parse(SqlDtr["qty"].ToString());
						//onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
					}
					rdr1.Close();
				}
				//******************
				//Total = Total+(DiscGiven-(Scheme+FOC));
				Total = Total+(DiscGiven-(Scheme+Tot));
				
			}
			SqlDtr.Close();
			Fleet_Amt = GenUtil.strNumericFormat(Total.ToString());
			//MessageBox.Show("Fleet Amount : "+Fleet_Amt.ToString());
			//****************
			//******* To Calculate The OEM Amount *********
			str="select pack_Type,Qty,schdiscount,Prod_Name,Pack_Type,foe from vw_fleetoediscount where Cust_Type like 'oe%' and cast(floor(cast(Invoice_Date as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(Invoice_Date as float)) as datetime)<='"+ EndDate +"'";
			SqlDtr = obj.GetRecordSet(str);
			double TotalOE=0;
			while(SqlDtr.Read())
			{	
				string Prod_ID="";
				double DiscGiven=0,Scheme=0;//,FOC=0;
				//DiscGiven=System.Convert.ToDouble(SqlDtr["schdiscount"].ToString());
				//*********
				DiscGiven=System.Convert.ToDouble(SqlDtr["foe"].ToString());
				DiscGiven=DiscGiven*Multiply(SqlDtr["qty"].ToString()+"X"+SqlDtr["Pack_Type"].ToString());
				//*********
				str="select Prod_ID from Products where Prod_Name='"+SqlDtr["Prod_Name"].ToString()+"' and Pack_Type='"+SqlDtr["Pack_Type"].ToString()+"'";
				rdr = obj1.GetRecordSet(str);
				if(rdr.Read())
				{
					Prod_ID=rdr.GetValue(0).ToString();
				}
				rdr.Close();
				str="select discount from oilscheme where Prodid='"+Prod_ID+"' and cast(floor(cast(datefrom as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ EndDate +"'";
				rdr = obj1.GetRecordSet(str);
				if(rdr.Read())
				{
					if(!rdr.GetValue(0).ToString().Equals("") || !rdr.GetValue(0).ToString().Equals("0"))
					{
						//Scheme=Multiply(rdr.GetValue(0).ToString()+"X"+SqlDtr["Pack_Type"].ToString());
						Scheme=Multiply(SqlDtr["qty"].ToString()+"X"+SqlDtr["Pack_Type"].ToString());
						Scheme=Scheme*double.Parse(rdr["discount"].ToString());
					}
				}
				rdr.Close();
				//				str="select discount from foe where Prodid='"+Prod_ID+"' and ctype like 'oe%' and cast(floor(cast(datefrom as float)) as datetime)>='"+ StartDate  +"' and cast(floor(cast(dateto as float)) as datetime)<='"+ EndDate +"'";
				//				rdr = obj1.GetRecordSet(str);
				//				if(rdr.Read())
				//				{
				//					if(!rdr.GetValue(0).ToString().Equals("") || !rdr.GetValue(0).ToString().Equals("0"))
				//					{
				//						FOC=Multiply(rdr.GetValue(0).ToString()+"X"+SqlDtr["Pack_Type"].ToString());
				//					}
				//				}
				//				rdr.Close();
				//*********************
				string schprodid="";//,prodid="";
				double free=0,onevery=0,Tot=0;
				string sql1="select freepack,onevery,schprodid from oilscheme where prodid='"+Prod_ID+"' and  cast(floor(cast(datefrom as float)) as datetime) <= '"+StartDate+"' and cast(floor(cast(dateto as float)) as datetime) >= '"+EndDate+"' and schprodid<>0";
				// Calls the sp_stockmovement for each product and create one stkmv temp. table.
				dbobj.SelectQuery(sql1,ref rdr);
				if(rdr.Read())
				{
					free=System.Convert.ToDouble(rdr.GetValue(0).ToString());
					onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
					schprodid=rdr.GetValue(2).ToString();
				}
				rdr.Close();
				if(free>0)
				{
					//string sql1="select freepack,onevery from oilscheme o where prodid='"+prodid+"' and  cast(floor(cast(o.datefrom as float)) as datetime) <= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text.Trim())+"' and cast(floor(cast(o.dateto as float)) as datetime) >= '"+GenUtil.str2MMDDYYYY(Textbox1.Text.Trim()) +"'";
					sql1="select Top 1(sal_rate) from price_updation where prod_id='"+schprodid+"' order by eff_date desc";
					// Calls the sp_stockmovement for each product and create one stkmv temp. table.
					dbobj.SelectQuery(sql1,ref rdr1);
					if(rdr1.Read())
					{
						Tot=System.Convert.ToDouble(rdr1.GetValue(0).ToString())*double.Parse(SqlDtr["qty"].ToString());
						//onevery=System.Convert.ToDouble(rdr.GetValue(1).ToString());
					}
					rdr1.Close();
				}
				//******************
				//TotalOE = TotalOE+(DiscGiven-(Scheme+FOC));
				TotalOE = TotalOE+(DiscGiven-(Scheme+Tot));
				
			}
			SqlDtr.Close();
			OEM_Amt = GenUtil.strNumericFormat(TotalOE.ToString());
			btnPrint.Visible=true;
			//*****************************************************
			TextBox[] TypeofClaim = {txtTypeofclaim1,txtTypeofclaim2,txtTypeofclaim3,txtTypeofclaim4,txtTypeofclaim5,txtTypeofclaim6,txtTypeofclaim7,txtTypeofclaim8};
			TextBox[] Period = {txtPeriod1,txtPeriod2,txtPeriod3,txtPeriod4,txtPeriod5,txtPeriod6,txtPeriod7,txtPeriod8};
			TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
			SqlDtr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DropMonth.SelectedItem.Text+DropYear.SelectedItem.Text+"'");
			if(SqlDtr.HasRows)
			{
				int c=0;
				double tot=0;
				while(SqlDtr.Read())
				{
					TypeofClaim[c].Text=SqlDtr["TypeofClaim"].ToString();
					Period[c].Text=SqlDtr["Period"].ToString();
					Amount[c].Text=SqlDtr["Amount"].ToString();
					tot+=double.Parse(SqlDtr["Amount"].ToString());
					c++;
				}
				txtTotal.Text=tot.ToString();
				while(c<8)
				{
					TypeofClaim[c].Text = "";
					Period[c].Text = "";
					Amount[c].Text = "";
					c++;
				}
			}
			else
			{
				txtTypeofclaim1.Text="Secodary Sales Claim";
				txtPeriod1.Text=DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text;
				txtAmount1.Text=Claim_Amt;
				txtTypeofclaim2.Text="Fleet Operator Claim";
				txtPeriod2.Text=DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text;
				txtAmount2.Text=Fleet_Amt;
				txtTypeofclaim3.Text="OE Dealer Claim";
				txtPeriod3.Text=DropMonth.SelectedItem.Text+" "+DropYear.SelectedItem.Text;
				txtAmount3.Text=OEM_Amt;
				double total=0;
				if(txtAmount1.Text!="")
					total+=double.Parse(txtAmount1.Text);
				if(txtAmount2.Text!="")
					total+=double.Parse(txtAmount2.Text);
				if(txtAmount3.Text!="")
					total+=double.Parse(txtAmount3.Text);
				txtTotal.Text=GenUtil.strNumericFormat(total.ToString());
			}
			SqlDtr.Close();
			//MessageBox.Show("OEM Amount : "+OEM_Amt.ToString());
			//****************
		}
	
		/// <summary>
		/// This method is used to calculate the FOC qty in liter.
		/// </summary>
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
		
		/// <summary>
		/// This method is used to calculate the qty in liter.
		/// </summary>
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
				//totalltr+=ans;
				//Cache["totalltr"]=totalltr/2;
				//Cache["totalltr"]=totalltr;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					//totalltr+=System.Convert.ToDouble( mystr[0].ToString()); 
					//Cache["totalltr"]=totalltr/2;
					//Cache["totalltr"]=totalltr;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
					return 0;
			}
		}
		
		/// <summary>
		/// This method is not used.
		/// </summary>
		public double DisGiven(string ltr,string qty)
		{
			double Tot=0;
			Tot=System.Convert.ToDouble(ltr)/System.Convert.ToDouble(qty);
			return Tot;
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the MonthlyClaimLatter.txt file name to print.
		/// </summary>
		protected void LinkButton1_Click(object sender, System.EventArgs e)
		{
			byte[] bytes = new byte[1024];

			// Connect to a remote device.
			try 
			{
				
				MakingReportInMSWord();
				
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
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:lnkPrint_Clickt    MonthlyClaimLatter Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MonthlyClaimLatter.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:lnkPrint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:lnkPrint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:lnkPrint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:lnkPrint_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to save the data in ClaimAnalysis table before check the data, if yoy insert the 
		/// data in first time than save it otherwise firstly delete the data with the help of claim id and after
		/// that insert the data.
		/// </summary>
		protected void btnLinkSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				TextBox[] TypeofClaim = {txtTypeofclaim1,txtTypeofclaim2,txtTypeofclaim3,txtTypeofclaim4,txtTypeofclaim5,txtTypeofclaim6,txtTypeofclaim7,txtTypeofclaim8};
				TextBox[] Period = {txtPeriod1,txtPeriod2,txtPeriod3,txtPeriod4,txtPeriod5,txtPeriod6,txtPeriod7,txtPeriod8};
				TextBox[] Amount = {txtAmount1,txtAmount2,txtAmount3,txtAmount4,txtAmount5,txtAmount6,txtAmount7,txtAmount8};
				SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
				SqlCommand cmd;
				Con.Open();
				cmd = new SqlCommand("delete from ClaimAnalysis where claimid='"+DropMonth.SelectedItem.Text+DropYear.SelectedItem.Text+"'",Con);
				cmd.ExecuteNonQuery();
				cmd.Dispose();
				Con.Close();
				for(int i=0;i<TypeofClaim.Length;i++)
				{
					if(TypeofClaim[i].Text!="" || Period[i].Text!="" || Amount[i].Text!="")
					{
						Con.Open();
						cmd = new SqlCommand("insert into ClaimAnalysis values('"+DropMonth.SelectedItem.Text+DropYear.SelectedItem.Text+"','"+TypeofClaim[i].Text+"','"+Period[i].Text+"','"+Amount[i].Text+"')",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();
						Con.Close();
					}
				}
				MessageBox.Show("Save Data Successfully");
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnLinkSave_Clickt    MonthlyClaimLatter Report  Save"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}

		/// <summary>
		/// This method is used to contacts the print server and sends the MonthlyClaimLatter.txt file name to print
		/// With the help of MakingReport() function.
		/// </summary>
		protected void btnLinkPrintText_Click(object sender, System.EventArgs e)
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
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnLinkPrintText_Clickt    MonthlyClaimLatter Report  Printed"+"  userid  " +uid);
					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\MonthlyClaimLatter.txt<EOF>");

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
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnLinkPrintText_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+ane.Message+"  userid  " +uid);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnLinkPrintText_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+se.Message+"  userid  " +uid);
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
					CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnLinkPrintText_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+es.Message+"  userid  " +uid);
				}

			} 
			catch (Exception ex) 
			{
				CreateLogFiles.ErrorLog("Form:MonthlyClaimLatter.aspx,Class:PetrolPumpClass.cs,Method:btnLinkPrintText_Clickt    MonthlyClaimLatter Report  Printed"+"  EXCEPTION "+ex.Message+"  userid  " +uid);
			}
		}
	}
}