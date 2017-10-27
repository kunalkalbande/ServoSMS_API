<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.PurchaseReturn" CodeFile="PurchaseReturn.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Purchase Return Credit Note</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<script language="javascript" id="sales" src="../../Sysitem/JS/Sales.js"></script>
		<script language="javascript" id="sales" src="../../Sysitem/JS/Fuel.js"></script>
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
		
	  
	function selectAll()
	{
		//alert("In");
		var CheckBox = new Array(document.Form1.Check1,document.Form1.Check2,document.Form1.Check3,document.Form1.Check4,document.Form1.Check5,document.Form1.Check6,document.Form1.Check7,document.Form1.Check8,document.Form1.Check9,document.Form1.Check10,document.Form1.Check11,document.Form1.Check12,document.Form1.Check13,document.Form1.Check14,document.Form1.Check15,document.Form1.Check16,document.Form1.Check17,document.Form1.Check18,document.Form1.Check19,document.Form1.Check20);
		var ProdName = new Array(document.Form1.txtProdName1,document.Form1.txtProdName2,document.Form1.txtProdName3,document.Form1.txtProdName4,document.Form1.txtProdName5,document.Form1.txtProdName6,document.Form1.txtProdName7,document.Form1.txtProdName8,document.Form1.txtProdName9,document.Form1.txtProdName10,document.Form1.txtProdName11,document.Form1.txtProdName12,document.Form1.txtProdName13,document.Form1.txtProdName14,document.Form1.txtProdName15,document.Form1.txtProdName16,document.Form1.txtProdName17,document.Form1.txtProdName18,document.Form1.txtProdName19,document.Form1.txtProdName20);
		//var Pack = new Array(document.Form1.txtPack1,document.Form1.txtPack2,document.Form1.txtPack3,document.Form1.txtPack4,document.Form1.txtPack5,document.Form1.txtPack6,document.Form1.txtPack7,document.Form1.txtPack8,document.Form1.txtPack9,document.Form1.txtPack10,document.Form1.txtPack11,document.Form1.txtPack12);
		var Qty = new Array(document.Form1.txtQty1,document.Form1.txtQty2,document.Form1.txtQty3,document.Form1.txtQty4,document.Form1.txtQty5,document.Form1.txtQty6,document.Form1.txtQty7,document.Form1.txtQty8,document.Form1.txtQty9,document.Form1.txtQty10,document.Form1.txtQty11,document.Form1.txtQty12,document.Form1.txtQty13,document.Form1.txtQty14,document.Form1.txtQty15,document.Form1.txtQty16,document.Form1.txtQty17,document.Form1.txtQty18,document.Form1.txtQty19,document.Form1.txtQty20);
		var Rate = new Array(document.Form1.txtRate1,document.Form1.txtRate2,document.Form1.txtRate3,document.Form1.txtRate4,document.Form1.txtRate5,document.Form1.txtRate6,document.Form1.txtRate7,document.Form1.txtRate8,document.Form1.txtRate9,document.Form1.txtRate10,document.Form1.txtRate11,document.Form1.txtRate12,document.Form1.txtRate13,document.Form1.txtRate14,document.Form1.txtRate15,document.Form1.txtRate16,document.Form1.txtRate17,document.Form1.txtRate18,document.Form1.txtRate19,document.Form1.txtRate20);
		var Amount = new Array(document.Form1.txtAmount1,document.Form1.txtAmount2,document.Form1.txtAmount3,document.Form1.txtAmount4,document.Form1.txtAmount5,document.Form1.txtAmount6,document.Form1.txtAmount7,document.Form1.txtAmount8,document.Form1.txtAmount9,document.Form1.txtAmount10,document.Form1.txtAmount11,document.Form1.txtAmount12,document.Form1.txtAmount13,document.Form1.txtAmount14,document.Form1.txtAmount15,document.Form1.txtAmount16,document.Form1.txtAmount17,document.Form1.txtAmount18,document.Form1.txtAmount19,document.Form1.txtAmount20);
		// var TempQty = new Array(document.Form1.txtTempQty1,document.Form1.txtTempQty2,document.Form1.txtTempQty3,document.Form1.txtTempQty4,document.Form1.txtTempQty5,document.Form1.txtTempQty6,document.Form1.txtTempQty7,document.Form1.txtTempQty8);
		var TempQty = new Array(document.Form1.tmpQty1,document.Form1.tmpQty2,document.Form1.tmpQty3,document.Form1.tmpQty4,document.Form1.tmpQty5,document.Form1.tmpQty6,document.Form1.tmpQty7,document.Form1.tmpQty8,document.Form1.tmpQty9,document.Form1.tmpQty10,document.Form1.tmpQty11,document.Form1.tmpQty12,document.Form1.tmpQty13,document.Form1.tmpQty14,document.Form1.tmpQty15,document.Form1.tmpQty16,document.Form1.tmpQty17,document.Form1.tmpQty18,document.Form1.tmpQty19,document.Form1.tmpQty20);

		if(document.Form1.CheckAll.checked == true)
		{
			//alert("if")
			for(var i = 0; i < CheckBox.length ; i++)
			{
				if(ProdName[i].value != "")
				{
					CheckBox[i].checked = true;
					ProdName[i].disabled = false; 
					//Pack[i].disabled = false;
					Qty[i].disabled = false;
					Rate[i].disabled = false;
					Amount[i].disabled = false; 
					calc(Qty[i],Rate[i],TempQty[i])
    			}
				else
					continue
			} 
		}
		else
		{
			for(var i = 0; i < CheckBox.length ; i++)
			{
				CheckBox[i].checked = false;
		        ProdName[i].disabled = true; 
		        //Pack[i].disabled = true;
		        Qty[i].disabled = true;
		        Rate[i].disabled = true;
		        Amount[i].disabled = true;  
		        Qty[i].value = TempQty[i].value;
		    }
			calc1(Qty[i],Rate[i])
		}
		//GetGrandTotal()
		// GetNetAmount()
	}
	function allUnChecked()
	{
		var CheckBox = new Array(document.Form1.Check1,document.Form1.Check2,document.Form1.Check3,document.Form1.Check4,document.Form1.Check5,document.Form1.Check6,document.Form1.Check7,document.Form1.Check8,document.Form1.Check9,document.Form1.Check10,document.Form1.Check11,document.Form1.Check12,document.Form1.Check13,document.Form1.Check14,document.Form1.Check15,document.Form1.Check16,document.Form1.Check17,document.Form1.Check18,document.Form1.Check19,document.Form1.Check20);
		var c = 0;
 
		for(var i= 0 ; i < CheckBox.length; i++)
		{	
			if(CheckBox[i].checked == false)
			{
				c++
			}
		}
		//if(c == 12)
		if(c == 20)
		{
			return true;
		}
		else
		return false;
	}		
 
	//function select1(check, product, pack, qty, rate, amount, tmpQty)
	function select1(check, product, qty, rate, amount, tmpQty)
	{
		if(check.checked == true)
		{
			product.disabled = false;
			//pack.disabled = false;
			qty.disabled = false;
			rate.disabled = false;
			amount.disabled = false; 
			calc(qty,rate,tmpQty)   
		}
		else
		{
			product.disabled = true;
			//pack.disabled = true;
			qty.disabled = true;
			rate.disabled = true;
			amount.disabled = true; 
			qty.value = tmpQty.value
			if(allUnChecked())
			{
				calc1(qty,rate)
			}
			else
			{
				calc(qty,rate,tmpQty)
			}
		}
		//  GetGrandTotal()
		//GetNetAmount()
	}

	function calc1(txtQty,txtRate)
	{		
		//alert("inside")
		var sarr = new Array()
		var temp ="";
		<%for(int i=1;i<=20;i++){%>
		document.Form1.txtAmount<%=i%>.value=document.Form1.txtQty<%=i%>.value*document.Form1.txtRate<%=i%>.value	
		if(document.Form1.txtAmount<%=i%>.value==0)
			document.Form1.txtAmount<%=i%>.value=""
		else
			makeRound(document.Form1.txtAmount<%=i%>)
		<%}%>
	
		/*document.Form1.txtAmount1.value=document.Form1.txtQty1.value*document.Form1.txtRate1.value	
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
		document.Form1.txtAmount9.value= document.Form1.txtQty9.value*document.Form1.txtRate9.value	
		if(document.Form1.txtAmount9.value==0)
			document.Form1.txtAmount9.value=""
		document.Form1.txtAmount10.value= document.Form1.txtQty10.value*document.Form1.txtRate10.value	
		if(document.Form1.txtAmount10.value==0)
			document.Form1.txtAmount10.value=""
		document.Form1.txtAmount11.value= document.Form1.txtQty11.value*document.Form1.txtRate11.value	
		if(document.Form1.txtAmount11.value==0)
			document.Form1.txtAmount11.value=""
		document.Form1.txtAmount12.value= document.Form1.txtQty12.value*document.Form1.txtRate12.value	
		if(document.Form1.txtAmount12.value==0)
			document.Form1.txtAmount12.value=""*/
		
		document.Form1.txtGrandTotal.value = document.Form1.tmpGrandTotal.value  
		makeRound(document.Form1.txtGrandTotal);
		document.Form1.txtDisc.value = document.Form1.tmpDisc.value  
		makeRound(document.Form1.txtDisc);
		document.Form1.txtNetAmount.value = document.Form1.tmpNetAmount.value  
		makeRound(document.Form1.txtNetAmount);
		document.Form1.txtCashDisc.value = document.Form1.tmpCashDisc.value  
		makeRound(document.Form1.txtCashDisc.value);
		document.Form1.txtVAT.value = document.Form1.tmpVatAmount.value  
		makeRound(document.Form1.txtVAT);	
		//****bhal add**/		 GetGrandTotal1();
		GetGrandTotaltemp();
		// alert("hello calc1");
	}  

	function calc(txtQty,txtRate,txtTempQty)
	{	
		var sarr = new Array()
		var temp ="";
		// alert(txtQty.value + "  "+txtTempQty.value);
		if(eval(txtQty.value) > eval(txtTempQty.value))
		{
			alert("Return quantity should not be greater than "+txtTempQty.value)
			txtQty.value = "";
			return false;
		}
		//***add bhal*/changeqtyltr()
		//***add bhal*/Getliter()
		/***add bhal*/GetGrandTotal1();
		<%for(int i=1;i<=20;i++){%>
		document.Form1.txtAmount<%=i%>.value=document.Form1.txtQty<%=i%>.value*document.Form1.txtRate<%=i%>.value	
		if(document.Form1.txtAmount<%=i%>.value==0)
			document.Form1.txtAmount<%=i%>.value=""
		else
			makeRound(document.Form1.txtAmount<%=i%>)
		<%}%>
	
		/*document.Form1.txtAmount1.value=document.Form1.txtQty1.value*document.Form1.txtRate1.value	
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
		document.Form1.txtAmount9.value= document.Form1.txtQty9.value*document.Form1.txtRate9.value	
		if(document.Form1.txtAmount9.value==0)
			document.Form1.txtAmount9.value=""
		document.Form1.txtAmount10.value= document.Form1.txtQty10.value*document.Form1.txtRate10.value	
		if(document.Form1.txtAmount10.value==0)
			document.Form1.txtAmount10.value=""
		document.Form1.txtAmount11.value= document.Form1.txtQty11.value*document.Form1.txtRate11.value	
		if(document.Form1.txtAmount11.value==0)
			document.Form1.txtAmount11.value=""
		document.Form1.txtAmount12.value= document.Form1.txtQty12.value*document.Form1.txtRate12.value	
		if(document.Form1.txtAmount12.value==0)
			document.Form1.txtAmount12.value=""*/

		GetGrandTotal()
		//GetGrandTotaltemp();//add by Mahesh On 01.05.008
		//*bhal-add*/GetGrandTotal1()
		/***com**/	 GetNetAmount()
		//*bhal-add*/ GetNetAmountEtaxnew()
	}	
	
	function GetGrandTotal()
	{
		var GTotal=0
		<%for(int i=1;i<=20;i++){%>
		if(document.Form1.txtAmount<%=i%>.value!="" && document.Form1.Check<%=i%>.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount<%=i%>.value)
		<%}%>
		/*if(document.Form1.txtAmount1.value!="" && document.Form1.Check1.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount1.value)
		if(document.Form1.txtAmount2.value!="" && document.Form1.Check2.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount2.value)
		if(document.Form1.txtAmount3.value!="" && document.Form1.Check3.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount3.value)
		if(document.Form1.txtAmount4.value!="" && document.Form1.Check4.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount4.value)
		if(document.Form1.txtAmount5.value!="" && document.Form1.Check5.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount5.value)
		if(document.Form1.txtAmount6.value!="" && document.Form1.Check6.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount6.value)
		if(document.Form1.txtAmount7.value!="" && document.Form1.Check7.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount7.value)
		if(document.Form1.txtAmount8.value!="" && document.Form1.Check8.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount8.value)
		if(document.Form1.txtAmount9.value!="" && document.Form1.Check9.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount9.value)
		if(document.Form1.txtAmount10.value!="" && document.Form1.Check10.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount10.value)
		if(document.Form1.txtAmount11.value!="" && document.Form1.Check11.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount11.value)
		if(document.Form1.txtAmount12.value!="" && document.Form1.Check12.checked == true)
		 	GTotal=GTotal+eval(document.Form1.txtAmount12.value)*/
		document.Form1.txtGrandTotal.value=GTotal ;
		makeRound(document.Form1.txtGrandTotal);
	}	

	/////////////*********bhal Add******
	function GetGrandTotaltemp()
	{	 
		var GTotal1=0
		var GTotal2=0
		changeqtyltr();
		<%for(int i=1;i<=20;i++){%>
		if(document.Form1.txtQty<%=i%>.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty<%=i%>.value);
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
		}
		<%}%>
		/*if(document.Form1.txtQty1.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty1.value);
	 		GTotal2=GTotal2+eval(document.Form1.txtqPack1.value)*eval(document.Form1.txtQty1.value);
		}	
		if(document.Form1.txtQty2.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty2.value)
			GTotal2=GTotal2+eval(document.Form1.txtqPack2.value)*eval(document.Form1.txtQty2.value);
		}
		if(document.Form1.txtQty3.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty3.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack3.value)*eval(document.Form1.txtQty3.value);
		}
		if(document.Form1.txtQty4.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty4.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack4.value)*eval(document.Form1.txtQty4.value);
		}
		if(document.Form1.txtQty5.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty5.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack5.value)*eval(document.Form1.txtQty5.value);
		}
		if(document.Form1.txtQty6.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty6.value)
	 		GTotal2=GTotal2+eval(document.Form1.txtqPack6.value)*eval(document.Form1.txtQty6.value);
		}
		if(document.Form1.txtQty7.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty7.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack7.value)*eval(document.Form1.txtQty7.value);
		}
		if(document.Form1.txtQty8.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty8.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack8.value)*eval(document.Form1.txtQty8.value);
		}
		if(document.Form1.txtQty9.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty9.value)
	 		GTotal2=GTotal2+eval(document.Form1.txtqPack9.value)*eval(document.Form1.txtQty9.value);
		}
		if(document.Form1.txtQty10.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty10.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack10.value)*eval(document.Form1.txtQty10.value);
		}
		if(document.Form1.txtQty11.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty11.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack11.value)*eval(document.Form1.txtQty11.value);
		}
		if(document.Form1.txtQty12.value!="")
		{
			GTotal1=GTotal1+eval(document.Form1.txtQty12.value)
		 	GTotal2=GTotal2+eval(document.Form1.txtqPack12.value)*eval(document.Form1.txtQty12.value);
		}*/
		document.Form1.txttotalqty.value=GTotal1 ;
		document.Form1.txttotalqtyltr.value=GTotal2 ;
		document.Form1.txtebirdamt.value=GTotal2*document.Form1.txtebird.value ;
		document.Form1.txttradeamt.value=GTotal2*document.Form1.txttrade.value ;
		makeRound(document.Form1.txtebirdamt)
		makeRound(document.Form1.txttradeamt)
		//alert("bird : "+GTotal1+":::::"+GTotal2);
		//alert("trade : "+document.Form1.txtebirdamt.value);
		makeRound(document.Form1.txttotalqty);
		makeRound(document.Form1.txttotalqtyltr);
	}
	
	/*function Getliterpack(txttempltr,txttempqtypck)
	{ 
		var GTotal1=0
		var GTotal2=0
	    changeqtyltr();
	    GetGrandTotal1();
		if(document.Form1.txtQty1.value!="" && document.Form1.Check1.checked==true)
		{
			document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty1.value);
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack1.value)*eval(document.Form1.txtQty1.value));
		}
		if(document.Form1.txtQty2.value!=""&& document.Form1.Check2.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty2.value)
			document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack2.value)*eval(document.Form1.txtQty2.value));
	    }
		if(document.Form1.txtQty3.value!=""&& document.Form1.Check3.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty3.value)
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack3.value)*eval(document.Form1.txtQty3.value));
	    }
		if(document.Form1.txtQty4.value!=""&& document.Form1.Check4.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty4.value)
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack4.value)*eval(document.Form1.txtQty4.value));
	    }
		if(document.Form1.txtQty5.value!=""&& document.Form1.Check5.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty5.value)
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack5.value)*eval(document.Form1.txtQty5.value));
	    }
		if(document.Form1.txtQty6.value!=""&& document.Form1.Check6.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty6.value)
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack6.value)*eval(document.Form1.txtQty6.value));
	    }
		if(document.Form1.txtQty7.value!=""&& document.Form1.Check7.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty7.value)
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack7.value)*eval(document.Form1.txtQty7.value));
	    }
		if(document.Form1.txtQty8.value!=""&& document.Form1.Check8.checked==true)
	 	{
	 		document.Form1.txttotalqty.value=eval(document.Form1.txttotalqty.value)-eval(document.Form1.txtQty8.value)
	 		document.Form1.txttotalqtyltr.value=eval(document.Form1.txttotalqtyltr.value)-(eval(document.Form1.txtqPack8.value)*eval(document.Form1.txtQty8.value));
	    }
		//	document.Form1.txttotalqty.value=GTotal1 ;
	
		//document.Form1.txttotalqtyltr.value=GTotal2 ;
		document.Form1.txtebirdamt.value=eval(document.Form1.txttotalqtyltr.value)*eval(document.Form1.txtebird.value) ;
		document.Form1.txttradeamt.value=eval(document.Form1.txttotalqtyltr.value)*eval(document.Form1.txttrade.value) ;
		//alert(document.Form1.txtebirdamt.value);
		makeRound(document.Form1.txttotalqty);
		makeRound(document.Form1.txttotalqtyltr);
	}*/

	function GetGrandTotal1()
	{ 
		var GTotal1=0
		var GTotal2=0
	    changeqtyltr();
		
		<%for(int i=1;i<=20;i++){%>
		if(document.Form1.txtQty<%=i%>.value!=""&& document.Form1.Check<%=i%>.checked==true)
	 	{
	 		GTotal1=GTotal1+eval(document.Form1.txtQty<%=i%>.value);
	 		GTotal2=GTotal2+eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
	    }
		<%}%>
	 /*if(document.Form1.txtQty1.value!=""&& document.Form1.Check1.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty1.value);
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack1.value)*eval(document.Form1.txtQty1.value);
	    }
	 if(document.Form1.txtQty2.value!=""&& document.Form1.Check2.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty2.value)
	   GTotal2=GTotal2+eval(document.Form1.txtqPack2.value)*eval(document.Form1.txtQty2.value);
	    }
	 if(document.Form1.txtQty3.value!=""&& document.Form1.Check3.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty3.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack3.value)*eval(document.Form1.txtQty3.value);
	    }
	 if(document.Form1.txtQty4.value!=""&& document.Form1.Check4.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty4.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack4.value)*eval(document.Form1.txtQty4.value);
	    }
	 if(document.Form1.txtQty5.value!=""&& document.Form1.Check5.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty5.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack5.value)*eval(document.Form1.txtQty5.value);
	    }
	 if(document.Form1.txtQty6.value!=""&& document.Form1.Check6.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty6.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack6.value)*eval(document.Form1.txtQty6.value);
	    }
	 if(document.Form1.txtQty7.value!=""&& document.Form1.Check7.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty7.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack7.value)*eval(document.Form1.txtQty7.value);
	    }
	 if(document.Form1.txtQty8.value!=""&& document.Form1.Check8.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty8.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack8.value)*eval(document.Form1.txtQty8.value);
	    }
	 if(document.Form1.txtQty9.value!="" && document.Form1.Check9.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty9.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack9.value)*eval(document.Form1.txtQty9.value);
	    }
	 if(document.Form1.txtQty10.value!=""&& document.Form1.Check10.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty10.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack10.value)*eval(document.Form1.txtQty10.value);
	    }
	 if(document.Form1.txtQty11.value!=""&& document.Form1.Check11.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty11.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack11.value)*eval(document.Form1.txtQty11.value);
	    }
	 if(document.Form1.txtQty12.value!=""&& document.Form1.Check12.checked==true)
	 	{GTotal1=GTotal1+eval(document.Form1.txtQty12.value)
	 	GTotal2=GTotal2+eval(document.Form1.txtqPack12.value)*eval(document.Form1.txtQty12.value);
	    }*/
	document.Form1.txttotalqty.value=GTotal1 ;
	
	document.Form1.txttotalqtyltr.value=GTotal2 ;
	document.Form1.txtebirdamt.value=GTotal2*document.Form1.txtebird.value ;
	document.Form1.txttradeamt.value=GTotal2*document.Form1.txttrade.value ;
	makeRound(document.Form1.txtebirdamt)
	makeRound(document.Form1.txttradeamt)
	// alert("txt"+document.Form1.txtebirdamt.value);
	 //document.Form1.temptrade.value=document.Form1.txttradeamt.value;
	 //document.Form1.tempebird.value=document.Form1.txtebirdamt.value;
	 //alert("without less temp"+document.Form1.tempebird.value);
	 makeRound(document.Form1.txttotalqty);
	 makeRound(document.Form1.txttotalqtyltr);
	}


	  
	 
		function changeqtyltr()
		{
			var f1=document.Form1
			var arrType= new Array();
			<%for(int i=1;i<=20;i++){%>
			//******************
	 		if(document.Form1.txtProdName<%=i%>.value.indexOf(":")>0)
				arrType = document.Form1.txtProdName<%=i%>.value.split(":")
			else
			{
				arrType[0]=""
				arrType[1]=""
			}
	 		//******************
			//if(f1.txtPack1.value != "")
			if(arrType[1].toString() != "")
			{
				var mainarr1 = new Array()
				//var hidarr1  = document.Form1.txtPack1.value
				var hidarr1  = arrType[1].toString()
				mainarr1 = hidarr1.split("X")				
				f1.txtqPack<%=i%>.value=mainarr1[0]* mainarr1[1]
			}
			<%}%>
			/*if(f1.txtPack1.value != "")
			{
				var mainarr1 = new Array()
				var hidarr1  = document.Form1.txtPack1.value
				mainarr1 = hidarr1.split("X")				
				f1.txtqPack1.value=mainarr1[0]* mainarr1[1]
				
			}
			if(f1.txtPack2.value != "")
			{
				var mainarr2 = new Array()
				var hidarr2  = document.Form1.txtPack2.value
				mainarr2 = hidarr2.split("X")				
				f1.txtqPack2.value=mainarr2[0]* mainarr2[1]
				
			}
			if(f1.txtPack3.value != "")
			{
				var mainarr3 = new Array()
				var hidarr3  = document.Form1.txtPack3.value
				mainarr3 = hidarr3.split("X")				
				f1.txtqPack3.value=mainarr3[0]* mainarr3[1]
				
			}
			if(f1.txtPack4.value != "")
			{
				var mainarr4 = new Array()
				var hidarr4  = document.Form1.txtPack4.value
				mainarr4 = hidarr4.split("X")				
				f1.txtqPack4.value=mainarr4[0]* mainarr4[1]
				
			}
			if(f1.txtPack5.value != "")
			{
				var mainarr5 = new Array()
				var hidarr5  = document.Form1.txtPack5.value
				mainarr5 = hidarr5.split("X")				
				f1.txtqPack5.value=mainarr5[0]* mainarr5[1]
				
			}
			if(f1.txtPack6.value != "")
			{
				var mainarr6 = new Array()
				var hidarr6  = document.Form1.txtPack6.value
				mainarr6 = hidarr6.split("X")				
				f1.txtqPack6.value=mainarr6[0]* mainarr6[1]
				
			}
			if(f1.txtPack7.value != "")
			{
				var mainarr7 = new Array()
				var hidarr7  = document.Form1.txtPack7.value
				mainarr7 = hidarr7.split("X")				
				f1.txtqPack7.value=mainarr7[0]* mainarr7[1]
				
			}
			if(f1.txtPack8.value != "")
			{
				var mainarr8 = new Array()
				var hidarr8  = f1.txtPack8.value
				mainarr8 = hidarr8.split("X")				
				f1.txtqPack8.value=mainarr8[0]* mainarr8[1]
				
			}
			if(f1.txtPack9.value != "")
			{
				var mainarr9 = new Array()
				var hidarr9  = f1.txtPack9.value
				mainarr9 = hidarr9.split("X")				
				f1.txtqPack9.value=mainarr9[0]* mainarr9[1]
				
			}
			if(f1.txtPack10.value != "")
			{
				var mainarr10 = new Array()
				var hidarr10  = f1.txtPack10.value
				mainarr10 = hidarr10.split("X")				
				f1.txtqPack10.value=mainarr10[0]* mainarr10[1]
				
			}
			if(f1.txtPack11.value != "")
			{
				var mainarr11 = new Array()
				var hidarr11  = f1.txtPack11.value
				mainarr11 = hidarr11.split("X")				
				f1.txtqPack11.value=mainarr11[0]* mainarr11[1]
				
			}
			if(f1.txtPack12.value != "")
			{
				var mainarr12 = new Array()
				var hidarr12  = f1.txtPack12.value
				mainarr12 = hidarr12.split("X")				
				f1.txtqPack12.value=mainarr12[0]* mainarr12[1]
				
			}*/
		}
		//*****function GetEtaxnew()
		function GetCashDiscount()
		{
			var Et=document.Form1.txtentry.value
			document.Form1.tempentrytax.value=eval(Et);
			if(Et=="" || isNaN(Et))
				Et=0
			if(document.Form1.txtentrytype.value=="%")
				Et=document.Form1.txtGrandTotal.value*Et/100
		
			//alert("et"+document.Form1.tempentrytax.value);
			var focDisc=document.Form1.txtfocamt.value
			document.Form1.tempfoc.value=eval(focDisc);
			if(focDisc=="" || isNaN(focDisc))
				focDisc=0
			if(document.Form1.txtfoctype.value=="%")
				focDisc=document.Form1.txtGrandTotal.value*focDisc/100
		
			//alert("foc"+document.Form1.tempfoc.value);
			var tradeDisc=document.Form1.txttradeamt.value
			if(tradeDisc=="" || isNaN(tradeDisc))
				tradeDisc=0
			var tradeless=document.Form1.txttradeless.value
			if(tradeless=="" || isNaN(tradeless))
				tradeless=0	
			document.Form1.temptrade.value=eval(tradeDisc)-eval(tradeless);
			//alert("trade"+document.Form1.temptrade.value);
			var bird=document.Form1.txtebirdamt.value
			if(bird=="" || isNaN(bird))
				bird=0
			var birdless=document.Form1.txtbirdless.value
			if(birdless=="" || isNaN(birdless))
				birdless=0
			document.Form1.tempebird.value=eval(bird)-eval(birdless);
			//alert("bird"+document.Form1.tempebird.value);
			var Disc=document.Form1.txtDisc.value
			if(Disc=="" || isNaN(Disc))
				Disc=0;
			var Dt=0
			if(document.Form1.txtDiscType.value=="%")
			{
				//old Dt=eval(document.Form1.txtGrandTotal.value)-(eval(bird)+eval(tradeDisc)+eval(focDisc))
				//Dt=eval(document.Form1.txtGrandTotal.value)-((eval(bird)-eval(birdless))+(eval(tradeDisc)-eval(tradeless))+eval(focDisc))
				Dt=eval(document.Form1.txtGrandTotal.value)
				Disc=Dt*Disc/100 
			}
			//alert("Discount"+Disc);
			var CashDisc=document.Form1.txtCashDisc.value
			if(CashDisc=="" || isNaN(CashDisc))
				CashDisc=0
			var GT=0
			if(document.Form1.txtCashDiscType.value=="%")
			{  		
				//old  GT=eval(document.Form1.txtGrandTotal.value)-(eval(bird)+eval(tradeDisc)+eval(focDisc))
				GT=eval(document.Form1.txtGrandTotal.value)-((eval(bird)-eval(birdless))+(eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc))
				CashDisc=GT*CashDisc/100 
			}
			//alert("CashDiscount"+CashDisc);
	
			document.Form1.txtVatValue.value = "";	
			//old		document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-eval(tradeDisc)-eval(focDisc)-eval(bird)-eval(CashDisc)-eval(Disc)
			document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-(eval(tradeDisc)-eval(tradeless))-eval(focDisc)-(eval(bird)-eval(birdless))-eval(CashDisc)-eval(Disc)
		}
		//**com****function GetVatAmountetaxnew()
	
	function GetVatAmount()
	{
		//****com*** GetEtaxnew()
	    GetCashDiscount()
	    if(document.Form1.No.checked)
	    {
			document.Form1.txtVAT.value = "";
	    } 
	    else
	    {
			var vat_rate = document.Form1.txtVatRate.value
			//alert(vat_rate);
			if(vat_rate == "")
				vat_rate = 0;
			var vat = document.Form1.txtVatValue.value ;
			if(vat == "" || vat == null || isNaN(vat))
			{
				// alert("if");
				vat = 0;
			}
			// alert("disc: "+vat)
			var vat_amount = vat * vat_rate/100
			// alert("vat_amt : "+vat_amount)
	    
	        document.Form1.txtVAT.value = vat_amount
			makeRound(document.Form1.txtVAT)
	        document.Form1.txtVatValue.value = eval(vat) + eval(vat_amount)
			// alert("total :"+document.Form1.txtVatValue.value)
		}
	}
	
	//**com****  function GetNetAmountEtaxnew()
	function GetNetAmount()
	{   
		//Getliter();
		GetGrandTotal1();
		//alert("getnetamount")
		var vat_value = 0;
		if(document.Form1.No.checked)
	    {
			GetCashDiscount()
			//***GetEtaxnew()
	        vat_value = document.Form1.txtVatValue.value;
			document.Form1.txtVAT.value = "";
	    }
	    else
	    {
			GetVatAmount()
			//*** GetVatAmountetaxnew()
			vat_value = document.Form1.txtVatValue.value;
	    }
	    
		if(vat_value=="" || isNaN(vat_value))
			vat_value=0
		document.Form1.txtNetAmount.value=eval(vat_value);
		makeRound(document.Form1.txtNetAmount);
		//alert(document.Form1.txtNetAmount.value)
		if(document.Form1.txtNetAmount.value==0)
			document.Form1.txtNetAmount.value==""
		
	}
	
	////////***********bhal End******
	/*function GetCashDiscount()
	{
		var CashDisc=document.Form1.txtCashDisc.value
		if(CashDisc=="" || isNaN(CashDisc))
		CashDisc=0
	
		if(document.Form1.txtCashDiscType.value=="%")
			CashDisc=document.Form1.txtGrandTotal.value*CashDisc/100
		//alert("1"+CashDisc); 
		document.Form1.txtVatValue.value = "";	
		document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) - eval(CashDisc);	
		//alert(document.Form1.txtVatValue.value);		    
	}*/
	
	/*function GetVatAmount()
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
			// alert("disc: "+vat)
			var vat_amount = vat * vat_rate/100
			// alert("vat_amt : "+vat_amount)
			document.Form1.txtVAT.value = vat_amount
			makeRound(document.Form1.txtVAT)
	    
	        document.Form1.txtVatValue.value = eval(vat) + eval(vat_amount)
			//alert("total : vat-out"+document.Form1.txtVatValue.value)
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
		if(document.Form1.txtDiscType.value=="%")
			Disc=vat_value * Disc/100 
        // alert(Disc);
		
		document.Form1.txtNetAmount.value=eval(vat_value) - eval(Disc);
		//alert("last net"+document.Form1.txtNetAmount);
		makeRound(document.Form1.txtNetAmount);
		if(document.Form1.txtNetAmount.value==0)
			document.Form1.txtNetAmount.value==""
	}
	*/
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .auto-style1 {
                width: 90px;
            }
        </style>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<INPUT id="tmpQty4" style="Z-INDEX: 121; LEFT: 390px; WIDTH: 7px; POSITION: absolute; TOP: -3px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty4" runat="server"><INPUT id="tempentrytax" style="Z-INDEX: 145; LEFT: 668px; WIDTH: 10px; POSITION: absolute; TOP: 13px; HEIGHT: 22px"
				type="hidden" size="1" name="tempentrytax" runat="server"><INPUT id="tempebird" style="Z-INDEX: 144; LEFT: 648px; WIDTH: 10px; POSITION: absolute; TOP: 10px; HEIGHT: 22px"
				type="hidden" size="1" name="tempebird" runat="server"><INPUT id="temptrade" style="Z-INDEX: 143; LEFT: 621px; WIDTH: 10px; POSITION: absolute; TOP: 7px; HEIGHT: 22px"
				type="hidden" size="1" name="temptrade" runat="server"><INPUT id="tempfoc" style="Z-INDEX: 142; LEFT: 592px; WIDTH: 10px; POSITION: absolute; TOP: 12px; HEIGHT: 22px"
				type="hidden" size="1" name="tempfoc" runat="server"> <INPUT id="tmpGrandTotal" style="Z-INDEX: 141; LEFT: 468px; WIDTH: 9px; POSITION: absolute; TOP: 5px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden2" runat="server"><INPUT id="tmpCashDisc" style="Z-INDEX: 137; LEFT: 483px; WIDTH: 6px; POSITION: absolute; TOP: 8px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden3" runat="server"><INPUT id="tmpVatAmount" style="Z-INDEX: 138; LEFT: 496px; WIDTH: 7px; POSITION: absolute; TOP: 10px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden4" runat="server"><INPUT id="tmpDisc" style="Z-INDEX: 139; LEFT: 508px; WIDTH: 8px; POSITION: absolute; TOP: 10px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden5" runat="server"><INPUT id="tmpNetAmount" style="Z-INDEX: 140; LEFT: 526px; WIDTH: 10px; POSITION: absolute; TOP: 9px; HEIGHT: 22px"
				type="hidden" size="1" name="Hidden1" runat="server"><INPUT id="tmpQty5" style="Z-INDEX: 122; LEFT: 399px; WIDTH: 7px; POSITION: absolute; TOP: -1px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty5" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><INPUT id="TxtVen" style="Z-INDEX: 100; LEFT: -544px; POSITION: absolute; TOP: -16px" type="text"
				name="TxtVen" runat="server"> <INPUT id="TextBox1" style="Z-INDEX: 101; LEFT: -248px; WIDTH: 76px; POSITION: absolute; TOP: -40px; HEIGHT: 22px"
				readOnly type="text" size="7" name="TextBox1" runat="server"> <INPUT id="TxtEnd" style="Z-INDEX: 102; LEFT: -208px; WIDTH: 52px; POSITION: absolute; TOP: -24px; HEIGHT: 22px"
				type="text" size="3" name="TxtEnd" runat="server"><INPUT id="Txtstart" style="Z-INDEX: 103; LEFT: -336px; WIDTH: 83px; POSITION: absolute; TOP: -16px; HEIGHT: 22px"
				type="text" size="8" name="Txtstart" runat="server"> <INPUT id="TxtCrLimit" style="Z-INDEX: 104; LEFT: -448px; WIDTH: 70px; POSITION: absolute; TOP: -16px; HEIGHT: 22px"
				accessKey="TxtEnd" type="text" size="6" name="TxtCrLimit" runat="server">
			<asp:textbox id="TextSelect" style="Z-INDEX: 105; LEFT: 216px; POSITION: absolute; TOP: 16px"
				runat="server" Visible="False" Width="16px"></asp:textbox><asp:textbox id="TextBox2" style="Z-INDEX: 106; LEFT: 192px; POSITION: absolute; TOP: 24px" runat="server"
				Visible="False" Width="8px" BorderStyle="Groove"></asp:textbox><asp:textbox id="TxtCrLimit1" style="Z-INDEX: 107; LEFT: 176px; POSITION: absolute; TOP: 16px"
				runat="server" Visible="False" Width="16px" BorderStyle="Groove" ReadOnly="True"></asp:textbox><INPUT id="temptext" style="Z-INDEX: 108; LEFT: 152px; WIDTH: 16px; POSITION: absolute; TOP: 16px; HEIGHT: 22px"
				type="hidden" size="1" name="temptext" runat="server">
			<asp:textbox id="txtTempQty1" style="Z-INDEX: 109; LEFT: 240px; POSITION: absolute; TOP: 16px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty2" style="Z-INDEX: 110; LEFT: 256px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty3" style="Z-INDEX: 111; LEFT: 272px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty4" style="Z-INDEX: 112; LEFT: 284px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="TextBox7" style="Z-INDEX: 113; LEFT: 336px; POSITION: absolute; TOP: 0px" runat="server"
				Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty5" style="Z-INDEX: 114; LEFT: 296px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty6" style="Z-INDEX: 115; LEFT: 304px; POSITION: absolute; TOP: 8px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty7" style="Z-INDEX: 116; LEFT: 312px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty8" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty9" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty10" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty11" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty12" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty13" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty14" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty15" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty16" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty17" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty18" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty19" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><asp:textbox id="txtTempQty20" style="Z-INDEX: 117; LEFT: 320px; POSITION: absolute; TOP: 0px"
				runat="server" Visible="False" Width="1px"></asp:textbox><INPUT id="tmpQty1" style="Z-INDEX: 118; LEFT: 350px; WIDTH: 10px; POSITION: absolute; TOP: 2px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty1" runat="server"> <INPUT id="tmpQty2" style="Z-INDEX: 119; LEFT: 365px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty2" runat="server"><INPUT id="tmpQty3" style="Z-INDEX: 120; LEFT: 377px; WIDTH: 7px; POSITION: absolute; TOP: 0px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty3" runat="server"><INPUT id="tmpQty6" style="Z-INDEX: 123; LEFT: 410px; WIDTH: 6px; POSITION: absolute; TOP: 0px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty6" runat="server"><INPUT id="tmpQty7" style="Z-INDEX: 124; LEFT: 416px; WIDTH: 5px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty7" runat="server"><INPUT id="tmpQty8" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty8" runat="server"><INPUT id="tmpQty9" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty9" runat="server"><INPUT id="tmpQty10" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty10" runat="server"><INPUT id="tmpQty11" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty11" runat="server"><INPUT id="tmpQty12" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty12" runat="server"><INPUT id="tmpQty13" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty13" runat="server"><INPUT id="tmpQty14" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty14" runat="server"><INPUT id="tmpQty15" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty15" runat="server"><INPUT id="tmpQty16" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty16" runat="server"><INPUT id="tmpQty17" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty17" runat="server"><INPUT id="tmpQty18" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty18" runat="server"><INPUT id="tmpQty19" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty19" runat="server"><INPUT id="tmpQty20" style="Z-INDEX: 125; LEFT: 422px; WIDTH: 2px; POSITION: absolute; TOP: -7px; HEIGHT: 22px"
				type="hidden" size="1" name="tmpQty20" runat="server"> <INPUT id="txtVatRate" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 22px"
				type="hidden" size="1" name="txtVatRate" runat="server"> <INPUT id="txtVatValue" style="Z-INDEX: 134; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtVatValue" runat="server"> <INPUT id="txtqPack1" style="Z-INDEX: 128; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack1" runat="server"> <INPUT id="txtqPack2" style="Z-INDEX: 129; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack2" runat="server"><INPUT id="txtqPack3" style="Z-INDEX: 136; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack3" runat="server"> <INPUT id="txtqPack4" style="Z-INDEX: 135; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack4" runat="server"> <INPUT id="txtqPack5" style="Z-INDEX: 133; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack5" runat="server"> <INPUT id="txtqPack6" style="Z-INDEX: 131; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack6" runat="server"><INPUT id="txtqPack7" style="Z-INDEX: 130; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack7" runat="server"> <INPUT id="txtqPack8" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack8" runat="server"><INPUT id="txtqPack9" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack9" runat="server"><INPUT id="txtqPack10" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack10" runat="server"><INPUT id="txtqPack11" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack11" runat="server"><INPUT id="txtqPack12" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack12" runat="server"><INPUT id="txtqPack13" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack13" runat="server"><INPUT id="txtqPack14" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack14" runat="server"><INPUT id="txtqPack15" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack15" runat="server"><INPUT id="txtqPack16" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack16" runat="server"><INPUT id="txtqPack17" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack17" runat="server"><INPUT id="txtqPack18" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack18" runat="server"><INPUT id="txtqPack19" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack19" runat="server"><INPUT id="txtqPack20" style="Z-INDEX: 127; LEFT: 452px; WIDTH: 5px; POSITION: absolute; TOP: 6px; HEIGHT: 22px"
				type="hidden" size="1" name="txtqPack20" runat="server">
			<table height="278" width="778" align="center">
				<tr>
					<th align="center" colSpan="3">
						<font color="#CE4848">Purchase&nbsp;Return Credit Note</font>
						<hr>
						<asp:label id="lblMessage" runat="server" Font-Size="8pt" ForeColor="DarkGreen"></asp:label></th></tr>
				<tr>
					<td align="center">
						<TABLE cellSpacing="0" cellPadding="5" width="550" border="1">
							<TBODY>
								<TR>
									<TD vAlign="middle" align="center">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD vAlign="middle">Invoice No</TD>
												<TD vAlign="middle"><asp:dropdownlist id="dropInvoiceNo" runat="server" Width="60px" CssClass="dropdownlist" AutoPostBack="True" onselectedindexchanged="dropInvoiceNo_SelectedIndexChanged">
														<asp:ListItem Value="Select">Select</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD>Invoice Date</TD>
												<TD><asp:label id="lblInvoiceDate" runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD>Vendor Invoice No.</TD>
												<TD><INPUT class="dropdownlist" id="lblVendInvoiceNo" style="WIDTH: 135px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 22px; BORDER-BOTTOM-STYLE: groove"
														disabled readOnly type="text" size="22" name="lblVendInvoiceNo" runat="server"></TD>
											</TR>
											<TR>
												<TD>Vendor Invoice Date</TD>
												<TD><INPUT class="dropdownlist" id="lblVendInvoiceDate" style="WIDTH: 135px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 22px; BORDER-BOTTOM-STYLE: groove"
														disabled readOnly type="text" size="22" name="lblVendInvoiceDate" runat="server"></TD>
											</TR>
										</TABLE>
									</TD>
									<TD vAlign="middle" align="center">
										<TABLE cellSpacing="0" cellPadding="0">
											<TR>
												<TD>Vendor Name&nbsp;&nbsp;</TD>
												<TD><INPUT class="dropdownlist" id="lblVendName" style="WIDTH: 158px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 22px; BORDER-BOTTOM-STYLE: groove"
														disabled readOnly type="text" size="22" name="lblCustName" runat="server"></TD>
											</TR>
											<TR>
												<TD>Place</TD>
												<TD><INPUT class="dropdownlist" id="lblPlace" style="WIDTH: 158px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 22px; BORDER-BOTTOM-STYLE: groove"
														disabled readOnly type="text" size="22" name="lblPlace" runat="server"></TD>
											</TR>
											<TR>
												<TD>Vehicle No</TD>
												<TD><INPUT class="dropdownlist" id="lblVehicleNo" style="WIDTH: 158px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; HEIGHT: 22px; BORDER-BOTTOM-STYLE: groove"
														disabled readOnly type="text" size="22" name="lblVehicleNo" runat="server"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="center" colSpan="2">
										<TABLE cellSpacing="0" cellPadding="0">
											<TBODY>
												<TR>
													<TD align="center" colSpan="7"><FONT color="#990066"><STRONG><U>Product &nbsp;Details</U></STRONG></FONT></TD>
												</TR>
												<TR>
													<TD align="center" width="290" colSpan="2"><FONT color="#990066">SKU Name With Pack</FONT></TD>
													<!--TD align="center"><FONT color="#990066">&nbsp;&nbsp;&nbsp;&nbsp;Package</FONT></TD-->
													<TD align="center" class="auto-style1"><FONT color="#990066">Qty
															<asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Please Fill Quentity"
																ControlToValidate="txtQty1">*</asp:requiredfieldvalidator></FONT></TD>
													<TD align="center"><FONT color="#990066">Rate</FONT></TD>
													<TD align="center"><FONT color="#990066">Amount</FONT></TD>
													<TD align="center" width="50"><FONT color="#990066">FOC</FONT></TD>
													<TD align="center"><FONT color="#990066">Select</FONT></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName1" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName1" runat="server"></TD>
													<!--TD><INPUT id="txtPack1" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack1" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1" ><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty1" onblur="calc(this,document.Form1.txtRate1,document.Form1.tmpQty1)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate1" runat="server" Width="105px" BorderStyle="Groove" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount1" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC1" disabled type="checkbox" name="chkFOC1" runat="server"></TD>
													<TD align="center"><INPUT id="Check1" onclick="select1(document.Form1.Check1,document.Form1.txtProdName1,document.Form1.txtQty1,document.Form1.txtRate1,document.Form1.txtAmount1,document.Form1.tmpQty1)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName2" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName2" runat="server"></TD>
													<!--TD><INPUT id="txtPack2" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack2" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty2" onblur="calc(this,document.Form1.txtRate2,document.Form1.tmpQty2)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate2" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount2" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC2" disabled type="checkbox" name="chkFOC2" runat="server"></TD>
													<TD align="center"><INPUT id="Check2" onclick="select1(document.Form1.Check2,document.Form1.txtProdName2,document.Form1.txtQty2,document.Form1.txtRate2,document.Form1.txtAmount2,document.Form1.tmpQty2)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName3" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName3" runat="server"></TD>
													<!--TD><INPUT id="txtPack3" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack3" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty3" onblur="calc(this,document.Form1.txtRate3,document.Form1.tmpQty3)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate3" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount3" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC3" disabled type="checkbox" name="chkFOC3" runat="server"></TD>
													<TD align="center"><INPUT id="Check3" onclick="select1(document.Form1.Check3,document.Form1.txtProdName3,document.Form1.txtQty3,document.Form1.txtRate3,document.Form1.txtAmount3,document.Form1.tmpQty3)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName4" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName4" runat="server"></TD>
													<!--TD><INPUT id="txtPack4" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack4" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty4" onblur="calc(this,document.Form1.txtRate4,document.Form1.tmpQty4)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate4" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount4" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC4" disabled type="checkbox" name="chkFOC4" runat="server"></TD>
													<TD align="center"><INPUT id="Check4" onclick="select1(document.Form1.Check4,document.Form1.txtProdName4,document.Form1.txtQty4,document.Form1.txtRate4,document.Form1.txtAmount4,document.Form1.tmpQty4)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName5" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName5" runat="server">
													</TD>
													<!--TD><INPUT id="txtPack5" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack5" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty5" onblur="calc(this,document.Form1.txtRate5,document.Form1.tmpQty5)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate5" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount5" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC5" disabled type="checkbox" name="chkFOC5" runat="server"></TD>
													<TD align="center"><INPUT id="Check5" onclick="select1(document.Form1.Check5,document.Form1.txtProdName5,document.Form1.txtQty5,document.Form1.txtRate5,document.Form1.txtAmount5,document.Form1.tmpQty5)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName6" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName6" runat="server"></TD>
													<!--TD><INPUT id="txtPack6" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack6" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty6" onblur="calc(this,document.Form1.txtRate6,document.Form1.tmpQty6)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate6" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount6" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC6" disabled type="checkbox" name="chkFOC6" runat="server"></TD>
													<TD align="center"><INPUT id="Check6" onclick="select1(document.Form1.Check6,document.Form1.txtProdName6,document.Form1.txtQty6,document.Form1.txtRate6,document.Form1.txtAmount6,document.Form1.tmpQty6)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName7" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName7" runat="server"></TD>
													<!--TD><INPUT id="txtPack7" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack7" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox id="txtQty7" onblur="calc(this,document.Form1.txtRate7,document.Form1.tmpQty7)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate7" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount7" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC7" disabled type="checkbox" name="chkFOC7" runat="server"></TD>
													<TD align="center"><INPUT id="Check7" onclick="select1(document.Form1.Check7,document.Form1.txtProdName7,document.Form1.txtQty7,document.Form1.txtRate7,document.Form1.txtAmount7,document.Form1.tmpQty7)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName8" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName8" runat="server"></TD>
													<!--TD><INPUT id="txtPack8" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack8" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty8" onblur="calc(this,document.Form1.txtRate8,document.Form1.tmpQty8)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate8" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount8" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC8" disabled type="checkbox" name="chkFOC8" runat="server"></TD>
													<TD align="center"><INPUT id="Check8" onclick="select1(document.Form1.Check8,document.Form1.txtProdName8,document.Form1.txtQty8,document.Form1.txtRate8,document.Form1.txtAmount8,document.Form1.tmpQty8)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName9" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName9" runat="server"></TD>
													<!--TD><INPUT id="txtPack9" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack9" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty9" onblur="calc(this,document.Form1.txtRate9,document.Form1.tmpQty9)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate9" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"
															Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount9" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC9" disabled type="checkbox" name="chkFOC9" runat="server"></TD>
													<TD align="center"><INPUT id="Check9" onclick="select1(document.Form1.Check9,document.Form1.txtProdName9,document.Form1.txtQty9,document.Form1.txtRate9,document.Form1.txtAmount9,document.Form1.tmpQty9)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName10" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName10" runat="server"></TD>
													<!--TD><INPUT id="txtPack10" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack10" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty10" onblur="calc(this,document.Form1.txtRate10,document.Form1.tmpQty10)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate10" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount10" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC10" disabled type="checkbox" name="chkFOC10" runat="server"></TD>
													<TD align="center"><INPUT id="Check10" onclick="select1(document.Form1.Check10,document.Form1.txtProdName10,document.Form1.txtQty10,document.Form1.txtRate10,document.Form1.txtAmount10,document.Form1.tmpQty10)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName11" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName11" runat="server"></TD>
													<!--TD><INPUT id="txtPack11" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack11" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty11" onblur="calc(this,document.Form1.txtRate11,document.Form1.tmpQty11)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate11" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount11" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC11" disabled type="checkbox" name="chkFOC11" runat="server"></TD>
													<TD align="center"><INPUT id="Check11" onclick="select1(document.Form1.Check11,document.Form1.txtProdName11,document.Form1.txtQty11,document.Form1.txtRate11,document.Form1.txtAmount11,document.Form1.tmpQty11)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName12" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack12" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty12" onblur="calc(this,document.Form1.txtRate12,document.Form1.tmpQty12)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate12" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount12" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC12" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check12" onclick="select1(document.Form1.Check12,document.Form1.txtProdName12,document.Form1.txtQty12,document.Form1.txtRate12,document.Form1.txtAmount12,document.Form1.tmpQty12)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName13" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack13" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty13" onblur="calc(this,document.Form1.txtRate13,document.Form1.tmpQty13)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate13" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount13" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC13" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check13" onclick="select1(document.Form1.Check13,document.Form1.txtProdName13,document.Form1.txtQty13,document.Form1.txtRate13,document.Form1.txtAmount13,document.Form1.tmpQty13)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName14" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack14" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty14" onblur="calc(this,document.Form1.txtRate14,document.Form1.tmpQty14)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate14" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount14" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC14" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check14" onclick="select1(document.Form1.Check14,document.Form1.txtProdName14,document.Form1.txtQty14,document.Form1.txtRate14,document.Form1.txtAmount14,document.Form1.tmpQty14)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName15" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack15" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty15" onblur="calc(this,document.Form1.txtRate15,document.Form1.tmpQty15)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate15" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount15" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC15" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check15" onclick="select1(document.Form1.Check15,document.Form1.txtProdName15,document.Form1.txtQty15,document.Form1.txtRate15,document.Form1.txtAmount15,document.Form1.tmpQty15)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName16" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack16" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty16" onblur="calc(this,document.Form1.txtRate16,document.Form1.tmpQty16)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate16" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount16" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC16" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check16" onclick="select1(document.Form1.Check16,document.Form1.txtProdName16,document.Form1.txtQty16,document.Form1.txtRate16,document.Form1.txtAmount16,document.Form1.tmpQty16)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName17" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack17" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty17" onblur="calc(this,document.Form1.txtRate17,document.Form1.tmpQty17)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate17" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount17" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC17" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check17" onclick="select1(document.Form1.Check17,document.Form1.txtProdName17,document.Form1.txtQty17,document.Form1.txtRate17,document.Form1.txtAmount17,document.Form1.tmpQty17)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName18" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack18" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty18" onblur="calc(this,document.Form1.txtRate18,document.Form1.tmpQty18)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate18" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount18" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC18" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check18" onclick="select1(document.Form1.Check18,document.Form1.txtProdName18,document.Form1.txtQty18,document.Form1.txtRate18,document.Form1.txtAmount18,document.Form1.tmpQty18)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName19" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack19" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty19" onblur="calc(this,document.Form1.txtRate19,document.Form1.tmpQty19)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate19" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount19" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC19" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check19" onclick="select1(document.Form1.Check19,document.Form1.txtProdName19,document.Form1.txtQty19,document.Form1.txtRate19,document.Form1.txtAmount19,document.Form1.tmpQty19)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<TR>
													<TD colSpan="2"><INPUT class="dropdownlist" id="txtProdName20" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtProdName12" runat="server"></TD>
													<!--TD><INPUT id="txtPack20" style="WIDTH: 100px; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"
															disabled readOnly type="text" size="22" name="txtPack12" runat="server" class="dropdownlist"></TD-->
													<TD class="auto-style1"><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtQty20" onblur="calc(this,document.Form1.txtRate20,document.Form1.tmpQty20)"
															runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtRate20" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD><asp:textbox id="txtAmount20" runat="server" Width="105px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
													<TD align="center"><INPUT id="chkFOC20" disabled type="checkbox" name="chkFOC12" runat="server"></TD>
													<TD align="center"><INPUT id="Check20" onclick="select1(document.Form1.Check20,document.Form1.txtProdName20,document.Form1.txtQty20,document.Form1.txtRate20,document.Form1.txtAmount20,document.Form1.tmpQty20)"
															type="checkbox" name="Checkbox1" runat="server"></TD>
												</TR>
												<tr>
													<td>&nbsp; Total Ltr/Kg<asp:textbox id="txttotalqtyltr" runat="server" Width="120px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist"></asp:textbox></td>
													<td>&nbsp;&nbsp;&nbsp;&nbsp; Total</td>
													<td class="auto-style1"><asp:textbox id="txttotalqty" runat="server" Width="120px" BorderStyle="Groove" ReadOnly="True"
															CssClass="dropdownlist"></asp:textbox></td>
													<td>&nbsp;</td>
													<td align="center" colSpan="2">Select All</td>
													<TD align="center"><INPUT id="CheckAll" onclick="return selectAll();" type="checkbox" name="Checkbox9" runat="server" onserverchange="CheckAll_ServerChange"></TD>
												</tr>
											</TBODY>
										</TABLE>
									</TD>
								</TR>
							</TBODY>
						</TABLE>
						<TABLE cellSpacing="0" cellPadding="0">
							<TR>
								<TD>Promo Scheme</TD>
								<TD><asp:textbox id="txtPromoScheme" runat="server" Width="120px" BorderStyle="Groove" ReadOnly="True"
										CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
								<TD width="20"></TD>
								<TD>Grand Total</TD>
								<TD><asp:textbox id="txtGrandTotal" runat="server" Width="124px" BorderStyle="Groove" ReadOnly="True"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Entry Tax</TD>
								<TD><asp:textbox id="txtentry" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox><asp:textbox id="txtentrytype" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="True" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
								<TD></TD>
								<TD></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD>Remark</TD>
								<TD>
									<P><asp:textbox id="txtRemark" runat="server" Width="120px" BorderStyle="Groove" ReadOnly="True"
											CssClass="dropdownlist" Enabled="False"></asp:textbox></P>
								</TD>
								<TD></TD>
								<TD>Cash Discount</TD>
								<TD><asp:textbox id="txtCashDisc" onblur="GetNetAmount()" runat="server" Width="124px" BorderStyle="Groove"
										ReadOnly="false" CssClass="dropdownlist"></asp:textbox><asp:textbox id="txtCashDiscType" onblur="GetNetAmount()" runat="server" Width="124px" BorderStyle="Groove"
										ReadOnly="True" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>FOC Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtfocamt" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="false" CssClass="dropdownlist"></asp:textbox><asp:textbox id="txtfoctype" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="True" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
								<!--<TD style="WIDTH: 150px; HEIGHT: 27px"></TD>
								<TD style="WIDTH: 184px; HEIGHT: 27px">
									<P>&nbsp;</P>
								</TD-->
								<TD></TD>
								<TD>Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtDisc" onblur="GetNetAmount()" runat="server" Width="124px" BorderStyle="Groove"
										ReadOnly="false" CssClass="dropdownlist"></asp:textbox><asp:textbox id="txtDiscType" onblur="GetNetAmount()" runat="server" Width="124px" BorderStyle="Groove"
										ReadOnly="True" CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Servo 
									Stk.&nbsp;Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txttrade" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="false" CssClass="dropdownlist"></asp:textbox><asp:textbox id="txttradeamt" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="true" CssClass="dropdownlist" Enabled="False"></asp:textbox><FONT color="#990066">Minus</FONT><asp:textbox id="txttradeless" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
								<!--<TD style="WIDTH: 150px"></TD>
								<TD style="WIDTH: 184px"></TD>-->
								<TD></TD>
								<TD>VAT
									<asp:radiobutton id="No" onclick="return GetNetAmount();" runat="server" Enabled="False" ToolTip="Not Applied"
										GroupName="VAT"  Checked="True"></asp:radiobutton>&nbsp;<asp:radiobutton id="Yes" onclick="return GetNetAmount();" runat="server" Enabled="False" ToolTip="Applied"
										GroupName="VAT"></asp:radiobutton></TD>
								<TD><asp:textbox id="txtVAT" runat="server" Width="124px" BorderStyle="Groove" ReadOnly="True" CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<TR>
								<!--TD style="WIDTH: 150px; HEIGHT: 27px"></TD>
								<TD style="WIDTH: 184px; HEIGHT: 27px"></TD-->
								<TD>Early Bird 
									Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								<TD><asp:textbox id="txtebird" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="false" CssClass="dropdownlist"></asp:textbox><asp:textbox id="txtebirdamt" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										ReadOnly="false" CssClass="dropdownlist" Enabled="False"></asp:textbox><FONT color="#990066">Minus</FONT><asp:textbox id="txtbirdless" onblur="GetNetAmount()" runat="server" Width="120px" BorderStyle="Groove"
										CssClass="dropdownlist"></asp:textbox></TD>
								<TD></TD>
								<TD>Net Amount</TD>
								<TD><asp:textbox id="txtNetAmount" runat="server" Width="124px" BorderStyle="Groove" ReadOnly="True"
										CssClass="dropdownlist"></asp:textbox></TD>
							</TR>
							<!--TR>
								<TD>Entry Date &amp; Time</TD>
								<TD><asp:label id="lblEntryTime" runat="server"></asp:label></TD>
								<TD></TD>
								<TD>Entry&nbsp;By</TD>
								<TD><asp:label id="lblEntryBy" runat="server"></asp:label></TD>
							</TR-->
							<TR>
								<TD>Message</TD>
								<TD colSpan="2"><asp:textbox id="txtMessage" runat="server" Width="120px" BorderStyle="Groove" ReadOnly="True"
										CssClass="dropdownlist" Enabled="False"></asp:textbox></TD>
								<TD align="right" colSpan="2"><asp:button id="btnSave" runat="server" Width="75px"  Text="Save"
										 onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;<asp:button id="btnPrint" runat="server" Width="75px" 
										Text="Print" onclick="btnPrint_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
		<script language="C#">


		</script>
	</body>
</HTML>
