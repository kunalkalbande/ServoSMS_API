<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="DBOperations"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ import namespace="RMG"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.Expences_Details" CodeFile="Expences_Details.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Expences Details Report</title> <!--
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
				Visible="False" Width="8px"></asp:textbox><input id="tempCustName" style="WIDTH: 1px" type="hidden" name="tempCustName" runat="server">
			<input id="tempCustType" style="WIDTH: 1px" type="hidden" name="tempCustType" runat="server">
			<input id="tempDistrict" style="WIDTH: 1px" type="hidden" name="tempDistrict" runat="server">
			<input id="tempSSR" style="WIDTH: 1px" type="hidden" name="tempSSR" runat="server">
			<input id="tempPlace" style="WIDTH: 1px" type="hidden" name="tempPlace" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" width="750" align="center">
				<TBODY>
					<TR>
						<TH vAlign="top" colSpan="9" height="20">
							<font color="#ce4848">Expences Details Report</font>
							<hr>
						</TH>
					</TR>
					<tr align="center" height="20">
						<td align="right" width="40%">From
							<asp:textbox id="txtDateFrom" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove"
								CssClass="dropdownlist" Height="21"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td width="20%">To
							<asp:textbox id="txtDateTo" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove"
								CssClass="dropdownlist" Height="21"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td align="left" width="60%"><asp:button id="btnview1" Width="65px" 
								Text="View" Runat="server" onclick="btnview1_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" Width="65px" 
								Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
					</tr>
					<tr>
						<td vAlign="top" align="center" colSpan="9">
							<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1">
								<%if(View==1)
								{
									for(int j=0;j<TotalSum.Length;j++)	
									{
										TotalSum[j]=0;
									}
									DBUtil dbobj1=new DBUtil(System.Configuration.ConfigurationSettings.AppSettings["Servosms"],true);
									int flag=0;
									SqlDataReader rdr = null,rdr1=null;
									//03.06.09 dbobj.SelectQuery(sql,ref rdr);
									InventoryClass obj=new InventoryClass();
									InventoryClass obj1=new InventoryClass();
									rdr=obj.GetRecordSet(sql);
									if(rdr.HasRows)
									{
										flag=1;
										%>
								<tr>
									<th>
										&nbsp;</th>
									<%
											for(int m=0;m<DateFrom.Length;m++)
											{
												%>
									<th>
										<%=GetMonthName(DateFrom[m].ToString())%>
									</th>
									<%
											}
											%>
									<th>
										Grand</th></tr>
								<tr bgColor="#ce4848">
									<td align="center"><b><font color="white">Expences_Name</font></b></td>
									<%
											for(int m=0;m<DateFrom.Length;m++)
											{
												%>
									<td align="center"><b><font color="white">Total</font></b></td>
									<%
											}
											%>
									<td align="center"><b><font color="white">Total</font></b></td>
								</tr>
								<%
									}
									if(flag==1)
									{
										while(rdr.Read())
										{
											/********Add by vikas 01.08.09*********************/
											double tot_temp=0;
											string Cust_ID=rdr["ledger_id"].ToString();
											
											rdr1=obj1.GetRecordSet("select sum(debit_Amount) from accountsledgertable where ledger_id="+Cust_ID+" and cast(floor(cast(entry_date as float)) as datetime)>= '"+GenUtil.str2MMDDYYYY(Request.Form["txtDateFrom"].ToString())+"' and  cast(floor(cast(entry_date as float)) as datetime)<='"+GenUtil.str2MMDDYYYY(Request.Form["txtDateTo"].ToString()+"'"));
											if(rdr1.Read())
											{
												if(rdr1.GetValue(0).ToString()!=null && rdr1.GetValue(0).ToString()!="")
													tot_temp=Convert.ToDouble(rdr1.GetValue(0).ToString());
											}
											rdr1.Close();
											
											if(tot_temp!=0)
											{
												/********End*********************/
											
												double Total_exp=0;
												%>
								<tr>
									<td><%=rdr["ledger_name"].ToString()%></td>
									<%int k=-1;
														for(int j=0;j<DateFrom.Length;j++)
														{	
															//Coment by vikas 01.08.09 string Cust_ID=rdr["ledger_id"].ToString();
															//03.06.09 dbobj.SelectQuery("select sum(debit_Amount) from accountsledgertable where ledger_id=1542 and cast(floor(cast(entry_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(entry_date as float)) as datetime)<='"+DateTo[j].ToString()+"'",ref rdr1); 
															
															rdr1=obj1.GetRecordSet("select sum(debit_Amount) from accountsledgertable where ledger_id="+Cust_ID+" and cast(floor(cast(entry_date as float)) as datetime)>= '"+DateFrom[j].ToString()+"' and  cast(floor(cast(entry_date as float)) as datetime)<='"+DateTo[j].ToString()+"'");
															if(rdr1.Read())
															{
																if(rdr1.GetValue(0).ToString()!=null && rdr1.GetValue(0).ToString()!="")
																{
																	%>
									<td align="right"><%=rdr1.GetValue(0).ToString()%>
										<%TotalSum[++k]+=double.Parse(rdr1.GetValue(0).ToString());%>
									</td>
									<%
																	Total_exp+=double.Parse(rdr1.GetValue(0).ToString());
																}
																else
																{
																	%>
									<td align="right">0</td>
									<%
																}
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
									<td align="right"><%=Total_exp%></td>
								</tr>
								<%
												}
											}
											%>
								<tr bgColor="#ce4848">
									<td align="center"><font color="white"><b>Total</b></font></td>
									<%
													double TotalSum1=0;
													for(int j=0;j<TotalSum.Length;j++)
													{
														%>
									<td align="right"><font color="white"><b><%=TotalSum[j].ToString()%></b></font></td>
									<%
														TotalSum1+=TotalSum[j];
													}
													%>
									<td align="center"><font color="white"><b><%=TotalSum1%></b></font></td>
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
