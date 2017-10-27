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
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
//*****
using System.Runtime;
using Microsoft.Win32;
//******
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes ;
using RMG;
using Confirm;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for Product_Entry.
	/// </summary>
	public partial class Product_Entry : System.Web.UI.Page
	{
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string pass;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
		/// and generate the next ID also.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				pass=(Session["User_Name"].ToString());
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method Page_Load "+ " EXCEPTION  "+ex.Message+" userid is   "+pass);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(tempDelinfo.Value=="Yes")
			{
				try
				{
					InventoryClass obj=new InventoryClass();
					SqlDataReader SqlDtr;
					int Count=0;
					string PName=dropProdID.SelectedItem.Text;
					string[] arrName = PName.Split(new char[] {':'},PName.Length);
					string str="select count(*) from Stock_Master where Productid=(select prod_id from products where Prod_Name='"+arrName[0].ToString()+"' and Pack_Type='"+arrName[1].ToString()+"') and (sales<>0 or receipt<>0 or salesfoc<>0 or recieptfoc<>0)";
					SqlDtr=obj.GetRecordSet(str);
					if(SqlDtr.Read())
					{
						Count=int.Parse(SqlDtr.GetValue(0).ToString());
					}
					SqlDtr.Close();
					string prod_id="";
					str="select prod_id from products where Prod_Name='"+arrName[0].ToString()+"' and Pack_Type='"+arrName[1].ToString()+"'";
					SqlDtr=obj.GetRecordSet(str);
					if(SqlDtr.Read())
					{
						prod_id=SqlDtr.GetValue(0).ToString();
					}
					SqlDtr.Close();

					if(Count == 0)
					{
						SqlConnection Con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
						Con.Open();
						SqlCommand cmd = new SqlCommand("delete from Products where Prod_Name='"+arrName[0].ToString()+"' and Pack_Type='"+arrName[1].ToString()+"'",Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();

						/* **********Add by vikas 08.06.09 ***************************/
						cmd = new SqlCommand("delete from Batchno where Prod_id="+prod_id,Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();

						cmd = new SqlCommand("delete from Batch_Transaction where Prod_id="+prod_id,Con);
						cmd.ExecuteNonQuery();
						cmd.Dispose();

						cmd = new SqlCommand("delete from Stockmaster_Batch where Productid="+prod_id,Con);
						cmd.ExecuteNonQuery();
						
						/* *************end************************/

						Con.Close();
						cmd.Dispose();
						MessageBox.Show("Product Deleted");
						Clear();
						CreateLogFiles.ErrorLog("Form:Product_Entry.aspx,Method:btnDelete_Click - Record Deleted, user : "+pass);
						GetProdName();
						dropProdID.Visible=false;
						dropProdID.SelectedIndex=0;
						btnEdit.Visible=true;
						lblProdID.Visible=true;
						GetNextProductID();
					}
					else
					{
						MessageBox.Show("Can Not Delete These Product because Some Transaction are available");
						Clear();
						return;
					}
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Product_Entry  Method Page_Load(btnDelete_Click) "+ " EXCEPTION  "+ex.Message+" userid is   "+pass);
				}
			}
			if (!IsPostBack)
			{
				try
				{
					checkPrevileges();

					InventoryClass obj=new InventoryClass (); 
				
					lb.Visible=false;
			
					txtunit.Enabled = false;
					txtBox.Enabled = false;

					GetNextProductID();
					FCategory();
			
					PType();

					fUnit();
				}
				catch(Exception ex)
				{
                   CreateLogFiles.ErrorLog("Form:Product_Entry  Method Page_Load "+ " EXCEPTION  "+ex.Message+" userid is   "+pass);
				}
			}
		}

		/// <summary>
		/// Check User Previleges.
		/// </summary>
		public void checkPrevileges()
		{
			#region Check Privileges
			int i;
			string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
			string Module="4";
			string SubModule="1";
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
			if(View_flag == "0")
			{
				Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				return;
			}
			
            if(Add_Flag == "0")
				   btnSave.Enabled = false;
			if(Edit_Flag == "0")
				btnEdit.Enabled = false; 
			if(Del_Flag == "0")
				btnDelete.Enabled = false; 

			#endregion
		}

		/// <summary>
		/// this is used to fetch category.
		/// </summary>
		public void FCategory()
		{
			if(!Page.IsPostBack)
			{
				InventoryClass obj=new InventoryClass (); 
				SqlDataReader SqlDtr;
				string sql;
				#region Fetch Categories
				sql="select distinct Category from Products where Category !='Fuel'order by  Category asc";
				SqlDtr = obj.GetRecordSet(sql); 
				while(SqlDtr.Read())
				{
					DropCategory.Items.Add(SqlDtr.GetValue(0).ToString ());
				}
				SqlDtr.Close ();
				#endregion
			}

		}

		/// <summary>
		/// this is used to fetch packtype.
		/// </summary>
		public void PType()
		{
			if(!Page.IsPostBack)
			{
				InventoryClass obj=new InventoryClass (); 
				SqlDataReader SqlDtr;
				string sql;
				#region Fetch Package Types
				sql="select distinct Pack_Type from Products where Category!='Fuel'";
				SqlDtr = obj.GetRecordSet(sql); 
				while(SqlDtr.Read())
				{
					if(!SqlDtr.GetValue(0).ToString().Trim().Equals("Loose Oil")) 
					DropPackage.Items.Add(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				#endregion
			}
		
		}
		
		/// <summary>
		/// this is used to fetch unit.
		/// </summary>
		public void fUnit()
		{
			if(!Page.IsPostBack)
			{
				InventoryClass obj=new InventoryClass (); 
				SqlDataReader SqlDtr;
				string sql;
				#region Fetch Units
				sql="select distinct Unit from Products";
				SqlDtr = obj.GetRecordSet(sql); 
				while(SqlDtr.Read())
				{
					if(DropUnit.Items.IndexOf(DropUnit.Items.FindByValue(SqlDtr.GetValue(0).ToString())) == -1)    
					DropUnit.Items.Add(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close ();
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


		/// <summary>
		/// this is used to print report & contact with printserver.
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

					Console.WriteLine("Socket connected to {0}",
						sender1.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					string home_drive = Environment.SystemDirectory;
					home_drive = home_drive.Substring(0,2); 
					byte[] msg = Encoding.ASCII.GetBytes(home_drive+"\\Inetpub\\wwwroot\\Servosms\\Sysitem\\ServosmsPrintServices\\ReportView\\ProductEntryReport.txt<EOF>");

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
				
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method :Print "+ " EXCEPTION  "+ane.Message+" userid is   "+pass);
				} 
				catch (SocketException se) 
				{
					Console.WriteLine("SocketException : {0}",se.ToString());
				
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method :Print "+ " EXCEPTION  "+se.Message+" userid is   "+pass);
				
				} 
				catch (Exception es) 
				{
					Console.WriteLine("Unexpected exception : {0}", es.ToString());
				
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method :Print "+ " EXCEPTION  "+es.Message+" userid is   "+pass);
				}

			} 
			catch (Exception es) 
			{
				Console.WriteLine( es.ToString());
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method btnSave_Click "+" userid is   "+pass);
			}
		}

		/// <summary>
		/// Its checks the before save that the account period is inserted in organisaton table or not.
		/// </summary>
		/// <returns></returns>
		public bool checkAcc_Period()
		{
			SqlDataReader SqlDtr = null;
			int c = 0;
			dbobj.SelectQuery("Select count(Acc_Date_From) from Organisation",ref SqlDtr);
			if(SqlDtr.Read())
			{
				c = System.Convert.ToInt32(SqlDtr.GetValue(0).ToString());  
			}
			SqlDtr.Close();

			if(c > 0)
				return true;
			else
				return false;

		}

		/// <summary>
		/// This method is used to save the product details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(!checkAcc_Period())
				{
					MessageBox.Show("Please enter the Accounts Period from Organization Details");
					return;
				}
				InventoryClass obj=new InventoryClass ();
				lb.Visible=false;
				string sql;
				if(dropProdID.Visible==true && dropProdID.SelectedIndex==0)
				{
					MessageBox.Show("Please Select the Product ID");
					return;
				}
				DropUnit.Enabled = true;
				if(DropPackage.SelectedIndex ==1)
					DropUnit.SelectedIndex = 4;

				if(txtProdName.Text=="" || (DropCategory.SelectedIndex==0 && txtCategory.Text=="") || (DropPackage.SelectedIndex==0 && txtPack1.Text=="" && txtPack2.Text=="" ) || (DropUnit.SelectedIndex==0 && txtunit.Text=="")|| DropStorein.SelectedIndex==0|| DropPackUnit.SelectedIndex==0)
				{
					MessageBox.Show("Fields Marked as * are Mandatory");
					return;
				}
               
				SqlDataReader SqlDr;
              
				string sql1; 
				
				string pname=StringUtil.OnlyFirstCharUpper(txtProdName.Text.ToString().Trim()); 
				
				if(DropPackage.SelectedIndex != 0  )
					sql1="select Prod_ID from Products where Prod_Name='"+pname+"' and Pack_Type='"+DropPackage.SelectedItem.Value.Trim()+"'";// and store_in = '"+store+"'";
				else
					sql1="select Prod_ID from Products where Prod_Name='"+pname+"' and Pack_Type='"+txtPack1.Text.ToString()+"X"+txtPack2.Text.ToString()+"'";// and store_in = '"+store+"'";
				SqlDr=obj.GetRecordSet(sql1);
				if(SqlDr.HasRows)
				{
					while(SqlDr.Read())
					{
						string prodid ="";
						if(lblProdID.Visible == true)
							prodid = lblProdID.Text.ToString();
						else
						{
							string[] arr1=dropProdID.SelectedItem.Text.Split(new char[]{':'},dropProdID.SelectedItem.Text.Length);  
							sql="select Prod_ID from Products where Prod_Name='"+arr1[0]+"' and Pack_Type='"+arr1[1]+"'";
							string ID1="";
							dbobj.SelectQuery(sql,"Prod_ID",ref ID1);
							prodid =ID1;
						}
						
						if(!prodid.Equals(SqlDr["Prod_ID"].ToString()))
						{
							if(DropPackage.SelectedIndex != 0  )
								MessageBox.Show("Product Name "+pname+"  with Packege "+DropPackage.SelectedItem.Value.Trim()+" Already Exist");
							else
								MessageBox.Show("Product Name "+pname+"  with Packege "+txtPack1.Text.ToString()+"X"+txtPack2.Text.ToString()+" Already Exist");
							return;
						}
						
					}
				}
				SqlDr.Close();
				if(lblProdID.Visible==true)
					dbobj.SelectQuery("select Prod_Code from Products order by Prod_Code",ref SqlDr);
				else
				{
					string[] arr1=dropProdID.SelectedItem.Text.Split(new char[]{':'},dropProdID.SelectedItem.Text.Length);
					dbobj.SelectQuery("select Prod_Code from Products where Prod_Name!='"+arr1[0].ToString()+"' or pack_type!='"+arr1[1].ToString()+"' order by Prod_Code",ref SqlDr);
				}
				if(SqlDr.HasRows)
				{
					while(SqlDr.Read())
					{
						if(txtProdCode.Text==SqlDr["Prod_Code"].ToString())
						{
							MessageBox.Show("Product Code Already Exist");
							return;
						}
					}
				}
				SqlDr.Close();		 
				/*bhal1*/
				//*bhal1***	obj.Product_Name =StringUtil.FirstCharUpper(txtProdName.Text.ToString());  
			
				obj.Product_Name  = txtProdName.Text.ToString().Trim(); 
				/*bhal*/			
				if(DropCategory.SelectedIndex!=0)
				{
					obj.Category=DropCategory.SelectedItem.Value.Trim();
				}
				else
				{
					obj.Category=StringUtil.FirstCharUpper( txtCategory.Text.ToString().Trim());
					
				}
				if(DropPackage.SelectedIndex !=0)
					obj.Package_Type=DropPackage.SelectedItem.Value.Trim();			
				else
					obj.Package_Type=txtPack1.Text.ToString().Trim()+"X"+txtPack2.Text.ToString().Trim();
				obj.Package_Unit =DropPackUnit.SelectedItem.Value.ToString().Trim();   

				obj.Total_Qty = txtTotalQty.Text.ToString();
				if(DropPackage.SelectedIndex == 1)
				{
					if(txtBox.Text!="")
						obj.Opening_Stock = txtBox.Text.ToString().Trim(); 
					else
						obj.Opening_Stock = "0";
				}
				else  
				{
					if(txtOp_Stock.Text!="")
						obj.Opening_Stock = txtOp_Stock.Text.ToString().Trim();
					else
						obj.Opening_Stock = "0";
				}
				if(!DropUnit.SelectedItem.Text.Equals("Other"))
				{
					obj.Unit=DropUnit.SelectedItem.Value.Trim();
				}
				else
				{
					if(txtunit.Text.Equals(""))
					{
						MessageBox.Show("Please Enter the Unit");
						return;
					}
					else
						obj.Unit=StringUtil.FirstCharUpper(txtunit.Text.ToString());
				}
				obj.Store_In=DropStorein.SelectedItem.Value.Trim();
				obj.Prod_Code=txtProdCode.Text.Trim();
				obj.MinLabel=txtMinLabel.Text.Trim();
				obj.MaxLabel=txtMaxLabel.Text.Trim();
				obj.ReOrderLabel=txtReOrderLabel.Text.Trim();
				//obj.BatchNo=txtBatchNo.Text;
				
				//coment by vikas 08.06.09 obj.BatchNo="";
				/**************Add by vikas 08.06.09 *******************/
				if(Yes.Checked==true)
					obj.BatchNo="1";
				else
					obj.BatchNo="0";
				/***************End*************************************/
				obj.MRP=txtMRP.Text.Trim();
				// Check if the label is visible then save the products, & if Drop down of products id is visible then Update the product details.		
				if(lblProdID.Visible==true)
				{
					obj.Prod_ID = lblProdID.Text.ToString();	
					obj.InsertProducts ();
					
					FCategory();
					PType();
					fUnit();
					// the code below adds the category, package and unit into the combo boxes if not present.
					if (txtCategory.Text!="")
					{
						if(DropCategory.Items.IndexOf(DropCategory.Items.FindByValue(StringUtil.FirstCharUpper(txtCategory.Text))) == -1)    
							DropCategory.Items.Add(StringUtil.FirstCharUpper(txtCategory.Text.ToString().Trim()));
					}
					if((txtPack1.Text!="") && (txtPack2.Text!=""))
					{
						if(DropPackage.SelectedIndex != 1)                           
						{
							string temp = txtPack1.Text.Trim() + "X" + txtPack2.Text.Trim();
							if(DropPackage.Items.IndexOf(DropPackage.Items.FindByValue(temp.Trim() )) == -1)     
								DropPackage.Items.Add(txtPack1.Text + "X" + txtPack2.Text); 	
						}
					}
					if(txtunit.Text!="")
					{
						if(DropUnit.Items.IndexOf(DropUnit.Items.FindByValue(StringUtil.FirstCharUpper(txtunit.Text.Trim()))) == -1)    
							DropUnit.Items.Add(StringUtil.FirstCharUpper(txtunit.Text.Trim()));                     	
					}

					/********Add by vikas 03.07.09*********************/
					if(Yes.Checked==true)    
						GetBatch();
					/*********End********************/
					//03.07.09 by vikas  GetBatch();

					MessageBox.Show("Product Saved");
					CreateLogFiles.ErrorLog("Form:Product_Entry  Method btnSave_Click.:   Product "+txtProdName.Text.ToString()+" Saved.     userid is   "+pass);
				}
				else
				{
					string[] arr=dropProdID.SelectedItem.Text.Split(new char[]{':'},dropProdID.SelectedItem.Text.Length);  
					sql="select Prod_ID from Products where Prod_Name='"+arr[0]+"' and Pack_Type='"+arr[1]+"'";
					string ID="";
					dbobj.SelectQuery(sql,"Prod_ID",ref ID);
					obj.Prod_ID =ID;
					obj.UpdateProducts();	
					if (txtCategory.Text!="")
					{
						if(DropCategory.Items.IndexOf(DropCategory.Items.FindByValue(StringUtil.FirstCharUpper(txtCategory.Text.Trim()))) == -1)    
							DropCategory.Items.Add(StringUtil.FirstCharUpper(txtCategory.Text.ToString().Trim()));
					}
					if((txtPack1.Text.Trim()!="") && (txtPack2.Text.Trim()!=""))
					{
						if(DropPackage.SelectedIndex != 1)                           
						{
							string temp = txtPack1.Text.Trim() + "X" + txtPack2.Text.Trim() ;
							if(DropPackage.Items.IndexOf(DropPackage.Items.FindByValue(temp.Trim() )) == -1)     
								DropPackage.Items.Add(txtPack1.Text.Trim() + "X" + txtPack2.Text.Trim()); 	
						}
					}
					if(txtunit.Text!="")
					{
						if(DropUnit.Items.IndexOf(DropUnit.Items.FindByValue(StringUtil.FirstCharUpper(txtunit.Text.Trim()))) == -1)
							DropUnit.Items.Add(StringUtil.FirstCharUpper(txtunit.Text.Trim()));
					}
					/********Add by vikas 03.07.09*********************/
					if(Yes.Checked==true)    
						GetBatch();
					/*********End********************/
					//03.07.09 by vikas  GetBatch();

					MessageBox.Show("Product Updated");
					CreateLogFiles.ErrorLog("Form:Product_Entry  Method btnSave_Click.:   Product "+dropProdID.SelectedItem.Text.Trim() +" updated.     userid is   "+pass);
				}
				Clear();
				GetNextProductID();
				lblProdID.Visible=true;
				btnEdit.Visible=true;
				dropProdID.Visible=false;
				checkPrevileges();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method btnSave_Click "+"  EXCEPTION  "+  ex.Message+" userid is   "+pass);
			}
		}

		/// <summary>
		/// This method is used to save the product with batch wise information in other table.
		/// </summary>
		public void GetBatch()
		{
			try
			{
				InventoryClass objprod = new InventoryClass();
				int x=0,batch_id=0,SNo=0,BID=0;
				SqlDataReader rdr = null;
				rdr = objprod.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
				if(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null)
						SNo=int.Parse(rdr.GetValue(0).ToString());
					else
						SNo=1;
				}
				else
					SNo=1;
				rdr.Close();
				if(Request.Params.Get("BatchNo")=="Yes" && batch.Value!="")
				{
					DateTime dt;
					dbobj.SelectQuery("select acc_date_from from organisation",ref rdr);
					if(rdr.Read())
					{
						dt=System.Convert.ToDateTime(rdr.GetValue(0).ToString());
					}
					else
					{
						MessageBox.Show("First Fill The Organisation Form");
						return;
					}
					string op="0";
					rdr = objprod.GetRecordSet("select max(Batch_ID) from BatchNo");
					if(rdr.Read())
					{
						if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null)
							batch_id=int.Parse(rdr.GetValue(0).ToString());
						else
							batch_id=0;
					}
					else
						batch_id=0;
					rdr.Close();
				
					if(txtOp_Stock.Text!="")
						op=txtOp_Stock.Text;
					int tot_bat_qty=0;								//add by vikas 11.06.09
					if(lblProdID.Visible==true)
					{
						string[] arr=batch.Value.Split(new char[] {','},batch.Value.Length);
						for(int n=0;n<arr.Length;n+=3)
						{
							if(!arr[n].ToString().Equals("''"))
							{
								string BNo=arr[n].ToString();
								string BQty=arr[n+2].ToString();
								//Coment by vikas 18.09.09 dbobj.Insert_or_Update("insert into BatchNo values('"+(++batch_id)+"',"+BNo+",'"+lblProdID.Text+"','"+dt+"',"+BQty+","+lblProdID.Text+")",ref x);
								dbobj.Insert_or_Update("insert into BatchNo values('"+(++batch_id)+"',"+BNo+",'"+lblProdID.Text+"','"+dt+"',"+BQty+")",ref x);
								dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+lblProdID.Text+"','Opening Stock','"+dt+"','"+lblProdID.Text+"',"+batch_id+","+BQty+","+BQty+")",ref x);
								dbobj.Insert_or_Update("insert into StockMaster_Batch values("+lblProdID.Text+",'"+batch_id+"','"+dt+"',"+BQty+",0,0,"+BQty+",0,0)",ref x);
							}
							else
							{
								
								if(arr[n+2].ToString()!="''")
								{
									dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+lblProdID.Text+"','Opening Stock','"+dt+"','"+lblProdID.Text+"','',"+arr[n+2].ToString()+","+arr[n+2].ToString()+")",ref x);
									dbobj.Insert_or_Update("insert into StockMaster_Batch values("+lblProdID.Text+",'','"+dt+"',"+arr[n+2].ToString()+",0,0,"+arr[n+2].ToString()+",0,0)",ref x);
								}
								break;
							}
						}
					}
					else
					{
						dbobj.SelectQuery("select batch_id from Batch_Transaction where prod_id='"+Prod_ID+"' and trans_id='"+Prod_ID+"'",ref rdr);
						string[] bat=new string[10];
						int q=0;
						while(rdr.Read())
						{
							bat[q]=rdr.GetValue(0).ToString();
							q++;
						}
						rdr.Close();

						dbobj.Insert_or_Update("delete from Batch_Transaction where trans_ID='"+Prod_ID+"' and prod_id='"+Prod_ID+"' and trans_type='Opening Stock'",ref x);
						rdr = objprod.GetRecordSet("select max(SNo)+1 from Batch_Transaction");
						if(rdr.Read())
						{
							if(rdr.GetValue(0).ToString()!="" && rdr.GetValue(0).ToString()!=null)
								SNo=int.Parse(rdr.GetValue(0).ToString());
							else
								SNo=1;
						}
						else
							SNo=1;
						rdr.Close();
						//18.09.09 vikas rdr=objprod.GetRecordSet("select * from BatchNo where Trans_No='"+Prod_ID+"' order by Batch_ID");
						rdr=objprod.GetRecordSet("select * from BatchNo where Prod_Id='"+Prod_ID+"' order by Batch_ID");
						if(rdr.Read())
						{
							BID=int.Parse(rdr["Batch_ID"].ToString());
						}
						rdr.Close();
						string[] arr=batch.Value.Split(new char[] {','},batch.Value.Length);
						for(int n=0,p=0;n<arr.Length;n+=3,p++)
						{
							if(!arr[n].ToString().Equals("''"))
							{
								string BNo=arr[n].ToString();
								string BQty=arr[n+2].ToString();
								//18.09.09 vikas rdr = objprod.GetRecordSet("select * from BatchNo where Trans_No='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"");
								rdr = objprod.GetRecordSet("select * from BatchNo where Prod_id='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"");
								if(rdr.HasRows)
								{
									//18.09.09 vikas dbobj.Insert_or_Update("update BatchNo set Batch_No="+BNo.ToString()+",Prod_ID='"+Prod_ID+"',Date='"+dt+"',Qty="+BQty.ToString()+" where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);
									dbobj.Insert_or_Update("update BatchNo set Batch_No="+BNo.ToString()+",Date='"+dt+"',Qty="+BQty.ToString()+" where Prod_ID='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);
									dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+arr[n+1].ToString()+","+BQty+","+BQty+")",ref x);
									dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='"+dt+"',opening_stock="+BQty+",closing_stock="+BQty+" where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
								}
								else
								{
									//18.09.09 vikas dbobj.Insert_or_Update("insert into BatchNo values("+(++batch_id)+","+BNo.ToString()+",'"+Prod_ID+"','"+dt+"',"+BQty.ToString()+",'"+Prod_ID+"')",ref x);
									dbobj.Insert_or_Update("insert into BatchNo values("+(++batch_id)+","+BNo.ToString()+",'"+Prod_ID+"','"+dt+"',"+BQty.ToString()+")",ref x);
									dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',"+batch_id+","+BQty+","+BQty+")",ref x);
									dbobj.Insert_or_Update("insert into StockMaster_Batch values("+Prod_ID+",'"+batch_id+"','"+dt+"',"+BQty+",0,0,"+BQty+",0,0)",ref x);
								}
								rdr.Close();
							}
							/****************This Condition Add by vikas date on 16.09.09 For insert Without Batch In to Batch Tranction and StockMaster_Batch Table****************************************/
							else if(arr[n+2].ToString()!="''")
							{

								dbobj.Insert_or_Update("insert into Batch_Transaction values("+(SNo++)+",'"+Prod_ID+"','Opening Stock','"+dt+"','"+Prod_ID+"',0,"+arr[n+2].ToString()+","+arr[n+2].ToString()+")",ref x);

								dbobj.SelectQuery("select * from StockMaster_Batch where productid='"+Prod_ID+"' and Batch_id=0",ref rdr);
								if(rdr.HasRows)
								{
									dbobj.Insert_or_Update("update StockMaster_Batch set stock_date='"+dt+"',opening_stock="+arr[n+2].ToString()+",closing_stock="+arr[n+2].ToString()+" where productid='"+Prod_ID+"' and batch_id=0",ref x);
								}
								else
								{
									dbobj.Insert_or_Update("insert into StockMaster_Batch values("+Prod_ID+",'0','"+dt+"',"+arr[n+2].ToString()+",0,0,"+arr[n+2].ToString()+",0,0)",ref x);
								}
								
								if((arr[n+1].ToString()!="''"))
								{
									if((arr[n+3].ToString()!="''"))              //Add by vikas sharma 10.11.09 becouse stockmaster_batch delete record where batchid=0
									{
										//18.09.09 vikas dbobj.Insert_or_Update("delete from BatchNo where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);                            // Coment by vikas 12.06.09
										dbobj.Insert_or_Update("delete from BatchNo where Prod_id='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);                            
										dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);                 // Coment by vikas 12.06.09
									}
								}
							}	
							/******************End**************************************/
							else
							{
								//18.09.09 rdr = objprod.GetRecordSet("select * from Batch_Transaction where Prod_id='"+Prod_ID+"' and Batch_ID=0");
								rdr = objprod.GetRecordSet("select * from Batch_Transaction where Prod_id='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"");
								if(!rdr.HasRows)
								{
									dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);
									//16.09.09 Coment by vikas dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id=0 and opening_Stock=0 and receipt=0 and sales=0 and closing_stock=0",ref x);

									if((arr[n+1].ToString()!="''"))
									{
										//18.09.09 vikas dbobj.Insert_or_Update("delete from BatchNo where Trans_no='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);                            // Coment by vikas 12.06.09
										dbobj.Insert_or_Update("delete from BatchNo where Prod_id='"+Prod_ID+"' and Batch_ID="+arr[n+1].ToString()+"",ref x);                            
										dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+arr[n+1].ToString()+"",ref x);                 // Coment by vikas 12.06.09
									}
								}
								rdr.Close();
							}
						}
					}
				}
				else  
				{
					dbobj.SelectQuery("select batch_id from Batch_Transaction where prod_id='"+Prod_ID+"' and trans_id='"+Prod_ID+"'",ref rdr);
					string[] bat=new string[10];
					int q=0;
					while(rdr.Read())
					{
						bat[q]=rdr.GetValue(0).ToString();
						q++;
					}
					rdr.Close();
					if(Prod_ID!="")
					{
						dbobj.Insert_or_Update("delete from Batch_Transaction where prod_id='"+Prod_ID+"' and Trans_id="+Prod_ID+"",ref x);					// Coment by vikas 12.06.09
						//18.09.09 vikas dbobj.Insert_or_Update("delete from Batchno where prod_id='"+Prod_ID+"' and Trans_No="+Prod_ID+"",ref x);								// Coment by vikas 12.06.09
						dbobj.Insert_or_Update("delete from Batchno where prod_id='"+Prod_ID+"'" ,ref x);								
					}
					for(int i=0;i<q;i++)
					{
						dbobj.Insert_or_Update("delete from StockMaster_Batch where productid='"+Prod_ID+"' and batch_id="+bat[i],ref x);						// Coment by vikas 12.06.09
					}
				}
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method GetBatch() "+"  EXCEPTION  "+  ex.Message+" userid is   "+pass);
			}
		}
		
		private string GetString(string str,string spc)
		{
			if(str.Length>spc.Length)
				return str;
			else
				return str+spc.Substring(0,spc.Length-str.Length)+"  ";			
		}
				
		private void getMaxLen(System.Data.SqlClient.SqlDataReader rdr,ref int len1,ref int len2,ref int len3,ref int len4,ref int len5,ref int len6,ref int len7,ref int len8,ref int len9)
		{
		
			while(rdr.Read())
			{
				if(rdr["Prod_ID"].ToString().Trim().Length>len1)
					len1=rdr["Prod_ID"].ToString().Trim().Length;					
				if(rdr["Prod_Name"].ToString().Trim().Length>len2)
					len2=rdr["Prod_Name"].ToString().Trim().Length;					
				if(rdr["Category"].ToString().Trim().Length>len3)
					len3=rdr["Category"].ToString().Trim().Length;
				if(rdr["Pack_Type"].ToString().Trim().Length>len4)
					len4=rdr["Pack_Type"].ToString().Trim().Length;	
				if(rdr["Pack_Unit"].ToString().Trim().Length>len5)
					len5=rdr["Pack_Unit"].ToString().Trim().Length;					
				if(rdr["Total_Qty"].ToString().Trim().Length>len6)
					len6=rdr["Total_Qty"].ToString().Trim().Length;	
				if(rdr["Opening_Stock"].ToString().Trim().Length>len7)
					len7=rdr["Opening_Stock"].ToString().Trim().Length;	
				if(rdr["Unit"].ToString().Trim().Length>len8)
					len8=rdr["Unit"].ToString().Trim().Length;	
				if(rdr["Store_In"].ToString().Trim().Length>len9)
					len9=rdr["Store_In"].ToString().Trim().Length;	
			}
		}
		
		private string GetString(string str,int maxlen,string spc)
		{		
			return str+spc.Substring(0,maxlen>str.Length?maxlen-str.Length:str.Length-maxlen);
		}
		private string MakeString(int len)
		{
			string spc="";
			for(int x=0;x<len;x++)
				spc+=" ";
			return spc;
		}
		
		/// <summary>
		/// this is used to make the report for printing.
		/// </summary>
		public void reportmaking()
		{
			/*
														 ========================                                                
														   PRODUCT ENTRY REPORT                                                  
														 ========================                                                
			+--------+---------------------+----------------+---------+----+---------+--------+----------+------------+
			|Prod ID |    Product Name     |    Category    |Pack Type|Pack|Quantity |Opening |   Unit   | Store In   |
			|        |                     |                |         |Unit|         | Stock  |          |            |
			+--------+---------------------+----------------+---------+----+---------+--------+----------+------------+
			 1003      Servo pride           Engine oil      100x100   1234  12345678 12345678  Packet     Sales Room

			*/			string home_drive = Environment.SystemDirectory;
			home_drive = home_drive.Substring(0,2); 
			string path = home_drive+@"\Inetpub\wwwroot\Servosms\Sysitem\ServosmsPrintServices\ReportView\ProductEntryReport.txt";

			StreamWriter sw = new StreamWriter(path);
			string info="";
			System.Data.SqlClient.SqlDataReader rdr=null;
			InventoryClass obj=new InventoryClass (); 
			SqlDataReader SqlDtr;
			string sql="";
			string store="";
			string packType = "";
			         			
			sql="select Prod_ID, Prod_Name, Category, Pack_Type, Pack_Unit, Total_Qty, Opening_Stock, Unit, Store_In from  Products";
			sw.WriteLine("                                             ======================== ");
			sw.WriteLine("                                               PRODUCT ENTRY REPORT ");
			sw.WriteLine("                                             ======================== ");
			sw.WriteLine("+--------+---------------------+----------------+---------+----+---------+--------+----------+------------+");
			sw.WriteLine("|Prod ID |    Product Name     |    Category    |Pack Type|Pack|Quantity |Opening |   Unit   | Store In   |");
			sw.WriteLine("|        |                     |                |         |Unit|         | Stock  |          |            |");
			sw.WriteLine("+--------+---------------------+----------------+---------+----+---------+--------+----------+------------+");
			//             1003      Servo pride           Engine oil      100x100   1234  12345678 12345678 Packet     Sales Room  
			info=" {0,-4:S}      {1,-20:S}  {2,-15:S} {3,-9:S} {4,-4:S}  {5,8:F} {6,8:F} {7,-10:S} {8,-12:S}";
			dbobj.SelectQuery(sql,ref rdr);

			if(rdr.HasRows)
			{
				while(rdr.Read())
				{
					store = rdr["Store_In"].ToString();
					if(rdr["Category"].ToString().Trim().Equals("Fuel")) 
					{
						
						SqlDtr = obj.GetRecordSet("select Prod_AbbName from Tank where Tank_ID = '"+store+"'");
						if(SqlDtr.Read())
							store = SqlDtr.GetValue(0).ToString();
						else
							store = "";
						SqlDtr.Close(); 
					}	
					packType = rdr["Pack_Type"].ToString();
					if(packType.Trim().IndexOf("Loose")!= -1)
						packType="";
				
					sw.WriteLine(info,rdr["Prod_ID"].ToString(),
						rdr["Prod_Name"].ToString(),
						rdr["Category"].ToString(),
						packType ,
						rdr["Pack_Unit"].ToString(),
						rdr["Total_Qty"].ToString(),
						rdr["Opening_Stock"].ToString(),
						rdr["Unit"].ToString(),
						store);	
				}
			}
			sw.WriteLine("+--------+---------------------+----------------+---------+----+---------+--------+----------+------------+");
			rdr.Close();
			sw.Close(); 
		}

		protected void DropPackage_SelectedIndexChanged(object sender, System.EventArgs e)
		{

		}

		/// <summary>
		/// Its clears the form.
		/// </summary>
		public void Clear()
		{

			Yes.Checked=false;  //Coment by vikas 08.06.09
			No.Checked=false;	//Coment by vikas 08.06.09
			
			lblProdID.Text="";
			dropProdID.SelectedIndex=0;
			DropCategory.SelectedIndex=0;
			DropPackage.SelectedIndex=0;
			DropPackUnit.SelectedIndex=0;
			DropStorein.SelectedIndex=0;
			DropUnit.SelectedIndex=0;
			txtProdName.Text="";
			txtCategory.Text=""; 
			txtPack1.Text="";
			txtPack2.Text="";
			txtOp_Stock.Text="";
			txtTotalQty.Text="";
			txtBox.Text=""; 
			txtunit.Text=""; 
			txtPack1.Enabled=true;
			txtPack2.Enabled=true;
			txtCategory.Enabled=true;
			txtProdCode.Text="";
			txtMinLabel.Text="";
			txtReOrderLabel.Text="";
			txtMaxLabel.Text="";
			tempDelinfo.Value="";
			//txtBatchNo.Text="";
			txtMRP.Text="";
			Prod_ID = "";
		}

		/// <summary>
		/// this is used to generate the nextID auto.
		/// </summary>
		public void GetNextProductID()
		{
			InventoryClass obj=new InventoryClass (); 
			SqlDataReader SqlDtr;
			string sql;

			#region Fetch Next Product ID
			sql="select Max(Prod_ID)+1 from Products";
			SqlDtr = obj.GetRecordSet(sql); 
			while(SqlDtr.Read())
			{
				lblProdID.Text =SqlDtr.GetValue(0).ToString();
				if(lblProdID.Text=="")
				{
					lblProdID.Text ="1001";
				}
			}
			SqlDtr.Close (); 
			#endregion
		}

		/// <summary>
		/// This method is used to fill the all product name in drpdownlist.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				lb.Visible=true;
				lblProdID.Visible=false;
				btnEdit.Visible=false;
				dropProdID.Visible=true;
				btnSave.Enabled = true;  
				GetProdName();				
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Product_Entry  Method: btnEdit_Click.  EXCEPTION  "+  ex.Message+".  User_id is   "+pass);
			}
		}
		
		/// <summary>
		/// This method is used to fatch the all product name with pack type in edit time.
		/// </summary>
		public void GetProdName()
		{
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string sql;
			#region Get All Products ID and fill in the ComboBox
			//*bhal**sql="select Prod_name+':'+Pack_Type from Products where Category!='Fuel' order by Prod_name ";
			sql="select Prod_name+':'+Pack_Type from Products where Category!='Fuel' order by Prod_name";
			SqlDtr=obj.GetRecordSet(sql);
			dropProdID.Items.Clear();
			dropProdID.Items.Add("Select");
			while(SqlDtr.Read())
			{
				dropProdID.Items.Add(SqlDtr.GetValue(0).ToString()); 
			}
			SqlDtr.Close();
			#endregion
		}

		static string Prod_ID="";
		/// <summary>
		/// This method is used to fatch the particular product information according to select product from dropdown list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropProdID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(dropProdID.SelectedIndex==0)
					return;

				InventoryClass obj=new InventoryClass();
				SqlDataReader SqlDtr;
				string sql;
				string[] arr=dropProdID.SelectedItem.Text.Split(new char[]{':'},dropProdID.SelectedItem.Text.Length);  

				#region Get All Products ID and fill in the ComboBox 
				//			sql="select * from Products where Category!='Fuel' and Prod_ID='"+ dropProdID.SelectedItem.Value +"'";
		//*bhal**		sql="select * from Products where Category!='Fuel' and Prod_Name='"+ arr[0] +"' and Pack_Type='"+arr[1]+"'";
		/*bhal*/		sql="select * from Products where Category!='Fuel' and Prod_Name='"+ arr[0] +"' and Pack_Type='"+arr[1]+"'";
				SqlDtr=obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					Prod_ID=SqlDtr["Prod_ID"].ToString();
					txtProdName.Text=SqlDtr.GetValue(1).ToString();  
					DropCategory.SelectedIndex=DropCategory.Items.IndexOf(DropCategory.Items.FindByValue(SqlDtr.GetValue(2).ToString ()));
					DropPackage.SelectedIndex= DropPackage.Items.IndexOf(DropPackage.Items.FindByValue(SqlDtr.GetValue(3).ToString ()));
					if(DropPackage.SelectedIndex != 1)
					{
						DropUnit.Enabled = true;
						txtOp_Stock.Enabled  = true;
						txtBox.Enabled = false;
						txtBox.Text = "";
						txtOp_Stock.Text ="";
						DropPackUnit.SelectedIndex= DropPackUnit.Items.IndexOf(DropPackUnit.Items.FindByValue(SqlDtr.GetValue(4).ToString ()));
						txtTotalQty.Text=SqlDtr.GetValue(5).ToString();
						txtOp_Stock.Text=SqlDtr.GetValue(6).ToString();
						if(txtTotalQty.Text!="" && txtOp_Stock.Text!="")
						{
							txtBox.Text= System.Convert.ToString(System.Convert.ToDouble(txtTotalQty.Text) *  System.Convert.ToDouble(txtOp_Stock.Text));
						}
						DropUnit.SelectedIndex=DropUnit.Items.IndexOf(DropUnit.Items.FindByValue(SqlDtr.GetValue(7).ToString ()));
						DropStorein.SelectedIndex=DropStorein.Items.IndexOf(DropStorein.Items.FindByValue(SqlDtr.GetValue(8).ToString()));
						txtProdCode.Text=SqlDtr.GetValue(9).ToString().Trim();
						txtMinLabel.Text=SqlDtr.GetValue(10).ToString().Trim();
						txtMaxLabel.Text=SqlDtr.GetValue(11).ToString().Trim();
						txtReOrderLabel.Text=SqlDtr.GetValue(12).ToString().Trim();
						//txtBatchNo.Text=SqlDtr["batchno"].ToString();
						txtMRP.Text=SqlDtr["mrp"].ToString();

						/********Add by vikas 08.06.09**************************/
						if(SqlDtr["BatchNo"].ToString()=="1")
						{
							Yes.Checked=true;
							No.Checked=false;
						}
						else
						{
							No.Checked=true;
							Yes.Checked=false;
						}
						/***********end***********************/
					}
					else
					{
						txtBox.Text = "";
						txtOp_Stock.Text ="";
						DropPackUnit.SelectedIndex= DropPackUnit.Items.IndexOf(DropPackUnit.Items.FindByValue(SqlDtr.GetValue(4).ToString ()));
						txtTotalQty.Text=SqlDtr.GetValue(5).ToString();
						txtOp_Stock.Enabled = false;
						txtBox.Enabled = true;
						txtBox.Text = SqlDtr.GetValue(6).ToString();
						DropUnit.SelectedIndex=DropUnit.Items.IndexOf(DropUnit.Items.FindByValue(SqlDtr.GetValue(7).ToString ()));
						DropUnit.Enabled = false;
						txtunit.Text ="";
						DropStorein.SelectedIndex=DropStorein.Items.IndexOf(DropStorein.Items.FindByValue(SqlDtr.GetValue(8).ToString()));
						txtProdCode.Text=SqlDtr.GetValue(9).ToString().Trim();
						txtMinLabel.Text=SqlDtr.GetValue(10).ToString().Trim();
						txtMaxLabel.Text=SqlDtr.GetValue(11).ToString().Trim();
						txtReOrderLabel.Text=SqlDtr.GetValue(12).ToString().Trim();
						//txtBatchNo.Text=SqlDtr["batchno"].ToString();
						txtMRP.Text=SqlDtr["mrp"].ToString();
						/********Add by vikas 08.06.09**************************/
						if(SqlDtr["BatchNo"].ToString()=="1")
						{
							Yes.Checked=true;
							No.Checked=false;
						}
						else
						{
							No.Checked=true;
							Yes.Checked=false;
						}
						/***********end***********************/
					}
				}
				SqlDtr.Close();

				/*************Add By Vikas Sharma 08.06.09*************************************************
				int ICount=0;
				sql="select count(*) from BatchNo where Prod_Id="+Prod_ID ;
				SqlDtr=obj.GetRecordSet(sql);
				if(SqlDtr.Read())
				{
					ICount=Convert.ToInt32(SqlDtr.GetValue(0));
				}
				SqlDtr.Close();
				
				if(ICount>0)
					//Request.Params.Get("BatchNo")=="Yes"
					Request.Params.Set("BatchNo","Yes");
				else
					Request.Params.Set("BatchNo","No");

				**************************************************************/
				txtCategory.Enabled = false;
				txtPack1.Enabled = false;
				txtPack2.Enabled = false;
				#endregion
			}
			catch(Exception ex)
			{
               CreateLogFiles.ErrorLog("Form:Product_Entry  Method: dropProdID_SelectedIndexChanged.  EXCEPTION  "+  ex.Message+".  User_id is   "+pass);
			}
		}

		protected void txtPack1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void DropUnit_SelectedIndexChanged(object sender, System.EventArgs e)
		{ 
			
		}

		protected void DropCategory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void txtBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void btnDelete_Click(object sender, System.EventArgs e)
		{
			// MessageBox.Show("ConfirmBox.Show("Hello")");
			//MessageBox.Show("OK");
			
		}
	}
}
