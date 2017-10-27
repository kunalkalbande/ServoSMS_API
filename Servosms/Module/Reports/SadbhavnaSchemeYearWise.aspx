<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.SadbhavnaSchemeYearWise" CodeFile="SadbhavnaSchemeYearWise.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SadbhavnaSchemeYearWise</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
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
			<table height="290" width="778" align="center">
				<TBODY>
					<TR height="20">
						<TH align="center">
							<!--font color="#CE4848">
								<%=str1%>
							</font><FONT color="#CE4848">
								<br>
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Customer Code:<%=str2%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
								SAP Code:
								<%=str3%>
							</FONT--><font color="#ce4848">Servo Sadbhavna Scheme&nbsp;Year Wise Sales &amp; 
								Points Report For Disbursment Of The&nbsp;Gift</font>&nbsp;
							<hr>
						</TH>
					</TR>
					<tr>
						<td vAlign="top" align="center">
							<TABLE id="Table1">
								<TBODY>
									<tr>
										<td colspan=3>Search By&nbsp;<asp:dropdownlist id="DropSearchBy" Runat="server" CssClass="dropdownlist" AutoPostBack="False" onchange="CheckSearchOption(this);">
												<asp:ListItem Value="Select">All</asp:ListItem>
												<asp:ListItem Value="Customer Group">Customer Group</asp:ListItem>
												<asp:ListItem Value="Customer SubGroup">Customer SubGroup</asp:ListItem>
												<asp:ListItem Value="City">City</asp:ListItem>
												<asp:ListItem Value="Country">District</asp:ListItem>
												<asp:ListItem Value="SSR">SSR</asp:ListItem>
											</asp:dropdownlist></td>
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
										<td>&nbsp;&nbsp;<asp:dropdownlist id="dropyear" runat="server" CssClass="fontstyle">
												<asp:ListItem Value="2000">2000-01</asp:ListItem>
												<asp:ListItem Value="2001">2001-02</asp:ListItem>
												<asp:ListItem Value="2002">2002-03</asp:ListItem>
												<asp:ListItem Value="2003">2003-04</asp:ListItem>
												<asp:ListItem Value="2004">2004-05</asp:ListItem>
												<asp:ListItem Value="2005">2005-06</asp:ListItem>
												<asp:ListItem Value="2006">2006-07</asp:ListItem>
												<asp:ListItem Value="2007">2007-08</asp:ListItem>
												<asp:ListItem Value="2008">2008-09</asp:ListItem>
												<asp:ListItem Value="2009">2009-10</asp:ListItem>
												<asp:ListItem Value="2010">2010-11</asp:ListItem>
												<asp:ListItem Value="2011">2011-12</asp:ListItem>
												<asp:ListItem Value="2012">2012-13</asp:ListItem>
												<asp:ListItem Value="2013">2013-14</asp:ListItem>
												<asp:ListItem Value="2014">2014-15</asp:ListItem>
												<asp:ListItem Value="2015">2015-16</asp:ListItem>
											</asp:dropdownlist>&nbsp;&nbsp;<asp:dropdownlist id="dropType" Runat="server" CssClass="fontstyle">
												<asp:ListItem Value="Existing">Existing</asp:ListItem>
												<asp:ListItem Value="New Report">New Report</asp:ListItem>
											</asp:dropdownlist>&nbsp;&nbsp;&nbsp;
											<asp:button id="btnShow" runat="server" Width="60px" CausesValidation="False" Text="View" 
												 onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Runat="server" Width="60px" CausesValidation="False" Text="Print "
												 onclick="BtnPrint_Click"></asp:button>&nbsp;<asp:button id="btnExcel" Runat="server" Width="60px" CausesValidation="False" Text="Excel"
												 onclick="btnExcel_Click"></asp:button>
												&nbsp;<asp:CheckBox ID="chkboxNew" Runat="server" Text="All"></asp:CheckBox>
										</td>
										
									</tr>
									<tr>
										<TD align="center" colSpan="9"><font color="#ce4848">Vat Inclusive Invoice Amount</font></TD>
										<td></td>
									</tr>
									<%
									if(flage==1)
									{
									if(dropType.SelectedIndex==0)
									{
										
										%>
									<TR>
										<TD align="center" colSpan="9"><asp:datagrid id="GridSalesReport" runat="server" BorderColor="#DEBA84" BackColor="#DEBA84" ShowFooter="True"
												CellPadding="1" BorderWidth="0px" BorderStyle="None" AutoGenerateColumns="False" CellSpacing="1" AllowSorting="True">
												<SelectedItemStyle Font-Bold="True" ForeColor="White" VerticalAlign="Top" BackColor="#738A9C"></SelectedItemStyle>
												<AlternatingItemStyle VerticalAlign="Top"></AlternatingItemStyle>
												<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
												<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
												<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
												<Columns>
													<asp:BoundColumn DataField="s1" SortExpression="s1" HeaderText="Customer Name" FooterText="Total:"
														FooterStyle-Font-Bold="True"></asp:BoundColumn>
													<asp:BoundColumn DataField="s2" SortExpression="s2" HeaderText="Place"></asp:BoundColumn>
													<asp:BoundColumn DataField="s3" SortExpression="s3" HeaderText="Customer&lt;br&gt; Category"></asp:BoundColumn>
													<asp:BoundColumn DataField="s4" HeaderText="UniqueCode"></asp:BoundColumn>
													<asp:BoundColumn HeaderText="StartsUP&lt;br&gt;Points"></asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Apr">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"4" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check4%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="May">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"5" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check5%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="June">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"6" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check6%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="July">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"7" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check7%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Aug">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"8" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check8%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Sept">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"9" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check9%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Oct">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"10" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check10%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Nov">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"11" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check11%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Dec">
														<ItemTemplate>
															<%#aprpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"12" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check12%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Jan">
														<ItemTemplate>
															<%#janpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"1" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check1%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Feb">
														<ItemTemplate>
															<%#janpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"2" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check2%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Mar">
														<ItemTemplate>
															<%#janpt(DataBinder.Eval(Container.DataItem,"s5").ToString(),"3" )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=check3%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Point&lt;br&gt;Comm">
														<ItemTemplate>
															<%#points(DataBinder.Eval(Container.DataItem,"s5").ToString() )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=totalpoints%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Comm.&lt;br&gt;Sale">
														<ItemTemplate>
															<%#liter(DataBinder.Eval(Container.DataItem,"s5").ToString() )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=GenUtil.strNumericFormat(totliter.ToString())%>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Point&lt;br&gt;Xtra Ach"></asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Points&lt;br&gt;xtra Regu"></asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Points&lt;br&gt;Total">
														<ItemTemplate>
															<%#points_new(DataBinder.Eval(Container.DataItem,"s5").ToString() )%>
														</ItemTemplate>
														<FooterStyle Font-Bold="True"></FooterStyle>
														<FooterTemplate>
															<%=totalpoints_new%>
														</FooterTemplate>
													</asp:TemplateColumn>
												</Columns>
												<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
											</asp:datagrid></TD>
									</TR>
									<%
									}
									else
									{
										try
										{
											if(dtr.HasRows)
											{
												%>
												<tr>
													<td colSpan="6">
														<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
															<tr bgColor="#ce4848">
																<TH><font color="#ffffff"><b>SNo.</b></font></TH>
																<TH><font color="#ffffff"><b>Name</b></font></TH>
																<TH><font color="#ffffff"><b>UC</b></font></TH>
																<TH><font color="#ffffff"><b>Type</b></font></TH>
																<TH>&nbsp;</TH>
																<TH><font color="#ffffff"><b>APR</b></font></TH>
																<TH><font color="#ffffff"><b>MAY</b></font></TH>
																<TH><font color="#ffffff"><b>JUNE</b></font></TH>
																<TH><font color="#ffffff"><b>JULY</b></font></TH>
																<TH><font color="#ffffff"><b>AUG</b></font></TH>
																<TH><font color="#ffffff"><b>SEPT</b></font></TH>
																<TH><font color="#ffffff"><b>OCT</b></font></TH>
															<TH><font color="#ffffff"><b>NOV</b></font></TH>
															<TH><font color="#ffffff"><b>DEC</b></font></TH>
															<TH><font color="#ffffff"><b>JAN</b></font></TH>
															<TH><font color="#ffffff"><b>FEB</b></font></TH>
															<TH><font color="#ffffff"><b>MAR</b></font></TH>
															<TH><font color="#ffffff"><b>Total</b></font></TH>
															<TH><font color="#ffffff"><b>SUP</b></font></TH>
															<TH><font color="#ffffff"><b>BAP</b></font></TH>
															<TH><font color="#ffffff"><b>BNP</b></font></TH>
															<TH><font color="#ffffff"><b>XP</b></font></TH>
															<TH><font color="#ffffff"><b>TP</b></font></TH></tr>
														<%
														
													
															int i=1;
															double Tot_Xtra_Points=0;
															double Tot_Basic_Points=0;
															double Tot_Grand=0;
															double Tot_Net=0;
														
															while(dtr.Read())
															{
																Tot_Xtra_Points=0;
																Tot_Basic_Points=0;
																%>
																<tr>
																	<td vAlign="top" rowSpan="4"><%=i.ToString()%></td>
																	<td vAlign="top" rowSpan="4"><%=dtr["s1"].ToString()+","+dtr["s2"].ToString()%></td>
																	<td vAlign="top" rowSpan="4"><%=dtr["s4"].ToString()%></td>
																	<td vAlign="top" rowSpan="4"><%=dtr["s3"].ToString()%></td>
																	<td>BAP</td>
																	<td align="right"><%CBPoints4=aprpt(dtr["s5"].ToString(),"4");
																		Tot_CBPoints+=CBPoints4;
																		Tot_CBPoints4=Tot_CBPoints;
																		%><%=CBPoints4%></td>
																	<td align="right"><%CBPoints5=aprpt(dtr["s5"].ToString(),"5");
																		Tot_CBPoints+=CBPoints5;
																		Tot_CBPoints5=Tot_CBPoints;
																		%><%=CBPoints5%></td>
																	<td align="right"><%CBPoints6=aprpt(dtr["s5"].ToString(),"6");
																		Tot_CBPoints+=CBPoints6;
																		Tot_CBPoints6=Tot_CBPoints;
																		if(Tot_CBPoints>=25000)
																		{
																			Bonus_Points6=CBPoints6*0.1;
																		}
																		else if(Tot_CBPoints>=12000)
																		{
																			Bonus_Points6=CBPoints6*0.05;
																		}
																		else if(Tot_CBPoints>=5000)
																		{
																			Bonus_Points6=CBPoints6*0.03;
																		}
																		else
																		{
																			Bonus_Points6=0;
																		}
																		Min.Clear();
																		Min.Add(CBPoints4);
																		Min.Add(CBPoints5);
																		Min.Add(CBPoints6);
																		Min.Sort();
																		if(int.Parse(Min[0].ToString())>=1000)
																		{
																			Xtra_Points6=CBPoints6*0.08;
																		}
																		else if(int.Parse(Min[0].ToString())>=500)
																		{
																			Xtra_Points6=CBPoints6*0.04;
																		}
																		else if(int.Parse(Min[0].ToString())>=100)
																		{
																			Xtra_Points6=CBPoints6*0.02;
																		}
																		else
																		{
																			Xtra_Points6=0;
																		}
																		Tot_Basic_Points+=Bonus_Points6;
																		Tot_Xtra_Points+=Xtra_Points6;
																		%><%=CBPoints6%></td>
													<td align="right"><%CBPoints7=aprpt(dtr["s5"].ToString(),"7");
																		Tot_CBPoints+=CBPoints7;
																		Tot_CBPoints7=Tot_CBPoints;
																		if(Tot_CBPoints>=25000)
																		{
																			Bonus_Points7=CBPoints7*0.1;
																		}
																		else if(Tot_CBPoints>=12000)
																		{
																			Bonus_Points7=CBPoints7*0.05;
																		}
																		else if(Tot_CBPoints>=5000)
																		{
																			Bonus_Points7=CBPoints7*0.03;
																		}
																		else
																		{
																			Bonus_Points7=0;
																		}
																		Min.Clear();
																		Min.Add(CBPoints5);
																		Min.Add(CBPoints6);
																		Min.Add(CBPoints7);
																		Min.Sort();
																		if(int.Parse(Min[0].ToString())>=1000)
																		{
																			Xtra_Points7=CBPoints7*0.08;
																		}
																		else if(int.Parse(Min[0].ToString())>=500)
																		{
																			Xtra_Points7=CBPoints7*0.04;
																		}
																		else if(int.Parse(Min[0].ToString())>=100)
																		{
																			Xtra_Points7=CBPoints7*0.02;
																		}
																		else
																		{
																			Xtra_Points7=0;
																		}
																		Tot_Basic_Points+=Bonus_Points7;
																		Tot_Xtra_Points+=Xtra_Points7;%><%=CBPoints7%></td>
													<td align="right"><%CBPoints8=aprpt(dtr["s5"].ToString(),"8");
																	Tot_CBPoints+=CBPoints8;
																	Tot_CBPoints8=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points8=CBPoints8*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points8=CBPoints8*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points8=CBPoints8*0.03;
																	}
																	else
																	{
																		Bonus_Points8=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints6);
																	Min.Add(CBPoints7);
																	Min.Add(CBPoints8);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points8=CBPoints8*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points8=CBPoints8*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points8=CBPoints8*0.02;
																	}
																	else
																	{
																		Xtra_Points8=0;
																	}
																	Tot_Basic_Points+=Bonus_Points8;
																	Tot_Xtra_Points+=Xtra_Points8;
																	%><%=CBPoints8%></td>
													<td align="right"><%CBPoints9=aprpt(dtr["s5"].ToString(),"9");
																	Tot_CBPoints+=CBPoints9;
																	Tot_CBPoints9=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points9=CBPoints9*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points9=CBPoints9*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points9=CBPoints9*0.03;
																	}
																	else
																	{
																		Bonus_Points9=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints7);
																	Min.Add(CBPoints8);
																	Min.Add(CBPoints9);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points9=CBPoints9*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points9=CBPoints9*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points9=CBPoints9*0.02;
																	}
																	else
																	{
																		Xtra_Points9=0;
																	}
																	Tot_Basic_Points+=Bonus_Points9;
																	Tot_Xtra_Points+=Xtra_Points9;%><%=CBPoints9%></td>
													<td align="right"><%CBPoints10=aprpt(dtr["s5"].ToString(),"10");
																	Tot_CBPoints+=CBPoints10;
																	Tot_CBPoints10=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points10=CBPoints10*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points10=CBPoints10*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points10=CBPoints10*0.03;
																	}
																	else
																	{
																		Bonus_Points10=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints8);
																	Min.Add(CBPoints9);
																	Min.Add(CBPoints10);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points10=CBPoints10*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points10=CBPoints10*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points10=CBPoints10*0.02;
																	}
																	else
																	{
																		Xtra_Points10=0;
																	}
																	Tot_Basic_Points+=Bonus_Points10;
																	Tot_Xtra_Points+=Xtra_Points10;
																	%><%=CBPoints10%></td>
													<td align="right"><%CBPoints11=aprpt(dtr["s5"].ToString(),"11");
																	Tot_CBPoints+=CBPoints11;
																	Tot_CBPoints11=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points11=CBPoints11*0.1;
																	}		
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points11=CBPoints11*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{	
																		Bonus_Points11=CBPoints11*0.03;
																	}
																	else
																	{
																		Bonus_Points11=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints9);
																	Min.Add(CBPoints10);
																	Min.Add(CBPoints11);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points11=CBPoints11*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points11=CBPoints11*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points11=CBPoints11*0.02;
																	}
																	else
																	{
																		Xtra_Points11=0;
																	}
																	Tot_Basic_Points+=Bonus_Points11;
																	Tot_Xtra_Points+=Xtra_Points11;
																	%><%=CBPoints11%></td>
													<td align="right"><%CBPoints12=aprpt(dtr["s5"].ToString(),"12");
																	Tot_CBPoints+=CBPoints12;
																	Tot_CBPoints12=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points12=CBPoints12*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points12=CBPoints12*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points12=CBPoints12*0.03;
																	}
																	else
																	{
																		Bonus_Points12=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints10);
																	Min.Add(CBPoints11);
																	Min.Add(CBPoints12);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points12=CBPoints12*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points12=CBPoints12*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points12=CBPoints12*0.02;
																	}
																	else
																	{
																		Xtra_Points12=0;
																	}
																	Tot_Basic_Points+=Bonus_Points12;
																	Tot_Xtra_Points+=Xtra_Points12;%><%=CBPoints12%></td>
													<td align="right"><%CBPoints1=janpt(dtr["s5"].ToString(),"1");
																	Tot_CBPoints+=CBPoints1;
																	Tot_CBPoints1=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points1=CBPoints1*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points1=CBPoints1*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points1=CBPoints1*0.03;
																	}
																	else
																	{
																		Bonus_Points1=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints11);
																	Min.Add(CBPoints12);
																	Min.Add(CBPoints1);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points1=CBPoints1*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points1=CBPoints1*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points1=CBPoints1*0.02;
																	}
																	else
																	{
																		Xtra_Points1=0;
																	}
																	Tot_Basic_Points+=Bonus_Points1;
																	Tot_Xtra_Points+=Xtra_Points1;
																	%><%=CBPoints1%></td>
													<td align="right"><%CBPoints2=janpt(dtr["s5"].ToString(),"2");
																	Tot_CBPoints+=CBPoints2;
																	Tot_CBPoints2=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points2=CBPoints2*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points2=CBPoints2*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points2=CBPoints2*0.03;
																	}
																	else
																	{
																		Bonus_Points2=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints12);
																	Min.Add(CBPoints1);
																	Min.Add(CBPoints2);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points2=CBPoints2*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points2=CBPoints2*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points2=CBPoints2*0.02;
																	}
																	else
																	{
																		Xtra_Points2=0;
																	}
																	Tot_Basic_Points+=Bonus_Points2;
																	Tot_Xtra_Points+=Xtra_Points2;
																	%><%=CBPoints2%></td>
													<td align="right"><%CBPoints3=janpt(dtr["s5"].ToString(),"3");
																	Tot_CBPoints+=CBPoints3;
																	Tot_CBPoints3=Tot_CBPoints;
																	if(Tot_CBPoints>=25000)
																	{
																		Bonus_Points3=CBPoints3*0.1;
																	}
																	else if(Tot_CBPoints>=12000)
																	{
																		Bonus_Points3=CBPoints3*0.05;
																	}
																	else if(Tot_CBPoints>=5000)
																	{
																		Bonus_Points3=CBPoints3*0.03;
																	}
																	else
																	{
																		Bonus_Points3=0;
																	}
																	Min.Clear();
																	Min.Add(CBPoints1);
																	Min.Add(CBPoints2);
																	Min.Add(CBPoints3);
																	Min.Sort();
																	if(int.Parse(Min[0].ToString())>=1000)
																	{
																		Xtra_Points3=CBPoints3*0.08;
																	}
																	else if(int.Parse(Min[0].ToString())>=500)
																	{
																		Xtra_Points3=CBPoints3*0.04;
																	}
																	else if(int.Parse(Min[0].ToString())>=100)
																	{
																		Xtra_Points3=CBPoints3*0.02;
																	}
																	else
																	{
																		Xtra_Points3=0;
																	}
																	Tot_Basic_Points+=Bonus_Points3;
																	Tot_Xtra_Points+=Xtra_Points3;
																	%><%=CBPoints3%></td>
													<td vAlign="top" align="right" rowSpan="4"><%=points(dtr["s5"].ToString())%></td>
													<td vAlign="top" align="right" rowSpan="4">&nbsp;</td>
													<td vAlign="top" align="right" rowSpan="4"><%=points(dtr["s5"].ToString())%></td>
													<td vAlign="top" align="right" rowSpan="4"><%=Tot_Basic_Points%></td>
													<td vAlign="top" align="right" rowSpan="4"><%=Tot_Xtra_Points%></td>
													<%
																double G_tot=Tot_Basic_Points+Tot_Xtra_Points+double.Parse(points(dtr["s5"].ToString()).ToString());
																Tot_Grand+=double.Parse(points(dtr["s5"].ToString()).ToString());
																Tot_Net+=G_tot;
																%>
													<td vAlign="top" align="right" rowSpan="4"><%=G_tot.ToString()%></td>
													<%Tot_CBPoints=0;%>
												</tr>
												<tr>
													<td>CBP</td>
													<td align="right"><%=Tot_CBPoints4.ToString()%></td>
													<td align="right"><%=Tot_CBPoints5.ToString()%></td>
													<td align="right"><%=Tot_CBPoints6.ToString()%></td>
													<td align="right"><%=Tot_CBPoints7.ToString()%></td>
													<td align="right"><%=Tot_CBPoints8.ToString()%></td>
													<td align="right"><%=Tot_CBPoints9.ToString()%></td>
													<td align="right"><%=Tot_CBPoints10.ToString()%></td>
													<td align="right"><%=Tot_CBPoints11.ToString()%></td>
													<td align="right"><%=Tot_CBPoints12.ToString()%></td>
													<td align="right"><%=Tot_CBPoints1.ToString()%></td>
													<td align="right"><%=Tot_CBPoints2.ToString()%></td>
													<td align="right"><%=Tot_CBPoints3.ToString()%></td>
												</tr>
												<tr>
													<td>BNP</td>
													<td>&nbsp;</td>
													<td>&nbsp;</td>
													<td align="right"><%=Bonus_Points6.ToString()%></td>
													<td align="right"><%=Bonus_Points7.ToString()%></td>
													<td align="right"><%=Bonus_Points8.ToString()%></td>
													<td align="right"><%=Bonus_Points9.ToString()%></td>
													<td align="right"><%=Bonus_Points10.ToString()%></td>
													<td align="right"><%=Bonus_Points11.ToString()%></td>
													<td align="right"><%=Bonus_Points12.ToString()%></td>
													<td align="right"><%=Bonus_Points1.ToString()%></td>
													<td align="right"><%=Bonus_Points2.ToString()%></td>
													<td align="right"><%=Bonus_Points3.ToString()%></td>
												</tr>
												<tr>
													<td>XP</td>
													<td>&nbsp;</td>
													<td>&nbsp;</td>
													<td align="right"><%=Xtra_Points6.ToString()%></td>
													<td align="right"><%=Xtra_Points7.ToString()%></td>
													<td align="right"><%=Xtra_Points8.ToString()%></td>
													<td align="right"><%=Xtra_Points9.ToString()%></td>
													<td align="right"><%=Xtra_Points10.ToString()%></td>
													<td align="right"><%=Xtra_Points11.ToString()%></td>
													<td align="right"><%=Xtra_Points12.ToString()%></td>
													<td align="right"><%=Xtra_Points1.ToString()%></td>
													<td align="right"><%=Xtra_Points2.ToString()%></td>
													<td align="right"><%=Xtra_Points3.ToString()%></td>
												</tr>
												<%
														i++;
													}
													dtr.Close();
													
													%>
												<tr bgColor="#ce4848">
													<TH colspan="17">
														<font color="#ffffff"><b>Total</b></font></TH>
													<TH>
														<font color="#ffffff"><b>
																<%=Tot_Grand.ToString()%>
															</b></font>
													</TH>
													<TH>
													</TH>
													<TH>
													</TH>
													<TH>
													</TH>
													<TH>
													</TH>
													<TH><font color="#ffffff"><b>
																<%=Tot_Net.ToString()%>
															</b></font>
													</TH>
												</tr>
											</table>
										</td>
									</tr>
									<%
									}
									}
											catch(Exception ex)
											{
													//MessageBox.Show(ex.Message.ToString());	
											}
									}
								}
									%>
								</TBODY>
							</TABLE>
						</td>
					</tr>
				</TBODY>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
