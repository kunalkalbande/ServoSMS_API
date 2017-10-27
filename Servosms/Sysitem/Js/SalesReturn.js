/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
function getschemeqty(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate)
{
	var ProdName
	var PackType 
	var mainarr = new Array()
	var CustFOE = new Array()
	//getschemeprimaryTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1);
	var packindex;
	var packtext;
	var count1=0;
	//var packtext  = packname.value
	//packname.value=packtext
	var ProdText = prodtype.value
	var hidarr  = document.Form1.temptext11.value
	//var FOE = document.Form1.temptext13.value
	var FOE = document.Form1.temptext13.value
	mainarr = hidarr.split(",")
	var custname=document.Form1.lblCustName.value
	//var customer
	//var index = document.Form1.DropCustName.selectedIndex
	//var custname  = document.Form1.DropCustName.options[index].text
	CustFOE = FOE.split(",")
	var prodarr = new Array()
	var ratearr = new Array()
	var Customer = new Array()
	//var stockarr = new Array()
	var unitarr = new Array()
	var schemearr = new Array()
	//var avlstkarr=new Array()
	//var avlstk=new Array()
	//var discountarr=new Array()
	var dateInv=lblInvoiceDate.value
	var status="n"
	var k = 0
	var r =0;
	
	prodtype1.value=""
	txtQty1.value=""
	//txtstk1.value=""
	//txtfoe1.value=""
	
	if(txtQty.value!=0)
	{
		
		for(var i=0;i<(mainarr.length);i++)
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
				if(prodarr[2]+":"+prodarr[3]==ProdText)
				{
					if(status!="y")
					{
						//alert(prodarr[2]+":::"+prodarr[3]+":::"+ProdText)
						//stockarr[k]= prodarr[4];
						unitarr[k]=  prodarr[5];
						ratearr[k] = prodarr[6];
						var s1=prodarr[7];
						var s2=prodarr[8]
						var dd;
						dd=txtQty.value%s1
						schemearr[k]=((txtQty.value-dd)/s1)*s2
						/*if(prodarr[14]=="Primary(Free Scheme)" || prodarr[14]=="Secondry(Free Scheme)")
						{
							avlstkarr[k]=prodarr[11]+" "+prodarr[12];
							avlstk[k]=prodarr[11];
						}
						else
							avlstkarr[k]="";*/
						k++;
					}
				} 
			}
		}
		/*getschemefoeTemp1(prodtype,txtQty,prodtype1,txtQty1,lblInvoiceDate,txtstk1,txtsch1,txtfoe1)*/
		var Flag=0;
		
		for(var i=0;i<(CustFOE.length);i++)
		{
			if(CustFOE[i]==custname)
			{
				Flag=1;
				txtQty1.value ="";
				prodtype1.value="";
				break;
			}
		}
		if(Flag==0)
		{
			for(n=0;n<ratearr.length;n++)
			{  
				
				prodtype1.value=unitarr[n]+":"+ratearr[n]
				if(prodtype1.value==0 || prodtype1.value=="0:0")
					prodtype1.value=""
				/*if(pname1.value==0)
					pname1.value=""
				packname1.value=ratearr[n]
				if(packname1.value==0)
					packname1.value=""*/
				txtQty1.value=schemearr[n]
				if(txtQty1.value=="NaN")
					txtQty1.value=""
			} 
		}
	}
}