<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.PurchaseBook" CodeFile="PurchaseBook.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Purchase Book Report</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript">
	function CheckSearchOption(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			if(index == 1)
				f.texthiddenprod.value=f.tempVendorName.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempInvoiceNo.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempPackType.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempProductGroup.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempProdWithPack.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
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
		<script language="javascript">
		function aa()
		{
			alert("hello");
		}
		</script>
	    <style type="text/css">
            .auto-style1 {
                width: 20%;
            }
        </style>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox><input id="tempProductGroup" style="WIDTH: 1px" type="hidden" name="tempProductGroup" runat="server">
			<input id="tempVendorName" style="WIDTH: 1px" type="hidden" name="tempVendorName" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPackType" style="WIDTH: 1px" type="hidden" name="tempPackType" runat="server">
			<input id="tempInvoiceNo" style="WIDTH: 1px" type="hidden" name="tempInvoiceNo" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempProdWithPack" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<TR>
					<TH vAlign="top" align="center" colSpan="6" height="20">
						<font color="#ce4848">Purchase Book Report</font>
						<hr>
					</TH>
				</TR>
				<tr height="20">
					<td align="left" class="auto-style1">From&nbsp;<asp:textbox id="txtDateFrom" runat="server" Width="70px" CssClass="fontstyle" BorderStyle="Groove"
							ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A></td>
					<td width="10%" align="left">&nbsp;To&nbsp;<asp:textbox id="Textbox1" runat="server" Width="70px" CssClass="fontstyle" BorderStyle="Groove"
							ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A></td>
					<td align="center" width="15%"></td>
					<td width="30%" colspan=2><asp:radiobutton id="RadioDetails" Text="Details" Runat="server" GroupName="Radio" Checked="True"></asp:radiobutton>&nbsp;<asp:radiobutton id="RadPerclaim" Text="Difference % Claim" Runat="server" GroupName="Radio"></asp:radiobutton></td>
					<td width="30%"><asp:radiobutton id="RadioSumrized" Text="Summerized" Runat="server" GroupName="Radio"></asp:radiobutton>&nbsp;<asp:checkbox id="chkDiscount" Text="  (%) Discount" Runat="server"></asp:checkbox></td>
				</tr>
				<tr vAlign="top">
					<TD align="center" class="auto-style1">Search By</TD>
					<TD align="left"><asp:dropdownlist id="DropSearchBy" runat="server" Width="140px" CssClass="dropdownlist" onchange="CheckSearchOption(this)">
							<asp:ListItem Value="All">All</asp:ListItem>
							<asp:ListItem Value="Customer Name">Vendor Name</asp:ListItem>
							<asp:ListItem Value="Invoice No">Invoice No</asp:ListItem>
							<asp:ListItem Value="Pack Type">Pack Type</asp:ListItem>
							<asp:ListItem Value="Product Group">Product Group</asp:ListItem>
							<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD align="center">Value</TD>
					<TD align="left" colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
							onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnShow)"
							style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 150px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							readOnly type="text" name="temp"><br>
						<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnShow)"
								id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnShow,document.Form1.DropValue)"
								style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
								type="select-one"></select></div>
					</TD>
					<td align="center" width="40%" colSpan="1"><asp:button id="btnShow" runat="server" Width="50px" Text="View" 
							onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;<asp:button id="BtnPrint" Width="50px" Text="Print  " Runat="server"
							onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="50px" Text="Excel" Runat="server"
							onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" colSpan="6">
						<!--Grid 1 For Purchase Book in Detail--><asp:datagrid id="GridReport" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
							BackColor="#DEBA84" ShowFooter="True" AllowSorting="True" OnSortCommand="sortcommand_click" CellSpacing="1" CellPadding="1" BorderWidth="0px"
							AutoGenerateColumns="False" onselectedindexchanged="GridReport_SelectedIndexChanged">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Vendor_ID" SortExpression="Vendor_ID" HeaderText="Vendor ID" FooterText="Total:"></asp:BoundColumn>
								<asp:BoundColumn DataField="Vendor_Name" SortExpression="Vendor_Name" HeaderText="Vendor Name">
									<HeaderStyle Width="150px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Place" SortExpression="Place" HeaderText="Place"></asp:BoundColumn>
								<asp:BoundColumn DataField="Vendor_Type" SortExpression="Vendor_Type" HeaderText="Vendor Type"></asp:BoundColumn>
								<asp:BoundColumn DataField="Vndr_Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No."></asp:BoundColumn>
								<asp:BoundColumn DataField="vndr_Invoice_Date" SortExpression="vndr_Invoice_Date" HeaderText="Invoice Date"
									DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
								<asp:BoundColumn DataField="Prod_Type" SortExpression="Prod_Type" HeaderText="Product Type"></asp:BoundColumn>
								<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Quantity In Lit's">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#GenUtil.strNumericFormat((Multiply(DataBinder.Eval(Container.DataItem,"Prod_Type").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Prod_Name").ToString()+"X"+DataBinder.Eval(Container.DataItem,"Qty").ToString())).ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=GenUtil.strNumericFormat(Cache["os"].ToString())%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Price" SortExpression="Price" HeaderText="Price" DataFormatString="{0:N2}">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Discount" SortExpression="Discount" HeaderText="Discount(if any)"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="InvoiceAmount">
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
									<ItemTemplate>
										<%#Multiply1(DataBinder.Eval(Container.DataItem,"Vndr_Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Net_Amount").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%#GenUtil.strNumericFormat(Cache["am"].ToString())%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Cr_Days" SortExpression="Cr_Days" HeaderText="Credit Days"></asp:BoundColumn>
								<asp:BoundColumn DataField="duedate" SortExpression="duedate" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}">   
                                    <HeaderStyle Width="150px"></HeaderStyle>                                 
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<!--Grid 2 For Purchase Book in Summerized--><asp:datagrid id="grdDetails" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
							BackColor="#DEBA84" ShowFooter="True" AllowSorting="True" OnSortCommand="sortcommand_click" CellSpacing="1" CellPadding="1" BorderWidth="0px"
							AutoGenerateColumns="False" OnItemDataBound="ItemDataBound">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Vendor_Name" SortExpression="Vendor_Name" HeaderText="Vendor Name" FooterText="Total">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Vndr_Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="vndr_Invoice_Date" SortExpression="vndr_Invoice_Date" HeaderText="Invoice Date"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Prod_Type" SortExpression="Prod_Type" HeaderText="Product Type">
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="qty" HeaderText="Qty In No's">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="qtyltr" HeaderText="Qty In Lit's">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RSP" SortExpression="Price" HeaderText="Price">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Trade Disc.">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#GetTradeDisc(DataBinder.Eval(Container.DataItem,"RSP").ToString(),DataBinder.Eval(Container.DataItem,"Trade").ToString(),DataBinder.Eval(Container.DataItem,"Trade_Discount").ToString(),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"qtyltr").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_Date").ToString(),DataBinder.Eval(Container.DataItem,"FOC").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="EarlyBird" SortExpression="EarlyBird" HeaderText="Early Bird">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FOC" SortExpression="FOC" HeaderText="FOC Disc.">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Cash Disc.">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#GetCashDisc(DataBinder.Eval(Container.DataItem,"RSP").ToString(),DataBinder.Eval(Container.DataItem,"Trade").ToString(),DataBinder.Eval(Container.DataItem,"EarlyBird").ToString(),DataBinder.Eval(Container.DataItem,"ETFOC").ToString(),DataBinder.Eval(Container.DataItem,"FOC").ToString(),DataBinder.Eval(Container.DataItem,"Discount").ToString(),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_Date").ToString(),DataBinder.Eval(Container.DataItem,"cash_discount").ToString(),DataBinder.Eval(Container.DataItem,"cash_disc_type").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<!--%=GenUtil.strNumericFormat(Cache["Cash"].ToString())%-->
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Discount(%)">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#GetDiscountInPer(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"RSP").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_Date").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Discount" SortExpression="Discount" HeaderText="Discount">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Total Disc.">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#GetTotalDiscount(DataBinder.Eval(Container.DataItem,"Trade").ToString(),DataBinder.Eval(Container.DataItem,"EarlyBird").ToString(),DataBinder.Eval(Container.DataItem,"FOC").ToString(),DataBinder.Eval(Container.DataItem,"Discount").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString(),DataBinder.Eval(Container.DataItem,"QtyLtr").ToString(),DataBinder.Eval(Container.DataItem,"RSP").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
						<!--Grid 3 For Purchase Book in % Claim--><asp:datagrid id="GridClaim" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
							BackColor="#DEBA84" ShowFooter="True" AllowSorting="True" OnSortCommand="sortcommand_click" CellSpacing="1" CellPadding="1" BorderWidth="0px"
							AutoGenerateColumns="False" OnItemDataBound="ItemDataBound1">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Vendor_Name" SortExpression="Vendor_Name" HeaderText="Vendor Name" FooterText="Total">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Vndr_Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="vndr_Invoice_Date" SortExpression="vndr_Invoice_Date" HeaderText="Invoice Date"
									DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="5%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
									<HeaderStyle Width="15%"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="qty" HeaderText="Qty In No's">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="qtyltr" HeaderText="Qty In Lit's">
									<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataFormatString="{0:N2}" DataField="RSP" SortExpression="Price" HeaderText="Price">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DiscType" SortExpression="DiscType" HeaderText="Eligible Trade-Disc.">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataFormatString="{0:N2}" DataField="Disc_Amount" SortExpression="Disc_Amount" HeaderText="Eligible Trade-Disc. Amount">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataFormatString="{0:N2}" DataField="Disc_Amount" SortExpression="Disc_Amount" HeaderText="Dedu. in Purchase Inv.">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Eligible Prom Disc%">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#Elgible_Discount(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())+Elgible_Discount_Type(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Eligible Prom Disc Amount">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#Dis_in_Amount(DataBinder.Eval(Container.DataItem,"RSP").ToString() ,Elgible_Discount(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString()).ToString())%>
									</ItemTemplate>
									<FooterTemplate>
										<%=GenUtil.strNumericFormat(Cache["Disoe"].ToString())%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Deduction in Purchase Invoice">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%=0%>
									</ItemTemplate>
									<FooterTemplate>
										<%=0%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=" Total Disc Claim ">
									<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#Claim_Amount(Dis_in_Amount1(DataBinder.Eval(Container.DataItem,"RSP").ToString() ,Elgible_Discount(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString()).ToString()).ToString(),GetDiscountInPer1(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"RSP").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_Date").ToString()).ToString())%>
									</ItemTemplate>
									<FooterTemplate>
										<%=Cache["total13oe"]%>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<asp:validationsummary id="vsPurchaseOrder" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></TD></TR><tr>
				<td align="right"><A href="javascript:window.print()"></A></td>
			</tr>
			</TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
