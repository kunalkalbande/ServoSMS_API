<%@ Page language="c#" Inherits="Servosms.Module.Inventory.UpdateModuleRole" CodeFile="BatchNo.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Batch No</title>
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
	
	/*var Batch = new Array();
	function ReturnValue()
	{
		var str=""
		for(var i=4;i<=33;i++)
		{
			str=str+document.Form1.elements[i].value+":"+document.Form1.elements[++i].value+":"+document.Form1.elements[++i].value+","
		}
		window.opener.document.all.tempbatch.value=str;
		alert(window.opener.document.all.tempbatch.value)
	}*/
	
	function check()
	{
		//alert("Enter");
		var f1 = document.Form1
		var arr = new Array();
		var i=0;
		
		/******************************************
		<%for(int k=0,j=0; j<10;k++,j++) {%>
		if(document.Form1.txtBat<%=j%>.value!="")
			arr[k]="'"+document.Form1.txtBat<%=j%>.value+"'"
		else
			arr[k]="''";
		if(document.Form1.txtQty<%=j%>.value!="")
			arr[++k]="'"+document.Form1.txtQty<%=j%>.value+"'"
		else
			arr[++k]="''";
		<%}%>
		//******************************************/
		
		if(document.Form1.txtBat0.value!="")
			arr[i++]="'"+document.Form1.txtBat0.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID0.value!="")
			arr[i++]="'"+document.Form1.txtBatID0.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty0.value!="")
			arr[i++]="'"+document.Form1.txtQty0.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat1.value!="")
			arr[i++]="'"+document.Form1.txtBat1.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID1.value!="")
			arr[i++]="'"+document.Form1.txtBatID1.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty1.value!="")
			arr[i++]="'"+document.Form1.txtQty1.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat2.value!="")
			arr[i++]="'"+document.Form1.txtBat2.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID2.value!="")
			arr[i++]="'"+document.Form1.txtBatID2.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty2.value!="")
			arr[i++]="'"+document.Form1.txtQty2.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat3.value!="")
			arr[i++]="'"+document.Form1.txtBat3.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID3.value!="")
			arr[i++]="'"+document.Form1.txtBatID3.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty3.value!="")
			arr[i++]="'"+document.Form1.txtQty3.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat4.value!="")
			arr[i++]="'"+document.Form1.txtBat4.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID4.value!="")
			arr[i++]="'"+document.Form1.txtBatID4.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty4.value!="")
			arr[i++]="'"+document.Form1.txtQty4.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat5.value!="")
			arr[i++]="'"+document.Form1.txtBat5.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID5.value!="")
			arr[i++]="'"+document.Form1.txtBatID5.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty5.value!="")
			arr[i++]="'"+document.Form1.txtQty5.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat6.value!="")
			arr[i++]="'"+document.Form1.txtBat6.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID6.value!="")
			arr[i++]="'"+document.Form1.txtBatID6.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty6.value!="")
			arr[i++]="'"+document.Form1.txtQty6.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat7.value!="")
			arr[i++]="'"+document.Form1.txtBat7.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID7.value!="")
			arr[i++]="'"+document.Form1.txtBatID7.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty7.value!="")
			arr[i++]="'"+document.Form1.txtQty7.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat8.value!="")
			arr[i++]="'"+document.Form1.txtBat8.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID8.value!="")
			arr[i++]="'"+document.Form1.txtBatID8.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty8.value!="")
			arr[i++]="'"+document.Form1.txtQty8.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBat9.value!="")
			arr[i++]="'"+document.Form1.txtBat9.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtBatID9.value!="")
			arr[i++]="'"+document.Form1.txtBatID9.value+"'"
		else
			arr[i++]="''";
		if(document.Form1.txtQty9.value!="")
			arr[i++]="'"+document.Form1.txtQty9.value+"'"
		else
			arr[i++]="''";
		
		var str="";
		for(i=0;i<arr.length;i++)
		{
			str+=arr[i].toString()+",";
		}
		
		//Condition for Batch Child Window Restrection for Null Entry   vikas 17.09.09
		if(arr[2].toString()=="''") 
		{
			alert("Enter Atleast One Batch No");
			return;
		}
		
		
		for(var m=0;m<arr.length;m++)
		{
			if(arr[m].toString()!="''")
			{
				m+=2;
				//if(arr[++m].toString()=="''")
				if(arr[m].toString()=="''")
				{
					alert("Qty Can Not Be Blank")
					return;
				}
			}
			else
				m+=2
		}
		
		str=str.substring(0,str.length-1)
		
		//*******************************************
		
		//*******************************************
		if(document.Form1.chkinfo.value=="chkbatch1")
			window.opener.document.all.bat0.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch2")
			window.opener.document.all.bat1.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch3")
			window.opener.document.all.bat2.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch4")
			window.opener.document.all.bat3.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch5")
			window.opener.document.all.bat4.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch6")
			window.opener.document.all.bat5.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch7")
			window.opener.document.all.bat6.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch8")
			window.opener.document.all.bat7.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch9")
			window.opener.document.all.bat8.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch10")
			window.opener.document.all.bat9.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch11")
			window.opener.document.all.bat10.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch12")
			window.opener.document.all.bat11.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch13")
			window.opener.document.all.bat12.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch14")
			window.opener.document.all.bat13.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch15")
			window.opener.document.all.bat14.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch16")
			window.opener.document.all.bat15.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch17")
			window.opener.document.all.bat16.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch18")
			window.opener.document.all.bat17.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch19")
			window.opener.document.all.bat18.value=str;
		else if(document.Form1.chkinfo.value=="chkbatch20")
			window.opener.document.all.bat19.value=str;
		else if(document.Form1.chkinfo.value=="Yes")
			window.opener.document.all.batch.value=str;
		
		//ReturnValue();
		window.close();
	}
	
	
	function check11()
	{
		//window.focus();
	}
	
	function checkDupBatch(t)
	{
		for(var i=0,j=1;i<10;i++,j++)
		{
			if(document.Form1.txtBat=i.value!="")
			{
				//document.Form1.txtBat+i.value
				//alert("");
			}
		}
	}
	
	
	function checkDup(t)
	{
		if(t.value!="")
		{
			var arr = new Array();
			var arr1 = new Array();
			var i=0;
			if(document.Form1.txtBat0.value!="")
				arr[i++]=document.Form1.txtBat0.value
			if(document.Form1.txtBat1.value!="")
				arr[i++]=document.Form1.txtBat1.value
			if(document.Form1.txtBat2.value!="")
				arr[i++]=document.Form1.txtBat2.value
			if(document.Form1.txtBat3.value!="")
				arr[i++]=document.Form1.txtBat3.value
			if(document.Form1.txtBat4.value!="")
				arr[i++]=document.Form1.txtBat4.value
			if(document.Form1.txtBat5.value!="")
				arr[i++]=document.Form1.txtBat5.value
			if(document.Form1.txtBat6.value!="")
				arr[i++]=document.Form1.txtBat6.value
			if(document.Form1.txtBat7.value!="")
				arr[i++]=document.Form1.txtBat7.value
			if(document.Form1.txtBat8.value!="")
				arr[i++]=document.Form1.txtBat8.value
			if(document.Form1.txtBat9.value!="")
				arr[i++]=document.Form1.txtBat9.value
			var Count=0;
			for(var j=0;j<arr.length;j++)
			{
				if(t.value.toLowerCase()==arr[j].toString().toLowerCase())
					Count++;
			}
			if(Count>=2)
			{
				alert("Value Can Not Be Duplicate");
				t.value="";
				t.focus()
				return			
			}
		}
	}
		
		</script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<input id="BatchNo" style="WIDTH: 1px" type="hidden" name="BatchNo" runat="server">
			<input id="chkinfo" style="WIDTH: 1px" type="hidden" name="chkinfo" runat="server">
			<input id="tempflage" style="WIDTH: 1px" type="hidden" name="chkinfo" runat="server">
			<div style="LEFT: 8px; POSITION: absolute; TOP: 5px">
				<table cellSpacing="0" cellPadding="0" width="185" onblur="check11()">
					<tr>
						<th class="fontstyle" colSpan="3">
							<font color="#ce4848">Enter Batch No</font><hr size="0">
						</th>
					</tr>
					<%int i=0,j=1;%>
					<tr>
						<th class="fontstyle">
							<font color="#000000">SNO</font></th>
						<th class="fontstyle">
							<font color="#000000">Batch No</font></th>
						<th class="fontstyle">
							<font color="#000000">Qty</font></th>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" type=text size=14 name=txtBat0 value="<%=arrBatch[i++].ToString()%>"  onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID0 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty0 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat1 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID1 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty1 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true)" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat2 value="<%=arrBatch[i++].ToString()%>"  onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID2 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty2 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat3 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID3 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty3 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat4 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID4 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty4 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat5 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID5 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty5 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat6 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID6 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty6 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat7 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID7 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty7 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat8 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID8 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty8 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td align="center"><%=j++%></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=14 name=txtBat9 value="<%=arrBatch[i++].ToString()%>" onblur="checkDup(this);" maxLength=49>
							<input class=fontstyle type=hidden size=1 name=txtBatID9 value="<%=arrBatch[i++].ToString()%>"></td>
						<td><input class=fontstyle 
      style="BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove" 
      type=text size=5 name=txtQty9 value="<%=arrBatch[i++].ToString()%>" onkeypress="return GetOnlyNumbers(this, event, false,true);" maxLength=4></td>
					</tr>
					<tr>
						<td colspan="3"><hr size="0">
						</td>
					</tr>
					<tr>
						<td align="center" colSpan="3"><asp:button id="btnBatch" Runat="server" Text="Submit" 
								 onclick="btnBatch_Click"></asp:button></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
