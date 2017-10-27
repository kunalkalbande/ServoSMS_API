<%@ Page language="c#" Inherits="Servosms.Module.Reports.FOC_Management_Report" CodeFile="FOC_Management_Report.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="RMG"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FOC_Management_Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
				f.texthiddenprod.value=f.tempCustName.value;
			else if(index == 3)
				f.texthiddenprod.value=f.tempInvoiceNo.value;
			else if(index == 4)
				f.texthiddenprod.value=f.tempPackType.value;
			else if(index == 5)
				f.texthiddenprod.value=f.tempProductGroup.value;
			else if(index == 6)
				f.texthiddenprod.value=f.tempProdWithPack.value;
			else if(index == 7)
				f.texthiddenprod.value=f.tempSSR.value;
			f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
		}
		else
			f.texthiddenprod.value="";
		document.Form1.DropValue.value="All";
		document.Form1.DropProdName.style.visibility="hidden"
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
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempProductGroup" style="WIDTH: 1px" type="hidden" name="tempProdGroup" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPackType" style="WIDTH: 1px" type="hidden" name="tempPackType" runat="server">
			<input id="tempInvoiceNo" style="WIDTH: 1px" type="hidden" name="tempInvoiceNo" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempProdWithPack" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center">
				<TR height="10">
					<TH align="center">
						<font color="#ce4848">FOC &amp; Non FOC Bill WIse/ Party Wise Details For Foc 
							Management Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center">
						<TABLE width="100%" border="0">
							<TR>
								<TD align="center" width="10%">Date From</TD>
								<TD width="20%"><asp:textbox id="txtDateFrom" runat="server" Width="80px" CssClass="fontstyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD align="center" width="10%">Date To</TD>
								<TD colSpan="3"><asp:textbox id="Textbox1" runat="server" Width="80px" CssClass="fontstyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.Textbox1);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;
									<asp:checkbox id="ChkFoc" Text="Only FOC" Runat="server"></asp:checkbox>
									&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:DropDownList ID="dropsummry" Runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="Summary">Summary</asp:ListItem>
										<asp:ListItem Value="Details">Details</asp:ListItem>
									</asp:DropDownList>
								</TD>
							</TR>
							<tr>
								<TD align="center">Search By</TD>
								<TD align="left"><asp:dropdownlist id="DropSearchBy" runat="server" Width="140px" CssClass="dropdownlist" onchange="CheckSearchOption(this)">
										<asp:ListItem Value="All">All</asp:ListItem>
										<asp:ListItem Value="Customer Category">Customer Category</asp:ListItem>
										<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
										<asp:ListItem Value="Invoice No">Invoice No</asp:ListItem>
										<asp:ListItem Value="Pack Type">Pack Type</asp:ListItem>
										<asp:ListItem Value="Product Group">Product Group</asp:ListItem>
										<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
										<asp:ListItem Value="SSR">SSR</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">Value&nbsp;&nbsp;&nbsp;</TD>
								<TD align="left" colSpan="2"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnShow)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnShow)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnShow,document.Form1.DropValue)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
											type="select-one"></select></div>
								</TD>
								<td colSpan="2"><asp:button id="btnShow" runat="server" Width="60px" Text="View" 
										 onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="BtnPrint" Width="60px" Text="Print " 
										Runat="server" onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;
									<asp:button id="btnExcel" Width="60px" Text="Excel" 
										Runat="server" onclick="btnExcel_Click"></asp:button></td>
							</tr>
						</TABLE>
						<%
						if(flage==1)
						{
						
							if(dropsummry.SelectedIndex==0)
							{
								TotalQty_No=0;
								TotalQty_Ltr=0;
								%>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="100%" border="1">
							<tr bgColor="#ce4848">
								<th>
									<font color="white">SN</font>
								</th>
								<th>
									<font color="white">Cust Name</font></th>
								<th>
									<font color="white">City</font></th>
								<th>
									<font color="white">Invoice No</font></th>
								<th>
									<font color="white">Invoice Date</font></th>
								<th>
									<font color="white">Product Name</font></th>
								<th>
									<font color="white">Product Type</font></th>
								<th>
									<font color="white">Qty in Nos.</font></th>
								<th>
									<font color="white">Qty in Ltr.</font></th></tr>
							<%
											for(int j=0;j<ALFOC.Count;j++)
											{
												string[] arr_value=ALFOC[j].ToString().Split(new char[] {':'});
												%>
							<tr>
								<td><%=i.ToString()%></td>
								<td><%=arr_value[2].ToString()%></td>
								<td><%=arr_value[1].ToString()%></td>
								<td><%=arr_value[0].ToString()%></td>
								<td><%=arr_value[3].ToString()%></td>
								<%
												if((arr_value[5].ToString()!=" ") && (arr_value[5].ToString()!=null))
												{
													%>
								<td><%=arr_value[4].ToString()%></td>
								<%
												}
												else
												{
													%>
								<td><font color="#ce4848"><b><%=arr_value[4].ToString()%></b></font></td>
								<%
												}
												%>
								<%
												if((arr_value[5].ToString()!=" ") && (arr_value[5].ToString()!=null))
												{
													%>
								<td><%=arr_value[5].ToString()%></td>
								<%
												}
												else
												{
													%>
								<td>&nbsp;</td>
								<%
												}
												%>
								<%
												if((arr_value[6].ToString()!=" ") && (arr_value[6].ToString()!=null))
												{
													%>
								<td><%=arr_value[6].ToString()%></td>
								<%
												}
												else
												{
													%>
								<td>&nbsp;</td>
								<%
												}
												%>
								<%
												if((arr_value[7].ToString()!=" ") && (arr_value[7].ToString()!=null))
												{
													%>
								<td><%=arr_value[7].ToString()%></td>
								<%
												}
												else
												{
													%>
								<td>&nbsp;</td>
								<%
												}
												%>
							</tr>
							<%
							
											if((arr_value[6].ToString()!=" ") && (arr_value[6].ToString()!=null))
											{
												TotalQty_No  +=Convert.ToDouble(arr_value[6].ToString());
												TotalQty_Ltr +=Convert.ToDouble(arr_value[7].ToString());
											}
											i++;
										}
										%>
							<tr bgColor="#ce4848">
								<td colSpan="7"><font color="white"><b>Total</b></font></td>
								<td align="right"><font color="white"><b><%=TotalQty_No.ToString()%></b></font></td>
								<td align="right"><font color="white"><b><%=TotalQty_Ltr.ToString()%></b></font></td>
							</tr>
						</table>
						<%
									}
									else
									{
									
									TotalQty_No=0;
								TotalQty_Ltr=0;
								
								TotalQty_No_main=0;
								TotalQty_Ltr_main=0;
								%>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" width="1000" border="1">
							<tr bgColor="#ce4848">
								<th width="2%">
									<font color="white">SN</font></th>
								<th width="20%">
									<font color="white">Cust Name</font></th>
								<th width="5%">
									<font color="white">City</font></th>
								<th width="3%">
									<font color="white">Invoice No</font></th>
								<th width="5%">
									<font color="white">Invoice Date</font></th>
								<th width="20%">
									<font color="white">Product Name</font></th>
								<th width="7%">
									<font color="white">Product Type</font></th>
								<th width="5%">
									<font color="white">Qty Nos.</font></th>
								<th width="5%">
									<font color="white">Qty Ltr.</font></th>
								<th width="15%">
									<font color="white">FOC Product</font></th>
								<th width="7%">
									<font color="white">Type</font></th>
								<th width="5%">
									<font color="white">Qty Nos.</font></th>
								<th width="5%">
									<font color="white">Qty Ltr.</font></th></tr>
							<%
											for(int j=0;j<ALFOC_Details.Count;j++)
											{
												string[] arr_value=ALFOC_Details[j].ToString().Split(new char[] {':'});
												%>
							<tr>
								<td><%=i.ToString()%></td>
								<td><%=arr_value[2].ToString()%></td>
								<td><%=arr_value[1].ToString()%></td>
								<td><%=arr_value[0].ToString()%></td>
								<td><%=arr_value[3].ToString()%></td>
								<%
													if((arr_value[4].ToString()!=" ") && (arr_value[4].ToString()!=null))
													{
														%>
								<td><%=arr_value[4].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td><font color="#ce4848"><b><%=arr_value[4].ToString()%></b></font></td>
								<%
													}
													%>
								<%
													if((arr_value[5].ToString()!=" ") && (arr_value[5].ToString()!=null))
													{
														%>
								<td><%=arr_value[5].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td><font color="#ce4848"><b><%=arr_value[5].ToString()%></b></font></td>
								<%
													}
													%>
								<%
													if((arr_value[6].ToString()!=" ") && (arr_value[6].ToString()!=null))
													{
														%>
								<td><%=arr_value[6].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td><font color="#ce4848"><b><%=arr_value[6].ToString()%></b></font></td>
								<%
													}
													%>
								<%
													if((arr_value[7].ToString()!=" ") && (arr_value[7].ToString()!=null))
													{
														%>
								<td><%=arr_value[7].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td><font color="#ce4848"><b><%=arr_value[7].ToString()%></b></font></td>
								<%
													}
													%>
								<%
													
													if((arr_value[8].ToString()!="") && (arr_value[8].ToString()!=null))
													{
														%>
								<td><%=arr_value[8].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td>&nbsp;</td>
								<%
													}
													%>
								<%	
													if((arr_value[9].ToString()!="") && (arr_value[9].ToString()!=null))
													{
														%>
								<td><%=arr_value[9].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td>&nbsp;</td>
								<%
													}
													%>
								<%
													if((arr_value[10].ToString()!="0") && (arr_value[10].ToString()!=null))
													{
														%>
								<td><%=arr_value[10].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td>&nbsp;</td>
								<%
													}
													%>
								<%
													if((arr_value[11].ToString()!="0") && (arr_value[11].ToString()!=null))
													{
														%>
								<td><%=arr_value[11].ToString()%></td>
								<%
													}
													else
													{
														%>
								<td>&nbsp;</td>
								<%
													}
													%>
							</tr>
							<%
											
												if((arr_value[6].ToString()!=" ") && (arr_value[6].ToString()!=null))
												{
													TotalQty_No  +=Convert.ToDouble(arr_value[6].ToString());
													TotalQty_Ltr +=Convert.ToDouble(arr_value[7].ToString());
												}
								
												if((arr_value[10].ToString()!=" ") && (arr_value[10].ToString()!=null))
												{
													TotalQty_No_main  +=Convert.ToDouble(arr_value[10].ToString());
													TotalQty_Ltr_main +=Convert.ToDouble(arr_value[11].ToString());
												}
												i++;
											}
										%>
							<tr bgColor="#ce4848">
								<td colSpan="7"><font color="white"><b>Total</b></font></td>
								<td align="right"><font color="white"><b><%=TotalQty_No.ToString()%></b></font></td>
								<td align="right"><font color="white"><b><%=TotalQty_Ltr.ToString()%></b></font></td>
								<td align="right"></td>
								<td align="right"></td>
								<td align="right"><font color="white"><b><%=TotalQty_No_main.ToString()%></b></font></td>
								<td align="right"><font color="white"><b><%=TotalQty_Ltr_main.ToString()%></b></font></td>
							</tr>
						</table>
						<%
									}
								}
							%>
					</td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
