<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Employee.Leave_Register" CodeFile="Leave_Register.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Leave Register</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language=javascript id=validation src="../../Sysitem/JS/Validations.js"></script>
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
		<form method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:TextBox>
			<table width="778" height="290" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Leave Application</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE style="WIDTH: 325px; HEIGHT: 120px">
							<TR>
								<TD colSpan="2"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD>
									Employee ID&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Please Select Employee ID" ControlToValidate="DropEmpName"
										Operator="NotEqual" ValueToCompare="Select">*</asp:CompareValidator>
									<FONT color="red">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</FONT></TD>
								<TD><asp:dropdownlist id="DropEmpName" runat="server" Width="215px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									Date&nbsp;From&nbsp;&nbsp;&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Please Select Date From">*</asp:RequiredFieldValidator></TD>
								<TD><asp:TextBox id="txtDateFrom" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
										CssClass="FontStyle" ontextchanged="txtDateFrom_TextChanged"></asp:TextBox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
							</TR>
							<TR>
								<TD>
									Date To&nbsp;&nbsp;&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTO" ErrorMessage="Please Select Date To">*</asp:RequiredFieldValidator></TD>
								<TD><asp:TextBox id="txtDateTO" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
										CssClass="FontStyle"></asp:TextBox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtDateTO);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
							</TR>
							<tr><td>Day's</td><td><asp:TextBox ID=txtleaveday Runat=server BorderStyle=Groove CssClass="FontStyle" MaxLength=4 Width=110px></asp:TextBox></td></tr>
							<TR>
								<TD>
									Reason&nbsp;&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please Specify the Reason of Leave"
										ControlToValidate="txtReason">*</asp:RequiredFieldValidator></TD>
								<td><asp:textbox id="txtReason" runat="server" Width="110px" Height="42px" TextMode="MultiLine" BorderStyle="Groove"
										Font-Names="Verdana" CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD align="right" colSpan="2">
									<asp:button id="btnApply" runat="server" Width="80px" Text="Apply" 
										 onclick="btnApply_Click"></asp:button>
								</TD>
							</TR>
						</TABLE>
						<asp:ValidationSummary id="ValidationSummary1" runat="server" Height="16px" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
					</td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
