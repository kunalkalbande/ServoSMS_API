/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
var count=0;		
 function getProdName(t,pname,packtype,avstock,srate,txtProdName,txtPack,txtQty,txtAmount,tempint)
 { 
  txtQty.value=""
  //*********
  if(t.selectedIndex==0)
  {
	if(tempint==1)
		getscheme(document.Form1.DropType1,document.Form1.txtProdName1,document.Form1.txtPack1,document.Form1.txtQty1,document.Form1.txtTypesch1,document.Form1.txtProdsch1,document.Form1.txtPacksch1,document.Form1.txtQtysch1,document.Form1.lblInvoiceDate,document.Form1.txtstk1,document.Form1.txtsch1,document.Form1.txtfoe1)
	if(tempint==2)
		getscheme(document.Form1.DropType2,document.Form1.txtProdName2,document.Form1.txtPack2,document.Form1.txtQty2,document.Form1.txtTypesch2,document.Form1.txtProdsch2,document.Form1.txtPacksch2,document.Form1.txtQtysch2,document.Form1.lblInvoiceDate,document.Form1.txtstk2,document.Form1.txtsch2,document.Form1.txtfoe2)
	if(tempint==3)
		getscheme(document.Form1.DropType3,document.Form1.txtProdName3,document.Form1.txtPack3,document.Form1.txtQty3,document.Form1.txtTypesch3,document.Form1.txtProdsch3,document.Form1.txtPacksch3,document.Form1.txtQtysch3,document.Form1.lblInvoiceDate,document.Form1.txtstk3,document.Form1.txtsch3,document.Form1.txtfoe3)
	if(tempint==4)
		getscheme(document.Form1.DropType4,document.Form1.txtProdName4,document.Form1.txtPack4,document.Form1.txtQty4,document.Form1.txtTypesch4,document.Form1.txtProdsch4,document.Form1.txtPacksch4,document.Form1.txtQtysch4,document.Form1.lblInvoiceDate,document.Form1.txtstk4,document.Form1.txtsch4,document.Form1.txtfoe4)
	if(tempint==5)
		getscheme(document.Form1.DropType5,document.Form1.txtProdName5,document.Form1.txtPack5,document.Form1.txtQty5,document.Form1.txtTypesch5,document.Form1.txtProdsch5,document.Form1.txtPacksch5,document.Form1.txtQtysch5,document.Form1.lblInvoiceDate,document.Form1.txtstk5,document.Form1.txtsch5,document.Form1.txtfoe5)
	if(tempint==6)
		getscheme(document.Form1.DropType6,document.Form1.txtProdName6,document.Form1.txtPack6,document.Form1.txtQty6,document.Form1.txtTypesch6,document.Form1.txtProdsch6,document.Form1.txtPacksch6,document.Form1.txtQtysch6,document.Form1.lblInvoiceDate,document.Form1.txtstk6,document.Form1.txtsch6,document.Form1.txtfoe6)
	if(tempint==7)
		getscheme(document.Form1.DropType7,document.Form1.txtProdName7,document.Form1.txtPack7,document.Form1.txtQty7,document.Form1.txtTypesch7,document.Form1.txtProdsch7,document.Form1.txtPacksch7,document.Form1.txtQtysch7,document.Form1.lblInvoiceDate,document.Form1.txtstk7,document.Form1.txtsch7,document.Form1.txtfoe7)
	if(tempint==8)
		getscheme(document.Form1.DropType8,document.Form1.txtProdName8,document.Form1.txtPack8,document.Form1.txtQty8,document.Form1.txtTypesch8,document.Form1.txtProdsch8,document.Form1.txtPacksch8,document.Form1.txtQtysch8,document.Form1.lblInvoiceDate,document.Form1.txtstk8,document.Form1.txtsch8,document.Form1.txtfoe8)
	if(tempint==9)
		getscheme(document.Form1.DropType9,document.Form1.txtProdName9,document.Form1.txtPack9,document.Form1.txtQty9,document.Form1.txtTypesch9,document.Form1.txtProdsch9,document.Form1.txtPacksch9,document.Form1.txtQtysch9,document.Form1.lblInvoiceDate,document.Form1.txtstk9,document.Form1.txtsch9,document.Form1.txtfoe9)
	if(tempint==10)
		getscheme(document.Form1.DropType10,document.Form1.txtProdName10,document.Form1.txtPack10,document.Form1.txtQty10,document.Form1.txtTypesch10,document.Form1.txtProdsch10,document.Form1.txtPacksch10,document.Form1.txtQtysch10,document.Form1.lblInvoiceDate,document.Form1.txtstk10,document.Form1.txtsch10,document.Form1.txtfoe10)
	if(tempint==11)
		getscheme(document.Form1.DropType11,document.Form1.txtProdName11,document.Form1.txtPack11,document.Form1.txtQty11,document.Form1.txtTypesch11,document.Form1.txtProdsch11,document.Form1.txtPacksch11,document.Form1.txtQtysch11,document.Form1.lblInvoiceDate,document.Form1.txtstk11,document.Form1.txtsch11,document.Form1.txtfoe11)
	if(tempint==12)
		getscheme(document.Form1.DropType12,document.Form1.txtProdName12,document.Form1.txtPack12,document.Form1.txtQty12,document.Form1.txtTypesch12,document.Form1.txtProdsch12,document.Form1.txtPacksch12,document.Form1.txtQtysch12,document.Form1.lblInvoiceDate,document.Form1.txtstk12,document.Form1.txtsch12,document.Form1.txtfoe12)
  }
	  
  //************
  var ProdName 
  var mainarr = new Array()
  var typeindex = t.selectedIndex
  var typetext  = t.options[typeindex].text
  
  
  var hidarr  = document.Form1.temptext.value
  mainarr = hidarr.split(",")
  //alert(cscarr)
  var prodarr = new Array()
  var prodnamearr = new Array()
  
  var status="n"
  var k = 0
  var r =0;
  pname.length    = 1
  packtype.length = 1
  pname.value     ="Select"
  packtype.value  ="Select"
  
  srate.value=""
  avstock.value=""
 
	// alert("Enter")
  txtAmount.value=""
	// alert("Enter33")
  txtProdName.value=""
  
  txtPack.value=""
 //txtsch.value="" 
  for(var i=0;i<(mainarr.length-1);i++)
  {
    prodarr = mainarr[i].split(":")
    //alert(sarr[i])
    if(ProdName==prodarr[1])
    {
 		status="y"; 
    }
    else
    {	
		ProdName=prodarr[1]
		status="n"
    }
    for(var j=0;j<prodarr.length;j++ )
    {  
      if(prodarr[j]==typetext)
      {
		if(status!="y")
		{ 
			prodnamearr[k]=prodarr[1];
			k++;
		}
      } 
    }
  }
    for(n=0;n<prodnamearr.length;n++)
     {  
        pname.add(new Option)  
        pname.options[n+1].text=prodnamearr[n]
        pname.options[n+1].value=prodnamearr[n]
     }	 
 }

