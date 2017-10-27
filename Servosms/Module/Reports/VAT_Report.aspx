<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.VAT_Report" CodeFile="VAT_Report.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: VAT Report</title> <!--
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
		<script language="javascript">
		function CheckPurchaseSum()
		{
			if(document.Form1.txtValue!=null)
			{
				if(document.Form1.txtValue.value!="")
				{
					if(document.Form1.txtValue.value!="-" && document.Form1.txtValue.value!=".")
					{
						document.Form1.txtSumPurchase.value=eval(document.Form1.txtValue.value)+eval(document.Form1.tempTotalPurchase.value)
						makeRound(document.Form1.txtSumPurchase)
						document.Form1.txtNetAmount.value=eval(document.Form1.txtSumPurchase.value)-eval(document.Form1.tempTotalSale.value)
						makeRound(document.Form1.txtNetAmount)
					}
					if(document.Form1.txtSumPurchase.value=="NaN")
					{
						document.Form1.txtSumPurchase.value=eval(document.Form1.tempTotalPurchase.value)
						makeRound(document.Form1.txtSumPurchase)
						document.Form1.txtNetAmount.value=eval(document.Form1.txtSumPurchase.value)-eval(document.Form1.tempTotalSale.value)
						makeRound(document.Form1.txtNetAmount)
					}
				}
				else
				{
					document.Form1.txtSumPurchase.value=eval(document.Form1.tempTotalPurchase.value);
					makeRound(document.Form1.txtSumPurchase)
					document.Form1.txtNetAmount.value=eval(document.Form1.txtSumPurchase.value)-eval(document.Form1.tempTotalSale.value)
					makeRound(document.Form1.txtNetAmount)
				}
			}
		}
		
		function makeRound(t)
		{
			var str = t.value;
			if(str != "")
			{
				str = eval(str)*100;
				str  = Math.round(str);
				str = eval(str)/100;
				t.value = str;
			}
		}
		
		function getTempValue()
		{
			if(document.Form1.txtValue!=null)
			{
				if(document.Form1.txtValue.value!="")
				{
					if(document.Form1.txtValue.value!="-" && document.Form1.txtValue.value!=".")
						document.Form1.tempvalue.value=eval(document.Form1.txtValue.value)
				}
			}
		}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox>
			<table height="290" width="778" align="center" border="0">
				<TBODY>
					<TR>
						<TH vAlign="top" height="20">
							<font color="#ce4848">VAT Report</font>
							<hr>
						</TH>
					</TR>
					<TR>
						<TD vAlign="top" align="center" height="20">
							<TABLE width="650" border="0">
								<TR>
									<TD>Date From</TD>
									<TD><asp:textbox id="txtDateFrom" runat="server" Width="100px" BorderStyle="Groove" ReadOnly="True"
											CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD align="center">To</TD>
									<TD><asp:textbox id="txtDateTo" runat="server" Width="90px" BorderStyle="Groove" ReadOnly="True"
											CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD colSpan="2"><asp:radiobutton id="RadDetails" Checked="True" GroupName="Radio" Text="Details" Runat="server" AutoPostBack="True" oncheckedchanged="RadDetails_CheckedChanged"></asp:radiobutton>&nbsp;&nbsp;<asp:radiobutton id="RadSummarized" GroupName="Radio" Text="Summarized" Runat="server" AutoPostBack="True" oncheckedchanged="RadSummarized_CheckedChanged"></asp:radiobutton></TD>
								</TR>
								<TR>
									<asp:Panel ID="PanReport" Runat="server">
										<TD>Report Type</TD>
										<TD>
											<asp:dropdownlist id="DropReportType" runat="server" Width="125px" CssClass="fontstyle">
												<asp:ListItem Value="Both" Selected="True">Both</asp:ListItem>
												<asp:ListItem Value="Sales Report">Sales Report</asp:ListItem>
												<asp:ListItem Value="Purchase Report">Purchase Report</asp:ListItem>
											</asp:dropdownlist></TD>
										<TD align="center">Report Category</TD>
										<TD>
											<asp:dropdownlist id="DropReportCategory" runat="server" Width="116px" CssClass="fontstyle">
												<asp:ListItem Value="Both" Selected="True">Both</asp:ListItem>
												<asp:ListItem Value="VAT">VAT</asp:ListItem>
												<asp:ListItem Value="Non VAT">Non VAT</asp:ListItem>
											</asp:dropdownlist></TD>
									</asp:Panel>
									<%if(PanReport.Visible==true){%>
									<TD align="right" colSpan="2">
										<%}else{%>
									<TD align="right" colSpan="6"><%}%>
										<asp:button id="cmdrpt" runat="server" Width="60px" Text="View " 
											 onclick="cmdrpt_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="60px" Text="Print" Runat="server" 
											 onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Width="60px" Text="Excel" Runat="server"
											 onclick="btnExcel_Click"></asp:button></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center"></TD>
					</TR>
					<TR>
						<TD align="center">
							<P><u><asp:label id="lblSalesHeading" runat="server" Visible="False" Font-Size="X-Small" Font-Bold="True"><font color="#CE4848">Detailed 
											Vat Report for Complete Party Wise/ Invoice Wise Sales</font></asp:label></u></P>
							<asp:datagrid id="SalesGrid" runat="server" Visible="False" BorderStyle="None" BorderColor="#DEBA84"
								BackColor="#DEBA84" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" ShowFooter="True"
								OnItemDataBound="ItemTotal" OnSortCommand="sortcommand_click1" CellSpacing="1" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No." FooterText="Total:">
										<HeaderStyle Width="60px"></HeaderStyle>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="invoice_date" SortExpression="invoice_date" HeaderText="Invoice Date"
										DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name">
										<HeaderStyle Width="150px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Tin_No" SortExpression="Tin_No" HeaderText="Tin No.">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Grand_Total" SortExpression="Grand_Total" HeaderText="Product Value"
										DataFormatString="{0:N2}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Total Discount">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#GetSaleDiscount(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString()).ToString()%>
										</ItemTemplate>
										<FooterTemplate>
											<%=Total.ToString()%>
										</FooterTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="VAT_Amount" SortExpression="VAT_Amount" HeaderText="VAT" DataFormatString="{0:N2}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Net_Amount" SortExpression="Net_Amount" HeaderText="Total Invoice Amount"
										DataFormatString="{0:N2}">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<P></P>
							<P><u><asp:label id="lblPurchaseHeading" runat="server" Visible="False" Font-Size="X-Small" Font-Bold="True"><font color="#CE4848">Detailed 
											Vat Report for Complete Party Wise/ Invoice Wise Purchase</font></asp:label></u></P>
							</U>
							<P><asp:datagrid id="PurchaseGrid" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
									CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="ItemTotal1"
									OnSortCommand="sortcommand_click2" CellSpacing="1" AllowSorting="True">
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
									<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
									<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
									<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
									<Columns>
										<asp:BoundColumn DataField="Vndr_Invoice_No" SortExpression="Vndr_Invoice_No" HeaderText="Invoice No."
											FooterText="Total:">
											<HeaderStyle Width="60px"></HeaderStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Left"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="invoice_date" SortExpression="invoice_date" HeaderText="Invoice Date"
											DataFormatString="{0:dd/MM/yyyy}">
											<HeaderStyle Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Supp_Name" SortExpression="Supp_Name" HeaderText="Vendor Name">
											<HeaderStyle Width="150px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place">
											<HeaderStyle Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Tin_No" SortExpression="Tin_No" HeaderText="Tin No.">
											<HeaderStyle Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Grand_Total" SortExpression="Grand_Total" HeaderText="Product Value"
											DataFormatString="{0:N2}">
											<HeaderStyle Width="60px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right"></ItemStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Entry_Tax1" SortExpression="Entry_Tax1" HeaderText="Entry Tax" DataFormatString="{0:N2}">
											<HeaderStyle Width="60px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Right"></ItemStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										</asp:BoundColumn>
										<asp:TemplateColumn HeaderText="Total Discount">
											<ItemStyle HorizontalAlign="Right"></ItemStyle>
											<ItemTemplate>
												<%#GetPurDiscount(DataBinder.Eval(Container.DataItem,"Vndr_Invoice_No").ToString()).ToString()%>
											</ItemTemplate>
											<FooterTemplate>
												<%=Total_Pur.ToString()%>
											</FooterTemplate>
											<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										</asp:TemplateColumn>
										<asp:BoundColumn DataField="VAT_Amount" SortExpression="VAT_Amount" HeaderText="VAT" DataFormatString="{0:N2}">
											<ItemStyle HorizontalAlign="Right"></ItemStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="Net_Amount" SortExpression="Net_Amount" HeaderText="Total Invoice Amount"
											DataFormatString="{0:N2}">
											<ItemStyle HorizontalAlign="Right"></ItemStyle>
											<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										</asp:BoundColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></P>
							<P><u><asp:label id="Label1" runat="server" Visible="False" Font-Size="X-Small" Font-Bold="True"><font color="#CE4848">Detailed 
											Vat Tax Calculation for Quarterly Return</font></asp:label></u></P>
							</U>
							<P><%if(RadSummarized.Checked){if(tempMonth.Count>0){%>
								<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="80%" border="1">
									<%//string[] DiffMon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};%>
									<tr bgColor="#ce4848">
										<th rowSpan="2">
											<font color="white">Month</font></th>
										<th colSpan="2">
											<font color="white">Sales Accounts</font></th>
										<th colSpan="2">
											<font color="white">Purchase Accounts</font></th></tr>
									<tr bgColor="#ce4848">
										<td align="center"><font color="white">Sales Amount</font></td>
										<td align="center"><font color="white">Vat Output On Sales</font></td>
										<td align="center"><font color="white">Purchase Amount</font></td>
										<td align="center"><font color="white">Vat Input On Purchase</font></td>
									</tr>
									<%
       Total_Sales=0;
		Total_Purchase=0;
		Total_PurVar=0;
		Total_SalVat=0;
       for(int i=0;i<tempMonth.Count;i++){%>
									<tr>
										<td align="center"><%=tempMonth[i].ToString()%></td>
										<td align="right"><%=getSales(getMonthName(tempMonth[i].ToString()))%>&nbsp;</td>
										<td align="right"><%=getVatSale(getMonthName(tempMonth[i].ToString()))%>&nbsp;</td>
										<td align="right"><%=getPurchase(getMonthName(tempMonth[i].ToString()))%>&nbsp;</td>
										<td align="right"><%=getVatPurchase(getMonthName(tempMonth[i].ToString()))%>&nbsp;</td>
									</tr>
									<%}%>
									<tr bgColor="#ce4848">
										<td align="center"><font color="white">Total</font></td>
										<td align="right"><font color="white"><%=GenUtil.strNumericFormat(Total_Sales.ToString())%></font>&nbsp;</td>
										<td align="right"><font color="white"><%=GenUtil.strNumericFormat(Total_SalVat.ToString())%></font>&nbsp;</td>
										<td align="right"><font color="white"><%=GenUtil.strNumericFormat(Total_Purchase.ToString())%></font>&nbsp;</td>
										<td align="right"><font color="white"><%=GenUtil.strNumericFormat(Total_PurVar.ToString())%></font>&nbsp;</td>
									</tr>
									<tr>
										<td colSpan="5">
											<table width="100%" border="0">
												<tr>
													<td colSpan="5">&nbsp;</td>
												</tr>
												<tr>
													<td width="80">&nbsp;</td>
													<td colSpan="2">Opening Input Rebate as on
														<%=txtDateFrom.Text%>
													</td>
													<td width="100"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, true,true);" id="txtValue" onkeyup="CheckPurchaseSum()"
															style="TEXT-ALIGN: right" Width="90px" BorderStyle="Groove" Runat="server" MaxLength="15" CssClass="fontstyle"
															value="0"></asp:textbox></td>
													<td width="150">&nbsp;</td>
												</tr>
												<tr>
													<td></td>
													<td colSpan="2">Add: Vat Paid on Purchase</td>
													<td align="right" width="100"><input 
                  id=tempTotalPurchase type=hidden 
                  value="<%=Total_PurVar.ToString()%>" name=tempTotalPurchase 
                   ><%=Total_PurVar.ToString()%>&nbsp;&nbsp;&nbsp;&nbsp;</td>
													<td></td>
												</tr>
												<tr>
													<td colSpan="3">&nbsp;</td>
													<td width="100">
														<hr>
													</td>
													<td></td>
												</tr>
												<tr>
													<td colSpan="3">&nbsp;</td>
													<td width="100">&nbsp;
														<%if(txtValue.Text!=""){%>
														<input 
                  class=fontstyle 
                  style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none" 
                  readOnly type=text size=12 
                  value="<%=System.Convert.ToString(double.Parse(txtValue.Text)+Total_PurVar)%>" name=txtSumPurchase 
                  ><%}else{%>
														<input 
                  class=fontstyle 
                  style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none" 
                  readOnly type=text size=12 
                  value="<%=Total_PurVar.ToString()%>" name=txtSumPurchase 
                  ><%}%>
													</td>
													<td></td>
												</tr>
												<tr>
													<td colSpan="5">&nbsp;</td>
												</tr>
												<tr>
													<td></td>
													<td colSpan="2">Less: Vat Payable on Sales</td>
													<td align="right"><input id=tempTotalSale 
                  type=hidden value="<%=Total_SalVat.ToString()%>" 
                  name=tempTotalSale><%=Total_SalVat.ToString()%>&nbsp;&nbsp;&nbsp;&nbsp;</td>
													<td></td>
												</tr>
												<tr>
													<td colSpan="3">&nbsp;</td>
													<td width="100">
														<hr>
													</td>
													<td></td>
												</tr>
												<tr>
													<td></td>
													<td colSpan="2">Net Vat Payable</td>
													<td width="100">&nbsp;
														<%if(txtValue.Text!=""){%>
														<input 
                  class=fontstyle
                  style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none" 
                  readOnly type=text size=12 
                  value="<%=double.Parse(Total_PurVar.ToString())+double.Parse(txtValue.Text)-double.Parse(Total_SalVat.ToString())%>" 
                  name=txtNetAmount>
														<%}else{%>
														<input 
                  class=fontstyle
                  style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none" 
                  readOnly type=text size=12 
                  value="<%=double.Parse(Total_PurVar.ToString())-double.Parse(Total_SalVat.ToString())%>" 
                  name=txtNetAmount><%}%>
													</td>
													<td></td>
												</tr>
												<tr>
													<td colSpan="5">&nbsp;</td>
												</tr>
											</table>
										</td>
									</tr>
								</table>
								<%}}%>
							</P>
						</TD>
					</TR>
				</TBODY>
			</table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
