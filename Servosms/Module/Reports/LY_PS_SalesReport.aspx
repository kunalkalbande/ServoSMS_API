<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Import namespace="DBOperations" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.LY_PS_SalesReport" CodeFile="LY_PS_SalesReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>LY_PS_SalesReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<LINK rel="stylesheet" type="text/css" href="../../Sysitem/Styles.css">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290px" width="778" cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr vAlign="top" height="20">
					<td align="center" colSpan="23"><b><font color="#CE4848">LAST YEAR PRIMARY / SECONDARY 
								SALES DATA UPDATION FORM</font></b><hr>
					</td>
				</tr>
				<tr vAlign="top">
					<td colSpan="23">&nbsp;&nbsp;YearFrom&nbsp;&nbsp;<asp:dropdownlist id="DropYearFrom" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;YearTo&nbsp;&nbsp;<asp:dropdownlist id="DropYearTo" Runat="server" CssClass="dropdownlist">
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
						</asp:dropdownlist>&nbsp;&nbsp;Discription&nbsp;&nbsp;<asp:TextBox ID=txtDiscription Runat=server CssClass=dropdownlist MaxLength=49 Width="200px" BorderStyle=Groove onkeypress="return GetAnyNumber(this, event);"></asp:TextBox>
							&nbsp;<asp:RadioButton ID=RadSummarized Runat=server Text=Summarized GroupName=Radio></asp:RadioButton>
							&nbsp;<asp:RadioButton ID="RadDetails" Runat=server Text=Details GroupName=Radio Checked=True></asp:RadioButton>
						&nbsp;&nbsp;<asp:button id="btnView"  Width="65"
							Text="View" Runat="server" onclick="btnView_Click"></asp:button>&nbsp;
						<asp:button id="btnSubmit"  Width="65"
							Text="Submit" Runat="server" Visible=False></asp:button></td>
				</tr>
				<tr>
					<td>
						<table borderColor="#deba84" cellSpacing="0" cellPadding="0" border="1" align=center>
							<%if(View==1){%>
							<tr bgColor="#CE4848">
								<%if(RadDetails.Checked){%>
								<th rowSpan="3">
								<%}else{%>
								<th rowSpan="2">
								<%}%>
									<font color="#ffffff">Month</font></th>
								<%if(RadDetails.Checked){%>
								<th colSpan="5">
								<%}else{%>
								<th colSpan="1">
								<%}%>
									<font color="#ffffff">Primary Sales</font></th>
								<%if(RadDetails.Checked){%>
								<th colSpan="30">
								<%}else{%>
								<th colSpan="1">
								<%}%>
									<font color="#ffffff">Secondary Sales</font></th></tr>
			<%
			InventoryClass obj=new InventoryClass();
			SqlDataReader SqlDtr;
			string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
			int count=7;
			if(RadDetails.Checked)
			{
			%>
							<tr bgColor="#CE4848">
								<td align="center" rowSpan="2"><font color="#ffffff">Purchase</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Purchase<br>
										FOC</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Genuine<br>
										Oils</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Greases</font></td>
								<td align="center" rowSpan="2"><font color="#ffffff">Total<br>Purchase</font></td>
								<%
			//int count=5;
			ArrayList arrstr = new ArrayList();
			//string str="select distinct case when customertypename like 'oe%' then 'Oe' else customertypename end as customertypename from customertype order by customertypename";
			string str="select distinct custtype,custtypeid from tempcustomertype order by custtypeid";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.HasRows)
			{
				while(SqlDtr.Read())
				{
					arrstr.Add(SqlDtr.GetValue(0).ToString());
					if(SqlDtr.GetValue(0).ToString().ToLower().StartsWith("ro") || SqlDtr.GetValue(0).ToString().ToLower().StartsWith("bazzar") || SqlDtr.GetValue(0).ToString().ToLower().StartsWith("bazar"))
					{%>
						<td align="center" colSpan="2"><font color="#ffffff"><%=SqlDtr.GetValue(0).ToString()%></font></td>
					<%
					}
					else
					{%>
						<td align="center" rowspan="2"><font color="#ffffff"><%=SqlDtr.GetValue(0).ToString()%></font></td>
					<%
					}
				}
				%>
				<td align="center" rowspan=2><font color="#ffffff">Total<br>Sales</font></td>
				<%
			}
			SqlDtr.Close();
			%>
			</tr>
			<tr bgColor="#CE4848">
			<%for(int n=0;n<arrstr.Count;n++)
			{
				if(arrstr[n].ToString().ToLower().StartsWith("ro") || arrstr[n].ToString().ToLower().StartsWith("bazzar") || arrstr[n].ToString().ToLower().StartsWith("bazar"))
				{count+=2;%>
								<td align="center"><font color="#ffffff">Lube</font></td>
								<td align="center"><font color="#ffffff">2T/4T</font></td>
								<%}else{count++;}
			}
           %>
							</tr>
							<%
			//string[] Mon={"Apr."+DropYearFrom.SelectedItem.Text,"May."+DropYearFrom.SelectedItem.Text,"Jun."+DropYearFrom.SelectedItem.Text,"Jul."+DropYearFrom.SelectedItem.Text,"Aug."+DropYearFrom.SelectedItem.Text,"Sep."+DropYearFrom.SelectedItem.Text,"Oct."+DropYearFrom.SelectedItem.Text,"Nov."+DropYearFrom.SelectedItem.Text,"Dec."+DropYearFrom.SelectedItem.Text,"Jan."+DropYearTo.SelectedItem.Text,"Feb."+DropYearTo.SelectedItem.Text,"Mar."+DropYearTo.SelectedItem.Text};
			str="select "+ColumnName+" from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.HasRows)
			{
			int ii=0;
			while(SqlDtr.Read())
			{
			%>
				<tr>
					<td bgColor="#ffffff" align=center><%=SqlDtr.GetValue(2).ToString()%><input type=hidden value="<%=SqlDtr.GetValue(2).ToString()%>" name="Month<%=ii%>"></td>
					<%for(int j=1;j<count;j++)
					{%>
						<td bgColor="#ffffff"><%=SqlDtr.GetValue(j+2).ToString()%></td>
					<%}ii++;%>
				</tr>
			<%}
			}
			else
			{
			
			MessageBox.Show("Data Not Available");
			}				
			}
			else
			{
			%>
			<tr bgColor="#CE4848">
				<td align=center><font color=white>Total Purchase</font></td>
				<td align=center><font color=white>Total Sales</font></td>
			</tr>
			<%
			string str="select Month,total_purchase,total_sales from ly_ps_sale where ly_ps_sales='"+DropYearFrom.SelectedItem.Text+DropYearTo.SelectedItem.Text+"'";
			SqlDtr=obj.GetRecordSet(str);
			if(SqlDtr.HasRows)
			{
				int i=0;
				while(SqlDtr.Read())
				{
					%>
					<tr>
						<td bgColor="#ffffff">&nbsp;&nbsp;<%=SqlDtr.GetValue(0).ToString()%></td>
						<td bgColor="#ffffff" align=right><%=SqlDtr.GetValue(1).ToString()%>&nbsp;</td>
						<td bgColor="#ffffff" align=right><%=SqlDtr.GetValue(2).ToString()%>&nbsp;</td>
					</tr>
			<%i++;}
			}
			else
			{
			MessageBox.Show("Data Not Available");
			//}
			count=3;}}%>
			<tr>
				<td><input type=hidden value="<%=Mon.Length%>" name=M><input type=hidden value="<%=count%>" name=col></td>
			</tr>
			<%}%>
						</table>
					</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
