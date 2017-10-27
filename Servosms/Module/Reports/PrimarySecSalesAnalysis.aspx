<%@ Page language="c#" Inherits="Servosms.Module.Reports.PrimarySecSalesAnalysis" CodeFile="PrimarySecSalesAnalysis.aspx.cs" %>
<%@ Import namespace="RMG"%>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Primary Secondary Sales Analysis Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" cellSpacing="0" cellPadding="0" align="center" border="0" width="778">
				<tr vAlign="top" height="20">
					<td align="center"><b><font color="#ce4848">Primary Secondary Sales Analysis Report</font></b><hr>
					</td>
				</tr>
				<tr height="20">
					<td align="center" colSpan="13">Year From&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="DropYearFrom" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;&nbsp;Year To&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="DropYearTo" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;&nbsp;<asp:button id="btnView"  Height="23"
							Width="70" Text="View" Runat="server" onclick="btnView_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnPrint"  Height="23"
							Width="70" Text="Print" Runat="server" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;&nbsp;<asp:button id="btnExcel"  Height="23"
							Width="70" Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr vAlign="top">
					<td>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1" align="center"
							width="760">
							<%if(View == 1){%>
							<tr bgColor="#ce4848">
								<th rowSpan="2">
									<font color="#ffffff">Month</font></th>
								<th colSpan="6">
									<font color="#ffffff">Primary Sales</font></th>
								<th colSpan="6">
									<font color="#ffffff">Secondary Sales</font></th></tr>
							<tr bgColor="#ce4848">
								<td align="center"><font color="#ffffff">Lube</font></td>
								<td align="center"><font color="#ffffff">2T</font></td>
								<td align="center"><font color="#ffffff">4T</font></td>
								<td align="center"><font color="#ffffff">Greases</font></td>
								<td align="center"><font color="#ffffff">Gen. Oils</font></td>
								<td align="center"><font color="#ffffff">Total</font></td>
								<td align="center"><font color="#ffffff">Lube</font></td>
								<td align="center"><font color="#ffffff">2T</font></td>
								<td align="center"><font color="#ffffff">4T</font></td>
								<td align="center"><font color="#ffffff">Greases</font></td>
								<td align="center"><font color="#ffffff">Gen. Oils</font></td>
								<td align="center"><font color="#ffffff">Total</font></td>
							</tr>
							<%
		//InventoryClass obj=new InventoryClass();
		//SqlDataReader SqlDtr;
		string[] DiffMon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
		string[] SameMon={"Jan."+DropYearFrom.SelectedItem.Text,"Feb."+DropYearFrom.SelectedItem.Text,"Mar."+DropYearFrom.SelectedItem.Text,"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text};
		double[] TotAmt = new double[8];
		//string str="select * from LY_PS_SALE";
		//SqlDtr=obj.GetRecordSet(str);
		int k=0;
		//if(SqlDtr.HasRows)
		//{
		//while(SqlDtr.Read())
		PrimarySales=0;
		SecSales=0;
		if(DropYearFrom.SelectedIndex==DropYearTo.SelectedIndex)
		{
		for(int i=0;i<SameMon.Length;i++)
		{
		TotalPrimarySales=0;
		TotalSecSales=0;
		k=0;
		%>
							<tr>
								<td align="center"><%=SameMon[i].ToString()%></td>
								<td align="right"><%=GetPrimaryLubeSales(SameMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySales("2T",SameMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySales("4T",SameMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySales("Grease",SameMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySalesGen(SameMon[i].ToString())%></td>
								<td align="right"><%=GenUtil.strNumericFormat(TotalPrimarySales.ToString())%></td>
								<td align="right"><%=GetSecLubeSales(SameMon[i].ToString())%></td>
								<td align="right"><%=GetSecSales("2T",SameMon[i].ToString())%></td>
								<td align="right"><%=GetSecSales("4T",SameMon[i].ToString())%></td>
								<td align="right"><%=GetSecSales("Grease",SameMon[i].ToString())%></td>
								<td align="right"><%=GetSecSalesGen(SameMon[i].ToString())%></td>
								<td align="right"><%=GenUtil.strNumericFormat(TotalSecSales.ToString())%></td>
							</tr>
							<%
		PrimarySales+=TotalPrimarySales;
		SecSales+=TotalSecSales;
		TotAmt[k]+=double.Parse(GetPrimarySales("2T",SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetPrimarySales("4T",SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetPrimarySales("Grease",SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetPrimarySalesGen(SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSales("2T",SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSales("4T",SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSales("Grease",SameMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSalesGen(SameMon[i].ToString()));
		}
		%>
							<tr bgColor="#ce4848">
								<td align="center"><b><font color="white">Total</font></b></td>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(PrimarySales.ToString())%></font></td>
								<%for(int p=0;p<4;p++){%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(TotAmt[p].ToString())%></font></td>
								<%}%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(PrimarySales.ToString())%></font></td>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(SecSales.ToString())%></font></td>
								<%for(int p=4;p<TotAmt.Length;p++){%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(TotAmt[p].ToString())%></font></td>
								<%}%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(SecSales.ToString())%></font></td>
							</tr>
							<%
		}
		else
		{
		for(int i=0;i<DiffMon.Length;i++)
		{
		TotalPrimarySales=0;
		TotalSecSales=0;
		k=0;
		%>
							<tr>
								<td align="center"><%=DiffMon[i].ToString()%></td>
								<td align="right"><%=GetPrimaryLubeSales(DiffMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySales("2T",DiffMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySales("4T",DiffMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySales("Grease",DiffMon[i].ToString())%></td>
								<td align="right"><%=GetPrimarySalesGen(DiffMon[i].ToString())%></td>
								<td align="right"><%=GenUtil.strNumericFormat(TotalPrimarySales.ToString())%></td>
								<td align="right"><%=GetSecLubeSales(DiffMon[i].ToString())%></td>
								<td align="right"><%=GetSecSales("2T",DiffMon[i].ToString())%></td>
								<td align="right"><%=GetSecSales("4T",DiffMon[i].ToString())%></td>
								<td align="right"><%=GetSecSales("Grease",DiffMon[i].ToString())%></td>
								<td align="right"><%=GetSecSalesGen(DiffMon[i].ToString())%></td>
								<td align="right"><%=GenUtil.strNumericFormat(TotalSecSales.ToString())%></td>
							</tr>
							<%
		PrimarySales+=TotalPrimarySales;
		SecSales+=TotalSecSales;
		TotAmt[k]+=double.Parse(GetPrimarySales("2T",DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetPrimarySales("4T",DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetPrimarySales("Grease",DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetPrimarySalesGen(DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSales("2T",DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSales("4T",DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSales("Grease",DiffMon[i].ToString()));
		TotAmt[++k]+=double.Parse(GetSecSalesGen(DiffMon[i].ToString()));
		}
		%>
							<tr bgColor="#ce4848">
								<td align="center"><b><font color="white">Total</font></b></td>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(PrimarySales.ToString())%></font></td>
								<%for(int p=0;p<4;p++){%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(TotAmt[p].ToString())%></font></td>
								<%}%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(PrimarySales.ToString())%></font></td>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(SecSales.ToString())%></font></td>
								<%for(int p=4;p<TotAmt.Length;p++){%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(TotAmt[p].ToString())%></font></td>
								<%}%>
								<td align="right"><font color="white"><%=GenUtil.strNumericFormat(SecSales.ToString())%></font></B></td>
							</tr>
							<tr>
								<td colspan="13">&nbsp;<b><font color="red">Note</font>&nbsp;:&nbsp;</b>Gen. Oils 
									is a Category of Maruti oil, Swaraj and Htm.</td>
							</tr>
							<%
		}
		//}
		//else
		//{
		//	MessageBox.Show("Data Not Available");
		//}
		//SqlDtr.Close();
		}%>
						</table>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
