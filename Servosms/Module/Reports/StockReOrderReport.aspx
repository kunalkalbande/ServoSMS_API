<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.StockReOrderReport" CodeFile="StockReOrderReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>StockReOrderReport</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
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
				<TR>
					<TH align="center">
						<font color="#CE4848">Stock ReOrdering Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">Stock as On&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:textbox id="txtDateFrom" runat="server" Width="100px" BorderStyle="Groove" ReadOnly="True" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
								border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:button id="btnShow" runat="server" 
							Text="View   " Width="75px" onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="BtnPrint" Text="Print  "
							Width="75px" Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnExcel" Text="Excel"
							Width="75px" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr>
					<td align="center">
						<TABLE>
							<TR>
								<TD align="center" colSpan="5" height="200"><asp:datagrid id="GridReport" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" OnSortCommand="SortCommand_Click"
										AllowSorting="True" CellSpacing="1" CellPadding="1" BorderWidth="0px" BorderStyle="None" AutoGenerateColumns="False">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="SKU Name With Pack"></asp:BoundColumn>
											<asp:TemplateColumn SortExpression="closing_stock" HeaderText="Stock as On">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetStock(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="minlabel" SortExpression="minlabel" HeaderText="Min Level">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="reorderlable" SortExpression="reorderlable" HeaderText="ReOrder Level">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="maxlabel" SortExpression="maxlabel" HeaderText="Max Level">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center"
											ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				<tr>
					<td align="right"></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
