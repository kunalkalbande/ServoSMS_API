<%@ Page language="c#" Inherits="Servosms.Module.Inventory.SchemeDiscount" CodeFile="SchemeDiscount.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Scheme Discount</title>
		<script language="javascript">
	function check()
	{
		var f1 = document.Form1
		var str=""
		var i=0;
		var count=0;
		
		if(document.Form1.txtSchDisc.value!="")
		{
			str = document.Form1.txtSchDisc.value
		}
		if(document.Form1.chkinfo.value=="chkSchDisc1")
			window.opener.document.all.tempSchDiscount1.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc2")
			window.opener.document.all.tempSchDiscount2.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc3")
			window.opener.document.all.tempSchDiscount3.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc4")
			window.opener.document.all.tempSchDiscount4.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc5")
			window.opener.document.all.tempSchDiscount5.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc6")
			window.opener.document.all.tempSchDiscount6.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc7")
			window.opener.document.all.tempSchDiscount7.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc8")
			window.opener.document.all.tempSchDiscount8.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc9")
			window.opener.document.all.tempSchDiscount9.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc10")
			window.opener.document.all.tempSchDiscount10.value=str;
		if(document.Form1.chkinfo.value=="chkSchDisc11")
			window.opener.document.all.tempSchDiscount11.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc12")
			window.opener.document.all.tempSchDiscount12.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc13")
			window.opener.document.all.tempSchDiscount13.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc14")
			window.opener.document.all.tempSchDiscount14.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc15")
			window.opener.document.all.tempSchDiscount15.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc16")
			window.opener.document.all.tempSchDiscount16.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc17")
			window.opener.document.all.tempSchDiscount17.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc18")
			window.opener.document.all.tempSchDiscount18.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc19")
			window.opener.document.all.tempSchDiscount19.value=str;
		else if(document.Form1.chkinfo.value=="chkSchDisc20")
			window.opener.document.all.tempSchDiscount20.value=str;
				
		<%for(int i=1;i<=20;i++){%>
			if(window.opener.document.all.tempSchDiscount<%=i%>.value!="")
			{
				count=count+eval(window.opener.document.all.tempSchDiscount<%=i%>.value);
				//alert(count)
			}
		<%}%>
		//coment by vikas 22.12.2012 window.opener.document.all.txtDisc.value=count;
		window.opener.document.all.txtPromoScheme.value=count;
		window.opener.GetNetAmountEtaxnew();
		window.close();
	}
		</script>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="StyleSheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<input id="chkinfo" style="WIDTH: 1px" type="hidden" name="chkinfo" runat="server">
			<table width="150">
				<tr>
					<td colspan="2" align="center"><b><font color="#ce4848">Scheme Discount</font></b><hr>
					</td>
				</tr>
				<tr>
					<td width="50%"><input type="text" maxlength="9" class="dropdownlist" onkeypress="return GetOnlyNumbers(this, event, true,true);"
							name="txtSchDisc" style="WIDTH: 100%; BORDER-TOP-STYLE: groove; BORDER-RIGHT-STYLE: groove; BORDER-LEFT-STYLE: groove; BORDER-BOTTOM-STYLE: groove"></td>
					<td width="50%" align="center"><asp:Button ID="btnSubmit" Runat="server" CssClass="dropdownlist" Height="20px" Width="60px"
							Text="Submit" ></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
