<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.CreditPeriodAnalysisSheetReport" CodeFile="CreditPeriodAnalysisSheetReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS : Credit Period Analysis Sheet Report</title> 
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
				f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
			}
			else
				f.texthiddenprod.value="";
				
			if(index == 1)
				document.Form1.DropValue.value="";
			else
				document.Form1.DropValue.value="All";
				
			document.Form1.DropProdName.style.visibility="hidden";
			//alert(f.texthiddenprod.value)
		}
		
		function getChild(t)
		{
			if(t.selectedIndex==1)
			{
				childWin=window.open("ChildWinCAS.aspx?chk="+t.name+":0:0", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=yes,width=205,height=300");
			}
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
						<font color="#ce4848">Credit Period Analysis Sheet Report</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<table>
							<tr>
								<td>&nbsp;Date From&nbsp;</td>
								<td><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" BorderStyle="Groove" CssClass="FontStyle"
										Width="65px"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>&nbsp;Date To&nbsp;</td>
								<td><asp:textbox id="txtDateTo" runat="server" ReadOnly="True" BorderStyle="Groove" CssClass="FontStyle"
										Width="65px"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>Search By&nbsp;</td>
								<td><asp:dropdownlist id="DropSearchBy" CssClass="dropdownlist" Runat="server" AutoPostBack="False" onchange="CheckSearchOption(this);getChild(this)" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
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
								<td align="right" colSpan="7">&nbsp;&nbsp;<asp:button id="btnView" Width="60" Runat="server" Text="View" 
										 onclick="btnView_Click"></asp:button><asp:button id="btnPrint" Width="60" Runat="server" Text="Print" 
										 Visible="False" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="60" Runat="server" Text="Excel" 
										 onclick="btnExcel_Click"></asp:button>
								</td>
								<td align="right" colSpan="9"><asp:checkbox id="ChkTrue" Runat="server" Text="Show Detail"></asp:checkbox></td>
							</tr>
						</table>
					</td>
				</tr>
				<%if(Count==1)
				{
					%>
				<tr>
					<td vAlign="top">
						<%  
							InventoryClass obj = new InventoryClass();
							InventoryClass obj1 = new InventoryClass();
							ArrayList arrMonth = new ArrayList();
							double[] arrAmount=null;
							double[] arrPriviousAmt=new double[1];
							double GTotal=0;
							int Flage=0;
							SqlDataReader rdr=null,rdr1=null;
							if(DropSearchBy.SelectedIndex==0)
							{
								//09.09.09 vikas rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,ssr from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,ssr order by cust_name");
								rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,ssr,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,ssr,contactperson,mobile order by cust_name");
								rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
							}
							else
							{
								if(DropValue.Value=="All")
								{
									//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
									rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
									rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
								}
								else
								{
									if(DropSearchBy.SelectedItem.Text=="SSR")
									{
										//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by city,cust_name");
										rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by city,cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
						
										Flage=1;           //Add by vikas 07.09.09
									}
									else if(DropSearchBy.SelectedItem.Text=="City")
									{
										//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.city='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
										rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.city='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.city='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
									}
									else if(DropSearchBy.SelectedItem.Text=="District")
									{
										//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
										rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
									}
									else if(DropSearchBy.SelectedItem.Text=="State")
									{
										//09.09.09 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city order by cust_name");
										rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
									} /*****Add by vikas 16.11.2012****************/
									else if(DropSearchBy.SelectedItem.Text=="Customer Group")
									{
										string Spc_Pack="";
										Spc_Pack=Session["group"].ToString();
										Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
										rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
										
										/*coment by vikas 7.6.2013 rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");*/
									}
									else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
									{
										rdr=obj.GetRecordSet("select c.cust_name,city,sum(amount),l.cust_id,contactperson,mobile from customer c,ledgerdetails l where c.cust_id=l.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by l.cust_id,c.cust_name,c.city,contactperson,mobile order by cust_name");
										rdr1=obj1.GetRecordSet("select distinct month(bill_date),year(bill_date) bill_date,substring(datename(month,bill_date),0,4)+' '+datename(year,bill_date),datename(month,bill_date)+' '+datename(year,bill_date) from ledgerdetails l,customer c where bill_no<>'o/b' and amount<>0 and l.cust_id=c.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by bill_date order by bill_date");
									} /*****End****************/
								}
							}
							int i=1;
							int flag=0;
							string strBillDate="";
							if(Flage==0)
							{
								if(rdr.HasRows)
								{
									%>
						<table borderColor="#deba84" cellSpacing="0" align="center" border="1">
							<tr bgColor="#ce4848">
								<td align="center"><B><font color="white">S.No</font></B></td>
								<td align="center"><B><font color="white">PARTY NAME</font></B></td>
								<td align="center"><B><font color="white">PLACE</font></B></td>
								<%
								if(ChkTrue.Checked==true)
								{
									%>
								<td align="center"><B><font color="white">Contact Persion</font></B></td>
								<td align="center"><B><font color="white">Mobile No</font></B></td>
								<%
								}
											if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
											{
												%>
								<td align="center"><B><font color="white">SSR</font></B></td>
								<%
											}
											%>
								<td align="center"><B><font color="white">BILL DATE</font></B></td>
								<%
											int xx=0;
											if(rdr1.HasRows)
											{
												while(rdr1.Read())
												{
													if(xx==0)
													{
														%>
								<td align="center"><b><font color="white">Privious Dues
											<%=GetMonthName(rdr1.GetValue(2).ToString())%>
										</font></b>
								</td>
								<%xx=1;
													}
													%>
								<td align="center"><b><font color="white"><%=rdr1.GetValue(2).ToString()%><%arrMonth.Add(rdr1.GetValue(3).ToString());%></font></b></td>
								<%
												}
											}
											else
											{
												%>
								<td align="center"><b><font color="white">Privious Dues
											<%=GetMonthName1()%>
										</font></b>
								</td>
								<%
											}
											rdr1.Close();
											arrAmount=new double[arrMonth.Count];%>
								<td align="center"><B><font color="white">TOTAL</font></B></td>
							</tr>
							<%
										while(rdr.Read())
										{
											%>
							<tr>
								<td align="center"><%=i++%></td>
								<td><%=rdr["Cust_Name"].ToString()%></td>
								<td><%=rdr["City"].ToString()%></td>
								<%
								if(ChkTrue.Checked==true)
								{
									if(rdr["ContactPerson"].ToString()!="" && rdr["ContactPerson"].ToString()!=null)
									{
										%>
								<td align="left"><%=rdr["ContactPerson"].ToString()%></td>
								<%
									}
									else
									{
										%>
								<td>&nbsp;</td>
								<%
									}
									
									if(rdr["Mobile"].ToString()!="" && rdr["Mobile"].ToString()!=null)
									{
										%>
								<td align="left"><%=rdr["Mobile"].ToString()%></td>
								<%
									}
									else
									{
										%>
								<td>&nbsp;</td>
								<%
									}
								}
								if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
										{
												rdr1=obj1.GetRecordSet("select Emp_Name from employee where Emp_ID='"+rdr["SSR"].ToString()+"'");
												if(rdr1.Read())
												{	
													%>
								<td><%=rdr1["Emp_Name"].ToString()%></td>
								<%	
												}
												else
												{
													%>
								<td></td>
								<%
												}
												rdr1.Close();
											}
											strBillDate="";
											if(DropSearchBy.SelectedIndex==0)
												rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where cust_id='"+rdr["Cust_id"].ToString()+"' and bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
											else
											{
												if(DropValue.Value=="All")
													rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where bill_no<>'o/b' and Amount<>0 and cust_id='"+rdr["Cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
												else
												{
													if(DropSearchBy.SelectedItem.Text=="SSR")
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
													else if(DropSearchBy.SelectedItem.Text=="City")
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.City='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
													else if(DropSearchBy.SelectedItem.Text=="District")
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
													else if(DropSearchBy.SelectedItem.Text=="State")
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
													else if(DropSearchBy.SelectedItem.Text=="Customer Group")
													{
														string Spc_Pack="";
														Spc_Pack=Session["group"].ToString();
														Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
										
														//Coment By vikas 11.6.2013 rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
													}
													else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
												}
											}
											while(rdr1.Read())
											{
												strBillDate+=" "+rdr1.GetValue(0).ToString()+"-"+rdr1.GetValue(1).ToString()+",";
											}
											rdr1.Close();
											if(strBillDate.Length>1)
											{
												strBillDate=strBillDate.Substring(0,strBillDate.Length-1);}
												if(strBillDate!="")
												{
													%>
								<td><%=strBillDate%></td>
								<%
												}
												else
												{
													%>
								<td>&nbsp;</td>
								<%
												}
												double PreviousDue = 0;
												string[] ss=txtDateFrom.Text.IndexOf("/")>0?txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length):txtDateFrom.Text.Split(new char[] {'-'},txtDateFrom.Text.Length);
												string[] s=null;
												if(tempPeriod.Value!="")
													s=tempPeriod.Value.Split(new char[] {':'},tempPeriod.Value.Length);
												if(int.Parse(s[0])==int.Parse(ss[1].ToString()) && int.Parse(s[1])==int.Parse(ss[2]))
													rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateFrom.Text))+"'");
												else
													rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<'"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"'");
												if(rdr1.Read())
												{
													%>
								<td align="right"><%
													if(rdr1["amount"].ToString()!="")
													{
														%>
									<%=Math.Round(double.Parse(rdr1["amount"].ToString()))%>
									<%arrPriviousAmt[0]+=double.Parse(rdr1["amount"].ToString());
														GTotal+=double.Parse(rdr1["amount"].ToString());
														PreviousDue=double.Parse(rdr1["amount"].ToString());
													}
													else
													{
														%>
									0<%
													}
														%></td>
								<%
												}
												else
												{
													arrPriviousAmt[0]+=0;%>
								<td align="right">0</td>
								<%
												}
												rdr1.Close();
												int c=0;
												for(int j=0;j<arrMonth.Count;j++)
												{
													if(DropSearchBy.SelectedIndex==0)
														rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
													else
													{
														if(DropValue.Value=="All")
															rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
														else
														{
															if(DropSearchBy.SelectedItem.Text=="SSR")
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
															else if(DropSearchBy.SelectedItem.Text=="City")
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.city='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
															else if(DropSearchBy.SelectedItem.Text=="District")
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.state='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
															else if(DropSearchBy.SelectedItem.Text=="State")
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.country='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
															else if(DropSearchBy.SelectedItem.Text=="Customer Group")
															{
																string Spc_Pack="";
																Spc_Pack=Session["group"].ToString();
																Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
										
																//Coment by Vikas 11.6.2013 rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
															}
															else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
														}
													}
													while(rdr1.Read())
													{
														if(arrMonth[j].ToString()==rdr1.GetValue(1).ToString())
														{
															flag=1;
															if(rdr1.GetValue(1).ToString()!="" && rdr1.GetValue(1).ToString()!=null)
															{
																arrAmount[j]=arrAmount[j]+double.Parse(GenUtil.strNumericFormat(rdr1.GetValue(0).ToString()));
															}
															break;
														}
														else
														{
															flag=0;
														}
													}
													if(flag==1)
													{
														%>
								<td align="right"><%=Math.Round(double.Parse(rdr1.GetValue(0).ToString()))%></td>
								<%flag=0;
													}
													else
													{
														flag=0;%>
								<td align="right">0</td>
								<%
													}
													rdr1.Close();
												}
												rdr1=obj1.GetRecordSet("select sum(amount) from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id");
												if(rdr1.Read())
												{
													%>
								<td align="right"><%=Math.Round(double.Parse(rdr1.GetValue(0).ToString())+PreviousDue)%><%GTotal+=double.Parse(rdr1.GetValue(0).ToString());%></td>
								<%
												}
												else
												{
													%>
								<td align="right"><%=Math.Round(PreviousDue)%></td>
								<%
												}
												rdr1.Close();
											}
											%>
							</tr>
							<tr bgColor="#ce4848">
								<%
												if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
												{
													if(ChkTrue.Checked==true)
													{
														%>
								<td align="center" colSpan="7"><b><font color="white">Total</font></b></td>
								<%	
												}
												else
												{
													%>
								<td align="center" colSpan="5"><b><font color="white">Total</font></b></td>
								<%	
												}
											}
											else
											{
												if(ChkTrue.Checked==true)
												{
													%>
								<td align="center" colSpan="6"><b><font color="white">Total</font></b></td>
								<%	
												}
												else
												{
													%>
								<td align="center" colSpan="4"><b><font color="white">Total</font></b></td>
								<%
												}
											}
											%>
								<td align="right"><b><font color="white"><%=Math.Round(double.Parse(arrPriviousAmt[0].ToString()))%></font></b></td>
								<%
											for(int k=0;k<arrAmount.Length;k++)
											{
												%>
								<td align="right"><b><font color="white"><%=Math.Round(double.Parse(arrAmount[k].ToString()))%></font></b></td>
								<%
											}
											%>
								<td align="right"><b><font color="white"><%=Math.Round(double.Parse(GTotal.ToString()))%></font></b></td>
							</tr>
						</table>
						<%
							}
							else
							{
								MessageBox.Show("Data Not Available");
							}
						}
						else
						{
							if(rdr.HasRows)
							{
								%>
						<table borderColor="#deba84" cellSpacing="0" align="center" border="1">
							<tr bgColor="#ce4848">
								<td align="center"><B><font color="white">S.No</font></B></td>
								<td align="center"><B><font color="white">PARTY NAME</font></B></td>
								<td align="center"><B><font color="white">PLACE</font></B></td>
								<%
								if(ChkTrue.Checked==true)
								{
									%>
								<td align="center"><B><font color="white">Contact Person</font></B></td>
								<td align="center"><B><font color="white">Mobile No</font></B></td>
								<%
								}
								
										if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
										{
											%>
								<td align="center"><B><font color="white">SSR</font></B></td>
								<%
										}
										%>
								<td align="center"><B><font color="white">BILL DATE</font></B></td>
								<%
										int xx=0;
										if(rdr1.HasRows)
										{
											while(rdr1.Read())
											{
												if(xx==0)
												{
													%>
								<td align="center"><b><font color="white">Privious Dues
											<%=GetMonthName(rdr1.GetValue(2).ToString())%>
										</font></b>
								</td>
								<%xx=1;
												}
												%>
								<td align="center"><b><font color="white"><%=rdr1.GetValue(2).ToString()%><%arrMonth.Add(rdr1.GetValue(3).ToString());%></font></b></td>
								<%
											}
										}
										else
										{
											%>
								<td align="center"><b><font color="white">Privious Dues
											<%=GetMonthName1()%>
										</font></b>
								</td>
								<%
										}
										rdr1.Close();
										arrAmount=new double[arrMonth.Count];%>
								<td align="center"><B><font color="white">TOTAL</font></B></td>
							</tr>
							<%
									string City="";
									while(rdr.Read())
									{
										if(City!=rdr["City"].ToString())
										{
											if(City!="")
											{
												City=rdr["City"].ToString();
												%>
							<tr>
								<%
												if(ChkTrue.Checked==true)
												{
													%>
								<td colSpan="7">&nbsp;</td>
								<%
												}
												else
												{
													%>
								<td colSpan="5">&nbsp;</td>
								<%
												}
												for(int f=0;f<arrMonth.Count;f++)
												{
													%>
								<td>&nbsp;</td>
								<%
												}
												City=rdr["City"].ToString();
															%>
								<td>&nbsp;</td>
								<%
											}
										}
										
										City=rdr["City"].ToString();
											%>
							<tr>
								<td align="center"><%=i++%></td>
								<td><%=rdr["Cust_Name"].ToString()%></td>
								<td><%=rdr["City"].ToString()%></td>
								<%
								if(ChkTrue.Checked==true)
								{
									if(rdr["ContactPerson"].ToString()!="" && rdr["ContactPerson"].ToString()!=null)
									{
										%>
								<td align="left"><%=rdr["ContactPerson"].ToString()%></td>
								<%
									}
									else
									{
										%>
								<td>&nbsp;</td>
								<%
									}
									
									if(rdr["Mobile"].ToString()!="" && rdr["Mobile"].ToString()!=null)
									{
										%>
								<td align="left"><%=rdr["Mobile"].ToString()%></td>
								<%
									}
									else
									{
										%>
								<td>&nbsp;</td>
								<%
									}
								}
								if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
												{
													rdr1=obj1.GetRecordSet("select Emp_Name from employee where Emp_ID='"+rdr["SSR"].ToString()+"'");
													if(rdr1.Read())
													{	
														%>
								<td><%=rdr1["Emp_Name"].ToString()%></td>
								<%	
													}
													else
													{
														%>
								<td></td>
								<%
													}
													rdr1.Close();
												}
												strBillDate="";
												if(DropSearchBy.SelectedIndex==0)
													rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where cust_id='"+rdr["Cust_id"].ToString()+"' and bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
												else
												{
													if(DropValue.Value=="All")
														rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails where bill_no<>'o/b' and Amount<>0 and cust_id='"+rdr["Cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,bill_no,Bill_Date");
													else
													{
														if(DropSearchBy.SelectedItem.Text=="SSR")
															rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
														else if(DropSearchBy.SelectedItem.Text=="City")
															rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.City='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
														else if(DropSearchBy.SelectedItem.Text=="District")
															rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.state='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
														else if(DropSearchBy.SelectedItem.Text=="State")
															rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.country='"+DropValue.Value+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
														else if(DropSearchBy.SelectedItem.Text=="Customer Group")
														{
															string Spc_Pack="";
															Spc_Pack=Session["group"].ToString();
															Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
										
															//coment By vikas 11.6.2013 rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
															rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
														}
														else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
															rdr1=obj1.GetRecordSet("select bill_no,convert(varchar(8),Bill_Date,3) from ledgerdetails ld,customer c where bill_no not like'payment%' and bill_no<>'o/b' and Amount<>0 and bill_no not like'voucher%' and c.cust_id=ld.cust_id and ld.cust_id='"+rdr["Cust_id"].ToString()+"' and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,bill_no,Bill_Date");
													}
												}
												while(rdr1.Read())
												{
													strBillDate+=" "+rdr1.GetValue(0).ToString()+"-"+rdr1.GetValue(1).ToString()+",";
												}
												rdr1.Close();
												if(strBillDate.Length>1)
												{	
													strBillDate=strBillDate.Substring(0,strBillDate.Length-1);}
													if(strBillDate!="")
													{
														%>
								<td><%=strBillDate%></td>
								<%
													}
													else
													{
														%>
								<td>&nbsp;</td>
								<%
													}
													double PreviousDue = 0;
													string[] ss=txtDateFrom.Text.IndexOf("/")>0?txtDateFrom.Text.Split(new char[] {'/'},txtDateFrom.Text.Length):txtDateFrom.Text.Split(new char[] {'-'},txtDateFrom.Text.Length);
													string[] s=null;
													if(tempPeriod.Value!="")
														s=tempPeriod.Value.Split(new char[] {':'},tempPeriod.Value.Length);
													if(int.Parse(s[0])==int.Parse(ss[1].ToString()) && int.Parse(s[1])==int.Parse(ss[2]))
														rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateFrom.Text))+"'");
													else
														rdr1=obj1.GetRecordSet("select sum(amount) amount from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<'"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"'");
													if(rdr1.Read())
													{
														%>
								<td align="right"><%
														if(rdr1["amount"].ToString()!="")
														{
															%>
									<%=Math.Round(double.Parse(rdr1["amount"].ToString()))%>
									<%arrPriviousAmt[0]+=double.Parse(rdr1["amount"].ToString());
															GTotal+=double.Parse(rdr1["amount"].ToString());
															PreviousDue=double.Parse(rdr1["amount"].ToString());
														}
														else
														{
															%>
									0<%
														}
															%></td>
								<%
													}
													else
													{
														arrPriviousAmt[0]+=0;%>
								<td align="right">0</td>
								<%
													}
													rdr1.Close();
													int c=0;
													for(int j=0;j<arrMonth.Count;j++)
													{
														if(DropSearchBy.SelectedIndex==0)
															rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
														else
														{
															if(DropValue.Value=="All")
																rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails where cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by cust_id");
															else
															{
																if(DropSearchBy.SelectedItem.Text=="SSR")
																	rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																else if(DropSearchBy.SelectedItem.Text=="City")
																	rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.city='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																else if(DropSearchBy.SelectedItem.Text=="District")
																	rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.state='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																else if(DropSearchBy.SelectedItem.Text=="State")
																	rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.country='"+DropValue.Value+"' and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																else if(DropSearchBy.SelectedItem.Text=="Customer Group")
																{
																	string Spc_Pack="";
																	Spc_Pack=Session["group"].ToString();
																	Spc_Pack=Spc_Pack.Substring(0,Spc_Pack.Length-1);
										
																	//coment By vikas 11.6.2013 rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																	rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where group_name in ("+Spc_Pack+")) and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
																}
																else if(DropSearchBy.SelectedItem.Text=="Customer SubGroup")
																	rdr1=obj1.GetRecordSet("select sum(amount),datename(month,bill_date)+' '+datename(year,bill_date) as Bill_Date from ledgerdetails ld,customer c where c.cust_id=ld.cust_id and c.cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and ld.cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by ld.cust_id,datename(month,bill_date)+' '+datename(year,bill_date) order by ld.cust_id");
															}
														}
														while(rdr1.Read())
														{
															if(arrMonth[j].ToString()==rdr1.GetValue(1).ToString())
															{
																flag=1;
																if(rdr1.GetValue(1).ToString()!="" && rdr1.GetValue(1).ToString()!=null)
																{
																	arrAmount[j]=arrAmount[j]+double.Parse(GenUtil.strNumericFormat(rdr1.GetValue(0).ToString()));
																}
																break;
															}
															else
															{
																flag=0;
															}
														}
														if(flag==1)
														{
															%>
								<td align="right"><%=Math.Round(double.Parse(rdr1.GetValue(0).ToString()))%></td>
								<%flag=0;
														}
														else
														{
															flag=0;%>
								<td align="right">0</td>
								<%
														}
														rdr1.Close();
													}
													rdr1=obj1.GetRecordSet("select sum(amount) from ledgerdetails where bill_no<>'o/b' and cust_id='"+rdr["cust_id"].ToString()+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(getFromDate(txtDateFrom.Text))+"' and cast(floor(cast(cast(bill_date as datetime) as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(getToDate(txtDateTo.Text))+"' group by cust_id");
													if(rdr1.Read())
													{
														%>
								<td align="right"><%=Math.Round(double.Parse(rdr1.GetValue(0).ToString())+PreviousDue)%><%GTotal+=double.Parse(rdr1.GetValue(0).ToString());%></td>
								<%
													}
													else
													{
														%>
								<td align="right"><%=Math.Round(PreviousDue)%></td>
								<%
													}
													rdr1.Close();
												//}
												//else
												//{
													
												//}
												
												}
												rdr.Close();
												%>
							</tr>
							<tr bgColor="#ce4848">
								<%
								
											if(DropSearchBy.SelectedIndex==0 && DropValue.Value=="All")
											{
												if(ChkTrue.Checked==true)
												{
													%>
								<td align="center" colSpan="7"><b><font color="white">Total</font></b></td>
								<%
												}
												else
												{
													%>
								<td align="center" colSpan="5"><b><font color="white">Total</font></b></td>
								<%
												}
											}
											else
											{
												if(ChkTrue.Checked==true)
												{
													%>
								<td align="center" colSpan="6"><b><font color="white">Total</font></b></td>
								<%
												}
												else
												{
													%>
								<td align="center" colSpan="4"><b><font color="white">Total</font></b></td>
								<%
												}
											}
											%>
								<td align="right"><b><font color="white"><%=Math.Round(double.Parse(arrPriviousAmt[0].ToString()))%></font></b></td>
								<%
											for(int k=0;k<arrAmount.Length;k++)
											{
												%>
								<td align="right"><b><font color="white"><%=Math.Round(double.Parse(arrAmount[k].ToString()))%></font></b></td>
								<%
											}
											%>
								<td align="right"><b><font color="white"><%=Math.Round(double.Parse(GTotal.ToString()))%></font></b></td>
							</tr>
						</table>
						<%
							}
							else
							{
								MessageBox.Show("Data Not Available");
							}
						}
				}
				
				%>
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
