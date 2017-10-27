/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/
function GetOnlyNumbers(obj, e, AllowNegative, AllowDecimal)
{
	
	var key;
	var isCtrl = false;
	var keychar;
	var reg;
	if(window.event) 
	{
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	else if(e.which) 
	{
		key = e.which;
		isCtrl = e.ctrlKey;
	}
	
	//	if (isNaN(key)) return true;
	keychar = String.fromCharCode(key);
	if(keychar=='-'&&obj.value.length>0)
		return false;
	
	// 	check for backspace or delete, or if Ctrl was pressed
	if (key == 8 || isCtrl)
	{
		return true;
	}
	
	reg = /\d/;
	var AllowNegNums = AllowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
	var AllowDecNums = AllowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;	
	return AllowNegNums || AllowDecNums || reg.test(keychar);
}

function GetAnyNumber(obj, e)
{
	var key;
	var isCtrl = false;
	var keychar;
	var reg;
	if(window.event) 
	{
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	
	keychar = String.fromCharCode(key);
	if(keychar>='A'&&keychar<='Z')
		return true;
	else if(keychar>='a'&&keychar<='z')
		return true;
	else if(keychar==' ')
		return true
	else if(keychar=='(')
		return true
	else if(keychar==')')
		return true
	else if(keychar>='0' && keychar<='9')
		return true
	else if(keychar=='@' || keychar=='-' || keychar=='_' || keychar=='.' || keychar=='&' || keychar=='%')
		return true
	
	else if (key == 8 || isCtrl)
		return true;
	else
		return false;
}

function GetOnlyChars(obj, e)
{
	var key;
	var isCtrl = false;
	var keychar;
		
	if(window.event) 
	{
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	
	keychar = String.fromCharCode(key);
	if(keychar>='A'&&keychar<='Z')
		return true;
	else if(keychar>='a'&&keychar<='z')
		return true;
	else if(keychar==' ')
		return true
		
	else if (key == 8 || isCtrl)
		return true;
	else
		return false;
}

function GetOnlyNumbersWithA(obj, e, AllowNegative, AllowDecimal,AllowToA)
{
	var key;
	var isCtrl = false;
	var keychar;
	var reg;
		
	if(window.event) 
	{
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	else if(e.which) 
	{
		key = e.which;
		isCtrl = e.ctrlKey;
	}
	
	//	if (isNaN(key)) return true;
	keychar = String.fromCharCode(key);
	if(keychar=='-'&&obj.value.length>0)
		return false;
	
	// 	check for backspace or delete, or if Ctrl was pressed

	if (key == 8 || isCtrl)
	{
		return true;
	}

	reg = /\d/;
	var AllowNegNums = AllowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
	var AllowDecNums = AllowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;	
	var AllowToAChar = AllowToA ? keychar == 'A' && obj.value.indexOf('.') == -1 : false;	
	
	return AllowNegNums || AllowDecNums || AllowToAChar || reg.test(keychar);
}

function Check1(txtCity)
{
	//alert("hello1");
	return false;
}