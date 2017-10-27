<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.help.AboutUs" CodeFile="AboutUs.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: About Us</title> <!--
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
		<form id="Form1" method="post" runat="server">
			<uc1:Header id="Header1" runat="server" DESIGNTIMEDRAGDROP="11"></uc1:Header>
			<table width="778" height="290" align="center">
				<tr>
					<td>
						<P align="center"><FONT color="#ce4848" size="5"><FONT color="#ce4848"><STRONG><FONT size="6">ServoSMS</FONT></STRONG></FONT>&nbsp;</FONT>&nbsp; 
							</FONT>
							<br>
							<FONT color="#006600"><STRONG><font color="#ce4848" size="3">Stockist / Distributor 
										Management System</font><br>
									<br>
								</STRONG></FONT><font color="#000000">Copyright © 2005-2014<br>
								bbnisys Technologies Private Ltd. Gwalior India.<br>
								All Rights Reserved</font> .
						</P>
						<p align="center"><asp:HyperLink Font-Bold="True" Font-Size="12" ForeColor="blue" ID="Hlnk" Runat="server" NavigateUrl="Version Info.Txt">Release Version: SSMS V 3.3 Dated : 16.03.2014</asp:HyperLink></p>
					</td>
				</tr>
			</table>
			<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
