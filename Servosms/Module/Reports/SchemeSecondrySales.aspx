<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SchemeSecondrySales" CodeFile="SchemeSecondrySales.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>SevoSMS: Scheme Secondry Sales Report</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE><LINK href="../../Sysitem/Styles.css" type=text/css rel=stylesheet >
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
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
</HEAD>
<body>
<form id=Form1 method=post runat="server"><uc1:header id=Header1 runat="server"></uc1:header>
<table height=278 width=778 align=center border=0>
  <tr height=20>
    <th colSpan=2><font color=#ce4848 
      >Scheme Secondry Sales Report</FONT> 
      <hr>
    </TH></TR>
  <tr height=20>
    <td align=right width="80%">Type&nbsp;<asp:dropdownlist id=droptype CssClass="dropdownlist" Runat="server">
    <asp:ListItem Value="Existing">Existing</asp:ListItem>
    <asp:ListItem Value="New PWSDC">New PWSDC</asp:ListItem>
    </asp:dropdownlist> Date From&nbsp;&nbsp; <asp:textbox id=txtDateFrom runat="server" CssClass="fontstyle" BorderStyle="Groove" ReadOnly="True" Width="110px"></asp:textbox><A 
      onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;" 
      ><IMG class=PopcalTrigger alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align=absMiddle border=0 ></A> 
      &nbsp;&nbsp;Date To&nbsp;&nbsp; <asp:textbox id=txtDateTo runat="server" CssClass="fontstyle" BorderStyle="Groove" ReadOnly="True" Width="110px"></asp:textbox><A 
      onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;" 
      ><IMG class=PopcalTrigger alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align=absMiddle border=0 ></A> 
&nbsp;&nbsp;<asp:checkbox id=chkFOC CssClass="dropdownlist" Runat="server" Text="Show FOC Product"></asp:checkbox> 
    </TD>
    <td width="20%">&nbsp;&nbsp;<asp:button id=btnShow runat="server" Width="60px" Text="View"  onclick="btnShow_Click"></asp:button>&nbsp; 
<asp:button id=btnExcel Width="60px" Runat="server" Text="Excel"  onclick="btnExcel_Click"></asp:button></TD></TR>
  <tr>
    <td colSpan=2>
      <table border=0>
        <tr>
          <td align=center><asp:datagrid id=gridSchemeSecondry BorderStyle="None" Width="100%" Runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" ShowFooter="True" AutoGenerateColumns="False" OnSortCommand="SortCommand_Click" AllowSorting="True" CellSpacing="1" CellPadding="1" BorderWidth="0px">
<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C">
</SelectedItemStyle>

<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7">
</ItemStyle>

<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848">
</HeaderStyle>

<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5">
</FooterStyle>

<Columns>
<asp:BoundColumn DataField="ProdName" SortExpression="ProdName" HeaderText="Product" FooterText="Total">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Material Code">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="Name Of Customer Category">
<HeaderStyle HorizontalAlign="Center" Width="150px">
</HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="150px">
</ItemStyle>

<HeaderTemplate>
<table width=100% cellpadding=0 cellspacing=0 border=1 rules=cols bordercolor="#CE4848">
<tr><td colspan=5 align=center><font color=white>Name of Customer Category<hr color="#DEBA84"></font></td></tr>
<tr>
<td align=center width=20%><font color=white>RO</font></td>
<td align=center width=20%><font color=white>Bazar</font></td>
<td align=center width=20%><font color=white>Fleet</font></td>
<td align=center width=20%><font color=white>OE</font></td>
<td align=center width=20%><font color=white>Others</font></td>
</tr>
</table width=100% cellpadding=0 cellspacing=0 border=0>
</HeaderTemplate>

<ItemTemplate>
<table width=100% cellpadding=0 cellspacing=0>
<tr>
<td width=20%><font color=#8C4510><%#GetRO(DataBinder.Eval(Container.DataItem,"Cust_Type").ToString(),DataBinder.Eval(Container.DataItem,"qty").ToString())%></font></td>
<td width=20%><font color=#8C4510><%#GetBazar(DataBinder.Eval(Container.DataItem,"Cust_Type").ToString(),DataBinder.Eval(Container.DataItem,"qty").ToString())%></font></td>
<td width=20%><font color=#8C4510><%#GetFleet(DataBinder.Eval(Container.DataItem,"Cust_Type").ToString(),DataBinder.Eval(Container.DataItem,"qty").ToString())%></font></td>
<td width=20%><font color=#8C4510><%#GetOE(DataBinder.Eval(Container.DataItem,"Cust_Type").ToString(),DataBinder.Eval(Container.DataItem,"qty").ToString())%></font></td>
<td width=20%><font color=#8C4510><%#GetOthers(DataBinder.Eval(Container.DataItem,"Cust_Type").ToString(),DataBinder.Eval(Container.DataItem,"qty").ToString())%></font></td>
</tr>
</table>
</ItemTemplate>

