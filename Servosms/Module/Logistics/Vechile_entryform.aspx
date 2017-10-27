<%@ Reference Page="~/Module/Logistics/Vehicle.aspx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Logistics.Vechile_entryform" CodeFile="Vechile_entryform.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Vehicle Entry</title> <!--
   Copyright (c) 2005 bbnisys Technologies. All Rights Reserved.
  
   No part of this software shall be reproduced, stored in a 
   retrieval system, or transmitted by any means, electronic 
   mechanical, photocopying, recording  or otherwise, or for
   any  purpose  without the express  written  permission of
   bbnisys Technologies.
-->
		<meta content="True" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" id="Validations" src="../../Sysitem/JS/Validations.js"></script>
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
	    <style type="text/css">
            .auto-style1 {
                width: 349px;
            }
            .auto-style2 {
                height: 26px;
                width: 349px;
            }
        </style>
	</HEAD>
	<body onkeydown="change(event)">
		<form method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table height="228" width="778" align="center" cellpadding="0" cellspacing="0">
				<TR>
					<TH align="center">
						<font color="#ce4848">Vehicle Entry</font>
						<hr>
						</FONT></TH></TR>
				<tr>
					<td align="center">
						<TABLE id="Table1" border="1" cellpadding="0" cellspacing="0">
							<TR>
								<TD colSpan="4"><asp:label id="Label4" runat="server" ForeColor="Red">asterisk (*) fields are mandatory</asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="1">&nbsp;Vehicle ID</TD>
								<TD align="left" colSpan="3">
									<asp:Label id="lblVehicleID" runat="server" Width="100px" Height="20px" ForeColor="Blue"></asp:Label><asp:dropdownlist id="DropVehicleID" runat="server" Width="60px" Visible="False" AutoPostBack="True"
										CssClass="fontstyle" onselectedindexchanged="DropVehicleID_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist>
									<asp:Button id="btnEdit1" runat="server" Width="35px" Text="..." ToolTip="Click here for edit"
										CausesValidation="False"  onclick="btnEdit1_Click"></asp:Button></TD>
							</TR>
							<TR>
								<TD>&nbsp;Vehicle's Type</TD>
								<TD class="auto-style1"><asp:dropdownlist id="DropVechileType2" runat="server" Width="100px" CssClass="fontstyle" onselectedindexchanged="DropVechileType2_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="2 Wheeler">2 Wheeler</asp:ListItem>
										<asp:ListItem Value="3 Wheeler">3 Wheeler</asp:ListItem>
										<asp:ListItem Value="Jeep">Jeep</asp:ListItem>
										<asp:ListItem Value="Lorry">Lorry</asp:ListItem>
										<asp:ListItem Value="Tanker">Tanker</asp:ListItem>
										<asp:ListItem Value="Truck">Truck</asp:ListItem>
										<asp:ListItem Value="Van">Van</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD>&nbsp;Vehicle No <FONT color="#ff0033" size="2">*</FONT></TD>
								<TD><asp:textbox id="txtVehicleno" runat="server" Width="100px" MaxLength="15" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><asp:requiredfieldvalidator id="RefLnm" Runat="server" Display="Dynamic" ErrorMessage="You Must Enter Vechicle No"
										ControlToValidate="txtVehicleno">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD>&nbsp;Vehicle Name</TD>
								<TD class="auto-style1"><asp:textbox id="txtVehiclenm" runat="server" Width="160px" MaxLength="49" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;RTO Registration Validity&nbsp;</TD>
								<TD><asp:textbox id="txtrtoregvalidity" ReadOnly="True" runat="server" Width="100px" MaxLength="12"
										BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtrtoregvalidity);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A><asp:comparevalidator id="Comparevalidator1" Runat="server" Display="Dynamic" ErrorMessage="Rto registration validity must be numeric."
										ControlToValidate="txtrtoregvalidity" Operator="DataTypeCheck" Enabled="False">*</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD>&nbsp;Model Name</TD>
								<TD class="auto-style1"><asp:textbox id="txtmodelnm" runat="server" Width="160px" MaxLength="49" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;RTO Registration No. <FONT color="#ff0033" size="2">*</FONT></TD>
								<TD><asp:textbox id="txtrtono" runat="server" Width="100px" MaxLength="20" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="You Must Enter R.T.O Registration No"
										ControlToValidate="txtrtono">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD>&nbsp;Vehicle Manufact. Date</TD>
								<TD class="auto-style1"><asp:textbox id="txtVehicleyear" runat="server" Width="160px" MaxLength="12" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;Insurance No.</TD>
								<TD><asp:textbox id="txtinsuranceno" runat="server" Width="100px" MaxLength="20" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Meter Reading (K.M.)</TD>
								<TD class="auto-style1"><asp:textbox id="txtVehiclemreading" runat="server" Width="160px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><asp:comparevalidator id="Comparevalidator4" Runat="server" Display="Dynamic" ErrorMessage="Meter reading must be numeric."
										ControlToValidate="txtVehiclemreading" Operator="DataTypeCheck" Type="Integer">*</asp:comparevalidator></TD>
								<TD>&nbsp;Insurance Validity</TD>
								<TD><asp:textbox id="txtvalidityinsurance" runat="server" ReadOnly="True" Width="100px" MaxLength="12"
										BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtvalidityinsurance);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
							</TR>
							<TR>
								<TD>&nbsp;Vehicle Route</TD>
								<TD class="auto-style1"><asp:dropdownlist id="DropDownList1" runat="server" Width="180px" CssClass="fontstyle"></asp:dropdownlist></TD>
								<TD>&nbsp;Insurance Company Name</TD>
								<TD><asp:textbox id="txtInsCompName" runat="server" Width="100px" MaxLength="49" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><asp:comparevalidator id="Comparevalidator6" Runat="server" Display="Dynamic" ErrorMessage="Driver salary must be numeric."
										ControlToValidate="txtInsCompName" Operator="DataTypeCheck">*</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD>&nbsp;Fuel Used ( Petrol/Deisel )</TD>
								<TD class="auto-style1"><asp:dropdownlist id="DropFuelused" runat="server" Width="120px" CssClass="fontstyle" onselectedindexchanged="DropFuelused_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Petrol(MS)">Petrol(MS)</asp:ListItem>
										<asp:ListItem Value="Deisel(HSD)">Deisel(HSD)</asp:ListItem>
									</asp:dropdownlist><asp:textbox id="txtfuelinword" runat="server" Width="110px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;Starting Fuel Qty.
								</TD>
								<TD>
									<asp:textbox id="txtfuelintank" runat="server" Width="100px" MaxLength="12" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="5"><b><B><SPAN style="FONT-SIZE: 8pt; FONT-FAMILY: Arial; mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: EN-US; mso-fareast-language: EN-US; mso-bidi-language: AR-SA">Fuel 
												/ Lubricants Uses</SPAN></B></b></TD>
							</TR>
							<TR>
								<TD colSpan="1" rowSpan="1">&nbsp;Engine Oil</TD>
								<TD class="auto-style1"><asp:dropdownlist id="DropEngineOil" runat="server" Width="250px" CssClass="fontstyle"></asp:dropdownlist><asp:textbox id="txtEngineQty" runat="server" Width="160px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;Date
									<asp:textbox id="txtEngineOilDate" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtEngineOilDate);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD vAlign="top">&nbsp;K.M&nbsp;<asp:textbox id="txtEngineKM" onkeypress="return GetOnlyNumbers(this, event);" runat="server"
										Width="100px" MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD>&nbsp;Gear Oil</TD>
								<TD class="auto-style1"><asp:dropdownlist id="Dropgear" runat="server" Width="250px" CssClass="fontstyle" onselectedindexchanged="Dropgear_SelectedIndexChanged"></asp:dropdownlist><asp:textbox id="txtgearinword" runat="server" Width="160px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;Date
									<asp:textbox id="txtgeardt" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtgeardt);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD vAlign="top">&nbsp;K.M&nbsp;<asp:textbox id="txtgearkm" onkeypress="return GetOnlyNumbers(this, event);" runat="server" Width="100px"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD>&nbsp;Brake Oil</TD>
								<TD class="auto-style1"><asp:dropdownlist id="Dropbreak" runat="server" Width="250px" CssClass="fontstyle"></asp:dropdownlist><asp:textbox id="txtbearkinword" runat="server" Width="160px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;Date
									<asp:textbox id="txtbreakdt" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtbreakdt);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD>&nbsp;K.M&nbsp;<asp:textbox id="txtbreakkm" onkeypress="return GetOnlyNumbers(this, event);" runat="server"
										Width="100px" MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD>&nbsp;Coolent</TD>
								<TD class="auto-style1"><asp:dropdownlist id="Dropcoolent" runat="server" Width="180px" CssClass="fontstyle"></asp:dropdownlist><asp:textbox id="txtcoolentinword" runat="server" Width="110px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD vAlign="top">&nbsp;Date
									<asp:textbox id="txtcoolentdt" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtcoolentdt);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD vAlign="top">&nbsp;K.M&nbsp;<asp:textbox id="txtcoolentkm" onkeypress="return GetOnlyNumbers(this, event);" runat="server"
										Width="100px" MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR style="HEIGHT: 17px">
								<TD>&nbsp;Grease</TD>
								<TD height="26" class="auto-style2"><asp:dropdownlist id="Dropgrease" runat="server" Width="250px" CssClass="fontstyle"></asp:dropdownlist><asp:textbox id="txtgreaseinword" runat="server" Width="160px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
								</TD>
								<TD>&nbsp;Date
									<asp:textbox id="txtgreasedt" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txtgreasedt);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD>&nbsp;K.M&nbsp;<asp:textbox id="txtgreasekm" onkeypress="return GetOnlyNumbers(this, event);" runat="server"
										Width="100px" MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Transmission Oil</TD>
								<TD class="auto-style1"><asp:dropdownlist id="Droptransmission" runat="server" Width="180px" CssClass="fontstyle"></asp:dropdownlist><asp:textbox id="txttransinword" runat="server" Width="110px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD>&nbsp;Date
									<asp:textbox id="txttransmissiondt" runat="server" Width="100px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.all.txttransmissiondt);return false;"><IMG class="PopcalTrigger" alt="" src="../../HeaderFooter/DTPicker/calender_icon.jpg" align="absMiddle"
											border="0"></A></TD>
								<TD>&nbsp;K.M&nbsp;<asp:textbox id="txttransmissionkm" onkeypress="return GetOnlyNumbers(this, event);" runat="server"
										Width="100px" MaxLength="12" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>&nbsp;Vehicle Average</TD>
								<TD colSpan="3"><asp:textbox id="txtvechileavarge" runat="server" Width="150px" onkeypress="return GetOnlyNumbers(this, event, false,true);"
										BorderStyle="Groove" CssClass="fontstyle"></asp:textbox><asp:comparevalidator id="Comparevalidator19" Runat="server" Display="Dynamic" ErrorMessage="Vechile average must be numeric"
										ControlToValidate="txtvechileavarge" Operator="DataTypeCheck" Enabled="False">*</asp:comparevalidator>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="5"><asp:button id="btnSave" runat="server" Text="Save" Width="70px" 
										 onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button id="btnEdit" runat="server" Width="70px" Text="Edit" 
										 onclick="btnEdit_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button id="btnDelete" runat="server" Text="Delete"  Width="70px" onclick="btnDelete_Click"></asp:Button></TD>
							</TR>
							<tr>
								<td colSpan="5"><asp:validationsummary id="vsVehicle" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
							</tr>
						</TABLE>
					</td>
					<td></td>
				</tr>
			</table>
		</form>
		<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
			name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
			scrolling="no" height="189"></iframe>
		<!--<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
			name="gToday:contrast:agenda.js" src="../shareables/style/ipopeng.htm" frameBorder="0" width="174" scrolling="no" height="189">
		</iframe>--><uc1:footer id="Footer1" runat="server"></uc1:footer>
	</body>
</HTML>
