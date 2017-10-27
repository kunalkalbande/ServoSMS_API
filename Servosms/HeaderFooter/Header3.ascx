<%@ Control Language="c#" Inherits="Servosms.Header3" CodeFile="Header3.ascx.cs" %>

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
  </HEAD>
	<body leftMargin="0" topMargin="0">
		<table cellSpacing="0" cellPadding="0" align="center" border="0">
			<tr>
				<td colspan="2"><div style="LEFT: 0px; POSITION: relative; TOP: 0px">
						<IMG src="/Servosms/HeaderFooter/images/eLDMS Header.png" width="778" height=75></div>
					<table cellSpacing="0" cellPadding="0" border="0" width="100%">
						<%try{%>
						<tr>
							<td><font face="Arial, Helvetica, sans-serif" color="#ce4848">&nbsp;<b>Date : </b>
									<asp:label id="lblDate" runat="server"></asp:label> - &nbsp;&nbsp;User Type : <b><%=Session["User_Type"].ToString()%></b> - Login By  : <b><%=Session["User_Name"].ToString()%></b></font></td>
							<td align="right"><asp:hyperlink id="Hyperlink1" runat="server" NavigateUrl="../LoginHome/HomePage.aspx" ForeColor="#CE4848"><b>Home</b></asp:hyperlink>&nbsp;&nbsp;</td>
						</tr>
						<%}
						catch(Exception ex)
						{
							Response.Redirect("\\Servosms\\Sysitem\\ErrorPage.aspx",false);
							return;
						}
						%>
					</table>
				</td>
			</tr>
			<!--tr>
				<td height="333">&nbsp;</td>
			</tr>
			<tr>
				<td width="779" height="37" background="images1/EServo-login_13.gif">
					<div align="center" class="b"><br>
						bbnisys Technologies (P) Ltd. India, Contact us : 0091 - 751 - 400134 
						E-mail:support@bbnisys.com</div>
				</td>
			</tr-->
		</table>
	</body>
</HTML>
