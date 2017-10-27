<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes" %>
<%@ Page language="c#" Inherits="Servosms.Module.Reports.ProductReport" CodeFile="ProductReport.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Product Report</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			
		function CheckSearchOption(t)
		{
			var index = t.selectedIndex
			var f = document.Form1;
			if(index != 0)
			{
				if(index == 1)
					f.texthiddenprod.value=f.tempProdWithPack.value;
				else if(index == 2)
					f.texthiddenprod.value=f.tempProduct.value;
				else if(index == 3)
					f.texthiddenprod.value=f.tempPack.value;
				else if(index == 4)
					f.texthiddenprod.value=f.tempProductGroup.value;
							
				f.texthiddenprod.value=f.texthiddenprod.value.substring(0,f.texthiddenprod.value.length-1)
			}
			else
				f.texthiddenprod.value="";
				
			document.Form1.DropValue.value="All";
			document.Form1.DropProdName.style.visibility="hidden"
		}
			
			
		</script>
</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<asp:textbox id="TextBox2" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox><uc1:header id="Header1" runat="server"></uc1:header>
			<input id="tempProduct" style="WIDTH: 1px" type="hidden" name="tempProduct" runat="server">
			<input id="tempSsrName" style="WIDTH: 1px" type="hidden" name="tempSsrName" runat="server">
			<input id="tempPartyName" style="WIDTH: 1px" type="hidden" name="tempPartyName" runat="server">
			<input id="tempPartyType" style="WIDTH: 1px" type="hidden" name="tempPartyType" runat="server">
			<input id="tempProdWithPack" style="WIDTH: 1px" type="hidden" name="tempProdWithPack" runat="server">
			<input id="tempProductGroup" style="WIDTH: 1px" type="hidden" name="tempProductGroup" runat="server">
			<input id="tempPack" style="WIDTH: 1px" type="hidden" name="tempPack" runat="server">
			<INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server"><INPUT id="texthiddenpartytype" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenpartytype" runat="server"><INPUT id="texthiddenpartyname" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenpartyname" runat="server">
			<table height="290" width="778" align="center">
				<TR>
					<TH align="center" colspan="3">
						<font color="#ce4848">Product&nbsp;Report</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center" valign="top">Search By&nbsp;
						<asp:dropdownlist id="DropSearchBy" runat="server" CssClass="dropdownlist" onchange="CheckSearchOption(this)">
							<asp:ListItem Value="All">All</asp:ListItem>
							<asp:ListItem Value="Product With Pack">Product With Pack</asp:ListItem>
							<asp:ListItem Value="Product Wise">Product Wise</asp:ListItem>
							<asp:ListItem Value="Pack Wise">Pack Wise</asp:ListItem>
							<asp:ListItem Value="Product Type">Product Type</asp:ListItem>
						</asp:dropdownlist></td>
					<td valign="top">&nbsp;Value&nbsp; 
					       <input class="TextBoxStyle"  onkeypress="return GetAnyNumber(this, event);" id="DropValue"
							onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropValue,document.Form1.btnShow)"
							style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 200px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							value="All" name="DropValue" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							readOnly type="text" name="temp"><br>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropValue,document.Form1.btnShow)"
								id="DropProdName" ondblclick="select(this,document.Form1.DropValue)" onkeyup="arrowkeyselect(this,event,document.Form1.btnShow,document.Form1.DropValue)"
								style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 220px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropValue)" multiple name="DropProdName"
								type="select-one"></select></div>
					</td>
					<td valign="top">
						<asp:button id="btnShow" runat="server" Width="75px" Text="View   "
							 onclick="btnShow_Click"></asp:button>&nbsp;<asp:button id="BtnPrint" Width="75px" Text="Print  " Runat="server"
							 onclick="BtnPrint_Click"></asp:button>&nbsp;
						<asp:button id="btnExcel" Width="75px" 
							Text="Excel" Runat="server" onclick="btnExcel_Click"></asp:button></td>
				</tr>
				<tr>
					<td align="center" colspan="3">
						<TABLE>
							<TR>
								<TD align="center" colSpan="5" height="200"><asp:datagrid id="GridReport" runat="server" AutoGenerateColumns="False" BorderStyle="None" BorderWidth="0px"
										BackColor="#DEBA84" BorderColor="#DEBA84" CellPadding="1" CellSpacing="1" AllowSorting="True" OnSortCommand="SortCommand_Click">
										<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
										<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
										<HeaderStyle Font-Bold="True" Height="25px" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
										<FooterStyle HorizontalAlign="Right" ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
										<Columns>
											<asp:BoundColumn DataField="Prod_ID" SortExpression="Prod_ID" HeaderText="Product ID"></asp:BoundColumn>
											<asp:BoundColumn DataField="Prod_Code" SortExpression="Prod_Code" HeaderText="Product Code"></asp:BoundColumn>
											<asp:BoundColumn DataField="Category" SortExpression="Category" HeaderText="Category"></asp:BoundColumn>
											<asp:TemplateColumn SortExpression="Prod_Name" HeaderText="Product Name">
												<ItemTemplate>
													<%#ProdName(DataBinder.Eval(Container.DataItem,"Prod_Name").ToString())%>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="Pack_Type" SortExpression="Pack_Type" HeaderText="Pack Type"></asp:BoundColumn>
											<asp:BoundColumn DataField="Unit" SortExpression="Unit" HeaderText="Unit"></asp:BoundColumn>
											<asp:BoundColumn DataField="Total_Qty" SortExpression="Total_Qty" HeaderText="Unit Qty">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="Store_In" SortExpression="Store_In" HeaderText="Store In"></asp:BoundColumn>
										</Columns>
										<PagerStyle Visible="False" NextPageText="Next" PrevPageText="Previous" HorizontalAlign="Center"
											ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
				<tr>
					<td align="right" colspan="3"></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
