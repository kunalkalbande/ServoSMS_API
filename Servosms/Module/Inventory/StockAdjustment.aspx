<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.StockAdjustment" CodeFile="StockAdjustment.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Stock Adjustment</title> <!--
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
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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


function calcPack(t,t1,t2,t3)
{
	t.value = "";
	//var index = t2.selectedIndex;
	//var typetext = t2.options[index].text;
	var typetext = t2.value;
	var temp = document.Form1.txtTemp1.value;  
	var temp1= "";
	var temp3 ="";
	var arr = new Array();
	var arr1 = new Array();
	var packArr = new Array();
	arr = temp.split("#");
	for(var i = 0 ; i< arr.length; i++)
	{
		temp1 = arr[i];
		arr1 = temp1.split("~");
		for(var j=0; j< arr1.length; j=j+2)
		{
			if(typetext == arr1[j])
			{
				temp3 = arr1[j+1];
				packArr = temp3.split("X");
				t.value = eval(t1.value)*eval(packArr[0])*eval(packArr[1]);
				t3.value = eval(t1.value)*eval(packArr[0])*eval(packArr[1]);
			}
		} 
	}
}

function checkStock(t,t1)
{
	//var index = t.selectedIndex;
	//var typetext = t.options[index].text;
	var typetext = t.value;
	var temp = document.Form1.txtQty.value;  
	var temp1= "";
	var arr = new Array();
	var arr1 = new Array();
	arr = temp.split("#");
	//alert("inside")
	for(var i = 0 ; i< arr.length; i++)
	{
		temp1 = arr[i];
		arr1 = temp1.split("~");
		for(var j=0; j< arr1.length; j=j+2)
		{
			if(typetext == arr1[j])
			{
				//alert(arr1[j+1]+" "+t1.value);
				if(eval(arr1[j+1]) < eval(t1.value))
				{
					alert("Insufficient Stock!");
					t1.value = "";
					t1.focus();
					return false;
				}
				else
				{
					calcTotal();
					break;
                }      
			}
		}
	}  
}

function checkTotal(tt,t,t1) 
{
	if(tt.value != "")
	{
		calcTotal();
		if(t.value == t1.value)
		{
			calcTotal();
		}
		else
		{
			alert("The IN Liter Quantity must be same as OUT Liter Quantity");
			t.value = "";
			tt.value = "";
			tt.focus();
			return false;
		}
	}
}

