<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.MechanicReport" CodeFile="MechanicReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Mechanic Report</title><!--
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table width="778" height="290px" align="center" border=0>
				<tr>
					<th valign=top height=20>
						<font color="#CE4848">Mechanic Report</font>
						<hr>
					</th>
				<tr>
					<td align="center" valign="top" height=20>
						<asp:Button id="btnview" runat="server" Text="View" 
							Width="60px"  onclick="btnview_Click"></asp:Button>&nbsp;
						<asp:Button id="btnprint" runat="server" Text="Print" 
							Width="60px"  onclick="btnprint_Click"></asp:Button>&nbsp;
						<asp:Button id="btnExcel" runat="server"  Width="60px" 
							 Text="Excel" onclick="btnExcel_Click"></asp:Button></td>
				</tr>
				</tr>
				<tr>
					<td>
						<table align="center" width="600">
							<tr>
								<td>
								</td>
							</tr>
							<tr>
								<td height="100"><asp:DataGrid id="DataGrid1" runat="server" Width="592px" AutoGenerateColumns="False" BorderColor="#DEBA84"
										BorderStyle="None" BorderWidth="0px" BackColor="#DEBA84" CellPadding="1" CellSpacing="1" OnSortCommand="sortcommand_click"
										AllowSorting="True">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle Height="2px" ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="2px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="r2" SortExpression="r2" HeaderText="Mechanic Code"></asp:BoundColumn>
											<asp:BoundColumn DataField="r3" SortExpression="r3" HeaderText="Name "></asp:BoundColumn>
											<asp:BoundColumn DataField="r4" SortExpression="r4" HeaderText="Type"></asp:BoundColumn>
											<asp:BoundColumn DataField="r6" SortExpression="r6" HeaderText="Customer "></asp:BoundColumn>
											<asp:BoundColumn DataField="r7" SortExpression="r7" HeaderText="Customer Type"></asp:BoundColumn>
											<asp:BoundColumn DataField="r1" SortExpression="r1" HeaderText="District"></asp:BoundColumn>
											<asp:BoundColumn DataField="r5" SortExpression="r5" HeaderText="Place"></asp:BoundColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:DataGrid>
								</td>
							</tr>
							<tr>
								<td>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
