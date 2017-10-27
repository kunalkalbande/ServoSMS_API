<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.PerformaInvoice" CodeFile="PerformaInvoice.aspx.cs" %>
<%@ Import namespace="Servosms.Sysitem.Classes"%>
<%@ Import namespace="RMG"%>
<%@ Import namespace="System.Data.SqlClient"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ServoSMS: PerformaInvoice</title> 
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
	
	function getBalance_Cust(t,City)
	{ 
		var mainarr = new Array()
		var vehiclearr = new Array(); 
		var typetext1  = t.value
		var typetext2=typetext1.split(":")
		typetext=typetext2[0]
		
		var hidtext  = document.Form1.TxtVen.value
		
		mainarr = hidtext.split("#")
		var taxarr = new Array()
		var subarr = new Array()
		var k = 0
		for(var i=0;i<(mainarr.length-1);i++)
		{
				taxarr = mainarr[i].split("~")
				if(taxarr[0]==typetext)
				{
					City.value=taxarr[1];
					break;
				}	
		} 
	}

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
					//alert(ProdName.value+" "+maxarr[0]+" "+PackType.value+" "+maxarr[1])
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
		<%
		for(int k=1; k<=12; k++)
		{
		%>
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
	
		/***************Add by vikas sharma 09.06.09************************/
			if(ProdType.value!="Type")
			{
				var	ProdType1=ProdType.value
				var ProdType2=ProdType1.split(":")	 
				ProdType.value=ProdType2[1]+":"+ProdType2[2]
			}
			else
			{
				return
			}
			/*****************end**********************/
			
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
			ProdType.value=ProdType2[0]+":"+ProdType2[1]+":"+ProdType2[2]   //add by vikas sharma 09.06.09
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
					ProdType.value=ProdType2[0]+":"+ProdType2[1]+":"+ProdType2[2]   //add by vikas sharma 09.06.09
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
				ProdType.value=ProdType2[0]+":"+ProdType2[1]+":"+ProdType2[2]   //add by vikas sharma 09.06.09
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
	
		<%for(int k=1; k<=12; k++){%>
			document.Form1.txtAmount<%=k%>.value=document.Form1.txtQty<%=k%>.value*document.Form1.txtRate<%=k%>.value
			if(document.Form1.txtAmount<%=k%>.value==0)
				document.Form1.txtAmount<%=k%>.value=""
			makeRound(document.Form1.txtAmount<%=k%>);
		<%}%>
	 	
	
		if(tempint==1)
			getscheme2_PI(document.Form1.DropType1,document.Form1.txtQty1,document.Form1.txtTypesch1,document.Form1.txtQtysch1,document.Form1.lblInvoiceDate,document.Form1.txtstk1,document.Form1.txtsch1,document.Form1.txtfoe1,document.Form1.tmpSchType1,document.Form1.txtTempSecSP1,document.Form1.tmpSecSPType1,document.Form1.tmpFoeType1)
		if(tempint==2)
			getscheme2_PI(document.Form1.DropType2,document.Form1.txtQty2,document.Form1.txtTypesch2,document.Form1.txtQtysch2,document.Form1.lblInvoiceDate,document.Form1.txtstk2,document.Form1.txtsch2,document.Form1.txtfoe2,document.Form1.tmpSchType2,document.Form1.txtTempSecSP2,document.Form1.tmpSecSPType2,document.Form1.tmpFoeType2)
		if(tempint==3)
			getscheme2_PI(document.Form1.DropType3,document.Form1.txtQty3,document.Form1.txtTypesch3,document.Form1.txtQtysch3,document.Form1.lblInvoiceDate,document.Form1.txtstk3,document.Form1.txtsch3,document.Form1.txtfoe3,document.Form1.tmpSchType3,document.Form1.txtTempSecSP3,document.Form1.tmpSecSPType3,document.Form1.tmpFoeType3)
		if(tempint==4)
			getscheme2_PI(document.Form1.DropType4,document.Form1.txtQty4,document.Form1.txtTypesch4,document.Form1.txtQtysch4,document.Form1.lblInvoiceDate,document.Form1.txtstk4,document.Form1.txtsch4,document.Form1.txtfoe4,document.Form1.tmpSchType4,document.Form1.txtTempSecSP4,document.Form1.tmpSecSPType4,document.Form1.tmpFoeType4)
		if(tempint==5)
			getscheme2_PI(document.Form1.DropType5,document.Form1.txtQty5,document.Form1.txtTypesch5,document.Form1.txtQtysch5,document.Form1.lblInvoiceDate,document.Form1.txtstk5,document.Form1.txtsch5,document.Form1.txtfoe5,document.Form1.tmpSchType5,document.Form1.txtTempSecSP5,document.Form1.tmpSecSPType5,document.Form1.tmpFoeType5)
		if(tempint==6)
			getscheme2_PI(document.Form1.DropType6,document.Form1.txtQty6,document.Form1.txtTypesch6,document.Form1.txtQtysch6,document.Form1.lblInvoiceDate,document.Form1.txtstk6,document.Form1.txtsch6,document.Form1.txtfoe6,document.Form1.tmpSchType6,document.Form1.txtTempSecSP6,document.Form1.tmpSecSPType6,document.Form1.tmpFoeType6)
		if(tempint==7)
			getscheme2_PI(document.Form1.DropType7,document.Form1.txtQty7,document.Form1.txtTypesch7,document.Form1.txtQtysch7,document.Form1.lblInvoiceDate,document.Form1.txtstk7,document.Form1.txtsch7,document.Form1.txtfoe7,document.Form1.tmpSchType7,document.Form1.txtTempSecSP7,document.Form1.tmpSecSPType7,document.Form1.tmpFoeType7)
		if(tempint==8)
			getscheme2_PI(document.Form1.DropType8,document.Form1.txtQty8,document.Form1.txtTypesch8,document.Form1.txtQtysch8,document.Form1.lblInvoiceDate,document.Form1.txtstk8,document.Form1.txtsch8,document.Form1.txtfoe8,document.Form1.tmpSchType8,document.Form1.txtTempSecSP8,document.Form1.tmpSecSPType8,document.Form1.tmpFoeType8)
		if(tempint==9)
			getscheme2_PI(document.Form1.DropType9,document.Form1.txtQty9,document.Form1.txtTypesch9,document.Form1.txtQtysch9,document.Form1.lblInvoiceDate,document.Form1.txtstk9,document.Form1.txtsch9,document.Form1.txtfoe9,document.Form1.tmpSchType9,document.Form1.txtTempSecSP9,document.Form1.tmpSecSPType9,document.Form1.tmpFoeType9)
		if(tempint==10)
			getscheme2_PI(document.Form1.DropType10,document.Form1.txtQty10,document.Form1.txtTypesch10,document.Form1.txtQtysch10,document.Form1.lblInvoiceDate,document.Form1.txtstk10,document.Form1.txtsch10,document.Form1.txtfoe10,document.Form1.tmpSchType10,document.Form1.txtTempSecSP10,document.Form1.tmpSecSPType10,document.Form1.tmpFoeType10)
		if(tempint==11)
			getscheme2_PI(document.Form1.DropType11,document.Form1.txtQty11,document.Form1.txtTypesch11,document.Form1.txtQtysch11,document.Form1.lblInvoiceDate,document.Form1.txtstk11,document.Form1.txtsch11,document.Form1.txtfoe11,document.Form1.tmpSchType11,document.Form1.txtTempSecSP11,document.Form1.tmpSecSPType11,document.Form1.tmpFoeType11)
		if(tempint==12)
			getscheme2_PI(document.Form1.DropType12,document.Form1.txtQty12,document.Form1.txtTypesch12,document.Form1.txtQtysch12,document.Form1.lblInvoiceDate,document.Form1.txtstk12,document.Form1.txtsch12,document.Form1.txtfoe12,document.Form1.tmpSchType12,document.Form1.txtTempSecSP12,document.Form1.tmpSecSPType12,document.Form1.tmpFoeType12)
	
		ProdType.value=""
		ProdType.value=ProdType2[0]+":"+ProdType2[1]+":"+ProdType2[2]												 //add by vikas sharma 09.06.09
	
		changescheme()
		GetGrandTotal()
		GetNetAmount()
		
	}
	
	function GetGrandTotal()
	{
		var GTotal=0
		
		<%
		for(int k=1; k<=12; k++) 
		{
		%>
		if(document.Form1.txtAmount<%=k%>.value!="")
	 		GTotal=GTotal+eval(document.Form1.txtAmount<%=k%>.value)
	 	<%
	 	}
	 	%>
		
		document.Form1.txtGrandTotal.value=GTotal ;
		makeRound(document.Form1.txtGrandTotal);
	}
		
	function TotalLiter()
	{
		changeschemefoe()
		document.Form1.txtliter.value=qtyfoe
	}
	var totaldisc=0
	function GetCashDiscount()
	{
		changescheme()
		changeschemefoe()
		changeschemeSecondrySP()
		var Scheme=document.Form1.txtschemetotal.value
		if(Scheme=="" || isNaN(Scheme))
			Scheme=0
		var Disc=document.Form1.txtDisc.value
		if(Disc=="" || isNaN(Disc))
			Disc=0

		if(document.Form1.DropDiscType.value=="Per")
		    Disc=(document.Form1.txtGrandTotal.value-eval(Scheme))*Disc/100 
		else
		    Disc=qtyfoe*Disc
		document.Form1.tempdiscount.value=eval(Disc)
		makeRound(document.Form1.tempdiscount)
		document.Form1.txtDiscount.value = document.Form1.tempdiscount.value;
			  
		TotalLiter()
	   
		var foe=qt
		document.Form1.txtfleetoediscountRs.value=foe
		makeRound(document.Form1.txtfleetoediscountRs)
	  
		var SP=qtSP
		document.Form1.txtSecondrySpDisc.value=SP            //comment by Mahesh on 19.11.008
		makeRound(document.Form1.txtSecondrySpDisc)          //comment by Mahesh on 19.11.008
	  
		//******************
		var SchSP=document.Form1.txtSecondrySpDisc.value
		//alert("scheme total"+document.Form1.txtschemetotal.value)
		if(SchSP=="" || isNaN(SchSP))
			SchSP=0
		//*******************  
	  
		var CashDisc=document.Form1.txtCashDisc.value
		if(CashDisc=="" || isNaN(CashDisc))
			CashDisc=0
		if(document.Form1.DropCashDiscType.value=="Per")
		{    
			//var CashDiscount=document.Form1.txtGrandTotal.value-eval(Disc)-eval(Scheme)-eval(foe)
			var CashDiscount=document.Form1.txtGrandTotal.value-eval(Disc)-eval(Scheme)-eval(foe)-eval(SchSP)
			CashDisc=eval(CashDiscount)*CashDisc/100 
			document.Form1.tempcashdis.value=eval(CashDisc)
			makeRound(document.Form1.tempcashdis)
		}
		else
		{
		    document.Form1.txtCashDiscount.value=qtyfoe*CashDisc
		    CashDisc=document.Form1.txtCashDiscount.value
		    document.Form1.tempcashdis.value=CashDisc
		    makeRound(document.Form1.txtCashDisc)
		}
		document.Form1.txtCashDiscount.value = eval(CashDisc);
		makeRound(document.Form1.txtCashDiscount)
		document.Form1.txtVatValue.value = "";
		//document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) - eval(CashDisc) - eval(Disc)-eval(Scheme)-eval(foe);
		document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) - eval(CashDisc) - eval(Disc)-eval(Scheme)-eval(foe)-eval(SchSP);
		totaldisc=eval(CashDisc) + eval(Disc)+eval(Scheme)+eval(foe)+eval(SchSP)
	}
	
	function GetSGSTAmount()
	{
	    GetCashDiscount()
	    if(document.Form1.Noo.checked)
	    {
	        document.Form1.Textsgst.value="";
	    }
	    else
	    {
	        var sgst_rate=document.Form1.Tempsgstrate.value
	        
	        if(sgst_rate == "")
	            sgst_rate=0;
	        var sgst= totalValue;
	        if(sgst == "" || isNaN(sgst))
	            sgst = 0;
	        var sgst_amount = sgst * sgst_rate/100
	
	        makeRound(document.Form1.Textsgst)
	     
	        document.Form1.txtVatValue.value = Math.round(eval(sgst) +Math.round(eval(sgst_amount),0),0);

            
	        if(document.Form1.Textsgst.value=="")
	        {
	            document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)+Math.round((sgst_amount),0),0);
	        }
	        document.Form1.Textsgst.value =  Math.round(sgst_amount,0);
	    }
	    return sgst_amount;
	}
	var persistedCgstNetAmount=0;
	var persistedIgstNetAmount=0;
	var persistedSgstNetAmount=0;
	var totalAmountAfterGst=0;
	function Getsgstamt()
	{
	    // ;
	    var sgst_value=0;
	    var sgst = 0;
	    if(document.Form1.Noo.checked)
	    {
	        GetCashDiscount()
	        sgst_value=document.Form1.txtVatValue.value;
	        if(document.Form1.Textsgst.value!="")
	            document.Form1.txtNetAmount.value =Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.Textsgst.value),0);
	        document.Form1.Textsgst.value="";
	    }
	    else
	    {
	        sgst = GetSGSTAmount()
	        sgst_value=document.Form1.txtVatValue.value;
	    }
	    if(sgst_value=="" || isNaN(sgst_value))
	        sgst_value=0
	    // document.Form1.txtNetAmount.value=eval(sgst_value);
	    var netamount=Math.round(document.Form1.txtNetAmount.value,0);
	    netamount=netamount+".00";
	    if (document.Form1.txtNetAmount.value!='')
	        persistedSgstNetAmount=  document.Form1.txtNetAmount.value;
	    if(document.Form1.Textsgst.value!='')
	        totalAmountAfterGst=totalAmountAfterGst+Math.round(eval(document.Form1.Textsgst.value),0);
	    //   document.Form1.txtNetAmount.value=persistedSgstNetAmount+netamount;
	    return sgst;
	}

	function GetCGSTAmount()
	{
	    GetCashDiscount()
	    
	    if(document.Form1.N.checked)
	    {
	        document.Form1.Textcgst.value = "";
	    } 
	    else
	    {
	        var cgst_rate = document.Form1.Tempcgstrate.value
	        
	        if(cgst_rate == "")
	            cgst_rate = 0;
	        var cgst = totalValue
	        if(cgst == "" || isNaN(cgst))
	            cgst = 0;
	        var cgst_amount = cgst * cgst_rate/100
	      
	        makeRound(document.Form1.Textcgst)
	     
	        document.Form1.txtVatValue.value = eval(cgst) + eval(cgst_amount)

	        if(document.Form1.Textcgst.value=="")
	        {
	            document.Form1.txtNetAmount.value =Math.round(eval(document.Form1.txtNetAmount.value)+eval(cgst_amount),0);
	        }
	        document.Form1.Textcgst.value =Math.round(cgst_amount,0);
	    }
	    return cgst_amount
	}

	function Getcgstamt()
	{
	    //;
	    var cgst_value=0;
	    var cgst = 0;
	    if(document.Form1.N.checked)
	    {
	        GetCashDiscount()
	        cgst_value=document.Form1.txtVatValue.value;
	        if(document.Form1.Textcgst.value!="")
	            document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.Textcgst.value),0);
	        document.Form1.Textcgst.value="";
	    }
	    else
	    {
	        cgst =  GetCGSTAmount()
	        cgst_value=document.Form1.txtVatValue.value;
	    }
	    if(cgst_value=="" || isNaN(cgst_value))
	        cgst_value=0
	    //   document.Form1.txtNetAmount.value=eval(cgst_value);
	    var netamount=Math.round(document.Form1.txtNetAmount.value,0);
	    netamount=netamount+".00";
	    if (document.Form1.txtNetAmount.value!='')
	        persistedCgstNetAmount= document.Form1.txtNetAmount.value;
	    if(document.Form1.Textcgst.value!='')
	        totalAmountAfterGst=totalAmountAfterGst+eval(document.Form1.Textcgst.value);
	    //  document.Form1.txtNetAmount.value=persistedCgstNetAmount+netamount;
	    return cgst;
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
	       
	        if(vat_rate == "")
	            vat_rate = 0;
	        var vat = totalValue   
	        if(vat == "" || isNaN(vat))
	            vat = 0;
	        var vat_amount = vat * vat_rate/100
	
	        makeRound(document.Form1.txtVAT)
	     
	        document.Form1.txtVatValue.value = eval(vat) + eval(vat_amount);
	        if(document.Form1.txtVAT.value=="")
	        {
	            document.Form1.txtNetAmount.value =Math.round(eval(document.Form1.txtNetAmount.value)+Math.round(eval(vat_amount),0),0);
	        }
	        document.Form1.txtVAT.value = Math.round(vat_amount,0);
	    }
	    return vat_amount
	}
	//Calculate IGST
	function Getigstamt()
	{
	  
	    var vat_value = 0;
	    var vat = 0;
	    if(document.Form1.No.checked)
	    {
	        GetCashDiscount()
	        vat_value = document.Form1.txtVatValue.value;
		
	        if(document.Form1.txtVAT.value!="")
	            document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.txtVAT.value),0);
	        document.Form1.txtVAT.value = "";   
	    }
	    else		{
		   
	        vat= GetVatAmount()
	        vat_value = document.Form1.txtVatValue.value;
		
	    }
	    if(vat_value=="" || isNaN(vat_value))
	        vat_value=0
	    //	document.Form1.txtNetAmount.value=eval(vat_value);
	    var netamount=Math.round(eval(vat_value),0);
	    netamount=netamount+".00";
	    if (document.Form1.txtNetAmount.value!='')
	        persistedIgstNetAmount = document.Form1.txtNetAmount.value;
	    if(document.Form1.txtVAT.value!='')
	        totalAmountAfterGst+=Math.round(eval(document.Form1.txtVAT.value),0);
	    //document.Form1.txtNetAmount.value=eval(persistedIgstNetAmount)+eval(Math.round(eval(vat_value),0));
	    return vat;

	}
	
	function GetNetAmount()
	{
        var dbValues =  document.Form1.txtMainIGST.value;	    	    
	    var mainarr = new Array()
	    var taxarr = new Array()
	    var selarr = new Array()
	    var totol= 0
	    var qtyfoe=0
	    var qt=0
	    var SchSP=0
	    var f1=document.Form1
	    document.Form1.txtVatRate.value=""
	    document.Form1.Tempcgstrate.value=""
	    document.Form1.Tempsgstrate.value=""
	    var cgstamount1=0,cgstamount2 = 0,cgstamount3=0,cgstamount4=0,cgstamount5=0,cgstamount6=0,cgstamount7=0,cgstamount8=0,cgstamount9=0,cgstamount10=0,cgstamount11=0,cgstamount12 = 0,
            cgstamount13=0,cgstamount14 = 0,cgstamount15=0,cgstamount16=0,cgstamount17=0,cgstamount18=0,cgstamount19=0,cgstamount20=0
	    var sgstamount1=0,sgstamount2 = 0,sgstamount3=0,sgstamount4=0,sgstamount5=0,sgstamount6=0,sgstamount7=0,sgstamount8=0,sgstamount9=0,sgstamount10=0,sgstamount11=0,sgstamount12 = 0,
            sgstamount13=0,sgstamount14 = 0,sgstamount15=0,sgstamount16=0,sgstamount17=0,sgstamount18=0,sgstamount19=0,sgstamount20=0
	    var igstamount1=0,igstamount2 = 0,igstamount3=0,igstamount4=0,igstamount5=0,igstamount6=0,igstamount7=0,igstamount8=0,igstamount9=0,igstamount10=0,igstamount11=0,igstamount12 = 0,
            igstamount13=0,igstamount14 = 0,igstamount15=0,igstamount16=0,igstamount17=0,igstamount18=0,igstamount19=0,igstamount20=0

        <% for (int i = 1; i <= 12; i++)
        {
				%>
	    if(document.Form1.DropType<%=i%>.value !="Type")
	    {
	        var selectedProduct = document.Form1.DropType<%=i%>.value
	        f1.tmpAmount<%=i%>.value=document.Form1.txtAmount<%=i%>.value
	        selarr=selectedProduct.split(":");
	        mainarr =dbValues.split("~");
	        var selproduct=selectedProduct.split(":");
	        var ltrs=selproduct[2].split("X")
	        var calcLtrs=ltrs[0]*ltrs[1]
	        if(f1.DropType<%=i%>.value.indexOf(":")>0)
	            arrType = f1.DropType<%=i%>.value.split(":")
	        else
	        {
	            arrType[0]=""
	            arrType[1]=""
	            arrType[2]=""				
	        }
			if(arrType[2] != "")
			{
				var mainarr1 = new Array()
				var hidarr1  = arrType[2]
										
				if(arrType[2] == "Loose Oil" || arrType[2] == "Loose oil")
				{
				
					totol=f1.txtQty<%=i%>.value*f1.txtsch<%=i%>.value
					total_fleetoe=f1.txtQty<%=i%>.value*f1.txtfoe<%=i%>.value
					totolSP=f1.txtQty<%=i%>.value*f1.txtTempschSP<%=i%>.value
				}
				else
				{
				
					mainarr1 = hidarr1.split("X")
					if(document.Form1.tmpSchType<%=i%>.value=="%")
					{
						totol=(document.Form1.txtAmount<%=i%>.value*f1.txtsch<%=i%>.value)/100
						
					}
					else 
					{
						totol=mainarr1[0]* mainarr1[1]*f1.txtQty<%=i%>.value*f1.txtsch<%=i%>.value
					}
				}
			}
	        if(arrType[2] != "")
	        {
	            var mainarr1 = new Array()
	            var hidarr1  = arrType[2]
	            if(arrType[2] == "Loose Oil" || arrType[2] == "Loose oil")
	            {
	                qtyfoe=f1.txtQty<%=i%>.value
	                qt=f1.txtQty<%=i%>.value*f1.txtfoe<%=i%>.value 
	            }
	            else
	            {
	                mainarr1 = hidarr1.split("X")				
	                qtyfoe=mainarr1[0]* mainarr1[1]*f1.txtQty<%=i%>.value
	                if(document.Form1.tmpFoeType<%=i%>.value=="Rs.")
	                {
	                    qt=mainarr1[0]* mainarr1[1]*f1.txtQty<%=i%>.value*f1.txtfoe<%=i%>.value
	                }
	                else
	                {
	                    qt=(document.Form1.txtAmount<%=i%>.value*f1.txtfoe<%=i%>.value)/100
	                }
	            }
	        }
	        var foe=qt
	        document.Form1.temfoe<%=i%>.value=f1.txtfoe<%=i%>.value
            if(arrType[2] != "")
			{
				var mainarr1 = new Array()
				var hidarr1  = arrType[2]
			
				if(arrType[2] == "Loose Oil" || arrType[2] == "Loose oil")
				{
					qtSP+=f1.txtQty<%=i%>.value*f1.txtTempSecSP<%=i%>.value   //comment by Mahesh on 19.11.008
				}
				else
				{
					mainarr1 = hidarr1.split("X")				
					if(document.Form1.tmpSecSPType<%=i%>.value=="Rs")
					{
						qtSP+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=i%>.value*f1.txtTempSecSP<%=i%>.value
					}
					else if(document.Form1.tmpSecSPType<%=i%>.value=="Unit")         //Add by vikas 23.11. 2012
					{
						qtSP+=mainarr1[0]*f1.txtQty<%=i%>.value*f1.txtTempSecSP<%=i%>.value      //Add by vikas 23.11. 2012
					}
					else
					{
						qtSP+=(document.Form1.txtAmount<%=i%>.value*f1.txtTempSecSP<%=i%>.value)/100
					}
				}
            }
	        SchSP =qtSP
            f1.tmpSch<%=i%>.value= f1.txtsch<%=i%>.value
	        var Disc=document.Form1.txtDisc.value
	        if(Disc=="" || isNaN(Disc))
	            Disc=0
	
	        if(document.Form1.DropDiscType.value=="Per")
	            Disc=(document.Form1.txtAmount<%=i%>.value-eval(totol))*Disc/100 
	        else
	            Disc=(calcLtrs*f1.txtQty<%=i%>.value)*Disc
	        document.Form1.tempdiscount.value=eval(Disc)
	        makeRound(document.Form1.tempdiscount)
	        
	        var CashDisc=document.Form1.txtCashDisc.value
	        if(CashDisc=="" || isNaN(CashDisc))
	            CashDisc=0
	        if(document.Form1.DropCashDiscType.value=="Per")
	        {    
	            var CashDiscount=document.Form1.txtAmount<%=i%>.value-eval(Disc)-eval(totol)-eval(foe)-eval(SchSP)
	            CashDisc=eval(CashDiscount)*CashDisc/100 
	            //********
	            document.Form1.tempcashdis.value=eval(CashDisc)
	            makeRound(document.Form1.tempcashdis)
	            document.Form1.txtCashDiscount.value = eval(CashDisc);
	            makeRound(document.Form1.txtCashDiscount)
	            //********
	        }
		
	        else
	        {
	            document.Form1.txtCashDiscount.value=(qtyfoe)*CashDisc
	            CashDisc=document.Form1.txtCashDiscount.value
	            document.Form1.tempcashdis.value=CashDisc
	            makeRound(document.Form1.txtCashDisc)
	        }

	        for(i=0;i<mainarr.length;i++)
	        {
	            taxarr = mainarr[i].split("|");	  
	            if(taxarr[0]==selarr[0])
	            {
	                document.Form1.txtVatRate.value=taxarr[3];
	                document.Form1.Tempcgstrate.value=taxarr[4];
	                document.Form1.Tempsgstrate.value=taxarr[5];
	                totalValue = eval(document.Form1.txtAmount<%=i%>.value) - eval(CashDisc) - eval(Disc)-eval(totol)-eval(foe)-eval(SchSP);	

	                totalAmountAfterGst=0;
	                var igstamount<%=i%> = Getigstamt()
	                document.Form1.tempIgst<%=i%>.value=igstamount<%=i%>
	                var sgstamount<%=i%> = Getsgstamt()
	                document.Form1.tempSgst<%=i%>.value=sgstamount<%=i%>
	                var cgstamount<%=i%> = Getcgstamt()
	                document.Form1.tempCgst<%=i%>.value=cgstamount<%=i%>
	                document.Form1.tempHsn<%=i%>.value=taxarr[6];
	            }
	        }
	    }
	    <%
        }
			%>

	    document.Form1.txtVAT.value=Math.round(igstamount1)+Math.round(igstamount2)+Math.round(igstamount3)+Math.round(igstamount4)+Math.round(igstamount5)+Math.round(igstamount6)+Math.round(igstamount7)+Math.round(igstamount8)
            +Math.round(igstamount9)+Math.round(igstamount10)+Math.round(igstamount11)+Math.round(igstamount12)+Math.round(igstamount13)+Math.round(igstamount14)+Math.round(igstamount15)+Math.round(igstamount16)+Math.round(igstamount17)+Math.round(igstamount18)+Math.round(igstamount19)+Math.round(igstamount20)
	    document.Form1.tempTotalIgst.value=document.Form1.txtVAT.value     

	    document.Form1.Textcgst.value = Math.round(cgstamount1)+Math.round(cgstamount2)+Math.round(cgstamount3)+Math.round(cgstamount4)+Math.round(cgstamount5)+Math.round(cgstamount6)+Math.round(cgstamount7)+Math.round(cgstamount8)
        +Math.round(cgstamount9)+Math.round(cgstamount10)+Math.round(cgstamount11)+Math.round(cgstamount12)+Math.round(cgstamount13)+Math.round(cgstamount14)+Math.round(cgstamount15)+Math.round(cgstamount16)+Math.round(cgstamount17)+Math.round(cgstamount18)+Math.round(cgstamount19)+Math.round(cgstamount20)
	    document.Form1.tempTotalCgst.value=document.Form1.Textcgst.value
        
	    document.Form1.Textsgst.value = Math.round(sgstamount1)+Math.round(sgstamount2)+Math.round(sgstamount3)+Math.round(sgstamount4)+Math.round(sgstamount5)+Math.round(sgstamount6)+Math.round(sgstamount7)+Math.round(sgstamount8)
        +Math.round(sgstamount9)+Math.round(sgstamount10)+Math.round(sgstamount11)+Math.round(sgstamount12)+Math.round(sgstamount13)+Math.round(sgstamount14)+Math.round(sgstamount15)+Math.round(sgstamount16)+Math.round(sgstamount17)+Math.round(sgstamount18)+Math.round(sgstamount19)+Math.round(sgstamount20)
	    document.Form1.tempTotalSgst.value=document.Form1.Textsgst.value

	    if(document.Form1.txtGrandTotal.value==""|| isNaN(document.Form1.txtGrandTotal.value))
	        document.Form1.txtGrandTotal.value=0;
	    f1.tmpGrandTotal.value=document.Form1.txtGrandTotal.value;
	    //var Scheme = document.Form1.txtschemetotal.value;
	    totalAmountAfterGst=Math.round(document.Form1.txtVAT.value)+Math.round(document.Form1.Textcgst.value)+Math.round(document.Form1.Textsgst.value)
	    //var totaldisc = eval(CashDisc) + eval(Disc) + eval(Scheme) + eval(foe) + eval(SchSP)
	    document.Form1.txtNetAmount.value=eval(document.Form1.txtGrandTotal.value)+eval(totalAmountAfterGst)-eval(totaldisc)
	    document.Form1.txtNetAmount.value=Math.round(document.Form1.txtNetAmount.value,0)
	    f1.tmpNetAmount.value=Math.round(document.Form1.txtNetAmount.value,0)
	}
	
	function changescheme()
	{
		var totol=0,total_fleetoe=0,totolSP=0
		var f1=document.Form1
		var arrType= new Array();
		
		<% for(int k=1; k<=12; k++) {%>
			if(f1.DropType<%=k%>.value.indexOf(":")>0)
				arrType = f1.DropType<%=k%>.value.split(":")
			else
			{
				arrType[0]=""
				arrType[1]=""
				arrType[2]=""				// add by vikas 09.06.09
			}
		/*
			if(arrType[1] != "")
			{
				var mainarr1 = new Array()
				var hidarr1  = arrType[1]
				if(arrType[1] == "Loose Oil" || arrType[1] == "Loose oil")
				{
					totol+=f1.txtQty<%=k%>.value*f1.txtsch<%=k%>.value
					total_fleetoe+=f1.txtQty<%=k%>.value*f1.txtfoe<%=k%>.value
					totolSP+=f1.txtQty<%=k%>.value*f1.txtTempschSP<%=k%>.value
				}
				else
				{
					mainarr1 = hidarr1.split("X")
					if(document.Form1.tmpSchType<%=k%>.value=="%")
						totol+=(document.Form1.txtAmount<%=k%>.value*f1.txtsch<%=k%>.value)/100
					else
						totol+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value*f1.txtsch<%=k%>.value
				}
			}*/
			
			/****************change in following code date on 22.04.09 by vikas***********************/
			if(arrType[2] != "")
			{
				var mainarr1 = new Array()
				var hidarr1  = arrType[2]
				
						
				if(arrType[2] == "Loose Oil" || arrType[2] == "Loose oil")
				{
					totol+=f1.txtQty<%=k%>.value*f1.txtsch<%=k%>.value
					total_fleetoe+=f1.txtQty<%=k%>.value*f1.txtfoe<%=k%>.value
					totolSP+=f1.txtQty<%=k%>.value*f1.txtTempschSP<%=k%>.value
				}
				else
				{
					mainarr1 = hidarr1.split("X")
					if(document.Form1.tmpSchType<%=k%>.value=="%")
					{
						totol+=(document.Form1.txtAmount<%=k%>.value*f1.txtsch<%=k%>.value)/100
					}
					else
					{
						totol+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value*f1.txtsch<%=k%>.value
					}
				}
			}
		<%
		}
		%>
		f1.txtschemetotal.value=eval(totol)
		f1.txtSecondrySpDisc.value=eval(totolSP)
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
				arrType[2]=""
			}
		
			/*if(arrType[1] != "")
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
			}*/
			/************************** add by vikas sharma ***********************************/
				if(arrType[2] != "")
				{
					var mainarr1 = new Array()
					var hidarr1  = arrType[2]
					if(arrType[2] == "Loose Oil" || arrType[2] == "Loose oil")
					{
						qtyfoe+=f1.txtQty<%=k%>.value
						qt+=f1.txtQty<%=k%>.value*f1.txtfoe<%=k%>.value 
					}
					else
					{
						mainarr1 = hidarr1.split("X")				
						qtyfoe+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value
						if(document.Form1.tmpFoeType<%=k%>.value=="Rs.")
						{
							qt+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value*f1.txtfoe<%=k%>.value
						}
						else
						{
							qt+=(document.Form1.txtAmount<%=k%>.value*f1.txtfoe<%=k%>.value)/100
						}
					}
				}
			
		<%}%>
	}
	
	var qtSP=0
	function changeschemeSecondrySP()
	{
		qtSP=0
		var f1=document.Form1
		var arrType = new Array();
		<%for(int k=1; k<=12; k++) {%>
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
				//qtyfoe+=f1.txtQty<%=k%>.value
				qtSP+=f1.txtQty<%=k%>.value*f1.txtTempSecSP<%=k%>.value //comment by Mahesh on 19.11.008
			}
			else
			{
				mainarr1 = hidarr1.split("X")				
				if(document.Form1.tmpSecSPType<%=k%>.value=="Rs")
				{
					qtSP+=mainarr1[0]* mainarr1[1]*f1.txtQty<%=k%>.value*f1.txtTempSecSP<%=k%>.value
				}
				else if(document.Form1.tmpSecSPType<%=k%>.value=="Nos")
				{
					qtSP+=f1.txtQty<%=k%>.value*f1.txtTempSecSP<%=k%>.value
				}
				else
				{
					qtSP+=(document.Form1.txtAmount<%=k%>.value*f1.txtTempSecSP<%=k%>.value)/100
				}
			}
		}
		<%}%>
	}
	
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
		//a-z, A-Z, 0-9
		if((key>=97 && key<=122) || (key>=65 && key<=90) || (key>=48 && key<=57))
		{
			key = String.fromCharCode(key);
			var now = new Date();
			var diff = (now.getTime() - timeStamp.getTime());
			timeStamp = new Date();
			//1 seconds = 1000 milliseconds
			if (diff > 1200)
				keys = key;
			else
				keys = keys + key;
			var cnt;
			//for (cnt=0;cnt<document.all("DropDownList1").children.length;cnt++)
			for (cnt=0;cnt<document.all(t.name).children.length;cnt++)
			{
				//var itm = document.all("DropDownList1").children[cnt].text;
				var itm = document.all(t.name).children[cnt].text.toLowerCase();
				if (itm.substring(0,keys.length)==keys)
				{
					//document.all("DropDownList1").selectedIndex = cnt;
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

	/*function getBalanceontext(t,City,CR_Days,Balance,CR_Limit,crlimit,vehicleno,SSR)
	{
		var mainarr = new Array()
		var vehiclearr = new Array(); 
		
		//var typetext  = t.value //Comment by vikas sharma 16.05.09
	//**********Start***Add by vikas sharma 16.05.09******************
	var typetext1  = t.value
	var typetext2=typetext1.split(":")
	typetext=typetext2[0]
	//***********End********************
		
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
	}*/

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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Sales.js"></script>
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop_PI.js"></script>
		
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:textbox id="lblInvoiceDate" runat="server" Width="120" ReadOnly="True" BorderStyle="Groove"
				CssClass="dropdownlist"></asp:textbox>
			<INPUT id="tmpQty4" style="Z-INDEX: 121; LEFT: 390px; WIDTH: 0px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
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
				type="hidden" name="totalltr" runat="server"><INPUT id="tmpFoeType1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType1" runat="server"> <INPUT id="tmpFoeType2" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType2" runat="server"> <INPUT id="tmpFoeType3" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType3" runat="server"> <INPUT id="tmpFoeType4" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType4" runat="server"> <INPUT id="tmpFoeType5" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType5" runat="server"> <INPUT id="tmpFoeType6" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType6" runat="server"> <INPUT id="tmpFoeType7" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType7" runat="server"> <INPUT id="tmpFoeType8" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType8" runat="server"> <INPUT id="tmpFoeType9" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType9" runat="server"> <INPUT id="tmpFoeType10" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType10" runat="server"> <INPUT id="tmpFoeType11" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType11" runat="server"> <INPUT id="tmpFoeType12" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpFoeType12" runat="server"><INPUT id="tmpSecSPType1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType1" runat="server"><INPUT id="tmpSecSPType2" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType2" runat="server"><INPUT id="tmpSecSPType3" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType3" runat="server"><INPUT id="tmpSecSPType4" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType4" runat="server"><INPUT id="tmpSecSPType5" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType5" runat="server"><INPUT id="tmpSecSPType6" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType6" runat="server"><INPUT id="tmpSecSPType7" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType7" runat="server"><INPUT id="tmpSecSPType8" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType8" runat="server"><INPUT id="tmpSecSPType9" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType9" runat="server"><INPUT id="tmpSecSPType10" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType10" runat="server"><INPUT id="tmpSecSPType11" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType11" runat="server"><INPUT id="tmpSecSPType12" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpSecSPType12" runat="server"><INPUT id="txtTempSecSP1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP1" runat="server"><INPUT id="txtTempSecSP2" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP2" runat="server"><INPUT id="txtTempSecSP3" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP3" runat="server"><INPUT id="txtTempSecSP4" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP4" runat="server"><INPUT id="txtTempSecSP5" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP5" runat="server"><INPUT id="txtTempSecSP6" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP6" runat="server"><INPUT id="txtTempSecSP7" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP7" runat="server"><INPUT id="txtTempSecSP8" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP8" runat="server"><INPUT id="txtTempSecSP9" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP9" runat="server"><INPUT id="txtTempSecSP10" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP10" runat="server"><INPUT id="txtTempSecSP11" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP11" runat="server"><INPUT id="txtTempSecSP12" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="txtTempSecSP12" runat="server"><INPUT id="temptextSecSP" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="2" name="temptextSecSP" runat="server"><INPUT id="tempCustGroup" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tempCustGroup" runat="server"><INPUT id="temcombo1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo1" runat="server"><INPUT id="temcombo2" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo2" runat="server"><INPUT id="temcombo3" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo3" runat="server"><INPUT id="temcombo4" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo4" runat="server"><INPUT id="temcombo5" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo5" runat="server"><INPUT id="temcombo6" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo6" runat="server"><INPUT id="temcombo7" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo7" runat="server"><INPUT id="temcombo8" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo8" runat="server"><INPUT id="temcombo9" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo9" runat="server"><INPUT id="temcombo10" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo10" runat="server"><INPUT id="temcombo11" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo11" runat="server"><INPUT id="temcombo12" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="temcombo12" runat="server">
            <INPUT id="txtMainIGST" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px" type="hidden" name="txtMainIGST" runat="server"/>
			<INPUT id="Tempcgstrate" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="Tempcgstrate" runat="server"/>
            <INPUT id="Tempsgstrate" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="Tempsgstrate" runat="server"/>
            <INPUT id="temfoe1" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe2" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe3" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe4" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe5" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe6" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe7" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe8" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe9" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe10" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe11" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="temfoe12" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
    type="hidden" size="1" name="tempHsn20" runat="server"/>
            <INPUT id="tempTotalCgst" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempTotalCgst" runat="server"/>
            <INPUT id="tempTotalSgst" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempTotalSgst" runat="server"/>
            <INPUT id="tempTotalIgst" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempTotalIgst" runat="server"/>
            <INPUT id="tempCgst1" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst1" runat="server"/> <INPUT id="tempCgst2" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst2" runat="server"/> <INPUT id="tempCgst3" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst3" runat="server"/> <INPUT id="tempCgst4" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst4" runat="server"/> <INPUT id="tempCgst5" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst5" runat="server"/> <INPUT id="tempCgst6" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst6" runat="server"/> <INPUT id="tempCgst7" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst7" runat="server"/> <INPUT id="tempCgst8" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst8" runat="server"/> <INPUT id="tempCgst9" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst9" runat="server"/> <INPUT id="tempCgst10" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst10" runat="server"/> <INPUT id="tempCgst11" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst11" runat="server"/> <INPUT id="tempCgst12" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst12" runat="server"/> <INPUT id="tempCgst13" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst13" runat="server"/> <INPUT id="tempCgst14" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst14" runat="server"/> <INPUT id="tempCgst15" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst15" runat="server"/> <INPUT id="tempCgst16" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst16" runat="server"/> <INPUT id="tempCgst17" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst17" runat="server"/> <INPUT id="tempCgst18" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst18" runat="server"/> <INPUT id="tempCgst19" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst19" runat="server"/> <INPUT id="tempCgst20" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempCgst20" runat="server"/>
                        <INPUT id="tempSgst1" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst1" runat="server"/> <INPUT id="tempSgst2" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst2" runat="server"/> <INPUT id="tempSgst3" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst3" runat="server"/> <INPUT id="tempSgst4" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst4" runat="server"/> <INPUT id="tempSgst5" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst5" runat="server"/> <INPUT id="tempSgst6" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst6" runat="server"/> <INPUT id="tempSgst7" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst7" runat="server"/> <INPUT id="tempSgst8" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst8" runat="server"/> <INPUT id="tempSgst9" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst9" runat="server"/> <INPUT id="tempSgst10" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst10" runat="server"/> <INPUT id="tempSgst11" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst11" runat="server"/> <INPUT id="tempSgst12" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst12" runat="server"/> <INPUT id="tempSgst13" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst13" runat="server"/> <INPUT id="tempSgst14" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst14" runat="server"/> <INPUT id="tempSgst15" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst15" runat="server"/> <INPUT id="tempSgst16" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst16" runat="server"/> <INPUT id="tempSgst17" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst17" runat="server"/> <INPUT id="tempSgst18" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst18" runat="server"/> <INPUT id="tempSgst19" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst19" runat="server"/> <INPUT id="tempSgst20" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempSgst20" runat="server"/>
             <INPUT id="tempIgst1" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst1" runat="server"/> <INPUT id="tempIgst2" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst2" runat="server"/> <INPUT id="tempIgst3" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst3" runat="server"/> <INPUT id="tempIgst4" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst4" runat="server"/> <INPUT id="tempIgst5" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst5" runat="server"/> <INPUT id="tempIgst6" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst6" runat="server"/> <INPUT id="tempIgst7" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst7" runat="server"/> <INPUT id="tempIgst8" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst8" runat="server"/> <INPUT id="tempIgst9" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst9" runat="server"/> <INPUT id="tempIgst10" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst10" runat="server"/> <INPUT id="tempIgst11" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst11" runat="server"/> <INPUT id="tempIgst12" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst12" runat="server"/> <INPUT id="tempIgst13" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst13" runat="server"/> <INPUT id="tempIgst14" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst14" runat="server"/> <INPUT id="tempIgst15" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst15" runat="server"/> <INPUT id="tempIgst16" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst16" runat="server"/> <INPUT id="tempIgst17" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst17" runat="server"/> <INPUT id="tempIgst18" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst18" runat="server"/> <INPUT id="tempIgst19" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst19" runat="server"/> <INPUT id="tempIgst20" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempIgst20" runat="server"/>
             <INPUT id="tempHsn1" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn1" runat="server"/> <INPUT id="tempHsn2" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn2" runat="server"/> <INPUT id="tempHsn3" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn3" runat="server"/> <INPUT id="tempHsn4" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn4" runat="server"/> <INPUT id="tempHsn5" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn5" runat="server"/> <INPUT id="tempHsn6" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn6" runat="server"/> <INPUT id="tempHsn7" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn7" runat="server"/> <INPUT id="tempHsn8" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn8" runat="server"/> <INPUT id="tempHsn9" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn9" runat="server"/> <INPUT id="tempHsn10" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn10" runat="server"/> <INPUT id="tempHsn11" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn11" runat="server"/> <INPUT id="tempHsn12" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn12" runat="server"/> <INPUT id="tempHsn13" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn13" runat="server"/> <INPUT id="tempHsn14" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn14" runat="server"/> <INPUT id="tempHsn15" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn15" runat="server"/> <INPUT id="tempHsn16" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn16" runat="server"/> <INPUT id="tempHsn17" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn17" runat="server"/> <INPUT id="tempHsn18" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn18" runat="server"/> <INPUT id="tempHsn19" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn19" runat="server"/> <INPUT id="tempHsn20" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn20" runat="server"/>

            <INPUT id="tmpSch1" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tmpSch1" runat="server"/> <INPUT id="tmpSch2" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn2" runat="server"/> <INPUT id="tmpSch3" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn3" runat="server"/> <INPUT id="tmpSch4" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn4" runat="server"/> <INPUT id="tmpSch5" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn5" runat="server"/> <INPUT id="tmpSch6" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn6" runat="server"/> <INPUT id="tmpSch7" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn7" runat="server"/> <INPUT id="tmpSch8" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn8" runat="server"/> <INPUT id="tmpSch9" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn9" runat="server"/> <INPUT id="tmpSch10" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn10" runat="server"/> <INPUT id="tmpSch11" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn11" runat="server"/> <INPUT id="tmpSch12" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn12" runat="server"/> <INPUT id="tmpSch13" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn13" runat="server"/> <INPUT id="tmpSch14" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn14" runat="server"/> <INPUT id="tmpSch15" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn15" runat="server"/> <INPUT id="tmpSch16" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn16" runat="server"/> <INPUT id="tmpSch17" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn17" runat="server"/> <INPUT id="tmpSch18" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn18" runat="server"/> <INPUT id="tmpSch19" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn19" runat="server"/> <INPUT id="tmpSch20" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempHsn20" runat="server"/>

            <INPUT id="tmpAmount1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount1" runat="server"><INPUT id="tmpAmount2" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount2" runat="server"><INPUT id="tmpAmount3" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount3" runat="server"><INPUT id="tmpAmount4" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount4" runat="server"><INPUT id="tmpAmount5" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount5" runat="server"><INPUT id="tmpAmount6" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount6" runat="server"><INPUT id="tmpAmount7" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount7" runat="server"><INPUT id="tmpAmount8" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount8" runat="server"><INPUT id="tmpAmount9" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount9" runat="server"><INPUT id="tmpAmount10" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount10" runat="server"><INPUT id="tmpAmount11" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount11" runat="server"><INPUT id="tmpAmount12" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpAmount12" runat="server">
            <INPUT id="tmpGrandTotal" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpGrandTotal" runat="server">
            <INPUT id="tmpNetAmount" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 1px; POSITION: absolute; TOP: 2px; HEIGHT: 20px"
				type="hidden" name="tmpNetAmount" runat="server">
            
            <table height="278" width="900" align="center" cellpadding="0" cellspacing="0">
				<tr>
					<th align="center" colSpan="3">
						<font color="#ce4848">Performa Invoice</font>
						<hr>
						<asp:label id="lblMessage" runat="server" ForeColor="DarkGreen" Font-Size="8pt">Price updation not available for some products </asp:label></th></tr>
				<tr>
					<td align="center" width="40%">
						<TABLE border="1" cellpadding="0" cellspacing="1">
							<TBODY>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD vAlign="middle">Customer Name&nbsp;<asp:requiredfieldvalidator id="rvf1" runat="server" ErrorMessage="Please Select Customer Name" ControlToValidate="lblPlace"
														Operator="NotEqual">*</asp:requiredfieldvalidator></TD>
												<TD vAlign="top"><input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="text1" onkeyup="search3(this,document.Form1.DropCustName,document.Form1.texthidden.value),arrowkeydown(this,event,document.Form1.DropCustName,document.Form1.texthidden)"
														style="Z-INDEX: 10; VISIBILITY: visible; WIDTH: 231px; HEIGHT: 19px" onclick="search1(document.Form1.DropCustName,document.Form1.texthidden),dropshow(document.Form1.DropCustName)"
														value="Select" name="text1" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
														onclick="search1(document.Form1.DropCustName,document.Form1.texthidden),dropshow(document.Form1.DropCustName)" readOnly type="text"
														name="temp"> <input onkeypress="return GetAnyNumber(this, event);" id="texthidden" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px"
														type="text" name="texthidden" runat="server"><br>
													<div id="Layer1" style="Z-INDEX: 2; POSITION: absolute"><select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.text1,document.Form1.DropType1),getBalance_Cust(this,document.Form1.lblPlace)"
															id="DropCustName" ondblclick="select(this,document.Form1.text1),getBalance_Cust(this,document.Form1.lblPlace)" onkeyup="arrowkeyselect(this,event,document.Form1.DropType1,document.Form1.text1),getBalance_Cust(this,document.Form1.lblPlace)"
															style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 250px; HEIGHT: 0px" onfocusout="HideList(this,document.Form1.text1),getBalance_Cust(this,document.Form1.lblPlace)"
															multiple name="DropCustName"></select></div>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD vAlign="top" align="center">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD>Place :</TD>
												<TD><INPUT class="dropdownlist" id="lblPlace" style="WIDTH: 250px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"
														readOnly type="text" size="22" name="lblPlace" runat="server"></TD>
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
															onfocusout="HideList(this,document.Form1.DropType1),getStock1(document.Form1.DropType1,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1,1)"
															multiple name="DropProdName1"></select></div>
												</TD>
												<TD align="center"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty1" onblur="calc2(this,document.Form1.txtAvStock1,document.Form1.txtRate1,document.Form1.tmpQty1,1,document.Form1.DropType1)"
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
															onfocusout="HideList(this,document.Form1.DropType2),getStock1(document.Form1.DropType2,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2,2)"
															multiple name="DropProdName2"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty2" onblur="calc2(this,document.Form1.txtAvStock2,document.Form1.txtRate2,document.Form1.tmpQty2,2,document.Form1.DropType2)"
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
															onfocusout="HideList(this,document.Form1.DropType3),getStock1(document.Form1.DropType3,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3,3)"
															multiple name="DropProdName3"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty3" onblur="calc2(this,document.Form1.txtAvStock3,document.Form1.txtRate3,document.Form1.tmpQty3,3,document.Form1.DropType3)"
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
															onfocusout="HideList(this,document.Form1.DropType4),getStock1(document.Form1.DropType4,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4,4)"
															multiple name="DropProdName4"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty4" onblur="calc2(this,document.Form1.txtAvStock4,document.Form1.txtRate4,document.Form1.tmpQty4,4,document.Form1.DropType4)"
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
															onfocusout="HideList(this,document.Form1.DropType5),getStock1(document.Form1.DropType5,document.Form1.txtAvStock5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5,5)"
															multiple name="DropProdName5"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty5" onblur="calc2(this,document.Form1.txtAvStock5,document.Form1.txtRate5,document.Form1.tmpQty5,5,document.Form1.DropType5)"
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
															onfocusout="HideList(this,document.Form1.DropType6),getStock1(document.Form1.DropType6,document.Form1.txtAvStock6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6,6)"
															multiple name="DropProdName6"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty6" onblur="calc2(this,document.Form1.txtAvStock6,document.Form1.txtRate6,document.Form1.tmpQty6,6,document.Form1.DropType6)"
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
															onfocusout="HideList(this,document.Form1.DropType7),getStock1(document.Form1.DropType7,document.Form1.txtAvStock7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7,7)"
															multiple name="DropProdName7"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty7" onblur="calc2(this,document.Form1.txtAvStock7,document.Form1.txtRate7,document.Form1.tmpQty7,7,document.Form1.DropType7)"
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
															onfocusout="HideList(this,document.Form1.DropType8),getStock1(document.Form1.DropType8,document.Form1.txtAvStock8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8,8)"
															multiple name="DropProdName8"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty8" onblur="calc2(this,document.Form1.txtAvStock8,document.Form1.txtRate8,document.Form1.tmpQty8,8,document.Form1.DropType8)"
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
											<!------------------------------------------->
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
															onfocusout="HideList(this,document.Form1.DropType9),getStock1(document.Form1.DropType9,document.Form1.txtAvStock9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9,9)"
															multiple name="DropProdName9"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty9" onblur="calc2(this,document.Form1.txtAvStock9,document.Form1.txtRate9,document.Form1.tmpQty9,9,document.Form1.DropType9)"
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
															onfocusout="HideList(this,document.Form1.DropType10),getStock1(document.Form1.DropType10,document.Form1.txtAvStock10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10,10)"
															multiple name="DropProdName10"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty10" onblur="calc2(this,document.Form1.txtAvStock10,document.Form1.txtRate10,document.Form1.tmpQty10,10,document.Form1.DropType10)"
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
															onfocusout="HideList(this,document.Form1.DropType11),getStock1(document.Form1.DropType11,document.Form1.txtAvStock11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11,11)"
															multiple name="DropProdName11"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty11" onblur="calc2(this,document.Form1.txtAvStock11,document.Form1.txtRate11,document.Form1.tmpQty11,11,document.Form1.DropType11)"
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
															onfocusout="HideList(this,document.Form1.DropType12),getStock1(document.Form1.DropType12,document.Form1.txtAvStock12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12,12)"
															multiple name="DropProdName12"></select></div>
												</TD>
												<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty12" onblur="calc2(this,document.Form1.txtAvStock12,document.Form1.txtRate12,document.Form1.tmpQty12,12,document.Form1.DropType12)"
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
											<!------------------------------------------->
											<!---------txt scheme ----------------------->
											<TR>
												<td colSpan="7">
													<table cellSpacing="0" cellPadding="0">
														<tr>
															<TD colSpan="3"><asp:textbox id="txtTypesch1" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch1" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td width="51">&nbsp;</td>
															<td width="51"><asp:textbox id="txtstk1" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</tr>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch2" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch2" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td>&nbsp;</td>
															<td><asp:textbox id="txtstk2" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch3" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch3" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk3" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch4" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch4" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk4" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch5" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch5" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk5" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch6" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch6" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk6" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch7" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch7" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk7" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch8" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch8" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk8" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch9" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch9" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk9" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch10" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch10" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk10" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch11" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch11" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk11" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3"><asp:textbox id="txtTypesch12" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<TD><asp:textbox id="txtQtysch12" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
																	CssClass="dropdownlist"></asp:textbox></TD>
															<td></td>
															<td><asp:textbox id="txtstk12" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
																	Enabled="False"></asp:textbox></td>
															<TD align="right" colSpan="1">&nbsp;</TD>
														</TR>
													</table>
												</td>
												<td colSpan="2">
													<table cellPadding="2" cellspacing="0">
														<tr>
															<td><IMG height="250" src="../../HeaderFooter/images/Servo Foc.jpg" width="125"></td>
														</tr>
													</table>
												</td>
											</TR>
											<!---------end---------------------></TABLE>
									</TD>
								</TR>
							</TBODY>
						</TABLE>
						<TABLE cellPadding="0" border="0" cellspacing="0">
							<TR>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
								<TD>&nbsp;</TD>
							</TR>
							<TR>
								<TD>Promo Scheme</TD>
								<TD><asp:textbox id="txtPromoScheme" runat="server" Width="110px" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
								<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD>Grand Total</TD>
								<TD><asp:textbox id="txtGrandTotal" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Scheme&nbsp;Discount&nbsp;</TD>
								<TD><asp:textbox id="txtschemetotal" onblur="GetNetAmount()" runat="server" Width="110px" ReadOnly="True"
										BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Ltr.&nbsp;&nbsp;&nbsp;
                                    <asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtliter" runat="server"
										Width="60px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox></TD>
								<TD></TD>
								<TD>Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtDisc" onblur="GetNetAmount()"
										runat="server" Width="124px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropDiscType" runat="server" CssClass="dropdownlist" onchange="GetNetAmount()">
										<asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
										<asp:ListItem Value="Per">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtDiscount" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Fleet/OE Discount</TD>
								<TD><asp:textbox id="txtfleetoediscount" onblur="GetNetAmount()" runat="server" Visible="False" Width="110px"
										BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox><asp:dropdownlist id="dropfleetoediscount" onblur="GetNetAmount()" runat="server" Visible="False"
										Width="58px" CssClass="dropdownlist">
										<asp:ListItem Value="Rs.">Rs.</asp:ListItem>
										<asp:ListItem Value="%">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtfleetoediscountRs" onblur="GetNetAmount()" runat="server" Width="110px" ReadOnly="True"
										BorderStyle="Groove" CssClass="dropdownlist"></asp:textbox>
									&nbsp;
									Secondry Sp. Disc.&nbsp;&nbsp;<asp:textbox id="txtSecondrySpDisc" Width="60px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"
										Runat="server"></asp:textbox></TD>
								<TD></TD>
								<TD>Cash Discount</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtCashDisc" onblur="GetNetAmount()"
										runat="server" Width="124px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox><asp:dropdownlist id="DropCashDiscType" runat="server" Height="17px" CssClass="dropdownlist" onchange="GetNetAmount()">
										<asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
										<asp:ListItem Value="Per">%</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtCashDiscount" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Message</TD>
								<TD><asp:textbox id="txtMessage" runat="server" Width="110px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
								<TD></TD>
								<TD align="left">
                                    <table width="100%">
                                        <tr>
                                            <td width="2px">IGST</td>
                                            <td width="80px">
                                                <asp:radiobutton id="No" onclick="return GetNetAmount();" runat="server" ToolTip="Not Applied" Checked="true" GroupName="VAT" ></asp:radiobutton>
                                                <asp:radiobutton id="Yes" onclick="return GetNetAmount();" runat="server" ToolTip="Apply" Checked="false" GroupName="VAT" ></asp:radiobutton>
                                            </td>
                                        </tr>
                                    </table>
									</TD>
								<TD><asp:textbox id="txtVAT" runat="server" Width="124px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
                             <TR>
                                <TD>&nbsp;Remark</TD>
                                <TD><asp:textbox id="txtRemark" runat="server" Width="110px" BorderStyle="Groove" CssClass="dropdownlist"
											MaxLength="49"></asp:textbox></TD>
                                <TD></TD>
                                <TD align="left">
                                    <table width="100%">
                                        <tr>
                                            <td width="2px">CGST</td>
                                            <td width="95px">
                                               <asp:RadioButton ID="N" onclick="return GetNetAmount();" runat="server" ToolTip="Not Applied"	Checked="false" GroupName="cgst"></asp:radiobutton>
                                               <asp:RadioButton ID="Y" onclick="return GetNetAmount();" runat="server" ToolTip="Applied"	Checked="true" GroupName="cgst"></asp:radiobutton>
                                            </td>
                                        </tr>
                                    </table>
                                    </TD>
                                <TD><asp:TextBox ID="Textcgst" runat="server" Width="124px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"></asp:TextBox></TD>
                            </TR>

                            <TR>
                                <TD></TD>
                                <TD></TD>
                                <TD></TD>
                                <TD align="left">
                                    <table width="100%">
                                        <tr>
                                            <td width="2px">SGST</td>
                                            <td width="95px">
                                                <asp:RadioButton ID="Noo" onclick="return GetNetAmount();" runat="server" ToolTip="Not Applied"	Checked="false" GroupName="sgst" ></asp:radiobutton>
                                                <asp:RadioButton ID="Yess" onclick="return GetNetAmount();" runat="server" ToolTip="Applied"	Checked="true" GroupName="sgst" ></asp:radiobutton>
                                            </td>
                                        </tr>
                                    </table>
                                    </TD>
                                <TD><asp:TextBox ID="Textsgst" runat="server" Width="124px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"></asp:TextBox></TD>
                            </TR>
							<TR>
								<TD>&nbsp;</TD>
								<TD>
									<P>&nbsp;</P>
								</TD>
								<TD></TD>
								<TD>Net Amount</TD>
								<TD><asp:textbox id="txtNetAmount" runat="server" Width="124px" ReadOnly="True" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD><asp:label id="lblEntryTime" runat="server"></asp:label></TD-->
								<TD align="right" colSpan="4">&nbsp;&nbsp;<asp:button id="Button1" runat="server" Width="50px"  Text="Print" 
										 onclick="Button1_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD>&nbsp;<TD align="right" colSpan="4">&nbsp;</TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></IFRAME>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
