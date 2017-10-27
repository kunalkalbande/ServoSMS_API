<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.help.ContactDetails" CodeFile="ContactDetails.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:o xmlns:st1>
	<HEAD>
		<title>ServoSMS: Contact Details</title> <!--
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
			<uc1:header id="Header1" runat="server" DESIGNTIMEDRAGDROP="11"></uc1:header>
			<table width="778" height="290" align="center">
				<tr>
					<!--td><asp:image id="Image1" runat="server" ImageUrl="CompanyLogo\logo2.GIF"></asp:image></td-->
					<td align="center" colspan="2"><asp:image id="Image2" runat="server" ImageUrl="../../HeaderFooter/images/logo2.gif"></asp:image></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" colSpan="2" rowSpan="1">
						<table width="600">
							<tr>
								<td align="center" colspan="3">
									<FONT color="#CF4848"><FONT face="Tw Cen MT Condensed Extra Bold" size="4">bbnisys 
											Technologies (P) Limited</FONT><br>
									</FONT>
								</td>
							</tr>
							<tr>
								<td align="center">
									<table align="center">
										<tr>
											<td>
												<FONT face="Tw Cen MT Condensed Extra Bold" size="2" color="darkblue"><U>Registered 
														Office:</U><br>
												</FONT>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 4, Nath Kripa, 
												Opp. Hotel Grace,<br>
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jhansi Road, Gwalior- 
												India-474002<br>
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Phone: +91-751-2341-037<br>
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fax: 
												+91-751-4010-134</FONT>
											</td>
										</tr>
										<tr>
											<td>
												Enquiry:&nbsp; <A href="mailto:info@bbnisys.com">info@bbnisys.com</A><br>
												Support: <A href="mailto:support@bbnisys.com">support@bbnisys.com</A>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<!--tr>
								<td align="center"><br>
									<br>
								</td>
							</tr-->
						</table>
						<!--P></P-->
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
