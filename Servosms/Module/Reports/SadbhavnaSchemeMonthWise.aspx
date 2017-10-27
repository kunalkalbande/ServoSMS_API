<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SadbhavnaSchemeMonthWise" CodeFile="SadbhavnaSchemeMonthWise.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SadbhavnaSchemeMonthWise</title>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TBODY>
					<TR height="10">
						<TH align="center">
							<!--font color="#CE4848">
								<%=str1%>
							</font><FONT color="#CE4848">
								<br>
								Customer Code:<%=str2%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
								SAP Code:
								<%=str3%>
							</FONT-->
							<font color="#ce4848">Servo Sadbhavna Scheme Month Wise Sales &amp; Points Report</font>
							<hr>
						</TH>
					</TR>
					<tr>
						<td vAlign="top" align="center">
							<TABLE id="Table1">
								<TBODY>
									<TR>
										<td>Customer Type</td>
										<td><asp:DropDownList ID="DropCustType" CssClass="fontstyle" Runat="server">
												<asp:ListItem Value="All">All</asp:ListItem>
												<asp:ListItem Value="Bazzar">Bazzar</asp:ListItem>
												<asp:ListItem Value="N-KSK">N-KSK</asp:ListItem>
												<asp:ListItem Value="KSK">KSK</asp:ListItem>
												<asp:ListItem Value="Essar ro">Essar ro</asp:ListItem>
											</asp:DropDownList></td>
										<TD width="100" align="center">Date From</TD>
										<TD align="center"><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" Width="60px" BorderStyle="Groove"
												CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A>
											<asp:requiredfieldvalidator id="rfvDateFrom" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Please Select From Date From the Calender">*</asp:requiredfieldvalidator></TD>
										<TD align="center" width="100">Date To</TD>
										<TD align="center"><asp:textbox id="Textbox1" runat="server" ReadOnly="True" Width="60px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A>
											<asp:requiredfieldvalidator id="rfvDateTo" runat="server" ControlToValidate="Textbox1" ErrorMessage="Please Select To Date From the Calender">*</asp:requiredfieldvalidator></TD>
										<TD><asp:button id="btnShow" runat="server" Width="60px" 
												Text="View" onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="60px" 
												Text="Print " Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Width="60px" 
												Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></TD>
									</TR>
									<tr>
										<TD align="center" colSpan="7"><font color="#ce4848">Vat Inclusive Invoice Amount</font></TD>
									</tr>
									<TR>
										<TD align="center" colSpan="7"><asp:datagrid id="GridSalesReport" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84" AllowSorting="True"
												CellSpacing="1" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px" CellPadding="1" ShowFooter="True" OnSortCommand="sortcommand_click"
												OnItemDataBound="ItemTotal">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
												<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="s1" HeaderText="UniqueCode" FooterText="Total:">
														<FooterStyle Font-Bold="True"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="s2" SortExpression="s2" HeaderText="Customer Name"></asp:BoundColumn>
													<asp:BoundColumn DataField="s3" SortExpression="s3" HeaderText="Place"></asp:BoundColumn>
													<asp:BoundColumn DataField="s4" SortExpression="s4" HeaderText="Customer&lt;br&gt; Category"></asp:BoundColumn>
													<asp:BoundColumn DataField="s5" SortExpression="s5" HeaderText="Invoice No.">
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="s6" SortExpression="s6" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
													<asp:BoundColumn DataField="s7" HeaderText="Quantity Ltr." DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="s8" HeaderText="Invoice&lt;br&gt;Amount" DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="IneligibleGrades&lt;br&gt;Value" DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Basic&lt;br&gt;Points" DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Bonus&lt;br&gt;Points" DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Special&lt;br&gt;Points" DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Total&lt;br&gt;Points" DataFormatString="{0:N2}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></TD>
									</TR>
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
