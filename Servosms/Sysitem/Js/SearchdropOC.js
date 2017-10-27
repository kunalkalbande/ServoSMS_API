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

/****This function add by vikas 10.11.2012************************/
	var combotot=0;
function getscheme2_OC(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1,tmpSchType,txtschSP,tmpSchSPType,tmpFoeType)
{
	var ProdName
	var PackType 
	var mainarr = new Array()
	var CustFOE = new Array()
		
	getschemeprimaryTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,tmpSchType);
	getschemeSecSP(prodtype,txtQty,prodtype1,txtschSP,tmpSchSPType);
	
	var packindex;
	var packtext;
	var count1=0;
	var ProdText = prodtype.value 
	var hidarr  = document.Form1.temptext11.value
	var FOE = document.Form1.temptext13.value
	
	mainarr = hidarr.split(",")
	
	var custname1=document.Form1.text1.value
	var custname2=custname1.split(":")
	var custname=custname2[0];
	
	var customer
	CustFOE = FOE.split(",")
	var prodarr = new Array()
	var ratearr = new Array()
	var stockarr = new Array()
	var unitarr = new Array()
	var schemearr = new Array()
	var avlstkarr=new Array()
	var avlstk=new Array()
	var discountarr=new Array()
	
	var gnamearr=new Array()						
	var schemuntarr=new Array()						
	var GType=document.Form1.tempCustGroup.value;	
	
	var Grouparr=new Array()						
	var Scheme;										
	var totcomboarr=new Array()
	var flage=0;									
	
	var t1=0,t2=0,t3=0,t4=0,t5=0,t6=0;              
	var t7=0,t8=0,t9=0,t10=0,t11=0,t12=0;           
	
	var totqty=0;
	
	var dateInv=lblInvoiceDate.value
	var status="n"
	var k = 0
	var r =0;
	var Scheme_Packtype="";
  
	prodtype1.value=""
	txtQty1.value=""
	txtstk1.value=""
	txtfoe1.value=""
	
	if(txtQty.value!=0)
	{
		for(var i=0;i<(mainarr.length-1);i++)
		{
			prodarr = mainarr[i].split(":")
	
						
			Grouparr = prodarr[15].split(".")
			for(var k=0;k<(Grouparr.length-1);k++)
			{
				if(GType==Grouparr[k])
				{
					Scheme="Yes";
				}
			}
			if(Scheme=="Yes")  
			{
				
				if(ProdName==prodarr[2]&&PackType==prodarr[3])
				{
					status="y"; 
				}
				else
				{	
					ProdName=prodarr[2]
					PackType=prodarr[3]
					status="n"
				}
				
				if(prodarr[16] == "Ltr.")
				{
						for(var j=0;j<prodarr.length;j++ )
						{ 
							if(prodarr[2]+":"+prodarr[3]==ProdText)   
							{
								
									stockarr[k]= prodarr[4];
									unitarr[k]=  prodarr[5];
									ratearr[k] = prodarr[6];
									
									if(prodarr[17] != "Combo")            
									{
										var Type = new Array()
										Type=prodarr[3].split("X")
										totqty=Type[0]*Type[1]*txtQty.value
										var a1=prodarr[7];
										var a2=prodarr[8]
										var dd1;
										dd1=totqty%a1
										schemearr[k]=((totqty-dd1)/a1)*a2
										if(prodarr[14]=="Primary(Free Scheme)" || prodarr[14]=="Secondry(Free Scheme)")
										{
											avlstkarr[k]=prodarr[11]+" "+prodarr[12];
											avlstk[k]=prodarr[11];
										}
										else
											avlstkarr[k]="";
										k++; 	
									}	
									else
									{
											Scheme_Packtype="Combo";
												var Type = new Array()
											if(txtQty.name=="txtQty1")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo1.value=totqty
											}
											else if(txtQty.name=="txtQty2")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo2.value=totqty
											}
											else if(txtQty.name=="txtQty3")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo3.value=totqty
											}
											else if(txtQty.name=="txtQty4")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo4.value=totqty
											}
											else if(txtQty.name=="txtQty5")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo5.value=totqty
											}
											else if(txtQty.name=="txtQty6")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo6.value=totqty
											}
											else if(txtQty.name=="txtQty7")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo7.value=totqty
											}
											else if(txtQty.name=="txtQty8")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo8.value=totqty
											}
											else if(txtQty.name=="txtQty9")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo9.value=totqty
											}
											else if(txtQty.name=="txtQty10")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo10.value=totqty
											}
											else if(txtQty.name=="txtQty11")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo11.value=totqty
											}
											else if(txtQty.name=="txtQty12")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo12.value=totqty
											}
										
											if(document.Form1.temcombo1.value!="")
												t1=document.Form1.temcombo1.value
											
											if(document.Form1.temcombo2.value!="")
												t2=document.Form1.temcombo2.value
											
											if(document.Form1.temcombo3.value!="")
												t3=document.Form1.temcombo3.value
											
											if(document.Form1.temcombo4.value!="")
												t4=document.Form1.temcombo4.value
											
											if(document.Form1.temcombo5.value!="")
												t5=document.Form1.temcombo5.value
												
											if(document.Form1.temcombo6.value!="")
												t6=document.Form1.temcombo6.value
												
											if(document.Form1.temcombo7.value!="")
												t7=document.Form1.temcombo7.value
												
											if(document.Form1.temcombo8.value!="")
												t8=document.Form1.temcombo8.value
												
											if(document.Form1.temcombo9.value!="")
												t9=document.Form1.temcombo9.value
												
											if(document.Form1.temcombo10.value!="")
												t10=document.Form1.temcombo10.value
												
											if(document.Form1.temcombo11.value!="")
												t11=document.Form1.temcombo11.value
												
											if(document.Form1.temcombo12.value!="")
												t12=document.Form1.temcombo12.value
												
											combotot=eval(t1)+eval(t2)+eval(t3)+eval(t4)+eval(t5)+eval(t6)+eval(t7)+eval(t8)+eval(t9)+eval(t10)+eval(t11)+eval(t12)
										
											flage=1;
											var a1=prodarr[7];
											var a2=prodarr[8]
											var dd1;
											dd1=combotot%a1
											schemearr[k]=((combotot-dd1)/a1)*a2
											if(prodarr[14]=="Primary(Free Scheme)" || prodarr[14]=="Secondry(Free Scheme)")
											{
												avlstkarr[k]=prodarr[11]+" "+prodarr[12];
												avlstk[k]=prodarr[11];
											}
											else
												avlstkarr[k]="";
											k++;
											
											break;
											
									}
								}
						}
				}
				else
				{
					for(var j=0;j<prodarr.length;j++ )
					{ 
						if(prodarr[2]+":"+prodarr[3]==ProdText)   
						{
							if(status!="y")
							{
								stockarr[k]= prodarr[4];
								unitarr[k]=  prodarr[5];
								ratearr[k] = prodarr[6];
								var s1=prodarr[7];
								var s2=prodarr[8]
								var dd;
								dd=txtQty.value%s1
								schemearr[k]=((txtQty.value-dd)/s1)*s2
								if(prodarr[14]=="Primary(Free Scheme)" || prodarr[14]=="Secondry(Free Scheme)")
								{
									avlstkarr[k]=prodarr[11]+" "+prodarr[12];
									avlstk[k]=prodarr[11];
								}
								else
									avlstkarr[k]="";
								k++;
							}
						} 
					}
				}
			}
			
		}	
		
		getschemefoeTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1,tmpFoeType)
		
		var Flag=0;
		for(var i=0;i<(CustFOE.length);i++)
		{
			if(CustFOE[i]==custname)
			{
				txtsch1.value =""
				Flag=1;
				break;
			}
		}
		
		if(Flag==0)
		{
			if(Scheme_Packtype=="Combo")
			{
				
				if(t1!=0)
				{
					document.Form1.txtTypesch1.value="";
					document.Form1.txtQtysch1.value="";
					document.Form1.txtstk1.value="";
				}
				if(t2!=0)
				{
					document.Form1.txtTypesch2.value="";
					document.Form1.txtQtysch2.value="";
					document.Form1.txtstk2.value="";
				}
				if(t3!=0)
				{
					document.Form1.txtTypesch3.value="";
					document.Form1.txtQtysch3.value="";
					document.Form1.txtstk3.value="";
				}
				if(t4!=0)
				{
					document.Form1.txtTypesch4.value="";
					document.Form1.txtQtysch4.value="";
					document.Form1.txtstk4.value="";
				}
				if(t5!=0)
				{
					document.Form1.txtTypesch5.value="";
					document.Form1.txtQtysch5.value="";
					document.Form1.txtstk5.value="";
				}
				if(t6!=0)
				{
					document.Form1.txtTypesch6.value="";
					document.Form1.txtQtysch6.value="";
					document.Form1.txtstk6.value="";
				}
				if(t7!=0)
				{
					document.Form1.txtTypesch7.value="";
					document.Form1.txtQtysch7.value="";
					document.Form1.txtstk7.value="";
				}
				if(t8!=0)
				{
					document.Form1.txtTypesch8.value="";
					document.Form1.txtQtysch8.value="";
					document.Form1.txtstk8.value="";
				}
				if(t9!=0)
				{
				document.Form1.txtTypesch9.value="";
					document.Form1.txtQtysch9.value="";
					document.Form1.txtstk9.value="";
				}
				if(t10!=0)
				{
				document.Form1.txtTypesch10.value="";
					document.Form1.txtQtysch10.value="";
					document.Form1.txtstk10.value="";
				}
				if(t11!=0)
				{
					document.Form1.txtTypesch11.value="";
					document.Form1.txtQtysch11.value="";
					document.Form1.txtstk11.value="";
				}
				if(t12!=0)
				{
					document.Form1.txtTypesch12.value="";
					document.Form1.txtQtysch12.value="";
					document.Form1.txtstk12.value="";
				}
				
				for(n=2;n<ratearr.length;n++)
				{  
					if(schemearr[n]!=0)
					{
						prodtype1.value=unitarr[n]+":"+ratearr[n]
						if(prodtype1.value==0)
							prodtype1.value=""
						txtQty1.value=schemearr[n]
						if(txtQty1.value=="NaN")
							txtQty1.value=""
						txtstk1.value=avlstkarr[n]
						if(avlstk[n]!= 0)
						{
							txtstk1.value=avlstkarr[n]
						}
						else
						{
							if(txtfoe1.value>0)
							{
								txtsch1.value =""
								prodtype1.value =""
								txtQty1.value =""
								txtstk1.value =""
							}
							else
							{
								//alert("Insufficient Free Stock");
							}
							break;
						}
					}
				}
			}
			else
			{
				for(n=2;n<ratearr.length;n++)
				{  
					if(schemearr[n]!=0)
					{
						prodtype1.value=unitarr[n]+":"+ratearr[n]
						if(prodtype1.value==0)
							prodtype1.value=""
						txtQty1.value=schemearr[n]
						if(txtQty1.value=="NaN")
							txtQty1.value=""
						txtstk1.value=avlstkarr[n]
						if(avlstk[n]!= 0)
						{
							txtstk1.value=avlstkarr[n]
						}
						else
						{
							if(txtfoe1.value>0)
							{
								txtsch1.value =""
								prodtype1.value =""
								txtQty1.value =""
								txtstk1.value =""
							}
							else
							{
								//alert("Insufficient Free Stock");
							}
							break;
						}
					}
				}
			}
		} 
		
	}

}


function getschemeprimaryTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,tmpSchType)
{
 	var ProdName
	var PackType 
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.temptext12.value
	var GType=document.Form1.tempCustGroup.value       
	
	var Scheme="No";										
	var Grouparr=new Array();							
	
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	var status="n"
	var k = 0
	prodtype1.value=""
	txtQty1.value=""
	txtstk1.value=""
	txtsch1.value=""
	tmpSchType.value=""
	if(txtQty.value!=0)
	{
		for(var i=0;i<(mainarr.length-1);i++)
		{
		
			prodarr = mainarr[i].split(":")
			
				for(var j=0;j<prodarr.length;j++ )
				{ 
					if(prodarr[2]+":"+prodarr[3] == prodtext)
					{
							Grouparr = prodarr[7].split(".")
							if(Grouparr.length!=0)
							{
								if(Grouparr.length==2)
								{
									if(GType==Grouparr[0])
									{
										discountarr[k]=prodarr[4];
										DisTypearr[k]=prodarr[6];
										k++;
										
									}
								}
								else
								{
									for(var k=0;k<(Grouparr.length-1);k++)
									{
										if(GType==Grouparr[k] )
										{
											Scheme="Yes";
										}
									}
									if(Scheme="Yes")
									{
										discountarr[k]=prodarr[4];
										DisTypearr[k]=prodarr[6];
										k++;
										
									}
								}
							}
						}
					} 
				
		}
		
		for(n=0;n<discountarr.length;n++)
		{  
			txtsch1.value=discountarr[n]
			tmpSchType.value=DisTypearr[n]
		} 
	}
}

