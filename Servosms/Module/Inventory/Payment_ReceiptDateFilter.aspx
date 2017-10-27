﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment_ReceiptDateFilter.aspx.cs" Inherits="Payment_ReceiptDateFilter" %>

<!DOCTYPE html>

<HTML>
	<HEAD>
		<title>ServoSMS: Yearly Report</title> <!--
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
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Searchdrop" src="../../Sysitem/JS/Searchdrop.js"></script>
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
		
        <script type="text/javascript">
        function SetDates() {

            var fromDate = document.getElementById("lblReceiptFromDate").value;
            fromDate = new Date(fromDate.split('/')[2], fromDate.split('/')[1] - 1, fromDate.split('/')[0]);
            var toDate = document.getElementById("lblReceiptToDate").value;
            toDate = new Date(toDate.split('/')[2], toDate.split('/')[1] - 1, toDate.split('/')[0]);
            
            if (fromDate > toDate) {
                alert("To Date should be greater than From Date.");
                return;
            }            

            if (window.opener != null && !window.opener.closed) {

                var txtFromDate = window.opener.document.getElementById("hidReceiptFromDate");
                txtFromDate.value = document.getElementById("lblReceiptFromDate").value;

                var txtToDate = window.opener.document.getElementById("hidReceiptToDate");
                txtToDate.value = document.getElementById("lblReceiptToDate").value;
            }
            window.parent.opener.document.forms[0].submit();
            window.close();
        }
</script>
		
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			
			<table height="290" align="center">
				<TBODY>
					<TR>
						<TH valign="top" height="20" colspan="5">
							<font color="#ce4848">Receipt Date Filter</font>
							<hr>
						</TH>
					</TR>
					<tr height="20">
						<td>From Date </td>
						<td><asp:textbox id="lblReceiptFromDate" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.lblReceiptFromDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td align="right">To Date</td>
						<td><asp:textbox id="lblReceiptToDate" runat="server" Width="70px" CssClass="dropdownlist" BorderStyle="Groove"
								ReadOnly="True"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.lblReceiptToDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A></td>
						<td>&nbsp;&nbsp;</td>
					</tr>
					<tr height="20">
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr height="20">
						<td>&nbsp;</td>
						<td>&nbsp;</td>
						<td><asp:button id="btnSubmit" Width="65px" Runat="server" Text="Submit" onClientClick="return SetDates()"
								 ></asp:button></td>
						<td>&nbsp;</td>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td vAlign="top" colspan="5">
							&nbsp;</td>
					</tr>
				</TBODY>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no" height="189"></iframe>
			</form>
	</body>
</HTML>
