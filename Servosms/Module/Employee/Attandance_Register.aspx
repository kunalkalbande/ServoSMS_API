<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
<%@ Page Language="c#" Inherits="Servosms.Module.Employee.Attandance_Register" CodeFile="Attandance_Register.aspx.cs" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="RMG"%>
<HTML>
	<HEAD>
	 <script language="javascript">
		function change(e)
		{
			if(window.event) 
			{
				key = e.keyCode;
				isCtrl = window.event.ctrlKey
				if(key==17)
					document.getElementById("STM0_0__0___").focus();		
			}
		}
		if(document.getElementById("STM0_0__0___")!=null)
			window.onload=change();
		</script>
	<script language=JavaScript>
	
	function selectAll()
	{
		var f=document.f1
		if(f.chkSelectAll.checked)
			for(var i=0;i<f.length;i++)
				f.elements[i].checked=true
		else
			for(var i=0;i<f1.length;i++)
				f.elements[i].checked=false
	}
	</script>
		<title>ServoSMS: Attandance Register</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src=../../Sysitem/JS/Validations.js></script>
	</HEAD>
	<body onkeydown="change(event)">
		<form name="f1" id="f1" method="post" runat=server>
		<uc1:header id="Header1" runat="server"></uc1:header>
			<table width=778 height=274 align=center>
				<TR>
					<TH align="center"><font color=#CE4848>Attendance Register</font><hr></TH>
				</TR>
				<tr>
					<td align=center>
						<table border=1 bordercolor="#DEBA84" cellpadding=0 cellspacing=0>
							<asp:Panel Runat=server ID=panEmp Visible="false">
							<tr>
							<td colspan=4>Attendance Date &nbsp;&nbsp;<asp:DropDownList Runat=server ID="DropEmp" AutoPostBack=True><asp:ListItem Value="Select">Select</asp:ListItem></asp:DropDownList>
							<asp:CompareValidator ID=cv1 Runat=server ControlToValidate="DropEmp" ErrorMessage="Please Select The Date" ValueToCompare="Select" Operator=NotEqual>*</asp:CompareValidator>
							<asp:RadioButton ID=RbtnAsc Runat=server Text="Asc" GroupName=Name AutoPostBack=True></asp:RadioButton> <asp:RadioButton ID=RbtnDesc Runat=server GroupName=Name Text="Desc" AutoPostBack=True></asp:RadioButton></td>
							</tr>
							</asp:Panel>
							<asp:Panel Runat=server ID="pandate">
							<tr><td colspan=4>Attendance Date &nbsp;&nbsp;<asp:textbox id="txtdate" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.f1.txtdate);return false;">
                                                <IMG class="PopcalTrigger" alt="" src=../../HeaderFooter/DTPicker/calender_icon.jpg
												align="absMiddle" border="0"></A>&nbsp;<asp:Button ID="btnView" Text=View Runat=server  Width=80 CausesValidation=True onclick="btnView_Click"></asp:Button></td></tr>
												</asp:Panel>
							<tr>
								<th width=60 align="center" bgcolor="#CE4848"><font color=white>Emp ID</font></th>
								<th width=200 align="center" bgcolor="#CE4848"><font color=white>Name</font></th>
								<th width=150 align="center" bgcolor="#CE4848"><font color=white>Designation</font></th>
								<th width=50 align="center" bgcolor="#CE4848"><font color=white>Status</font></th>
							</tr>
							<% 
							//try{
                           
								EmployeeClass obj=new EmployeeClass();
								SqlDataReader SqlDtr;
								string sql;
								int Row_No=0;
								string str1;
								int i=0;
								//coment by vikas 29.10.2012 str1=obj.date();
								str1=GenUtil.str2DDMMYYYY(txtdate.Text.ToString());
								if(panEmp.Visible==false)
								{ 
									//coment by vikas 29.10.2012 sql=" select employee.emp_id,employee.Emp_Name,employee.Designation from employee where emp_id!=all(select distinct Attandance_Register.emp_id from Attandance_Register where att_Date ='"+str1+"')and emp_ID!=all(    select  emp_id from leave_Register where  getdate() between Date_from and DATEADD(day, 1, date_to) and  issanction=1)"; 
									//coment by vikas 6.11.2012 sql=" select employee.emp_id,employee.Emp_Name,employee.Designation from employee where status='1' and emp_id!=all(select distinct Attandance_Register.emp_id from Attandance_Register where att_Date ='"+str1+"')and emp_ID!=all( select  emp_id from leave_Register where  getdate() between Date_from and DATEADD(day, 1, date_to) and  issanction=1)"; 
									sql="select employee.emp_id,employee.Emp_Name,employee.Designation from employee where status='1' and emp_ID!=all( select  emp_id from leave_Register where  Convert(datetime,'"+str1+"',103) between Date_from and DATEADD(day, 1, date_to) and  issanction=1) order by Designation";
									SqlDtr=obj.GetRecordSet(sql);
									while(SqlDtr.Read())
									{
							%>
							<tr>
								<td bgColor="#FFF7E7">&nbsp;<font color="#8C4510"><%=SqlDtr.GetValue(0).ToString()%></font><input type=hidden name=lblEmpID<%=Row_No%> value="<%=SqlDtr.GetValue(0).ToString()%>"></td>
								<td bgColor="#FFF7E7">&nbsp;<font color="#8C4510"><%=SqlDtr.GetValue(1).ToString()%></font><input type=hidden name=lblEmpName<%=Row_No%> value="<%=SqlDtr.GetValue(1).ToString()%>"></td>
								<td bgColor="#FFF7E7">&nbsp;<font color="#8C4510"><%=SqlDtr.GetValue(2).ToString()%></font><input type=hidden name=lblDesig<%=Row_No%> value="<%=SqlDtr.GetValue(2).ToString()%>"></td>
								
								<%
									SqlConnection SqlCon =new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Servosms"]);
									SqlCon.Open();
									sql="select status from attandance_register where emp_id='"+SqlDtr.GetValue(0).ToString()+"' and att_date='"+GenUtil.str2MMDDYYYY(str1)+"'";
									SqlCommand SqlCmd=new SqlCommand (sql,SqlCon );
									SqlDataReader SqlDtRed  = SqlCmd.ExecuteReader();
									if(SqlDtRed.HasRows)
									{
										while(SqlDtRed.Read())
										{
											if(SqlDtRed["status"]!=null && SqlDtRed["status"]!="")
											{
												if(double.Parse(SqlDtRed["Status"].ToString())==1)
												{
													%>
													<td bgColor="#FFF7E7" align=center><input type=checkbox name=chk<%=Row_No%> checked></td>
													<%
												}
												else
												{
													%>
													<td bgColor="#FFF7E7" align=center><input type=checkbox name=chk<%=Row_No%>></td>
													<%
												}
											}
										}
									}
									else
									{
										%>
										<td bgColor="#FFF7E7" align=center><input type=checkbox name=chk<%=Row_No%>></td>
										<%
										}
									SqlDtRed.Close();
								
								%>
								
							</tr>
							<%		Row_No++;
									}
									SqlDtr.Close();
							//}
							//catch(Exception ex)
							//{
							//CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:page_load() EXCEPTION: "+ ex.Message+" userid :"+ Session["User_Name"].ToString());
							//}
							%>
							<input type=hidden name=lblTotal_Row value=<%=Row_No%>>
							<%}
							else if(DropEmp.SelectedIndex!=0)
							{
								//Coment By Vikas 23.10.2012 string str="Select a.Emp_ID,e.Emp_Name,e.Designation,a.Status from Attandance_Register a,Employee e where Att_date='"+GenUtil.str2MMDDYYYY(DropEmp.SelectedItem.Text)+"' and a.Emp_ID=e.Emp_ID";
								string str="";
								
								if(RbtnAsc.Checked==true)
									str="Select a.Emp_ID,e.Emp_Name,e.Designation,a.Status from Attandance_Register a,Employee e where Att_date='"+GenUtil.str2MMDDYYYY(DropEmp.SelectedItem.Text)+"' and a.Emp_ID=e.Emp_ID order by Emp_Name";
								else if(RbtnDesc.Checked==true)
									str="Select a.Emp_ID,e.Emp_Name,e.Designation,a.Status from Attandance_Register a,Employee e where Att_date='"+GenUtil.str2MMDDYYYY(DropEmp.SelectedItem.Text)+"' and a.Emp_ID=e.Emp_ID order by Emp_Name desc";
								else
									str="Select a.Emp_ID,e.Emp_Name,e.Designation,a.Status from Attandance_Register a,Employee e where Att_date='"+GenUtil.str2MMDDYYYY(DropEmp.SelectedItem.Text)+"' and a.Emp_ID=e.Emp_ID";
									
							SqlDtr = obj.GetRecordSet(str);
							
							while(SqlDtr.Read())
							{
							%>
							<tr>
							<td>&nbsp;<font color=#4A3C8C><%=SqlDtr["Emp_ID"].ToString()%></font><input type=hidden name=tempEmpID<%=i%> value=<%=SqlDtr["Emp_ID"].ToString()%>></td>
							<td>&nbsp;<font color=#4A3C8C><%=SqlDtr["Emp_Name"].ToString()%></font></td>
							<td>&nbsp;<font color=#4A3C8C><%=SqlDtr["Designation"].ToString()%></font></td>
							<%if(double.Parse(SqlDtr["Status"].ToString())==1){%>
							<td align=center><input type=checkbox name=chk<%=i%> checked></td>
							<%}else{%>
							<td align=center><input type=checkbox name=chk<%=i%>></td>
							<%}%>
							</tr>
							<%i++;}}%>
							<tr><th bgColor="#CE4848" colspan=3 align=right><font color=white><b>Select All</b></font>&nbsp;&nbsp;&nbsp;&nbsp;</th><td align=center bgColor="#CE4848"><input type=checkbox name=chkSelectAll onClick="selectAll();"></td></tr>
							<tr><td><input type=hidden name=CountEdit value=<%=i%>></td></tr>
						</table>
					</td>
				</tr>
				<TR>
					<td align=center><asp:Button ID=Btnsave Text=Submit Runat=server Width=80 CausesValidation=True onclick="Btnsave_Click"></asp:Button>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnEdit" Text=Edit Runat=server OnClick="View"  Visible=False  Width=70 CausesValidation=True></asp:Button></td>
				</TR>
				<tr><td><asp:ValidationSummary ID=vs1 Runat=server ShowMessageBox=True ShowSummary=False></asp:ValidationSummary></td></tr>
				
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src=../../HeaderFooter/DTPicker/ipopeng.htm frameBorder="0" width="174" scrolling="no"
				height="189"></iframe><uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>


