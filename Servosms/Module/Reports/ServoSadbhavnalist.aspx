<%@ Page language="c#" Inherits="Servosms.Module.Reports.ServoSadbhavnalist" CodeFile="ServoSadbhavnalist.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSadbhavnalist</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<table height="290" width="778" align="center">
				<tr height="20">
					<th>
						<FONT color="#ce4848">Servo Sadbhavna Enrollment List</FONT><br>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td align="center"><asp:dropdownlist id="droptype" runat="server" CssClass="fontstyle">
							<asp:ListItem Value="All">All</asp:ListItem>
							<asp:ListItem Value="Bazzar">Bazzar</asp:ListItem>
							<asp:ListItem Value="N-KSK">N-KSK</asp:ListItem>
							<asp:ListItem Value="KSK">KSK</asp:ListItem>
							<asp:ListItem Value="Essar ro">Essar ro</asp:ListItem>
						</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnview" Text="View" Runat="server" Width="60px" 
							 onclick="btnview_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnPrint" runat="server" Text="Print" Width="60px" 
							 onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="btnExcel" runat="server" Text="Excel" Width="60px" 
							 onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr>
					<td>
						<table width="486" align="center">
							<tr>
								<td><asp:panel id="Panel1" runat="server" Visible="False" ><STRONG>District : 
											Ashok Nagar</STRONG>
										<asp:DataGrid id="DataGrid1" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickAshok" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel2" runat="server" Visible="False" ><STRONG>District : 
											Bhind</STRONG>
										<asp:DataGrid id="Datagrid2" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickBhind" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel3" runat="server" Visible="False" ><STRONG>District : 
											Datia </STRONG>
										<asp:DataGrid id="Datagrid3" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickDatia" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel4" runat="server" Visible="False" ><STRONG>District : 
											Guna </STRONG>
										<asp:DataGrid id="Datagrid4" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickGuna" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle  ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel5" runat="server" Visible="False" ><STRONG>District : 
											Gwalior </STRONG>
										<asp:DataGrid id="Datagrid5" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickGwalior" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel6" runat="server" Visible="False" ><STRONG>District : 
											Morena </STRONG>
										<asp:DataGrid id="Datagrid6" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickMorena" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel7" runat="server" Visible="False" ><STRONG>District : 
											Sheopur</STRONG>
										<asp:DataGrid id="Datagrid7" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickSheopur" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
							<tr>
								<td><asp:panel id="Panel8" runat="server" Visible="False" ><STRONG>District : 
											Shivpuri </STRONG>
										<asp:DataGrid id="Datagrid8" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" Width="478px"
											Visible="False" OnSortCommand="sortcommand_clickShivpuri" AllowSorting="True" AutoGenerateColumns="False"
											BorderStyle="None" CellPadding="1" BorderWidth="0px" CellSpacing="1">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
											<Columns>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r2" SortExpression="r2" HeaderText="Firm Name"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r3" SortExpression="r3" HeaderText="Place"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r4" SortExpression="r4" HeaderText="Category"></asp:BoundColumn>
												<asp:BoundColumn HeaderStyle-Font-Bold="true" DataField="r1" HeaderText="SadbhavnaCode"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
									</asp:panel></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
