<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.FleetOeDiscount" CodeFile="FleetOeDiscountReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FleetOeDiscount</title>
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
				Width="8px" Visible="False"></asp:textbox>
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<TBODY>
					<TR>
						<TH style="HEIGHT: 4px" align="center">
							<font color="#ce4848">Fleet/OE Discount Report</font>
							<hr>
						</TH>
					</TR>
					<tr>
						<td vAlign="top" align="center">
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
								<TBODY>
									<TR>
										<TD align="center" width="10%">Date From</TD>
										<TD align="center"><asp:textbox id="txtDateFrom" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
												CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A></TD>
										<TD align="center" width="10%">Date To</TD>
										<TD align="center"><asp:textbox id="Textbox1" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A></TD>
										<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;Customer Type&nbsp;&nbsp;
											<asp:dropdownlist id="droptype" runat="server" Width="80px" CssClass="fontstyle">
												<asp:ListItem Value="Both" Selected="True">Both</asp:ListItem>
												<asp:ListItem Value="Fleet">Fleet</asp:ListItem>
												<asp:ListItem Value="OE">OE</asp:ListItem>
											</asp:dropdownlist>&nbsp;&nbsp;&nbsp;
											<asp:button id="btnShow" runat="server" Width="60px" Text="View" 
												 onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="60px" Text="Print " 
												 Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Width="60px" Text="Excel" 
												 Runat="server" onclick="btnExcel_Click"></asp:button></TD>
									</TR>
									<%if(GridSalesReport.Visible==true){%>
									<tr>
										<th align="center" colSpan="5">
											Fleet Discount Report For The Period Of
											<%=txtDateFrom.Text%>
											To
											<%=Textbox1.Text%>
										</th>
									</tr>
									<%}%>
									<TR>
										<TD align="center" colSpan="5"><asp:datagrid id="GridSalesReport" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
												OnItemDataBound="ItemDataBound" ShowFooter="True" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" CellSpacing="1"
												AllowSorting="True" OnSortCommand="sortcommand_click">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
												<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No." FooterText="Total">
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<FooterStyle Font-Bold="True"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
														DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
													<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Customer Category"></asp:BoundColumn>
													<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer &#160;Name"></asp:BoundColumn>
													<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place"></asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name"></asp:BoundColumn>
													<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Package Type"></asp:BoundColumn>
													<asp:TemplateColumn SortExpression="Qty" HeaderText="Quantity Ltr.">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="ffffff">Quantity</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Nos.</font></td>
																	<td align="right"><font color="#ffffff">Ltr</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Qty").ToString()%></font></td>
																	<td align="right"><font color="#8C4510"><%#Multiply11qty(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))%></font></td>
																</tr>
															</table>
															<!--%#Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))%-->
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td align="left">&nbsp;</td>
																	<td align="right"><b><font color="#8C4510"><%=Cache["totalltrqty"]%></font></b></td>
																</tr>
															</table>
															<!--%=Cache["totalltr"]%-->
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Product Value">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<ItemTemplate>
															<%#GetProductValue(DataBinder.Eval(Container.DataItem,"Rate").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
														<FooterTemplate>
															<%=Cache["ProdValue2"].ToString()%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="foe" HeaderText="Discount(F/Oe)">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center" width="100"><font color="ffffff">Discount Given</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="#ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#addfoetype(DataBinder.Eval(Container.DataItem,"foe").ToString(),DataBinder.Eval(Container.DataItem,"FoeType").ToString())%></font></td>
																	<td align="right"><font color="#8C4510"><%#DisGivenfleet(DataBinder.Eval(Container.DataItem,"foe").ToString(),(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))).ToString(),Cache["ProdValue"].ToString(),DataBinder.Eval(Container.DataItem,"FoeType").ToString())%></font></td>
																</tr>
															</table>
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="right"><font color="#8C4510"><b><%=Cache["Disfleet"].ToString()%></b></font></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Prod_Name" HeaderText="FOC">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="#ffffff">FOC</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="#ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())%></font></td>
																	<td align="right"><font color="#8C4510"><%#FOCcost12(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString()))%></font></td>
																</tr>
															</table>
															<!--%#schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())%-->
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="right"><font color="#8C4510"><b><%=Cache["FOCcost"].ToString()%></b></font></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Prod_Name" HeaderText="scheme">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="#ffffff">Scheme</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="#ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())+schdiscount1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())%></font></td>
																	<td align="right"><font color="#8C4510"><%#Multiplyclaim(schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()).ToString(),Multiply11(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")).ToString(),Cache["ProdValue"].ToString(),schdiscount1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()).ToString())%></font></td>
																</tr>
															</table>
															<!--%#schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())%-->
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="right"><font color="#8C4510"><b><%=Cache["Mulclaim"].ToString()%></b></font></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Qty" HeaderText="Claim Amount">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="ffffff">Claim Amount</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<!--td align="left"><font color="#8C4510"><%#discountdis14(DisGiven(DataBinder.Eval(Container.DataItem,"schdiscount").ToString(),(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))).ToString()),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString()),schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()))%></font></td-->
																	<td align="left"><font color="#8C4510"><%#discountdis14(DataBinder.Eval(Container.DataItem,"foe").ToString(),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString()),schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()))%></font></td>
																	<td align="right"><font color="#8C4510"><%#discountdis13(DisGiven3(DataBinder.Eval(Container.DataItem,"foe").ToString(),(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))).ToString(),Cache["ProdValue"].ToString(),DataBinder.Eval(Container.DataItem,"FoeType").ToString()),FOCcost(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())),Multiply222(schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()).ToString(),Multiply11(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")).ToString(),Cache["ProdValue"].ToString(),schdiscount1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())))%></font></td>
																</tr>
															</table>
															<!--%#discountdis(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")),DataBinder.Eval(Container.DataItem,"foe").ToString())%-->
															<!--%#discountdis(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")),DataBinder.Eval(Container.DataItem,"foe").ToString(),schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()))%-->
														</ItemTemplate>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><b><!--%=Cache["total14"]%--></b></font></td>
																	<td align="right"><font color="#8C4510"><b><%=Cache["total13"]%></b></font></td>
																</tr>
															</table>
															<!--%=Cache["total"]%-->
														</FooterTemplate>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></TD>
									</TR>
									<%if(Datagrid1.Visible==true){%>
									<tr>
										<th align="center" colSpan="5">
											OE Discount Report For The Period Of
											<%=txtDateFrom.Text%>
											To
											<%=Textbox1.Text%>
										</th>
									</tr>
									<%}%>
									<TR>
										<TD align="center" colSpan="5"><asp:datagrid id="Datagrid1" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
												OnItemDataBound="ItemDataBound" ShowFooter="True" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" CellSpacing="1"
												AllowSorting="True" OnSortCommand="sortcommand_click1">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
												<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No." FooterText="Total">
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<FooterStyle Font-Bold="True"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
														DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
													<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Customer Category"></asp:BoundColumn>
													<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name"></asp:BoundColumn>
													<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place"></asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name"></asp:BoundColumn>
													<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Package Type"></asp:BoundColumn>
													<asp:TemplateColumn SortExpression="Qty" HeaderText="Quantity Ltr.">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="ffffff">Quantity</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="ffffff">Nos.</font></td>
																	<td align="right"><font color="ffffff">Ltr.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Qty").ToString()%></font></td>
																	<td align="right"><font color="#8C4510"><%#Multiply1(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))%></font></td>
																</tr>
															</table>
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td align="left">&nbsp;</td>
																	<td align="right"><b><font color="#8C4510"><%=Cache["totalltr1"]%></font></b></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Product Value">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<ItemTemplate>
															<%#GetProductValue1(DataBinder.Eval(Container.DataItem,"Rate").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
														<FooterTemplate>
															<%=Cache["ProdValue1"].ToString()%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="foe" HeaderText="Discount(F/Oe)">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center" width="100"><font color="ffffff">Discount Given</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="#ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#addfoetype(DataBinder.Eval(Container.DataItem,"foe").ToString(),DataBinder.Eval(Container.DataItem,"FoeType").ToString())%></font></td>
																	<td align="right"><font color="#8C4510"><%#DisGivenoe(DataBinder.Eval(Container.DataItem,"foe").ToString(),(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))).ToString(),Cache["ProdValueoe"].ToString(),DataBinder.Eval(Container.DataItem,"FoeType").ToString())%></font></td>
																</tr>
															</table>
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="right"><font color="#8C4510"><b><%=Cache["Disoe"].ToString()%></b></font></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Prod_Name" HeaderText="FOC">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="#ffffff">FOC</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="#ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())%></font></td>
																	<td align="right"><font color="#8C4510"><%#FOCcost1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString()))%></font></td>
																</tr>
															</table>
															<!--%#schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())%-->
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="right"><font color="#8C4510"><b><%=Cache["FOC1"].ToString()%></b></font></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Prod_Name" HeaderText="scheme">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="#ffffff">Scheme</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="#ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="#ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())+schdiscount1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())%></font></td>
																	<td align="right"><font color="#8C4510"><%#Multiplyclaimoe(schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()).ToString(),Multiply11oe(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")).ToString(),Cache["ProdValueoe"].ToString(),schdiscount1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()).ToString())%></font></td>
																</tr>
															</table>
															<!--%#schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())%-->
														</ItemTemplate>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="right"><font color="#8C4510"><b><%=Cache["Mulclaimoe"].ToString()%></b></font></td>
																</tr>
															</table>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="Qty" HeaderText="Claim Amount">
														<HeaderTemplate>
															<table width="100%">
																<tr>
																	<td colspan="2" align="center"><font color="ffffff">Claim Amount</font></td>
																</tr>
																<tr>
																	<td align="left"><font color="ffffff">Rs./Ltr</font></td>
																	<td align="right"><font color="ffffff">Rs.</font></td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><%#discountdis14(DataBinder.Eval(Container.DataItem,"foe").ToString(),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString()),schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()))%></font></td>
																	<td align="right"><font color="#8C4510"><%#discountdis13oe(DisGiven3(DataBinder.Eval(Container.DataItem,"foe").ToString(),(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type"))).ToString(),Cache["ProdValueoe"].ToString(),DataBinder.Eval(Container.DataItem,"FoeType").ToString()),FOCcost(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),FOCdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())),Multiply222(schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()).ToString(),Multiply11(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")).ToString(),Cache["ProdValueoe"].ToString(),schdiscount1(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString())))%></font></td>
																</tr>
															</table>
															<!--%#discountdis(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")),DataBinder.Eval(Container.DataItem,"foe").ToString())%-->
															<!--%#discountdis(Multiply(DataBinder.Eval(Container.DataItem,"Qty").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Pack_Type")),DataBinder.Eval(Container.DataItem,"foe").ToString(),schdiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_Type").ToString()))%-->
														</ItemTemplate>
														<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
														<FooterTemplate>
															<table width="100%">
																<tr>
																	<td align="left"><font color="#8C4510"><b><!--%=Cache["total14"]%--></b></font></td>
																	<td align="right"><font color="#8C4510"><b><%=Cache["total13oe"]%></b></font></td>
																</tr>
															</table>
															<!--%=Cache["total"]%-->
														</FooterTemplate>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></TD>
									</TR>
								</TBODY>
							</TABLE>
							<asp:validationsummary id="ValidationSummary1" runat="server" Width="778px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
					</tr>
					<!--TR>
						<TD align="left"><FONT color="#ff0033"><STRONG><U>Note</U>:</STRONG>&nbsp;</FONT><FONT color="black">
								To take a printout press the above Print button, to redirect the output to a 
								new page. Use the Page Setup option in the File menu to set the appropriate
								<BR>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; margins, 
								then use the Print option in the file menu to send the output to the printer. </FONT>
						</TD>
					</TR--></TBODY></table>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
