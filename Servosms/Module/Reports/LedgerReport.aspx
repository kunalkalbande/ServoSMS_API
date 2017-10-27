<%@ Page language="c#" Inherits="Servosms.Module.Reports.LedgerReport" CodeFile="LedgerReport.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Ledger Report</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JScript" name="vs_defaultClientScript">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
				Visible="False" Width="8px"></asp:textbox><INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 0px; POSITION: absolute; HEIGHT: 20px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<TBODY>
					<TR>
						<TH vAlign="top" height="20">
							<font color="#ce4848">Ledger Report</font>
							<hr>
						</TH>
					</TR>
					<TR vAlign="top" height="20">
						<TD align="center">
							<TABLE cellSpacing="0" cellPadding="0">
								<TR>
									<TD vAlign="middle" align="center"></TD>
									<TD vAlign="top" align="center"></TD>
									<TD>Date From</TD>
									<TD><asp:textbox id="txtDateFrom" runat="server" Width="100px" BorderStyle="Groove" CssClass="dropdownlist"
											ReadOnly="True" ontextchanged="txtDateFrom_TextChanged"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD>To</TD>
									<TD><asp:textbox id="txtDateTo" runat="server" Width="100px" BorderStyle="Groove" CssClass="dropdownlist"
											ReadOnly="True" ontextchanged="txtDateTo_TextChanged"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<td>&nbsp;<u>Remark:</u><STRONG>CB</STRONG>(Closing Balance)&nbsp;</td>
								</TR>
								<TR>
									<TD vAlign="middle" align="center"></TD>
									<TD vAlign="top" align="center"></TD>
									<TD>Report Type</TD>
									<td colSpan="2"><asp:dropdownlist id="DropReportType" Width="135" CssClass="dropdownlist" Runat="server">
											<asp:ListItem Value="Detail Ledger">Detail Ledger</asp:ListItem>
											<asp:ListItem Value="Summerized Ledger">Summerized Ledger</asp:ListItem>
										</asp:dropdownlist>&nbsp;&nbsp;&nbsp; Party Name&nbsp;&nbsp;</td>
									<td colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropPartyName"
											onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropPartyName,document.Form1.cmdrpt)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 210px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											value="Select" name="DropPartyName" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											readOnly type="text" name="temp"><br>
										<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropPartyName,document.Form1.cmdrpt)"
												id="DropProdName" ondblclick="select(this,document.Form1.DropPartyName)" onkeyup="arrowkeyselect(this,event,document.Form1.cmdrpt,document.Form1.DropPartyName)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 230px; HEIGHT: 0px" multiple name="DropProdName" type="select-one"></select></div>
									</td>
								</TR>
								<TR>
								<tr>
									<TD align="center" colSpan="11"><asp:button id="cmdrpt" runat="server" Width="60px" 
											Text="View " onclick="cmdrpt_Click"></asp:button>&nbsp;&nbsp;&nbsp;
										<asp:button id="BtnPrint" Width="60px" Runat="server" 
											 Text="Print" onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;
										<asp:button id="btnExcel" Width="60px" Runat="server" 
											 Text="Excel" onclick="btnExcel_Click"></asp:button></TD>
								</tr>
							</TABLE>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD align="center"><asp:datagrid id="Datagrid1" runat="server" Visible="False" Width="500px" BorderStyle="None" BackColor="#DEBA84"
								BorderColor="#DEBA84" OnSortCommand="sortcommand_click" AllowSorting="True" CellSpacing="1" OnItemDataBound="ItemTotal1"
								AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1" onselectedindexchanged="Datagrid1_SelectedIndexChanged">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn HeaderText="Transaction No." FooterText="Total:">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Particulars" SortExpression="Particulars" HeaderText="Transaction Type">
										<HeaderStyle Width="120px"></HeaderStyle>
										<ItemStyle Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Entry_Date" SortExpression="Entry_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle Width="60px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Debit_Amount" SortExpression="Debit_Amount" HeaderText="Debit" DataFormatString="{0:N2}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Credit_Amount" SortExpression="Credit_Amount" HeaderText="Credit" DataFormatString="{0:N2}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="balance" HeaderText="Closing Balance">
										<HeaderStyle Width="120px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
										<ItemTemplate>
											<%#setBal(GenUtil.strNumericFormat(DataBinder.Eval(Container.DataItem,"balance","{0:N2}").ToString()))%>
											<!--%#setBal(GenUtil.strNumericFormat(DataBinder.Eval(Container.DataItem,"debit_amount","{0:N2}")).ToString(),DataBinder.Eval(Container.DataItem,"credit_amount").ToString(),DataBinder.Eval(Container.DataItem,"particulars").ToString(),DataBinder.Eval(Container.DataItem,"balance").ToString(),DataBinder.Eval(Container.DataItem,"bal_type").ToString())%-->
											<%#setType(DataBinder.Eval(Container.DataItem,"bal_type").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="CustomerGrid" runat="server" Visible="False" Width="500px" BorderStyle="None"
								BackColor="#DEBA84" BorderColor="#DEBA84" OnSortCommand="sortcommand_click" AllowSorting="True" CellSpacing="1"
								OnItemDataBound="ItemTotal" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn HeaderText="Transaction No." FooterText="Total:">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Particulars" SortExpression="Particulars" HeaderText="Transaction Type">
										<HeaderStyle Width="120px"></HeaderStyle>
										<ItemStyle Width="120px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Entry_Date" SortExpression="Entry_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle Width="60px"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Debit_Amount" SortExpression="Debit_Amount" HeaderText="Debit" DataFormatString="{0:N2}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Credit_Amount" SortExpression="Credit_Amount" HeaderText="Credit" DataFormatString="{0:N2}">
										<HeaderStyle Width="60px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="balance" HeaderText="Closing Balance">
										<HeaderStyle Width="120px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
										<ItemTemplate>
											<%#setBal(GenUtil.strNumericFormat(DataBinder.Eval(Container.DataItem,"balance","{0:N2}").ToString()))%>
											<!--%#setBal(GenUtil.strNumericFormat(DataBinder.Eval(Container.DataItem,"debit_amount","{0:N2}").ToString()),DataBinder.Eval(Container.DataItem,"credit_amount").ToString(),DataBinder.Eval(Container.DataItem,"Particulars").ToString(),DataBinder.Eval(Container.DataItem,"balance").ToString(),DataBinder.Eval(Container.DataItem,"bal_type").ToString())%-->
											<%#setType(DataBinder.Eval(Container.DataItem,"bal_type").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="GridSummerized" runat="server" Width="500" BorderStyle="None" BackColor="#DEBA84"
								BorderColor="#DEBA84" OnSortCommand="sortcommand_click" AllowSorting="True" CellSpacing="1" OnItemDataBound="ItemTotal2"
								AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1" ShowFooter="True">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"
									Height="25"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Month" FooterText="Total">
										<HeaderStyle Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"entry_date").ToString()%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Debit_Amount" HeaderText="Debit_Amount">
										<HeaderStyle Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
										<ItemTemplate>
											<%#GetDebit(DataBinder.Eval(Container.DataItem,"Debit_Amount","{0:N2}").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px"></FooterStyle>
										<FooterTemplate>
											<%=GenUtil.strNumericFormat(Cache["dr"].ToString())%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Credit_Amount" HeaderText="Credit_Amount">
										<HeaderStyle Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
										<ItemTemplate>
											<%#GetCredit(DataBinder.Eval(Container.DataItem,"Credit_Amount","{0:N2}").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="100px"></FooterStyle>
										<FooterTemplate>
											<%=GenUtil.strNumericFormat(Cache["cr"].ToString())%>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="balance" HeaderText="Closing Balance">
										<HeaderStyle Width="130px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right" Width="130px"></ItemStyle>
										<ItemTemplate>
											<!--%#setCBal(DataBinder.Eval(Container.DataItem,"Debit_Amount").ToString(),DataBinder.Eval(Container.DataItem,"Credit_Amount").ToString())%-->
											<%#setCBal(DataBinder.Eval(Container.DataItem,"Entry_Date").ToString(),DataBinder.Eval(Container.DataItem,"dateyear").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right" Width="130px"></FooterStyle>
										<FooterTemplate>
											<!--%#setMonth()%-->
											<%=ClosingBal%>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TBODY>
			</table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
