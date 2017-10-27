<%@ Page language="c#" Inherits="Servosms.Module.Accounts.Ledger" CodeFile="Ledger.aspx.cs" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Ledger Creation</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
		function getGroup(t)
		{
			var typeindex = t.selectedIndex
            var typetext  = t.options[typeindex].text
            document.Form1.TxtSub.value = "";
                
            var mainarr = new Array();
            var secarr  = new Array();
            var n =0;
            var hidarr  = document.Form1.txtValue.value;
            mainarr = hidarr.split("#");
            document.Form1.DropGroup.length = 1;
            if(typetext == "Other")
            {
                document.Form1.TxtSub.disabled = false;
                hidarr  = document.Form1.txtGrp.value;
                secarr = hidarr.split("~");
                for(var j=0;j<secarr.length-1;j++)
                {
					document.Form1.DropGroup.add(new Option) 
					if(secarr[j]  != "")
					{
						document.Form1.DropGroup.options[n+1].text=secarr[j];                  
						n = n+1;
					}
				}
                document.Form1.DropGroup.add(new Option)  
                document.Form1.DropGroup.options[n+1].text="Other";
			}
            else
            {
                document.Form1.TxtSub.disabled = true;
                for(var i=0;i < mainarr.length;i++)
                {
					secarr = mainarr[i].split("~");
					if(typetext == secarr[0])
                    {
						document.Form1.DropGroup.add(new Option)  
						document.Form1.DropGroup.options[n+1].text=secarr[1];
						n = n + 1;                   
						if(secarr[2] == "Assets")
						{
							document.Form1.RadioAsset.checked = true;
						}
						else if(secarr[2] == "Liabilities")
						{
							document.Form1.RadioLiab.checked = true;
						}
						else if(secarr[2] == "Expenses")
						{
							document.Form1.RadioExp.checked = true;
						}
						else
						{
							document.Form1.RadioIncome.checked = true;
						}
					} 
				}
                document.Form1.DropGroup.selectedIndex = 1;
                document.Form1.DropGroup.add(new Option)  
                document.Form1.DropGroup.options[n+1].text="Other";
                setNature(t);
            }
        }  
        
        function setNature(t)
        {
			var typeindex = t.selectedIndex
            var typetext  = t.options[typeindex].text
            var mainarr = new Array();
            var secarr  = new Array();
            var hidarr  = document.Form1.txtValue.value;
            mainarr = hidarr.split("#");               
            var typeindex = document.Form1.DropGroup.selectedIndex
            var typetext1  = document.Form1.DropGroup.options[typeindex].text
            document.Form1.txtTempGrp.value = typetext1;
            if(typetext1  == "Other")
            {
				document.Form1.TxtGroup.disabled = false;
                document.Form1.RadioAsset.checked = true;
            }
            else
            {
				document.Form1.TxtGroup.disabled = true;
                document.Form1.TxtGroup.value = "";
                for(var i=0;i < mainarr.length;i++)
                {
					secarr = mainarr[i].split("~");
					if(typetext == secarr[0] && typetext1 == secarr[1] )
                    {
						if(secarr[2] == "Assets")
						{
							document.Form1.RadioAsset.checked = true;
						}
						else if(secarr[2] == "Liabilities")
						{
							document.Form1.RadioLiab.checked = true;
						}
						else if(secarr[2] == "Expenses")
						{
							document.Form1.RadioExp.checked = true;
						}
						else
						{
							document.Form1.RadioIncome.checked = true;
						}
					} 
                }
            }
        }
        
        function checkDelRec()
		{
			if(document.Form1.btnEdit1 == null)
			{
				if(document.Form1.dropLedgerName.value!="Select")
				{
					if(confirm("Do You Want To Delete The Ledger"))
						document.Form1.tempInfo.value="Yes";
					else
						document.Form1.tempInfo.value="No";
				}
				else
				{
					alert("Please Select The Ledger Name");
					return;
				}
			}
			else
			{
				alert("Please Click The Edit button");
				return;
			}
			if(document.Form1.tempInfo.value=="Yes")
			document.Form1.submit();
		}
		</script>
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><INPUT id="txtValue" style="Z-INDEX: 102; LEFT: 144px; WIDTH: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 22px"
				type="hidden" size="1" name="txtValue" runat="server"> <INPUT id="txtTempGrp" style="Z-INDEX: 103; LEFT: 160px; WIDTH: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 24px"
				type="hidden" size="1" name="txtTempGrp" runat="server"> <INPUT id="txtGrp" style="Z-INDEX: 104; LEFT: 176px; WIDTH: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 24px"
				type="hidden" size="1" name="txtGrp" runat="server"><input id="tempInfo" name="tempInfo" runat="server" type="hidden" style="WIDTH:0px">
			<TABLE height="290" width="778" align="center">
				<tr>
					<th align="center">
						&nbsp;<font color="#ce4848">Ledger Creation</font>
						<hr>
					</th>
				</tr>
				<TR>
					<TD vAlign="top" align="center">
						<TABLE border="0" cellpadding="2">
							<TR>
								<TD colSpan="2"><FONT color="#ff0000">Fields Marked as (*) Are Mandatory</FONT></TD>
							</TR>
							<TR>
								<TD align="left">Ledger Name&nbsp; <FONT color="#ff0000">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Ledger Name"
										ControlToValidate="TxtLedger">*</asp:requiredfieldvalidator><FONT color="red"></FONT></TD>
								<TD><asp:dropdownlist id="dropLedgerName" runat="server" Width="240px" AutoPostBack="True" Visible="False"
										CssClass="DropDownList" onselectedindexchanged="dropLedgerName_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="TxtLedger" runat="server" Width="240px" CssClass="DropDownList" BorderStyle="Groove"></asp:textbox><asp:button id="btnEdit1" runat="server" CausesValidation="False" ToolTip="Click Here For Edit"
										Text="..." onclick="btnEdit1_Click"></asp:button></TD>
							</TR>
							<TR>
								<TD align="left">SubGroup Name <FONT color="#ff3333">*</FONT></TD>
								<TD><asp:dropdownlist id="DropSub" runat="server" Width="170px" onChange="return getGroup(this);" CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist>&nbsp;<FONT color="#0000ff">&nbsp;&nbsp;&nbsp;(if another, 
										Specify)</FONT>&nbsp;
									<asp:textbox id="TxtSub" runat="server" Width="110px" CssClass="DropDownList" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>Group Name <FONT color="#ff0000">*</FONT>
								</TD>
								<TD><asp:dropdownlist id="DropGroup" runat="server" Width="170px" onchange="return setNature(document.Form1.DropSub);"
										CssClass="DropDownList">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist><FONT color="#0000ff">&nbsp;&nbsp;&nbsp;&nbsp;(if another, 
										Specify)</FONT>&nbsp;
									<asp:textbox id="TxtGroup" runat="server" Width="110px" CssClass="DropDownList" BorderStyle="Groove"></asp:textbox></TD>
							</TR>
							<TR>
								<TD align="left">Nature of Group <FONT color="#ff0000">&nbsp;&nbsp;&nbsp;</FONT></TD>
								<TD>
									<P align="left"><asp:radiobutton id="RadioAsset" runat="server" Text="Asset" GroupName="Nature" Checked="True"></asp:radiobutton>s&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="RadioLiab" runat="server" Text="Liabilities" GroupName="Nature"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="RadioExp" runat="server" Text="Expenses" GroupName="Nature"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:radiobutton id="RadioIncome" runat="server" Text="Income" GroupName="Nature"></asp:radiobutton></P>
								</TD>
							</TR>
							<!--	<TR>
								<TD style="HEIGHT: 30px" align="left" colSpan="4">&nbsp;Effective 
									From&nbsp;&nbsp;&nbsp;
									<asp:textbox id="TxtFrom" runat="server" Width="84px"></asp:textbox>&nbsp;&nbsp;&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDate);return false;"><IMG class="PopcalTrigger" id="Img3" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0" runat="server"></A>&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;Effective TO
									<asp:textbox id="TxtTo" runat="server" Width="60px"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDate);return false;"><IMG class="PopcalTrigger" id="Img1" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0" runat="server"></A>
								</TD>
							</TR>-->
							<TR>
								<TD>Opening Balance&nbsp;</TD>
								<TD><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="TxtOpeningBal"
										runat="server" Width="120px" CssClass="DropDownList" BorderStyle="Groove"></asp:textbox>&nbsp;&nbsp;
									<asp:dropdownlist id="DropBalType" runat="server" Width="40px" CssClass="DropDownList">
										<asp:ListItem Value="Debit">Dr</asp:ListItem>
										<asp:ListItem Value="Credit">Cr</asp:ListItem>
									</asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD></TD>
							</TR>
							<tr>
								<td colSpan="2"></td>
							</tr>
							<TR>
								<TD align="center" colSpan="2"><asp:button id="btnSave" runat="server" Width="70px" Text="Add" 
									 onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnEdit" runat="server" Width="70px" Text="Edit" 
										 onclick="btnEdit_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;<asp:button id="btnDelete" runat="server" Width="70px" CausesValidation="False" Text="Delete"
										 onmouseup="checkDelRec()" onclick="btnDelete_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<asp:validationsummary id="ValidationSummary1" runat="server" Height="209px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></TD>
				</TR>
			</TABLE>
			<!--<IFRAME id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></IFRAME>--><iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174" scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
		</FORM></FORM>
	</body>
</HTML>
