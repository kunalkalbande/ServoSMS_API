<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.PrimSecDiscount" CodeFile="PrimSecDiscount.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PrimSecDiscount</title>
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
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox>
			<table height="340" width="778" align="center" cellpadding="0" cellspacing="0">
				<TBODY>
					<TR>
						<TH style="HEIGHT: 4px" align="center">
							<font color="#ce4848">Primary/Secondry&nbsp;Sales Discount Report</font>
							<hr>
						</TH>
					</TR>
					<tr>
						<td vAlign="top" align="center">
							<TABLE id="Table1" cellpadding="0" cellspacing="0">
								<TBODY>
									<TR>
										<TD align="center">Date From</TD>
										<TD align="center"><asp:textbox id="txtDateFrom" runat="server" Width="111" ReadOnly="True" BorderStyle="Groove"
												CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A>
											<asp:requiredfieldvalidator id="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Please Select From Date From the Calender">*</asp:requiredfieldvalidator></TD>
										<TD align="center">Date To</TD>
										<TD align="center"><asp:textbox id="TextBox1" runat="server" Width="111px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.TextBox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A>
											<asp:requiredfieldvalidator id="rfvDateTo" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please Select To Date From the Calender">*</asp:requiredfieldvalidator></TD>
									</TR>
									<tr>
										<TD align="center" colSpan="4"><asp:button id="btnShow" runat="server" Width="60px" 
												Text="View" onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="60px" 
												Text="Print " Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;
											<asp:button id="btnExcel" Width="60px" 
												Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></TD>
									</tr>
									<tr>
										<TD align="center" colSpan="4"><asp:panel id="Panel1" runat="server" Height="56px">
												<TABLE cellPadding="5" border="1">
													<TR>
														<TD>
															<TABLE cellSpacing="0" cellPadding="1" border="0">
																<TR>
																	<TD align="center" width="200"><STRONG>Primary Sales Discount</STRONG></TD>
																	<TD width="10"></TD>
																	<TD width="10"><STRONG>Purchase:</STRONG>&nbsp;&nbsp;&nbsp;</TD>
																	<TD width="120">
																		<asp:Label id="lbltotalpurltr" runat="server" CssClass="fontstyle" Height="12px" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;<STRONG>Ltr./Kg</STRONG></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">1.Early Bird Discount</TD>
																	<TD width="10"></TD>
																	<TD width="10"></TD>
																	<TD width="10">
																		<asp:Label id="lblebird" runat="server" Width="70px" CssClass="fontstyle" Height="12"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">2.Trade Discount</TD>
																	<TD width="10"></TD>
																	<TD width="10"></TD>
																	<TD width="10">
																		<asp:Label id="lbltrade" runat="server" Width="80px" CssClass="fontstyle" Height="12"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD style="HEIGHT: 16px" width="200">3.Cash Discount</TD>
																	<TD style="HEIGHT: 16px" width="10"></TD>
																	<TD style="HEIGHT: 16px" width="10"></TD>
																	<TD style="HEIGHT: 16px" width="10">
																		<asp:Label id="lblcash" runat="server" Width="74px" CssClass="fontstyle" Height="12"></asp:Label></TD>
																	<TD style="HEIGHT: 16px" width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">4.% Discount</TD>
																	<TD width="10"></TD>
																	<TD width="10"></TD>
																	<TD width="10">
																		<asp:Label id="lblfixed" runat="server" Width="74px" CssClass="fontstyle" Height="12"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">5.FOC Discount</TD>
																	<TD width="10"></TD>
																	<TD width="10"></TD>
																	<TD width="10">
																		<asp:Label id="lblfoc" runat="server" Width="74px" CssClass="fontstyle" Height="12px"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">6.Old Rate Discount</TD>
																	<TD width="10"></TD>
																	<TD width="10"></TD>
																	<TD width="10">
																		<asp:Label id="lblold" runat="server" Width="74px" CssClass="fontstyle" Height="12px"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD align="center" width="200"><STRONG>Total Primary Discount Recieved</STRONG></TD>
																	<TD width="10"></TD>
																	<TD width="10"></TD>
																	<TD width="10">
																		<asp:Label id="lblpurtotal" runat="server" Width="76px" CssClass="fontstyle" Height="12px"
																			Font-Bold="True"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
													<TR>
														<TD>
															<TABLE cellSpacing="0" cellPadding="1" border="0">
																<TR>
																	<TD align="center" width="200"><STRONG>Secondry&nbsp;Sales Discount</STRONG></TD>
																	<TD width="10"></TD>
																	<TD width="63"><STRONG>&nbsp;Sales:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																		</STRONG>
																	</TD>
																	<TD style="WIDTH: 120px" width="86">
																		<asp:Label id="lbltotalsalltr" runat="server" CssClass="fontstyle" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;<STRONG>Ltr./Kg</STRONG></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">1.Secondry Sales Scheme&nbsp;Discount</TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lblsecsale" runat="server" Width="76px" CssClass="fontstyle"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">2.Primary Sales Scheme Discount</TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lblprimsale" runat="server" Width="74px" CssClass="fontstyle"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">3.Cash Discount</TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lblcashsale" runat="server" Width="74px" CssClass="fontstyle"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">4.Fleet Discount</TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lblfleetsale" runat="server" Width="74px" CssClass="fontstyle"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">5.OE Discount</TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lbloesale" runat="server" Width="74px" CssClass="fontstyle"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD width="200">6.% Discount</TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lblperdis" runat="server" Width="74px" CssClass="fontstyle"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
																<TR>
																	<TD align="center" width="200"><STRONG>Total&nbsp;Secondry Discount Passed</STRONG></TD>
																	<TD width="10"></TD>
																	<TD width="63"></TD>
																	<TD style="WIDTH: 86px" width="86">
																		<asp:Label id="lblsaletotal" runat="server" Width="74px" CssClass="fontstyle" Height="17px"
																			Font-Bold="True"></asp:Label></TD>
																	<TD width="10"></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</asp:panel>
										</TD>
									</tr>
								</TBODY>
							</TABLE>
						</td>
					</tr>
				</TBODY>
			</table>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
