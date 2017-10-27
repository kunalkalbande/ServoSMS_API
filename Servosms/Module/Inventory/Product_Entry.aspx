<%@ Page language="c#" Inherits="Servosms.Module.Inventory.Product_Entry" CodeFile="Product_Entry.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Product Entry</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
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
		function  check(t)
		{
		var index = t.selectedIndex;
		//var value = t.options[index].value;
		//alert(index)
		if(index == 0)
		{
		//alert("if")
		document.f1.txtCategory.disabled = false;
		return false;
		}
		else
		{
		//alert("else")
		document.f1.txtCategory.disabled = true;
		document.f1.txtCategory.value = "";
		return false;
		}
		
		}
		
		function  check1(t)
		{
		var index = t.selectedIndex;
		var value = t.options[index].value;
		//alert(index)
		if(value == "Other")
		{
		//alert("if")
		document.f1.txtunit.disabled = false;
		return false;
		}
		else
		{
		//alert("else")
		document.f1.txtunit.disabled = true;
		document.f1.txtunit.value = "";
		
		return false;
		}
		
		}
		function  check2(t)
		{
		var index = t.selectedIndex;
	//	var value = t.options[index].value;
	//	alert(index)
		if(index == 0)
		{
	//	alert("if")
		document.f1.txtPack1.disabled = false;
		document.f1.txtPack2.disabled = false;
		document.f1.DropUnit.selectedIndex = 0;
		document.f1.DropUnit.disabled = false;
		document.f1.txtOp_Stock.disabled = false;
		document.f1.txtBox.disabled = true;
		document.f1.DropUnit.disabled = false;
		document.f1.txtOp_Stock.value = "";
		document.f1.txtBox.value = "";
		document.f1.txtTotalQty.value ="";
		document.f1.DropPackUnit.selectedIndex =0;
		
		
		return false;
		}
		else if(index == 1)
		{
		document.f1.txtPack1.disabled = true;
		document.f1.txtPack2.disabled = true;
		document.f1.txtPack1.value = "";
		document.f1.txtPack2.value = "";
		document.f1.txtOp_Stock.disabled = true;
		document.f1.txtBox.value = "";
		document.f1.txtBox.disabled = false;
		document.f1.txtOp_Stock.value = "";
		document.f1.txtTotalQty.value = "0";
		document.f1.txtunit.value = "";
		document.f1.txtunit.disabled = true;
		document.f1.DropUnit.selectedIndex = 4;
		document.f1.DropUnit.disabled = true;
		document.f1.DropUnit.disabled = true;	
		document.f1.DropPackUnit.selectedIndex = 2;
		
		}
		else
		{
	//	alert("else")
		document.f1.txtPack1.disabled = true;
		document.f1.txtPack2.disabled = true;
		document.f1.txtPack1.value = "";
		document.f1.txtPack2.value = "";
		document.f1.DropUnit.selectedIndex = 0;
		document.f1.DropUnit.disabled = false;
		document.f1.txtOp_Stock.disabled = false;
		document.f1.txtBox.disabled = true;
		document.f1.DropUnit.disabled = false;
		document.f1.txtOp_Stock.value = "";
		document.f1.txtBox.value = "";
		document.f1.txtTotalQty.value ="";
		document.f1.DropPackUnit.selectedIndex =0;
		CalcTotalQty1(t);
		return false;
		}
		
		}
		
	function CalcTotalQty()
	{
		var f=document.f1;
		f.txtTotalQty.value=f.txtPack1.value*f.txtPack2.value;
	}
	function CalcTotalQty1(t)
	{
		var index = t.selectedIndex;
		var value = t.options[index].value;
		if(value != "")
		{
		var Qty = new Array();
		Qty = value.split("X");
		var q1=0;
		var q2=0;
		if(Qty[0]!="")
		q1=Qty[0];
		if(Qty[1] != "")
		q2=Qty[1];		  	
		document.f1.txtTotalQty.value= q1*q2;
		}
	}
	function CalcQty()
	{
		var f=document.f1;
		f.txtBox.value=f.txtTotalQty.value*f.txtOp_Stock.value;
	}
	function checkDelRec()
	{
		if(document.f1.btnEdit == null)
		{
			if(document.f1.dropProdID.value!="Select")
			{
				if(confirm("Do You Want To Delete The Product"))
					document.f1.tempDelinfo.value="Yes";
				else
					document.f1.tempDelinfo.value="No";
			}
			else
			{
				alert("Please Select The Product Name");
				return;
			}
		}
		else
		{
			alert("Please Click The Edit button");
			return;
		}
		if(document.f1.tempDelinfo.value=="Yes")
			document.f1.submit();
	}
	
	
	function getBatch(t,prd,prd_id)  
	{		
		if(t.value=="Yes")
		{
			if(prd_id!=null)
			{
				childWin=window.open("BatchNo.aspx?chk="+t.value+":"+prd.value+":"+prd_id.value+":","ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
			}
			else
				childWin=window.open("BatchNo.aspx?chk="+t.value+":"+prd.value+":"+document.f1.dropProdID.value+":", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
		}
	}
	
	/*function getBatch(t,prd,prd_id,batch)  add by vikas 
	{		
		
		if(t.value=="Yes")
		{
			if(prd_id!=null)
			{
				childWin=window.open("BatchNo.aspx?chk="+t.value+":"+prd.value+":"+prd_id.value+":","ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
				//alert(t.value+":"+prd.value+":"+prd_id.value)
			}
			else
			{
				childWin=window.open("BatchNo.aspx?chk="+t.value+":"+prd.value+":"+document.f1.dropProdID.value+":"+batch+":", "ChildWin", "toolbar=no,status=no,menubar=no,scrollbars=no,width=205,height=326");
				//alert(t.value+":"+prd.value+":"+batch.value)
			}
		}
	}*/
	
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="f1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 101; LEFT: 152px; POSITION: absolute; TOP: 16px" runat="server"
				Width="1" Visible="False"></asp:textbox><input id="tempDelinfo" style="WIDTH: 1px" type="hidden" name="tempDelinfo" runat="server">
			<input id="batch" style="WIDTH: 1px" type="hidden" name="batch" runat="server"> <input id="tempbatch" style="WIDTH: 1px" type="hidden" name="batch" runat="server">
			<table height="290" width="778" align="center">
				<TR>
					<TH align="center">
						<font color="#ce4848">Product Entry</font>
						<HR>
					</TH>
				</TR>
				<TR>
					<TD align="center">
						<TABLE cellSpacing="0" cellPadding="0">
							<TR>
								<TD>Product</TD>
								<TD colSpan="3"><asp:textbox id="lblProdID" runat="server" Width="120px" BorderStyle="Groove"  CssClass="dropdownlist"
										ReadOnly="True"></asp:textbox><asp:button id="btnEdit" runat="server" Width="25px" 
										 Text="..." ToolTip="Click For Edit" Height="20px" CausesValidation="False" onclick="btnEdit_Click"></asp:button>
									<asp:dropdownlist id="dropProdID" runat="server" Width="230px" Visible="False" CssClass="dropdownlist"
										AutoPostBack="True" onselectedindexchanged="dropProdID_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist>&nbsp;<asp:label id="lb" runat="server" ForeColor="Red" CssClass="dropdownlist"></asp:label></TD>
							</TR>
							<TR>
								<TD>Product Name <FONT color="red">*</FONT></TD>
								<TD><asp:textbox id="txtProdName" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist"
										MaxLength="49"></asp:textbox></TD>
								<TD>&nbsp; Product Code&nbsp;<asp:requiredfieldvalidator id="rfv1" Runat="server" ControlToValidate="txtProdCode" ErrorMessage="Please Enter the Product Code">*</asp:requiredfieldvalidator></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtProdCode" runat="server"
										Width="120px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="9"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Category&nbsp;Type <FONT color="red">*</FONT></TD>
								<TD><asp:dropdownlist id="DropCategory" Width="100px" CssClass="dropdownlist" AutoPostBack="false" Runat="server"
										OnChange="check(this);" onselectedindexchanged="DropCategory_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD><FONT color="#0000ff">&nbsp; (if another, Specify)</FONT></TD>
								<TD><asp:textbox id="txtCategory" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist"
										MaxLength="49"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Package Type <FONT color="red">*</FONT></TD>
								<TD><asp:dropdownlist id="DropPackage" runat="server" Width="75px" CssClass="dropdownlist" onChange="check2(this);" onselectedindexchanged="DropPackage_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Loose Oil">Loose Oil</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD><FONT color="#0000ff">&nbsp; (if another, Specify)</FONT></TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPack1" onblur="CalcTotalQty()"
										runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5" ontextchanged="txtPack1_TextChanged"></asp:textbox>&nbsp; 
									X&nbsp;&nbsp;
									<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPack2" onblur="CalcTotalQty()"
										runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="5"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Opening Stock
								</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtOp_Stock" onblur="CalcQty()"
										runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="8"></asp:textbox><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtBox" runat="server"
										Width="78px" BorderStyle="Groove" CssClass="dropdownlist" ontextchanged="txtBox_TextChanged"></asp:textbox></TD>
								<TD>&nbsp; Package&nbsp;Qty&nbsp; <FONT color="red">*</FONT></TD>
								<TD><asp:textbox id="txtTotalQty" runat="server" Width="120px" BorderStyle="Groove" CssClass="dropdownlist"
										ReadOnly="True"></asp:textbox><asp:dropdownlist id="DropPackUnit" runat="server" Width="63px" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Kg.">Kg.</asp:ListItem>
										<asp:ListItem Value="Ltr.">Ltr.</asp:ListItem>
										<asp:ListItem Value="Ml.">Ml.</asp:ListItem>
										<asp:ListItem Value="Nos.">Nos.</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD>Unit <FONT color="red">*</FONT></TD>
								<TD><asp:dropdownlist id="DropUnit" runat="server" Width="75px" CssClass="dropdownlist" onChange="check1(this);" onselectedindexchanged="DropUnit_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Barrel">Barrel</asp:ListItem>
										<asp:ListItem Value="Bucket">Bucket</asp:ListItem>
										<asp:ListItem Value="Carton">Carton</asp:ListItem>
										<asp:ListItem Value="Loose Oil">Loose Oil</asp:ListItem>
										<asp:ListItem Value="Pouch">Pouch</asp:ListItem>
										<asp:ListItem Value="Tin">Tin</asp:ListItem>
										<asp:ListItem Value="Other">Other</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD><FONT color="#0000ff">&nbsp;<asp:label id="Label1" runat="server" Width="88px">(if another, Specify)</asp:label></FONT></TD>
								<TD><asp:textbox id="txtunit" onblur="CalcTotalQty()" runat="server" Width="120px" BorderStyle="Groove"
										CssClass="dropdownlist" MaxLength="15"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Store in <FONT color="red">*</FONT></TD>
								<TD><asp:dropdownlist id="DropStorein" runat="server" Width="60px" CssClass="dropdownlist">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Godown">Godown</asp:ListItem>
										<asp:ListItem Value="Sales Room">Sales Room</asp:ListItem>
									</asp:dropdownlist></TD>
								<td>&nbsp; Minimum Lavel</td>
								<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtMinLabel" Width="120px"
										BorderStyle="Groove" CssClass="dropdownlist" MaxLength="8" Runat="server"></asp:textbox></td>
							</TR>
							<tr>
								<td>ReOrder Lavel</td>
								<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtReOrderLabel"
										Width="120px" BorderStyle="Groove" CssClass="dropdownlist" MaxLength="8" Runat="server"></asp:textbox></td>
								<td>&nbsp; Max Lavel</td>
								<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,false);" id="txtMaxLabel" Width="120px"
										BorderStyle="Groove" CssClass="dropdownlist" MaxLength="8" Runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td>Batch No</td>
								<td><asp:radiobutton id="Yes" onclick="getBatch(this,document.f1.txtProdName,document.f1.lblProdID,document.f1.tempbatch)"
										Text="Yes" Runat="server" GroupName="BatchNo"></asp:radiobutton>&nbsp;
									<asp:radiobutton id="No" Text="No" Runat="server" GroupName="BatchNo"></asp:radiobutton></td>
								<td>&nbsp; MRP</td>
								<td><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtMRP" Width="120px"
										BorderStyle="Groove" CssClass="dropdownlist" MaxLength="9" Runat="server"></asp:textbox></td>
							</tr>
							<TR>
								<TD align="right" colSpan="4"><asp:button id="btnSave" runat="server" Width="60px" 
										 Text="Save" onclick="btnSave_Click"></asp:button>&nbsp;
									<asp:button onmouseup="checkDelRec()" id="btnDelete" runat="server" Width="60px" 
										 Text="Delete" onclick="btnDelete_Click"></asp:button></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td><asp:validationsummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
