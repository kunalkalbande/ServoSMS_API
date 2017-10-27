<%@ Page language="c#" Inherits="Servosms.Module.Reports.Return_Report" CodeFile="Return_Report.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS : Return Report</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../../Sysitem/Styles.css" type=text/css rel=stylesheet >
<script language=javascript id=Validations src="../../Sysitem/JS/Validations.js"></script>

<script language=javascript>
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

<script language=javascript>
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
<body onkeydown=change(event)>
<form id=Form1 method=post runat="server"><uc1:header id=Header1 runat="server"></uc1:header><asp:textbox id=TextBox1 style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server" Width="8px" Visible="False"></asp:textbox>
<table height=290 width=778 align=center border=0>
  <TBODY>
  <TR>
    <TH vAlign=top height=20><font color=#ce4848 
      >Sales / Purchase Return Report</font> 
      <hr>
    </TH></TR>
  <TR>
    <TD vAlign=top align=center height=20>
      <TABLE width="100%" border=0>
        <TR>
          <TD>Date From</TD>
          <TD><asp:textbox id=txtDateFrom runat="server" Width="110px" BorderStyle="Groove" ReadOnly="True" CssClass="fontstyle"></asp:textbox><A 
            onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;" 
            ><IMG class=PopcalTrigger alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align=absMiddle border=0 ></A></TD>
          <TD align=center>To</TD>
          <TD><asp:textbox id=txtDateTo runat="server" Width="110px" BorderStyle="Groove" ReadOnly="True" CssClass="fontstyle"></asp:textbox><A 
            onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;" 
            ><IMG class=PopcalTrigger alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align=absMiddle border=0 ></A></TD>
          <TD>Report Type</TD>
          <TD><asp:dropdownlist id=DropReportType runat="server" Width="90px" CssClass="fontstyle">
											<asp:ListItem Value="Both" Selected="True">Both</asp:ListItem>
											<asp:ListItem Value="Sales Return">Sales Return</asp:ListItem>
											<asp:ListItem Value="Purchase Return">Purchase Return</asp:ListItem>
										</asp:dropdownlist></TD>
          <td><asp:button id=cmdrpt runat="server" Width="60px"  Text="View " onclick="cmdrpt_Click"></asp:button>&nbsp;<asp:button id=BtnPrint Width="60px" Text="Print" Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id=btnExcel Width="60px"  Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td></TR></TABLE></TD></TR>
  <TR>
    <TD align=center>&nbsp;</TD></TR>
  <TR>
    <TD vAlign=top align=center><asp:label id=lblSales Visible="False" ForeColor="#CE4848" Runat="server" Font-Bold="True"></asp:label></TD></TR>
  <TR>
    <TD vAlign=top align=center><asp:datagrid id=SalesGrid runat="server" Visible="False" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" ShowFooter="True" OnSortCommand="sortcommand_click1" CellSpacing="1" OnItemDataBound="ItemTotal" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No.">
										<HeaderStyle Width="60px"></HeaderStyle>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Entry_time" SortExpression="Entry_Time" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
										<HeaderStyle Width="150px"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Product" SortExpression="Product" HeaderText="Product Name">
										<HeaderStyle Width="150px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Qty" SortExpression="Qty" HeaderText="Qty">
										<HeaderStyle Width="60px"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign=Left></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Ltr" SortExpression="Ltr" HeaderText="Ltr" DataFormatString="{0:N2}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Rate" SortExpression="Rate" HeaderText="Price">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Vat Amount">
										<HeaderStyle Width="70px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#GetVat(DataBinder.Eval(Container.DataItem,"qty").ToString(),DataBinder.Eval(Container.DataItem,"Rate").ToString())%>
										</ItemTemplate>
										<FooterTemplate>
										<%#GenUtil.strNumericFormat(total_Vat.ToString())%>
										</FooterTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="InvoiceAmount">
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<%#Multiply1(DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Net_Amount").ToString())%>
										</ItemTemplate>
										<FooterTemplate>
										<%#GenUtil.strNumericFormat(Cache["Saleam"].ToString())%>
									</FooterTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD></TR>
  <tr>
    <td>&nbsp;</td></tr>
  <TR>
    <TD vAlign=top align=center><asp:label id=lblPurchas Visible="False" ForeColor="#CE4848" Runat="server" Font-Bold="True"></asp:label></TD></TR>
  <tr>
    <td vAlign=top align=center><asp:datagrid id=PurchaseGrid runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84" CellPadding="1" BorderWidth="0px" AutoGenerateColumns="False" ShowFooter="True" OnSortCommand="sortcommand_click2" CellSpacing="1" OnItemDataBound="ItemTotal1" AllowSorting="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn DataField="Vndr_Invoice_No" SortExpression="Vndr_Invoice_No" HeaderText="Invoice No">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Entry_time" SortExpression="Entry_time" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="60px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Supp_Name" SortExpression="Supp_Name" HeaderText="Vendor Name" FooterText="Total">
										<HeaderStyle Width="150px"></HeaderStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Product" SortExpression="Product" HeaderText="Product">
										<HeaderStyle Width="150px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Qty" SortExpression="Qty" HeaderText="Qty">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign=Left></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Ltr" SortExpression="Ltr" HeaderText="Ltr">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Rate" SortExpression="Rate" HeaderText="Price">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="VAT Amount">
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<%#Multiply3(DataBinder.Eval(Container.DataItem,"Vndr_Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"VAT_Amount").ToString())%>
										</ItemTemplate>
										<FooterTemplate>
										<%#GenUtil.strNumericFormat(Cache["VAT"].ToString())%>
									</FooterTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="InvoiceAmount">
										<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
										<ItemTemplate>
											<%#Multiply2(DataBinder.Eval(Container.DataItem,"Vndr_Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_No").ToString(),DataBinder.Eval(Container.DataItem,"Net_Amount").ToString())%>
										</ItemTemplate>
										<FooterTemplate>
										<%#GenUtil.strNumericFormat(Cache["am"].ToString())%>
									</FooterTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td></tr></TBODY></table></TD></TR></TBODY></TABLE><iframe 
id=gToday:contrast:agenda.js 
style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px" 
name=gToday:contrast:agenda.js src="../../HeaderFooter/DTPicker/ipopeng.htm" 
frameBorder=0 width=174 scrolling=no height=189> </iframe><uc1:footer id=Footer1 runat="server"></uc1:footer></form>
	</body>
</HTML>
