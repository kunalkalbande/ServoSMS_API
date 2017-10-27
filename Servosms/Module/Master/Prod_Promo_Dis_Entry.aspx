<%@ Page language="c#" Inherits="Servosms.Module.Master.Prod_Promo_Dis_Entry" CodeFile="Prod_Promo_Dis_Entry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Servo Stockist Discount Entry</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
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
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 136px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox>
			<table height="200" cellSpacing="0" cellPadding="0" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Product Promotion&nbsp;Grades Entry&nbsp;</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellSpacing="0" cellPadding="0">
							<TBODY>
								<TR>
									<TD colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Scheme ID&nbsp;<asp:requiredfieldvalidator id="rfv1" InitialValue="Select" ErrorMessage="Please Select The Scheme ID" ControlToValidate="dropschid"
											Runat="server">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="dropschid" runat="server" Width="296px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="dropschid_SelectedIndexChanged"></asp:dropdownlist>&nbsp;<asp:label id="lblschid" runat="server" Width="50px"></asp:label>&nbsp;&nbsp;&nbsp;
										<asp:button id="btschid" runat="server" Width="30px" Text="..." CausesValidation="False" 
											 onclick="btschid_Click" Height="23px"></asp:button></TD>
								</TR>
								<TR>
									<TD align="left" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Scheme 
										Name&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:textbox id="txtschname" runat="server" Width="110px" CssClass="dropdownlist" BorderStyle="Groove"></asp:textbox></TD>
								<tr>
									<td vAlign="top" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
										Date From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:textbox id="txtDateFrom" runat="server" Width="110px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date 
										To&nbsp;&nbsp;&nbsp;
										<asp:textbox id="txtDateTo" runat="server" Width="110px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Scheme 
										Discount&nbsp;<asp:requiredfieldvalidator id="rfv2" ErrorMessage="Please Enter The Scheme Discount" ControlToValidate="txtSchDiscount"
											Runat="server">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtSchDiscount"
											Width="110px" Runat="server" CssClass="dropdownlist" BorderStyle="Groove" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropSchDiscount" Runat="server" CssClass="dropdownlist">
											<asp:ListItem Value="Rs">Rs</asp:ListItem>
											<asp:ListItem Value="%">%</asp:ListItem>
										</asp:dropdownlist></td>
								<TR>
									<TD align="center"><FONT color="#000066">Products Availableucts Available</FONT></TD>
									<TD></TD>
									<TD align="center"><FONT color="#000066">Products Assigned <FONT color="red">*</FONT></FONT></TD>
								</TR>
								<TR>
									<TD><asp:listbox id="ListEmpAvailable" runat="server" Width="350px" SelectionMode="Multiple"
											Height="160px"></asp:listbox></TD>
									<TD>
										<P><asp:button id="btnIn" runat="server" Width="35px" Text=">" CausesValidation="False" 
												 Font-Bold="True" onclick="btnIn_Click"></asp:button></P>
										<P dir="ltr" align="justify"><asp:button id="btnout" runat="server" Width="35px" Text="<" CausesValidation="False" 
												 Font-Bold="True" onclick="btnout_Click"></asp:button></P>
										<P><asp:button id="btn1" runat="server" Width="35px" Text=">>" CausesValidation="False" 
												 Height="25px" Font-Bold="True" onclick="btn1_Click"></asp:button></P>
									</TD>
									<TD align="right"><asp:listbox id="ListEmpAssigned" runat="server" Width="350px" CssClass="Dropdownlist" SelectionMode="Multiple"
											Height="160px"></asp:listbox></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="3"><asp:button id="btnSubmit" runat="server" Width="60px" Text="Submit" 
											 onclick="btnSubmit_Click"></asp:button><asp:button id="btnupdate" runat="server" Width="60px" Text="Update" 
											 onclick="btnupdate_Click"></asp:button></TD>
								</TR>
								<TR>
									<TD align="center" colSpan="3">&nbsp;</TD>
								</TR>
								<TR>
									<TD colSpan="3"><asp:validationsummary id="vsShiftAssignment" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD>
								</TR>
							</TBODY>
						</TABLE>
					</td>
				</tr>
			</table>
			</TD></TR></TBODY></TABLE><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no"
				height="189"> </iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
