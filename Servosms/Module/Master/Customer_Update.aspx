<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Master.Customer_Update_aspx" CodeFile="Customer_Update.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Update</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Validation" src="../../Sysitem/JS/Validations.js"></script>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<asp:textbox id="txtbeatname" Width="1" Runat="server" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"></asp:textbox>
			<asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" Visible="False"
				runat="server" Width="1px"></asp:textbox>
			<asp:textbox id="TempCustName" Width="1" Visible="False" Runat="server" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"></asp:textbox>
			<table height="290" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Update Customer</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellpadding="0" cellspacing="0" width="65%" border="0">
							<TR>
								<TD colSpan="4"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD>Customer ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD colSpan="2"><asp:label id="LblCustomerID" Width="97px" Runat="server" ForeColor="Blue"></asp:label></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD width="20%">Name</TD>
								<TD colSpan="3" width="80%">
									<asp:TextBox id="lblName" runat="server" Width="100%" BorderStyle="Groove" MaxLength="49" CssClass="dropdownlist" ontextchanged="lblName_TextChanged"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD width="20%">Type&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator1" runat="server" Operator="NotEqual" ValueToCompare="Select"
										ErrorMessage="Please Select Customer Type" ControlToValidate="DropCustType">*</asp:comparevalidator></TD>
								<TD width="20%"><asp:dropdownlist id="DropCustType" runat="server" Width="130px" CssClass="dropdownlist"></asp:dropdownlist></TD>
								<TD align="center" width="20%">&nbsp;&nbsp;&nbsp;&nbsp;Unique&nbsp;Code</TD>
								<TD width="30%"><asp:textbox id="txtcode" runat="server" Width="100%" BorderStyle="Groove" MaxLength="12" CssClass="dropdownlist"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD>Address</TD>
								<TD colSpan="3"><asp:textbox id="txtAddress" runat="server" Width="100%" Height="22px" BorderStyle="Groove" TextMode="SingleLine"
										MaxLength="99" Font-Names="Verdana" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>City&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator2" runat="server" Operator="NotEqual" ValueToCompare="Select"
										ErrorMessage="Please Select City" ControlToValidate="DropCity">*</asp:comparevalidator></TD>
								<TD><asp:dropdownlist id="DropCity" runat="server" Width="130px" onchange="getbeatInfo(this,document.Form1.DropState,document.Form1.DropCountry);"
										AutoPostBack="false" CssClass="dropdownlist" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GSTIN 
									No.&nbsp;</TD>
								<TD><asp:textbox id="txtTinNo" runat="server" Width="100%" BorderStyle="Groove" MaxLength="15" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>District</TD>
								<TD><asp:dropdownlist id="DropState" runat="server" Width="130px" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SSR</TD>
								<TD><asp:DropDownList ID="DropSSR" Runat="server" Width="100%" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
							<TR>
								<TD>State</TD>
								<TD><asp:dropdownlist id="DropCountry" runat="server" Width="130px" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">Contact Person</TD>
								<TD><asp:TextBox ID="txtContactPerson" Runat="server" MaxLength="27" BorderStyle="Groove" CssClass="fontstyle"
										Width="100%"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD>Res.&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ErrorMessage="Contact No. Between 6-10 Digits"
										ControlToValidate="txtPhoneRes" ValidationExpression="\d{6,12}">*</asp:regularexpressionvalidator></TD>
								<TD>Off.&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" ErrorMessage="Contact No. Between 6-10 Digits"
										ControlToValidate="txtPhoneOff" ValidationExpression="\d{6,12}">*</asp:regularexpressionvalidator></TD>
								<TD>Mobile&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Mobile No. Between 10-12 Digits"
										ControlToValidate="txtMobile" ValidationExpression="\d{10,12}">*</asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD>Contact No</TD>
								<TD><asp:textbox id="txtPhoneRes" runat="server" Width="130px" BorderStyle="Groove" MaxLength="12"
										CssClass="dropdownlist" onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
								<TD><asp:textbox id="txtPhoneOff" runat="server" Width="130px" BorderStyle="Groove" MaxLength="12"
										CssClass="dropdownlist" onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
								<TD><asp:textbox id="txtMobile" runat="server" Width="100%" BorderStyle="Groove" MaxLength="12" CssClass="dropdownlist"
										onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>E - Mail&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Please Fill Valid E-mail"
										ControlToValidate="txtEMail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator>&nbsp;</TD>
								<TD colSpan="3"><asp:textbox id="txtEMail" runat="server" Width="100%" BorderStyle="Groove" MaxLength="49" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Credit Limit&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" ErrorMessage="Credit Limit Should be Correct"
										ControlToValidate="txtCRLimit" ValidationExpression="(\d+\.\d+)|(\d+)">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox id="txtCRLimit" runat="server" Width="130px" BorderStyle="Groove" MaxLength="8"
										CssClass="dropdownlist" onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Credit Days</TD>
								<TD><asp:dropdownlist id="DropCrDay" Width="72px" Runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>Op. Balance&nbsp;
									<asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" ErrorMessage="Opening Balance Should be Correct"
										ControlToValidate="txtOpBalance" ValidationExpression="(\d+\.\d+)|(\d+)">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox id="txtOpBalance" runat="server" Width="130px" BorderStyle="Groove" MaxLength="8"
										CssClass="dropdownlist" onkeypress="return GetOnlyNumbers(this, event,false,true);"></asp:textbox></TD>
								<TD><asp:dropdownlist id="DropBal" Runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="Cr.">Cr.</asp:ListItem>
										<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4">
									<HR width="100%" color="#000099" SIZE="1">
									<asp:button id="btnUpdate" runat="server" Width="90px" 
										 Text="Update" onclick="btnUpdate_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