function getschemeSecSP(prodtype,txtQty,prodtype1,txtschSP,tmpSchSPType)
{
	var ProdName
	var PackType 
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.temptextSecSP.value
	
	var GType=document.Form1.tempCustGroup.value       
	
	var Scheme;										
	var Grouparr = new Array();						
	
	mainarr = hidarr.split(",")
	
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	var status="n"
	var k = 0
	prodtype1.value=""
	txtschSP.value=""
	tmpSchSPType.value=""
	if(txtQty.value!=0)
	{
		for(var i=0;i<(mainarr.length-1);i++)
		{
			prodarr = mainarr[i].split(":")
			
			/*********Add by vikas 31.10.2012** for Scheme Apply on more then one group*************/
			Grouparr = prodarr[7].split(".")
			for(var k=0;k<(Grouparr.length-1);k++)
			{
				if(GType==Grouparr[k])
				{
					Scheme="Yes";
				}
			}
			/*********End*************/
			if(Scheme=="Yes")                 
			{
				if(ProdName==prodarr[2] && PackType==prodarr[3])
				{
					status="y"; 
				}
				else
				{	
					ProdName=prodarr[2]
					PackType=prodarr[3]
					status="n"
				}
				for(var j=0;j<prodarr.length;j++ )
				{ 
					if(prodarr[2]+":"+prodarr[3] == prodtext)
					{
						if(status!="y")
						{
							discountarr[k]=prodarr[4];
							DisTypearr[k]=prodarr[6];
							k++;
						}
					} 
				}
			}
		}
		
		for(n=0;n<discountarr.length;n++)
		{  
			txtschSP.value=discountarr[n]
			tmpSchSPType.value=DisTypearr[n]
		} 
	}
}

function getschemefoeTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1,tmpFoeType)
{
	/***********Start****Add by vikas sharma date on 16.05.09**************/
	var custname1=document.Form1.text1.value
	var custname2=custname1.split(":")
	var custname=custname2[0];
	/***********end******************/
	var customer
	var ProdName
	var PackType 
	var mainarr = new Array()
	var ProdText  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
  	var hidarr  = document.Form1.temptextfoe.value
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var discounttypearr=new Array()
	var status="n"
	var k = 0
	txtfoe1.value=""
	tmpFoeType.value=""
	var name=""
	for(var i=0;i<(mainarr.length-1);i++)
	{
		prodarr = mainarr[i].split(":")
		if(ProdName==prodarr[2]&&PackType==prodarr[3]&& customer==prodarr[6])
		{
			status="y";
		}
		else
		{	
			ProdName=prodarr[2]
			PackType=prodarr[3]
			customer=prodarr[6]
			status="n"
		}
		for(var j=0;j<prodarr.length;j++ )
		{ 
			if(prodarr[2]+":"+prodarr[3]==ProdText && prodarr[6]==custname)
			{
				if(status!="y")
				{
					discountarr[k]=prodarr[4];
					discounttypearr[k]=prodarr[5]; 
					k++;
				}
			} 
		}
		for(var j=0;j<prodarr.length;j++ )
		{
			if(prodarr[2]=='0' && prodarr[3]=='0' && prodarr[6]==custname )
			{ 
				if(status!="y")
				{	
					discountarr[k]=prodarr[4];
					discounttypearr[k]=prodarr[5];
					k++;
				}
			} 
		}
	}
 
	for(n=0;n<discountarr.length;n++)
    {  
		txtfoe1.value = discountarr[n]
		tmpFoeType.value=discounttypearr[n]
		if(txtfoe1.value>0)
		{ 
			txtsch1.value =""
			prodtype1.value =""
			txtQty1.value =""
			txtstk1.value ="" 
		}		
    } 
}