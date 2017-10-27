<%@ Page language="c#" Inherits="Servosms.Module.Inventory.CashSalesInvoice" CodeFile="CashSalesInvoice.aspx.cs" %>

<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc2" TagName="Footer" Src="~/HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Sales Invoice</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="sales" src="../../Sysitem/JS/Sales.js"></script>
		<script language="javascript" id="fuel" src="../../Sysitem/JS/Fuel.js"></script>
		<meta content="False" name="vs_snapToGrid">
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
		function checkProd()
		{
		var packArray = new Array();		
		var index1 = document.Form1.DropType1.selectedIndex;
		var index2 = document.Form1.DropProd1.selectedIndex;
		var index3 = document.Form1.DropPack1.selectedIndex;
		
		var index4 = document.Form1.DropType2.selectedIndex;
		var index5 = document.Form1.DropProd2.selectedIndex;
		var index6 = document.Form1.DropPack2.selectedIndex;
		
		var index7 = document.Form1.DropType3.selectedIndex;
		var index8 = document.Form1.DropProd3.selectedIndex;
		var index9 = document.Form1.DropPack3.selectedIndex;
		
		var index10 = document.Form1.DropType4.selectedIndex;
		var index11 = document.Form1.DropProd4.selectedIndex;
		var index12 = document.Form1.DropPack4.selectedIndex;
		
		var index13 = document.Form1.DropType5.selectedIndex;
		var index14= document.Form1.DropProd5.selectedIndex;
		var index15 = document.Form1.DropPack5.selectedIndex;
		
		var index16= document.Form1.DropType6.selectedIndex;
		var index17= document.Form1.DropProd6.selectedIndex;
		var index18= document.Form1.DropPack6.selectedIndex;
		
		var index19 = document.Form1.DropType7.selectedIndex;
		var index20 = document.Form1.DropProd7.selectedIndex;
		var index21 = document.Form1.DropPack7.selectedIndex;
		
		var index22 = document.Form1.DropType8.selectedIndex;
		var index23 = document.Form1.DropProd8.selectedIndex;
		var index24 = document.Form1.DropPack8.selectedIndex;
		
		if(index3==-1 )
		packArray[0]=document.Form1.DropType1.options[index1].text+document.Form1.DropProd1.options[index2].text
		else
		packArray[0]=document.Form1.DropType1.options[index1].text+document.Form1.DropProd1.options[index2].text+document.Form1.DropPack1.options[index3].text;
		
		if(index6==-1)
		packArray[1]=document.Form1.DropType2.options[index4].text+document.Form1.DropProd2.options[index5].text
		else
		packArray[1]=document.Form1.DropType2.options[index4].text+document.Form1.DropProd2.options[index5].text+document.Form1.DropPack2.options[index6].text;
		
		
		if(index9==-1 )
		packArray[2]=document.Form1.DropType3.options[index7].text+document.Form1.DropProd3.options[index8].text;
		else
		packArray[2]=document.Form1.DropType3.options[index7].text+document.Form1.DropProd3.options[index8].text+document.Form1.DropPack3.options[index9].text;
	
		
		if(index12==-1)
		packArray[3]=document.Form1.DropType4.options[index10].text+document.Form1.DropProd4.options[index11].text
		else
		packArray[3]=document.Form1.DropType4.options[index10].text+document.Form1.DropProd4.options[index11].text+document.Form1.DropPack4.options[index12].text;
		
		
		if(index15==-1)
		packArray[4]=document.Form1.DropType5.options[index13].text+document.Form1.DropProd5.options[index14].text;
		else
		packArray[4]=document.Form1.DropType5.options[index13].text+document.Form1.DropProd5.options[index14].text+document.Form1.DropPack5.options[index15].text;

		
		if(index18==-1)
		packArray[5]=document.Form1.DropType6.options[index16].text+document.Form1.DropProd6.options[index17].text;
		else
		packArray[5]=document.Form1.DropType6.options[index16].text+document.Form1.DropProd6.options[index17].text+document.Form1.DropPack6.options[index18].text;
		
		
		if(index21==-1)
		packArray[6]=document.Form1.DropType7.options[index19].text+document.Form1.DropProd7.options[index20].text;
		else
		packArray[6]=document.Form1.DropType7.options[index19].text+document.Form1.DropProd7.options[index20].text+document.Form1.DropPack7.options[index21].text;
		
		
		if(index24==-1)
		packArray[7]=document.Form1.DropType8.options[index22].text+document.Form1.DropProd8.options[index23].text;
		else
		packArray[7]=document.Form1.DropType8.options[index22].text+document.Form1.DropProd8.options[index23].text+document.Form1.DropPack8.options[index24].text;
	
		var count = 0;

		for (var i=0;i<8;i++)
		{
		for (var j=0;j<8;j++)
		{

		if(packArray[i]==packArray[j] && packArray[i]!="TypeSelect")
		{
		count=count+1;
		if(count>1)
		{
		alert("Product Already Selected!");
		
		return false;
		}
		
		}

		else
		continue;
		}
		count = 0;

		}
		return true;
				
					
		
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
	//alert(str)
	}
	
	}
		
	  function check(slipNo)
	  {
	  var start = document.Form1.Txtstart.value;
	  var end  = document.Form1.TxtEnd.value;
	  var slip = slipNo.value;
	  var slip1 = document.Form1.SlipNo.value;  
	  var temp = document.Form1.txtSlipTemp.value;
	  var arr = new Array();
	  arr = temp.split("#");
	  
	    

	  if (eval(slip)<eval(start) || eval(slip)>eval(end))
	  {
	  alert("Invalid Slip No.");
	  slipNo.value="";
	  return false;	  
	  }   
	  
	  for(var i=0; i<arr.length; i++)
	  {
	     if(eval(slip1) != eval(slip))
	     {
	        if(eval(slip) == eval(arr[i]))
	        {
	           alert("Slip No. already Used.");
	          return false;
	        }
	        else
	         continue;
	     }
	  } 
	
	  }
	 
	function calc(txtQty,txtAvstock,txtRate,txtTempQty)
	{	
	
	
	 var sarr = new Array()
	 var temp ="";
	 sarr = txtAvstock.value.split(" ")
	 if((txtQty.value=="" || txtQty.value=="0") && (txtRate.value!=""))
	 {
		alert("Please insert the Quantity")
	
		return
	 }
	 if(document.Form1.btnEdit == null )
	 {

	 var temp2 = txtTempQty.value;

	 if(eval(txtQty.value) > eval(txtTempQty.value))
	 {
	 temp = eval(txtQty.value) - eval(txtTempQty.value);
 
	  if(eval(temp) > eval(sarr[0]))
	 {
		alert("Insufficient Stock")
		txtQty.value=txtTempQty.value;
		txtQty.focus()
		return
	  }
	 
	 }
	 }
	 else
	 {

	 if(eval(txtQty.value)>eval(sarr[0]))
	 {
		alert("Insufficient Stock")
		txtQty.value=""
		txtQty.focus()
		return
	  }
	 }
	 	  
	  
	  
	
	 document.Form1.txtAmount1.value=document.Form1.txtQty1.value*document.Form1.txtRate1.value	
	 if(document.Form1.txtAmount1.value==0)
		document.Form1.txtAmount1.value=""
	 document.Form1.txtAmount2.value= document.Form1.txtQty2.value*document.Form1.txtRate2.value	
 	 if(document.Form1.txtAmount2.value==0)
		document.Form1.txtAmount2.value=""
	 document.Form1.txtAmount3.value= document.Form1.txtQty3.value*document.Form1.txtRate3.value	
	 if(document.Form1.txtAmount3.value==0)
		document.Form1.txtAmount3.value=""
	 document.Form1.txtAmount4.value= document.Form1.txtQty4.value*document.Form1.txtRate4.value	
	 if(document.Form1.txtAmount4.value==0)
		document.Form1.txtAmount4.value=""
	 document.Form1.txtAmount5.value= document.Form1.txtQty5.value*document.Form1.txtRate5.value	
	 if(document.Form1.txtAmount5.value==0)
		document.Form1.txtAmount5.value=""
	 document.Form1.txtAmount6.value= document.Form1.txtQty6.value*document.Form1.txtRate6.value	
	 if(document.Form1.txtAmount6.value==0)
		document.Form1.txtAmount6.value=""
	 document.Form1.txtAmount7.value= document.Form1.txtQty7.value*document.Form1.txtRate7.value	
	 if(document.Form1.txtAmount7.value==0)
		document.Form1.txtAmount7.value=""
	 document.Form1.txtAmount8.value= document.Form1.txtQty8.value*document.Form1.txtRate8.value	
	 if(document.Form1.txtAmount8.value==0)
		document.Form1.txtAmount8.value=""

	 GetGrandTotal()
	 GetNetAmount()
	}	
	function GetGrandTotal()
	{
	 var GTotal=0
	 if(document.Form1.txtAmount1.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount1.value)
	 if(document.Form1.txtAmount2.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount2.value)
	 if(document.Form1.txtAmount3.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount3.value)
	 if(document.Form1.txtAmount4.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount4.value)
	 if(document.Form1.txtAmount5.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount5.value)
	 if(document.Form1.txtAmount6.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount6.value)
	 if(document.Form1.txtAmount7.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount7.value)
	 if(document.Form1.txtAmount8.value!="")
	 	GTotal=GTotal+eval(document.Form1.txtAmount8.value)
	 document.Form1.txtGrandTotal.value=GTotal ;
	 makeRound(document.Form1.txtGrandTotal);
	}	
	
	function GetCashDiscount()
	{
	 var CashDisc=document.Form1.txtCashDisc.value
	 if(CashDisc=="" || isNaN(CashDisc))
		CashDisc=0
	
		if(document.Form1.DropCashDiscType.value=="Per")
			CashDisc=document.Form1.txtGrandTotal.value*CashDisc/100 
		document.Form1.txtVatValue.value = "";	
		document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) - eval(CashDisc);	
					    
	}
	
	function GetVatAmount()
	{
	    GetCashDiscount()
	    
	    if(document.Form1.No.checked)
	    {
	       document.Form1.txtVAT.value = "";
	      
	    } 
	    else
	    {
	    var vat_rate = document.Form1.txtVatRate.value
	   // alert(vat_rate);
	    if(vat_rate == "")
	       vat_rate = 0;
	     var vat = document.Form1.txtVatValue.value    
	         if(vat == "" || isNaN(vat))
	       vat = 0;
	       //alert("disc: "+vat)
	     var vat_amount = vat * vat_rate/100
	    // alert("vat_amt : "+vat_amount)
	     document.Form1.txtVAT.value = vat_amount
	     makeRound(document.Form1.txtVAT)
	    
	     
	     document.Form1.txtVatValue.value = eval(vat) + eval(vat_amount)
	    // alert("total :"+document.Form1.txtVatValue.value)
	     
	       
	    }
	
	}
	
	function GetNetAmount()
	{
	
	var vat_value = 0;
	if(document.Form1.No.checked)
	    {
	    GetCashDiscount()
	    vat_value = document.Form1.txtVatValue.value;
	    document.Form1.txtVAT.value = "";
	    }
	    else
	    {
	    GetVatAmount()
	    vat_value = document.Form1.txtVatValue.value;
	    }
	    
	    if(vat_value=="" || isNaN(vat_value))
		vat_value=0
	 var Disc=document.Form1.txtDisc.value
	 if(Disc=="" || isNaN(Disc))
		Disc=0

	 var NetAmount
		if(document.Form1.DropDiscType.value=="Per")
			Disc=vat_value * Disc/100 

		
		document.Form1.txtNetAmount.value=eval(vat_value) - eval(Disc);
		makeRound(document.Form1.txtNetAmount);
		var index = document.Form1.DropSalesType.selectedIndex;
		var val =  document.Form1.DropSalesType.options[index].text;
		if(val == "Credit")
		{
		//alert(document.Form1.txtNetAmount.value);
		if(eval(document.Form1.txtNetAmount.value) > eval(document.Form1.TxtCrLimit.value))
		 {
		    alert("Credit Limit is less than Net Amount")
		    return;
		 }
		 else
		 {
		   document.Form1.lblCreditLimit.value = eval(document.Form1.TxtCrLimit.value) - eval(document.Form1.txtNetAmount.value)
		 }
		}
		else
		{
		  document.Form1.lblCreditLimit.value = document.Form1.TxtCrLimit.value
		}
		
		 
		if(document.Form1.txtNetAmount.value==0)
			document.Form1.txtNetAmount.value==""
	}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<INPUT id="tmpQty4" style="Z-INDEX: 122; LEFT: 390px; WIDTH: 7px; POSITION: absolute; TOP: -3px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty4" runat="server"><INPUT id="tmpQty5" style="Z-INDEX: 123; LEFT: 399px; WIDTH: 7px; POSITION: absolute; TOP: -1px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty5" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><INPUT id="TxtVen" style="Z-INDEX: 101; LEFT: -544px; POSITION: absolute; TOP: -16px" type="text"
				name="TxtVen" runat="server"> <INPUT id="TextBox1" style="Z-INDEX: 102; LEFT: -248px; WIDTH: 76px; POSITION: absolute; TOP: -40px; HEIGHT: 22px"
				readOnly type="text" size="7" name="TextBox1" runat="server"> <INPUT id="TxtEnd" style="Z-INDEX: 103; LEFT: -208px; WIDTH: 52px; POSITION: absolute; TOP: -24px; HEIGHT: 22px"
				type="text" size="3" name="TxtEnd" runat="server"><INPUT id="Txtstart" style="Z-INDEX: 104; LEFT: -336px; WIDTH: 83px; POSITION: absolute; TOP: -16px; HEIGHT: 22px"
				type="text" size="8" name="Txtstart" runat="server"> <INPUT id="TxtCrLimit" style="Z-INDEX: 105; LEFT: -448px; WIDTH: 70px; POSITION: absolute; TOP: -16px; HEIGHT: 22px"
				accessKey="TxtEnd" type="text" size="6" name="TxtCrLimit" runat="server">
			<asp:textbox id="TextSelect" style="Z-INDEX: 106; LEFT: 216px; POSITION: absolute; TOP: 16px"
				runat="server" Width="16px" Visible="False"></asp:textbox><asp:textbox id="TextBox2" style="Z-INDEX: 107; LEFT: 192px; POSITION: absolute; TOP: 24px" runat="server"
				Width="8px" Visible="False" BorderStyle="Groove"></asp:textbox><asp:textbox id="TxtCrLimit1" style="Z-INDEX: 108; LEFT: 176px; POSITION: absolute; TOP: 16px"
				runat="server" Width="16px" Visible="False" BorderStyle="Groove" ReadOnly="True"></asp:textbox><INPUT id="temptext" style="Z-INDEX: 109; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="temptext" runat="server">
			<asp:textbox id="txtTempQty1" style="Z-INDEX: 110; LEFT: 240px; POSITION: absolute; TOP: 16px"
				runat="server" Width="6px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty2" style="Z-INDEX: 111; LEFT: 256px; POSITION: absolute; TOP: 8px"
				runat="server" Width="8px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty3" style="Z-INDEX: 112; LEFT: 272px; POSITION: absolute; TOP: 8px"
				runat="server" Width="4px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty4" style="Z-INDEX: 113; LEFT: 284px; POSITION: absolute; TOP: 8px"
				runat="server" Width="9px" Visible="False"></asp:textbox><asp:textbox id="TextBox7" style="Z-INDEX: 114; LEFT: 336px; POSITION: absolute; TOP: 0px" runat="server"
				Width="2px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty5" style="Z-INDEX: 115; LEFT: 296px; POSITION: absolute; TOP: 8px"
				runat="server" Width="4px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty6" style="Z-INDEX: 116; LEFT: 304px; POSITION: absolute; TOP: 8px"
				runat="server" Width="4px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty7" style="Z-INDEX: 117; LEFT: 312px; POSITION: absolute; TOP: 0px"
				runat="server" Width="4px" Visible="False"></asp:textbox><asp:textbox id="txtTempQty8" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Width="8px" Visible="False"></asp:textbox><INPUT id="tmpQty1" style="Z-INDEX: 119; LEFT: 350px; WIDTH: 10px; POSITION: absolute; TOP: 2px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty1" runat="server"> <INPUT id="tmpQty2" style="Z-INDEX: 120; LEFT: 365px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty2" runat="server"><INPUT id="tmpQty3" style="Z-INDEX: 121; LEFT: 377px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty3" runat="server"><INPUT id="tmpQty6" style="Z-INDEX: 124; LEFT: 410px; WIDTH: 6px; POSITION: absolute; TOP: 0px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty6" runat="server"><INPUT id="tmpQty7" style="Z-INDEX: 125; LEFT: 416px; WIDTH: 5px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty7" runat="server"><INPUT id="tmpQty8" style="Z-INDEX: 126; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty8" runat="server"> <INPUT id="txtVatRate" style="Z-INDEX: 127; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 22px"
				type="hidden" size="1" name="txtVatRate" runat="server"> <INPUT id="txtVatValue" style="Z-INDEX: 128; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtVatValue" runat="server"> <INPUT id="txtSlipTemp" style="Z-INDEX: 129; LEFT: 462px; WIDTH: 6px; POSITION: absolute; TOP: 7px; HEIGHT: 22px"
				type="hidden" size="1" name="txtSlipTemp" runat="server"><INPUT id="SlipNo" style="Z-INDEX: 130; LEFT: 478px; WIDTH: 5px; POSITION: absolute; TOP: 7px; HEIGHT: 22px"
				type="hidden" size="1" name="SlipNo" runat="server"> <INPUT id="lblVehicleNo" style="Z-INDEX: 131; LEFT: 488px; WIDTH: 12px; POSITION: absolute; TOP: 8px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server">
			<table height="278" width="778" align=center>
				<tr>
					<th style="HEIGHT: 52px" align="center" colSpan="3">
						<font color="#CE4848">Cash Sales&nbsp;Invoice</font>
						<hr>
						<asp:label id="lblMessage" runat="server" ForeColor="DarkGreen" Font-Size="8pt"></asp:label></th></tr>
				<tr>
					<td align="center">
						<TABLE style="WIDTH: 457px; HEIGHT: 351px" border="1">
							<TBODY>
								<TR>
									<TD style="WIDTH: 241px; HEIGHT: 160px" align="center">
										<TABLE style="WIDTH: 258px; HEIGHT: 149px">
											<TR>
												<TD>Invoice No</TD>
												<TD><asp:dropdownlist id="dropInvoiceNo" runat="server" Width="89px" Visible="False" AutoPostBack="True">
														<asp:ListItem Value="Select">Select</asp:ListItem>
													</asp:dropdownlist><asp:label id="lblInvoiceNo" runat="server" Width="80px" ForeColor="Blue"></asp:label><asp:button id="btnEdit" runat="server" Width="25px" ToolTip="Click For Edit" Text="..." CausesValidation="False"></asp:button></TD>
											</TR>
											<TR>
												<TD>Invoice Date</TD>
												<TD><asp:label id="lblInvoiceDate" runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 31px">Customer Name
													<asp:comparevalidator id="CompareValidator1" runat="server" Operator="NotEqual" ValueToCompare="Select"
														ControlToValidate="DropCustName" ErrorMessage="Please Select Customer Name">*</asp:comparevalidator></TD>
												<TD style="HEIGHT: 31px" align="right"><asp:dropdownlist id="DropCustName" runat="server" Width="167px" AutoPostBack="True">
														<asp:ListItem Value="Select">Select</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD>Vehicle No
													<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtVehicleNo" ErrorMessage="Please Enter Vehicle No.">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="DropVehicleNo" ErrorMessage="Please Select Vehicle No."
														InitialValue="Select">*</asp:requiredfieldvalidator></TD>
												<TD align="right"><asp:textbox id="txtVehicleNo" runat="server" Width="168px" BorderStyle="Groove" Font-Size="Larger"></asp:textbox><asp:dropdownlist id="DropVehicleNo" runat="server" Width="168px" Visible="False"></asp:dropdownlist></TD>
											</TR>
										</TABLE>
									</TD>
									<TD style="HEIGHT: 160px" align="center">
										<TABLE style="HEIGHT: 145px">
											<TR>
												<TD style="HEIGHT: 24px" align="center" colSpan="2">&nbsp;&nbsp; <STRONG><U>Product Sales</U>&nbsp;&nbsp;
													</STRONG>
												</TD>
											</TR>
											<TR>
												<TD><FONT color="orange"><STRONG>Petrol</STRONG></FONT></TD>
												<TD style="HEIGHT: 27px"><INPUT id="txtPetrolSales" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="5" name="Text1" runat="server"></TD>
											</TR>
											<TR>
												<TD><STRONG><FONT color="darkblue">Diesel</FONT></STRONG></TD>
												<TD style="HEIGHT: 17px"><INPUT id="txtDieselSales" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="5" name="Text2" runat="server"></TD>
											</TR>
											<TR>
												<TD><STRONG><FONT color="gold">Super&nbsp;Petrol</FONT></STRONG>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
												<TD><INPUT id="txtSPetrolSales" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="5" name="Text3" runat="server"></TD>
											</TR>
											<TR>
												<TD><STRONG><FONT color="green">Super Diesel</FONT></STRONG></TD>
												<TD><INPUT id="txtSDieselSales" style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="5" name="Text4" runat="server"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2">
										<TABLE style="WIDTH: 446px; HEIGHT: 209px">
											<TBODY>
												<TR>
													<TD style="HEIGHT: 4px" align="center" colSpan="7"><FONT color="#990066"><STRONG><U>Product 
																	&nbsp;Details</U></STRONG></FONT></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 193px; HEIGHT: 6px" align="center"><FONT color="#990066">Name
															<asp:comparevalidator id="CompareValidator4" runat="server" Operator="NotEqual" ValueToCompare="Select"
																ControlToValidate="DropProd1" ErrorMessage="Please Select atleast One Product Name">*</asp:comparevalidator></FONT></TD>
													<TD style="HEIGHT: 6px" align="center"><FONT color="#990066">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															Qty
															<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtQty1" ErrorMessage="Please Fill Quantity">*</asp:requiredfieldvalidator></FONT></TD>
													<TD style="HEIGHT: 6px" align="center"><FONT color="#990066">&nbsp;&nbsp; Rate</FONT></TD>
													<TD style="HEIGHT: 6px" align="center"><FONT color="#990066">&nbsp;&nbsp; Amount</FONT></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 193px"><asp:dropdownlist id="DropProd1" runat="server" Width="185px" onchange="getPack(document.Form1.DropType1,this,document.Form1.DropPack1,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.txtProdName1,document.Form1.txtPack1,document.Form1.txtQty1,document.Form1.txtAmount1)"
															Height="8px">
															<asp:ListItem Value="Select">Select</asp:ListItem>
														</asp:dropdownlist><INPUT id="txtProdName1" style="WIDTH: 140px" type="hidden" name="txtProdName1" runat="server"></TD>
													<TD align="right"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty1" onblur="calc(this,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.tmpQty1)"
															runat="server" Width="52px" BorderStyle="Groove"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtRate1" runat="server" Width="52px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtAmount1" runat="server" Width="79px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 193px"><asp:dropdownlist id="DropProd2" runat="server" Width="185px" onchange="getPack(document.Form1.DropType2,this,document.Form1.DropPack2,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.txtProdName2,document.Form1.txtPack2,document.Form1.txtQty2,document.Form1.txtAmount2)"
															Height="8px">
															<asp:ListItem Value="Select">Select</asp:ListItem>
														</asp:dropdownlist><INPUT id="txtProdName2" style="WIDTH: 140px" type="hidden" name="txtProdName2" runat="server"></TD>
													<TD align="right"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty2" onblur="calc(this,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.tmpQty2)"
															runat="server" Width="52px" BorderStyle="Groove"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtRate2" runat="server" Width="52px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtAmount2" runat="server" Width="79px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 193px"><asp:dropdownlist id="DropProd3" runat="server" Width="185px" onchange="getPack(document.Form1.DropType3,this,document.Form1.DropPack3,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.txtProdName3,document.Form1.txtPack3,document.Form1.txtQty3,document.Form1.txtAmount3)"
															Height="8px">
															<asp:ListItem Value="Select">Select</asp:ListItem>
														</asp:dropdownlist><INPUT id="txtProdName3" style="WIDTH: 140px" type="hidden" name="txtProdName3" runat="server"></TD>
													<TD align="right"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty3" onblur="calc(this,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.tmpQty3)"
															runat="server" Width="52px" BorderStyle="Groove"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtRate3" runat="server" Width="52px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtAmount3" runat="server" Width="79px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 193px"><asp:dropdownlist id="DropProd4" runat="server" Width="185px" onchange="getPack(document.Form1.DropType4,this,document.Form1.DropPack4,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.txtProdName4,document.Form1.txtPack4,document.Form1.txtQty4,document.Form1.txtAmount4)"
															Height="8px">
															<asp:ListItem Value="Select">Select</asp:ListItem>
														</asp:dropdownlist><INPUT id="txtProdName4" style="WIDTH: 140px" type="hidden" name="txtProdName4" runat="server"></TD>
													<TD align="right"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty4" onblur="calc(this,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.tmpQty4)"
															runat="server" Width="52px" BorderStyle="Groove"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtRate4" runat="server" Width="52px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
													<TD align="right"><asp:textbox id="txtAmount4" runat="server" Width="79px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
												</TR>
											</TBODY>
										</TABLE>
									</TD>
								</TR>
							</TBODY>
						</TABLE>
						<TABLE style="WIDTH: 448px; HEIGHT: 77px">
							<TR>
								<TD></TD>
								<TD></TD>
								<TD style="WIDTH: 52px"></TD>
								<TD align="right">Grand Total</TD>
								<TD align="right"><asp:textbox id="txtGrandTotal" runat="server" Width="102px" BorderStyle="Groove" ReadOnly="True"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 17px">Entry&nbsp;By</TD>
								<TD style="HEIGHT: 17px"><asp:label id="lblEntryBy" runat="server"></asp:label></TD>
								<TD style="WIDTH: 52px; HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
								<TD style="HEIGHT: 17px"></TD>
							</TR>
							<TR>
								<TD>Entry Date &amp; Time</TD>
								<TD><asp:label id="lblEntryTime" runat="server"></asp:label></TD>
								<TD style="WIDTH: 52px"></TD>
								<TD align="right" colSpan="2"><asp:button id="Button1" runat="server" Width="70px" ForeColor="white" Text="Pre Print" BorderColor="#CE4848"
										BackColor="#CE4848"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnSave" runat="server" Width="70px"  Text="Print" 
										 onclick="btnSave_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" Width="468px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></td>
				</tr>
			</table>
			<uc2:footer id="Footer1" runat="server"></uc2:footer></form>
		<script language="C#">


		</script>
	</body>
</HTML>