<script language=C# runat=server >
		private void Page_Load(object sender, System.EventArgs e)
		{
			string uid="";
			DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			try
			{
				 uid=(Session["User_Name"].ToString());
               txtdate.Text = Request.Form["txtdate"] == null ? DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year.ToString() : Request.Form["txtdate"].ToString();
			}
			catch(Exception ex)
			{
				CreateLogFiles.ErrorLog("Form:Addtandance_Registor,Method:Page_load  userid "+ uid);
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
					
			}
			if(!IsPostBack)
			{
				#region Check Privileges
				int i;
				string View_flag="0", Add_Flag="0", Edit_Flag="0", Del_Flag="0";
				string Module="2";
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
				
				if(View_flag=="0")
				{
					string msg="UnAthourized Visit to Attandance Register Page";
					
					Response.Redirect("../../Sysitem/AccessDeny.aspx",false);
				}
				if(Add_Flag=="0")
				Btnsave.Enabled=false;
				#endregion
			}
			
			
		}
		
	// this method used to save the attendance of the present employee.
	public void attan(Object sender, EventArgs e)
	{
		try
		{
			EmployeeClass obj=new EmployeeClass(); 
			int Total_Rows=0;
			SqlDataReader SqlDtr;
			string sql;
			/*
			sql="select Count(*) from Attandance_Register where Att_Date='"+DateTime.Today.Month.ToString()+"/"+DateTime.Today.Day.ToString()+"/"+DateTime.Today.Year.ToString() +"'";

			SqlDtr=obj.GetRecordSet(sql); 
			while(SqlDtr.Read())
			{
				int flag=System.Convert.ToInt32(SqlDtr.GetValue(0).ToString()); 
				if(flag>0)
				{
						return;					
				}
			}
			
			SqlDtr.Close();
			*/
			
			string empid ="";
			string empid1 ="";
			Total_Rows=System.Convert.ToInt32(Request.Params.Get("lblTotal_Row"));
			/*comrnt by vikas 6.11.2012 if(panEmp.Visible==false)
			{
				Total_Rows=System.Convert.ToInt32(Request.Params.Get("lblTotal_Row"));
				for(int i=0;i<Total_Rows;i++)
				{
					if(Request.Params.Get("Chk"+i)!=null)
					{
						string str="";
						//coment by vikas 29.10.2012 obj.Att_Date=DateTime.Now.ToShortDateString(); 
						obj.Att_Date=GenUtil.str2MMDDYYYY(txtdate.Text.ToString()); 
												 
						obj.Emp_ID=Request.Params.Get("lblEmpID"+i); 
						obj.Status="1";
						EmployeeClass obj1=new EmployeeClass(); 
						SqlDataReader SqlDtr11;
						string sql1;
						//coment by vikas 29.10.2012 sql1="select Status from Attandance_Register where Att_Date='"+DateTime.Now.ToShortDateString()+"' and  Emp_ID="+Request.Params.Get("lblEmpID"+i)+""; 
						sql1="select Status from Attandance_Register where Att_Date='"+GenUtil.str2MMDDYYYY(txtdate.Text.ToString())+"' and  Emp_ID="+Request.Params.Get("lblEmpID"+i)+""; 
						SqlDtr11=obj1.GetRecordSet(sql1);
						while(SqlDtr11.Read())
						{
							str=SqlDtr11.GetValue(0).ToString();
						}
						if(str.Equals("0") || str.Equals(""))		
						{
							obj.InsertEmployeeAttandance();
							CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). Attendance of employee ID "+Request.Params.Get("lblEmpID"+i)+" Saved. userid :"+ Session["User_Name"].ToString());			
							empid= empid+" "+Request.Params.Get("lblEmpID"+i)+"   ";
						}
						else
						{
							empid1= empid1+" "+Request.Params.Get("lblEmpID"+i)+"   ";
						}
					}
				}
				if(!empid.Equals(""))
				{
					MessageBox.Show("Attendance Saved");
				}
				//coment by vikas 6.11.2012 if(!empid1.Equals(""))
				//coment by vikas 6.11.2012    MessageBox.Show("Attandance Already Exits For Employee ID "+empid1); 
			}
			else
			{*/
				Total_Rows=System.Convert.ToInt32(Request.Params.Get("CountEdit"));
				for(int i=0;i<Row_No;i++)
				{
					string str="";
					if(Request.Params.Get("Chk"+i)!=null)
					{	
						obj.Att_Date=GenUtil.str2MMDDYYYY(txtdate.Text.ToString());  
						obj.Emp_ID=Request.Params.Get("lblEmpID"+i); 
						obj.Status="1";
						obj.UpdateEmployeeAttandance();
						CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). Attendance of employee ID "+Request.Params.Get("tempEmpID"+i)+" Updated. userid :"+ Session["User_Name"].ToString());
					}
					else
					{
						/*coment by vikas 29.10.2012 obj.Att_Date=GenUtil.str2MMDDYYYY(DropEmp.SelectedItem.Text);  
						obj.Emp_ID=Request.Params.Get("tempEmpID"+i); 
						obj.Status="0";
						obj.UpdateEmployeeAttandance();
						CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). Attendance of employee ID "+Request.Params.Get("tempEmpID"+i)+" Updated. userid :"+ Session["User_Name"].ToString());*/
						string Emp_ID=Request.Params.Get("lblEmpID"+i);
						string Attan_Date=GenUtil.str2MMDDYYYY(txtdate.Text.ToString());
						sql="delete from Attandance_Register where emp_id="+Emp_ID+" and att_date='"+Attan_Date+"'";
						obj.ExecRecord(sql);
					}
				}
				MessageBox.Show("Attendance Update");
				panEmp.Visible=false;
				pandate.Visible=true;
			//}
		}
		catch(Exception ex)
		{
			CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). EXCEPTION: "+ ex.Message+" userid :"+ Session["User_Name"].ToString());
		}
	}
		
	public void View(Object sender, EventArgs e)
	{
		try
		{
			pandate.Visible=false;
			panEmp.Visible=true;
			EmployeeClass obj=new EmployeeClass(); 
			SqlDataReader SqlDtr;
			string sql;
			//Coment By Vikas 23.10.2012 sql="select distinct Att_Date from Attandance_Register";
			sql="select distinct Att_Date from Attandance_Register order by Att_Date desc";
			SqlDtr=obj.GetRecordSet(sql);
			DropEmp.Items.Clear();
			DropEmp.Items.Add("Select");
			while(SqlDtr.Read())
			{
				DropEmp.Items.Add(GenUtil.str2DDMMYYYY(GenUtil.trimDate(SqlDtr["Att_Date"].ToString())));
			}
			SqlDtr.Close();
		}
		catch(Exception ex)
		{
			CreateLogFiles.ErrorLog("Form:Attendance_Register.aspx.cs,Method:attan(). EXCEPTION: "+ ex.Message+" userid :"+ Session["User_Name"].ToString());
		}
	}
	
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
</script>
