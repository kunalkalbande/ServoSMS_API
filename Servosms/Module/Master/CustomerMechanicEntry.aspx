<%@ Page language="c#" Inherits="Servosms.Module.Master.CustomerMechanicEntry" CodeFile="CustomerMechanicEntry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Customer Mechanic Entry</title><!--
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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox>
			<table height="274" width="778" align="center" cellpadding="0" cellspacing="0">
				<TR>
					<TH align="center">
						<font color="#ce4848">Customer Mechanic Entry</font>
						<hr>
					</TH>
				</TR>
				<TR>
					<td align="center">
						<TABLE cellSpacing="0" cellPadding="0" width="600">
							<TR>
								<TD colSpan="1"><FONT color="black">&nbsp; Customer&nbsp;ID :</FONT></TD>
								<TD colSpan="5"><FONT color="#ff0000"><asp:dropdownlist id="DropID" runat="server" Visible="False" AutoPostBack="True" CssClass="FontStyle" Width="100px" onselectedindexchanged="DropID_SelectedIndexChanged">
<asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
										</asp:dropdownlist>
										<asp:Label id="txtcustid" runat="server" ForeColor="blue" CssClass="FontStyle" Width="100px">Label</asp:Label><asp:button id="btnselect" runat="server" Width="20px" ToolTip="Click here to edit" Text="..."
											 Height="20px" onclick="btnselect_Click"></asp:button></FONT></TD>
							</TR>
							<TR>
								<TD colSpan="1">&nbsp;&nbsp;Customer&nbsp;&nbsp;Name :</TD>
								<TD colSpan="5"><asp:dropdownlist id="DropCustomerName" runat="server" Width="234px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<td align="center"><asp:label id="mccd" Visible="False" Runat="server">Mechanic Code</asp:label></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;1.MechanicName</TD>
								<TD><asp:textbox id="txtname1" runat="server" Width="152px"  BorderStyle="Groove"
										CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp;&nbsp;Type&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:dropdownlist id="Droptype1" runat="server" Width="105px"  Height="22px"
										CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp;&nbsp;City&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:dropdownlist id="Dropcity1" runat="server" Width="120px"  CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd1" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;2.MechanicName</TD>
								<TD><asp:textbox id="txtname2" runat="server" Width="152px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype2" runat="server" Width="105" Height="22" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp;&nbsp;City</TD>
								<TD><asp:dropdownlist id="Dropcity2" runat="server" Width="120px" CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd2" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;3.MechanicName</TD>
								<TD><asp:textbox id="txtname3" runat="server" Width="152px"  BorderStyle="Groove"
										CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype3" runat="server" Width="104px"  CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp;&nbsp;City</TD>
								<TD><asp:dropdownlist id="Dropcity3" runat="server" Width="120px"  CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd3" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;4.MechanicName</TD>
								<TD><asp:textbox id="txtname4" runat="server" Width="152px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype4" runat="server" Width="104px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity4" runat="server" Width="120px" CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd4" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;5.MechanicName</TD>
								<TD><asp:textbox id="txtname5" runat="server" Width="152px"  BorderStyle="Groove"
										CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype5" runat="server" Width="104px"  CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity5" runat="server" Width="120px"  CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd5" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;6.MechanicName</TD>
								<TD><asp:textbox id="txtname6" runat="server" Width="152px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype6" runat="server" Width="104px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity6" runat="server" Width="120px" CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd6" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;7.MechanicName</TD>
								<TD><asp:textbox id="txtname7" runat="server" Width="152px"  BorderStyle="Groove"
										CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype7" runat="server" Width="104px"  CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity7" runat="server" Width="120px"  CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd7" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;8.MechanicName</TD>
								<TD><asp:textbox id="txtname8" runat="server" Width="152px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype8" runat="server" Width="104px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity8" runat="server" Width="120px" CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd8" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;9.MechanicName</TD>
								<TD><asp:textbox id="txtname9" runat="server" Width="152px"  BorderStyle="Groove"
										CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype9" runat="server" Width="104px"  CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity9" runat="server" Width="122"  Height="22" CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd9" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD>&nbsp;&nbsp;10.MechanicName&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtname10" runat="server" Width="152px" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></TD>
								<TD>&nbsp;&nbsp; Type</TD>
								<TD><asp:dropdownlist id="Droptype10" runat="server" Width="104px" CssClass="FontStyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="4 Wheeler">4 Wheeler</asp:ListItem>
										<asp:ListItem Value="Car">Car</asp:ListItem>
										<asp:ListItem Value="HCV">HCV</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="LCV">LCV</asp:ListItem>
										<asp:ListItem Value="PumpSets">PumpSets</asp:ListItem>
										<asp:ListItem Value="Tractor">Tractor</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;&nbsp; City</TD>
								<TD><asp:dropdownlist id="Dropcity10" runat="server" Width="122px" Height="22px" CssClass="FontStyle"></asp:dropdownlist></TD>
								<td><asp:textbox id="mccd10" Visible="False" Width="80" ReadOnly="True" BorderStyle="Groove" Runat="server"
										CssClass="FontStyle"></asp:textbox></td>
							</TR>
							<TR>
								<TD align="center" colSpan="7">
									<asp:button id="btnadd" runat="server" Width="60px" Text="Add" onclick="btnadd_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnedit" runat="server" Width="60px" Text="Edit" onclick="btnedit_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnDelete" runat="server" Text="Delete" Enabled="False" Width="60px" onclick="btnDelete_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" Height="81px" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
