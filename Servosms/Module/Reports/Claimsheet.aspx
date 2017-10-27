<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.Claimsheet" CodeFile="Claimsheet.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Secon. Sales Claim Report</title>
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
						<font color="#ce4848">Secon. Sales Claim Report</font>
						<hr>
					</TH>
				</TR>
				<TR vAlign="top">
					<TD align="center" colSpan="1" rowSpan="1">
						<TABLE>
							<TR>
								<td><asp:dropdownlist id="dropType" CssClass="dropdownlist" Runat="server">
										<asp:ListItem Value="Existing">Existing</asp:ListItem>
										<asp:ListItem Value="New">New</asp:ListItem>
									</asp:dropdownlist></td>
								<TD align="center">&nbsp;&nbsp;Date From&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateFrom" runat="server" Width="110px" CssClass="fontstyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD align="center" colSpan="1" rowSpan="1">To&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateTo" runat="server" Width="110px" CssClass="fontstyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD><asp:button id="cmdrpt" runat="server" Width="60px" 
										 Text="View" onclick="cmdrpt_Click"></asp:button>&nbsp;&nbsp;<asp:button id="prnButton" runat="server" Width="60px" 
										 Text=" Print " onclick="prnButton_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="60px" 
										 Text="Excel" onclick="btnExcel_Click"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 214px" align="center"><asp:datagrid id="grdLeg" runat="server" BorderStyle="None" BackColor="#DEBA84" BorderColor="#DEBA84"
							AutoGenerateColumns="False" Height="8px" CellSpacing="1" BorderWidth="0px" CellPadding="1" ShowFooter="True" AllowSorting="True"
							OnSortCommand="sortcommand_click">
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
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["osp"]%></b></font></TD-->
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
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["rectp"]%></b></font></TD-->
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
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcptfoc").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"rcptfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["salesp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["sales"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="Sales&lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ1(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"salesfoc").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fpurp"]%></b></font></TD-->
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
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"salesfoc").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["fsale"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Closing &lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["csp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["fpur"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="SO Discount P/Ltr">
									<ItemTemplate>
										<%#discount(DataBinder.Eval(Container.DataItem,"prod_Name").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="HO Discount P/Ltr">
									<ItemTemplate>
										<%#discount2(DataBinder.Eval(Container.DataItem,"prod_Name").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="SO Total">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#totalfoc_new(Multiplyfoc(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type")),Multiplyfoc(DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type")),discount(DataBinder.Eval(Container.DataItem,"prod_Name").ToString()),DataBinder.Eval(Container.DataItem,"prod_id").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Cache["foctotal"]%>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="HO Total">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#totalfoc_new2(Multiplyfoc(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type")),Multiplyfoc(DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type")),discount2(DataBinder.Eval(Container.DataItem,"prod_Name").ToString()),DataBinder.Eval(Container.DataItem,"prod_id").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									<FooterTemplate>
										<%=Cache["foctotal2"]%>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:datagrid id="grdnewdata" runat="server" Width="900px" BorderStyle="None" BackColor="#DEBA84"
							BorderColor="#DEBA84" AutoGenerateColumns="False" Height="8px" CellSpacing="1" BorderWidth="0px" CellPadding="1"
							ShowFooter="True" AllowSorting="True" OnSortCommand="sortcommand_click">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code" FooterText="Total">
									<HeaderStyle Width="5%"></HeaderStyle>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Product Name">
									<HeaderStyle ForeColor="White" Width="25%"></HeaderStyle>
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="op" HeaderText="Opening&lt;br&gt; Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"op").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"op").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["osp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["os"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="rcpt" HeaderText="Receipt&lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcpt").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ(DataBinder.Eval(Container.DataItem,"rcpt").ToString()+"X" +DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_ID").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["rectp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["rect"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Receipt&lt;br&gt;(FOC)Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"rcptfoc").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"rcptfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["salesp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["sales"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="Sales&lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"sales").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#MultiplySJ1(DataBinder.Eval(Container.DataItem,"sales").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"),DataBinder.Eval(Container.DataItem,"salesfoc").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fpurp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["cs"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Sales&lt;br&gt;(FOC)Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"salesfoc").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"salesfoc").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["fsale"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="cs" HeaderText="Closing &lt;br&gt;Lt./Kg">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemTemplate>
										<TABLE align="center" border="0" cellspacing="0">
											<TR>
												<!--TD width="40" align="left"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"cs").ToString(),DataBinder.Eval(Container.DataItem,"Category").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD-->
												<TD width="60" align="right"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"cs").ToString()+"X"+DataBinder.Eval(Container.DataItem,"pack_type"))%></font></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["csp"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["fpur"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="RO &lt;br&gt;Sale">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#Sale_RO(DataBinder.Eval(Container.DataItem,"prod_Name").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b>
															<%=Cache["TotRoSale"]%>
														</b></font>
												</TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="RO &lt;br&gt;Disc.">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#discount_RO(DataBinder.Eval(Container.DataItem,"prod_Name").ToString())%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="RO &lt;br&gt;Total">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#totalRo()%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=GrandTotRO.ToString()%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Bazzar&lt;br&gt; Sale">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#Sale_Bazzar(DataBinder.Eval(Container.DataItem,"prod_Name").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=Cache["TotBazzarSale"]%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="sales" HeaderText="Bazzar&lt;br&gt; Disc.">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#discount_Bazzar(DataBinder.Eval(Container.DataItem,"prod_Name").ToString())%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Bazzar&lt;br&gt;Total">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#totalBazz()%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=GrandTotBazzar.ToString()%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Total&lt;br&gt; (RO+Bazzar)">
									<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="White"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%#totalRoBazz()%>
									</ItemTemplate>
									<FooterStyle Font-Bold="True"></FooterStyle>
									<FooterTemplate>
										<TABLE borderColor="#ccffff" width="100%" align="center" cellpadding="0" cellspacing="0">
											<TR>
												<!--TD width="100%"><font color="#8C4510"><b><%=Cache["fsalep"]%></b></font></TD-->
												<TD align="right" width="100%"><font color="#8C4510"><b><%=GTotROBazz.ToString()%></b></font></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></TD>
				</TR>
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
