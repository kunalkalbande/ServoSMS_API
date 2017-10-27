<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.CustomerWiseOutstanding" CodeFile="CustomerWiseOutstanding.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Outstanding Report</title> <!--
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
		/*coment by vikas 17.11.2012
		if(index == 1)
			f.texthiddenprod.value=f.tempCustName.value;
		else if(index == 2)
			f.texthiddenprod.value=f.tempCustType.value;
		else if(index == 3)
			f.texthiddenprod.value=f.tempDistrict.value;
		else if(index == 4)
			f.texthiddenprod.value=f.tempPlace.value;
		else if(index == 5)
			f.texthiddenprod.value=f.tempSSR.value;*/
			
		if(index == 1)
			f.texthiddenprod.value=f.tempCustName.value;
		else if(index == 2)
			f.texthiddenprod.value=f.tempGroup.value;
		else if(index == 3)
			f.texthiddenprod.value=f.tempSubGroup.value;
		else if(index == 4)
			f.texthiddenprod.value=f.tempDistrict.value;
		else if(index == 5)
			f.texthiddenprod.value=f.tempPlace.value;
		else if(index == 6)
			f.texthiddenprod.value=f.tempSSR.value;
			
			
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
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center">
				<TBODY>
					<TR>
						<TH style="HEIGHT: 10px">
							<font color="#ce4848">Customer Wise Outstanding Report</font>
							<hr>
						</TH>
					</TR>
					<TR>
						<TD align="center" height="20">
							<TABLE>
								<TR>
									<TD align="center">Date From
										<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Date Required" ControlToValidate="txtDateFrom">*</asp:requiredfieldvalidator></TD>
									<TD><asp:textbox id="txtDateTo" runat="server" Width="80px" BorderStyle="Groove" CssClass="fontstyle"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD align="center" colSpan="1" rowSpan="1">&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD><asp:textbox id="txtDateFrom" runat="server" Width="80px" BorderStyle="Groove" CssClass="fontstyle"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
									<TD vAlign="middle" align="center">Balance Type&nbsp;&nbsp;&nbsp;</TD>
									<TD vAlign="top"><asp:dropdownlist id="drpOptions" runat="server" CssClass="dropdownlist">
											<asp:ListItem Value="All" Selected="True">All</asp:ListItem>
											<asp:ListItem Value="Total Balance">Closing Balance</asp:ListItem>
											<asp:ListItem Value="Opening Balance">Opening Balance</asp:ListItem>
											<asp:ListItem Value="Transaction">Transaction</asp:ListItem>
										</asp:dropdownlist></TD>
									<td vAlign="top"><asp:checkbox id="chkZeroBal" CssClass="fontstyle" Runat="server" Checked="True" Text="Zero Balance"></asp:checkbox><asp:checkbox id="chkShowCR" CssClass="fontstyle" Runat="server" Text="Show Cr. Balance"></asp:checkbox></td>
								</TR>
								<TR>
									<td>Search By</td>
									<td><asp:dropdownlist id="DropSearchBy" Width="102px" CssClass="fontstyle" Runat="server" onchange="CheckSearchOption(this)" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
											<asp:ListItem Value="All">All</asp:ListItem>
											<asp:ListItem Value="Customer Name">Name</asp:ListItem>
											<asp:ListItem Value="Group">Group</asp:ListItem>
											<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
											<asp:ListItem Value="District">District</asp:ListItem>
											<asp:ListItem Value="Place">Place</asp:ListItem>
											<asp:ListItem Value="SSR">SSR</asp:ListItem>
										</asp:dropdownlist></td>
									<td>Value</td>
									<td colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
											onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.cmdrpt)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 170px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											readOnly type="text" name="temp"><br>
										<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.cmdrpt)"
												id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.cmdrpt,document.Form1.DropValue)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 190px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
												type="select-one"></select></div>
									</td>
									<td align="right" colSpan="2"><asp:button id="cmdrpt" runat="server" Width="74px" 
											 Text="View " onclick="cmdrpt_Click"></asp:button>&nbsp;&nbsp;<asp:button id="BtnPrint" Width="74px" Runat="server" 
											 Text="Print" onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="74px" Runat="server" 
											 Text="Excel" onclick="btnExcel_Click"></asp:button></td>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center">
							<%
	                    if(IsPostBack)
	
	             {
			     if(Session["Btype"].ToString()=="Opening Balance") 
			     {
			       %>
							<asp:datagrid id="grdLeg" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								OnSortCommand="sortcommand_click" AllowSorting="True" CellPadding="1" BorderWidth="0px" CellSpacing="1"
								Height="8px" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="ItemTotal" onselectedindexchanged="grdLeg_SelectedIndexChanged">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn SortExpression="cust_Name" HeaderText="Customer Name" FooterText="Total:">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City" HeaderText="Place">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"City")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="cust_Type" HeaderText="Customer Category">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Type")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Op_Balance" HeaderText="Opening Balance&lt;br&gt;Debit &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Credit">
										<ItemTemplate>
											<TABLE border="0" align="center">
												<TR>
													<TD align="left" width="60"><font color="#8C4510"><%#CheckDebit(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%></font></TD>
													<TD align="right" width="60"><font color="#8C4510"><%#CheckCredit(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<%}
 else if(Session["Btype"].ToString()=="Total Balance") 
 {
 %>
							<asp:datagrid id="Datagrid1" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								OnSortCommand="sortcommand_click" AllowSorting="True" CellPadding="1" BorderWidth="0px" CellSpacing="1"
								Height="8px" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="ItemTotalclosing">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn SortExpression="cust_Name" HeaderText="Customer Name" FooterText="Total:">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City" HeaderText="Place">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"City")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="cust_Type" HeaderText="Customer Category">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Type")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="balance" HeaderText="Closing Balance">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<!--<%#Limit(DataBinder.Eval(Container.DataItem,"balance").ToString())%>
											<%#DataBinder.Eval(Container.DataItem,"balancetype")%>-->
											<%#CheckClosing(DataBinder.Eval(Container.DataItem,"balance").ToString(),DataBinder.Eval(Container.DataItem,"balancetype").ToString())%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<%}
						
					     else if(Session["Btype"].ToString()=="Transaction") 
			     
			     
			     
			     {
			     
			     
			     
			     
			     %>
							<asp:datagrid id="Datagrid2" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								OnSortCommand="sortcommand_click" AllowSorting="True" CellPadding="1" BorderWidth="0px" CellSpacing="1"
								Height="8px" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="ItemTotaltr" onselectedindexchanged="Datagrid2_SelectedIndexChanged">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn SortExpression="cust_Name" HeaderText="Customer Name" FooterText="Total">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City" HeaderText="Place">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"City")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="cust_Type" HeaderText="Customer Category">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Type")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="debitamount" HeaderText="Transaction&lt;br&gt;Debit &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Credit">
										<ItemTemplate>
											<table align="center" border="0">
												<tr>
													<td align="left" width="60"><font color="#8C4510"><%#Limit1(DataBinder.Eval(Container.DataItem,"debitamount").ToString())%></font></td>
													<td align="right" width="60"><font color="#8C4510"><%#Limit2(DataBinder.Eval(Container.DataItem,"CreditAmount").ToString())%></font></td>
												</tr>
											</table>
										</ItemTemplate>
										<FooterTemplate>
											<table>
												<tr>
													<td align="left" width="60"><font color="maroon"><b><%=Cache["Transdr"].ToString()%></b></font></td>
													<td align="right" width="60"><font color="maroon"><b><%=Cache["Transcr"].ToString()%></b></font></td>
												</tr>
											</table>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<%}
						 else if(Session["Btype"].ToString()=="All")
			     
			     
			     
			     
			     {
			    ;
			     
			     
			     
			     %>
							<asp:datagrid id="Datagrid3" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								OnSortCommand="sortcommand_click" AllowSorting="True" CellPadding="1" BorderWidth="0px" CellSpacing="1"
								Height="8px" AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="ItemTotal1" PageSize="5">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn SortExpression="cust_Name" HeaderText="Customer Name" FooterText="Total">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="City" HeaderText="Place">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"City")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="cust_Type" HeaderText="Customer Category">
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"cust_Type")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Op_Balance" HeaderText="Opening Balance&lt;br&gt;Debit &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Credit">
										<ItemTemplate>
											<TABLE border="0" align="center">
												<TR>
													<TD align="left" width="60"><font color="#8C4510"><%#CheckDebit(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%></font></TD>
													<TD align="right" width="60"><font color="#8C4510"><%#CheckCredit(DataBinder.Eval(Container.DataItem,"cust_id").ToString())%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<FooterTemplate>
											<table>
												<tr>
													<td align="left" width="60"><font color="maroon"><b><%=Cache["Opndr"].ToString()%></b></font></td>
													<td align="right" width="60"><font color="maroon"><b><%=Cache["Opncr"].ToString()%></b></font></td>
												</tr>
											</table>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="debitamount" HeaderText="Transaction&lt;br&gt;Debit &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; Credit">
										<ItemTemplate>
											<table align="center" border="0">
												<tr>
													<td align="left" width="60"><font color="#8C4510"><%#Limit1(DataBinder.Eval(Container.DataItem,"debitamount").ToString())%></font></td>
													<td align="right" width="60"><font color="#8C4510"><%#Limit2(DataBinder.Eval(Container.DataItem,"CreditAmount").ToString())%></font></td>
												</tr>
											</table>
										</ItemTemplate>
										<FooterTemplate>
											<table>
												<tr>
													<td align="left" width="60"><font color="maroon"><b><%=Cache["Transdr"].ToString()%></b></font></td>
													<td align="right" width="60"><font color="maroon"><b><%=Cache["Transcr"].ToString()%></b></font></td>
												</tr>
											</table>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="balance" HeaderText="Closing Balance">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#Limit(DataBinder.Eval(Container.DataItem,"balance").ToString())%>
											<%#DataBinder.Eval(Container.DataItem,"balancetype")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center" ForeColor="#8C4510"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<TR>
						<TD align="right"><A href="javascript:window.print()"></A></TD>
					</TR>
				</TBODY>
			</table>
			<%}}%>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
