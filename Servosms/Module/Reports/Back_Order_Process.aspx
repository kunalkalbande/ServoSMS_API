<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.Back_Order_Process" CodeFile="Back_Order_Process.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS : Back Order Process</title> 
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
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
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
					f.texthiddenprod.value=f.tempGroup.value;
				else if(index == 2)
					f.texthiddenprod.value=f.tempSubGroup.value;
				else if(index == 3)
					f.texthiddenprod.value=f.tempPlace.value;
				else if(index == 4)
					f.texthiddenprod.value=f.tempDistrict.value;
				else if(index == 5)
					f.texthiddenprod.value=f.tempSSR.value;
				f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
			}
			else
				f.texthiddenprod.value="";
			document.Form1.DropValue.value="All";
			document.Form1.DropProdName.style.visibility="hidden";
			//alert(f.texthiddenprod.value)
		}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempPeriod" style="WIDTH: 1px" type="hidden" name="tempPeriod" runat="server">
			<input id="tempState" style="WIDTH: 1px" type="hidden" name="tempState" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<tr vAlign="top" height="20">
					<th colSpan="5">
						<font color="#ce4848">Back Order Process</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<table width="100%">
							<tr>
								<td>&nbsp;Date From&nbsp;</td>
								<td><asp:textbox id="txtDateFrom" runat="server" Width="65px" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>&nbsp;Date To&nbsp;</td>
								<td><asp:textbox id="txtDateTo" runat="server" Width="65px" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>Search By&nbsp;</td>
								<td><asp:dropdownlist id="DropSearchBy" CssClass="dropdownlist" onchange="CheckSearchOption(this);" AutoPostBack="False"
										Runat="server" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
										<asp:ListItem Value="Select">All</asp:ListItem>
										<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
										<asp:ListItem Value="Customer SubGroup">Customer SubGroup</asp:ListItem>
										<asp:ListItem Value="City">City</asp:ListItem>
										<asp:ListItem Value="Country">District</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
									</asp:dropdownlist></td>
								<td>&nbsp;&nbsp;Value&nbsp;</td>
								<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnView)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 125px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnView)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnView,document.Form1.DropValue)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 145px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
											type="select-one"></select></div>
								</td>
							</tr>
							<tr>
								<td align="right" colSpan="9">&nbsp;&nbsp;<asp:button id="btnView" Width="60" Runat="server" 
										 Text="Search" onclick="btnView_Click"></asp:button><asp:button id="btnPrint" Width="60" Runat="server" 
										 Text="Print" Visible="False" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="60" Runat="server" 
										 Text="Excel" Visible="False" onclick="btnExcel_Click"></asp:button>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="9">
									<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="100%" border="1">
										<%
							if(flage==1)
							{
								InventoryClass obj=new InventoryClass();
								//coment by vikas string sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and item_qty>sale_qty and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"'";
								string sql="";
								int i=1;
								if(DropSearchBy.SelectedIndex!=0)
								{
									if(DropSearchBy.SelectedIndex==1)
										sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and cast(item_qty as float)>cast(sale_qty as float) and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
									else if(DropSearchBy.SelectedIndex==2)
										sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and cast(item_qty as float)>cast(sale_qty as float) and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
									else if(DropSearchBy.SelectedIndex==3)
										sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and cast(item_qty as float)>cast(sale_qty as float) and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
									else if(DropSearchBy.SelectedIndex==4)
										sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and cast(item_qty as float)>cast(sale_qty as float) and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
									else if(DropSearchBy.SelectedIndex==5)
										sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and cast(item_qty as float)>cast(sale_qty as float) and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
								}
								else
								{
									sql="select order_id,cust_name,Order_Date,prod_name,Pack_type,item_qty,(cast(item_qty as int) -cast(sale_qty as int)) qty from ovd o,customer c, products p where o.cust_id=c.cust_id and prod_id=item_id and cast(item_qty as float)>cast(sale_qty as float) and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' order by Order_id";
								}
								rdr=obj.GetRecordSet(sql);
								
								if(rdr.HasRows)
								{
									%>
										<tr>
											<th bgColor="#ce4848">
												<font color="#ffffff">SNo</font></th>
											<th bgColor="#ce4848">
												<font color="#ffffff">Order No</font></th>
											<th bgColor="#ce4848">
												<font color="#ffffff">Customer Name</font></th>
											<th bgColor="#ce4848">
												<font color="#ffffff">Order Date</font></th>
											<th bgColor="#ce4848">
												<font color="#ffffff">Product</font></th>
											<th bgColor="#ce4848">
												<font color="#ffffff">Order Qty</font></th></tr>
										<%
									while(rdr.Read())
									{
										%>
										<tr>
											<td><%=i.ToString()%></td>
											<td><%=rdr["Order_Id"].ToString()%></td>
											<td><%=rdr["Cust_Name"].ToString()%></td>
											<td><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Order_Date"].ToString()))%></td>
											<td><%=rdr["Prod_Name"].ToString()+":"+rdr["Pack_Type"].ToString()%></td>
											<td><%=rdr["Qty"].ToString()%></td>
										</tr>
										<%
										i++;
									}
									rdr.Close();
								}
								else
								{
									MessageBox.Show(" Data Not Available ");
								}
							}
							%>
									</table>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="8"><asp:button id="btnPrecess" Width="100" Runat="server" 
										 Text="BO Process" onclick="btnPrecess_Click"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><asp:validationsummary id="vs1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
