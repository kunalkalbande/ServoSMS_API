<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Accounts.voucher" CodeFile="voucher.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: Voucher Entry</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="True" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie8" name="vs_targetSchema">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
		<script language="javascript">
function getAcountName(t,t1)
{
    debugger;
	var index = t.selectedIndex;
	var typetext  = t.options[index].text;
	//alert(typetext)
	var mainarr = new Array();
	var temp = "", temp1 = "";
	if(typetext == "Contra")
	{
		temp = document.Form1.txtTempContra.value;
		temp1 = document.Form1.txtTempContra1.value;
		//fillCombo(temp,t1);         
	}
	else if(typetext == "Credit Note")
	{
		temp = document.Form1.txTempCredit.value;
		temp1 = document.Form1.txTempCredit1.value;
		//fillCombo(temp,t1);         
	}
	else if(typetext == "Debit Note")
	{
		temp = document.Form1.txtTempDebit.value;
		temp1 = document.Form1.txtTempDebit1.value;
		//fillCombo(temp,t1);         
	}
	else
	{
		temp = document.Form1.txtTempJournal.value;
		temp1 = document.Form1.txtTempJournal1.value;
		//fillCombo(temp,t1);    
	}
	//**********
	var mainarr = new Array();
	mainarr = temp.split("~");
	t1.value = mainarr[0];
	document.Form1.txtID.value = mainarr[0];
	document.Form1.texthiddenprod.value=temp1;
	//alert(document.Form1.texthiddenprod.value)
	//**********
}
/*
function fillCombo(temp,t1)
{
	alert(temp)
	document.Form1.dropAccName1.length = 1;
	document.Form1.dropAccName2.length = 1;
	document.Form1.dropAccName3.length = 1;
	document.Form1.dropAccName4.length = 1;
	document.Form1.dropAccName5.length = 1;
	document.Form1.dropAccName6.length = 1;
	document.Form1.dropAccName7.length = 1;
	document.Form1.dropAccName8.length = 1;
	document.Form1.txtAccName1.value = "Select"; 
	document.Form1.txtAccName2.value = "Select";
	document.Form1.txtAccName3.value = "Select";
	document.Form1.txtAccName4.value = "Select";
	document.Form1.txtAccName5.value = "Select";
	document.Form1.txtAccName6.value = "Select";
	document.Form1.txtAccName7.value = "Select";
	document.Form1.txtAccName8.value = "Select";
	var mainarr = new Array();
	mainarr = temp.split("~");
	var n=0;
	//document.Form1.txtVouchID.disabled = false; 
	//alert(t1.value);
	//t1.value = "hello";
	t1.value = mainarr[0];
	document.Form1.txtID.value = mainarr[0];
	//alert(document.Form1.txtVouchID.value);
	//document.Form1.txtVouchID.disabled = true;
	for(var i=1;i<mainarr.length-1;i++)
	{
		document.Form1.dropAccName1.add(new Option) 
		document.Form1.dropAccName2.add(new Option) 
		document.Form1.dropAccName3.add(new Option) 
		document.Form1.dropAccName4.add(new Option) 
		document.Form1.dropAccName5.add(new Option) 
		document.Form1.dropAccName6.add(new Option) 
		document.Form1.dropAccName7.add(new Option) 
		document.Form1.dropAccName8.add(new Option) 
		if(mainarr[i]  != "")
		{
			document.Form1.dropAccName1.options[n+1].text=mainarr[i];                  
			document.Form1.dropAccName2.options[n+1].text=mainarr[i]; 
			document.Form1.dropAccName3.options[n+1].text=mainarr[i]; 
			document.Form1.dropAccName4.options[n+1].text=mainarr[i]; 
			document.Form1.dropAccName5.options[n+1].text=mainarr[i]; 
			document.Form1.dropAccName6.options[n+1].text=mainarr[i]; 
			document.Form1.dropAccName7.options[n+1].text=mainarr[i]; 
			document.Form1.dropAccName8.options[n+1].text=mainarr[i]; 
			n = n + 1;
		}
	}
}
*/
function fillCombo(temp,t1)
{
	/*document.Form1.DropAccountName1.length = 1;
	document.Form1.DropAccountName2.length = 1;
	document.Form1.DropAccountName3.length = 1;
	document.Form1.DropAccountName4.length = 1;
	document.Form1.DropAccountName5.length = 1;
	document.Form1.DropAccountName6.length = 1;
	document.Form1.DropAccountName7.length = 1;
	document.Form1.DropAccountName8.length = 1;
	document.Form1.txtAccName1.value = "Select"; 
	document.Form1.txtAccName2.value = "Select";
	document.Form1.txtAccName3.value = "Select";
	document.Form1.txtAccName4.value = "Select";
	document.Form1.txtAccName5.value = "Select";
	document.Form1.txtAccName6.value = "Select";
	document.Form1.txtAccName7.value = "Select";
	document.Form1.txtAccName8.value = "Select";*/
	var mainarr = new Array();
	mainarr = temp.split("~");
	//var n=0;
	//document.Form1.txtVouchID.disabled = false; 
	//alert(t1.value);
	//t1.value = "hello";
	t1.value = mainarr[0];
	document.Form1.txtID.value = mainarr[0];
	//alert(document.Form1.txtVouchID.value);
	//document.Form1.txtVouchID.disabled = true;
	/*for(var i=1;i<mainarr.length-1;i++)
	{
		if(mainarr[i]  != "")
		{
			document.Form1.DropAccountName1.add(new Option) 
			document.Form1.DropAccountName2.add(new Option) 
			document.Form1.DropAccountName3.add(new Option) 
			document.Form1.DropAccountName4.add(new Option) 
			document.Form1.DropAccountName5.add(new Option) 
			document.Form1.DropAccountName6.add(new Option) 
			document.Form1.DropAccountName7.add(new Option) 
			document.Form1.DropAccountName8.add(new Option)
			document.Form1.DropAccountName1.options[n+1].text=mainarr[i];                  
			document.Form1.DropAccountName2.options[n+1].text=mainarr[i]; 
			document.Form1.DropAccountName3.options[n+1].text=mainarr[i]; 
			document.Form1.DropAccountName4.options[n+1].text=mainarr[i]; 
			document.Form1.DropAccountName5.options[n+1].text=mainarr[i]; 
			document.Form1.DropAccountName6.options[n+1].text=mainarr[i]; 
			document.Form1.DropAccountName7.options[n+1].text=mainarr[i]; 
			document.Form1.DropAccountName8.options[n+1].text=mainarr[i];
			n = n + 1;
		}
		
	}*/
}

