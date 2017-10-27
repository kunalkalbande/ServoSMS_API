<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SSRWiseSales" CodeFile="SSRWiseSales.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: SSR Wise Sales Figure</title> 
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
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
		<script language="javascript">

	function CheckSearchOption(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			if(index == 1)
				f.texthiddenprod.value=f.tempCustType.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempPlace.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
		//alert(f.texthiddenprod.value)
	}
		</script>
</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server"><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<TBODY>
					<tr vAlign="top" height="20">
						<th colSpan="5">
							<font color="#ce4848">SSR Wise Sales Figure</font>
							<hr>
						</th>
					</tr>
					<tr vAlign="top">
						<td align="center">
							<table width="85%" align="center" border="0">
								<tr height="20">
									<td align="center">Date From</td>
									<td><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" Width="70px" BorderStyle="Groove"
											CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></td>
									<td>To</td>
									<td><asp:textbox id="txtDateTo" runat="server" ReadOnly="True" Width="70px" BorderStyle="Groove"
											CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></td>
									<td><asp:radiobutton id="RadDetails" Text="Details" GroupName="Sales" Runat="server"></asp:radiobutton><asp:radiobutton id="RadSummarized" Text="Summarized" GroupName="Sales" Runat="server" Checked="True"></asp:radiobutton>&nbsp;&nbsp;<asp:button id="btnview" runat="server" Width="60px" Text="View" 
											onclick="btnview_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnprint" runat="server" Width="60px" Text="Print"
											onclick="btnprint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel"
											onclick="btnExcel_Click"></asp:button></td>
								</tr>
								<tr>
									<td>&nbsp;SSR</td>
									<td colSpan="2"><asp:dropdownlist id="DropSSR" CssClass="fontstyle" Runat="server">
											<asp:ListItem Value="All">All</asp:ListItem>
										</asp:dropdownlist></td>
									<td align="center">Customer</td>
									<td><asp:dropdownlist id="DropCustomer" Width="240px" CssClass="fontstyle" Runat="server">
											<asp:ListItem Value="All">All</asp:ListItem>
										</asp:dropdownlist>&nbsp;&nbsp;Zero Balance&nbsp;&nbsp;<asp:checkbox id="chkZeroBal" CssClass="fontstyle" Runat="server"></asp:checkbox></td>
								</tr>
							</table>
						</td>
					</tr>
					<TR>
						<TD colSpan="5">
							<TABLE cellPadding="0" width="700" align="center" border="0">
								<TR>
									<TD><asp:datagrid id="datagrid11" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
											BackColor="#DEBA84" ShowFooter="True" AllowSorting="True" CellSpacing="1" CellPadding="1"
											BorderWidth="0px" AutoGenerateColumns="False" OnSortCommand="SortCommand_Click">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="SNo">
													<HeaderStyle HorizontalAlign="Center" Width="50px" Height="25"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%=i++%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="m1" SortExpression="m1" HeaderText="Category" FooterText="Total">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m2" SortExpression="m2" HeaderText="Party Name">
													<HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m3" SortExpression="m3" HeaderText="Place">
													<HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="m4" HeaderText="Total lub.">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#SumTotQty(DataBinder.Eval(Container.DataItem,"m4").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["TotQty"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m5" HeaderText="2T Sales">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#SumOil2t(DataBinder.Eval(Container.DataItem,"m5").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Oil2t"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m6" HeaderText="4T Sales">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#SumOil4t(DataBinder.Eval(Container.DataItem,"m6").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Oil4t"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid><asp:datagrid id="GridDetails" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
											BackColor="#DEBA84" ShowFooter="True" AllowSorting="True" CellSpacing="1" CellPadding="1" BorderWidth="0px"
											AutoGenerateColumns="False" OnSortCommand="SortCommand_Click">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="SNo">
													<HeaderStyle HorizontalAlign="Center" Height="25px" Width="40px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
													<ItemTemplate>
														<%=i++%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="m1" SortExpression="m1" HeaderText="Category" FooterText="Total">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle Width="100px"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m2" SortExpression="m2" HeaderText="Party Name">
													<HeaderStyle HorizontalAlign="Center" Width="220px"></HeaderStyle>
													<ItemStyle Width="220px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m3" SortExpression="m3" HeaderText="Place">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle Width="100px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m7" SortExpression="m7" HeaderText="Product Name">
													<HeaderStyle HorizontalAlign="Center" Width="300px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="m4" HeaderText="Total lub.">
													<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
													<ItemTemplate>
														<%#SumTotQty(DataBinder.Eval(Container.DataItem,"m4").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["TotQty"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m5" HeaderText="2T Sales">
													<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
													<ItemTemplate>
														<%#SumOil2t(DataBinder.Eval(Container.DataItem,"m5").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Oil2t"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m6" HeaderText="4T Sales">
													<HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
													<ItemTemplate>
														<%#SumOil4t(DataBinder.Eval(Container.DataItem,"m6").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Oil4t"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid><asp:datagrid id="GridSumZero" runat="server" Width="100%" BorderStyle="None" BorderColor="#DEBA84"
											BackColor="#DEBA84" ShowFooter="True" AllowSorting="True" CellSpacing="1" CellPadding="1" BorderWidth="0px"
											AutoGenerateColumns="False" OnSortCommand="SortCommand_Click">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="SNo">
													<HeaderStyle HorizontalAlign="Center" Width="50px" Height="25"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<%=i++%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="m1" SortExpression="m1" HeaderText="Category" FooterText="Total">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m2" SortExpression="m2" HeaderText="Party Name">
													<HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m3" SortExpression="m3" HeaderText="Place">
													<HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="m3" HeaderText="Total lub.">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#Tot_Lub(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Tot_lub"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m5" HeaderText="2T Sales">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#Tot_2T(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Tot_2t"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m6" HeaderText="4T Sales">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#Tot_4T(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["Tot_4t"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
