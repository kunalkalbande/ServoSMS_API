<%@ Page Language="c#" Inherits="Servosms.Module.LoginHome.login" CodeFile="login.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
	<HEAD>
		<title>ServoSMS: Login</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<script language="javascript">
	function Check()
	{
		if(document.Form1.DropUser.value=="Select")
		{
			document.Form1.TxtUserName.disabled=true
			document.Form1.TxtPassword.disabled=true
		}
	}
		
	function Enable()
	{
		if(document.Form1.DropUser.value=="Select")
		{
			document.Form1.TxtUserName.disabled=true
			document.Form1.TxtPassword.disabled=true
		}
		else
		{
			document.Form1.TxtUserName.disabled=false
			document.Form1.TxtPassword.disabled=false
		}		
	}
		</script>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<LINK href="../Sysitem/Styles.css" type="text/css" rel="stylesheet">
			<script language="JavaScript" type="text/JavaScript">
		
<!--
function MM_jumpMenu(targ,selObj,restore){ //v3.0
eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
if (restore) selObj.selectedIndex=0;
}
//-->
			</script>
	</HEAD>
	<body style="MARGIN: 0px">
		<form id="Form1" runat="server">
			<table cellSpacing="0" cellPadding="0" width="1350" align="center" bgColor="#ffffff" border="0">
				<tr>
					<td vAlign="top" width="1350"><IMG height="88" src="../HeaderFooter/images/eLDMS Header.png" width="1350"></td>
				</tr>
				<tr>
					<td>
						<table height="490" cellSpacing="0" cellPadding="0" width="1350" border="0">
							<tr>
								<td width="31">&nbsp;</td>
								<td vAlign="top" width="500">
									<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
										height="450" width="500" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" VIEWASTEXT>
										<PARAM NAME="_cx" VALUE="6615">
										<PARAM NAME="_cy" VALUE="8229">
										<PARAM NAME="FlashVars" VALUE="6615">
										<PARAM NAME="Movie" VALUE="../HeaderFooter/images/Servosmsbannar.swf">
										<PARAM NAME="Src" VALUE="../HeaderFooter/images/Servosmsbannar.swf">
										<PARAM NAME="WMode" VALUE="Window">
										<PARAM NAME="Play" VALUE="-1">
										<PARAM NAME="Loop" VALUE="-1">
										<PARAM NAME="Quality" VALUE="High">
										<PARAM NAME="SAlign" VALUE="">
										<PARAM NAME="Menu" VALUE="-1">
										<PARAM NAME="Base" VALUE="">
										<PARAM NAME="AllowScriptAccess" VALUE="always">
										<PARAM NAME="Scale" VALUE="ShowAll">
										<PARAM NAME="DeviceFont" VALUE="0">
										<PARAM NAME="EmbedMovie" VALUE="0">
										<PARAM NAME="BGColor" VALUE="">
										<PARAM NAME="SWRemote" VALUE="">
										<embed src="../HeaderFooter/images/Servosmsbannar.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
											type="application/x-shockwave-flash" width="500" height="490"> </embed>
									</OBJECT>
								</td>
								
								<td vAlign="middle" align="center" width="700" style="padding-bottom:50">
									<table align="center" border="0">
										<tr>
											<td align="center" height="30"><asp:label id="lblMessage" CssClass="fontstyle" Runat="server" ForeColor="#CE4848" Font-Bold="True"></asp:label></td>
										</tr>
									</table>
									<TABLE cellSpacing="0" cellPadding="0" width="346" align="center" border="0">
										<TR>
											<TD width="15"><IMG height="31" alt="" src="../HeaderFooter/Images/windowsms_01.jpg" width="15"></TD>
											<TD colSpan="3"><IMG height="31" alt="" src="../HeaderFooter/Images/windowsms_112.jpg" width="322"></TD>
											<TD width="10"><IMG height="31" alt="" src="../HeaderFooter/images/windowsms_03.jpg" width="10"></TD>
										</TR>
										<TR>
											<TD vAlign="top"><IMG height="200" alt="" src="../HeaderFooter/images/windowsms_04.jpg" width="15"></TD>
											<TD vAlign="top" colSpan="3"><form name="form1" method="post" action="">
													<table cellSpacing="3" cellPadding="3" width="100%" align="center" border="0">
														<tr>
															<td colSpan="3">&nbsp;</td>
														</tr>
														<tr vAlign="top">
															<td vAlign="middle" align="left" width="8%"><IMG src="../HeaderFooter/images/windowsms_05.jpg"></td>
															<td vAlign="middle" width="30%"><IMG height="14" src="../HeaderFooter/images/UserType.jpg" width="66"></td>
															<td vAlign="middle" width="62%"><asp:dropdownlist id="DropUser" Width="140" CssClass="fontstyle" Runat="server">
																	<asp:ListItem Value="Select">Select</asp:ListItem>
																</asp:dropdownlist></td>
														</tr>
														<tr vAlign="top">
															<td vAlign="middle" align="left"><IMG src="../HeaderFooter/images/windowsms_07.jpg"></td>
															<td vAlign="middle"><IMG height="16" src="../HeaderFooter/images/UserLogin.jpg" width="66"></td>
															<td vAlign="middle"><asp:textbox id="txtUserLogin" Width="140" CssClass="fontstyle" Runat="server" BorderStyle="Groove"></asp:textbox></td>
														</tr>
														<tr vAlign="top">
															<td vAlign="middle" align="center"><IMG src="../HeaderFooter/images/windowsms_08.jpg"></td>
															<td vAlign="middle" align="left"><IMG height="14" src="../HeaderFooter/images/Passwd.jpg" width="66"></td>
															<td vAlign="middle"><asp:textbox id="txtPasswd" Width="140" CssClass="fontstyle" Runat="server" BorderStyle="Groove"
																	TextMode="Password"></asp:textbox></td>
														</tr>
														<tr vAlign="top">
															<td vAlign="middle" align="left"><IMG src="../HeaderFooter/images/windowsms_09.jpg"></td>
															<td vAlign="middle" align="left"><IMG height="14" src="../HeaderFooter/images/SetDate.jpg" width="66"></td>
															<td vAlign="middle"><asp:textbox id="txtSetDate" Width="140" CssClass="fontstyle" Runat="server" BorderStyle="Groove"
																	ReadOnly="True"></asp:textbox>&nbsp;<A id="a1" onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtSetDate);return false;">
																	<IMG class="PopcalTrigger" id="Cal_Img" height="20" src="../HeaderFooter/DTPicker/Calender_icon.jpg"
																		width="21" align="absMiddle" runat="server"></A><IMG class="PopcalTrigger" id="Cal_Img1" src="../HeaderFooter/DTPicker/calendar_icon1.gif"
																	align="absMiddle" runat="server"></td>
														</tr>
														<tr vAlign="top">
															<td align="center" colSpan="2" height="29">&nbsp;</td>
															<td vAlign="middle" align="center">
																<div align="left"><input id="btnSign" type="image" src="../HeaderFooter/images/submitsms.jpg" value="Submit"
																		name="Submit" runat="server"></div>
															</td>
														</tr>
													</table>
												</form>
											</TD>
											<TD vAlign="top"><IMG height="199" alt="" src="../HeaderFooter/images/windowsms_06.jpg" width="10"></TD>
										</TR>
										<TR>
											<TD><IMG height="10" alt="" src="../HeaderFooter/images/windowsms_16.jpg" width="15"></TD>
											<TD vAlign="top" align="left" colSpan="3"><IMG height="10" alt="" src="../HeaderFooter/images/windowsms_17.jpg" width="322"></TD>
											<TD><IMG height="10" alt="" src="../HeaderFooter/images/windowsms_18.jpg" width="10"></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="top"></td>
				</tr>
				<tr>
					<td vAlign="top">
						<TABLE cellSpacing="0" cellPadding="0" width="780" border="0">
							<%--<TR>
								<TD><IMG height="15" alt="" src="../HeaderFooter/images/footersms_01.jpg" width="1100"></TD>
								<TD><IMG height="15" alt="" src="../HeaderFooter/images/footersms_02.jpg" width="237"></TD>
								<TD><IMG height="15" alt="" src="../HeaderFooter/images/footersms_03.jpg" width="22"></TD>
							</TR>--%>
							<TR>
								<TD><IMG height="55" alt="" src="../HeaderFooter/images/eLDMS Footer.png" width="1350"></TD>
								<%--<TD><A href="#"><IMG height="28" alt="" src="../HeaderFooter/images/footersms_05.jpg" width="237" border="0"></A></TD>--%>
								<%--<TD><IMG height="28" alt="" src="../HeaderFooter/images/footersms_06.jpg" width="22"></TD>--%>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe></form>
	</body>
</HTML>
