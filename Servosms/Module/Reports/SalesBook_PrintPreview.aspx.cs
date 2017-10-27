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
using System.Data.SqlClient;

namespace Servosms.Module.Reports
{
	/// <summary>
	/// Summary description for SalesBook_PrintPreview.
	/// </summary>
	public partial class SalesBook_PrintPreview : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
					
			
		}
		//************
		public double totltr=0;
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
				totltr+=ans;
				Cache["totltr"]=totltr;
				return ans;
			}
			else
			{
				if(!mystr[0].Trim().Equals(""))
				{
					totltr+=System.Convert.ToDouble( mystr[0].ToString()); 
					Cache["totltr"]=totltr;
					return System.Convert.ToDouble( mystr[0].ToString()); 
				}
				else
				{
						Cache["totltr"]="0";
					return 0;
				}
			}
			
		}
		public double in_amt=0,am=0;
		//double amt=0;
		double amt1=0;//,amt2=0;
		int count=0,i=0,status=0,Flag=0;
		protected string Multiply1(string inv_no)
		{
			PetrolPumpClass  obj=new PetrolPumpClass();
			SqlDataReader SqlDtr;
			string sql;
			in_amt=0;
			
			if(Flag==0)
			{
				Cache["Invoice_No"]=inv_no;
				Flag=1;
			}
			else if(Flag==3)
			{
				Cache["Invoice_No"] = inv_no;
			}
			if(status==0)
			{
				sql = "select count(*) from vw_SaleBook  where Invoice_No="+Cache["Invoice_No"].ToString()+"";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					count += int.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
				status=1;
			}
			if(i<count)
			{
				
				
				Flag=2;
				i++;
			}
			if(i==count)
			{
				//amt1=amt;
				sql = "select Net_amount from sales_master where Invoice_No="+Cache["Invoice_No"].ToString()+"";
				SqlDtr =obj.GetRecordSet(sql);
				while(SqlDtr.Read())
				{
					amt1 = double.Parse(SqlDtr.GetValue(0).ToString());
				}
				SqlDtr.Close();
							
				//amt=0;
				status=0;
				i=0;
				Flag=3;
				count=0;
				
			}
			else
			{
				amt1=0;
				Flag=4;
			}
			
			if(Flag==4)
				return "     ---     ";
			else if(Flag==3)
			{
				am+=amt1;
				Cache["am"]=am;
				return GenUtil.strNumericFormat(amt1.ToString());
			}
			return "";
		}
		//***************

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
	}
}