function calcTotal()
{
	var OutLtrTotal = 0;
	var OutPackTotal = 0;
	var InLtrTotal = 0;
	var InPackTotal = 0;
	
	if(document.Form1.txtOutQtyPack1.value != "")
	{
		OutPackTotal = OutPackTotal+ eval(document.Form1.txtOutQtyPack1.value);
		calcPack(document.Form1.txtOutQtyLtr1,document.Form1.txtOutQtyPack1,document.Form1.DropOutProd1,document.Form1.tmpOutQtyLtr1);
	}
  
	if(document.Form1.txtOutQtyLtr1.value != "")
	{
		OutLtrTotal = OutLtrTotal+ eval(document.Form1.txtOutQtyLtr1.value);
	}
  
	if(document.Form1.txtOutQtyPack2.value != "")
	{
		OutPackTotal = OutPackTotal+ eval(document.Form1.txtOutQtyPack2.value);
		calcPack(document.Form1.txtOutQtyLtr2,document.Form1.txtOutQtyPack2,document.Form1.DropOutProd2,document.Form1.tmpOutQtyLtr2);
	}
  
	if(document.Form1.txtOutQtyLtr2.value != "")
	{
		OutLtrTotal = OutLtrTotal+ eval(document.Form1.txtOutQtyLtr2.value);
	}
	if(document.Form1.txtOutQtyPack3.value != "")
	{
		OutPackTotal = OutPackTotal+ eval(document.Form1.txtOutQtyPack3.value);
		calcPack(document.Form1.txtOutQtyLtr3,document.Form1.txtOutQtyPack3,document.Form1.DropOutProd3,document.Form1.tmpOutQtyLtr3);
	}
  
	if(document.Form1.txtOutQtyLtr3.value != "")
	{
		OutLtrTotal = OutLtrTotal+ eval(document.Form1.txtOutQtyLtr3.value);
	}
	if(document.Form1.txtOutQtyPack4.value != "")
	{
		OutPackTotal = OutPackTotal+ eval(document.Form1.txtOutQtyPack4.value);
		calcPack(document.Form1.txtOutQtyLtr4,document.Form1.txtOutQtyPack4,document.Form1.DropOutProd4,document.Form1.tmpOutQtyLtr4);
	}
  
	if(document.Form1.txtOutQtyLtr4.value != "")
	{
	    OutLtrTotal = OutLtrTotal+ eval(document.Form1.txtOutQtyLtr4.value);
	}
  
	if(document.Form1.txtInQtyPack1.value != "")
	{
		InPackTotal = InPackTotal+ eval(document.Form1.txtInQtyPack1.value);
		calcPack(document.Form1.txtInQtyLtr1,document.Form1.txtInQtyPack1,document.Form1.DropInProd1,document.Form1.tmpInQtyLtr1 );
	}
  
	if(document.Form1.txtInQtyLtr1.value != "")
	{
		InLtrTotal = InLtrTotal+ eval(document.Form1.txtInQtyLtr1.value);
	}
  
	if(document.Form1.txtInQtyPack2.value != "")
	{
		InPackTotal = InPackTotal+ eval(document.Form1.txtInQtyPack2.value);
		calcPack(document.Form1.txtInQtyLtr2,document.Form1.txtInQtyPack2,document.Form1.DropInProd2,document.Form1.tmpInQtyLtr2);
	}
  
	if(document.Form1.txtInQtyLtr2.value != "")
	{
		InLtrTotal = InLtrTotal+ eval(document.Form1.txtInQtyLtr2.value);
	}
  
	if(document.Form1.txtInQtyPack3.value != "")
	{
		InPackTotal = InPackTotal+ eval(document.Form1.txtInQtyPack3.value);
		calcPack(document.Form1.txtInQtyLtr3,document.Form1.txtInQtyPack3,document.Form1.DropInProd3,document.Form1.tmpInQtyLtr3);
	}
  
	if(document.Form1.txtInQtyLtr3.value != "")
	{
		InLtrTotal = InLtrTotal+ eval(document.Form1.txtInQtyLtr3.value);
	}
	if(document.Form1.txtInQtyPack4.value != "")
	{
		InPackTotal = InPackTotal+ eval(document.Form1.txtInQtyPack4.value);
		calcPack(document.Form1.txtInQtyLtr4,document.Form1.txtInQtyPack4,document.Form1.DropInProd4,document.Form1.tmpInQtyLtr4);
	}
  
	if(document.Form1.txtInQtyLtr4.value != "")
	{
		InLtrTotal = InLtrTotal+ eval(document.Form1.txtInQtyLtr4.value);
	}
	document.Form1.txtTotalOutQtyLtr.value ="";
	document.Form1.txtTotalOutQtyPack.value = "";
	document.Form1.txtTotalInQtyLtr.value ="";
	document.Form1.txtTotalInQtyPack.value = "";
    document.Form1.txtTotalOutQtyLtr.value = OutLtrTotal;
	makeRound( document.Form1.txtTotalOutQtyLtr);
    document.Form1.txtTotalOutQtyPack.value = OutPackTotal;
	makeRound( document.Form1.txtTotalOutQtyPack);
    document.Form1.txtTotalInQtyLtr.value = InLtrTotal;
	makeRound( document.Form1.txtTotalInQtyLtr);
    document.Form1.txtTotalInQtyPack.value = InPackTotal;
	makeRound( document.Form1.txtTotalInQtyPack);
}

function makeRound(t)
{
	var str = t.value;
	if(str != "")
	{
		str = eval(str)*100;
		str  = Math.round(str);
		str = eval(str)/100;
		t.value = str;
	}
	
}

