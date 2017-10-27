<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.CustomerDataMining" CodeFile="CustomerDataMining.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Data Mining</title> <!--
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
				/* coment by vikas 17.11.2012 
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
			<table height="290" width="778" align="center" border="0">
				<TBODY>
					<TR vAlign="top" height="20">
						<TH>
							<font color="#ce4848">Customer Data Mining</font>
							<hr>
						</TH>
					</TR>
					<TR vAlign="top">
						<TD align="center">
							<TABLE>
								<TR>
									<TD>Search By</TD>
									<td><asp:dropdownlist id="DropSearchBy" Width="125px" CssClass="fontstyle" onchange="CheckSearchOption(this)"
											Runat="server">
											<asp:ListItem Value="All">All</asp:ListItem>
											<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
											<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
											<asp:ListItem Value="Customer SubGroup">Customer SubGroup</asp:ListItem>
											<asp:ListItem Value="District">District</asp:ListItem>
											<asp:ListItem Value="Place">Place</asp:ListItem>
											<asp:ListItem Value="SSR">SSR</asp:ListItem>
										</asp:dropdownlist></td>
									<td>Value</td>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
											onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.txtview)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 150px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
											readOnly type="text" name="temp"><br>
										<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.txtview)"
												id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.txtview,document.Form1.DropValue)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 170px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
												type="select-one"></select></div>
									</td>
									<TD vAlign="middle"><asp:checkbox id="chkTesting" Runat="server" Text="Required Info"></asp:checkbox>&nbsp;&nbsp;<asp:button id="txtview" runat="server" Width="60px" Text="View" Height="24" 
											onclick="txtview_Click"></asp:button>&nbsp;<asp:button id="btnPrint" runat="server" Width="60px" Text="Print" Height="24px" 
											onclick="btnPrint_Click"></asp:button>&nbsp;
										<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel" Height="24px" 
											onclick="btnExcel_Click"></asp:button><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"></A></TD>
									<TD></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD align="center"><asp:datagrid id="CustomerGrid" runat="server" Height="4px" BackColor="#DEBA84" BorderColor="#DEBA84"
								OnSortCommand="sortcommand_click" AllowSorting="True" BorderWidth="0px" Font-Size="10pt" Font-Names="Verdana"
								CellSpacing="1" AutoGenerateColumns="False" HorizontalAlign="Center" BorderStyle="None" CellPadding="1">
								<SelectedItemStyle Font-Size="2pt" Font-Bold="True" Height="4px" ForeColor="White" CssClass="DataGridItem"
									BackColor="#738A9C"></SelectedItemStyle>
								<EditItemStyle Height="4px"></EditItemStyle>
								<ItemStyle Font-Size="40pt" Height="4px" ForeColor="#8C4510" CssClass="DataGridItem" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Size="40px" Font-Names="Verdana" Font-Bold="True" Wrap="False" HorizontalAlign="Center"
									Height="2px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:BoundColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" DataField="sadbhavnacd" SortExpression="sadbhavnacd" HeaderText="Unique Code"></asp:BoundColumn>
									<asp:BoundColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" DataField="Tin_No" SortExpression="Tin_No" HeaderText="Tin No"></asp:BoundColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="Cust_name" HeaderText="Customer Name">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>                                           
											<%#DataBinder.Eval(Container.DataItem,"Cust_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="Cust_Type" HeaderText="Type">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Cust_Type")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="Address" HeaderText="Address">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Address")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="City" HeaderText="City">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"City")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" HeaderText="Contact Number&lt;br&gt;Office &#160;&#160;&#160;&#160;Residence &#160;&#160;&#160;&#160;Mobile">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<TABLE border="0" align="center">
												<TR>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Tel_Off").ToString()%></font></TD>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Tel_Res").ToString()%></font></TD>
													<TD align="Right" width="60"><font color="#8C4510"><%#DataBinder.Eval(Container.DataItem,"Mobile").ToString()%></font></TD>
												</TR>
											</TABLE>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" HeaderText="Contact Person">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"ContactPerson")%>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid><asp:datagrid id="CustomerTestGrid" runat="server" Width="60%" BackColor="#DEBA84" BorderColor="#DEBA84"
								OnSortCommand="sortcommand_click" AllowSorting="True" BorderWidth="0px" Font-Size="10pt" Font-Names="Verdana"
								CellSpacing="1" AutoGenerateColumns="False" HorizontalAlign="Center" BorderStyle="None" CellPadding="1">
								<SelectedItemStyle Font-Size="2pt" Font-Bold="True" Height="4px" ForeColor="White" CssClass="DataGridItem"
									BackColor="#738A9C"></SelectedItemStyle>
								<EditItemStyle Height="4px"></EditItemStyle>
								<ItemStyle Font-Size="10pt" Height="4px" ForeColor="#8C4510" CssClass="DataGridItem" BackColor="#FFF7E7"></ItemStyle>
								<HeaderStyle Font-Size="2px" Font-Names="Verdana" Font-Bold="True" Wrap="False" HorizontalAlign="Center"
									Height="20px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
								<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="Cust_name" HeaderText="Customer Name">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Cust_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="City" HeaderText="City">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"City")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderStyle-Font-Size="Small" HeaderStyle-Font-Bold="true" SortExpression="Cust_Type" HeaderText="Type">
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem,"Cust_Type")%>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
					<!--TR>
						<td align="left"><FONT color="#ff0033"><STRONG><U>Note</U>:</STRONG>&nbsp;</FONT><FONT color="black">
								To take a printout press the above Print button, to redirect the output to a 
								new page. Use the Page Setup option in the File menu to set the appropriate
								<br>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; margins, 
								then use the Print option in the file menu to send the output to the printer. </FONT>
						</td>
					</TR--></TBODY></table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
