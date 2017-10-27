<%@ Page language="c#" Inherits="Servosms.Module.Logistics.Routeedit" CodeFile="Routeedit.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Route Insertion</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="True" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		<form method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TR valign=top>
					<TH align="center">
						<FONT color="#ce4848">Route Insertion</FONT>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE style="WIDTH: 331px; HEIGHT: 60px">
							<TR>
								<TD id="r1">Route Name&nbsp;<asp:RequiredFieldValidator ID="Requiredfieldvalidator2" Runat="server" ControlToValidate="DropDownList1" ErrorMessage="Please Select Route"
										InitialValue="Select">*</asp:RequiredFieldValidator></TD>
								<TD><asp:dropdownlist id="DropDownList1" runat="server" AutoPostBack="True" Width="170px" CssClass="fontstyle" onselectedindexchanged="DropDownList1_SelectedIndexChanged"></asp:dropdownlist><asp:Label ID="lblRouteID" Runat="server"></asp:Label>&nbsp;&nbsp;<asp:button id="btnEdit" runat="server" Width="20px" Height="20px" Text="..." 
										              CausesValidation="False" onclick="btnEdit_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD id="r2">Route Name&nbsp;<asp:RequiredFieldValidator ID="rfv1" Runat="server" ControlToValidate="txtrname" ErrorMessage="Please Enter Route Name">*</asp:RequiredFieldValidator></TD>
								<TD><asp:textbox id="txtrname" runat="server" Width="170px" MaxLength="49" CssClass="fontstyle" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD id="r3">Route KM&nbsp;<asp:RequiredFieldValidator ID="Requiredfieldvalidator1" Runat="server" ControlToValidate="txtrkm" ErrorMessage="Please Enter Route KM">*</asp:RequiredFieldValidator></TD>
								<TD><asp:textbox id="txtrkm" runat="server" Width="170px" MaxLength="15" CssClass="fontstyle" BorderStyle="Groove" ontextchanged="txtrkm_TextChanged"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="6"><asp:button id="Button1" runat="server" Width="60px" Text="Add " 
										 onclick="Button1_Click"></asp:button><asp:button id="btnsave" runat="server" Width="60px" Text="Edit " 
										 onclick="btnsave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnDel" runat="server" Text="Delete" 
										Width="60px" onclick="btnDel_Click"></asp:button></TD>
							</TR>
							<tr>
								<td vAlign="middle" align="center" colSpan="2"></td>
							</tr>
						</TABLE>
						<P><asp:validationsummary id="vsRoute" runat="server" Width="158px" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></P>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
