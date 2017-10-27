<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Employee.Mechanic" CodeFile="Mechanic.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Mechanic Report</title><!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="278" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#CE4848">Mechanic Entry</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE style="WIDTH: 494px; HEIGHT: 302px" border="0">
							<TR>
								<TD colSpan="4"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 71px">Mechanic&nbsp;ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD style="WIDTH: 142px"><asp:label id="LblMechanicID" ForeColor="Blue" Width="120px" Runat="server"></asp:label></TD>
								<TD style="WIDTH: 145px"></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 71px"></TD>
								<TD style="WIDTH: 142px">First Name</TD>
								<TD style="WIDTH: 145px">Middle Name</TD>
								<TD>Last Name</TD>
							</TR>
							<TR>
								<TD style="WIDTH: 71px">Name&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtFName" ErrorMessage="Please Fill Employee Name">*</asp:requiredfieldvalidator></TD>
								<TD style="WIDTH: 142px"><asp:textbox onkeypress="return GetOnlyChars(this, event);" id="txtFName" runat="server" Width="130px"
										BorderStyle="Groove" MaxLength="50"></asp:textbox></TD>
								<TD style="WIDTH: 145px"><asp:textbox onkeypress="return GetOnlyChars(this, event);" id="txtMName" runat="server" Width="130px"
										BorderStyle="Groove" MaxLength="50"></asp:textbox></TD>
								<TD><asp:textbox onkeypress="return GetOnlyChars(this, event);" id="txtLName" runat="server" Width="130px"
										BorderStyle="Groove" MaxLength="50"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 71px; HEIGHT: 41px">Address</TD>
								<TD style="WIDTH: 281px; HEIGHT: 41px" colSpan="2"><asp:textbox id="txtAddress" runat="server" Width="271px" BorderStyle="Groove" TextMode="MultiLine"></asp:textbox></TD>
								<TD style="HEIGHT: 41px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 71px; HEIGHT: 24px">City&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="DropCity" ErrorMessage="Please Select City"
										ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></TD>
								<TD style="WIDTH: 281px; HEIGHT: 24px" colSpan="2"><asp:dropdownlist id="DropCity" runat="server" Width="208px" AutoPostBack="True" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD style="HEIGHT: 24px"></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 71px; HEIGHT: 20px">District</TD>
								<TD style="WIDTH: 281px; HEIGHT: 20px" colSpan="2"><asp:dropdownlist id="DropState" runat="server" Width="208px">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD style="HEIGHT: 20px"></TD>
							<tr>
								<td style="WIDTH: 71px">State</td>
								<TD style="WIDTH: 281px; HEIGHT: 14px" colSpan="2"><asp:dropdownlist id="DropCountry" runat="server" Width="208px">
										<asp:ListItem Value="Select"></asp:ListItem>
									</asp:dropdownlist></TD>
							</tr>
							<TR>
								<TD style="WIDTH: 71px">Contact No&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtContactNo"
										ErrorMessage="Contact No. Between 6-10 Digits" ValidationExpression="\d{6,10}">*</asp:regularexpressionvalidator></TD>
								<TD style="WIDTH: 142px"><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtContactNo" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="12"></asp:textbox></TD>
								<TD style="WIDTH: 145px" align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Mobile 
									No
									<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile No. Between 10-12 Digits"
										ValidationExpression="\d{10,12}">*</asp:regularexpressionvalidator></TD>
								<TD><FONT size="2"><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtMobile" runat="server" Width="130px"
											BorderStyle="Groove" MaxLength="15"></asp:textbox></FONT></FONT></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4">
									<asp:button id="btnUpdate" runat="server" Width="100px" Text="Save Profile" 
										 onclick="btnUpdate_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" Height="4px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
