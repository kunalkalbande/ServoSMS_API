<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="System.Data.SqlClient" %>
<%@ Import namespace="RMG" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.NillSellingRo_Report" CodeFile="NillSellingRO_Report.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Nill Selling RO</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
					f.texthiddenprod.value=f.tempDistrict.value;
				else if(index == 4)
					f.texthiddenprod.value=f.tempSSR.value;
				f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
			}
			else
				f.texthiddenprod.value="";
			document.Form1.DropValue.value="All";
			document.Form1.DropProdName.style.visibility="hidden";
		}
		
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
			<table height="290" cellSpacing="0" cellPadding="0" width="775" align="center" border="0">
				<tr vAlign="top">
					<th colSpan="8" height="20">
						<font color="#ce4848">Nill Selling Customers</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<table>
							<tr>
								<td>&nbsp;Date From&nbsp;</td>
								<td><asp:textbox id="txtDateFrom" runat="server" Width="65px" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox>&nbsp; <A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;">
										<IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>&nbsp;Date To&nbsp;</td>
								<td><asp:textbox id="txtDateTo" runat="server" Width="65px" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox>&nbsp; <A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;">
										<IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td>Search By&nbsp;</td>
								<td><asp:dropdownlist id="DropSearchBy" CssClass="dropdownlist" Runat="server" onchange="CheckSearchOption(this);"
										AutoPostBack="False">
										<asp:ListItem Value="Select">All</asp:ListItem>
										<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
										<asp:ListItem Value="Customer SubGroup">Customer SubGroup</asp:ListItem>
										<asp:ListItem Value="Country">District</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
									</asp:dropdownlist></td>
								<td>&nbsp;&nbsp;Value&nbsp;</td>
								<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnView)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 125px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="All" name="DropValue" runat="server"> <input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnView)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnView,document.Form1.DropValue)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 145px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
											type="select-one"></select></div>
								</td>
							</tr>
							<tr vAlign="top">
								<td vAlign="top" align="center" colSpan="8">&nbsp;
									<asp:RadioButton ID="RadNill" CssClass="dropdownlist" GroupName="Customer" Runat="server" Text="Nill"
										Checked="True"></asp:RadioButton>&nbsp;
									<asp:RadioButton ID="RadSele" CssClass="dropdownlist" GroupName="Customer" Runat="server" Text="Selling"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnView" Width="70" Runat="server" 
										Text="View" onclick="btnView_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnPrint" Width="70" Runat="server" 
										Text="Print" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" Width="70" Runat="server" 
										Text="Excel" onclick="btnExcel_Click"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr vAlign="top">
					<td colSpan="8">
						<%
						try
						{
							if(flage==1)
							{
								DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								DBUtil dbobj2=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
								SqlDataReader rdr=null,rdr1=null,SqlDtr=null;
								string sql="";
								//coment by vikas 19.11.2012 dbobj.SelectQuery("select distinct state from customer order by state",ref rdr);
								if(RadNill.Checked==true)
								{
									if(DropSearchBy.SelectedIndex!=0)
									{
										if(DropSearchBy.SelectedIndex==1)
											sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') order by state";
										else if(DropSearchBy.SelectedIndex==2)
											sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') order by state";
										else if(DropSearchBy.SelectedIndex==3)
											sql="select distinct state from customer where state ='"+DropValue.Value+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
										else if(DropSearchBy.SelectedIndex==4)
											sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by state";
									}
									else
									{
										sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
									}
								}
								else
								{
								
									if(DropSearchBy.SelectedIndex!=0)
									{
										if(DropSearchBy.SelectedIndex==1)
											sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') order by state";
										else if(DropSearchBy.SelectedIndex==2)
											sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') order by state";
										else if(DropSearchBy.SelectedIndex==3)
											sql="select distinct state from customer where state ='"+DropValue.Value+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
										else if(DropSearchBy.SelectedIndex==4)
											sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by state";
									}
									else
									{
										sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
									}
								
								}
								//coment by vikas 20.11.2012 sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
								dbobj.SelectQuery(sql,ref rdr);
								if(rdr.HasRows)
								{
									while(rdr.Read())
									{
										%>
						<table width="600" align="center" border="0" vAlign="top">
							<%
										//coment by vikas 19.11.2012 dbobj1.SelectQuery("select * from customer where state='"+rdr["State"].ToString()+"' order by cust_type",ref SqlDtr);
										if(RadNill.Checked==true)
										{
											if(DropSearchBy.SelectedIndex!=0)
											{
												if(DropSearchBy.SelectedIndex==1)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') order by cust_type";
												else if(DropSearchBy.SelectedIndex==2)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') order by cust_type";
												else if(DropSearchBy.SelectedIndex==3)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by cust_type";
												else if(DropSearchBy.SelectedIndex==4)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by cust_type";
											}
											else
											{
												sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by cust_type";
											}
										}
										else
										{
											if(DropSearchBy.SelectedIndex!=0)
											{
												if(DropSearchBy.SelectedIndex==1)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') order by cust_type";
												else if(DropSearchBy.SelectedIndex==2)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') order by cust_type";
												else if(DropSearchBy.SelectedIndex==3)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by cust_type";
												else if(DropSearchBy.SelectedIndex==4)
													sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by cust_type";
											}
											else
											{
												sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by cust_type";
											}
										}
										//sql="select * from customer where state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by cust_type";
										dbobj1.SelectQuery(sql,ref SqlDtr);
										if(SqlDtr.HasRows)
										{
											%>
							<tr>
								<td colSpan="4">&nbsp;&nbsp;&nbsp;&nbsp;<b>District :<%=rdr["State"].ToString()%></b></td>
							</tr>
							<tr>
								<td>
									<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="100%" bgColor="#fff7e7"
										border="1">
										<tr bgColor="#ce4848" height="22">
											<td align="center" width="15%">&nbsp;<b><font color="white">District</font></b></td>
											<td align="center" width="50%">&nbsp;<b><font color="white">Firm Name</font></b></td>
											<td align="center" width="20%">&nbsp;<b><font color="white">Place</font></b></td>
											<td align="center" width="15%">&nbsp;<b><font color="white">Category</font></b></td>
										</tr>
										<%
														while(SqlDtr.Read())
														{
															%>
										<tr>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["State"].ToString()%></font></td>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["Cust_Name"].ToString()%></font></td>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["City"].ToString()%></font></td>
											<td>&nbsp;<font color="#8c4510"><%=SqlDtr["Cust_Type"].ToString()%></font></td>
										</tr>
										<%
														}
														//coment By vikas 19.11.2012 dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' group by cust_type)",ref rdr1);
														if(RadNill.Checked==true)
														{
															if(DropSearchBy.SelectedIndex!=0)
															{
																if(DropSearchBy.SelectedIndex==1)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
																else if(DropSearchBy.SelectedIndex==2)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
																else if(DropSearchBy.SelectedIndex==3)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
																else if(DropSearchBy.SelectedIndex==4)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
															}
															else
															{
																sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
															}
														}
														else
														{
														
															if(DropSearchBy.SelectedIndex!=0)
															{
																if(DropSearchBy.SelectedIndex==1)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
																else if(DropSearchBy.SelectedIndex==2)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
																else if(DropSearchBy.SelectedIndex==3)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
																else if(DropSearchBy.SelectedIndex==4)
																	sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
															}
															else
															{
																sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
															}
														
														}
														//sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end,count(*) from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type,count(*) from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and state='"+rdr["State"].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
														dbobj2.SelectQuery(sql,ref rdr1);
														while(rdr1.Read())
														{
															%>
										<tr bgColor="#ce4848">
											<td colSpan="1">&nbsp;</td>
											<td>&nbsp;<font color="white"><b>Total&nbsp;<%=rdr1.GetValue(0).ToString()%></b></font></td>
											<td colSpan="2">&nbsp;<font color="white"><b><%=rdr1.GetValue(1).ToString()%></b></font></td>
										</tr>
										<%
														}
														rdr1.Close();
														%>
									</table>
								</td>
							</tr>
							<%
										}
									}
									SqlDtr.Close();%>
						</table>
						<%	
								}
								else
								{
									MessageBox.Show("Data Not Available");
									//return;
								}
								%>
					</td>
				</tr>
				<tr vAlign="top">
					<td colSpan="8">
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="77%" align="center"
							bgColor="#fff7e7" border="1">
							<%
									ArrayList arrType = new ArrayList();
									ArrayList arrState = new ArrayList();
									//coment by vikas 19.11.2012 dbobj2.SelectQuery("select distinct state from customer order by state",ref rdr1);
														
									if(RadNill.Checked==true)
									{
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') order by state";
											else if(DropSearchBy.SelectedIndex==2)
												sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') order by state";
											else if(DropSearchBy.SelectedIndex==3)
												sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and state='"+DropValue.Value+"' order by state";
											else if(DropSearchBy.SelectedIndex==4)
												sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by state";
										}
										else
										{
											sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
										}
									}
									else
									{
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') order by state";
											else if(DropSearchBy.SelectedIndex==2)
												sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') order by state";
											else if(DropSearchBy.SelectedIndex==3)
												sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and state='"+DropValue.Value+"' order by state";
											else if(DropSearchBy.SelectedIndex==4)
												sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') order by state";
										}
										else
										{
											sql="select distinct state from customer where cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
										}
									}
									//sql="select distinct state from customer where cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) order by state";
									dbobj2.SelectQuery(sql,ref rdr1);
									if(rdr1.HasRows)
									{
										%>
							<tr bgColor="#ce4848" height="22">
								<td align="center"><b><font color="white">Type / Distict</font></b></td>
								<%
											while(rdr1.Read())
											{
												arrState.Add(rdr1.GetValue(0).ToString());%>
								<td align="center"><b><font color="white"><%=rdr1["State"].ToString()%></font></b></td>
								<%		
											}
												%>
								<td align="center"><b><font color="white">Total</font></b></td>
							</tr>
							<%
									}
									rdr1.Close();
									double Total=0;
									int count=0;
									//coment by vikas 19.11.2012 dbobj2.SelectQuery("(select distinct case when substring(cust_type,1,2)='Ro' then 'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') group by cust_type)",ref rdr1);
									
									if(RadNill.Checked==true)
									{
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
											else if(DropSearchBy.SelectedIndex==2)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
											else if(DropSearchBy.SelectedIndex==3)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
											else if(DropSearchBy.SelectedIndex==4)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
										}
										else
										{
											sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
										}
									}
									else
									{
									
										if(DropSearchBy.SelectedIndex!=0)
										{
											if(DropSearchBy.SelectedIndex==1)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
											else if(DropSearchBy.SelectedIndex==2)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
											else if(DropSearchBy.SelectedIndex==3)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
											else if(DropSearchBy.SelectedIndex==4)
												sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
										}
										else
										{
											sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
										}
									
									}
									//sql="(select distinct case when substring(cust_type,1,2)='Ro' then  'RO' when substring(cust_type,1,2)='Oe' then 'OE' else substring(cust_type,1,2) end from customer where (substring(cust_type,1,2)='Ro' or substring(cust_type,1,2)='oe') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by substring(cust_type,1,2)) union (select cust_type from customer where (substring(cust_type,1,2)!='Ro' and substring(cust_type,1,2)!='oe') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103)) group by cust_type)";
									dbobj2.SelectQuery(sql,ref rdr1);
									while(rdr1.Read())
									{
										arrType.Add(rdr1.GetValue(0).ToString());
									}
									rdr1.Close();
									double Colm=arrState.Count;
									double Row=arrType.Count;
									double tot=Colm*Row;
									double[] Counter=new double[140];
									ArrayList Row_Tot = new ArrayList();
									for(int i=0;i<arrType.Count;i++)
									{
										Total=0;
										%>
							<tr>
								<td>&nbsp;<b><font color="#8c4510">Total&nbsp;<%=arrType[i].ToString()%></font></b></FONT></td>
								<%
												for(int j=0;j<arrState.Count;j++)
												{
													//coment by vikas 19.11.2012 dbobj2.SelectQuery("select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"'",ref rdr1);
													if(RadNill.Checked==true)
													{
														if(DropSearchBy.SelectedIndex!=0)
														{	
															if(DropSearchBy.SelectedIndex==1)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
															else if(DropSearchBy.SelectedIndex==2)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
															else if(DropSearchBy.SelectedIndex==3)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
															else if(DropSearchBy.SelectedIndex==4)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
														}
														else
														{
															sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
														}
													}	
													else
													{
														if(DropSearchBy.SelectedIndex!=0)
														{	
															if(DropSearchBy.SelectedIndex==1)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_type in (select customertypename from customertype where group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
															else if(DropSearchBy.SelectedIndex==2)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_type in (select customertypename from customertype where sub_group_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
															else if(DropSearchBy.SelectedIndex==3)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
															else if(DropSearchBy.SelectedIndex==4)
																sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and ssr=(select emp_id from employee where emp_name='"+DropValue.Value+"') and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
														}
														else
														{
															sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_id in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
														}
													}
													//sql="select count(*) from customer where cust_type like'"+arrType[i].ToString()+"%' and state='"+arrState[j].ToString()+"' and cust_id not in (select cust_id from sales_master where  cast(floor(cast(invoice_date as float)) as datetime)>=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateFrom"].ToString()) + "',103) and  cast(floor(cast(invoice_date as float)) as datetime)<=Convert(datetime,'" + GenUtil.str2DDMMYYYY(Request.Form["txtDateTo"].ToString()) + "',103))";
													dbobj2.SelectQuery(sql,ref rdr1);
															
													if(rdr1.Read())
													{
														%>
								<td align="center"><font color="#8c4510"><%=rdr1.GetValue(0).ToString()%></font><%Total+=double.Parse(rdr1.GetValue(0).ToString());%></td>
								<%
														Counter[count++]=double.Parse(rdr1.GetValue(0).ToString());
													}
													rdr1.Close();
												}
												%>
								<td align="center"><b><font color="#8c4510"><%=Total.ToString()%></font></b></td>
							</tr>
							<%
										}
										double[] Tot_Counter=new double[10];
										double Tot_=0;	
										int row=arrType.Count;
										int colm=arrState.Count;
										for(int j=0,i=0;j<140;)
										{
											Tot_+=Counter[j];
											Tot_Counter[i++]+=Counter[j++];
											//MessageBox.Show(Tot_Counter[j].ToString());
											if(i==colm)
											{
												i=0;
											}
										}				
										%>
							<tr bgColor="#ce4848">
								<td align="center"><b><font color="white">ToTal</font></b></td>
								<% 
											for(int i=0;i<arrState.Count;i++)
											{
												%>
								<td align="center"><b><font color="white"><%=Tot_Counter[i]%></font></b></td>
								<%
											}
											%>
								<td align="center"><b><font color="white"><%=Tot_%></font></b></td>
							</tr>
						</table>
					</td>
				</tr>
				<%
							}
						}
						catch(Exception ex)
						{
							//MessageBox.Show(ex.Message);
						}
						%>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
