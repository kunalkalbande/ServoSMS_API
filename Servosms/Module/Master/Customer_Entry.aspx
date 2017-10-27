<%@ Page language="c#" Inherits="Servosms.Module.Master.Customer_Entry" CodeFile="Customer_Entry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Entry</title><!--
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="Beat" src="../../Sysitem/JS/Beat.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
			<uc1:header id="Header1" runat="server"></uc1:header><input id="txtbeatname" type="hidden" name="txtbeatname" runat="server">
			<table cellSpacing="0" cellPadding="0" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Customer Entry</FONTS>
							<hr>
						</font>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellSpacing="0" cellPadding="0">
							<TR>
								<TD colSpan="4"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD>Customer ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:label id="LblCustomerID" CssClass="dropdownlist" Runat="server" ForeColor="Blue" Width="97px"></asp:label></TD>
								<TD align="center">&nbsp;Unique Code</TD>
								<TD><asp:textbox id="txtcode" runat="server" CssClass="dropdownlist" Width="131px" MaxLength="12"
										BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Customer Name&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtFName" ErrorMessage="Please Fill Customer name">*</asp:requiredfieldvalidator></TD>
								<TD colspan="3"><asp:textbox id="txtFName" runat="server" CssClass="dropdownlist" Width="130px" MaxLength="49"
										BorderStyle="Groove" ontextchanged="txtFName_TextChanged"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 3px">Type&nbsp;&nbsp; <FONT color="#ff0000">*</FONT> <FONT color="red">
										<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="DropType" ErrorMessage="Please Select Customer Type"
											ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></FONT></TD>
								<TD style="HEIGHT: 3px"><asp:dropdownlist id="DropType" runat="server" CssClass="dropdownlist" Width="130px" onselectedindexchanged="DropType_SelectedIndexChanged"></asp:dropdownlist></TD>
								<TD align="center" style="HEIGHT: 3px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GSTIN 
									No.<FONT color="#ff0033">&nbsp;</FONT></TD>
								<TD style="HEIGHT: 3px"><asp:textbox id="txtTinNo" runat="server" CssClass="dropdownlist" Width="130px" MaxLength="15"
										BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Address</TD>
								<TD colSpan="3"><asp:textbox id="txtAddress" runat="server" CssClass="dropdownlist" Width="130px" MaxLength="99"
										BorderStyle="Groove" Font-Names="Verdana" ontextchanged="txtAddress_TextChanged"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>City&nbsp;&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator2" runat="server" ControlToValidate="DropCity" ErrorMessage="Please Select City"
										ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></TD>
								<TD><asp:dropdownlist id="DropCity" runat="server" CssClass="dropdownlist" Width="130px" AutoPostBack="false"
										onchange="getbeatInfo(this,document.Form1.DropState,document.Form1.DropCountry);" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SSR</TD>
								<TD><asp:dropdownlist id="DropSSR" CssClass="dropdownlist" Runat="server" Width="100%">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>District</TD>
								<TD><asp:dropdownlist id="DropState" runat="server" CssClass="dropdownlist" Width="130px">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">Contact Person</TD>
								<TD><asp:textbox id="txtContactPerson" CssClass="fontstyle" Runat="server" Width="100%" MaxLength="49"
										BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>State</TD>
								<TD><asp:dropdownlist id="DropCountry" runat="server" CssClass="dropdownlist" Width="130px">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD>&nbsp;Off
									<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneOff"
										ErrorMessage="Contact No. Between 6-10 Digits" ValidationExpression="\d{6,12}">*</asp:regularexpressionvalidator></TD>
								<TD>Res&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhoneRes"
										ErrorMessage="Contact No. Between 6-10 Digits" ValidationExpression="\d{6,12}">*</asp:regularexpressionvalidator></TD>
								<TD>Mobile&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile No. Between 10-12 Digits"
										ValidationExpression="\d{10,12}">*</asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD>Contact No</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtPhoneOff" runat="server"
										CssClass="dropdownlist" Width="130px" MaxLength="12" BorderStyle="Groove"></asp:textbox></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtPhoneRes" runat="server"
										CssClass="dropdownlist" Width="130px" MaxLength="12" BorderStyle="Groove"></asp:textbox></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtMobile" runat="server" CssClass="dropdownlist"
										Width="130px" MaxLength="12" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>E - Mail
									<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtEMail" ErrorMessage="Please Fill Valid E-mail"
										ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></TD>
								<TD colSpan="3"><asp:textbox id="txtEMail" runat="server" CssClass="dropdownlist" Width="130px" MaxLength="49"
										BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Credit Limit&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" ControlToValidate="txtCRLimit" ErrorMessage="Credit Limit Should be Correct"
										ValidationExpression="(\d+\.\d+)|(\d+)">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtCRLimit" runat="server"
										CssClass="dropdownlist" Width="130px" MaxLength="8" BorderStyle="Groove"></asp:textbox></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Credit Days</TD>
								<TD><asp:dropdownlist id="DropCrDay" CssClass="dropdownlist" Runat="server" Width="72px">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>Op. Balance&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" ControlToValidate="txtOpBalance"
										ErrorMessage="Opening Balance Should be Correct" ValidationExpression="(\d+\.\d+)|(\d+)">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,true);" id="txtOpBalance" runat="server"
										CssClass="dropdownlist" Width="130px" MaxLength="8" BorderStyle="Groove"></asp:textbox></TD>
								<TD><asp:dropdownlist id="DropBal" CssClass="dropdownlist" Runat="server">
										<asp:ListItem Value="Cr.">Cr.</asp:ListItem>
										<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4">
									<HR width="100.09%" color="#000099" SIZE="1">
									<asp:button id="btnUpdate" runat="server" Width="90px" Text="Save Profile"
										 onclick="btnUpdate_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
							Height="12px"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
