<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
<%@ Page Language="c#" Inherits="Servosms.Module.Employee.Leave_Assignment" CodeFile="Leave_Assignment.aspx.cs" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
		<title>ServoSMS: Leave Sanction</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form name="f1" id="f1" method="post" runat=server>
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<table width=778 height=290px align=center>
				<TR valign=top height=20>
					<TH align="center"><font color=#CE4848>Leave Sanction</font><hr>
					</TH>
				</TR>
				<tr>
					<td align=center valign=top>
						<table border=1 BorderColor="#DEBA84" cellpadding=0 cellspacing=0 align=center>
							<tr>
								<th align="center"  bgcolor=#CE4848><font color=#ffffff>Employee ID</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Name</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>From</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>To</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Reason</font></th>
								<th align="center" bgcolor=#CE4848><font color=#ffffff>Accept</font></th>
							</tr>
							<%
								EmployeeClass obj=new EmployeeClass();
								SqlDataReader SqlDtr;
								string sql;
								int Row_No=0;
								DateTime da;
								
								string str;
								string str1;
						
								DateTime dt;
								DateTime dt1;

							
								sql="select e.emp_id, emp_name, Date_From, Date_To, Reason, isSanction from employee e, Leave_Register lr where e.emp_id= lr.emp_id  and lr.isSanction=0 order by e.emp_id";
								SqlDtr=obj.GetRecordSet(sql);
								while(SqlDtr.Read())
								{
								
							str=SqlDtr.GetValue(2).ToString();
							dt=System.Convert.ToDateTime(str);
							str1=SqlDtr.GetValue(3).ToString();
							dt1=System.Convert.ToDateTime(str1);
							
							
							
							%>
							
							<tr>
								<td bgcolor=#fff7e7>&nbsp;<font color="#8c4510"><%=SqlDtr.GetValue(0).ToString()%><input type=hidden name=lblEmpID<%=Row_No%> value="<%=SqlDtr.GetValue(0).ToString()%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color="#8c4510"><%=SqlDtr.GetValue(1).ToString()%><input type=hidden name=lblEmpName<%=Row_No%> value="<%=SqlDtr.GetValue(1).ToString()%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color="#8c4510"><%=GenUtil.str2DDMMYYYY(dt.ToString("d"))%><input type=hidden name=lblDateFrom<%=Row_No%> value="<%=dt.ToString("d")%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color="#8c4510"><%=GenUtil.str2DDMMYYYY(dt1.ToString("d"))%><input type=hidden name=lblDateTo<%=Row_No%> value="<%=dt1.ToString("d")%>"></font></td>
								<td bgcolor=#fff7e7>&nbsp;<font color="#8c4510"><%=SqlDtr.GetValue(4).ToString()%><input type=hidden name=lblReason<%=Row_No%> value="<%=SqlDtr.GetValue(4).ToString()%>"></font></td>
								<%	if(SqlDtr.GetValue(5).ToString()=="1")
									{
								%>
										<td align=center  bgcolor=#fff7e7><input type=checkbox name=chk<%=Row_No%>></td>
								<%	}
									else
									{
								 %>
										<td align=center  bgcolor=#fff7e7><input type=checkbox name=chk<%=Row_No%>></td>
								<%	}
								%>
							</tr>
							<%	Row_No++;
								}
							%>
							<tr bgcolor=#fff7e7><td bgcolor=#fff7e7><input type=hidden name=lblTotal_Row value=<%=Row_No%>></td></tr>
							<tr><td colspan=5 align=right bgcolor=#fff7e7><font color=#8c4510>Select All</font></td><td align=center bgcolor=#fff7e7><input type=checkbox name=chkSelectAll onclick="selectAll();"></td></tr>
						</table>
					</td>
				</tr>
				<TR>
					<td align=center><asp:Button ID=Btnsave Runat=server Text=Submit OnClick="save1" Width=80></asp:Button></td>
				</TR>
			</table><uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
<script language=C# runat=server >
		private void Page_Load(object sender, System.EventArgs e)
		{
		
			try
			{
				string pass;
				pass=(Session["User_Name"].ToString());
			}
			catch
			{
				Response.Redirect("../../Sysitem/ErrorPage.aspx",false);
				return;
			}
			if(!Page.IsPostBack )
			{
				DBOperations.DBUtil dbobj=new DBOperations.DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
			
			}
			
		}
		
		public void save1(Object sender, EventArgs e)
		{ 
		
		try
		{
			EmployeeClass obj=new EmployeeClass(); 
			int Total_Rows=0;
			int f=0;
			Total_Rows=System.Convert.ToInt32(Request.Params.Get("lblTotal_Row"));
			for(int i=0;i<Total_Rows;i++)
			{
				if(Request.Params.Get("Chk"+i)!=null)
				{
				
					obj.Emp_Name=Request.Params.Get("lblEmpID"+i); 
					obj.Date_From=Request.Params.Get("lblDateFrom"+i); 
					
					obj.Date_To=Request.Params.Get("lblDateTo"+i); 
					
					obj.Reason=Request.Params.Get("lblReason"+i); 
					obj.isSanction="1";
					obj.UpdateLeave(); 
					f =1;
					CreateLogFiles.ErrorLog("Form:Leave_Assignment.aspx,Method:save1().  Leave Sanctioned for employee "+Request.Params.Get("lblEmpID"+i)+" of date from "+Request.Params.Get("lblDateFrom"+i)+" to date "+Request.Params.Get("lblDateTo"+i)+".  USERID  "+ Session["User_Name"].ToString());
					
					
				}
				else
				{
					}
			}
			if(f == 1)
			MessageBox.Show("Leave Sanctioned"); 
			}
			catch(Exception ex)
			{
			CreateLogFiles.ErrorLog("Form:Leave_Assignment.aspx,Method:save1().  Exception: "+ex.Message+"  USERID  "+ Session["User_Name"].ToString());
			}
			
			
			}
</script>
