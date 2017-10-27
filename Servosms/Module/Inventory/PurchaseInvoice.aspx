<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>

<%@ Page Language="c#" Inherits="Servosms.Module.Inventory.PurchaseInvoice" CodeFile="PurchaseInvoice.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ServoSMS: Other Purchase Invoice</title>
    <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
    <script language="javascript" id="purchase" src="../../Sysitem/JS/Purchase.js"></script>
    <script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop1.js"></script>
    <script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
    <script language="javascript">
		
        function get_fixdisc()
        {
            var disRate=document.Form1.tempfixdisc.value
            if(disRate=="" || isNaN(disRate))
                disRate=0
				
            var tot_qty=document.Form1.txttotalqtyltr1.value
            if(tot_qty=="" || isNaN(tot_qty))
                tot_qty=0
				
            var fixdiscTot=eval(disRate)*eval(tot_qty)
			
            document.Form1.txtfixdisc.value=disRate
            document.Form1.txtfixdiscamount.value=fixdiscTot
        }
		
		
        function get_EBD()
        {
            var v_date=document.Form1.txtVInvoiceDate.value
			
            var EB_period=document.Form1.tempEBPeriod.value      //add by vikas 6.11.2012
			
            var EB_Per=new Array()
            EB_Per=EB_period.split(',')
            if(v_date!="")
            {
                var ebd=document.Form1.temp_EDB.value
				
                var ebd_rate=new Array()
                var ebd_Date=new Array()
                ebd_rate=ebd.split('/')
                ebd_Date=v_date.split('/')
                var day=ebd_Date[0]
				
                //coment by vikas 31.10.2012 if(day<=9 && day>=1)
				
                if(day<=EB_Per[1] && day>=EB_Per[0])
                {
					
                    document.Form1.txtebird.value=ebd_rate[0]
                    document.Form1.temp_erly_bird.value=ebd_rate[0]
                }
                    //coment by vikas 31.10.2012 else if(day<=18 && day>=10)
                else if(day<=EB_Per[3] && day>=EB_Per[2])
                {
					
                    document.Form1.txtebird.value=ebd_rate[1]
                    document.Form1.temp_erly_bird.value=ebd_rate[1]
                }
                    //coment by vikas 31.10.2012 else if(day<=27 && day>=19)
                else if(day<=EB_Per[5] && day>=EB_Per[4])
                {
					
                    document.Form1.txtebird.value=ebd_rate[2]
                    document.Form1.temp_erly_bird.value=ebd_rate[2]
                }
                else
                {
                    document.Form1.txtebird.value=ebd_rate[3]
                    document.Form1.temp_erly_bird.value=ebd_rate[3]
                }
            }
            else
            {
                document.Form1.txtebird.value="0.0"
                document.Form1.temp_erly_bird.value="0.0"
				
            }	              
            GetNetAmountEtaxnew()											//add by vikas 27.10.2012
        }
		
		
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
        
        var prodCount=0;
        function checkProd()
        {
            var packArray = new Array();
            prodCount=0;	
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
            packArray[12]=document.Form1.DropType13.value
            packArray[13]=document.Form1.DropType14.value
            packArray[14]=document.Form1.DropType15.value
            packArray[15]=document.Form1.DropType16.value
            packArray[16]=document.Form1.DropType17.value
            packArray[17]=document.Form1.DropType18.value
            packArray[18]=document.Form1.DropType19.value
            packArray[19]=document.Form1.DropType20.value
            var count = 0;
            for(var i=0;i<20;i++)
            {
                for (var j=0;j<20;j++)
                {
                    //if(packArray[i]==packArray[j] && packArray[i]!="TypeSelectSelect")
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
            for(var m=0;m<20;m++)
            {
                if( packArray[m]!="Type")
                    prodCount=prodCount+1
            }

            return true;
        }
	
        function calc()
        {	
            document.Form1.txtfoc.value="0";
            //****************************
			<% for (int i = 1; i <= 20; i++)
        {
				%>
            document.Form1.txtAmount<%=i%>.value=document.Form1.txtQty<%=i%>.value*document.Form1.txtRate<%=i%>.value	
            if(document.Form1.txtAmount<%=i%>.value==0)
                document.Form1.txtAmount<%=i%>.value=""
            else
                makeRound(document.Form1.txtAmount<%=i%>)
            if(document.Form1.chkfoc<%=i%>.checked)
                document.Form1.txtfoc.value=eval(document.Form1.txtfoc.value)+eval(document.Form1.txtAmount<%=i%>.value)
            <%
        }
			%>
            //****************************
			<% for (int i = 1; i <= 20; i++)
        {
				%>
            if(document.Form1.DropType<%=i%>.value != "Type" && document.Form1.txtQty<%=i%>.value != "")
            {
                getschemeprimary(document.Form1.DropType<%=i%>,document.Form1.tempSchDis<%=i%>);
                GetStockistScheme(document.Form1.DropType<%=i%>,document.Form1.tempStktSchDis<%=i%>);
                GetProductsUnit(document.Form1.DropType<%=i%>,document.Form1.tempUnit<%=i%>);//add
                getschemeAddprimary(document.Form1.DropType<%=i%>,document.Form1.tempSchAddDis<%=i%>);   //Add by vikas 30.06.09
					
                GetFixedDiscount(document.Form1.DropType<%=i%>,document.Form1.tempFixedDisc<%=i%>);       //Add by Vikas 1.1.2013
					
            }
            else
            {
                document.Form1.tempSchDis<%=i%>.value="";
                document.Form1.tempStktSchDis<%=i%>.value="";										//add
                document.Form1.tempSchAddDis<%=i%>.value="";							            //Add by vikas 30.06.09
					
                document.Form1.tempFixedDisc<%=i%>.value="";	           //Add by Vikas 1.1.2013
            }
				<%
        }
			%>
            //****************************
            changeqtyltr();
            document.Form1.txtfixedamt.value="0";
            var schdistot = 0;
            var stktdistot = 0;
            var schdistot_Add = 0;				//Add by vikas 30.06.09
            var stktdis = new Array();
            var schdis = new Array();
            var schdis_Add = new Array();   //Add by vikas 30.06.09
			
			<% 
        for (int i = 1; i <= 20; i++)
        {
				%>
            if(document.Form1.tempSchDis<%=i%>.value!="")
            {
                var dis = document.Form1.tempSchDis<%=i%>.value
                schdis = dis.split(":")
                if(schdis[1]=="%")
                {
                    schdistot+=eval(document.Form1.txtAmount<%=i%>.value)*eval(schdis[0])/100
                }
                else
                {
                    schdistot=schdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*schdis[0];
                }
            }
            if(document.Form1.tempUnit<%=i%>.value =="Barrel"||document.Form1.tempUnit<%=i%>.value=="Drum")
                {
                    if(document.Form1.tempStktSchDis<%=i%>.value!="")
                    {
                        var stkt = document.Form1.tempStktSchDis<%=i%>.value
                        stktdis = stkt.split(":")
                        if(!document.Form1.chkfoc<%=i%>.checked)
                        {
                            if(stktdis[1]=="%")
                            {
                                stktdistot=stktdistot+(eval(document.Form1.txtAmount<%=i%>.value)*eval(stktdis[0]))/100;
                            }
                            else
                            {
                                stktdistot=stktdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                            }
                        }
                    }
                }
                else
                {
                    if(document.Form1.tempStktSchDis<%=i%>.value!="")
                    {
                        var dbValues=document.Form1.txtMainGST.value;
                        var mainarr = new Array()
                        mainarr =dbValues.split("~");
                        var selproduct=document.Form1.DropType<%=i%>.value.split(":");
                        var cgst=0;
                        var sgst=0;
                        for(i=0;i<mainarr.length-1;i++)
                        {
                            taxarr = mainarr[i].split("|")
                            if(taxarr[0]==selproduct[0])
                            {
                                cgst=taxarr[4];
                                sgst=taxarr[5];
                            }
                        }
                        var stkt = document.Form1.tempStktSchDis<%=i%>.value
                        stktdis = stkt.split(":")
                        if(!document.Form1.chkfoc<%=i%>.checked)
                        {
                            if(stktdis[1]=="%")
                            {
                                var stckDis=(eval(document.Form1.txtAmount<%=i%>.value)*(eval(cgst)+eval(sgst)))/100;
                                var stckTot=eval(stckDis)+eval(document.Form1.txtAmount<%=i%>.value)
                                stktdistot=stktdistot+(eval(stckTot)*eval(stktdis[0]))/100;
                            }
                            else
                            {
                                stktdistot=stktdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                            }
                        }
                    }
                }
            /***Add by vikas 30.06.09************/
            if(document.Form1.tempSchAddDis<%=i%>.value!="")
            {
                //alert("Check"+document.Form1.tempSchDis<%=i%>.value)
                var dis = document.Form1.tempSchAddDis<%=i%>.value
                schdis_Add = dis.split(":")
                //alert(schdis[0]+":::"+schdis[1]);
                if(schdis_Add[1]=="%")
                {
                    schdistot_Add+=eval(document.Form1.txtAmount<%=i%>.value)*eval(schdis_Add[0])/100
                    }
                    else
                    {
                        schdistot_Add=schdistot_Add+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*schdis_Add[0];
                    }
                }
            /****end***********/
				<%
        }
			%>
            document.Form1.txtfixedamt.value=eval(schdistot);
            makeRound(document.Form1.txtfixedamt)
            document.Form1.txttradedisamt.value=eval(stktdistot);
            makeRound(document.Form1.txttradedisamt)
			
            /***Add by vikas 30.06.09***************/
            document.Form1.txtAddDis.value=eval(schdistot_Add);
            makeRound(document.Form1.txtAddDis)
            /***end********************/
		
            makeRound(document.Form1.txtfoc)
            GetGrandTotal()
            GetGrandTotal1()
            // alert("hello");
            //Comment by Vikas  2.1.2013 get_fixdisc()        //add by vikas 31.10.2012
            GetNetAmountEtaxnew()
            //Getcgstamt()
            //Getsgstamt()
            //document.Form1.txtNetAmount.value=   Math.round(eval(document.Form1.txtGrandTotal.value) +eval(totalAmountAfterGst),0);
            //totalAmountAfterGst=0;
			
        }	
	
        function GetProductsUnit(prodtype,prodUnit)
        {
	
            var ProdName
            var PackType 
            var ProdCode
            var mainarr = new Array()
            var prodtext  = prodtype.value
            var packindex;
            var packtext;
            var count1=0;
            var hidarr  = document.Form1.tempUnit.value
            mainarr = hidarr.split(",")
            var prodarr = new Array()
            var discountarr=new Array()
            var DisTypearr=new Array()
            var status="n"
            var k = 0
            prodUnit.value=""
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
                           // DisTypearr[k]=prodarr[5];
                            k++;
                        }
                    } 
                }
            }
            for(n=0;n<discountarr.length;n++)
            {  
                prodUnit.value=discountarr[n]
            } 
        }
		
        function GetGrandTotal()
        {
            var GTotal=0
			<% for (int i = 1; i <= 20; i++)
        {
				%>
            if(document.Form1.txtAmount<%=i%>.value!="")
                GTotal=GTotal+eval(document.Form1.txtAmount<%=i%>.value)
	 			<%
        }
	 		%>
            document.Form1.txtGrandTotal.value=GTotal;
            makeRound(document.Form1.txtGrandTotal);
        }	
      
      
        function GetGrandTotal1()
        {   
            changeqtyltr();
            //alert("hello");
           
            var fixdiscTot=0;      //Add by vikas 2.1.2013
            var GTotal1=0
            var GTotal2=0
            var GTotal3=0
            var stktdistot = 0;
            var stktdis = new Array();
            //var GTotal4=0//only for percent value in fixed discount column
            //*************************
			<% for (int i = 1; i <= 20; i++)
        {
				%>
            if(document.Form1.txtQty<%=i%>.value!="")
            {
                GTotal1=GTotal1+eval(document.Form1.txtQty<%=i%>.value);
                //GTotal2=GTotal2+changeqtyltr(eval(document.Form1.txtPack1.value)+eval(document.Form1.txtQty1.value))
                GTotal2=GTotal2+eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
                GTotal3=GTotal3+eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
	 				
                if(document.Form1.chkfoc<%=i%>.checked==true)
                {	
                    if(document.Form1.txtQty<%=i%>.value!="" && GTotal2!=0)
                        GTotal2=GTotal2-eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
                }
                if(document.Form1.tempSchDis<%=i%>.value!="")
                {	
                    GTotal4=GTotal2-eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
                }
                /***Add by vikas 30.06.09******************
                if(document.Form1.tempSchAddDis<%=i%>.value!="")
                {	
                    GTotal4=GTotal2-eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value);
                }
                /****end***************/
            }
            //*******************
            if(document.Form1.tempUnit<%=i%>.value=="Barrel"||document.Form1.tempUnit<%=i%>.value=="Drum")
                {
                    if(document.Form1.tempStktSchDis<%=i%>.value!="")
                    {
                        var stkt = document.Form1.tempStktSchDis<%=i%>.value
                        stktdis = stkt.split(":")
                        if(!document.Form1.chkfoc<%=i%>.checked)
                        {
                            if(stktdis[1]=="%")
                            {
                                stktdistot=stktdistot+(eval(document.Form1.txtAmount<%=i%>.value)*eval(stktdis[0]))/100;
                            }
                            else
                            {
                                stktdistot=stktdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                            }
                        }
                    }
                }
                else
                {
                    if(document.Form1.tempStktSchDis<%=i%>.value!="")
                    {
                        var dbValues=document.Form1.txtMainGST.value;
                        var mainarr = new Array()
                        mainarr =dbValues.split("~");
                        var selproduct=document.Form1.DropType<%=i%>.value.split(":");
                        var cgst=0;
                        var sgst=0;
                        for(i=0;i<mainarr.length-1;i++)
                        {
                            taxarr = mainarr[i].split("|")
                            if(taxarr[0]==selproduct[0])
                            {
                                cgst=taxarr[4];
                                sgst=taxarr[5];
                            }
                        }
                        var stkt = document.Form1.tempStktSchDis<%=i%>.value
                        stktdis = stkt.split(":")
                        if(!document.Form1.chkfoc<%=i%>.checked)
                        {
                            if(stktdis[1]=="%")
                            {
                                var stckDis=(eval(document.Form1.txtAmount<%=i%>.value)*(eval(cgst)+eval(sgst)))/100;
                                var stckTot=eval(stckDis)+eval(document.Form1.txtAmount<%=i%>.value)
                                stktdistot=stktdistot+(eval(stckTot)*eval(stktdis[0]))/100;
                            }
                            else
                            {
                                stktdistot=stktdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                            }
                        }
                    }
                }
            //*******************
				
            /**************Add By Vikas 2.1.2013************************/
            if(document.Form1.tempFixedDisc<%=i%>.value!="")
            {
                var stkt = document.Form1.tempFixedDisc<%=i%>.value
                stktdis = stkt.split(":")
					
                if(stktdis[1]=="%")
                {
							
                }
                else
                {
                    fixdiscTot=fixdiscTot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                        document.Form1.txtfixdisc.value=stktdis[0]
                        //document.Form1.txtfixdiscamount.value=fixdiscTot
                    }
					
                }
				
            /**************End************************/
				<%
        }
			%>
            //*************************
            //alert(GTotal1)
            document.Form1.txttotalqty.value=GTotal1;
            document.Form1.txttotalqtyltr.value=GTotal2;
            document.Form1.txttotalqtyltr1.value=GTotal3;
			
            //document.Form1.txtebirdamt.value=GTotal2*document.Form1.txtebird.value;    //Comment By Vikas Sharma 25.04.09
			
            //****************Start**Add by Vikas Sharma 25.04.09*********************************************/
			
            var EarlyDisType=document.Form1.tempEarlyDisType.value;
			
            var getTotamount=document.Form1.txtGrandTotal.value;
			
            if(EarlyDisType!="%")
            {
                document.Form1.txtebirdamt.value=GTotal2*document.Form1.txtebird.value;
            }
            else
            {
                var EarlyRs=getTotamount*document.Form1.txtebird.value;
                document.Form1.txtebirdamt.value=EarlyRs/100;
            }
				
            //****************end**Add by Vikas Sharma 25.04.09*********************************************/
			
            //***************** add by Mahesh on 30/12/2008, calcculate the trade discount value and after that store in textbox with round the value.
            document.Form1.txttradedisamt.value=eval(stktdistot);
            makeRound(document.Form1.txttradedisamt);
            //5.11.2012 StockistDiscount_New();
            //*****************
            //document.Form1.txttradedisamt.value=GTotal2*document.Form1.txttradedis.value;
            /// comment by Mahesh on 17.12.008, this discount calculate by given different -2 discount in every products then calculate in calc() function. 
            //**var diff=document.Form1.txtGrandTotal.value-document.Form1.txtfoc.value;
            //**document.Form1.txttradedisamt.value=diff*document.Form1.txttradedis.value/100;
            ///**************
            //document.Form1.txtfixedamt.value=GTotal2*document.Form1.txtfixed.value;//comment by Mahesh b'coz txtfixedamt is used for Percent Discount on 03.07.008
            //alert(GTotal1+"::"+GTotal2+"::"+GTotal3)
			
            //alert(document.Form1.txtGrandTotal.value)
            document.Form1.tempGrandTotal.value=document.Form1.txtGrandTotal.value-document.Form1.txtfixedamt.value;
            //alert(document.Form1.tempGrandTotal.value)
			
            makeRound(document.Form1.txtfixedamt);
            makeRound(document.Form1.txtebirdamt);
            //makeRound(document.Form1.txttradedisamt);
            makeRound(document.Form1.txttotalqty);
            //makeRound(document.Form1.txttotalqtyltr);
			
            document.Form1.txtfixdiscamount.value=fixdiscTot    //Add by vikas 2.1.2013
            makeRound(document.Form1.txtfixdiscamount);         //Add by vikas 2.1.2013
        }
        //***************************	
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
        var totaldisc=0;
        //var entrytax=0;
        var bird=0;
        //this coment add by vikas Grand total and Net Total in GetEtaxnew() function 31.10.2012
        function GetEtaxnew()    //         
        {
            totaldisc=0;
            var fixedDisc=0
            //fixedDisc=document.Form1.txtfixed.value
            fixedDisc=document.Form1.txtfixedamt.value
            if(fixedDisc=="" || isNaN(fixedDisc))
                fixedDisc=0
				
				
            /****Add by vikas 30.06.09 ***************/
            var fixedDisc_Add=0
            fixedDisc_Add=document.Form1.txtAddDis.value
            if(fixedDisc_Add=="" || isNaN(fixedDisc_Add))
                fixedDisc_Add=0
            /*******************/
				
            //Mahesh,date:07.04.007 if(document.Form1.dropfixed.value=="Per")
            //Mahesh,date:07.04.007 fixedDisc=document.Form1.txtGrandTotal.value*fixedDisc/100
            //****old******
            /* var Et=document.Form1.txtentry.value
			if(Et=="" || isNaN(Et))
			Et=0
			if(document.Form1.dropentry.value=="Per")
			Et=document.Form1.txtGrandTotal.value*Et/100     */
            //**********old**********
            //******new*****
            //var Et=document.Form1.txtentrytax.value
            //if(Et=="" || isNaN(Et))
            //    Et=0
            //if(document.Form1.dropentry.value=="Per")
            //Et=document.Form1.txtGrandTotal.value*Et/100
            //alert(document.Form1.tempGrandTotal.value)
            //Et=document.Form1.tempGrandTotal.value*Et/100
            //document.Form1.txtentry.value=Et
            //makeRound(document.Form1.txtentry)
            //****new*********
            var focDisc=document.Form1.txtfoc.value
            if(focDisc=="" || isNaN(focDisc))
                focDisc=0
            if(document.Form1.dropfoc.value=="Per")
                focDisc=document.Form1.txtGrandTotal.value*focDisc/100
            //**********
            var ETFOC=(eval(document.Form1.txtfoc.value)*2)/100
            if(isNaN(ETFOC))
                ETFOC=0
            //**********
            var tradeDisc=document.Form1.txttradedisamt.value
            if(tradeDisc=="" || isNaN(tradeDisc))
                tradeDisc=0
            var tradeless=document.Form1.txttradeless.value
            if(tradeless=="" || isNaN(tradeless) || tradeless=="-")
                tradeless=0	
            bird=document.Form1.txtebirdamt.value
            if(bird=="" || isNaN(bird))
                bird=0
            var birdless=document.Form1.txtbirdless.value
            if(birdless=="" || isNaN(birdless) || birdless=="-")
                birdless=0
            ///document.Form1.txtTotalDisc.value=""
            var Disc=document.Form1.txtDisc.value
            if(Disc=="" || isNaN(Disc))
                Disc=0;
            var Dt=0
			
            var DiscStatus=document.Form1.txtDiscStatus.value
			
            if(DiscStatus=="" || isNaN(DiscStatus))
                DiscStatus=0;
			
            if(DiscStatus==0)
            {
                if(document.Form1.DropDiscType.value=="Per")
                {
                    Dt=eval(document.Form1.txtGrandTotal.value)
                    Disc=Dt*Disc/100 
                    document.Form1.txtTotalDisc.value=Disc
                    makeRound(document.Form1.txtTotalDisc,2)
                    if(isNaN(document.Form1.txtTotalDisc.value))
                        document.Form1.txtTotalDisc.value=""
                }
                else
                {
                    document.Form1.txtTotalDisc.value=document.Form1.txttotalqtyltr1.value*document.Form1.txtDisc.value
                    makeRound(document.Form1.txtTotalDisc.value)
                }

            }
            else
            {
                if(document.Form1.DropDiscType.value=="Per")
                {
                    //old Dt=eval(document.Form1.txtGrandTotal.value)-(eval(bird)+eval(tradeDisc)+eval(focDisc))
                    //		Dt=eval(document.Form1.txtGrandTotal.value)-((eval(bird)-eval(birdless))+(eval(tradeDisc)-eval(tradeless))+eval(focDisc))
                    //if(Disc>0)
                    {
                        Dt=eval(document.Form1.txtGrandTotal.value)
                        Disc=Dt*Disc/100 
                        document.Form1.txtTotalDisc.value=Disc
                        makeRound(document.Form1.txtTotalDisc,2)
                        if(isNaN(document.Form1.txtTotalDisc.value))
                            document.Form1.txtTotalDisc.value=""
                    }
                    // else
                    //{
                    //    Disc=document.Form1.txtTotalDisc.value
                    //    makeRound(Disc)
                    //}
                }
                else
                {
                    /*var disRate=document.Form1.tempfixdisc.value
					if(disRate=="" || isNaN(disRate))
						disRate=0
					var tot_qty=document.Form1.txttotalqtyltr1.value
					if(tot_qty=="" || isNaN(tot_qty))
						tot_qty=0
					var fixdiscTot=eval(disRate)*eval(tot_qty)
					document.Form1.txtfixdisc.value=disRate
					document.Form1.txtfixdiscamount.value=fixdiscTot*/
                    //if(Disc>0)
                    {
                        Dt=eval(document.Form1.txttotalqtyltr1.value)
                        Disc=Dt*Disc 
                        document.Form1.txtTotalDisc.value=Disc
                        makeRound(document.Form1.txtTotalDisc,2)
                        if(isNaN(document.Form1.txtTotalDisc.value))
                            document.Form1.txtTotalDisc.value=""
                    }
                    //else
                    //{
                    //    Disc=document.Form1.txtTotalDisc.value
                    //    makeRound(Disc)
                    //}
                }	
            }
            if(Disc==""||isNaN(Disc))
                Disc=0
            /****Add by vikas 22.12.2012 ***************/
            var Sch_Disc=0
            Sch_Disc=document.Form1.txtPromoScheme.value
            if(Sch_Disc=="" || isNaN(Sch_Disc))
                Sch_Disc=0
            /*******************/
			
			
            var tot_fixdisc=document.Form1.txtfixdiscamount.value           //Add by vikas 31.10.2012
            var Disc=document.Form1.txtTotalDisc.value
            var CashDisc=document.Form1.txtCashDisc.value
            if(CashDisc=="" || isNaN(CashDisc))
                CashDisc=0
            var GT=0
            document.Form1.txtTotalCashDisc.value=""
            if(document.Form1.DropCashDiscType.value=="Per")
            {  		
                //old  GT=eval(document.Form1.txtGrandTotal.value)-(eval(bird)+eval(tradeDisc)+eval(focDisc))
                //Mahesh ** GT=eval(document.Form1.txtGrandTotal.value)+ eval(Et)-((eval(bird)-eval(birdless))+(eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc)+eval(fixedDisc))
                //alert("grandtotal : "+ document.Form1.txtGrandTotal.value);
                //alert("etax : "+Et);
                //alert("ebird : "+bird);
                //alert("tradedis : "+tradeDisc);
                //alert("foc : "+focDisc);
                //alert("dis : "+Disc);
                //alert("fixed dis : "+fixedDisc);
                //alert("ETFOC : "+ETFOC)
                //GT=eval(document.Form1.txtGrandTotal.value)+ eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc)+eval(fixedDisc)+(eval(bird)-eval(birdless)))
                //coment by vikas 30.06.09 GT=eval(document.Form1.txtGrandTotal.value)+ eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc)+eval(fixedDisc)+(eval(bird)-eval(birdless))+ETFOC)
                //coment by vikas 31.10.2012 GT=eval(document.Form1.txtGrandTotal.value)+ eval(Et)-eval(fixedDisc_Add)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc)+eval(fixedDisc)+(eval(bird)-eval(birdless))+ETFOC)
				
                //coment by vikas 22.12.2012 GT=eval(document.Form1.txtGrandTotal.value)+ eval(Et)-eval(tot_fixdisc)-eval(fixedDisc_Add)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc)+eval(fixedDisc)+(eval(bird)-eval(birdless))+ETFOC)
                GT=eval(document.Form1.txtGrandTotal.value)-eval(tot_fixdisc)-eval(fixedDisc_Add)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+eval(Disc)+eval(fixedDisc)+(eval(bird)-eval(birdless)+eval(Sch_Disc)))   // Add by vikas 22.12.2012
                //GT=GT-eval(fixedDisc_Add)
				 				
                CashDisc=(GT*CashDisc)/100
                document.Form1.txtTotalCashDisc.value=eval(CashDisc)
                makeRound(document.Form1.txtTotalCashDisc,2)
                if(isNaN(document.Form1.txtTotalCashDisc.value))
                    document.Form1.txtTotalCashDisc.value=""
                //alert(CashDisc) 
            }
            else
            {
                document.Form1.txtTotalCashDisc.value=document.Form1.txtCashDisc.value*document.Form1.txttotalqtyltr1.value
                CashDisc=document.Form1.txtTotalCashDisc.value
                makeRound(document.Form1.txtTotalCashDisc)
            }
			
			
            document.Form1.txtVatValue.value = "";	
            //old		document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-eval(tradeDisc)-eval(focDisc)-eval(bird)-eval(CashDisc)-eval(Disc)
		
            //coment by vikas 31.10.2012 document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc)+eval(fixedDisc)+eval(fixedDisc_Add))   //Add by vikas 30.06.09
		
		
            //coment by vikas 22.12.2012 document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc)+eval(fixedDisc)+eval(fixedDisc_Add)+eval(tot_fixdisc))   //Add by vikas 31.10.2012
            document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc)+eval(fixedDisc)+eval(fixedDisc_Add)+eval(tot_fixdisc)+eval(Sch_Disc))   //Add by vikas 22.12.2012
            totaldisc=((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc)+eval(fixedDisc)+eval(fixedDisc_Add)+eval(tot_fixdisc)+eval(Sch_Disc))
            if(isNaN(totaldisc))
                totaldisc=0
            //entrytax=eval(Et)
            //coment by vikas 30.06.09  document.Form1.txtVatValue.value = eval(document.Form1.txtGrandTotal.value) + eval(Et)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc)+(eval(bird)-eval(birdless))+eval(CashDisc)+eval(Disc)+eval(fixedDisc))
		
            //document.Form1.txtVatValue.value = CashDisc-(eval(bird)-eval(birdless))
            //alert(document.Form1.txtVatValue.value);
        }

        var persistedCgstNetAmount=0;
        var persistedIgstNetAmount=0;
        var persistedSgstNetAmount=0;
        var totalAmountAfterGst=0;
        function GetCGSTAmount()
        {
            GetEtaxnew()
            if(document.Form1.N.checked)
            {
                document.Form1.Textcgst.value = "";
            } 
            else
            {
                var cgst_rate = document.Form1.Tempcgstrate.value
                //
                if(cgst_rate == "")
                    cgst_rate = 0;
                var cgst = totalValue ; //document.Form1.txtVatValue.value ;
			
                if(cgst == "" || cgst == null || isNaN(cgst))
                {
                    cgst = 0;
                }
                var cgst_amount = (cgst * cgst_rate)/100
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
        //Calculate CGST
        function Getcgstamt()
        {
            
            GetGrandTotal1();
            var cgst_value = 0;
            var cgstamount=0;
            if(document.Form1.N.checked)
            {
                GetEtaxnew()
                cgst_value = document.Form1.txtVatValue.value;
                if(document.Form1.Textcgst.value!="")
                    document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.Textcgst.value),0);
                document.Form1.Textcgst.value = "";
            }
            else
            {
                cgstamount = GetCGSTAmount()
                cgst_value = document.Form1.txtVatValue.value;
            }
            if(cgst_value=="" || isNaN(cgst_value))
                cgst_value=0
            //document.Form1.txtNetAmount.value=eval(cgst_value);
            //**	makeRound(document.Form1.txtNetAmount);
     
            var netamount=Math.round(document.Form1.txtNetAmount.value,0);
            netamount=netamount+".00";
            if(document.Form1.txtNetAmount.value!='')
                persistedCgstNetAmount= document.Form1.txtNetAmount.value;
            if(document.Form1.Textcgst.value!="")
                totalAmountAfterGst=totalAmountAfterGst+eval(document.Form1.Textcgst.value);
            
            GetEBT()
            return cgstamount
        }
       
        function GetSGSTAmount()
        {
            GetEtaxnew()
            if(document.Form1.Noo.checked)
            {
                document.Form1.Textsgst.value = "";
            } 
            else
            {
                var sgst_rate = document.Form1.Tempsgstrate.value
                //
                if(sgst_rate == "")
                    sgst_rate = 0;
                var sgst = totalValue ;
			
                if(sgst == "" || sgst == null || isNaN(sgst))
                {
                    sgst = 0;
                }
                var sgst_amount = (sgst * sgst_rate)/100
                makeRound(document.Form1.Textsgst)
	    
                document.Form1.txtVatValue.value = eval(sgst) + eval(sgst_amount)
                if(document.Form1.Textsgst.value=="")
                {
                    document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)+Math.round((sgst_amount),0),0);
                }
                document.Form1.Textsgst.value =  Math.round(sgst_amount,0);
                
            }
            return sgst_amount;
        }
        //calculate SGST
        function Getsgstamt()
        {
            
            GetGrandTotal1();
            var sgst_value = 0;
            var sgst = 0;
            if(document.Form1.Noo.checked)
            {
                GetEtaxnew()
                sgst_value = document.Form1.txtVatValue.value;
                if(document.Form1.Textsgst.value!="")
                    document.Form1.txtNetAmount.value =Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.Textsgst.value),0);
                document.Form1.Textsgst.value = "";
            }
            else
            {
                sgst = GetSGSTAmount()
                sgst_value = document.Form1.txtVatValue.value;
            }
            if(sgst_value=="" || isNaN(sgst_value))
                sgst_value=0
            //document.Form1.txtNetAmount.value=eval(sgst_value);
            //**	makeRound(document.Form1.txtNetAmount);
     
            var netamount=Math.round(document.Form1.txtNetAmount.value,0);
            netamount=netamount+".00";
            
		
            if(document.Form1.txtNetAmount.value!='' )
                persistedSgstNetAmount=  document.Form1.txtNetAmount.value;
            if(document.Form1.Textsgst.value!="")
                totalAmountAfterGst=totalAmountAfterGst+Math.round(eval(document.Form1.Textsgst.value),0);
            GetEBT()
            return sgst;
        }
        function GetVatAmountetaxnew()
        {
            GetEtaxnew()
            if(document.Form1.No.checked)
            {
                document.Form1.txtVAT.value = "";
            } 
            else
            {
                var vat_rate = document.Form1.txtVatRate.value
                //
                if(vat_rate == "")
                    vat_rate = 0;
                var vat = totalValue ;
			
                if(vat == "" || vat == null || isNaN(vat))
                {
                    vat = 0;
                }
                var vat_amount = (vat * vat_rate)/100
                makeRound(document.Form1.txtVAT)
                document.Form1.txtVatValue.value = eval(vat) + eval(vat_amount)
                if(document.Form1.txtVAT.value=="")
                {
                    if(isNaN(document.Form1.txtNetAmount.value))
                        document.Form1.txtNetAmount.value=0;
                    document.Form1.txtNetAmount.value =Math.round(eval(document.Form1.txtNetAmount.value)+Math.round(eval(vat_amount),0),0);
                }
                document.Form1.txtVAT.value = Math.round(vat_amount,0);
               
            }
            return vat_amount;
        }
	
        function GetEBT()
        {
            //var birdless = document.Form1.txtbirdless.value
            if(document.Form1.txtbirdless.value != "" && document.Form1.txtbirdless.value != "-")
            {
                document.Form1.txtebirdamt.value = eval(document.Form1.txtebirdamt.value)-eval(document.Form1.txtbirdless.value)
            }
            if(document.Form1.txttradeless.value!="" && document.Form1.txttradeless.value!="-")
            {
                document.Form1.txttradedisamt.value=eval(document.Form1.txttradedisamt.value)-eval(document.Form1.txttradeless.value)
            }
        }
        //calculat IGST
        function GetIgstamt()
        {
            GetGrandTotal1();
            var vat_value = 0;
            var vat=0;
            if(document.Form1.No.checked)
            {
                GetEtaxnew()
                vat_value = document.Form1.txtVatValue.value;
                if(document.Form1.txtVAT.value!="")
                    document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.txtVAT.value),0);
                document.Form1.txtVAT.value = "";
            }
            else
            {
                vat = GetVatAmountetaxnew()
                vat_value = document.Form1.txtVatValue.value;
            }
            if(vat_value=="" || isNaN(vat_value))
                vat_value=0
            //document.Form1.txtNetAmount.value=eval(vat_value);
            //**	makeRound(document.Form1.txtNetAmount);
     
            var netamount=Math.round(document.Form1.txtNetAmount.value,0);
            netamount=netamount+".00";
            //document.Form1.txtNetAmount.value=netamount;
		
            if(document.Form1.txtNetAmount.value!='' )
                persistedIgstNetAmount = document.Form1.txtNetAmount.value;
            if(document.Form1.txtVAT.value!="")
                totalAmountAfterGst+=Math.round(eval(document.Form1.txtVAT.value),0);
            GetEBT()            
            return vat;
        }
        var totalValue = 0;
        function GetNetAmountEtaxnew()
        {
            checkProd()
            var selarr = new Array()
            var dbValues=document.Form1.txtMainGST.value;
            var mainarr = new Array()
            var taxarr = new Array()
            var focDisc=0
            var birdperProd=0
            var cgstamount1=0,cgstamount2 = 0,cgstamount3=0,cgstamount4=0,cgstamount5=0,cgstamount6=0,cgstamount7=0,cgstamount8=0,cgstamount9=0,cgstamount10=0,cgstamount11=0,cgstamount12 = 0,
                cgstamount13=0,cgstamount14 = 0,cgstamount15=0,cgstamount16=0,cgstamount17=0,cgstamount18=0,cgstamount19=0,cgstamount20=0
            var sgstamount1=0,sgstamount2 = 0,sgstamount3=0,sgstamount4=0,sgstamount5=0,sgstamount6=0,sgstamount7=0,sgstamount8=0,sgstamount9=0,sgstamount10=0,sgstamount11=0,sgstamount12 = 0,
                sgstamount13=0,sgstamount14 = 0,sgstamount15=0,sgstamount16=0,sgstamount17=0,sgstamount18=0,sgstamount19=0,sgstamount20=0
            var igstamount1=0,igstamount2 = 0,igstamount3=0,igstamount4=0,igstamount5=0,igstamount6=0,igstamount7=0,igstamount8=0,igstamount9=0,igstamount10=0,igstamount11=0,igstamount12 = 0,
                igstamount13=0,igstamount14 = 0,igstamount15=0,igstamount16=0,igstamount17=0,igstamount18=0,igstamount19=0,igstamount20=0
             
            var focDisc1=0,focDisc2=0,focDisc3=0,focDisc4=0,focDisc5=0,focDisc6=0,focDisc7=0,focDisc8=0,focDisc9=0,focDisc10=0,focDisc11=0,focDisc12=0,focDisc13=0,focDisc14=0,focDisc15=0,focDisc16=0,focDisc17=0,focDisc18=0,focDisc19=0,focDisc20=0;
            document.Form1.txtVatRate.value=""
            document.Form1.Tempcgstrate.value=""
            document.Form1.Tempsgstrate.value=""
            var stktdis=0
           
            mainarr =dbValues.split("~");
            
            // debugger;
            <% for (int i = 1; i <= 20; i++)
        {
				%>
            if(document.Form1.DropType<%=i%>.value !="Type")
            {
               
                var selectedProduct = document.Form1.DropType<%=i%>.value
                var amount = document.Form1.txtAmount<%=i%>.value
                var stckDisc= document.Form1.tempStktSchDis<%=i%>.value 
                var prodUnit=document.Form1.tempUnit<%=i%>.value 
                var fixedDisc=0
                var schdistot_Add=0
                var tot_fixdisc= 0
                //FOC
                document.Form1.txtfoc.value="0";
                if(document.Form1.chkfoc<%=i%>.checked)
                    focDisc<%=i%>=eval(document.Form1.txtfoc.value)+eval(document.Form1.txtAmount<%=i%>.value)
                document.Form1.txtfoc.value= Math.round(focDisc1)+ Math.round(focDisc2)+ Math.round(focDisc3)+Math.round(focDisc4)+Math.round(focDisc5)+Math.round(focDisc6)+Math.round(focDisc7)+Math.round(focDisc8)+ Math.round(focDisc9)+ Math.round(focDisc10)+Math.round(focDisc11)+Math.round(focDisc12)+Math.round(focDisc13)+Math.round(focDisc14)+Math.round(focDisc15)+ Math.round(focDisc16)+ Math.round(focDisc17)+Math.round(focDisc18)+Math.round(focDisc19)+Math.round(focDisc20)
                focDisc= document.Form1.txtfoc.value
                var schdistot=0
                var stktdistot=0
                if(focDisc=="" || isNaN(focDisc))
                    focDisc=0
                if(document.Form1.dropfoc.value=="Per")
                    focDisc<%=i%>=document.Form1.txtAmount<%=i%>.value*focDisc<%=i%>/100
                //**********
                var ETFOC=(eval(focDisc<%=i%>)*2)/100
                if(isNaN(ETFOC))
                    ETFOC=0
                //**********
               
                var tradeless=document.Form1.txttradeless.value
                if(prodCount!=0 && prodCount>=1)
                {
                    tradeless=tradeless/prodCount
                }
                if(tradeless=="" || isNaN(tradeless) || tradeless=="-")
                    tradeless=0	
                //var bird=document.Form1.txtebirdamt.value
                //if(bird=="" || isNaN(bird))
                //    bird=0

                var EarlyDisType=document.Form1.tempEarlyDisType.value;
                if(!document.Form1.chkfoc<%=i%>.checked)
                {
                    if(EarlyDisType!="%")
                    {
                        birdperProd=(eval(document.Form1.txtqPack<%=i%>.value)*eval(document.Form1.txtQty<%=i%>.value))*document.Form1.txtebird.value;
                    }
                    else
                    {
                        var early=document.Form1.txtAmount<%=i%>.value*document.Form1.txtebird.value;
                        birdperProd=early/100;
                    }
                }

                var birdless=document.Form1.txtbirdless.value
                if(prodCount!=0 && prodCount>=1)
                {
                    birdless=birdless/prodCount
                }
                if(birdless=="" || isNaN(birdless) || birdless=="-")
                    birdless=0
                selarr.push(selectedProduct) 
                var selproduct=selectedProduct.split(":");
                var ltrs=selproduct[2].split("X")
                var calcLtrs=ltrs[0]*ltrs[1]
                var Disc=document.Form1.txtDisc.value
                if(document.Form1.DropDiscType.value=="Per")
                {
                    Dt=eval(document.Form1.txtAmount<%=i%>.value)
                    Disc=Dt*Disc/100 
                    makeRound(Disc,2)
                    if(isNaN(Disc))
                        Disc=""
                }
                else
                {
                    Disc=(calcLtrs*document.Form1.txtQty<%=i%>.value)*document.Form1.txtDisc.value
                    makeRound(Disc)
                }

                var CashDisc=document.Form1.txtCashDisc.value
                if(CashDisc=="" || isNaN(CashDisc))
                    CashDisc=0
                var GT=0
                document.Form1.txtTotalCashDisc.value=""
                //additional discount
                if(document.Form1.tempSchAddDis<%=i%>.value!="")
                {
                    var dis = document.Form1.tempSchAddDis<%=i%>.value
                    schdis_Add = dis.split(":")
                    
                    if(schdis_Add[1]=="%")
                    {
                        schdistot_Add=eval(document.Form1.txtAmount<%=i%>.value)*eval(schdis_Add[0])/100
                    }
                    else
                    {
                        schdistot_Add=schdistot_Add+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*schdis_Add[0];
                    }
                }
                var fixedDisc_Add=0
                fixedDisc_Add=schdistot_Add
                if(fixedDisc_Add=="" || isNaN(fixedDisc_Add))
                    fixedDisc_Add=0
                //*****

                //schDiscount
                var Sch_Disc=0
                //Sch_Disc=document.Form1.txtPromoScheme.value
                if(document.Form1.tempSchDiscount<%=i%>.value!="")
                    Sch_Disc=eval(document.Form1.tempSchDiscount<%=i%>.value);
                if(Sch_Disc=="" || isNaN(Sch_Disc))
                    Sch_Disc=0

                //servo stock discount
                stktdis = stckDisc.split(":")

                if(prodUnit=="Barrel"||prodUnit=="Drum")
                {
                    if(document.Form1.tempStktSchDis<%=i%>.value!="")
                    {
                        var stkt = document.Form1.tempStktSchDis<%=i%>.value
                        stktdis = stkt.split(":")
                        if(!document.Form1.chkfoc<%=i%>.checked)
                        {
                            if(stktdis[1]=="%")
                            {
                                stktdistot=stktdistot+(eval(document.Form1.txtAmount<%=i%>.value)*eval(stktdis[0]))/100;
                            }
                            else
                            {
                                stktdistot=stktdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                            }
                        }
                    }
                }
                else
                {
                    if(document.Form1.tempStktSchDis<%=i%>.value!="")
                    {
                        var cgst=0;
                        var sgst=0;
                        for(i=0;i<mainarr.length-1;i++)
                        {
                            taxarr = mainarr[i].split("|")
                            if(taxarr[0]==selproduct[0])
                            {
                                cgst=taxarr[4];
                                sgst=taxarr[5];
                            }
                        }
                        var stkt = document.Form1.tempStktSchDis<%=i%>.value
                        stktdis = stkt.split(":")
                        if(!document.Form1.chkfoc<%=i%>.checked)
                        {
                            if(stktdis[1]=="%")
                            {
                                var stckDis=(eval(document.Form1.txtAmount<%=i%>.value)*(eval(cgst)+eval(sgst)))/100;
                                var stckTot=eval(stckDis)+eval(document.Form1.txtAmount<%=i%>.value)
                                stktdistot=stktdistot+(eval(stckTot)*eval(stktdis[0]))/100;
                            }
                            else
                            {
                                stktdistot=stktdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                            }
                        }
                    }
                }
                
                
                if(document.Form1.tempFixedDisc<%=i%>.value!="")
                {
                    var stkt = document.Form1.tempFixedDisc<%=i%>.value
                    stktdis = stkt.split(":")
					
                    if(stktdis[1]=="%")
                    {
							
                    }
                    else
                    {
                        tot_fixdisc=tot_fixdisc+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*stktdis[0];
                        //document.Form1.txtfixdisc.value=stktdis[0]
                    }
					
                }

                //fixed discount
                if(document.Form1.tempSchDis<%=i%>.value!="")
                {
                    var dis = document.Form1.tempSchDis<%=i%>.value
                    schdis = dis.split(":")
                    if(schdis[1]=="%")
                    {
                        schdistot=eval(document.Form1.txtAmount<%=i%>.value)*eval(schdis[0])/100
                    }
                    else
                    {
                        schdistot=schdistot+document.Form1.txtqPack<%=i%>.value*document.Form1.txtQty<%=i%>.value*schdis[0];
                    }
                }
                fixedDisc=schdistot
                if(fixedDisc=="" || isNaN(fixedDisc))
                    fixedDisc=0
                //trade discount
                var tradeDisc=stktdistot
                if(tradeDisc=="" || isNaN(tradeDisc))
                    tradeDisc=0
                //entry tax
                //var Et=document.Form1.txtentrytax.value
                
                //if(Et=="" || isNaN(Et))
                    //Et=0
                //Et=(document.Form1.txtAmount<%=i%>.value-eval(schdistot))*Et/100
                //cashdiscount
                if(document.Form1.DropCashDiscType.value=="Per")
                {  		
                    GT=eval(document.Form1.txtAmount<%=i%>.value)-eval(tot_fixdisc)-eval(fixedDisc_Add)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc<%=i%>)+eval(Disc)+eval(schdistot)+(eval(birdperProd)-eval(birdless)+eval(Sch_Disc)))   // Add by vikas 22.12.2012
                    var cashdiscount=(GT*CashDisc)/100
                    //document.Form1.txtTotalCashDisc.value=eval(CashDisc)
                    makeRound(cashdiscount,2)
                    if(isNaN(cashdiscount))
                        cashdiscount=""
                }
                else
                {
                    var cashdiscount=document.Form1.txtCashDisc.value*(calcLtrs*document.Form1.txtQty<%=i%>.value)
                    //CashDisc=document.Form1.txtTotalCashDisc.value
                    makeRound(cashdiscount)
                }

                var discount = Disc
                var cashdiscount = cashdiscount
                for(i=0;i<mainarr.length-1;i++)
                {
                    taxarr = mainarr[i].split("|")
                    if(taxarr[0]==selproduct[0])
                    {
                        document.Form1.txtVatRate.value=taxarr[3];
                        document.Form1.Tempcgstrate.value=taxarr[4];
                        document.Form1.Tempsgstrate.value=taxarr[5];
                        var earbird=eval(birdperProd)-eval(birdless);
                        totalValue=eval(document.Form1.txtAmount<%=i%>.value)-((eval(tradeDisc)-eval(tradeless))+eval(focDisc<%=i%>)+(eval(earbird))+eval(cashdiscount)+eval(Disc)+eval(fixedDisc)+eval(fixedDisc_Add)+eval(tot_fixdisc)+eval(Sch_Disc))
                        //var totalValue1 =(Math.round(amount)+Math.round(Et))-Math.round(stktdistot)-Math.round(schdistot)-Math.round(discount)-Math.round(cashdiscount);
                        totalAmountAfterGst=0;
                        var igstamount<%=i%> = GetIgstamt()
                        document.Form1.tempIgst<%=i%>.value=igstamount<%=i%>
                        makeRound(document.Form1.tempIgst<%=i%>)
                        var cgstamount<%=i%> = Getcgstamt()
                        document.Form1.tempCgst<%=i%>.value=cgstamount<%=i%>
                        makeRound(document.Form1.tempCgst<%=i%>)
                        var sgstamount<%=i%> = Getsgstamt()
                        document.Form1.tempSgst<%=i%>.value=sgstamount<%=i%>
                        makeRound(document.Form1.tempSgst<%=i%>)
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
            totalAmountAfterGst=Math.round(document.Form1.txtVAT.value)+Math.round(document.Form1.Textcgst.value)+Math.round(document.Form1.Textsgst.value)
            document.Form1.txtNetAmount.value=   Math.round(eval(document.Form1.txtGrandTotal.value) +eval(totalAmountAfterGst)-eval(totaldisc),0);

            
            //if(document.Form1.txtNetAmount.value = ""|| isNaN(document.Form1.txtNetAmount.value))
            //    document.Form1.txtNetAmount.value=0;
            //GetGrandTotal1();
            //var vat_value = 0;
            //if(document.Form1.No.checked)
            //{
            //    GetEtaxnew()
            //    vat_value = document.Form1.txtVatValue.value;
            //    if(document.Form1.txtVAT.value!="")
            //        document.Form1.txtNetAmount.value = Math.round(eval(document.Form1.txtNetAmount.value)-eval(document.Form1.txtVAT.value),0);
            //    document.Form1.txtVAT.value = "";
            //}
            //else
            //{
            //    GetVatAmountetaxnew()
            //    vat_value = document.Form1.txtVatValue.value;
            //}
            //if(vat_value=="" || isNaN(vat_value))
            //    vat_value=0
            ////document.Form1.txtNetAmount.value=eval(vat_value);
            ////**	makeRound(document.Form1.txtNetAmount);
     
            //var netamount=Math.round(document.Form1.txtNetAmount.value,0);
            //netamount=netamount+".00";
            ////document.Form1.txtNetAmount.value=netamount;
		
            //if(document.Form1.txtNetAmount.value!='' )
            //    persistedIgstNetAmount = document.Form1.txtNetAmount.value;
            
            //totalAmountAfterGst+=Math.round(eval(document.Form1.txtVAT.value),0);
            //GetEBT()
         
        }
        
	
        function changeqtyltr()
        {
            var f1=document.Form1
            //************************
		<% for (int i = 1; i <= 20; i++)
        {
			%>
            var mainarr2 = new Array()
            var hidarr2  = document.Form1.DropType<%=i%>.value
            if(hidarr2 != "Type")
            {
                mainarr2 = hidarr2.split(":")
                //if(f1.txtPack<%=i%>.value != "")
                if(mainarr2[2].toString() != "")
                {
                    var mainarr1 = new Array()
                    //var hidarr1  = document.Form1.txtPack<%=i%>.value
		            //mainarr1 = hidarr1.split("X")
		            mainarr1 = mainarr2[2].toString().split("X")
		            f1.txtqPack<%=i%>.value=mainarr1[0] * mainarr1[1]
                }
            }
            else
                f1.txtqPack<%=i%>.value="0"
			<%
        }
		%>
        }
	
        function checkDelRec()
        {
            if(document.Form1.BtnEdit == null)
            {
                if(document.Form1.DropInvoiceNo.value!="Select")
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
	
        function getInvoiceNo(t)
        {
            if(t.value!="")
            {
                var mainarr = new Array()
                var hidtext  = document.Form1.tempInvoiceInfo.value
                var InNo=t.value
                mainarr = hidtext.split("~")
                if(document.Form1.BtnEdit == null)
                {
                    if(document.Form1.tempVndrInvoiceNo.value!=document.Form1.txtVInnvoiceNo.value)
                    {
                        for(var i=0;i<mainarr.length;i++)
                        {
                            if(eval(mainarr[i])==eval(t.value))
                            {  
                                alert("Vendor Invoice No AllReady Exist");
                                t.value="";
                                return;
                            }   
                        }
                    }
                }    
                else
                {
                    for(var i=0;i<mainarr.length;i++)
                    {
                        if(eval(mainarr[i])==eval(t.value))
                        {  
                            alert("Vendor Invoice No AllReady Exist");
                            t.value="";
                            return;
                        }   
                    }
                }
            }
        }
	
        function GetFOC(t,amt)
        {
            if(t.checked)
            {
                if(amt.value!="")
                {
                    if(document.Form1.txtfoc.value=="")
                        document.Form1.txtfoc.value=eval(amt.value);
                    else
                        document.Form1.txtfoc.value=eval(document.Form1.txtfoc.value)+eval(amt.value);
                    makeRound(document.Form1.txtfoc)
                    calc()
                }
                else
                {
                    alert("Please Select The Product and Fill Qty");
                    t.checked=false;
                }
            }
            else
            {
                if(amt.value!="")
                {
                    document.Form1.txtfoc.value=eval(document.Form1.txtfoc.value)-eval(amt.value);
                    makeRound(document.Form1.txtfoc)
                    calc()
                }
            }
            //alert(document.Form1.txtfoc.value);
        }
	
        //function getBatch(t,prd,Invo,qty,pck)
        function getBatch(t,prd,Invo,qty)
        {
            if(t.checked)
            {
                if(prd.value!="Type" && qty.value!="")
                {
                    var Result="";
                    if(Invo!=null)
                    {
                        childWin=window.open("BatchNo.aspx?chk="+t.name+":"+prd.value+":"+Invo.value, "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
                    }
                    else
                        childWin=window.open("BatchNo.aspx?chk="+t.name+":"+prd.value+":"+document.Form1.DropInvoiceNo.value, "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
                }
                else
                {
                    alert("Please Select The Prod & Fill The Qty");
                    t.checked=false;
                }
            }
        }
	
        function getSchDisc(t,prd,Invo,qty,tempDisc)
        {
            var count=0;
            if(t.checked)
            {
                if(prd.value!="Type" && qty.value!="")
                {
                    var Result="";
                    if(Invo!=null)
                        childWin=window.open("SchemeDiscount.aspx?chk="+t.name+":"+prd.value+":"+Invo.value, "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=165px,height=90px");
                    else
                        childWin=window.open("SchemeDiscount.aspx?chk="+t.name+":"+prd.value+":"+document.Form1.DropInvoiceNo.value, "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=165xp,height=90px");
                }
                else
                {
                    alert("Please Select The Prod & Fill The Qty");
                    t.checked=false;
                }
            }
            else
            {
                tempDisc.value="";
			<%
        for (int i = 1; i <= 20; i++)
        {
				%>
                if(document.Form1.tempSchDiscount<%=i%>.value!="")
                    count=count+eval(document.Form1.tempSchDiscount<%=i%>.value);
				<%
        }
			%>
                //coment by vikas 22.12.2012 document.Form1.txtDisc.value=count;
                document.Form1.txtPromoScheme.value=count;
                GetNetAmountEtaxnew();
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
        function SetFocus()
        {
            document.Form1.DropType1.focus();
        }
    </script>
    </SCRIPT>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <script language="javascript" id="Fuel" src="../../Sysitem/JS/Fuel.js"></script>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Sysitem/Styles.css" type="text/css" rel="StyleSheet">
    <style type="text/css">
        .auto-style1 {
            height: 21px;
        }
    </style>
</head>
<body onkeydown="change(event)">
    <form id="Form1" name="Form1" method="post" runat="server">
        <asp:TextBox ID="txtTempQty2" Style="z-index: 106; left: 184px; position: absolute; top: 8px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <input id="txtqPack20" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack20" runat="server">
        <input id="txtqPack19" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack19" runat="server">
        <input id="txtqPack18" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack18" runat="server">
        <input id="txtqPack17" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack17" runat="server">
        <input id="txtqPack16" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack16" runat="server">
        <input id="txtqPack15" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack15" runat="server">
        <input id="txtqPack14" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack14" runat="server">
        <input id="txtqPack13" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack13" runat="server">
        <input id="txtqPack12" style="z-index: 129; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack12" runat="server">
        <input id="txtqPack11" style="z-index: 126; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack11" runat="server">
        <input id="txtqPack10" style="z-index: 127; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack10" runat="server">
        <input id="txtqPack9" style="z-index: 128; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack9" runat="server">
        <input id="txtqPack8" style="z-index: 130; left: 416px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack8" runat="server">
        <input id="txtqPack7" style="z-index: 125; left: 400px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack7" runat="server">
        <input id="txtqPack6" style="z-index: 124; left: 384px; width: 5px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtqPack6" runat="server">
        <input id="txtqPack5" style="z-index: 123; left: 368px; width: 5px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtqPack5" runat="server">
        <input id="txtqPack4" style="z-index: 122; left: 352px; width: 5px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtqPack4" runat="server">
        <input id="txtqPack3" style="z-index: 121; left: 336px; width: 5px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtqPack3" runat="server">
        <input id="txtqPack2" style="z-index: 120; left: 312px; width: 5px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtqPack2" runat="server">
        <input id="txtqPack1" style="z-index: 119; left: 296px; width: 3px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtqPack1" runat="server">
        <input id="txtVatRate" style="z-index: 118; left: 248px; width: 8px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtVatRate" runat="server">
        <input id="txtVatValue" style="z-index: 117; left: 272px; width: 5px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="txtVatValue" runat="server">
        <input id="temp_EDB" style="z-index: 117; left: 272px; width: 5px; position: absolute; top: 8px; height: 22px"
            type="hidden" size="1" name="temp_EDB" runat="server">
        <asp:TextBox ID="txtTempQty7" Style="z-index: 111; left: 224px; position: absolute; top: 8px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty6" Style="z-index: 110; left: 216px; position: absolute; top: 8px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty4" Style="z-index: 108; left: 200px; position: absolute; top: 8px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty3" Style="z-index: 107; left: 192px; position: absolute; top: 0px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <input id="TxtVen" style="z-index: 102; left: -544px; position: absolute; top: -24px" type="text"
            name="TxtVen" runat="server">
        <asp:TextBox ID="Txtselect" Style="z-index: 103; left: 144px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="TextBox1" Style="z-index: 104; left: 160px; position: absolute; top: 16px" runat="server"
            Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty1" Style="z-index: 105; left: 176px; position: absolute; top: 8px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty5" Style="z-index: 109; left: 208px; position: absolute; top: 8px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty8" Style="z-index: 112; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty9" Style="z-index: 116; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty10" Style="z-index: 114; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty11" Style="z-index: 113; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty12" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty13" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty14" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty15" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty16" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty17" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty18" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty19" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
        <asp:TextBox ID="txtTempQty20" Style="z-index: 115; left: 232px; position: absolute; top: 16px"
            runat="server" Visible="False" Width="8px"></asp:TextBox>
       <%-- <input id="txtentrytax" style="z-index: 132; left: 432px; width: 12px; position: absolute; top: 16px; height: 22px"
            type="hidden" size="1" name="txtentrytax" runat="server">--%>
        <input id="tempDelinfo" style="width: 1px" type="hidden" name="tempDelinfo" runat="server">
        <input id="tempInvoiceInfo" style="width: 1px" type="hidden" name="tempInvoiceInfo" runat="server">
        <input id="temp_erly_bird" style="width: 1px" type="hidden" name="temp_erly_bird" runat="server">
        <input id="bat0" style="width: 1px" type="hidden" name="bat0" runat="server"><input id="bat1" style="width: 1px" type="hidden" name="bat1" runat="server"><input id="bat2" style="width: 1px" type="hidden" name="bat2" runat="server">
        <input id="bat3" style="width: 1px" type="hidden" name="bat3" runat="server"><input id="bat4" style="width: 1px" type="hidden" name="bat4" runat="server"><input id="bat5" style="width: 1px" type="hidden" name="bat5" runat="server">
        <input id="bat6" style="width: 1px" type="hidden" name="bat6" runat="server"><input id="bat7" style="width: 1px" type="hidden" name="bat7" runat="server"><input id="bat8" style="width: 1px" type="hidden" name="bat8" runat="server">
        <input id="bat9" style="width: 1px" type="hidden" name="bat9" runat="server"><input id="bat10" style="width: 1px" type="hidden" name="bat10" runat="server"><input id="bat11" style="width: 1px" type="hidden" name="bat11" runat="server"><input id="bat12" style="width: 1px" type="hidden" name="bat12" runat="server"><input id="bat13" style="width: 1px" type="hidden" name="bat13" runat="server"><input id="bat14" style="width: 1px" type="hidden" name="bat14" runat="server"><input id="bat15" style="width: 1px" type="hidden" name="bat15" runat="server"><input id="bat16" style="width: 1px" type="hidden" name="bat16" runat="server"><input id="bat17" style="width: 1px" type="hidden" name="bat17" runat="server"><input id="bat18" style="width: 1px" type="hidden" name="bat18" runat="server"><input id="bat19" style="width: 1px" type="hidden" name="bat19" runat="server">
        <input id="tempSchDis1" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempEarlyDisType" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis2" style="width: 1px" type="hidden" name="tempSchDis2" runat="server">
        <input id="tempSchDis3" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis4" style="width: 1px" type="hidden" name="tempSchDis4" runat="server">
        <input id="tempSchDis5" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis6" style="width: 1px" type="hidden" name="tempSchDis6" runat="server">
        <input id="tempSchDis7" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis8" style="width: 1px" type="hidden" name="tempSchDis8" runat="server">
        <input id="tempSchDis9" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis10" style="width: 1px" type="hidden" name="tempSchDis10" runat="server">
        <input id="tempSchDis11" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis12" style="width: 1px" type="hidden" name="tempSchDis12" runat="server">
        <input id="tempSchDis13" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis14" style="width: 1px" type="hidden" name="tempSchDis14" runat="server">
        <input id="tempSchDis15" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis16" style="width: 1px" type="hidden" name="tempSchDis16" runat="server">
        <input id="tempSchDis17" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis18" style="width: 1px" type="hidden" name="tempSchDis18" runat="server">
        <input id="tempSchDis19" style="width: 1px" type="hidden" name="tempSchDis1" runat="server">
        <input id="tempSchDis20" style="width: 1px" type="hidden" name="tempSchDis20" runat="server">
        <input id="tempSchAddDis1" style="width: 1px" type="hidden" name="tempSchAddDis1" runat="server">
        <input id="tempSchAddDis2" style="width: 1px" type="hidden" name="tempSchAddDis2" runat="server">
        <input id="tempSchAddDis3" style="width: 1px" type="hidden" name="tempSchAddDis3" runat="server">
        <input id="tempSchAddDis4" style="width: 1px" type="hidden" name="tempSchAddDis4" runat="server">
        <input id="tempSchAddDis5" style="width: 1px" type="hidden" name="tempSchAddDis5" runat="server">
        <input id="tempSchAddDis6" style="width: 1px" type="hidden" name="tempSchAddDis6" runat="server">
        <input id="tempSchAddDis7" style="width: 1px" type="hidden" name="tempSchAddDis7" runat="server">
        <input id="tempSchAddDis8" style="width: 1px" type="hidden" name="tempSchAddDis8" runat="server">
        <input id="tempSchAddDis9" style="width: 1px" type="hidden" name="tempSchAddDis9" runat="server">
        <input id="tempSchAddDis10" style="width: 1px" type="hidden" name="tempSchAddDis10" runat="server">
        <input id="tempSchAddDis11" style="width: 1px" type="hidden" name="tempSchAddDis11" runat="server">
        <input id="tempSchAddDis12" style="width: 1px" type="hidden" name="tempSchAddDis12" runat="server">
        <input id="tempSchAddDis13" style="width: 1px" type="hidden" name="tempSchAddDis13" runat="server">
        <input id="tempSchAddDis14" style="width: 1px" type="hidden" name="tempSchAddDis14" runat="server">
        <input id="tempSchAddDis15" style="width: 1px" type="hidden" name="tempSchAddDis15" runat="server">
        <input id="tempSchAddDis16" style="width: 1px" type="hidden" name="tempSchAddDis16" runat="server">
        <input id="tempSchAddDis17" style="width: 1px" type="hidden" name="tempSchAddDis17" runat="server">
        <input id="tempSchAddDis18" style="width: 1px" type="hidden" name="tempSchAddDis18" runat="server">
        <input id="tempSchAddDis19" style="width: 1px" type="hidden" name="tempSchAddDis19" runat="server">
        <input id="tempSchAddDis20" style="width: 1px" type="hidden" name="tempSchAddDis20" runat="server">
        <input id="tempStktSchDis1" style="width: 1px" type="hidden" name="tempStktSchDis1" runat="server">
        <input id="tempStktSchDis2" style="width: 1px" type="hidden" name="tempStktSchDis2" runat="server">
        <input id="tempStktSchDis3" style="width: 1px" type="hidden" name="tempStktSchDis3" runat="server">
        <input id="tempStktSchDis4" style="width: 1px" type="hidden" name="tempStktSchDis4" runat="server">
        <input id="tempStktSchDis5" style="width: 1px" type="hidden" name="tempStktSchDis5" runat="server">
        <input id="tempStktSchDis6" style="width: 1px" type="hidden" name="tempStktSchDis6" runat="server">
        <input id="tempStktSchDis7" style="width: 1px" type="hidden" name="tempStktSchDis7" runat="server">
        <input id="tempStktSchDis8" style="width: 1px" type="hidden" name="tempStktSchDis8" runat="server">
        <input id="tempStktSchDis9" style="width: 1px" type="hidden" name="tempStktSchDis9" runat="server">
        <input id="tempStktSchDis10" style="width: 1px" type="hidden" name="tempStktSchDis10" runat="server">
        <input id="tempStktSchDis11" style="width: 1px" type="hidden" name="tempStktSchDis11" runat="server">
        <input id="tempStktSchDis12" style="width: 1px" type="hidden" name="tempStktSchDis12" runat="server">
        <input id="tempStktSchDis13" style="width: 1px" type="hidden" name="tempStktSchDis13" runat="server">
        <input id="tempStktSchDis14" style="width: 1px" type="hidden" name="tempStktSchDis14" runat="server">
        <input id="tempStktSchDis15" style="width: 1px" type="hidden" name="tempStktSchDis15" runat="server">
        <input id="tempStktSchDis16" style="width: 1px" type="hidden" name="tempStktSchDis16" runat="server">
        <input id="tempStktSchDis17" style="width: 1px" type="hidden" name="tempStktSchDis17" runat="server">
        <input id="tempStktSchDis18" style="width: 1px" type="hidden" name="tempStktSchDis18" runat="server">
        <input id="tempStktSchDis19" style="width: 1px" type="hidden" name="tempStktSchDis19" runat="server">
        <input id="tempStktSchDis20" style="width: 1px" type="hidden" name="tempStktSchDis20" runat="server">
        <input id="tempStktSchDisType1" style="width: 1px" type="hidden" name="tempStktSchDisType1"
            runat="server">
        <input id="tempStktSchDisType2" style="width: 1px" type="hidden" name="tempStktSchDisType2"
            runat="server">
        <input id="tempStktSchDisType3" style="width: 1px" type="hidden" name="tempStktSchDisType3"
            runat="server">
        <input id="tempStktSchDisType4" style="width: 1px" type="hidden" name="tempStktSchDisType4"
            runat="server">
        <input id="tempStktSchDisType5" style="width: 1px" type="hidden" name="tempStktSchDisType5"
            runat="server">
        <input id="tempStktSchDisType6" style="width: 1px" type="hidden" name="tempStktSchDisType6"
            runat="server">
        <input id="tempStktSchDisType7" style="width: 1px" type="hidden" name="tempStktSchDisType7"
            runat="server">
        <input id="tempStktSchDisType8" style="width: 1px" type="hidden" name="tempStktSchDisType8"
            runat="server">
        <input id="tempStktSchDisType9" style="width: 1px" type="hidden" name="tempStktSchDisType9"
            runat="server">
        <input id="tempStktSchDisType10" style="width: 1px" type="hidden" name="tempStktSchDisType10"
            runat="server">
        <input id="tempStktSchDisType11" style="width: 1px" type="hidden" name="tempStktSchDisType11"
            runat="server">
        <input id="tempStktSchDisType12" style="width: 1px" type="hidden" name="tempStktSchDisType12"
            runat="server">
        <input id="tempStktSchDisType13" style="width: 1px" type="hidden" name="tempStktSchDisType13"
            runat="server">
        <input id="tempStktSchDisType14" style="width: 1px" type="hidden" name="tempStktSchDisType14"
            runat="server">
        <input id="tempStktSchDisType15" style="width: 1px" type="hidden" name="tempStktSchDisType15"
            runat="server">
        <input id="tempStktSchDisType16" style="width: 1px" type="hidden" name="tempStktSchDisType16"
            runat="server">
        <input id="tempStktSchDisType17" style="width: 1px" type="hidden" name="tempStktSchDisType17"
            runat="server">
        <input id="tempStktSchDisType18" style="width: 1px" type="hidden" name="tempStktSchDisType18"
            runat="server">
        <input id="tempStktSchDisType19" style="width: 1px" type="hidden" name="tempStktSchDisType19"
            runat="server">
        <input id="tempStktSchDisType20" style="width: 1px" type="hidden" name="tempStktSchDisType20"
            runat="server"><input id="tempStktSchDis" style="width: 1px" type="hidden" name="tempStktSchDis" runat="server">
        <input id="tempGrandTotal" style="width: 1px" type="hidden" name="tempGrandTotal" runat="server">
        <input id="texthiddenprod" style="z-index: 2; visibility: hidden; width: 0px; position: absolute; height: 20px"
            type="text" name="texthiddenprod" runat="server">
        <input class="dropdownlist" id="temptext" style="width: 1px" type="hidden" name="temptext"
            runat="server">
        <input id="temptext12" style="z-index: 108; left: 152px; width: 16px; position: absolute; top: 0px; height: 20px"
            type="hidden" size="2" name="temptext12" runat="server">
        <input id="temptext_add1" style="z-index: 108; left: 152px; width: 16px; position: absolute; top: 0px; height: 20px"
            type="hidden" size="2" name="temptext_add1" runat="server">
        <input id="tempVndrInvoiceNo" style="z-index: 108; left: 152px; width: 16px; position: absolute; top: 0px; height: 20px"
            type="hidden" size="2" name="tempVndrInvoiceNo" runat="server">
        <input id="tempSchDiscount1" style="width: 1px" type="hidden" name="tempSchDiscount1" runat="server">
        <input id="tempSchDiscount2" style="width: 1px" type="hidden" name="tempSchDiscount2" runat="server">
        <input id="tempSchDiscount3" style="width: 1px" type="hidden" name="tempSchDiscount3" runat="server">
        <input id="tempSchDiscount4" style="width: 1px" type="hidden" name="tempSchDiscount4" runat="server">
        <input id="tempSchDiscount5" style="width: 1px" type="hidden" name="tempSchDiscount5" runat="server">
        <input id="tempSchDiscount6" style="width: 1px" type="hidden" name="tempSchDiscount6" runat="server">
        <input id="tempSchDiscount7" style="width: 1px" type="hidden" name="tempSchDiscount7" runat="server">
        <input id="tempSchDiscount8" style="width: 1px" type="hidden" name="tempSchDiscount8" runat="server">
        <input id="tempSchDiscount9" style="width: 1px" type="hidden" name="tempSchDiscount9" runat="server">
        <input id="tempSchDiscount10" style="width: 1px" type="hidden" name="tempSchDiscount10"
            runat="server">
        <input id="tempSchDiscount11" style="width: 1px" type="hidden" name="tempSchDiscount11"
            runat="server">
        <input id="tempSchDiscount12" style="width: 1px" type="hidden" name="tempSchDiscount12"
            runat="server">
        <input id="tempSchDiscount13" style="width: 1px" type="hidden" name="tempSchDiscount13"
            runat="server">
        <input id="tempSchDiscount14" style="width: 1px" type="hidden" name="tempSchDiscount14"
            runat="server">
        <input id="tempSchDiscount15" style="width: 1px" type="hidden" name="tempSchDiscount15"
            runat="server">
        <input id="tempSchDiscount16" style="width: 1px" type="hidden" name="tempSchDiscount16"
            runat="server">
        <input id="tempSchDiscount17" style="width: 1px" type="hidden" name="tempSchDiscount17"
            runat="server">
        <input id="tempSchDiscount18" style="width: 1px" type="hidden" name="tempSchDiscount18"
            runat="server">
        <input id="tempSchDiscount19" style="width: 1px" type="hidden" name="tempSchDiscount19"
            runat="server">
        <input id="tempSchDiscount20" style="width: 1px" type="hidden" name="tempSchDiscount20"
            runat="server"><input id="tempfixdisc" style="width: 1px" type="hidden" name="tempfixdisc" runat="server">
        <input id="tempStkdisc" style="width: 1px" type="hidden" name="tempStkdisc" runat="server">
        <input id="tempEBPeriod" style="width: 1px" type="hidden" name="tempEBPeriod" runat="server">
        <input id="txtDiscStatus" style="width: 1px" type="hidden" name="txtDiscStatus" runat="server">
        <input id="tempFixedDisc1" style="width: 1px" type="hidden" name="tempFixedDisc1" runat="server">
        <input id="tempFixedDisc2" style="width: 1px" type="hidden" name="tempFixedDisc2" runat="server">
        <input id="tempFixedDisc3" style="width: 1px" type="hidden" name="tempFixedDisc3" runat="server">
        <input id="tempFixedDisc4" style="width: 1px" type="hidden" name="tempFixedDisc4" runat="server">
        <input id="tempFixedDisc5" style="width: 1px" type="hidden" name="tempFixedDisc5" runat="server">
        <input id="tempFixedDisc6" style="width: 1px" type="hidden" name="tempFixedDisc6" runat="server">
        <input id="tempFixedDisc7" style="width: 1px" type="hidden" name="tempFixedDisc7" runat="server">
        <input id="tempFixedDisc8" style="width: 1px" type="hidden" name="tempFixedDisc8" runat="server">
        <input id="tempFixedDisc9" style="width: 1px" type="hidden" name="tempFixedDisc9" runat="server">
        <input id="tempFixedDisc10" style="width: 1px" type="hidden" name="tempFixedDisc10" runat="server">
        <input id="tempFixedDisc11" style="width: 1px" type="hidden" name="tempFixedDisc11" runat="server">
        <input id="tempFixedDisc12" style="width: 1px" type="hidden" name="tempFixedDisc12" runat="server">
        <input id="tempFixedDisc13" style="width: 1px" type="hidden" name="tempFixedDisc13" runat="server">
        <input id="tempFixedDisc14" style="width: 1px" type="hidden" name="tempFixedDisc14" runat="server">
        <input id="tempFixedDisc15" style="width: 1px" type="hidden" name="tempFixedDisc15" runat="server">
        <input id="tempFixedDisc16" style="width: 1px" type="hidden" name="tempFixedDisc16" runat="server">
        <input id="tempFixedDisc17" style="width: 1px" type="hidden" name="tempFixedDisc17" runat="server">
        <input id="tempFixedDisc18" style="width: 1px" type="hidden" name="tempFixedDisc18" runat="server">
        <input id="tempFixedDisc19" style="width: 1px" type="hidden" name="tempFixedDisc19" runat="server">
        <input id="tempFixedDisc20" style="width: 1px" type="hidden" name="tempFixedDisc20" runat="server">
        <input id="tempFixedDisc" style="width: 1px" type="hidden" name="tempFixedDisc" runat="server">
        <input id="Tempcgstrate" style="width: 1px" type="hidden" name="Tempcgstrate" runat="server"/>
        <input id="Tempsgstrate" style="width: 1px" type="hidden" name="Tempsgstrate" runat="server"/>
        <input id="txtMainGST" style="width: 1px" type="hidden" name="txtMainGST" runat="server"/>

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
            <INPUT id="tempTotalCgst" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempTotalCgst" runat="server"/>
            <INPUT id="tempTotalSgst" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempTotalSgst" runat="server"/>
            <INPUT id="tempTotalIgst" style="Z-INDEX: 126; LEFT: 432px; WIDTH: 8px; POSITION: absolute; TOP: 0px; HEIGHT: 20px"
				type="hidden" size="1" name="tempTotalIgst" runat="server"/>
        <input id="tempUnit" style="width: 1px" type="hidden" name="tempUnit" runat="server">
        <input id="tempUnit1" style="width: 1px" type="hidden" name="tempUnit1" runat="server">
        <input id="tempUnit2" style="width: 1px" type="hidden" name="tempUnit2" runat="server">
        <input id="tempUnit3" style="width: 1px" type="hidden" name="tempUnit3" runat="server">
        <input id="tempUnit4" style="width: 1px" type="hidden" name="tempUnit4" runat="server">
        <input id="tempUnit5" style="width: 1px" type="hidden" name="tempUnit5" runat="server">
        <input id="tempUnit6" style="width: 1px" type="hidden" name="tempUnit6" runat="server">
        <input id="tempUnit7" style="width: 1px" type="hidden" name="tempUnit7" runat="server">
        <input id="tempUnit8" style="width: 1px" type="hidden" name="tempUnit8" runat="server">
        <input id="tempUnit9" style="width: 1px" type="hidden" name="tempUnit9" runat="server">
        <input id="tempUnit10" style="width: 1px" type="hidden" name="tempUnit10" runat="server">
        <input id="tempUnit11" style="width: 1px" type="hidden" name="tempUnit11" runat="server">
        <input id="tempUnit12" style="width: 1px" type="hidden" name="tempUnit12" runat="server">
        <input id="tempUnit13" style="width: 1px" type="hidden" name="tempUnit13" runat="server">
        <input id="tempUnit14" style="width: 1px" type="hidden" name="tempUnit14" runat="server">
        <input id="tempUnit15" style="width: 1px" type="hidden" name="tempUnit15" runat="server">
        <input id="tempUnit16" style="width: 1px" type="hidden" name="tempUnit16" runat="server">
        <input id="tempUnit17" style="width: 1px" type="hidden" name="tempUnit17" runat="server">
        <input id="tempUnit18" style="width: 1px" type="hidden" name="tempUnit18" runat="server">
        <input id="tempUnit19" style="width: 1px" type="hidden" name="tempUnit19" runat="server">
        <input id="tempUnit20" style="width: 1px" type="hidden" name="tempUnit20" runat="server">
        <table style="width: 778px" align="center">
            <tr>
                <th align="center" colspan="3">
                    <font color="#ce4848">Purchase Invoice</font>
                    <hr>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="DarkGreen" Font-Size="8pt">Price updation not available for some products</asp:Label></th>
            </tr>
            <tr>
                <td align="center">
                    <table border="1">
                        <tr>
                            <td align="center" width="40%">
                                <table cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>&nbsp;&nbsp;&nbsp;Invoice No</td>
                                        <td>
                                            <asp:TextBox ID="lblInvoiceNo" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="fontstyle" Height="16px"></asp:TextBox><asp:DropDownList ID="DropInvoiceNo" runat="server" Width="114px" CssClass="dropdownlist" AutoPostBack="True" OnSelectedIndexChanged="DropInvoiceNo_SelectedIndexChanged" onmouseup="checkProd(),GetNetAmountEtaxnew()">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                </asp:DropDownList><asp:Button ID="BtnEdit" TabIndex="200" runat="server" Text="..." ToolTip="Edit Existing Record "
                                                    CausesValidation="False" OnClick="BtnEdit_Click"></asp:Button></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;&nbsp;&nbsp;Invoice Date</td>
                                        <td>
                                            <asp:TextBox ID="lblInvoiceDate" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox><a onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.lblInvoiceDate);return false;"><img class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
                                                    align="absMiddle" border="0"></a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;&nbsp;&nbsp;Mode of Payment <font color="red">&nbsp;</font>&nbsp;&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="DropModeType" runat="server" Width="100px" CssClass="dropdownlist" Height="20px" OnSelectedIndexChanged="DropModeType_SelectedIndexChanged">
                                                <asp:ListItem Value="Cash" Selected="True">Cash</asp:ListItem>
                                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                <asp:ListItem Value="DD on Delivery">DD on Delivery</asp:ListItem>
                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="center" width="60%"><font color="#990066">Vendor Information</font></FONT></U>
									<table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>Vendor&nbsp;Name&nbsp;<font color="#ff0000">*</font>
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="DropVendorID" runat="server" Width="125px" CssClass="dropdownlist" AutoPostBack="False"
                                                    onChange="getCity(this,document.Form1.lblPlace);" OnSelectedIndexChanged="DropVendorID_SelectedIndexChanged">
                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                </asp:DropDownList>&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please Select The Vendor Name"
                                                    ValueToCompare="Select" ControlToValidate="DropVendorID" Operator="NotEqual">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Place</td>
                                            <td colspan="2">
                                                <input class="dropdownlist" id="lblPlace" style="border-top-style: groove; border-right-style: groove; border-left-style: groove; border-bottom-style: groove"
                                                    readonly type="text" size="38" width="420px" name="lblPlace" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>Vehicle No&nbsp;<font color="#ff0000">*</font>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox onkeypress="return GetAnyNumber(this, event);" ID="txtVehicleNo" onkeyup="MoveFocus(this,document.Form1.txtVInnvoiceNo,event)"
                                                    runat="server" Width="100px" BorderStyle="Groove" CssClass="dropdownlist" Height="20px" MaxLength="15" OnTextChanged="txtVehicleNo_TextChanged"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Vehicle No."
                                                        ControlToValidate="txtVehicleNo">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>Invoice No&nbsp; <font color="red">*</font>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Numeric only" ControlToValidate="txtVInnvoiceNo"
                                                    ValidationExpression="\d+">*</asp:RegularExpressionValidator></td>
                                            <td colspan="2">
                                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtVInnvoiceNo"
                                                    onblur="getInvoiceNo(this);" runat="server" Width="100px" BorderStyle="Groove" CssClass="dropdownlist"
                                                    Height="20px" MaxLength="9"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ErrorMessage="Please Enter Vendor Invoice No"
                                                        ControlToValidate="txtVInnvoiceNo">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>Invoice Date <font color="#ff0000">*</font>
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtVInvoiceDate" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
                                                    CssClass="dropdownlist"></asp:TextBox><a onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtVInvoiceDate);return false;"><img class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
                                                        align="absMiddle" border="0"></a><asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ErrorMessage="Please Enter Invoice Date"
                                                            ControlToValidate="txtVInvoiceDate">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <table id="Table4" cellspacing="0" cellpadding="0" width="778" border="0">
                                    <tr>
                                        <td align="center" colspan="7"><font color="#990066"><strong><u>Products Details</u></strong></font></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3"><font color="#990066">SKU Name With Pack
													<asp:CompareValidator ID="Comparevalidator2" runat="server" ErrorMessage="Please Select The Product Type"
                                                        ValueToCompare="Type" ControlToValidate="DropType1" Operator="NotEqual">*</asp:CompareValidator></font></td>
                                        <!--TD style="WIDTH: 158px" align="center"><FONT color="#990066">Name</FONT></TD>
											<TD align="center"><FONT color="#990066">Package</FONT></TD-->
                                        <td style="width: 54px" align="center"><font color="#990066">Qty
													<asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ErrorMessage="Please Enter Qty" ControlToValidate="txtQty1">*</asp:RequiredFieldValidator></font></td>
                                        <td align="center"><font color="#990066">Rate</font></td>
                                        <td align="center"><font color="#990066">Amount</font></td>
                                        <td align="center"><font color="#990066">FOC</font></td>
                                        <td align="center"><font color="#990066">&nbsp;Bat&nbsp;</font></td>
                                        <td align="center"><font color="#990066">SchDisc</font></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType1"
                                                onkeyup="search3(this,document.Form1.DropProdName1,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName1,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName1,event,document.Form1.DropType1,document.Form1.txtQty1),getStock1(this,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName1)"
                                                value="Type" name="DropType1" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName1,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName1),get_EBD()" readOnly type="text" name="temp4"><br>
                                            <div id="Layer2" style="Z-INDEX: 2; POSITION: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType1,document.Form1.txtQty1),getStock1(document.Form1.DropType1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1)"
                                                    id="DropProdName1" ondblclick="select(this,document.Form1.DropType1),getStock1(document.Form1.DropType1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty1,document.Form1.DropType1)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType1),getStock1(document.Form1.DropType1,document.Form1.txtRate1,document.Form1.txtQty1,document.Form1.txtAmount1)"
                                                    multiple name="DropProdName1">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty1" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType2,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate1" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount1" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc1" onclick="GetFOC(this,document.Form1.txtAmount1)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType1,document.Form1.lblInvoiceNo,document.Form1.txtQty1)"
                                                type="checkbox" name="chkbatch1"></td>
                                        <td align="center">
                                            <input id="chkSchDisc1" onclick="getSchDisc(this,document.Form1.DropType1,document.Form1.lblInvoiceNo,document.Form1.txtQty1,document.Form1.tempSchDiscount1)"
                                                type="checkbox" name="chkSchDisc1" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType2"
                                                onkeyup="search3(this,document.Form1.DropProdName2,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName2,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName2,event,document.Form1.DropType2,document.Form1.txtQty2),getStock1(this,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName2)"
                                                value="Type" name="DropType2" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName2,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName2)" readonly type="text"
                                                    name="temp2"><br>
                                            <div id="Layer3" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType2,document.Form1.txtQty2),getStock1(document.Form1.DropType2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2)"
                                                    id="DropProdName2" ondblclick="select(this,document.Form1.DropType2),getStock1(document.Form1.DropType2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty2,document.Form1.DropType2)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType2),getStock1(document.Form1.DropType2,document.Form1.txtRate2,document.Form1.txtQty2,document.Form1.txtAmount2)"
                                                    multiple name="DropProdName2">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty2" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType3,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td style="width: 54px">
                                            <asp:TextBox ID="txtRate2" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount2" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc2" onclick="GetFOC(this,document.Form1.txtAmount2)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType2,document.Form1.lblInvoiceNo,document.Form1.txtQty2)"
                                                type="checkbox" name="chkbatch2"></td>
                                        <td align="center">
                                            <input id="chkSchDisc2" onclick="getSchDisc(this,document.Form1.DropType2,document.Form1.lblInvoiceNo,document.Form1.txtQty2,document.Form1.tempSchDiscount2)"
                                                type="checkbox" name="chkSchDisc2" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType3"
                                                onkeyup="search3(this,document.Form1.DropProdName3,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName3,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName3,event,document.Form1.DropType3,document.Form1.txtQty3),getStock1(this,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName3)"
                                                value="Type" name="DropType3" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName3,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName3)" readonly type="text"
                                                    name="temp4"><br>
                                            <div id="Layer4" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType3,document.Form1.txtQty3),getStock1(document.Form1.DropType3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3)"
                                                    id="DropProdName3" ondblclick="select(this,document.Form1.DropType3),getStock1(document.Form1.DropType3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty3,document.Form1.DropType3)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType3),getStock1(document.Form1.DropType3,document.Form1.txtRate3,document.Form1.txtQty3,document.Form1.txtAmount3)"
                                                    multiple name="DropProdName3">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty3" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType4,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td style="width: 54px">
                                            <asp:TextBox ID="txtRate3" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount3" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc3" onclick="GetFOC(this,document.Form1.txtAmount3)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType3,document.Form1.lblInvoiceNo,document.Form1.txtQty3)"
                                                type="checkbox" name="chkbatch3"></td>
                                        <td align="center">
                                            <input id="chkSchDisc3" onclick="getSchDisc(this,document.Form1.DropType3,document.Form1.lblInvoiceNo,document.Form1.txtQty3,document.Form1.tempSchDiscount3)"
                                                type="checkbox" name="chkSchDisc3" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType4"
                                                onkeyup="search3(this,document.Form1.DropProdName4,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName4,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName4,event,document.Form1.DropType4,document.Form1.txtQty4),getStock1(this,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName4)"
                                                value="Type" name="DropType4" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName4,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName4)" readonly type="text"
                                                    name="temp5"><br>
                                            <div id="Layer5" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType4,document.Form1.txtQty4),getStock1(document.Form1.DropType4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4)"
                                                    id="DropProdName4" ondblclick="select(this,document.Form1.DropType4),getStock1(document.Form1.DropType4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty4,document.Form1.DropType4)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType4),getStock1(document.Form1.DropType4,document.Form1.txtRate4,document.Form1.txtQty4,document.Form1.txtAmount4)"
                                                    multiple name="DropProdName4">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty4" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType5,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate4" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount4" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc4" onclick="GetFOC(this,document.Form1.txtAmount4)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType4,document.Form1.lblInvoiceNo,document.Form1.txtQty4)"
                                                type="checkbox" name="chkbatch4"></td>
                                        <td align="center">
                                            <input id="chkSchDisc4" onclick="getSchDisc(this,document.Form1.DropType4,document.Form1.lblInvoiceNo,document.Form1.txtQty4,document.Form1.tempSchDiscount4)"
                                                type="checkbox" name="chkSchDisc4" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType5"
                                                onkeyup="search3(this,document.Form1.DropProdName5,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName5,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName5,event,document.Form1.DropType5,document.Form1.txtQty5),getStock1(this,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName5)"
                                                value="Type" name="DropType5" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName5,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName5)" readonly type="text"
                                                    name="temp5"><br>
                                            <div id="Layer6" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType5,document.Form1.txtQty5),getStock1(document.Form1.DropType5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5)"
                                                    id="DropProdName5" ondblclick="select(this,document.Form1.DropType5),getStock1(document.Form1.DropType5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty5,document.Form1.DropType5)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType5),getStock1(document.Form1.DropType5,document.Form1.txtRate5,document.Form1.txtQty5,document.Form1.txtAmount5)"
                                                    multiple name="DropProdName5">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty5" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType6,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate5" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount5" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc5" onclick="GetFOC(this,document.Form1.txtAmount5)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType5,document.Form1.lblInvoiceNo,document.Form1.txtQty5)"
                                                type="checkbox" name="chkbatch5"></td>
                                        <td align="center">
                                            <input id="chkSchDisc5" onclick="getSchDisc(this,document.Form1.DropType5,document.Form1.lblInvoiceNo,document.Form1.txtQty5,document.Form1.tempSchDiscount5)"
                                                type="checkbox" name="chkSchDisc5" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType6"
                                                onkeyup="search3(this,document.Form1.DropProdName6,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName6,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName6,event,document.Form1.DropType6,document.Form1.txtQty6),getStock1(this,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName6)"
                                                value="Type" name="DropType6" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName6,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName6)" readonly type="text"
                                                    name="temp6"><br>
                                            <div id="Layer7" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType6,document.Form1.txtQty6),getStock1(document.Form1.DropType6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6)"
                                                    id="DropProdName6" ondblclick="select(this,document.Form1.DropType6),getStock1(document.Form1.DropType6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty6,document.Form1.DropType6)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType6),getStock1(document.Form1.DropType6,document.Form1.txtRate6,document.Form1.txtQty6,document.Form1.txtAmount6)"
                                                    multiple name="DropProdName6">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty6" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType7,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate6" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount6" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc6" onclick="GetFOC(this,document.Form1.txtAmount6)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType6,document.Form1.lblInvoiceNo,document.Form1.txtQty6)"
                                                type="checkbox" name="chkbatch6"></td>
                                        <td align="center">
                                            <input id="chkSchDisc6" onclick="getSchDisc(this,document.Form1.DropType6,document.Form1.lblInvoiceNo,document.Form1.txtQty6,document.Form1.tempSchDiscount6)"
                                                type="checkbox" name="chkSchDisc6" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType7"
                                                onkeyup="search3(this,document.Form1.DropProdName7,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName7,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName7,event,document.Form1.DropType7,document.Form1.txtQty7),getStock1(this,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName7)"
                                                value="Type" name="DropType7" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName7,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName7)" readonly type="text"
                                                    name="temp7"><br>
                                            <div id="Layer8" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType7,document.Form1.txtQty7),getStock1(document.Form1.DropType7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7)"
                                                    id="DropProdName7" ondblclick="select(this,document.Form1.DropType7),getStock1(document.Form1.DropType7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty7,document.Form1.DropType7)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType7),getStock1(document.Form1.DropType7,document.Form1.txtRate7,document.Form1.txtQty7,document.Form1.txtAmount7)"
                                                    multiple name="DropProdName7">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty7" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType8,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate7" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount7" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc7" onclick="GetFOC(this,document.Form1.txtAmount7)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType7,document.Form1.lblInvoiceNo,document.Form1.txtQty7)"
                                                type="checkbox" name="chkbatch7"></td>
                                        <td align="center">
                                            <input id="chkSchDisc7" onclick="getSchDisc(this,document.Form1.DropType7,document.Form1.lblInvoiceNo,document.Form1.txtQty7,document.Form1.tempSchDiscount7)"
                                                type="checkbox" name="chkSchDisc7" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType8"
                                                onkeyup="search3(this,document.Form1.DropProdName8,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName8,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName8,event,document.Form1.DropType8,document.Form1.txtQty8),getStock1(this,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName8)"
                                                value="Type" name="DropType8" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName8,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName8)" readonly type="text"
                                                    name="temp8"><br>
                                            <div id="Layer9" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType8,document.Form1.txtQty8),getStock1(document.Form1.DropType8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8)"
                                                    id="DropProdName8" ondblclick="select(this,document.Form1.DropType8),getStock1(document.Form1.DropType8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty8,document.Form1.DropType8)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType8),getStock1(document.Form1.DropType8,document.Form1.txtRate8,document.Form1.txtQty8,document.Form1.txtAmount8)"
                                                    multiple name="DropProdName8">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty8" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType9,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate8" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount8" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc8" onclick="GetFOC(this,document.Form1.txtAmount8)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType8,document.Form1.lblInvoiceNo,document.Form1.txtQty8)"
                                                type="checkbox" name="chkbatch8"></td>
                                        <td align="center">
                                            <input id="chkSchDisc8" onclick="getSchDisc(this,document.Form1.DropType8,document.Form1.lblInvoiceNo,document.Form1.txtQty8,document.Form1.tempSchDiscount8)"
                                                type="checkbox" name="chkSchDisc8" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType9"
                                                onkeyup="search3(this,document.Form1.DropProdName9,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName9,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName9,event,document.Form1.DropType9,document.Form1.txtQty9),getStock1(this,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName9,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName9)"
                                                value="Type" name="DropType9" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName9,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName9)" readonly type="text"
                                                    name="temp9"><br>
                                            <div id="Layer10" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType9,document.Form1.txtQty9),getStock1(document.Form1.DropType9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9)"
                                                    id="DropProdName9" ondblclick="select(this,document.Form1.DropType9),getStock1(document.Form1.DropType9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty9,document.Form1.DropType9)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType9),getStock1(document.Form1.DropType9,document.Form1.txtRate9,document.Form1.txtQty9,document.Form1.txtAmount9)"
                                                    multiple name="DropProdName9">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty9" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType10,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate9" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount9" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc9" onclick="GetFOC(this,document.Form1.txtAmount9)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType9,document.Form1.lblInvoiceNo,document.Form1.txtQty9)"
                                                type="checkbox" name="chkbatch9"></td>
                                        <td align="center">
                                            <input id="chkSchDisc9" onclick="getSchDisc(this,document.Form1.DropType9,document.Form1.lblInvoiceNo,document.Form1.txtQty9,document.Form1.tempSchDiscount9)"
                                                type="checkbox" name="chkSchDisc9" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType10"
                                                onkeyup="search3(this,document.Form1.DropProdName10,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName10,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName10,event,document.Form1.DropType10,document.Form1.txtQty10),getStock1(this,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName10,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName10)"
                                                value="Type" name="DropType10" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName10,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName10)" readonly type="text"
                                                    name="temp10"><br>
                                            <div id="Layer11" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType10,document.Form1.txtQty10),getStock1(document.Form1.DropType10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10)"
                                                    id="DropProdName10" ondblclick="select(this,document.Form1.DropType10),getStock1(document.Form1.DropType10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty10,document.Form1.DropType10)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType10),getStock1(document.Form1.DropType10,document.Form1.txtRate10,document.Form1.txtQty10,document.Form1.txtAmount10)"
                                                    multiple name="DropProdName10">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty10" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType11,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate10" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount10" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc10" onclick="GetFOC(this,document.Form1.txtAmount10)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType10,document.Form1.lblInvoiceNo,document.Form1.txtQty10)"
                                                type="checkbox" name="chkbatch10"></td>
                                        <td align="center">
                                            <input id="chkSchDisc10" onclick="getSchDisc(this,document.Form1.DropType10,document.Form1.lblInvoiceNo,document.Form1.txtQty10,document.Form1.tempSchDiscount10)"
                                                type="checkbox" name="chkSchDisc10" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType11"
                                                onkeyup="search3(this,document.Form1.DropProdName11,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName11,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName11,event,document.Form1.DropType11,document.Form1.txtQty11),getStock1(this,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName11,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName11)"
                                                value="Type" name="DropType11" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName11,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName11)" readonly type="text"
                                                    name="temp11"><br>
                                            <div id="Layer12" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType11,document.Form1.txtQty11),getStock1(document.Form1.DropType11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11)"
                                                    id="DropProdName11" ondblclick="select(this,document.Form1.DropType11),getStock1(document.Form1.DropType11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty11,document.Form1.DropType11)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType11),getStock1(document.Form1.DropType11,document.Form1.txtRate11,document.Form1.txtQty11,document.Form1.txtAmount11)"
                                                    multiple name="DropProdName11">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty11" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType12,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate11" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount11" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc11" onclick="GetFOC(this,document.Form1.txtAmount11)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType11,document.Form1.lblInvoiceNo,document.Form1.txtQty11)"
                                                type="checkbox" name="chkbatch11"></td>
                                        <td align="center">
                                            <input id="chkSchDisc11" onclick="getSchDisc(this,document.Form1.DropType11,document.Form1.lblInvoiceNo,document.Form1.txtQty11,document.Form1.tempSchDiscount11)"
                                                type="checkbox" name="chkSchDisc11" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType12"
                                                onkeyup="search3(this,document.Form1.DropProdName12,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName12,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName12,event,document.Form1.DropType12,document.Form1.txtQty12),getStock1(this,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName12,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName12)"
                                                value="Type" name="DropType12" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName12,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName12)" readonly type="text"
                                                    name="temp12"><br>
                                            <div id="Layer13" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType12,document.Form1.txtQty12),getStock1(document.Form1.DropType12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12)"
                                                    id="DropProdName12" ondblclick="select(this,document.Form1.DropType12),getStock1(document.Form1.DropType12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty12,document.Form1.DropType12)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType12),getStock1(document.Form1.DropType12,document.Form1.txtRate12,document.Form1.txtQty12,document.Form1.txtAmount12)"
                                                    multiple name="DropProdName12">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty12" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType13,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate12" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount12" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc12" onclick="GetFOC(this,document.Form1.txtAmount12)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType12,document.Form1.lblInvoiceNo,document.Form1.txtQty12)"
                                                type="checkbox" name="chkbatch12"></td>
                                        <td align="center">
                                            <input id="chkSchDisc12" onclick="getSchDisc(this,document.Form1.DropType12,document.Form1.lblInvoiceNo,document.Form1.txtQty12,document.Form1.tempSchDiscount12)"
                                                type="checkbox" name="chkSchDisc12" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType13"
                                                onkeyup="search3(this,document.Form1.DropProdName13,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName13,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName13,event,document.Form1.DropType13,document.Form1.txtQty13),getStock1(this,document.Form1.txtRate13,document.Form1.txtQty13,document.Form1.txtAmount13)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName13,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName13)"
                                                value="Type" name="DropType13" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName13,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName13)" readonly type="text"
                                                    name="temp12"><br>
                                            <div id="Layer14" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType13,document.Form1.txtQty13),getStock1(document.Form1.DropType13,document.Form1.txtRate13,document.Form1.txtQty13,document.Form1.txtAmount13)"
                                                    id="DropProdName13" ondblclick="select(this,document.Form1.DropType13),getStock1(document.Form1.DropType13,document.Form1.txtRate13,document.Form1.txtQty13,document.Form1.txtAmount13)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty13,document.Form1.DropType13)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType13),getStock1(document.Form1.DropType13,document.Form1.txtRate13,document.Form1.txtQty13,document.Form1.txtAmount13)"
                                                    multiple name="DropProdName13">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty13" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType14,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate13" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount13" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc13" onclick="GetFOC(this,document.Form1.txtAmount13)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType13,document.Form1.lblInvoiceNo,document.Form1.txtQty13)"
                                                type="checkbox" name="chkbatch13"></td>
                                        <td align="center">
                                            <input id="chkSchDisc13" onclick="getSchDisc(this,document.Form1.DropType13,document.Form1.lblInvoiceNo,document.Form1.txtQty13,document.Form1.tempSchDiscount13)"
                                                type="checkbox" name="chkSchDisc13" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType14"
                                                onkeyup="search3(this,document.Form1.DropProdName14,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName14,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName14,event,document.Form1.DropType14,document.Form1.txtQty14),getStock1(this,document.Form1.txtRate14,document.Form1.txtQty14,document.Form1.txtAmount14)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName14,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName14)"
                                                value="Type" name="DropType14" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName14,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName14)" readonly type="text"
                                                    name="temp14"><br>
                                            <div id="Layer15" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType14,document.Form1.txtQty14),getStock1(document.Form1.DropType14,document.Form1.txtRate14,document.Form1.txtQty14,document.Form1.txtAmount14)"
                                                    id="DropProdName14" ondblclick="select(this,document.Form1.DropType14),getStock1(document.Form1.DropType14,document.Form1.txtRate14,document.Form1.txtQty14,document.Form1.txtAmount14)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty14,document.Form1.DropType14)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType14),getStock1(document.Form1.DropType14,document.Form1.txtRate14,document.Form1.txtQty14,document.Form1.txtAmount14)"
                                                    multiple name="DropProdName14">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty14" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType15,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate14" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount14" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc14" onclick="GetFOC(this,document.Form1.txtAmount14)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType14,document.Form1.lblInvoiceNo,document.Form1.txtQty14)"
                                                type="checkbox" name="chkbatch14"></td>
                                        <td align="center">
                                            <input id="chkSchDisc14" onclick="getSchDisc(this,document.Form1.DropType14,document.Form1.lblInvoiceNo,document.Form1.txtQty14,document.Form1.tempSchDiscount14)"
                                                type="checkbox" name="chkSchDisc14" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType15"
                                                onkeyup="search3(this,document.Form1.DropProdName15,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName15,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName15,event,document.Form1.DropType15,document.Form1.txtQty15),getStock1(this,document.Form1.txtRate15,document.Form1.txtQty15,document.Form1.txtAmount15)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName15,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName15)"
                                                value="Type" name="DropType15" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName15,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName15)" readonly type="text"
                                                    name="temp15"><br>
                                            <div id="Layer16" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType15,document.Form1.txtQty15),getStock1(document.Form1.DropType15,document.Form1.txtRate15,document.Form1.txtQty15,document.Form1.txtAmount15)"
                                                    id="DropProdName15" ondblclick="select(this,document.Form1.DropType15),getStock1(document.Form1.DropType15,document.Form1.txtRate15,document.Form1.txtQty15,document.Form1.txtAmount15)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty15,document.Form1.DropType15)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType15),getStock1(document.Form1.DropType15,document.Form1.txtRate15,document.Form1.txtQty15,document.Form1.txtAmount15)"
                                                    multiple name="DropProdName15">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty15" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType16,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate15" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount15" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc15" onclick="GetFOC(this,document.Form1.txtAmount15)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType15,document.Form1.lblInvoiceNo,document.Form1.txtQty15)"
                                                type="checkbox" name="chkbatch15"></td>
                                        <td align="center">
                                            <input id="chkSchDisc15" onclick="getSchDisc(this,document.Form1.DropType15,document.Form1.lblInvoiceNo,document.Form1.txtQty15,document.Form1.tempSchDiscount15)"
                                                type="checkbox" name="chkSchDisc15" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType16"
                                                onkeyup="search3(this,document.Form1.DropProdName16,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName16,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName16,event,document.Form1.DropType16,document.Form1.txtQty16),getStock1(this,document.Form1.txtRate16,document.Form1.txtQty16,document.Form1.txtAmount16)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName16,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName16)"
                                                value="Type" name="DropType16" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName16,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName16)" readonly type="text"
                                                    name="temp16"><br>
                                            <div id="Layer17" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType16,document.Form1.txtQty16),getStock1(document.Form1.DropType16,document.Form1.txtRate16,document.Form1.txtQty16,document.Form1.txtAmount16)"
                                                    id="DropProdName16" ondblclick="select(this,document.Form1.DropType16),getStock1(document.Form1.DropType16,document.Form1.txtRate16,document.Form1.txtQty16,document.Form1.txtAmount16)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty16,document.Form1.DropType16)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType16),getStock1(document.Form1.DropType16,document.Form1.txtRate16,document.Form1.txtQty16,document.Form1.txtAmount16)"
                                                    multiple name="DropProdName16">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty16" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType17,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate16" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount16" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc16" onclick="GetFOC(this,document.Form1.txtAmount16)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType16,document.Form1.lblInvoiceNo,document.Form1.txtQty16)"
                                                type="checkbox" name="chkbatch16"></td>
                                        <td align="center">
                                            <input id="chkSchDisc16" onclick="getSchDisc(this,document.Form1.DropType16,document.Form1.lblInvoiceNo,document.Form1.txtQty16,document.Form1.tempSchDiscount16)"
                                                type="checkbox" name="chkSchDisc16" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType17"
                                                onkeyup="search3(this,document.Form1.DropProdName17,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName17,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName17,event,document.Form1.DropType17,document.Form1.txtQty17),getStock1(this,document.Form1.txtRate17,document.Form1.txtQty17,document.Form1.txtAmount17)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName17,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName17)"
                                                value="Type" name="DropType17" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName17,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName17)" readonly type="text"
                                                    name="temp17"><br>
                                            <div id="Layer18" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType17,document.Form1.txtQty17),getStock1(document.Form1.DropType17,document.Form1.txtRate17,document.Form1.txtQty17,document.Form1.txtAmount17)"
                                                    id="DropProdName17" ondblclick="select(this,document.Form1.DropType17),getStock1(document.Form1.DropType17,document.Form1.txtRate17,document.Form1.txtQty17,document.Form1.txtAmount17)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty17,document.Form1.DropType17)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType17),getStock1(document.Form1.DropType17,document.Form1.txtRate17,document.Form1.txtQty17,document.Form1.txtAmount17)"
                                                    multiple name="DropProdName17">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty17" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType18,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate17" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount17" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc17" onclick="GetFOC(this,document.Form1.txtAmount17)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType17,document.Form1.lblInvoiceNo,document.Form1.txtQty17)"
                                                type="checkbox" name="chkbatch17"></td>
                                        <td align="center">
                                            <input id="chkSchDisc17" onclick="getSchDisc(this,document.Form1.DropType17,document.Form1.lblInvoiceNo,document.Form1.txtQty17,document.Form1.tempSchDiscount17)"
                                                type="checkbox" name="chkSchDisc17" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType18"
                                                onkeyup="search3(this,document.Form1.DropProdName18,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName18,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName18,event,document.Form1.DropType18,document.Form1.txtQty18),getStock1(this,document.Form1.txtRate18,document.Form1.txtQty18,document.Form1.txtAmount18)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName18,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName18)"
                                                value="Type" name="DropType18" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName18,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName18)" readonly type="text"
                                                    name="temp18"><br>
                                            <div id="Layer19" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType18,document.Form1.txtQty18),getStock1(document.Form1.DropType18,document.Form1.txtRate18,document.Form1.txtQty18,document.Form1.txtAmount18)"
                                                    id="DropProdName18" ondblclick="select(this,document.Form1.DropType18),getStock1(document.Form1.DropType18,document.Form1.txtRate18,document.Form1.txtQty18,document.Form1.txtAmount18)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.DropType19,document.Form1.DropType18)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType18),getStock1(document.Form1.DropType18,document.Form1.txtRate18,document.Form1.txtQty18,document.Form1.txtAmount18)"
                                                    multiple name="DropProdName18">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty18" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType19,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate18" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount18" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc18" onclick="GetFOC(this,document.Form1.txtAmount18)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType18,document.Form1.lblInvoiceNo,document.Form1.txtQty18)"
                                                type="checkbox" name="chkbatch18"></td>
                                        <td align="center">
                                            <input id="chkSchDisc18" onclick="getSchDisc(this,document.Form1.DropType18,document.Form1.lblInvoiceNo,document.Form1.txtQty18,document.Form1.tempSchDiscount18)"
                                                type="checkbox" name="chkSchDisc18" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType19"
                                                onkeyup="search3(this,document.Form1.DropProdName19,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName19,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName19,event,document.Form1.DropType19,document.Form1.txtQty19),getStock1(this,document.Form1.txtRate19,document.Form1.txtQty19,document.Form1.txtAmount19)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName19,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName19)"
                                                value="Type" name="DropType19" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName19,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName19)" readonly type="text"
                                                    name="temp19"><br>
                                            <div id="Layer20" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType19,document.Form1.txtQty19),getStock1(document.Form1.DropType19,document.Form1.txtRate19,document.Form1.txtQty19,document.Form1.txtAmount19)"
                                                    id="DropProdName19" ondblclick="select(this,document.Form1.DropType19),getStock1(document.Form1.DropType19,document.Form1.txtRate19,document.Form1.txtQty19,document.Form1.txtAmount19)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty19,document.Form1.DropType19)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType19),getStock1(document.Form1.DropType19,document.Form1.txtRate19,document.Form1.txtQty19,document.Form1.txtAmount19)"
                                                    multiple name="DropProdName19">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty19" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.DropType20,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate19" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount19" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc19" onclick="GetFOC(this,document.Form1.txtAmount19)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType19,document.Form1.lblInvoiceNo,document.Form1.txtQty19)"
                                                type="checkbox" name="chkbatch19"></td>
                                        <td align="center">
                                            <input id="chkSchDisc19" onclick="getSchDisc(this,document.Form1.DropType19,document.Form1.lblInvoiceNo,document.Form1.txtQty19,document.Form1.tempSchDiscount19)"
                                                type="checkbox" name="chkSchDisc19" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <input class="TextBoxStyle" onkeypress="return GetAnyNumber(this, event);" id="DropType20"
                                                onkeyup="search3(this,document.Form1.DropProdName20,document.Form1.texthiddenprod.value),arrowkeydown(this,event,document.Form1.DropProdName20,document.Form1.texthiddenprod),Selectbyenter(document.Form1.DropProdName20,event,document.Form1.DropType20,document.Form1.txtQty20),getStock1(this,document.Form1.txtRate20,document.Form1.txtQty20,document.Form1.txtAmount20)"
                                                style="z-index: 10; visibility: visible; width: 285px; height: 19px" onclick="search1(document.Form1.DropProdName20,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName20)"
                                                value="Type" name="DropType20" runat="server"><input class="ComboBoxSearchButtonStyle" onkeypress="return GetAnyNumber(this, event);"
                                                    onclick="search1(document.Form1.DropProdName20,document.Form1.texthiddenprod),dropshow(document.Form1.DropProdName20)" readonly type="text"
                                                    name="temp20"><br>
                                            <div id="Layer21" style="z-index: 2; position: absolute">
                                                <select class="ListBoxborderstyle" onkeypress="Selectbyenter(this,event,document.Form1.DropType20,document.Form1.txtQty20),getStock1(document.Form1.DropType20,document.Form1.txtRate20,document.Form1.txtQty20,document.Form1.txtAmount20)"
                                                    id="DropProdName20" ondblclick="select(this,document.Form1.DropType20),getStock1(document.Form1.DropType20,document.Form1.txtRate20,document.Form1.txtQty20,document.Form1.txtAmount20)"
                                                    onkeyup="arrowkeyselect(this,event,document.Form1.txtQty20,document.Form1.DropType20)" 
                                                    style="Z-INDEX: 10; VISIBILITY: hidden; WIDTH: 310px; HEIGHT: 0px"
                                                    onfocusout="HideList(this,document.Form1.DropType20),getStock1(document.Form1.DropType20,document.Form1.txtRate20,document.Form1.txtQty20,document.Form1.txtAmount20)"
                                                    multiple name="DropProdName20">
                                                </select>
                                            </div>
                                        </td>
                                        <td style="width: 115px">
                                            <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,false);" ID="txtQty20" onblur="calc(),get_EBD()"
                                                onkeyup="MoveFocus(this,document.Form1.txtRemark,event)" runat="server" Width="115px" BorderStyle="Groove" CssClass="dropdownlist"
                                                MaxLength="5"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRate20" onblur="calc()" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtAmount20" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td align="center">
                                            <asp:CheckBox ID="chkfoc20" onclick="GetFOC(this,document.Form1.txtAmount20)" runat="server"></asp:CheckBox></td>
                                        <td align="center">
                                            <input onclick="getBatch(this,document.Form1.DropType20,document.Form1.lblInvoiceNo,document.Form1.txtQty20)"
                                                type="checkbox" name="chkbatch20"></td>
                                        <td align="center">
                                            <input id="chkSchDisc20" onclick="getSchDisc(this,document.Form1.DropType20,document.Form1.lblInvoiceNo,document.Form1.txtQty20,document.Form1.tempSchDiscount20)"
                                                type="checkbox" name="chkSchDisc20" runat="server"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">Total</td>
                                        <td>
                                            <asp:TextBox ID="txttotalqtyltr1" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox>
                                        <td align="center">Total</td>
                                        <td style="width: 115px">
                                            <asp:TextBox ID="txttotalqty" runat="server" Width="115px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>
                                        <td style="width: 54px">&nbsp;</td>
                                        <td></td>
                                        <td align="center"></td>
                                        <td align="center"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">&nbsp; Ltr/Kg</td>
                                        <td>
                                            <asp:TextBox ID="txttotalqtyltr" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove"
                                                CssClass="dropdownlist"></asp:TextBox></td>

                                        <td style="width: 54px">&nbsp;</td>
                                        <td></td>
                                        <td align="center"></td>
                                        <td align="center"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 770px" cellspacing="0" cellpadding="0" >
                        <tr>
                            <td>Sch. Discount</td>
                            <td>
                                <asp:TextBox ID="txtPromoScheme" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox></td>
                            <td width="175">&nbsp;Grand Total</td>
                            <td>
                                <asp:TextBox ID="txtGrandTotal" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
                                    CssClass="dropdownlist"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>FOC Discount</td>
                            <td>
                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtfoc" runat="server"
                                    Width="120px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox><asp:DropDownList ID="dropfoc" runat="server" Width="45px" CssClass="dropdownlist" onchange="GetNetAmountEtaxnew()"
                                        Enabled="False">
                                        <asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
                                        <asp:ListItem Value="Per">%</asp:ListItem>
                                    </asp:DropDownList></td>
                            <td>&nbsp;EarlyBird Discount &nbsp;</td>
                            <td>
                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtebird" onblur="GetNetAmountEtaxnew(),get_EBD()"
                                    runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:TextBox>&nbsp;<asp:TextBox ID="txtebirdamt" onblur="GetNetAmountEtaxnew(),get_EBD()" runat="server" Width="80px"
                                        ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox>&nbsp;<font color="#990066">Minus</font>&nbsp;<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, true,true);" ID="txtbirdless" onblur="GetNetAmountEtaxnew(),get_EBD()"
                                            runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="6"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Discount(%)</td>
                            <td style="height: 21px">
                                <asp:TextBox ID="txtfixedamt" runat="server" Width="70px" ReadOnly="True" BorderStyle="Groove"
                                        CssClass="dropdownlist" OnTextChanged="txtfixed_TextChanged"></asp:TextBox></td>
                            <td>&nbsp;Servo Stk. Discount&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txttradedisamt" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove"
                                    CssClass="dropdownlist"></asp:TextBox>&nbsp;<font color="#990066">Minus</font>&nbsp;<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, true,true);" ID="txttradeless" onblur="GetNetAmountEtaxnew()"
                                        runat="server" Width="140px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="6"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 21px">
                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtfixed" runat="server"
                                    Visible="False" Width="1px" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox>Addl. 
									Discount(%)&nbsp;&nbsp;<td><asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtAddDis" runat="server"
                                        Width="60px" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox></td></td>
                            <td style="height: 21px">&nbsp;Credit/Cash Discount</td>
                            <td style="height: 21px">
                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtCashDisc" onblur="GetNetAmountEtaxnew()"
                                    runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:TextBox>&nbsp;<asp:DropDownList ID="DropCashDiscType" runat="server" Width="45px" CssClass="dropdownlist" onchange="GetNetAmountEtaxnew()">
                                        <asp:ListItem Value="Rs">Rs.</asp:ListItem>
                                        <asp:ListItem Value="Per" Selected="True">%</asp:ListItem>
                                    </asp:DropDownList>&nbsp;<asp:TextBox ID="txtTotalCashDisc" onblur="GetNetAmountEtaxnew()" runat="server" Width="80px"
                                        BorderStyle="Groove" CssClass="dropdownlist" OnTextChanged="txtTotalCashDisc_TextChanged"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Message</td>
                            <td>
                                <asp:TextBox ID="txtMessage" runat="server" Width="120px" ReadOnly="True" BorderStyle="Groove"
                                    CssClass="dropdownlist"></asp:TextBox></td>
                            <td>&nbsp;Discount&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, true,true);" ID="txtDisc" onblur="GetNetAmountEtaxnew()"
                                    runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="6"></asp:TextBox>&nbsp;<asp:DropDownList ID="DropDiscType" runat="server" Width="45px" CssClass="dropdownlist" onchange="GetNetAmountEtaxnew()">
                                        <asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
                                        <asp:ListItem Value="Per">%</asp:ListItem>
                                    </asp:DropDownList>&nbsp;<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtTotalDisc" onblur="GetNetAmountEtaxnew()"
                                        runat="server" Width="80px" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>&nbsp;Remark</td>
                            <td><asp:TextBox ID="txtRemark" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="49"></asp:TextBox></td>
                            <td align="left">
                                <table width="100%">
                                    <tr>
                                        <td width="2px">IGST</td>
                                        <td width="80px">
                                            <asp:RadioButton ID="No"   onclick="return GetNetAmountEtaxnew();" runat="server" ToolTip="Not Applied" Checked="true" GroupName="VAT"></asp:RadioButton>
                                            <asp:RadioButton ID="Yes" onclick="return GetNetAmountEtaxnew();" runat="server" ToolTip="Apply" Checked="false" GroupName="VAT"></asp:RadioButton>
                                        </td>
                                     </tr>
                                </table>
                            </td>
                            <td><asp:TextBox ID="txtVAT" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist" OnTextChanged="txtVAT_TextChanged"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td></td>
                            <td  align="left"">
                                <table width="100%">
                                    <tr>
                                    <td width="2px">CGST</td>
                                    <td width="95px">
                                        <asp:RadioButton ID="N"  onclick="return GetNetAmountEtaxnew()" runat="server" Width="39px" ToolTip="Not Applied" Checked="false" GroupName="cgst"></asp:RadioButton>
                                        <asp:RadioButton ID="Y" onclick="return GetNetAmountEtaxnew()" runat="server" Width="75px" ToolTip="Applied" Checked="true" GroupName="cgst"></asp:RadioButton>
                                    </td>
                                    </tr>
                                </table>
                            </td>
                            <td><asp:TextBox ID="Textcgst" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td align="left">
                                <table width="100%">
                                    <tr>
                                        <td width="2px">SGST</td>
                                        <td width="95px">
                                            <asp:RadioButton  ID="Noo" onclick="return GetNetAmountEtaxnew()" runat="server" Width="39px" ToolTip="Not Applied" Checked="false" GroupName="sgst"></asp:RadioButton>
                                            <asp:RadioButton ID="Yess" onclick="return GetNetAmountEtaxnew()" runat="server" Width="75px" ToolTip="Applied" Checked="true" GroupName="sgst"></asp:RadioButton>
                                        </td>
                                    </tr>
                                </table>

                            </td>

                            <td><asp:TextBox ID="Textsgst" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox></td>

                        </tr>
                        
                        <tr>
                            <td>&nbsp;Fixed Discount&nbsp;</td>
                            <td>
                                <asp:TextBox onkeypress="return GetOnlyNumbers(this, event, true,true);" ID="txtfixdisc" runat="server"
                                    Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="6"></asp:TextBox><asp:DropDownList ID="DropFixDisType" runat="server" Width="45px" CssClass="dropdownlist" onchange="GetNetAmountEtaxnew()"
                                        Enabled="false">
                                        <asp:ListItem Value="Rs" Selected="True">Rs.</asp:ListItem>
                                        <asp:ListItem Value="Per">%</asp:ListItem>
                                    </asp:DropDownList>&nbsp;<asp:TextBox onkeypress="return GetOnlyNumbers(this, event, false,true);" ID="txtfixdiscamount"
                                        onblur="GetNetAmountEtaxnew()" runat="server" Width="80px" ReadOnly="True" BorderStyle="Groove" CssClass="dropdownlist"></asp:TextBox></td>
                            <td>&nbsp;Net Amount</td>
                            <td>
                                <asp:TextBox ID="txtNetAmount" runat="server" Width="140px" ReadOnly="True" BorderStyle="Groove"
                                    CssClass="dropdownlist"></asp:TextBox></td>
                        </tr>
                        <!--TR>
								<TD style="HEIGHT: 2px">Entry&nbsp;By</TD>
								<TD style="WIDTH: 211px; HEIGHT: 2px"><asp:label id="lblEntryBy" runat="server"></asp:label></TD>
								<TD style="HEIGHT: 2px" align="right" colSpan="2"></TD>
							</TR-->
                        <tr>
                            <!--TD>Entry Date &amp; Time&nbsp;&nbsp;&nbsp;
								</TD>
								<TD style="WIDTH: 211px"><asp:label id="lblEntryTime" runat="server"></asp:label></TD-->
                            <td align="right" colspan="4">
                                <asp:Button ID="btnSave" runat="server" Width="75px" Text="Save"
                                    OnClick="btnSave_Click"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnPrint" runat="server" Width="75px" Text="Print" CausesValidation="False"
                                        OnClick="btnPrint_Click" onmouseup="GetNetAmountEtaxnew()"></asp:Button>&nbsp;&nbsp;<asp:Button onmouseup="checkDelRec()" Text="Delete" ID="btnDelete" runat="server" Width="75px"
                                            OnClick="btnDelete_Click"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnPreview" runat="server" Width="75px" Text="Preview"
                                                OnClick="btnPreview_Click"></asp:Button></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
                </td>
            </tr>
        </table>
        <iframe id="gToday:contrast:agenda.js" style="z-index: 101; left: -500px; visibility: visible; position: absolute; top: 0px"
            name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameborder="0"
            width="174" scrolling="no" height="189"></iframe>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>
