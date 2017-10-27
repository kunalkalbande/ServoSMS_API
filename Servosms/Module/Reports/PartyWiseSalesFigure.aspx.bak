<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.PartyWiseSalesFigure" CodeFile="PartyWiseSalesFigure.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Party Wise Sales Figure</title> 
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
			/*coment by vikas 17.11.2012 
			if(index == 1)
				f.texthiddenprod.value=f.tempCustType.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempPlace.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempSSR.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempDist.value;*/
				
			if(index == 1)
				f.texthiddenprod.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempPlace.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempSSR.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempDist.value;
			else if(index == 7)
				f.texthiddenprod.value=f.tempCustType.value;
				
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
		//alert(f.texthiddenprod.value)
	}
	
	function CheckSearchOption1(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			/*coment by vikas 17.11.2012
			if(index == 1)
				f.texthiddenprod1.value=f.tempCustType.value;
			else if(index == 2)
				f.texthiddenprod1.value=f.tempCustName.value;
			else if(index == 3)
				f.texthiddenprod1.value=f.tempPlace.value;
			else if(index == 4)
				f.texthiddenprod1.value=f.tempSSR.value;
			else if(index == 5)
				f.texthiddenprod1.value=f.tempDist.value;*/
			
			if(index == 1)
				f.texthiddenprod1.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod1.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod1.value=f.tempCustName.value;
			else if(index == 4)
				f.texthiddenprod1.value=f.tempPlace.value;
			else if(index == 5)
				f.texthiddenprod1.value=f.tempSSR.value;
			else if(index == 6)
				f.texthiddenprod1.value=f.tempDist.value;
			else if(index == 7)
				f.texthiddenprod1.value=f.tempCustType.value;
					
			f.texthiddenprod1.value=f.texthiddenprod1.value.substring(0,f.texthiddenprod1.value.length-1)
		}
		else
			f.texthiddenprod1.value="";
		document.Form1.DropValue1.value="All";
		document.Form1.DropProdName1.style.visibility="hidden"
		//alert(f.texthiddenprod.value)
	}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempDist" style="WIDTH: 1px" type="hidden" name="tempDist" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server"> <INPUT id="texthiddenprod1" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod1" runat="server">
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<tr vAlign="top" height="20">
					<th colSpan="5">
						<font color="#ce4848">Party Wise Sales Figure</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td>
						<table width="100%" align="center">
							<tr height="20">
								<td align="center">Date From</td>
								<td><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" Width="110px" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>To</td>
								<td><asp:textbox id="txtDateTo" runat="server" ReadOnly="True" Width="110px" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td colSpan="4"><asp:button id="btnview" runat="server" Width="60px"  Text="View" onclick="btnview_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnprint" runat="server" Width="60px"  Text="Print" onclick="btnprint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="60px" 
										Text="Excel" onclick="btnExcel_Click"></asp:button></td>
							</tr>
							<tr vAlign="top">
								<td align="center">Search By</td>
								<td><asp:dropdownlist id="DropSelectOption" Width="100" CssClass="fontstyle" Runat="server" onchange="CheckSearchOption(this)" onselectedindexchanged="DropSelectOption_SelectedIndexChanged">
										<asp:ListItem Value="All">All</asp:ListItem>
										<asp:ListItem Value="Group">Group</asp:ListItem>
										<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
										<asp:ListItem Value="Name">Name</asp:ListItem>
										<asp:ListItem Value="Place">Place</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
										<asp:ListItem Value="District">District</asp:ListItem>
										<asp:ListItem Value="Customer Type">Customer Type</asp:ListItem>
									</asp:dropdownlist>&nbsp;&nbsp;</td>
                                <td align="center">Option</td>
								<td  colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnview)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 180px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnview)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnview,document.Form1.DropValue)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 200px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
											type="select-one"></select></div>
								</td>
								<td align="center">Search By</td>
								<td><asp:dropdownlist id="DropSelectOption1" Width="100" CssClass="fontstyle" Runat="server" onchange="CheckSearchOption1(this)">
										<asp:ListItem Value="All">All</asp:ListItem>
										<asp:ListItem Value="Group">Group</asp:ListItem>
										<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
										<asp:ListItem Value="Name">Name</asp:ListItem>
										<asp:ListItem Value="Place">Place</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
										<asp:ListItem Value="District">District</asp:ListItem>
										<asp:ListItem Value="Customer Type">Customer Type</asp:ListItem>
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
						</table>
					</td>
				</tr>
				<asp:panel id="panTotalSales" BackColor="#3300ff" Runat="server">
					<TR>
						<TD colSpan="5">
							<TABLE cellPadding="0" width="650" align="center" border="0">
								<TR>
									<TD>
										<asp:datagrid id="datagrid11" runat="server" BorderStyle="None" Width="100%" BackColor="#DEBA84"
											BorderColor="#DEBA84" ShowFooter="True" AllowSorting="True" CellSpacing="1" CellPadding="1"
											BorderWidth="0px" AutoGenerateColumns="False" OnSortCommand="SortCommand_Click">
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="SNo">
													<HeaderStyle Font-Bold="False"></HeaderStyle>
													<ItemTemplate>
														<%=i++%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="m1" SortExpression="m1" HeaderText="Group" FooterText="Total">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="m7" HeaderText="Sub_Grp">
													<HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
													<ItemTemplate>
														<%#GetSSRName(DataBinder.Eval(Container.DataItem,"m7").ToString())%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="m2" SortExpression="m2" HeaderText="Party Name">
													<HeaderStyle HorizontalAlign="Center" Width="280px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="m3" SortExpression="m3" HeaderText="Place">
													<HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn SortExpression="m4" HeaderText="Total Sale">
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
												<asp:TemplateColumn SortExpression="m8" HeaderText="Lube Sale">
													<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<ItemTemplate>
														<%#SumLubeSale(DataBinder.Eval(Container.DataItem,"m8").ToString())%>
													</ItemTemplate>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
													<FooterTemplate>
														<%=GenUtil.strNumericFormat(Cache["LubeSale"].ToString())%>
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn SortExpression="m5" HeaderText="2T Sale">
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
												<asp:TemplateColumn SortExpression="m6" HeaderText="4T Sale">
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
										</asp:datagrid></TD>
								</TR>
								<TR>
									<TD colSpan="5">
										<%
									if(Count==1)
									{
										InventoryClass obj=new InventoryClass();
										InventoryClass obj1=new InventoryClass();
										SqlDataReader SqlDtr=null,rdr=null;
										string str="",str1="",str2="",str3="",str4="",str5="",str6="",str7="",str8="",str9="";
						
										if(DropSelectOption.SelectedIndex==4)
										{
											if(DropValue.Value!="All")
												//12.4.2013 str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') group by c.cust_type order by c.cust_type";
												str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') group by c.cust_type order by c.cust_type";
											else
												//12.4.2013 str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type order by c.cust_type";
												str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type order by c.cust_type";
										}
										else
											//12.4.2013 str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type order by c.cust_type";
											str="select c.cust_type,sum(totqty),sum(oil2t),sum(oil4t),sum(totqty) -(sum(oil2t)+sum(oil4t)) from sales_oil so,sales_master sm,customer c where so.invoice_no=sm.invoice_no and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.cust_type order by c.cust_type";
							
										if(DropSelectOption.SelectedIndex==4)
										{
											if(DropValue.Value!="All")
												//12.4.2013 str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
												str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
											else
												//12.4.2013 str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
												str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
										}
										else
											//12.4.2013 str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
											str8="select distinct sum(v.totalqty) m4,sum(v.oil2t) m5,sum(v.oil4t) m6,sum(v.totalqty) -(sum(v.oil2t)+sum(v.oil4t)) m8 from customer,vw_Salesoil v,sales_master sm where cust_name in(select cust_name from customer where cust_id=sm.cust_id and v.invoice_no=sm.invoice_no) and cast(floor(cast(v.SaleDate as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(v.SaleDate as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
										
										str9="select sum(qty*total_qty) from purchase_details pd,purchase_master pm,products p where pd.invoice_no=pm.invoice_no and p.prod_id=pd.prod_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateFrom.Text)).ToShortDateString()+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+System.Convert.ToDateTime(GenUtil.str2MMDDYYYY(txtDateTo.Text)).ToShortDateString()+"'";
										
										%>
										<TABLE borderColor="#deba84" cellSpacing="0" cellPadding="0" width="100%" border="1">
											<%
											SqlDtr=obj.GetRecordSet(str);
											if(SqlDtr.HasRows)
											{
												while(SqlDtr.Read())
												{
													%>
											<TR bgColor="#ff9900">
												<TD width="50%"><FONT color="#ffffff"><B>&nbsp;Total&nbsp;<%=SqlDtr.GetValue(0).ToString()%>&nbsp;Sales</B></FONT></TD>
												<%
													if(DropSelectOption.SelectedIndex==4)
													{
														if(DropValue.Value!="All")
															rdr = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+SqlDtr.GetValue(0).ToString()+"' and ssr=(select Emp_ID from Employee where Emp_Name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')");
														else
															rdr = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+SqlDtr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')");
													}
													else
														rdr = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where cust_type='"+SqlDtr.GetValue(0).ToString()+"' and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')");
													
													if(rdr.Read())
													{
														%>
												<TD align="right" width="10%"><FONT color="#ffffff"><B><%=rdr.GetValue(0).ToString()%></B></FONT></TD>
												<%
													}
													rdr.Close();
													%>
												<TD align="right" width="10%"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString())%></B></FONT></TD>
												<TD align="right" width="11%"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(4).ToString())%></B></FONT></TD>
												<TD align="right" width="9%"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString())%></B></FONT></TD>
												<TD align="right" width="10%"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString())%></B></FONT></TD>
											</TR>
											<%
											}
										}
										SqlDtr.Close();
										%>
											<TR bgColor="#ce4848">
												<TD><FONT color="#ffffff"><B>&nbsp;Total Secondary Lube Sales</B></FONT></TD>
												<%
												SqlDtr=obj.GetRecordSet(str8);
												if(SqlDtr.Read())
												{
													if(SqlDtr.GetValue(0).ToString()!="")
													{
														if(DropSelectOption.SelectedIndex==4)
														{
															if(DropValue.Value!="All")
																rdr = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')");
															else
																rdr = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')");
														}
														else
															rdr = obj1.GetRecordSet("select count(cust_type) from customer where cust_id in(select sm.cust_id from sales_master sm,customer c where c.cust_id=sm.cust_id and cast(floor(cast(invoice_date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')");
												
													if(rdr.Read())
													{
														%>
												<TD align="right"><FONT color="#ffffff"><B><%=rdr.GetValue(0).ToString()%></B></FONT></TD>
												<%
													}
													rdr.Close();%>
												<TD align="right"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(0).ToString())%></B></FONT></TD>
												<%
												}
												else
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B>0.00</B></FONT></TD>
												<%
												}
												if(SqlDtr.GetValue(3).ToString()!="")
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(3).ToString())%></B></FONT></TD>
												<%
												}
												else
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B>0.00</B></FONT></TD>
												<%
												}
												if(SqlDtr.GetValue(1).ToString()!="")
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(1).ToString())%></B></FONT></TD>
												<%
												}
												else
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B>0.00</B></FONT></TD>
												<%
												}
												if(SqlDtr.GetValue(2).ToString()!="")
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(SqlDtr.GetValue(2).ToString())%></B></FONT></TD>
												<%
												}
												else
												{
													%>
												<TD align="right"><FONT color="#ffffff"><B>0.00</B></FONT></TD>
												<%
												}
											}
											SqlDtr.Close();%>
											</TR>
											<TR bgColor="#ce4848">
												<TD><FONT color="#ffffff"><B>&nbsp;Total Primary Sales</B></FONT></TD>
												<%double Primary=0;
												SqlDtr=obj.GetRecordSet(str9);
												if(SqlDtr.Read()){
												//Primary+=double.Parse(GenUtil.changeqtyltr(SqlDtr.GetValue(1).ToString(),int.Parse(SqlDtr.GetValue(0).ToString())));
												if(SqlDtr.GetValue(0).ToString()!="")
													Primary+=double.Parse(SqlDtr.GetValue(0).ToString());
												}
												SqlDtr.Close();%>
												<TD align="right" colSpan="5"><FONT color="#ffffff"><B><%=GenUtil.strNumericFormat(Primary.ToString())%></B></FONT></TD>
											</TR>
											<%
									}
										%>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</asp:panel></table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
