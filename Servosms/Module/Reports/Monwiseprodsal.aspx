<%@ Page language="c#" Inherits="Servosms.Module.Reports.Monwiseprodsal" CodeFile="Monwiseprodsal.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ import namespace="RMG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Mont. Prod. Secon. Sales</title><!--
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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			if(index == 1)
				f.texthiddenprod.value=f.tempCategory.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempDistrict.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempNameWithPack.value;
			else if(index == 4)
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
				Visible="False" Width="8px"></asp:textbox>
			<input type="hidden" runat="server" id="tempCategory" style="WIDTH:1px" name="tempCustName">
			<input type="hidden" runat="server" id="tempDistrict" style="WIDTH:1px" NAME="tempDistrict">
			<input type="hidden" runat="server" id="tempNameWithPack" style="WIDTH:1px" NAME="tempInvoiceNo">
			<input type="hidden" runat="server" id="tempSSR" style="WIDTH:1px" NAME="tempSSR">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" align="center">
				<TBODY>
					<TR>
						<TH vAlign="top" height="20" colspan="9">
							<font color="#ce4848">District Wise / Month Wise /&nbsp;Channel Wise / Products 
								Wise Sales Report</font>
							<hr>
						</TH>
					</TR>
					<tr height="20">
						<td align="center" height="20">Search By</td>
						<td><asp:DropDownList ID="DropSearchBy" Runat="server" CssClass="dropdownlist" onchange="CheckSearchOption(this)" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
								<asp:ListItem Value="All">All</asp:ListItem>
								<asp:ListItem Value="Category">Category</asp:ListItem>
								<asp:ListItem Value="District">District</asp:ListItem>
								<asp:ListItem Value="Name With Pack">Name With Pack</asp:ListItem>
								<asp:ListItem Value="SSR">SSR</asp:ListItem>
							</asp:DropDownList></td>
						<td>Value</td>
						<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
								onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnview1)"
								style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 210px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
								value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
								readOnly type="text" name="temp"><br>
							<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnview1)"
									id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnview1,document.Form1.DropValue)"
									style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 230px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
									type="select-one"></select></div>
						</td>
						<td>From</td>
						<td><asp:textbox id="txtDateFrom" runat="server" Width="100px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td>To</td>
						<td><asp:textbox id="txtDateTo" runat="server" Width="100px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td><asp:button id="btnview1" Width="60" Runat="server" Text="View" 
								 onclick="btnview1_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="60" Runat="server" Text="Excel" 
								 onclick="btnExcel_Click"></asp:button></td>
					</tr>
					<tr>
						<td vAlign="top" colspan="9">
							<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
								<%if(View==1)
								{
									int flag=0;
									if(arrProductList.Length!=0)
									{
										flag=1;%>
										<tr><th>&nbsp;</th>
										<%
										for(int m=0;m<DateFrom.Length;m++)
										{
											%>
											<th colSpan="8"><%=GetMonthName(DateFrom[m].ToString())%></th>
											<%
										}
										%>
									</tr>
									<tr bgColor="#ce4848">
										<td align="center" width="250"><b><font color="white">Product</font></b></td>
										<%
										for(int m=0;m<DateFrom.Length;m++)
										{
											%>
											<td align="center"><b><font color="white">KSK</font></b></td>
											<td align="center"><b><font color="white">N-KSK</font></b></td>
											<td align="center"><b><font color="white">IBP</font></b></td>
											<td align="center"><b><font color="white">Bazzar</font></b></td>
											<td align="center"><b><font color="white">Ro</font></b></td>
											<td align="center"><b><font color="white">Oe</font></b></td>
											<td align="center"><b><font color="white">Fleet</font></b></td>
											<td align="center"><b><font color="white">Total</font></b></td>
											<%
										}
										%>
									</tr>
									<%
								}
								if(flag==1)
								{
									for(int i=0;i<arrProduct.GetLength(0);i++)
									{
										if(Convert.ToString(arrProductList[i])!=null)
										{
											%>
											<tr>
												<td><%=arrProductList[i].ToString()%></td>
												<%
												for(int j=0;j<8*(DateFrom.Length);j++)
												{
													%>
													<td align="right"><%=arrProduct[i,j].ToString()%></td>
													<%
												}
												%>	
											</tr>
											<%
										}
									}
									%>
									<tr bgColor="#ce4848"><td align="center"><b><font color="white">Total</font></b></td>
									<%
									for(int j=0;j<GrantTotal.Length;j++)
									{
										%>
										<td align="right"><b><font color="white"><%=GrantTotal[j].ToString()%></font></b></td>
										<%
									}
									%>
									</tr>
									<%
								}
								}	%>
							</table>
						</td>
					</tr>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
