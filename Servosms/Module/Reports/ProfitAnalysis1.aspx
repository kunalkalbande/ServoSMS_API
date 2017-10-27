<%@ Page language="c#" Inherits="Servosms.Module.Reports.ProfitAnalysis1" CodeFile="ProfitAnalysis1.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Profit Analysis Report</title> <!--
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
	<body language="javascript" onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox>
			<table height="290" width="778" align="center">
				<TR>
					<TH style="HEIGHT: 4px">
						<font color="#CE4848">Profit Analysis&nbsp;Report</font>
						<hr>
					</TH>
				</TR>
				<TR>
					<TD align="center" style="HEIGHT: 40px">
						<TABLE>
							<TR>
								<TD align="center">Date From&nbsp;&nbsp;
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Date Required" ControlToValidate="txtDateFrom">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateFrom" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD align="center" colSpan="1" rowSpan="1">To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateTo" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD vAlign="middle">&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="cmdrpt" runat="server" Width="60px" Text="View" 
										 onclick="cmdrpt_Click"></asp:button>
									<asp:button id="prnButton" runat="server" Width="60px" Text=" Print " 
										 onclick="prnButton_Click"></asp:button>
									<asp:button id="btnExcel" runat="server" Width="60px" 
										 Text="Excel" onclick="btnExcel_Click"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td>
						<!--***************************************************-->
						<table style="WIDTH: 589px; HEIGHT: 130px" height="130" width="589" align="center">
							<TBODY>
								<tr>
									<td colspan="4" valign="middle"><hr size="1" color="#CE4848">
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 110px; HEIGHT: 17px">Opening Stock</td>
									<td style="WIDTH: 139px; HEIGHT: 17px"><%=os%></td>
									<td style="WIDTH: 95px; HEIGHT: 17px">Sales</td>
									<td style="WIDTH: 100px; HEIGHT: 17px"><%=sales%></td>
								</tr>
								<tr>
									<td style="WIDTH: 110px; HEIGHT: 11px">Purchase</td>
									<td style="WIDTH: 139px; HEIGHT: 11px"><%=rect%></td>
									<td style="WIDTH: 95px; HEIGHT: 11px">Closing Stock</td>
									<td style="HEIGHT: 11px"><%=cs%></td>
								</tr>
								<tr>
									<td colspan="4" valign="middle"><hr size="1" color="#CE4848">
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 110px; HEIGHT: 11px">Total Sales</td>
									<td style="WIDTH: 139px; HEIGHT: 11px" align="right"><%=sales%></td>
								</tr>
								<tr>
									<td style="WIDTH: 110px; HEIGHT: 10px">Sales IBP</td>
									<td style="WIDTH: 139px; HEIGHT: 10px" align="right"><%=ibp1%></td>
								</tr>
								<tr>
									<td style="WIDTH: 110px; HEIGHT: 5px">Purchase Eicher</td>
									<td style="WIDTH: 139px; HEIGHT: 5px" align="right"><%=eicher1%></td>
								</tr>
								<tr>
									<td style="WIDTH: 110px; HEIGHT: 10px">Purchase Force</td>
									<td style="WIDTH: 139px; HEIGHT: 10px" align="right"><%=force1%></td>
								</tr>
								<tr>
									<td colspan="4" valign="middle"><hr size="1" color="#CE4848">
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 110px"><font color="#990033">Grand Total</font></td>
									<td style="WIDTH: 139px" align="right"><font color="#990033"><%=grandtotal%></font></td>
								</tr>
							</TBODY>
						</table>
					</td>
				</tr>
				<!--***************************************************-->
				<tr>
					<td align="center"><asp:datagrid id="grdLeg" runat="server" CellPadding="1" BackColor="#DEBA84" BorderWidth="0px"
							BorderStyle="None" BorderColor="#DEBA84" CellSpacing="1" Height="110px" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Product Name">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Product Name</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Location</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#IsTank(DataBinder.Eval(Container.DataItem,"location").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Opening Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Opening Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="right" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Receipt</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Sales</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Closing Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				<!--******************************************************************-->
				<tr>
					<td><asp:datagrid id="Datagrid1" runat="server" CellPadding="1" BackColor="#DEBA84" BorderWidth="0px"
							BorderStyle="None" BorderColor="#DEBA84" CellSpacing="1" Height="108px" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Product Name">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Product Name</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Location</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#IsTank(DataBinder.Eval(Container.DataItem,"location").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Opening Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Opening Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="right" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply1(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Receipt</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply1(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Sales</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply1(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Closing Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply1(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<!--******************************************************************-->
				<tr>
					<td><asp:datagrid id="Datagrid2" runat="server" CellPadding="1" BackColor="#DEBA84" BorderWidth="0px"
							BorderStyle="None" BorderColor="#DEBA84" CellSpacing="1" Height="8px" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Product Name">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Product Name</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Location</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#IsTank(DataBinder.Eval(Container.DataItem,"location").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Opening Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Opening Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="right" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply2(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Receipt</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply2(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Sales</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply2(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Closing Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply2(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<!--******************************************************************-->
				<tr>
					<td><asp:datagrid id="Datagrid3" runat="server" CellPadding="1" BackColor="#DEBA84" BorderWidth="0px"
							BorderStyle="None" BorderColor="#DEBA84" CellSpacing="1" Height="8px" AutoGenerateColumns="False">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Product Name">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Product Name</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%"><font color="#ffffff">Location</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<%#IsTank(DataBinder.Eval(Container.DataItem,"location").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Opening Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Opening Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="right" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply3(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Receipt</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply3(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Sales</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply3(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Receiving Stock">
									<HeaderStyle ForeColor="White"></HeaderStyle>
									<HeaderTemplate>
										<TABLE width="100%" align="center">
											<TR>
												<TD align="center" width="100%" colSpan="2"><font color="#ffffff">Closing Stock</font></TD>
											</TR>
											<TR>
												<TD align="left" width="100%"><font color="#ffffff">Pkg</font></TD>
												<TD align="left" width="100%"><font color="#ffffff">Lt./Kg</font></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply3(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<!--******************************************************************-->
				<tr>
					<td align="right"><A href="javascript:window.print()"></A></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