function getPack(prodtype,t,packtype,avstock,srate,prodname,txtPack,txtQty,txtAmount,tempint)
 { 
	txtQty.value=""
	//*********
	if( t.selectedIndex==0)
	{
		if(tempint==1)
			getscheme(document.Form1.DropType1,document.Form1.txtProdName1,document.Form1.txtPack1,document.Form1.txtQty1,document.Form1.txtTypesch1,document.Form1.txtProdsch1,document.Form1.txtPacksch1,document.Form1.txtQtysch1,document.Form1.lblInvoiceDate,document.Form1.txtstk1,document.Form1.txtsch1,document.Form1.txtfoe1)
		if(tempint==2)
			getscheme(document.Form1.DropType2,document.Form1.txtProdName2,document.Form1.txtPack2,document.Form1.txtQty2,document.Form1.txtTypesch2,document.Form1.txtProdsch2,document.Form1.txtPacksch2,document.Form1.txtQtysch2,document.Form1.lblInvoiceDate,document.Form1.txtstk2,document.Form1.txtsch2,document.Form1.txtfoe2)
		if(tempint==3)
			getscheme(document.Form1.DropType3,document.Form1.txtProdName3,document.Form1.txtPack3,document.Form1.txtQty3,document.Form1.txtTypesch3,document.Form1.txtProdsch3,document.Form1.txtPacksch3,document.Form1.txtQtysch3,document.Form1.lblInvoiceDate,document.Form1.txtstk3,document.Form1.txtsch3,document.Form1.txtfoe3)
		if(tempint==4)
			getscheme(document.Form1.DropType4,document.Form1.txtProdName4,document.Form1.txtPack4,document.Form1.txtQty4,document.Form1.txtTypesch4,document.Form1.txtProdsch4,document.Form1.txtPacksch4,document.Form1.txtQtysch4,document.Form1.lblInvoiceDate,document.Form1.txtstk4,document.Form1.txtsch4,document.Form1.txtfoe4)
		if(tempint==5)
			getscheme(document.Form1.DropType5,document.Form1.txtProdName5,document.Form1.txtPack5,document.Form1.txtQty5,document.Form1.txtTypesch5,document.Form1.txtProdsch5,document.Form1.txtPacksch5,document.Form1.txtQtysch5,document.Form1.lblInvoiceDate,document.Form1.txtstk5,document.Form1.txtsch5,document.Form1.txtfoe5)
		if(tempint==6)
			getscheme(document.Form1.DropType6,document.Form1.txtProdName6,document.Form1.txtPack6,document.Form1.txtQty6,document.Form1.txtTypesch6,document.Form1.txtProdsch6,document.Form1.txtPacksch6,document.Form1.txtQtysch6,document.Form1.lblInvoiceDate,document.Form1.txtstk6,document.Form1.txtsch6,document.Form1.txtfoe6)
		if(tempint==7)
			getscheme(document.Form1.DropType7,document.Form1.txtProdName7,document.Form1.txtPack7,document.Form1.txtQty7,document.Form1.txtTypesch7,document.Form1.txtProdsch7,document.Form1.txtPacksch7,document.Form1.txtQtysch7,document.Form1.lblInvoiceDate,document.Form1.txtstk7,document.Form1.txtsch7,document.Form1.txtfoe7)
		if(tempint==8)
			getscheme(document.Form1.DropType8,document.Form1.txtProdName8,document.Form1.txtPack8,document.Form1.txtQty8,document.Form1.txtTypesch8,document.Form1.txtProdsch8,document.Form1.txtPacksch8,document.Form1.txtQtysch8,document.Form1.lblInvoiceDate,document.Form1.txtstk8,document.Form1.txtsch8,document.Form1.txtfoe8)
		if(tempint==9)
			getscheme(document.Form1.DropType9,document.Form1.txtProdName9,document.Form1.txtPack9,document.Form1.txtQty9,document.Form1.txtTypesch9,document.Form1.txtProdsch9,document.Form1.txtPacksch9,document.Form1.txtQtysch9,document.Form1.lblInvoiceDate,document.Form1.txtstk9,document.Form1.txtsch9,document.Form1.txtfoe9)
		if(tempint==10)
			getscheme(document.Form1.DropType10,document.Form1.txtProdName10,document.Form1.txtPack10,document.Form1.txtQty10,document.Form1.txtTypesch10,document.Form1.txtProdsch10,document.Form1.txtPacksch10,document.Form1.txtQtysch10,document.Form1.lblInvoiceDate,document.Form1.txtstk10,document.Form1.txtsch10,document.Form1.txtfoe10)
		if(tempint==11)
			getscheme(document.Form1.DropType11,document.Form1.txtProdName11,document.Form1.txtPack11,document.Form1.txtQty11,document.Form1.txtTypesch11,document.Form1.txtProdsch11,document.Form1.txtPacksch11,document.Form1.txtQtysch11,document.Form1.lblInvoiceDate,document.Form1.txtstk11,document.Form1.txtsch11,document.Form1.txtfoe11)
		if(tempint==12)
			getscheme(document.Form1.DropType12,document.Form1.txtProdName12,document.Form1.txtPack12,document.Form1.txtQty12,document.Form1.txtTypesch12,document.Form1.txtProdsch12,document.Form1.txtPacksch12,document.Form1.txtQtysch12,document.Form1.lblInvoiceDate,document.Form1.txtstk12,document.Form1.txtsch12,document.Form1.txtfoe12)
	}
  //alert("packtype")
  // getschemefoe(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
  //************
var index1 = prodtype.selectedIndex
var ptype = prodtype.options[index1].text
	
 if(ptype=="Fuel")
  {
   packtype.disabled = true;
   getStock(prodtype,t,packtype,avstock,srate,prodname,txtQty,txtAmount);
  }
  else
  packtype.disabled = false; 
  
	packtype.length=1	
	packtype.value="";
	packtype.selectedIndex = 0;
	
	// srate.value=""
     //avstock.value=""
     
  txtAmount.value=""
  txtPack.value="" 
	prodname.value=""
	//txtsch.value=""
	var index = t.selectedIndex
	var Prod = t.options[index].text
	
    prodname.value=Prod
	var mainarr = new Array()
	var parr = new Array()
	var k=0
	var packarray = new Array()
	var hiddenarr  = document.Form1.temptext.value
	mainarr = hiddenarr.split(",")
	for(var i=0;i<(mainarr.length-1);i++)
	{
    parr = mainarr[i].split(":")
    for(var j=0;j<parr.length;j++ )
    {  
      if(parr[j]==Prod)
      {
        packarray[k]=parr[j+1]
        k++;
      } 
    }
  }
     for(n=0;n<packarray.length;n++)
     {
        packtype.add(new Option)  
        packtype.options[n+1].text=packarray[n]
        packtype.options[n+1].value=packarray[n]
     }
   //  getStock(prodtype,t,packtype,avstock,srate)
 }
 
