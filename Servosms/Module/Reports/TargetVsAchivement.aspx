<%@ Page language="c#" Inherits="Servosms.Module.Reports.TargetVsAchivement" CodeFile="TargetVsAchivement.aspx.cs" %>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Target Vs Achivement Report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" cellSpacing="0" cellPadding="0" align="center" border="0" width="778">
				<tr vAlign="top" height="20">
					<td align="center"><b><font color="#CE4848">Target Vs Achievement Report</font></b><hr>
					</td>
				</tr>
				<tr height="20">
					<td align="center" colSpan="13">Year From&nbsp;<asp:RequiredFieldValidator ID="rfv3" Runat=server ErrorMessage="Please Select From Year" ControlToValidate="DropYearFrom" InitialValue="Select">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
					<asp:dropdownlist id="DropYearFrom" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;&nbsp;Year To&nbsp;<asp:RequiredFieldValidator ID="rfv2" Runat=server ErrorMessage="Please Select To Year" ControlToValidate="DropYearTo" InitialValue="Select">*</asp:RequiredFieldValidator>&nbsp;&nbsp;
						<asp:dropdownlist id="DropYearTo" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;&nbsp;Target SSA&nbsp;&nbsp;&nbsp;<asp:TextBox ID=txtTargetSSA Runat=server BorderStyle=Groove onkeypress="return GetOnlyNumbers(this, event,true,false)" CssClass=dropdownlist Width=110px MaxLength=10></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID=rfv1 Runat=server ErrorMessage="Target SSA Can Not Be Blank" ControlToValidate="txtTargetSSA">*</asp:RequiredFieldValidator>&nbsp;&nbsp;<asp:button id="btnView" Height="23"
							Width="70" Text="View" Runat="server" onclick="btnView_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnPrint" Visible=False  Height="23"
							Width="70" Text="Print" Runat="server" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel" Height="23"
							Width="70" Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr vAlign="top">
					<td>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1" align="center"
							width="778">
							<%if(View == 1){%>
							<tr bgColor="#CE4848">
								<td align=center>
									<font color="#ffffff">FY</font></td>
								<td align=center>
									<font color="#ffffff">Total</font></td>
								<td align=center>
									<font color="#ffffff">Total</font></td>
								<td align=center>
									<font color="#ffffff">Target</font></td>
								<td align=center>
									<font color="#ffffff">Negetive</font></td>
								<td align=center>
									<font color="#ffffff">Target</font></td>
								<td align=center>
									<font color="#ffffff">Target</font></td>
								<td align=center>
									<font color="#ffffff">FY</font></td>
								<td align=center>
									<font color="#ffffff">Total</font></td>
								<td align=center>
									<font color="#ffffff">Total</font></td>
								<td align=center>
									<font color="#ffffff">Target</font></td>
								<td align=center>
									<font color="#ffffff">Negetive</font></td>
								<td align=center>
									<font color="#ffffff">Target</font></td>
								<td align=center>
									<font color="#ffffff">Target</font></td>
								<td colspan="2" align=center>
									<font color="#ffffff">Negetive in Ly Vs Cy</font></td>
							</tr>
							<tr bgColor="#CE4848">
								<td align=center>
									<font color="#ffffff">
										<%=DropYearFrom.SelectedItem.Text%>-<%=DropYearTo.SelectedItem.Text%></font></td>
								<td align=center>
									<font color="#ffffff">Sales</font></td>
								<td align=center>
									<font color="#ffffff">Purch.</font></td>
								<td align=center>
									<font color="#ffffff">SSA</font></td>
								<td align=center>
									<font color="#ffffff">in Target</font></td>
								<td align=center>
									<font color="#ffffff">Ach.</font></td>
								<td align=center>
									<font color="#ffffff">Minus</font></td>
								<td align=center>
									<font color="#ffffff">
										<%=DropYearFrom.SelectedItem.Text%>-<%=DropYearTo.SelectedItem.Text%></font></td>
								<td align=center>
									<font color="#ffffff">Sales</font></td>
								<td align=center>
									<font color="#ffffff">Purch.</font></td>
								<td align=center>
									<font color="#ffffff">SSA</font></td>
								<td align=center>
									<font color="#ffffff">in Target</font></td>
								<td align=center>
									<font color="#ffffff">Ach.</font></td>
								<td align=center>
									<font color="#ffffff">Minus</font></td>
								<td align=center>
									<font color="#ffffff">Sales</font></td>
								<td align=center>
									<font color="#ffffff">Uplft</font></td>
							</tr>
							<tr bgColor="#CE4848">
								<td align="center">&nbsp;</td>
								<td align="center"><font color="#ffffff">Sec.</font></td>
								<td align="center"><font color="#ffffff">Primary</font></td>
								<td align="center">&nbsp;</td>
								<td align="center"><font color="#ffffff">in Ltr</font></td>
								<td align="center"><font color="#ffffff">in %</font></td>
								<td align="center"><font color="#ffffff">in %</font></td>
								<td align="center">&nbsp;</td>
								<td align="center"><font color="#ffffff">Sec.</font></td>
								<td align="center"><font color="#ffffff">Primary</font></td>
								<td align="center">&nbsp;</td>
								<td align="center"><font color="#ffffff">in Ltr</font></td>
								<td align="center"><font color="#ffffff">in %</font></td>
								<td align="center"><font color="#ffffff">in %</font></td>
								<td colspan="2" align="center"><font color="#ffffff">IN LITERS</font></td>
							</tr>
							<%
		string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"Apr."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"May."+DropYearFrom.SelectedItem.Text,"May."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jun."+DropYearFrom.SelectedItem.Text,"Jun."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jul."+DropYearFrom.SelectedItem.Text,"Jul."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Aug."+DropYearFrom.SelectedItem.Text,"Aug."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Sep."+DropYearFrom.SelectedItem.Text,"Sep."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Oct."+DropYearFrom.SelectedItem.Text,"Oct."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Nov."+DropYearFrom.SelectedItem.Text,"Nov."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Dec."+DropYearFrom.SelectedItem.Text,"Dec."+System.Convert.ToString(double.Parse(DropYearFrom.SelectedItem.Text)-1),"Jan."+DropYearTo.SelectedItem.Text,"Jan."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1),"Feb."+DropYearTo.SelectedItem.Text,"Feb."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1),"Mar."+DropYearTo.SelectedItem.Text,"Mar."+System.Convert.ToString(double.Parse(DropYearTo.SelectedItem.Text)-1)};
		
			TotalPrimarySales = 0;
			TotalSecSales = 0;
			TargetSSA = 0;
			TargetLtr = 0;
			TargetAch = 0;
			TargetMinus = 0;
			TargetLtr1 = 0;
			TargetAch1 = 0;
			TargetMinus1 = 0;
			Uplft = 0;
			Sales = 0;
			for(int i=0;i<Mon.Length;i++)
			{
			PrimarySales = 0;
			PrimarySales1 = 0;
			SecSales = 0;
			SecSales1 = 0;
		%>
		<tr>
		<td align=center><%=Mon[i].ToString()%></td>
		<td align="right"><%=GetSecSales(Mon[i].ToString())%></td>
		<td align="right"><%=GetPrimarySales(Mon[i].ToString())%></td>
		<td align="right"><%=txtTargetSSA.Text%></td>
		<td align="right"><%=Math.Round(PrimarySales-double.Parse(txtTargetSSA.Text))%></td>
		<td align="right"><%=Math.Round((PrimarySales/double.Parse(txtTargetSSA.Text))*100)%></td>
		<td align="right"><%=Math.Round(((PrimarySales-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100)%></td>
		<td align=center><%=Mon[++i].ToString()%></td>
		<td align="right"><%=GetSecSales1(Mon[i].ToString())%></td>
		<td align="right"><%=GetPrimarySales1(Mon[i].ToString())%></td>
		<td align="right"><%=txtTargetSSA.Text%></td>
		<td align="right"><%=Math.Round(PrimarySales1-double.Parse(txtTargetSSA.Text))%></td>
		<td align="right"><%=Math.Round((PrimarySales1/double.Parse(txtTargetSSA.Text))*100)%></td>
		<td align="right"><%=Math.Round(((PrimarySales1-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100)%></td>
		<td align=right><%=Math.Round(SecSales-SecSales1)%></td>
		<td align=right><%=Math.Round(PrimarySales-PrimarySales1)%></td>
		</tr>
		<%
		TargetSSA+=double.Parse(txtTargetSSA.Text);
		TargetLtr+=PrimarySales-double.Parse(txtTargetSSA.Text);
		TargetAch+=(PrimarySales/double.Parse(txtTargetSSA.Text))*100;
		TargetMinus+=((PrimarySales-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100;
		TargetLtr1+=PrimarySales1-double.Parse(txtTargetSSA.Text);
		TargetAch1+=(PrimarySales1/double.Parse(txtTargetSSA.Text))*100;
		TargetMinus1+=((PrimarySales1-double.Parse(txtTargetSSA.Text))/double.Parse(txtTargetSSA.Text))*100;
		TotalPrimarySales+=PrimarySales;
		TotalPrimarySales1+=PrimarySales1;
		TotalSecSales+=SecSales;
		TotalSecSales1+=SecSales1;
		Uplft+=PrimarySales-PrimarySales1;
		Sales+=SecSales-SecSales1;
		}
			%>
			<tr bgColor="#CE4848">
			<td align=center><font color=white><b>Total</b></font></td>
			<td align=right><font color=white><%=Math.Round(TotalSecSales)%></font></td>
			<td align=right><font color=white><%=Math.Round(TotalPrimarySales)%></font></td>
			<td align=right><font color=white><%=TargetSSA.ToString()%></font></td>
			<td align=right><font color=white><%=Math.Round(TargetLtr)%></font></td>
			<td align=right><font color=white><%=Math.Round(TargetAch)%></font></td>
			<td align=right><font color=white><%=Math.Round(TargetMinus)%></font></td>
			<td align=center><font color=white><b>Total</b></font></td>
			<td align=right><font color=white><%=Math.Round(TotalSecSales1)%></font></td>
			<td align=right><font color=white><%=Math.Round(TotalPrimarySales1)%></font></td>
			<td align=right><font color=white><%=TargetSSA.ToString()%></font></td>
			<td align=right><font color=white><%=Math.Round(TargetLtr1)%></font></td>
			<td align=right><font color=white><%=Math.Round(TargetAch1)%></font></td>
			<td align=right><font color=white><%=Math.Round(TargetMinus1)%></font></td>
			<td align=right><font color=white><%=Math.Round(Sales)%></font></td>
			<td align=right><font color=white><%=Math.Round(Uplft)%></font></td>
			</tr>
			<%
		
		}%>
						</table>
					</td>
				</tr>
				<tr>
				<td><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
