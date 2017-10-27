<%@ Page language="c#" Inherits="Servosms.Module.Master.CustomerMapping" CodeFile="CustomerMapping.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomerMapping</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" width="778" align="center" cellpadding="0" cellspacing="0">
				<TR>
					<TH align="center" height="20">
						<font color="#ce4848">Customer Mapping&nbsp;</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellpadding="5" cellspacing="5">
							<tbody>
								<TR>
									<TD align="center" colSpan="3">Selection Of SSR&nbsp;<FONT color="red">*&nbsp;&nbsp;</FONT>&nbsp;&nbsp;<asp:dropdownlist id="DropSSRName" runat="server" Width="165px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="DropSSRName_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist><asp:comparevalidator id="cvShiftName" runat="server" ValueToCompare="Select" Operator="NotEqual" ControlToValidate="DropSSRName"
											ErrorMessage="Please Select The SSR Name">*</asp:comparevalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="center"><FONT color="#000066">Customer Name</FONT></TD>
									<TD></TD>
									<TD align="center"><FONT color="#000066">Assigned Customer&nbsp;<FONT color="red">*</FONT></FONT></TD>
								</TR>
								<TR>
									<TD><asp:listbox id="ListCustomer" runat="server" Width="350px" Font-Size="8pt" SelectionMode="Multiple"
											Height="160px"></asp:listbox></TD>
									<TD>
										<P><asp:button id="btnIn" runat="server" Width="50px" Text=">" CausesValidation="False" Font-Bold="True" onclick="btnIn_Click"></asp:button></P>
										<P dir="ltr" align="justify"><asp:button id="btnout" runat="server" Width="50px" Text="<" CausesValidation="False" Font-Bold="True" onclick="btnout_Click"></asp:button></P>
										<P><asp:button id="btn1" runat="server" Width="50px" Text=">>" CausesValidation="False" Height="25px" Font-Bold="True" onclick="btn1_Click"></asp:button></P>
									</TD>
									<TD><asp:listbox id="ListassignCustomer" runat="server" Width="350px" CssClass="Dropdownlist" SelectionMode="Multiple"
											Height="160px"></asp:listbox></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="3"><asp:button id="btnSubmit" runat="server" Width="75px" Text="Submit" onclick="btnSubmit_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD colSpan="3"><asp:validationsummary id="vsShiftAssignment" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD>
								</TR>
							</tbody>
						</TABLE>
					</td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
		</FORM>
	</body>
</HTML>
