<%@ Reference Page="~/Module/Employee/OT_Compensation.aspx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Employee.Salary_Statement" CodeFile="Salary_Statement.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Salary Statement</title> <!--
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="278" width="778" align="center">
				<tr>
					<TH style="HEIGHT: 35px" align="center">
						<font color="#ce4848">Salary Statement</font>
						<hr>
					</TH>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE>
							<TR>
								<TD align="center"><asp:label id="Label1" runat="server">Salary Month</asp:label>&nbsp;&nbsp;&nbsp;
									<FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="DropMonth" ValueToCompare="Select"
										Operator="NotEqual" ErrorMessage="Please Select the Salary Month">*</asp:comparevalidator>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:dropdownlist id="DropMonth" runat="server" Width="116px" CssClass="FontStyle" onselectedindexchanged="DropMonth_SelectedIndexChanged">
										<asp:ListItem Value="Select" Selected="True">Select</asp:ListItem>
										<asp:ListItem Value="January">January</asp:ListItem>
										<asp:ListItem Value="February">February</asp:ListItem>
										<asp:ListItem Value="March">March</asp:ListItem>
										<asp:ListItem Value="April">April</asp:ListItem>
										<asp:ListItem Value="May">May</asp:ListItem>
										<asp:ListItem Value="June">June</asp:ListItem>
										<asp:ListItem Value="July">July</asp:ListItem>
										<asp:ListItem Value="August">August</asp:ListItem>
										<asp:ListItem Value="September">September</asp:ListItem>
										<asp:ListItem Value="October">October</asp:ListItem>
										<asp:ListItem Value="November">November</asp:ListItem>
										<asp:ListItem Value="December">December</asp:ListItem>
									</asp:dropdownlist></TD>
								<td><asp:textbox id="txtYear" runat="server" Width="53px" CssClass="FontStyle" Visible="False" ReadOnly="True"
										BorderStyle="Groove"></asp:textbox><asp:dropdownlist id="dropyear" runat="server" CssClass="FontStyle" onselectedindexchanged="dropyear_SelectedIndexChanged">
										<asp:ListItem Value="2000">2000</asp:ListItem>
										<asp:ListItem Value="2001">2001</asp:ListItem>
										<asp:ListItem Value="2002">2002</asp:ListItem>
										<asp:ListItem Value="2003">2003</asp:ListItem>
										<asp:ListItem Value="2004">2004</asp:ListItem>
										<asp:ListItem Value="2005">2005</asp:ListItem>
										<asp:ListItem Value="2006">2006</asp:ListItem>
										<asp:ListItem Value="2007">2007</asp:ListItem>
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
										<asp:ListItem Value="2011">2011</asp:ListItem>
										<asp:ListItem Value="2012">2012</asp:ListItem>
										<asp:ListItem Value="2013">2013</asp:ListItem>
										<asp:ListItem Value="2014">2014</asp:ListItem>
										<asp:ListItem Value="2015">2015</asp:ListItem>
										<asp:ListItem Value="2016">2016</asp:ListItem>
										<asp:ListItem Value="2017">2017</asp:ListItem>
										<asp:ListItem Value="2018">2018</asp:ListItem>
										<asp:ListItem Value="2019">2019</asp:ListItem>
										<asp:ListItem Value="2020">2020</asp:ListItem>
										<asp:ListItem Value="2021">2021</asp:ListItem>
										<asp:ListItem Value="2022">2022</asp:ListItem>
										<asp:ListItem Value="2023">2023</asp:ListItem>
										<asp:ListItem Value="2024">2024</asp:ListItem>
										<asp:ListItem Value="2025">2025</asp:ListItem>
										<asp:ListItem Value="2026">2026</asp:ListItem>
										<asp:ListItem Value="2027">2027</asp:ListItem>
										<asp:ListItem Value="2028">2028</asp:ListItem>
										<asp:ListItem Value="2029">2029</asp:ListItem>
										<asp:ListItem Value="2030">2030</asp:ListItem>
									</asp:dropdownlist></td>
								<td align="center"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></td>
							</TR>
							<tr>
								<td></td>
								<TD colSpan="3"><asp:button id="btnShow" runat="server" Width="70px" Text="Show" 
										onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnprint" runat="server" Width="70px" Text="Print" 
										onclick="btnprint_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="70px" Text="Excel" 
										onclick="btnExcel_Click"></asp:button></TD>
							</tr>
						</TABLE>
						<asp:datagrid id="GridMachineReport" style="TOP: 50px" runat="server" BorderStyle="None" BorderColor="#DEBA84"
							BackColor="#DEBA84" Height="70px" ShowFooter="True" PageSize="3" CellPadding="1" BorderWidth="0px"
							AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True" OnSortCommand="sortcommand_click" onselectedindexchanged="GridMachineReport_SelectedIndexChanged">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle VerticalAlign="Top" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
								BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn SortExpression="emp_id" DataField="Emp_ID" HeaderText="Emp ID" FooterText="Total:">
									<HeaderStyle Width="50px"></HeaderStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:BoundColumn SortExpression="emp_name" DataField="Emp_Name" HeaderText="Name">
									<HeaderStyle Width="150px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Working Days">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Leave">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterTemplate>
										<%=" "%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Extra Days">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterTemplate>
										<%=" "%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Days - Leave+ Xtra Days Working">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterTemplate>
										<%=" "%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Salary" HeaderText="Monthly Salary">
									<HeaderStyle Width="75px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Net Salary">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Tot_Net_Salary%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Advance">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Tot_Advance%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Incentive">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Tot_Incentive%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Salery-Advance + Incentive">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=G_Tot_Salary%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="TA/DA">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Tot_TADA%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Net Expenditure to Employees">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Tot_Expendeture%>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center" ForeColor="#8C4510"
								Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				<TR>
					<TD vAlign="bottom" align="center"><FONT color="#ff0033"><STRONG><U>Note</U> :</STRONG></FONT>
						If the Salary Calculation based on 30 days, then the attendance should be 
						marked for Sundays also.</TD>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
