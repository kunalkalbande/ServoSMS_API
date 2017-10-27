<%@ Page language="c#" Inherits="Servosms.Module.Reports.PurchaseListIOCL" CodeFile="PurchaseListIOCL.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PurchaseListIOCL</title>
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
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox>
			<table height="290" width="778" align="center" border="0">
				<TBODY>
					<TR>
						<TH vAlign="top" height="20">
							<font color="#ce4848">Purchase List For IOCL</font>&nbsp;
							<hr>
						</TH>
					</TR>
					<TR>
						<TD vAlign="top" align="center" height="20">
							<TABLE width="620">
								<TR>
									<TD vAlign="middle" align="center"></TD>
									<TD vAlign="top" align="center"></TD>
									<TD vAlign="middle" align="center">Date From</TD>
									<TD vAlign="top"><asp:textbox id="txtDateFrom" runat="server" Width="110px" CssClass="fontstyle" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD vAlign="middle" align="center" colSpan="1" rowSpan="1">To</TD>
									<TD vAlign="top"><asp:textbox id="txtDateTo" runat="server" Width="110px" CssClass="fontstyle" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD align="center" colSpan="11"><asp:button id="cmdrpt" runat="server" Width="60px" 
											Text="View " onclick="cmdrpt_Click"></asp:button>&nbsp;&nbsp;&nbsp;
										<asp:button id="BtnPrint" Width="60px" 
											Text="Print" Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;
										<asp:button id="btnExcel" Width="60px" 
											Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD vAlign="top" align="center"><asp:datagrid id="PurchaseGrid" runat="server" BorderStyle="None" BackColor="#DEBA84" BorderColor="#DEBA84"
								AllowSorting="True" CellSpacing="1" OnSortCommand="sortcommand_click2" OnItemDataBound="ItemTotal" ShowFooter="True" AutoGenerateColumns="False"
								BorderWidth="0px" CellPadding="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="vndr_invoice_no" SortExpression="vndr_invoice_no" HeaderText="Invoice No."
										FooterText="Total:">
										<HeaderStyle Width="60px"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Left"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="vndr_invoice_date" SortExpression="invoice_date" HeaderText="Invoice Date"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="totalqtyltr" SortExpression="totalqtyltr" HeaderText="Qty In Ltr">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Net_Amount" SortExpression="Net_Amount" HeaderText="Total Invoice Amount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Trade_discount" SortExpression="Trade_discount" HeaderText="Trade&lt;br&gt;Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Cash Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#discountfocDisc(DataBinder.Eval(Container.DataItem,"invoice_no").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<%=Cache["CashDisctotal"].ToString()%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ebird_discount" SortExpression="ebird_discount" HeaderText="EBird&lt;br&gt;Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="FOC Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#discountfocDiscB(DataBinder.Eval(Container.DataItem,"invoice_no").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<%=Cache["focDisctotal"].ToString()%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="fixed_discount_type" SortExpression="fixed_discount_type" HeaderText="Discount (%)">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Promo_Scheme" SortExpression="Promo_Scheme" HeaderText="Add. Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Old Rate Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#discountfocDiscC(DataBinder.Eval(Container.DataItem,"invoice_no").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<%=Cache["Disctotal"]%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Fixed Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#discountfixdDisc(DataBinder.Eval(Container.DataItem,"invoice_no").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<%=Cache["fixd_Disctotal"]%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Total&lt;br&gt;Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<!--%#discountfocDiscA(DataBinder.Eval(Container.DataItem,"invoice_no").ToString())%-->
											<%#GetTotal(DataBinder.Eval(Container.DataItem,"trade_discount").ToString(),discountfocDisc1(DataBinder.Eval(Container.DataItem,"invoice_no").ToString()),DataBinder.Eval(Container.DataItem,"ebird_discount").ToString(),discountfocDiscB1(DataBinder.Eval(Container.DataItem,"invoice_no").ToString()),discountfocDiscC1(DataBinder.Eval(Container.DataItem,"invoice_no").ToString()),DataBinder.Eval(Container.DataItem,"Promo_Scheme").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<!--%=Cache["fixedDisctotal"]%-->
											<%=Cache["TotDis"]%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="FOC Quantity">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#foc_qty(DataBinder.Eval(Container.DataItem,"invoice_no").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<%=Cache["foc_qty_tot"]%>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TBODY>
			</table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
