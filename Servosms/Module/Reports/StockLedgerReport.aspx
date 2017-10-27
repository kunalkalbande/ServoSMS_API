<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.StockLedgerReport" CodeFile="StockLedgerReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Stock Ledger Report</title> <!--
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
</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 152px; POSITION: absolute; TOP: 8px" runat="server"
				Width="8px" Visible="False"></asp:textbox><INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<TR vAlign="top" height="20">
					<TH>
						<font color="#ce4848">Stock Ledger Report</font>
						<hr>
					</TH>
				</TR>
				<TR vAlign="top">
					<TD align="center">
						<TABLE width="80%">
							<TR>
								<TD>From Date</TD>
								<TD><asp:textbox id="txtDateFrom" runat="server" Width="80px" BorderStyle="Groove" CssClass="fontstyle"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;To 
									Date&nbsp;&nbsp;&nbsp;
									<asp:textbox id="txtDateTo" runat="server" Width="80px" BorderStyle="Groove" CssClass="fontstyle"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<td colSpan="2"><asp:button id="cmdrpt" runat="server" Width="60px" 
										 Text="View " onclick="cmdrpt_Click"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="btnPrint" Width="60px" 
										Text="Print  " Runat="server" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;
									<asp:button id="btnExcel" Width="60px" 
										Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
							</TR>
							<tr>
								<TD>Product Name</TD>
								<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="drpProductName"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.drpProductName,document.Form1.cmdrpt)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 260px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="Select" name="drpProductName" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.drpProductName,document.Form1.cmdrpt)"
											id="DropProdName" ondblclick="select(this,document.Form1.drpProductName)" onkeyup="arrowkeyselect(this,event,document.Form1.cmdrpt,document.Form1.drpProductName)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 280px; HEIGHT: 0px" multiple name="DropProdName"></select></div>
								</td>
								<TD>Transaction Type</TD>
								<td><asp:dropdownlist id="drpTransType" runat="server" Width="111px" CssClass="dropdownlist">
										<asp:ListItem Value="All" Selected="True">All</asp:ListItem>
										<asp:ListItem Value="Sales">Sales</asp:ListItem>
										<asp:ListItem Value="Purchase">Purchase</asp:ListItem>
										<asp:ListItem Value="Others">Others</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr vAlign="top">
					<td align="center"><asp:datagrid id="Stock_Ledger" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
							BackColor="#DEBA84" ShowFooter="True" OnItemDataBound="ItemDataBound" OnSortCommand="sortcommand_click" AllowSorting="True"
							CellSpacing="1" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="30px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Shipping Party" FooterText="Total">
									<HeaderStyle Width="140px"></HeaderStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Trans_Type" SortExpression="Trans_Type" HeaderText="Trans. Type">
									<HeaderStyle Width="90px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Trans_ID" SortExpression="Trans_ID" HeaderText="Trans. ID">
									<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Trans_Date" SortExpression="Trans_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
									<HeaderStyle Width="60px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="In_Qty_Nos" HeaderText="IN&lt;br&gt;Qty in Nos.&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Qty in Ltr">
									<HeaderStyle HorizontalAlign="Center" Width="130px"></HeaderStyle>
									<ItemTemplate>
										<TABLE border="0" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="60"><font color="#8C4510"><%#checkValue(DataBinder.Eval(Container.DataItem,"In_Qty_Nos").ToString())%></font></TD>
												<TD align="right" width="60"><font color="#8C4510"><%#checkValue(DataBinder.Eval(Container.DataItem,"In_Qty_Ltr").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE border="0" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="60"><font color="#8C4510"><b><%=InQtyNos.ToString()%></b></font></TD>
												<TD align="right" width="60"><font color="#8C4510"><b><%=InQtyLtr.ToString()%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Out_Qty_Nos" HeaderText="OUT&lt;br&gt;Qty in Nos.&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Qty in Ltr">
									<HeaderStyle HorizontalAlign="Center" Width="140px"></HeaderStyle>
									<ItemTemplate>
										<TABLE border="0" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="60"><font color="#8C4510"><%#checkValue(DataBinder.Eval(Container.DataItem,"Out_Qty_Nos").ToString())%></font></TD>
												<TD align="right" width="60"><font color="#8C4510"><%#checkValue(DataBinder.Eval(Container.DataItem,"Out_Qty_Ltr").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE border="0" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="60"><font color="#8C4510"><b><%=OutQtyNos.ToString()%></b></font></TD>
												<TD align="right" width="60"><font color="#8C4510"><b><%=OutQtyLtr.ToString()%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Closing_Bal_Nos" HeaderText="CLOSING BALANCE&lt;br&gt;In Nos.&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;In Ltr">
									<HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
									<ItemTemplate>
										<TABLE border="0" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="60"><font color="#8C4510"><%#checkValue(DataBinder.Eval(Container.DataItem,"Closing_Bal_Nos").ToString())%></font></TD>
												<TD align="right" width="60"><font color="#8C4510"><%#checkValue(DataBinder.Eval(Container.DataItem,"Closing_Bal_Ltr").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterTemplate>
										<TABLE border="0" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="60"><font color="#8C4510"><b><%=closing_bal_nos.ToString()%></b></font></TD>
												<TD align="right" width="60"><font color="#8C4510"><b><%=closing_bal_ltr.ToString()%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
				<tr>
					<td align="right"><A href="javascript:window.print()"></A></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
