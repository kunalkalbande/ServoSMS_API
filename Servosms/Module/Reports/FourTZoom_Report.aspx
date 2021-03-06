<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="DBOperations"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.FourTZoom_Report" CodeFile="FourTZoom_Report.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: 4T Zoom Sales Report</title>
		<meta name="vs_showGrid" content="False"> <!--
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
			/* Coment By Vikas 17.11.2012
			if(index == 1)
				f.texthiddenprod.value=f.tempCustType.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempDistrict.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempPlace.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempSSR.value;*/
				
			if(index == 1)
				f.texthiddenprod.value=f.tempGroup.value;
			else if(index == 2)
				f.texthiddenprod.value=f.tempSubGroup.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempDistrict.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempCustName.value;
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
				Width="8px" Visible="False"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<input id="tempGroup" style="WIDTH: 1px" type="hidden" name="tempGroup" runat="server">
			<input id="tempSubGroup" style="WIDTH: 1px" type="hidden" name="tempSubGroup" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" align="center">
				<TBODY>
					<TR>
						<TH vAlign="top" colSpan="9" height="20">
							<font color="#ce4848">4T Zoom Sales/Communitive Yearly Sales Report</font>
							<hr>
						</TH>
					</TR>
					<tr height="20">
						<td colspan="9" align="center"><asp:radiobutton id="RadioInvoice" GroupName="Wise" Checked="True" Runat="server" Text="Invoice Wise"></asp:radiobutton>
							<asp:radiobutton id="RadioYearly" GroupName="Wise" Runat="server" Text="Year Wise"></asp:radiobutton>&nbsp;&nbsp;From&nbsp;&nbsp;<asp:textbox id="txtDateFrom" runat="server" Width="100px" BorderStyle="Groove" ReadOnly="True"
								CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A>&nbsp; To
							<asp:textbox id="txtDateTo" runat="server" Width="100px" BorderStyle="Groove" ReadOnly="True"
								CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A>&nbsp;
							<asp:button id="btnview1" Width="50px" Runat="server" Text="View" 
								 onclick="btnview1_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="50px" Runat="server" Text="Excel"  onclick="btnExcel_Click"></asp:button></td>
					</tr>
					<tr height="20">
						<td align="center" colspan="2">Search By
							<asp:dropdownlist id="DropSearchBy" runat="server" CssClass="dropdownlist" onchange="CheckSearchOption(this)">
								<asp:ListItem Value="All">All</asp:ListItem>
								<asp:ListItem Value="Group">Group</asp:ListItem>
								<asp:ListItem Value="SubGroup">SubGroup</asp:ListItem>
								<asp:ListItem Value="District">District</asp:ListItem>
								<asp:ListItem Value="Name">Name</asp:ListItem>
								<asp:ListItem Value="Place">Place</asp:ListItem>
								<asp:ListItem Value="SSR">SSR</asp:ListItem>
							</asp:dropdownlist>
							Value</td>
						<td>
							<input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
								onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnview1)"
								style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 210px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
								value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
								readOnly type="text" name="temp"><br>
							<div id="Layer1" style="Z-INDEX: 4; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnview1)"
									id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnview1,document.Form1.DropValue)"
									style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 230px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
									type="select-one"></select></div>
						</td>
						<td colspan="6">&nbsp;<asp:CheckBox ID="chkFoc" Runat="server" Text="FOC"></asp:CheckBox>&nbsp;</td>
					<tr>
						<td vAlign="top" colSpan="9" align="center">
							<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
								<%if(View==1)
								{
									DBUtil dbobj=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
									DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
									int flag=0;
									SqlDataReader rdr = null,rdr1=null;
									//dbobj.SelectQuery("select c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd from Customer c,sales_master sm,vw_salesoil v where c.cust_id=sm.cust_id and sm.invoice_no=v.invoice_no and cast(floor(cast(invoice_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(txtDateFrom.Text)+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(txtDateTo.Text)+"' group by c.Cust_ID,Cust_Name,state,city,cust_type,sadbhavnacd order by state,sadbhavnacd,cust_name,cust_type,city",ref rdr);
									dbobj.SelectQuery(sql,ref rdr);
									if(rdr.HasRows)
									{
										flag=1;
										%>
								<tr>
									<th colSpan="3">
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
									<th colSpan="1">
										Total</th></tr>
								<tr bgColor="#ce4848">
									<td align="center"><b><font color="white">Sr. No.</font></b></td>
									<td align="center"><b><font color="white">Customer Name</font></b></td>
									<td align="center"><b><font color="white">Place</font></b></td>
									<%
												for(int m=0;m<DateFrom.Length;m++)
												{
													%>
									<td align="center"><b><font color="white">Sales</font></b></td>
									<%
												}
												%>
									<td align="center"><b><font color="white">Total</font></b></td>
								</tr>
								<%
									}
									else
									{
										
										MessageBox.Show("Data Not Available");
									}
									if(flag==1)
									{
										while(rdr.Read())
										{
											%>
								<tr>
									<td><%=i.ToString()%></td>
									<td><%=rdr["Cust_Name"].ToString()%></td>
									<td><%=rdr["City"].ToString()%></td>
									<%
													int k=-1;
													double tot_row=0;
													for(int j=0;j<DateFrom.Length;j++)
													{	
														string Cust_ID=rdr["Cust_ID"].ToString();
														dbobj.SelectQuery("select sum(oil4t) t4 from vw_salesoil v,sales_master sm where sm.invoice_no=v.invoice_no and sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[j].ToString()+"' group by sm.cust_id",ref rdr1);
														if(rdr1.Read())
														{
															%>
									<td align="right"><%=rdr1["t4"].ToString()%><%TotalSum[++k]+=double.Parse(rdr1["t4"].ToString());
															tot_row+=double.Parse(rdr1["t4"].ToString());%></td>
									<%	
														}
														else
														{
															%>
									<td align="right">0</td>
									<%k+=1;
														}
														rdr1.Close();
													}
													%>
									<td align="right"><%=tot_row.ToString()%></td>
								</tr>
								<%	
								i++;
										}
										%>
								<tr bgColor="#ce4848">
									<td align="center" colSpan="3"><font color="white"><b>Total</b></font></td>
									<%
											double total=0;
											for(int j=0;j<TotalSum.Length;j++)
											{
												%>
									<td align="right"><font color="white"><b><%=TotalSum[j].ToString()%><%total+=TotalSum[j];%></b></font></td>
									<%
											}
											%>
									<td align="right"><font color="white"><b><%=total.ToString()%></b></font></td>
								</tr>
							</table>
							<%
									}
								}
								else
								{
									%>
							<table>
								<tr align="center">
									<td align="center" colspan="9"><asp:datagrid id="Grid4tZoom" Runat="server" ShowFooter="True" AutoGenerateColumns="False" AllowSorting="True"
											OnSortCommand="sortcommand_click" OnItemDataBound="ItemTotal">
											<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
											<FooterStyle Font-Bold="True" ForeColor="White" BackColor="#CE4848"></FooterStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="SNo">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
													<ItemTemplate>
														<%=i++%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Cust_Name" SortExpression="Cust_Name" HeaderText="Party Name" FooterText="Total">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
													<FooterStyle Font-Bold="True"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="City" SortExpression="City" HeaderText="Place">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="invoice_no" SortExpression="invoice_no" HeaderText="Invoice No.">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="invoice_date" SortExpression="invoice_date" HeaderText="Invoice Date"
													DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="prod_name" SortExpression="prod_name" HeaderText="Product Name">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="pack_type" SortExpression="pack_type" HeaderText="Pack Type">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Qty_Ltr" SortExpression="Qty_Ltr" HeaderText="Qty Nos.">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="oil4t" SortExpression="oil4t" HeaderText="Qty Ltr.">
													<HeaderStyle Font-Bold="True"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Net_Amount" SortExpression="Net_Amount" HeaderText="Amount">
													<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
													<FooterStyle Font-Bold="True" HorizontalAlign="Center"></FooterStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
								<%
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
