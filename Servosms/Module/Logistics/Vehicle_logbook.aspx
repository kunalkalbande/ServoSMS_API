<%@ Register TagPrefix="uc1" TagName="Header" Src="../../HeaderFooter/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../HeaderFooter/Footer.ascx" %>
<%@ Page language="c#" Inherits="Servosms.Module.Logistics.Vehicle_logbook" CodeFile="Vehicle_logbook.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ServoSMS: Vehicle Daily Log Book</title> <!--
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
		function getVehicleInfo(t)
		{
		var index = t.selectedIndex;
		var typetext = t.options[index].text;
		//alert(typetext);
		var temp = document.all.txtHidden.value;
		var mainArr = new Array();
		mainArr = temp.split("#");
		
		var t1="";
		for(var i=0; i<mainArr.length;i++)
		 {
	      t1 = mainArr[i];
	     // alert(t1);
	      var secArr = new Array();
		   secArr = t1.split("~");
		 //  alert(secArr[0])
		   for(var j = 0;j<secArr.length;j=j+4)
		   {
		 //  alert("j=="+j)
		    //   alert(secArr[j])
		       if( typetext == secArr[j])
		       {
		  
		           document.all.txtVehiclename.value = secArr[j + 1]
		           document.all.txtdrivername.value = secArr[j + 2]
		           document.all.txtmeterreadpre.value = secArr[j + 3]
		          break
		       }
		   
		   } 
		   
		
		 }  
		}
		</script>
	</HEAD>
	<body>
		<form method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header><INPUT id="txtHidden" style="WIDTH: 8px; HEIGHT: 16px" type="hidden" size="1" name="txtHidden"
				runat="server">
			<table width="900" align="center" border="0">
				<TR>
					<TH align="center">
						<font color="#ce4848">Vehicle Daily Log Book</font>
						<hr>
						</FONT></TH></TR>
				<tr>
					<td align="center">
						<TABLE id="Table1" style="WIDTH: 850px" cellSpacing="0" cellPadding="0" border="1">
							<TR>
								<TD colSpan="4"><asp:label id="Label4" runat="server" ForeColor="Red">asterisk (*) fields are mandatory</asp:label></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 68px">&nbsp;VDLB ID</TD>
								<TD style="WIDTH: 266px"><asp:label id="lblVDLBID" runat="server" ForeColor="Blue"></asp:label><asp:dropdownlist id="DropVDLBID" runat="server" Visible="False" AutoPostBack="True" CssClass="fontstyle" onselectedindexchanged="DropVDLBID_SelectedIndexChanged">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist><asp:button id="btnEdit1" runat="server" Width="35px" Text="..." ToolTip="Click here for Edit"
										CausesValidation="False" onclick="btnEdit1_Click"></asp:button></TD>
								<TD  align="left">&nbsp; Vehicle No.
									<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Vehicle No."
										ControlToValidate="DropVehicleNo" InitialValue="Select">*</asp:requiredfieldvalidator>&nbsp;<FONT color="#ff0000">*</FONT>
									
									<asp:dropdownlist id="DropVehicleNo" runat="server" Width="135px" onchange="return getVehicleInfo(this);"
										CssClass="fontstyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
									</asp:dropdownlist></TD>
								<TD> &nbsp; Vehicle&nbsp;Name
									
									<asp:textbox id="txtVehiclename" runat="server" Width="132px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 340px" colSpan="2">&nbsp;DOE (Date of Entry)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!--/TD>
								<TD style=" WIDTH: 273px; HEIGHT: 19px"--><asp:textbox id="txtDOE" runat="server" Width="105px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
									<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Vechile_entryform.txtDOE);return false;">
										&nbsp;</A></TD>
								<TD colSpan="2">&nbsp;Driver's Name&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:textbox id="txtdrivername" runat="server" Width="160px" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 340px" colSpan="2">&nbsp;MeterReading(PreviousDay)&nbsp; 
									<!--/TD>
								<TD style=" WIDTH: 273px; HEIGHT: 26px"--><asp:textbox id="txtmeterreadpre" runat="server" Width="105" ReadOnly="True" BorderStyle="Groove"
										CssClass="fontstyle"></asp:textbox></TD>
								<TD colSpan="2">&nbsp;Meter Reading (Current Day)<FONT color="#ff0033">*</FONT>
									<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Current Meter Reading"
										ControlToValidate="txtmeterreadcurr">*</asp:requiredfieldvalidator><asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtmeterreadcurr"
										runat="server" Width="95px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 68px">&nbsp;Vehicle&nbsp;Route</TD>
								<TD style="WIDTH: 266px"><asp:dropdownlist id="Dropvehicleroute" runat="server" Width="188px" CssClass="fontstyle"></asp:dropdownlist></TD>
								<!--td style="WIDTH: 122px">Fuel Used</td-->
								<TD style="WIDTH: 350px" colSpan="2">&nbsp;Fuel Used <FONT color="#ff0000">*</FONT><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Fuel Used"
										ControlToValidate="Dropfuelused" InitialValue="Select">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="Dropfuelused" runat="server" Width="188px" CssClass="fontstyle">
										<asp:ListItem Value="Select">Select</asp:ListItem>
										<asp:ListItem Value="Petrol(MS)">Petrol(MS)</asp:ListItem>
										<asp:ListItem Value="Deisel(HSD)">Deisel(HSD)</asp:ListItem>
									</asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter Fuel Used quantity"
										ControlToValidate="txtfuelused">*</asp:requiredfieldvalidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									Qty:&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfuelused" runat="server"
										Width="50px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 68px" vAlign="top">&nbsp;Engine Oil</TD>
								<TD style="WIDTH: 266px" vAlign="top"><asp:dropdownlist id="Dropengineoil" runat="server" Width="188px" CssClass="fontstyle"></asp:dropdownlist>
									Qty<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtengineqty" runat="server"
										Width="49px" Height="20px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<!--td style="WIDTH: 122px" >GearOil</td-->
								<TD style="WIDTH: 350px" vAlign="top" colSpan="2">&nbsp;GearOil&nbsp;
									<asp:dropdownlist id="Dropgearoil" runat="server" Width="245px" CssClass="fontstyle"></asp:dropdownlist>Qty:&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtGearqty" runat="server"
										Width="50" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 68px" vAlign="top">&nbsp;Brake Oil</TD>
								<TD style="WIDTH: 266px" vAlign="top"><asp:dropdownlist id="Dropbrakeoil" runat="server" Width="188" CssClass="fontstyle"></asp:dropdownlist>&nbsp;Qty<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtBrakeqty" runat="server"
										Width="49px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>&nbsp;&nbsp;&nbsp;</TD>
								<!--td style="WIDTH: 122px">Coolent</td-->
								<TD style="WIDTH: 350px" vAlign="top" colSpan="2">&nbsp;Coolent&nbsp;
									<asp:dropdownlist id="Dropcoolent" runat="server" Width="245px" CssClass="fontstyle"></asp:dropdownlist>Qty:&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtCoolentqty"
										runat="server" Width="49px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
							</TR>
							<tr>
								<td style="WIDTH: 68px">&nbsp;Grease&nbsp;&nbsp;</td>
								<td style="WIDTH: 266px" vAlign="top"><asp:dropdownlist id="Dropgrease" runat="server" Width="187px" CssClass="fontstyle"></asp:dropdownlist>&nbsp;Qty<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtGreaseqty" runat="server"
										Width="49" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
								</td>
								<!--td  style="WIDTH: 122px">Trans.Oil</td-->
								<td style="WIDTH: 350px" vAlign="top" colSpan="2">&nbsp;Trans.Oil&nbsp;
									<asp:dropdownlist id="Droptranoil" runat="server" Width="237px" CssClass="fontstyle"></asp:dropdownlist>Qty:&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtTranqty" runat="server"
										Width="49px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></td>
							</tr>
							<TR>
								<TD style="WIDTH: 68px">&nbsp;Other Exp.in&nbsp; Rupees</TD>
								<TD style="WIDTH: 266px">&nbsp;Toll&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtTollqty" runat="server"
										Width="80px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
									Misc.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtMiscqty" runat="server"
										Width="80px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox></TD>
								<TD colSpan="2">&nbsp;Police&nbsp;<A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Vechile_entryform.txtfueluseddt);return false;"></A><A onclick="if(self.gfPop)gfPop.fPopCalendar(document.Vechile_entryform.txtfueluseddt);return false;"></A>&nbsp;&nbsp;
									<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtPoliceqty" runat="server"
										Width="100px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>&nbsp;&nbsp;Food 
									&nbsp;<asp:textbox onkeypress="return GetOnlyNumbers(this, event, false,true);" id="txtfoodqty" runat="server"
										Width="100px" BorderStyle="Groove" CssClass="fontstyle"></asp:textbox>
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="5" height="10"><asp:button id="btnSave" runat="server"  Width="75px" Text="Save" 
                                     onclick="btnSave_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnEdit" runat="server"  Width="75px" Text="Edit" 
										 onclick="btnEdit_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnDelete" runat="server"  Width="75px" Text="Delete" 
										 onclick="btnDelete_Click"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:button id="btnPrint" runat="server"  Width="75px" Text="Print" onclick="btnPrint_Click"></asp:button></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td colSpan="5" height="5"><asp:validationsummary id="vsVehicle_log" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:validationsummary></td>
				</tr>
			</table>
			<iframe id="gToday:contrast:agenda.js" style="Z-INDEX: 101; LEFT: -500px; VISIBILITY: visible; POSITION: absolute; TOP: 0px"
				name="gToday:contrast:agenda.js" src="../../HeaderFooter/DTPicker/ipopeng.htm" frameBorder="0" width="174"
				scrolling="no" height="189"></iframe>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>
