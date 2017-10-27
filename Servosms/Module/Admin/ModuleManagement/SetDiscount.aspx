<%@ Reference Page="~/Module/Admin/Privileges.aspx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Admin.ModuleManagement.SetDiscount" CodeFile="SetDiscount.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: SetDiscount</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<script language="javascript" id="Validations" src="../../../Sysitem/JS/Validations.js"></script>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
			<table height="290" cellSpacing="0" cellPadding="0" width="775" align="center" border="0">
				<TR vAlign="top">
					<TH align="center" colSpan="2" height="20">
						<font color="#ce4848">Set Discount</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td vAlign="top" align="center" width="40%">
						<table>
							<tr>
								<td><asp:radiobutton id="RadPurchase" Runat="server" Text="Purchase Invoice" Checked="True" GroupName="Radio"
										AutoPostBack="True" oncheckedchanged="RadPurchase_CheckedChanged"></asp:radiobutton></td>
							</tr>
							<tr>
								<td><asp:radiobutton id="RadSales" Runat="server" Text="Sales Invoice" GroupName="Radio" AutoPostBack="True" oncheckedchanged="RadSales_CheckedChanged"></asp:radiobutton></td>
							</tr>
							<tr>
								<td><asp:radiobutton id="RadModCen" Runat="server" Text="Modvat/Cenvat Invoice" GroupName="Radio" AutoPostBack="True" oncheckedchanged="RadModCen_CheckedChanged"></asp:radiobutton></td>
							</tr>
							<tr>
								<td><asp:radiobutton id="RadSSrInc" Runat="server" Text="SSR Incentive" GroupName="Radio" AutoPostBack="True" oncheckedchanged="RadSSrInc_CheckedChanged"></asp:radiobutton></td>
							</tr>
						</table>
					</td>
					<td vAlign="top" width="60%">
						<TABLE borderColor="#deba84" cellSpacing="0" cellPadding="0" rules="all" width="300">
							<asp:panel id="PanPurchase" Runat="server">
								<TBODY>
									<TR>
										<TD colSpan="4">
											<TABLE cellSpacing="0" cellPadding="0" rules="all" width="400">
												<TR bgColor="#ce4848">
													<TD align="center" width="100"><FONT color="white"><B>Discount Name</B></FONT></TD>
													<TD align="center" width="290"><FONT color="white"><B>Discount</B></FONT></TD>
													<TD align="center" width="30"><FONT color="white"><B>Liter</B></FONT></TD>
													<TD align="center" width="30"><FONT color="white"><B>Apply</B></FONT></TD>
												</TR>
												<TR>
													<TD>&nbsp;Date From</TD>
													<TD>
														<asp:DropDownList id="drop1" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="01">01</asp:ListItem>
															<asp:ListItem Value="02">02</asp:ListItem>
															<asp:ListItem Value="03">03</asp:ListItem>
															<asp:ListItem Value="04">04</asp:ListItem>
															<asp:ListItem Value="05">05</asp:ListItem>
															<asp:ListItem Value="06">06</asp:ListItem>
															<asp:ListItem Value="07">07</asp:ListItem>
															<asp:ListItem Value="08">08</asp:ListItem>
															<asp:ListItem Value="09">09</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
														</asp:DropDownList>
														<asp:DropDownList id="drop3" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="05">05</asp:ListItem>
															<asp:ListItem Value="06">06</asp:ListItem>
															<asp:ListItem Value="07">07</asp:ListItem>
															<asp:ListItem Value="08">08</asp:ListItem>
															<asp:ListItem Value="09">09</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="11">11</asp:ListItem>
															<asp:ListItem Value="12">12</asp:ListItem>
															<asp:ListItem Value="13">13</asp:ListItem>
															<asp:ListItem Value="14">14</asp:ListItem>
														</asp:DropDownList>
														<asp:DropDownList id="drop5" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="11">11</asp:ListItem>
															<asp:ListItem Value="12">12</asp:ListItem>
															<asp:ListItem Value="13">13</asp:ListItem>
															<asp:ListItem Value="14">14</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="16">16</asp:ListItem>
															<asp:ListItem Value="17">17</asp:ListItem>
															<asp:ListItem Value="18">18</asp:ListItem>
															<asp:ListItem Value="19">19</asp:ListItem>
														</asp:DropDownList>
														<asp:DropDownList id="drop7" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="16">16</asp:ListItem>
															<asp:ListItem Value="17">17</asp:ListItem>
															<asp:ListItem Value="18">18</asp:ListItem>
															<asp:ListItem Value="19">19</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="21">21</asp:ListItem>
															<asp:ListItem Value="22">22</asp:ListItem>
															<asp:ListItem Value="23">23</asp:ListItem>
															<asp:ListItem Value="24">24</asp:ListItem>
														</asp:DropDownList></TD>
													<TD>&nbsp;</TD>
													<TD>&nbsp;</TD>
												</TR>
												<TR>
													<TD>&nbsp;Date To</TD>
													<TD>
														<asp:DropDownList id="drop2" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="05">05</asp:ListItem>
															<asp:ListItem Value="06">06</asp:ListItem>
															<asp:ListItem Value="07">07</asp:ListItem>
															<asp:ListItem Value="08">08</asp:ListItem>
															<asp:ListItem Value="09">09</asp:ListItem>
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="11">11</asp:ListItem>
															<asp:ListItem Value="12">12</asp:ListItem>
															<asp:ListItem Value="13">13</asp:ListItem>
															<asp:ListItem Value="14">14</asp:ListItem>
														</asp:DropDownList>
														<asp:DropDownList id="drop4" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="10">10</asp:ListItem>
															<asp:ListItem Value="11">11</asp:ListItem>
															<asp:ListItem Value="12">12</asp:ListItem>
															<asp:ListItem Value="13">13</asp:ListItem>
															<asp:ListItem Value="14">14</asp:ListItem>
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="16">16</asp:ListItem>
															<asp:ListItem Value="17">17</asp:ListItem>
															<asp:ListItem Value="18">18</asp:ListItem>
															<asp:ListItem Value="19">19</asp:ListItem>
														</asp:DropDownList>
														<asp:DropDownList id="drop6" Runat="server" CssClass="fontstyle" Width="40px">
															<asp:ListItem Value="15">15</asp:ListItem>
															<asp:ListItem Value="16">16</asp:ListItem>
															<asp:ListItem Value="17">17</asp:ListItem>
															<asp:ListItem Value="18">18</asp:ListItem>
															<asp:ListItem Value="19">19</asp:ListItem>
															<asp:ListItem Value="20">20</asp:ListItem>
															<asp:ListItem Value="21">21</asp:ListItem>
															<asp:ListItem Value="22">22</asp:ListItem>
															<asp:ListItem Value="23">23</asp:ListItem>
															<asp:ListItem Value="24">24</asp:ListItem>
														</asp:DropDownList>
														<asp:DropDownList id="drop8" Runat="server" CssClass="fontstyle" Width="60px">
															<asp:ListItem Value="E.O.M.">E.O.M.</asp:ListItem>
														</asp:DropDownList></TD>
													<TD>&nbsp;</TD>
													<TD>&nbsp;</TD>
												</TR>
												<TR>
													<TD>&nbsp;Early Bird Dis.</TD>
													<TD>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurEarlyBird"
															Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurEarlyBird1"
															Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurEarlyBird2"
															Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurEarlyBird3"
															Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD>
														<asp:DropDownList id="DropPurEarlyRs" Runat="server" CssClass="fontstyle" Width="100%">
															<asp:ListItem Value="%">%</asp:ListItem>
															<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
														</asp:DropDownList></TD>
													<TD align="center">
														<asp:CheckBox id="chkPurEarlyBird" Runat="server"></asp:CheckBox></TD>
												</TR>
												<TR>
													<TD>Servo Stk. Discount</TD>
													<TD>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurServostk"
															Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD>
														<asp:DropDownList id="DropPurServostkRs" Runat="server" CssClass="fontstyle" Width="90%">
															<asp:ListItem Value="%">%</asp:ListItem>
														</asp:DropDownList></TD>
													<TD align="center">
														<asp:CheckBox id="chkPurServostk" Runat="server"></asp:CheckBox></TD>
												</TR>
												<TR>
													<TD>&nbsp;Fixed Discount</TD>
													<TD>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurFixed" Runat="server"
															CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD>
														<asp:DropDownList id="DropPurFixedRs" Runat="server" CssClass="fontstyle">
															<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
														</asp:DropDownList></TD>
													<TD align="center">
														<asp:CheckBox id="chkPurFixed" Runat="server"></asp:CheckBox></TD>
												</TR>
												<TR>
													<TD>&nbsp;Cash Discount</TD>
													<TD>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurCashDis"
															Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD>
														<asp:DropDownList id="DropPurCashDisRs" Runat="server" CssClass="fontstyle">
															<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
															<asp:ListItem Value="%">%</asp:ListItem>
														</asp:DropDownList></TD>
													<TD align="center">
														<asp:CheckBox id="chkPurCashDis" Runat="server"></asp:CheckBox></TD>
												</TR>
												<TR>
													<TD>&nbsp;Discount</TD>
													<TD>
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurDis" Runat="server"
															CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD>
														<asp:DropDownList id="DropPurDisRs" Runat="server" CssClass="fontstyle">
															<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
															<asp:ListItem Value="%">%</asp:ListItem>
														</asp:DropDownList></TD>
													<TD align="center">
														<asp:CheckBox id="chkPurDis" Runat="server"></asp:CheckBox></TD>
												</TR>
												<%--<TR>
													<TD style="HEIGHT: 23px">&nbsp;IGST</TD>
													<TD style="HEIGHT: 23px">
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPurVat" Runat="server"
															CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD style="HEIGHT: 23px">
														<asp:DropDownList id="DropPurVatRs" Runat="server" CssClass="fontstyle" Width="90%">
															<asp:ListItem Value="%">%</asp:ListItem>
														</asp:DropDownList></TD>
													<TD style="HEIGHT: 23px" align="center">
														<asp:CheckBox id="chkPurVat" Runat="server"></asp:CheckBox></TD>
												</TR>
                                                <TR>
													<TD style="HEIGHT: 23px">&nbsp;CGST</TD>
													<TD style="HEIGHT: 23px">
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TextBoxcgst" Runat="server"
															CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD style="HEIGHT: 23px">
														<asp:DropDownList id="DropDownListcgst" Runat="server" CssClass="fontstyle" Width="90%">
															<asp:ListItem Value="%">%</asp:ListItem>
														</asp:DropDownList></TD>
													<TD style="HEIGHT: 23px" align="center">
														<asp:CheckBox id="CheckBoxcgst" Runat="server"></asp:CheckBox></TD>
												</TR>
                                                <TR>
													<TD style="HEIGHT: 23px">&nbsp;SGST</TD>
													<TD style="HEIGHT: 23px">
														<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TextBoxsgst" Runat="server"
															CssClass="FontStyle" BorderStyle="Groove" Width="140px" MaxLength="6"></asp:TextBox></TD>
													<TD style="HEIGHT: 23px">
														<asp:DropDownList id="DropDownListsgst" Runat="server" CssClass="fontstyle" Width="90%">
															<asp:ListItem Value="%">%</asp:ListItem>
														</asp:DropDownList></TD>
													<TD style="HEIGHT: 23px" align="center">
														<asp:CheckBox id="CheckBoxsgst" Runat="server"></asp:CheckBox></TD>
												</TR>--%>
											</TABLE>
										</TD>
									</TR>
                                    <!--Sales Invoice -->
							</asp:panel><asp:panel id="PanSales" Runat="server">
								<TR bgColor="#ce4848">
									<TD align="center"><FONT color="white"><B>Discount Name</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Discount</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Liter</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Apply</B></FONT></TD>
								</TR>
								<TR>
									<TD width="100%">&nbsp;Scheme Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtSalesSchDis"
											Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="120px" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropSalesSchDisRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkSalesSchDis" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Fleet/Oe Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtSalesFleetOe"
											Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropSalesFleetOeRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkSalesFleetOe" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtSalesDis" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropSalesDisRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkSalesDis" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Cash Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtSalesCashDis"
											Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropSalesCashDisRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkSalesCashDis" Runat="server"></asp:CheckBox></TD>
								</TR>
								<%--<TR>
									<TD>&nbsp;IGST</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtSalesVat" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropSalesVatRs" Runat="server" CssClass="fontstyle" >
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkSalesVat" Runat="server"></asp:CheckBox></TD>
								</TR>
                                <TR>
									<TD>&nbsp;CGST</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TextBoxCGST2" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropDownListCGST2" Runat="server" CssClass="fontstyle" >
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="CheckBoxCGST2" Runat="server"></asp:CheckBox></TD>
								</TR>
                                <TR>
									<TD>&nbsp;SGST</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TextBoxSGST2" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropDownListSGST2" Runat="server" CssClass="fontstyle" >
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="CheckBoxSGST2" Runat="server"></asp:CheckBox></TD>
								</TR>--%>
							</asp:panel>
							<!--Modvat/ Cenvat Invoice Penal -->
							<asp:panel id="PanModCen" Runat="server">
								<TR bgColor="#ce4848">
									<TD align="center"><FONT color="white"><B>Discount Name</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Discount</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Liter</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Apply</B></FONT></TD>
								</TR>
								<TR>
									<TD width="100%">&nbsp;Scheme Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModSchDis" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="120px" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModSchDisRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModSchDis" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Fleet/Oe Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModFleetOe"
											Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModFleetOeRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModFleetOe" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModDis" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModDisRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModDis" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Cash Discount</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModCashDis"
											Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModCashDisRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModCashDis" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;IGST</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModVat" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModVatRs" Runat="server" CssClass="fontstyle" Width="90%">
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModVat" Runat="server"></asp:CheckBox></TD>
								</TR>
                                <TR>
									<TD>&nbsp;CGST</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TextBoxCGST1" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropDownListCGST1" Runat="server" CssClass="fontstyle" Width="90%">
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="CheckBoxCGST1" Runat="server"></asp:CheckBox></TD>
								</TR>
                                <TR>
									<TD>&nbsp;SGST</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TextBoxSGST1" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropDownListSGST1" Runat="server" CssClass="fontstyle" Width="90%">
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="CheckBoxSGST1" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD width="100%">&nbsp;Excise Duty</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModExcise" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModExciseRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModExcise" Runat="server"></asp:CheckBox></TD>
								</TR>
								<TR>
									<TD>&nbsp;Entry Tax</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtModEntryTax"
											Runat="server" CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="DropModEntryTaxRs" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="chkModEntryTax" Runat="server"></asp:CheckBox></TD>
								</TR>
							</asp:panel>
							<!--  Genaral scheme/ Discount-->
							<asp:Panel ID="panSSRInc" Visible="False" Runat="server">
								<TR bgColor="#ce4848">
									<TD align="center"><FONT color="white"><B>Discount Name</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Discount</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Liter</B></FONT></TD>
									<TD align="center"><FONT color="white"><B>Apply</B></FONT></TD>
								</TR>
								<TR>
								<TR>
									<TD>&nbsp;SSR Incentive</TD>
									<TD>
										<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtdisname" Runat="server"
											CssClass="FontStyle" BorderStyle="Groove" Width="100%" MaxLength="6"></asp:TextBox></TD>
									<TD>
										<asp:DropDownList id="Dropdisc" Runat="server" CssClass="fontstyle">
											<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:DropDownList></TD>
									<TD align="center">
										<asp:CheckBox id="Checkappl" Runat="server"></asp:CheckBox></TD>
								</TR>
							</asp:Panel>
							<TR>
								<TD align="right" colSpan="4"><asp:button id="btnUpdate" runat="server" Text="Submit" Width="70px" CausesValidation="true"
									onclick="btnUpdate_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
				</TBODY></table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
