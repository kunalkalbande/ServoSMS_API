<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.StockMovement" CodeFile="StockMovement.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Stock Movement Reportl</title> 
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
				Visible="False" Width="8px"></asp:textbox>
			<table height="290" width="778" align="center">
				<TR valign="top" height="20">
					<TH>
						<font color="#ce4848">Stock Movement Report</font>
						<hr>
					</TH>
				</TR>
				<TR valign="top">
					<TD align="center">
						<TABLE>
							<TR>
								<TD align="center">Date From&nbsp;
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Date Required">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateFrom" runat="server" Width="80px" CssClass="fontstyle" ReadOnly="True"
										BorderStyle="Groove"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD align="center" colSpan="1" rowSpan="1">To&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateTo" runat="server" Width="80px" CssClass="fontstyle" ReadOnly="True"
										BorderStyle="Groove"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD vAlign="middle">Stock Location&nbsp;&nbsp;
									<asp:dropdownlist id="drpstore" runat="server" Width="100px" CssClass="fontstyle"></asp:dropdownlist></TD>
								<td>&nbsp;&nbsp;Pack Type&nbsp;&nbsp;<asp:dropdownlist id="dropPackType" runat="server" Width="100px" CssClass="fontstyle"></asp:dropdownlist></td>
							</TR>
							<tr>
								<td>Category</td>
								<td><asp:dropdownlist id="dropcategory" runat="server" CssClass="fontstyle"></asp:dropdownlist></td>
								<td align="right" colSpan="4"><asp:radiobutton id="RadSM" Runat="server" Text="StockMovement" GroupName="Stock" Checked="True"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="RadSJ" Runat="server" Text="SJ" GroupName="Stock" oncheckedchanged="RadSJ_CheckedChanged"></asp:radiobutton>
									&nbsp;&nbsp;&nbsp;<asp:checkbox id="chkZeroBal" Runat="server"></asp:checkbox>&nbsp;&nbsp;Zero&nbsp;Stock&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="cmdrpt" runat="server" Width="60px" Text="View" 
										 onclick="cmdrpt_Click"></asp:button>&nbsp;
									<asp:button id="prnButton" runat="server" Width="60px" Text=" Print " 
										 onclick="prnButton_Click"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel" 
										 onclick="btnExcel_Click"></asp:button></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
				<tr height="150">
					<td align="center"><asp:datagrid id="grdLeg" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" OnSortCommand="sortcommand_click"
							AllowSorting="True" ShowFooter="True" CellPadding="1" BorderWidth="0px" BorderStyle="None" CellSpacing="1"
							Height="8px" AutoGenerateColumns="False" onselectedindexchanged="grdLeg_SelectedIndexChanged">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code" FooterText="Total">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="Prod_Name" HeaderText="Product Name">
									<HeaderStyle ForeColor="White" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="location" HeaderText="Location">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<%#IsTank(DataBinder.Eval(Container.DataItem,"location").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="op" HeaderText="Opening Stock&lt;br&gt;Pkg &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD width="100%"><font color="#8C4510"><b><%=Cache["osp"]%></b></font></TD>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["os"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="rcpt" HeaderText="Receipt&lt;br&gt;Pkg &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD width="100%"><font color="#8C4510"><b><%=Cache["rectp"]%></b></font></TD>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["rect"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="Sales&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD width="100%"><font color="#8C4510"><b><%=Cache["salesp"]%></b></font></TD>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["sales"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Closing Stock&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD width="100%"><font color="#8C4510"><b><%=Cache["csp"]%></b></font></TD>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["cs"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:datagrid id="gridSJ" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" OnSortCommand="sortcommand_click"
							AllowSorting="True" ShowFooter="True" CellPadding="1" BorderWidth="0px" BorderStyle="None" CellSpacing="1"
							Height="8px" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code" FooterText="Total">
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Product Name">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="op" HeaderText="Opening&lt;br&gt; Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["os"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="rcpt" HeaderText="Receipt&lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["rect"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Receipt&lt;br&gt;(FOC)Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"rcptfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["sales"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="SJ&lt;br&gt;In">
									<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#getSJOut(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%>
									</ItemTemplate>
									<FooterTemplate>
										<b>
											<%=SJOut.ToString()%>
										</b>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="Sales&lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ1(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"salesfoc").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["cs"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Sales&lt;br&gt;(FOC)Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["fsale"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="SJ&lt;br&gt;Out">
									<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#getSJIn(DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%>
									</ItemTemplate>
									<FooterTemplate>
										<b>
											<%=SJin.ToString()%>
										</b>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Closing &lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["fpur"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
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
