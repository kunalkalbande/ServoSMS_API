<%@ Reference Page="~/Module/Admin/Privileges.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Admin.ModuleManagement.ModuleManage" CodeFile="ModuleManage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Module Management</title> 
		<!--
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
		<LINK href="../../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
				<tr height="10">
					<th align="center" colSpan="2">
						<font color="#ce4848">Module Management</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top" height="20">
					<td width="40%"></td>
					<td width="60%"><asp:checkbox id="chkStock" Text="Stock Updation" Runat="server"></asp:checkbox></td>
				</tr>
				<tr vAlign="top">
					<td width="40%"></td>
					<td width="60%"><asp:checkbox id="chkCustBal" Text="Customer Balance Updation" Runat="server"></asp:checkbox></td>
				</tr>
				<tr>
					<td align="center" colSpan="2"><asp:button id="btnUpdate" runat="server" Text="Update Management" onclick="btnUpdate_Click"></asp:button></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
