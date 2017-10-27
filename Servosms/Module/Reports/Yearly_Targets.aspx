<%@ Page language="c#" Inherits="Servosms.Module.Reports.Yearly_Targets" CodeFile="Yearly_Targets.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ import namespace="RMG"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ import namespace="DBOperations"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Yearly Report</title> <!--
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
	function CheckSearchOption(t)
	{
		var index = t.selectedIndex
		var f = document.Form1;
		if(index != 0)
		{
			if(index == 1)
				f.texthiddenprod.value=f.tempCustType.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempDistrict.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempPlace.value;
			else if(index == 5)
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
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Width="8px" Visible="False"></asp:textbox>
			<input type="hidden" runat="server" id="tempCustName" style="WIDTH:1px" name="tempCustName">
			<input type="hidden" runat="server" id="tempCustType" style="WIDTH:1px" NAME="tempCustType">
			<input type="hidden" runat="server" id="tempDistrict" style="WIDTH:1px" NAME="tempDistrict">
			<input type="hidden" runat="server" id="tempSSR" style="WIDTH:1px" NAME="tempSSR">
			<input type="hidden" runat="server" id="tempPlace" style="WIDTH:1px" NAME="tempPlace">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" align="center">
				<TBODY>
					<TR>
						<TH valign="top" height="20" colspan="9">
							<font color="#ce4848">Yearly Targets Report</font>
							<hr>
						</TH>
					</TR>
					<tr height="20">
						<td align="center">Search By</td>
						<td><asp:dropdownlist id="DropSearchBy" runat="server" onchange="CheckSearchOption(this)" CssClass="dropdownlist" onselectedindexchanged="DropSearchBy_SelectedIndexChanged">
								<asp:ListItem Value="All">All</asp:ListItem>
								<asp:ListItem Value="Category">Category</asp:ListItem>
								<asp:ListItem Value="District">District</asp:ListItem>
								<asp:ListItem Value="Name">Name</asp:ListItem>
								<asp:ListItem Value="Place">Place</asp:ListItem>
								<asp:ListItem Value="SSR">SSR</asp:ListItem>
							</asp:dropdownlist></td>
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
						<td><asp:textbox id="txtDateFrom" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td>To</td>
						<td><asp:textbox id="txtDateTo" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td><asp:button id="btnview1" Width="65px" Runat="server" Text="View" 
								 onclick="btnview1_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="65px" Runat="server" Text="Excel" 
								 onclick="btnExcel_Click"></asp:button></td>
					</tr>
					<tr>
						<td vAlign="top" colspan="9">
							<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
								<%if(View==1)
								{
									DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
									DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
									int flag=0;
									SqlDataReader rdr = null,rdr1=null;
									dbobj.SelectQuery(sql,ref rdr);
									
									if(rdr.HasRows)
									{
										flag=1;%>
								<tr>
									<th colspan="4">
										&nbsp;</th>
									<%
											for(int m=0;m<DateFrom.Length;m++)
											{
												%>
									<th colSpan="1">
										<%=GetMonthName(DateFrom[m].ToString())%>
									</th>
									<%
											}
											%>
									<th>
										Total</th>
									<th>
										Target after</th>
								</tr>
								<tr bgColor="#ce4848">
									<td align="center"><b><font color="white">Sr.No.</font></b></td>
									<td align="center"><b><font color="white">Customer Name</font></b></td>
									<td align="center"><b><font color="white">Type</font></b></td>
									<td align="center"><b><font color="white">Place</font></b></td>
									<%
											for(int m=0;m<DateFrom.Length;m++)
											{
												%>
									<td align="center"><b><font color="white">Total</font></b></td>
									<%
											}
											%>
									<td></td>
									<td align="center"><b><font color="white">10% Growth</font></b></td>
								</tr>
								<%
									}
									if(flag==1)
									{
										while(rdr.Read())
										{
											%>
								<tr>
									<td><%=i.ToString()%></td>
									<td><%=rdr["Cust_Name"].ToString()+":"+rdr["Cust_ID"].ToString()%></td>
									<td><%=rdr["Cust_Type"].ToString()%></td>
									<td><%=rdr["City"].ToString()%></td>
									<%int k=-1;
											double Tot=0;
											double Tot_Ten=0;
											for(int j=0;j<DateFrom.Length;j++)
											{	
												string Cust_ID=rdr["Cust_ID"].ToString();
												//dbobj.SelectQuery("select sum(totalqty) total,sum(oil2t+oil4t) t2t4 from vw_salesoil v,sales_master sm where sm.invoice_no=v.invoice_no and sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[j].ToString()+"' group by sm.cust_id",ref rdr1); //Comment By Vikas Sharma 17.04.09
												dbobj.SelectQuery("select sum(totalqty) total from vw_salesoil v,sales_master sm where sm.invoice_no=v.invoice_no and sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[j].ToString()+"' group by sm.cust_id",ref rdr1);
												if(rdr1.Read())
												{
													%>
									<td align="right"><%=Math.Round(double.Parse(rdr1["Total"].ToString()))%><%TotalSum[++k]+=Math.Round(double.Parse(rdr1["Total"].ToString()));%></td>
									<%Tot+=Math.Round(double.Parse(rdr1["Total"].ToString()));
												}
												else
												{
													%>
									<td align="right">0</td>
									<%
													k++;
												}
												rdr1.Close();
											}
											if(Tot!=0)
											{
												Tot_Ten=Tot/10;
												Tot_Ten=Tot_Ten+Tot;
											}
											else
											{
												Tot_Ten=0;
											}
											%>
									<td align="right">&nbsp;<%=Tot.ToString()%></td>
									<td align="right">&nbsp;<%=Math.Round(Tot_Ten)%></td>
								</tr>
								<%	i++;
										}
										%>
								<tr bgColor="#ce4848">
									<td colspan="4" align="center"><font color="white"><b>Total</b></font></td>
									<%
											double G_Tot=0;
											for(int j=0;j<TotalSum.Length;j++)
											{
												%>
									<td align="right"><font color="white"><b><%=TotalSum[j].ToString()%></b></font></td>
									<%
												G_Tot+=TotalSum[j];
											}
											double G_Tot_Ten=0;
											if(G_Tot!=0)
											{
												G_Tot_Ten=G_Tot/10;
												G_Tot_Ten=G_Tot_Ten+G_Tot;
											}
											else
											{
												G_Tot_Ten=0;
											}
											%>
									<td align="right"><font color="white"><b><%=Math.Round(G_Tot)%></b></font></td>
									<td align="right"><font color="white"><b><%=Math.Round(G_Tot_Ten)%></b></font></td>
								</tr>
								<%
										}
									}
									%>
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
