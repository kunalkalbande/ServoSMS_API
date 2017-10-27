<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations"%>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.HSD_MS_Report" CodeFile="HSD_MS_Report.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS : HSD MS Report</title> 
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
						<font color="#ce4848">MS HSD Report</font>
						<hr>
					</th>
				</tr>
				<tr vAlign="top">
					<td align="center">
						<table width="95%">
							<tr>
								<td width="8%">Date From</td>
								<td><asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" BorderStyle="Groove" CssClass="FontStyle"
										Width="65px"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td width="7%">Date To</td>
								<td><asp:textbox id="txtDateTo" runat="server" ReadOnly="True" BorderStyle="Groove" CssClass="FontStyle"
										Width="65px"></asp:textbox>&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></td>
								<td width="8%">Search By</td>
								<td><asp:dropdownlist id="DropSearchBy" CssClass="dropdownlist" Runat="server" AutoPostBack="False" onchange="CheckSearchOption(this);" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
										<asp:ListItem Value="Select">All</asp:ListItem>
										<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
										<asp:ListItem Value="Customer SubGroup">SubGroup</asp:ListItem>
										<asp:ListItem Value="City">City</asp:ListItem>
										<asp:ListItem Value="Country">District</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
									</asp:dropdownlist></td>
								<td>Value</td>
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
								<td align="right" colSpan="9">&nbsp;&nbsp;<asp:button id="btnView" Width="60" Runat="server" Text="Search" 
										 onclick="btnView_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnPrint" Width="60" Runat="server" Text="Print"
										 onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="60" Runat="server" Text="Excel" 
										 onclick="btnExcel_Click"></asp:button>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="9">
									<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="80%" border="1">
										<%
										if(flage==1)
										{	
											double Tot_MS=0;
											double Tot_HSD=0;
											int i=1;
											string Cust_type="",Place="";
											try
											{
												string sql="";
												InventoryClass obj1=new InventoryClass();
												SqlDataReader dtr=null;
												//string sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
												
												if(DropSearchBy.SelectedIndex!=0)
												{
													if(DropSearchBy.SelectedIndex==1)
														sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and c.cust_name='"+DropValue.Value.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
													else if(DropSearchBy.SelectedIndex==2)
														sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and Cust_type in (select customertypename from customertype where Sub_group_name='"+DropValue.Value.ToString()+"') and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
													else if(DropSearchBy.SelectedIndex==3)
														sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and City='"+DropValue.Value.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
													else if(DropSearchBy.SelectedIndex==4)
														sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and State='"+DropValue.Value.ToString()+"' and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
													else if(DropSearchBy.SelectedIndex==5)
														sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and SSR in (select emp_id from employee where Emp_Name='"+DropValue.Value.ToString()+"') and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
												}
												else
												{
													sql="select c.Cust_id,Cust_Name,cust_type,city,ms,hsd,datefrom,dateto from Customer c,Cust_sale_ms_hsd CSMH where c.cust_id=csmh.cust_id and (cast(floor(cast(datefrom as float)) as datetime)>='"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and cast(floor(cast(dateto as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(datefrom as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' or cast(floor(cast(dateto as float)) as datetime) between '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and '"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"')";
												}
			
												dtr=obj1.GetRecordSet(sql);
												if(dtr.HasRows)
												{
													%>
										<tr>
											<th bgcolor="#ce4848">
												<font color="#ffffff">SNo</font></th>
											<th bgcolor="#ce4848">
												<font color="#ffffff">Customer Name</font></th>
											<th bgcolor="#ce4848">
												<font color="#ffffff">Cust Type</font></th>
											<th bgcolor="#ce4848">
												<font color="#ffffff">Place</font></th>
											<th bgcolor="#ce4848">
												<font color="#ffffff">MS</font></th>
											<th bgcolor="#ce4848">
												<font color="#ffffff">HSD</font></th>
										</tr>
										<%	
													while(dtr.Read())
													{
														%>
										<tr>
											<td><%=i.ToString()%></td>
											<td><%=dtr["Cust_Name"].ToString()%></td>
											<td><%=dtr["Cust_Type"].ToString()%></td>
											<td><%=dtr["City"].ToString()%></td>
											<td align="right"><%=dtr["MS"].ToString()%></td>
											<td align="right"><%=dtr["HSD"].ToString()%></td>
										</tr>
										<%
														i++;
														Tot_MS+=double.Parse(dtr["MS"].ToString());
														Tot_HSD+=double.Parse(dtr["HSD"].ToString());
													}
													dtr.Close();
													
													if(DropSearchBy.SelectedIndex==0)
													{
														sql="select * from cust_sale_ms_hsd where cust_id=0";
														dtr=obj1.GetRecordSet(sql);
														if(dtr.HasRows)
														{
															while(dtr.Read())
															{
																%>
										<tr>
											<td><%=i.ToString()%></td>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
											<td align="right"><%=dtr["MS"].ToString()%></td>
											<td align="right"><%=dtr["HSD"].ToString()%></td>
										</tr>
										<%
																i++;
																Tot_MS+=double.Parse(dtr["MS"].ToString());
																Tot_HSD+=double.Parse(dtr["HSD"].ToString());
															}
															dtr.Close();
														}
													}
													%>
										<tr>
											<th colspan="4" bgcolor="#ce4848">
												<font color="#ffffff">Total</font></th>
											<th align="right" bgcolor="#ce4848">
												<font color="#ffffff">
													<%=Tot_MS%>
												</font>
											</th>
											<th align="right" bgcolor="#ce4848">
												<font color="#ffffff">
													<%=Tot_HSD%>
												</font>
											</th>
										</tr>
										<%	
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
