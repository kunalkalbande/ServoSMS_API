<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SaleBook" CodeFile="SaleBook.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Sales Book Report</title> <!--
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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
	function CheckSearchOption(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			/*    Coment By vikas 16.11.2012
			if(index == 1)
				f.texthiddenprod.value=f.tempCustType.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempInvoiceNo.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempPackType.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempProductGroup.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempProdWithPack.value;
			else if(index == 7)
				f.texthiddenprod.value=f.tempSSR.value;*/
				
			if(index == 1)
				f.texthiddenprod.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempInvoiceNo.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempPackType.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempProductGroup.value;
			else if(index == 7)
				f.texthiddenprod.value=f.tempProdWithPack.value;
			else if(index == 8)
				f.texthiddenprod.value=f.tempSSR.value;
				
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
		
		if(index == 5)
		{
			document.Form1.DropValue.value="";
		}
		else if(index == 6)
		{
			document.Form1.DropValue.value="";
		}
		else if(index == 7)
		{
			document.Form1.DropValue.value="";
		}
		else if(index == 8)
		{
			document.Form1.DropValue.value="";
		}
	}
	
	function getChild(t)
		{
			if(t.selectedIndex==5)
			{
				childWin=window.open("SpecificPacksSales.aspx?chk="+t.name+":5", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=205,height=626");
			}
			else if(t.selectedIndex==8)
			{
				childWin=window.open("SpecificPacksSales.aspx?chk="+t.name+":8", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=225,height=326");
			}
			else if(t.selectedIndex==7)
			{
				childWin=window.open("SpecificPacksSales.aspx?chk="+t.name+":7", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=320,height=700");
			}
			else if(t.selectedIndex==6)
			{
				childWin=window.open("SpecificPacksSales.aspx?chk="+t.name+":6", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=225,height=700");
			}
		}
		</script>
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
				Visible="False" Width="8px"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempProductGroup" style="WIDTH: 1px" type="hidden" name="tempProdGroup" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPackType" style="WIDTH: 1px" type="hidden" name="tempPackType" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<input id="tempInvoiceNo" style="WIDTH: 1px" type="hidden" name="tempInvoiceNo" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempProdWithPack" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center">
				<TR height="10">
					<TH align="center">
						<font color="#ce4848">Sale Book Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">
						<TABLE width="100%" border="0">
							<TR>
								<TD align="center" width="10%">Date From</TD>
								<TD width="20%"><asp:textbox id="txtDateFrom" runat="server" Width="80px" CssClass="fontstyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD align="center" width="10%">Date To</TD>
								<TD colSpan="3"><asp:textbox id="Textbox1" runat="server" Width="80px" CssClass="fontstyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;
								</TD>
								<td></td>
							</TR>
							<tr>
								<TD align="center">Search By</TD>
								<TD align="left"><asp:dropdownlist id="DropSearchBy" runat="server" Width="140px" CssClass="dropdownlist" onchange="CheckSearchOption(this),getChild(this)">
										<asp:ListItem Value="All">All</asp:ListItem>
										<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
										<asp:ListItem Value="Customer SubGroup">Customer SubGroup</asp:ListItem>
										<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
										<asp:ListItem Value="Invoice No">Invoice No</asp:ListItem>
										<asp:ListItem Value="Pack Type">Pack Type</asp:ListItem>
										<asp:ListItem Value="Product Group">Product Group</asp:ListItem>
										<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">Value&nbsp;&nbsp;&nbsp;</TD>
								<TD align="left" colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnShow)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnShow)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnShow,document.Form1.DropValue)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
											type="select-one"></select></div>
								</TD>
								<td colSpan="2"><asp:button id="btnShow" runat="server" Width="60px" Text="View" 
										 onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="BtnPrint" Width="60px" Text="Print " 
										Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnExcel" Width="60px" Text="Excel" 
										Runat="server" onclick="btnExcel_Click"></asp:button></td>
							</tr>
							<tr>
								<td align="right" colSpan="7"><asp:radiobutton id="RadioDetails" Text="Details" Runat="server" GroupName="Radio" Checked="True"></asp:radiobutton><asp:radiobutton id="RadioSumrized" Text="Summerized" Runat="server" GroupName="Radio"></asp:radiobutton><asp:checkbox id="chkDiscount" Text="  Discount (%)" Runat="server"></asp:checkbox><asp:checkbox id="chkDailySales" Text="Daily Sales" Runat="server"></asp:checkbox><asp:checkbox id="ChkDaily" Text="Godown Report" Runat="server"></asp:checkbox><asp:checkbox id="ChkMonthWise" Text="Monthly Sales" Runat="server"></asp:checkbox></td>
							</tr>
							<TR>
								<TD align="center" colSpan="7"><asp:datagrid id="GridSalesReport" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
										BorderColor="#DEBA84" OnItemDataBound="ItemTotal1" ShowFooter="True" OnSortCommand="sortcommand_click" AllowSorting="True"
										CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
										<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
												<HeaderStyle Width="150px"></HeaderStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
												DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
												<HeaderStyle Width="200px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Package Type"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Quantity&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Ltr">
												<HeaderStyle Width="100px"></HeaderStyle>
												<ItemTemplate>
													<table width="100%">
														<tr>
															<td align="left"><font color="#8C4510"><%#TotalSumQty(DataBinder.Eval(Container.DataItem,"quant").ToString())%></font></td>
															<td align="right"><font color="#8C4510"><%#TotalSumQtyinLtr(DataBinder.Eval(Container.DataItem,"Total_Qty").ToString())%></font></td>
														</tr>
													</table>
												</ItemTemplate>
												<FooterTemplate>
													<table width="100%">
														<tr>
															<td align="left"><font color="#8C4510"><b><%=TotalQty.ToString()%></b></font></td>
															<td align="right"><font color="#8C4510"><b><%=TotalQtyLtr.ToString()%></b></font></td>
														</tr>
													</table>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Rate" SortExpression="Rate" HeaderText="RSP" DataFormatString="{0:N2}">
												<HeaderStyle Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="TotalRate" SortExpression="TotalRate" HeaderText="Total RSP" DataFormatString="{0:N2}">
												<HeaderStyle Width="70px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Sec. Discount">
												<HeaderStyle Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetSchDiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Total_Qty").ToString(),DataBinder.Eval(Container.DataItem,"TotalRate").ToString(),DataBinder.Eval(Container.DataItem,"Cust_id").ToString(),DataBinder.Eval(Container.DataItem,"Sch_Type").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=total_SchDisc.ToString()%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Discount(%)">
												<HeaderStyle Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<!--%#GetPerDiscount(DataBinder.Eval(Container.DataItem,"Discount").ToString(),DataBinder.Eval(Container.DataItem,"Discount_Type").ToString(),DataBinder.Eval(Container.DataItem,"TotalRate").ToString(),DataBinder.Eval(Container.DataItem,"foe").ToString(),DataBinder.Eval(Container.DataItem,"Total_Qty").ToString(),DataBinder.Eval(Container.DataItem,"InvoiceNo").ToString())%-->
													<%#GetPerDiscount(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Total_Qty").ToString(),DataBinder.Eval(Container.DataItem,"TotalRate").ToString(),DataBinder.Eval(Container.DataItem,"Cust_id").ToString(),DataBinder.Eval(Container.DataItem,"Sch_Type").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=System.Convert.ToString(Math.Round(total_PerDisc,1))%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Fleet/Oe">
												<HeaderStyle Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetFleetOeDiscount(DataBinder.Eval(Container.DataItem,"foe").ToString(),DataBinder.Eval(Container.DataItem,"Total_Qty").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=total_FleetOe.ToString()%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Cash Disc.">
												<HeaderStyle Width="70px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetCashDiscount(DataBinder.Eval(Container.DataItem,"cash_discount").ToString(),DataBinder.Eval(Container.DataItem,"cash_disc_type").ToString(),DataBinder.Eval(Container.DataItem,"TotalRate").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=GenUtil.strNumericFormat(total_CashDisc.ToString())%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Vat">
												<HeaderStyle Width="70px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetVat(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=System.Convert.ToString(Math.Round(total_Vat,1))%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Total">
												<HeaderStyle Width="70px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetTotal(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=total_Total.ToString()%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Total Amount">
												<HeaderStyle Width="100px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetTotalAmount(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Net_Amount").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=total_TotalAmount.ToString()%>
												</FooterTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><asp:datagrid id="GridSalesSummerized" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
										BorderColor="#DEBA84" OnItemDataBound="ItemTotal" ShowFooter="True" OnSortCommand="sortcommand_click" AllowSorting="True"
										CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
										<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
												<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place"></asp:BoundColumn>
											<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
												DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty in No.">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="totqty" SortExpression="totqty" HeaderText="Qty in Ltr">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Discount" SortExpression="Discount" HeaderText="Discount&lt;br&gt;(if any)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Net_Amount" SortExpression="net_amount" HeaderText="Net Amount">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<tr>
								<td align="center" colSpan="7"><asp:datagrid id="GridSaleMonthly" runat="server" Width="60%" BorderStyle="None" BackColor="#DEBA84"
										BorderColor="#DEBA84" OnItemDataBound="ItemTotal_Monthly" ShowFooter="True" OnSortCommand="sortcommand_click_Monthly"
										AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
										<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
												<HeaderStyle Width="300px"></HeaderStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
												<HeaderStyle Width="200px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Package Type"></asp:BoundColumn>
											<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty In Nos">
												<HeaderStyle Width="50px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="total_qty" SortExpression="total_qty" HeaderText="Qty In Ltr">
												<HeaderStyle Width="50px"></HeaderStyle>
											</asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></td>
							</tr>
							<asp:panel id="panAnil" Visible="False" Runat="server">
								<TR>
									<TD align="left" colSpan="7"><B>SSR :- Anil Agarwal</B></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="7"><!--Add by vikas 31.08.09 -->
										<asp:datagrid id="GridAnil" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
											BorderColor="#DEBA84" OnItemDataBound="ItemTotal_anil" ShowFooter="True" OnSortCommand="sortcommand_click"
											AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total Sales SSR Anil Agarwal">
													<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Cotegory"></asp:BoundColumn>
												<asp:BoundColumn DataField="prod_name" SortExpression="prod_name" HeaderText="Product Name With Pack">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty in No.">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="total_qty" SortExpression="total_qty" HeaderText="Qty in Ltr">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalRate" SortExpression="TotalRate" HeaderText="Total Amount">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</asp:panel><asp:panel id="PanGopal" Visible="False" Runat="server">
								<TR>
									<TD align="left" colSpan="7"><B>SSR :- Gopal Prajapati</B></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="7"><!--Add by vikas 31.08.09 -->
										<asp:datagrid id="GridGopal" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
											BorderColor="#DEBA84" OnItemDataBound="ItemTotal_Gopal" ShowFooter="True" OnSortCommand="sortcommand_click"
											AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total Sales SSR Gopal Prajapati">
													<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Cotegory"></asp:BoundColumn>
												<asp:BoundColumn DataField="prod_name" SortExpression="prod_name" HeaderText="Product Name With Pack">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty in No.">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="total_qty" SortExpression="total_qty" HeaderText="Qty in Ltr">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalRate" SortExpression="TotalRate" HeaderText="Total Amount">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</asp:panel><asp:panel id="PanChauhan" Visible="False" Runat="server">
								<TR>
									<TD align="left" colSpan="7"><B>SSR :- Surendra Chauhan</B></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="7"><!--Add by vikas 31.08.09 -->
										<asp:datagrid id="GridChauhan" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
											BorderColor="#DEBA84" OnItemDataBound="ItemTotal_Chauhan" ShowFooter="True" OnSortCommand="sortcommand_click"
											AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total Sales SSR Surendra Chauhan">
													<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Cotegory"></asp:BoundColumn>
												<asp:BoundColumn DataField="prod_name" SortExpression="prod_name" HeaderText="Product Name With Pack">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty in No.">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="total_qty" SortExpression="total_qty" HeaderText="Qty in Ltr">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalRate" SortExpression="TotalRate" HeaderText="Total Amount">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</asp:panel><asp:panel id="PanRajKumar" Visible="False" Runat="server">
								<TR>
									<TD align="left" colSpan="7"><B>SSR :- Rajkumar Garg</B></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="7"><!--Add by vikas 31.08.09 -->
										<asp:datagrid id="GridRajKumar" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
											BorderColor="#DEBA84" OnItemDataBound="ItemTotal_Rajkumar" ShowFooter="True" OnSortCommand="sortcommand_click"
											AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total Sales SSR Rajkumar Garg">
													<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Cotegory"></asp:BoundColumn>
												<asp:BoundColumn DataField="prod_name" SortExpression="prod_name" HeaderText="Product Name With Pack">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty in No.">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="total_qty" SortExpression="total_qty" HeaderText="Qty in Ltr">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalRate" SortExpression="TotalRate" HeaderText="Total Amount">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</asp:panel><asp:panel id="PanKeyCustomer" Visible="False" Runat="server">
								<TR>
									<TD align="left" colSpan="7"><B>SSR :- Key Customer</B></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="7"><!--Add by vikas 31.08.09 -->
										<asp:datagrid id="GridKeyCustomer" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
											BorderColor="#DEBA84" OnItemDataBound="ItemTotal_KeyCustmer" ShowFooter="True" OnSortCommand="sortcommand_click"
											AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total Sales SSR Key Customer">
													<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Cotegory"></asp:BoundColumn>
												<asp:BoundColumn DataField="prod_name" SortExpression="prod_name" HeaderText="Product Name With Pack">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="quant" SortExpression="quant" HeaderText="Qty in No.">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="total_qty" SortExpression="total_qty" HeaderText="Qty in Ltr">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TotalRate" SortExpression="TotalRate" HeaderText="Total Amount">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</asp:panel>
							<%
							if(flage==1)
							{
								%>
							<tr bgColor="#ce4848">
								<TD align="right" colSpan="7"><font color="#ffffff"><b>Grand Total for the Period of date 
											from
											<%=txtDateFrom.Text.ToString()%>
											date to
											<%=Textbox1.Text.ToString()%>
											Of All SSR </b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<font color="#ffffff"><b>
											<%=Math.Round(Grand_TotalQty_No)%>
										</b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<font color="#ffffff"><b>
											<%=Math.Round(Grand_TotalQty_Ltr)%>
										</b></font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<font color="#ffffff"><b>
											<%=Math.Round(Grand_TotalNet_Amount)%>
										</b></font>
								</TD>
							</tr>
							<%
							}
							%>
							<asp:panel id="panDaily" Visible="False" Runat="server">
								<TR>
									<TD align="center" colSpan="7">
										<asp:datagrid id="GridDaily" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
											BorderColor="#DEBA84" OnItemDataBound="ItemTotal1" ShowFooter="True" OnSortCommand="sortcommand_click"
											AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
											<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
													<HeaderStyle Width="400px"></HeaderStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
													DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
												<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
													<HeaderStyle Width="200px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Package Type"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Quantity&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Ltr">
													<HeaderStyle Width="100px"></HeaderStyle>
													<ItemTemplate>
														<table width="100%">
															<tr>
																<td align="left"><font color="#8C4510"><%#TotalSumQty(DataBinder.Eval(Container.DataItem,"quant").ToString())%></font></td>
																<td align="right"><font color="#8C4510"><%#TotalSumQtyinLtr(DataBinder.Eval(Container.DataItem,"Total_Qty").ToString())%></font></td>
															</tr>
														</table>
													</ItemTemplate>
													<FooterTemplate>
														<table width="100%">
															<tr>
																<td align="left"><font color="#8C4510"><b><%=TotalQty.ToString()%></b></font></td>
																<td align="right"><font color="#8C4510"><b><%=TotalQtyLtr.ToString()%></b></font></td>
															</tr>
														</table>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Rate" SortExpression="Rate" HeaderText="RSP" DataFormatString="{0:N2}">
													<HeaderStyle Width="50px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Total">
													<HeaderStyle Width="70px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#GetTotal_Daily(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Prod_Name").ToString(),DataBinder.Eval(Container.DataItem,"Pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Total_Qty").ToString(),DataBinder.Eval(Container.DataItem,"TotalRate").ToString(),DataBinder.Eval(Container.DataItem,"Cust_id").ToString(),DataBinder.Eval(Container.DataItem,"Sch_Type").ToString(),DataBinder.Eval(Container.DataItem,"foe").ToString(),DataBinder.Eval(Container.DataItem,"cash_discount").ToString(),DataBinder.Eval(Container.DataItem,"cash_disc_type").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=total_Total.ToString()%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Total Amount">
													<HeaderStyle Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#GetTotalAmount(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Net_Amount").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=total_TotalAmount.ToString()%>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</asp:panel></TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				<!--tr>
					<td align="left"><FONT color="#ff0033"><STRONG><U>Note</U>:</STRONG>&nbsp;</FONT><FONT color="black">
							To take a printout press the above Print button, to redirect the output to a 
							new page. Use the Page Setup option in the File menu to set the appropriate
							<br>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; margins, 
							then use the Print option in the file menu to send the output to the printer. </FONT>
					</td>
				</tr--></table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
