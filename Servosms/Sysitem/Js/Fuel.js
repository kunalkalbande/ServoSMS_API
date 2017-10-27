/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/	
	
 function getTaxRate(t,rate,reduction,entrytax,rpgcharge,rpgsurcharge,ltc,transportcharge,other,lst,lstsurcharge,lfr,txtdo,txtUnit)
 { 
	//alert("in")
	var mainarr = new Array()
	var typeindex = t.selectedIndex
	var typetext  = t.options[typeindex].text
	// alert(countrytext)
	var hidtext  = document.Form1.FuelText.value
	mainarr = hidtext.split("#")
	//alert(cscarr)
	var taxarr = new Array()
	var k = 0
	rate.value=""
	reduction.value=""
	entrytax.value=""
	rpgcharge.value=""
	rpgsurcharge.value=""
	ltc.value=""
	transportcharge.value=""
	other.value=""
	lst.value=""
	lstsurcharge.value=""
	lfr.value=""
	txtdo.value=""
	txtUnit.value = ""
  
	for(var i=0;i<(mainarr.length-1);i++)
	{
		taxarr = mainarr[i].split("~")
		//alert(sarr[i])
		for(var j=0;j<taxarr.length;j++ )
		{  
			if(taxarr[j]==typetext)
			{  //alert("in"+taxarr[2])
				rate.value=taxarr[2];
				reduction.value=taxarr[3];
				entrytax.value=taxarr[4];
				rpgcharge.value=taxarr[5];
				rpgsurcharge.value=taxarr[6];
				ltc.value=taxarr[7];
				transportcharge.value=taxarr[8];
				other.value=taxarr[9];
				lst.value=taxarr[10];
				lstsurcharge.value=taxarr[11];
				lfr.value=taxarr[12];
				txtdo.value=taxarr[13];
				txtUnit.value = taxarr[14]+"#"+taxarr[15]+"#"+taxarr[16]+"#"+taxarr[17]+"#"+taxarr[18]+"#"+taxarr[19]+"#"+taxarr[20]+"#"+taxarr[21]+"#"+taxarr[22]+"#"+taxarr[23]+"#"+taxarr[24];
				break;
			}	
		}
		//alert(txtUnit.value)
	} 
}
 
function getCity(t,city)
{
	var mainarr = new Array()
	var typeindex = t.selectedIndex
	var typetext  = t.options[typeindex].text
	//alert(typetext)
	var hidtext  = document.Form1.TxtVen.value
	// alert(hidtext)
	mainarr = hidtext.split("#")
	var cityarr = new Array()
	city.value=""
  
	for(var i=0;i<(mainarr.length - 1);i++)
	{
		cityarr = mainarr[i].split("~")
		// alert(cityarr.length)
		for(var j=0;j<cityarr.length;j++ )
		{ 
			if(cityarr[j]==typetext)
			{  
				city.value=cityarr[1]         
				// break
			}   
		}    
	}
} 
  
function getBalance(t,City,CR_Days,Balance,CR_Limit,crlimit,vehicleno,SSR)
{ 
		//alert(t.options[t.selectedIndex].text)//+","+City.value+","+CR_Days.value+","+Balance.value+","+CR_Limit.value+","+crlimit.value+","+vehicleno.value+","+SSR.value)



	var mainarr = new Array()
	var vehiclearr = new Array(); 
	var typeindex = t.selectedIndex
	
	//var typetext  = t.options[typeindex].text //Comment by vikas sharma 27.04.09
	
	/**********start* Add by vikas sharma 27.04.09****/
	var typetext1 = t.options[typeindex].text
	var typetext2 = typetext1.split(':')
	var typetext=typetext2[0]
	/***********end*********************************/
	
	var hidtext  = document.Form1.TxtVen.value
	var hidtext1 = document.Form1.lblVehicleNo.value
	// alert("1 : "+hidtext)
	mainarr = hidtext.split("#")
	vehiclearr = hidtext1.split("#");
	//alert(cscarr)
	var taxarr = new Array()
	var subarr = new Array()
	var k = 0
	City.value=""
	CR_Days.value=""
	CR_Limit.value=""
	crlimit.value=""
	vehicleno.value=""
	SSR.value="Select"
	//lblCreditLimit.value = "";
	//Cust_ID.value=""
	Balance.value=""
	//txtstart.value=""
	//txtend.value=""
  
	//alert(document.Form1.TxtVen.value)
	for(var i=0;i<(mainarr.length-1);i++)
	{
		//alert(mainarr[i])
		taxarr = mainarr[i].split("~")
		//alert(sarr[i])
		//**for(var j=0;j<taxarr.length;j++ )
		//**{  
			// alert("2 : "+taxarr[j]+" #### "+typetext)
			//**if(taxarr[j]==typetext)
			if(taxarr[0]==typetext)
			{
				City.value=taxarr[1];
				// alert("3 : "+taxarr[1])
				CR_Days.value=taxarr[2];
				CR_Limit.value=taxarr[3];
				crlimit.value=taxarr[3];
		
				//lblCreditLimit.value = taxarr[3];
				//Cust_ID.value=taxarr[4];
				Balance.value=taxarr[4]+" "+taxarr[5];
				//txtstart.value=taxarr[7]
				//txtend.value=taxarr[8]
				//BalanceType.value=taxarr[6];
				//**********
				SSR.value=taxarr[6]
				//**********
				break;
			}	
		//**}
	} 
}

