<%@ Page language="c#" Inherits="Servosms.Module.Reports.DocumentCancelReport" CodeFile="DocumentCancelReport.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Document Cancel Report</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
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
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center" border="0">
				<tr vAlign="top" height="20">
					<th colSpan="5">
						<font color="#ce4848">Document Cancel&nbsp;Report</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td align="right" width="170">Date From&nbsp;&nbsp;</td>
					<td width="110"><asp:textbox id="txtDateFrom" runat="server" BorderStyle="Groove" CssClass="fontstyle" ReadOnly="True"
							Width="80px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A></td>
					<td align="center">Date To</td>
					<td width="110"><asp:textbox id="txtDateTo" runat="server" BorderStyle="Groove" CssClass="fontstyle" ReadOnly="True"
							Width="80px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A></td>
					<td><asp:button id="btnShow" runat="server" Width="60px" 
							Text="View" onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="60px" 
							Text="Print " Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Width="60px" 
							Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<%if(Flag!=0){%>
				<tr>
					<td vAlign="top" align="center" colSpan="5">
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="500" border="1">
							<tr>
								<%int Count=0;
								if(Purchase.Count>0){Count=1;%>
								<td vAlign="top" align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="center" bgColor="#ce4848"><font color="white"><b>Purchase</b></font></td>
										</tr>
										<%for(int i=0;i<Purchase.Count;i++){%>
										<tr>
											<td align="center"><%=Purchase[i].ToString()%></td>
										</tr>
										<%}%>
									</table>
								</td>
								<%}%>
								<%if(Sales.Count>0){Count=1;%>
								<td vAlign="top" align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="center" bgColor="#ce4848"><font color="white"><b>Sales</b></font></td>
										</tr>
										<%for(int i=0;i<Sales.Count;i++){%>
										<tr>
											<td align="center"><%=Sales[i].ToString()%></td>
										</tr>
										<%}%>
									</table>
								</td>
								<%}%>
								<%if(Receipt.Count>0){Count=1;%>
								<td vAlign="top" align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="center" bgColor="#ce4848"><font color="white"><b>Receipt</b></font></td>
										</tr>
										<%for(int i=0;i<Receipt.Count;i++){%>
										<tr>
											<td align="center"><%=Receipt[i].ToString()%></td>
										</tr>
										<%}%>
									</table>
								</td>
								<%}%>
								<%if(Payment.Count>0){Count=1;%>
								<td vAlign="top" align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="center" bgColor="#ce4848"><font color="white"><b>Payment</b></font></td>
										</tr>
										<%for(int i=0;i<Payment.Count;i++){%>
										<tr>
											<td align="center"><%=Payment[i].ToString()%></td>
										</tr>
										<%}%>
									</table>
								</td>
								<%}
							if(Count==0)
								MessageBox.Show("Data Not Available");
							%>
							</tr>
						</table>
					</td>
				</tr>
				<%}%>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