function setValue(t)
{
	//var index = t.selectedIndex;
	//var typetext = t.options[index].text;
	var typetext = t.value;
	//alert(t.name);
	if(t.name == "DropOutProd1")
		document.Form1.txtAccName1.value = typetext;
	if(t.name == "DropOutProd2")
	    document.Form1.txtAccName2.value = typetext;
	if(t.name == "DropOutProd3")
	    document.Form1.txtAccName3.value = typetext;
	if(t.name == "DropOutProd4")
		document.Form1.txtAccName4.value = typetext;
	if(t.name == "DropOutProd5")
		document.Form1.txtAccName5.value = typetext;
	if(t.name == "DropOutProd6")
		document.Form1.txtAccName6.value = typetext;
	if(t.name == "DropOutProd7")
		document.Form1.txtAccName7.value = typetext;
	if(t.name == "DropOutProd8")
		document.Form1.txtAccName8.value = typetext;     
}
	
function setStore(t,t1,t2,t3)
{
	//alert("in");
	t2.value = "";
	t3.value = "";
	t1.value = "";
	//var index = t.selectedIndex;
	//var typetext = t.options[index].text;
	if(t.value!="")
	{
		var typetext = t.value;
		var temp = document.Form1.txtTemp.value; 
		var temp1="";
		var arr =new Array();
		var secArr = new Array();
		var str = new String();
		arr = temp.split("#");
	
		for(var i= 0 ; i<arr.length; i++)
		{
			temp1 = arr[i];
		    secArr = temp1.split("~");
		    for(var j=0; j< secArr.length; j= j+4)
		    { 
				if(typetext == secArr[j])
				{
					t1.value = secArr[j+2]
					str = secArr[j+3]
					if(secArr[j+1] == "Fuel" || str.indexOf("Loose",0) > -1  )
					{
						t2.disabled = true;
						t3.disabled = false;
			        }
					else
					{
						t2.disabled = false;
						t3.disabled = true;
					}
				}
			}
		}
		calcTotal();
	}
}
	
