<%@ Page language="c#" Inherits="Servosms.Module.Reports.PriceList" CodeFile="PriceList.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Price List Report</title> <!--
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
				f.texthiddenprod.value=f.tempProdCode.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempProdID.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempProdName.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempProdWithName.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempPackType.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
		//alert(f.texthiddenprod.value)
	}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><input id="tempProdCode" style="WIDTH: 1px" type="hidden" name="tempProdCode" runat="server">
			<input id="tempProdID" style="WIDTH: 1px" type="hidden" name="tempProdID" runat="server">
			<input id="tempProdName" style="WIDTH: 1px" type="hidden" name="tempProdName" runat="server">
			<input id="tempProdWithName" style="WIDTH: 1px" type="hidden" name="tempProdWithName" runat="server">
			<input id="tempPackType" style="WIDTH: 1px" type="hidden" name="tempPackType" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TH align="center" height="10">
							<font color="#ce4848">Price List Report</font>
							<hr>
						</TH>
					</TR>
					<TR height="20">
						<td align="center">
							<TABLE cellSpacing="2" cellPadding="0" border="0">
								<TBODY>
									<TR>
										<TD align="center" colSpan="6">Type Of Report&nbsp;
											<asp:dropdownlist id="DropSearch1" CssClass="dropdownlist" Runat="server" onselectedindexchanged="DropSearch_SelectedIndexChanged">
												<asp:ListItem Value="All">All</asp:ListItem>
												<asp:ListItem Value="Current Stock Pricing">Current Stock Pricing</asp:ListItem>
												<asp:ListItem Value="Last 3 Month Avarage Pricing">Last 3 Month Avarage Pricing</asp:ListItem>
												<asp:ListItem Value="Last 1 Year Avarage Pricing">Last 1 Year Avarage Pricing</asp:ListItem>
											</asp:dropdownlist>&nbsp;Date From&nbsp;<asp:textbox id="txtDateFrom" runat="server" CssClass="dropdownlist" BorderStyle="Groove" ReadOnly="True"
												Width="60px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absBottom" border="0"></A> &nbsp;&nbsp;To&nbsp;&nbsp;
											<asp:textbox id="txtDateTo" runat="server" CssClass="dropdownlist" BorderStyle="Groove" ReadOnly="True"
												Width="60px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absBottom" border="0"></A> &nbsp;&nbsp;<asp:button id="Button1" runat="server" Width="70px" Text="View " 
												 onclick="Button1_Click"></asp:button>&nbsp;&nbsp;<asp:button id="Btnprint" Runat="server" Width="70px" Text="Print" 
												 onclick="Btnprint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="70px" Text="Excel" 
												 onclick="btnExcel_Click"></asp:button>
										</TD>
									</TR>
									<TR>
										<TD>&nbsp;&nbsp;Search by&nbsp;&nbsp;&nbsp;&nbsp;</TD>
										<TD><asp:dropdownlist id="DropSearch" CssClass="dropdownlist" Runat="server" Width="135" onchange="CheckSearchOption(this)">
												<asp:ListItem Value="All">All</asp:ListItem>
												<asp:ListItem Value="Product Code">Product Code</asp:ListItem>
												<asp:ListItem Value="Product ID">Product ID</asp:ListItem>
												<asp:ListItem Value="Product Name With Pack">Product Name With Pack</asp:ListItem>
												<asp:ListItem Value="Product Name">Product Name</asp:ListItem>
												<asp:ListItem Value="Pack Type">Pack Type</asp:ListItem>
											</asp:dropdownlist></TD>
										<TD>&nbsp;&nbsp;Value&nbsp;&nbsp;</TD>
										<TD colSpan="3"><INPUT class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
												onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.Button1)"
												style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
												value="All" name="DropValue" runat="server"><INPUT class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
												readOnly name="temp">&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:checkbox id="chkCurrPrice" Runat="server" Text="Current Price" Visible="False"></asp:checkbox><BR>
											<DIV id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><SELECT class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.Button1)"
													id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.Button1,document.Form1.DropValue)"
													style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
													type="select-one"></SELECT></DIV>
										</TD>
									</TR>
									<tr>
										<td vAlign="top" align="center" colSpan="6"><asp:datagrid id="GridReport" runat="server" BorderStyle="None" Width="100%" BorderColor="#DEBA84"
												BackColor="#DEBA84" OnSortCommand="sortcommand_click" AllowSorting="True" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False"
												Height="100%" CellSpacing="1">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_ID" SortExpression="Prod_ID" HeaderText="Product ID">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product/Item Name">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Type Of Pack">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Pur_Rate" SortExpression="Pur_Rate" HeaderText="Purchase Rate" DataFormatString="{0:N2}">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Sal_Rate" SortExpression="Sal_Rate" HeaderText="Selling Rate" DataFormatString="{0:N2}">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Eff_Date" SortExpression="Eff_Date" HeaderText="Last Update Date" DataFormatString="{0:dd/MM/yyyy}">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid><asp:datagrid id="GridCurrReport" runat="server" BorderStyle="None" Width="100%" BorderColor="#DEBA84"
												BackColor="#DEBA84" OnSortCommand="sortcommand_click" AllowSorting="True" CellPadding="1" BorderWidth="0px"
												AutoGenerateColumns="False" Height="100%" CellSpacing="1" OnItemDataBound="ItemDataBound">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_ID" SortExpression="Prod_ID" HeaderText="Product ID">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_Name" SortExpression="Prod_Name" HeaderText="Product/Item Name">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Type Of Pack">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Purchase Rate">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Selling Rate">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Last Update Date">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid><asp:datagrid id="GridNew" runat="server" Width="100%" BorderStyle="None" BackColor="#DEBA84"
												BorderColor="#DEBA84" CellSpacing="1" Height="100%" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1"
												AllowSorting="True" OnSortCommand="sortcommand_click" OnItemDataBound="ItemDataBoundNew" ShowFooter="True">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Left" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle Font-Bold="True" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Prod_ID" SortExpression="Prod_ID" HeaderText="Product ID">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="product" SortExpression="product" HeaderText="Product/Item Name" FooterText="Total">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Pur_Rate" SortExpression="Pur_Rate" HeaderText="Purchase Rate">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Sal_Rate" HeaderText="Selling Rate" SortExpression="Sal_Rate">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="eff_date" HeaderText="Last Update Date" DataFormatString="{0:dd/MM/yyyy}"
														SortExpression="eff_date">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Center"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="closing_Stock" SortExpression="closing_Stock" HeaderText="Current Stock">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
													<asp:BoundColumn HeaderText="Avg Sale (Ltr.)">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<FooterStyle Font-Bold="True" Wrap="False" HorizontalAlign="Right"></FooterStyle>
													</asp:BoundColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></td>
									</tr>
								</TBODY>
							</TABLE>
						</td>
					</TR>
					<tr>
						<td align="right"><A href="javascript:window.print()"></A></td>
					</tr>
					<tr>
						<td><asp:validationsummary id="vs1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
					</tr>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe></TR></TBODY></TABLE><uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
