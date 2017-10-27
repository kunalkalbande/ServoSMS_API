<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Master.Supplier_Entry" CodeFile="Supplier_Entry.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Vendor Entry</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script id="Validations" language="javascript" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="Beat" src="../../Sysitem/JS/Beat.js"></script>
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
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<asp:TextBox id="txtbeatname" Runat="server" Height="1" Width="1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px"></asp:TextBox>
			<asp:TextBox id="TextBox1" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" runat="server"
				Width="1px" Visible="False"></asp:TextBox>
			<table width="778" height="290" align="center" cellpadding="0" cellspacing="0">
				<TR>
					<TH align="center">
						<font color="#ce4848">Vendor&nbsp;Entry</font><hr>
					</TH>
				</TR>
				<tr>
					<td align="center" valign="top">
						<TABLE cellpadding="0" cellspacing="0">
							<TR>
								<TD>Vendor ID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:label id="lblSupplierID" Width="108px" ForeColor="Blue" Runat="server"></asp:label></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>
								</TD>
								<TD>
									First Name</TD>
								<TD>
									<P>Middle&nbsp;Name</P>
								</TD>
								<TD>
									Last Name</TD>
							</TR>
							<TR>
								<TD>
									Name&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtFName" ErrorMessage="Please Fill Supplier Name">*</asp:RequiredFieldValidator></TD>
								<TD><asp:textbox id="txtFName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="20" CssClass="DropDownList"></asp:textbox></TD>
								<TD><asp:textbox id="txtMName" runat="server" Width="127px" BorderStyle="Groove" MaxLength="15" CssClass="DropDownList"></asp:textbox></TD>
								<TD><asp:textbox id="txtLName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="15" CssClass="DropDownList"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									Type&nbsp;&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="DropType" ErrorMessage="Please Select Customer Type"
										ValueToCompare="Select" Operator="NotEqual">*</asp:CompareValidator></TD>
								<TD><asp:dropdownlist id="DropType" runat="server" Width="132px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Lubricants">Lubricants</asp:ListItem>
										<asp:ListItem Value="Misc.">Misc.</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>GSTIN No.<FONT color="#ff0033"> *</FONT>&nbsp;<asp:RegularExpressionValidator id="Regularexpressionvalidator2" runat="server" ErrorMessage="Tin No. must be of 15 Digits"
										ControlToValidate="txtTinNo" ValidationExpression="^[a-zA-Z0-9]+$">*</asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter The Tin No"
										ControlToValidate="txtTinNo">*</asp:RequiredFieldValidator>
								</TD>
								<TD>
									<asp:textbox  id="txtTinNo" runat="server" Width="130px"
										BorderStyle="Groove" MaxLength="15" CssClass="DropDownList"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									City&nbsp;&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:CompareValidator id="CompareValidator2" runat="server" ControlToValidate="DropCity" ErrorMessage="Please Select City"
										ValueToCompare="Select" Operator="NotEqual">*</asp:CompareValidator></TD>
								<TD><asp:dropdownlist id="DropCity" runat="server" Width="130px" AutoPostBack="false" onchange="getbeatInfo(this,document.Form1.DropState,document.Form1.DropCountry);"
										CssClass="DropDownList" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>Office<asp:RegularExpressionValidator id="Regularexpressionvalidator6" runat="server" ErrorMessage="Contact No. Between 6-10 Digits"
										ControlToValidate="txtPhoneOff" ValidationExpression="\d{6,12}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtPhoneOff" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="12" CssClass="DropDownList" ontextchanged="txtPhoneOff_TextChanged"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									District&nbsp;</TD>
								<TD><asp:dropdownlist id="DropState" runat="server" Width="130px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>Residence<asp:RegularExpressionValidator id="Regularexpressionvalidator7" runat="server" ErrorMessage="Contact No. Between 6-10 Digits"
										ControlToValidate="txtPhoneRes" ValidationExpression="\d{6,12}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtPhoneRes" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="12" CssClass="DropDownList"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									State</TD>
								<TD><asp:dropdownlist id="DropCountry" runat="server" Width="130px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>Mobile<asp:RegularExpressionValidator id="Regularexpressionvalidator8" runat="server" ErrorMessage="Mobile No. Between 10-12 Digits"
										ControlToValidate="txtMobile" ValidationExpression="\d{10,12}">*</asp:RegularExpressionValidator></TD>
								<TD>
									<asp:textbox id="txtMobile" onkeypress="return GetOnlyNumbers(this, event);" runat="server" Width="130px"
										BorderStyle="Groove" MaxLength="12" CssClass="DropDownList"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									Address</TD>
								<TD colSpan="3"><asp:textbox id="txtAddress" runat="server" Width="130px" BorderStyle="Groove" Font-Names="Verdana"
										MaxLength="99" CssClass="DropDownList"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Op. Balance&nbsp;
									<asp:RegularExpressionValidator id="RegularExpressionValidator5" runat="server" ErrorMessage="Opening Balance Should be Correct"
										ControlToValidate="txtOpBalance" ValidationExpression="(\d+\.\d+)|(\d+)">*</asp:RegularExpressionValidator></TD>
								<TD><asp:textbox id="txtOpBalance" runat="server" Width="130px" BorderStyle="Groove" MaxLength="8"
										CssClass="DropDownList" onkeypress="return GetOnlyNumbers(this, event,false,true);"></asp:textbox></TD>
								<TD>
									<asp:DropDownList id="DropBal" Runat="server" CssClass="DropDownList">
										<asp:ListItem Value="Cr.">Cr.</asp:ListItem>
										<asp:ListItem Value="Dr.">Dr.</asp:ListItem>
									</asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Credit Days</TD>
								<TD>
									<asp:dropdownlist id="DropCrDay" Runat="server" Width="72px" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>
									E - Mail&nbsp;
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtEMail" ErrorMessage="Please Fill Valid E-mail"
										ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></TD>
								<TD colSpan="2"><asp:textbox id="txtEMail" runat="server" Width="130px" BorderStyle="Groove" MaxLength="49" CssClass="DropDownList"></asp:textbox></TD>
								<TD align="right">
									<asp:button id="btnUpdate" runat="server" Width="90px" Text="Save Profile" onclick="btnUpdate_Click"></asp:button>
								</TD>
							</TR>
						</TABLE>
						<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:ValidationSummary>
					</td>
				</tr>
			</table>
			<uc1:Footer id="Footer1" runat="server"></uc1:Footer>
		</form>
	</body>
</HTML>
