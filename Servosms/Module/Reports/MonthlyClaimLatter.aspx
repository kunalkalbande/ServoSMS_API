<%@ Page language="c#" Inherits="Servosms.Module.Reports.MonthlyClaimLatter" CodeFile="MonthlyClaimLatter.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ import namespace="RMG"%>
<%@ import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Monthly Claim Letter</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<%
		//if(DropMonth.SelectedIndex==0 || DropYear.SelectedIndex==0)
		//{
		%>
			<uc1:header id="Header1" runat="server"></uc1:header>
			<%//}%>
			<table height="290" width="755" align="center" border="0" cellpadding="0" cellspacing="0">
				<TBODY>
					<asp:panel id="panMonth" Runat="server">
  <TR>
    <TH vAlign=top height=20><FONT color=#ce4848>Monthly Claim Letter</FONT> 
      <HR>
    </TH></TR>
  <TR vAlign=top>
    <TD align=center>Select The Claim Month&nbsp;&nbsp; 
<asp:RequiredFieldValidator id=rfv1 Runat="server" InitialValue="Select" ErrorMessage="Please Select The Month First" ControlToValidate="DropMonth">*</asp:RequiredFieldValidator>&nbsp; 
<asp:RequiredFieldValidator id=Requiredfieldvalidator1 Runat="server" InitialValue="Select" ErrorMessage="Please Select The Year First" ControlToValidate="DropYear">*</asp:RequiredFieldValidator>&nbsp; 
<asp:dropdownlist id=DropMonth Runat="server" CssClass="fontstyle" AutoPostBack="False">
									<asp:ListItem Value="Select">Select</asp:ListItem>
									<asp:ListItem Value="January">January</asp:ListItem>
									<asp:ListItem Value="February">February</asp:ListItem>
									<asp:ListItem Value="March">March</asp:ListItem>
									<asp:ListItem Value="April">April</asp:ListItem>
									<asp:ListItem Value="May">May</asp:ListItem>
									<asp:ListItem Value="June">June</asp:ListItem>
									<asp:ListItem Value="July">July</asp:ListItem>
									<asp:ListItem Value="August">August</asp:ListItem>
									<asp:ListItem Value="September">September</asp:ListItem>
									<asp:ListItem Value="October">October</asp:ListItem>
									<asp:ListItem Value="November">November</asp:ListItem>
									<asp:ListItem Value="December">December</asp:ListItem>
								</asp:dropdownlist>
<asp:DropDownList id=DropYear Runat="server" CssClass="fontstyle">
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
								</asp:DropDownList>&nbsp; 
