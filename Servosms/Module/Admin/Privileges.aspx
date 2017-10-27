<%@ Page language="c#" Inherits="Servosms.Module.Admin.Privileges" CodeFile="Privileges.aspx.cs" EnableViewState="true" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Privileges</title> <!--
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
	function ClearAll()
	{
		var f=document.Form1
		for(var i=0;i<f.length;i++)
			f.elements[i].checked=false
	}
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Sysitem/Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="290" cellSpacing="0" cellPadding="0" width="778" align="center">
				<TBODY>
					<TR>
						<TH align="center">
							<font color="#990033">Privileges</font>
							<hr>
						</TH>
					</TR>
					<tr>
						<td align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="1">
								<TR>
									<TD align="center">&nbsp;User ID <FONT color="red">*</FONT>&nbsp;&nbsp;
										<asp:dropdownlist id="DropUserID" runat="server" CssClass="FontStyle" Width="115px" AutoPostBack="True" onselectedindexchanged="DropUserID_SelectedIndexChanged">
											<asp:ListItem Value="Select">Select</asp:ListItem>
										</asp:dropdownlist><asp:textbox id="txtUserName" runat="server" CssClass="FontStyle" BorderStyle="Groove" Enabled="False"></asp:textbox></TD>
								</TR>
								<TR>
									<TD align="center">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD colSpan="6"><asp:checkbox id="chkAccount" runat="server" AutoPostBack="True" ForeColor="#990033" Text="Account Module" oncheckedchanged="chkAccount_CheckedChanged"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="6"><asp:panel id="PanelAcc" runat="server" Visible="False" Width="512px">
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD width="5%"></TD>
																<TD width="59%"></TD>
																<TD align="center" width="9%"><STRONG><STRONG>View</STRONG></STRONG></TD>
																<TD align="center" width="9%"><STRONG><STRONG>&nbsp;Add</STRONG></STRONG></TD>
																<TD align="center" width="9%"><STRONG>Edit</STRONG></TD>
																<TD align="center" width="9%"><STRONG>&nbsp;Del</STRONG></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;1.
																</TD>
																<TD>Ledger Creation</TD>
																<TD align="center">
																	<asp:checkbox id="chkView1" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd1" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit1" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel1" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;2.
																</TD>
																<TD>Payment</TD>
																<TD align="center">
																	<asp:checkbox id="chkView2" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd2" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit2" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel2" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;3.
																</TD>
																<TD>Receipt</TD>
																<TD align="center">
																	<asp:checkbox id="chkView3" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd3" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit3" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel3" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;4.
																</TD>
																<TD>Voucher Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView4" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd4" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit4" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel4" runat="server"></asp:checkbox></TD>
															</TR>
														</TABLE>
													</asp:panel></TD>
											</TR>
											<TR>
												<TD colSpan="6"><asp:checkbox id="chkEmployee" runat="server" AutoPostBack="True" ForeColor="#990033" Text="Employee Module" oncheckedchanged="chkEmployee_CheckedChanged"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="6"><asp:panel id="PanelEmp" runat="server" Width="512px" Visible="False">
														<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
															<TR>
																<TD width="5%">&nbsp;</TD>
																<TD width="59%"><STRONG></STRONG></TD>
																<TD align="center" width="9%"><STRONG>View</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Add</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Edit</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Del</STRONG></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;1.</TD>
																<TD>Attandance Register</TD>
																<TD align="center">
																	<asp:checkbox id="chkView5" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd5" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit5" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel5" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;2.</TD>
																<TD>Employee Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView6" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd6" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit6" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel6" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;3.
																</TD>
																<TD>Leave Application</TD>
																<TD align="center">
																	<asp:checkbox id="chkView7" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd7" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit7" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel7" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;4.</TD>
																<TD>Leave Sanction</TD>
																<TD align="center">
																	<asp:checkbox id="chkView8" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd8" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit8" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel8" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;5.</TD>
																<TD>Salary Statement</TD>
																<TD align="center">
																	<asp:checkbox id="chkView9" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd9" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit9" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel9" runat="server"></asp:checkbox></TD>
															</TR>
														</TABLE>
													</asp:panel></TD>
											</TR>
											<TR>
												<TD colSpan="6"><asp:checkbox id="chkMaster" runat="server" AutoPostBack="True" ForeColor="#990033" Text="Master Module" oncheckedchanged="chkMaster_CheckedChanged"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="6"><asp:panel id="PanelMaster" runat="server" Width="512px" Visible="False" DESIGNTIMEDRAGDROP="1043">
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD width="5%"></TD>
																<TD width="59%"><STRONG></STRONG></TD>
																<TD align="center" width="9%"><STRONG>View</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Add</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Edit</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Del</STRONG></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;1.
																</TD>
																<TD>Beat Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView10" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd10" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit10" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel10" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;2.</TD>
																<TD>Customer&nbsp;Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView11" runat="server" oncheckedchanged="chkView11_CheckedChanged"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd11" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit11" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel11" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;3.</TD>
																<TD>Customer Data Mining</TD>
																<TD align="center">
																	<asp:checkbox id="chkView12" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd12" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit12" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel12" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;4.</TD>
																<TD>Customer Mechanic Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView13" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd13" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit13" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel13" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;5.</TD>
																<TD>Customer Mapping</TD>
																<TD align="center">
																	<asp:checkbox id="chkView14" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd14" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit14" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel14" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;6.</TD>
																<TD>Customer Type</TD>
																<TD align="center">
																	<asp:checkbox id="chkView15" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd15" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit15" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel15" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;7.</TD>
																<TD>Customer Updation</TD>
																<TD align="center">
																	<asp:checkbox id="chkView16" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd16" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit16" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel16" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;8.</TD>
																<TD>Fleet/OE Discount Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView17" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd17" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit17" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel17" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;9.</TD>
																<TD>Market Customer Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView18" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd18" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit18" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel18" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;10.</TD>
																<TD>Sales Person Assignment</TD>
																<TD align="center">
																	<asp:checkbox id="chkView66" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd66" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit66" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel66" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;11.</TD>
																<TD>Scheme/Discount Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView67" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd67" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit67" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel67" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;12.</TD>
																<TD>Vendor&nbsp;Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView68" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd68" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit68" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel68" runat="server"></asp:checkbox></TD>
															</TR>
														</TABLE>
													</asp:panel></TD>
											</TR>
											<TR>
												<TD colSpan="6"><asp:checkbox id="chkInventory" runat="server" AutoPostBack="True" ForeColor="#990033" Text="Purchase / Sales/ Inventory Module" oncheckedchanged="chkInventory_CheckedChanged"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="6"><asp:panel id="PanelInventory" runat="server" Width="512px" Visible="False" DESIGNTIMEDRAGDROP="1043">
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD width="5%"></TD>
																<TD width="59%"><STRONG></STRONG></TD>
																<TD align="center" width="9%"><STRONG>View</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Add</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Edit</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Del</STRONG></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;1.
																</TD>
																<TD>Product Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView19" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd19" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit19" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel19" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;2.</TD>
																<TD>Price Updation</TD>
																<TD align="center">
																	<asp:checkbox id="chkView20" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd20" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit20" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel20" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;3.</TD>
																<TD>Purchase Invoice</TD>
																<TD align="center">
																	<asp:checkbox id="chkView21" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd21" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit21" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel21" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;4.</TD>
																<TD>Sales Invoice</TD>
																<TD align="center">
																	<asp:checkbox id="chkView22" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd22" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit22" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel22" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;5.</TD>
																<TD>Performa Invoice</TD>
																<TD align="center">
																	<asp:checkbox id="chkView23" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd23" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit23" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel23" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;6.</TD>
																<TD>Sales Return</TD>
																<TD align="center">
																	<asp:checkbox id="chkView24" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd24" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit24" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel24" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;7.</TD>
																<TD>Purchase Return</TD>
																<TD align="center">
																	<asp:checkbox id="chkView25" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd25" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit25" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel25" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;8.</TD>
																<TD>Stock Adjustment
																</TD>
																<TD align="center">
																	<asp:checkbox id="chkView26" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd26" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit26" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel26" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;9.</TD>
																<TD>Customer Ledger Updation
																</TD>
																<TD align="center">
																	<asp:checkbox id="chkView27" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd27" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit27" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel27" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;10.</TD>
																<TD>Stock Ledger Updation</TD>
																<TD align="center">
																	<asp:checkbox id="chkView44" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd44" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit44" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel44" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;11.</TD>
																<TD>LY_PS_Sales</TD>
																<TD align="center">
																	<asp:checkbox id="chkView64" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd64" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit64" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel64" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;12.</TD>
																<TD>Order Collection</TD>
																<TD align="center">
																	<asp:checkbox id="chkView69" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd69" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit69" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel69" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;13.</TD>
																<TD>Modvat/Cenvat Invoice</TD>
																<TD align="center">
																	<asp:checkbox id="chkView70" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd70" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit70" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel70" runat="server"></asp:checkbox></TD>
															</TR>
														</TABLE>
													</asp:panel></TD>
											</TR>
											<TR>
												<TD colSpan="6"><asp:checkbox id="chkAdmin" runat="server" AutoPostBack="True" ForeColor="#990033" Text="Admin Module" oncheckedchanged="chkAdmin_CheckedChanged"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="6"><asp:panel id="PanelAdmin" runat="server" Width="512px" Visible="False">
														<P>
															<TABLE cellSpacing="0" cellPadding="0" width="100%">
																<TR>
																	<TD width="5%"></TD>
																	<TD width="59%"><STRONG></STRONG></TD>
																	<TD align="center" width="9%"><STRONG>View</STRONG></TD>
																	<TD align="center" width="9%"><STRONG>Add</STRONG></TD>
																	<TD align="center" width="9%"><STRONG>Edit</STRONG></TD>
																	<TD align="center" width="9%"><STRONG>Del</STRONG></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;1.</TD>
																	<TD>Attandance Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView28" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd28" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit28" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel28" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;2.</TD>
																	<TD>Balance Sheet</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView29" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd29" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit29" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel29" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;3.</TD>
																	<TD>Bank Reconciliation</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView30" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd30" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit30" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel30" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;4.</TD>
																	<TD>Batch Wise Stock</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView31" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd31" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit31" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel31" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;5.</TD>
																	<TD>Batch Wise Stock Ledger</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView32" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd32" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit32" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel32" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;6.</TD>
																	<TD>Claim Analysis</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView33" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd33" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit33" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel33" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;7.</TD>
																	<TD>Credit Analysis Sheet</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView34" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd34" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit34" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel34" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;8.</TD>
																	<TD>Customer Bill Ageing</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView35" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd35" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit35" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel35" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;9.</TD>
																	<TD>Customer Wise Outstanding</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView36" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd36" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit36" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel36" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;10.</TD>
																	<TD>Customer Wise Sales</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView37" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd37" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit37" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel37" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;11.</TD>
																	<TD>Day Book Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView38" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd38" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit38" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel38" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;12.</TD>
																	<TD>District Wise Channel Sales</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView39" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd39" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit39" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel39" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;13.</TD>
																	<TD>Document Cancellation</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView40" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd40" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit40" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel40" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;14.</TD>
																	<TD>Fleet/OE Discount Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView41" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd41" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit41" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel41" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;15.</TD>
																	<TD>Leave Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView42" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd42" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit42" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel42" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;16.</TD>
																	<TD>Ledger Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView43" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd43" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit43" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel43" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;17.</TD>
																	<TD>Lube Indent Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView45" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd45" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit45" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel45" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;18.</TD>
																	<TD>PY_PS_Sales Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView46" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd46" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit46" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel46" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;19.</TD>
																	<TD>Market Potential Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView47" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd47" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit47" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel47" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;20.</TD>
																	<TD>Master List</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView48" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd48" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit48" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel48" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;21.</TD>
																	<TD>Mechanic Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView49" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd49" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit49" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel49" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;22.</TD>
																	<TD>Monthly Claim Latter</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView50" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd50" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit50" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel50" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;23.</TD>
																	<TD>Mont. Cust. Secon. Sales</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView54" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd54" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit54" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel54" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;24.</TD>
																	<TD>Mont. Prod. Secon. Sales</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView55" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd55" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit55" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel55" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;25.</TD>
																	<TD>Party Wise Sales Figure</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView56" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd56" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit56" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel56" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;26.</TD>
																	<TD>Price Calculation</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView57" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd57" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit57" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel57" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;27.</TD>
																	<TD>Price List</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView58" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd58" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit58" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel58" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;28.</TD>
																	<TD>Primary/Secon Sales Analysis</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView59" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd59" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit59" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel59" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;29.</TD>
																	<TD>Primary/SecDiscount Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView60" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd60" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit60" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel60" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;30.</TD>
																	<TD>Product Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView61" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd61" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit61" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel61" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;31.</TD>
																	<TD>Product Wise Sales</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView62" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd62" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit62" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel62" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;32.</TD>
																	<TD>Profit Analysis Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView63" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd63" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit63" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel63" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;33.</TD>
																	<TD>Propose Lube Indent</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView65" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd65" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit65" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel65" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;34.</TD>
																	<TD>Purchase Book</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView71" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd71" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit71" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel71" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;35.</TD>
																	<TD>PurchaseListIOCL Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView72" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd72" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit72" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel72" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;36.</TD>
																	<TD>Sales Book</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView73" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd73" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit73" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel73" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;37.</TD>
																	<TD>Scheme List Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView74" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd74" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit74" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel74" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;38.</TD>
																	<TD>Secon. Sales Claim Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView76" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd76" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit76" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel76" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;39.</TD>
																	<TD>Servo Sadbhavna List Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView77" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd77" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit77" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel77" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;40.</TD>
																	<TD>SD. Scheme Month Wise Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView78" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd78" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit78" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel78" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;41.</TD>
																	<TD>SD. Scheme Year Wise Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView79" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd79" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit79" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel79" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;42.</TD>
																	<TD>SSR Incentive Sheet</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView80" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd80" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit80" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel80" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;43.</TD>
																	<TD>SSR Performance</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView81" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd81" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit81" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel81" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;44.</TD>
																	<TD>SSR Wise Sales Figure</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView82" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd82" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit82" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel82" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;45.</TD>
																	<TD>Stock Ledger Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView83" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd83" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit83" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel83" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;46.</TD>
																	<TD>Stock Movement</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView84" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd84" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit84" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel84" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;47.</TD>
																	<TD>Stock Reordering Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView85" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd85" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit85" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel85" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;48.</TD>
																	<TD>Stock Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView86" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd86" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit86" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel86" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;49.</TD>
																	<TD>Target Vs Achievement</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView87" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd87" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit87" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel87" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;50.</TD>
																	<TD>Trading Account</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView88" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd88" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit88" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel88" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;51.</TD>
																	<TD>Vat Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView89" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd89" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit89" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel89" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;52.</TD>
																	<TD>Vehicle Log Book Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView90" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd90" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit90" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel90" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;53.</TD>
																	<TD>Nill Selling Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView91" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd91" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit91" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel91" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;54.</TD>
																	<TD>SRSM Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView92" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd92" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit92" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel92" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;55.</TD>
																	<TD>Return Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView93" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd93" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit93" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel93" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;56.</TD>
																	<TD>RO-Bazzar Reward Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView94" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd94" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit94" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel94" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;57.</TD>
																	<TD>BackOrder Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView95" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd95" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit95" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel95" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;58.</TD>
																	<TD>MS/HSD Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView96" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd96" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit96" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel96" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;59.</TD>
																	<TD>Customer Data Mining Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView97" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd97" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit97" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel97" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;60.</TD>
																	<TD>SSR Wise Traget Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView98" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd98" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit98" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel98" runat="server"></asp:checkbox></TD>
																</TR>
																<TR>
																	<TD align="center">&nbsp;61.</TD>
																	<TD>Secondry Sales Report</TD>
																	<TD align="center">
																		<asp:checkbox id="chkView99" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkAdd99" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkEdit99" runat="server"></asp:checkbox></TD>
																	<TD align="center">
																		<asp:checkbox id="chkDel99" runat="server"></asp:checkbox></TD>
																</TR>
															</TABLE>
														</P>
													</asp:panel></TD>
											</TR>
											<TR>
												<TD colSpan="6"><asp:checkbox id="checkLogistics" runat="server" AutoPostBack="True" ForeColor="#990033" Text="Logistics Module" oncheckedchanged="checkLogistics_CheckedChanged"></asp:checkbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="6"><asp:panel id="panelLogistics" runat="server" Visible="False" Width="512px"><!--P-->
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD width="5%"></TD>
																<TD width="59%"></TD>
																<TD align="center" width="9%"><STRONG>View</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Add</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Edit</STRONG></TD>
																<TD align="center" width="9%"><STRONG>Del</STRONG></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;1.
																</TD>
																<TD>Route Master</TD>
																<TD align="center">
																	<asp:checkbox id="chkView51" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd51" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit51" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel51" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;2.</TD>
																<TD>Vehicle Daily Log Book</TD>
																<TD align="center">
																	<asp:checkbox id="chkView52" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd52" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit52" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel52" runat="server"></asp:checkbox></TD>
															</TR>
															<TR>
																<TD align="center">&nbsp;3.</TD>
																<TD>Vehicle Entry</TD>
																<TD align="center">
																	<asp:checkbox id="chkView53" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkAdd53" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkEdit53" runat="server"></asp:checkbox></TD>
																<TD align="center">
																	<asp:checkbox id="chkDel53" runat="server"></asp:checkbox></TD>
															</TR>
														</TABLE> <!--/P--></asp:panel></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="center"><asp:button id="btnAllocate" runat="server" Text="Allocate Privileges" onclick="btnAllocate_Click"></asp:button>
                                                       <asp:button id="btnSave" runat="server" Width="80px" Text="Save" onclick="btnSave_Click"></asp:button>
								    </TD>
								</TR>
							</TABLE>
						</td>
					</tr>
				</TBODY>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
