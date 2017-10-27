<%@ Reference Page="~/Module/Admin/Privileges.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ import namespace="RMG" %>
<%@ Page language="c#" Inherits="Servosms.Module.Admin.BackupRestore" CodeFile="BackupRestore.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Backup & Restore</title>
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
		
	function Check(t)
	{
		var str=t.value
		if(t.value!="" && str.indexOf(".")>0)
		{
			str=str.substring(0,str.lastIndexOf("."));
			//if(t.value.indexOf(".LDF")>0)
			if(t.value.toLowerCase().indexOf("servosms.bak")>0)
			{
				document.Form1.btnRestore.disabled=false;
				document.Form1.tempPath.value=str;
			}
			else
			{
				alert("sdfPlease Select Appropriate 'ServoSMS.bak' File");
				document.Form1.btnRestore.disabled=true;
				return;
			}
		}
		else
		{
			//alert("Please Select The 'LDF' File");
			alert("Please Select The 'bak' File");
			document.Form1.btnRestore.disabled=false;
			return;
		}
	}
	//var count=0;
	function Progressbar()
	{
		document.Form1.lblPro.style.visibility="visible"
		document.Form1.lblPro.style.font.bold=true
	}
		</script> <!--
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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server" DESIGNTIMEDRAGDROP="11"></uc1:header>
			<input type="hidden" id="tempPath" name="tempPath" runat="server">
			<table height="260" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<tr>
					<TH align="center" height="20">
						<font color="#ce4848">Backup and Restore</font><hr>
					</TH>
				</tr>
				<tr>
					<td>
						<p align="center"><br>
							<input type="button" NAME="lblPro" style="BORDER-RIGHT: palegoldenrod thin; TABLE-LAYOUT: auto; BORDER-TOP: palegoldenrod thin; DISPLAY: block; FONT-WEIGHT: bold; VISIBILITY: hidden; BORDER-LEFT: palegoldenrod thin; WIDTH: 200px; CURSOR: wait; COLOR: firebrick; DIRECTION: ltr; TEXT-INDENT: 25px; BORDER-BOTTOM: palegoldenrod thin; BORDER-COLLAPSE: collapse; HEIGHT: 30px; BACKGROUND-COLOR: palegoldenrod"
								value="Processing Please Wait..."></p>
						<P align="center"><FONT size="5" color="#00ffff"><br>
								<br>
								<INPUT id="ff1" style="BORDER-TOP-STYLE: groove;BORDER-RIGHT-STYLE: groove;BORDER-LEFT-STYLE: groove;BORDER-BOTTOM-STYLE: groove"
									type="file" onchange="Check(this);" size="70" name="ff1">&nbsp;<asp:button id="btnRestore" runat="server" Width="90px"  Text="Restore" onclick="btnRestore_Click"></asp:button>&nbsp;<asp:button id="btnBackup" runat="server" Width="90px"  Text="Backup" onclick="btnBackup_Click"></asp:button></FONT></P>
						<br>
						<br>
						<P align="left"><FONT size="5"><FONT color="black" size="2"><STRONG>&nbsp;&nbsp;&nbsp;<U>Remark:</U></STRONG>
									The backup process stores the copy of your data&nbsp;in the home drive in a 
									folder <STRONG>ServoSMSBackup</STRONG>.<br>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									For example if your home drive is C: then your backup is copied in 
									C:\ServoSMSBackup folder.&nbsp;</FONT></P>
						</FONT></FONT></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
