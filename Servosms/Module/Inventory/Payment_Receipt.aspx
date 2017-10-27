<%@ Page language="c#" Inherits="Servosms.Module.Inventory.Payment_Receipt" CodeFile="Payment_Receipt.aspx.cs"  EnableEventValidation="false"%>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Receipt</title><!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
        <script type = "text/javascript">
        //function getDateFilter()
            function getDateFilter(windowWidth,windowHeight)
            {	                                
                var centerLeft = parseInt((window.screen.availWidth - windowWidth) / 2);
                var centerTop = parseInt((window.screen.availHeight - windowHeight) / 2);
                var misc_features = 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=yes, copyhistory=no';

                var windowFeatures = 'width=' + windowWidth + ',height=' + windowHeight + ',left=' + centerLeft + ',top=' + centerTop + misc_features;
                
                childWin=window.open("Payment_ReceiptDateFilter.aspx", "ChildWin", windowFeatures);	                                
                childWin.focus();
            }            
        </script>

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
function GetFinalDues()
{
	var f=document.Form1
	
	f.txtFinalDues.value=""
	f.txtCr.value=""
	f.Textbox2.value=""
	f.Textbox3.value=""
	//alert(f.txtTotalBalance.value);
	//**********
	var discount=0;
	if(f.txtDisc1.value!="")
		discount+=eval(f.txtDisc1.value)
	if(f.txtDisc2.value!="")
		discount+=eval(f.txtDisc2.value)
	//**********
	if(f.txtRecAmount.value!="" && f.txtTotalBalance.value !="" )
	{
		//f.txtRecAmount.value=eval(f.txtRecAmount.value)+eval(discount);
		
		if(eval(f.txtTotalBalance.value)<=eval(f.txtRecAmount.value)+eval(discount))
		{
			f.txtFinalDues.value=eval(f.txtRecAmount.value)+eval(discount)-eval(f.txtTotalBalance.value)
			
			if(f.txtFinalDues.value.isNaN != true)
			{
				f.txtCr.value="Cr."
			}
			else
			{
			    f.txtFinalDues.value="";
			    f.txtCr.value="";
			}
		}
		else
		{
			f.txtFinalDues.value=eval(f.txtTotalBalance.value)-(eval(f.txtRecAmount.value)+eval(discount))
			if(f.txtFinalDues.value.isNaN != true)
				f.txtCr.value="Dr."
			else
			{
			     f.txtFinalDues.value="";
			     f.txtCr.value="";
			}
		}
	
		makeRound(f.txtFinalDues);
		f.Textbox3.value=f.txtCr.value
		if(f.DropReceiptNo==null)
		{
			f.Textbox2.value=f.txtFinalDues.value
			f.Textbox1.value=eval(f.txtRecAmount.value)+eval(discount)
			makeRound(f.Textbox1);
			f.lblAmountinWords.value=eval(f.txtRecAmount.value)+eval(discount)+"/-"
		}
		else
		{
			var str=(eval(f.txtRecAmount.value)+eval(discount)) - <%=RecAmt%>
			var str1=f.txtTotalBalance.value - str
			//alert("str : "+str+",str1 : "+str1)
			f.txtFinalDues.value=str1
			makeRound(f.txtFinalDues);
			f.Textbox2.value=f.txtFinalDues.value
			f.Textbox1.value=eval(str)
			makeRound(f.Textbox1);
			f.lblAmountinWords.value=eval(str)+"/-"
		}
	}
}

function makeRound(t)
{
	var str = t.value;
	//alert(str);
	if(str != "")
	{
		str = eval(str)*100;
		str  = Math.round(str);
		str = eval(str)/100;
		t.value = str;
		//alert(str)
	}
}

function chkSelect(t)
{
	if(t.value=="Select")
	{
		document.Form1.txtDisc1.disabled=true;
		document.Form1.txtDisc1.value="";
		GetFinalDues();
	}
	else
		document.Form1.txtDisc1.disabled=false;
}

