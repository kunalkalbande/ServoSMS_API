<%@ Page language="c#" Inherits="Servosms.Module.Master.MarketCustEntry" CodeFile="MarketCustEntry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Market Customer Entry</title><!--
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Market Customer Entry</font>
						<hr>
					</TH>
				</TR>
				<TR>
					<td align="center">
						<TABLE id="AutoNumber1" cellSpacing="0" cellPadding="0" width="80%" align="center" border="0"
							rules="groups">
							<TBODY>
								<tr>
									<td colSpan="1">&nbsp;MC ID</td>
									<td colSpan="7"><asp:Label id="txtid" runat="server" ForeColor=blue>Label</asp:Label><asp:dropdownlist id="dropid" runat="server" Width="80" AutoPostBack="True" Visible="False" CssClass="dropdownlist" onselectedindexchanged="dropid_SelectedIndexChanged"></asp:dropdownlist><asp:button id="btnselect" runat="server" Text="select" 
											    onclick="btnselect_Click"></asp:button>
									</td>
								</tr>
								<tr>
									<td colSpan="1">&nbsp;Firm Name</td>
									<td colSpan="7"><asp:textbox id="txtfname" runat="server" Width="150px" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></td>
								</tr>
								<tr>
									<td>&nbsp;Type:</td>
									<td><asp:dropdownlist id="droptype" runat="server" Width="136px" CssClass="dropdownlist" onselectedindexchanged="droptype_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist></td>
									<td align="right" colspan="6"></td>
						&nbsp;
					</td>
				</TR>
				<tr>
					<td>&nbsp;Tele No:
						<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txttelno" ErrorMessage="Contact No. Between 6-10 Digits"
							ValidationExpression="\d{6,11}">*</asp:regularexpressionvalidator></td>
					<td colSpan="7"><asp:textbox id="txttelno" onkeypress="return GetOnlyNumbers(this, event, true,false);" runat="server"
							Width="150px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="11"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Contact Person</td>
					<td colSpan="7"><asp:textbox id="txtcontact" runat="server" Width="150px" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Specific Detail</td>
					<td colSpan="7"><asp:textbox id="txtdetail" runat="server" Width="150px" BorderStyle="Groove" CssClass="dropdownlist" ontextchanged="txtdetail_TextChanged"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Regular Customer</td>
					<td colSpan="7"><asp:dropdownlist id="dropregcust" runat="server" Width="64px" Height="24px" CssClass="dropdownlist" onselectedindexchanged="dropregcust_SelectedIndexChanged">
							<asp:ListItem Value="Select">Select</asp:ListItem>
							<asp:ListItem Value="YES">YES</asp:ListItem>
							<asp:ListItem Value="NO">NO</asp:ListItem>
						</asp:dropdownlist>
						&nbsp;&nbsp;&nbsp;&nbsp;Place&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="dropplace" Runat="server" Width="150" CssClass="dropdownlist"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td>&nbsp;Potential(Total)</td>
					<td colSpan="1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtpot" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;Servo</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtservo" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;Castrol</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtcastrol" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>Shell&nbsp;</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtshell" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;BPCL</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtbpcl" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;Veedol</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtveedol" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;ELF</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtelf" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;HPCL</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txthpcl" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
				</tr>
				<tr>
					<td>&nbsp;Pennzoil</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtpennzoil" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;Spurious</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtspurious" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td>&nbsp;Others</td>
					<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtother" 
							BorderStyle="Groove" Runat="server" CssClass="dropdownlist" MaxLength="9"></asp:textbox></td>
					<td colspan="2"></td>
				</tr>
				<tr>
					<td align="center" colSpan="8"><asp:button id="btnadd" runat="server" Width="76px" Text="Add" Height="24px" 
							  onclick="btnadd_Click"></asp:button><asp:button id="btnedit" runat="server" Width="76px" Text="Edit" Height="24px" 
							  onclick="btnedit_Click"></asp:button><asp:button id="btndelete" runat="server" Width="76" Text="Delete" Height="24" 
							  onclick="btndelete_Click"></asp:button></td>
				</tr>
			</table></TD></TR></TBODY></TABLE>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