function getBalance_Cust(t,City)
{ 
	var mainarr = new Array()
	var vehiclearr = new Array(); 
	var typeindex = t.selectedIndex
	/**********start* Add by vikas sharma 27.04.09****/
	var typetext1 = t.options[typeindex].text
	var typetext2 = typetext1.split(':')
	var typetext=typetext2[0]
	/***********end*********************************/
	
	var hidtext  = document.Form1.TxtVen.value
	var hidtext1 = document.Form1.lblVehicleNo.value
	mainarr = hidtext.split("#")
	vehiclearr = hidtext1.split("#");
	var taxarr = new Array()
	var subarr = new Array()
	var k = 0
	City.value=""
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


//function getschemeprimary(prodtype,txtQty,lblInvoiceDate,,txtsch1,tmpSchType)
function getschemeprimary(prodtype,tempschdis)
{
	//alert(tempschdis.value)
	var ProdName
	var PackType 
	var ProdCode
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.temptext12.value
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	var status="n"
	var k = 0
	tempschdis.value=""
	//if(txtQty.value!=0)
	//{
		for(var i=0;i<(mainarr.length);i++)
		{
			prodarr = mainarr[i].split(":")
			if(ProdName==prodarr[2] && PackType==prodarr[3] && ProdCode==prodarr[1])
				status="y"; 
			else
			{	
				ProdName=prodarr[2]
				PackType=prodarr[3]
				ProdCode=prodarr[1]
				status="n"
			}
			for(var j=0;j<prodarr.length;j++ )
			{ 
				if(prodarr[1]+":"+prodarr[2]+":"+prodarr[3] == prodtext)
				{
					if(status!="y")
					{
						discountarr[k]=prodarr[4];
						DisTypearr[k]=prodarr[5];
						k++;
					}
				} 
			}
		}
		for(n=0;n<discountarr.length;n++)
		{  
			//txtsch1.value=discountarr[n]
			//tmpSchType.value=DisTypearr[n]
			tempschdis.value=discountarr[n]+":"+DisTypearr[n]
			//alert(tempschdis.value)
		} 
	//}
}

function getschemeAddprimary(prodtype,tempSchAddDis)
{
	//alert(tempschdis.value)
	var ProdName
	var PackType 
	var ProdCode
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.temptext_add1.value
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	var status="n"
	var k = 0
	tempSchAddDis.value=""
		for(var i=0;i<(mainarr.length);i++)
		{
			prodarr = mainarr[i].split(":")
			if(ProdName==prodarr[2] && PackType==prodarr[3] && ProdCode==prodarr[1])
				status="y"; 
			else
			{	
				ProdName=prodarr[2]
				PackType=prodarr[3]
				ProdCode=prodarr[1]
				status="n"
			}
			for(var j=0;j<prodarr.length;j++ )
			{ 
				if(prodarr[1]+":"+prodarr[2]+":"+prodarr[3] == prodtext)
				{
					if(status!="y")
					{
						discountarr[k]=prodarr[4];
						DisTypearr[k]=prodarr[5];
						k++;
					}
				} 
			}
		}
		for(n=0;n<discountarr.length;n++)
		{  
			tempSchAddDis.value=discountarr[n]+":"+DisTypearr[n]
			//alert(tempschdis.value)
		}
}




function GetStockistScheme(prodtype,stktschdis)
{
	
	var ProdName
	var PackType 
	var ProdCode
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.tempStktSchDis.value
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	var status="n"
	var k = 0
	stktschdis.value=""
	for(var i=0;i<(mainarr.length);i++)
	{
		prodarr = mainarr[i].split(":")
		if(ProdName==prodarr[2] && PackType==prodarr[3] && ProdCode==prodarr[1])
			status="y"; 
		else
		{	
			ProdName=prodarr[2]
			PackType=prodarr[3]
			ProdCode=prodarr[1]
			status="n"
		}
		for(var j=0;j<prodarr.length;j++ )
		{ 
			if(prodarr[1]+":"+prodarr[2]+":"+prodarr[3] == prodtext)
			{
				if(status!="y")
				{
					discountarr[k]=prodarr[4];
					DisTypearr[k]=prodarr[5];
					k++;
				}
			} 
		}
	}
	for(n=0;n<discountarr.length;n++)
	{  
		stktschdis.value=discountarr[n]+":"+DisTypearr[n]
	} 
}

/**************Add by vikas 1.1.2013*******************************************/

function GetFixedDiscount(prodtype,stktschdis)
{
	
	var ProdName
	var PackType 
	var ProdCode
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.tempFixedDisc.value
	//alert(hidarr)
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	var status="n"
	var k = 0
	stktschdis.value=""
	for(var i=0;i<(mainarr.length);i++)
	{
		prodarr = mainarr[i].split(":")
		if(ProdName==prodarr[2] && PackType==prodarr[3] && ProdCode==prodarr[1])
			status="y"; 
		else
		{	
			ProdName=prodarr[2]
			PackType=prodarr[3]
			ProdCode=prodarr[1]
			status="n"
		}
		for(var j=0;j<prodarr.length;j++ )
		{ 
			if(prodarr[1]+":"+prodarr[2]+":"+prodarr[3] == prodtext)
			{
				if(status!="y")
				{
					discountarr[k]=prodarr[4];
					DisTypearr[k]=prodarr[5];
					k++;
				}
			} 
		}
	}
	for(n=0;n<discountarr.length;n++)
	{  
		stktschdis.value=discountarr[n]+":"+DisTypearr[n]
	} 
}

/***************End******************************************/