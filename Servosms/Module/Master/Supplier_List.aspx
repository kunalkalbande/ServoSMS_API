<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Master.Supplier_List" CodeFile="Supplier_List.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Vendor List</title> <!--
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
		<script id="Validations" language="javascript" src="../../Sysitem/JS/Validations.js"></script>
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
		<FORM id="View_Customer" method="post" runat="server">
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<table width="778" height="290px" align="center">
				<TR>
					<TH align="center" valign="top">
						<font color="#CE4848">Vendor List</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center" valign="top">
						<TABLE cellpadding="0" cellspacing="0">
							<TR>
								<TD>
									Vendor&nbsp;ID&nbsp;</TD>
								<TD><asp:textbox id="txtSuppID" runat="server" BorderStyle="Groove" onkeypress="return GetOnlyNumbers(this, event, false,false);"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>
									Name</TD>
								<TD><asp:textbox id="txtName" runat="server" onkeypress="return GetAnyNumber(this, event);" BorderStyle="Groove"></asp:textbox></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>
									Place</TD>
								<TD><asp:textbox id="txtPlace" runat="server" onkeypress="return GetOnlyChars(this, event);" BorderStyle="Groove"></asp:textbox></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;<asp:linkbutton id="btnSearch" runat="server" onclick="btnSearch_Click">Search</asp:linkbutton></TD>
							</TR>
						</TABLE>
						<asp:datagrid id="GridSearch" AllowPaging="True" AutoGenerateColumns="False" Runat="server" HeaderStyle-BackColor="#ff99ff"
							BorderColor="#DEBA84" BorderStyle="None" BorderWidth="0px" BackColor="#DEBA84" CellPadding="1"
							Font-Size="Smaller" Font-Names="Arial" PageSize="5" CellSpacing="1" AllowSorting="True" OnSortCommand="sortcommand_click" onselectedindexchanged="GridSearch_SelectedIndexChanged">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Ledger_ID" SortExpression="Ledger_ID" HeaderText="Ledger ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="Supp_ID" SortExpression="Supp_ID" ReadOnly="True" HeaderText="Vendor ID">
									<HeaderStyle Width="80px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="Supp_Name" SortExpression="Supp_Name" ReadOnly="True" HeaderText="Name">
									<HeaderStyle Width="250px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="City" SortExpression="City" ReadOnly="True" HeaderText="Place">
									<HeaderStyle Width="150px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:HyperLinkColumn Text="Edit" DataNavigateUrlField="Supp_ID" DataNavigateUrlFormatString="Supplier_Update.aspx?ID={0}"
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
			<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</FORM>
	</body>
</HTML>
