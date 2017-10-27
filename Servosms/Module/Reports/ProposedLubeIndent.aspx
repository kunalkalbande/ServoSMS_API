<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.ProposedLubeIndent" CodeFile="ProposedLubeIndent.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: SSA Lube Indent</title>
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
		<script language=javascript>
		function Check(t,e)
		{
			if(window.event) 
			{
				var	key = e.keyCode;
				if(key==13)
				{
					if(t!=null)
						t.focus();
				}
			}
		}
		</script>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<script language=javascript id=Validations src="../../Sysitem/JS/Validations.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center">
				<TR>
					<TH align="center" valign="top" height="20">
						<font color="#CE4848">SSA Lube Indent For The Month Of&nbsp;
							<asp:DropDownList ID="DropMonth" Runat="server" Width="100" AutoPostBack="true" CssClass=fontstyle onselectedindexchanged="DropMonth_SelectedIndexChanged">
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
							</asp:DropDownList><asp:CompareValidator ID="cv1" Runat="server" ValueToCompare="Select" ControlToValidate="DropMonth" Operator="NotEqual"
								ErrorMessage="Please Select The Month">*</asp:CompareValidator><asp:DropDownList ID="DropYear" Runat="server" Width="100" CssClass=fontstyle>
								<asp:ListItem Value="2001">2001</asp:ListItem>
								<asp:ListItem Value="2002">2002</asp:ListItem>
								<asp:ListItem Value="2003">2003</asp:ListItem>
								<asp:ListItem Value="2004">2004</asp:ListItem>
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
							</asp:DropDownList></font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center" height="20"><asp:button id="btnShow" runat="server" 
							Text="Submit" Width="75px" CausesValidation="True" onclick="btnShow_Click"></asp:button></td>
				</tr>
				<tr>
					<td align="center" valign="top">
						<asp:datagrid id="DataGrid1" runat="server" AutoGenerateColumns="False">
							<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848" Height=22px></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="RSE" HeaderText="RSE"></asp:BoundColumn>
								<asp:BoundColumn DataField="SUPEX" HeaderText="SUP EX"></asp:BoundColumn>
								<asp:BoundColumn DataField="RETAILMPSO" HeaderText="RETAIL MPSO"></asp:BoundColumn>
								<asp:BoundColumn DataField="SKUTYPE" HeaderText="SKU TYPE"></asp:BoundColumn>
								<asp:BoundColumn DataField="PACKCODE" HeaderText="PACK CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="PACKQTY" SortExpression="PACKQTY" HeaderText="PACK QTY"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRODCODE" SortExpression="PRODCODE" HeaderText="PROD CODE"></asp:BoundColumn>
								<asp:BoundColumn DataField="SKUNAMEWITHPACK" SortExpression="SKUNAMEWITHPACK" HeaderText="SKU NAME WITH PACK"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="INDENT">
									<ItemTemplate>
										<input type=text size=6 name="txt<%=j++%>" onkeyup="Check(document.Form1.txt<%=j%>,event)" value="<%#GetIndent(DataBinder.Eval(Container.DataItem,"prodcode").ToString(),DataBinder.Eval(Container.DataItem,"skunamewithpack").ToString())%>" class=fontstyle maxlength=9  onkeypress="return GetOnlyNumbers(this, event);" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove">
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
				<tr>
					<td><asp:ValidationSummary ID="val1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>