function changeType(t)
{
	//alert(t.name);
	var index = t.selectedIndex;
	var temp = t.name;
	var arr = new Array();
	var drop = new Array(document.Form1.dropType_1,document.Form1.dropType_2,document.Form1.dropType_3,document.Form1.dropType_4,document.Form1.dropType_5,document.Form1.dropType_6,document.Form1.dropType_7,document.Form1.dropType_8);
	arr = temp.split("_");
	
	if(eval(arr[1]) <= 4)
	{
		//alert(arr[1]);
		if(index == 0)
		{
			drop[(eval(arr[1])+4)-1].selectedIndex = 0;
			//alert(drop[(eval(arr[1])+4)-1].name);
		}
		else
		{
			drop[(eval(arr[1])+4)-1].selectedIndex = 1;
			//alert(drop[(eval(arr[1])+4)-1].name);
		}
	}
	else
	{
		//alert(arr[1]);
		if(index == 0)
		{
			drop[(eval(arr[1])-4)-1].selectedIndex = 0;
		}
		else
		{
			drop[(eval(arr[1])-4)-1].selectedIndex = 1;
		}
	}
	calcTotal(); 
}

function calcTotal()
{
	var LDrTotal = 0;
	var LCrTotal = 0;
	var RDrTotal = 0;
	var RCrTotal = 0;
	if(document.Form1.txtAmount1.value != "")
	{
		document.Form1.txtAmount5.value = document.Form1.txtAmount1.value;
		var index = document.Form1.dropType_1.selectedIndex;
		var typetext = document.Form1.dropType_1.options[index].text;
		if(typetext == "Dr")
			LDrTotal = LDrTotal+ eval(document.Form1.txtAmount1.value);
		else
			LCrTotal = LCrTotal+ eval(document.Form1.txtAmount1.value);
        
	}
	if(document.Form1.txtAmount2.value != "")
	{
		document.Form1.txtAmount6.value = document.Form1.txtAmount2.value;
		var index2 = document.Form1.dropType_2.selectedIndex;
		var typetext2 = document.Form1.dropType_2.options[index2].text;
		if(typetext2 == "Dr")
			LDrTotal = LDrTotal+ eval(document.Form1.txtAmount2.value);
		else
			LCrTotal = LCrTotal+ eval(document.Form1.txtAmount2.value);
	}
	if(document.Form1.txtAmount3.value != "")
	{
		document.Form1.txtAmount7.value = document.Form1.txtAmount3.value;
		var index3 = document.Form1.dropType_3.selectedIndex;
		var typetext3 = document.Form1.dropType_3.options[index3].text;
		if(typetext3 == "Dr")
			LDrTotal = LDrTotal+ eval(document.Form1.txtAmount3.value);
		else
			LCrTotal = LCrTotal+ eval(document.Form1.txtAmount3.value);
    }
	if(document.Form1.txtAmount4.value != "")
	{
		document.Form1.txtAmount8.value = document.Form1.txtAmount4.value;
		var index4 = document.Form1.dropType_4.selectedIndex;
		var typetext4 = document.Form1.dropType_4.options[index4].text;
		if(typetext4 == "Dr")
			LDrTotal = LDrTotal+ eval(document.Form1.txtAmount4.value);
		else
			LCrTotal = LCrTotal+ eval(document.Form1.txtAmount4.value);
    }
	if(document.Form1.txtAmount5.value != "")
	{
		document.Form1.txtAmount1.value = document.Form1.txtAmount5.value;
		var index5 = document.Form1.dropType_5.selectedIndex;
		var typetext5 = document.Form1.dropType_5.options[index5].text;
		if(typetext5 == "Dr")
			RDrTotal = RDrTotal+ eval(document.Form1.txtAmount5.value);
		else
		    RCrTotal = RCrTotal+ eval(document.Form1.txtAmount5.value);
    }
	if(document.Form1.txtAmount6.value != "")
	{
		document.Form1.txtAmount2.value = document.Form1.txtAmount6.value;
		var index6 = document.Form1.dropType_6.selectedIndex;
		var typetext6 = document.Form1.dropType_6.options[index6].text;
		if(typetext6 == "Dr")
			RDrTotal = RDrTotal+ eval(document.Form1.txtAmount6.value);
		else
			RCrTotal = RCrTotal+ eval(document.Form1.txtAmount6.value);
    }
	if(document.Form1.txtAmount7.value != "")
	{
		document.Form1.txtAmount3.value = document.Form1.txtAmount7.value;
		var index7 = document.Form1.dropType_7.selectedIndex;
		var typetext7 = document.Form1.dropType_7.options[index7].text;
		if(typetext7 == "Dr")
			RDrTotal = RDrTotal+ eval(document.Form1.txtAmount7.value);
		else
			RCrTotal = RCrTotal+ eval(document.Form1.txtAmount7.value);
    }
	if(document.Form1.txtAmount8.value != "")
	{
		document.Form1.txtAmount4.value = document.Form1.txtAmount8.value;
		var index8 = document.Form1.dropType_8.selectedIndex;
		var typetext8 = document.Form1.dropType_8.options[index8].text;
		if(typetext8 == "Dr")
			RDrTotal = RDrTotal+ eval(document.Form1.txtAmount8.value);
		else
			RCrTotal = RCrTotal+ eval(document.Form1.txtAmount8.value);
    }
  
	document.Form1.txtLCr.value = LCrTotal;
	makeRound( document.Form1.txtLCr);
	document.Form1.txtLDr.value = LDrTotal;
	makeRound( document.Form1.txtLDr);
	document.Form1.txtRCr.value = RCrTotal;
	makeRound( document.Form1.txtRCr);
	document.Form1.txtRDr.value = RDrTotal;
	makeRound( document.Form1.txtRDr);
  
}
	function makeRound(t)
	{
//	alert(t.value)
	var str = t.value;
	if(str != "")
	{
	str = eval(str)*100;
//	alert(str)
	str  = Math.round(str);
//	alert(str)
	str = eval(str)/100;
//	alert(str)
	t.value = str;
	}
	
	}
	function setValue(t)
	{
	  //var index = t.selectedIndex;
	  //var typetext = t.options[index].text;
	  var typetext = t.value;
	  //alert(t.name+"::"+t.value);
	  if(t.name == "dropAccName1")
	     document.Form1.txtAccName1.value = typetext;
	  if(t.name == "dropAccName2")
	     document.Form1.txtAccName2.value = typetext;
	  if(t.name == "dropAccName3")
	     document.Form1.txtAccName3.value = typetext;
	  if(t.name == "dropAccName4")
		document.Form1.txtAccName4.value = typetext;
	  if(t.name == "dropAccName5")
		document.Form1.txtAccName5.value = typetext;
	  if(t.name == "dropAccName6")
		document.Form1.txtAccName6.value = typetext;
	  if(t.name == "dropAccName7")
		document.Form1.txtAccName7.value = typetext;
	  if(t.name == "dropAccName8")
		document.Form1.txtAccName8.value = typetext;
	}
	function CheckLength(t)
	{
		if(t.value.length>199)
		{
			t.value=t.value.substring(0,199);
			alert("Only 200 Charactors Allowed In Narrations");
		}
	}
		</script>
