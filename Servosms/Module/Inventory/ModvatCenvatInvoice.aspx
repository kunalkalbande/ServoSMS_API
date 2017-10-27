<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Import namespace="System.Data.SqlClient"%>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.ModvatCenvatInvoice" CodeFile="ModvatCenvatInvoice.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: ModVat/CenVat Invoice</title> 
		<!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
	--><LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="sales" src="../../Sysitem/JS/Sales.js"></script>
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="fuel" src="../../Sysitem/JS/Fuel.js"></script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		packArray[0]=document.Form1.DropType1.value
		packArray[1]=document.Form1.DropType2.value
		packArray[2]=document.Form1.DropType3.value
		packArray[3]=document.Form1.DropType4.value
		packArray[4]=document.Form1.DropType5.value
		packArray[5]=document.Form1.DropType6.value
		packArray[6]=document.Form1.DropType7.value
		packArray[7]=document.Form1.DropType8.value
		packArray[8]=document.Form1.DropType9.value
		packArray[9]=document.Form1.DropType10.value
		packArray[10]=document.Form1.DropType11.value
		packArray[11]=document.Form1.DropType12.value
			
		var count=0;
		for (var i=0;i<12;i++)
		{
			for (var j=0;j<12;j++)
			{
				if(packArray[i]==packArray[j] && packArray[i]!="Type")
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
		
	var qtyfoe=0
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
	 
	function calc(txtQty,txtAvstock,txtRate,txtTempQty,tempint,ProdName,PackType)
	{	
		var sarr = new Array()
		var temp ="";
		var tempint=tempint;
		var max=document.Form1.tempminmax.value;
		var minmaxarr = new Array()
		var maxarr = new Array()
		var vmm=""
		minmaxarr = max.split("~")
	 
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
				for(var i=0;i<minmaxarr.length;i++)
				{
					vmm=minmaxarr[i]
					maxarr=vmm.split(":")
					if(ProdName.value==maxarr[0] && PackType.value==maxarr[1]) 
					{
						if(eval(eval(sarr[0])-eval(txtQty.value)) <= eval(maxarr[4]))
						{
							alert("Quantity of ''"+maxarr[0]+"'' is below the minimum level") 
						}
					}
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
		for(var i=0;i<minmaxarr.length;i++)
		{
			vmm=minmaxarr[i]
			maxarr=vmm.split(":")
			if(ProdName.value==maxarr[0] && PackType.value==maxarr[1]) 
			{
				if(eval(eval(sarr[0])-eval(txtQty.value)) <= eval(maxarr[4]))
				{
					alert("Quantity of ''"+maxarr[0]+"'' is below the minimum level") 
				}
			}
		}
		<% for(int k=1; k<=12; k++) {%>
			document.Form1.txtAmount<%=k%>.value=document.Form1.txtQty<%=k%>.value*document.Form1.txtRate<%=k%>.value
			if(document.Form1.txtAmount<%=k%>.value==0)
				document.Form1.txtAmount<%=k%>.value=""
			makeRound(document.Form1.txtAmount<%=k%>);
		<%}%>
	 
		<% for(int k=1; k<=12; k++) {%>
		if(tempint==<%=k%>)
			getscheme(document.Form1.DropType<%=k%>,document.Form1.txtProdName<%=k%>,document.Form1.txtPack<%=k%>,document.Form1.txtQty<%=k%>,document.Form1.txtTypesch<%=k%>,document.Form1.txtProdsch<%=k%>,document.Form1.txtPacksch<%=k%>,document.Form1.txtQtysch<%=k%>,document.Form1.lblInvoiceDate,document.Form1.txtstk<%=k%>,document.Form1.txtsch<%=k%>,document.Form1.txtfoe<%=k%>)
		<%}%>	
		
		changescheme()
		GetGrandTotal()
		GetNetAmount()
	}	
	
	function calc2(txtQty,txtAvstock,txtRate,txtTempQty,tempint,ProdType)
	{	
		var sarr = new Array()
		var temp ="";
		var tempint=tempint;
		var max=document.Form1.tempminmax.value;
		var minmaxarr = new Array()
		var maxarr = new Array()
		var vmm=""
		minmaxarr = max.split("~")
	 
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
				for(var i=0;i<minmaxarr.length;i++)
				{
					vmm=minmaxarr[i]
					maxarr=vmm.split(":")
					if(maxarr[0]+":"+maxarr[1]==ProdType.value) 
					{
						if(eval(eval(sarr[0])-eval(txtQty.value)+eval(txtTempQty.value)) <= eval(maxarr[4]))
						{
							alert("Quantity of ''"+maxarr[0]+"'' is below the minimum level") 
						}
					}
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
			for(var i=0;i<minmaxarr.length;i++)
			{
				vmm=minmaxarr[i]
				maxarr=vmm.split(":")
				if(maxarr[0]+":"+maxarr[1]==ProdType.value)
				{
					if(eval(eval(sarr[0])-eval(txtQty.value)) <= eval(maxarr[4]))
					{
						alert("Quantity of ''"+maxarr[0]+"'' is below the minimum level") 
					}
				}
			}
		}
	
		<% for(int k=1; k<=12; k++) {%>
			document.Form1.txtAmount<%=k%>.value=document.Form1.txtQty<%=k%>.value*document.Form1.txtRate<%=k%>.value
			if(document.Form1.txtAmount<%=k%>.value==0)
				document.Form1.txtAmount<%=k%>.value=""
			makeRound(document.Form1.txtAmount<%=k%>);
		<%}%>
	 
	if(tempint==1)
		getscheme2(document.Form1.DropType1,document.Form1.txtQty1,document.Form1.txtTypesch1,document.Form1.txtQtysch1,document.Form1.lblInvoiceDate,document.Form1.txtstk1,document.Form1.txtsch1,document.Form1.txtfoe1,document.Form1.tmpSchType1)
	if(tempint==2)
		getscheme2(document.Form1.DropType2,document.Form1.txtQty2,document.Form1.txtTypesch2,document.Form1.txtQtysch2,document.Form1.lblInvoiceDate,document.Form1.txtstk2,document.Form1.txtsch2,document.Form1.txtfoe2,document.Form1.tmpSchType2)
	if(tempint==3)
		getscheme2(document.Form1.DropType3,document.Form1.txtQty3,document.Form1.txtTypesch3,document.Form1.txtQtysch3,document.Form1.lblInvoiceDate,document.Form1.txtstk3,document.Form1.txtsch3,document.Form1.txtfoe3,document.Form1.tmpSchType3)
	if(tempint==4)
		getscheme2(document.Form1.DropType4,document.Form1.txtQty4,document.Form1.txtTypesch4,document.Form1.txtQtysch4,document.Form1.lblInvoiceDate,document.Form1.txtstk4,document.Form1.txtsch4,document.Form1.txtfoe4,document.Form1.tmpSchType4)
	if(tempint==5)
		getscheme2(document.Form1.DropType5,document.Form1.txtQty5,document.Form1.txtTypesch5,document.Form1.txtQtysch5,document.Form1.lblInvoiceDate,document.Form1.txtstk5,document.Form1.txtsch5,document.Form1.txtfoe5,document.Form1.tmpSchType5)
	if(tempint==6)
		getscheme2(document.Form1.DropType6,document.Form1.txtQty6,document.Form1.txtTypesch6,document.Form1.txtQtysch6,document.Form1.lblInvoiceDate,document.Form1.txtstk6,document.Form1.txtsch6,document.Form1.txtfoe6,document.Form1.tmpSchType6)
	if(tempint==7)
		getscheme2(document.Form1.DropType7,document.Form1.txtQty7,document.Form1.txtTypesch7,document.Form1.txtQtysch7,document.Form1.lblInvoiceDate,document.Form1.txtstk7,document.Form1.txtsch7,document.Form1.txtfoe7,document.Form1.tmpSchType7)
	if(tempint==8)
		getscheme2(document.Form1.DropType8,document.Form1.txtQty8,document.Form1.txtTypesch8,document.Form1.txtQtysch8,document.Form1.lblInvoiceDate,document.Form1.txtstk8,document.Form1.txtsch8,document.Form1.txtfoe8,document.Form1.tmpSchType8)
    if(tempint==9)
		getscheme2(document.Form1.DropType9,document.Form1.txtQty9,document.Form1.txtTypesch9,document.Form1.txtQtysch9,document.Form1.lblInvoiceDate,document.Form1.txtstk9,document.Form1.txtsch9,document.Form1.txtfoe9,document.Form1.tmpSchType9)
	if(tempint==10)
		getscheme2(document.Form1.DropType10,document.Form1.txtQty10,document.Form1.txtTypesch10,document.Form1.txtQtysch10,document.Form1.lblInvoiceDate,document.Form1.txtstk10,document.Form1.txtsch10,document.Form1.txtfoe10,document.Form1.tmpSchType10)
	if(tempint==11)
		getscheme2(document.Form1.DropType11,document.Form1.txtQty11,document.Form1.txtTypesch11,document.Form1.txtQtysch11,document.Form1.lblInvoiceDate,document.Form1.txtstk11,document.Form1.txtsch11,document.Form1.txtfoe11,document.Form1.tmpSchType11)
	if(tempint==12)
		getscheme2(document.Form1.DropType12,document.Form1.txtQty12,document.Form1.txtTypesch12,document.Form1.txtQtysch12,document.Form1.lblInvoiceDate,document.Form1.txtstk12,document.Form1.txtsch12,document.Form1.txtfoe12,document.Form1.tmpSchType12)
	
	changescheme()
	GetGrandTotal()
	GetNetAmount()
	
	}
	
	function GetGrandTotal()
	{
	 var GTotal=0
	 
		<% for(int k=1; k<=12; k++) {%>
		if(document.Form1.txtAmount<%=k%>.value!="")
	 		GTotal=GTotal+eval(document.Form1.txtAmount<%=k%>.value)
	 	<%}%>
		 	
	 document.Form1.txtGrandTotal.value=GTotal ;
	 makeRound(document.Form1.txtGrandTotal);
	
	}	
	
	function TotalLiter()
	{
		changeschemefoe()
		document.Form1.txtliter.value=qtyfoe
	}
	
	function GetCashDiscount()
	{
		changescheme()
		changeschemefoe()
		var Scheme=document.Form1.txtschemetotal.value
		if(Scheme=="" || isNaN(Scheme))
			Scheme=0
		var Disc=document.Form1.txtDisc.value
		if(Disc=="" || isNaN(Disc))
			Disc=0

		if(document.Form1.DropDiscType.value=="Per")
		Disc=(document.Form1.txtGrandTotal.value-eval(Scheme))*Disc/100 
		document.Form1.tempdiscount.value=eval(Disc)
		makeRound(document.Form1.tempdiscount)
		document.Form1.txtDiscount.value = document.Form1.tempdiscount.value;
			  
		TotalLiter()
	   
		var foe=qt
		document.Form1.txtfleetoediscountRs.value=foe
		makeRound(document.Form1.txtfleetoediscountRs)
	  
		var CashDisc=document.Form1.txtCashDisc.value
		if(CashDisc=="" || isNaN(CashDisc))
			CashDisc=0
		if(document.Form1.DropCashDiscType.value=="Per")
		{    
			var CashDiscount=document.Form1.txtGrandTotal.value-eval(Disc)-eval(Scheme)-eval(foe)
			CashDisc=eval(CashDiscount)*CashDisc/100 
			document.Form1.tempcashdis.value=eval(CashDisc)
			makeRound(document.Form1.tempcashdis)
		}
		document.Form1.txtCashDiscount.value = eval(CashDisc);
		makeRound(document.Form1.txtCashDiscount)
		document.Form1.txtVatValue.value = "";
		document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) - eval(CashDisc) - eval(Disc)-eval(Scheme)-eval(foe);	
	}
	
	function GetVatAmount()
	{
	    GetCashDiscount()
	    if(document.Form1.No.checked)
	       document.Form1.txtVAT.value = "";
	    else
	    {
			var vat_rate = document.Form1.txtVatRate.value
			if(vat_rate == "")
				vat_rate = 0;
			var vat = document.Form1.txtVatValue.value    
	        if(vat == "" || isNaN(vat))
				vat = 0;
			var vat_amount = vat * vat_rate/100
			document.Form1.txtVAT.value = vat_amount
			makeRound(document.Form1.txtVAT)
	        document.Form1.txtVatValue.value = eval(vat) + eval(vat_amount)
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
		document.Form1.txtNetAmount.value=eval(vat_value);
		var netamount=Math.round(document.Form1.txtNetAmount.value,0);
		netamount=netamount+".00";
		document.Form1.txtNetAmount.value=netamount;
		
		var index = document.Form1.DropSalesType.selectedIndex;
		var val =  document.Form1.DropSalesType.options[index].text;
		if(val == "Credit")
		{
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
	
	function changescheme()
		{
		
			var totol=0
			var f1=document.Form1
			var arrType= new Array();
	
		<% for(int k=1; k<=12; k++) {%>
		if(f1.DropType<%=k%>.value.indexOf(":")>0)
			arrType = f1.DropType<%=k%>.value.split(":")
		else
		{
			arrType[0]=""
			arrType[1]=""
		}
		
		if(arrType[1] != "")
		{
			var mainarr1 = new Array()
			var hidarr1  = arrType[1]
			if(arrType[1] == "Loose Oil" || arrType[1] == "Loose oil")
			{
				totol+=f1.txtQty<%=k%>.value*f1.txtsch<%=k%>.value
			}
			else
			{
				mainarr1 = hidarr1.split("X")
				if(document.Form1.tmpSchType<%=k%>.value=="%")
					totol+=(document.Form1.txtAmount<%=k%>.value*f1.txtsch<%=k%>.value)/100
				else
					totol+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value*f1.txtsch<%=k%>.value
			}
		}
		
		<%}%>
			
		f1.txtschemetotal.value=eval(totol)
		makeRound(f1.txtschemetotal)
	}
		
	var qt=0
	function changeschemefoe()
	{
		qtyfoe=0
		qt=0
		var f1=document.Form1
		var arrType = new Array();
		
		<% for(int k=1; k<=12; k++) {%>
		if(f1.DropType<%=k%>.value.indexOf(":")>0)
			arrType = f1.DropType<%=k%>.value.split(":")
		else
		{
			arrType[0]=""
			arrType[1]=""
		}
		if(arrType[1] != "")
		{
			var mainarr1 = new Array()
			var hidarr1  = arrType[1]
			
			if(arrType[1] == "Loose Oil" || arrType[1] == "Loose oil")
			{
				qtyfoe+=f1.txtQty<%=k%>.value
				qt+=f1.txtQty<%=k%>.value*f1.txtfoe<%=k%>.value
			}
			else
			{
				mainarr1 = hidarr1.split("X")				
				qtyfoe+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value
				qt+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value*f1.txtfoe<%=k%>.value
			}
		}
		<%}%>
		}
		
		function calc1(t)
		{}
		
		function checkDelRec()
		{
			if(document.Form1.btnEdit == null)
			{
				if(document.Form1.dropInvoiceNo.value!="Select")
				{
					if(confirm("Do You Want To Delete The Product"))
						document.Form1.tempDelinfo.value="Yes";
					else
						document.Form1.tempDelinfo.value="No";
				}
				else
				{
					alert("Please Select The Invoice No");
					return;
				}
			}
			else
			{
				alert("Please Click The Edit button");
				return;
			}
			if(document.Form1.tempDelinfo.value=="Yes")
				document.Form1.submit();
		}
		
		</script>
		<SCRIPT language="javascript">
<!--
var keys;
var timeStamp;
timeStamp = new Date();
function DropDownList1_onkeypress(t)
{
	alert("Welcome To Dynamic Java Script");
	var key = event.keyCode;
	event.returnValue=false;
	if((key>=97 && key<=122) || (key>=65 && key<=90) || (key>=48 && key<=57))
	{
		key = String.fromCharCode(key);
		var now = new Date();
		var diff = (now.getTime() - timeStamp.getTime());
		timeStamp = new Date();
		if (diff > 1200)
			keys = key;
		else
			keys = keys + key;
		var cnt;
		for (cnt=0;cnt<document.all(t.name).children.length;cnt++)
		{
			var itm = document.all(t.name).children[cnt].text.toLowerCase();
			if (itm.substring(0,keys.length)==keys)
			{
				document.all(t.name).selectedIndex = cnt;
				break;
			}
		}
	}
}
function testingtesting()
{
	alert("");
}
function getBalanceontext(t,City,CR_Days,Balance,CR_Limit,crlimit,vehicleno,SSR)
{
	var mainarr = new Array()
	var vehiclearr = new Array(); 
	var typetext  = t.value
	var hidtext  = document.Form1.TxtVen.value
	var hidtext1 = document.Form1.lblVehicleNo.value
	mainarr = hidtext.split("#")
	vehiclearr = hidtext1.split("#");
	var taxarr = new Array()
	var subarr = new Array()
	var k = 0
	City.value=""
	CR_Days.value=""
	CR_Limit.value=""
	crlimit.value=""
	SSR.value="Select"
	Balance.value=""
	for(var i=0;i<(mainarr.length-1);i++)
	{
		taxarr = mainarr[i].split("~")
			if(taxarr[0]==typetext)
			{
				City.value=taxarr[1];
				CR_Days.value=taxarr[2];
				CR_Limit.value=taxarr[3];
				crlimit.value=taxarr[3];
				Balance.value=taxarr[4]+" "+taxarr[5];
				SSR.value=taxarr[6]
				break;
			}	
	} 
}
function MoveFocus(t,drop,e)
{
	if(t.value != "")
	{
		if(window.event) 
		{
			var	key = e.keyCode;
			if(key==13)
			{
				drop.focus();
			}
		}
	}
}
//-->
		</SCRIPT>
	    <style type="text/css">
            .auto-style1 {
                width: 108px;
            }
        </style>
	</HEAD>
	<BODY onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><INPUT id="tmpQty4" style="Z-INDEX: 121; LEFT: 390px; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="0" name="tmpQty4" runat="server"><asp:textbox id="txtpname12" style="Z-INDEX: 149; LEFT: 752px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="0px" Height="20"></asp:textbox><asp:textbox id="txtpname11" style="Z-INDEX: 149; LEFT: 752px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="0px" Height="0px"></asp:textbox><asp:textbox id="txtpname10" style="Z-INDEX: 149; LEFT: 752px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="0px" Height="0px"></asp:textbox><asp:textbox id="txtpname9" style="Z-INDEX: 149; LEFT: 752px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="16px"></asp:textbox><asp:textbox id="txtpname8" style="Z-INDEX: 148; LEFT: 752px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtpname7" style="Z-INDEX: 147; LEFT: 728px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtpname6" style="Z-INDEX: 146; LEFT: 696px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtpname5" style="Z-INDEX: 145; LEFT: 672px; POSITION: absolute; TOP: -8px"
				runat="server" Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtpname4" style="Z-INDEX: 144; LEFT: 656px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtpname3" style="Z-INDEX: 143; LEFT: 640px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="16px"></asp:textbox><asp:textbox id="txtpname2" style="Z-INDEX: 142; LEFT: 624px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="16px"></asp:textbox><asp:textbox id="txtpname1" style="Z-INDEX: 141; LEFT: 600px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="16px"></asp:textbox><asp:textbox id="txtmwid12" style="Z-INDEX: 140; LEFT: 593px; POSITION: absolute; TOP: 6px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid11" style="Z-INDEX: 140; LEFT: 593px; POSITION: absolute; TOP: 6px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid10" style="Z-INDEX: 140; LEFT: 593px; POSITION: absolute; TOP: 6px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid9" style="Z-INDEX: 140; LEFT: 593px; POSITION: absolute; TOP: 6px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid8" style="Z-INDEX: 139; LEFT: 586px; POSITION: absolute; TOP: 6px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid7" style="Z-INDEX: 138; LEFT: 573px; POSITION: absolute; TOP: 6px" runat="server"
				Visible="False" Width="0px" Height="0px"></asp:textbox><asp:textbox id="txtmwid6" style="Z-INDEX: 137; LEFT: 564px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Width="0px" Height="16px"></asp:textbox><asp:textbox id="txtmwid5" style="Z-INDEX: 136; LEFT: 552px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid4" style="Z-INDEX: 135; LEFT: 540px; POSITION: absolute; TOP: 4px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid2" style="Z-INDEX: 134; LEFT: 523px; POSITION: absolute; TOP: -3px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox><asp:textbox id="txtmwid3" style="Z-INDEX: 133; LEFT: 530px; POSITION: absolute; TOP: 4px" runat="server"
				Visible="False" Width="0px" Height="20px"></asp:textbox>
			<INPUT id="tmpQty5" style="Z-INDEX: 122; LEFT: 399px; WIDTH: 7px; POSITION: absolute; TOP: -1px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty5" runat="server"> <INPUT id="tmpQty6" style="Z-INDEX: 122; LEFT: 399px; WIDTH: 7px; POSITION: absolute; TOP: -1px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty6" runat="server"> <INPUT id="tmpQty7" style="Z-INDEX: 122; LEFT: 399px; WIDTH: 7px; POSITION: absolute; TOP: -1px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty7" runat="server"> <INPUT id="tmpQty8" style="Z-INDEX: 122; LEFT: 399px; WIDTH: 7px; POSITION: absolute; TOP: -1px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty8" runat="server"> <INPUT id="TxtVen" style="Z-INDEX: 100; LEFT: -544px; POSITION: absolute; TOP: -16px; HEIGHT: 20px"
				type="text" name="TxtVen" runat="server"> <INPUT id="TextBox1" style="Z-INDEX: 101; LEFT: -248px; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				readOnly type="text" size="7" name="TextBox1" runat="server"> <INPUT id="TxtEnd" style="Z-INDEX: 102; LEFT: -208px; WIDTH: 52px; POSITION: absolute; TOP: -24px; HEIGHT: 20px"
				type="text" size="3" name="TxtEnd" runat="server"> <INPUT id="Txtstart" style="Z-INDEX: 103; LEFT: -336px; WIDTH: 83px; POSITION: absolute; TOP: -16px; HEIGHT: 20px"
				type="text" size="8" name="Txtstart" runat="server"> <INPUT id="TxtCrLimit" style="Z-INDEX: 104; LEFT: -448px; WIDTH: 70px; POSITION: absolute; TOP: -16px; HEIGHT: 20px"
				accessKey="TxtEnd" type="text" size="6" name="TxtCrLimit" runat="server">
			<asp:textbox id="TxtCrLimit1" style="Z-INDEX: 107; LEFT: 176px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="16px" Height="20" ReadOnly="True" BorderStyle="Groove"></asp:textbox><INPUT id="temptext" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="temptext" runat="server"> <INPUT id="tempminmax" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="tempminmax" runat="server"> <INPUT id="temptextfoe" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="temptextfoe" runat="server"> <INPUT id="temptext11" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="temptext11" runat="server"> <INPUT id="temptext12" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="temptext12" runat="server"> <INPUT id="temptext13" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="temptext13" runat="server">
			<asp:textbox id="txtTempQty1" style="Z-INDEX: 109; LEFT: 224px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="6px" Height="20"></asp:textbox><asp:textbox id="txtTempQty2" style="Z-INDEX: 110; LEFT: 256px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempQty3" style="Z-INDEX: 111; LEFT: 272px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempQty4" style="Z-INDEX: 112; LEFT: 284px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="9px"></asp:textbox><asp:textbox id="txtTempQty5" style="Z-INDEX: 114; LEFT: 296px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempQty6" style="Z-INDEX: 115; LEFT: 304px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempQty7" style="Z-INDEX: 116; LEFT: 312px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempQty8" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempQty9" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempQty10" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempQty11" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempQty12" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempSchQty1" style="Z-INDEX: 109; LEFT: 224px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="6px"></asp:textbox><asp:textbox id="txtTempSchQty2" style="Z-INDEX: 110; LEFT: 256px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempSchQty3" style="Z-INDEX: 111; LEFT: 272px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempSchQty4" style="Z-INDEX: 112; LEFT: 284px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="9px"></asp:textbox><asp:textbox id="txtTempSchQty5" style="Z-INDEX: 114; LEFT: 296px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempSchQty6" style="Z-INDEX: 115; LEFT: 304px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempSchQty7" style="Z-INDEX: 116; LEFT: 312px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="4px"></asp:textbox><asp:textbox id="txtTempSchQty8" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempSchQty9" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempSchQty10" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempSchQty11" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="txtTempSchQty12" style="Z-INDEX: 118; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="8px"></asp:textbox><asp:textbox id="TextBox7" style="Z-INDEX: 113; LEFT: 336px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="2px"></asp:textbox><INPUT id="tmpQty1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 10px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty1" runat="server"> <INPUT id="tmpQty2" style="Z-INDEX: 119; LEFT: 365px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty2" runat="server"> <INPUT id="tmpQty3" style="Z-INDEX: 120; LEFT: 377px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty3" runat="server"> <INPUT id="texthiddenprod" style="Z-INDEX: 103; LEFT: 160px; VISIBILITY: hidden; WIDTH: 5px; POSITION: absolute; TOP: 8px; HEIGHT: 20px"
				type="text" name="texthiddenprod" runat="server"> <INPUT id="tmpQty10" style="Z-INDEX: 126; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty10" runat="server"> <INPUT id="tmpQty11" style="Z-INDEX: 126; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty11" runat="server"><INPUT id="tmpQty12" style="Z-INDEX: 126; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty12" runat="server"> <INPUT id="tmpQty9" style="Z-INDEX: 126; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQty9" runat="server"> <INPUT id="tmpQtysch3" style="Z-INDEX: 120; LEFT: 377px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch3" runat="server"> <INPUT id="tmpQtysch4" style="Z-INDEX: 123; LEFT: 410px; WIDTH: 6px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch4" runat="server"> <INPUT id="tmpQtysch5" style="Z-INDEX: 124; LEFT: 416px; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch5" runat="server"> <INPUT id="tmpQtysch6" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch6" runat="server"> <INPUT id="tmpQtysch7" style="Z-INDEX: 124; LEFT: 416px; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch7" runat="server"> <INPUT id="tmpQtysch8" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch8" runat="server"><asp:textbox id="TextBox2" style="Z-INDEX: 123; LEFT: 410px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="0px" Height="20" BorderStyle="Groove"></asp:textbox>
			<INPUT id="tmpQtysch9" style="Z-INDEX: 125; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch9" runat="server"> <INPUT id="texthidden1" style="Z-INDEX: 198; LEFT: 205px; VISIBILITY: hidden; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="text" size="1" name="texthidden1" runat="server" DESIGNTIMEDRAGDROP="3889">
			<INPUT id="tmpQtysch10" style="Z-INDEX: 125; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch10" runat="server"> <INPUT id="tmpQtysch11" style="Z-INDEX: 125; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch11" runat="server"> <INPUT id="tmpQtysch12" style="Z-INDEX: 125; LEFT: 428px; WIDTH: 2px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpQtysch12" runat="server"> <INPUT id="txtVatRate" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="txtVatRate" runat="server"> <INPUT id="txtVatValue" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="txtVatValue" runat="server"> <INPUT id="txtSlipTemp" style="Z-INDEX: 128; LEFT: 462px; WIDTH: 6px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="txtSlipTemp" runat="server"> <INPUT id="SlipNo" style="Z-INDEX: 129; LEFT: 478px; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="SlipNo" runat="server"> <INPUT id="tempcashdis" style="Z-INDEX: 129; LEFT: 478px; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempcashdis" runat="server"> <INPUT id="tempdiscount" style="Z-INDEX: 129; LEFT: 478px; WIDTH: 5px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempdiscount" runat="server"> <INPUT id="lblVehicleNo" style="Z-INDEX: 130; LEFT: 488px; WIDTH: 12px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="Hidden1" runat="server">
			<asp:textbox id="TextSelect" style="Z-INDEX: 123; LEFT: 410px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="0px" Height="5"></asp:textbox><asp:textbox id="txtcusttype" style="Z-INDEX: 131; LEFT: 503px; POSITION: absolute; TOP: 4px"
				runat="server" Visible="False" Width="4" Height="24"></asp:textbox><asp:textbox id="txtmwid1" style="Z-INDEX: 132; LEFT: 513px; POSITION: absolute; TOP: 1px" runat="server"
				Visible="False" Width="4px" Height="20px"></asp:textbox><input id="tempDelinfo" style="Z-INDEX: 123; LEFT: 410px; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" name="tempDelinfo" runat="server"> <INPUT id="tempInvoiceDate" style="Z-INDEX: 123; LEFT: 410px; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" name="tempInvoicedate" runat="server"> <INPUT id="tmpSchType1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType1" runat="server"> <INPUT id="tmpSchType2" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType2" runat="server"> <INPUT id="tmpSchType3" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType3" runat="server"> <INPUT id="tmpSchType4" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType4" runat="server"> <INPUT id="tmpSchType5" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType5" runat="server"> <INPUT id="tmpSchType6" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType6" runat="server"> <INPUT id="tmpSchType7" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType7" runat="server"> <INPUT id="tmpSchType8" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType8" runat="server"> <INPUT id="tmpSchType9" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType9" runat="server"> <INPUT id="tmpSchType10" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType10" runat="server"> <INPUT id="tmpSchType11" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType11" runat="server"> <INPUT id="tmpSchType12" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSchType12" runat="server"><INPUT id="totalltr" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="totalltr" runat="server">
			<table height="278" width="778" align="center">
				<tr>
					<th align="center" colSpan="3">
						<font color="#ce4848">ModVat/cenVat Invoice</font>
						<hr>
						<asp:label id="lblMessage" runat="server" ForeColor="DarkGreen" Font-Size="8pt"></asp:label></th></tr>
				<tr>
					<td align="center" width="40%">
						<TABLE border="1">
							<TBODY>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD>Invoice No</TD>
												<TD noWrap><asp:dropdownlist id="dropInvoiceNo" runat="server" Visible="False" Width="125px" CssClass="dropdownlist"
														AutoPostBack="True" onselectedindexchanged="dropInvoiceNo_SelectedIndexChanged">
														<asp:ListItem Value="Select">Select</asp:ListItem>
													</asp:dropdownlist><asp:textbox id="lblInvoiceNo" runat="server" Width="107px" ReadOnly="True" BorderStyle="Groove"
														 CssClass="fontstyle"></asp:textbox><asp:button id="btnEdit" runat="server" Width="25px" ToolTip="Click For Edit"
														Text="..." CausesValidation="False" onclick="btnEdit_Click"></asp:button></TD>
											</TR>
											<TR>
												<TD>Invoice Date</TD>
												<TD><asp:textbox id="lblInvoiceDate" runat="server" Width="125px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.lblInvoiceDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
															border="0"></A></TD>
											</TR>
											<TR>
												<TD>Sales Type</TD>
												<TD><asp:dropdownlist id="DropSalesType" runat="server" Width="60px" CssClass="dropdownlist">
														<asp:ListItem Value="Cash" Selected="True">Cash</asp:ListItem>
														<asp:ListItem Value="Credit">Credit</asp:ListItem>
														<asp:ListItem Value="Van">Van</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD>Under Sales Man&nbsp;
													<asp:comparevalidator id="CompareValidator2" runat="server" ErrorMessage="Please Select Sales Man" ControlToValidate="DropUnderSalesMan"
														ValueToCompare="Select" Operator="NotEqual">*</asp:comparevalidator></TD>
												<TD><asp:dropdownlist id="DropUnderSalesMan" runat="server" Width="125px" CssClass="dropdownlist">
														<asp:ListItem Value="Select">Select</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD>Challan No</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtChallanNo"
														Width="125px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="9" Runat="server"></asp:textbox></TD>
											</TR>
											<TR>
												<TD>Challan Date</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtChallanDate"
														Width="125px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist" Runat="server"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtChallanDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
															border="0"></A></TD>
											</TR>
										</TABLE>
									</TD>
									<TD vAlign="top" align="center" width="60%">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD vAlign="middle">Customer Name&nbsp;<asp:requiredfieldvalidator id="rvf1" runat="server" ErrorMessage="Please Select Customer Name" ControlToValidate="lblPlace"
														Operator="NotEqual">*</asp:requiredfieldvalidator></TD>
												<TD vAlign="top"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="text1" onkeyup="search3(this,document.Form1.DropCustName,document.Form1.texthidden.value),arrowkeydown(this,event,document.Form1.DropCustName,document.Form1.texthidden),Selectbyenter(document.Form1.DropCustName,event,document.Form1.text1,document.Form1.txtVehicleNo)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 231px; HEIGHT: 19px" onclick="search1(document.Form1.DropCustName,document.Form1.texthidden),dropshow(document.Form1.DropCustName)"
														value="Select" name="text1" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropCustName,document.Form1.texthidden),dropshow(document.Form1.DropCustName)" readOnly type="text"
														name="temp"> <input onkeypress="return GetAnyNumber(this, event);" id="texthidden" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px"
														type="text" name="texthidden" runat="server"><br>
													<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.text1,document.Form1.txtVehicleNo),getBalance(this,document.Form1.lblPlace,document.Form1.lblDueDate,document.Form1.lblCurrBalance,document.Form1.lblCreditLimit,document.Form1.TxtCrLimit,document.Form1.txtVehicleNo,document.Form1.DropUnderSalesMan)"
															id="DropCustName" ondblclick="select(this,document.Form1.text1),getBalance(this,document.Form1.lblPlace,document.Form1.lblDueDate,document.Form1.lblCurrBalance,document.Form1.lblCreditLimit,document.Form1.TxtCrLimit,document.Form1.txtVehicleNo,document.Form1.DropUnderSalesMan)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtVehicleNo,document.Form1.text1),getBalance(this,document.Form1.lblPlace,document.Form1.lblDueDate,document.Form1.lblCurrBalance,document.Form1.lblCreditLimit,document.Form1.TxtCrLimit,document.Form1.txtVehicleNo,document.Form1.DropUnderSalesMan)"
															style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 250px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.text1)" multiple name="DropCustName"></select></div>
												</TD>
											</TR>
											<TR>
												<TD>Place</TD>
												<TD><INPUT class="dropdownlist" id="lblPlace" style="WIDTH: 250px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="22" name="lblPlace" runat="server"></TD>
											</TR>
											<TR>
												<TD>Due Date</TD>
												<TD><INPUT class="dropdownlist" id="lblDueDate" style="WIDTH: 250px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="22" name="lblDueDate" runat="server"></TD>
											</TR>
											<TR>
												<TD>Current 
													Balance&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
												<TD><INPUT class="dropdownlist" id="lblCurrBalance" style="WIDTH: 250px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="22" name="lblCurrBalance" runat="server"></TD>
											</TR>
											<TR>
												<TD>Credit Limit
												</TD>
												<TD><INPUT class="dropdownlist" id="lblCreditLimit" style="WIDTH: 250px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
														readOnly type="text" size="22" name="lblCreditLimit" runat="server"></TD>
											</TR>
											<TR>
												<TD>Vehicle No
													<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Vehicle No."
														ControlToValidate="txtVehicleNo">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Vehicle No."
														ControlToValidate="DropVehicleNo" InitialValue="Select">*</asp:requiredfieldvalidator></TD>
												<TD><asp:textbox onkeypress="return GetAnyNumber(this, event);" id="txtVehicleNo" onkeyup="MoveFocus(this,document.Form1.DropType1,event)"
														runat="server" Width="125px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="15"></asp:textbox><asp:dropdownlist id="DropVehicleNo" runat="server" Visible="False" Width="220px" CssClass="dropdownlist"></asp:dropdownlist></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" colSpan="2">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR vAlign="top">
												<TD align="center" colSpan="9" height="15"><FONT color="#990066"><STRONG><U>Product Details</U></STRONG></FONT></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3"><FONT color="#990066">SKU Name With Pack
														<asp:comparevalidator id="CompareValidator3" runat="server" ErrorMessage="Please Select Atleast one Product Type"
															ControlToValidate="DropType1" ValueToCompare="Type" Operator="NotEqual">*</asp:comparevalidator></FONT></TD>
												<TD align="center"><FONT color="#990066">Qty
														<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Please Fill Quantity"
															ControlToValidate="txtQty1">*</asp:requiredfieldvalidator></FONT></TD>
												<TD align="center"><FONT color="#990066">Scheme</FONT></TD>
												<TD align="center"><FONT color="#990066">FOC</FONT></TD>
												<TD align="center"><FONT color="#990066">AvailableStock</FONT></TD>
												<TD align="center"><FONT color="#990066">Rate</FONT></TD>
												<TD align="center"><FONT color="#990066">Amount</FONT></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType1"
														onkeyup="search3(this,document.Form1.DropProdName1,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName1,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName1,event,document.Form1.DropType1,document.Form1.txtQty1),getStock1(this,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1,1)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName1)"
														value="Type" name="DropType1" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName1)" readOnly type="text"
														name="temp1"><br>
													<div id="Layer2" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType1,document.Form1.txtQty1),getStock1(document.Form1.DropType1,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1,1)"
															id="DropProdName1" ondblclick="select(this,document.Form1.DropType1),getStock1(document.Form1.DropType1,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1,1)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty1,document.Form1.DropType1)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType1)" multiple name="DropProdName1"></select></div>
												</TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty1" onblur="calc2(this,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.tmpQty1,1,document.Form1.DropType1)"
														onkeyup="MoveFocus(this,document.Form1.DropType2,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch1" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe1" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAvStock1" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate1" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount1" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType2"
														onkeyup="search3(this,document.Form1.DropProdName2,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName2,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName2,event,document.Form1.DropType2,document.Form1.txtQty2),getStock1(this,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2,2)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName2)"
														value="Type" name="DropType2" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName2)" readOnly type="text"
														name="temp4"><br>
													<div id="Layer3" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType2,document.Form1.txtQty2),getStock1(document.Form1.DropType2,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2,2)"
															id="DropProdName2" ondblclick="select(this,document.Form1.DropType2),getStock1(document.Form1.DropType2,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2,2)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty2,document.Form1.DropType2)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType2)" multiple name="DropProdName2"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty2" onblur="calc2(this,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.tmpQty2,2,document.Form1.DropType2)"
														onkeyup="MoveFocus(this,document.Form1.DropType3,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch2" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe2" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock2" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate2" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount2" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType3"
														onkeyup="search3(this,document.Form1.DropProdName3,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName3,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName3,event,document.Form1.DropType3,document.Form1.txtQty3),getStock1(this,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3,3)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName3)"
														value="Type" name="DropType3" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName3)" readOnly type="text"
														name="temp4"><br>
													<div id="Layer4" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType3,document.Form1.txtQty3),getStock1(document.Form1.DropType3,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3,3)"
															id="DropProdName3" ondblclick="select(this,document.Form1.DropType3),getStock1(document.Form1.DropType3,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3,3)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty3,document.Form1.DropType3)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType3)" multiple name="DropProdName3"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty3" onblur="calc2(this,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.tmpQty3,3,document.Form1.DropType3)"
														onkeyup="MoveFocus(this,document.Form1.DropType4,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch3" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe3" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock3" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate3" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount3" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType4"
														onkeyup="search3(this,document.Form1.DropProdName4,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName4,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName4,event,document.Form1.DropType4,document.Form1.txtQty4),getStock1(this,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4,4)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName4)"
														value="Type" name="DropType4" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName4)" readOnly type="text"
														name="temp5"><br>
													<div id="Layer5" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType4,document.Form1.txtQty4),getStock1(document.Form1.DropType4,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4,4)"
															id="DropProdName4" ondblclick="select(this,document.Form1.DropType4),getStock1(document.Form1.DropType4,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4,4)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty4,document.Form1.DropType4)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType4)" multiple name="DropProdName4"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty4" onblur="calc2(this,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.tmpQty4,4,document.Form1.DropType4)"
														onkeyup="MoveFocus(this,document.Form1.DropType5,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch4" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe4" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock4" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate4" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount4" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType5"
														onkeyup="search3(this,document.Form1.DropProdName5,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName5,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName5,event,document.Form1.DropType5,document.Form1.txtQty5),getStock1(this,document.Form1.txtAvStock5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5,5)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName5)"
														value="Type" name="DropType5" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName5)" readOnly type="text"
														name="temp5"><br>
													<div id="Layer6" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType5,document.Form1.txtQty5),getStock1(document.Form1.DropType5,document.Form1.txtAvStock5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5,5)"
															id="DropProdName5" ondblclick="select(this,document.Form1.DropType5),getStock1(document.Form1.DropType5,document.Form1.txtAvStock5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5,5)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty5,document.Form1.DropType5)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType5)" multiple name="DropProdName5"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty5" onblur="calc2(this,document.Form1.txtAvStock5,document.Form1.txtRate5,document.Form1.tmpQty5,5,document.Form1.DropType5)"
														onkeyup="MoveFocus(this,document.Form1.DropType6,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch5" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe5" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock5" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate5" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount5" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType6"
														onkeyup="search3(this,document.Form1.DropProdName6,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName6,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName6,event,document.Form1.DropType6,document.Form1.txtQty6),getStock1(this,document.Form1.txtAvStock6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6,6)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName6)"
														value="Type" name="DropType6" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName6)" readOnly type="text"
														name="temp6"><br>
													<div id="Layer7" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType6,document.Form1.txtQty6),getStock1(document.Form1.DropType6,document.Form1.txtAvStock6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6,6)"
															id="DropProdName6" ondblclick="select(this,document.Form1.DropType6),getStock1(document.Form1.DropType6,document.Form1.txtAvStock6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6,6)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty6,document.Form1.DropType6)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType6)" multiple name="DropProdName6"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty6" onblur="calc2(this,document.Form1.txtAvStock6,document.Form1.txtRate6,document.Form1.tmpQty6,6,document.Form1.DropType6)"
														onkeyup="MoveFocus(this,document.Form1.DropType7,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch6" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe6" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock6" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate6" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount6" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType7"
														onkeyup="search3(this,document.Form1.DropProdName7,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName7,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName7,event,document.Form1.DropType7,document.Form1.txtQty7),getStock1(this,document.Form1.txtAvStock7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7,7)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName7)"
														value="Type" name="DropType7" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName7)" readOnly type="text"
														name="temp7"><br>
													<div id="Layer8" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType7,document.Form1.txtQty7),getStock1(document.Form1.DropType7,document.Form1.txtAvStock7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7,7)"
															id="DropProdName7" ondblclick="select(this,document.Form1.DropType7),getStock1(document.Form1.DropType7,document.Form1.txtAvStock7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7,7)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty7,document.Form1.DropType7)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType7)" multiple name="DropProdName7"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty7" onblur="calc2(this,document.Form1.txtAvStock7,document.Form1.txtRate7,document.Form1.tmpQty7,7,document.Form1.DropType7)"
														onkeyup="MoveFocus(this,document.Form1.DropType8,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch7" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe7" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock7" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate7" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount7" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType8"
														onkeyup="search3(this,document.Form1.DropProdName8,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName8,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName8,event,document.Form1.DropType8,document.Form1.txtQty8),getStock1(this,document.Form1.txtAvStock8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8,8)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName8)"
														value="Type" name="DropType8" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName8)" readOnly type="text"
														name="temp8"><br>
													<div id="Layer9" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType8,document.Form1.txtQty8),getStock1(document.Form1.DropType8,document.Form1.txtAvStock8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8,8)"
															id="DropProdName8" ondblclick="select(this,document.Form1.DropType8),getStock1(document.Form1.DropType8,document.Form1.txtAvStock8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8,8)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty8,document.Form1.DropType8)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType8)" multiple name="DropProdName8"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty8" onblur="calc2(this,document.Form1.txtAvStock8,document.Form1.txtRate8,document.Form1.tmpQty8,8,document.Form1.DropType8)"
														onkeyup="MoveFocus(this,document.Form1.DropType9,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch8" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe8" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock8" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate8" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount8" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType9"
														onkeyup="search3(this,document.Form1.DropProdName9,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName9,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName9,event,document.Form1.DropType9,document.Form1.txtQty9),getStock1(this,document.Form1.txtAvStock9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9,9)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName9,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName9)"
														value="Type" name="DropType9" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName9,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName9)" readOnly type="text"
														name="temp9"><br>
													<div id="Layer10" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType9,document.Form1.txtQty9),getStock1(document.Form1.DropType9,document.Form1.txtAvStock9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9,9)"
															id="DropProdName9" ondblclick="select(this,document.Form1.DropType9),getStock1(document.Form1.DropType9,document.Form1.txtAvStock9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9,9)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty9,document.Form1.DropType9)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType9)" multiple name="DropProdName9"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty9" onblur="calc2(this,document.Form1.txtAvStock9,document.Form1.txtRate9,document.Form1.tmpQty9,9,document.Form1.DropType9)"
														onkeyup="MoveFocus(this,document.Form1.DropType10,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch9" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe9" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock9" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate9" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount9" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType10"
														onkeyup="search3(this,document.Form1.DropProdName10,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName10,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName10,event,document.Form1.DropType10,document.Form1.txtQty10),getStock1(this,document.Form1.txtAvStock10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10,10)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName10,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName10)"
														value="Type" name="DropType10" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName10,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName10)" readOnly type="text"
														name="temp10"><br>
													<div id="Layer11" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType10,document.Form1.txtQty10),getStock1(document.Form1.DropType10,document.Form1.txtAvStock10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10,10)"
															id="DropProdName10" ondblclick="select(this,document.Form1.DropType10),getStock1(document.Form1.DropType10,document.Form1.txtAvStock10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10,10)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty10,document.Form1.DropType10)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType10)" multiple name="DropProdName10"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty10" onblur="calc2(this,document.Form1.txtAvStock10,document.Form1.txtRate10,document.Form1.tmpQty10,10,document.Form1.DropType10)"
														onkeyup="MoveFocus(this,document.Form1.DropType11,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch10" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe10" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock10" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate10" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount10" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType11"
														onkeyup="search3(this,document.Form1.DropProdName11,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName11,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName11,event,document.Form1.DropType11,document.Form1.txtQty11),getStock1(this,document.Form1.txtAvStock11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11,11)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName11,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName11)"
														value="Type" name="DropType11" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName11,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName11)" readOnly type="text"
														name="temp11"><br>
													<div id="Layer12" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType11,document.Form1.txtQty11),getStock1(document.Form1.DropType11,document.Form1.txtAvStock11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11,11)"
															id="DropProdName11" ondblclick="select(this,document.Form1.DropType11),getStock1(document.Form1.DropType11,document.Form1.txtAvStock11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11,11)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty11,document.Form1.DropType11)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType11)" multiple name="DropProdName11"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty11" onblur="calc2(this,document.Form1.txtAvStock11,document.Form1.txtRate11,document.Form1.tmpQty11,11,document.Form1.DropType11)"
														onkeyup="MoveFocus(this,document.Form1.DropType12,event)" runat="server" Width="52px" BorderStyle="Groove"
														CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch11" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe11" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock11" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate11" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount11" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<TD colSpan="3"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType12"
														onkeyup="search3(this,document.Form1.DropProdName12,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName12,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName12,event,document.Form1.DropType12,document.Form1.txtQty12),getStock1(this,document.Form1.txtAvStock12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12,12)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 310px; HEIGHT: 19px" onclick="search1(document.Form1.DropProdName12,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName12)"
														value="Type" name="DropType12" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropProdName12,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName12)" readOnly type="text"
														name="temp12"><br>
													<div id="Layer13" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType12,document.Form1.txtQty12),getStock1(document.Form1.DropType12,document.Form1.txtAvStock12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12,12)"
															id="DropProdName12" ondblclick="select(this,document.Form1.DropType12),getStock1(document.Form1.DropType12,document.Form1.txtAvStock12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12,12)"
															onkeyup="arrowkeyselect(this,event,document.Form1.txtQty12,document.Form1.DropType12)" style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 330px; HEIGHT: 0px"
															onfocusout="HideList(this,document.Form1.DropType12)" multiple name="DropProdName12"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtQty12" onblur="calc2(this,document.Form1.txtAvStock12,document.Form1.txtRate12,document.Form1.tmpQty12,12,document.Form1.DropType12)"
														runat="server" Width="52px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtsch12" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoe12" runat="server"
														Width="50px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
												<TD align="center"><asp:textbox id="txtAvStock12" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
												<TD><asp:textbox id="txtRate12" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
												<TD><asp:textbox id="txtAmount12" runat="server" Width="79px" ReadOnly="True" BorderStyle="Groove"
														CssClass="dropdownlist"></asp:textbox></TD>
											</TR>
											<TR>
												<td colSpan="7">
													<table cellSpacing="0" cellPadding="0">
														<tr>
															<TD colSpan="3"><asp:textbox id="txtTypesch1" runat="server" Width="326px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch1" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td width="51">&nbsp;</td>
															<td width="51">&nbsp;</td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk1" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</tr>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch2" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch2" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td>&nbsp;</td>
															<td>&nbsp;</td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk2" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch3" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch3" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk3" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch4" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch4" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right"><asp:textbox id="txtstk4" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch5" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch5" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk5" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch6" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch6" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk6" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch7" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch7" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk7" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch8" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch8" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk8" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch9" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch9" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk9" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch10" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch10" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk10" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch11" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch11" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk11" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch12" runat="server" Width="100%" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch12" runat="server" Width="52px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td></td>
															<TD align="right" colSpan="1"><asp:textbox id="txtstk12" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></TD>
														</TR>
													</table>
												</td>
												<td colSpan="2">
													<table cellPadding="2">
														<tr>
															<td><IMG src="../../HeaderFooter/images/servo foc.jpg"></td>
														</tr>
													</table>
												</td>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TBODY>
						</TABLE>
						<TABLE cellPadding="0" border="0">
							<TR>
								<TD>Promo Scheme</TD>
								<TD><asp:textbox id="txtPromoScheme" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
								<TD class="auto-style1"></TD>
								<TD>Grand Total</TD>
								<TD><asp:textbox id="txtGrandTotal" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Scheme&nbsp;Discount&nbsp;</TD>
								<TD><asp:textbox id="txtschemetotal" onblur="GetNetAmount()" runat="server" Width="120px" ReadOnly="True"
										BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox>&nbsp;&nbsp; 
									Total Ltr.
									<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtliter" runat="server"
										Width="120px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
								<TD class="auto-style1"></TD>
								<TD>Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtDisc" onblur="GetNetAmount()"
										runat="server" Width="42px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropDiscType" runat="server" CssClass="dropdownlist" onchange="GetNetAmount()">
										<asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
										<asp:ListItem Value="Per">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtDiscount" runat="server" Width="40px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Fleet/OE Discount</TD>
								<TD><asp:textbox id="txtfleetoediscount" onblur="GetNetAmount()" runat="server" Visible="False" Width="34px"
										BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox><asp:dropdownlist id="dropfleetoediscount" onblur="GetNetAmount()" runat="server" Visible="False"
										Width="58px" Height="22px" CssClass="dropdownlist">
										<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
										<asp:ListItem Value="%">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtfleetoediscountRs" onblur="GetNetAmount()" runat="server" Width="120px" ReadOnly="True"
										BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
								<TD class="auto-style1"></TD>
								<TD>Cash Discount</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtCashDisc" onblur="GetNetAmount()"
										runat="server" Width="42px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropCashDiscType" runat="server" Height="22" CssClass="dropdownlist" onchange="GetNetAmount()">
										<asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
										<asp:ListItem Value="Per">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtCashDiscount" runat="server" Width="40px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Message</TD>
								<TD><asp:textbox id="txtMessage" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
								<TD class="auto-style1"></TD>
								<TD>Entry Tax</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtEntryTax" onblur="GetNetAmount()"
										runat="server" Width="42px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropEntryTax" runat="server" Height="22" CssClass="dropdownlist" onchange="GetNetAmount()">
										<asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
										<asp:ListItem Value="Per">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtEntryTaxAmount" runat="server" Width="40px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Remark</TD>
								<TD>
									<P><asp:textbox id="txtRemark" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist"
											MaxLength="49"></asp:textbox></P>
								</TD>
								<TD class="auto-style1"></TD>
								<TD>Excise Duty</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtExcise" onblur="GetNetAmount()"
										runat="server" Width="42px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropExcise" runat="server" Height="22" CssClass="dropdownlist" onchange="GetNetAmount()">
										<asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
										<asp:ListItem Value="Per">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtExciseAmount" runat="server" Width="40px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<tr>
								<td></td>
								<td></td>
								<td class="auto-style1"></td>
								<TD>VAT
									<asp:radiobutton id="No" onclick="return GetNetAmount();" runat="server" ToolTip="Not Applied" 
										GroupName="VAT" Checked="false"></asp:radiobutton>&nbsp;<asp:radiobutton id="Yes" onclick="return GetNetAmount();" runat="server" ToolTip="Apply" 
										GroupName="VAT" Checked="true"></asp:radiobutton></TD>
								<TD><asp:textbox id="txtVAT" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td class="auto-style1"></td>
								<TD>Net Amount</TD>
								<TD><asp:textbox id="txtNetAmount" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</tr>
							<TR>
								<TD align="right" colSpan="5"><asp:button id="btnSave" runat="server" Width="80px" 
										 Text="Save"></asp:button>&nbsp;&nbsp;<asp:button id="Button1" runat="server" Width="80px" 
										 Text="Print"></asp:button>&nbsp;&nbsp;<asp:button onmouseup="checkDelRec();" id="btnDelete" runat="server" Width="80px" 
										Text="Delete" CausesValidation="False" ></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</BODY>
</HTML>
