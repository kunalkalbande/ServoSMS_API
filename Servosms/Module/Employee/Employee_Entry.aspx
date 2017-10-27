<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Employee.Employee_Entry" CodeFile="Employee_Entry.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Employee Entry</title><!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
			};
		}
		if(document.getElementById("STM0_0__0___")!=null)
			window.onload=change();
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="txtbeatname" style="Z-INDEX: 101; LEFT: 176px; POSITION: absolute; TOP: 24px"
				Runat="server"  Width="160px"></asp:textbox>
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Employee Entry</font>
						<hr>
					</TH>
				</TR>
				<tr>
					<td align="center">
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD colSpan="4"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD>Employee ID</TD>
								<TD><asp:label id="LblEmployeeID" Runat="server" Width="120px" ForeColor="Blue"></asp:label></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD>First Name</TD>
								<TD>Middle Name</TD>
								<TD>Last Name</TD>
							</TR>
							<TR>
								<TD>Name&nbsp;<FONT color="#ff0000">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtFName" ErrorMessage="Please Fill Employee Name">*</asp:requiredfieldvalidator></TD>
								<TD><asp:textbox id="txtFName" runat="server" Width="150px" BorderStyle="Groove" MaxLength="20" CssClass="dropdownlist"></asp:textbox></TD>
								<TD><asp:textbox id="txtMName" runat="server" Width="150px" BorderStyle="Groove" MaxLength="15" CssClass="dropdownlist" ontextchanged="txtMName_TextChanged"></asp:textbox></TD>
								<TD><asp:textbox id="txtLName" runat="server" Width="130px" BorderStyle="Groove" MaxLength="15" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Address</TD>
								<TD colSpan="3"><asp:textbox id="txtAddress" runat="server" Width="150px" BorderStyle="Groove" MaxLength="99"
										CssClass="fontstyle" TextMode="SingleLine" Font-Names="Verdana"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>City&nbsp;<FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="DropCity" ErrorMessage="Please Select City"
										ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></TD>
								<TD colSpan="1"><asp:dropdownlist id="DropCity" runat="server" Width="150px" CssClass="dropdownlist" AutoPostBack="false"
										onchange="getbeatInfo(this,document.Form1.DropState,document.Form1.DropCountry);" onselectedindexchanged="DropCity_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">Contact No
									<asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtContactNo"
										ErrorMessage="Contact No. Between 6-10 Digits" ValidationExpression="\d{6,12}">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtContactNo" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="12" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>District</TD>
								<TD colSpan="1"><asp:dropdownlist id="DropState" runat="server" Width="150px" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD align="center">Mobile No
									<asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobile" ErrorMessage="Mobile No. Between 10-12 Digits"
										ValidationExpression="\d{10,12}">*</asp:regularexpressionvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event);" id="txtMobile" runat="server" Width="130px"
										BorderStyle="Groove" MaxLength="12" CssClass="fontstyle"></asp:textbox></TD>
							<tr>
								<td>State</td>
								<TD colSpan="1"><asp:dropdownlist id="DropCountry" runat="server" Width="150px" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<td align="center">Status</td>
								<TD><asp:radiobutton id="RbtnActive" Runat="server" Text="Active" GroupName="Status" Checked="True"></asp:radiobutton>&nbsp;<asp:radiobutton id="RbtnNone" Runat="server" Text="None" GroupName="Status"></asp:radiobutton></TD>
							</tr>
							<TR>
								<TD>E - Mail&nbsp;<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtEMail" ErrorMessage="Please Type Valid E-Mail"
										ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></TD>
								<TD colSpan="3"><asp:textbox id="txtEMail" runat="server" Width="150px" BorderStyle="Groove" MaxLength="50" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD vAlign="middle">Designation <FONT color="#ff0000">*</FONT>
									<asp:comparevalidator id="CompareValidator5" runat="server" ControlToValidate="DropDesig" ErrorMessage="Please Select Designation"
										ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></TD>
								<TD vAlign="top" colSpan="2"><asp:dropdownlist id="DropDesig" runat="server" Width="190px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="DropDesig_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Accountant">Accountant</asp:ListItem>
										<asp:ListItem Value="Computer Operator">Computer Operator</asp:ListItem>
										<asp:ListItem Value="Driver">Driver</asp:ListItem>
										<asp:ListItem Value="Godown Incharge">Godown Incharge</asp:ListItem>
										<asp:ListItem Value="Godown Keeper">Godown Keeper</asp:ListItem>
										<asp:ListItem Value="Guard">Guard</asp:ListItem>
										<asp:ListItem Value="Labour">Labour</asp:ListItem>
										<asp:ListItem Value="Manager">Manager</asp:ListItem>
										<asp:ListItem Value="Servo Sales Representative">Servo Sales Representative</asp:ListItem>
									</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Op. Balance</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtopbal" runat="server"
										Width="90px" BorderStyle="Groove" MaxLength="8" CssClass="dropdownlist"></asp:textbox><asp:dropdownlist id="DropType" runat="server" Width="39px" CssClass="dropdownlist">
										<asp:ListItem Value="Dr">Dr</asp:ListItem>
										<asp:ListItem Value="Cr">Cr</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblDrLicense" runat="server" Font-Size="8pt">Driving License No.</asp:label></TD>
								<TD><asp:textbox id="txtLicenseNo" runat="server" BorderStyle="Groove" MaxLength="20" CssClass="dropdownlist"></asp:textbox></TD>
								<TD align="center"><asp:label id="lblLicenseVali" runat="server" Font-Size="8pt">Validity In</asp:label></TD>
								<TD><asp:textbox id="txtLicenseValidity" runat="server" Width="130px" BorderStyle="Groove" MaxLength="10"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblLICPolicy" runat="server" Font-Size="8pt">Driver LIC Policy No.</asp:label></TD>
								<TD><asp:textbox id="txtLICNo" runat="server" Width="130px" BorderStyle="Groove" MaxLength="30" CssClass="dropdownlist"></asp:textbox></TD>
								<TD align="center"><FONT color="#ff0000"><asp:label id="lblLICValid" runat="server" ForeColor="Black" Font-Size="8pt">Validity In</asp:label></FONT></TD>
								<TD><asp:textbox id="txtLICvalidity" runat="server" Width="130px" BorderStyle="Groove" MaxLength="10"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblVehicleNo" runat="server" Font-Size="8pt">Vehicle No</asp:label></TD>
								<TD><asp:dropdownlist id="DropVehicleNo" runat="server" Width="133px" CssClass="dropdownlist" onselectedindexchanged="DropVehicleNo_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>Salary&nbsp;<FONT color="#ff0000">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtSalary" ErrorMessage="Please Fill Salary of Employee">*</asp:requiredfieldvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtSalary" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="8" CssClass="dropdownlist"></asp:textbox></TD>
								<TD>OT Compensation<FONT color="#ff0000"> &nbsp;&nbsp;Rs./ Hour </FONT>
								</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtOT_Comp" runat="server"
										Width="130px" BorderStyle="Groove" MaxLength="8" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="4"><asp:button id="btnUpdate" runat="server" Width="90px"  Text="Save Profile"
										 onclick="btnUpdate_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" Height="4px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
