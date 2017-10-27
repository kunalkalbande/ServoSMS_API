<%@ Page language="c#" Inherits="Servosms.Module.Master.SalespesonAssignment" CodeFile="SalespersonAssignment.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Servosms: Sales Peson Assignment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Sales Person Assignment&nbsp;</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellSpacing="5" cellPadding="5">
							<tbody>
								<TR>
									<TD align="center" colSpan="3">Selection Of SSR&nbsp;<FONT color="red">*&nbsp;&nbsp;</FONT>&nbsp;&nbsp;<asp:dropdownlist id="DropSSRname" runat="server" AutoPostBack="True" CssClass="dropdownlist" Width="165px" onselectedindexchanged="DropSSRname_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist><asp:comparevalidator id="cvShiftName" runat="server" ErrorMessage="Please select the Scheme Type" ControlToValidate="DropSSRname"
											Operator="NotEqual" ValueToCompare="Select">*</asp:comparevalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD align="center"><FONT color="#000066">Beat Name</FONT></TD>
									<TD></TD>
									<TD align="center"><FONT color="#000066">Assigned Beat&nbsp;<FONT color="red">*</FONT></FONT></TD>
								</TR>
								<TR>
									<TD><asp:listbox id="Listbeat" runat="server" Width="350px" Height="160px" SelectionMode="Multiple"
											Font-Size="8pt"></asp:listbox></TD>
									<TD>
										<P><asp:button id="btnIn" runat="server" Width="50px" Font-Bold="True" 
												 CausesValidation="False" Text=">" onclick="btnIn_Click"></asp:button></P>
										<P dir="ltr" align="justify"><asp:button id="btnout" runat="server" Width="50px" Font-Bold="True" 
												 CausesValidation="False" Text="<" onclick="btnout_Click"></asp:button></P>
										<P><asp:button id="btn1" runat="server" Width="50px" Height="25px" Font-Bold="True" 
												 CausesValidation="False" Text=">>" onclick="btn1_Click"></asp:button></P>
									</TD>
									<TD><asp:listbox id="Listassibeat" runat="server" CssClass="Dropdownlist" Width="350px" Height="160px"
											SelectionMode="Multiple"></asp:listbox></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="3"><asp:button id="btnSubmit" runat="server" Width="75px" 
											 Text="Submit" onclick="btnSubmit_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD colSpan="3"><asp:validationsummary id="vsShiftAssignment" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></TD>
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
	</body>
</HTML>
