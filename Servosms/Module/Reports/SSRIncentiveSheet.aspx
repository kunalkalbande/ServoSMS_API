<%@ Page language="c#" Inherits="Servosms.Module.Reports.SSRIncentiveSheet" CodeFile="SSRIncentiveSheet.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ import namespace="System.Data.SqlClient" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: SSR Incentive Sheet Report</title> 
		<!--
	Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
    No part of this software shall be reproduced, stored in a 
	retrieval system, or transmitted by any means, electronic 
	mechanical, photocopying, recording  or otherwise, or for
	any  purpose  without the express  written  permission of
	bbnisys Technologies.
	-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox><uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center" border="0">
				<TR valign="top" height="20">
					<TH colspan="2">
						<font color="#ce4848">SSR Incentive Sheet Report</font>
						<hr>
					</TH>
				</TR>
				<tr valign="top">
					<td align="right" width="60%">Date From&nbsp;&nbsp;
						<asp:textbox id="txtDateFrom" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
							CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A> &nbsp;&nbsp;Date To&nbsp;&nbsp;
						<asp:textbox id="txtDateTo" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
							CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td width="40%"><asp:button id="btnShow" runat="server" Width="75px" 
							 Text="View   " onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="BtnPrint" Width="75px"
							Text="Print  " Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;
						<asp:button id="btnExcel" Width="75px"
							Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<%if(View==1){%>
				<tr align="center">
					<td align="center" colspan="2">
						<%
						InventoryClass obj = new InventoryClass();
						SqlDataReader rdr=null;
						//coment by vikas 29.10.2012 string str="select Emp_ID,Emp_Name from Employee where Designation='Servo Sales Representative'";
						string str="select Emp_ID,Emp_Name from Employee where status='1' and Designation='Servo Sales Representative'";
						rdr = obj.GetRecordSet(str);
						TotalAmount = new double[10];
						if(rdr.HasRows){
						%>
						<TABLE width="90%" border="1" borderColor="#deba84" cellpadding="0" cellspacing="0">
							<tr bgColor="#ce4848" height="40">
								<td align="center"><font color="white"><b>SSR Name</b></font></td>
								<td align="center"><font color="white"><b>Receipt</b></font></td>
								<td align="center"><font color="white"><b>Cash<br>
											Discount</b></font></td>
								<td align="center"><font color="white"><b>Spacial<br>
											Discount</b></font></td>
								<td align="center"><font color="white"><b>Credit Note</b></font></td>
								<td align="center"><font color="white"><b>Cheque Bounce</b></font></td>
								<td align="center"><font color="white"><b>Total<br>
											Receipt</b></font></td>
								<td align="center"><font color="white"><b>Total<br>
											Incentive</b></font></td>
								<td align="center"><font color="white"><b>Basic<br>
											Salary</b></font></td>
								<td align="center"><font color="white"><b>Salary +<br>
											Incentive</b></font></td>
							</tr>
							<%while(rdr.Read()){%>
							<tr>
								<td height="20">&nbsp;<%=rdr["Emp_Name"].ToString()%></td>
								<td align="right"><%=GetReceipt(rdr["Emp_ID"].ToString())%>&nbsp;</td>
								<td align="right"><%=GetCashDiscount(rdr["Emp_ID"].ToString())%>&nbsp;</td>
								<td align="right"><%=GetSpacialDiscount(rdr["Emp_ID"].ToString())%>&nbsp;</td>
								<td align="right"><%=GetCreditNote(rdr["Emp_ID"].ToString())%>&nbsp;</td>
								<td align="right"><%=GetBounce(rdr["Emp_ID"].ToString())%>&nbsp;</td>
								<td align="right"><%=GenUtil.strNumericFormat(TotalReceipt.ToString())%><%TotalAmount[5]+=TotalReceipt;%>&nbsp;</td>
								<td align="right"><%=GetIncentive(TotalReceipt.ToString())%>&nbsp;</td>
								<td align="right"><%=GetBasicSalary(rdr["Emp_ID"].ToString())%>&nbsp;</td>
								<td align="right"><%=GetSalaryIncentive(TotalReceipt.ToString(),rdr["Emp_ID"].ToString())%>&nbsp;</td>
							</tr>
							<%}%>
							<tr bgColor="#ce4848">
								<td align="center"><font color="white"><b>Total</b></font></td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[0].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[1].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[2].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[3].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[4].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[5].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[6].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[7].ToString())%></b></font>&nbsp;</td>
								<td align="right"><font color="white"><b><%=GenUtil.strNumericFormat(TotalAmount[8].ToString())%></b></font>&nbsp;</td>
							</tr>
						</TABLE>
						<%}%>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				<%}%>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
