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
using System.Data.SqlClient ;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Servosms.Sysitem.Classes ;
using RMG;

namespace Servosms.Module.Employee
{
	/// <summary>
	/// Summary description for Leave_Register.
	/// </summary>
	public partial class OT_Compensation : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
		string uid;

		/// <summary>
		/// This method is used for setting the Session variable for userId and 
		/// after that filling the required dropdowns with database values 
		/// and also check accessing priviledges for particular user
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
				CreateLogFiles.ErrorLog("Form:Leave_Register.aspx,Method:pageload"+"EXCEPTION  "+ ex.Message+ uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);	
				return;
			}
			if(!Page.IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="2";
				string SubModule="3";
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
				//***********
				if(Add_Flag=="0")
					btnApply.Enabled=false;
				//***********
				#endregion
				try
				{
					// Sets todays date in from and to date text boxes.
					txtDateFrom.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
					txtDateTO.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();  

					#region Fetch Employee ID and Name of All Employee
					EmployeeClass  obj=new EmployeeClass();
					SqlDataReader SqlDtr;
					string sql;
	
					//coment by vikas 17.11.2012 sql="select Emp_ID,Emp_Name from Employee order by Emp_Name";
					sql="select Emp_ID,Emp_Name from Employee where status='1' order by Emp_Name";
					SqlDtr=obj.GetRecordSet(sql);
					while(SqlDtr.Read())
					{
						DropEmpName.Items.Add(SqlDtr.GetValue(0).ToString ()+":"+SqlDtr.GetValue(1).ToString());				
					}
					SqlDtr.Close();
					#endregion
				}
				catch(Exception ex)
				{
					CreateLogFiles.ErrorLog("Form:Leave_Register.aspx,Method:pageload"+"EXCEPTION  "+ ex.Message+ uid);
				}
			}
            txtDateFrom.Text = Request.Form["txtDateFrom"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateFrom"].ToString().Trim();
            txtDateTO.Text = Request.Form["txtDateTO"] == null ? GenUtil.str2DDMMYYYY(System.DateTime.Now.ToShortDateString()) : Request.Form["txtDateTO"].ToString().Trim();
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
		/// method converts the dd/mm/yyyy date to mm/dd/yyyy
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
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

		/// <summary>
		/// This method is used to save the employee leave with the help of ProLeaveEntry Procedure
		/// before check the date if Leave save already in this given date then first delete the record 
		/// and save the record update otherwise save the record in Leave_Register table.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnApply_Click(object sender, System.EventArgs e)
		{
			EmployeeClass obj=new EmployeeClass();
			try
			{
				#region Check Validation				
                var dt1 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()));
                var dt2 = System.Convert.ToDateTime(GenUtil.str2DDMMYYYY(Request.Form["txtDateTO"].ToString()));
                if (DateTime.Compare(dt1, dt2) > 0)
                {
                    MessageBox.Show("Date From Should be less than Date To");
                    return;
				}
				int Count=0;
				string str ="";
				str = "select count(*) from OverTime_Register where Emp_ID='"+DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":"))+"' and cast(floor(cast(cast(ot_from as datetime) as float)) as datetime) <='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"' and cast(floor(cast(cast(ot_to as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"'";
				dbobj.ExecuteScalar(str,ref Count);
				if(Count>0)
				{
					MessageBox.Show("OverTime already Saved");
					return;
				}
				int x=0;
				str = "select * from OverTime_Register where Emp_ID='"+DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":"))+"' and cast(floor(cast(cast(ot_from as datetime) as float)) as datetime) <='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"' and cast(floor(cast(cast(ot_to as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"'";
				dbobj.ExecuteScalar(str,ref Count);
				if(Count>0)
				{
					dbobj.Insert_or_Update("delete from OverTime_Register where Emp_ID='"+DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":"))+"' and cast(floor(cast(cast(ot_from as datetime) as float)) as datetime) <='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"' and cast(floor(cast(cast(ot_to as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"]) +"'",ref x);
				}
				#endregion
				obj.Leave_ID=NextOt_Id();
				obj.Emp_Name = DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":")) ;
				
				string Emp_id=DropEmpName.SelectedItem.Value.Substring(0,DropEmpName.SelectedItem.Value.LastIndexOf(":")) ;

				obj.Date_From  = GenUtil.str2MMDDYYYY(txtDateFrom.Text); 
				obj.Date_To  = GenUtil.str2MMDDYYYY(txtDateTO.Text);
				string todate=DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
				//obj.Reason =StringUtil.FirstCharUpper(txtReason.Text.ToString());
				//obj.Days = txtleaveday.Text.ToString().Trim();           //add by vikas 17.11.2012
				// calls fuction to insert the leave
				//obj.InsertLeave();
				x=0;
				dbobj.Insert_or_Update("Insert into OverTime_Register (OT_ID,OT_Date,Emp_Id,OT_From,Ot_To) values("+ Convert.ToInt32(obj.Leave_ID)+",'"+ GenUtil.str2MMDDYYYY(todate)+"',"+Emp_id+",'"+ GenUtil.str2MMDDYYYY(txtDateFrom.Text) + "','"+ GenUtil.str2MMDDYYYY(txtDateTO.Text)+"')",ref x);
				MessageBox.Show("OverTime Information Saved");
				Clear();
				
				CreateLogFiles.ErrorLog("Form : OT_Compensation.aspx, Method : btnApply_Click"+"  empname  :" + obj.Emp_Name   +" datefrom  "+ obj.Date_From  + "  uptodate "+obj.Date_To  +" for Reason "+ obj.Reason  +"  is saved  "+"  userid:   "+uid);
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form : OT_Compensation.aspx,Method:btnApply_Click"+"  empname  :" + obj.Emp_Name    +"  is saved  "+"  EXCEPTION  "+ex.Message  +"  userid:   "+uid);
			}
		}
		string Next_id="";
		public string NextOt_Id()
		{
			try
			{
				Next_id="1";
				EmployeeClass obj=new EmployeeClass();
				SqlDataReader rdr=null;
				string str = "select max(OT_ID) from OverTime_Register";
				rdr=obj.GetRecordSet(str);
				while(rdr.Read())
				{
					if(rdr.GetValue(0).ToString()!=null && rdr.GetValue(0).ToString()!="")
					{
						Next_id=rdr.GetValue(0).ToString();                       
					}
					else
						Next_id="1";
				}
				rdr.Close();
                int newID = Convert.ToInt32(Next_id) + 1;
                Next_id = newID.ToString();
                return Next_id;
			}
			catch(Exception ex)
			{
				return Next_id;
			}
		}
	

		/// <summary>
		/// Method to clear the form.
		/// </summary>
		public void Clear()
		{
			DropEmpName.SelectedIndex=0;
			txtDateFrom.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();
			txtDateTO.Text=DateTime.Today.Day.ToString()+"/"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Year.ToString();  
			txtReason.Text="";
			txtleaveday.Text="";
		}

		protected void txtDateFrom_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
