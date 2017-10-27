<%@ Page language="c#" Inherits="Servosms.Module.Master.BeatMaster_Entry" CodeFile="BeatMaster_Entry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: BeatMaster Entry</title> <!--
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
		<script language="javascript">
		function  check1()
		{
		alert("hello");
		return false;
		}
		</script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TR valign="top">
					<TH style="HEIGHT: 54px" align="center">
						<font color="#ce4848">Beat&nbsp;Entry</font>
						<hr>
      </TD></TR>
				<tr>
					<td align="center" valign=top>
						<TABLE>
							<TR>
								<TD colSpan="2"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR><TD>Beat No.&nbsp;<asp:RequiredFieldValidator ID="rfv1" Runat="server" ControlToValidate="DropBeatNo" ErrorMessage="Please Select the Beat">*</asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:label id="lblBeatNo" runat="server" ForeColor="Blue" Width="152px" CssClass="Fontstyle"></asp:label><asp:dropdownlist id="DropBeatNo" runat="server" Width="160px" AutoPostBack="True" Visible="False"
										CssClass="fontStyle" onselectedindexchanged="DropBeatNo_SelectedIndexChanged"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>City&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtCity" ErrorMessage="Please Fill City">*</asp:requiredfieldvalidator></TD>
								<TD><asp:textbox id="txtCity" runat="server" Width="160px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>District</TD>
								<TD><asp:textbox id="txtState" runat="server" Width="160px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>State&nbsp;</TD>
								<TD><asp:textbox id="txtCountry" runat="server" Width="160px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2">
									<HR width="100%" color="#000099" SIZE="1">
									<asp:button id="btnSave" runat="server" Width="60px" CausesValidation="true" Text="Add" onclick="btnSave_Click"></asp:button>&nbsp;<asp:button id="btnEdit" runat="server" Width="60px" CausesValidation="False" Text="Edit" 
										 onclick="btnEdit_Click"></asp:button><asp:button id="Edit1" runat="server" Width="61px" Text="Edit" onclick="Edit1_Click"></asp:button>&nbsp;<asp:button id="btnDelete" runat="server" Width="60px" CausesValidation="False" Text="Delete"
										 onclick="btnDelete_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
