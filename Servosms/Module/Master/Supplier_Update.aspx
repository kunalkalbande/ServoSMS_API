<%@ Page language="c#" Inherits="Servosms.Module.Master.Supplier_Update_aspx" CodeFile="Supplier_Update.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Vendor Update</title> <!--
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
		<script id="Validations" language="javascript" src="../../Sysitem/JS/Validations.js"></script>
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
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<asp:TextBox id="txtbeatname" Runat="server" Width="1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"></asp:TextBox>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" runat="server"
				Width="1px" Visible="False"></asp:TextBox>
			<asp:TextBox id="TempVenderName" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"
				runat="server" Width="1px" Visible="False"></asp:TextBox>
			<table width="778" height="290" align="center" cellpadding="0" cellspacing="0" border="0">
				<TR>
					<TD></TD>
					<TH align="center">
						<font color="#CE4848">Update Vendor</font><hr>
					</TH>
				</TR>
				<tr>
					<td></td>
					<td align="center" valign="top">
						<TABLE cellpadding="0" cellspacing="0" border="0">
							<TR>
								<TD>&nbsp;Vendor&nbsp;ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD colSpan="2"><asp:label id="lblSupplierID" Width="166px" ForeColor="Blue" Runat="server"></asp:label></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>&nbsp;Name</TD>
								<TD colSpan="3">
									<asp:TextBox id="lblName" runat="server" Width="100%" BorderStyle="Groove" CssClass="DropDownList"
										MaxLength="49"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Type&nbsp;<FONT color="#ff0000">*</FONT>
									<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Please Select Customer Type"
										ControlToValidate="DropSuppType" Operator="NotEqual" ValueToCompare="Select">*</asp:CompareValidator></TD>
								<TD><asp:dropdownlist id="DropSuppType" runat="server" Width="132px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Lubricants">Lubricants</asp:ListItem>
										<asp:ListItem Value="Misc.">Misc.</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; GSTin No.&nbsp;<FONT color="#ff0033">*</FONT>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtTinNo" ErrorMessage="Please enter Tin No.">*</asp:RequiredFieldValidator></TD>
								<TD>
									<asp:textbox id="txtTinNo" runat="server" Width="130px" BorderStyle="Groove" MaxLength="15" CssClass="DropDownList"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;City <FONT color="#ff0000">*</FONT> &nbsp;
									<asp:CompareValidator id="CompareValidator2" runat="server" ErrorMessage="Please Select City" ControlToValidate="DropCity"
										Operator="NotEqual" ValueToCompare="Select">*</asp:CompareValidator></TD>
								<TD><asp:dropdownlist id="DropCity" runat="server" Width="130px" AutoPostBack="false" onchange="getbeatInfo(this,document.Form1.DropState,document.Form1.DropCountry);"
										CssClass="DropDownList" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select ">Select </asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;Residance<asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneRes"
										ErrorMessage="Contact No. Between 6-12 Digits" ValidationExpression="\d{6,12}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox id="txtPhoneRes" runat="server" Width="131" BorderStyle="Groove" MaxLength="12"
										CssClass="DropDownList" onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									&nbsp;District</TD>
								<TD><asp:dropdownlist id="DropState" runat="server" Width="130px" CssClass="DropDownList" onselectedindexchanged="DropState_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;Office<asp:RegularExpressionValidator id="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhoneOff"
										ErrorMessage="Contact No. Between 6-12 Digits" ValidationExpression="\d{6,12}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox id="txtPhoneOff" runat="server" Width="131" BorderStyle="Groove" MaxLength="12"
										CssClass="DropDownList" onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;State</TD>
								<TD><asp:dropdownlist id="DropCountry" runat="server" Width="130px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;Mobile<asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile No. Between 6-10 Digits"
										ValidationExpression="\d{10,12}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox id="txtMobile" runat="server" Width="131px" BorderStyle="Groove" MaxLength="12"
										CssClass="DropDownList" onkeypress="return GetOnlyNumbers(this, event);"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Address</TD>
								<TD colSpan="3"><asp:textbox id="txtAddress" runat="server" Width="100%" BorderStyle="Groove" Font-Names="Verdana"
										CssClass="DropDownList" MaxLength="99"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Op.Balance&nbsp;
									<asp:RegularExpressionValidator id="RegularExpressionValidator5" runat="server" ControlToValidate="txtOpBalance"
										ErrorMessage="Opening Balance Should be Correct" ValidationExpression="(\d+\.\d+)|(\d+)">*</asp:RegularExpressionValidator></TD>
								<TD><asp:textbox id="txtOpBalance" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownList"
										MaxLength="8" onkeypress="return GetOnlyNumbers(this, event,false,true);"></asp:textbox></TD>
								<TD>
									<asp:DropDownList id="DropBal" Runat="server" CssClass="DropDownList">
										<asp:ListItem Value="Cr.">Cr.</asp:ListItem>
										<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
									</asp:DropDownList>&nbsp;&nbsp;&nbsp;Credit Days&nbsp;&nbsp;&nbsp;</TD>
								<TD>
									<asp:dropdownlist id="DropCrDay" Runat="server" Width="72px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD colspan="1">
									&nbsp;E - Mail&nbsp;
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Please Fill Valid E-mail"
										ControlToValidate="txtEMail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></TD>
								<TD colSpan="2"><asp:textbox id="txtEMail" runat="server" Width="100%" BorderStyle="Groove" CssClass="DropDownList"
										MaxLength="49"></asp:textbox></TD>
								<TD align="right">
									<asp:button id="btnUpdate" runat="server" Width="90px" Text="Update" 
										 onclick="btnUpdate_Click"></asp:button>
								</TD>
							</TR>
						</TABLE>
						<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
					</td>
					<td></td>
				</tr>
			</table>
			<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
