<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Inventory.Credit_Bill" CodeFile="Credit_Bill.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Credit Bill</title> <!--
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
	</HEAD>
	<body onkeydown="change(event)">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><asp:textbox id="TextBox1" style="Z-INDEX: 102; LEFT: 528px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Enabled="False" BorderStyle="None" ReadOnly="True"></asp:textbox><asp:textbox id="TextBox2" style="Z-INDEX: 103; LEFT: 152px; POSITION: absolute; TOP: 8px" runat="server"
				Visible="False" Width="8px"></asp:textbox>
			<table height="278" width="764" align="center">
				<TR valign="top">
					<TD colSpan="3" align="center">
						<table height="6" border="0">
							<tr valign="top">
								<td vAlign="top"><asp:image id="imgSample" runat="server" Width="48px" Height="51px"></asp:image></td>
								<td vAlign="top" align="center"><asp:label id="txtname1" runat="server" Width="472px" Height="8px" Font-Size="X-Small" Font-Bold="True"></asp:label><br>
									<asp:label id="txtdet1" runat="server" Width="472px" Font-Size="8pt" Font-Bold="True"></asp:label><br>
									<asp:label id="txtadd1" runat="server" Width="472px" Font-Size="8pt" Font-Bold="True"></asp:label><br>
									<asp:label id="txtci11" Width="224px" Font-Size="8pt" Font-Bold="True" Runat="server"></asp:label><br>
									<!--<P>-->
									<!--	<table style="WIDTH: 487px; HEIGHT: 90px" border=0>
              <TBODY>
											<tr>
												<td style="HEIGHT: 8px"><asp:label id="txtname" runat="server" Width="472px" Font-Size="X-Small" Font-Bold="True" Height="8px"></asp:label></td>
											</tr>
											<TR>
												<td style="HEIGHT: 10pt"><asp:label id="txtdet" runat="server" Width="472px" Font-Size="10pt"></asp:label></td>
											</TR>
											<tr>
												<td style="HEIGHT: 9pt"><asp:label id="txtadd" runat="server" Width="472px" Font-Size="9pt"></asp:label></td>
											</tr>
											<tr>
												<td style="HEIGHT: 7pt"><asp:label id="txtci1" Width="224px" Font-Size="7pt" Runat="server"></asp:label></td>
											</tr>
											<!--<tr>
												<td height=10></td>
											</tr>
										</table>-->
									<!--</P>--></td>
								</FONT></FONT></tr>
						</table>
						<!-- <P></P>-->
						<hr align="center">
					</TD>
				</TR>
				<TR valign="top" height="10">
					<TD align="right" style="WIDTH: 97px">Bill No:
					</TD>
					<TD align="left" style="WIDTH: 540px"><asp:label id="lblBillNo" runat="server" Width="95%" ForeColor="Blue"></asp:label>Date:</TD>
					<TD><asp:label id="lblDate" runat="server"></asp:label></TD>
				</TR>
				<TR valign="top" height="15">
					<TD style="WIDTH: 97px"></TD>
					<TD align="left" style="WIDTH: 540px">
						<P>&nbsp;&nbsp; From&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtDateFrom" runat="server" ReadOnly="True" Width="97px" ontextchanged="txtDateFrom_TextChanged"></asp:textbox><A onClick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateFrom);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp; 
							To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;<asp:textbox id="txtDateTO" runat="server" ReadOnly="True" Width="99px"></asp:textbox><A onClick="if(self.gfPop)gfPop.fPopCalendar(document.Form1.txtDateTO);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg"
									align="absMiddle" border="0"></A>
							<asp:button id="printBtn" runat="server" Width="82px" Text="Save &amp; Print" 
								 onclick="printBtn_Click"></asp:button></P>
					</TD>
					<TD></TD>
				</TR>
				<TR valign="top" height="15">
					<TD align="right" style="WIDTH: 97px">&nbsp;</TD>
					<TD align="left" width="540" style="WIDTH: 540px">&nbsp;&nbsp;&nbsp;M/s <FONT color="#ff0000">
							*</FONT>
						<asp:comparevalidator id="CompareValidator7" runat="server" ErrorMessage="Please Select Firm Name" ValueToCompare="Select"
							ControlToValidate="DropCustID" Operator="NotEqual">*</asp:comparevalidator><FONT color="red">&nbsp;</FONT>
						<asp:dropdownlist id="DropCustID" runat="server" Width="190px" AutoPostBack="True" onselectedindexchanged="DropCustID_SelectedIndexChanged">
							<asp:ListItem Value="Select">Select</asp:ListItem>
						</asp:dropdownlist>
						&nbsp;&nbsp;Vehicle No.
						<asp:DropDownList id="DropVehicleNo" runat="server" Width="130px" AutoPostBack="True" onselectedindexchanged="DropVehicleNo_SelectedIndexChanged">
							<asp:ListItem Value="All">All</asp:ListItem>
						</asp:DropDownList></TD>
					<TD></TD>
				</TR>
				<tr height="110" valign="top">
					<td style="WIDTH: 97px"></td>
					<td align="center" style="WIDTH: 540px"><asp:datagrid id="GridCreditBill" runat="server" BorderStyle="None" CellPadding="1" BackColor="#DEBA84"
							BorderWidth="0px" BorderColor="#DEBA84" AutoGenerateColumns="False" Height="61px" Width="548px" CellSpacing="1" onselectedindexchanged="GridCreditBill_SelectedIndexChanged">
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
							<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
							<HeaderStyle Font-Size="Large" Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#CE4848"></HeaderStyle>
							<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
							<Columns>
								<asp:BoundColumn DataField="invoice_date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}">
									<HeaderStyle Width="60px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="slip_no" HeaderText="Slip No.">
									<HeaderStyle Width="60px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="prod_Name" HeaderText="Particulars">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="qty" HeaderText="Qty">
									<HeaderStyle Width="40px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="rate" HeaderText="Rate">
									<HeaderStyle Width="40px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="amount" HeaderText="Amount" DataFormatString="{0:N2}">
									<HeaderStyle Width="50px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="vehicle_no" HeaderText="Vehicle No.">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
					<td></td>
				</tr>
				<TR>
					<TD style="WIDTH: 97px"></TD>
					<TD align="left" style="WIDTH: 540px"><asp:validationsummary id="ValidationSummary1" runat="server" ShowSummary="False" ShowMessageBox="True"
							Height="7px"></asp:validationsummary>
						<p><FONT color="#ff0033"><STRONG><U>Note</U> :</STRONG></FONT> Only Vehicle numbers 
							entered through Customer Vehicle Entry are populated in the above dropdown.<br>
							&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Vehicle number 
							entered manually through Sales Invoice are not populated.</p>
					</TD>
					<TD></TD>
				</TR>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0"
				width="174" scrolling="no"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
