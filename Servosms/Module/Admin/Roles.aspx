<%@ Reference Page="~/Module/Admin/Privileges.aspx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Admin.Roles" CodeFile="Roles.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Roles</title> <!--
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			<table height="290" width="778" align="center" border="0">
				<TR valign="top">
					<TH align="center">
						<font color="#ce4848">Roles</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE>
							<TR>
								<TD>Role&nbsp;ID&nbsp;<asp:RequiredFieldValidator id="Requiredfieldvalidator2" runat="server" ErrorMessage="Please Select The Role ID"
										ControlToValidate="dropRoleID" InitialValue="Select">*</asp:RequiredFieldValidator></TD>
								<TD><asp:dropdownlist id="dropRoleID" runat="server" Width="60px" AutoPostBack="True" Visible="False"
										CssClass="fontstyle" onselectedindexchanged="dropRoleID_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist><asp:label id="lblRoleID" ForeColor="Blue" Width="100px" Runat="server"></asp:label>
									<asp:button id="btnEdit" runat="server" Width="22px" Text="..." ToolTip="Click For Edit" CausesValidation="False"
										 onclick="btnEdit_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD>Role Name
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please Fill the Role Name"
										ControlToValidate="txtRoleName">*</asp:RequiredFieldValidator></TD>
								<TD><asp:textbox id="txtRoleName" runat="server" Width="125px" BorderStyle="Groove" onkeypress="return GetOnlyChars(this, event);"
										MaxLength="30" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Description&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;</TD>
								<TD colSpan="2"><asp:textbox id="txtDesc" runat="server" Width="115px" BorderStyle="Groove" TextMode="MultiLine"
										CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4">
									<asp:button id="btnUpdate" runat="server" Width="70px" Text="Save" onclick="btnUpdate_Click"></asp:button>
									<asp:button id="btnDelete" runat="server" Width="70px" Text="Delete" onclick="btnDelete_Click"></asp:button></TD>
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
