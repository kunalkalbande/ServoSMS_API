<%@ Page language="c#" Inherits="Servosms.Sysitem.ErrorPage" CodeFile="ErrorPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Error Page</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="778" height="290" align=center>
				<tr>
					<td>
						<P align="center"><FONT color="red" size="5"><FONT color="#ff9966">Your&nbsp;&nbsp;Session&nbsp;Has&nbsp;Expired</FONT>
							</FONT>
						</P>
						<P align="center">
							<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="../Loginhome/login.aspx" Font-Size="Medium">Please Login</asp:HyperLink></P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
