<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="DBOperations"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ import namespace="RMG"%>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SRSM_Format_Report" CodeFile="SRSM_Format_Report.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS : SRSM_Format_Report</title>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TBODY>
					<TR vAlign="top" height="20">
						<TH>
							<FONT color="#ce4848" size="2">SRSM Report</FONT>
							<hr>
						</TH>
					</TR>
					<tr vAlign="top">
						<td align="center">Select Month&nbsp;&nbsp;
							<asp:requiredfieldvalidator id="rfv1" Runat="server" InitialValue="Select" ErrorMessage="Please Select The Month First"
								ControlToValidate="DropMonth">*</asp:requiredfieldvalidator>&nbsp;
							<asp:requiredfieldvalidator id="Requiredfieldvalidator1" Runat="server" InitialValue="Select" ErrorMessage="Please Select The Year First"
								ControlToValidate="DropYear">*</asp:requiredfieldvalidator>&nbsp;
							<asp:dropdownlist id="DropMonth" Runat="server" CssClass="fontstyle" AutoPostBack="False" style="margin-left: 0px">
								<asp:ListItem Value="Select">Select</asp:ListItem>
								<asp:ListItem Value="January">January</asp:ListItem>
								<asp:ListItem Value="February">February</asp:ListItem>
								<asp:ListItem Value="March">March</asp:ListItem>
								<asp:ListItem Value="April">April</asp:ListItem>
								<asp:ListItem Value="May">May</asp:ListItem>
								<asp:ListItem Value="June">June</asp:ListItem>
								<asp:ListItem Value="July">July</asp:ListItem>
								<asp:ListItem Value="August">August</asp:ListItem>
								<asp:ListItem Value="September">September</asp:ListItem>
								<asp:ListItem Value="October">October</asp:ListItem>
								<asp:ListItem Value="November">November</asp:ListItem>
								<asp:ListItem Value="December">December</asp:ListItem>
							</asp:dropdownlist><asp:dropdownlist id="DropYear" Runat="server" CssClass="fontstyle" style="margin-left: 3px">
								<asp:ListItem Value="Select">Select</asp:ListItem>
								<asp:ListItem Value="2005">2005</asp:ListItem>
								<asp:ListItem Value="2006">2006</asp:ListItem>
								<asp:ListItem Value="2007">2007</asp:ListItem>
								<asp:ListItem Value="2008">2008</asp:ListItem>
								<asp:ListItem Value="2009">2009</asp:ListItem>
								<asp:ListItem Value="2010">2010</asp:ListItem>
								<asp:ListItem Value="2011">2011</asp:ListItem>
								<asp:ListItem Value="2012">2012</asp:ListItem>
								<asp:ListItem Value="2013">2013</asp:ListItem>
								<asp:ListItem Value="2014">2014</asp:ListItem>
								<asp:ListItem Value="2015">2015</asp:ListItem>
								<asp:ListItem Value="2016">2016</asp:ListItem>
								<asp:ListItem Value="2017">2017</asp:ListItem>
								<asp:ListItem Value="2018">2018</asp:ListItem>
								<asp:ListItem Value="2019">2019</asp:ListItem>
								<asp:ListItem Value="2020">2020</asp:ListItem>
							</asp:dropdownlist>&nbsp;&nbsp;SRSM Type&nbsp;&nbsp;&nbsp;<asp:requiredfieldvalidator id="validdroptype" Runat="server" InitialValue="Select" ErrorMessage="Please Select Report Type"
								ControlToValidate="dropType">*</asp:requiredfieldvalidator>&nbsp;<asp:dropdownlist id="dropType" Runat="server" CssClass="dropdownlist" AutoPostBack="True" Width="100px" onselectedindexchanged="dropType_SelectedIndexChanged">
								<asp:ListItem Value="Select">Select</asp:ListItem>
								<asp:ListItem Value="SRSM 1-4">SRSM 1-4</asp:ListItem>
								<asp:ListItem Value="SRSM 5">SRSM 5</asp:ListItem>
								<asp:ListItem Value="SRSM 6">SRSM 6</asp:ListItem>
								<asp:ListItem Value="SRSM 9">SRSM 9</asp:ListItem>
								<asp:ListItem Value="SRSM 10 & 12">SRSM 10 & 12</asp:ListItem>
								<asp:ListItem Value="SRSM 14">SRSM 14</asp:ListItem>
							</asp:dropdownlist>&nbsp;&nbsp;<asp:button id="btnShow" Runat="server" Width="60px" 
								Text="Show" onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;<asp:textbox id="txtvalue" runat="server" CssClass="fontstyle" Width="130px" BorderStyle="Groove"
								Visible="False"></asp:textbox>
							&nbsp;&nbsp;<asp:button id="btnSave" Runat="server" Width="60px" 
								Text="Update" Visible="False" CausesValidation="False" onclick="btnSave_Click"></asp:button>
							&nbsp;&nbsp;<asp:button id="btneExcel" Runat="server" Width="60px" 
								 Text="Excel" onclick="btneExcel_Click"></asp:button></td>
					</tr>
					<tr vAlign="top">
						<td align="center">
							<table cellSpacing="0" cellPadding="0" width="100%" align="center">
								<TBODY>
									<%
											
										
								if(dropType.SelectedIndex==1)
								{
									%>
									<asp:panel id="panSRSM_1_4" Runat="server" Visible="False">
										<TR vAlign="top" height="20">
											<TH>
												<FONT color="#ce4848" size="1">SRSM 1-4</FONT></TH></TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="1000" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-1</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848" colSpan="8">
															<FONT color="#ffffff" size="1">MONTH :
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="8">
															<FONT color="#ffffff" size="1">CUMULATIVE :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS/RSE</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS/<BR>
																SAP Code</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">No of Districts Covered</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Potential</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Target</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Growth</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">% GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">% Achv.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">% of Potential</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Potential</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Target</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Growth</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">% GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">% Achv.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">% of Potential</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD>178676</TD>
														<TD>6</TD>
														<TD>100</TD>
														<TD align="right"><%=Target%></TD>
														<TD align="right"><%=CY_P_Sale%></TD>
														<TD align="right"><%=LY_P_Sale%></TD>
														<TD align="right"><%=Growth%></TD>
														<TD align="right"><%=GR_Per%></TD>
														<TD align="right"><%=Achiv%></TD>
														<TD align="right"><%=Poten_Per%></TD>
														<TD></TD>
														<TD align="right"><%=Cumu_Target%></TD>
														<TD align="right"><%=Cumu_CY_P_Sale%></TD>
														<TD align="right"><%=Cumu_LY_P_Sale%></TD>
														<TD align="right"><%=Cumu_Growth%></TD>
														<TD align="right"><%=Cumu_GR_Per%></TD>
														<TD align="right"><%=Cumu_Achiv%></TD>
														<TD align="right"><%=Cumu_Poten_Per%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-2</FONT></TH>
														<TH bgColor="#ce4848" colSpan="15">
															<FONT color="#ffffff" size="1">LFR, 2T+4T/MS RATIO</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="7">
															<FONT color="#ffffff" size="1">MONTH :
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="7">
															<FONT color="#ffffff" size="1">CUMULATIVE :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">MS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">HSD</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO Lube</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO LFR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO 2T</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO 4T</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">2T+4T/MS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">MS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">HSD</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO Lube</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO LFR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO 2T</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO 4T</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">2T+4T/MS</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD align="right"><%=MS_Sale.ToString()%></TD>
														<TD align="right"><%=HSD_Sale.ToString()%></TD>
														<TD align="right"><%=RO_Lube.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_LFR.ToString()%></TD>
														<TD align="right"><%=RO_2T.ToString()%></TD>
														<TD align="right"><%=RO_4T.ToString()%></TD>
														<TD align="right"><%=MS_2T_4T.ToString()%></TD>
														<TD align="right"><%=Cumu_MS_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_HSD_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_Lube.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_LFR.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_2T.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_4T.ToString()%></TD>
														<TD align="right"><%=Cumu_MS_2T_4T.ToString()%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-3A</FONT></TH>
														<TH bgColor="#ce4848" colSpan="16">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO :</FONT></TH>
														<TH bgColor="#ce4848" colSpan="8">
															<FONT color="#ffffff" size="1">MONTH :
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="8">
															<FONT color="#ffffff" size="1">CUMULATIVE :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">NON KSK RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">KSK RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">ESSAR RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">RO TOTAL</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">NON KSK RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">KSK RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">ESSAR RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">RO TOTAL</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD align="right"><%=NKR_CY_Sale.ToString()%></TD>
														<TD align="right"><%=NKR_LY_Sale.ToString()%></TD>
														<TD align="right"><%=KR_CY_Sale.ToString()%></TD>
														<TD align="right"><%=KR_LY_Sale.ToString()%></TD>
														<TD align="right"><%=ERO_CY_Sale.ToString()%></TD>
														<TD align="right"><%=ERO_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Tot_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Tot_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_NKR_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_NKR_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_KR_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_KR_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_ERO_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_ERO_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_Tot_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_Tot_LY_Sale.ToString()%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-3B</FONT></TH>
														<TH align="right" bgColor="#ce4848" colSpan="20">
															<FONT color="#ffffff" size="1">CHANNEL-WISE SALES PERFORMANCE</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO :</FONT></TH>
														<TH bgColor="#ce4848" colSpan="10">
															<FONT color="#ffffff" size="1">MONTH :
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="10">
															<FONT color="#ffffff" size="1">CUMULATIVE :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">RO TOTAL</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">BAZZAR</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">OE</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">FLEET</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">RO TOTAL</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">BAZZAR</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">OE</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">FLEET</FONT></TH>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD align="right"><%=RO_CY_Sale.ToString()%></TD>
														<TD align="right"><%=RO_LY_Sale.ToString()%></TD>
														<TD align="right"><%=BAZ_CY_Sale.ToString()%></TD>
														<TD align="right"><%=BAZ_LY_Sale.ToString()%></TD>
														<TD align="right"><%=OE_CY_Sale.ToString()%></TD>
														<TD align="right"><%=OE_LY_Sale.ToString()%></TD>
														<TD align="right"><%=FLEET_CY_Sale.ToString()%></TD>
														<TD align="right"><%=FLEET_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Tot_CY_Sale3B.ToString()%></TD>
														<TD align="right"><%=Tot_LY_Sale3B.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_RO_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_BAZ_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_BAZ_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_OE_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_OE_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_FLEET_CY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_FLEET_LY_Sale.ToString()%></TD>
														<TD align="right"><%=Cumu_Tot_CY_Sale3B.ToString()%></TD>
														<TD align="right"><%=Cumu_Tot_LY_Sale3B.ToString()%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-4</FONT></TH>
														<TH align="center" bgColor="#ce4848" colSpan="21">
															<FONT color="#ffffff" size="1">CHANNEL-MARKET PENETRATION</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO : SO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="21">
															<FONT color="#ffffff" size="1">CUMULATIVE :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">NON KSK RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">KSK RO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">BAZZAR</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">OE DEALERS</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">FLEET OWNER</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">IBP</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD align="right"><%=NKR_Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=NKR_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=NKR_LY_Sale_4.ToString()%></TD>
														<TD align="right"><%=KR_Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=KR_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=KR_LY_Sale_4.ToString()%></TD>
														<TD align="right"><%=BAZ_Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=BAZ_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=BAZ_LY_Sale_4.ToString()%></TD>
														<TD align="right"><%=OE_Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=OE_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=OE_LY_Sale_4.ToString()%></TD>
														<TD align="right"><%=FLEET_Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=FLEET_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=FLEET_LY_Sale_4.ToString()%></TD>
														<TD align="right"><%=IBP_Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=IBP_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=IBP_LY_Sale_4.ToString()%></TD>
														<TD align="right"><%=Tot_Sale_4.ToString()%></TD>
														<TD align="right"><%=Tot_CY_Sale_4.ToString()%></TD>
														<TD align="right"><%=Tot_LY_Sale_4.ToString()%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</asp:panel>
									<%
									}
									else if(dropType.SelectedIndex==2)
									{
										i=1;
										ii=0;
										InventoryClass obj=new InventoryClass();
										sql="select cust_id,cust_name,prod_name+' : '+pack_type prod,(quant*total_qty) Qty_Ltr from vw_salebook where cust_type like 'OE%' and cast(floor(cast(invoice_date as float)) as datetime)>='"+StartDate.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+EndDate.ToString()+"' order by cust_name";
										dtr1=obj.GetRecordSet(sql);
										if(dtr1.HasRows)
										{
											%>
									<asp:panel id="panSRSM_5" Runat="server" Visible="False">
										<TR vAlign="top" height="20">
											<TH>
												<FONT color="#ce4848" size="1">SRSM 5</FONT></TH></TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO</FONT></TH>
														<TH bgColor="#ce4848" colSpan="5">
															<FONT color="#ffffff" size="1">
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">FIGS in KL</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848" colSpan="7">
															<FONT color="#ffffff" size="1">MARUTI</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SL. No.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Name of OE Dealer</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Genuine Oil 1</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Genuine Oil 2</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Genuine Oil 3</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Genuine Oil 4</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Total</FONT></TH></TR>
													<%
															while(dtr1.Read())
															{
																if(dtr1["cust_name"].ToString()!=Cust_Name)
																{
																	i=1;
																}
																
																if(ii!=0)
																{
																	if(i==1)
																	{
																		%>
													<TR bgColor="#ce4848">
														<TD>&nbsp;</TD>
														<TD colSpan="2"><FONT color="#ffffff"><B>Total</B></FONT></TD>
														<TD align="right"><FONT color="#ffffff"><B><%=Total.ToString()%></B></FONT></TD>
														<TD>&nbsp;</TD>
														<TD>&nbsp;</TD>
														<TD>&nbsp;</TD>
													</TR>
													<%
																		Total=0;
																	}
																}
																%>
													<TR>
														<TD><%=i.ToString()%></TD>
														<%
																	if(dtr1["cust_name"].ToString()!=Cust_Name)
																	{
																		Cust_Name=dtr1["cust_name"].ToString();
																		%>
														<TD><%=dtr1["cust_name"].ToString()%></TD>
														<%
																		
																	}
																	else
																	{
																		%>
														<TD>&nbsp;</TD>
														<%
																	}
																	%>
														<TD><%=dtr1["prod"].ToString()%></TD>
														<%Total+=double.Parse(dtr1["Qty_Ltr"].ToString());%>
														<TD align="right"><%=dtr1["Qty_Ltr"].ToString()%></TD>
														<TD>&nbsp;</TD>
														<TD>&nbsp;</TD>
														<TD>&nbsp;</TD>
													</TR>
													<%
																
																i++;
																ii++;
															}
															dtr1.Close();
															%>
													<TR bgColor="#ce4848">
														<TD>&nbsp;</TD>
														<TD colSpan="2"><FONT color="#ffffff"><B>Total</B></FONT></TD>
														<TD align="right"><FONT color="#ffffff"><B><%=Total.ToString()%></B></FONT></TD>
														<TD>&nbsp;</TD>
														<TD>&nbsp;</TD>
														<TD>&nbsp;</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</asp:panel>
									<%
										}
									}
									else if(dropType.SelectedIndex==3)
									{
										
											%>
									<asp:panel id="panSRSM_6" Runat="server" Visible="False">
										<TR vAlign="top" height="20">
											<TH>
												<FONT color="#ce4848" size="1">SRSM 6</FONT></TH></TR>
										<%
												double Month_Tot_Target=0;
												double Month_Tot_CY=0;
												double Month_Tot_LY=0;
												double Month_Tot_Growth=0;
												double Month_Tot_Achivent=0;
												
												double Cumu_Tot_Target=0;
												double Cumu_Tot_CY=0;
												double Cumu_Tot_LY=0;
												double Cumu_Tot_Growth=0;
												double Cumu_Tot_Achivent=0;
												
												double sale1=0;
												double sale2=0;
												double growth=0;
												double GR=0;
												i=1;
												InventoryClass obj=new InventoryClass();
												sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where category like '%GEN OIL%' or category ='Mstar' or  category = 'Eicher' or  category ='hyundai oils' or  category = 'L&t' order by Prod_Name";
												dtr1=obj.GetRecordSet(sql);
												if(dtr1.HasRows)
												{
													%>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">SRSM-6 A</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO :</FONT></TH>
														<TH bgColor="#ce4848" colSpan="8">
															<FONT color="#ffffff" size="1">MONTH :
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-6 A</FONT></TH>
														<TH bgColor="#ce4848" colSpan="4">
															<FONT color="#ffffff" size="1">MONTH :
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="4">
															<FONT color="#ffffff" size="1">CUM :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SR. No.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Product Code</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GENUINE OIL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GRTH</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GRTH</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH></TR>
													<%
																while(dtr1.Read())
																{
																	%>
													<TR>
														<TD><%=i.ToString()%></TD>
														<TD><%=dtr1["Prod_code"].ToString()%></TD>
														<TD><%=dtr1["Product"].ToString()%></TD>
														<%
																		sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
																		Month_Tot_CY+=sale1;
																		Month_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																		}
																		%>
														<TD align="right"><%=sale1.ToString()%></TD>
														<TD align="right"><%=sale2.ToString()%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<%
																		sale1=0;
																		sale2=0;
																		sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
																		Cumu_Tot_CY+=sale1;
																		Cumu_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																		}
																		%>
														<TD align="right"><%=sale1%></TD>
														<TD align="right"><%=sale2%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
													</TR>
													<%
																	i++;
																}
																dtr1.Close();
																	%>
													<TR>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">Total</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<%
												}
												
												Month_Tot_Target=0;
												Month_Tot_CY=0;
												Month_Tot_LY=0;
												Month_Tot_Growth=0;
												Month_Tot_Achivent=0;
												
												Cumu_Tot_Target=0;
												Cumu_Tot_CY=0;
												Cumu_Tot_LY=0;
												Cumu_Tot_Growth=0;
												Cumu_Tot_Achivent=0;
												
												sale1=0;
												sale2=0;
												growth=0;
												double Target=0;
												double Achivment=0;
												GR=0;
												i=1;
												sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where (prod_name like 'PREMIUM CF4 15w40%' or prod_name = '4T' or prod_name = '4T Goben' or prod_name like 'Pride TC 15w40%' or prod_name like 'GEAR HP ALPHA 80w90%' or prod_name like 'Tractor Oil 20w40%' or prod_name like 'Transfluid A%' or prod_name like 'Autorickshaw oil%' or prod_name like 'Gear HP 90 - 20%' or prod_name like 'Pride XL 15w40%' or prod_name like 'Super MG CF4 15W-40%' or prod_name like 'GAS ENGINE OIL%' or prod_name like 'Agrospray%' or prod_name like 'Pride  GEO%' or prod_name like 'HBF Super HD%' or prod_name like 'KOOL PLUS%' or prod_name like 'KOOL READY%' or prod_name like 'Glysantin G 05%'  or prod_name like 'Glysantin G 48%') order by Prod_Name";
												dtr1=obj.GetRecordSet(sql);
												if(dtr1.HasRows)
												{
													%>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="850" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">SRSM-6 B</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SO :</FONT></TH>
														<TH bgColor="#ce4848" colSpan="12">
															<FONT color="#ffffff" size="1">SALES OF FOCUS GRADES</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848" colSpan="6">
															<FONT color="#ffffff" size="1">
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="6">
															<FONT color="#ffffff" size="1">CUM :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SR. No.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Product Code</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GRADE</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TARGET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%ACHV.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TARGET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%ACHV.</FONT></TH></TR>
													<%
																while(dtr1.Read())
																{
																	%>
													<TR>
														<TD><%=i.ToString()%></TD>
														<TD><%=dtr1["Prod_code"].ToString()%></TD>
														<TD><%=dtr1["Product"].ToString()%></TD>
														<%
																		sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
																		Month_Tot_CY+=sale1;
																		Month_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																			Target=Math.Round((sale1*1.1),2);
																			Achivment=Math.Round(((sale1/Target)*100),2);
																			
																			if(Target!=0)
																				Achivment=Math.Round(((sale1/Target)*100),2);
																			else
																				Achivment=0;
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																			Target=0;
																			Achivment=0;
																		}
																		
																		%>
														<TD align="right"><%=Target.ToString()%></TD>
														<TD align="right"><%=sale1.ToString()%></TD>
														<TD align="right"><%=sale2.ToString()%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<TD align="right"><%=Achivment.ToString()%></TD>
														<%
																		sale1=0;
																		sale2=0;
																		sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
																		Cumu_Tot_CY+=sale1;
																		Cumu_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																			Target=Math.Round((sale1*1.1),2);
																			
																			if(Target!=0)
																				Achivment=Math.Round(((sale1/Target)*100),2);
																			else
																				Achivment=0;
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																			Target=0;
																			Achivment=0;
																		}
																		
																		%>
														<TD align="right"><%=Target.ToString()%></TD>
														<TD align="right"><%=sale1%></TD>
														<TD align="right"><%=sale2%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<TD align="right"><%=Achivment.ToString()%></TD>
													</TR>
													<%
																	i++;
																}
																dtr1.Close();
																	%>
													<TR>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">Total</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<%
													}
													
												Month_Tot_Target=0;
												Month_Tot_CY=0;
												Month_Tot_LY=0;
												Month_Tot_Growth=0;
												Month_Tot_Achivent=0;
												
												Cumu_Tot_Target=0;
												Cumu_Tot_CY=0;
												Cumu_Tot_LY=0;
												Cumu_Tot_Growth=0;
												Cumu_Tot_Achivent=0;
												
													sale1=0;
												sale2=0;
												growth=0;
												Target=0;
												Achivment=0;
												GR=0;
												i=1;
												sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where (prod_name like '2T Supreme%' or prod_name like 'TowerGen%' or prod_name like 'PSO%' or prod_name like 'Servo system 68%' or prod_name like 'Servosystem HLP 68%' or prod_name like 'Servo Hydrex TH 46%') order by Prod_Name";
												dtr1=obj.GetRecordSet(sql);
												if(dtr1.HasRows)
												{
													%>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">SRSM-6 C</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-6 C</FONT></TH>
														<TH bgColor="#ce4848" colSpan="12">
															<FONT color="#ffffff" size="1">SALES OF IMPORTANT GRADES</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848" colSpan="6">
															<FONT color="#ffffff" size="1">
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="6">
															<FONT color="#ffffff" size="1">CUM :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SR. No.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Product Code</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GRADE</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TARGET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%ACHV.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TARGET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%ACHV.</FONT></TH></TR>
													<%
																while(dtr1.Read())
																{
																	%>
													<TR>
														<TD><%=i.ToString()%></TD>
														<TD><%=dtr1["Prod_code"].ToString()%></TD>
														<TD><%=dtr1["Product"].ToString()%></TD>
														<%
																		sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
																		Month_Tot_CY+=sale1;
																		Month_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																			Target=Math.Round((sale1*1.1),2);
																			Achivment=Math.Round(((sale1/Target)*100),2);
																			
																			if(Target!=0)
																				Achivment=Math.Round(((sale1/Target)*100),2);
																			else
																				Achivment=0;
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																			Target=0;
																			Achivment=0;
																		}
																		
																		%>
														<TD align="right"><%=Target.ToString()%></TD>
														<TD align="right"><%=sale1.ToString()%></TD>
														<TD align="right"><%=sale2.ToString()%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<TD align="right"><%=Achivment.ToString()%></TD>
														<%
																		sale1=0;
																		sale2=0;
																		sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
																		Cumu_Tot_CY+=sale1;
																		Cumu_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																			Target=Math.Round((sale1*1.1),2);
																			
																			if(Target!=0)
																				Achivment=Math.Round(((sale1/Target)*100),2);
																			else
																				Achivment=0;
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																			Target=0;
																			Achivment=0;
																		}
																		
																		%>
														<TD align="right"><%=Target.ToString()%></TD>
														<TD align="right"><%=sale1%></TD>
														<TD align="right"><%=sale2%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<TD align="right"><%=Achivment.ToString()%></TD>
													</TR>
													<%
																	i++;
																}
																dtr1.Close();
																	%>
													<TR>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">Total</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<%
													}
													
												Month_Tot_Target=0;
												Month_Tot_CY=0;
												Month_Tot_LY=0;
												Month_Tot_Growth=0;
												Month_Tot_Achivent=0;
												
												Cumu_Tot_Target=0;
												Cumu_Tot_CY=0;
												Cumu_Tot_LY=0;
												Cumu_Tot_Growth=0;
												Cumu_Tot_Achivent=0;
												
												sale1=0;
												sale2=0;
												growth=0;
												Target=0;
												Achivment=0;
												GR=0;
												i=1;
												sql="select Prod_Id,Prod_Name+' : '+Pack_Type Product,Prod_Code from products  where (prod_name like 'GREASE WB%' or prod_name like 'GREASE MP%' or prod_name like 'Servo Grease MP3%' or prod_name like 'Servo Gem RR 3%') order by Prod_Name";
												dtr1=obj.GetRecordSet(sql);
												if(dtr1.HasRows)
												{
												%>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848" colSpan="2">
															<FONT color="#ffffff" size="1">SRSM-6 D</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM-6 D</FONT></TH>
														<TH bgColor="#ce4848" colSpan="12">
															<FONT color="#ffffff" size="1">SALES OF GREASES</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848" colSpan="6">
															<FONT color="#ffffff" size="1">
																<%=GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848" colSpan="6">
															<FONT color="#ffffff" size="1">CUM :
																<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SR. No.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Product Code</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GRADE</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TARGET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%ACHV.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TARGET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">CY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">LY</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%GR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">%ACHV.</FONT></TH></TR>
													<%
																while(dtr1.Read())
																{
																	%>
													<TR>
														<TD><%=i.ToString()%></TD>
														<TD><%=dtr1["Prod_code"].ToString()%></TD>
														<TD><%=dtr1["Product"].ToString()%></TD>
														<%
																		sale1=Month_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Month_LY_Sale(dtr1["Prod_id"].ToString());
																		Month_Tot_CY+=sale1;
																		Month_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																			Target=Math.Round((sale1*1.1),2);
																			Achivment=Math.Round(((sale1/Target)*100),2);
																			
																			if(Target!=0)
																				Achivment=Math.Round(((sale1/Target)*100),2);
																			else
																				Achivment=0;
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																			Target=0;
																			Achivment=0;
																		}
																		
																		%>
														<TD align="right"><%=Target.ToString()%></TD>
														<TD align="right"><%=sale1.ToString()%></TD>
														<TD align="right"><%=sale2.ToString()%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<TD align="right"><%=Achivment.ToString()%></TD>
														<%
																		sale1=0;
																		sale2=0;
																		sale1=Cumu_CY_Sale(dtr1["Prod_id"].ToString());
																		sale2=Cumu_LY_Sale(dtr1["Prod_id"].ToString());
																		Cumu_Tot_CY+=sale1;
																		Cumu_Tot_LY+=sale2;
																		if(sale2!=0 && sale2!=0)
																		{
																			growth=Math.Round((sale1-sale2),2);
																			GR=Math.Round(((growth/sale2)*100),2);
																			Target=Math.Round((sale1*1.1),2);
																			
																			if(Target!=0)
																				Achivment=Math.Round(((sale1/Target)*100),2);
																			else
																				Achivment=0;
																		}
																		else
																		{
																			growth=0;
																			GR=0;
																			Target=0;
																			Achivment=0;
																		}
																		
																		%>
														<TD align="right"><%=Target.ToString()%></TD>
														<TD align="right"><%=sale1%></TD>
														<TD align="right"><%=sale2%></TD>
														<TD align="right"><%=growth.ToString()%></TD>
														<TD align="right"><%=GR.ToString()%></TD>
														<TD align="right"><%=Achivment.ToString()%></TD>
													</TR>
													<%
																	i++;
																}
																dtr1.Close();
																	%>
													<TR>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">Total</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Month_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_CY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=Cumu_Tot_LY.ToString()%>
															</FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"></FONT>
														</TH>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<%
													}
													%>
									</asp:panel>
									<%
											}
											else if(dropType.SelectedIndex==4)
											{
											 
									%>
									<asp:panel id="panSRSM_9" Runat="server" Visible="False">
										<TR vAlign="top" height="20">
											<TH>
												<FONT color="#ce4848" size="1">SRSM 9</FONT></TH></TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="99%" align="center" border="1">
													<TR>
														<TD align="center" colSpan="14">OUTGO THROUGH INCENTIVE SCHEMES IN RETAIL LUBE 
															SALES -
															<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>
														</TD>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH><%
																for(int m=0;m<DateFrom.Length;m++)
																{
																	%>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">
																<%=GetMonthName(DateFrom[m].ToString())%>
															</FONT>
														</TH>
														<%
																}
																%>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Total</FONT>
														</TH>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Primery Sales Vol, KL (50 Ltrs &amp; Brls)</FONT></TH><%
															double total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=PS_Barel[m].ToString()%></TD>
														<%
														total+=PS_Barel[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Primery Sales Vol, KL (Smalls)</FONT></TH><%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=PS_Small[m].ToString()%></TD>
														<%
														total+=PS_Small[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Total Primery Sales Vol, KL</FONT></TH><%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=PS_Tot[m].ToString()%></TD>
														<%
														total+=PS_Tot[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Secondary Channel Incentive, Rs.</FONT></TH><%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=PS_Claim[m].ToString()%></TD>
														<%
														total+=PS_Claim[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">Resellers, Rs.</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>Product Discount</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>LFR &amp; 2T+4T to MS Ratio Scheme</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>4T PROMOTIONAL QUATERLY BASIS</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>ANNUAL GROWTH INCENTIVE</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>DIFF PROMOTIONAL DISCOUNT CLAIM PBS</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>DLP DIFF DISCOUNT</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>CAP KE NICHE CASH</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>QUATERLY GROWTH INCENTIVE</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SERVO BONANZA OFFER
														</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">OE SEGMENT, Rs.</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>VOLUME DISCOUNT</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>EQUIPMENT FINANCE INCL EICHER CAPEX</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">FLEET OWNERS, Rs.</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>VOLUME DISCOUNT</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>XTRAPOWER REWARD</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL CHANNEL INCENTIVE, Rs.</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA INCENTIVE, Rs.</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SSA TRADE DISCOUNT</TD>
														<%
														total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=Trade_Disc[m].ToString()%></TD>
														<%total+=Trade_Disc[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TD>SSA CASH DISCOUNT</TD>
														<%total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=Cash_Disc[m].ToString()%></TD>
														<%total+=Cash_Disc[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TD>SSA EARLY BIRD DISCOUNT</TD>
														<%total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=EB_Disc[m].ToString()%></TD>
														<%total+=EB_Disc[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TD>BAZZAR PROMOTION SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right">0</TD>
														<%
															}
															%>
														<TD align="right"><B>0</B></TD>
													</TR>
													<TR>
														<TD>KSK PROMOSCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right">0</TD>
														<%
															}
															%>
														<TD align="right"><B>0</B></TD>
													</TR>
													<TR>
														<TD>DISCOUNT ON 50 LTRS &amp; BRLS</TD>
														<%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=baral_Disc[m].ToString()%></TD>
														<%total+=baral_Disc[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TD>ADVANCE DISCOUNT ADJUST IN SSC</TD>
														<%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD align="right"><%=Fixed_Disc[m].ToString()%></TD>
														<%total+=Fixed_Disc[m];
															}
															%>
														<TD align="right"><B><%=total.ToString()%></B></TD>
													</TR>
													<TR>
														<TD>PGP INCENTIVE</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SGP INCENTIVE</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>ANNUAL GROWTH INCENTIVE</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>CONSISTENCY GROWTH INCENTIVE</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>PRODUCT PROMO DISCOUNT</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>DEBIT NOTE FOR 50 LTRS &amp; BRLS INCENTIVE NOT PASSED</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL SSA INCENTIVE, RS.</FONT></TH><%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff"><B>
																	<%=Tot_SSA_Inc1[m].ToString()%>
																</B></FONT>
														</TH>
														<%
																total+=Tot_SSA_Inc1[m];
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff"><B>
																	<%=total.ToString()%>
																</B></FONT>
														</TH>
													</TR>
													<TR>
														<TH align="left">
															<FONT size="1">SALES PROMOTION SCHEME, RS.</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SERVO SADBHAVNA SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>CUSTOMER PULL SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SERVO MECHANIC CLUB SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SERVO VISIBILTY SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SERVO GIVEAWAY SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>SERVO VAN PUBLICITY SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TD>REWARD SCHEME</TD>
														<%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TD>&nbsp;</TD>
														<%
															}
															%>
														<TD>&nbsp;</TD>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL OUTGO ON RETAIL SCHEME, RS.</FONT></TH><%
															total=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff"><B>
																	<%=Tot_SSA_Inc1[m].ToString()%>
																</B></FONT>
														</TH>
														<%total+=Tot_SSA_Inc1[m];
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">
																<%=total.ToString()%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL OUTGO</FONT></TH><%
															total=0;
															double tem=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																tem=Math.Round((Tot_SSA_Inc1[m]/PS_Barel[m]));
																total+=tem;
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">
																<%=tem.ToString()%>
															</FONT>
														</TH>
														<%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">
																<%=total.ToString()%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SECONDARY CHANNEL INCENTIVE</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH><%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH></TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RESELLER INCENTIVE</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH><%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH></TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">OE SEGMENT INCENTIVE</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH><%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH></TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">FLEET SEGMENT INCENTIVE</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH><%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH></TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA INCENTIVE</FONT></TH><%
															total=0;
															tem=0;
															for(int m=0;m<DateFrom.Length;m++)
															{
																tem=Math.Round((Tot_SSA_Inc1[m]/PS_Tot[m]));
																total+=tem;
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">
																<%=tem.ToString()%>
															</FONT>
														</TH>
														<%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">
																<%=total.ToString()%>
															</FONT>
														</TH>
													</TR>
													<TR>
														<TH align="left" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SALES PROMOTION SCHEMES</FONT></TH><%
															for(int m=0;m<DateFrom.Length;m++)
															{
																%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH><%
															}
															%>
														<TH align="right" bgColor="#ce4848">
															<FONT color="#ffffff">0</FONT></TH></TR>
												</TABLE>
											</TD>
										</TR>
									</asp:panel>
									<%
									}
									else if(dropType.SelectedIndex==5)
									{
									%>
									<asp:panel id="panSRSM_10_12" Runat="server" Visible="False">
										<TR vAlign="top" height="20">
											<TH>
												<FONT color="#ce4848" size="1">SRSM 10 &amp; 12</FONT></TH></TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="1">
													<TR>
														<TD align="center" colSpan="14">SALE IN SMALLS/BARRELS MONTH: NOV-12</TD>
													</TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM 10</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">CURRENT YEAR
																<BR>
																(<%=GetMonthName(EndDate.ToString())%>)</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">LAST YEAR
																<BR>
																(<%=GetMonthName(s2.ToString())%>)</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">CURRENT YEAR CUM
																<BR>
																(<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>)</FONT></TH>
														<TH bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">LAST YEAR CUM
																<BR>
																(<%=GetMonthName(s1.ToString())+" - "+GetMonthName(s2.ToString())%>)</FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SMALLS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">BARELS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SMALLS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">BARELS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SMALLS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">BARELS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SMALLS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">BARELS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD align="right"><%=CY_Small_M.ToString()%></TD>
														<TD align="right"><%=CY_Barel_M.ToString()%></TD>
														<TD align="right"><%=CY_Tot_M.ToString()%></TD>
														<TD align="right"><%=LY_Small_M.ToString()%></TD>
														<TD align="right"><%=LY_Barel_M.ToString()%></TD>
														<TD align="right"><%=LY_Tot_M.ToString()%></TD>
														<TD align="right"><%=CY_Small_C.ToString()%></TD>
														<TD align="right"><%=CY_Barel_C.ToString()%></TD>
														<TD align="right"><%=CY_Tot_C.ToString()%></TD>
														<TD align="right"><%=LY_Small_C.ToString()%></TD>
														<TD align="right"><%=LY_Barel_C.ToString()%></TD>
														<TD align="right"><%=LY_Tot_C.ToString()%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="80%" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SRSM 12</FONT></TH>
														<TH bgColor="#ce4848" colSpan="5">
															<FONT color="#ffffff" size="1">MONTHEND OUTSTANDING,Rs. Lacs</FONT></TH>
														<TH bgColor="#ce4848" rowSpan="2">
															<FONT color="#ffffff" size="1">MONTH END
																<BR>
																INVENTORY (KL)</FONT></TH>
														<TH bgColor="#ce4848" rowSpan="2">
															<FONT color="#ffffff" size="1">NO OF DAY'S<BR>
																COVERAGE<BR>
																(MIN 15 DAYS)</FONT></TH></TR>
													<TR>
														<TH width="20%" bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SSA/GSS</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">RO</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">BAZZAR</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">OE</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">FLEET</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL</FONT></TH></TR>
													<TR>
														<TD>SSA GWALIOR</TD>
														<TD align="right"><%=RO_OS.ToString()%></TD>
														<TD align="right"><%=BAZZAR_OS.ToString()%></TD>
														<TD align="right"><%=OE_OS.ToString()%></TD>
														<TD align="right"><%=FLEET_OS.ToString()%></TD>
														<TD align="right"><%=TOTAL_OS.ToString()%></TD>
														<TD align="right"><%=Closing_Stock.ToString()%></TD>
														<TD align="right"><%=NODC.ToString()%></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</asp:panel>
									<%
									}
									else if(dropType.SelectedIndex==6)
									{
									
										try
										{
											InventoryClass obj=new InventoryClass();
											
											i=1;
											sql="select cust_type,cust_name,city,cust_id from customer where cust_type='ESSAR RO' order by cust_name";
											dtr=obj.GetRecordSet(sql);
											if(dtr.HasRows)
											{
												%>
									<asp:panel id="panSRSM_14" Runat="server" Visible="False">
										<TR vAlign="top" height="20">
											<TH>
												<FONT color="#ce4848" size="1">SRSM 14</FONT></TH></TR>
										<TR>
											<TD align="center">ESSAR RO SALES (<%=GetMonthName(StartDate.ToString())+" - "+GetMonthName(EndDate.ToString())%>)</TD>
										</TR>
										<TR vAlign="top">
											<TD align="center">
												<TABLE cellSpacing="0" cellPadding="0" width="850" align="center" border="1">
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">&nbsp;</FONT></TH><%
																	for(int m=0;m<DateFrom.Length;m++)
																	{
																		%>
														<TH width="8%" bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="1">
																<%=GetMonthName(DateFrom[m].ToString())%>
															</FONT>
														</TH>
														<%
																	}
																	%>
														<TH width="8%" bgColor="#ce4848" colSpan="3">
															<FONT color="#ffffff" size="2"><B>Total</B></FONT></TH></TR>
													<TR>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">SL<BR>
																NO.</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">NAME OF ESSAR RO</FONT></TH><%
																		for(int m=0;m<DateFrom.Length;m++)
																		{
																			%>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">2T<BR>
																SALES</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">OTHER<BR>
																SALE</FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1">TOTAL<BR>
																SALE</FONT></TH><%
																		}
																		%>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"><B>2T<BR>
																	SALES</B></FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"><B>OTHER<BR>
																	SALES</B></FONT></TH>
														<TH bgColor="#ce4848">
															<FONT color="#ffffff" size="1"><B>TOTAL<BR>
																	SALES</B></FONT></TH></TR>
													<%
																		while(dtr.Read())
																		{
																			Total_2t=0;
																			Total_Other=0;
																			Total_Tot=0;
																			%>
													<TR>
														<TD><%=i.ToString()%></TD>
														<TD><%=dtr["cust_name"].ToString()%></TD>
														<%for(int m=0;m<DateFrom.Length;m++)
																				{
																					InventoryClass obj1=new InventoryClass();
																					sql="select distinct sum(v.oil2t) TT_sale,sum(v.totalqty - v.oil2t) OTH_sale,sum(v.totalqty) Tot_sale from customer c,vw_Salesoil v,sales_master sm where c.cust_id=sm.cust_id and c.cust_id='"+dtr["cust_id"].ToString()+"' and cust_type='ESSAR RO' and v.invoice_no=sm.invoice_no and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+DateFrom[m].ToString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+DateTo[m].ToString()+"' group by cust_type,cust_name,city,city";
																					dtr1=obj1.GetRecordSet(sql);
																					if(dtr1.Read())
																					{
																						%>
														<TD align="right"><%=Math.Round(double.Parse(dtr1["TT_sale"].ToString()),2)%></TD>
														<TD align="right"><%=Math.Round(double.Parse(dtr1["OTH_sale"].ToString()),2)%></TD>
														<TD align="right"><%=Math.Round(double.Parse(dtr1["Tot_sale"].ToString()),2)%></TD>
														<%
																							Total_2t+=Math.Round(double.Parse(dtr1["TT_sale"].ToString()),2);
																							Total_Other+=Math.Round(double.Parse(dtr1["OTH_sale"].ToString()),2);
																							Total_Tot+=Math.Round(double.Parse(dtr1["Tot_sale"].ToString()),2);
																					}
																					else
																					{
																						%>
														<TD align="right">0</TD>
														<TD align="right">0</TD>
														<TD align="right">0</TD>
														<%
																					}
																					dtr1.Close();
																				}
																				%>
														<TD align="right"><B><%=Total_2t.ToString()%></B></TD>
														<TD align="right"><B><%=Total_Other.ToString()%></B></TD>
														<TD align="right"><B><%=Total_Tot.ToString()%></B></TD>
														<%
																				i++;
																			}
																			dtr.Close();
																			%>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</asp:panel>
									<%
															}
														}
														catch(Exception ex)
														{
															MessageBox.Show(ex.Message.ToString());
														}
													}
												%>
								</TBODY>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer><asp:validationsummary id="vs1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>
