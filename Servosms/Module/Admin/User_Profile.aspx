<%@ Reference Page="~/Module/Admin/Roles.aspx" %>
<%@ Reference Page="~/Module/Admin/Privileges.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Admin.User_Profile" CodeFile="User_Profile.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: User Profile</title> <!--
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
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--<script language="javascript" id="Validations" src="Validations.js"></script>-->
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
			<table height="290" width="778" align="center">
				<TR valign="top">
					<TH align="center">
						<font color="#ce4848">User Profile</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE>
							<TR>
								<TD>User&nbsp;ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD colSpan="3">
									<asp:dropdownlist id="dropUserID" runat="server" Width="60px" AutoPostBack="True" Visible="False"
										CssClass="FontStyle" onselectedindexchanged="dropUserID_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist>
									<asp:label id="lblUserID" Runat="server" ForeColor="Blue" Width="100px"></asp:label>
									<asp:button id="btnEdit" runat="server" Width="25px" Text="..." ToolTip="Click For Edit" CausesValidation="False"
										 onclick="btnEdit_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD>Login Name&nbsp;
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please Fill the Login Name"
										ControlToValidate="txtLoginName">*</asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</TD>
								<TD>
									<asp:textbox id="txtLoginName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="20"
										CssClass="FontStyle"></asp:textbox></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>Password
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please Fill the Password"
										ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassword"
										ErrorMessage="Minimum 5 Maximum 30 characters allowed" ValidationExpression="\w{5,30}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox id="txtPassword" runat="server" TextMode="Password" Width="130px" BorderStyle="Groove"
										MaxLength="30" CssClass="FontStyle"></asp:textbox></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD align="center">First Name</TD>
								<TD>Middle Name</TD>
								<TD align="center">Last Name</TD>
							</TR>
							<TR>
								<TD>Name
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Please Fill the User Name"
										ControlToValidate="txtFName">*</asp:RequiredFieldValidator></TD>
								<TD><asp:textbox id="txtFName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="15" CssClass="FontStyle"></asp:textbox></TD>
								<TD><asp:textbox id="txtMName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="10" CssClass="FontStyle"></asp:textbox></TD>
								<TD><asp:textbox id="txtLName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="20" CssClass="FontStyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Role&nbsp;
									<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Please Select the Role Name"
										ControlToValidate="DropRole" Operator="NotEqual" ValueToCompare="Select">*</asp:CompareValidator></TD>
								<TD colspan="3"><asp:dropdownlist id="DropRole" runat="server" Width="120px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4">
									<asp:button id="btnUpdate" runat="server" Width="90px" Text="Save Profile" onclick="btnUpdate_Click"></asp:button>
									<asp:button id="btnDelete" runat="server" Width="80px" Text="Delete" CausesValidation="False"
										 onclick="btnDelete_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
