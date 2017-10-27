/*
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.

*/	

function getbeatInfo(t,state,country)
{
	var mainarr=new Array()
	var typeindex=t.selectedIndex
	var typetext=t.options[typeindex].text
	var hidtext=document.Form1.txtbeatname.value
	//alert(document.Form1.txtbeatname.value)
	mainarr=hidtext.split('#')
	var cityarr=new Array()
	country.value=""
	state.value=""
	var k=0
	var statearr=new Array()
 
	for(var i=0;i<(mainarr.length-1);i++)
	{
		cityarr=mainarr[i].split(":")
		for(var j=0;j<cityarr.length;j++)
		{
			if(cityarr[j]==typetext)
			{
				state.value=cityarr[1]
				country.value=cityarr[2]
				break
			}
		}
	}
}