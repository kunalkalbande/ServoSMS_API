<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.Schemelist" CodeFile="Schemelist.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Scheme Updation Report</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
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
			else if(index == 6)                                        //Add by vikas 12.09.09 
				f.texthiddenprod.value=f.tempCustName.value;           //Add by vikas 12.09.09 
				
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
		//alert(f.texthiddenprod.value)
	}
	
	function SchemeName(t)
	{
			var index = t.selectedIndex
			var f = document.Form1;
			f.DropSchemeType.length = 0;
				f.DropSchemeType.add(new Option) 
				f.DropSchemeType.options[0].text="All";
			if(index == 1)
			{
				var Scheme=new Array();
				Scheme=f.tempsaleScheme.value.split(':');
				for(var i=0;i<=Scheme.length-1;i++)
				{
					f.DropSchemeType.add(new Option) 
					f.DropSchemeType.options[i+1].text=Scheme[i];
					f.DropSchemeType.options[i+1].value=Scheme[i];
				}
				
			}
			else if(index == 2)
			{
				
				var Scheme=new Array();
				Scheme=f.temppurScheme.value.split(':');
				for(var i=0;i<=Scheme.length-1;i++)
				{
					f.DropSchemeType.add(new Option) 
					f.DropSchemeType.options[i+1].text=Scheme[i];
					f.DropSchemeType.options[i+1].value=Scheme[i];
				}
				
			}
	}
	
	function SelectScheme(t)
	{
		var index = t.selectedIndex
		 document.Form1.tempschemtype.value=t.options[index].value;
		
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
			<input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server"> <input id="tempsaleScheme" style="WIDTH: 1px" type="hidden" name="tempsaleScheme" runat="server">
			<input id="temppurScheme" style="WIDTH: 1px" type="hidden" name="temppurScheme" runat="server">
			<input id="tempschemtype" style="WIDTH: 1px" type="hidden" name="tempschemtype" runat="server">
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center">
				<tr>
					<th colSpan="8" height="17">
						<font color="#ce4848">Scheme&nbsp;List Report</font>
						<hr>
					</th>
				</tr>
				<TR>
					<TD align="center" colSpan="8">Date From&nbsp;&nbsp;
						<asp:textbox id="txtDateFrom" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove"
							CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A> &nbsp;To&nbsp;
						<asp:textbox id="txtDateTo" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove"
							CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A> &nbsp;<asp:radiobutton id="RadBtnScheme" GroupName="Type" Text="Reseller Sch" Runat="server" Checked="True"
							AutoPostBack="True" oncheckedchanged="RadBtnScheme_CheckedChanged"></asp:radiobutton>&nbsp; &nbsp;<asp:radiobutton id="RadBtnReSale" GroupName="Type" Text="Fleet-OE" Runat="server" AutoPostBack="True" oncheckedchanged="RadBtnReSale_CheckedChanged"></asp:radiobutton>&nbsp; 
						&nbsp;&nbsp;<asp:label id="lblDroptype" Runat="server"></asp:label>
						<asp:dropdownlist id="dropsalpur" CssClass="dropdownlist" Runat="server" AutoPostBack="True" onselectedindexchanged="dropsalpur_SelectedIndexChanged">
							<asp:ListItem Value="Select">Select</asp:ListItem>
							<asp:ListItem Value="Sale">Sale</asp:ListItem>
							<asp:ListItem Value="Purchase">Purchase</asp:ListItem>
						</asp:dropdownlist><asp:dropdownlist id="DropSchemeType" runat="server" CssClass="dropdownlist">
							<asp:ListItem>All</asp:ListItem>
						</asp:dropdownlist><asp:dropdownlist id="DropReSailer" runat="server" CssClass="dropdownlist" Visible="False">
							<asp:ListItem Value="All">All</asp:ListItem>
						</asp:dropdownlist>&nbsp;</TD>
				</TR>
				<TR>
					<TD align="center">&nbsp;Search&nbsp;<asp:dropdownlist id="DropSearch" runat="server" Width="135" CssClass="dropdownlist" onchange="CheckSearchOption(this)">
							<asp:ListItem Value="All">All</asp:ListItem>
							<asp:ListItem Value="Product Code">Product Code</asp:ListItem>
							<asp:ListItem Value="Product ID">Product ID</asp:ListItem>
							<asp:ListItem Value="Product Name With Pack">Product Name With Pack</asp:ListItem>
							<asp:ListItem Value="Product Name">Product Name</asp:ListItem>
							<asp:ListItem Value="Pack Type">Pack Type</asp:ListItem>
							<asp:ListItem Value="Cust Name">Cust Name</asp:ListItem>
						</asp:dropdownlist></TD>
					<td>&nbsp;Value&nbsp;</td>
					<td>&nbsp;<input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
							onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.Button1)"
							style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							readOnly name="temp"><br>
						<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.Button1)"
								id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.Button1,document.Form1.DropValue)"
								style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
								type="select-one"></select></div>
					</td>
					<td align="left" colSpan="5">Group Name&nbsp;<asp:dropdownlist id="dropGroup" CssClass="dropdownlist" Runat="server">
							<asp:ListItem Value="Select">Select</asp:ListItem>
						</asp:dropdownlist></td>
				</TR>
				<tr>
					<td align="right" colSpan="8"><asp:button id="btnview" runat="server" Width="60px" Text="View" 
							 onclick="btnview_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnprint" runat="server" Width="60px" Text="Print" 
							 onclick="btnprint_Click"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel" 
							 onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<asp:panel id="Pansch" Runat="server">
					<TR>
						<TD colSpan="8">
							<TABLE align="center">
								<TR>
									<TD align="center" height="110">
										<asp:datagrid id="DataGrid1" runat="server" Width="900" BorderStyle="None" BorderColor="#DEBA84"
											BackColor="#DEBA84" CellSpacing="1" PageSize="7" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False"
											AllowSorting="True" OnSortCommand="sortcommand_click" onselectedindexchanged="DataGrid1_SelectedIndexChanged">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="s1" SortExpression="s1" HeaderText="Product&lt;br&gt;Id">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s11" SortExpression="s11" HeaderText="Product&lt;br&gt;Code">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s3" SortExpression="s3" HeaderText="Product Name">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s4" SortExpression="s4" HeaderText="Scheme Type">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s5" SortExpression="s5" HeaderText="Discount">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s6" SortExpression="s6" HeaderText="On Every">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s7" SortExpression="s7" HeaderText="Free Pack">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s10" SortExpression="s10" HeaderText="Scheme Product">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s12" SortExpression="s12" HeaderText="Group">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s8" SortExpression="s8" HeaderText="Datrfrom" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="s9" SortExpression="s9" HeaderText="Dateto" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Center" ForeColor="#8C4510"
												PageButtonCount="7" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</asp:panel><asp:panel id="panreseller" Runat="server">
					<TR>
						<TD colSpan="8">
							<TABLE align="center">
								<TR>
									<TD align="center" height="110">
										<asp:datagrid id="GridReseller" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
											CellSpacing="1" PageSize="7" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False"
											AllowSorting="True" OnSortCommand="sortcommand_click">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name"></asp:BoundColumn>
												<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Product" SortExpression="Product" HeaderText="Product Name">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Discount" SortExpression="Discount" HeaderText="Discount"></asp:BoundColumn>
												<asp:BoundColumn DataField="Distype" SortExpression="Distype" HeaderText="Discount Type">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="datefrom" SortExpression="datefrom" HeaderText="Effective From" DataFormatString="{0:dd/MM/yyyy}">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="dateto" SortExpression="dateto" HeaderText="Effective To" DataFormatString="{0:dd/MM/yyyy}">
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Ctype" SortExpression="Ctype" HeaderText="Cust Type">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Center" ForeColor="#8C4510"
												PageButtonCount="7" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</asp:panel></table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
