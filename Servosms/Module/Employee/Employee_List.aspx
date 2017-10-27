<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Employee.Employee_List" CodeFile="Employee_List.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Employee List</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			};
		}
		if(document.getElementById("STM0_0__0___")!=null)
			window.onload=change();
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<FORM id="View_Customer" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TR>
					<TH style="HEIGHT: 3px" align="center">
						<font color="#ce4848">Employee List</font>
						<HR>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">
						<TABLE cellSpacing="0" cellPadding="0">
							<TR>
								<TD>Employee&nbsp;ID&nbsp;&nbsp;</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtEmpID" runat="server"
										BorderStyle="Groove"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>Name</TD>
								<TD><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtName" runat="server" BorderStyle="Groove"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>Designation</TD>
								<TD><asp:textbox onkeypress="return GetOnlyChars(this, event);" id="txtDesig" runat="server" BorderStyle="Groove"></asp:textbox></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;<asp:linkbutton id="btnSearch" runat="server" Width="80px" onclick="btnSearch_Click">Search</asp:linkbutton></TD>
							</TR>
						</TABLE>
						<asp:datagrid id="GridSearch" BorderStyle="None" OnSortCommand="sortcommand_click" AllowSorting="True"
							CellSpacing="1" PageSize="20" AllowPaging="True" Runat="server" HeaderStyle-BackColor="#ff99ff"
							AutoGenerateColumns="False" BorderColor="#DEBA84" BorderWidth="0px" BackColor="#DEBA84" CellPadding="1"
							Font-Size="Smaller" Font-Names="Arial" onselectedindexchanged="GridSearch_SelectedIndexChanged">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Ledger_ID" SortExpression="Ledger_ID" HeaderText="Ledger ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="Emp_ID" SortExpression="Emp_ID" ReadOnly="True" HeaderText="Employee ID">
									<HeaderStyle Width="80pt"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Emp_Name" SortExpression="Emp_Name" ReadOnly="True" HeaderText="Name">
									<HeaderStyle Width="200px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Designation" SortExpression="Designation" ReadOnly="True" HeaderText="Designation">
									<HeaderStyle Width="150px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:HyperLinkColumn Text="Edit" DataNavigateUrlField="Emp_ID" DataNavigateUrlFormatString="Employee_Update.aspx?ID={0}"
									HeaderText="Edit">
									<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:HyperLinkColumn>
								<asp:ButtonColumn Text="Delete" HeaderText="Delete" CommandName="Delete"></asp:ButtonColumn>
							</Columns>
							<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Center" ForeColor="#8C4510"
								Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></FORM>
	</body>
</HTML>
