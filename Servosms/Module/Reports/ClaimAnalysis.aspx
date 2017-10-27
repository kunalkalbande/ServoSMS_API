<%@ Reference Control="~/HeaderFooter/Header.ascx" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.ClaimAnalysis" CodeFile="ClaimAnalysis.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Claim Analysis Report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
				Visible="False" Width="8px"></asp:textbox>
			<table height="290" width="778" align="center" border="0">
				<TR valign="top" height="20">
					<TH>
						<font color="#ce4848">Claim Analysis Report</font>
						<hr>
					</TH>
				</TR>
				<TR valign="top" height="20">
					<TD align="center" colSpan="1" rowSpan="1">
						<TABLE>
							<TR>
								<TD align="center">&nbsp;&nbsp;Year From&nbsp;&nbsp;</TD>
								<TD><asp:DropDownList ID="DropYearFrom" Runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2005">2005</asp:ListItem>
										<asp:ListItem Value="2006">2006</asp:ListItem>
										<asp:ListItem Value="2007">2007</asp:ListItem>
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
										<asp:ListItem Value="2011">2011</asp:ListItem>
										<asp:ListItem Value="2012">2012</asp:ListItem>
										<asp:ListItem Value="2013">2013</asp:ListItem>
										<asp:ListItem Value="2014">2014</asp:ListItem>
										<asp:ListItem Value="2015">2015</asp:ListItem>
										<asp:ListItem Value="2016">2016</asp:ListItem>
										<asp:ListItem Value="2017">2017</asp:ListItem>
										<asp:ListItem Value="2018">2018</asp:ListItem>
										<asp:ListItem Value="2019">2019</asp:ListItem>
										<asp:ListItem Value="2020">2020</asp:ListItem>
									</asp:DropDownList></TD>
								<TD align="center" colSpan="1" rowSpan="1">To&nbsp;&nbsp;</TD>
								<TD><asp:DropDownList ID="DropYearTo" Runat="server" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2005">2005</asp:ListItem>
										<asp:ListItem Value="2006">2006</asp:ListItem>
										<asp:ListItem Value="2007">2007</asp:ListItem>
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
										<asp:ListItem Value="2011">2011</asp:ListItem>
										<asp:ListItem Value="2012">2012</asp:ListItem>
										<asp:ListItem Value="2013">2013</asp:ListItem>
										<asp:ListItem Value="2014">2014</asp:ListItem>
										<asp:ListItem Value="2015">2015</asp:ListItem>
										<asp:ListItem Value="2016">2016</asp:ListItem>
										<asp:ListItem Value="2017">2017</asp:ListItem>
										<asp:ListItem Value="2018">2018</asp:ListItem>
										<asp:ListItem Value="2019">2019</asp:ListItem>
										<asp:ListItem Value="2020">2020</asp:ListItem>
									</asp:DropDownList></TD>
								<TD><asp:button id="cmdrpt" runat="server" Width="60px" Text="View" 
										 onclick="cmdrpt_Click"></asp:button>&nbsp;&nbsp;<asp:button id="prnButton" runat="server" Width="60px" Text=" Print " 
										 onclick="prnButton_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnExcel" runat="server" Width="60px" Text="Excel" 
										 onclick="btnExcel_Click"></asp:button>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR valign="top">
					<TD align="center" colSpan="1" rowSpan="1">
						<%if(View==1){%>
						<TABLE border="1" borderColor="#deba84" cellpadding=0 cellspacing=0>
							<%InventoryClass obj = new InventoryClass();
							SqlDataReader rdr = null;
							string[] SameMon = {"January"+DropYearFrom.SelectedItem.Text,"February"+DropYearFrom.SelectedItem.Text,"March"+DropYearFrom.SelectedItem.Text,"April"+DropYearFrom.SelectedItem.Text,"May"+DropYearFrom.SelectedItem.Text,"June"+DropYearFrom.SelectedItem.Text,"July"+DropYearFrom.SelectedItem.Text,"August"+DropYearFrom.SelectedItem.Text,"September"+DropYearFrom.SelectedItem.Text,"October"+DropYearFrom.SelectedItem.Text,"November"+DropYearFrom.SelectedItem.Text,"December"+DropYearFrom.SelectedItem.Text};
							string[] DiffMon = {"April"+DropYearFrom.SelectedItem.Text,"May"+DropYearFrom.SelectedItem.Text,"June"+DropYearFrom.SelectedItem.Text,"July"+DropYearFrom.SelectedItem.Text,"August"+DropYearFrom.SelectedItem.Text,"September"+DropYearFrom.SelectedItem.Text,"October"+DropYearFrom.SelectedItem.Text,"November"+DropYearFrom.SelectedItem.Text,"December"+DropYearFrom.SelectedItem.Text,"January"+DropYearTo.SelectedItem.Text,"February"+DropYearTo.SelectedItem.Text,"March"+DropYearTo.SelectedItem.Text};
							int Flag = 0,Count=0;
							double Tot = 0;
							double[] TotalAmt = null;
							ArrayList Header = new ArrayList();
							//if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
							//{
								for(int i=0;i<SameMon.Length;i++)
								{
									if(Flag==1)
									{
										if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
											rdr = obj.GetRecordSet("select count(*) from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
										else
											rdr = obj.GetRecordSet("select count(*) from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
										if(rdr.Read())
										{
											if(int.Parse(rdr.GetValue(0).ToString())>Count)
											{
												Count=0;
												Flag=0;
												Header = new ArrayList();
											}
										}
										rdr.Close();
									}
									if(Flag==0)
									{
										%>
							<!--tr-->
								<%
										if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
											rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
										else
											rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
										if(rdr.HasRows)
										{
											Flag=1;
											
											%>
								<!--td>Month</td-->
								<%
											Header.Add("Month");
											while(rdr.Read())
											{
												Header.Add(rdr["TypeofClaim"].ToString());
												%>
								<!--td align="center"><%=rdr["TypeofClaim"].ToString()%></td-->
								<%Count++;
											}
											Header.Add("Total");
											%>
								<!--td align="center">Total</td-->
								<%
										}
										rdr.Close();
										%>
							<!--/tr-->
							<%
									}
									//else
									//{
									//	break;
									//}
								}
								//MessageBox.Show(Count.ToString());
								%>
								<tr bgColor="#ce4848">
									<%for(int l=0;l<Header.Count;l++){%>
									<td align=center><font color="#ffffff"><%=Header[l].ToString()%></font></td>
									<%}%>
								</tr>
								<%
								int NoofCol=Count;
								TotalAmt = new double[Count+1];
								if(Flag==1)
								{
								if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
								{
								for(int i=0;i<SameMon.Length;i++)
								{
									%>
									<tr>
								<%
									
									rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+SameMon[i].ToString()+"'");
									if(rdr.HasRows)
									{
										Tot=0;Count=-1;
										%>
								<td>&nbsp;<%=SameMon[i].ToString()%></td>
								<%
										//int rowcounter =0;
										while(rdr.Read())
										{
											%>
								<td align="right"><%=GenUtil.strNumericFormat(rdr["Amount"].ToString())%>&nbsp;</td>
								<%Tot+=double.Parse(rdr["Amount"].ToString());
											TotalAmt[++Count]+=double.Parse(rdr["Amount"].ToString());
											//MessageBox.Show(i.ToString()+"::"+Count.ToString()+":"+TotalAmt[Count].ToString());
											//rowcounter++;
										}
										//while(TotalAmt.Length-1>rowcounter){
										while(NoofCol!=Count+1){
										%>
										<td align=right>0.00&nbsp;</td>
										<%Count++;}%>
								<td align="right"><%=GenUtil.strNumericFormat(Tot.ToString())%>&nbsp;</td>
								<%
										TotalAmt[++Count]+=Tot;
									}
									else
									{
										%>
								<td>&nbsp;<%=SameMon[i].ToString()%></td>
								<%
										for(int k=0;k<TotalAmt.Length;k++)
										{
											%>
								<td align="right">0.00&nbsp;</td>
								<%
										}
									}
									rdr.Close();
									%>
							</tr>
							<%}
							}
							else
							{
								for(int i=0;i<DiffMon.Length;i++)
								{
									%>
							<tr>
								<%
									
									rdr = obj.GetRecordSet("select * from ClaimAnalysis where ClaimID='"+DiffMon[i].ToString()+"'");
									if(rdr.HasRows)
									{
										Tot=0;Count=-1;
										//int rowcounter =0;
										%>
								<td>&nbsp;<%=DiffMon[i].ToString()%></td>
								<%
										while(rdr.Read())
										{
											%>
								<td align="right"><%=GenUtil.strNumericFormat(rdr["Amount"].ToString())%>&nbsp;</td>
								<%Tot+=double.Parse(rdr["Amount"].ToString());
											TotalAmt[++Count]+=double.Parse(rdr["Amount"].ToString());
											//MessageBox.Show(i.ToString()+"::"+Count.ToString()+":"+TotalAmt[Count].ToString());
											//rowcounter++;
										}
										while(NoofCol!=Count+1){
										%>
										<td align=right>0.00&nbsp;</td>
										<%Count++;}%>
								<td align="right"><%=GenUtil.strNumericFormat(Tot.ToString())%>&nbsp;</td>
								<%
										TotalAmt[++Count]+=Tot;
									}
									else
									{
										%>
								<td>&nbsp;<%=DiffMon[i].ToString()%></td>
								<%
										for(int k=0;k<TotalAmt.Length;k++)
										{
											%>
								<td align="right">0.00&nbsp;</td>
								<%
										}
									}
									rdr.Close();
									%>
							</tr>
							<%}
							}
							%>
							<tr bgColor="#ce4848">
								<td align="center"><font color="#ffffff">Total</font></td>
								<%for(int j=0;j<TotalAmt.Length;j++){%>
								<td align="right"><font color="#ffffff"><%=GenUtil.strNumericFormat(TotalAmt[j].ToString())%></font>&nbsp;</td>
								<%}%>
							</tr>
							<%}else{MessageBox.Show("Data Not Available");}%>
						</TABLE>
						<%}%>
					</TD>
				</TR>
				<tr>
					<td align="center"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				<tr>
					<td align="right"><A href="javascript:window.print()"></A></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