function getBatch(t,prd,Invo,qty)
{
	if(t.checked)
	{
		if(prd.value!="Type" && qty.value!="")
		{
			var Result="";
			if(Invo!=null)
				childWin=window.open("BatchNo.aspx?chk="+t.name+":0:"+prd.value+":"+Invo.value, "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
		}
		else
		{
			alert("Please Select The Prod & Fill The Qty");
			t.checked=false;
		}
	}
}

function SetFocus(t,e)
{
	if(window.event) 
	{
		var	key = e.keyCode;
		if(key==13)
		{
			t.focus();
		}
		/*else if(key==9)
		{
			t1.focus();
		}*/
	}
}
		</script>
	    <style type="text/css">
            .auto-style1 {
                width: 221px;
            }
        </style>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><INPUT id="txtTemp" style="Z-INDEX: 102; LEFT: 160px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="txtTemp1" style="Z-INDEX: 103; LEFT: 176px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="txtTemp1" runat="server"><INPUT id="txtQty" style="Z-INDEX: 104; LEFT: 192px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="tmpOutQtyLtr1" style="Z-INDEX: 105; LEFT: 208px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="tmpOutQtyLtr2" style="Z-INDEX: 106; LEFT: 224px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="tmpOutQtyLtr3" style="Z-INDEX: 107; LEFT: 240px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden3" runat="server"><INPUT id="tmpOutQtyLtr4" style="Z-INDEX: 108; LEFT: 256px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden4" runat="server"><INPUT id="tmpInQtyLtr1" style="Z-INDEX: 109; LEFT: 272px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden5" runat="server"><INPUT id="tmpInQtyLtr2" style="Z-INDEX: 110; LEFT: 288px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden6" runat="server"><INPUT id="tmpInQtyLtr3" style="Z-INDEX: 111; LEFT: 304px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden7" runat="server"><INPUT id="tmpInQtyLtr4" style="Z-INDEX: 112; LEFT: 320px; WIDTH: 8px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden8" runat="server"><input id="bat0" style="WIDTH: 1px" type="hidden" name="bat0" runat="server"><input id="bat1" style="WIDTH: 1px" type="hidden" name="bat1" runat="server"><input id="bat2" style="WIDTH: 1px" type="hidden" name="bat2" runat="server">
			<input id="bat3" style="WIDTH: 1px" type="hidden" name="bat3" runat="server"><INPUT id="texthiddenprod" style="Z-INDEX: 103; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 10px"
				type="text" name="texthiddenprod" runat="server">
			<TABLE height="283" cellSpacing="0" cellPadding="0" width="778" align="center" border="0">
				<tr>
					<th align="center">
						<font color="#ce4848">Stock Adjustment Voucher</font>
						<hr>
					</th>
				</tr>
				<TR>
					<td vAlign="top" align="center">
						<table cellSpacing="0" cellPadding="0" border="0">
							<TR>
								<TD style="HEIGHT: 23px" align="left">&nbsp;<STRONG>SAV ID</STRONG>&nbsp;<font color="red">*</font>&nbsp;<asp:requiredfieldvalidator id="rfv1" Runat="server" InitialValue="Select" ErrorMessage="Please Select SavID From DropDownList"
										ControlToValidate="DropSavID">*</asp:requiredfieldvalidator>&nbsp;&nbsp;
									<asp:textbox id="lblSAV_ID" runat="server" CssClass="dropdownlist" BorderStyle="Groove" 
										Width="80"></asp:textbox><asp:dropdownlist id="DropSavID" Runat="server" CssClass="fontstyle" BorderStyle="Groove" Width="100px"
										AutoPostBack="True" onselectedindexchanged="DropSavID_SelectedIndexChanged"></asp:dropdownlist><asp:button id="btnEdit" Runat="server"  Width="20" Text="..." Height="20"
										CausesValidation="False" onclick="btnEdit_Click"></asp:button></TD>
								<td style="HEIGHT: 23px" align="right">Date&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtDate" Runat="server" CssClass="fontstyle" BorderStyle="Groove" Width="103px"
										ReadOnly="True"></asp:textbox>&nbsp;&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
											align="absMiddle" border="0"></A>&nbsp;</td>
							</TR>
							<tr>
								<td align="center"><font color="#ce4848"><b>OUT</b></font>
								</td>
								<td align="center"><font color="#ce4848"><b>IN</b></font>
								</td>
							</tr>
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" border="1">
										<tr>
											<td align="center" bgColor="#ce4848" class="auto-style1"><FONT color="#ffffff"><b>Product Name &amp; Package</b></FONT>
											</td>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Store In</b></FONT>
											</td>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Qty. in Pack</b></FONT>
											</td>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Qty. in Ltr.</b></FONT>
											</td>
										</tr>
										<tr>
											<td class="auto-style1"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropOutProd1"
													onkeyup="search3(this,document.Form1.DropProductName1,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName1,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName1,event,document.Form1.DropOutProd1,document.Form1.txtOutQtyPack1),setStore(this,document.Form1.txtOutStoreIn1,document.Form1.txtOutQtyPack1,document.Form1.txtOutQtyLtr1)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 200px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName1)"
													value="Select" name="DropOutProd1" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName1)"
													readOnly type="text" name="temp"><br>
												<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropOutProd1,document.Form1.txtOutQtyPack1),setStore(document.Form1.DropOutProd1,document.Form1.txtOutStoreIn1,document.Form1.txtOutQtyPack1,document.Form1.txtOutQtyLtr1)"
														id="DropProductName1" ondblclick="select(this,document.Form1.DropOutProd1),setStore(document.Form1.DropOutProd1,document.Form1.txtOutStoreIn1,document.Form1.txtOutQtyPack1,document.Form1.txtOutQtyLtr1)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtOutQtyPack1,document.Form1.DropOutProd1),setStore(document.Form1.DropOutProd1,document.Form1.txtOutStoreIn1,document.Form1.txtOutQtyPack1,document.Form1.txtOutQtyLtr1)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 200px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropOutProd1)" multiple name="DropProductName1"></select></div>
											</td>
											<td><asp:textbox id="txtOutStoreIn1" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="60px" ReadOnly="True"></asp:textbox></td>
											<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtOutQtyPack1"
													onblur="return checkStock(document.Form1.DropOutProd1,this);" onkeyup="SetFocus(document.Form1.DropInProd1,event);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></td>
											<td><asp:textbox id="txtOutQtyLtr1" onblur="return checkStock(document.Form1.DropOutProd1,this);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></td>
										</tr>
										<TR>
											<TD class="auto-style1"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropOutProd2"
													onkeyup="search3(this,document.Form1.DropProductName2,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName2,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName2,event,document.Form1.DropOutProd2,document.Form1.txtOutQtyPack2),setStore(this,document.Form1.txtOutStoreIn2,document.Form1.txtOutQtyPack2,document.Form1.txtOutQtyLtr2)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 200px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName2)"
													value="Select" name="DropOutProd2" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName2)"
													readOnly type="text" name="temp"><br>
												<div id="Layer2" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropOutProd2,document.Form1.txtOutQtyPack2),setStore(document.Form1.DropOutProd2,document.Form1.txtOutStoreIn2,document.Form1.txtOutQtyPack2,document.Form1.txtOutQtyLtr2)"
														id="DropProductName2" ondblclick="select(this,document.Form1.DropOutProd2),setStore(document.Form1.DropOutProd2,document.Form1.txtOutStoreIn2,document.Form1.txtOutQtyPack2,document.Form1.txtOutQtyLtr2)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtOutQtyPack2,document.Form1.DropOutProd2),setStore(document.Form1.DropOutProd2,document.Form1.txtOutStoreIn2,document.Form1.txtOutQtyPack2,document.Form1.txtOutQtyLtr2)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 200px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropOutProd2)" multiple name="DropProductName2"></select></div>
											</TD>
											<TD><asp:textbox id="txtOutStoreIn2" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="60px" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtOutQtyPack2"
													onblur="return checkStock(document.Form1.DropOutProd2,this);" onkeyup="SetFocus(document.Form1.DropInProd2,event);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></TD>
											<TD><asp:textbox id="txtOutQtyLtr2" onblur="return checkStock(document.Form1.DropOutProd2,this);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="auto-style1"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropOutProd3"
													onkeyup="search3(this,document.Form1.DropProductName3,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName3,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName3,event,document.Form1.DropOutProd3,document.Form1.txtOutQtyPack3),setStore(this,document.Form1.txtOutStoreIn3,document.Form1.txtOutQtyPack3,document.Form1.txtOutQtyLtr3)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 200px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName3)"
													value="Select" name="DropOutProd3" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName3)"
													readOnly type="text" name="temp"><br>
												<div id="Layer3" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropOutProd3,document.Form1.txtOutQtyPack3),setStore(document.Form1.DropOutProd3,document.Form1.txtOutStoreIn3,document.Form1.txtOutQtyPack3,document.Form1.txtOutQtyLtr3)"
														id="DropProductName3" ondblclick="select(this,document.Form1.DropOutProd3),setStore(document.Form1.DropOutProd3,document.Form1.txtOutStoreIn3,document.Form1.txtOutQtyPack3,document.Form1.txtOutQtyLtr3)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtOutQtyPack3,document.Form1.DropOutProd3),setStore(document.Form1.DropOutProd3,document.Form1.txtOutStoreIn3,document.Form1.txtOutQtyPack3,document.Form1.txtOutQtyLtr3)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 200px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropOutProd3)" multiple name="DropProductName3"></select></div>
											</TD>
											<TD><asp:textbox id="txtOutStoreIn3" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="60px" ReadOnly="True" ontextchanged="TextBox4_TextChanged"></asp:textbox></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtOutQtyPack3"
													onblur="return checkStock(document.Form1.DropOutProd3,this);" onkeyup="SetFocus(document.Form1.DropInProd3,event);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></TD>
											<TD><asp:textbox id="txtOutQtyLtr3" onblur="return checkStock(document.Form1.DropOutProd3,this);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="auto-style1"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropOutProd4"
													onkeyup="search3(this,document.Form1.DropProductName4,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName4,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName4,event,document.Form1.DropOutProd4,document.Form1.txtOutQtyPack4),setStore(document.Form1.DropOutProd4,document.Form1.txtOutStoreIn4,document.Form1.txtOutQtyPack4,document.Form1.txtOutQtyLtr4)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 200px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName4)"
													value="Select" name="DropOutProd4" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName4)"
													readOnly type="text" name="temp"><br>
												<div id="Layer4" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropOutProd4,document.Form1.txtOutQtyPack4),setStore(document.Form1.DropOutProd4,document.Form1.txtOutStoreIn4,document.Form1.txtOutQtyPack4,document.Form1.txtOutQtyLtr4)"
														id="DropProductName4" ondblclick="select(this,document.Form1.DropOutProd4),setStore(document.Form1.DropOutProd4,document.Form1.txtOutStoreIn4,document.Form1.txtOutQtyPack4,document.Form1.txtOutQtyLtr4)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtOutQtyPack4,document.Form1.DropOutProd4),setStore(document.Form1.DropOutProd4,document.Form1.txtOutStoreIn4,document.Form1.txtOutQtyPack4,document.Form1.txtOutQtyLtr4)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 200px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropOutProd4)" multiple name="DropProductName4"></select></div>
											</TD>
											<TD><asp:textbox id="txtOutStoreIn4" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="60px" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtOutQtyPack4"
													onblur="return checkStock(document.Form1.DropOutProd4,this);" onkeyup="SetFocus(document.Form1.DropInProd4,event);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></TD>
											<TD><asp:textbox id="txtOutQtyLtr4" onblur="return checkStock(document.Form1.DropOutProd4,this);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></TD>
										</TR>
										<TR>
											<TD class="auto-style1"><STRONG>Total Out:</STRONG></TD>
											<TD>&nbsp;</TD>
											<TD><asp:textbox id="txtTotalOutQtyPack" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="40px" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox id="txtTotalOutQtyLtr" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="40px" ReadOnly="True"></asp:textbox></TD>
										</TR>
									</table>
								</td>
								<td>
									<table cellSpacing="0" cellPadding="0" border="1">
										<tr>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Product Name &amp; Package</b></FONT>
											</td>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Store In</b></FONT>
											</td>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Qty. in Pack</b></FONT>
											</td>
											<td align="center" bgColor="#ce4848"><FONT color="#ffffff"><b>Qty. in Ltr.</b></FONT>
											</td>
										</tr>
										<tr>
											<td><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropInProd1"
													onkeyup="search3(this,document.Form1.DropProductName5,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName5,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName5,event,document.Form1.DropInProd1,document.Form1.txtInQtyPack1),setStore(this,document.Form1.txtInStoreIn1,document.Form1.txtInQtyPack1,document.Form1.txtInQtyLtr1)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 160px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName5)"
													value="Select" name="DropInProd1" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName5)"
													readOnly type="text" name="temp"><br>
												<div id="Layer5" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropInProd1,document.Form1.txtInQtyPack1),setStore(document.Form1.DropInProd1,document.Form1.txtInStoreIn1,document.Form1.txtInQtyPack1,document.Form1.txtInQtyLtr1)"
														id="DropProductName5" ondblclick="select(this,document.Form1.DropInProd1),setStore(document.Form1.DropInProd1,document.Form1.txtInStoreIn1,document.Form1.txtInQtyPack1,document.Form1.txtInQtyLtr1)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtInQtyPack1,document.Form1.DropInProd1),setStore(document.Form1.DropInProd1,document.Form1.txtInStoreIn1,document.Form1.txtInQtyPack1,document.Form1.txtInQtyLtr1)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 160px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropInProd1)" multiple name="DropProductName5"></select></div>
											</td>
											<td><asp:textbox id="txtInStoreIn1" runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="60px"
													ReadOnly="True"></asp:textbox></td>
											<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtInQtyPack1"
													onblur="return checkTotal(this,document.Form1.txtInQtyLtr1,document.Form1.txtOutQtyLtr1);"
													onkeyup="SetFocus(document.Form1.DropOutProd2,event);" runat="server" CssClass="dropdownlist"
													BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></td>
											<td><asp:textbox id="txtInQtyLtr1" onblur="return checkTotal(document.Form1.txtInQtyLtr1,document.Form1.txtOutQtyLtr1);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></td>
											<td align="center"></td>
										</tr>
										<TR>
											<TD><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropInProd2"
													onkeyup="search3(this,document.Form1.DropProductName6,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName6,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName6,event,document.Form1.DropInProd2,document.Form1.txtInQtyPack2),setStore(document.Form1.DropInProd2,document.Form1.txtInStoreIn2,document.Form1.txtInQtyPack2,document.Form1.txtInQtyLtr2)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 160px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName6)"
													value="Select" name="DropInProd2" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName6)"
													readOnly type="text" name="temp"><br>
												<div id="Layer6" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropInProd2,document.Form1.txtInQtyPack2),setStore(document.Form1.DropInProd2,document.Form1.txtInStoreIn2,document.Form1.txtInQtyPack2,document.Form1.txtInQtyLtr2)"
														id="DropProductName6" ondblclick="select(this,document.Form1.DropInProd2),setStore(document.Form1.DropInProd2,document.Form1.txtInStoreIn2,document.Form1.txtInQtyPack2,document.Form1.txtInQtyLtr2)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtInQtyPack2,document.Form1.DropInProd2),setStore(document.Form1.DropInProd2,document.Form1.txtInStoreIn2,document.Form1.txtInQtyPack2,document.Form1.txtInQtyLtr2)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 160px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropInProd2)" multiple name="DropProductName6"></select></div>
											</TD>
											<TD><asp:textbox id="txtInStoreIn2" runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="60px"
													ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtInQtyPack2"
													onblur="return checkTotal(this,document.Form1.txtInQtyLtr2,document.Form1.txtOutQtyLtr2);"
													onkeyup="SetFocus(document.Form1.DropOutProd3,event);" runat="server" CssClass="dropdownlist"
													BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></TD>
											<TD><asp:textbox id="txtInQtyLtr2" onblur="return checkTotal(document.Form1.txtInQtyLtr2,document.Form1.txtOutQtyLtr2);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></TD>
											<td align="center"></td>
										</TR>
										<TR>
											<TD><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropInProd3"
													onkeyup="search3(this,document.Form1.DropProductName7,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName7,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName7,event,document.Form1.DropInProd3,document.Form1.txtInQtyPack3),setStore(document.Form1.DropInProd3,document.Form1.txtInStoreIn3,document.Form1.txtInQtyPack3,document.Form1.txtInQtyLtr3)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 160px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName7)"
													value="Select" name="DropInProd3" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName7)"
													readOnly type="text" name="temp"><br>
												<div id="Layer7" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropInProd3,document.Form1.txtInQtyPack3),setStore(document.Form1.DropInProd3,document.Form1.txtInStoreIn3,document.Form1.txtInQtyPack3,document.Form1.txtInQtyLtr3)"
														id="DropProductName7" ondblclick="select(this,document.Form1.DropInProd3),setStore(document.Form1.DropInProd3,document.Form1.txtInStoreIn3,document.Form1.txtInQtyPack3,document.Form1.txtInQtyLtr3)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtInQtyPack3,document.Form1.DropInProd3),setStore(document.Form1.DropInProd3,document.Form1.txtInStoreIn3,document.Form1.txtInQtyPack3,document.Form1.txtInQtyLtr3)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 160px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropInProd3)" multiple name="DropProductName7"></select></div>
											</TD>
											<TD><asp:textbox id="txtInStoreIn3" runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="60px"
													ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtInQtyPack3"
													onblur="return checkTotal(this,document.Form1.txtInQtyLtr3,document.Form1.txtOutQtyLtr3);"
													onkeyup="SetFocus(document.Form1.DropOutProd4,event);" runat="server" CssClass="dropdownlist"
													BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></TD>
											<TD><asp:textbox id="txtInQtyLtr3" onblur="return checkTotal(document.Form1.txtInQtyLtr3,document.Form1.txtOutQtyLtr3);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></TD>
											<td align="center"></td>
										</TR>
										<TR>
											<TD><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropInProd4"
													onkeyup="search3(this,document.Form1.DropProductName8,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProductName8,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProductName8,event,document.Form1.DropInProd4,document.Form1.txtInQtyPack4),setStore(document.Form1.DropInProd4,document.Form1.txtInStoreIn4,document.Form1.txtInQtyPack4,document.Form1.txtInQtyLtr4)"
													style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 160px; HEIGHT: 19px" onclick="search1(document.Form1.DropProductName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName8)"
													value="Select" name="DropInProd4" runat="server"><input class="ComboBoxSearchButtonStyle" onclick="search1(document.Form1.DropProductName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropProductName8)"
													readOnly type="text" name="temp"><br>
												<div id="Layer8" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxBorderStyle" onkeypress="Selectbyenter(this,event,document.Form1.DropInProd4,document.Form1.txtInQtyPack4),setStore(document.Form1.DropInProd4,document.Form1.txtInStoreIn4,document.Form1.txtInQtyPack4,document.Form1.txtInQtyLtr4)"
														id="DropProductName8" ondblclick="select(this,document.Form1.DropInProd4),setStore(document.Form1.DropInProd4,document.Form1.txtInStoreIn4,document.Form1.txtInQtyPack4,document.Form1.txtInQtyLtr4)"
														onkeyup="arrowkeyselect(this,event,document.Form1.txtInQtyPack4,document.Form1.DropInProd4),setStore(document.Form1.DropInProd4,document.Form1.txtInStoreIn4,document.Form1.txtInQtyPack4,document.Form1.txtInQtyLtr4)"
														style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 160px; HEIGHT: 19px" onfocusout="HideList(this,document.Form1.DropInProd4)" multiple name="DropProductName8"></select></div>
											</TD>
											<TD><asp:textbox id="txtInStoreIn4" runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="60px"
													ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event,false,false);" id="txtInQtyPack4"
													onblur="return checkTotal(this,document.Form1.txtInQtyLtr4,document.Form1.txtOutQtyLtr4);"
													onkeyup="SetFocus(document.Form1.txtNarration,event);" runat="server" CssClass="dropdownlist"
													BorderStyle="Groove" Width="40px" MaxLength="5"></asp:textbox></TD>
											<TD><asp:textbox id="txtInQtyLtr4" onblur="return checkTotal(document.Form1.txtInQtyLtr4,document.Form1.txtOutQtyLtr4);"
													runat="server" CssClass="dropdownlist" BorderStyle="Groove" Width="40px" Enabled="False"></asp:textbox></TD>
											<td align="center"></td>
										</TR>
										<TR>
											<TD><STRONG>Total IN:</STRONG></TD>
											<TD>&nbsp;</TD>
											<TD><asp:textbox id="txtTotalInQtyPack" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="40px" ReadOnly="True"></asp:textbox></TD>
											<TD><asp:textbox id="txtTotalInQtyLtr" runat="server" CssClass="dropdownlist" BorderStyle="Groove"
													Width="40px" ReadOnly="True"></asp:textbox></TD>
										</TR>
									</table>
								</td>
							</tr>
							<TR>
								<TD colSpan="2">&nbsp;&nbsp;Narration&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtNarration" Runat="server" CssClass="fontstyle" BorderStyle="Groove" Width="120px"
										MaxLength="99"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2"><asp:button id="btnPrint" runat="server"  Width="75px" Text="Save" 
										 onclick="btnPrint_Click"></asp:button>&nbsp;&nbsp;<asp:button id="Button1" runat="server"  Width="75px" Text="Print" 
										 onclick="Button1_Click"></asp:button></TD>
							</TR>
						</table>
						<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
							name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
							width="174" scrolling="no"></IFRAME>
						<asp:validationsummary id="vs1" Runat="server" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</TR>
			</TABLE>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
