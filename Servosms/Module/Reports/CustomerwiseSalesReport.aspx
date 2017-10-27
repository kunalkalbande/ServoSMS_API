<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.CustomerwiseSalesReport" CodeFile="CustomerwiseSalesReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Mont. Cust. Secon. Sales Report</title> <!--
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
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
	function CheckSearchOption(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			if(index == 1)
				f.texthiddenprod.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempInvoiceNo.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempProductGroup.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempProdWithPack.value;
			else if(index == 7)
				f.texthiddenprod.value=f.tempSSR.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
	}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 136px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempProductGroup" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<input id="tempInvoiceNo" style="WIDTH: 1px" type="hidden" name="tempInvoiceNo" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center">
				<TR>
					<TH align="center" height="20">
						<font color="#ce4848">Mont. Cust. Secon. Sales Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">
						<TABLE id="Table1" width="100%">
							<TR>
								<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;Date From</TD>
								<TD align="left"><asp:textbox id="txtDateFrom" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"
										href="javascript:void(0)"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD align="left">Date To</TD>
								<TD align="left"><asp:textbox id="Textbox1" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"
										href="javascript:void(0)"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<td colSpan="2"></td>
							</TR>
							<TR>
								<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;Search By</TD>
								<TD align="left"><asp:dropdownlist id="DropSearchBy" runat="server" Width="110px" CssClass="dropdownlist" onchange="CheckSearchOption(this)" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
										<asp:ListItem Value="All">All</asp:ListItem>
										<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
										<asp:ListItem Value="Customer Sub Group">Customer Sub Group</asp:ListItem>
										<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
										<asp:ListItem Value="Invoice No">Invoice No</asp:ListItem>
										<asp:ListItem Value="Product Group">Product Group</asp:ListItem>
										<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="left">Value&nbsp;&nbsp;&nbsp;</TD>
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
								<td><asp:button id="btnShow" runat="server" Width="60px" Text="View" 
										 onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;<asp:button id="BtnPrint" runat="server" Width="60px" Text="Print " 
										 onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel" 
										 onclick="btnExcel_Click"></asp:button></td>
							</TR>
							<TR>
								<TD align="center" colSpan="6"><asp:datagrid id="GridReport" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
										BackColor="#DEBA84" OnItemDataBound="ItemTotal" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" Height="100%"
										CellSpacing="1" AllowSorting="True" OnSortCommand="sortcommand_click" ShowFooter="True" onselectedindexchanged="GridReport_SelectedIndexChanged">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
												<FooterStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="Place" SortExpression="Place" HeaderText="Place"></asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
												DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="Prod_Type" SortExpression="Prod_Type" HeaderText="Product Type"></asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name"></asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="qty" SortExpression="qty" HeaderText="Qty&lt;BR&gt;(No's)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="qtyltr" SortExpression="qtyltr" HeaderText="Qty&lt;BR&gt;(Ltr)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderStyle-Font-Bold="true" HeaderText="Price">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#getPrice(DataBinder.Eval(Container.DataItem,"rate").ToString(),DataBinder.Eval(Container.DataItem,"qty").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_Date").ToString(),DataBinder.Eval(Container.DataItem,"cust_id").ToString(),DataBinder.Eval(Container.DataItem,"qtyltr").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=Cache["TotalPrice"].ToString()%>
												</FooterTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			<asp:validationsummary id="vsCustWiseSales" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
