<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="DBOperations"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.PWSDCReport" CodeFile="PWSDCReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Product Wise Secondary Discount Claim Report</title> 
		<!--
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
					f.texthiddenprod.value=f.tempDistrict.value;
				else if(index == 4)
					f.texthiddenprod.value=f.tempCustName.value;
				else if(index == 5)
					f.texthiddenprod.value=f.tempPlace.value;
				else if(index == 6)
					f.texthiddenprod.value=f.tempSSR.value;
				f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
			}
			else
				f.texthiddenprod.value="";
			document.Form1.DropValue.value="All";
			document.Form1.DropProdName.style.visibility="hidden"
		}
	
		function ShowHide()
		{
			if(document.Form1.dropType.selectedIndex==1)
			{
				//document.Form1.elements[10].style.visibility="hidden"
				//document.Form1.elements[11].style.visibility="hidden"
				document.Form1.txtMessage.style.visibility="hidden"
				document.Form1.dropValueType.style.visibility="hidden"
			}
			else
			{
				//document.Form1.elements[10].style.visibility="visible"
				//document.Form1.elements[11].style.visibility="visible"
				document.Form1.txtMessage.style.visibility="visible"
				document.Form1.dropValueType.style.visibility="visible"
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
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" align="center">
				<TBODY>
					<TR>
						<TH vAlign="top" colSpan="9" height="20">
							<font color="#ce4848">Product Wise Secondary Discount Claim Report</font>
							<hr>
						</TH>
					</TR>
					<tr vAlign="top" height="20">
						<td align="center">Search By</td>
						<td><asp:dropdownlist id="DropSearchBy" runat="server" onchange="CheckSearchOption(this)" CssClass="dropdownlist" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
								<asp:ListItem Value="All">All</asp:ListItem>
								<asp:ListItem Value="Group">Group</asp:ListItem>
								<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
								<asp:ListItem Value="District">District</asp:ListItem>
								<asp:ListItem Value="Name">Name</asp:ListItem>
								<asp:ListItem Value="Place">Place</asp:ListItem>
								<asp:ListItem Value="SSR">SSR</asp:ListItem>
							</asp:dropdownlist></td>
						<td>Value</td>
						<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
								onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnview1)"
								style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 210px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
								value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
								readOnly type="text" name="temp"><br>
							<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnview1)"
									id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnview1,document.Form1.DropValue)"
									style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 230px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
									type="select-one"></select></div>
						</td>
						<td>From</td>
						<td><asp:textbox id="txtDateFrom" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td>To</td>
						<td><asp:textbox id="txtDateTo" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td><asp:button id="btnview1" Width="65px" Runat="server" Text="View" 
								 onclick="btnview1_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="65px" Runat="server" Text="Excel" 
								 onclick="btnExcel_Click"></asp:button></td>
					</tr>
					<tr>
						<td align="center" colSpan="9" height="100"><asp:datagrid id="DataGrid" runat="server" Width="700px" BorderStyle="None" BackColor="#DEBA84"
								BorderColor="#DEBA84" ShowFooter="True" OnItemDataBound="ItemTotal" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1"
								CellSpacing="1" OnSortCommand="sortcommand_click" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle Height="2px" ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" Height="2px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn SortExpression="Product" HeaderText="SN">
										<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
										<ItemTemplate>
											<%#i++%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Date" FooterText="Total">
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Bill No">
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Cust_Type" SortExpression="Cust_Type" HeaderText="Cust Type">
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name">
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Prod Code">
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Prod_Name" SortExpression="Net" HeaderText="Invoice Amt">
										<HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</tr>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
