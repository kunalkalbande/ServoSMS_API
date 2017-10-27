<%@ Page language="c#" Inherits="Servosms.Module.Reports.Reward_Report" CodeFile="Reward_Report.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="System.Data.SqlClient" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS : Reward Report</title>
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
					f.texthiddenprod.value=f.tempCustName.value;
				else if(index == 2)
					f.texthiddenprod.value=f.tempPlace.value;
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
			<input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="778" align="center">
				<TBODY>
					<TR height="20">
						<TH align="center">
							<font color="#ce4848">Reward For RO/Bazzar Report</font>&nbsp;
							<hr>
						</TH>
					</TR>
					<tr>
						<td vAlign="top" align="center">
							<TABLE id="Table1">
								<TBODY>
									<tr>
										<td align="right">&nbsp;Search&nbsp;<asp:dropdownlist id="DropSearchBy" Runat="server" CssClass="dropdownlist" AutoPostBack="False" onchange="CheckSearchOption(this);">
												<asp:ListItem Value="Select">All</asp:ListItem>
												<asp:ListItem Value="Customer Name">Customer Name</asp:ListItem>
												<asp:ListItem Value="City">City</asp:ListItem>
												<asp:ListItem Value="Country">District</asp:ListItem>
												<asp:ListItem Value="SSR">SSR</asp:ListItem>
											</asp:dropdownlist></td>
										<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropValue"
												onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnShow)"
												style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 125px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
												value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
												readOnly type="text" name="temp"><br>
											<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnShow)"
													id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnShow,document.Form1.DropValue)"
													style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 145px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
													type="select-one"></select></div>
										</td>
										<td colspan="8">&nbsp;&nbsp;&nbsp;From&nbsp;&nbsp;<asp:textbox id="txtDateFrom" runat="server" Width="70px" BorderStyle="Groove" ReadOnly="True"
												CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A> To
											<asp:textbox id="txtDateTo" runat="server" Width="70px" BorderStyle="Groove" ReadOnly="True"
												CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
													align="absMiddle" border="0"></A>&nbsp;
										</td>
									</tr>
									<tr>
										<td colspan="9" align="center">
											<asp:dropdownlist id="dropType" Runat="server" CssClass="dropdownlist" AutoPostBack="False">
												<asp:ListItem Value="Ro">Ro</asp:ListItem>
												<asp:ListItem Value="Bazzar">Bazzar</asp:ListItem>
											</asp:dropdownlist>&nbsp;&nbsp;
											<asp:button id="btnShow" runat="server" Width="60px" CausesValidation="False" Text="View" 
												 onclick="btnShow_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="BtnPrint" Runat="server" Width="60px" Visible="False" CausesValidation="False"
												Text="Print " onclick="BtnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" Runat="server" Width="60px" CausesValidation="False" Text="Excel"
												 onclick="btnExcel_Click"></asp:button></td>
									</tr>
									<tr>
										<TD align="center" colSpan="9"><font color="#ce4848">Vat Inclusive Invoice Amount</font></TD>
										<td></td>
									</tr>
									<%
									if(flage==1)
									{
										InventoryClass obj2 = new InventoryClass();
										dtr=obj2.GetRecordSet(sql);
										if(dropType.SelectedIndex==0)
										{
											if(dtr.HasRows)
											{
												%>
									<tr>
										<td colSpan="6" align="center">
											<table width="990" borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
												<tr valign="top" bgColor="#ce4848">
													<TH width="5%">
														<font color="#ffffff"><b>SNo.</b></font></TH>
													<TH width="15%">
														<font color="#ffffff"><b>Name</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Sale LY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Sale CY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>2T+4T Sale in CY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>MS+XP Sales CY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>HSD+XM sales CY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>No. of Month Uplifted</b></font></TH>
													<TH width="10%">
														<font color="#ffffff"><b>% Growth in average monthly lube sale **</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>LFR</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>2T+4T to MS Ratio</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>FINAL RATING ON GROWTH</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>LFR/2T MS RATIO RATING</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>CONSISTENCY RATING</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>TOTAL RATING</b></font></TH>
												</tr>
												<%
															try
															{
																InventoryClass obj=new InventoryClass();
																SqlDataReader dtr1;
																int Uplifted=0;
																sql="";
																int i=1;
																double Diff_Sale=0;
																double LYSale=0,CYSale=0,Sale_2t_4t=0;
																double MS=0,HSD=0;
																while(dtr.Read())
																{
																	Uplifted=0;
																	%>
												<tr>
													<td vAlign="top"><%=i.ToString()%></td>
													<td vAlign="top"><%=dtr["cust_name"].ToString()+","+dtr["city"].ToString()%></td>
													<%
																		for(int j=0;j<DateFrom.Length;j++)
																		{	
																			//string Cust_ID=rdr["Cust_ID"].ToString();
																			//dbobj.SelectQuery("select sum(totalqty) total from vw_salesoil v,sales_master sm where sm.invoice_no=v.invoice_no and sm.cust_id='"+Cust_ID+"' and cast(floor(cast(invoice_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(invoice_date as float)) as datetime)<='"+DateTo[j].ToString()+"' group by sm.cust_id",ref rdr1);
																			sql="select * from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+DateFrom[j].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[j].ToString()+"'";
																			dtr1=obj.GetRecordSet(sql);
																			if(dtr1.HasRows)
																			{	
																				Uplifted++;
																			}
																			dtr1.Close();
																		}
																		sql="select sum(quant*total_qty) LY_Sale from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
																		dtr1=obj.GetRecordSet(sql);
																		if(dtr1.HasRows)
																		{
																			while(dtr1.Read())
																			{
																				if(dtr1["LY_Sale"].ToString()!=null && dtr1["LY_Sale"].ToString()!="")
																				{
																					%>
													<td align="right" vAlign="top"><%=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2)%></td>
													<%
																					LYSale=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2);
																				}
																				else
																				{
																					%>
													<td align="right" vAlign="top">0</td>
													<%
																				}
																			}
																		}
																		else
																		{
																			%>
													<td align="right" vAlign="top">0</td>
													<%
																		}
																		dtr1.Close();
																		if(dtr["total_sale"].ToString()!=null && dtr["total_sale"].ToString()!="")
																		{
																			%>
													<td align="right" vAlign="top"><%=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2)%></td>
													<%
																			CYSale=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2);
																		}
																		else
																		{
																			%>
													<td align="right" vAlign="top">0</td>
													<%
																		}
																		sql="select sum(quant*total_qty) tf_Sale from vw_SaleBook where (category like '4t%' or category like '2t%' ) and cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
																		dtr1=obj.GetRecordSet(sql);
																		if(dtr1.HasRows)
																		{
																			while(dtr1.Read())
																			{
																				if(dtr1["tf_Sale"].ToString()!=null && dtr1["tf_Sale"].ToString()!="")
																				{	
																					%>
													<td align="right" vAlign="top"><%=Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2)%></td>
													<%
																					Sale_2t_4t=Math.Round((double.Parse(dtr1["tf_sale"].ToString())/1000),2);
																				}
																				else
																				{
																					%>
													<td align="right" vAlign="top">0</td>
													<%
																					Sale_2t_4t=0;
																				}
																			}
																		}
																		else
																		{
																			Sale_2t_4t=0;
																			%>
													<td align="right" vAlign="top">0</td>
													<%
																		}
																		dtr1.Close();
																		sql="select * from Cust_Sale_MS_HSD where cust_id='"+dtr["cust_id"].ToString()+"'";
																		dtr1=obj.GetRecordSet(sql);
																		if(dtr1.HasRows)
																		{
																			if(dtr1.Read())
																			{
																				MS=double.Parse(dtr1["MS"].ToString());
																				HSD=double.Parse(dtr1["HSD"].ToString());
																				%>
													<td align="right" vAlign="top"><%=MS.ToString()%></td>
													<td align="right" vAlign="top"><%=HSD.ToString()%></td>
													<%
																			}
																		}
																		else
																		{
																			%>
													<td align="right" vAlign="top">0</td>
													<td align="right" vAlign="top">0</td>
													<%
																		}
																		dtr1.Close();
																		%>
													<td align="right" vAlign="top"><%=Uplifted.ToString()%></td>
													<%
																		Diff_Sale=CYSale-LYSale;
																		double G_Lube_Sale=0;
																		if(LYSale!=0)
																		{
																			G_Lube_Sale=Math.Round(((Diff_Sale*100)/LYSale),2);
																		}
																		else
																		{
																			G_Lube_Sale=0;
																		}
																		%>
													<td align="right" vAlign="top"><%=G_Lube_Sale.ToString()%>&nbsp;%</td>
													<%
																		double LFR=0;
																		LFR=Math.Round((CYSale*100)/(MS+HSD),2);
																		%>
													<td align="right" vAlign="top"><%=LFR.ToString()%></td>
													<%
																		double MS_Ratio=0;
																		MS_Ratio=Math.Round(((Sale_2t_4t*100)/MS),2);
																		%>
													<td align="right" vAlign="top"><%=MS_Ratio.ToString()%></td>
													<%
																		double Rat_on_Growth=0;
																		//IF(I2<=10, I2*4/10, (40+(I2-10)*1))
																		if(G_Lube_Sale<=10)
																		{
																			Rat_on_Growth=Math.Round(((G_Lube_Sale*4)/10),2);
																		}
																		else
																		{
																			Rat_on_Growth=Math.Round((40+(G_Lube_Sale-10)*1),2);
																		}
																		double Final_Rat_on_Growth=0;
																		//IF(L2<=45,L2,45)
																		if(Rat_on_Growth<=45)
																		{
																			Final_Rat_on_Growth=Rat_on_Growth;
																		}
																		else
																		{
																			Final_Rat_on_Growth=45;
																		}
																		%>
													<td align="right" vAlign="top"><%=Final_Rat_on_Growth.ToString()%></td>
													<%
																		double LFR_MS_Ratio=0;
																		//IF(J2>K2,J2,K2)
																		if(LFR>MS_Ratio)
																		{
																			LFR_MS_Ratio=LFR;
																		}
																		else
																		{
																			LFR_MS_Ratio=MS_Ratio;
																		}
																		double LFR_MS_Rating=0;
																		//IF(N2<=0.25,0,IF(N2<=0.75,(N2-0.25)*80,IF(N2<=1,40+(N2-0.75)*20,45)))
																		if(LFR_MS_Ratio<=0.25)
																		{
																			LFR_MS_Rating=0;
																		}
																		else if(LFR_MS_Ratio<=0.75)
																		{
																			LFR_MS_Rating=Math.Round(((LFR_MS_Ratio-0.25)*80),2);
																		}
																		else if(LFR_MS_Ratio<=1)
																		{
																			LFR_MS_Rating=Math.Round((40+(LFR_MS_Ratio-0.75)*20),2);
																		}
																		else
																		{
																			LFR_MS_Rating=45;
																		}
																		%>
													<td align="right" vAlign="top"><%=LFR_MS_Rating.ToString()%></td>
													<%
																		double Consistency_Rating=0;
																		//20*H2/12
																		Consistency_Rating=Math.Round(double.Parse(((20*Uplifted)/12).ToString()),2);
																		%>
													<td align="right" vAlign="top"><%=Consistency_Rating.ToString()%></td>
													<%
																		//M2+O2+P2
																		double Tot_Rating=0;
																		Tot_Rating=Math.Round((Final_Rat_on_Growth+LFR_MS_Rating+Consistency_Rating),2);
																		%>
													<td align="right" vAlign="top"><%=Tot_Rating.ToString()%></td>
													<%
																		i++;
																	}
																	dtr.Close();
																}
																catch(Exception ex)
																{
																	//MessageBox.Show(ex.Message.ToString());	
																}
															}
															else
															{
																flage=0;
																MessageBox.Show("Data Not Available");
																
															}
																%>
												</tr>
											</table>
										</td>
									</tr>
									<%
												}
												else
												{
													if(dtr.HasRows)
													{
													%>
									<tr>
										<td colSpan="6" align="center">
											<table width="900" borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
												<tr valign="top" bgColor="#ce4848">
													<TH width="5%">
														<font color="#ffffff"><b>SNo.</b></font></TH>
													<TH width="20%">
														<font color="#ffffff"><b>Name</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Sale LY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Sale CY</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>No. of SKU'S</b></font></TH>
													<TH width="15%">
														<font color="#ffffff"><b>No. Of Month Uplifted More Then 200 Ltr.</b></font></TH>
													<TH width="15%">
														<font color="#ffffff"><b>% Growth in Average Monthly Lube Sale**</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Rating On Growth</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Final Rating On Growth</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Consistency Rating</b></font></TH>
													<TH width="10%">
														<font color="#ffffff"><b>Rating On No of SKU'S</b></font></TH>
													<TH width="5%">
														<font color="#ffffff"><b>Total Rating</b></font></TH>
													<%
																	try
																	{
																		InventoryClass obj=new InventoryClass();
																		SqlDataReader dtr1;
																		int Uplifted=0;
																		sql="";
																		int i=1;
																		double Diff_Sale=0;
																		double LYSale=0,CYSale=0,Sale_2t_4t=0,SKU=0;
																		while(dtr.Read())
																		{
																			Uplifted=0;
																			%>
												<tr>
													<td vAlign="top"><%=i.ToString()%></td>
													<td vAlign="top"><%=dtr["cust_name"].ToString()+","+dtr["city"].ToString()%></td>
													<%
																				for(int j=0;j<DateFrom.Length;j++)
																				{	
																					sql="select * from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and (quant*total_qty )>='200' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+DateFrom[j].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+DateTo[j].ToString()+"'";
																					dtr1=obj.GetRecordSet(sql);
																					if(dtr1.Read())
																					{
																						Uplifted++;
																					}
																					dtr1.Close();	
																				}
																				sql="select sum(quant*total_qty) LY_Sale from vw_SaleBook where cust_id='"+dtr["cust_id"].ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) >= '"+s1.ToString()+"' and cast(floor(cast(invoice_date as float)) as datetime) <= '"+s2.ToString()+"'";
																				dtr1=obj.GetRecordSet(sql);
																				if(dtr1.HasRows)
																				{
																					while(dtr1.Read())
																					{
																						if(dtr1["LY_Sale"].ToString()!=null && dtr1["LY_Sale"].ToString()!="")
																						{
																							%>
													<td align="right" vAlign="top"><%=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2)%></td>
													<%
																							LYSale=Math.Round((double.Parse(dtr1["LY_Sale"].ToString())/1000),2);
																						}
																						else
																						{
																							%>
													<td align="right" vAlign="top">0</td>
													<%
																						}
																					}
																				}
																				else
																				{
																					%>
													<td align="right" vAlign="top">0</td>
													<%
																				}
																				dtr1.Close();
																				if(dtr["total_sale"].ToString()!=null && dtr["total_sale"].ToString()!="")
																				{
																					%>
													<td align="right" vAlign="top"><%=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2)%></td>
													<%
																					CYSale=Math.Round((double.Parse(dtr["total_sale"].ToString())/1000),2);
																				}
																				else
																				{
																					%>
													<td align="right" vAlign="top">0</td>
													<%
																				}
																				SKU=double.Parse(dtr["sku"].ToString());
																				%>
													<td align="right" vAlign="top"><%=dtr["sku"].ToString()%>&nbsp;</td>
													<td align="right" vAlign="top"><%=Uplifted.ToString()%></td>
													<%
																				Diff_Sale=CYSale-LYSale;
																				if(LYSale!=0)
																				{
																					Diff_Sale=Math.Round((Diff_Sale*100)/LYSale);
																				}
																				else
																				{
																					Diff_Sale=0;
																				}
																				%>
													<td align="right" vAlign="top"><%=Diff_Sale.ToString()%>&nbsp;%</td>
													<%
																				double Rat_Growth=0;
																				if(Diff_Sale<=10)
																				{
																					Rat_Growth=Math.Round((Diff_Sale*4/10),2);
																				}
																				else
																				{
																					Rat_Growth=Math.Round((40+(Diff_Sale-10)*1),2);
																				}
																				%>
													<td align="right" vAlign="top"><%=Rat_Growth.ToString()%></td>
													<%
																				double F_Rat_Growth=0;
																				if(Rat_Growth<=45)
																				{
																					F_Rat_Growth=Math.Round(Rat_Growth,2);
																				}
																				else
																				{
																					F_Rat_Growth=45;
																				}
																				%>
													<td align="right" vAlign="top"><%=F_Rat_Growth.ToString()%></td>
													<%
																				double Con_Rating=0;
																				Con_Rating=Math.Round(double.Parse((Uplifted*20/12).ToString()),2);
																				%>
													<td align="right" vAlign="top"><%=Con_Rating.ToString()%></td>
													<%
																				double Rat_SKU=0;
																				if(SKU<=20)
																				{
																					Rat_SKU=0;
																				}
																				else if(SKU<=50)
																				{
																					Rat_SKU=Math.Round(((SKU-20)*1.33),2);
																				}
																				else if(SKU<=60)
																				{
																					Rat_SKU=Math.Round((40+(SKU-50)*0.5),2);
																				}
																				else
																				{
																					Rat_SKU=45;
																				}
																				%>
													<td align="right" vAlign="top"><%=Rat_SKU.ToString()%></td>
													<%
																				double Tot_Rating=0;
																				Tot_Rating=F_Rat_Growth+Rat_SKU+Con_Rating;
																				%>
													<td align="right" vAlign="top"><%=Tot_Rating.ToString()%></td>
													<%
																				i++;
																			}
																			dtr.Close();
																		}
																		catch(Exception ex)
																		{
																			//MessageBox.Show(ex.Message.ToString());	
																		}
																	}
																	else
																	{
																		flage=0;
																		MessageBox.Show("Data Not Available");
																		
																	}
																		%>
												</tr>
											</table>
										</td>
									</tr>
									<%
												}
											}
										%>
								</TBODY>
							</TABLE>
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
