<%@ Reference Page="~/Module/Admin/Privileges.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header1" Src="../../HeaderFooter/Header1.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Admin.OrganisationDetails" CodeFile="OrganisationDetails.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Organisation Details</title>
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
		<script language="javascript">
	function StartInvoice(t)
	{
		if(t.value=="0" || t.value=="00" || t.value=="000" || t.value=="0000" || t.value=="00000" || t.value=="000000")
		{
			alert("Please Enter The Number is Greater Than Or Equels To 1");
			t.value="";
			t.focus()
			
		}
	}
		</script>
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="Beat" src="../../Sysitem/JS/Beat.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" encType="multipart/form-data" runat="server">
			<asp:textbox id="txtdumy" style="Z-INDEX: 101; LEFT: 168px; POSITION: absolute; TOP: 16px" runat="server"
				Width="0px" BorderStyle="Groove" Visible="False"></asp:textbox><asp:textbox id="txtdummy" style="Z-INDEX: 103; LEFT: 152px; POSITION: absolute; TOP: 16px" runat="server"
				Width="0px" Visible="False" ontextchanged="txtdummy_TextChanged"></asp:textbox><uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 104; LEFT: 184px; POSITION: absolute; TOP: 16px" runat="server"
				Width="0px" Visible="False"></asp:textbox><asp:textbox id="txtbeatname" style="Z-INDEX: 105; LEFT: 208px; POSITION: absolute; TOP: 16px"
				Width="0px" Runat="server"></asp:textbox>
			<table height="278" cellSpacing="0" cellPadding="0" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Organization Details</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellSpacing="0" cellPadding="0">
							<TR>
								<TD><asp:label id="Label1" runat="server" Width="64px">Company ID</asp:label></TD>
								<TD><asp:label id="LblCompanyID" Width="72px" Runat="server" ForeColor="Blue"></asp:label><asp:dropdownlist id="Drop" runat="server" Width="60px" Visible="False" AutoPostBack="True" CssClass="DropDownlist" onselectedindexchanged="Drop_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist><asp:button id="Button1" runat="server" Text="..." CausesValidation="False" Height="20px" onclick="Button1_Click"></asp:button></TD>
								<TD colspan="2" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<FONT color="#ff0000">Fields 
										Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD>Name Of Dealer</TD>
								<TD colSpan="3"><asp:textbox id="txtDealerName" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownlist" ontextchanged="txtDealerName_TextChanged"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Stockist Type</TD>
								<TD colSpan="3"><asp:dropdownlist id="DropDealerShip" runat="server" Width="150px" CssClass="DropDownlist" onselectedindexchanged="DropDealerShip_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Servo Stockist(Auto)">Servo Stockist(Auto)</asp:ListItem>
										<asp:ListItem Value="Servo Stockist(Industrial)">Servo Stockist(Industrial)</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="TxtDealership" runat="server" Width="350px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Address</TD>
								<TD colSpan="1"><asp:textbox id="TxtAddress" runat="server" Width="150px"  BorderStyle="Groove" TextMode="MultiLine"
										Font-Names="Verdana" CssClass="DropDownlist"></asp:textbox></TD>
                                <TD align="center">GSTIN No.</TD>
								<TD><asp:textbox  id="TxtTinno" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="15" CssClass="DropDownlist"></asp:textbox></TD>
							</TR>
							<!--TR>
								<TD></TD>
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 10px"></TD>
								<TD style="HEIGHT: 10px" colSpan="3"></TD>
							</TR-->
							<TR>
								<TD>City&nbsp;<FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator2" runat="server" ControlToValidate="DropCity" ErrorMessage="Please Select City"
										ValueToCompare="Select" Operator="NotEqual" Width="20px" Height="20px">*</asp:comparevalidator></TD>
								<TD><asp:dropdownlist id="DropCity" runat="server" Width="130px" AutoPostBack="false" onchange="getbeatInfo(this,document.Form1.DropState,document.Form1.DropCountry);"
										CssClass="DropDownlist" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
                                <TD align="center">Phone no.<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneOff"
										ErrorMessage="Contact No. Between 7-12 Digits" ValidationExpression="\d{7,12}">*</asp:regularexpressionvalidator></TD>
                                <TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtPhoneOff" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="12" CssClass="DropDownlist"></asp:textbox></TD>
                                
								
							</TR>
							<TR>
								<TD>District</TD>
								<TD><asp:dropdownlist id="DropState" runat="server" Width="130px" CssClass="DropDownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
                                <TD align="center">Fax No<asp:regularexpressionvalidator id="Regularexpressionvalidator4" runat="server" ControlToValidate="TxtFaxNo" ErrorMessage="Fax No. Between 7-12 Digits"
										ValidationExpression="\d{7,12}">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="TxtFaxNo" runat="server"
										Width="130px" BorderStyle="Groove" CssClass="DropDownlist" MaxLength="12"></asp:textbox></TD>
                                
								
							</TR>
							<TR>
								<TD>State</TD>
								<TD><asp:dropdownlist id="DropCountry" runat="server" Width="130px" CssClass="DropDownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
                                <TD align="center">E - Mail<asp:regularexpressionvalidator id="Regularexpressionvalidator3" runat="server" ControlToValidate="txtEMail" ErrorMessage="Please Fill Valid E-mail"
										ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></TD>
								<TD colSpan="1"><asp:textbox id="txtEMail" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox></TD>
                                
								
							</TR>
							<TR>
                                <TD>State Office</TD>
								<TD><asp:dropdownlist id="dropstateoffice" runat="server" Width="130px" CssClass="DropDownlist">
										<asp:ListItem Value="APSO">APSO</asp:ListItem>
										<asp:ListItem Value="BSO">BSO</asp:ListItem>
										<asp:ListItem Value="DSO">DSO</asp:ListItem>
										<asp:ListItem Value="GJSO">GJSO</asp:ListItem>
										<asp:ListItem Value="KASO">KASO</asp:ListItem>
										<asp:ListItem Value="KESO">KESO</asp:ListItem>
										<asp:ListItem Value="MHSO">MHSO</asp:ListItem>
										<asp:ListItem Value="MPSO">MPSO</asp:ListItem>
										<asp:ListItem Value="NESO">NESO</asp:ListItem>
										<asp:ListItem Value="OSO">OSO</asp:ListItem>
										<asp:ListItem Value="PBSO">PBSO</asp:ListItem>
										<asp:ListItem Value="RJSO">RJSO</asp:ListItem>
										<asp:ListItem Value="TNSO">TNSO</asp:ListItem>
										<asp:ListItem Value="WBSO">WBSO</asp:ListItem>
										<asp:ListItem Value="UPSO">UPSO</asp:ListItem>
										<asp:ListItem></asp:ListItem>
									</asp:dropdownlist></TD>
                                <TD align="center">Web Site</TD>
								<TD colSpan="1"><asp:textbox id="TxtWebsite" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox></TD>
								
                                
							</TR>
							<TR>
								<TD>Auth. Person</TD>
								<TD><asp:textbox id="txtfood" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox></TD>
                                <TD align="center">Entry Tax</TD>
								<TD valign="middle"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtentry" runat="server"
										Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox>%</TD>
							</TR>
							<TR>
								
								
							</TR>
							<TR>
								<TD>SAP Code</TD>
								<TD><asp:textbox id="TxtWMlic" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox></TD>
                                <TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Start Invoice No
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartInvoiceNo"
										ErrorMessage="Please Enter The Start Invoice No">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtStartInvoiceNo" onkeyup="StartInvoice(this)"
										Width="130px" BorderStyle="Groove" Runat="Server" MaxLength="5" CssClass="DropDownlist"></asp:textbox></TD>
                                

								<!--<TD align="center">VAT Rate</TD>
								<TD vAlign="middle"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtVatRate" runat="server"
										Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox>&nbsp;<STRONG>%</STRONG></TD>-->
							</TR>
							<TR>
                                <td></td>
                                <td></td>
								
								
							</TR>
							<TR>
								<TD>Company Logo</TD>
								<TD colSpan="3"><input class="DropDownList" id="txtFileContents" style="WIDTH: 344px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 20px; BORDER-BOTTOM-STYLE: groove"
										dataSrc="d:\test\" type="file" size="38" name="txtFileContents" Runat="Server"></TD>
							</TR>
							<TR>
								<TD>Message</TD>
								<TD colSpan="3"><asp:textbox id="txtMsg" runat="server" Width="130px" BorderStyle="Groove" CssClass="DropDownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Accounts Period From&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDateFrom" runat="server" Width="130px" BorderStyle="Groove" ReadOnly="True"
										CssClass="DropDownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
								<TD align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;To</TD>
								<TD><asp:textbox id="txtDateTo" runat="server" Width="130px" BorderStyle="Groove" ReadOnly="True"
										CssClass="DropDownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTo);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4">
									<HR width="100.09%" color="#000099" SIZE="1">
									&nbsp;
									<asp:button id="btnUpdate" runat="server" Width="90px" Text="Save Profile"
										 onclick="btnUpdate_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 102; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
