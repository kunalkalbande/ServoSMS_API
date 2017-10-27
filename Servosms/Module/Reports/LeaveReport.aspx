<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.LeaveReport" CodeFile="LeaveReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Leave Report</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
			<asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox><uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center" border="0">
				<TR>
					<TH align="center" height="20" colspan="2">
						<font color="#ce4848">Leave Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="right">
						Date From&nbsp;&nbsp;
						<asp:textbox id="txtDateFrom" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
							CssClass="fontstyle"></asp:textbox><A onClick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A> &nbsp;&nbsp;Date To&nbsp;&nbsp;
						<asp:textbox id="Textbox1" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
							CssClass="fontstyle"></asp:textbox><A onClick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0"></A>
					</td>
					<td>&nbsp;&nbsp;<asp:button id="btnShow" runat="server" Width="75px" Text="View" 
							onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="75px" Text="Print  " Runat="server" 
							onclick="BtnPrint_Click"></asp:button>&nbsp;
						<asp:button id="btnExcel" Width="75px" 
							Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr valign="top">
					<td align="center" colspan="2">
						<TABLE width="90%">
							<TR>
								<TD align="center" colSpan="5" style="HEIGHT: 160px"><asp:datagrid id="GridReport" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px"
										BackColor="#DEBA84" BorderColor="#DEBA84" CellPadding="1" CellSpacing="1" AllowSorting="True" OnSortCommand="SortCommand_Click" Width="90%">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" ForeColor="White" Height="25" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Emp_ID" SortExpression="Emp_ID" HeaderText="Employee ID">
												<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Emp_Name" SortExpression="Emp_Name" HeaderText="Employee Name"></asp:BoundColumn>
											<asp:BoundColumn DataField="Date_From" SortExpression="Date_From" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:BoundColumn DataField="Date_To" SortExpression="Date_To" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:BoundColumn DataField="Reason" SortExpression="Reason" HeaderText="Reason">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn SortExpression="isSanction" HeaderText="Approved">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<%#Approved(DataBinder.Eval(Container.DataItem,"isSanction").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center"
											ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
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
