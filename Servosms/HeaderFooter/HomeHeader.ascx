<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeHeader.ascx.cs" Inherits="HeaderFooter_HomeHeader" %>
<HTML>
	<HEAD>
		<title>Untitled Document</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<LINK href="images1/style.css" type="text/css" rel="stylesheet">
		<LINK href="../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<table cellSpacing="0" cellPadding="0" border="0" width="1350" align="center">
			<tr>
				<td width="1350" height="75"><div style="LEFT: 0px; POSITION: relative; TOP: 0px">
						<IMG width="1350" src="/Servosms/HeaderFooter/images/eLDMS Header.png" height="75"></DIVS></div>
				</td>
			<tr>
				<td>
					<table cellSpacing="0" cellPadding="0" border="0" width="1350">
						<TR>
							<TD bordercolor="#252787" background="/Servosms/HeaderFooter/images/Nav.jpg" colSpan="2"
								height="25" width="779">
								<SCRIPT src="/Servosms/HeaderFooter/images/stm31.js"></SCRIPT>
								<SCRIPT src="/Servosms/HeaderFooter/images/menu.htm"></SCRIPT>
							</TD>
						</TR>
						<%try{%>
						<tr >
							<td style="padding-left:25"><font face="Arial, Helvetica, sans-serif" color="maroon">&nbsp;<b>Date :</b>
									<asp:label id="lblDate" runat="server"></asp:label>
									- &nbsp;&nbsp;User Type : <b>
										<%=Session["User_Type"].ToString()%>
									</b>- Login By : <b>
										<%=Session["User_Name"].ToString()%>
									</b></font>&nbsp;&nbsp;<font face="Arial, Helvetica, sans-serif" color="blue"><b>Release 
										Version:</b> SSMS V 3.3&nbsp;<b>Dated :</b>&nbsp;<asp:label id="lblvrsndt" runat="server"></asp:label></font>
							</td>
							<td align="right" style="padding-right:25"><asp:hyperlink id="Hyperlink1" runat="server" ForeColor="maroon" NavigateUrl="../LoginHome/login.aspx"><b>LogOut</b></asp:hyperlink>&nbsp;&nbsp;</td>
						</tr>
						<%}
						catch(Exception ex)
						{
							Response.Redirect("../Sysitem/ErrorPage.aspx",false);
							return;
						}
						%>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
