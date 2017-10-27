/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
	
	var z=0
	function search1(t,tempst)
	{
	    var val=new Array()
		var count=0
		var z=0    
		t.length=1
		var items=new Array()
		var name=tempst.value
		items=name.split(",")
		var count=items.length
		t.length=count
		for(var j=0;j<count;j++)
		{
		    t.options[j].text=items[j]
		}
	}
	
	function select(t,text)
	{
		text.value=t.options[t.selectedIndex].text //Comment by vikas sharma 2.05.09
		
		/**************Add by vikas sharma 27.04.09*********************
		var txtval1=t.options[t.selectedIndex].text
		var txtval2=txtval1.split(':')
		text.value=txtval2[0]
		/***********************************/
		/******Add by vikas 27.10.2012****************/
		if(t.name=="DropCustName")
			{
				getBalanceontext(document.Form1.text1,document.Form1.lblPlace,document.Form1.lblDueDate,document.Form1.lblCurrBalance,document.Form1.lblCreditLimit,document.Form1.TxtCrLimit,document.Form1.txtVehicleNo,document.Form1.DropUnderSalesMan)
			}	
		/******End****************/
		
		t.style.visibility="hidden";
		t.style.height=0
	}
	function MouseMove(t)
	{
		
	}
	function Selectbyenter(t,e,text,temp)
	{
		if(text.value!="" && t.style.visibility!="hidden")
		{
			if(window.event) 
			{
				var	key = e.keyCode;
				if(key==13)
				{
					var j=t.selectedIndex
					text.value=t.options[j].text  //Comment by vikas sharma 2.05.09
					/************Add by vikas sharma 27.04.09****************************
					var val1=t.options[j].text
					var val2=val1.split(':');
					text.value=val2[0]
					/************End*****************************************************/
					
					t.style.visibility="hidden"
					t.style.height=0
					if(t.name=="DropCustName")
					{
						getBalanceontext(document.Form1.text1,document.Form1.lblPlace,document.Form1.lblDueDate,document.Form1.lblCurrBalance,document.Form1.lblCreditLimit,document.Form1.TxtCrLimit,document.Form1.txtVehicleNo,document.Form1.DropUnderSalesMan)
					}
					if(text.value!="" && text.value!="Type")
						temp.focus();
					else
						text.focus();
				}
			}
		}
	}
	
	function arrowkeydown(t,e,Drop,tempst)
	{
		if(window.event) 
		{
			var	key = e.keyCode;
			
			if(key==40)
			{
				
				if(Drop.length!=0)
				{
					if(t.value=="")
						search1(Drop,tempst)
					Drop.style.visibility="visible"
					Drop.style.height=120
					Drop.focus();
					var j=Drop.selectedIndex
					t.value=Drop.options[j].text
					if(Drop.name=="DropCustName")
						getBalanceontext(document.Form1.text1,document.Form1.lblPlace,document.Form1.lblDueDate,document.Form1.lblCurrBalance,document.Form1.lblCreditLimit,document.Form1.TxtCrLimit,document.Form1.txtVehicleNo,document.Form1.DropUnderSalesMan)
				}
			}
			if(key==8)
			{
				if(t.value=="Selec" || t.value=="Typ")
					t.value="";
			}
	    }
	    
	}
	function HideList(t,text)
	{
		var j=t.selectedIndex
		text.value=t.options[j].text
		t.style.visibility="hidden"
		t.style.height=0
	}
	function OpenList(t)
	{
		if(t.length!=0)
		{
			t.style.visibility="visible"
			t.style.height=120
		}
	}
	function ScrollList(t)
	{
		alert("scroll")
		t.focus()
	}
	function arrowkeyselect(t,e,v,text)
	{
		
		if(window.event) 
		{
			var	key = e.keyCode;
			//alert("k:"+key)
			//alert(t.length)
			if(key==40)
			{
				var j=t.selectedIndex
				text.value=t.options[j].text
			}
			if(key ==38)
			{
				var j=t.selectedIndex
				text.value=t.options[j].text
			}
			else
			{
				var j=t.selectedIndex
				text.value=t.options[j].text
			}
			if(text.value!="Type" && text.value!="Select")
			{
				if(key==9||key==13)
					v.focus()
			}
	    }
	}
	
	function mousekeyselect(t)
	{
		var j=t.selectedIndex
		document.Form1.text1.value=t.options[j].text
	}
		
	function dropshow(t)
	{
		if(t.style.visibility=="hidden")
		{
			if(t.length!=0)
			{
				t.style.visibility="visible"
				t.style.height=120
				t.selectedIndex=0
			}
		}
		else
		{
			t.style.visibility="hidden"
		    t.style.height=0
		}
	}
	function Test()
	{
		alert("")
	}
		
	function search3(text,t,v1)
	{
		var val=new Array()
		var v2=new Array()
		var count=0
		var z=0		     
		var items=new Array()
		val=v1.split(",");
		var v=text.value
       	var val1=text.value
       	var al=new Array();
		var c=0;
		var k=0
		var flag=false
		for(var i=0;i<val.length;i++)
		{
			var v3=""
			var v1=val[i]
			if(i!=0)
			{
				v2=v1.split(":")
				v3=v2[1]+":"+v2[2] 
			}
			else
				v3=v1
			
			val1=val1.toUpperCase()
			v1=v1.toUpperCase()
			if(val1.indexOf("(")>=0)
			{
				if(val1.indexOf(")")>=0)
				{
					if(v1.search(val1)==0)
					{
						al[c]=val[i]
						c++;
						k=i;
						flag=true;
						break;
					}
					else if(v3.search(val1)==0)
					{
						al[c]=val[i]
						c++;
						k=i;
						flag=true;
						break;
					}
					else
					{
						flag=false
					}
				}
			}
			else
			{
				if(v1.search(val1)==0)
				{
					al[c]=val[i]
					c++;
					k=i;
					flag=true;
					break;
				}
				else if(v3.search(val1)==0)
				{
					al[c]=val[i]
					c++;
					k=i;
					flag=true;
					break;
				}
				else
				{
					flag=false
				}
			}
		}
		if(flag==true)
		{
			if(t.length!=0)
			{
				t.style.visibility="visible";
				t.style.height=120
			}
		}
		else
		{
			t.style.visibility="hidden";
			t.style.height=0
		}
		for(i=k+1;i<val.length;i++)
		{
			al[c]=val[i]
			c++;
		}
		t.length=c
		for(var i=0;i<c;i++)
		{
			t.options[i].text=al[i]				
		}
		t.selectedIndex=0
	}