<asp:Button id=btnView runat="server" Text="View" Width="70px"  onclick="btnView_Click"></asp:Button>&nbsp;&nbsp; 
<asp:Button id=btnPrint runat="server" Text="Print" Width="70px" onclick="btnPrint_Click"></asp:Button></TD></TR>
					</asp:panel>
					<%
		InventoryClass obj = new InventoryClass();
		SqlDataReader SqlDtr;
		string str = "select * from organisation";
		SqlDtr = obj.GetRecordSet(str);
		if(DropMonth.SelectedIndex!=0 && DropYear.SelectedIndex!=0)
		{
		if(DropMonth.SelectedIndex!=0 && DropYear.SelectedIndex!=0)
		{
		if(SqlDtr.Read())
		{
			
		%>
					<tr>
						<td align="right"><asp:linkbutton id="btnLinkPrintText" runat="server" onclick="btnLinkPrintText_Click">Print In Text</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:linkbutton id="lnkPrint" runat="server" onclick="LinkButton1_Click">Print In MS Word</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;<A href="MonthlyClaimLatter.aspx">Back</A></td>
					</tr>
					<tr>
						<td>
							<table cellSpacing="0" cellPadding="0" width="90%" border="0">
								<tr>
									<td vAlign="top" colSpan="8"><b><font size="4">
												<%=SqlDtr["DealerName"].ToString().ToUpper()%>
											</font></b>
									</td>
									<td vAlign="middle" align="center" rowSpan="8"><asp:image id="imgLogo" Runat="server" Width="150px" Height="150px"></asp:image></td>
								</tr>
								<tr>
									<td colSpan="8"><b><font size="4">SOLE EXCLUSIVE SERVO STOCKIST(AUTOMOTIVE)</font></b></td>
								</tr>
								<tr>
									<td colSpan="8"><%=SqlDtr["Address"].ToString()+" "+SqlDtr["City"].ToString()%></td>
								</tr>
								<tr>
									<td colSpan="8">Tele. No 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=SqlDtr["PhoneNo"].ToString()%></td>
								</tr>
								<tr>
									<td colSpan="8">Fax No 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=SqlDtr["FaxNo"].ToString()%></td>
								</tr>
								<tr>
									<td colSpan="8">Mobile&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								</tr>
								<tr>
									<td colSpan="8">E-Mail&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=SqlDtr["Email"].ToString()%></td>
								</tr>
								<tr>
									<td colSpan="8">Web 
            Site&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=SqlDtr["Website"].ToString()%></td>
								</tr>
								<tr>
									<td colSpan="9">
										<hr color="#000000" SIZE="2">
									</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">TO,</td>
								</tr>
								<tr>
									<td class="font1" colSpan="5">RETAIL SALES EXECUTIVE</td>
									<td class="font2" colSpan="4">Ref: 
            &nbsp;&nbsp;&nbsp;Claim/SSA-<%=SqlDtr["City"].ToString()%>
										                          /<%=DateToYear(SqlDtr["Acc_Date_From"].ToString())%>
										                          -
										<%=DateToYear(SqlDtr["Acc_Date_To"].ToString())%>
									</td>
								</tr>
								<tr>
									<td class="font1" colSpan="5">INDIAN OIL CORPORATION LIMITED</td>
									<td class="font2" colSpan="4">Dated:&nbsp;&nbsp;<%=DateTime.Now.Day%>
										                          /<%=DateTime.Now.Month%>
										                          /<%=DateTime.Now.Year%></td>
								</tr>
								<tr>
									<td class="font1" colSpan="9"><%=SqlDtr["Div_Office"].ToString()%></td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9"><b>Sub: Secondary Sales Claim For the month of
											<%=DropMonth.SelectedItem.Text%>
											&nbsp;<%=DropYear.SelectedItem.Text%>
										</b>
									</td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9"><b>Dear Sir</b></td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font2" colSpan="9">We are submitting our 
            monthly secondary sales claim for the month of
										<%=DropMonth.SelectedItem.Text%>
										&nbsp;<%=DropYear.SelectedItem.Text%>
										                          and we are also attaching all essential document for approving our 
            claim easier with company rules. All Scheme Given to party as per 
            policy. no PRCN or party bill cancelled in
										<%=DropMonth.SelectedItem.Text%>
										&nbsp;<%=DropYear.SelectedItem.Text%>
									</td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font1" width="10%" colSpan="2"><b>Sr.No.</b></td>
									<td class="font1" width="30%" colSpan="3"><b>Type Of Claim</b></td>
									<td class="font1" width="20%" colSpan="2"><b>Period</b></td>
									<td class="font1" width="20%" colSpan="2"><b>Amount</b></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">1</font></td>
									<!--td class="font2" width="30%" colSpan="3">Secondary Sales Claim</td>
									<td class="font2" width="20%" colSpan="2"><%=DropMonth.SelectedItem.Text%>&nbsp;<%=DropYear.SelectedItem.Text%></td>
									<td class="font2" width="20%" colSpan="2"><%=Claim_Amt%></td-->
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim1" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox id="txtPeriod1" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox id="txtAmount1" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">2</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim2" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove">Fleet Operator Claim</asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox id="txtPeriod2" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox id="txtAmount2" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">3</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim3" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox id="txtPeriod3" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox id="txtAmount3" Runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">4</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim4" Runat="server" Width="100%" BorderStyle="Groove" MaxLength="49"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtPeriod4" Runat="server" Width="100%"
											BorderStyle="Groove" MaxLength="20"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,true);" id="txtAmount4" Runat="server"
											Width="100%" BorderStyle="Groove" MaxLength="10"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">5</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim5" Runat="server" Width="100%" BorderStyle="Groove" MaxLength="49"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtPeriod5" Runat="server" Width="100%"
											BorderStyle="Groove" MaxLength="20"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,true);" id="txtAmount5" Runat="server"
											Width="100%" BorderStyle="Groove" MaxLength="10"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">6</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim6" Runat="server" Width="100%" BorderStyle="Groove" MaxLength="49"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtPeriod6" Runat="server" Width="100%"
											BorderStyle="Groove" MaxLength="20"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,true);" id="txtAmount6" Runat="server"
											Width="100%" BorderStyle="Groove" MaxLength="10"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">7</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim7" Runat="server" Width="100%" BorderStyle="Groove" MaxLength="49"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtPeriod7" Runat="server" Width="100%"
											BorderStyle="Groove" MaxLength="20"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,true);" id="txtAmount7" Runat="server"
											Width="100%" BorderStyle="Groove" MaxLength="10"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font2" width="10%" colSpan="2"><font size="2">8</font></td>
									<td class="font2" width="30%" colSpan="3"><asp:textbox id="txtTypeofclaim8" Runat="server" Width="100%" BorderStyle="Groove" MaxLength="49"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtPeriod8" Runat="server" Width="100%"
											BorderStyle="Groove" MaxLength="20"></asp:textbox></td>
									<td class="font2" width="20%" colSpan="2"><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,true);" id="txtAmount8" Runat="server"
											Width="100%" BorderStyle="Groove" MaxLength="10"></asp:textbox></td>
								</tr>
								<tr>
									<td class="font1" colSpan="7"><b>Total Claim Amount</b></td>
									<!--td class="font2" colSpan="2"><font size=2><b><%=GenUtil.strNumericFormat((double.Parse(Claim_Amt)+double.Parse(OEM_Amt)+double.Parse(Fleet_Amt)).ToString())%></b></font></td-->
									<td class="font2" colSpan="2"><asp:textbox id="txtTotal" Runat="server" Width="100%" BorderStyle="Groove" ></asp:textbox></td>
								</tr>
								<tr>
									<td align="center" colSpan="9"><font size="3"><b><asp:linkbutton id="btnLinkSave" Runat="server" onclick="btnLinkSave_Click">Save</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;<asp:linkbutton id="btnLinkPrint" Runat="server">Print</asp:linkbutton></b></font></td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td colSpan="9">
										<table borderColor="black" cellSpacing="0" cellPadding="5" width="95%" border="1">
											<tr>
												<td>S.No.</td>
												<td>Total Invoice Qty</td>
												<td>Invoice Amount</td>
												<td>Cash Discount</td>
												<td>Trade Discount</td>
												<td>Early Bird</td>
												<td>Free Carton Discount</td>
												<td>Old Rate Diff. Discount</td>
												<td>Total</td>
											</tr>
											<tr>
												<td>1</td>
												<td><%=Invoice_Qty%>&nbsp;Ltr.</td>
												<td><%=Invoice_Amount%></td>
												<td><%=Cash_Dis%></td>
												<td><%=Trade_Dis%></td>
												<td><%=EarlyBird_Dis%></td>
												<td>&nbsp;</td>
												<td>&nbsp;</td>
												<td><%=GenUtil.strNumericFormat((double.Parse(Cash_Dis)+double.Parse(Trade_Dis)+double.Parse(EarlyBird_Dis)).ToString())%></td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>Sec. Claim Amount</td>
												<td>OEM Amount</td>
												<td>Fleet Amount</td>
												<td>IBP Amount</td>
												<td colSpan="3">&nbsp;</td>
												<td>Total Amount</td>
											</tr>
											<tr>
												<td>2</td>
												<td><%=Claim_Amt%></td>
												<td><%=OEM_Amt%></td>
												<td><%=Fleet_Amt%></td>
												<td>0.00</td>
												<td colSpan="3">&nbsp;</td>
												<td><%=GenUtil.strNumericFormat((double.Parse(Claim_Amt)+double.Parse(OEM_Amt)+double.Parse(Fleet_Amt)).ToString())%></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">Enclosed:</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">1) CFA Purchase &amp; SSA Sales Invoice Copies.</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">2) Secondary Sales / Purchase statement.</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">3) Fleet / OE Claim Statement (With Bills Attached)</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">4) Attached Sales figure &amp; Stock Movement, Stock 
										list (Servosms Generated).</td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9">Thanks &amp; Regards,</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9"><b><%=SqlDtr["DealerName"].ToString()%></b></td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td colSpan="9">&nbsp;</td>
								</tr>
								<tr>
									<td class="font1" colSpan="9"><b>(<%=SqlDtr["FoodLicNo"].ToString()%>
											)</b></td>
								</tr>
							</table>
						</td>
					</tr>
					<%
				
		}
		SqlDtr.Close();
		
		}
		else
		{
			MessageBox.Show("Please Select The Year");
		}
		}
		%>
				</TBODY>
			</table>
			<%
		//if(DropMonth.SelectedIndex==0 || DropYear.SelectedIndex==0)
		//{
		%>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<asp:validationsummary id="vs1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<%//}%>
		</form>
	</body>
</HTML>
