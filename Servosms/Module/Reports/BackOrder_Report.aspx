<%@ Page language="c#" Inherits="Servosms.Module.Reports.BackOrder_Report" CodeFile="BackOrder_Report.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS : BackOrder Report</title> 
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
				/*
				//Coment by vikas 16.11.2012
				if(index == 1)
					f.texthiddenprod.value=f.tempPlace.value;
				else if(index == 2)
					f.texthiddenprod.value=f.tempDistrict.value;
				//Coment by vikas 01.10.09 else if(index == 3)
				//Coment by vikas 01.10.09	f.texthiddenprod.value=f.tempState.value;
				else if(index == 3)
					f.texthiddenprod.value=f.tempSSR.value;*/
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
				else if(index == 6)
					f.texthiddenprod.value=f.tempBackOrderNo.value;
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
			<input id="tempBackOrderNo" style="WIDTH: 1px" type="hidden" name="tempBackOrderNo" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center" border="0">
				<tr vAlign="top" height="20">
					<th colSpan="5">
						<font color="#ce4848">BackOrder Report</font>
						<hr>
					</th>
				</tr>
				<tr>
					<td valign="top" align="center" colSpan="9">
						<table borderColor="#deba84" cellSpacing="0" cellPadding="6" border="0">
							<tr>
								<td >Date From</td>
								<td><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" BorderStyle="Groove" CssClass="FontStyle"
										Width="60px"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td >Date To</td>
								<td><asp:textbox id="txtDateTo" runat="server" ReadOnly="True" BorderStyle="Groove" CssClass="FontStyle"
										Width="60px"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td ><asp:DropDownList ID="droptype" CssClass="dropdownlist" Runat="server">
										<asp:ListItem Value="Pending">Pending</asp:ListItem>
										<asp:ListItem Value="Complete">Complete</asp:ListItem>
										<asp:ListItem Value="All" Selected="True">All</asp:ListItem>
									</asp:DropDownList></td>
								<td align="center">Search By</td>
								<td><asp:dropdownlist id="DropSearchBy" CssClass="dropdownlist" Runat="server" AutoPostBack="False" onchange="CheckSearchOption(this);" onselectedindexchanged="DropSearchBy_SelectedIndexChanged" Width="100px">
										<asp:ListItem Value="Select">All</asp:ListItem>
										<asp:ListItem Value="Customer Group">Group</asp:ListItem>
										<asp:ListItem Value="Customer SubGroup">SubGroup</asp:ListItem>
										<asp:ListItem Value="City">City</asp:ListItem>
										<asp:ListItem Value="Country">District</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
										<asp:ListItem Value="BackOrder No">BackOrder No</asp:ListItem>
									</asp:dropdownlist></td>
								<td>Value</td>
								<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnView)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 100px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnView)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnView,document.Form1.DropValue)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 145px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
											type="select-one"></select></div>
								</td>
							</tr>
							<tr>
								<td align="right" colSpan="9">&nbsp;&nbsp;<asp:button id="btnView" Width="60" Runat="server" Text="Search" 
										 onclick="btnView_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnPrint" Width="60" Runat="server" Text="Print" 
										 onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="60" Runat="server" Text="Excel"
										 onclick="btnExcel_Click"></asp:button>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<table>
							<tr>
								<td align="center" colSpan="9">
									<table  cellSpacing="0" cellPadding="0" border="1">
										<%
							if(flage==1)
							{
								try
								{
								
									/*********Add by vikas 24.12.12********************************/
									string[] Order_No=DropValue.Value.ToString().Split(new char[] {':'});
									int Count=Order_No.Length;
									/***************************End********************************/
									
									InventoryClass obj=new InventoryClass();
									
									/*Coment by vikas 18.12.2012 if(DropSearchBy.SelectedIndex!=0)
									{
										if(DropSearchBy.SelectedIndex==1)
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"'";
										else if(DropSearchBy.SelectedIndex==2)
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"'";
										else if(DropSearchBy.SelectedIndex==3)
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"'";
										else if(DropSearchBy.SelectedIndex==4)
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"'";
										else if(DropSearchBy.SelectedIndex==5)
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"'";
									}
									else
									{
										sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"'";
									}*/
									
									if(droptype.SelectedIndex==2)
									{
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==2)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==3)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==4)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==5)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==6)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and (bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
										}
										else
										{
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
										}
									}
									else if(droptype.SelectedIndex==1)
									{
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)<=cast(sale_qty as float) and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==2)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)<=cast(sale_qty as float) and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==3)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)<=cast(sale_qty as float) and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==4)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)<=cast(sale_qty as float) and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==5)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)<=cast(sale_qty as float) and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==6)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and (bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
										}
										else
										{
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)<=cast(sale_qty as float) and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
										}
									}
									else if(droptype.SelectedIndex==0)
									{
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and Cust_type in (select customertypename from customertype where group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==2)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==3)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and City='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==4)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and State='"+DropValue.Value.ToString()+"' and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==5)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
											else if(DropSearchBy.SelectedIndex==6)
												sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and (bo_1="+Order_No[1].ToString()+" or bo_2="+Order_No[1].ToString()+" or bo_3="+Order_No[1].ToString()+") and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
										}
										else
										{
											sql="select Order_id,Order_Date,Cust_name,City,cust_type,prod_name,pack_type,item_qty,sale_trans_id,sale_trans_date,sale_qty,o.cust_id,o.item_id from ovd o,products p,customer c where o.cust_id=c.cust_id and o.item_id=p.prod_id and cast(item_qty as float)>cast(sale_qty as float) and cast(floor(cast(Order_Date as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and cast(floor(cast(Order_Date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString())+"' order by Order_id";
										}
									}
									
									rdr=obj.GetRecordSet(sql);
									int i=1;
									string Pack="";
									double ltr_qty=0;
									if(rdr.HasRows)
									{
									
										%>
										<tr>
											<th rowspan="2" valign="top" width="2%" bgColor="#ce4848">
												<font color="#ffffff" size="1">SNo</font></th>
											<th rowspan="2" valign="top" width="4%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Order<br>
													No</font></th>
											<th rowspan="2" valign="top" width="5%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Order<br>
													Date</font></th>
											<th rowspan="2" valign="top" width="20%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Customer Name,&nbsp;Place</font></th>
											<th rowspan="2" valign="top" width="6%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Cust<br>
													Type</font></th>
											<th rowspan="2" valign="top" width="17%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Product Name</font></th>
											<th valign="top" width="9%" bgColor="#ce4848" colSpan="2">
												<font color="#ffffff" size="1">Order Qty</font></th>
											<th rowspan="2" valign="top" width="5%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Invoice</font></th>
											<th rowspan="2" valign="top" width="5%" bgColor="#ce4848">
												<font color="#ffffff" size="1">Invoice</font></th>
											<th valign="top" width="6%" bgColor="#ce4848" colSpan="2">
												<font color="#ffffff" size="1">Sale Qty</font></th>
											<th rowspan="2" valign="top" width="4%" bgColor="#ce4848">
												<font color="#ffffff" size="1">BO<BR>
													No</font></th>
											<th rowspan="2" valign="top" width="4%" bgColor="#ce4848">
												<font color="#ffffff" size="1">BO<BR>
													Date</font></th>
										</tr>
										<tr>
											<th align=left valign="top" bgColor="#ce4848">
												<font color="#ffffff" size="1">Nos</font></th>
											<th align=right valign="top" bgColor="#ce4848">
												<font color="#ffffff" size="1">Ltr</font></th>
											<th align=left valign="top" bgColor="#ce4848">
												<font color="#ffffff" size="1">Nos</font></th>
											<th align=right valign="top" bgColor="#ce4848">
												<font color="#ffffff" size="1">Ltr</font></th>
										</tr>
										<%
											while(rdr.Read())
											{
												Pack=rdr["Pack_Type"].ToString();
												string[] Arr_Pack=Pack.Split(new char[] {'X'});
												ltr_qty=double.Parse(Arr_Pack[0].ToString())*double.Parse(Arr_Pack[1].ToString());
												%>
										<tr>
											<td align="right"><%=i.ToString()%></td>
											<td align="right"><%=rdr["Order_Id"].ToString()%></td>
											<td><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Order_Date"].ToString()))%></td>
											<td><%=rdr["Cust_Name"].ToString()+", "+rdr["City"].ToString()%></td>
											<td><%=rdr["Cust_Type"].ToString()%></td>
											<td><%=rdr["Prod_Name"].ToString()+" : "+rdr["Pack_Type"].ToString()%></td>
											<td align=left><%=rdr["item_Qty"].ToString()%></td>
											<td align="right"><%=Math.Round(ltr_qty*double.Parse(rdr["item_Qty"].ToString()),1)%></td>
											<td><%=rdr["Sale_Trans_id"].ToString()%></td>
											<td><%=GenUtil.str2DDMMYYYY(GenUtil.trimDate(rdr["Sale_Trans_Date"].ToString()))%></td>
											<td align=left><%=rdr["Sale_Qty"].ToString()%></td>
											<td align="right"><%=Math.Round(ltr_qty*double.Parse(rdr["Sale_Qty"].ToString()),1)%></td>
											<td align="right"><%=GetBackOrderNo(rdr["Order_Id"].ToString(),rdr["item_Id"].ToString(),rdr["cust_Id"].ToString())%></td>
											<td align="right"><%=GetBackOrderDate(rdr["Order_Id"].ToString(),rdr["item_Id"].ToString(),rdr["cust_Id"].ToString())%></td>
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
								catch(Exception ex)
								{
									MessageBox.Show(ex.Message.ToString());
								}
							}
								%>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><asp:validationsummary id="vs1" Runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
