<%@ Page language="c#" Inherits="Servosms.Module.Reports.Customer_Bill_Ageing" CodeFile="Customer_Bill_Ageing.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Customer Bill Ageing</title> <!--
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
function check(t1,t)
{
var temp = t.Value;
if(temp.Trim() =="")
{
alert("Please Enter the Interest Rate")
return false;
}
return true;

}
function CheckSearchOption(t)
{
	var index = t.selectedIndex
	var f = document.Form1;
	if(index != 0)
	{
		/*Coment By vikas 17.11.2012
		if(index == 1)
			f.texthiddenprod.value=f.tempCustName.value;
		else if(index == 2)
			f.texthiddenprod.value=f.tempCustType.value;
		else if(index == 3)
			f.texthiddenprod.value=f.tempDistrict.value;
		else if(index == 4)
			f.texthiddenprod.value=f.tempInvoiceNo.value;
		else if(index == 5)
			f.texthiddenprod.value=f.tempPlace.value;
		else if(index == 6)
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
			f.texthiddenprod.value=f.tempInvoiceNo.value;
		else if(index == 6)
			f.texthiddenprod.value=f.tempPlace.value;
		else if(index == 7)
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
	    <style type="text/css">
            .auto-style1 {
                width: 88px;
            }
        </style>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="4px"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempInvoiceNo" style="WIDTH: 1px" type="hidden" name="tempInvoiceNo" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<TR>
					<TH vAlign="top" align="center" height="20">
						<font color="#ce4848">Customer Bill&nbsp;Ageing Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">
						<TABLE  cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TR>
								<td colSpan="6">
									<table align="center">
										<tr>
											<TD>Type&nbsp;<asp:DropDownList ID="DropType" Runat="server" CssClass="fontstyle">
													<asp:ListItem Value="Existing">Existing</asp:ListItem>
													<asp:ListItem Value="New">New</asp:ListItem>
												</asp:DropDownList></TD>
											<TD>Catgory&nbsp;<asp:DropDownList ID="dropCat" Runat="server" CssClass="fontstyle">
													<asp:ListItem Value="All">All</asp:ListItem>
													<asp:ListItem Value="Regular">Regular</asp:ListItem>
													<asp:ListItem Value="Beyond">Beyond</asp:ListItem>
													<asp:ListItem Value="Serius">Serius</asp:ListItem>
												</asp:DropDownList>&nbsp;Date From</TD>
											<TD class="auto-style1">&nbsp;<asp:textbox id="txtDateFrom" runat="server" Width="60px" CssClass="fontstyle" BorderStyle="Groove"
													ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
														align="absMiddle" border="0"></A>&nbsp;</TD>
											
											<TD align="left">Date To&nbsp;<asp:textbox id="Textbox1" runat="server" Width="60px" CssClass="fontstyle" BorderStyle="Groove"
													ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
														align="absMiddle" border="0"></A>&nbsp;</TD>
											<TD>Interest Rate&nbsp;&nbsp;</TD>
											<TD align="right"><asp:textbox id="InterestText" runat="server" Width="110px" CssClass="fontstyle" BorderStyle="Groove" ontextchanged="InterestText_TextChanged"></asp:textbox><asp:checkbox id="c" runat="server" Width="35px" Height="12px"></asp:checkbox>&nbsp;&nbsp;<asp:button id="Update1" runat="server" Width="65px" Text="Update" 
													 onclick="Update1_Click"></asp:button></TD>
										</tr>
										<tr>
											<td>Search By</td>
											<td><asp:dropdownlist id="DropSearchBy" Width="125px" CssClass="fontstyle" onchange="CheckSearchOption(this)"
													Runat="server" onselectedindexchanged="DropSearch_SelectedIndexChanged">
													<asp:ListItem Value="All">All</asp:ListItem>
													<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
													<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
													<asp:ListItem Value="Customer SubGroup">Customer SubGroup</asp:ListItem>
													<asp:ListItem Value="District">District</asp:ListItem>
													<asp:ListItem Value="Invoice No">Invoice No</asp:ListItem>
													<asp:ListItem Value="Place">Place</asp:ListItem>
													<asp:ListItem Value="SSR">SSR</asp:ListItem>
												</asp:dropdownlist></td>
											<td class="auto-style1">Value</td>
											<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
													onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnShow)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 110px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
													value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
													readOnly type="text" name="temp"><br>
												<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnShow)"
														id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnShow,document.Form1.DropValue)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 170px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
														type="select-one"></select></div>
											</td>
											<td align="right" colSpan="2"><asp:button id="btnShow" runat="server" Width="65px" Text="View" 
													 onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;<asp:button id="BtnPrint" Width="65px" Text="Print" 
													 Runat="server" onclick="BtnPrint_Click"></asp:button>
												&nbsp;&nbsp;<asp:button id="btnExcel" Width="65px" Text="Excel" 
													 Runat="server" onclick="btnExcel_Click"></asp:button></td>
										</tr>
									</table>
								</td>
							</TR>
							<TR>
								<TD align="center" colSpan="6"><asp:datagrid id="GridReport" runat="server" Width="100%" BorderStyle="None" Height="100%" BackColor="#DEBA84"
										BorderColor="#DEBA84" AutoGenerateColumns="False" BorderWidth="0px" CellPadding="1" CellSpacing="1" OnSortCommand="sortcommand_click"
										AllowSorting="True" ShowFooter="True">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Customer Name" FooterText="Total">
												<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place"></asp:BoundColumn>
											<asp:BoundColumn DataField="Invoice_No" SortExpression="Invoice_No" HeaderText="Invoice No."></asp:BoundColumn>
											<asp:BoundColumn DataField="Invoice_Date" SortExpression="Invoice_Date" HeaderText="Invoice Date"
												DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:TemplateColumn SortExpression="Net_Amount" HeaderText="Bill Amount">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#SumAmount(DataBinder.Eval(Container.DataItem,"Net_Amount").ToString())%>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=GenUtil.strNumericFormat(Cache["Amount"].ToString())%>
												</FooterTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Cr_Days" SortExpression="Cr_Days" HeaderText="Credit Days">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="due_date" SortExpression="due_date" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
											<asp:BoundColumn DataField="tcr" SortExpression="tcr" HeaderText="Total Due Days">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="tdd" SortExpression="tdd" HeaderText="Total Overdue Days">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
											</asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Amt. with Interest">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
												<ItemTemplate>
													<%#CalcInterest(DataBinder.Eval(Container.DataItem, "Net_Amount").ToString(),DataBinder.Eval(Container.DataItem, "tdd").ToString()) %>
												</ItemTemplate>
												<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												<FooterTemplate>
													<%=Cache["Amt1"]%>
												</FooterTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<%
						if(DropType.SelectedIndex==1)
						{
							InventoryClass obj=new InventoryClass();
							string sql="";
							
							if(dropCat.SelectedIndex==0)
							{
								if(DateTime.Compare(Convert.ToDateTime(GenUtil.str2DDMMYYYY(txtDateFrom.Text)),Convert.ToDateTime(GenUtil.str2DDMMYYYY(Textbox1.Text)))>0)
									sql="select * from vw_Cust_Ageing where cast(floor(cast(invoice_date as float)) as datetime)>='"+(GenUtil.str2MMDDYYYY(txtDateFrom.Text))+"' and cast(floor(cast(invoice_date as float)) as datetime)<='"+(GenUtil.str2MMDDYYYY(Textbox1.Text))+"' order by cust_id,invoice_date ,invoice_no" ;
								else
									sql="select * from vw_Cust_Ageing order by cust_id,invoice_date ,invoice_no" ;
							}
							else if(dropCat.SelectedIndex==1)
							{
								sql="select * from vw_Cust_Ageing where tcr between 1 and 15 order by cust_id,invoice_date ,invoice_no" ;
							}
							else if(dropCat.SelectedIndex==2)
							{
								sql="select * from vw_Cust_Ageing where tcr between 16 and 30 order by cust_id,invoice_date ,invoice_no" ;
							}
							else if(dropCat.SelectedIndex==3)
							{
								sql="select * from vw_Cust_Ageing where tcr >30 order by cust_id,invoice_date ,invoice_no" ;
							}
							dtr=obj.GetRecordSet(sql);
							int i=1;
							if(dtr.HasRows)
							{
								%>
						<table cellpadding="0" cellspacing="0" border="1">
							<tr>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">SNo.</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Customer Name</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Balance</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Bill No</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Bill Amount</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Bill Date</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Day's</font></th>
								<th bgColor="#ce4848">
									<font color="#ffffff" size="1">Catagory</font></th>
							</tr>
							<%
								while(dtr.Read())
								{
									%>
							<tr>
								<td><%=i.ToString()%></td>
								<td><%=dtr.GetValue(1).ToString()%></td>
								<td align="right"><%=GenUtil.strNumericFormat(dtr.GetValue(5).ToString())%></td>
								<td align="right"><%=dtr.GetValue(3).ToString()%></td>
								<td align="right"><%=GenUtil.strNumericFormat(BillAmount(dtr.GetValue(3).ToString()))%></td>
								<td><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(dtr.GetValue(4).ToString())).ToString()%></td>
								<td><%=dtr.GetValue(8).ToString()%></td>
								<%
										int Days=int.Parse(dtr.GetValue(8).ToString());
										String Catgory="";
										if(Days>=1 && Days<=15)
										{
											Catgory="Regular";
										}
										else if(Days>=16 && Days<=30)
										{
											Catgory="Beyond";
										}
										else
										{
											Catgory="Serious";
										}
										%>
								<td><%=Catgory.ToString()%></td>
							</tr>
							<%
									i++;
									//MessageBox.Show(dtr.GetValue(0).ToString()+" : "+dtr.GetValue(1).ToString());
								}
								dtr.Close();
							}
							%>
						</table>
						<%
						}
						%>
						<asp:validationsummary id="vsCustWiseSales" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
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
