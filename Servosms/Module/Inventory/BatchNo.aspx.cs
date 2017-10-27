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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RMG;
using Servosms.Sysitem.Classes;

namespace Servosms.Module.Inventory
{
	/// <summary>
	/// Summary description for UpdateModuleRole.
	/// </summary>
	public partial class UpdateModuleRole : System.Web.UI.Page
	{
		public SqlDataReader rdr=null;
		public SqlDataReader rdr1=null;
		//public static string[] arrBatch = new string[20];
		public static string[] arrBatch = new string[30];
		public static string[] str=null;
		public static int flage;
		string uid;
	
		/// <summary>
		/// This method is used for filling the required textboxes with database values 
		/// Fatching the data from batchno table according to given some value passing by url.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				uid=Session["User_Name"].ToString();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:BatchNo.aspx,Method:pageload"+ex.Message+"  EXCEPTION "+uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			int i=0;
			for(i=0;i<30;i++)
			{
				arrBatch[i]="";
			}
			
			string s=Request.Params.Get("chk");
			str= s.Split(new char[] {':'},s.Length);
			InventoryClass obj = new InventoryClass();
			InventoryClass obj1 = new InventoryClass(); //Add by vikas 17.09.09
			chkinfo.Value=str[0];
			if(str[0].ToString()=="Yes")
			{
				//rdr = obj.GetRecordSet("select * from Batchno where trans_No=(select Prod_ID from Products where Prod_Name='"+str[1].ToString()+"') and prod_id=(select prod_id from products where prod_name='"+str[1].ToString()+"')");
				//18.09.09 rdr = obj.GetRecordSet("select * from Batchno where trans_No=(select Prod_ID from Products where Prod_Name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"') and prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"')");         //Existing
				rdr = obj.GetRecordSet("select * from Batchno where prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"')");        
				rdr1 = obj1.GetRecordSet("select * from Batch_transaction where Batch_id=0 and trans_id=(select Prod_ID from Products where Prod_Name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"') and prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"')");         //Add by vikas 17.09.09
			}
			else
			{
				//rdr = obj.GetRecordSet("select * from Batchno where trans_No=(select Invoice_No from purchase_master where vndr_Invoice_No='"+str[4].ToString()+"') and prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"')");//Comment By Mahesh on 12.11.008
				//coment by vikas 16.06.09 rdr = obj.GetRecordSet("select * from Batchno where trans_No='"+str[4].ToString()+"' and prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"') order by batch_id");
				//coment by vikas 16.06.09 rdr = obj.GetRecordSet("select bn.batch_no,bn.batch_id,bt.qty from Batchno bn,batch_transaction bt where bt.trans_id='"+str[4].ToString()+"' and bt.batch_id!=0 and bn.prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"') and bt.trans_type ='Purchase Invoice' order by bn.batch_id");
				//18.09.09 rdr = obj.GetRecordSet("select bn.batch_no,bn.batch_id,bt.qty from Batchno bn,batch_transaction bt where bt.trans_id='"+str[4].ToString()+"' and bn.trans_no=bt.trans_id and bn.batch_id=bt.batch_id and bt.batch_id!=0 and bn.prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"') and bt.trans_type ='Purchase Invoice' order by bn.batch_id");
				rdr = obj.GetRecordSet("select bn.batch_no,bn.batch_id,bt.qty from Batchno bn,batch_transaction bt where bt.trans_id='"+str[4].ToString()+"' and bn.batch_id=bt.batch_id and bt.batch_id!=0 and bn.prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"') and bt.trans_type ='Purchase Invoice' order by bn.batch_id");
				rdr1 = obj1.GetRecordSet("select * from Batch_transaction where Batch_id=0 and trans_id='"+str[4].ToString()+"' and prod_id=(select prod_id from products where prod_name='"+str[2].ToString()+"' and pack_type='"+str[3].ToString()+"')");         //Add by vikas 17.09.09
			}
			i=0;
			string[] ss=new string[30];
			int flag=0;
			while(rdr.Read())
			{
				//Coment by vikas 16.09.09  ss[i]=rdr["Batch_No"].ToString();
				
				/*************Add by vikas ****************************/
				if(rdr["Batch_ID"].ToString()!="0")
				{
					ss[i]=rdr["Batch_No"].ToString();
				}
				else
				{
					ss[i]="";
				}
				/*****************************************/

				ss[++i]=rdr["Batch_ID"].ToString();													//add new batchid column
				if(rdr["qty"].ToString()!="0")
					ss[++i]=rdr["qty"].ToString();
				else
					ss[++i]="";
				i++;
				flag=1;
			}
			rdr.Close();
			/*********Add by vikas 17.09.09********************/
				while(rdr1.Read())
				{
					ss[i]="";
					ss[++i]=rdr1["Batch_ID"].ToString();													//add new batchid column
					if(rdr1["qty"].ToString()!="0")
						ss[++i]=rdr1["qty"].ToString();
					else
						ss[++i]="";
					i++;
					flag=1;
				}
				rdr1.Close();
			
			/*********End********************/
			for(int k=i;k<ss.Length;k++)
			{
				ss[k]="";
			}

			if(flag==1)
			{
				for(int j=0;j<ss.Length;j++)
				{
					arrBatch[j]=ss[j].ToString();
				}
			}
			btnBatch.Attributes.Add("OnClick","check();");
			Session["Batch"]=BatchNo.Value;
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

		protected void btnBatch_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
