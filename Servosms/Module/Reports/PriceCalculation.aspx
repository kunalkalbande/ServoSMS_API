<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.PriceCalculation" CodeFile="PriceCalculation.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Price Calculation</title><!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express	 written  permission of
   bbnisys Technologies.
-->
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
				f.texthiddenprod.value=f.tempProdWithPack.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempProdName.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempProdType.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempProdCode.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempPackType.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempPackUnit.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
	}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<input id="tempET" style="Z-INDEX: 102; LEFT: 144px; WIDTH: 2px; POSITION: absolute; TOP: 16px"
				type="hidden" name="tempET" runat="server"> <input id="tempVat" style="Z-INDEX: 102; LEFT: 150px; WIDTH: 2px; POSITION: absolute; TOP: 16px"
				type="hidden" name="tempVat" runat="server"> <input id="tempProdCode" style="WIDTH: 1px" type="hidden" name="tempProdCode" runat="server">
			<input id="tempProdType" style="WIDTH: 1px" type="hidden" name="tempProdType" runat="server">
			<input id="tempProdName" style="WIDTH: 1px" type="hidden" name="tempProdName" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempProdWithPack" runat="server">
			<input id="tempPackType" style="WIDTH: 1px" type="hidden" name="tempPackType" runat="server">
			<input id="tempPackUnit" style="WIDTH: 1px" type="hidden" name="tempPackUnit" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center" border="0">
				<TR>
					<TH align="center" colSpan="5" height="20">
						<font color="#ce4848">Price Calculation Report</font>
						<hr>
					</TH>
				</TR>
				<tr vAlign="top" height="20">
					<td vAlign="top" colSpan="5">&nbsp;&nbsp;Date From&nbsp;&nbsp;<asp:textbox id="txtDateFrom" runat="server" CssClass="dropdownlist" BorderStyle="Groove" ReadOnly="True"
							Width="70px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A> &nbsp;&nbsp;Date To&nbsp;&nbsp;<asp:textbox id="txtDateTo" runat="server" CssClass="dropdownlist" BorderStyle="Groove" ReadOnly="True"
							Width="70px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A> &nbsp;&nbsp;<asp:radiobutton id="RadSale" Text="Sales" Runat="server" Checked="True" GroupName="Price"></asp:radiobutton>&nbsp;
						<asp:radiobutton id="RadPurchase" Text="Purchase" Runat="server" GroupName="Price"></asp:radiobutton>&nbsp;
						<asp:radiobutton id="RadSummarized" Text="Summarized" Runat="server" GroupName="Price"></asp:radiobutton>&nbsp;&nbsp;&nbsp;
						<b>Report Type</b>&nbsp;<asp:radiobutton id="RadSummarize" Text="Summarized" Runat="server" Checked="True" GroupName="type"></asp:radiobutton>&nbsp;
						<asp:radiobutton id="RadComplete" Text="Complete" Runat="server" GroupName="type"></asp:radiobutton></td>
				<tr height="20">
					<td>&nbsp;&nbsp;Search by</td>
					<td><asp:dropdownlist id="DropSearch" CssClass="dropdownlist" Width="135" Runat="server" onchange="CheckSearchOption(this)">
							<asp:ListItem Value="All">All</asp:ListItem>
							<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
							<asp:ListItem Value="Product Name">Product Name</asp:ListItem>
							<asp:ListItem Value="Product Type">Product Type</asp:ListItem>
							<asp:ListItem Value="Product Code">Product Code</asp:ListItem>
							<asp:ListItem Value="Pack Type">Pack Type</asp:ListItem>
							<asp:ListItem Value="Pack Unit">Pack Unit</asp:ListItem>
						</asp:dropdownlist></td>
					<td>Value</td>
					<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
							onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.Button1)"
							style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							readOnly type="text" name="temp">&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkCurrPrice" Runat="server" Text="Curr Price"></asp:CheckBox><br>
						<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.Button1)"
								id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.Button1,document.Form1.DropValue)"
								style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
								type="select-one"></select></div>
					</td>
					<td><asp:button id="btnShow" runat="server" Width="60px" Text="View" 
							 onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;<asp:button id="BtnPrint" Width="60px" Text="Print" Runat="server" 
							 onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="60px" Text="Excel" Runat="server" 
							 onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr vAlign="top">
					<td align="center" colSpan="5">
						<TABLE width="80%">
							<TR>
								<TD align="center" colSpan="5"><asp:datagrid id="GridSalesSummarized" runat="server" BorderStyle="None" Width="100%" BorderColor="#DEBA84"
										BackColor="#DEBA84" OnSortCommand="SortCommand_Click" AllowSorting="True" CellSpacing="1" CellPadding="1" BorderWidth="0px"
										AutoGenerateColumns="False">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="mrp" SortExpression="mrp" HeaderText="MRP">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Purchase Rate">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetBasicRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"sal_rate").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Entry Tax">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetETRate(GetBasicRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"sal_rate").ToString()))%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Sales Rate">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetSaleRateNew()%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Vat">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetVatRateNew()%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="DLP">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetDLPNew()%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Eff. Date">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%#GetEffDate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"eff_date").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center"
											ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><asp:datagrid id="GridPurchaseSummarized" runat="server" BorderStyle="None" Width="100%" BorderColor="#DEBA84"
										BackColor="#DEBA84" OnSortCommand="SortCommand_Click" AllowSorting="True" CellSpacing="1" CellPadding="1"
										BorderWidth="0px" AutoGenerateColumns="False">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
												<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name"></asp:BoundColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type"></asp:BoundColumn>
											<asp:BoundColumn DataField="mrp" SortExpression="mrp" HeaderText="MRP">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Basic Rate">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="ET">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetETRate(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString()))%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn SortExpression="isSanction" HeaderText="Vat">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetVatPerRate(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString()),GetETRate(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString())))%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Purchase Price">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetNetAmount(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString()),GetVatPerRate(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString()),GetETRate(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString()))),GetETRate(GetBasicPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString())))%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Eff. Date">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%#GetEffDate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"eff_date").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center"
											ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><asp:datagrid id="GridSummarizedSummarized" runat="server" BorderStyle="None" Width="100%" BorderColor="#DEBA84"
										BackColor="#DEBA84" OnSortCommand="SortCommand_Click" AllowSorting="True" CellSpacing="1" CellPadding="1"
										BorderWidth="0px" AutoGenerateColumns="False">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="mrp" SortExpression="mrp" HeaderText="MRP">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Sales Price">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetSalesRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"sal_rate").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Purchase Price">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Differance">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#GetDiff(GetPurRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"pur_rate").ToString()),GetSalesRate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"sal_rate").ToString()))%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Eff. Date">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%#GetEffDate(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"eff_date").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center"
											ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid><asp:datagrid id="gridCurrPrice" runat="server" BorderStyle="None" Width="100%" BorderColor="#DEBA84"
										BackColor="#DEBA84" OnSortCommand="SortCommand_Click" AllowSorting="True" CellSpacing="1" CellPadding="1"
										BorderWidth="0px" AutoGenerateColumns="False" OnItemDataBound="ItemDataBound">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product Name">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="mrp" SortExpression="mrp" HeaderText="MRP">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderText="Sales Price">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderText="Purchase Price">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderText="Differance">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn HeaderText="Eff. Date">
												<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
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
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