// function getStock(prodtype,pname,t,avstock,srate,packname,txtQty,txtAmount,txtsch)
function getStock(prodtype,pname,t,avstock,srate,packname,txtQty,txtAmount,tempint)
 {
  txtQty.value=""
 //*********
  if( t.selectedIndex==0)
  {
  if(tempint==1)
	  getscheme(document.Form1.DropType1,document.Form1.txtProdName1,document.Form1.txtPack1,document.Form1.txtQty1,document.Form1.txtTypesch1,document.Form1.txtProdsch1,document.Form1.txtPacksch1,document.Form1.txtQtysch1,document.Form1.lblInvoiceDate,document.Form1.txtstk1,document.Form1.txtsch1,document.Form1.txtfoe1)
  if(tempint==2)
  getscheme(document.Form1.DropType2,document.Form1.txtProdName2,document.Form1.txtPack2,document.Form1.txtQty2,document.Form1.txtTypesch2,document.Form1.txtProdsch2,document.Form1.txtPacksch2,document.Form1.txtQtysch2,document.Form1.lblInvoiceDate,document.Form1.txtstk2,document.Form1.txtsch2,document.Form1.txtfoe2)
  if(tempint==3)
  getscheme(document.Form1.DropType3,document.Form1.txtProdName3,document.Form1.txtPack3,document.Form1.txtQty3,document.Form1.txtTypesch3,document.Form1.txtProdsch3,document.Form1.txtPacksch3,document.Form1.txtQtysch3,document.Form1.lblInvoiceDate,document.Form1.txtstk3,document.Form1.txtsch3,document.Form1.txtfoe3)
 if(tempint==4)
 getscheme(document.Form1.DropType4,document.Form1.txtProdName4,document.Form1.txtPack4,document.Form1.txtQty4,document.Form1.txtTypesch4,document.Form1.txtProdsch4,document.Form1.txtPacksch4,document.Form1.txtQtysch4,document.Form1.lblInvoiceDate,document.Form1.txtstk4,document.Form1.txtsch4,document.Form1.txtfoe4)
  if(tempint==5)
  getscheme(document.Form1.DropType5,document.Form1.txtProdName5,document.Form1.txtPack5,document.Form1.txtQty5,document.Form1.txtTypesch5,document.Form1.txtProdsch5,document.Form1.txtPacksch5,document.Form1.txtQtysch5,document.Form1.lblInvoiceDate,document.Form1.txtstk5,document.Form1.txtsch5,document.Form1.txtfoe5)
  if(tempint==6)
  getscheme(document.Form1.DropType6,document.Form1.txtProdName6,document.Form1.txtPack6,document.Form1.txtQty6,document.Form1.txtTypesch6,document.Form1.txtProdsch6,document.Form1.txtPacksch6,document.Form1.txtQtysch6,document.Form1.lblInvoiceDate,document.Form1.txtstk6,document.Form1.txtsch6,document.Form1.txtfoe6)
 if(tempint==7)
 getscheme(document.Form1.DropType7,document.Form1.txtProdName7,document.Form1.txtPack7,document.Form1.txtQty7,document.Form1.txtTypesch7,document.Form1.txtProdsch7,document.Form1.txtPacksch7,document.Form1.txtQtysch7,document.Form1.lblInvoiceDate,document.Form1.txtstk7,document.Form1.txtsch7,document.Form1.txtfoe7)
 if(tempint==8)
 getscheme(document.Form1.DropType8,document.Form1.txtProdName8,document.Form1.txtPack8,document.Form1.txtQty8,document.Form1.txtTypesch8,document.Form1.txtProdsch8,document.Form1.txtPacksch8,document.Form1.txtQtysch8,document.Form1.lblInvoiceDate,document.Form1.txtstk8,document.Form1.txtsch8,document.Form1.txtfoe8)
  
  if(tempint==9)
 getscheme(document.Form1.DropType9,document.Form1.txtProdName9,document.Form1.txtPack9,document.Form1.txtQty9,document.Form1.txtTypesch9,document.Form1.txtProdsch9,document.Form1.txtPacksch9,document.Form1.txtQtysch9,document.Form1.lblInvoiceDate,document.Form1.txtstk9,document.Form1.txtsch9,document.Form1.txtfoe9)
 if(tempint==10)
 getscheme(document.Form1.DropType10,document.Form1.txtProdName10,document.Form1.txtPack10,document.Form1.txtQty10,document.Form1.txtTypesch10,document.Form1.txtProdsch10,document.Form1.txtPacksch10,document.Form1.txtQtysch10,document.Form1.lblInvoiceDate,document.Form1.txtstk10,document.Form1.txtsch10,document.Form1.txtfoe10)
 if(tempint==11)
 getscheme(document.Form1.DropType11,document.Form1.txtProdName11,document.Form1.txtPack11,document.Form1.txtQty11,document.Form1.txtTypesch11,document.Form1.txtProdsch11,document.Form1.txtPacksch11,document.Form1.txtQtysch11,document.Form1.lblInvoiceDate,document.Form1.txtstk11,document.Form1.txtsch11,document.Form1.txtfoe11)
 if(tempint==12)
 getscheme(document.Form1.DropType12,document.Form1.txtProdName12,document.Form1.txtPack12,document.Form1.txtQty12,document.Form1.txtTypesch12,document.Form1.txtProdsch12,document.Form1.txtPacksch12,document.Form1.txtQtysch12,document.Form1.lblInvoiceDate,document.Form1.txtstk12,document.Form1.txtsch12,document.Form1.txtfoe12)
  }
  //************
 if (!checkProd())
 {
 prodtype.selectedIndex = 0;
 pname.selectedIndex=0;
 t.length=1;
 t.selectedIndex=0;
 return false;
 
 }
  var ProdName
  var PackType
 
  var mainarr = new Array()
  var prodindex = pname.selectedIndex
  var prodtext  = pname.options[prodindex].text
  
  var packindex = t.selectedIndex
//  alert(packindex)
  var packtext;
  if(packindex==-1)
  packtext="";
  else
  var packtext  = t.options[packindex].text
  packname.value=packtext
  // alert(countrytext)
  var hidarr  = document.Form1.temptext.value
  mainarr = hidarr.split(",")
  //alert(cscarr)
  var prodarr = new Array()
  var ratearr = new Array()
  var stockarr = new Array()
  var unitarr = new Array()
  var schemearr = new Array()
  var status="n"
  var k = 0
  var r =0;
   srate.value=""
  avstock.value=""
  
  txtAmount.value=""
// txtsch.value=""
  for(var i=0;i<(mainarr.length-1);i++)
  {
    prodarr = mainarr[i].split(":")
    //alert(sarr[i])
   
    if(ProdName==prodarr[1]&&PackType==prodarr[2])
    {
 		status="y"; 
    }
    else
    {	
	ProdName=prodarr[1]
	PackType=prodarr[2]
	status="n"
    }
    for(var j=0;j<prodarr.length;j++ )
    { 
    
      if(prodarr[1]==prodtext&& prodarr[2]==packtext)
      {
	if(status!="y")
	{// alert(prodarr[3]+" "+prodarr[4]+" "+prodarr[5])
	//alert("in sta ")
	   ratearr[k] = prodarr[3];
	   stockarr[k]= prodarr[4];
	   unitarr[k]=  prodarr[5];
	   schemearr[k]=prodarr[6];//open comment by mahesh,date : 4.4.007
	   //alert(schemearr[k])
       k++;
	}
      } 
    }
  }
   for(n=0;n<ratearr.length;n++)
     {  
     //alert("for")
        srate.value=ratearr[n]
        avstock.value=stockarr[n]+" "+unitarr[n]
        //txtsch.value=schemearr[n]//open comment by mahesh,date : 4.4.007
     } 
	
 }

