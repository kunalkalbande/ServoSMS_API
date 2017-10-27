<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.ProductWiseSales" CodeFile="ProductWiseSales.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Product Wise Sales Reportl</title> <!--
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
		<script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() 
{
}
	function CheckSearchOption(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			if(index == 1)
				f.texthiddenprod.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempProdWithPack.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempProduct.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempPack.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempProductGroup.value;
			else if(index == 7)                                       
				f.texthiddenprod.value=f.tempSsrName.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
			
		if(index == 3)
		{
			document.Form1.DropValue.value="";
		}
		else if(index == 6)
		{
			document.Form1.DropValue.value="";
		}
		else
		{
		document.Form1.DropValue.value="All";
		}
		document.Form1.DropProdName.style.visibility="hidden"
		//document.Form1.DropPartyName.style.visibility="hidden"
		//document.Form1.DropPartyType.style.visibility="hidden"
	}

//-->


	function CheckSearchOption1(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			if(index == 1)
				f.texthiddenprod1.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod1.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod1.value=f.tempProdWithPack.value;
			else if(index == 4)
				f.texthiddenprod1.value=f.tempProduct.value;
			else if(index == 5)
				f.texthiddenprod1.value=f.tempPack.value;
			else if(index == 6)
				f.texthiddenprod1.value=f.tempProductGroup.value;
			else if(index == 7)                                       
				f.texthiddenprod1.value=f.tempSsrName.value;
					
			f.texthiddenprod1.value=f.texthiddenprod1.value.substring(0,f.texthiddenprod1.value.length-1)
		}
		else
			f.texthiddenprod1.value="";
		
		if(index == 5)
		{
			document.Form1.DropValue1.value="";
		}
		else if(index == 7)
		{
			document.Form1.DropValue1.value="";
		}
		else if(index == 3)
		{
			document.Form1.DropValue1.value="";
		}
		else if(index == 6)
		{
			document.Form1.DropValue1.value="";
		}
		else
		{
			document.Form1.DropValue1.value="All";
		}
		document.Form1.DropProdName1.style.visibility="hidden"
	}
	
		function getChild(t,t2)
		{
			//onclick="getChild(this,document.Form1.DropType,document.Form1.lblschid)
			if(t.selectedIndex==5)
			{
				childWin=window.open("SpecificPacks.aspx?chk="+t.name+":"+t2.value+":5", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=205,height=626");
			}
			else if(t.selectedIndex==7)
			{
				childWin=window.open("SpecificPacks.aspx?chk="+t.name+":"+t2.value+":7", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=225,height=326");
			}
			else if(t.selectedIndex==3)
			{
				childWin=window.open("SpecificPacks.aspx?chk="+t.name+":"+t2.value+":3", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=320,height=700");
			}
			else if(t.selectedIndex==6)
			{
				childWin=window.open("SpecificPacks.aspx?chk="+t.name+":"+t2.value+":6", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=225,height=700");
			}
			
		}
		
		function getChild1(t,t2)
		{
			//onclick="getChild(this,document.Form1.DropType,document.Form1.lblschid)
			if(t.selectedIndex==5)
			{
				childWin=window.open("SpecificPacks1.aspx?chk="+t.name+":"+t2.value+":5", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=205,height=626");
			}
			else if(t.selectedIndex==7)
			{
				childWin=window.open("SpecificPacks1.aspx?chk="+t.name+":"+t2.value+":7", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=225,height=326");
			}
			else if(t.selectedIndex==3)
			{
				childWin=window.open("SpecificPacks1.aspx?chk="+t.name+":"+t2.value+":3", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=320,height=700");
			}
			else if(t.selectedIndex==6)
			{
				childWin=window.open("SpecificPacks1.aspx?chk="+t.name+":"+t2.value+":6", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=225,height=700");
			}
		}
		
		</script>
	    <style type="text/css">
            .auto-style2 {
                height: 53px;
            }
            .auto-style3 {
                width: 111px;
            }
        </style>
	</HEAD>
	<body language="javascript" onkeydown="change(event)" onload="return window_onload()">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox><input id="tempProduct" style="WIDTH: 1px" type="hidden" name="tempProduct" runat="server">
			<input id="tempSsrName" style="WIDTH: 1px" type="hidden" name="tempSsrName" runat="server">
			<input id="tempPartyName" style="WIDTH: 1px" type="hidden" name="tempPartyName" runat="server">
			<input id="tempPartyType" style="WIDTH: 1px" type="hidden" name="tempPartyType" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempProdWithPack" runat="server">
			<input id="tempProductGroup" style="WIDTH: 1px" type="hidden" name="tempProductGroup" runat="server">
			<input id="tempPack" style="WIDTH: 1px" type="hidden" name="tempPack" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server"><INPUT id="texthiddenpartytype" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenpartytype" runat="server"><INPUT id="texthiddenpartyname" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenpartyname" runat="server"><INPUT id="texthiddenprod1" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod1" runat="server">
			<table height="290" width="778" align="center">
				<TBODY>
					<TR>
						<TH vAlign="top" height="20">
							<font color="#ce4848">Product Wise Sales</font>
							<hr>
						</TH>
					</TR>
					<TR>
						<TD vAlign="top" align="center" colSpan="1" height="20" rowSpan="1">
							<TABLE id="Table1" cellSpacing="1" cellPadding="1" border="0">
								<tr>
									<td align="right" colSpan="8"><asp:checkbox id="chkChild" onclick="getChild(this,document.Form1.RadAmount)" Visible="False"
											Runat="server" Text="Specific Packs Sale"></asp:checkbox></td>
								</tr>
								<TR>
									<TD >From&nbsp;</TD>
									<TD class="auto-style3"  ><asp:textbox id="txtDateFrom" runat="server" Width="60px" CssClass="fontstyle" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"></A></TD>
									<td align="center" class="auto-style2">To</td>
									<TD align="left" colSpan="5" ><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><asp:textbox id="txtDateTo" runat="server" Width="60px" CssClass="fontstyle" BorderStyle="Groove"
												ReadOnly="True"></asp:textbox></A><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absBottom" border="0"></A><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"></A><asp:radiobutton id="RadAmount" Runat="server" Text="Sales With Amount" GroupName="Amount"></asp:radiobutton>&nbsp;&nbsp;
										<asp:radiobutton id="RadNonAmount" Runat="server" Text="Sales" GroupName="Amount" Checked="True"></asp:radiobutton>&nbsp;&nbsp;
										<asp:radiobutton id="RadPurchase" Runat="server" Text="Sales &amp; Purchase" GroupName="Amount"></asp:radiobutton>&nbsp;&nbsp;
										<asp:button id="cmdrpt" runat="server" Width="55px" Text="View  " 
											 onclick="cmdrpt_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="55px" Runat="server" Text="Print  " 
											 onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Width="55px" Runat="server" Text="Excel" 
											 onclick="btnExcel_Click"></asp:button></TD>
								</TR>
								<tr>
									<td>Search By&nbsp;</td>
									<td class="auto-style3" ><asp:dropdownlist id="DropSearchBy" runat="server" CssClass="dropdownlist" onchange="CheckSearchOption(this);getChild(this,document.Form1.RadAmount)">
											<asp:ListItem Value="All">All</asp:ListItem>
											<asp:ListItem Value="Group">Group</asp:ListItem>
											<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
											<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
											<asp:ListItem Value="Product Wise">Product Wise</asp:ListItem>
											<asp:ListItem Value="Pack Wise">Pack Wise</asp:ListItem>
											<asp:ListItem Value="Product Type">Product Type</asp:ListItem>
											<asp:ListItem Value="SSR Name">SSR Name</asp:ListItem>
										</asp:dropdownlist></td>
									<td>&nbsp;Value&nbsp;</td>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
											onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.cmdrpt)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 130px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											readOnly type="text" name="temp"><br>
										<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.cmdrpt)"
												id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.cmdrpt,document.Form1.DropValue)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 220px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
												type="select-one"></select></div>
									</td>
									<td align="center">Search By</td>
									<td><asp:dropdownlist id="DropSelectOption1" Width="100" Runat="server" CssClass="fontstyle" onchange="CheckSearchOption1(this);getChild1(this,document.Form1.RadAmount)">
											<asp:ListItem Value="All">All</asp:ListItem>
											<asp:ListItem Value="Group">Group</asp:ListItem>
											<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
											<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
											<asp:ListItem Value="Product Wise">Product Wise</asp:ListItem>
											<asp:ListItem Value="Pack Wise">Pack Wise</asp:ListItem>
											<asp:ListItem Value="Product Type">Product Type</asp:ListItem>
											<asp:ListItem Value="SSR Name">SSR Name</asp:ListItem>
										</asp:dropdownlist>&nbsp;&nbsp;Option</td>
									<td colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue1"
											onkeyup="search3(this,document.Form1.DropProdName1,document.Form1.texthiddenprod1.value),arrowkeydown(this,event,document.Form1.DropProdName1,document.Form1.texthiddenprod1),Selectbyenter(document.Form1.DropProdName1,event,document.Form1.DropValue1,document.Form1.btnview)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 180px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName1,document.Form1.texthiddenprod1),dropshow(document.Form1.DropProdName1)"
											value="All" name="DropValue1" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName1,document.Form1.texthiddenprod1),dropshow(document.Form1.DropProdName1)"
											readOnly type="text" name="temp1"><br>
										<div id="Layer2" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue1,document.Form1.btnview)"
												id="DropProdName1" ondblclick="select(this,document.Form1.DropValue1)" onkeyup="arrowkeyselect(this,event,document.Form1.btnview,document.Form1.DropValue1)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 200px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue1)" multiple name="DropProdName1"
												type="select-one"></select></div>
									</td>
								</tr>
							</TABLE>
						</TD>
					</TR>
					<tr>
						<td align="center"><asp:datagrid id="grdLegAmount" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								ShowFooter="True" OnSortCommand="sortcommand_click" AllowSorting="True" CellSpacing="1" Height="8px" AutoGenerateColumns="False"
								BorderWidth="0px" CellPadding="1" onselectedindexchanged="grdLeg_SelectedIndexChanged">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="SN">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<%#i++%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Prod_Code" HeaderText="Product Code">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Product Name" FooterText="Total">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Sales" HeaderText="Sales&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"Sales").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"pack_type").ToString()+"X" +DataBinder.Eval(Container.DataItem,"Sales").ToString())%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<FooterTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><b><%=Cache["SalesPKG"].ToString()%></b></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><b><%=Cache["SalesKG"].ToString()%></b></font></TD>
												</TR>
											</TABLE>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="amount" HeaderText="Amount">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%#GenUtil.strNumericFormat(GetCS(DataBinder.Eval(Container.DataItem,"amount").ToString()))%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
										<FooterTemplate>
											<%=GenUtil.strNumericFormat(Cache["CS"].ToString())%>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="grdLegNonAmount" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								ShowFooter="True" OnSortCommand="sortcommand_click" AllowSorting="True" CellSpacing="1" Height="8px" AutoGenerateColumns="False"
								BorderWidth="0px" CellPadding="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="SN">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<%#i++%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Product Name" FooterText="Total">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Sales" HeaderText="Sales&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><%#Check(DataBinder.Eval(Container.DataItem,"Sales").ToString(),DataBinder.Eval(Container.DataItem,"pack_type").ToString())%></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><%#Multiply(DataBinder.Eval(Container.DataItem,"pack_type").ToString()+"X" +DataBinder.Eval(Container.DataItem,"Sales").ToString())%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<FooterTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><b><%=Cache["SalesPKG"].ToString()%></b></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><b><%=Cache["SalesKG"].ToString()%></b></font></TD>
												</TR>
											</TABLE>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
							<!--Grid 3 Sales & Purchase--><asp:datagrid id="GrdSalesPurchase" runat="server" BorderStyle="None" BorderColor="#DEBA84" BackColor="#DEBA84"
								ShowFooter="True" OnSortCommand="sortcommand_click" AllowSorting="True" CellSpacing="1" Height="8px" AutoGenerateColumns="False"
								BorderWidth="0px" CellPadding="1">
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
								<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="SN">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<%#i++%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn SortExpression="prod_Name" HeaderText="Product Name" FooterText="Total">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"prod_Name")%>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
									</asp:TemplateColumn>
									<asp:TemplateColumn SortExpression="Sales" HeaderText="Sales&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><%#Check2(DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><%#Multiply22(DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<FooterTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><b><%=Cache["SalePKG"].ToString()%></b></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><b><%=Cache["SaleKG"].ToString()%></b></font></TD>
												</TR>
											</TABLE>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Purchase&lt;br&gt;Pkg&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;Lt./Kg">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><%#Check1(DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><%#Multiply11(DataBinder.Eval(Container.DataItem,"pack_type").ToString(),DataBinder.Eval(Container.DataItem,"Prod_id").ToString())%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
										<FooterStyle Font-Bold="True"></FooterStyle>
										<FooterTemplate>
											<TABLE align="center" border="0" cellspacing="0">
												<TR>
													<TD align="left" width="75"><font color="#8C4510"><b><%=Cache["PurchasePKG"].ToString()%></b></font></TD>
													<TD align="right" width="75"><font color="#8C4510"><b><%=Cache["PurchaseKG"].ToString()%></b></font></TD>
												</TR>
											</TABLE>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:validationsummary id="ValidationSummary1" runat="server" Height="16px" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
					</tr>
					<tr>
						<td align="right"><A href="javascript:window.print()"></A></td>
					</tr>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
