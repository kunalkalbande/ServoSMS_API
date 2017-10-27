<%@ Page language="c#" Inherits="Servosms.Module.Master.CustomerVehicleEntry" CodeFile="CustomerVehicleEntry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Vehicle Entry</title> <!--
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
		<LINK href="../../Sysitem/Styles.css" rel="stylesheet">
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
		<form id="Shift_Entry" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:TextBox>
			<table height="274" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Customer Vehicle&nbsp;Entry</font>
						<hr>
					</TH>
				</TR>
				<TR>
					<td align="center">
						<TABLE style="WIDTH: 512px; HEIGHT: 283px">
							<TR>
								<TD colSpan="1" style="WIDTH: 229px"><FONT color="black">CVE ID :</FONT></TD>
								<TD colSpan="1" style="WIDTH: 229px"><FONT color="#ff0000">
										<asp:Label id="lblID" runat="server" Width="52px" ForeColor="Blue">1001</asp:Label>
										<asp:DropDownList id="DropID" runat="server" Visible="False" AutoPostBack="True" onselectedindexchanged="DropID_SelectedIndexChanged">
											<asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
										</asp:DropDownList>
										<asp:Button id="btnEdit" runat="server" Text="..." ToolTip="Click here to edit" 
											 onclick="btnEdit_Click"></asp:Button></FONT></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px; HEIGHT: 9px">Customer Name :</TD>
								<TD colspan="3" style="WIDTH: 153px; HEIGHT: 9px">
									<asp:dropdownlist id="DropCustomerName" runat="server" Width="234px">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px">Vehicle No. 1&nbsp;</TD>
								<TD style="WIDTH: 153px"><asp:textbox id="txtVehicle1" runat="server" BorderStyle="Groove"></asp:textbox></TD>
								<TD style="WIDTH: 271px">Vehicle No. 2&nbsp;</TD>
								<TD><asp:textbox id="txtVehicle2" runat="server" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px">Vehicle No. 3</TD>
								<TD style="WIDTH: 153px"><asp:textbox id="txtVehicle3" runat="server" BorderStyle="Groove" MaxLength="20"></asp:textbox></TD>
								<TD style="WIDTH: 271px">Vehicle No. 4</TD>
								<TD><asp:textbox id="txtVehicle4" runat="server" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px">Vehicle No. 5</TD>
								<TD style="WIDTH: 153px">
									<asp:textbox id="txtVehicle5" runat="server" BorderStyle="Groove" MaxLength="20"></asp:textbox></TD>
								<TD style="WIDTH: 271px">Vehicle No.&nbsp;6&nbsp;</TD>
								<TD><asp:textbox id="txtVehicle6" runat="server" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px">Vehicle No. 7</TD>
								<TD style="WIDTH: 153px">
									<asp:textbox id="txtVehicle7" runat="server" BorderStyle="Groove" MaxLength="20"></asp:textbox></TD>
								<TD style="WIDTH: 271px">Vehicle No.&nbsp;8&nbsp;</TD>
								<TD><asp:textbox id="txtVehicle8" runat="server" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 84px">Vehicle No. 9</TD>
								<TD style="WIDTH: 153px">
									<asp:textbox id="txtVehicle9" runat="server" BorderStyle="Groove" MaxLength="20"></asp:textbox></TD>
								<TD style="WIDTH: 271px">Vehicle No. 10&nbsp;</TD>
								<TD><asp:textbox id="txtVehicle10" runat="server" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="4">
									<HR color="#000099" SIZE="1">
									<asp:button id="btnSave" runat="server" Width="60px" Text="Save" 
										 onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button id="btnUpdate" runat="server" Width="60px" Text="Edit" 
										 onclick="btnUpdate_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button id="btnDelete" runat="server" Text="Delete" 
										 Width="60px" onclick="btnDelete_Click"></asp:Button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" Height="4px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