function getStock1(t,avstock,srate,txtQty,txtAmount,tempint)
{
	txtQty.value=""
	if( t.value=="" || t.value=="Select")
	{
		if(tempint==1)
			getscheme2(document.Form1.DropType1,document.Form1.txtQty1,document.Form1.txtTypesch1,document.Form1.txtQtysch1,document.Form1.lblInvoiceDate,document.Form1.txtstk1,document.Form1.txtsch1,document.Form1.txtfoe1,document.Form1.tmpSchType1,document.Form1.txtTempSecSP1,document.Form1.tmpSecSPType1,document.Form1.tmpFoeType1)
		if(tempint==2)
			getscheme2(document.Form1.DropType2,document.Form1.txtQty2,document.Form1.txtTypesch2,document.Form1.txtQtysch2,document.Form1.lblInvoiceDate,document.Form1.txtstk2,document.Form1.txtsch2,document.Form1.txtfoe2,document.Form1.tmpSchType2,document.Form1.txtTempSecSP2,document.Form1.tmpSecSPType2,document.Form1.tmpFoeType2)
		if(tempint==3)
			getscheme2(document.Form1.DropType3,document.Form1.txtQty3,document.Form1.txtTypesch3,document.Form1.txtQtysch3,document.Form1.lblInvoiceDate,document.Form1.txtstk3,document.Form1.txtsch3,document.Form1.txtfoe3,document.Form1.tmpSchType3,document.Form1.txtTempSecSP3,document.Form1.tmpSecSPType3,document.Form1.tmpFoeType3)
		if(tempint==4)
			getscheme2(document.Form1.DropType4,document.Form1.txtQty4,document.Form1.txtTypesch4,document.Form1.txtQtysch4,document.Form1.lblInvoiceDate,document.Form1.txtstk4,document.Form1.txtsch4,document.Form1.txtfoe4,document.Form1.tmpSchType4,document.Form1.txtTempSecSP4,document.Form1.tmpSecSPType4,document.Form1.tmpFoeType4)
		if(tempint==5)
			getscheme2(document.Form1.DropType5,document.Form1.txtQty5,document.Form1.txtTypesch5,document.Form1.txtQtysch5,document.Form1.lblInvoiceDate,document.Form1.txtstk5,document.Form1.txtsch5,document.Form1.txtfoe5,document.Form1.tmpSchType5,document.Form1.txtTempSecSP5,document.Form1.tmpSecSPType5,document.Form1.tmpFoeType5)
		if(tempint==6)
			getscheme2(document.Form1.DropType6,document.Form1.txtQty6,document.Form1.txtTypesch6,document.Form1.txtQtysch6,document.Form1.lblInvoiceDate,document.Form1.txtstk6,document.Form1.txtsch6,document.Form1.txtfoe6,document.Form1.tmpSchType6,document.Form1.txtTempSecSP6,document.Form1.tmpSecSPType6,document.Form1.tmpFoeType6)
		if(tempint==7)
			getscheme2(document.Form1.DropType7,document.Form1.txtQty7,document.Form1.txtTypesch7,document.Form1.txtQtysch7,document.Form1.lblInvoiceDate,document.Form1.txtstk7,document.Form1.txtsch7,document.Form1.txtfoe7,document.Form1.tmpSchType7,document.Form1.txtTempSecSP7,document.Form1.tmpSecSPType7,document.Form1.tmpFoeType7)
		if(tempint==8)
			getscheme2(document.Form1.DropType8,document.Form1.txtQty8,document.Form1.txtTypesch8,document.Form1.txtQtysch8,document.Form1.lblInvoiceDate,document.Form1.txtstk8,document.Form1.txtsch8,document.Form1.txtfoe8,document.Form1.tmpSchType8,document.Form1.txtTempSecSP8,document.Form1.tmpSecSPType8,document.Form1.tmpFoeType8)
		if(tempint==9)
			getscheme2(document.Form1.DropType9,document.Form1.txtQty9,document.Form1.txtTypesch9,document.Form1.txtQtysch9,document.Form1.lblInvoiceDate,document.Form1.txtstk9,document.Form1.txtsch9,document.Form1.txtfoe9,document.Form1.tmpSchType9,document.Form1.txtTempSecSP9,document.Form1.tmpSecSPType9,document.Form1.tmpFoeType9)
		if(tempint==10)
			getscheme2(document.Form1.DropType10,document.Form1.txtQty10,document.Form1.txtTypesch10,document.Form1.txtQtysch10,document.Form1.lblInvoiceDate,document.Form1.txtstk10,document.Form1.txtsch10,document.Form1.txtfoe10,document.Form1.tmpSchType10,document.Form1.txtTempSecSP10,document.Form1.tmpSecSPType10,document.Form1.tmpFoeType10)
		if(tempint==11)
			getscheme2(document.Form1.DropType11,document.Form1.txtQty11,document.Form1.txtTypesch11,document.Form1.txtQtysch11,document.Form1.lblInvoiceDate,document.Form1.txtstk11,document.Form1.txtsch11,document.Form1.txtfoe11,document.Form1.tmpSchType11,document.Form1.txtTempSecSP11,document.Form1.tmpSecSPType11,document.Form1.tmpFoeType11)
		if(tempint==12)
			getscheme2(document.Form1.DropType12,document.Form1.txtQty12,document.Form1.txtTypesch12,document.Form1.txtQtysch12,document.Form1.lblInvoiceDate,document.Form1.txtstk12,document.Form1.txtsch12,document.Form1.txtfoe12,document.Form1.tmpSchType12,document.Form1.txtTempSecSP12,document.Form1.tmpSecSPType12,document.Form1.tmpFoeType12)
	}
		
	if(!checkProd())
	{
		t.value="Type";
		return false;
	}
	var ProdName
	var PackType
	var mainarr = new Array()
	var packtext;
	var packindex
	//packtext = t.value   //By Vikas Sharma 21.04.09 
	 
	/*************** Add By Vikas Sharma 21.04.09 **********************/
	var packtext1;
	var packtext2;
	packtext1  = t.value
	packtext2=packtext1.split(":")
	packtext=packtext2[1]+":"+packtext2[2]
	/**************** end *********************/
	
	var hidarr  = document.Form1.temptext.value
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var ratearr = new Array()
	var stockarr = new Array()
	var unitarr = new Array()
	var schemearr = new Array()
	var status="n"
	var k = 0
	var r =0;
	srate.value=""
	avstock.value=""
	txtAmount.value=""
	for(var i=0;i<(mainarr.length-1);i++)
	{
		prodarr = mainarr[i].split(":")
		if(ProdName==prodarr[1]&&PackType==prodarr[2])
		{
 			status="y"; 
		}
		else
		{	
			ProdName=prodarr[1]
			PackType=prodarr[2]
			status="n"
		}
		for(var j=0;j<prodarr.length;j++ )
		{ 
			if(prodarr[1]+":"+prodarr[2]==packtext)
			{
				if(status!="y")
				{
					ratearr[k] = prodarr[3];
					stockarr[k]= prodarr[4];
					unitarr[k]=  prodarr[5];
					schemearr[k]=prodarr[6]; //open comment by mahesh,date : 4.4.007
					k++;
					break;
				}
			} 
		}
		if(k>0)
			break;
	}
	for(n=0;n<ratearr.length;n++)
	{  
        srate.value=ratearr[n]
        avstock.value=stockarr[n]+" "+unitarr[n]
    } 
}