function chkSelect1(t)
{
	if(t.value=="Select")
	{
		document.Form1.txtDisc2.disabled=true;
		document.Form1.txtDisc2.value="";
		GetFinalDues();
	}
	else
		document.Form1.txtDisc2.disabled=false;
}

function GetInfo(t)
{
	if(t.value!="" || t.value!="Select")
	{
		document.Form1.submit();
	}
}

function GetInfo1(t,e)
{
	var key
	if(window.event) 
	{
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	else if(e.which) 
	{
		key = e.which;
		isCtrl = e.ctrlKey;
	}
	if(key==13)
	{
		if(t.value!="" && t.value!="Select")
		{
			<%GetInfo();%>
			document.Form1.submit();
		}
	}
}

function CheckReceipt(t)
{
	if(t.value!="" || t.value!="Select")
	{
		if(document.Form1.DropReceiptNo!=null)
		{
			if(document.Form1.DropReceiptNo.value=="Select")
				alert("Java Script : Please Select The Receipt No")
		}
	}
}
		</script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<asp:HiddenField ID="hidReceiptFromDate" runat="server" />
            <asp:HiddenField ID="hidReceiptToDate" runat="server" />
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox4" style="Z-INDEX: 101; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="8px"></asp:textbox><INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<TR>
					<TH style="HEIGHT: 53px" colSpan="5">
						&nbsp;<font color="#ce4848">Receipt</font>
						<HR>
						<asp:label id="lblMessage" runat="server" Width="300px" Font-Size="8pt" Font-Bold="True" Forecolor="#CE4848"></asp:label></TH></TR>
				<asp:panel id="PanReceiptNo" Runat="server">
					<TR>
						<TD></TD>
						<TD>Receipt No&nbsp;
							<asp:RequiredFieldValidator id="rfv1" Runat="server" InitialValue="Select" ErrorMessage="Please Select The Receipt No"
								ControlToValidate="DropReceiptNo">*</asp:RequiredFieldValidator></TD>
						<TD>
							<asp:DropDownList id="DropReceiptNo" runat="server" Width="150" AutoPostBack="True" CssClass="DropDownList" onselectedindexchanged="DropReceiptNo_SelectedIndexChanged"></asp:DropDownList>&nbsp;
						</TD>
						<TD colSpan="2"></TD>
					</TR>
				</asp:panel>
				<tr>
					<td></td>
					<td>Receipt No</td>
					<td><asp:label id="lblId" Runat="server" ForeColor="blue"></asp:label></td>
					<td></td>
				</tr>
				<TR>
					<TD width="12%"></TD>
					<TD style="HEIGHT: 1px" width="20%">Received with thanks from&nbsp;<FONT color="#ff0000">*</FONT>&nbsp;<asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="DropCustName" ErrorMessage="Please Select Party Name"
							ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></TD>
					<td width="30%"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropCustName"
							onkeyup="search3(this,document.Form1.DropProdName,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName,event,document.Form1.DropCustName,document.Form1.txtRecAmount),GetInfo1(this,event)"
							style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 220px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							value="Select" name="DropCustName" runat="server" onserverchange="DropCustName_ServerChange"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProdName,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName)"
							readOnly type="text" name="temp"><br>
						<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onmousemove="MouseMove(this)" onkeypress="Selectbyenter(this,event,document.Form1.DropCustName,document.Form1.txtRecAmount),GetInfo(this)"
								id="DropProdName" ondblclick="select(this,document.Form1.DropCustName),GetInfo(this)" onkeyup="arrowkeyselect(this,event,document.Form1.txtRecAmount,document.Form1.DropCustName)"
								style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 240px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.DropCustName),GetInfo(this)"
								multiple name="DropProdName" type="select-one"></select></div>
					</td>
					<td vAlign="top"><asp:textbox id="txtCity" runat="server" Width="120px" CssClass="DropDownList" Height="19px"
							BorderStyle="Groove"></asp:textbox></td>
					</TD>
					<td></td>
				</TR>
				<TR>
					<TD></TD>
					<TD colSpan="3">The sum of Rupees&nbsp;&nbsp;<asp:textbox id="lblAmountinWords" Width="80" Runat="server" CssClass="dropdownlist" 
							BorderStyle="Groove" ReadOnly="True"></asp:textbox>&nbsp;in&nbsp;Full / 
						Part&nbsp;&nbsp;payment against Bill details given on account of your supply.</TD>
					<TD style="HEIGHT: 20px"></TD>
				</TR>
				<tr>
					<td></td>
					<td colSpan="4">Received 
						Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtReceivedDate" Width="80" Runat="server" CssClass="dropdownlist" BorderStyle="Groove"
							ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtReceivedDate);return false;"><IMG class="PopcalTrigger" id="Img2" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
								align="absMiddle" border="0" runat="server"></A></td>
				</tr>
				<tr>
					<td></td>
					<td colSpan="3">
						<TABLE cellSpacing="0" cellPadding="0" width="700" border="1">
							<TR>
								<TD align="center" width="25%">Due Payment</TD>
								<TD align="center" width="55%">Received Payment</TD>
								<TD align="center" width="20%">Final Dues After Payment</TD>
							</TR>
							<TR>
								<TD vAlign="top"><asp:datagrid id="GridDuePayment" runat="server" Width="100%" CellSpacing="1" HorizontalAlign="Center"
										AutoGenerateColumns="False" onselectedindexchanged="GridDuePayment_SelectedIndexChanged">
										<HeaderStyle Font-Size="Large" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="invoice_no" HeaderText="Bill No">
												<HeaderStyle Width="60px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="invoice_date" HeaderText="Bill Date" DataFormatString="{0:dd-MM-yyyy}">
												<HeaderStyle Width="150px"></HeaderStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="balance" HeaderText="Amount" DataFormatString="{0:N2}">
												<HeaderStyle Width="75px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid></TD>
								<TD vAlign="top" align="center">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<TR>
											<TD align="center" width="50%">Mode</TD>
											<TD align="center" width="50%">Amount&nbsp;<FONT color="#ff0000">*</FONT>
												<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtRecAmount" ErrorMessage="Please Fill Received Amount">*</asp:requiredfieldvalidator></TD>
										</TR>
										<TR>
											<TD><asp:dropdownlist id="DropMode" runat="server" Width="40%" CssClass="DropDownList" AutoPostBack="True" onselectedindexchanged="DropMode_SelectedIndexChanged">
													<asp:ListItem Value="Cash">Cash</asp:ListItem>
													<asp:ListItem Value="Cheque">Cheque</asp:ListItem>
													<asp:ListItem Value="DD">DD</asp:ListItem>
													<asp:ListItem Value="Pay Order">Pay Order</asp:ListItem>
                                                    <asp:ListItem Value="NEFT">NEFT</asp:ListItem>
                                                    <asp:ListItem Value="RTGS">RTGS</asp:ListItem>
                                                    <asp:ListItem Value="IMPS">IMPS</asp:ListItem>
												</asp:dropdownlist></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtRecAmount" onblur="GetFinalDues()"
													runat="server" Width="100%" CssClass="FontStyle" BorderStyle="Groove" MaxLength="8"></asp:textbox></TD>
										</TR>
										<tr>
											<td><asp:dropdownlist id="DropDiscount1" Width="100%" Runat="server" CssClass="dropdownlist" onchange="chkSelect(this);">
													<asp:ListItem Value="Select">Select</asp:ListItem>
												</asp:dropdownlist></td>
											<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtDisc1" onblur="GetFinalDues()"
													Width="100%" Runat="server" CssClass="FontStyle" BorderStyle="Groove" MaxLength="8"></asp:textbox></td>
										</tr>
										<tr>
											<td><asp:dropdownlist id="DropDiscount2" Width="100%" Runat="server" CssClass="dropdownlist" onchange="chkSelect1(this);" onselectedindexchanged="DropDiscount2_SelectedIndexChanged">
													<asp:ListItem Value="Select">Select</asp:ListItem>
												</asp:dropdownlist></td>
											<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtDisc2" onblur="GetFinalDues()"
													Width="100%" Runat="server" CssClass="FontStyle" BorderStyle="Groove" MaxLength="8"></asp:textbox></td>
										</tr>
										<asp:panel id="PanBankInfo" runat="server">
											<TR> <!--TD>Bank Name</TD>
												<TD>
													<asp:textbox id="txtBankName" runat="server" Width="96px" CssClass="DropDownList" BorderStyle="Groove"></asp:textbox></TD-->
												<TD style="HEIGHT: 24px">&nbsp;Bank A/C&nbsp;
													<asp:CompareValidator id="cv1" Runat="server" ErrorMessage="Please Select The Bank" ControlToValidate="DropBankName"
														Operator="NotEqual" ValueToCompare="Select">*</asp:CompareValidator></TD>
												<TD style="HEIGHT: 24px">
													<asp:DropDownList id="DropBankName" Width="100%" Runat="server" CssClass="FontStyle">
														<asp:ListItem Value="Select">Select</asp:ListItem>
													</asp:DropDownList></TD>
											</TR>
											<TR>
												<TD>&nbsp;Cust. Bank Name</TD>
												<TD>
													<asp:TextBox id="txtCustBankName" Width="100%" Runat="server" CssClass="FontStyle" BorderStyle="Groove"
														MaxLength="49"></asp:TextBox></TD>
											</TR>
											<TR>
												<TD>&nbsp;Cheque No</TD>
												<TD>
													<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtChequeno" runat="server"
														Width="100%" CssClass="FontStyle" BorderStyle="Groove" MaxLength="8"></asp:textbox></TD>
											</TR>
											<TR>
												<TD>&nbsp;Date</TD>
												<TD height="10">
													<asp:textbox id="txtDate" runat="server" Width="70px" CssClass="FontStyle" BorderStyle="Groove"
														ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDate);return false;"><IMG class="PopcalTrigger" id="Img1" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
															align="absMiddle" border="0" runat="server"></A></TD>
											</TR>
										</asp:panel></TABLE>
								</TD>
								<TD align="center">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD height="17">&nbsp;</TD>
										</TR>
										<TR>
											<TD align="right">&nbsp;&nbsp;&nbsp;<asp:textbox id="txtCr" runat="server" Width="96px" CssClass="FontStyle" BorderStyle="Groove"
													ReadOnly="True"></asp:textbox><asp:textbox id="txtFinalDues" runat="server" Width="96px" CssClass="FontStyle" BorderStyle="Groove"
													ReadOnly="True"></asp:textbox></TD>
										</TR>
										<TR>
											<TD></TD>
										</TR>
										<TR>
											<TD></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD align="right">&nbsp;&nbsp;&nbsp;&nbsp;Total&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:textbox id="txtTotalBalance" runat="server" Width="96px" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox></TD>
								<TD align="right"><asp:textbox id="Textbox1" runat="server" Width="50%" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox></TD>
								<TD align="right"><asp:textbox id="Textbox3" runat="server" Width="96px" CssClass="FontStyle" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox><asp:textbox id="Textbox2" runat="server" Width="96px" CssClass="DropDownList" BorderStyle="Groove"
										ReadOnly="True"></asp:textbox></TD>
							</TR>
							<tr>
								<td align="center">Narration</td>
								<td colSpan="3"><asp:textbox id="txtNar" Width="30%" Runat="server" CssClass="FontStyle" Height="20" BorderStyle="Groove"
										MaxLength="149"></asp:textbox></td>
							</tr>
						</TABLE>
					</td>
					<td></td>
				</tr>
				<TR>
					<TD></TD>
					<TD align="right" colSpan="3"><asp:button id="btnSave" runat="server" Width="70px" Text="Save" onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnEdit" runat="server" Width="70px" 
							 Text="Edit" CausesValidation="False" onClientClick="return getDateFilter(450, 300)"></asp:button>&nbsp;&nbsp;<asp:button id="btnPrint" runat="server" Width="70px" 
							 Text="Print" onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnDel" runat="server" Width="70px" 
							 Text="Delete" onclick="btnDel_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
					<TD></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="right" colSpan="3"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD>
					<TD></TD>
				</TR>
			</table>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
