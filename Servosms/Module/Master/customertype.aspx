<%@ Page language="c#" Inherits="Servosms.Module.Master.customertype" CodeFile="customertype.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS : Customer Type</title><!--
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
			
			function setOther(t)
			{
				var name=t.name;
				var typeindex = t.selectedIndex
				var typetext  = t.options[typeindex].text
				if(name=='dropGroup')
				{
					if(typetext  == "Other")
					{
						document.Form1.txtGroup.disabled = false;
					}
					else
					{
						document.Form1.txtGroup.disabled = true;
						document.Form1.txtGroup.value = "";
					}
				}
				else if(name=='dropSGroup')
				{
					if(typetext  == "Other")
					{
						document.Form1.txtSGroup.disabled = false;
					}
					else
					{
						document.Form1.txtSGroup.disabled = true;
						document.Form1.txtSGroup.value = "";
					}
				}
			}
		</script>
</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="770" align="center">
				<tr>
					<th style="HEIGHT: 30px" borderColor="limegreen" colSpan="1" rowSpan="1" noWrap>
						<b><font color="#ce4848">Customer Type Entry</font></b></B><hr>
					</th>
				</tr>
				<TR>
					<td>
						<table width="550" align="center" border="0" rules="groups">
							<tr>
								<td align="center" colspan="3">
								</td>
							<tr>
								<td>Customer TypeID</td>
								<td colSpan="2"><asp:dropdownlist id="dropid" runat="server" Width="115px" Visible="False" AutoPostBack="True" CssClass="FontStyle" onselectedindexchanged="dropid_SelectedIndexChanged"></asp:dropdownlist>
									<asp:TextBox id="txtid" runat="server"  BorderStyle="Groove" ReadOnly="True" CssClass="FontStyle"></asp:TextBox></td>
							</tr>
							<tr>
								<td style="HEIGHT: 21px">Customer Type Name
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Not Blank" ControlToValidate="txtname">*</asp:RequiredFieldValidator></td>
								<td colSpan="2" style="HEIGHT: 21px">
									<asp:textbox id="txtname" runat="server" BorderStyle="Groove" CssClass="FontStyle"></asp:textbox></td>
							</tr>
							<tr>
								<td>Group Name <font color="#ff0000"></font>
								</td>
								<td><asp:DropDownList ID="dropGroup" Runat="server" CssClass="FontStyle" Width=70px onChange="setOther(this)"></asp:DropDownList><FONT color="#0000ff">&nbsp;&nbsp;&nbsp;&nbsp;(if 
										another, Specify)</FONT>&nbsp;
									<asp:textbox id="txtGroup" runat="server" Width="110px" MaxLength="20" CssClass="DropDownList"
										BorderStyle="Groove"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td>SubGroup Name <font color="#ff0000"></font>
								</td>
								<td><asp:DropDownList ID="dropSGroup" Runat="server" CssClass="FontStyle" Width="110px" onChange="setOther(this)"></asp:DropDownList><FONT color="#0000ff">&nbsp;&nbsp;&nbsp;&nbsp;(if 
										another, Specify)</FONT>&nbsp;
									<asp:textbox id="txtSGroup" runat="server" Width="110px" MaxLength="20" CssClass="DropDownList"
										BorderStyle="Groove"></asp:textbox>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="3">&nbsp;
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="3">&nbsp;
									<asp:button id="btnAdd" runat="server" Text="Add" Height="24px" Width="60px" onclick="btnAdd_Click"></asp:button>
									<asp:button id="btnEdit" runat="server" Text="Edit" Height="24px" Width="60px" CausesValidation="False"
										 onclick="btnEdit_Click"></asp:button>
									<asp:button id="btnDelete" runat="server" Text="Delete" Height="24" Width="60px" CausesValidation="False"
										Enabled="False" onclick="btnDelete_Click"></asp:button>
								</td>
							</tr>
						</table>
						<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
							Height="32px"></asp:ValidationSummary>
					</td>
					<td>&nbsp;</td>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