<FooterTemplate>
<table width=100% cellpadding=0 cellspacing=0>
<tr>
<td width=20%><font color=#8C4510><b><%=TotalRo.ToString()%></b></font></td>
<td width=20%><font color=#8C4510><b><%=TotalBazar.ToString()%></b></font></td>
<td width=20%><font color=#8C4510><b><%=TotalFleet.ToString()%></b></font></td>
<td width=20%><font color=#8C4510><b><%=TotalOE.ToString()%></b></font></td>
<td width=20%><font color=#8C4510><b><%=TotalOthers.ToString()%></b></font></td>
</tr>
</table>
</FooterTemplate>
</asp:TemplateColumn>
<asp:BoundColumn DataField="Cust_type" SortExpression="Cust_type" HeaderText="Category of Customer">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="LTR Per&lt;br&gt;Discount">
<HeaderStyle HorizontalAlign="Center">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center">
</ItemStyle>

<ItemTemplate>
	<%#GetDiscount(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString(),DataBinder.Eval(Container.DataItem,"Invoice_Date").ToString())%>
</ItemTemplate>

<FooterStyle HorizontalAlign="Center">
</FooterStyle>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Discount allowed&lt;br&gt;since previous report">
<HeaderStyle HorizontalAlign="Center" Width="120px">
</HeaderStyle>

<HeaderTemplate>
<table width=100% border=1 cellpadding=0 bordercolor="#CE4848" cellspacing=0 rules=cols>
<tr><td colspan=5 align=center><font color=white>Discount allowed since<br>previous report<hr color="#DEBA84"></font></td></tr>
<tr>
<td align=center><font color=white>Qty(KL)</font></td>
<td align=right><font color=white>Amount(Rs.)</font></td>
</tr>
</table>
</HeaderTemplate>

<ItemTemplate>
<table width=100% border=0 cellpadding=0 cellspacing=0>
<tr>
<td align=left><font color=#8C4510><%#DataBinder.Eval(Container.DataItem,"Qty").ToString()%></font></td>
<td align=right><font color=#8C4510><%#GetAmount(DataBinder.Eval(Container.DataItem,"Qty").ToString())%></font></td>
</tr>
</table>
</ItemTemplate>

<FooterTemplate>
<table width=100% border=0 cellpadding=0 cellspacing=0>
<tr>
<td align=left><font color=#8C4510>&nbsp;</font></td>
<td align=right><font color=#8C4510><b><%=TotalAmount.ToString()%></b></font></td>
</tr>
</table>
</FooterTemplate>
</asp:TemplateColumn>
</Columns>
</asp:datagrid></TD></TR>
        <tr>
          <td align=center><asp:datagrid id=DatagPWSDC BorderStyle="None" Width="900px" Runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" ShowFooter="True" AutoGenerateColumns="False" OnSortCommand="SortCommand_Click" AllowSorting="True" CellSpacing="1" CellPadding="1" BorderWidth="0px">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
				<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
				<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
				<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
				<Columns>
				<asp:TemplateColumn SortExpression="Qty" HeaderText="Disc" >
									<ItemStyle HorizontalAlign=Right Wrap="False"></ItemStyle>
									<ItemTemplate>
										<%#i++%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
					<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
						<asp:BoundColumn DataField="Cust_type" SortExpression="Cust_type" HeaderText="Cust Type">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Prod Code">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ProdName" SortExpression="ProdName" HeaderText="Product" FooterText="Total">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
						<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type">
						<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ltr" SortExpression="ltr" HeaderText="Qty">
						<HeaderStyle HorizontalAlign=Center></HeaderStyle>
						<ItemStyle HorizontalAlign=Right></ItemStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Qty" SortExpression="Qty" HeaderText="LTR">
						<HeaderStyle HorizontalAlign=Center></HeaderStyle>
						<ItemStyle HorizontalAlign=Right></ItemStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn SortExpression="Qty" HeaderText="Disc" >
									<ItemStyle HorizontalAlign=Right Wrap="False"></ItemStyle>
									<ItemTemplate>
										<%#Discount(DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="Qty" HeaderText="Sch Disc Total">
									<ItemStyle HorizontalAlign=Right Wrap="False"></ItemStyle>
									<ItemTemplate>
										<%#DiscAmt(DataBinder.Eval(Container.DataItem,"Prod_id").ToString(),DataBinder.Eval(Container.DataItem,"Qty").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign=Right></FooterStyle>
									<FooterTemplate><%#TotClaim.ToString()%></FooterTemplate>
								</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></TD></TR></TABLE></TD></TR></TABLE><iframe 
id=gToday:contrast:agenda.js 
style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px" 
name=gToday:contrast:agenda.js src="../../HeaderFooter/DTPicker/ipopeng.htm" 
frameBorder=0 width=174 scrolling=no height=189></IFRAME><uc1:footer id=Footer1 runat="server"></uc1:footer></FORM>
	</body>
</HTML>