function getscheme(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
{
  var ProdName
  var PackType 
  var mainarr = new Array()
  var CustFOE = new Array()
  var prodtext  = pname.value
   //alert("Ok")
  getschemeprimary(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1);
  //getschemefoe1(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
  var packindex;
  var packtext;
  var count1=0;
  var packtext  = packname.value
  packname.value=packtext

  var hidarr  = document.Form1.temptext11.value
  var FOE = document.Form1.temptext13.value
  
  mainarr = hidarr.split(",")
  //****Mahesh
  var custname=document.Form1.DropCustName.value
  var customer
  var index = document.Form1.DropCustName.selectedIndex
  var custname  = document.Form1.DropCustName.options[index].text
  //alert(custname)
  CustFOE = FOE.split(",")
  //****
  var prodarr = new Array()
  var ratearr = new Array()
  var stockarr = new Array()
  var unitarr = new Array()
  var schemearr = new Array()
  var avlstkarr=new Array()
  var avlstk=new Array()
  var discountarr=new Array()
 var dateInv=lblInvoiceDate.value
  var status="n"
  var k = 0
  var r =0;
  
  prodtype1.value=""
  pname1.value=""
  packname1.value=""
  txtQty1.value=""
  txtstk1.value=""
  txtfoe1.value=""
  //txtsch1.value=""
if(txtQty.value!=0)
{
  for(var i=0;i<(mainarr.length-1);i++)
  {
    prodarr = mainarr[i].split(":")
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
    for(var j=0;j<prodarr.length;j++ )
    { 
     if(prodarr[2] == prodtext && prodarr[3]==packtext)
      {
	if(status!="y")
	{
	  stockarr[k]= prodarr[4];
	  unitarr[k]=  prodarr[5];
	  ratearr[k] = prodarr[6];
	   //alert(prodarr[6])
	   var s1=prodarr[7];
	   var s2=prodarr[8]
	   var dd;
	   //alert(prodarr[7]+" "+prodarr[8]+" "+txtQty.value)
	   //schemearr[k]=parseInt(txtQty.value/s1*s2);
	   dd=txtQty.value%s1
	   schemearr[k]=((txtQty.value-dd)/s1)*s2
	   //alert(dd+" "+schemearr[k])
	   if(prodarr[14]=="Primary(Free Scheme)" || prodarr[14]=="Secondry(Free Scheme)")
	   {
	   
	   avlstkarr[k]=prodarr[11]+" "+prodarr[12];
	   //alert(prodarr[11]+" "+prodarr[12])
	   avlstk[k]=prodarr[11];
	   }
	   else
	   avlstkarr[k]="";
	   //if(prodarr[14]!="Secondry")
	  // discountarr[k]=prodarr[13];
	  // else
	  // discountarr[k]="";
	   k++;
	}
      } 
    }
  }
  getschemefoe1(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
  //alert(txtfoe1.value)
  
//************
for(var i=0;i<(CustFOE.length);i++)
{
	//alert(custname+" "+CustFOE[i]+" Enter")
	if(CustFOE[i]==custname)
	{
		//alert(custname+" Enter "+CustFOE[i])
		txtsch1.value =""
	}
}
//**************
  for(n=0;n<ratearr.length;n++)
  {  
     prodtype1.value=stockarr[n]
     if(prodtype1.value==0)
     prodtype1.value=""
	pname1.value=unitarr[n]
	if(pname1.value==0)
  pname1.value=""
  packname1.value=ratearr[n]
 if(packname1.value==0)
 packname1.value=""
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
  {     txtsch1.value =""
		prodtype1.value =""
		pname1.value =""
		packname1.value =""
		txtQty1.value =""
		txtstk1.value =""
  }
  else
  {
  alert("Insufficient Free Stock");
  }
  txtQty1.value="";

  //count++;
  break;
  
  }
  
  
     } 

  }
   
}

/*var combotot=0;
function getscheme2(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1,tmpSchType,txtschSP,tmpSchSPType,tmpFoeType)
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
	
	//var custname=document.Form1.text1.value
	/***********Start****Add by vikas sharma date on 16.05.09**************
	var custname1=document.Form1.text1.value
	var custname2=custname1.split(":")
	var custname=custname2[0];
	/***********end******************
	
	
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
	
	var gnamearr=new Array()						//add by vikas 25.10.2012
	var schemuntarr=new Array()						//add by vikas 25.10.2012
	var GType=document.Form1.tempCustGroup.value;	//add by vikas 25.10.2012
	
	var Grouparr=new Array()						//add by vikas 31.10.2012
	var Scheme;										//add by vikas 31.10.2012
	var totcomboarr=new Array()
	var flage=0;									//add by vikas 8.11.2012
	
	var t1=0,t2=0,t3=0,t4=0,t5=0,t6=0;              //add by vikas 9.11.2012
	var t7=0,t8=0,t9=0,t10=0,t11=0,t12=0;           //add by vikas 9.11.2012
	
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
			
			/*********Add by vikas 31.10.2012** for Scheme Apply on more then group*************
			Grouparr = prodarr[15].split(".")
			for(var k=0;k<(Grouparr.length-1);k++)
			{
				if(GType==Grouparr[k])
				{
					Scheme="Yes";
				}
			}
			
			//coment by vikas 31.10.2012 if(prodarr[15]==GType)
			if(Scheme=="Yes")                       //add by vikas 31.10.2012
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
									
									//alert(prodarr[2]+" : "+prodarr[3]+" : "+prodarr[17])
									
									if(prodarr[17] != "Combo")              //this condition add by vikas 8.11.2012
									{
										
										//alert(prodarr[4]+" : "+prodarr[5]+" : "+prodarr[6])
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
										
										break;	
									}	
									else
									{
											/*********add by vikas 8.11.2012***********************
											Scheme_Packtype="Combo";
												var Type = new Array()
											if(txtQty.name=="txtQty1")
											{
												
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo1.value=totqty
												document.Form1.tempschID1.value=prodarr[18]
												//alert(document.Form1.tempschID1.value+" = "+prodarr[18])
											}
											else if(txtQty.name=="txtQty2")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo2.value=totqty
												document.Form1.tempschID2.value=prodarr[18]
												//alert(document.Form1.tempschID2.value+" = "+prodarr[18])
											}
											else if(txtQty.name=="txtQty3")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo3.value=totqty
												document.Form1.tempschID3.value=prodarr[18]
												//alert(document.Form1.tempschID3.value+" = "+prodarr[18])
											}
											else if(txtQty.name=="txtQty4")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo4.value=totqty
												document.Form1.tempschID4.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty5")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo5.value=totqty
												document.Form1.tempschID5.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty6")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo6.value=totqty
												document.Form1.tempschID6.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty7")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo7.value=totqty
												document.Form1.tempschID7.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty8")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo8.value=totqty
												document.Form1.tempschID8.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty9")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo9.value=totqty
												document.Form1.tempschID9.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty10")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo10.value=totqty
												document.Form1.tempschID10.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty11")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo11.value=totqty
												document.Form1.tempschID11.value=prodarr[18]
											}
											else if(txtQty.name=="txtQty12")
											{
												Type=prodarr[3].split("X")
												totqty=Type[0]*Type[1]*txtQty.value
												document.Form1.temcombo12.value=totqty
												document.Form1.tempschID12.value=prodarr[18]
											}
											//9.11.2012 var t1=0,t2=0,t3=0,t4=0,t5=0,t6=0;
											//9.11.2012 var t7=0,t8=0,t9=0,t10=0,t11=0,t12=0;
											
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
												alert("Hello")
											//alert(eval(t1)+" : "+eval(t2)+" : "+eval(t3)+" : "+eval(t4)+" : "+eval(t5)+" : "+eval(t6)+" : "+eval(t7)+" : "+eval(t8)+" : "+eval(t9)+" : "+eval(t10)+" : "+eval(t11)+" : "+eval(t12))
											//25.7.2013 combotot=eval(t1)+eval(t2)+eval(t3)+eval(t4)+eval(t5)+eval(t6)+eval(t7)+eval(t8)+eval(t9)+eval(t10)+eval(t11)+eval(t12)
											/************25.7.2013*******************
											<% 
												for(int k=1; k<=12; k++) 
												{
													%>
													if(document.Form1.txtQty<%=k%>.value!="")
													{
														if(document.Form1.tempschID12.value==prodarr[18])
														{
															combotot=combotot+eval(document.Form1.temcombo<%=k%>.value);
														}
													}
													<%
												}
											%>
											************end*******************
											********************************
											Scheme_Packtype="Combo";
											var Type = new Array()
											Type=prodarr[3].split("X")
											totqty=Type[0]*Type[1]*txtQty.value
											combotot+=totqty;*********
											
											flage=1;
											var a1=prodarr[7];
											var a2=prodarr[8]
											var dd1;
											dd1=combotot%a1
											//alert(a1+" - "+a2+" - "+dd1)
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
											
											*********End***********************
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
								
								break;
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
				//txtsch1.value =""
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
				
				//coment by vikas 9.11.2012 for(n=2;n<ratearr.length;n++)
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
								alert("Insufficient Free Stock");
							}
							break;
						}
					}
					******if(txtQty1.name != "txtQty1")
					{
						document.Form1.txtTypesch1.value=unitarr[n]+":"+ratearr[n]
						document.Form1.txtQtysch1.value=schemearr[n]
						if(avlstk[n]!= 0)
						{
							document.Form1.txtstk1.value=avlstkarr[n]
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
								alert("Insufficient Free Stock");
							}
							break;
						}
					}
					else	
					{
						prodtype1.value=""
						txtQty1.value=""
						txtstk1.value=""
					}*****
				}
			}
			else
			{
				
				//coment by vikas 9.11.2012 for(n=2;n<ratearr.length;n++)
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
								alert("Insufficient Free Stock");
							}
							break;
						}
					}
					else
					{
								prodtype1.value =""
								txtQty1.value =""
								txtstk1.value =""
					}
					
				}
			}
		} 
		
	}
}*/

function getscheme1(pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
{

  var ProdName
  var PackType 
  var mainarr = new Array()
  var CustFOE = new Array()
  var prodtext  = pname.value
  getschemeprimary(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1);
  var packindex;
  var packtext;
  var count1=0;
  var packtext  = packname.value
  packname.value=packtext

  var hidarr  = document.Form1.temptext11.value
  var FOE = document.Form1.temptext13.value
  mainarr = hidarr.split(",")
  
  var custname=document.Form1.DropCustName.value
  
  var customer
  var index = document.Form1.DropCustName.selectedIndex
  var custname  = document.Form1.DropCustName.options[index].text
  CustFOE = FOE.split(",")
  var prodarr = new Array()
  var ratearr = new Array()
  var stockarr = new Array()
  var unitarr = new Array()
  var schemearr = new Array()
  var avlstkarr=new Array()
  var avlstk=new Array()
  var discountarr=new Array()
 var dateInv=lblInvoiceDate.value
  var status="n"
  var k = 0
  var r =0;
  
  prodtype1.value=""
  pname1.value=""
  packname1.value=""
  txtQty1.value=""
  txtstk1.value=""
  txtfoe1.value=""
if(txtQty.value!=0)
{
  for(var i=0;i<(mainarr.length-1);i++)
  {
    prodarr = mainarr[i].split(":")
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
    for(var j=0;j<prodarr.length;j++ )
    { 
     if(prodarr[2] == prodtext && prodarr[3]==packtext)
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
  getschemefoe1(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
  
for(var i=0;i<(CustFOE.length);i++)
{
	if(CustFOE[i]==custname)
	{
		txtsch1.value =""
	}
}
  for(n=0;n<ratearr.length;n++)
  {  
     
     prodtype1.value=stockarr[n]
     if(prodtype1.value==0)
     prodtype1.value=""
	pname1.value=unitarr[n]
	if(pname1.value==0)
  pname1.value=""
  packname1.value=ratearr[n]
 if(packname1.value==0)
 packname1.value=""
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
  {     txtsch1.value =""
		prodtype1.value =""
		pname1.value =""
		packname1.value =""
		txtQty1.value =""
		txtstk1.value =""
  }
  else
  {
  alert("Insufficient Free Stock");
  }
  txtQty1.value="";

  break;
  
  }
  
     } 
  }
}


function getschemeprimary(prodtype,pname,packname,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1)
{
  var ProdName
  var PackType 
  var mainarr = new Array()
  var prodtext  = pname.value
  var packindex;
  var packtext;
  var count1=0;
  var packtext  = packname.value
  packname.value=packtext
  
  var hidarr  = document.Form1.temptext12.value
  
  mainarr = hidarr.split(",")
  var prodarr = new Array()
  var discountarr=new Array()
  var status="n"
  var k = 0
   
  prodtype1.value=""
  
  pname1.value=""
  
  packname1.value=""
  txtQty1.value=""
   
  txtstk1.value=""
  
  txtsch1.value=""
  
if(txtQty.value!=0)
{

  for(var i=0;i<(mainarr.length);i++)
  {
  
    prodarr = mainarr[i].split(":")

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
     if(prodarr[2] == prodtext && prodarr[3]==packtext)
      {
	if(status!="y")
	{
	   
	   discountarr[k]=prodarr[4];
	   k++;
	}
      } 
    }
  }
  for(n=0;n<discountarr.length;n++)
     {  
		
		txtsch1.value=discountarr[n]
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
		
	var GType=document.Form1.tempCustGroup.value        //add by vikas 26.10.2012
	var Scheme="No";										//add by vikas 31.10.2012
	var Grouparr=new Array();							//add by vikas 31.10.2012
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var DisTypearr=new Array()
	//var gnamearr=new Array()    // by vikas 25.10.2012
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
			/*********Add by vikas 31.10.2012** for Scheme Apply on more then group*************/
			Grouparr = prodarr[7].split(".")
			//alert(prodarr[7]+" : "+Grouparr.length)
			for(var k=0;k<(Grouparr.length-1);k++)
			{
				if(GType==Grouparr[k])
				{
					Scheme="Yes";
				}
			}
			/*********End*************/
			
			//if(prodarr[7]==GType)            //add by vikas 26.10.2012
			if(Scheme=="Yes")
			{
				/*coment by vikas 9.11.2012 if(ProdName==prodarr[2] && PackType==prodarr[3])
				{
					
					status="y";
				}
				else
				{	
					ProdName=prodarr[2]
					PackType=prodarr[3]
					status="n"
				}*/
				for(var j=0;j<prodarr.length;j++ )
				{ 
					if(prodarr[2]+":"+prodarr[3] == prodtext)
					{
						//if(status!="y")
						//{
						
							/*********Add by vikas 9.11.2012** for Scheme Apply on more then group*************/
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
							/*********End**vikas 9.11.2012***********/
							
							
						}
					} 
				//coment by vikas 9.11.2012}
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
	//alert("getschemeSecSP "+txtschSP.value+" == "+tmpSchSPType.value)
	var ProdName
	var PackType 
	var mainarr = new Array()
	var prodtext  = prodtype.value
	var packindex;
	var packtext;
	var count1=0;
	var hidarr  = document.Form1.temptextSecSP.value
	
	var GType=document.Form1.tempCustGroup.value        //add by vikas 26.10.2012
	
	var Scheme;										//add by vikas 31.10.2012
	var Grouparr = new Array();						//add by vikas 31.10.2012
	
	//alert("Hello")
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
			//alert(mainarr[i])
			//if(prodarr[7]==GType)            //add by vikas 26.10.2012
			if(Scheme=="Yes")                  //Add by vikas 31.10.2012
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

function getschemefoe1(prodtype,txtQty,prodtype1,pname1,packname1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)
{
	var custname=document.Form1.text1.value
	var customer
	var ProdName
	var PackType 
	var mainarr = new Array()
	var prodtext  = pname.value
	var packindex;
	var packtext;
	var count1=0;
	var packtext  = packname.value
	packname.value=packtext
  
	var hidarr  = document.Form1.temptextfoe.value
	mainarr = hidarr.split(",")
	var prodarr = new Array()
	var discountarr=new Array()
	var status="n"
	var k = 0
	txtfoe1.value=""
	var name=""
	for(var i=0;i<(mainarr.length-1);i++)
	{
		prodarr = mainarr[i].split(":")
		if(ProdName==prodarr[2]&&PackType==prodarr[3]&& customer==prodarr[5])
    {
  status="y"; 
    }
    else
    {	
	  ProdName=prodarr[2]
		PackType=prodarr[3]
	 customer=prodarr[5]
	status="n"
    }
    for(var j=0;j<prodarr.length;j++ )
    { 
   
     if(prodarr[2] == prodtext && prodarr[3]==packtext && prodarr[5]==custname)
      {
	if(status!="y")
	{
	   discountarr[k]=prodarr[4];
	   k++;
	}
      } 
    }
     for(var j=0;j<prodarr.length;j++ )
    {
      if(prodarr[2]=='0' && prodarr[3]=='0' && prodarr[5]==custname )
      { 
       
	if(status!="y")
	{	
	   discountarr[k]=prodarr[4];
	   k++;
	}
      } 
    }
  }
  for(n=0;n<discountarr.length;n++)
     {  
		txtfoe1.value = discountarr[n]
		if(txtfoe1.value>0)
		{ 
		txtsch1.value =""
		prodtype1.value =""
		pname1.value =""
		packname1.value =""
		txtQty1.value =""
		txtstk1.value ="" 
		
		}		
		
     } 
  
}

function getschemefoeTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1,tmpFoeType)
{
	//alert(prodtype.value+" , "+txtQty.value+" , "+prodtype1.value+" , "+txtQty1.value+" , "+lblInvoiceDate.value+" , "+txtstk1.value+" , "+txtsch1.value+" , "+txtfoe1.value+" , "+tmpFoeType.value)
	//var custname=document.Form1.text1.value
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
			//alert(prodarr[2]+":"+prodarr[3]==ProdText && prodarr[6]==custname)
			
			if(prodarr[2]+":"+prodarr[3]==ProdText && prodarr[6]==custname)
			{
				if(status!="y")
				{
					discountarr[k]=prodarr[4];
					discounttypearr[k]=prodarr[5];//add
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