</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 144px; POSITION: absolute; TOP: 16px" runat="server"
				Visible="False" Width="1px"></asp:textbox><INPUT id="txtTempContra" style="Z-INDEX: 103; LEFT: 160px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 24px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txtTempContra1" style="Z-INDEX: 103; LEFT: 160px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 24px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txTempCredit" style="Z-INDEX: 104; LEFT: 176px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txTempCredit1" style="Z-INDEX: 104; LEFT: 176px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txtTempDebit" style="Z-INDEX: 105; LEFT: 192px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 24px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txtTempDebit1" style="Z-INDEX: 105; LEFT: 192px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 24px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txtTempJournal" style="Z-INDEX: 106; LEFT: 208px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txtTempJournal1" style="Z-INDEX: 106; LEFT: 208px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				name="texthiddenprod" runat="server">
			<TABLE height="290" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<tr>
					<th align="center">
						<font color="#ce4848">Voucher Entry</font>
						<hr>
					</th>
				</tr>
				<TR>
					<TD vAlign="middle" align="center">
						<TABLE cellSpacing="0" cellPadding="2" align="center" border="0">
							<TBODY>
								<TR vAlign="top">
									<TD colSpan="6"><FONT color="#ff0000">Fields Marked as (*) Are 
											Mandatory&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:label id="lblVid" runat="server" Width="66px"></asp:label></FONT></TD>
								</TR>
								<TR>
									<TD>Voucher Type <FONT color="#ff0000">*</FONT>
										<asp:comparevalidator id="Comparevalidator5" runat="server" ErrorMessage="Please Select Voucher type"
											ControlToValidate="DropVoucherName" Operator="NotEqual" ValueToCompare="Select">*</asp:comparevalidator><asp:dropdownlist id="DropVoucherName" runat="server" Width="130px" 
											CssClass="FontStyle" onselectedindexchanged="DropVoucherName_SelectedIndexChanged" AutoPostBack="True">
											<asp:ListItem Value="Select">Select</asp:ListItem>
											<asp:ListItem Value="Contra">Contra</asp:ListItem>
											<asp:ListItem Value="Credit Note">Credit Note</asp:ListItem>
											<asp:ListItem Value="Debit Note">Debit Note</asp:ListItem>
											<asp:ListItem Value="Journal">Journal</asp:ListItem>
										</asp:dropdownlist></TD>
									<TD align="center" colspan="2">&nbsp;Voucher ID <FONT color="#ff0000">*</FONT>
										<asp:comparevalidator id="Comparevalidator6" runat="server" ErrorMessage="Please Select Voucher ID " ControlToValidate="DropDownID"
											Operator="NotEqual" ValueToCompare="Select" ForeColor="White">*</asp:comparevalidator>
									<asp:dropdownlist id="DropDownID" runat="server" Width="69px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="DropDownID_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist><INPUT class="dropdownlist" id="txtVouchID" style="WIDTH: 55px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
											disabled size="6" name="txtVouchID" runat="server"><asp:button id="btnEdit1" runat="server" 
											ToolTip="click here for Edit" CausesValidation="False" Text="..." onclick="btnEdit1_Click"></asp:button><INPUT id="txtID" style="WIDTH: 9px; HEIGHT: 22px" type="hidden" size="1" name="txtID"
											runat="server"></TD>
									<TD align="center">Voucher Date <FONT color="#ff0000">*</FONT>
										<asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="Please Enter Voucher Date"
											ControlToValidate="txtDate" ForeColor="White">*</asp:requiredfieldvalidator></TD>
									<TD colSpan="2"><asp:textbox id="txtDate" runat="server" Width="84px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox>&nbsp;&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
												align="absMiddle" border="0"></A></TD>
								</TR>
                              
								<tr>
									<td align="center"><FONT color="#990066">Account Name</FONT></td>
									<td align="center"><FONT color="#990066">Amount</FONT></td>
									<td>&nbsp;</td>
									<td align="center"><FONT color="#990066">Account Name</FONT></td>
									<td align="center"><FONT color="#990066">Amount</FONT></td>
									<td></td>
								</tr>
                                
								<tr>
									<td >
                                        <input 
											style="VISIBILITY: hidden; WIDTH: 250px; HEIGHT: 0px" />
                                        <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName1"
											onkeyup="search3(this,document.Form1.DropAccountName1,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName1,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName1,event,document.Form1.dropAccName1,document.Form1.txtAmount1),setValue(document.Form1.dropAccName1)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName1)"
											value="Select" name="dropAccName1" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName1)"
											readOnly name="temp"><br>
										<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName1,document.Form1.txtAmount1),setValue(document.Form1.dropAccName1)"
												id="DropAccountName1" ondblclick="select(this,document.Form1.dropAccName1),setValue(document.Form1.dropAccName1)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName2,document.Form1.dropAccName1),setValue(document.Form1.dropAccName1)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName1)" multiple name="DropAccountName1"></select></div>
										<INPUT class="dropdownlist" id="txtAccName1" style="WIDTH: 124px; HEIGHT: 19px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount1" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_1" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
										</asp:dropdownlist></td>
									<td>
                                        <input 
											style="VISIBILITY: hidden; WIDTH: 250px; HEIGHT: 0px" />
                                        <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName5"
											onkeyup="search3(this,document.Form1.DropAccountName5,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName5,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName5,event,document.Form1.dropAccName5,document.Form1.txtAmount5),setValue(document.Form1.dropAccName5)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName5)"
											value="Select" name="dropAccName5" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName5)"
											readOnly name="temp"><br>
										<div id="Layer5" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName5,document.Form1.txtAmount5),setValue(document.Form1.dropAccName5)"
												id="DropAccountName5"  ondblclick="select(this,document.Form1.dropAccName5),setValue(document.Form1.dropAccName5)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName2,document.Form1.dropAccName5),setValue(document.Form1.dropAccName5)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName5)" multiple name="DropAccountName5"></select></div>
										<INPUT class="dropdownlist" id="txtAccName5" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount5" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_5" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName2"
											onkeyup="search3(this,document.Form1.DropAccountName2,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName2,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName2,event,document.Form1.dropAccName2,document.Form1.txtAmount2)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName2)"
											value="Select" name="dropAccName2" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName2)"
											readOnly name="temp"><br>
										<div id="Layer2" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName2,document.Form1.txtAmount2)"
												id="DropAccountName2" ondblclick="select(this,document.Form1.dropAccName2)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName3,document.Form1.dropAccName2)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName2)" multiple name="DropAccountName2"></select></div>
										<INPUT class="dropdownlist" id="txtAccName2" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount2" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_2" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
										</asp:dropdownlist></td>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName6"
											onkeyup="search3(this,document.Form1.DropAccountName6,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName6,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName6,event,document.Form1.dropAccName6,document.Form1.txtAmount6)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName6)"
											value="Select" name="dropAccName6" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName6)"
											readOnly name="temp"><br>
										<div id="Layer6" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName6,document.Form1.txtAmount6)"
												id="DropAccountName6" ondblclick="select(this,document.Form1.dropAccName6)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName3,document.Form1.dropAccName6)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName6)" multiple name="DropAccountName6"></select></div>
										<INPUT class="dropdownlist" id="txtAccName6" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount6" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_6" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName3"
											onkeyup="search3(this,document.Form1.DropAccountName3,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName3,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName3,event,document.Form1.dropAccName3,document.Form1.txtAmount3)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName3)"
											value="Select" name="dropAccName3" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName3)"
											readOnly name="temp"><br>
										<div id="Layer3" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName3,document.Form1.txtAmount3)"
												id="DropAccountName3" ondblclick="select(this,document.Form1.dropAccName3)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName4,document.Form1.dropAccName3)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName3)" multiple name="DropAccountName3"></select></div>
										<INPUT class="dropdownlist" id="txtAccName3" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount3" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_3" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle" onselectedindexchanged="Dropdownlist10_SelectedIndexChanged">
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
										</asp:dropdownlist></td>
									<td ><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName7"
											onkeyup="search3(this,document.Form1.DropAccountName7,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName7,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName7,event,document.Form1.dropAccName7,document.Form1.txtAmount7)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName7)"
											value="Select" name="dropAccName7" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName7)"
											readOnly name="temp"><br>
										<div id="Layer7" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName7,document.Form1.txtAmount7)"
												id="DropAccountName7" ondblclick="select(this,document.Form1.dropAccName7)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName4,document.Form1.dropAccName7)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName7)" multiple name="DropAccountName7"></select></div>
										<INPUT class="dropdownlist" id="txtAccName7" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount7" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_7" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName4"
											onkeyup="search3(this,document.Form1.DropAccountName4,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName4,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName4,event,document.Form1.dropAccName4,document.Form1.txtAmount4)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName4)"
											value="Select" name="dropAccName4" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName4)"
											readOnly name="temp"><br>
										<div id="Layer4" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName4,document.Form1.txtAmount4)"
												id="DropAccountName4" ondblclick="select(this,document.Form1.dropAccName4)" onkeyup="arrowkeyselect(this,event,document.Form1.dropAccName8,document.Form1.dropAccName4)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName4)" multiple name="DropAccountName4"></select></div>
										<INPUT class="dropdownlist" id="txtAccName4" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount4" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_4" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
										</asp:dropdownlist></td>
									<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="dropAccName8"
											onkeyup="search3(this,document.Form1.DropAccountName8,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropAccountName8,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropAccountName8,event,document.Form1.dropAccName8,document.Form1.txtAmount8)"
											style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 225px; HEIGHT: 19px" onclick="search1(document.Form1.DropAccountName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName8)"
											value="Select" name="dropAccName8" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropAccountName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropAccountName8)"
											readOnly name="temp"><br>
										<div id="Layer8" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.dropAccName8,document.Form1.txtAmount8)"
												id="DropAccountName8" ondblclick="select(this,document.Form1.dropAccName8)" onkeyup="arrowkeyselect(this,event,document.Form1.btnSave,document.Form1.dropAccName8)"
												style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 245px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.dropAccName8)" multiple name="DropAccountName8"></select></div>
										<INPUT class="dropdownlist" id="txtAccName8" style="WIDTH: 124px; HEIGHT: 22px" type="hidden"
											size="15" name="Hidden1" runat="server"></td>
									<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtAmount8" onblur="return calcTotal();"
											runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove" Height="19px"></asp:textbox></td>
									<td><asp:dropdownlist id="dropType_8" runat="server" Width="40px" onChange="return changeType(this);"
											CssClass="FontStyle">
											<asp:ListItem Value="Cr">Cr</asp:ListItem>
											<asp:ListItem Value="Dr">Dr</asp:ListItem>
										</asp:dropdownlist></td>
								</tr>
								<tr>
									<td align="right"><b>Total CR&nbsp;: </b>
									</td>
									<td><asp:textbox id="txtLCr" runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox></td>
									<td></td>
									<td></td>
									<td><asp:textbox id="txtRCr" runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox></td>
									<td></td>
								</tr>
								<tr>
									<td align="right"><b>Total DR : </b>
									</td>
									<td><asp:textbox id="txtLDr" runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox></td>
									<td></td>
									<td></td>
									<td><asp:textbox id="txtRDr" runat="server" Width="150px" CssClass="dropdownlist" BorderStyle="Groove"
											ReadOnly="True"></asp:textbox></td>
									<td></td>
								</tr>
								<tr>
									<TD vAlign="top" colSpan="3"><FONT color="#ff0000"><b><FONT color="black">Narration:&nbsp; </FONT>
											</b>&nbsp;
											<asp:textbox onkeypress="CheckLength(this);" id="txtNarration" runat="server" Width="150px" CssClass="dropdownlist"
												BorderStyle="Groove" Height="40" TextMode="MultiLine" MaxLength="10"></asp:textbox></FONT></TD>
									<td align="right" colSpan="3"><asp:button id="btnAdd" runat="server" Width="75px" 
											  Text="Save" onclick="btnAdd_Click"></asp:button><asp:button id="btnEdit" runat="server" Width="75px" 
											  Text="Edit" onclick="btnEdit_Click"></asp:button><asp:button id="btnDelete" runat="server" Width="75px" 
											  Text="Delete" onclick="btnDelete_Click"></asp:button><asp:button id="btnPrint" runat="server" Width="75px" 
											  Text="Print" onclick="btnPrint_Click"></asp:button></td>
								</tr>
							</TBODY>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD>
				</TR>
			</TABLE>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form></FORM>
	</body>
</HTML>
