<%@ Page language="c#" Inherits="Servosms.Module.Accounts.Payment" CodeFile="Payment.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Payment</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
	-->
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
	function CheckLength(t)
	{
		if(t.value.length>99)
		{
			t.value=t.value.substring(0,99);
			alert("Only 100 Charactors Allowed In Narrations");
		}
	}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<asp:textbox id="TextBox1" runat="server" Visible="False" Width="5" style="Z-INDEX: 103; LEFT: 160px; VISIBILITY: hidden; POSITION: absolute; TOP: 8px"
				Height="20"></asp:textbox>
			<input id="tempPaymentID" style="Z-INDEX: 103; LEFT: 160px; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 8px; HEIGHT: 20px"
				type="hidden" runat="server"><INPUT id="texthiddenprod" style="Z-INDEX: 103; LEFT: 160px; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 8px; HEIGHT: 20px"
				type="text" name="texthiddenprod" runat="server">
			<TABLE height="290" width="778" align="center">
				<tr>
					<th style="HEIGHT: 13px" align="center">
						&nbsp;<font color="#ce4848">Payment</font>
						<hr>
					</th>
				</tr>
				<TR>
					<TD align="center">
						<TABLE cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD colSpan="3"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD width="20%">Ledger Name<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" InitialValue="Select" ControlToValidate="DropLedgerName"
										ErrorMessage="Please Select Ledger Name">*</asp:requiredfieldvalidator><FONT color="red"></FONT></TD>
								<asp:Panel ID="panLedgerName1" Runat="server">
									<TD width="30%">
										<asp:dropdownlist id="DropLedgerName1" runat="server" Height="20px" Width="300px" AutoPostBack="True"
											CssClass="dropdownlist" onselectedindexchanged="DropLedgerName1_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist></TD>
								</asp:Panel>
								<td width="50%"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropLedgerName"
										onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropLedgerName,document.Form1.btnSave)"
										style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 280px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										value="Select" name="DropLedgerName" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
										readOnly type="text" name="temp"><br>
									<div id="Layer1" style="Z-INDEX: 101; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropLedgerName,document.Form1.txtAmount)"
											id="DropProdName" ondblclick="select(this,document.Form1.DropLedgerName)" onkeyup="arrowkeyselect(this,event,document.Form1.txtAmount,document.Form1.DropLedgerName)"
											style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 300px; HEIGHT: 0px" multiple name="DropProdName"></select></div>
								</td>
								<td><asp:button id="btnEdit1" runat="server" Width="20px" CausesValidation="False" Text="..." ToolTip="Click here for edit"
										 Height="20px" onclick="btnEdit1_Click"></asp:button></td>
							</TR>
							<tr>
								<td>Date</td>
								<td colspan="3"><asp:textbox id="txtDate" Width="100" CssClass="dropdownlist" Runat="server" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDate);return false;"><IMG class="PopcalTrigger" id="Img2" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0" runat="server"></A></td>
							</tr>
							<TR>
								<TD>By&nbsp;
									<asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" InitialValue="Select" ControlToValidate="DropBy"
										ErrorMessage="Please Select By Account Name">*</asp:requiredfieldvalidator></TD>
								<TD colspan="3"><asp:dropdownlist id="DropBy" runat="server" Width="154px" AutoPostBack="True" CssClass="dropdownlist" onselectedindexchanged="DropBy_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<asp:panel id="pnlBankInfo" Runat="server">
								<TR>
									<TD>Bank Name</TD>
									<TD colSpan="3">
										<asp:textbox id="txtBankname" runat="server" CssClass="dropdownlist" BorderStyle="Groove" MaxLength="49"></asp:textbox>&nbsp; 
										Cheque No.
										<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtCheque" runat="server"
											Width="87px" CssClass="dropdownlist" BorderStyle="Groove" MaxLength="8"></asp:textbox>&nbsp;&nbsp; 
										Date
										<asp:textbox id="txtchkDate" runat="server" Width="70px" CssClass="dropdownlist" ReadOnly="True"
											BorderStyle="Groove"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtchkDate);return false;"><IMG class="PopcalTrigger" id="Img1" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0" runat="server"></A></TD>
								</TR>
							</asp:panel>
							<TR>
								<TD>Amount
									<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="TxtAmount" ErrorMessage="Please Enter Amount ">*</asp:requiredfieldvalidator></TD>
								<TD colspan="3"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtAmount" runat="server"
										CssClass="dropdownlist" BorderStyle="Groove" MaxLength="8"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Narrations&nbsp;&nbsp;&nbsp; <FONT color="#ff0000">&nbsp;&nbsp;</FONT></TD>
								<TD colspan="3"><TEXTAREA id="txtNarrartion" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 60px; BORDER-BOTTOM-STYLE: groove"
										accessKey="txtNarArea" name="TEXTAREA1" rows="1" wrap="soft" cols="35" onchange="CheckLength(this);"
										runat="server"></TEXTAREA></TD>
							</TR>
							<TR>
								<td></td>
								<TD align="left" colSpan="3"><asp:button id="btnSave" runat="server" Width="75px" Text="Save" 
										 onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnEdit" runat="server" Width="75px" Text="Edit" 
										 onclick="btnEdit_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnDelete" runat="server" Width="75px" Text="Delete" 
										 onclick="btnDelete_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnPrint" runat="server" Width="75px" Text="Print" 
										 onclick="btnPrint_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></TD>
				</TR>
			</TABLE>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 102; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
		</FORM>
	</body>
</HTML>
