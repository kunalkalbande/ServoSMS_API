<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Logistics.Vehicle" CodeFile="Vehicle.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Vehicle Category</title><!--
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
			<table height="232" width="778" align="center">
				<TR>
					<TH align="center">
						<FONT color="#ce4848">Vehicle Category</FONT>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE style="WIDTH: 329px; HEIGHT: 93px">
							<TBODY>
								<TR>
									<TD style="WIDTH: 107px; HEIGHT: 24px">Vehicle Category
									</TD>
									<TD style="HEIGHT: 24px"><asp:dropdownlist id="Dropvech" runat="server" AutoPostBack="True" Width="104px" CssClass="fontstyle" onselectedindexchanged="Dropvech_SelectedIndexChanged"></asp:dropdownlist></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 107px; HEIGHT: 14px">Add New</TD>
									<TD style="HEIGHT: 14px"><asp:textbox id="txtveccategory" runat="server" Width="102px" CssClass="fontstyle" BorderStyle="Groove"></asp:textbox></TD>
								</TR>
								<TR>
									<TD colSpan="3" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="btnaddnew" runat="server" Width="50px" Text="Add  " BackColor="#CE4848" BorderColor="#CE4848"
											ForeColor="White" onclick="btnaddnew_Click"></asp:button><asp:button id="btnEdit" runat="server" Width="50px" Text="Edit" BackColor="#CE4848" BorderColor="#CE4848"
											ForeColor="White" onclick="btnEdit_Click"></asp:button><asp:button id="btneditsave" runat="server" Text="Edit  " BackColor="#CE4848" BorderColor="#CE4848"
											ForeColor="White" Width="50px" onclick="btneditsave_Click"></asp:button><asp:button id="btnDel" runat="server" Width="60px" Text="Delete" BackColor="#CE4848" BorderColor="#CE4848"
											ForeColor="White" onclick="btnDel_Click"></asp:button></TD>
								</TR>
							</TBODY>
						</TABLE>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
